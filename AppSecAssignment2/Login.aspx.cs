﻿using System;
using System.Text;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Globalization;
using System.Data.SqlClient;
using System.Configuration;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace AppSecAssignment2
{
    public partial class Login : Page
    {
        string SITConnectionString = ConfigurationManager.ConnectionStrings["SITConnection"].ConnectionString;
        int LoginAttemptCount;
        protected void Page_Load(object sender, EventArgs e)
        {
            lbMsg.Text = "";
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            LoginAttemptCount++;
            bool validLogin = ValidateLogin();

            if (validLogin)
            {
                Session["LoggedIn"] = tbEmail.Text.Trim();

                string guid = Guid.NewGuid().ToString();
                Session["AuthToken"] = guid;

                Response.Cookies.Add(new HttpCookie("AuthToken", guid));

                Response.Redirect("Homepage.aspx", false);
            }
        }

        public bool ValidateLogin()
        {
            if (!IsEmailValid(tbEmail.Text))
            {
                lbMsg.Text += "Check email formatting. </br>";
            }
            if (!DoesEmailExists(tbEmail.Text))
            {
                lbMsg.Text += "Email does not exist in the system. <a href=\"~/Register.aspx\">Register Now!</a> </br>";
            }

            if (!IsPasswordCorrect(tbEmail.Text, tbPass.Text))
            {
                lbMsg.Text += "Password is incorrect! </br>";
            }

            if (LoginAttemptCount > 3)
            {
                lbMsg.Text += "Too many Login attempts. Try again later. </br>";
            }

            //All Clear
            if (lbMsg.Text == "")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // Taken from https://docs.microsoft.com/en-us/dotnet/standard/base-types/how-to-verify-that-strings-are-in-valid-email-format
        public static bool IsEmailValid(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                // Normalize the domain
                email = Regex.Replace(email, @"(@)(.+)$", DomainMapper,
                                      RegexOptions.None, TimeSpan.FromMilliseconds(200));

                // Examines the domain part of the email and normalizes it.
                string DomainMapper(Match match)
                {
                    // Use IdnMapping class to convert Unicode domain names.
                    var idn = new IdnMapping();

                    // Pull out and process domain name (throws ArgumentException on invalid)
                    string domainName = idn.GetAscii(match.Groups[2].Value);

                    return match.Groups[1].Value + domainName;
                }
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
            catch (ArgumentException)
            {
                return false;
            }

            try
            {
                return Regex.IsMatch(email,
                    @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }

        public bool DoesEmailExists(string email)
        {
            string e = null;
            SqlConnection con = new SqlConnection(SITConnectionString);
            string sqlstmt = "Select Email from Account where email = @Email";
            SqlCommand cmd = new SqlCommand(sqlstmt, con);
            cmd.Parameters.AddWithValue("@Email", email);
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    if (reader["Email"] != null)
                    {
                        if (reader["Email"] != DBNull.Value)
                        {
                            e = reader["Email"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                con.Close();
            }
            if (e != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsPasswordCorrect(string email, string pass)
        {
            SHA512Managed hashing = new SHA512Managed();
            string dbHash = getDBHash(email);
            string dbSalt = getDBSalt(email);
            if (dbSalt != null && dbSalt.Length > 0 && dbHash != null && dbHash.Length > 0)
            {
                string pwdWithSalt = pass + dbSalt;
                byte[] hashWithSalt = hashing.ComputeHash(Encoding.UTF8.GetBytes(pwdWithSalt));
                string userHash = Convert.ToBase64String(hashWithSalt);
                if (userHash.Equals(dbHash))
                {
                    return true;
                    //Session["email"] = email;
                    //Response.Redirect("Success.aspx", false);
                }
                else
                {
                    return false;
                }
            }

            else
            {
                return false;
                //lbMsg.Text += "Userid or password is not valid. Please try again.";
                //Response.Redirect("Login.aspx", false);
            }
        }

        protected string getDBHash(string email)
        {
            string h = null;
            SqlConnection con = new SqlConnection(SITConnectionString);
            string sqlstmt = "Select PasswordHash from Account where email = @Email";
            SqlCommand cmd = new SqlCommand(sqlstmt, con);
            cmd.Parameters.AddWithValue("@Email", email);
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    if (reader["PasswordHash"] != null)
                    {
                        if (reader["PasswordHash"] != DBNull.Value)
                        {
                            h = reader["PasswordHash"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                con.Close();
            }
            return h;
        }

        protected string getDBSalt(string email)
        {
            string s = null;
            SqlConnection con = new SqlConnection(SITConnectionString);
            string sqlstmt = "Select PasswordSalt from Account where email = @Email";
            SqlCommand cmd = new SqlCommand(sqlstmt, con);
            cmd.Parameters.AddWithValue("@Email", email);
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    if (reader["PasswordSalt"] != null)
                    {
                        if (reader["PasswordSalt"] != DBNull.Value)
                        {
                            s = reader["PasswordSalt"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                con.Close();
            }
            return s;
        }
    }
}