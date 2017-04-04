using Orchard.Themes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BaseCourse.Presentation.Controllers
{
    [Themed]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

    }
}
