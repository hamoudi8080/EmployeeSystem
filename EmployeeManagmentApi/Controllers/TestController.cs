using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagmentApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    /*
     attribute [Authorize].
     This means this Controller can only be interacted with, if the caller provides a valid JWT.
    */
    [Authorize]
    public class TestController : ControllerBase
    {
        [HttpGet("authorized")]
        public ActionResult GetAsAuthorized()
        {
            return Ok("This was accepted as authorized");
        }


    }
}
