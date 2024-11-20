using Explorer.BuildingBlocks.Core.Domain;
using System.Linq;
using System.Net.Mail;

namespace Explorer.Stakeholders.Core.Domain.Persons;

public class Person : Entity
{
    public long UserId { get; private set; }
    public string Name { get; private set; }
    public string Surname { get; private set; }
    public string Email { get; private set; }
    public string Description { get; private set; }
    public string Motto { get; private set; }
    public long? ImageId { get; set; }

    public List<Follower> Followers { get; private set; } //ko tebe prati kao korisnika

    public List<Follower> Followings { get; private set; } //


    public Person() {
        
    }
    public Person(long userId, string name, string surname, string email)
    {
        UserId = userId;
        Name = name;
        Surname = surname;
        Email = email;
        Description = "";
        Motto = "";
        ImageId = null;
        Followers = new List<Follower>();
        Followings = new List<Follower>();
        Validate();

    }

    public Person(long userId, string name, string surname, string email, string description, string motto, long imageId)
    {
        UserId = userId;
        Name = name;
        Surname = surname;
        Email = email;
        Description = description;
        Motto = motto;
        ImageId = imageId;
        Followers = new List<Follower>();
        Followings = new List<Follower>();
        Validate();

    }
    private void Validate()
    {
        if (UserId == 0) throw new ArgumentException("Invalid UserId");
        if (string.IsNullOrWhiteSpace(Name)) throw new ArgumentException("Invalid Name");
        if (string.IsNullOrWhiteSpace(Surname)) throw new ArgumentException("Invalid Surname");
        if (!MailAddress.TryCreate(Email, out _)) throw new ArgumentException("Invalid Email");

    }

    public void AddFollower(int followerId)
    {
        if(Followers == null)
        {
            Followers = new List<Follower>();
        }
        Followers.Add(new Follower(followerId));
        

    }

    public void AddFollowing(int followingId)
    {
        if(Followings == null)
        {
            Followings = new List<Follower>();
        }
        Followings.Add(new Follower(followingId));
        
    }
}