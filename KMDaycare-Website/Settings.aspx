<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.master" Theme="Theme1" AutoEventWireup="true" CodeFile="Settings.aspx.cs" Inherits="Settings" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="https://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/4.0.0-beta.2/css/bootstrap.css" rel="stylesheet" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/2.1.1/jquery.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/4.0.0-beta/js/bootstrap.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
        <h1>Website Settings</h1>
        <asp:Label ID="feedbackLabel" runat="server" />
        <div id="accordion">
            <div class="card">
                <div class="card-header" id="headingTwo">
                    <h5 class="mb-0">
                        <button class="btn btn-link collapsed" data-toggle="collapse" data-target="#collapseTwo" aria-expanded="false" aria-controls="collapseTwo">
                            Change Newsletter
                        </button>
                    </h5>
                </div>
                <div id="collapseTwo" class="collapse" aria-labelledby="headingTwo" data-parent="#accordion">
                    <div class="card-body">
                        <a href="ViewNewsletter.aspx">Current Newsletter</a>
                        <asp:FileUpload runat="server" ID="fileupload" />
                        <asp:Button ID="Button1" runat="server" OnClick="Newsletter_Save" Text="Save" Style="width: 85px" />
                    </div>
                </div>
            </div>
            <div class="card">
                <div class="card-header" id="headingOne">
                    <h5 class="mb-0">
                        <button class="btn btn-link" data-toggle="collapse" data-target="#collapseOne" aria-expanded="false" aria-controls="collapseOne">
                            Manage Homepage Gallery
                        </button>
                    </h5>
                </div>
                <div id="collapseOne" class="collapse" aria-labelledby="headingOne" data-parent="#accordion">
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
                <div class="card-header" id="headingThree">
                    <h5 class="mb-0">
                        <button class="btn btn-link collapsed" data-toggle="collapse" data-target="#collapseThree" aria-expanded="false" aria-controls="collapseThree">
                            Change Email Settings
                        </button>
                    </h5>
                </div>
                <div id="collapseThree" class="collapse" aria-labelledby="headingThree" data-parent="#accordion">
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
                <div class="card-header" id="headingFour">
                    <h5 class="mb-0">
                        <button class="btn btn-link collapsed" data-toggle="collapse" data-target="#collapseFour" aria-expanded="false" aria-controls="collapseFour">
                            Change Flickr Album URL
                        </button>
                    </h5>
                </div>
                <div id="collapseFour" class="collapse" aria-labelledby="headingThree" data-parent="#accordion">
                    <div class="card-body">
                        <asp:Table runat="server">
                            <asp:TableRow>
                                <asp:TableCell>
                                    <a runat="server" href="" id="CurrentAlbumURL">View Current Page</a>
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

