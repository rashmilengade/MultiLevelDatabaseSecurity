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
    public partial class ID_Settings : Form
    {
        SqlConnection con = null;
        int saveflag = 1;
        public ID_Settings()
        {
            InitializeComponent();
            con = new connectDB().getConn();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txt_doc_prefix.Text.Equals(""))
            {
                MessageBox.Show("Enter Doctor ID Prefix maximum of 3 characters");
                txt_doc_prefix.Focus();
                return;
            }
            if (txt_doc_id.Text.Equals(""))
            {
                MessageBox.Show("Enter Start Range of Doctor Id");
                txt_doc_id.Focus();
                return;
            }
            
            if (txt_pat_prefix.Text.Equals(""))
            {
                MessageBox.Show("Enter Patient ID Prefix maximum of 3 characters");
                txt_pat_prefix.Focus();
                return;
            }
            if (txt_pat_id.Text.Equals(""))
            {
                MessageBox.Show("Enter Start Range of Patient Id");
                txt_pat_id.Focus();
                return;
            }
            if (txt_emp_prefix.Text.Equals(""))
            {
                MessageBox.Show("Enter Employee ID Prefix maximum of 3 characters");
                txt_emp_prefix.Focus();
                return;
            }
            if (txt_emp_id.Text.Equals(""))
            {
                MessageBox.Show("Enter Start Range of Employee Id");
                txt_emp_id.Focus();
                return;
            }
            try
            {
                if (saveflag == 1)
                {
                    SqlCommand inscmd = new SqlCommand("INSERT INTO IdSettings values ('" + txt_doc_prefix.Text + "'," + txt_doc_id.Text + ",'" + txt_pat_prefix.Text + "'," + txt_pat_id.Text + ",'" + txt_emp_prefix.Text + "'," + txt_emp_id.Text + ")", con);
                    con.Open();
                    inscmd.ExecuteNonQuery();
                    MessageBox.Show("New Settings Added");
                }
                else
                {
                    SqlCommand updcmd = new SqlCommand("Update IdSettings set docPrefix='" + txt_doc_prefix.Text + "', docid=" + txt_doc_id.Text + ",patprefix='" + txt_pat_prefix.Text + "',patid=" + txt_pat_id.Text+",empprefix='" + txt_emp_prefix.Text + "',empid=" + txt_emp_id.Text , con);
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

        private void ID_Settings_Load(object sender, EventArgs e)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("Select  * From IDSettings", con);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                   txt_doc_prefix.Text = dr["docprefix"].ToString();
                   txt_doc_id.Text= dr["docid"].ToString();
                   txt_pat_prefix.Text = dr["patprefix"].ToString();
                   txt_pat_id.Text = dr["patid"].ToString();
                   txt_emp_prefix.Text = dr["empprefix"].ToString();
                   txt_emp_id.Text = dr["empid"].ToString();
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

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
