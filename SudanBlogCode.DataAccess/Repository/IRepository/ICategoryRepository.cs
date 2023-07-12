using SudanBlogCode.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudanBlogCode.DataAccess.Repository.IRepository
{
    public interface ICategoryRepository
    {
        void AddCategory(Category category);
        IEnumerable<Category> GetAllCategories();
        Category GetCategory(int id);
        void UpdateCategory(Category category);
        void DeleteCategory(int id);

    }
}
