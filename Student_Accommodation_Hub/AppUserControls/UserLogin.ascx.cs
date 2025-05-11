using Student_Accommodation_Hub.AppUtilties;
using Student_Accommodation_Hub.Constants;
using Student_Accommodation_Hub.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using System.Diagnostics;
using AjaxControlToolkit.HtmlEditor.ToolbarButtons;
using Student_Accommodation_Hub.Models;

namespace Student_Accommodation_Hub.AppUserControls
{
    public partial class UserLogin : System.Web.UI.UserControl
    {
        public string OtpCode
        {
            get
            {
                if (ViewState["OtpCode"] != null)
                    return (string)ViewState["OtpCode"];
                return null;
            }
            set
            {
                ViewState["OtpCode"] = value;
            }
        }
        public string userGmail
        {
            get
            {
                if (ViewState["userGmail"] != null)
                    return (string)ViewState["userGmail"];
                return null;
            }
            set
            {
                ViewState["userGmail"] = value;
            }
        }
        public bool isSignUpMode
        {
            get
            {
                if (ViewState["isSignUpMode"] != null)
                    return (bool)ViewState["isSignUpMode"];
                return false;
            }
            set
            {
                ViewState["isSignUpMode"] = value;
            }
        }
        public bool isAdminLogin
        {
            get
            {
                if (ViewState["isAdminLogin"] != null)
                    return (bool)ViewState["isAdminLogin"];
                return false;
            }
            set
            {
                ViewState["isAdminLogin"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string currentPageUrl = Request.Url.AbsoluteUri;
                if (currentPageUrl.Contains("SignUp.aspx"))
                {
                    isSignUpMode= true; 
                }
                if(currentPageUrl.Contains("Admin"))
                {
                    isAdminLogin = true;
                }
                PreparePage();
            }
        }
        private void PreparePage()
        {
            if (isSignUpMode)
            {
                btnLogin.Visible= false;
                btnSignup.Visible = true;
                pnlPassword.Visible = false;
                pnlSignup.Visible = false;
                pnlBacktoLogin.Visible = true;
                pnlWelcomeMsg.Visible = false;
                lbtnForgotPassword.Visible = false;
                clearFields();
            }
            else if (isAdminLogin)
            {
                pnlSignup.Visible = false;
                pnlWelcomeMsg.Visible = true;
                lbtnForgotPassword.Visible = false;
                clearFields();
            }
            else
            {
                clearFields();
            }
        }
        private void clearFields()
        {
            txtEmail.Text = string.Empty;
            txtPassword.Text = string.Empty;
        }
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if (isAdminLogin)
            {
                try
                {
                    string password = EncryptionHelper.EncryptStringAES(txtPassword.Text);
                    //AdminLogin.updatepas();
                    var user = AdminLogin.GetAdminByGmailAndPassword(txtEmail.Text);
                    if (user != null)
                    {
                        

                        var isPasswordCorrect = txtPassword.Text == EncryptionHelper.DecryptStringAES(user.UserPassword);


                        if (isPasswordCorrect)
                        {
                            UserBaseControl.UserEmail = user.UserEmail;
                            UserBaseControl.UserName = user.UserName;
                            UserBaseControl.UserRole = AppConstants.UserRole.Admin;
                            UserBaseControl.UserEmail = user.UserEmail;
                            UserBaseControl.UserId = user.UserId;
                            UserBaseControl.UserRole =AppConstants.UserRole.Admin;
                            Response.Redirect(AppConstants.CommonPath.defaultPage);
                            lblError.Visible = false;

                        }
                        else
                        {
                            lblError.Visible = true;
                        }

                    }
                    else
                    {
                        
                        lblError.Visible = true;
                    }
                
                }
                catch
                {
                    lblError.Text = "Error occurred while Validate the Email/Password";
                    lblError.Visible = true;
                }
            }
            else
            {
                try
                {
                    var pass = EncryptionHelper.EncryptStringAES(txtPassword.Text);
                    var student = Student.GetStudentByEmail(txtEmail.Text);
                    if (student != null)
                    {

                        var isPasswordCorrect = txtPassword.Text == EncryptionHelper.DecryptStringAES(student.Password);

                        //string originolPass = EncryptionHelper.DecryptPassword(encryptPass);
                        if (isPasswordCorrect)
                        {
                            UserBaseControl.UserEmail = student.Email;
                            UserBaseControl.UserName = student.StudentName;
                            UserBaseControl.UserRole = AppConstants.UserRole.Student;
                            UserBaseControl.UserEmail = student.Email;
                            UserBaseControl.UserId = student.StudentID;
                            UserBaseControl.UserRole = AppConstants.UserRole.Student;
                            Response.Redirect(AppConstants.CommonPath.StudentDefaultPage,false);
                            lblError.Visible = false;

                        }
                        else
                        {
                            lblError.Visible = true;
                        }

                    }
                    else
                    {

                        lblError.Visible = true;
                    }

                }
                catch
                {

                    lblError.Text = "Error occurred while Validate the Email/Password";
                    lblError.Visible = true;
                }
            }

        }

        protected void btnSignup_Click(object sender, EventArgs e)
        {
            try
            {
                    var student = Student.GetStudentByEmail(txtEmail.Text);
                    if (student != null)
                    {
                        if (!student.IsSignUp)
                        {

                            string otpCode = GenerateOTP();

                            // Store OTP in session
                            OtpCode = otpCode;
                            userGmail = txtEmail.Text;
                        EmailSystemModel email = new EmailSystemModel
                        {
                            Body = $@"
                            Dear {{ToName}},

                            Thank you for registering with us.

                            Your One-Time Password (OTP) to complete your signup is: {{OTP}}

                            This code is valid for the next 30 minutes. Please do not share it with anyone.

                            If you did not initiate this request, you can safely ignore this email.

                            Best regards,  
                            Student Accommodation Hub Team",

                            Subject = "Verify Your Email – OTP Code for Signup",
                            ToEmail = student.Email,
                            ToName = student.StudentName,
                            OTP = otpCode
                        };


                        // Send OTP via email
                        bool isSent =EmailSystem.SendOTPEmail(email);

                            if (isSent)
                            {
                            mpeSignupPopup.Show();
                            }
                            else
                            {
                                lblError.Text = "Failed to send OTP. Please try again.";
                                lblError.ForeColor = System.Drawing.Color.Red;
                            }

                        }
                        else
                        {
                            lblError.Text = "You are already SignUp.";
                            lblError.Visible = true;
                        }
                    }
                    else
                    {
                        lblError.Text = "Invalid UserID/Email. Please enter a valid Email.";
                        lblError.Visible = true;
                    }
                
                
            }
            catch (Exception)
            {

                lblError.Text = "An error occurred.please try again.";
                lblError.Visible = true;
            }
        }

       
        private string GenerateOTP()
        {
            Random rand = new Random();
            return rand.Next(100000, 999999).ToString();
        }

        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                if (OtpCode == txtOtpCode.Text)
                {
                   var password = EncryptionHelper.EncryptStringAES(txtNewPassword.Text);
                   

                    int result = Student.SaveStudentPasswordByEmail(userGmail,password);
                    if (result == 1)
                    {
                        string msg = "";
                        if (!isSignUpMode)
                        {
                            msg = "Your password has been updated successfully.Please log in using your new password to continue.";
                        }
                        else
                        {
                            msg = "Your account has been created successfully.Please log in using your registered email and password to access your dashboard.";
                        }
                        
                        ScriptManager.RegisterStartupScript(this, GetType(), "ShowMessageBox", "ShowMessageBox('" + msg + "');", true);
                        mpeSignupPopup.Hide();
                    }
                }
            }
            catch
            {
                mpeSignupPopup.Hide();
                lblError.Text = "An error occurred.please try again.";
                lblError.Visible = true;
            }
        }

        protected void btnSendOTP_Click(object sender, EventArgs e)
        {
            try
            {
                var student = Student.GetStudentByEmail(txtForgotEmail.Text);
                if (student != null)
                {

                    string otpCode = GenerateOTP();

                    // Store OTP in session
                    OtpCode = otpCode;
                    userGmail = txtForgotEmail.Text;

                    // Send OTP via email
                    EmailSystemModel email = new EmailSystemModel
                    {
                        Body = @"Dear {ToName},

                        We received a request to reset your account password.

                        Your One-Time Password (OTP) to proceed with the password reset is: {OTP}

                        This code is valid for the next 10 minutes. Please do not share it with anyone.

                        If you did not request a password reset, please ignore this email or contact our support team.

                        Best regards,  
                        Student Accommodation Hub Team",

                        Subject = "Reset Your Password – OTP Code Inside",
                        ToEmail = student.Email,
                        ToName = student.StudentName,
                        OTP = otpCode
                    };


                    // Send OTP via email
                    bool isSent = EmailSystem.SendOTPEmail(email);

                    if (isSent)
                    {
                        mpeForgotPassword.Hide();
                        mpeSignupPopup.Show();
                        //ScriptManager.RegisterStartupScript(this, this.GetType(), "openSavePassPopup", "openSavePassPopup();", true);

                    }
                    else
                    {
                        lblError.Text = "Failed to send OTP. Please try again.";
                        lblError.ForeColor = System.Drawing.Color.Red;
                    }
                }
                else
                {
                    lblError.Text = "Invalid UserID/Email. Please enter a valid Email.";
                    lblError.Visible = true;
                }


            }
            catch (Exception)
            {

                lblError.Text = "An error occurred.please try again.";
                lblError.Visible = true;
            }
        }
    }
}