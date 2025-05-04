<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MessBlockRequest.ascx.cs" Inherits="Student_Accommodation_Hub.AppUserControls.MessBlockRequest" %>
<%@ Register Src="~/PagingControl/PagingUserControl.ascx" TagPrefix="uc4" TagName="PagingUserControl" %>
<div class="pageHeading">
    <table style="width: 100%;">
        <tr>
            <td>
                <asp:Label ID="lblPageHeading" runat="server" Text="Mess Block Requests"></asp:Label>
            </td>
        </tr>
    </table>
</div>
<asp:Panel ID="pnlFilters" runat="server" DefaultButton="btnSearch">
    <table class="tblFilter">
        <tr>
            <td class="lbltd">Student Name:
            </td>
            <td style="width: 120px">
                <asp:TextBox ID="txtStudentName" CssClass="ftrFields" runat="server" ></asp:TextBox>
                <ajaxToolkit:AutoCompleteExtender
                    ID="AutoCompleteExtender1"
                    runat="server"
                    TargetControlID="txtStudentName" ServicePath="~/SampleServices/SampleServices.asmx"
                    ServiceMethod="GetStudentNames"
                    MinimumPrefixLength="1"
                    CompletionInterval="10" CompletionListCssClass="completion-list"
                    CompletionListItemCssClass="completion-item"
                    CompletionListHighlightedItemCssClass="completion-item-highlighted"
                    EnableCaching="true"
                    FirstRowSelected="true"
                    CompletionSetCount="10">
                </ajaxToolkit:AutoCompleteExtender>
            </td>
            <td class="lbltd">
                Month:
            </td>
            <td style="width:120px">
                <asp:DropDownList ID="ddlMonths" CssClass="ftrFields" runat="server">
                </asp:DropDownList>
            </td>
            
            <td class="lbltd">
                Year:
            </td>
            <td style="width:70px">
                <asp:TextBox ID="txtYear" CssClass="ftrFields" Width="50px" Style="padding-left: 15px" MaxLength="2" placeholder="YY" runat="server">
                </asp:TextBox>
            </td>
            <td class="lbltd">Status:
            </td>
            <td style="width: 120px">
                <asp:DropDownList ID="ddlStatus" CssClass="ftrFields" Width="100px" runat="server">
                    <asp:ListItem Text="Any" Value="-1"></asp:ListItem>
                    
                    <asp:ListItem Text="Pending" Value="Pending"></asp:ListItem>
                    <asp:ListItem Text="Approved" Value="Approved"></asp:ListItem>
                    <asp:ListItem Text="Rejected" Value="Rejected"></asp:ListItem>
                </asp:DropDownList>
            </td>

        </tr>
        <tr>
            <td colspan="8" style="text-align:right;padding:20px">
                <asp:Button ID="btnReset"  runat="server" Text="Reset" OnClick="btnReset_Click" CssClass="custom-button reset-button" />&nbsp;&nbsp;
                <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" Text="Search" CssClass="custom-button search-button"></asp:Button>
            </td>
        </tr>
    </table>
</asp:Panel>
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
    <asp:Panel ID="pnlMessBlockRequestsDetail" runat="server">
        <asp:Repeater ID="rprMessBlockRequests" runat="server" OnItemDataBound="rprMessBlockRequests_ItemDataBound">
            <HeaderTemplate>
                <table cellpadding="0" cellspacing="0" width="100%" class="tblShowData">

                    <tr>
                        <th>Student Info</th>
                        <th>Month</th>
                        <th>Year</th>
                        <th style="text-align: center">Status</th>

                        <th style="text-align: center">Request Date</th>
                        <th style="text-align: center">Approved Date</th>
                        <th>Mess Block Date</th>
                        <th>Reason</th>
                        <th style="text-align: center">Actions</th>

                    </tr>
            </HeaderTemplate>

            <ItemTemplate>
                <tr class="rowControl">
                    <td>
                        <asp:Label runat="server" ID="lblStudentName">
                            <strong>Name:&nbsp;&nbsp;</strong> <%# Eval("StudentName") %><br />
                            <strong>Email:&nbsp;&nbsp;</strong><%# Eval("Email") %><br />
                             <strong>CNIC:&nbsp;&nbsp;</strong><%# Eval("CNIC") %>
                        </asp:Label>

                    </td>
                    <td>
                        <asp:Label runat="server" ID="lblMonthName"><%# Eval("Month") %></asp:Label>

                    </td>
                    <td>
                        <asp:Label runat="server" ID="lblYear"><%# Eval("Year") %></asp:Label>
                    </td>
                    <td style="text-align: center">
                        <asp:Label runat="server" ID="lblStatus">
                            <%# Eval("Status") %>
                        </asp:Label>
                    </td>
                    <td>
                        <asp:Label runat="server" ID="lblRequestDate"><%# Convert.ToDateTime(Eval("RequestedDate")) %></asp:Label>
                    </td>
                    <td style="text-align: center">
                        <asp:Label runat="server" ID="lblApprovedDate"><%# Convert.ToDateTime(Eval("ApprovedDate")) %></asp:Label>
                    </td>
                    <td>
                        <asp:Label runat="server" ID="lblMessBlockDate">
                          <strong> From: &nbsp;&nbsp;</strong>   <%# Convert.ToDateTime(Eval("StartDate")).ToString("yyyy-MM-dd") %> <br />
                           <strong>To:&nbsp;&nbsp; </strong>  <%# Convert.ToDateTime(Eval("EndDate")).ToString("yyyy-MM-dd") %>

                        </asp:Label>
                    </td>
                    <td>
                        <asp:Label runat="server" ID="lblReason"><%# Eval("Reason") %></asp:Label>

                    </td>
                    <td>
                        <asp:LinkButton ID="lbtnReject" CssClass="lbtn" CommandArgument='<%# Eval("RequestId") %>' OnClick="lbtnReject_Click" Text="Reject" OnClientClick="return confirm('Are you sure you want to reject this request?');" runat="server">
                        </asp:LinkButton>&nbsp;
                        <asp:LinkButton ID="lbtnAccept" CssClass="lbtn" CommandArgument='<%# Eval("RequestId") %>' OnClick="lbtnAccept_Click" OnClientClick="return confirm('Are you sure you want to accept this request?');" runat="server">Accept</asp:LinkButton>
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
<uc4:PagingUserControl ID="PagingUserControl1" runat="server" />

 <div id="successMessageBox" class="success-box" style="display: none;">
    Request Reject successfully!
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
       
      
    </div>
    <div class="dialog-body">
        <asp:Label ID="lblDialogMessage" runat="server" Text="Message content goes here."></asp:Label>
    </div>
   <div style="display:flex;flex-direction:row;justify-content:center">
       <asp:Button ID="btnOk" runat="server" Width="80px" OnClientClick="return hidePopup();" CssClass="btn btn-primary btn-sm" BackColor="#21479e" Text="Ok" />
   </div>
</asp:Panel>
<script>
    function showSuccessMessage(message) {
       
        var msgBox = document.getElementById('successMessageBox');

        if (message) {
            msgBox.textContent = message;
        } 
        $('#successMessageBox').fadeIn().delay(3000).fadeOut();
    }
</script>
