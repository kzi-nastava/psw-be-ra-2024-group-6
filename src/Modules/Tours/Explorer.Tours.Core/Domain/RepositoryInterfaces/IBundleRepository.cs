using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain.RepositoryInterfaces
{
    public interface IBundleRepository
    {
        public List<Bundle> GetAll();
        public Bundle Create(Bundle bundle);
        public Bundle Update(Bundle bundle);
        public Bundle GetById(long bundleId);
    }
}
