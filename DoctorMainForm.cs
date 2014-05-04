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
    public partial class DoctorMainForm : Form
    {
        public DoctorMainForm()
        {
            InitializeComponent();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            Add_Edit_Patient_Details c = new Add_Edit_Patient_Details();
            c.MdiParent = this;
            c.Show();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            ViewAppointments c = new ViewAppointments();
            c.MdiParent = this;
            c.Show();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            Add_Patient_Details c = new Add_Patient_Details();
            c.MdiParent = this;
            c.Show();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            ViewPatients c = new ViewPatients();
            c.MdiParent = this;
            c.Show();
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            Add_Edit_Patient_Details c = new Add_Edit_Patient_Details();
            c.MdiParent = this;
            c.Show();
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       
    }
}
