using System;
using System.Collections.Generic;
using System.Text;

namespace OpenGpsTracker.Model
{
    public class TrackerServerInfo
    {
        public Tracker Tracker { get; set; }
        public Coordinates Coordinates { get; set; }
        public int ChargeLvl { get; set; }

    }

    public class Coordinates
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }



}
