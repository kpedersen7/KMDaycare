﻿<%@ Page Title="" Language="C#" MasterPageFile="~/General.master" AutoEventWireup="true" CodeFile="MyAccount.aspx.cs" Inherits="MyAccount" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Label runat="server" ID="ChildName"></asp:Label>
    <asp:Label runat="server" ID="Parent1Name"></asp:Label>
    <asp:Label runat="server" ID="Parent2Name"></asp:Label>
    <asp:Label runat="server" ID="HomeAddress"></asp:Label>
    <asp:Label runat="server" ID="PostalCode"></asp:Label>
    <asp:Label runat="server" ID="ContactNumber"></asp:Label>
</asp:Content>
