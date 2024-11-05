namespace SE_Project.PagesParts
{
    partial class ProjectCard
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProjectCard));
            this.guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            this.CardDesc = new System.Windows.Forms.TextBox();
            this.guna2ComboBox1 = new Guna.UI2.WinForms.Guna2ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.guna2Panel2 = new Guna.UI2.WinForms.Guna2Panel();
            this.guna2ImageRadioButton1 = new Guna.UI2.WinForms.Guna2ImageRadioButton();
            this.CardTitle = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.CardCreatedBy = new System.Windows.Forms.TextBox();
            this.guna2Panel1.SuspendLayout();
            this.guna2Panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // guna2Panel1
            // 
            this.guna2Panel1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(118)))), ((int)(((byte)(212)))));
            this.guna2Panel1.BorderThickness = 1;
            this.guna2Panel1.Controls.Add(this.CardCreatedBy);
            this.guna2Panel1.Controls.Add(this.label1);
            this.guna2Panel1.Controls.Add(this.CardDesc);
            this.guna2Panel1.Controls.Add(this.guna2ComboBox1);
            this.guna2Panel1.Controls.Add(this.label3);
            this.guna2Panel1.Controls.Add(this.label2);
            this.guna2Panel1.Controls.Add(this.guna2Panel2);
            this.guna2Panel1.Location = new System.Drawing.Point(17, 15);
            this.guna2Panel1.Margin = new System.Windows.Forms.Padding(13, 12, 0, 0);
            this.guna2Panel1.Name = "guna2Panel1";
            this.guna2Panel1.Size = new System.Drawing.Size(253, 217);
            this.guna2Panel1.TabIndex = 0;
            // 
            // CardDesc
            // 
            this.CardDesc.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.CardDesc.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CardDesc.Location = new System.Drawing.Point(4, 68);
            this.CardDesc.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.CardDesc.Multiline = true;
            this.CardDesc.Name = "CardDesc";
            this.CardDesc.Size = new System.Drawing.Size(245, 37);
            this.CardDesc.TabIndex = 5;
            this.CardDesc.Text = "Project Management System";
            this.CardDesc.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // guna2ComboBox1
            // 
            this.guna2ComboBox1.BackColor = System.Drawing.Color.Transparent;
            this.guna2ComboBox1.BorderRadius = 10;
            this.guna2ComboBox1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.guna2ComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.guna2ComboBox1.FillColor = System.Drawing.Color.Cyan;
            this.guna2ComboBox1.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.guna2ComboBox1.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.guna2ComboBox1.Font = new System.Drawing.Font("Century Gothic", 9.75F);
            this.guna2ComboBox1.ForeColor = System.Drawing.Color.Black;
            this.guna2ComboBox1.ItemHeight = 30;
            this.guna2ComboBox1.Items.AddRange(new object[] {
            "Completed",
            "InProgress"});
            this.guna2ComboBox1.Location = new System.Drawing.Point(77, 166);
            this.guna2ComboBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.guna2ComboBox1.Name = "guna2ComboBox1";
            this.guna2ComboBox1.Size = new System.Drawing.Size(171, 36);
            this.guna2ComboBox1.TabIndex = 4;
            this.guna2ComboBox1.SelectedIndexChanged += new System.EventHandler(this.guna2ComboBox1_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(4, 177);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 23);
            this.label3.TabIndex = 3;
            this.label3.Text = "Status";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(3, 44);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 19);
            this.label2.TabIndex = 1;
            this.label2.Text = "Description";
            // 
            // guna2Panel2
            // 
            this.guna2Panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(118)))), ((int)(((byte)(212)))));
            this.guna2Panel2.BorderRadius = 10;
            this.guna2Panel2.Controls.Add(this.guna2ImageRadioButton1);
            this.guna2Panel2.Controls.Add(this.CardTitle);
            this.guna2Panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.guna2Panel2.Location = new System.Drawing.Point(0, 0);
            this.guna2Panel2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.guna2Panel2.Name = "guna2Panel2";
            this.guna2Panel2.Size = new System.Drawing.Size(253, 39);
            this.guna2Panel2.TabIndex = 0;
            // 
            // guna2ImageRadioButton1
            // 
            this.guna2ImageRadioButton1.CheckedState.Image = ((System.Drawing.Image)(resources.GetObject("resource.Image")));
            this.guna2ImageRadioButton1.Image = ((System.Drawing.Image)(resources.GetObject("guna2ImageRadioButton1.Image")));
            this.guna2ImageRadioButton1.ImageOffset = new System.Drawing.Point(0, 0);
            this.guna2ImageRadioButton1.ImageRotate = 0F;
            this.guna2ImageRadioButton1.Location = new System.Drawing.Point(204, 6);
            this.guna2ImageRadioButton1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.guna2ImageRadioButton1.Name = "guna2ImageRadioButton1";
            this.guna2ImageRadioButton1.Size = new System.Drawing.Size(32, 30);
            this.guna2ImageRadioButton1.TabIndex = 1;
            this.guna2ImageRadioButton1.CheckedChanged += new System.EventHandler(this.guna2ImageRadioButton1_CheckedChanged);
            // 
            // CardTitle
            // 
            this.CardTitle.AutoSize = true;
            this.CardTitle.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CardTitle.ForeColor = System.Drawing.Color.White;
            this.CardTitle.Location = new System.Drawing.Point(7, 10);
            this.CardTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.CardTitle.Name = "CardTitle";
            this.CardTitle.Size = new System.Drawing.Size(50, 23);
            this.CardTitle.TabIndex = 0;
            this.CardTitle.Text = "Task";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(3, 103);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 19);
            this.label1.TabIndex = 6;
            this.label1.Text = "Created By";
            // 
            // CardCreatedBy
            // 
            this.CardCreatedBy.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.CardCreatedBy.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CardCreatedBy.Location = new System.Drawing.Point(3, 126);
            this.CardCreatedBy.Margin = new System.Windows.Forms.Padding(4);
            this.CardCreatedBy.Multiline = true;
            this.CardCreatedBy.Name = "CardCreatedBy";
            this.CardCreatedBy.Size = new System.Drawing.Size(245, 37);
            this.CardCreatedBy.TabIndex = 7;
            this.CardCreatedBy.Text = "Project Management System";
            // 
            // ProjectCard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.guna2Panel1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "ProjectCard";
            this.Size = new System.Drawing.Size(272, 234);
            this.Load += new System.EventHandler(this.ProjectCard_Load);
            this.guna2Panel1.ResumeLayout(false);
            this.guna2Panel1.PerformLayout();
            this.guna2Panel2.ResumeLayout(false);
            this.guna2Panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel2;
        private System.Windows.Forms.Label label2;
        private Guna.UI2.WinForms.Guna2ImageRadioButton guna2ImageRadioButton1;
        private System.Windows.Forms.Label label3;
        private Guna.UI2.WinForms.Guna2ComboBox guna2ComboBox1;
        private System.Windows.Forms.Label CardTitle;
        private System.Windows.Forms.TextBox CardDesc;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox CardCreatedBy;
    }
}
