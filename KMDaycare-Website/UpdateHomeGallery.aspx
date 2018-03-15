<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.master" AutoEventWireup="true" CodeFile="UpdateHomeGallery.aspx.cs" Inherits="UpdateHomeGallery" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <form id="form1" runat="server" cssclass="form-heading">
        <asp:Label runat="server">Image 1:</asp:Label><asp:FileUpload runat="server" ID="fileupload1" /><br />
        <asp:Label runat="server">Image 2:</asp:Label><asp:FileUpload runat="server" ID="fileupload2" /><br />
        <asp:Label runat="server">Image 3:</asp:Label><asp:FileUpload runat="server" ID="fileupload3" /><br />
        <asp:Button ID="btnsave" runat="server" OnClick="btnsave_Click" Text="Save" Style="width: 85px" />
        <asp:Label ID="feedbackLabel" runat="server" />
    </form>
</asp:Content>

