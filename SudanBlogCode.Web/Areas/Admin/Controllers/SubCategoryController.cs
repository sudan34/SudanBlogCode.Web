using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SudanBlogCode.DataAccess.Repository.IRepository;
using SudanBlogCode.Models;

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
    }
}
