using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System.Text;
using System.Web;

namespace ProxyImageCache.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var url = HttpUtility.UrlEncode("https://api.stage.loootus.cn/documents/a2ae8af400b540f0b2e44763e374b506");
            return Content($"""<img src="/ProxyImage?url={url}" />""","text/html",Encoding.UTF8);
        }

    }
}
