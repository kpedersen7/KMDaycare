<%@ Page Title="" Language="C#" MasterPageFile="~/General.master" AutoEventWireup="true" Theme="Theme1" CodeFile="Enrollment.aspx.cs" Inherits="Volunteer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="margin:5%;">
        <p class="SomeClass">
            Not a Member Yet?
        </p>
        <p class="SomeClass">
            <a href="SendQuery.aspx">Sign Up Here</a>
        </p>
        <p class="SomeClass">
            <a href="Files/RegistrationForm.docx" target="_blank">Download the Registration Form</a>
        </p>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>

