//  using System.ComponentModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using static SurvLine.mdl.MdlNSDefine;
using static SurvLine.mdl.DEFINE;
using static SurvLine.mdl.MdlUtility;
using static SurvLine.frmMain;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.AxHost;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static SurvLine.mdl.MdlPlotProc;
using static SurvLine.mdl.MdlMain;
using static SurvLine.RegistAngle;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;
using System.Collections.ObjectModel;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.Remoting;
using SurvLine.mdl;

namespace SurvLine
{

   public partial class PlotPane : UserControl
    {

        public PlotPane()
        {
            InitializeComponent();
        }

        public void PlotPane_Initialize()
        {
            return;
        }

        private void PlotPane_Load(object sender, EventArgs e)
        {
            UserControl_Initialize();

            m_clsPlotPort.Class_Initialize();

            m_btmp = new Bitmap(picView.Width, picView.Height);

            UserControl_Resize();

            return;
        }


        //==========================================================================================
        /*[VB]
        '*******************************************************************************
        'プロットペイン
        '
        '平面直角座標→実座標、ｍ。
        '論理エリア　→プロジェクト全体の範囲、ピクセル。左上が(0,0)。
        'デバイス　　→デバイスコンテキストの範囲、ピクセル。論理エリア内の実際に描画する範囲。
        'ビュー　　　→ビューの範囲、ピクセル。論理エリア内で画面や印刷で実際に見える範囲。
        '
        '位置(座標)は基本的に論理エリア座標系である。単位はピクセル。
        '
        'picView がフロントバッファとなる。これがビューと呼ばれ、実際に画面に表示される。
        'picMemDC がバックバッファとなる。これがデバイスと呼ばれ、表示されないが描画が行われる。
        '基本は picMemDC に描画して、表示する時に picView にコピーする。
        '強調オブジェクト等の変化の多い部分は表示する直前に picView へじかに描画する。
        '
        '強調オブジェクトとは、選択されているオブジェクトや閉合の登録済、候補オブジェクトなど、強調して表示されるオブジェクトである。
        '強調オブジェクトは m_clsEmphasisHead のリストに保持される。

        Option Explicit
        [VB]*/
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '定数
        Private Const BACKGROUND_COLOR As Long = &H0& '背景色。

        Private Const NORTHARROW_COLOR_LINE As Long = &HCCCCCC 'ノースアロー線色。
        Private Const NORTHARROW_COLOR_SYMBOL As Long = &HFF& 'ノースアロー記号色。
        Private Const NORTHARROW_WIDTH_LINE As Long = 2 'ノースアロー線幅(ピクセル)。

        Private Const PLOT_SCALEBAR_COLOR_SYMBOL As Long = &H66FF66 'スケールバー記号色。

        Private Const SMALL_CHANGE As Long = 32 'スクロール量。

        Private Enum SCALE_STATUS 'スケール状態。
            SCALE_STATUS_NONE = 0 'なし。
            SCALE_STATUS_BEGIN '開始済。
            SCALE_STATUS_RANGE '範囲指定中。
            SCALE_STATUS_SLIDE 'スライド中。
        End Enum

        Private SCALE_RANGE_SHIFT_X As Long '範囲スケールに移行する移動量(ピクセル)。
        Private SCALE_RANGE_SHIFT_Y As Long '範囲スケールに移行する移動量(ピクセル)。
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'定数
        private const long BACKGROUND_COLOR = 0x00;                 //背景色。

        private const long NORTHARROW_COLOR_LINE = 0xCCCCCC;        //ノースアロー線色。
        private const long NORTHARROW_COLOR_SYMBOL = 0xFF;          //ノースアロー記号色。
        private const long NORTHARROW_WIDTH_LINE = 2;               //ノースアロー線幅(ピクセル)。

        private const long PLOT_SCALEBAR_COLOR_SYMBOL = 0x66FF66;   //スケールバー記号色。

        private const long SMALL_CHANGE = 32;                       //スクロール量。

        private enum SCALE_STATUS                                   //スケール状態。
        {
            SCALE_STATUS_NONE = 0,                                  //なし。
            SCALE_STATUS_BEGIN,                                     //開始済。
            SCALE_STATUS_RANGE,                                     //範囲指定中。
            SCALE_STATUS_SLIDE,                                     //スライド中。
        }

        private long SCALE_RANGE_SHIFT_X;                           //範囲スケールに移行する移動量(ピクセル)。
        private long SCALE_RANGE_SHIFT_Y;                           //範囲スケールに移行する移動量(ピクセル)。

#if true
        private const int PICVIEW_WIDTH = 450;
        private const int PICVIEW_HEIGTH = 600;
#else   //debug
        private const int PICVIEW_WIDTH = 300;
        private const int PICVIEW_HEIGTH = 300;
#endif

        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'イベント

        'オブジェクト左クリックイベント。
        '
        'オブジェクトが左クリックされたときに発生する。
        '
        '引き数：
        'clsObject 対象とするオブジェクト。
        'Button ボタンを示す整数値。ビット 0 がマウスの左ボタン、ビット 1 が右ボタン、ビット 2 が中央ボタンに対応。
        'Shift Shift キー、Ctrl キーおよび Alt キーの状態を示す整数値。ビット 0 が Shift キー、ビット 1 が Ctrl キー、ビット 2 が Alt キーに対応。ビットは、キーが押されている場合にオン (1) になる。
        Public Event ClickObject(ByVal clsObject As Object, ByVal Button As Integer, ByVal Shift As Integer)

        'オブジェクト右クリックイベント。
        '
        'オブジェクトが右クリックされたときに発生する。
        Public Event RightDown()

        '等倍スケール要求イベント。
        '
        '等倍スケールが要求されたときに発生する。
        '
        '引き数：
        'bEnlarge 拡大フラグ。True=拡大。False=縮小。
        'X ビューの座標(ピクセル)。
        'Y ビューの座標(ピクセル)。
        Public Event MultipleScale(ByVal bEnlarge As Boolean, ByVal X As Single, ByVal Y As Single)

        '範囲指定スケール要求イベント。
        '
        '範囲指定スケールが要求されたときに発生する。
        '
        '引き数：
        'bEnlarge 拡大フラグ。True=拡大。False=縮小。
        'nSX ビューの範囲始点座標(ピクセル)。
        'nSY ビューの範囲始点座標(ピクセル)。
        'nEX ビューの範囲終点座標(ピクセル)。
        'nEY ビューの範囲終点座標(ピクセル)。
        Public Event RangeScale(ByVal bEnlarge As Boolean, ByVal nSX As Single, ByVal nSY As Single, ByVal nEX As Single, ByVal nEY As Single)
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        'イベント

        'オブジェクト左クリックイベント。
        '
        'オブジェクトが左クリックされたときに発生する。
        '
        '引き数：
        'clsObject 対象とするオブジェクト。
        'Button ボタンを示す整数値。ビット 0 がマウスの左ボタン、ビット 1 が右ボタン、ビット 2 が中央ボタンに対応。
        'Shift Shift キー、Ctrl キーおよび Alt キーの状態を示す整数値。ビット 0 が Shift キー、ビット 1 が Ctrl キー、ビット 2 が Alt キーに対応。ビットは、キーが押されている場合にオン (1) になる。
        */
#if false
        /*
            *************************** 修正要 sakai
        */
        public event ClickObject(Object clsObject, int Button, int Shift)
#endif
        /*
        'オブジェクト右クリックイベント。
        '
        'オブジェクトが右クリックされたときに発生する。
        */
#if false
        /*
            *************************** 修正要 sakai
        */
        public event RightDown()
#endif
        /*
        '等倍スケール要求イベント。
        '
        '等倍スケールが要求されたときに発生する。
        '
        '引き数：
        'bEnlarge 拡大フラグ。True=拡大。False=縮小。
        'X ビューの座標(ピクセル)。
        'Y ビューの座標(ピクセル)。
        */
#if false
        /*
            *************************** 修正要 sakai
        */
        public event MultipleScale(bool bEnlarge, float X, float Y)
#endif
        /*
        '範囲指定スケール要求イベント。
        '
        '範囲指定スケールが要求されたときに発生する。
        '
        '引き数：
        'bEnlarge 拡大フラグ。True=拡大。False=縮小。
        'nSX ビューの範囲始点座標(ピクセル)。
        'nSY ビューの範囲始点座標(ピクセル)。
        'nEX ビューの範囲終点座標(ピクセル)。
        'nEY ビューの範囲終点座標(ピクセル)。
        */
#if false
        /*
            *************************** 修正要 sakai
        */
        public event RangeScale(bool bEnlarge, float nSX, float nSY, float nEX, float nEY)
#endif
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'インプリメンテーション
        Private m_clsPlotPort As New PlotPort 'プロット領域。
        Private m_clsPlotNative As New PlotNative 'プロットネイティブ。
        Private m_clsEmphasisHead As ChainList '強調オブジェクトリストのヘッダー。要素が ObservationPoint の場合それは代表観測点である。
        Private m_objFont As StdFont '標準のフォント。
        Private m_clsClickManager As New ClickManager 'クリックマネージャ。
        Private m_nScaleMode As SCALE_MODE 'スケールモード。
        Private m_nScaleStatus As SCALE_STATUS 'スケール状態。
        Private m_bDevicePoint As Boolean '描画座標設定要求フラグ。
        Private m_nPointSX As Long 'キャプチャ時マウス位置(ピクセル)。
        Private m_nPointSY As Long 'キャプチャ時マウス位置(ピクセル)。
        Private m_nPointEX As Long '移動時マウス位置(ピクセル)。
        Private m_nPointEY As Long '移動時マウス位置(ピクセル)。
        Private m_bSetRedraw As Boolean '描画許可フラグ。
        Private m_clsRegistAngle As RegistAngle '閉合登録。この変数が非 Nothing であれば閉合画面とみなす。Nothing であれば通常の配置図とみなす。
        Private m_objCurrent As Object 'クリックカレントオブジェクト。
        Private m_hPenNorthArrow As Long 'ノースアローペン。
        Private m_hBrushBK As Long '背景ブラシ。
        Private m_hBrushScale1 As Long 'スケールバーブラシ１。
        Private m_hBrushScale2 As Long 'スケールバーブラシ２。
        Private m_bDrawPermission As Boolean '描画許可フラグ。
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'インプリメンテーション
#if false
        private PlotPort m_clsPlotPort = new PlotPort();                //プロット領域。
        private MdlPlotProc m_clsMdlPlotProc = new MdlPlotProc();       //'プロット画面プロシージャ
        private PlotNative m_clsPlotNative = new PlotNative();          //'プロットネイティブ。
        //private ChainList m_clsEmphasisHead;                            //'強調オブジェクトリストのヘッダー。要素が ObservationPoint の場合それは代表観測点である。
        private ChainList m_clsEmphasisHead = new ChainList();          //'強調オブジェクトリストのヘッダー。要素が ObservationPoint の場合それは代表観測点である。
#else
        private PlotPort m_clsPlotPort;                                 //プロット領域。
        private MdlPlotProc m_clsMdlPlotProc;                           //'プロット画面プロシージャ
        private PlotNative m_clsPlotNative;                             //'プロットネイティブ。
        private ChainList m_clsEmphasisHead;                            //'強調オブジェクトリストのヘッダー。要素が ObservationPoint の場合それは代表観測点である。
        private List<GENBA_STRUCT_S> m_List_Genba_S;                    //現場リスト
#endif
#if false
        /*
            *************************** 修正要 sakai
        */
        private StdFont m_objFont;                                      //'標準のフォント。
#endif
        private ClickManager m_clsClickManager = new ClickManager();    //'クリックマネージャ。
        private SCALE_MODE m_nScaleMode;                                //'スケールモード。
        private SCALE_STATUS m_nScaleStatus;                            //'スケール状態。
        private bool m_bDevicePoint;                                    //'描画座標設定要求フラグ。
        private long m_nPointSX;                                        //'キャプチャ時マウス位置(ピクセル)。
        private long m_nPointSY;                                        //'キャプチャ時マウス位置(ピクセル)。
        private long m_nPointEX;                                        //'移動時マウス位置(ピクセル)。
        private long m_nPointEY;                                        //'移動時マウス位置(ピクセル)。
        private bool m_bSetRedraw;                                      //'描画許可フラグ。
        private RegistAngle m_clsRegistAngle;                           //'閉合登録。この変数が非 Nothing であれば閉合画面とみなす。Nothing であれば通常の配置図とみなす。
        private object m_objCurrent;                                    //'クリックカレントオブジェクト。
        private long m_hPenNorthArrow;                                  //'ノースアローペン。
        private long m_hBrushBK;                                        //'背景ブラシ。
        private long m_hBrushScale1;                                    //'スケールバーブラシ１。
        private long m_hBrushScale2;                                    //'スケールバーブラシ２。
        private bool m_bDrawPermission;                                 //'描画許可フラグ。
        private MdlMain m_clsMdlMain;                                   //MdlMainインスタンス

        public object SM_CXDRAG { get; private set; }
        public object SM_CYDRAG { get; private set; }
        public object SM_CXVSCROLL { get; private set; }
        public object SM_CYHSCROLL { get; private set; }

        public Bitmap m_btmp;

        //==========================================================================================

        //==========================================================================================
        //[C#]
        public void List_Genba_set(List<GENBA_STRUCT_S> List_Genba_S)
        {
            m_List_Genba_S = List_Genba_S;
            return;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '*******************************************************************************
        'プロパティ

        'スケールモード。
        Property Let ScaleMode(ByVal nScaleMode As SCALE_MODE)
            m_nScaleMode = nScaleMode
            m_nScaleStatus = SCALE_STATUS_NONE
            Select Case m_nScaleMode
                Case SCALE_MODE_ENLARGE
                    picView.MouseIcon = imlCursors.ListImages("Range").Picture
                    picView.MousePointer = vbCustom
                    scrVert.MousePointer = vbArrow
                    scrHori.MousePointer = vbArrow
                Case SCALE_MODE_REDUCE
                    picView.MouseIcon = imlCursors.ListImages("Range").Picture
                    picView.MousePointer = vbCustom
                    scrVert.MousePointer = vbArrow
                    scrHori.MousePointer = vbArrow
                Case SCALE_MODE_MULTIPLE
                    picView.MouseIcon = imlCursors.ListImages("Multiple").Picture
                    picView.MousePointer = vbCustom
                    scrVert.MousePointer = vbArrow
                    scrHori.MousePointer = vbArrow
                Case SCALE_MODE_RANGE
                    picView.MouseIcon = imlCursors.ListImages("Range").Picture
                    picView.MousePointer = vbCustom
                    scrVert.MousePointer = vbArrow
                    scrHori.MousePointer = vbArrow
                Case SCALE_MODE_SLIDE
                    picView.MouseIcon = imlCursors.ListImages("Slide").Picture
                    picView.MousePointer = vbCustom
                    scrVert.MousePointer = vbArrow
                    scrHori.MousePointer = vbArrow
                Case Else
                    picView.MousePointer = vbDefault
                    scrVert.MousePointer = vbDefault
                    scrHori.MousePointer = vbDefault
            End Select
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '*******************************************************************************
        'プロパティ
        
        'スケールモード。
        */
        public void ScaleMode(SCALE_MODE nScaleMode)
        {
            m_nScaleMode = nScaleMode;
            m_nScaleStatus = SCALE_STATUS.SCALE_STATUS_NONE;
            switch ((int)m_nScaleMode)
            {
                case (int)SCALE_MODE.SCALE_MODE_ENLARGE:
                    picView.Cursor = Cursors.Hand;
                    //picView.MousePointer = vbCustom;
                    //scrVert.MousePointer = vbArrow;
                    //scrHori.MousePointer = vbArrow;
                    break;
                case (int)SCALE_MODE.SCALE_MODE_REDUCE:
                    picView.Cursor = Cursors.Hand;
                    //picView.MousePointer = vbCustom;
                    //scrVert.MousePointer = vbArrow;
                    //scrHori.MousePointer = vbArrow;
                    break;
                case (int)SCALE_MODE.SCALE_MODE_MULTIPLE:
                    picView.Cursor = Cursors.Hand;
                    //picView.MousePointer = vbCustom;
                    //scrVert.MousePointer = vbArrow;
                    //scrHori.MousePointer = vbArrow;
                    break;
                case (int)SCALE_MODE.SCALE_MODE_RANGE:
                    picView.Cursor = Cursors.Hand;
                    //picView.MousePointer = vbCustom;
                    //scrVert.MousePointer = vbArrow;
                    //scrHori.MousePointer = vbArrow;
                    break;
                case (int)SCALE_MODE.SCALE_MODE_SLIDE:
                    picView.Cursor = Cursors.Hand;
                    //picView.MousePointer = vbCustom;
                    //scrVert.MousePointer = vbArrow;
                    //scrHori.MousePointer = vbArrow;
                    break;
                default:
                    picView.Cursor = Cursors.Hand;
                    //scrVert.MousePointer = vbDefault;
                    //scrHori.MousePointer = vbDefault;
                    break;
            }
            return;
        }
        //==========================================================================================

        //==========================================================================================
        //[C#]
        public Bitmap Dis_Get_btmp()
        {
            if (m_btmp != null)
            {
                m_btmp.Dispose();
                m_btmp = null;
            }
            m_btmp = new Bitmap(picView.Width, picView.Height);

            return m_btmp;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'スケールモード。
        Property Get ScaleMode() As SCALE_MODE
            ScaleMode = m_nScaleMode
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'スケールモード。
        public SCALE_MODE ScaleMode()
        {
            return m_nScaleMode;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '描画許可フラグ。
        Property Let SetRedraw(ByVal bSetRedraw As Boolean)
            If bSetRedraw Then
                If Not m_bSetRedraw Then
                    m_bSetRedraw = bSetRedraw
                    Call Display
                End If
            Else
                m_bSetRedraw = bSetRedraw
            End If
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'描画許可フラグ。
#if false
        public void SetRedraw(bool bSetRedraw)
        {
#else
        public void SetRedraw(bool bSetRedraw, Bitmap btmp)
        {
#endif
            if (bSetRedraw)
            {
                if (!m_bSetRedraw)
                {
#if false
                    m_bSetRedraw = bSetRedraw;
                    Display();
#else
                    m_bSetRedraw = bSetRedraw;
                    Display(btmp);
#endif
                }
            }
            else
            {
                m_bSetRedraw = bSetRedraw;
            }
            return;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '描画許可フラグ。
        Property Get SetRedraw() As Boolean
            SetRedraw = m_bSetRedraw
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'描画許可フラグ。
        public bool SetRedraw()
        {
            return m_bSetRedraw;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '*******************************************************************************
        'イベント

        '初期化。
        Private Sub UserControl_Initialize()

            On Error GoTo ErrorHandler


            SCALE_RANGE_SHIFT_X = GetSystemMetrics(SM_CXDRAG)
            SCALE_RANGE_SHIFT_Y = GetSystemMetrics(SM_CYDRAG)


            Set m_clsEmphasisHead = New ChainList
            Set m_objFont = New StdFont


            m_bSetRedraw = True
            m_nScaleStatus = SCALE_STATUS_NONE
            m_bDrawPermission = True
    
            'ビューの初期化。
            picView.Left = 0
            picView.Top = 0
            picMemDC.Left = 0
            picMemDC.Top = 0
    
            'フォント。
            m_objFont.Name = picView.Font.Name
            m_objFont.Charset = picView.Font.Charset
            m_objFont.SIZE = picView.Font.SIZE
            m_objFont.Weight = picView.Font.Weight
            Set picView.Font = m_objFont
            Set picMemDC.Font = m_objFont
    
            '描画範囲。
            picView.ScaleMode = vbPixels
            picView.ScaleLeft = 0
            picView.ScaleTop = 0
            picMemDC.ScaleMode = vbPixels
            picMemDC.ScaleLeft = 0
            picMemDC.ScaleTop = 0
            m_nScaleMode = SCALE_MODE_NOSCALE
    
            'クリックマネージャ。
            m_clsClickManager.BasedRadius = PLOT_OBSPNT_RADIUS_BASE / Screen.TwipsPerPixelX
            m_clsClickManager.FixedRadius = PLOT_OBSPNT_RADIUS_FIX / Screen.TwipsPerPixelX
            m_clsClickManager.PointRadius = PLOT_OBSPNT_RADIUS / Screen.TwipsPerPixelX
            m_clsClickManager.ArrowLength = PLOT_VECTOR_ARROW_LENGTH / Screen.TwipsPerPixelX
            m_clsClickManager.ArrowWidth = PLOT_VECTOR_ARROW_WIDTH / Screen.TwipsPerPixelX
    
            'スクロールバー。
            scrHori.Left = picView.ScaleLeft
            scrVert.Top = picView.ScaleTop
            scrHori.Height = GetSystemMetrics(SM_CYHSCROLL)
            scrVert.Width = GetSystemMetrics(SM_CXVSCROLL)
            scrHori.Min = 0
            scrVert.Min = 0


            Exit Sub


        ErrorHandler:
            Call mdlMain.ErrorExit

        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '*******************************************************************************
        'イベント
        
        '初期化。
        */
        private void UserControl_Initialize()
        {
            try
            {
                SCALE_RANGE_SHIFT_X = GetSystemMetrics((int)DEFINE.SM_CXDRAG);
                SCALE_RANGE_SHIFT_Y = GetSystemMetrics((int)DEFINE.SM_CYDRAG);


                ChainList m_clsEmphasisHead = new ChainList();
#if false
                /*
                    *************************** 修正要 sakai
                */
                StdFont m_objFont = new StdFont();
#endif
                m_bSetRedraw = true;
                m_nScaleStatus = SCALE_STATUS.SCALE_STATUS_NONE;
                m_bDrawPermission = true;


                //'ビューの初期化。
                picView.Left = 0;
                picView.Top = 0;
                picMemDC.Left = 0;
                picMemDC.Top = 0;


                //'フォント。
#if false
                /*
                    *************************** 修正要 sakai
                */
                m_objFont.Name = picView.Font.Name;
                m_objFont.Charset = picView.Font.Charset;
                m_objFont.SIZE = picView.Font.SIZE;
                m_objFont.Weight = picView.Font.Weight;
                picView.Font = m_objFont;
                picMemDC.Font = m_objFont;
#endif

                //'描画範囲。
#if false
                /*
                    *************************** 修正要 sakai
                */
                picView.ScaleMode = vbPixels;
                picView.ScaleLeft = 0;
                picView.ScaleTop = 0;
                picMemDC.ScaleMode = vbPixels;
                picMemDC.ScaleLeft = 0;
                picMemDC.ScaleTop = 0;
#else
                m_nScaleMode = SCALE_MODE.SCALE_MODE_NOSCALE;
#endif


                //'クリックマネージャ。
#if false
                /*
                    *************************** 修正要 sakai
                */
                m_clsClickManager.BasedRadius = PLOT_OBSPNT_RADIUS_BASE / Screen.TwipsPerPixelX;
                m_clsClickManager.FixedRadius = PLOT_OBSPNT_RADIUS_FIX / Screen.TwipsPerPixelX;
                m_clsClickManager.PointRadius = PLOT_OBSPNT_RADIUS / Screen.TwipsPerPixelX;
                m_clsClickManager.ArrowLength = PLOT_VECTOR_ARROW_LENGTH / Screen.TwipsPerPixelX;
                m_clsClickManager.ArrowWidth = PLOT_VECTOR_ARROW_WIDTH / Screen.TwipsPerPixelX;
#endif

#if false
                /*
                    *************************** 修正要 sakai
                */
                //'スクロールバー。
                scrHori.Left = picView.ScaleLeft;
                scrVert.Top = picView.ScaleTop;
#else
                scrHori.Height = GetSystemMetrics((int)DEFINE.SM_CYHSCROLL);
                scrVert.Width = GetSystemMetrics((int)DEFINE.SM_CXVSCROLL);
                scrHori.Minimum = 0;
                scrVert.Minimum = 0;
                return;
#endif
            }


            catch
            {
                m_clsMdlMain.ErrorExit();
                return;
            }
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '終了。
        Private Sub Class_Terminate()

            On Error GoTo ErrorHandler

            If m_hPenNorthArrow<> 0 Then Call DeleteObject(m_hPenNorthArrow)
            If m_hBrushBK<> 0 Then Call DeleteObject(m_hBrushBK)
            If m_hBrushScale1<> 0 Then Call DeleteObject(m_hBrushScale1)
            If m_hBrushScale2<> 0 Then Call DeleteObject(m_hBrushScale2)

            Call m_clsEmphasisHead.RemoveAll

            Exit Sub

        ErrorHandler:
            Call mdlMain.ErrorExit

        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'終了。
        private void Class_Terminate()
        {
            try
            {
                if (m_hPenNorthArrow != 0)
                {
#if false
                    /*
                        *************************** 修正要 sakai
                    */
                    DeleteObject(m_hPenNorthArrow);
#endif
                }
                if (m_hBrushBK != 0)
                {
#if false
                    /*
                        *************************** 修正要 sakai
                    */
                    DeleteObject(m_hBrushBK);
#endif
                }
                if (m_hBrushScale1 != 0)
                {
#if false
                    /*
                        *************************** 修正要 sakai
                    */
                    DeleteObject(m_hBrushScale1);
#endif
                }
                if (m_hBrushScale2 != 0)
                {
#if false
                    /*
                        *************************** 修正要 sakai
                    */
                    DeleteObject(m_hBrushScale2);
#endif
                }

                m_clsEmphasisHead.RemoveAll();
                return;

            }

            catch
            {
                m_clsMdlMain.ErrorExit();
                return;
            }
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'リサイズ。
        Private Sub UserControl_Resize()

            On Error GoTo ErrorHandler

            'ビューのリサイズ。
            picView.Width = ScaleWidth
            picView.Height = ScaleHeight
            picMemDC.Width = ScaleWidth * PLOT_MEMDC_SIZE
            picMemDC.Height = ScaleHeight * PLOT_MEMDC_SIZE


            m_clsPlotPort.ViewWidth = picView.ScaleWidth
            m_clsPlotPort.ViewHeight = picView.ScaleHeight
            m_clsPlotPort.DeviceWidth = picMemDC.ScaleWidth
            m_clsPlotPort.DeviceHeight = picMemDC.ScaleHeight

            '表示範囲の更新。
            Call UpdateViewArea

            'デバイス位置の更新。
            m_clsPlotPort.DeviceLeft = m_clsPlotPort.ViewLeft - (picMemDC.ScaleWidth - m_clsPlotPort.ViewWidth) * 0.5
            m_clsPlotPort.DeviceTop = m_clsPlotPort.ViewTop - (picMemDC.ScaleHeight - m_clsPlotPort.ViewHeight) * 0.5

            '描画位置の補正。
            Call m_clsPlotPort.CorrectDrawArea

            '描画。
            Dim nStyle As Long
            nStyle = GetWindowLong(hWnd, GWL_STYLE)
            If (nStyle And WS_VISIBLE) <> 0 Then
                Call Draw(picMemDC)
                Call Display
            End If


            Exit Sub


        ErrorHandler:
            Call mdlMain.ErrorExit


        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'リサイズ。
#if false
        private void UserControl_Resize()
        {
#else
        private void UserControl_Resize()
        {
#endif
            try
            {
                //'ビューのリサイズ。
                picView.Width = PICVIEW_WIDTH;
                picView.Height = PICVIEW_HEIGTH;
                picMemDC.Width = PICVIEW_WIDTH * (int)PLOT_MEMDC_SIZE;
                picMemDC.Height = PICVIEW_HEIGTH * (int)PLOT_MEMDC_SIZE;


                m_clsPlotPort.ViewWidth = picView.Width;
                m_clsPlotPort.ViewHeight = picView.Height;
                m_clsPlotPort.DeviceWidth = picMemDC.Width;
                m_clsPlotPort.DeviceHeight = picMemDC.Height;


                //'表示範囲の更新。
                UpdateViewArea();


                //'デバイス位置の更新。
                m_clsPlotPort.DeviceLeft = m_clsPlotPort.ViewLeft - (picMemDC.Width - m_clsPlotPort.ViewWidth) * 0.5;
                m_clsPlotPort.DeviceTop = m_clsPlotPort.ViewTop - (picMemDC.Height - m_clsPlotPort.ViewHeight) * 0.5;


                //'描画位置の補正。
                m_clsPlotPort.CorrectDrawArea();


                //'描画。
                long nStyle = 0;
                IntPtr hWnd = picMemDC.Handle;
                uint style = (uint)GWL_STYLE;
                nStyle = GetWindowLong(hWnd, style);
                if ((nStyle & WS_VISIBLE) != 0)
                {
#if false
                    Draw(ref picMemDC);
                    Display();
#else
                    Draw(ref m_btmp);
                    Display(m_btmp);
#endif
                }
                else
                {
#if false
                    Display();
#else
                    Display(m_btmp);
#endif
                }
                return;
            }

            catch (Exception ex)
            {
                m_clsMdlMain.ErrorExit();
                return;
            }
        }

        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '表示。
        Private Sub UserControl_Show()
            '非表示中は描画は抑制してあるので、再描画が必要。
            Call Redraw
            Call Refresh
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'表示。
        private void UserControl_Show()
        {
            //'非表示中は描画は抑制してあるので、再描画が必要。
            Redraw();
            Refresh();
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'マウスダウン。
        Private Sub picView_MouseDown(Button As Integer, Shift As Integer, X As Single, Y As Single)

            On Error GoTo ErrorHandler


            If m_nScaleMode = SCALE_MODE_NOSCALE Then
                Dim nDeviceX As Long
                Dim nDeviceY As Long
                nDeviceX = X + m_clsPlotPort.DeviceLeft
                nDeviceY = Y + m_clsPlotPort.DeviceTop

                Dim clsObject As Object
                If(Button And vbRightButton) <> 0 Then
                    If m_clsRegistAngle Is Nothing Then
                        '選択オブジェクトの変更。
                        Dim objCollection As Collection
                        Dim objFirst As Object
                        Set objCollection = m_clsClickManager.HitObjectAll(nDeviceX, nDeviceY, objFirst)
                        'クリックオブジェクトは在る？
                        If objCollection.Count > 0 Then
                            '選択済オブジェクトが在る？
                            Dim clsChainList As ChainList
                            Set clsChainList = m_clsEmphasisHead.NextList
                            Do While Not clsChainList Is Nothing
                                If LookupCollectionObject(objCollection, clsObject, Hex$(GetPointer(clsChainList.Element))) Then Exit Do
                                Set clsChainList = clsChainList.NextList
                            Loop
                            '選択済オブジェクトがなければ選択する。
                            If clsChainList Is Nothing Then
                                Set m_objCurrent = objFirst
                                'オブジェクトクリックイベント。
                                RaiseEvent ClickObject(objFirst, Button, Shift)
                            End If
                            '右クリックイベント。
                            If(Button And vbRightButton) <> 0 Then RaiseEvent RightDown
                        End If
                    Else
                        '閉合登録処理。
                    End If
                Else
                    If m_clsRegistAngle Is Nothing Then
                        '選択オブジェクトの変更。
                        Set clsObject = m_clsClickManager.HitObject(nDeviceX, nDeviceY, m_objCurrent)
                        Set m_objCurrent = clsObject
                        'オブジェクトクリックイベント。
                        RaiseEvent ClickObject(clsObject, Button, Shift)
                    Else
                        '閉合登録処理。
                        Set objCollection = m_clsClickManager.HitObjectAll(nDeviceX, nDeviceY)
                        'メニューの設定。
                        Dim clsBaseLineVector As BaseLineVector
                        Dim nIndex As Long
                        nIndex = 0
                        For Each clsBaseLineVector In objCollection
                            'メニューが足りなかったらゴメンナサイ。
                            If mnuAngleSelect.UBound<nIndex Then Exit For
                            '候補として有効な基線ベクトルのみ追加。
                            If m_clsRegistAngle.IsCandidate(clsBaseLineVector) Then
                                mnuAngleSelect(nIndex).Caption = m_clsPlotNative.GetCandidateCommandString(clsBaseLineVector)
                                mnuAngleSelect(nIndex).Visible = True
                                mnuAngleSelect(nIndex).Tag = Hex$(GetPointer(clsBaseLineVector))
                                nIndex = nIndex + 1
                                Set clsObject = clsBaseLineVector 'とりあえずなんでもいいから記憶する。
                            End If
                        Next
                        If nIndex > 1 Then
                            '余ったメニューは非表示。
                            For nIndex = nIndex To mnuAngleSelect.UBound
                                mnuAngleSelect(nIndex).Visible = False
                            Next
                            'ポップアップ。
                            Call PopupMenu(mnuAngle)
                        ElseIf nIndex > 0 Then
                            'オブジェクトクリックイベント。
                            RaiseEvent ClickObject(clsObject, Button, Shift)
                        Else
                            '候補が一つもなければ通常の選択オブジェクトの変更。
                            Set clsObject = m_clsClickManager.HitObject(nDeviceX, nDeviceY, m_objCurrent)
                            Set m_objCurrent = clsObject
                            'オブジェクトクリックイベント。
                            RaiseEvent ClickObject(clsObject, Button, Shift)
                        End If
                    End If
                End If
            ElseIf m_nScaleStatus = SCALE_STATUS_NONE Then
                If m_nScaleMode = SCALE_MODE_ENLARGE Then
                    '拡大スケールイベント。
                    m_nPointSX = X
                    m_nPointSY = Y
                    m_nPointEX = X
                    m_nPointEY = Y
                    m_nScaleStatus = SCALE_STATUS_BEGIN
                ElseIf m_nScaleMode = SCALE_MODE_REDUCE Then
                    '縮小スケールイベント。
                    m_nPointSX = X
                    m_nPointSY = Y
                    m_nPointEX = X
                    m_nPointEY = Y
                    m_nScaleStatus = SCALE_STATUS_BEGIN
                ElseIf m_nScaleMode = SCALE_MODE_MULTIPLE Then
                    '等倍スケールイベント。
                    RaiseEvent MultipleScale(Button = vbLeftButton, X, Y)
                ElseIf m_nScaleMode = SCALE_MODE_RANGE Then
                    '範囲指定スケール。
                    m_nPointSX = X
                    m_nPointSY = Y
                    m_nPointEX = X
                    m_nPointEY = Y
                    m_nScaleStatus = SCALE_STATUS_RANGE
                ElseIf m_nScaleMode = SCALE_MODE_SLIDE Then
                    If(Button And vbLeftButton) <> 0 Then
                        'スライド。
                        picView.MouseIcon = imlCursors.ListImages("Hold").Picture
                        Dim pnt As POINT
                        Call GetCursorPos(pnt)
                        m_nPointSX = pnt.X
                        m_nPointSY = pnt.Y
                        m_nPointEX = pnt.X
                        m_nPointEY = pnt.Y
                        m_nScaleStatus = SCALE_STATUS_SLIDE
                    End If
                End If
            End If

            Exit Sub

        ErrorHandler:
            Call mdlMain.ErrorExit


        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'マウスダウン。
        private void picView_MouseDown(int Button, int Shift, float X, float Y)
        {
            try
            {
                if (m_nScaleMode == SCALE_MODE.SCALE_MODE_NOSCALE)
                {
                    long nDeviceX;
                    long nDeviceY;
                    nDeviceX = (long)(X + m_clsPlotPort.DeviceLeft);
                    nDeviceY = (long)(Y + m_clsPlotPort.DeviceTop);

                    object clsObject;
                    if ((Button & (int)MouseButtons.Right) != 0)
                    {
                        if (m_clsRegistAngle == null)
                        {
#if false
                            /*
                             *************************** 修正要 sakai
                            */
                            //'選択オブジェクトの変更。
                            Collection objCollection;
                            object objFirst;
                            objCollection = m_clsClickManager.HitObjectAll(nDeviceX, nDeviceY, objFirst);
                            //'クリックオブジェクトは在る？
                            if (objCollection.Count() > 0)
                            {
                                //'選択済オブジェクトが在る？
                                ChainList clsChainList;
                                clsChainList = m_clsEmphasisHead.NextList();
                                while (clsChainList != null)
                                {
                                    if (LookupCollectionObject(objCollection, clsObject, Hex$(GetPointer(clsChainList.Element))))
                                    {
                                        break;
                                    }
                                    clsChainList = clsChainList.NextList();
                                }
                                //'選択済オブジェクトがなければ選択する。
                                if (clsChainList == null)
                                {
                                    m_objCurrent = objFirst;
                                    //'オブジェクトクリックイベント。
                                    RaiseEvent ClickObject(objFirst, Button, Shift);
                                }
                                //'右クリックイベント。
                                if ((Button & (int)MouseButtons.Right) != 0)
                                {
                                    RaiseEvent RightDown();
                                }
                            }
#endif
                        }
                        else
                        {
                            //'閉合登録処理。
                        }
                    }
                    else
                    {
                        if (m_clsRegistAngle == null)
                        {
#if false
                            /*
                             *************************** 修正要 sakai
                            */
                            //'選択オブジェクトの変更。
                            clsObject = m_clsClickManager.HitObject(nDeviceX, nDeviceY, m_objCurrent);
                            m_objCurrent = clsObject;
                            //'オブジェクトクリックイベント。
                            RaiseEvent ClickObject(clsObject, Button, Shift);
#endif
                        }
                        else
                        {
#if false
                            /*
                             *************************** 修正要 sakai
                            */
                            //'閉合登録処理。
                            objCollection = m_clsClickManager.HitObjectAll(nDeviceX, nDeviceY);
#endif
                            //'メニューの設定。
                            BaseLineVector clsBaseLineVector;
                            long nIndex;
                            nIndex = 0;
                            //For Each clsBaseLineVector In objCollection
                            for (int i1 = 0; i1 < 100; i1++)
                            {
#if false
                                /*
                                 *************************** 修正要 sakai
                                */
                                //'メニューが足りなかったらゴメンナサイ。
                                If (mnuAngleSelect.UBound < nIndex Then Exit For
                                //'候補として有効な基線ベクトルのみ追加。
                                if (m_clsRegistAngle.IsCandidate(clsBaseLineVector))
                                {
                                    mnuAngleSelect[nIndex].Caption = m_clsPlotNative.GetCandidateCommandString(clsBaseLineVector);
                                    mnuAngleSelect(nIndex).Visible = true;
                                    mnuAngleSelect(nIndex).Tag = Hex$(GetPointer(clsBaseLineVector));
                                    nIndex = nIndex + 1;
                                    clsObject = clsBaseLineVector;  //'とりあえずなんでもいいから記憶する。
                                }
#endif
                            }
                            if (nIndex > 1)
                            {
#if false
                                /*
                                 *************************** 修正要 sakai
                                */
                                //'余ったメニューは非表示。
                                //For nIndex = nIndex To mnuAngleSelect.UBound
                                for (nIndex = nIndex; nIndex < mnuAngleSelect.UBound; nIndex++)
                                {
                                    mnuAngleSelect(nIndex).Visible = false;
                                }
                                //'ポップアップ。
                                PopupMenu(mnuAngle);
#endif
                            }
                            else if (nIndex > 0)
                            {
#if false
                                //'オブジェクトクリックイベント。
                                /*
                                 *************************** 修正要 sakai
                                */
                                RaiseEvent ClickObject(clsObject, Button, Shift);
#endif
                            }
                            else
                            {
#if false
                                /*
                                 *************************** 修正要 sakai
                                */
                                //'候補が一つもなければ通常の選択オブジェクトの変更。
                                clsObject = m_clsClickManager.HitObject(nDeviceX, nDeviceY, m_objCurrent);
                                m_objCurrent = clsObject;
                                //'オブジェクトクリックイベント。
                                RaiseEvent ClickObject(clsObject, Button, Shift);
#endif
                            }
                        }
                    }
                }
                else if (m_nScaleStatus == SCALE_STATUS.SCALE_STATUS_NONE)
                {
                    if (m_nScaleMode == SCALE_MODE.SCALE_MODE_ENLARGE)
                    {
                        //'拡大スケールイベント。
                        m_nPointSX = (long)X;
                        m_nPointSY = (long)Y;
                        m_nPointEX = (long)X;
                        m_nPointEY = (long)Y;
                        m_nScaleStatus = SCALE_STATUS.SCALE_STATUS_BEGIN;
                    }
                    else if (m_nScaleMode == SCALE_MODE.SCALE_MODE_REDUCE)
                    {
                        //'縮小スケールイベント。
                        m_nPointSX = (long)X;
                        m_nPointSY = (long)Y;
                        m_nPointEX = (long)X;
                        m_nPointEY = (long)Y;
                        m_nScaleStatus = SCALE_STATUS.SCALE_STATUS_BEGIN;
                    }
                    else if (m_nScaleMode == SCALE_MODE.SCALE_MODE_MULTIPLE)
                    {
                        //'等倍スケールイベント。
#if false
                        /*
                         *************************** 修正要 sakai
                        */
                        RaiseEvent MultipleScale(Button = (int)MouseButtons.Left, X, Y);
#endif
                    }
                    else if (m_nScaleMode == SCALE_MODE.SCALE_MODE_RANGE)
                    {
                        //'範囲指定スケール。
                        m_nPointSX = (long)X;
                        m_nPointSY = (long)Y;
                        m_nPointEX = (long)X;
                        m_nPointEY = (long)Y;
                        m_nScaleStatus = SCALE_STATUS.SCALE_STATUS_RANGE;
                    }
                    else if (m_nScaleMode == SCALE_MODE.SCALE_MODE_SLIDE)
                    {
                        if ((Button & (int)MouseButtons.Left) != 0)
                        {
                            //'スライド。
                            //picView.MouseIcon = imlCursors.ListImages("Hold").Picture
                            picView.Cursor = Cursors.Hand;
                            GetCursorPos(out POINT pnt);
                            m_nPointSX = pnt.X;
                            m_nPointSY = pnt.Y;
                            m_nPointEX = pnt.X;
                            m_nPointEY = pnt.Y;
                            m_nScaleStatus = SCALE_STATUS.SCALE_STATUS_SLIDE;
                        }
                    }
                }
                return;
            }

            catch
            {
                m_clsMdlMain.ErrorExit();
                return;
            }
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'マウスアップ。
        Private Sub picView_MouseUp(Button As Integer, Shift As Integer, X As Single, Y As Single)

            On Error GoTo ErrorHandler


            If m_nScaleStatus = SCALE_STATUS_BEGIN Then
                If m_nScaleMode = SCALE_MODE_ENLARGE Then
                    '等倍拡大。
                    RaiseEvent MultipleScale(True, X, Y)
                ElseIf m_nScaleMode = SCALE_MODE_REDUCE Then
                    '等倍縮小。
                    RaiseEvent MultipleScale(False, X, Y)
                End If
                m_nScaleStatus = SCALE_STATUS_NONE
            ElseIf m_nScaleStatus = SCALE_STATUS_RANGE Then
                '範囲指定スケール。
                Dim bEnlarge As Boolean
                '拡大縮小？
                If m_nScaleMode = SCALE_MODE_RANGE Then
                bEnlarge = Button = vbLeftButton
                ElseIf m_nScaleMode = SCALE_MODE_ENLARGE Then
                bEnlarge = True
                ElseIf m_nScaleMode = SCALE_MODE_REDUCE Then
                    bEnlarge = False
                End If
                '矩形消去。
                picView.DrawMode = vbInvert
                picView.DrawWidth = 1
                picView.Line (m_nPointSX, m_nPointSY)-(m_nPointEX, m_nPointEY), , B
                'キャンセル？
                If picView.ScaleLeft <= X And X<picView.ScaleLeft + picView.ScaleWidth And picView.ScaleTop <= Y And Y<picView.ScaleTop + picView.ScaleHeight Then
                    '補正。
                    If X<m_nPointSX Then
                        m_nPointEX = m_nPointSX
                        m_nPointSX = X
                    Else
                        m_nPointEX = X
                    End If
                    If Y<m_nPointSY Then
                        m_nPointEY = m_nPointSY
                        m_nPointSY = Y
                    Else
                        m_nPointEY = Y
                    End If
                    '範囲指定スケールイベント。
                    If m_nPointSX<> m_nPointEX And m_nPointSY<> m_nPointEY Then RaiseEvent RangeScale(bEnlarge, m_nPointSX, m_nPointSY, m_nPointEX, m_nPointEY)
                End If
                m_nScaleStatus = SCALE_STATUS_NONE
            ElseIf m_nScaleStatus = SCALE_STATUS_SLIDE Then
                picView.MouseIcon = imlCursors.ListImages("Slide").Picture
                m_nScaleStatus = SCALE_STATUS_NONE
            End If


            Exit Sub


        ErrorHandler:
            Call mdlMain.ErrorExit

        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'マウスアップ。
        private void picView_MouseUp(int Button, int Shift, float X, float Y)
        {

            try
            {
                if (m_nScaleStatus == SCALE_STATUS.SCALE_STATUS_BEGIN)
                {
                    if (m_nScaleMode == SCALE_MODE.SCALE_MODE_ENLARGE)
                    {
#if false
                        /*
                         *************************** 修正要 sakai
                        */
                        //'等倍拡大。
                        RaiseEvent MultipleScale(true, X, Y);
#endif
                    }
                    else if (m_nScaleMode == SCALE_MODE.SCALE_MODE_REDUCE)
                    {
#if false
                        /*
                         *************************** 修正要 sakai
                        */
                        //'等倍縮小。
                        RaiseEvent MultipleScale(false, X, Y);
#endif
                    }
                    m_nScaleStatus = SCALE_STATUS.SCALE_STATUS_NONE;
                }
                else if (m_nScaleStatus == SCALE_STATUS.SCALE_STATUS_RANGE)
                {
                    //'範囲指定スケール。
                    bool bEnlarge;
                    //'拡大縮小？
                    if (m_nScaleMode == SCALE_MODE.SCALE_MODE_RANGE)
                    {
                        Button = (int)MouseButtons.Left;
                        if (Button == (int)MouseButtons.Left)
                        {
                            bEnlarge = true;
                        }
                        else
                        {
                            bEnlarge = false;
                        }
                    }
                    else if (m_nScaleMode == SCALE_MODE.SCALE_MODE_ENLARGE)
                    {
                        bEnlarge = true;
                    }
                    else if (m_nScaleMode == SCALE_MODE.SCALE_MODE_REDUCE)
                    {
                        bEnlarge = false;
                    }
                    //'矩形消去。
                    //
                    //picView.DrawMode = vbInvert
                    //picView.DrawWidth = 1
                    //picView.Line(m_nPointSX, m_nPointSY) - (m_nPointEX, m_nPointEY), , B
                    //
                    //描画先とするImageオブジェクトを作成する
                    Bitmap canvas = new Bitmap(picView.Width, picView.Height);
                    //ImageオブジェクトのGraphicsオブジェクトを作成する
                    Graphics graph = Graphics.FromImage(canvas);
                    // Create pen. 黒 幅1
                    Pen blackPen = new Pen(Color.Black, 1);
                    //幅1の黒い四角を書く
                    graph.DrawRectangle(blackPen, m_nPointSX, m_nPointSY, m_nPointEX, m_nPointEY);
                    //リソースを解放する
                    graph.Dispose();
                    blackPen.Dispose();
                    //PictureBox1に表示する
                    picView.Image = canvas;

                    //'キャンセル？
                    //If picView.ScaleLeft <= X And X<picView.ScaleLeft +picView.ScaleWidth And picView.ScaleTop <= Y And Y<picView.ScaleTop +picView.ScaleHeight Then
                    if ((picView.Left <= X) && (X < (picView.Left + picView.Width)) && (picView.Top <= Y) && (Y < (picView.Top + picView.Height)))
                    {
                        //'補正。
                        if (X < m_nPointSX)
                        {
                            m_nPointEX = m_nPointSX;
                            m_nPointSX = (long)X;
                        }
                        else
                        {
                            m_nPointEX = (long)X;
                        }
                        if (Y < m_nPointSY)
                        {
                            m_nPointEY = m_nPointSY;
                            m_nPointSY = (long)Y;
                        }
                        else
                        {
                            m_nPointEY = (long)Y;
                        }
                        //'範囲指定スケールイベント。
                        if ((m_nPointSX != m_nPointEX) && (m_nPointSY != m_nPointEY))
                        {
#if false
                            /*
                             *************************** 修正要 sakai
                            */
                            RaiseEvent RangeScale(bEnlarge, m_nPointSX, m_nPointSY, m_nPointEX, m_nPointEY);
#endif
                        }
                    }
                    m_nScaleStatus = SCALE_STATUS.SCALE_STATUS_NONE;
                }
                else if (m_nScaleStatus == SCALE_STATUS.SCALE_STATUS_SLIDE)
                {
                    //picView.MouseIcon = imlCursors.ListImages("Slide").Picture;
                    picView.Cursor = Cursors.Hand;
                    m_nScaleStatus = SCALE_STATUS.SCALE_STATUS_NONE;
                }
                return;
            }


            catch
            {
                m_clsMdlMain.ErrorExit();
                return;
            }
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'マウスムーブ。
        Private Sub picView_MouseMove(Button As Integer, Shift As Integer, X As Single, Y As Single)

            On Error GoTo ErrorHandler


            If m_nScaleStatus >= SCALE_STATUS_BEGIN Then
                If GetCapture() = picView.hWnd Then
                    If m_nScaleStatus = SCALE_STATUS_BEGIN Then
                        m_nPointEX = X
                        m_nPointEY = Y
                        'ある程度移動したら範囲選択に移行。
                        If Abs(m_nPointEX - m_nPointSX) > SCALE_RANGE_SHIFT_X Or Abs(m_nPointEY - m_nPointSY) > SCALE_RANGE_SHIFT_Y Then
                            '範囲選択に移行。
                            m_nScaleStatus = SCALE_STATUS_RANGE
                            If m_nPointEX<picView.ScaleLeft Or picView.ScaleLeft + picView.ScaleWidth <= m_nPointEX Or m_nPointEY<picView.ScaleTop Or picView.ScaleTop + picView.ScaleHeight <= m_nPointEY Then
                                m_nPointEX = m_nPointSX
                                m_nPointEY = m_nPointSY
                            End If
                            '矩形描画。
                            picView.DrawMode = vbInvert
                            picView.DrawWidth = 1
                            picView.Line (m_nPointSX, m_nPointSY)-(m_nPointEX, m_nPointEY), , B
                End If
                ElseIf m_nScaleStatus = SCALE_STATUS_RANGE Then
                        '矩形消去。
                        picView.DrawMode = vbInvert
                picView.DrawWidth = 1
                        picView.Line (m_nPointSX, m_nPointSY)-(m_nPointEX, m_nPointEY), , B
                m_nPointEX = X
                        m_nPointEY = Y
                        If m_nPointEX < picView.ScaleLeft Or picView.ScaleLeft + picView.ScaleWidth <= m_nPointEX Or m_nPointEY<picView.ScaleTop Or picView.ScaleTop + picView.ScaleHeight <= m_nPointEY Then
                            m_nPointEX = m_nPointSX
                            m_nPointEY = m_nPointSY
                        End If
                        '矩形描画。
                        picView.Line (m_nPointSX, m_nPointSY)-(m_nPointEX, m_nPointEY), , B
                ElseIf m_nScaleStatus = SCALE_STATUS_SLIDE Then
                        'スライド処理。
                        Dim pnt As POINT
                        Call GetCursorPos(pnt)
                        m_nPointEX = pnt.X
                        m_nPointEY = pnt.Y
                        Call Slide
                    End If
                End If
            End If


            Exit Sub


        ErrorHandler:
            Call mdlMain.ErrorExit

        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'マウスムーブ。
        private void picView_MouseMove(int Button, int Shift, float X, float Y)
        {
            try
            {
                if (m_nScaleStatus >= SCALE_STATUS.SCALE_STATUS_BEGIN)
                {
                    //If GetCapture() = picView.hWnd Then
                    if (GetCapture() != 0)
                    {
                        if (m_nScaleStatus == SCALE_STATUS.SCALE_STATUS_BEGIN)
                        {
                            m_nPointEX = (long)X;
                            m_nPointEY = (long)Y;
                            //'ある程度移動したら範囲選択に移行。
                            if ((Math.Abs(m_nPointEX - m_nPointSX) > SCALE_RANGE_SHIFT_X) || (Math.Abs(m_nPointEY - m_nPointSY) > SCALE_RANGE_SHIFT_Y))
                            {
                                //'範囲選択に移行。
                                m_nScaleStatus = SCALE_STATUS.SCALE_STATUS_RANGE;
                                if ((m_nPointEX < picView.Left) || ((picView.Left + picView.Width) <= m_nPointEX) || (m_nPointEY < picView.Top) || ((picView.Top + picView.Height) <= m_nPointEY))
                                {
                                    m_nPointEX = m_nPointSX;
                                    m_nPointEY = m_nPointSY;
                                }
                                //'矩形描画。
                                //
                                //picView.DrawMode = vbInvert;
                                //picView.DrawWidth = 1;
                                //picView.Line(m_nPointSX, m_nPointSY) - (m_nPointEX, m_nPointEY), , B;
                                //
                                //描画先とするImageオブジェクトを作成する
                                Bitmap canvas = new Bitmap(picView.Width, picView.Height);
                                //ImageオブジェクトのGraphicsオブジェクトを作成する
                                Graphics graph = Graphics.FromImage(canvas);
                                // Create pen. 黒 幅1
                                Pen blackPen = new Pen(Color.Black, 1);
                                //幅1の黒い四角を書く
                                graph.DrawRectangle(blackPen, m_nPointSX, m_nPointSY, m_nPointEX, m_nPointEY);
                                //リソースを解放する
                                graph.Dispose();
                                blackPen.Dispose();
                                //PictureBox1に表示する
                                picView.Image = canvas;

                            }
                        }
                        else if (m_nScaleStatus == SCALE_STATUS.SCALE_STATUS_RANGE)
                        {
                            //'矩形消去。
                            //
                            //picView.DrawMode = vbInvert;
                            //picView.DrawWidth = 1;
                            //picView.Line(m_nPointSX, m_nPointSY) - (m_nPointEX, m_nPointEY), , B;
                            //
                            //描画先とするImageオブジェクトを作成する
                            Bitmap canvas = new Bitmap(picView.Width, picView.Height);
                            //ImageオブジェクトのGraphicsオブジェクトを作成する
                            Graphics graph = Graphics.FromImage(canvas);
                            // Create pen. 黒 幅1
                            Pen blackPen = new Pen(Color.Black, 1);
                            //幅1の黒い四角を書く
                            graph.DrawRectangle(blackPen, m_nPointSX, m_nPointSY, m_nPointEX, m_nPointEY);
                            //リソースを解放する
                            graph.Dispose();
                            blackPen.Dispose();
                            //PictureBox1に表示する
                            picView.Image = canvas;

                            m_nPointEX = (long)X;
                            m_nPointEY = (long)Y;
                            if ((m_nPointEX < picView.Left) || ((picView.Left + picView.Width) <= m_nPointEX) || (m_nPointEY < picView.Top) || ((picView.Top + picView.Height) <= m_nPointEY))
                            {
                                m_nPointEX = m_nPointSX;
                                m_nPointEY = m_nPointSY;
                            }
                            //'矩形描画。
                            //
                            //picView.Line(m_nPointSX, m_nPointSY) - (m_nPointEX, m_nPointEY), , B;
                            //
                            //描画先とするImageオブジェクトを作成する
                            Bitmap canvas1 = new Bitmap(picView.Width, picView.Height);
                            //ImageオブジェクトのGraphicsオブジェクトを作成する
                            Graphics graph_1 = Graphics.FromImage(canvas);
                            // Create pen. 黒 幅1
                            Pen blackPen_1 = new Pen(Color.Black, 1);
                            //幅1の黒い四角を書く
                            graph_1.DrawRectangle(blackPen, m_nPointSX, m_nPointSY, m_nPointEX, m_nPointEY);
                            //リソースを解放する
                            graph_1.Dispose();
                            blackPen_1.Dispose();
                            //PictureBox1に表示する
                            picView.Image = canvas;
                        }
                        else if (m_nScaleStatus == SCALE_STATUS.SCALE_STATUS_SLIDE)
                        {
                            //'スライド処理。
                            GetCursorPos(out POINT pnt);
                            m_nPointEX = pnt.X;
                            m_nPointEY = pnt.Y;
                            Slide();
                        }
                    }
                }
                return;
            }


            catch
            {
                m_clsMdlMain.ErrorExit();
                return;
            }
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '2022/07/21 Hitz H.Nakamura *******************************************************************
        'ファイルのドロップを追加。

        'ドラッグ＆ドロップ。
        Private Sub picView_OLEDragOver(data As DataObject, Effect As Long, Button As Integer, Shift As Integer, X As Single, Y As Single, State As Integer)

            On Error GoTo ErrorHandler


            Effect = Effect And Parent.GetDragOverIcon()

            Exit Sub

        ErrorHandler:
            Call mdlMain.ErrorExit


        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'ドラッグ＆ドロップ。
        private void picView_OLEDragOver(DataObject data, long Effect, int Button, int Shift, float X, float Y, int State)
        {
            try
            {
                //Effect = Effect && Parent.GetDragOverIcon();
                Effect = Effect & 1;
                return;
            }

            catch
            {
                m_clsMdlMain.ErrorExit();
                return;
            }
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'ドラッグ＆ドロップ。
        Private Sub picView_OLEDragDrop(data As DataObject, Effect As Long, Button As Integer, Shift As Integer, X As Single, Y As Single)

            On Error GoTo ErrorHandler


            If(Effect And vbDropEffectCopy) = 0 Then Exit Sub

            Call frmMain.DropFiles(data)

            Exit Sub

        ErrorHandler:
            Call mdlMain.ErrorExit


        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'ドラッグ＆ドロップ。
        private void picView_OLEDragDrop(DataObject data, long Effect, int Button, int Shift, float X, float Y)
        {
            try
            {
                //if ((Effect & vbDropEffectCopy) == 0)
                if ((Effect & 1) == 0)
                {
                    return;
                }
#if false
                /*
                 *************************** 修正要 sakai
                */
                frmMain.DropFiles(data);
#endif
            }

            catch
            {
                m_clsMdlMain.ErrorExit();
                return;
            }
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '**********************************************************************************************

        '横スクロール。
        Private Sub scrHori_Change()

            On Error GoTo ErrorHandler
    
            '表示位置。
            m_clsPlotPort.ViewLeft = scrHori.Value / scrHori.Max * (m_clsPlotPort.LogicalWidth * m_clsPlotPort.DrawScale - m_clsPlotPort.ViewWidth)


            If m_clsPlotPort.ViewLeft<m_clsPlotPort.DeviceLeft Or m_clsPlotPort.DeviceLeft + picMemDC.ScaleWidth<m_clsPlotPort.ViewLeft + m_clsPlotPort.ViewWidth Then
                '描画範囲も移動。
                m_clsPlotPort.DeviceLeft = m_clsPlotPort.ViewLeft - (picMemDC.ScaleWidth - m_clsPlotPort.ViewWidth) * 0.5
                m_clsPlotPort.DeviceTop = m_clsPlotPort.ViewTop - (picMemDC.ScaleHeight - m_clsPlotPort.ViewHeight) * 0.5
                '描画範囲が論理範囲からはみ出さないように補正。
                Call m_clsPlotPort.CorrectDrawArea
                '再描画。
                Call Redraw
            End If
    
            '再表示。
            Call Refresh


            Exit Sub


        ErrorHandler:
            Call mdlMain.ErrorExit

        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '**********************************************************************************************
        
        '横スクロール。
        */
        private void scrHori_Change()
        {
            try
            {
                //'表示位置。
                m_clsPlotPort.ViewLeft = scrHori.Value / scrHori.Maximum * (m_clsPlotPort.LogicalWidth * m_clsPlotPort.DrawScale - m_clsPlotPort.ViewWidth);

                if ((m_clsPlotPort.ViewLeft < m_clsPlotPort.DeviceLeft) || ((m_clsPlotPort.DeviceLeft + picMemDC.Width) < (m_clsPlotPort.ViewLeft + m_clsPlotPort.ViewWidth)))
                {
                    //'描画範囲も移動。
                    m_clsPlotPort.DeviceLeft = m_clsPlotPort.ViewLeft - (picMemDC.Width - m_clsPlotPort.ViewWidth) * 0.5;
                    m_clsPlotPort.DeviceTop = m_clsPlotPort.ViewTop - (picMemDC.Height - m_clsPlotPort.ViewHeight) * 0.5;
                    //'描画範囲が論理範囲からはみ出さないように補正。
                    m_clsPlotPort.CorrectDrawArea();
                    //'再描画。
                    Redraw();
                }
                //'再表示。
                Refresh();

                return;
            }

            catch
            {
                m_clsMdlMain.ErrorExit();
                return;
            }
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '縦スクロール。
        Private Sub scrVert_Change()

            On Error GoTo ErrorHandler
    
            '表示位置。
            m_clsPlotPort.ViewTop = scrVert.Value / scrVert.Max * (m_clsPlotPort.LogicalHeight * m_clsPlotPort.DrawScale - m_clsPlotPort.ViewHeight)


            If m_clsPlotPort.ViewTop<m_clsPlotPort.DeviceTop Or m_clsPlotPort.DeviceTop + picMemDC.ScaleHeight<m_clsPlotPort.ViewTop + m_clsPlotPort.ViewHeight Then
                '描画範囲も移動。
                m_clsPlotPort.DeviceLeft = m_clsPlotPort.ViewLeft - (picMemDC.ScaleWidth - m_clsPlotPort.ViewWidth) * 0.5
                m_clsPlotPort.DeviceTop = m_clsPlotPort.ViewTop - (picMemDC.ScaleHeight - m_clsPlotPort.ViewHeight) * 0.5
                '描画範囲が論理範囲からはみ出さないように補正。
                Call m_clsPlotPort.CorrectDrawArea
                '再描画。
                Call Redraw
            End If
    
            '再表示。
            Call Refresh


            Exit Sub


        ErrorHandler:
            Call mdlMain.ErrorExit

        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'縦スクロール。
        private void scrVert_Change()
        {
            try
            {
                //'表示位置。
                m_clsPlotPort.ViewTop = scrVert.Value / scrVert.Maximum * (m_clsPlotPort.LogicalHeight * m_clsPlotPort.DrawScale - m_clsPlotPort.ViewHeight);


                if ((m_clsPlotPort.ViewTop < m_clsPlotPort.DeviceTop) || ((m_clsPlotPort.DeviceTop + picMemDC.Height) < (m_clsPlotPort.ViewTop + m_clsPlotPort.ViewHeight)))
                {
                    //'描画範囲も移動。
                    m_clsPlotPort.DeviceLeft = m_clsPlotPort.ViewLeft - (picMemDC.Width - m_clsPlotPort.ViewWidth) * 0.5;
                    m_clsPlotPort.DeviceTop = m_clsPlotPort.ViewTop - (picMemDC.Height - m_clsPlotPort.ViewHeight) * 0.5;
                    //'描画範囲が論理範囲からはみ出さないように補正。
                    m_clsPlotPort.CorrectDrawArea();
                    //'再描画。
                    Redraw();
                }
                //'再表示。
                Refresh();

                return;
            }

            catch
            {
                m_clsMdlMain.ErrorExit();
                return;
            }
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '横スクロール。
        Private Sub scrHori_Scroll()
            Call scrHori_Change
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'横スクロール。
        private void scrHori_Scroll()
        {
            scrHori_Change();
            return;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '縦スクロール。
        Private Sub scrVert_Scroll()
            Call scrVert_Change
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'縦スクロール。
        private void scrVert_Scroll()
        {
            scrVert_Change();
            return;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '閉合登録選択イベント。
        Private Sub mnuAngleSelect_Click(Index As Integer)

            On Error GoTo ErrorHandler
    
            '基線ベクトルコレクション。
            Dim clsChainList As ChainList
            Set clsChainList = GetDocument().NetworkModel.BaseLineVectorHead
            Do While Not clsChainList Is Nothing
                If StrComp(Hex$(GetPointer(clsChainList.Element)), mnuAngleSelect(Index).Tag) = 0 Then
                    'オブジェクトクリックイベント。
                    RaiseEvent ClickObject(clsChainList.Element, vbLeftButton, 0)
                    Exit Do
                End If
                Set clsChainList = clsChainList.NextList
            Loop

            Exit Sub

        ErrorHandler:
            Call mdlMain.ErrorExit


        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'閉合登録選択イベント。
        private void mnuAngleSelect_Click(int Index)
        {
            try
            {
                //'基線ベクトルコレクション。
#if false
                /*
                 *************************** 修正要 sakai
                */
                //Set clsChainList = GetDocument().NetworkModel.BaseLineVectorHead
                ChainList clsChainList = m_clsMdlMain.GetDocument().NetworkModel().BaseLineVectorHead();
                while (clsChainList != null)
                {
                    if (StrComp((GetPointer(clsChainList.Element)), mnuAngleSelect(Index).Tag) == 0)
                    {
                        //'オブジェクトクリックイベント。
                        RaiseEvent ClickObject(clsChainList.Element, vbLeftButton, 0);
                        break;
                    }
                    clsChainList = clsChainList.NextList();
                }
#endif
            }

            catch
            {
                m_clsMdlMain.ErrorExit();
                return;
            }
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '*******************************************************************************
        'メソッド

        '初期化。
        '
        '引き数：
        'nPlotType プロット種別。
        Public Sub Initialize(ByVal nPlotType As PLOTTYPE_ENUM)

            'ペン。
            m_hPenNorthArrow = CreatePen(PS_SOLID, NORTHARROW_WIDTH_LINE, NORTHARROW_COLOR_LINE)
    
            'ブラシ。
            Dim tLogBrush As LOGBRUSH
            tLogBrush.lbStyle = BS_SOLID
            tLogBrush.lbHatch = 0
            tLogBrush.lbColor = BACKGROUND_COLOR
            m_hBrushBK = CreateBrushIndirect(tLogBrush)
            tLogBrush.lbColor = PLOT_SCALEBAR_COLOR_1
            m_hBrushScale1 = CreateBrushIndirect(tLogBrush)
            tLogBrush.lbColor = PLOT_SCALEBAR_COLOR_2
            m_hBrushScale2 = CreateBrushIndirect(tLogBrush)


            Call m_clsPlotNative.Initialize(nPlotType)

        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '*******************************************************************************
        'メソッド
        
        '初期化。
        '
        '引き数：
        'nPlotType  プロット種別。
        'MdlMain    MdlMainインスタンス
        */
        public void Initialize(PLOTTYPE_ENUM nPlotType, MdlMain clsMdlMain)
        {
#if true
            //'ペン。
            m_hPenNorthArrow = new long();
            m_hPenNorthArrow = CreatePen((int)PS_SOLID, (int)NORTHARROW_WIDTH_LINE, (uint)NORTHARROW_COLOR_LINE);

            //'ブラシ。
            LOGBRUSH tLogBrush = new LOGBRUSH();
            tLogBrush.lbStyle = BS_SOLID;
            tLogBrush.lbHatch = 0;
            tLogBrush.lbColor = BACKGROUND_COLOR;
            m_hBrushBK = CreateBrushIndirect(ref tLogBrush);
            tLogBrush.lbColor = PLOT_SCALEBAR_COLOR_1;
            m_hBrushScale1 = CreateBrushIndirect(ref tLogBrush);
            tLogBrush.lbColor = PLOT_SCALEBAR_COLOR_2;
            m_hBrushScale2 = CreateBrushIndirect(ref tLogBrush);
#endif
            m_clsMdlPlotProc = new MdlPlotProc();                               //'プロット画面プロシージャ
            m_clsPlotPort = new PlotPort(clsMdlMain, m_clsMdlPlotProc);         //プロット領域。
            m_clsPlotNative = new PlotNative(clsMdlMain, m_clsMdlPlotProc);     //'プロットネイティブ。
            m_clsEmphasisHead = new ChainList();                                //'強調オブジェクトリストのヘッダー。要素が ObservationPoint の場合それは代表観測点である。
            MdlNSUtility clsMdlNSUtility = new MdlNSUtility();
            m_clsMdlMain = clsMdlMain;                                          //MdlMainインスタンス

            m_clsPlotNative.Initialize(nPlotType);
            return;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '描画座標設定要求フラグを設定する。
        Public Sub SetUpdateDevicePoint()
            m_bDevicePoint = True
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'描画座標設定要求フラグを設定する。
        public void SetUpdateDevicePoint()
        {
            m_bDevicePoint = true;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '描画スケールを画面サイズに合わせる。
        '
        '論理エリア(配置図)全体が画面に表示されるように描画スケールを設定する。
        Public Sub FitOnView()

            Dim bDrawPermission As Boolean
            bDrawPermission = m_bDrawPermission
            m_bDrawPermission = False
    
            '描画スケール。
            If (picView.ScaleWidth / m_clsPlotPort.LogicalWidth) < (picView.ScaleHeight / m_clsPlotPort.LogicalHeight) Then
                m_clsPlotPort.DrawScale = picView.ScaleWidth / m_clsPlotPort.LogicalWidth
            Else
                m_clsPlotPort.DrawScale = picView.ScaleHeight / m_clsPlotPort.LogicalHeight
            End If


            m_clsPlotPort.DeviceLeft = (m_clsPlotPort.LogicalWidth * m_clsPlotPort.DrawScale - picMemDC.ScaleWidth) * 0.5
            m_clsPlotPort.DeviceTop = (m_clsPlotPort.LogicalHeight * m_clsPlotPort.DrawScale - picMemDC.ScaleHeight) * 0.5
    
            '表示範囲の更新。
            Call UpdateViewArea


            m_clsPlotPort.ViewLeft = (m_clsPlotPort.LogicalWidth * m_clsPlotPort.DrawScale - m_clsPlotPort.ViewWidth) * 0.5
            m_clsPlotPort.ViewTop = (m_clsPlotPort.LogicalHeight * m_clsPlotPort.DrawScale - m_clsPlotPort.ViewHeight) * 0.5
    
            '描画座標再設定要求。
            Call SetUpdateDevicePoint


            m_bDrawPermission = bDrawPermission


        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '描画スケールを画面サイズに合わせる。
        '
        '論理エリア(配置図)全体が画面に表示されるように描画スケールを設定する。
        */
        public void FitOnView()
        {
            bool bDrawPermission;
            bDrawPermission = m_bDrawPermission;
            m_bDrawPermission = false;


            //'描画スケール。
            if ((picView.Width / m_clsPlotPort.LogicalWidth) < (picView.Height / m_clsPlotPort.LogicalHeight))
            {
                m_clsPlotPort.DrawScale = picView.Width / m_clsPlotPort.LogicalWidth;
            }
            else
            {
                m_clsPlotPort.DrawScale = picView.Height / m_clsPlotPort.LogicalHeight;
            }

            m_clsPlotPort.DeviceLeft = (m_clsPlotPort.LogicalWidth * m_clsPlotPort.DrawScale - picMemDC.Width) * 0.5;
            m_clsPlotPort.DeviceTop = (m_clsPlotPort.LogicalHeight * m_clsPlotPort.DrawScale - picMemDC.Height) * 0.5;

            //'表示範囲の更新。
            UpdateViewArea();

            m_clsPlotPort.ViewLeft = (m_clsPlotPort.LogicalWidth * m_clsPlotPort.DrawScale - m_clsPlotPort.ViewWidth) * 0.5;
            m_clsPlotPort.ViewTop = (m_clsPlotPort.LogicalHeight * m_clsPlotPort.DrawScale - m_clsPlotPort.ViewHeight) * 0.5;

            //'描画座標再設定要求。
            SetUpdateDevicePoint();

            m_bDrawPermission = bDrawPermission;
            return;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'ビューのスケールを等倍変更する。
        '
        '描画スケールを2倍、もしくは2分の1にする。
        '
        '引き数：
        'bEnlarge 拡大フラグ。True=拡大。False=縮小。
        'nX 中心位置(デバイス座標、ピクセル)。
        'nY 中心位置(デバイス座標、ピクセル)。
        Public Sub ScaleViewMultiple(ByVal bEnlarge As Boolean, ByVal nX As Single, ByVal nY As Single)

            Dim bDrawPermission As Boolean
            bDrawPermission = m_bDrawPermission
            m_bDrawPermission = False
    
            '変更後の中心座標。
            Dim nCenterLeft As Double
            Dim nCenterTop As Double
            nCenterLeft = (nX + m_clsPlotPort.DeviceLeft) / m_clsPlotPort.DrawScale
            nCenterTop = (nY + m_clsPlotPort.DeviceTop) / m_clsPlotPort.DrawScale
    
            '等倍スケール。
            If bEnlarge Then
                '拡大。
                m_clsPlotPort.DrawScale = m_clsPlotPort.DrawScale* 2
            Else
                '縮小。
                m_clsPlotPort.DrawScale = m_clsPlotPort.DrawScale* 0.5
            End If
    
            'スケールを補正し、センタリングする。
            Call ScaleViewCentering(nCenterLeft, nCenterTop)


            m_bDrawPermission = bDrawPermission

        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        'ビューのスケールを等倍変更する。
        '
        '描画スケールを2倍、もしくは2分の1にする。
        '
        '引き数：
        'bEnlarge 拡大フラグ。True=拡大。False=縮小。
        'nX 中心位置(デバイス座標、ピクセル)。
        'nY 中心位置(デバイス座標、ピクセル)。
        */
        public void ScaleViewMultiple(bool bEnlarge, float nX, float nY)
        {
            bool bDrawPermission;
            bDrawPermission = m_bDrawPermission;
            m_bDrawPermission = false;

            //'変更後の中心座標。
            double nCenterLeft;
            double nCenterTop;
            nCenterLeft = (nX + m_clsPlotPort.DeviceLeft) / m_clsPlotPort.DrawScale;
            nCenterTop = (nY + m_clsPlotPort.DeviceTop) / m_clsPlotPort.DrawScale;

            //'等倍スケール。
            if (bEnlarge)
            {
                //'拡大。
                m_clsPlotPort.DrawScale = m_clsPlotPort.DrawScale * 2;
            }
            else
            {
                //'縮小。
                m_clsPlotPort.DrawScale = m_clsPlotPort.DrawScale * 0.5;
            }

            //'スケールを補正し、センタリングする。
            ScaleViewCentering(nCenterLeft, nCenterTop);

            m_bDrawPermission = bDrawPermission;
            return;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'ビューのスケールを任意の範囲に合わせる。
        '
        '拡大の場合。指定された範囲が画面いっぱいに表示されるように描画スケールを変更する。
        '縮小の場合。今画面に表示されている範囲が、指定された範囲いっぱいに表示されるように描画スケールを変更する。
        '
        '引き数：
        'bEnlarge 拡大フラグ。True=拡大。False=縮小。
        'nSX 範囲左辺(デバイス座標、ピクセル)。
        'nSY 範囲上辺(デバイス座標、ピクセル)。
        'nEX 範囲右辺(デバイス座標、ピクセル)。
        'nEY 範囲下辺(デバイス座標、ピクセル)。
        Public Sub ScaleViewRange(ByVal bEnlarge As Boolean, ByVal nSX As Single, ByVal nSY As Single, ByVal nEX As Single, ByVal nEY As Single)

            Dim bDrawPermission As Boolean
            bDrawPermission = m_bDrawPermission
            m_bDrawPermission = False
    
            '変更後の中心座標。
            Dim nCenterLeft As Double
            Dim nCenterTop As Double
            nCenterLeft = ((nSX + nEX) * 0.5 + m_clsPlotPort.DeviceLeft) / m_clsPlotPort.DrawScale
            nCenterTop = ((nSY + nEY) * 0.5 + m_clsPlotPort.DeviceTop) / m_clsPlotPort.DrawScale


            If bEnlarge Then
                '拡大。
                If(picView.ScaleWidth / (nEX - nSX) * m_clsPlotPort.DrawScale) < (picView.ScaleHeight / (nEY - nSY) * m_clsPlotPort.DrawScale) Then
                    m_clsPlotPort.DrawScale = picView.ScaleWidth / (nEX - nSX) * m_clsPlotPort.DrawScale
                Else
                    m_clsPlotPort.DrawScale = picView.ScaleHeight / (nEY - nSY) * m_clsPlotPort.DrawScale
                End If

            Else
                '縮小。
                If((nEX - nSX) / picView.ScaleWidth* m_clsPlotPort.DrawScale) < ((nEY - nSY) / picView.ScaleHeight* m_clsPlotPort.DrawScale) Then
                    m_clsPlotPort.DrawScale = (nEX - nSX) / picView.ScaleWidth* m_clsPlotPort.DrawScale
                Else
                    m_clsPlotPort.DrawScale = (nEY - nSY) / picView.ScaleHeight * m_clsPlotPort.DrawScale
                End If
            End If
    
            'スケールを補正し、センタリングする。
            Call ScaleViewCentering(nCenterLeft, nCenterTop)

            m_bDrawPermission = bDrawPermission


        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        'ビューのスケールを任意の範囲に合わせる。
        '
        '拡大の場合。指定された範囲が画面いっぱいに表示されるように描画スケールを変更する。
        '縮小の場合。今画面に表示されている範囲が、指定された範囲いっぱいに表示されるように描画スケールを変更する。
        '
        '引き数：
        'bEnlarge 拡大フラグ。True=拡大。False=縮小。
        'nSX 範囲左辺(デバイス座標、ピクセル)。
        'nSY 範囲上辺(デバイス座標、ピクセル)。
        'nEX 範囲右辺(デバイス座標、ピクセル)。
        'nEY 範囲下辺(デバイス座標、ピクセル)。
        */
        public void ScaleViewRange(bool bEnlarge, float nSX, float nSY, float nEX, float nEY)
        {
            bool bDrawPermission;
            bDrawPermission = m_bDrawPermission;
            m_bDrawPermission = false;

            //'変更後の中心座標。
            double nCenterLeft;
            double nCenterTop;
            nCenterLeft = ((nSX + nEX) * 0.5 + m_clsPlotPort.DeviceLeft) / m_clsPlotPort.DrawScale;
            nCenterTop = ((nSY + nEY) * 0.5 + m_clsPlotPort.DeviceTop) / m_clsPlotPort.DrawScale;

            if (bEnlarge)
            {
                //'拡大。
                if ((picView.Width / (nEX - nSX) * m_clsPlotPort.DrawScale) < (picView.Height / (nEY - nSY) * m_clsPlotPort.DrawScale))
                {
                    m_clsPlotPort.DrawScale = picView.Width / (nEX - nSX) * m_clsPlotPort.DrawScale;
                }
                else
                {
                    m_clsPlotPort.DrawScale = picView.Height / (nEY - nSY) * m_clsPlotPort.DrawScale;
                }
            }
            else
            {
                //'縮小。
                if (((nEX - nSX) / picView.Width * m_clsPlotPort.DrawScale) < ((nEY - nSY) / picView.Height * m_clsPlotPort.DrawScale))
                {
                    m_clsPlotPort.DrawScale = (nEX - nSX) / picView.Width * m_clsPlotPort.DrawScale;
                }
                else
                {
                    m_clsPlotPort.DrawScale = (nEY - nSY) / picView.Height * m_clsPlotPort.DrawScale;
                }
            }

            //'スケールを補正し、センタリングする。
            ScaleViewCentering(nCenterLeft, nCenterTop);

            m_bDrawPermission = bDrawPermission;
            return;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'ビューのスケールを補正し、センタリングする。
        '
        '指定された座標が画面中央にくるようにデバイス位置を設定する。
        '
        '引き数：
        'nCenterLeft 中心座標(実座標、ｍ)。
        'nCenterLeft 中心座標(実座標、ｍ)。
        Public Sub ScaleViewCentering(ByVal nCenterLeft As Double, ByVal nCenterTop As Double)

            Dim bDrawPermission As Boolean
            bDrawPermission = m_bDrawPermission
            m_bDrawPermission = False
    
            'スケールを補正。
            m_clsPlotPort.DrawScale = GetCorrectDrawScale(m_clsPlotPort.DrawScale)
    
            '表示範囲の更新。
            Call UpdateViewArea
    
            'センタリング。
            m_clsPlotPort.DeviceLeft = nCenterLeft* m_clsPlotPort.DrawScale - picMemDC.ScaleWidth* 0.5
            m_clsPlotPort.DeviceTop = nCenterTop * m_clsPlotPort.DrawScale - picMemDC.ScaleHeight * 0.5
            m_clsPlotPort.ViewLeft = nCenterLeft * m_clsPlotPort.DrawScale - m_clsPlotPort.ViewWidth * 0.5
            m_clsPlotPort.ViewTop = nCenterTop * m_clsPlotPort.DrawScale - m_clsPlotPort.ViewHeight * 0.5
    
            '描画範囲が論理範囲からはみ出さないように補正。
            Call m_clsPlotPort.CorrectDrawArea
    
            'スクロール位置。
            If scrHori.Visible Then scrHori.Value = m_clsPlotPort.ViewLeft / (m_clsPlotPort.LogicalWidth * m_clsPlotPort.DrawScale - m_clsPlotPort.ViewWidth) * scrHori.Max
            If scrVert.Visible Then scrVert.Value = m_clsPlotPort.ViewTop / (m_clsPlotPort.LogicalHeight * m_clsPlotPort.DrawScale - m_clsPlotPort.ViewHeight) * scrVert.Max
    
            '描画座標再設定要求。
            Call SetUpdateDevicePoint


            m_bDrawPermission = bDrawPermission


        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        'ビューのスケールを補正し、センタリングする。
        '
        '指定された座標が画面中央にくるようにデバイス位置を設定する。
        '
        '引き数：
        'nCenterLeft 中心座標(実座標、ｍ)。
        'nCenterLeft 中心座標(実座標、ｍ)。
        */
        public void ScaleViewCentering(double nCenterLeft, double nCenterTop)
        {
            bool bDrawPermission;
            bDrawPermission = m_bDrawPermission;
            m_bDrawPermission = false;

            //'スケールを補正。
            m_clsPlotPort.DrawScale = GetCorrectDrawScale(m_clsPlotPort.DrawScale);

            //'表示範囲の更新。
            UpdateViewArea();

            //'センタリング。
            m_clsPlotPort.DeviceLeft = nCenterLeft * m_clsPlotPort.DrawScale - picMemDC.Width * 0.5;
            m_clsPlotPort.DeviceTop = nCenterTop * m_clsPlotPort.DrawScale - picMemDC.Height * 0.5;
            m_clsPlotPort.ViewLeft = nCenterLeft * m_clsPlotPort.DrawScale - m_clsPlotPort.ViewWidth * 0.5;
            m_clsPlotPort.ViewTop = nCenterTop * m_clsPlotPort.DrawScale - m_clsPlotPort.ViewHeight * 0.5;

            //'描画範囲が論理範囲からはみ出さないように補正。
            m_clsPlotPort.CorrectDrawArea();


            //'スクロール位置。
            if (scrHori.Visible)
            {
                scrHori.Value = (int)(m_clsPlotPort.ViewLeft / (m_clsPlotPort.LogicalWidth * m_clsPlotPort.DrawScale - m_clsPlotPort.ViewWidth) * scrHori.Maximum);
            }
            if (scrVert.Visible)
            {
                scrVert.Value = (int)(m_clsPlotPort.ViewTop / (m_clsPlotPort.LogicalHeight * m_clsPlotPort.DrawScale - m_clsPlotPort.ViewHeight) * scrVert.Maximum);
            }

            //'描画座標再設定要求。
            SetUpdateDevicePoint();

            m_bDrawPermission = bDrawPermission;
            return;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '描画スケールを補正した値を取得する。
        '
        '描画スケールが限界を超えないように補正する。
        '
        '引き数：
        'nDrawScale 描画スケール。平面直角座標(実座標、ｍ)をデバイス座標(ピクセル)へ変換する係数。
        '
        '戻り値：補正した描画スケール。
        Public Function GetCorrectDrawScale(ByVal nDrawScale As Double) As Double
            GetCorrectDrawScale = nDrawScale
            '縮小率の制限。
            If GetCorrectDrawScale<FLT_EPSILON * 2 Then GetCorrectDrawScale = FLT_EPSILON * 2
            '拡大率の制限。
            If m_clsPlotPort.LogicalWidth* GetCorrectDrawScale > LONG_MAX* 0.5 Then GetCorrectDrawScale = LONG_MAX * 0.5 / m_clsPlotPort.LogicalWidth
            If m_clsPlotPort.LogicalHeight* GetCorrectDrawScale > LONG_MAX* 0.5 Then GetCorrectDrawScale = LONG_MAX * 0.5 / m_clsPlotPort.LogicalHeight
            If GetSmallChange(1, m_clsPlotPort.LogicalWidth, GetCorrectDrawScale) < 1 Then GetCorrectDrawScale = SMALL_CHANGE / m_clsPlotPort.LogicalWidth * SHRT_MAX
            If GetSmallChange(1, m_clsPlotPort.LogicalHeight, GetCorrectDrawScale) < 1 Then GetCorrectDrawScale = SMALL_CHANGE / m_clsPlotPort.LogicalHeight * SHRT_MAX
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '描画スケールを補正した値を取得する。
        '
        '描画スケールが限界を超えないように補正する。
        '
        '引き数：
        'nDrawScale 描画スケール。平面直角座標(実座標、ｍ)をデバイス座標(ピクセル)へ変換する係数。
        '
        '戻り値：補正した描画スケール。
        */
        public double GetCorrectDrawScale(double nDrawScale)
        {
            double w_GetCorrectDrawScale = nDrawScale;
            //'縮小率の制限。
            if (w_GetCorrectDrawScale < FLT_EPSILON * 2)
            {
                w_GetCorrectDrawScale = FLT_EPSILON * 2;
            }
            //'拡大率の制限。
            if ((m_clsPlotPort.LogicalWidth * w_GetCorrectDrawScale) > (LONG_MAX * 0.5))
            {
                w_GetCorrectDrawScale = LONG_MAX * 0.5 / m_clsPlotPort.LogicalWidth;
            }
            if ((m_clsPlotPort.LogicalHeight * w_GetCorrectDrawScale) > (LONG_MAX * 0.5))
            {
                w_GetCorrectDrawScale = LONG_MAX * 0.5 / m_clsPlotPort.LogicalHeight;
            }
            if (GetSmallChange(1, m_clsPlotPort.LogicalWidth, w_GetCorrectDrawScale) < 1)
            {
                w_GetCorrectDrawScale = SMALL_CHANGE / m_clsPlotPort.LogicalWidth * SHRT_MAX;
            }
            if (GetSmallChange(1, m_clsPlotPort.LogicalHeight, w_GetCorrectDrawScale) < 1)
            {
                w_GetCorrectDrawScale = SMALL_CHANGE / m_clsPlotPort.LogicalHeight * SHRT_MAX;
            }
            return w_GetCorrectDrawScale;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '論理エリアを更新する。
        '
        '配置図の描画するべき領域として、観測点や基線ベクトル等のオブジェクト群に外接する四角形を論理範囲として設定する。外接といってもすこし余白を持たせる。
        '
        '引き数：
        'bRescale 再スケーリングフラグ。True=描画スケールを初期化する。False=描画スケールは変更しない。
        Public Sub UpdateLogicalDrawArea(ByVal bRescale As Boolean)

            Dim bDrawPermission As Boolean
            bDrawPermission = m_bDrawPermission
            m_bDrawPermission = False
    
            '論理範囲を更新する。
            Call m_clsPlotPort.UpdateLogicalDrawArea


            If bRescale Then
                'スケールを元に戻す。
                Call FitOnView
            Else
                '表示範囲の更新。
                Call UpdateViewArea
                '描画範囲が論理範囲からはみ出さないように補正。
                Call m_clsPlotPort.CorrectDrawArea
                'スクロールバーの位置を設定。
                If scrHori.Visible Then scrHori.Value = m_clsPlotPort.ViewLeft / (m_clsPlotPort.LogicalWidth * m_clsPlotPort.DrawScale - m_clsPlotPort.ViewWidth) * scrHori.Max
                If scrVert.Visible Then scrVert.Value = m_clsPlotPort.ViewTop / (m_clsPlotPort.LogicalHeight * m_clsPlotPort.DrawScale - m_clsPlotPort.ViewHeight) * scrVert.Max
            End If
    
            '描画座標再設定要求。
            Call SetUpdateDevicePoint


            m_bDrawPermission = bDrawPermission


        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '論理エリアを更新する。
        '
        '配置図の描画するべき領域として、観測点や基線ベクトル等のオブジェクト群に外接する四角形を論理範囲として設定する。外接といってもすこし余白を持たせる。
        '
        '引き数：
        'bRescale 再スケーリングフラグ。True=描画スケールを初期化する。False=描画スケールは変更しない。
        */
        public void UpdateLogicalDrawArea(bool bRescale)
        {
            bool bDrawPermission;
            bDrawPermission = m_bDrawPermission;
            m_bDrawPermission = false;

            //'論理範囲を更新する。
            m_clsPlotPort.UpdateLogicalDrawArea();

            if (bRescale)
            {
                //'スケールを元に戻す。
                FitOnView();
            }
            else
            {
                //'表示範囲の更新。
                UpdateViewArea();
                //'描画範囲が論理範囲からはみ出さないように補正。
                m_clsPlotPort.CorrectDrawArea();
                //'スクロールバーの位置を設定。
                if (scrHori.Visible)
                {
                    scrHori.Value = (int)(m_clsPlotPort.ViewLeft / (m_clsPlotPort.LogicalWidth * m_clsPlotPort.DrawScale - m_clsPlotPort.ViewWidth) * scrHori.Maximum);
                }
                if (scrVert.Visible)
                {
                    scrVert.Value = (int)(m_clsPlotPort.ViewTop / (m_clsPlotPort.LogicalHeight * m_clsPlotPort.DrawScale - m_clsPlotPort.ViewHeight) * scrVert.Maximum);
                }
            }

            //'描画座標再設定要求。
            SetUpdateDevicePoint();

            m_bDrawPermission = bDrawPermission;
            return;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '再描画する。
        '
        'バックバッファ(picMemDC)に描画する。
        '画面への表示は行われない。表示は Refresh メソッドで行う。
        Public Sub Redraw()
            Call Draw(picMemDC)
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '再描画する。
        '
        'バックバッファ(picMemDC)に描画する。
        '画面への表示は行われない。表示は Refresh メソッドで行う。
        */
#if false
        public void Redraw()
        {
            Draw(ref picMemDC);
            return;
        }
#else
        public void Redraw()
        {
            Draw(ref m_btmp);
            return;
        }
#endif
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'リフレッシュする。
        '
        '画面の表示を更新する。
        '基本的に描画内容は変わらない。再描画は Redraw　メソッドで行う。
        Public Sub Refresh()
            Call Display
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        'リフレッシュする。
        '
        '画面の表示を更新する。
        '基本的に描画内容は変わらない。再描画は Redraw　メソッドで行う。
        */
#if false
        public void Refresh()
        {
            Display();
            return;
        }
#else
        public void Refresh()
        {
            Display(m_btmp);
            return;
        }
#endif
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '強調オブジェクトをすべて削除する。
        Public Sub ClearEmphasis()
            Call m_clsEmphasisHead.RemoveAll
            Set m_clsEmphasisHead = New ChainList
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'強調オブジェクトをすべて削除する。
        public void ClearEmphasis()
        {
            m_clsEmphasisHead.RemoveAll();
            /*
             * ClearEmphasis() を読んだ後に、new を入れてね！
             * 
            ChainList m_clsEmphasisHead = new ChainList();
            */
            return;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '強調オブジェクトを追加する。
        '
        '引き数：
        'objEmphasis 強調オブジェクトとして追加するオブジェクト。
        Public Sub AddEmphasis(ByVal objEmphasis As Object)
            Call m_clsEmphasisHead.InsertNext(objEmphasis)
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '強調オブジェクトを追加する。
        '
        '引き数：
        'objEmphasis 強調オブジェクトとして追加するオブジェクト。
        */
        public void AddEmphasis(object objEmphasis)
        {
            m_clsEmphasisHead.InsertNext(objEmphasis);
            return;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '閉合登録開始。
        '
        '閉合画面の場合、最初にこのメソッドを呼ぶ。
        '最後に EndRegistAngle を呼ぶ。
        '
        '引き数：
        'clsRegistAngle RegistAngle オブジェクト。
        Public Sub BeginRegistAngle(ByVal clsRegistAngle As RegistAngle)
            Set m_clsRegistAngle = clsRegistAngle
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '閉合登録開始。
        '
        '閉合画面の場合、最初にこのメソッドを呼ぶ。
        '最後に EndRegistAngle を呼ぶ。
        '
        '引き数：
        'clsRegistAngle RegistAngle オブジェクト。
        */
#if false
        /*
            *************************** 修正要 sakai
        */
        public void BeginRegistAngle(RegistAngle clsRegistAngle)
        {
            m_clsRegistAngle = clsRegistAngle;
            return;
        }
#endif
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '閉合登録終了。
        '
        '閉合画面の場合、最初にこのメソッドを呼ぶ。
        '最後に EndRegistAngle を呼ぶ。
        Public Sub EndRegistAngle()
            Set m_clsRegistAngle = Nothing
            Call Refresh
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '閉合登録終了。
        '
        '閉合画面の場合、最初にこのメソッドを呼ぶ。
        '最後に EndRegistAngle を呼ぶ。
        */
        public void EndRegistAngle()
        {
            m_clsRegistAngle = null;
            Refresh();
            return;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '2022/07/21 Hitz H.Nakamura *******************************************************************
        'ファイルのドロップを追加。

        'ドロップモードの設定。
        Public Sub SetDropMode(ByVal bMode As Boolean)
            If bMode Then
                picView.OLEDropMode = 1
            Else
                picView.OLEDropMode = 0
            End If
        End Sub

        '**********************************************************************************************
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'ドロップモードの設定。
        public void SetDropMode(bool bMode)
        {
            if (bMode)
            {
                picView.AllowDrop = true;
            }
            else
            {
                picView.AllowDrop = false;
            }
            return;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '*******************************************************************************
        'インプリメンテーション

        '表示範囲の更新。
        '
        '画面サイズが変わったり、描画スケールが変わったりしたらこのメソッドを呼ぶ。
        'ビューのサイズに従いスクロールバーの設定を行う。
        Private Sub UpdateViewArea()

            'ビューのサイズ。スクロールバーの有無。
            If picView.ScaleWidth<m_clsPlotPort.LogicalWidth* m_clsPlotPort.DrawScale And Not GetDocument().NetworkModel.RepresentPointHead Is Nothing Then
                '横スクロールあり。
                m_clsPlotPort.ViewHeight = picView.ScaleHeight - scrHori.Height
                scrHori.Visible = True
                If m_clsPlotPort.ViewHeight<m_clsPlotPort.LogicalHeight* m_clsPlotPort.DrawScale Then
                    '縦スクロールあり。
                    m_clsPlotPort.ViewWidth = picView.ScaleWidth - scrVert.Width
                    scrVert.Visible = True
                Else
                    '縦スクロールなし。
                    m_clsPlotPort.ViewWidth = picView.ScaleWidth
                    scrVert.Visible = False
                End If
            ElseIf picView.ScaleHeight<m_clsPlotPort.LogicalHeight* m_clsPlotPort.DrawScale And Not GetDocument().NetworkModel.RepresentPointHead Is Nothing Then
                '縦スクロールあり。
                m_clsPlotPort.ViewWidth = picView.ScaleWidth - scrVert.Width
                scrVert.Visible = True
                If m_clsPlotPort.ViewWidth<m_clsPlotPort.LogicalWidth* m_clsPlotPort.DrawScale Then
                    '横スクロールあり。
                    m_clsPlotPort.ViewHeight = picView.ScaleHeight - scrHori.Height
                    scrHori.Visible = True
                Else
                    '横スクロールなし。
                    m_clsPlotPort.ViewHeight = picView.ScaleHeight
                    scrHori.Visible = False
                End If
            Else
                '縦横スクロールなし。
                m_clsPlotPort.ViewWidth = picView.ScaleWidth
                m_clsPlotPort.ViewHeight = picView.ScaleHeight
                scrHori.Visible = False
                scrVert.Visible = False
            End If
    
            '補正。
            If m_clsPlotPort.ViewWidth< 1 Then m_clsPlotPort.ViewWidth = 1
            If m_clsPlotPort.ViewHeight< 1 Then m_clsPlotPort.ViewHeight = 1
    
            'スクロールバーサイズ。
            Dim nSmallChange As Long
            Dim nLargeChange As Long
            Dim nMax As Long
            If scrHori.Visible Then
                scrHori.Top = picView.ScaleTop + m_clsPlotPort.ViewHeight
                scrHori.Width = m_clsPlotPort.ViewWidth
                nMax = (m_clsPlotPort.LogicalWidth* m_clsPlotPort.DrawScale - m_clsPlotPort.ViewWidth) / (m_clsPlotPort.LogicalWidth* m_clsPlotPort.DrawScale) * SHRT_MAX
                If nMax< 1 Then nMax = 1
                nSmallChange = GetSmallChange(1, m_clsPlotPort.LogicalWidth, m_clsPlotPort.DrawScale)
                If nSmallChange< 1 Then nSmallChange = 1
                If SHRT_MAX < nSmallChange Then nSmallChange = SHRT_MAX
                nLargeChange = Fix(m_clsPlotPort.ViewWidth / (m_clsPlotPort.LogicalWidth * m_clsPlotPort.DrawScale) * SHRT_MAX / scrHori.SmallChange) * scrHori.SmallChange
                If nLargeChange < scrHori.SmallChange Then nLargeChange = scrHori.SmallChange
                If SHRT_MAX < nLargeChange Then nLargeChange = SHRT_MAX
                scrHori.Max = nMax
                scrHori.SmallChange = nSmallChange
                scrHori.LargeChange = nLargeChange
            End If
            If scrVert.Visible Then
                scrVert.Left = picView.ScaleLeft + m_clsPlotPort.ViewWidth
                scrVert.Height = m_clsPlotPort.ViewHeight
                nMax = (m_clsPlotPort.LogicalHeight * m_clsPlotPort.DrawScale - m_clsPlotPort.ViewHeight) / (m_clsPlotPort.LogicalHeight * m_clsPlotPort.DrawScale) * SHRT_MAX
                If nMax < 1 Then nMax = 1
                nSmallChange = GetSmallChange(1, m_clsPlotPort.LogicalHeight, m_clsPlotPort.DrawScale)
                If nSmallChange < 1 Then nSmallChange = 1
                If SHRT_MAX < nSmallChange Then nSmallChange = SHRT_MAX
                nLargeChange = Fix(m_clsPlotPort.ViewHeight / (m_clsPlotPort.LogicalHeight * m_clsPlotPort.DrawScale) * SHRT_MAX / scrVert.SmallChange) * scrVert.SmallChange
                If nLargeChange < scrVert.SmallChange Then nLargeChange = scrVert.SmallChange
                If SHRT_MAX < nLargeChange Then nLargeChange = SHRT_MAX
                scrVert.Max = nMax
                scrVert.SmallChange = nSmallChange
                scrVert.LargeChange = nLargeChange
            End If


        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '*******************************************************************************
        'インプリメンテーション
        
        '表示範囲の更新。
        '
        '画面サイズが変わったり、描画スケールが変わったりしたらこのメソッドを呼ぶ。
        'ビューのサイズに従いスクロールバーの設定を行う。
        */
        private void UpdateViewArea()
        {
            //'ビューのサイズ。スクロールバーの有無。
            if (picView.Width < (int)(m_clsPlotPort.LogicalWidth * m_clsPlotPort.DrawScale) && (m_clsMdlMain.GetDocument().NetworkModel().RepresentPointHead() != null))
            {
                //'横スクロールあり。
                m_clsPlotPort.ViewHeight = picView.Height - scrHori.Height;
                scrHori.Visible = true;
                if (m_clsPlotPort.ViewHeight < (m_clsPlotPort.LogicalHeight * m_clsPlotPort.DrawScale))
                {
                    //'縦スクロールあり。
                    m_clsPlotPort.ViewWidth = picView.Width - scrVert.Width;
                    scrVert.Visible = true;
                }
                else
                {
                    //'縦スクロールなし。
                    m_clsPlotPort.ViewWidth = picView.Width;
                    scrVert.Visible = false;
                }
            }
            else if (picView.Height < (int)(m_clsPlotPort.LogicalHeight * m_clsPlotPort.DrawScale) && (m_clsMdlMain.GetDocument().NetworkModel().RepresentPointHead() != null))
            {
                //'縦スクロールあり。
                m_clsPlotPort.ViewWidth = picView.Width - scrVert.Width;
                scrVert.Visible = true;
                if (m_clsPlotPort.ViewWidth < (m_clsPlotPort.LogicalWidth * m_clsPlotPort.DrawScale))
                {
                    //'横スクロールあり。
                    m_clsPlotPort.ViewHeight = picView.Height - scrHori.Height;
                    scrHori.Visible = true;
                }
                else
                {
                    //'横スクロールなし。
                    m_clsPlotPort.ViewHeight = picView.Height;
                    scrHori.Visible = false;
                }
            }
            else
            {
                //'縦横スクロールなし。
                m_clsPlotPort.ViewWidth = picView.Width;
                m_clsPlotPort.ViewHeight = picView.Height;
                scrHori.Visible = false;
                scrVert.Visible = false;
            }


            //'補正。
            if (m_clsPlotPort.ViewWidth < 1)
            {
                m_clsPlotPort.ViewWidth = 1;
            }
            if (m_clsPlotPort.ViewHeight < 1)
            {
                m_clsPlotPort.ViewHeight = 1;
            }


            //'スクロールバーサイズ。
            long nSmallChange;
            long nLargeChange;
            long nMax;
            if (scrHori.Visible)
            {
                scrHori.Top = (int)(picView.Top + m_clsPlotPort.ViewHeight);
                scrHori.Width = (int)m_clsPlotPort.ViewWidth;
                nMax = (long)((m_clsPlotPort.LogicalWidth * m_clsPlotPort.DrawScale - m_clsPlotPort.ViewWidth) / (m_clsPlotPort.LogicalWidth * m_clsPlotPort.DrawScale) * SHRT_MAX);
                if (nMax < 1)
                {
                    nMax = 1;
                }
                nSmallChange = (long)GetSmallChange(1, m_clsPlotPort.LogicalWidth, m_clsPlotPort.DrawScale);
                if (nSmallChange < 1)
                {
                    nSmallChange = 1;
                }
                if (SHRT_MAX < nSmallChange)
                {
                    nSmallChange = SHRT_MAX;
                }
                nLargeChange = (long)Math.Truncate(m_clsPlotPort.ViewWidth / (m_clsPlotPort.LogicalWidth * m_clsPlotPort.DrawScale) * SHRT_MAX / scrHori.SmallChange) * scrHori.SmallChange;
                if (nLargeChange < scrHori.SmallChange)
                {
                    nLargeChange = scrHori.SmallChange;
                }
                if (SHRT_MAX < nLargeChange)
                {
                    nLargeChange = SHRT_MAX;
                }
                scrHori.Maximum = (int)nMax;
                scrHori.SmallChange = (int)nSmallChange;
                scrHori.LargeChange = (int)nLargeChange;
            }
            if (scrVert.Visible)
            {
                scrVert.Left = (int)(picView.Left + m_clsPlotPort.ViewWidth);
                scrVert.Height = (int)m_clsPlotPort.ViewHeight;
                nMax = (long)((m_clsPlotPort.LogicalHeight * m_clsPlotPort.DrawScale - m_clsPlotPort.ViewHeight) / (m_clsPlotPort.LogicalHeight * m_clsPlotPort.DrawScale) * SHRT_MAX);
                if (nMax < 1)
                {
                    nMax = 1;
                }
                nSmallChange = (long)GetSmallChange(1, m_clsPlotPort.LogicalHeight, m_clsPlotPort.DrawScale);
                if (nSmallChange < 1)
                {
                    nSmallChange = 1;
                }
                if (SHRT_MAX < nSmallChange)
                {
                    nSmallChange = SHRT_MAX;
                }
                nLargeChange = (long)(Math.Truncate(m_clsPlotPort.ViewHeight / (m_clsPlotPort.LogicalHeight * m_clsPlotPort.DrawScale) * SHRT_MAX / scrVert.SmallChange) * scrVert.SmallChange);
                if (nLargeChange < scrVert.SmallChange)
                {
                    nLargeChange = scrVert.SmallChange;
                }
                if (SHRT_MAX < nLargeChange)
                {
                    nLargeChange = SHRT_MAX;
                }
                scrVert.Maximum = (int)nMax;
                scrVert.SmallChange = (int)nSmallChange;
                scrVert.LargeChange = (int)nLargeChange;
            }
            return;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'スクロールバーの単位スクロール量を取得する。
        '
        '指定されたパラメータでスクロールバーの単位スクロール量を計算する。
        '
        '引き数：
        'nTwipsPerPixel ピクセルあたりのTwips数。
        'nLogicalSize 論理エリアサイズ(ｍ)。
        'nDrawScale 描画スケール。平面直角座標(実座標、ｍ)をデバイス座標(ピクセル)へ変換する係数。
        '
        '戻り値：スクロールバーの単位スクロール量を返す。
        Private Function GetSmallChange(ByVal nTwipsPerPixel As Double, ByVal nLogicalSize As Double, ByVal nDrawScale As Double) As Double
            GetSmallChange = (SMALL_CHANGE* nTwipsPerPixel) / (nLogicalSize* nDrawScale)* SHRT_MAX
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        'スクロールバーの単位スクロール量を取得する。
        '
        '指定されたパラメータでスクロールバーの単位スクロール量を計算する。
        '
        '引き数：
        'nTwipsPerPixel ピクセルあたりのTwips数。
        'nLogicalSize 論理エリアサイズ(ｍ)。
        'nDrawScale 描画スケール。平面直角座標(実座標、ｍ)をデバイス座標(ピクセル)へ変換する係数。
        '
        '戻り値：スクロールバーの単位スクロール量を返す。
        */
        private double GetSmallChange(double nTwipsPerPixel, double nLogicalSize, double nDrawScale)
        {
            return SMALL_CHANGE * nTwipsPerPixel / (nLogicalSize * nDrawScale) * SHRT_MAX;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '表示。
        '
        '画面を表示する。
        'picMemDC(バックバッファ)の内容を picView(フロントバッファ)へコピーする。
        '強調オブジェクト等の変化の多い部分を直接 picView へ描画する。
        Private Sub Display()

            If Not m_bDrawPermission Then Exit Sub
            If Not m_bSetRedraw Then Exit Sub

            '砂時計。
            Dim clsWaitCursor As New WaitCursor
            Set clsWaitCursor.Object = picView

            picView.DrawMode = vbCopyPen

            'メモリデバイスコンテキストからコピー。
            Dim nScaleLeft As Long
            Dim nScaleTop As Long
            nScaleLeft = m_clsPlotPort.ViewLeft - m_clsPlotPort.DeviceLeft
            nScaleTop = m_clsPlotPort.ViewTop - m_clsPlotPort.DeviceTop
            picView.ScaleLeft = nScaleLeft
            picView.ScaleTop = nScaleTop
            Dim nDstLeft As Long
            Dim nDstTop As Long
            Dim nSrcLeft As Long
            Dim nSrcTop As Long
            nDstLeft = m_clsPlotPort.ViewLeft - m_clsPlotPort.DeviceLeft - nScaleLeft
            nDstTop = m_clsPlotPort.ViewTop - m_clsPlotPort.DeviceTop - nScaleTop
            nSrcLeft = m_clsPlotPort.ViewLeft - m_clsPlotPort.DeviceLeft
            nSrcTop = m_clsPlotPort.ViewTop - m_clsPlotPort.DeviceTop
            Call BitBlt(picView.hDC, nDstLeft, nDstTop, m_clsPlotPort.ViewWidth, m_clsPlotPort.ViewHeight, picMemDC.hDC, nSrcLeft, nSrcTop, SRCCOPY)
                Dim hOldPen As Long
                Dim hOldFont As Long
                Dim hOldBrush As Long
            hOldPen = GetCurrentObject(picView.hDC, OBJ_PEN)
            hOldFont = GetCurrentObject(picView.hDC, OBJ_FONT)
            hOldBrush = GetCurrentObject(picView.hDC, OBJ_BRUSH)


            On Error GoTo ErrorHandler


            Dim pnt As POINT
            Call SetViewportOrgEx(picView.hDC, -nScaleLeft, -nScaleTop, pnt)

            'ノースアロー。
            Call DrawNorthArrow(picView.hDC, m_clsPlotPort.ViewLeft - m_clsPlotPort.DeviceLeft, m_clsPlotPort.ViewTop - m_clsPlotPort.DeviceTop, 1 / Screen.TwipsPerPixelX, m_hPenNorthArrow, NORTHARROW_COLOR_SYMBOL)

            'スケールバー。
            Call DrawScalebar(picView.hDC, m_clsPlotPort.ViewLeft - m_clsPlotPort.DeviceLeft, m_clsPlotPort.ViewTop - m_clsPlotPort.DeviceTop, m_clsPlotPort.ViewWidth, m_clsPlotPort.ViewHeight, 1 / Screen.TwipsPerPixelX, 0, m_hBrushScale1, m_hBrushScale2, PLOT_SCALEBAR_COLOR_SYMBOL, m_clsPlotPort.DrawScale)

            'クリップ領域。
            Dim nClipLeft As Double
            Dim nClipTop As Double
            Dim nClipWidth As Double
            Dim nClipHeight As Double
            Call m_clsPlotPort.GetClipArea(nClipLeft, nClipTop, nClipWidth, nClipHeight)

            '観測点、基線ベクトルの描画。
            Call m_clsPlotNative.DrawObjectFilm(picView.hDC, m_clsPlotPort.DeviceLeft, m_clsPlotPort.DeviceTop, nClipLeft, nClipTop, nClipWidth, nClipHeight, 1 / Screen.TwipsPerPixelX, m_clsEmphasisHead, m_clsRegistAngle, m_clsRegistAngle Is Nothing)


            Call SetViewportOrgEx(picView.hDC, pnt.X, pnt.Y, pnt)


            Call SelectObject(picView.hDC, hOldPen)
            Call SelectObject(picView.hDC, hOldFont)
            Call SelectObject(picView.hDC, hOldBrush)


            On Error GoTo 0

            'スクロールバー隙間。
            If scrHori.Visible And scrVert.Visible Then
                picView.Line (scrVert.Left, scrHori.Top)-Step(scrVert.Width, scrHori.Height), GetSysColor(COLOR_3DFACE), BF
            Else
                '何も描画しなかった場合は強制的に表示させる。
                Call picView.Refresh
            End If

            Call clsWaitCursor.Back
                Exit Sub

        ErrorHandler:
            Call SelectObject(picView.hDC, hOldPen)
            Call SelectObject(picView.hDC, hOldFont)
            Call SelectObject(picView.hDC, hOldBrush)
            Call Err.Raise(Err.Number, Err.Source, Err.Description, Err.HelpFile, Err.HelpContext)

        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '表示。
        '
        '画面を表示する。
        'picMemDC(バックバッファ)の内容を picView(フロントバッファ)へコピーする。
        '強調オブジェクト等の変化の多い部分を直接 picView へ描画する。
        */
#if false
        private void Display()
        {
#else
        private void Display(Bitmap btmp)
        {
#endif
            if (!m_bDrawPermission)
            {
                return;
            }
            if (!m_bSetRedraw)
            {
                return;
            }

            //'砂時計。
            //WaitCursor clsWaitCursor = new WaitCursor;
            //clsWaitCursor.Object = picView;
            picView.Cursor = Cursors.WaitCursor;

#if false
            /*
                *************************** 修正要? sakai
            */
            picView.DrawMode = vbCopyPen;
#endif
#if false
            //'メモリデバイスコンテキストからコピー。
            long nScaleLeft;
            long nScaleTop;
            nScaleLeft = (long)(m_clsPlotPort.ViewLeft - m_clsPlotPort.DeviceLeft);
            nScaleTop = (long)(m_clsPlotPort.ViewTop - m_clsPlotPort.DeviceTop);
            picView.Left = (int)nScaleLeft;
            picView.Top = (int)nScaleTop;
            long nDstLeft;
            long nDstTop;
            long nSrcLeft;
            long nSrcTop;
            nDstLeft = (long)(m_clsPlotPort.ViewLeft - m_clsPlotPort.DeviceLeft - nScaleLeft);
            nDstTop = (long)(m_clsPlotPort.ViewTop - m_clsPlotPort.DeviceTop - nScaleTop);
            nSrcLeft = (long)(m_clsPlotPort.ViewLeft - m_clsPlotPort.DeviceLeft);
            nSrcTop = (long)(m_clsPlotPort.ViewTop - m_clsPlotPort.DeviceTop);
            BitBlt(picView.Handle, (int)nDstLeft, (int)nDstTop, (int)m_clsPlotPort.ViewWidth, (int)m_clsPlotPort.ViewHeight, picMemDC.Handle, (int)nSrcLeft, (int)nSrcTop, (TernaryRasterOperations)SRCCOPY);
            long hOldPen;
            long hOldFont;
            long hOldBrush;
            hOldPen = GetCurrentObject(picView.Handle, (int)OBJ_PEN);
            hOldFont = GetCurrentObject(picView.Handle, (int)OBJ_FONT);
            hOldBrush = GetCurrentObject(picView.Handle, (int)OBJ_BRUSH);
#else
            long nScaleLeft;
            long nScaleTop;
            nScaleLeft = (long)(m_clsPlotPort.ViewLeft - m_clsPlotPort.DeviceLeft);
            nScaleTop = (long)(m_clsPlotPort.ViewTop - m_clsPlotPort.DeviceTop);
#endif
            try
            {
                POINT pnt;
                SetViewportOrgEx(picView.Handle, (int)-nScaleLeft, (int)-nScaleTop, out pnt);


                //'ノースアロー。
#if false
                /*
                 *************************** 修正要 sakai
                */
                m_clsMdlPlotProc.DrawNorthArrow((long)picView.Handle, m_clsPlotPort.ViewLeft - m_clsPlotPort.DeviceLeft, m_clsPlotPort.ViewTop - m_clsPlotPort.DeviceTop,
                    (double)1 / Screen.BitsPerPixel, m_hPenNorthArrow, NORTHARROW_COLOR_SYMBOL);
#else
#if false
                m_clsMdlPlotProc.DrawNorthArrow(picView.Handle, m_clsPlotPort.ViewLeft - m_clsPlotPort.DeviceLeft, m_clsPlotPort.ViewTop - m_clsPlotPort.DeviceTop,
                    (double)1 / 15, m_hPenNorthArrow, NORTHARROW_COLOR_SYMBOL);
#else
#if true
                //picView.Image = m_clsMdlPlotProc.DrawNorthArrow(picView.Image, m_clsPlotPort.ViewLeft - m_clsPlotPort.DeviceLeft, m_clsPlotPort.ViewTop - m_clsPlotPort.DeviceTop,
                //    (double)1 / 15, m_hPenNorthArrow, NORTHARROW_COLOR_SYMBOL, picView.Width, picView.Height);
                //Bitmap btmp = new Bitmap(picView.Width, picView.Height);
                picView.Image = m_clsMdlPlotProc.DrawNorthArrow(btmp, m_clsPlotPort.ViewLeft - m_clsPlotPort.DeviceLeft, m_clsPlotPort.ViewTop - m_clsPlotPort.DeviceTop,
                    (double)1 / 15, m_hPenNorthArrow, NORTHARROW_COLOR_SYMBOL, picView.Width, picView.Height);
#else
                picView.Image = btmp;
#endif
#endif
#endif

                //'スケールバー。
#if false
                /*
                 *************************** 修正要 sakai
                */
                m_clsMdlPlotProc.DrawScalebar((long)picView.Handle, m_clsPlotPort.ViewLeft - m_clsPlotPort.DeviceLeft, m_clsPlotPort.ViewTop - m_clsPlotPort.DeviceTop,
                    m_clsPlotPort.ViewWidth, m_clsPlotPort.ViewHeight, (double)1 / Screen.TwipsPerPixelX, 0, m_hBrushScale1, m_hBrushScale2, PLOT_SCALEBAR_COLOR_SYMBOL, m_clsPlotPort.DrawScale);
#else
#if false
                m_clsMdlPlotProc.DrawScalebar(picView.Handle, m_clsPlotPort.ViewLeft - m_clsPlotPort.DeviceLeft, m_clsPlotPort.ViewTop - m_clsPlotPort.DeviceTop,
                    m_clsPlotPort.ViewWidth, m_clsPlotPort.ViewHeight, (double)1 / 15, 0, m_hBrushScale1, m_hBrushScale2, PLOT_SCALEBAR_COLOR_SYMBOL, m_clsPlotPort.DrawScale);
#else
#if true
                picView.Image = m_clsMdlPlotProc.DrawScalebar((Bitmap)picView.Image, m_clsPlotPort.ViewLeft - m_clsPlotPort.DeviceLeft, m_clsPlotPort.ViewTop - m_clsPlotPort.DeviceTop,
                    m_clsPlotPort.ViewWidth, m_clsPlotPort.ViewHeight, (double)1 / 15, 0, m_hBrushScale1, m_hBrushScale2, PLOT_SCALEBAR_COLOR_SYMBOL, m_clsPlotPort.DrawScale);
#endif
#endif
#endif

                //'クリップ領域。
                double nClipLeft = 0;
                double nClipTop = 0;
                double nClipWidth = 0;
                double nClipHeight = 0;
                m_clsPlotPort.GetClipArea(ref nClipLeft, ref nClipTop, ref nClipWidth, ref nClipHeight);

                //'観測点、基線ベクトルの描画。
#if false
                m_clsPlotNative.DrawObjectFilm(picView.Handle, m_clsPlotPort.DeviceLeft, m_clsPlotPort.DeviceTop, nClipLeft, nClipTop, nClipWidth, nClipHeight,
                    (double)1 / Screen.TwipsPerPixelX, m_clsEmphasisHead, m_clsRegistAngle, m_clsRegistAngle Is Nothing);
#else
                picView.Image = m_clsPlotNative.DrawObjectFilm((Bitmap)picView.Image, m_clsPlotPort.DeviceLeft, m_clsPlotPort.DeviceTop, nClipLeft, nClipTop, nClipWidth, nClipHeight,
                    (double)1 / 15, m_clsEmphasisHead, m_clsRegistAngle, m_clsRegistAngle == null);
#endif

                SetViewportOrgEx(picView.Handle, (int)pnt.X, (int)pnt.Y, out pnt);
#if false
                SelectObject(picView.Handle, (int)hOldPen);
                SelectObject(picView.Handle, (int)hOldFont);
                SelectObject(picView.Handle, (int)hOldBrush);
#endif
            }

            catch
            {
#if false
                SelectObject(picView.Handle, (int)hOldPen);
                SelectObject(picView.Handle, (int)hOldFont);
                SelectObject(picView.Handle, (int)hOldBrush);
#endif
                m_clsMdlMain.ErrorExit();
                return;
            }

            //'スクロールバー隙間。
            if (scrHori.Visible && scrVert.Visible)
            {
                /*
                picView.Line(scrVert.Left, scrHori.Top) - Step(scrVert.Width, scrHori.Height), GetSysColor(COLOR_3DFACE), BF;
                 */
                //描画先とするImageオブジェクトを作成する
                //Bitmap canvas = new Bitmap(picView.Width, picView.Height);
                Bitmap canvas = (Bitmap)picView.Image;
                //ImageオブジェクトのGraphicsオブジェクトを作成する
                Graphics graph = Graphics.FromImage(canvas);
                // Create pen. COLOR_3DFACE 幅1
                Pen colorPen = new Pen(Color.FromArgb(0, 0xF0, 0xF0, 0xF0), 1);
                //幅1の黒い四角を書く
                graph.DrawRectangle(colorPen, scrVert.Left, scrHori.Top, scrVert.Width, scrHori.Height);
                //リソースを解放する
                graph.Dispose();
                colorPen.Dispose();
                //PictureBox1に表示する
                picView.Image = canvas;
            }
            else
            {
                //'何も描画しなかった場合は強制的に表示させる。
                picView.Refresh();
            }

            picView.Cursor = Cursors.Default;
            return;
        }

        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '描画。
        '
        'デバイスへ描画する。
        '
        '引き数：
        'objDevice デバイス。
        Private Sub Draw(ByVal objDevice As Object)

            If Not m_bDrawPermission Then Exit Sub

            '砂時計。
            Dim clsWaitCursor As New WaitCursor
            Set clsWaitCursor.Object = picView

            objDevice.DrawMode = vbCopyPen

            'クリア。
            objDevice.Line (objDevice.ScaleLeft, objDevice.ScaleTop)-(objDevice.ScaleLeft + objDevice.ScaleWidth, objDevice.ScaleTop + objDevice.ScaleHeight), BACKGROUND_COLOR, BF

            'デバイス範囲。
            Dim nDeviceRight As Single
            Dim nDeviceBottom As Single
            nDeviceRight = m_clsPlotPort.DeviceLeft + objDevice.ScaleWidth
            nDeviceBottom = m_clsPlotPort.DeviceTop + objDevice.ScaleHeight


            Dim clsBaseLineVector As BaseLineVector
            Dim clsObservationPoint As ObservationPoint

            '観測点の描画座標を設定する。
            If m_bDevicePoint Then
                m_bDevicePoint = False
                Dim clsChainList As ChainList
                Set clsChainList = GetDocument().NetworkModel.RepresentPointHead
                Do While Not clsChainList Is Nothing
                    Set clsObservationPoint = clsChainList.Element
                    Call m_clsPlotPort.SetDevicePoint(clsObservationPoint)
                    Set clsChainList = clsChainList.NextList
                Loop
            End If

            'クリップ領域。
            Dim nClipLeft As Double
            Dim nClipTop As Double
            Dim nClipWidth As Double
            Dim nClipHeight As Double
            Call m_clsPlotPort.GetClipArea(nClipLeft, nClipTop, nClipWidth, nClipHeight)

            'クリックオブジェクト登録開始。
            Dim nBaseLineVectorCount As Long
            Dim nRepresentPointCount As Long
            nBaseLineVectorCount = GetDocument().NetworkModel.BaseLineVectorCount
            nRepresentPointCount = GetDocument().NetworkModel.RepresentPointCount
            '↓クリックエリアから観測点記号がはみ出るとクリックできなくなる。
            'Call m_clsClickManager.StartRegist(m_clsPlotPort.LogicalWidth * m_clsPlotPort.DrawScale, m_clsPlotPort.LogicalHeight * m_clsPlotPort.DrawScale, nBaseLineVectorCount, nRepresentPointCount * 2)
            Dim nClickWidth As Double
            Dim nClickHeight As Double
            nClickWidth = m_clsPlotPort.LogicalWidth* m_clsPlotPort.DrawScale
            nClickHeight = m_clsPlotPort.LogicalHeight * m_clsPlotPort.DrawScale
            Dim nMapScale As Double
            Dim nBottomSpace As Double
            nMapScale = 1 / Screen.TwipsPerPixelX
            nBottomSpace = (nClipHeight - nClickHeight) * 0.5
            If PLOT_OBSPNT_SYMBOL_SPACE * nMapScale<nBottomSpace Then
                nClickHeight = nClickHeight + PLOT_OBSPNT_SYMBOL_SPACE* nMapScale
            ElseIf 0 < nBottomSpace Then
                nClickHeight = nClickHeight + nBottomSpace
            End If
            Call m_clsClickManager.StartRegist(nClickWidth, nClickHeight, nBaseLineVectorCount, nRepresentPointCount* 2)
            '↑クリックエリアを下に広く拡張する。

            '観測点、基線ベクトルの描画。
            Call m_clsPlotNative.DrawObject(objDevice.hDC, m_clsPlotPort.DeviceLeft, m_clsPlotPort.DeviceTop, nDeviceRight, nDeviceBottom, nClipLeft, nClipTop, nClipWidth, nClipHeight, nMapScale, m_clsClickManager, m_clsRegistAngle Is Nothing, False, &HFFFFFFFF, IIf(m_clsRegistAngle Is Nothing, OBJ_TYPE_OBSERVATIONPOINT Or OBJ_TYPE_BASELINEVECTOR, OBJ_TYPE_BASELINEVECTOR))

            'クリックオブジェクト登録完了。
            m_clsClickManager.EndRegist

            Call clsWaitCursor.Back

        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '描画。
        '
        'デバイスへ描画する。
        '
        '引き数：
        'objDevice デバイス。
        */
#if false
        private void Draw(ref PictureBox objDevice)
        {
#else
        private Bitmap Draw(ref Bitmap objDevice)
        {
#endif
#if false
            if (!m_bDrawPermission) { return; }
#else
            if (!m_bDrawPermission) { return objDevice; }
#endif

            //'砂時計。
            //WaitCursor clsWaitCursor = new WaitCursor();
            //clsWaitCursor.object = picView;
            picView.Cursor = Cursors.WaitCursor;


#if false
            /*
             *************************** 修正要 sakai
            */
            objDevice.DrawMode = vbCopyPen;
#endif
            //'クリア。
            /*
            objDevice.Line(objDevice.Left, objDevice.Top) - (objDevice.Left + objDevice.Width, objDevice.Top + objDevice.Height), BACKGROUND_COLOR, BF;
            */
            //描画先とするImageオブジェクトを作成する
            Bitmap canvas = new Bitmap(picView.Width, picView.Height);
            //Bitmap canvas = (Bitmap)objDevice.Image;
            //ImageオブジェクトのGraphicsオブジェクトを作成する
            Graphics graph = Graphics.FromImage(canvas);
            // Create pen. COLOR_3DFACE 幅1
            //Pen colorPen = new Pen(Color.FromArgb(0, 0xF0, 0xF0, 0xF0), 1);
            Pen colorPen = new Pen(Color.Black, 1);
            //幅1の黒い四角を書く
            //graph.DrawRectangle(colorPen, objDevice.Left, objDevice.Top, objDevice.Left + objDevice.Width, objDevice.Top + objDevice.Height);
            graph.DrawRectangle(colorPen, 0, 0, objDevice.Width, objDevice.Height);
            //リソースを解放する
            graph.Dispose();
            colorPen.Dispose();
            //PictureBox1に表示する
            //objDevice.Image = canvas;
            objDevice = canvas;


            //'デバイス範囲。
            float nDeviceRight;
            float nDeviceBottom;
            nDeviceRight = (long)(m_clsPlotPort.DeviceLeft + objDevice.Width);
            nDeviceBottom = (long)(m_clsPlotPort.DeviceTop + objDevice.Height);

            BaseLineVector clsBaseLineVector;
            ObservationPoint clsObservationPoint;

            //'観測点の描画座標を設定する。
            if (m_bDevicePoint)
            {
                m_bDevicePoint = false;
                ChainList clsChainList;
                clsChainList = m_clsMdlMain.GetDocument().NetworkModel().RepresentPointHead();
                while (clsChainList != null)
                {
                    clsObservationPoint = (ObservationPoint)clsChainList.Element;
                    m_clsPlotPort.SetDevicePoint(clsObservationPoint);
                    clsChainList = clsChainList.NextList();
                }
            }

            //'クリップ領域。
            double nClipLeft = 0;
            double nClipTop = 0;
            double nClipWidth = 0;
            double nClipHeight = 0;
            m_clsPlotPort.GetClipArea(ref nClipLeft, ref nClipTop, ref nClipWidth, ref nClipHeight);

            //'クリックオブジェクト登録開始。
            long nBaseLineVectorCount;
            long nRepresentPointCount;
            nBaseLineVectorCount = m_clsMdlMain.GetDocument().NetworkModel().BaseLineVectorCount();
            nRepresentPointCount = m_clsMdlMain.GetDocument().NetworkModel().RepresentPointCount();
            //'↓クリックエリアから観測点記号がはみ出るとクリックできなくなる。
            //'Call m_clsClickManager.StartRegist(m_clsPlotPort.LogicalWidth * m_clsPlotPort.DrawScale, m_clsPlotPort.LogicalHeight * m_clsPlotPort.DrawScale, nBaseLineVectorCount, nRepresentPointCount * 2)
            double nClickWidth;
            double nClickHeight;
            nClickWidth = m_clsPlotPort.LogicalWidth * m_clsPlotPort.DrawScale;
            nClickHeight = m_clsPlotPort.LogicalHeight * m_clsPlotPort.DrawScale;
            double nMapScale;
            double nBottomSpace;
#if false
            nMapScale = (double)1 / Screen.TwipsPerPixelX;
#else
            nMapScale = (double)1 / 15;
#endif
            nBottomSpace = (double)(nClipHeight - nClickHeight) * 0.5;
            if (PLOT_OBSPNT_SYMBOL_SPACE * nMapScale < nBottomSpace)
            {
                nClickHeight = (double)(nClickHeight + (PLOT_OBSPNT_SYMBOL_SPACE * nMapScale));
            }
            else if (0 < nBottomSpace)
            {
                nClickHeight = (double)nClickHeight + nBottomSpace;
            }
            m_clsClickManager.StartRegist(nClickWidth, nClickHeight, nBaseLineVectorCount, nRepresentPointCount * 2);
            //'↑クリックエリアを下に広く拡張する。

#if false
            //'観測点、基線ベクトルの描画。
            m_clsPlotNative.DrawObject((long)objDevice.Handle, m_clsPlotPort.DeviceLeft, m_clsPlotPort.DeviceTop, nDeviceRight, nDeviceBottom, nClipLeft, nClipTop,
                nClipWidth, nClipHeight, nMapScale, m_clsClickManager, m_clsRegistAngle == null, false, 0xFFFFFFFF,
                m_clsRegistAngle == null ? OBJ_TYPE_OBSERVATIONPOINT | OBJ_TYPE_BASELINEVECTOR : OBJ_TYPE_BASELINEVECTOR);
#else
            //'観測点、基線ベクトルの描画。
            objDevice = m_clsPlotNative.DrawObject(objDevice, m_clsPlotPort.DeviceLeft, m_clsPlotPort.DeviceTop, nDeviceRight, nDeviceBottom, nClipLeft, nClipTop,
                nClipWidth, nClipHeight, nMapScale, m_clsClickManager, m_clsRegistAngle == null, false, 0xFFFFFFFF,
                m_clsRegistAngle == null ? OBJ_TYPE_OBSERVATIONPOINT | OBJ_TYPE_BASELINEVECTOR : OBJ_TYPE_BASELINEVECTOR);
#endif
            //'クリックオブジェクト登録完了。
            m_clsClickManager.EndRegist();

            //clsWaitCursor.Back();
            picView.Cursor = Cursors.Default;
#if false
            return;
#else
            return objDevice;
#endif
        }

        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'スライド処理。
        Private Sub Slide()

            Dim nDX As Long
            Dim nDY As Long
            nDX = m_nPointEX - m_nPointSX
            nDY = m_nPointEY - m_nPointSY
            m_nPointSX = m_nPointEX
            m_nPointSY = m_nPointEY
    
            '表示位置。
            If scrHori.Visible Then m_clsPlotPort.ViewLeft = m_clsPlotPort.ViewLeft - nDX
            If scrVert.Visible Then m_clsPlotPort.ViewTop = m_clsPlotPort.ViewTop - nDY


            Dim bRedraw As Boolean
            If m_clsPlotPort.ViewLeft<m_clsPlotPort.DeviceLeft Or m_clsPlotPort.DeviceLeft + picMemDC.ScaleWidth<m_clsPlotPort.ViewLeft + m_clsPlotPort.ViewWidth Then
                '描画範囲も移動。
                m_clsPlotPort.DeviceLeft = m_clsPlotPort.ViewLeft - (picMemDC.ScaleWidth - m_clsPlotPort.ViewWidth) * 0.5
                m_clsPlotPort.DeviceTop = m_clsPlotPort.ViewTop - (picMemDC.ScaleHeight - m_clsPlotPort.ViewHeight) * 0.5
                bRedraw = True
            End If
            If m_clsPlotPort.ViewTop<m_clsPlotPort.DeviceTop Or m_clsPlotPort.DeviceTop + picMemDC.ScaleHeight<m_clsPlotPort.ViewTop + m_clsPlotPort.ViewHeight Then
                '描画範囲も移動。
                m_clsPlotPort.DeviceLeft = m_clsPlotPort.ViewLeft - (picMemDC.ScaleWidth - m_clsPlotPort.ViewWidth) * 0.5
                m_clsPlotPort.DeviceTop = m_clsPlotPort.ViewTop - (picMemDC.ScaleHeight - m_clsPlotPort.ViewHeight) * 0.5
                bRedraw = True
            End If

            If bRedraw Then
                '描画範囲が論理範囲からはみ出さないように補正。
                Call m_clsPlotPort.CorrectDrawArea
                '再描画。
                Call Redraw
            End If
    
            'スクロール位置。
            If scrHori.Visible Then scrHori.Value = m_clsPlotPort.ViewLeft / (m_clsPlotPort.LogicalWidth * m_clsPlotPort.DrawScale - m_clsPlotPort.ViewWidth) * scrHori.Max
            If scrVert.Visible Then scrVert.Value = m_clsPlotPort.ViewTop / (m_clsPlotPort.LogicalHeight * m_clsPlotPort.DrawScale - m_clsPlotPort.ViewHeight) * scrVert.Max
    
            '再表示。
            Call Refresh


        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'スライド処理。
        private void Slide()
        {
            long nDX;
            long nDY;
            nDX = m_nPointEX - m_nPointSX;
            nDY = m_nPointEY - m_nPointSY;
            m_nPointSX = m_nPointEX;
            m_nPointSY = m_nPointEY;

            //'表示位置。
            if (scrHori.Visible)
            {
                m_clsPlotPort.ViewLeft = m_clsPlotPort.ViewLeft - nDX;
            }
            if (scrVert.Visible)
            {
                m_clsPlotPort.ViewTop = m_clsPlotPort.ViewTop - nDY;
            }

            bool bRedraw = false;
            if (m_clsPlotPort.ViewLeft < m_clsPlotPort.DeviceLeft || (m_clsPlotPort.DeviceLeft + picMemDC.Width) < (m_clsPlotPort.ViewLeft + m_clsPlotPort.ViewWidth))
            {
                //'描画範囲も移動。
                m_clsPlotPort.DeviceLeft = m_clsPlotPort.ViewLeft - (picMemDC.Width - m_clsPlotPort.ViewWidth) * 0.5;
                m_clsPlotPort.DeviceTop = m_clsPlotPort.ViewTop - (picMemDC.Height - m_clsPlotPort.ViewHeight) * 0.5;
                bRedraw = true;
            }
            if (m_clsPlotPort.ViewTop < m_clsPlotPort.DeviceTop || (m_clsPlotPort.DeviceTop + picMemDC.Height) < (m_clsPlotPort.ViewTop + m_clsPlotPort.ViewHeight))
            {
                //'描画範囲も移動。
                m_clsPlotPort.DeviceLeft = m_clsPlotPort.ViewLeft - (picMemDC.Width - m_clsPlotPort.ViewWidth) * 0.5;
                m_clsPlotPort.DeviceTop = m_clsPlotPort.ViewTop - (picMemDC.Height - m_clsPlotPort.ViewHeight) * 0.5;
                bRedraw = true;
            }

            if (bRedraw)
            {
                //'描画範囲が論理範囲からはみ出さないように補正。
                m_clsPlotPort.CorrectDrawArea();
                //'再描画。
                Redraw();
            }

            //'スクロール位置。
            if (scrHori.Visible)
            {
                scrHori.Value = (int)(m_clsPlotPort.ViewLeft / (m_clsPlotPort.LogicalWidth * m_clsPlotPort.DrawScale - m_clsPlotPort.ViewWidth) * scrHori.Maximum);
            }
            if (scrVert.Visible)
            {
                scrVert.Value = (int)(m_clsPlotPort.ViewTop / (m_clsPlotPort.LogicalHeight * m_clsPlotPort.DrawScale - m_clsPlotPort.ViewHeight) * scrVert.Maximum);
            }

            //'再表示。
            Refresh();

            return;
        }

        private void UserControl_Resize(object sender, EventArgs e)
        {

        }
        //==========================================================================================
    }
}