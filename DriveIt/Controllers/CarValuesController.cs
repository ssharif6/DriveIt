using DriveIt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DriveIt.Controllers
{
    public class CarValuesController : Controller
    {
        
        // GET: CarValues
        public ActionResult Index()
        {
            return View();
        }

        public CarOutputModel Post(int userId, int pid, int value)
        {
            // this should talk to the PID DB to get actual information given the pid
            var pidModel = new PidModel
            {
                Description = "blah",
                MaxValue = 255,
                MinValue = 0,
                Pid = pid,
                Units = "units"
            };

            var model = new CarOutputModel
            {
                PidModel = pidModel,
                Rating = 1.0,
                Value = value
            };

            return model;
        }

        public CarInputModel Get(int userId)
        {
            // get User

        }
    }
}