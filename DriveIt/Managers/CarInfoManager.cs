using DriveIt.DTO;
using DriveIt.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DriveIt.Managers
{
    public class CarInfoManager
    {
        private static string _connectionString = "Server=driveit.database.windows.net;Database=driveit;Trusted_Connection=\'false\';User=DubHacks;Password=Glowacki123";
        
        public static string _getUserCarInfoById = @"SELECT Histories.PId, PidTable.Descriptions, PidTable.Units, Users.FirstName, Users.LastName, Users.Age, Cars.Make, Cars.Model, Cars.Year, Cars.IsHybrid, AVG(Histories.Value) AS Value FROM Histories, PidTable, Cars, Users WHERE PidTable.PId = Histories.PId AND Histories.UserId = Users.UserId AND Histories.UserId = @userId AND Histories.CarId = @carId AND Histories.CarId = 5 GROUP BY Histories.PId, PidTable.Descriptions, PidTable.Units, Users.FirstName, Users.LastName, Users.Age, Cars.Make, Cars.Model, Cars.Year, Cars.IsHybrid;";

        public CarInputModel GetUserCarInfo(int userId, int carId)
        {
            using (var conn = new SqlConnection())
            {
                conn.ConnectionString = _connectionString;
                conn.Open();
                var command = new SqlCommand(_getUserCarInfoById, conn);
                command.Parameters.Add(new SqlParameter("userId", userId));
                command.Parameters.Add(new SqlParameter("carId", carId));
                var result = new CarInputModel();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (result.UserModel == null)
                        {
                            result.UserModel = new UserModel()
                            {
                                FirstName = reader["FirstName"].ToString(),
                                LastName = reader["LastName"].ToString(),
                                Age = (int)reader["Age"],
                                CarMake = reader["Make"].ToString(),
                                CarModel = reader["Model"].ToString(),
                                CarYear = (int)reader["Year"]
                            };
                        }
                        result.PidModel.Add(reader["PId"].ToString(), new PidModel() {
                            Description = reader["Description"].ToString(),
                            Units = reader["Units"].ToString(),
                            Value = (double)reader["Value"]
                        });
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

        public static string _tryGetCarId = @"SELECT CarId FROM Cars WHERE Make=@make AND Model=@model AND Year=@year AND IsHybrid=@isHybrid";

        public static string _insertCarRow = @"INSERT INTO Cars (Make, Model, Year, IsHybrid) VALUES(@make,@model,@year,@isHybrid)";

        public int GetCarId(string make, string model, int year, bool isHybrid)
        {
            using (var conn = new SqlConnection())
            {
                conn.ConnectionString = _connectionString;
                conn.Open();
                var command = new SqlCommand(_getUserCarInfoById, conn);
                command.Parameters.Add(new SqlParameter("make", make));
                command.Parameters.Add(new SqlParameter("model", model));
                command.Parameters.Add(new SqlParameter("year", year));
                command.Parameters.Add(new SqlParameter("isHybrid", isHybrid ? 1 : 0));

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return (int)reader["CarId"];
                    }

                    var insertCommand = new SqlCommand(_getUserCarInfoById, conn);
                    insertCommand.Parameters.Add(new SqlParameter("make", make));
                    insertCommand.Parameters.Add(new SqlParameter("model", model));
                    insertCommand.Parameters.Add(new SqlParameter("year", year));
                    insertCommand.Parameters.Add(new SqlParameter("isHybrid", isHybrid ? 1 : 0));
                    insertCommand.ExecuteNonQuery();

                    using (SqlDataReader reader2 = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return (int)reader["CarId"];
                        }
                    }
                }

                return 3;
            }
        }
    }
}