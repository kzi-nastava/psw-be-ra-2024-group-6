﻿using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Blog.API.Dtos;
using Explorer.Blog.API.Public;
using Explorer.Blog.Core.Domain.Blogs;
using Explorer.Blog.Core.Domain.RepositoryInterfaces;
using FluentResults;
using Explorer.Stakeholders.API.Internal;

namespace Explorer.Blog.Core.UseCases
{
    public class CommentService : CrudService<CommentDto, Comment>, ICommentService
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IMapper _mapper;
        private readonly IInternalInstructorService _internalInstructorService;
        public CommentService(ICommentRepository commentRepository, ICrudRepository<Comment> repository, IMapper mapper, IInternalInstructorService internalInstructorService) : base(repository, mapper)
        {
            _commentRepository = commentRepository;
            _mapper = mapper;
            _internalInstructorService = internalInstructorService;
        }

       
            public Result<IEnumerable<CommentDto>> GetByBlogId(long id)
            {
                var comments = _commentRepository.GetByBlogId(id);

                var userIds = comments.Select(comment => comment.UserId).Distinct().ToList();

                var usersResult = _internalInstructorService.GetMany(userIds);
                if (!usersResult.IsSuccess)
                {
                    return Result.Fail<IEnumerable<CommentDto>>("Failed to retrieve users.");
                }

                var users = usersResult.Value.ToDictionary(user => user.Id, user => user.Username);

                var commentsDto = comments.Select(comment =>
                {
                    var commentDto = _mapper.Map<CommentDto>(comment);

                    if (users.TryGetValue(comment.UserId, out var username))
                    {
                        commentDto.AuthorUsername = username;
                    }
                    else
                    {
                        commentDto.AuthorUsername = "Unknown";
                    }

                    return commentDto;
                }).ToList();

                return Result.Ok(commentsDto.AsEnumerable());
            }

        

    }
}
