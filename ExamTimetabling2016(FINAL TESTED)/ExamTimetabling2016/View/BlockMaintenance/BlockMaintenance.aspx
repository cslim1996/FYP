<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BlockMaintenance.aspx.cs" Inherits="FYP.WebForm1" MasterPageFile="~/Site1.Master" %>

<asp:Content runat="server" ContentPlaceHolderID="MainContent">


    <h1 style="font-family:'Asap'">Block Maintenance</h1>
    <hr />

    <div class="form-horizontal col-md-12">
        <div class="form-group">
            <asp:Button ID="Button1" runat="server" Text="Add New Block" CssClass="btn btn-success pull-right" PostBackUrl="AddBlock.aspx"/>
        </div>
    </div>

    <asp:GridView ID="GridView1"  runat="server"  RowStyle-Font-Names="Asap" CssClass="table table-striped table-responsive" AutoGenerateColumns="False" DataKeyNames="BlockCode" DataSourceID="BlockDataSource" AllowPaging="True" AllowSorting="True" OnRowDeleting="GridView1_RowDeleting" OnRowEditing="GridView1_RowEditing" GridLines="None" ShowHeaderWhenEmpty="True">
        <Columns>
            <asp:TemplateField HeaderText="BlockCode" SortExpression="BlockCode">
                <EditItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("BlockCode") %>'></asp:Label>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("BlockCode") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="TotalFloor" HeaderText="TotalFloor" SortExpression="TotalFloor" />
            <asp:BoundField DataField="Campus" HeaderText="Campus" SortExpression="Campus" />
            <asp:TemplateField ShowHeader="False">
                <EditItemTemplate>
                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="True" CommandName="Update" Text="Update"></asp:LinkButton>
                    &nbsp;<asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel"></asp:LinkButton>
                </EditItemTemplate>
                <ItemTemplate>
                    <div class="pull-right">
                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Edit" Text="Edit" CssClass="btn btn-info btn-sm"><span class="glyphicon glyphicon-pencil"></span></asp:LinkButton>
                        <asp:LinkButton ID="LinkButton3" runat="server" CausesValidation="False" CommandName="Delete" Text="Delete" CssClass="btn btn-danger btn-sm"><span class="glyphicon glyphicon-trash"></span></asp:LinkButton>
                    </div>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <PagerStyle CssClass="pagination-ys" />
    </asp:GridView>
    <asp:SqlDataSource ID="BlockDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:Examination %>" SelectCommand="SELECT * FROM [Block]" ConflictDetection="CompareAllValues" DeleteCommand="DELETE FROM [Block] WHERE [BlockCode] = @original_BlockCode AND (([TotalFloor] = @original_TotalFloor) OR ([TotalFloor] IS NULL AND @original_TotalFloor IS NULL)) AND (([Campus] = @original_Campus) OR ([Campus] IS NULL AND @original_Campus IS NULL))" InsertCommand="INSERT INTO [Block] ([BlockCode], [TotalFloor], [Campus]) VALUES (@BlockCode, @TotalFloor, @Campus)" OldValuesParameterFormatString="original_{0}" UpdateCommand="UPDATE [Block] SET [TotalFloor] = @TotalFloor, [Campus] = @Campus WHERE [BlockCode] = @original_BlockCode AND (([TotalFloor] = @original_TotalFloor) OR ([TotalFloor] IS NULL AND @original_TotalFloor IS NULL)) AND (([Campus] = @original_Campus) OR ([Campus] IS NULL AND @original_Campus IS NULL))">
        <DeleteParameters>
            <asp:Parameter Name="original_BlockCode" Type="String" />
            <asp:Parameter Name="original_TotalFloor" Type="Int32" />
            <asp:Parameter Name="original_Campus" Type="String" />
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="BlockCode" Type="String" />
            <asp:Parameter Name="TotalFloor" Type="Int32" />
            <asp:Parameter Name="Campus" Type="String" />
        </InsertParameters>
        <UpdateParameters>
            <asp:Parameter Name="TotalFloor" Type="Int32" />
            <asp:Parameter Name="Campus" Type="String" />
            <asp:Parameter Name="original_BlockCode" Type="String" />
            <asp:Parameter Name="original_TotalFloor" Type="Int32" />
            <asp:Parameter Name="original_Campus" Type="String" />
        </UpdateParameters>
    </asp:SqlDataSource>

</asp:Content>
