<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SimultaneousPaperCreate.aspx.cs" Inherits="ExamTimetabling2016.SimultaneousPaperCreate" MasterPageFile="~/Site1.Master" %>

<asp:Content runat="server" ContentPlaceHolderID="MainContent">
    <!DOCTYPE html>

    <html xmlns="http://www.w3.org/1999/xhtml">
    <body style="height: 441px">
        <div>
            <br />
            <asp:Panel ID="Panel1" runat="server" Visible="False">
                <asp:Label ID="lblErrorMsg" runat="server" Enabled="False" Style="text-align: center; font-size: large; background-color: #ff1a1a" CssClass="row col-lg-12" Font-Bold="True" Font-Size="Medium"></asp:Label>
                <asp:Label ID="lblInsertStatus" runat="server" Enabled="False" Style="text-align: center; font-size: large; background-color: lightgreen" CssClass="row col-lg-12" Font-Bold="True" Font-Size="Medium"></asp:Label>
            </asp:Panel>

            <div style="width: 400px; margin: 0px auto;">
                <table class="table table-hover">
                    <tr>
                        <td style="text-align: left; vertical-align: top">Faculty:</td>
                        <td colspan="2">
                            <asp:DropDownList ID="ddlFaculty" runat="server" DataSourceID="SqlDataSource1" DataTextField="Faculty" DataValueField="FacultyCode" CssClass="form-control"></asp:DropDownList>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td style="text-align: left; vertical-align: top">Course:</td>
                        <td colspan="2">
                            <ajaxToolkit:ComboBox ID="cbCourse" runat="server" DataSourceID="SqlDataSource2" DataTextField="CourseCode" DataValueField="CourseCode" MaxLength="0" Style="display: inline;" AutoCompleteMode="SuggestAppend" DropDownStyle="DropDownList"></ajaxToolkit:ComboBox>
                        </td>
                        <td>
                            <asp:Button ID="btnAdd" runat="server" OnClick="btnAdd_Click" Text="Add" CssClass="btn btn-default btn-block" />
                        </td>
                    </tr>
                    <tr>
                        <td rowspan="2" style="text-align: left; vertical-align: top">Simultaneous Courses:</td>
                        <td colspan="2">
                            <asp:ListBox ID="ListBox1" runat="server" CssClass="form-control"></asp:ListBox>
                        </td>
                        <td>
                            <asp:Button ID="btnRemove" runat="server" Text="Remove" CssClass="btn btn-default btn-block" OnClick="btnRemove_Click" />
                            <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="btn btn-default btn-block" OnClick="btnClear_Click" />
                        </td>
                    </tr>
                </table>
            </div>
            <div class="row" style="width: 400px; margin: 0 auto;">
                <hr />
                <div class="col-lg-6">
                    <asp:Button ID="btnCreate" runat="server" Text="Create" OnClick="btnCreate_Click" CssClass="btn btn-default btn-block" />
                </div>
                <div class="col-lg-6">
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-default btn-block" PostBackUrl="~/View/SimultaneousMaintenance/SimultaneousMaintenance.aspx" />
                </div>
            </div>
            <br />
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:Examination %>" SelectCommand="SELECT * FROM [Faculty]"></asp:SqlDataSource>
            <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:Examination %>" SelectCommand="SELECT DISTINCT ProgrammeStructureCourse.CourseCode FROM Course INNER JOIN ProgrammeStructureCourse ON Course.CourseCode = ProgrammeStructureCourse.CourseCode INNER JOIN ProgrammeStructure ON ProgrammeStructureCourse.PSID = ProgrammeStructure.PSID WHERE (ProgrammeStructure.Session = '201505') AND (Course.ExamWeight &gt; 0)"></asp:SqlDataSource>
            <script src="Scripts/bootstrap.js"></script>
            <script src="Scripts/bootstrap.min.js"></script>
        </div>
    </body>
    </html>
</asp:Content>
