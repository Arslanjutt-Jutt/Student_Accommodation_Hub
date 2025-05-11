using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Student_Accommodation_Hub.Models
{
    public class MessBlockRequestModel
    {
        public int RequestId { get; set; }
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public string CNIC { get; set; }
        public string Email { get; set; }
        public string Month { get; set; }
        public string Reason { get; set; }
        public int Year { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Status { get; set; }
        public DateTime? RequestedDate { get; set; }
        public DateTime? ApprovedDate { get; set; }

        // Static method to map from SqlDataReader
        public static MessBlockRequestModel MapMessBlockRequest(SqlDataReader reader)
        {
            MessBlockRequestModel request = new MessBlockRequestModel();

            if (reader.HasRows) // Ensure there are rows before reading
            {
                request.RequestId = reader["RequestId"] != DBNull.Value ? Convert.ToInt32(reader["RequestId"]) : 0;
                request.StudentId = reader["StudentId"] != DBNull.Value ? Convert.ToInt32(reader["StudentId"]) : 0;
                request.StudentName = reader["StudentName"] != DBNull.Value ? reader["StudentName"].ToString() : string.Empty;
                request.CNIC = reader["CNIC"] != DBNull.Value ? reader["CNIC"].ToString() : string.Empty;
                request.Email = reader["Email"] != DBNull.Value ? reader["Email"].ToString() : string.Empty;
                request.Month = reader["Month"] != DBNull.Value ? reader["Month"].ToString() : string.Empty;
                request.Reason = reader["Reason"] != DBNull.Value ? reader["Reason"].ToString() : string.Empty;

                request.Year = reader["Year"] != DBNull.Value ? Convert.ToInt32(reader["Year"]) : 0;
                request.StartDate = reader["StartDate"] != DBNull.Value ? Convert.ToDateTime(reader["StartDate"]) : (DateTime?)null;
                request.EndDate = reader["EndDate"] != DBNull.Value ? Convert.ToDateTime(reader["EndDate"]) : (DateTime?)null;
                request.Status = reader["Status"] != DBNull.Value ? reader["Status"].ToString() : string.Empty;
                request.RequestedDate = reader["RequestedDate"] != DBNull.Value ? Convert.ToDateTime(reader["RequestedDate"]) : (DateTime?)null;
                request.ApprovedDate = reader["ApprovedDate"] != DBNull.Value ? Convert.ToDateTime(reader["ApprovedDate"]) : (DateTime?)null;
            }

            return request; // Return the fully filled object
        }
        public static MessBlockRequestModel MapMessBlockRequestsByStudent(SqlDataReader reader)
        {
            MessBlockRequestModel request = new MessBlockRequestModel();

            if (reader.HasRows) // Ensure there are rows before reading
            {
                request.RequestId = reader["RequestId"] != DBNull.Value ? Convert.ToInt32(reader["RequestId"]) : 0;
                request.Month = reader["Month"] != DBNull.Value ? reader["Month"].ToString() : string.Empty;
                request.Year = reader["Year"] != DBNull.Value ? Convert.ToInt32(reader["Year"]) : 0;
                request.StartDate = reader["StartDate"] != DBNull.Value ? Convert.ToDateTime(reader["StartDate"]) : (DateTime?)null;
                request.EndDate = reader["EndDate"] != DBNull.Value ? Convert.ToDateTime(reader["EndDate"]) : (DateTime?)null;
                request.Status = reader["Status"] != DBNull.Value ? reader["Status"].ToString() : string.Empty;
                request.RequestedDate = reader["RequestedDate"] != DBNull.Value ? Convert.ToDateTime(reader["RequestedDate"]) : (DateTime?)null;
                request.ApprovedDate = reader["ApprovedDate"] != DBNull.Value ? Convert.ToDateTime(reader["ApprovedDate"]) : (DateTime?)null;
                request.Reason = reader["Reason"] != DBNull.Value ? reader["Reason"].ToString() : string.Empty;
            }

            return request; // Return the fully filled object
        }
    }

}