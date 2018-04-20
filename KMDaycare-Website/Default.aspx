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
        <div class="SomeClass2">Welcome To Knottwood Montessori Daycare </div>
    </div>

    <div class="col-lg-12 text-justify">
        <div class="SomeClass">
            Knottwood Montessori Daycare & OSC has an open door policy and learning through play is the base of our program. Learning through play provides the children opportunities to grow and enhance their creative, intellectual, social, physical and emotional development. We strive to foster a positive self-image and respect for all the children and adults.
        Building positive relationships is the core of our practice. Families play the most important role in children’s lives. We believe keeping open communication with families is important for the optimal care of the children.
        </div>
    </div>

    <div class="col-lg-12">
         <div class="SomeClass">
        <ul>
            <li>Open 6.30AM-6PM Monday to Friday</li>
            <li>Experienced staff.</li>
            <li>Easy Access.</li>
            <li>Affordable fees.</li>
            <li>The maximum capacity of children in our daycare is 70.</li>
            <%-- <li>Enhanced ratios</li>--%>
        </ul>
    </div>
</div>
    <div class="col-lg-12">
        <div class="panel panel-default">
            <div class="panel-heading">Our Events</div>
            <div class="panel-body">
                <div class="col-lg-12">
                    <div class="col-lg-4">
                        <div class="col-lg-12">
                            <img src="HomeGallery/event1.jpg" class="img-thumbnail" />
                        </div>
                        <div class="col-lg-12">
                            <h4>Summer fun</h4>
                        </div>
                        <div class="col-lg-12">
                            We have exciting activities for the Summer, We take the Kids  for outing and play games and much more....
                        </div>
                    </div>
                    <div class="col-lg-4">
                        <div class="col-lg-12">
                            <img src="HomeGallery/event2.jpg" class="img-thumbnail" />
                        </div>
                        <div class="col-lg-12">
                            <h4>Halloween Party</h4>
                        </div>
                        <div class="col-lg-12">
                            Our lovely kids dresses up for the halloween party and we had greate fun togeather.
                        </div>
                    </div>
                    <div class="col-lg-4">
                        <div class="col-lg-12">
                            <img src="HomeGallery/event3.jpg" class="img-thumbnail" />
                        </div>
                        <div class="col-lg-12">
                            <h4>Christmas Celebration</h4>
                        </div>
                        <div class="col-lg-12">
                            Our children really enjoyed their Christams party, they shared gifts with thier friends and teachers. we had greate time togeather.
                        </div>
                    </div>


                </div>
            </div>
        </div>
    </div>

</asp:Content>

