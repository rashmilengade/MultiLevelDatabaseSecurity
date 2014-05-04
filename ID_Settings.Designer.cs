namespace healthcare
{
    partial class ID_Settings
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.txt_pat_prefix = new System.Windows.Forms.TextBox();
            this.txt_pat_id = new System.Windows.Forms.TextBox();
            this.txt_doc_prefix = new System.Windows.Forms.TextBox();
            this.txt_doc_id = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txt_emp_prefix = new System.Windows.Forms.TextBox();
            this.txt_emp_id = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(34, 19);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(291, 22);
            this.label1.TabIndex = 0;
            this.label1.Text = "\"Patient,Doctor Auto ID Settings\"";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(59, 62);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(579, 19);
            this.label2.TabIndex = 1;
            this.label2.Text = "\"Please note that you will not be able to change these settings, once you configu" +
                "re it.\"";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(76, 104);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(247, 19);
            this.label3.TabIndex = 2;
            this.label3.Text = "Prefix Patient ID with{3 Characters}";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(76, 143);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(216, 19);
            this.label4.TabIndex = 3;
            this.label4.Text = "Patient ID starts from (Number)";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(76, 184);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(249, 19);
            this.label5.TabIndex = 4;
            this.label5.Text = "Prefix Doctor ID  with{3Characters} ";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(78, 218);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(210, 19);
            this.label6.TabIndex = 5;
            this.label6.Text = "Doctor ID starts from(Number)";
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(189, 352);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(192, 36);
            this.button1.TabIndex = 6;
            this.button1.Text = "Update Settings";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(421, 352);
            this.button2.Margin = new System.Windows.Forms.Padding(2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(94, 36);
            this.button2.TabIndex = 7;
            this.button2.Text = "Close";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // txt_pat_prefix
            // 
            this.txt_pat_prefix.Location = new System.Drawing.Point(345, 105);
            this.txt_pat_prefix.Margin = new System.Windows.Forms.Padding(2);
            this.txt_pat_prefix.Name = "txt_pat_prefix";
            this.txt_pat_prefix.Size = new System.Drawing.Size(266, 20);
            this.txt_pat_prefix.TabIndex = 8;
            // 
            // txt_pat_id
            // 
            this.txt_pat_id.Location = new System.Drawing.Point(345, 144);
            this.txt_pat_id.Margin = new System.Windows.Forms.Padding(2);
            this.txt_pat_id.Name = "txt_pat_id";
            this.txt_pat_id.Size = new System.Drawing.Size(266, 20);
            this.txt_pat_id.TabIndex = 9;
            // 
            // txt_doc_prefix
            // 
            this.txt_doc_prefix.Location = new System.Drawing.Point(345, 185);
            this.txt_doc_prefix.Margin = new System.Windows.Forms.Padding(2);
            this.txt_doc_prefix.Name = "txt_doc_prefix";
            this.txt_doc_prefix.Size = new System.Drawing.Size(266, 20);
            this.txt_doc_prefix.TabIndex = 10;
            // 
            // txt_doc_id
            // 
            this.txt_doc_id.Location = new System.Drawing.Point(345, 217);
            this.txt_doc_id.Margin = new System.Windows.Forms.Padding(2);
            this.txt_doc_id.Name = "txt_doc_id";
            this.txt_doc_id.Size = new System.Drawing.Size(266, 20);
            this.txt_doc_id.TabIndex = 11;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(78, 255);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(263, 19);
            this.label7.TabIndex = 12;
            this.label7.Text = "Prefix Employee ID with{3 Characters}";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(78, 291);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(228, 19);
            this.label8.TabIndex = 13;
            this.label8.Text = "Employee ID starts from(Number)";
            // 
            // txt_emp_prefix
            // 
            this.txt_emp_prefix.Location = new System.Drawing.Point(345, 255);
            this.txt_emp_prefix.Margin = new System.Windows.Forms.Padding(2);
            this.txt_emp_prefix.Name = "txt_emp_prefix";
            this.txt_emp_prefix.Size = new System.Drawing.Size(266, 20);
            this.txt_emp_prefix.TabIndex = 14;
            // 
            // txt_emp_id
            // 
            this.txt_emp_id.Location = new System.Drawing.Point(345, 292);
            this.txt_emp_id.Margin = new System.Windows.Forms.Padding(2);
            this.txt_emp_id.Name = "txt_emp_id";
            this.txt_emp_id.Size = new System.Drawing.Size(266, 20);
            this.txt_emp_id.TabIndex = 15;
            // 
            // ID_Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::healthcare.Properties.Resources.background;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(764, 408);
            this.Controls.Add(this.txt_emp_id);
            this.Controls.Add(this.txt_emp_prefix);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txt_doc_id);
            this.Controls.Add(this.txt_doc_prefix);
            this.Controls.Add(this.txt_pat_id);
            this.Controls.Add(this.txt_pat_prefix);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "ID_Settings";
            this.Text = "ID_Settings";
            this.Load += new System.EventHandler(this.ID_Settings_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox txt_pat_prefix;
        private System.Windows.Forms.TextBox txt_pat_id;
        private System.Windows.Forms.TextBox txt_doc_prefix;
        private System.Windows.Forms.TextBox txt_doc_id;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txt_emp_prefix;
        private System.Windows.Forms.TextBox txt_emp_id;
    }
}