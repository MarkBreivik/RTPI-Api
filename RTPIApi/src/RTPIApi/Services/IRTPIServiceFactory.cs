// IRTPIServiceFactory.cs

using RTPIAPI.Models;

namespace RTPIAPI.Services
{
    public interface IRTPIServiceFactory
    {
        void RegisterRTPIService(string serviceId, IRTPISourceService service);
        IRTPISourceService GetRTPIService(string serviceId);
    }
}
