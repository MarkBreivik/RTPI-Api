// IHttpRequestService.cs

using System.Net.Http;
using System.Threading.Tasks;

namespace RTPIAPI.Services
{
    public interface IHttpRequestService
    {
        Task<HttpResponseMessage> GetData(string queryString);
    }
}
