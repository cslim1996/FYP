<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SimultaneousPaperEdit.aspx.cs" Inherits="ExamTimetabling2016.SimultaneousPaperEdit" MasterPageFile="~/Site1.Master" %>

<asp:Content runat="server" ContentPlaceHolderID="MainContent">
    <!DOCTYPE html>

    <html xmlns="http://www.w3.org/1999/xhtml">
    <body style="height: 441px">
        <div>
            <br />
            <asp:Panel ID="Panel1" runat="server" Visible="False">
                <asp:Label ID="lblErrorMsg" runat="server" Enabled="False" Style="text-align: center; font-size: large; background-color: #ff1a1a" CssClass="row col-lg-12" Font-Bold="True" Font-Size="Medium"></asp:Label>
                <asp:Label ID="lblUpdateStatus" runat="server" Enabled="False" Style="text-align: center; font-size: large; background-color: lightgreen" CssClass="row col-lg-12" Font-Bold="True" Font-Size="Medium"></asp:Label>
            </asp:Panel>
            <div style="width: 400px; margin: 0 auto;">
                <table class="table table-hover">
                    <tr>
                        <td>Faculty:</td>
                        <td colspan="2">
                            <asp:DropDownList ID="ddlFaculty" runat="server" DataSourceID="SqlDataSource1" DataTextField="Faculty" DataValueField="FacultyCode" CssClass="form-control"></asp:DropDownList>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td>Course:</td>
                        <td colspan="2">
                            <ajaxToolkit:ComboBox ID="cbCourse" runat="server" DataSourceID="SqlDataSource2" DataTextField="CourseCode" DataValueField="CourseCode" MaxLength="0" Style="display: inline;" AutoCompleteMode="SuggestAppend" DropDownStyle="DropDownList"></ajaxToolkit:ComboBox>
                        </td>
                        <td>
                            <asp:Button ID="btnAdd" runat="server" OnClick="btnAdd_Click" Text="Add" CssClass="btn btn-default btn-block" />
                        </td>
                    </tr>
                    <tr>
                        <td>Simultaneous Courses:</td>
                        <td colspan="2">
                            <asp:ListBox ID="ListBox1" runat="server" CssClass="form-control"></asp:ListBox>
                        </td>
                        <td>
                            <asp:Button ID="Button1" runat="server" Text="Remove" CssClass="btn btn-default btn-block" OnClick="btnRemove_Click" />
                            <asp:Button ID="Button2" runat="server" Text="Clear" CssClass="btn btn-default btn-block" OnClick="btnClear_Click" />
                        </td>
                    </tr>
                </table>
            </div>
            <div class="row" style="width: 400px; margin: 0 auto;">
                <hr />
                <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Save" CssClass="btn btn-default btn-block" />
            </div>
            <br />
            <div class="row" style="width: 400px; margin: 0 auto; text-align: center">
                <asp:LinkButton ID="lbtnBack" runat="server" CssClass="btn-link btn-block" PostBackUrl="~/View/SimultaneousMaintenance/SimultaneousMaintenance.aspx">Back</asp:LinkButton>
            </div>
        </div>
        <br />
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:Examination %>" SelectCommand="SELECT * FROM [Faculty]"></asp:SqlDataSource>
        <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:Examination %>" SelectCommand="SELECT DISTINCT ProgrammeStructureCourse.CourseCode FROM Course INNER JOIN ProgrammeStructureCourse ON Course.CourseCode = ProgrammeStructureCourse.CourseCode INNER JOIN ProgrammeStructure ON ProgrammeStructureCourse.PSID = ProgrammeStructure.PSID WHERE (ProgrammeStructure.Session = '201505') AND (Course.ExamWeight &gt; 0)"></asp:SqlDataSource>
        <script src="Scripts/bootstrap.js"></script>
        <script src="Scripts/bootstrap.min.js"></script>
        <asp:Label ID="Label1" runat="server" Text="Label" Visible="False"></asp:Label>
    </body>
    </html>
</asp:Content>
