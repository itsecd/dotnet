namespace Shared4.Models
{
    public sealed class Person
    {
        public int Id { get; set; }

        public bool IsFemale { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;
    }
}
