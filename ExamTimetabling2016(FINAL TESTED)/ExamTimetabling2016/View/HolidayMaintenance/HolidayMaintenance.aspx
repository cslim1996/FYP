<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HolidayMaintenance.aspx.cs" MasterPageFile="~/Site1.Master" Inherits="FYP.Holiday_Maintenance.HolidayMaintenance" %>

<asp:Content runat="server" ContentPlaceHolderID="MainContent">
    <div>
        <h2>Holiday Maintenance</h2>
        <hr />

        <div class="form-horizontal col-md-12">
            <div class="form-group">
                <asp:Button ID="Button1" runat="server" CssClass="btn btn-success pull-right" Text="Add New Holiday" PostBackUrl="AddHoliday.aspx" />
            </div>
        </div>

        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1" CssClass="table table-responsive table-striped table-hover" GridLines="None" AllowPaging="True" AllowSorting="True" ShowHeaderWhenEmpty="True" DataKeyNames="HolidayID" OnRowEditing="GridView1_RowEditing" Font-Bold="False" Font-Size="14px" RowStyle-HorizontalAlign="Justify" RowStyle-VerticalAlign="Middle" OnRowDeleting="GridView1_RowDeleting">
            <Columns>


                <asp:TemplateField HeaderText="HolidayID" InsertVisible="False" SortExpression="HolidayID">
                    <EditItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("HolidayID") %>'></asp:Label>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("HolidayID") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="HolidayName" HeaderText="Holiday's Name" SortExpression="HolidayName" />
                <asp:BoundField DataField="StartDate" HeaderText="Start Date" SortExpression="StartDate" />
                <asp:BoundField DataField="EndDate" HeaderText="End Date" SortExpression="EndDate" />
                <asp:BoundField DataField="isKL" HeaderText="Kuala Lumpur" SortExpression="isKL" />
                <asp:BoundField DataField="isPenang" HeaderText="Penang" SortExpression="isPenang" />
                <asp:BoundField DataField="isPerak" HeaderText="Perak" SortExpression="isPerak" />
                <asp:BoundField DataField="isJohor" HeaderText="Johor" SortExpression="isJohor" />
                <asp:BoundField DataField="isPahang" HeaderText="Pahang" SortExpression="isPahang" />
                <asp:BoundField DataField="isSabah" HeaderText="Sabah" SortExpression="isSabah" />
                <asp:TemplateField ShowHeader="False">
                    <EditItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="True" CommandName="Update" Text="Update"></asp:LinkButton>
                        &nbsp;<asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel"></asp:LinkButton>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <div class="">
                            <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Edit" Text="Edit" CssClass="btn btn-info btn-sm"><span class="glyphicon glyphicon-pencil"></span></asp:LinkButton>
                            &nbsp;<asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="Delete" Text="Delete" CssClass="btn btn-danger btn-sm"><span class="glyphicon glyphicon-trash"></span></asp:LinkButton>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <RowStyle HorizontalAlign="Justify" VerticalAlign="Middle" />
        </asp:GridView>

        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConflictDetection="CompareAllValues" ConnectionString="<%$ ConnectionStrings:Examination %>" DeleteCommand="DELETE FROM [Holiday] WHERE [HolidayID] = @original_HolidayID AND (([HolidayName] = @original_HolidayName) OR ([HolidayName] IS NULL AND @original_HolidayName IS NULL)) AND (([StartDate] = @original_StartDate) OR ([StartDate] IS NULL AND @original_StartDate IS NULL)) AND (([EndDate] = @original_EndDate) OR ([EndDate] IS NULL AND @original_EndDate IS NULL)) AND (([isKL] = @original_isKL) OR ([isKL] IS NULL AND @original_isKL IS NULL)) AND (([isPenang] = @original_isPenang) OR ([isPenang] IS NULL AND @original_isPenang IS NULL)) AND (([isPerak] = @original_isPerak) OR ([isPerak] IS NULL AND @original_isPerak IS NULL)) AND (([isJohor] = @original_isJohor) OR ([isJohor] IS NULL AND @original_isJohor IS NULL)) AND (([isPahang] = @original_isPahang) OR ([isPahang] IS NULL AND @original_isPahang IS NULL)) AND (([isSabah] = @original_isSabah) OR ([isSabah] IS NULL AND @original_isSabah IS NULL))" InsertCommand="INSERT INTO [Holiday] ([HolidayName], [StartDate], [EndDate], [isKL], [isPenang], [isPerak], [isJohor], [isPahang], [isSabah]) VALUES (@HolidayName, @StartDate, @EndDate, @isKL, @isPenang, @isPerak, @isJohor, @isPahang, @isSabah)" OldValuesParameterFormatString="original_{0}" SelectCommand="SELECT * FROM [Holiday]" UpdateCommand="UPDATE [Holiday] SET [HolidayName] = @HolidayName, [StartDate] = @StartDate, [EndDate] = @EndDate, [isKL] = @isKL, [isPenang] = @isPenang, [isPerak] = @isPerak, [isJohor] = @isJohor, [isPahang] = @isPahang, [isSabah] = @isSabah WHERE [HolidayID] = @original_HolidayID AND (([HolidayName] = @original_HolidayName) OR ([HolidayName] IS NULL AND @original_HolidayName IS NULL)) AND (([StartDate] = @original_StartDate) OR ([StartDate] IS NULL AND @original_StartDate IS NULL)) AND (([EndDate] = @original_EndDate) OR ([EndDate] IS NULL AND @original_EndDate IS NULL)) AND (([isKL] = @original_isKL) OR ([isKL] IS NULL AND @original_isKL IS NULL)) AND (([isPenang] = @original_isPenang) OR ([isPenang] IS NULL AND @original_isPenang IS NULL)) AND (([isPerak] = @original_isPerak) OR ([isPerak] IS NULL AND @original_isPerak IS NULL)) AND (([isJohor] = @original_isJohor) OR ([isJohor] IS NULL AND @original_isJohor IS NULL)) AND (([isPahang] = @original_isPahang) OR ([isPahang] IS NULL AND @original_isPahang IS NULL)) AND (([isSabah] = @original_isSabah) OR ([isSabah] IS NULL AND @original_isSabah IS NULL))">
            <DeleteParameters>
                <asp:Parameter Name="original_HolidayID" Type="Int32" />
                <asp:Parameter Name="original_HolidayName" Type="String" />
                <asp:Parameter DbType="Date" Name="original_StartDate" />
                <asp:Parameter DbType="Date" Name="original_EndDate" />
                <asp:Parameter Name="original_isKL" Type="String" />
                <asp:Parameter Name="original_isPenang" Type="String" />
                <asp:Parameter Name="original_isPerak" Type="String" />
                <asp:Parameter Name="original_isJohor" Type="String" />
                <asp:Parameter Name="original_isPahang" Type="String" />
                <asp:Parameter Name="original_isSabah" Type="String" />
            </DeleteParameters>
            <InsertParameters>
                <asp:Parameter Name="HolidayName" Type="String" />
                <asp:Parameter DbType="Date" Name="StartDate" />
                <asp:Parameter DbType="Date" Name="EndDate" />
                <asp:Parameter Name="isKL" Type="String" />
                <asp:Parameter Name="isPenang" Type="String" />
                <asp:Parameter Name="isPerak" Type="String" />
                <asp:Parameter Name="isJohor" Type="String" />
                <asp:Parameter Name="isPahang" Type="String" />
                <asp:Parameter Name="isSabah" Type="String" />
            </InsertParameters>
            <UpdateParameters>
                <asp:Parameter Name="HolidayName" Type="String" />
                <asp:Parameter DbType="Date" Name="StartDate" />
                <asp:Parameter DbType="Date" Name="EndDate" />
                <asp:Parameter Name="isKL" Type="String" />
                <asp:Parameter Name="isPenang" Type="String" />
                <asp:Parameter Name="isPerak" Type="String" />
                <asp:Parameter Name="isJohor" Type="String" />
                <asp:Parameter Name="isPahang" Type="String" />
                <asp:Parameter Name="isSabah" Type="String" />
                <asp:Parameter Name="original_HolidayID" Type="Int32" />
                <asp:Parameter Name="original_HolidayName" Type="String" />
                <asp:Parameter DbType="Date" Name="original_StartDate" />
                <asp:Parameter DbType="Date" Name="original_EndDate" />
                <asp:Parameter Name="original_isKL" Type="String" />
                <asp:Parameter Name="original_isPenang" Type="String" />
                <asp:Parameter Name="original_isPerak" Type="String" />
                <asp:Parameter Name="original_isJohor" Type="String" />
                <asp:Parameter Name="original_isPahang" Type="String" />
                <asp:Parameter Name="original_isSabah" Type="String" />
            </UpdateParameters>
        </asp:SqlDataSource>

    </div>
</asp:Content>
