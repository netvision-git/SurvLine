using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static SurvLine.mdl.DEFINE;
using static SurvLine.mdl.MdlNSDefine;
using static SurvLine.mdl.MdlUtility;
using static SurvLine.mdl.MdlAccountMake;
using static SurvLine.mdl.MdlNSGUI;
using static SurvLine.mdl.MdlNSSDefine;
using static SurvLine.mdl.MdiVBfunctions;
using static SurvLine.mdl.MdlMain;
using static SurvLine.mdl.MdlGUI;
using static SurvLine.mdl.MdlNSUtility;
using SurvLine.mdl;
using System.Runtime.Remoting;

namespace SurvLine
{
    public partial class frmInputCoordinate : Form
    {
        private MdlMain m_clsMdlMain;                                   //MdlMainインスタンス
        public frmInputCoordinate(MdlMain MdlMain)
        {
            InitializeComponent();
            m_clsMdlMain = MdlMain;
            Load += Form_Load;
        }



        //'*******************************************************************************
        //'観測点情報の編集画面
        //'*******************************************************************************

        //6==========================================================================================
        /*[VB]
            Option Explicit

            '観測点情報の編集画面

            'プロパティ
            public Result As Long 'ダイアログの結果。
            public PointNumber As String '観測点番号。
            public PointName As String '観測点名称。
            public EditCode As EDITCODE_STYLE '編集コード。0の時はデフォルト。
            public nLat As Double '緯度(度)。
            public nLon As Double '経度(度)。
            public nX As Double '平面直角X(ｍ)。
            public nY As Double '平面直角Y(ｍ)。
            public nHeight As Double '楕円体高(ｍ)。
            public nAlt As Double '標高(ｍ)。

            'Private m_clsCoordinateFixed As New CoordinatePointFix '固定座標。
            Private m_bOptionBtn As Boolean 'オプションボタン制御フラグ。True=コードによる変更である。False=通常のイベントである。
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#] //6
        /// <summary>
        /// プロパティ
        /// </summary>
        public long Result;             //ダイアログの結果。
        public string PointNumber;      //観測点番号。
        public string PointName;        //観測点名称。
        //      public EDITCODE_STYLE EditCode; //編集コード。0の時はデフォルト。
        public long EditCode;           //編集コード。0の時はデフォルト。
        public double nLat;             //緯度(度)。
        public double nLon;             //経度(度)。
        public double nX;               //平面直角X(ｍ)。
        public double nY;               //平面直角Y(ｍ)。
        public double nHeight;          //楕円体高(ｍ)。
        public double nAlt;             //標高(ｍ)。

        //  'Private m_clsCoordinateFixed As New CoordinatePointFix '固定座標。
        private bool m_bOptionBtn;      //オプションボタン制御フラグ。True=コードによる変更である。False=通常のイベントである。



        //6==========================================================================================
        /*[VB]
            '初期化。
            Private Sub Form_Load()

                On Error GoTo ErrorHandler
       
                '変数初期化。
                Result = vbCancel
                '値の設定。
                txtNumber.Text = PointNumber
                txtName.Text = PointName
    
                '座標。
                If (EditCode And EDITCODE_COORD_JGD) <> 0 Then
                    txtX.Text = FormatRound0Trim(nX, GUI_JGD_DECIMAL)
                    txtY.Text = FormatRound0Trim(nY, GUI_JGD_DECIMAL)
                    optJGD.Value = True
                ElseIf (EditCode And EDITCODE_COORD_DMS) <> 0 Then
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
                    optDMS.Value = True
                Else
                    'デフォルトはBL。
                    optDMS.Value = True
                End If
    
                '高さ。
                m_bOptionBtn = True
                If (EditCode And EDITCODE_VERT_ALT) <> 0 Then
                    If GetDocument().GeoidoEnable Then
                        optAlt.Value = True
                        txtHeight.Text = nAlt
                    Else
                        optHeight.Value = True
                        txtHeight.Text = nHeight
                        Call MsgBox("ジオイドモデルが指定されていません。" & vbCrLf & "標高を楕円体高に変更します。", vbExclamation)
                    End If
                Else
                    optHeight.Value = True
                    txtHeight.Text = nHeight
                End If
                m_bOptionBtn = False
    
                'コントロール初期化。
                optAlt.Enabled = GetDocument().GeoidoEnable
    
                Exit Sub
    
            ErrorHandler:
                Call mdlMain.ErrorExit
    
            End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#] //6
        private void Form_Load(object sender, EventArgs e)
        {
            try
            {
                //*********************
                //  変数初期化。
                //*********************
                Result = vbCancel;
                //*********************
                //  値の設定。
                //*********************
                txtNumber.Text = PointNumber;
                txtName.Text = PointName;

                //*********************
                //  座標。
                //*********************
                if ((EditCode & (long)EDITCODE_STYLE.EDITCODE_COORD_JGD) != 0)
                {
                    txtX.Text = FormatRound0Trim(nX, GUI_JGD_DECIMAL);
                    txtY.Text = FormatRound0Trim(nY, GUI_JGD_DECIMAL);
                }
                else if ((EditCode & (long)EDITCODE_STYLE.EDITCODE_COORD_DMS) != 0)
                {
                    int nH = 0;
                    int nM = 0;
                    double nS = 0;
                    d_to_dms_decimal(nLat, ref nH, ref nM, ref nS, (int)GUI_SEC_DECIMAL);
                    txtLatH.Text = nH.ToString();
                    txtLatM.Text = nM.ToString();
                    txtLatS.Text = FormatRound0Trim(nS, GUI_SEC_DECIMAL);
                    d_to_dms_decimal(nLon, ref nH, ref nM, ref nS, (int)GUI_SEC_DECIMAL);
                    txtLonH.Text = nH.ToString();
                    txtLonM.Text = nM.ToString(MESSAGE_CONTENT_EMPTY);
                    txtLonS.Text = FormatRound0Trim(nS, GUI_SEC_DECIMAL);
                    optDMS.Checked = true;
                }
                else
                {
                    //デフォルトはBL。
                    optDMS.Checked = true;
                }

                /*
                '高さ。
                m_bOptionBtn = True
                If (EditCode And EDITCODE_VERT_ALT) <> 0 Then
                    If GetDocument().GeoidoEnable Then
                        optAlt.Value = True
                        txtHeight.Text = nAlt
                    Else
                        optHeight.Value = True
                        txtHeight.Text = nHeight
                        Call MsgBox("ジオイドモデルが指定されていません。" & vbCrLf & "標高を楕円体高に変更します。", vbExclamation)
                    End If
                Else
                    optHeight.Value = True
                    txtHeight.Text = nHeight
                End If
                 */
                //*************
                //  高さ。
                //*************
                m_bOptionBtn = true;
                if ((EditCode & (long)EDITCODE_STYLE.EDITCODE_VERT_ALT) != 0)
                {
                    if (m_clsMdlMain.GetDocument().GeoidoEnable())
                    {
                        optAlt.Checked = true;
                        txtHeight.Text = nAlt.ToString();
                    }
                    else
                    {
                        optHeight.Checked = true;
                        txtHeight.Text = nHeight.ToString();
                        //  Call MsgBox("ジオイドモデルが指定されていません。" & vbCrLf & "標高を楕円体高に変更します。", vbExclamation)
                        _ = MessageBox.Show("ジオイドモデルが指定されていません。" + vbCrLf + "標高を楕円体高に変更します。", "確認", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    optHeight.Checked = true;
                    txtHeight.Text = nHeight.ToString();
                }

                m_bOptionBtn = false;



                //コントロール初期化。
                optAlt.Enabled = m_clsMdlMain.GetDocument().GeoidoEnable();



            }
            catch (Exception ex)
            {
                _ = MessageBox.Show(ex.Message, "エラー発生", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //------------------------------------------------------------------------------------------
        //------------------------------------------------------------------------------------------
        //[C#]
        /// <summary>
        /// テキストをすべて選択
        /// </summary>
        /// <param name="txtTextBox"></param>
        public void SelectText(TextBox txtTextBox)    //2
        {
            txtTextBox.Paste();
        }

        //6==========================================================================================
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
        //[C#] //6
        /// <summary>
        /// テキストをすべて選択する。
        /// 
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


        //6==========================================================================================
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
        //[C#] //6
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


        //6==========================================================================================
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
        //[C#] //6
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

        //6==========================================================================================
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
        //[C#] //6
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


        //6==========================================================================================
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
        //[C#] //6
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

        //6==========================================================================================
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
        //[C#] //6
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

        //6==========================================================================================
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
        //[C#] //6
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

        //6==========================================================================================
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
        //[C#] //6
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

        //6==========================================================================================
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
        //[C#] //6
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

        //6==========================================================================================
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
                End If
    
                Exit Sub
    
            ErrorHandler:
                Call mdlMain.ErrorExit
    
            End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#] //6
        private void cmdChange_Click(object sender, EventArgs e)
        {
            try
            {
                if (optDMS.Checked)
                {
                    //入力値の検査。
                    if (!CheckDMS())
                    {
                        return;
                    }

                    if (optHeight.Checked)
                    {
                        //楕円体高。
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
                    //**********************
                    //  DMS→XYH。
                    //**********************
                    double nLat;
                    double nLon;
                    nLat = dms_to_d(int.Parse(txtLatH.Text), int.Parse(txtLatM.Text), double.Parse(txtLatS.Text));
                    nLon = dms_to_d(int.Parse(txtLonH.Text), int.Parse(txtLonM.Text), double.Parse(txtLonS.Text));
                    double nX = 0;
                    double nY = 0;
                    double nZ = 0;
                    WGS84dms_to_JGDxyz((int)m_clsMdlMain.GetDocument().CoordNum(), nLat, nLon, 0, ref nX, ref nY, ref nZ);
                    txtX.Text = FormatRound0Trim(nX, GUI_JGD_DECIMAL);
                    txtY.Text = FormatRound0Trim(nY, GUI_JGD_DECIMAL);

                }
                else if (optJGD.Checked)
                {
                    //入力値の検査。
                    if (!CheckJGD())
                    {
                        return;
                    }
                    if (optHeight.Checked)
                    {
                        //楕円体高。
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

                    //**********************
                    //  XYH→DMS。
                    //**********************
                    double nHeight = 0;
                    JGDxyz_to_WGS84dms((int)m_clsMdlMain.GetDocument().CoordNum(), double.Parse(txtX.Text), double.Parse(txtY.Text), 0, ref nLat, ref nLon, ref nHeight);
                    int nH = 0;
                    int nM = 0;
                    double nS = 0;
                    d_to_dms_decimal(nLat, ref nH, ref  nM, ref nS, (int)GUI_SEC_DECIMAL);
                    txtLatH.Text = nH.ToString();
                    txtLatM.Text = nM.ToString();
                    txtLatS.Text = FormatRound0Trim(nS, GUI_SEC_DECIMAL);
                    d_to_dms_decimal(nLon, ref nH, ref nM, ref nS, (int)GUI_SEC_DECIMAL);
                    txtLonH.Text = nH.ToString();
                    txtLonM.Text = nM.ToString();
                    txtLonS.Text = FormatRound0Trim(nS, GUI_SEC_DECIMAL);
                }

            }
            catch (Exception ex)
            {
                _ = MessageBox.Show(ex.Message, "エラー発生", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        //6==========================================================================================
        /*[VB]
            '緯度経度。
            Private Sub optDMS_Click()

                On Error GoTo ErrorHandler
    
                '緯度経度入力を有効にする。
                cmdChange.Caption = "↓"
                Call EnableDMS(True)
                Call EnableJGD(False)
    
                Exit Sub
    
            ErrorHandler:
                Call mdlMain.ErrorExit
    
            End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#] //6
        /// <summary>
        /// 緯度経度。
        /// 
        /// </summary>
        private void optDMS_CheckedChanged(object sender, EventArgs e)
        {
            optDMS_Click();
        }
        private void optDMS_Click()
        {
            try
            {
                //緯度経度入力を有効にする。
                cmdChange.Text = "↓";
                EnableDMS(true);
                EnableJGD(false);
            }
            catch (Exception ex)
            {
                _ = MessageBox.Show(ex.Message, "エラー発生", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //6==========================================================================================
        /*[VB]
            '平面直角。
            Private Sub optJGD_Click()

                On Error GoTo ErrorHandler
    
                '平面直角入力を有効にする。
                cmdChange.Caption = "↑"
                Call EnableDMS(False)
                Call EnableJGD(True)
    
                Exit Sub
    
            ErrorHandler:
                Call mdlMain.ErrorExit
    
            End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#] //6
        /// <summary>
        /// 平面直角
        /// 
        /// </summary>
        private void optJGD_CheckedChanged(object sender, EventArgs e)
        {
            optJGD_Click();
        }
        private void optJGD_Click()
        {
            try
            {
                //平面直角入力を有効にする。
                cmdChange.Text = "↑";
                EnableDMS(false);
                EnableJGD(true);
            }
            catch (Exception ex)
            {
                _ = MessageBox.Show(ex.Message, "エラー発生", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        //6==========================================================================================
        /*[VB]
            '楕円体高。
            Private Sub optHeight_Click()

                On Error GoTo ErrorHandler
    
                'コードによる変更である場合何もしない。
                If m_bOptionBtn Then Exit Sub
    
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
        //[C#] //6
        /// <summary>
        /// 楕円体高。
        /// 
        /// </summary>
        private void optHeight_CheckedChanged(object sender, EventArgs e)
        {
            optHeight_Click();
        }
        private void optHeight_Click()
        {
            try
            {
                //コードによる変更である場合何もしない。
                if (m_bOptionBtn)
                {
                    return;
                }

                //標高の入力チェック。
                if (!CheckAlt())
                {
                    CancelOptionBtn(optAlt);
                    return; // Exit Sub
                }

                //座標の入力チェック。
                if (optDMS.Checked)
                {
                    if (!CheckDMS())
                    {
                        CancelOptionBtn(optAlt);
                        return; // Exit Sub
                    }
                }
                else if (optJGD.Checked)
                {
                    if (!CheckJGD())
                    {
                        CancelOptionBtn(optAlt);
                        return; // Exit Sub
                    }
                }
                //'楕円体高に変換。
                txtHeight.Text = FormatRound0Trim(double.Parse(txtHeight.Text) + GetGeoidoHeight(), GUI_HEIGHT_DECIMAL);

            }
            catch (Exception ex)
            {
                _ = MessageBox.Show(ex.Message, "エラー発生", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //6==========================================================================================
        /*[VB]
            '標高。
            Private Sub optAlt_Click()

                On Error GoTo ErrorHandler
    
                'コードによる変更である場合何もしない。
                If m_bOptionBtn Then Exit Sub
    
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
        //[C#] //6
        /// <summary>
        /// 標高。
        /// 
        /// </summary>
        private void optAlt_CheckedChanged(object sender, EventArgs e)
        {
            optAlt_Click();
        }
        private void optAlt_Click()
        {
            try
            {
                //コードによる変更である場合何もしない。
                if (m_bOptionBtn)
                {
                    return;
                }

                //楕円体高の入力チェック。
                if (!CheckHeight())
                {
                    CancelOptionBtn(optHeight);
                    return; // Exit Sub
                }

                //座標の入力チェック。
                if (optDMS.Checked)
                {
                    if (!CheckDMS())
                    {
                        CancelOptionBtn(optHeight);
                        return; // Exit Sub
                    }
                }
                else if (optJGD.Checked)
                {
                    if (!CheckJGD())
                    {
                        CancelOptionBtn(optHeight);
                        return; // Exit Sub
                    }
                }
                //標高に変換。
                txtHeight.Text = FormatRound0Trim(double.Parse(txtHeight.Text) - GetGeoidoHeight(), GUI_HEIGHT_DECIMAL);

            }
            catch (Exception ex)
            {
                _ = MessageBox.Show(ex.Message, "エラー発生", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //6==========================================================================================
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
        //[C#] //6
        /// <summary>
        /// キャンセルでダイアログを終了する。
        /// 
        /// </summary>
        private void CancelButton_Click(object sender, EventArgs e)
        {
            try
            {
                Close();     //Call Unload(Me)
            }
            catch (Exception ex)
            {
                _ = MessageBox.Show(ex.Message, "エラー発生", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //6==========================================================================================
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
        //[C#] //6
        /// <summary>
        /// ＯＫでダイアログを終了する。
        /// 
        /// </summary>
        private void OKButton_Click(object sender, EventArgs e)
        {

            try
            {
                //入力値の検査。
                if (!CheckData())
                {
                    return;
                }
                //値を反映させる。
                ReflectData();
                //終了。
                Result = vbOK;

                Close();    //Unload(this);
            }
            catch (Exception ex)
            {
                _ = MessageBox.Show(ex.Message, "エラー発生", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //'*******************************************************************************
        //'インプリメンテーション
        //'*******************************************************************************

        //[C#] 
        /// <summary>
        /// 入力値が空であるか検査する。    CheckInputEmptyの継承対応場合
        ///
        /// 引き数：
        /// txtTextBox 検査対象コントロール。
        /// sLabel 対象コントロールの名称。
        /// bFocus 検査に引っかかった場合フォーカスを移すか？
        ///
        /// </summary>
        /// <param name="txtTextBox"></param>
        /// <param name="sLabel"></param>
        /// <param name="bFocus"></param>
        /// <returns></returns>
        public static bool CheckInputEmpty(TextBox txtTextBox, string sLabel, bool bFocus)
        {

            bool CheckInputEmpty = false;

            //'空であるか？
            if (txtTextBox.Text.Length <= 0)
            {
                _ = MessageBox.Show($" {sLabel} {GUI_MSG_EMPTY}", "エラー発生", MessageBoxButtons.OK, MessageBoxIcon.Error); //2
                if (bFocus)
                {
                    _ = txtTextBox.Focus(); //2
                }
                return CheckInputEmpty;
            }

            CheckInputEmpty = true;
            return CheckInputEmpty;
        }


        //6==========================================================================================
        /*[VB]
            '入力値を検査する。
            '
            '戻り値：
            '入力値が正常である場合 True を返す。
            'それ以外の場合 False を返す。
            Private Function CheckData() As Boolean

                CheckData = False
    
                'No
                If Not CheckInputEmpty(txtNumber, "No") Then Exit Function
   
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
    
                CheckData = True
    
            End Function
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#] //6
        /// <summary>
        /// 入力値を検査する。
        /// 
        /// </summary>
        /// <returns>
        /// 戻り値：
        /// 入力値に問題がない場合 True を返す。
        /// それ以外の場合 False を返す。
        /// </returns>
        private bool CheckData()
        {
            bool CheckData = false;

            //'No
            if (!CheckInputEmpty(txtNumber, "No", true))
            {
                return CheckData;
            }

            //'座標。
            if (optDMS.Checked)
            {
                //'緯度経度。
                if (!CheckDMS())
                {
                    return CheckData;
                }
            }
            else if (optJGD.Checked)
            {
                //'平面直角。
                if (!CheckJGD())
                {
                    return CheckData;
                }
            }

            //'高さ。
            if (optHeight.Checked)
            {
                //'楕円体高。
                if (!CheckHeight())
                {
                    return CheckData;
                }
            }
            else
            {
                //'標高。
                if (!CheckAlt())
                {
                    return CheckData;
                }
            }
            CheckData = true;
            return CheckData;
        }

        //6==========================================================================================
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
        //[C#] //6
        /// <summary>
        /// 緯度経度の入力値を検査する。
        /// 
        /// </summary>
        /// <returns>
        /// 戻り値：
        /// 入力値に問題がない場合 True を返す。
        /// それ以外の場合 False を返す。
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

        //6==========================================================================================
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
        //[C#] //6
        /// <summary>
        /// 平面直角の入力値を検査する。
        /// 
        /// </summary>
        /// <returns>
        /// 戻り値：
        /// 入力値に問題がない場合 True を返す。
        /// それ以外の場合 False を返す。
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

        //6==========================================================================================
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
        //[C#] //6
        /// <summary>
        /// 楕円体高の入力値を検査する。
        /// 
        /// </summary>
        /// <returns>
        /// 戻り値：
        /// 入力値に問題がない場合 True を返す。
        /// それ以外の場合 False を返す。
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

        //6==========================================================================================
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
        //[C#] //6
        /// <summary>
        /// 標高の入力値を検査する。
        /// 
        /// </summary>
        /// <returns>
        /// 戻り値：
        /// 入力値に問題がない場合 True を返す。
        /// それ以外の場合 False を返す。
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

        //6==========================================================================================
        /*[VB]
            '値を反映させる。
            Private Sub ReflectData()
                '値の取得。
                Dim nEditCode As EDITCODE_STYLE
    
                PointNumber = txtNumber.Text
                PointName = txtName.Text
    
                If optHeight.Value Then
                    nEditCode = EDITCODE_VERT_HEIGHT
                    nHeight = CDbl(txtHeight.Text)
                Else
                    nEditCode = EDITCODE_VERT_ALT
                    nAlt = CDbl(txtHeight.Text)
                    nHeight = Val(txtHeight.Text) + GetGeoidoHeight()
                End If
    
                If optDMS.Value Then
                    EditCode = nEditCode + EDITCODE_COORD_DMS
                    nLat = dms_to_d(CLng(txtLatH.Text), CLng(txtLatM.Text), CDbl(txtLatS.Text))
                    nLon = dms_to_d(CLng(txtLonH.Text), CLng(txtLonM.Text), CDbl(txtLonS.Text))
                Else
                    EditCode = nEditCode + EDITCODE_COORD_JGD
                    nX = CDbl(txtX.Text)
                    nY = CDbl(txtY.Text)
                    Dim dHeight As Double
                    Call JGDxyz_to_WGS84dms(GetDocument().CoordNum, nX, nY, 0, nLat, nLon, dHeight)
                End If
            End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#] //6
        /// <summary>
        /// 値を反映させる。
        /// 
        /// </summary>
        private void ReflectData()
        {
            //値の取得。
            long nEditCode;

            PointNumber = txtNumber.Text;
            PointName = txtName.Text;

            if (optHeight.Checked)
            {
                nEditCode = (long)EDITCODE_STYLE.EDITCODE_VERT_HEIGHT;
                nHeight = double.Parse(txtHeight.Text);
            }
            else
            {
                nEditCode = (long)EDITCODE_STYLE.EDITCODE_VERT_ALT;
                nAlt = double.Parse(txtHeight.Text);
                nHeight = double.Parse(txtHeight.Text) + GetGeoidoHeight();
            }

            if (optDMS.Checked)
            {
                EditCode = nEditCode + (long)EDITCODE_STYLE.EDITCODE_COORD_DMS;
                nLat = dms_to_d(int.Parse(txtLatH.Text), int.Parse(txtLatM.Text), double.Parse(txtLatS.Text));
                nLon = dms_to_d(int.Parse(txtLonH.Text), int.Parse(txtLonM.Text), double.Parse(txtLonS.Text));
            }
            else
            {
                EditCode = nEditCode + (long)EDITCODE_STYLE.EDITCODE_COORD_JGD;
                nX = double.Parse(txtX.Text);
                nY = double.Parse(txtY.Text);
                double dHeight = 0;
                JGDxyz_to_WGS84dms((int)m_clsMdlMain.GetDocument().CoordNum(), nX, nY, 0, ref nLat, ref nLon, ref dHeight);
            }
        }

        //6==========================================================================================
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
        //[C#] //6
        /// <summary>
        /// 楕円体高を取得する。
        /// '
        /// 入力されている高さから楕円体高を計算する。
        /// 
        /// </summary>
        /// <returns>
        /// 戻り値：楕円体高(ｍ)を返す。
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

        //6==========================================================================================
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
        //[C#] //6
        /// <summary>
        /// ジオイド高を取得する。
        /// '
        /// 入力されている座標からジオイド高を計算する。
        /// 
        /// </summary>
        /// <returns>
        /// 戻り値：ジオイド高(ｍ)を返す。
        /// </returns>
        private double GetGeoidoHeight()
        {
            double nLat = 0;
            double nLon = 0;
            GetLatLon(ref nLat,ref nLon);
            double nGeo = 0;
            if (get_geo_height(m_clsMdlMain.GetDocument().GeoidoPath(), nLat, nLon, ref nGeo) != 0)
            {
                nGeo = 0;
                //Call MsgBox("ジオイド高が取得できませんでした。" & vbCrLf & "ジオイド高は0とみなされます。", vbExclamation)
                _ = MessageBox.Show("ジオイド高が取得できませんでした。" + vbCrLf + "ジオイド高は0とみなされます。", "注意", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            return nGeo;
        }

        //6==========================================================================================
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
                    optAlt.Enabled = GetDocument().GeoidoEnable
                    txtHeight.Enabled = bEnable
                    lblHeightUnit.Enabled = bEnable
                    cmdChange.Enabled = bEnable
                End If
            End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#] //6
        /// <summary>
        /// 緯度経度の有効/無効化。
        /// '
        /// コントロールの有効/無効を設定する。
        /// '
        /// 引き数：
        /// bEnable 有効フラグ。
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
                optAlt.Enabled = m_clsMdlMain.GetDocument().GeoidoEnable();
                txtHeight.Enabled = bEnable;
                lblHeightUnit.Enabled = bEnable;
                cmdChange.Enabled = bEnable;
            }

        }

        //6==========================================================================================
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
                    optAlt.Enabled = GetDocument().GeoidoEnable
                    txtHeight.Enabled = bEnable
                    lblHeightUnit.Enabled = bEnable
                    cmdChange.Enabled = bEnable
                End If
            End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#] //6
        /// <summary>
        /// 平面直角の有効/無効化。
        /// '
        /// コントロールの有効/無効を設定する。
        /// '
        /// 引き数：
        /// bEnable 有効フラグ。
        /// /
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
                optAlt.Enabled = m_clsMdlMain.GetDocument().GeoidoEnable();
                txtHeight.Enabled = bEnable;
                lblHeightUnit.Enabled = bEnable;
                cmdChange.Enabled = bEnable;
            }
        }

        //6==========================================================================================
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
        //[C#] //6
        /// <summary>
        /// 緯度経度を取得する。
        /// '
        /// 入力されている座標から緯度経度を計算する。
        /// '
        /// 引き数：
        /// nLat 緯度が設定される。
        /// nLon 経度が設定される。
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

        //6==========================================================================================
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
        //[C#] //6
        /// <summary>
        /// オプションボタンの変更をキャンセルする。
        /// '
        /// オプションボタンが切り替わるのをキャンセルして、もとの選択にもどす。
        /// '
        /// 引き数：
        /// optButton もともと選択されていたオプションボタン。
        /// 
        /// </summary>
        /// <param name="optButton"></param>
        private void CancelOptionBtn(RadioButton optButton)
        {
            m_bOptionBtn = true;
            optButton.Checked = true;
            m_bOptionBtn = false;

        }





        //------------------------------------------------------------------------------------------
        //------------------------------------------------------------------------------------------

    }
}
