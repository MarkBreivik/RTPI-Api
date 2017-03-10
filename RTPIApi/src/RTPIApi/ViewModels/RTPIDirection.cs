// RTPIDirection.cs
using System.Collections.Generic;

namespace RTPIAPI.ViewModels
{
    public class RTPIDirection
    {
        public string Name { get; set; }
        public List<RTPIArrival> Arrivals { get; set; }
    }
}