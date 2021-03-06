﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.master" Theme="Theme1" AutoEventWireup="true" CodeFile="CreateAccount.aspx.cs" Inherits="CreateAccount" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Label runat="server" ID="feedback"></asp:Label>

    <div class="form-horizontal">
        <div class="form-group col-lg-12">
            <h1>Create Account</h1>
        </div>

        <div class="form-group col-lg-12">
            <div class="col-lg-6">
                <div class="col-lg-3">
                    <span>Child First Name :</span>
                </div>

                <div class="col-lg-9">
                    <asp:TextBox runat="server" ID="ChildFirstName" placeholder="Child First Name" CssClass="form-control"></asp:TextBox>
                </div>
            </div>

            <div class="col-lg-6">
                <div class="col-lg-3">
                    <span>Child Last Name :</span>
                </div>
                <div class="col-lg-9">
                    <asp:TextBox runat="server" ID="ChildLastName" placeholder="Child Last Name" CssClass="form-control"></asp:TextBox>
                </div>
            </div>
        </div>

        <div class="col-lg-12 form-group">
            <div class="col-lg-6">
                <div class="col-lg-3">
                    <span>Parent 1 First Name :</span>
                </div>
                <div class="col-lg-9">
                    <asp:TextBox runat="server" ID="Parent1FirstName" placeholder="Parent 1 Last Name" CssClass="form-control"></asp:TextBox>
                </div>
            </div>

            <div class="col-lg-6">
                <div class="col-lg-3">
                    <span>Parent 1 Last Name :</span>
                </div>
                <div class="col-lg-9">
                    <asp:TextBox runat="server" ID="Parent1LastName" placeholder="Parent 1 Last Name" CssClass="form-control"></asp:TextBox>
                </div>
            </div>
        </div>

        <div class="col-lg-12 form-group">
            <div class="col-lg-6">
                <div class="col-lg-3">
                    <span>Parent 2 First Name :</span>
                </div>
                <div class="col-lg-9">
                    <asp:TextBox runat="server" ID="Parent2FirstName" placeholder="Parent 2 First Name" CssClass="form-control"></asp:TextBox>
                </div>
            </div>

            <div class="col-lg-6">
                <div class="col-lg-3">
                    <span>Parent 2 Last Name :</span>
                </div>
                <div class="col-lg-9">
                    <asp:TextBox runat="server" ID="Parent2LastName" placeholder="Parent 2 Last Name" CssClass="form-control"></asp:TextBox>
                </div>
            </div>
        </div>

        <div class="col-lg-12 form-group">
            <div class="col-lg-6">
                <div class="col-lg-3">
                    <span>Home Address :</span>
                </div>
                <div class="col-lg-9">
                    <asp:TextBox runat="server" ID="HomeAddress" placeholder="Home Address" CssClass="form-control"></asp:TextBox>
                </div>
            </div>

            <div class="col-lg-6">
                <div class="col-lg-3">
                    <span>Postal Code :</span>
                </div>
                <div class="col-lg-9">
                    <asp:TextBox runat="server" ID="PostalCode" placeholder="Postal Code" CssClass="form-control"></asp:TextBox>
                </div>
            </div>
        </div>

        <div class="col-lg-12 form-group">
            <div class="col-lg-6">
                <div class="col-lg-3">
                    <span>Contact Number :</span>
                </div>
                <div class="col-lg-9">
                    <asp:TextBox runat="server" ID="ContactNumber" placeholder="Contact Number" CssClass="form-control"></asp:TextBox>
                </div>
            </div>

            <div class="col-lg-6">
                <div class="col-lg-3">
                    <span>Email :</span>
                </div>
                <div class="col-lg-9">
                    <asp:TextBox runat="server" ID="Email" placeholder="Email" CssClass="form-control"></asp:TextBox>
                </div>
            </div>
        </div>

        <div class="col-lg-12 form-group">
            <div class="col-lg-6">
                <div class="col-lg-3">
                    <span>Password :</span>
                </div>
                <div class="col-lg-9">
                    <asp:TextBox runat="server" ID="Password" placeholder="Password" CssClass="form-control"></asp:TextBox>
                </div>
            </div>

            <div class="col-lg-6">
                <div class="col-lg-3">
                    <span>Role :</span>
                </div>
                <div class="col-lg-9">
                    <asp:DropDownList runat="server" ID="RoleDD" Height="42" CssClass="form-control dropdown">
                        <asp:ListItem Value="1">Admin</asp:ListItem>
                        <asp:ListItem Value="2">Parent</asp:ListItem>
                        <asp:ListItem Value="3">Staff</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
        </div>

        <div class="col-lg-12 form-group">
            <div class="col-lg-6 col-lg-offset-6">
                <div class="col-lg-12 text-right">
                    <asp:Button runat="server" ID="SubmitButton" OnClick="SubmitButton_Click" Text="Submit" CssClass="btn btn-primary" />
                </div>
            </div>
        </div>
    </div>

</asp:Content>

