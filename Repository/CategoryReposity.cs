using _01_WEBAPI.Data;
using _01_WEBAPI.Interfaces;
using linhkien_donet.Entities;
using linhkien_donet.Interfaces;

namespace _01_WEBAPI.Repository
{
    public class CategoryReposity : ICategoryRepository
    {
        private readonly DataContext _context;

        public CategoryReposity(DataContext context)
        {
            _context = context;

        }

        public bool CategoryExists(int id)
        {
            return _context.Category.Any(b => b.Id == id);
        }

        public ICollection<Category> GetAllCategory()
        {
            return _context.Category.OrderBy(p => p.Id).ToList();
        }

        public Category GetCategoryById(int CategoryId)
        {
            return _context.Category.Where(b => b.Id == CategoryId).FirstOrDefault();
        }

        public int CreateCategory(Category Category)
        {
            _context.Category.Add(Category);
            return _context.SaveChanges();

        }

        public int UpdateCategory(Category Category)
        {
            _context.Category.Update(Category);
            return _context.SaveChanges();
        }

        public int DeleteCategory(Category Category)
        {
            _context.Category.Remove(Category);
            return _context.SaveChanges();
        }
    }
}
