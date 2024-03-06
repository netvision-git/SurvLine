using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using System.Windows.Forms;
using static SurvLine.mdl.MdlNSDefine;

namespace SurvLine.mdl
{
    public class MdlAccountMake
    {
#if false
        //'オブジェクトモード。
        public enum OBJ_MODE
        {
            OBJ_MODE_ADOPT = 0,     //'採用。
            OBJ_MODE_CHECK,          //'点検。
            OBJ_MODE_DUPLICATE,     //'重複。
            OBJ_MODE_HALF_FST,      //'前半。   '2023/06/26 Hitz H.Nakamura GNSS水準測量対応。前半後半較差の追加。
            OBJ_MODE_HALF_LST,      //'後半。   '2023/06/26 Hitz H.Nakamura GNSS水準測量対応。前半後半較差の追加。
            //End Enum
        }
#endif

        //'範囲タイプ。
        public enum RANGE_TYPE
        {
            RANGE_TYPE_ALL,      //'プロジェクト全体。
            RANGE_TYPE_SESSION,  //'セッション指定。
            RANGE_TYPE_OBJECT,   //'オブジェクト指定。
        }

        //'選択オブジェクトタイプ。
        public enum SELECTED_OBJECT_TYPE
        {
            SELECTED_OBJECT_TYPE_UNKNOWN,           //'不明。
            SELECTED_OBJECT_TYPE_OBSERVATION,       //'観測点。
            SELECTED_OBJECT_TYPE_BASELINEVECTOR,    //'基線ベクトル。
            SELECTED_OBJECT_TYPE_SESSION,           //'セッション。
        }
#if true
        //'用紙サイズ幅。
        public const float PAPER_WID_LETTER = (float)8.5 * 1440;                //'レター、8 1/2 x 11 インチ
        public const float PAPER_WID_LETTERSMALL = (float)8.5 * 1400;           //'レター スモール、8 1/2 x 11 インチ
        public const float PAPER_WID_TABLOID = 11 * 1440;                       //'タブロイド、11 x 17 インチ
        public const float PAPER_WID_LEDGER = 11 * 1440;                        //'レジャー、17 x 11 インチ
        public const float PAPER_WID_LEGAL = (float)8.5 * 1440;                 //'リーガル、8 1/2 x 14 インチ
        public const float PAPER_WID_STATEMENT = (float)5.5 * 1440;             //'ステートメント、5 1/2 x 8 1/2 インチ
        public const float PAPER_WID_EXECUTIVE = (float)7.5 * 1440;             //'エグゼクティブ、7 1/2 x 10 1/2 インチ
        public const float PAPER_WID_A3 = (float)29.7 * 567;                    //'A3, 297 x 420 mm
        public const float PAPER_WID_A4 = 21 * 567;                             //'A4, 210 x 297 mm
        public const float PAPER_WID_A4SMALL = 21 * 567;                        //'A4 Small, 210 x 297 mm
        public const float PAPER_WID_A5 = (float)14.8 * 567;                    //'A5, 148 x 210 mm
        public const float PAPER_WID_B4 = 25 * 567;                             //'B4, 250 x 354 mm
        public const float PAPER_WID_B5 = (float)18.2 * 567;                    //'B5, 182 x 257 mm
        public const float PAPER_WID_FOLIO = (float)8.5 * 1440;                 //'フォリオ、8 1/2 x 13 インチ
        public const float PAPER_WID_QUARTO = (float)21.5 * 567;                //'クォート、215 x 275 mm
        public const float PAPER_WID_10X14 = 10 * 1440;                         //'10 x 14 インチ
        public const float PAPER_WID_11X17 = 11 * 1440;                         //'11 x 17 インチ
        public const float PAPER_WID_NOTE = (float)8.5 * 1440;                  //'ノート、8 1/2 x 11 インチ
        public const float PAPER_WID_ENV9 = 31 / 8 * 1440;                      //'封筒 #9、3 7/8 x 8 7/8 インチ
        public const float PAPER_WID_ENV10 = 33 / 8 * 1440;                     //'封筒 #10、4 1/8 x 9 1/2 インチ
        public const float PAPER_WID_ENV11 = (float)4.5 * 1440;                 //'封筒 #11、4 1/2 x 10 3/8 インチ
        public const float PAPER_WID_ENV12 = (float)4.5 * 1440;                 //'封筒 #12、4 1/2 x 11 インチ
        public const float PAPER_WID_ENV14 = 5 * 1440;                          //'封筒 #14、5 x 11 1/2 インチ
        public const float PAPER_WID_CSHEET = 0;                                //'C サイズ シート
        public const float PAPER_WID_DSHEET = 0;                                //'D サイズ シート
        public const float PAPER_WID_ESHEET = 0;                                //'E サイズ シート
        public const float PAPER_WID_ENVDL = 11 * 567;                          //'封筒 DL、110 x 220 mm
        public const float PAPER_WID_ENVC5 = (float)16.2 * 567;                 //'封筒 C5、162 x 229 mm
        public const float PAPER_WID_ENVC3 = (float)32.4 * 567;                 //'封筒 C3、324 x 458 mm
        public const float PAPER_WID_ENVC4 = (float)22.9 * 567;                 //'封筒 C4、229 x 324 mm
        public const float PAPER_WID_ENVC6 = (float)11.4 * 567;                 //'封筒 C6、114 x 162 mm
        public const float PAPER_WID_ENVC65 = (float)11.4 * 567;                //'封筒 C65、114 x 229 mm
        public const float PAPER_WID_ENVB4 = 25 * 567;                          //'封筒 B4、250 x 353 mm
        public const float PAPER_WID_ENVB5 = (float)17.6 * 567;                 //'封筒 B5、176 x 250 mm
        public const float PAPER_WID_ENVB6 = (float)17.6 * 567;                 //'封筒 B6、176 x 125 mm
        public const float PAPER_WID_ENVITALY = 11 * 567;                       //'封筒、110 x 230 mm
        public const float PAPER_WID_ENVMONARCH = 31 / 8 * 1440;                //'封筒 Monarch、3 7/8 x 7 1/2 インチ
        public const float PAPER_WID_ENVPERSONAL = 29 / 8 * 1440;               //'封筒、3 5/8 x 6 1/2 インチ
        public const float PAPER_WID_FANFOLDUS = 119 / 8 * 1440;                //'US スタンダード ファンフォールド、14 7/8 x 11 インチ
        public const float PAPER_WID_FANFOLDSTDGERMAN = (float)8.5 * 1440;      //'ドイツ スタンダード ファンフォールド、8 1/2 x 12 インチ
        public const float PAPER_WID_FANFOLDLGLGERMAN = (float)8.5 * 1440;      //'ドイツ リーガル ファンフォールド、8 1/2 x 13 インチ
        public const float PAPER_WID_USER = 0;                                  //'ユーザー定義のサイズ

        //'用紙サイズ高さ。
        public const float PAPER_HEI_LETTER = 11 * 1440;                        //'レター、8 1/2 x 11 インチ
        public const float PAPER_HEI_LETTERSMALL = 11 * 1400;                   //'レター スモール、8 1/2 x 11 インチ
        public const float PAPER_HEI_TABLOID = 17 * 1440;                       //'タブロイド、11 x 17 インチ
        public const float PAPER_HEI_LEDGER = 17 * 1440;                        //'レジャー、17 x 11 インチ
        public const float PAPER_HEI_LEGAL = 14 * 1440;                         //'リーガル、8 1/2 x 14 インチ
        public const float PAPER_HEI_STATEMENT = (float)8.5 * 1440;             //'ステートメント、5 1/2 x 8 1/2 インチ
        public const float PAPER_HEI_EXECUTIVE = (float)10.5 * 1440;            //'エグゼクティブ、7 1/2 x 10 1/2 インチ
        public const float PAPER_HEI_A3 = 42 * 567;                             //'A3, 297 x 420 mm
        public const float PAPER_HEI_A4 = (float)29.7 * 567;                    //'A4, 210 x 297 mm
        public const float PAPER_HEI_A4SMALL = (float)29.7 * 567;               //'A4 Small, 210 x 297 mm
        public const float PAPER_HEI_A5 = 21 * 567;                             //'A5, 148 x 210 mm
        public const float PAPER_HEI_B4 = (float)35.4 * 567;                    //'B4, 250 x 354 mm
        public const float PAPER_HEI_B5 = (float)25.7 * 567;                    //'B5, 182 x 257 mm
        public const float PAPER_HEI_FOLIO = 13 * 1440;                         //'フォリオ、8 1/2 x 13 インチ
        public const float PAPER_HEI_QUARTO = (float)27.5 * 567;                //'クォート、215 x 275 mm
        public const float PAPER_HEI_10X14 = 14 * 1440;                         //'10 x 14 インチ
        public const float PAPER_HEI_11X17 = 17 * 1440;                         //'11 x 17 インチ
        public const float PAPER_HEI_NOTE = 11 * 1440;                          //'ノート、8 1/2 x 11 インチ
        public const float PAPER_HEI_ENV9 = 71 / 8 * 1440;                      //'封筒 #9、3 7/8 x 8 7/8 インチ
        public const float PAPER_HEI_ENV10 = (float)9.5 * 1440;                 //'封筒 #10、4 1/8 x 9 1/2 インチ
        public const float PAPER_HEI_ENV11 = 83 / 8 * 1440;                     //'封筒 #11、4 1/2 x 10 3/8 インチ
        public const float PAPER_HEI_ENV12 = 11 * 1440;                         //'封筒 #12、4 1/2 x 11 インチ
        public const float PAPER_HEI_ENV14 = (float)11.5 * 1440;                //'封筒 #14、5 x 11 1/2 インチ
        public const float PAPER_HEI_CSHEET = 0;                                //'C サイズ シート
        public const float PAPER_HEI_DSHEET = 0;                                //'D サイズ シート
        public const float PAPER_HEI_ESHEET = 0;                                //'E サイズ シート
        public const float PAPER_HEI_ENVDL = 22 * 567;                          //'封筒 DL、110 x 220 mm
        public const float PAPER_HEI_ENVC5 = (float)22.9 * 567;                 //'封筒 C5、162 x 229 mm
        public const float PAPER_HEI_ENVC3 = (float)45.8 * 567;                 //'封筒 C3、324 x 458 mm
        public const float PAPER_HEI_ENVC4 = (float)32.4 * 567;                 //'封筒 C4、229 x 324 mm
        public const float PAPER_HEI_ENVC6 = (float)16.2 * 567;                 //'封筒 C6、114 x 162 mm
        public const float PAPER_HEI_ENVC65 = (float)22.9 * 567;                //'封筒 C65、114 x 229 mm
        public const float PAPER_HEI_ENVB4 = (float)35.3 * 567;                 //'封筒 B4、250 x 353 mm
        public const float PAPER_HEI_ENVB5 = 25 * 567;                          //'封筒 B5、176 x 250 mm
        public const float PAPER_HEI_ENVB6 = (float)12.5 * 567;                 //'封筒 B6、176 x 125 mm
        public const float PAPER_HEI_ENVITALY = 23 * 567;                       //'封筒、110 x 230 mm
        public const float PAPER_HEI_ENVMONARCH = (float)7.5 * 1440;            //'封筒 Monarch、3 7/8 x 7 1/2 インチ
        public const float PAPER_HEI_ENVPERSONAL = (float)6.5 * 1440;           //'封筒、3 5/8 x 6 1/2 インチ
        public const float PAPER_HEI_FANFOLDUS = 11 * 1440;                     //'US スタンダード ファンフォールド、14 7/8 x 11 インチ
        public const float PAPER_HEI_FANFOLDSTDGERMAN = 12 * 1440;              //'ドイツ スタンダード ファンフォールド、8 1/2 x 12 インチ
        public const float PAPER_HEI_FANFOLDLGLGERMAN = 13 * 1440;              //'ドイツ リーガル ファンフォールド、8 1/2 x 13 インチ
        public const float PAPER_HEI_USER = 0;                                  //'ユーザー定義のサイズ

        //'時間帯。
        public const string DISP_TIME_ZONE_UTC = "UTC";
        public const string DISP_TIME_ZONE_JST = "JST";
        public string[] DISP_TIME_ZONE = new string[(int)TIME_ZONE.TIME_ZONE_COUNT];

        //'書式。
        public const string ACCOUNT_FORMAT_MIN = "00";                          //'座標。分。
        public const string ACCOUNT_FORMAT_SEC = "00";                          //'座標、秒。
        public const string ACCOUNT_FORMAT_DISPERSION = "0.00000E+000";         //'分散･共分散。

#endif
        //'少数点以下桁数。
        public const long ACCOUNT_DECIMAL_SEC = 4;                              //As Long = 4 '座標、秒。
        public const long ACCOUNT_DECIMAL_SEC_L = 5;                            //As Long = 5 '座標、秒。
        public const long ACCOUNT_DECIMAL_ALT = 3;                              //As Long = 3 '標高。
        public const long ACCOUNT_DECIMAL_GEOIDO = 3;                           //As Long = 3 'ジオイド高。
        public const long ACCOUNT_DECIMAL_HEIGHT = 3;                           //As Long = 3 '楕円体高。
        public const long ACCOUNT_DECIMAL_DISTANCE = 3;                         //As Long = 3 '斜距離。
        public const long ACCOUNT_DECIMAL_XYZ = 3;                              //As Long = 3 '地心直交。
        public const long ACCOUNT_DECIMAL_ANGLE = 2;                            //As Long = 2 '角度、秒。
        public const long ACCOUNT_DECIMAL_ANTHEIGHT = 3;                        //As Long = 3 'アンテナ高。
        public const long ACCOUNT_DECIMAL_DEGREE = 8;                           //As Long = 8 '座標。度。
        public const long ACCOUNT_DECIMAL_RADIAN = 8;                           // As Long = 8 '座標。ラジアン。
        public const long ACCOUNT_DECIMAL_VECTOR = 4;                           //As Long = 4 'ベクトル（三次元網平均計算簿の「基線ベクトルの平均値」用） 2006/9/1 NGS Yamada。

        public const string GEODETIC_REFERENCE_SYSTEM = "世界測地系";           //As String = "世界測地系"








        //==========================================================================================
        /*[VB]
        'セクションのフォーマットを調整する。
        '
        'CanGrowがONであるコントロールの高さを評価し、高さが拡張されていたらそれに合わせてフレームと罫線の高さを拡張する。
        '
        '引き数：
        'ar 対象オブジェクト。
        'objCtrl 高さを評価するコントロール。
        'objFrame フレーム。
        'linBorders 罫線。参照する配列の要素は(0 To ...)。Ubound 関数を使用する。
        Public Sub AdjustDetailSectionFormatSimple(ByVal ar As ActiveReport, ByRef objCtrl As Object, ByVal objFrame As Object, ByRef linBorders() As DDActiveReports2.Line)
            Dim objCtrls(0) As Object
            Set objCtrls(0) = objCtrl
            Call AdjustDetailSectionFormat(ar, objCtrls, objFrame, linBorders)
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'セクションのフォーマットを調整する。
        '
        'CanGrowがONであるコントロールの高さを評価し、高さが拡張されていたらそれに合わせてフレームと罫線の高さを拡張する。
        '高さを評価するコントロールが複数である場合に対応。
        '
        '引き数：
        'ar 対象オブジェクト。
        'objCtrls 高さを評価するコントロール。参照する配列の要素は(0 To ...)。Ubound 関数を使用する。
        'objFrame フレーム。
        'linBorders 罫線。参照する配列の要素は(0 To ...)。Ubound 関数を使用する。
        Public Sub AdjustDetailSectionFormat(ByVal ar As ActiveReport, ByRef objCtrls() As Object, ByVal objFrame As Object, ByRef linBorders() As DDActiveReports2.Line)
            '底辺位置。
            Dim nBottom As Double
            Dim i As Long
            nBottom = 0
            For i = UBound(objCtrls) To 0 Step -1 '降順なのは速度最適化のため。他に意味無し。
                If nBottom<objCtrls(i).Top + objCtrls(i).Height Then nBottom = objCtrls(i).Top + objCtrls(i).Height
            Next
            '高さ補正。
            If objFrame.Top + objFrame.Height<nBottom Then
                '枠。
                objFrame.Height = nBottom - objFrame.Top
                '境界線。
                For i = UBound(linBorders) To 0 Step -1 '降順なのは速度最適化のため。他に意味無し。
                    linBorders(i).Y2 = nBottom
                Next
            End If
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '用紙サイズ。
        '
        '指定された用紙の幅を取得する。
        '縦置き時のサイズ。
        '
        '引き数：
        'vbPRPS 用紙種類。
        '
        '戻り値：用紙の幅(Twips)。
        Public Function PAPER_WID(ByVal vbPRPS As Long) As Single
            Select Case vbPRPS
            Case vbPRPSLetter
                PAPER_WID = PAPER_WID_LETTER
            Case vbPRPSLetterSmall
                PAPER_WID = PAPER_WID_LETTERSMALL
            Case vbPRPSTabloid
                PAPER_WID = PAPER_WID_TABLOID
            Case vbPRPSLedger
                PAPER_WID = PAPER_WID_LEDGER
            Case vbPRPSLegal
                PAPER_WID = PAPER_WID_LEGAL
            Case vbPRPSStatement
                PAPER_WID = PAPER_WID_STATEMENT
            Case vbPRPSExecutive
                PAPER_WID = PAPER_WID_EXECUTIVE
            Case vbPRPSA3
                PAPER_WID = PAPER_WID_A3
            Case vbPRPSA4
                PAPER_WID = PAPER_WID_A4
            Case vbPRPSA4Small
                PAPER_WID = PAPER_WID_A4SMALL
            Case vbPRPSA5
                PAPER_WID = PAPER_WID_A5
            Case vbPRPSB4
                PAPER_WID = PAPER_WID_B4
            Case vbPRPSB5
                PAPER_WID = PAPER_WID_B5
            Case vbPRPSFolio
                PAPER_WID = PAPER_WID_FOLIO
            Case vbPRPSQuarto
                PAPER_WID = PAPER_WID_QUARTO
            Case vbPRPS10x14
                PAPER_WID = PAPER_WID_10X14
            Case vbPRPS11x17
                PAPER_WID = PAPER_WID_11X17
            Case vbPRPSNote
                PAPER_WID = PAPER_WID_NOTE
            Case vbPRPSEnv9
                PAPER_WID = PAPER_WID_ENV9
            Case vbPRPSEnv10
                PAPER_WID = PAPER_WID_ENV10
            Case vbPRPSEnv11
                PAPER_WID = PAPER_WID_ENV11
            Case vbPRPSEnv12
                PAPER_WID = PAPER_WID_ENV12
            Case vbPRPSEnv14
                PAPER_WID = PAPER_WID_ENV14
            Case vbPRPSCSheet
                PAPER_WID = PAPER_WID_CSHEET
            Case vbPRPSDSheet
                PAPER_WID = PAPER_WID_DSHEET
            Case vbPRPSESheet
                PAPER_WID = PAPER_WID_ESHEET
            Case vbPRPSEnvDL
                PAPER_WID = PAPER_WID_ENVDL
            Case vbPRPSEnvC5
                PAPER_WID = PAPER_WID_ENVC5
            Case vbPRPSEnvC3
                PAPER_WID = PAPER_WID_ENVC3
            Case vbPRPSEnvC4
                PAPER_WID = PAPER_WID_ENVC4
            Case vbPRPSEnvC6
                PAPER_WID = PAPER_WID_ENVC6
            Case vbPRPSEnvC65
                PAPER_WID = PAPER_WID_ENVC65
            Case vbPRPSEnvB4
                PAPER_WID = PAPER_WID_ENVB4
            Case vbPRPSEnvB5
                PAPER_WID = PAPER_WID_ENVB5
            Case vbPRPSEnvB6
                PAPER_WID = PAPER_WID_ENVB6
            Case vbPRPSEnvItaly
                PAPER_WID = PAPER_WID_ENVITALY
            Case vbPRPSEnvMonarch
                PAPER_WID = PAPER_WID_ENVMONARCH
            Case vbPRPSEnvPersonal
                PAPER_WID = PAPER_WID_ENVPERSONAL
            Case vbPRPSFanfoldUS
                PAPER_WID = PAPER_WID_FANFOLDUS
            Case vbPRPSFanfoldStdGerman
                PAPER_WID = PAPER_WID_FANFOLDSTDGERMAN
            Case vbPRPSFanfoldLglGerman
                PAPER_WID = PAPER_WID_FANFOLDLGLGERMAN
            Case vbPRPSUser
                PAPER_WID = PAPER_WID_USER
            End Select
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '用紙サイズ。
        '
        '指定された用紙の高さを取得する。
        '縦置き時のサイズ。
        '
        '引き数：
        'vbPRPS 用紙種類。
        '
        '戻り値：用紙の高さ(Twips)。
        Public Function PAPER_HEI(ByVal vbPRPS As Long) As Single
            Select Case vbPRPS
            Case vbPRPSLetter
                PAPER_HEI = PAPER_HEI_LETTER
            Case vbPRPSLetterSmall
                PAPER_HEI = PAPER_HEI_LETTERSMALL
            Case vbPRPSTabloid
                PAPER_HEI = PAPER_HEI_TABLOID
            Case vbPRPSLedger
                PAPER_HEI = PAPER_HEI_LEDGER
            Case vbPRPSLegal
                PAPER_HEI = PAPER_HEI_LEGAL
            Case vbPRPSStatement
                PAPER_HEI = PAPER_HEI_STATEMENT
            Case vbPRPSExecutive
                PAPER_HEI = PAPER_HEI_EXECUTIVE
            Case vbPRPSA3
                PAPER_HEI = PAPER_HEI_A3
            Case vbPRPSA4
                PAPER_HEI = PAPER_HEI_A4
            Case vbPRPSA4Small
                PAPER_HEI = PAPER_HEI_A4SMALL
            Case vbPRPSA5
                PAPER_HEI = PAPER_HEI_A5
            Case vbPRPSB4
                PAPER_HEI = PAPER_HEI_B4
            Case vbPRPSB5
                PAPER_HEI = PAPER_HEI_B5
            Case vbPRPSFolio
                PAPER_HEI = PAPER_HEI_FOLIO
            Case vbPRPSQuarto
                PAPER_HEI = PAPER_HEI_QUARTO
            Case vbPRPS10x14
                PAPER_HEI = PAPER_HEI_10X14
            Case vbPRPS11x17
                PAPER_HEI = PAPER_HEI_11X17
            Case vbPRPSNote
                PAPER_HEI = PAPER_HEI_NOTE
            Case vbPRPSEnv9
                PAPER_HEI = PAPER_HEI_ENV9
            Case vbPRPSEnv10
                PAPER_HEI = PAPER_HEI_ENV10
            Case vbPRPSEnv11
                PAPER_HEI = PAPER_HEI_ENV11
            Case vbPRPSEnv12
                PAPER_HEI = PAPER_HEI_ENV12
            Case vbPRPSEnv14
                PAPER_HEI = PAPER_HEI_ENV14
            Case vbPRPSCSheet
                PAPER_HEI = PAPER_HEI_CSHEET
            Case vbPRPSDSheet
                PAPER_HEI = PAPER_HEI_DSHEET
            Case vbPRPSESheet
                PAPER_HEI = PAPER_HEI_ESHEET
            Case vbPRPSEnvDL
                PAPER_HEI = PAPER_HEI_ENVDL
            Case vbPRPSEnvC5
                PAPER_HEI = PAPER_HEI_ENVC5
            Case vbPRPSEnvC3
                PAPER_HEI = PAPER_HEI_ENVC3
            Case vbPRPSEnvC4
                PAPER_HEI = PAPER_HEI_ENVC4
            Case vbPRPSEnvC6
                PAPER_HEI = PAPER_HEI_ENVC6
            Case vbPRPSEnvC65
                PAPER_HEI = PAPER_HEI_ENVC65
            Case vbPRPSEnvB4
                PAPER_HEI = PAPER_HEI_ENVB4
            Case vbPRPSEnvB5
                PAPER_HEI = PAPER_HEI_ENVB5
            Case vbPRPSEnvB6
                PAPER_HEI = PAPER_HEI_ENVB6
            Case vbPRPSEnvItaly
                PAPER_HEI = PAPER_HEI_ENVITALY
            Case vbPRPSEnvMonarch
                PAPER_HEI = PAPER_HEI_ENVMONARCH
            Case vbPRPSEnvPersonal
                PAPER_HEI = PAPER_HEI_ENVPERSONAL
            Case vbPRPSFanfoldUS
                PAPER_HEI = PAPER_HEI_FANFOLDUS
            Case vbPRPSFanfoldStdGerman
                PAPER_HEI = PAPER_HEI_FANFOLDSTDGERMAN
            Case vbPRPSFanfoldLglGerman
                PAPER_HEI = PAPER_HEI_FANFOLDLGLGERMAN
            Case vbPRPSUser
                PAPER_HEI = PAPER_HEI_USER
            End Select
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '地籍情報の書式化。
        '
        '指定された帳票パラメータに従い、地籍情報文字列を作成する。
        '地籍情報の書式は、｢0000年00月00日　　記入者 ○○○　　点検者 ○○○｣。
        '
        '引き数：
        'clsAccountCadastralParam 帳票パラメータ。
        '
        '戻り値：書式化された地籍情報の文字列。
        Public Function CadastralFormat(ByVal clsAccountCadastralParam As Object) As String
            Dim sText As String
            sText = Format$(clsAccountCadastralParam.ADate, "yyyy年mm月dd日　　記入者 ")
            If Len(clsAccountCadastralParam.Writer) > 4 Then
                sText = sText & clsAccountCadastralParam.Writer
            Else
                sText = sText & Format$(clsAccountCadastralParam.Writer, "!@@@@&;    ")
            End If
            sText = sText & "　　点検者 "
            If Len(clsAccountCadastralParam.Checker) > 4 Then
                sText = sText & clsAccountCadastralParam.Checker
            Else
                sText = sText & Format$(clsAccountCadastralParam.Checker, "!@@@@&;    ")
            End If
            CadastralFormat = sText
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //==========================================================================================
    }
}
