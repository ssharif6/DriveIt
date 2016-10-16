using DriveIt.Managers;
using DriveIt.Models;
using System;
using System.Net.Http;
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
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, @"https://prod-05.westus.logic.azure.com:443/workflows/2cd0b9b38c38493aa77e97643861978c/triggers/request/run?api-version=2016-06-01&sp=%2Ftriggers%2Frequest%2Frun&sv=1.0&sig=qynkVrL3QEuk3KGluUFCFkh-1hpjd7XsRzbQsR9YfVc");
                var random = new Random();
                string error = string.Empty;
                switch (random.Next(5)) {
                    case 0:
                        error = "Intake air temperature: " + (random.Next(15) + 200) + " Degrees Celcius";
                        break;
                    case 1:
                        error = "Distance traveled with malfunction indicator lamp (MIL) on: " + (random.Next(535) + 65000) + "km";
                        break;
                    case 2:
                        error = "Absolute load value: " + (random.Next(13700) + 12000) + "%";
                        break;
                    case 3:
                        error = "Ethanol fuel %: " + (random.Next(10) + random.Next(2) * 90);
                        break;
                    case 4:
                        error = "Intake air temperature: " + (random.Next(20) + 190);
                        break;
                    default:
                        error = "Run time since engine start: " + (random.Next(535) + 65000);
                        break;
                }
                request.Content = new StringContent(error);
                client.SendAsync(request);
            }
        }

        public CarInputModel Get(int userId, int carId)
        {
            // Gets Info from History Table
            return _manager.GetUserCarInfo(userId, carId);
        }
    }
}