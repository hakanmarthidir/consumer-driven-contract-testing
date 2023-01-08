using Microsoft.AspNetCore.Mvc;

namespace producer.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CategoryController : ControllerBase
    {
        List<Category> categories = new List<Category>() {
                    new Category() { Id = Guid.NewGuid(), Name = "category1", ProductCount = 3 },
                    new Category() { Id = Guid.NewGuid(), Name = "category2", ProductCount = 5} };     

        public CategoryController()
        {           
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(categories);
        }
    }
}