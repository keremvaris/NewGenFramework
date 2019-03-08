namespace FwGen
{
    partial class Form1
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
      this.panel1 = new System.Windows.Forms.Panel();
      this.comboBox1 = new System.Windows.Forms.ComboBox();
      this.label1 = new System.Windows.Forms.Label();
      this.label5 = new System.Windows.Forms.Label();
      this.label4 = new System.Windows.Forms.Label();
      this.txtProjectName = new System.Windows.Forms.TextBox();
      this.textBox3 = new System.Windows.Forms.TextBox();
      this.label2 = new System.Windows.Forms.Label();
      this.button1 = new System.Windows.Forms.Button();
      this.chkNh = new System.Windows.Forms.CheckBox();
      this.chkEf = new System.Windows.Forms.CheckBox();
      this.textBox2 = new System.Windows.Forms.TextBox();
      this.label3 = new System.Windows.Forms.Label();
      this.textBox1 = new System.Windows.Forms.TextBox();
      this.lblProjectPath = new System.Windows.Forms.Label();
      this.panel1.SuspendLayout();
      this.SuspendLayout();
      // 
      // panel1
      // 
      this.panel1.Controls.Add(this.comboBox1);
      this.panel1.Controls.Add(this.label1);
      this.panel1.Controls.Add(this.label5);
      this.panel1.Controls.Add(this.label4);
      this.panel1.Controls.Add(this.txtProjectName);
      this.panel1.Controls.Add(this.textBox3);
      this.panel1.Controls.Add(this.label2);
      this.panel1.Controls.Add(this.button1);
      this.panel1.Controls.Add(this.chkNh);
      this.panel1.Controls.Add(this.chkEf);
      this.panel1.Controls.Add(this.textBox2);
      this.panel1.Controls.Add(this.label3);
      this.panel1.Controls.Add(this.textBox1);
      this.panel1.Controls.Add(this.lblProjectPath);
      this.panel1.Location = new System.Drawing.Point(0, 3);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(692, 439);
      this.panel1.TabIndex = 0;
      this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
      // 
      // comboBox1
      // 
      this.comboBox1.FormattingEnabled = true;
      this.comboBox1.Location = new System.Drawing.Point(107, 307);
      this.comboBox1.Name = "comboBox1";
      this.comboBox1.Size = new System.Drawing.Size(525, 24);
      this.comboBox1.TabIndex = 16;
      this.comboBox1.Visible = false;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
      this.label1.Location = new System.Drawing.Point(16, 314);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(62, 17);
      this.label1.TabIndex = 15;
      this.label1.Text = "Entities";
      this.label1.Visible = false;
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
      this.label5.Location = new System.Drawing.Point(12, 218);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(638, 68);
      this.label5.TabIndex = 14;
      this.label5.Text = resources.GetString("label5.Text");
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
      this.label4.Location = new System.Drawing.Point(12, 111);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(100, 17);
      this.label4.TabIndex = 13;
      this.label4.Text = "ProjectName";
      // 
      // txtProjectName
      // 
      this.txtProjectName.Location = new System.Drawing.Point(124, 106);
      this.txtProjectName.Name = "txtProjectName";
      this.txtProjectName.Size = new System.Drawing.Size(525, 22);
      this.txtProjectName.TabIndex = 12;
      // 
      // textBox3
      // 
      this.textBox3.Location = new System.Drawing.Point(124, 78);
      this.textBox3.Name = "textBox3";
      this.textBox3.Size = new System.Drawing.Size(525, 22);
      this.textBox3.TabIndex = 11;
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
      this.label2.Location = new System.Drawing.Point(12, 83);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(103, 17);
      this.label2.TabIndex = 10;
      this.label2.Text = "ContextName";
      // 
      // button1
      // 
      this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
      this.button1.Location = new System.Drawing.Point(508, 367);
      this.button1.Name = "button1";
      this.button1.Size = new System.Drawing.Size(120, 45);
      this.button1.TabIndex = 9;
      this.button1.Text = "Generate";
      this.button1.UseVisualStyleBackColor = true;
      this.button1.Click += new System.EventHandler(this.button1_Click);
      // 
      // chkNh
      // 
      this.chkNh.AutoSize = true;
      this.chkNh.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
      this.chkNh.Location = new System.Drawing.Point(123, 142);
      this.chkNh.Name = "chkNh";
      this.chkNh.Size = new System.Drawing.Size(112, 21);
      this.chkNh.TabIndex = 8;
      this.chkNh.Text = "NHibernate";
      this.chkNh.UseVisualStyleBackColor = true;
      // 
      // chkEf
      // 
      this.chkEf.AutoSize = true;
      this.chkEf.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
      this.chkEf.Location = new System.Drawing.Point(253, 142);
      this.chkEf.Name = "chkEf";
      this.chkEf.Size = new System.Drawing.Size(149, 21);
      this.chkEf.TabIndex = 7;
      this.chkEf.Text = "EntityFramework";
      this.chkEf.UseVisualStyleBackColor = true;
      // 
      // textBox2
      // 
      this.textBox2.Location = new System.Drawing.Point(124, 50);
      this.textBox2.Name = "textBox2";
      this.textBox2.Size = new System.Drawing.Size(525, 22);
      this.textBox2.TabIndex = 6;
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
      this.label3.Location = new System.Drawing.Point(12, 55);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(100, 17);
      this.label3.TabIndex = 5;
      this.label3.Text = "Entities Path";
      // 
      // textBox1
      // 
      this.textBox1.Location = new System.Drawing.Point(124, 22);
      this.textBox1.Name = "textBox1";
      this.textBox1.Size = new System.Drawing.Size(525, 22);
      this.textBox1.TabIndex = 3;
      // 
      // lblProjectPath
      // 
      this.lblProjectPath.AutoSize = true;
      this.lblProjectPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
      this.lblProjectPath.Location = new System.Drawing.Point(12, 27);
      this.lblProjectPath.Name = "lblProjectPath";
      this.lblProjectPath.Size = new System.Drawing.Size(97, 17);
      this.lblProjectPath.TabIndex = 0;
      this.lblProjectPath.Text = "Project Path";
      // 
      // Form1
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(695, 444);
      this.Controls.Add(this.panel1);
      this.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
      this.Name = "Form1";
      this.Text = "NewGenFramework Code Generator";
      this.Load += new System.EventHandler(this.Form1_Load);
      this.panel1.ResumeLayout(false);
      this.panel1.PerformLayout();
      this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblProjectPath;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox chkNh;
        private System.Windows.Forms.CheckBox chkEf;
        private System.Windows.Forms.Button button1;
        public System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.TextBox txtProjectName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label1;
    }
}

