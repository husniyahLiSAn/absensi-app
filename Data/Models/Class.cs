using Data.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Models
{
    public class Class : BaseModel
    {
        public string ClassName { get; set; }
        public string Grade { get; set; }
        public Teacher Teacher { get; set; }
    }
}
