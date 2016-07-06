using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace EasyGeneratorAdditionalProject.App_Start
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

              );

            bundles.Add(
              new StyleBundle("~/Content/css")
                .Include("~/Content/durandal.css")
              );
        }
    }
}