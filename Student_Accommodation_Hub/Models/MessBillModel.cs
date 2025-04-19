using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Student_Accommodation_Hub.Models
{
    public class MessBillModel
    {
        public int BillId { get; set; }
        public int StudentId { get; set; }
        public string Month { get; set; }
        public int Year { get; set; }
        public decimal TotalBill { get; set; }
        public int BlockedDays { get; set; }
        public DateTime GeneratedDate { get; set; }
        public DateTime DueDate { get; set; }
        public string Remarks { get; set; }
        public int PaymentStatus { get; set; }

        // Function to populate model from SqlDataReader
        public static MessBillModel FromDataReader(SqlDataReader reader)
        {
            MessBillModel bill = new MessBillModel();

            bill.BillId = Convert.ToInt32(reader["BillId"]);
            bill.StudentId = Convert.ToInt32(reader["StudentId"]);
            bill.Month = reader["Month"].ToString();
            bill.Year = Convert.ToInt32(reader["Year"]);
            bill.TotalBill = Convert.ToDecimal(reader["TotalBill"]);
            bill.BlockedDays = Convert.ToInt32(reader["BlockedDays"]);
            bill.GeneratedDate = reader.GetDateTime(reader.GetOrdinal("GeneratedDate"));
            bill.DueDate = reader.GetDateTime(reader.GetOrdinal("DueDate"));
            bill.Remarks = reader["Remarks"].ToString();
            bill.PaymentStatus = Convert.ToInt32(reader["PaymentStatus"]);

            return bill;
        }
    }
}