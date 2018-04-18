using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class General : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        DirectoryInfo dir = new DirectoryInfo(Server.MapPath("~/HomeGallery/"));
        dir.Refresh();
        Image1.ImageUrl = "HomeGallery/image1.jpg";
        Image2.ImageUrl = "HomeGallery/image2.jpg";
        Image3.ImageUrl = "HomeGallery/image3.jpg";
        SecurityController s = HttpContext.Current.User as SecurityController;
        if (s != null)
        {
            UserController users = new UserController();
            User u = users.GetUser(HttpContext.Current.User.Identity.Name);
            LoggedInUser.Text = "Logged in as " + HttpContext.Current.User.Identity.Name;
            if (s.IsInRole("Parent") || s.IsInRole("Admin"))
            {
                Configuration webConfigApp = WebConfigurationManager.OpenWebConfiguration("~");
                string url = webConfigApp.AppSettings.Settings["albumURL"].Value;
                PhotoAlbumLink.Attributes.Add("href", url);
                form1.Controls.Remove(LoginLink);
            }
            else if (!s.IsInRole("Admin") && !s.IsInRole("Parent"))
            {
                form1.Controls.Remove(PhotoAlbumLink);
                form1.Controls.Remove(LogoutLink);
            }
        }
        else
        {
            form1.Controls.Remove(PhotoAlbumLink);
            form1.Controls.Remove(LogoutLink);
        }
    }
    public void Signout_Click(object sender, EventArgs e)
    {
        FormsAuthentication.SignOut();
        Response.Redirect("Login.aspx");
    }
}
