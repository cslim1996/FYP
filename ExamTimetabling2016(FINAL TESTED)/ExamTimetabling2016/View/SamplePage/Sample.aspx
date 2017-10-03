<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Sample.aspx.cs" MasterPageFile="~/Site1.Master" Inherits="ExamTimetabling2016.View.Sample" %>

<asp:Content runat="server" ContentPlaceHolderID="MainContent">




      <h1 style="font-family:'Asap'">Sample Page</h1>
    <hr />

     <div class="form-horizontal col-md-12">
        <div class="form-group">
            <asp:Button ID="Button1" runat="server" Text="Add Button" CssClass="btn btn-success pull-right"/>
        </div>
    </div>

    <asp:GridView ID="GridView1" runat="server" CssClass="table table-hover table-responsive table-striped" ShowHeaderWhenEmpty="true">

    </asp:GridView>


    <div class="jumbotron" style="margin-top:80px;">
        <h1>Sample Content</h1>
    </div>

</asp:Content>