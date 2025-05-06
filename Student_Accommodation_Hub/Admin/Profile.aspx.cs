using Student_Accommodation_Hub.AppUtilties;
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
    public partial class Profile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
                txtEmail.Enabled = false;
                LoadData();
            }

        }
        private void LoadData()
        {
            try
            {
                if (UserBaseControl.UserId > 0)
                {
                    var user = AdminLogin.GetAdminByGmailAndPassword(UserBaseControl.UserEmail);
                    if (user != null)
                    {
                        txtName.Text = user.UserName;
                        txtEmail.Text = user.UserEmail;
                        txtPhone.Text = user.PhoneNo;
                        txtCNIC.Text = user.CNIC;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                var user = new UserLoginModel();
                user.UserName = txtName.Text;
                user.PhoneNo = txtPhone.Text;
                user.UserId = Convert.ToInt32(UserBaseControl.UserId);
                user.CNIC = txtCNIC.Text;
                int result = AdminLogin.SaveAdminProfileDataById(user);
                if (result == 1)
                {

                    ShowMessage("Changes saved successfully", "Message", false);
                }
            }
            catch (Exception)
            {

                ShowMessage("An error occurred.please try again.", "Error", true); ;
            }
        }
        private void ShowMessage(string message, string title, bool isStayOnPage)
        {
            // Set the message and title of the dialog
            lblDialogMessage.Text = message;
            lblDialogTitle.Text = title;
            if (isStayOnPage)
            {
                btnOk.OnClientClick = "hidePopup(); return false;";
                btnClose.OnClientClick = "hidePopup(); return false;";
            }
            mpePopup.Show();

        }

        protected void btnOk_Click(object sender, EventArgs e)
        {
            Response.Redirect(AppConstants.CommonPath.defaultPage);
        }
    }
}