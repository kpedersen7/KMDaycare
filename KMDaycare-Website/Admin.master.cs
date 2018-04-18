using System;
using System.Configuration;
using System.Web;
using System.Web.Configuration;
using System.Web.Security;

public partial class Admin : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        SecurityController s = HttpContext.Current.User as SecurityController;
        if (s != null)
        {
            UserController users = new UserController();
            User u = users.GetUser(HttpContext.Current.User.Identity.Name);
            if (s.IsInRole("Admin"))
            {
                Configuration webConfigApp = WebConfigurationManager.OpenWebConfiguration("~");
                string url = webConfigApp.AppSettings.Settings["albumURL"].Value;
                PhotoAlbumLink.Attributes.Add("href", url);
            }
        }
    }
    public void Signout_Click(object sender, EventArgs e)
    {
        FormsAuthentication.SignOut();
        Response.Redirect("Login.aspx");
    }
}
