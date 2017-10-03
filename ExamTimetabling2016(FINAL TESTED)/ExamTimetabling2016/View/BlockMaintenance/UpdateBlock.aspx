<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UpdateBlock.aspx.cs" Inherits="FYP.Venue_Maintenance.UpdateBlock" MasterPageFile="~/Site1.Master"%>

<asp:Content runat="server" ContentPlaceHolderID="MainContent"> 
    <h2>Update Block
    </h2>
    <hr />

    <div class="form-horizontal">

        <div class="form-group">
            <label class="control-label col-md-2">Block Code</label>
            <div class="col-md-10">
                <asp:TextBox ID="txt_BlockCode" runat="server" CssClass="form-control" Font-Bold="True" Font-Names="Century Gothic"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="" Display="Dynamic" ControlToValidate="txt_BlockCode">
                    <div class="text-danger bg-danger" style="width:76%;margin-top:2px;margin-bottom:-10px">
                        <small style="font-family:Lora">* This field is required</small>
                    </div>
                </asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="form-group">
            <label class="control-label col-md-2">Total Floor</label>
            <div class="col-md-10">
                <asp:TextBox ID="txt_Floor" runat="server" CssClass="form-control" Font-Bold="True" Font-Names="Century Gothic"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="" Display="Dynamic" ControlToValidate="txt_Floor">
                    <div class="text-danger bg-danger" style="width:76%;margin-top:2px;margin-bottom:-10px">
                        <small style="font-family:Lora">* This field is required</small>
                    </div>
                </asp:RequiredFieldValidator>
            </div>
        </div>

        <div class="form-group">
            <label class="control-label col-md-2">Campus</label>
            <div class="col-md-10">
                <asp:DropDownList ID="ddl_Campus" runat="server" Width="29%" CssClass="form-control" DataSourceID="CampusDS" DataTextField="CampusName" DataValueField="CampusName"></asp:DropDownList>
                <asp:SqlDataSource ID="CampusDS" runat="server" ConnectionString="Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Examination.mdf;Integrated Security=True" ProviderName="System.Data.SqlClient" SelectCommand="SELECT [CampusName] FROM [Campus]"></asp:SqlDataSource>
            </div>

        </div>


        <div class="form-group">

            <div class="col-md-offset-2 col-md-10">
                <asp:Button ID="btn_update" runat="server" Text="Update" CssClass="btn btn-info" OnClick="btn_update_Click"/>
            </div>
        </div>

        <div class="form-group">

            <div class="col-md-offset-2 col-md-10" style="margin-bottom: 15px">
                <a href="BlockMaintenance.aspx">Back to List</a>
            </div>
        </div>


    </div>
</asp:Content>