using FileChangeConsumer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FileChangeConsumer.Infra.Mappers
{
    public class DomainFileMapping : IEntityTypeConfiguration<DomainFile>
    {
        public void Configure(EntityTypeBuilder<DomainFile> builder)
        {
            builder.ToTable("files");

            builder.HasKey(f => f.Name);

            builder.Property(f => f.Name)
                   .IsUnicode(false)
                   .HasMaxLength(250);

            builder.Property(f => f.Size)
                   .IsRequired();
        }
    }
}
