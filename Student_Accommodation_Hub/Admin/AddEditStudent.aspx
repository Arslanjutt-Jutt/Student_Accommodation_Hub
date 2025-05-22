<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMasterPage.Master" AutoEventWireup="true" CodeBehind="AddEditStudent.aspx.cs" Inherits="Student_Accommodation_Hub.Admin.AddEditStudent" EnableEventValidation="false" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
   <style>
    /* Center the form */
    .form-container {
        width: 50%;
        margin: 40px auto;
        padding: 20px;
        border-radius: 8px;
        background: #f8f8f8;
        box-shadow: 0px 0px 10px rgba(0, 0, 0, 0.1);
    }

    /* Table styles */
    .form-table {
        width: 100%;
        border-collapse: collapse;
         border-collapse: separate; /* Must be separate for border-spacing to work */
    border-spacing: 0 5px;
    }

    /* Labels */
    .form-table td label {
       
        display: block;
        margin-bottom: 5px;
        font-size: 14px;
    }

    /* Input fields */
    .form-control {
        width: 100%;
        padding: 8px;
        border: 1px solid #ccc;
        border-radius: 5px;
        font-size: 12px;
    }

    /* Dropdowns */
    select.form-control {
        appearance: none;
        background-color: #fff;
        cursor: pointer;
    }

    /* Textarea */
    .form-control[TextMode="MultiLine"] {
        height: 80px;
    }

    /* Button */
  

    
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
  <div class="pageHeading">
    <table style="width: 100%;">
        <tr>
            <td>
                <asp:Label ID="lblRegisterStudent" runat="server" Text=""></asp:Label>
            </td>
        </tr>
    </table>
</div>
 
<div class="form-container">
    <table class="form-table">
        <tr>
            <td><label for="StudentName">Student Name:</label></td>
            <td><asp:TextBox ID="txtStudentName" runat="server" CssClass="form-control"></asp:TextBox></td>
        </tr>
        
        <tr>
            <td><label for="StudentID">CNIC No:</label></td>
            <td><asp:TextBox ID="txtCnic" runat="server" CssClass="form-control"></asp:TextBox></td>
        </tr>

        <tr>
            <td>
            <label for="Gender" style="display: inline">Gender:</label>&nbsp;
           
            </td>
            <td>
                <asp:DropDownList ID="ddlGender" runat="server" CssClass="form-control">
                    <asp:ListItem Text="Select Gender" Value="-1"></asp:ListItem>
                    <asp:ListItem Text="Male" Value="Male"></asp:ListItem>
                    <asp:ListItem Text="Female" Value="Female"></asp:ListItem>
                </asp:DropDownList>                 
            </td>
            
        </tr>
    <tr>
        <td>
            <label for="dob">Date of Birth:</label></td>
        <td>
            <asp:TextBox ID="txtDob" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox></td>
    </tr>

        <tr>
            <td><label for="Email">Email:</label></td>
            <td><asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" TextMode="Email"></asp:TextBox></td>
        </tr>

        <tr>
            <td><label for="PhoneNumber">Phone Number:</label></td>
            <td><asp:TextBox ID="txtPhoneNumber" runat="server" CssClass="form-control"></asp:TextBox></td>
        </tr>
    <tr>
        <td>
            <label for="Country">Country:</label></td>
        <td>
            <asp:DropDownList ID="ddlCountry" runat="server" CssClass="form-control">
               
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td>
            <label for="State">State:</label></td>
        <td>
            <asp:DropDownList ID="ddlState" runat="server" CssClass="form-control">
             
            </asp:DropDownList>
            <ajaxToolkit:CascadingDropDown ID="cddState"
                runat="server" TargetControlID="ddlState" ParentControlID="ddlCountry"
                PromptText="Select State" ServiceMethod="GetStatesByCountryId" Category="State"
                ServicePath="~/SampleServices/SampleServices.asmx" SelectedValue="" LoadingText="Loading..." />
        </td>
    </tr>
        <tr>
            <td><label for="Address">Address:</label></td>
            <td><asp:TextBox ID="txtAddress" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox></td>
        </tr>

        <tr>
            <td><label for="BlockNo">Block Number:</label></td>
            <td>
                <asp:DropDownList ID="ddlBlockNo" runat="server" CssClass="form-control">
                    <asp:ListItem Text="Select Block" Value="-1"></asp:ListItem>
                    <asp:ListItem Text="A" Value="A"></asp:ListItem>
                    <asp:ListItem Text="B" Value="B"></asp:ListItem>
                    <asp:ListItem Text="C" Value="C"></asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>

        <tr>
            <td><label for="RoomNumber">Room Number:</label></td>
            <td>
                <asp:DropDownList ID="ddlRoomNumber" runat="server" CssClass="form-control">
                    <asp:ListItem Text="Select Room" Value="-1"></asp:ListItem>
                </asp:DropDownList>
                <ajaxToolkit:CascadingDropDown ID="cddBlockNo"
                    runat="server" TargetControlID="ddlRoomNumber" ParentControlID="ddlBlockNo"
                    PromptText="Select Room Number" ServiceMethod="GetRoomNumbersByBlock" Category="Block"
                    ServicePath="~/SampleServices/SampleServices.asmx" SelectedValue="" LoadingText="Loading..." />
            </td>
        </tr>
        <tr>
            <%--<td>
                &nbsp;
            </td>--%>
            <td align="center" colspan="2">
                <div style="display:flex;justify-content:space-evenly;margin-top:7px">
                    <div>
                         <asp:CheckBox ID="chkSecurityDeposit" Text="Has security deposit?" CssClass="chkBox" TextAlign="Left" runat="server" />
                    </div>
                    <div>
                        <asp:CheckBox ID="chkIsActive" Text="isActive" CssClass="chkBox" TextAlign="Left" runat="server" />
                    </div>
                </div>
            </td>
        </tr>

        <tr>
            <td colspan="2" style="padding-top: 30px">
                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btnCancel" OnClick="btnCancel_Click" />&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnRegister" runat="server" Text="Register" Width="100px" CssClass="btn btn-primary" OnClientClick="return validateForm();" OnClick="btnRegister_Click" />
                
            </td>
        </tr>
    </table>
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
        <asp:Button ID="btnClose" BackColor="White" runat="server" CssClass="btn-close" OnClick="btnCloseDialog_Click" />
       
      
    </div>
    <div class="dialog-body">
        <asp:Label ID="lblDialogMessage" runat="server" Text="Message content goes here."></asp:Label>
    </div>
   <div style="display:flex;flex-direction:row;justify-content:center">
       <asp:Button ID="btnOk" runat="server" Width="80px" OnClick="btnCloseDialog_Click" CssClass="btn btn-primary btn-sm" BackColor="#21479e" Text="Ok" />
   </div>
</asp:Panel>
<script>
        function validateForm() {
        var isValid = true;

        // Function to check if a field is empty
        function checkField(id) {
            var field = $("#" + id);
        if (field.val().trim() === "" || field.val() === "-1") {
            field.css("background-color", "yellow");
        isValid = false;
            } else {
            field.css("background-color", "");
            }
        }

        // Validate required fields
        checkField('<%= txtStudentName.ClientID %>');
        checkField('<%= txtCnic.ClientID %>');
        checkField('<%= ddlGender.ClientID %>');
        checkField('<%= txtDob.ClientID %>');
        checkField('<%= txtEmail.ClientID %>');
        checkField('<%= txtPhoneNumber.ClientID %>');
        checkField('<%= txtAddress.ClientID %>');
        checkField('<%= ddlBlockNo.ClientID %>');
        checkField('<%= ddlRoomNumber.ClientID %>');

        // Show alert if form is invalid
        if (!isValid) {
            alert("Please fill all required fields highlighted in yellow.");
        return false;
        }
        return true;
    }
    function hidePopup() {
        var popup = $find('<%= mpePopup.ClientID %>'); // Get the modal popup extender
         if (popup) {
             popup.hide(); // Hide the popup
         }
     }
</script>
</asp:Content>
