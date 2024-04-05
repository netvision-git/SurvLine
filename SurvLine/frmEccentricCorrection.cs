using SurvLine.mdl;
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
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static SurvLine.mdl.mdlEccentricCorrection;
using System.Security.Cryptography;
using System.Drawing.Drawing2D;
using ComboBox = System.Windows.Forms.ComboBox;
using TextBox = System.Windows.Forms.TextBox;

namespace SurvLine
{
    public partial class frmEccentricCorrection : Form
    {

        //'*******************************************************************************
        //'オプション画面
        //
        //Option Explicit
        //
        //'*******************************************************************************

        //1==========================================================================================
        /*[VB]
            'タブ番号。
            Private Enum TAB_NUMBER
                TAB_ANGLE = 0 '角度。
                TAB_DISTANCE '距離。
                TAB_ELEVATION '高度角補正。
            End Enum

            'プロパティ
            Public Result As Long 'ダイアログの結果。
            Public GenuineNumber As String '本点番号。
            Public GenuineName As String '本点名称。

            '2007/3/15 追加 NGS Yamada
            Public MarkNumber As String
            Public MarkName As String

            'インプリメンテーション
            Private m_clsEccentricCorrectionParam As New EccentricCorrectionParam '偏心補正パラメータ。
            Private m_clsObservationPoint As ObservationPoint '観測点(HeadPoint)。
            Private m_clsMarkers() As BaseLineVector '方位標ベクトル。配列の要素は(-1 To ...)、要素 -1 は未使用。

            Private m_bUsePointChanging As Boolean '偏心座標候補コンボボックスがChange中。'2009/11 H.Nakamura
            Private m_clsMarkPoint As New ObservationPoint  '手入力時の方位標 2007/3/15 NGS Yamada
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]

        //'タブ番号。
        private enum TAB_NUMBER
        {
            TAB_ANGLE = 0,      //'角度。
            TAB_DISTANCE,       //'距離。
            TAB_ELEVATION,      //'高度角補正。
        }

        //'プロパティ
        public long Result;             //'ダイアログの結果。
        public string GenuineNumber;    //'本点番号。
        public string GenuineName;      //'本点名称。

        //'2007/3/15 追加 NGS Yamada
        public string MarkNumber;
        public string MarkName;

        //'インプリメンテーション
        private EccentricCorrectionParam m_clsEccentricCorrectionParam;  //'偏心補正パラメータ。

        private ObservationPoint m_clsObservationPoint; //'観測点(HeadPoint)。

        private List<BaseLineVector> m_clsMarkers;      //'方位標ベクトル。配列の要素は(-1 To ...)、要素 -1 は未使用。

        private bool m_bUsePointChanging;               //'偏心座標候補コンボボックスがChange中。'2009/11 H.Nakamura
        private ObservationPoint m_clsMarkPoint;        // '手入力時の方位標 2007/3/15 NGS Yamada


        //------------------------------------------------------------------------------------------
        //[C#]
        private MdlMain m_mdlMain;
        private mdlEccentricCorrection mdlEccentricCorrection = new mdlEccentricCorrection();

        public frmEccentricCorrection(MdlMain mdlMain)
        {
            InitializeComponent();
            m_mdlMain = mdlMain;
            m_clsEccentricCorrectionParam = new EccentricCorrectionParam(mdlMain);
            
            //  m_clsMarkPoint = new ObservationPoint();

            Load += Form_Load;

        }

        //'*******************************************************************************
        //'プロパティ
        //'*******************************************************************************

        //6==========================================================================================
        /*[VB]
            '偏心補正パラメータ。
            Property Let EccentricCorrectionParam(ByVal clsEccentricCorrectionParam As EccentricCorrectionParam)
                If clsEccentricCorrectionParam Is Nothing Then
                    Set m_clsEccentricCorrectionParam = New EccentricCorrectionParam
                Else
                    Let m_clsEccentricCorrectionParam = clsEccentricCorrectionParam
                End If
            End Property
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#] //6
        /// <summary>
        /// 偏心補正パラメータ。
        /// 
        /// </summary>
        /// <param name="clsEccentricCorrectionParam"></param>
        public void EccentricCorrectionParam(EccentricCorrectionParam clsEccentricCorrectionParam)
        {
            if (clsEccentricCorrectionParam == null)
            {
                m_clsEccentricCorrectionParam = new EccentricCorrectionParam(m_mdlMain);
            }
            else
            {
                m_clsEccentricCorrectionParam = clsEccentricCorrectionParam;
            }
        }


        //6==========================================================================================
        /*[VB]
            '偏心補正パラメータ。
            Property Get EccentricCorrectionParam() As EccentricCorrectionParam
                Set EccentricCorrectionParam = m_clsEccentricCorrectionParam
            End Property
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#] //6
        /// <summary>
        /// 偏心補正パラメータ。
        /// </summary>
        /// <returns></returns>
        public EccentricCorrectionParam EccentricCorrectionParam()
        {
            return m_clsEccentricCorrectionParam;
        }


        //6==========================================================================================
        /*[VB]
            '観測点。
            Property Set ObservationPoint(ByVal clsObservationPoint As ObservationPoint)
                Set m_clsObservationPoint = clsObservationPoint
            End Property
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#] //6
        /// <summary>
        /// 観測点。
        /// 
        /// </summary>
        /// <param name="clsObservationPoint"></param>
        public void ObservationPoint(ObservationPoint clsObservationPoint)
        {
            m_clsObservationPoint = clsObservationPoint;
        }

        //6==========================================================================================
        /*[VB]
            Private Sub cmdInput_Click()
                frmInputCoordinate.PointNumber = m_clsEccentricCorrectionParam.MarkNumber
                frmInputCoordinate.PointName = m_clsEccentricCorrectionParam.MarkName
                frmInputCoordinate.EditCode = m_clsEccentricCorrectionParam.MarkEditCode
                frmInputCoordinate.nLat = m_clsEccentricCorrectionParam.MarkLat
                frmInputCoordinate.nLon = m_clsEccentricCorrectionParam.MarkLon
                frmInputCoordinate.nX = m_clsEccentricCorrectionParam.MarkX
                frmInputCoordinate.nY = m_clsEccentricCorrectionParam.MarkY
                frmInputCoordinate.nHeight = m_clsEccentricCorrectionParam.MarkHeight
                frmInputCoordinate.nAlt = m_clsEccentricCorrectionParam.MarkAlt
    
                '編集ダイアログ。
                Call frmInputCoordinate.Show(1)
                If frmInputCoordinate.Result = vbOK Then
                    m_clsEccentricCorrectionParam.MarkNumber = frmInputCoordinate.PointNumber
                    m_clsEccentricCorrectionParam.MarkName = frmInputCoordinate.PointName
                    m_clsEccentricCorrectionParam.MarkEditCode = frmInputCoordinate.EditCode
                    m_clsEccentricCorrectionParam.MarkLat = frmInputCoordinate.nLat
                    m_clsEccentricCorrectionParam.MarkLon = frmInputCoordinate.nLon
                    m_clsEccentricCorrectionParam.MarkX = frmInputCoordinate.nX
                    m_clsEccentricCorrectionParam.MarkY = frmInputCoordinate.nY
                    m_clsEccentricCorrectionParam.MarkHeight = frmInputCoordinate.nHeight
                    m_clsEccentricCorrectionParam.MarkAlt = frmInputCoordinate.nAlt
                    Call UpdateMark
                    Call UpdateEccentricPicture
                End If
            End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#] //6
        /// <summary>
        /// 入力ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdInput_Click(object sender, EventArgs e)
        {
            frmInputCoordinate frmInputCoordinate = new frmInputCoordinate(m_mdlMain)
            {
                PointNumber = m_clsEccentricCorrectionParam.MarkNumber,
                PointName = m_clsEccentricCorrectionParam.MarkName,
                //  EditCode = (MdlNSDefine.EDITCODE_STYLE)m_clsEccentricCorrectionParam.MarkEditCode,
                EditCode = m_clsEccentricCorrectionParam.MarkEditCode,
                nLat = m_clsEccentricCorrectionParam.MarkLat,
                nLon = m_clsEccentricCorrectionParam.MarkLon,
                nX = m_clsEccentricCorrectionParam.MarkX,
                nY = m_clsEccentricCorrectionParam.MarkY,
                nHeight = m_clsEccentricCorrectionParam.MarkHeight,
                nAlt = m_clsEccentricCorrectionParam.MarkAlt
            };
            //**************************
            //'編集ダイアログ。
            //**************************
            frmInputCoordinate.ShowDialog();

            if (frmInputCoordinate.Result == vbOK)
            {
                m_clsEccentricCorrectionParam.MarkNumber = frmInputCoordinate.PointNumber;
                m_clsEccentricCorrectionParam.MarkName = frmInputCoordinate.PointName;
                m_clsEccentricCorrectionParam.MarkEditCode = (long)frmInputCoordinate.EditCode;
                m_clsEccentricCorrectionParam.MarkLat = frmInputCoordinate.nLat;
                m_clsEccentricCorrectionParam.MarkLon = frmInputCoordinate.nLon;
                m_clsEccentricCorrectionParam.MarkX = frmInputCoordinate.nX;
                m_clsEccentricCorrectionParam.MarkY = frmInputCoordinate.nY;
                m_clsEccentricCorrectionParam.MarkHeight = frmInputCoordinate.nHeight;
                m_clsEccentricCorrectionParam.MarkAlt = frmInputCoordinate.nAlt;
                //  Call UpdateMark
                //  Call UpdateEccentricPicture

            }

        }

        //'*******************************************************************************
        //
        //'イベント
        //
        //'*******************************************************************************


        //6==========================================================================================
        /*[VB]
            '初期化。
            Private Sub Form_Load()

                On Error GoTo ErrorHandler
    
                '変数初期化。
                Result = vbCancel
    
                'タブ初期化。
                Dim objTab As Object
                For Each objTab In tsTab.Tabs
                    'fraTab(objTab.Index - 1).BorderStyle = vbBSNone
                Next
                'Call ChangeTab(tsTab, fraTab)
    
                '2009/11 H.Nakamura
                'コントロールの初期化。
                lblX.Visible = False
                lblY.Visible = False
                lblH.Visible = False
    
                '方位標リスト。
                Dim clsBaseLineVectors() As BaseLineVector
                ReDim clsBaseLineVectors(-1 To -1)
                Call GetDocument().NetworkModel.GetConnectBaseLineVectorsEx(m_clsObservationPoint, clsBaseLineVectors)
                ReDim m_clsMarkers(-1 To -1)
                Dim i As Long
                For i = 0 To UBound(clsBaseLineVectors)
                    Dim clsObservationPoint As ObservationPoint
                    '反対側の観測点。
                    If clsBaseLineVectors(i).StrPoint.HeadPoint Is m_clsObservationPoint Then
                        Set clsObservationPoint = clsBaseLineVectors(i).EndPoint
                    Else
                        Set clsObservationPoint = clsBaseLineVectors(i).StrPoint
                    End If
                    Call cmbMarker.AddItem(clsObservationPoint.Number & "(" & clsObservationPoint.Name & ") 基線" & clsBaseLineVectors(i).Session)
                    ReDim Preserve m_clsMarkers(-1 To UBound(m_clsMarkers) + 1)
                    Set m_clsMarkers(UBound(m_clsMarkers)) = clsBaseLineVectors(i)
                Next
    
                '偏心座標候補。'2009/11 H.Nakamura
                Call MakeUsePoint(cmbUsePointHori)
                Call MakeUsePoint(cmbUsePointDrct)
    
                '値の設定。
                lblEccentricPoint.Caption = m_clsObservationPoint.DispNumber & "(" & m_clsObservationPoint.Name & ")"
                txtGenuineNumber.Text = GenuineNumber
                txtGenuineName.Text = GenuineName
                Dim nH As Long
                Dim nM As Long
                Dim nS As Double
                If m_clsEccentricCorrectionParam.AngleType = ANGLETYPE_HORIZONTAL Then
                    optHorizontal.Value = True
                    optGPS.Value = True
                ElseIf m_clsEccentricCorrectionParam.AngleType = ANGLETYPE_MARK Then
                    optHorizontal.Value = True
                    optInput.Value = True
                Else
                    optDirection.Value = True
                End If
    
                Call UpdateMark
    
                For i = 0 To UBound(m_clsMarkers)
                    If m_clsMarkers(i).Key = m_clsEccentricCorrectionParam.Marker Then
                        cmbMarker.ListIndex = i
                        Exit For
                    End If
                Next
                Call d_to_dms_decimal(m_clsEccentricCorrectionParam.Horizontal / PAI * 180, nH, nM, nS, GUI_SEC_DECIMAL)
                txtHoriH.Text = CStr(nH)
                txtHoriM.Text = CStr(nM)
                txtHoriS.Text = FormatRound0Trim(nS, GUI_SEC_DECIMAL)
                Call d_to_dms_decimal(m_clsEccentricCorrectionParam.Direction / PAI * 180, nH, nM, nS, GUI_SEC_DECIMAL)
                txtDrctH.Text = CStr(nH)
                txtDrctM.Text = CStr(nM)
                txtDrctS.Text = FormatRound0Trim(nS, GUI_SEC_DECIMAL)
                txtDistance.Text = FormatRound0Trim(m_clsEccentricCorrectionParam.Distance, GUI_FLOAT_DECIMAL)
                chkTS.Value = IIf(m_clsEccentricCorrectionParam.UseTS, 1, 0)
                txtFromHeightTS.Text = FormatRound0Trim(m_clsEccentricCorrectionParam.FromHeightTS, GUI_FLOAT_DECIMAL)
                txtToHeightTS.Text = FormatRound0Trim(m_clsEccentricCorrectionParam.ToHeightTS, GUI_FLOAT_DECIMAL)
                Call d_to_dms_decimal(Abs(m_clsEccentricCorrectionParam.ElevationBC) / PAI * 180, nH, nM, nS, GUI_SEC_DECIMAL)
                txtElevationBCH.Text = IIf(m_clsEccentricCorrectionParam.ElevationBC < 0, "-", "") & CStr(nH)
                txtElevationBCM.Text = CStr(nM)
                txtElevationBCS.Text = FormatRound0Trim(nS, GUI_SEC_DECIMAL)
                Call d_to_dms_decimal(Abs(m_clsEccentricCorrectionParam.ElevationCB) / PAI * 180, nH, nM, nS, GUI_SEC_DECIMAL)
                txtElevationCBH.Text = IIf(m_clsEccentricCorrectionParam.ElevationCB < 0, "-", "") & CStr(nH)
                txtElevationCBM.Text = CStr(nM)
                txtElevationCBS.Text = FormatRound0Trim(nS, GUI_SEC_DECIMAL)
                txtFromHeightBC.Text = FormatRound0Trim(m_clsEccentricCorrectionParam.FromHeightBC, GUI_HEIGHT_DECIMAL)
                txtToHeightBC.Text = FormatRound0Trim(m_clsEccentricCorrectionParam.ToHeightBC, GUI_HEIGHT_DECIMAL)
                txtFromHeightCB.Text = FormatRound0Trim(m_clsEccentricCorrectionParam.FromHeightCB, GUI_HEIGHT_DECIMAL)
                txtToHeightCB.Text = FormatRound0Trim(m_clsEccentricCorrectionParam.ToHeightCB, GUI_HEIGHT_DECIMAL)
    
                '2009/11 H.Nakamura
                m_bUsePointChanging = True
                For i = 0 To UBound(m_clsMarkers)
                    If m_clsMarkers(i).Key = m_clsEccentricCorrectionParam.UsePoint Then
                        cmbUsePointHori.ListIndex = i
                        cmbUsePointDrct.ListIndex = i
                        Exit For
                    End If
                Next
                m_bUsePointChanging = False
    
                Call UpdateTS
    
                picEccentricPicture.Line (picEccentricPicture.ScaleLeft, picEccentricPicture.ScaleTop)-(picEccentricPicture.ScaleLeft + picEccentricPicture.ScaleWidth, picEccentricPicture.ScaleTop + picEccentricPicture.ScaleHeight), GetSysColor(COLOR_WINDOW), BF
                Call UpdateEccentricPicture
    
                Exit Sub
    
            ErrorHandler:
                Call mdlMain.ErrorExit
   
            End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#] //6
        /// <summary>
        /// 初期化。
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form_Load(object sender, EventArgs e)
        {
            try
            {
                //*****************************************************
                //  LostFocus定義　追加
                //*****************************************************
                txtHoriH.LostFocus += new EventHandler(txtHoriH_LostFocus);
                txtHoriM.LostFocus += new EventHandler(txtHoriM_LostFocus);
                txtHoriS.LostFocus += new EventHandler(txtHoriS_LostFocus);
                txtDrctH.LostFocus += new EventHandler(txtDrctH_LostFocus);
                txtDrctM.LostFocus += new EventHandler(txtDrctM_LostFocus);
                txtDrctS.LostFocus += new EventHandler(txtDrctS_LostFocus);

                //*****************************************************
                //  GotFocus定義  追加
                //*****************************************************
                txtGenuineNumber.GotFocus += new EventHandler(txtGenuineNumber_GotFocus);
                txtGenuineName.GotFocus += new EventHandler(txtGenuineName_GotFocus);
                txtHoriH.GotFocus += new EventHandler(txtHoriH_GotFocus);
                txtHoriM.GotFocus += new EventHandler(txtHoriM_GotFocus);
                txtHoriS.GotFocus += new EventHandler(txtHoriS_GotFocus);
                txtDrctH.GotFocus += new EventHandler(txtDrctH_GotFocus);
                txtDrctM.GotFocus += new EventHandler(txtDrctM_GotFocus);
                txtDrctS.GotFocus += new EventHandler(txtDrctS_GotFocus);
                txtDistance.GotFocus += new EventHandler(txtDistance_GotFocus);
                txtFromHeightTS.GotFocus += new EventHandler(txtFromHeightTS_GotFocus);
                txtToHeightTS.GotFocus += new EventHandler(txtToHeightTS_GotFocus);
                txtElevationBCH.GotFocus += new EventHandler(txtElevationBCH_GotFocus);
                txtElevationBCM.GotFocus += new EventHandler(txtElevationBCM_GotFocus);
                txtElevationBCS.GotFocus += new EventHandler(txtElevationBCS_GotFocus);
                txtElevationCBH.GotFocus += new EventHandler(txtElevationCBH_GotFocus);
                txtElevationCBM.GotFocus += new EventHandler(txtElevationCBM_GotFocus);
                txtElevationCBS.GotFocus += new EventHandler(txtElevationCBS_GotFocus);
                txtFromHeightBC.GotFocus += new EventHandler(txtFromHeightBC_GotFocus);
                txtToHeightCB.GotFocus += new EventHandler(txtToHeightCB_GotFocus);


                //*****************************************************
                //変数初期化。
                Result = vbCancel;


                //タブ初期化。
                object objTab;
                //foreach(objTab in tsTab.Tabs)
                //{
                ///////'fraTab(objTab.Index - 1).BorderStyle = vbBSNone
                //}
                //'Call ChangeTab(tsTab, fraTab)


                //'2009/11 H.Nakamura
                //'コントロールの初期化。
                lblX.Visible = false;
                lblY.Visible = false;
                lblH.Visible = false;

                //'方位標リスト。
                List<BaseLineVector> clsBaseLineVectors = new List<BaseLineVector>();
                //      ReDim clsBaseLineVectors(-1 To - 1)
                m_mdlMain.GetDocument().NetworkModel().GetConnectBaseLineVectorsEx(m_clsObservationPoint, clsBaseLineVectors);

                //      ReDim m_clsMarkers(-1 To - 1)
                m_clsMarkers = new List<BaseLineVector>();
                int i;
                for (i = 0; i < clsBaseLineVectors.Count; i++)
                {
                    ObservationPoint clsObservationPoint;
                    //反対側の観測点。
                    if (clsBaseLineVectors[i].StrPoint().HeadPoint() == m_clsObservationPoint)
                    {
                        clsObservationPoint = clsBaseLineVectors[i].EndPoint();
                    }
                    else
                    {
                        clsObservationPoint = clsBaseLineVectors[i].StrPoint();
                    }
                    _ = cmbMarker.Items.Add(clsObservationPoint.Number() + "(" + clsObservationPoint.Name() + ") 基線" + clsBaseLineVectors[i].Session);

                    /*---------------------------------------------------------------------
                        ReDim Preserve m_clsMarkers(-1 To UBound(m_clsMarkers) + 1)
                        Set m_clsMarkers(UBound(m_clsMarkers)) = clsBaseLineVectors(i)
                     */
                    m_clsMarkers.Add(clsBaseLineVectors[i]);

                }//for (i = 0; i < clsBaseLineVectors.Count; i++)

                //偏心座標候補。'2009 / 11 H.Nakamura
                cmbUsePointHori.Text = "";
                MakeUsePoint(cmbUsePointHori);
                cmbUsePointDrct.Text = "";
                MakeUsePoint(cmbUsePointDrct);

                //値の設定。
                lblEccentricPoint.Text = m_clsObservationPoint.DispNumber() + "(" + m_clsObservationPoint.Name() + ")";
                txtGenuineNumber.Text = GenuineNumber;
                txtGenuineName.Text = GenuineName;
                int nH = 0;
                int nM = 0;
                double nS = 0;
                if (m_clsEccentricCorrectionParam.AngleType == (long)ANGLE_TYPE.ANGLETYPE_HORIZONTAL)
                {
                    optHorizontal.Checked = true;
                    optGPS.Checked = true;
                }
                else if (m_clsEccentricCorrectionParam.AngleType == (long)ANGLE_TYPE.ANGLETYPE_MARK)
                {
                    optHorizontal.Checked = true;
                    optInput.Checked = true;
                }
                else
                {
                    optDirection.Checked = false;
                }

                UpdateMark();


                cmbMarker.Text = "";
                for (i = 0; i < m_clsMarkers.Count; i++)
                {
                    if (m_clsMarkers[i].Key() == m_clsEccentricCorrectionParam.Marker)
                    {
                        cmbMarker.SelectedIndex = i;
                        break;
                    }
                }


                d_to_dms_decimal(m_clsEccentricCorrectionParam.Horizontal / PAI * 180, ref nH, ref nM, ref nS, (int)GUI_SEC_DECIMAL);
                txtHoriH.Text = nH.ToString();
                txtHoriM.Text = nM.ToString();
                txtHoriS.Text = FormatRound0Trim(nS, GUI_SEC_DECIMAL);
                d_to_dms_decimal(m_clsEccentricCorrectionParam.Direction / PAI * 180, ref nH, ref nM, ref nS, (int)GUI_SEC_DECIMAL);
                txtDrctH.Text = nH.ToString();
                txtDrctM.Text = nM.ToString();
                txtDrctS.Text = FormatRound0Trim(nS, GUI_SEC_DECIMAL);
                txtDistance.Text = FormatRound0Trim(m_clsEccentricCorrectionParam.Distance, GUI_FLOAT_DECIMAL);
                if (m_clsEccentricCorrectionParam.UseTS)
                {
                    chkTS.Checked = true;
                }
                else
                {
                    chkTS.Checked = false;
                }
                txtFromHeightTS.Text = FormatRound0Trim(m_clsEccentricCorrectionParam.FromHeightTS, GUI_FLOAT_DECIMAL);
                txtToHeightTS.Text = FormatRound0Trim(m_clsEccentricCorrectionParam.ToHeightTS, GUI_FLOAT_DECIMAL);
                d_to_dms_decimal(Math.Abs(m_clsEccentricCorrectionParam.ElevationBC) / PAI * 180, ref nH, ref nM, ref nS, (int)GUI_SEC_DECIMAL);

                if (m_clsEccentricCorrectionParam.ElevationBC < 0)
                {
                    txtElevationBCH.Text = "-" + nH.ToString();
                }
                else
                {
                    txtElevationBCH.Text = "" + nH.ToString();
                }
                txtElevationBCM.Text = nM.ToString();
                txtElevationBCS.Text = FormatRound0Trim(nS, GUI_SEC_DECIMAL);
                d_to_dms_decimal(Math.Abs(m_clsEccentricCorrectionParam.ElevationCB) / PAI * 180, ref nH, ref nM, ref nS, (int)GUI_SEC_DECIMAL);

                if (m_clsEccentricCorrectionParam.ElevationCB < 0)
                {
                    txtElevationCBH.Text = "-" + nH.ToString();
                }
                else
                {
                    txtElevationCBH.Text = "" + nH.ToString();
                }
                txtElevationCBM.Text = nM.ToString();
                txtElevationCBS.Text = FormatRound0Trim(nS, GUI_SEC_DECIMAL);
                txtFromHeightBC.Text = FormatRound0Trim(m_clsEccentricCorrectionParam.FromHeightBC, GUI_HEIGHT_DECIMAL);
                txtToHeightBC.Text = FormatRound0Trim(m_clsEccentricCorrectionParam.ToHeightBC, GUI_HEIGHT_DECIMAL);
                txtFromHeightCB.Text = FormatRound0Trim(m_clsEccentricCorrectionParam.FromHeightCB, GUI_HEIGHT_DECIMAL);
                txtToHeightCB.Text = FormatRound0Trim(m_clsEccentricCorrectionParam.ToHeightCB, GUI_HEIGHT_DECIMAL);

                //'2009/11 H.Nakamura
                m_bUsePointChanging = true;
                for (i = 0; i <m_clsMarkers.Count; i++)
                {
                    if (m_clsMarkers[i].Key() == m_clsEccentricCorrectionParam.UsePoint)
                    {
                        cmbUsePointHori.SelectedIndex = i;
                        cmbUsePointDrct.SelectedIndex = i;
                        break;
                    }
                }

                m_bUsePointChanging = false;

                UpdateTS();



                /*検討  
                 picEccentricPicture.Line(picEccentricPicture.ScaleLeft, picEccentricPicture.ScaleTop) - (picEccentricPicture.ScaleLeft + picEccentricPicture.ScaleWidth, picEccentricPicture.ScaleTop + picEccentricPicture.ScaleHeight), GetSysColor(COLOR_WINDOW), BF
                */


                UpdateEccentricPicture();


            }
            catch (Exception ex)
            {
                _ = MessageBox.Show(ex.Message, "エラー発生", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        //6==========================================================================================
        /*[VB]
             '終了処理。
            Private Sub Form_Unload(Cancel As Integer)

                On Error GoTo ErrorHandler
    
                Set m_clsObservationPoint = Nothing
                ReDim m_clsMarkers(-1 To -1)
    
                Exit Sub
    
            ErrorHandler:
                Call mdlMain.ErrorExit
    
            End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#] //6
        /// <summary>
        /// 終了処理。
        /// 
        /// </summary>
        /// <param name="Cancel"></param>
        private void Form_Unload(int Cancel)
        {
            try
            {
                m_clsObservationPoint = null;
                //  ReDim m_clsMarkers(-1 To - 1)
                m_clsMarkers = new List<BaseLineVector>();

            }
            catch (Exception ex)
            {
                _ = MessageBox.Show(ex.Message, "エラー発生", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }




        //6==========================================================================================
        /*[VB]
            '水平角-GPSベクトル使用
            Private Sub optGPS_Click()

                On Error GoTo ErrorHandler
    
                Call ChangeAngleType2(True)
                Call UpdateEccentricPicture
    
                Exit Sub
    
            ErrorHandler:
                Call mdlMain.ErrorExit
    
            End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#] //6
        /// <summary>
        /// 水平角-GPSベクトル使用
        /// 
        /// </summary>
        private void optGPS_CheckedChanged(object sender, EventArgs e)
        {
            optGPS_Click();
        }
        private void optGPS_Click()
        {
            try
            {
                ChangeAngleType2(true);
                UpdateEccentricPicture();

            }
            catch (Exception ex)
            {
                _ = MessageBox.Show(ex.Message, "エラー発生", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //6==========================================================================================
        /*[VB]
            Private Sub optInput_Click()

                On Error GoTo ErrorHandler
    
                Call ChangeAngleType2(False)
                Call UpdateEccentricPicture
    
                Exit Sub
    
            ErrorHandler:
                Call mdlMain.ErrorExit
    
            End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#] //6
        /// <summary>
        /// 入力ボタン
        /// </summary>
        private void optInput_CheckedChanged(object sender, EventArgs e)
        {
            optInput_Click();
        }
        private void optInput_Click()
        {
            try
            {
                ChangeAngleType2(false);
                UpdateEccentricPicture();
            }
            catch (Exception ex)
            {
                _ = MessageBox.Show(ex.Message, "エラー発生", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //6==========================================================================================
        /*[VB]
            'テキストをすべて選択する。
            Private Sub txtGenuineNumber_GotFocus()

                On Error GoTo ErrorHandler
    
                Call SelectText(txtGenuineNumber)
    
                Exit Sub
    
            ErrorHandler:
                Call mdlMain.ErrorExit
    
            End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#] //6  /66
        /// <summary>
        /// テキストをすべて選択する。
        /// 
        /// </summary>
        private void txtGenuineNumber_GotFocus(object sender, EventArgs e)
        {
            txtGenuineNumber_GotFocus();
        }
        private void txtGenuineNumber_GotFocus()
        {
            try
            {
                SelectText(txtGenuineNumber);
            }
            catch (Exception ex)
            {
                _ = MessageBox.Show(ex.Message, "エラー発生", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //6==========================================================================================
        /*[VB]
            'テキストをすべて選択する。
            Private Sub txtGenuineName_GotFocus()

                On Error GoTo ErrorHandler
    
                Call SelectText(txtGenuineName)
    
                Exit Sub
    
            ErrorHandler:
                Call mdlMain.ErrorExit
    
            End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#] //6
        /// <summary>
        /// テキストをすべて選択する。
        /// </summary>
        private void txtGenuineName_GotFocus(object sender, EventArgs e)
        {
            txtGenuineName_GotFocus();
        }
        private void txtGenuineName_GotFocus()
        {
            try
            {
                SelectText(txtGenuineName);
            }
            catch (Exception ex)
            {
                _ = MessageBox.Show(ex.Message, "エラー発生", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //6==========================================================================================
        /*[VB]
            'テキストをすべて選択する。
            Private Sub txtDrctH_GotFocus()

                On Error GoTo ErrorHandler
    
                Call SelectText(txtDrctH)
    
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
        private void txtDrctH_GotFocus(object sender, EventArgs e)
        {
            txtDrctH_GotFocus();
        }
        private void txtDrctH_GotFocus()
        {
            try
            {
                SelectText(txtDrctH);
            }
            catch (Exception ex)
            {
                _ = MessageBox.Show(ex.Message, "エラー発生", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //6==========================================================================================
        /*[VB]
            'テキストをすべて選択する。
            Private Sub txtDrctM_GotFocus()

                On Error GoTo ErrorHandler
    
                Call SelectText(txtDrctM)
    
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
        private void txtDrctM_GotFocus(object sender, EventArgs e)
        {
            txtDrctM_GotFocus();
        }
        private void txtDrctM_GotFocus()
        {
            try
            {
                SelectText(txtDrctM);
            }
            catch (Exception ex)
            {
                _ = MessageBox.Show(ex.Message, "エラー発生", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //6==========================================================================================
        /*[VB]
            'テキストをすべて選択する。
            Private Sub txtDrctS_GotFocus()

                On Error GoTo ErrorHandler
    
                Call SelectText(txtDrctS)
    
                Exit Sub
    
            ErrorHandler:
                Call mdlMain.ErrorExit
    
            End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#] //6
        /// <summary>
        /// テキストをすべて選択する。
        /// </summary>
        private void txtDrctS_GotFocus(object sender, EventArgs e)
        {
            txtDrctS_GotFocus();
        }
        private void txtDrctS_GotFocus()
        {
            try
            {
                SelectText(txtDrctS);
            }
            catch (Exception ex)
            {
                _ = MessageBox.Show(ex.Message, "エラー発生", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //6==========================================================================================
        /*[VB]
            'テキストをすべて選択する。
            Private Sub txtHoriH_GotFocus()

                On Error GoTo ErrorHandler
    
                Call SelectText(txtHoriH)
    
                Exit Sub
    
            ErrorHandler:
                Call mdlMain.ErrorExit
    
            End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#] //6
        /// <summary>
        /// テキストをすべて選択する。
        /// </summary>
        private void txtHoriH_GotFocus(object sender, EventArgs e)
        {
            txtHoriH_GotFocus();
        }
        private void txtHoriH_GotFocus()
        {
            try
            {
                SelectText(txtHoriH);
            }
            catch (Exception ex)
            {
                _ = MessageBox.Show(ex.Message, "エラー発生", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //6==========================================================================================
        /*[VB]
            'テキストをすべて選択する。
            Private Sub txtHoriM_GotFocus()

                On Error GoTo ErrorHandler
    
                Call SelectText(txtHoriM)
    
                Exit Sub
    
            ErrorHandler:
                Call mdlMain.ErrorExit
    
            End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#] //6
        /// <summary>
        /// テキストをすべて選択する。
        /// </summary>
        private void txtHoriM_GotFocus(object sender, EventArgs e)
        {
            txtHoriM_GotFocus();
        }
        private void txtHoriM_GotFocus()
        {
            try
            {
                SelectText(txtHoriM);
            }
            catch (Exception ex)
            {
                _ = MessageBox.Show(ex.Message, "エラー発生", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //6==========================================================================================
        /*[VB]
            'テキストをすべて選択する。
            Private Sub txtHoriS_GotFocus()

                On Error GoTo ErrorHandler
    
                Call SelectText(txtHoriS)
    
                Exit Sub
    
            ErrorHandler:
                Call mdlMain.ErrorExit
    
            End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#] //6
        /// <summary>
        /// テキストをすべて選択する。
        /// </summary>
        private void txtHoriS_GotFocus(object sender, EventArgs e)
        {
            txtHoriS_GotFocus();
        }
        private void txtHoriS_GotFocus()
        {
            try
            {
                SelectText(txtHoriS);
            }
            catch (Exception ex)
            {
                _ = MessageBox.Show(ex.Message, "エラー発生", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //6==========================================================================================
        /*[VB]
            'テキストをすべて選択する。
            Private Sub txtDistance_GotFocus()

                On Error GoTo ErrorHandler
    
                Call SelectText(txtDistance)
    
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
        private void txtDistance_GotFocus(object sender, EventArgs e)
        {
            txtDistance_GotFocus();
        }
        private void txtDistance_GotFocus()
        {
            try
            {
                SelectText(txtDistance);
            }
            catch (Exception ex)
            {
                _ = MessageBox.Show(ex.Message, "エラー発生", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //6==========================================================================================
        /*[VB]
            'テキストをすべて選択する。
            Private Sub txtFromHeightTS_GotFocus()

                On Error GoTo ErrorHandler
    
                Call SelectText(txtFromHeightTS)
    
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
        private void txtFromHeightTS_GotFocus(object sender, EventArgs e)
        {
            txtFromHeightTS_GotFocus();
        }
        private void txtFromHeightTS_GotFocus()
        {
            try
            {
                SelectText(txtFromHeightTS);
            }
            catch (Exception ex)
            {
                _ = MessageBox.Show(ex.Message, "エラー発生", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //6==========================================================================================
        /*[VB]
            'テキストをすべて選択する。
            Private Sub txtToHeightTS_GotFocus()

                On Error GoTo ErrorHandler
    
                Call SelectText(txtToHeightTS)
    
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
        private void txtToHeightTS_GotFocus(object sender, EventArgs e)
        {
            txtToHeightTS_GotFocus();
        }
        private void txtToHeightTS_GotFocus()
        {
            try
            {
                SelectText(txtToHeightTS);
            }
            catch (Exception ex)
            {
                _ = MessageBox.Show(ex.Message, "エラー発生", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //6==========================================================================================
        /*[VB]
            'テキストをすべて選択する。
            Private Sub txtElevationBCH_GotFocus()

                On Error GoTo ErrorHandler
    
                Call SelectText(txtElevationBCH)
    
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
        private void txtElevationBCH_GotFocus(object sender, EventArgs e)
        {
            txtElevationBCH_GotFocus();
        }
        private void txtElevationBCH_GotFocus()
        {
            try
            {
                SelectText(txtElevationBCH);
            }
            catch (Exception ex)
            {
                _ = MessageBox.Show(ex.Message, "エラー発生", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //6==========================================================================================
        /*[VB]
            'テキストをすべて選択する。
            Private Sub txtElevationBCM_GotFocus()

                On Error GoTo ErrorHandler
    
                Call SelectText(txtElevationBCM)
    
                Exit Sub
    
            ErrorHandler:
                Call mdlMain.ErrorExit
    
            End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#] //6
        /// <summary>
        /// テキストをすべて選択する。
        /// </summary>
        private void txtElevationBCM_GotFocus(object sender, EventArgs e)
        {
            txtElevationBCM_GotFocus();
        }
        private void txtElevationBCM_GotFocus()
        {
            try
            {
                SelectText(txtElevationBCM);
            }
            catch (Exception ex)
            {
                _ = MessageBox.Show(ex.Message, "エラー発生", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //6==========================================================================================
        /*[VB]
            'テキストをすべて選択する。
            Private Sub txtElevationBCS_GotFocus()

                On Error GoTo ErrorHandler
    
                Call SelectText(txtElevationBCS)
    
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
        private void txtElevationBCS_GotFocus(object sender, EventArgs e)
        {
            txtElevationBCS_GotFocus();
        }
        private void txtElevationBCS_GotFocus()
        {
            try
            {
                SelectText(txtElevationBCS);
            }
            catch (Exception ex)
            {
                _ = MessageBox.Show(ex.Message, "エラー発生", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //6==========================================================================================
        /*[VB]
            'テキストをすべて選択する。
            Private Sub txtElevationCBH_GotFocus()

                On Error GoTo ErrorHandler
    
                Call SelectText(txtElevationCBH)
    
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
        private void txtElevationCBH_GotFocus(object sender, EventArgs e)
        {
            txtElevationCBH_GotFocus();
        }
        private void txtElevationCBH_GotFocus()
        {
            try
            {
                SelectText(txtElevationCBH);
            }
            catch (Exception ex)
            {
                _ = MessageBox.Show(ex.Message, "エラー発生", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //6==========================================================================================
        /*[VB]
            'テキストをすべて選択する。
            Private Sub txtElevationCBM_GotFocus()

                On Error GoTo ErrorHandler
    
                Call SelectText(txtElevationCBM)
    
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
        private void txtElevationCBM_GotFocus(object sender, EventArgs e)
        {
            txtElevationCBM_GotFocus();
        }
        private void txtElevationCBM_GotFocus()
        {
            try
            {
                SelectText(txtElevationCBM);
            }
            catch (Exception ex)
            {
                _ = MessageBox.Show(ex.Message, "エラー発生", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //6==========================================================================================
        /*[VB]
            'テキストをすべて選択する。
            Private Sub txtElevationCBS_GotFocus()

                On Error GoTo ErrorHandler
    
                Call SelectText(txtElevationCBS)
    
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
        private void txtElevationCBS_GotFocus(object sender, EventArgs e)
        {
            txtElevationCBS_GotFocus();
        }
        private void txtElevationCBS_GotFocus()
        {
            try
            {
                SelectText(txtElevationCBS);
            }
            catch (Exception ex)
            {
                _ = MessageBox.Show(ex.Message, "エラー発生", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //6==========================================================================================
        /*[VB]
            'テキストをすべて選択する。
            Private Sub txtFromHeightBC_GotFocus()

                On Error GoTo ErrorHandler
    
                Call SelectText(txtFromHeightBC)
    
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
        private void txtFromHeightBC_GotFocus(object sender, EventArgs e)
        {
            txtFromHeightBC_GotFocus();
        }
        private void txtFromHeightBC_GotFocus()
        {
            try
            {
                SelectText(txtFromHeightBC);
            }
            catch (Exception ex)
            {
                _ = MessageBox.Show(ex.Message, "エラー発生", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //6==========================================================================================
        /*[VB]
            'テキストをすべて選択する。
            Private Sub txtToHeightBC_GotFocus()

                On Error GoTo ErrorHandler
    
                Call SelectText(txtToHeightBC)
    
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
        private void txtToHeightBC_GotFocus(object sender, EventArgs e)
        {
            txtToHeightBC_GotFocus();
        }
        private void txtToHeightBC_GotFocus()
        {
            try
            {
                SelectText(txtToHeightBC);
            }
            catch (Exception ex)
            {
                _ = MessageBox.Show(ex.Message, "エラー発生", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //6==========================================================================================
        /*[VB]
            'テキストをすべて選択する。
            Private Sub txtToHeightCB_GotFocus()

                On Error GoTo ErrorHandler
    
                Call SelectText(txtToHeightCB)
    
                Exit Sub
    
            ErrorHandler:
                Call mdlMain.ErrorExit
    
            End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#] //6  //66
        /// <summary>
        /// テキストをすべて選択する。
        /// 
        /// </summary>
        private void txtToHeightCB_GotFocus(object sender, EventArgs e)
        {
            txtToHeightCB_GotFocus();
        }
        private void txtToHeightCB_GotFocus()
        {
            try
            {
                SelectText(txtToHeightCB);
            }
            catch (Exception ex)
            {
                _ = MessageBox.Show(ex.Message, "エラー発生", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //6==========================================================================================
        /*[VB]
            'タブ変更
            Private Sub tsTab_MouseUp(Button As Integer, Shift As Integer, X As Single, Y As Single)

                On Error GoTo ErrorHandler
    
                'Call ChangeTab(tsTab, fraTab)
    
                Exit Sub
    
            ErrorHandler:
                Call mdlMain.ErrorExit
    
            End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#] //6
        /// <summary>
        /// タブ変更
        /// 
        /// </summary>
        /// <param name="Button"></param>
        /// <param name="Shift"></param>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        private void tsTab_MouseUp(int Button, int Shift, Single X, Single Y)
        {
            try
            {
                //  'Call ChangeTab(tsTab, fraTab)
            }
            catch (Exception ex)
            {
                _ = MessageBox.Show(ex.Message, "エラー発生", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //6==========================================================================================
        /*[VB]
            'タブ変更
            Private Sub tsTab_Click()

                On Error GoTo ErrorHandler
    
                'Call ChangeTab(tsTab, fraTab)
    
                Exit Sub
    
            ErrorHandler:
                Call mdlMain.ErrorExit
    
            End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#] //6
        /// <summary>
        /// タブ変更    ＜不要とする
        /// </summary>
        private void tsTab_Click()
        {
            try
            {
                //  ChangeTab(tsTab, fraTab0);
            }
            catch (Exception ex)
            {
                _ = MessageBox.Show(ex.Message, "エラー発生", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //6==========================================================================================
        /*[VB]
            '水平角。
            Private Sub optHorizontal_Click()

                On Error GoTo ErrorHandler
    
                Call ChangeAngleType(True)
                Call UpdateEccentricPicture
    
                Exit Sub
    
            ErrorHandler:
                Call mdlMain.ErrorExit
    
            End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#] //6
        /// <summary>
        /// 水平角。
        /// </summary>
        private void optHorizontal_CheckedChanged(object sender, EventArgs e)
        {
            optHorizontal_Click();
        }
        private void optHorizontal_Click()
        {
            try
            {
                ChangeAngleType(true);
                UpdateEccentricPicture();
            }
            catch (Exception ex)
            {
                _ = MessageBox.Show(ex.Message, "エラー発生", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //6==========================================================================================
        /*[VB]
            '方位角。
            Private Sub optDirection_Click()

                On Error GoTo ErrorHandler
    
                Call ChangeAngleType(False)
                Call UpdateEccentricPicture
    
                Exit Sub
    
            ErrorHandler:
                Call mdlMain.ErrorExit
    
            End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#] //6
        /// <summary>
        /// 方位角。
        /// 
        /// </summary>
        private void optDirection_CheckedChanged(object sender, EventArgs e)
        {
            optDirection_Click();
        }
        private void optDirection_Click()
        {
            try
            {
                ChangeAngleType(false);
                UpdateEccentricPicture();
            }
            catch (Exception ex)
            {
                _ = MessageBox.Show(ex.Message, "エラー発生", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //6==========================================================================================
        /*[VB]
            '方位標。
            Private Sub cmbMarker_Click()

                On Error GoTo ErrorHandler
    
                Call UpdateEccentricPicture
    
                Exit Sub
    
            ErrorHandler:
                Call mdlMain.ErrorExit
    
            End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#] //6
        /// <summary>
        /// 方位標。
        /// </summary>
        private void cmbMarker_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbMarker_Click();
        }
        private void cmbMarker_Click()
        {
            try
            {
                UpdateEccentricPicture();
            }
            catch (Exception ex)
            {
                _ = MessageBox.Show(ex.Message, "エラー発生", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //6==========================================================================================
        /*[VB]
            '水平角(度)。
            Private Sub txtHoriH_LostFocus()

                On Error GoTo ErrorHandler
    
                Call UpdateEccentricPicture
    
                Exit Sub
    
            ErrorHandler:
                Call mdlMain.ErrorExit
    
            End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#] //6
        /// <summary>
        /// 水平角(度)。
        /// 
        /// </summary>
        private void txtHoriH_LostFocus(object sender, EventArgs e)
        {
            try
            {
                UpdateEccentricPicture();
            }
            catch (Exception ex)
            {
                _ = MessageBox.Show(ex.Message, "エラー発生", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //6==========================================================================================
        /*[VB]
            '水平角(分)。
            Private Sub txtHoriM_LostFocus()

                On Error GoTo ErrorHandler
    
                Call UpdateEccentricPicture
    
                Exit Sub
    
            ErrorHandler:
                Call mdlMain.ErrorExit
    
            End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#] //6  //66
        /// <summary>
        /// 水平角(分)。
        /// 
        /// </summary>
        private void txtHoriM_LostFocus(object sender, EventArgs e)
        {
            try
            {
                UpdateEccentricPicture();
            }
            catch (Exception ex)
            {
                _ = MessageBox.Show(ex.Message, "エラー発生", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //6==========================================================================================
        /*[VB]
            '水平角(秒)。
            Private Sub txtHoriS_LostFocus()

                On Error GoTo ErrorHandler
    
                Call UpdateEccentricPicture
    
                Exit Sub
    
            ErrorHandler:
                Call mdlMain.ErrorExit
    
            End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#] //6
        /// <summary>
        /// 水平角(秒)。
        /// 
        /// </summary>
        private void txtHoriS_LostFocus(object sender, EventArgs e)
        {
            try
            {
                UpdateEccentricPicture();
            }
            catch (Exception ex)
            {
                _ = MessageBox.Show(ex.Message, "エラー発生", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //6==========================================================================================
        /*[VB]
            '方位角(度)。
            Private Sub txtDrctH_LostFocus()

                On Error GoTo ErrorHandler
    
                Call UpdateEccentricPicture
    
                Exit Sub
    
            ErrorHandler:
                Call mdlMain.ErrorExit
    
            End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#] //6
        /// <summary>
        /// 方位角(度)。
        /// 
        /// </summary>
        private void txtDrctH_LostFocus(object sender, EventArgs e)
        {
            try
            {
                UpdateEccentricPicture();
            }
            catch (Exception ex)
            {
                _ = MessageBox.Show(ex.Message, "エラー発生", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //6==========================================================================================
        /*[VB]
            '方位角(分)。
            Private Sub txtDrctM_LostFocus()

                On Error GoTo ErrorHandler
    
                Call UpdateEccentricPicture
    
                Exit Sub
    
            ErrorHandler:
                Call mdlMain.ErrorExit
    
            End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#] //6
        /// <summary>
        /// 方位角(分)。
        /// 
        /// </summary>
        private void txtDrctM_LostFocus(object sender, EventArgs e)
        {
            try
            {
                UpdateEccentricPicture();
            }
            catch (Exception ex)
            {
                _ = MessageBox.Show(ex.Message, "エラー発生", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //6==========================================================================================
        /*[VB]
            '方位角(秒)。
            Private Sub txtDrctS_LostFocus()

                On Error GoTo ErrorHandler
    
                Call UpdateEccentricPicture
    
                Exit Sub
    
            ErrorHandler:
                Call mdlMain.ErrorExit
    
            End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#] //6
        /// <summary>
        /// 方位角(秒)。
        /// 
        /// </summary>
        private void txtDrctS_LostFocus(object sender, EventArgs e)
        {
            try
            {
                UpdateEccentricPicture();
            }
            catch (Exception ex)
            {
                _ = MessageBox.Show(ex.Message, "エラー発生", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //6==========================================================================================
        /*[VB]
            '測距儀を使用する。
            Private Sub chkTS_Click()

                On Error GoTo ErrorHandler
    
                Call UpdateTS
    
                Exit Sub
    
            ErrorHandler:
                Call mdlMain.ErrorExit
    
            End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#] //6
        /// <summary>
        /// 測距儀を使用する。
        /// 
        /// </summary>
        private void chkTS_CheckedChanged(object sender, EventArgs e)
        {
            chkTS_Click();
        }
        private void chkTS_Click()
        {
            try
            {
                UpdateTS();
            }
            catch (Exception ex)
            {
                _ = MessageBox.Show(ex.Message, "エラー発生", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //6==========================================================================================
        /*[VB]
            '2009/11 H.Nakamura
            '偏心座標候補。
            Private Sub cmbUsePointHori_Click()

                On Error GoTo ErrorHandler
    
                If m_bUsePointChanging Then Exit Sub
                m_bUsePointChanging = True
                cmbUsePointDrct.ListIndex = cmbUsePointHori.ListIndex
                m_bUsePointChanging = False
    
                Call UpdateEccentricPicture
    
                Exit Sub
    
            ErrorHandler:
                Call mdlMain.ErrorExit
    
            End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#] //6
        /// <summary>
        /// 偏心座標候補。
        /// 
        /// </summary>
        private void cmbUsePointHori_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbUsePointHori_Click();
        }
        private void cmbUsePointHori_Click()
        {
            try
            {
                if (m_bUsePointChanging)
                {
                    return;
                }
                m_bUsePointChanging = true;
                cmbUsePointDrct.SelectedIndex = cmbUsePointHori.SelectedIndex;
                m_bUsePointChanging = false;

                UpdateEccentricPicture();

            }
            catch (Exception ex)
            {
                _ = MessageBox.Show(ex.Message, "エラー発生", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //6==========================================================================================
        /*[VB]
            '2009/11 H.Nakamura
            '偏心座標候補。
            Private Sub cmbUsePointDrct_Click()

                On Error GoTo ErrorHandler
    
                If m_bUsePointChanging Then Exit Sub
                m_bUsePointChanging = True
                cmbUsePointHori.ListIndex = cmbUsePointDrct.ListIndex
                m_bUsePointChanging = False
    
                Call UpdateEccentricPicture
    
                Exit Sub
    
            ErrorHandler:
                Call mdlMain.ErrorExit
    
            End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#] //6
        /// <summary>
        /// 偏心座標候補。
        /// </summary>
        private void cmbUsePointDrct_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbUsePointDrct_Click();
        }
        private void cmbUsePointDrct_Click()
        {
            try
            {
                if (m_bUsePointChanging)
                {
                    return;
                }
                m_bUsePointChanging = true;
                cmbUsePointHori.SelectedIndex = cmbUsePointDrct.SelectedIndex;
                m_bUsePointChanging = false;

                UpdateEccentricPicture();

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
                Close();    //Unload(Me)
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
        /// ＯＫでダイアログを終了する
        /// 
        /// </summary>
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
                Close();    //Call Unload(Me)
            }
            catch (Exception ex)
            {
                _ = MessageBox.Show(ex.Message, "エラー発生", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        //'*******************************************************************************
        //'インプリメンテーション
        //'*******************************************************************************


        //6==========================================================================================
        /*[VB]
            '角度種別の変更。
            '
            'コントロールの表示/非表示を更新する。
            '
            '引き数：
            'bHori 水平角フラグ。True=水平角で入力。False=方位角で入力。
            Private Sub ChangeAngleType(ByVal bHori As Boolean)

                fraHorizontal.Visible = bHori
                fraDirection.Visible = Not bHori
            '    lblMarker.Visible = bHori
            '    cmbMarker.Visible = bHori
            '    lblHorizontal.Visible = bHori
            '    txtHoriH.Visible = bHori
            '    txtHoriM.Visible = bHori
            '    txtHoriS.Visible = bHori
            '    lblHoriH.Visible = bHori
            '    lblHoriM.Visible = bHori
            '    lblHoriS.Visible = bHori
            '
            '    lblDirection.Visible = Not bHori
            '    txtDrctH.Visible = Not bHori
            '    txtDrctM.Visible = Not bHori
            '    txtDrctS.Visible = Not bHori
            '    lblDrctH.Visible = Not bHori
            '    lblDrctM.Visible = Not bHori
            '    lblDrctS.Visible = Not bHori
    
            End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#] //6
        /// <summary>
        /// 角度種別の変更。
        /// </summary>
        /// <param name="bHori"></param>
        private void ChangeAngleType(bool bHori)
        {
            fraHorizontal.Visible = bHori;
            fraDirection.Visible = !bHori;

            /*
            '    lblMarker.Visible = bHori
            '    cmbMarker.Visible = bHori
            '    lblHorizontal.Visible = bHori
            '    txtHoriH.Visible = bHori
            '    txtHoriM.Visible = bHori
            '    txtHoriS.Visible = bHori
            '    lblHoriH.Visible = bHori
            '    lblHoriM.Visible = bHori
            '    lblHoriS.Visible = bHori
            '
            '    lblDirection.Visible = Not bHori
            '    txtDrctH.Visible = Not bHori
            '    txtDrctM.Visible = Not bHori
            '    txtDrctS.Visible = Not bHori
            '    lblDrctH.Visible = Not bHori
            '    lblDrctM.Visible = Not bHori
            '    lblDrctS.Visible = Not bHori
             */
        }

        //6==========================================================================================
        /*[VB]
            '方位標の選択方式の変更。
            '
            'コントロールの表示/非表示を更新する。
            '
            '引き数：
            'bHori　方位標フラグ。True=GPSベクトル。False=手入力。
            Private Sub ChangeAngleType2(ByVal bHori As Boolean)
                '2009/11 H.Nakamura
                fraGpsVector.Visible = bHori
                fraInput.Visible = Not bHori
            End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#] //6
        /// <summary>
        /// 方位標の選択方式の変更。
        /// '
        /// コントロールの表示/非表示を更新する。
        /// '
        /// 引き数：
        /// bHori　方位標フラグ。True=GPSベクトル。False=手入力。
        /// 
        /// </summary>
        /// <param name="bHori"></param>
        private void ChangeAngleType2(bool bHori)
        {
            //'2009/11 H.Nakamura
            fraInput.Visible = bHori;
            fraInput.Visible = !bHori;
        }

        //6==========================================================================================
        /*[VB]
            '測距儀を使用する、の更新。
            '
            'コントロールの有効/無効を更新する。
            Private Sub UpdateTS()
                If chkTS.Value = 1 Then
                    txtFromHeightTS.Enabled = True
                    txtToHeightTS.Enabled = True
                    txtFromHeightTS.BackColor = txtDistance.BackColor
                    txtToHeightTS.BackColor = txtDistance.BackColor
                Else
                    txtFromHeightTS.Enabled = False
                    txtToHeightTS.Enabled = False
                    txtFromHeightTS.BackColor = BackColor
                    txtToHeightTS.BackColor = BackColor
                End If
            End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#] //6
        /// <summary>
        /// 測距儀を使用する、の更新。
        /// '
        /// コントロールの有効/無効を更新する。
        /// 
        /// </summary>
        private void UpdateTS()
        {
            if (chkTS.Checked)
            {
                txtFromHeightTS.Enabled = true;
                txtToHeightTS.Enabled = true;
                txtFromHeightTS.BackColor = txtDistance.BackColor;
                txtToHeightTS.BackColor = txtDistance.BackColor;
            }
            else
            {
                txtFromHeightTS.Enabled = false;
                txtToHeightTS.Enabled = false;
                txtFromHeightTS.BackColor = BackColor;
                txtToHeightTS.BackColor = BackColor;
            }
        }

        //6==========================================================================================
        /*[VB]
            '偏心図の更新。
            Private Sub UpdateEccentricPicture()

                If optHorizontal.Value Then
                    Dim T0 As Double
                    If optGPS.Value Then    'GPSベクトル選択時
                        If cmbMarker.ListIndex < 0 Then Exit Sub
                        '方位標の方位角。
                        T0 = GetMarkerDirection(m_clsObservationPoint, m_clsMarkers(cmbMarker.ListIndex))
                    Else    '手入力時
                        If lblNo.Caption = "" Then Exit Sub
                        If m_clsEccentricCorrectionParam.MarkLat = 0 Then Exit Sub
                        If m_clsEccentricCorrectionParam.MarkLon = 0 Then Exit Sub
                        '2009/11 H.Nakamura
                        If cmbUsePointHori.ListIndex < 0 Then Exit Sub
                        'T0 = GetMarkerDirection2(m_clsObservationPoint, m_clsEccentricCorrectionParam.MarkLat, m_clsEccentricCorrectionParam.MarkLon, m_clsEccentricCorrectionParam.MarkHeight)
                        T0 = GetMarkerDirection2(m_clsObservationPoint, m_clsMarkers(cmbUsePointHori.ListIndex), m_clsEccentricCorrectionParam.MarkLat, m_clsEccentricCorrectionParam.MarkLon, m_clsEccentricCorrectionParam.MarkHeight)
                    End If
                    '水平角。
                    Dim nH As Long
                    Dim nM As Long
                    Dim nS As Double
                    nH = Val(txtHoriH.Text)
                    nM = Val(txtHoriM.Text)
                    nS = Val(txtHoriS.Text)
                    If nH < 0 Or 360 <= nH Then Exit Sub
                    If nM < 0 Or 90 <= nM Then Exit Sub
                    If nS < 0 Or 90 <= nS Then Exit Sub
                    Dim Φ As Double
                    Φ = dms_to_d(nH, nM, nS) / 180 * PAI
                    '描画。
                    Call DrawEccentricPicture(picEccentricPicture, Φ, T0)
                ElseIf optDirection.Value Then
                    nH = Val(txtDrctH.Text)
                    nM = Val(txtDrctM.Text)
                    nS = Val(txtDrctS.Text)
                    If nH < 0 Or 360 <= nH Then Exit Sub
                    If nM < 0 Or 90 <= nM Then Exit Sub
                    If nS < 0 Or 90 <= nS Then Exit Sub
                    '方位角。
                    Φ = dms_to_d(nH, nM, nS) / 180 * PAI
                    '描画。
                    Call DrawEccentricPicture(picEccentricPicture, Φ)
                Else
                    Exit Sub
                End If
    
            End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#] //6
        /// <summary>
        /// 偏心図の更新。
        /// 
        /// </summary>
        private void UpdateEccentricPicture()
        {
            int nH = 0;
            int nM = 0;
            double nS = 0;
            double Φ;

            if (optHorizontal.Checked)
            {
                double T0;
                if (optGPS.Checked)    //'GPSベクトル選択時
                {
                    if (cmbMarker.SelectedIndex < 0)
                    {
                        return;
                    }
                    //'方位標の方位角。
                    T0 = mdlEccentricCorrection.GetMarkerDirection(m_clsObservationPoint, m_clsMarkers[cmbMarker.SelectedIndex]);
                }
                else
                {
                    //'手入力時
                    if (lblNo.Text == "")
                    {
                        return;
                    }
                    if (m_clsEccentricCorrectionParam.MarkLat == 0)
                    {
                        return;
                    }
                    if (m_clsEccentricCorrectionParam.MarkLon == 0)
                    {
                        return;
                    }

                    //'2009/11 H.Nakamura
                    if (cmbUsePointHori.SelectedIndex < 0)
                    {
                        return;
                    }
                    //  'T0 = GetMarkerDirection2(m_clsObservationPoint, m_clsEccentricCorrectionParam.MarkLat, m_clsEccentricCorrectionParam.MarkLon, m_clsEccentricCorrectionParam.MarkHeight)
                    T0 = mdlEccentricCorrection.GetMarkerDirection2(m_clsObservationPoint, m_clsMarkers[cmbUsePointHori.SelectedIndex], m_clsEccentricCorrectionParam.MarkLat, m_clsEccentricCorrectionParam.MarkLon, m_clsEccentricCorrectionParam.MarkHeight);

                    //'水平角。
                    nH = int.Parse(txtHoriH.Text);
                    nM = int.Parse(txtHoriM.Text);
                    nS = double.Parse(txtHoriS.Text);
                    if (nH < 0 || 360 <= nH)
                    {
                        return;
                    }
                    if (nM < 0 || 90 <= nM)
                    {
                        return;
                    }
                    if (nS < 0 || 90 <= nS)
                    {
                        return;
                    }
                    Φ = dms_to_d(nH, nM, nS) / 180 * PAI;
                    //'描画。
                    DrawEccentricPicture(picEccentricPicture, Φ, T0);

                }
            }
            else if (optDirection.Checked)
            {
                nH = int.Parse(txtDrctH.Text);
                nM = int.Parse(txtDrctM.Text);
                nS = double.Parse(txtDrctS.Text);
                if (nH < 0 || 360 <= nH)
                {
                    return;
                }
                if (nM < 0 || 90 <= nM)
                {
                    return;
                }
                if (nS < 0 || 90 <= nS)
                {
                    return;
                }

                //'方位角。
                Φ = dms_to_d(nH, nM, nS) / 180 * PAI;
                //'描画。
                DrawEccentricPicture(picEccentricPicture, Φ);
            }
            else
            {
                return;
            }
        }


        //6==========================================================================================
        /*[VB]
            '方位角偏心図の描画。
            '
            'T0 に 0 以上を指定すると、水平角で入力、と判断する。
            'T0 に負の値を指定すると、方位角で入力、と判断する。
            '
            '引き数：
            'objView 描画対象。
            'Φ 水平角で入力の時は、水平角(ラジアン)。方位角で入力の時は方位角(ラジアン)。
            'T0 水平角で入力の時は、方位表の方位角(ラジアン)。方位角で入力の時は負の値。
            Private Sub DrawEccentricPicture(ByVal objView As PictureBox, ByVal Φ As Double, Optional ByVal T0 As Double = -1)
                'クリア。
                objView.Line (objView.ScaleLeft, objView.ScaleTop)-(objView.ScaleLeft + objView.ScaleWidth, objView.ScaleTop + objView.ScaleHeight), GetSysColor(COLOR_WINDOW), BF
                '描画。
                Dim hDC As Long
                hDC = objView.hDC
                Dim pnt As POINT
                Call SetViewportOrgEx(hDC, objView.ScaleWidth * 0.5 / Screen.TwipsPerPixelX, objView.ScaleHeight * 0.5 / Screen.TwipsPerPixelY, pnt)
                Call mdlEccentricPicture.DrawEccentricPicture(hDC, objView.ScaleWidth * 0.5 / Screen.TwipsPerPixelX, objView.ScaleHeight * 0.5 / Screen.TwipsPerPixelY, Φ, T0)
                Call SetViewportOrgEx(hDC, pnt.X, pnt.Y, pnt)
                Call objView.Refresh
            End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#] //6
        /// <summary>
        /// 方位角偏心図の描画。
        /// '
        /// T0 に 0 以上を指定すると、水平角で入力、と判断する。
        /// T0 に負の値を指定すると、方位角で入力、と判断する。
        /// '
        /// 引き数：
        /// objView 描画対象。
        /// Φ 水平角で入力の時は、水平角(ラジアン)。方位角で入力の時は方位角(ラジアン)。
        /// T0 水平角で入力の時は、方位表の方位角(ラジアン)。方位角で入力の時は負の値。
        /// 
        /// </summary>
        /// <param name="objView"></param>
        /// <param name="Φ"></param>
        /// <param name="T0"></param>
        private void DrawEccentricPicture(PictureBox objView, double Φ, double T0 = -1)
        {
            //'クリア。
            //  objView.Line(objView.ScaleLeft, objView.ScaleTop) - (objView.ScaleLeft + objView.ScaleWidth, objView.ScaleTop + objView.ScaleHeight), GetSysColor(COLOR_WINDOW), BF

            //'描画。
            long hDC;

            /*  検討

            hDC = objView.Handle();
            POINT pnt;
            SetViewportOrgEx(hDC, objView.ScaleWidth * 0.5 / Screen.TwipsPerPixelX, objView.ScaleHeight * 0.5 / Screen.TwipsPerPixelY, pnt);
            mdlEccentricPicture.DrawEccentricPicture(hDC, objView.ScaleWidth * 0.5 / Screen.TwipsPerPixelX, objView.ScaleHeight * 0.5 / Screen.TwipsPerPixelY, Φ, T0);
            SetViewportOrgEx(hDC, pnt.X, pnt.Y, pnt);
            objView.Refresh();

            */
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
    
                '観測点No。
                If Not CheckFileNameInputLength(txtGenuineNumber, "観測点No", RINEX_STR_OBSNUMBER - 1) Then Exit Function
                '観測点名称。
                If Not CheckInputLength(txtGenuineName, "観測点名称", RINEX_STR_OBSNAME - 1) Then Exit Function
    
                '角度。
                If optHorizontal.Value Then
                    '方位標。
                    If optGPS.Value = True Then
                        If cmbMarker.ListIndex < 0 Then
                            Call MsgBox("方位標を選択してください。", vbCritical)
                            tsTab.Tabs(TAB_ANGLE + 1).Selected = True
                            Call cmbMarker.SetFocus
                            Exit Function
                        End If
                    Else
                        If lblNo.Caption = "" Then
                            Call MsgBox("座標を入力してください。", vbCritical)
                            Exit Function
                        End If
            
                        '2009/11 H.Nakamura
                        If cmbUsePointHori.ListIndex < 0 Then
                            Call MsgBox("偏心座標候補を選択してください。", vbCritical)
                            tsTab.Tabs(TAB_ANGLE + 1).Selected = True
                            Call cmbUsePointHori.SetFocus
                            Exit Function
                        End If
                    End If
        
                    '水平角。
                    If Not CheckIntegerInputRange(txtHoriH, "水平角(度)", 0, 360, fraTab(TAB_ANGLE).Visible) Then
                        tsTab.Tabs(TAB_ANGLE + 1).Selected = True
                        Call txtHoriH.SetFocus
                        Exit Function
                    End If
                    If Not CheckIntegerInputRange(txtHoriM, "水平角(分)", 0, 60, fraTab(TAB_ANGLE).Visible) Then
                        tsTab.Tabs(TAB_ANGLE + 1).Selected = True
                        Call txtHoriM.SetFocus
                        Exit Function
                    End If
                    If Not CheckFloatInputRange(txtHoriS, "水平角(秒)", 0, 60, fraTab(TAB_ANGLE).Visible) Then
                        tsTab.Tabs(TAB_ANGLE + 1).Selected = True
                        Call txtHoriS.SetFocus
                        Exit Function
                    End If
                ElseIf optDirection.Value Then
                    '方位角。
                    If Not CheckIntegerInputRange(txtDrctH, "方位角(度)", 0, 360, fraTab(TAB_ANGLE).Visible) Then
                        tsTab.Tabs(TAB_ANGLE + 1).Selected = True
                        Call txtDrctH.SetFocus
                        Exit Function
                    End If
                    If Not CheckIntegerInputRange(txtDrctM, "方位角(分)", 0, 60, fraTab(TAB_ANGLE).Visible) Then
                        tsTab.Tabs(TAB_ANGLE + 1).Selected = True
                        Call txtDrctM.SetFocus
                        Exit Function
                    End If
                    If Not CheckFloatInputRange(txtDrctS, "方位角(秒)", 0, 60, fraTab(TAB_ANGLE).Visible) Then
                        tsTab.Tabs(TAB_ANGLE + 1).Selected = True
                        Call txtDrctS.SetFocus
                        Exit Function
                    End If
        
                    '2009/11 H.Nakamura
                    If cmbUsePointDrct.ListIndex < 0 Then
                        Call MsgBox("偏心座標候補を選択してください。", vbCritical)
                        tsTab.Tabs(TAB_ANGLE + 1).Selected = True
                        Call cmbUsePointDrct.SetFocus
                        Exit Function
                    End If
                Else
                    Call MsgBox("角度を選択してください。", vbCritical)
                    tsTab.Tabs(TAB_ANGLE + 1).Selected = True
                    Call optHorizontal.SetFocus
                    Exit Function
                End If
    
                '斜距離。
                If Not CheckFloatInputRange(txtDistance, "斜距離", 0, 32768, fraTab(TAB_DISTANCE).Visible) Then
                    tsTab.Tabs(TAB_DISTANCE + 1).Selected = True
                    Call txtDistance.SetFocus
                    Exit Function
                End If
                If chkTS.Value = 1 Then
                    If Not CheckFloatInputRange(txtFromHeightTS, "測距儀高", 0, 32768, fraTab(TAB_DISTANCE).Visible) Then
                        tsTab.Tabs(TAB_DISTANCE + 1).Selected = True
                        Call txtFromHeightTS.SetFocus
                        Exit Function
                    End If
                    If Not CheckFloatInputRange(txtToHeightTS, "反射鏡高", 0, 32768, fraTab(TAB_DISTANCE).Visible) Then
                        tsTab.Tabs(TAB_DISTANCE + 1).Selected = True
                        Call txtToHeightTS.SetFocus
                        Exit Function
                    End If
                End If
    
                'B→C。
                If Not CheckIntegerInputRange(txtElevationBCH, "観測高度角(度)", -80, 80, fraTab(TAB_ELEVATION).Visible) Then
                    tsTab.Tabs(TAB_ELEVATION + 1).Selected = True
                    Call txtElevationBCH.SetFocus
                    Exit Function
                End If
                If Not CheckIntegerInputRange(txtElevationBCM, "観測高度角(分)", 0, 60, fraTab(TAB_ELEVATION).Visible) Then
                    tsTab.Tabs(TAB_ELEVATION + 1).Selected = True
                    Call txtElevationBCM.SetFocus
                    Exit Function
                End If
                If Not CheckFloatInputRange(txtElevationBCS, "観測高度角(秒)", 0, 60, fraTab(TAB_ELEVATION).Visible) Then
                    tsTab.Tabs(TAB_ELEVATION + 1).Selected = True
                    Call txtElevationBCS.SetFocus
                    Exit Function
                End If
                If Not CheckFloatInputRange(txtFromHeightBC, "器械高", 0, 32768, fraTab(TAB_ELEVATION).Visible) Then
                    tsTab.Tabs(TAB_ELEVATION + 1).Selected = True
                    Call txtFromHeightBC.SetFocus
                    Exit Function
                End If
                If Not CheckFloatInputRange(txtToHeightBC, "目標高", 0, 32768, fraTab(TAB_ELEVATION).Visible) Then
                    tsTab.Tabs(TAB_ELEVATION + 1).Selected = True
                    Call txtToHeightBC.SetFocus
                    Exit Function
                End If
    
                'C→B。
                If Not CheckIntegerInputRange(txtElevationCBH, "観測高度角(度)", -80, 80, fraTab(TAB_ELEVATION).Visible) Then
                    tsTab.Tabs(TAB_ELEVATION + 1).Selected = True
                    Call txtElevationCBH.SetFocus
                    Exit Function
                End If
                If Not CheckIntegerInputRange(txtElevationCBM, "観測高度角(分)", 0, 60, fraTab(TAB_ELEVATION).Visible) Then
                    tsTab.Tabs(TAB_ELEVATION + 1).Selected = True
                    Call txtElevationCBM.SetFocus
                    Exit Function
                End If
                If Not CheckFloatInputRange(txtElevationCBS, "観測高度角(秒)", 0, 60, fraTab(TAB_ELEVATION).Visible) Then
                    tsTab.Tabs(TAB_ELEVATION + 1).Selected = True
                    Call txtElevationCBS.SetFocus
                    Exit Function
                End If
                If Not CheckFloatInputRange(txtFromHeightCB, "器械高", 0, 32768, fraTab(TAB_ELEVATION).Visible) Then
                    tsTab.Tabs(TAB_ELEVATION + 1).Selected = True
                    Call txtFromHeightCB.SetFocus
                    Exit Function
                End If
                If Not CheckFloatInputRange(txtToHeightCB, "目標高", 0, 32768, fraTab(TAB_ELEVATION).Visible) Then
                    tsTab.Tabs(TAB_ELEVATION + 1).Selected = True
                    Call txtToHeightCB.SetFocus
                    Exit Function
                End If
    
                '偏心補正計算評価。
                Dim clsEccentricCorrectionParam As New EccentricCorrectionParam
                Call GetData(clsEccentricCorrectionParam)
                If Not mdlEccentricCorrection.CheckParame(clsEccentricCorrectionParam) Then
                    Call MsgBox("偏心補正の計算ができません。", vbCritical)
                    Exit Function
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
        /// 入力値が正常である場合 True を返す。
        /// それ以外の場合 False を返す。
        /// </returns>
        private bool CheckData()
        {
            bool CheckData = false;

            /*----------------------------------------
                '観測点No。
                If Not CheckFileNameInputLength(txtGenuineNumber, "観測点No", RINEX_STR_OBSNUMBER - 1) Then Exit Function
                '観測点名称。
                If Not CheckInputLength(txtGenuineName, "観測点名称", RINEX_STR_OBSNAME - 1) Then Exit Function
             */
            //************
            //観測点No。
            //************
            if (!CheckFileNameInputLength(txtGenuineNumber, "観測点No", RINEX_STR_OBSNUMBER - 1))
            {
                return CheckData;
            }
            //************
            //観測点名称。
            //************
            if (!CheckInputLength(txtGenuineName, "観測点名称", RINEX_STR_OBSNAME - 1))
            {
                return CheckData;
            }

            //************
            //角度。
            //************
            if (optHorizontal.Checked)
            {
                /*-------------------------------------------------------
                    '方位標。
                    If optGPS.Value = True Then
                        If cmbMarker.ListIndex < 0 Then
                            Call MsgBox("方位標を選択してください。", vbCritical)
                            tsTab.Tabs(TAB_ANGLE + 1).Selected = True
                            Call cmbMarker.SetFocus
                            Exit Function
                        End If
                    Else
                        If lblNo.Caption = "" Then
                            Call MsgBox("座標を入力してください。", vbCritical)
                            Exit Function
                        End If
            
                        '2009/11 H.Nakamura
                        If cmbUsePointHori.ListIndex < 0 Then
                            Call MsgBox("偏心座標候補を選択してください。", vbCritical)
                            tsTab.Tabs(TAB_ANGLE + 1).Selected = True
                            Call cmbUsePointHori.SetFocus
                            Exit Function
                        End If
                    End If
                 */
                if (optGPS.Checked)
                {
                    //***********
                    //方位標。
                    //***********
                    if (optGPS.Checked)
                    {
                        if (cmbMarker.SelectedIndex < 0)
                        {
                            //  Call MsgBox("方位標を選択してください。", vbCritical)
                            _ = MessageBox.Show("方位標を選択してください。", "エラー発生", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            //tsTab.Tabs(TAB_ANGLE + 1).Selected = True
                            cmbMarker.Focus();
                            return CheckData;
                        }
                    }
                    else
                    {
                        if (lblNo.Text == "")
                        {
                            //  Call MsgBox("座標を入力してください。", vbCritical)
                            _ = MessageBox.Show("座標を入力してください。", "エラー発生", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return CheckData;
                        }

                        //'2009/11 H.Nakamura
                        if (cmbUsePointHori.SelectedIndex < 0)
                        {
                            //  Call MsgBox("偏心座標候補を選択してください。", vbCritical)
                            _ = MessageBox.Show("偏心座標候補を選択してください。", "エラー発生", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            //不要    tsTab.Tabs[TAB_ANGLE + 1].Selected = true;
                            cmbUsePointHori.Focus();
                            return CheckData;
                        }
                        /*
                            '水平角。
                            If Not CheckIntegerInputRange(txtHoriH, "水平角(度)", 0, 360, fraTab(TAB_ANGLE).Visible) Then
                                tsTab.Tabs(TAB_ANGLE + 1).Selected = True
                                Call txtHoriH.SetFocus
                                Exit Function
                            End If
                            If Not CheckIntegerInputRange(txtHoriM, "水平角(分)", 0, 60, fraTab(TAB_ANGLE).Visible) Then
                                tsTab.Tabs(TAB_ANGLE + 1).Selected = True
                                Call txtHoriM.SetFocus
                                Exit Function
                            End If
                            If Not CheckFloatInputRange(txtHoriS, "水平角(秒)", 0, 60, fraTab(TAB_ANGLE).Visible) Then
                                tsTab.Tabs(TAB_ANGLE + 1).Selected = True
                                Call txtHoriS.SetFocus
                                Exit Function
                            End If
                        */
                        //************
                        // 水平角。
                        //************
                        if (!CheckIntegerInputRange(txtHoriH, "水平角(度)", 0, 360, fraTab0/*(TAB_NUMBER.TAB_ANGLE)*/.Visible))
                        {
                            //不要    tsTab.Tabs(TAB_ANGLE + 1).Selected = true;
                            txtHoriH_GotFocus();
                            return CheckData;
                        }
                        if (!CheckIntegerInputRange(txtHoriM, "水平角(分)", 0, 60, fraTab0/*(TAB_NUMBER.TAB_ANGLE)*/.Visible))
                        {
                            //不要    tsTab.Tabs(TAB_ANGLE + 1).Selected = True
                            txtHoriM_GotFocus();
                            return CheckData;
                        }
                        if (!CheckFloatInputRange(txtHoriS, "水平角(秒)", 0, 60, fraTab0/*(TAB_NUMBER.TAB_ANGLE)*/.Visible))
                        {
                            //不要    tsTab.Tabs(TAB_ANGLE + 1).Selected = True
                            txtHoriS_GotFocus();
                            return CheckData;
                        }
                    }
                }

            }
            else if (optDirection.Checked)
            {
                /*-------------------------------------------------
                    '方位角。
                    If Not CheckIntegerInputRange(txtDrctH, "方位角(度)", 0, 360, fraTab(TAB_ANGLE).Visible) Then
                        tsTab.Tabs(TAB_ANGLE + 1).Selected = True
                        Call txtDrctH.SetFocus
                        Exit Function
                    End If
                    If Not CheckIntegerInputRange(txtDrctM, "方位角(分)", 0, 60, fraTab(TAB_ANGLE).Visible) Then
                        tsTab.Tabs(TAB_ANGLE + 1).Selected = True
                        Call txtDrctM.SetFocus
                        Exit Function
                    End If
                    If Not CheckFloatInputRange(txtDrctS, "方位角(秒)", 0, 60, fraTab(TAB_ANGLE).Visible) Then
                        tsTab.Tabs(TAB_ANGLE + 1).Selected = True
                        Call txtDrctS.SetFocus
                        Exit Function
                    End If
        
                    '2009/11 H.Nakamura
                    If cmbUsePointDrct.ListIndex < 0 Then
                        Call MsgBox("偏心座標候補を選択してください。", vbCritical)
                        tsTab.Tabs(TAB_ANGLE + 1).Selected = True
                        Call cmbUsePointDrct.SetFocus
                        Exit Function
                    End If
                 */
                //***********
                // 方位角。
                //***********
                if (!CheckIntegerInputRange(txtDrctH, "方位角(度)", 0, 360, fraTab0/*(TAB_NUMBER.TAB_ANGLE)*/.Visible))
                {
                    //不要    tsTab.Tabs(TAB_ANGLE + 1).Selected = True
                    txtDrctH_GotFocus();
                    return CheckData;
                }
                if (!CheckIntegerInputRange(txtDrctM, "方位角(分)", 0, 60, fraTab0/*(TAB_NUMBER.TAB_ANGLE)*/.Visible))
                {
                    //不要    tsTab.Tabs(TAB_ANGLE + 1).Selected = True
                    txtDrctM_GotFocus();
                    return CheckData;
                }
                if (!CheckFloatInputRange(txtDrctS, "方位角(秒)", 0, 60, fraTab0/*(TAB_NUMBER.TAB_ANGLE)*/.Visible))
                {
                    //不要    tsTab.Tabs(TAB_ANGLE + 1).Selected = True
                    txtDrctS_GotFocus();
                    return CheckData;
                }

                //'2009/11 H.Nakamura
                if (cmbUsePointDrct.SelectedIndex < 0)
                {
                    //      Call MsgBox("偏心座標候補を選択してください。", vbCritical)
                    _ = MessageBox.Show("偏心座標候補を選択してください。", "エラー発生", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //不要  tsTab.Tabs(TAB_ANGLE + 1).Selected = True
                    cmbUsePointDrct.Focus();
                    return CheckData;
                }

            }
            else
            {
                //Call MsgBox("角度を選択してください。", vbCritical)
                _ = MessageBox.Show("角度を選択してください。", "エラー発生", MessageBoxButtons.OK, MessageBoxIcon.Error);

                //不要    tsTab.Tabs(TAB_ANGLE + 1).Selected = true;
                optHorizontal.Focus();
                return CheckData;

            }

            /*---------------------------------------------------
                '斜距離。
                If Not CheckFloatInputRange(txtDistance, "斜距離", 0, 32768, fraTab(TAB_DISTANCE).Visible) Then
                    tsTab.Tabs(TAB_DISTANCE + 1).Selected = True
                    Call txtDistance.SetFocus
                    Exit Function
                End If
                If chkTS.Value = 1 Then
                    If Not CheckFloatInputRange(txtFromHeightTS, "測距儀高", 0, 32768, fraTab(TAB_DISTANCE).Visible) Then
                        tsTab.Tabs(TAB_DISTANCE + 1).Selected = True
                        Call txtFromHeightTS.SetFocus
                        Exit Function
                    End If
                    If Not CheckFloatInputRange(txtToHeightTS, "反射鏡高", 0, 32768, fraTab(TAB_DISTANCE).Visible) Then
                        tsTab.Tabs(TAB_DISTANCE + 1).Selected = True
                        Call txtToHeightTS.SetFocus
                        Exit Function
                    End If
                End If
             */
            //**********************
            //  斜距離。
            //**********************
            if (!CheckFloatInputRange(txtDistance, "斜距離", 0, 32768, fraTab1/*(TAB_NUMBER.TAB_DISTANCE)*/.Visible))
            {
                //不要 tsTab.Tabs(TAB_DISTANCE + 1).Selected = true;
                txtDistance_GotFocus();
                return CheckData;
            }
            if (chkTS.Checked)
            {
                if (!CheckFloatInputRange(txtFromHeightTS, "測距儀高", 0, 32768, fraTab1/*(TAB_NUMBER.TAB_DISTANCE)*/.Visible))
                {
                    //  tsTab.Tabs(TAB_DISTANCE + 1).Selected = true;
                    txtFromHeightTS_GotFocus();
                    return CheckData;
                }
                if (!CheckFloatInputRange(txtToHeightTS, "反射鏡高", 0, 32768, fraTab1/*(TAB_NUMBER.TAB_DISTANCE)*/.Visible))
                {
                    //不要 tsTab.Tabs(TAB_DISTANCE + 1).Selected = true;
                    txtToHeightTS_GotFocus();
                    return CheckData;
                }

            }//if (chkTS.Checked)

            /*----------------------------------------------------
                'B→C。
                If Not CheckIntegerInputRange(txtElevationBCH, "観測高度角(度)", -80, 80, fraTab(TAB_ELEVATION).Visible) Then
                    tsTab.Tabs(TAB_ELEVATION + 1).Selected = True
                    Call txtElevationBCH.SetFocus
                    Exit Function
                End If
                If Not CheckIntegerInputRange(txtElevationBCM, "観測高度角(分)", 0, 60, fraTab(TAB_ELEVATION).Visible) Then
                    tsTab.Tabs(TAB_ELEVATION + 1).Selected = True
                    Call txtElevationBCM.SetFocus
                    Exit Function
                End If
                If Not CheckFloatInputRange(txtElevationBCS, "観測高度角(秒)", 0, 60, fraTab(TAB_ELEVATION).Visible) Then
                    tsTab.Tabs(TAB_ELEVATION + 1).Selected = True
                    Call txtElevationBCS.SetFocus
                    Exit Function
                End If
                If Not CheckFloatInputRange(txtFromHeightBC, "器械高", 0, 32768, fraTab(TAB_ELEVATION).Visible) Then
                    tsTab.Tabs(TAB_ELEVATION + 1).Selected = True
                    Call txtFromHeightBC.SetFocus
                    Exit Function
                End If
                If Not CheckFloatInputRange(txtToHeightBC, "目標高", 0, 32768, fraTab(TAB_ELEVATION).Visible) Then
                    tsTab.Tabs(TAB_ELEVATION + 1).Selected = True
                    Call txtToHeightBC.SetFocus
                    Exit Function
                End If

             */
            //**********************
            //  'B→C。
            //**********************
            if (!CheckIntegerInputRange(txtElevationBCH, "観測高度角(度)", -80, 80, fraTab2/*(TAB_NUMBER.TAB_ELEVATION)*/.Visible))
            {
                //不要    tsTab.Tabs(TAB_ELEVATION + 1).Selected = true;
                txtElevationBCH_GotFocus();
                return CheckData;
            }
            if (!CheckIntegerInputRange(txtElevationBCM, "観測高度角(分)", 0, 60, fraTab2/*(TAB_NUMBER.TAB_ELEVATION)*/.Visible))
            {
                //不要    tsTab.Tabs(TAB_ELEVATION + 1).Selected = true;
                txtElevationBCM_GotFocus();
                return CheckData;
            }
            if (!CheckFloatInputRange(txtElevationBCS, "観測高度角(秒)", 0, 60, fraTab2/*(TAB_NUMBER.TAB_ELEVATION)*/.Visible))
            {
                //不要    tsTab.Tabs(TAB_ELEVATION + 1).Selected = true;
                txtElevationBCS_GotFocus();
                return CheckData;
            }
            if (!CheckFloatInputRange(txtFromHeightBC, "器械高", 0, 32768, fraTab2/*(TAB_NUMBER.TAB_ELEVATION)*/.Visible))
            {
                //不要    tsTab.Tabs(TAB_ELEVATION + 1).Selected = true;
                txtFromHeightBC_GotFocus();
                return CheckData;
            }
            if (!CheckFloatInputRange(txtToHeightBC, "目標高", 0, 32768, fraTab2/*(TAB_NUMBER.TAB_ELEVATION)*/.Visible))
            {
                //不要    tsTab.Tabs(TAB_ELEVATION + 1).Selected = true;
                txtToHeightBC_GotFocus();
                return CheckData;
            }

            /*----------------------------------------------------
                'C→B。
                If Not CheckIntegerInputRange(txtElevationCBH, "観測高度角(度)", -80, 80, fraTab(TAB_ELEVATION).Visible) Then
                    tsTab.Tabs(TAB_ELEVATION + 1).Selected = True
                    Call txtElevationCBH.SetFocus
                    Exit Function
                End If
                If Not CheckIntegerInputRange(txtElevationCBM, "観測高度角(分)", 0, 60, fraTab(TAB_ELEVATION).Visible) Then
                    tsTab.Tabs(TAB_ELEVATION + 1).Selected = True
                    Call txtElevationCBM.SetFocus
                    Exit Function
                End If
                If Not CheckFloatInputRange(txtElevationCBS, "観測高度角(秒)", 0, 60, fraTab(TAB_ELEVATION).Visible) Then
                    tsTab.Tabs(TAB_ELEVATION + 1).Selected = True
                    Call txtElevationCBS.SetFocus
                    Exit Function
                End If
                If Not CheckFloatInputRange(txtFromHeightCB, "器械高", 0, 32768, fraTab(TAB_ELEVATION).Visible) Then
                    tsTab.Tabs(TAB_ELEVATION + 1).Selected = True
                    Call txtFromHeightCB.SetFocus
                    Exit Function
                End If
                If Not CheckFloatInputRange(txtToHeightCB, "目標高", 0, 32768, fraTab(TAB_ELEVATION).Visible) Then
                    tsTab.Tabs(TAB_ELEVATION + 1).Selected = True
                    Call txtToHeightCB.SetFocus
                    Exit Function
                End If
            */
            //************
            //  C→B。
            //************
            if (!CheckIntegerInputRange(txtElevationCBH, "観測高度角(度)", -80, 80, fraTab2/*(TAB_NUMBER.TAB_ELEVATION)*/.Visible))
            {
                //不要    tsTab.Tabs(TAB_ELEVATION + 1).Selected = true;
                txtElevationCBH_GotFocus();
                return CheckData;
            }
            if (!CheckIntegerInputRange(txtElevationCBM, "観測高度角(分)", 0, 60, fraTab2/*(TAB_NUMBER.TAB_ELEVATION)*/.Visible))
            {
                //不要    tsTab.Tabs(TAB_ELEVATION + 1).Selected = true;
                txtElevationCBM_GotFocus();
                return CheckData;
            }
            if (!CheckFloatInputRange(txtElevationCBS, "観測高度角(秒)", 0, 60, fraTab2/*(TAB_NUMBER.TAB_ELEVATION)*/.Visible))
            {
                //不要    tsTab.Tabs(TAB_ELEVATION + 1).Selected = true;
                txtElevationCBS_GotFocus();
                return CheckData;
            }
            if (!CheckFloatInputRange(txtFromHeightCB, "器械高", 0, 32768, fraTab2/*(TAB_NUMBER.TAB_ELEVATION)*/.Visible))
            {
                //不要    tsTab.Tabs(TAB_ELEVATION + 1).Selected = true;
                txtFromHeightCB.Focus();
                return CheckData;
            }
            if (!CheckFloatInputRange(txtToHeightCB, "目標高", 0, 32768, fraTab2/*(TAB_NUMBER.TAB_ELEVATION)*/.Visible))
            {
                //不要    tsTab.Tabs(TAB_ELEVATION + 1).Selected = true;
                txtToHeightCB_GotFocus();
                return CheckData;
            }

            /*
            '偏心補正計算評価。
                Dim clsEccentricCorrectionParam As New EccentricCorrectionParam
                Call GetData(clsEccentricCorrectionParam)
                If Not mdlEccentricCorrection.CheckParame(clsEccentricCorrectionParam) Then
                    Call MsgBox("偏心補正の計算ができません。", vbCritical)
                    Exit Function
                End If
                */
            EccentricCorrectionParam clsEccentricCorrectionParam = new EccentricCorrectionParam(m_mdlMain);
            GetData(clsEccentricCorrectionParam);
            if (!CheckParame(clsEccentricCorrectionParam))
            {
                //Call MsgBox("偏心補正の計算ができません。", vbCritical)
                return CheckData;

            }


            CheckData = true;
            return CheckData;
        }

        //6==========================================================================================
        /*[VB]
            '値を反映させる。
            Private Sub ReflectData()
                '値の取得。
                Call GetData(m_clsEccentricCorrectionParam)
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
            GetData(m_clsEccentricCorrectionParam);
        }


        //6==========================================================================================
        /*[VB]
            '値を取得する。
            '
            'clsEccentricCorrectionParam に入力値の内容を設定する。
            '
            '引き数：
            'clsEccentricCorrectionParam 偏心補正パラメータ。
            Private Sub GetData(ByVal clsEccentricCorrectionParam As EccentricCorrectionParam)
                '値の取得。
                GenuineNumber = txtGenuineNumber.Text
                GenuineName = txtGenuineName.Text
                If optHorizontal.Value Then
                    If optGPS.Value = True Then
                        clsEccentricCorrectionParam.AngleType = ANGLETYPE_HORIZONTAL
                        clsEccentricCorrectionParam.Marker = m_clsMarkers(cmbMarker.ListIndex).Key
                    Else    '手入力の場合　2007/3/14 NGS Yamada
                        clsEccentricCorrectionParam.AngleType = ANGLETYPE_MARK
                        With m_clsEccentricCorrectionParam
                            clsEccentricCorrectionParam.MarkNumber = .MarkNumber
                            clsEccentricCorrectionParam.MarkName = .MarkName
                            clsEccentricCorrectionParam.MarkEditCode = .MarkEditCode
                            clsEccentricCorrectionParam.MarkLat = .MarkLat
                            clsEccentricCorrectionParam.MarkLon = .MarkLon
                            clsEccentricCorrectionParam.MarkX = .MarkX
                            clsEccentricCorrectionParam.MarkY = .MarkY
                            clsEccentricCorrectionParam.MarkHeight = .MarkHeight
                            clsEccentricCorrectionParam.MarkAlt = .MarkAlt
                        End With
                        clsEccentricCorrectionParam.UsePoint = m_clsMarkers(cmbUsePointHori.ListIndex).Key '2009/11 H.Nakamura
                    End If
                    clsEccentricCorrectionParam.Horizontal = dms_to_d(Val(txtHoriH.Text), Val(txtHoriM.Text), Val(txtHoriS.Text)) / 180 * PAI
                Else
                    clsEccentricCorrectionParam.AngleType = ANGLETYPE_DIRECTION
                    clsEccentricCorrectionParam.Direction = dms_to_d(Val(txtDrctH.Text), Val(txtDrctM.Text), Val(txtDrctS.Text)) / 180 * PAI
                    clsEccentricCorrectionParam.UsePoint = m_clsMarkers(cmbUsePointDrct.ListIndex).Key '2009/11 H.Nakamura
                End If
                clsEccentricCorrectionParam.Distance = Val(txtDistance.Text)
                clsEccentricCorrectionParam.UseTS = (chkTS.Value = 1)
                If chkTS.Value = 1 Then
                    clsEccentricCorrectionParam.FromHeightTS = Val(txtFromHeightTS.Text)
                    clsEccentricCorrectionParam.ToHeightTS = Val(txtToHeightTS.Text)
                End If
                clsEccentricCorrectionParam.ElevationBC = dms_to_d(Abs(Val(txtElevationBCH.Text)), Val(txtElevationBCM.Text), Val(txtElevationBCS.Text)) / 180 * PAI * IIf(Asc(LTrim(txtElevationBCH.Text)) = 45, -1, 1)
                clsEccentricCorrectionParam.ElevationCB = dms_to_d(Abs(Val(txtElevationCBH.Text)), Val(txtElevationCBM.Text), Val(txtElevationCBS.Text)) / 180 * PAI * IIf(Asc(LTrim(txtElevationCBH.Text)) = 45, -1, 1)
                clsEccentricCorrectionParam.FromHeightBC = Val(txtFromHeightBC.Text)
                clsEccentricCorrectionParam.ToHeightBC = Val(txtToHeightBC.Text)
                clsEccentricCorrectionParam.FromHeightCB = Val(txtFromHeightCB.Text)
                clsEccentricCorrectionParam.ToHeightCB = Val(txtToHeightCB.Text)
            End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#] //6
        /// <summary>
        /// 値を取得する。
        /// '
        /// clsEccentricCorrectionParam に入力値の内容を設定する。
        /// '
        /// 引き数：
        /// clsEccentricCorrectionParam 偏心補正パラメータ。
        /// 
        /// </summary>
        /// <param name="clsEccentricCorrectionParam"></param>
        private void GetData(EccentricCorrectionParam clsEccentricCorrectionParam)
        {
            //値の取得。
            GenuineNumber = txtGenuineNumber.Text;
            GenuineName = txtGenuineName.Text;
            if (optHorizontal.Checked)
            {
                if (optGPS.Checked)
                {
                    clsEccentricCorrectionParam.AngleType = (long)ANGLE_TYPE.ANGLETYPE_HORIZONTAL;
                    clsEccentricCorrectionParam.Marker = m_clsMarkers[cmbMarker.SelectedIndex].Key();
                }
                else
                {
                    //'手入力の場合　2007/3/14 NGS Yamada
                    clsEccentricCorrectionParam.AngleType = (long)ANGLE_TYPE.ANGLETYPE_MARK;
                    //    With m_clsEccentricCorrectionParam
                    {
                        clsEccentricCorrectionParam.MarkNumber = m_clsEccentricCorrectionParam.MarkNumber;
                        clsEccentricCorrectionParam.MarkName = m_clsEccentricCorrectionParam.MarkName;
                        clsEccentricCorrectionParam.MarkEditCode = m_clsEccentricCorrectionParam.MarkEditCode;
                        clsEccentricCorrectionParam.MarkLat = m_clsEccentricCorrectionParam.MarkLat;
                        clsEccentricCorrectionParam.MarkLon = m_clsEccentricCorrectionParam.MarkLon;
                        clsEccentricCorrectionParam.MarkX = m_clsEccentricCorrectionParam.MarkX;
                        clsEccentricCorrectionParam.MarkY = m_clsEccentricCorrectionParam.MarkY;
                        clsEccentricCorrectionParam.MarkHeight = m_clsEccentricCorrectionParam.MarkHeight;
                        clsEccentricCorrectionParam.MarkAlt = m_clsEccentricCorrectionParam.MarkAlt;
                    }
                    clsEccentricCorrectionParam.UsePoint = m_clsMarkers[cmbUsePointHori.SelectedIndex].Key();     //'2009/11 H.Nakamura
                }
                clsEccentricCorrectionParam.Horizontal = dms_to_d(int.Parse(txtHoriH.Text), int.Parse(txtHoriM.Text), double.Parse(txtHoriS.Text)) / 180 * PAI;
            }
            else
            {
                /*-------------------------------------------
                    clsEccentricCorrectionParam.AngleType = ANGLETYPE_DIRECTION
                    clsEccentricCorrectionParam.Direction = dms_to_d(Val(txtDrctH.Text), Val(txtDrctM.Text), Val(txtDrctS.Text)) / 180 * PAI
                    clsEccentricCorrectionParam.UsePoint = m_clsMarkers(cmbUsePointDrct.ListIndex).Key '2009/11 H.Nakamura
                 */
                clsEccentricCorrectionParam.AngleType = (long)ANGLE_TYPE.ANGLETYPE_DIRECTION;
                clsEccentricCorrectionParam.Direction = dms_to_d(int.Parse(txtDrctH.Text), int.Parse(txtDrctM.Text), double.Parse(txtDrctS.Text)) / 180 * PAI;
                clsEccentricCorrectionParam.UsePoint = m_clsMarkers[cmbUsePointDrct.SelectedIndex].Key();   //'2009/11 H.Nakamura
            }

            clsEccentricCorrectionParam.Distance = double.Parse(txtDistance.Text);
            if (chkTS.Checked)
            {
                clsEccentricCorrectionParam.UseTS = true;
            }
            else
            {
                clsEccentricCorrectionParam.UseTS = false;
            }
            if (chkTS.Checked)
            {
                clsEccentricCorrectionParam.FromHeightTS = double.Parse(txtFromHeightTS.Text);
                clsEccentricCorrectionParam.ToHeightTS = double.Parse(txtToHeightTS.Text);
            }

            double s1 = Convert.ToInt32(txtElevationBCH.Text.TrimStart()) == 45 ? -1 : 1;
            double s2 = Convert.ToInt32(txtElevationCBH.Text.TrimStart()) == 45 ? -1 : 1;
            clsEccentricCorrectionParam.ElevationBC = dms_to_d(Math.Abs(int.Parse(txtElevationBCH.Text)), int.Parse(txtElevationBCM.Text), double.Parse(txtElevationBCS.Text)) / 180 * PAI * s1;
            clsEccentricCorrectionParam.ElevationCB = dms_to_d(Math.Abs(int.Parse(txtElevationCBH.Text)), int.Parse(txtElevationCBM.Text), double.Parse(txtElevationCBS.Text)) / 180 * PAI * s2;
            clsEccentricCorrectionParam.FromHeightBC = double.Parse(txtFromHeightBC.Text);
            clsEccentricCorrectionParam.ToHeightBC = double.Parse(txtToHeightBC.Text);
            clsEccentricCorrectionParam.FromHeightCB = double.Parse(txtFromHeightCB.Text);
            clsEccentricCorrectionParam.ToHeightCB = double.Parse(txtToHeightCB.Text);

        }

        //6==========================================================================================
        /*[VB]
            '手入力の方位標を情報を更新
            Private Sub UpdateMark()
                With m_clsEccentricCorrectionParam
                    If .MarkNumber <> "" Then
                        lblNo.Caption = .MarkNumber & "(" & .MarkName & ")"
                    Else
                        lblNo.Caption = ""
                    End If
        
                    If .MarkEditCode And EDITCODE_COORD_DMS Then
                        Dim nH As Long
                        Dim nM As Long
                        Dim nS As Double
                        Call d_to_dms_decimal(.MarkLat, nH, nM, nS, GUI_SEC_DECIMAL)
                        lblX.Caption = "緯度: " & CStr(nH) & "°" & CStr(nM) & "′" & FormatRound0Trim(nS, GUI_SEC_DECIMAL) & "″"
                        Call d_to_dms_decimal(.MarkLon, nH, nM, nS, GUI_SEC_DECIMAL)
                        lblY.Caption = "経度:" & CStr(nH) & "°" & CStr(nM) & "′" & FormatRound0Trim(nS, GUI_SEC_DECIMAL) & "″"
                    ElseIf .MarkEditCode And EDITCODE_COORD_JGD Then
                        lblX.Caption = "X:" & FormatRound0Trim(.MarkX, GUI_JGD_DECIMAL) & "m"
                        lblY.Caption = "Y:" & FormatRound0Trim(.MarkY, GUI_JGD_DECIMAL) & "m"
                    Else
                        lblX.Caption = ""
                        lblY.Caption = ""
                    End If
        
                    '高さ
                    If .MarkEditCode = EDITCODE_VERT_HEIGHT Then
                        lblH.Caption = "楕円体高:" & FormatRound0Trim(.MarkHeight, GUI_JGD_DECIMAL) & "m"
                    ElseIf .MarkEditCode = EDITCODE_VERT_ALT Then
                        lblH.Caption = "標高:" & FormatRound0Trim(.MarkAlt, GUI_JGD_DECIMAL) & "m"
                    Else
                        lblH.Caption = ""
                    End If
                End With
            End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#] //6
        /// <summary>
        /// 手入力の方位標を情報を更新
        /// 
        /// </summary>
        private void UpdateMark()
        {
            //  With m_clsEccentricCorrectionParam
            {
                if (m_clsEccentricCorrectionParam.MarkNumber != "")
                {
                    lblNo.Text = m_clsEccentricCorrectionParam.MarkNumber + "(" + m_clsEccentricCorrectionParam.MarkName + ")";
                }
                else
                {
                    lblNo.Text = "";
                }
                if ((m_clsEccentricCorrectionParam.MarkEditCode & (long)EDITCODE_STYLE.EDITCODE_COORD_DMS) > 0)
                {
                    int nH = 0;
                    int nM = 0;
                    double nS = 0;
                    d_to_dms_decimal(m_clsEccentricCorrectionParam.MarkLat, ref nH, ref nM, ref nS, (int)GUI_SEC_DECIMAL);
                    lblX.Text = "緯度: " + nH.ToString() + "°" + nM.ToString() + "′" + FormatRound0Trim(nS, GUI_SEC_DECIMAL) + "″";
                    d_to_dms_decimal(m_clsEccentricCorrectionParam.MarkLon, ref nH, ref nM, ref nS, (int)GUI_SEC_DECIMAL);
                    lblY.Text = "経度:" + nH.ToString() + "°" + nM.ToString() + "′" + FormatRound0Trim(nS, GUI_SEC_DECIMAL) + "″";
                }
                else if ((m_clsEccentricCorrectionParam.MarkEditCode & (long)EDITCODE_STYLE.EDITCODE_COORD_JGD) > 0)
                {
                    lblX.Text = "X:" + FormatRound0Trim(m_clsEccentricCorrectionParam.MarkX, GUI_JGD_DECIMAL) + "m";
                    lblY.Text = "Y:" + FormatRound0Trim(m_clsEccentricCorrectionParam.MarkY, GUI_JGD_DECIMAL) + "m";
                }
                else
                {
                    lblX.Text = "";
                    lblY.Text = "";
                }

                //高さ
                if (m_clsEccentricCorrectionParam.MarkEditCode == (long)EDITCODE_STYLE.EDITCODE_VERT_HEIGHT)
                {
                    lblH.Text = "楕円体高:" + FormatRound0Trim(m_clsEccentricCorrectionParam.MarkHeight, GUI_JGD_DECIMAL) + "m";
                }
                else if (m_clsEccentricCorrectionParam.MarkEditCode == (long)EDITCODE_STYLE.EDITCODE_VERT_ALT)
                {
                    lblH.Text = "標高:" + FormatRound0Trim(m_clsEccentricCorrectionParam.MarkAlt, GUI_JGD_DECIMAL) + "m";
                }
                else
                {
                    lblH.Text = "";
                }
            }
        }

        //6==========================================================================================
        /*[VB]
            '2009/11 H.Nakamura
            '偏心座標候補リストの作成。
            Private Sub MakeUsePoint(ByVal cmbUsePoint As ComboBox)
                Dim i As Long
                For i = 0 To UBound(m_clsMarkers)
                    Dim sPoint As String
                    If m_clsMarkers(i).StrPoint.Number = m_clsObservationPoint.Number Then
                        sPoint = " 始点"
                    Else
                        sPoint = " 終点"
                    End If
                    Call cmbUsePoint.AddItem(m_clsMarkers(i).StrPoint.Number & "(" & m_clsMarkers(i).StrPoint.Name & ")-" & m_clsMarkers(i).EndPoint.Number & "(" & m_clsMarkers(i).EndPoint.Name & ") " & m_clsMarkers(i).Session & sPoint)
                Next
            End Sub
           [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#] //6
        /// <summary>
        /// 偏心座標候補リストの作成。
        /// 
        /// </summary>
        /// <param name="cmbUsePoint"></param>
        private void MakeUsePoint(ComboBox cmbUsePoint)
        {
            int i;
            for (i = 0; i <m_clsMarkers.Count; i++)
            {
                string sPoint;
                if (m_clsMarkers[i].StrPoint().Number() == m_clsObservationPoint.Number())
                {
                    sPoint = " 始点";
                }
                else
                {
                    sPoint = " 終点";
                }
                cmbUsePoint.Items.Add(m_clsMarkers[i].StrPoint().Number() + "(" + m_clsMarkers[i].StrPoint().Name() + ")-" + m_clsMarkers[i].EndPoint().Number() + "(" + m_clsMarkers[i].EndPoint().Name() + ") " + m_clsMarkers[i].Session + sPoint);
            }

        }

#if false
        //==========================================================================================
        /*[VB]
        '範囲付きで整数入力値を検査する。
        '
        'nMin以上、nMax未満を許可。
        '
        '引き数：
        'txtTextBox 検査対象コントロール。
        'sLabel 対象コントロールの名称。
        'nMin 最小値。
        'nMax 最大値。
        'bFocus 検査に引っかかった場合フォーカスを移すか？
        '
        '戻り値：
        '入力値が正常である場合 True を返す。
        'それ以外の場合 False を返す。
        Public Function CheckIntegerInputRange(ByVal txtTextBox As Object, ByVal sLabel As String, ByVal nMin As Long, ByVal nMax As Long, Optional ByVal bFocus As Boolean = True) As Boolean

            CheckIntegerInputRange = False
    
            '整数入力であるか？
            If Not CheckIntegerInput(txtTextBox, sLabel, bFocus) Then Exit Function
    
            '範囲内か？
            Dim nValue As Long
            nValue = Val(txtTextBox.Text)
            If nValue < nMin Or nMax <= nValue Then
                Call MsgBox(MakeRangeMessage(CStr(nMin), CStr(nMax), sLabel), vbCritical)
                If bFocus Then Call txtTextBox.SetFocus
                Exit Function
            End If
    
            CheckIntegerInputRange = True
    
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]  mdlGUIからコピー
        /// <summary>
        /// 範囲付きで整数入力値を検査する。
        /// 
        /// nMin以上、nMax未満を許可。
        /// 
        /// 引き数：
        /// txtTextBox 検査対象コントロール。
        /// sLabel 対象コントロールの名称。
        /// nMin 最小値。
        /// nMax 最大値。
        /// bFocus 検査に引っかかった場合フォーカスを移すか？
        /// 
        /// </summary>
        /// <param name="txtTextBox"></param>
        /// <param name="sLabel"></param>
        /// <param name="nMin"></param>
        /// <param name="nMax"></param>
        /// <param name="bFocus"></param>
        /// <returns>
        /// 戻り値：
        /// 入力値が正常である場合 True を返す。
        /// それ以外の場合 False を返す。
        /// </returns>
        public static bool CheckIntegerInputRange(TextBox txtTextBox, string sLabel, long nMin, long nMax, bool bFocus = true)
        {
            bool CheckIntegerInputRange = false;


            //'整数入力であるか？
            if (!CheckIntegerInput(txtTextBox, sLabel, bFocus))
            {
                return CheckIntegerInputRange;
            }

            //'範囲内か？
            long nValue;
            nValue = long.Parse(txtTextBox.Text);
            if (nValue < nMin || nMax <= nValue)
            {
                _ = MessageBox.Show(MakeRangeMessage(nMin.ToString(), nMax.ToString(), sLabel), "エラー発生", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (bFocus)
                {
                    _ = txtTextBox.Focus();
                    return CheckIntegerInputRange;
                }
            }

            CheckIntegerInputRange = true;
            return CheckIntegerInputRange;

        }
#endif
        //==========================================================================================
        /*[VB]
            'テキストボックスのテキストすべてを選択状態にする。
            '
            '引き数：
            'txtTextBox 対象コントロール。
            Public Sub SelectText(ByVal txtTextBox As Object)
                txtTextBox.SelStart = 0
                txtTextBox.SelLength = Len(txtTextBox.Text)
            End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#] mdlGUIからコピー
        /// <summary>
        /// テキストボックスのテキストすべてを選択状態にする。
        /// 
        /// </summary>
        /// <param name="txtTextBox"></param>
        public void SelectText(TextBox txtTextBox)    //2
        {
            txtTextBox.SelectionStart = 0;
            txtTextBox.SelectionLength = txtTextBox.Text.Length;
        }




        //------------------------------------------------------------------------------------------

    }
}
