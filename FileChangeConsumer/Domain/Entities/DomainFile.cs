using FileChangeConsumer.Domain.Exceptions;

namespace FileChangeConsumer.Domain.Entities
{
    public class DomainFile
    {
        public string Name { get; private set; }
        public long Size { get; private set; }
        public DateTime LastModified { get; private set; }

        protected DomainFile() { }

        public DomainFile(string name, long size)
        {
            Name = name;
            Size = size;
            LastModified = DateTime.UtcNow;
        }

        public void Update(string name, long size, DateTime notificationDate)
        {
            if (LastModified >= notificationDate) throw new InvalidFileModificationException();

            Name = name;
            Size = size;
            LastModified = DateTime.UtcNow;
        }
    }
}
