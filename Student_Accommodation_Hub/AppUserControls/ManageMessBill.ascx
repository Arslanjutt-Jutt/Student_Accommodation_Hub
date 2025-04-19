<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ManageMessBill.ascx.cs" Inherits="Student_Accommodation_Hub.AppUserControls.ManageMessBill" %>
<%@ Register Src="~/PagingControl/PagingUserControl.ascx" TagPrefix="uc4" TagName="PagingUserControl" %> 
<div class="pageHeading">
    <table style="width: 100%;">
        <tr>
            <td>
                <asp:Label ID="lblPageHeading" runat="server" Text="Manage Mess Bill"></asp:Label>
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
                <asp:LinkButton ID="hlReset" runat="server" Text="Reset" CssClass="custom-button reset-button"></asp:LinkButton>&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="custom-button search-button"></asp:Button>
            </td>

        </tr>
    </table>
</div>
<asp:Panel ID="pnlMessDetail" runat="server" style="width:100%; margin-top:30px">
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
              <asp:DropDownList runat="server" ID="ddlPageSize" CssClass="ftrFields" Width="45px">
                 <asp:ListItem Text="5" Value="5"></asp:ListItem>
                 <asp:ListItem Text="10" Value="10"></asp:ListItem>
                 <asp:ListItem Text="15" Value="15"></asp:ListItem>
              </asp:DropDownList>
          </td>
     </tr>
 </table>
    <asp:Panel ID="pnlHostelRentData" runat="server">
        <asp:Repeater ID="rprMessBill" runat="server" OnItemDataBound="rprMessBill_ItemDataBound">
            <HeaderTemplate>
                <table cellpadding="0" cellspacing="0" width="100%" class="tblShowData">

                    <tr>
                        <th>Month</th>
                        <th>Year</th>
                        <th style="text-align:center">Payment Status</th>
                        <th>Remarks</th>
                        <th style="text-align:center">Due Date</th>
                         <th style="text-align:center">Total Bill</th>
                        <th style="text-align:center">Block Days</th>
                        <th style="text-align:center">Deduction Amount</th>
                        <th style="text-align:center">Final Bill</th>
                        <th style="text-align:center">Actions</th>
                    </tr>
            </HeaderTemplate>

            <ItemTemplate>
                <tr class="rowControl">
                    <td>
                        <asp:Label runat="server" id="lblMonthName"><%# Eval("Month") %></asp:Label>
                        <input type="hidden" id="hfId" value='<%# Eval("BillId") %>' />
                    </td>
                    <td>
                        <asp:Label runat="server" id="lblYear"><%# Eval("Year") %></asp:Label>
                    </td>
                    <td style="text-align: center">
                        <asp:Label runat="server" id="lblPaymentStatus">
                            <%# Convert.ToBoolean(Eval("PaymentStatus")) ? "Paid" : "Pending" %>
                        </asp:Label>
                    </td>
                    <td>
                        <asp:Label runat="server" id="lblRemarks"><%# Eval("Remarks") %></asp:Label>
                    </td>
                    <td style="text-align:center">
                        <asp:Label runat="server" id="lblDueDate"><%# Convert.ToDateTime(Eval("DueDate")).ToString("yyyy-MM-dd") %></asp:Label>
                    </td>
                    <td style="text-align: center">
                        <asp:Label runat="server" id="lblTotalBill"><%# Convert.ToInt32( Eval("TotalBill") )%></asp:Label>
                    </td>
                    <td style="text-align: center">
                        <asp:Label runat="server" id="lblBlockDays"><%# Convert.ToInt32( Eval("BlockedDays") )%></asp:Label>
                    </td>
                    <td style="text-align: center">
                        <asp:Label runat="server" id="lblDeductionAmount"></asp:Label>
                    </td>
                    <td style="text-align: center">
                        <asp:Label runat="server" id="lblFinalBill"></asp:Label>
                    </td>
                    <td style="width:230px">
                        <asp:LinkButton ID="lbtnEdit" Width="80px" CssClass="btn btn-primary btn-sm" OnClientClick="return openUpdateRetPopup(this);" Text="Edit" runat="server">
                        </asp:LinkButton>&nbsp;
                         <asp:Button ID="btnChangeStatus" Width="120px" runat="server" OnClientClick="return confirm('Are you sure you want to update the payment status?');"
                             CssClass="btn btn-outline-danger btn-sm" OnClick="btnChangeStatus_Click"  CommandArgument='<%# Eval("BillId") %>'
                             Text="Change Status" />
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

<div id="successMessageBox" class="success-box" style="display: none;">
    Payment Status Updated successfully!
</div>

   <!-- Paging control -->  
<uc4:PagingUserControl ID="PagingUserControl1" runat="server" />

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

<ajaxToolkit:ModalPopupExtender 
    ID="mpeEditMessBillPopup" 
    runat="server" 
    TargetControlID="dummylbl" 
    PopupControlID="pnlMessBillPopup"  
    BackgroundCssClass="modal-background" PopupDragHandleControlID="dialog-header" />

<asp:Label ID="dummylbl" runat="server" Style="display: none"></asp:Label>
<asp:Panel ID="pnlMessBillPopup" runat="server" CssClass="message-dialog" style="display:none;width:700px;height:300px">
    <div class="dialog-header">
      <asp:Label ID="lblTitlePopup" runat="server" Text="Update Mess Bill" style="font-weight:bold;font-size:14px"></asp:Label> 
         <asp:Button ID="btnClose" BackColor="White" runat="server" CssClass="btn-close" OnClientClick="return closeUpdateRentPopup();" />
    </div>
    <div class="dialog-body" style="height:70%" >
         <asp:HiddenField ID="hfMessBillID" runat="server" />
        <table class="tblPopupFields">
            <tr>
                <td style="width:30px"></td>
                <td class="lbltd"> <label>Total Bill:</label></td>
                <td>
                   <asp:TextBox ID="txtTotalBill" CssClass="ftrFields" runat="server" ></asp:TextBox>
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
       <asp:Button ID="btnCencelPopup" runat="server" Width="80px" Height="30px" BackColor="White" OnClientClick="return closeUpdateMessBillPopup();" CssClass="btnCancel" Text="Cancel" />&nbsp;&nbsp;&nbsp;&nbsp;
         <asp:Button ID="btnUpdate" runat="server" Width="80px" Height="30px" CssClass="btn btn-primary btn-sm" OnClick="btnUpdate_Click" Text="Update" />
           
   </div>
</asp:Panel>
<script>
    function closePopup() {
        // Close the ModalPopupExtender using JavaScript
        var popup = $find('<%= mpePopup.ClientID %>');
         popup.hide();
         return false;
    }
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
    function openUpdateRetPopup(sender) {
        var popup = $find('<%= mpeEditMessBillPopup.ClientID %>');
         var item = $(sender).closest(".rowControl");
        var totalBill = $(item).find("[id*='lblTotalBill']").text().trim();
        var dueDate = $(item).find("[id*='lblDueDate']").text().trim();
        var remarks = $(item).find("[id*='lblRemarks']").text().trim();
        var rentId = $(item).find("input[type='hidden'][id*='hfId']").val();
        
        $("#<%= txtTotalBill.ClientID %>").val(totalBill);
         $("#<%= hfMessBillID.ClientID %>").val(rentId);
         $("#<%= txtDueDate.ClientID %>").val(dueDate);
         $("#<%= txtRemarks.ClientID %>").val(remarks);
        
         popup.show();
         return false;
    }
    function closeUpdateMessBillPopup() {
        // Close the ModalPopupExtender using JavaScript
        var popup = $find('<%= mpeEditMessBillPopup.ClientID %>');
         popup.hide();
         return false;
     }
</script>