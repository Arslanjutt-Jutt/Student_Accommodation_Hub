using Student_Accommodation_Hub.Constants;
using Student_Accommodation_Hub.DAL;
using Student_Accommodation_Hub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Student_Accommodation_Hub.Admin
{
    public partial class AddEditRoom : System.Web.UI.Page
    {
        public int roomId
        {
            get
            {
                if (ViewState["roomId"] != null)
                    return (int)ViewState["roomId"];
                return 0;
            }
            set
            {
                ViewState["roomId"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                roomId = Convert.ToInt32(Request.QueryString[AppConstants.QueryStringVariables.roomId]);
                PreparePage();
            }
        }
        public void PreparePage()
        {
            if (roomId > 0)
            {
                lblPageHeading.Text = "Manage Room";
                var room = Room.GetRoomById(roomId);
                txtRoomNumber.Text = room.RoomNumber;
                txtRoomRent.Text = room.RoomRent.ToString();
                txtSecurityDeposit.Text = room.SecurityDeposit.ToString();
                ddlBlockNo.SelectedValue = room.BlockNo;
                ddlRoomType.SelectedValue = room.RoomType;
                chkHasAC.Checked = room.HasAC;
                chkHasAttachedBathroom.Checked = room.HasAttachedBathroom;
                chkHasWifi.Checked = room.HasWiFi;
                btnAddRoom.Text = "Save";
            }

        }
        private RoomModel fillModel()
        {
            var room = new RoomModel();
            room.RoomNumber = txtRoomNumber.Text;
            room.RoomType = ddlRoomType.SelectedValue;
            room.BlockNo = ddlBlockNo.SelectedValue;
            room.RoomStatus = AppConstants.RoomStatus.Available.ToString();
            room.RoomRent = int.Parse(txtRoomRent.Text);
            room.SecurityDeposit = int.Parse(txtSecurityDeposit.Text);
            room.HasAttachedBathroom = chkHasAttachedBathroom.Checked;
            room.HasAC = chkHasAC.Checked;
            room.HasWiFi = chkHasWifi.Checked;

            return room;
        }
        private void clearFields()
        {
            // Clear TextBox controls
            txtRoomNumber.Text = string.Empty;
            txtRoomRent.Text = string.Empty;
            txtSecurityDeposit.Text = string.Empty;

            // Reset DropDownLists to default value
            ddlRoomType.SelectedValue = "-1";
            ddlBlockNo.SelectedValue = "-1";
           

            // Uncheck CheckBoxes
            chkHasAttachedBathroom.Checked = false;
            chkHasAC.Checked = false;
            chkHasWifi.Checked = false;
        }
        protected void btnAddRoom_Click(object sender, EventArgs e)
        {
            try
            {
                int result = 0;
                if (roomId > 0)
                {
                    var room = fillModel();
                    result = Room.AddNewRoom(room,roomId);
                    if (result == 1)
                    {
                        clearFields();
                        ShowMessage("Room has been updated successfully", "Message", false);

                    }
                }
                else
                {
                    var room = fillModel();
                     result = Room.AddNewRoom(room);
                    if (result == 1)
                    {
                        clearFields();
                        ShowMessage("Room has been created successfully", "Message", false);

                    }
                }
               
                if (result == -1)
                {
                    ShowMessage("Room Number is already exist please change room number", "Message",true);
                }

            }
            catch (Exception ex)
            {
                ShowMessage(ex.Message.ToString(), "Error",false);
            }
        }
        private void ShowMessage(string message, string title,bool isStayOnPage)
        {
            // Set the message and title of the dialog
            lblDialogMessage.Text = message;
            lblDialogTitle.Text = title;
            if (isStayOnPage)
            {
                btnOk.OnClientClick = "hidePopup(); return false;";
                btnClose.OnClientClick= "hidePopup(); return false;";
            }
            mpePopup.Show();
          
        }
        protected void btnCloseDialog_Click(object sender, EventArgs e)
        {
            mpePopup.Hide();
            Response.Redirect(AppConstants.CommonPath.ManageRoom, false);
        }

        protected void btnCencel_Click(object sender, EventArgs e)
        {
            Response.Redirect(AppConstants.CommonPath.ManageRoom, false);
        }
    }
}