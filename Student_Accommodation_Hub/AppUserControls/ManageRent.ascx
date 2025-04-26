<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ManageRent.ascx.cs" Inherits="Student_Accommodation_Hub.AppUserControls.ManageRent" %>
<%@ Register Src="~/PagingControl/PagingUserControl.ascx" TagPrefix="uc4" TagName="PagingUserControl" %>    
<div class="pageHeading">
    <table style="width: 100%;">
        <tr>
            <td>
                <asp:Label ID="lblPageHeading" runat="server" Text="Manage Hostel Rent"></asp:Label>
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
                     <asp:ListItem Text="Paid" Value="1"></asp:ListItem>
                     <asp:ListItem Text="Pending" Value="0"></asp:ListItem>
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
                <asp:Button ID="hlReset" OnClick="hlReset_Click" runat="server" Text="Reset" CssClass="custom-button reset-button"></asp:Button>&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" CssClass="custom-button search-button"></asp:Button>
            </td>

        </tr>
    </table>
</div>
<asp:Panel ID="pnlRentDetail" runat="server" style="width:100%; margin-top:30px">
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
              <asp:DropDownList runat="server" ID="ddlPageSize" CssClass="ftrFields" OnTextChanged="ddlPageSize_TextChanged" AutoPostBack="true" Width="45px">
                 <asp:ListItem Text="5" Value="5"></asp:ListItem>
                 <asp:ListItem Text="10" Value="10"></asp:ListItem>
                 <asp:ListItem Text="15" Value="15"></asp:ListItem>
              </asp:DropDownList>
          </td>
     </tr>
 </table>
    <asp:Panel ID="pnlHostelRentData" runat="server">
        <asp:Repeater ID="rprHostelRent" runat="server" OnItemDataBound="rprHostelRent_ItemDataBound">
            <HeaderTemplate>
                <table cellpadding="0" cellspacing="0" width="100%" class="tblShowData">

                    <tr>
                       
                       
                        <th>Month</th>
                        <th>Year</th>
                        <th style="text-align:center">Payment Status</th>
                        <th>Remarks</th>
                        <th>Due Date</th>
                         <th style="text-align:center">Total Rent</th>
                        <asp:Panel ID="pnlActionHead" runat="server">
                        <th style="text-align:center">Actions</th>
                        </asp:Panel>
                    </tr>
            </HeaderTemplate>

            <ItemTemplate>
                <tr class="rowControl">
                    <td>
                        <label id="lblMonthName"><%# Eval("MonthName") %></label>
                        <input type="hidden" id="hfId" value='<%# Eval("RentId") %>' />
                    </td>
                    <td>
                        <label id="lblYear"><%# Eval("Year") %></label>
                    </td>
                    <td style="text-align: center">
                        <label id="lblPaymentStatus">
                            <%# Convert.ToBoolean(Eval("PaymentStatus")) ? "Paid" : "Pending" %>
                        </label>
                    </td>
                    <td>
                        <label id="lblRemarks"><%# Eval("Remarks") %></label>
                    </td>
                    <td>
                        <label id="lblDueDate"><%# Convert.ToDateTime(Eval("DueDate")).ToString("yyyy-MM-dd") %></label>
                    </td>
                    <td style="text-align: center">
                        <label id="lblTotalRent"><%# Convert.ToInt32( Eval("TotalRent") )%></label>
                    </td>
                     <asp:Panel ID="pnlAction" runat="server">
                    <td style="width:230px">
                        <asp:LinkButton ID="lbtnEdit" Width="80px" CssClass="btn btn-primary btn-sm" OnClientClick="return openUpdateRetPopup(this);" Text="Edit" runat="server">
                        </asp:LinkButton>&nbsp;
                         <asp:Button ID="btnChangeStatus" Width="120px" runat="server" OnClientClick="return confirm('Are you sure you want to update the payment status?');"
                             CssClass="btn btn-outline-danger btn-sm"  CommandArgument='<%# Eval("RentId") %>'
                             Text="Change Status" OnClick="btnChangeStatus_Click" />
                    </td>
                     </asp:Panel>
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
 <uc4:PagingUserControl ID="PagingUserControl1" runat="server" />

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
       <asp:Button ID="btnOk" runat="server" Width="80px" OnClientClick="return hidePopup();" CssClass="btn btn-primary btn-sm" BackColor="#21479e" Text="Ok" />
   </div>
</asp:Panel>

    <div id="successMessageBox" class="success-box" style="display: none;">
    Status Updated successfully!
</div>

<ajaxToolkit:ModalPopupExtender 
    ID="mpeEditHostelRentPopup" 
    runat="server" 
    TargetControlID="dummylbl" 
    PopupControlID="pnlHostelRentPopu"  
    BackgroundCssClass="modal-background" PopupDragHandleControlID="dialog-header" />

<asp:Label ID="dummylbl" runat="server" Style="display: none"></asp:Label>
<asp:Panel ID="pnlHostelRentPopu" runat="server" CssClass="message-dialog" style="display:none;width:700px;height:300px">
    <div class="dialog-header">
      <asp:Label ID="lblTitlePopup" runat="server" Text="Update Hostel Rent" style="font-weight:bold;font-size:14px"></asp:Label> 
         <asp:Button ID="btnClose" BackColor="White" runat="server" CssClass="btn-close" OnClientClick="return closeUpdateRentPopup();" />
    </div>
    <div class="dialog-body" style="height:70%" >
         <asp:HiddenField ID="hfRentID" runat="server" />
        <table class="tblPopupFields">
            <tr>
                <td style="width:30px"></td>
                <td class="lbltd"> <label>Total Rent:</label></td>
                <td>
                   <asp:TextBox ID="txtTotalRent" CssClass="ftrFields" runat="server" ></asp:TextBox>
                </td>
            </tr>
             <tr>
                 <td style="width: 30px"></td>
                 <td class="lbltd">
                     <label>Extend Due Date:</label>
                 </td>
                 <td>
                     <asp:TextBox ID="txtDueDate" runat="server" CssClass="ftrFields"></asp:TextBox>
                      <ajaxToolkit:CalendarExtender ID="ceDueDate" runat="server" TargetControlID="txtDueDate" Format="dd/MM/yyyy" />
                 </td>
            </tr>
             <tr>
                 <td style="width: 30px"></td>
                 <td class="lbltd">
                     <label>Remarks:</label>
                 </td>
                 <td>
                     <asp:TextBox ID="txtRemarks" runat="server" Height="70px" CssClass="ftrFields"></asp:TextBox>
                 </td>
             </tr>
        </table>
    </div>
   <div style="display:flex;flex-direction:row;justify-content:center">
       <asp:Button ID="btnCencelPopup" runat="server" Width="80px" Height="30px" BackColor="White" OnClientClick="return closeUpdateRentPopup();" CssClass="btnCancel" Text="Cancel" />&nbsp;&nbsp;&nbsp;&nbsp;
         <asp:Button ID="btnUpdate" runat="server" Width="80px" Height="30px" CssClass="btn btn-primary btn-sm" OnClick="btnUpdate_Click" Text="Update" />
           
   </div>
</asp:Panel>
<script>
    function ShowMessage(message) {
        // Close the modal popup
       
        var msgBox = document.getElementById('successMessageBox');

        if (message) {
            msgBox.textContent = message;
        } // Show the success message box


        msgBox.style.display = 'block';

        // Hide the message after 1 second
        $("#successMessageBox").stop(true, true).fadeIn(800).delay(1500).fadeOut(1000);
    }
    function hidePopup() {
        var popup = $find('<%= mpePopup.ClientID %>'); // Get the modal popup extender
         if (popup) {
             popup.hide(); // Hide the popup
        }
        return false;
    }
    function closeUpdateRentPopup() {
        // Close the ModalPopupExtender using JavaScript
        var popup = $find('<%= mpeEditHostelRentPopup.ClientID %>');
          popup.hide();
          return false;
    }
    function openUpdateRetPopup(sender) {
        var popup = $find('<%= mpeEditHostelRentPopup.ClientID %>');
        var item = $(sender).closest(".rowControl");
        var totalRent = $(item).find("[id$='lblTotalRent']").text().trim();
        var dueDate = $(item).find("[id$='lblDueDate']").text().trim();
        var remarks = $(item).find("[id$='lblRemarks']").text().trim();
        var rentId = $(item).find("input[type='hidden'][id*='hfId']").val();

        $("#<%= hfRentID.ClientID %>").val(rentId);
        $("#<%= txtDueDate.ClientID %>").val(dueDate);
        $("#<%= txtRemarks.ClientID %>").val(remarks);
        $("#<%= txtTotalRent.ClientID %>").val(totalRent);
        popup.show();
        return false;
    }
</script>