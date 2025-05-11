using Student_Accommodation_Hub.DAL.SQL_Helper;
using Student_Accommodation_Hub.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using Student_Accommodation_Hub.Constants;

namespace Student_Accommodation_Hub.DAL
{
    public class MessBill
    {
        public static int InsertMessBill(MessBillModel bill)
        {
            int result;
            try
            {
                SqlHelper sqlHelper = new SqlHelper();
                // Adding parameters
                sqlHelper.AddParameter("@MonthName", SqlDbType.NVarChar, bill.Month);
                sqlHelper.AddParameter("@Year", SqlDbType.Int, bill.Year);
                sqlHelper.AddParameter("@DueDate", SqlDbType.DateTime, bill.DueDate);
                sqlHelper.AddParameter("@BillAmount", SqlDbType.Decimal, bill.TotalBill);
                sqlHelper.AddParameter("@Remarks", SqlDbType.NVarChar, bill.Remarks);

                // Output parameter
                SqlCommand command;
                sqlHelper.ExecuteStoredProcedure("InsertMessBill", "@Output", out command);

                // Getting output result
                result = Convert.ToInt32(command.Parameters["@Output"].Value);

                return result; // Returning the stored procedure result (1 or -1)
            }
            catch (Exception ex)
            {
                result = -1; // If an error occurs, return -1
                throw;
            }
        }
        public static List<MessBillModel> GetMessBillData(MessBillModel filters, int pageSize, int currentPage, out int totalRecords)
        {
            totalRecords = 0; // Initialize total records
            List<MessBillModel> rentList = new List<MessBillModel>(); // List to store results
            SqlCommand command;

            try
            {
                SqlHelper sqlHelper = new SqlHelper(); // ✅ Custom SQL Helper

                // ✅ Add Filters as Stored Procedure Parameters
                sqlHelper.AddParameter("@MonthName", SqlDbType.NVarChar, filters.Month);
                sqlHelper.AddParameter("@Year", SqlDbType.Int, filters.Year);
                sqlHelper.AddParameter("@PaymentStatus", SqlDbType.Int, filters.PaymentStatus);
                sqlHelper.AddParameter("@StudentId", SqlDbType.Int, (filters.StudentId > 0) ? filters.StudentId : (object)DBNull.Value);

                // ✅ Pagination Parameters
                sqlHelper.AddParameter("@PageSize", SqlDbType.Int, pageSize);
                sqlHelper.AddParameter("@CurrentPage", SqlDbType.Int, currentPage);

                // ✅ Execute Stored Procedure
                using (SqlDataReader reader = sqlHelper.ExecuteStoredProcedure("spGetMessBill", "@TotalRecords", out command))
                {
                    if (reader.HasRows && !reader.IsClosed)
                    {
                        while (reader.Read())
                        {
                            // **Call ReadFromReader Function**
                            MessBillModel bill = MessBillModel.FromDataReader(reader);
                            rentList.Add(bill);
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
        public static int UpdatePaymentStatusById(int id, int updateStatus)
        {
            try
            {
                SqlHelper sqlHelper = new SqlHelper();

                // Add parameters

                sqlHelper.AddParameter("@NewStatus", SqlDbType.Bit, updateStatus);
                sqlHelper.AddParameter("@MessBill", SqlDbType.Int, id);


                // Define update query
                string query = @"UPDATE MessBill SET PaymentStatus = @NewStatus WHERE BillId = @MessBill";

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
        public static int UpdateMessBillById(MessBillModel model)
        {
            try
            {
                SqlHelper sqlHelper = new SqlHelper();

                // Add parameters

                sqlHelper.AddParameter("@TotalBill", SqlDbType.Decimal, model.TotalBill);
                sqlHelper.AddParameter("@BillId", SqlDbType.Int, model.BillId);
                sqlHelper.AddParameter("@DueDate", SqlDbType.Date, model.DueDate);
                sqlHelper.AddParameter("@Remarks", SqlDbType.NVarChar, model.Remarks);

                // Define update query
                string query = @"UPDATE MessBill SET TotalBill = @TotalBill, DueDate = @DueDate, Remarks = @Remarks WHERE BillId = @BillId";

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

        public static List<MessBlockRequestModel> GetMessBlockRequests(MessBlockRequestModel filters, int pageSize, int currentPage, out int totalRecords)
        {
            totalRecords = 0; // Initialize total records
            List<MessBlockRequestModel> blockMessRequests = new List<MessBlockRequestModel>(); // List to store results
            SqlCommand command;

            try
            {
                SqlHelper sqlHelper = new SqlHelper(); // ✅ Custom SQL Helper

                // ✅ Add Filters as Stored Procedure Parameters
                sqlHelper.AddParameter("@Month", SqlDbType.NVarChar, filters.Month);
                sqlHelper.AddParameter("@Year", SqlDbType.Int, filters.Year);
                sqlHelper.AddParameter("@Status", SqlDbType.NVarChar, filters.Status);
                sqlHelper.AddParameter("@StudentName", SqlDbType.NVarChar, filters.StudentName);

                // ✅ Pagination Parameters
                sqlHelper.AddParameter("@PageSize", SqlDbType.Int, pageSize);
                sqlHelper.AddParameter("@CurrentPage", SqlDbType.Int, currentPage);

                // ✅ Execute Stored Procedure
                using (SqlDataReader reader = sqlHelper.ExecuteStoredProcedure("spGetMessBlockRequests", "@TotalRecords", out command))
                {
                    if (reader.HasRows && !reader.IsClosed)
                    {
                        while (reader.Read())
                        {
                            // **Call ReadFromReader Function**
                            MessBlockRequestModel bill = MessBlockRequestModel.MapMessBlockRequest(reader);
                            blockMessRequests.Add(bill);
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

            return blockMessRequests;
        }
        public static List<MessBlockRequestModel> GetMessBlockRequestsByStudent(MessBlockRequestModel filters, int pageSize, int currentPage, out int totalRecords)
        {
            totalRecords = 0; // Initialize total records
            List<MessBlockRequestModel> blockMessRequests = new List<MessBlockRequestModel>(); // List to store results
            SqlCommand command;

            try
            {
                SqlHelper sqlHelper = new SqlHelper(); // ✅ Custom SQL Helper

                // ✅ Add Filters as Stored Procedure Parameters
                sqlHelper.AddParameter("@Month", SqlDbType.NVarChar, filters.Month);
                sqlHelper.AddParameter("@Year", SqlDbType.Int, filters.Year);
                sqlHelper.AddParameter("@Status", SqlDbType.NVarChar, filters.Status);
                sqlHelper.AddParameter("@StudentId", SqlDbType.Int, filters.StudentId);

                // ✅ Pagination Parameters
                sqlHelper.AddParameter("@PageSize", SqlDbType.Int, pageSize);
                sqlHelper.AddParameter("@CurrentPage", SqlDbType.Int, currentPage);

                // ✅ Execute Stored Procedure
                using (SqlDataReader reader = sqlHelper.ExecuteStoredProcedure("spGetMessBlockRequestsByStudent", "@TotalRecords", out command))
                {
                    if (reader.HasRows && !reader.IsClosed)
                    {
                        while (reader.Read())
                        {
                            // **Call ReadFromReader Function**
                            MessBlockRequestModel medel = MessBlockRequestModel.MapMessBlockRequestsByStudent(reader);
                            blockMessRequests.Add(medel);
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

            return blockMessRequests;
        }
        public static int SaveStudentMessBlockRequest(MessBlockRequestModel model)
        {
            try
            {
                SqlHelper sqlHelper = new SqlHelper();

                // Add parameters

                sqlHelper.AddParameter("@StudentId", SqlDbType.Int, model.StudentId);
                sqlHelper.AddParameter("@Month", SqlDbType.NVarChar, model.Month);
                sqlHelper.AddParameter("@Year", SqlDbType.Int, model.Year);
                sqlHelper.AddParameter("@StartDate", SqlDbType.Date, model.StartDate);
                sqlHelper.AddParameter("@EndDate", SqlDbType.Date, model.EndDate);
                sqlHelper.AddParameter("@Reason", SqlDbType.NVarChar, model.Reason);
                sqlHelper.AddParameter("@Status", SqlDbType.NVarChar, model.Status); // Default on insert
                sqlHelper.AddParameter("@RequestedDate", SqlDbType.DateTime, model.RequestedDate);

                // Define update query
                string query = @"INSERT INTO MessBlockRequests (StudentId, Month, Year, StartDate,
                                EndDate, Reason, Status, RequestedDate)
                                VALUES (@StudentId, @Month, @Year, @StartDate, @EndDate, @Reason, @Status, @RequestedDate);";

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
        public static int RejectMessBlockRequest(int RequestId)
        {
            try
            {
                SqlHelper sqlHelper = new SqlHelper();

                // Add parameters

                sqlHelper.AddParameter("@ApprovedDate", SqlDbType.DateTime, DateTime.Now);
                sqlHelper.AddParameter("@Status", SqlDbType.NVarChar,AppConstants.MessBlockRequestStatus.Rejected);
                sqlHelper.AddParameter("@RequestId", SqlDbType.Int, RequestId);
                

                // Define update query
                string query = @"UPDATE MessBlockRequests SET Status = @Status,
                                ApprovedDate = @ApprovedDate 
                                WHERE RequestId = @RequestId";
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

        public static int ApproveMessBlockRequest(int RequestId)
        {
            try
            {
                SqlHelper sqlHelper = new SqlHelper();

                // Add parameters

                sqlHelper.AddParameter("@ApprovedDate", SqlDbType.DateTime, DateTime.Now);
                sqlHelper.AddParameter("@Status", SqlDbType.NVarChar, AppConstants.MessBlockRequestStatus.Approved);
                sqlHelper.AddParameter("@RequestId", SqlDbType.Int, RequestId);


                // Define update query
                string query = @"UPDATE MessBlockRequests SET Status = @Status,
                                ApprovedDate = @ApprovedDate 
                                WHERE RequestId = @RequestId";
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
        public static int deletePendingMessBlockRequestByStudent(int RequestId)
        {
            try
            {
                SqlHelper sqlHelper = new SqlHelper();

                sqlHelper.AddParameter("@RequestId", SqlDbType.Int, RequestId);


                // Define update query
                string query = @"delete MessBlockRequests where RequestId = @RequestId";
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