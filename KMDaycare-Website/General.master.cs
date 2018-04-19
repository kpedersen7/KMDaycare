using System;
using System.Configuration;
using System.IO;
using System.Web;
using System.Web.Configuration;
using System.Web.Security;

public partial class General : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
      
        SecurityController s = HttpContext.Current.User as SecurityController;
        if (s != null)
        {
            UserController users = new UserController();
            User u = users.GetUser(HttpContext.Current.User.Identity.Name);
            LoggedInUser.Text = "Logged in as " + HttpContext.Current.User.Identity.Name;
            Configuration webConfigApp = WebConfigurationManager.OpenWebConfiguration("~");
            string url = webConfigApp.AppSettings.Settings["albumURL"].Value;
            if (s.IsInRole("Parent") || s.IsInRole("Admin"))
            {
                
                PhotoAlbumLink.Attributes.Add("href", url);
                form1.Controls.Remove(LoginLink);
            }
            else if (!s.IsInRole("Admin") && !s.IsInRole("Parent"))
            {
                PhotoAlbumLink.Attributes.Remove("href");
                PhotoAlbumLink.Attributes.Add("href", url);
                //form1.Controls.Remove(PhotoAlbumLink);
                form1.Controls.Remove(LogoutLink);
            }
        }
        else
        {
            //form1.Controls.Remove(PhotoAlbumLink);
            form1.Controls.Remove(LogoutLink);
        }
    }
    public void Signout_Click(object sender, EventArgs e)
    {
        FormsAuthentication.SignOut();
        Response.Redirect("Login.aspx");
    }
}
