using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.Stakeholders.Core.Domain.ProfileNotifications;

namespace Explorer.Stakeholders.Core.Domain.RepositoryInterfaces
{
    public interface IFollowerRepository
    {
        List<Follower> GetFollowersByUserId(long userId);
        void Add(Follower follower);
        void Remove(Follower follower);
    }
}
