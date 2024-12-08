using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Internal;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.Core.Domain.Tours;
using FluentResults;

namespace Explorer.Tours.Core.UseCases
{
    public class AuthorRecommenderService : IAuthorRecommenderService
    {
        private readonly ITourRepository _tourRepository;
        private readonly IInternalTourPersonService _internalTourPersonService;

        public AuthorRecommenderService(ITourRepository tourRepository,
            IInternalTourPersonService internalTourPersonService)
        {
            _tourRepository = tourRepository;
            _internalTourPersonService = internalTourPersonService;
        }

        public Result<List<AuthorLeaderboardDto>> GetBestAuthors(List<long> userIds, int count)
        {
            Dictionary<long, double> authorScores = new Dictionary<long, double>();
            foreach (long userId in userIds)
            {
                List<Tour> authorTours = _tourRepository.GetByUserId(userId);
                double authorScore =
                    CalculateWeightedScore(GetAverageAuthorRating(authorTours), GetAuthorReviews(authorTours), 1);
                authorScores.Add(userId, authorScore);
            }

            return GetSortedDtos(authorScores, count);
        }

        private double CalculateWeightedScore(double averageRating, int numberOfReviews, double C)
        {
            return ((averageRating * numberOfReviews) + (averageRating * C)) / (numberOfReviews + C);
        }

        private List<AuthorLeaderboardDto> GetSortedDtos(Dictionary<long, double> authorScore, int count)
        {
            List<AuthorLeaderboardDto> authorLeaderboardDtos = new List<AuthorLeaderboardDto>();
            int ranking = 1;
            foreach (KeyValuePair<long, double> author in authorScore.OrderByDescending(key => key.Value))
            {
                PersonDto personDto = _internalTourPersonService.GetByUserId((int)author.Key).Value;
                authorLeaderboardDtos.Add(new AuthorLeaderboardDto()
                {
                    UserId = author.Key,
                    Ranking = ranking,
                    Name = personDto.Name,
                    Surname = personDto.Surname,
                    ImageUrl = personDto.PictureURL
                });
                ranking++;
                if (authorLeaderboardDtos.Count == count)
                    break;
            }

            return authorLeaderboardDtos;
        }
        private double GetAverageAuthorRating(List<Tour> tours)
        {
            double sum = 0;
            foreach (Tour tour in tours)
            {
                sum += tour.GetAverageRating();
            }

            return sum / tours.Count;
        }
        private int GetAuthorReviews(List<Tour> tours)
        {
            int sum = 0;
            foreach (Tour tour in tours)
            {
                sum += tour.GetNumberOfReviews();
            };
            return sum;
        }

    }
}


