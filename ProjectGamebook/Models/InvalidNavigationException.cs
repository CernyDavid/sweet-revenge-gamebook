namespace ProjectGamebook.Models
{
    public class InvalidNavigationException : Exception
    {
        public InvalidNavigationException()
        {
        }

        public InvalidNavigationException(string message) : base(message)
        {
        }
    }
}
