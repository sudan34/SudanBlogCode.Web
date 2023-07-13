using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SudanBlogCode.DataAccess.Repository.IRepository;
using SudanBlogCode.Models;
using SudanBlogCode.Models.ViewModels;

namespace SudanBlogCode.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class SubCategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<IdentityUser> _applicationUser;

        public SubCategoryController(IUnitOfWork unitOfWork, UserManager<IdentityUser> applicationUser)
        {
            _unitOfWork = unitOfWork;
            _applicationUser = applicationUser;
        }

        public IActionResult Index()
        {
            IEnumerable<SubCategory> objList = _unitOfWork.SubCategory.GetAll();
            return View(objList);
        }
        public IActionResult Create()
        {
            SubCategoryVM obj = new()
            {
                SubCategory = new(),
                CategoryList = _unitOfWork.Category.GetAll().Select(c => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Text = c.CategoryName,
                    Value = c.Id.ToString()
                })
            };

            return View(obj);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(SubCategoryVM obj)
        {
            obj.CategoryList = _unitOfWork.Category.GetAll().Select(c => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
            {
                Text = c.CategoryName,
                Value = c.Id.ToString()
            });
            if (ModelState.IsValid)
            {
                obj.SubCategory.CreatedBy = _applicationUser.GetUserId(HttpContext.User);
                _unitOfWork.SubCategory.Add(obj.SubCategory);
                _unitOfWork.Save();
                TempData["success"] = "Sub Category saved successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }
        public IActionResult Edit(int Id)
        {
            SubCategory objSub = _unitOfWork.SubCategory.GetFirstOrDefault(c => c.Id == Id);
            if (objSub != null)
            {
                SubCategoryVM obj = new()
                {
                    SubCategory = objSub,
                    CategoryList = _unitOfWork.Category.GetAll().Select(c => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                    {
                        Text = c.CategoryName,
                        Value = c.Id.ToString()
                    })
                };
                return View(obj);
            }
            else
            {
                return RedirectToAction("Index");  
            }


        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(SubCategoryVM obj)
        {
            obj.CategoryList = _unitOfWork.Category.GetAll().Select(c => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
            {
                Text = c.CategoryName,
                Value = c.Id.ToString()
            });
            if (ModelState.IsValid)
            {
                obj.SubCategory.ModifiedDate = DateTime.Now;
                obj.SubCategory.ModifiedBy = _applicationUser.GetUserId(HttpContext.User);
                _unitOfWork.SubCategory.Update(obj.SubCategory);
                _unitOfWork.Save();
                TempData["success"] = "Sub Category updated successfully";
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
            var obj = _unitOfWork.SubCategory.GetFirstOrDefault(x => x.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            SubCategoryVM objVm = new()
            {
                SubCategory = obj,
                CategoryList = _unitOfWork.Category.GetAll().Select(c => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Text = c.CategoryName,
                    Value = c.Id.ToString()
                })
            };
            
            return View(objVm);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var obj = _unitOfWork.SubCategory.GetFirstOrDefault(x => x.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            _unitOfWork.SubCategory.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "SubCategory deleted successfully";
            return RedirectToAction("Index");
        }
    }
}
