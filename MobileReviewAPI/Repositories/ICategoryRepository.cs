using MobileReviewAPI.Models;

namespace MobileReviewAPI.Repositories
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetCategories();
        Task<Category> GetCategoryById(int id);
        Task<IEnumerable<Mobile>>GetMobileByCategory(int categoryId);
        bool CategoryExists(int categoryId);

    }
}
