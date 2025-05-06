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

namespace Student_Accommodation_Hub.Students
{
    public partial class Profile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData();
            }
        }
        private void LoadData()
        {
            try
            {
                var student = Student.GetStudentById(UserBaseControl.UserId);
                if (student != null)
                {

                    txtName.Text = student.StudentName;
                    txtEmail.Text = student.Email;
                    txtCNIC.Text = student.CNIC;
                    txtPhone.Text = student.PhoneNumber;
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
                if (UserBaseControl.UserId > 0)
                {
                    var student = new StudentDataModel();
                    student.StudentName = txtName.Text;
                    student.Email = txtEmail.Text;
                    student.CNIC = txtCNIC.Text;
                    student.PhoneNumber = txtPhone.Text;
                    student.StudentID = UserBaseControl.UserId;
                    int result = Student.SaveStudentProfileDataById(student);
                    if (result == 1)
                    {
                       ScriptManager.RegisterStartupScript(this, this.GetType(), "showSuccess", "showSuccessMessage();", true);
                        
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