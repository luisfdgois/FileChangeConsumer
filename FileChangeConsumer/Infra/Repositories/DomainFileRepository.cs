using FileChangeConsumer.Domain.Entities;
using FileChangeConsumer.Domain.Interfaces;
using FileChangeConsumer.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace FileChangeConsumer.Infra.Repositories
{
    public class DomainFileRepository : IDomainFileRepository
    {
        private readonly DbContext _dbContext;

        public DomainFileRepository(FileChangeContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<DomainFile> Add(DomainFile file, CancellationToken cancellationToken = default)
        {
            var entity = await _dbContext.AddAsync(file, cancellationToken);

            return entity.Entity;
        }

        public async Task<DomainFile?> GetByName(string name, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Set<DomainFile>().FirstOrDefaultAsync(f => f.Name.Equals(name), cancellationToken);
        }

        public async Task<bool> CommitChanges(CancellationToken cancellationToken = default)
        {
            return await _dbContext.SaveChangesAsync(cancellationToken) > 0;
        }
    }
}
