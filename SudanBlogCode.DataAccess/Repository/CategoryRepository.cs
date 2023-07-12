using Microsoft.EntityFrameworkCore;
using SudanBlogCode.DataAccess.Repository.IRepository;
using SudanBlogCode.Models;
using SudanBlogCode.Web.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudanBlogCode.DataAccess.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private ApplicationDbContext _db;
        internal DbSet<Category> _categoryEntity;

        public CategoryRepository(ApplicationDbContext db)
        {
            _db = db;
            this._categoryEntity = _db.Set<Category>();
        }
        public void AddCategory(Category category)
        {
            _db.Category.Add(category);
            _db.SaveChanges();
        }

        public void DeleteCategory(int id)
        {
            Category category = GetCategory(id);
            _db.Category.Remove(category);
            _db.SaveChanges();
        }

        public IEnumerable<Category> GetAllCategories()
        {
            return _db.Category.AsEnumerable();
        }

        public Category GetCategory(int id)
        {
            return _db.Category.SingleOrDefault(u => u.Id == id);

        }

        public void UpdateCategory(Category category)
        {
            if (category != null)
                _db.Category.Update(category);
            _db.SaveChanges();
        }
    }
}
