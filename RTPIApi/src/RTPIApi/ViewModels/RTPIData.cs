// RTPIData.cs
using System;
using System.Collections.Generic;

namespace RTPIAPI.ViewModels
{
    // Real Time Passenger Information abstraction
    public class RTPIData
    {
        public string Error { get; set; } = "";
        public string Message { get; set; }
        public string StopName { get; set; }
        public string ServiceName { get; set; }
        public DateTime ValidTime { get; set; }
        public List<RTPIDirection> Directions { get; set; }
    }
}
