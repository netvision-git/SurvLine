namespace SurvLine
{
    partial class ListPane
    {
        /// <summary> 
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region コンポーネント デザイナーで生成されたコード

        /// <summary> 
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を 
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.objTab = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.grdFlexGrid1 = new System.Windows.Forms.ListView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.grdFlexGrid2 = new System.Windows.Forms.ListView();
            this.picEdgeMask = new System.Windows.Forms.PictureBox();
            this.objTab.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picEdgeMask)).BeginInit();
            this.SuspendLayout();
            // 
            // objTab
            // 
            this.objTab.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.objTab.Controls.Add(this.tabPage1);
            this.objTab.Controls.Add(this.tabPage2);
            this.objTab.Location = new System.Drawing.Point(31, 31);
            this.objTab.Name = "objTab";
            this.objTab.SelectedIndex = 0;
            this.objTab.Size = new System.Drawing.Size(600, 286);
            this.objTab.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.grdFlexGrid1);
            this.tabPage1.Location = new System.Drawing.Point(4, 4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(592, 260);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "観測点";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.PeachPuff;
            this.label2.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.Location = new System.Drawing.Point(6, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(360, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "坂井様＜暫定＞ユーザーコントロールを作成しましたが、";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.PeachPuff;
            this.label1.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.Location = new System.Drawing.Point(363, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(258, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "グリッドが無かった為,ListVewにしまいｓた";
            // 
            // grdFlexGrid1
            // 
            this.grdFlexGrid1.CheckBoxes = true;
            this.grdFlexGrid1.HideSelection = false;
            this.grdFlexGrid1.Location = new System.Drawing.Point(23, 29);
            this.grdFlexGrid1.Name = "grdFlexGrid1";
            this.grdFlexGrid1.Size = new System.Drawing.Size(275, 206);
            this.grdFlexGrid1.TabIndex = 0;
            this.grdFlexGrid1.UseCompatibleStateImageBehavior = false;
            this.grdFlexGrid1.SelectedIndexChanged += new System.EventHandler(this.grdFlexGrid1_SelectedIndexChanged);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.grdFlexGrid2);
            this.tabPage2.Location = new System.Drawing.Point(4, 4);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(592, 260);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "ベクトル";
            this.tabPage2.UseVisualStyleBackColor = true;
            this.tabPage2.Click += new System.EventHandler(this.tabPage2_Click);
            // 
            // grdFlexGrid2
            // 
            this.grdFlexGrid2.HideSelection = false;
            this.grdFlexGrid2.Location = new System.Drawing.Point(21, 27);
            this.grdFlexGrid2.Name = "grdFlexGrid2";
            this.grdFlexGrid2.Size = new System.Drawing.Size(275, 206);
            this.grdFlexGrid2.TabIndex = 1;
            this.grdFlexGrid2.UseCompatibleStateImageBehavior = false;
            // 
            // picEdgeMask
            // 
            this.picEdgeMask.Location = new System.Drawing.Point(12, 191);
            this.picEdgeMask.Name = "picEdgeMask";
            this.picEdgeMask.Size = new System.Drawing.Size(171, 47);
            this.picEdgeMask.TabIndex = 1;
            this.picEdgeMask.TabStop = false;
            // 
            // ListPane
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.picEdgeMask);
            this.Controls.Add(this.objTab);
            this.Name = "ListPane";
            this.Size = new System.Drawing.Size(800, 400);
            this.Load += new System.EventHandler(this.ListPane_Load);
            this.objTab.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picEdgeMask)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.PictureBox picEdgeMask;
        internal System.Windows.Forms.TabControl objTab;
        internal System.Windows.Forms.ListView grdFlexGrid1;
        internal System.Windows.Forms.ListView grdFlexGrid2;
        internal System.Windows.Forms.Label label1;
        internal System.Windows.Forms.Label label2;
        internal System.Windows.Forms.TabPage tabPage1;
    }
}
