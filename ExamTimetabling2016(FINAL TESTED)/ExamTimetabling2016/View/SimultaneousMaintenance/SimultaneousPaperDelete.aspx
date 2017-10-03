<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SimultaneousPaperDelete.aspx.cs" Inherits="ExamTimetabling2016.SimultaneousPaperDelete" MasterPageFile="~/Site1.Master" %>

<asp:Content runat="server" ContentPlaceHolderID="MainContent">
    <!DOCTYPE html>

    <html xmlns="http://www.w3.org/1999/xhtml">
    <body style="height: 441px">
        <div>
            <br />
            <asp:Panel ID="statusPanel" runat="server">
                <div style="text-align: center; font-size: large; background-color: #ff1a1a">
                    <span class="glyphicon glyphicon-warning-sign"></span>
                    <strong>
                        <asp:Label ID="lblDeleteStatus" runat="server" Text="Are you sure you want to delete this record?"></asp:Label>
                    </strong>
                </div>
            </asp:Panel>
            <asp:Panel ID="statusPanel2" runat="server" Visible="False">
                <div style="text-align: center; font-size: large; background-color: lightgreen">
                    <span class="glyphicon glyphicon-ok"></span>
                    <strong>
                        <asp:Label ID="lblSuccess" runat="server" Text="Delete Successfully"></asp:Label>
                    </strong>
                </div>
            </asp:Panel>
            <br />
            <asp:Panel ID="Panel1" runat="server">
                <div class="row" style="width: 400px; margin: 0 auto;">
                    <table class="table-hover table">
                        <tr>
                            <td style="text-align: left; vertical-align: top">Record ID:</td>
                            <td>
                                <asp:TextBox ID="txtID" runat="server" ReadOnly="True" CssClass="form-control"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left; vertical-align: top">Faculty:</td>
                            <td>
                                <asp:TextBox ID="txtFaculty" runat="server" ReadOnly="True" CssClass="form-control"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left; vertical-align: top">Simultaneous Courses:</td>
                            <td>
                                <asp:TextBox ID="txtSCList" runat="server" ReadOnly="True" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="row" style="width: 400px; margin: 0 auto;">
                    <hr />
                    <div class="col-lg-6">
                        <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="btn btn-default btn-block" OnClick="btnDelete_Click" />
                    </div>
                    <div class="col-lg-6">
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-default btn-block" PostBackUrl="~/View/SimultaneousMaintenance/SimultaneousMaintenance.aspx" />
                    </div>
                </div>
            </asp:Panel>
            <div class="row" style="width: 400px; margin: 0 auto; text-align: center;">
                <asp:Panel ID="Panel2" runat="server" Visible="False">
                    <asp:LinkButton ID="LinkButton1" runat="server" CssClass="btn-link" PostBackUrl="~/View/SimultaneousMaintenance/SimultaneousMaintenance.aspx">Back</asp:LinkButton>
                </asp:Panel>
            </div>
            <br />
            <script src="Scripts/bootstrap.js"></script>
            <script src="Scripts/bootstrap.min.js"></script>
        </div>
    </body>
    </html>
</asp:Content>
