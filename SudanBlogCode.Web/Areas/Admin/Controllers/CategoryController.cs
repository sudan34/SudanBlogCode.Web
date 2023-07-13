using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SudanBlogCode.DataAccess.Repository.IRepository;
using SudanBlogCode.Models;

namespace SudanBlogCode.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<IdentityUser> _applicationUser;

        public CategoryController(IUnitOfWork unitOfWork, UserManager<IdentityUser> applicationUser)
        {
            _unitOfWork = unitOfWork;
            _applicationUser = applicationUser;
        }

        public IActionResult Index()
        {
            IEnumerable<Category> objList = _unitOfWork.Category.GetAll();
            return View(objList);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)
        {
            if (ModelState.IsValid)
            {
                obj.CreatedBy = _applicationUser.GetUserId(HttpContext.User);
                _unitOfWork.Category.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Category saved successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }
    }
}
