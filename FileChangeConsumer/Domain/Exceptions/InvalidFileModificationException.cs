namespace FileChangeConsumer.Domain.Exceptions
{
    public class InvalidFileModificationException : Exception
    {
        public InvalidFileModificationException() : base("It is not possible to update the file data with older modifications.") { }
    }
}
