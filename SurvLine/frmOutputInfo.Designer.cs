namespace SurvLine
{
    partial class frmOutputInfo
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
            this.lblDescription = new System.Windows.Forms.GroupBox();
            this.optSession = new System.Windows.Forms.RadioButton();
            this.optAll = new System.Windows.Forms.RadioButton();
            this.cmbSession = new System.Windows.Forms.ComboBox();
            this.fraCtrls3 = new System.Windows.Forms.GroupBox();
            this.optAutomation = new System.Windows.Forms.RadioButton();
            this.optFile = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Label5 = new System.Windows.Forms.Label();
            this.cmbFixed = new System.Windows.Forms.ComboBox();
            this.CancelButton = new System.Windows.Forms.Button();
            this.OKButton = new System.Windows.Forms.Button();
            this.fraCtrls2 = new System.Windows.Forms.GroupBox();
            this.lblDescription.SuspendLayout();
            this.fraCtrls3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.fraCtrls2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblDescription
            // 
            this.lblDescription.Controls.Add(this.optSession);
            this.lblDescription.Controls.Add(this.optAll);
            this.lblDescription.Controls.Add(this.cmbSession);
            this.lblDescription.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblDescription.Location = new System.Drawing.Point(20, 19);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(312, 87);
            this.lblDescription.TabIndex = 0;
            this.lblDescription.TabStop = false;
            this.lblDescription.Text = "出力する範囲を選択してください";
            // 
            // optSession
            // 
            this.optSession.AutoSize = true;
            this.optSession.Location = new System.Drawing.Point(25, 49);
            this.optSession.Name = "optSession";
            this.optSession.Size = new System.Drawing.Size(72, 16);
            this.optSession.TabIndex = 8;
            this.optSession.TabStop = true;
            this.optSession.Text = "セッション";
            this.optSession.UseVisualStyleBackColor = true;
            // 
            // optAll
            // 
            this.optAll.AutoSize = true;
            this.optAll.Location = new System.Drawing.Point(24, 23);
            this.optAll.Name = "optAll";
            this.optAll.Size = new System.Drawing.Size(71, 16);
            this.optAll.TabIndex = 7;
            this.optAll.TabStop = true;
            this.optAll.Text = "現場全体";
            this.optAll.UseVisualStyleBackColor = true;
            // 
            // cmbSession
            // 
            this.cmbSession.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.cmbSession.FormattingEnabled = true;
            this.cmbSession.Location = new System.Drawing.Point(133, 44);
            this.cmbSession.Name = "cmbSession";
            this.cmbSession.Size = new System.Drawing.Size(115, 20);
            this.cmbSession.TabIndex = 6;
            // 
            // fraCtrls3
            // 
            this.fraCtrls3.Controls.Add(this.optAutomation);
            this.fraCtrls3.Controls.Add(this.optFile);
            this.fraCtrls3.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.fraCtrls3.Location = new System.Drawing.Point(20, 116);
            this.fraCtrls3.Name = "fraCtrls3";
            this.fraCtrls3.Size = new System.Drawing.Size(312, 89);
            this.fraCtrls3.TabIndex = 1;
            this.fraCtrls3.TabStop = false;
            this.fraCtrls3.Text = "出力先を選択してください";
            // 
            // optAutomation
            // 
            this.optAutomation.AutoSize = true;
            this.optAutomation.Location = new System.Drawing.Point(24, 47);
            this.optAutomation.Name = "optAutomation";
            this.optAutomation.Size = new System.Drawing.Size(190, 16);
            this.optAutomation.TabIndex = 8;
            this.optAutomation.TabStop = true;
            this.optAutomation.Text = "NS-Networkにエクスポートする。";
            this.optAutomation.UseVisualStyleBackColor = true;
            // 
            // optFile
            // 
            this.optFile.AutoSize = true;
            this.optFile.Location = new System.Drawing.Point(24, 23);
            this.optFile.Name = "optFile";
            this.optFile.Size = new System.Drawing.Size(129, 16);
            this.optFile.TabIndex = 7;
            this.optFile.TabStop = true;
            this.optFile.Text = "ファイルに出力する。";
            this.optFile.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmbFixed);
            this.groupBox1.Controls.Add(this.Label5);
            this.groupBox1.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.groupBox1.Location = new System.Drawing.Point(20, 219);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(312, 62);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            // 
            // Label5
            // 
            this.Label5.AutoSize = true;
            this.Label5.Location = new System.Drawing.Point(10, 3);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(124, 12);
            this.Label5.TabIndex = 0;
            this.Label5.Text = "計算に使用する既知点:";
            // 
            // cmbFixed
            // 
            this.cmbFixed.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.cmbFixed.FormattingEnabled = true;
            this.cmbFixed.Location = new System.Drawing.Point(54, 21);
            this.cmbFixed.Name = "cmbFixed";
            this.cmbFixed.Size = new System.Drawing.Size(252, 20);
            this.cmbFixed.TabIndex = 7;
            // 
            // CancelButton
            // 
            this.CancelButton.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.CancelButton.Location = new System.Drawing.Point(121, 13);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(95, 30);
            this.CancelButton.TabIndex = 1;
            this.CancelButton.Text = "ｷｬﾝｾﾙ";
            this.CancelButton.UseVisualStyleBackColor = true;
            // 
            // OKButton
            // 
            this.OKButton.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.OKButton.Location = new System.Drawing.Point(17, 14);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(95, 30);
            this.OKButton.TabIndex = 0;
            this.OKButton.Text = "OK";
            this.OKButton.UseVisualStyleBackColor = true;
            // 
            // fraCtrls2
            // 
            this.fraCtrls2.Controls.Add(this.CancelButton);
            this.fraCtrls2.Controls.Add(this.OKButton);
            this.fraCtrls2.Location = new System.Drawing.Point(110, 288);
            this.fraCtrls2.Name = "fraCtrls2";
            this.fraCtrls2.Size = new System.Drawing.Size(224, 54);
            this.fraCtrls2.TabIndex = 6;
            this.fraCtrls2.TabStop = false;
            // 
            // frmOutputInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(357, 345);
            this.Controls.Add(this.fraCtrls2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.fraCtrls3);
            this.Controls.Add(this.lblDescription);
            this.Name = "frmOutputInfo";
            this.Text = "frmOutputInfo";
            this.Load += new System.EventHandler(this.frmOutputInfo_Load);
            this.lblDescription.ResumeLayout(false);
            this.lblDescription.PerformLayout();
            this.fraCtrls3.ResumeLayout(false);
            this.fraCtrls3.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.fraCtrls2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox lblDescription;
        private System.Windows.Forms.RadioButton optSession;
        private System.Windows.Forms.RadioButton optAll;
        private System.Windows.Forms.ComboBox cmbSession;
        private System.Windows.Forms.GroupBox fraCtrls3;
        private System.Windows.Forms.RadioButton optAutomation;
        private System.Windows.Forms.RadioButton optFile;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cmbFixed;
        private System.Windows.Forms.Label Label5;
        private System.Windows.Forms.Button CancelButton;
        private System.Windows.Forms.Button OKButton;
        private System.Windows.Forms.GroupBox fraCtrls2;
    }
}