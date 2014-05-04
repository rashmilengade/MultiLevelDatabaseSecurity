using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BioEnable.SDK.BioEnBSP;  // for HFDU06 device
using NITGEN.SDK.NBioBSP;
using System.Data.SqlClient;
using System.IO;
using System.Security.Cryptography;

namespace healthcare
{
    public partial class Add_Doctor_Details : Form
    {
        BioAPI m_BioAPI;
        NBioAPI.Type.WINDOW_OPTION m_WinOption; //used in Capture function
        NBioAPI.Type.DEVICE_INFO_EX[] m_DeviceInfoEx;
        NBioAPI.Type.FIR_TEXTENCODE m_textFIR; //for storing the fingerprint
        static short DeviceID = NBioAPI.Type.DEVICE_ID.NONE; // for getting the device id connected to machine
        int saveflag = 1;
        SqlConnection con = null;
        public Add_Doctor_Details()
        {
            InitializeComponent();
            m_BioAPI = new BioAPI();
            m_WinOption = new NBioAPI.Type.WINDOW_OPTION();
            m_WinOption.Option2 = new NBioAPI.Type.WINDOW_OPTION_2();
            con = new connectDB().getConn();
        }

        void enableControls()
        {
            txtDocId.Enabled = true;
            txtDoctorName.Enabled = true;
            txtAddress.Enabled = true;
            txtEmail.Enabled = true;
            txtExperience.Enabled = true;
            txtPassword.Enabled = true;
            txtQualification.Enabled = true;
            txtRPassword.Enabled = true;
            txtSpecialization.Enabled = true;
            mtxtCellNo.Enabled = true;
            cboGender.Enabled = true;
            dtpDOB.Enabled = true;
            btnBrowse.Enabled = true;
            btnCapture.Enabled = true;
            btnSearch.Enabled = true;
        }

        void disableControls()
        {
            txtDocId.Enabled = false;
            txtDoctorName.Enabled = false;
            txtAddress.Enabled = false;
            txtEmail.Enabled = false;
            txtExperience.Enabled = false;
            txtPassword.Enabled = false;
            txtQualification.Enabled = false;
            txtRPassword.Enabled = false;
            txtSpecialization.Enabled = false;
            mtxtCellNo.Enabled = false;
            cboGender.Enabled = false;
            dtpDOB.Enabled = false;
            btnBrowse.Enabled = false;
            btnCapture.Enabled = false;
            btnSearch.Enabled = false;
        }

        void clearControls()
        {
            txtDocId.Text = "";
            txtDoctorName.Text = "";
            txtAddress.Text = "";
            txtEmail.Text = "";
            txtExperience.Text = "";
            txtPassword.Text = "";
            txtQualification.Text = "";
            txtRPassword.Text = "";
            txtSpecialization.Text = "";
            mtxtCellNo.Text = "";
            cboGender.Text = "";
            
        }
        private void Add_Doctor_Details_Load(object sender, EventArgs e)
        {
            disableControls();
            clearControls();
            txtDocId.Enabled = true;
            btnSearch.Enabled = true;
            try
            {
                NBioAPI.Type.VERSION version = new NBioAPI.Type.VERSION();
                m_BioAPI.GetVersion(out version);
                // Microsoft.ACE.OLEDB.12.0
                Text = String.Format("BSP Demo for C#.NET (BSP Version : v{0}.{1:D4})", version.Major, version.Minor);
                // myconnection.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0; Data Source =" + AppDomain.CurrentDomain.BaseDirectory + "BioFP.mdb";  // Set database path to connection string
                //myconnection.Open();
                // to load the devicenames connected to the machine
                int i;
                uint nNumDevice;
                short[] nDeviceID;
                uint ret = m_BioAPI.EnumerateDevice(out nNumDevice, out nDeviceID, out m_DeviceInfoEx); //Get connected device 

                // add first element in combo box
                cboDevice.Items.Add("Auto_Detect");
                for (i = 0; i < nNumDevice; i++)
                {
                    cboDevice.Items.Add(m_DeviceInfoEx[i].Name + " (ID:" + m_DeviceInfoEx[i].Instance.ToString("D2") + ")");
                }
                if (cboDevice.Items.Count > 1)
                    lblMessage.Text = "Device Found...";
                else lblMessage.Text = "No Device Found...";
                cboDevice.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            
        }
        private void SetInitValue(Int32 handle)
        {
            // Window style

            m_WinOption.WindowStyle = NBioAPI.Type.WINDOW_STYLE.INVISIBLE;

            m_WinOption.Option2.FPForeColor[0] = Convert.ToByte("00", 16);
            m_WinOption.Option2.FPForeColor[1] = Convert.ToByte("00", 16);
            m_WinOption.Option2.FPForeColor[2] = Convert.ToByte("00", 16);

            m_WinOption.Option2.FPBackColor[0] = Convert.ToByte("FF", 16);
            m_WinOption.Option2.FPBackColor[1] = Convert.ToByte("FF", 16);
            m_WinOption.Option2.FPBackColor[2] = Convert.ToByte("FF", 16);





            // Window Option
            m_WinOption.WindowStyle |= (uint)NBioAPI.Type.WINDOW_STYLE.NO_FPIMG;
            m_WinOption.WindowStyle |= (uint)NBioAPI.Type.WINDOW_STYLE.NO_TOPMOST;
            m_WinOption.WindowStyle |= (uint)NBioAPI.Type.WINDOW_STYLE.NO_WELCOME;

            // Callback funtion




            // Select finger for enrollment
            m_WinOption.Option2.DisableFingerForEnroll[0] = (byte)NBioAPI.Type.FALSE;
            m_WinOption.Option2.DisableFingerForEnroll[1] = (byte)NBioAPI.Type.FALSE;
            m_WinOption.Option2.DisableFingerForEnroll[2] = (byte)NBioAPI.Type.FALSE;
            m_WinOption.Option2.DisableFingerForEnroll[3] = (byte)NBioAPI.Type.FALSE;
            m_WinOption.Option2.DisableFingerForEnroll[4] = (byte)NBioAPI.Type.FALSE;
            m_WinOption.Option2.DisableFingerForEnroll[5] = (byte)NBioAPI.Type.FALSE;
            m_WinOption.Option2.DisableFingerForEnroll[6] = (byte)NBioAPI.Type.FALSE;
            m_WinOption.Option2.DisableFingerForEnroll[7] = (byte)NBioAPI.Type.FALSE;
            m_WinOption.Option2.DisableFingerForEnroll[8] = (byte)NBioAPI.Type.FALSE;
            m_WinOption.Option2.DisableFingerForEnroll[9] = (byte)NBioAPI.Type.FALSE;
            m_WinOption.FingerWnd = (uint)handle;

        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            enableControls();
            clearControls();
            btnSave.Enabled = true;
            btnNew.Enabled = false;
            try
            {
                SqlCommand cmd = new SqlCommand("Select * from IDSettings",con);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    String prefix = dr["docPrefix"].ToString();
                    String id = dr["docid"].ToString();
                    txtDocId.Text = prefix + "_" + id;
                    SqlConnection con1 = new connectDB().getConn();
                    SqlCommand updateCmd = new SqlCommand("Update IDSettings set docid=docid+1", con1);
                    con1.Open();
                    updateCmd.ExecuteNonQuery();
                    con1.Close();
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
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


     
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtDocId.Text.Equals(""))
            {
                MessageBox.Show("Doctor Id Is Blank");
                return;
            }
            if (txtDoctorName.Text.Equals(""))
            {
                MessageBox.Show("Enter Doctor Name");
                txtDoctorName.Focus();
                return;
            }
            if (txtAddress.Text.Equals(""))
            {
                MessageBox.Show("Enter Address");
                txtAddress.Focus();
                return;
            }
            if (dtpDOB.Text.Equals(DateTime.Now.ToShortDateString()))
            {
                MessageBox.Show("Date Cannot be same as today's Date");
                dtpDOB.Focus();
                return;
            }
            if (txtEmail.Text.Equals(""))
            {
                MessageBox.Show("Enter Email Id");
                txtEmail.Focus();
                return;
            }
            if (txtQualification.Text.Equals(""))
            {
                MessageBox.Show("Enter Qualification");
                txtQualification.Focus();
                return;

            }
            if (mtxtCellNo.Text.Equals(""))
            {
                MessageBox.Show("Enter Cell No");
                mtxtCellNo.Focus();
                return;
            }
            if (cboGender.SelectedIndex==-1)
            {
                MessageBox.Show("Select Gender");
                cboGender.Focus();
                return;
            }
            if (txtSpecialization.Text.Equals(""))
            {
                MessageBox.Show("Enter Specialization");
                txtSpecialization.Focus();
                return;
            }
            if (txtExperience.Text.Equals(""))
            {
                MessageBox.Show("EnterExperience");
                txtExperience.Focus();
                return;
            }
            if (txtPassword.Text.Equals(""))
            {
                MessageBox.Show("Enter Password");
                txtPassword.Focus();
                return;
            }
            if (txtRPassword.Text.Equals(""))
            {
                MessageBox.Show("Re-Enter Password");
                txtRPassword.Focus();
                return;
            }
            if (!txtPassword.Text.Equals(txtRPassword.Text))
            {
                MessageBox.Show("Password Mismatch");
                txtRPassword.Focus();
                return;
            }
           // MD5Class mc = new MD5Class();
            //string pass = mc.EncodePassword(txtPassword.Text);
             //   MessageBox.Show(pass);
              //  string decode = mc.decodePassword(pass);
               // MessageBox.Show(decode);
            try
            {
                //bool exist = false;
                //SqlCommand cmd = new SqlCommand("Select * from Doctor where email='" + txtEmail.Text + "'", con);
                //con.Open();
                //SqlDataReader dr = cmd.ExecuteReader();
                //while (dr.Read())
                //{
                //    exist = true;
                //}
                //con.Close();
                string pass = txtPassword.Text;
                string Password = "123456";

                string EncryptedString = EncryptString(pass, Password);

                if (saveflag==1)
                {
                    SqlCommand insCmd = new SqlCommand("INSERT into Doctor (DocId,DocName,Address,DOB,Gender,email,cellno,qualification,Experience,Specialization,pass,fingerprint) values ('" + txtDocId.Text + "','" + txtDoctorName.Text + "','" + txtAddress.Text + "','" + dtpDOB.Text + "','" + cboGender.SelectedItem.ToString() + "','" + txtEmail.Text + "','" + mtxtCellNo.Text + "','" + txtQualification.Text + "','" + txtExperience.Text + "','" + txtSpecialization.Text + "','" + EncryptedString + "','" + m_textFIR.TextFIR + "')", con);
                    con.Open();
                    insCmd.ExecuteNonQuery();
                    MessageBox.Show("Doctor Registered");
                    con.Close();

                }
                else
                {
                   // MessageBox.Show("Update Doctor set docName='" + txtDoctorName.Text + "', Address='" + txtAddress.Text + "',DOB='" + dtpDOB.Text + "',Gender='" + cboGender.Text + "',Email='" + txtEmail.Text + "',CellNo='" + mtxtCellNo.Text + "',Qualification='" + txtQualification.Text + "',Experience='" + txtExperience.Text + "',Specialization='" + txtSpecialization.Text + "',pass='" + txtPassword.Text + "' where DocId = '" + txtDocId + "'");
                    SqlCommand updcmd = new SqlCommand("Update Doctor set docName='" + txtDoctorName.Text + "', Address='" + txtAddress.Text + "',DOB='" + dtpDOB.Text + "',Gender='" + cboGender.Text + "',Email='" + txtEmail.Text + "',CellNo='" + mtxtCellNo.Text + "',Qualification='" + txtQualification.Text + "',Experience='" + txtExperience.Text + "',Specialization='" + txtSpecialization.Text + "',pass='" + EncryptedString + "' where DocId = '" + txtDocId.Text + "'", con);
                    con.Open();
                    updcmd.ExecuteNonQuery();
                    MessageBox.Show("Doctor Details Updated");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            disableControls();
            clearControls();
            btnSave.Enabled = false;
            btnNew.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cboDevice_SelectedIndexChanged(object sender, EventArgs e)
        {
            //The CloseDevice method should be used to close the device. 
            //The same DeviceID used to call the Open method must be used again to call the CloseDevice method.
            //The current device must be closed before opening another device.

            // Code to Check the Opened devices and if open first close the active device 
            if (cboDevice.SelectedIndex > 0)
            {
                DeviceID = (short)(m_DeviceInfoEx[cboDevice.SelectedIndex - 1].NameID + (m_DeviceInfoEx[cboDevice.SelectedIndex - 1].Instance >> 8));
            }
            else
                DeviceID = NBioAPI.Type.DEVICE_ID.AUTO; //The device can be set automatically using NBioAPI.Type.DEVICE_ID.AUTO
            //closesthe active device
            m_BioAPI.CloseDevice(DeviceID);

            uint ret = m_BioAPI.OpenDevice(DeviceID);
            if (ret == BioAPI.Error.NONE)
            {
                // Open device success ...
                lblMessage.Text = "Device open Successfully !!";
            }
            else
            {
                // Open device failed ...
                lblMessage.Text = "Device open Failed !!";
            }
        }

        private void btnCapture_Click(object sender, EventArgs e)
        {
            try
            {

                SetInitValue(pbFingerPrint.Handle.ToInt32());
                

                //declare all your variables here
                NBioAPI.Type.HFIR m_FIR; //creates a FIR(Fingerprint Indetification Record) handle
                // uint ret3 = bioenable.Capture(out m_FIR);
                uint ret = m_BioAPI.Capture(out m_FIR, NBioAPI.Type.TIMEOUT.DEFAULT, m_WinOption);
                //-------------------- Capture Finger Print -----------------------------


                //-----------------------------------------------------------------------

                if (ret == BioAPI.Error.NONE)
                {
                    uint ret1 = m_BioAPI.GetTextFIRFromHandle(m_FIR, out m_textFIR, true);
                    //button_save.Visible = true;
                    // button_cancel.Visible = true;
                    if (ret1 != BioAPI.Error.NONE)
                    {
                        MessageBox.Show("Capture process failed !!! ");
                    }
                    
                }
                

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK) // Test result.
            {
                MessageBox.Show(openFileDialog1.FileName.ToString());

                string oldPath = openFileDialog1.FileName.ToString();
                string newpath = @"C:\NewFolder\";
                string newFileName = txtDocId.Text;
                FileInfo f1 = new FileInfo(oldPath);
                if (f1.Exists)
                {
                    if (!Directory.Exists(newpath))
                    {
                        Directory.CreateDirectory(newpath);
                    }
                    f1.CopyTo(string.Format("{0}{1}{2}", newpath, newFileName, f1.Extension));
                }
                pbIdentity.Image = Image.FromFile(newpath+newFileName+".jpeg");
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            enableControls();
            btnCapture.Enabled = false;
            try
            {
                SqlCommand cmd = new SqlCommand("Select  * From Doctor where docid='"+txtDocId.Text+"'", con);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    txtDocId.Text = dr["docId"].ToString();
                    txtDoctorName.Text = dr["docName"].ToString();
                    txtAddress.Text = dr["Address"].ToString();
                    dtpDOB.Text = dr["DOB"].ToString();
                    cboGender.Text = dr["Gender"].ToString();
                    txtEmail.Text = dr["Email"].ToString();
                    mtxtCellNo.Text = dr["CellNo"].ToString();
                    txtQualification.Text = dr["Qualification"].ToString();
                    txtExperience.Text = dr["Experience"].ToString();
                    txtSpecialization.Text = dr["Specialization"].ToString();
                    txtPassword.Text = dr["pass"].ToString();
                    txtRPassword.Text = dr["pass"].ToString();
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
            disableControls();
            btnNew.Enabled = true;
            btnSave.Enabled = true;
            btnSearch.Enabled = true;
            btnCancel.Enabled = true;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            enableControls();
            btnCapture.Enabled = false;
        }
    }
}
