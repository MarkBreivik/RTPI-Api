// DublinBusRTPIService.cs

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RTPIAPI.Services;
using RTPIAPI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace RTPIAPI.Models
{
    // Service that knows how to query the Dublin Bus RTPI api and transform it's response into the RTPIData
    // abstraction
    public class DublinBusRTPIService : IRTPISourceService
    {
        private IHttpRequestService _http;

        public DublinBusRTPIService(IHttpRequestService http)
        {
            this._http = http;
        }

        public async Task<RTPIData> GetRTPIForStop(string stopId)
        {
            RTPIData data = new RTPIData { Error = "Found no arrivals for stop" };

            var encodedStopId = WebUtility.UrlEncode(stopId);
            // TODO: pull this out of code
            string address = $"https://data.dublinked.ie/cgi-bin/rtpi/realtimebusinformation?stopid={encodedStopId}&format=json";

            var response = await _http.GetData(address);
            var rawJson = await response.Content.ReadAsStringAsync();
            try
            {
                var jsonResults = JObject.Parse(rawJson);
                data = TransformServiceData(jsonResults);
            }
            catch (JsonException)
            {
                // Failed to parse the JSON, give the user an error.
            }

            return data;
        }

        private static RTPIData TransformServiceData(JObject jsonResults)
        {
            RTPIData data = new RTPIData
            {
                StopName = (string)jsonResults["stopid"],
                Message = (string)jsonResults["errormessage"],
                Directions = new List<RTPIDirection>
                {
                    new RTPIDirection
                    {
                        Name = "Inbound",
                        Arrivals = (from result in jsonResults["results"]
                                    where (string)result["direction"] == "Inbound"
                                    select ExtractArrivalDetails(result))
                                    .ToList()
                    },
                    new RTPIDirection
                    {
                        Name = "Outbound",
                        Arrivals = (from result in jsonResults["results"]
                                    where (string)result["direction"] == "Outbound"
                                    select ExtractArrivalDetails(result))
                                    .ToList()
                    }
                }
            };
            return data;
        }

        private static RTPIArrival ExtractArrivalDetails(JToken result)
        {
            return new RTPIArrival
            {
                Destination = (string)result["destination"],
                DueIn = ConvertBusArrivalStringToDateTime((string)result["duetime"]),
                RouteName = (string)result["route"]
            };
        }

        private static TimeSpan ConvertBusArrivalStringToDateTime(string dueMins)
        {

            if (dueMins.ToUpper() == "DUE")
            {
                return TimeSpan.FromMinutes(0);
            }
            else
            {
                int ts = 0;
                if (int.TryParse(dueMins, out ts))
                {
                    return TimeSpan.FromMinutes(ts);
                }
            }

            throw new ArgumentException("Can't convert Dublin Bus due time string", nameof(dueMins));
        }

    }
}
