namespace UserWall;

public sealed class User
{
    public int Id { get; set; }

    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    // public List<Post> Posts { get; } = new();
}
