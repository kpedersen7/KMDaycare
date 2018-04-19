<%@ Page Title="" Language="C#" MasterPageFile="~/General.master" Theme="Theme1" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container">
        <div id="loginbox" style="margin-top: 50px;" class="mainbox col-lg-6 col-lg-offset-3 col-sm-8 col-sm-offset-2">
            <div class="panel panel-info">
                <div class="panel-heading">
                    <div class="panel-title">Login</div>
                </div>
                <div style="padding-top: 30px" class="panel-body">
                    <asp:Label ID="lblmsg" runat="server"></asp:Label>
                    <div style="margin-bottom: 25px" class="input-group">
                        <asp:TextBox ID="txtUserName" runat="server" CssClass="form-control" placeholder="Email"></asp:TextBox>
                    </div>
                    <div style="margin-bottom: 25px" class="input-group">
                        <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" placeholder="Password" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div style="margin-top: 10px" class="form-group">
                        <div class="col-sm-12 controls">
                            <asp:Button ID="btnLogin" runat="server" OnClick="btnLogin_Click" CssClass="btn btn-primary" Text="Submit" />
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-lg-12 control">
                            <div style="border-top: 1px solid#888; padding-top: 15px; font-size: 85%"><a href="ChangePassword.aspx">Forgot Your Password?</a></div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

