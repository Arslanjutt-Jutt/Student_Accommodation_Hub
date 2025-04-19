<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMasterPage.Master" AutoEventWireup="true" CodeBehind="ManageRent.aspx.cs" Inherits="Student_Accommodation_Hub.Admin.ManageRent" %>
<%@ Register Src="~/AppUserControls/ManageRent.ascx" TagPrefix="uc5" TagName="ManageRent" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
     <uc5:ManageRent ID="ucManageRent" runat="server" />
</asp:Content>
