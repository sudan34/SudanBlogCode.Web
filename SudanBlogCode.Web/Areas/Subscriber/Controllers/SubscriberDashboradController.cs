using Microsoft.AspNetCore.Mvc;

namespace SudanBlogCode.Web.Areas.Subscriber.Controllers
{
    public class SubscriberDashboradController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
