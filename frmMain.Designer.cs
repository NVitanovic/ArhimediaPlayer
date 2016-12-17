namespace CIITLAB_UltraSonic2016
{
    partial class frmMain
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
            this.components = new System.ComponentModel.Container();
            this.tCheck = new System.Windows.Forms.Timer(this.components);
            this.pbLogo = new System.Windows.Forms.PictureBox();
            this.lblConsole = new System.Windows.Forms.Label();
            this.lblConsole2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // tCheck
            // 
            this.tCheck.Interval = 1;
            this.tCheck.Tick += new System.EventHandler(this.tCheck_Tick);
            // 
            // pbLogo
            // 
            this.pbLogo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbLogo.Image = global::CIITLAB_UltraSonic2016.Properties.Resources.Logo;
            this.pbLogo.Location = new System.Drawing.Point(0, 0);
            this.pbLogo.Name = "pbLogo";
            this.pbLogo.Size = new System.Drawing.Size(842, 469);
            this.pbLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbLogo.TabIndex = 0;
            this.pbLogo.TabStop = false;
            // 
            // lblConsole
            // 
            this.lblConsole.AutoSize = true;
            this.lblConsole.Location = new System.Drawing.Point(0, 0);
            this.lblConsole.Name = "lblConsole";
            this.lblConsole.Size = new System.Drawing.Size(58, 13);
            this.lblConsole.TabIndex = 1;
            this.lblConsole.Text = "CONSOLE";
            // 
            // lblConsole2
            // 
            this.lblConsole2.AutoSize = true;
            this.lblConsole2.Location = new System.Drawing.Point(0, 13);
            this.lblConsole2.Name = "lblConsole2";
            this.lblConsole2.Size = new System.Drawing.Size(67, 13);
            this.lblConsole2.TabIndex = 2;
            this.lblConsole2.Text = "CONSOLE 2";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.ClientSize = new System.Drawing.Size(842, 469);
            this.ControlBox = false;
            this.Controls.Add(this.lblConsole2);
            this.Controls.Add(this.lblConsole);
            this.Controls.Add(this.pbLogo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmMain";
            this.Text = "UltraSonic Vitanovic i Zdravkovic";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer tCheck;
        private System.Windows.Forms.PictureBox pbLogo;
        private System.Windows.Forms.Label lblConsole;
        private System.Windows.Forms.Label lblConsole2;
    }
}

