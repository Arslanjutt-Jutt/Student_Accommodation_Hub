<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMasterPage.Master" AutoEventWireup="true" CodeBehind="ManageMessManu.aspx.cs" Inherits="Student_Accommodation_Hub.Admin.ManageMessManu" %>
<%@ Register Src="~/AppUserControls/MessManu.ascx" TagPrefix="uc6" TagName="MessManu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
     <uc6:MessManu ID="ucManageMessBill" runat="server" />
</asp:Content>
