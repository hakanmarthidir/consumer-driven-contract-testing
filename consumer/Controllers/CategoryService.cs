using Newtonsoft.Json;
using System.Net;

namespace consumer.Controllers
{
    public class CategoryService : ICategoryService
    {

        private readonly IHttpClientFactory _httpClientFactory;

        public CategoryService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
  
        public async Task<List<CategoryDto>> Get()
        {

            var url = $"api/v1/category";

            var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.Add("Accept", "application/json");

            var httpClient = this._httpClientFactory.CreateClient("Category");
            var response = await httpClient.SendAsync(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var content = await response.Content.ReadAsStringAsync();
                return !string.IsNullOrEmpty(content)
                            ? JsonConvert.DeserializeObject<List<CategoryDto>>(content)
                            : new List<CategoryDto>();
            }

            return new List<CategoryDto>();
        }   

    }
}