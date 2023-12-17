namespace ProjectGamebook.Models
{
    public class LocationNotFoundException : Exception
    {
        public LocationNotFoundException()
        {
        }

        public LocationNotFoundException(string message) : base(message)
        {
        }
    }
}
