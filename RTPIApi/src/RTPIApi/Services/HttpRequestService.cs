// HttpRequestService.cs
using System.Net.Http;
using System.Threading.Tasks;

namespace RTPIAPI.Services
{
    public class HttpRequestService : IHttpRequestService
    {
        public async Task<HttpResponseMessage> GetData(string queryString)
        {
            HttpResponseMessage response;
            using (var http = new HttpClient())
            {
                //TODO: much error handling
                response = await http.GetAsync(queryString); 
            }

            return response;
        }
    }
}
