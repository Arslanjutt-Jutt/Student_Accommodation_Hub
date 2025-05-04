<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserLogin.ascx.cs" Inherits="Student_Accommodation_Hub.AppUserControls.UserLogin" %>
<%@ Register Src="~/Admin/UserControls/footerSection.ascx" TagPrefix="uc2" TagName="footerSection" %>
<style>
    body {
        margin: 0px;
        padding: 0px;
        font-size: 12px !important;
        font-family: Verdana, Helvetica, sans-serif
    }
    .title-container img {
        height:130px;
        width:250px;
        display:block
    }
    .title-container span{
        color:#b3b3b3;
        font-size:14px;
         font-weight:300;
    }
    .title-container a{
        color:#29377b;
        font-weight:bold;
        font-size:14px;
        text-decoration:none
    }
    .inner-container {
        width: 600px;
        height: 350px;
        background-color: gray;
        border:1px solid #A9A9A9
    }
    .login-fields{
        width:270px;
        border:1px solid #7f9db9
    }
    .tbl-login {
        border-collapse: separate;
        border-spacing: 0 10px;
    }
    .btn{
        width:80px;
        height:25px;
        padding:0px
    }
    .welcome-message{
        font-weight:bold;
        font-size:14px;
    }
    .error-message
    {
        color:white;
        font-weight:bold
    }
</style>
<asp:ScriptManager ID="ScriptManager1" runat="server" />
<table style="width:950px;height:400px;margin:auto;margin-top:60px;background-color:#DCDEFB">
    <tr>
        <td align="center">
           <div class="inner-container">
               <table style="width:100%;height:50%;" cellpadding="0" cellspacing="0">
                   <tr>
                       <td align="center" style="background-color: white">
                           <div class="title-container">
                               <asp:Image runat="server" ID="imgBrandLogo" ImageUrl="~/ImageIcons/BrandLogo.svg" />
                               <asp:Panel ID="pnlSignup" runat="server">
                               <asp:Label runat="server" ID="lblSignup">Don't have an account ? <a href="../Students/SignUp.aspx">Register Here !</a></asp:Label>
                               </asp:Panel>
                               <asp:Panel ID="pnlBacktoLogin" runat="server" Visible="false">
                                   <asp:Label runat="server" ID="lblBackLogin">
                                     Back to Login: 
                                  <a href="../Students/Login.aspx">
                                       <i class="fa fa-arrow-left"></i>
                                       Login                                 
                                </a>
                                   </asp:Label>
                               </asp:Panel>
                               <asp:Panel ID="pnlWelcomeMsg" CssClass="welcome-message" runat="server" Visible="false" >
                                   Welcome to Admin Portal.
                               </asp:Panel>
        
                               </div>
                       </td>
                   </tr>
                   
               </table>
               <table class="tbl-login" style="margin-top:10px">
                   <tr>
                       <td>
                           &nbsp;
                       </td>
                       <td>
                        <asp:Label ID="lblError" CssClass="error-message" runat="server" Visible="false" Text="Invalid UserID/Email or Password"></asp:Label>
                       </td>
                   </tr>
                   <tr>
                       <td style="text-align: right">
                           <asp:Label ID="lblEmail" Style="font-weight: bold" runat="server" Text="User ID/Email: "></asp:Label>
                       </td>
                       <td style="text-align:left">
                           <asp:TextBox ID="txtEmail" CssClass="login-fields" runat="server"></asp:TextBox>
                       </td>
                   </tr>
                   <tr>
                       <asp:Panel ID="pnlPassword" runat="server">
                           <td style="text-align: right">
                               <asp:Label ID="lblPassword" Style="font-weight: bold" runat="server" Text="Password: "></asp:Label>
                           </td>
                           <td style="text-align: left">
                               <asp:TextBox ID="txtPassword" CssClass="login-fields" TextMode="Password" runat="server"></asp:TextBox>
                           </td>
                       </asp:Panel>
                      
                   </tr>
                   <tr>
                       <td style="text-align:right">&nbsp;&nbsp;&nbsp;</td>
                       <td style="text-align:left">
                           <div style="width:260px;display:flex;flex-direction:row;justify-content:space-between">
                               <asp:LinkButton ID="lbtnForgotPassword" ForeColor="White" Font-Bold="true" runat="server" Text="Forgot Password"></asp:LinkButton>
                                <asp:Button ID="btnLogin" CssClass="btn btn-primary btn-sm" runat="server" Text="Login" OnClientClick="return ValidateLoginFields();" OnClick="btnLogin_Click" ></asp:Button>
                                <asp:Button ID="btnSignup" CssClass="btn btn-primary btn-sm" OnClick="btnSignup_Click" OnClientClick="return ValidateSignUpFields();" runat="server" Text="SignUp" Visible="false"></asp:Button>                         
                           </div>
                       </td>
                   </tr>
               </table>
           </div>
        </td>
    </tr>
</table>
<div style="width:950px;margin:auto;margin-top:25px">
<uc2:footerSection runat="server" id="footerSection" />
</div>
<ajaxToolkit:ModalPopupExtender 
    ID="mpeSignupPopup" 
    runat="server" 
    TargetControlID="lblDummy" 
    PopupControlID="pnlMessageDialog"  
    BackgroundCssClass="modal-background" PopupDragHandleControlID="dialog-header" />

<asp:Label ID="lblDummy" runat="server" Style="display: none"></asp:Label>
<asp:Panel ID="pnlMessageDialog" runat="server" CssClass="message-dialog" style="display:none;width:700px;height:300px">
    <div class="dialog-header">
      <asp:Label ID="lblDialogTitle" runat="server" Text="Save Password" style="font-weight:bold;font-size:14px"></asp:Label> 
         <asp:Button ID="btnClose" BackColor="White" runat="server" CssClass="btn-close" OnClientClick="return closePopup();" />
    </div>
    <div class="dialog-body" style="height:70%" >
         <asp:HiddenField ID="hfMessID" runat="server" />
        <table class="tblPopupFields">
             <tr>
                 <td style="width: 30px"></td>
                 <td class="lbltd">
                     <label>OTP Code:</label>
                 </td>
                 <td>
                     <asp:TextBox ID="txtOtpCode" runat="server" CssClass="ftrFields"></asp:TextBox>
                 </td>
            </tr>
             <tr>
                 <td style="width: 30px"></td>
                 <td class="lbltd">
                     <label>New Password:</label>
                 </td>
                 <td>
                     <asp:TextBox ID="txtNewPassword" runat="server" CssClass="ftrFields"></asp:TextBox>
                 </td>
             </tr>
             <tr>
                 <td style="width: 30px"></td>
                 <td class="lbltd">
                     <label>Confirm Password:</label>
                 </td>
                 <td>
                     <asp:TextBox ID="txtConfirmPassword" runat="server" CssClass="ftrFields"></asp:TextBox>
                 </td>
             </tr>
        </table>
    </div>
   <div style="display:flex;flex-direction:row;justify-content:center">
       <asp:Button ID="Button1" runat="server" Width="80px" Height="30px" BackColor="White" OnClientClick="return closePopup();" CssClass="btnCancel" Text="Cancel" />&nbsp;&nbsp;&nbsp;&nbsp;
         <asp:Button ID="btnConfirm" runat="server" Width="80px" Height="30px" OnClientClick="return validateConfirmPassFields();" OnClick="btnConfirm_Click" CssClass="btn btn-primary btn-sm" Text="Confirm" />
           
   </div>
</asp:Panel>

    <div id="successMessageBox" class="success-box" style="display: none;">
   Your account has been successfully created! You will be redirected shortly...
</div>
<script>
     function ValidateLoginFields() {
        var email = $("#<%= txtEmail.ClientID %>").val().trim();
        var password = $("#<%= txtPassword.ClientID %>").val().trim();

        if (email === "" && password === "") {
            alert("Please enter your User ID/Email.");
            return false;
        } else if (email === "") {
            alert("User ID/Email field cannot be empty.");
            return false;
        } else if (password === "") {
            alert("Password field cannot be empty.");
            return false;
        }
        
        return true;
    }
    function ValidateSignUpFields() {
        var email = $("#<%= txtEmail.ClientID %>").val().trim();
        if (email === "") {
            alert("Please enter your User ID/Email.");
            return false;
        }
        return true;

    }
    function closePopup() {
        // Close the ModalPopupExtender using JavaScript
        var popup = $find('<%= mpeSignupPopup.ClientID %>');
        popup.hide();
        return false;
    }
    function validateConfirmPassFields() {
        var isValid = true;

        // Get field values
        var otpCode = $("#<%= txtOtpCode.ClientID %>").val().trim();
         var newPassword = $("#<%= txtNewPassword.ClientID %>").val().trim();
         var confirmPassword = $("#<%= txtConfirmPassword.ClientID %>").val().trim();

         // Reset previous error styles
         $("#<%= txtOtpCode.ClientID %>, #<%= txtNewPassword.ClientID %>, #<%= txtConfirmPassword.ClientID %>").css("border", "");

        // Validate OTP Code
        if (otpCode === "") {
            alert("Please enter the OTP Code.");
            $("#<%= txtOtpCode.ClientID %>").css("border", "2px solid red");
            isValid = false;
        }

        // Validate New Password
        if (newPassword === "") {
            alert("Please enter a new password.");
            $("#<%= txtNewPassword.ClientID %>").css("border", "2px solid red");
            isValid = false;
        }

        // Validate Confirm Password
        if (confirmPassword === "") {
            alert("Please confirm your password.");
            $("#<%= txtConfirmPassword.ClientID %>").css("border", "2px solid red");
            isValid = false;
        }

        // Check if passwords match
        if (newPassword !== "" && confirmPassword !== "" && newPassword !== confirmPassword) {
            alert("New Password and Confirm Password do not match.");
             $("#<%= txtNewPassword.ClientID %>, #<%= txtConfirmPassword.ClientID %>").css("border", "2px solid red");
             isValid = false;
         }

         return isValid; // If false, prevents postback
    }
    function ShowMessageBox(message) {
        // Close the modal popup

        var msgBox = document.getElementById('successMessageBox');

        if (message) {
            msgBox.textContent = message;
        } // Show the success message box


        msgBox.style.display = 'block';

        // Hide the message after 1 second
        $("#successMessageBox").stop(true, true).fadeIn(800).delay(1500).fadeOut(1000);
    }
</script>