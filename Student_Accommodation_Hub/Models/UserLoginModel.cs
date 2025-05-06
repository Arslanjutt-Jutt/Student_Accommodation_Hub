using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Student_Accommodation_Hub.Models
{
    public class UserLoginModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string CNIC { get; set; }
        public string PhoneNo { get; set; }
        public string UserRole { get; set; }
        public string UserPassword { get; set; }
    }
}