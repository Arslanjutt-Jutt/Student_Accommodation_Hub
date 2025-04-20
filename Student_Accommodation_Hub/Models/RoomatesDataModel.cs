using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Student_Accommodation_Hub.Models
{
    public class RoomatesDataModel
    {
        public string StudentName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        public static RoomatesDataModel FromDataReader(SqlDataReader reader)
        {
            return new RoomatesDataModel
            {
                StudentName = reader["StudentName"].ToString(), 
                Email = reader["Email"].ToString(),
                PhoneNumber = reader["PhoneNumber"].ToString(),
            };
        }
    }
}