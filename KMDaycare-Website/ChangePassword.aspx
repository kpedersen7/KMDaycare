<%@ Page Title="" Language="C#" MasterPageFile="~/General.master" Theme="Theme1" AutoEventWireup="true" CodeFile="ChangePassword.aspx.cs" Inherits="ChangePassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Panel runat="server" ID="PageControls">
        <asp:Label runat="server" ID="FeedbackLabel"></asp:Label>
        <div class="container">
            <div id="changepassword" style="margin-top: 50px;" class="mainbox col-lg-6 col-lg-offset-3 col-sm-8 col-sm-offset-2">
                <div class="panel panel-info">
                    <div class="panel-heading">
                        <div class="panel-title">Change Password</div>
                    </div>
                    <div style="padding-top: 30px" class="panel-body">
                        <asp:Label runat="server"> Email: </asp:Label>
                        <div style="margin-bottom: 25px" class="input-group">
                            <asp:TextBox runat="server" ID="EmailTB" placeholder="Email" CssClass="form-control" TextMode="Email"></asp:TextBox>

                        </div>
                    </div>

                </div>
                <div style="margin-top: 10px" class="form-group">
                    <div class="col-sm-12 controls">
                        <asp:Button runat="server" ID="RequestButton" Text="Request Change Of Password" OnClick="RequestButton_Click" CssClass="btn btn-primary" />
                    </div>
                </div>

                <div style="padding-top: 30px" class="panel-body">
                    <asp:Label runat="server" ID="NewPasswordLabel"> Password:  </asp:Label>
                    <div style="margin-bottom: 25px" class="input-group">
                        <asp:TextBox runat="server" ID="NewPasswordTB" placeholder="Child First Name" CssClass="form-control" TextMode="Password" MaxLength="25"></asp:TextBox>
                    </div>
                </div>

            </div>
            <div style="padding-top: 30px" class="panel-body">
                <asp:Label runat="server" ID="ConfirmNewPasswordLabel"> Confirm New Password: 
                </asp:Label>
                <div style="margin-bottom: 25px" class="input-group">
                    <asp:TextBox runat="server" ID="ConfirmNewPasswordTB" TextMode="Password" MaxLength="25"></asp:TextBox>
                </div>
            </div>
        </div>
        <div style="margin-top: 10px" class="form-group">
            <div class="col-sm-12 controls">
                <asp:Button runat="server" ID="SubmitButton" Text="Submit" OnClick="SubmitButton_Click" CssClass="btn btn-primary" />
            </div>
        </div>



    </asp:Panel>

</asp:Content>

