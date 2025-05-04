<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ChangePassword.ascx.cs" Inherits="Student_Accommodation_Hub.AppUserControls.ChangePassword" %>
<ajaxToolkit:ModalPopupExtender 
    ID="mpeChangePassword" 
    runat="server" 
    TargetControlID="lblDummyTrigger" 
    PopupControlID="pnlChangePasswordPopup"  
    BackgroundCssClass="modal-background" 
    PopupDragHandleControlID="dialog-header" />

<asp:Label ID="lblDummyTrigger" runat="server" Style="display: none"></asp:Label>

<asp:Panel ID="pnlChangePasswordPopup" runat="server" CssClass="message-dialog" style="display:none;width:700px;height:330px;">
    <div class="dialog-header" id="dialog-header">
        <asp:Label ID="lblPopupTitle" runat="server" Text="Change Password" CssClass="fw-bold fs-5"></asp:Label> 
        <asp:Button ID="btnClosePopup" runat="server" CssClass="btn-close float-end" BackColor="White" OnClientClick="return closePopup();" />
    </div>

    <div class="dialog-body" style="height:70%;">
        <table class="table table-borderless" style="width: 100%;">
            <tr>
                <td style="width: 30px;"></td>
                <td style="width: 180px;">
                    <label class="form-label">Old Password:</label>
                </td>
                <td>
                    <asp:TextBox ID="txtOldPassword" runat="server" CssClass="form-control" TextMode="Password" />
                </td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <label class="form-label">New Password:</label>
                </td>
                <td>
                    <asp:TextBox ID="txtNewPassword" runat="server" CssClass="form-control" TextMode="Password" />
                </td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <label class="form-label">Confirm Password:</label>
                </td>
                <td>
                    <asp:TextBox ID="txtConfirmPassword" runat="server" CssClass="form-control" TextMode="Password" />
                </td>
            </tr>
        </table>
    </div>

    <div class="text-center pb-3">
        <asp:Button ID="btnCancel" runat="server" Width="90px" Height="35px" Text="Cancel" CssClass="custom-button reset-button" OnClientClick="return closePopup();" />
        <asp:Button ID="btnChangePassword" runat="server" Width="90px" Height="35px" Text="Change" CssClass="custom-button search-button" OnClick="btnChangePassword_Click" OnClientClick="return validatePasswordChange();" />
    </div>
</asp:Panel>
<script>
    function validatePasswordChange() {
        var oldPassElem = document.getElementById('<%= txtOldPassword.ClientID %>');
         var newPassElem = document.getElementById('<%= txtNewPassword.ClientID %>');
         var confirmPassElem = document.getElementById('<%= txtConfirmPassword.ClientID %>');

     var oldPass = oldPassElem.value.trim();
     var newPass = newPassElem.value.trim();
     var confirmPass = confirmPassElem.value.trim();

     var isValid = true;

     // Reset backgrounds
     oldPassElem.style.backgroundColor = '';
     newPassElem.style.backgroundColor = '';
     confirmPassElem.style.backgroundColor = '';

     // Validate empty fields
     if (oldPass === '') {
         oldPassElem.style.backgroundColor = 'yellow';
         isValid = false;
     }
     if (newPass === '') {
         newPassElem.style.backgroundColor = 'yellow';
         isValid = false;
     }
     if (confirmPass === '') {
         confirmPassElem.style.backgroundColor = 'yellow';
         isValid = false;
     }

     if (!isValid) {
         alert("All fields are required.");
         return false;
     }

     // Validate password match
     if (newPass !== confirmPass) {
         alert("New password and confirm password do not match.");
         return false;
     }

     return true;
 }

 function openPopup() {
     $find('<%= mpeChangePassword.ClientID %>').show();
      return false;
  }

 function closePopup() {
        $find('<%= mpeChangePassword.ClientID %>').hide();
        return false;
    }

</script>