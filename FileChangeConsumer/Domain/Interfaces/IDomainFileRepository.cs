using FileChangeConsumer.Domain.Entities;

namespace FileChangeConsumer.Domain.Interfaces
{
    public interface IDomainFileRepository
    {
        Task<DomainFile> Add(DomainFile file, CancellationToken cancellationToken = default);
        Task<DomainFile?> GetByName(string name, CancellationToken cancellationToken = default);
        Task<bool> CommitChanges(CancellationToken cancellationToken = default);
    }
}
