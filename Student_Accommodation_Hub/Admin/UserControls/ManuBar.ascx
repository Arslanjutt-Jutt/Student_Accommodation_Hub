<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ManuBar.ascx.cs" Inherits="Student_Accommodation_Hub.Admin.UserContriols.ManuBar" %>

<table class="tblHeader" style="width: 100%;" cellspacing="0" cellpadding="0">
    <tr>

        <td style="width: 700px"></td>
        <td>Welcome: &nbsp
            <asp:Label runat="server" Font-Bold="true" ID="lblUserName" Text=""></asp:Label>
        </td>
        <td>&nbsp &nbsp
        </td>
        <td style="text-align: center">
            <asp:HyperLink ID="hlProfile" CssClass="hlClass" runat="server" Text="Profile" NavigateUrl="~/Students/default.aspx" OnClientClick="return false;"></asp:HyperLink>
            &nbsp | &nbsp 
            <asp:LinkButton ID="lbtnLogOut" CssClass="hlClass" runat="server" OnClick="lbtnLogOut_Click" Text="Log Out"></asp:LinkButton>

        </td>
    </tr>
</table>

<table class="tblTabs" cellpadding="3">
    <tr>
        <td class="tdContainer">
            <asp:Panel ID="pnlUserManagement"  CssClass="pnlControlOptions" runat="server" >
            <asp:HyperLink ID="hlUserManagement" ForeColor="White" runat="server" Text="Manage Users"></asp:HyperLink>
                <asp:Panel ID="pnlUsersHover" CssClass="pnlHoverOption" runat="server">
                     <asp:HyperLink ID="hlManageStu" runat="server" Text="Manage Students" NavigateUrl="~/Admin/default.aspx"></asp:HyperLink>
                     <asp:HyperLink ID="hlManageStaff"  runat="server" Text="Manage Staff"></asp:HyperLink>
                </asp:Panel>
            </asp:Panel>
        </td>
        <td>
            <asp:HyperLink ID="hlRoomManagement" ForeColor="White" runat="server" Text="Manage Rooms" NavigateUrl="~/Admin/ManageRoom.aspx"></asp:HyperLink>
        </td>
        <td class="tdContainer">
           
            <asp:Panel ID="pnlManageFee" CssClass="pnlControlOptions" runat="server">
                 <asp:HyperLink ID="hlFeeMangement" ForeColor="White" runat="server" Text="Financial Managment"></asp:HyperLink>
                <asp:Panel ID="pnlDuesOption" CssClass="pnlHoverOption" runat="server">
                    <asp:HyperLink ID="hlHostelRent" runat="server" Text="Add Hostel Rent" NavigateUrl="~/Admin/AddHostelRent.aspx"></asp:HyperLink>
                    <asp:HyperLink ID="hlMessDues" runat="server" Text="Add Mess Bill" NavigateUrl="~/Admin/AddMessBill.aspx"></asp:HyperLink>
                </asp:Panel>
            </asp:Panel>
        </td>
        <td>
            <asp:HyperLink ID="hlComplaintManagement" ForeColor="White" runat="server" Text="Manage Complaints"></asp:HyperLink>
        </td>
        <td>
            <asp:HyperLink ID="hlAnnNot" ForeColor="White" runat="server" Text="Manage Mess Manu" NavigateUrl="~/Admin/ManageMessManu.aspx"></asp:HyperLink>
        </td>
        <td class="hlTabs" style="width: 150px">
            <asp:HyperLink ID="hlReports" ForeColor="White" runat="server" Text="Reports"></asp:HyperLink>
        </td>
        <%--<asp:Button ID="btn" runat="server" Text="button" OnClientClick="" />--%>
    </tr>
</table>
<script>
    /// <reference path="../../Scripts/jquery-3.7.1.intellisense.js">
    $(document).ready(function () {
        $(".tdContainer").hover(
           
            function () {
                $(this).find(".pnlHoverOption").fadeIn(0.1);
            },
            function () {
                $(this).find(".pnlHoverOption").fadeOut(0.1);
            }
        );
    });
 </script>
       