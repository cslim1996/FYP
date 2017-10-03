<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="AddVenue.aspx.cs" Inherits="FYP.Venue_Maintenance.AddVenue" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">


    <h2>Add Exam Venue</h2>
    <hr />

    <div runat="server" id="alert" class="alert alert-danger alert-dismissable fade in" role="alertdialog" visible="false">
        <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
        <strong style="">
            <asp:Label ID="lbl_alert" runat="server" Text="Label"></asp:Label></strong>
    </div>

    <div runat="server" id="success_Alert" class="alert alert-success  alert-dismissable fade in" role="alertdialog" visible="false">
        <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
        <strong style="">
            <asp:Label ID="lbl_success" runat="server" Text="Label"></asp:Label>
        </strong>
    </div>

    <div class="form-horizontal">

        <div class="form-group">
            <label class="control-label col-md-2">Block</label>
            <div class="col-md-4">
                <asp:DropDownList ID="dd_block" runat="server" DataSourceID="SqlDataSource3" DataTextField="BlockCode" DataValueField="BlockCode" AutoPostBack="True" OnSelectedIndexChanged="dd_block_SelectedIndexChanged" CssClass="form-control" OnDataBound="dd_block_DataBound"></asp:DropDownList>
                <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:Examination %>" SelectCommand="SELECT [BlockCode] FROM [Block]"></asp:SqlDataSource>
            </div>

            <label class="control-label col-md-2">Preference Paper</label>
            <div class="col-md-4">
                <asp:TextBox ID="txt_prefPaper" runat="server" CssClass="form-control" TextMode="Search" data-toggle="tooltip" title="State the preference paper of the venue"></asp:TextBox>
            </div>

        </div>

        <div class="form-group">
            <label class="control-label col-md-2">Venue ID</label>
            <div class="col-md-4">
                <asp:TextBox ID="txt_Venue" runat="server" CssClass="form-control" TextMode="Search" data-toggle="tooltip" title="Venue ID such as 'H1' or 'PA1'"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="" Display="Dynamic" ControlToValidate="txt_Venue">
                    <div class="text-danger bg-danger" style="width:100%;margin-top:2px;margin-bottom:-10px">
                        <small style="font-family:Lora">* This field is required</small>
                    </div>
                </asp:RequiredFieldValidator>
            </div>

            <label class="control-label col-md-2">Max. Programme</label>
            <div class="col-md-4">
                <asp:TextBox ID="txt_maxProg" runat="server" CssClass="form-control" TextMode="Number" data-toggle="tooltip" title="State the maximum number of programmes from venue"></asp:TextBox>
            </div>

        </div>

        <div class="form-group">
            <label class="control-label col-md-2">Capacity</label>
            <div class="col-md-4">
                <asp:TextBox ID="txt_Cap" runat="server" CssClass="form-control" MaxLength="4" TextMode="Number"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="" ControlToValidate="txt_Cap" Display="Dynamic">
                                        <div class="text-danger bg-danger" style="width:100%;margin-top:2px;margin-bottom:-10px">
                        <small style="font-family:Lora">* This field is required</small>
                    </div>
                </asp:RequiredFieldValidator>
            </div>

            <label class="control-label col-md-2">Exit Location</label>
            <div class="col-md-4">
                <asp:TextBox ID="txt_exitLoc" runat="server" CssClass="form-control" TextMode="Search" data-toggle="tooltip" title="State the direction of exits, such as 'Left' or 'Right'"></asp:TextBox>
            </div>

        </div>

        <div class="form-group">
            <label class="control-label col-md-2">Floor</label>
            <div class="col-md-4">
                <asp:DropDownList ID="dd_floor" runat="server" CssClass="form-control"></asp:DropDownList>
            </div>

            <label class="control-label col-md-2">Max Column.</label>
            <div class="col-md-4">
                <asp:TextBox ID="txt_maxCol" runat="server" CssClass="form-control" TextMode="Number" OnTextChanged="txt_maxCol_TextChanged" data-toggle="tooltip" title="Max Column of Seats"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="" ControlToValidate="txt_maxCol" Display="Dynamic">
                                        <div class="text-danger bg-danger" style="width:100%;margin-top:2px;margin-bottom:-10px">
                        <small style="font-family:Lora">* This field is required</small>
                    </div>
                </asp:RequiredFieldValidator>
            </div>
        </div>

        <div class="form-group">
            <label class="control-label col-md-2">East/West</label>
            <div class="col-md-4">
                <asp:RadioButtonList ID="rbl_eastWest" runat="server" Font-Overline="False" CssClass="table">
                    <asp:ListItem>East</asp:ListItem>
                    <asp:ListItem>West</asp:ListItem>
                </asp:RadioButtonList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="" ControlToValidate="rbl_eastWest" Display="Dynamic">
                                        <div class="text-danger bg-danger" style="width:100%;margin-top:-20px;margin-bottom:0px">
                        <small style="font-family:Lora">* This field is required</small>
                    </div>
                </asp:RequiredFieldValidator>
            </div>

            <label class="control-label col-md-2">Max Row.</label>
            <div class="col-md-4">
                <asp:TextBox ID="txt_maxRow" runat="server" CssClass="form-control" TextMode="Number" data-toggle="tooltip" title="Max Row of Seats"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="" ControlToValidate="txt_maxRow" Display="Dynamic">
                                        <div class="text-danger bg-danger" style="width:100%;margin-top:2px;margin-bottom:-10px">
                        <small style="font-family:Lora">* This field is required</small>
                    </div>
                </asp:RequiredFieldValidator>
            </div>

        </div>

        <div class="form-group">

            <div class="col-md-offset-2 col-md-10">
                <asp:Button ID="btn_Add" runat="server" Text="Add This Venue" CssClass="btn btn-primary" OnClick="btn_Add_Click" />
                <asp:Button ID="Button3" runat="server" Text="Clear" CssClass="btn btn-default" CausesValidation="False" OnClick="Button3_Click" />
            </div>
        </div>

        <div class="form-group">

            <div class="col-md-offset-2 col-md-10" style="margin-bottom: 15px; margin-top: 30px">
                <a href="VenueMaintenance.aspx">Back to Venue Maintenance</a>
            </div>
        </div>
    </div>





</asp:Content>
