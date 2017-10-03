<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SimultaneousMaintenance.aspx.cs" Inherits="ExamTimetabling2016.SimultaneousMaintenance" MasterPageFile="~/Site1.Master" %>

<asp:Content runat="server" ContentPlaceHolderID="MainContent">
    <!DOCTYPE html>

    <html xmlns="http://www.w3.org/1999/xhtml">

    <body>
        <asp:ListView ID="ListView1" runat="server" DataKeyNames="SCID" DataSourceID="SqlDataSource2">
            <EmptyDataTemplate>
                <span>No data was returned.</span>
            </EmptyDataTemplate>
            <ItemTemplate>
                <div class="row info">
                    <div class="col-lg-4" style="text-align: right">
                        <asp:LinkButton ID="lnkSelect" Text="Select" CommandName="Select" runat="server" OnClick="lnkSelect_Click" />
                    </div>
                    <div class="col-lg-4">
                        <span style="">
                            <table class="table table-condensed">
                                <tr>
                                    <td style="text-align: right">Record ID:</td>
                                    <td>
                                        <asp:Label ID="SCIDLabel" runat="server" Text='<%# Eval("SCID") %>' />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right">Faculty:</td>
                                    <td>
                                        <asp:Label ID="FacultyLabel" runat="server" Text='<%# Eval("Faculty") %>' />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right">Simultaneous Courses:</td>
                                    <td>
                                        <asp:Label ID="SCListLabel" runat="server" Text='<%# Eval("SCList") %>' />
                                    </td>
                                </tr>
                            </table>
                        </span>
                    </div>
                    <div class="col-lg-4"></div>
                </div>
            </ItemTemplate>
            <SelectedItemTemplate>
                <div class="row alert-info">
                    <div class="col-lg-4" style="text-align: right">
                        <asp:LinkButton ID="lnkSelect" Text="Select" CommandName="Select" runat="server" OnClick="lnkSelect_Click" />
                    </div>
                    <div class="col-lg-4">
                        <span style="">
                            <table class="table table-condensed">
                                <tr>
                                    <td style="text-align: right">Record ID:</td>
                                    <td>
                                        <asp:Label ID="SCIDLabel" runat="server" Text='<%# Eval("SCID") %>' />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right">Faculty:</td>
                                    <td>
                                        <asp:Label ID="FacultyLabel" runat="server" Text='<%# Eval("Faculty") %>' />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right">Simultaneous Courses:</td>
                                    <td>
                                        <asp:Label ID="SCListLabel" runat="server" Text='<%# Eval("SCList") %>' />
                                    </td>
                                </tr>
                            </table>
                        </span>
                    </div>
                    <div class="col-lg-4"></div>
                </div>
            </SelectedItemTemplate>
            <LayoutTemplate>
                <div id="itemPlaceholderContainer" runat="server" style="height: auto; text-align: center">
                    <span runat="server" id="itemPlaceholder" />
                </div>
                <div style="text-align: center;">
                    <asp:DataPager ID="DataPager1" runat="server" PageSize="5">
                        <Fields>
                            <asp:NextPreviousPagerField ButtonCssClass="btn btn-default" ButtonType="Button" ShowFirstPageButton="False" ShowNextPageButton="False" ShowPreviousPageButton="True" />
                            <asp:NumericPagerField />
                            <asp:NextPreviousPagerField ButtonCssClass="btn btn-default" ButtonType="Button" ShowLastPageButton="False" ShowNextPageButton="True" ShowPreviousPageButton="False" />
                        </Fields>
                    </asp:DataPager>
                </div>
            </LayoutTemplate>
        </asp:ListView>
        <br />
        <div class="row">
            <div class="col-lg-4"></div>
            <div class="col-lg-4">
                <div class="col-lg-4">
                    <asp:Button ID="btnCreate" runat="server" OnClick="btnCreate_Click" PostBackUrl="~/View/SimultaneousMaintenance/SimultaneousPaperCreate.aspx" Text="Create New Record" CssClass="btn btn-default btn-block btn-primary" />
                </div>
                <div class="col-lg-4">
                    <asp:Button ID="btnEdit" runat="server" Text="Edit" CssClass="btn btn-default btn-block btn-info" PostBackUrl="~/View/SimultaneousMaintenance/SimultaneousPaperEdit.aspx" Enabled="False" />
                </div>
                <div class="col-lg-4">
                    <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="btn btn-danger btn-block" Enabled="False" PostBackUrl="~/View/SimultaneousMaintenance/SimultaneousPaperDelete.aspx" />
                </div>
            </div>
            <div class="col-lg-4"></div>
        </div>
        <br />
        <asp:Label ID="lblTest" runat="server" Visible="False"></asp:Label>
        <br />
        <br />
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:Examination %>" SelectCommand="SELECT * FROM [Faculty]"></asp:SqlDataSource>
        <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:Examination %>" SelectCommand="SELECT SimultaneousCourses.SCID, Faculty.Faculty, SimultaneousCourses.Status, SimultaneousCourses.SCList FROM Faculty INNER JOIN SimultaneousCourses ON Faculty.FacultyCode = SimultaneousCourses.FacultyCode WHERE SimultaneousCourses.Status = 'Active'"></asp:SqlDataSource>
        <br />
    </body>
    </html>
</asp:Content>

