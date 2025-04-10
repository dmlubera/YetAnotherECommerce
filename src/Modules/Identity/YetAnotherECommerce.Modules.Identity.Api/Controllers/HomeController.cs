using Microsoft.AspNetCore.Mvc;

namespace YetAnotherECommerce.Modules.Identity.Api.Controllers;

[Route("identity-module/")]
internal class HomeController : ControllerBase
{
    [HttpGet]
    public ActionResult<string> Get() => "Identity Module API";
}