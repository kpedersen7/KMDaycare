﻿using System;
using System.IO;
using System.Web;
using System.Web.UI;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_PreInit(object sender, EventArgs e)
    {
        SecurityController s = HttpContext.Current.User as SecurityController;
        if(s != null)
        {
            UserController users = new UserController();
            User u = users.GetUser(HttpContext.Current.User.Identity.Name);
            if (s.IsInRole("Admin"))
            {
                Page.MasterPageFile = "~/Admin.master";
            }
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        DirectoryInfo dir = new DirectoryInfo(Server.MapPath("~/HomeGallery/"));
        dir.Refresh();
        Image1.ImageUrl = "HomeGallery/image1.jpg";
        Image2.ImageUrl = "HomeGallery/image2.jpg";
        Image3.ImageUrl = "HomeGallery/image3.jpg";
    }
}