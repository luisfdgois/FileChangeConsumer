using FileChangeConsumer.App.Extensions;
using FileChangeConsumer.App.Messages;
using FileChangeConsumer.Domain.Interfaces;

namespace FileChangeConsumer.App.Handlers
{
    public class FileNotificationHandler : IFileNotificationHandler
    {
        private readonly ILogger<FileNotificationHandler> _logger;
        private readonly IDomainFileRepository _repository;

        public FileNotificationHandler(ILogger<FileNotificationHandler> logger, IDomainFileRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task Handle(FileUpdatedNotification notification, CancellationToken cancellationToken = default)
        {
            try
            {
                var file = await _repository.GetByName(notification.fileName, cancellationToken);

                if (file is null)
                    await AddFile(notification, cancellationToken);
                else
                    file.Update(notification.fileName, notification.fileSize, notification.date);

                await _repository.CommitChanges(cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.GetType().FullName}: {ex.Message} \n{notification}");
            }
        }

        private async Task AddFile(FileUpdatedNotification notification, CancellationToken cancellationToken = default)
        {
            var file = notification.MapToDomainFile();

            await _repository.Add(file, cancellationToken);
        }
    }
}
