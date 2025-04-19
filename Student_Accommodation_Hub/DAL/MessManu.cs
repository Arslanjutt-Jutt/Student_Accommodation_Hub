using Student_Accommodation_Hub.DAL.SQL_Helper;
using Student_Accommodation_Hub.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Student_Accommodation_Hub.DAL
{
    public class MessManu
    {
        public static List<MessMenuModel> GetWeeklyMessMenu()
        {
            List<MessMenuModel> menuList = new List<MessMenuModel>();

            try
            {
                SqlHelper sqlHelper = new SqlHelper();
                string query = "SELECT * FROM MessMenu ORDER BY ID";

                using (var reader = sqlHelper.ExecuteQuery(query))
                {
                    while (reader.Read())
                    {
                        // Using FromDataReader() to populate the object
                        MessMenuModel menu = MessMenuModel.FromDataReader(reader);
                        menuList.Add(menu);
                    }
                }
            }
            catch (Exception ex)
            {
                throw; // Handle logging if necessary
            }

            return menuList;
        }
        public static int UpdateMessMenu(MessMenuModel messMenu)
        {
            try
            {
                SqlHelper sqlHelper = new SqlHelper();

                // Add parameters
                sqlHelper.AddParameter("@ID", SqlDbType.Int, messMenu.ID);
                sqlHelper.AddParameter("@DayOfWeek", SqlDbType.NVarChar, messMenu.DayOfWeek);
                sqlHelper.AddParameter("@Breakfast", SqlDbType.NVarChar, messMenu.Breakfast);
                sqlHelper.AddParameter("@Lunch", SqlDbType.NVarChar, messMenu.Lunch);
                sqlHelper.AddParameter("@Dinner", SqlDbType.NVarChar, messMenu.Dinner);
               

                // Define update query
                string query = @"UPDATE MessMenu 
                         SET DayOfWeek = @DayOfWeek, 
                             Breakfast = @Breakfast, 
                             Lunch = @Lunch, 
                             Dinner = @Dinner 
                         WHERE ID = @ID";

                // Execute the query
                int rowsAffected = sqlHelper.ExecuteNonQuery(query);

                // Check if any rows were updated
                return rowsAffected > 0 ? 1 : -1;
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}