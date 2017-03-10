// RTPIArrival.cs
using System;

namespace RTPIAPI.ViewModels
{
    public class RTPIArrival
    {
        public TimeSpan DueIn { get; set; }
        public bool Due { get { return DueIn.TotalMinutes == 0; } }
        public string Destination { get; set; }
        public string RouteName { get; set; }
    }
}
