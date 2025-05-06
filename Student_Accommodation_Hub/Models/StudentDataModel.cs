using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Student_Accommodation_Hub.Models
{
    public class StudentDataModel
    {
        public int StudentID { get; set; }
        public string StudentName { get; set; }
        public string CNIC { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Country { get; set; }
        public int? CountryID { get; set; }
        public string State { get; set; }
        public int? StateID { get; set; }
        public string Address { get; set; }
        public string BlockNo { get; set; }
        public int? RoomId { get; set; }
        public string RoomNo { get; set; }
        public string Password { get; set; }
        public bool HasSecurityDeposit { get; set; }
        public bool isActive { get; set; }
        public bool IsSignUp { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime Dob {get; set; }
        public DateTime UpdatedDate { get; set; }

        public static StudentDataModel FromDataReader(SqlDataReader reader)
        {

            return new StudentDataModel
            {
                StudentID = Convert.ToInt32(reader["StudentID"]),
                StudentName = reader["StudentName"].ToString(),
                CNIC = reader["CNIC"].ToString(),
                Gender = reader["Gender"].ToString(),
                Email = reader["Email"].ToString(),
                PhoneNumber = reader["PhoneNumber"].ToString(),
                Country = reader["CountryName"].ToString(),
                State = reader["StateName"].ToString(),
                Address = reader["Address"].ToString(),
                BlockNo = reader["BlockNo"].ToString(),
                RoomNo = reader["RoomNumber"].ToString(),
                UpdatedDate = reader["UpdatedDate"] != DBNull.Value ? Convert.ToDateTime(reader["Dob"]) : DateTime.MinValue,
                HasSecurityDeposit = Convert.ToBoolean(reader["HasSecurityDeposit"]),
                Dob = (DateTime)reader["Dob"],/*!= DBNull.Value ? Convert.ToDateTime(reader["Dob"]) : (DateTime?)null),*/
                CreatedDate = reader["CreatedDate"] != DBNull.Value ? Convert.ToDateTime(reader["CreatedDate"]) : DateTime.MinValue,
               
            };
        }
        public static StudentDataModel studentTblDataReader(SqlDataReader reader)
        {
            try
            {
                var student = new StudentDataModel();

                student.StudentID = Convert.ToInt32(reader["StudentID"]);
                student.StudentName = reader["StudentName"].ToString();
                student.CNIC = reader["CNIC"].ToString();
                student.Gender = reader["Gender"].ToString();
                student.Email = reader["Email"].ToString();
                student.PhoneNumber = reader["PhoneNumber"].ToString();
                student.Address = reader["Address"].ToString();
                student.BlockNo = reader["BlockNo"].ToString();
                student.Password = reader["Password"].ToString();
                student.RoomId = Convert.ToInt32(reader["RoomId"]);
                student.UpdatedDate = reader["UpdatedDate"] != DBNull.Value ? Convert.ToDateTime(reader["Dob"]) : DateTime.MinValue;
                student.HasSecurityDeposit = Convert.ToBoolean(reader["HasSecurityDeposit"]);
                student.isActive = Convert.ToBoolean(reader["isActive"]);
                student.IsSignUp = Convert.ToBoolean(reader["IsSignUp"]);
                student.Dob = (DateTime)reader["Dob"];/*!= DBNull.Value ? Convert.ToDateTime(reader["Dob"]) : (DateTime?)null),*/
                student.CreatedDate = reader["CreatedDate"] != DBNull.Value ? Convert.ToDateTime(reader["CreatedDate"]) : DateTime.MinValue;
                student.CountryID = Convert.ToInt32(reader["Country"]);
                student.StateID = Convert.ToInt32(reader["State"]);
                return student;
            }
            catch (Exception)
            {

                throw;
            }
        }

    }

}