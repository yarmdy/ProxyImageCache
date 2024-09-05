using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System.Text;
using System.Web;

namespace ProxyImageCache.Controllers
{
    public class ProxyImageController : Controller
    {
        [FromServices]
        public IHttpClientFactory HttpClientFactory { get; set; } = default!;
        protected HttpClient GetHttpClient() => HttpClientFactory.CreateClient();
        public async Task<IActionResult> Index(string url)
        {
            var res = await GetHttpClient().GetAsync(url);
            var ms = await res.Content.ReadAsStreamAsync();
            Response.Headers.CacheControl = "max-age=15552000";
            return File(ms, res.Content.Headers.ContentType?.ToString() ?? "image/jpg");
        }
    }
}
