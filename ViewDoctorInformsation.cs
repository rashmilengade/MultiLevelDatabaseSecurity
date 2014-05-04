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
    public partial class ViewDoctorInformsation : Form
    {
        SqlConnection con = null;
        public ViewDoctorInformsation()
        {
            InitializeComponent();
            con = new connectDB().getConn();
        }
        void fillgrid()
        {
            SqlDataAdapter da = new SqlDataAdapter("Select DocId,DocName,Address,Email,CellNo,Qualification,Specialization from Doctor", con);
            DataSet ds = new DataSet();
            da.Fill(ds, "Doctor");
            dataGridView1.DataSource = ds.Tables[0];
        }

        private void ViewDoctorInformsation_Load(object sender, EventArgs e)
        {
            fillgrid();
        }
    }
}
