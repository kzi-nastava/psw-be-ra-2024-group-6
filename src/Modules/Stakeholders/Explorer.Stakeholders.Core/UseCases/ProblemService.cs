using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using FluentResults;
using Explorer.Stakeholders.API.Public.Administration;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.Problems;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.API.Internal;

namespace Explorer.Stakeholders.Core.UseCases
{
    public class ProblemService : CrudService<ProblemDto, Problem>, IProblemService
    {
        private readonly ICrudRepository<Problem> _crudRepository;
        private readonly IUserService userService;
        private readonly INotificationService notificationService;
        private readonly IProblemRepository repository;
        private readonly IMapper mapper;
        private readonly IInternalProblemTourAuthorService problemTourAuthorService;
        public ProblemService(ICrudRepository<Problem> crudRepository, IProblemRepository problemRepository,
            IMapper mapper, IUserService userService, IInternalProblemTourAuthorService problemTourAuthorService,
            INotificationService notificationService) : base(crudRepository, mapper)
        {
            _crudRepository = crudRepository;
            this.userService = userService;
            this.repository = problemRepository;
            this.problemTourAuthorService = problemTourAuthorService;
            this.mapper = mapper;
            this.notificationService = notificationService;
        }

        public Result Delete(int id, long userId)
        {
            try
            {
                Problem problem = repository.Get(id);
                if (userId != problem.TouristId)
                    return Result.Fail("you are not the creator of this problem");
                repository.Delete(id);
                return Result.Ok();
            }
            catch (KeyNotFoundException e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }
        }


        public Result<PagedResult<ProblemDto>> GetAll(long userId)
        {
            var result = GetPaged(1, 10);
            var filtered = new List<ProblemDto>();
            try
            {
                if (userService.Get(userId).Value.Role.Equals(UserRole.Tourist.ToString()))
                {
                    foreach (var problem in result.Value.Results)
                    {
                        if (problem.TouristId == userId)
                        {
                            filtered.Add(problem);
                        }
                    }
                    var r = new PagedResult<ProblemDto>(filtered, filtered.Count);
                    return r;
                }
                else if(userService.Get(userId).Value.Role.Equals(UserRole.Author.ToString()))
                {
                    foreach (var problem in result.Value.Results)
                    {
                        
                        if (userId == problemTourAuthorService.GetTour(problem.TourId).Value.AuthorId)
                        {
                            filtered.Add(problem);
                        }
                    }
                    var r = new PagedResult<ProblemDto>(filtered, filtered.Count);
                    return r;
                }
                else
                {
                    return result;
                }

            }
            catch (KeyNotFoundException e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }
        }


    

        public Result<PagedResult<ProblemDto>> GetPaged(int page, int pageSize)
        {
            var result = MapToDto(_crudRepository.GetPaged(page, pageSize));
            return result;
        }

        public Result<ProblemDto> Create(ProblemDto problem, long userId)
        {
            try
            {
                if (!userService.Get(userId).Value.Role.Equals(UserRole.Tourist.ToString()))
                    return Result.Fail("user is not a tourist");
                repository.Create(mapper.Map<Problem>(problem));
                return mapper.Map<ProblemDto>(problem);
            }
            catch (KeyNotFoundException e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }
        }
        public Result<ProblemDto> Update(ProblemDto problem, int userId)
        {
            try
            {
                if (userService.Get(userId).Value.Role.Equals(UserRole.Author.ToString()))
                    return Result.Fail("user does not have permission");
                if (userService.Get(userId).Value.Role.Equals(UserRole.Tourist.ToString()))
                {
                    if (userId != problem.TouristId)
                        return Result.Fail("you are not the creator of this problem");
                }

                var oldProblem = repository.Get(problem.Id);
                if(oldProblem.DueDate != problem.DueDate)
                {
                    var authorId = problemTourAuthorService.GetTour(problem.TourId).Value.AuthorId;
                    if (authorId.HasValue)
                    {
                        SendNotification(problem, userId, authorId.Value);
                    }
                    else
                    {
                        return Result.Fail(FailureCode.NotFound).WithError("Author not found");
                    }
                }

                repository.Update(mapper.Map<Problem>(problem));
                return mapper.Map<ProblemDto>(problem);
            }
            catch (KeyNotFoundException e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }
        }



        public Result<ProblemDto> SendMessage(int userId, ProblemDto problem, ProblemMessageDto message)
        {
            try
            {
                if (userId == problem.TouristId ||
                    problemTourAuthorService.GetTour(problem.TourId).Value.AuthorId == userId ||
                    userService.Get(userId).Value.Role.Equals(UserRole.Administrator.ToString()))
                {
                    mapper.Map<Problem>(problem).SendMessage(mapper.Map<ProblemMessage>(message));

                    // send notification
                    var authorId = problemTourAuthorService.GetTour(problem.TourId).Value.AuthorId;

                    if (userService.GetUserRole(userId).Value.Equals(UserRole.Author.ToString().ToLower()))
                    {
                        SendNotification(problem, userId, problem.TouristId);
                    }
                    else if (userService.GetUserRole(userId).Value.Equals(UserRole.Tourist.ToString().ToLower()))
                    {
                        if (authorId.HasValue)
                        {
                            SendNotification(problem, userId, authorId.Value);
                        }
                        else
                        {
                            return Result.Fail(FailureCode.NotFound).WithError("Author not found");
                        }
                    } else
                    {
                        SendNotification(problem, userId, problem.TouristId);

                        if (authorId.HasValue)
                        {
                            SendNotification(problem, userId, authorId.Value);
                        }
                        else
                        {
                            return Result.Fail(FailureCode.NotFound).WithError("Author not found");
                        }
                    }


                    var updatedProblem = repository.Update(MapToDomain(problem));

                    return Result.Ok(mapper.Map<ProblemDto>(updatedProblem));
                }
                else
                {
                    return Result.Fail(FailureCode.Forbidden).WithError("User is not authorized to send a message on this problem.");
                }
            }
            catch (KeyNotFoundException e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }
            catch (Exception e)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
            }
        }

        private void SendNotification(ProblemDto problem, int senderId, long receiverId)
        {
            var notificationAuthor = new NotificationDto
            {
                Content = "A new message has been sent on problem " + problem.Id,
                Type = NotificationType.TourIssue.ToString(),
                SenderPersonId = senderId,
                ReceiverPersonId = receiverId,
                CreatedAt = DateTime.UtcNow,
                IsRead = false,
                LinkId = problem.Id
            };
            notificationService.SendNotification(notificationAuthor);
        }
    }
}





