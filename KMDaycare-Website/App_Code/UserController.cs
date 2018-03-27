using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for UserController
/// </summary>
public class UserController
{
    public bool CreateUser(User userForAdd)
    {
        try
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KMDaycare"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("CreateUser", con))
                {
                    try
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@email", userForAdd.Email);
                        cmd.Parameters.AddWithValue("@password", userForAdd.Password);
                        cmd.Parameters.AddWithValue("@role", userForAdd.Role);
                        con.Open();

                        SqlDataReader sqldr = cmd.ExecuteReader();
                        con.Close();
                        return true;
                    }
                    catch
                    {
                        return false;
                    }

                }
            }
        }
        catch (Exception e)
        {
            return false;
        }
    }
}