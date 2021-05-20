using System.Web.Http;

namespace JukkaOWINTest.Controller
.Controllers
{
    [Authorize]
    public class POCController : ApiController
    {
        [HttpGet]
        [Route("api/TestMethod")]
        public string TestMethod()
        {
            return "Hello, From the Jukka Cloud Service. ";
        }

    }
}