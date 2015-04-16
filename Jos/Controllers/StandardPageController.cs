using EPiServer.Web.Mvc;
using Jos.ContentJson.Extensions;
using Jos.Models.Pages;

namespace Jos.Controllers
{
    public class StandardPageController : PageController<StandardPage>
    {
        public string Index(StandardPage currentPage)
        {
            Response.ContentType = "application/json";
            return currentPage.ToJson();
        }
    }
}