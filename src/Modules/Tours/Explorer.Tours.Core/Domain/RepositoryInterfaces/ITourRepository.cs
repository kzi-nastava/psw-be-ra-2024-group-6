using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.Core.Domain.ShoppingCarts;
using Explorer.Tours.Core.Domain.Tours;
using FluentResults;

namespace Explorer.Tours.Core.Domain.RepositoryInterfaces
{
    public interface ITourRepository
    {
        List<Tour> GetByUserId(long userId);
        public Tour Create(Tour tour);
        public Tour Update(Tour tour);
        public void Delete(long id);
        public Tour GetAggregate(long id);
        public Tour Get(long id);
        public Tour GetTourWithReviews(long tourId);
        public PagedResult<Tour> GetToursWithReviews(int page, int size);

        public List<Tour> GetPublishedToursWithCheckpoints();
        public Tour GetById(long tourId);
        public List<Tour> GetAllByIds(List<int> mostBoughtToursIds);
    }
}
