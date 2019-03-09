using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace OpenGpsTracker.Model
{
    [Table("Trackers")]
    public class Tracker
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int UserID { get; set; }
        public string DeviceID { get; set; } //Server device id
        public string Description { get; set; }
        public bool Enabled { get; set; } //define will show on map or not
        public bool AutoUpdateEnable { get; set; }
        public int AutoUpdateInterval { get; set; } //Seconds
    }
}
