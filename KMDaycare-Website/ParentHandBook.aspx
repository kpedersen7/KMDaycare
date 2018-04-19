<%@ Page Title="" Language="C#" MasterPageFile="~/General.master" AutoEventWireup="true" Theme="Theme1" CodeFile="ParentHandBook.aspx.cs" Inherits="ParentHandBook" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="margin: 5%;">
        <p class="SomeClass">
            Not a Member Yet??
        </p>
        <p class="SomeClass">
            <a href="SendQuery.aspx">Sign Up Here</a>
        </p>
        <p class="SomeClass">
            <a href="Files/ParentHandbook-Final-Aug2017.docx" target="_blank">Download the Parent Handbook</a>
        </p>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>

