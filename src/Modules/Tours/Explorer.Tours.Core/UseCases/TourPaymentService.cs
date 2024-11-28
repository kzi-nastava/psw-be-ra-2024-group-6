using AutoMapper;
using Explorer.Tours.API.Dtos.TourDtos;
using Explorer.Tours.API.Internal;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.Core.Domain.Tours;

namespace Explorer.Tours.Core.UseCases;

public class InternalTourPaymentService : CrudService<TourDto, Tour>, IInternalTourPaymentService
{
    private readonly ITourRepository tourRepository;
    private readonly IMapper mapper;

    public InternalTourPaymentService(ICrudRepository<Tour> repository, ITourRepository tourRepository, IMapper mapper) : base(repository, mapper)
    {
        this.tourRepository = tourRepository;
        this.mapper = mapper;
    }

    public TourDto Get(long id)
    {
        return mapper.Map<TourDto>(tourRepository.Get(id));
    }

    public bool IsUserAuthor(long tourId, long userId)
    {
        var tour = tourRepository.Get(tourId);
        return tour.IsUserAuthor(userId);
    }
}
