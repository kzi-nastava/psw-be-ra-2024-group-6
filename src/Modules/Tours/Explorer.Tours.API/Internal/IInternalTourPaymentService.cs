using Explorer.Tours.API.Dtos.TourDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Internal;

public interface IInternalTourPaymentService
{
    TourDto Get(long id);
}