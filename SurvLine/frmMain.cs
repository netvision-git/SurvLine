using SurvLine.mdl;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using static SurvLine.mdl.DEFINE;
using static SurvLine.mdl.MdlMain;
using static SurvLine.mdl.MdlNSDefine;
using static SurvLine.mdl.MdlAccountMakeNSS;
using static SurvLine.mdl.MdlListPane;
using static SurvLine.mdl.MdlBaseLineAnalyser;
using static SurvLine.mdl.MdlUtility;
using static SurvLine.mdl.MdlSession;   //2

using Microsoft.VisualBasic.Logging;
using System.Xml.Linq;

namespace SurvLine
{
    public partial class frmMain : Form
    {
        IniFileControl iniFileControl = new IniFileControl();

        //  [DllImport("user32.dll")]
        //  private static extern bool RedrawWindow(IntPtr hWnd, IntPtr lprcUpdate, IntPtr hrgnUpdate, int flags);

        public string receiveData = "";
        //MdlUtility Utility = new MdlUtility();

        public frmMain()
        {
            //PlotNative clsPlotNative = new PlotNative();
            //clsPlotNative.Class_Terminate();

            InitializeComponent();

            Form_Load();

            MdlMain mdlMain = new MdlMain();
            mdlMain.Main();

        }


        //==========================================================================================
        /*[VB]
        '*******************************************************************************
        'メイン画面

        Option Explicit
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '定数
        Private Const SPLITTER_SIZE As Long = 4 'スプリッターのサイズ(ピクセル)。

        Private Enum STATUSBAR_NUM
            STATUSBAR_NUM_STANDARD = 0 'ステータスバー番号、標準。
            STATUSBAR_NUM_COORDNUM 'ステータスバー番号、座標系。
            STATUSBAR_NUM_GEOIDO 'ステータスバー番号、ジオイド。
            STATUSBAR_NUM_COUNT 'ステータスバー番号数。
        End Enum
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'定数
        private const long SPLITTER_SIZE = 4;   //'スプリッターのサイズ(ピクセル)。

        private enum STATUSBAR_NUM
        {
            STATUSBAR_NUM_STANDARD = 0,         //'ステータスバー番号、標準。
            STATUSBAR_NUM_COORDNUM,             //'ステータスバー番号、座標系。
            STATUSBAR_NUM_GEOIDO,               //'ステータスバー番号、ジオイド。
            STATUSBAR_NUM_COUNT,                //'ステータスバー番号数。
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'インプリメンテーション
        Private WithEvents m_clsDocument As Document 'ドキュメント。
        Private m_clsSplitHori As SplitController '左右スプリットコントローラー。
        Private m_clsSplitVert As SplitController '上下スプリットコントローラー。
        Private m_bResize As Boolean 'クールバーリサイズフラグ。クールバーリサイズでスプリッターのリサイズを制御。
        Private m_clsChangeSelRow As New CtrlValue '行選択イベント有効/無効。
        Private m_clsPopupMenuController As New PopupMenuController 'ポップアップメニューコントローラ。
        Private m_clsSproObject As New SproObject 'SuperPro オブジェクト。
        Private m_clsSproDataApp As New SproDataApp 'アプリケーション SproData。
        Private m_clsSproDataCadastral As New SproDataCadastral '地籍測量 SproData。
        Private m_clsLicenseCheck As New LicenseCheck   '2006/12/8 NGS Yamada

        '2008/10/14 NGS Yamada
        'ユーザデータを管理するフォルダのパスを追加
        'パスの変更は、NS-Surveyの起動時のみに反省させるため、2つに別ける。
        '各処理はこちらは参照。Documentは設定用
        'form_loadで１度だけ同期を取る。
        Public UserDataPath As String
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'インプリメンテーション
        //private WithEvents m_clsDocument As Document                //'ドキュメント。
        private Document m_clsDocument;                             //'ドキュメント。

        //==========================================================================================
        /*[VB]
        Private m_clsFileDialog As FileDialog 'ファイルダイアログ。
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        private FileDialog m_clsFileDialog;     //'ファイルダイアログ。



        private MdlMain m_clsMdlMain;
        private List<GENBA_STRUCT_S> m_List_Genba_S;                 //現場リスト

        private MdlGUI m_clsMdlGUI;

#if false
        /*
            *************************** 修正要 sakai
        */
        private SplitController m_clsSplitHori;                     //'左右スプリットコントローラー。
        private SplitController m_clsSplitVert;                     //'上下スプリットコントローラー。
#endif
        public bool m_bResize;                                      //'クールバーリサイズフラグ。クールバーリサイズでスプリッターのリサイズを制御。
        private CtrlValue m_clsChangeSelRow = new CtrlValue();       //'行選択イベント有効/無効。
#if false
        /*
            *************************** 修正要 sakai
        */
        private PopupMenuController m_clsPopupMenuController = new PopupMenuController();   //'ポップアップメニューコントローラ。
#endif
        private SproObject m_clsSproObject = new SproObject();       //'SuperPro オブジェクト。
#if false
        /*
            *************************** 修正要 sakai
        */
        private SproDataApp m_clsSproDataApp = new SproDataApp;     //'アプリケーション SproData。
        private SproDataCadastral m_clsSproDataCadastral = new SproDataCadastral;   //'地籍測量 SproData。
        private LicenseCheck m_clsLicenseCheck = new LicenseCheck;  //'2006/12/8 NGS Yamada
#endif

        //'2008/10/14 NGS Yamada
        //'ユーザデータを管理するフォルダのパスを追加
        //'パスの変更は、NS-Surveyの起動時のみに反省させるため、2つに別ける。
        //'各処理はこちらは参照。Documentは設定用
        //'form_loadで１度だけ同期を取る。
        public string UserDataPath;
        //==========================================================================================



        //==========================================================================================
        /*[VB]
        '*******************************************************************************
        'イベント

        '初期化｡
        Private Sub Form_Load()

            On Error GoTo ErrorHandler
    
            'スプリッターの初期化が終わるまでクールバーリサイズでスプリッターのリサイズは行わない｡
            m_bResize = True
    
            'ライセンス認証。
        '    If Not CheckSecurityApp(m_clsSproObject, m_clsSproDataApp) Then End
    
            If SPRO_ENABLE Then
                'ライセンスコードの認証を追加　2006/12/7 NGS Yamada
                If Not CheckSecurityApp(m_clsSproObject, m_clsSproDataApp) Then
                    End
                Else
                    If Not CheckLicenseApp(m_clsSproObject, m_clsLicenseCheck) Then
                        End
                    End If
                End If
            End If


            Set m_clsDocument = GetDocument()
            m_clsChangeSelRow.Value = True

            UserDataPath = m_clsDocument.UserDataPath '2008/10/14 NGS Yamada
    
            'タイトル。
            Call UpdateTitle
    
            'アイコン。
            Me.Icon = frmIcon.GetIcon(GetSystemMetrics(SM_CXSMICON), GetSystemMetrics(SM_CYSMICON))
    
            '左右スプリッターの初期化。
            Set m_clsSplitHori = New SplitController
            Set m_clsSplitHori.PaneFirst = objPlotPane
            Set m_clsSplitHori.PaneSecond = objListPane
            Set m_clsSplitHori.Splitter = imgSplitterHori
            Set m_clsSplitHori.Tracker = picTracker
            Let m_clsSplitHori.SplitHorizon = True
            Let m_clsSplitHori.SplitterSize = SPLITTER_SIZE* Screen.TwipsPerPixelX
            Let m_clsSplitHori.TrackLimitFirst = 500
            Let m_clsSplitHori.TrackLimitSecond = 500
    
            '上下スプリッターの初期化。
            Set m_clsSplitVert = New SplitController
            Set m_clsSplitVert.PaneFirst = m_clsSplitHori
            Set m_clsSplitVert.PaneSecond = objOutput
            Set m_clsSplitVert.Splitter = imgSplitterVert
            Set m_clsSplitVert.Tracker = picTracker
            Let m_clsSplitVert.SplitHorizon = False
            Let m_clsSplitVert.SplitterSize = SPLITTER_SIZE * Screen.TwipsPerPixelY
            Let m_clsSplitVert.TrackLimitFirst = 500
            Let m_clsSplitVert.TrackLimitSecond = 500
    
            'ウィンドウサイズを戻す。
            Dim clsRegNsNetwork As New RegKey
            Set clsRegNsNetwork = OpenRegistry()
            Call LoadWindowPos(clsRegNsNetwork, REG_KEY_WIN_MAIN, Me)
    
            'ペイン表示状態を戻す。
            m_clsSplitHori.VisiblePaneFirst = clsRegNsNetwork.QueryValue(REG_KEY_PANEPLOT, 1) <> 0
            m_clsSplitVert.VisiblePaneSecond = False
            mnuViewPlot.Checked = m_clsSplitHori.VisiblePaneFirst
            mnuViewOutput.Checked = m_clsSplitVert.VisiblePaneSecond
            'スプリッターの位置を戻す。
            imgSplitterHori.Left = clsRegNsNetwork.QueryValue(REG_KEY_SPLITHORI, imgSplitterHori.Left / Screen.TwipsPerPixelX) * Screen.TwipsPerPixelX
            imgSplitterVert.Top = clsRegNsNetwork.QueryValue(REG_KEY_SPLITVERT, imgSplitterVert.Left / Screen.TwipsPerPixelY) * Screen.TwipsPerPixelY
    
            'リストの作成。
            Call objListPane.RemakeList(False)
    
            'プロット初期化。
            Call objPlotPane.Initialize(PLOTTYPE_STANDARD)
            Call objPlotPane.UpdateLogicalDrawArea(True)
    
            'ステータスバー初期化。
            Call UpdateStatusBarAll
    
            'ツールバー初期化。
            Set tbToolBar(0).ImageList = imlToolbarIcons
            Set tbToolBar(1).ImageList = imlToolbarIcons
            Set tbToolBar(2).ImageList = imlToolbarIcons
            tbToolBar(0).Buttons.Item("FileNew").Image = "FileNew"
            tbToolBar(0).Buttons.Item("FileOpen").Image = "FileOpen"
            tbToolBar(0).Buttons.Item("FileSave").Image = "FileSave"
            tbToolBar(0).Buttons.Item("FileEdit").Image = "FileEdit"
            tbToolBar(0).Buttons.Item("FileImport").Image = "FileImport"
            tbToolBar(0).Buttons.Item("FileExport").Image = "FileExport"
            tbToolBar(1).Buttons.Item("EditUndo").Image = "EditUndo"
            tbToolBar(1).Buttons.Item("EditValid").Image = "EditValid"
            tbToolBar(1).Buttons.Item("EditEccentric").Image = "EditEccentric"
            tbToolBar(1).Buttons.Item("EditCombination").Image = "EditCombination"
            tbToolBar(1).Buttons.Item("EditSeparation").Image = "EditSeparation"
            tbToolBar(1).Buttons.Item("EditAttributeCommon").Image = "EditAttributeCommon"
            tbToolBar(1).Buttons.Item("EditAttribute").Image = "EditAttribute"
            tbToolBar(1).Buttons.Item("EditBaseLine").Image = "EditBaseLine"
            tbToolBar(1).Buttons.Item("EditSession").Image = "EditSession"
            tbToolBar(1).Buttons.Item("EditReverse").Image = "EditReverse"
            tbToolBar(1).Buttons.Item("EditRemove").Image = "EditRemove"
            tbToolBar(1).Buttons.Item("EditAutoOrder").Image = "EditAutoOrder"
            tbToolBar(2).Buttons.Item("ViewNoScale").Image = "ViewNoScale"
            tbToolBar(2).Buttons.Item("ViewEnlarge").Image = "ViewEnlarge"
            tbToolBar(2).Buttons.Item("ViewReduce").Image = "ViewReduce"
            tbToolBar(2).Buttons.Item("ViewMultiple").Image = "ViewMultiple"
            tbToolBar(2).Buttons.Item("ViewRange").Image = "ViewRange"
            tbToolBar(2).Buttons.Item("ViewFit").Image = "ViewFit"
            tbToolBar(2).Buttons.Item("ViewSlide").Image = "ViewSlide"
            tbToolBar(1).Buttons.Item("ViewData").Image = "ViewData"
            tbToolBar(1).Buttons.Item("AnalysisBaseLineAll").Image = "AnalysisBaseLine"
            tbToolBar(1).Buttons.Item("Check").Image = "Check"
            tbToolBar(1).Buttons.Item("Account").Image = "Account"
            tbToolBar(1).Buttons.Item("Generate").Image = "Generate"
            Call mnuViewNoScale_Click
    
            'コントロールバー。
            mnuViewToolbar.Checked = clsRegNsNetwork.QueryValue(REG_KEY_TOOLVISIBLE, 1) <> 0
            mnuViewStatusBar.Checked = clsRegNsNetwork.QueryValue(REG_KEY_STATUSVISIBLE, 1) <> 0
            cbCoolBar.Visible = mnuViewToolbar.Checked
            sbStatusBar.Visible = mnuViewStatusBar.Checked
    
            'メニューの初期化。
            mnuViewNoScale.Enabled = mnuViewPlot.Checked
            mnuViewEnlarge.Enabled = mnuViewPlot.Checked
            mnuViewReduce.Enabled = mnuViewPlot.Checked
            mnuViewMultiple.Enabled = mnuViewPlot.Checked
            mnuViewRange.Enabled = mnuViewPlot.Checked
            mnuViewFit.Enabled = mnuViewPlot.Checked
            mnuViewSlide.Enabled = mnuViewPlot.Checked
            tbToolBar(2).Buttons("ViewNoScale").Enabled = mnuViewNoScale.Enabled
            tbToolBar(2).Buttons("ViewEnlarge").Enabled = mnuViewEnlarge.Enabled
            tbToolBar(2).Buttons("ViewReduce").Enabled = mnuViewReduce.Enabled
            tbToolBar(2).Buttons("ViewMultiple").Enabled = mnuViewMultiple.Enabled
            tbToolBar(2).Buttons("ViewRange").Enabled = mnuViewRange.Enabled
            tbToolBar(2).Buttons("ViewFit").Enabled = mnuViewFit.Enabled
            tbToolBar(2).Buttons("ViewSlide").Enabled = mnuViewSlide.Enabled
                mnuAccountCadastral.Visible = m_clsSproObject.Check(m_clsSproDataCadastral)
    
            'クールバー。
            cbCoolBar.Bands(1).MinWidth = tbToolBar(0).Buttons(tbToolBar(0).Buttons.Count).Left + tbToolBar(0).Buttons(tbToolBar(0).Buttons.Count).Width
                cbCoolBar.Bands(2).MinWidth = tbToolBar(1).Buttons(tbToolBar(1).Buttons.Count).Left + tbToolBar(1).Buttons(tbToolBar(1).Buttons.Count).Width
                cbCoolBar.Bands(3).MinWidth = tbToolBar(2).Buttons(tbToolBar(2).Buttons.Count).Left + tbToolBar(2).Buttons(tbToolBar(2).Buttons.Count).Width
            cbCoolBar.Bands(4).MinWidth = cmbZone.Left + cmbZone.Width
            cbCoolBar.Bands(1).Width = cbCoolBar.Bands(1).MinWidth
            cbCoolBar.Bands(2).Width = cbCoolBar.Bands(2).MinWidth
            cbCoolBar.Bands(3).Width = cbCoolBar.Bands(3).MinWidth
            cbCoolBar.Bands(4).Width = cbCoolBar.Bands(4).MinWidth
    
            'ドキュメントのOpen/Closeによるメニューの更新。
            Call UpdateDocumentMenu
    
            'セキュリティチェックタイマー。
            timSecurity.Interval = SRCURITYCHECK_TIMER_INTERVAL
            timSecurity.Enabled = True

            m_bResize = False
    
            '2015/09/02 ファイルのタイムスタンプチェック。
            Dim sFile(19) As String
            Dim tDate(19) As Date
            sFile(0) = "DAT Division.exe"
            tDate(0) = DateSerial(2014, 6, 14)
            sFile(1) = "DeleteLicense.exe"
            tDate(1) = DateSerial(2006, 12, 8)
            sFile(2) = "GpsConv.dll"
            tDate(2) = DateSerial(2008, 3, 24)
            sFile(3) = "gpss_pps.dll"
            tDate(3) = DateSerial(2008, 2, 18)
            sFile(4) = "libpps.dll"
            tDate(4) = DateSerial(2013, 10, 8)
            sFile(5) = "ngs2rin.dll"
            tDate(5) = DateSerial(2006, 10, 30)
            sFile(6) = "ngs3rin.dll"
            tDate(6) = DateSerial(2014, 7, 31)
            sFile(7) = "ngs4rin.dll"
            tDate(7) = DateSerial(2014, 10, 14)
            sFile(8) = "ngs5rin.dll"
            tDate(8) = DateSerial(2015, 4, 2)
            sFile(9) = "ngs_check_dat.dll"
            tDate(9) = DateSerial(2014, 4, 22)
            sFile(10) = "ngs_rdrin.dll"
            tDate(10) = DateSerial(2015, 3, 3)
            sFile(11) = "NS-Survey.exe"
            tDate(11) = DateSerial(2015, 8, 4)
            sFile(12) = "NSCAB.dll"
            tDate(12) = DateSerial(2006, 3, 23)
            sFile(13) = "NSLtool.dll"
            tDate(13) = DateSerial(2007, 9, 3)
            sFile(14) = "NSPPS.dll"
            tDate(14) = DateSerial(2006, 1, 13)
            sFile(15) = "NSPPS2.dll"
            tDate(15) = DateSerial(2015, 8, 4)
            sFile(16) = "NSSP3CtoA.dll"
            tDate(16) = DateSerial(2006, 1, 13)
            sFile(17) = "pps.dll"
            tDate(17) = DateSerial(2015, 7, 14)
            sFile(18) = "SemiDynaNGS.dll"
            tDate(18) = DateSerial(2009, 11, 25)
            If Not CheckModuleTimeStamp(sFile, tDate, 19) Then
                Call frmCheckModule.Show(0, Me)
            End If
    
            '2022/07/21 Hitz H.Nakamura *******************************************************************
            'ファイルのドロップを追加。
            Call objPlotPane.SetDropMode(True)
            '**********************************************************************************************
    
            Exit Sub


        ErrorHandler:
            Call mdlMain.ErrorExit


        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'*******************************************************************************
        //'イベント

        //'初期化｡
        public void Form_Load()
        {

            try
            {
                //'スプリッターの初期化が終わるまでクールバーリサイズでスプリッターのリサイズは行わない｡
                m_bResize = true;


                //'ライセンス認証。
                //'if Not CheckSecurityApp(m_clsSproObject, m_clsSproDataApp)) { End


                //if (MdlNSSDefine.SPRO_ENABLE)
                if (true)
                {
                    //'ライセンスコードの認証を追加　2006/12/7 NGS Yamada

#if false
                    /*
                     *************************** 修正要 sakai
                    */
                    if (!MdlNSUtility.CheckSecurityApp(m_clsSproObject, m_clsSproDataApp))
                    {
                        //End
                        return;
                    }
                    else
                    {
                        if (!MdlNSUtility.CheckLicenseApp(m_clsSproObject, m_clsLicenseCheck))
                        {
                            //End
                            return;
                        }
                    }
#endif
                }

                m_clsMdlMain = new MdlMain();
                m_clsDocument = m_clsMdlMain.GetDocument();
                m_clsChangeSelRow.Value = true;

                UserDataPath = m_clsDocument.UserDataPath; //'2008/10/14 NGS Yamada


                m_clsMdlGUI = new MdlGUI();





                //'タイトル。
                UpdateTitle();


                //'アイコン。
                //Me.Icon = frmIcon.GetIcon(GetSystemMetrics(SM_CXSMICON), GetSystemMetrics(SM_CYSMICON))


                //'左右スプリッターの初期化。
#if false
                /*
                 *************************** 修正要 sakai
                */
                SplitController m_clsSplitHori = new SplitController();
                m_clsSplitHori.PaneFirst = objPlotPane;
                m_clsSplitHori.PaneSecond = objListPane;
                m_clsSplitHori.Splitter = imgSplitterHori;
                m_clsSplitHori.Tracker = picTracker;
                m_clsSplitHori.SplitHorizon = true;
                m_clsSplitHori.SplitterSize = SPLITTER_SIZE * Screen.TwipsPerPixelX;
                m_clsSplitHori.TrackLimitFirst = 500;
                m_clsSplitHori.TrackLimitSecond = 500;
#endif

                //'上下スプリッターの初期化。
#if false
                /*
                 *************************** 修正要 sakai
                */
                SplitController m_clsSplitVert = new SplitController();
                m_clsSplitVert.PaneFirst = m_clsSplitHori;
                m_clsSplitVert.PaneSecond = objOutput;
                m_clsSplitVert.Splitter = imgSplitterVert;
                m_clsSplitVert.Tracker = picTracker;
                m_clsSplitVert.SplitHorizon = false;
                m_clsSplitVert.SplitterSize = SPLITTER_SIZE * Screen.TwipsPerPixelY;
                m_clsSplitVert.TrackLimitFirst = 500;
                m_clsSplitVert.TrackLimitSecond = 500;
#endif


                //'ウィンドウサイズを戻す。
#if false
                /*
                 *************************** 修正要 sakai
                */
                RegKey clsRegNsNetwork As New RegKey;
                clsRegNsNetwork = OpenRegistry();
                LoadWindowPos(clsRegNsNetwork, REG_KEY_WIN_MAIN, Me);
#endif


                //'ペイン表示状態を戻す。
#if false
                /*
                 *************************** 修正要 sakai
                */
                m_clsSplitHori.VisiblePaneFirst = clsRegNsNetwork.QueryValue(REG_KEY_PANEPLOT, 1) != 0;
                m_clsSplitVert.VisiblePaneSecond = false;
                mnuViewPlot.Checked = m_clsSplitHori.VisiblePaneFirst;
                mnuViewOutput.Checked = m_clsSplitVert.VisiblePaneSecond;
                //'スプリッターの位置を戻す。
                imgSplitterHori.Left = clsRegNsNetwork.QueryValue(REG_KEY_SPLITHORI, imgSplitterHori.Left / Screen.TwipsPerPixelX) * Screen.TwipsPerPixelX;
                imgSplitterVert.Top = clsRegNsNetwork.QueryValue(REG_KEY_SPLITVERT, imgSplitterVert.Left / Screen.TwipsPerPixelY) * Screen.TwipsPerPixelY;
#endif


                //'リストの作成。
                objListPane.UserControl_Initialize(m_clsMdlMain);
                objListPane.RemakeList(false);


                //'プロット初期化。
                objPlotPane.Initialize(PLOTTYPE_ENUM.PLOTTYPE_STANDARD, m_clsMdlMain);
                objPlotPane.UpdateLogicalDrawArea(true);


                //'ステータスバー初期化。
#if false
                /*
                 *************************** 修正要 sakai
                */
                UpdateStatusBarAll()
#endif


                //'ツールバー初期化。
#if false
                /*
                 *************************** 修正要 sakai
                */
                tbToolBar(0).ImageList = imlToolbarIcons;
                tbToolBar(1).ImageList = imlToolbarIcons;
                tbToolBar(2).ImageList = imlToolbarIcons;
                tbToolBar(0).Buttons.Item("FileNew").Image = "FileNew";
                tbToolBar(0).Buttons.Item("FileOpen").Image = "FileOpen";
                tbToolBar(0).Buttons.Item("FileSave").Image = "FileSave";
                tbToolBar(0).Buttons.Item("FileEdit").Image = "FileEdit";
                tbToolBar(0).Buttons.Item("FileImport").Image = "FileImport";
                tbToolBar(0).Buttons.Item("FileExport").Image = "FileExport";
                tbToolBar(1).Buttons.Item("EditUndo").Image = "EditUndo";
                tbToolBar(1).Buttons.Item("EditValid").Image = "EditValid";
                tbToolBar(1).Buttons.Item("EditEccentric").Image = "EditEccentric";
                tbToolBar(1).Buttons.Item("EditCombination").Image = "EditCombination";
                tbToolBar(1).Buttons.Item("EditSeparation").Image = "EditSeparation";
                tbToolBar(1).Buttons.Item("EditAttributeCommon").Image = "EditAttributeCommon";
                tbToolBar(1).Buttons.Item("EditAttribute").Image = "EditAttribute";
                tbToolBar(1).Buttons.Item("EditBaseLine").Image = "EditBaseLine";
                tbToolBar(1).Buttons.Item("EditSession").Image = "EditSession";
                tbToolBar(1).Buttons.Item("EditReverse").Image = "EditReverse";
                tbToolBar(1).Buttons.Item("EditRemove").Image = "EditRemove";
                tbToolBar(1).Buttons.Item("EditAutoOrder").Image = "EditAutoOrder";
                tbToolBar(2).Buttons.Item("ViewNoScale").Image = "ViewNoScale";
                tbToolBar(2).Buttons.Item("ViewEnlarge").Image = "ViewEnlarge";
                tbToolBar(2).Buttons.Item("ViewReduce").Image = "ViewReduce";
                tbToolBar(2).Buttons.Item("ViewMultiple").Image = "ViewMultiple";
                tbToolBar(2).Buttons.Item("ViewRange").Image = "ViewRange";
                tbToolBar(2).Buttons.Item("ViewFit").Image = "ViewFit";
                tbToolBar(2).Buttons.Item("ViewSlide").Image = "ViewSlide";
                tbToolBar(1).Buttons.Item("ViewData").Image = "ViewData";
                tbToolBar(1).Buttons.Item("AnalysisBaseLineAll").Image = "AnalysisBaseLine";
                tbToolBar(1).Buttons.Item("Check").Image = "Check";
                tbToolBar(1).Buttons.Item("Account").Image = "Account";
                tbToolBar(1).Buttons.Item("Generate").Image = "Generate";
                mnuViewNoScale_Click();
#endif


                //'コントロールバー。
#if false
                /*
                 *************************** 修正要 sakai
                */
                mnuViewToolbar.Checked = clsRegNsNetwork.QueryValue(REG_KEY_TOOLVISIBLE, 1) != 0;
                mnuViewStatusBar.Checked = clsRegNsNetwork.QueryValue(REG_KEY_STATUSVISIBLE, 1) != 0;
                cbCoolBar.Visible = mnuViewToolbar.Checked;
                sbStatusBar.Visible = mnuViewStatusBar.Checked;
#endif


                //'メニューの初期化。
#if false
                /*
                 *************************** 修正要 sakai
                */
                mnuViewNoScale.Enabled = mnuViewPlot.Checked;
                mnuViewEnlarge.Enabled = mnuViewPlot.Checked;
                mnuViewReduce.Enabled = mnuViewPlot.Checked;
                mnuViewMultiple.Enabled = mnuViewPlot.Checked;
                mnuViewRange.Enabled = mnuViewPlot.Checked;
                mnuViewFit.Enabled = mnuViewPlot.Checked;
                mnuViewSlide.Enabled = mnuViewPlot.Checked;
                tbToolBar(2).Buttons("ViewNoScale").Enabled = mnuViewNoScale.Enabled;
                tbToolBar(2).Buttons("ViewEnlarge").Enabled = mnuViewEnlarge.Enabled;
                tbToolBar(2).Buttons("ViewReduce").Enabled = mnuViewReduce.Enabled;
                tbToolBar(2).Buttons("ViewMultiple").Enabled = mnuViewMultiple.Enabled;
                tbToolBar(2).Buttons("ViewRange").Enabled = mnuViewRange.Enabled;
                tbToolBar(2).Buttons("ViewFit").Enabled = mnuViewFit.Enabled;
                tbToolBar(2).Buttons("ViewSlide").Enabled = mnuViewSlide.Enabled;
                mnuAccountCadastral.Visible = m_clsSproObject.Check(m_clsSproDataCadastral);
#endif


                //'クールバー。
#if false
                /*
                 *************************** 修正要 sakai
                */
                cbCoolBar.Bands(1).MinWidth = tbToolBar(0).Buttons(tbToolBar(0).Buttons.Count).Left + tbToolBar(0).Buttons(tbToolBar(0).Buttons.Count).Width;
                cbCoolBar.Bands(2).MinWidth = tbToolBar(1).Buttons(tbToolBar(1).Buttons.Count).Left + tbToolBar(1).Buttons(tbToolBar(1).Buttons.Count).Width;
                cbCoolBar.Bands(3).MinWidth = tbToolBar(2).Buttons(tbToolBar(2).Buttons.Count).Left + tbToolBar(2).Buttons(tbToolBar(2).Buttons.Count).Width;
                cbCoolBar.Bands(4).MinWidth = cmbZone.Left + cmbZone.Width;
                cbCoolBar.Bands(1).Width = cbCoolBar.Bands(1).MinWidth;
                cbCoolBar.Bands(2).Width = cbCoolBar.Bands(2).MinWidth;
                cbCoolBar.Bands(3).Width = cbCoolBar.Bands(3).MinWidth;
                cbCoolBar.Bands(4).Width = cbCoolBar.Bands(4).MinWidth;
#endif


                //'ドキュメントのOpen/Closeによるメニューの更新。
#if false
                /*
                 *************************** 修正要 sakai
                */
                Call UpdateDocumentMenu
#endif


                //'セキュリティチェックタイマー。
#if false
                /*
                 *************************** 修正要 sakai
                */
                timSecurity.Interval = SRCURITYCHECK_TIMER_INTERVAL;
                timSecurity.Enabled = true;

                m_bResize = false;
#endif


#if false
                /*
                 *************************** 修正要 sakai
                */
                //'2015/09/02 ファイルのタイムスタンプチェック。
                string[] sFile = new string[19];
                Date[] tDate = new Date[19];
                sFile(0) = "DAT Division.exe";
                tDate(0) = DateSerial(2014, 6, 14);
                sFile(1) = "DeleteLicense.exe";
                tDate(1) = DateSerial(2006, 12, 8);
                sFile(2) = "GpsConv.dll";
                tDate(2) = DateSerial(2008, 3, 24);
                sFile(3) = "gpss_pps.dll";
                tDate(3) = DateSerial(2008, 2, 18);
                sFile(4) = "libpps.dll";
                tDate(4) = DateSerial(2013, 10, 8);
                sFile(5) = "ngs2rin.dll";
                tDate(5) = DateSerial(2006, 10, 30);
                sFile(6) = "ngs3rin.dll";
                tDate(6) = DateSerial(2014, 7, 31);
                sFile(7) = "ngs4rin.dll";
                tDate(7) = DateSerial(2014, 10, 14);
                sFile(8) = "ngs5rin.dll";
                tDate(8) = DateSerial(2015, 4, 2);
                sFile(9) = "ngs_check_dat.dll";
                tDate(9) = DateSerial(2014, 4, 22);
                sFile(10) = "ngs_rdrin.dll";
                tDate(10) = DateSerial(2015, 3, 3);
                sFile(11) = "NS-Survey.exe";
                tDate(11) = DateSerial(2015, 8, 4);
                sFile(12) = "NSCAB.dll";
                tDate(12) = DateSerial(2006, 3, 23);
                sFile(13) = "NSLtool.dll";
                tDate(13) = DateSerial(2007, 9, 3);
                sFile(14) = "NSPPS.dll";
                tDate(14) = DateSerial(2006, 1, 13);
                sFile(15) = "NSPPS2.dll";
                tDate(15) = DateSerial(2015, 8, 4);
                sFile(16) = "NSSP3CtoA.dll";
                tDate(16) = DateSerial(2006, 1, 13);
                sFile(17) = "pps.dll";
                tDate(17) = DateSerial(2015, 7, 14);
                sFile(18) = "SemiDynaNGS.dll";
                tDate(18) = DateSerial(2009, 11, 25);
                if (!CheckModuleTimeStamp(sFile, tDate, 19))
                {
                    frmCheckModule.Show(0, Me);
                }
#endif


                //'2022/07/21 Hitz H.Nakamura *******************************************************************
                //'ファイルのドロップを追加。
                objPlotPane.SetDropMode(true);
                //'**********************************************************************************************
            }


            catch
            {
                MdlMain mdlMain = new MdlMain();
                mdlMain.ErrorExit();
            }

        }
        //==========================================================================================








        //*******************************************************************************
        //*******************************************************************************
        /// <summary>
        /// メインメニュー：新規現場の作成(&N)...
        ///  '引き数：
        ///  'system object：クラス階級のすべてのクラスをサポートし、派生クラスに下位レベルのサービスを提供。
        ///  'EventArgs :イベントデータを格納するクラスの基底クラスを表し、イベントデータを含まないイベントに使用する値を提供。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <returns>
        /// </returns>
        //*******************************************************************************
        private void mnuFileNew_Click(object sender, EventArgs e)
        {
#if false
            frmMain fmain = new frmMain();

            //frmMain fMain = new frmMain();
            // textBox1.Text = $"mnuFileNew_Click";
            //label1.Text = textBox1.Text;
            //label1.Refresh();


            frmJobEdit fJobEdit = new frmJobEdit();
            fJobEdit.Text = "新規現場の作成";


            fJobEdit.ShowDialog();
#else

            frmMain fmain = new frmMain();

            //23/12/29 K.setoguchi@NV---------->>>>>>>>>>


            //[VB]  On Error GoTo ErrorHandler

            //[VB]  '既存のプロジェクトを閉じるか確認する。
            //[VB]  If Not ConfirmCloseProject() Then Exit Sub


            //[VB]  '現場の新規作成。
            //[VB]  If Not CreateJob Then Exit Sub
            if (!CreateJob()) { return; }
            //------------------------------------------------------------


            //[VB]  'タイトル。
            //[VB]  Call UpdateTitle


            //[VB]  '砂時計。
            //[VB]  Dim clsWaitCursor As New WaitCursor
            //[VB]  Set clsWaitCursor.Object = Me


            //[VB]  'リストの作成。
            //[VB]  Call objListPane.SelectElement(Nothing)
            //[VB]  Call objListPane.RemakeList(False)
            //[VB]  
            //[VB]  
            //[VB]  'プロットの再描画。
            //[VB]  Call objPlotPane.UpdateLogicalDrawArea(True)
            //[VB]  Call objPlotPane.Redraw
            //[VB]  Call objPlotPane.Refresh


            //[VB]  '座標系番号を更新する。
            //[VB]  Call SetListIndexManual(cmbZone, m_clsDocument.CoordNum -1)

            //[VB]  'ステータスバーの更新。
            //[VB]  Call UpdateStatusBarAll


            //[VB]  'ドキュメントのOpen/Closeによるメニューの更新。
            //[VB]  Call UpdateDocumentMenu
            //[VB]  
            //[VB]  
            //[VB]  Call clsWaitCursor.Back
            //[VB]  
            //[VB]  Exit Sub
            //[VB]  
            //[VB]  ErrorHandler:
            //[VB]  Call mdlMain.ErrorExit
            //[VB]  
            //[VB]  End Sub


#endif

        }
        /// <summary>
        /// '現場の新規作成。
        /// '
        /// 
        /// </summary>
        /// <returns>
        /// '戻り値：
        ///     '正常終了の場合 True を返す。
        ///     'キャンセルの場合 False を返す。
        /// </returns>
        private bool CreateJob()
        {
            bool CreateJob = false;


            m_clsDocument.Class_Initialize();



            //------------------------------------------------------------
            //[VB]      '既存の値。
            //[VB]      frmJobEdit.JobName = ""
            //[VB]      frmJobEdit.DistrictName = ""
            //[VB]      frmJobEdit.Folder = ""
            frmJobEdit fJobEdit = new frmJobEdit();


            fJobEdit.txtJobName.Text = "";
            fJobEdit.txtDistrictName.Text = "";
            fJobEdit.lblFolder.Text = "";


            //------------------------------------------------------------------------------------
            //[VB]      frmJobEdit.CoordNum = GetPrivateProfileInt(PROFILE_SAVE_SEC_ACCOUNT, PROFILE_SAVE_KEY_COORDNUM, 9, App.Path & "\" & App.Title & PROFILE_SAVE_EXT)
            //              PROFILE_SAVE_SEC_ACCOUNT =  "ACCOUNT"
            //              PROFILE_SAVE_KEY_COORDNUM = "COORDNUM"
            //              App.Path = "C:\Develop\NetSurv\Src\NS-App\NS-Survey"
            //              App.Title =  "NS-Survey"
            //              PROFILE_SAVE_EXT =   ".ini"
            string App_Path = @"C:\Develop\NetSurv\Src\NS-App\NS-Survey";
            string App_Title = "NS-Survey";
            fJobEdit.CoordNum = iniFileControl.GetPrivateProfileInt(MdlNSDefine.PROFILE_SAVE_SEC_ACCOUNT, MdlNSDefine.PROFILE_SAVE_KEY_COORDNUM, 9, $"{App_Path}\\{App_Title}{MdlNSSDefine.PROFILE_SAVE_EXT}");
            fJobEdit.CoordNum -= 1;
            fJobEdit.cmbZone.SelectedIndex = (int)fJobEdit.CoordNum;


            //------------------------------------------------------------------------------------
            //[VB]      frmJobEdit.GeoidoEnable = Not m_clsDocument.GeoidoPathDef = ""
            //[VB]      frmJobEdit.GeoidoPath = m_clsDocument.GeoidoPathDef
            fJobEdit.chkGeoidoEnable.Checked = true;                                       //true : "C:\Develop\パラメータファイル\gsigeo2011_ver1.asc"
            fJobEdit.txtGeoidoPath.Text = @"C:\Develop\パラメータファイル\gsigeo2011_ver1.asc"; //"C:\Develop\パラメータファイル\gsigeo2011_ver1.asc"

            if (m_clsDocument.GeoidoPathDef != "")
            {
                fJobEdit.chkGeoidoEnable.Checked = true;                                       //true : "C:\Develop\パラメータファイル\gsigeo2011_ver1.asc"
            }
            else
            {
                fJobEdit.chkGeoidoEnable.Checked = false;                                       //true : "C:\Develop\パラメータファイル\gsigeo2011_ver1.asc"

            }
            fJobEdit.txtGeoidoPath.Text = m_clsDocument.GeoidoPathDef;


            //------------------------------------------------------------------------------------
            //'セミ・ダイナミック対応。'2009/11 H.Nakamura
            //[VB]      frmJobEdit.SemiDynaEnable = False
            //[VB]      frmJobEdit.SemiDynaPath = m_clsDocument.SemiDynaPathDef
            //[VB]      frmJobEdit.SemiDynaValid = True
            fJobEdit.chkSemiDynaEnable.Checked = false;
            fJobEdit.SemiDynaPath = m_clsDocument.SemiDynaPathDef;          //"C:\Develop\パラメータファイル\SemiDyna2009.par"
            fJobEdit.txtSemiDynaPath.Text = m_clsDocument.SemiDynaPathDef;  //@"C:\Develop\パラメータファイル\SemiDyna2009.par";
            fJobEdit.SemiDynaValid = true;

            //------------------------------------------------------------------------------------
            //[VB]      //'Caption を設定すると Form_Load が走るので注意すべし！
            //[VB]      frmJobEdit.Caption = "新規現場の作成"
            fJobEdit.Text = "新規現場の作成";


            //--------------------------------------------
            //    Call frmJobEdit.Show(1)
            fJobEdit.ShowDialog();

            //--------------------------------------------
            //    If frmJobEdit.Result<> vbOK Then Exit Function
            if (fJobEdit.Result != DEFINE.vbOK) { return false; }


            //--------------------------------------------
            //    '再描画。
            //    If RedrawWindow(Me.hWnd, 0, 0, RDW_UPDATENOW) = 0 Then Call Err.Raise(ERR_FATAL, , GetLastErrorMessage())
            //
            //          //＜＜＜　坂井様お願い致します。　＞＞＞           


            //--------------------------------------------
            //[VB]    'プロジェクトフォルダの生成。
            //[VB]    On Error GoTo FileErrorHandler
            ProjectFileManager clsProjectFileManager = new ProjectFileManager();
            string sProjectFolderName = clsProjectFileManager.CreateProjectFolder();        //"0004"
            if (sProjectFolderName == "")
            {
                MessageBox.Show($"これ以上現場を保存することができません。\n 不要な現場を削除してください。", "エラー発生");
                return CreateJob;

            }

            //  C:\Develop\NetSurv\Src\NS-App\NS-Survey\UserData\0277\ObsPoint

            //--------------------------------------------
            //    On Error GoTo 0
            //   
            //    '既存の現場を閉じる。
            CloseProject();

            //--------------------------------------------
            //    '新規設定。
            m_clsDocument.SetJob(fJobEdit.JobName, fJobEdit.DistrictName, fJobEdit.CoordNum, fJobEdit.GeoidoEnable, fJobEdit.GeoidoPath, fJobEdit.SemiDynaEnable, fJobEdit.SemiDynaPath);



            //--------------------------------------------
            //    '2009/11 H.Nakamura
            //    'セミ・ダイナミックパラメータファイルのパスはデフォルトにも反映させる。
            App_Path = @"C:\Develop\NetSurv\Src\NS-App\NS-Survey";
            App_Title = "NS-Survey";
            if (m_clsDocument.m_bSemiDynaEnable)
            {
                m_clsDocument.SemiDynaPathDef = m_clsDocument.m_sSemiDynaPath;

                //--------------------------------------------
                //
                // "C:\Develop\NetSurv\Src\NS-App\NS-Survey" "\" "NS-Survey" .ini
                //      App_Path = C:\Hitz\NetSurv\Prog\Src
                //      App_Title =  "NS-Survey"  
                //
                if (!iniFileControl.WritePrivateProfileString(MdlNSDefine.PROFILE_SAVE_SEC_SEMIDYNA, MdlNSDefine.PROFILE_SAVE_KEY_PATH, m_clsDocument.SemiDynaPathDef, $"{App_Path}\\{App_Title}{MdlNSSDefine.PROFILE_SAVE_EXT}"))
                {
                    //Call Err.Raise(ERR_FATAL, , GetLastErrorMessage())
                }
            }


            //--------------------------------------------
            //    '2009/11 H.Nakamura
            //    'セミ・ダイナミックの初期化。
            MdlSemiDyna mdlSemiDyna = new MdlSemiDyna();
            mdlSemiDyna.Initialize(m_clsDocument.m_bSemiDynaEnable, m_clsDocument.m_sSemiDynaPath);



            //--------------------------------------------
            //    '保存。
            //--------  C:\Develop\NetSurv\Src\NS-App\NS-Survey\UserData\0277\data
            Save(clsProjectFileManager.GetSaveFilePath(sProjectFolderName));



            //--------------------------------------------
            //    'iniファイルに保存。
            //                      App_Path = C:\Hitz\NetSurv\Prog\Src
            //                      App_Title =  "NS-Survey"  
            string sValue;
            sValue = fJobEdit.CoordNum.ToString();
            if (iniFileControl.WritePrivateProfileString(MdlNSDefine.PROFILE_SAVE_SEC_ACCOUNT, MdlNSDefine.PROFILE_SAVE_KEY_COORDNUM, sValue, $"{App_Path}\\{App_Title}{MdlNSSDefine.PROFILE_SAVE_EXT}") == false)
            {
                //  Call Err.Raise(ERR_FATAL, , GetLastErrorMessage())
                MessageBox.Show("iniファイルに保存", "エラーが発生");
            }


            //<<<<<<<<<-----------24/01/04 K.setoguchi@NV


            return CreateJob;
        }
        //==========================================================================================
        /*[VB]
            'インプリメンテーション
        
            'プロジェクトを保存する。
            '
            '引き数：
            'sPath 保存先ファイルのパス。
            Private Sub Save(ByVal sPath As String)
        
                '砂時計。
                Dim clsWaitCursor As New WaitCursor
                Set clsWaitCursor.Object = Me
        
                On Error GoTo FileErrorHandler
                Call m_clsDocument.Save(sPath)
                On Error GoTo 0
        
        
                Call clsWaitCursor.Back
        
        
                Exit Sub
        
        
            FileErrorHandler:
                Call MsgBox(Err.Description, vbCritical)
        
            End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /// <summary>
        ///インプリメンテーション
        ///
        /// プロジェクトを保存する。
        ///
        /// 引き数：
        ///     sPath 保存先ファイルのパス。
        /// </summary>
        /// <param name="sPath"></param>
        private void Save(string sPath)
        {

            //------------------------------
            //[VB]    '砂時計。
            //[VB]    Dim clsWaitCursor As New WaitCursor
            //[VB]    Set clsWaitCursor.Object = Me
            Cursor = Cursors.WaitCursor;

            //------------------------------
            //[VB]   On Error GoTo FileErrorHandler
            //[VB]   Call m_clsDocument.Save(sPath)
            //[VB]   On Error GoTo 0
            m_clsDocument.Save(sPath);

            //------------------------------
            //[VB]   Call clsWaitCursor.Back
            Cursor = Cursors.Default;

        }

        //*******************************************************************************
        /// <summary>
        /// メインメニュー：現場を選択(&O)...
        ///  '引き数：
        ///  'system object：クラス階級のすべてのクラスをサポートし、派生クラスに下位レベルのサービスを提供。
        ///  'EventArgs :イベントデータを格納するクラスの基底クラスを表し、イベントデータを含まないイベントに使用する値を提供。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <returns>
        /// </returns>
        //*******************************************************************************
        private void mnuFileOpen_Click(object sender, EventArgs e)
        {
            //DEBUG >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
            //   MessageBox.Show(mnuFileOpen.Text);
            //DEBUG <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<

            //[VB]  On Error GoTo ErrorHandler
            //[VB]  
            //[VB]  '既存のプロジェクトを閉じるか確認する。
            //[VB]  If Not ConfirmCloseProject() Then Exit Sub
            //[VB]  
            //[VB]  
            //[VB]  'プロジェクトを開く。
            //[VB]  If Not OpenProject() Then Exit Sub

            try
            {
                ListPane fListPane = new ListPane();

                if (OpenProject(fListPane) == false)
                {
                    return;
                }

                //fListPane.ShowDialog();

                /*[VB]
                'タイトル。
                Call UpdateTitle
                [VB]*/


                /*[VB]
                '砂時計。
                Dim clsWaitCursor As New WaitCursor
                Set clsWaitCursor.Object = Me
                [VB] */
                Cursor = Cursors.WaitCursor;


                /*[VB]
                'リストの作成。
                Call objListPane.SelectElement(Nothing)
                Call objListPane.RemakeList(False)
                [VB] */
                /*[C#]*/
#if true
                objListPane.SelectElement(null);
                objListPane.RemakeList(false);
#endif

                /*[VB]
                'プロットの再描画。
                Call objPlotPane.UpdateLogicalDrawArea(True)
                Call objPlotPane.Redraw
                Call objPlotPane.Refresh
                [VB] */
                /*[C#]*/
                //objPlotPane.PlotPane_Initialize();
                Bitmap btmp = objPlotPane.Dis_Get_btmp();
                objPlotPane.List_Genba_set(m_List_Genba_S);
                objPlotPane.UpdateLogicalDrawArea(true);
                objPlotPane.Redraw();
                objPlotPane.Refresh();


                /*[VB]
                'ステータスバーの更新。
                Call UpdateStatusBarAll
                [VB]*/


                /*[VB]
                'ドキュメントのOpen/Closeによるメニューの更新。
                Call UpdateDocumentMenu
                [VB]*/


                /*[VB]
                Call clsWaitCursor.Back
                [VB] */
                Cursor = Cursors.Default;

                /*[VB]
                Exit Sub
                [VB]*/
                /*[C#]*/
                return;
            }

            catch { return; }
        }

        //***************************************
        //'タイトルを更新する。
        //Private Sub UpdateTitle()
        //    If m_clsDocument.IsEmpty Then
        //        Caption = App.Title
        //    Else
        //        Caption = App.Title & " - " & m_clsDocument.JobName
        //    End If
        //End Sub
        private void UpdateTitle()
        {

        }
        //**************************************************************************************
        //**************************************************************************************
        //2==========================================================================================
        /*[VB]
            'プロジェクトを開く。
            '
            '戻り値：
            '正常終了の場合 True を返す。
            'キャンセルの場合 False を返す。
            Private Function OpenProject() As Boolean

                OpenProject = False
    
                Call frmJobOpen.Show(1)
                If frmJobOpen.Result <> vbOK Then Exit Function
    
                '再描画。
                If RedrawWindow(Me.hWnd, 0, 0, RDW_UPDATENOW) = 0 Then Call Err.Raise(ERR_FATAL, , GetLastErrorMessage())
    
                '保存ファイルのパス。
                Dim sPath As String
                Dim clsProjectFileManager As New ProjectFileManager
                sPath = clsProjectFileManager.GetSaveFilePath(frmJobOpen.FolderName)
    
                '閉じてから開く。
                Call CloseProject
                Call Load(sPath)
    
                'ジオイドモデルのパスの評価。
                If m_clsDocument.GeoidoEnable Then
                    'ファイルの存在を確認。
                    Dim clsFind As New FileFind
                    If clsFind.FindFile(m_clsDocument.GeoidoPath) Then
                        Select Case AltitudeSupport_EstimateFile(m_clsDocument.GeoidoPath)
                        Case 0
                        Case 1
                            Call MsgBox("ジオイドモデルで指定されたファイルが不正です。" & vbCrLf & "ジオイド補正をOFFにします。", vbCritical)
                            m_clsDocument.GeoidoEnable = False
                        Case Else
                            Call Err.Raise(ERR_FATAL, , MESSAGE_GEOIDO_ESTIMATE)
                        End Select
                    Else
                        If m_clsDocument.GeoidoPathDef <> "" Then
                            If MsgBox("ジオイドモデルで指定されたファイルが見つかりません。" & vbCrLf & "「デフォルトで使用するジオイドモデルファイル」に変更しますか？", vbQuestion Or vbYesNo) = vbYes Then
                                m_clsDocument.GeoidoPath = m_clsDocument.GeoidoPathDef
                            Else
                                m_clsDocument.GeoidoEnable = False
                            End If
                        Else
                            Call MsgBox("ジオイドモデルで指定されたファイルが見つかりません。" & vbCrLf & "ジオイド補正をOFFにします。", vbCritical)
                            m_clsDocument.GeoidoEnable = False
                        End If
                    End If
                End If
    
                '2009/11 H.Nakamura
                'セミ・ダイナミックパラメータファイルのパスの評価。
                If m_clsDocument.SemiDynaEnable Then
                    'ファイルの存在を確認。
                    If clsFind.FindFile(m_clsDocument.SemiDynaPath) Then
                        If Not mdlSemiDyna.EstimateFile(m_clsDocument.SemiDynaPath) Then
                            Call MsgBox("セミ・ダイナミック補正で指定されたファイルが不正です。" & vbCrLf & "セミ・ダイナミック補正をOFFにします。", vbCritical)
                            m_clsDocument.SemiDynaEnable = False
                        End If
                    Else
                        If m_clsDocument.SemiDynaPathDef <> "" Then
                            If MsgBox("セミ・ダイナミック補正で指定されたファイルが見つかりません。" & vbCrLf & "「デフォルトで使用するセミ・ダイナミック補正パラメータファイル」に変更しますか？", vbQuestion Or vbYesNo) = vbYes Then
                                If clsFind.FindFile(m_clsDocument.SemiDynaPathDef) Then
                                    If Not mdlSemiDyna.EstimateFile(m_clsDocument.SemiDynaPathDef) Then
                                        Call MsgBox("デフォルトのセミ・ダイナミック補正で指定されたファイルが不正です。" & vbCrLf & "セミ・ダイナミック補正をOFFにします。", vbCritical)
                                        m_clsDocument.SemiDynaEnable = False
                                    Else
                                        m_clsDocument.SemiDynaPath = m_clsDocument.SemiDynaPathDef
                                    End If
                                Else
                                    Call MsgBox("デフォルトのセミ・ダイナミック補正で指定されたファイルが見つかりません。" & vbCrLf & "セミ・ダイナミック補正をOFFにします。", vbCritical)
                                    m_clsDocument.SemiDynaEnable = False
                                End If
                            Else
                                m_clsDocument.SemiDynaEnable = False
                            End If
                        Else
                            Call MsgBox("セミ・ダイナミック補正で指定されたファイルが見つかりません。" & vbCrLf & "セミ・ダイナミック補正をOFFにします。", vbCritical)
                            m_clsDocument.SemiDynaEnable = False
                        End If
                    End If
                End If
    
                '2009/11 H.Nakamura
                'セミ・ダイナミックの初期化。
                Call mdlSemiDyna.Initialize(m_clsDocument.SemiDynaEnable, m_clsDocument.SemiDynaPath)
    
                '座標系番号を更新する。
                Call SetListIndexManual(cmbZone, m_clsDocument.CoordNum - 1)
    
                OpenProject = True
    
            End Function

            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#] 2
        /// <summary>
        //'プロジェクトを開く。   メニュー＞現場を選択
        /// 引き数：
        //  frmListPane：現場の詳細データ表示フォーム。
        /// </summary>
        /// <param name="frmListPane"></param>
        /// <returns>
        //'戻り値：
        //  '正常終了の場合 True を返す。
        //  'キャンセルの場合 False を返す。
        /// </returns>
        //**************************************************************************************
        private bool OpenProject(ListPane fListPane)
        {

#if true   //K.S 0304
            //[VB]---------------------------------------------------
            //[VB]      Call frmJobOpen.Show(1)
            //---------------------------------------------------
            frmJobOpen form = new frmJobOpen();
            form.fMain = this;          //サブフォームからの受信用信　this.ReceiveData
            //---------------------------------------------------
            form.ShowDialog();

            if (OpenProject_Check(this.ReceiveData, (int)form.lvProject.SelectedItems[0].Index) == false) { return false; }
#else
            frmJobOpen frmJobOpen = new frmJobOpen();
            frmJobOpen.ShowDialog();

            if (frmJobOpen.Result != vbOK)
            {
                return false;
            }
            if (OpenProject_Check(this.ReceiveData, (int)frmJobOpen.lvProject.SelectedItems[0].Index) == false) { return false; }
#endif


            //[VB]---------------------------------------------------
            //[VB]      '再描画。
            //[VB]      If RedrawWindow(Me.hWnd, 0, 0, RDW_UPDATENOW) = 0 Then Call Err.Raise(ERR_FATAL, , GetLastErrorMessage())
            //----------------------------------.-----------------
            //再描画
            if (RedrawWindow(this.Handle, IntPtr.Zero, IntPtr.Zero, (int)DEFINE.RDW_UPDATENOW) == false)
            {
                return false;
            }

            //[VB]---------------------------------------------------
            //[VB]      '保存ファイルのパス。
            //[VB]      Dim sPath As String
            //[VB]      Dim clsProjectFileManager As New ProjectFileManager
            //[VB]      sPath = clsProjectFileManager.GetSaveFilePath(frmJobOpen.FolderName)
            string sPath;
            ProjectFileManager ProjectFile = new ProjectFileManager();
#if true   //K.S 0304
            ListViewItem itemx = form.lvProject.SelectedItems[0];
#else
            ListViewItem itemx = frmJobOpen.lvProject.SelectedItems[0];
#endif

            //------------------------------
            //フォルダ:Ex:0006
            string FolderName = itemx.SubItems[2].Text;

            //------------------------------
            //パス　:Ex"C:\\Develop\\NetSurv\\Src\\NS-App\\NS-Survey\\UserData\\0006\\"
            sPath = ProjectFile.GetSaveFilePath(FolderName);

            //------------------------------
            //'Documentを破棄し、Documentを確保する。
            //------------------------------
            m_clsMdlMain.Document_Dispose();
            m_clsDocument = m_clsMdlMain.GetDocument();

            //------------------------------
            //'閉じてから開く。
            //------------------------------
            //[VB]      Call CloseProject
            //------------------------------
            CloseProject();

            //-----------------------------------
            // 「XXXX.data」ファイル読み込み
            //-----------------------------------
            //[VB]      Call Load(sPath)
            Load_frmMain(sPath, fListPane);                  //Cursor = Cursors.Default;
            //------------------------------



            return true;
        }

        //**************************************************************************************
        //**************************************************************************************
        /// <summary>
        ///  プロジェクトを読み込む
        ///  引き数：
        ///  sPath 読み込み元ファイルのパス。
        /// </summary>
        /// <param name="sPath"></param>
        //**************************************************************************************
        private void Load_frmMain(string sPath, ListPane listPane)
        {
            //[VB]      '砂時計。
            //[VB]          Dim clsWaitCursor As New WaitCursor
            //[VB]          Set clsWaitCursor.Object = Me
            Cursor = Cursors.WaitCursor;

#if false       //sakai
            Document document = new Document();
#else           //sakai
            Document document = m_clsMdlMain.GetDocument();
#endif          //sakai

            //[VB]          On Error GoTo FileErrorHandler
            //[VB]          Call m_clsDocument.Load(sPath)
            //[VB]          On Error GoTo 0
            try
            {
                //サンプル>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
                //   GENBA_STRUCT_S Genba_S = new GENBA_STRUCT_S();
                //サンプル <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<


                //-----------------------------------
                // 「XXXX.data」ファイル読み込み
                //-----------------------------------

                List<GENBA_STRUCT_S> List_Genba_S = new List<GENBA_STRUCT_S>();

                document.Load(sPath, ref List_Genba_S);

                m_List_Genba_S = List_Genba_S;

#if false
//******************************************************************:
//坂井様へ　下記の処理は　ＮＧ　です、表示処理をお願いします
//******************************************************************:
                //-----------------------------------
                // リスト表示（観測点・ベクトル表示）
                //-----------------------------------
                listPane.ListDataDispMain();
#endif


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "エラーが発生");
                return;
            }
        }
        //[VB]---------------------------------------------------
        //[VB]  'プロジェクトを読み込む。
        //[VB]  '
        //[VB]  '引き数：
        //[VB]  'sPath 読み込み元ファイルのパス。
        //[VB]  Private Sub Load(ByVal sPath As String)
        //[VB]  
        //[VB]      '砂時計。
        //[VB]          Dim clsWaitCursor As New WaitCursor
        //[VB]          Set clsWaitCursor.Object = Me
        //[VB]  
        //[VB]          On Error GoTo FileErrorHandler
        //[VB]          Call m_clsDocument.Load(sPath)
        //[VB]          On Error GoTo 0
        //[VB]  
        //[VB]  
        //[VB]          Call clsWaitCursor.Back
        //[VB]  
        //[VB]  
        //[VB]          Exit Sub
        //[VB]  
        //[VB]  FileErrorHandler:
        //[VB]          Call MsgBox(Err.Description, vbCritical)
        //[VB]          Call m_clsDocument.Clear
        //[VB]  
        //[VB]          End Sub
        //**************************************************************************************
        //**************************************************************************************




        //**************************************************************************************
        //**************************************************************************************
        //'プロジェクトを閉じる。
        private void CloseProject()
        {
            //[VB]---------------------------------------------------
            //[VB]      '砂時計。
            //[VB]      Dim clsWaitCursor As New WaitCursor
            //[VB]      Set clsWaitCursor.Object = Me
            Cursor = Cursors.WaitCursor;




        }
        //[VB]    'プロジェクトを閉じる。
        //[VB]  Private Sub CloseProject()
        //[VB]    
        //[VB]      '砂時計。
        //[VB]      Dim clsWaitCursor As New WaitCursor
        //[VB]      Set clsWaitCursor.Object = Me
        //[VB]        
        //[VB]      'クリア。
        //[VB]      Call m_clsDocument.Clear
        //[VB]        
        //[VB]      '座標系番号を更新する。
        //[VB]      Call SetListIndexManual(cmbZone, m_clsDocument.CoordNum - 1)
        //[VB]    
        //[VB]    
        //[VB]      Call clsWaitCursor.Back
        //[VB]    
        //[VB]
        //[VB]  End Sub
        //**************************************************************************************
        //**************************************************************************************

        //**************************************************************************************
        //**************************************************************************************
        //2==========================================================================================
        /*[VB]
            'プロジェクトを開く。
            '
            '戻り値：
            '正常終了の場合 True を返す。
            'キャンセルの場合 False を返す。
            Private Function OpenProject() As Boolean

                OpenProject = False
    
                Call frmJobOpen.Show(1)
                If frmJobOpen.Result <> vbOK Then Exit Function
    
                '再描画。
                If RedrawWindow(Me.hWnd, 0, 0, RDW_UPDATENOW) = 0 Then Call Err.Raise(ERR_FATAL, , GetLastErrorMessage())
    
                '保存ファイルのパス。
                Dim sPath As String
                Dim clsProjectFileManager As New ProjectFileManager
                sPath = clsProjectFileManager.GetSaveFilePath(frmJobOpen.FolderName)
    
                '閉じてから開く。
                Call CloseProject
                Call Load(sPath)
    
                'ジオイドモデルのパスの評価。
                If m_clsDocument.GeoidoEnable Then
                    'ファイルの存在を確認。
                    Dim clsFind As New FileFind
                    If clsFind.FindFile(m_clsDocument.GeoidoPath) Then
                        Select Case AltitudeSupport_EstimateFile(m_clsDocument.GeoidoPath)
                        Case 0
                        Case 1
                            Call MsgBox("ジオイドモデルで指定されたファイルが不正です。" & vbCrLf & "ジオイド補正をOFFにします。", vbCritical)
                            m_clsDocument.GeoidoEnable = False
                        Case Else
                            Call Err.Raise(ERR_FATAL, , MESSAGE_GEOIDO_ESTIMATE)
                        End Select
                    Else
                        If m_clsDocument.GeoidoPathDef <> "" Then
                            If MsgBox("ジオイドモデルで指定されたファイルが見つかりません。" & vbCrLf & "「デフォルトで使用するジオイドモデルファイル」に変更しますか？", vbQuestion Or vbYesNo) = vbYes Then
                                m_clsDocument.GeoidoPath = m_clsDocument.GeoidoPathDef
                            Else
                                m_clsDocument.GeoidoEnable = False
                            End If
                        Else
                            Call MsgBox("ジオイドモデルで指定されたファイルが見つかりません。" & vbCrLf & "ジオイド補正をOFFにします。", vbCritical)
                            m_clsDocument.GeoidoEnable = False
                        End If
                    End If
                End If
    
                '2009/11 H.Nakamura
                'セミ・ダイナミックパラメータファイルのパスの評価。
                If m_clsDocument.SemiDynaEnable Then
                    'ファイルの存在を確認。
                    If clsFind.FindFile(m_clsDocument.SemiDynaPath) Then
                        If Not mdlSemiDyna.EstimateFile(m_clsDocument.SemiDynaPath) Then
                            Call MsgBox("セミ・ダイナミック補正で指定されたファイルが不正です。" & vbCrLf & "セミ・ダイナミック補正をOFFにします。", vbCritical)
                            m_clsDocument.SemiDynaEnable = False
                        End If
                    Else
                        If m_clsDocument.SemiDynaPathDef <> "" Then
                            If MsgBox("セミ・ダイナミック補正で指定されたファイルが見つかりません。" & vbCrLf & "「デフォルトで使用するセミ・ダイナミック補正パラメータファイル」に変更しますか？", vbQuestion Or vbYesNo) = vbYes Then
                                If clsFind.FindFile(m_clsDocument.SemiDynaPathDef) Then
                                    If Not mdlSemiDyna.EstimateFile(m_clsDocument.SemiDynaPathDef) Then
                                        Call MsgBox("デフォルトのセミ・ダイナミック補正で指定されたファイルが不正です。" & vbCrLf & "セミ・ダイナミック補正をOFFにします。", vbCritical)
                                        m_clsDocument.SemiDynaEnable = False
                                    Else
                                        m_clsDocument.SemiDynaPath = m_clsDocument.SemiDynaPathDef
                                    End If
                                Else
                                    Call MsgBox("デフォルトのセミ・ダイナミック補正で指定されたファイルが見つかりません。" & vbCrLf & "セミ・ダイナミック補正をOFFにします。", vbCritical)
                                    m_clsDocument.SemiDynaEnable = False
                                End If
                            Else
                                m_clsDocument.SemiDynaEnable = False
                            End If
                        Else
                            Call MsgBox("セミ・ダイナミック補正で指定されたファイルが見つかりません。" & vbCrLf & "セミ・ダイナミック補正をOFFにします。", vbCritical)
                            m_clsDocument.SemiDynaEnable = False
                        End If
                    End If
                End If
    
                '2009/11 H.Nakamura
                'セミ・ダイナミックの初期化。
                Call mdlSemiDyna.Initialize(m_clsDocument.SemiDynaEnable, m_clsDocument.SemiDynaPath)
    
                '座標系番号を更新する。
                Call SetListIndexManual(cmbZone, m_clsDocument.CoordNum - 1)
    
                OpenProject = True
    
            End Function
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#] 2
        /// <summary>
        /// 'プロジェクトを開く。 
        /// '
        /// </summary>
        /// <param name="sRecvData"></param>
        /// <param name="nIndex"></param>
        /// <returns>
        /// '戻り値：
        /// '正常終了の場合 True を返す。
        /// 'キャンセルの場合 False を返す。
        /// </returns>
        private bool OpenProject_Check(string sRecvData, int nIndex)
        {
            //---------------------------------------------------
            //サブフォームが「OK」ボタンにより処理を行う
            //---------------------------------------------------
            if (sRecvData == "OK")
            {
                try
                {
                    int itemxsle2 = nIndex;
                    if (itemxsle2 >= 0)
                    {
                        //  //*** 選択 OK ***
                        //  //DEBUG >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
                        //  ListViewItem itemx = form.lvProject.SelectedItems[0];
                        //  MessageBox.Show(itemx.Text + " | " + itemx.SubItems[1].Text + " | " + itemx.SubItems[2].Text);
                        //  //DEBUG <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<

                    }
                }
                catch (ArgumentOutOfRangeException)
                {
                    MessageBox.Show("「現場の選択画面」の操作を確認して下さい ");
                    return false;
                }
            }
            else
            {
                return false;
            }

            return true;
        }
        //**************************************************************************************
        //**************************************************************************************




        private void mnuFileClose_Click(object sender, EventArgs e)
        {
            MessageBox.Show(mnuFileClose.Text);

        }


        //==========================================================================================
        /*[VB]
        'プロジェクトを上書き保存する。
        Private Sub mnuFileSave_Click()

            On Error GoTo ErrorHandler
    
            'プロジェクトの上書き保存。
            If Not SaveProject() Then Exit Sub
    
            Exit Sub
    
        ErrorHandler:
            Call mdlMain.ErrorExit
    
        End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]  //2
        /// <summary>
        /// プロジェクトを上書き保存する。
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuFileSave_Click(object sender, EventArgs e)  //2
        {
            //2     MessageBox.Show(mnuFileSave.Text);
            try
            {
                //'プロジェクトの上書き保存。
                if (!SaveProject())             //2
                {
                    return;
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
        'プロジェクトを上書き保存する。
        '
        '戻り値：
        '正常終了の場合 True を返す。
        'キャンセルの場合 False を返す。
        Private Function SaveProject() As Boolean

            SaveProject = False
    
            If m_clsDocument.Path = "" Then
                'If Not SaveAsProject() Then Exit Function
            Else
                Call Save(m_clsDocument.Path)
            End If
    
            SaveProject = True
    
        End Function            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]  //2
        /// <summary>
        ///'プロジェクトを上書き保存する。
        ///'
        /// </summary>
        /// <returns>
        ///'戻り値：
        ///'正常終了の場合 True を返す。
        ///'キャンセルの場合 False を返す。
        /// </returns>
        private bool SaveProject()      //2
        {
            bool SaveProject = false;

            if (m_clsDocument.Path() == "")
            {
                if (!SaveAsProject())
                {
                    return SaveProject;
                }
            }
            else
            {
                Save(m_clsDocument.Path());
            }

            SaveProject = true;
            return SaveProject;

        }

        //==========================================================================================
        /*[VB]
        'プロジェクトを名前を付けて保存する。
        Private Sub mnuFileSaveAs_Click()

            On Error GoTo ErrorHandler
    
            'プロジェクトを名前を付けて保存。
            If Not SaveAsProject() Then Exit Sub
    
            'タイトル。
            Call UpdateTitle
    
            '座標系番号を更新する。
            Call SetListIndexManual(cmbZone, m_clsDocument.CoordNum - 1)
    
            '砂時計。
            Dim clsWaitCursor As New WaitCursor
            Set clsWaitCursor.Object = Me
    
            'プロットの再描画。
            Call objPlotPane.UpdateLogicalDrawArea(False)
            Call objPlotPane.Redraw
            Call objPlotPane.Refresh
    
            'ステータスバーの更新。
            Call UpdateStatusBar(STATUSBAR_NUM_COORDNUM)
            Call UpdateStatusBar(STATUSBAR_NUM_GEOIDO)
    
            Exit Sub
    
        ErrorHandler:
            Call mdlMain.ErrorExit
    
        End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /// <summary>
        /// 'プロジェクトを名前を付けて保存する。
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuFileSaveAs_Click(object sender, EventArgs e)
        {
            // MessageBox.Show(mnuFileSaveAs.Text);

            //'プロジェクトを名前を付けて保存。
            if (!SaveAsProject())
            {
                return;
            }


            //'タイトル。
            UpdateTitle();


            //'座標系番号を更新する。
            //Call SetListIndexManual(cmbZone, m_clsDocument.CoordNum -1)

            //'砂時計。
            Cursor = Cursors.WaitCursor;


            //'プロットの再描画。
            //Call objPlotPane.UpdateLogicalDrawArea(False)
            //Call objPlotPane.Redraw
            //Call objPlotPane.Refresh


            //'ステータスバーの更新。
            //Call UpdateStatusBar(STATUSBAR_NUM_COORDNUM)
            //Call UpdateStatusBar(STATUSBAR_NUM_GEOIDO)

        }
        //==========================================================================================
        /*[VB]
        'プロジェクトを名前を付けて保存する。
        '
        '戻り値：
        '正常終了の場合 True を返す。
        'キャンセルの場合 False を返す。
        Private Function SaveAsProject() As Boolean

            SaveAsProject = False
    
            '既存の値。
            frmJobEdit.JobName = m_clsDocument.JobName
            frmJobEdit.DistrictName = m_clsDocument.DistrictName
            frmJobEdit.Folder = ""
            frmJobEdit.CoordNum = m_clsDocument.CoordNum
            frmJobEdit.GeoidoEnable = m_clsDocument.GeoidoEnable
            frmJobEdit.GeoidoPath = m_clsDocument.GeoidoPath
    
            'セミ・ダイナミック対応。'2009/11 H.Nakamura
            frmJobEdit.SemiDynaEnable = m_clsDocument.SemiDynaEnable
            frmJobEdit.SemiDynaPath = m_clsDocument.SemiDynaPath
            frmJobEdit.SemiDynaValid = False
    
            'Caption を設定すると Form_Load が走るので注意すべし！
            frmJobEdit.Caption = "名前を付けて保存"
    
            Call frmJobEdit.Show(1)
            If frmJobEdit.Result <> vbOK Then Exit Function
    
            '再描画。
            If RedrawWindow(Me.hWnd, 0, 0, RDW_UPDATENOW) = 0 Then Call Err.Raise(ERR_FATAL, , GetLastErrorMessage())
    
            'プロジェクトフォルダの生成。
            On Error GoTo FileErrorHandler
            Dim clsProjectFileManager As New ProjectFileManager
            Dim sProjectFolderName As String
            sProjectFolderName = clsProjectFileManager.CreateProjectFolder
            If sProjectFolderName = "" Then
                Call MsgBox("これ以上現場を保存することができません。" & vbCrLf & "不要な現場を削除してください。", vbCritical)
                Exit Function
            End If
            On Error GoTo 0
    
            '設定。
            Call m_clsDocument.SetJob(frmJobEdit.JobName, frmJobEdit.DistrictName, frmJobEdit.CoordNum, frmJobEdit.GeoidoEnable, frmJobEdit.GeoidoPath, frmJobEdit.SemiDynaEnable, frmJobEdit.SemiDynaPath)
    
            '保存。
            Call Save(clsProjectFileManager.GetSaveFilePath(sProjectFolderName))
    
            SaveAsProject = True
    
            Exit Function
    
        FileErrorHandler:
            If Err.Number <> ERR_FILE Then Call Err.Raise(Err.Number, Err.Source, Err.Description, Err.HelpFile, Err.HelpContext)
            Call MsgBox(Err.Description, vbCritical)
    
        End Function
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /// <summary>
        /// 'プロジェクトを名前を付けて保存する。
        /// '
        /// '戻り値：
        /// '正常終了の場合 True を返す。
        /// 'キャンセルの場合 False を返す。
        /// 
        /// </summary>
        /// <returns>
        /// '戻り値：
        /// '正常終了の場合 True を返す。
        /// 'キャンセルの場合 False を返す。
        /// </returns>
        private bool SaveAsProject()
        {
            bool SaveAsProject = false;

            frmJobEdit frmJobEdit = new frmJobEdit
            {
                //'既存の値。
                JobName = m_clsDocument.JobName(),
                DistrictName = m_clsDocument.DistrictName(),
                Folder = "",
                CoordNum = m_clsDocument.CoordNum(),
                GeoidoEnable = m_clsDocument.GeoidoEnable(),
                GeoidoPath = m_clsDocument.GeoidoPath(),


                //'セミ・ダイナミック対応。'2009 / 11 H.Nakamura
                SemiDynaEnable = m_clsDocument.SemiDynaEnable(),
                SemiDynaPath = m_clsDocument.SemiDynaPath(),
                SemiDynaValid = false,

                //'Caption を設定すると Form_Load が走るので注意すべし！
                Text = "名前を付けて保存"
            };

            frmJobEdit.ShowDialog();
            if (frmJobEdit.Result != DEFINE.vbOK) { return false; }

            //'再描画。
            //If RedrawWindow(Me.hWnd, 0, 0, RDW_UPDATENOW) = 0 Then Call Err.Raise(ERR_FATAL, , GetLastErrorMessage())


            //'プロジェクトフォルダの生成。
            ProjectFileManager clsProjectFileManager = new ProjectFileManager();
            string sProjectFolderName;
            sProjectFolderName = clsProjectFileManager.CreateProjectFolder();
            if (sProjectFolderName == "")
            {
                _ = MessageBox.Show($"これ以上現場を保存することができません。\n 不要な現場を削除してください。", "エラー発生", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return SaveAsProject;
            }

            //'設定。
            _ = m_clsDocument.SetJob(frmJobEdit.JobName, frmJobEdit.DistrictName, frmJobEdit.CoordNum, frmJobEdit.GeoidoEnable, frmJobEdit.GeoidoPath, frmJobEdit.SemiDynaEnable, frmJobEdit.SemiDynaPath);

            //'保存。
            Save(clsProjectFileManager.GetSaveFilePath(sProjectFolderName));


            SaveAsProject = true;
            return SaveAsProject;
        }


        //==========================================================================================
        /*[VB]
        private void mnuFileEdit_Click(object sender, EventArgs e)
        {

            On Error GoTo ErrorHandler


            '現場の新規作成。
            Call EditJob


            'タイトル。
            Call UpdateTitle


            '座標系番号を更新する。
            Call SetListIndexManual(cmbZone, m_clsDocument.CoordNum -1)
    
            '砂時計。
            Dim clsWaitCursor As New WaitCursor
            Set clsWaitCursor.Object = Me


            'プロットの再描画。
            Call objPlotPane.UpdateLogicalDrawArea(False)
            Call objPlotPane.Redraw
            Call objPlotPane.Refresh


            'メニューの更新。
            mnuAccountSemiDynaGAN2KON.Enabled = (mdlSemiDyna.m_nSemiDyna<> 0) 'セミ・ダイナミック対応。'2009 / 11 H.Nakamura


            'ステータスバーの更新。
            Call UpdateStatusBar(STATUSBAR_NUM_COORDNUM)
            Call UpdateStatusBar(STATUSBAR_NUM_GEOIDO)


            Exit Sub


        ErrorHandler:
                    Call mdlMain.ErrorExit

                End Sub

        }
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /// <summary>
        /// 'プロジェクトを編集する。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        private void mnuFileEdit_Click(object sender, EventArgs e)
        {

            //現場の新規作成。
            _ = EditJob(sender);


            //'タイトル。
            UpdateTitle();

            //'座標系番号を更新する。
            //  SetListIndexManual(cmbZone, m_clsDocument.CoordNum - 1);

            //'砂時計。
            Cursor = Cursors.WaitCursor;


            //'プロットの再描画。
            //Call objPlotPane.UpdateLogicalDrawArea(False)
            //Call objPlotPane.Redraw
            //Call objPlotPane.Refresh


            //'メニューの更新。
            //mnuAccountSemiDynaGAN2KON.Enabled = (mdlSemiDyna.m_nSemiDyna<> 0) 'セミ・ダイナミック対応。'2009 / 11 H.Nakamura


            //'ステータスバーの更新。
            //UpdateStatusBar(STATUSBAR_NUM_COORDNUM);
            //UpdateStatusBar(STATUSBAR_NUM_GEOIDO);


        }

        //==========================================================================================
        /*[VB]
        '現場の編集。
        '
        '戻り値：
        '正常終了の場合 True を返す。
        'キャンセルの場合 False を返す。
        Private Function EditJob() As Boolean

            EditJob = False

            '既存の値。
            frmJobEdit.JobName = m_clsDocument.JobName
            frmJobEdit.DistrictName = m_clsDocument.DistrictName
            frmJobEdit.Folder = m_clsDocument.Folder
            frmJobEdit.CoordNum = m_clsDocument.CoordNum
            frmJobEdit.GeoidoEnable = m_clsDocument.GeoidoEnable
            frmJobEdit.GeoidoPath = m_clsDocument.GeoidoPath


            'セミ・ダイナミック対応。'2009/11 H.Nakamura
            frmJobEdit.SemiDynaEnable = m_clsDocument.SemiDynaEnable
            frmJobEdit.SemiDynaPath = m_clsDocument.SemiDynaPath
            frmJobEdit.SemiDynaValid = True


            Call frmJobEdit.Show(1)
            If frmJobEdit.Result<> vbOK Then Exit Function

            '再描画。
            If RedrawWindow(Me.hWnd, 0, 0, RDW_UPDATENOW) = 0 Then Call Err.Raise(ERR_FATAL, , GetLastErrorMessage())

            '設定。
            Call m_clsDocument.SetJob(frmJobEdit.JobName, frmJobEdit.DistrictName, frmJobEdit.CoordNum, frmJobEdit.GeoidoEnable, frmJobEdit.GeoidoPath, frmJobEdit.SemiDynaEnable, frmJobEdit.SemiDynaPath)

            '2009/11 H.Nakamura
            'セミ・ダイナミックパラメータファイルのパスはデフォルトにも反映させる。
            If m_clsDocument.SemiDynaEnable Then
                m_clsDocument.SemiDynaPathDef = m_clsDocument.SemiDynaPath
                If WritePrivateProfileString(PROFILE_SAVE_SEC_SEMIDYNA, PROFILE_SAVE_KEY_PATH, m_clsDocument.SemiDynaPathDef, App.Path & "\" & App.Title & PROFILE_SAVE_EXT) = 0 Then Call Err.Raise(ERR_FATAL, , GetLastErrorMessage())
            End If

            '2009/11 H.Nakamura
            'セミ・ダイナミックの初期化。
            Call mdlSemiDyna.Initialize(m_clsDocument.SemiDynaEnable, m_clsDocument.SemiDynaPath)

            EditJob = True


        End Function

            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        private bool EditJob(object sender)
        {

            bool EditJob = false;


            frmJobEdit frmJobEdit = new frmJobEdit
            {
                //'既存の値。
                JobName = m_clsDocument.JobName(),
                DistrictName = m_clsDocument.DistrictName(),
                Folder = m_clsDocument.Folder(),
                CoordNum = m_clsDocument.CoordNum(),
                GeoidoEnable = m_clsDocument.GeoidoEnable(),
                GeoidoPath = m_clsDocument.GeoidoPath(),


                //'セミ・ダイナミック対応。'2009 / 11 H.Nakamura
                SemiDynaEnable = m_clsDocument.SemiDynaEnable(),
                SemiDynaPath = m_clsDocument.SemiDynaPath(),
                SemiDynaValid = true
            };


            _ = frmJobEdit.ShowDialog();
            if (frmJobEdit.Result != vbOK) { return false; }



            //再描画
            if (RedrawWindow(this.Handle, IntPtr.Zero, IntPtr.Zero, (int)RDW_UPDATENOW) == false)
            {
                return false;
            }

            //'設定。
            _ = m_clsDocument.SetJob(frmJobEdit.JobName, frmJobEdit.DistrictName, frmJobEdit.CoordNum, frmJobEdit.GeoidoEnable, frmJobEdit.GeoidoPath, frmJobEdit.SemiDynaEnable, frmJobEdit.SemiDynaPath);


            //'2009/11 H.Nakamura
            //'セミ・ダイナミックパラメータファイルのパスはデフォルトにも反映させる。
            string App_Path = @"C:\Develop\NetSurv\Src\NS-App\NS-Survey";
            string App_Title = "NS-Survey";
            if (m_clsDocument.m_bSemiDynaEnable)
            {
                m_clsDocument.SemiDynaPathDef = m_clsDocument.m_sSemiDynaPath;

                //--------------------------------------------
                //
                // "C:\Develop\NetSurv\Src\NS-App\NS-Survey" "\" "NS-Survey" .ini
                //      App_Path = C:\Hitz\NetSurv\Prog\Src
                //      App_Title =  "NS-Survey"  
                //
                if (!iniFileControl.WritePrivateProfileString(MdlNSDefine.PROFILE_SAVE_SEC_SEMIDYNA, MdlNSDefine.PROFILE_SAVE_KEY_PATH, m_clsDocument.SemiDynaPathDef, $"{App_Path}\\{App_Title}{MdlNSSDefine.PROFILE_SAVE_EXT}"))
                {
                    //Call Err.Raise(ERR_FATAL, , GetLastErrorMessage())
                }
            }


            //'2009/11 H.Nakamura
            //'セミ・ダイナミックの初期化。
            MdlSemiDyna mdlSemiDyna = new MdlSemiDyna();
            mdlSemiDyna.Initialize(m_clsDocument.m_bSemiDynaEnable, m_clsDocument.m_sSemiDynaPath);


            return EditJob;
        }


        private void mnuCheck_Click(object sender, EventArgs e)
        {

        }

        private void mnuAnalysis_Click(object sender, EventArgs e)
        {

        }

        private void mnuAccount_Click(object sender, EventArgs e)
        {

        }

        private void mnuAccountEtc_Click(object sender, EventArgs e)
        {

        }
        public string ReceiveData
        {
            set
            {
                receiveData = value;
            }
            get
            {
                return receiveData;
            }
        }

        private void toolBar1_ButtonClick(object sender, ToolBarButtonClickEventArgs e)
        {

        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {

        }

        private void 新規現場作成ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void toolStripSplitButton1_ButtonClick(object sender, EventArgs e)
        {

        }

        //観測点／ベクトル
        private void listPane1_Load(object sender, EventArgs e)
        {



        }

        private void mnuFile_Click(object sender, EventArgs e)
        {

        }

        private void mnuFile_Click_1(object sender, EventArgs e)
        {

        }



#if false
        //==========================================================================================
        /*[VB]
        'ドキュメントを取得する。
        '
        '戻り値：Document オブジェクト。
        Public Function GetDocument() As Document
            If m_clsDocument Is Nothing Then Set m_clsDocument = New Document
            Set GetDocument = m_clsDocument
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        'ドキュメントを取得する。
        '
        '戻り値：Document オブジェクト。
        */
        public Document GetDocument()
        {
            if (m_clsDocument == null)
            {
                Document m_clsDocument = new Document();
                clsDocument.Class_Initialize();
            }
            return m_clsDocument;
        }
        //==========================================================================================
#endif

        //==========================================================================================
        /*[VB]
        'ファイルダイアログを取得する。
        '
        '戻り値：FileDialog オブジェクト。
        Public Function GetFileDialog() As FileDialog
            If m_clsFileDialog Is Nothing Then Set m_clsFileDialog = New FileDialog
            Set GetFileDialog = m_clsFileDialog
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        'ファイルダイアログを取得する。
        '
        '戻り値：FileDialog オブジェクト。
        */
#if false
        public FileDialog GetFileDialog()
        {
            /*
             *************************** 修正要 sakai
             */
             * if (m_clsFileDialog == null) { FileDialog m_clsFileDialog = new FileDialog; }
            return m_clsFileDialog;
        }
#else   //瀬戸口　作成
        public object GetFileDialog()
        {
            if (m_clsFileDialog == null) { m_clsFileDialog = new FileDialog(); }
            return (object)m_clsFileDialog;
        }

#endif
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'ファイルのドロップ。
        Public Sub DropFiles(data As Object)

            On Error GoTo ErrorHandler


            If Not mnuFileImport.Enabled Then Exit Sub
            If data.Files.Count< 1 Then Exit Sub
    
            'ngs_check_dat.dll の CheckDatType 関数だと、RINEXファイルをNS2000のDATファイルに間違える可能性があるかもしれない。
            'NS6000、NS4000、NS3000のDATファイルの先頭は0xAA、NS5000のDATファイルの先頭は0x02なので、ASCIIでないことからRINEXと区別できる。
            'NS2000のDATファイルの先頭は't'である。
            'ngs_rdrin302.dll の CheckVersionRNX 関数でRINEXファイルのバージョンを評価し、0 が返った場合はDATファイルとみなせる。
            '
            '結論：
            '拡張子で判断する。


            Dim sPath() As String
            ReDim sPath(data.Files.Count - 1)


            Dim nType As Integer
            Dim sDrive As String
            Dim sDir As String
            Dim sTitle As String
            Dim sExt As String
            Dim i As Integer
            For i = 1 To data.Files.Count
                Call SplitPath(data.Files.Item(i), sDrive, sDir, sTitle, sExt)
                sExt = StrConv(sExt, vbUpperCase)
                If(sExt = ".DAT") Then
                    nType = nType Or &H1
                Else
                    If Mid(sExt, 4, 1) = "O" Then
                        Dim nC As Integer
                        nC = Asc(Mid(sExt, 2, 1))
                        If(nC< 48) Or(57 < nC) Then
                            nType = &H8
                            Exit For
                        End If
                        nC = Asc(Mid(sExt, 3, 1))
                        If(nC< 48) Or(57 < nC) Then
                            nType = &H8
                            Exit For
                        End If
                        nType = nType Or &H2
                    Else
                        nType = &H8
                        Exit For
                    End If
                End If
                sPath(i - 1) = data.Files.Item(i)
            Next


            Dim nImportType As IMPORT_TYPE
            If(nType And &H8) <> 0 Then
                Call MsgBox("ファイルの拡張子が不正です。", vbCritical Or vbOKOnly)
                Exit Sub
            ElseIf nType = &H1 Then
                nImportType = IMPORT_TYPE_DAT
            ElseIf nType = &H2 Then
                nImportType = IMPORT_TYPE_RINEX
            Else
                Call MsgBox("DATファイルとRINEXファイルが混在しています。種類を分けてインポートして下さい。", vbCritical Or vbOKOnly)
                Exit Sub
            End If
    
            'インポート。
            Dim clsProcessImport As New ProcessImport
            Call clsProcessImport.Import(nImportType, sPath, False)
    
            '再描画。
            If RedrawWindow(Me.hWnd, 0, 0, RDW_UPDATENOW) = 0 Then Call Err.Raise(ERR_FATAL, , GetLastErrorMessage())
    
            '砂時計。
            Dim clsWaitCursor As New WaitCursor
            Set clsWaitCursor.Object = Me
    
            'リストの作成。
            Call objListPane.RemakeList(True)
    
            'プロットの再描画。
            Call objPlotPane.UpdateLogicalDrawArea(True)
            Call objPlotPane.Redraw
            Call objPlotPane.Refresh

            Call clsWaitCursor.Back

            Exit Sub

        ErrorHandler:
            Call mdlMain.ErrorExit

        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'ファイルのドロップ。
        public void DropFiles(object data)
        {
            return;
        }

        private void Form_Load(object sender, EventArgs e)
        {

        }




        //==========================================================================================
        /*[VB]
         * 新規
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /// <summary>
        /// 【インポート】NetSurvデータファイル：   'nvfファイルをインポートする。
        ///  インポート：
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuFileImportNVF_Click(object sender, EventArgs e)
        {
            try
            {
                //'インポート。
                FileImportClick(IMPORT_TYPE.IMPORT_TYPE_NVF);

            }
            catch (Exception ex)
            {
                //Call mdlMain.ErrorExit

            }

        }


        //==========================================================================================
        /*[VB]
            'DATファイルをインポートする。
            Private Sub mnuFileImportDAT_Click()

                On Error GoTo ErrorHandler
    
                'インポート。
                Call FileImportClick(IMPORT_TYPE_DAT)
    
                Exit Sub
    
            ErrorHandler:
                Call mdlMain.ErrorExit
    
            End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /// <summary>
        /// 【インポート】NetSurvデータファイル：   'DATファイルをインポートする。
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuFileImportDAT_Click(object sender, EventArgs e)
        {
            try
            {
                //'インポート。
                FileImportClick(IMPORT_TYPE.IMPORT_TYPE_DAT);

            }
            catch (Exception ex)
            {
                //Call mdlMain.ErrorExit
                MessageBox.Show(ex.Message, "エラー発生", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }
        //==========================================================================================
        /*[VB]
        'インポートコマンド。
        Private Sub FileImportClick(ByVal nImportType As IMPORT_TYPE)

            'インポート。
            If Not Import(nImportType) Then Exit Sub
    
            '砂時計。
            Dim clsWaitCursor As New WaitCursor
            Set clsWaitCursor.Object = Me
    
            'リストの作成。
            Call objListPane.RemakeList(True)
    
            'プロットの再描画。
            Call objPlotPane.UpdateLogicalDrawArea(True)
            Call objPlotPane.Redraw
            Call objPlotPane.Refresh
    
            Call clsWaitCursor.Back
    
        End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /// <summary>
        /// インポートコマンド。
        /// 
        /// </summary>
        /// <param name="nImportType"></param>
        private void FileImportClick(IMPORT_TYPE nImportType)
        {
            if (!Import(nImportType))
            {
                return;
            }

            //'砂時計。
            Cursor = Cursors.WaitCursor;

            //坂井様へ'リストの作成。
            //坂井様へCall objListPane.RemakeList(True)


            //坂井様へ'プロットの再描画。
            //坂井様へCall objPlotPane.UpdateLogicalDrawArea(True)
            //坂井様へCall objPlotPane.Redraw
            //坂井様へCall objPlotPane.Refresh



            Cursor = Cursors.Default;

        }

        //==========================================================================================
        /*[VB]
        '外部ファイルをインポートする。
        '
        '引き数：
        'nImportType インポートファイル種別。
        '
        '戻り値：
        '正常終了の場合 True を返す。
        'キャンセルの場合 False を返す。
        Private Function Import(ByVal nImportType As IMPORT_TYPE) As Boolean

            Import = False
    
            Dim sPath() As String
    
            If nImportType <> IMPORT_TYPE_DIRECT Then
                dlgCommonDialog.DialogTitle = "インポート"
                dlgCommonDialog.CancelError = True
                dlgCommonDialog.flags = cdlOFNFileMustExist Or cdlOFNHideReadOnly Or cdlOFNLongNames Or cdlOFNNoChangeDir Or cdlOFNPathMustExist Or cdlOFNAllowMultiselect Or cdlOFNExplorer
                dlgCommonDialog.MaxFileSize = 1024
                dlgCommonDialog.FileName = ""
                Select Case nImportType
                Case IMPORT_TYPE_UNKNOWN
                    dlgCommonDialog.DefaultExt = IMPORT_FILE_EXT
                    dlgCommonDialog.Filter = IMPORT_FILE_FILTER
                Case IMPORT_TYPE_JOB
                    dlgCommonDialog.DefaultExt = JOB_FILE_EXT
                    dlgCommonDialog.Filter = JOB_FILE_FILTER
                Case IMPORT_TYPE_NVF
                    dlgCommonDialog.DefaultExt = NVF_FILE_EXT
                    dlgCommonDialog.Filter = NVF_FILE_FILTER
                Case IMPORT_TYPE_DAT
                    dlgCommonDialog.DefaultExt = DAT_FILE_EXT
                    dlgCommonDialog.Filter = DAT_FILE_FILTER
                Case IMPORT_TYPE_RINEX
                    dlgCommonDialog.DefaultExt = RINEX_FILE_EXT
                    dlgCommonDialog.Filter = RINEX_FILE_FILTER
                End Select
                dlgCommonDialog.FilterIndex = 1
        
                If Not GetFileDialog().ShowOpen(dlgCommonDialog) Then Exit Function
        
                '再描画。
                If RedrawWindow(Me.hWnd, 0, 0, RDW_UPDATENOW) = 0 Then Call Err.Raise(ERR_FATAL, , GetLastErrorMessage())
        
                Call ConvertOpenDialogMultiList(dlgCommonDialog.FileName, sPath)
            Else
                With frmImportDirect
        '            .ComPortType = m_clsDocument.ImportComPortType
                    .ComPortCount = 0
            
                    If m_clsDocument.ImportComPortType = True Then  '2007/7/2 NGS Yamada
        '                Dim autoComPort As String
        '                If SearchComPort(autoComPort) = True Then
        '                    .ComPort = autoComPort
                        If SearchComPort(frmImportDirect) = True Then
                        Else
                            Call MsgBox("NetSurvとの接続に失敗しました。", vbCritical)
                            Exit Function
                        End If
                    Else
                        '.ComPort = "COM" & m_clsDocument.ImportComPort + 1
                        Call .AddComPort(m_clsDocument.ImportComPort + 1)
                    End If
                    .DataSave = m_clsDocument.ImportDataSave
        '            .ComPort = "COM" & m_clsDocument.ImportComPort + 1
                    .Path = App.Path & TEMPORARY_PATH & IMPORT_PATH
                    .Show vbModal
            
                    If .Result <> vbOK Then Exit Function
            
                    sPath = .SaveFilePaths
                End With
            End If
            'インポート。
            Dim clsProcessImport As New ProcessImport
            Call clsProcessImport.Import(nImportType, sPath, False)
    
            '再描画。
            If RedrawWindow(Me.hWnd, 0, 0, RDW_UPDATENOW) = 0 Then Call Err.Raise(ERR_FATAL, , GetLastErrorMessage())
    
            Import = True
    
        End Function
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]

        private bool Import(IMPORT_TYPE nImportType)
        {
            bool Import = false;

            List<string> sPath = new List<string>();


            OpenFileDialog dlgCommonDialog = new OpenFileDialog();


            if (nImportType != IMPORT_TYPE.IMPORT_TYPE_DIRECT)
            {
                dlgCommonDialog.Title = "インポート";
                //dlgCommonDialog.CancelError = True
                //dlgCommonDialog.flags = cdlOFNFileMustExist Or cdlOFNHideReadOnly Or cdlOFNLongNames Or cdlOFNNoChangeDir Or cdlOFNPathMustExist Or cdlOFNAllowMultiselect Or cdlOFNExplorer
                //dlgCommonDialog.MaxFileSize = 1024;

                dlgCommonDialog.Filter = "テキストファイル (*.txt)|*.txt|"
                                 + "すべてのファイル (*.*)|*.*";
                dlgCommonDialog.FilterIndex = 2;
                dlgCommonDialog.Multiselect = true;

                dlgCommonDialog.FileName = "";
                switch (nImportType)
                {
                    case IMPORT_TYPE.IMPORT_TYPE_UNKNOWN:
                        dlgCommonDialog.DefaultExt = MdlNSSDefine.IMPORT_FILE_EXT;
                        dlgCommonDialog.Filter = MdlNSSDefine.IMPORT_FILE_FILTER;
                        break;
                    case IMPORT_TYPE.IMPORT_TYPE_JOB:
                        dlgCommonDialog.DefaultExt = JOB_FILE_EXT;
                        dlgCommonDialog.Filter = JOB_FILE_FILTER;
                        break;
                    case IMPORT_TYPE.IMPORT_TYPE_NVF:
                        dlgCommonDialog.DefaultExt = NVF_FILE_EXT;
                        dlgCommonDialog.Filter = NVF_FILE_FILTER;
                        break;
                    case IMPORT_TYPE.IMPORT_TYPE_DAT:
                        dlgCommonDialog.DefaultExt = DAT_FILE_EXT;
                        dlgCommonDialog.Filter = DAT_FILE_FILTER;
                        break;
                    case IMPORT_TYPE.IMPORT_TYPE_RINEX:
                        dlgCommonDialog.DefaultExt = RINEX_FILE_EXT;
                        dlgCommonDialog.Filter = RINEX_FILE_FILTER;
                        break;
                    default:
                        return Import;
                }
                dlgCommonDialog.FilterIndex = 1;

                FileDialog fd = (FileDialog)m_clsMdlMain.GetFileDialog();

                if (!fd.ShowOpen(dlgCommonDialog))
                {
                    return Import;
                }

                //'再描画。
                //If RedrawWindow(Me.hWnd, 0, 0, RDW_UPDATENOW) = 0 Then Call Err.Raise(ERR_FATAL, , GetLastErrorMessage())


                m_clsMdlGUI.ConvertOpenDialogMultiList(dlgCommonDialog, ref sPath);


            }
            else
            {
                //        With frmImportDirect
                //'            .ComPortType = m_clsDocument.ImportComPortType
                //            .ComPortCount = 0;

            }

            //'インポート。
            ProcessImport clsProcessImport = new ProcessImport();
            clsProcessImport.Import(nImportType, ref sPath, false);


            //            '再描画。
            //            If RedrawWindow(Me.hWnd, 0, 0, RDW_UPDATENOW) = 0 Then Call Err.Raise(ERR_FATAL, , GetLastErrorMessage())


            Import = true;
            return Import;

        }

        //==========================================================================================
        /*[VB]
        'RINEXファイルをインポートする。
        Private Sub mnuFileImportRINEX_Click()

            On Error GoTo ErrorHandler
    
            'インポート。
            Call FileImportClick(IMPORT_TYPE_RINEX)
    
            Exit Sub
    
        ErrorHandler:
            Call mdlMain.ErrorExit
    
        End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /// <summary>
        ///  【インポート】RINEXファイル：RINEXファイルをインポートする。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuFileImportRINEX_Click(object sender, EventArgs e)
        {
            try
            {
                //'インポート。
                FileImportClick(IMPORT_TYPE.IMPORT_TYPE_RINEX);

            }
            catch (Exception ex)
            {
                //Call mdlMain.ErrorExit

            }

        }


        //==========================================================================================
        /*[VB]
             'RINEXファイルをエクスポートする。
            Private Sub mnuFileExportRinex_Click()

                On Error GoTo ErrorHandler


                Dim clsOutputParam As New OutputParam
                Let clsOutputParam = m_clsDocument.OutputParam(OUTPUT_TYPE_RINEX)
    
                '帳票パラメータを入力する。
                frmAccountInfo.AccountType = ACCOUNT_TYPE_RINEX
                Let frmAccountInfo.AccountParam = clsOutputParam.AccountParam
                Call frmAccountInfo.Show(1)
                If frmAccountInfo.Result<> vbOK Then Exit Sub


                Let clsOutputParam.AccountParam = frmAccountInfo.AccountParam


                Call ExportRinex(clsOutputParam, True)

                Exit Sub

            ErrorHandler:
                Call mdlMain.ErrorExit

            End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /// <summary>
        /// 【エクスポート】RINEXファイル：RINEXファイルをエクスポートする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuFileExportRinex_Click(object sender, EventArgs e)
        {

            try
            {

                //On Error GoTo ErrorHandler

                OutputParam clsOutputParam = new OutputParam();

                clsOutputParam = (OutputParam)m_clsDocument.OutputParam((long)MdlNSSDefine.OUTPUT_TYPE.OUTPUT_TYPE_RINEX);



                frmAccountInfo frmAccountInfo = new frmAccountInfo
                {
                    //'帳票パラメータを入力する。
                    AccountType = (long)ACCOUNT_TYPE.ACCOUNT_TYPE_RINEX
                };



                frmAccountInfo.AccountParam(clsOutputParam.AccountParam());
                _ = frmAccountInfo.ShowDialog();
                if (frmAccountInfo.Result != vbOK)
                {
                    return;
                }


                clsOutputParam.AccountParam((AccountParam)frmAccountInfo.AccountParam());


                ExportRinex(clsOutputParam, true);

            }
            catch (Exception ex)
            {
                //ErrorHandler:
                //  Call mdlMain.ErrorExit
                _ = MessageBox.Show(ex.Message, "エラー発生", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }

        //==========================================================================================
        /*[VB]
            'RINEXファイルのエクスポート。
            '
            'bAutoFileName に True が指定された場合、出力先のフォルダだけを指定して出力ファイル名は自動で決定する。
            'bAutoFileName に False が指定された場合、出力ファイルのパスは一つ一つ指定する。
            '
            '引き数：
            'clsOutputParam 外部出力ファイル出力パラメータ。
            'bAutoFileName 自動ファイル名フラグ。
            Private Sub ExportRinex(ByVal clsOutputParam As OutputParam, ByVal bAutoFileName As Boolean)

                On Error GoTo CancelHandler
    
                '観測点リスト。
                Dim clsObservationPoints() As ObservationPoint
                clsObservationPoints = GetAccountObservationPoints(m_clsDocument.NetworkModel.RepresentPointHead, clsOutputParam.AccountParam)
                '出力するものがなければ戻る。
                If UBound(clsObservationPoints) < 0 Then
                    Call MsgBox(MESSAGE_CONTENT_EMPTY, vbExclamation)
                    Exit Sub
                End If


                If bAutoFileName Then
                    Dim clsFolderDialog As New FolderDialog
                    clsFolderDialog.Caption = "エクスポート"
                    clsFolderDialog.Title = "出力先のフォルダを選択してください。"
                    clsFolderDialog.Path = clsOutputParam.Path
                    clsFolderDialog.CancelError = True
                    Call clsFolderDialog.ShowOpen(Me.hWnd)

                    clsOutputParam.Path = clsFolderDialog.Path
        
                    '出力。
                    Dim clsProcessExport As New ProcessExport
                    Call clsProcessExport.ExportRinexMulti(clsOutputParam)
                Else
                    dlgCommonDialog.CancelError = True
                    dlgCommonDialog.DefaultExt = ""
                    dlgCommonDialog.Filter = RINEX_FILE_FILTER
                    dlgCommonDialog.FilterIndex = 1
                    dlgCommonDialog.flags = cdlOFNHideReadOnly Or cdlOFNLongNames Or cdlOFNNoChangeDir Or cdlOFNNoReadOnlyReturn Or cdlOFNPathMustExist 'ここでは上書き確認はしない。


                    Dim i As Long
                    For i = 0 To UBound(clsObservationPoints)
                        Dim sSrcRinexExt As String
                        sSrcRinexExt = StrConv("." & clsObservationPoints(i).RinexExt, vbUpperCase)


                        dlgCommonDialog.DialogTitle = clsObservationPoints(i).Number & "-" & clsObservationPoints(i).Session & " のエクスポート"
                        'RINEXファイルのファイル名称を「観測点No＋セッション名」に変更 2006/12/22 NGS Yamada
            '            dlgCommonDialog.FileName = IIf(clsOutputParam.Path <> "", clsOutputParam.Path & "\", "") & clsObservationPoints(i).SrcTitle & sSrcRinexExt & RNX_OBS_EXTENSION
                        dlgCommonDialog.FileName = IIf(clsOutputParam.Path<> "", clsOutputParam.Path & "\", "") & clsObservationPoints(i).RinexTitle & sSrcRinexExt & RNX_OBS_EXTENSION


                        If Not GetFileDialog().ShowSave(dlgCommonDialog) Then Exit Sub


                        Dim sDrive As String
                        Dim sDir As String
                        Dim sTitle As String
                        Dim sExt As String
                        Call SplitPath(dlgCommonDialog.FileName, sDrive, sDir, sTitle, sExt)
                        clsOutputParam.Path = RTrimEx(sDrive & sDir, "\")
                        Dim sDstRinexExt As String
                        sDstRinexExt = RTrimEx(StrConv(sExt, vbUpperCase), StrConv(RNX_OBS_EXTENSION, vbUpperCase))


                        If sSrcRinexExt<> sDstRinexExt Then
                            sTitle = sTitle & sExt
                            sDstRinexExt = sSrcRinexExt
                        End If


                        '出力。
                        Set clsProcessExport = New ProcessExport
                        If Not clsProcessExport.ExportRinexSingle(clsOutputParam, sTitle & sDstRinexExt, clsObservationPoints(i)) Then Exit Sub
                    Next
                End If


                Exit Sub


            CancelHandler:
                If Err.Number<> cdlCancel Then Call Err.Raise(Err.Number, Err.Source, Err.Description, Err.HelpFile, Err.HelpContext)


            End Sub

            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        private void ExportRinex(OutputParam clsOutputParam, bool bAutoFileName)
        {

            //On Error GoTo CancelHandler


            //'観測点リスト。
            List<ObservationPoint> clsObservationPoints = new List<ObservationPoint>();

            //            clsObservationPoints = GetAccountObservationPoints(m_clsDocument.NetworkModel.RepresentPointHead, clsOutputParam.AccountParam)
            //'出力するものがなければ戻る。
            if (clsObservationPoints.Count < 0)
            {
                //Call MsgBox(MESSAGE_CONTENT_EMPTY, vbExclamation)
            }



        }


        //==========================================================================================
        /*[VB]
        'プロジェクトを削除する。
        Private Sub mnuFileRemove_Click()

            On Error GoTo ErrorHandler
    
            '既存のプロジェクトを閉じる。
            If Not ConfirmCloseProject() Then Exit Sub
    
            '現場の削除。
            If Not RemoveJob Then Exit Sub
    
            'タイトル。
            Call UpdateTitle
    
            '砂時計。
            Dim clsWaitCursor As New WaitCursor
            Set clsWaitCursor.Object = Me
    
            'リストの作成。
            Call objListPane.RemakeList(False)
    
            'プロットの再描画。
            Call objPlotPane.UpdateLogicalDrawArea(True)
            Call objPlotPane.Redraw
            Call objPlotPane.Refresh
    
            'ステータスバーの更新。
            Call UpdateStatusBarAll
    
            'ドキュメントのOpen/Closeによるメニューの更新。
            Call UpdateDocumentMenu
    
            Call clsWaitCursor.Back
    
            Exit Sub
    
        ErrorHandler:
            Call mdlMain.ErrorExit

        End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /// <summary>
        /// 現場を削除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuFileRemove_Click(object sender, EventArgs e)
        {
            //坂井様へ  //On Error GoTo ErrorHandler  


            //坂井様へ //'既存のプロジェクトを閉じる。
            //坂井様へ //If Not ConfirmCloseProject() Then Exit Sub


            //'現場の削除。
            if (!RemoveJob())
            {
                return;
            }


            //'タイトル。
            UpdateTitle();


            //'砂時計。
            Cursor = Cursors.WaitCursor;


            //坂井様へ'リストの作成。
            //坂井様へCall objListPane.RemakeList(False)


            //坂井様へ'プロットの再描画。
            //坂井様へCall objPlotPane.UpdateLogicalDrawArea(True)
            //坂井様へCall objPlotPane.Redraw
            //坂井様へCall objPlotPane.Refresh


            //坂井様へ'ステータスバーの更新。
            //坂井様へCall UpdateStatusBarAll


            //坂井様へ'ドキュメントのOpen/Closeによるメニューの更新。
            //坂井様へCall UpdateDocumentMenu


            Cursor = Cursors.Default;


        }
        //<<<<<<<<<-----------24/01/24 K.setoguchi@NV
        //***************************************************************************


        //24/01/24 K.setoguchi@NV---------->>>>>>>>>>
        //***************************************************************************
        //==========================================================================================
        /*[VB]
        '現場の削除。
        '
        '戻り値：
        '正常終了の場合 True を返す。
        'キャンセルの場合 False を返す。
        Private Function RemoveJob() As Boolean

            RemoveJob = False
    
            On Error GoTo FileErrorHandler
    
            '再描画。
            If RedrawWindow(Me.hWnd, 0, 0, RDW_UPDATENOW) = 0 Then Call Err.Raise(ERR_FATAL, , GetLastErrorMessage())
    
            frmJobSelect.Caption = "現場を削除"
            frmJobSelect.Description = "削除する現場を選択してください｡"
            frmJobSelect.TextOK = "削除"
            frmJobSelect.MsgOK = "選択された現場を削除します。"
            frmJobSelect.MsgUnselected = "削除する現場をチェックしてください。"
            Call frmJobSelect.Show(1)
            If frmJobSelect.Result <> vbOK Then Exit Function
    
            '再描画。
            If RedrawWindow(Me.hWnd, 0, 0, RDW_UPDATENOW) = 0 Then Call Err.Raise(ERR_FATAL, , GetLastErrorMessage())
    
            '閉じてから削除する。
            Call CloseProject
    
            Dim sFolderNames() As String
            sFolderNames = frmJobSelect.FolderNames
            Dim clsProjectFileManager As New ProjectFileManager
    
            Dim i As Long
            For i = 0 To UBound(sFolderNames)
                Call clsProjectFileManager.DeleteProjectFolder(sFolderNames(i))
            Next
    
            RemoveJob = True
    
            Exit Function
    
        FileErrorHandler:
            If Err.Number <> ERR_FILE Then Call Err.Raise(Err.Number, Err.Source, Err.Description, Err.HelpFile, Err.HelpContext)
            Call MsgBox(Err.Description, vbCritical)
    
        End Function
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /// <summary>
        /// '現場の削除。
        /// 
        /// </summary>
        /// <returns>
        /// '戻り値：
        ///     '正常終了の場合 True を返す。
        ///     'キャンセルの場合 False を返す。
        /// </returns>
        private bool RemoveJob()
        {
            bool RemoveJob = false;

            //坂井様へOn Error GoTo FileErrorHandler


            //'再描画。
            if (RedrawWindow(Handle, IntPtr.Zero, IntPtr.Zero, (int)RDW_UPDATENOW) == false)
            {
                //Then Call Err.Raise(ERR_FATAL, , GetLastErrorMessage())
            }


            frmJobSelect frmJobSelect = new frmJobSelect
            {
                Text = "現場を削除"
            };
            frmJobSelect.Description("削除する現場を選択してください｡");
            frmJobSelect.TextOK("削除");
            frmJobSelect.MsgOK = "選択された現場を削除します。";
            frmJobSelect.MsgUnselected = "削除する現場をチェックしてください。";

            _ = frmJobSelect.ShowDialog();

            if (frmJobSelect.Result != vbOK) { return false; }

            //再描画
            if (RedrawWindow(Handle, IntPtr.Zero, IntPtr.Zero, (int)RDW_UPDATENOW) == false)
            {
                return false;
            }


            //'閉じてから削除する。
            CloseProject();


            // sFolderNames() As String
            List<string> sFolderNames = new List<string>();

            //  sFolderNames.Add(frmJobSelect.FolderNames());
            sFolderNames = frmJobSelect.FolderNames();

            ProjectFileManager clsProjectFileManager = new ProjectFileManager();


            for (int i = 0; i < sFolderNames.Count; i++)
            {
                clsProjectFileManager.DeleteProjectFolder(sFolderNames[i]);
            }


            RemoveJob = true;
            return RemoveJob;
        }

        //==========================================================================================
        /*[VB]
            'プロジェクトをインポートする。
            Private Sub mnuFileImportProject_Click()

                On Error GoTo ErrorHandler
    
                '既存のプロジェクトを閉じる。
                If Not ConfirmCloseProject() Then Exit Sub
    
                '現場のインポート。
                If Not ImportProject Then Exit Sub
    
                'タイトル。
                Call UpdateTitle
    
                '砂時計。
                Dim clsWaitCursor As New WaitCursor
                Set clsWaitCursor.Object = Me
    
                'リストの作成。
                Call objListPane.RemakeList(False)
    
                'プロットの再描画。
                Call objPlotPane.UpdateLogicalDrawArea(True)
                Call objPlotPane.Redraw
                Call objPlotPane.Refresh
    
                'ステータスバーの更新。
                Call UpdateStatusBarAll
    
                'ドキュメントのOpen/Closeによるメニューの更新。
                Call UpdateDocumentMenu
    
                Call clsWaitCursor.Back
    
                Exit Sub
    
            ErrorHandler:
                Call mdlMain.ErrorExit

            End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /// <summary>
        /// プロジェクトをインポートする。
        ///     メニュー：現場のインポート。
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuFileImportProject_Click(object sender, EventArgs e)
        {
            try
            {
                //  On Error GoTo ErrorHandler

                //坂井様へ  '既存のプロジェクトを閉じる。
                //坂井様へ  If Not ConfirmCloseProject() Then Exit Sub


                //'現場のインポート。
                if (!ImportProject())
                {
                    return;
                }

                //'タイトル。
                UpdateTitle();


                //'砂時計。
                Cursor = Cursors.WaitCursor;


                //坂井様へ    'リストの作成。
                //坂井様へCall objListPane.RemakeList(False)


                //坂井様へ'プロットの再描画。
                //坂井様へCall objPlotPane.UpdateLogicalDrawArea(True)
                //坂井様へCall objPlotPane.Redraw
                //坂井様へCall objPlotPane.Refresh


                //坂井様へ'ステータスバーの更新。
                //坂井様へCall UpdateStatusBarAll


                //坂井様へ  'ドキュメントのOpen/Closeによるメニューの更新。
                //坂井様へ  Call UpdateDocumentMenu


                Cursor = Cursors.Default;

                return;
            }
            catch (Exception ex)
            {
                //ErrorHandler:
                //  Call mdlMain.ErrorExit
                _ = MessageBox.Show(ex.Message, "エラー発生", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }
        //==========================================================================================
        /*[VB]
            '現場のインポート。
            '
            '戻り値：
            '正常終了の場合 True を返す。
            'キャンセルの場合 False を返す。
            Private Function ImportProject() As Boolean

                ImportProject = False
    
                On Error GoTo CancelHandler
    
                dlgCommonDialog.DialogTitle = "開く"
                dlgCommonDialog.CancelError = True
                dlgCommonDialog.DefaultExt = NSSEXP_FILE_EXT
                dlgCommonDialog.Filter = NSSEXP_FILE_FILTER
                dlgCommonDialog.FilterIndex = 1
                dlgCommonDialog.flags = cdlOFNFileMustExist Or cdlOFNHideReadOnly Or cdlOFNLongNames Or cdlOFNNoChangeDir Or cdlOFNPathMustExist Or cdlOFNAllowMultiselect Or cdlOFNExplorer
                dlgCommonDialog.MaxFileSize = 1024
                dlgCommonDialog.FileName = ""
    
                If Not GetFileDialog().ShowOpen(dlgCommonDialog) Then Exit Function
    
                '再描画。
                If RedrawWindow(Me.hWnd, 0, 0, RDW_UPDATENOW) = 0 Then Call Err.Raise(ERR_FATAL, , GetLastErrorMessage())
    
                '閉じてからインポートする。
                Call CloseProject
    
                Dim sPath() As String
                Call ConvertOpenDialogMultiList(dlgCommonDialog.FileName, sPath)
    
                'インポート。
                Dim clsProcessProject As New ProcessProject
    
                If Not clsProcessProject.ImportProjectFolders(sPath) Then Exit Function
    
                ImportProject = True
    
                Exit Function
    
            CancelHandler:
                If Err.Number <> cdlCancel Then Call MsgBox(Err.Description, vbCritical)
    
            End Function
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        private bool ImportProject()
        {
            bool ImportProject = false;
#if false
            dlgCommonDialog.Title = "インポート";
            //dlgCommonDialog.CancelError = True
            //dlgCommonDialog.flags = cdlOFNFileMustExist Or cdlOFNHideReadOnly Or cdlOFNLongNames Or cdlOFNNoChangeDir Or cdlOFNPathMustExist Or cdlOFNAllowMultiselect Or cdlOFNExplorer
            //dlgCommonDialog.MaxFileSize = 1024;

            dlgCommonDialog.Filter = "テキストファイル (*.txt)|*.txt|"
                             + "すべてのファイル (*.*)|*.*";
            dlgCommonDialog.FilterIndex = 2;
            dlgCommonDialog.Multiselect = true;

            dlgCommonDialog.FileName = "";
#else   //-------------------------------------------------------------------
            OpenFileDialog dlgCommonDialog = new OpenFileDialog
            {
                Title = "開く",
                //    dlgCommonDialog.CancelError = True
                //     dlgCommonDialog.DefaultExt = NSSEXP_FILE_EXT
                Filter = MdlNSDefine.NSSEXP_FILE_FILTER,
                FilterIndex = 1,
                //     dlgCommonDialog.flags = cdlOFNFileMustExist Or cdlOFNHideReadOnly Or cdlOFNLongNames Or cdlOFNNoChangeDir Or cdlOFNPathMustExist Or cdlOFNAllowMultiselect Or cdlOFNExplorer
                //     dlgCommonDialog.MaxFileSize = 1024
                FileName = ""
            };
#endif


            FileDialog fd = (FileDialog)m_clsMdlMain.GetFileDialog();

            if (!fd.ShowOpen(dlgCommonDialog))
            {
                return ImportProject;
            }

            //'再描画。
            //If RedrawWindow(Me.hWnd, 0, 0, RDW_UPDATENOW) = 0 Then Call Err.Raise(ERR_FATAL, , GetLastErrorMessage())


            //'閉じてからインポートする。
            CloseProject();

            List<string> sPath = new List<string>();
            m_clsMdlGUI.ConvertOpenDialogMultiList(dlgCommonDialog, ref sPath);



            //'インポート。
            ProcessProject clsProcessProject = new ProcessProject();
            if (!clsProcessProject.ImportProjectFolders(ref sPath))
            {
                return ImportProject;
            }


            ImportProject = true;
            return ImportProject;

        }

        //==========================================================================================
        /*[VB]
            'プロジェクトをエクスポートする。
            Private Sub mnuFileExportProject_Click()

                On Error GoTo ErrorHandler
    
                '既存のプロジェクトを閉じる。
                If Not ConfirmCloseProject() Then Exit Sub
    
                '現場のエクスポート。
                If Not ExportProject Then Exit Sub
    
                'タイトル。
                Call UpdateTitle
    
                '砂時計。
                Dim clsWaitCursor As New WaitCursor
                Set clsWaitCursor.Object = Me
    
                'リストの作成。
                Call objListPane.RemakeList(False)
    
                'プロットの再描画。
                Call objPlotPane.UpdateLogicalDrawArea(True)
                Call objPlotPane.Redraw
                Call objPlotPane.Refresh
    
                'ステータスバーの更新。
                Call UpdateStatusBarAll
    
                'ドキュメントのOpen/Closeによるメニューの更新。
                Call UpdateDocumentMenu


                Call clsWaitCursor.Back

                Exit Sub

            ErrorHandler:
                Call mdlMain.ErrorExit

            End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /// <summary>
        ///  プロジェクトをエクスポートする。                
        ///     メニュー：現場のエクスポート。
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuFileExportProject_Click(object sender, EventArgs e)
        {

            try
            {
                //On Error GoTo ErrorHandler


                //坂井様へ  '既存のプロジェクトを閉じる。
                //坂井様へ  If Not ConfirmCloseProject() Then Exit Sub


                //'現場のエクスポート。
                if (!ExportProject()) { return; }


                //タイトル。
                UpdateTitle();


                //'砂時計。
                Cursor = Cursors.WaitCursor;


                //坂井様へ  'リストの作成。
                //坂井様へ  Call objListPane.RemakeList(False)


                //坂井様へ  'プロットの再描画。
                //坂井様へ  Call objPlotPane.UpdateLogicalDrawArea(True)
                //坂井様へ  Call objPlotPane.Redraw
                //坂井様へ  Call objPlotPane.Refresh


                //坂井様へ  'ステータスバーの更新。
                //坂井様へ  Call UpdateStatusBarAll


                //坂井様へ  'ドキュメントのOpen/Closeによるメニューの更新。
                //坂井様へ  Call UpdateDocumentMenu

                Cursor = Cursors.Default;

                return;
            }
            catch (Exception ex)
            {
                //ErrorHandler:
                //  Call mdlMain.ErrorExit
                _ = MessageBox.Show(ex.Message, "エラー発生", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }


        //==========================================================================================
        /*[VB]
            '現場のエクスポート。
            '
            '戻り値：
            '正常終了の場合 True を返す。
            'キャンセルの場合 False を返す。
            Private Function ExportProject() As Boolean

                ExportProject = False
    
                On Error GoTo CancelHandler
    
                '再描画。
                If RedrawWindow(Me.hWnd, 0, 0, RDW_UPDATENOW) = 0 Then Call Err.Raise(ERR_FATAL, , GetLastErrorMessage())
    
                frmJobSelect.Caption = "現場をエクスポート"
                frmJobSelect.Description = "エクスポートする現場を選択してください｡"
                frmJobSelect.TextOK = "OK"
                frmJobSelect.MsgOK = ""
                frmJobSelect.MsgUnselected = "エクスポートする現場をチェックしてください。"
                Call frmJobSelect.Show(1)
                If frmJobSelect.Result <> vbOK Then Exit Function
    
                '再描画。
                If RedrawWindow(Me.hWnd, 0, 0, RDW_UPDATENOW) = 0 Then Call Err.Raise(ERR_FATAL, , GetLastErrorMessage())
    
                If 0 < UBound(frmJobSelect.FolderNames) Then
                    '出力先フォルダを選択。
                    Dim clsFolderDialog As New FolderDialog
                    clsFolderDialog.Caption = "現場をエクスポート"
                    clsFolderDialog.Title = "出力先のフォルダを選択してください。"
                    clsFolderDialog.Path = ""
                    clsFolderDialog.CancelError = True
                    Call clsFolderDialog.ShowOpen(Me.hWnd)
        
                    Dim sPath As String
                    Dim bSingle As Boolean
                    sPath = clsFolderDialog.Path
                    bSingle = False
                Else
                    Dim sFolderNames() As String
                    sFolderNames = frmJobSelect.FolderNames
                    Dim clsProjectFileManager As New ProjectFileManager
        
                    Dim sJobName As String
                    Dim sDistrictName As String
                    If Not clsProjectFileManager.GetJobInfo(sFolderNames(0), sJobName, sDistrictName) Then Exit Function
        
                    '出力先ファイルを選択。
                    dlgCommonDialog.DialogTitle = "名前を付けて保存"
                    dlgCommonDialog.CancelError = True
                    dlgCommonDialog.DefaultExt = NSSEXP_FILE_EXT
                    dlgCommonDialog.Filter = NSSEXP_FILE_FILTER
                    dlgCommonDialog.FilterIndex = 1
                    dlgCommonDialog.flags = cdlOFNHideReadOnly Or cdlOFNLongNames Or cdlOFNNoChangeDir Or cdlOFNNoReadOnlyReturn Or cdlOFNOverwritePrompt Or cdlOFNPathMustExist
                    dlgCommonDialog.FileName = sJobName & "." & NSSEXP_FILE_EXT
                    If Not GetFileDialog().ShowSave(dlgCommonDialog) Then Exit Function
        
                    sPath = dlgCommonDialog.FileName
                    bSingle = True
                End If
    
                '再描画。
                If RedrawWindow(Me.hWnd, 0, 0, RDW_UPDATENOW) = 0 Then Call Err.Raise(ERR_FATAL, , GetLastErrorMessage())
    
                '閉じてからエクスポートする。
                Call CloseProject
    
                'エクスポート。
                Dim clsProcessProject As New ProcessProject
                If Not clsProcessProject.ExportProjectFolders(frmJobSelect.FolderNames, sPath, bSingle) Then Exit Function
    
                ExportProject = True
    
                Exit Function
    
            CancelHandler:
                If Err.Number <> cdlCancel Then Call MsgBox(Err.Description, vbCritical)
    
            End Function
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /// <summary>
        /// 現場のエクスポート。
        /// 
        /// </summary>
        /// <returns>
        /// 戻り値：
        ///     正常終了の場合 True を返す。
        ///     キャンセルの場合 False を返す。
        /// </returns>
        private bool ExportProject()
        {
            bool ExportProject = false;

            try
            {
                //On Error GoTo CancelHandler


                //'再描画。
                if (RedrawWindow(Handle, IntPtr.Zero, IntPtr.Zero, (int)RDW_UPDATENOW) == false)
                {
                    //坂井様へ  
                    //Then Call Err.Raise(ERR_FATAL, , GetLastErrorMessage())
                }


                frmJobSelect frmJobSelect = new frmJobSelect
                {
                    Text = "現場をエクスポート"
                };
                frmJobSelect.lblDescription.Text = "エクスポートする現場を選択してください｡";
                frmJobSelect.TextOK("OK");
                frmJobSelect.MsgOK = "";
                frmJobSelect.MsgUnselected = "エクスポートする現場をチェックしてください。";

                _ = frmJobSelect.ShowDialog();

                if (frmJobSelect.Result != vbOK) { return false; }

                ExportProject = true;

                //'再描画。
                if (RedrawWindow(Handle, IntPtr.Zero, IntPtr.Zero, (int)RDW_UPDATENOW) == false)
                {
                    //坂井様へ  
                    //Then Call Err.Raise(ERR_FATAL, , GetLastErrorMessage())
                }

                string sPath;
                bool bSingle;
                List<string> sFolderNames = new List<string>();

                sFolderNames = frmJobSelect.FolderNames();

                if (sFolderNames.Count > 1)
                {
                    //'出力先フォルダを選択。  複数選択の場合は、フォルダをダイアログ

                    FolderDialog clsFolderDialog = new FolderDialog
                    {
                        Caption = "現場をエクスポート",
                        Title = "出力先のフォルダを選択してください。",
                        Path = "",
                        CancelError = true
                    };

                    clsFolderDialog.ShowOpen(frmJobSelect.Handle);

                    sPath = clsFolderDialog.Path;
                    bSingle = false;

                }
                else
                {
                    //選択が１つの場合

                    ProjectFileManager clsProjectFileManager = new ProjectFileManager();

                    string sJobName = "";
                    string sDistrictName = "";

                    if (!clsProjectFileManager.GetJobInfo(sFolderNames[0], ref sJobName, ref sDistrictName))
                    {
                        return ExportProject;
                    }


                    OpenFileDialog dlgCommonDialog = new OpenFileDialog
                    {
                        //'出力先ファイルを選択。
                        Title = "名前を付けて保存",
                        //dlgCommonDialog.CancelError = True
                        //dlgCommonDialog.DefaultExt = NSSEXP_FILE_EXT
                        Filter = MdlNSDefine.NSSEXP_FILE_FILTER,
                        FilterIndex = 1,
                        //dlgCommonDialog.flags = cdlOFNHideReadOnly Or cdlOFNLongNames Or cdlOFNNoChangeDir Or cdlOFNNoReadOnlyReturn Or cdlOFNOverwritePrompt Or cdlOFNPathMustExist
                        FileName = $"{sJobName}.{MdlNSDefine.NSSEXP_FILE_EXT}"
                    };

                    FileDialog fd = (FileDialog)m_clsMdlMain.GetFileDialog();

                    if (!fd.ShowSave(dlgCommonDialog))
                    {
                        return ExportProject;
                    }

                    sPath = dlgCommonDialog.FileName;
                    bSingle = true;

                }

                //'再描画。
                if (RedrawWindow(Handle, IntPtr.Zero, IntPtr.Zero, (int)RDW_UPDATENOW) == false)
                {
                    //坂井様へ  
                    //  Then Call Err.Raise(ERR_FATAL, , GetLastErrorMessage())
                    return false;
                }


                //'閉じてからエクスポートする。
                CloseProject();


                //'エクスポート。
                ProcessProject clsProcessProject = new ProcessProject();

                if (!clsProcessProject.ExportProjectFolders(ref sFolderNames, sPath, bSingle))
                {
                    return ExportProject;
                }

                ExportProject = true;


            }
            catch (Exception ex)
            {
                //CancelHandler:
                //  If Err.Number<> cdlCancel Then Call MsgBox(Err.Description, vbCritical)
                _ = MessageBox.Show(ex.Message, "エラー発生", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }


            return ExportProject;

        }

        //==========================================================================================
        /*[VB]
            '受信機からDATファイルをインポートする。 2007/4/10 NGS Yamada
            Private Sub mnuFileImportDirect_Click()

                On Error GoTo ErrorHandler
    
                'インポート。
                Call FileImportClick(IMPORT_TYPE_DIRECT)
    
                Exit Sub
    
            ErrorHandler:
                Call mdlMain.ErrorExit

            End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /// <summary>
        ////  【インポート】受信機からインポート：受信機からDATファイルをインポートする。 2007/4/10 NGS Yamada
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuFileImportDirect_Click(object sender, EventArgs e)
        {
            try
            {
                //'インポート。
                FileImportClick(IMPORT_TYPE.IMPORT_TYPE_DIRECT);

            }
            catch (Exception ex)
            {
                //Call mdlMain.ErrorExit

            }
        }

        private void mnuFileExport_Click(object sender, EventArgs e)
        {

        }

        //************************************************************************************
        //************************************************************************************
        //************************************************************************************


        //1==========================================================================================
        /*[VB]
            '観測点の共通属性を編集する。
            Private Sub mnuEditAttributeCommon_Click()

                On Error GoTo ErrorHandler
    
                '属性の編集。
                Dim clsObservationPoint As ObservationPoint
                Set clsObservationPoint = objListPane.SelectedElement(LIST_NUM_OBSPNT)
                If Not EditAttributeCommon(clsObservationPoint) Then Exit Sub
    
                '砂時計。
                Dim clsWaitCursor As New WaitCursor
                Set clsWaitCursor.Object = Me
    
                '再描画抑制。
                Dim clsCtrlValue As New CtrlValue
                Call clsCtrlValue.SetObject(m_clsChangeSelRow, False)
    
                'スクロールはしないようにする。
                objListPane.RowLock = True
                '汎用作業キーを利用して、設定が変更された観測点を記憶する。
                Call m_clsDocument.NetworkModel.ClearIsList
                If clsObservationPoint.Eccentric Then clsObservationPoint.CorrectPoint.TopParentPoint.IsList = True
                Set clsObservationPoint = clsObservationPoint.HeadPoint
                Do While Not clsObservationPoint Is Nothing
                    clsObservationPoint.IsList = True
                    Dim clsBaseLineVectors() As BaseLineVector
                    ReDim clsBaseLineVectors(-1 To -1)
                    Call m_clsDocument.NetworkModel.GetConnectBaseLineVectors(clsObservationPoint, clsBaseLineVectors)
                    Dim i As Long
                    For i = 0 To UBound(clsBaseLineVectors)
                        clsBaseLineVectors(i).IsList = True
                    Next
                    Set clsObservationPoint = clsObservationPoint.NextPoint
                Loop
                'リストの更新。
                Call objListPane.UpdateRowIsList
                 'スクロールを元に戻す。
                objListPane.RowLock = False
    
                'プロットの再描画。
                Call objPlotPane.UpdateLogicalDrawArea(False)
                Call objPlotPane.Redraw
    
                'メニューの更新。
                Call clsCtrlValue.Back
                Call objListPane_ChangeSelRow(objListPane.List)
    
                Call clsWaitCursor.Back
    
                Exit Sub
    
            ErrorHandler:
                Call mdlMain.ErrorExit
    
            End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /// <summary>
        /// 編集＞観測点情報の編集
        /// 
        /// '観測点の共通属性を編集する。
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuEditAttributeCommon_Click(object sender, EventArgs e)
        {
            try
            {
                //On Error GoTo ErrorHandler

                //'属性の編集。
                ObservationPoint clsObservationPoint;
                clsObservationPoint = (ObservationPoint)objListPane.SelectedElement((long)LIST_NUM_PANE.LIST_NUM_OBSPNT);
                if (!EditAttributeCommon(clsObservationPoint))
                {
                    Cursor = Cursors.Default;
                    return;
                }

                //'砂時計。
                Cursor = Cursors.WaitCursor;

                //'再描画抑制。
                CtrlValue clsCtrlValue = new CtrlValue();
                clsCtrlValue.SetObject(m_clsChangeSelRow, false);

#if false
                //'スクロールはしないようにする。
                objListPane.RowLock = true;
                '汎用作業キーを利用して、設定が変更された観測点を記憶する。
                Call m_clsDocument.NetworkModel.ClearIsList
                If clsObservationPoint.Eccentric Then clsObservationPoint.CorrectPoint.TopParentPoint.IsList = True
                Set clsObservationPoint = clsObservationPoint.HeadPoint
                Do While Not clsObservationPoint Is Nothing
                    clsObservationPoint.IsList = True
                    Dim clsBaseLineVectors() As BaseLineVector
                    ReDim clsBaseLineVectors(-1 To - 1)
                    Call m_clsDocument.NetworkModel.GetConnectBaseLineVectors(clsObservationPoint, clsBaseLineVectors)
                    Dim i As Long
                    For i = 0 To UBound(clsBaseLineVectors)
                        clsBaseLineVectors(i).IsList = True
                    Next
                    Set clsObservationPoint = clsObservationPoint.NextPoint
                Loop
#endif

#if false       //コメント（瀬戸口）：リスト更新が正しいが、リスト作成で対応する


                //'リストの更新。
                objListPane.UpdateRowIsList();          //2


                //'スクロールを元に戻す。
                objListPane.RowLock(false);             //2
#else
                //'リストの作成。
                objListPane.SelectElement(null);        //2
                objListPane.RemakeList(false);          //2
#endif

                //'プロットの再描画。                             
                //  Call objPlotPane.UpdateLogicalDrawArea(False)    
                //  Call objPlotPane.Redraw
                Bitmap btmp = objPlotPane.Dis_Get_btmp();       //2
                objPlotPane.List_Genba_set(m_List_Genba_S);     //2
                objPlotPane.UpdateLogicalDrawArea(true);        //2
                objPlotPane.Redraw();                           //2
                objPlotPane.Refresh();                          //2


                //'メニューの更新。
                //  Call clsCtrlValue.Back
                //  Call objListPane_ChangeSelRow(objListPane.List)


               Cursor = Cursors.Default;


            }
            catch (Exception ex)
            {
                //    Call mdlMain.ErrorExit
                MessageBox.Show(ex.Message, "エラー発生", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }
        //1==========================================================================================
        /*[VB]
             '観測点の属性を編集する。
            Private Sub mnuEditAttribute_Click()

                On Error GoTo ErrorHandler
    
                Dim clsObservationPoint As ObservationPoint
                Set clsObservationPoint = objListPane.SelectedElement(LIST_NUM_OBSPNT)
    
                '汎用作業キーを利用して、設定が変更された観測点を記憶する。
                Call m_clsDocument.NetworkModel.ClearIsList
                clsObservationPoint.IsList = True
    
                Dim clsBaseLineVectors() As BaseLineVector
                ReDim clsBaseLineVectors(-1 To -1)
                Call GetDocument().NetworkModel.GetConnectBaseLineVectors(clsObservationPoint, clsBaseLineVectors)
                Dim bAnalysis As Boolean '解析済か？
                Dim i As Long
                For i = 0 To UBound(clsBaseLineVectors)
                    If clsBaseLineVectors(i).Analysis <= ANALYSIS_STATUS_FIX Then
                        '解析削除あり。
                        bAnalysis = True
                        '偏心補正の再計算の有無にかかわらず更新する。方位票の評価にはコストがかかる。
                        If clsBaseLineVectors(i).StrPoint.Number = clsObservationPoint.Number Then
                            Call GetDocument().NetworkModel.SetConnectBaseLineVectorsIsListEx(clsBaseLineVectors(i).EndPoint)
                        Else
                            Call GetDocument().NetworkModel.SetConnectBaseLineVectorsIsListEx(clsBaseLineVectors(i).StrPoint)
                        End If
                    End If
                Next
                Call GetDocument().NetworkModel.SetConnectBaseLineVectorsIsListEx(clsObservationPoint)
    
                '属性の編集。
                If Not EditAttribute(clsObservationPoint, bAnalysis) Then Exit Sub
    
                '砂時計。
                Dim clsWaitCursor As New WaitCursor
                Set clsWaitCursor.Object = Me
    
                '再描画抑制。
                Dim clsCtrlValue As New CtrlValue
                Call clsCtrlValue.SetObject(m_clsChangeSelRow, False)
    
                'スクロールはしないようにする。
                objListPane.RowLock = True
                'リストの更新。
                Call objListPane.UpdateRowIsList
                 'スクロールを元に戻す。
                objListPane.RowLock = False
    
                'プロットの再描画。
                Call objPlotPane.UpdateLogicalDrawArea(False)
                Call objPlotPane.Redraw
    
                'メニューの更新。
                Call clsCtrlValue.Back
                Call objListPane_ChangeSelRow(objListPane.List)
    
                Call clsWaitCursor.Back
    
                Exit Sub
    
            ErrorHandler:
                Call mdlMain.ErrorExit

            End Sub
           [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //
        /// <summary>
        /// 編集＞観測点データの編集
        ///
        /// '観測点の属性を編集する。
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuEditAttribute_Click(object sender, EventArgs e)
        {
            try
            {

#if false   // DEBUG 画面レイアウト確認------------------------------------
                frmAttribute frmAttribute = new frmAttribute();
                frmAttribute.ShowDialog();  //編集＞観測点データの編集
#endif      // DEBUG 画面レイアウト確認------------------------------------

                ObservationPoint clsObservationPoint;

                clsObservationPoint = (ObservationPoint)objListPane.SelectedElement((long)LIST_NUM_PANE.LIST_NUM_OBSPNT);


#if false
                //'汎用作業キーを利用して、設定が変更された観測点を記憶する。
                m_clsDocument.NetworkModel.ClearIsList;
                clsObservationPoint.IsList(true);
                List<BaseLineVector> clsBaseLineVectors = new List<BaseLineVector>();
                clsBaseLineVectors.Clear();

                Call GetDocument().NetworkModel.GetConnectBaseLineVectors(clsObservationPoint, clsBaseLineVectors)
#endif
                bool bAnalysis = false;     //'解析済か？
#if false
                long i;
                for (i = 0; i < clsBaseLineVectors.Count; i++)
                {
                    if (clsBaseLineVectors[(int)i].Analysis <= ANALYSIS_STATUS.ANALYSIS_STATUS_FIX)
                    {
                        //'解析削除あり。
                        bAnalysis = true;
                        //'偏心補正の再計算の有無にかかわらず更新する。方位票の評価にはコストがかかる。
                        if (clsBaseLineVectors[(int)i].StrPoint.Number = clsObservationPoint.Number)
                        {
                            //  Call GetDocument().NetworkModel.SetConnectBaseLineVectorsIsListEx(clsBaseLineVectors(i).EndPoint)
                        }
                        else
                        {
                            //Call GetDocument().NetworkModel.SetConnectBaseLineVectorsIsListEx(clsBaseLineVectors(i).StrPoint)
                        }

                    }

                }

                Call GetDocument().NetworkModel.SetConnectBaseLineVectorsIsListEx(clsObservationPoint)
#endif

                //'属性の編集。
                if (!EditAttribute(clsObservationPoint, bAnalysis))
                {
                    Cursor = Cursors.Default;
                    return;
                }


                //'砂時計。
                Cursor = Cursors.WaitCursor;

                //'再描画抑制。
                CtrlValue clsCtrlValue = new CtrlValue();
                clsCtrlValue.SetObject(m_clsChangeSelRow, false);



                //'スクロールはしないようにする。
                //      objListPane.RowLock = True
                //'リストの更新。
                //      Call objListPane.UpdateRowIsList
                // 'スクロールを元に戻す。
                //      objListPane.RowLock = False


                //'プロットの再描画。
                //      Call objPlotPane.UpdateLogicalDrawArea(False)
                //      Call objPlotPane.Redraw


                //'メニューの更新。
                //      Call clsCtrlValue.Back
                //      Call objListPane_ChangeSelRow(objListPane.List)


                Cursor = Cursors.Default;


            }
            catch (Exception ex)
            {
                //    Call mdlMain.ErrorExit
                MessageBox.Show(ex.Message, "エラー発生", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //1==========================================================================================
        /*[VB]
            '基線ベクトルを編集する。
            Private Sub mnuEditBaseLine_Click()

                On Error GoTo ErrorHandler
    
                If m_clsPopupMenuController.IsPopup(mnuEditBaseLine) Then Exit Sub
    
                Dim clsBaseLineVector As BaseLineVector
                Set clsBaseLineVector = objListPane.SelectedElement(LIST_NUM_VECTOR)
    
                '属性の編集。
                If Not EditBaseLine(clsBaseLineVector) Then Exit Sub
    
                Exit Sub
    
            ErrorHandler:
                Call mdlMain.ErrorExit

            End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /// <summary>
        /// 編集＞基線ベクトルの編集
        /// 
        /// 基線ベクトルを編集する。
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuEditBaseLine_Click(object sender, EventArgs e)
        {

        }
        //1==========================================================================================
        /*[VB]
            'セッションを変更する。
            Private Sub mnuEditSession_Click()

                On Error GoTo ErrorHandler
    
                '選択オブジェクト。
                Dim objElements As New Collection
                Dim objElement As Object
                Dim nPos As Long
                '観測点、基線ベクトルのどちらかのみ選択されているはず。
                nPos = objListPane.StartSelectedAssoc(LIST_NUM_OBSPNT)
                Do While (nPos > 0)
                    Set objElement = objListPane.GetNextAssoc(nPos)
                    '観測点の場合、本点は除外する。
                    If objElement.Genuine Then GoTo ContinueHandler
                    Call objElements.Add(objElement, Hex$(GetPointer(objElement)))
            ContinueHandler:
                Loop
                nPos = objListPane.StartSelectedAssoc(LIST_NUM_VECTOR)
                Do While (nPos > 0)
                    Set objElement = objListPane.GetNextAssoc(nPos)
                    Call objElements.Add(objElement, Hex$(GetPointer(objElement)))
                Loop
    
                '同じ観測点番号が選択されていたら許可しない。
                If Not CheckSessionObsName(objElements, "同じ観測点Noの観測点が選択されています。" & vbCrLf & "同じ観測点Noの観測点を同じセッションに変更することは出来ません｡ ", "始点と終点で同じ観測点Noの基線ベクトルが選択されています。" & vbCrLf & "同じ観測点Noを始点終点に持つ基線ベクトルを同じセッションに変更することは出来ません｡ ") Then Exit Sub
    
                'セッションの変更。
                If Not EditSession(objElements) Then Exit Sub
    
                '砂時計。
                Dim clsWaitCursor As New WaitCursor
                Set clsWaitCursor.Object = Me
    
                '再描画抑制。
                Dim clsCtrlValue As New CtrlValue
                Call clsCtrlValue.SetObject(m_clsChangeSelRow, False)
    
                'スクロールはしないようにする。
                objListPane.RowLock = True
                '汎用作業キーを利用して、設定が変更された観測点を記憶する。
                Call m_clsDocument.NetworkModel.ClearIsList
                For Each objElement In objElements
                    objElement.IsList = True
                    If (objElement.ObjectType And OBJ_TYPE_OBSERVATIONPOINT) <> 0 Then
                        Dim clsBaseLineVectors() As BaseLineVector
                        ReDim clsBaseLineVectors(-1 To -1)
                        Call GetDocument().NetworkModel.GetConnectBaseLineVectors(objElement, clsBaseLineVectors)
                        Dim i As Long
                        For i = 0 To UBound(clsBaseLineVectors)
                            clsBaseLineVectors(i).IsList = True
                        Next
                    Else
                        objElement.StrPoint.TopParentPoint.IsList = True
                        objElement.EndPoint.TopParentPoint.IsList = True
                    End If
                Next
                'リストの更新。
                Call objListPane.UpdateRowIsList
                 'スクロールを元に戻す。
                objListPane.RowLock = False
    
                'メニューの更新。
                Call clsCtrlValue.Back
                Call objListPane_ChangeSelRow(objListPane.List)
    
                Call clsWaitCursor.Back
    
                Exit Sub
    
            ErrorHandler:
                Call mdlMain.ErrorExit
    
            End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /// <summary>
        /// 編集＞セクション名の変更
        /// 
        /// 'セッションを変更する。
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuEditSession_Click(object sender, EventArgs e)
        {
            try
            {
#if false       // DEBUG 画面レイアウト確認------------------------------------
                MessageBox.Show("mnuEditSession_Click", "エラー発生", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
#endif          // DEBUG 画面レイアウト確認------------------------------------

                /*------------------------------------------------------------------------
                    '選択オブジェクト。
                    Dim objElements As New Collection
                    Dim objElement As Object
                    Dim nPos As Long
                 */
                //'選択オブジェクト。

                object objElement = new object();
                Dictionary<string, object> objElements = new Dictionary<string, object>();
                long nPos;
                long SelectLine = -1;

                //'観測点、基線ベクトルのどちらかのみ選択されているはず。
                nPos = objListPane.StartSelectedAssoc((long)LIST_NUM_PANE.LIST_NUM_OBSPNT);
                while (nPos >= 0)
                {
                    SelectLine = nPos;
                    m_clsMdlMain.PaneSelectedTab = (long)LIST_NUM_PANE.LIST_NUM_OBSPNT;      //PaneのTab（観測点/座標／ベクトル      //2
                    m_clsMdlMain.PaneSelcttedNo = nPos;                                      //Paneの選択行(0～）                    //2
                    objElement = objListPane.GetNextAssoc(ref nPos, (long)LIST_NUM_PANE.LIST_NUM_OBSPNT);
                    objElements.Add(nPos.ToString(), objElement);
                    break;
                }
                nPos = objListPane.StartSelectedAssoc((long)LIST_NUM_PANE.LIST_NUM_VECTOR);
                while (nPos >= 0)
                {
                    SelectLine = nPos;
                    m_clsMdlMain.PaneSelectedTab = (long)LIST_NUM_PANE.LIST_NUM_VECTOR;      //PaneのTab（観測点/座標／ベクトル      //2
                    m_clsMdlMain.PaneSelcttedNo = nPos;                                      //Paneの選択行(0～）                    //2
                    objElement = objListPane.GetNextAssoc(ref nPos, (long)LIST_NUM_PANE.LIST_NUM_VECTOR);
                    objElements.Add(nPos.ToString(), objElement);
                    break;
                }



#if true        // DEBUG -----------------------------------------------------------------------------
                //      <<< 選択されていない場合は、無いが、現在は開発途中の為、入れておく。
                if ( SelectLine == -1)
                {
                    MessageBox.Show("観測点／ベクトルのいずれかを選択して下さい！", "エラー発生", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    return;
                }
#endif          //-------------------------------------------------------------------------------


                //'セッションの変更。
                if (!EditSession(objElements, SelectLine))
                {
                    Cursor = Cursors.Default;
                    return;
                }

                //'砂時計。
                Cursor = Cursors.WaitCursor;

                //'再描画抑制。
                CtrlValue clsCtrlValue = new CtrlValue();
                clsCtrlValue.SetObject(m_clsChangeSelRow, false);

#if false
                'スクロールはしないようにする。
                objListPane.RowLock = True
                '汎用作業キーを利用して、設定が変更された観測点を記憶する。
                Call m_clsDocument.NetworkModel.ClearIsList
                For Each objElement In objElements
                    objElement.IsList = True
                    If(objElement.ObjectType And OBJ_TYPE_OBSERVATIONPOINT) <> 0 Then
                        Dim clsBaseLineVectors() As BaseLineVector
                        ReDim clsBaseLineVectors(-1 To - 1)
                        Call GetDocument().NetworkModel.GetConnectBaseLineVectors(objElement, clsBaseLineVectors)
                        Dim i As Long
                        For i = 0 To UBound(clsBaseLineVectors)
                            clsBaseLineVectors(i).IsList = True
                        Next
                    Else
                        objElement.StrPoint.TopParentPoint.IsList = True
                        objElement.EndPoint.TopParentPoint.IsList = True
                    End If
                Next

#endif

                //'リストの更新。
                //  Call objListPane.UpdateRowIsList
#if true
                objListPane.SelectElement(null);        //2
                objListPane.RemakeList(false);          //2
#endif
                /* 新規追加--------------   //2
                'プロットの再描画。
                Call objPlotPane.UpdateLogicalDrawArea(True)
                Call objPlotPane.Redraw
                Call objPlotPane.Refresh
                [VB] */
                /*[C#]*/
                //objPlotPane.PlotPane_Initialize();
                Bitmap btmp = objPlotPane.Dis_Get_btmp();
                objPlotPane.List_Genba_set(m_List_Genba_S);
                objPlotPane.UpdateLogicalDrawArea(true);
                objPlotPane.Redraw();
                objPlotPane.Refresh();


                //'スクロールを元に戻す。
                //  objListPane.RowLock = False


                //'メニューの更新。
                //  Call clsCtrlValue.Back
                //  Call objListPane_ChangeSelRow(objListPane.List)



                Cursor = Cursors.Default;


            }
            catch (Exception ex)
            {
                //    Call mdlMain.ErrorExit
                MessageBox.Show(ex.Message, "エラー発生", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        //1==========================================================================================
        /*[VB]
            '偏心補正設定を行う。
            Private Sub mnuEditEccentric_Click()

                On Error GoTo ErrorHandler
    
                Dim clsObservationPoint As ObservationPoint
                Set clsObservationPoint = objListPane.SelectedElement(LIST_NUM_OBSPNT)
                If clsObservationPoint.Genuine Then Set clsObservationPoint = clsObservationPoint.CorrectPoint
    
                Dim bEccentric As Boolean
                bEccentric = clsObservationPoint.Eccentric
    
                '偏心補正設定。
                If Not EditEccentric(clsObservationPoint.HeadPoint) Then Exit Sub
    
                '砂時計。
                Dim clsWaitCursor As New WaitCursor
                Set clsWaitCursor.Object = Me
    
                '再描画抑制。
                Dim clsCtrlValue As New CtrlValue
                Call clsCtrlValue.SetObject(m_clsChangeSelRow, False)
    
                If bEccentric Then
                    'スクロールはしないようにする。
                    objListPane.RowLock = True
                    '汎用作業キーを利用して、設定が変更されたオブジェクトを記憶する。
                    Call m_clsDocument.NetworkModel.ClearIsList
                    clsObservationPoint.CorrectPoint.TopParentPoint.IsList = True
                    Call m_clsDocument.NetworkModel.SetConnectBaseLineVectorsIsListEx(clsObservationPoint)
                    'リストの更新。
                    Call objListPane.UpdateRowIsList
                     'スクロールを元に戻す。
                    objListPane.RowLock = False
                Else
                    'リストの作成。
                    Call objListPane.RemakeList(True)
                End If
    
                'プロットの再描画。
                Call objPlotPane.UpdateLogicalDrawArea(False)
                Call objPlotPane.Redraw
    
                'メニューの更新。
                Call clsCtrlValue.Back
                Call objListPane_ChangeSelRow(objListPane.List)
    
                Call clsWaitCursor.Back
    
                Exit Sub
    
            ErrorHandler:
                Call mdlMain.ErrorExit
    
            End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /// <summary>
        ///  編集＞偏心設定
        /// 
        /// '偏心補正設定を行う。
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuEditEccentric_Click(object sender, EventArgs e)
        {
            try
            {
#if true    // DEBUG 画面レイアウト確認------------------------------------
                frmEccentricCorrection frmEccentricCorrection = new frmEccentricCorrection();
                frmEccentricCorrection.ShowDialog();  //編集＞偏心設定
#endif      // DEBUG 画面レイアウト確認------------------------------------




                ObservationPoint clsObservationPoint;
                clsObservationPoint = (ObservationPoint)objListPane.SelectedElement((long)LIST_NUM_PANE.LIST_NUM_OBSPNT);
#if false
                if (clsObservationPoint.Genuine())
                {
                    clsObservationPoint = clsObservationPoint.CorrectPoint();
                }

                bool bEccentric;
                bEccentric = clsObservationPoint.Eccentric();
#endif

                //'偏心補正設定。
                if (!EditEccentric(clsObservationPoint.HeadPoint()))
                {
                    return;
                }


                //'砂時計。
                Cursor = Cursors.WaitCursor;

                //'再描画抑制。
                CtrlValue clsCtrlValue = new CtrlValue();
                clsCtrlValue.SetObject(m_clsChangeSelRow, false);

#if false
                If bEccentric Then
                    'スクロールはしないようにする。
                    objListPane.RowLock = True
                    '汎用作業キーを利用して、設定が変更されたオブジェクトを記憶する。
                    Call m_clsDocument.NetworkModel.ClearIsList
                    clsObservationPoint.CorrectPoint.TopParentPoint.IsList = True
                    Call m_clsDocument.NetworkModel.SetConnectBaseLineVectorsIsListEx(clsObservationPoint)
                    'リストの更新。
                    Call objListPane.UpdateRowIsList
                     'スクロールを元に戻す。
                    objListPane.RowLock = False
                Else
                    'リストの作成。
                    Call objListPane.RemakeList(True)
                End If


                'プロットの再描画。
                Call objPlotPane.UpdateLogicalDrawArea(False)
                Call objPlotPane.Redraw


                'メニューの更新。
                Call clsCtrlValue.Back
                Call objListPane_ChangeSelRow(objListPane.List)
#endif

                Cursor = Cursors.Default;

            }
            catch (Exception ex)
            {
                //    Call mdlMain.ErrorExit
                MessageBox.Show(ex.Message, "エラー発生", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //1==========================================================================================
        /*[VB]
            '観測点を結合する。
            Private Sub mnuEditCombination_Click()

                On Error GoTo ErrorHandler
    
                '観測点の結合。
                If Not CombinationObservationPoint() Then Exit Sub
    
                '砂時計。
                Dim clsWaitCursor As New WaitCursor
                Set clsWaitCursor.Object = Me
    
                '再描画抑制。
                Dim clsCtrlValue As New CtrlValue
                Call clsCtrlValue.SetObject(m_clsChangeSelRow, False)
    
                'リストの作成。本点が削除される可能性がある。
                Call objListPane.RemakeList(True)
    
                'プロットの再描画。
                Call objPlotPane.UpdateLogicalDrawArea(False)
                Call objPlotPane.Redraw
    
                'メニューの更新。
                Call clsCtrlValue.Back
                Call objListPane_ChangeSelRow(objListPane.List)
    
                Call clsWaitCursor.Back
    
                Exit Sub
    
            ErrorHandler:
                Call mdlMain.ErrorExit
    
            End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //編集＞結合
        private void mnuEditCombination_Click(object sender, EventArgs e)
        {
#if true    // DEBUG 画面レイアウト確認------------------------------------
            frmCombination frmCombination = new frmCombination();
            frmCombination.ShowDialog();  //編集＞偏心設定
#endif      // DEBUG 画面レイアウト確認------------------------------------

        }
        //1==========================================================================================
        /*[VB]
            '観測点を分離する。
            Private Sub mnuEditSeparation_Click()

                On Error GoTo ErrorHandler
    
                Exit Sub
    
            ErrorHandler:
                Call mdlMain.ErrorExit

            End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //編集＞分離
        private void mnuEditSeparation_Click(object sender, EventArgs e)
        {

        }
        //1==========================================================================================
        /*[VB]
            '観測点または基線ベクトルの有効/無効を設定する。
            Private Sub mnuEditValidOn_Click()

                On Error GoTo ErrorHandler
    
                Call EditValid(True)
    
                Exit Sub
    
            ErrorHandler:
                Call mdlMain.ErrorExit

            End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]  //2
        //  編集＞有効
        /// <summary>
        /// 観測点または基線ベクトルの有効/無効を設定する。
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuEditValidOn_Click(object sender, EventArgs e)   //2
        {
            try
            {
                EditValid(true);
            }
            catch (Exception ex)
            {
                _ = MessageBox.Show(ex.Message, "エラー発生", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //1==========================================================================================
        /*[VB]
            '観測点または基線ベクトルの有効/無効を設定する。
            Private Sub mnuEditValidOff_Click()

                On Error GoTo ErrorHandler
    
                Call EditValid(False)
    
                Exit Sub
    
            ErrorHandler:
                Call mdlMain.ErrorExit

            End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //編集＞無効
        //[C#]  //2
        /// <summary>
        /// 観測点または基線ベクトルの有効/無効を設定する。
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuEditValidOff_Click(object sender, EventArgs e)
        {
            try
            {
                EditValid(false);
            }
            catch (Exception ex)
            {
                _ = MessageBox.Show(ex.Message, "エラー発生", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        //2==========================================================================================
        /*[VB]
            '観測点または基線ベクトルの有効/無効を設定する。
            '
            '引き数：
            'bEnable 有効フラグ。
            Private Sub EditValid(ByVal bEnable As Boolean)

                '砂時計。
                Dim clsWaitCursor As New WaitCursor
                Set clsWaitCursor.Object = Me
    
                '再描画抑制。
                Dim clsCtrlValue As New CtrlValue
                Call clsCtrlValue.SetObject(m_clsChangeSelRow, False)
    
                '汎用作業キーを利用して、設定が変更された観測点を記憶する。
                Call m_clsDocument.NetworkModel.ClearIsList
    
                '設定。
                Dim objElement As Object
                Dim nPos As Long
                nPos = objListPane.StartSelectedAssoc(LIST_NUM_OBSPNT)
                Do While (nPos > 0)
                    Set objElement = objListPane.GetNextAssoc(nPos)
                    If Not objElement.Genuine Then Call m_clsDocument.EnableObservationPoint(objElement, bEnable)
                Loop
                nPos = objListPane.StartSelectedAssoc(LIST_NUM_VECTOR)
                Do While (nPos > 0)
                    Set objElement = objListPane.GetNextAssoc(nPos)
                    Call m_clsDocument.EnableBaseLineVector(objElement, bEnable)
                Loop
    
                'スクロールはしないようにする。
                objListPane.RowLock = True
                'リストの更新。
                Call objListPane.UpdateRowIsList
                'スクロールを元に戻す。
                objListPane.RowLock = False
    
                'プロットの再描画。
                Call objPlotPane.UpdateLogicalDrawArea(False)
                Call objPlotPane.Redraw
    
                'メニューの更新。
                Call clsCtrlValue.Back
                Call objListPane_ChangeSelRow(objListPane.List)
    
                Call clsWaitCursor.Back
    
            End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#] 2
        private void EditValid(bool bEnable)
        {

#if false   // DEBUG 画面レイアウト確認------------------------------------
            _ = bEnable
                ? MessageBox.Show("有効", "DEBUG", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
                : MessageBox.Show("無効", "DEBUG", MessageBoxButtons.OK, MessageBoxIcon.Information);
#endif      // DEBUG 画面レイアウト確認------------------------------------


            //'砂時計。
            Cursor = Cursors.WaitCursor;


            ObservationPoint clsObservationPoint;
            clsObservationPoint = (ObservationPoint)objListPane.SelectedElement((long)LIST_NUM_PANE.LIST_NUM_OBSPNT);



            //'再描画抑制。
            /*
                Dim clsCtrlValue As New CtrlValue
                Call clsCtrlValue.SetObject(m_clsChangeSelRow, False)
             */
            CtrlValue clsCtrlValue = new CtrlValue();
            clsCtrlValue.SetObject(m_clsChangeSelRow, false);


            //'汎用作業キーを利用して、設定が変更された観測点を記憶する。
            /*
                Call m_clsDocument.NetworkModel.ClearIsList
             */


            /*---------------------------------------------------------------------------
                //'設定。
                m_clsDocument.SetAttributeCommon(clsObservationPoint, frmAttributeCommon.PointNumber, frmAttributeCommon.PointName, frmAttributeCommon.Fixed, frmAttributeCommon.CoordinateFixed());


                Dim objElement As Object
                Dim nPos As Long
                nPos = objListPane.StartSelectedAssoc(LIST_NUM_OBSPNT)
                Do While(nPos > 0)
                    Set objElement = objListPane.GetNextAssoc(nPos)
                    If Not objElement.Genuine Then Call m_clsDocument.EnableObservationPoint(objElement, bEnable)
                Loop
                nPos = objListPane.StartSelectedAssoc(LIST_NUM_VECTOR)
                Do While(nPos > 0)
                    Set objElement = objListPane.GetNextAssoc(nPos)
                    Call m_clsDocument.EnableBaseLineVector(objElement, bEnable)
                Loop
             ----------------------------------------------------------------------------*/
            //'設定。
            object objElement = new object();
            Dictionary<string, object> objElements = new Dictionary<string, object>();
            long nPos;
            long SelectLine = -1;

            //観測点、基線ベクトルのどちらかのみ選択されているはず。
            nPos = objListPane.StartSelectedAssoc((long)LIST_NUM_PANE.LIST_NUM_OBSPNT);
            while (nPos >= 0)
            {
                SelectLine = nPos;
                m_clsMdlMain.PaneSelectedTab = (long)LIST_NUM_PANE.LIST_NUM_OBSPNT;      //PaneのTab（観測点/座標／ベクトル      //2
                m_clsMdlMain.PaneSelcttedNo = nPos;                                      //Paneの選択行(0～）                    //2
                objElement = objListPane.GetNextAssoc(ref nPos, (long)LIST_NUM_PANE.LIST_NUM_OBSPNT);
                objElements.Add(nPos.ToString(), objElement);
                //観測点(有効／無効）
                m_clsDocument.EnableObservationPoint((ObservationPoint)objElement, bEnable);

                break;
            }
            nPos = objListPane.StartSelectedAssoc((long)LIST_NUM_PANE.LIST_NUM_VECTOR);
            while (nPos >= 0)
            {
                SelectLine = nPos;
                m_clsMdlMain.PaneSelectedTab = (long)LIST_NUM_PANE.LIST_NUM_VECTOR;      //PaneのTab（観測点/座標／ベクトル      //2
                m_clsMdlMain.PaneSelcttedNo = nPos;                                      //Paneの選択行(0～）                    //2
                objElement = objListPane.GetNextAssoc(ref nPos, (long)LIST_NUM_PANE.LIST_NUM_VECTOR);
                objElements.Add(nPos.ToString(), objElement);
                //基線ベクトル(有効／無効）
                m_clsDocument.EnableBaseLineVector((BaseLineVector)objElement, bEnable);

                break;
            }


            //'スクロールはしないようにする。
            /*
                objListPane.RowLock = True
             */


            //'リストの更新。
            /*
                Call objListPane.UpdateRowIsList
             */
#if true
            objListPane.SelectElement(null);
            objListPane.RemakeList(false);
#endif


            //'スクロールを元に戻す。
            /*
                objListPane.RowLock = False
             */


            //'プロットの再描画。
            /*
                Call objPlotPane.UpdateLogicalDrawArea(False)
                Call objPlotPane.Redraw
             */
            //objPlotPane.PlotPane_Initialize();
            Bitmap btmp = objPlotPane.Dis_Get_btmp();
            objPlotPane.List_Genba_set(m_List_Genba_S);
            objPlotPane.UpdateLogicalDrawArea(true);
            objPlotPane.Redraw();
            objPlotPane.Refresh();


            //'メニューの更新。
            /*
                Call clsCtrlValue.Back
                Call objListPane_ChangeSelRow(objListPane.List)
             */

            Cursor = Cursors.Default;

        }




        //1==========================================================================================
        /*[VB]
            'オブジェクトに採用を設定する。
            Private Sub mnuEditAdopt_Click()

                On Error GoTo ErrorHandler
    
                If 0 < objListPane.StartSelectedAssoc(LIST_NUM_VECTOR) Then
                    Call EditLineType(OBJ_MODE_ADOPT)
                Else
                    Call EditObsPntMode(OBJ_MODE_ADOPT)
                End If
    
                Exit Sub
    
            ErrorHandler:
                Call mdlMain.ErrorExit

            End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //編集＞採用
        private void mnuEditAdopt_Click(object sender, EventArgs e)
        {

        }
        //1==========================================================================================
        /*[VB]
            'オブジェクトに点検を設定する。
            Private Sub mnuEditCheck_Click()

                On Error GoTo ErrorHandler
    
                If 0 < objListPane.StartSelectedAssoc(LIST_NUM_VECTOR) Then
                    Call EditLineType(OBJ_MODE_CHECK)
                Else
                    Call EditObsPntMode(OBJ_MODE_CHECK)
                End If
    
                Exit Sub
    
            ErrorHandler:
                Call mdlMain.ErrorExit

            End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //編集＞点検
        private void mnuEditCheck_Click(object sender, EventArgs e)
        {

        }
        //1==========================================================================================
        /*[VB]
            'オブジェクトに重複を設定する。
            Private Sub mnuEditDuplicate_Click()

                On Error GoTo ErrorHandler
    
                If 0 < objListPane.StartSelectedAssoc(LIST_NUM_VECTOR) Then
                    Call EditLineType(OBJ_MODE_DUPLICATE)
                Else
                    Call EditObsPntMode(OBJ_MODE_DUPLICATE)
                End If
    
                Exit Sub
    
            ErrorHandler:
                Call mdlMain.ErrorExit

            End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //編集＞重複
        private void mnuEditDuplicate_Click(object sender, EventArgs e)
        {

        }
        //1==========================================================================================
        /*[VB]
            '2023/06/26 Hitz H.Nakamura **************************************************
            'GNSS水準測量対応。
            '前半後半較差の追加。
            'オブジェクトに前半を設定する。
            Private Sub mnuEditHalfFst_Click()

                On Error GoTo ErrorHandler
    
                If 0 < objListPane.StartSelectedAssoc(LIST_NUM_VECTOR) Then
                    Call EditLineType(OBJ_MODE_HALF_FST)
                Else
                End If
    
                Exit Sub
    
            ErrorHandler:
                Call mdlMain.ErrorExit

            End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //編集＞前半
        private void mnuEditHalfFst_Click(object sender, EventArgs e)
        {

        }
        //1==========================================================================================
        /*[VB]
            'オブジェクトに後半を設定する。
            Private Sub mnuEditHalfLst_Click()

                On Error GoTo ErrorHandler
    
                If 0 < objListPane.StartSelectedAssoc(LIST_NUM_VECTOR) Then
                    Call EditLineType(OBJ_MODE_HALF_LST)
                Else
                End If
    
                Exit Sub
    
            ErrorHandler:
                Call mdlMain.ErrorExit

            End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //編集＞後半
        private void mnuEditHalfLst_Click(object sender, EventArgs e)
        {

        }
        //1==========================================================================================
        /*[VB]
            '基線ベクトルの向きを反転する。
            Private Sub mnuEditReverse_Click()

                On Error GoTo ErrorHandler
    
                '砂時計。
                Dim clsWaitCursor As New WaitCursor
                Set clsWaitCursor.Object = Me
    
                '再描画抑制。
                Dim clsCtrlValue As New CtrlValue
                Call clsCtrlValue.SetObject(m_clsChangeSelRow, False)
    
                '同じ向きの重複する基線ベクトルも一緒に反転させる。
                Dim nPos As Long
                nPos = objListPane.StartSelectedAssoc(LIST_NUM_VECTOR)
                Dim objBaseLineVectors As New Collection
                Do While (nPos > 0)
                    Dim clsBaseLineVector As BaseLineVector
                    Set clsBaseLineVector = objListPane.GetNextAssoc(nPos)
                    Call SetAtCollectionObject(objBaseLineVectors, clsBaseLineVector, GetPointer(clsBaseLineVector))
                    Dim clsDuplicationBaseLineVectors() As BaseLineVector
                    clsDuplicationBaseLineVectors = m_clsDocument.NetworkModel.GetDuplicationBaseLineVectors(clsBaseLineVector)
                    Dim i As Long
                    For i = 0 To UBound(clsDuplicationBaseLineVectors)
                        If clsBaseLineVector.StrPoint.HeadPoint Is clsDuplicationBaseLineVectors(i).StrPoint.HeadPoint Then Call SetAtCollectionObject(objBaseLineVectors, clsDuplicationBaseLineVectors(i), GetPointer(clsDuplicationBaseLineVectors(i)))
                    Next
                Loop
    
                '解析結果削除確認。
                For Each clsBaseLineVector In objBaseLineVectors
                    If clsBaseLineVector.Analysis < ANALYSIS_STATUS_FAILED Then
                        If MsgBox("解析済みの基線ベクトルが含まれています。反転を行いますと解析の結果が失われます。よろしいですか?", vbExclamation Or vbOKCancel) = vbOK Then
                            Exit For
                        Else
                            Exit Sub
                        End If
                    End If
                Next
    
                '汎用作業キーを利用して、設定が変更された観測点を記憶する。
                Call m_clsDocument.NetworkModel.ClearIsList
    
                '反転。
                For Each clsBaseLineVector In objBaseLineVectors
                    Call m_clsDocument.ReplaceBaseLineVector(clsBaseLineVector)
                    clsBaseLineVector.IsList = True
                    '偏心補正の再計算の有無にかかわらず更新する。方位票の評価にはコストがかかる。
                    Call m_clsDocument.NetworkModel.SetConnectBaseLineVectorsIsListEx(clsBaseLineVector.StrPoint)
                    Call m_clsDocument.NetworkModel.SetConnectBaseLineVectorsIsListEx(clsBaseLineVector.EndPoint)
                Next
    
                'スクロールはしないようにする。
                objListPane.RowLock = True
                'リストの更新。
                Call objListPane.UpdateRowIsList
                'スクロールを元に戻す。
                objListPane.RowLock = False
    
                'プロットの再描画。
                Call objPlotPane.UpdateLogicalDrawArea(False)
                Call objPlotPane.Redraw
    
                'メニューの更新。
                Call clsCtrlValue.Back
                Call objListPane_ChangeSelRow(objListPane.List)
    
                Call clsWaitCursor.Back
    
                Exit Sub
    
            ErrorHandler:
                Call mdlMain.ErrorExit

            End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //編集＞反転
        private void mnuEditReverse_Click(object sender, EventArgs e)
        {

        }
        //1==========================================================================================
        /*[VB]
            '観測点を削除する。
            Private Sub mnuEditRemove_Click()

                On Error GoTo ErrorHandler
    
                If MsgBox("選択されている観測点、または偏心補正後の本点を削除します。", vbOKCancel Or vbExclamation) <> vbOK Then Exit Sub
    
                '砂時計。
                Dim clsWaitCursor As New WaitCursor
                Set clsWaitCursor.Object = Me
    
                '再描画抑制。
                Dim clsCtrlValue As New CtrlValue
                Call clsCtrlValue.SetObject(m_clsChangeSelRow, False)
    
                '選択オブジェクト。
                Dim objElements As New Collection
                Dim objElement As Object
                Dim nPos As Long
                nPos = objListPane.StartSelectedAssoc(LIST_NUM_OBSPNT)
                Do While (nPos > 0)
                    Set objElement = objListPane.GetNextAssoc(nPos)
                    Call objElements.Add(objElement, Hex$(GetPointer(objElement)))
                Loop
    
                '削除。
                Call m_clsDocument.RemoveObservationPoint(objElements)
    
                'リストの作成。
                Call objListPane.RemakeList(True)
    
                'プロットの再描画。
                Call objPlotPane.UpdateLogicalDrawArea(False)
                Call objPlotPane.Redraw
    
                'メニューの更新。
                Call clsCtrlValue.Back
                Call objListPane_ChangeSelRow(objListPane.List)
    
                Call clsWaitCursor.Back
    
                Exit Sub
    
            ErrorHandler:
                Call mdlMain.ErrorExit

            End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //編集＞削除
        private void mnuEditRemove_Click(object sender, EventArgs e)
        {

        }
        //1==========================================================================================
        /*[VB]
            '基線ベクトルの向きの自動整列。
            Private Sub mnuEditAutoOrder_Click()

                On Error GoTo ErrorHandler
    
                '基線ベクトルの向きの自動整列。
                If Not AutoOrderVector() Then Exit Sub
    
                '砂時計。
                Dim clsWaitCursor As New WaitCursor
                Set clsWaitCursor.Object = Me
    
                'スクロールはしないようにする。
                objListPane.RowLock = True
                'リストの更新。
                Call objListPane.UpdateAllRow(LIST_NUM_VECTOR)
                'スクロールを元に戻す。
                objListPane.RowLock = False
    
                'プロットの再描画。
                Call objPlotPane.UpdateLogicalDrawArea(False)
                Call objPlotPane.Redraw
                Call objPlotPane.Refresh
    
                Call clsWaitCursor.Back
    
                Exit Sub
    
            ErrorHandler:
                Call mdlMain.ErrorExit
    
            End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //編集＞基線ベクトルの向きの自動整列(&O)...
        private void mnuEditAutoOrder_Click(object sender, EventArgs e)
        {

        }

        //1==========================================================================================
        //1==========================================================================================
        //1==========================================================================================

        //1==========================================================================================
        /*[VB]
            '観測点の共通属性の編集を行なう。
            '
            '引き数：
            'clsObservationPoint 対象とする観測点(代表観測点)。
            '
            '戻り値：
            '正常終了の場合 True を返す。
            'キャンセルの場合 False を返す。
            Private Function EditAttributeCommon(ByVal clsObservationPoint As ObservationPoint) As Boolean

                EditAttributeCommon = False
    
                '既存の値。
                frmAttributeCommon.PointNumber = clsObservationPoint.Number
                frmAttributeCommon.PointName = clsObservationPoint.Name
                frmAttributeCommon.Fixed = clsObservationPoint.Fixed
                frmAttributeCommon.CoordinateDisplay = clsObservationPoint.CoordinateDisplay
                frmAttributeCommon.CoordinateFixed = clsObservationPoint.CoordinateFixed
                frmAttributeCommon.OldEditCode = clsObservationPoint.Attributes.Common.OldEditCode
    
                '編集ダイアログ。
                Call frmAttributeCommon.Show(1)
                If frmAttributeCommon.Result <> vbOK Then Exit Function
    
                '再描画。
                If RedrawWindow(Me.hWnd, 0, 0, RDW_UPDATENOW) = 0 Then Call Err.Raise(ERR_FATAL, , GetLastErrorMessage())
    
                '砂時計。
                Dim clsWaitCursor As New WaitCursor
                Set clsWaitCursor.Object = Me
    
                '設定。
                Call m_clsDocument.SetAttributeCommon(clsObservationPoint, frmAttributeCommon.PointNumber, frmAttributeCommon.PointName, frmAttributeCommon.Fixed, frmAttributeCommon.CoordinateFixed)
    
                Call clsWaitCursor.Back
    
                EditAttributeCommon = True
    
            End Function
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /// <summary>
        /// 観測点の共通属性の編集を行なう。    メニュー＞現場を選択＞
        /// '
        /// 引き数：
        /// clsObservationPoint 対象とする観測点(代表観測点)。
        /// 
        /// </summary>
        /// <param name="clsObservationPoint"></param>
        /// <returns>
        /// 戻り値：
        /// 正常終了の場合 True を返す。
        /// キャンセルの場合 False を返す。
        /// </returns>
        private bool EditAttributeCommon(ObservationPoint clsObservationPoint)
        {
            bool EditAttributeCommon = false;


            frmAttributeCommon frmAttributeCommon = new frmAttributeCommon(m_clsMdlMain);


            //'既存の値。
            frmAttributeCommon.PointNumber = clsObservationPoint.Number();                  //2
            frmAttributeCommon.PointName = clsObservationPoint.Name();                      //2
            frmAttributeCommon.Fixed = clsObservationPoint.Fixed();
            frmAttributeCommon.CoordinateDisplay(clsObservationPoint.CoordinateDisplay());  //2
            frmAttributeCommon.CoordinateFixed(clsObservationPoint.CoordinateFixed());
            frmAttributeCommon.OldEditCode = (int)clsObservationPoint.Attributes().Common().OldEditCode;    //2


            //'編集ダイアログ。
            _ = frmAttributeCommon.ShowDialog();
            if (frmAttributeCommon.Result != vbOK)
            {
                return EditAttributeCommon;
            }

            //'再描画。
            if (RedrawWindow(Handle, IntPtr.Zero, IntPtr.Zero, (int)RDW_UPDATENOW) == false)
            {
                //Then Call Err.Raise(ERR_FATAL, , GetLastErrorMessage())
                return EditAttributeCommon;
            }


            //'砂時計。
            Cursor = Cursors.WaitCursor;


            //'設定。
            m_clsDocument.SetAttributeCommon(clsObservationPoint, frmAttributeCommon.PointNumber, frmAttributeCommon.PointName, frmAttributeCommon.Fixed, frmAttributeCommon.CoordinateFixed());

            Cursor = Cursors.Default;


            EditAttributeCommon = true;
            return EditAttributeCommon;

        }

        //1==========================================================================================
        /*[VB]
            '観測点の属性の編集を行なう。
            '
            '引き数：
            'clsObservationPoint 対象とする観測点(代表観測点)。
            'bAnalysis 解析済フラグ。True=解析済。False=未解析。
            '
            '戻り値：
            '正常終了の場合 True を返す。
            'キャンセルの場合 False を返す。
            Private Function EditAttribute(ByVal clsObservationPoint As ObservationPoint, ByVal bAnalysis As Boolean) As Boolean

                EditAttribute = False
    
                '既存の値。
                frmAttribute.Session = clsObservationPoint.Session
                frmAttribute.RecType = clsObservationPoint.RecType
                frmAttribute.RecNumber = clsObservationPoint.RecNumber
                frmAttribute.AntType = clsObservationPoint.AntType
                frmAttribute.AntNumber = clsObservationPoint.AntNumber
                frmAttribute.AntMeasurement = clsObservationPoint.AntMeasurement
                frmAttribute.AntHeight = clsObservationPoint.AntHeight
                frmAttribute.Analysis = bAnalysis
                frmAttribute.ElevationMask = clsObservationPoint.ElevationMask
                frmAttribute.ElevationMaskHand = clsObservationPoint.Attributes.ElevationMaskHand
                frmAttribute.NumberOfMinSV = IIf(m_clsDocument.NumberOfMinSV > 0, m_clsDocument.NumberOfMinSV, clsObservationPoint.NumberOfMinSV)
                frmAttribute.NumberOfMinSVHand = clsObservationPoint.Attributes.NumberOfMinSVHand
                Set frmAttribute.ObservationPoint = clsObservationPoint
    
                '編集ダイアログ。
                Call frmAttribute.Show(1)
                If frmAttribute.Result<> vbOK Then Exit Function
    
                '再描画。
                If RedrawWindow(Me.hWnd, 0, 0, RDW_UPDATENOW) = 0 Then Call Err.Raise(ERR_FATAL, , GetLastErrorMessage())
    
                '砂時計。
                Dim clsWaitCursor As New WaitCursor
                Set clsWaitCursor.Object = Me
    
                '設定。
                Dim objElements As New Collection
                Call objElements.Add(clsObservationPoint)
                Call m_clsDocument.SetSession(objElements, frmAttribute.Session)
                If frmAttribute.Extend Then Call m_clsDocument.SetSessionExtend(objElements, frmAttribute.Session)
                Call m_clsDocument.SetAttribute(clsObservationPoint, frmAttribute.RecType, frmAttribute.RecNumber, frmAttribute.AntType, frmAttribute.AntNumber, frmAttribute.AntMeasurement, frmAttribute.AntHeight, frmAttribute.ElevationMaskHand, frmAttribute.NumberOfMinSVHand)


                Call clsWaitCursor.Back


                EditAttribute = True


            End Function
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        private bool EditAttribute(ObservationPoint clsObservationPoint, bool bAnalysis)
        {
            bool EditAttribute = false;

            frmAttribute frmAttribute = new frmAttribute(m_clsMdlMain);

            //'既存の値。
            frmAttribute.Session = clsObservationPoint.Session();
            frmAttribute.RecType = clsObservationPoint.RecType();
            frmAttribute.RecNumber = clsObservationPoint.RecNumber();
            frmAttribute.AntType = clsObservationPoint.AntType();
            frmAttribute.AntNumber = clsObservationPoint.AntNumber();
            frmAttribute.AntMeasurement = clsObservationPoint.AntMeasurement();
            frmAttribute.AntHeight = clsObservationPoint.AntHeight();
            frmAttribute.Analysis = bAnalysis;
            frmAttribute.ElevationMask = (long)clsObservationPoint.ElevationMask();
            frmAttribute.ElevationMaskHand = clsObservationPoint.Attributes().ElevationMaskHand;

            if (m_clsDocument.NumberOfMinSV > 0)
            {
                frmAttribute.NumberOfMinSV = m_clsDocument.NumberOfMinSV;

            } else {
                frmAttribute.NumberOfMinSV = clsObservationPoint.NumberOfMinSV();
            }

            frmAttribute.NumberOfMinSVHand = clsObservationPoint.Attributes().NumberOfMinSVHand;
            frmAttribute.ObservationPoint(clsObservationPoint);

            //'編集ダイアログ。
            frmAttribute.ShowDialog();
            if (frmAttribute.Result != vbOK)
            {
                return EditAttribute;
            }


            //'再描画。
            if (RedrawWindow(Handle, IntPtr.Zero, IntPtr.Zero, (int)RDW_UPDATENOW) == false)
            {
                //Then Call Err.Raise(ERR_FATAL, , GetLastErrorMessage())
                return EditAttribute;
            }


            //'砂時計。
            Cursor = Cursors.WaitCursor;

#if false
            //'設定。
            Dim objElements As New Collection
            Call objElements.Add(clsObservationPoint)
            Call m_clsDocument.SetSession(objElements, frmAttribute.Session)
            If frmAttribute.Extend Then Call m_clsDocument.SetSessionExtend(objElements, frmAttribute.Session)
            Call m_clsDocument.SetAttribute(clsObservationPoint, frmAttribute.RecType, frmAttribute.RecNumber, frmAttribute.AntType, frmAttribute.AntNumber, frmAttribute.AntMeasurement, frmAttribute.AntHeight, frmAttribute.ElevationMaskHand, frmAttribute.NumberOfMinSVHand)

#endif

            Cursor = Cursors.Default;



            EditAttribute = true;
            return EditAttribute;
        }


        //1==========================================================================================
        /*[VB]
            'セッションの変更を行なう。
            '
            'セッション名を変更する。
            '
            '引き数：
            'objElements 対象とするオブジェクト。要素はオブジェクト。キーは任意。
            '
            '戻り値：
            '正常終了の場合 True を返す。
            'キャンセルの場合 False を返す。
            Private Function EditSession(ByVal objElements As Collection) As Boolean

                EditSession = False
    
                '既存の値。
                Set frmSession.Elements = objElements
    
                'セッションの変更ダイアログ。
                Call frmSession.Show(1)
                If frmSession.Result<> vbOK Then Exit Function
    
                '再描画。
                If RedrawWindow(Me.hWnd, 0, 0, RDW_UPDATENOW) = 0 Then Call Err.Raise(ERR_FATAL, , GetLastErrorMessage())
    
                '砂時計。
                Dim clsWaitCursor As New WaitCursor
                Set clsWaitCursor.Object = Me
    
                '設定。
                Call m_clsDocument.SetSession(objElements, frmSession.Session)
                If frmSession.Extend Then Call m_clsDocument.SetSessionExtend(objElements, frmSession.Session)

                Call clsWaitCursor.Back

                EditSession = True


            End Function
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]  //2
        /// <summary>
        /// セッションの変更を行なう。
        /// '
        /// セッション名を変更する。
        /// '
        /// 引き数：
        /// objElements 対象とするオブジェクト。要素はオブジェクト。キーは任意。
        /// 
        /// </summary>
        /// <param name="objElements"></param>
        /// <param name="nPos"></param>
        /// <returns>
        /// 戻り値：
        /// 正常終了の場合 True を返す。
        /// キャンセルの場合 False を返す。
        /// </returns>
        private bool EditSession(Dictionary<string, object> objElements, long nPos) //2
        {
            bool EditSession = false;

            frmSession frmSession = new frmSession(m_clsMdlMain);


            //'既存の値。
            frmSession.Elements(objElements);
            frmSession.SelectLine = nPos;



            //'セッションの変更ダイアログ。
            _ = frmSession.ShowDialog();
            if (frmSession.Result != vbOK)
            {
                return EditSession;
            }

            //'再描画。
            if (RedrawWindow(Handle, IntPtr.Zero, IntPtr.Zero, (int)RDW_UPDATENOW) == false)
            {
                //Then Call Err.Raise(ERR_FATAL, , GetLastErrorMessage())
                _ = MessageBox.Show(GetLastErrorMessage(), "エラー発生", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return EditSession;
            }

            //'砂時計。
            Cursor = Cursors.WaitCursor;

            //'設定。
            m_clsDocument.SetSession(objElements, frmSession.Session);      //Ex.)objElements>item1 = S001(元) --->　(変)frmSession.Session = 134F      

            if (frmSession.Extend)
            {
                m_clsDocument.SetSessionExtend(objElements, frmSession.Session);
            }
            EditSession = true;
            return EditSession;

        }


        //1==========================================================================================
        /*[VB]
            '偏心補正設定を行なう。
            '
            '引き数：
            'clsObservationPoint 対象とする観測点(HeadPoint)。
            '
            '戻り値：
            '正常終了の場合 True を返す。
            'キャンセルの場合 False を返す。
            Private Function EditEccentric(ByVal clsObservationPoint As ObservationPoint) As Boolean

                EditEccentric = False
    
                '既存の値。
                Let frmEccentricCorrection.GenuineNumber = clsObservationPoint.GenuineNumber
                Let frmEccentricCorrection.GenuineName = clsObservationPoint.GenuineName
                Let frmEccentricCorrection.EccentricCorrectionParam = clsObservationPoint.EccentricCorrectionParam
                Set frmEccentricCorrection.ObservationPoint = clsObservationPoint
    
                '偏心設定ダイアログ。
                Call frmEccentricCorrection.Show(1)
                If frmEccentricCorrection.Result<> vbOK Then Exit Function
    
                '再描画。
                If RedrawWindow(Me.hWnd, 0, 0, RDW_UPDATENOW) = 0 Then Call Err.Raise(ERR_FATAL, , GetLastErrorMessage())
    
                '偏心補正。
                Call m_clsDocument.CorrectEccentric(clsObservationPoint, frmEccentricCorrection.EccentricCorrectionParam, frmEccentricCorrection.GenuineNumber, frmEccentricCorrection.GenuineName)
    
                '再描画。
                If RedrawWindow(Me.hWnd, 0, 0, RDW_UPDATENOW) = 0 Then Call Err.Raise(ERR_FATAL, , GetLastErrorMessage())
    
                EditEccentric = True

            End Function
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        private bool EditEccentric(ObservationPoint clsObservationPoint)
        {

            bool EditEccentric = false;

            frmEccentricCorrection frmEccentricCorrection = new frmEccentricCorrection();


#if false
            '既存の値。
            Let frmEccentricCorrection.GenuineNumber = clsObservationPoint.GenuineNumber
            Let frmEccentricCorrection.GenuineName = clsObservationPoint.GenuineName
            Let frmEccentricCorrection.EccentricCorrectionParam = clsObservationPoint.EccentricCorrectionParam
            Set frmEccentricCorrection.ObservationPoint = clsObservationPoint
#endif

            //'偏心設定ダイアログ。
            _ = frmEccentricCorrection.ShowDialog();
            if (frmEccentricCorrection.Result != vbOK)
            {
                return EditEccentric;
            }

            //'再描画。
            if (RedrawWindow(Handle, IntPtr.Zero, IntPtr.Zero, (int)RDW_UPDATENOW) == false)
            {
                //Then Call Err.Raise(ERR_FATAL, , GetLastErrorMessage())
                return EditEccentric;
            }


            //'偏心補正。
            //Call m_clsDocument.CorrectEccentric(clsObservationPoint, frmEccentricCorrection.EccentricCorrectionParam, frmEccentricCorrection.GenuineNumber, frmEccentricCorrection.GenuineName)


            //'再描画。
            if (RedrawWindow(Handle, IntPtr.Zero, IntPtr.Zero, (int)RDW_UPDATENOW) == false)
            {
                //Then Call Err.Raise(ERR_FATAL, , GetLastErrorMessage())
                return EditEccentric;
            }


            EditEccentric = true;
            return EditEccentric;

        }





        //==========================================================================================
    }
}
