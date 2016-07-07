using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EasyGeneratorAdditionalProject.Controllers
{
    public class CoursesController : Controller
    {
        
        [Route("courses", Name = "IndexMain")]
        public ActionResult Index()
        {
            return View();
        }
    }
}