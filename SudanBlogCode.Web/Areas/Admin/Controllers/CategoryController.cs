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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(wwwRootPath, @"images\category");
                    var extension = Path.GetExtension(file.FileName);
                    if (obj.DefautlImageUrl != null)
                    {
                        var oldImagePath = Path.Combine(wwwRootPath, obj.DefautlImageUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }
                    using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                    {
                        file.CopyTo(fileStreams);
                    }
                    obj.DefautlImageUrl = @"\images\category\" + fileName + extension;
                }

                obj.CreatedBy = _applicationUser.GetUserId(HttpContext.User);
                _unitOfWork.Category.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Category updted successfully";
                return RedirectToAction("Index");
            }
            return View(obj);

        }
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var obj = _unitOfWork.Category.GetFirstOrDefault(u=>u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var obj = _unitOfWork.Category.GetFirstOrDefault(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            _unitOfWork.Category.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "Category deleted successfully";
            return RedirectToAction("Index");
        }
        /*//POST
        [HttpDelete]
        public IActionResult Delete(int? id)
        {

            var obj = _unitOfWork.Category.GetFirstOrDefault(u => u.Id == id);
            if (obj == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, obj.DefautlImageUrl.TrimStart('\\'));
            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }
            _unitOfWork.Category.Remove(obj);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Deleted Successfully!" });

        }*/
    }
}
