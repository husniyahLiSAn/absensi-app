using Data.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Models
{
    public class Presence : BaseModel
    {
        public Class Class { get; set; }
        public Student Student { get; set; }
        public Score Score { get; set; }
        public DateTime PresenceDate { get; set; }
    }
}
