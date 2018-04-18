<%@ Page Title="" Language="C#" MasterPageFile="~/General.master" Theme="Theme1" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="clearfix">
        <asp:Label runat="server">Username: </asp:Label>
        <asp:TextBox ID="txtUserName" runat="server" MaxLength="50"></asp:TextBox>
        <asp:Label runat="server">Password: </asp:Label>
        <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" MaxLength="25"></asp:TextBox>
        <asp:Button ID="btnLogin" runat="server" OnClick="btnLogin_Click" Text="Submit" />
        <asp:Label ID="lblmsg" runat="server"></asp:Label>
    </div>
</asp:Content>

