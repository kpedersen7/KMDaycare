using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for CheckLogin
/// </summary>
public class CheckLogin
{
    public bool VerifyLogin(string EmailId, string Password)
    {
        try
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Local"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("SP_Verifylogin", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@EmailID", EmailId);
                    cmd.Parameters.AddWithValue("@Password", Password);
                    con.Open();

                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        return true;
                    }else
                    {
                        return false;
                    }
                    con.Close();
                }
            }
        }
        catch(Exception e)
        {
            return false;
        }
    }
}