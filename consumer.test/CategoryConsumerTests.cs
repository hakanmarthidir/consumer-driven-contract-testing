using consumer.Controllers;
using Newtonsoft.Json;
using PactNet;
using System.Net;
using Newtonsoft.Json.Serialization;
using Moq;
using Moq.Protected;
using Xunit.Abstractions;
using PactNet.Infrastructure.Outputters;

namespace consumer.test
{
    public class XUnitOutput : IOutput
    {
        private readonly ITestOutputHelper output;

        public XUnitOutput(ITestOutputHelper output)
        {
            this.output = output;
        }

        public void WriteLine(string line)
        {
            this.output.WriteLine(line);
        }
    }


    public class CategoryConsumerTests
    {

        private readonly IPactBuilderV3 pactBuilder;

        public CategoryConsumerTests(ITestOutputHelper testOutputHelper)
        {
            var config = new PactConfig
            {
                PactDir = "../../../../pacts/",
                DefaultJsonSettings = new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                },
                Outputters = new[] { new XUnitOutput(testOutputHelper) }
            };

            var pact = Pact.V3("CatalogConsumer", "CategoryAPI", config);
            this.pactBuilder = pact.WithHttpInteractions();
        }

        [Fact]
        public async Task GetCategory_ReturnsAllActiveCategories()
        {
            List<CategoryDto> mockCategories = new List<CategoryDto>() {
                    new CategoryDto() { Id = new Guid("62380d5e-2554-49c9-a814-13ab1f50d73e"), Name = "category1", ProductCount = 3 },
                    new CategoryDto() { Id = new Guid("1c319a81-22b6-4971-b774-4963db5099db"), Name = "category2", ProductCount = 5} };

            this.pactBuilder
                .UponReceiving("GET - all categories")
                    .Given("Null - Get All")
                    .WithRequest(HttpMethod.Get, "/api/v1/category")
                    .WithHeader("Accept", "application/json")
                .WillRespond()
                    .WithStatus(HttpStatusCode.OK)
                    .WithHeader("Content-Type", "application/json; charset=utf-8")
                    .WithJsonBody(mockCategories);

            await this.pactBuilder.VerifyAsync(async ctx =>
            {
                var mockFactory = new Mock<IHttpClientFactory>();

                var httpclient = new HttpClient() { BaseAddress = ctx.MockServerUri };
                mockFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(httpclient);

                var client = new CategoryService(mockFactory.Object);
                var categories = await client.Get();
                Assert.Equal(categories.Count, mockCategories.Count);
            });
        }
    }
}
