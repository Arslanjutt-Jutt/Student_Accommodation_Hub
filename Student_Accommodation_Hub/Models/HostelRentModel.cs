using Student_Accommodation_Hub.Constants;
using Student_Accommodation_Hub.DAL;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Student_Accommodation_Hub.Models
{
    public class HostelRentModel
    {
        public int RentId { get; set; } // Unique Rent ID (Primary Key)
        public int StudentId { get; set; } // Student ID (Foreign Key)
        public string StudentName { get; set; } // Student Name
        public int RoomId { get; set; } // Room ID (Foreign Key)
        public string RoomNo { get; set; } // Room Number
        public decimal TotalRent { get; set; } // Total Rent Amount
        public string MonthName { get; set; } // Month Name (e.g., January)
        public string Year { get; set; } // Year (2-digit format, e.g., "24")
        public int PaymentStatus { get; set; } // Rent Paid (true) or Pending (false)
        public string Remarks { get; set; } // Optional Remarks
        public DateTime DueDate { get; set; }

        public static HostelRentModel MapHostelRent(SqlDataReader reader)
        {
            HostelRentModel rent = new HostelRentModel();

            if (reader.HasRows) // Ensure there are rows before reading
            {
                rent.RentId = reader["RentId"] != DBNull.Value ? Convert.ToInt32(reader["RentId"]) : 0;
                rent.StudentId = reader["StudentId"] != DBNull.Value ? Convert.ToInt32(reader["StudentId"]) : 0;
                rent.RoomId = reader["RoomId"] != DBNull.Value ? Convert.ToInt32(reader["RoomId"]) : 0;
                rent.TotalRent = reader["TotalRent"] != DBNull.Value ? Convert.ToDecimal(reader["TotalRent"]) : 0;
                rent.MonthName = reader["MonthName"] != DBNull.Value ? reader["MonthName"].ToString() : string.Empty;
                rent.Year = reader["Year"].ToString();
                rent.PaymentStatus =Convert.ToInt32(reader["PaymentStatus"]);
                rent.Remarks = reader["Remarks"] != DBNull.Value ? reader["Remarks"].ToString() : string.Empty;
                rent.DueDate = reader["DueDate"] != DBNull.Value ? Convert.ToDateTime(reader["DueDate"]) : DateTime.MinValue;
            }

            return rent; // Return the fully filled object
        }
    }

    }