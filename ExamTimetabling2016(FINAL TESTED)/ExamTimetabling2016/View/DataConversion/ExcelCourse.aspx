<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="ExcelCourse.aspx.cs" Inherits="ExamTimetabling2016.View.ExcelCourse" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript" src="../../Scripts/script.js"></script>
    <div id="parentExcel">
        <div class="leftExcel">
            <h2>Data Import for Course</h2>

            <table border="1">
                <tr>
                    <td><b>Faculty</b></td>
                    <td><b>Level</b></td>
                </tr>
                <tr>
                    <td>
                        <asp:RadioButtonList ID="rListFaculty" runat="server" AutoPostBack="True" OnSelectedIndexChanged="rListFaculty_SelectedIndexChanged">
                            <asp:ListItem>CNBL</asp:ListItem>
                            <asp:ListItem>CPUS</asp:ListItem>
                            <asp:ListItem>FASC</asp:ListItem>
                            <asp:ListItem>FAFB</asp:ListItem>
                            <asp:ListItem>FEBE</asp:ListItem>
                            <asp:ListItem>FSAH</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                    <td>
                        <asp:RadioButtonList ID="rListLevel" runat="server">
                            <asp:ListItem>Bachelor</asp:ListItem>
                            <asp:ListItem>Diploma</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td><b>Invigilator</b></td>
                </tr>
                <tr>
                    <td>
                        <asp:RadioButton ID="RadioButtonInv" runat="server" Text="Invigilator" value="Invigilation" AutoPostBack="True" OnCheckedChanged="RadioButtonInv_CheckedChanged" />
                    </td>
                </tr>
            </table>
        </div>
        <div class="rightExcel">
            <h2>User Guideline</h2>
            <asp:Label ID="lblGuideline" runat="server"></asp:Label>
        </div>
    </div>
    <br />
    <br />
    <asp:Label ID="lblSelectFile" runat="server" Text="Select Files: "></asp:Label>&nbsp;&nbsp;<asp:FileUpload ID="FileUpload1" runat="server" />
    <br />
    <asp:Label ID="lblRowNo" runat="server" Text="Starting Row No: "></asp:Label><br />
    <asp:TextBox ID="txtRowNo" runat="server" Width="90px" MaxLength="2"></asp:TextBox>
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
