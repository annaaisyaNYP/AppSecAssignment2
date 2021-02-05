using System;
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
    public partial class Settings : System.Web.UI.Page
    {
        string SITConnectionString = ConfigurationManager.ConnectionStrings["SITConnection"].ConnectionString;
        static string finalHash;
        static string salt;
        byte[] Key;
        byte[] IV;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["LoggedIn"] != null && Session["AuthToken"] != null && Request.Cookies["AuthToken"] != null)
            {
                if (!Session["AuthToken"].ToString().Equals(Request.Cookies["AuthToken"].Value))
                {
                    Response.Redirect("Login.aspx", false);
                }
            }

            if (Session["LoggedIn"] == null)
            {
                Response.Redirect("~/CustomError/403Error.html");
            }

            lbMsg.Text = "";
            lbSuccessMsg.Text = "";
        }
        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            Session.RemoveAll();

            if (Request.Cookies["ASP.NET_SessionId"] != null)
            {
                Response.Cookies["ASP.NET_SessionId"].Value = string.Empty;
                Response.Cookies["ASP.NET_SessionId"].Expires = DateTime.Now.AddMonths(-20);
            }

            if (Request.Cookies["AuthToken"] != null)
            {
                Response.Cookies["AuthToken"].Value = string.Empty;
                Response.Cookies["AuthToken"].Expires = DateTime.Now.AddMonths(-20);
            }

            Response.Redirect("Login.aspx", false);
        }

        protected void btnChaPass_Click(object sender, EventArgs e)
        {
            bool checkField = FieldChecker();

            if (checkField)
            {
                string pwd = tbNewPass.Text.ToString().Trim(); ;

                RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
                byte[] saltByte = new byte[8];

                rng.GetBytes(saltByte);
                salt = Convert.ToBase64String(saltByte);

                SHA512Managed hashing = new SHA512Managed();

                string pwdWithSalt = pwd + salt;

                byte[] plainHash = hashing.ComputeHash(Encoding.UTF8.GetBytes(pwd));
                byte[] hashWithSalt = hashing.ComputeHash(Encoding.UTF8.GetBytes(pwdWithSalt));
                finalHash = Convert.ToBase64String(hashWithSalt);

                RijndaelManaged cipher = new RijndaelManaged();
                cipher.GenerateKey();
                Key = cipher.Key;
                IV = cipher.IV;
                UpdatePassword();
                lbSuccessMsg.Text += "Password changed sucessfully.";
            }
        }

        public bool FieldChecker()
        {
            string email = Session["LoggedIn"].ToString();
            if (!IsPasswordCorrect(email, tbCurrPass.Text))
            {
                lbMsg.Text += "Password is incorrect! </br>";
            }

            if (!IsPasswordValid(tbNewPass.Text))
            {
                lbMsg.Text += "Password is invalid. Please make sure you follow the requirements. </br>";
            }

            if (IsPassTooNew(email))
            {
                lbMsg.Text += "Password was changed recently. Please change password at another time. </br>";
            }

            if (tbCurrPass.Text == tbNewPass.Text)
            {
                lbMsg.Text += "Passwords are the same. Please choose a different password. </br>";
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
                }
                else
                {
                    return false;
                }
            }

            else
            {
                return false;
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

        public bool IsPasswordValid(string pwd)
        {
            if (string.IsNullOrWhiteSpace(pwd))
                return false;

            try
            {
                return Regex.IsMatch(pwd, @"^.*(?=.{8,})(?=.+\d)(?=.+[a-z])(?=.+[A-Z])(?=.+[!*@#$%^&+=]).*",
                    RegexOptions.None, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }

        public bool IsPassTooNew(string email)
        {
            DateTime passAge;
            SqlConnection con = new SqlConnection(SITConnectionString);
            string sqlStmt = "SELECT passwordage from Account where email = @Email";
            SqlCommand cmd = new SqlCommand(sqlStmt, con);
            cmd.Parameters.AddWithValue("@Email", email);

            try
            {
                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (reader["PasswordAge"] != DBNull.Value)
                        {
                            passAge = Convert.ToDateTime(reader["PasswordAge"]);

                            DateTime now = DateTime.Now;
                            TimeSpan interval = now - passAge;
                            if (interval.TotalMinutes < 5)
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                        else { return false; }
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

            return false;
        }

        public void UpdatePassword()
        {
            try
            {
                SqlConnection con = new SqlConnection(SITConnectionString);
                string sqlStmt = "UPDATE Account SET passwordHash = @PassHash, passwordSalt = @PassSalt, " +
                    "[key] = @Key, IV = @IV, passwordage = @PassAge " +
                    "WHERE email = @Email";
                SqlCommand cmd = new SqlCommand(sqlStmt, con);

                cmd.Parameters.AddWithValue("@Email",Session["LoggedIn"].ToString());
                cmd.Parameters.AddWithValue("@PassHash", finalHash);
                cmd.Parameters.AddWithValue("@PassSalt",  salt);
                cmd.Parameters.AddWithValue("@Key",Convert.ToBase64String(Key));
                cmd.Parameters.AddWithValue("@IV",Convert.ToBase64String(IV));
                cmd.Parameters.AddWithValue("@PassAge",DateTime.Now);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
    }
}