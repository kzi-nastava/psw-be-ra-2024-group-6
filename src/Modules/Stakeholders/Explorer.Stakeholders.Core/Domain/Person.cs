using Explorer.BuildingBlocks.Core.Domain;
using System.Net.Mail;

namespace Explorer.Stakeholders.Core.Domain;

public class Person : Entity
{
    public long UserId { get; init; }
    public string Name { get; init; }
    public string Surname { get; init; }
    public string Email { get; init; }
    public string Description { get; init; }
    public string Motto { get; init; }

    public string PictureURL {  get; init; }




    public Person(long userId, string name, string surname, string email)
    {
        UserId = userId;
        Name = name;
        Surname = surname;
        Email = email;
        Description = "";
        Motto = "";
        PictureURL = "";
        Validate();

    }

    public Person(long userId,string name, string surname, string email,string description,string motto,string picture)
    {
        UserId = userId;
        Name = name;
        Surname = surname;
        Email = email;
        Description =description;
        Motto = motto;
        PictureURL = picture;
        Validate();

    }
    private void Validate()
    {
        if (UserId == 0) throw new ArgumentException("Invalid UserId");
        if (string.IsNullOrWhiteSpace(Name)) throw new ArgumentException("Invalid Name");
        if (string.IsNullOrWhiteSpace(Surname)) throw new ArgumentException("Invalid Surname");
        if (!MailAddress.TryCreate(Email, out _)) throw new ArgumentException("Invalid Email");
    
    }
}