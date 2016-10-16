using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DriveIt.Models
{
    public class CarOutputModel
    {
        public PidModel PidModel { get; set; }
        public int Value { get; set; }
        public double Rating { get; set; }

    }
}