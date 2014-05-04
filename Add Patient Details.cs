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


namespace healthcare
{
    public partial class Add_Patient_Details : Form
    {
        BioAPI m_BioAPI;
        NBioAPI.Type.WINDOW_OPTION m_WinOption; //used in Capture function
        NBioAPI.Type.DEVICE_INFO_EX[] m_DeviceInfoEx;
        NBioAPI.Type.FIR_TEXTENCODE m_textFIR; //for storing the fingerprint
        static short DeviceID = NBioAPI.Type.DEVICE_ID.NONE; // for getting the device id connected to machine
        int saveflag = 1;
        SqlConnection con = null;
        public Add_Patient_Details()
        {
            InitializeComponent();
            m_BioAPI = new BioAPI();
            m_WinOption = new NBioAPI.Type.WINDOW_OPTION();
            m_WinOption.Option2 = new NBioAPI.Type.WINDOW_OPTION_2();
            con = new connectDB().getConn();
        }

        void enableControls()
        {
            txtPatId.Enabled = true;
            txtPatientName.Enabled = true;
            txtAddress.Enabled = true;
            txtEmail.Enabled = true;
            txtPassword.Enabled = true;
            txtRPassword.Enabled = true;
            mtxtCellNo.Enabled = true;
            cboGender.Enabled = true;
            dtpDOB.Enabled = true;
            btnBrowse.Enabled = true;
            btnCapture.Enabled = true;
            txtBgp.Enabled = true;
            txtMs.Enabled = true;
            txtheight.Enabled = true;
            txtweight.Enabled = true;

        }

        void disableControls()
        {
            txtPatId.Enabled = false;
            txtPatientName.Enabled = false;
            txtAddress.Enabled = false;
            txtEmail.Enabled = false;
            txtPassword.Enabled = false;
            txtRPassword.Enabled = false;
            mtxtCellNo.Enabled = false;
            cboGender.Enabled = false;
            dtpDOB.Enabled = false;
            btnBrowse.Enabled = false;
            btnCapture.Enabled = false;
            txtBgp.Enabled = false;
            txtMs.Enabled = false;
            txtheight.Enabled = false;
            txtweight.Enabled = false;

        }

        void clearControls()
        {
            txtPatId.Text = "";
            txtPatientName.Text = "";
            txtAddress.Text = "";
            txtEmail.Text = "";
            txtPassword.Text = "";
            txtRPassword.Text = "";
            mtxtCellNo.Text = "";
            cboGender.Text = "";
            txtBgp.Text = "";
            txtMs.Text = "";
            txtheight.Text = "";
            txtweight.Text = "";

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void Add_Patient_Details_Load(object sender, EventArgs e)
        {

            disableControls();
            clearControls();
            txtPatId.Enabled = true;
            btnSearch.Enabled = true;
            comboBox1.Items.Clear();
            try
            {
                SqlCommand cmd = new SqlCommand("Select * from Doctor", con);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    comboBox1.Items.Add(dr["DocName"].ToString());
                    
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

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            enableControls();
            clearControls();
            btnSave.Enabled = true;
            btnNew.Enabled = false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

        }

        private void btnNew_Click_1(object sender, EventArgs e)
        {
            enableControls();
            clearControls();
            btnSave.Enabled = true;
            btnNew.Enabled = false;
            try
            {
                SqlCommand cmd = new SqlCommand("Select * from IDSettings", con);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    String prefix = dr["patPrefix"].ToString();
                    String id = dr["patid"].ToString();
                    txtPatId.Text = prefix + "_" + id;
                    SqlConnection con1 = new connectDB().getConn();
                    SqlCommand updateCmd = new SqlCommand("Update IDSettings set patid=patid+1", con1);
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

        private void btnSave_Click_1(object sender, EventArgs e)
        {
             if (txtPatId.Text.Equals(""))
            {
                MessageBox.Show("Patient Id Is Blank");
                return;
            }
            if (txtPatientName.Text.Equals(""))
            {
                MessageBox.Show("Enter Patient Name");
                txtPatientName.Focus();
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

            if (mtxtCellNo.Text.Equals(""))
            {
                MessageBox.Show("Enter Cell No");
                mtxtCellNo.Focus();
                return;
            }
            if (cboGender.SelectedIndex == -1)
            {
                MessageBox.Show("Select Gender");
                cboGender.Focus();
                return;
            }

            if (txtBgp.Text.Equals(""))
            {
                MessageBox.Show("Enter Blood group");
                txtBgp.Focus();
                return;
            }

            if (txtMs.Text.Equals(""))
            {
                MessageBox.Show("Enter Marital Status");
                txtMs.Focus();
                return;
            }


            if (txtheight.Text.Equals(""))
            {
                MessageBox.Show("Enter Height");
                txtheight.Focus();
                return;
            }

            if (txtweight.Text.Equals(""))
            {
                MessageBox.Show("Enter Weight");
                txtweight.Focus();
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
            try
            {
                //bool exist = false;
                //SqlCommand cmd = new SqlCommand("Select * from Patient where email='" + txtEmail.Text + "'", con);
                //con.Open();
                //SqlDataReader dr = cmd.ExecuteReader();
                //while (dr.Read())
                //{
                //    exist = true;
                //}
                //con.Close();
                if (saveflag==1)
                {
                    //MessageBox.Show("INSERT into Patient (PatId,PatName,Address,DOB,Gender,email,cellno,Bloodgp,Maritals,Height,Weight,pass,fingerprint) values ('" + txtPatId.Text + "','" + txtPatientName.Text + "','" + txtAddress.Text + "','" + dtpDOB.Text + "','" + cboGender.SelectedItem.ToString() + "','" + txtEmail.Text + "','" + mtxtCellNo.Text + "','" + txtBgp.Text + "','" + txtMs.Text + "','" + txtheight.Text + "','" + txtweight.Text + "','" + txtPassword.Text + "','" + m_textFIR.TextFIR + "')");
                    SqlCommand insCmd = new SqlCommand("INSERT into Patient (PatId,PatName,Address,DOB,Gender,email,cellno,Bloodgp,Maritals,Height,Weight,pass,fingerprint,DocName) values ('" + txtPatId.Text + "','" + txtPatientName.Text + "','" + txtAddress.Text + "','" + dtpDOB.Text + "','" + cboGender.SelectedItem.ToString() + "','" + txtEmail.Text + "','" + mtxtCellNo.Text + "','" + txtBgp.Text + "','" + txtMs.Text + "','" + txtheight.Text + "','" + txtweight.Text + "','" + txtPassword.Text + "','" + m_textFIR.TextFIR + "','" + comboBox1.SelectedItem.ToString() + "')", con);
                    con.Open();
                    insCmd.ExecuteNonQuery();
                    MessageBox.Show("Patient Registered");
                    con.Close();
                }
                else
                {
                    MessageBox.Show("Update Patient set PatName='" + txtPatientName.Text + "', Address='" + txtAddress.Text + "',DOB='" + dtpDOB.Text + "',Gender='" + cboGender.Text + "',Email='" + txtEmail.Text + "',CellNo='" + mtxtCellNo.Text + "',Bloodgp='" + txtBgp.Text + "',Maritals='" + txtMs.Text + "',Height='" + txtheight.Text + "',Weight='" + txtweight.Text + "',pass='" + txtPassword.Text + "' where PatId = '" + txtPatId.Text + "'");
                    SqlCommand updcmd = new SqlCommand("Update Patient set PatName='" + txtPatientName.Text + "', Address='" + txtAddress.Text + "',DOB='" + dtpDOB.Text + "',Gender='" + cboGender.Text + "',Email='" + txtEmail.Text + "',CellNo='" + mtxtCellNo.Text + "',Bloodgp='" + txtBgp.Text + "',Maritals='" + txtMs.Text + "',Height='" + txtheight.Text + "',Weight='" + txtweight.Text + "',pass='" + txtPassword.Text + "' where PatId = '" + txtPatId.Text + "'", con);
                    con.Open();
                    updcmd.ExecuteNonQuery();
                    MessageBox.Show("Patient Details Updated");
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

        private void button1_Click(object sender, EventArgs e)
        {
            enableControls();
            btnCapture.Enabled = false;
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

        private void btnSearch_Click(object sender, EventArgs e)
        {
            enableControls();
            btnCapture.Enabled = false;
            try
            {
                SqlCommand cmd = new SqlCommand("Select  * From Patient where PatId='"+txtPatId.Text+"'", con);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    txtPatientName.Text = dr["PatName"].ToString();
                    txtAddress.Text = dr["Address"].ToString();
                    dtpDOB.Text = dr["DOB"].ToString();
                    cboGender.Text = dr["Gender"].ToString();
                    txtEmail.Text = dr["Email"].ToString();
                    mtxtCellNo.Text = dr["CellNo"].ToString();
                    txtBgp.Text = dr["Bloodgp"].ToString();
                    txtMs.Text = dr["Maritals"].ToString();
                    txtheight.Text = dr["Height"].ToString();
                    txtweight.Text = dr["Weight"].ToString();
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

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK) // Test result.
            {
                MessageBox.Show(openFileDialog1.FileName.ToString());

                string oldPath = openFileDialog1.FileName.ToString();
                string newpath = @"C:\NewFolder\";
                string newFileName = txtPatId.Text;
                FileInfo f1 = new FileInfo(oldPath);
                if (f1.Exists)
                {
                    if (!Directory.Exists(newpath))
                    {
                        Directory.CreateDirectory(newpath);
                    }
                    f1.CopyTo(string.Format("{0}{1}{2}", newpath, newFileName, f1.Extension));
                }
                pbIdentity.Image = Image.FromFile(newpath + newFileName + ".jpeg");
            }
        }
        
    }
}

