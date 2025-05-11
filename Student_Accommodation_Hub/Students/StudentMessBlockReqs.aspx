<%@ Page Title="" Language="C#" MasterPageFile="~/Students/StudentMaster.Master" AutoEventWireup="true" CodeBehind="StudentMessBlockReqs.aspx.cs" Inherits="Student_Accommodation_Hub.Students.StudentMessBlockReqs" %>
<%@ Register Src="~/PagingControl/PagingUserControl.ascx" TagPrefix="uc4" TagName="PagingUserControl" %>  
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
        <div class="pageHeading">
    <table style="width: 100%;">
        <tr>
            <td>
                <asp:Label ID="lblPageHeading" runat="server" Text="Mess Block Requests"></asp:Label>
            </td>
            <td style="text-align:right">
                <asp:LinkButton ID="lbtnSendRequest" OnClientClick="return openSendRequestPopup();" CssClass="lbtn" runat="server" Text="Send New Request"></asp:LinkButton>
            </td>
        </tr>
    </table>
</div>
    <div>
    <table class="tblFilter">
        <tr>
            <td class="lbltd">
                Month:
            </td>
            <td style="width:120px">
                <asp:DropDownList ID="ddlMonths" CssClass="ftrFields" runat="server">
                </asp:DropDownList>
            </td>
            <td class="lbltd">
                Status:
            </td>
            <td style="width:120px">
                <asp:DropDownList ID="ddlStatus" CssClass="ftrFields" Width="100px" runat="server">
                    <asp:ListItem Text="Any" Value="-1"></asp:ListItem>

                    <asp:ListItem Text="Pending" Value="Pending"></asp:ListItem>
                    <asp:ListItem Text="Approved" Value="Approved"></asp:ListItem>
                    <asp:ListItem Text="Rejected" Value="Rejected"></asp:ListItem>
                </asp:DropDownList>
            </td>
            <td class="lbltd">
                Year:
            </td>
            <td style="width:140px">
                <asp:TextBox ID="txtYear" CssClass="ftrFields" Width="50px" Style="padding-left: 15px" MaxLength="2" placeholder="YY" runat="server">
                </asp:TextBox>
            </td>
            <td>
                <asp:Button ID="hlReset" runat="server" Text="Reset" CssClass="custom-button reset-button"></asp:Button>&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" CssClass="custom-button search-button"></asp:Button>
            </td>

        </tr>
    </table>
</div>
    <asp:Panel ID="pnlPageDetail" runat="server" style="width:100%; margin-top:30px">
     <table style="width:100%" >
     <tr>
         <td style="text-align:left;font-weight:bold;">
             Showing
             <asp:Label ID="lblFromRec" runat="server" Text=""></asp:Label>
             -
             <asp:Label ID="lblToRec" runat="server" Text=""></asp:Label>
             (out of
             <asp:Label ID="lblTotalRec" runat="server" Text=""></asp:Label>
             )
         </td>
         
          <td style="text-align:right">
              Page Size:&nbsp;
              <asp:DropDownList runat="server" ID="ddlPageSize" OnTextChanged="ddlPageSize_TextChanged" AutoPostBack="true" CssClass="ftrFields" Width="45px">
                 <asp:ListItem Text="5" Value="5"></asp:ListItem>
                 <asp:ListItem Text="10" Value="10"></asp:ListItem>
                 <asp:ListItem Text="15" Value="15"></asp:ListItem>
              </asp:DropDownList>
          </td>
     </tr>
 </table>
    <asp:Panel ID="pnlStudentMessBlockReqsInfo" runat="server">
        <asp:Repeater ID="rprStudentMessBlockReqs" runat="server" OnItemDataBound="rprStudentMessBlockReqs_ItemDataBound">
            <HeaderTemplate>
                <table cellpadding="0" cellspacing="0" width="100%" class="tblShowData">

                    <tr>


                        <th>Month</th>
                        <th>Year</th>
                        <th style="text-align: center">Status</th>
                        <th>Mess Block Date</th>
                        <th style="text-align: center">Request Date</th>
                        <th style="text-align: center">Approved/Rejected Date</th>
                        <th style="text-align: center">Reason</th>
                        <th style="width:50px"></th>
                    </tr>
            </HeaderTemplate>

            <ItemTemplate>
                <tr class="rowControl">
                    <td>
                        <label id="lblMonthName"><%# Eval("Month") %></label>
                    </td>
                    <td>
                        <label id="lblYear"><%# Eval("Year") %></label>
                    </td>
                    <td style="text-align: center">
                        <asp:Label ID="lblStatus" runat="server">
                            <%# Eval("Status") %>
                        </asp:Label>
                    </td>
                    <td style="width:200px">
                        <asp:Label runat="server" ID="lblMessBlockDate">
                          <strong> From: &nbsp;&nbsp;</strong>   <%# Convert.ToDateTime(Eval("StartDate")).ToString("yyyy-MM-dd") %> <br />
                          <strong>To:&nbsp;&nbsp; </strong>  <%# Convert.ToDateTime(Eval("EndDate")).ToString("yyyy-MM-dd") %>
                        </asp:Label>
                    </td>
                    <td style="text-align: center;">
                        <asp:Label runat="server" ID="lblRequestDate"><%# Convert.ToDateTime(Eval("RequestedDate")) %></asp:Label>
                    </td>
                    <td style="text-align: center;width:70px">
                        <asp:Label runat="server" ID="lblApprovedDate"
                            Text='<%# Eval("ApprovedDate") == DBNull.Value ? "N/A" : Convert.ToDateTime(Eval("ApprovedDate")).ToString("dd-MM-yyyy") %>'>
                        </asp:Label>
                    </td>
                    <td>
                        <label id="lblReason"><%# Eval("Reason") %></label>
                    </td>
                    <td>
                        <asp:LinkButton ID="lbtnDelete" runat="server" Visible="false" CommandArgument='<%# Eval("RequestId") %>' OnClick="lbtnDelete_Click" CssClass="lbtn" OnClientClick="return confirm('Are you sure you want to delete this mess block request?');" Text="Delete"></asp:LinkButton>
                    </td>
                </tr>
            </ItemTemplate>

            <FooterTemplate>
                </tbody>
       </table>
            </FooterTemplate>
        </asp:Repeater>

    </asp:Panel>

</asp:Panel>
    <asp:Panel ID="pnlNoRec" CssClass="pnlNoRecFound" runat="server" Style="width: 100%">
        <div>
            No records found.
        </div>
    </asp:Panel>
    <!-- Paging control -->
    <uc4:pagingusercontrol id="PagingUserControl1" runat="server" />

     <!-- Popup for Exceptions -->
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
       
      
    </div>
    <div class="dialog-body">
        <asp:Label ID="lblDialogMessage" runat="server" Text="Message content goes here."></asp:Label>
    </div>
   <div style="display:flex;flex-direction:row;justify-content:center">
       <asp:Button ID="btnOk" runat="server" Width="80px" OnClientClick="return hidePopup();" CssClass="btn btn-primary btn-sm" Text="Ok" />
   </div>
</asp:Panel>
     <!-- Popup for Send new mess block request -->
 <ajaxToolkit:ModalPopupExtender 
    ID="mpeMessBlockRequest" 
    runat="server" 
    TargetControlID="lblDummy2" 
    PopupControlID="pnlBlockRequestController" 
    BackgroundCssClass="modal-background" PopupDragHandleControlID="dialog-header" />

<asp:Label ID="lblDummy2" runat="server" Style="display: none"></asp:Label>
<asp:Panel ID="pnlBlockRequestController" runat="server" CssClass="message-dialog" style="display:none;width:500px">
    <div class="dialog-header">
        <asp:Label ID="lblPopupMessTitle" runat="server" Text="Send Mess Block Request" CssClass="fs-6" style="font-weight:bold"></asp:Label>
            <asp:Button ID="btnClosePopup" runat="server" CssClass="btn-close float-end btn-sm" BackColor="White" OnClientClick="return hideMessBlockPopup();" />
    </div>
    <div class="dialog-body">
        <table  cellpadding="10px" style="width:100%">
            <tr>
                <td class="lbltd">Month:
                </td>
                <td >
                    <asp:DropDownList ID="ddlMonthPopup" style="width:90px" CssClass="ftrFields" runat="server">
                    </asp:DropDownList>
                </td>
                <td class="lbltd">Year:
                </td>
                <td>
                    <asp:TextBox ID="txtYearPopup" CssClass="ftrFields" Width="50px" Style="padding-left: 15px" MaxLength="2" placeholder="YY" runat="server">
                    </asp:TextBox>
                </td>
            </tr>
            
            <tr>
                
                <td class="lbltd">From:
                </td>
                <td>
                 <asp:TextBox ID="txtFromDate" runat="server" autocomplete="off" Width="90px" CssClass="ftrFields"></asp:TextBox>
                  <ajaxToolkit:CalendarExtender ID="calFromDate" runat="server" TargetControlID="txtFromDate" Format="dd/MM/yyyy" />
                </td>
                <td class="lbltd">To:
                </td>
                <td>
                    <asp:TextBox ID="txtToDate" CssClass="ftrFields" autocomplete="off" Width="90px" runat="server">
                    </asp:TextBox>
                     <ajaxToolkit:CalendarExtender ID="calToDate" runat="server" TargetControlID="txtToDate" Format="dd/MM/yyyy" />
                </td>
            </tr>
            <tr>
                <td class="lbltd">
                    Reason:
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtReason" runat="server" CssClass="ftrFields" Height="45px" Width="100%"></asp:TextBox>
                </td>
            </tr>
        </table>
    </div>
   <div style="display:flex;flex-direction:row;justify-content:center;margin-top:10px">
        <asp:Button ID="btnCencel" runat="server" Width="80px" OnClientClick="return hideMessBlockPopup();" CssClass="custom-button reset-button" Text="Cancel" /> &nbsp;&nbsp;&nbsp;
       <asp:Button ID="btnSubmit" runat="server" Width="80px" OnClientClick="return validateMessBlockRequestForm();" OnClick="btnSubmit_Click" CssClass="custom-button search-button" Text="Submit" />
   </div>
</asp:Panel>

    <div id="successMessageBox" class="alert alert-success" style="display:none; position: fixed; top: 20px; left: 50%; transform: translateX(-50%); z-index: 9999; min-width: 250px;">
        Your request has been submitted successfully!
    </div>
 <script>
     function showSuccessMessage(message) {
         
         if (message) {

             $('#successMessageBox').text(message);

         }
         $('#successMessageBox').fadeIn().delay(3000).fadeOut();
     }
     function hidePopup() {
         var popup = $find('<%= mpePopup.ClientID %>'); // Get the modal popup extender
         if (popup)
         {
         popup.hide(); // Hide the popup
         }
         return false;
     }
     function hideMessBlockPopup() {
         var popup = $find('<%= mpeMessBlockRequest.ClientID %>'); // Get the modal popup extender
         if (popup) {
             popup.hide(); // Hide the popup
         }
         return false;
     }
     function openSendRequestPopup() {
         $('#<%= ddlMonthPopup.ClientID %>').prop('selectedIndex', 0);
         $('#<%= txtYearPopup.ClientID %>').val('');
         $('#<%= txtFromDate.ClientID %>').val('');
        $('#<%= txtToDate.ClientID %>').val('');
         $('#<%= txtReason.ClientID %>').val('');
         // Close the ModalPopupExtender using JavaScript
         var popup = $find('<%= mpeMessBlockRequest.ClientID %>');
          popup.show();
          return false;
     }
    function validateMessBlockRequestForm() {
        var isValid = true;
         var emptyFields = [];

         // Get values
         var month = $('#<%= ddlMonthPopup.ClientID %>').val();
        var year = $('#<%= txtYearPopup.ClientID %>').val();
        var fromDateStr = $('#<%= txtFromDate.ClientID %>').val();
        var toDateStr = $('#<%= txtToDate.ClientID %>').val();
        var reason = $('#<%= txtReason.ClientID %>').val();

          // Reset background colors
        $('#<%= ddlMonthPopup.ClientID %>, #<%= txtYearPopup.ClientID %>, #<%= txtFromDate.ClientID %>, #<%= txtToDate.ClientID %>, #<%= txtReason.ClientID %>')
            .css('background-color', '');

        // Check required fields
        if (month === "-1" || month === "") {
            $('#<%= ddlMonthPopup.ClientID %>').css('background-color', 'yellow');
            isValid = false;
        }
        if (year.trim() === "") {
            $('#<%= txtYearPopup.ClientID %>').css('background-color', 'yellow');
            isValid = false;
        }
        if (fromDateStr.trim() === "") {
            $('#<%= txtFromDate.ClientID %>').css('background-color', 'yellow');
            isValid = false;
        }
        if (toDateStr.trim() === "") {
            $('#<%= txtToDate.ClientID %>').css('background-color', 'yellow');
            isValid = false;
        }
        if (reason.trim() === "") {
            $('#<%= txtReason.ClientID %>').css('background-color', 'yellow');
          isValid = false;
        }

          if (!isValid) {
              alert("Please fill all required fields highlighted in yellow.");
          return false;
        }

          // Compare month from FromDate and ToDate
          var fromDate = new Date(fromDateStr.split('/').reverse().join('-')); // Convert dd/MM/yyyy to yyyy-MM-dd
          var toDate = new Date(toDateStr.split('/').reverse().join('-'));
          var selectedMonth = month.trim().toLowerCase();
          var fromMonth = fromDate.toLocaleString('default', {month: 'long' }).toLowerCase();
          var toMonth = toDate.toLocaleString('default', {month: 'long' }).toLowerCase();

          if (fromMonth !== selectedMonth || toMonth !== selectedMonth) {
              alert("Selected month must match the month of both From and To dates.");
          return false;
        }

          return true;
    }


 </script>
</asp:Content>
