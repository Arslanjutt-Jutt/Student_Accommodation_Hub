<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMasterPage.Master" AutoEventWireup="true" CodeBehind="ManageMessBill.aspx.cs" Inherits="Student_Accommodation_Hub.Admin.WebForm1" %>
<%@ Register Src="~/AppUserControls/ManageMessBill.ascx" TagPrefix="uc5" TagName="ManageMessBill" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
     <uc5:ManageMessBill ID="ucManageMessBill" runat="server" />
</asp:Content>
