using Microsoft.AspNetCore.Mvc;

namespace YetAnotherECommerce.Modules.Orders.Api.Controllers
{
    [Route("orders-module")]
    internal class HomeController : ControllerBase
    {
        [HttpGet]
        public ActionResult<string> Get() => "Orders Module API";
    }
}