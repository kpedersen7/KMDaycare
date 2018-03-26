<%@ Page Title="" Language="C#" MasterPageFile="~/General.master" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    Email <asp:TextBox ID="txtEmail" runat="server" ></asp:TextBox>
    Password : <asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox>
    <asp:Button ID="btnLogin" runat="server" OnClick="btnLogin_Click" Text="Submit"/>
    <asp:Label ID="lblmsg" runat="server" ></asp:Label>
</asp:Content>

