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

using System.IO;
using System.Data.SqlClient;

namespace healthcare
{
    public partial class biometriclogin : Form
    {
        string uname = "";
        BioAPI m_BioAPI;
        NBioAPI.Type.WINDOW_OPTION m_WinOption; //used in Capture function
        NBioAPI.Type.DEVICE_INFO_EX[] m_DeviceInfoEx;
        NBioAPI.Type.FIR_TEXTENCODE m_textFIR; //for storing the fingerprint
        static short DeviceID = NBioAPI.Type.DEVICE_ID.NONE; // for getting the device id connected to machine
        SqlConnection con = null;
        public biometriclogin()
        {
            InitializeComponent();
            m_BioAPI = new BioAPI();
            m_WinOption = new NBioAPI.Type.WINDOW_OPTION();
            m_WinOption.Option2 = new NBioAPI.Type.WINDOW_OPTION_2();
            con = new connectDB().getConn();
            uname = "";
        }
        public biometriclogin(string u,string t)
        {
            InitializeComponent();
            m_BioAPI = new BioAPI();
            m_WinOption = new NBioAPI.Type.WINDOW_OPTION();
            m_WinOption.Option2 = new NBioAPI.Type.WINDOW_OPTION_2();
            con = new connectDB().getConn();
            uname = u;
            txtUserId.Text = u;
            lblEType.Text = t;

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

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

        private void biometriclogin_Load(object sender, EventArgs e)
        {
            try
            {
                NBioAPI.Type.VERSION version = new NBioAPI.Type.VERSION();
                m_BioAPI.GetVersion(out version);
                // Microsoft.ACE.OLEDB.12.0
                Text = String.Format("BSP Demo for C#.NET (BSP Version : v{0}.{1:D4})", version.Major, version.Minor);
                // myconnection.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0; Data Source =" + AppDomain.CurrentDomain.BaseDirectory + "BioFP.mdb";  // Set database path to connection string
                //myconnection.Open();
                // to load the devicenames connected to the machine
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

        private void button1_Click_1(object sender, EventArgs e)
        {
            //code to capture the fingerprint data and then verify with the database record.
            //  call this method to set Window_option
            // m_BioAPI.SetWindowStyle(pbVerify.Handle.ToInt32(),true,false,false,false );
            SetInitValue(pbVerify.Handle.ToInt32());
            NBioAPI.Type.HFIR m_FIR;
            NBioAPI.Type.FIR_TEXTENCODE m1_textFIR;
            //validation for txtName textbox
            if (txtUserId.Text == "")
            {
                MessageBox.Show("Please Enter User Id to verify ", "Verifcation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            //capture the fingerprint and then store the result in m_FIR and return ret as output
            uint ret = m_BioAPI.Capture(out m_FIR, NBioAPI.Type.TIMEOUT.DEFAULT, m_WinOption);
            if (ret == BioAPI.Error.NONE) //if no errors then extract the values from the DB and then match
            {

                m_BioAPI.GetTextFIRFromHandle(m_FIR, out m1_textFIR, true);
                lblMessage.Text = "Capture Successful !!";
                try
                {
                    //create a Sql Command 
                    if (lblEType.Text.Equals("Doctor"))
                    {
                        SqlCommand cmd = new SqlCommand("Select * from doctor where DocId ='" + txtUserId.Text + "'");
                        cmd.Connection = con; //initilize the cmd object with the connection made
                        con.Open();//opens the connection
                        SqlDataReader reader = cmd.ExecuteReader(); //execute the command and then store the result in the reader object
                        string userID = "";
                        string Fpdata = "";
                        while (reader.Read())
                        {
                            userID = reader["DocId"].ToString();
                            Fpdata = reader["fingerprint"].ToString();

                        }
                        con.Close();//close the connection
                        //create a FIR_TextEncode object and then store the encoded image data in it
                        NBioAPI.Type.FIR_TEXTENCODE textFIR = new NBioAPI.Type.FIR_TEXTENCODE();
                        textFIR.TextFIR = Fpdata;
                        bool result;
                        //create a payload
                        NBioAPI.Type.FIR_PAYLOAD payload = new NBioAPI.Type.FIR_PAYLOAD();
                        m_BioAPI.VerifyMatch(textFIR, m1_textFIR, out result, payload); //verify
                        if (result) //if result is true then verified success else failed
                        {
                            DoctorMainForm bl = new DoctorMainForm();
                            bl.Show();
                            this.Close();

                        }
                        else
                            MessageBox.Show("Verification Failed !!! Try Again ");
                        //pbVerify.Image = null;

                        cmd.Dispose();
                    }

                    else if (lblEType.Text.Equals("Employee"))
                    {
                        SqlCommand cmd = new SqlCommand("Select * from Employee where EmpId ='" + txtUserId.Text + "'");
                        cmd.Connection = con; //initilize the cmd object with the connection made
                        con.Open();//opens the connection
                        SqlDataReader reader = cmd.ExecuteReader(); //execute the command and then store the result in the reader object
                        string userID = "";
                        string Fpdata = "";
                        while (reader.Read())
                        {
                            userID = reader["EmpId"].ToString();
                            Fpdata = reader["fingerprint"].ToString();

                        }
                        con.Close();//close the connection
                        //create a FIR_TextEncode object and then store the encoded image data in it
                        NBioAPI.Type.FIR_TEXTENCODE textFIR = new NBioAPI.Type.FIR_TEXTENCODE();
                        textFIR.TextFIR = Fpdata;
                        bool result;
                        //create a payload
                        NBioAPI.Type.FIR_PAYLOAD payload = new NBioAPI.Type.FIR_PAYLOAD();
                        m_BioAPI.VerifyMatch(textFIR, m1_textFIR, out result, payload); //verify
                        if (result) //if result is true then verified success else failed
                        {
                            EmployeeMainForm bl = new EmployeeMainForm();
                            bl.Show();
                            this.Close();
                        }
                        else
                            MessageBox.Show("Verification Failed !!! Try Again ");
                        //pbVerify.Image = null;

                        cmd.Dispose();
                    }
                    else 
                    {
                        SqlCommand cmd = new SqlCommand("Select * from Administrator where AdminId ='" + txtUserId.Text + "'");
                        cmd.Connection = con; //initilize the cmd object with the connection made
                        con.Open();//opens the connection
                        SqlDataReader reader = cmd.ExecuteReader(); //execute the command and then store the result in the reader object
                        string userID = "";
                        string Fpdata = "";
                        while (reader.Read())
                        {
                            userID = reader["AdminId"].ToString();
                            Fpdata = reader["fingerprint"].ToString();

                        }
                        con.Close();//close the connection
                        //create a FIR_TextEncode object and then store the encoded image data in it
                        NBioAPI.Type.FIR_TEXTENCODE textFIR = new NBioAPI.Type.FIR_TEXTENCODE();
                        textFIR.TextFIR = Fpdata;
                        bool result;
                        //create a payload
                        NBioAPI.Type.FIR_PAYLOAD payload = new NBioAPI.Type.FIR_PAYLOAD();
                        m_BioAPI.VerifyMatch(textFIR, m1_textFIR, out result, payload); //verify
                        if (result) //if result is true then verified success else failed
                        {
                            AdminMainForm bl = new AdminMainForm();
                            bl.Show();
                            this.Close();
                        }
                        else
                            MessageBox.Show("Verification Failed !!! Try Again ");
                        //pbVerify.Image = null;

                        cmd.Dispose();
                    }

                }


                catch (Exception ex)
                {
                    lblMessage.Text = "Database Exceptiom occured..";
                }
            }
            else
            {
                MessageBox.Show("Capture process Failed !!!");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
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

    }
}
