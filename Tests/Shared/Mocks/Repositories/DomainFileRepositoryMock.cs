using FileChangeConsumer.Domain.Entities;
using FileChangeConsumer.Domain.Interfaces;
using NSubstitute;

namespace Tests.Shared.Mocks.Repositories
{
    public class DomainFileRepositoryMock
    {
        private IDomainFileRepository _repository;

        private DomainFileRepositoryMock()
        {
            _repository = Substitute.For<IDomainFileRepository>();
        }

        public static DomainFileRepositoryMock Instance() =>
            new DomainFileRepositoryMock();

        public DomainFileRepositoryMock SetGetByName(DomainFile? domainFile = default)
        {
            if (domainFile is not null)
                _repository.GetByName(domainFile.Name, default).Returns(domainFile);
            else
                _repository.GetByName(Arg.Any<string>(), default).Returns(domainFile);

            return this;
        }

        public DomainFileRepositoryMock VerifyAdd(int callsCount = 1)
        {
            _repository.Received(callsCount).Add(Arg.Any<DomainFile>(), default);

            return this;
        }

        public DomainFileRepositoryMock VerifyCommitChanges(int callsCount = 1)
        {
            _repository.Received(callsCount).CommitChanges(default);

            return this;
        }

        public IDomainFileRepository Build() => _repository;
    }
}
