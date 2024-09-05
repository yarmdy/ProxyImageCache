using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace ProxyImageCache.Controllers
{
    public class ProxyImageController : Controller
    {
        [FromServices]
        public IHttpClientFactory HttpClientFactory { get; set; } = default!;
        [FromServices]
        public IOptionsMonitor<FileCacheOptions> CacheOptions { get; set; }=default!;
        protected HttpClient GetHttpClient() => HttpClientFactory.CreateClient();
        public async Task<IActionResult> Index(string url)
        {

            var kehu = Request.Headers.IfNoneMatch;
            var fileTime = CacheOptions.CurrentValue.LastModified;
            var etagVal = $"\"{string.Join("", BitConverter.GetBytes(fileTime.GetHashCode()).Select(a => a.ToString("x2")))}{(uint)url.GetHashCode()}\"";
            //判断协商缓存
            if (kehu == etagVal)
            {
                Response.StatusCode = (int)HttpStatusCode.NotModified;
                return Empty;
            }


            var res = await GetHttpClient().GetAsync(url);
            var ms = await res.Content.ReadAsStreamAsync();
            
            

            //正规的协商缓存实现
            return File(ms, res.Content.Headers.ContentType?.ToString() ?? "image/jpg",new DateTimeOffset(DateTime.UtcNow, TimeSpan.Zero), EntityTagHeaderValue.Parse(etagVal));
        }
    }
}
