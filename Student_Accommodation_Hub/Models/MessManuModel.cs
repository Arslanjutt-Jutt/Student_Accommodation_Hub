using Student_Accommodation_Hub.DAL;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Student_Accommodation_Hub.Models
{
    public class MessMenuModel
    {
        public int ID { get; set; }
        public string DayOfWeek { get; set; }
        public string Breakfast { get; set; }
        public string Lunch { get; set; }
        public string Dinner { get; set; }
        public DateTime CreatedAt { get; set; }

        // Function to populate model from SqlDataReader
        public static MessMenuModel FromDataReader(SqlDataReader reader)
        {
            MessMenuModel menu = new MessMenuModel();

            menu.ID = Convert.ToInt32(reader["ID"]);
            menu.DayOfWeek = reader["DayOfWeek"].ToString();
            menu.Breakfast = reader["Breakfast"].ToString();
            menu.Lunch = reader["Lunch"].ToString();
            menu.Dinner = reader["Dinner"].ToString();
            menu.CreatedAt = reader.GetDateTime(reader.GetOrdinal("CreatedAt")); 

            return menu;
        }
    }
}