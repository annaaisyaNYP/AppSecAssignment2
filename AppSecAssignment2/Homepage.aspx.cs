using System;
using System.Data.SqlClient;
using System.Configuration;

namespace AppSecAssignment2
{
    public partial class Homepage : System.Web.UI.Page
    {
        string SITConnectionString = ConfigurationManager.ConnectionStrings["SITConnection"].ConnectionString;
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

            string email = Session["LoggedIn"].ToString();
            if (IsPassOld(email))
            {
                PanelPassExpired.Visible = true;
            }
        }

        public bool IsPassOld(string email)
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
                            if (interval.TotalMinutes > 15)
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
    }
}