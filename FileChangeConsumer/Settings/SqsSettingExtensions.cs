using Amazon;
using Amazon.Runtime;
using Amazon.SQS;

namespace FileChangeConsumer.Settings
{
    public static class SqsSettingExtensions
    {
        public static IServiceCollection AddAmazonSQS(this IServiceCollection services, IConfiguration configuration)
        {
            var sqsCredentials = configuration.GetSection("SqsCredentials")
                                              .Get<SqsCredentials>();

            var credentials = new BasicAWSCredentials(sqsCredentials!.AccessKey, sqsCredentials.SecretKey);
            var clientConfig = new AmazonSQSConfig { RegionEndpoint = RegionEndpoint.GetBySystemName(sqsCredentials.Region), ServiceURL = sqsCredentials.ServiceURL };

            services.AddSingleton<IAmazonSQS>(_ => new AmazonSQSClient(credentials, clientConfig));

            return services;
        }
    }
}
