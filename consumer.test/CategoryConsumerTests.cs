using consumer.Controllers;
using Newtonsoft.Json;
using PactNet;
using System.Net;
using Newtonsoft.Json.Serialization;
using Moq;
using Moq.Protected;

namespace consumer.test
{
    public class CategoryConsumerTests
    {

        private readonly IPactBuilderV3 pactBuilder;

        public CategoryConsumerTests()
        {
            var config = new PactConfig
            {
                PactDir = "../../../pacts/",
                DefaultJsonSettings = new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                }
            };

            var pact = Pact.V3("CatalogConsumer", "CategoryAPI", config);
            this.pactBuilder = pact.WithHttpInteractions();
        }

        [Fact]
        public async Task GetCategory_ReturnsAllActiveCategories()
        {
            List<CategoryDto> mockCategories = new List<CategoryDto>() {
                    new CategoryDto() { Id = Guid.NewGuid(), Name = "category1", ProductCount = 3 },
                    new CategoryDto() { Id = Guid.NewGuid(), Name = "category2", ProductCount = 5} };

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
