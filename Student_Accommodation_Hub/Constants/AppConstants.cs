using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Student_Accommodation_Hub.Constants
{
    public static class AppConstants
    {
        public enum RoomStatus
        {
            Available,   // 0
            Occupied,    // 1
            Maintenance, // 2
            Reserved     // 3
        }
        public static class CommonPath
        {
            public static string AddEditStudentPage = "AddEditStudent.aspx";
            public static string defaultPage = "default.aspx";
            public static string ManageRoom = "ManageRoom.aspx";
            public static string AddEditRoom = "AddEditRoom.aspx";
            public static string ManageRent = "ManageRent.aspx";
            public static string ManageMessBill = "ManageMessBill.aspx";
            public static string AdminLogin = "~/Admin/Login.aspx";
            public static string StudentDefaultPage = "~/Students/default.aspx";
            public static string StudentLogin = "~/Students/Login.aspx";
            public static string MessManu = "~/Students/MessManu.aspx";
            public static string HostelRent = "~/Students/HostelRent.aspx";
            public static string MessBill = "~/Students/MessBill.aspx";
        }
        public static class QueryStringVariables
        {
            public static string studentId = "studentId";
            public static string roomId = "roomId";
        }
        public enum RoomRentStatus
        {
            paid=1,   // 0
            pending=0,
            All=-1// 1
               
        }
       public static class UserRole
        {
            public static string Admin = "Admin";
            public static string Student = "Student";
        }
        public static class MessBlockRequestStatus
        {
            public static string Approved = "Approved";
            public static string Pending = "Pending";
            public static string Rejected = "Rejected";
        }
    }
}