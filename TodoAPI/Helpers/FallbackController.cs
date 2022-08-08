using Microsoft.AspNetCore.Mvc;

namespace TodoAPI.Helpers;

public class FallbackController : Controller
{
    public ActionResult Index()
    {
        return PhysicalFile(
            Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "index.html"),
            "text/html");
    }
}
