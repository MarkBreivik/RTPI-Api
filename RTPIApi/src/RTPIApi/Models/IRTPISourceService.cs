// IRTPISourceService.cs
using RTPIAPI.ViewModels;
using System.Threading.Tasks;

namespace RTPIAPI.Models
{
    public interface IRTPISourceService
    {
        // abstract the calling of and returning of RTPI from an online service (LUAS, DublinBus, etc)
        Task<RTPIData> GetRTPIForStop(string stopId);
    }
}
