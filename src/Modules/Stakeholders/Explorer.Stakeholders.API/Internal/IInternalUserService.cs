namespace Explorer.Stakeholders.API.Internal;

public interface IInternalUserService
{
    public bool IsUserAuthor(long userId);
}