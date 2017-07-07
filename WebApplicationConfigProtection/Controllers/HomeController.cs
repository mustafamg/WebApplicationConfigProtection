using System.Configuration;
using System.Web.Mvc;

namespace WebApplicationConfigProtection.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        public ContentResult CS()
        {
            return Content(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
        }
        public ActionResult Install()
        {
#if !DEBUG

            //Opens the specified client configuration file as a Configuration object 
            Configuration config = ConfigurationManager.OpenExeConfiguration("");

            // Get the connectionStrings section. 
            ConfigurationSection section = config.GetSection("connectionStrings");

            //Ensures that the section is not already protected 
            if (!section.SectionInformation.IsProtected)
            {
                //Uses the Windows Data Protection API (DPAPI) to encrypt the 
                //configuration section using a machine-specific secret key 
                section.SectionInformation.ProtectSection(
                    "DataProtectionConfigurationProvider");
                config.Save();
            }
#endif
            return Content("Installed");
        }

    }
}
