<%@ Page Title="" Language="C#" MasterPageFile="~/Students/StudentMaster.Master" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="Student_Accommodation_Hub.Students.Profile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
     <div class="container mt-5">
            <h3 class="mb-4 text-center">Student Profile</h3>
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
                    <asp:Button ID="btnSave" runat="server" Text="Save Profile" OnClick="btnSave_Click" OnClientClick="return validateStudentProfile(); " CssClass="btn btn-primary" />
                </div>
            </div>
        </div>
     <div id="successMessageBox" class="alert alert-success" style="display:none; position: fixed; top: 20px; left: 50%; transform: translateX(-50%); z-index: 9999; min-width: 250px;">
     Profile saved successfully!
 </div>
    <script type="text/javascript">
       
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
   
        function showSuccessMessage() {
            $('#successMessageBox').fadeIn().delay(3000).fadeOut();
        }
    </script>

</asp:Content>
