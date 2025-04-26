<%@ Page Title="" Language="C#" MasterPageFile="~/Students/StudentMaster.Master" AutoEventWireup="true" CodeBehind="MessManu.aspx.cs" Inherits="Student_Accommodation_Hub.Students.MessManu" %>
<%@ Register Src="~/AppUserControls/MessManu.ascx" TagPrefix="uc6" TagName="MessManu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
   <uc6:MessManu ID="ucManageMessBill" runat="server" />
</asp:Content>
