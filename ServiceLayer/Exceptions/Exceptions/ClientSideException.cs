namespace ServiceLayer.Exceptions.Exceptions
{
    public class ClientSideException : Exception
    {
        public ClientSideException(string? message) : base(message)
        {
        }
    }
}
