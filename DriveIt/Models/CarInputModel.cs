using System.Collections.Generic;

namespace DriveIt.Models
{
    public class CarInputModel
    {
        public UserModel UserModel { get; set; }
        public Dictionary<string, PidModel> PidModel { get; set; }
    }
}