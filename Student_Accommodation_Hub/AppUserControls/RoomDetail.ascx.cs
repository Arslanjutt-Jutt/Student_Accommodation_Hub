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
            if (studentId > 0)
            {
                List<RoomatesDataModel> roommates = new List<RoomatesDataModel>();
                roommates = Student.GetRoomatesInfo(studentId);
                if (roommates.Count > 0)
                {
                    rptRoommates.DataSource = roommates;
                    rptRoommates.DataBind();
                }

                RoomModel roomDetail= Room.GetStudentRoomDetail(studentId);

                if (roomDetail != null)
                {
                    lblRoomNo.Text = roomDetail.RoomNumber;
                    lblBlockNo.Text = roomDetail.BlockNo;
                    lblRoomStatus.Text = roomDetail.RoomStatus;
                    lblSecurityValue.Text = roomDetail.SecurityDeposit.ToString();
                    lblRoomRent.Text = roomDetail.RoomRent.ToString();
                    lblRoomType.Text = roomDetail.RoomType;

                    lblWifiValue.Text = roomDetail.HasWiFi ? "Yes" : "No";
                    lblACValue.Text = roomDetail.HasAC ? "Yes" : "No";
                    lblBathroomValue.Text = roomDetail.HasAttachedBathroom ? "Yes" : "No";
                    
                }
            }
        }
    }
}