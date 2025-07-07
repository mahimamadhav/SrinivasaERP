using Microsoft.AspNetCore.Mvc;

namespace SrinivasaERP.Controllers
{
    public class Leave : Controller
    {
        public IActionResult LeaveManagement()
        {
            return View();
        }
    }
}
