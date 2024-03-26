namespace SurvLine
{
    partial class frmAttribute
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
            this.tsTab = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.fraTab0 = new System.Windows.Forms.GroupBox();
            this.fraCtrls1 = new System.Windows.Forms.GroupBox();
            this.cmbNumberOfMinSV = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.optNumberOfMinSVStatic = new System.Windows.Forms.RadioButton();
            this.optNumberOfMinSVObsData = new System.Windows.Forms.RadioButton();
            this.label5 = new System.Windows.Forms.Label();
            this.fraCtrls0 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.udElevationMask = new System.Windows.Forms.VScrollBar();
            this.txtElevationMask = new System.Windows.Forms.TextBox();
            this.optElevationMaskInput = new System.Windows.Forms.RadioButton();
            this.optElevationMaskObsData = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbSession = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.fraTab1 = new System.Windows.Forms.GroupBox();
            this.txtRecNumber = new System.Windows.Forms.TextBox();
            this.cmbRecType = new System.Windows.Forms.ComboBox();
            this.txtRecName = new System.Windows.Forms.TextBox();
            this.cmbRecManufacturer = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.optRecName = new System.Windows.Forms.RadioButton();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.optRecList = new System.Windows.Forms.RadioButton();
            this.label10 = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.fraTab2 = new System.Windows.Forms.GroupBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.cmbAntMeasurement = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtAntNumber = new System.Windows.Forms.TextBox();
            this.cmbAntType = new System.Windows.Forms.ComboBox();
            this.txtAntHeight = new System.Windows.Forms.TextBox();
            this.cmbAntManufacturer = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.CancelButton = new System.Windows.Forms.Button();
            this.OKButton = new System.Windows.Forms.Button();
            this.tsTab.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.fraTab0.SuspendLayout();
            this.fraCtrls1.SuspendLayout();
            this.fraCtrls0.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.fraTab1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.fraTab2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tsTab
            // 
            this.tsTab.Controls.Add(this.tabPage1);
            this.tsTab.Controls.Add(this.tabPage2);
            this.tsTab.Controls.Add(this.tabPage3);
            this.tsTab.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.tsTab.Location = new System.Drawing.Point(20, 12);
            this.tsTab.Name = "tsTab";
            this.tsTab.SelectedIndex = 0;
            this.tsTab.Size = new System.Drawing.Size(505, 241);
            this.tsTab.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.fraTab0);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(497, 215);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "観測";
            this.tabPage1.UseVisualStyleBackColor = true;
            this.tabPage1.Click += new System.EventHandler(this.tabPage1_Click);
            // 
            // fraTab0
            // 
            this.fraTab0.Controls.Add(this.fraCtrls1);
            this.fraTab0.Controls.Add(this.fraCtrls0);
            this.fraTab0.Controls.Add(this.cmbSession);
            this.fraTab0.Controls.Add(this.label1);
            this.fraTab0.Location = new System.Drawing.Point(22, 23);
            this.fraTab0.Name = "fraTab0";
            this.fraTab0.Size = new System.Drawing.Size(446, 159);
            this.fraTab0.TabIndex = 0;
            this.fraTab0.TabStop = false;
            // 
            // fraCtrls1
            // 
            this.fraCtrls1.Controls.Add(this.cmbNumberOfMinSV);
            this.fraCtrls1.Controls.Add(this.label4);
            this.fraCtrls1.Controls.Add(this.optNumberOfMinSVStatic);
            this.fraCtrls1.Controls.Add(this.optNumberOfMinSVObsData);
            this.fraCtrls1.Controls.Add(this.label5);
            this.fraCtrls1.Location = new System.Drawing.Point(7, 85);
            this.fraCtrls1.Name = "fraCtrls1";
            this.fraCtrls1.Size = new System.Drawing.Size(420, 35);
            this.fraCtrls1.TabIndex = 3;
            this.fraCtrls1.TabStop = false;
            // 
            // cmbNumberOfMinSV
            // 
            this.cmbNumberOfMinSV.FormattingEnabled = true;
            this.cmbNumberOfMinSV.Location = new System.Drawing.Point(326, 0);
            this.cmbNumberOfMinSV.Name = "cmbNumberOfMinSV";
            this.cmbNumberOfMinSV.Size = new System.Drawing.Size(50, 20);
            this.cmbNumberOfMinSV.TabIndex = 7;
            this.cmbNumberOfMinSV.Text = "cmbNumberOfMinSV";
            this.cmbNumberOfMinSV.SelectedIndexChanged += new System.EventHandler(this.cmbNumberOfMinSV_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(384, 5);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(17, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "個";
            // 
            // optNumberOfMinSVStatic
            // 
            this.optNumberOfMinSVStatic.AutoSize = true;
            this.optNumberOfMinSVStatic.Location = new System.Drawing.Point(307, 3);
            this.optNumberOfMinSVStatic.Name = "optNumberOfMinSVStatic";
            this.optNumberOfMinSVStatic.Size = new System.Drawing.Size(14, 13);
            this.optNumberOfMinSVStatic.TabIndex = 3;
            this.optNumberOfMinSVStatic.TabStop = true;
            this.optNumberOfMinSVStatic.UseVisualStyleBackColor = true;
            // 
            // optNumberOfMinSVObsData
            // 
            this.optNumberOfMinSVObsData.AutoSize = true;
            this.optNumberOfMinSVObsData.Location = new System.Drawing.Point(176, 3);
            this.optNumberOfMinSVObsData.Name = "optNumberOfMinSVObsData";
            this.optNumberOfMinSVObsData.Size = new System.Drawing.Size(129, 16);
            this.optNumberOfMinSVObsData.TabIndex = 2;
            this.optNumberOfMinSVObsData.TabStop = true;
            this.optNumberOfMinSVObsData.Text = "オプション設定に従う";
            this.optNumberOfMinSVObsData.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(0, 5);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(148, 12);
            this.label5.TabIndex = 1;
            this.label5.Text = "手簿に出力する最少衛星数:";
            // 
            // fraCtrls0
            // 
            this.fraCtrls0.Controls.Add(this.label3);
            this.fraCtrls0.Controls.Add(this.udElevationMask);
            this.fraCtrls0.Controls.Add(this.txtElevationMask);
            this.fraCtrls0.Controls.Add(this.optElevationMaskInput);
            this.fraCtrls0.Controls.Add(this.optElevationMaskObsData);
            this.fraCtrls0.Controls.Add(this.label2);
            this.fraCtrls0.Location = new System.Drawing.Point(6, 40);
            this.fraCtrls0.Name = "fraCtrls0";
            this.fraCtrls0.Size = new System.Drawing.Size(420, 35);
            this.fraCtrls0.TabIndex = 2;
            this.fraCtrls0.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(384, 5);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "度";
            // 
            // udElevationMask
            // 
            this.udElevationMask.Location = new System.Drawing.Point(363, -5);
            this.udElevationMask.Name = "udElevationMask";
            this.udElevationMask.Size = new System.Drawing.Size(14, 31);
            this.udElevationMask.TabIndex = 5;
            // 
            // txtElevationMask
            // 
            this.txtElevationMask.Location = new System.Drawing.Point(325, 0);
            this.txtElevationMask.Name = "txtElevationMask";
            this.txtElevationMask.Size = new System.Drawing.Size(33, 19);
            this.txtElevationMask.TabIndex = 4;
            this.txtElevationMask.Text = "1234";
            this.txtElevationMask.TextChanged += new System.EventHandler(this.txtElevationMask_TextChanged);
            // 
            // optElevationMaskInput
            // 
            this.optElevationMaskInput.AutoSize = true;
            this.optElevationMaskInput.Location = new System.Drawing.Point(307, 3);
            this.optElevationMaskInput.Name = "optElevationMaskInput";
            this.optElevationMaskInput.Size = new System.Drawing.Size(14, 13);
            this.optElevationMaskInput.TabIndex = 3;
            this.optElevationMaskInput.TabStop = true;
            this.optElevationMaskInput.UseVisualStyleBackColor = true;
            // 
            // optElevationMaskObsData
            // 
            this.optElevationMaskObsData.AutoSize = true;
            this.optElevationMaskObsData.Location = new System.Drawing.Point(176, 3);
            this.optElevationMaskObsData.Name = "optElevationMaskObsData";
            this.optElevationMaskObsData.Size = new System.Drawing.Size(114, 16);
            this.optElevationMaskObsData.TabIndex = 2;
            this.optElevationMaskObsData.TabStop = true;
            this.optElevationMaskObsData.Text = "観測データを使用";
            this.optElevationMaskObsData.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(0, 5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(148, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "手簿に出力する最低高度角:";
            // 
            // cmbSession
            // 
            this.cmbSession.FormattingEnabled = true;
            this.cmbSession.Location = new System.Drawing.Point(76, 2);
            this.cmbSession.Name = "cmbSession";
            this.cmbSession.Size = new System.Drawing.Size(83, 20);
            this.cmbSession.TabIndex = 1;
            this.cmbSession.Text = "cmbSession";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(0, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "セッション名:";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.fraTab1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(497, 215);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "受信機";
            this.tabPage2.UseVisualStyleBackColor = true;
            this.tabPage2.Click += new System.EventHandler(this.tabPage2_Click);
            // 
            // fraTab1
            // 
            this.fraTab1.Controls.Add(this.txtRecNumber);
            this.fraTab1.Controls.Add(this.cmbRecType);
            this.fraTab1.Controls.Add(this.txtRecName);
            this.fraTab1.Controls.Add(this.cmbRecManufacturer);
            this.fraTab1.Controls.Add(this.label8);
            this.fraTab1.Controls.Add(this.optRecName);
            this.fraTab1.Controls.Add(this.label7);
            this.fraTab1.Controls.Add(this.label6);
            this.fraTab1.Controls.Add(this.optRecList);
            this.fraTab1.Controls.Add(this.label10);
            this.fraTab1.Location = new System.Drawing.Point(22, 23);
            this.fraTab1.Name = "fraTab1";
            this.fraTab1.Size = new System.Drawing.Size(446, 159);
            this.fraTab1.TabIndex = 1;
            this.fraTab1.TabStop = false;
            // 
            // txtRecNumber
            // 
            this.txtRecNumber.Location = new System.Drawing.Point(77, 131);
            this.txtRecNumber.Name = "txtRecNumber";
            this.txtRecNumber.Size = new System.Drawing.Size(336, 19);
            this.txtRecNumber.TabIndex = 12;
            this.txtRecNumber.Text = "12345678901234567890123456789012345678901234567";
            // 
            // cmbRecType
            // 
            this.cmbRecType.FormattingEnabled = true;
            this.cmbRecType.Location = new System.Drawing.Point(118, 60);
            this.cmbRecType.Name = "cmbRecType";
            this.cmbRecType.Size = new System.Drawing.Size(261, 20);
            this.cmbRecType.TabIndex = 11;
            this.cmbRecType.Text = "cmbRecType";
            this.cmbRecType.SelectedIndexChanged += new System.EventHandler(this.cmbRecType_SelectedIndexChanged);
            // 
            // txtRecName
            // 
            this.txtRecName.Location = new System.Drawing.Point(118, 99);
            this.txtRecName.Name = "txtRecName";
            this.txtRecName.Size = new System.Drawing.Size(294, 19);
            this.txtRecName.TabIndex = 10;
            this.txtRecName.Text = "12345678901234567890123456789012345678901234567";
            this.txtRecName.TextChanged += new System.EventHandler(this.txtRecName_TextChanged);
            // 
            // cmbRecManufacturer
            // 
            this.cmbRecManufacturer.FormattingEnabled = true;
            this.cmbRecManufacturer.Location = new System.Drawing.Point(118, 32);
            this.cmbRecManufacturer.Name = "cmbRecManufacturer";
            this.cmbRecManufacturer.Size = new System.Drawing.Size(261, 20);
            this.cmbRecManufacturer.TabIndex = 9;
            this.cmbRecManufacturer.Text = "cmbRecManufacturer";
            this.cmbRecManufacturer.SelectedIndexChanged += new System.EventHandler(this.cmbRecManufacturer_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(0, 134);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(74, 12);
            this.label8.TabIndex = 8;
            this.label8.Text = "シリアル番号:";
            // 
            // optRecName
            // 
            this.optRecName.AutoSize = true;
            this.optRecName.Location = new System.Drawing.Point(44, 102);
            this.optRecName.Name = "optRecName";
            this.optRecName.Size = new System.Drawing.Size(59, 16);
            this.optRecName.TabIndex = 7;
            this.optRecName.TabStop = true;
            this.optRecName.Text = "手入力";
            this.optRecName.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(59, 63);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(31, 12);
            this.label7.TabIndex = 6;
            this.label7.Text = "名称:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(59, 37);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(50, 12);
            this.label6.TabIndex = 5;
            this.label6.Text = "メーカー:";
            // 
            // optRecList
            // 
            this.optRecList.AutoSize = true;
            this.optRecList.Location = new System.Drawing.Point(44, 4);
            this.optRecList.Name = "optRecList";
            this.optRecList.Size = new System.Drawing.Size(97, 16);
            this.optRecList.TabIndex = 4;
            this.optRecList.TabStop = true;
            this.optRecList.Text = "リストから選択";
            this.optRecList.UseVisualStyleBackColor = true;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(0, 5);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(31, 12);
            this.label10.TabIndex = 0;
            this.label10.Text = "名称:";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.fraTab2);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(497, 215);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "アンテナ";
            this.tabPage3.UseVisualStyleBackColor = true;
            this.tabPage3.Click += new System.EventHandler(this.tabPage3_Click);
            // 
            // fraTab2
            // 
            this.fraTab2.Controls.Add(this.label15);
            this.fraTab2.Controls.Add(this.label14);
            this.fraTab2.Controls.Add(this.cmbAntMeasurement);
            this.fraTab2.Controls.Add(this.label13);
            this.fraTab2.Controls.Add(this.txtAntNumber);
            this.fraTab2.Controls.Add(this.cmbAntType);
            this.fraTab2.Controls.Add(this.txtAntHeight);
            this.fraTab2.Controls.Add(this.cmbAntManufacturer);
            this.fraTab2.Controls.Add(this.label9);
            this.fraTab2.Controls.Add(this.label11);
            this.fraTab2.Controls.Add(this.label12);
            this.fraTab2.Location = new System.Drawing.Point(22, 23);
            this.fraTab2.Name = "fraTab2";
            this.fraTab2.Size = new System.Drawing.Size(446, 159);
            this.fraTab2.TabIndex = 2;
            this.fraTab2.TabStop = false;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(170, 134);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(16, 12);
            this.label15.TabIndex = 16;
            this.label15.Text = "ｍ";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(3, 133);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(62, 12);
            this.label14.TabIndex = 15;
            this.label14.Text = "アンテナ高:";
            // 
            // cmbAntMeasurement
            // 
            this.cmbAntMeasurement.FormattingEnabled = true;
            this.cmbAntMeasurement.Location = new System.Drawing.Point(86, 96);
            this.cmbAntMeasurement.Name = "cmbAntMeasurement";
            this.cmbAntMeasurement.Size = new System.Drawing.Size(260, 20);
            this.cmbAntMeasurement.TabIndex = 14;
            this.cmbAntMeasurement.Text = "cmbAntMeasurement";
            this.cmbAntMeasurement.SelectedIndexChanged += new System.EventHandler(this.cmbAntMeasurement_SelectedIndexChanged);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(4, 100);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(55, 12);
            this.label13.TabIndex = 13;
            this.label13.Text = "測位方法:";
            // 
            // txtAntNumber
            // 
            this.txtAntNumber.Location = new System.Drawing.Point(86, 65);
            this.txtAntNumber.Name = "txtAntNumber";
            this.txtAntNumber.Size = new System.Drawing.Size(336, 19);
            this.txtAntNumber.TabIndex = 12;
            this.txtAntNumber.Text = "12345678901234567890123456789012345678901234567";
            // 
            // cmbAntType
            // 
            this.cmbAntType.FormattingEnabled = true;
            this.cmbAntType.Location = new System.Drawing.Point(86, 33);
            this.cmbAntType.Name = "cmbAntType";
            this.cmbAntType.Size = new System.Drawing.Size(336, 20);
            this.cmbAntType.TabIndex = 11;
            this.cmbAntType.Text = "cmbAntType";
            this.cmbAntType.SelectedIndexChanged += new System.EventHandler(this.cmbAntType_SelectedIndexChanged);
            // 
            // txtAntHeight
            // 
            this.txtAntHeight.Location = new System.Drawing.Point(86, 130);
            this.txtAntHeight.Name = "txtAntHeight";
            this.txtAntHeight.Size = new System.Drawing.Size(82, 19);
            this.txtAntHeight.TabIndex = 10;
            this.txtAntHeight.Text = "123456789012";
            // 
            // cmbAntManufacturer
            // 
            this.cmbAntManufacturer.FormattingEnabled = true;
            this.cmbAntManufacturer.Location = new System.Drawing.Point(86, 0);
            this.cmbAntManufacturer.Name = "cmbAntManufacturer";
            this.cmbAntManufacturer.Size = new System.Drawing.Size(260, 20);
            this.cmbAntManufacturer.TabIndex = 9;
            this.cmbAntManufacturer.Text = "cmbAntManufacturer";
            this.cmbAntManufacturer.SelectedIndexChanged += new System.EventHandler(this.cmbAntManufacturer_SelectedIndexChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(3, 68);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(74, 12);
            this.label9.TabIndex = 8;
            this.label9.Text = "シリアル番号:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(3, 36);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(31, 12);
            this.label11.TabIndex = 6;
            this.label11.Text = "名称:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(3, 2);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(50, 12);
            this.label12.TabIndex = 5;
            this.label12.Text = "メーカー:";
            // 
            // CancelButton
            // 
            this.CancelButton.Location = new System.Drawing.Point(431, 270);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(94, 22);
            this.CancelButton.TabIndex = 14;
            this.CancelButton.Text = "キャンセル";
            this.CancelButton.UseVisualStyleBackColor = true;
            this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // OKButton
            // 
            this.OKButton.Location = new System.Drawing.Point(318, 270);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(95, 25);
            this.OKButton.TabIndex = 13;
            this.OKButton.Text = "OK";
            this.OKButton.UseVisualStyleBackColor = true;
            this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
            // 
            // frmAttribute
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(549, 311);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.OKButton);
            this.Controls.Add(this.tsTab);
            this.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Name = "frmAttribute";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "観測データの編集";
            this.tsTab.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.fraTab0.ResumeLayout(false);
            this.fraTab0.PerformLayout();
            this.fraCtrls1.ResumeLayout(false);
            this.fraCtrls1.PerformLayout();
            this.fraCtrls0.ResumeLayout(false);
            this.fraCtrls0.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.fraTab1.ResumeLayout(false);
            this.fraTab1.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.fraTab2.ResumeLayout(false);
            this.fraTab2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tsTab;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        internal System.Windows.Forms.Button CancelButton;
        internal System.Windows.Forms.Button OKButton;
        private System.Windows.Forms.GroupBox fraTab0;
        private System.Windows.Forms.ComboBox cmbSession;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox fraCtrls0;
        private System.Windows.Forms.RadioButton optElevationMaskObsData;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtElevationMask;
        private System.Windows.Forms.RadioButton optElevationMaskInput;
        private System.Windows.Forms.VScrollBar udElevationMask;
        private System.Windows.Forms.GroupBox fraCtrls1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RadioButton optNumberOfMinSVStatic;
        private System.Windows.Forms.RadioButton optNumberOfMinSVObsData;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbNumberOfMinSV;
        private System.Windows.Forms.GroupBox fraTab1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.RadioButton optRecList;
        private System.Windows.Forms.ComboBox cmbRecManufacturer;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.RadioButton optRecName;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtRecName;
        private System.Windows.Forms.TextBox txtRecNumber;
        private System.Windows.Forms.ComboBox cmbRecType;
        private System.Windows.Forms.GroupBox fraTab2;
        private System.Windows.Forms.TextBox txtAntNumber;
        private System.Windows.Forms.ComboBox cmbAntType;
        private System.Windows.Forms.TextBox txtAntHeight;
        private System.Windows.Forms.ComboBox cmbAntManufacturer;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ComboBox cmbAntMeasurement;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label15;
    }
}