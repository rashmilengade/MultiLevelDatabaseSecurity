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
    public partial class ViewPatients : Form
    {
        SqlConnection con = null;
        public ViewPatients()
        {
            InitializeComponent();
            con = new connectDB().getConn();
        }
        void fillgrid()
        {
            SqlDataAdapter da = new SqlDataAdapter("Select PatId,Patname,Address,email,cellno,Gender from Patient", con);
            DataSet ds = new DataSet();
            da.Fill(ds, "Patient");
            dataGridView1.DataSource = ds.Tables[0];
        }
        private void ViewPatients_Load(object sender, EventArgs e)
        {
            fillgrid();
        }
    }
}
