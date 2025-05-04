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
                    var user = AdminLogin.GetAdminByGmailAndPassword(txtEmail.Text, password);
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
            var student = Student.GetStudentByEmail(txtEmail.Text);
            if (student != null)
            {
                if (student.Password == null || student.Password.Length == 0)
                {

                    string otpCode = GenerateOTP();

                    // Store OTP in session
                    OtpCode = otpCode;
                    userGmail = txtEmail.Text;

                    // Send OTP via email
                    bool isSent = SendOTPEmail(txtEmail.Text.Trim(), otpCode);

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

        private bool SendOTPEmail(string email, string otpCode)
        {
            try
            {
                string senderEmail = "jarslanjutt570@gmail.com"; // Replace with your email
                string senderPassword = "mderhdjbnkcmdwxd"; // Use secure methods to store credentials
                string smtpServer = "smtp.gmail.com"; // Replace with your SMTP server
                int smtpPort = 587; // Use the appropriate port (e.g., 587 for TLS)

                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(senderEmail);
                mail.To.Add(email);
                mail.Subject = "Your OTP Code for Signup";
                mail.Body = $"Your OTP code is: {otpCode}. Please do not share this code with anyone.";
                mail.IsBodyHtml = false;

                SmtpClient smtpClient = new SmtpClient(smtpServer, smtpPort)
                {
                    Credentials = new NetworkCredential(senderEmail, senderPassword),
                    EnableSsl = true
                };

                smtpClient.Send(mail);
                return true;
            }
            catch 
            {
                
                return false;
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
                        
                        ScriptManager.RegisterStartupScript(this, GetType(), "ShowMessageBox", "ShowMessageBox();", true);
                        var student = Student.GetStudentByEmail(userGmail);
                        UserBaseControl.UserEmail=student.Email;
                        UserBaseControl.UserRole = AppConstants.UserRole.Student;
                        UserBaseControl.UserName = student.StudentName;
                        UserBaseControl.UserId = student.StudentID;                      
                        UserBaseControl.UserRole = AppConstants.UserRole.Student;
                    }
                }
            }
            catch
            {

            }
        }
    }
}