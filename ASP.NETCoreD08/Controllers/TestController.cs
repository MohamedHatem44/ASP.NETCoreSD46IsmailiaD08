using ASP.NETCoreD08.Helper;
using Microsoft.AspNetCore.Mvc;

namespace ASP.NETCoreD08.Controllers
{
    public class TestController : Controller
    {
        /*------------------------------------------------------------------*/
        private readonly IPrint _print;
        private readonly IConfiguration _configuration;
        /*------------------------------------------------------------------*/
        public TestController(IPrint print, IConfiguration configuration)
        {
            _print = print;
            _configuration = configuration;
        }
        /*------------------------------------------------------------------*/

        public IActionResult Index()
        {
            _print.PrintDateTime();
            //_print.PrintDateTime2();
            return Content("Print");
        }
        /*------------------------------------------------------------------*/
        public IActionResult TestLifeTime()
        {
            ViewBag.Id =  _print.Id;
            _print.PrintDateTime();
            //_print.PrintDateTime2();
            return View();
        }
        /*------------------------------------------------------------------*/
        public IActionResult ReadFromConfigration()
        {
            var appName = _configuration.GetSection("AppName").Value;
            return Content($"AppName: {appName}");
        }
        /*------------------------------------------------------------------*/
    }
}
