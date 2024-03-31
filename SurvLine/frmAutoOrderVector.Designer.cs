namespace SurvLine
{
    partial class frmAutoOrderVector
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
            this.optOne = new System.Windows.Forms.RadioButton();
            this.optAll = new System.Windows.Forms.RadioButton();
            this.cmbFixed = new System.Windows.Forms.ComboBox();
            this.CancelButton = new System.Windows.Forms.Button();
            this.OKButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(32, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(196, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "始点とする固定点を選択してください。";
            // 
            // optOne
            // 
            this.optOne.AutoSize = true;
            this.optOne.Location = new System.Drawing.Point(28, 80);
            this.optOne.Name = "optOne";
            this.optOne.Size = new System.Drawing.Size(59, 16);
            this.optOne.TabIndex = 1;
            this.optOne.TabStop = true;
            this.optOne.Text = "固定点";
            this.optOne.UseVisualStyleBackColor = true;
            // 
            // optAll
            // 
            this.optAll.AutoSize = true;
            this.optAll.Location = new System.Drawing.Point(28, 116);
            this.optAll.Name = "optAll";
            this.optAll.Size = new System.Drawing.Size(169, 16);
            this.optAll.TabIndex = 2;
            this.optAll.TabStop = true;
            this.optAll.Text = "全ての固定点を始点にする。";
            this.optAll.UseVisualStyleBackColor = true;
            // 
            // cmbFixed
            // 
            this.cmbFixed.FormattingEnabled = true;
            this.cmbFixed.Location = new System.Drawing.Point(102, 78);
            this.cmbFixed.Name = "cmbFixed";
            this.cmbFixed.Size = new System.Drawing.Size(135, 20);
            this.cmbFixed.TabIndex = 3;
            this.cmbFixed.Text = "cmbFixed";
            this.cmbFixed.SelectedIndexChanged += new System.EventHandler(this.cmbFixed_SelectedIndexChanged);
            // 
            // CancelButton
            // 
            this.CancelButton.Location = new System.Drawing.Point(163, 174);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(94, 22);
            this.CancelButton.TabIndex = 8;
            this.CancelButton.Text = "キャンセル";
            this.CancelButton.UseVisualStyleBackColor = true;
            this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // OKButton
            // 
            this.OKButton.Location = new System.Drawing.Point(46, 173);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(95, 25);
            this.OKButton.TabIndex = 7;
            this.OKButton.Text = "OK";
            this.OKButton.UseVisualStyleBackColor = true;
            this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
            // 
            // frmAutoOrderVector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 231);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.OKButton);
            this.Controls.Add(this.cmbFixed);
            this.Controls.Add(this.optAll);
            this.Controls.Add(this.optOne);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Name = "frmAutoOrderVector";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "基線ベクトルの向きの自動整列";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton optOne;
        private System.Windows.Forms.RadioButton optAll;
        private System.Windows.Forms.ComboBox cmbFixed;
        internal System.Windows.Forms.Button CancelButton;
        internal System.Windows.Forms.Button OKButton;
    }
}