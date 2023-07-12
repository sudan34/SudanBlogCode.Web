using Microsoft.AspNetCore.Mvc;
using SudanBlogCode.DataAccess.Repository.IRepositiry;

namespace SudanBlogCode.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private ICategoryRepository _CategoryRepository;

        public CategoryController(ICategoryRepository iCategoryRepository)
        {
            _CategoryRepository = iCategoryRepository;
        }

        public IActionResult Index()
        {
            var getAll = _CategoryRepository.GetAllCategories();
            return View();
        }
    }
}
