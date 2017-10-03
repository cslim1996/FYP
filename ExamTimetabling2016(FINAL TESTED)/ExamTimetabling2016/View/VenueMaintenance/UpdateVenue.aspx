<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="UpdateVenue.aspx.cs" Inherits="FYP.Venue_Maintenance.UpdateVenue" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h1>Update Exam Venue</h1>
    <hr />

    <div class="form-horizontal">

        <div class="form-group">
            <label class="control-label col-md-2">Block</label>
            <div class="col-md-4">
                <asp:DropDownList ID="dd_block" runat="server" DataSourceID="SqlDataSource1" DataTextField="BlockCode" DataValueField="BlockCode" AutoPostBack="True" OnSelectedIndexChanged="dd_block_SelectedIndexChanged" CssClass="form-control" OnDataBound="dd_block_DataBound" Width="76%"></asp:DropDownList>
                
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:Examination %>" SelectCommand="SELECT * FROM [Block]"></asp:SqlDataSource>
                
            </div>

            <label class="control-label col-md-2">Pref. Paper</label>
            <div class="col-md-4">
                <asp:TextBox ID="txt_prefPaper" runat="server" CssClass="form-control"></asp:TextBox>
            </div>

        </div>

        <div class="form-group">
            <label class="control-label col-md-2">Venue ID</label>
            <div class="col-md-4">
                <asp:TextBox ID="txt_Venue" runat="server" CssClass="form-control" Width="76%"></asp:TextBox>
            </div>

            <label class="control-label col-md-2">Max. Programme</label>
            <div class="col-md-4">
                <asp:TextBox ID="txt_maxProg" runat="server" CssClass="form-control"></asp:TextBox>
            </div>

        </div>

        <div class="form-group">
            <label class="control-label col-md-2">Capacity</label>
            <div class="col-md-4">
                <asp:TextBox ID="txt_Cap" runat="server" CssClass="form-control" Width="76%"></asp:TextBox>
            </div>

            <label class="control-label col-md-2">Exit Location</label>
            <div class="col-md-4">
                <asp:TextBox ID="txt_exitLoc" runat="server" CssClass="form-control"></asp:TextBox>
            </div>

        </div>

        <div class="form-group">
            <label class="control-label col-md-2">Floor</label>
            <div class="col-md-10">
                <asp:DropDownList ID="dd_floor" runat="server" CssClass="form-control" Width="29%"></asp:DropDownList>
            </div>
        </div>

        <div class="form-group">
            <label class="control-label col-md-2">East/West</label>
            <div class="col-md-10">
                <asp:RadioButtonList ID="rbl_eastWest" runat="server" BorderStyle="None" Font-Overline="False" CssClass="table table-bordered table-striped table-hover table-condensed" Width="29%">
                    <asp:ListItem Value="E">East</asp:ListItem>
                    <asp:ListItem Value="W">West</asp:ListItem>
                </asp:RadioButtonList>
            </div>

        </div>

        <div class="form-group">

            <div class="col-md-offset-2 col-md-10">
                <asp:Button ID="btn_Update" runat="server" Text="Update This Venue" CssClass="btn btn-warning" OnClick="btn_Update_Click"/>
                <asp:Button ID="Button3" runat="server" Text="Cancel" CssClass="btn btn-default" PostBackUrl="VenueMaintenance.aspx" />
            </div>
        </div>
    </div>

</asp:Content>
