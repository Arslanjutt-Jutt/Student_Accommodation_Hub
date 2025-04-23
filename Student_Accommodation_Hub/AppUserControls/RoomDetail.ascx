<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RoomDetail.ascx.cs" Inherits="Student_Accommodation_Hub.AppUserControls.RoomDetail" %>

<asp:Panel ID="pnlRoomInfo" style ="width:100%;margin-top:20px" runat="server" >
    <asp:Panel ID="pnlHeading" runat="server" CssClass="cce-heading" >
        <asp:Label ID="lblHeading" runat="server" Text="Room Detail:"></asp:Label>
        <asp:Image ID="imgIcon" runat="server" ImageUrl="~/ImageIcons/minus.png" />
    </asp:Panel>

    <asp:Panel ID="pnlRoomDetail" runat="server" CssClass="detail-container">
        <table class="room-data" cellspacing="10">
            <tr>
                <td class="lbltd"><asp:Label ID="lblRoom" runat="server" Text="Room No:"></asp:Label></td>
                <td>
                    <asp:Label ID="lblRoomNo" runat="server" Text=""></asp:Label></td>
                <td style="width: 280px"></td>
                <td class="lbltd">
                    <asp:Label ID="lblBlock" runat="server" Text="Block No:"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblBlockNo" runat="server" Text=""></asp:Label></td>
            </tr>
            <tr>
                <td class="lbltd">
                    <asp:Label ID="lblType" runat="server" Text="Room Type:"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblRoomType" runat="server" Text=""></asp:Label>
                </td>
                <td style="width: 280px"></td>
                <td class="lbltd">
                    <asp:Label ID="lblStatus" runat="server" Text="Room Status:"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblRoomStatus" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="lbltd">
                    <asp:Label ID="lblSecurity" runat="server" Text="Security Deposite:"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblSecurityValue" runat="server" Text=""></asp:Label>
                </td>
                <td style="width: 280px"></td>
                <td class="lbltd">
                    <asp:Label ID="lblRent" runat="server" Text="Room Rent:"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblRoomRent" runat="server" Text=""></asp:Label>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <ajaxToolkit:CollapsiblePanelExtender ID="cpeRoom" runat="server"
    TargetControlID="pnlRoomDetail"
    ExpandControlID="pnlHeading"
    CollapseControlID="pnlHeading"
    Collapsed="false"
    AutoCollapse="false"
    AutoExpand="false"
    ImageControlID="imgIcon"
    CollapsedImage="~/ImageIcons/Add.svg"
    ExpandedImage="~/ImageIcons/minus.png"
    SuppressPostBack="true" 
/>
</asp:Panel>

<asp:Panel ID="pnlFacilities" style ="width:100%;margin-top:20px" runat="server" >
    <asp:Panel ID="pnlFacilitiesHead" runat="server" CssClass="cce-heading" >
        <asp:Label ID="lblFacility" runat="server" Text="Facilities:"></asp:Label>
        <asp:Image ID="imgfacilityIcon" runat="server" ImageUrl="~/ImageIcons/minus.png" />
    </asp:Panel>

    <asp:Panel ID="pnlFacilitiesDetail" runat="server" CssClass="detail-container">
        <table class="room-data" style="margin-left:100px" cellspacing="10">
            <tr>
                <td class="lbltd">
                    <asp:Label ID="lblBed" runat="server" Text="🛏️ Bed:"></asp:Label> 
                </td>
                <td>
                    <asp:Label ID="lblBedValue" runat="server" Text="Yes"></asp:Label></td>
                <td style="width: 350px"></td>
                <td class="lbltd">
                    <asp:Label ID="lblAC" runat="server" Text="❄️ AC:"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblACValue" runat="server" Text="No"></asp:Label></td>
            </tr>
            <tr>
                <td class="lbltd">
                    <asp:Label ID="lblBathroom" runat="server" Text="🛁 Attached Bathroom:"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblBathroomValue" runat="server" Text="Yes"></asp:Label>
                </td>
                <td style="width: 280px"></td>
                <td class="lbltd">
                    <asp:Label ID="lblWifi" runat="server" Text="📶 Wi-Fi:"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblWifiValue" runat="server" Text="Yes"></asp:Label>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <ajaxToolkit:CollapsiblePanelExtender ID="CollapsiblePanelExtender1" runat="server"
    TargetControlID="pnlFacilitiesDetail"
    ExpandControlID="pnlFacilitiesHead"
    CollapseControlID="pnlFacilitiesHead"
    Collapsed="true"
    AutoCollapse="false"
    AutoExpand="false"
    ImageControlID="imgfacilityIcon"
    CollapsedImage="~/ImageIcons/Add.svg"
    ExpandedImage="~/ImageIcons/minus.png"
    SuppressPostBack="true" 
/>
</asp:Panel>

<asp:Panel ID="pnlRoomates" style ="width:100%;margin-top:20px" runat="server" >
    <asp:Panel ID="pnlRoomatesHead" runat="server" CssClass="cce-heading" >
        <asp:Label ID="lblRoomates" runat="server" Text="Roomates Detail:"></asp:Label>
        <asp:Image ID="imgRoomatesIcon" runat="server" ImageUrl="~/ImageIcons/minus.png" />
    </asp:Panel>

    <asp:Panel ID="pnlRoomateDetail" runat="server" CssClass="detail-container">
        <div style="display: flex; flex-wrap: wrap; gap: 20px;margin:10px">
            <asp:Repeater ID="rptRoommates" runat="server">
                <ItemTemplate>
                    <div style="flex: 0 0 calc(30% - 10px); border: 1px solid #ccc; border-radius: 10px; padding: 15px; box-shadow: 2px 2px 10px rgba(0,0,0,0.1); box-sizing: border-box;">
                        <strong><%# Eval("StudentName") %></strong><br />
                        <span><%# Eval("PhoneNumber") %></span><br />
                        <a href='mailto:<%# Eval("Email") %>'><%# Eval("Email") %></a>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </asp:Panel>
    <ajaxToolkit:CollapsiblePanelExtender ID="CollapsiblePanelExtender2" runat="server"
    TargetControlID="pnlRoomateDetail"
    ExpandControlID="pnlRoomatesHead"
    CollapseControlID="pnlRoomatesHead"
    Collapsed="true"
    AutoCollapse="false"
    AutoExpand="false"
    ImageControlID="imgRoomatesIcon"
    CollapsedImage="~/ImageIcons/Add.svg"
    ExpandedImage="~/ImageIcons/minus.png"
    SuppressPostBack="true" 
/>
</asp:Panel>