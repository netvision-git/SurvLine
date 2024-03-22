using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static SurvLine.mdl.DEFINE;
using static SurvLine.mdl.MdlNSDefine;
using static SurvLine.mdl.MdlUtility;
using static SurvLine.mdl.MdlAccountMake;
using static SurvLine.mdl.MdlNSGUI;
using static SurvLine.mdl.MdlGUI;
using static SurvLine.mdl.MdlNSSDefine;
using static SurvLine.mdl.MdiVBfunctions;
using static SurvLine.mdl.MdlMain;
using static SurvLine.mdl.MdlNSUtility;

using SurvLine.mdl;


namespace SurvLine
{
    public partial class frmAttributeCommon : Form
    {
        //'*******************************************************************************
        //  '観測点情報の編集画面
        //
        //  Option Explicit
        //

        //==========================================================================================
        /*[VB]
            '*******************************************************************************
            '観測点情報の編集画面

            Option Explicit

            'プロパティ
            Public Result As Long 'ダイアログの結果。
            Public PointNumber As String '観測点番号。
            Public PointName As String '観測点名称。
            Public Fixed As Boolean '固定点フラグ。
            Public OldEditCode As EDITCODE_STYLE '旧編集コード。

            'インプリメンテーション
            Private m_clsCoordinateDisplay As New CoordinatePointXYZ '表示座標。
            Private m_clsCoordinateFixed As New CoordinatePointFix '固定座標。
            Private m_bOptionBtn As Boolean 'オプションボタン制御フラグ。True=コードによる変更である。False=通常のイベントである。
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'プロパティ
        public long Result;         //'ダイアログの結果。
        public string PointNumber;  //'観測点番号。
        public string PointName;    //'観測点名称。
        public bool Fixed;                  //'固定点フラグ。
        //  public EDITCODE_STYLE OldEditCode;  //'旧編集コード。
        public int OldEditCode;  //'旧編集コード。

        //'インプリメンテーション
        private CoordinatePointXYZ m_clsCoordinateDisplay = new CoordinatePointXYZ();   //'表示座標。
        private CoordinatePointFix m_clsCoordinateFixed = new CoordinatePointFix();     //'固定座標。
        private bool m_bOptionBtn;  //'オプションボタン制御フラグ。True=コードによる変更である。False=通常のイベントである。


        //==========================================================================================
        //[C#]
        private MdlMain m_clsMdlMain;                                   //MdlMainインスタンス

        public frmAttributeCommon(MdlMain clsMdlMain)
        {
            InitializeComponent();
            m_clsMdlMain = clsMdlMain;
            this.Load += Form_Load;
            return;
        }
        //==========================================================================================
        //------------------------------------------------------------------------------------------
        //[C#]
        public frmAttributeCommon()
        {
            InitializeComponent();
            this.Load += Form_Load;

        }


        //==========================================================================================
        /*[VB]
            '*******************************************************************************
            'プロパティ

            '表示座標。
            Property Let CoordinateDisplay(ByVal clsCoordinatePoint As CoordinatePoint)
                Let m_clsCoordinateDisplay = clsCoordinatePoint
            End Property

            '固定座標。
            Property Let CoordinateFixed(ByVal clsCoordinatePointFix As CoordinatePointFix)
                Let m_clsCoordinateFixed = clsCoordinatePointFix
            End Property

            '固定座標。
            Property Get CoordinateFixed() As CoordinatePointFix
                Set CoordinateFixed = m_clsCoordinateFixed
            End Property
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'*******************************************************************************
        //'プロパティ
        //
        //'表示座標。
        public void CoordinateDisplay(CoordinatePoint clsCoordinatePoint)
        {
            m_clsCoordinateDisplay = (CoordinatePointXYZ)clsCoordinatePoint;
        }
        //
        //'固定座標。
        public void CoordinateFixed(CoordinatePointFix clsCoordinatePointFix)
        {
            m_clsCoordinateFixed = clsCoordinatePointFix;
        }
        //
        //'固定座標。
        public CoordinatePointFix CoordinateFixed()
        {
            return m_clsCoordinateFixed;
        }


        //'*******************************************************************************
        //'イベント
        //'*******************************************************************************

        //==========================================================================================
        /*[VB]
            '初期化。
            Private Sub Form_Load()

                On Error GoTo ErrorHandler
    
                '変数初期化。
                Result = vbCancel
                m_bOptionBtn = False
    
                '値の設定。
                txtNumber.Text = PointNumber
                txtName.Text = PointName
                chkFixed.Value = IIf(Fixed, 1, 0)
    
                If Not Fixed Then
                    'デフォルトはBL。
                    Dim sLatH As String
                    Dim sLatM As String
                    Dim sLatS As String
                    Dim sLonH As String
                    Dim sLonM As String
                    Dim sLonS As String
                    Dim nLatH As Long
                    Dim nLatM As Long
                    Dim nLatS As Double
                    Dim nLonH As Long
                    Dim nLonM As Long
                    Dim nLonS As Double
                    Dim nHeight As Double
                    Dim vAlt As Variant
                    Call m_clsCoordinateDisplay.GetDMS(nLatH, nLatM, nLatS, nLonH, nLonM, nLonS, nHeight, vAlt, GUI_SEC_DECIMAL, IIf(GetDocument().GeoidoEnable, GetDocument().GeoidoPath, ""))
                    sLatH = CStr(nLatH)
                    sLatM = CStr(nLatM)
                    sLatS = FormatRound0Trim(nLatS, GUI_SEC_DECIMAL)
                    sLonH = CStr(nLonH)
                    sLonM = CStr(nLonM)
                    sLonS = FormatRound0Trim(nLonS, GUI_SEC_DECIMAL)
                    'デフォルトは楕円体高。
                    Dim sHeight As String
                    sHeight = FormatRound(nHeight, GUI_HEIGHT_DECIMAL)
                    'デフォルトの固定座標。
                    Call m_clsCoordinateFixed.SetDMS(EDITCODE_VERT_HEIGHT, sLatH, sLatM, sLatS, sLonH, sLonM, sLonS, sHeight, IIf(GetDocument().GeoidoEnable, GetDocument().GeoidoPath, ""))
                ElseIf m_clsCoordinateFixed.EditCode = 0 Then
                    '編集コードが未設定の場合設定しておく。
                    If OldEditCode = 0 Then OldEditCode = EDITCODE_COORD_DMS Or EDITCODE_VERT_HEIGHT
                    Call m_clsCoordinateFixed.SetOldEditCode(OldEditCode, GetDocument().CoordNum, IIf(GetDocument().GeoidoEnable, GetDocument().GeoidoPath, ""))
                End If
    
                '座標。
                If (m_clsCoordinateFixed.EditCode And EDITCODE_COORD_XYZ) <> 0 Then
                    txtWGSX.Text = m_clsCoordinateFixed.EditX
                    txtWGSY.Text = m_clsCoordinateFixed.EditY
                    txtWGSZ.Text = m_clsCoordinateFixed.EditZ
                    optWGSXYZ.Value = True
                ElseIf (m_clsCoordinateFixed.EditCode And EDITCODE_COORD_DEG) <> 0 Then
                    Call d_to_dms_decimal(Val(m_clsCoordinateFixed.EditLat), nLatH, nLatM, nLatS, GUI_SEC_DECIMAL)
                    txtLatH.Text = CStr(nLatH)
                    txtLatM.Text = CStr(nLatM)
                    txtLatS.Text = FormatRound0Trim(nLatS, GUI_SEC_DECIMAL)
                    Call d_to_dms_decimal(Val(m_clsCoordinateFixed.EditLon), nLonH, nLonM, nLonS, GUI_SEC_DECIMAL)
                    txtLonH.Text = CStr(nLonH)
                    txtLonM.Text = CStr(nLonM)
                    txtLonS.Text = FormatRound0Trim(nLonS, GUI_SEC_DECIMAL)
                    optDMS.Value = True
                ElseIf (m_clsCoordinateFixed.EditCode And EDITCODE_COORD_DMS) <> 0 Then
                    Call m_clsCoordinateFixed.GetEditDMS(m_clsCoordinateFixed.EditLat, sLatH, sLatM, sLatS)
                    txtLatH.Text = sLatH
                    txtLatM.Text = sLatM
                    txtLatS.Text = sLatS
                    Call m_clsCoordinateFixed.GetEditDMS(m_clsCoordinateFixed.EditLon, sLonH, sLonM, sLonS)
                    txtLonH.Text = sLonH
                    txtLonM.Text = sLonM
                    txtLonS.Text = sLonS
                    optDMS.Value = True
                ElseIf (m_clsCoordinateFixed.EditCode And EDITCODE_COORD_JGD) <> 0 Then
                    If GetDocument().CoordNum <> m_clsCoordinateFixed.EditCoordNum Then
                        Dim nLat As Double
                        Dim nLon As Double
                        Call JGDxyz_to_WGS84dms(m_clsCoordinateFixed.EditCoordNum, Val(m_clsCoordinateFixed.EditX), Val(m_clsCoordinateFixed.EditY), 0, nLat, nLon, nHeight)
                        Dim nX As Double
                        Dim nY As Double
                        Dim nZ As Double
                        Call WGS84dms_to_JGDxyz(GetDocument().CoordNum, nLat, nLon, 0, nX, nY, nZ)
                        txtX.Text = FormatRound0Trim(nX, GUI_JGD_DECIMAL)
                        txtY.Text = FormatRound0Trim(nY, GUI_JGD_DECIMAL)
                    Else
                        txtX.Text = m_clsCoordinateFixed.EditX
                        txtY.Text = m_clsCoordinateFixed.EditY
                    End If
                    optJGD.Value = True
                Else
                    txtWGSX.Text = FormatRound0Trim(m_clsCoordinateFixed.X, GUI_XYZ_DECIMAL)
                    txtWGSY.Text = FormatRound0Trim(m_clsCoordinateFixed.Y, GUI_XYZ_DECIMAL)
                    txtWGSZ.Text = FormatRound0Trim(m_clsCoordinateFixed.Z, GUI_XYZ_DECIMAL)
                    optWGSXYZ.Value = True
                End If
    
                '高さ。
                m_bOptionBtn = True
                If (m_clsCoordinateFixed.EditCode And EDITCODE_VERT_ALT) <> 0 Then
                    If GetDocument().GeoidoEnable Then
                        '2012/05/30 H.Nakamura セミ・ダイナミックがONの場合は固定座標は標高で入力できない。''''''''''''''''''''''''''''''
                        'optAlt.Value = True
                        'If (m_clsCoordinateFixed.EditCode And EDITCODE_COORD_XYZ) = 0 Then txtHeight.Text = m_clsCoordinateFixed.EditHeight
                        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                        If GetDocument().SemiDynaEnable Then
                            optHeight.Value = True
                            If (m_clsCoordinateFixed.EditCode And EDITCODE_COORD_XYZ) = 0 Then
                                Call m_clsCoordinateFixed.GetDEG(nLat, nLon, nHeight, vAlt, "")
                                txtHeight.Text = FormatRound0Trim(nHeight, GUI_HEIGHT_DECIMAL)
                                Call MsgBox("セミ・ダイナミック補正が有効です。" & vbCrLf & "標高を楕円体高に変換します。これにより高さが若干ずれます。", vbExclamation)
                            End If
                        Else
                            optAlt.Value = True
                            If (m_clsCoordinateFixed.EditCode And EDITCODE_COORD_XYZ) = 0 Then txtHeight.Text = m_clsCoordinateFixed.EditHeight
                        End If
                        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                    Else
                        optHeight.Value = True
                        If (m_clsCoordinateFixed.EditCode And EDITCODE_COORD_XYZ) = 0 Then
                            Call m_clsCoordinateFixed.GetDEG(nLat, nLon, nHeight, vAlt, "")
                            txtHeight.Text = FormatRound0Trim(nHeight, GUI_HEIGHT_DECIMAL)
                            Call MsgBox("ジオイドモデルが指定されていません。" & vbCrLf & "標高を楕円体高に変換します。これにより高さが若干ずれます。", vbExclamation)
                        End If
                    End If
                Else
                    optHeight.Value = True
                    If (m_clsCoordinateFixed.EditCode And EDITCODE_COORD_XYZ) = 0 Then txtHeight.Text = m_clsCoordinateFixed.EditHeight
                End If
                m_bOptionBtn = False
    
                'コントロール初期化。
                '2012/05/30 H.Nakamura セミ・ダイナミックがONの場合は固定座標は標高で入力できない。''''''''''''''''''''''''''''''
                'optAlt.Enabled = GetDocument().GeoidoEnable
                '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                optAlt.Enabled = (GetDocument().GeoidoEnable And (Not GetDocument().SemiDynaEnable))
                '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    
                Call chkFixed_Click
    
                Exit Sub
    
            ErrorHandler:
                Call mdlMain.ErrorExit
    
            End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /// <summary>
        ///     初期化。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form_Load(object sender, EventArgs e)
        {

            try         //On Error GoTo ErrorHandler

            {
                //'変数初期化。
                Result = vbCancel;
                m_bOptionBtn = false;


                //'値の設定。
                txtNumber.Text = PointNumber;
                txtName.Text = PointName;
                chkFixed.Checked = Fixed;

                //'デフォルトはBL。
                string sLatH = "";
                string sLatM = "";
                string sLatS = "";
                string sLonH = "";
                string sLonM = "";
                string sLonS = "";
                long nLatH = 0;
                long nLatM = 0;
                double nLatS = 0;
                long nLonH = 0;
                long nLonM = 0;
                double nLonS = 0;
                double nHeight = 0;
                double vAlt = 0;    //As Variant
                if (!Fixed)
                {

                    //m_clsCoordinateDisplay.GetDMS(ref nLatH, nLatM, nLatS, nLonH, nLonM, nLonS, nHeight, ref vAlt, GUI_SEC_DECIMAL, m_clsMdlMain.GetDocument().GeoidoEnable, m_clsMdlMain.GetDocument().GeoidoPath, "");
                    _ = m_clsMdlMain.GetDocument().GeoidoEnable();  //2
                    m_clsCoordinateDisplay.GetDMS(ref nLatH, ref nLatM, ref nLatS, ref nLonH, ref nLonM, ref nLonS, ref nHeight, ref vAlt, GUI_SEC_DECIMAL, m_clsMdlMain.GetDocument().GeoidoPath());
                    sLatH = nLatH.ToString();
                    sLatM = nLatM.ToString();
                    sLatS = FormatRound0Trim(nLatS, GUI_SEC_DECIMAL);
                    sLonH = nLonH.ToString();
                    sLonM = nLonM.ToString();
                    sLonS = FormatRound0Trim(nLonS, GUI_SEC_DECIMAL);
                    //'デフォルトは楕円体高。
                    string sHeight = "";
                    sHeight = FormatRound(nHeight, GUI_HEIGHT_DECIMAL);
                    //'デフォルトの固定座標。
                    //  m_clsCoordinateFixed.SetDMS(EDITCODE_STYLE.EDITCODE_VERT_HEIGHT, sLatH, sLatM, sLatS, sLonH, sLonM, sLonS, sHeight, m_clsMdlMain.GetDocument().GeoidoEnable, m_clsMdlMain.GetDocument().GeoidoPath);
                    m_clsCoordinateFixed.SetDMS(EDITCODE_STYLE.EDITCODE_VERT_HEIGHT, sLatH, sLatM, sLatS, sLonH, sLonM, sLonS, sHeight, m_clsMdlMain.GetDocument().GeoidoPath());
                }
                else if(m_clsCoordinateFixed.EditCode() == 0)
                {
                    //'編集コードが未設定の場合設定しておく。
                    //If OldEditCode = 0 Then OldEditCode = EDITCODE_COORD_DMS Or EDITCODE_VERT_HEIGHT
                    if (OldEditCode == 0)
                    {
                        OldEditCode = (int)EDITCODE_STYLE.EDITCODE_COORD_DMS | (int)EDITCODE_STYLE.EDITCODE_VERT_HEIGHT;
                    }

                    //  m_clsCoordinateFixed.SetOldEditCode(OldEditCode, m_clsMdlMain.GetDocument().CoordNum, m_clsMdlMain.GetDocument().GeoidoEnable(), m_clsMdlMain.GetDocument().GeoidoPath);
                    m_clsCoordinateFixed.SetOldEditCode((EDITCODE_STYLE)OldEditCode, m_clsMdlMain.GetDocument().CoordNum(), m_clsMdlMain.GetDocument().GeoidoPath());


                }


                //'座標。
                if ((m_clsCoordinateFixed.EditCode() & (int)EDITCODE_STYLE.EDITCODE_COORD_XYZ) != 0)
                {
                    txtWGSX.Text = m_clsCoordinateFixed.EditX();
                    txtWGSY.Text = m_clsCoordinateFixed.EditY();
                    txtWGSZ.Text = m_clsCoordinateFixed.EditZ();
                    optWGSXYZ.Checked = true;

                }
                else if ((m_clsCoordinateFixed.EditCode() & (int)EDITCODE_STYLE.EDITCODE_COORD_DEG) != 0)
                {
                    //-------------------------------------------------------------------------------------------------
                    int deg = (int)nLatH;
                    int Min = (int)nLatM;
                    double Sec = nLatS;
                    int deci = (int)GUI_SEC_DECIMAL;
                    d_to_dms_decimal(double.Parse(m_clsCoordinateFixed.EditLat()), ref deg, ref Min, ref Sec, deci);
                    nLatH = deg;
                    nLatM = Min;
                    nLatS = Sec;
                    //-------------------------------------------------------------------------------------------------
                    txtLatH.Text = nLatH.ToString();
                    txtLatM.Text = nLatM.ToString();
                    txtLatS.Text = FormatRound0Trim(nLatS, GUI_SEC_DECIMAL);
                    //-------------------------------------------------------------------------------------------------
                    deg = (int)nLonH;
                    Min = (int)nLonM;
                    Sec = nLonS;
                    deci = (int)GUI_SEC_DECIMAL;
                    d_to_dms_decimal(double.Parse(m_clsCoordinateFixed.EditLon()), ref deg, ref Min, ref Sec, deci);
                    nLonH = deg;
                    nLonM = Min;
                    nLonS = Sec;
                    //-------------------------------------------------------------------------------------------------
                    txtLonH.Text = nLonH.ToString();
                    txtLonM.Text = nLonM.ToString();
                    txtLonS.Text = FormatRound0Trim(nLonS, GUI_SEC_DECIMAL);
                    optDMS.Checked = true;

                }
                else if ((m_clsCoordinateFixed.EditCode() & (int)EDITCODE_STYLE.EDITCODE_COORD_DMS) != 0)
                {
                    m_clsCoordinateFixed.GetEditDMS(m_clsCoordinateFixed.EditLat(), ref sLatH, ref sLatM, ref sLatS);
                    txtLatH.Text = sLatH.ToString();
                    txtLatM.Text = sLatM.ToString();
                    txtLatS.Text = sLatS.ToString();
                    m_clsCoordinateFixed.GetEditDMS(m_clsCoordinateFixed.EditLon(), ref sLonH, ref sLonM, ref sLonS);
                    txtLonH.Text = sLonH.ToString();
                    txtLonM.Text = sLonM.ToString();
                    txtLonS.Text = sLonS.ToString();
                    optDMS.Checked = true;

                }
                else if ((m_clsCoordinateFixed.EditCode() & (int)EDITCODE_STYLE.EDITCODE_COORD_JGD) != 0)
                {
                    if (m_clsMdlMain.GetDocument().CoordNum() != m_clsCoordinateFixed.EditCoordNum())
                    {
                        double nLat = 0;
                        double nLon = 0;
                        JGDxyz_to_WGS84dms((int)m_clsCoordinateFixed.EditCoordNum(), double.Parse(m_clsCoordinateFixed.EditX()), double.Parse(m_clsCoordinateFixed.EditY()), 0, ref nLat, ref nLon, ref nHeight);
                        double nX = 0;
                        double nY = 0;
                        double nZ = 0;
                        WGS84dms_to_JGDxyz((int)m_clsMdlMain.GetDocument().CoordNum(), nLat, nLon, 0, ref nX, ref nY, ref nZ);
                        txtX.Text = FormatRound0Trim(nX, GUI_JGD_DECIMAL);
                        txtY.Text = FormatRound0Trim(nY, GUI_JGD_DECIMAL);
                    }
                    else
                    {
                        txtX.Text = m_clsCoordinateFixed.EditX();
                        txtY.Text = m_clsCoordinateFixed.EditY();
                    }
                    optJGD.Checked = true;

                }
                else
                {
                    txtWGSX.Text = FormatRound0Trim(m_clsCoordinateFixed.X, GUI_XYZ_DECIMAL);
                    txtWGSY.Text = FormatRound0Trim(m_clsCoordinateFixed.Y, GUI_XYZ_DECIMAL);
                    txtWGSZ.Text = FormatRound0Trim(m_clsCoordinateFixed.Z, GUI_XYZ_DECIMAL);
                    optWGSXYZ.Checked = true;
                }

                //'高さ。
                m_bOptionBtn = true;
                if ((m_clsCoordinateFixed.EditCode() & (int)EDITCODE_STYLE.EDITCODE_VERT_ALT) != 0)
                {
                    if (m_clsMdlMain.GetDocument().GeoidoEnable())
                    {
                        //  '2012/05/30 H.Nakamura セミ・ダイナミックがONの場合は固定座標は標高で入力できない。''''''''''''''''''''''''''''''
                        //  'optAlt.Value = True
                        //  'If (m_clsCoordinateFixed.EditCode And EDITCODE_COORD_XYZ) = 0 Then txtHeight.Text = m_clsCoordinateFixed.EditHeight
                        //'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                        if (m_clsMdlMain.GetDocument().SemiDynaEnable())
                        {
                            optHeight.Checked = true;
                            if ((m_clsCoordinateFixed.EditCode() & (int)EDITCODE_STYLE.EDITCODE_COORD_XYZ) == 0)
                            {
                                double nLat = 0; double nLon = 0;
                                m_clsCoordinateFixed.GetDEG(ref nLat, ref nLon, ref nHeight, ref vAlt, "");
                                txtHeight.Text = FormatRound0Trim(nHeight, GUI_HEIGHT_DECIMAL);
                                _ = MessageBox.Show("セミ・ダイナミック補正が有効です。。" + vbCrLf + "標高を楕円体高に変換します。これにより高さが若干ずれます。", "エラー発生", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                            }
                        }
                        else
                        {
                            optAlt.Checked = true;
                            if ((m_clsCoordinateFixed.EditCode() & (int)EDITCODE_STYLE.EDITCODE_COORD_XYZ) == 0)
                            {
                                txtHeight.Text = m_clsCoordinateFixed.EditHeight();
                            }
                        }
                    }
                    else
                    {
                        optHeight.Checked = true;
                        if ((m_clsCoordinateFixed.EditCode() & (int)EDITCODE_STYLE.EDITCODE_COORD_XYZ) == 0)
                        {
                            double nLat = 0; double nLon = 0;
                            m_clsCoordinateFixed.GetDEG(ref nLat, ref nLon, ref nHeight, ref vAlt, "");

                            txtHeight.Text = FormatRound0Trim(nHeight, GUI_HEIGHT_DECIMAL);
                            _ = MessageBox.Show("ジオイドモデルが指定されていません。" + vbCrLf + "標高を楕円体高に変換します。これにより高さが若干ずれます。", "エラー発生", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                        }
                    }

                }
                else
                {
                    optHeight.Checked = true;
                    if ((m_clsCoordinateFixed.EditCode() & (int)EDITCODE_STYLE.EDITCODE_COORD_XYZ) == 0)
                    {
                        txtHeight.Text = m_clsCoordinateFixed.EditHeight();
                    }

                }

                m_bOptionBtn = false;

                //  'コントロール初期化。
                //  '2012/05/30 H.Nakamura セミ・ダイナミックがONの場合は固定座標は標高で入力できない。''''''''''''''''''''''''''''''
                //  'optAlt.Enabled = GetDocument().GeoidoEnable
                //'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                //2     optAlt.Enabled = m_clsMdlMain.GetDocument().GeoidoEnable() & (!m_clsMdlMain.GetDocument().SemiDynaEnable());
                optAlt.Enabled = false;
                if (m_clsMdlMain.GetDocument().GeoidoEnable())
                {
                    if (!m_clsMdlMain.GetDocument().SemiDynaEnable())
                    {
                        optAlt.Enabled = true;
                    }
                }

                //'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

                //chkFixed_Click();
                chkFixed_CheckedChanged(sender, e);

            }
            catch (Exception ex)
            {
                //ErrorHandler:
                //    Call mdlMain.ErrorExit
                _ = MessageBox.Show(ex.Message, "エラー発生", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        //==========================================================================================
        public static void SelectText(TextBox txtTextBox)    //2
        {
            _ = txtTextBox.Focus();
        }
        //==========================================================================================
        /*[VB]
            'テキストをすべて選択する。
            Private Sub txtNumber_GotFocus()

                On Error GoTo ErrorHandler
    
                Call SelectText(txtNumber)
    
                Exit Sub
    
            ErrorHandler:
                Call mdlMain.ErrorExit
    
            End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        ///2
        /// <summary>
        /// テキストをすべて選択する。
        /// </summary>
        private void txtNumber_GotFocus()
        {
            try
            {
                SelectText(txtNumber);
            }
            catch (Exception ex) 
            {
                _ = MessageBox.Show(ex.Message, "エラー発生", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //==========================================================================================
        /*[VB]
            'テキストをすべて選択する。
            Private Sub txtName_GotFocus()

                On Error GoTo ErrorHandler
    
                Call SelectText(txtName)
    
                Exit Sub
    
            ErrorHandler:
                Call mdlMain.ErrorExit
    
            End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //2
        /// <summary>
        /// テキストをすべて選択する。
        /// </summary>
        private void txtName_GotFocus()
        {
            try
            {
                SelectText(txtName);
            }
            catch (Exception ex)
            {
                _ = MessageBox.Show(ex.Message, "エラー発生", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //==========================================================================================
        /*[VB]
            'テキストをすべて選択する。
            Private Sub txtLatH_GotFocus()

                On Error GoTo ErrorHandler
    
                Call SelectText(txtLatH)
    
                Exit Sub
    
            ErrorHandler:
                Call mdlMain.ErrorExit
    
            End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //2
        /// <summary>
        /// テキストをすべて選択する。
        /// </summary>
        private void txtLatH_GotFocus()
        {
            try
            {
                SelectText(txtLatH);
            }
            catch (Exception ex)
            {
                _ = MessageBox.Show(ex.Message, "エラー発生", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //==========================================================================================
        /*[VB]
            'テキストをすべて選択する。
            Private Sub txtLatM_GotFocus()

                On Error GoTo ErrorHandler
    
                Call SelectText(txtLatM)
    
                Exit Sub
    
            ErrorHandler:
                Call mdlMain.ErrorExit
    
            End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //2
        /// <summary>
        /// テキストをすべて選択する。
        /// </summary>
        private void txtLatM_GotFocus()
        {
            try
            {
                SelectText(txtLatM);
            }
            catch (Exception ex)
            {
                _ = MessageBox.Show(ex.Message, "エラー発生", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //==========================================================================================
        /*[VB]
            'テキストをすべて選択する。
            Private Sub txtLatS_GotFocus()

                On Error GoTo ErrorHandler
    
                Call SelectText(txtLatS)
    
                Exit Sub
    
            ErrorHandler:
                Call mdlMain.ErrorExit
    
            End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //2
        /// <summary>
        /// テキストをすべて選択する。
        /// </summary>
        private void txtLatS_GotFocus()
        {
            try
            {
                SelectText(txtLatS);
            }
            catch (Exception ex)
            {
                _ = MessageBox.Show(ex.Message, "エラー発生", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //==========================================================================================
        /*[VB]
            'テキストをすべて選択する。
            Private Sub txtLonH_GotFocus()

                On Error GoTo ErrorHandler
    
                Call SelectText(txtLonH)
    
                Exit Sub
    
            ErrorHandler:
                Call mdlMain.ErrorExit
    
            End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //2
        /// <summary>
        /// テキストをすべて選択する。
        /// </summary>
        private void txtLonH_GotFocus()
        {
            try
            {
                SelectText(txtLonH);
            }
            catch (Exception ex)
            {
                _ = MessageBox.Show(ex.Message, "エラー発生", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //==========================================================================================
        /*[VB]
            'テキストをすべて選択する。
            Private Sub txtLonM_GotFocus()

                On Error GoTo ErrorHandler
    
                Call SelectText(txtLonM)
    
                Exit Sub
    
            ErrorHandler:
                Call mdlMain.ErrorExit
    
            End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //2
        /// <summary>
        /// テキストをすべて選択する。
        /// </summary>
        private void txtLonM_GotFocus()
        {
            try
            {
                SelectText(txtLonM);
            }
            catch (Exception ex)
            {
                _ = MessageBox.Show(ex.Message, "エラー発生", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //==========================================================================================
        /*[VB]
            'テキストをすべて選択する。
            Private Sub txtLonS_GotFocus()

                On Error GoTo ErrorHandler
    
                Call SelectText(txtLonS)
    
                Exit Sub
    
            ErrorHandler:
                Call mdlMain.ErrorExit
    
            End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //2
        /// <summary>
        /// テキストをすべて選択する。
        /// </summary>
        private void txtLonS_GotFocus()
        {
            try
            {
                SelectText(txtLonS);
            }
            catch (Exception ex)
            {
                _ = MessageBox.Show(ex.Message, "エラー発生", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //==========================================================================================
        /*[VB]
            'テキストをすべて選択する。
            Private Sub txtX_GotFocus()

                On Error GoTo ErrorHandler
    
                Call SelectText(txtX)
    
                Exit Sub
    
            ErrorHandler:
                Call mdlMain.ErrorExit
    
            End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //2
        /// <summary>
        /// テキストをすべて選択する。
        /// </summary>
        private void txtX_GotFocus()
        {
            try
            {
                SelectText(txtX);
            }
            catch (Exception ex)
            {
                _ = MessageBox.Show(ex.Message, "エラー発生", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //==========================================================================================
        /*[VB]
            'テキストをすべて選択する。
            Private Sub txtY_GotFocus()

                On Error GoTo ErrorHandler
    
                Call SelectText(txtY)
    
                Exit Sub
    
            ErrorHandler:
                Call mdlMain.ErrorExit
    
            End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //2
        /// <summary>
        /// テキストをすべて選択する。
        /// </summary>
        private void txtY_GotFocus()
        {
            try
            {
                SelectText(txtY);
            }
            catch (Exception ex)
            {
                _ = MessageBox.Show(ex.Message, "エラー発生", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //==========================================================================================
        /*[VB]
            'テキストをすべて選択する。
            Private Sub txtHeight_GotFocus()

                On Error GoTo ErrorHandler
    
                Call SelectText(txtHeight)
    
                Exit Sub
    
            ErrorHandler:
                Call mdlMain.ErrorExit
    
            End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //2
        /// <summary>
        /// テキストをすべて選択する。
        /// </summary>
        private void txtHeight_GotFocus()
        {
            try
            {
                SelectText(txtHeight);
            }
            catch (Exception ex)
            {
                _ = MessageBox.Show(ex.Message, "エラー発生", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //==========================================================================================
        /*[VB]
            'テキストをすべて選択する。
            Private Sub txtWGSX_GotFocus()

                On Error GoTo ErrorHandler
    
                Call SelectText(txtWGSX)
    
                Exit Sub
    
            ErrorHandler:
                Call mdlMain.ErrorExit
    
            End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //2
        /// <summary>
        /// テキストをすべて選択する。
        /// </summary>
        private void txtWGSX_GotFocus()
        {
            try
            {
                SelectText(txtWGSX);
            }
            catch (Exception ex)
            {
                _ = MessageBox.Show(ex.Message, "エラー発生", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //==========================================================================================
        /*[VB]
            'テキストをすべて選択する。
            Private Sub txtWGSY_GotFocus()

                On Error GoTo ErrorHandler
    
                Call SelectText(txtWGSY)
    
                Exit Sub
    
            ErrorHandler:
                Call mdlMain.ErrorExit
    
            End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //2
        /// <summary>
        /// テキストをすべて選択する。
        /// </summary>
        private void txtWGSY_GotFocus()
        {
            try
            {
                SelectText(txtWGSY);
            }
            catch (Exception ex)
            {
                _ = MessageBox.Show(ex.Message, "エラー発生", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //==========================================================================================
        /*[VB]
            'テキストをすべて選択する。
            Private Sub txtWGSZ_GotFocus()

                On Error GoTo ErrorHandler
    
                Call SelectText(txtWGSZ)
    
                Exit Sub
    
            ErrorHandler:
                Call mdlMain.ErrorExit
    
            End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //2
        /// <summary>
        /// テキストをすべて選択する。
        /// </summary>
        private void txtWGSZ_GotFocus()
        {
            try
            {
                SelectText(txtWGSZ);
            }
            catch (Exception ex)
            {
                _ = MessageBox.Show(ex.Message, "エラー発生", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        //==========================================================================================
        /*[VB]
             '固定点。
            Private Sub chkFixed_Click()

                On Error GoTo ErrorHandler
    
                If chkFixed.Value = 1 Then
                    optDMS.Enabled = True
                    optJGD.Enabled = True
                    optWGSXYZ.Enabled = True
                    fraChange.Enabled = True
                    lblFrom.Enabled = True
                    lblTo1.Enabled = True
                    lblTo2.Enabled = True
                    If optDMS.Value Then
                        Call optDMS_Click
                    ElseIf optJGD.Value Then
                        Call optJGD_Click
                    Else
                        Call optWGSXYZ_Click
                    End If
                Else
                    optDMS.Enabled = False
                    optJGD.Enabled = False
                    optWGSXYZ.Enabled = False
                    fraChange.Enabled = False
                    lblFrom.Enabled = False
                    lblTo1.Enabled = False
                    lblTo2.Enabled = False
                    Call EnableDMS(False)
                    Call EnableJGD(False)
                    Call EnableWGSXYZ(False)
                    optHeight.Enabled = False
                    optAlt.Enabled = False
                    txtHeight.Enabled = False
                    lblHeightUnit.Enabled = False
                    cmdChange.Enabled = False
                End If
    
                Exit Sub
    
            ErrorHandler:
                Call mdlMain.ErrorExit
    
            End Sub
           [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /// <summary>
        ///  '固定点。
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkFixed_CheckedChanged(object sender, EventArgs e)
        {

            if (chkFixed.Checked)
            {
                optDMS.Enabled = true;
                optJGD.Enabled = true;
                optWGSXYZ.Enabled = true;
                fraChange.Enabled = true;
                lblFrom.Enabled = true;
                lblTo1.Enabled = true;
                lblTo2.Enabled = true;

                if (optDMS.Checked)
                {
                    optDMS_CheckedChanged(sender, e);
                }
                else if (optJGD.Checked)
                {
                    optJGD_CheckedChanged(sender, e);
                }
                else
                {
                    optWGSXYZ_CheckedChanged(sender, e);
                }


            }
            else
            {
                optDMS.Enabled = false;
                optJGD.Enabled = false;
                optWGSXYZ.Enabled = false;
                fraChange.Enabled = false;
                lblFrom.Enabled = false;
                lblTo1.Enabled = false;
                lblTo2.Enabled = false;
                EnableDMS(false);
                EnableJGD(false);
                EnableWGSXYZ(false);
                optHeight.Enabled = false;
                optAlt.Enabled = false;
                txtHeight.Enabled = false;
                lblHeightUnit.Enabled = false;
                cmdChange.Enabled = false;

            }

        }

        //==========================================================================================
        /*[VB]
             '座標変換。
            Private Sub cmdChange_Click()

                On Error GoTo ErrorHandler
    
                If optDMS.Value Then
                    '入力値の検査。
                    If Not CheckDMS() Then Exit Sub
                    If optHeight.Value Then
                        '楕円体高。
                        If Not CheckHeight() Then Exit Sub
                    Else
                        '標高。
                        If Not CheckAlt() Then Exit Sub
                    End If
                    'DMS→XYH。
                    Dim nLat As Double
                    Dim nLon As Double
                    nLat = dms_to_d(Val(txtLatH.Text), Val(txtLatM.Text), Val(txtLatS.Text))
                    nLon = dms_to_d(Val(txtLonH.Text), Val(txtLonM.Text), Val(txtLonS.Text))
                    Dim nX As Double
                    Dim nY As Double
                    Dim nZ As Double
                    Call WGS84dms_to_JGDxyz(GetDocument().CoordNum, nLat, nLon, 0, nX, nY, nZ)
                    txtX.Text = FormatRound0Trim(nX, GUI_JGD_DECIMAL)
                    txtY.Text = FormatRound0Trim(nY, GUI_JGD_DECIMAL)
                    'DMS→XYZ。
                    Call WGS84dms_to_WGS84xyz(nLat, nLon, GetHeight(), nX, nY, nZ)
                    txtWGSX.Text = FormatRound0Trim(nX, GUI_XYZ_DECIMAL)
                    txtWGSY.Text = FormatRound0Trim(nY, GUI_XYZ_DECIMAL)
                    txtWGSZ.Text = FormatRound0Trim(nZ, GUI_XYZ_DECIMAL)
                ElseIf optJGD.Value Then
                    '入力値の検査。
                    If Not CheckJGD() Then Exit Sub
                    If optHeight.Value Then
                        '楕円体高。
                        If Not CheckHeight() Then Exit Sub
                    Else
                        '標高。
                        If Not CheckAlt() Then Exit Sub
                    End If
                    'XYH→DMS。
                    Dim nHeight As Double
                    Call JGDxyz_to_WGS84dms(GetDocument().CoordNum, Val(txtX.Text), Val(txtY.Text), 0, nLat, nLon, nHeight)
                    Dim nH As Long
                    Dim nM As Long
                    Dim nS As Double
                    Call d_to_dms_decimal(nLat, nH, nM, nS, GUI_SEC_DECIMAL)
                    txtLatH.Text = CStr(nH)
                    txtLatM.Text = CStr(nM)
                    txtLatS.Text = FormatRound0Trim(nS, GUI_SEC_DECIMAL)
                    Call d_to_dms_decimal(nLon, nH, nM, nS, GUI_SEC_DECIMAL)
                    txtLonH.Text = CStr(nH)
                    txtLonM.Text = CStr(nM)
                    txtLonS.Text = FormatRound0Trim(nS, GUI_SEC_DECIMAL)
                    'XYH→XYZ。
                    Call JGDxyz_to_WGS84xyz(GetDocument().CoordNum, Val(txtX.Text), Val(txtY.Text), GetHeight(), nX, nY, nZ)
                    txtWGSX.Text = FormatRound0Trim(nX, GUI_XYZ_DECIMAL)
                    txtWGSY.Text = FormatRound0Trim(nY, GUI_XYZ_DECIMAL)
                    txtWGSZ.Text = FormatRound0Trim(nZ, GUI_XYZ_DECIMAL)
                ElseIf optWGSXYZ.Value Then
                    '入力値の検査。
                    If Not CheckWGSXYZ() Then Exit Sub
                    'XYZ→DMS。
                    Call WGS84xyz_to_WGS84dms(Val(txtWGSX.Text), Val(txtWGSY.Text), Val(txtWGSZ.Text), nLat, nLon, nHeight)
                    Call d_to_dms_decimal(nLat, nH, nM, nS, GUI_SEC_DECIMAL)
                    txtLatH.Text = CStr(nH)
                    txtLatM.Text = CStr(nM)
                    txtLatS.Text = FormatRound0Trim(nS, GUI_SEC_DECIMAL)
                    Call d_to_dms_decimal(nLon, nH, nM, nS, GUI_SEC_DECIMAL)
                    txtLonH.Text = CStr(nH)
                    txtLonM.Text = CStr(nM)
                    txtLonS.Text = FormatRound0Trim(nS, GUI_SEC_DECIMAL)
                    'XYZ→XYH。
                    Call WGS84xyz_to_JGDxyz(GetDocument().CoordNum, Val(txtWGSX.Text), Val(txtWGSY.Text), Val(txtWGSZ.Text), nX, nY, nZ)
                    txtX.Text = FormatRound0Trim(nX, GUI_JGD_DECIMAL)
                    txtY.Text = FormatRound0Trim(nY, GUI_JGD_DECIMAL)
                    '高さ。
                    If optAlt.Value Then
                        txtHeight.Text = FormatRound0Trim(nHeight - GetGeoidoHeight(), GUI_HEIGHT_DECIMAL)
                    Else
                        txtHeight.Text = FormatRound0Trim(nHeight, GUI_HEIGHT_DECIMAL)
                    End If
                End If
    
                Exit Sub
    
            ErrorHandler:
                Call mdlMain.ErrorExit
    
            End Sub
           [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /// <summary>
        ///  '座標変換。[＞]ボタン
        ///  
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdChange_Click(object sender, EventArgs e)
        {

            try
            {
                if (optDMS.Checked)
                {
                    //'入力値の検査。
                    if (!CheckDMS())
                    {
                        return;
                    }

                    if (optHeight.Checked)
                    {
                        //'楕円体高。
                        if (!CheckHeight())
                        {
                            return;
                        }
                    }
                    else
                    {
                        //標高。
                        if (!CheckAlt())
                        {
                            return;
                        }
                    }

                    //'DMS→XYH。
                    double nLat = 0;
                    double nLon = 0;
                    nLat = dms_to_d(int.Parse(txtLatH.Text), int.Parse(txtLatM.Text), double.Parse(txtLatS.Text));
                    nLon = dms_to_d(int.Parse(txtLonH.Text), int.Parse(txtLonM.Text), double.Parse(txtLonS.Text));

                    double nX = 0;
                    double nY = 0;
                    double nZ = 0;
                    WGS84dms_to_JGDxyz((int)m_clsMdlMain.GetDocument().CoordNum(), nLat, nLon, 0, ref nX, ref nY, ref nZ);
                    txtX.Text = FormatRound0Trim(nX, GUI_JGD_DECIMAL);
                    txtY.Text = FormatRound0Trim(nY, GUI_JGD_DECIMAL);

                    //'DMS→XYZ。
                    WGS84dms_to_WGS84xyz(nLat, nLon, GetHeight(), ref nX, ref nY, ref nZ);
                    txtWGSX.Text = FormatRound0Trim(nX, GUI_XYZ_DECIMAL);
                    txtWGSY.Text = FormatRound0Trim(nY, GUI_XYZ_DECIMAL);
                    txtWGSZ.Text = FormatRound0Trim(nZ, GUI_XYZ_DECIMAL);

                }
                else if (optJGD.Checked)
                {
                    //'入力値の検査。
                    if (!CheckJGD())
                    {
                        return;
                    }

                    if (optHeight.Checked)
                    {
                        //'楕円体高。
                        if (!CheckHeight())
                        {
                            return;
                        }
                    }
                    else
                    {
                        //標高。
                        if (!CheckAlt())
                        {
                            return;
                        }
                    }
                    //'XYH→DMS。
                    /*
                        Dim nHeight As Double
                        Call JGDxyz_to_WGS84dms(GetDocument().CoordNum, Val(txtX.Text), Val(txtY.Text), 0, nLat, nLon, nHeight)
                    */
                    double nHeight = 0;
                    double nLat = 0;
                    double nLon = 0;
                    JGDxyz_to_WGS84dms((int)m_clsMdlMain.GetDocument().CoordNum(), double.Parse(txtX.Text), double.Parse(txtY.Text), 0, ref nLat, ref nLon, ref nHeight);
                    //------------------------------------------------------------------------
                    /*
                        Dim nH As Long
                        Dim nM As Long
                        Dim nS As Double
                        Call d_to_dms_decimal(nLat, nH, nM, nS, GUI_SEC_DECIMAL)
                        txtLatH.Text = CStr(nH)
                        txtLatM.Text = CStr(nM)
                        txtLatS.Text = FormatRound0Trim(nS, GUI_SEC_DECIMAL)
                    */
                    double nH = 0;
                    double nM = 0;
                    double nS = 0;
                    int nH2 = (int)nH;
                    int nM2 = (int)nM;
                    d_to_dms_decimal(nLat, ref nH2, ref nM2, ref nS, (int)GUI_SEC_DECIMAL);
                    nH = nH2;
                    nM = nM2;
                    txtLatH.Text = nH.ToString();
                    txtLatM.Text = nM.ToString();
                    txtLatS.Text = FormatRound0Trim(nS, GUI_SEC_DECIMAL);
                    /*
                        Call d_to_dms_decimal(nLon, nH, nM, nS, GUI_SEC_DECIMAL)
                        txtLonH.Text = CStr(nH)
                        txtLonM.Text = CStr(nM)
                        txtLonS.Text = FormatRound0Trim(nS, GUI_SEC_DECIMAL)
                    */
                    nH2 = (int)nH;
                    nM2 = (int)nM;
                    d_to_dms_decimal(nLon, ref nH2, ref nM2, ref nS, (int)GUI_SEC_DECIMAL);
                    nH = nH2;
                    nM = nM2;
                    txtLonH.Text = nH.ToString();
                    txtLonM.Text = nM.ToString();
                    txtLonS.Text = FormatRound0Trim(nS, GUI_SEC_DECIMAL);
                    /*
                        'XYH→XYZ。
                        Call JGDxyz_to_WGS84xyz(GetDocument().CoordNum, Val(txtX.Text), Val(txtY.Text), GetHeight(), nX, nY, nZ)
                        txtWGSX.Text = FormatRound0Trim(nX, GUI_XYZ_DECIMAL)
                        txtWGSY.Text = FormatRound0Trim(nY, GUI_XYZ_DECIMAL)
                        txtWGSZ.Text = FormatRound0Trim(nZ, GUI_XYZ_DECIMAL)
                    */
                    //'XYH→XYZ。
                    double nX = 0;
                    double nY = 0;
                    double nZ = 0;
                    JGDxyz_to_WGS84xyz(m_clsMdlMain.GetDocument().CoordNum(), double.Parse(txtX.Text), double.Parse(txtY.Text), GetHeight(), ref nX, ref nY, ref nZ);
                    txtWGSX.Text = FormatRound0Trim(nX, GUI_XYZ_DECIMAL);
                    txtWGSY.Text = FormatRound0Trim(nY, GUI_XYZ_DECIMAL);
                    txtWGSZ.Text = FormatRound0Trim(nZ, GUI_XYZ_DECIMAL);

                }
                else if (optWGSXYZ.Checked)
                {
                    //'入力値の検査。
                    if (!CheckWGSXYZ())
                    {
                        return;
                    }

                    //'XYZ→DMS。
                    //------------------------------------------------------------------------
                    /*
                        Call WGS84xyz_to_WGS84dms(Val(txtWGSX.Text), Val(txtWGSY.Text), Val(txtWGSZ.Text), nLat, nLon, nHeight)
                    */
                    double nHeight = 0;
                    double nLat = 0;
                    double nLon = 0;
                    WGS84xyz_to_WGS84dms(double.Parse(txtWGSX.Text), double.Parse(txtWGSY.Text), double.Parse(txtWGSZ.Text), ref nLat, ref nLon, ref nHeight);
                    /*
                        Call d_to_dms_decimal(nLat, nH, nM, nS, GUI_SEC_DECIMAL)
                        txtLatH.Text = CStr(nH)
                        txtLatM.Text = CStr(nM)
                        txtLatS.Text = FormatRound0Trim(nS, GUI_SEC_DECIMAL)
                    */
                    double nH = 0;
                    double nM = 0;
                    double nS = 0;
                    int nH2 = (int)nH;
                    int nM2 = (int)nM;
                    d_to_dms_decimal(nLat, ref nH2, ref nM2, ref nS, (int)GUI_SEC_DECIMAL);
                    nH = nH2;
                    nM = nM2;
                    txtLatH.Text = nH.ToString();
                    txtLatM.Text = nM.ToString();
                    txtLatS.Text = FormatRound0Trim(nS, GUI_SEC_DECIMAL);

                    /*
                        Call d_to_dms_decimal(nLon, nH, nM, nS, GUI_SEC_DECIMAL)
                        txtLonH.Text = CStr(nH)
                        txtLonM.Text = CStr(nM)
                        txtLonS.Text = FormatRound0Trim(nS, GUI_SEC_DECIMAL)
                    */
                    nH2 = (int)nH;
                    nM2 = (int)nM;
                    d_to_dms_decimal(nLon, ref nH2, ref nM2, ref nS, (int)GUI_SEC_DECIMAL);
                    nH = nH2;
                    nM = nM2;
                    txtLonH.Text = nH.ToString();
                    txtLonM.Text = nM.ToString();
                    txtLonS.Text = FormatRound0Trim(nS, GUI_SEC_DECIMAL);

                    /*
                        'XYZ→XYH。
                        Call WGS84xyz_to_JGDxyz(GetDocument().CoordNum, Val(txtWGSX.Text), Val(txtWGSY.Text), Val(txtWGSZ.Text), nX, nY, nZ)
                        txtX.Text = FormatRound0Trim(nX, GUI_JGD_DECIMAL)
                        txtY.Text = FormatRound0Trim(nY, GUI_JGD_DECIMAL)
                    */
                    //'XYZ→XYH。
                    double nX = 0;
                    double nY = 0;
                    double nZ = 0;
                    WGS84xyz_to_JGDxyz(m_clsMdlMain.GetDocument().CoordNum(), double.Parse(txtWGSX.Text), double.Parse(txtWGSY.Text), double.Parse(txtWGSZ.Text), ref nX, ref nY, ref nZ);
                    txtX.Text = FormatRound0Trim(nX, GUI_JGD_DECIMAL);
                    txtY.Text = FormatRound0Trim(nY, GUI_JGD_DECIMAL);

                    /*
                        '高さ。
                        If optAlt.Value Then
                            txtHeight.Text = FormatRound0Trim(nHeight - GetGeoidoHeight(), GUI_HEIGHT_DECIMAL)
                        Else
                            txtHeight.Text = FormatRound0Trim(nHeight, GUI_HEIGHT_DECIMAL)
                        End If
                    */
                    //'高さ。
                    if (optAlt.Checked)
                    {
                        txtHeight.Text = FormatRound0Trim(nHeight - GetGeoidoHeight(), GUI_HEIGHT_DECIMAL);
                    }
                    else
                    {
                        txtHeight.Text = FormatRound0Trim(nHeight, GUI_HEIGHT_DECIMAL);
                    }

                }

            }
            catch (Exception ex)
            {
                //ErrorHandler:
                //    Call mdlMain.ErrorExit
                _ = MessageBox.Show(ex.Message, "エラー発生", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }





        }

        //==========================================================================================
        /*[VB]
            '緯度経度。
            Private Sub optDMS_Click()

                On Error GoTo ErrorHandler
    
                '緯度経度入力を有効にする。
                lblFrom.Caption = optDMS.Caption
                lblTo1.Caption = optJGD.Caption
                lblTo2.Caption = optWGSXYZ.Caption
                Call EnableDMS(True)
                Call EnableJGD(False)
                Call EnableWGSXYZ(False)
    
                Exit Sub
    
            ErrorHandler:
                Call mdlMain.ErrorExit
    
            End Sub
            [VB]*/
                    //------------------------------------------------------------------------------------------
                    //[C#]
                    /// <summary>
                    /// 緯度経度
                    /// 
                    /// </summary>
                    /// <param name="sender"></param>
                    /// <param name="e"></param>
                    /// 
                    private void optDMS_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                //'緯度経度入力を有効にする。
                lblFrom.Text = optDMS.Text;
                lblTo1.Text = optJGD.Text;
                lblTo2.Text = optWGSXYZ.Text;
                EnableDMS(true);
                EnableJGD(false);
                EnableWGSXYZ(false);
            }
            catch (Exception ex)
            {
                //ErrorHandler:
                //    Call mdlMain.ErrorExit
                _ = MessageBox.Show(ex.Message, "エラー発生", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //==========================================================================================
        /*[VB]
            '平面直角。
            Private Sub optJGD_Click()

                On Error GoTo ErrorHandler
    
                '平面直角入力を有効にする。
                lblFrom.Caption = optJGD.Caption
                lblTo1.Caption = optDMS.Caption
                lblTo2.Caption = optWGSXYZ.Caption
                Call EnableDMS(False)
                Call EnableJGD(True)
                Call EnableWGSXYZ(False)
    
                Exit Sub
    
            ErrorHandler:
                Call mdlMain.ErrorExit
    
            End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /// <summary>
        /// 平面直角。
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void optJGD_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                //'平面直角入力を有効にする。
                lblFrom.Text = optJGD.Text;
                lblTo1.Text = optDMS.Text;
                lblTo2.Text = optWGSXYZ.Text;
                EnableDMS(false);
                EnableJGD(true);
                EnableWGSXYZ(false);
            }
            catch (Exception ex)
            {
                //ErrorHandler:
                //    Call mdlMain.ErrorExit
                _ = MessageBox.Show(ex.Message, "エラー発生", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        //==========================================================================================
        /*[VB]
             '地心直交
            Private Sub optWGSXYZ_Click()

                On Error GoTo ErrorHandler
    
                '地心直交入力を有効にする。
                lblFrom.Caption = optWGSXYZ.Caption
                lblTo1.Caption = optJGD.Caption
                lblTo2.Caption = optDMS.Caption
                Call EnableDMS(False)
                Call EnableJGD(False)
                Call EnableWGSXYZ(True)
    
                Exit Sub
    
            ErrorHandler:
                Call mdlMain.ErrorExit
    
            End Sub
           [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /// <summary>
        /// 地心直交
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void optWGSXYZ_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                //'地心直交入力を有効にする。
                lblFrom.Text = optWGSXYZ.Text;
                lblTo1.Text = optJGD.Text;
                lblTo2.Text = optDMS.Text;
                EnableDMS(false);
                EnableJGD(false);
                EnableWGSXYZ(true);
            }
            catch (Exception ex)
            {
                //ErrorHandler:
                //    Call mdlMain.ErrorExit
                _ = MessageBox.Show(ex.Message, "エラー発生", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //==========================================================================================
        /*[VB]
            '楕円体高。
            Private Sub optHeight_Click()

                On Error GoTo ErrorHandler
    
                'コードによる変更である場合何もしない。
                If m_bOptionBtn Then Exit Sub
                '地心直交の場合何もしない。
                If optWGSXYZ.Value Then Exit Sub
    
                '標高の入力チェック。
                If Not CheckAlt() Then
                    Call CancelOptionBtn(optAlt)
                    Exit Sub
                End If
                '座標の入力チェック。
                If optDMS.Value Then
                    If Not CheckDMS() Then
                        Call CancelOptionBtn(optAlt)
                        Exit Sub
                    End If
                ElseIf optJGD.Value Then
                    If Not CheckJGD() Then
                        Call CancelOptionBtn(optAlt)
                        Exit Sub
                    End If
                End If
                '楕円体高に変換。
                txtHeight.Text = FormatRound0Trim(Val(txtHeight.Text) + GetGeoidoHeight(), GUI_HEIGHT_DECIMAL)
    
                Exit Sub
    
            ErrorHandler:
                Call mdlMain.ErrorExit
    
            End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /// <summary>
        /// 楕円体高。
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void optHeight_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                //'コードによる変更である場合何もしない。
                if (m_bOptionBtn)
                {
                    return;
                }
                //'地心直交の場合何もしない。
                if (optWGSXYZ.Checked)
                {
                    return;
                }
                //'標高の入力チェック。
                if (!CheckAlt())
                {
                    CancelOptionBtn(optAlt);
                    return;
                }

                //'座標の入力チェック。
                if (optDMS.Checked)
                {
                    if (!CheckDMS()){
                        CancelOptionBtn(optAlt);
                        return;
                    }
                }
                else if (optJGD.Checked)
                {
                    if (!CheckJGD()) {
                        CancelOptionBtn(optAlt);
                        return;
                    }
                }

                //'楕円体高に変換。
                txtHeight.Text = FormatRound0Trim(double.Parse(txtHeight.Text) + GetGeoidoHeight(), GUI_HEIGHT_DECIMAL);

            }
            catch (Exception ex)
            {
                //ErrorHandler:
                //    Call mdlMain.ErrorExit
                _ = MessageBox.Show(ex.Message, "エラー発生", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //==========================================================================================
        /*[VB]
            '標高。
            Private Sub optAlt_Click()

                On Error GoTo ErrorHandler
    
                'コードによる変更である場合何もしない。
                If m_bOptionBtn Then Exit Sub
                '地心直交の場合何もしない。
                If optWGSXYZ.Value Then Exit Sub
    
                '楕円体高の入力チェック。
                If Not CheckHeight() Then
                    Call CancelOptionBtn(optHeight)
                    Exit Sub
                End If
                '座標の入力チェック。
                If optDMS.Value Then
                    If Not CheckDMS() Then
                        Call CancelOptionBtn(optHeight)
                        Exit Sub
                    End If
                ElseIf optJGD.Value Then
                    If Not CheckJGD() Then
                        Call CancelOptionBtn(optHeight)
                        Exit Sub
                    End If
                End If
                '標高に変換。
                txtHeight.Text = FormatRound0Trim(Val(txtHeight.Text) - GetGeoidoHeight(), GUI_HEIGHT_DECIMAL)
    
                Exit Sub
    
            ErrorHandler:
                Call mdlMain.ErrorExit
    
            End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /// <summary>
        /// 標高。
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void optAlt_CheckedChanged(object sender, EventArgs e)
        {
            try
            {

                //'コードによる変更である場合何もしない。
                if (m_bOptionBtn)
                {
                    return;
                }
                //'地心直交の場合何もしない。
                if (optWGSXYZ.Checked)
                {
                    return;
                }

                //'楕円体高の入力チェック。
                if (!CheckHeight())
                {
                    CancelOptionBtn(optHeight);
                    return;
                }

                //'座標の入力チェック。
                if (optDMS.Checked)
                {
                    if (!CheckDMS()) {
                        CancelOptionBtn(optHeight);
                        return;
                    }
                }
                else if (optJGD.Checked)
                {
                    if (!CheckJGD())
                    {
                        CancelOptionBtn(optHeight);
                        return;
                    }
                }

                //'標高に変換。
                txtHeight.Text = FormatRound0Trim(double.Parse(txtHeight.Text) - GetGeoidoHeight(), GUI_HEIGHT_DECIMAL);
            }
            catch (Exception ex)
            {
                //ErrorHandler:
                //    Call mdlMain.ErrorExit
                _ = MessageBox.Show(ex.Message, "エラー発生", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //==========================================================================================
        /*[VB]
            'キャンセルでダイアログを終了する。
            Private Sub CancelButton_Click()

                On Error GoTo ErrorHandler
    
                Call Unload(Me)
    
                Exit Sub
    
            ErrorHandler:
                Call mdlMain.ErrorExit

            End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /// <summary>
        /// キャンセルでダイアログを終了する。
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelButton_Click(object sender, EventArgs e)
        {
            try
            {
                Result = vbCancel;
                Close();

            }
            catch (Exception ex)
            {
                _ = MessageBox.Show(ex.Message, "エラー発生", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        //==========================================================================================
        /*[VB]
            'ＯＫでダイアログを終了する。
            Private Sub OKButton_Click()

                On Error GoTo ErrorHandler
    
                '入力値の検査。
                If Not CheckData() Then Exit Sub
                '値を反映させる。
                Call ReflectData
                '終了。
                Result = vbOK
                Call Unload(Me)
    
                Exit Sub
    
            ErrorHandler:
                Call mdlMain.ErrorExit

            End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        private void OKButton_Click(object sender, EventArgs e)
        {
            try
            {
                //'入力値の検査。
                if (!CheckData())
                {
                    return;
                }
                //'値を反映させる。
                ReflectData();
                //'終了。
                Result = vbOK;
                this.Close();

            }
            catch (Exception ex)
            {
                _ = MessageBox.Show(ex.Message, "エラー発生", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        //'*******************************************************************************
        //'インプリメンテーション


        //==========================================================================================
        /*[VB]
            '入力値を検査する。
            '
            '戻り値：
            '入力値が正常である場合 True を返す。
            'それ以外の場合 False を返す。
            Private Function CheckData() As Boolean

                CheckData = False
    
                '観測点番号。
                If Not CheckFileNameInputLength(txtNumber, "観測点No", RINEX_STR_OBSNUMBER - 1) Then Exit Function
                '値が変化していた場合、既存の観測点番号と重複しないようにする。
                If txtNumber.Text <> PointNumber Then
                    Dim clsChainList As ChainList
                    Set clsChainList = GetDocument().NetworkModel.RepresentPointHead
                    Do While Not clsChainList Is Nothing
                        Dim clsObservationPoint As ObservationPoint
                        Set clsObservationPoint = clsChainList.Element
                        If clsObservationPoint.PrevPoint Is Nothing And Not clsObservationPoint.Genuine Then
                            If clsObservationPoint.Number = txtNumber.Text Then
                                Call MsgBox("この観測点Noはすでに使用されています。", vbCritical)
                                Call txtNumber.SetFocus
                                Exit Function
                            End If
                        End If
                        Set clsChainList = clsChainList.NextList
                    Loop
                End If
    
                '観測点名称。
                If Not CheckInputLength(txtName, "観測点名称", RINEX_STR_OBSNAME - 1) Then Exit Function
    
                If chkFixed.Value = 1 Then
                    If optWGSXYZ.Value Then
                        '地心直交。
                        If Not CheckWGSXYZ() Then Exit Function
                    Else
                        '座標。
                        If optDMS.Value Then
                            '緯度経度。
                            If Not CheckDMS() Then Exit Function
                        ElseIf optJGD.Value Then
                            '平面直角。
                            If Not CheckJGD() Then Exit Function
                        End If
                        '高さ。
                        If optHeight.Value Then
                            '楕円体高。
                            If Not CheckHeight() Then Exit Function
                        Else
                            '標高。
                            If Not CheckAlt() Then Exit Function
                        End If
                    End If
                End If
    
                CheckData = True
    
            End Function
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /// <summary>
        /// '入力値を検査する。
        /// 
        /// </summary>
        /// <returns>
        /// '戻り値：
        ///     '入力値が正常である場合 True を返す。
        ///     'それ以外の場合 False を返す。
        /// </returns>
        private bool CheckData()
        {
            bool CheckData = false;

#if false
            //'観測点番号。
            if (!CheckFileNameInputLength(txtNumber, "観測点No", RINEX_STR_OBSNUMBER - 1))
            {
                return CheckData;
            }
#endif

#if false
                '観測点番号。
                If Not CheckFileNameInputLength(txtNumber, "観測点No", RINEX_STR_OBSNUMBER - 1) Then Exit Function
                '値が変化していた場合、既存の観測点番号と重複しないようにする。
                If txtNumber.Text <> PointNumber Then
                    Dim clsChainList As ChainList
                    Set clsChainList = GetDocument().NetworkModel.RepresentPointHead
                    Do While Not clsChainList Is Nothing
                        Dim clsObservationPoint As ObservationPoint
                        Set clsObservationPoint = clsChainList.Element
                        If clsObservationPoint.PrevPoint Is Nothing And Not clsObservationPoint.Genuine Then
                            If clsObservationPoint.Number = txtNumber.Text Then
                                Call MsgBox("この観測点Noはすでに使用されています。", vbCritical)
                                Call txtNumber.SetFocus
                                Exit Function
                            End If
                        End If
                        Set clsChainList = clsChainList.NextList
                    Loop
                End If

#endif
            // BBBBBBBBBBBBBBBBBBBBBBBBBBBB

            CheckData = true;
            return CheckData;
        }

        //==========================================================================================
        /*[VB]
            '緯度経度の入力値を検査する。
            '
            '戻り値：
            '入力値に問題がない場合 True を返す。
            'それ以外の場合 False を返す。
            Private Function CheckDMS() As Boolean
                CheckDMS = False
                If Not CheckIntegerInputRange(txtLatH, "緯度(度)", 0, 90) Then Exit Function
                If Not CheckIntegerInputRange(txtLatM, "緯度(分)", 0, 60) Then Exit Function
                If Not CheckFloatInputRange(txtLatS, "緯度(秒)", 0, 60) Then Exit Function
                If Not CheckIntegerInputRange(txtLonH, "経度(度)", 0, 180) Then Exit Function
                If Not CheckIntegerInputRange(txtLonM, "経度(分)", 0, 60) Then Exit Function
                If Not CheckFloatInputRange(txtLonS, "経度(秒)", 0, 60) Then Exit Function
                If Not CheckCoordRangeDMS(txtLatH, txtLatM, txtLatS, txtLonH, txtLonM, txtLonS, "緯度経度") Then Exit Function
                CheckDMS = True
            End Function
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /// <summary>
        /// '緯度経度の入力値を検査する。
        /// 
        /// </summary>
        /// <returns>
        /// '戻り値：
        ///     '入力値に問題がない場合 True を返す。
        ///     'それ以外の場合 False を返す。
        /// </returns>
        private bool CheckDMS()
        {
            bool CheckDMS = false;
            if (!CheckIntegerInputRange(txtLatH, "緯度(度)", 0, 90))
            {
                return CheckDMS;
            }
            if (!CheckIntegerInputRange(txtLatM, "緯度(分)", 0, 60))
            {
                return CheckDMS;
            }
            if (!CheckFloatInputRange(txtLatS, "緯度(秒)", 0, 60))
            {
                return CheckDMS;
            }

            if (!CheckIntegerInputRange(txtLonH, "経度(度)", 0, 180))
            {
                return CheckDMS;
            }
            if (!CheckIntegerInputRange(txtLonM, "経度(分)", 0, 60))
            {
                return CheckDMS;
            }
            if (!CheckFloatInputRange(txtLonS, "経度(秒)", 0, 60))
            {
                return CheckDMS;
            }

            if (!CheckCoordRangeDMS(txtLatH, txtLatM, txtLatS, txtLonH, txtLonM, txtLonS, "緯度経度"))
            {
                return CheckDMS;
            }

            CheckDMS = true;
            return CheckDMS;
        }


        //==========================================================================================
        /*[VB]
            '平面直角の入力値を検査する。
            '
            '戻り値：
            '入力値に問題がない場合 True を返す。
            'それ以外の場合 False を返す。
            Private Function CheckJGD() As Boolean
                CheckJGD = False
                If Not CheckFloatInput(txtX, "X") Then Exit Function
                If Not CheckFloatInput(txtY, "Y") Then Exit Function
                If Not CheckCoordRangeJGD(txtX, txtY, "平面直角座標") Then Exit Function
                CheckJGD = True
            End Function
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /// <summary>
        /// '平面直角の入力値を検査する。
        /// 
        /// </summary>
        /// <returns>
        /// '戻り値：
        /// '入力値に問題がない場合 True を返す。
        /// 'それ以外の場合 False を返す。
        /// </returns>
        private bool CheckJGD()
        {
            bool CheckJGD = false;

            if (!CheckFloatInput(txtX, "X"))
            {
                return CheckJGD;
            }
            if (!CheckFloatInput(txtY, "Y"))
            {
                return CheckJGD;
            }
            if (!CheckCoordRangeJGD(txtX, txtY, "平面直角座標"))
            {
                return CheckJGD;
            }

            CheckJGD = true;
            return CheckJGD;
        }


        //==========================================================================================
        /*[VB]
            '地心直交の入力値を検査する。
            '
            '戻り値：
            '入力値に問題がない場合 True を返す。
            'それ以外の場合 False を返す。
            Private Function CheckWGSXYZ() As Boolean
                CheckWGSXYZ = False
                If Not CheckFloatInput(txtWGSX, "X") Then Exit Function
                If Not CheckFloatInput(txtWGSY, "Y") Then Exit Function
                If Not CheckFloatInput(txtWGSZ, "Z") Then Exit Function
                If Not CheckCoordRangeXYZ(txtWGSX, txtWGSY, txtWGSZ, "地心直交座標") Then Exit Function
                CheckWGSXYZ = True
            End Function
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /// <summary>
        /// '地心直交の入力値を検査する。
        /// 
        /// </summary>
        /// <returns>
        /// '戻り値：
        /// '入力値に問題がない場合 True を返す。
        /// 'それ以外の場合 False を返す。
        /// </returns>
        private bool CheckWGSXYZ()
        {
            bool CheckWGSXYZ = false;

            if (!CheckFloatInput(txtWGSX, "X"))
            {
                return CheckWGSXYZ;
            }
            if (!CheckFloatInput(txtWGSY, "Y"))
            {
                return CheckWGSXYZ;
            }
            if (!CheckFloatInput(txtWGSZ, "Z"))
            {
                return CheckWGSXYZ;
            }
            if (!CheckCoordRangeXYZ(txtWGSX, txtWGSY, txtWGSZ, "地心直交座標"))
            {
                return CheckWGSXYZ;
            }

            CheckWGSXYZ = true;
            return CheckWGSXYZ;

        }

        //==========================================================================================
        /*[VB]
            '楕円体高の入力値を検査する。
            '
            '戻り値：
            '入力値に問題がない場合 True を返す。
            'それ以外の場合 False を返す。
            Private Function CheckHeight() As Boolean
                CheckHeight = False
                If Not CheckFloatInputRange(txtHeight, "楕円体高", COORD_MIN_HEIGHT, COORD_MAX_HEIGHT) Then Exit Function
                CheckHeight = True
            End Function
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /// <summary>
        /// '楕円体高の入力値を検査する。
        /// 
        /// </summary>
        /// <returns>
        /// '戻り値：
        /// '入力値に問題がない場合 True を返す。
        /// 'それ以外の場合 False を返す。
        /// </returns>
        private bool CheckHeight()
        {
            bool CheckHeight = false;

            if (!CheckFloatInputRange(txtHeight, "楕円体高", COORD_MIN_HEIGHT, COORD_MAX_HEIGHT)){
                return CheckHeight;
            }
            CheckHeight = true;
            return CheckHeight;
        }


        //==========================================================================================
        /*[VB]
            '標高の入力値を検査する。
            '
            '戻り値：
            '入力値に問題がない場合 True を返す。
            'それ以外の場合 False を返す。
            Private Function CheckAlt() As Boolean
                CheckAlt = False
                If Not CheckFloatInputRange(txtHeight, "標高", COORD_MIN_HEIGHT, COORD_MAX_HEIGHT) Then Exit Function
                CheckAlt = True
            End Function
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /// <summary>
        /// '標高の入力値を検査する。
        /// 
        /// </summary>
        /// <returns>
        /// '戻り値：
        /// '入力値に問題がない場合 True を返す。
        /// 'それ以外の場合 False を返す。
        /// </returns>
        private bool CheckAlt()
        {
            bool CheckAlt = false;

            if (!CheckFloatInputRange(txtHeight, "標高", COORD_MIN_HEIGHT, COORD_MAX_HEIGHT))
            {
                return CheckAlt;
            }
            CheckAlt = true;
            return CheckAlt;
        }

        //==========================================================================================
        /*[VB]
            '値を反映させる。
            Private Sub ReflectData()
                '値の取得。
                PointNumber = txtNumber.Text
                PointName = txtName.Text
                Fixed = (chkFixed.Value = 1)
                If Fixed Then Call GetCoordinatePoint(m_clsCoordinateFixed)
            End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /// <summary>
        /// '値を反映させる。
        /// 
        /// </summary>
        private void ReflectData()
        {
            PointNumber = txtNumber.Text;
            PointName = txtName.Text;
            Fixed = chkFixed.Checked;
            if (Fixed)
            {
                GetCoordinatePoint(m_clsCoordinateFixed);
            }
        }

        //==========================================================================================
        /*[VB]
            '地心座標を取得する。
            '
            '入力されている座標から地心座標を計算する。
            '
            '引き数：
            'clsCoordinatePointFix 地心座標が設定される。
            Private Sub GetCoordinatePoint(ByVal clsCoordinatePointFix As CoordinatePointFix)
                If optWGSXYZ.Value Then
                    Call clsCoordinatePointFix.SetXYZ(txtWGSX.Text, txtWGSY.Text, txtWGSZ.Text)
                Else
                    Dim nEditCode As EDITCODE_STYLE
                    If optHeight.Value Then
                        nEditCode = EDITCODE_VERT_HEIGHT
                    Else
                        nEditCode = EDITCODE_VERT_ALT
                    End If
                    If optDMS.Value Then
                        Call clsCoordinatePointFix.SetDMS(nEditCode, txtLatH.Text, txtLatM.Text, txtLatS.Text, txtLonH.Text, txtLonM.Text, txtLonS.Text, txtHeight.Text, IIf(GetDocument().GeoidoEnable, GetDocument().GeoidoPath, ""))
                    Else
                        Call clsCoordinatePointFix.SetJGD(nEditCode, GetDocument().CoordNum, txtX.Text, txtY.Text, txtHeight.Text, IIf(GetDocument().GeoidoEnable, GetDocument().GeoidoPath, ""))
                    End If
                End If
            End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /// <summary>
        /// '地心座標を取得する。
        /// '
        /// '入力されている座標から地心座標を計算する。
        /// '
        /// '引き数：
        /// 'clsCoordinatePointFix 地心座標が設定される。
        /// 
        /// </summary>
        /// <param name="clsCoordinatePointFix"></param>
        private void GetCoordinatePoint(CoordinatePointFix clsCoordinatePointFix)
        {
            if (optWGSXYZ.Checked)
            {
                clsCoordinatePointFix.SetXYZ(txtWGSX.Text, txtWGSY.Text, txtWGSZ.Text);
            }
            else
            {
                EDITCODE_STYLE nEditCode;
                if (optHeight.Checked)
                {
                    nEditCode = EDITCODE_STYLE.EDITCODE_VERT_HEIGHT;
                }
                else
                {
                    nEditCode = EDITCODE_STYLE.EDITCODE_VERT_ALT;
                }
                if (optDMS.Checked)
                {
                    //  clsCoordinatePointFix.SetDMS(nEditCode, txtLatH.Text, txtLatM.Text, txtLatS.Text, txtLonH.Text, txtLonM.Text, txtLonS.Text, txtHeight.Text, IIf(GetDocument().GeoidoEnable, GetDocument().GeoidoPath, ""))
                    clsCoordinatePointFix.SetDMS(nEditCode, txtLatH.Text, txtLatM.Text, txtLatS.Text, txtLonH.Text, txtLonM.Text, txtLonS.Text, txtHeight.Text, m_clsMdlMain.GetDocument().GeoidoPath());
                }
                else
                {
                    clsCoordinatePointFix.SetJGD(nEditCode, m_clsMdlMain.GetDocument().CoordNum(), txtX.Text, txtY.Text, txtHeight.Text, m_clsMdlMain.GetDocument().GeoidoPath());
                }

            }

        }

        //==========================================================================================
        /*[VB]
            '楕円体高を取得する。
            '
            '入力されている高さから楕円体高を計算する。
            '
            '戻り値：楕円体高(ｍ)を返す。
            Private Function GetHeight() As Double
                If optHeight.Value Then
                    GetHeight = Val(txtHeight.Text)
                Else
                    GetHeight = Val(txtHeight.Text) + GetGeoidoHeight()
                End If
            End Function
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /// <summary>
        /// '楕円体高を取得する。
        /// '
        /// '入力されている高さから楕円体高を計算する。
        /// 
        /// </summary>
        /// <returns>
        /// '戻り値：楕円体高(ｍ)を返す。
        /// </returns>
        private double GetHeight()
        {
            double GetHeight = 0;
            if (optHeight.Checked)
            {
                GetHeight = double.Parse(txtHeight.Text);

            }
            else
            {
                GetHeight = double.Parse(txtHeight.Text) + GetGeoidoHeight();
            }

            return GetHeight;
        }

        //==========================================================================================
        /*[VB]
            'ジオイド高を取得する。
            '
            '入力されている座標からジオイド高を計算する。
            '
            '戻り値：ジオイド高(ｍ)を返す。
            Private Function GetGeoidoHeight() As Double
                Dim nLat As Double
                Dim nLon As Double
                Call GetLatLon(nLat, nLon)
                Dim nGeo As Double
                If get_geo_height(GetDocument().GeoidoPath, nLat, nLon, nGeo) <> 0 Then
                    nGeo = 0
                    Call MsgBox("ジオイド高が取得できませんでした。" & vbCrLf & "ジオイド高は0とみなされます。", vbExclamation)
                End If
                GetGeoidoHeight = nGeo
            End Function
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /// <summary>
        /// 'ジオイド高を取得する。
        /// '
        /// '入力されている座標からジオイド高を計算する。
        /// 
        /// </summary>
        /// <returns>
        /// '戻り値：ジオイド高(ｍ)を返す。
        /// 
        /// </returns>
        private double GetGeoidoHeight()
        {
            double nLat = 0;
            double nLon = 0;
            GetLatLon(ref nLat, ref nLon);
            double nGeo = 0;
            if (get_geo_height(m_clsMdlMain.GetDocument().GeoidoPath(), nLat, nLon, ref nGeo) != 0)
            {
                nGeo = 0;
                //Call MsgBox("ジオイド高が取得できませんでした。" & vbCrLf & "ジオイド高は0とみなされます。", vbExclamation)
                _ = MessageBox.Show("ジオイド高が取得できませんでした。" + vbCrLf + "ジオイド高は0とみなされます。", "エラー発生", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            }
            return nGeo;
        }

        //==========================================================================================
        /*[VB]
            '緯度経度を取得する。
            '
            '入力されている座標から緯度経度を計算する。
            '
            '引き数：
            'nLat 緯度が設定される。
            'nLon 経度が設定される。
            Private Sub GetLatLon(ByRef nLat As Double, ByRef nLon As Double)
                If optDMS.Value Then
                    nLat = dms_to_d(Val(txtLatH.Text), Val(txtLatM.Text), Val(txtLatS.Text))
                    nLon = dms_to_d(Val(txtLonH.Text), Val(txtLonM.Text), Val(txtLonS.Text))
                Else
                    Dim nHeight As Double
                    Call JGDxyz_to_WGS84dms(GetDocument().CoordNum, Val(txtX.Text), Val(txtY.Text), 0, nLat, nLon, nHeight)
                End If
            End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /// <summary>
        /// '緯度経度を取得する。
        /// '
        /// '入力されている座標から緯度経度を計算する。
        /// '
        /// '引き数：
        /// 'nLat 緯度が設定される。
        /// 'nLon 経度が設定される。
        /// 
        /// </summary>
        /// <param name="nLat"></param>
        /// <param name="nLon"></param>
        private void GetLatLon(ref double nLat, ref double nLon)
        {

            if (optDMS.Checked)
            {
                nLat = dms_to_d(int.Parse(txtLatH.Text), int.Parse(txtLatM.Text), double.Parse(txtLatS.Text));
                nLon = dms_to_d(int.Parse(txtLonH.Text), int.Parse(txtLonM.Text), double.Parse(txtLonS.Text));
             }
            else
            {
                double nHeight = 0;
                JGDxyz_to_WGS84dms((int)m_clsMdlMain.GetDocument().CoordNum(), double.Parse(txtX.Text), double.Parse(txtY.Text), 0, ref nLat, ref nLon, ref nHeight);
            }

        }

        //==========================================================================================
        /*[VB]
            '緯度経度の有効/無効化。
            '
            'コントロールの有効/無効を設定する。
            '
            '引き数：
            'bEnable 有効フラグ。
            Private Sub EnableDMS(ByVal bEnable As Boolean)
                txtLatH.Enabled = bEnable
                txtLatM.Enabled = bEnable
                txtLatS.Enabled = bEnable
                txtLonH.Enabled = bEnable
                txtLonM.Enabled = bEnable
                txtLonS.Enabled = bEnable
                lblLat.Enabled = bEnable
                lblLatH.Enabled = bEnable
                lblLatM.Enabled = bEnable
                lblLatS.Enabled = bEnable
                lblLon.Enabled = bEnable
                lblLonH.Enabled = bEnable
                lblLonM.Enabled = bEnable
                lblLonS.Enabled = bEnable
                If bEnable Then
                    optHeight.Enabled = bEnable
                    '2012/05/30 H.Nakamura セミ・ダイナミックがONの場合は固定座標は標高で入力できない。''''''''''''''''''''''''''''''
                    'optAlt.Enabled = GetDocument().GeoidoEnable
                    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                    optAlt.Enabled = (GetDocument().GeoidoEnable And (Not GetDocument().SemiDynaEnable))
                    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                    txtHeight.Enabled = bEnable
                    lblHeightUnit.Enabled = bEnable
                    cmdChange.Enabled = bEnable
                End If
            End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /// <summary>
        /// '緯度経度の有効/無効化。
        /// '
        /// 'コントロールの有効/無効を設定する。
        /// '
        /// '引き数：
        /// 'bEnable 有効フラグ。
        /// 
        /// </summary>
        /// <param name="bEnable"></param>
        private void EnableDMS(bool bEnable)
        {
            txtLatH.Enabled = bEnable;
            txtLatM.Enabled = bEnable;
            txtLatS.Enabled = bEnable;
            txtLonH.Enabled = bEnable;
            txtLonM.Enabled = bEnable;
            txtLonS.Enabled = bEnable;
            lblLat.Enabled = bEnable;
            lblLatH.Enabled = bEnable;
            lblLatM.Enabled = bEnable;
            lblLatS.Enabled = bEnable;
            lblLon.Enabled = bEnable;
            lblLonH.Enabled = bEnable;
            lblLonM.Enabled = bEnable;
            lblLonS.Enabled = bEnable;

            if (bEnable)
            {
                optHeight.Enabled = bEnable;
                //  '2012/05/30 H.Nakamura セミ・ダイナミックがONの場合は固定座標は標高で入力できない。''''''''''''''''''''''''''''''
                //  'optAlt.Enabled = GetDocument().GeoidoEnable
                //'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                optAlt.Enabled = m_clsMdlMain.GetDocument().GeoidoEnable() & (!m_clsMdlMain.GetDocument().SemiDynaEnable());
                //'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                txtHeight.Enabled = bEnable;
                lblHeightUnit.Enabled = bEnable;
                cmdChange.Enabled = bEnable;
            }

        }

        //==========================================================================================
        /*[VB]
            '平面直角の有効/無効化。
            '
            'コントロールの有効/無効を設定する。
            '
            '引き数：
            'bEnable 有効フラグ。
            Private Sub EnableJGD(ByVal bEnable As Boolean)
                txtX.Enabled = bEnable
                txtY.Enabled = bEnable
                lblX.Enabled = bEnable
                lblY.Enabled = bEnable
                lblXUnit.Enabled = bEnable
                lblYUnit.Enabled = bEnable
                If bEnable Then
                    optHeight.Enabled = bEnable
                    '2012/05/30 H.Nakamura セミ・ダイナミックがONの場合は固定座標は標高で入力できない。''''''''''''''''''''''''''''''
                    'optAlt.Enabled = GetDocument().GeoidoEnable
                    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                    optAlt.Enabled = (GetDocument().GeoidoEnable And (Not GetDocument().SemiDynaEnable))
                    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                    txtHeight.Enabled = bEnable
                    lblHeightUnit.Enabled = bEnable
                    cmdChange.Enabled = bEnable
                End If
            End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /// <summary>
        /// '平面直角の有効/無効化。
        /// '
        /// 'コントロールの有効/無効を設定する。
        /// '
        /// '引き数：
        /// 'bEnable 有効フラグ。
        /// 
        /// </summary>
        /// <param name="bEnable"></param>
        private void EnableJGD(bool bEnable)
        {
            txtX.Enabled = bEnable;
            txtY.Enabled = bEnable;
            lblX.Enabled = bEnable;
            lblY.Enabled = bEnable;
            lblXUnit.Enabled = bEnable;
            lblYUnit.Enabled = bEnable;
            if (bEnable)
            {
                optHeight.Enabled = bEnable;
                //  '2012/05/30 H.Nakamura セミ・ダイナミックがONの場合は固定座標は標高で入力できない。''''''''''''''''''''''''''''''
                //  'optAlt.Enabled = GetDocument().GeoidoEnable
                //'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                optAlt.Enabled = m_clsMdlMain.GetDocument().GeoidoEnable() & (!m_clsMdlMain.GetDocument().SemiDynaEnable());
                //'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                txtHeight.Enabled = bEnable;
                lblHeightUnit.Enabled = bEnable;
                cmdChange.Enabled = bEnable;
            }

        }

        //==========================================================================================
        /*[VB]
            '地心直交の有効/無効化。
            '
            'コントロールの有効/無効を設定する。
            '
            '引き数：
            'bEnable 有効フラグ。
            Private Sub EnableWGSXYZ(ByVal bEnable As Boolean)
                txtWGSX.Enabled = bEnable
                txtWGSY.Enabled = bEnable
                txtWGSZ.Enabled = bEnable
                lblWGSX.Enabled = bEnable
                lblWGSY.Enabled = bEnable
                lblWGSZ.Enabled = bEnable
                lblWGSXUnit.Enabled = bEnable
                lblWGSYUnit.Enabled = bEnable
                lblWGSZUnit.Enabled = bEnable
                If bEnable Then
                    optHeight.Enabled = False
                    optAlt.Enabled = False
                    txtHeight.Enabled = False
                    lblHeightUnit.Enabled = False
                    cmdChange.Enabled = bEnable
                End If
            End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /// <summary>
        /// '地心直交の有効/無効化。
        /// '
        /// 'コントロールの有効/無効を設定する。
        /// '
        /// '引き数：
        /// 'bEnable 有効フラグ。
        /// </summary>
        /// <param name="bEnable"></param>
        private void EnableWGSXYZ(bool bEnable)
        {
            txtWGSX.Enabled = bEnable;
            txtWGSY.Enabled = bEnable;
            txtWGSZ.Enabled = bEnable;
            lblWGSX.Enabled = bEnable;
            lblWGSY.Enabled = bEnable;
            lblWGSZ.Enabled = bEnable;
            lblWGSXUnit.Enabled = bEnable;
            lblWGSYUnit.Enabled = bEnable;
            lblWGSZUnit.Enabled = bEnable;
            if (bEnable)
            {
                optHeight.Enabled = false;
                optAlt.Enabled = false;
                txtHeight.Enabled = false;
                lblHeightUnit.Enabled = false;
                cmdChange.Enabled = bEnable;
            }

        }

        //==========================================================================================
        /*[VB]
            'オプションボタンの変更をキャンセルする。
            '
            'オプションボタンが切り替わるのをキャンセルして、もとの選択にもどす。
            '
            '引き数：
            'optButton もともと選択されていたオプションボタン。
            Private Sub CancelOptionBtn(ByVal optButton As OptionButton)
                m_bOptionBtn = True
                optButton.Value = True
                m_bOptionBtn = False
            End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /// <summary>
        /// 'オプションボタンの変更をキャンセルする。
        /// '
        /// 'オプションボタンが切り替わるのをキャンセルして、もとの選択にもどす。
        /// '
        /// '引き数：
        /// 'optButton もともと選択されていたオプションボタン。
        /// 
        /// 
        /// </summary>
        /// <param name="optButton"></param>
        private void CancelOptionBtn(RadioButton optButton)
        {
            m_bOptionBtn = true;
            optButton.Checked = true;
            m_bOptionBtn = false;

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

    }
}
