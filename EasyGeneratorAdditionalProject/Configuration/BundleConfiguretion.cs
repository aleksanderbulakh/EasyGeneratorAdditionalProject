using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace EasyGeneratorAdditionalProject.Web.Configuration
{
    public class BundleConfiguretion
    {
        public static void Configure()
        {
            var bundles = BundleTable.Bundles;
            bundles.IgnoreList.Clear();

            bundles.Add(
              new ScriptBundle("~/Scripts/vendor")
                  .Include("~/Scripts/jquery-{version}.js")
                  .Include("~/Scripts/knockout-{version}.js")
                  .Include("~/Scripts/less-{version}.js")
                  .Include("~/Scripts/q.min.js")
                  .Include("~/Scripts/underscore.min.js")
                  .IncludeDirectory("~/Scripts/extenders/", "*extender.js")
                  .IncludeDirectory("~/Scripts/knockout-bindings/", "*binding.js")
              );

            bundles.Add(
              new StyleBundle("~/Content/css")
                .Include("~/Content/durandal.css")
              );
        }
    }
}