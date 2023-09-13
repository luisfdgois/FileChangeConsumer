using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace FileChangeConsumer.Infra.Data
{
    public class FileChangeContext : DbContext
    {
        public FileChangeContext(DbContextOptions<FileChangeContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }
    }
}
