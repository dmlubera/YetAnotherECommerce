using Microsoft.AspNetCore.Mvc;

namespace YetAnotherECommerce.Modules.Carts.Api.Controllers
{
    [Route("carts-module/")]
    internal class HomeController : ControllerBase
    {
        [HttpGet]
        public ActionResult<string> Get() => "Carts Module API";
    }
}