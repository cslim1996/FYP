
<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="ConstraintAndSetting.aspx.cs" Inherits="ExamTimetabling2016.CSTEST.ConstraintAndSetting" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <style type="text/css">
        .auto-style2 {
            height: 66px;
            width: 50%;
        }
        .auto-style4 {
            height: 30px;
            width: 50%;
        }
        .auto-style5 {
            height: 20px;
        }
        .auto-style6 {
            width: 50%;
        }
        .auto-style7 {
            height: 22px;
        }
        .auto-style8 {
            width: 50%;
            height: 22px;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table class="nav-justified">
        <tr>
            <td class="auto-style2">
                <asp:Panel ID="Panel1" runat="server">
                    <h3>Exam and Invigilation Duty Property</h3>
                    <table class="nav-justified">
                        <tr>
                            <td class="auto-style7">
                                Session of Invigilation Duty</td>
                            <td class="auto-style8">
                                <asp:DropDownList ID="DropDownListSession" runat="server">
                                    <asp:ListItem Selected="True" Value="Not Specified">Not Specified</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style7">Duration of Invigilation Duty</td>
                            <td class="auto-style8">
                                <asp:DropDownList ID="ddlDuration" runat="server">
                                    <asp:ListItem Selected="True">Not Specified</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>Category of Invigilation Duty</td>
                            <td class="auto-style6">
                                <asp:DropDownList ID="ddlCategoryOfInvigilation" runat="server" Width="109px">
                                    <asp:ListItem Selected="True">Not Specified</asp:ListItem>
                                    <asp:ListItem Value="Chief">Chief Invigilator</asp:ListItem>
                                    <asp:ListItem Value="Relief">Relief Invigilator</asp:ListItem>
                                    <asp:ListItem Value="In-Charge">Invigilator In-Charge</asp:ListItem>
                                    <asp:ListItem>Invigilator</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>Faculty Code of Exam paper</td>
                            <td class="auto-style6">
                                <asp:DropDownList ID="ddlFacultyExam" runat="server">
                                    <asp:ListItem Selected="True">Not Specified</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>Paper type of Exam</td>
                            <td class="auto-style6">
                                <asp:DropDownList ID="ddlPaperType" runat="server">
                                    <asp:ListItem Selected="True">Not Specified</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style4">Include Special Papers:</td>
                            <td class="auto-style4">
                                <asp:CheckBox ID="CheckBoxDoubleSeating" runat="server" Text="Double Seating Paper" />
                                <br />
                                <asp:CheckBox ID="CheckBoxCnbl" runat="server" Text="Cnbl Paper" />
                            </td>
                        </tr>
                        <tr>
                            <td>Day Of Examination: </td>
                            <td class="auto-style6">
                                <asp:DropDownList ID="ddlDayOfExam" runat="server">
                                    <asp:ListItem Selected="True">Not Specified</asp:ListItem>
                                    <asp:ListItem>Monday</asp:ListItem>
                                    <asp:ListItem>Tuesday</asp:ListItem>
                                    <asp:ListItem>Wednesday</asp:ListItem>
                                    <asp:ListItem>Thursday</asp:ListItem>
                                    <asp:ListItem>Friday</asp:ListItem>
                                    <asp:ListItem>Saturday</asp:ListItem>
                                    <asp:ListItem>Sunday</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="text-center" colspan="2">
                                <br />
                                <asp:Button ID="btnResetExam" runat="server" OnClick="btnResetExam_Click" Text="Reset Exam and Invigilation Property" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td>
                <h3>Staff Property<br />
                </h3>
                <asp:Panel ID="Panel2" runat="server">
                    <table class="nav-justified">
                        <tr>
                            <td class ="auto-style4">Staff is Muslim?</td>
                            <td>
                                <asp:DropDownList ID="ddlMuslim" runat="server">
                                    <asp:ListItem Selected="True">Not Specified</asp:ListItem>
                                    <asp:ListItem>Yes</asp:ListItem>
                                    <asp:ListItem>No</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>Staff is Experienced Invigilator</td>
                            <td>
                                <asp:DropDownList ID="ddlExperiencedInvigilator" runat="server">
                                    <asp:ListItem Selected="True">Not Specified</asp:ListItem>
                                    <asp:ListItem>Yes</asp:ListItem>
                                    <asp:ListItem>No</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>Staff is Chief Invigilator</td>
                            <td>
                                <asp:DropDownList ID="ddlChief" runat="server">
                                    <asp:ListItem Selected="True">Not Specified</asp:ListItem>
                                    <asp:ListItem>Yes</asp:ListItem>
                                    <asp:ListItem>No</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>Staff is Taking STSPhD?</td>
                            <td>
                                <asp:DropDownList ID="ddlSTSPhd" runat="server">
                                    <asp:ListItem Selected="True">Not Specified</asp:ListItem>
                                    <asp:ListItem>Yes</asp:ListItem>
                                    <asp:ListItem>No</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style5">Staff Employ Type</td>
                            <td class="auto-style5">
                                <asp:DropDownList ID="ddlEmployType" runat="server">
                                    <asp:ListItem Selected="True">Not Specified</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="text-center" colspan="2">
                                <asp:Button ID="btnResetStaff" runat="server" OnClick="btnResetStaff_Click" Text="Reset Staff Property" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <br />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Panel ID="Panel3" runat="server">
                    <h3>Constraint Property</h3>
                    <table class="nav-justified">
                        <tr>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="auto-style5"></td>
                            <td class="auto-style5"></td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                    </table>
                    <br />
                </asp:Panel>
            </td>
        </tr>
    </table>
&nbsp;
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
</asp:Content>
