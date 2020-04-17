using System;
using System.Collections.Generic;
using System.Text;

namespace Data.ViewModels
{
    public class PresenceVM
    {
        public int Id { get; set; }
        public int ClassId { get; set; }
        public int StudentId { get; set; }
        public string ClassName { get; set; }
        public string StudentName { get; set; }
        public int ScoreId { get; set; }
        public string Note { get; set; }
        public DateTime PresenceDate { get; set; }
    }
}
