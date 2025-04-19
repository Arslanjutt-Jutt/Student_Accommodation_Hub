<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMasterPage.Master" AutoEventWireup="true" CodeBehind="ManageRoom.aspx.cs" Inherits="Student_Accommodation_Hub.Admin.ManageRoom" %>


<%@ Register Src="~/PagingControl/PagingUserControl.ascx" TagPrefix="uc4" TagName="PagingUserControl" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
 .completion-list {
    background-color: #fff; /* White background */
    border: 1px solid #ccc; /* Border color */
    max-height: 200px; /* Max height for scrollable list */
    overflow-y: auto; /* Vertical scrolling */
    padding: 0; /* Remove padding */
    margin: 0; /* Remove margin */
    list-style-type: none; /* Remove bullet points */
    width: 100%
}


.completion-item {
    padding-left: 5px;
    cursor: pointer;
    font-size: 14px;
    color: #333;
    font-size: 11px;
    height:20px
}

.completion-item-highlighted {
    background-color: #0078FF;
    padding-left: 5px;
    font-size: 11px;
    color:white;
    height:20px
}
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <div class="pageHeading">
    <table style="width: 100%;margin-right:5px">
        <tr>
            <td>
                <asp:Label ID="lblManageRoom" runat="server" Text="Manage Room"></asp:Label>
            </td>
            <td style="text-align:right">
                <asp:HyperLink ID="btnAddRoom" runat="server" Text="Add New Room" CssClass="btn btn-primary btn-sm" NavigateUrl="~/Admin/AddEditRoom.aspx"></asp:HyperLink>

            </td>
        </tr>
    </table>

</div>
    <div>
        <table class="tblFilter">
            <tr>
                <td class="lbltd">Room No:</td>
                <td>
                    <asp:TextBox ID="txtRoomNo" CssClass="ftrFields" runat="server"></asp:TextBox>
                    <ajaxToolkit:AutoCompleteExtender
                        ID="AutoCompleteExtender1"
                        runat="server"
                        TargetControlID="txtRoomNo" ServicePath="~/SampleServices/SampleServices.asmx"
                        ServiceMethod="GetRommNumbers"
                        MinimumPrefixLength="1"
                        CompletionInterval="10" CompletionListCssClass="completion-list"
                        CompletionListItemCssClass="completion-item"
                        CompletionListHighlightedItemCssClass="completion-item-highlighted"
                        EnableCaching="true"
                        FirstRowSelected="true"
                        CompletionSetCount="10">
                    </ajaxToolkit:AutoCompleteExtender>
                </td>
                <td class="lbltd">Room Type:</td>
                <td>
                    <asp:DropDownList ID="ddlRoomType" runat="server" CssClass="ftrFields">
                        <asp:ListItem Text="All" Value="0"></asp:ListItem>
                        <asp:ListItem Text="4-Seater Room" Value="4"></asp:ListItem>
                        <asp:ListItem Text="6-Seater Room" Value="6"></asp:ListItem>
                        <asp:ListItem Text="8-Seater Room" Value="8"></asp:ListItem>
                    </asp:DropDownList>

                </td>
                <td class="lbltd">Block No:</td>
                <td>
                    <asp:DropDownList ID="ddlBlockNo" CssClass="ftrFields" runat="server">
                        <asp:ListItem Text="Any" Value="0"></asp:ListItem>
                        <asp:ListItem Text="A" Value="A"></asp:ListItem>
                        <asp:ListItem Text="B" Value="B"></asp:ListItem>
                        <asp:ListItem Text="C" Value="C"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td colspan="6" style="padding-top: 15px;text-align:right">
                    <asp:HyperLink ID="hlReset" runat="server" Text="Reset" CssClass="custom-button reset-button"></asp:HyperLink>&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" CssClass="custom-button search-button" ></asp:Button>
                </td>
            </tr>
        </table>
    </div>
    <asp:Panel ID="pnlRoomDetail" runat="server" style="width:100%; margin-top:30px">
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
              <asp:DropDownList runat="server" ID="ddlPageSize" OnTextChanged="ddlPageSize_TextChanged" CssClass="ftrFields" Width="45px" AutoPostBack="true">
                 <asp:ListItem Text="5" Value="5"></asp:ListItem>
                 <asp:ListItem Text="10" Value="10"></asp:ListItem>
                 <asp:ListItem Text="15" Value="15"></asp:ListItem>
              </asp:DropDownList>
          </td>
     </tr>
 </table>
         <div>
   <asp:Repeater ID="rptRooms" runat="server" OnItemDataBound="rptRooms_ItemDataBound">
    <HeaderTemplate>
        <table cellpadding="0" cellspacing="0" width="100%" class="tblShowData">
           
                <tr>
                    <th>Room Number</th>
                    <th style="text-align:center">Room Capacity</th>
                    <th style="text-align:center">Block No</th>
                    <th>Room Status</th>
                    <th>Room Rent</th>
                    <th>Security Deposit</th>
                    <th style="text-align:center">Attached Bathroom</th>
                    <th style="text-align:center">AC</th>
                    <th style="text-align:center">WiFi</th>
                    <th style="text-align:center">Student Count</th>
                    <th>Action</th>
                </tr>
           
    </HeaderTemplate>
    
    <ItemTemplate>
        <tr class="rowControl">
            <td><%# Eval("RoomNumber") %></td>

            <td style="text-align:center"><%# Eval("RoomType") %></td>

            <td style="text-align:center"><%# Eval("BlockNo") %></td>

            <td><%# Eval("RoomStatus") %></td>

            <td><%# Eval("RoomRent") %></td>

            <td><%# Eval("SecurityDeposit") %></td>

            <td style="text-align:center"><%# Convert.ToBoolean(Eval("HasAttachedBathroom")) ? "Yes" : "No" %></td>

            <td style="text-align:center"><%# Convert.ToBoolean(Eval("HasAC")) ? "Yes" : "No" %></td>

            <td style="text-align:center"><%# Convert.ToBoolean(Eval("HasWiFi")) ? "Yes" : "No" %></td>

            <td style="text-align:center;">
                <asp:HyperLink ID="lnkStudentCount" Width="100%" style="text-decoration:none" runat="server" >    
                </asp:HyperLink>
            </td>
            <td>
                <asp:HyperLink ID="hlEditRoom" style="cursor:pointer" runat="server">
                    <asp:Image ID="imgEdit" runat="server" Height="22px" ImageUrl="~/ImageIcons/EditIcon.svg" ToolTip="Edit Room Detail." />
                </asp:HyperLink> 
            </td>
        </tr>
    </ItemTemplate>
    
    <FooterTemplate>
           
        </table>
    </FooterTemplate>
</asp:Repeater>
 </div>
    </asp:Panel>
       <asp:Panel ID="pnlNoRec" CssClass="pnlNoRecFound" runat="server" style="width:100%">
       <div>
           No records found.
       </div>
   </asp:Panel>
<!-- Paging control -->
       
 <uc4:PagingUserControl ID="PagingUserControl1" runat="server" />
        <!-- Success/Error Message Panel -->
<ajaxToolkit:ModalPopupExtender 
    ID="mpePopup" 
    runat="server" 
    TargetControlID="lblDummy" 
    PopupControlID="pnlMessageDialog"  
    BackgroundCssClass="modal-background" PopupDragHandleControlID="dialog-header" OnCancelScript="btnCloseDialog_Click" />

<asp:Label ID="lblDummy" runat="server" Style="display: none"></asp:Label>
<asp:Panel ID="pnlMessageDialog" runat="server" CssClass="message-dialog" style="display:none">
    <div class="dialog-header">
        <asp:Label ID="lblDialogTitle" runat="server" Text="Update Mess Manu" style="font-weight:bold"></asp:Label>

        
       
    </div>
    <div class="dialog-body">
        <asp:Label ID="lblDialogMessage" runat="server" Text="Message content goes here."></asp:Label>
    </div>
   <div style="display:flex;flex-direction:row;justify-content:center">
       <asp:Button ID="btnOk" runat="server" Width="80px" CssClass="btn btn-primary btn-sm" BackColor="#21479e" Text="Ok" />
   </div>
</asp:Panel>
</asp:Content>

