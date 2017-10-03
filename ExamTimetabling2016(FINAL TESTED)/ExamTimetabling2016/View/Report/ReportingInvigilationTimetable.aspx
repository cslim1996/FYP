<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="ReportingInvigilationTimetable.aspx.cs" Inherits="ExamTimetabling2016.ReportingInvigilationTimetable" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript" src="../../Scripts/script.js"></script>
    <h2>Invigilation Timetable</h2>
    <asp:Button ID="btnInvTimetable" runat="server" Text="Generate Invigilation Timetable" OnClick="btnInvTimetable_Click" />
    <br />
    <br />
    <asp:Label ID="lblMsg" runat="server"></asp:Label>

    <div class="loading" align="center">
        Processing. Please wait.<br />
        Do not press anything.<br />
        <img src="../../images/loader.gif" alt="" />
    </div>
</asp:Content>
