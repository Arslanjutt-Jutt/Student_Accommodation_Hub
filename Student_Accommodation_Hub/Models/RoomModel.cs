using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Student_Accommodation_Hub.Models
{
    public class RoomModel
    {
        public DateTime createdDate { get; set; }
        public int RoomId { get; set; }
        public string RoomNumber { get; set; }
        public string RoomType { get; set; }
        public string BlockNo { get; set; }
        public string RoomStatus { get; set; }
        public int RoomRent { get; set; }
        public int SecurityDeposit { get; set; }
        public bool HasAttachedBathroom { get; set; }
        public bool HasAC { get; set; }
        public bool HasWiFi { get; set; }
        public int StudentCount { get; set; }
        public DateTime CreatedDate { get; set; }

        // Method to fill the properties from the SqlDataReader object
        public static RoomModel FillFromReader(SqlDataReader reader)
        {
            RoomModel room=new RoomModel();
            if (reader != null)
            {

                room.RoomId = reader.GetInt32(reader.GetOrdinal("RoomId"));
                room.createdDate = reader.GetDateTime(reader.GetOrdinal("createdDate"));
                room.RoomNumber = reader.GetString(reader.GetOrdinal("RoomNumber"));
                room.RoomType = reader.GetString(reader.GetOrdinal("RoomType"));
                room.BlockNo = reader.GetString(reader.GetOrdinal("BlockNo"));
                room.RoomStatus = reader.GetString(reader.GetOrdinal("RoomStatus"));
                room.RoomRent = Convert.ToInt32(reader.GetDecimal(reader.GetOrdinal("RoomRent")));
                room.SecurityDeposit = Convert.ToInt32(reader.GetDecimal(reader.GetOrdinal("SecurityDeposit")));
                room.HasAttachedBathroom = reader.GetBoolean(reader.GetOrdinal("HasAttachedBathroom"));
                room.HasAC = reader.GetBoolean(reader.GetOrdinal("HasAC"));
                room.HasWiFi = reader.GetBoolean(reader.GetOrdinal("HasWiFi"));
            }
            return room;
        }
        public static RoomModel ReadRoomFromReader(SqlDataReader reader)
        {
            RoomModel room = new RoomModel();

            if (reader!=null)
            {
                room.CreatedDate = reader.GetDateTime(reader.GetOrdinal("CreatedDate"));
                room.RoomId = reader.GetInt32(reader.GetOrdinal("RoomID"));
                room.RoomNumber = reader["RoomNumber"] as string ?? string.Empty;
                room.RoomType = reader["RoomType"] as string ?? string.Empty;
                room.BlockNo = reader["BlockNo"] as string ?? string.Empty;
                room.RoomStatus = reader["RoomStatus"] as string ?? string.Empty;
                room.RoomRent = Convert.ToInt32(reader.GetDecimal(reader.GetOrdinal("RoomRent")));
                room.SecurityDeposit = Convert.ToInt32(reader.GetDecimal(reader.GetOrdinal("SecurityDeposit")));
                room.HasAttachedBathroom = reader.GetBoolean(reader.GetOrdinal("HasAttachedBathroom"));
                room.HasAC = reader.GetBoolean(reader.GetOrdinal("HasAC"));
                room.HasWiFi = reader.GetBoolean(reader.GetOrdinal("HasWiFi"));
                room.StudentCount = reader.GetInt32(reader.GetOrdinal("StudentCount"));
            }

            return room; // آخر میں فل شدہ آبجیکٹ ریٹرن کریں
        }
    }
}