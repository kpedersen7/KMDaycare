<%@ Page Title="" Language="C#" MasterPageFile="~/General.master" AutoEventWireup="true" Theme="Theme1" CodeFile="SendQuery.aspx.cs" Inherits="SendQuery" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--  <img src="HomeGallery/KMDayCareBanner1.jpg" class="pull-left thumbnail" alt="Responsive image" style="height: 150px; margin-right:5px;"/>--%>
    <div class="SomeClass">
        We’d excited to hear from you. Please feel free to contact us.
                <p>Address: 1047 Knottwood Rd E NW,Edmonton,AB, T6K 3N5</p>
        <p>Phone: (780) 461-3320 </p>
        <p>Email: knotwoodmontessori@gmail.com </p>
    </div>
    <div class="container">
        <div id="loginbox" style="margin-top: 50px;" class="mainbox col-lg-6 col-lg-offset-3 col-sm-8 col-sm-offset-2">
            <div class="panel panel-info">
                <div class="panel-heading">
                    <div class="panel-title">Contact Us</div>
                </div>

                <div style="padding-top: 30px" class="panel-body">
                    <asp:Label ID="lblmsg" runat="server"></asp:Label>
                    <asp:Panel ID="Panel1" runat="server" DefaultButton="btnSend">
                        <p class="SomeClass">
                            Please Fill the Following to Contact Us.
                        </p>
                        <div style="margin-bottom: 25px" class="input-group">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ErrorMessage="Name is Rquired" ForeColor="Red"
                                ControlToValidate="YourName" ValidationGroup="save" /><br />

                            <asp:TextBox ID="YourName" runat="server" placeholder="Your Name" CssClass="form-control" /><br />
                        </div>
                        <div style="margin-bottom: 25px" class="input-group">

                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Email is Rquired" ForeColor="Red"
                                ControlToValidate="YourEmail" ValidationGroup="save" /><br />
                            <asp:TextBox ID="YourEmail" runat="server" placeholder="Email Address" CssClass="form-control" />
                            <asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator23"
                                SetFocusOnError="true" Text="Example: username@gmail.com" ControlToValidate="YourEmail"
                                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" Display="Dynamic"
                                ValidationGroup="save" /><br />
                        </div>
                        <div style="margin-bottom: 25px" class="input-group">

                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Subject is required" ForeColor="Red"
                                ControlToValidate="YourSubject" ValidationGroup="save" /><br />
                            <asp:TextBox ID="YourSubject" placeholder=" Subject" CssClass="form-control" runat="server" /><br />
                        </div>
                        <div style="margin-bottom: 25px" class="input-group">

                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Your Quesiton is required" ForeColor="Red"
                                ControlToValidate="Comments" ValidationGroup="save" /><br />
                            <asp:TextBox ID="Comments" placeholder="Your Question" CssClass="form-control" runat="server"
                                TextMode="MultiLine" Rows="10" />
                        </div>
                        <div style="margin-top: 10px" class="form-group">
                            <div class="col-sm-12 controls">
                                <asp:Button ID="btnSend" runat="server" Text="Send" CssClass="btn btn-primary" OnClick="btnSend_Click" ValidationGroup="save" />
                            </div>
                        </div>
                    </asp:Panel>
                    <p>
                        <div style="padding-top: 30px" class="panel-body">
                            <asp:Label ID="DisplayMessage" runat="server" Visible="false" />
                        </div>
                    </p>
                </div>
            </div>
        </div>
    </div>


</asp:Content>

