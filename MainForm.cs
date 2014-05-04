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
    public partial class MainForm : Form
    {
        SqlConnection con = null;
        public MainForm()
        {
            InitializeComponent();
            con = new connectDB().getConn();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("Select  * From clinicalSettings", con);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lblHospitalName.Text = dr["hospital_name"].ToString();
                    lblSlogan.Text = dr["hospital_slogan"].ToString();
                    lblAddress.Text = dr["hospital_address"].ToString();
                    lblCity.Text = dr["city"].ToString();
                    lblState.Text = dr["state"].ToString();
                    lblZipcode.Text = dr["zipcode"].ToString();
                    lblCountry.Text = dr["country"].ToString();
                    lblPhone1.Text = dr["phone1"].ToString();
                    lblPhone2.Text = dr["phone2"].ToString();
                    lblEmail.Text = dr["email"].ToString();
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

        private void btnLogin_Click(object sender, EventArgs e)
        {
            userlogin c = new userlogin();
            
            c.Show();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
     
        private void lblSlogan_Click(object sender, EventArgs e)
        {

        }

        private void btnClose1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       
    }
}