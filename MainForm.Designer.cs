namespace SoftwareRendering
{
    partial class MainForm
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
            this.pb_surface = new System.Windows.Forms.PictureBox();
            this.t_ticker = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pb_surface)).BeginInit();
            this.SuspendLayout();
            // 
            // pb_surface
            // 
            this.pb_surface.BackColor = System.Drawing.Color.Black;
            this.pb_surface.Location = new System.Drawing.Point(0, 0);
            this.pb_surface.Name = "pb_surface";
            this.pb_surface.Size = new System.Drawing.Size(480, 360);
            this.pb_surface.TabIndex = 0;
            this.pb_surface.TabStop = false;
            // 
            // t_ticker
            // 
            this.t_ticker.Interval = 16;
            this.t_ticker.Tick += new System.EventHandler(this.t_ticker_Tick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(480, 360);
            this.Controls.Add(this.pb_surface);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sheepstein 3D";
            ((System.ComponentModel.ISupportInitialize)(this.pb_surface)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pb_surface;
        private System.Windows.Forms.Timer t_ticker;
    }
}

