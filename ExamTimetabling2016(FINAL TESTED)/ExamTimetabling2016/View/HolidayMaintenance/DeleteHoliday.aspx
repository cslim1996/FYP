<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site1.Master" CodeBehind="DeleteHoliday.aspx.cs" Inherits="ExamTimetabling2016.View.HolidayMaintenance.DeleteHoliday" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>


            <h2>Delete Holiday</h2>
            <hr />


            <div class="form-horizontal">
                <div class="form-group">
                    <label class="control-label col-md-2">Holiday Name</label>
                    <div class="col-md-10">
                        <asp:TextBox ID="txt_holidayName" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <label class="control-label col-md-2">Start Date</label>
                    <div class="col-md-10">
                        <div class="input-group" style="width: 29%; height: 100%">
                            <asp:TextBox ID="txt_showStart" runat="server" CssClass="form-control" Enabled="false" Font-Bold="True" Font-Names="Calibri"></asp:TextBox>
                            <span class="input-group-btn">
                                <asp:Button ID="btn_showStart" runat="server" Text="Show" CssClass="btn btn-success"/>
                            </span>
                        </div>
                        <asp:Calendar ID="calendar_Start" runat="server" Font-Size="9pt" BackColor="White" BorderColor="#CCCCCC" CellPadding="5" Font-Names="Century Gothic" ForeColor="Black" Height="100%" Width="29%" BorderWidth="1px">
                            <DayHeaderStyle BackColor="Transparent" Font-Bold="true" Font-Size="10pt" />
                            <NextPrevStyle VerticalAlign="Bottom" />
                            <OtherMonthDayStyle ForeColor="#808080" />
                            <SelectedDayStyle BackColor="Green" Font-Bold="True" ForeColor="White" />
                            <SelectorStyle BackColor="#CCCCCC" />
                            <TitleStyle BackColor="Transparent" BorderColor="Transparent" Font-Bold="false" Font-Size="10pt" />
                            <TodayDayStyle BackColor="LightGray" ForeColor="Black" />
                            <WeekendDayStyle BackColor="WhiteSmoke" />
                        </asp:Calendar>
                    </div>
                </div>

                <div class="form-group">
                    <label class="control-label col-md-2">End Date</label>
                    <div class="col-md-10">
                        <div class="input-group" style="width: 29%; height: 100%">
                            <asp:TextBox ID="txt_showEnd" runat="server" CssClass="form-control" Enabled="false" Font-Bold="True" Font-Names="Calibri"></asp:TextBox>
                            <span class="input-group-btn">
                                <asp:Button ID="btn_showEnd" runat="server" Text="Show" CssClass="btn btn-success"/>
                            </span>
                        </div>
                        <asp:Calendar ID="calendar_End" runat="server" Font-Size="9pt" BackColor="White" BorderColor="#CCCCCC" CellPadding="5" Font-Names="Century Gothic" ForeColor="Black" Height="100%" Width="29%" BorderWidth="1px">
                            <DayHeaderStyle BackColor="Transparent" Font-Bold="true" Font-Size="10pt" />
                            <NextPrevStyle VerticalAlign="Bottom" />
                            <OtherMonthDayStyle ForeColor="#808080" />
                            <SelectedDayStyle BackColor="Green" Font-Bold="True" ForeColor="White" />
                            <SelectorStyle BackColor="#CCCCCC" />
                            <TitleStyle BackColor="Transparent" BorderColor="Transparent" Font-Bold="false" Font-Size="10pt" />
                            <TodayDayStyle BackColor="LightGray" ForeColor="Black" />
                            <WeekendDayStyle BackColor="WhiteSmoke" />
                        </asp:Calendar>
                    </div>
                </div>

                <div class="form-group">
                    <label class="control-label col-md-2">States Affected</label>
                    <div class="col-md-10">
                        <asp:CheckBoxList ID="cbl_States" runat="server" Font-Names="Century Gothic" CssClass="table table-striped table-condensed table-bordered" CellPadding="3" Width="29%" RepeatColumns="2">
                            <asp:ListItem>Kuala Lumpur</asp:ListItem>
                            <asp:ListItem>Penang</asp:ListItem>
                            <asp:ListItem>Perak</asp:ListItem>
                            <asp:ListItem>Johor</asp:ListItem>
                            <asp:ListItem>Pahang</asp:ListItem>
                            <asp:ListItem>Sabah</asp:ListItem>
                        </asp:CheckBoxList>
                    </div>
                </div>

                <div class="form-group">
                    <div class="col-md-offset-2 col-md-10">
                        <asp:Button ID="btn_Delete" runat="server" Text="Delete This Venue" CssClass="btn btn-danger" OnClick="btn_Delete_Click" />
                        <asp:Button ID="btn_Clear" runat="server" Text="Cancel" CssClass="btn btn-default" PostBackUrl="~/View/HolidayMaintenance/HolidayMaintenance.aspx" />
                    </div>
                </div>

            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
