﻿using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

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
                        cmd.Parameters.AddWithValue("@username", userForAdd.UserName);
                        cmd.Parameters.AddWithValue("@password", userForAdd.Password);
                        cmd.Parameters.AddWithValue("@role", userForAdd.Role);
                        con.Open();

                        SqlDataReader sqldr = cmd.ExecuteReader();
                        con.Close();
                        return true;
                    }
                    catch (Exception e)
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

    public User GetUser(string username)
    {
        User u = new User();
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KMDaycare"].ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand("GetUser", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserName", username);
                con.Open();

                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    u.Email = dr["Email"].ToString();
                    u.UserName = dr["UserName"].ToString();
                    u.Role = int.Parse(dr["Role"].ToString());
                    u.Active = int.Parse(dr["Active"].ToString());
                }
                con.Close();
            }
        }
        return u;
    }

    public User GetUserByEmail(string email)
    {
        User u = new User();
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KMDaycare"].ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand("GetUserbyEmail", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Email", email);
                con.Open();

                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    u.Email = dr["Email"].ToString();
                    u.UserName = dr["UserName"].ToString();
                    u.Role = int.Parse(dr["Role"].ToString());
                }
                con.Close();
            }
        }
        return u;
    }

    public string GetUserRole(int roleID)
    {
        string roleName = "";
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KMDaycare"].ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand("GetUserRole", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@RoleID", roleID);
                con.Open();

                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    roleName = dr["RoleName"].ToString();
                }
                con.Close();
            }
        }
        return roleName;
    }

    public bool VerifyLogin(string UserName, string Password)
    {
        try
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KMDaycare"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("Verifylogin", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserName", UserName);
                    cmd.Parameters.AddWithValue("@Password", Password);
                    con.Open();

                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        con.Close();
                        return true;
                    }
                    else
                    {
                        con.Close();
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

    public bool ToggleUserActiveStatus(string u, int active)
    {
        try
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KMDaycare"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("ToggleUserActiveStatus", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@userName", u);
                    cmd.Parameters.AddWithValue("@active", active);
                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.RecordsAffected > 0)
                    {
                        con.Close();
                        return true;
                    }
                    else
                    {
                        con.Close();
                        return false;
                    }
                }
            }
        }
        catch(Exception ex)
        {
            return false;
        }
    }
}