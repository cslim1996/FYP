<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="ExaminationSetting.aspx.cs" Inherits="ExamTimetabling2016.View.InvigilationMaintenance.ExaminationSetting" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        
        .auto-style8 {
            width: 50%;
            height: 22px;
            padding-top: 7px;
            padding-left: 10px;
            padding-bottom: 7px;
        }
        .auto-style9 {
            height: 22px;
            padding-top: 7px;
            padding-left: 10px;
            padding-bottom: 7px;
            text-align: center;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Examination Setting</h2>
    <table class="nav-justified" border="1">
        <tr>
            <td class="auto-style8">Assign To Examiner</td>
            <td class="auto-style8">
                <asp:DropDownList ID="ddlAssignToExaminer" runat="server">
                    <asp:ListItem Selected="True">Yes</asp:ListItem>
                    <asp:ListItem>No</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="auto-style8">Maximum Count Of Extra Session</td>
            <td class="auto-style8">
                <asp:TextBox ID="tbExtra" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="auto-style8">Maximum Count Of Evening Session</td>
            <td class="auto-style8">
                <asp:TextBox ID="tbEvening" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="auto-style8">Maximum Count Of Saturday Sesion </td>
            <td class="auto-style8">
                <asp:TextBox ID="tbSaturday" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="auto-style8">Maximum Count Of Relief Session</td>
            <td class="auto-style8">
                <asp:TextBox ID="tbRelief" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="auto-style8">Day of Exemption For Examiner</td>
            <td class="auto-style8">
                <asp:TextBox ID="tbExemptionForExaminer" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="auto-style8">Maximum Consecutive Day Of Duty</td>
            <td class="auto-style8">
                <asp:TextBox ID="tbMaxConsecutiveDuty" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="auto-style8">Maximum Percentage Of Staff Assigned To Own Faculty Duty</td>
            <td class="auto-style8">
                <asp:TextBox ID="tbMaxStaffToOwnFaculty" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="auto-style9" colspan="2">
                <asp:Button ID="btnReset" runat="server" OnClick="btnReset_Click" Text="Reset" />
&nbsp;&nbsp;
                <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Save" />
            </td>
        </tr>
    </table>
</asp:Content>
