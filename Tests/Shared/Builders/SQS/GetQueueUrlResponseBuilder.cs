using Amazon.SQS.Model;
using Bogus;

namespace Tests.Shared.Builders.SQS
{
    public static class GetQueueUrlResponseBuilder
    {
        public static Faker<GetQueueUrlResponse> Map() =>
            new Faker<GetQueueUrlResponse>().CustomInstantiator(f =>
                new GetQueueUrlResponse { QueueUrl = f.Internet.Url() });
    }
}
