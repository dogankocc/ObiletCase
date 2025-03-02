using MediatR;
using Microsoft.AspNetCore.Mvc;
using ObiletService.Core.Application.Features.Queries.BusLocation.List;
using ObiletService.Core.Application.Features.Queries.Journeys.List;
using ObiletService.Filters;

namespace ObiletService.Controllers
{
    [Route("[controller]")]
    [SessionAttribute]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMediator _mediator;

        public HomeController(ILogger<HomeController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }
        [HttpGet("index")]
        public async Task<IActionResult> Index(GetBusLocationQueryRequest request)
        {
            request.DeviceSesssion = new Core.Application.Dto.SessionDto()
            {
                DeviceId = HttpContext.Session.GetString("DeviceId"),
                SessionId = HttpContext.Session.GetString("SessionId"),
            };
            request.Date = "2016-03-11T11:33:00";

            var response = await _mediator.Send(request);
            if(response.IsSuccessful)
                return View(response.Result);

            return RedirectToAction("Error");
        }

        // GET: HomeController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }
    }
}
