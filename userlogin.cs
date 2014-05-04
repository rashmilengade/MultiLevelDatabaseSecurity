using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using System.Security.Cryptography;

namespace healthcare
{
    public partial class userlogin : Form
    {
        SqlConnection con = null;
        public userlogin()
        {
            InitializeComponent();
            con = new connectDB().getConn();
        }
        public static string DecryptString(string Message, string Passphrase)
        {
            byte[] Results;
            System.Text.UTF8Encoding UTF8 = new System.Text.UTF8Encoding();

            // Step 1. We hash the passphrase using MD5
            // We use the MD5 hash generator as the result is a 128 bit byte array
            // which is a valid length for the TripleDES encoder we use below

            MD5CryptoServiceProvider HashProvider = new MD5CryptoServiceProvider();
            byte[] TDESKey = HashProvider.ComputeHash(UTF8.GetBytes(Passphrase));

            // Step 2. Create a new TripleDESCryptoServiceProvider object
            TripleDESCryptoServiceProvider TDESAlgorithm = new TripleDESCryptoServiceProvider();

            // Step 3. Setup the decoder
            TDESAlgorithm.Key = TDESKey;
            TDESAlgorithm.Mode = CipherMode.ECB;
            TDESAlgorithm.Padding = PaddingMode.PKCS7;

            // Step 4. Convert the input string to a byte[]
            byte[] DataToDecrypt = Convert.FromBase64String(Message);

            // Step 5. Attempt to decrypt the string
            try
            {
                ICryptoTransform Decryptor = TDESAlgorithm.CreateDecryptor();
                Results = Decryptor.TransformFinalBlock(DataToDecrypt, 0, DataToDecrypt.Length);
            }
            finally
            {
                // Clear the TripleDes and Hashprovider services of any sensitive information
                TDESAlgorithm.Clear();
                HashProvider.Clear();
            }

            // Step 6. Return the decrypted string in UTF8 format
            return UTF8.GetString(Results);
        }
        private void button1_Click(object sender, EventArgs e)
        {


            if (txtUserId.Text.Equals(""))
            {
                MessageBox.Show("Doctor Id Is Blank");
                return;
            }

            if (txtPassword.Text.Equals(""))
            {
                MessageBox.Show("Enter Password");
                txtPassword.Focus();
                return;
            }
            try
            {
                if (cboType.Text.Equals("Doctor"))
                {
                    bool exist = false;
                    SqlCommand cmd = new SqlCommand("Select * from Doctor where DocId='" + txtUserId.Text + "'", con);
                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        txtUserId.Text = dr["DocId"].ToString();
                        string sav = dr["pass"].ToString();

                        string Password = "123456";
                        string DecryptedString = DecryptString(sav, Password);

                        if (DecryptedString.Equals(txtPassword.Text))
                        
                        {
                            exist = true;

                        }
                    }
                    con.Close();
                    if (exist)
                    {
                        biometriclogin bl = new biometriclogin(txtUserId.Text,cboType.SelectedItem.ToString());
                        bl.Show();
                        this.Close();

                    }
                    else
                    {
                        MessageBox.Show("Login failed");
                    }
                }
                else if (cboType.Text.Equals("Employee"))
                {

                    bool exist = false;
                    SqlCommand cmd = new SqlCommand("Select * from Employee where EmpId='" + txtUserId.Text + "'", con);
                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        txtUserId.Text = dr["EmpId"].ToString();
                        string sav = dr["pass"].ToString();
                        string Password = "123456";
                        string DecryptedString = DecryptString(sav, Password);

                        if (DecryptedString.Equals(txtPassword.Text))
                        
                        {
                            exist = true;

                        }
                    }
                    con.Close();
                    if (exist)
                    {
                        biometriclogin bl = new biometriclogin(txtUserId.Text, cboType.SelectedItem.ToString());
                        bl.Show();
                        this.Visible = false;
                    }
                    else
                    {
                        MessageBox.Show("Login failed");
                    }

                }
                else
                {
                    bool exist = false;
                    SqlCommand cmd = new SqlCommand("Select * from Administrator where AdminId='" + txtUserId.Text + "'", con);
                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        txtUserId.Text = dr["AdminId"].ToString();
                        string sav = dr["pass"].ToString();
                         string Password = "123456";
                         string DecryptedString = DecryptString(sav, Password);
                         if (txtPassword.Text.Equals(DecryptedString))
                        {
                            exist = true;

                        }
                    }
                    con.Close();
                    if (exist)
                    {
                        biometriclogin bl = new biometriclogin(txtUserId.Text, cboType.SelectedItem.ToString());
                        bl.Show();
                        this.Visible = false;
                    }
                    else
                    {
                        MessageBox.Show("Login failed");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void userlogin_Load(object sender, EventArgs e)
        {

        }
    }
}
