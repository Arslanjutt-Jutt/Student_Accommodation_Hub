<%@ Page Title="" Language="C#" MasterPageFile="~/Students/StudentMaster.Master" AutoEventWireup="true" CodeBehind="MessBill.aspx.cs" Inherits="Student_Accommodation_Hub.Students.MessBill" %>
<%@ Register Src="~/AppUserControls/ManageMessBill.ascx" TagPrefix="uc5" TagName="ManageMessBill" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
  <uc5:ManageMessBill ID="ucManageMessBill" runat="server" />
</asp:Content>
