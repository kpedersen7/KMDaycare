<%@ Page Title="" Language="C#" MasterPageFile="~/General.master" Theme="Theme1" AutoEventWireup="true" CodeFile="MyAccount.aspx.cs" Inherits="MyAccount" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="loginbox" style="margin-top: 50px;" class="mainbox col-lg-6 col-lg-offset-3 col-sm-8 col-sm-offset-2">
        <div class="panel panel-info">
            <div class="panel-heading">
                <div class="panel-title">View Account</div>
            </div>
            <asp:Label runat="server" ID="FeedbackLabel"></asp:Label>
            <asp:Panel ID="MemberSearchPanel" runat="server">
                <div style="padding-top: 30px" class="panel-body">
                    <asp:Label ID="lblmsg" runat="server"></asp:Label>
                    <div style="margin-bottom: 25px" class="input-group">
                        <asp:TextBox ID="MemberSearchTextbox" runat="server" CssClass="form-control" placeholder="Search for member"></asp:TextBox>
                    </div>

                    <div style="margin-top: 10px" class="form-group">
                        <div class="col-sm-12 controls">
                            <asp:Button ID="MemberSearchButton" runat="server" OnClick="SearchMemberButton_Click" CssClass="btn btn-primary" Text="Submit" />
                        </div>
                    </div>
                </div>
            </asp:Panel>
            <asp:Panel runat="server" ID="AccountDetails">

                <div style="margin-bottom: 25px" class="input-group">
                    <asp:TextBox ID="ChildNameTextbox" runat="server" CssClass="form-control" placeholder="Child Name"></asp:TextBox>
                </div>

                <div style="margin-bottom: 25px" class="input-group">
                    <asp:TextBox ID="Parent1NameTextbox" runat="server" CssClass="form-control" MaxLength="101" placeholder="Parent 1 Name"></asp:TextBox>
                </div>

                <div style="margin-bottom: 25px" class="input-group">
                    <asp:TextBox ID="Parent2NameTextbox" runat="server" MaxLength="101" CssClass="form-control" placeholder="Parent 2 Name"></asp:TextBox>
                </div>

                <div style="margin-bottom: 25px" class="input-group">
                    <asp:TextBox ID="HomeAddressTextbox" runat="server" MaxLength="50" CssClass="form-control" placeholder="Home Address"></asp:TextBox>
                </div>

                <div style="margin-bottom: 25px" class="input-group">
                    <asp:TextBox ID="PostalCodeTextbox" runat="server" MaxLength="6" CssClass="form-control" placeholder="Postal Code"></asp:TextBox>
                </div>

                <div style="margin-bottom: 25px" class="input-group">
                    <asp:TextBox ID="ContactNumberTextbox" runat="server" MaxLength="10" CssClass="form-control" placeholder="Contact Number"></asp:TextBox>
                </div>

                <div class="form-group">
                    <div class="col-lg-12 control">
                        <div style="border-top: 1px solid#888; padding-top: 15px; font-size: 85%"><a href="ChangePassword.aspx"></a></div>
                    </div>
                </div>

                <div style="margin-top: 10px" class="form-group">
                    <div class="col-sm-12 controls">
                        <asp:Button ID="SubmitUpdateButton" runat="server" OnClick="SubmitUpdateButton_Click" CssClass="btn btn-primary" Text="Submit" />
                        <asp:Button ID="ToggleActive" runat="server" OnClick="ToggleActive_Click" Text="" />
                    </div>
                </div>
            </asp:Panel>
        </div>
        </div>
</asp:Content>

