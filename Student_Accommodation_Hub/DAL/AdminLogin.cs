using Student_Accommodation_Hub.DAL.SQL_Helper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using Student_Accommodation_Hub.AppUserControls;
using Student_Accommodation_Hub.Models;
using Student_Accommodation_Hub.AppUtilties;

namespace Student_Accommodation_Hub.DAL
{
    public class AdminLogin
    {
        public static UserLoginModel GetAdminByGmailAndPassword(string gmail, string password)
        {
            SqlHelper sqlHelper = new SqlHelper();
            sqlHelper.AddParameter("@Gmail", SqlDbType.NVarChar, gmail);
            //sqlHelper.AddParameter("@Password", SqlDbType.NVarChar, password);

            string query = "SELECT * FROM Admins WHERE Gmail = @Gmail";

            using (SqlDataReader reader = sqlHelper.ExecuteQuery(query))
            {
                if (reader != null && reader.Read())
                {
                    UserLoginModel userLogin = new UserLoginModel
                    {
                        UserId = Convert.ToInt32(reader["Id"]),
                        UserName = reader["Name"].ToString(),
                        UserEmail = reader["Gmail"].ToString(),
                        UserPassword = reader["Password"].ToString()
                    };
                    return userLogin;
                }
            }
            return null;
        }
        public static void  updatepas()
        {
            string hashedPassword = PasswordHelper.HashPassword("admin123");
            string query = "UPDATE Admins SET Password = @Password WHERE Gmail = @Gmail";
            SqlHelper sqlHelper = new SqlHelper();
            sqlHelper.AddParameter("@Password", SqlDbType.NVarChar, hashedPassword);
            sqlHelper.AddParameter("@Gmail", SqlDbType.NVarChar, "admin@gmail.com");
            sqlHelper.ExecuteNonQuery(query);

        }

    }
}