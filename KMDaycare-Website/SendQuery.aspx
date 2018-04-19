<%@ Page Title="" Language="C#" MasterPageFile="~/General.master" AutoEventWireup="true" Theme="Theme1" CodeFile="SendQuery.aspx.cs" Inherits="SendQuery" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container">
        <div id="loginbox" style="margin-top: 50px;" class="mainbox col-lg-6 col-lg-offset-3 col-sm-8 col-sm-offset-2">
            <div class="panel panel-info">
                <div class="panel-heading">
                    <div class="panel-title">Contact Us</div>
                </div>

                <div style="padding-top: 30px" class="panel-body">


                    <asp:Label ID="lblmsg" runat="server"></asp:Label>
                    <asp:Panel ID="Panel1" runat="server" DefaultButton="btnSend">
                        <p>
                            Please Fill the Following to Contact us.
                        </p>
                        <div style="margin-bottom: 25px" class="input-group">
                            Your name:
        <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ErrorMessage="Name is Rquired" ForeColor="Red"
            ControlToValidate="YourName" ValidationGroup="save" /><br />
                            <asp:TextBox ID="YourName" runat="server" CssClass="form-control" Width="250px" /><br />
                            </div>
                          <div style="margin-bottom: 25px" class="input-group">
                            Your email address:
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Email is Rquired" ForeColor="Red"
            ControlToValidate="YourEmail" ValidationGroup="save" /><br />
                            <asp:TextBox ID="YourEmail" runat="server" CssClass="form-control" Width="250px" />
                            <asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator23"
                                SetFocusOnError="true" Text="Example: username@gmail.com" ControlToValidate="YourEmail"
                                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" Display="Dynamic"
                                ValidationGroup="save" /><br />
                              </div>
                          <div style="margin-bottom: 25px" class="input-group">
                            Subject:
        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Subject is required" ForeColor="Red"
            ControlToValidate="YourSubject" ValidationGroup="save" /><br />
                            <asp:TextBox ID="YourSubject" CssClass="form-control" runat="server" Width="400px" /><br />
                              </div>
                        <div style="margin-bottom: 25px" class="input-group">
                            Your Question:
        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Your Quesiton is required" ForeColor="Red"
            ControlToValidate="Comments" ValidationGroup="save" /><br />
                            <asp:TextBox ID="Comments" CssClass="form-control" runat="server"
                                TextMode="MultiLine" Rows="10" Width="400px" />
                       </div>
                        <p>
                            <asp:Button ID="btnSend" runat="server" Text="Send" CssClass="btn btn-primary" OnClick="btnSend_Click" ValidationGroup="save" />
                        </p>
                    </asp:Panel>
                    <p>
                        <asp:Label ID="DisplayMessage" runat="server" Visible="false" />
                    </p>
                </div>
            </div>
        </div>
    </div>

</asp:Content>

