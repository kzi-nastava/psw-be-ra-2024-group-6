using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.Core.Domain.Persons;

namespace Explorer.Stakeholders.Core.Domain.RepositoryInterfaces
{
    public interface IPersonRepository
    {
        Person GetByUserId(int userId);

        Person Update(Person person);

        List<Person> GetMostFollowedAuthors(List<long> authorsIds, int count = 4);

        List<Person> GetAllActivePersons();

    }
}
