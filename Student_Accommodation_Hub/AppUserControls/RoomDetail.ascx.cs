using Student_Accommodation_Hub.AppUtilties;
using Student_Accommodation_Hub.Models;
using Student_Accommodation_Hub.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Student_Accommodation_Hub.AppUserControls
{
    public partial class RoomDetail : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData();
            }
        }
        public void LoadData()
        {
            int studentId = UserBaseControl.UserId;
            List<RoomatesDataModel> roommates = new List<RoomatesDataModel>();
            roommates= Student.GetRoomatesInfo(studentId);
            if(roommates.Count > 0)
            {
                rptRoommates.DataSource= roommates;
                rptRoommates.DataBind();
            }
        }
    }
}