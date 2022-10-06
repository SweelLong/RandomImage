using Microsoft.AspNetCore.Mvc;

namespace RandomImage.Controllers
{
    [Route("api/plugins/[controller]")]
    [ApiController]
    public class HelloController : ControllerBase
    {
        public ActionResult Get()
        {
            string str = $"欢迎使用本插件！";
            return Ok(str);
        }
    }
}