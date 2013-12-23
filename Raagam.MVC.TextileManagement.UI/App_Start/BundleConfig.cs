using System.Web;
using System.Web.Optimization;

namespace Raagam.MVC.TextileManagement.UI
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-1.9.1.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-1.8.20.custom.js",
                        "~/Scripts/jquery-migrate-1.2.1.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));


            bundles.Add(new ScriptBundle("~/bundles/themes").Include(
              "~/js/bootstrap/bootstrap.js",
              "~/plugins/forms/uniform/jquery.uniform.js" 
              ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryplugin").Include(
  "~/js/jquery.mousewheel.js",
  "~/js/libs/jRespond.min.js"
  ));
            bundles.Add(new ScriptBundle("~/bundles/charts").Include(
  "~/plugins/charts/sparkline/jquery.sparkline.min.js" 
  ));

            bundles.Add(new ScriptBundle("~/bundles/misc").Include(
"~/plugins/misc/qtip/jquery.qtip.min.js",
"~/plugins/misc/totop/jquery.ui.totop.min.js"
));

            bundles.Add(new ScriptBundle("~/bundles/search").Include(
"~/plugins/misc/search/tipuesearch_set.js",
"~/plugins/misc/search/tipuesearch_data.js",
"~/plugins/misc/search/tipuesearch.js"
));


            bundles.Add(new ScriptBundle("~/bundles/grid").Include(
                "~/scripts/i18n/grid.locale-en.js",
"~/scripts/jquery.jqGrid.src.js" 
));

            bundles.Add(new ScriptBundle("~/bundles/mask").Include(

 "~/plugins/forms/maskedinput/jquery.inputmask.js",
 "~/plugins/forms/maskedinput/jquery.inputmask.date.extensions.js",
 "~/plugins/forms/maskedinput/jquery.inputmask.extensions.js",
 "~/plugins/forms/maskedinput/jquery.inputmask.numeric.extensions.js",
 "~/plugins/forms/maskedinput/jquery.inputmask.phone.extensions.js",
 "~/plugins/forms/maskedinput/jquery.inputmask.regex.extensions.js",
 "~/plugins/forms/togglebutton/jquery.toggle.buttons.js",
 "~/Scripts/chosen.jquery.js",
  "~/Scripts/prism.js"

));


            bundles.Add(new ScriptBundle("~/bundles/form").Include(
"~/plugins/forms/elastic/jquery.elastic.js",
    "~/plugins/forms/elastic/jquery.elastic.js",
    "~/plugins/forms/inputlimiter/jquery.inputlimiter.1.3.min.js",
    "~/plugins/forms/togglebutton/jquery.toggle.buttons.js",
    "~/plugins/forms/uniform/jquery.uniform.min.js",
    "~/plugins/forms/globalize/globalize.js",
    "~/plugins/forms/color-picker/colorpicker.js",
    "~/plugins/forms/timeentry/jquery.timeentry.min.js",
    "~/plugins/forms/select/select2.min.js",
    "~/plugins/forms/dualselect/jquery.dualListBox-1.3.min.js",
    "~/plugins/forms/tiny_mce/tinymce.min.js",
    "~/js/supr-theme/jquery-ui-sliderAccess.js" 

));




            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

          
            bundles.Add(new StyleBundle("~/Content/css").Include(
                        "~/css/bootstrap/bootstrap.css",
                        "~/css/bootstrap/bootstrap-theme.css",
                        "~/css/bootstrap/bootstrap-responsive.css",
                        "~/css/supr-theme/jquery.ui.supr.css",
                        "~/css/icons.css",
                        "~/css/main.css",
                        "~/css/custom.css"));

        

            bundles.Add(new StyleBundle("~/Content/plugin").Include(
                       "~/plugins/misc/qtip/jquery.qtip.css",
                       "~/plugins/forms/inputlimiter/jquery.inputlimiter.css",
                       "~/plugins/forms/togglebutton/toggle-buttons.css",
                       "~/plugins/forms/uniform/uniform.default.css",
                       "~/plugins/forms/color-picker/color-picker.css",
                       "~/plugins/forms/select/select2.css",
                       "~/plugins/forms/validate/validate.css",
                       "~/Content/chosen.css",
                       "~/Content/prism.css",
                       "~/plugins/forms/smartWizzard/smart_wizard.css"));
        }
    }
}