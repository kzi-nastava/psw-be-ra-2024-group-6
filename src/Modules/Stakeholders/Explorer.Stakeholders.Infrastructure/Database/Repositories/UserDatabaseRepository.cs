using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using System.Linq;

namespace Explorer.Stakeholders.Infrastructure.Database.Repositories;

public class UserDatabaseRepository : IUserRepository
{
    private readonly StakeholdersContext _dbContext;

    public UserDatabaseRepository(StakeholdersContext dbContext)
    {
        _dbContext = dbContext;
    }

    public bool Exists(string username)
    {
        return _dbContext.Users.Any(user => user.Username == username);
    }

    public User? GetActiveByName(string username)
    {
        return _dbContext.Users.FirstOrDefault(user => user.Username == username && user.IsActive);
    }

    public User Create(User user)
    {
        _dbContext.Users.Add(user);
        _dbContext.SaveChanges();
        return user;
    }

    public long GetPersonId(long userId)
    {
        var person = _dbContext.People.FirstOrDefault(i => i.UserId == userId);
        if (person == null) throw new KeyNotFoundException("Not found.");
        return person.Id;
    }
    public string GetUserEmail(long userId)
    {
        var person = _dbContext.People.FirstOrDefault(i => i.UserId == userId);
        if (person == null) return "";
        return person.Email;
    }

    public User GetById(long id)
    {
        var user = _dbContext.Users.FirstOrDefault(i => i.Id == id);
        if (user == null) throw new KeyNotFoundException("Not found");
        return user;
    }

    public List<User> GetByIds(List<int> instructorIds)
    {
        var users = _dbContext.Users
                          .Where(user => instructorIds.Contains((int)user.Id))
                          .ToList();
        return users;
    }

    public List<long> GetAllAuthorsIds()
    {
        return _dbContext.Users
            .Where(u => u.Role == UserRole.Author)
            .Select(u => u.Id)
            .ToList();
    }
}