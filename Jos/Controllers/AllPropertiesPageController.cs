using System.Diagnostics;
using EPiServer.Web.Mvc;
using Jos.ContentJson.Extensions;
using Jos.Models.Pages;

namespace Jos.Controllers
{
    public class AllPropertiesPageController : PageController<AllPropertiesPage>
    {
        public string Index(AllPropertiesPage currentPage)
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