using Data.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Models
{
    public class Teacher : BaseModel
    {
        public User User { get; set; }
        public School School { get; set; }
    }
}
