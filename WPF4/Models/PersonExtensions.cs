namespace WPF4.Models
{
    public static class PersonExtensions
    {
        public static string ToFullName(this Person person)
        {
            return $"{person.FirstName} {person.LastName}";
        }
    }
}
