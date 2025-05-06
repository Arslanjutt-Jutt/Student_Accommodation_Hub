<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMasterPage.Master" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="Student_Accommodation_Hub.Admin.Profile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <div class="pageHeading">
        <table style="width: 100%;">
            <tr>
                <td>
                    <asp:Label ID="lblRegisterStudent" runat="server" Text="Profile"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
        <div class="container mt-5" style="width:700px;margin:auto">
           <div class="card p-4 shadow-sm">
               <div class="mb-3 row">
                   <label for="txtName" class="col-sm-2 col-form-label">Name</label>
                   <div class="col-sm-10">
                       <asp:TextBox ID="txtName" runat="server" CssClass="form-control" placeholder="Enter full name" />
                   </div>
               </div>
               <div class="mb-3 row">
                   <label for="txtCNIC" class="col-sm-2 col-form-label">CNIC</label>
                   <div class="col-sm-10">
                       <asp:TextBox ID="txtCNIC" runat="server" CssClass="form-control" placeholder="xxxxx-xxxxxxx-x" />
                   </div>
               </div>
               <div class="mb-3 row">
                   <label for="txtEmail" class="col-sm-2 col-form-label">Email</label>
                   <div class="col-sm-10">
                       <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" placeholder="example@email.com" />
                   </div>
               </div>
               <div class="mb-3 row">
                   <label for="txtPhone" class="col-sm-2 col-form-label">Phone</label>
                   <div class="col-sm-10">
                       <asp:TextBox ID="txtPhone" runat="server" CssClass="form-control" placeholder="03xx-xxxxxxx" />
                   </div>
               </div>
               <div class="text-center">
                   <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Submit" OnClientClick="return validateStudentProfile();" CssClass="btn btn-primary" />
               </div>
           </div>
       </div>
       <ajaxToolkit:ModalPopupExtender 
    ID="mpePopup" 
    runat="server" 
    TargetControlID="lblDummy" 
    PopupControlID="pnlMessageDialog" 
    BackgroundCssClass="modal-background" PopupDragHandleControlID="dialog-header" />

<asp:Label ID="lblDummy" runat="server" Style="display: none"></asp:Label>
<asp:Panel ID="pnlMessageDialog" runat="server" CssClass="message-dialog" style="display:none">
    <div class="dialog-header">
        <asp:Label ID="lblDialogTitle" runat="server" Text="Success/Error" style="font-weight:bold"></asp:Label>
        <asp:Button ID="btnClose" BackColor="White" runat="server" CssClass="btn-close" OnClick="btnOk_Click" />
       
      
    </div>
    <div class="dialog-body">
        <asp:Label ID="lblDialogMessage" runat="server" Text="Message content goes here."></asp:Label>
    </div>
   <div style="display:flex;flex-direction:row;justify-content:center">
       <asp:Button ID="btnOk" runat="server" Width="80px" OnClick="btnOk_Click" CssClass="btn btn-primary btn-sm" BackColor="#21479e" Text="Ok" />
   </div>
</asp:Panel>
    <script>
        function validateStudentProfile() {
            var name = document.getElementById('<%= txtName.ClientID %>');
             var cnic = document.getElementById('<%= txtCNIC.ClientID %>');
     var email = document.getElementById('<%= txtEmail.ClientID %>');
            var phone = document.getElementById('<%= txtPhone.ClientID %>');

            var isValid = true;

            // Reset background colors
            name.style.backgroundColor = '';
            cnic.style.backgroundColor = '';
            email.style.backgroundColor = '';
            phone.style.backgroundColor = '';

            // Check and highlight empty fields
            if (name.value.trim() === '') {
                name.style.backgroundColor = 'yellow';
                isValid = false;
            }
            if (cnic.value.trim() === '') {
                cnic.style.backgroundColor = 'yellow';
                isValid = false;
            }
            if (email.value.trim() === '') {
                email.style.backgroundColor = 'yellow';
                isValid = false;
            }
            if (phone.value.trim() === '') {
                phone.style.backgroundColor = 'yellow';
                isValid = false;
            }

            if (!isValid) {
                alert('Please fill out all required fields.');
            }

            return isValid;
        }

        function hidePopup() {
            var popup = $find('<%= mpePopup.ClientID %>'); // Get the modal popup extender
            if (popup) {
                popup.hide(); // Hide the popup
            }
        }

    </script>
</asp:Content>
