<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMasterPage.Master" AutoEventWireup="true" CodeBehind="AddMessBill.aspx.cs" Inherits="Student_Accommodation_Hub.Admin.AddMessBill" %>
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
                <asp:Label ID="lblPageHeading" runat="server" Text="Add Mess Bill"></asp:Label>
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
             <td class="lbltd">Bill Amount:
             </td>
             <td colspan="3">
                 <asp:TextBox ID="txtBillAmount" runat="server" CssClass="ftrFields"></asp:TextBox>

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
                
    
                 <asp:Button ID="btnUploadMessBill" runat="server" Text="Upload Bill" OnClick="btnUploadMessBill_Click" OnClientClick="return validateMessBillForm();" CssClass="btn btn-primary btn-sm" />

             </td>
         </tr>
     </table>
 </div>
    <div id="successMessageBox" class="success-box" style="display: none;">
        Hostel Rent Uploaded successfully!
    </div>

        <!-- Success/Error Message Panel -->
    <ajaxToolkit:ModalPopupExtender
        ID="mpePopup"
        runat="server"
        TargetControlID="lblDummy"
        PopupControlID="pnlMessageDialog"
        BackgroundCssClass="modal-background" PopupDragHandleControlID="dialog-header" />

    <asp:Label ID="lblDummy" runat="server" Style="display: none"></asp:Label>
    <asp:Panel ID="pnlMessageDialog" runat="server" CssClass="message-dialog" Style="display: none">
        <div class="dialog-header">
            <asp:Label ID="lblDialogTitle" runat="server" Text="Success/Error" Style="font-weight: bold"></asp:Label>
        </div>
        <div class="dialog-body">
            <asp:Label ID="lblDialogMessage" runat="server" Text="Message content goes here."></asp:Label>
        </div>
        <div style="display: flex; flex-direction: row; justify-content: center">
            <asp:Button ID="btnOk" runat="server" Width="80px" OnClientClick="return closePopup();" CssClass="btn btn-primary btn-sm" BackColor="#21479e" Text="Ok" />
        </div>
    </asp:Panel>
    <script>
        function validateMessBillForm() {
            var isValid = true;
            function checkField(id) {
                var field = $("#" + id);
                var value = field.val();

                if (typeof value === "undefined" || value === null) {
                    value = ""; // Set empty string to avoid errors
                }

                value = value.trim();  // Ensure it's a string before trimming

                if (value === "" || value === "-1") {
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
            checkField('<%= txtBillAmount.ClientID %>');
           
            if (!isValid) {
                alert("Please fill all required fields highlighted in yellow.");
                return false;
            }
            return true;
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
        function closePopup() {
            // Close the ModalPopupExtender using JavaScript
            var popup = $find('<%= mpePopup.ClientID %>');
            popup.hide();
            return false;
        }
    </script>
</asp:Content>
