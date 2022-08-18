using AppService.DepartmentRequests.MRQueries.RequestsQueryMR;
using Core.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NUPCO.Api.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestController : ControllerBase
    {
        private readonly IMediator _mediatR;

        public RequestController(IMediator mediatR)
        {
            _mediatR = mediatR;
        }

        [HttpGet]
        public IActionResult Get()
        {
            GetAllRequestsQueryMR query = new GetAllRequestsQueryMR();
            var result =_mediatR.Send(query).Result;
            return Ok(result);
        }

    }

}
