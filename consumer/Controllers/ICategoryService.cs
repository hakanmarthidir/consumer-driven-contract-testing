namespace consumer.Controllers
{
    public interface ICategoryService
    {
        Task<List<CategoryDto>> Get();
    }
}