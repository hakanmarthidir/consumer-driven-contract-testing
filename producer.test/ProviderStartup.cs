using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using producer.Controllers;

namespace producer.test
{
    public class ProviderStartup
    {              
        public ProviderStartup(IConfiguration configuration)
        {            
        }
        public void ConfigureServices(IServiceCollection services)
        {            
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();            

            app.UseEndpoints(endpoints =>
            {
                List<Category> categories = new List<Category>() {
                    new Category() { Id = new Guid("62380d5e-2554-49c9-a814-13ab1f50d73e"), Name = "category1", ProductCount = 3 },
                    new Category() { Id = new Guid("1c319a81-22b6-4971-b774-4963db5099db"), Name = "category2", ProductCount = 5} };
                endpoints.MapGet("/api/v1/category", () => { return Results.Ok(categories); });
            });
        }
    }




}
