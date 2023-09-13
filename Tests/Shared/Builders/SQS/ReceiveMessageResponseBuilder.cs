using Amazon.SQS.Model;
using Bogus;
using System.Net;

namespace Tests.Shared.Builders.SQS
{
    public static class ReceiveMessageResponseBuilder
    {
        public static Faker<ReceiveMessageResponse> Map() =>
            new Faker<ReceiveMessageResponse>().CustomInstantiator(f =>
                new ReceiveMessageResponse { HttpStatusCode = HttpStatusCode.OK });

        public static Faker<ReceiveMessageResponse> WithHttpStatusCode(this Faker<ReceiveMessageResponse> faker, HttpStatusCode statusCode)
        {
            return faker.RuleFor(f => f.HttpStatusCode, _ => statusCode);
        }

        public static Faker<ReceiveMessageResponse> WithMessages(this Faker<ReceiveMessageResponse> faker, List<Message> messages)
        {
            return faker.RuleFor(f => f.Messages, _ => messages);
        }
    }
}
