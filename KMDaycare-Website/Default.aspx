<%@ Page Title="" Language="C#" MasterPageFile="~/General.master" Theme="Theme1" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .carouselimg {
            margin: 0 auto;
        }

        #carousel {
            z-index: -1;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="col-lg-12">
        <div id="demo" class="carousel slide" data-ride="carousel">
            <!-- Indicators -->
            <ul class="carousel-indicators">
                <li data-target="#demo" data-slide-to="0" class="active"></li>
                <li data-target="#demo" data-slide-to="1"></li>
                <li data-target="#demo" data-slide-to="2"></li>
            </ul>
            <!-- The slideshow -->
            <div class="carousel-inner">
                <div class="carousel-item active">
                    <asp:Image runat="server" ID="Image1" CssClass="d-block w-100 carouselimg" Height="420" />
                </div>
                <div class="carousel-item">
                    <asp:Image runat="server" ID="Image2" CssClass="d-block w-100 carouselimg" Height="420" />
                </div>
                <div class="carousel-item">
                    <asp:Image runat="server" ID="Image3" CssClass=" d-block w-100 carouselimg" Height="420" />
                </div>
            </div>
            <!-- Left and right controls -->
            <a class="carousel-control-prev" href="#demo" data-slide="prev">
                <span class="carousel-control-prev-icon"></span>
            </a>
            <a class="carousel-control-next" href="#demo" data-slide="next">
                <span class="carousel-control-next-icon"></span>
            </a>
        </div>
    </div>
    <div class="col-lg-12">
        <h3 style="color: mediumblue">Welcome And Thanks for visiting us!</h3>
    </div>

    <div class="col-lg-12 text-justify">
        knotwood montessori Daycare development Center is a fully licensed and Accredited Childcare 
        facility approved by Alberta Health Authority. Our program provides a quality child care service 
        for children ages 13months to 6 year old . We believe that each child is unique and will grow at his or her own pace. 
        Therefore, our program is flexible and adaptable to meet the needs of each child. We strive to provide a safe, fun, nurtured and 
        learning environment that foster growth in all their developmental domains. Features:
    </div>

    <div class="col-lg-12">
        <ul>
            <li>experienced staff.</li>
            <li>Subsidy welcome.</li>
            <li>Affordable fees.</li>
            <li>Open 7:30am to 6:00pm Monday to Friday.</li>
            <li>The maximum capacity of children in our care is 70.</li>
            <%-- <li>Enhanced ratios</li>--%>
        </ul>
    </div>

    <div class="col-lg-12">
        <div class="panel panel-default">
            <div class="panel-heading">Recent Blog Post</div>
            <div class="panel-body">
                <div class="col-lg-12">
                    <div class="col-lg-4">
                        <div class="col-lg-12">
                            <img src="HomeGallery/KMDayCareThumbnail3.jpg" class="img-thumbnail" />
                        </div>
                        <div class="col-lg-12">
                            <h4>Exploring Nature</h4>
                        </div>
                        <div class="col-lg-12">
                            Children visited Prairie garden to explore Flora and Fauna
                        </div>
                    </div>
                    <div class="col-lg-4">
                        <div class="col-lg-12">
                            <img src="HomeGallery/KMDayCareThumbnail2.jpg" class="img-thumbnail" />
                        </div>
                        <div class="col-lg-12">
                            <h4>Summer fun</h4>
                        </div>
                        <div class="col-lg-12">
                            We had great summer with lots of field trips one of our favorite is Visit to Elk Island Park. Thanks to all the parent volunteer
                        </div>
                    </div>
                    <div class="col-lg-4">
                        <div class="col-lg-12">
                            <img src="HomeGallery/KMDayCareThumbnail5.jpg" class="img-thumbnail" />
                        </div>
                        <div class="col-lg-12">
                            <h4>April/May</h4>
                        </div>
                        <div class="col-lg-12">
                            Our children really enjoyed their visit to Salisbury green house. some important dates for April and May Yoga days: April -8th   and   29th May- 6th and 20th  Rainbow music: will be visiting on the following dates -April 21st, May […]
                        </div>
                    </div>


                </div>
            </div>
        </div>
    </div>
</asp:Content>

