using Bogus;
using FileChangeConsumer.Domain.Entities;

namespace Tests.Shared.Builders.Entities
{
    public static class DomainFileBuilder
    {
        public static Faker<DomainFile> Map() =>
            new Faker<DomainFile>().CustomInstantiator(f =>
                new DomainFile(name: f.System.FileName(),
                               size: f.Random.Long(min: 1, max: 1000)));
    }
}
