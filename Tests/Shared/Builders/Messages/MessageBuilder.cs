using Amazon.SQS.Model;
using Bogus;
using System.Text.Json;

namespace Tests.Shared.Builders.Messages
{
    public static class MessageBuilder
    {
        public static Faker<Message> Map() =>
            new Faker<Message>().CustomInstantiator(f =>
                new Message
                {
                    ReceiptHandle = f.Random.AlphaNumeric(10),
                    Body = BuildBody(f)
                });

        public static Faker<Message> WithBody(this Faker<Message> faker, string body)
        {
            return faker.RuleFor(f => f.Body, _ => body);
        }

        private static string BuildBody(Faker f)
        {
            var jsonObject = new
            {
                fileName = f.System.FileName(),
                fileSize = f.Random.Long(min: 1, max: 1000),
                date = f.Date.Future()
            };

            return JsonSerializer.Serialize(jsonObject);
        }
    }
}
