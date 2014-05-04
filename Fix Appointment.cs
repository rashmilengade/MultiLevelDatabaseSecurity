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
    public partial class Fix_Appointment : Form
    {
        int saveflag = 1;
        SqlConnection con = null;
        public Fix_Appointment()
        {
            InitializeComponent();
            con = new connectDB().getConn();
        }

        //void enableControls()
        //{
        //    txtDocId.Enabled = true;
        //    txtDoctorName.Enabled = true;
        //    txtAddress.Enabled = true;
        //    txtEmail.Enabled = true;
        //    txtExperience.Enabled = true;
        //    txtPassword.Enabled = true;
        //    txtQualification.Enabled = true;
        //    txtRPassword.Enabled = true;
        //    txtSpecialization.Enabled = true;
        //    txtUname.Enabled = true;
        //    mtxtCellNo.Enabled = true;
        //    cboGender.Enabled = true;
        //    dtpDOB.Enabled = true;
        //    btnBrowse.Enabled = true;
        //    btnCapture.Enabled = true;
        //}

        //void disableControls()
        //{
        //    txtDocId.Enabled = false;
        //    txtDoctorName.Enabled = false;
        //    txtAddress.Enabled = false;
        //    txtEmail.Enabled = false;
        //    txtExperience.Enabled = false;
        //    txtPassword.Enabled = false;
        //    txtQualification.Enabled = false;
        //    txtRPassword.Enabled = false;
        //    txtSpecialization.Enabled = false;
        //    txtUname.Enabled = false;
        //    mtxtCellNo.Enabled = false;
        //    cboGender.Enabled = false;
        //    dtpDOB.Enabled = false;
        //    btnBrowse.Enabled = false;
        //    btnCapture.Enabled = false;
        //}

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();


        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                bool exist = false;
                SqlCommand cmd = new SqlCommand("Select * from Patient where PatId='" + txtPatientId.Text + "'", con);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lblPateintName.Text = dr["PatName"].ToString();
                }
                con.Close();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlCommand insCmd = new SqlCommand("INSERT into Appointment (Pid,Date,Time) values ('" + txtPatientId.Text + "','" + txtDate.Text + "','" + dtpTime.Text + "')", con);
            con.Open();
            insCmd.ExecuteNonQuery();
            MessageBox.Show("Appointment confirmed");
            con.Close();
        }

        private void lblPatientName_TextChanged(object sender, EventArgs e)
        {

        }

        private void Fix_Appointment_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                bool exist = false;
                SqlCommand cmd = new SqlCommand("Select * from Patient where PatId='" + txtPatientId.Text + "' and docName='"+cboDoctorName.Text+"'", con);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lblPateintName.Text = dr["PatName"].ToString();
                    saveflag = 2;
                    exist = true;
                }
                if (!exist)
                    MessageBox.Show("Patient Does not belong to the specified doctor");
                con.Close();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            //if (txtDate.Text.Equals(DateTime.Now.ToShortDateString()))
            //{
              //  MessageBox.Show("Date Cannot be same as today's Date");
                //txtDate.Focus();
                //return;
            //}
            if (saveflag == 1)
            {
                SqlCommand insCmd = new SqlCommand("INSERT into Appointment (Pid,PatientName,DoctorName,AppDate,AppTime) values ('" + txtPatientId.Text + "','"+lblPateintName.Text+"','"+cboDoctorName.SelectedItem.ToString()+"','" + txtDate.Text + "','" + dtpTime.Text + "')", con);
                con.Open();
                insCmd.ExecuteNonQuery();
                MessageBox.Show("Appointment confirmed");

                con.Close();
            }
            else
            {
                SqlCommand updateCmd = new SqlCommand("Update  Appointment set Pid='" + txtPatientId.Text + "',PatientName='"+lblPateintName.Text+"',DoctorName='"+cboDoctorName.SelectedItem.ToString()+"', AppDate='" + txtDate.Text + "',AppTime='" + dtpTime.Text + "'", con);
                con.Open();
                updateCmd.ExecuteNonQuery();
                MessageBox.Show("Appointment Updated");

                con.Close();
            
            }
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            saveflag = 1;
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void Fix_Appointment_Load_1(object sender, EventArgs e)
        {
            txtDate.MinDate = DateTime.Now;
            cboDoctorName.Items.Clear();
            try
            {
                SqlCommand cmd = new SqlCommand("Select * from Doctor", con);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    cboDoctorName.Items.Add(dr["DocName"].ToString());

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

        private void txtDate_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
