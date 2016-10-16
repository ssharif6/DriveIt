using DriveIt.Managers;
using DriveIt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using DriveIt.DTO;

namespace DriveIt.Controllers
{
    public class CarValuesController : ApiController
    {
        public static CarInfoManager _manager = new CarInfoManager();
        // GET: CarValues
        public string Index()
        {
            return "hello";
        }

        public CarOutputModel Post(int userId, int carId, double value, string pId)
        {
            // this should talk to the PID DB to get actual information given the pid
            // This should INSERT data into Histories table, then calculate rating
            _manager.PostData(userId, carId, value, pId);
            // TODO: Calculate Rating
            return null;
        }

        public GetCarInfoDto Get(int userId, int carId)
        {
            // Gets Info from History Table
            var resp = _manager.GetUserCarInfo(userId, carId);
            return resp;
        }
    }
}