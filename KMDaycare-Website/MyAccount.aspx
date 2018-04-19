<%@ Page Title="" Language="C#" MasterPageFile="~/General.master" Theme="Theme1" AutoEventWireup="true" CodeFile="MyAccount.aspx.cs" Inherits="MyAccount" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="clearfix" style="margin:3%;">
        <asp:Label runat="server" ID="FeedbackLabel"></asp:Label>
        <asp:Table runat="server" ID="FoundUsersTable">
            <asp:TableRow ID="MemberSearch">
                <asp:TableCell>
                    <asp:Label runat="server" ID="MemberSearchLabel">Search for member: </asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox runat="server" ID="MemberSearchTextbox"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:LinkButton runat="server" ID="SearchMemberButton" OnClick="SearchMemberButton_Click">Search</asp:LinkButton>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        <asp:Table runat="server" ID="AccountDetails">
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label runat="server" ID="ChildNameLabel">Child Name: </asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox runat="server" ID="ChildNameTextbox" MaxLength="101"></asp:TextBox>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label runat="server" ID="Parent1NameLabel">Parent 1 Name: </asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox runat="server" ID="Parent1NameTextbox" MaxLength="101"></asp:TextBox>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label runat="server" ID="Parent2NameLabel">Parent 2 Name: </asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox runat="server" ID="Parent2NameTextbox" MaxLength="101"></asp:TextBox>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label runat="server" ID="HomeAddressLabel">Home Address: </asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox runat="server" ID="HomeAddressTextbox" MaxLength="50"></asp:TextBox>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label runat="server" ID="PostalCodeLabel">Postal Code: </asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox runat="server" ID="PostalCodeTextbox" MaxLength="6"></asp:TextBox>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label runat="server" ID="ContactNumberLabel">Contact Number: </asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox runat="server" ID="ContactNumberTextbox" MaxLength="10"></asp:TextBox>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
                    <a href="ChangePassword.aspx"></a>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow runat="server" ID="ButtonRow">
                <asp:TableCell>
                    <asp:Button runat="server" ID="SubmitUpdateButton" OnClick="SubmitUpdateButton_Click" Text="Submit" />
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
    </div>
</asp:Content>

