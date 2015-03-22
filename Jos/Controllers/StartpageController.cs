using System.Diagnostics;
using ContentJson.Extensions;
using EPiServer.Web.Mvc;
using Jos.Models.Pages;

namespace Jos.Controllers
{
    public class StartpageController : PageController<Startpage>
    {
        public string Index(Startpage currentPage)
        {
            Response.ContentType = "application/json";
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            var json = currentPage.ToJson();
            stopwatch.Stop();
            Debug.WriteLine(stopwatch.ElapsedMilliseconds);
            return json;
        }
    }
}