using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.ObjectModel;
using System.Diagnostics.Metrics;

namespace webapi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiExplorerSettings(IgnoreApi = true)]
    [ApiController]
    public class ServicesLifeCycleController : ControllerBase
    {

        public ServicesLifeCycleController()
        {
                
        }
    }
}
