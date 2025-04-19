using Student_Accommodation_Hub.DAL.SQL_Helper;
using Student_Accommodation_Hub.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace Student_Accommodation_Hub.DAL
{
    public class HostelRent
    {
        public static int UploadHostelRent(HostelRentModel rent, out int result)
        {
            try
            {
                SqlHelper sqlHelper = new SqlHelper();

                // Adding parameters
                sqlHelper.AddParameter("@MonthName", SqlDbType.NVarChar, rent.MonthName);
                sqlHelper.AddParameter("@Year", SqlDbType.Int, rent.Year);
                sqlHelper.AddParameter("@Status", SqlDbType.Int, rent.PaymentStatus);
                sqlHelper.AddParameter("@DueDate", SqlDbType.Date, rent.DueDate);
                sqlHelper.AddParameter("@Remarks", SqlDbType.NVarChar, rent.Remarks);

                // Output parameter
                SqlCommand command;
                sqlHelper.ExecuteStoredProcedure("sp_AddHostelRent", "@Output", out command);

                // Getting output result
                result = Convert.ToInt32(command.Parameters["@Output"].Value);

                return result; // Returning the stored procedure result
            }
            catch (Exception ex)
            {
                result = -1; // If an error occurs, return -1
                throw;
            }
        }
        public static List<HostelRentModel> GetHostelRentData(HostelRentModel filters, int pageSize, int currentPage, out int totalRecords)
        {
            totalRecords = 0; // Initialize total records
            List<HostelRentModel> rentList = new List<HostelRentModel>(); // List to store results
            SqlCommand command;

            try
            {
                SqlHelper sqlHelper = new SqlHelper(); // ✅ Custom SQL Helper

                // ✅ Add Filters as Stored Procedure Parameters
                sqlHelper.AddParameter("@MonthName", SqlDbType.NVarChar,filters.MonthName);
                sqlHelper.AddParameter("@Year", SqlDbType.Int,filters.Year==""?0:Convert.ToInt32(filters.Year) ) ;
                sqlHelper.AddParameter("@PaymentStatus", SqlDbType.Int,filters.PaymentStatus);
                sqlHelper.AddParameter("@StudentId", SqlDbType.Int, (filters.StudentId > 0) ? filters.StudentId : (object)DBNull.Value);

                // ✅ Pagination Parameters
                sqlHelper.AddParameter("@PageSize", SqlDbType.Int, pageSize);
                sqlHelper.AddParameter("@CurrentPage", SqlDbType.Int, currentPage);

                // ✅ Execute Stored Procedure
                using (SqlDataReader reader = sqlHelper.ExecuteStoredProcedure("spGetHostelRent", "@TotalRecords", out command))
                {
                    if (reader.HasRows && !reader.IsClosed)
                    {
                        while (reader.Read())
                        {
                            // **Call ReadFromReader Function**
                            HostelRentModel rent = HostelRentModel.MapHostelRent(reader);
                            rentList.Add(rent);
                        }
                    }
                }

                // ✅ Get Total Records from Output Parameter
                totalRecords = Convert.ToInt32(command.Parameters["@TotalRecords"].Value);
            }
            catch (Exception)
            {
                throw;
            }

            return rentList;
        }
        public static int UpdatePaymentStatusById(int id,int updateStatus)
        {
            try
            {
                SqlHelper sqlHelper = new SqlHelper();

                // Add parameters
                
                sqlHelper.AddParameter("@NewStatus", SqlDbType.Bit, updateStatus);
                sqlHelper.AddParameter("@RentId", SqlDbType.Int, id);


                // Define update query
                string query = @"UPDATE HostelRent SET PaymentStatus = @NewStatus WHERE RentId = @RentId";

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
        public static int UpdateHostelRentById(HostelRentModel model)
        {
            try
            {
                SqlHelper sqlHelper = new SqlHelper();

                // Add parameters

                sqlHelper.AddParameter("@TotalRent", SqlDbType.Decimal, model.TotalRent);
                sqlHelper.AddParameter("@RentId", SqlDbType.Int,model.RentId);
                sqlHelper.AddParameter("@DueDate", SqlDbType.Date, model.DueDate);
                sqlHelper.AddParameter("@Remarks", SqlDbType.NVarChar, model.Remarks);

                // Define update query
                string query = @"UPDATE HostelRent SET TotalRent = @TotalRent, DueDate = @DueDate, Remarks = @Remarks WHERE RentId = @RentId";

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