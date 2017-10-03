<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="ExcelVenue.aspx.cs" Inherits="ExamTimetabling2016.View.ExcelVenue" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript" src="../../Scripts/script.js"></script>
    <div id="parentExcel">
        <div class="leftExcel">
            <h2>Data Import for Venue</h2>
        </div>
        <div class="rightExcel">
            <h2>User Guideline</h2>
            <asp:Label ID="lblGuideline" runat="server"></asp:Label>
        </div>
    </div>
    <br />
    <asp:Label ID="lblSelectFile" runat="server" Text="Select Files: "></asp:Label>&nbsp;&nbsp;<asp:FileUpload ID="FileUpload1" runat="server" />
    <br />
    <br />
    <asp:Button ID="btnImport" runat="server" Text="Import Data" OnClick="btnImport_Click" />
    <br />
    <br />
    <asp:Label ID="lblMsg" runat="server" Font-Bold="true"></asp:Label>

    <div class="loading" align="center">
        Processing. Please wait.<br />
        Do not press anything.<br />
        <img src="../../images/loader.gif" alt="" />
    </div>
</asp:Content>
