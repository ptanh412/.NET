namespace SE_Project.View.PagesParts
{
    partial class AllTasks
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
            this.btnAddTask = new Guna.UI2.WinForms.Guna2Button();
            this.pnAllCardTask = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // btnAddTask
            // 
            this.btnAddTask.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnAddTask.BorderRadius = 22;
            this.btnAddTask.CheckedState.FillColor = System.Drawing.Color.DarkGreen;
            this.btnAddTask.CheckedState.ForeColor = System.Drawing.Color.White;
            this.btnAddTask.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAddTask.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnAddTask.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnAddTask.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnAddTask.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnAddTask.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnAddTask.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddTask.ForeColor = System.Drawing.Color.White;
            this.btnAddTask.Location = new System.Drawing.Point(810, 4);
            this.btnAddTask.Margin = new System.Windows.Forms.Padding(4);
            this.btnAddTask.Name = "btnAddTask";
            this.btnAddTask.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnAddTask.Size = new System.Drawing.Size(240, 55);
            this.btnAddTask.TabIndex = 14;
            this.btnAddTask.Text = "Add Task";
            // 
            // pnAllCardTask
            // 
            this.pnAllCardTask.Location = new System.Drawing.Point(0, 81);
            this.pnAllCardTask.Margin = new System.Windows.Forms.Padding(0);
            this.pnAllCardTask.Name = "pnAllCardTask";
            this.pnAllCardTask.Size = new System.Drawing.Size(1189, 504);
            this.pnAllCardTask.TabIndex = 13;
            // 
            // AllTasks
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnAddTask);
            this.Controls.Add(this.pnAllCardTask);
            this.Name = "AllTasks";
            this.Size = new System.Drawing.Size(1189, 588);
            this.ResumeLayout(false);

        }

        #endregion
        private Guna.UI2.WinForms.Guna2Button btnAddTask;
        private System.Windows.Forms.FlowLayoutPanel pnAllCardTask;

    }
}
