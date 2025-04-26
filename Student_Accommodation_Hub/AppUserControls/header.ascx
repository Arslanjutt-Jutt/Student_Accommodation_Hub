<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="header.ascx.cs" Inherits="Student_Accommodation_Hub.AppUserControls.header" %>
<div class="header-container">
    <div style="float:left">
        <img style="height:40px;width:200px" src="../ImageIcons/logo.png" />
    </div>

    <div style="position: absolute; left: 50%; transform: translateX(-50%); top: 50%; transform: translate(-50%, -50%);font-weight:bold;font-size:18px;color:#199ad6">
        Student's Portal
    </div>
    <div class="dropdown-container">
        <div class="dropdown-box">
            <div class="user-avatar">
                <asp:Image ID="imgProfile" Width="18px" runat="server" ImageUrl="~/ImageIcons/admin.svg" />
            </div>&nbsp;&nbsp;
            <div class="user-info">Welcome: <%= Student_Accommodation_Hub.AppUtilties.UserBaseControl.UserName %></div>
            <div class="dropdown-icon">
                <img src="../ImageIcons/arrow.svg" width="10px" />

            </div>
        </div>
        <div class="dropdown-itemsBox">
            <a href="#" class="dropdown-option">Profile</a>
            <a href="#" class="dropdown-option">Change Password</a>
            <a href="#" class="dropdown-option fa fa-sign-out">
                <img src="../ImageIcons/logout.png" width="15px" /> Logout</a>
        </div>
    </div>
</div>

<div class="manu-container">
    <div style="width: 1000px; display: flex; margin: auto;">
        <div class="lnk-page"> 
            <asp:HyperLink runat="server" NavigateUrl="~/Students/default.aspx" ID="hlDefaultPage">Room Detail</asp:HyperLink>
        </div>
        <div class="lnk-page">
            <a href="#">Hostel Rent</a>
        </div>
        <div class="lnk-page">
            <a href="#">Mess Bill</a>
        </div>
        <div class="lnk-page">
            <asp:HyperLink runat="server" NavigateUrl="~/Students/MessManu.aspx" ID="hlMessManu">Mess Manu</asp:HyperLink>
        </div>
    </div>
</div>

<script>
        $(document).ready(function() {
            let fadeSpeed = 200;
            let timeoutId;
            // Show dropdown on hover with fade effect
            $("#userDropdown").hover(
                function() {
                    clearTimeout(timeoutId);
                    $("#dropdownMenu").stop().fadeIn(fadeSpeed);
                },
                function() {
                    timeoutId = setTimeout(function() {
                        $("#dropdownMenu").stop().fadeOut(fadeSpeed);
                    }, 200); // Small delay before hiding to improve UX
                }
            ); 
           
        });
</script>