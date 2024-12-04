using FluentResults;

namespace Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;

public interface IUserRepository
{
    bool Exists(string username);
    User? GetActiveByName(string username);
    User Create(User user);
    long GetPersonId(long userId);
    public string GetUserEmail(long userId);
    public User GetById(long id);
    public List<User> GetByIds(List<int> instructorIds);
    public List<long> GetAllAuthorsIds();
}