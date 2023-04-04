using System;

namespace UserWall.Dto;

public sealed class PostDto
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public DateTime PublicationTime { get; set; }

    public string Message { get; set; } = string.Empty;
}
