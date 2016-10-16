using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DriveIt.Models
{
    public class CarInputModel
    {
        public UserModel UserModel { get; set; }
        public int Value { get; set; }
        public PidModel Pid { get; set; }
    }
}