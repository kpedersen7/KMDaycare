<%@ Page Title="" Language="C#" MasterPageFile="~/General.master" Theme="Theme1" AutoEventWireup="true" CodeFile="ChangePassword.aspx.cs" Inherits="ChangePassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Panel runat="server" ID="PageControls">
        <asp:Label runat="server" ID="FeedbackLabel"></asp:Label>
        <asp:Label runat="server">Email: </asp:Label><asp:TextBox runat="server" ID="EmailTB" TextMode="Email"></asp:TextBox>
        <asp:Button runat="server" ID="RequestButton" Text="Request Change Of Password" OnClick="RequestButton_Click" />
        <asp:Label runat="server" ID="NewPasswordLabel">New Password: </asp:Label><asp:TextBox runat="server" ID="NewPasswordTB" TextMode="Password" MaxLength="25"></asp:TextBox>
        <asp:Label runat="server" ID="ConfirmNewPasswordLabel">Confirm New Password: </asp:Label><asp:TextBox runat="server" ID="ConfirmNewPasswordTB" TextMode="Password" MaxLength="25"></asp:TextBox>
        <asp:Button runat="server" ID="SubmitButton" Text="Submit" OnClick="SubmitButton_Click" />
    </asp:Panel>

</asp:Content>

