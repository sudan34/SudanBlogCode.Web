using SudanBlogCode.DataAccess.Repository;
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
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;
        public ICategoryRepository Category {  get; private set; }
        public ISubCategoryRepository SubCategory {  get; private set; }
        public IBlogImagesRepository BlogImages {  get; private set; }

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Category = new CategoryRepository(_db);
            SubCategory = new SubCategoryRepository(_db);
            BlogImages = new BlogImagesRepository(_db);
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
