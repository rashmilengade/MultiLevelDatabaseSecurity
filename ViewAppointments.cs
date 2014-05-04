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
    public partial class ViewAppointments : Form
    {
        SqlConnection con = null;
        public ViewAppointments()
        {
            InitializeComponent();
            con = new connectDB().getConn();
        }
        void fillgrid()
        {
            SqlDataAdapter da = new SqlDataAdapter("Select * from Appointment", con);
            DataSet ds = new DataSet();
            da.Fill(ds, "Appointment");
            dataGridView1.DataSource = ds.Tables[0];
        }
        private void ViewAppointments_Load(object sender, EventArgs e)
        {
            fillgrid();
        }
    }
}
