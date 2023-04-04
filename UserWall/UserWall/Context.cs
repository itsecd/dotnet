using System;
using System.Collections.Generic;

namespace UserWall;

public sealed class Context
{
    public List<Post> Posts { get; } = new();

    public List<User> Users { get; } = new();

    public Context()
    {
        Users.Add(new User { Id = 1, FirstName = "Caelina", LastName = "Carlton" });
        Users.Add(new User { Id = 2, FirstName = "Ivan", LastName = "Petrov" });

        Posts.Add(new Post
        {
            Id = 11,
            UserId = 1,
            PublicationTime = new DateTime(2023, 02, 13),
            Message = "Hello!"
        });
        Posts.Add(new Post
        {
            Id = 12,
            UserId = 1,
            PublicationTime = new DateTime(2023, 02, 14),
            Message = "Hi!"
        });

        Posts.Add(new Post
        {
            Id = 21,
            UserId = 2,
            PublicationTime = new DateTime(2022, 02, 10),
            Message = "Post1"
        });
        Posts.Add(new Post
        {
            Id = 22,
            UserId = 2,
            PublicationTime = new DateTime(2023, 02, 12),
            Message = "Post2"
        });
        Posts.Add(new Post
        {
            Id = 23,
            UserId = 2,
            PublicationTime = new DateTime(2023, 02, 11),
            Message = "Post3"
        });
    }
}
