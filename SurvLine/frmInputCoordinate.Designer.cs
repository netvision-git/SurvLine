namespace SurvLine
{
    partial class frmInputCoordinate
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtNumber = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.optDMS = new System.Windows.Forms.RadioButton();
            this.lblLonS = new System.Windows.Forms.Label();
            this.lblLatS = new System.Windows.Forms.Label();
            this.txtLonS = new System.Windows.Forms.TextBox();
            this.txtLatS = new System.Windows.Forms.TextBox();
            this.lblLonM = new System.Windows.Forms.Label();
            this.lblLatM = new System.Windows.Forms.Label();
            this.txtLonM = new System.Windows.Forms.TextBox();
            this.txtLatM = new System.Windows.Forms.TextBox();
            this.lblLonH = new System.Windows.Forms.Label();
            this.lblLatH = new System.Windows.Forms.Label();
            this.txtLonH = new System.Windows.Forms.TextBox();
            this.txtLatH = new System.Windows.Forms.TextBox();
            this.lblLon = new System.Windows.Forms.Label();
            this.lblLat = new System.Windows.Forms.Label();
            this.fraDMS = new System.Windows.Forms.GroupBox();
            this.optJGD = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblYUnit = new System.Windows.Forms.Label();
            this.lblXUnit = new System.Windows.Forms.Label();
            this.txtY = new System.Windows.Forms.TextBox();
            this.txtX = new System.Windows.Forms.TextBox();
            this.lblY = new System.Windows.Forms.Label();
            this.lblX = new System.Windows.Forms.Label();
            this.cmdChange = new System.Windows.Forms.Button();
            this.Frame1 = new System.Windows.Forms.GroupBox();
            this.optAlt = new System.Windows.Forms.RadioButton();
            this.optHeight = new System.Windows.Forms.RadioButton();
            this.lblHeightUnit = new System.Windows.Forms.Label();
            this.txtHeight = new System.Windows.Forms.TextBox();
            this.CancelButton = new System.Windows.Forms.Button();
            this.OKButton = new System.Windows.Forms.Button();
            this.fraDMS.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.Frame1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(21, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "No:";
            // 
            // txtNumber
            // 
            this.txtNumber.Location = new System.Drawing.Point(54, 25);
            this.txtNumber.Name = "txtNumber";
            this.txtNumber.Size = new System.Drawing.Size(109, 19);
            this.txtNumber.TabIndex = 1;
            this.txtNumber.Text = "1234567890123456";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "名称:";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(55, 56);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(228, 19);
            this.txtName.TabIndex = 3;
            this.txtName.Text = "123456789012345678901234567890123456";
            // 
            // optDMS
            // 
            this.optDMS.AutoSize = true;
            this.optDMS.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.optDMS.Location = new System.Drawing.Point(22, 96);
            this.optDMS.Name = "optDMS";
            this.optDMS.Size = new System.Drawing.Size(71, 16);
            this.optDMS.TabIndex = 20;
            this.optDMS.TabStop = true;
            this.optDMS.Text = "緯度経度";
            this.optDMS.UseVisualStyleBackColor = true;
            this.optDMS.CheckedChanged += new System.EventHandler(this.optDMS_CheckedChanged);
            // 
            // lblLonS
            // 
            this.lblLonS.AutoSize = true;
            this.lblLonS.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblLonS.Location = new System.Drawing.Point(246, 51);
            this.lblLonS.Name = "lblLonS";
            this.lblLonS.Size = new System.Drawing.Size(17, 12);
            this.lblLonS.TabIndex = 14;
            this.lblLonS.Text = "″";
            // 
            // lblLatS
            // 
            this.lblLatS.AutoSize = true;
            this.lblLatS.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblLatS.Location = new System.Drawing.Point(246, 27);
            this.lblLatS.Name = "lblLatS";
            this.lblLatS.Size = new System.Drawing.Size(17, 12);
            this.lblLatS.TabIndex = 13;
            this.lblLatS.Text = "″";
            // 
            // txtLonS
            // 
            this.txtLonS.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtLonS.Location = new System.Drawing.Point(172, 48);
            this.txtLonS.Name = "txtLonS";
            this.txtLonS.Size = new System.Drawing.Size(72, 19);
            this.txtLonS.TabIndex = 12;
            // 
            // txtLatS
            // 
            this.txtLatS.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtLatS.Location = new System.Drawing.Point(171, 24);
            this.txtLatS.Name = "txtLatS";
            this.txtLatS.Size = new System.Drawing.Size(72, 19);
            this.txtLatS.TabIndex = 11;
            // 
            // lblLonM
            // 
            this.lblLonM.AutoSize = true;
            this.lblLonM.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblLonM.Location = new System.Drawing.Point(150, 51);
            this.lblLonM.Name = "lblLonM";
            this.lblLonM.Size = new System.Drawing.Size(17, 12);
            this.lblLonM.TabIndex = 10;
            this.lblLonM.Text = "′";
            // 
            // lblLatM
            // 
            this.lblLatM.AutoSize = true;
            this.lblLatM.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblLatM.Location = new System.Drawing.Point(151, 27);
            this.lblLatM.Name = "lblLatM";
            this.lblLatM.Size = new System.Drawing.Size(17, 12);
            this.lblLatM.TabIndex = 9;
            this.lblLatM.Text = "′";
            // 
            // txtLonM
            // 
            this.txtLonM.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtLonM.Location = new System.Drawing.Point(117, 48);
            this.txtLonM.Name = "txtLonM";
            this.txtLonM.Size = new System.Drawing.Size(31, 19);
            this.txtLonM.TabIndex = 8;
            // 
            // txtLatM
            // 
            this.txtLatM.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtLatM.Location = new System.Drawing.Point(117, 24);
            this.txtLatM.Name = "txtLatM";
            this.txtLatM.Size = new System.Drawing.Size(31, 19);
            this.txtLatM.TabIndex = 7;
            // 
            // lblLonH
            // 
            this.lblLonH.AutoSize = true;
            this.lblLonH.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblLonH.Location = new System.Drawing.Point(98, 52);
            this.lblLonH.Name = "lblLonH";
            this.lblLonH.Size = new System.Drawing.Size(17, 12);
            this.lblLonH.TabIndex = 6;
            this.lblLonH.Text = "°";
            // 
            // lblLatH
            // 
            this.lblLatH.AutoSize = true;
            this.lblLatH.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblLatH.Location = new System.Drawing.Point(98, 27);
            this.lblLatH.Name = "lblLatH";
            this.lblLatH.Size = new System.Drawing.Size(17, 12);
            this.lblLatH.TabIndex = 5;
            this.lblLatH.Text = "°";
            // 
            // txtLonH
            // 
            this.txtLonH.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtLonH.Location = new System.Drawing.Point(55, 49);
            this.txtLonH.Name = "txtLonH";
            this.txtLonH.Size = new System.Drawing.Size(40, 19);
            this.txtLonH.TabIndex = 4;
            // 
            // txtLatH
            // 
            this.txtLatH.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtLatH.Location = new System.Drawing.Point(55, 24);
            this.txtLatH.Name = "txtLatH";
            this.txtLatH.Size = new System.Drawing.Size(40, 19);
            this.txtLatH.TabIndex = 3;
            // 
            // lblLon
            // 
            this.lblLon.AutoSize = true;
            this.lblLon.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblLon.Location = new System.Drawing.Point(17, 53);
            this.lblLon.Name = "lblLon";
            this.lblLon.Size = new System.Drawing.Size(31, 12);
            this.lblLon.TabIndex = 2;
            this.lblLon.Text = "経度:";
            // 
            // lblLat
            // 
            this.lblLat.AutoSize = true;
            this.lblLat.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblLat.Location = new System.Drawing.Point(17, 28);
            this.lblLat.Name = "lblLat";
            this.lblLat.Size = new System.Drawing.Size(31, 12);
            this.lblLat.TabIndex = 1;
            this.lblLat.Text = "緯度:";
            // 
            // fraDMS
            // 
            this.fraDMS.Controls.Add(this.lblLonS);
            this.fraDMS.Controls.Add(this.lblLatS);
            this.fraDMS.Controls.Add(this.txtLonS);
            this.fraDMS.Controls.Add(this.txtLatS);
            this.fraDMS.Controls.Add(this.lblLonM);
            this.fraDMS.Controls.Add(this.lblLatM);
            this.fraDMS.Controls.Add(this.txtLonM);
            this.fraDMS.Controls.Add(this.txtLatM);
            this.fraDMS.Controls.Add(this.lblLonH);
            this.fraDMS.Controls.Add(this.lblLatH);
            this.fraDMS.Controls.Add(this.txtLonH);
            this.fraDMS.Controls.Add(this.txtLatH);
            this.fraDMS.Controls.Add(this.lblLon);
            this.fraDMS.Controls.Add(this.lblLat);
            this.fraDMS.Location = new System.Drawing.Point(21, 95);
            this.fraDMS.Name = "fraDMS";
            this.fraDMS.Size = new System.Drawing.Size(274, 85);
            this.fraDMS.TabIndex = 19;
            this.fraDMS.TabStop = false;
            // 
            // optJGD
            // 
            this.optJGD.AutoSize = true;
            this.optJGD.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.optJGD.Location = new System.Drawing.Point(24, 229);
            this.optJGD.Name = "optJGD";
            this.optJGD.Size = new System.Drawing.Size(71, 16);
            this.optJGD.TabIndex = 22;
            this.optJGD.TabStop = true;
            this.optJGD.Text = "平面直角";
            this.optJGD.UseVisualStyleBackColor = true;
            this.optJGD.CheckedChanged += new System.EventHandler(this.optJGD_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lblYUnit);
            this.groupBox2.Controls.Add(this.lblXUnit);
            this.groupBox2.Controls.Add(this.txtY);
            this.groupBox2.Controls.Add(this.txtX);
            this.groupBox2.Controls.Add(this.lblY);
            this.groupBox2.Controls.Add(this.lblX);
            this.groupBox2.Location = new System.Drawing.Point(22, 234);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(272, 87);
            this.groupBox2.TabIndex = 21;
            this.groupBox2.TabStop = false;
            // 
            // lblYUnit
            // 
            this.lblYUnit.AutoSize = true;
            this.lblYUnit.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblYUnit.Location = new System.Drawing.Point(230, 52);
            this.lblYUnit.Name = "lblYUnit";
            this.lblYUnit.Size = new System.Drawing.Size(16, 12);
            this.lblYUnit.TabIndex = 14;
            this.lblYUnit.Text = "ｍ";
            // 
            // lblXUnit
            // 
            this.lblXUnit.AutoSize = true;
            this.lblXUnit.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblXUnit.Location = new System.Drawing.Point(230, 28);
            this.lblXUnit.Name = "lblXUnit";
            this.lblXUnit.Size = new System.Drawing.Size(16, 12);
            this.lblXUnit.TabIndex = 13;
            this.lblXUnit.Text = "ｍ";
            // 
            // txtY
            // 
            this.txtY.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtY.Location = new System.Drawing.Point(41, 49);
            this.txtY.Name = "txtY";
            this.txtY.Size = new System.Drawing.Size(184, 19);
            this.txtY.TabIndex = 12;
            // 
            // txtX
            // 
            this.txtX.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtX.Location = new System.Drawing.Point(40, 24);
            this.txtX.Name = "txtX";
            this.txtX.Size = new System.Drawing.Size(185, 19);
            this.txtX.TabIndex = 11;
            // 
            // lblY
            // 
            this.lblY.AutoSize = true;
            this.lblY.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblY.Location = new System.Drawing.Point(17, 53);
            this.lblY.Name = "lblY";
            this.lblY.Size = new System.Drawing.Size(14, 12);
            this.lblY.TabIndex = 2;
            this.lblY.Text = "Y:";
            // 
            // lblX
            // 
            this.lblX.AutoSize = true;
            this.lblX.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblX.Location = new System.Drawing.Point(17, 28);
            this.lblX.Name = "lblX";
            this.lblX.Size = new System.Drawing.Size(14, 12);
            this.lblX.TabIndex = 1;
            this.lblX.Text = "X:";
            // 
            // cmdChange
            // 
            this.cmdChange.Location = new System.Drawing.Point(106, 190);
            this.cmdChange.Name = "cmdChange";
            this.cmdChange.Size = new System.Drawing.Size(83, 32);
            this.cmdChange.TabIndex = 23;
            this.cmdChange.Text = "↓";
            this.cmdChange.UseVisualStyleBackColor = true;
            this.cmdChange.Click += new System.EventHandler(this.cmdChange_Click);
            // 
            // Frame1
            // 
            this.Frame1.Controls.Add(this.optAlt);
            this.Frame1.Controls.Add(this.optHeight);
            this.Frame1.Controls.Add(this.lblHeightUnit);
            this.Frame1.Controls.Add(this.txtHeight);
            this.Frame1.Location = new System.Drawing.Point(32, 333);
            this.Frame1.Name = "Frame1";
            this.Frame1.Size = new System.Drawing.Size(216, 61);
            this.Frame1.TabIndex = 24;
            this.Frame1.TabStop = false;
            // 
            // optAlt
            // 
            this.optAlt.AutoSize = true;
            this.optAlt.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.optAlt.Location = new System.Drawing.Point(1, 33);
            this.optAlt.Name = "optAlt";
            this.optAlt.Size = new System.Drawing.Size(47, 16);
            this.optAlt.TabIndex = 15;
            this.optAlt.TabStop = true;
            this.optAlt.Text = "標高";
            this.optAlt.UseVisualStyleBackColor = true;
            this.optAlt.CheckedChanged += new System.EventHandler(this.optAlt_CheckedChanged);
            // 
            // optHeight
            // 
            this.optHeight.AutoSize = true;
            this.optHeight.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.optHeight.Location = new System.Drawing.Point(1, 15);
            this.optHeight.Name = "optHeight";
            this.optHeight.Size = new System.Drawing.Size(71, 16);
            this.optHeight.TabIndex = 14;
            this.optHeight.TabStop = true;
            this.optHeight.Text = "楕円体高";
            this.optHeight.UseVisualStyleBackColor = true;
            this.optHeight.CheckedChanged += new System.EventHandler(this.optHeight_CheckedChanged);
            // 
            // lblHeightUnit
            // 
            this.lblHeightUnit.AutoSize = true;
            this.lblHeightUnit.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblHeightUnit.Location = new System.Drawing.Point(184, 28);
            this.lblHeightUnit.Name = "lblHeightUnit";
            this.lblHeightUnit.Size = new System.Drawing.Size(16, 12);
            this.lblHeightUnit.TabIndex = 13;
            this.lblHeightUnit.Text = "ｍ";
            // 
            // txtHeight
            // 
            this.txtHeight.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtHeight.Location = new System.Drawing.Point(82, 23);
            this.txtHeight.Name = "txtHeight";
            this.txtHeight.Size = new System.Drawing.Size(97, 19);
            this.txtHeight.TabIndex = 11;
            // 
            // CancelButton
            // 
            this.CancelButton.Location = new System.Drawing.Point(199, 424);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(94, 22);
            this.CancelButton.TabIndex = 26;
            this.CancelButton.Text = "キャンセル";
            this.CancelButton.UseVisualStyleBackColor = true;
            this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // OKButton
            // 
            this.OKButton.Location = new System.Drawing.Point(86, 423);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(95, 25);
            this.OKButton.TabIndex = 25;
            this.OKButton.Text = "OK";
            this.OKButton.UseVisualStyleBackColor = true;
            this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
            // 
            // frmInputCoordinate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(314, 470);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.OKButton);
            this.Controls.Add(this.Frame1);
            this.Controls.Add(this.cmdChange);
            this.Controls.Add(this.optJGD);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.optDMS);
            this.Controls.Add(this.fraDMS);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtNumber);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Name = "frmInputCoordinate";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "方位標入力";
            this.fraDMS.ResumeLayout(false);
            this.fraDMS.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.Frame1.ResumeLayout(false);
            this.Frame1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtNumber;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.RadioButton optDMS;
        private System.Windows.Forms.Label lblLonS;
        private System.Windows.Forms.Label lblLatS;
        private System.Windows.Forms.TextBox txtLonS;
        private System.Windows.Forms.TextBox txtLatS;
        private System.Windows.Forms.Label lblLonM;
        private System.Windows.Forms.Label lblLatM;
        private System.Windows.Forms.TextBox txtLonM;
        private System.Windows.Forms.TextBox txtLatM;
        private System.Windows.Forms.Label lblLonH;
        private System.Windows.Forms.Label lblLatH;
        private System.Windows.Forms.TextBox txtLonH;
        private System.Windows.Forms.TextBox txtLatH;
        private System.Windows.Forms.Label lblLon;
        private System.Windows.Forms.Label lblLat;
        private System.Windows.Forms.GroupBox fraDMS;
        private System.Windows.Forms.RadioButton optJGD;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lblYUnit;
        private System.Windows.Forms.Label lblXUnit;
        private System.Windows.Forms.TextBox txtY;
        private System.Windows.Forms.TextBox txtX;
        private System.Windows.Forms.Label lblY;
        private System.Windows.Forms.Label lblX;
        private System.Windows.Forms.Button cmdChange;
        private System.Windows.Forms.GroupBox Frame1;
        private System.Windows.Forms.RadioButton optAlt;
        private System.Windows.Forms.RadioButton optHeight;
        private System.Windows.Forms.Label lblHeightUnit;
        private System.Windows.Forms.TextBox txtHeight;
        internal System.Windows.Forms.Button CancelButton;
        internal System.Windows.Forms.Button OKButton;
    }
}