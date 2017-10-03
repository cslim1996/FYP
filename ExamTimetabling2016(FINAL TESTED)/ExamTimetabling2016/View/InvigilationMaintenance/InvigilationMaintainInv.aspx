<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="InvigilationMaintainInv.aspx.cs" Inherits="ExamTimetabling2016.View.InvigilationMaintainInv" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>User Guideline</h2>
    <asp:Label ID="lblGuideline" runat="server"></asp:Label>
    <div id="staffDetails">
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConflictDetection="CompareAllValues" ConnectionString="<%$ ConnectionStrings:ExamTimetableDBConnectionString %>" DeleteCommand="DELETE FROM [Staff] WHERE [StaffID] = @original_StaffID AND (([Title] = @original_Title) OR ([Title] IS NULL AND @original_Title IS NULL)) AND (([Name] = @original_Name) OR ([Name] IS NULL AND @original_Name IS NULL)) AND (([Gender] = @original_Gender) OR ([Gender] IS NULL AND @original_Gender IS NULL)) AND (([Position] = @original_Position) OR ([Position] IS NULL AND @original_Position IS NULL)) AND (([Department] = @original_Department) OR ([Department] IS NULL AND @original_Department IS NULL)) AND (([isMuslim] = @original_isMuslim) OR ([isMuslim] IS NULL AND @original_isMuslim IS NULL)) AND (([isChiefInvi] = @original_isChiefInvi) OR ([isChiefInvi] IS NULL AND @original_isChiefInvi IS NULL)) AND (([isInviAbove2Years] = @original_isInviAbove2Years) OR ([isInviAbove2Years] IS NULL AND @original_isInviAbove2Years IS NULL))" InsertCommand="INSERT INTO [Staff] ([Title], [Name], [Gender], [Position], [Department], [isMuslim], [isChiefInvi], [isInviAbove2Years]) VALUES (@Title, @Name, @Gender, @Position, @Department, @isMuslim, @isChiefInvi, @isInviAbove2Years)" OldValuesParameterFormatString="original_{0}" SelectCommand="SELECT [StaffID], [Title], [Name], [Gender], [Position], [Department], [isMuslim], [isChiefInvi], [isInviAbove2Years] FROM [Staff] ORDER BY [Name]" UpdateCommand="UPDATE [Staff] SET [Title] = @Title, [Name] = @Name, [Gender] = @Gender, [Position] = @Position, [Department] = @Department, [isMuslim] = @isMuslim, [isChiefInvi] = @isChiefInvi, [isInviAbove2Years] = @isInviAbove2Years WHERE [StaffID] = @original_StaffID AND (([Title] = @original_Title) OR ([Title] IS NULL AND @original_Title IS NULL)) AND (([Name] = @original_Name) OR ([Name] IS NULL AND @original_Name IS NULL)) AND (([Gender] = @original_Gender) OR ([Gender] IS NULL AND @original_Gender IS NULL)) AND (([Position] = @original_Position) OR ([Position] IS NULL AND @original_Position IS NULL)) AND (([Department] = @original_Department) OR ([Department] IS NULL AND @original_Department IS NULL)) AND (([isMuslim] = @original_isMuslim) OR ([isMuslim] IS NULL AND @original_isMuslim IS NULL)) AND (([isChiefInvi] = @original_isChiefInvi) OR ([isChiefInvi] IS NULL AND @original_isChiefInvi IS NULL)) AND (([isInviAbove2Years] = @original_isInviAbove2Years) OR ([isInviAbove2Years] IS NULL AND @original_isInviAbove2Years IS NULL))">
            <DeleteParameters>
                <asp:Parameter Name="original_StaffID" Type="Int32" />
                <asp:Parameter Name="original_Title" Type="String" />
                <asp:Parameter Name="original_Name" Type="String" />
                <asp:Parameter Name="original_Gender" Type="String" />
                <asp:Parameter Name="original_Position" Type="String" />
                <asp:Parameter Name="original_Department" Type="String" />
                <asp:Parameter Name="original_isMuslim" Type="String" />
                <asp:Parameter Name="original_isChiefInvi" Type="String" />
                <asp:Parameter Name="original_isInviAbove2Years" Type="String" />
            </DeleteParameters>
            <InsertParameters>
                <asp:Parameter Name="Title" Type="String" />
                <asp:Parameter Name="Name" Type="String" />
                <asp:Parameter Name="Gender" Type="String" />
                <asp:Parameter Name="Position" Type="String" />
                <asp:Parameter Name="Department" Type="String" />
                <asp:Parameter Name="isMuslim" Type="String" />
                <asp:Parameter Name="isChiefInvi" Type="String" />
                <asp:Parameter Name="isInviAbove2Years" Type="String" />
            </InsertParameters>
            <UpdateParameters>
                <asp:Parameter Name="Title" Type="String" />
                <asp:Parameter Name="Name" Type="String" />
                <asp:Parameter Name="Gender" Type="String" />
                <asp:Parameter Name="Position" Type="String" />
                <asp:Parameter Name="Department" Type="String" />
                <asp:Parameter Name="isMuslim" Type="String" />
                <asp:Parameter Name="isChiefInvi" Type="String" />
                <asp:Parameter Name="isInviAbove2Years" Type="String" />
                <asp:Parameter Name="original_StaffID" Type="Int32" />
                <asp:Parameter Name="original_Title" Type="String" />
                <asp:Parameter Name="original_Name" Type="String" />
                <asp:Parameter Name="original_Gender" Type="String" />
                <asp:Parameter Name="original_Position" Type="String" />
                <asp:Parameter Name="original_Department" Type="String" />
                <asp:Parameter Name="original_isMuslim" Type="String" />
                <asp:Parameter Name="original_isChiefInvi" Type="String" />
                <asp:Parameter Name="original_isInviAbove2Years" Type="String" />
            </UpdateParameters>
        </asp:SqlDataSource>
        <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:ExamTimetableDBConnectionString %>" DeleteCommand="DELETE FROM [Exemption] WHERE [ExemptionID] = @original_ExemptionID" InsertCommand="INSERT INTO [Exemption] ([StaffID], [Date], [Session]) VALUES (@StaffID, @Date, @Session)" OldValuesParameterFormatString="original_{0}" SelectCommand="SELECT [ExemptionID], [StaffID], [Date], [Session] FROM [Exemption] WHERE ([StaffID] = @StaffID)" UpdateCommand="UPDATE [Exemption] SET [StaffID] = @StaffID, [Date] = @Date, [Session] = @Session WHERE [ExemptionID] = @original_ExemptionID">
            <SelectParameters>
                <asp:SessionParameter Name="StaffID" SessionField="StaffID" DefaultValue="0" Type="Int32" />
            </SelectParameters>
            <DeleteParameters>
                <asp:Parameter Name="original_ExemptionID" Type="Int32" />
            </DeleteParameters>
            <InsertParameters>
                <asp:SessionParameter Name="StaffID" SessionField="StaffID" DefaultValue="0" Type="Int32" />
                <asp:Parameter DbType="Date" Name="Date" />
                <asp:Parameter Name="Session" Type="String" />
            </InsertParameters>
            <UpdateParameters>
                <asp:Parameter Name="StaffID" Type="Int32" />
                <asp:Parameter DbType="Date" Name="Date" />
                <asp:Parameter Name="Session" Type="String" />
                <asp:Parameter Name="original_ExemptionID" Type="Int32" />
            </UpdateParameters>
        </asp:SqlDataSource>
        <br />

        <h2>Staff Details</h2>
        <asp:ListView ID="ListView1" runat="server" DataKeyNames="StaffID" DataSourceID="SqlDataSource1" Style="margin-bottom: 0px" OnSelectedIndexChanged="ListView1_SelectedIndexChanged">
            <AlternatingItemTemplate>
                <tr style="background-color: #FAFAD2; color: #284775;">
                    <td>
                        <asp:Button ID="EditButton" runat="server" CausesValidation="false" CommandName="Edit" Text="Edit" />
                    </td>
                    <td>
                        <asp:LinkButton ID="StaffIDLabel" runat="server" CausesValidation="false" CommandName="Select" Text='<%# Eval("StaffID") %>' />
                    </td>
                    <td>
                        <asp:Label ID="TitleLabel" runat="server" Text='<%# Eval("Title") %>' />
                    </td>
                    <td>
                        <asp:Label ID="NameLabel" runat="server" Text='<%# Eval("Name") %>' />
                    </td>
                    <td>
                        <asp:Label ID="GenderLabel" runat="server" Text='<%# Eval("Gender") %>' />
                    </td>
                    <td>
                        <asp:Label ID="PositionLabel" runat="server" Text='<%# Eval("Position") %>' />
                    </td>
                    <td>
                        <asp:Label ID="DepartmentLabel" runat="server" Text='<%# Eval("Department") %>' />
                    </td>
                    <td>
                        <asp:Label ID="isMuslimLabel" runat="server" Text='<%# Eval("isMuslim") %>' />
                    </td>
                    <td>
                        <asp:Label ID="isChiefInviLabel" runat="server" Text='<%# Eval("isChiefInvi") %>' />
                    </td>
                    <td>
                        <asp:Label ID="isInviAbove2YearsLabel" runat="server" Text='<%# Eval("isInviAbove2Years") %>' />
                    </td>
                </tr>
            </AlternatingItemTemplate>
            <EditItemTemplate>
                <tr style="background-color: #FFCC66; color: #000080">
                    <td>
                        <asp:Button ID="UpdateButton" runat="server" ValidationGroup="StaffGroup" CommandName="Update" Text="Update" />
                        <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" Text="Cancel" />
                    </td>
                    <td>
                        <asp:Label ID="StaffIDLabel1" runat="server" Width="45px" Text='<%# Eval("StaffID") %>' />
                    </td>
                    <td>
                        <asp:TextBox ID="TitleTextBox" runat="server" Width="40px" Text='<%# Bind("Title") %>' /><br />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorTitle" ValidationGroup="StaffGroup" runat="server" ControlToValidate="TitleTextBox" ErrorMessage="Title cannot be empty" Text="*" ForeColor="Red"></asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <asp:TextBox ID="NameTextBox" runat="server" Width="400px" Text='<%# Bind("Name") %>' /><br />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorName" ValidationGroup="StaffGroup" runat="server" ControlToValidate="NameTextBox" ErrorMessage="Name cannot be empty" Text="*" ForeColor="Red"></asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <asp:DropDownList ID="GenderList" runat="server" Width="65px" Text='<%# Bind("Gender") %>'>
                            <asp:ListItem>M</asp:ListItem>
                            <asp:ListItem>F</asp:ListItem>
                        </asp:DropDownList><br /><br />
                    </td>
                    <td>
                        <asp:TextBox ID="PositionTextBox" runat="server" Width="180px" Text='<%# Bind("Position") %>' /><br />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorPosition" ValidationGroup="StaffGroup" runat="server" ControlToValidate="PositionTextBox" ErrorMessage="Position cannot be empty" Text="*" ForeColor="Red"></asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <asp:TextBox ID="DepartmentTextBox" runat="server" Width="200px" Text='<%# Bind("Department") %>' /><br />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorDepartment" ValidationGroup="StaffGroup" runat="server" ControlToValidate="DepartmentTextBox" ErrorMessage="Department cannot be empty" Text="*" ForeColor="Red"></asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <asp:DropDownList ID="isMuslimList" runat="server" Width="60px" Text='<%# Bind("isMuslim") %>'>
                            <asp:ListItem>Y</asp:ListItem>
                            <asp:ListItem>N</asp:ListItem>
                        </asp:DropDownList><br /><br />
                    </td>
                    <td>
                        <asp:DropDownList ID="isChiefList" runat="server" Width="90px" Text='<%# Bind("isChiefInvi") %>'>
                            <asp:ListItem>Y</asp:ListItem>
                            <asp:ListItem>N</asp:ListItem>
                        </asp:DropDownList><br /><br />   
                    </td>
                    <td>
                        <asp:DropDownList ID="isInviAbove2YearsList" runat="server" Width="100px" Text='<%# Bind("isInviAbove2Years") %>'>
                            <asp:ListItem>Y</asp:ListItem>
                            <asp:ListItem>N</asp:ListItem>
                        </asp:DropDownList><br /><br />   
                    </td>
                </tr>
            </EditItemTemplate>
            <EmptyDataTemplate>
                <table runat="server" style="background-color: #FFFFFF; border-collapse: collapse; border-color: #999999; border-style: none; border-width: 1px;">
                    <tr>
                        <td>No data record.</td>
                    </tr>
                </table>
            </EmptyDataTemplate>
            <InsertItemTemplate>
                <tr>
                    <td>
                        <asp:Button ID="InsertButton" runat="server" CommandName="Insert" Text="Insert" />
                        <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" Text="Clear" />
                    </td>
                    <td>&nbsp;</td>
                    <td>
                        <asp:TextBox ID="TitleTextBox" runat="server" Text='<%# Bind("Title") %>' />
                    </td>
                    <td>
                        <asp:TextBox ID="NameTextBox" runat="server" Text='<%# Bind("Name") %>' />
                    </td>
                    <td>
                        <asp:TextBox ID="GenderTextBox" runat="server" Text='<%# Bind("Gender") %>' />
                    </td>
                    <td9
                        <asp:TextBox ID="PositionTextBox" runat="server" Text='<%# Bind("Position") %>' />
                    </td>
                    <td>
                        <asp:TextBox ID="DepartmentTextBox" runat="server" Text='<%# Bind("Department") %>' />
                    </td>
                    <td>
                        <asp:TextBox ID="isMuslimTextBox" runat="server" Text='<%# Bind("isMuslim") %>' />
                    </td>
                    <td>
                        <asp:TextBox ID="isChiefInviTextBox" runat="server" Text='<%# Bind("isChiefInvi") %>' />
                    </td>
                    <td>
                        <asp:TextBox ID="isInviAbove2YearsTextBox" runat="server" Text='<%# Bind("isInviAbove2Years") %>' />
                    </td>
                </tr>
            </InsertItemTemplate>
            <ItemTemplate>
                <tr style="background-color: #FFFBD6; color: #333333;">
                    <td>
                        <asp:Button ID="EditButton" runat="server" CommandName="Edit" Text="Edit" />
                    </td>
                    <td>
                        <asp:LinkButton ID="StaffIDLabel" runat="server" CommandName="Select" Text='<%# Eval("StaffID") %>' />
                    </td>
                    <td>
                        <asp:Label ID="TitleLabel" runat="server" Text='<%# Eval("Title") %>' />
                    </td>
                    <td>
                        <asp:Label ID="NameLabel" runat="server" Text='<%# Eval("Name") %>' />
                    </td>
                    <td>
                        <asp:Label ID="GenderLabel" runat="server" Text='<%# Eval("Gender") %>' />
                    </td>
                    <td>
                        <asp:Label ID="PositionLabel" runat="server" Text='<%# Eval("Position") %>' />
                    </td>
                    <td>
                        <asp:Label ID="DepartmentLabel" runat="server" Text='<%# Eval("Department") %>' />
                    </td>
                    <td>
                        <asp:Label ID="isMuslimLabel" runat="server" Text='<%# Eval("isMuslim") %>' />
                    </td>
                    <td>
                        <asp:Label ID="isChiefInviLabel" runat="server" Text='<%# Eval("isChiefInvi") %>' />
                    </td>
                    <td>
                        <asp:Label ID="isInviAbove2YearsLabel" runat="server" Text='<%# Eval("isInviAbove2Years") %>' />
                    </td>
                </tr>
            </ItemTemplate>
            <LayoutTemplate>
                <table runat="server">
                    <tr runat="server">
                        <td runat="server">
                            <table id="itemPlaceholderContainer" runat="server" border="1" style="background-color: #FFFFFF; border-collapse: collapse; border-color: #999999; border-style: none; border-width: 1px; font-family: Verdana, Arial, Helvetica, sans-serif;">
                                <tr runat="server" style="background-color: #FFFBD6; color: #333333;">
                                    <th runat="server" style="width:50px"></th>
                                    <th runat="server" style="width:45px">ID</th>
                                    <th runat="server" style="width:40px">Title</th>
                                    <th runat="server" style="width:400px">Name</th>
                                    <th runat="server" style="width:65px">Gender</th>
                                    <th runat="server" style="width:180px">Position</th>
                                    <th runat="server" style="width:200px">Department</th>
                                    <th runat="server" style="width:60px">Muslim</th>
                                    <th runat="server" style="width:90px">Chief Invigilator</th>
                                    <th runat="server" style="width:100px">Invigilator Above 2 Years in TARUC</th>
                                </tr>
                                <tr id="itemPlaceholder" runat="server">
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr runat="server">
                        <td runat="server" style="text-align: center; background-color: #FFCC66; font-family: Verdana, Arial, Helvetica, sans-serif; color: #333333;">
                            <asp:DataPager ID="DataPager1" runat="server">
                                <Fields>
                                    <asp:NextPreviousPagerField ButtonType="Button" ShowFirstPageButton="True" ShowNextPageButton="False" ShowPreviousPageButton="False" />
                                    <asp:NumericPagerField />
                                    <asp:NextPreviousPagerField ButtonType="Button" ShowLastPageButton="True" ShowNextPageButton="False" ShowPreviousPageButton="False" />
                                </Fields>
                            </asp:DataPager>
                        </td>
                    </tr>
                </table>
            </LayoutTemplate>
            <SelectedItemTemplate>
                <tr style="background-color: #FFCC66; font-weight: bold; color: #000080;">
                    <td>
                        <asp:Button ID="EditButton" runat="server" CommandName="Edit" Text="Edit" />
                    </td>
                    <td>
                        <asp:Label ID="StaffIDLabel" runat="server" Text='<%# Eval("StaffID") %>' />
                    </td>
                    <td>
                        <asp:Label ID="TitleLabel" runat="server" Text='<%# Eval("Title") %>' />
                    </td>
                    <td>
                        <asp:Label ID="NameLabel" runat="server" Text='<%# Eval("Name") %>' />
                    </td>
                    <td>
                        <asp:Label ID="GenderLabel" runat="server" Text='<%# Eval("Gender") %>' />
                    </td>
                    <td>
                        <asp:Label ID="PositionLabel" runat="server" Text='<%# Eval("Position") %>' />
                    </td>
                    <td>
                        <asp:Label ID="DepartmentLabel" runat="server" Text='<%# Eval("Department") %>' />
                    </td>
                    <td>
                        <asp:Label ID="isMuslimLabel" runat="server" Text='<%# Eval("isMuslim") %>' />
                    </td>
                    <td>
                        <asp:Label ID="isChiefInviLabel" runat="server" Text='<%# Eval("isChiefInvi") %>' />
                    </td>
                    <td>
                        <asp:Label ID="isInviAbove2YearsLabel" runat="server" Text='<%# Eval("isInviAbove2Years") %>' />
                    </td>
                </tr>
            </SelectedItemTemplate>
        </asp:ListView>

        <br />
        <h2>Exemption Details</h2>
        <asp:ListView ID="ListView2" runat="server" DataKeyNames="ExemptionID" DataSourceID="SqlDataSource2" InsertItemPosition="LastItem">
            <AlternatingItemTemplate>
                <tr style="background-color: #FFF8DC;">
                    <td>
                        <asp:Button ID="DeleteButton" runat="server" CommandName="Delete" Text="Delete" />
                        <asp:Button ID="EditButton" runat="server" CommandName="Edit" Text="Edit" />
                    </td>
                    <td>
                        <asp:Label ID="ExemptionIDLabel" runat="server" Text='<%# Eval("ExemptionID") %>' />
                    </td>
                    <td>
                        <asp:Label ID="StaffIDLabel" runat="server" Text='<%# Eval("StaffID") %>' />
                    </td>
                    <td>
                        <asp:Label ID="DateLabel" runat="server" Text='<%# Eval("Date","{0:dd/MM/yyyy}") %>' />
                    </td>
                    <td>
                        <asp:Label ID="SessionLabel" runat="server" Text='<%# Eval("Session") %>' />
                    </td>
                </tr>
            </AlternatingItemTemplate>
            <EditItemTemplate>
                <tr style="background-color: #008A8C; color:black;">
                    <td>
                        <asp:Button ID="UpdateButton" runat="server" ValidationGroup="ExemptionGroup2" CommandName="Update" Text="Update" />
                        <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" Text="Cancel" />
                    </td>
                    <td>
                        <asp:Label ID="ExemptionIDLabel1" runat="server" Text='<%# Eval("ExemptionID") %>' />
                    </td>
                    <td>
                        <asp:TextBox ID="StaffIDTextBox" runat="server" Text='<%# Bind("StaffID") %>' Enabled="false" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorStaffID" runat="server" ValidationGroup="ExemptionGroup2" ErrorMessage="Staff must be selected" ForeColor="Red" ControlToValidate="StaffIDTextBox" Text="*"></asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <asp:TextBox ID="DateTextBox" runat="server" Text='<%# Bind("Date","{0:dd/MM/yyyy}") %>' />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorDate" runat="server" ValidationGroup="ExemptionGroup2" ErrorMessage="Date cannot be empty" ForeColor="Red" ControlToValidate="DateTextBox" Text="*"></asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="CompareValidatorDate" runat="server" ValidationGroup="ExemptionGroup2" Operator="DataTypeCheck" Type="Date" ControlToValidate="DateTextBox" ForeColor="Red" ErrorMessage="Please enter date in format dd/mm/yyyy" Text="*"></asp:CompareValidator>
                    </td>
                    <td>
                        <asp:DropDownList ID="SessionList" runat="server" Width="90px" Text='<%# Bind("Session") %>'>
                            <asp:ListItem>AM</asp:ListItem>
                            <asp:ListItem>PM</asp:ListItem>
                            <asp:ListItem>EV</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
            </EditItemTemplate>
            <EmptyDataTemplate>
                <table runat="server" style="background-color: #FFFFFF; border-collapse: collapse; border-color: #999999; border-style: none; border-width: 1px;">
                    <tr>
                        <td>No exemption record.</td>
                    </tr>
                </table>
            </EmptyDataTemplate>
            <InsertItemTemplate>
                <tr style="">
                    <td>
                        <asp:Button ID="InsertButton" runat="server" ValidationGroup="ExemptionGroup" CommandName="Insert" Text="Insert" />
                        <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" Text="Clear" />
                    </td>
                    <td>&nbsp;</td>
                    <td>
                        <asp:TextBox ID="StaffIDTextBox" runat="server" Text='<%# Session["StaffID"] %>' Enabled="false" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorStaffID" runat="server" ValidationGroup="ExemptionGroup" ErrorMessage="Staff must be selected" ForeColor="Red" ControlToValidate="StaffIDTextBox" Text="*"></asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <asp:TextBox ID="DateTextBox" runat="server" Text='<%# Bind("Date") %>' />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorDate" runat="server" ValidationGroup="ExemptionGroup" ErrorMessage="Date cannot be empty" ForeColor="Red" ControlToValidate="DateTextBox" Text="*"></asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="CompareValidatorDate" runat="server" ValidationGroup="ExemptionGroup" Operator="DataTypeCheck" Type="Date" ControlToValidate="DateTextBox" ForeColor="Red" ErrorMessage="Please enter date in format dd/mm/yyyy" Text="*"></asp:CompareValidator>
                    </td>
                    <td>
                        <asp:DropDownList ID="SessionList" runat="server" Width="90px" SelectedValue= '<%# Bind("Session") %>'>
                            <asp:ListItem>AM</asp:ListItem>
                            <asp:ListItem>PM</asp:ListItem>
                            <asp:ListItem>EV</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
            </InsertItemTemplate>
            <ItemTemplate>
                <tr style="background-color: #DCDCDC; color: #000000;">
                    <td>
                        <asp:Button ID="DeleteButton" runat="server" CausesValidation="false" CommandName="Delete" Text="Delete" />
                        <asp:Button ID="EditButton" runat="server" CausesValidation="false" CommandName="Edit" Text="Edit" />
                    </td>
                    <td>
                        <asp:Label ID="ExemptionIDLabel" runat="server" Text='<%# Eval("ExemptionID") %>' />
                    </td>
                    <td>
                        <asp:Label ID="StaffIDLabel" runat="server" Text='<%# Eval("StaffID") %>' />
                    </td>
                    <td>
                        <asp:Label ID="DateLabel" runat="server" Text='<%# Eval("Date","{0:dd/MM/yyyy}") %>' />
                    </td>
                    <td>
                        <asp:Label ID="SessionLabel" runat="server" Text='<%# Eval("Session") %>' />
                    </td>
                </tr>
            </ItemTemplate>
            <LayoutTemplate>
                <table runat="server">
                    <tr runat="server">
                        <td runat="server">
                            <table id="itemPlaceholderContainer" runat="server" border="1" style="background-color: #FFFFFF; border-collapse: collapse; border-color: #999999; border-style: none; border-width: 1px; font-family: Verdana, Arial, Helvetica, sans-serif;">
                                <tr runat="server" style="background-color: #DCDCDC; color: #000000;">
                                    <th runat="server"></th>
                                    <th runat="server">ExemptionID</th>
                                    <th runat="server">StaffID</th>
                                    <th runat="server">Date (dd/mm/yyyy)</th>
                                    <th runat="server">Session</th>
                                </tr>
                                <tr id="itemPlaceholder" runat="server">
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr runat="server">
                        <td runat="server" style="text-align: center; background-color: #CCCCCC; font-family: Verdana, Arial, Helvetica, sans-serif; color: #000000;">
                            <asp:DataPager ID="DataPager1" runat="server">
                                <Fields>
                                    <asp:NextPreviousPagerField ButtonType="Button" ShowFirstPageButton="True" ShowNextPageButton="False" ShowPreviousPageButton="False" />
                                    <asp:NumericPagerField />
                                    <asp:NextPreviousPagerField ButtonType="Button" ShowLastPageButton="True" ShowNextPageButton="False" ShowPreviousPageButton="False" />
                                </Fields>
                            </asp:DataPager>
                        </td>
                    </tr>
                </table>
            </LayoutTemplate>
            <SelectedItemTemplate>
                <tr style="background-color: #008A8C; font-weight: bold; color: #FFFFFF;">
                    <td>
                        <asp:Button ID="DeleteButton" runat="server" CausesValidation="false" CommandName="Delete" Text="Delete" />
                        <asp:Button ID="EditButton" runat="server" CausesValidation="false" CommandName="Edit" Text="Edit" />
                    </td>
                    <td>
                        <asp:Label ID="ExemptionIDLabel" runat="server" Text='<%# Eval("ExemptionID") %>' />
                    </td>
                    <td>
                        <asp:Label ID="StaffIDLabel" runat="server" Text='<%# Eval("StaffID") %>' />
                    </td>
                    <td>
                        <asp:Label ID="DateLabel" runat="server" Text='<%# Eval("Date") %>' />
                    </td>
                    <td>
                        <asp:Label ID="SessionLabel" runat="server" Text='<%# Eval("Session") %>' />
                    </td>
                </tr>
            </SelectedItemTemplate>
        </asp:ListView>
        <br /><br />
        <asp:ValidationSummary ID="ValidationSummary1" ValidationGroup="ExemptionGroup" runat="server" />
        <asp:ValidationSummary ID="ValidationSummary2" ValidationGroup="StaffGroup" runat="server" Height="38px" />
        <asp:ValidationSummary ID="ValidationSummary3" ValidationGroup="ExemptionGroup2" runat="server" Height="38px" />
        <br /><br /><br />
    </div>
</asp:Content>
