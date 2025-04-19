<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMasterPage.Master" AutoEventWireup="true" CodeBehind="AddHostelRent.aspx.cs" Inherits="Student_Accommodation_Hub.Admin.AddHostelRent" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .form-table {
            width: 100%;
            border-collapse: collapse;
            border-collapse: separate; /* Must be separate for border-spacing to work */
            border-spacing: 0 12px;
        }
       .form-table tr td 
       {
           padding-top:20px
       }
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">

    <div class="pageHeading">
    <table style="width: 100%; margin-right: 5px">
        <tr>
            <td>
                <asp:Label ID="lblPageHeading" runat="server" Text="Add Hostel Rent"></asp:Label>
            </td>
        </tr>
    </table>
</div>
    <div>
        <table class="form-table">
            <tr>
                <td style="width:200px">

                </td>
                <td class="lbltd">
                    Month :
                </td>
                <td style="width:100px">
                    <asp:DropDownList ID="ddlMonths" CssClass="ftrFields" Width="100px" runat="server">

                    </asp:DropDownList>
                </td>
                <td class="lbltd" style="width:70px">Year :
                </td>
                <td >
                    <asp:TextBox ID="txtYear" CssClass="ftrFields" Width="50px" style="padding-left:15px" MaxLength="2" placeholder="YY" runat="server">
                    </asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 200px"></td>
                <td class="lbltd">
                    Due Date :
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtDueDate" runat="server" CssClass="ftrFields" placeholder="Select Due Date"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="calDueDate" runat="server" TargetControlID="txtDueDate" Format="dd/MM/yyyy" />
                </td>
            </tr>
            <tr>
                <td style="width: 200px"></td>
                <td class="lbltd">Remarks:
                </td>
                <td colspan="3">
                   <asp:TextBox ID="txtRemarks" runat="server" CssClass="ftrFields" height="60px" Width="250px" TextMode="MultiLine" placeholder="Optional Comments"></asp:TextBox>
                   
                </td>
            </tr>
            <tr>
                 <td style="width: 200px"></td>
                <td colspan="2" class="lbltd" style="padding-top:30px">
                     <asp:Button ID="btnCancel" runat="server" OnClientClick="return resetRentForm();" Text="Clear" Height="31.2px" CssClass="btnCancel" />&nbsp;&nbsp;&nbsp;
                </td>
                <td colspan="2" style="padding-top:30px">
                   
       
                    <asp:Button ID="btnUploadRent" runat="server" Text="Upload Rent" OnClick="btnUploadRent_Click" OnClientClick="return validateRentForm();" CssClass="btn btn-primary btn-sm" />

                </td>
            </tr>
        </table>
    </div>
    <div id="successMessageBox" class="success-box" style="display: none;">
    Hostel Rent Uploaded successfully!
</div>
    <script>
        function validateRentForm() {
            var isValid = true;
            function checkField(id) {
                var field = $("#" + id);
                if (field.val().trim() === "" || field.val() === "-1") {
                    field.css("background-color", "yellow");
                    isValid = false;
                } else {
                    field.css("background-color", "");
                }
            }

            // Validate required fields (except Remarks)
            checkField('<%= ddlMonths.ClientID %>');  // Month Dropdown
            checkField('<%= txtYear.ClientID %>');    // Year Field
            checkField('<%= txtDueDate.ClientID %>'); // Due Date Field

            // Show alert if form is invalid
            if (!isValid) {
                alert("Please fill all required fields highlighted in yellow.");
                return false;
            }
            return true;
        }
        function resetRentForm() {
            // Clear all input fields
            $("#<%= ddlMonths.ClientID %>").val("-1"); // Reset dropdown to default
            $("#<%= txtYear.ClientID %>").val("");     // Clear Year field
            $("#<%= txtDueDate.ClientID %>").val("");  // Clear Due Date field
            $("#<%= txtRemarks.ClientID %>").val("");  // Clear Remarks field

            // Remove yellow background color
            $("#<%= ddlMonths.ClientID %>").css("background-color", "");
            $("#<%= txtYear.ClientID %>").css("background-color", "");
            $("#<%= txtDueDate.ClientID %>").css("background-color", "");

            // Reset the form position if needed
            $("form")[0].reset();
            return false;
        }
        function closePopupAndShowMessage(message) {
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
</asp:Content>
