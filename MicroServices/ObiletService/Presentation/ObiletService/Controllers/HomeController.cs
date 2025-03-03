using MediatR;
using Microsoft.AspNetCore.Mvc;
using ObiletService.Core.Application.Dto;
using ObiletService.Core.Application.Features.Queries.BusLocation.List;
using ObiletService.Core.Application.Features.Queries.BusLocation.Search;
using ObiletService.Core.Application.Features.Queries.Journeys.List;
using ObiletService.Core.Domain.Wrapper;
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
            try
            {
                request.DeviceSesssion = new Core.Application.Dto.SessionDto()
                {
                    DeviceId = HttpContext.Session.GetString("DeviceId"),
                    SessionId = HttpContext.Session.GetString("SessionId"),
                };
                request.Date = "2016-03-11T11:33:00";

                var response = await _mediator.Send(request);

                if (response.IsSuccessful)
                {
                    ViewBag.DefaultDepartureLocationId = response.Result.Data[0].Id;
                    ViewBag.DefaultDepartureLocation = response.Result.Data[0].Name;
                    ViewBag.DefaultDestinationLocationId = response.Result.Data[1].Id;
                    ViewBag.DefaultDestinationLocation = response.Result.Data[1].Name;
                    ViewBag.DefaultSelectedDate = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd");

                    return View(response.Result);
                }

                return View("Error", new ErrorDto { Message = $"Bir şeyler ters gitti ! daha sonra tekrar deneyiniz.\n Hata Mesajı: {response.Message}" });
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorDto { Message = ex.Message });
            }
        }

        [HttpPost("SearchBusLocations")]
        public async Task<Response<GetBusLocationQueryResponse>> SearchBusLocations([FromBody]SearchBusLocationQueryRequest request)
        {
            try
            {
                request.DeviceSesssion = new Core.Application.Dto.SessionDto()
                {
                    DeviceId = HttpContext.Session.GetString("DeviceId"),
                    SessionId = HttpContext.Session.GetString("SessionId"),
                };
                request.Date = "2016-03-11T11:33:00";

                var response = await _mediator.Send(request);

                return response;
            }
            catch (Exception ex)
            {
                return new Response<GetBusLocationQueryResponse>
                {
                    Message = ex.Message,
                    HasExceptionError = true,
                    IsSuccessful = false
                };
            }
        }

        [HttpGet("journey")]
        public async Task<IActionResult> Journey(long originId, long destinationId, string selectedDate)
        {
            try
            {
                if (DateTime.Parse(selectedDate).Date < DateTime.Now.Date || originId == destinationId)
                {
                    return View("Error", new ErrorDto { Message = "Arama kriterleri hatalı!" });
                }
                var request = new GetJourneysQueryRequest
                {
                    DeviceSesssion = new Core.Application.Dto.SessionDto()
                    {
                        DeviceId = HttpContext.Session.GetString("DeviceId"),
                        SessionId = HttpContext.Session.GetString("SessionId"),
                    },
                    Date = "2016-03-11T11:33:00",
                    Data = new GetJourneysRequestData()
                    {
                        OriginId = originId,
                        DestinationId = destinationId,
                        DepartureDate = selectedDate
                    }
                };
                var response = await _mediator.Send(request);

                if (!response.IsSuccessful)
                    return View("Error", new ErrorDto { Message = $"Bir şeyler ters gitti ! daha sonra tekrar deneyiniz. \n Hata Mesajı: {response.Message}" });

                if (response.Result != null && response.Result.Journeys.Count > 0)
                {
                    return View(response.Result);
                }

                return View("JourneyNotFound");
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorDto { Message = ex.Message });
            }
        }

        [HttpGet("error")]
        public async Task<IActionResult> Error()
        {
            return View();
        }
    }
}
