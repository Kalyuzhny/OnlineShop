namespace Stock.Domain.Exceptions
{
    public class NotEnoughQuantityException : Exception
    {
        public NotEnoughQuantityException()
        { }

        public NotEnoughQuantityException(string message)
            : base(message)
        { }

        public NotEnoughQuantityException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
