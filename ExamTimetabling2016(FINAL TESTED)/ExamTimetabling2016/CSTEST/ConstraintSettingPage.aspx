<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="ConstraintSettingPage.aspx.cs" Inherits="ExamTimetabling2016.View.InvigilationMaintenance.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    a. Staff selection<br />
    <br />
    select table : 
    <asp:DropDownList ID="StaffTableDDL" runat="server" DataSourceID="SqlDataSource3" AppendDataBoundItems ="True" DataTextField="TABLE_NAME" DataValueField="TABLE_NAME" OnSelectedIndexChanged="StaffTableDDL_SelectedIndexChanged" AutoPostBack="True">
        <asp:ListItem Selected="True">Select An Table</asp:ListItem>
    </asp:DropDownList>
    <br />
    <br />
    select attribute for staff : 
    <asp:DropDownList ID="StaffDDL" runat="server" AppendDataBoundItems ="True" DataTextField="COLUMN_NAME" DataValueField="COLUMN_NAME" AutoPostBack="True">
        <asp:ListItem Selected="True">Select An Attribute</asp:ListItem>
    </asp:DropDownList>
    <br />
    <br />
    select operator :
    <asp:DropDownList ID="StaffOperatorDDL" runat="server" AutoPostBack="True">
        <asp:ListItem Selected="True">Select An Operator</asp:ListItem>
        <asp:ListItem Value="!=">Not Equals</asp:ListItem>
        <asp:ListItem Value="==">Equals</asp:ListItem>
        <asp:ListItem Value="&gt;=">More than or equals</asp:ListItem>
        <asp:ListItem Value="&gt;">More than</asp:ListItem>
        <asp:ListItem Value="&lt;=">Less than or equals</asp:ListItem>
        <asp:ListItem Value="&lt;">Less than</asp:ListItem>
        <asp:ListItem Value="&amp;&amp;">AND</asp:ListItem>
        <asp:ListItem Value="||">OR</asp:ListItem>
    </asp:DropDownList>
    <br />
    <br />
    insert value:
    <asp:TextBox ID="StaffValue" runat="server"></asp:TextBox>
    <br />
    <br />
    b&nbsp;
    Examination selection<br />
    Set the condition for the examination<br />
    <br />
    select table : 
    <asp:DropDownList ID="ExamTableDDL" runat="server" DataSourceID="SqlDataSource3" AppendDataBoundItems ="True" DataTextField="TABLE_NAME" DataValueField="TABLE_NAME" OnSelectedIndexChanged="ExamTableDDL_SelectedIndexChanged" AutoPostBack="True">
        <asp:ListItem Selected="True">Select a table</asp:ListItem>
    </asp:DropDownList>
    <br />
    <br />
    select attribute for examination : 
    <asp:DropDownList ID="ExamDDL" runat="server" AppendDataBoundItems ="True" DataTextField="COLUMN_NAME" DataValueField="COLUMN_NAME" AutoPostBack="True">
        <asp:ListItem Selected="True">Select An Attribute</asp:ListItem>
    </asp:DropDownList>
    <br />
    <br />
    select operator :
    <asp:DropDownList ID="OperatorDDL" runat="server" AutoPostBack="True">
        <asp:ListItem Selected="True">Select An Operator</asp:ListItem>
        <asp:ListItem Value="!=">Not Equals</asp:ListItem>
        <asp:ListItem Value="==">Equals</asp:ListItem>
        <asp:ListItem Value="&gt;=">More than or equals</asp:ListItem>
        <asp:ListItem Value="&gt;">More than</asp:ListItem>
        <asp:ListItem Value="&lt;=">Less than or equals</asp:ListItem>
        <asp:ListItem Value="&lt;">Less than</asp:ListItem>
        <asp:ListItem Value="&amp;&amp;">AND</asp:ListItem>
        <asp:ListItem Value="||">OR</asp:ListItem>
    </asp:DropDownList>
    <br />
    <br />
    insert value :<asp:TextBox ID="ExamValue" runat="server"></asp:TextBox>
    <br />
    <br />
    Set a condition for checking<br />
    <br />
    select an attribute for check<br />
    <br />
    select table instance :
    <br />
    <br />
    <br />
    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:ExamTimetableDBConnectionString %>" SelectCommand="select distinct * from @tablename">
        <SelectParameters>
            <asp:SessionParameter Name="tablename" SessionField="tableName" />
        </SelectParameters>
    </asp:SqlDataSource>
    <br />
    <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:ExamTimetableDBConnectionString %>" SelectCommand="SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES"></asp:SqlDataSource>
    <br />
    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Button" />
</asp:Content>
