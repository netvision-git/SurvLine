namespace SurvLine
{
    partial class frmJobSelect
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
            this.lvProject = new System.Windows.Forms.ListView();
            this.CancelButton = new System.Windows.Forms.Button();
            this.OKButton = new System.Windows.Forms.Button();
            this.cmdUnselect = new System.Windows.Forms.Button();
            this.cmdSelect = new System.Windows.Forms.Button();
            this.lblDescription = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lvProject
            // 
            this.lvProject.FullRowSelect = true;
            this.lvProject.HideSelection = false;
            this.lvProject.Location = new System.Drawing.Point(43, 28);
            this.lvProject.Name = "lvProject";
            this.lvProject.Size = new System.Drawing.Size(778, 288);
            this.lvProject.TabIndex = 1;
            this.lvProject.UseCompatibleStateImageBehavior = false;
            // 
            // CancelButton
            // 
            this.CancelButton.Location = new System.Drawing.Point(727, 346);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(94, 22);
            this.CancelButton.TabIndex = 4;
            this.CancelButton.Text = "キャンセル";
            this.CancelButton.UseVisualStyleBackColor = true;
            this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // OKButton
            // 
            this.OKButton.Location = new System.Drawing.Point(595, 345);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(95, 25);
            this.OKButton.TabIndex = 3;
            this.OKButton.Text = "OK";
            this.OKButton.UseVisualStyleBackColor = true;
            this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
            // 
            // cmdUnselect
            // 
            this.cmdUnselect.Location = new System.Drawing.Point(465, 345);
            this.cmdUnselect.Name = "cmdUnselect";
            this.cmdUnselect.Size = new System.Drawing.Size(95, 25);
            this.cmdUnselect.TabIndex = 5;
            this.cmdUnselect.Text = "全解除";
            this.cmdUnselect.UseVisualStyleBackColor = true;
            this.cmdUnselect.Click += new System.EventHandler(this.cmdUnselect_Click);
            // 
            // cmdSelect
            // 
            this.cmdSelect.Location = new System.Drawing.Point(335, 346);
            this.cmdSelect.Name = "cmdSelect";
            this.cmdSelect.Size = new System.Drawing.Size(95, 25);
            this.cmdSelect.TabIndex = 6;
            this.cmdSelect.Text = "全選択";
            this.cmdSelect.UseVisualStyleBackColor = true;
            this.cmdSelect.Click += new System.EventHandler(this.cmdSelect_Click);
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblDescription.Location = new System.Drawing.Point(47, 8);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(129, 12);
            this.lblDescription.TabIndex = 7;
            this.lblDescription.Text = "現場を選択してください。";
            // 
            // frmJobSelect2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(869, 393);
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.cmdSelect);
            this.Controls.Add(this.cmdUnselect);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.OKButton);
            this.Controls.Add(this.lvProject);
            this.Name = "frmJobSelect2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "現場を選択";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.ListView lvProject;
        internal System.Windows.Forms.Button CancelButton;
        internal System.Windows.Forms.Button OKButton;
        internal System.Windows.Forms.Button cmdUnselect;
        internal System.Windows.Forms.Button cmdSelect;
        internal System.Windows.Forms.Label lblDescription;
    }
}