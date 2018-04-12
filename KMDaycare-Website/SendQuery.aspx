<%@ Page Title="" Language="C#" MasterPageFile="~/General.master" AutoEventWireup="true" CodeFile="SendQuery.aspx.cs" Inherits="SendQuery" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
        <h2>Contact Us
        </h2>
        <asp:Panel ID="Panel1" runat="server" DefaultButton="btnSend">
            <p>
                Please Fill the Following to Contact us.
            </p>
            <p>
                Your name:
        <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ErrorMessage="Name is Rquired" ForeColor="Red"
            ControlToValidate="YourName" ValidationGroup="save" /><br />
                <asp:TextBox ID="YourName" runat="server" Width="250px" /><br />
                Your email address:
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Email is Rquired" ForeColor="Red"
            ControlToValidate="YourEmail" ValidationGroup="save" /><br />
                <asp:TextBox ID="YourEmail" runat="server" Width="250px" />
                <asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator23"
                    SetFocusOnError="true" Text="Example: username@gmail.com" ControlToValidate="YourEmail"
                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" Display="Dynamic"
                    ValidationGroup="save" /><br />
                Subject:
        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Subject is required" ForeColor="Red"
            ControlToValidate="YourSubject" ValidationGroup="save" /><br />
                <asp:TextBox ID="YourSubject" runat="server" Width="400px" /><br />
                Your Question:
        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Your Quesiton is required" ForeColor="Red"
            ControlToValidate="Comments" ValidationGroup="save" /><br />
                <asp:TextBox ID="Comments" runat="server"
                    TextMode="MultiLine" Rows="10" Width="400px" />
            </p>
            <p>
                <asp:Button ID="btnSend" runat="server" Text="Send" OnClick="btnSend_Click" ValidationGroup="save" />
            </p>
        </asp:Panel>
        <p>
            <asp:Label ID="DisplayMessage" runat="server" Visible="false" />
        </p>
  
</asp:Content>

