using linhkien_donet.Entities;

namespace _01_WEBAPI.Interfaces
{
    public interface ICategoryRepository
    {
        ICollection<Category> GetAllCategory();

        bool CategoryExists(int id);

        Category GetCategoryById(int CategoryId);

        int CreateCategory(Category Category);

        int UpdateCategory(Category Category);

        int DeleteCategory(Category Category);

    }
}
