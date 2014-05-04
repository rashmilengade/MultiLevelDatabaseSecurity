using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace healthcare
{
    public partial class EmployeeMainForm : Form
    {
        public EmployeeMainForm()
        {
            InitializeComponent();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Add_Patient_Details c = new Add_Patient_Details();
            c.MdiParent = this;
            c.Show();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            Add_Patient_Details c = new Add_Patient_Details();
            c.MdiParent = this;
            c.Show();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            Fix_Appointment c = new Fix_Appointment();
            c.MdiParent = this;
            c.Show();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            Fix_Appointment c = new Fix_Appointment();
            c.MdiParent = this;
            c.Show();
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
