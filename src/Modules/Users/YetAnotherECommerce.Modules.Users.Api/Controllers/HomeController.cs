using Microsoft.AspNetCore.Mvc;

namespace YetAnotherECommerce.Modules.Users.Api.Controllers
{
    [Route("users-module/")]
    internal class HomeController : ControllerBase
    {
        [HttpGet]
        public ActionResult<string> Get() => "Users Module API";
    }
}