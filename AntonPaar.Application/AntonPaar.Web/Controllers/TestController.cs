using Microsoft.AspNetCore.Mvc;

namespace AntonPaar.Web.Controllers
{
    public class TestController : Controller
    {
        public TestController(ILogger<TestController> t) 
        {
            Console.Write(t);
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
