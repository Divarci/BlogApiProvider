namespace ServiceLayer.Exceptions.Exceptions
{
    public class ConflictException : Exception
    {
        public ConflictException(string? message) : base(message)
        {
        }
    }
}
