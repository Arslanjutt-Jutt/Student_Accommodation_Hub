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
        public static UserLoginModel GetAdminByGmailAndPassword(string gmail)
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
                        UserPassword = reader["Password"].ToString(),
                        CNIC = reader["CNIC"].ToString(),
                        PhoneNo = reader["PhoneNo"].ToString()
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
        public static int SaveAdminProfileDataById(UserLoginModel admin)
        {
            try
            {
                SqlHelper sqlHelper = new SqlHelper();
                //sqlHelper.AddParameter("@Email", SqlDbType.NVarChar, admin.UserEmail);
                sqlHelper.AddParameter("@StudentName", SqlDbType.NVarChar, admin.UserName);
                sqlHelper.AddParameter("@CNIC", SqlDbType.NVarChar, admin.CNIC);
                sqlHelper.AddParameter("@PhoneNumber", SqlDbType.NVarChar, admin.PhoneNo);
                sqlHelper.AddParameter("@Id", SqlDbType.Int, admin.UserId);
                string query = "update Admins set Name=@StudentName , CNIC=@CNIC, PhoneNo=@PhoneNumber where Id=@Id";
                int rowsAffected = sqlHelper.ExecuteNonQuery(query);

                // Check if any rows were affected (i.e., if the record was deleted)
                if (rowsAffected > 0)
                {
                    return 1; // Deletion successful
                }
                else
                {
                    return -1; // No record was deleted (StudentID might not exist)

                }
            }
            catch
            {
                throw;
            }

        }

    }
}