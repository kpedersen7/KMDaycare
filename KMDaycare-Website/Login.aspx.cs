using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        string username = txtUserName.Text;
        string password = txtPassword.Text;
        Cryptography c = new Cryptography();
        string hashedPassword = c.Encrypt(password);

        KBAIST kb = new KBAIST();
        bool success = kb.VerifyLogin(username, hashedPassword);
        if (success)
        {
            User thisUser = kb.GetUser(username);
            string userRole = kb.GetUserRole(thisUser.Role);
            FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(1, txtUserName.Text, DateTime.Now, DateTime.Now.AddMinutes(30), false, userRole);
            string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
            HttpCookie authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
            Response.Cookies.Add(authCookie);
            Response.Redirect(FormsAuthentication.GetRedirectUrl(txtUserName.Text, false));
        }
        else
        {
            lblmsg.Text = "Login failed, check username or password.";
        }
    }



    
}