using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DriveIt.Models
{
    public class PidModel
    {
        public string Description { get; set; }
        public string Pid { get; set; }
        public string Units { get; set; }
        public double Value { get; set; }
        public double NationalValue { get; set; }
    }
}