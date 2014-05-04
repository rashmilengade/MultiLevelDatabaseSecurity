using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace healthcare
{
    public partial class Clinical_Settings : Form
    {
        SqlConnection con = null;
        int saveflag = 1;
        public Clinical_Settings()
        {
            InitializeComponent();
            con = new connectDB().getConn();
        }
        void enableControls()
        {
            txtHospitalName.Enabled = true;
            txtSlogan.Enabled = true;
            txtAddress.Enabled = true;
            txtEmail.Enabled = true;
            txtPhone1.Enabled = true;
            txtPhone2.Enabled = true;
            btnUpdate.Enabled = true;
            btnClose.Enabled = true;
        }

        void disableControls()
        {
            txtHospitalName.Enabled = false;
            txtSlogan.Enabled = false;
            txtAddress.Enabled = false;
            txtEmail.Enabled = false;
            txtPhone1.Enabled = false;
            txtPhone2.Enabled = false;
            btnUpdate.Enabled = false;
            btnClose.Enabled = false;
        }

        void clearControls()
        {
            txtHospitalName.Text = "";
            txtSlogan.Text = "";
            txtAddress.Text = "";
            txtEmail.Text = "";
            txtPhone1.Text = "";
            txtPhone2.Text = "";
            btnUpdate.Text = "";
            btnClose.Text = "";
        }
        

        private void btnImage_Click(object sender, EventArgs e)
        {

        }

        private void Clinical_Settings_Load(object sender, EventArgs e)
        {
            enableControls();
            try
            {
                SqlCommand cmd = new SqlCommand("Select  * From clinicalSettings", con);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    txtHospitalName.Text = dr["hospital_name"].ToString();
                    txtSlogan.Text = dr["hospital_slogan"].ToString();
                    txtAddress.Text = dr["hospital_address"].ToString();
                    txtCity.Text = dr["city"].ToString();
                    txtState.Text = dr["state"].ToString();
                    txtZipcode.Text = dr["zipcode"].ToString();
                    txtCountry.Text = dr["country"].ToString();
                    txtPhone1.Text = dr["phone1"].ToString();
                    txtPhone2.Text = dr["phone2"].ToString();
                    txtFax.Text = dr["fax"].ToString();
                    txtEmail.Text = dr["email"].ToString();
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

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (txtHospitalName.Text.Equals(""))
            {
                MessageBox.Show("Hospital Name Field Is Blank");
                txtHospitalName.Focus();
                return;
            }
            if (txtSlogan.Text.Equals(""))
            {
                MessageBox.Show("Hospital Slogan Field Is Blank");
                txtSlogan.Focus();
                return;
            }
            if (txtAddress.Text.Equals(""))
            {
                MessageBox.Show("Hospital Address Field Is Blank");
                txtAddress.Focus();
                return;
            }
            if (txtEmail.Text.Equals(""))
            {
                MessageBox.Show("Hospital Email Field Is Blank");
                txtEmail.Focus();
                return;
            }
            if (txtPhone1.Text.Equals(""))
            {
                MessageBox.Show("Hospital Phone Number Field Is Blank");
                txtPhone1.Focus();
                return;
            }
            if (txtPhone2.Text.Equals(""))
            {
                MessageBox.Show("Hospital Phone Number Field Is Blank");
                txtPhone2.Focus();
                return;
            }
            try
            {
                if (saveflag == 1)
                {
                    SqlCommand inscmd = new SqlCommand("INSERT INTO clinicalSettings values ('" + txtHospitalName.Text + "','" + txtSlogan.Text + "','" + txtAddress.Text + "','" + txtCity.Text + "','" + txtState.Text + "','" + txtZipcode.Text + "','" + txtCountry.Text + "','" + txtPhone1.Text + "','" + txtPhone2.Text + "','" + txtFax.Text + "','" + txtEmail.Text + "')", con);
                    con.Open();
                    inscmd.ExecuteNonQuery();
                    MessageBox.Show("New Settings Added");
                }
                else
                {
                    SqlCommand updcmd = new SqlCommand("Update clinicalSettings set hospital_name='" + txtHospitalName.Text + "', hospital_slogan='" + txtSlogan.Text + "',hospital_address='" + txtAddress.Text + "',city='" + txtCity.Text + "',state='" + txtState.Text + "',zipcode='" + txtZipcode.Text + "',country='" + txtCountry.Text + "',phone1='" + txtPhone1.Text + "',phone2='" + txtPhone2.Text + "',fax='" + txtFax.Text + "',email='" + txtEmail.Text + "'", con);
                    con.Open();
                    updcmd.ExecuteNonQuery();
                    MessageBox.Show("New Settings Updated");
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
    }
}
