namespace SurvLine
{
    partial class frmProgressDialog2
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
            this.CancelButton = new System.Windows.Forms.Button();
            this.lblLabel0 = new System.Windows.Forms.Label();
            this.lblLabel1 = new System.Windows.Forms.Label();
            this.lblPrompt = new System.Windows.Forms.Label();
            this.lblTextSize = new System.Windows.Forms.Label();
            this.pgbProgress0 = new System.Windows.Forms.ProgressBar();
            this.pgbProgress1 = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // CancelButton
            // 
            this.CancelButton.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.CancelButton.Location = new System.Drawing.Point(181, 176);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(79, 31);
            this.CancelButton.TabIndex = 2;
            this.CancelButton.Text = "ｷｬﾝｾﾙ";
            this.CancelButton.UseVisualStyleBackColor = true;
            this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // lblLabel0
            // 
            this.lblLabel0.AutoSize = true;
            this.lblLabel0.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblLabel0.Location = new System.Drawing.Point(58, 35);
            this.lblLabel0.Name = "lblLabel0";
            this.lblLabel0.Size = new System.Drawing.Size(0, 12);
            this.lblLabel0.TabIndex = 3;
            // 
            // lblLabel1
            // 
            this.lblLabel1.AutoSize = true;
            this.lblLabel1.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblLabel1.Location = new System.Drawing.Point(58, 68);
            this.lblLabel1.Name = "lblLabel1";
            this.lblLabel1.Size = new System.Drawing.Size(0, 12);
            this.lblLabel1.TabIndex = 4;
            // 
            // lblPrompt
            // 
            this.lblPrompt.Location = new System.Drawing.Point(58, 115);
            this.lblPrompt.Name = "lblPrompt";
            this.lblPrompt.Size = new System.Drawing.Size(342, 58);
            this.lblPrompt.TabIndex = 5;
            // 
            // lblTextSize
            // 
            this.lblTextSize.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.lblTextSize.Location = new System.Drawing.Point(75, 124);
            this.lblTextSize.Name = "lblTextSize";
            this.lblTextSize.Size = new System.Drawing.Size(314, 20);
            this.lblTextSize.TabIndex = 6;
            // 
            // pgbProgress0
            // 
            this.pgbProgress0.Location = new System.Drawing.Point(60, 35);
            this.pgbProgress0.Name = "pgbProgress0";
            this.pgbProgress0.Size = new System.Drawing.Size(324, 20);
            this.pgbProgress0.TabIndex = 7;
            this.pgbProgress0.Tag = "0";
            // 
            // pgbProgress1
            // 
            this.pgbProgress1.Location = new System.Drawing.Point(60, 68);
            this.pgbProgress1.Name = "pgbProgress1";
            this.pgbProgress1.Size = new System.Drawing.Size(324, 20);
            this.pgbProgress1.TabIndex = 8;
            // 
            // frmProgressDialog2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(434, 231);
            this.Controls.Add(this.pgbProgress1);
            this.Controls.Add(this.pgbProgress0);
            this.Controls.Add(this.lblTextSize);
            this.Controls.Add(this.lblPrompt);
            this.Controls.Add(this.lblLabel1);
            this.Controls.Add(this.lblLabel0);
            this.Controls.Add(this.CancelButton);
            this.Name = "frmProgressDialog2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmProgressDialog";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button CancelButton;
        private System.Windows.Forms.Label lblLabel0;
        private System.Windows.Forms.Label lblLabel1;
        private System.Windows.Forms.Label lblPrompt;
        private System.Windows.Forms.Label lblTextSize;
        private System.Windows.Forms.ProgressBar pgbProgress1;
        private System.Windows.Forms.ProgressBar pgbProgress0;
    }
}