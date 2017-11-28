<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="ViewConstraints.aspx.cs" Inherits="ExamTimetabling2016.View.InvigilationMaintenance.ViewConstraints" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style3 {
            font-size: medium;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h1>View Constraints </h1>
    <p>
        
    </p>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="constraintID" DataSourceID="SqlDataSource1" OnRowCommand="GridView1_RowCommand" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" Width="80%">
                <Columns>
                    <asp:ButtonField ButtonType="Button" CommandName="Select" Text="Select">
                    <HeaderStyle Width="50px" />
                    </asp:ButtonField>
                    <asp:ButtonField ButtonType="Button" CommandName="Delete" Text="Delete">
                    <HeaderStyle Width="50px" />
                    </asp:ButtonField>
                    <asp:BoundField DataField="constraintID" HeaderText="ID" InsertVisible="False" ReadOnly="True" SortExpression="constraintID" />
                    <asp:BoundField DataField="remark" HeaderText="Description" SortExpression="remark" />
                </Columns>
            </asp:GridView>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ExamTimetablingConnectionString %>" DeleteCommand="delete from dbo.constraints where constraintid = @id" OnSelecting="SqlDataSource1_Selecting" SelectCommand="select constraintID, remark from dbo.Constraints">
                <DeleteParameters>
                    <asp:Parameter Name="ID" Type="Int32" />
                </DeleteParameters>
            </asp:SqlDataSource>
            <br />
            <asp:Label ID="lblDetail" runat="server" CssClass="auto-style3" Text="Constraint Details" Visible="False"></asp:Label>
            <br />
            <asp:DetailsView ID="DetailsView1" runat="server" AutoGenerateRows="False" DataKeyNames="ConstraintID" DataSourceID="SqlDataSource2" EnableViewState="False" Height="50px" Visible="False" Width="125px">
                <Fields>
                    <asp:BoundField DataField="ConstraintID" HeaderText="ConstraintID" InsertVisible="False" ReadOnly="True" SortExpression="ConstraintID" />
                    <asp:BoundField DataField="IsHardConstraint" HeaderText="IsHardConstraint" SortExpression="IsHardConstraint" />
                    <asp:BoundField DataField="IsCnblPaper" HeaderText="IsCnblPaper" SortExpression="IsCnblPaper" />
                    <asp:BoundField DataField="IsDoubleSeating" HeaderText="IsDoubleSeating" SortExpression="IsDoubleSeating" />
                    <asp:BoundField DataField="MinExperiencedInvigilatorPerVenue" HeaderText="MinExperiencedInvigilatorPerVenue" SortExpression="MinExperiencedInvigilatorPerVenue" />
                    <asp:BoundField DataField="ConstraintImportanceValue" HeaderText="ConstraintImportanceValue" SortExpression="ConstraintImportanceValue" />
                    <asp:BoundField DataField="HasOtherDutyOnSameDay" HeaderText="HasOtherDutyOnSameDay" SortExpression="HasOtherDutyOnSameDay" />
                    <asp:BoundField DataField="HasSpecificSessionDutyOnSameDay" HeaderText="HasSpecificSessionDutyOnSameDay" SortExpression="HasSpecificSessionDutyOnSameDay" />
                    <asp:BoundField DataField="HasSpecificDurationDutyOnSameDay" HeaderText="HasSpecificDurationDutyOnSameDay" SortExpression="HasSpecificDurationDutyOnSameDay" />
                    <asp:BoundField DataField="HasSpecificSessionAndDurationDutyOnSameDay" HeaderText="HasSpecificSessionAndDurationDutyOnSameDay" SortExpression="HasSpecificSessionAndDurationDutyOnSameDay" />
                    <asp:BoundField DataField="HasSpecificSessionString" HeaderText="HasSpecificSessionString" SortExpression="HasSpecificSessionString" />
                    <asp:BoundField DataField="HasSpecificDurationInt" HeaderText="HasSpecificDurationInt" SortExpression="HasSpecificDurationInt" />
                    <asp:BoundField DataField="DayOfWeek" HeaderText="DayOfWeek" SortExpression="DayOfWeek" />
                    <asp:BoundField DataField="StaffIsMuslim" HeaderText="StaffIsMuslim" SortExpression="StaffIsMuslim" />
                    <asp:BoundField DataField="StaffFacultyCode" HeaderText="StaffFacultyCode" SortExpression="StaffFacultyCode" />
                    <asp:BoundField DataField="StaffIsInviAbove2Years" HeaderText="StaffIsInviAbove2Years" SortExpression="StaffIsInviAbove2Years" />
                    <asp:BoundField DataField="StaffIsChiefInvi" HeaderText="StaffIsChiefInvi" SortExpression="StaffIsChiefInvi" />
                    <asp:BoundField DataField="StaffIsTakingSTSPhd" HeaderText="StaffIsTakingSTSPhd" SortExpression="StaffIsTakingSTSPhd" />
                    <asp:BoundField DataField="StaffTypeOfEmploy" HeaderText="StaffTypeOfEmploy" SortExpression="StaffTypeOfEmploy" />
                    <asp:BoundField DataField="ExamFacultyCode" HeaderText="ExamFacultyCode" SortExpression="ExamFacultyCode" />
                    <asp:BoundField DataField="ExamPaperType" HeaderText="ExamPaperType" SortExpression="ExamPaperType" />
                    <asp:BoundField DataField="CategoryOfInvigilationDuty" HeaderText="CategoryOfInvigilationDuty" SortExpression="CategoryOfInvigilationDuty" />
                    <asp:BoundField DataField="DurationOfInvigilationDuty" HeaderText="DurationOfInvigilationDuty" SortExpression="DurationOfInvigilationDuty" />
                    <asp:BoundField DataField="LocationOfInvigilationDuty" HeaderText="LocationOfInvigilationDuty" SortExpression="LocationOfInvigilationDuty" />
                    <asp:BoundField DataField="SessionOfInvigilationDuty" HeaderText="SessionOfInvigilationDuty" SortExpression="SessionOfInvigilationDuty" />
                    <asp:BoundField DataField="Remark" HeaderText="Remark" SortExpression="Remark" />
                </Fields>
            </asp:DetailsView>
            <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:ExamTimetablingConnectionString %>" SelectCommand="select * from dbo.constraints where constraintid = @id">
                <SelectParameters>
                    <asp:SessionParameter DefaultValue="1" Name="id" SessionField="id" />
                </SelectParameters>
            </asp:SqlDataSource>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
