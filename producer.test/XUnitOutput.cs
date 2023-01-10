using Xunit.Abstractions;
using PactNet.Infrastructure.Outputters;

namespace producer.test
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



    //internal class ProducerTestSetup
    //{

    //    private static IHost? _server;

    //    public static Uri ServerUri { get; } = BuildUri();

    //    //[OneTimeSetUp]
    //    public void BeforeAny()
    //    {

    //        _server = Host
    //            .CreateDefaultBuilder()
    //            .ConfigureWebHostDefaults(webBuilder =>
    //            {

    //                webBuilder.UseUrls(ServerUri.ToString());
    //                webBuilder.UseStartup<TestStartup>();
    //            })
    //            .Build();

    //        _server.Start();
    //    }

    //    //[OneTimeTearDown]
    //    public void AfterAll()
    //    {

    //        _server?.Dispose();
    //    }

    //    private static Uri BuildUri()
    //    {
    //        return new Uri("http://localhost:55511");
    //    }


    //}


    //internal class TestStartup
    //{

    //    //private readonly Startup _startup;

    //    public TestStartup(IConfiguration configuration)
    //    {

    //        //_startup = new Startup();
    //    }

    //    public void ConfigureServices(IServiceCollection services)
    //    {

    //        // used to generate a valid token when a fake token is found in the request
    //        //services.AddScoped<ITokenProvider, TokenProvider>();
    //        //services.AddTransient<BearerTokenReplacementMiddleware>();
    //        //_startup.ConfigureServices(services);
    //    }

    //    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    //    {

    //        //app.UseMiddleware<ProviderStateMiddleware>();
    //        //app.UseMiddleware<BearerTokenReplacementMiddleware>();

    //        //_startup.Configure(app, env);
    //    }
    //}




}
