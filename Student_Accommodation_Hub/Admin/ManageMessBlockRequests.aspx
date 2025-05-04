<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMasterPage.Master" AutoEventWireup="true" CodeBehind="ManageMessBlockRequests.aspx.cs" Inherits="Student_Accommodation_Hub.Admin.ManageMessBlockRequests" %>
<%@ Register Src="~/AppUserControls/MessBlockRequest.ascx" TagPrefix="uc7" TagName="MessBlockRequest" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
     <uc7:MessBlockRequest ID="ucManageMessBill" runat="server" />
</asp:Content>
