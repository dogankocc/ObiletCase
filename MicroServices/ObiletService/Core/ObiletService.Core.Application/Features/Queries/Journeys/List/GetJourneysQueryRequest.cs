
using MediatR;
using ObiletService.Core.Application.Dto;
using ObiletService.Core.Domain.Wrapper;

namespace ObiletService.Core.Application.Features.Queries.Journeys.List
{
    public class GetJourneysQueryRequest : PaginationDto,  IRequest<Response<GetJourneysQueryResponse>>
    {
        public SessionDto Session { get; set; }
    }
}
