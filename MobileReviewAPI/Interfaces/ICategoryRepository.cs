using MobileReviewAPI.Models;

namespace MobileReviewAPI.Interfaces
{
    public interface ICategoryRepository
    {
        //Get Section
        Task<IEnumerable<Category>> GetCategories();
        Task<Category> GetCategoryById(int id);
        Task<IEnumerable<Mobile>> GetMobileByCategory(int categoryId);
        bool CategoryExists(int categoryId);

        //Create Section
        Task<bool> CreateCategory(Category category);
        Task<bool> Save();

        //Update
        Task<bool> UpdateCategory(Category category);

        //Delete
        Task<bool> DeleteCategory(Category category);
    }
}
