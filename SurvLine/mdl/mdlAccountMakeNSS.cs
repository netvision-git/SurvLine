using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Runtime.CompilerServices;
using static SurvLine.MdlAccountMake;
using static SurvLine.mdl.MdlNSDefine;

namespace SurvLine
{
    internal class MdlAccountMakeNSS
    {
        //==========================================================================================
        /*[VB]
        '*******************************************************************************
        '帳票作成

        Option Explicit
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        Private Const TO_WIDTH_X As Long = 900 '間引き基準幅。
        Private Const LABEL_OFFSET_Y As Long = 60 'Y軸ラベルオフセット。
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        private const long TO_WIDTH_X = 900;        //'間引き基準幅。
        private const long LABEL_OFFSET_Y = 60;     //'Y軸ラベルオフセット。
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '帳票種別。
        Public Enum ACCOUNT_TYPE
            ACCOUNT_TYPE_DATAVIEW 'データ表示。
            ACCOUNT_TYPE_HAND '観測手簿。
            ACCOUNT_TYPE_WRITE '観測記簿。
            ACCOUNT_TYPE_COORDINATE '座標計算簿。
            ACCOUNT_TYPE_OVERLAP '点検計算簿(重複基線)。
            ACCOUNT_TYPE_OVERLAPCHECK '点検測量(重複基線較差)。
            ACCOUNT_TYPE_RING '点検計算簿(環閉合差)。
            ACCOUNT_TYPE_BETWEEN '点検計算簿(電子基準点間の閉合差)。
            ACCOUNT_TYPE_ECCENTRICCORRECT '偏心計算簿。
            ACCOUNT_TYPE_SEMIDYNA_GAN2KON '元期→今期補正表。'2009/11 H.Nakamura
            ACCOUNT_TYPE_CADASTRALACCURACYRING '地籍図根三角測量精度管理表(環閉合差)。
            ACCOUNT_TYPE_CADASTRALACCURACYOVERLAP '地籍図根三角測量精度管理表(重複辺の較差)。
            ACCOUNT_TYPE_CADASTRALOVERLAP '地籍測量、点検計算簿(重複基線)。
            ACCOUNT_TYPE_CADASTRALRING '地籍測量、点検計算簿(環閉合差)。
            ACCOUNT_TYPE_CADASTRALBETWEEN '地籍測量、点検計算簿(電子基準点間の閉合差)。
            ACCOUNT_TYPE_⊿XYZ2⊿NEURING 'ΔXYZからΔNEUへの変換(環閉合差)。
            ACCOUNT_TYPE_⊿XYZ2⊿NEUBETWEEN 'ΔXYZからΔNEUへの変換(電子基準点間の閉合差)。
            ACCOUNT_TYPE_RESULTBASE '座標一覧。2007/7/18 NGS Yamada
            ACCOUNT_TYPE_NVF 'NVF。
            ACCOUNT_TYPE_NVB 'NVB。
            ACCOUNT_TYPE_JOB 'JOB。
            ACCOUNT_TYPE_RINEX 'RINEXエクスポート。
            ACCOUNT_TYPE_CSV 'CSV。
            ACCOUNT_TYPE_ANGLEDIFF_HEIGHT '点検計算簿(楕円体高の閉合差)。'2023/05/19 Hitz H.Nakamura 楕円体高の閉合差を追加。
            ACCOUNT_TYPE_OVERLAP_HALF '点検計算簿(前後半基線較差)。'2023/07/12 Hitz H.Nakamura GNSS水準測量対応。点検計算簿(前後半基線較差) の追加。
        End Enum
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'帳票種別。
        public enum ACCOUNT_TYPE
        {
            ACCOUNT_TYPE_DATAVIEW,                      //'データ表示。
            ACCOUNT_TYPE_HAND,                          //'観測手簿。
            ACCOUNT_TYPE_WRITE,                         //'観測記簿。
            ACCOUNT_TYPE_COORDINATE,                    //'座標計算簿。
            ACCOUNT_TYPE_OVERLAP,                       //'点検計算簿(重複基線)。
            ACCOUNT_TYPE_OVERLAPCHECK,                  //'点検測量(重複基線較差)。
            ACCOUNT_TYPE_RING,                          //'点検計算簿(環閉合差)。
            ACCOUNT_TYPE_BETWEEN,                       //'点検計算簿(電子基準点間の閉合差)。
            ACCOUNT_TYPE_ECCENTRICCORRECT,              //'偏心計算簿。
            ACCOUNT_TYPE_SEMIDYNA_GAN2KON,              //'元期→今期補正表。'2009/11 H.Nakamura
            ACCOUNT_TYPE_CADASTRALACCURACYRING,         //'地籍図根三角測量精度管理表(環閉合差)。
            ACCOUNT_TYPE_CADASTRALACCURACYOVERLAP,      //'地籍図根三角測量精度管理表(重複辺の較差)。
            ACCOUNT_TYPE_CADASTRALOVERLAP,              //'地籍測量、点検計算簿(重複基線)。
            ACCOUNT_TYPE_CADASTRALRING,                 //'地籍測量、点検計算簿(環閉合差)。
            ACCOUNT_TYPE_CADASTRALBETWEEN,              //'地籍測量、点検計算簿(電子基準点間の閉合差)。
            ACCOUNT_TYPE__XYZ2_NEURING,                 //'ΔXYZからΔNEUへの変換(環閉合差)。
            ACCOUNT_TYPE__XYZ2_NEUBETWEEN,              //'ΔXYZからΔNEUへの変換(電子基準点間の閉合差)。
            ACCOUNT_TYPE_RESULTBASE,                    //'座標一覧。2007/7/18 NGS Yamada
            ACCOUNT_TYPE_NVF,                           //'NVF。
            ACCOUNT_TYPE_NVB,                           //'NVB。
            ACCOUNT_TYPE_JOB,                           //'JOB。
            ACCOUNT_TYPE_RINEX,                         //'RINEXエクスポート。
            ACCOUNT_TYPE_CSV,                           //'CSV。
            ACCOUNT_TYPE_ANGLEDIFF_HEIGHT,              //'点検計算簿(楕円体高の閉合差)。'2023/05/19 Hitz H.Nakamura 楕円体高の閉合差を追加。
            ACCOUNT_TYPE_OVERLAP_HALF,                  //'点検計算簿(前後半基線較差)。'2023/07/12 Hitz H.Nakamura GNSS水準測量対応。点検計算簿(前後半基線較差) の追加。
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '初期値。
        Public Const DEF_OUTPUT_RANGE_TYPE = RANGE_TYPE_ALL
        Public Const DEF_OUTPUT_SELECTED_OBJECT_TYPE = SELECTED_OBJECT_TYPE_SESSION
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'初期値。
        public const long DEF_OUTPUT_RANGE_TYPE = (long)RANGE_TYPE.RANGE_TYPE_ALL;
        public const long DEF_OUTPUT_SELECTED_OBJECT_TYPE = (long)SELECTED_OBJECT_TYPE.SELECTED_OBJECT_TYPE_SESSION;
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '書式｡
        Public Const ACCOUNT_FORMAT_STDDEV As String = "0.000E+000" '標準偏差。
        Public Const ACCOUNT_DECIMAL_STDDEV_MM As Long = 2 '標準偏差、㎜。
        Public Const ACCOUNT_DECIMAL_TEMPERATURE As Long = 1 '温度。
        Public Const ACCOUNT_DECIMAL_PRESSURE As Long = 1 '気圧。
        Public Const ACCOUNT_DECIMAL_HUMIDITY As Long = 1 '湿度。
        Public Const ACCOUNT_DECIMAL_TEMPERATURE_W As Long = 0 '温度。
        Public Const ACCOUNT_DECIMAL_PRESSURE_W As Long = 0 '気圧。
        Public Const ACCOUNT_DECIMAL_HUMIDITY_W As Long = 0 '湿度。
        Public Const ACCOUNT_DECIMAL_BIAS As Long = 2 'バイアス決定比。
        Public Const ACCOUNT_DECIMAL_REJECT As Long = 1 '棄却率。
        Public Const ACCOUNT_DECIMAL_RMS As Long = 3 'RMS。
        Public Const ACCOUNT_DECIMAL_RMS_MM As Long = 1 'RMS、㎜。
        Public Const ACCOUNT_DECIMAL_RDOP As Long = 4 'RDOP。
        Public Const ACCOUNT_DECIMAL_LC As Long = 4 'アンテナ位相補正。
        Public Const ACCOUNT_DECIMAL_INTERVAL As Long = 2 'データ取得間隔。
        Public Const ACCOUNT_DECIMAL_ELEVATIONMASK As Long = 2 '仰角マスク。
        Public Const ACCOUNT_DECIMAL_ELEVATIONMASK_W As Long = 0 '仰角マスク。
        Public Const ACCOUNT_DECIMAL_AMBIGUITY As Long = 3 'フロートアンビギュイティ。
        Public Const ACCOUNT_DECIMAL_OFFSETL As Long = 4 'アンテナ位相補正。

        Public Const ACCOUNT_MODAL As Long = 1 '帳票モーダルフラグ。

        Public Const STDDEV_IMPOSSIBLE As String = "" '標準偏差計算不能。

        Public Const DEF_ANALYSISMETHOD As String = "" 'デフォルトの基線解析方法。

        Public SV_COLOR_L() As Long '衛星カラーテーブル(明)。配列の要素は(0 To ...)。
        Public SV_COLOR_D() As Long '衛星カラーテーブル(暗)。配列の要素は(0 To ...)。
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'書式｡
        public const string ACCOUNT_FORMAT_STDDEV = "0.000E+000";   //'標準偏差。
        public const long ACCOUNT_DECIMAL_STDDEV_MM = 2;            //'標準偏差、㎜。
        public const long ACCOUNT_DECIMAL_TEMPERATURE = 1;          //'温度。
        public const long ACCOUNT_DECIMAL_PRESSURE = 1;             //'気圧。
        public const long ACCOUNT_DECIMAL_HUMIDITY = 1;             //'湿度。
        public const long ACCOUNT_DECIMAL_TEMPERATURE_W = 0;        //'温度。
        public const long ACCOUNT_DECIMAL_PRESSURE_W = 0;           //'気圧。
        public const long ACCOUNT_DECIMAL_HUMIDITY_W = 0;           //'湿度。
        public const long ACCOUNT_DECIMAL_BIAS = 2;                 //'バイアス決定比。
        public const long ACCOUNT_DECIMAL_REJECT = 1;               //'棄却率。
        public const long ACCOUNT_DECIMAL_RMS = 3;                  //'RMS。
        public const long ACCOUNT_DECIMAL_RMS_MM = 1;               //'RMS、㎜。
        public const long ACCOUNT_DECIMAL_RDOP = 4;                 //'RDOP。
        public const long ACCOUNT_DECIMAL_LC = 4;                   //'アンテナ位相補正。
        public const long ACCOUNT_DECIMAL_INTERVAL = 2;             //'データ取得間隔。
        public const long ACCOUNT_DECIMAL_ELEVATIONMASK = 2;        //'仰角マスク。
        public const long ACCOUNT_DECIMAL_ELEVATIONMASK_W = 0;      //'仰角マスク。
        public const long ACCOUNT_DECIMAL_AMBIGUITY = 3;            //'フロートアンビギュイティ。
        public const long ACCOUNT_DECIMAL_OFFSETL = 4;              //'アンテナ位相補正。

        public const long ACCOUNT_MODAL = 1;                        //'帳票モーダルフラグ。

        public const string STDDEV_IMPOSSIBLE = "";                 //'標準偏差計算不能。

        public const string DEF_ANALYSISMETHOD = "";                //'デフォルトの基線解析方法。

        public uint[] SV_COLOR_L = new uint[16];                    //'衛星カラーテーブル(明)。配列の要素は(0 To ...)。
        public uint[] SV_COLOR_D = new uint[16];                    //'衛星カラーテーブル(暗)。配列の要素は(0 To ...)。
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '初期化。
        Public Sub InitializeAccountMake()
            '時間帯。
            DISP_TIME_ZONE(TIME_ZONE_UTC) = DISP_TIME_ZONE_UTC
            DISP_TIME_ZONE(TIME_ZONE_JST) = DISP_TIME_ZONE_JST
    
            '衛星カラーテーブル。
            ReDim SV_COLOR_L(15)
            SV_COLOR_L(0) = RGB(255, 0, 0)
            SV_COLOR_L(1) = RGB(0, 255, 0)
            SV_COLOR_L(2) = RGB(0, 0, 255)
            SV_COLOR_L(3) = RGB(255, 255, 0)
            SV_COLOR_L(4) = RGB(0, 255, 255)
            SV_COLOR_L(5) = RGB(255, 0, 255)
            SV_COLOR_L(6) = RGB(255, 102, 0)
            SV_COLOR_L(7) = RGB(0, 255, 102)
            SV_COLOR_L(8) = RGB(102, 0, 255)
            SV_COLOR_L(9) = RGB(255, 255, 102)
            SV_COLOR_L(10) = RGB(102, 255, 255)
            SV_COLOR_L(11) = RGB(255, 102, 255)
            SV_COLOR_L(12) = RGB(255, 204, 153)
            SV_COLOR_L(13) = RGB(153, 255, 204)
            SV_COLOR_L(14) = RGB(204, 153, 255)
            SV_COLOR_L(15) = RGB(255, 255, 255)
            ReDim SV_COLOR_D(15)
            SV_COLOR_D(0) = RGB(153, 0, 0)
            SV_COLOR_D(1) = RGB(0, 153, 0)
            SV_COLOR_D(2) = RGB(0, 0, 153)
            SV_COLOR_D(3) = RGB(153, 153, 0)
            SV_COLOR_D(4) = RGB(0, 153, 153)
            SV_COLOR_D(5) = RGB(153, 0, 153)
            SV_COLOR_D(6) = RGB(153, 51, 0)
            SV_COLOR_D(7) = RGB(0, 153, 51)
            SV_COLOR_D(8) = RGB(51, 0, 153)
            SV_COLOR_D(9) = RGB(153, 153, 51)
            SV_COLOR_D(10) = RGB(51, 153, 153)
            SV_COLOR_D(11) = RGB(153, 51, 153)
            SV_COLOR_D(12) = RGB(153, 102, 102)
            SV_COLOR_D(13) = RGB(102, 153, 102)
            SV_COLOR_D(14) = RGB(102, 102, 153)
            SV_COLOR_D(15) = RGB(153, 153, 153)
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'初期化。
        public void InitializeAccountMake()
        {
            //'時間帯。
            MdlAccountMake clsMdlAccountMake = new MdlAccountMake();
            clsMdlAccountMake.DISP_TIME_ZONE[(int)TIME_ZONE.TIME_ZONE_UTC] = DISP_TIME_ZONE_UTC;
            clsMdlAccountMake.DISP_TIME_ZONE[(int)TIME_ZONE.TIME_ZONE_JST] = DISP_TIME_ZONE_JST;


            //'衛星カラーテーブル。
            SV_COLOR_L[0] = (255 << 16) | (0 << 8) | 0;     //(0 << 16) | (0 << 8) | 0; //(255, 0, 0);
            SV_COLOR_L[1] = (0 << 16) | (255 << 8) | 0;     //(0 << 16) | (0 << 8) | 0; //(0, 255, 0);
            SV_COLOR_L[2] = (0 << 16) | (0 << 8) | 255; //(0, 0, 255);
            SV_COLOR_L[3] = (255 << 16) | (255 << 8) | 0; //(255, 255, 0);
            SV_COLOR_L[4] = (0 << 16) | (255 << 8) | 255; //(0, 255, 255);
            SV_COLOR_L[5] = (255 << 16) | (0 << 8) | 255; //(255, 0, 255);
            SV_COLOR_L[6] = (255 << 16) | (102 << 8) | 0; //(255, 102, 0);
            SV_COLOR_L[7] = (0 << 16) | (255 << 8) | 102; //(0, 255, 102);
            SV_COLOR_L[8] = (102 << 16) | (0 << 8) | 255; //(102, 0, 255);
            SV_COLOR_L[9] = (255 << 16) | (255 << 8) | 102; //(255, 255, 102);
            SV_COLOR_L[10] = (102 << 16) | (255 << 8) | 255; //(102, 255, 255);
            SV_COLOR_L[11] = (255 << 16) | (102 << 8) | 255; //(255, 102, 255);
            SV_COLOR_L[12] = (255 << 16) | (204 << 8) | 153; //(255, 204, 153);
            SV_COLOR_L[13] = (153 << 16) | (255 << 8) | 204; //(153, 255, 204);
            SV_COLOR_L[14] = (204 << 16) | (153 << 8) | 255; //(204, 153, 255);
            SV_COLOR_L[15] = (255 << 16) | (255 << 8) | 255; //(255, 255, 255);
            SV_COLOR_D[0] = (153 << 16) | (0 << 8) | 0; //(153, 0, 0);
            SV_COLOR_D[1] = (0 << 16) | (153 << 8) | 0; //(0, 153, 0);
            SV_COLOR_D[2] = (0 << 16) | (0 << 8) | 153; //(0, 0, 153);
            SV_COLOR_D[3] = (153 << 16) | (153 << 8) | 0; //(153, 153, 0);
            SV_COLOR_D[4] = (0 << 16) | (153 << 8) | 153; //(0, 153, 153);
            SV_COLOR_D[5] = (153 << 16) | (0 << 8) | 153; //(153, 0, 153);
            SV_COLOR_D[6] = (153 << 16) | (51 << 8) | 0; //(153, 51, 0);
            SV_COLOR_D[7] = (0 << 16) | (153 << 8) | 51; //(0, 153, 51);
            SV_COLOR_D[8] = (51 << 16) | (0 << 8) | 153; //(51, 0, 153);
            SV_COLOR_D[9] = (153 << 16) | (153 << 8) | 51; //(153, 153, 51);
            SV_COLOR_D[10] = (51 << 16) | (153 << 8) | 153; //(51, 153, 153);
            SV_COLOR_D[11] = (153 << 16) | (51 << 8) | 153; //(153, 51, 153);
            SV_COLOR_D[12] = (153 << 16) | (102 << 8) | 102; //(153, 102, 102);
            SV_COLOR_D[13] = (102 << 16) | (153 << 8) | 102; //(102, 153, 102);
            SV_COLOR_D[14] = (102 << 16) | (102 << 8) | 153; //(102, 102, 153);
            SV_COLOR_D[15] = (153 << 16) | (153 << 8) | 153; //(153, 153, 153);
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '衛星カラーを取得する(明)。
        '
        '引き数：
        'nNumber 色番号。0～15。
        '
        '戻り値：衛星カラーを返す。
        Public Function GetSVColorL(ByVal nNumber As Long) As Long
            GetSVColorL = SV_COLOR_L(nNumber Mod (UBound(SV_COLOR_L) + 1))
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '衛星カラーを取得する(暗)。
        '
        '引き数：
        'nNumber 色番号。0～15。
        '
        '戻り値：衛星カラーを返す。
        Public Function GetSVColorD(ByVal nNumber As Long) As Long
            GetSVColorD = SV_COLOR_D(nNumber Mod (UBound(SV_COLOR_D) + 1))
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '矩形を描画する。
        '
        '引き数：
        'objCanvas 描画対象。
        'nLeft 左辺位置(Twips)。
        'nTop 上辺位置(Twips)。
        'nWidth 幅(Twips)。
        'nHeight 高さ(Twips)。
        Public Sub DrawFrame(ByVal objCanvas As Canvas, ByVal nLeft As Single, ByVal nTop As Single, ByVal nWidth As Single, ByVal nHeight As Single)
            Call objCanvas.DrawLine(nLeft, nTop, nLeft, nTop + nHeight)
            Call objCanvas.DrawLine(nLeft, nTop + nHeight, nLeft + nWidth, nTop + nHeight)
            Call objCanvas.DrawLine(nLeft + nWidth, nTop + nHeight, nLeft + nWidth, nTop)
            Call objCanvas.DrawLine(nLeft + nWidth, nTop, nLeft, nTop)
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'X軸グリッドの描画。
        '
        'clsDataViewInfo で指定されたデータの内容に適するX軸間隔(スケール)を計算する。
        '間隔に従いグリッド線を描画する。
        'bLabel が True の場合は軸とラベルを描画する。
        '
        '引き数：
        'objCanvas 描画対象。
        'nGraphLeft 描画範囲左辺位置(Twips)。
        'nGraphTop 描画範囲上辺位置(Twips)。
        'nGraphWidth 描画範囲幅(Twips)。
        'nGraphHeight 描画範囲高さ(Twips)。
        'bLabel ラベル描画フラグ。
        'clsDataViewInfo データ表示情報。
        '
        '戻り値：X軸のスケールを返す。
        Public Function DrawGridX(ByVal objCanvas As Canvas, ByVal nGraphLeft As Single, ByVal nGraphTop As Single, ByVal nGraphWidth As Single, ByVal nGraphHeight As Single, ByVal bLabel As Boolean, ByVal clsDataViewInfo As DataViewInfo) As Double

            'スケール。
            Dim nScaleX As Double
            Dim nSpan As Long
            nSpan = DateDiff("s", clsDataViewInfo.StrTimeGPS, clsDataViewInfo.EndTimeGPS)
            If nSpan > 0 Then
                nScaleX = nGraphWidth / nSpan
            Else
                nScaleX = 1
            End If
    
            '時間。
            Dim tStrTime As Date
            Dim tEndTime As Date
            tStrTime = GetTimeFromGPS(clsDataViewInfo.StrTimeGPS, clsDataViewInfo.LeapSeconds, GetDocument().TimeZone)
            tEndTime = GetTimeFromGPS(clsDataViewInfo.EndTimeGPS, clsDataViewInfo.LeapSeconds, GetDocument().TimeZone)
            Dim tTime As Date
    
            '間引き幅。
            Dim nTO As Long
            Dim sFormat As String
            If nScaleX * 1 > TO_WIDTH_X Then '1秒間隔。
                nTO = 1
                sFormat = "hh:nn:ss"
                tTime = DateSerial(Year(tStrTime), Month(tStrTime), Day(tStrTime)) + TimeSerial(Hour(tStrTime), Minute(tStrTime), Second(tStrTime))
            ElseIf nScaleX * 10 > TO_WIDTH_X Then '10秒間隔。
                nTO = 10
                sFormat = "hh:nn:ss"
                tTime = DateSerial(Year(tStrTime), Month(tStrTime), Day(tStrTime)) + TimeSerial(Hour(tStrTime), Minute(tStrTime), 0)
            ElseIf nScaleX * 30 > TO_WIDTH_X Then '30秒間隔。
                nTO = 30
                sFormat = "hh:nn:ss"
                tTime = DateSerial(Year(tStrTime), Month(tStrTime), Day(tStrTime)) + TimeSerial(Hour(tStrTime), Minute(tStrTime), 0)
            ElseIf nScaleX * 60 > TO_WIDTH_X Then '1分間隔。
                nTO = 60
                sFormat = "hh:nn"
                tTime = DateSerial(Year(tStrTime), Month(tStrTime), Day(tStrTime)) + TimeSerial(Hour(tStrTime), Minute(tStrTime), 0)
            ElseIf nScaleX * 300 > TO_WIDTH_X Then '5分間隔。
                nTO = 300
                sFormat = "hh:nn"
                tTime = DateSerial(Year(tStrTime), Month(tStrTime), Day(tStrTime)) + TimeSerial(Hour(tStrTime), 0, 0)
            ElseIf nScaleX * 600 > TO_WIDTH_X Then '10分間隔。
                nTO = 600
                sFormat = "hh:nn"
                tTime = DateSerial(Year(tStrTime), Month(tStrTime), Day(tStrTime)) + TimeSerial(Hour(tStrTime), 0, 0)
            ElseIf nScaleX * 1200 > TO_WIDTH_X Then '20分間隔。
                nTO = 1200
                sFormat = "hh:nn"
                tTime = DateSerial(Year(tStrTime), Month(tStrTime), Day(tStrTime)) + TimeSerial(Hour(tStrTime), 0, 0)
            ElseIf nScaleX * 1800 > TO_WIDTH_X Then '30分間隔。
                nTO = 1800
                sFormat = "hh:nn"
                tTime = DateSerial(Year(tStrTime), Month(tStrTime), Day(tStrTime)) + TimeSerial(Hour(tStrTime), 0, 0)
            ElseIf nScaleX * 3600 > TO_WIDTH_X Then '1時間間隔。
                nTO = 3600
                sFormat = "hh:nn"
                tTime = DateSerial(Year(tStrTime), Month(tStrTime), Day(tStrTime)) + TimeSerial(Hour(tStrTime), 0, 0)
            ElseIf nScaleX * 7200 > TO_WIDTH_X Then '2時間間隔。
                nTO = 7200
                sFormat = "hh:nn"
                tTime = DateSerial(Year(tStrTime), Month(tStrTime), Day(tStrTime)) + TimeSerial(0, 0, 0)
            ElseIf nScaleX * 10800 > TO_WIDTH_X Then '3時間間隔。
                nTO = 10800
                sFormat = "hh:nn"
                tTime = DateSerial(Year(tStrTime), Month(tStrTime), Day(tStrTime)) + TimeSerial(0, 0, 0)
            ElseIf nScaleX * 14400 > TO_WIDTH_X Then '4時間間隔。
                nTO = 14400
                sFormat = "mm/dd" & vbCrLf & "hh:nn"
                tTime = DateSerial(Year(tStrTime), Month(tStrTime), Day(tStrTime)) + TimeSerial(0, 0, 0)
            ElseIf nScaleX * 21600 > TO_WIDTH_X Then '6時間間隔。
                nTO = 21600
                sFormat = "mm/dd" & vbCrLf & "hh:nn"
                tTime = DateSerial(Year(tStrTime), Month(tStrTime), Day(tStrTime)) + TimeSerial(0, 0, 0)
            ElseIf nScaleX * 28800 > TO_WIDTH_X Then '8時間間隔。
                nTO = 28800
                sFormat = "mm/dd" & vbCrLf & "hh:nn"
                tTime = DateSerial(Year(tStrTime), Month(tStrTime), Day(tStrTime)) + TimeSerial(0, 0, 0)
            ElseIf nScaleX * 43200 > TO_WIDTH_X Then '12時間間隔。
                nTO = 43200
                sFormat = "mm/dd" & vbCrLf & "hh:nn"
                tTime = DateSerial(Year(tStrTime), Month(tStrTime), Day(tStrTime)) + TimeSerial(0, 0, 0)
            ElseIf nScaleX * 86400 > TO_WIDTH_X Then '1日間隔。
                nTO = 86400
                sFormat = "m/d"
                tTime = DateSerial(Year(tStrTime), Month(tStrTime), Day(tStrTime)) + TimeSerial(0, 0, 0)
            ElseIf nScaleX * 172800 > TO_WIDTH_X Then '2日間隔。
                nTO = 172800
                sFormat = "m/d"
                tTime = DateSerial(Year(tStrTime), Month(tStrTime), 1) + TimeSerial(0, 0, 0)
            ElseIf nScaleX * 345600 > TO_WIDTH_X Then '4日間隔。
                nTO = 345600
                sFormat = "m/d"
                tTime = DateSerial(Year(tStrTime), Month(tStrTime), 1) + TimeSerial(0, 0, 0)
            Else '7日間隔。
                nTO = 604800
                sFormat = "m/d"
                tTime = DateSerial(Year(tStrTime), Month(tStrTime), 1) + TimeSerial(0, 0, 0)
            End If
    
            'ラベル、グリッド。
            Do While DateDiff("s", tTime, tEndTime) > 0
                If DateDiff("s", tTime, tStrTime) <= 0 Then
                    Dim nLeft As Double
                    nLeft = nGraphLeft + DateDiff("s", tStrTime, tTime) * nScaleX
                    objCanvas.PenStyle = ddLSDash
                    Call objCanvas.DrawLine(nLeft, nGraphTop, nLeft, nGraphTop + nGraphHeight)
            
                    If bLabel Then
                        objCanvas.PenStyle = ddLSSolid
                        objCanvas.Alignment = ddTXCenter
                        Call objCanvas.DrawLine(nLeft, nGraphTop + nGraphHeight, nLeft, nGraphTop + nGraphHeight + LABEL_OFFSET_Y)
                        Dim sLabel As String
                        sLabel = Format$(tTime, sFormat)
                        Dim nW As Long
                        Dim nH As Long
                        Call objCanvas.MeasureText(sLabel, nW, nH)
                        Call objCanvas.DrawText(sLabel, nLeft - nW* 0.5, nGraphTop + nGraphHeight + LABEL_OFFSET_Y, nW, nH)
                    End If
                End If
                'インクリメント。
                tTime = DateAdd("s", nTO, tTime)
            Loop

            DrawGridX = nScaleX


        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '出力観測点のリストを取得する。
        '
        'clsAccountParam で指定される条件に従い clsRepresentPointHead の中から帳票に出力する観測点を選択する。
        '
        '引き数：
        'clsRepresentPointHead 代表観測点リストのヘッダー。要素は ObservationPoint オブジェクト(代表観測点)。
        'clsAccountParam 帳票パラメータ。
        '
        '戻り値：出力観測点(代表観測点)のリストを返す。配列の要素は(-1 To ...)、要素 -1 は未使用。
        Public Function GetAccountObservationPoints(ByVal clsRepresentPointHead As ChainList, ByVal clsAccountParam As AccountParam) As ObservationPoint()

            '観測点リスト。
            Dim clsObservationPoints() As ObservationPoint
            ReDim clsObservationPoints(-1 To -1)
            Dim clsChainList As ChainList
            Set clsChainList = clsRepresentPointHead
            If clsAccountParam.RangeType = RANGE_TYPE_SESSION Then
                Do While Not clsChainList Is Nothing
                    If clsChainList.Element.Enable And clsChainList.Element.Session = clsAccountParam.Session And Not clsChainList.Element.Genuine Then
                        ReDim Preserve clsObservationPoints(-1 To UBound(clsObservationPoints) + 1)
                        Set clsObservationPoints(UBound(clsObservationPoints)) = clsChainList.Element
                End If
                    Set clsChainList = clsChainList.NextList
                Loop
            ElseIf clsAccountParam.RangeType = RANGE_TYPE_OBJECT Then
                Do While Not clsChainList Is Nothing
                    If clsChainList.Element.Enable And Not clsChainList.Element.Genuine Then
                        Dim vItem As Variant
                        If LookupCollectionVariant(clsAccountParam.SelectedObjects, vItem, clsChainList.Element.Key) Then
                            ReDim Preserve clsObservationPoints(-1 To UBound(clsObservationPoints) + 1)
                            Set clsObservationPoints(UBound(clsObservationPoints)) = clsChainList.Element
                        End If
                    End If
                    Set clsChainList = clsChainList.NextList
                Loop
            Else
                Do While Not clsChainList Is Nothing
                    If clsChainList.Element.Enable And Not clsChainList.Element.Genuine Then
                        ReDim Preserve clsObservationPoints(-1 To UBound(clsObservationPoints) + 1)
                        Set clsObservationPoints(UBound(clsObservationPoints)) = clsChainList.Element
                    End If
                    Set clsChainList = clsChainList.NextList
                Loop
            End If
    
            '観測点番号でソート。
            If UBound(clsObservationPoints) >= 0 Then
                Dim clsObservationPointSessionSorter As New ObservationPointSessionSorter
                Call clsObservationPointSessionSorter.SortArray(clsObservationPoints)
            End If


            GetAccountObservationPoints = clsObservationPoints

        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '出力基線ベクトルのリストを取得する。
        '
        'clsAccountParam で指定される条件に従い clsBaseLineVectorHead の中から帳票に出力する基線ベクトルを選択する。
        '
        '引き数：
        'clsBaseLineVectorHead 基線ベクトルリストのヘッダー。要素は BaseLineVector オブジェクト。
        'clsAccountParam 帳票パラメータ。
        'clsBaseLineVectorSorter ソートに使用する ObjectSortInterface オブジェクト。
        '
        '戻り値：出力基線ベクトルのリストを返す。配列の要素は(-1 To ...)、要素 -1 は未使用。
        Public Function GetAccountBaseLineVectors(ByVal clsBaseLineVectorHead As ChainList, ByVal clsAccountParam As AccountParam, ByVal clsBaseLineVectorSorter As Object) As BaseLineVector()

            '基線ベクトルリスト。
            Dim clsBaseLineVectors() As BaseLineVector
            ReDim clsBaseLineVectors(-1 To -1)
            Dim clsChainList As ChainList
            Set clsChainList = clsBaseLineVectorHead
            If clsAccountParam.RangeType = RANGE_TYPE_SESSION Then
                Do While Not clsChainList Is Nothing
                    If clsChainList.Element.Enable And clsChainList.Element.Session = clsAccountParam.Session And clsChainList.Element.Analysis<ANALYSIS_STATUS_FAILED Then
                        ReDim Preserve clsBaseLineVectors(-1 To UBound(clsBaseLineVectors) + 1)
                        Set clsBaseLineVectors(UBound(clsBaseLineVectors)) = clsChainList.Element
                End If
                    Set clsChainList = clsChainList.NextList
                Loop
            ElseIf clsAccountParam.RangeType = RANGE_TYPE_OBJECT Then
                Do While Not clsChainList Is Nothing
                    If clsChainList.Element.Enable And clsChainList.Element.Analysis<ANALYSIS_STATUS_FAILED Then
                        Dim vItem As Variant
                        If LookupCollectionVariant(clsAccountParam.SelectedObjects, vItem, clsChainList.Element.Key) Then
                            ReDim Preserve clsBaseLineVectors(-1 To UBound(clsBaseLineVectors) + 1)
                            Set clsBaseLineVectors(UBound(clsBaseLineVectors)) = clsChainList.Element
                        End If
                    End If
                    Set clsChainList = clsChainList.NextList
                Loop
            Else
                Do While Not clsChainList Is Nothing
                    If clsChainList.Element.Enable And clsChainList.Element.Analysis<ANALYSIS_STATUS_FAILED Then
                        ReDim Preserve clsBaseLineVectors(-1 To UBound(clsBaseLineVectors) + 1)
                        Set clsBaseLineVectors(UBound(clsBaseLineVectors)) = clsChainList.Element
                    End If
                    Set clsChainList = clsChainList.NextList
                Loop
            End If
    
            '観測点番号でソート。
            If UBound(clsBaseLineVectors) >= 0 Then Call clsBaseLineVectorSorter.SortArray(clsBaseLineVectors)

            GetAccountBaseLineVectors = clsBaseLineVectors


        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //==========================================================================================
    }
}
