<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.master" Theme="Theme1" AutoEventWireup="true" CodeFile="Settings.aspx.cs" Inherits="Settings" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h1>Website Settings</h1>
    <asp:Label ID="feedbackLabel" runat="server" />

    <div id="accordion">

        <div class="card">
            <div class="card-header">
                <h2><a class="card-link" data-toggle="collapse" href="#collapseOne">Change Newsletter
                </a></h2>
            </div>
            <div id="collapseOne" class="collapse show" data-parent="#accordion">
                <div class="card-body">
                    <a href="ViewNewsletter.aspx" target="_blank">Current Newsletter</a>
                    <asp:FileUpload runat="server" ID="fileupload" />
                    <asp:Button ID="Button1" runat="server" OnClick="Newsletter_Save" Text="Save" Style="width: 85px" />
                </div>
            </div>
        </div>

        <div class="card">
            <div class="card-header">
                <h2><a class="collapsed card-link" data-toggle="collapse" href="#collapseTwo">Manage Homepage Gallery
                </a></h2>
            </div>
            <div id="collapseTwo" class="collapse" data-parent="#accordion">
                <div class="card-body">
                    <asp:Table runat="server">
                        <asp:TableRow>
                            <asp:TableCell>
                                <asp:Label runat="server">Image 1:</asp:Label><asp:FileUpload runat="server" ID="fileupload1" />
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:Image runat="server" ID="Image1" Height="100" Width="100" />
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell>
                                <asp:Label runat="server">Image 2:</asp:Label><asp:FileUpload runat="server" ID="fileupload2" />
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:Image runat="server" ID="Image2" Height="100" Width="100" />
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell>
                                <asp:Label runat="server">Image 3:</asp:Label><asp:FileUpload runat="server" ID="fileupload3" />
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:Image runat="server" ID="Image3" Height="100" Width="100" />
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                    <asp:Button ID="btnsave" runat="server" OnClick="HomeGallery_Save" Text="Save" Style="width: 85px" />
                </div>
            </div>
        </div>

        <div class="card">
            <div class="card-header">
                <h2><a class="collapsed card-link" data-toggle="collapse" href="#collapseThree">Change Email Settings
                </a></h2>
            </div>
            <div id="collapseThree" class="collapse" data-parent="#accordion">
                <div class="card-body">
                    <asp:Table runat="server">
                        <asp:TableRow>
                            <asp:TableCell>
                                    <asp:Label runat="server">Current Email: </asp:Label>
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:Label runat="server" ID="CurrentEmailLabel"></asp:Label>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell>
                                    <asp:Label runat="server">New Email: </asp:Label>
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:TextBox runat="server" ID="SiteEmailAddress" TextMode="Email"></asp:TextBox>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell>
                                    <asp:Label runat="server">New Password: </asp:Label>
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:TextBox runat="server" ID="SiteEmailPassword" TextMode="Password"></asp:TextBox>
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                    <asp:Button runat="server" OnClick="EmailAddress_Save" Text="Save" />
                </div>
            </div>
        </div>

        <div class="card">
            <div class="card-header">
                <h2><a class="collapsed card-link" data-toggle="collapse" href="#collapseFour">Change Flickr Album URL
                </a></h2>
            </div>
            <div id="collapseFour" class="collapse" data-parent="#accordion">
                <div class="card-body">
                    <asp:Table runat="server">
                        <asp:TableRow>
                            <asp:TableCell>
                                <a runat="server" href="" id="CurrentAlbumURL" target="_blank">View Current Page</a>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell>
                                    <asp:Label runat="server">New URL: </asp:Label>
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:TextBox runat="server" ID="NewAlbumURL" TextMode="Url"></asp:TextBox>
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                    <asp:Button runat="server" OnClick="AlbumURL_Save" Text="Save" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>

