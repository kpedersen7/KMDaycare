using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        string UserName = txtUserName.Text;
        string strPass = txtPassword.Text;
        Cryptography objCry = new Cryptography();
        string EncyPass= objCry.Encrypt(strPass);

        KBAIST objKbaist = new KBAIST();
        bool success = objKbaist.VerifyLogin(UserName, EncyPass);
        if (success)
        {
            Response.Redirect("Default.aspx");
        }else
        {
            lblmsg.Text = "Email Address or Password is incorrect!";
        }
    }



    
}