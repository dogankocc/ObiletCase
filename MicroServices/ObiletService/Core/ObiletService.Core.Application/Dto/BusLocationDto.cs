using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObiletService.Core.Application.Dto
{
    public class BusLocationDto
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("long-name")]
        public string LongName { get; set; }

        public string Name { get; set; }

        [JsonProperty("city-name")]
        public string CityName { get; set; }

        [JsonProperty("country-name")]
        public string CountryName { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("keywords")]
        public string Keywords { get; set; }

    }
}
