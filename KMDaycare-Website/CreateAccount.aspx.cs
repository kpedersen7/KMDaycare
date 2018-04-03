using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CreateAccount : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }


    protected void SubmitButton_Click(object sender, EventArgs e)
    {
        KBAIST kb = new KBAIST();
        string userName = MakeUsername(ChildFirstName.Text, ChildLastName.Text, Parent1FirstName.Text);
        bool success1 = kb.CreateAccount(ChildFirstName.Text, ChildLastName.Text, Parent1FirstName.Text, Parent1LastName.Text, Parent2FirstName.Text, Parent2LastName.Text, HomeAddress.Text, PostalCode.Text, EmergencyContactNumber.Text);

        Cryptography c = new Cryptography();
        string encryptedPassword = c.Encrypt(Password.Text);
        bool success2 = kb.CreateUser(Email.Text, userName ,encryptedPassword, int.Parse(RoleDD.SelectedValue));
        if (success1 && success2)
        {
            Response.Write("worked");
        }
        else
        {
            Response.Write("did not work bro");
        }


        //System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage();
        //SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
        //smtp.Credentials = new NetworkCredential("pedeyk@gmail.com", "");
        //smtp.EnableSsl = true;
        //smtp.Send("pedeyk@gmail.com", Email.Text, "Account Registered at Knottwood Montessori Daycare", "Hello! An account as been made for you at Knottwood Montessori! USERNAME AND PASSWORD");
    }

    private string MakeUsername(string childfname, string childlname, string parentfname)
    {
        string username = childfname.Substring(0, 1).ToLower() + childlname + parentfname;
        return username;
    }
}