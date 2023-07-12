using Microsoft.AspNetCore.Mvc;

namespace SudanBlogCode.Web.Areas.Author.Controllers
{
    public class AuthorDashboradController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
