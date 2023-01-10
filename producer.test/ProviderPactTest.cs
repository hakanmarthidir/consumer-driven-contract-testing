using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using PactNet.Infrastructure.Outputters;
using PactNet.Verifier;
using Xunit.Abstractions;

namespace producer.test
{

    public class ProviderApiTests : IClassFixture<ProducerTestSetup>
    {
        private readonly ITestOutputHelper output;
        private readonly ProducerTestSetup setup;

        public ProviderApiTests(ProducerTestSetup setup, ITestOutputHelper output)
        {
            this.output = output;
            this.setup = setup;
        }

        [Fact]
        public void ProviderApiPact_WorksWithConsumer()
        {

            var pactLocation = @"../../../../pacts/CatalogConsumer-CategoryAPI.json";

            if (!File.Exists(pactLocation))
                throw new ArgumentNullException("File path is wrong.");           

            var config = new PactVerifierConfig()
            {
                LogLevel = PactNet.PactLogLevel.Debug,
                Outputters = new List<IOutput>
                {
                    new XUnitOutput(output),
                },
            };

            var verifier = new PactVerifier(config);

            verifier.ServiceProvider("CategoryAPI", this.setup.ServerUri)
            .WithFileSource(new FileInfo(pactLocation))
            .WithRequestTimeout(TimeSpan.FromSeconds(10))
            //.WithProviderStateUrl(new Uri(this.setup.ServerUri, "/provider-states"))
            .WithSslVerificationDisabled()
            .Verify();
        }
    }
}
