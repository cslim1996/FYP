<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="CsInvigilationTable.aspx.cs" Inherits="ExamTimetabling2016.CSTEST.CsInvigilationTable" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript" src="../../Scripts/script.js"></script>
    <h2>Planning of invigilation duty</h2>
    <p>
        <asp:Button ID="btnPlan" runat="server" Text="Plan" OnClick="btnPlan_Click" />
    </p>
    <p>
        <asp:Label ID="lblMsg" runat="server"></asp:Label>
    </p>
    <p>
        <asp:Label ID="Label1" runat="server"></asp:Label>
        
    <div class="loading" align="center">
        Processing. Please wait.<br />
        Do not press anything.<br />
        <img src="../../images/loader.gif" alt="" />
    </p>
</asp:Content>