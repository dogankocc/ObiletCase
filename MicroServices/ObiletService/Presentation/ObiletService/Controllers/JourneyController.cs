using MediatR;
using Microsoft.AspNetCore.Mvc;
using ObiletService.Core.Application.Features.Queries.Journeys.List;
using ObiletService.Core.Domain.Wrapper;

namespace ObiletService.Controllers
{
    [ApiController]
    [Route("obiletapi/[controller]")]
    public class JourneyController : ControllerBase
    {

        private readonly ILogger<JourneyController> _logger;
        private readonly IMediator _mediator;
        public JourneyController(ILogger<JourneyController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpPost("getbuslocations")]
        public async Task<Response<GetJourneysQueryResponse>> GetJourneys(GetJourneysQueryRequest request)
        {
            return await _mediator.Send(request);
        }
    }
}
