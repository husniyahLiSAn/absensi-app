using Data.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Models
{
    public class Student : BaseModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Class Class { get; set; }
    }
}
