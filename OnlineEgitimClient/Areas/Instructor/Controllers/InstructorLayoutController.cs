using Microsoft.AspNetCore.Mvc;

namespace OnlineEgitimClient.Areas.Instructor.Controllers
{
    [Area("Instructor")]
    public class InstructorLayoutController : Controller
    {
        public PartialViewResult HeadPartial()
        {
            return PartialView();
        }
        public PartialViewResult NavbarPartial()
        {
            return PartialView();
        }
        public PartialViewResult SidebarPartial()
        {
            return PartialView();
        }
        public PartialViewResult ScriptPartial()
        {
            return PartialView();
        }
    }
}
