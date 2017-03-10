// RTPIServiceFactory.cs

using RTPIAPI.Models;
using System.Collections.Generic;

namespace RTPIAPI.Services
{
    // TODO: This is very barebones.
    public class RTPIServiceFactory : IRTPIServiceFactory
    {
        Dictionary<string, IRTPISourceService> _serviceTable = 
            new Dictionary<string, IRTPISourceService>();
        
        public void RegisterRTPIService(string serviceId, IRTPISourceService service)
        {
            _serviceTable.Add(serviceId, service);
        }

        public IRTPISourceService GetRTPIService(string serviceId)
        {
            if (_serviceTable.ContainsKey(serviceId))
            {
                return _serviceTable[serviceId];
            }
            else
            {
                return null;
            }
        }
    }
}
