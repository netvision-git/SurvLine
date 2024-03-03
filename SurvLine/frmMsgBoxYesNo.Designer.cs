namespace SurvLine
{
    partial class frmMsgBoxYesNo
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
            this.imgFrame = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.fraButtons = new System.Windows.Forms.GroupBox();
            this.OKButton = new System.Windows.Forms.Button();
            this.AllYesButton = new System.Windows.Forms.Button();
            this.NoButton = new System.Windows.Forms.Button();
            this.AllNoButton = new System.Windows.Forms.Button();
            this.lblText = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.imgFrame)).BeginInit();
            this.fraButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // imgFrame
            // 
            this.imgFrame.Location = new System.Drawing.Point(20, 18);
            this.imgFrame.Name = "imgFrame";
            this.imgFrame.Size = new System.Drawing.Size(47, 31);
            this.imgFrame.TabIndex = 0;
            this.imgFrame.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.Location = new System.Drawing.Point(18, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "拡張高さ";
            // 
            // fraButtons
            // 
            this.fraButtons.Controls.Add(this.AllNoButton);
            this.fraButtons.Controls.Add(this.NoButton);
            this.fraButtons.Controls.Add(this.AllYesButton);
            this.fraButtons.Controls.Add(this.OKButton);
            this.fraButtons.Location = new System.Drawing.Point(45, 64);
            this.fraButtons.Name = "fraButtons";
            this.fraButtons.Size = new System.Drawing.Size(386, 50);
            this.fraButtons.TabIndex = 2;
            this.fraButtons.TabStop = false;
            this.fraButtons.Enter += new System.EventHandler(this.fraButtons_Enter);
            // 
            // OKButton
            // 
            this.OKButton.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.OKButton.Location = new System.Drawing.Point(11, 11);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(84, 25);
            this.OKButton.TabIndex = 0;
            this.OKButton.Text = "はい";
            this.OKButton.UseVisualStyleBackColor = true;
            // 
            // AllYesButton
            // 
            this.AllYesButton.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.AllYesButton.Location = new System.Drawing.Point(101, 11);
            this.AllYesButton.Name = "AllYesButton";
            this.AllYesButton.Size = new System.Drawing.Size(84, 25);
            this.AllYesButton.TabIndex = 1;
            this.AllYesButton.Text = "すべてはい";
            this.AllYesButton.UseVisualStyleBackColor = true;
            // 
            // NoButton
            // 
            this.NoButton.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.NoButton.Location = new System.Drawing.Point(191, 11);
            this.NoButton.Name = "NoButton";
            this.NoButton.Size = new System.Drawing.Size(84, 25);
            this.NoButton.TabIndex = 2;
            this.NoButton.Text = "いいえ";
            this.NoButton.UseVisualStyleBackColor = true;
            // 
            // AllNoButton
            // 
            this.AllNoButton.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.AllNoButton.Location = new System.Drawing.Point(281, 11);
            this.AllNoButton.Name = "AllNoButton";
            this.AllNoButton.Size = new System.Drawing.Size(84, 25);
            this.AllNoButton.TabIndex = 3;
            this.AllNoButton.Text = "すべていいえ";
            this.AllNoButton.UseVisualStyleBackColor = true;
            // 
            // lblText
            // 
            this.lblText.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblText.Location = new System.Drawing.Point(77, 10);
            this.lblText.Name = "lblText";
            this.lblText.Size = new System.Drawing.Size(375, 57);
            this.lblText.TabIndex = 3;
            // 
            // frmMsgBoxYesNo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(463, 120);
            this.Controls.Add(this.lblText);
            this.Controls.Add(this.fraButtons);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.imgFrame);
            this.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmMsgBoxYesNo";
            this.Text = "frmMsgBoxYesNo";
            this.Load += new System.EventHandler(this.frmMsgBoxYesNo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.imgFrame)).EndInit();
            this.fraButtons.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox imgFrame;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox fraButtons;
        private System.Windows.Forms.Button AllYesButton;
        private System.Windows.Forms.Button OKButton;
        private System.Windows.Forms.Button AllNoButton;
        private System.Windows.Forms.Button NoButton;
        private System.Windows.Forms.Label lblText;
    }
}