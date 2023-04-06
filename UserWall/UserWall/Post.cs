using System;

namespace UserWall;

public sealed class Post
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public DateTime PublicationTime { get; set; }

    public string Message { get; set; } = string.Empty;

    public User User { get; set; } = null!;
}
