using Student_Accommodation_Hub.DAL.SQL_Helper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using Student_Accommodation_Hub.Models;

namespace Student_Accommodation_Hub.DAL
{
    public class Student
    {

        public static int SaveStudent(StudentDataModel student, int studenId = 0)
        {
            try
            {
                SqlHelper sqlHelper = new SqlHelper();
                sqlHelper.AddParameter("@StudentId", SqlDbType.Int, studenId);
                sqlHelper.AddParameter("@StudentName", SqlDbType.NVarChar, student.StudentName);
                sqlHelper.AddParameter("@CNIC", SqlDbType.NVarChar, student.CNIC);
                sqlHelper.AddParameter("@Gender", SqlDbType.NVarChar, student.Gender);
                sqlHelper.AddParameter("@Email", SqlDbType.NVarChar, student.Email);
                sqlHelper.AddParameter("@PhoneNumber", SqlDbType.NVarChar, student.PhoneNumber);
                sqlHelper.AddParameter("@Country", SqlDbType.NVarChar, student.CountryID.ToString());
                sqlHelper.AddParameter("@State", SqlDbType.NVarChar, student.StateID.ToString());
                sqlHelper.AddParameter("@Address", SqlDbType.NVarChar, student.Address);
                sqlHelper.AddParameter("@BlockNo", SqlDbType.NVarChar, student.BlockNo);
                sqlHelper.AddParameter("@RoomNumber", SqlDbType.Int, student.RoomId);
                sqlHelper.AddParameter("@HasSecurityDeposit", SqlDbType.Bit, student.HasSecurityDeposit);
                sqlHelper.AddParameter("@isActive", SqlDbType.Bit, student.isActive);
                sqlHelper.AddParameter("@Dob", SqlDbType.Date, student.Dob);

                /* sqlHelper.AddParameter("@result", SqlDbType.Int, DBNull.Value);*/ // Output parameter

                SqlCommand command;
                sqlHelper.ExecuteStoredProcedure("SaveStudentData", "@result", out command);

                return Convert.ToInt32(command.Parameters["@result"].Value); // Return the stored procedure result (-3, -2, -1, or 1)
            }
            catch (Exception ex)
            {
                throw;
            }



        }

        public static StudentDataModel GetStudentById(int studentId)
        {
            SqlHelper sqlHelper = new SqlHelper();
            sqlHelper.AddParameter("@StudentID", SqlDbType.Int, studentId);

            string query = "SELECT * FROM Students WHERE StudentID = @StudentID";
            using (SqlDataReader reader = sqlHelper.ExecuteQuery(query))
            {
                if (reader != null && reader.Read())
                {
                    return StudentDataModel.studentTblDataReader(reader);
                }
            }
            return null;
        }
        public static List<string> GetStudentNameByPrefix(string prefixName)
        {
            List<string> studentNames = new List<string>();
            try
            {
                SqlHelper sqlHelper = new SqlHelper();

                sqlHelper.AddParameter("@Prefix", SqlDbType.Text, prefixName + "%");

                string query = "SELECT StudentName FROM Students WHERE StudentName LIKE @Prefix";
                using (SqlDataReader reader = sqlHelper.ExecuteQuery(query))
                {

                    while (reader.Read())
                    {
                        studentNames.Add(reader["StudentName"].ToString());
                    }


                }
            }
            catch (Exception)
            {
                throw;
            }
            return studentNames;
        }


        public static List<StudentDataModel> GetStudents(StudentDataModel studentFilters, int pageSize, int pageNumber, out int totalRecords)
        {
            // ✅ SqlHelper Object
            totalRecords = 0;
            List<StudentDataModel> students = new List<StudentDataModel>();
            SqlCommand command;
            try
            {
                SqlHelper sqlHelper = new SqlHelper();
                // ✅ Add Stored Procedure Parameters
                sqlHelper.AddParameter("@StudentName", SqlDbType.NVarChar, studentFilters.StudentName);
                sqlHelper.AddParameter("@CNIC", SqlDbType.NVarChar, studentFilters.CNIC);
                sqlHelper.AddParameter("@Gender", SqlDbType.NVarChar, studentFilters.Gender);
                sqlHelper.AddParameter("@Email", SqlDbType.NVarChar, studentFilters.Email);
                sqlHelper.AddParameter("@PhoneNumber", SqlDbType.NVarChar, studentFilters.PhoneNumber);
                sqlHelper.AddParameter("@CountryID", SqlDbType.Int, studentFilters.CountryID);
                sqlHelper.AddParameter("@StateID", SqlDbType.Int, studentFilters.StateID);
                sqlHelper.AddParameter("@BlockNo", SqlDbType.NVarChar, studentFilters.BlockNo);
                sqlHelper.AddParameter("@RoomID", SqlDbType.Int, studentFilters.RoomId);
                sqlHelper.AddParameter("@HasSecurityDeposit", SqlDbType.Bit, studentFilters.HasSecurityDeposit);

                sqlHelper.AddParameter("@PageSize", SqlDbType.Int, pageSize);
                sqlHelper.AddParameter("@CurrentPage", SqlDbType.Int, pageNumber);


                //string debugQuery = $@"
                //EXEC spGetStudents 
                //@StudentName = {(studentFilters.StudentName == null ? "NULL" : $"'{studentFilters.StudentName}'")},  
                //@CNIC = {(studentFilters.CNIC == null ? "NULL" : $"'{studentFilters.CNIC}'")},  
                //@Gender = {(studentFilters.Gender == null ? "NULL" : $"'{studentFilters.Gender}'")},  
                //@Email = {(studentFilters.Email == null ? "NULL" : $"'{studentFilters.Email}'")},  
                //@PhoneNumber = {(studentFilters.PhoneNumber == null ? "NULL" : $"'{studentFilters.PhoneNumber}'")},  
                //@CountryID = {(studentFilters.CountryID == null ? "NULL" : studentFilters.CountryID.ToString())},  
                //@StateID = {(studentFilters.StateID == null ? "NULL" : studentFilters.StateID.ToString())},  
                //@BlockNo = {(studentFilters.BlockNo == null ? "NULL" : $"'{studentFilters.BlockNo}'")},  
                //@RoomID = {(studentFilters.RoomId == null ? "NULL" : studentFilters.RoomId.ToString())},  
                //@HasSecurityDeposit = {(studentFilters.HasSecurityDeposit == null ? "NULL" : studentFilters.HasSecurityDeposit.ToString())},  
                //@PageSize = {pageSize},  
                //@CurrentPage = {pageNumber},  
                //@TotalRecords = {totalRecords} OUTPUT;";
                //Console.WriteLine(debugQuery);  // **Console پر Print**
                //System.Diagnostics.Debug.WriteLine(debugQuery);





                using (SqlDataReader reader = sqlHelper.ExecuteStoredProcedure("spGetStudents", "@TotalRecords", out command))
                {
                    if (reader.HasRows && !reader.IsClosed) {
                        while (reader.Read())
                        {

                            students.Add(StudentDataModel.FromDataReader(reader));

                        }
                    }

                }

                totalRecords = Convert.ToInt32(command.Parameters["@TotalRecords"].Value);


            }
            catch (Exception)
            {
                throw;
            }
            return students;
        }
        public static int DeleteStudentRecord(int studentID)
        {
            try
            {
                SqlHelper sqlHelper = new SqlHelper();

                // Add the parameter for StudentID
                sqlHelper.AddParameter("@StudentID", SqlDbType.Int, studentID);

                // Define the query to delete the student record
                string query = "DELETE FROM Students WHERE StudentID = @StudentID";

                // Execute the query and get the number of rows affected
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
            catch (Exception)
            {
                throw;
            }
        }
        public static StudentDataModel GetStudentByEmail(string email)
        {
            SqlHelper sqlHelper = new SqlHelper();
            sqlHelper.AddParameter("@Email", SqlDbType.NVarChar, email);

            string query = "SELECT * FROM Students WHERE Email = @Email";
            using (SqlDataReader reader = sqlHelper.ExecuteQuery(query))
            {
                if (reader != null && reader.Read())
                {
                    return StudentDataModel.studentTblDataReader(reader);
                }
            }
            return null;
        }
        public static int SaveStudentPasswordByEmail(string email, string password)
        {
            try
            {
                SqlHelper sqlHelper = new SqlHelper();
                sqlHelper.AddParameter("@Email", SqlDbType.NVarChar, email);
                sqlHelper.AddParameter("@Password", SqlDbType.NVarChar, password);
                string query = "update Students set Password=@Password WHERE Email = @Email";
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
        public static List<RoomatesDataModel> GetRoomatesInfo(int studentId)
        {
            List<RoomatesDataModel> Roomates = new List<RoomatesDataModel>();
            try
            {
                SqlHelper sqlHelper = new SqlHelper();

                sqlHelper.AddParameter("@StudentId", SqlDbType.Int, studentId);

                string query = "SELECT s.StudentName,s.Email, s.PhoneNumber FROM  Students s " +
                    "WHERE s.RoomId = ( SELECT RoomId  FROM Students  WHERE StudentId = @StudentId) AND s.StudentId <> @StudentId";
                using (SqlDataReader reader = sqlHelper.ExecuteQuery(query))
                {

                    while (reader.Read())
                    {
                        Roomates.Add(RoomatesDataModel.FromDataReader(reader));
                    }


                }
            }
            catch (Exception)
            {
                throw;
            }
            return Roomates;
        }

    }
}