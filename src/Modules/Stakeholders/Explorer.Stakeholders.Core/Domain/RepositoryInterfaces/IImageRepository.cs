using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Core.Domain.RepositoryInterfaces
{
    public interface IImageRepository
    {
        public void Add(Image image);
        public void Delete(long id);
        public Image Get(long id);
        public Image Update(Image image);
    }
}
