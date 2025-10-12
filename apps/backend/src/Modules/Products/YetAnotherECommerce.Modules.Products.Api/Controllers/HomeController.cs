using Microsoft.AspNetCore.Mvc;

namespace YetAnotherECommerce.Modules.Products.Api.Controllers
{
    [Route("products-module/")]
    internal class HomeController : ControllerBase
    {
        [HttpGet]
        public ActionResult<string> Get() => "Products Module API";
    }
}