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
    public class BlogImagesRepository : Repository<BlogImages>, IBlogImagesRepository
    {
        private readonly ApplicationDbContext _context;

        public BlogImagesRepository(ApplicationDbContext context) : base(context) 
        {
            _context = context;
        }

        public void Update(BlogImages obj)
        {
            _context.Update(obj);
        }
    }
}
