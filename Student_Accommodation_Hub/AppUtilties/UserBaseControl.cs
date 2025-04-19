using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Student_Accommodation_Hub.AppUtilties
{
    public class UserBaseControl
    {
        public static int UserId
        {
            get { return HttpContext.Current.Session["UserId"] != null ? (int)HttpContext.Current.Session["UserId"] : 0; }
            set { HttpContext.Current.Session["UserId"] = value; }
        }

        public static string UserName
        {
            get { return HttpContext.Current.Session["UserName"] as string; }
            set { HttpContext.Current.Session["UserName"] = value; }
        }

        public static string UserEmail
        {
            get { return HttpContext.Current.Session["UserEmail"] as string; }
            set { HttpContext.Current.Session["UserEmail"] = value; }
        }

        public static string UserRole
        {
            get { return HttpContext.Current.Session["UserRole"] as string; }
            set { HttpContext.Current.Session["UserRole"] = value; }
        }

        // Check if a user is authenticated
        public static bool IsAuthenticated
        {
            get { return HttpContext.Current.Session["UserId"] != null; }
        }

        // Clear session when user logs out
        public static void Logout()
        {
            HttpContext.Current.Session.Clear();
            HttpContext.Current.Session.Abandon();
        }
    }
}