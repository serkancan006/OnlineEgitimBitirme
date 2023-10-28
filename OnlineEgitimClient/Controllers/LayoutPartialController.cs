using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace OnlineEgitimClient.Controllers
{
    public class LayoutPartialController : Controller
    {
        public PartialViewResult HeadPartial()
        {
            return PartialView();
        }
        public PartialViewResult TopbarPartial()
        {
            return PartialView();
        }
        public PartialViewResult NavbarPartial()
        {
            return PartialView();
        }
        public PartialViewResult FooterPartial()
        {
            return PartialView();
        }
        public PartialViewResult ScriptPartial()
        {
            return PartialView();
        }
    }
}
