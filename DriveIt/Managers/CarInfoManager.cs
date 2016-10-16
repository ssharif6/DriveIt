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

        public static string _getUserCarInfoById = @"SELECT h1.PId, PidTable.Descriptions, PidTable.Units, Users.FirstName, Users.LastName, Users.Age, Cars.Make, Cars.Model, Cars.Year, Cars.IsHybrid, AVG(h1.Value) AS Value,
	(SELECT AVG(h2.Value) AS NationalValue 
	FROM Histories h2 
	WHERE h1.PId = h2.PId AND h2.CarId = @carId
	GROUP BY h2.PId) AS NationalValue
FROM Histories h1, PidTable, Cars, Users 
WHERE PidTable.PId = h1.PId AND h1.UserId = Users.UserId AND h1.UserId = @userId AND h1.CarId = @carId 
GROUP BY h1.PId, PidTable.Descriptions, PidTable.Units, Users.FirstName, Users.LastName, Users.Age, Cars.Make, Cars.Model, Cars.Year, Cars.IsHybrid;";

        public int AddCarid(int carId)
        {
            using (var conn = new SqlConnection())
            {
                conn.ConnectionString = _connectionString;
                conn.Open();
                var command = new SqlCommand(@"INSERT INTO CarIdTable (CarId) VALUES (@carId)", conn);
                command.Parameters.Add(new SqlParameter("carId", carId));
                command.ExecuteNonQuery();
            }

            return carId;
        }

        public int GetMostRecentCarId()
        {
            using (var conn = new SqlConnection())
            {
                conn.ConnectionString = _connectionString;
                conn.Open();
                var command = new SqlCommand(@"SELECT CarId FROM CarIdTable ORDER BY Id Desc", conn);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return (int)reader["CarId"];
                    }
                }

                return 5;
            }
        }

        public string GetPIdDesciption(string pid, double value)
        {
            using (var conn = new SqlConnection())
            {
                conn.ConnectionString = _connectionString;
                conn.Open();
                var command = new SqlCommand(@"SELECT Descriptions, Units FROM PidTable Where PId = @pid", conn);
                command.Parameters.Add(new SqlParameter("pid", pid));

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return string.Format("{0}: {1}{2}", reader["Descriptions"].ToString(), value, reader["Units"]);
                    }
                }

                return string.Empty;
            }
        }

        public CarInputModel GetUserCarInfo(int userId, int carId)
        {
            using (var conn = new SqlConnection())
            {
                var privSet = new HashSet<string>();
                conn.ConnectionString = _connectionString;
                conn.Open();
                var command = new SqlCommand(_getUserCarInfoById, conn);
                command.Parameters.Add(new SqlParameter("userId", userId));
                command.Parameters.Add(new SqlParameter("carId", carId));
                var result = new CarInputModel()
                {
                    PidModel = new List<PidModel>()
                };

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
                                CarYear = reader["Year"].ToString()
                            };
                        }

                        if (!privSet.Contains(reader["PId"].ToString()))
                        {
                            result.PidModel.Add(new PidModel()
                            {
                                Description = reader["Descriptions"].ToString(),
                                Pid = reader["PId"].ToString(),
                                Units = reader["Units"].ToString(),
                                Value = Math.Round((double)reader["Value"], 2),
                                NationalValue = Math.Round((double)reader["NationalValue"], 2)
                            });

                            privSet.Add(reader["PId"].ToString());
                        }
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
                var command = new SqlCommand(_tryGetCarId, conn);
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
                }
            }

            using (var conn = new SqlConnection())
            {
                conn.ConnectionString = _connectionString;
                conn.Open();
                var insertCommand = new SqlCommand(_insertCarRow, conn);
                insertCommand.Parameters.Add(new SqlParameter("make", make));
                insertCommand.Parameters.Add(new SqlParameter("model", model));
                insertCommand.Parameters.Add(new SqlParameter("year", year));
                insertCommand.Parameters.Add(new SqlParameter("isHybrid", isHybrid ? 1 : 0));
                insertCommand.ExecuteNonQuery();
            }

            using (var conn = new SqlConnection())
            {
                conn.ConnectionString = _connectionString;
                conn.Open();
                var command = new SqlCommand(_tryGetCarId, conn);
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
                }
            }

            return 3;
        }
    }
}