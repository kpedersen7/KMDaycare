<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.master" Theme="Theme1" AutoEventWireup="true" CodeFile="CreateAccount.aspx.cs" Inherits="CreateAccount" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="col clearfix">
        <asp:Label runat="server" ID="feedback"></asp:Label>

        <asp:Label runat="server">Child First Name</asp:Label>
        <asp:TextBox runat="server" ID="ChildFirstName"></asp:TextBox>

        <asp:Label runat="server">Child Last Name</asp:Label>
        <asp:TextBox runat="server" ID="ChildLastName"></asp:TextBox>

        <asp:Label runat="server">Parent 1 First Name</asp:Label>
        <asp:TextBox runat="server" ID="Parent1FirstName"></asp:TextBox>

        <asp:Label runat="server">Parent 1 Last Name</asp:Label>
        <asp:TextBox runat="server" ID="Parent1LastName"></asp:TextBox>

        <asp:Label runat="server">Parent 2 First Name</asp:Label>
        <asp:TextBox runat="server" ID="Parent2FirstName"></asp:TextBox>

        <asp:Label runat="server">Parent 2 Last Name</asp:Label>
        <asp:TextBox runat="server" ID="Parent2LastName"></asp:TextBox>

        <asp:Label runat="server">Home Address</asp:Label>
        <asp:TextBox runat="server" ID="HomeAddress"></asp:TextBox>

        <asp:Label runat="server">Postal Code</asp:Label>
        <asp:TextBox runat="server" ID="PostalCode"></asp:TextBox>

        <asp:Label runat="server">Contact Number</asp:Label>
        <asp:TextBox runat="server" ID="EmergencyContactNumber"></asp:TextBox>

        <asp:Label runat="server">Email</asp:Label>
        <asp:TextBox runat="server" ID="Email"></asp:TextBox>

        <asp:Label runat="server">Password</asp:Label>
        <asp:TextBox runat="server" ID="Password"></asp:TextBox>

        <asp:Label runat="server">Role</asp:Label>
        <asp:DropDownList runat="server" ID="RoleDD">
            <asp:ListItem Value="1">Admin</asp:ListItem>
            <asp:ListItem Value="2">Parent</asp:ListItem>
            <asp:ListItem Value="3">Staff</asp:ListItem>
        </asp:DropDownList>
        <asp:Button runat="server" ID="SubmitButton" OnClick="SubmitButton_Click" Text="Submit" />
    </div>
</asp:Content>

