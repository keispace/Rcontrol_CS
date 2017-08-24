namespace EZRcontrol
{
    partial class RClinetForm
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
            this.pbx_remote = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbx_remote)).BeginInit();
            this.SuspendLayout();
            // 
            // pbx_remote
            // 
            this.pbx_remote.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbx_remote.Location = new System.Drawing.Point(0, 0);
            this.pbx_remote.Name = "pbx_remote";
            this.pbx_remote.Size = new System.Drawing.Size(284, 261);
            this.pbx_remote.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbx_remote.TabIndex = 0;
            this.pbx_remote.TabStop = false;
            this.pbx_remote.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbx_remote_MouseDown);
            this.pbx_remote.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pbx_remote_MouseMove);
            this.pbx_remote.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pbx_remote_MouseUp);
            // 
            // RClinetForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.pbx_remote);
            this.Name = "RClinetForm";
            this.Text = "RClinetForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.RClinetForm_FormClosed);
            this.Load += new System.EventHandler(this.RClinetForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.RClinetForm_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.RClinetForm_KeyUp);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.RClinetForm_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.RClinetForm_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.RClinetForm_MouseUp);
            ((System.ComponentModel.ISupportInitialize)(this.pbx_remote)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pbx_remote;
    }
}