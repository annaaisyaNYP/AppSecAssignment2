using System;
using System.Text;
using System.Data;
using System.Web.UI;
using System.Globalization;
using System.Data.SqlClient;
using System.Configuration;
using System.Security.Cryptography;
using System.Text.RegularExpressions;


namespace AppSecAssignment2
{
    public partial class Registration : Page
    {
        string SITConnectionString = ConfigurationManager.ConnectionStrings["SITConnection"].ConnectionString;
        static string finalHash;
        static string salt;
        byte[] Key;
        byte[] IV;

        protected void Page_Load(object sender, EventArgs e)
        {
            lbMsg.Text = "";
        }

        public bool FieldChecker()
        {
            if (tbFName.Text == "" || tbLName.Text == "" || tbCCNo.Text == "" || tbCVV.Text == "" || tbEmail.Text == "" || 
                tbPass.Text == "")
            {
                lbMsg.Text += "All field(s) required! </br>";
            }

            if (!ValidateDOB(tbBirthDate.Text))
            {
                lbMsg.Text += "Birthdate invalid. </br>";
            }

            if (tbCCNo.Text.Length < 16)
            {
                lbMsg.Text += "Credit Card No. is too short.  </br>";
            }

            if (tbCCNo.Text.Length > 16)
            {
                lbMsg.Text += "Credit Card No. is too long  </br>";
            }

            if (tbCVV.Text.Length < 3)
            {
                lbMsg.Text += "CVV is too short  </br>";
            }

            if (tbCVV.Text.Length > 3)
            {
                lbMsg.Text += "CVV is too long.  </br>";
            }

            if (!ValidateMonth(tbCCExMth.Text))
            {
                lbMsg.Text += "Expiry Month invalid. </br>";
            }

            if (!IsEmailValid(tbEmail.Text))
            {
                lbMsg.Text += "Email is invalid! Check formatting. </br>";
            }

            if (DoesEmailExists(tbEmail.Text))
            {
                lbMsg.Text += "Email exists in the system. Please choose another email. </br>";
            }

            if (!IsPasswordValid(tbPass.Text))
            {
                lbMsg.Text += "Password is invalid. Please make sure you follow the requirements. </br>";
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

        public bool ValidateDOB(string dob)
        {
            bool result;
            result = DateTime.TryParse(dob, out _);
            if (!result)
            {
                return false;
            }

            // Check if date is invalid AKA from the future
            DateTime DOB = Convert.ToDateTime(dob);
            DateTime now = DateTime.Today;
            if (now <= DOB)
            {
                return false;
            }

            else
            {
                return true;
            }
        }

        public bool ValidateMonth(string mth)
        {
            bool result;
            result = DateTime.TryParse(mth, out _);
            if (!result)
            {
                return false;
            }

            // Check if CC is expired
            DateTime MTH = Convert.ToDateTime(mth);
            DateTime now = DateTime.Today;
            if (now >= MTH)
            {
                return false;
            }

            else
            {
                return true;
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

        // Created with reference from the above method
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

        public void CreateAccount()
        {
            try
            {
                SqlConnection con = new SqlConnection(SITConnectionString);
                string sqlStmt = "INSERT INTO Account(email, fname, lname, birthdate, " +
                    "ccno, ccexmth, cvv, passwordhash, passwordsalt, [key], IV) " +
                    "VALUES(@Email, @FName, @LName, @BirthDate," +
                    "@CCNo, @CCExMth, @CVV, @PassHash, @PassSalt, @Key, @IV)";
                SqlCommand cmd = new SqlCommand(sqlStmt, con);

                cmd.Parameters.AddWithValue("@Email", tbEmail.Text);
                cmd.Parameters.AddWithValue("@FName", tbFName.Text.Trim());
                cmd.Parameters.AddWithValue("@LName", tbLName.Text.Trim());
                cmd.Parameters.AddWithValue("@BirthDate", Convert.ToDateTime(tbBirthDate.Text));
                cmd.Parameters.AddWithValue("@CCNo", Convert.ToBase64String(encryptData(tbCCNo.Text)));
                cmd.Parameters.AddWithValue("@CCExMth", Convert.ToDateTime(tbCCExMth.Text));
                cmd.Parameters.AddWithValue("@CVV", tbCVV.Text.Trim());
                cmd.Parameters.AddWithValue("@PassHash", finalHash);
                cmd.Parameters.AddWithValue("@PassSalt", salt);
                cmd.Parameters.AddWithValue("@Key", Convert.ToBase64String(Key));
                cmd.Parameters.AddWithValue("@IV", Convert.ToBase64String(IV));

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        protected byte[] encryptData(string data)
        {
            byte[] cipherText = null;
            try
            {
                RijndaelManaged cipher = new RijndaelManaged();
                cipher.IV = IV;
                cipher.Key = Key;
                ICryptoTransform encryptTransform = cipher.CreateEncryptor();
                byte[] plainText = Encoding.UTF8.GetBytes(data);
                cipherText = encryptTransform.TransformFinalBlock(plainText, 0, plainText.Length);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally { }
            return cipherText;
        }

        protected void btnSignUp_Click(object sender, EventArgs e)
        {
            bool checkField = FieldChecker();

            if (checkField) 
            {
                string pwd = tbPass.Text.ToString().Trim(); ;

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
                CreateAccount();
                Response.Redirect("Login.aspx");
            }
            
        }
    }
}