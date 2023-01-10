using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using producer.Controllers;
using System.Net;

namespace producer.test
{

    public class ProviderStateMiddleware
    {
        private readonly RequestDelegate _next;
        public ProviderStateMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            context.Response.ContentType = "application/json; charset=utf-8";
            context.Response.StatusCode = (int)HttpStatusCode.OK;

            if (context.Request.Method == HttpMethod.Get.ToString())
            {
                if (context.Request.Path.Value == "/api/v1/category")
                {
                    List<Category> categories = new List<Category>() {
                    new Category() { Id = new Guid("62380d5e-2554-49c9-a814-13ab1f50d73e"), Name = "category1", ProductCount = 3 },
                    new Category() { Id = new Guid("1c319a81-22b6-4971-b774-4963db5099db"), Name = "category2", ProductCount = 5} };

                    DefaultContractResolver contractResolver = new DefaultContractResolver
                    {
                        NamingStrategy = new CamelCaseNamingStrategy()
                    };

                    var serialized = JsonConvert.SerializeObject(categories, new JsonSerializerSettings
                    {
                        ContractResolver = contractResolver,
                        Formatting = Formatting.Indented
                    });
                    await context.Response.WriteAsync(serialized);
                    return;
                }
                else
                {
                    List<Category> categories = new List<Category>();
                    var serialized = JsonConvert.SerializeObject(categories);
                    await context.Response.WriteAsync(serialized);
                    return;
                }
            }

            await this._next(context);
        }
    }
}
