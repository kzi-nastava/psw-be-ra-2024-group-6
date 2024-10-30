﻿using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;


namespace Explorer.Blog.Core.Domain.Blogs
{
    public class Blog : Entity
    {
        public string Title { get; private set; }
        public string Description { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public Status Status { get; private set; }
        public int UserId { get; private set; }
        public List<BlogPicture> Pictures { get; private set; }
        public List<Rating> Ratings { get; private set; }
        public List<Comment> Comments { get; private set; }

        private Blog() { }

        public Blog(string title, string description, Status status, int userId, List<BlogPicture> pictures = null)
        {
            if (string.IsNullOrWhiteSpace(title) && string.IsNullOrWhiteSpace(description)) throw new ArgumentNullException("Invalid title.");
            if (status == null) throw new ArgumentNullException("Invalid status.");
            Title = title;
            Description = description;
            CreatedAt = DateTime.Now;
            Status = status;
            UserId = userId;
            Pictures = pictures ?? new List<BlogPicture>();
        }
    }

    public enum Status
    {
        Draft,
        Closed,
        Published
    }

}
