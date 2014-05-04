using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Security.Cryptography;

namespace healthcare
{
    public partial class Add_Edit_Patient_Details : Form
    {
        SqlConnection con = null;
        int saveflag = 1;
        public Add_Edit_Patient_Details()
        {
            InitializeComponent();
            con = new connectDB().getConn();
        }

        private void Add_Edit_Patient_Details_Load(object sender, EventArgs e)
        {

        }
        void enableControls()
        {
            cboDoctorName.Enabled = true;
            txtSymptoms.Enabled = true;
            txtDiagnosis.Enabled = true;
            txtTreatment.Enabled = true;
            txtCaseSummary.Enabled = true;
            txtPrescription.Enabled = true;
            
        }

        void disableControls()
        {
            cboDoctorName.Enabled = false;
            txtSymptoms.Enabled = false;
            txtDiagnosis.Enabled = false;
            txtTreatment.Enabled = false;
            txtCaseSummary.Enabled = false;
            txtPrescription.Enabled = false;
        }

        void clearControls()
        {
            cboDoctorName.Text = "";
            txtSymptoms.Text = "";
            txtDiagnosis.Text = "";
            txtTreatment.Text = "";
            txtCaseSummary.Text = "";
            txtPrescription.Text = "";
            
            

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

        public static string EncryptString(string Message, string Passphrase)
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

            // Step 3. Setup the encoder
            TDESAlgorithm.Key = TDESKey;
            TDESAlgorithm.Mode = CipherMode.ECB;
            TDESAlgorithm.Padding = PaddingMode.PKCS7;

            // Step 4. Convert the input string to a byte[]
            byte[] DataToEncrypt = UTF8.GetBytes(Message);

            // Step 5. Attempt to encrypt the string
            try
            {
                ICryptoTransform Encryptor = TDESAlgorithm.CreateEncryptor();
                Results = Encryptor.TransformFinalBlock(DataToEncrypt, 0, DataToEncrypt.Length);
            }
            finally
            {
                // Clear the TripleDes and Hashprovider services of any sensitive information
                TDESAlgorithm.Clear();
                HashProvider.Clear();
            }

            // Step 6. Return the encrypted string as a base64 encoded string
            return Convert.ToBase64String(Results);
        }



        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            enableControls();
            
            btnSave.Enabled = true;
            //btnNew.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("Select  * From Patient where PatId='" + txtPatientId.Text + "'", con);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lblPatientID.Text = dr["PatId"].ToString();
                    lblPatientName.Text = dr["PatName"].ToString();
                    lblgender.Text = dr["Gender"].ToString();
                    lblbloodgroup.Text = dr["Bloodgp"].ToString();

                }
                con.Close();
                SqlCommand cmd1 = new SqlCommand("Select  * From Consultation where PatId = '" + txtPatientId.Text + "' ", con);
                con.Open();
                SqlDataReader dr1 = cmd1.ExecuteReader();
                while (dr1.Read())
                {
                    string symptoms = dr1["Symptoms"].ToString();
                    string diagnosis = "" + dr1["Diagnosis"];
                    string treatment = dr1["Treatment"].ToString();
                    string prescription = dr1["Prescription"].ToString();
                    string casesummary = dr1["CaseSummary"].ToString();

                    string Password = "123456";
                    string DecryptedSymptoms = DecryptString(symptoms, Password);
                    string DecryptedDiagnosis = DecryptString(diagnosis, Password);
                    string DecryptedTreatment = DecryptString(treatment, Password);
                    string DecryptedPrescription = DecryptString(prescription, Password);
                    string DecryptedCaseSummary = DecryptString(casesummary, Password);
                    txtSymptoms1.Text = DecryptedSymptoms;
                    txtDiagnosis1.Text = DecryptedDiagnosis;
                    txtTreatment1.Text = DecryptedTreatment;
                    txtPrescription1.Text = DecryptedPrescription;
                    txtCaseSummary1.Text = DecryptedCaseSummary;

                    saveflag = 2;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                con.Close();
            }
            
        }

        private void Label10_Click(object sender, EventArgs e)
        {

        }

        private void lblpid_Click(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtSymptoms.Text.Equals(""))
            {
                MessageBox.Show("Symptoms Details Is Blank");
                return;
            }
            if (txtDiagnosis.Text.Equals(""))
            {
                MessageBox.Show("Diagnosis Details Is Blank");
                return;
            }
            if (txtTreatment.Text.Equals(""))
            {
                MessageBox.Show("Treatment Plan Details Is Blank");
                return;
            }
            if (txtPrescription.Text.Equals(""))
            {
                MessageBox.Show("Prescription Details Is Blank");
                return;
            }
            if (txtCaseSummary.Text.Equals(""))
            {
                MessageBox.Show("Case Summary Details Is Blank");
                return;
            }
            try
            {
                string Password = "123456";

                string EncryptedSymptoms = EncryptString(txtSymptoms.Text, Password);
                string EncryptedDiagnosis = EncryptString(txtDiagnosis.Text, Password);
                string EncryptedTreatment = EncryptString(txtTreatment.Text, Password);
                string EncryptedPrescription = EncryptString(txtPrescription.Text, Password);
                string EncryptedCaseSummary = EncryptString(txtCaseSummary.Text, Password);
                if (saveflag == 1)
                {
                    SqlCommand inscmd = new SqlCommand("INSERT INTO Consultation values ('" + cboDoctorName.Text + "','" + txtPatientId.Text + "','" + EncryptedSymptoms + "','" + EncryptedDiagnosis+ "','" + EncryptedTreatment + "','" + EncryptedPrescription + "','" + EncryptedCaseSummary + "')", con);
                    con.Open();
                    inscmd.ExecuteNonQuery();
                    MessageBox.Show("Consultation Details Saved");
                }
                else
                {
                    SqlCommand updcmd = new SqlCommand("Update Consultation set DoctorName='" + cboDoctorName.Text + "', PatID='" + txtPatientId.Text + "',Symptoms='" + EncryptedSymptoms + "',Diagnosis='" + EncryptedDiagnosis + "',Treatment='" + EncryptedTreatment + "',Prescription='" +EncryptedPrescription + "',CaseSummary='" + EncryptedCaseSummary + "'", con);
                    con.Open();
                    updcmd.ExecuteNonQuery();
                    MessageBox.Show("Consultation Details Saved");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                con.Close();
                
            }

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

    }
}
