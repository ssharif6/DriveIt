using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DriveIt.DTO
{
    public class GetCarInfoDto
    {
        public int HistoryId { get; set; }
        public int UserId { get; set; }
        public int CarId { get; set; }
        public string PId { get; set; }
        public double Value { get; set; }
    }
}