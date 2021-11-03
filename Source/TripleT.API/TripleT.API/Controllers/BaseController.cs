using MediatR;
using Microsoft.AspNetCore.Mvc;
using TripleT.API.Filter;

namespace TripleT.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ServiceFilter(typeof(ExceptionFilter))]
    public class BaseController : ControllerBase
    {
        protected IMediator Mediator { get; private set; }

        public BaseController(IMediator mediator)
        {
            Mediator = mediator;
        }
    }
}
