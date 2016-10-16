using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DriveIt.Managers
{
    public class CarInfoManager
    {
        private static string _connectionString = "Server=driveit.database.windows.net;Database=driveit;Trusted_Connection=true";

        public static string _getUserCarInfoById = @"SELECT * FROM Histories WHERE UserId = @userId AND CarId = @carId";
        public void GetUserCarInfo(int userId, int carId)
        {
            var conn = withSqlConnection(_connectionString);
            SqlCommand command = new SqlCommand(_getUserCarInfoById, conn);
            command.Parameters.Add(new SqlParameter("userId", userId));
            command.Parameters.Add(new SqlParameter("carId", carId));

            // read Sql
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Console.WriteLine(reader[0]);
                }
            }

        }

        private SqlConnection withSqlConnection(string connectionName)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = connectionName;
                conn.Open();
                return conn;
            }
        }

        private SqlDataReader readSql(SqlCommand command)
        {
            return null;
        }
    }
}