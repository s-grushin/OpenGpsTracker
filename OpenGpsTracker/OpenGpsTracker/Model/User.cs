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
        [Unique]
        public string Login { get; set; }
        public string Password { get; set; }
        public bool Current { get; set; }

        [Ignore]
        public List<Tracker> Trackers { get; set; } //Personal user trackers settings

    }

}
