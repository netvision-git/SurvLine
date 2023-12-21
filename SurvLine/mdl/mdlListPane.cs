using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.Runtime.ConstrainedExecution;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Diagnostics;
using System.Collections;

//namespace SurvLine.mdl
namespace SurvLine
{

    public class MdlListPane
    {



        //'*******************************************************************************
        //'リストペインネイティブコード

        //リスト種別。
        public enum LIST_NUM_PANE
        {
            LIST_NUM_OBSPNT = 0,    //'観測点。
            LIST_NUM_VECTOR,        //'ベクトル。
            LIST_NUM_COUNT,         //'リスト番号数。
        }

        //'リストオブジェクト種別インデックス。
        public enum tagLIST_OBJ_TYPE_INDEX
        {
            LIST_OBJ_TYPE_INDEX_OBSERVATIONPOINT = 0,   //'観測点。
            LIST_OBJ_TYPE_INDEX_BASELINEVECTOR,         //'基線ベクトル。
            LIST_OBJ_TYPE_INDEX_COUNT,                  //'リスト種別数。
        }

        //'リスト名称。
        public const string LIST_NAM_OBSPNT = "観測点";
        public const string LIST_NAM_VECTOR = "ﾍﾞｸﾄﾙ";

        //'観測点カラム番号。
        public enum COL_NUM_OBSPNT
        {
            COL_NUM_OBSPNT_WORK = 0,        //'作業用。
            COL_NUM_OBSPNT_ENABLE,          //'有効/無効。
            COL_NUM_OBSPNT_TYPE,            //'観測点種類。
            COL_NUM_OBSPNT_MODE,            //'モード。
            COL_NUM_OBSPNT_SESSION,         //'セッション名。
            COL_NUM_OBSPNT_NUMBER,          //'観測点番号。
            COL_NUM_OBSPNT_NAME,            //'観測点名称。
            COL_NUM_OBSPNT_STRTIME,         //'観測開始日時。
            COL_NUM_OBSPNT_ENDTIME,         //'観測終了日時。
            COL_NUM_OBSPNT_RECTYPE,         //'受信機名称。
            COL_NUM_OBSPNT_RECNUMBER,       //'受信機シリアル。
            COL_NUM_OBSPNT_ANTTYPE,         //'アンテナ種別。
            COL_NUM_OBSPNT_ANTNUMBER,       //'アンテナシリアル。
            COL_NUM_OBSPNT_ANTMEASUREMENT,  //'アンテナ測位方法。
            COL_NUM_OBSPNT_ANTHEIGHT,       //'アンテナ高。
            COL_NUM_OBSPNT_ANTMOUNT,        //'アンテナ底面高。
            COL_NUM_OBSPNT_ANTCENTER,       //'アンテナ位相中心高。
            COL_NUM_OBSPNT_INTERVAL,        //'取得間隔。
            COL_NUM_OBSPNT_ELEVATIONMASK,   //'仰角マスク。
            COL_NUM_OBSPNT_SATELLITE,       //'最少衛星数。
            //'    COL_NUM_OBSPNT_COORDLAT '座標、緯度。
            //'    COL_NUM_OBSPNT_COORDLON '座標、経度。
            //'    COL_NUM_OBSPNT_COORDHEIGHT '座標、楕円体高。
            //'    COL_NUM_OBSPNT_COORDALT '座標、標高。
            //'    COL_NUM_OBSPNT_COORDX '座標、Ｘ。
            //'    COL_NUM_OBSPNT_COORDY '座標、Ｙ。
            //'    COL_NUM_OBSPNT_COORDZ '座標、Ｚ。
            COL_NUM_OBSPNT_COUNT,           //'番号数。
        }

        //'ベクトルカラム番号｡
        public enum COL_NUM_VECTOR
        {
            COL_NUM_VECTOR_WORK = 0,        //'作業用。
            COL_NUM_VECTOR_ENABLE,          //'有効/無効。
            COL_NUM_VECTOR_ADOPT,           //'採用/点検。
            COL_NUM_VECTOR_SESSION,         //'セッション名。
            COL_NUM_VECTOR_STRNUMBER,       //'始点観測点番号(名称)。
            COL_NUM_VECTOR_ENDNUMBER,       //'終点観測点番号(名称)。
            COL_NUM_VECTOR_ANALYSIS,        //'解の種類。
            COL_NUM_VECTOR_BIAS,            //'バイアス決定比。
            //'COL_NUM_VECTOR_AMBPERCENTAGE,  //'FIX率。
            COL_NUM_VECTOR_REJECTRATIO,     //'棄却率。
            COL_NUM_VECTOR_FREQUENCY,       //'使用周波数。
            COL_NUM_VECTOR_LC,              //'解析周波数。
            COL_NUM_VECTOR_STRTIME,         //'観測開始日時。
            COL_NUM_VECTOR_ENDTIME,         //'観測終了日時。
            COL_NUM_VECTOR_ANALYSISSTRTIME, //'解析開始日時。
            COL_NUM_VECTOR_ANALYSISENDTIME, //'解析終了日時。
            //'COL_NUM_VECTOR_SOLVEMODE,      //'基線解析モード。
            COL_NUM_VECTOR_DX,              //'ＤＸ。
            COL_NUM_VECTOR_DY,              //'ＤＹ。
            COL_NUM_VECTOR_DZ,              //'ＤＺ。
            COL_NUM_VECTOR_XX,              //'分散Ｘ。
            COL_NUM_VECTOR_YY,              //'分散Ｙ。
            COL_NUM_VECTOR_ZZ,              //'分散Ｚ。
            COL_NUM_VECTOR_XY,              //'共分散ＸＹ。
            COL_NUM_VECTOR_XZ,              //'共分散ＸＺ。
            COL_NUM_VECTOR_YZ,              //'共分散ＹＺ。
            //'COL_NUM_VECTOR_RMS,            //'RMS値。
            COL_NUM_VECTOR_CORRECTX,        //'偏心補正後ＤＸ。
            COL_NUM_VECTOR_CORRECTY,        //'偏心補正後ＤＹ。
            COL_NUM_VECTOR_CORRECTZ,        //'偏心補正後ＤＺ。
            COL_NUM_VECTOR_COUNT,           //'番号数。
        }

        //'カラム数。
        public const long LIST_COL_COUNT_OBSPNT = (long)COL_NUM_OBSPNT.COL_NUM_OBSPNT_COUNT;
        public const long LIST_COL_COUNT_VECTOR = (long)COL_NUM_VECTOR.COL_NUM_VECTOR_COUNT;

        public const string COL_NAM_OBSPNT_WORK = "";                   //'観測点カラム名称、作業用。
        public const string COL_NAM_OBSPNT_ENABLE = "有効";             //'観測点カラム名称、有効/無効。
        public const string COL_NAM_OBSPNT_TYPE = "種類";               //'観測点カラム名称、観測点種類。
        public const string COL_NAM_OBSPNT_MODE = "ﾓｰﾄﾞ";               //'観測点カラム名称、観測点モード。
        public const string COL_NAM_OBSPNT_SESSION = "ｾｯｼｮﾝ名";         //'観測点カラム名称、セッション名。
        public const string COL_NAM_OBSPNT_NUMBER = "観測点No";         //'観測点カラム名称、観測点番号。
        public const string COL_NAM_OBSPNT_NAME = "観測点名称";         //'観測点カラム名称、観測点名称。
        public const string COL_NAM_OBSPNT_STRTIME = "観測開始日時(JST)";       //'観測点カラム名称、観測開始日時。
        public const string COL_NAM_OBSPNT_ENDTIME = "観測終了日時(JST)";       //'観測点カラム名称、観測終了日時。
        public const string COL_NAM_OBSPNT_RECTYPE = "受信機名称";       //'観測点カラム名称、受信機名称。
        public const string COL_NAM_OBSPNT_RECNUMBER = "受信機ｼﾘｱﾙ";     //'観測点カラム名称、受信機シリアル。
        public const string COL_NAM_OBSPNT_ANTTYPE = "ｱﾝﾃﾅ名称";         //'観測点カラム名称、アンテナ種別。
        public const string COL_NAM_OBSPNT_ANTNUMBER = "ｱﾝﾃﾅｼﾘｱﾙ";       //'観測点カラム名称、アンテナシリアル。
        public const string COL_NAM_OBSPNT_ANTMEASUREMENT = "ｱﾝﾃﾅ測位方法";     //'観測点カラム名称、アンテナ測位方法。
        public const string COL_NAM_OBSPNT_ANTHEIGHT = "ｱﾝﾃﾅ高(m)";      //'観測点カラム名称、アンテナ高。
        public const string COL_NAM_OBSPNT_ANTMOUNT = "ｱﾝﾃﾅ底面高(m)";   //'観測点カラム名称、アンテナ底面高。
        public const string COL_NAM_OBSPNT_ANTCENTER = "位相中心高(m)";  //'観測点カラム名称、アンテナ位相中心高。2006/10/12 NGS Yamada
        //'public const string COL_NAM_OBSPNT_ANTCENTER = "ｱﾝﾃﾅ位相中心高(m)";  //'観測点カラム名称、アンテナ位相中心高。
        public const string COL_NAM_OBSPNT_INTERVAL = "取得間隔(秒)";    //'観測点カラム名称、取得間隔。
        public const string COL_NAM_OBSPNT_ELEVATIONMASK = "仰角ﾏｽｸ(度)";//'観測点カラム名称、仰角マスク。
        public const string COL_NAM_OBSPNT_SATELLITE = "最少衛星数";     //'観測点カラム名称、最少衛星数。
        //'public const string COL_NAM_OBSPNT_COORDLAT = "緯度" '観測点カラム名称、座標、緯度。
        //'public const string COL_NAM_OBSPNT_COORDLON = "経度" '観測点カラム名称、座標、経度。
        //'public const string COL_NAM_OBSPNT_COORDHEIGHT = "楕円体高(m)" '観測点カラム名称、座標、楕円体高。
        //'public const string COL_NAM_OBSPNT_COORDALT = "標高(m)" '観測点カラム名称、座標、標高。
        //'public const string COL_NAM_OBSPNT_COORDX = "地心直交X(m)" '観測点カラム名称、座標、Ｘ。
        //'public const string COL_NAM_OBSPNT_COORDY = "地心直交Y(m)" '観測点カラム名称、座標、Ｙ。
        //'public const string COL_NAM_OBSPNT_COORDZ = "地心直交Z(m)" '観測点カラム名称、座標、Ｚ。

        public const string COL_NAM_VECTOR_WORK = "";                   //'ベクトルカラム名称、作業用。
        public const string COL_NAM_VECTOR_ENABLE = "有効";             //'ベクトルカラム名称、有効/無効。
        public const string COL_NAM_VECTOR_ADOPT = "ﾓｰﾄﾞ";              //'ベクトルカラム名称、採用/点検。
        public const string COL_NAM_VECTOR_SESSION = "ｾｯｼｮﾝ名";         //'ベクトルカラム名称、セッション名。
        public const string COL_NAM_VECTOR_STRNUMBER = "始点No";        //'ベクトルカラム名称、始点観測点番号。 文字数を削減 2006/10/12 NGS Yamada
        public const string COL_NAM_VECTOR_ENDNUMBER = "終点No";        //'ベクトルカラム名称、終点観測点番号。
        //'public const string COL_NAM_VECTOR_STRNUMBER = "始点観測点No" 'ベクトルカラム名称、始点観測点番号。
        //'public const string COL_NAM_VECTOR_ENDNUMBER = "終点観測点No" 'ベクトルカラム名称、終点観測点番号。
        public const string COL_NAM_VECTOR_STRTIME = "観測開始日時(JST)";//'ベクトルカラム名称、観測開始日時。
        public const string COL_NAM_VECTOR_ENDTIME = "観測終了日時(JST)";//'ベクトルカラム名称、観測終了日時。
        public const string COL_NAM_VECTOR_ANALYSIS = "解の種類";        //'ベクトルカラム名称、解の種類。
        public const string COL_NAM_VECTOR_FREQUENCY = "使用周波数";     //'ベクトルカラム名称、使用周波数。
        public const string COL_NAM_VECTOR_LC = "解析周波数";            //'ベクトルカラム名称、解析周波数。
        public const string COL_NAM_VECTOR_SOLVEMODE = "基線解析ﾓｰﾄﾞ";   //'ベクトルカラム名称、基線解析モード。
        public const string COL_NAM_VECTOR_ANALYSISSTRTIME = "解析開始日時(JST)";     //'ベクトルカラム名称、解析開始日時。
        public const string COL_NAM_VECTOR_ANALYSISENDTIME = "解析終了日時(JST)";     //'ベクトルカラム名称、解析終了日時。
        public const string COL_NAM_VECTOR_DX = "DX(m)";                //'ベクトルカラム名称、ＤＸ。
        public const string COL_NAM_VECTOR_DY = "DY(m)";                //'ベクトルカラム名称、ＤＹ。
        public const string COL_NAM_VECTOR_DZ = "DZ(m)";                //'ベクトルカラム名称、ＤＺ。
        public const string COL_NAM_VECTOR_BIAS = "ﾊﾞｲｱｽ決定比";        //'ベクトルカラム名称、バイアス決定比。
        public const string COL_NAM_VECTOR_AMBPERCENTAGE = "FIX率(%)";  //'ベクトルカラム名称、FIX率。
        public const string COL_NAM_VECTOR_REJECTRATIO = "棄却率(%)";   //'ベクトルカラム名称、棄却率。
        public const string COL_NAM_VECTOR_RMS = "RMS(m)";              //'ベクトルカラム名称、RMS値。
        public const string COL_NAM_VECTOR_XX = "分散X";                //'ベクトルカラム名称、分散Ｘ。
        public const string COL_NAM_VECTOR_YY = "分散Y";                //'ベクトルカラム名称、分散Ｙ。
        public const string COL_NAM_VECTOR_ZZ = "分散Z";                //'ベクトルカラム名称、分散Ｚ。
        public const string COL_NAM_VECTOR_XY = "共分散XY";             //'ベクトルカラム名称、共分散ＸＹ。
        public const string COL_NAM_VECTOR_XZ = "共分散XZ";             //'ベクトルカラム名称、共分散ＸＺ。
        public const string COL_NAM_VECTOR_YZ = "共分散YZ";             //'ベクトルカラム名称、共分散ＹＺ。
        public const string COL_NAM_VECTOR_CORRECTX = "偏心補正DX(m)";  //'ベクトルカラム名称、偏心補正後ＤＸ。
        public const string COL_NAM_VECTOR_CORRECTY = "偏心補正DY(m)";  //'ベクトルカラム名称、偏心補正後ＤＹ。
        public const string COL_NAM_VECTOR_CORRECTZ = "偏心補正DZ(m)";  //'ベクトルカラム名称、偏心補正後ＤＺ。

        public const long COL_WID_OBSPNT_WORK = 0;          //'観測点カラム幅(Twips)、作業用。
        public const long COL_WID_OBSPNT_ENABLE = 744;      //'観測点カラム幅(Twips)、有効/無効。
        public const long COL_WID_OBSPNT_TYPE = 744;        //'観測点カラム幅(Twips)、観測点種類。
        public const long COL_WID_OBSPNT_MODE = 744;        //'観測点カラム幅(Twips)、観測点モード。
        public const long COL_WID_OBSPNT_SESSION = 900;     //'観測点カラム幅(Twips)、セッション名。
        public const long COL_WID_OBSPNT_NUMBER = 1000;     //'観測点カラム幅(Twips)、観測点番号。
        public const long COL_WID_OBSPNT_NAME = 1400;       //'観測点カラム幅(Twips)、観測点名称。
        public const long COL_WID_OBSPNT_STRTIME = 2000;    //'観測点カラム幅(Twips)、観測開始日時。
        public const long COL_WID_OBSPNT_ENDTIME = 2000;    //'観測点カラム幅(Twips)、観測終了日時。
        public const long COL_WID_OBSPNT_RECTYPE = 3600;    //'観測点カラム幅(Twips)、受信機名称。
        public const long COL_WID_OBSPNT_RECNUMBER = 1200;  //'観測点カラム幅(Twips)、受信機シリアル。
        public const long COL_WID_OBSPNT_ANTTYPE = 3500;    //'観測点カラム幅(Twips)、アンテナ種別。
        public const long COL_WID_OBSPNT_ANTNUMBER = 1200;     //'観測点カラム幅(Twips)、アンテナシリアル。
        public const long COL_WID_OBSPNT_ANTMEASUREMENT = 2000;//'観測点カラム幅(Twips)、アンテナ測位方法。
        public const long COL_WID_OBSPNT_ANTHEIGHT = 1308;     //'観測点カラム幅(Twips)、アンテナ高。
        public const long COL_WID_OBSPNT_ANTMOUNT = 1600;      //'観測点カラム幅(Twips)、アンテナ底面高。
        public const long COL_WID_OBSPNT_ANTCENTER = 1600;     //'観測点カラム幅(Twips)、アンテナ位相中心高。
        public const long COL_WID_OBSPNT_INTERVAL = 1308;      //'観測点カラム幅(Twips)、取得間隔。
        public const long COL_WID_OBSPNT_ELEVATIONMASK = 1308; //'観測点カラム幅(Twips)、仰角マスク。
        public const long COL_WID_OBSPNT_SATELLITE = 1308;     //'観測点カラム幅(Twips)、最少衛星数。
        //'public const long COL_WID_OBSPNT_LAT = 1800 '観測点カラム幅(Twips)、緯度。
        //'public const long COL_WID_OBSPNT_LON = 1800 '観測点カラム幅(Twips)、経度。
        //'public const long COL_WID_OBSPNT_HEIGHT = 1300 '観測点カラム幅(Twips)、楕円体高。
        //'public const long COL_WID_OBSPNT_ALT = 1300 '観測点カラム幅(Twips)、標高。
        //'public const long COL_WID_OBSPNT_X = 1728 '観測点カラム幅(Twips)、Ｘ。
        //'public const long COL_WID_OBSPNT_Y = 1728 '観測点カラム幅(Twips)、Ｙ。
        //'public const long COL_WID_OBSPNT_Z = 1728 '観測点カラム幅(Twips)、Ｚ。

        public const long COL_WID_VECTOR_WORK = 0;            //'ベクトルカラム幅(Twips)、作業用。
        public const long COL_WID_VECTOR_ENABLE = 744;        //'ベクトルカラム幅(Twips)、有効/無効。
        public const long COL_WID_VECTOR_ADOPT = 744;         //'ベクトルカラム幅(Twips)、採用/点検。
        public const long COL_WID_VECTOR_SESSION = 900;       //'ベクトルカラム幅(Twips)、セッション名。
        public const long COL_WID_VECTOR_STRNUMBER = 800;     //'ベクトルカラム幅(Twips)、始点観測点番号。 文字数を削減 2006/10/12 NGS Yamada
        public const long COL_WID_VECTOR_ENDNUMBER = 800;     //'ベクトルカラム幅(Twips)、終点観測点番号。
        //'public const long COL_WID_VECTOR_STRNUMBER = 1400 'ベクトルカラム幅(Twips)、始点観測点番号。
        //'public const long COL_WID_VECTOR_ENDNUMBER = 1400 'ベクトルカラム幅(Twips)、終点観測点番号。
        public const long COL_WID_VECTOR_STRTIME = 2000;     //'ベクトルカラム幅(Twips)、観測開始日時。
        public const long COL_WID_VECTOR_ENDTIME = 2000;     //'ベクトルカラム幅(Twips)、観測終了日時。
        public const long COL_WID_VECTOR_ANALYSIS = 1000;    //'ベクトルカラム幅(Twips)、解の種類。
        public const long COL_WID_VECTOR_FREQUENCY = 1400;   //'ベクトルカラム幅(Twips)、使用周波数。
        public const long COL_WID_VECTOR_LC = 1400;          //'ベクトルカラム幅(Twips)、解析周波数。
        public const long COL_WID_VECTOR_SOLVEMODE = 1500;   //'ベクトルカラム幅(Twips)、基線解析モード。
        public const long COL_WID_VECTOR_ANALYSISSTRTIME = 2000;     //'ベクトルカラム幅(Twips)、解析開始日時。
        public const long COL_WID_VECTOR_ANALYSISENDTIME = 2000;     //'ベクトルカラム幅(Twips)、解析終了日時。
        public const long COL_WID_VECTOR_DX = 1188;         //'ベクトルカラム幅(Twips)、ＤＸ。
        public const long COL_WID_VECTOR_DY = 1188;         //'ベクトルカラム幅(Twips)、ＤＹ。
        public const long COL_WID_VECTOR_DZ = 1188;         //'ベクトルカラム幅(Twips)、ＤＺ。
        public const long COL_WID_VECTOR_BIAS = 1308;       //'ベクトルカラム幅(Twips)、バイアス決定比。
        public const long COL_WID_VECTOR_AMBPERCENTAGE = 1000;   //'ベクトルカラム幅(Twips)、FIX率。
        public const long COL_WID_VECTOR_REJECTRATIO = 1000;     //'ベクトルカラム幅(Twips)、棄却率。
        public const long COL_WID_VECTOR_RMS = 1000;        //'ベクトルカラム幅(Twips)、RMS値。
        public const long COL_WID_VECTOR_XX = 1284;         //'ベクトルカラム幅(Twips)、分散Ｘ。
        public const long COL_WID_VECTOR_YY = 1284;         //'ベクトルカラム幅(Twips)、分散Ｙ。
        public const long COL_WID_VECTOR_ZZ = 1284;         //'ベクトルカラム幅(Twips)、分散Ｚ。
        public const long COL_WID_VECTOR_XY = 1284;         //'ベクトルカラム幅(Twips)、共分散ＸＹ。
        public const long COL_WID_VECTOR_XZ = 1284;         //'ベクトルカラム幅(Twips)、共分散ＸＺ。
        public const long COL_WID_VECTOR_YZ = 1284;         //'ベクトルカラム幅(Twips)、共分散ＹＺ。
        public const long COL_WID_VECTOR_CORRECTX = 1284;   //'ベクトルカラム幅(Twips)、偏心補正後ＤＸ。
        public const long COL_WID_VECTOR_CORRECTY = 1284;   //'ベクトルカラム幅(Twips)、偏心補正後ＤＹ。
        public const long COL_WID_VECTOR_CORRECTZ = 1284;   //'ベクトルカラム幅(Twips)、偏心補正後ＤＺ。

        public const long GUI_PERCENTAGE_DECIMAL = 1;       //'率。


        //**************************************************************************************
        //**************************************************************************************
        /// <summary>
        /// 'リストを作成する。 ListPane
        /// 引き数：
        //  ListPane：観測点＆ベクトル表示
        /// </summary>
        /// <param name="ListPane"></param>
        /// <returns>
        /// </returns>
        //**************************************************************************************
        public void MakeList(long nList, ListPane listPane)
        {


            //----------------------------------------------------------
            //[VB]      Select Case nList
            //[VB]      Case LIST_NUM_OBSPNT
            //[VB]          Call MakeListObsPnt(grdFlexGrid)
            //[VB]      Case LIST_NUM_VECTOR
            //[VB]          Call MakeListVector(grdFlexGrid)
            //[VB]      End Select
            switch (nList)
            {
                case (int)LIST_NUM_PANE.LIST_NUM_OBSPNT:
                    MakeListObsPnt(listPane);
                    break;
                case (int)LIST_NUM_PANE.LIST_NUM_VECTOR:
                    MakeListVector(listPane);
                    break;
                default:
                    break;
            }
            //----------------------------------------------------------
        }
        //--------------------------------------------------------------------------------
        //[VB]  'リストを作成する。
        //[VB]  '
        //[VB]  'nList で指定されるリストを再作成する。
        //[VB]  'カラムを初期化する。行は空になる。
        //[VB]  '行の内容を設定するのは RemakeList 関係のメソッド。
        //[VB]  '
        //[VB]  '引き数：
        //[VB]  'nList リスト種別。
        //[VB]  'grdFlexGrid リストコントロール。
        //[VB]  Public Sub MakeList(ByVal nList As Long, ByVal grdFlexGrid As NSFlexGrid)
        //[VB]      Select Case nList
        //[VB]      Case LIST_NUM_OBSPNT
        //[VB]          Call MakeListObsPnt(grdFlexGrid)
        //[VB]      Case LIST_NUM_VECTOR
        //[VB]          Call MakeListVector(grdFlexGrid)
        //[VB]      End Select
        //[VB]  End Sub
        //'*******************************************************************************
        //'*******************************************************************************


        //**************************************************************************************
        //**************************************************************************************
        /// <summary>
        /// '観測点リストを作成する。 ListPane
        /// 引き数：
        //  ListPane：観測点＆ベクトル表示
        /// </summary>
        /// <param name="ListPane"></param>
        /// <returns>
        /// </returns>
        //**************************************************************************************
        private void MakeListObsPnt(ListPane listPane)
        {
            int nWidth;             //    Dim nWidth As Long
            long nTotalWidth;       // Dim nTotalWidth As Long



            listPane.grdFlexGrid1.View = View.Details;


            //--------------------------------------------------------------------------------------
            //[VB]      grdFlexGrid.TextMatrix(0, COL_NUM_OBSPNT_WORK) = COL_NAM_OBSPNT_WORK
            nWidth = 40;
            nTotalWidth = nWidth;
            listPane.grdFlexGrid1.Columns.Add(COL_NAM_OBSPNT_WORK, nWidth);
            //--------------------------------------------------------------------------------------
            //[VB]      grdFlexGrid.TextMatrix(0, COL_NUM_OBSPNT_ENABLE) = COL_NAM_OBSPNT_ENABLE
            nWidth = 50;
            nTotalWidth += nWidth;
            listPane.grdFlexGrid1.Columns.Add(COL_NAM_OBSPNT_ENABLE, nWidth);
            //--------------------------------------------------------------------------------------
            //[VB]      grdFlexGrid.TextMatrix(0, COL_NUM_OBSPNT_TYPE) = COL_NAM_OBSPNT_TYPE
            nWidth = 100;
            nTotalWidth += nWidth;
            listPane.grdFlexGrid1.Columns.Add(COL_NAM_OBSPNT_TYPE, nWidth);
            //--------------------------------------------------------------------------------------
            //[VB]      grdFlexGrid.TextMatrix(0, COL_NUM_OBSPNT_MODE) = COL_NAM_OBSPNT_MODE
            nWidth = 100;
            nTotalWidth += nWidth;
            listPane.grdFlexGrid1.Columns.Add(COL_NAM_OBSPNT_MODE, nWidth);
            //--------------------------------------------------------------------------------------
            //[VB]      grdFlexGrid.TextMatrix(0, COL_NUM_OBSPNT_SESSION) = COL_NAM_OBSPNT_SESSION
            nWidth = 100;
            nTotalWidth += nWidth;
            listPane.grdFlexGrid1.Columns.Add(COL_NAM_OBSPNT_SESSION, nWidth);
            //--------------------------------------------------------------------------------------
            //[VB]      grdFlexGrid.TextMatrix(0, COL_NUM_OBSPNT_NUMBER) = COL_NAM_OBSPNT_NUMBER
            nWidth = 100;
            nTotalWidth += nWidth;
            listPane.grdFlexGrid1.Columns.Add(COL_NAM_OBSPNT_NUMBER, nWidth);
            //--------------------------------------------------------------------------------------
            //[VB]      grdFlexGrid.TextMatrix(0, COL_NUM_OBSPNT_NAME) = COL_NAM_OBSPNT_NAME
            nWidth = 100;
            nTotalWidth += nWidth;
            listPane.grdFlexGrid1.Columns.Add(COL_NAM_OBSPNT_NAME, nWidth);
            //--------------------------------------------------------------------------------------
            //[VB]      grdFlexGrid.TextMatrix(0, COL_NUM_OBSPNT_STRTIME) = COL_NAM_OBSPNT_STRTIME
            nWidth = 150;
            nTotalWidth += nWidth;
            listPane.grdFlexGrid1.Columns.Add(COL_NAM_OBSPNT_STRTIME, nWidth);
            //--------------------------------------------------------------------------------------
            //[VB]      grdFlexGrid.TextMatrix(0, COL_NUM_OBSPNT_ENDTIME) = COL_NAM_OBSPNT_ENDTIME
            nWidth = 150;
            nTotalWidth += nWidth;
            listPane.grdFlexGrid1.Columns.Add(COL_NAM_OBSPNT_ENDTIME, nWidth);
            //--------------------------------------------------------------------------------------
            //[VB]      grdFlexGrid.TextMatrix(0, COL_NUM_OBSPNT_RECTYPE) = COL_NAM_OBSPNT_RECTYPE
            nWidth = 100;
            nTotalWidth += nWidth;
            listPane.grdFlexGrid1.Columns.Add(COL_NAM_OBSPNT_RECTYPE, nWidth);
            //--------------------------------------------------------------------------------------
            //[VB]      grdFlexGrid.TextMatrix(0, COL_NUM_OBSPNT_RECNUMBER) = COL_NAM_OBSPNT_RECNUMBER
            nWidth = 100;
            nTotalWidth += nWidth;
            listPane.grdFlexGrid1.Columns.Add(COL_NAM_OBSPNT_RECNUMBER, nWidth);
            //--------------------------------------------------------------------------------------
            //[VB]      grdFlexGrid.TextMatrix(0, COL_NUM_OBSPNT_ANTTYPE) = COL_NAM_OBSPNT_ANTTYPE
            nWidth = 100;
            nTotalWidth += nWidth;
            listPane.grdFlexGrid1.Columns.Add(COL_NAM_OBSPNT_ANTTYPE, nWidth);
            //--------------------------------------------------------------------------------------
            //[VB]      grdFlexGrid.TextMatrix(0, COL_NUM_OBSPNT_ANTNUMBER) = COL_NAM_OBSPNT_ANTNUMBER
            nWidth = 100;
            nTotalWidth += nWidth;
            listPane.grdFlexGrid1.Columns.Add(COL_NAM_OBSPNT_ANTNUMBER, nWidth);
            //--------------------------------------------------------------------------------------
            //[VB]      grdFlexGrid.TextMatrix(0, COL_NUM_OBSPNT_ANTMEASUREMENT) = COL_NAM_OBSPNT_ANTMEASUREMENT
            nWidth = 100;
            nTotalWidth += nWidth;
            listPane.grdFlexGrid1.Columns.Add(COL_NAM_OBSPNT_ANTMEASUREMENT, nWidth);
            //--------------------------------------------------------------------------------------
            //[VB]      grdFlexGrid.TextMatrix(0, COL_NUM_OBSPNT_ANTHEIGHT) = COL_NAM_OBSPNT_ANTHEIGHT
            nWidth = 100;
            nTotalWidth += nWidth;
            listPane.grdFlexGrid1.Columns.Add(COL_NAM_OBSPNT_ANTHEIGHT, nWidth);
            //--------------------------------------------------------------------------------------
            //[VB]      grdFlexGrid.TextMatrix(0, COL_NUM_OBSPNT_ANTMOUNT) = COL_NAM_OBSPNT_ANTMOUNT
            nWidth = 100;
            nTotalWidth += nWidth;
            listPane.grdFlexGrid1.Columns.Add(COL_NAM_OBSPNT_ANTMOUNT, nWidth);
            //--------------------------------------------------------------------------------------
            //[VB]      grdFlexGrid.TextMatrix(0, COL_NUM_OBSPNT_ANTCENTER) = COL_NAM_OBSPNT_ANTCENTER
            nWidth = 100;
            nTotalWidth += nWidth;
            listPane.grdFlexGrid1.Columns.Add(COL_NAM_OBSPNT_ANTCENTER, nWidth);
            //--------------------------------------------------------------------------------------
            //[VB]      grdFlexGrid.TextMatrix(0, COL_NUM_OBSPNT_INTERVAL) = COL_NAM_OBSPNT_INTERVAL
            nWidth = 100;
            nTotalWidth += nWidth;
            listPane.grdFlexGrid1.Columns.Add(COL_NAM_OBSPNT_INTERVAL, nWidth);
            //--------------------------------------------------------------------------------------
            //[VB]      grdFlexGrid.TextMatrix(0, COL_NUM_OBSPNT_ELEVATIONMASK) = COL_NAM_OBSPNT_ELEVATIONMASK
            nWidth = 100;
            nTotalWidth += nWidth;
            listPane.grdFlexGrid1.Columns.Add(COL_NAM_OBSPNT_ELEVATIONMASK, nWidth);
            //--------------------------------------------------------------------------------------
            //[VB]      grdFlexGrid.TextMatrix(0, COL_NUM_OBSPNT_SATELLITE) = COL_NAM_OBSPNT_SATELLITE
            nWidth = 100;
            nTotalWidth += nWidth;
            listPane.grdFlexGrid1.Columns.Add(COL_NAM_OBSPNT_SATELLITE, nWidth);
            //[VB]      '    grdFlexGrid.TextMatrix(0, COL_NUM_OBSPNT_COORDLAT) = COL_NAM_OBSPNT_COORDLAT
            //[VB]      '    grdFlexGrid.TextMatrix(0, COL_NUM_OBSPNT_COORDLON) = COL_NAM_OBSPNT_COORDLON
            //[VB]      '    grdFlexGrid.TextMatrix(0, COL_NUM_OBSPNT_COORDHEIGHT) = COL_NAM_OBSPNT_COORDHEIGHT
            //[VB]      '    grdFlexGrid.TextMatrix(0, COL_NUM_OBSPNT_COORDALT) = COL_NAM_OBSPNT_COORDALT
            //[VB]      '    grdFlexGrid.TextMatrix(0, COL_NUM_OBSPNT_COORDX) = COL_NAM_OBSPNT_COORDX
            //[VB]      '    grdFlexGrid.TextMatrix(0, COL_NUM_OBSPNT_COORDY) = COL_NAM_OBSPNT_COORDY
            //[VB]      '    grdFlexGrid.TextMatrix(0, COL_NUM_OBSPNT_COORDZ) = COL_NAM_OBSPNT_COORDZ
            //--------------------------------------------------------------------------------------
            //grdFlexGrid1.Width = (int)nTotalWidth;
            listPane.grdFlexGrid1.Width = 700;
            listPane.grdFlexGrid1.Height = 400;


            //--------------------------------------------------------------------------------------
#if !DEBUG
          int datacount = 5;
            for (int i = 0; i < datacount; i++)
            {
                var item = new ListViewItem("〆");
                item.SubItems.Add("aaa2");
                item.SubItems.Add("aaa3");
                item.SubItems.Add("aaa4");
                item.SubItems.Add("aaa5");
                item.SubItems.Add("aaa6");
                item.SubItems.Add("aaa7");
                item.SubItems.Add("aaa8");
                item.SubItems.Add("aaa9");
                item.SubItems.Add("aaa10");
                item.SubItems.Add("aaa11");
                item.SubItems.Add("aaa12");

                listPane.grdFlexGrid1.Items.Add(item);
            }
#endif

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="listPane"></param>

        public void ListDataDisp(ListPane listPane)
        {


            listPane.m_list = 15;


            listPane.grdFlexGrid1.View = View.Details;
#if DEBUG
            int datacount = 5;
            for (int i = 0; i < datacount; i++)
            {
                var item = new ListViewItem("〆");
                item.SubItems.Add("aaa2");
                item.SubItems.Add("aaa3");
                item.SubItems.Add("aaa4");
                item.SubItems.Add("aaa5");
                item.SubItems.Add("aaa6");
                item.SubItems.Add("aaa7");
                item.SubItems.Add("aaa8");
                item.SubItems.Add("aaa9");
                item.SubItems.Add("aaa10");
                item.SubItems.Add("aaa11");
                item.SubItems.Add("aaa12");

                listPane.grdFlexGrid1.Items.Add(item);
            }
#endif



        }


        //---------------------------------------------------------9-----------------------
        //[VB]  '観測点リストを作成する。
        //[VB]  '
        //[VB]  '引き数：
        //[VB]  'grdFlexGrid リストコントロール。
        //[VB]  Public Sub MakeListObsPnt(ByVal grdFlexGrid As NSFlexGrid)
        //[VB]  
        //[VB]      'リストのクリア。
        //[VB]      Dim bRedraw As Boolean
        //[VB]      bRedraw = grdFlexGrid.Redraw
        //[VB]      grdFlexGrid.Redraw = False
        //[VB]      grdFlexGrid.Clear
        //[VB]  
        //[VB]      'カラム名称。
        //[VB]      grdFlexGrid.Cols = COL_NUM_OBSPNT_COUNT
        //[VB]      grdFlexGrid.FixedCols = 1
        //[VB]      grdFlexGrid.TextMatrix(0, COL_NUM_OBSPNT_WORK) = COL_NAM_OBSPNT_WORK
        //[VB]      grdFlexGrid.TextMatrix(0, COL_NUM_OBSPNT_ENABLE) = COL_NAM_OBSPNT_ENABLE
        //[VB]      grdFlexGrid.TextMatrix(0, COL_NUM_OBSPNT_TYPE) = COL_NAM_OBSPNT_TYPE
        //[VB]      grdFlexGrid.TextMatrix(0, COL_NUM_OBSPNT_MODE) = COL_NAM_OBSPNT_MODE
        //[VB]      grdFlexGrid.TextMatrix(0, COL_NUM_OBSPNT_SESSION) = COL_NAM_OBSPNT_SESSION
        //[VB]      grdFlexGrid.TextMatrix(0, COL_NUM_OBSPNT_NUMBER) = COL_NAM_OBSPNT_NUMBER
        //[VB]      grdFlexGrid.TextMatrix(0, COL_NUM_OBSPNT_NAME) = COL_NAM_OBSPNT_NAME
        //[VB]      grdFlexGrid.TextMatrix(0, COL_NUM_OBSPNT_STRTIME) = COL_NAM_OBSPNT_STRTIME
        //[VB]      grdFlexGrid.TextMatrix(0, COL_NUM_OBSPNT_ENDTIME) = COL_NAM_OBSPNT_ENDTIME
        //[VB]      grdFlexGrid.TextMatrix(0, COL_NUM_OBSPNT_RECTYPE) = COL_NAM_OBSPNT_RECTYPE
        //[VB]      grdFlexGrid.TextMatrix(0, COL_NUM_OBSPNT_RECNUMBER) = COL_NAM_OBSPNT_RECNUMBER
        //[VB]      grdFlexGrid.TextMatrix(0, COL_NUM_OBSPNT_ANTTYPE) = COL_NAM_OBSPNT_ANTTYPE

        //[VB]      grdFlexGrid.TextMatrix(0, COL_NUM_OBSPNT_ANTNUMBER) = COL_NAM_OBSPNT_ANTNUMBER
        //[VB]      grdFlexGrid.TextMatrix(0, COL_NUM_OBSPNT_ANTMEASUREMENT) = COL_NAM_OBSPNT_ANTMEASUREMENT
        //[VB]      grdFlexGrid.TextMatrix(0, COL_NUM_OBSPNT_ANTHEIGHT) = COL_NAM_OBSPNT_ANTHEIGHT
        //[VB]      grdFlexGrid.TextMatrix(0, COL_NUM_OBSPNT_ANTMOUNT) = COL_NAM_OBSPNT_ANTMOUNT
        //[VB]      grdFlexGrid.TextMatrix(0, COL_NUM_OBSPNT_ANTCENTER) = COL_NAM_OBSPNT_ANTCENTER
        //[VB]      grdFlexGrid.TextMatrix(0, COL_NUM_OBSPNT_INTERVAL) = COL_NAM_OBSPNT_INTERVAL
        //[VB]      grdFlexGrid.TextMatrix(0, COL_NUM_OBSPNT_ELEVATIONMASK) = COL_NAM_OBSPNT_ELEVATIONMASK
        //[VB]      grdFlexGrid.TextMatrix(0, COL_NUM_OBSPNT_SATELLITE) = COL_NAM_OBSPNT_SATELLITE
        //[VB]      '    grdFlexGrid.TextMatrix(0, COL_NUM_OBSPNT_COORDLAT) = COL_NAM_OBSPNT_COORDLAT
        //[VB]      '    grdFlexGrid.TextMatrix(0, COL_NUM_OBSPNT_COORDLON) = COL_NAM_OBSPNT_COORDLON
        //[VB]      '    grdFlexGrid.TextMatrix(0, COL_NUM_OBSPNT_COORDHEIGHT) = COL_NAM_OBSPNT_COORDHEIGHT
        //[VB]      '    grdFlexGrid.TextMatrix(0, COL_NUM_OBSPNT_COORDALT) = COL_NAM_OBSPNT_COORDALT
        //[VB]      '    grdFlexGrid.TextMatrix(0, COL_NUM_OBSPNT_COORDX) = COL_NAM_OBSPNT_COORDX
        //[VB]      '    grdFlexGrid.TextMatrix(0, COL_NUM_OBSPNT_COORDY) = COL_NAM_OBSPNT_COORDY
        //[VB]      '    grdFlexGrid.TextMatrix(0, COL_NUM_OBSPNT_COORDZ) = COL_NAM_OBSPNT_COORDZ
        //[VB]  
        //[VB]      '行数。
        //[VB]      grdFlexGrid.FixedRows = 1
        //[VB]  
        //[VB]      'カラムアライメント。
        //[VB]      grdFlexGrid.ColAlignment(COL_NUM_OBSPNT_WORK) = flexAlignCenterCenter
        //[VB]      grdFlexGrid.ColAlignment(COL_NUM_OBSPNT_ENABLE) = flexAlignCenterCenter
        //[VB]      grdFlexGrid.ColAlignment(COL_NUM_OBSPNT_TYPE) = flexAlignCenterCenter
        //[VB]      grdFlexGrid.ColAlignment(COL_NUM_OBSPNT_MODE) = flexAlignCenterCenter
        //[VB]      grdFlexGrid.ColAlignment(COL_NUM_OBSPNT_SESSION) = flexAlignLeftCenter
        //[VB]      grdFlexGrid.ColAlignment(COL_NUM_OBSPNT_NUMBER) = flexAlignLeftCenter
        //[VB]      grdFlexGrid.ColAlignment(COL_NUM_OBSPNT_NAME) = flexAlignLeftCenter
        //[VB]      grdFlexGrid.ColAlignment(COL_NUM_OBSPNT_STRTIME) = flexAlignLeftCenter
        //[VB]      grdFlexGrid.ColAlignment(COL_NUM_OBSPNT_ENDTIME) = flexAlignLeftCenter
        //[VB]      grdFlexGrid.ColAlignment(COL_NUM_OBSPNT_RECTYPE) = flexAlignLeftCenter
        //[VB]      grdFlexGrid.ColAlignment(COL_NUM_OBSPNT_RECNUMBER) = flexAlignLeftCenter
        //[VB]      grdFlexGrid.ColAlignment(COL_NUM_OBSPNT_ANTTYPE) = flexAlignLeftCenter
        //[VB]      grdFlexGrid.ColAlignment(COL_NUM_OBSPNT_ANTNUMBER) = flexAlignLeftCenter
        //[VB]      grdFlexGrid.ColAlignment(COL_NUM_OBSPNT_ANTMEASUREMENT) = flexAlignLeftCenter
        //[VB]      grdFlexGrid.ColAlignment(COL_NUM_OBSPNT_ANTHEIGHT) = flexAlignRightCenter
        //[VB]      grdFlexGrid.ColAlignment(COL_NUM_OBSPNT_ANTMOUNT) = flexAlignRightCenter
        //[VB]      grdFlexGrid.ColAlignment(COL_NUM_OBSPNT_ANTCENTER) = flexAlignRightCenter
        //[VB]      grdFlexGrid.ColAlignment(COL_NUM_OBSPNT_INTERVAL) = flexAlignRightCenter
        //[VB]      grdFlexGrid.ColAlignment(COL_NUM_OBSPNT_ELEVATIONMASK) = flexAlignRightCenter
        //[VB]      grdFlexGrid.ColAlignment(COL_NUM_OBSPNT_SATELLITE) = flexAlignRightCenter
        //[VB]      '    grdFlexGrid.ColAlignment(COL_NUM_OBSPNT_COORDLAT) = flexAlignRightCenter
        //[VB]      '    grdFlexGrid.ColAlignment(COL_NUM_OBSPNT_COORDLON) = flexAlignRightCenter
        //[VB]      '    grdFlexGrid.ColAlignment(COL_NUM_OBSPNT_COORDHEIGHT) = flexAlignRightCenter
        //[VB]      '    grdFlexGrid.ColAlignment(COL_NUM_OBSPNT_COORDALT) = flexAlignRightCenter
        //[VB]      '    grdFlexGrid.ColAlignment(COL_NUM_OBSPNT_COORDX) = flexAlignRightCenter
        //[VB]      '    grdFlexGrid.ColAlignment(COL_NUM_OBSPNT_COORDY) = flexAlignRightCenter
        //[VB]      '    grdFlexGrid.ColAlignment(COL_NUM_OBSPNT_COORDZ) = flexAlignRightCenter
        //[VB]      '固定行のアライメント。
        //[VB]      grdFlexGrid.Row = 0
        //[VB]      Dim i As Long
        //[VB]      For i = 0 To COL_NUM_OBSPNT_COUNT - 1
        //[VB]          grdFlexGrid.Col = i
        //[VB]          grdFlexGrid.CellAlignment = flexAlignCenterCenter
        //[VB]      Next
        //[VB]  
        //[VB]      'カラム幅。
        //[VB]  grdFlexGrid.ColWidth(COL_NUM_OBSPNT_WORK) = COL_WID_OBSPNT_WORK
        //[VB]  grdFlexGrid.ColWidth(COL_NUM_OBSPNT_ENABLE) = COL_WID_OBSPNT_ENABLE
        //[VB]  grdFlexGrid.ColWidth(COL_NUM_OBSPNT_TYPE) = COL_WID_OBSPNT_TYPE
        //[VB]  grdFlexGrid.ColWidth(COL_NUM_OBSPNT_MODE) = COL_WID_OBSPNT_MODE
        //[VB]  grdFlexGrid.ColWidth(COL_NUM_OBSPNT_SESSION) = COL_WID_OBSPNT_SESSION
        //[VB]  grdFlexGrid.ColWidth(COL_NUM_OBSPNT_NUMBER) = COL_WID_OBSPNT_NUMBER
        //[VB]  grdFlexGrid.ColWidth(COL_NUM_OBSPNT_NAME) = COL_WID_OBSPNT_NAME
        //[VB]  grdFlexGrid.ColWidth(COL_NUM_OBSPNT_STRTIME) = COL_WID_OBSPNT_STRTIME
        //[VB]  grdFlexGrid.ColWidth(COL_NUM_OBSPNT_ENDTIME) = COL_WID_OBSPNT_ENDTIME
        //[VB]  grdFlexGrid.ColWidth(COL_NUM_OBSPNT_RECTYPE) = COL_WID_OBSPNT_RECTYPE
        //[VB]  grdFlexGrid.ColWidth(COL_NUM_OBSPNT_RECNUMBER) = COL_WID_OBSPNT_RECNUMBER
        //[VB]  grdFlexGrid.ColWidth(COL_NUM_OBSPNT_ANTTYPE) = COL_WID_OBSPNT_ANTTYPE
        //[VB]  grdFlexGrid.ColWidth(COL_NUM_OBSPNT_ANTNUMBER) = COL_WID_OBSPNT_ANTNUMBER
        //[VB]  grdFlexGrid.ColWidth(COL_NUM_OBSPNT_ANTMEASUREMENT) = COL_WID_OBSPNT_ANTMEASUREMENT
        //[VB]  grdFlexGrid.ColWidth(COL_NUM_OBSPNT_ANTHEIGHT) = COL_WID_OBSPNT_ANTHEIGHT
        //[VB]  grdFlexGrid.ColWidth(COL_NUM_OBSPNT_ANTMOUNT) = COL_WID_OBSPNT_ANTMOUNT
        //[VB]  grdFlexGrid.ColWidth(COL_NUM_OBSPNT_ANTCENTER) = COL_WID_OBSPNT_ANTCENTER
        //[VB]  grdFlexGrid.ColWidth(COL_NUM_OBSPNT_INTERVAL) = COL_WID_OBSPNT_INTERVAL
        //[VB]  grdFlexGrid.ColWidth(COL_NUM_OBSPNT_ELEVATIONMASK) = COL_WID_OBSPNT_ELEVATIONMASK
        //[VB]  grdFlexGrid.ColWidth(COL_NUM_OBSPNT_SATELLITE) = COL_WID_OBSPNT_SATELLITE
        //[VB]  '    grdFlexGrid.ColWidth(COL_NUM_OBSPNT_COORDLAT) = COL_WID_OBSPNT_LAT
        //[VB]  '    grdFlexGrid.ColWidth(COL_NUM_OBSPNT_COORDLON) = COL_WID_OBSPNT_LON
        //[VB]  '    grdFlexGrid.ColWidth(COL_NUM_OBSPNT_COORDHEIGHT) = COL_WID_OBSPNT_HEIGHT
        //[VB]  '    grdFlexGrid.ColWidth(COL_NUM_OBSPNT_COORDALT) = COL_WID_OBSPNT_ALT
        //[VB]  '    grdFlexGrid.ColWidth(COL_NUM_OBSPNT_COORDX) = COL_WID_OBSPNT_X
        //[VB]  '    grdFlexGrid.ColWidth(COL_NUM_OBSPNT_COORDY) = COL_WID_OBSPNT_Y
        //[VB]  '    grdFlexGrid.ColWidth(COL_NUM_OBSPNT_COORDZ) = COL_WID_OBSPNT_Z
        //[VB]    
        //[VB]    '表示。
        //[VB]  grdFlexGrid.Redraw = bRedraw
        //[VB]
        //[VB]
        //[VB]  End Sub
        //'*******************************************************************************
        //'*******************************************************************************


        //**************************************************************************************
        //**************************************************************************************
        /// <summary>
        /// '基線ベクトルリストを作成する。 ListPane
        /// 引き数：
        //  ListPane：観測点＆ベクトル表示
        /// </summary>
        /// <param name="ListPane"></param>
        /// <returns>
        /// </returns>
        //**************************************************************************************
        private void MakeListVector(ListPane listPane)
        {
            //[VB]      'リストのクリア。
            //[VB]      Dim bRedraw As Boolean
            //[VB]      bRedraw = grdFlexGrid.Redraw
            //[VB]      grdFlexGrid.Redraw = False
            //[VB]      grdFlexGrid.Clear

            int nWidth;             //    Dim nWidth As Long
            long nTotalWidth;       // Dim nTotalWidth As Long

            //--------------------------------------------------------------------------------------
            //[VB]      'カラム名称。
            //[VB]      grdFlexGrid.Cols = COL_NUM_VECTOR_COUNT
            //[VB]      grdFlexGrid.FixedCols = 1

            listPane.grdFlexGrid2.View = View.Details;

            //--------------------------------------------------------------------------------------
            //[VB]      grdFlexGrid.TextMatrix(0, COL_NUM_VECTOR_WORK) = COL_NAM_VECTOR_WORK
            nWidth = 40;
            nTotalWidth = nWidth;
            listPane.grdFlexGrid2.Columns.Add(COL_NAM_VECTOR_WORK, nWidth);
            //--------------------------------------------------------------------------------------
            //[VB]      grdFlexGrid.TextMatrix(0, COL_NUM_VECTOR_ENABLE) = COL_NAM_VECTOR_ENABLE
            nWidth = 50;
            nTotalWidth += nWidth;
            listPane.grdFlexGrid2.Columns.Add(COL_NAM_VECTOR_ENABLE, nWidth);
            //--------------------------------------------------------------------------------------
            //[VB]      grdFlexGrid.TextMatrix(0, COL_NUM_VECTOR_ADOPT) = COL_NAM_VECTOR_ADOPT
            nWidth = 100;
            nTotalWidth += nWidth;
            listPane.grdFlexGrid2.Columns.Add(COL_NAM_VECTOR_ADOPT, nWidth);
            //--------------------------------------------------------------------------------------
            //[VB]      grdFlexGrid.TextMatrix(0, COL_NUM_VECTOR_SESSION) = COL_NAM_VECTOR_SESSION
            nWidth = 100;
            nTotalWidth += nWidth;
            listPane.grdFlexGrid2.Columns.Add(COL_NAM_VECTOR_SESSION, nWidth);
            //--------------------------------------------------------------------------------------
            //[VB]      grdFlexGrid.TextMatrix(0, COL_NUM_VECTOR_STRNUMBER) = COL_NAM_VECTOR_STRNUMBER
            nWidth = 100;
            nTotalWidth += nWidth;
            listPane.grdFlexGrid2.Columns.Add(COL_NAM_VECTOR_STRNUMBER, nWidth);
            //--------------------------------------------------------------------------------------
            //[VB]      grdFlexGrid.TextMatrix(0, COL_NUM_VECTOR_ENDNUMBER) = COL_NAM_VECTOR_ENDNUMBER
            nWidth = 100;
            nTotalWidth += nWidth;
            listPane.grdFlexGrid2.Columns.Add(COL_NAM_VECTOR_ENDNUMBER, nWidth);
            //--------------------------------------------------------------------------------------
            //[VB]      grdFlexGrid.TextMatrix(0, COL_NUM_VECTOR_STRTIME) = COL_NAM_VECTOR_STRTIME
            nWidth = 150;
            nTotalWidth += nWidth;
            listPane.grdFlexGrid2.Columns.Add(COL_NAM_VECTOR_STRTIME, nWidth);
            //--------------------------------------------------------------------------------------
            //[VB]      grdFlexGrid.TextMatrix(0, COL_NUM_VECTOR_ENDTIME) = COL_NAM_VECTOR_ENDTIME
            nWidth = 150;
            nTotalWidth += nWidth;
            listPane.grdFlexGrid2.Columns.Add(COL_NAM_VECTOR_ENDTIME, nWidth);
            //--------------------------------------------------------------------------------------
            //[VB]      grdFlexGrid.TextMatrix(0, COL_NUM_VECTOR_ANALYSIS) = COL_NAM_VECTOR_ANALYSIS
            nWidth = 100;
            nTotalWidth += nWidth;
            listPane.grdFlexGrid2.Columns.Add(COL_NAM_VECTOR_ANALYSIS, nWidth);
            //--------------------------------------------------------------------------------------
            //[VB]      grdFlexGrid.TextMatrix(0, COL_NUM_VECTOR_FREQUENCY) = COL_NAM_VECTOR_FREQUENCY
            nWidth = 100;
            nTotalWidth += nWidth;
            listPane.grdFlexGrid2.Columns.Add(COL_NAM_VECTOR_FREQUENCY, nWidth);
            //--------------------------------------------------------------------------------------
            //[VB]      grdFlexGrid.TextMatrix(0, COL_NUM_VECTOR_LC) = COL_NAM_VECTOR_LC
            nWidth = 100;
            nTotalWidth += nWidth;
            listPane.grdFlexGrid2.Columns.Add(COL_NAM_VECTOR_LC, nWidth);


            //--------------------------------------------------------------------------------------
            //[VB]      //'grdFlexGrid.TextMatrix(0, COL_NUM_VECTOR_SOLVEMODE) = COL_NAM_VECTOR_SOLVEMODE
            nWidth = 100;
            nTotalWidth += nWidth;
            listPane.grdFlexGrid2.Columns.Add(COL_NAM_VECTOR_SOLVEMODE, nWidth);
            //--------------------------------------------------------------------------------------
            //[VB]      grdFlexGrid.TextMatrix(0, COL_NUM_VECTOR_ANALYSISSTRTIME) = COL_NAM_VECTOR_ANALYSISSTRTIME
            nWidth = 150;
            nTotalWidth += nWidth;
            listPane.grdFlexGrid2.Columns.Add(COL_NAM_VECTOR_ANALYSISSTRTIME, nWidth);
            //--------------------------------------------------------------------------------------
            //[VB]      grdFlexGrid.TextMatrix(0, COL_NUM_VECTOR_ANALYSISENDTIME) = COL_NAM_VECTOR_ANALYSISENDTIME
            nWidth = 150;
            nTotalWidth += nWidth;
            listPane.grdFlexGrid2.Columns.Add(COL_NAM_VECTOR_ANALYSISENDTIME, nWidth);
            //--------------------------------------------------------------------------------------
            //[VB]      grdFlexGrid.TextMatrix(0, COL_NUM_VECTOR_DX) = COL_NAM_VECTOR_DX
            nWidth = 100;
            nTotalWidth += nWidth;
            listPane.grdFlexGrid2.Columns.Add(COL_NAM_VECTOR_DX, nWidth);
            //--------------------------------------------------------------------------------------
            //[VB]      grdFlexGrid.TextMatrix(0, COL_NUM_VECTOR_DY) = COL_NAM_VECTOR_DY
            nWidth = 100;
            nTotalWidth += nWidth;
            listPane.grdFlexGrid2.Columns.Add(COL_NAM_VECTOR_DY, nWidth);
            //--------------------------------------------------------------------------------------
            //[VB]      grdFlexGrid.TextMatrix(0, COL_NUM_VECTOR_DZ) = COL_NAM_VECTOR_DZ
            nWidth = 100;
            nTotalWidth += nWidth;
            listPane.grdFlexGrid2.Columns.Add(COL_NAM_VECTOR_DZ, nWidth);
            //--------------------------------------------------------------------------------------
            //[VB]      grdFlexGrid.TextMatrix(0, COL_NUM_VECTOR_BIAS) = COL_NAM_VECTOR_BIAS
            nWidth = 100;
            nTotalWidth += nWidth;
            listPane.grdFlexGrid2.Columns.Add(COL_NAM_VECTOR_BIAS, nWidth);
            //--------------------------------------------------------------------------------------
            //[VB]      //'grdFlexGrid.TextMatrix(0, COL_NUM_VECTOR_AMBPERCENTAGE) = COL_NAM_VECTOR_AMBPERCENTAGE
            //--------------------------------------------------------------------------------------
            //[VB]      grdFlexGrid.TextMatrix(0, COL_NUM_VECTOR_REJECTRATIO) = COL_NAM_VECTOR_REJECTRATIO
            nWidth = 100;
            nTotalWidth += nWidth;
            listPane.grdFlexGrid2.Columns.Add(COL_NAM_VECTOR_REJECTRATIO, nWidth);
            //--------------------------------------------------------------------------------------
            //[VB]      //'grdFlexGrid.TextMatrix(0, COL_NUM_VECTOR_RMS) = COL_NAM_VECTOR_RMS
            //--------------------------------------------------------------------------------------
            //[VB]      grdFlexGrid.TextMatrix(0, COL_NUM_VECTOR_XX) = COL_NAM_VECTOR_XX
            nWidth = 100;
            nTotalWidth += nWidth;
            listPane.grdFlexGrid2.Columns.Add(COL_NAM_VECTOR_XX, nWidth);
            //--------------------------------------------------------------------------------------
            //[VB]      grdFlexGrid.TextMatrix(0, COL_NUM_VECTOR_YY) = COL_NAM_VECTOR_YY
            nWidth = 100;
            nTotalWidth += nWidth;
            listPane.grdFlexGrid2.Columns.Add(COL_NAM_VECTOR_YY, nWidth);
            //--------------------------------------------------------------------------------------
            //[VB]      grdFlexGrid.TextMatrix(0, COL_NUM_VECTOR_ZZ) = COL_NAM_VECTOR_ZZ
            nWidth = 100;
            nTotalWidth += nWidth;
            listPane.grdFlexGrid2.Columns.Add(COL_NAM_VECTOR_ZZ, nWidth);
            //--------------------------------------------------------------------------------------
            //[VB]      grdFlexGrid.TextMatrix(0, COL_NUM_VECTOR_XY) = COL_NAM_VECTOR_XY
            nWidth = 100;
            nTotalWidth += nWidth;
            listPane.grdFlexGrid2.Columns.Add(COL_NAM_VECTOR_XY, nWidth);
            //--------------------------------------------------------------------------------------
            //[VB]      grdFlexGrid.TextMatrix(0, COL_NUM_VECTOR_XZ) = COL_NAM_VECTOR_XZ
            nWidth = 100;
            nTotalWidth += nWidth;
            listPane.grdFlexGrid2.Columns.Add(COL_NAM_VECTOR_XZ, nWidth);
            //--------------------------------------------------------------------------------------
            //[VB]      grdFlexGrid.TextMatrix(0, COL_NUM_VECTOR_YZ) = COL_NAM_VECTOR_YZ
            nWidth = 100;
            nTotalWidth += nWidth;
            listPane.grdFlexGrid2.Columns.Add(COL_NAM_VECTOR_YZ, nWidth);
            //--------------------------------------------------------------------------------------
            //[VB]      grdFlexGrid.TextMatrix(0, COL_NUM_VECTOR_CORRECTX) = COL_NAM_VECTOR_CORRECTX
            nWidth = 100;
            nTotalWidth += nWidth;
            listPane.grdFlexGrid2.Columns.Add(COL_NAM_VECTOR_CORRECTX, nWidth);
            //--------------------------------------------------------------------------------------
            //[VB]      grdFlexGrid.TextMatrix(0, COL_NUM_VECTOR_CORRECTY) = COL_NAM_VECTOR_CORRECTY
            nWidth = 100;
            nTotalWidth += nWidth;
            listPane.grdFlexGrid2.Columns.Add(COL_NAM_VECTOR_CORRECTY, nWidth);
            //--------------------------------------------------------------------------------------
            //[VB]      grdFlexGrid.TextMatrix(0, COL_NUM_VECTOR_CORRECTZ) = COL_NAM_VECTOR_CORRECTZ
            nWidth = 100;
            nTotalWidth += nWidth;
            listPane.grdFlexGrid2.Columns.Add(COL_NAM_VECTOR_CORRECTZ, nWidth);
            //--------------------------------------------------------------------------------------

            //--------------------------------------------------------------------------------------
            //grdFlexGrid2.Width = (int)nTotalWidth;
            listPane.grdFlexGrid2.Width = 700;
            listPane.grdFlexGrid2.Height = 400;
            //--------------------------------------------------------------------------------------
            //--------------------------------------------------------------------------------------
        }
        //--------------------------------------------------------------------------------
        //--------------------------------------------------------------------------------
        //[VB]  '基線ベクトルリストを作成する。
        //[VB]  '
        //[VB]  '引き数：
        //[VB]  'grdFlexGrid リストコントロール。
        //[VB]  Public Sub MakeListVector(ByVal grdFlexGrid As NSFlexGrid)
        //[VB]  
        //[VB]      'リストのクリア。
        //[VB]      Dim bRedraw As Boolean
        //[VB]      bRedraw = grdFlexGrid.Redraw
        //[VB]      grdFlexGrid.Redraw = False
        //[VB]      grdFlexGrid.Clear
        //[VB]      
        //[VB]      'カラム名称。
        //[VB]      grdFlexGrid.Cols = COL_NUM_VECTOR_COUNT
        //[VB]      grdFlexGrid.FixedCols = 1
        //[VB]      grdFlexGrid.TextMatrix(0, COL_NUM_VECTOR_WORK) = COL_NAM_VECTOR_WORK
        //[VB]      grdFlexGrid.TextMatrix(0, COL_NUM_VECTOR_ENABLE) = COL_NAM_VECTOR_ENABLE
        //[VB]      grdFlexGrid.TextMatrix(0, COL_NUM_VECTOR_ADOPT) = COL_NAM_VECTOR_ADOPT
        //[VB]      grdFlexGrid.TextMatrix(0, COL_NUM_VECTOR_SESSION) = COL_NAM_VECTOR_SESSION
        //[VB]      grdFlexGrid.TextMatrix(0, COL_NUM_VECTOR_STRNUMBER) = COL_NAM_VECTOR_STRNUMBER
        //[VB]      grdFlexGrid.TextMatrix(0, COL_NUM_VECTOR_ENDNUMBER) = COL_NAM_VECTOR_ENDNUMBER
        //[VB]      grdFlexGrid.TextMatrix(0, COL_NUM_VECTOR_STRTIME) = COL_NAM_VECTOR_STRTIME
        //[VB]      grdFlexGrid.TextMatrix(0, COL_NUM_VECTOR_ENDTIME) = COL_NAM_VECTOR_ENDTIME
        //[VB]      grdFlexGrid.TextMatrix(0, COL_NUM_VECTOR_ANALYSIS) = COL_NAM_VECTOR_ANALYSIS
        //[VB]      grdFlexGrid.TextMatrix(0, COL_NUM_VECTOR_FREQUENCY) = COL_NAM_VECTOR_FREQUENCY
        //[VB]      grdFlexGrid.TextMatrix(0, COL_NUM_VECTOR_LC) = COL_NAM_VECTOR_LC
        //[VB]      //'grdFlexGrid.TextMatrix(0, COL_NUM_VECTOR_SOLVEMODE) = COL_NAM_VECTOR_SOLVEMODE
        //[VB]      grdFlexGrid.TextMatrix(0, COL_NUM_VECTOR_ANALYSISSTRTIME) = COL_NAM_VECTOR_ANALYSISSTRTIME
        //[VB]      grdFlexGrid.TextMatrix(0, COL_NUM_VECTOR_ANALYSISENDTIME) = COL_NAM_VECTOR_ANALYSISENDTIME
        //[VB]      grdFlexGrid.TextMatrix(0, COL_NUM_VECTOR_DX) = COL_NAM_VECTOR_DX
        //[VB]      grdFlexGrid.TextMatrix(0, COL_NUM_VECTOR_DY) = COL_NAM_VECTOR_DY
        //[VB]      grdFlexGrid.TextMatrix(0, COL_NUM_VECTOR_DZ) = COL_NAM_VECTOR_DZ
        //[VB]      grdFlexGrid.TextMatrix(0, COL_NUM_VECTOR_BIAS) = COL_NAM_VECTOR_BIAS
        //[VB]      //'grdFlexGrid.TextMatrix(0, COL_NUM_VECTOR_AMBPERCENTAGE) = COL_NAM_VECTOR_AMBPERCENTAGE
        //[VB]      grdFlexGrid.TextMatrix(0, COL_NUM_VECTOR_REJECTRATIO) = COL_NAM_VECTOR_REJECTRATIO
        //[VB]      //'grdFlexGrid.TextMatrix(0, COL_NUM_VECTOR_RMS) = COL_NAM_VECTOR_RMS
        //[VB]      grdFlexGrid.TextMatrix(0, COL_NUM_VECTOR_XX) = COL_NAM_VECTOR_XX
        //[VB]      grdFlexGrid.TextMatrix(0, COL_NUM_VECTOR_YY) = COL_NAM_VECTOR_YY
        //[VB]      grdFlexGrid.TextMatrix(0, COL_NUM_VECTOR_ZZ) = COL_NAM_VECTOR_ZZ
        //[VB]      grdFlexGrid.TextMatrix(0, COL_NUM_VECTOR_XY) = COL_NAM_VECTOR_XY
        //[VB]      grdFlexGrid.TextMatrix(0, COL_NUM_VECTOR_XZ) = COL_NAM_VECTOR_XZ
        //[VB]      grdFlexGrid.TextMatrix(0, COL_NUM_VECTOR_YZ) = COL_NAM_VECTOR_YZ
        //[VB]      grdFlexGrid.TextMatrix(0, COL_NUM_VECTOR_CORRECTX) = COL_NAM_VECTOR_CORRECTX
        //[VB]      grdFlexGrid.TextMatrix(0, COL_NUM_VECTOR_CORRECTY) = COL_NAM_VECTOR_CORRECTY
        //[VB]      grdFlexGrid.TextMatrix(0, COL_NUM_VECTOR_CORRECTZ) = COL_NAM_VECTOR_CORRECTZ
        //[VB]      
        //[VB]      '行数。
        //[VB]      grdFlexGrid.FixedRows = 1
        //[VB]      
        //[VB]      'カラムアライメント。
        //[VB]      grdFlexGrid.ColAlignment(COL_NUM_VECTOR_WORK) = flexAlignCenterCenter
        //[VB]      grdFlexGrid.ColAlignment(COL_NUM_VECTOR_ENABLE) = flexAlignCenterCenter
        //[VB]      grdFlexGrid.ColAlignment(COL_NUM_VECTOR_ADOPT) = flexAlignCenterCenter
        //[VB]      grdFlexGrid.ColAlignment(COL_NUM_VECTOR_SESSION) = flexAlignLeftCenter
        //[VB]      grdFlexGrid.ColAlignment(COL_NUM_VECTOR_STRNUMBER) = flexAlignLeftCenter
        //[VB]      grdFlexGrid.ColAlignment(COL_NUM_VECTOR_ENDNUMBER) = flexAlignLeftCenter
        //[VB]      grdFlexGrid.ColAlignment(COL_NUM_VECTOR_STRTIME) = flexAlignLeftCenter
        //[VB]      grdFlexGrid.ColAlignment(COL_NUM_VECTOR_ENDTIME) = flexAlignLeftCenter
        //[VB]      grdFlexGrid.ColAlignment(COL_NUM_VECTOR_ANALYSIS) = flexAlignCenterCenter
        //[VB]      grdFlexGrid.ColAlignment(COL_NUM_VECTOR_FREQUENCY) = flexAlignCenterCenter
        //[VB]      grdFlexGrid.ColAlignment(COL_NUM_VECTOR_LC) = flexAlignCenterCenter
        //[VB]      //'grdFlexGrid.ColAlignment(COL_NUM_VECTOR_SOLVEMODE) = flexAlignCenterCenter
        //[VB]      grdFlexGrid.ColAlignment(COL_NUM_VECTOR_ANALYSISSTRTIME) = flexAlignLeftCenter
        //[VB]      grdFlexGrid.ColAlignment(COL_NUM_VECTOR_ANALYSISENDTIME) = flexAlignLeftCenter
        //[VB]      grdFlexGrid.ColAlignment(COL_NUM_VECTOR_DX) = flexAlignRightCenter
        //[VB]      grdFlexGrid.ColAlignment(COL_NUM_VECTOR_DY) = flexAlignRightCenter
        //[VB]      grdFlexGrid.ColAlignment(COL_NUM_VECTOR_DZ) = flexAlignRightCenter
        //[VB]      grdFlexGrid.ColAlignment(COL_NUM_VECTOR_BIAS) = flexAlignRightCenter
        //[VB]      //'grdFlexGrid.ColAlignment(COL_NUM_VECTOR_AMBPERCENTAGE) = flexAlignRightCenter
        //[VB]      grdFlexGrid.ColAlignment(COL_NUM_VECTOR_REJECTRATIO) = flexAlignRightCenter
        //[VB]      //'grdFlexGrid.ColAlignment(COL_NUM_VECTOR_RMS) = flexAlignRightCenter
        //[VB]      grdFlexGrid.ColAlignment(COL_NUM_VECTOR_XX) = flexAlignRightCenter
        //[VB]      grdFlexGrid.ColAlignment(COL_NUM_VECTOR_YY) = flexAlignRightCenter
        //[VB]      grdFlexGrid.ColAlignment(COL_NUM_VECTOR_ZZ) = flexAlignRightCenter
        //[VB]      grdFlexGrid.ColAlignment(COL_NUM_VECTOR_XY) = flexAlignRightCenter
        //[VB]      grdFlexGrid.ColAlignment(COL_NUM_VECTOR_XZ) = flexAlignRightCenter
        //[VB]      grdFlexGrid.ColAlignment(COL_NUM_VECTOR_YZ) = flexAlignRightCenter
        //[VB]      grdFlexGrid.ColAlignment(COL_NUM_VECTOR_CORRECTX) = flexAlignRightCenter
        //[VB]      grdFlexGrid.ColAlignment(COL_NUM_VECTOR_CORRECTY) = flexAlignRightCenter
        //[VB]      grdFlexGrid.ColAlignment(COL_NUM_VECTOR_CORRECTZ) = flexAlignRightCenter
        //[VB]      '固定行のアライメント。
        //[VB]      grdFlexGrid.Row = 0
        //[VB]      Dim i As Long
        //[VB]          For i = 0 To COL_NUM_VECTOR_COUNT - 1
        //[VB]          grdFlexGrid.Col = i
        //[VB]          grdFlexGrid.CellAlignment = flexAlignCenterCenter
        //[VB]      Next
        //[VB]      
        //[VB]      'カラム幅。
        //[VB]      grdFlexGrid.ColWidth(COL_NUM_VECTOR_WORK) = COL_WID_VECTOR_WORK
        //[VB]      grdFlexGrid.ColWidth(COL_NUM_VECTOR_ENABLE) = COL_WID_VECTOR_ENABLE
        //[VB]      grdFlexGrid.ColWidth(COL_NUM_VECTOR_ADOPT) = COL_WID_VECTOR_ADOPT
        //[VB]      grdFlexGrid.ColWidth(COL_NUM_VECTOR_SESSION) = COL_WID_VECTOR_SESSION
        //[VB]      grdFlexGrid.ColWidth(COL_NUM_VECTOR_STRNUMBER) = COL_WID_VECTOR_STRNUMBER
        //[VB]      grdFlexGrid.ColWidth(COL_NUM_VECTOR_ENDNUMBER) = COL_WID_VECTOR_ENDNUMBER
        //[VB]      grdFlexGrid.ColWidth(COL_NUM_VECTOR_STRTIME) = COL_WID_VECTOR_STRTIME
        //[VB]      grdFlexGrid.ColWidth(COL_NUM_VECTOR_ENDTIME) = COL_WID_VECTOR_ENDTIME
        //[VB]      grdFlexGrid.ColWidth(COL_NUM_VECTOR_ANALYSIS) = COL_WID_VECTOR_ANALYSIS
        //[VB]      grdFlexGrid.ColWidth(COL_NUM_VECTOR_FREQUENCY) = COL_WID_VECTOR_FREQUENCY
        //[VB]      grdFlexGrid.ColWidth(COL_NUM_VECTOR_LC) = COL_WID_VECTOR_LC
        //[VB]      //'grdFlexGrid.ColWidth(COL_NUM_VECTOR_SOLVEMODE) = COL_WID_VECTOR_SOLVEMODE
        //[VB]      grdFlexGrid.ColWidth(COL_NUM_VECTOR_ANALYSISSTRTIME) = COL_WID_VECTOR_ANALYSISSTRTIME
        //[VB]      grdFlexGrid.ColWidth(COL_NUM_VECTOR_ANALYSISENDTIME) = COL_WID_VECTOR_ANALYSISENDTIME
        //[VB]      grdFlexGrid.ColWidth(COL_NUM_VECTOR_DX) = COL_WID_VECTOR_DX
        //[VB]      grdFlexGrid.ColWidth(COL_NUM_VECTOR_DY) = COL_WID_VECTOR_DY
        //[VB]      grdFlexGrid.ColWidth(COL_NUM_VECTOR_DZ) = COL_WID_VECTOR_DZ
        //[VB]      grdFlexGrid.ColWidth(COL_NUM_VECTOR_BIAS) = COL_WID_VECTOR_BIAS
        //[VB]      //'grdFlexGrid.ColWidth(COL_NUM_VECTOR_AMBPERCENTAGE) = COL_WID_VECTOR_AMBPERCENTAGE
        //[VB]      grdFlexGrid.ColWidth(COL_NUM_VECTOR_REJECTRATIO) = COL_WID_VECTOR_REJECTRATIO
        //[VB]      //'grdFlexGrid.ColWidth(COL_NUM_VECTOR_RMS) = COL_WID_VECTOR_RMS
        //[VB]      grdFlexGrid.ColWidth(COL_NUM_VECTOR_XX) = COL_WID_VECTOR_XX
        //[VB]      grdFlexGrid.ColWidth(COL_NUM_VECTOR_YY) = COL_WID_VECTOR_YY
        //[VB]      grdFlexGrid.ColWidth(COL_NUM_VECTOR_ZZ) = COL_WID_VECTOR_ZZ
        //[VB]      grdFlexGrid.ColWidth(COL_NUM_VECTOR_XY) = COL_WID_VECTOR_XY
        //[VB]      grdFlexGrid.ColWidth(COL_NUM_VECTOR_XZ) = COL_WID_VECTOR_XZ
        //[VB]      grdFlexGrid.ColWidth(COL_NUM_VECTOR_YZ) = COL_WID_VECTOR_YZ
        //[VB]      grdFlexGrid.ColWidth(COL_NUM_VECTOR_CORRECTX) = COL_WID_VECTOR_CORRECTX
        //[VB]      grdFlexGrid.ColWidth(COL_NUM_VECTOR_CORRECTY) = COL_WID_VECTOR_CORRECTY
        //[VB]      grdFlexGrid.ColWidth(COL_NUM_VECTOR_CORRECTZ) = COL_WID_VECTOR_CORRECTZ
        //[VB]      
        //[VB]      '表示。
        //[VB]      grdFlexGrid.Redraw = bRedraw
        //[VB]      
        //[VB]      
        //[VB]  End Sub
        //'*******************************************************************************
        //'*******************************************************************************

    }
}
