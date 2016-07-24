using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EasyGeneratorAdditionalProject.Web.JsonResults
{
    public class JsonSuccessResult : JsonResult
    {
        public JsonSuccessResult(object data)
        {
            this.Data = new { Success = true, RequestData = data };
            this.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
        }
    }
}