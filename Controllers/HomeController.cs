using Microsoft.AspNetCore.Mvc;
using PluginCore;

namespace RandomImage.Controllers
{
    [Route("Plugins/RandomImage")]
    public class HomeController : Controller
    {
        public ActionResult Get()
        {
            string indexFilePath = System.IO.Path.Combine(PluginPathProvider.PluginWwwRootDir("RandomImage"), "index.html");
            return PhysicalFile(indexFilePath, "text/html");
        }
    }
}