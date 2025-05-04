using Student_Accommodation_Hub.AppUtilties;
using Student_Accommodation_Hub.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Student_Accommodation_Hub.AppUserControls
{
    public partial class ChangePassword : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnChangePassword_Click(object sender, EventArgs e)
        {
            try
            {
                int studentId = UserBaseControl.UserId;
                if (studentId > 0)
                {
                    var student = Student.GetStudentById(studentId);
                    string oldPass = EncryptionHelper.DecryptStringAES(student.Password);
                    if (oldPass == txtOldPassword.Text)
                    {
                        var encryptPass = EncryptionHelper.EncryptStringAES(txtNewPassword.Text);
                        int result = Student.ChangeStudentPasswordById(studentId, encryptPass);
                        if (result == 1)
                        {
                            mpeChangePassword.Hide();
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "showSuccessChangePasswordMessage", "showSuccessChangePasswordMessage();", true);

                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Old password is incorrect.');", true);
                        mpeChangePassword.Show();
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}