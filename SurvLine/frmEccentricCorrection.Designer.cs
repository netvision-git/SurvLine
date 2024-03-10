namespace SurvLine
{
    partial class frmEccentricCorrection
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
            this.Label4 = new System.Windows.Forms.Label();
            this.lblEccentricPoint = new System.Windows.Forms.Label();
            this.fraTab0 = new System.Windows.Forms.GroupBox();
            this.fraHorizontal = new System.Windows.Forms.GroupBox();
            this.fraInput = new System.Windows.Forms.GroupBox();
            this.cmbUsePointHori = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.lblNo = new System.Windows.Forms.Label();
            this.cmdInput = new System.Windows.Forms.Button();
            this.optInput = new System.Windows.Forms.RadioButton();
            this.optGPS = new System.Windows.Forms.RadioButton();
            this.lblMarker = new System.Windows.Forms.Label();
            this.lblHoriS = new System.Windows.Forms.Label();
            this.txtHoriS = new System.Windows.Forms.TextBox();
            this.lblHoriM = new System.Windows.Forms.Label();
            this.txtHoriM = new System.Windows.Forms.TextBox();
            this.lblHoriH = new System.Windows.Forms.Label();
            this.txtHoriH = new System.Windows.Forms.TextBox();
            this.lblHorizontal = new System.Windows.Forms.Label();
            this.optDirection = new System.Windows.Forms.RadioButton();
            this.optHorizontal = new System.Windows.Forms.RadioButton();
            this.fraTab1 = new System.Windows.Forms.GroupBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtToHeightTS = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.txtFromHeightTS = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.chkTS = new System.Windows.Forms.CheckBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtDistance = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.fraTab2 = new System.Windows.Forms.GroupBox();
            this.fraCtrls1 = new System.Windows.Forms.GroupBox();
            this.label23 = new System.Windows.Forms.Label();
            this.txtToHeightCB = new System.Windows.Forms.TextBox();
            this.label24 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.txtFromHeightCB = new System.Windows.Forms.TextBox();
            this.label26 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.txtElevationCBS = new System.Windows.Forms.TextBox();
            this.label28 = new System.Windows.Forms.Label();
            this.txtElevationCBM = new System.Windows.Forms.TextBox();
            this.label29 = new System.Windows.Forms.Label();
            this.txtElevationCBH = new System.Windows.Forms.TextBox();
            this.label30 = new System.Windows.Forms.Label();
            this.fraCtrls0 = new System.Windows.Forms.GroupBox();
            this.label21 = new System.Windows.Forms.Label();
            this.txtToHeightBC = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.txtFromHeightBC = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.txtElevationBCS = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.txtElevationBCM = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.txtElevationBCH = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.txtGenuineName = new System.Windows.Forms.TextBox();
            this.txtGenuineNumber = new System.Windows.Forms.TextBox();
            this.label33 = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.picEccentricPicture = new System.Windows.Forms.PictureBox();
            this.CancelButton = new System.Windows.Forms.Button();
            this.OKButton = new System.Windows.Forms.Button();
            this.fraTab0.SuspendLayout();
            this.fraHorizontal.SuspendLayout();
            this.fraInput.SuspendLayout();
            this.fraTab1.SuspendLayout();
            this.fraTab2.SuspendLayout();
            this.fraCtrls1.SuspendLayout();
            this.fraCtrls0.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picEccentricPicture)).BeginInit();
            this.SuspendLayout();
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.Location = new System.Drawing.Point(11, 16);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(57, 12);
            this.Label4.TabIndex = 0;
            this.Label4.Text = "偏心点(B)";
            // 
            // lblEccentricPoint
            // 
            this.lblEccentricPoint.Location = new System.Drawing.Point(76, 17);
            this.lblEccentricPoint.Name = "lblEccentricPoint";
            this.lblEccentricPoint.Size = new System.Drawing.Size(214, 34);
            this.lblEccentricPoint.TabIndex = 1;
            this.lblEccentricPoint.Text = "0000(&&&&)123456789012345678901234567";
            // 
            // fraTab0
            // 
            this.fraTab0.Controls.Add(this.fraHorizontal);
            this.fraTab0.Controls.Add(this.optDirection);
            this.fraTab0.Controls.Add(this.optHorizontal);
            this.fraTab0.Location = new System.Drawing.Point(18, 49);
            this.fraTab0.Name = "fraTab0";
            this.fraTab0.Size = new System.Drawing.Size(309, 255);
            this.fraTab0.TabIndex = 2;
            this.fraTab0.TabStop = false;
            this.fraTab0.Tag = "";
            this.fraTab0.Text = "角度";
            // 
            // fraHorizontal
            // 
            this.fraHorizontal.Controls.Add(this.fraInput);
            this.fraHorizontal.Controls.Add(this.optInput);
            this.fraHorizontal.Controls.Add(this.optGPS);
            this.fraHorizontal.Controls.Add(this.lblMarker);
            this.fraHorizontal.Controls.Add(this.lblHoriS);
            this.fraHorizontal.Controls.Add(this.txtHoriS);
            this.fraHorizontal.Controls.Add(this.lblHoriM);
            this.fraHorizontal.Controls.Add(this.txtHoriM);
            this.fraHorizontal.Controls.Add(this.lblHoriH);
            this.fraHorizontal.Controls.Add(this.txtHoriH);
            this.fraHorizontal.Controls.Add(this.lblHorizontal);
            this.fraHorizontal.Location = new System.Drawing.Point(3, 47);
            this.fraHorizontal.Name = "fraHorizontal";
            this.fraHorizontal.Size = new System.Drawing.Size(301, 202);
            this.fraHorizontal.TabIndex = 25;
            this.fraHorizontal.TabStop = false;
            this.fraHorizontal.Tag = "";
            this.fraHorizontal.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // fraInput
            // 
            this.fraInput.Controls.Add(this.cmbUsePointHori);
            this.fraInput.Controls.Add(this.label8);
            this.fraInput.Controls.Add(this.lblNo);
            this.fraInput.Controls.Add(this.cmdInput);
            this.fraInput.Location = new System.Drawing.Point(0, 86);
            this.fraInput.Name = "fraInput";
            this.fraInput.Size = new System.Drawing.Size(301, 104);
            this.fraInput.TabIndex = 25;
            this.fraInput.TabStop = false;
            // 
            // cmbUsePointHori
            // 
            this.cmbUsePointHori.FormattingEnabled = true;
            this.cmbUsePointHori.Location = new System.Drawing.Point(49, 72);
            this.cmbUsePointHori.Name = "cmbUsePointHori";
            this.cmbUsePointHori.Size = new System.Drawing.Size(211, 20);
            this.cmbUsePointHori.TabIndex = 28;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label8.Location = new System.Drawing.Point(13, 48);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(79, 12);
            this.label8.TabIndex = 27;
            this.label8.Text = "偏心座標候補:";
            // 
            // lblNo
            // 
            this.lblNo.Location = new System.Drawing.Point(60, 16);
            this.lblNo.Name = "lblNo";
            this.lblNo.Size = new System.Drawing.Size(138, 26);
            this.lblNo.TabIndex = 26;
            this.lblNo.Text = "123456789012345678901";
            this.lblNo.Click += new System.EventHandler(this.label1_Click);
            // 
            // cmdInput
            // 
            this.cmdInput.Location = new System.Drawing.Point(199, 12);
            this.cmdInput.Name = "cmdInput";
            this.cmdInput.Size = new System.Drawing.Size(62, 21);
            this.cmdInput.TabIndex = 25;
            this.cmdInput.Text = "座標入力";
            this.cmdInput.UseVisualStyleBackColor = true;
            // 
            // optInput
            // 
            this.optInput.AutoSize = true;
            this.optInput.Location = new System.Drawing.Point(190, 63);
            this.optInput.Name = "optInput";
            this.optInput.Size = new System.Drawing.Size(59, 16);
            this.optInput.TabIndex = 23;
            this.optInput.TabStop = true;
            this.optInput.Text = "手入力";
            this.optInput.UseVisualStyleBackColor = true;
            // 
            // optGPS
            // 
            this.optGPS.AutoSize = true;
            this.optGPS.Location = new System.Drawing.Point(87, 63);
            this.optGPS.Name = "optGPS";
            this.optGPS.Size = new System.Drawing.Size(86, 16);
            this.optGPS.TabIndex = 22;
            this.optGPS.TabStop = true;
            this.optGPS.Text = "GPSベクトル";
            this.optGPS.UseVisualStyleBackColor = true;
            // 
            // lblMarker
            // 
            this.lblMarker.AutoSize = true;
            this.lblMarker.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblMarker.Location = new System.Drawing.Point(18, 66);
            this.lblMarker.Name = "lblMarker";
            this.lblMarker.Size = new System.Drawing.Size(43, 12);
            this.lblMarker.TabIndex = 21;
            this.lblMarker.Text = "方位標:";
            // 
            // lblHoriS
            // 
            this.lblHoriS.AutoSize = true;
            this.lblHoriS.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblHoriS.Location = new System.Drawing.Point(234, 26);
            this.lblHoriS.Name = "lblHoriS";
            this.lblHoriS.Size = new System.Drawing.Size(17, 12);
            this.lblHoriS.TabIndex = 20;
            this.lblHoriS.Text = "″";
            this.lblHoriS.Click += new System.EventHandler(this.label3_Click);
            // 
            // txtHoriS
            // 
            this.txtHoriS.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtHoriS.Location = new System.Drawing.Point(194, 23);
            this.txtHoriS.Name = "txtHoriS";
            this.txtHoriS.Size = new System.Drawing.Size(39, 19);
            this.txtHoriS.TabIndex = 19;
            this.txtHoriS.Text = "1234";
            this.txtHoriS.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // lblHoriM
            // 
            this.lblHoriM.AutoSize = true;
            this.lblHoriM.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblHoriM.Location = new System.Drawing.Point(178, 26);
            this.lblHoriM.Name = "lblHoriM";
            this.lblHoriM.Size = new System.Drawing.Size(17, 12);
            this.lblHoriM.TabIndex = 18;
            this.lblHoriM.Text = "′";
            this.lblHoriM.Click += new System.EventHandler(this.label5_Click);
            // 
            // txtHoriM
            // 
            this.txtHoriM.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtHoriM.Location = new System.Drawing.Point(143, 23);
            this.txtHoriM.Name = "txtHoriM";
            this.txtHoriM.Size = new System.Drawing.Size(32, 19);
            this.txtHoriM.TabIndex = 17;
            this.txtHoriM.Text = "1234";
            this.txtHoriM.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // lblHoriH
            // 
            this.lblHoriH.AutoSize = true;
            this.lblHoriH.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblHoriH.Location = new System.Drawing.Point(130, 26);
            this.lblHoriH.Name = "lblHoriH";
            this.lblHoriH.Size = new System.Drawing.Size(17, 12);
            this.lblHoriH.TabIndex = 16;
            this.lblHoriH.Text = "°";
            this.lblHoriH.Click += new System.EventHandler(this.label6_Click);
            // 
            // txtHoriH
            // 
            this.txtHoriH.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtHoriH.Location = new System.Drawing.Point(87, 23);
            this.txtHoriH.Name = "txtHoriH";
            this.txtHoriH.Size = new System.Drawing.Size(40, 19);
            this.txtHoriH.TabIndex = 15;
            this.txtHoriH.Text = "12345";
            this.txtHoriH.TextChanged += new System.EventHandler(this.textBox4_TextChanged);
            // 
            // lblHorizontal
            // 
            this.lblHorizontal.AutoSize = true;
            this.lblHorizontal.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblHorizontal.Location = new System.Drawing.Point(18, 27);
            this.lblHorizontal.Name = "lblHorizontal";
            this.lblHorizontal.Size = new System.Drawing.Size(63, 12);
            this.lblHorizontal.TabIndex = 14;
            this.lblHorizontal.Text = "水平角(Φ):";
            this.lblHorizontal.Click += new System.EventHandler(this.label7_Click);
            // 
            // optDirection
            // 
            this.optDirection.AutoSize = true;
            this.optDirection.Location = new System.Drawing.Point(121, 20);
            this.optDirection.Name = "optDirection";
            this.optDirection.Size = new System.Drawing.Size(94, 16);
            this.optDirection.TabIndex = 1;
            this.optDirection.TabStop = true;
            this.optDirection.Text = "方位角で入力";
            this.optDirection.UseVisualStyleBackColor = true;
            // 
            // optHorizontal
            // 
            this.optHorizontal.AutoSize = true;
            this.optHorizontal.Location = new System.Drawing.Point(18, 20);
            this.optHorizontal.Name = "optHorizontal";
            this.optHorizontal.Size = new System.Drawing.Size(94, 16);
            this.optHorizontal.TabIndex = 0;
            this.optHorizontal.TabStop = true;
            this.optHorizontal.Text = "水平角で入力";
            this.optHorizontal.UseVisualStyleBackColor = true;
            // 
            // fraTab1
            // 
            this.fraTab1.Controls.Add(this.label13);
            this.fraTab1.Controls.Add(this.txtToHeightTS);
            this.fraTab1.Controls.Add(this.label14);
            this.fraTab1.Controls.Add(this.label11);
            this.fraTab1.Controls.Add(this.txtFromHeightTS);
            this.fraTab1.Controls.Add(this.label12);
            this.fraTab1.Controls.Add(this.chkTS);
            this.fraTab1.Controls.Add(this.label10);
            this.fraTab1.Controls.Add(this.txtDistance);
            this.fraTab1.Controls.Add(this.label9);
            this.fraTab1.Location = new System.Drawing.Point(19, 304);
            this.fraTab1.Name = "fraTab1";
            this.fraTab1.Size = new System.Drawing.Size(309, 115);
            this.fraTab1.TabIndex = 3;
            this.fraTab1.TabStop = false;
            this.fraTab1.Tag = "";
            this.fraTab1.Text = "距離";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label13.Location = new System.Drawing.Point(270, 87);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(16, 12);
            this.label13.TabIndex = 28;
            this.label13.Text = "ｍ";
            // 
            // txtToHeightTS
            // 
            this.txtToHeightTS.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtToHeightTS.Location = new System.Drawing.Point(187, 83);
            this.txtToHeightTS.Name = "txtToHeightTS";
            this.txtToHeightTS.Size = new System.Drawing.Size(79, 19);
            this.txtToHeightTS.TabIndex = 27;
            this.txtToHeightTS.Text = "123456789012";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label14.Location = new System.Drawing.Point(128, 86);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(55, 12);
            this.label14.TabIndex = 26;
            this.label14.Text = "反射鏡高:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label11.Location = new System.Drawing.Point(270, 62);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(16, 12);
            this.label11.TabIndex = 25;
            this.label11.Text = "ｍ";
            // 
            // txtFromHeightTS
            // 
            this.txtFromHeightTS.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtFromHeightTS.Location = new System.Drawing.Point(187, 58);
            this.txtFromHeightTS.Name = "txtFromHeightTS";
            this.txtFromHeightTS.Size = new System.Drawing.Size(79, 19);
            this.txtFromHeightTS.TabIndex = 24;
            this.txtFromHeightTS.Text = "123456789012";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label12.Location = new System.Drawing.Point(128, 61);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(55, 12);
            this.label12.TabIndex = 23;
            this.label12.Text = "測距儀高:";
            // 
            // chkTS
            // 
            this.chkTS.AutoSize = true;
            this.chkTS.Location = new System.Drawing.Point(22, 61);
            this.chkTS.Name = "chkTS";
            this.chkTS.Size = new System.Drawing.Size(94, 16);
            this.chkTS.TabIndex = 22;
            this.chkTS.Text = "測距儀を使用";
            this.chkTS.UseVisualStyleBackColor = true;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label10.Location = new System.Drawing.Point(166, 27);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(16, 12);
            this.label10.TabIndex = 21;
            this.label10.Text = "ｍ";
            // 
            // txtDistance
            // 
            this.txtDistance.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtDistance.Location = new System.Drawing.Point(85, 23);
            this.txtDistance.Name = "txtDistance";
            this.txtDistance.Size = new System.Drawing.Size(79, 19);
            this.txtDistance.TabIndex = 16;
            this.txtDistance.Text = "123456789012";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label9.Location = new System.Drawing.Point(20, 26);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(59, 12);
            this.label9.TabIndex = 15;
            this.label9.Text = "斜距離(D):";
            // 
            // fraTab2
            // 
            this.fraTab2.Controls.Add(this.fraCtrls1);
            this.fraTab2.Controls.Add(this.fraCtrls0);
            this.fraTab2.Location = new System.Drawing.Point(21, 425);
            this.fraTab2.Name = "fraTab2";
            this.fraTab2.Size = new System.Drawing.Size(552, 136);
            this.fraTab2.TabIndex = 4;
            this.fraTab2.TabStop = false;
            this.fraTab2.Text = "高度角補正";
            // 
            // fraCtrls1
            // 
            this.fraCtrls1.Controls.Add(this.label23);
            this.fraCtrls1.Controls.Add(this.txtToHeightCB);
            this.fraCtrls1.Controls.Add(this.label24);
            this.fraCtrls1.Controls.Add(this.label25);
            this.fraCtrls1.Controls.Add(this.txtFromHeightCB);
            this.fraCtrls1.Controls.Add(this.label26);
            this.fraCtrls1.Controls.Add(this.label27);
            this.fraCtrls1.Controls.Add(this.txtElevationCBS);
            this.fraCtrls1.Controls.Add(this.label28);
            this.fraCtrls1.Controls.Add(this.txtElevationCBM);
            this.fraCtrls1.Controls.Add(this.label29);
            this.fraCtrls1.Controls.Add(this.txtElevationCBH);
            this.fraCtrls1.Controls.Add(this.label30);
            this.fraCtrls1.Location = new System.Drawing.Point(281, 17);
            this.fraCtrls1.Name = "fraCtrls1";
            this.fraCtrls1.Size = new System.Drawing.Size(259, 107);
            this.fraCtrls1.TabIndex = 1;
            this.fraCtrls1.TabStop = false;
            this.fraCtrls1.Text = "C→B";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label23.Location = new System.Drawing.Point(173, 76);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(16, 12);
            this.label23.TabIndex = 33;
            this.label23.Text = "ｍ";
            // 
            // txtToHeightCB
            // 
            this.txtToHeightCB.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtToHeightCB.Location = new System.Drawing.Point(90, 72);
            this.txtToHeightCB.Name = "txtToHeightCB";
            this.txtToHeightCB.Size = new System.Drawing.Size(79, 19);
            this.txtToHeightCB.TabIndex = 32;
            this.txtToHeightCB.Text = "123456789012";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label24.Location = new System.Drawing.Point(21, 75);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(43, 12);
            this.label24.TabIndex = 31;
            this.label24.Text = "目標高:";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label25.Location = new System.Drawing.Point(172, 50);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(16, 12);
            this.label25.TabIndex = 30;
            this.label25.Text = "ｍ";
            // 
            // txtFromHeightCB
            // 
            this.txtFromHeightCB.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtFromHeightCB.Location = new System.Drawing.Point(89, 46);
            this.txtFromHeightCB.Name = "txtFromHeightCB";
            this.txtFromHeightCB.Size = new System.Drawing.Size(79, 19);
            this.txtFromHeightCB.TabIndex = 29;
            this.txtFromHeightCB.Text = "123456789012";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label26.Location = new System.Drawing.Point(20, 49);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(43, 12);
            this.label26.TabIndex = 28;
            this.label26.Text = "器械高:";
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label27.Location = new System.Drawing.Point(233, 23);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(17, 12);
            this.label27.TabIndex = 27;
            this.label27.Text = "″";
            // 
            // txtElevationCBS
            // 
            this.txtElevationCBS.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtElevationCBS.Location = new System.Drawing.Point(195, 19);
            this.txtElevationCBS.Name = "txtElevationCBS";
            this.txtElevationCBS.Size = new System.Drawing.Size(35, 19);
            this.txtElevationCBS.TabIndex = 26;
            this.txtElevationCBS.Text = "1234";
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label28.Location = new System.Drawing.Point(180, 22);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(17, 12);
            this.label28.TabIndex = 25;
            this.label28.Text = "′";
            // 
            // txtElevationCBM
            // 
            this.txtElevationCBM.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtElevationCBM.Location = new System.Drawing.Point(145, 19);
            this.txtElevationCBM.Name = "txtElevationCBM";
            this.txtElevationCBM.Size = new System.Drawing.Size(32, 19);
            this.txtElevationCBM.TabIndex = 24;
            this.txtElevationCBM.Text = "1234";
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label29.Location = new System.Drawing.Point(132, 22);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(17, 12);
            this.label29.TabIndex = 23;
            this.label29.Text = "°";
            // 
            // txtElevationCBH
            // 
            this.txtElevationCBH.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtElevationCBH.Location = new System.Drawing.Point(89, 19);
            this.txtElevationCBH.Name = "txtElevationCBH";
            this.txtElevationCBH.Size = new System.Drawing.Size(40, 19);
            this.txtElevationCBH.TabIndex = 22;
            this.txtElevationCBH.Text = "12345";
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label30.Location = new System.Drawing.Point(20, 23);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(67, 12);
            this.label30.TabIndex = 21;
            this.label30.Text = "観測高度角:";
            // 
            // fraCtrls0
            // 
            this.fraCtrls0.Controls.Add(this.label21);
            this.fraCtrls0.Controls.Add(this.txtToHeightBC);
            this.fraCtrls0.Controls.Add(this.label22);
            this.fraCtrls0.Controls.Add(this.label19);
            this.fraCtrls0.Controls.Add(this.txtFromHeightBC);
            this.fraCtrls0.Controls.Add(this.label20);
            this.fraCtrls0.Controls.Add(this.label15);
            this.fraCtrls0.Controls.Add(this.txtElevationBCS);
            this.fraCtrls0.Controls.Add(this.label16);
            this.fraCtrls0.Controls.Add(this.txtElevationBCM);
            this.fraCtrls0.Controls.Add(this.label17);
            this.fraCtrls0.Controls.Add(this.txtElevationBCH);
            this.fraCtrls0.Controls.Add(this.label18);
            this.fraCtrls0.Location = new System.Drawing.Point(12, 18);
            this.fraCtrls0.Name = "fraCtrls0";
            this.fraCtrls0.Size = new System.Drawing.Size(259, 107);
            this.fraCtrls0.TabIndex = 0;
            this.fraCtrls0.TabStop = false;
            this.fraCtrls0.Text = "B→C";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label21.Location = new System.Drawing.Point(173, 76);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(16, 12);
            this.label21.TabIndex = 33;
            this.label21.Text = "ｍ";
            // 
            // txtToHeightBC
            // 
            this.txtToHeightBC.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtToHeightBC.Location = new System.Drawing.Point(90, 72);
            this.txtToHeightBC.Name = "txtToHeightBC";
            this.txtToHeightBC.Size = new System.Drawing.Size(79, 19);
            this.txtToHeightBC.TabIndex = 32;
            this.txtToHeightBC.Text = "123456789012";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label22.Location = new System.Drawing.Point(21, 75);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(43, 12);
            this.label22.TabIndex = 31;
            this.label22.Text = "目標高:";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label19.Location = new System.Drawing.Point(172, 50);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(16, 12);
            this.label19.TabIndex = 30;
            this.label19.Text = "ｍ";
            // 
            // txtFromHeightBC
            // 
            this.txtFromHeightBC.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtFromHeightBC.Location = new System.Drawing.Point(89, 46);
            this.txtFromHeightBC.Name = "txtFromHeightBC";
            this.txtFromHeightBC.Size = new System.Drawing.Size(79, 19);
            this.txtFromHeightBC.TabIndex = 29;
            this.txtFromHeightBC.Text = "123456789012";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label20.Location = new System.Drawing.Point(20, 49);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(43, 12);
            this.label20.TabIndex = 28;
            this.label20.Text = "器械高:";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label15.Location = new System.Drawing.Point(233, 23);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(17, 12);
            this.label15.TabIndex = 27;
            this.label15.Text = "″";
            // 
            // txtElevationBCS
            // 
            this.txtElevationBCS.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtElevationBCS.Location = new System.Drawing.Point(195, 19);
            this.txtElevationBCS.Name = "txtElevationBCS";
            this.txtElevationBCS.Size = new System.Drawing.Size(35, 19);
            this.txtElevationBCS.TabIndex = 26;
            this.txtElevationBCS.Text = "1234";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label16.Location = new System.Drawing.Point(180, 22);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(17, 12);
            this.label16.TabIndex = 25;
            this.label16.Text = "′";
            // 
            // txtElevationBCM
            // 
            this.txtElevationBCM.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtElevationBCM.Location = new System.Drawing.Point(145, 19);
            this.txtElevationBCM.Name = "txtElevationBCM";
            this.txtElevationBCM.Size = new System.Drawing.Size(32, 19);
            this.txtElevationBCM.TabIndex = 24;
            this.txtElevationBCM.Text = "1234";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label17.Location = new System.Drawing.Point(132, 22);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(17, 12);
            this.label17.TabIndex = 23;
            this.label17.Text = "°";
            // 
            // txtElevationBCH
            // 
            this.txtElevationBCH.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtElevationBCH.Location = new System.Drawing.Point(89, 19);
            this.txtElevationBCH.Name = "txtElevationBCH";
            this.txtElevationBCH.Size = new System.Drawing.Size(40, 19);
            this.txtElevationBCH.TabIndex = 22;
            this.txtElevationBCH.Text = "12345";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label18.Location = new System.Drawing.Point(20, 23);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(67, 12);
            this.label18.TabIndex = 21;
            this.label18.Text = "観測高度角:";
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Location = new System.Drawing.Point(334, 16);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(45, 12);
            this.label31.TabIndex = 5;
            this.label31.Text = "本点(C)";
            // 
            // txtGenuineName
            // 
            this.txtGenuineName.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtGenuineName.Location = new System.Drawing.Point(458, 41);
            this.txtGenuineName.Name = "txtGenuineName";
            this.txtGenuineName.Size = new System.Drawing.Size(255, 19);
            this.txtGenuineName.TabIndex = 10;
            // 
            // txtGenuineNumber
            // 
            this.txtGenuineNumber.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtGenuineNumber.Location = new System.Drawing.Point(456, 10);
            this.txtGenuineNumber.Name = "txtGenuineNumber";
            this.txtGenuineNumber.Size = new System.Drawing.Size(255, 19);
            this.txtGenuineNumber.TabIndex = 9;
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label33.Location = new System.Drawing.Point(385, 45);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(67, 12);
            this.label33.TabIndex = 8;
            this.label33.Text = "観測点名称:";
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label34.Location = new System.Drawing.Point(385, 16);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(57, 12);
            this.label34.TabIndex = 7;
            this.label34.Text = "観測点No:";
            // 
            // picEccentricPicture
            // 
            this.picEccentricPicture.Location = new System.Drawing.Point(344, 69);
            this.picEccentricPicture.Name = "picEccentricPicture";
            this.picEccentricPicture.Size = new System.Drawing.Size(369, 350);
            this.picEccentricPicture.TabIndex = 11;
            this.picEccentricPicture.TabStop = false;
            // 
            // CancelButton
            // 
            this.CancelButton.Location = new System.Drawing.Point(619, 528);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(94, 22);
            this.CancelButton.TabIndex = 14;
            this.CancelButton.Text = "キャンセル";
            this.CancelButton.UseVisualStyleBackColor = true;
            // 
            // OKButton
            // 
            this.OKButton.Location = new System.Drawing.Point(619, 489);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(95, 25);
            this.OKButton.TabIndex = 13;
            this.OKButton.Text = "OK";
            this.OKButton.UseVisualStyleBackColor = true;
            // 
            // frmEccentricCorrection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(731, 573);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.OKButton);
            this.Controls.Add(this.picEccentricPicture);
            this.Controls.Add(this.txtGenuineName);
            this.Controls.Add(this.txtGenuineNumber);
            this.Controls.Add(this.label33);
            this.Controls.Add(this.label34);
            this.Controls.Add(this.label31);
            this.Controls.Add(this.fraTab2);
            this.Controls.Add(this.fraTab1);
            this.Controls.Add(this.fraTab0);
            this.Controls.Add(this.lblEccentricPoint);
            this.Controls.Add(this.Label4);
            this.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Name = "frmEccentricCorrection";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "偏心設定";
            this.fraTab0.ResumeLayout(false);
            this.fraTab0.PerformLayout();
            this.fraHorizontal.ResumeLayout(false);
            this.fraHorizontal.PerformLayout();
            this.fraInput.ResumeLayout(false);
            this.fraInput.PerformLayout();
            this.fraTab1.ResumeLayout(false);
            this.fraTab1.PerformLayout();
            this.fraTab2.ResumeLayout(false);
            this.fraCtrls1.ResumeLayout(false);
            this.fraCtrls1.PerformLayout();
            this.fraCtrls0.ResumeLayout(false);
            this.fraCtrls0.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picEccentricPicture)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Label4;
        private System.Windows.Forms.Label lblEccentricPoint;
        private System.Windows.Forms.GroupBox fraTab0;
        private System.Windows.Forms.RadioButton optHorizontal;
        private System.Windows.Forms.RadioButton optDirection;
        private System.Windows.Forms.GroupBox fraHorizontal;
        private System.Windows.Forms.RadioButton optInput;
        private System.Windows.Forms.RadioButton optGPS;
        private System.Windows.Forms.Label lblMarker;
        private System.Windows.Forms.Label lblHoriS;
        private System.Windows.Forms.TextBox txtHoriS;
        private System.Windows.Forms.Label lblHoriM;
        private System.Windows.Forms.TextBox txtHoriM;
        private System.Windows.Forms.Label lblHoriH;
        private System.Windows.Forms.TextBox txtHoriH;
        private System.Windows.Forms.Label lblHorizontal;
        private System.Windows.Forms.GroupBox fraInput;
        private System.Windows.Forms.Button cmdInput;
        private System.Windows.Forms.Label lblNo;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cmbUsePointHori;
        private System.Windows.Forms.GroupBox fraTab1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtFromHeightTS;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.CheckBox chkTS;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtDistance;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtToHeightTS;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.GroupBox fraTab2;
        private System.Windows.Forms.GroupBox fraCtrls0;
        private System.Windows.Forms.GroupBox fraCtrls1;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.TextBox txtToHeightCB;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.TextBox txtFromHeightCB;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.TextBox txtElevationCBS;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.TextBox txtElevationCBM;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.TextBox txtElevationCBH;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TextBox txtToHeightBC;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox txtFromHeightBC;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtElevationBCS;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox txtElevationBCM;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox txtElevationBCH;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.TextBox txtGenuineName;
        private System.Windows.Forms.TextBox txtGenuineNumber;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.PictureBox picEccentricPicture;
        internal System.Windows.Forms.Button CancelButton;
        internal System.Windows.Forms.Button OKButton;
    }
}