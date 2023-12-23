using SurvLine;
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


//23/12/22 K.setoguchi@NV---------->>>>>>>>>>
//(del)#if false
        //'用紙サイズ幅。
        public const Single PAPER_WID_LETTER = 8.5f * 1440;                 //As Single'レター、8 1/2 x 11 インチ
        public const Single PAPER_WID_LETTERSMALL = (float)8.5f * 1400;     //As Single 'レター スモール、8 1/2 x 11 インチ
        public const Single PAPER_WID_TABLOID = 11 * 1440;                  //As Single'タブロイド、11 x 17 インチ
        public const Single PAPER_WID_LEDGER = 11 * 1440;                   //As Single'レジャー、17 x 11 インチ
        public const Single PAPER_WID_LEGAL = 8.5f * 1440;                  //As Single'リーガル、8 1/2 x 14 インチ
        public const Single PAPER_WID_STATEMENT = 5.5f * 1440;              //As Single'ステートメント、5 1/2 x 8 1/2 インチ
        public const Single PAPER_WID_EXECUTIVE = 7.5f * 1440;              //As Single 'エグゼクティブ、7 1/2 x 10 1/2 インチ
        public const Single PAPER_WID_A3 = 29.7f * 567;                     //As Single 'A3, 297 x 420 mm
        public const Single PAPER_WID_A4 = 21 * 567;                        //As Single 'A4, 210 x 297 mm
        public const Single PAPER_WID_A4SMALL = 21 * 567;                   //As Single 'A4 Small, 210 x 297 mm
        public const Single PAPER_WID_A5 = 14.8f * 567;                     //As Single 'A5, 148 x 210 mm
        public const Single PAPER_WID_B4 = 25 * 567;                        //As Single 'B4, 250 x 354 mm
        public const Single PAPER_WID_B5 = 18.2f * 567;                     //As Single 'B5, 182 x 257 mm
        public const Single PAPER_WID_FOLIO = 8.5f * 1440;                  //As Single 'フォリオ、8 1/2 x 13 インチ
        public const Single PAPER_WID_QUARTO = 21.5f * 567;                 //As Single 'クォート、215 x 275 mm
        public const Single PAPER_WID_10X14 = 10 * 1440;                    //As Single '10 x 14 インチ
        public const Single PAPER_WID_11X17 = 11 * 1440;                    //As Single '11 x 17 インチ
        public const Single PAPER_WID_NOTE = 8.5f * 1440;                   //As Single 'ノート、8 1/2 x 11 インチ
        public const Single PAPER_WID_ENV9 = 31 / 8 * 1440;                 //As Single '封筒 #9、3 7/8 x 8 7/8 インチ
        public const Single PAPER_WID_ENV10 = 33 / 8 * 1440;                //As Single '封筒 #10、4 1/8 x 9 1/2 インチ
        public const Single PAPER_WID_ENV11 = 4.5f * 1440;                  //As Single '封筒 #11、4 1/2 x 10 3/8 インチ
        public const Single PAPER_WID_ENV12 = 4.5f * 1440;                  //As Single '封筒 #12、4 1/2 x 11 インチ
        public const Single PAPER_WID_ENV14 = 5 * 1440;                     //As Single '封筒 #14、5 x 11 1/2 インチ
        public const Single PAPER_WID_CSHEET = 0;                           //As Single 'C サイズ シート
        public const Single PAPER_WID_DSHEET = 0;                           //As Single 'D サイズ シート
        public const Single PAPER_WID_ESHEET = 0;                           //As Single 'E サイズ シート
        public const Single PAPER_WID_ENVDL = 11 * 567;                     //As Single '封筒 DL、110 x 220 mm
        public const Single PAPER_WID_ENVC5 = 16.2f * 567;                  //As Single 封筒 C5、162 x 229 mm
        public const Single PAPER_WID_ENVC3 = 32.4f * 567;                  //As Single '封筒 C3、324 x 458 mm
        public const Single PAPER_WID_ENVC4 = 22.9f * 567;                  //As Single '封筒 C4、229 x 324 mm
        public const Single PAPER_WID_ENVC6 = 11.4f * 567;                  //As Single '封筒 C6、114 x 162 mm
        public const Single PAPER_WID_ENVC65 = 11.4f * 567;                 //As Single '封筒 C65、114 x 229 mm
        public const Single PAPER_WID_ENVB4 = 25 * 567;                     //As Single '封筒 B4、250 x 353 mm
        public const Single PAPER_WID_ENVB5 = 17.6f * 567;                  //As Single '封筒 B5、176 x 250 mm
        public const Single PAPER_WID_ENVB6 = 17.6f * 567;                  //As Single '封筒 B6、176 x 125 mm
        public const Single PAPER_WID_ENVITALY = 11 * 567;                  //As Single '封筒、110 x 230 mm
        public const Single PAPER_WID_ENVMONARCH = 31 / 8 * 1440;           //As Single '封筒 Monarch、3 7/8 x 7 1/2 インチ
        public const Single PAPER_WID_ENVPERSONAL = 29 / 8 * 1440;          //As Single '封筒、3 5/8 x 6 1/2 インチ
        public const Single PAPER_WID_FANFOLDUS = 119 / 8 * 1440;           //As Single 'US スタンダード ファンフォールド、14 7/8 x 11 インチ
        public const Single PAPER_WID_FANFOLDSTDGERMAN = 8.5f * 1440;       //As Single 'ドイツ スタンダード ファンフォールド、8 1/2 x 12 インチ
        public const Single PAPER_WID_FANFOLDLGLGERMAN = 8.5f * 1440;       //As Single 'ドイツ リーガル ファンフォールド、8 1/2 x 13 インチ
        public const Single PAPER_WID_USER = 0;                             //As Single 'ユーザー定義のサイズ
        //<<<<<<<<<-----------23/12/22 K.setoguchi@NV
        //23/12/22 K.setoguchi@NV---------->>>>>>>>>>
        //'用紙サイズ高さ。
        public const Single PAPER_HEI_LETTER = 11 * 1440;                   //As Single 'レター、8 1/2 x 11 インチ
        public const Single PAPER_HEI_LETTERSMALL = 11 * 1400;              //As Single 'レター スモール、8 1/2 x 11 インチ
        public const Single PAPER_HEI_TABLOID = 17 * 1440;                  //As Single 'タブロイド、11 x 17 インチ
        public const Single PAPER_HEI_LEDGER = 17 * 1440;                   //As Single 'レジャー、17 x 11 インチ
        public const Single PAPER_HEI_LEGAL = 14 * 1440;                    //As Single 'リーガル、8 1/2 x 14 インチ
        public const Single PAPER_HEI_STATEMENT = 8.5f * 1440;              //As Single 'ステートメント、5 1/2 x 8 1/2 インチ
        public const Single PAPER_HEI_EXECUTIVE = 10.5f * 1440;             //As Single 'エグゼクティブ、7 1/2 x 10 1/2 インチ
        public const Single PAPER_HEI_A3 = 42 * 567;                        //As Single 'A3, 297 x 420 mm
        public const Single PAPER_HEI_ = 29.7f * 567;                       //As Single 'A4, 210 x 297 mm
        public const Single PAPER_HEI_A4SMALL = 29.7f * 567;                //As Single 'A4 Small, 210 x 297 mm
        public const Single PAPER_HEI_A5 = 21 * 567;                        //As Single 'A5, 148 x 210 mm
        public const Single PAPER_HEI_B4 = 35.4f * 567;                     //As Single 'B4, 250 x 354 mm
        public const Single PAPER_HEI_B5 = 25.7f * 567;                     //As Single 'B5, 182 x 257 mm
        public const Single PAPER_HEI_FOLIO = 13 * 1440;                    //As Single 'フォリオ、8 1/2 x 13 インチ
        public const Single PAPER_HEI_QUARTO = 27.5f * 567;                 //As Single 'クォート、215 x 275 mm
        public const Single PAPER_HEI_10X14 = 14 * 1440;                    //As Single '10 x 14 インチ
        public const Single PAPER_HEI_11X17 = 17 * 1440;                    //As Single '11 x 17 インチ
        public const Single PAPER_HEI_NOTE = 11 * 1440;                     //As Single 'ノート、8 1/2 x 11 インチ
        public const Single PAPER_HEI_ENV9 = 71 / 8 * 1440;                 //As Single '封筒 #9、3 7/8 x 8 7/8 インチ
        public const Single PAPER_HEI_ENV10 = 9.5f * 1440;                  //As Single '封筒 #10、4 1/8 x 9 1/2 インチ
        public const Single PAPER_HEI_ENV11 = 83 / 8 * 1440;                //As Single '封筒 #11、4 1/2 x 10 3/8 インチ
        public const Single PAPER_HEI_ENV12 = 11 * 1440;                    //As Single '封筒 #12、4 1/2 x 11 インチ
        public const Single PAPER_HEI_ENV14 = 11.5f * 1440;                 //As Single '封筒 #14、5 x 11 1/2 インチ
        public const Single PAPER_HEI_CSHEET = 0;                           //'C サイズ シート
        public const Single PAPER_HEI_DSHEET = 0;                           //'D サイズ シート
        public const Single PAPER_HEI_ESHEET = 0;                           //'E サイズ シート
        public const Single PAPER_HEI_ENVDL = 22 * 567;                     //'封筒 DL、110 x 220 mm
        public const Single PAPER_HEI_ENVC5 = 22.9f * 567;                  //'封筒 C5、162 x 229 mm
        public const Single PAPER_HEI_ENVC3 = 45.8f * 567;                  //'封筒 C3、324 x 458 mm
        public const Single PAPER_HEI_ENVC4 = 32.4f * 567;                  //'封筒 C4、229 x 324 mm
        public const Single PAPER_HEI_ENVC6 = 16.2f * 567;                  //'封筒 C6、114 x 162 mm
        public const Single PAPER_HEI_ENVC65 = 22.9f * 567;                 //'封筒 C65、114 x 229 mm
        public const Single PAPER_HEI_ENVB4 = 35.3f * 567;                  //'封筒 B4、250 x 353 mm
        public const Single PAPER_HEI_ENVB5 = 25 * 567;                     //'封筒 B5、176 x 250 mm
        public const Single PAPER_HEI_ENVB6 = 12.5f * 567;                  //'封筒 B6、176 x 125 mm
        public const Single PAPER_HEI_ENVITALY = 23 * 567;                  //'封筒、110 x 230 mm
        public const Single PAPER_HEI_ENVMONARCH = 7.5f * 1440;             //'封筒 Monarch、3 7/8 x 7 1/2 インチ
        public const Single PAPER_HEI_ENVPERSONAL = 6.5f * 1440;            //'封筒、3 5/8 x 6 1/2 インチ
        public const Single PAPER_HEI_FANFOLDUS = 11 * 1440;                //'US スタンダード ファンフォールド、14 7/8 x 11 インチ
        public const Single PAPER_HEI_FANFOLDSTDGERMAN = 12 * 1440;         //'ドイツ スタンダード ファンフォールド、8 1/2 x 12 インチ
        public const Single PAPER_HEI_FANFOLDLGLGERMAN = 13 * 1440;         //'ドイツ リーガル ファンフォールド、8 1/2 x 13 インチ
        public const Single PAPER_HEI_USER = 0;                             //'ユーザー定義のサイズ

        //'時間帯。
        public const string DISP_TIME_ZONE_UTC = "UTC";                     //As String           
        public const string DISP_TIME_ZONE_JST = "JST";                     //As String
        //(del)     Public DISP_TIME_ZONE(TIME_ZONE_COUNT - 1) As String
        public List<String> DISP_TIME_ZONE;                                 //As String 終了OPAデータ詳細情報



        //'書式。
        public const string ACCOUNT_FORMAT_MIN = "00";                      //As String '座標。分。
        public const string ACCOUNT_FORMAT_SEC  = "00";                     //As String '座標、秒。
        public const string ACCOUNT_FORMAT_DISPERSION = "0.00000E+000";     //As String'分散･共分散。

        //(del)     #endif
        //<<<<<<<<<-----------23/12/22 K.setoguchi@NV
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
