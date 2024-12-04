using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.Stakeholders.API.Internal;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using Explorer.Stakeholders.Core.Domain;

namespace Explorer.Stakeholders.Core.UseCases
{
    public class InternalUserService : IInternalUserService
    {
        private readonly IUserRepository _userRepository;

        public InternalUserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public bool IsUserAuthor(long userId)
        {
            return _userRepository.GetById(userId).Role == UserRole.Author;
        }
    }
}
