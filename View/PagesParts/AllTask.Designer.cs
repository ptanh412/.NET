namespace SE_Project.PagesParts
{
    partial class AllTask
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
            this.AllProjectsPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.btn_AddTask = new Guna.UI2.WinForms.Guna2Button();
            this.SuspendLayout();
            // 
            // AllProjectsPanel
            // 
            this.AllProjectsPanel.Location = new System.Drawing.Point(0, 81);
            this.AllProjectsPanel.Margin = new System.Windows.Forms.Padding(0);
            this.AllProjectsPanel.Name = "AllProjectsPanel";
            this.AllProjectsPanel.Size = new System.Drawing.Size(1812, 588);
            this.AllProjectsPanel.TabIndex = 0;
            this.AllProjectsPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.AllProjectsPanel_Paint);
            // 
            // btn_AddTask
            // 
            this.btn_AddTask.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btn_AddTask.BorderRadius = 22;
            this.btn_AddTask.CheckedState.FillColor = System.Drawing.Color.DarkGreen;
            this.btn_AddTask.CheckedState.ForeColor = System.Drawing.Color.White;
            this.btn_AddTask.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_AddTask.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btn_AddTask.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btn_AddTask.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btn_AddTask.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btn_AddTask.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btn_AddTask.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_AddTask.ForeColor = System.Drawing.Color.White;
            this.btn_AddTask.Location = new System.Drawing.Point(4, 4);
            this.btn_AddTask.Margin = new System.Windows.Forms.Padding(4);
            this.btn_AddTask.Name = "btn_AddTask";
            this.btn_AddTask.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btn_AddTask.Size = new System.Drawing.Size(240, 55);
            this.btn_AddTask.TabIndex = 11;
            this.btn_AddTask.Text = "Add Task";
            this.btn_AddTask.Click += new System.EventHandler(this.btn_AddTask_Click);
            // 
            // AllTask
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.btn_AddTask);
            this.Controls.Add(this.AllProjectsPanel);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "AllTask";
            this.Size = new System.Drawing.Size(1189, 665);
            this.Load += new System.EventHandler(this.AllProjects_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel AllProjectsPanel;
        private Guna.UI2.WinForms.Guna2Button btn_AddTask;
    }
}
