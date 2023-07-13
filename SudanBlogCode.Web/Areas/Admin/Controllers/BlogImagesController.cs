using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SudanBlogCode.DataAccess.Repository.IRepository;
using SudanBlogCode.Models;
using SudanBlogCode.Models.ViewModels;

namespace SudanBlogCode.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class BlogImagesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly UserManager<IdentityUser> _applicationUser;

        public BlogImagesController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment, UserManager<IdentityUser> applicationUser)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
            _applicationUser = applicationUser;
        }

        public IActionResult Index()
        {
            IEnumerable<BlogImages> objImagesList = _unitOfWork.BlogImages.GetAll(includeProperties: "SubCategory");
            return View(objImagesList);
        }

        public IActionResult Create()
        {
            BlogImagesVM obj = new()
            {
                BlogImages = new(),
                SubCategoryList = _unitOfWork.SubCategory.GetAll(includeProperties: "Category").Select(i => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Text = i.SubCategoryName,
                    Value = i.Id.ToString()
                })
            };
            return View(obj);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(BlogImagesVM obj, List<IFormFile>? files)
        {
            List<BlogImages> objList = new List<BlogImages>();
            obj.SubCategoryList = _unitOfWork.SubCategory.GetAll(includeProperties: "Category").Select(i => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
            {
                Text = i.SubCategoryName,
                Value = i.Id.ToString()
            });

            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if (files != null && files.Count>0)
                {
                    foreach (var file in files)
                    {
                        BlogImages objSingle = new BlogImages();
                        string fileName = Guid.NewGuid().ToString();
                        var uploads = Path.Combine(wwwRootPath, @"images\BlogImages");
                        var extension = Path.GetExtension(file.FileName);

                        using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                        {
                            file.CopyTo(fileStreams);
                        }
                        obj.BlogImages.ImageUrl = @"\images\category\" + fileName + extension;
                        obj.BlogImages.CreatedBy = _applicationUser.GetUserId(HttpContext.User);
                        objSingle.BlogTitle = obj.BlogImages.BlogTitle;
                        objSingle.ImageUrl = obj.BlogImages.ImageUrl;
                        objSingle.SubCategoryId = obj.BlogImages.SubCategoryId;
                        objSingle.CreatedBy = obj.BlogImages.CreatedBy;
                        objList.Add(objSingle);
                    }
                    _unitOfWork.BlogImages.AddRange(objList);
                    _unitOfWork.Save();
                    TempData["success"] = "Blog Images uploded successfully";
                    return RedirectToAction("Index");
                }
            }
            return View(obj);
        }
    }
}
