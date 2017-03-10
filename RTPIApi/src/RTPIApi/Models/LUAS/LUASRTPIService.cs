// LUASRTPIService.cs
using RTPIAPI.Services;
using RTPIAPI.ViewModels;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace RTPIAPI.Models
{
    // Service that knows how to query the LUAS RTPI api and transform it's response into the RTPIData
    // abstraction
    public class LUASRTPIService : IRTPISourceService
    {
        private IHttpRequestService _http;

        public LUASRTPIService(IHttpRequestService http)
        {
            this._http = http;
        }

        public async Task<RTPIData> GetRTPIForStop(string stopId)
        {
            RTPIData data = new RTPIData { Error = "Found no arrivals for stop" };

            var encodedStopId = WebUtility.UrlEncode(stopId);
            // TODO: pull this out of code
            string address = $"http://luasforecasts.rpa.ie/xml/get.ashx?action=forecast&encrypt=false&stop={encodedStopId}";

            var response = await _http.GetData(address);

            using (Stream rawResponse = await response.Content.ReadAsStreamAsync())
            {
                var xml = new XmlSerializer(typeof(LUASStopInfo));
                try
                {
                    var LUASData = xml.Deserialize(rawResponse) as LUASStopInfo;
                    if (LUASData != null)
                    {
                        data = TransformServiceData(LUASData);
                    }
                }
                catch (InvalidOperationException)
                {
                    // The LUAS API returns a plain string error message if it can not find the stop
                    // this will break the deserialization, just fall trough with error set above.
                }
            }

            return data;
        }

        private static RTPIData TransformServiceData(LUASStopInfo stopInfo)
        {
            RTPIData data = new RTPIData
            {
                Message = stopInfo.ServiceMessage,
                StopName = stopInfo.LongName,
                ValidTime = stopInfo.Created,
                Directions = (from direction in stopInfo.Directions
                              select new RTPIDirection
                              {
                                  Name = direction.DirectionName,
                                  Arrivals = (from arrival in direction.Arrivals
                                              select new RTPIArrival
                                              {
                                                  Destination = arrival.Destination,
                                                  DueIn = ConvertLUASArrivalStringToDateTime(arrival.DueMins)
                                              }).ToList()
                              }).ToList()
            };

            return data;
        }

        private static TimeSpan ConvertLUASArrivalStringToDateTime(string dueMins)
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

            throw new ArgumentException("LUAS dueMins duration format unknown, should be an int value or \"DUE\" ", nameof(dueMins));
        }
    }
}
