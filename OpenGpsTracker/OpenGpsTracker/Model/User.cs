using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace OpenGpsTracker.Model
{
    [Table("Users")]
    public class User
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        //public List<TrackerUserSettings> Trackers { get; set; } //Personal user trackers settings

    }

    public class TrackerUserSettings
    {
        public Tracker Tracker { get; set; }
        public string ColorHex { get; set; }
    }

}
