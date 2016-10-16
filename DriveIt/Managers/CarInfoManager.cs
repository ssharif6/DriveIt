using DriveIt.DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DriveIt.Managers
{
    public class CarInfoManager
    {
        private static string _connectionString = "Server=driveit.database.windows.net;Database=driveit;Trusted_Connection=\'false\';User=DubHacks;Password=Glowacki123";

        public static string _getUserCarInfoById = @"SELECT * FROM Histories WHERE UserId = @userId AND CarId = @carId";

        public GetCarInfoDto GetUserCarInfo(int userId, int carId)
        {
            using (var conn = new SqlConnection())
            {
                conn.ConnectionString = _connectionString;
                conn.Open();
                var command = new SqlCommand(_getUserCarInfoById, conn);
                command.Parameters.Add(new SqlParameter("userId", userId));
                command.Parameters.Add(new SqlParameter("carId", carId));
                var result = new GetCarInfoDto();
                //int pHistoryId;
                //int pUserId;
                //int pCarId;
                //string pPid;
                //double pValue;

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine(reader[0]);
                        result.HistoryId = (int)reader[0];
                        result.UserId = (int) reader[1];
                        result.CarId = (int) reader[2];
                        result.PId = (string) reader[3];
                        result.Value = (double) reader[4];
                    }
                }

                // read Sql
                return result;
            }
        }

        public static string _insertHistoryRow = @"INSERT INTO Histories (CarId,PId,UserId,Value) VALUES(@pCarId,@pPId,@pUserId,@pValue)";

        public void PostData(int userId, int carId, double value, string pId)
        {
            // Query PID Table, detect if isUrgent, if it is, Push notify
            {
                using (var conn = new SqlConnection())
                {
                    conn.ConnectionString = _connectionString;
                    conn.Open();
                    var command = new SqlCommand(_insertHistoryRow, conn);
                    command.Parameters.Add(new SqlParameter("pCarId", carId));
                    command.Parameters.Add(new SqlParameter("pPId", pId));
                    command.Parameters.Add(new SqlParameter("pUserId", userId));
                    command.Parameters.Add(new SqlParameter("pValue", value));
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}