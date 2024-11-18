using AutoMapper;
using Explorer.Tours.API.Dtos.TourDtos;
using Explorer.Tours.API.Internal;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.UseCases;

public class InternalTourPaymentService : IInternalTourPaymentService
{
    private readonly ITourRepository tourRepository;
    private readonly IMapper mapper;

    public InternalTourPaymentService(ITourRepository tourRepository, IMapper mapper)
    {
        this.tourRepository = tourRepository;
        this.mapper = mapper;
    }

    public TourDto Get(long id)
    {
        return mapper.Map<TourDto>(tourRepository.Get(id));
    }
}
