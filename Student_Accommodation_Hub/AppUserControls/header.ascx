<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="header.ascx.cs" Inherits="Student_Accommodation_Hub.AppUserControls.header" %>
<div class="header-container">
    <div style="float:left;font-weight:bold;font-size:18px;color:#199ad6">
        Student's Portal
    </div>
    <div class="dropdown-container">
        <div class="dropdown-box">
            <div class="user-avatar">
                <asp:Image ID="imgProfile" runat="server" ImageUrl="~/ImageIcons/EditProfile.svg" />
            </div>&nbsp;&nbsp;
            <div class="user-info">Welcome: Arslan Sabir</div>
            <div class="dropdown-icon">▼</div>
        </div>
        <div class="dropdown-itemsBox">
            <a href="#" class="dropdown-option">Profile</a>
            <a href="#" class="dropdown-option">Change Password</a>
            <a href="#" class="dropdown-option fa fa-sign-out">Logout</a>
        </div>
    </div>
</div>

<div class="manu-container">
    <div style="width: 1000px; display: flex; margin: auto;">
        <div class="lnk-page"> 
            <asp:HyperLink runat="server" ID="hlDefaultPage">Room Detail</asp:HyperLink>
        </div>
        <div class="lnk-page">
            <a href="#">Hostel Rent</a>
        </div>
        <div class="lnk-page">
            <a href="#">Mess Bill</a>
        </div>
        <div class="lnk-page">
            <a href="#">Mess Manu</a>
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