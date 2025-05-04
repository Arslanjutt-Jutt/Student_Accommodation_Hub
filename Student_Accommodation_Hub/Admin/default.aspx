<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMasterPage.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="Student_Accommodation_Hub.Admin._default" EnableEventValidation="false" %>
<%@ Register Src="~/PagingControl/PagingUserControl.ascx" TagPrefix="uc3" TagName="PagingUserControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
 


    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
  <div class="pageHeading">
    <table style="width:100%;margin-right:5px">
        <tr>
            <td>
                 <asp:Label ID="lblManageStudents" runat="server" Text="Manage Students"></asp:Label>
            </td>
            <td style="text-align:right">
                <asp:HyperLink ID="btnAddStudent" runat="server" Text="Add New Student" CssClass="btn btn-primary btn-sm" NavigateUrl="~/Admin/AddEditStudent.aspx"></asp:HyperLink>
            </td>
           
        </tr>
    </table>
   
</div>
    <div>
        <table class="tblFilter">
            <tr>
                <td class="lbltd">Student Name:</td>
                <td>
                    <asp:TextBox ID="txtStudentName" CssClass="ftrFields" runat="server"></asp:TextBox>
                    <ajaxToolkit:AutoCompleteExtender
                        ID="AutoCompleteExtender1"
                        runat="server"
                        TargetControlID="txtStudentName" ServicePath="~/SampleServices/SampleServices.asmx"
                        ServiceMethod="GetStudentNames"
                        MinimumPrefixLength="1"
                        CompletionInterval="10" CompletionListCssClass="completion-list"
                        CompletionListItemCssClass="completion-item"
                        CompletionListHighlightedItemCssClass="completion-item-highlighted"
                        EnableCaching="true"
                        FirstRowSelected="true"
                        CompletionSetCount="10">
                    </ajaxToolkit:AutoCompleteExtender>
                </td>
                <td class="lbltd">CNIC No:</td>
                <td>
                    <asp:TextBox ID="txtStudentID" CssClass="ftrFields" runat="server"></asp:TextBox></td>
                <td class="lbltd">Gender:</td>
                <td>
                    <asp:DropDownList ID="ddlGender" CssClass="ftrFields" runat="server">
                        <asp:ListItem Text="Any" Value="0"></asp:ListItem>
                        <asp:ListItem Text="Male" Value="Male"></asp:ListItem>
                        <asp:ListItem Text="Female" Value="Female"></asp:ListItem>
                    </asp:DropDownList>
            </tr>

            <tr>
                <td class="lbltd">Email:</td>
                <td>
                    <asp:TextBox ID="txtEmail" runat="server" CssClass="ftrFields" TextMode="Email"></asp:TextBox></td>
                <td class="lbltd">Phone Number:</td>
                <td>
                    <asp:TextBox ID="txtPhoneNumber" CssClass="ftrFields" runat="server"></asp:TextBox></td>
                <td class="lbltd">Country:</td>
                <td> 
                    <asp:DropDownList ID="ddlCountry" CssClass="ftrFields" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="lbltd">States:</td>
                <td>
                    <asp:DropDownList ID="ddlState" CssClass="ftrFields" runat="server">
                    </asp:DropDownList>
                    <ajaxToolkit:CascadingDropDown ID="cddState"
                        runat="server" TargetControlID="ddlState" ParentControlID="ddlCountry"
                        PromptText="All States" ServiceMethod="GetStatesByCountryId" Category="State"
                        ServicePath="~/SampleServices/SampleServices.asmx" SelectedValue="0" LoadingText="Loading..." />

                </td>
                <td class="lbltd">Block Number:</td>
                <td>
                    <asp:DropDownList ID="ddlBlockNo" CssClass="ftrFields chosen-select" runat="server">
                        <asp:ListItem Text="All Block" Value="0"></asp:ListItem>
                        <asp:ListItem Text="A" Value="A"></asp:ListItem>
                        <asp:ListItem Text="B" Value="B"></asp:ListItem>
                        <asp:ListItem Text="C" Value="C"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="lbltd">Room Number:</td>
                <td>
                    <asp:DropDownList ID="ddlRoomNumber" CssClass="ftrFields" runat="server">
                        <asp:ListItem Text="Select Room" Value="-1"></asp:ListItem>
                    </asp:DropDownList>
                </td>
               
            </tr>
            <tr>
                <td class="lbltd"></td>
                <td colspan="4" style="padding-top: 0px">

                    <asp:Label runat="server" AssociatedControlID="chkSecurityDeposit" Style="display: inline">Has security deposit?</asp:Label>
                    <asp:CheckBox ID="chkSecurityDeposit" TextAlign="Left" runat="server" />
                </td>
                <td style="padding-top:12px">
                    <asp:HyperLink ID="hlReset" runat="server" Text="Reset" CssClass="custom-button reset-button"></asp:HyperLink>&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="custom-button search-button" OnClick="btnSearch_Click"></asp:Button>
                </td>

            </tr>
         
        </table>
    </div>
    <asp:Panel ID="pnlStudentDetail" runat="server" style="width:100%; margin-top:30px" >
        <table style="width:100%" >
            <tr>
                <td style="text-align:left;font-weight:bold;">
                    Showing
                    <asp:Label ID="lblFromRec" runat="server" Text=""></asp:Label>
                    -
                    <asp:Label ID="lblToRec" runat="server" Text=""></asp:Label>
                    (out of
                    <asp:Label ID="lblTotalRec" runat="server" Text=""></asp:Label>
                    )
                </td>
                
                 <td style="text-align:right">
                     Page Size:&nbsp;
                     <asp:DropDownList runat="server" ID="ddlPageSize" CssClass="ftrFields" Width="45px" OnTextChanged="ddlPageSize_TextChanged" AutoPostBack="true">
                        <asp:ListItem Text="5" Value="5"></asp:ListItem>
                        <asp:ListItem Text="10" Value="10"></asp:ListItem>
                        <asp:ListItem Text="15" Value="15"></asp:ListItem>
                     </asp:DropDownList>
                 </td>
            </tr>
        </table>
        <div>
            <asp:Repeater ID="rptStudents" runat="server" OnItemDataBound="rptStudents_ItemDataBound">
                <HeaderTemplate>
                    <table style="width: 100%;margin-bottom:10px" class="tblShowData">
                      
                            <tr>
                                <th>Personal Detail</th>
                                <th>Contact Details</th>
                                <th>Gender</th>
                                <th>Address Detail</th>
                                <th>Room Detail</th>
                                <th>View Hotel Rent</th>
                                <th>View Mess Bill</th>
                                <th style="width:70px">Actions</th>
                            </tr>
                       
                </HeaderTemplate>

                <ItemTemplate>
                    <tr class="rowControl">
                        <!-- Student Name -->
                        <td>
                             <strong>Student Name:</strong>  <%# Eval("StudentName") %>
                            <br />
                              <strong>Date of Birth:</strong> <asp:Label ID="lblDob" runat="server" ></asp:Label>
                            <br />
                            <strong>CNIC:</strong>  <%# Eval("CNIC") %>
                           </td>

                        <!-- Contact Details -->
                        <td>
                            <strong>Phone:</strong> <%# Eval("PhoneNumber") %>
                            <br />
                            <strong>Email:</strong> <%# Eval("Email") %>
                        </td>

                        <!-- Gender -->
                        <td><%# Eval("Gender") %></td>

                        <!-- Address -->
                        <td>
                            <strong>Country:</strong> <%# Eval("Country") %>
                            <br />
                            <strong>State:</strong> <%# Eval("State") %>
                            <br />
                            <strong>Adress:</strong> <%# Eval("Address") %>
                        </td>

                        <!-- Room Details -->
                        <td>
                            <strong>Block No:</strong> <%# Eval("BlockNo") %>
                            <br />
                            <strong>Room No:</strong> <%# Eval("RoomNo") %>
                        </td>
                        <td>
                            <asp:HyperLink ID="hlViewHostelRent" style="text-decoration:none" runat="server" Text="Manage hostel Rent"></asp:HyperLink>
                        </td>
                        <td>
                            <asp:HyperLink ID="hlViewMessBill" style="text-decoration:none" runat="server" Text="Manage Mess Bill"></asp:HyperLink>
                        </td>

                        <!-- Actions -->
                        <td>
                            <asp:HyperLink ID="hlEdit" runat="server" >
                                <asp:Image ID="imgEdit" runat="server" Height="22px" ImageUrl="~/ImageIcons/EditProfile.svg" ToolTip="Edit the Student profile" />
                            </asp:HyperLink>
                            <asp:HyperLink ID="btnDelete" style="cursor:pointer" runat="server" CommandName="Delete" CommandArgument='<%# Eval("StudentID") %>' OnClick="btnDelete_Click" OnClientClick="return confirm('Are you sure you want to delete this student?');" >
                                <asp:Image ID="imgDelete" runat="server" Height="22px" ToolTip="Delete student." ImageUrl="~/ImageIcons/DeleteIcon.svg" />
                            </asp:HyperLink>
                        </td>
                    </tr>
                </ItemTemplate>

                <FooterTemplate>
                   
                 </table>
                </FooterTemplate>
            </asp:Repeater>

        </div>
    </asp:Panel>
    <asp:Panel ID="pnlNoRec" CssClass="pnlNoRecFound" runat="server" style="width:100%">
        <div>
            No records found.
        </div>
    </asp:Panel>
 <!-- Paging control -->
        
  <uc3:PagingUserControl ID="PagingUserControl1" runat="server" />

    <!-- Success/Error Message Panel -->
<ajaxToolkit:ModalPopupExtender 
    ID="mpePopup" 
    runat="server" 
    TargetControlID="lblDummy" 
    PopupControlID="pnlMessageDialog"  
    BackgroundCssClass="modal-background" PopupDragHandleControlID="dialog-header" OnCancelScript="btnCloseDialog_Click" />

<asp:Label ID="lblDummy" runat="server" Style="display: none"></asp:Label>
<asp:Panel ID="pnlMessageDialog" runat="server" CssClass="message-dialog" style="display:none">
    <div class="dialog-header">
        <asp:Label ID="lblDialogTitle" runat="server" Text="Success/Error" style="font-weight:bold"></asp:Label>

        
       
    </div>
    <div class="dialog-body">
        <asp:Label ID="lblDialogMessage" runat="server" Text="Message content goes here."></asp:Label>
    </div>
   <div style="display:flex;flex-direction:row;justify-content:center">
       <asp:Button ID="btnOk" runat="server" Width="80px" CssClass="btn btn-primary btn-sm" BackColor="#21479e" Text="Ok" />
   </div>
</asp:Panel>
    <script>
        $(document).ready(function () {
            $('#<%= ddlRoomNumber.ClientID %>').chosen({
                // Makes it responsive
                no_results_text: "No match found!", // Custom message for no results
                allow_single_deselect: true // Allows deselecting
            });
            $("#txtStudentName").on("keyup", function () {
                $.ajax({
                    type: "POST",
                    url: "/SampleServices/SampleServices.asmx/GetStudentNames",
                    data: JSON.stringify({ prefixText: $(this).val() }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        console.log(response.d); // چیک کریں کہ کیا ڈیٹا آ رہا ہے
                    },
                    error: function (xhr, status, error) {
                        console.error("Error:", xhr.responseText); // مکمل ایرر کنسول میں دکھائیں
                    }
                });
            });
           
        });
        function closePopup() {
            // Close the ModalPopupExtender using JavaScript
            var popup = $find('<%= mpePopup.ClientID %>');
            popup.hide();
            return false;
         }
    </script>
</asp:Content>
