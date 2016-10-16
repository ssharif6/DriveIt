using DriveIt.Managers;
using DriveIt.Models;
using System.Web.Http;

namespace DriveIt.Controllers
{
    public class CarValuesController : ApiController
    {
        public static CarInfoManager _manager = new CarInfoManager();

        public int Post(string make, string model, int year, bool isHybrid)
        {
            return _manager.GetCarId(make, model, year, isHybrid);
        }

        public void Post(int userId, int carId, double value, string pId)
        {
            // this should talk to the PID DB to get actual information given the pid
            // This should INSERT data into Histories table, then calculate rating
            _manager.PostData(userId, carId, value, pId);
            if (pId == "23" || pId == "43" || pId == "21")
            {
                // push notification
            }
        }

        public CarInputModel Get(int userId, int carId)
        {
            // Gets Info from History Table
            return _manager.GetUserCarInfo(userId, carId);
        }
    }
}