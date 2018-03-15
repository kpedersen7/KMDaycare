<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.master" Theme="Theme1" AutoEventWireup="true" CodeFile="CreateNewsletter.aspx.cs" Inherits="CreateNewsletter" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
        <form id="form1" runat="server" cssclass="form-heading">
    <asp:FileUpload runat="server" ID="fileupload"/>
    <asp:Button ID="btnsave" runat="server" onclick="btnsave_Click"  Text="Save" style="width:85px" />
    <asp:Label ID="feedbackLabel" runat="server" />
            </form>
</asp:Content>

