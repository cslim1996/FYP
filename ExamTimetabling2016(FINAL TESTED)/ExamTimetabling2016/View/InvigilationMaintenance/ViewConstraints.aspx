<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="ViewConstraints.aspx.cs" Inherits="ExamTimetabling2016.View.InvigilationMaintenance.ViewConstraints" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
