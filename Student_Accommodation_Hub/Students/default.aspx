<%@ Page Title="" Language="C#" MasterPageFile="~/Students/StudentMaster.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="Student_Accommodation_Hub.Students._default" %>
<%@ Register Src="~/AppUserControls/RoomDetail.ascx" TagPrefix="uc3" TagName="RoomDetail" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   <uc3:RoomDetail ID="PagingUserControl1" runat="server" />
</asp:Content>
