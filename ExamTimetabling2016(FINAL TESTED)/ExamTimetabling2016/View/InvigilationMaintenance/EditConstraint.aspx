<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="EditConstraint.aspx.cs" Inherits="ExamTimetabling2016.View.InvigilationMaintenance.WebForm2" %>
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
    <table class="nav-justified" __designer:mapid="2">
        <tr __designer:mapid="3">
            <td class="auto-style8" __designer:mapid="4">
                <asp:Panel ID="Panel1" runat="server">
                    <h3>Exam and Invigilation Duty Property</h3>
                    <table class="nav-justified" border="1">
                        <tr>
                            <td class="auto-style8">Session of Invigilation Duty</td>
                            <td class="auto-style8">
                                <asp:DropDownList ID="DropDownListSession" runat="server" OnSelectedIndexChanged="DropDownListSession_SelectedIndexChanged">
                                    <asp:ListItem Selected="True" Value="Not Specified">Not Specified</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style8">Duration of Invigilation Duty</td>
                            <td class="auto-style8">
                                <asp:DropDownList ID="ddlDuration" runat="server" OnSelectedIndexChanged="ddlDuration_SelectedIndexChanged">
                                    <asp:ListItem Selected="True">Not Specified</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class ="auto-style8">Category of Invigilation Duty</td>
                            <td class="auto-style8">
                                <asp:DropDownList ID="ddlCategoryOfInvigilation" runat="server" Width="109px" OnSelectedIndexChanged="ddlCategoryOfInvigilation_SelectedIndexChanged">
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
                                <asp:DropDownList ID="ddlFacultyExam" runat="server" OnSelectedIndexChanged="ddlFacultyExam_SelectedIndexChanged">
                                    <asp:ListItem Selected="True">Not Specified</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class  ="auto-style8">Paper type of Exam</td>
                            <td class="auto-style8">
                                <asp:DropDownList ID="ddlPaperType" runat="server" OnSelectedIndexChanged="ddlPaperType_SelectedIndexChanged">
                                    <asp:ListItem Selected="True">Not Specified</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style8">Include Special Papers:</td>
                            <td class="auto-style8">
                                <asp:CheckBox ID="CheckBoxDoubleSeating" runat="server" Text="Double Seating Paper" OnCheckedChanged="CheckBoxDoubleSeating_CheckedChanged" />
                                <br />
                                <asp:CheckBox ID="CheckBoxCnbl" runat="server" Text="Cnbl Paper" OnCheckedChanged="CheckBoxCnbl_CheckedChanged" />
                            </td>
                        </tr>
                        <tr>
                            <td class  ="auto-style8">Day Of Examination: </td>
                            <td class="auto-style8">
                                <asp:DropDownList ID="ddlDayOfExam" runat="server" OnSelectedIndexChanged="ddlDayOfExam_SelectedIndexChanged">
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
                                <asp:TextBox ID="tbMinPercentageOfExpInvi" runat="server" OnTextChanged="tbMinPercentageOfExpInvi_TextChanged"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style7" colspan="2" >
                                <br />
                                <asp:Button ID="btnResetExam" UseSubmitBehavior="false" runat="server" OnClick="btnResetExam_Click" Text="Reset Exam and Invigilation Property" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        <tr __designer:mapid="3f">
            <td __designer:mapid="40">
                <h3 __designer:mapid="41">Staff Property<br __designer:mapid="42" /></h3>
                <asp:Panel ID="Panel2" runat="server">
                    <table class="nav-justified" border="1">
                        <tr>
                            <td class ="auto-style8">Staff is Muslim?</td>
                            <td class  ="auto-style8">
                                <asp:DropDownList ID="ddlMuslim" runat="server" OnSelectedIndexChanged="ddlMuslim_SelectedIndexChanged">
                                    <asp:ListItem Selected="True">Not Specified</asp:ListItem>
                                    <asp:ListItem>Yes</asp:ListItem>
                                    <asp:ListItem>No</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class  ="auto-style8">Faculty of Staff</td>
                            <td class  ="auto-style8">
                                <asp:DropDownList ID="ddlStaffFaculty" runat="server" OnSelectedIndexChanged="ddlStaffFaculty_SelectedIndexChanged">
                                    <asp:ListItem Selected="True">Not Specified</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style8">Staff is Experienced Invigilator</td>
                            <td class="auto-style8">
                                <asp:DropDownList ID="ddlExperiencedInvigilator" runat="server" OnSelectedIndexChanged="ddlExperiencedInvigilator_SelectedIndexChanged">
                                    <asp:ListItem Selected="True">Not Specified</asp:ListItem>
                                    <asp:ListItem>Yes</asp:ListItem>
                                    <asp:ListItem>No</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class  ="auto-style8">Staff is Chief Invigilator</td>
                            <td class  ="auto-style8">
                                <asp:DropDownList ID="ddlChief" runat="server" OnSelectedIndexChanged="ddlChief_SelectedIndexChanged">
                                    <asp:ListItem Selected="True">Not Specified</asp:ListItem>
                                    <asp:ListItem>Yes</asp:ListItem>
                                    <asp:ListItem>No</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class  ="auto-style8">Staff is Taking STSPhD?</td>
                            <td class  ="auto-style8">
                                <asp:DropDownList ID="ddlSTSPhd" runat="server" OnSelectedIndexChanged="ddlSTSPhd_SelectedIndexChanged">
                                    <asp:ListItem Selected="True">Not Specified</asp:ListItem>
                                    <asp:ListItem>Yes</asp:ListItem>
                                    <asp:ListItem>No</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style8">Staff Employ Type</td>
                            <td class="auto-style8">
                                <asp:DropDownList ID="ddlEmployType" runat="server" OnSelectedIndexChanged="ddlEmployType_SelectedIndexChanged">
                                    <asp:ListItem Selected="True">Not Specified</asp:ListItem>
                                    <asp:ListItem Value="F">Full Time</asp:ListItem>
                                    <asp:ListItem Value="P">Part Time</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style8">Staff Has Other Duty On Same Day</td>
                            <td class="auto-style8">
                                <asp:DropDownList ID="ddlOtherDuty" runat="server" OnSelectedIndexChanged="ddlOtherDuty_SelectedIndexChanged">
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
                                                <asp:DropDownList ID="ddlDurationDuty" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlDurationDuty_SelectedIndexChanged" style="height: 22px">
                                                    <asp:ListItem Selected="True">Not Specified</asp:ListItem>
                                                    <asp:ListItem>Yes</asp:ListItem>
                                                    <asp:ListItem>No</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td class="auto-style8" rowspan="2">Session Field:</td>
                                            <td class="auto-style8" rowspan="2">
                                                <asp:TextBox ID="tbSession" runat="server" OnTextChanged="tbSession_TextChanged"></asp:TextBox>
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
                                                <asp:TextBox ID="tbDuration" runat="server" OnTextChanged="tbDuration_TextChanged"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </caption>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style7" colspan="2">
                                <asp:Button ID="btnResetStaff" UseSubmitBehavior="false" runat="server" OnClick="btnResetStaff_Click" Text="Reset Staff Property" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <br __designer:mapid="96" /></td>
        </tr>
        <tr __designer:mapid="97">
            <td __designer:mapid="98">
                <asp:Panel ID="Panel3" runat="server">
                    <h3>Constraint Property</h3>
                    <table class="nav-justified" border="1">
                        <tr>
                            <td class ="auto-style8">Constraint Importance Level</td>
                            <td class  ="auto-style8">
                                <asp:DropDownList ID="ddlConstraintImportance" runat="server" OnSelectedIndexChanged="ddlConstraintImportance_SelectedIndexChanged" style="height: 22px; width: 75px">
                                    <asp:ListItem Selected="True">Low</asp:ListItem>
                                    <asp:ListItem>Medium</asp:ListItem>
                                    <asp:ListItem>High</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class  ="auto-style8">Is Hard Constraint?</td>
                            <td class  ="auto-style8">
                                <asp:DropDownList ID="ddlHardConstraint" runat="server" OnSelectedIndexChanged="ddlHardConstraint_SelectedIndexChanged">
                                    <asp:ListItem Selected="True">No</asp:ListItem>
                                    <asp:ListItem>Yes</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style8">Remark for the Constraint<br /> <span class="auto-style9">*Record the meaning for the constraint (max character 500)</span><br /> </td>
                            <td class="auto-style8">
                                <asp:TextBox ID="tbRemark" runat="server" Height="101px" Width="399px" OnTextChanged="tbRemark_TextChanged"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class  ="auto-style7" colspan="2">
                                <asp:Button ID="btnResetConstraint" UseSubmitBehavior="false" runat="server" OnClick="btnResetConstraint_Click" Text="Reset Constraint Property" />
                            </td>
                        </tr>
                    </table>
                    <br />
                </asp:Panel>
            </td>
        </tr>
    </table>
&nbsp;<br __designer:mapid="b4" />
    <asp:Panel ID="Panel4" runat="server">
        <div class="text-center">
            <asp:Label ID="lblValidate" runat="server" Font-Italic="True" ForeColor="Red"></asp:Label>
            <br />
            <br />
        </div>
    </asp:Panel>
    <asp:Panel ID="Panel5" runat="server">
        <div class="text-center">
            <asp:Button ID="btnValidate" runat="server" OnClick="btnValidate_Click" Text="Validate" UseSubmitBehavior="false" />
            &nbsp;
            <asp:Button ID="btnResetAll" runat="server" OnClick="btnResetAll_Click" Text="Reset All" UseSubmitBehavior="false" />
            &nbsp;
            <asp:Button ID="btnSubmit0" runat="server" OnClick="btnSubmit_Click" Text="Submit" UseSubmitBehavior="false" />
        </div>
    </asp:Panel>
</asp:Content>
