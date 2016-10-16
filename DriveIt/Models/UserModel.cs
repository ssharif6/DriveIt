using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DriveIt.Models
{
    public class UserModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CarModel { get; set; }
        public string CarMake { get; set; } // make enum
        public string CarYear { get; set; }
        public int Age { get; set; }
    }
}