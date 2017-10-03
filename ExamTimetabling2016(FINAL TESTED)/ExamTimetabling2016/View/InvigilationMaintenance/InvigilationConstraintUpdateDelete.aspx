<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="InvigilationConstraintUpdateDelete.aspx.cs" Inherits="ExamTimetabling2016.InvigilationConstraintUpdateDelete" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <link rel="stylesheet" type="text/css" href="../Resources/css/customize.css" />
    <h1>Constraints</h1>
    <div class="boxD" id="boxD" runat="server">
       
    </div>
    <script type = "text/javascript">
        function Confirm() {
            return confirm("Do you want to delete data?")
        }
    </script>
</asp:Content>