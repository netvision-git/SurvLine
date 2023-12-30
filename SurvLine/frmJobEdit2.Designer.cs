namespace SurvLine
{
    partial class frmJobEdit2
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
            this.label2 = new System.Windows.Forms.Label();
            this.txtJobName = new System.Windows.Forms.TextBox();
            this.txtDistrictName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lblFolder = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbZone = new System.Windows.Forms.ComboBox();
            this.chkGeoidoEnable = new System.Windows.Forms.CheckBox();
            this.chkSemiDynaEnable = new System.Windows.Forms.CheckBox();
            this.txtGeoidoPath = new System.Windows.Forms.TextBox();
            this.txtSemiDynaPath = new System.Windows.Forms.TextBox();
            this.cmdRefGeoido = new System.Windows.Forms.Button();
            this.cmdRefSemiDyna = new System.Windows.Forms.Button();
            this.OKButton = new System.Windows.Forms.Button();
            this.CancelButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.Location = new System.Drawing.Point(32, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "現場名:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(32, 79);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "地区名:";
            // 
            // txtJobName
            // 
            this.txtJobName.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtJobName.Location = new System.Drawing.Point(93, 35);
            this.txtJobName.Name = "txtJobName";
            this.txtJobName.Size = new System.Drawing.Size(231, 19);
            this.txtJobName.TabIndex = 2;
            // 
            // txtDistrictName
            // 
            this.txtDistrictName.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtDistrictName.Location = new System.Drawing.Point(93, 76);
            this.txtDistrictName.Name = "txtDistrictName";
            this.txtDistrictName.Size = new System.Drawing.Size(231, 19);
            this.txtDistrictName.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(32, 113);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(50, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "フォルダ:";
            // 
            // lblFolder
            // 
            this.lblFolder.AutoSize = true;
            this.lblFolder.Location = new System.Drawing.Point(117, 113);
            this.lblFolder.Name = "lblFolder";
            this.lblFolder.Size = new System.Drawing.Size(29, 12);
            this.lblFolder.TabIndex = 5;
            this.lblFolder.Text = "0001";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(32, 150);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(91, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "平面直角座標系:";
            // 
            // cmbZone
            // 
            this.cmbZone.FormattingEnabled = true;
            this.cmbZone.Items.AddRange(new object[] {
            "１　系　330000.　1293000.",
            "２　系　330000.　1310000.",
            "３　系　360000.　1321000.",
            "４　系　330000.　1333000.",
            "５　系　360000.　1342000.",
            "６　系　360000.　1360000.",
            "７　系　360000.　1371000.",
            "８　系　360000.　1383000.",
            "９　系　360000.　1395000.",
            "１０系　400000.　1405000.",
            "１１系　440000.　1401500.",
            "１２系　440000.　1421500.",
            "１３系　440000.　1441500.",
            "１４系　260000.　1420000.",
            "１５系　260000.　1273000.",
            "１６系　260000.　1240000.",
            "１７系　260000.　1310000.",
            "１８系　200000.　1360000.",
            "１９系　260000.　1540000."});
            this.cmbZone.Location = new System.Drawing.Point(35, 168);
            this.cmbZone.Name = "cmbZone";
            this.cmbZone.Size = new System.Drawing.Size(355, 20);
            this.cmbZone.TabIndex = 7;
            // 
            // chkGeoidoEnable
            // 
            this.chkGeoidoEnable.AutoSize = true;
            this.chkGeoidoEnable.Location = new System.Drawing.Point(35, 210);
            this.chkGeoidoEnable.Name = "chkGeoidoEnable";
            this.chkGeoidoEnable.Size = new System.Drawing.Size(110, 16);
            this.chkGeoidoEnable.TabIndex = 8;
            this.chkGeoidoEnable.Text = "ジオイド補正する";
            this.chkGeoidoEnable.UseVisualStyleBackColor = true;
            // 
            // chkSemiDynaEnable
            // 
            this.chkSemiDynaEnable.AutoSize = true;
            this.chkSemiDynaEnable.Location = new System.Drawing.Point(35, 278);
            this.chkSemiDynaEnable.Name = "chkSemiDynaEnable";
            this.chkSemiDynaEnable.Size = new System.Drawing.Size(154, 16);
            this.chkSemiDynaEnable.TabIndex = 10;
            this.chkSemiDynaEnable.Text = "セミ・ダイナミック補正する";
            this.chkSemiDynaEnable.UseVisualStyleBackColor = true;
            // 
            // txtGeoidoPath
            // 
            this.txtGeoidoPath.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtGeoidoPath.Location = new System.Drawing.Point(35, 234);
            this.txtGeoidoPath.Name = "txtGeoidoPath";
            this.txtGeoidoPath.Size = new System.Drawing.Size(274, 19);
            this.txtGeoidoPath.TabIndex = 11;
            // 
            // txtSemiDynaPath
            // 
            this.txtSemiDynaPath.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtSemiDynaPath.Location = new System.Drawing.Point(35, 303);
            this.txtSemiDynaPath.Name = "txtSemiDynaPath";
            this.txtSemiDynaPath.Size = new System.Drawing.Size(274, 19);
            this.txtSemiDynaPath.TabIndex = 12;
            // 
            // cmdRefGeoido
            // 
            this.cmdRefGeoido.Location = new System.Drawing.Point(326, 230);
            this.cmdRefGeoido.Name = "cmdRefGeoido";
            this.cmdRefGeoido.Size = new System.Drawing.Size(61, 25);
            this.cmdRefGeoido.TabIndex = 13;
            this.cmdRefGeoido.Text = "参照";
            this.cmdRefGeoido.UseVisualStyleBackColor = true;
            // 
            // cmdRefSemiDyna
            // 
            this.cmdRefSemiDyna.Location = new System.Drawing.Point(329, 302);
            this.cmdRefSemiDyna.Name = "cmdRefSemiDyna";
            this.cmdRefSemiDyna.Size = new System.Drawing.Size(57, 24);
            this.cmdRefSemiDyna.TabIndex = 14;
            this.cmdRefSemiDyna.Text = "参照";
            this.cmdRefSemiDyna.UseVisualStyleBackColor = true;
            // 
            // OKButton
            // 
            this.OKButton.Location = new System.Drawing.Point(182, 361);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(79, 32);
            this.OKButton.TabIndex = 15;
            this.OKButton.Text = "OK";
            this.OKButton.UseVisualStyleBackColor = true;
            this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
            // 
            // CancelButton
            // 
            this.CancelButton.Location = new System.Drawing.Point(301, 360);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(86, 33);
            this.CancelButton.TabIndex = 16;
            this.CancelButton.Text = "キャンセル";
            this.CancelButton.UseVisualStyleBackColor = true;
            this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // frmJobEdit2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(428, 431);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.OKButton);
            this.Controls.Add(this.cmdRefSemiDyna);
            this.Controls.Add(this.cmdRefGeoido);
            this.Controls.Add(this.txtSemiDynaPath);
            this.Controls.Add(this.txtGeoidoPath);
            this.Controls.Add(this.chkSemiDynaEnable);
            this.Controls.Add(this.chkGeoidoEnable);
            this.Controls.Add(this.cmbZone);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lblFolder);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtDistrictName);
            this.Controls.Add(this.txtJobName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Name = "frmJobEdit2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "現場の編集";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button cmdRefGeoido;
        private System.Windows.Forms.Button cmdRefSemiDyna;
        private System.Windows.Forms.Button OKButton;
        private System.Windows.Forms.Button CancelButton;
        internal System.Windows.Forms.Label lblFolder;
        internal System.Windows.Forms.TextBox txtSemiDynaPath;
        internal System.Windows.Forms.TextBox txtJobName;
        internal System.Windows.Forms.TextBox txtDistrictName;
        internal System.Windows.Forms.ComboBox cmbZone;
        internal System.Windows.Forms.CheckBox chkGeoidoEnable;
        internal System.Windows.Forms.CheckBox chkSemiDynaEnable;
        internal System.Windows.Forms.TextBox txtGeoidoPath;
    }
}