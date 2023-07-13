using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
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
        private readonly IWebHostEnvironment _webHostEnvironment;

        public CategoryController(IUnitOfWork unitOfWork, UserManager<IdentityUser> applicationUser, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _applicationUser = applicationUser;
            _webHostEnvironment = webHostEnvironment;
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
        public IActionResult Create(Category obj, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(wwwRootPath, @"images\category");
                    var extension = Path.GetExtension(file.FileName);
                    
                    using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                    {
                        file.CopyTo(fileStreams);
                    }
                    obj.DefautlImageUrl = @"\images\category\" + fileName + extension;
                }



                obj.CreatedBy = _applicationUser.GetUserId(HttpContext.User);
                _unitOfWork.Category.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Category saved successfully";
                return RedirectToAction("Index");
            }
            return View(obj);

        }
        public IActionResult Edit(int Id)
        {
            Category obj = _unitOfWork.Category.GetFirstOrDefault(u=>u.Id == Id);
            if (obj != null)
            {
                return View(obj);
            }else
            {
                return RedirectToAction("Index");
            }
        }
    }
}
