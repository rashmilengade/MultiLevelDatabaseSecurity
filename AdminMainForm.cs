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
    public partial class AdminMainForm : Form
    {
        public AdminMainForm()
        {
            InitializeComponent();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Clinical_Settings c = new Clinical_Settings();
            c.MdiParent = this;
            c.Show();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            ID_Settings c = new ID_Settings();
            c.MdiParent = this;
            c.Show();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            Add_Doctor_Details c = new Add_Doctor_Details();
            c.MdiParent = this;
            c.Show();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            Add_Employee_Details c = new Add_Employee_Details();
            c.MdiParent = this;
            c.Show();
        }

        private void AdminMainForm_Load(object sender, EventArgs e)
        {

        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            ViewDoctorInformsation c = new ViewDoctorInformsation();
            c.MdiParent = this;
            c.Show();
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            Add_Employee_Details c = new Add_Employee_Details();
            c.MdiParent = this;
            c.Show();
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            Add_Doctor_Details c = new Add_Doctor_Details();
            c.MdiParent = this;
            c.Show();
        }
    }
}
