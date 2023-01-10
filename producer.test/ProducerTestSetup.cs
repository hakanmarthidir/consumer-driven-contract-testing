using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Hosting;

namespace producer.test
{
    public class ProducerTestSetup : IDisposable
    {
        private readonly IHost server;
        public Uri ServerUri { get; }      

        public ProducerTestSetup()
        {
            ServerUri = new Uri("http://localhost:9222");

            server = Host.CreateDefaultBuilder()
                     .ConfigureWebHostDefaults(webBuilder =>
                     {
                         webBuilder.UseUrls(ServerUri.ToString());
                         webBuilder.UseStartup<ProviderStartup>();
                     })
                     .Build();
            server.Start();
        }

        public void Dispose()
        {
            server.Dispose();
        }

    }




}
