using Data.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Models
{
    public class School : BaseModel
    {
        public string SchoolName { get; set; }
        public string Address { get; set; }
        public Country Country { get; set; }
    }
}
