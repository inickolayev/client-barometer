using Microsoft.AspNetCore.Mvc;

namespace ClientBarometer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class BaseApiController : Controller
    {
    }
}
