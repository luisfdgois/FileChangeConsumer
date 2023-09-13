using FileChangeConsumer;
using FileChangeConsumer.App.Handlers;
using FileChangeConsumer.Domain.Interfaces;
using FileChangeConsumer.Infra.Repositories;
using FileChangeConsumer.Settings;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        var configuration = context.Configuration;

        services.Configure<SqsProperties>(configuration.GetSection("SqsProperties"));
        services.Configure<SqsCredentials>(configuration.GetSection("SqsCredentials"));

        services.AddHostedService<Worker>();

        services.AddScoped<IFileNotificationHandler, FileNotificationHandler>();
        services.AddAmazonSQS(configuration);

        services.AddScoped<IDomainFileRepository, DomainFileRepository>();
        services.AddDataBase(configuration);
    })
    .Build();

host.Run();
