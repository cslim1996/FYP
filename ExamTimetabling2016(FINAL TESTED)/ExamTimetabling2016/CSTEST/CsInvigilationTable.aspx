<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="CsInvigilationTable.aspx.cs" Inherits="ExamTimetabling2016.CSTEST.CsInvigilationTable" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Planning of invigilation duty</h2>
    <p>
        <asp:Button ID="btnPlan" runat="server" Text="Plan" OnClick="btnPlan_Click" />
    </p>
    <p>
        <asp:Label ID="lblMsg" runat="server">Plan</asp:Label>
    </p>
    <p>
        <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
    </p>
</asp:Content>
