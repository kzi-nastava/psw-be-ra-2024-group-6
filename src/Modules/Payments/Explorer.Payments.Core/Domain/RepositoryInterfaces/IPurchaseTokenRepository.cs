using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.Core.Domain.RepositoryInterfaces;

public interface IPurchaseTokenRepository
{
    public PurchaseToken Create(PurchaseToken token);

    public List<PurchaseToken> GetByUserId(long id);

    public PurchaseToken? GetByUserAndTour(long userId, long tourId);
    public PurchaseToken Update(PurchaseToken token);
}
