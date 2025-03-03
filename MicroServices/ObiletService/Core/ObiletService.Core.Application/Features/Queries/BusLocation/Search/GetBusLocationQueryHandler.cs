
using AutoMapper;
using MediatR;
using Newtonsoft.Json;
using ObiletService.Core.Application.Features.Queries.BusLocation.List;
using ObiletService.Core.Domain.Wrapper;
using System.Text;

namespace ObiletService.Core.Application.Features.Queries.BusLocation.Search
{
    public class SearchBusLocationQueryHandler : IRequestHandler<SearchBusLocationQueryRequest, Response<GetBusLocationQueryResponse>>
    {
        private readonly IMapper _mapper;
        public SearchBusLocationQueryHandler(IMapper mapper) 
        {
            _mapper = mapper;
        }
        public async Task<Response<GetBusLocationQueryResponse>> Handle(SearchBusLocationQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string jsonContent = JsonConvert.SerializeObject(request, new JsonSerializerSettings
                    {
                        Formatting = Formatting.Indented,
                        ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver() // Özel camelCase vs. olmasın
                    });

                    var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                    client.DefaultRequestHeaders.Add("Authorization", "Basic JEcYcEMyantZV095WVc3G2JtVjNZbWx1");

                    HttpResponseMessage httpResponse = await client.PostAsync("https://v2-api.obilet.com/api/location/getbuslocations", content);

                    string responseBody = await httpResponse.Content.ReadAsStringAsync();

                    var response = JsonConvert.DeserializeObject<GetBusLocationQueryResponse>(responseBody);

                    return new Response<GetBusLocationQueryResponse>
                    {
                        IsSuccessful = true,
                        Result = response
                    };
                }
            }
            catch (Exception ex)
            {
                return new Response<GetBusLocationQueryResponse>
                {
                    IsSuccessful = false,
                    HasExceptionError = true,
                    Message = ex.Message
                };
            }
        }
    }
}
