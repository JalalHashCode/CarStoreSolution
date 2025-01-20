using Microsoft.AspNetCore.Mvc;

namespace CarStoreApi.Controllers
{
    public class Users : Controller
    {
        [Route("api/CarStoreApi")]
        [ApiController]
        public IActionResult Index()
        {
            return View();
        }
    }
}
