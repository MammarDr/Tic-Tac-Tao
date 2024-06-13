namespace Tict_Tao
{
    partial class startGame
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(startGame));
            this.pvp = new System.Windows.Forms.Button();
            this.pvc = new System.Windows.Forms.Button();
            this.Exit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // pvp
            // 
            this.pvp.Location = new System.Drawing.Point(444, 145);
            this.pvp.Name = "pvp";
            this.pvp.Size = new System.Drawing.Size(221, 70);
            this.pvp.TabIndex = 0;
            this.pvp.Text = "Player Vs Player";
            this.pvp.UseVisualStyleBackColor = true;
            this.pvp.Click += new System.EventHandler(this.pvp_Click);
            // 
            // pvc
            // 
            this.pvc.Location = new System.Drawing.Point(110, 145);
            this.pvc.Name = "pvc";
            this.pvc.Size = new System.Drawing.Size(221, 70);
            this.pvc.TabIndex = 1;
            this.pvc.Text = "Player Vs Computer";
            this.pvc.UseVisualStyleBackColor = true;
            this.pvc.Click += new System.EventHandler(this.pvc_Click);
            // 
            // Exit
            // 
            this.Exit.Location = new System.Drawing.Point(302, 326);
            this.Exit.Name = "Exit";
            this.Exit.Size = new System.Drawing.Size(167, 49);
            this.Exit.TabIndex = 2;
            this.Exit.Text = "Exit";
            this.Exit.UseVisualStyleBackColor = true;
            this.Exit.Click += new System.EventHandler(this.Exit_Click);
            // 
            // startGame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.Exit);
            this.Controls.Add(this.pvc);
            this.Controls.Add(this.pvp);
            this.Name = "startGame";
            this.Text = "startGame";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button pvp;
        private System.Windows.Forms.Button pvc;
        private System.Windows.Forms.Button Exit;
    }
}