<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VenueMaintenance.aspx.cs" Inherits="FYP.VenueMaintenance" MasterPageFile="~/Site1.Master" %>


<asp:Content runat="server" ContentPlaceHolderID="MainContent">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div>
                <h2>Exam Venue Maintenance
                </h2>
                <hr />
            </div>


            <div class="form-horizontal col-md-12">
                <div class="form-group">
                    <label class="control-label col-md-1">Campus</label>
                    <div class="col-md-7">
                        <asp:DropDownList ID="ddl_Campus" runat="server" CssClass="form-control" Width="25%" DataTextField="CampusName" DataValueField="CampusName" AutoPostBack="True" OnSelectedIndexChanged="ddl_Campus_SelectedIndexChanged" OnDataBinding="ddl_Campus_DataBinding" OnDataBound="ddl_Campus_DataBound"></asp:DropDownList>
                    </div>

                </div>

                <div class="form-group">
                    <label class="control-label col-md-1">Block</label>
                    <div class="col-md-7">
                        <asp:DropDownList ID="ddl_Block" runat="server" CssClass="form-control" Width="25%" AutoPostBack="True" DataTextField="BlockCode" DataValueField="BlockCode" OnDataBound="ddl_Block_DataBound" OnSelectedIndexChanged="ddl_Block_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                    <a href="AddVenue.aspx" class="btn btn-success pull-right"><span class="" style="padding: 0px; vertical-align: top" data-toggle="tooltip" title="Add new examination venue">Add Exam Venue</span> </span></a>
                </div>
            </div>



            <asp:GridView ID="GridView1" runat="server" CssClass="table table-hover table-responsive table-striped" AllowPaging="True" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" DataKeyNames="VenueID" OnPageIndexChanging="GridView1_PageIndexChanging" OnRowEditing="GridView1_RowEditing" OnRowDeleting="GridView1_RowDeleting" GridLines="None">
                <Columns>
                    <asp:TemplateField HeaderText="Exam Venue" SortExpression="VenueID">
                        <EditItemTemplate>
                            <asp:Label ID="lbl_VenueID" runat="server" Text='<%# Eval("VenueID") %>'></asp:Label>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lbl_VenueID" runat="server" Text='<%# Bind("VenueID") %>' CssClass=""></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="RoomAssigned" HeaderText="Room Assigned" SortExpression="RoomAssigned" ReadOnly="True" />
                    <asp:BoundField DataField="Capacity" HeaderText="Capacity" SortExpression="Capacity" />
                    <asp:BoundField DataField="Floor" HeaderText="Floor" SortExpression="Floor" />
                    <asp:TemplateField ShowHeader="False">
                        <EditItemTemplate>
                            <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="True" CommandName="Update" Text="Update"></asp:LinkButton>
                            &nbsp;<asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel"></asp:LinkButton>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <div class="pull-right">
                                <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Edit" Text="Edit" CssClass="btn btn-info btn-sm" data-toggle="tooltip" title="Edit this particular record"><span class="glyphicon glyphicon-pencil"></span></asp:LinkButton>
                                &nbsp;<asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="Delete" Text="Delete" CssClass="btn btn-danger btn-sm" data-toggle="tooltip" title="Delete this particular record"><span class="glyphicon glyphicon-trash"></asp:LinkButton>
                            </div>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <PagerSettings Mode="NumericFirstLast" />
                <PagerStyle CssClass="pagination-ys list-unstyled" />
            </asp:GridView>

            <asp:SqlDataSource ID="VenueDS" runat="server" ConflictDetection="CompareAllValues" ConnectionString="<%$ ConnectionStrings:Examination %>" DeleteCommand="DELETE FROM [Venue] WHERE [VenueID] = @original_VenueID AND (([Location] = @original_Location) OR ([Location] IS NULL AND @original_Location IS NULL)) AND (([Capacity] = @original_Capacity) OR ([Capacity] IS NULL AND @original_Capacity IS NULL)) AND (([Floor] = @original_Floor) OR ([Floor] IS NULL AND @original_Floor IS NULL)) AND (([MaximumProgramme] = @original_MaximumProgramme) OR ([MaximumProgramme] IS NULL AND @original_MaximumProgramme IS NULL)) AND (([PreferencePaper] = @original_PreferencePaper) OR ([PreferencePaper] IS NULL AND @original_PreferencePaper IS NULL)) AND (([EastOrWest] = @original_EastOrWest) OR ([EastOrWest] IS NULL AND @original_EastOrWest IS NULL)) AND (([ExitLocation] = @original_ExitLocation) OR ([ExitLocation] IS NULL AND @original_ExitLocation IS NULL))" InsertCommand="INSERT INTO [Venue] ([VenueID], [Location], [Capacity], [Floor], [MaximumProgramme], [PreferencePaper], [EastOrWest], [ExitLocation]) VALUES (@VenueID, @Location, @Capacity, @Floor, @MaximumProgramme, @PreferencePaper, @EastOrWest, @ExitLocation)" OldValuesParameterFormatString="original_{0}" SelectCommand="SELECT Venue.VenueID, Stuff((Select ','+ Room.RoomCode from Room room where Room.VenueID = Venue.VenueID for XML PATH('')),1,1,' ') as &quot;RoomAssigned&quot;, Venue.Capacity, Venue.Floor FROM Venue LEFT JOIN Room ON Venue.VenueID = Room.VenueID
WHERE Venue.Location = @Block
Group by Venue.VenueID, Venue.Capacity , Venue.Floor
Order by (substring(Venue.VenueID,1,1)),
case when isNumeric(substring(Venue.VenueID,2,1)) = 1 THEN substring(Venue.VenueID,3,1)
when isNumeric(substring(Venue.VenueID,2,1)) = 0 THEN substring(Venue.VenueID,4,1)
end, 
substring(Venue.VenueID,2,1),
substring(Venue.VenueID,3,1)
"
                UpdateCommand="UPDATE [Venue] SET [Location] = @Location, [Capacity] = @Capacity, [Floor] = @Floor, [MaximumProgramme] = @MaximumProgramme, [PreferencePaper] = @PreferencePaper, [EastOrWest] = @EastOrWest, [ExitLocation] = @ExitLocation WHERE [VenueID] = @original_VenueID AND (([Location] = @original_Location) OR ([Location] IS NULL AND @original_Location IS NULL)) AND (([Capacity] = @original_Capacity) OR ([Capacity] IS NULL AND @original_Capacity IS NULL)) AND (([Floor] = @original_Floor) OR ([Floor] IS NULL AND @original_Floor IS NULL)) AND (([MaximumProgramme] = @original_MaximumProgramme) OR ([MaximumProgramme] IS NULL AND @original_MaximumProgramme IS NULL)) AND (([PreferencePaper] = @original_PreferencePaper) OR ([PreferencePaper] IS NULL AND @original_PreferencePaper IS NULL)) AND (([EastOrWest] = @original_EastOrWest) OR ([EastOrWest] IS NULL AND @original_EastOrWest IS NULL)) AND (([ExitLocation] = @original_ExitLocation) OR ([ExitLocation] IS NULL AND @original_ExitLocation IS NULL))">
                <DeleteParameters>
                    <asp:Parameter Name="original_VenueID" Type="String" />
                    <asp:Parameter Name="original_Location" Type="String" />
                    <asp:Parameter Name="original_Capacity" Type="Int32" />
                    <asp:Parameter Name="original_Floor" Type="Int32" />
                    <asp:Parameter Name="original_MaximumProgramme" Type="Int32" />
                    <asp:Parameter Name="original_PreferencePaper" Type="String" />
                    <asp:Parameter Name="original_EastOrWest" Type="String" />
                    <asp:Parameter Name="original_ExitLocation" Type="String" />
                </DeleteParameters>
                <InsertParameters>
                    <asp:Parameter Name="VenueID" Type="String" />
                    <asp:Parameter Name="Location" Type="String" />
                    <asp:Parameter Name="Capacity" Type="Int32" />
                    <asp:Parameter Name="Floor" Type="Int32" />
                    <asp:Parameter Name="MaximumProgramme" Type="Int32" />
                    <asp:Parameter Name="PreferencePaper" Type="String" />
                    <asp:Parameter Name="EastOrWest" Type="String" />
                    <asp:Parameter Name="ExitLocation" Type="String" />
                </InsertParameters>
                <SelectParameters>
                    <asp:ControlParameter ControlID="ddl_Block" Name="Block" PropertyName="SelectedValue" />
                </SelectParameters>
                <UpdateParameters>
                    <asp:Parameter Name="Location" Type="String" />
                    <asp:Parameter Name="Capacity" Type="Int32" />
                    <asp:Parameter Name="Floor" Type="Int32" />
                    <asp:Parameter Name="MaximumProgramme" Type="Int32" />
                    <asp:Parameter Name="PreferencePaper" Type="String" />
                    <asp:Parameter Name="EastOrWest" Type="String" />
                    <asp:Parameter Name="ExitLocation" Type="String" />
                    <asp:Parameter Name="original_VenueID" Type="String" />
                    <asp:Parameter Name="original_Location" Type="String" />
                    <asp:Parameter Name="original_Capacity" Type="Int32" />
                    <asp:Parameter Name="original_Floor" Type="Int32" />
                    <asp:Parameter Name="original_MaximumProgramme" Type="Int32" />
                    <asp:Parameter Name="original_PreferencePaper" Type="String" />
                    <asp:Parameter Name="original_EastOrWest" Type="String" />
                    <asp:Parameter Name="original_ExitLocation" Type="String" />
                </UpdateParameters>
            </asp:SqlDataSource>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
