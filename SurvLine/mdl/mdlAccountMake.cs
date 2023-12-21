using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTS
{
    public class MdlAccountMake
    {
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
#if false
        //'用紙サイズ幅。
        Public Const PAPER_WID_LETTER As Single = 8.5 * 1440 'レター、8 1/2 x 11 インチ
        Public Const PAPER_WID_LETTERSMALL As Single = 8.5 * 1400 'レター スモール、8 1/2 x 11 インチ
        Public Const PAPER_WID_TABLOID As Single = 11 * 1440 'タブロイド、11 x 17 インチ
        Public Const PAPER_WID_LEDGER As Single = 11 * 1440 'レジャー、17 x 11 インチ
        Public Const PAPER_WID_LEGAL As Single = 8.5 * 1440 'リーガル、8 1/2 x 14 インチ
        Public Const PAPER_WID_STATEMENT As Single = 5.5 * 1440 'ステートメント、5 1/2 x 8 1/2 インチ
        Public Const PAPER_WID_EXECUTIVE As Single = 7.5 * 1440 'エグゼクティブ、7 1/2 x 10 1/2 インチ
        Public Const PAPER_WID_A3 As Single = 29.7 * 567 'A3, 297 x 420 mm
        Public Const PAPER_WID_A4 As Single = 21 * 567 'A4, 210 x 297 mm
        Public Const PAPER_WID_A4SMALL As Single = 21 * 567 'A4 Small, 210 x 297 mm
        Public Const PAPER_WID_A5 As Single = 14.8 * 567 'A5, 148 x 210 mm
        Public Const PAPER_WID_B4 As Single = 25 * 567 'B4, 250 x 354 mm
        Public Const PAPER_WID_B5 As Single = 18.2 * 567 'B5, 182 x 257 mm
        Public Const PAPER_WID_FOLIO As Single = 8.5 * 1440 'フォリオ、8 1/2 x 13 インチ
        Public Const PAPER_WID_QUARTO As Single = 21.5 * 567 'クォート、215 x 275 mm
        Public Const PAPER_WID_10X14 As Single = 10 * 1440 '10 x 14 インチ
        Public Const PAPER_WID_11X17 As Single = 11 * 1440 '11 x 17 インチ
        Public Const PAPER_WID_NOTE As Single = 8.5 * 1440 'ノート、8 1/2 x 11 インチ
        Public Const PAPER_WID_ENV9 As Single = 31 / 8 * 1440 '封筒 #9、3 7/8 x 8 7/8 インチ
        Public Const PAPER_WID_ENV10 As Single = 33 / 8 * 1440 '封筒 #10、4 1/8 x 9 1/2 インチ
        Public Const PAPER_WID_ENV11 As Single = 4.5 * 1440 '封筒 #11、4 1/2 x 10 3/8 インチ
        Public Const PAPER_WID_ENV12 As Single = 4.5 * 1440 '封筒 #12、4 1/2 x 11 インチ
        Public Const PAPER_WID_ENV14 As Single = 5 * 1440 '封筒 #14、5 x 11 1/2 インチ
        Public Const PAPER_WID_CSHEET As Single = 0 'C サイズ シート
        Public Const PAPER_WID_DSHEET As Single = 0 'D サイズ シート
        Public Const PAPER_WID_ESHEET As Single = 0 'E サイズ シート
        Public Const PAPER_WID_ENVDL As Single = 11 * 567 '封筒 DL、110 x 220 mm
        Public Const PAPER_WID_ENVC5 As Single = 16.2 * 567 '封筒 C5、162 x 229 mm
        Public Const PAPER_WID_ENVC3 As Single = 32.4 * 567 '封筒 C3、324 x 458 mm
        Public Const PAPER_WID_ENVC4 As Single = 22.9 * 567 '封筒 C4、229 x 324 mm
        Public Const PAPER_WID_ENVC6 As Single = 11.4 * 567 '封筒 C6、114 x 162 mm
        Public Const PAPER_WID_ENVC65 As Single = 11.4 * 567 '封筒 C65、114 x 229 mm
        Public Const PAPER_WID_ENVB4 As Single = 25 * 567 '封筒 B4、250 x 353 mm
        Public Const PAPER_WID_ENVB5 As Single = 17.6 * 567 '封筒 B5、176 x 250 mm
        Public Const PAPER_WID_ENVB6 As Single = 17.6 * 567 '封筒 B6、176 x 125 mm
        Public Const PAPER_WID_ENVITALY As Single = 11 * 567 '封筒、110 x 230 mm
        Public Const PAPER_WID_ENVMONARCH As Single = 31 / 8 * 1440 '封筒 Monarch、3 7/8 x 7 1/2 インチ
        Public Const PAPER_WID_ENVPERSONAL As Single = 29 / 8 * 1440 '封筒、3 5/8 x 6 1/2 インチ
        Public Const PAPER_WID_FANFOLDUS As Single = 119 / 8 * 1440 'US スタンダード ファンフォールド、14 7/8 x 11 インチ
        Public Const PAPER_WID_FANFOLDSTDGERMAN As Single = 8.5 * 1440 'ドイツ スタンダード ファンフォールド、8 1/2 x 12 インチ
        Public Const PAPER_WID_FANFOLDLGLGERMAN As Single = 8.5 * 1440 'ドイツ リーガル ファンフォールド、8 1/2 x 13 インチ
        Public Const PAPER_WID_USER As Single = 0 'ユーザー定義のサイズ

        '用紙サイズ高さ。
        Public Const PAPER_HEI_LETTER As Single = 11 * 1440 'レター、8 1/2 x 11 インチ
        Public Const PAPER_HEI_LETTERSMALL As Single = 11 * 1400 'レター スモール、8 1/2 x 11 インチ
        Public Const PAPER_HEI_TABLOID As Single = 17 * 1440 'タブロイド、11 x 17 インチ
        Public Const PAPER_HEI_LEDGER As Single = 17 * 1440 'レジャー、17 x 11 インチ
        Public Const PAPER_HEI_LEGAL As Single = 14 * 1440 'リーガル、8 1/2 x 14 インチ
        Public Const PAPER_HEI_STATEMENT As Single = 8.5 * 1440 'ステートメント、5 1/2 x 8 1/2 インチ
        Public Const PAPER_HEI_EXECUTIVE As Single = 10.5 * 1440 'エグゼクティブ、7 1/2 x 10 1/2 インチ
        Public Const PAPER_HEI_A3 As Single = 42 * 567 'A3, 297 x 420 mm
        Public Const PAPER_HEI_A4 As Single = 29.7 * 567 'A4, 210 x 297 mm
        Public Const PAPER_HEI_A4SMALL As Single = 29.7 * 567 'A4 Small, 210 x 297 mm
        Public Const PAPER_HEI_A5 As Single = 21 * 567 'A5, 148 x 210 mm
        Public Const PAPER_HEI_B4 As Single = 35.4 * 567 'B4, 250 x 354 mm
        Public Const PAPER_HEI_B5 As Single = 25.7 * 567 'B5, 182 x 257 mm
        Public Const PAPER_HEI_FOLIO As Single = 13 * 1440 'フォリオ、8 1/2 x 13 インチ
        Public Const PAPER_HEI_QUARTO As Single = 27.5 * 567 'クォート、215 x 275 mm
        Public Const PAPER_HEI_10X14 As Single = 14 * 1440 '10 x 14 インチ
        Public Const PAPER_HEI_11X17 As Single = 17 * 1440 '11 x 17 インチ
        Public Const PAPER_HEI_NOTE As Single = 11 * 1440 'ノート、8 1/2 x 11 インチ
        Public Const PAPER_HEI_ENV9 As Single = 71 / 8 * 1440 '封筒 #9、3 7/8 x 8 7/8 インチ
        Public Const PAPER_HEI_ENV10 As Single = 9.5 * 1440 '封筒 #10、4 1/8 x 9 1/2 インチ
        Public Const PAPER_HEI_ENV11 As Single = 83 / 8 * 1440 '封筒 #11、4 1/2 x 10 3/8 インチ
        Public Const PAPER_HEI_ENV12 As Single = 11 * 1440 '封筒 #12、4 1/2 x 11 インチ
        Public Const PAPER_HEI_ENV14 As Single = 11.5 * 1440 '封筒 #14、5 x 11 1/2 インチ
        Public Const PAPER_HEI_CSHEET As Single = 0 'C サイズ シート
        Public Const PAPER_HEI_DSHEET As Single = 0 'D サイズ シート
        Public Const PAPER_HEI_ESHEET As Single = 0 'E サイズ シート
        Public Const PAPER_HEI_ENVDL As Single = 22 * 567 '封筒 DL、110 x 220 mm
        Public Const PAPER_HEI_ENVC5 As Single = 22.9 * 567 '封筒 C5、162 x 229 mm
        Public Const PAPER_HEI_ENVC3 As Single = 45.8 * 567 '封筒 C3、324 x 458 mm
        Public Const PAPER_HEI_ENVC4 As Single = 32.4 * 567 '封筒 C4、229 x 324 mm
        Public Const PAPER_HEI_ENVC6 As Single = 16.2 * 567 '封筒 C6、114 x 162 mm
        Public Const PAPER_HEI_ENVC65 As Single = 22.9 * 567 '封筒 C65、114 x 229 mm
        Public Const PAPER_HEI_ENVB4 As Single = 35.3 * 567 '封筒 B4、250 x 353 mm
        Public Const PAPER_HEI_ENVB5 As Single = 25 * 567 '封筒 B5、176 x 250 mm
        Public Const PAPER_HEI_ENVB6 As Single = 12.5 * 567 '封筒 B6、176 x 125 mm
        Public Const PAPER_HEI_ENVITALY As Single = 23 * 567 '封筒、110 x 230 mm
        Public Const PAPER_HEI_ENVMONARCH As Single = 7.5 * 1440 '封筒 Monarch、3 7/8 x 7 1/2 インチ
        Public Const PAPER_HEI_ENVPERSONAL As Single = 6.5 * 1440 '封筒、3 5/8 x 6 1/2 インチ
        Public Const PAPER_HEI_FANFOLDUS As Single = 11 * 1440 'US スタンダード ファンフォールド、14 7/8 x 11 インチ
        Public Const PAPER_HEI_FANFOLDSTDGERMAN As Single = 12 * 1440 'ドイツ スタンダード ファンフォールド、8 1/2 x 12 インチ
        Public Const PAPER_HEI_FANFOLDLGLGERMAN As Single = 13 * 1440 'ドイツ リーガル ファンフォールド、8 1/2 x 13 インチ
        Public Const PAPER_HEI_USER As Single = 0 'ユーザー定義のサイズ

        '時間帯。
        Public Const DISP_TIME_ZONE_UTC As String = "UTC"
        Public Const DISP_TIME_ZONE_JST As String = "JST"
        Public DISP_TIME_ZONE(TIME_ZONE_COUNT - 1) As String

        '書式。
        Public Const ACCOUNT_FORMAT_MIN As String = "00" '座標。分。
        Public Const ACCOUNT_FORMAT_SEC As String = "00" '座標、秒。
        Public Const ACCOUNT_FORMAT_DISPERSION As String = "0.00000E+000" '分散･共分散。

#endif
        //'少数点以下桁数。
        public const long ACCOUNT_DECIMAL_SEC = 4;          //As Long = 4 '座標、秒。
        public const long ACCOUNT_DECIMAL_SEC_L = 5;        //As Long = 5 '座標、秒。
        public const long ACCOUNT_DECIMAL_ALT = 3;          //As Long = 3 '標高。
        public const long ACCOUNT_DECIMAL_GEOIDO = 3;       //As Long = 3 'ジオイド高。
        public const long ACCOUNT_DECIMAL_HEIGHT = 3;       //As Long = 3 '楕円体高。
        public const long ACCOUNT_DECIMAL_DISTANCE = 3;     //As Long = 3 '斜距離。
        public const long ACCOUNT_DECIMAL_XYZ = 3;          //As Long = 3 '地心直交。
        public const long ACCOUNT_DECIMAL_ANGLE = 2;        //As Long = 2 '角度、秒。
        public const long ACCOUNT_DECIMAL_ANTHEIGHT = 3;    //As Long = 3 'アンテナ高。
        public const long ACCOUNT_DECIMAL_DEGREE = 8;       //As Long = 8 '座標。度。
        public const long ACCOUNT_DECIMAL_RADIAN = 8;       // As Long = 8 '座標。ラジアン。
        public const long ACCOUNT_DECIMAL_VECTOR = 4;       //As Long = 4 'ベクトル（三次元網平均計算簿の「基線ベクトルの平均値」用） 2006/9/1 NGS Yamada。

        public const string GEODETIC_REFERENCE_SYSTEM = "世界測地系";    //As String = "世界測地系"





    }
}
