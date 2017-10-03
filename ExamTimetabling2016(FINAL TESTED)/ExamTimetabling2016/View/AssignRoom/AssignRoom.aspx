<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="AssignRoom.aspx.cs" Inherits="FYP.Venue_Maintenance.AssignRoom" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">



            <h1>Assign Room(s) To Venue
            </h1>
            <hr />

            <div class="form-horizontal">
                <div class="form-group">
                    <label class="control-label col-md-2">Block</label>
                    <div class="col-md-10">
                        <asp:DropDownList ID="ddl_Block" runat="server" CssClass="form-control" Width="29%" DataSourceID="BlockDS" DataTextField="BlockCode" DataValueField="BlockCode" AutoPostBack="True" OnDataBound="ddl_Block_DataBound" OnSelectedIndexChanged="ddl_Block_SelectedIndexChanged"></asp:DropDownList>
                        <asp:SqlDataSource ID="BlockDS" runat="server" ConnectionString="<%$ ConnectionStrings:Examination %>" SelectCommand="SELECT * FROM [Block]"></asp:SqlDataSource>
                    </div>
                </div>

                <div class="form-group">
                    <label class="control-label col-md-2">Venue</label>
                    <div class="col-md-10">
                        <asp:DropDownList ID="ddl_Venue" runat="server" CssClass="form-control" Width="29%" AutoPostBack="True" OnDataBound="ddl_Venue_DataBound" OnSelectedIndexChanged="ddl_Venue_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                </div>


                <div class="form-group">
                    <div class="col-md-6">
                        <h1><small>Assigned Room</small></h1>
                        <hr />
                        <asp:ListView ID="ListView1" runat="server" DataKeyNames="RoomCode" GroupItemCount="3" OnItemDeleting="ListView1_ItemDeleting">
                            <AlternatingItemTemplate>
                                <td runat="server" style="">
                                    <table class="table table-bordered table-condensed" style="width: 185px; height: 150px">
                                        <tr>
                                            <td colspan="2" style="background-color: cornflowerblue; text-align: center; color: white; font: bolder; vertical-align: middle">
                                                <asp:Label ID="RoomCodeLabel" runat="server" Text='<%# Eval("RoomCode") %>' Font-Bold="True" /></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 15%">Func.</td>
                                            <td>
                                                <asp:Label ID="FunctionLabel" runat="server" Text='<%# Eval("Function") %>' /></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 15%">Floor</td>
                                            <td>
                                                <asp:Label ID="FloorLabel" runat="server" Text='<%# Eval("Floor") %>' /></td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <asp:Button ID="DeleteButton" runat="server" CommandName="Delete" Text="Unassign" CssClass="btn btn-block btn-danger btn-sm" /></td>
                                        </tr>
                                    </table>
                                </td>
                            </AlternatingItemTemplate>
                            <EmptyDataTemplate>
                                <table runat="server" style="text-align: center">
                                    <tr>
                                        <td style="">No room(s) were assigned.</td>
                                    </tr>
                                </table>
                            </EmptyDataTemplate>
                            <EmptyItemTemplate>
                                <td runat="server" />
                            </EmptyItemTemplate>
                            <GroupTemplate>
                                <tr id="itemPlaceholderContainer" runat="server">
                                    <td id="itemPlaceholder" runat="server"></td>
                                </tr>
                            </GroupTemplate>
                            <ItemTemplate>
                                <td runat="server" style="">
                                    <table class="table table-bordered table-condensed" style="width: 185px; height: 150px">
                                        <tr>
                                            <td colspan="2" style="background-color: cornflowerblue; text-align: center; color: white; font: bolder; vertical-align: middle">
                                                <asp:Label ID="RoomCodeLabel" runat="server" Text='<%# Eval("RoomCode") %>' Font-Bold="True" /></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 15%">Func.</td>
                                            <td>
                                                <asp:Label ID="FunctionLabel" runat="server" Text='<%# Eval("Function") %>' /></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 15%">Floor</td>
                                            <td>
                                                <asp:Label ID="FloorLabel" runat="server" Text='<%# Eval("Floor") %>' /></td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <asp:Button ID="DeleteButton" runat="server" CommandName="Delete" Text="Unassign" CssClass="btn btn-block btn-danger btn-sm" /></td>
                                        </tr>
                                    </table>
                                </td>
                            </ItemTemplate>
                            <LayoutTemplate>
                                <table runat="server">
                                    <tr runat="server">
                                        <td runat="server">
                                            <table id="groupPlaceholderContainer" runat="server" border="0" style="">
                                                <tr id="groupPlaceholder" runat="server">
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr runat="server">
                                        <td runat="server" style="">
                                            <asp:DataPager ID="DataPager1" runat="server" PageSize="12">
                                                <Fields>
                                                    <asp:NextPreviousPagerField ButtonType="Button" ShowFirstPageButton="True" ShowNextPageButton="False" ShowPreviousPageButton="False" ButtonCssClass="btn btn-default btn-xs" />
                                                    <asp:NumericPagerField />
                                                    <asp:NextPreviousPagerField ButtonType="Button" ShowLastPageButton="True" ShowNextPageButton="False" ShowPreviousPageButton="False" ButtonCssClass="btn btn-default btn-xs" />
                                                </Fields>
                                            </asp:DataPager>
                                        </td>
                                    </tr>
                                </table>
                            </LayoutTemplate>
                        </asp:ListView>
                    </div>

                    <div class="col-md-6">
                        <h1><small>Unassigned Room</small></h1>
                        <hr />

                        <asp:ListView ID="ListView2" runat="server" DataKeyNames="RoomCode" GroupItemCount="3" OnItemDeleting="ListView2_ItemDeleting">
                            <AlternatingItemTemplate>
                                <td runat="server" style="">
                                    <table class="table table-bordered table-condensed" style="width: 185px; height: 150px">
                                        <tr>
                                            <td colspan="2" style="background-color: cornflowerblue; text-align: center; color: white; font: bolder; vertical-align: middle">
                                                <asp:Label ID="RoomCodeLabel" runat="server" Text='<%# Eval("RoomCode") %>' Font-Bold="True" /></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 15%">Func.</td>
                                            <td>
                                                <asp:Label ID="FunctionLabel" runat="server" Text='<%# Eval("Function") %>' /></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 15%">Floor</td>
                                            <td>
                                                <asp:Label ID="FloorLabel" runat="server" Text='<%# Eval("Floor") %>' /></td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <asp:Button ID="DeleteButton" runat="server" CommandName="Delete" Text="Assign" CssClass="btn btn-block btn-success btn-sm" /></td>
                                        </tr>
                                    </table>
                                </td>
                            </AlternatingItemTemplate>
                            <EmptyDataTemplate>
                                <table runat="server" style="">
                                    <tr>
                                        <td>All room(s) were assigned.</td>
                                    </tr>
                                </table>
                            </EmptyDataTemplate>
                            <EmptyItemTemplate>
                                <td runat="server" />
                            </EmptyItemTemplate>
                            <GroupTemplate>
                                <tr id="itemPlaceholderContainer" runat="server">
                                    <td id="itemPlaceholder" runat="server"></td>
                                </tr>
                            </GroupTemplate>
                            <ItemTemplate>
                                <td runat="server" style="">
                                    <table class="table table-bordered table-condensed" style="width: 185px; height: 150px">
                                        <tr>
                                            <td colspan="2" style="background-color: cornflowerblue; text-align: center; color: white; font: bolder; vertical-align: middle">
                                                <asp:Label ID="RoomCodeLabel" runat="server" Text='<%# Eval("RoomCode") %>' Font-Bold="True" /></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 15%">Func.</td>
                                            <td>
                                                <asp:Label ID="FunctionLabel" runat="server" Text='<%# Eval("Function") %>' /></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 15%">Floor</td>
                                            <td>
                                                <asp:Label ID="FloorLabel" runat="server" Text='<%# Eval("Floor") %>' /></td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <asp:Button ID="DeleteButton" runat="server" CommandName="Delete" Text="Assign" CssClass="btn btn-block btn-success btn-sm" /></td>
                                        </tr>
                                    </table>
                                </td>
                            </ItemTemplate>
                            <LayoutTemplate>
                                <table runat="server">
                                    <tr runat="server">
                                        <td runat="server">
                                            <table id="groupPlaceholderContainer" runat="server" border="0" style="">
                                                <tr id="groupPlaceholder" runat="server">
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr runat="server">
                                        <td runat="server" style="">
                                            <asp:DataPager ID="DataPager1" runat="server" PageSize="12">
                                                <Fields>
                                                    <asp:NextPreviousPagerField ButtonType="Button" ShowFirstPageButton="True" ShowNextPageButton="False" ShowPreviousPageButton="False" ButtonCssClass="btn btn-default btn-xs" />
                                                    <asp:NumericPagerField />
                                                    <asp:NextPreviousPagerField ButtonType="Button" ShowLastPageButton="True" ShowNextPageButton="False" ShowPreviousPageButton="False" ButtonCssClass="btn btn-default btn-xs" />
                                                </Fields>
                                            </asp:DataPager>
                                        </td>
                                    </tr>
                                </table>
                            </LayoutTemplate>
                        </asp:ListView>

                    </div>
                </div>

                <div class="form-group">
                </div>


            </div>



</asp:Content>
