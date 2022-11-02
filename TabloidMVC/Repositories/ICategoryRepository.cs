using System.Collections.Generic;
using TabloidMVC.Models;

namespace TabloidMVC.Repositories
{
    public interface ICategoryRepository
    {
        List<Category> GetAll();
        void Add(Category category);
        void Delete(int id);
        Category GetCategoryById(int id);
        void UpdateCategory(Category category);
    }
}