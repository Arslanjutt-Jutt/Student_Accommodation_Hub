using Student_Accommodation_Hub.Constants;
using Student_Accommodation_Hub.DAL;
using Student_Accommodation_Hub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Student_Accommodation_Hub.Admin
{

    public partial class AddEditStudent : System.Web.UI.Page
    {
        public int studentId
        {
            get
            {
                if (ViewState["studentId"] != null)
                    return (int)ViewState["studentId"];
                return 0;
            }
            set
            {
                ViewState["studentId"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString[AppConstants.QueryStringVariables.studentId]))
                {
                    studentId = Convert.ToInt32(Request.QueryString[AppConstants.QueryStringVariables.studentId]);
                }
                PreparePage();
            }

        }
        private void LoadCountries()
        {
           
            List<CountryModel> countries = WebUtilty.GetAllCountries();

            ddlCountry.DataSource = countries;
            ddlCountry.DataTextField = "CountryName";
            ddlCountry.DataValueField = "CountryID";
            ddlCountry.DataBind();
            ddlCountry.SelectedValue = "1";
           
        }
        public void PreparePage()
        {
            try { 
            
            LoadCountries();
                lblRegisterStudent.Text = "Register New Student";
                btnRegister.CommandName = "Register";
                if (studentId > 0)
            {
                StudentDataModel student = Student_Accommodation_Hub.DAL.Student.GetStudentById(studentId);
                ddlCountry.SelectedValue = student.CountryID.ToString();
                cddState.SelectedValue = student.StateID.ToString();
                ddlGender.SelectedValue = student.Gender;
                ddlBlockNo.SelectedValue = student.BlockNo;
                cddBlockNo.SelectedValue = student.RoomId.ToString();
                txtStudentName.Text = student.StudentName;
                txtPhoneNumber.Text = student.PhoneNumber;
                txtEmail.Text = student.Email;
                txtDob.Text = student.Dob.ToString("yyyy-MM-dd");
                txtCnic.Text = student.CNIC;
                txtAddress.Text = student.Address;
                chkSecurityDeposit.Checked = student.HasSecurityDeposit;
                    chkIsActive.Checked = student.isActive;
                btnRegister.Text = "Save";
                btnRegister.CommandName = "Update";
                lblRegisterStudent.Text = "Manage Student";

                }
              
            }
            catch(Exception ex)
            {
                ShowMessage(ex.Message, "Error", true);
            }
           
        }
        private StudentDataModel FillStudentModel()
        {
            var student = new StudentDataModel();

            student.StudentName = txtStudentName.Text.Trim();
            student.CNIC = txtCnic.Text.Trim();
            student.Gender = ddlGender.SelectedValue;
            student.Email = txtEmail.Text.Trim();
            student.PhoneNumber = txtPhoneNumber.Text.Trim();
            student.CountryID = Convert.ToInt32(ddlCountry.SelectedValue);
            student.StateID = Convert.ToInt32(ddlState.SelectedValue);
            student.Address = txtAddress.Text.Trim();
            student.BlockNo = ddlBlockNo.SelectedValue;
            student.RoomId = Convert.ToInt32(ddlRoomNumber.SelectedValue);
            student.HasSecurityDeposit = chkSecurityDeposit.Checked;
            student.isActive = chkIsActive.Checked;
            student.CreatedDate = DateTime.Now;
            student.Dob = Convert.ToDateTime(txtDob.Text);

            return student;
        }
        private void ClearFormFields()
        {
            txtStudentName.Text = string.Empty;
            txtCnic.Text = string.Empty;
            ddlGender.SelectedIndex = 0;  // Set to default value
            txtEmail.Text = string.Empty;
            txtPhoneNumber.Text = string.Empty;
            ddlCountry.SelectedIndex = 0;  // Set to default value
            ddlState.SelectedIndex = 0;    // Set to default value
            txtAddress.Text = string.Empty;
            ddlBlockNo.SelectedIndex = 0;  // Set to default value
            ddlRoomNumber.SelectedIndex = 0;  // Set to default value
            chkSecurityDeposit.Checked = false;
            txtDob.Text = string.Empty;
        }
        protected void btnRegister_Click(object sender, EventArgs e)
        {
            try
            {
                var btn = (Button)sender;
                if (btn.CommandName == "Register")
                {

                    StudentDataModel student = FillStudentModel();
                    int result = Student_Accommodation_Hub.DAL.Student.SaveStudent(student);
                    if (result == 1)
                    {
                        ClearFormFields();
                        ShowMessage("Student register successfully.", "Message", false);
                    }
                    if (result == -1)
                    {
                        ShowMessage("Email already exists", "Message", true);
                    }
                    if (result == -2)
                    {
                        ShowMessage("CNIC already exists", "Message", true);
                    }
                    if (result == -3)
                    {
                        ShowMessage("Email and CNIC already exists", "Message", true);
                    }
                    if (result == -4)
                    {
                        ShowMessage("This room has been full.Please change Room Number", "Message", true);
                    }
                }
                else if (btn.CommandName == "Update")
                {
                    StudentDataModel student = FillStudentModel();
                    int result = Student_Accommodation_Hub.DAL.Student.SaveStudent(student,studentId);
                    if (result == 1)
                    {
                        ClearFormFields();
                        ShowMessage("Student updated successfully.", "Message", false);
                    }
                    if (result == -1)
                    {
                        ShowMessage("Email already exists", "Message", true);
                    }
                    if (result == -2)
                    {
                        ShowMessage("CNIC already exists", "Message", true);
                    }
                    if (result == -3)
                    {
                        ShowMessage("Email and CNIC already exists", "Message", true);
                    }
                    if (result == -4)
                    {
                        ShowMessage("This room has been full.Please change Room Number", "Message", true);
                    }
                }
            }
            catch(Exception ex) 
                {
                ShowMessage(ex.Message, "Error", true);
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
        protected void btnCloseDialog_Click(object sender, EventArgs e)
        {
            mpePopup.Hide();
            Response.Redirect(AppConstants.CommonPath.defaultPage, false);
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(AppConstants.CommonPath.defaultPage, false);
        }
    }
}