<%@ Page Title="" Language="C#" MasterPageFile="~/General.master" AutoEventWireup="true" Theme="Theme1" CodeFile="Enrollment.aspx.cs" Inherits="Volunteer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container">

        <div class="container-fluid">
            <div class="row">
                <div class="col-lg-12">
                    <br />
                    <%-- <img src="Images/policies.jpg" class="pull-left" alt="Responsive image" style="height: 300px;">--%>
                    <p class="SomeClass">
                        &nbsp;  Not a Member Yet?????
                    </p>
                    <p class="SomeClass">
                        &nbsp;  <a href="SendQuery.aspx">Sign Up Here
                        </a>
                    </p>
                    <p class="SomeClass">
                        &nbsp;  <a href="Files/RegistrationForm.docx" target="_blank">Download the Registration Form</a>

                    </p>

                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>

