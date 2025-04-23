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
    public class Room
    {
        public static int AddNewRoom(RoomModel room , int roomId=0)
        {
            int result = 0;
            try
            {
                SqlHelper sqlHelper = new SqlHelper();

                // Adding parameters to the SqlHelper
                sqlHelper.AddParameter("@RoomStatus", SqlDbType.VarChar, room.RoomStatus);
                sqlHelper.AddParameter("@RoomNumber", SqlDbType.VarChar, room.RoomNumber);
                sqlHelper.AddParameter("@RoomType", SqlDbType.VarChar, room.RoomType);
                sqlHelper.AddParameter("@BlockNo", SqlDbType.VarChar, room.BlockNo);
                sqlHelper.AddParameter("@RoomRent", SqlDbType.Decimal, room.RoomRent);
                sqlHelper.AddParameter("@SecurityDeposit", SqlDbType.Decimal, room.SecurityDeposit);
                sqlHelper.AddParameter("@HasAttachedBathroom", SqlDbType.Bit, room.HasAttachedBathroom);
                sqlHelper.AddParameter("@HasAC", SqlDbType.Bit, room.HasAC);
                sqlHelper.AddParameter("@HasWiFi", SqlDbType.Bit, room.HasWiFi);
                sqlHelper.AddParameter("@RoomId", SqlDbType.Int, roomId);
                SqlCommand command;

                // Execute the stored procedure and retrieve the result
                SqlDataReader reader = sqlHelper.ExecuteStoredProcedure("SP_AddNewRoom","@result", out command);
                return Convert.ToInt32(command.Parameters["@result"].Value);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            return result;
        }

        public static List<RoomModel> GetRoomsByBlock(string blockNo)
        {
            List<RoomModel> rooms = new List<RoomModel>();
            try
            {
                if (blockNo != null && !string.IsNullOrEmpty(blockNo))
                {
                    SqlHelper sqlHelper = new SqlHelper();

                    string query = "SELECT RoomId, RoomNumber FROM Rooms WHERE BlockNo = @BlockNo";

                    sqlHelper.AddParameter("@BlockNo", SqlDbType.VarChar, blockNo);

                    using (var reader = sqlHelper.ExecuteQuery(query))
                    {
                        while (reader.Read())
                        {
                            rooms.Add(new RoomModel
                            {
                                RoomId = Convert.ToInt32(reader["RoomId"]),
                                RoomNumber = reader["RoomNumber"].ToString()
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return rooms;
        }
        public static List<RoomModel> GetRooms()
        {
            List<RoomModel> rooms = new List<RoomModel>();
            try
            {
                
               
                    SqlHelper sqlHelper = new SqlHelper();

                    string query = "SELECT RoomId, RoomNumber FROM Rooms";

                    

                    using (var reader = sqlHelper.ExecuteQuery(query))
                    {
                    if (reader != null && reader.Read())
                    {
                        while (reader.Read())
                        {
                            rooms.Add(new RoomModel
                            {
                                RoomId = Convert.ToInt32(reader["RoomId"]),
                                RoomNumber = reader["RoomNumber"].ToString()
                            });
                        }
                    }
                    }
                
            }
            catch (Exception)
            {
                throw;
            }

            return rooms;
        }
        public static List<string> GetRoomNoByPrefix(string prefixName)
        {
            List<string> RoomNo = new List<string>();
            try
            {
                SqlHelper sqlHelper = new SqlHelper();

                sqlHelper.AddParameter("@Prefix", SqlDbType.Text, prefixName + "%");

                string query = "SELECT RoomNumber FROM Rooms WHERE RoomNumber LIKE @Prefix";
                using (SqlDataReader reader = sqlHelper.ExecuteQuery(query))
                {

                    while (reader.Read())
                    {
                        RoomNo.Add(reader["RoomNumber"].ToString());
                    }


                }
            }
            catch (Exception)
            {
                throw;
            }
            return RoomNo;
        }
        public static List<RoomModel> GetRooms(RoomModel roomFilters, int pageSize, int currentPage, out int totalRecords)
        {
            totalRecords = 0; // Initialize total records
            List<RoomModel> rooms = new List<RoomModel>(); // List to store rooms
            SqlCommand command;

            try
            {
                SqlHelper sqlHelper = new SqlHelper(); // ✅ Custom SQL Helper

                // ✅ Add Stored Procedure Parameters for Filters
                sqlHelper.AddParameter("@RoomNo", SqlDbType.NVarChar, roomFilters.RoomNumber);
                sqlHelper.AddParameter("@RoomType", SqlDbType.NVarChar, roomFilters.RoomType);
                sqlHelper.AddParameter("@BlockNo", SqlDbType.NVarChar, roomFilters.BlockNo);
               

                // ✅ Pagination Parameters
                sqlHelper.AddParameter("@PageSize", SqlDbType.Int, pageSize);
                sqlHelper.AddParameter("@CurrentPage", SqlDbType.Int, currentPage);

                // ✅ Execute Stored Procedure
                using (SqlDataReader reader = sqlHelper.ExecuteStoredProcedure("spGetRoomsWithStudentCount", "@TotalRecords", out command))
                {
                    if (reader.HasRows && !reader.IsClosed)
                    {
                        while (reader.Read())
                        {
                            // **Call ReadRoomFromReader Function**
                            RoomModel room = RoomModel.ReadRoomFromReader(reader);
                            rooms.Add(room);
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

            return rooms;
        }
        public static RoomModel GetRoomById(int roomId)
        {
            RoomModel room = new RoomModel();
            try
            {
                if (roomId != 0)
                {
                    SqlHelper sqlHelper = new SqlHelper();

                    string query = "SELECT * FROM Rooms WHERE RoomId = @RoomId";

                    sqlHelper.AddParameter("@RoomId", SqlDbType.Int, roomId);

                    using (var reader = sqlHelper.ExecuteQuery(query))
                    {
                        if (reader.Read() && reader!=null)
                        {
                            room = RoomModel.FillFromReader(reader);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return room;
        }

        public static RoomModel GetStudentRoomDetail(int StudentId)
        {
            RoomModel room = new RoomModel();
            try
            {
                if (StudentId != 0)
                {
                    SqlHelper sqlHelper = new SqlHelper();

                    string query = "SELECT r.RoomNumber, r.RoomType, r.BlockNo, r.RoomStatus, r.RoomRent, r.SecurityDeposit, r.HasAttachedBathroom, r.HasAC, r.HasWiFi, r.RoomId, r.createdDate FROM dbo.Students s INNER JOIN dbo.Rooms r ON s.RoomId = r.RoomId WHERE s.StudentID=@StudentID";

                    sqlHelper.AddParameter("@StudentId", SqlDbType.Int, StudentId);

                    using (var reader = sqlHelper.ExecuteQuery(query))
                    {
                        if (reader.Read() && reader != null)
                        {
                            room = RoomModel.FillFromReader(reader);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return room;
        }
    }
}