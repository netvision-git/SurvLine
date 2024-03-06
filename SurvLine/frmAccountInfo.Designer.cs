namespace SurvLine
{
    partial class frmAccountInfo
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
            this.fraCtrls3 = new System.Windows.Forms.GroupBox();
            this.chkAutomation = new System.Windows.Forms.CheckBox();
            this.lblDescription = new System.Windows.Forms.Label();
            this.lvObject = new System.Windows.Forms.ListView();
            this.fraCtrls1 = new System.Windows.Forms.GroupBox();
            this.optObject = new System.Windows.Forms.RadioButton();
            this.optSession = new System.Windows.Forms.RadioButton();
            this.optAll = new System.Windows.Forms.RadioButton();
            this.cmbSession = new System.Windows.Forms.ComboBox();
            this.fraCtrls2 = new System.Windows.Forms.GroupBox();
            this.CancelButton = new System.Windows.Forms.Button();
            this.OKButton = new System.Windows.Forms.Button();
            this.cmdUnselect = new System.Windows.Forms.Button();
            this.cmdSelect = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.fraCtrls3.SuspendLayout();
            this.fraCtrls1.SuspendLayout();
            this.fraCtrls2.SuspendLayout();
            this.SuspendLayout();
            // 
            // fraCtrls3
            // 
            this.fraCtrls3.Controls.Add(this.chkAutomation);
            this.fraCtrls3.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.fraCtrls3.Location = new System.Drawing.Point(36, 351);
            this.fraCtrls3.Name = "fraCtrls3";
            this.fraCtrls3.Size = new System.Drawing.Size(238, 43);
            this.fraCtrls3.TabIndex = 0;
            this.fraCtrls3.TabStop = false;
            // 
            // chkAutomation
            // 
            this.chkAutomation.AutoSize = true;
            this.chkAutomation.Location = new System.Drawing.Point(12, 6);
            this.chkAutomation.Name = "chkAutomation";
            this.chkAutomation.Size = new System.Drawing.Size(212, 16);
            this.chkAutomation.TabIndex = 0;
            this.chkAutomation.Text = "すぐにNS-Networkでインポートする。";
            this.chkAutomation.UseVisualStyleBackColor = true;
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblDescription.Location = new System.Drawing.Point(35, 18);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(163, 12);
            this.lblDescription.TabIndex = 1;
            this.lblDescription.Text = "出力する%sを選択してください。";
            // 
            // lvObject
            // 
            this.lvObject.CheckBoxes = true;
            this.lvObject.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lvObject.HideSelection = false;
            this.lvObject.Location = new System.Drawing.Point(37, 123);
            this.lvObject.Name = "lvObject";
            this.lvObject.Size = new System.Drawing.Size(571, 208);
            this.lvObject.TabIndex = 2;
            this.lvObject.UseCompatibleStateImageBehavior = false;
            this.lvObject.View = System.Windows.Forms.View.Details;
            // 
            // fraCtrls1
            // 
            this.fraCtrls1.Controls.Add(this.optObject);
            this.fraCtrls1.Controls.Add(this.optSession);
            this.fraCtrls1.Controls.Add(this.optAll);
            this.fraCtrls1.Controls.Add(this.cmbSession);
            this.fraCtrls1.Location = new System.Drawing.Point(33, 31);
            this.fraCtrls1.Name = "fraCtrls1";
            this.fraCtrls1.Size = new System.Drawing.Size(253, 81);
            this.fraCtrls1.TabIndex = 4;
            this.fraCtrls1.TabStop = false;
            // 
            // optObject
            // 
            this.optObject.AutoSize = true;
            this.optObject.Location = new System.Drawing.Point(16, 56);
            this.optObject.Name = "optObject";
            this.optObject.Size = new System.Drawing.Size(83, 16);
            this.optObject.TabIndex = 6;
            this.optObject.TabStop = true;
            this.optObject.Text = "オブジェクト";
            this.optObject.UseVisualStyleBackColor = true;
            // 
            // optSession
            // 
            this.optSession.AutoSize = true;
            this.optSession.Location = new System.Drawing.Point(16, 36);
            this.optSession.Name = "optSession";
            this.optSession.Size = new System.Drawing.Size(72, 16);
            this.optSession.TabIndex = 5;
            this.optSession.TabStop = true;
            this.optSession.Text = "セッション";
            this.optSession.UseVisualStyleBackColor = true;
            // 
            // optAll
            // 
            this.optAll.AutoSize = true;
            this.optAll.Location = new System.Drawing.Point(16, 15);
            this.optAll.Name = "optAll";
            this.optAll.Size = new System.Drawing.Size(71, 16);
            this.optAll.TabIndex = 4;
            this.optAll.TabStop = true;
            this.optAll.Text = "現場全体";
            this.optAll.UseVisualStyleBackColor = true;
            this.optAll.CheckedChanged += new System.EventHandler(this.optAll_CheckedChanged_1);
            // 
            // cmbSession
            // 
            this.cmbSession.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.cmbSession.FormattingEnabled = true;
            this.cmbSession.Location = new System.Drawing.Point(125, 36);
            this.cmbSession.Name = "cmbSession";
            this.cmbSession.Size = new System.Drawing.Size(115, 20);
            this.cmbSession.TabIndex = 3;
            this.cmbSession.SelectedIndexChanged += new System.EventHandler(this.cmbSession_SelectedIndexChanged);
            // 
            // fraCtrls2
            // 
            this.fraCtrls2.Controls.Add(this.CancelButton);
            this.fraCtrls2.Controls.Add(this.OKButton);
            this.fraCtrls2.Location = new System.Drawing.Point(362, 388);
            this.fraCtrls2.Name = "fraCtrls2";
            this.fraCtrls2.Size = new System.Drawing.Size(246, 54);
            this.fraCtrls2.TabIndex = 5;
            this.fraCtrls2.TabStop = false;
            // 
            // CancelButton
            // 
            this.CancelButton.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.CancelButton.Location = new System.Drawing.Point(145, 13);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(95, 30);
            this.CancelButton.TabIndex = 1;
            this.CancelButton.Text = "ｷｬﾝｾﾙ";
            this.CancelButton.UseVisualStyleBackColor = true;
            this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // OKButton
            // 
            this.OKButton.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.OKButton.Location = new System.Drawing.Point(6, 14);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(95, 30);
            this.OKButton.TabIndex = 0;
            this.OKButton.Text = "OK";
            this.OKButton.UseVisualStyleBackColor = true;
            this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
            // 
            // cmdUnselect
            // 
            this.cmdUnselect.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.cmdUnselect.Location = new System.Drawing.Point(158, 403);
            this.cmdUnselect.Name = "cmdUnselect";
            this.cmdUnselect.Size = new System.Drawing.Size(95, 30);
            this.cmdUnselect.TabIndex = 7;
            this.cmdUnselect.Text = "全解除";
            this.cmdUnselect.UseVisualStyleBackColor = true;
            this.cmdUnselect.Click += new System.EventHandler(this.cmdUnselect_Click);
            // 
            // cmdSelect
            // 
            this.cmdSelect.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.cmdSelect.Location = new System.Drawing.Point(47, 403);
            this.cmdSelect.Name = "cmdSelect";
            this.cmdSelect.Size = new System.Drawing.Size(95, 30);
            this.cmdSelect.TabIndex = 8;
            this.cmdSelect.Text = "全選択";
            this.cmdSelect.UseVisualStyleBackColor = true;
            this.cmdSelect.Click += new System.EventHandler(this.cmdSelect_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(480, 12);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(80, 16);
            this.checkBox1.TabIndex = 9;
            this.checkBox1.Text = "checkBox1";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(446, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(114, 31);
            this.label1.TabIndex = 10;
            // 
            // frmAccountInfo2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(647, 450);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.cmdSelect);
            this.Controls.Add(this.cmdUnselect);
            this.Controls.Add(this.fraCtrls2);
            this.Controls.Add(this.fraCtrls1);
            this.Controls.Add(this.lvObject);
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.fraCtrls3);
            this.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Name = "frmAccountInfo2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmAccountInfo";
            this.Load += new System.EventHandler(this.frmAccountInfo2_Load);
            this.fraCtrls3.ResumeLayout(false);
            this.fraCtrls3.PerformLayout();
            this.fraCtrls1.ResumeLayout(false);
            this.fraCtrls1.PerformLayout();
            this.fraCtrls2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox fraCtrls3;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.GroupBox fraCtrls1;
        private System.Windows.Forms.CheckBox chkAutomation;
        private System.Windows.Forms.ComboBox cmbSession;
        private System.Windows.Forms.GroupBox fraCtrls2;
        private System.Windows.Forms.Button OKButton;
        private System.Windows.Forms.Button cmdUnselect;
        private System.Windows.Forms.Button cmdSelect;
        private System.Windows.Forms.Button CancelButton;
        private System.Windows.Forms.RadioButton optObject;
        private System.Windows.Forms.RadioButton optSession;
        private System.Windows.Forms.RadioButton optAll;
        private System.Windows.Forms.CheckBox checkBox1;
        internal System.Windows.Forms.ListView lvObject;
        private System.Windows.Forms.Label label1;
    }
}