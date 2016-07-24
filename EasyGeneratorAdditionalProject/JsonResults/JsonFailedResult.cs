using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EasyGeneratorAdditionalProject.Web.JsonResults
{
    public class JsonFailedResult : JsonResult
    {
        public JsonFailedResult(string errorMessage)
        {
            this.Data = new { Success = false, RequestData = errorMessage };
            this.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
        }
    }
}