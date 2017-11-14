
<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" MaintainScrollPositionOnPostBack="true" AutoEventWireup="true" CodeBehind="ConstraintAndSetting.aspx.cs" Inherits="ExamTimetabling2016.ConstraintAndSetting" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <style type="text/css">
        .auto-style7 {
            width: 50%;
            height: 22px;
            padding-top: 7px;
            padding-left: 10px;
            padding-bottom: 7px;
            text-align:center;
        }
        .auto-style8 {
            width: 50%;
            height: 22px;
            padding-top: 7px;
            padding-left: 10px;
            padding-bottom: 7px;
        }
        .auto-style9 {
            font-size: xx-small;
        }
        </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table class="nav-justified">
        <tr>
            <td class="auto-style8">
                <asp:Panel ID="Panel1" runat="server">
                    <h3>Exam and Invigilation Duty Property</h3>
                    <table class="nav-justified" border="1">
                        <tr>
                            <td class="auto-style8">
                                Session of Invigilation Duty</td>
                            <td class="auto-style8">
                                <asp:DropDownList ID="DropDownListSession" runat="server">
                                    <asp:ListItem Selected="True" Value="Not Specified">Not Specified</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style8">Duration of Invigilation Duty</td>
                            <td class="auto-style8">
                                <asp:DropDownList ID="ddlDuration" runat="server">
                                    <asp:ListItem Selected="True">Not Specified</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class ="auto-style8">Category of Invigilation Duty</td>
                            <td class="auto-style8">
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
                            <td class ="auto-style8">Faculty Code of Exam paper</td>
                            <td class="auto-style8">
                                <asp:DropDownList ID="ddlFacultyExam" runat="server">
                                    <asp:ListItem Selected="True">Not Specified</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class  ="auto-style8">Paper type of Exam</td>
                            <td class="auto-style8">
                                <asp:DropDownList ID="ddlPaperType" runat="server">
                                    <asp:ListItem Selected="True">Not Specified</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style8">Include Special Papers:</td>
                            <td class="auto-style8">
                                <asp:CheckBox ID="CheckBoxDoubleSeating" runat="server" Text="Double Seating Paper" />
                                <br />
                                <asp:CheckBox ID="CheckBoxCnbl" runat="server" Text="Cnbl Paper" />
                            </td>
                        </tr>
                        <tr>
                            <td class  ="auto-style8">Day Of Examination: </td>
                            <td class="auto-style8">
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
                            <td class="auto-style8">Minimum Percentage Of Experienced Invigilator In Venue</td>
                            <td class="auto-style8">
                                <asp:TextBox ID="tbMinPercentageOfExpInvi" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style7" colspan="2" >
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
                    <table class="nav-justified" border="1">
                        <tr>
                            <td class ="auto-style8">Staff is Muslim?</td>
                            <td class  ="auto-style8">
                                <asp:DropDownList ID="ddlMuslim" runat="server">
                                    <asp:ListItem Selected="True">Not Specified</asp:ListItem>
                                    <asp:ListItem>Yes</asp:ListItem>
                                    <asp:ListItem>No</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class  ="auto-style8">Staff is Experienced Invigilator</td>
                            <td class  ="auto-style8">
                                <asp:DropDownList ID="ddlExperiencedInvigilator" runat="server">
                                    <asp:ListItem Selected="True">Not Specified</asp:ListItem>
                                    <asp:ListItem>Yes</asp:ListItem>
                                    <asp:ListItem>No</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class  ="auto-style8">Staff is Chief Invigilator</td>
                            <td class  ="auto-style8">
                                <asp:DropDownList ID="ddlChief" runat="server">
                                    <asp:ListItem Selected="True">Not Specified</asp:ListItem>
                                    <asp:ListItem>Yes</asp:ListItem>
                                    <asp:ListItem>No</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class  ="auto-style8">Staff is Taking STSPhD?</td>
                            <td class  ="auto-style8">
                                <asp:DropDownList ID="ddlSTSPhd" runat="server">
                                    <asp:ListItem Selected="True">Not Specified</asp:ListItem>
                                    <asp:ListItem>Yes</asp:ListItem>
                                    <asp:ListItem>No</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style8">Staff Employ Type</td>
                            <td class="auto-style8">
                                <asp:DropDownList ID="ddlEmployType" runat="server">
                                    <asp:ListItem Selected="True">Not Specified</asp:ListItem>
                                    <asp:ListItem Value="F">Full Time</asp:ListItem>
                                    <asp:ListItem Value="P">Part Time</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style8">Staff Has Other Duty On Same Day</td>
                            <td class="auto-style8">
                                <asp:DropDownList ID="ddlOtherDuty" runat="server">
                                    <asp:ListItem Selected="True">Not Specified</asp:ListItem>
                                    <asp:ListItem>Yes</asp:ListItem>
                                    <asp:ListItem>No</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style8" colspan="2">
                                <table>
                                    <caption>
                                        *for the following part only one of these three can be selected as yes or no at a same time<tr>
                                            <td class="auto-style8">Staff Has Specific Duration Duty On Same Day</td>
                                            <td class="auto-style8">
                                                <asp:DropDownList ID="ddlDurationDuty" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlDurationDuty_SelectedIndexChanged">
                                                    <asp:ListItem Selected="True">Not Specified</asp:ListItem>
                                                    <asp:ListItem>Yes</asp:ListItem>
                                                    <asp:ListItem>No</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td class="auto-style8" rowspan="2">Session Field:</td>
                                            <td class="auto-style8" rowspan="2">
                                                <asp:TextBox ID="tbSession" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="auto-style8">Staff Has Specific Session Duty On Same Day</td>
                                            <td class="auto-style8">
                                                <asp:DropDownList ID="ddlSessionDuty" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlSessionDuty_SelectedIndexChanged">
                                                    <asp:ListItem Selected="True">Not Specified</asp:ListItem>
                                                    <asp:ListItem>Yes</asp:ListItem>
                                                    <asp:ListItem>No</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="auto-style8">Staff has Specific Session and Duration Duty On Same Day</td>
                                            <td class="auto-style8">
                                                <asp:DropDownList ID="ddlSessionAndDurationDuty" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlSessionAndDurationDuty_SelectedIndexChanged">
                                                    <asp:ListItem Selected="True">Not Specified</asp:ListItem>
                                                    <asp:ListItem>Yes</asp:ListItem>
                                                    <asp:ListItem>No</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td class="auto-style8">Duration Field</td>
                                            <td class="auto-style8">
                                                <asp:TextBox ID="tbDuration" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </caption>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style7" colspan="2">
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
                    <table class="nav-justified" border="1">
                        <tr>
                            <td class ="auto-style8">Constraint Importance Level</td>
                            <td class  ="auto-style8">
                                <asp:DropDownList ID="ddlConstraintImportance" runat="server">
                                    <asp:ListItem Selected="True">Low</asp:ListItem>
                                    <asp:ListItem>Medium</asp:ListItem>
                                    <asp:ListItem>High</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class  ="auto-style8">Is Hard Constraint?</td>
                            <td class  ="auto-style8">
                                <asp:DropDownList ID="ddlHardConstraint" runat="server">
                                    <asp:ListItem Selected="True">No</asp:ListItem>
                                    <asp:ListItem>Yes</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style8">Remark for the Constraint<br /> <span class="auto-style9">*Record the meaning for the constraint (max character 500)</span><br /> </td>
                            <td class="auto-style8">
                                <asp:TextBox ID="tbRemark" runat="server" Height="101px" Width="399px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class  ="auto-style7" colspan="2">
                                <asp:Button ID="btnResetConstraint" runat="server" OnClick="btnResetConstraint_Click" Text="Reset Constraint Property" />
                            </td>
                        </tr>
                    </table>
                    <br />
                </asp:Panel>
            </td>
        </tr>
    </table>
&nbsp;
    <br />
    <asp:Panel ID="Panel4" runat="server">
        <div class="text-center">
            <br />
            <asp:Label ID="lblValidate" runat="server"></asp:Label>
            <br />
            <br />
            <asp:Button ID="btnValidate" runat="server" Text="Validate" OnClick="btnValidate_Click" />
            &nbsp;&nbsp;
            <asp:Button ID="btnResetAll" runat="server" OnClick="btnResetAll_Click" Text="Reset All" />
            &nbsp;
            <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" Text="Submit" />
        </div>
</asp:Panel>
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
</asp:Content>
