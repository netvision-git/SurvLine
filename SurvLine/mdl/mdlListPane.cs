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
using System.Runtime.Remoting;
using static SurvLine.mdl.MdlNSDefine;
using static SurvLine.mdl.MdlNSUtility;
using static SurvLine.mdl.MdlNSSDefine;
using static SurvLine.mdl.MdlNSSUtility;
using static SurvLine.mdl.MdlNSGUI;
using static SurvLine.mdl.MdlBaseLineAnalyser;
using static SurvLine.mdl.MdlAccountMakeNSS;
using static SurvLine.mdl.MdlOutputFile;
using Microsoft.VisualBasic;
using System.Xml.Linq;
using Microsoft.VisualBasic.Logging;

//namespace SurvLine.mdl
namespace SurvLine.mdl
{

    public class MdlListPane
    {

        private MdlListProc m_clsMdlListProc;

        public MdlListPane(MdlMain clsMdlMain)
        {
            m_clsMdlListProc = new MdlListProc(clsMdlMain, this);

        }



        //'*******************************************************************************
        //'リストペインネイティブコード


        //リスト種別。
        public enum LIST_NUM_PANE
        {
            LIST_NUM_OBSPNT = 0,    //'観測点。
            LIST_NUM_ZAHYOU,        //'座標。
            LIST_NUM_VECTOR,        //'ベクトル。
            LIST_NUM_COUNT,         //'リスト番号数。
        }

        //'リストオブジェクト種別インデックス。
        public enum TagLIST_OBJ_TYPE_INDEX
        {
            LIST_OBJ_TYPE_INDEX_OBSERVATIONPOINT = 0,   //'観測点。
            LIST_OBJ_TYPE_INDEX_ZAHYOU,                 //'座標。
            LIST_OBJ_TYPE_INDEX_BASELINEVECTOR,         //'基線ベクトル。
            LIST_OBJ_TYPE_INDEX_COUNT,                  //'リスト種別数。
        }

        //'リスト名称。
        public const string LIST_NAM_OBSPNT = "観測点";
        public const string LIST_NAM_ZAHYOU = "座標";
        public const string LIST_NAM_VECTOR = "ﾍﾞｸﾄﾙ";

        //'観測点カラム番号。
        public enum COL_NUM_OBSPNT
        {
            COL_NUM_OBSPNT_WORK = 0,        //'作業用。
            COL_NUM_OBSPNT_ENABLE,           //'有効/無効。
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
#if false
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
#else
        public const long COL_WID_OBSPNT_WORK = 0;          //'観測点カラム幅(Twips)、作業用。
        public const long COL_WID_OBSPNT_ENABLE = 60;      //'観測点カラム幅(Twips)、有効/無効。
        public const long COL_WID_OBSPNT_TYPE = 60;        //'観測点カラム幅(Twips)、観測点種類。
        public const long COL_WID_OBSPNT_MODE = 60;        //'観測点カラム幅(Twips)、観測点モード。
        public const long COL_WID_OBSPNT_SESSION = 75;     //'観測点カラム幅(Twips)、セッション名。
        public const long COL_WID_OBSPNT_NUMBER = 100;     //'観測点カラム幅(Twips)、観測点番号。
        public const long COL_WID_OBSPNT_NAME = 100;       //'観測点カラム幅(Twips)、観測点名称。
        public const long COL_WID_OBSPNT_STRTIME = 130;    //'観測点カラム幅(Twips)、観測開始日時。
        public const long COL_WID_OBSPNT_ENDTIME = 130;    //'観測点カラム幅(Twips)、観測終了日時。
        public const long COL_WID_OBSPNT_RECTYPE = 200;    //'観測点カラム幅(Twips)、受信機名称。
        public const long COL_WID_OBSPNT_RECNUMBER = 100;  //'観測点カラム幅(Twips)、受信機シリアル。
        public const long COL_WID_OBSPNT_ANTTYPE = 200;    //'観測点カラム幅(Twips)、アンテナ種別。
        public const long COL_WID_OBSPNT_ANTNUMBER = 100;     //'観測点カラム幅(Twips)、アンテナシリアル。
        public const long COL_WID_OBSPNT_ANTMEASUREMENT = 120;//'観測点カラム幅(Twips)、アンテナ測位方法。
        public const long COL_WID_OBSPNT_ANTHEIGHT = 100;     //'観測点カラム幅(Twips)、アンテナ高。
        public const long COL_WID_OBSPNT_ANTMOUNT = 110;      //'観測点カラム幅(Twips)、アンテナ底面高。
        public const long COL_WID_OBSPNT_ANTCENTER = 110;     //'観測点カラム幅(Twips)、アンテナ位相中心高。
        public const long COL_WID_OBSPNT_INTERVAL = 100;      //'観測点カラム幅(Twips)、取得間隔。
        public const long COL_WID_OBSPNT_ELEVATIONMASK = 100; //'観測点カラム幅(Twips)、仰角マスク。
        public const long COL_WID_OBSPNT_SATELLITE = 100;     //'観測点カラム幅(Twips)、最少衛星数。
        //'public const long COL_WID_OBSPNT_LAT = 1800 '観測点カラム幅(Twips)、緯度。
        //'public const long COL_WID_OBSPNT_LON = 1800 '観測点カラム幅(Twips)、経度。
        //'public const long COL_WID_OBSPNT_HEIGHT = 1300 '観測点カラム幅(Twips)、楕円体高。
        //'public const long COL_WID_OBSPNT_ALT = 1300 '観測点カラム幅(Twips)、標高。
        //'public const long COL_WID_OBSPNT_X = 1728 '観測点カラム幅(Twips)、Ｘ。
        //'public const long COL_WID_OBSPNT_Y = 1728 '観測点カラム幅(Twips)、Ｙ。
        //'public const long COL_WID_OBSPNT_Z = 1728 '観測点カラム幅(Twips)、Ｚ。
#endif
#if false
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
#else
        public const long COL_WID_VECTOR_WORK = 0;            //'ベクトルカラム幅(Twips)、作業用。
        public const long COL_WID_VECTOR_ENABLE = 60;        //'ベクトルカラム幅(Twips)、有効/無効。
        public const long COL_WID_VECTOR_ADOPT = 60;         //'ベクトルカラム幅(Twips)、採用/点検。
        public const long COL_WID_VECTOR_SESSION = 75;       //'ベクトルカラム幅(Twips)、セッション名。
        public const long COL_WID_VECTOR_STRNUMBER = 70;     //'ベクトルカラム幅(Twips)、始点観測点番号。 文字数を削減 2006/10/12 NGS Yamada
        public const long COL_WID_VECTOR_ENDNUMBER = 70;     //'ベクトルカラム幅(Twips)、終点観測点番号。
        //'public const long COL_WID_VECTOR_STRNUMBER = 1400 'ベクトルカラム幅(Twips)、始点観測点番号。
        //'public const long COL_WID_VECTOR_ENDNUMBER = 1400 'ベクトルカラム幅(Twips)、終点観測点番号。
        public const long COL_WID_VECTOR_STRTIME = 140;     //'ベクトルカラム幅(Twips)、観測開始日時。
        public const long COL_WID_VECTOR_ENDTIME = 140;     //'ベクトルカラム幅(Twips)、観測終了日時。
        public const long COL_WID_VECTOR_ANALYSIS = 80;    //'ベクトルカラム幅(Twips)、解の種類。
        public const long COL_WID_VECTOR_FREQUENCY = 90;   //'ベクトルカラム幅(Twips)、使用周波数。
        public const long COL_WID_VECTOR_LC = 90;          //'ベクトルカラム幅(Twips)、解析周波数。
        public const long COL_WID_VECTOR_SOLVEMODE = 100;   //'ベクトルカラム幅(Twips)、基線解析モード。
        public const long COL_WID_VECTOR_ANALYSISSTRTIME = 140;     //'ベクトルカラム幅(Twips)、解析開始日時。
        public const long COL_WID_VECTOR_ANALYSISENDTIME = 140;     //'ベクトルカラム幅(Twips)、解析終了日時。
        public const long COL_WID_VECTOR_DX = 80;         //'ベクトルカラム幅(Twips)、ＤＸ。
        public const long COL_WID_VECTOR_DY = 80;         //'ベクトルカラム幅(Twips)、ＤＹ。
        public const long COL_WID_VECTOR_DZ = 80;         //'ベクトルカラム幅(Twips)、ＤＺ。
        public const long COL_WID_VECTOR_BIAS = 100;       //'ベクトルカラム幅(Twips)、バイアス決定比。
        public const long COL_WID_VECTOR_AMBPERCENTAGE = 80;   //'ベクトルカラム幅(Twips)、FIX率。
        public const long COL_WID_VECTOR_REJECTRATIO = 80;     //'ベクトルカラム幅(Twips)、棄却率。
        public const long COL_WID_VECTOR_RMS = 90;        //'ベクトルカラム幅(Twips)、RMS値。
        public const long COL_WID_VECTOR_XX = 90;         //'ベクトルカラム幅(Twips)、分散Ｘ。
        public const long COL_WID_VECTOR_YY = 90;         //'ベクトルカラム幅(Twips)、分散Ｙ。
        public const long COL_WID_VECTOR_ZZ = 90;         //'ベクトルカラム幅(Twips)、分散Ｚ。
        public const long COL_WID_VECTOR_XY = 90;         //'ベクトルカラム幅(Twips)、共分散ＸＹ。
        public const long COL_WID_VECTOR_XZ = 90;         //'ベクトルカラム幅(Twips)、共分散ＸＺ。
        public const long COL_WID_VECTOR_YZ = 90;         //'ベクトルカラム幅(Twips)、共分散ＹＺ。
        public const long COL_WID_VECTOR_CORRECTX = 110;   //'ベクトルカラム幅(Twips)、偏心補正後ＤＸ。
        public const long COL_WID_VECTOR_CORRECTY = 110;   //'ベクトルカラム幅(Twips)、偏心補正後ＤＹ。
        public const long COL_WID_VECTOR_CORRECTZ = 110;   //'ベクトルカラム幅(Twips)、偏心補正後ＤＺ。
#endif
        public const long GUI_PERCENTAGE_DECIMAL = 1;       //'率。

        //==========================================================================================
        /*[VB]
        'ソートオーダーの初期化。
        '
        '引き数：
        'clsListSort 初期化する ListSort オブジェクトの配列。配列の要素は(0 To LIST_NUM_COUNT - 1)。
        Public Sub InitializeSortOrder(ByRef clsListSort() As ListSort)

            Call clsListSort(LIST_NUM_OBSPNT).Initialize(COL_NUM_OBSPNT_COUNT)
            Call clsListSort(LIST_NUM_OBSPNT).ReplaceSortOrder(COL_NUM_OBSPNT_SESSION)
            Call clsListSort(LIST_NUM_OBSPNT).ReplaceSortOrder(COL_NUM_OBSPNT_NUMBER)
    
            Call clsListSort(LIST_NUM_VECTOR).Initialize(COL_NUM_VECTOR_COUNT)
            Call clsListSort(LIST_NUM_VECTOR).ReplaceSortOrder(COL_NUM_VECTOR_ENDTIME)
            Call clsListSort(LIST_NUM_VECTOR).ReplaceSortOrder(COL_NUM_VECTOR_STRTIME)
            Call clsListSort(LIST_NUM_VECTOR).ReplaceSortOrder(COL_NUM_VECTOR_ENDNUMBER)
            Call clsListSort(LIST_NUM_VECTOR).ReplaceSortOrder(COL_NUM_VECTOR_STRNUMBER)
            Call clsListSort(LIST_NUM_VECTOR).ReplaceSortOrder(COL_NUM_VECTOR_SESSION)
    
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        'ソートオーダーの初期化。
        '
        '引き数：
        'clsListSort 初期化する ListSort オブジェクトの配列。配列の要素は(0 To LIST_NUM_COUNT - 1)。
        */
        public void InitializeSortOrder(ref ListSort[] clsListSort)
        {
            //'観測点。
            clsListSort[(int)LIST_NUM_PANE.LIST_NUM_OBSPNT].Initialize((long)COL_NUM_OBSPNT.COL_NUM_OBSPNT_COUNT);
            clsListSort[(int)LIST_NUM_PANE.LIST_NUM_OBSPNT].ReplaceSortOrder((long)COL_NUM_OBSPNT.COL_NUM_OBSPNT_SESSION);
            clsListSort[(int)LIST_NUM_PANE.LIST_NUM_OBSPNT].ReplaceSortOrder((long)COL_NUM_OBSPNT.COL_NUM_OBSPNT_NUMBER);

            //'ベクトル。
            clsListSort[(int)LIST_NUM_PANE.LIST_NUM_VECTOR].Initialize((long)COL_NUM_VECTOR.COL_NUM_VECTOR_COUNT);
            clsListSort[(int)LIST_NUM_PANE.LIST_NUM_VECTOR].ReplaceSortOrder((long)COL_NUM_VECTOR.COL_NUM_VECTOR_ENDTIME);
            clsListSort[(int)LIST_NUM_PANE.LIST_NUM_VECTOR].ReplaceSortOrder((long)COL_NUM_VECTOR.COL_NUM_VECTOR_STRTIME);
            clsListSort[(int)LIST_NUM_PANE.LIST_NUM_VECTOR].ReplaceSortOrder((long)COL_NUM_VECTOR.COL_NUM_VECTOR_ENDNUMBER);
            clsListSort[(int)LIST_NUM_PANE.LIST_NUM_VECTOR].ReplaceSortOrder((long)COL_NUM_VECTOR.COL_NUM_VECTOR_STRNUMBER);
            clsListSort[(int)LIST_NUM_PANE.LIST_NUM_VECTOR].ReplaceSortOrder((long)COL_NUM_VECTOR.COL_NUM_VECTOR_SESSION);


        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'リストソート比較。
        '
        'リストのソートにおける比較関数。
        'nList で指定されるリストを対象とする。
        'Row1 と Row2 で指定される行を比較する。
        'Cmp に比較の結果を設定する。
        'Row1 が Row2 より前の場合 -1 を設定する。
        'Row1 と Row2 が等しい場合 0 を設定する。
        'Row1 が Row2 より後の場合 1 を設定する。
        '
        '引き数：
        'nList リスト種別。
        'grdFlexGrid リストコントロール。
        'clsListSort ListSort オブジェクト。
        'Row1 比較する行の番号。
        'Row2 比較する行の番号。
        'Cmp 比較の結果。
        Public Sub CompareSortList(ByVal nList As Long, ByVal grdFlexGrid As NSFlexGrid, ByVal clsListSort As ListSort, ByVal Row1 As Long, ByVal Row2 As Long, Cmp As Integer)
            Select Case nList
            Case LIST_NUM_OBSPNT
                Call CompareSortObsPnt(grdFlexGrid, clsListSort, Row1, Row2, Cmp)
            Case LIST_NUM_VECTOR
                Call CompareSortVector(grdFlexGrid, clsListSort, Row1, Row2, Cmp)
            End Select
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        'リストソート比較。
        '
        'リストのソートにおける比較関数。
        'nList で指定されるリストを対象とする。
        'Row1 と Row2 で指定される行を比較する。
        'Cmp に比較の結果を設定する。
        'Row1 が Row2 より前の場合 -1 を設定する。
        'Row1 と Row2 が等しい場合 0 を設定する。
        'Row1 が Row2 より後の場合 1 を設定する。
        '
        '引き数：
        'nList リスト種別。
        'grdFlexGrid リストコントロール。
        'clsListSort ListSort オブジェクト。
        'Row1 比較する行の番号。
        'Row2 比較する行の番号。
        'Cmp 比較の結果。
        */
        public void CompareSortList(long nList, DataGridView grdFlexGrid, ListSort clsListSort, long Row1, long Row2,ref int Cmp)
        {
            switch (nList)
            {
                case (long)LIST_NUM_PANE.LIST_NUM_OBSPNT:       //'観測点。
                    CompareSortObsPnt(grdFlexGrid, clsListSort, Row1, Row2, ref Cmp);
                    break;
                case (long)LIST_NUM_PANE.LIST_NUM_VECTOR:       //'ベクトル。
                    CompareSortVector(grdFlexGrid, clsListSort, Row1, Row2, ref Cmp);
                    break;
                case (long)LIST_NUM_PANE.LIST_NUM_ZAHYOU:       //'座標。

                    break;
            }
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '観測点リストソート比較。
        '
        'Row1 と Row2 で指定される行を比較する。
        'Cmp に比較の結果を設定する。
        'Row1 が Row2 より前の場合 -1 を設定する。
        'Row1 と Row2 が等しい場合 0 を設定する。
        'Row1 が Row2 より後の場合 1 を設定する。
        '比較するカラムの順位は clsListSort に従う。
        '
        '引き数：
        'grdFlexGrid リストコントロール。
        'clsListSort ListSort オブジェクト。
        'Row1 比較する行の番号。
        'Row2 比較する行の番号。
        'Cmp 比較の結果。
        Public Sub CompareSortObsPnt(ByVal grdFlexGrid As NSFlexGrid, ByVal clsListSort As ListSort, ByVal Row1 As Long, ByVal Row2 As Long, Cmp As Integer)

            Dim nCompare As Double
            Dim i As Long
            For i = 0 To clsListSort.Cols - 1
                Select Case clsListSort.Col(i)
                Case COL_NUM_OBSPNT_WORK
                    Cmp = StrComp(grdFlexGrid.TextMatrix(Row1, clsListSort.Col(i)), grdFlexGrid.TextMatrix(Row2, clsListSort.Col(i)))
                    If Cmp <> 0 Then Exit For
                Case COL_NUM_OBSPNT_ENABLE
                    Cmp = StrComp(grdFlexGrid.TextMatrix(Row1, clsListSort.Col(i)), grdFlexGrid.TextMatrix(Row2, clsListSort.Col(i)))
                    If Cmp <> 0 Then Exit For
                Case COL_NUM_OBSPNT_TYPE
                    Cmp = StrComp(grdFlexGrid.TextMatrix(Row1, clsListSort.Col(i)), grdFlexGrid.TextMatrix(Row2, clsListSort.Col(i)))
                    If Cmp <> 0 Then Exit For
                Case COL_NUM_OBSPNT_MODE
                    Cmp = StrComp(grdFlexGrid.TextMatrix(Row1, clsListSort.Col(i)), grdFlexGrid.TextMatrix(Row2, clsListSort.Col(i)))
                    If Cmp <> 0 Then Exit For
                Case COL_NUM_OBSPNT_SESSION
                    Cmp = StrComp(grdFlexGrid.TextMatrix(Row1, clsListSort.Col(i)), grdFlexGrid.TextMatrix(Row2, clsListSort.Col(i)))
                    If Cmp <> 0 Then Exit For
                Case COL_NUM_OBSPNT_NUMBER
                    Cmp = StrComp(grdFlexGrid.TextMatrix(Row1, clsListSort.Col(i)), grdFlexGrid.TextMatrix(Row2, clsListSort.Col(i)))
                    If Cmp <> 0 Then Exit For
                Case COL_NUM_OBSPNT_NAME
                    Cmp = StrComp(grdFlexGrid.TextMatrix(Row1, clsListSort.Col(i)), grdFlexGrid.TextMatrix(Row2, clsListSort.Col(i)))
                    If Cmp <> 0 Then Exit For
                Case COL_NUM_OBSPNT_STRTIME
                    Cmp = StrComp(grdFlexGrid.TextMatrix(Row1, clsListSort.Col(i)), grdFlexGrid.TextMatrix(Row2, clsListSort.Col(i)))
                    If Cmp <> 0 Then Exit For
                Case COL_NUM_OBSPNT_ENDTIME
                    Cmp = StrComp(grdFlexGrid.TextMatrix(Row1, clsListSort.Col(i)), grdFlexGrid.TextMatrix(Row2, clsListSort.Col(i)))
                    If Cmp <> 0 Then Exit For
                Case COL_NUM_OBSPNT_RECTYPE
                    Cmp = StrComp(grdFlexGrid.TextMatrix(Row1, clsListSort.Col(i)), grdFlexGrid.TextMatrix(Row2, clsListSort.Col(i)))
                    If Cmp <> 0 Then Exit For
                Case COL_NUM_OBSPNT_RECNUMBER
                    Cmp = StrComp(grdFlexGrid.TextMatrix(Row1, clsListSort.Col(i)), grdFlexGrid.TextMatrix(Row2, clsListSort.Col(i)))
                    If Cmp <> 0 Then Exit For
                Case COL_NUM_OBSPNT_ANTTYPE
                    Cmp = StrComp(grdFlexGrid.TextMatrix(Row1, clsListSort.Col(i)), grdFlexGrid.TextMatrix(Row2, clsListSort.Col(i)))
                    If Cmp <> 0 Then Exit For
                Case COL_NUM_OBSPNT_ANTNUMBER
                    Cmp = StrComp(grdFlexGrid.TextMatrix(Row1, clsListSort.Col(i)), grdFlexGrid.TextMatrix(Row2, clsListSort.Col(i)))
                    If Cmp <> 0 Then Exit For
                Case COL_NUM_OBSPNT_ANTMEASUREMENT
                    Cmp = StrComp(grdFlexGrid.TextMatrix(Row1, clsListSort.Col(i)), grdFlexGrid.TextMatrix(Row2, clsListSort.Col(i)))
                    If Cmp <> 0 Then Exit For
                Case COL_NUM_OBSPNT_ANTHEIGHT
                    nCompare = Val(grdFlexGrid.TextMatrix(Row1, clsListSort.Col(i))) - Val(grdFlexGrid.TextMatrix(Row2, clsListSort.Col(i)))
                    Cmp = IIf(nCompare < 0, -1, IIf(nCompare > 0, 1, 0))
                    If Cmp <> 0 Then Exit For
                Case COL_NUM_OBSPNT_ANTMOUNT
                    nCompare = Val(grdFlexGrid.TextMatrix(Row1, clsListSort.Col(i))) - Val(grdFlexGrid.TextMatrix(Row2, clsListSort.Col(i)))
                    Cmp = IIf(nCompare < 0, -1, IIf(nCompare > 0, 1, 0))
                    If Cmp <> 0 Then Exit For
                Case COL_NUM_OBSPNT_ANTCENTER
                    nCompare = Val(grdFlexGrid.TextMatrix(Row1, clsListSort.Col(i))) - Val(grdFlexGrid.TextMatrix(Row2, clsListSort.Col(i)))
                    Cmp = IIf(nCompare < 0, -1, IIf(nCompare > 0, 1, 0))
                    If Cmp <> 0 Then Exit For
                Case COL_NUM_OBSPNT_INTERVAL
                    nCompare = Val(grdFlexGrid.TextMatrix(Row1, clsListSort.Col(i))) - Val(grdFlexGrid.TextMatrix(Row2, clsListSort.Col(i)))
                    Cmp = IIf(nCompare < 0, -1, IIf(nCompare > 0, 1, 0))
                    If Cmp <> 0 Then Exit For
                Case COL_NUM_OBSPNT_ELEVATIONMASK
                    Cmp = StrComp(grdFlexGrid.TextMatrix(Row1, clsListSort.Col(i)), grdFlexGrid.TextMatrix(Row2, clsListSort.Col(i)))
                    If Cmp <> 0 Then Exit For
                Case COL_NUM_OBSPNT_SATELLITE
                    nCompare = Val(grdFlexGrid.TextMatrix(Row1, clsListSort.Col(i))) - Val(grdFlexGrid.TextMatrix(Row2, clsListSort.Col(i)))
                    Cmp = IIf(nCompare < 0, -1, IIf(nCompare > 0, 1, 0))
                    If Cmp <> 0 Then Exit For
        '        Case COL_NUM_OBSPNT_COORDLAT
        '            Cmp = StrComp(grdFlexGrid.TextMatrix(Row1, clsListSort.Col(i)), grdFlexGrid.TextMatrix(Row2, clsListSort.Col(i)))
        '            If Cmp <> 0 Then Exit For
        '        Case COL_NUM_OBSPNT_COORDLON
        '            Cmp = StrComp(grdFlexGrid.TextMatrix(Row1, clsListSort.Col(i)), grdFlexGrid.TextMatrix(Row2, clsListSort.Col(i)))
        '            If Cmp <> 0 Then Exit For
        '        Case COL_NUM_OBSPNT_COORDHEIGHT
        '            nCompare = Val(grdFlexGrid.TextMatrix(Row1, clsListSort.Col(i))) - Val(grdFlexGrid.TextMatrix(Row2, clsListSort.Col(i)))
        '            Cmp = IIf(nCompare < 0, -1, IIf(nCompare > 0, 1, 0))
        '            If Cmp <> 0 Then Exit For
        '        Case COL_NUM_OBSPNT_COORDALT
        '            nCompare = Val(grdFlexGrid.TextMatrix(Row1, clsListSort.Col(i))) - Val(grdFlexGrid.TextMatrix(Row2, clsListSort.Col(i)))
        '            Cmp = IIf(nCompare < 0, -1, IIf(nCompare > 0, 1, 0))
        '            If Cmp <> 0 Then Exit For
        '        Case COL_NUM_OBSPNT_COORDX
        '            nCompare = Val(grdFlexGrid.TextMatrix(Row1, clsListSort.Col(i))) - Val(grdFlexGrid.TextMatrix(Row2, clsListSort.Col(i)))
        '            Cmp = IIf(nCompare < 0, -1, IIf(nCompare > 0, 1, 0))
        '            If Cmp <> 0 Then Exit For
        '        Case COL_NUM_OBSPNT_COORDY
        '            nCompare = Val(grdFlexGrid.TextMatrix(Row1, clsListSort.Col(i))) - Val(grdFlexGrid.TextMatrix(Row2, clsListSort.Col(i)))
        '            Cmp = IIf(nCompare < 0, -1, IIf(nCompare > 0, 1, 0))
        '            If Cmp <> 0 Then Exit For
        '        Case COL_NUM_OBSPNT_COORDZ
        '            nCompare = Val(grdFlexGrid.TextMatrix(Row1, clsListSort.Col(i))) - Val(grdFlexGrid.TextMatrix(Row2, clsListSort.Col(i)))
        '            Cmp = IIf(nCompare < 0, -1, IIf(nCompare > 0, 1, 0))
        '            If Cmp <> 0 Then Exit For
                End Select
            Next
    
            '昇順降順変換。
            If i < clsListSort.Cols Then
                If Not clsListSort.Ascending(i) Then Cmp = -Cmp
            Else
                Cmp = 0
            End If
    
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '観測点リストソート比較。
        '
        'Row1 と Row2 で指定される行を比較する。
        'Cmp に比較の結果を設定する。
        'Row1 が Row2 より前の場合 -1 を設定する。
        'Row1 と Row2 が等しい場合 0 を設定する。
        'Row1 が Row2 より後の場合 1 を設定する。
        '比較するカラムの順位は clsListSort に従う。
        '
        '引き数：
        'grdFlexGrid リストコントロール。
        'clsListSort ListSort オブジェクト。
        'Row1 比較する行の番号。
        'Row2 比較する行の番号。
        'Cmp 比較の結果。
        */
        public void CompareSortObsPnt(DataGridView grdFlexGrid, ListSort clsListSort, long Row1, long Row2,ref int Cmp)
        {

            double nCompare;
            int i1;
            int i2 = (int)clsListSort.Cols();
            int i3;
            int rtn = 0;
            for (i1 = 0; i1 < i2; i1++)
            {
                if (rtn != 0)
                {
                    break;
                }
                i3 = (int)clsListSort.Col(i1);
                switch (i3)
                {
                    case (int)COL_NUM_OBSPNT.COL_NUM_OBSPNT_WORK:
                        Cmp = string.Compare(grdFlexGrid[i3, (int)Row1].ToString(), grdFlexGrid[i3, (int)Row2].ToString());
                        if (Cmp != 0) { rtn = -1; }
                        break;
                    case (int)COL_NUM_OBSPNT.COL_NUM_OBSPNT_ENABLE:
                        Cmp = string.Compare(grdFlexGrid[i3, (int)Row1].ToString(), grdFlexGrid[i3, (int)Row2].ToString());
                        if (Cmp != 0) { rtn = -1; }
                        break;
                    case (int)COL_NUM_OBSPNT.COL_NUM_OBSPNT_TYPE:
                        Cmp = string.Compare(grdFlexGrid[i3, (int)Row1].ToString(), grdFlexGrid[i3, (int)Row2].ToString());
                        if (Cmp != 0) { rtn = -1; }
                        break;
                    case (int)COL_NUM_OBSPNT.COL_NUM_OBSPNT_MODE:
                        Cmp = string.Compare(grdFlexGrid[i3, (int)Row1].ToString(), grdFlexGrid[i3, (int)Row2].ToString());
                        if (Cmp != 0) { rtn = -1; }
                        break;
                    case (int)COL_NUM_OBSPNT.COL_NUM_OBSPNT_SESSION:
                        Cmp = string.Compare(grdFlexGrid[i3, (int)Row1].ToString(), grdFlexGrid[i3, (int)Row2].ToString());
                        if (Cmp != 0) { rtn = -1; }
                        break;
                    case (int)COL_NUM_OBSPNT.COL_NUM_OBSPNT_NUMBER:
                        Cmp = string.Compare(grdFlexGrid[i3, (int)Row1].ToString(), grdFlexGrid[i3, (int)Row2].ToString());
                        if (Cmp != 0) { rtn = -1; }
                        break;
                    case (int)COL_NUM_OBSPNT.COL_NUM_OBSPNT_NAME:
                        Cmp = string.Compare(grdFlexGrid[i3, (int)Row1].ToString(), grdFlexGrid[i3, (int)Row2].ToString());
                        if (Cmp != 0) { rtn = -1; }
                        break;
                    case (int)COL_NUM_OBSPNT.COL_NUM_OBSPNT_STRTIME:
                        Cmp = string.Compare(grdFlexGrid[i3, (int)Row1].ToString(), grdFlexGrid[i3, (int)Row2].ToString());
                        if (Cmp != 0) { rtn = -1; }
                        break;
                    case (int)COL_NUM_OBSPNT.COL_NUM_OBSPNT_ENDTIME:
                        Cmp = string.Compare(grdFlexGrid[i3, (int)Row1].ToString(), grdFlexGrid[i3, (int)Row2].ToString());
                        if (Cmp != 0) { rtn = -1; }
                        break;
                    case (int)COL_NUM_OBSPNT.COL_NUM_OBSPNT_RECTYPE:
                        Cmp = string.Compare(grdFlexGrid[i3, (int)Row1].ToString(), grdFlexGrid[i3, (int)Row2].ToString());
                        if (Cmp != 0) { rtn = -1; }
                        break;
                    case (int)COL_NUM_OBSPNT.COL_NUM_OBSPNT_RECNUMBER:
                        Cmp = string.Compare(grdFlexGrid[i3, (int)Row1].ToString(), grdFlexGrid[i3, (int)Row2].ToString());
                        if (Cmp != 0) { rtn = -1; }
                        break;
                    case (int)COL_NUM_OBSPNT.COL_NUM_OBSPNT_ANTTYPE:
                        Cmp = string.Compare(grdFlexGrid[i3, (int)Row1].ToString(), grdFlexGrid[i3, (int)Row2].ToString());
                        if (Cmp != 0) { rtn = -1; }
                        break;
                    case (int)COL_NUM_OBSPNT.COL_NUM_OBSPNT_ANTNUMBER:
                        Cmp = string.Compare(grdFlexGrid[i3, (int)Row1].ToString(), grdFlexGrid[i3, (int)Row2].ToString());
                        if (Cmp != 0) { rtn = -1; }
                        break;
                    case (int)COL_NUM_OBSPNT.COL_NUM_OBSPNT_ANTMEASUREMENT:
                        Cmp = string.Compare(grdFlexGrid[i3, (int)Row1].ToString(), grdFlexGrid[i3, (int)Row2].ToString());
                        if (Cmp != 0) { rtn = -1; }
                        break;
                    case (int)COL_NUM_OBSPNT.COL_NUM_OBSPNT_ANTHEIGHT:
                        nCompare = Convert.ToDouble(grdFlexGrid[i3, (int)Row1].ToString()) - Convert.ToDouble(grdFlexGrid[i3, (int)Row2].ToString());
                        Cmp = nCompare < 0 ? -1 : 0;
                        if (Cmp != 0) { rtn = -1; }
                        break;
                    case (int)COL_NUM_OBSPNT.COL_NUM_OBSPNT_ANTMOUNT:
                        nCompare = Convert.ToDouble(grdFlexGrid[i3, (int)Row1].ToString()) - Convert.ToDouble(grdFlexGrid[i3, (int)Row2].ToString());
                        Cmp = nCompare < 0 ? -1 : 0;
                        if (Cmp != 0) { rtn = -1; }
                        break;
                    case (int)COL_NUM_OBSPNT.COL_NUM_OBSPNT_ANTCENTER:
                        nCompare = Convert.ToDouble(grdFlexGrid[i3, (int)Row1].ToString()) - Convert.ToDouble(grdFlexGrid[i3, (int)Row2].ToString());
                        Cmp = nCompare < 0 ? -1 : 0;
                        if (Cmp != 0) { rtn = -1; }
                        break;
                    case (int)COL_NUM_OBSPNT.COL_NUM_OBSPNT_INTERVAL:
                        nCompare = Convert.ToDouble(grdFlexGrid[i3, (int)Row1].ToString()) - Convert.ToDouble(grdFlexGrid[i3, (int)Row2].ToString());
                        Cmp = nCompare < 0 ? -1 : 0;
                        if (Cmp != 0) { rtn = -1; }
                        break;
                    case (int)COL_NUM_OBSPNT.COL_NUM_OBSPNT_ELEVATIONMASK:
                        Cmp = string.Compare(grdFlexGrid[i3, (int)Row1].ToString(), grdFlexGrid[i3, (int)Row2].ToString());
                        if (Cmp != 0) { rtn = -1; }
                        break;
                    case (int)COL_NUM_OBSPNT.COL_NUM_OBSPNT_SATELLITE:
                        nCompare = Convert.ToDouble(grdFlexGrid[i3, (int)Row1].ToString()) - Convert.ToDouble(grdFlexGrid[i3, (int)Row2].ToString());
                        Cmp = nCompare < 0 ? -1 : 0;
                        if (Cmp != 0) { rtn = -1; }
                        break;
                    //'        Case COL_NUM_OBSPNT_COORDLAT
                    //'            Cmp = StrComp(grdFlexGrid.TextMatrix(Row1, clsListSort.Col(i)), grdFlexGrid.TextMatrix(Row2, clsListSort.Col(i)))
                    //'            If Cmp <> 0 Then Exit For
                    //'        Case COL_NUM_OBSPNT_COORDLON
                    //'            Cmp = StrComp(grdFlexGrid.TextMatrix(Row1, clsListSort.Col(i)), grdFlexGrid.TextMatrix(Row2, clsListSort.Col(i)))
                    //'            If Cmp <> 0 Then Exit For
                    //'        Case COL_NUM_OBSPNT_COORDHEIGHT
                    //'            nCompare = Val(grdFlexGrid.TextMatrix(Row1, clsListSort.Col(i))) - Val(grdFlexGrid.TextMatrix(Row2, clsListSort.Col(i)))
                    //'            Cmp = IIf(nCompare < 0, -1, IIf(nCompare > 0, 1, 0))
                    //'            If Cmp <> 0 Then Exit For
                    //'        Case COL_NUM_OBSPNT_COORDALT
                    //'            nCompare = Val(grdFlexGrid.TextMatrix(Row1, clsListSort.Col(i))) - Val(grdFlexGrid.TextMatrix(Row2, clsListSort.Col(i)))
                    //'            Cmp = IIf(nCompare < 0, -1, IIf(nCompare > 0, 1, 0))
                    //'            If Cmp <> 0 Then Exit For
                    //'        Case COL_NUM_OBSPNT_COORDX
                    //'            nCompare = Val(grdFlexGrid.TextMatrix(Row1, clsListSort.Col(i))) - Val(grdFlexGrid.TextMatrix(Row2, clsListSort.Col(i)))
                    //'            Cmp = IIf(nCompare < 0, -1, IIf(nCompare > 0, 1, 0))
                    //'            If Cmp <> 0 Then Exit For
                    //'        Case COL_NUM_OBSPNT_COORDY
                    //'            nCompare = Val(grdFlexGrid.TextMatrix(Row1, clsListSort.Col(i))) - Val(grdFlexGrid.TextMatrix(Row2, clsListSort.Col(i)))
                    //'            Cmp = IIf(nCompare < 0, -1, IIf(nCompare > 0, 1, 0))
                    //'            If Cmp <> 0 Then Exit For
                    //'        Case COL_NUM_OBSPNT_COORDZ
                    //'            nCompare = Val(grdFlexGrid.TextMatrix(Row1, clsListSort.Col(i))) - Val(grdFlexGrid.TextMatrix(Row2, clsListSort.Col(i)))
                    //'            Cmp = IIf(nCompare < 0, -1, IIf(nCompare > 0, 1, 0))
                    //'            If Cmp <> 0 Then Exit For
                    default:
                        break;
                }
            }

            //'昇順降順変換。
            if (i1 < clsListSort.Cols())
            {
                if (!clsListSort.Ascending(i1))
                {
                    Cmp = -Cmp;
                }
            }
            else
            {
                Cmp = 0;
            }
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'ベクトルリストソート比較。
        '
        'Row1 と Row2 で指定される行を比較する。
        'Cmp に比較の結果を設定する。
        'Row1 が Row2 より前の場合 -1 を設定する。
        'Row1 と Row2 が等しい場合 0 を設定する。
        'Row1 が Row2 より後の場合 1 を設定する。
        '比較するカラムの順位は clsListSort に従う。
        '
        '引き数：
        'grdFlexGrid リストコントロール。
        'clsListSort ListSort オブジェクト。
        'Row1 比較する行の番号。
        'Row2 比較する行の番号。
        'Cmp 比較の結果。
        Public Sub CompareSortVector(ByVal grdFlexGrid As NSFlexGrid, ByVal clsListSort As ListSort, ByVal Row1 As Long, ByVal Row2 As Long, Cmp As Integer)

            Dim nCompare As Double
            Dim i As Long
            For i = 0 To clsListSort.Cols - 1
                Select Case clsListSort.Col(i)
                Case COL_NUM_VECTOR_WORK
                    Cmp = StrComp(grdFlexGrid.TextMatrix(Row1, clsListSort.Col(i)), grdFlexGrid.TextMatrix(Row2, clsListSort.Col(i)))
                    If Cmp <> 0 Then Exit For
                Case COL_NUM_VECTOR_ENABLE
                    Cmp = StrComp(grdFlexGrid.TextMatrix(Row1, clsListSort.Col(i)), grdFlexGrid.TextMatrix(Row2, clsListSort.Col(i)))
                    If Cmp <> 0 Then Exit For
                Case COL_NUM_VECTOR_ADOPT
                    Cmp = StrComp(grdFlexGrid.TextMatrix(Row1, clsListSort.Col(i)), grdFlexGrid.TextMatrix(Row2, clsListSort.Col(i)))
                    If Cmp <> 0 Then Exit For
                Case COL_NUM_VECTOR_SESSION
                    Cmp = StrComp(grdFlexGrid.TextMatrix(Row1, clsListSort.Col(i)), grdFlexGrid.TextMatrix(Row2, clsListSort.Col(i)))
                    If Cmp <> 0 Then Exit For
                Case COL_NUM_VECTOR_STRNUMBER
                    Cmp = StrComp(grdFlexGrid.TextMatrix(Row1, clsListSort.Col(i)), grdFlexGrid.TextMatrix(Row2, clsListSort.Col(i)))
                    If Cmp <> 0 Then Exit For
                Case COL_NUM_VECTOR_ENDNUMBER
                    Cmp = StrComp(grdFlexGrid.TextMatrix(Row1, clsListSort.Col(i)), grdFlexGrid.TextMatrix(Row2, clsListSort.Col(i)))
                    If Cmp <> 0 Then Exit For
                Case COL_NUM_VECTOR_STRTIME
                    Cmp = StrComp(grdFlexGrid.TextMatrix(Row1, clsListSort.Col(i)), grdFlexGrid.TextMatrix(Row2, clsListSort.Col(i)))
                    If Cmp <> 0 Then Exit For
                Case COL_NUM_VECTOR_ENDTIME
                    Cmp = StrComp(grdFlexGrid.TextMatrix(Row1, clsListSort.Col(i)), grdFlexGrid.TextMatrix(Row2, clsListSort.Col(i)))
                    If Cmp <> 0 Then Exit For
                Case COL_NUM_VECTOR_ANALYSIS
                    Cmp = StrComp(grdFlexGrid.TextMatrix(Row1, clsListSort.Col(i)), grdFlexGrid.TextMatrix(Row2, clsListSort.Col(i)))
                    If Cmp <> 0 Then Exit For
                Case COL_NUM_VECTOR_FREQUENCY
                    Cmp = StrComp(grdFlexGrid.TextMatrix(Row1, clsListSort.Col(i)), grdFlexGrid.TextMatrix(Row2, clsListSort.Col(i)))
                    If Cmp <> 0 Then Exit For
                Case COL_NUM_VECTOR_LC
                    Cmp = StrComp(grdFlexGrid.TextMatrix(Row1, clsListSort.Col(i)), grdFlexGrid.TextMatrix(Row2, clsListSort.Col(i)))
                    If Cmp <> 0 Then Exit For
        '        Case COL_NUM_VECTOR_SOLVEMODE
        '            Cmp = StrComp(grdFlexGrid.TextMatrix(Row1, clsListSort.Col(i)), grdFlexGrid.TextMatrix(Row2, clsListSort.Col(i)))
        '            If Cmp <> 0 Then Exit For
                Case COL_NUM_VECTOR_ANALYSISSTRTIME
                    Cmp = StrComp(grdFlexGrid.TextMatrix(Row1, clsListSort.Col(i)), grdFlexGrid.TextMatrix(Row2, clsListSort.Col(i)))
                    If Cmp <> 0 Then Exit For
                Case COL_NUM_VECTOR_ANALYSISENDTIME
                    Cmp = StrComp(grdFlexGrid.TextMatrix(Row1, clsListSort.Col(i)), grdFlexGrid.TextMatrix(Row2, clsListSort.Col(i)))
                    If Cmp <> 0 Then Exit For
                Case COL_NUM_VECTOR_DX
                    nCompare = Val(grdFlexGrid.TextMatrix(Row1, clsListSort.Col(i))) - Val(grdFlexGrid.TextMatrix(Row2, clsListSort.Col(i)))
                    Cmp = IIf(nCompare < 0, -1, IIf(nCompare > 0, 1, 0))
                    If Cmp <> 0 Then Exit For
                Case COL_NUM_VECTOR_DY
                    nCompare = Val(grdFlexGrid.TextMatrix(Row1, clsListSort.Col(i))) - Val(grdFlexGrid.TextMatrix(Row2, clsListSort.Col(i)))
                    Cmp = IIf(nCompare < 0, -1, IIf(nCompare > 0, 1, 0))
                    If Cmp <> 0 Then Exit For
                Case COL_NUM_VECTOR_DZ
                    nCompare = Val(grdFlexGrid.TextMatrix(Row1, clsListSort.Col(i))) - Val(grdFlexGrid.TextMatrix(Row2, clsListSort.Col(i)))
                    Cmp = IIf(nCompare < 0, -1, IIf(nCompare > 0, 1, 0))
                    If Cmp <> 0 Then Exit For
                Case COL_NUM_VECTOR_BIAS
                    nCompare = Val(grdFlexGrid.TextMatrix(Row1, clsListSort.Col(i))) - Val(grdFlexGrid.TextMatrix(Row2, clsListSort.Col(i)))
                    Cmp = IIf(nCompare < 0, -1, IIf(nCompare > 0, 1, 0))
                    If Cmp <> 0 Then Exit For
                'Case COL_NUM_VECTOR_AMBPERCENTAGE
                '    nCompare = Val(grdFlexGrid.TextMatrix(Row1, clsListSort.Col(i))) - Val(grdFlexGrid.TextMatrix(Row2, clsListSort.Col(i)))
                '    Cmp = IIf(nCompare < 0, -1, IIf(nCompare > 0, 1, 0))
                '    If Cmp <> 0 Then Exit For
                Case COL_NUM_VECTOR_REJECTRATIO
                    nCompare = Val(grdFlexGrid.TextMatrix(Row1, clsListSort.Col(i))) - Val(grdFlexGrid.TextMatrix(Row2, clsListSort.Col(i)))
                    Cmp = IIf(nCompare < 0, -1, IIf(nCompare > 0, 1, 0))
                    If Cmp <> 0 Then Exit For
                'Case COL_NUM_VECTOR_RMS
                '    nCompare = Val(grdFlexGrid.TextMatrix(Row1, clsListSort.Col(i))) - Val(grdFlexGrid.TextMatrix(Row2, clsListSort.Col(i)))
                '    Cmp = IIf(nCompare < 0, -1, IIf(nCompare > 0, 1, 0))
                '    If Cmp <> 0 Then Exit For
                Case COL_NUM_VECTOR_XX
                    nCompare = Val(grdFlexGrid.TextMatrix(Row1, clsListSort.Col(i))) - Val(grdFlexGrid.TextMatrix(Row2, clsListSort.Col(i)))
                    Cmp = IIf(nCompare < 0, -1, IIf(nCompare > 0, 1, 0))
                    If Cmp <> 0 Then Exit For
                Case COL_NUM_VECTOR_YY
                    nCompare = Val(grdFlexGrid.TextMatrix(Row1, clsListSort.Col(i))) - Val(grdFlexGrid.TextMatrix(Row2, clsListSort.Col(i)))
                    Cmp = IIf(nCompare < 0, -1, IIf(nCompare > 0, 1, 0))
                    If Cmp <> 0 Then Exit For
                Case COL_NUM_VECTOR_ZZ
                    nCompare = Val(grdFlexGrid.TextMatrix(Row1, clsListSort.Col(i))) - Val(grdFlexGrid.TextMatrix(Row2, clsListSort.Col(i)))
                    Cmp = IIf(nCompare < 0, -1, IIf(nCompare > 0, 1, 0))
                    If Cmp <> 0 Then Exit For
                Case COL_NUM_VECTOR_XY
                    nCompare = Val(grdFlexGrid.TextMatrix(Row1, clsListSort.Col(i))) - Val(grdFlexGrid.TextMatrix(Row2, clsListSort.Col(i)))
                    Cmp = IIf(nCompare < 0, -1, IIf(nCompare > 0, 1, 0))
                    If Cmp <> 0 Then Exit For
                Case COL_NUM_VECTOR_XZ
                    nCompare = Val(grdFlexGrid.TextMatrix(Row1, clsListSort.Col(i))) - Val(grdFlexGrid.TextMatrix(Row2, clsListSort.Col(i)))
                    Cmp = IIf(nCompare < 0, -1, IIf(nCompare > 0, 1, 0))
                    If Cmp <> 0 Then Exit For
                Case COL_NUM_VECTOR_YZ
                    nCompare = Val(grdFlexGrid.TextMatrix(Row1, clsListSort.Col(i))) - Val(grdFlexGrid.TextMatrix(Row2, clsListSort.Col(i)))
                    Cmp = IIf(nCompare < 0, -1, IIf(nCompare > 0, 1, 0))
                    If Cmp <> 0 Then Exit For
                Case COL_NUM_VECTOR_CORRECTX
                    nCompare = Val(grdFlexGrid.TextMatrix(Row1, clsListSort.Col(i))) - Val(grdFlexGrid.TextMatrix(Row2, clsListSort.Col(i)))
                    Cmp = IIf(nCompare < 0, -1, IIf(nCompare > 0, 1, 0))
                    If Cmp <> 0 Then Exit For
                Case COL_NUM_VECTOR_CORRECTY
                    nCompare = Val(grdFlexGrid.TextMatrix(Row1, clsListSort.Col(i))) - Val(grdFlexGrid.TextMatrix(Row2, clsListSort.Col(i)))
                    Cmp = IIf(nCompare < 0, -1, IIf(nCompare > 0, 1, 0))
                    If Cmp <> 0 Then Exit For
                Case COL_NUM_VECTOR_CORRECTZ
                    nCompare = Val(grdFlexGrid.TextMatrix(Row1, clsListSort.Col(i))) - Val(grdFlexGrid.TextMatrix(Row2, clsListSort.Col(i)))
                    Cmp = IIf(nCompare < 0, -1, IIf(nCompare > 0, 1, 0))
                    If Cmp <> 0 Then Exit For
                End Select
            Next
    
            '昇順降順変換。
            If i < clsListSort.Cols Then
                If Not clsListSort.Ascending(i) Then Cmp = -Cmp
            Else
                Cmp = 0
            End If
    
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        'ベクトルリストソート比較。
        '
        'Row1 と Row2 で指定される行を比較する。
        'Cmp に比較の結果を設定する。
        'Row1 が Row2 より前の場合 -1 を設定する。
        'Row1 と Row2 が等しい場合 0 を設定する。
        'Row1 が Row2 より後の場合 1 を設定する。
        '比較するカラムの順位は clsListSort に従う。
        '
        '引き数：
        'grdFlexGrid リストコントロール。
        'clsListSort ListSort オブジェクト。
        'Row1 比較する行の番号。
        'Row2 比較する行の番号。
        'Cmp 比較の結果。
        */
        public void CompareSortVector(DataGridView grdFlexGrid, ListSort clsListSort, long Row1, long Row2, ref int Cmp)
        {

            double nCompare;
            int i1;
            int i2 = (int)clsListSort.Cols();
            int i3;
            int rtn = 0;
            for (i1 = 0; i1 < i2; i1++)
            {
                if (rtn != 0)
                {
                    break;
                }
                i3 = (int)clsListSort.Col(i1);
                switch (i3)
                {
                    /*
                    case (int)COL_NUM_VECTOR.COL_NUM_VECTOR_WORK:
                        Cmp = string.Compare(grdFlexGrid[i3, (int)Row1].ToString(), grdFlexGrid[i3, (int)Row2].ToString());
                        if (Cmp != 0) { rtn = -1; }
                        break;
                    */
                    case (int)COL_NUM_VECTOR.COL_NUM_VECTOR_ENABLE:
                        Cmp = string.Compare(grdFlexGrid[i3, (int)Row1].ToString(), grdFlexGrid[i3, (int)Row2].ToString());
                        if (Cmp != 0) { rtn = -1; }
                        break;
                    case (int)COL_NUM_VECTOR.COL_NUM_VECTOR_ADOPT:
                        Cmp = string.Compare(grdFlexGrid[i3, (int)Row1].ToString(), grdFlexGrid[i3, (int)Row2].ToString());
                        if (Cmp != 0) { rtn = -1; }
                        break;
                    case (int)COL_NUM_VECTOR.COL_NUM_VECTOR_SESSION:
                        Cmp = string.Compare(grdFlexGrid[i3, (int)Row1].ToString(), grdFlexGrid[i3, (int)Row2].ToString());
                        if (Cmp != 0) { rtn = -1; }
                        break;
                    case (int)COL_NUM_VECTOR.COL_NUM_VECTOR_STRNUMBER:
                        Cmp = string.Compare(grdFlexGrid[i3, (int)Row1].ToString(), grdFlexGrid[i3, (int)Row2].ToString());
                        if (Cmp != 0) { rtn = -1; }
                        break;
                    case (int)COL_NUM_VECTOR.COL_NUM_VECTOR_ENDNUMBER:
                        Cmp = string.Compare(grdFlexGrid[i3, (int)Row1].ToString(), grdFlexGrid[i3, (int)Row2].ToString());
                        if (Cmp != 0) { rtn = -1; }
                        break;
                    case (int)COL_NUM_VECTOR.COL_NUM_VECTOR_STRTIME:
                        Cmp = string.Compare(grdFlexGrid[i3, (int)Row1].ToString(), grdFlexGrid[i3, (int)Row2].ToString());
                        if (Cmp != 0) { rtn = -1; }
                        break;
                    case (int)COL_NUM_VECTOR.COL_NUM_VECTOR_ENDTIME:
                        Cmp = string.Compare(grdFlexGrid[i3, (int)Row1].ToString(), grdFlexGrid[i3, (int)Row2].ToString());
                        if (Cmp != 0) { rtn = -1; }
                        break;
                    case (int)COL_NUM_VECTOR.COL_NUM_VECTOR_ANALYSIS:
                        Cmp = string.Compare(grdFlexGrid[i3, (int)Row1].ToString(), grdFlexGrid[i3, (int)Row2].ToString());
                        if (Cmp != 0) { rtn = -1; }
                        break;
                    case (int)COL_NUM_VECTOR.COL_NUM_VECTOR_FREQUENCY:
                        Cmp = string.Compare(grdFlexGrid[i3, (int)Row1].ToString(), grdFlexGrid[i3, (int)Row2].ToString());
                        if (Cmp != 0) { rtn = -1; }
                        break;
                    case (int)COL_NUM_VECTOR.COL_NUM_VECTOR_LC:
                        Cmp = string.Compare(grdFlexGrid[i3, (int)Row1].ToString(), grdFlexGrid[i3, (int)Row2].ToString());
                        if (Cmp != 0) { rtn = -1; }
                        break;
                    //' Case COL_NUM_VECTOR_SOLVEMODE
                    //'     Cmp = StrComp(grdFlexGrid.TextMatrix(Row1, clsListSort.Col(i)), grdFlexGrid.TextMatrix(Row2, clsListSort.Col(i)))
                    //'     If Cmp <> 0 Then Exit For
                    case (int)COL_NUM_VECTOR.COL_NUM_VECTOR_ANALYSISSTRTIME:
                        Cmp = string.Compare(grdFlexGrid[i3, (int)Row1].ToString(), grdFlexGrid[i3, (int)Row2].ToString());
                        if (Cmp != 0) { rtn = -1; }
                        break;
                    case (int)COL_NUM_VECTOR.COL_NUM_VECTOR_ANALYSISENDTIME:
                        Cmp = string.Compare(grdFlexGrid[i3, (int)Row1].ToString(), grdFlexGrid[i3, (int)Row2].ToString());
                        if (Cmp != 0) { rtn = -1; }
                        break;
                    case (int)COL_NUM_VECTOR.COL_NUM_VECTOR_DX:
                        nCompare = Convert.ToDouble(grdFlexGrid[i3, (int)Row1].ToString()) - Convert.ToDouble(grdFlexGrid[i3, (int)Row2].ToString());
                        Cmp = nCompare < 0 ? -1 : 0;
                        if (Cmp != 0) { rtn = -1; }
                        break;
                    case (int)COL_NUM_VECTOR.COL_NUM_VECTOR_DY:
                        nCompare = Convert.ToDouble(grdFlexGrid[i3, (int)Row1].ToString()) - Convert.ToDouble(grdFlexGrid[i3, (int)Row2].ToString());
                        Cmp = nCompare < 0 ? -1 : 0;
                        if (Cmp != 0) { rtn = -1; }
                        break;
                    case (int)COL_NUM_VECTOR.COL_NUM_VECTOR_DZ:
                        nCompare = Convert.ToDouble(grdFlexGrid[i3, (int)Row1].ToString()) - Convert.ToDouble(grdFlexGrid[i3, (int)Row2].ToString());
                        Cmp = nCompare < 0 ? -1 : 0;
                        if (Cmp != 0) { rtn = -1; }
                        break;
                    case (int)COL_NUM_VECTOR.COL_NUM_VECTOR_BIAS:
                        nCompare = Convert.ToDouble(grdFlexGrid[i3, (int)Row1].ToString()) - Convert.ToDouble(grdFlexGrid[i3, (int)Row2].ToString());
                        Cmp = nCompare < 0 ? -1 : 0;
                        if (Cmp != 0) { rtn = -1; }
                        break;
                    //'Case COL_NUM_VECTOR_AMBPERCENTAGE
                    //'    nCompare = Val(grdFlexGrid.TextMatrix(Row1, clsListSort.Col(i))) - Val(grdFlexGrid.TextMatrix(Row2, clsListSort.Col(i)))
                    //'    Cmp = IIf(nCompare < 0, -1, IIf(nCompare > 0, 1, 0))
                    //'    If Cmp <> 0 Then Exit For
                    case (int)COL_NUM_VECTOR.COL_NUM_VECTOR_REJECTRATIO:
                        nCompare = Convert.ToDouble(grdFlexGrid[i3, (int)Row1].ToString()) - Convert.ToDouble(grdFlexGrid[i3, (int)Row2].ToString());
                        Cmp = nCompare < 0 ? -1 : 0;
                        if (Cmp != 0) { rtn = -1; }
                        break;
                    //'Case COL_NUM_VECTOR_RMS
                    //'    nCompare = Val(grdFlexGrid.TextMatrix(Row1, clsListSort.Col(i))) - Val(grdFlexGrid.TextMatrix(Row2, clsListSort.Col(i)))
                    //'    Cmp = IIf(nCompare < 0, -1, IIf(nCompare > 0, 1, 0))
                    //'    If Cmp <> 0 Then Exit For
                    case (int)COL_NUM_VECTOR.COL_NUM_VECTOR_XX:
                        nCompare = Convert.ToDouble(grdFlexGrid[i3, (int)Row1].ToString()) - Convert.ToDouble(grdFlexGrid[i3, (int)Row2].ToString());
                        Cmp = nCompare < 0 ? -1 : 0;
                        if (Cmp != 0) { rtn = -1; }
                        break;
                    case (int)COL_NUM_VECTOR.COL_NUM_VECTOR_YY:
                        nCompare = Convert.ToDouble(grdFlexGrid[i3, (int)Row1].ToString()) - Convert.ToDouble(grdFlexGrid[i3, (int)Row2].ToString());
                        Cmp = nCompare < 0 ? -1 : 0;
                        if (Cmp != 0) { rtn = -1; }
                        break;
                    case (int)COL_NUM_VECTOR.COL_NUM_VECTOR_ZZ:
                        nCompare = Convert.ToDouble(grdFlexGrid[i3, (int)Row1].ToString()) - Convert.ToDouble(grdFlexGrid[i3, (int)Row2].ToString());
                        Cmp = nCompare < 0 ? -1 : 0;
                        if (Cmp != 0) { rtn = -1; }
                        break;
                    case (int)COL_NUM_VECTOR.COL_NUM_VECTOR_XY:
                        nCompare = Convert.ToDouble(grdFlexGrid[i3, (int)Row1].ToString()) - Convert.ToDouble(grdFlexGrid[i3, (int)Row2].ToString());
                        Cmp = nCompare < 0 ? -1 : 0;
                        if (Cmp != 0) { rtn = -1; }
                        break;
                    case (int)COL_NUM_VECTOR.COL_NUM_VECTOR_XZ:
                        nCompare = Convert.ToDouble(grdFlexGrid[i3, (int)Row1].ToString()) - Convert.ToDouble(grdFlexGrid[i3, (int)Row2].ToString());
                        Cmp = nCompare < 0 ? -1 : 0;
                        if (Cmp != 0) { rtn = -1; }
                        break;
                    case (int)COL_NUM_VECTOR.COL_NUM_VECTOR_YZ:
                        nCompare = Convert.ToDouble(grdFlexGrid[i3, (int)Row1].ToString()) - Convert.ToDouble(grdFlexGrid[i3, (int)Row2].ToString());
                        Cmp = nCompare < 0 ? -1 : 0;
                        if (Cmp != 0) { rtn = -1; }
                        break;
                    case (int)COL_NUM_VECTOR.COL_NUM_VECTOR_CORRECTX:
                        nCompare = Convert.ToDouble(grdFlexGrid[i3, (int)Row1].ToString()) - Convert.ToDouble(grdFlexGrid[i3, (int)Row2].ToString());
                        Cmp = nCompare < 0 ? -1 : 0;
                        if (Cmp != 0) { rtn = -1; }
                        break;
                    case (int)COL_NUM_VECTOR.COL_NUM_VECTOR_CORRECTY:
                        nCompare = Convert.ToDouble(grdFlexGrid[i3, (int)Row1].ToString()) - Convert.ToDouble(grdFlexGrid[i3, (int)Row2].ToString());
                        Cmp = nCompare < 0 ? -1 : 0;
                        if (Cmp != 0) { rtn = -1; }
                        break;
                    case (int)COL_NUM_VECTOR.COL_NUM_VECTOR_CORRECTZ:
                        nCompare = Convert.ToDouble(grdFlexGrid[i3, (int)Row1].ToString()) - Convert.ToDouble(grdFlexGrid[i3, (int)Row2].ToString());
                        Cmp = nCompare < 0 ? -1 : 0;
                        if (Cmp != 0) { rtn = -1; }
                        break;
                    default:
                        break;
                }
            }

            //'昇順降順変換。
            if (i1 < clsListSort.Cols())
            {
                if (!clsListSort.Ascending(i1))
                {
                    Cmp = -Cmp;
                }
            }
            else
            {
                Cmp = 0;
            }
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'リスト名。
        '
        'nList で指定されるリストの名前を取得する。
        '
        '引き数：
        'nList リスト種別。
        '
        '戻り値：リスト名を返す。
        Public Function LIST_NAM(ByVal nList As Long) As String
            Select Case nList
            Case LIST_NUM_OBSPNT
                LIST_NAM = LIST_NAM_OBSPNT
            Case LIST_NUM_VECTOR
                LIST_NAM = LIST_NAM_VECTOR
            End Select
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        'リスト名。
        '
        'nList で指定されるリストの名前を取得する。
        '
        '引き数：
        'nList リスト種別。
        '
        '戻り値：リスト名を返す。
        */
        public string LIST_NAM(long nList)
        {
            string w_LIST_NAM;
            switch (nList)
            {
                case (long)LIST_NUM_PANE.LIST_NUM_OBSPNT:       //'観測点。
                    w_LIST_NAM = LIST_NAM_OBSPNT;
                    break;
                case (long)LIST_NUM_PANE.LIST_NUM_VECTOR:       //'ベクトル。
                    w_LIST_NAM = LIST_NAM_VECTOR;
                    break;
                case (long)LIST_NUM_PANE.LIST_NUM_ZAHYOU:       //'座標。
                    w_LIST_NAM = null;
                    break;
                default:
                    w_LIST_NAM = null;
                    break;
            }
            return w_LIST_NAM;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'オブジェクト種別。
        '
        'nList で指定されるリストのオブジェクト種別を取得する。
        '
        '引き数：
        'nList リスト種別。
        '
        '戻り値：オブジェクト種別を返す。
        Public Function LIST_OBJ_TYPE(ByVal nList As Long) As Long
            Select Case nList
            Case LIST_NUM_OBSPNT
                LIST_OBJ_TYPE = OBJ_TYPE_OBSERVATIONPOINT
            Case LIST_NUM_VECTOR
                LIST_OBJ_TYPE = OBJ_TYPE_BASELINEVECTOR
            End Select
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        'オブジェクト種別。
        '
        'nList で指定されるリストのオブジェクト種別を取得する。
        '
        '引き数：
        'nList リスト種別。
        '
        '戻り値：オブジェクト種別を返す。
        */
        public long LIST_OBJ_TYPE(long nList)
        {
            long w_LIST_OBJ_TYPE;
            switch (nList)
            {
                case (long)LIST_NUM_PANE.LIST_NUM_OBSPNT:       //'観測点。
                    w_LIST_OBJ_TYPE = OBJ_TYPE_OBSERVATIONPOINT;
                    break;
                case (long)LIST_NUM_PANE.LIST_NUM_VECTOR:       //'ベクトル。
                    w_LIST_OBJ_TYPE = OBJ_TYPE_BASELINEVECTOR;
                    break;
                case (long)LIST_NUM_PANE.LIST_NUM_ZAHYOU:       //'座標。
                    w_LIST_OBJ_TYPE = OBJ_TYPE_OBSERVATIONDISTANCE;
                    break;
                default:
                    w_LIST_OBJ_TYPE = -1;
                    break;
            }
            return w_LIST_OBJ_TYPE;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'オブジェクト種別インデックス。
        '
        'nList で指定されるリストのオブジェクト種別インデックスを取得する。
        '
        '引き数：
        'nList リスト種別。
        '
        '戻り値：オブジェクト種別インデックスを返す。
        Public Function LIST_OBJ_TYPE_INDEX(ByVal nList As Long) As Long
            Select Case nList
            Case LIST_NUM_OBSPNT
                LIST_OBJ_TYPE_INDEX = LIST_OBJ_TYPE_INDEX_OBSERVATIONPOINT
            Case LIST_NUM_VECTOR
                LIST_OBJ_TYPE_INDEX = LIST_OBJ_TYPE_INDEX_BASELINEVECTOR
            End Select
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        'オブジェクト種別インデックス。
        '
        'nList で指定されるリストのオブジェクト種別インデックスを取得する。
        '
        '引き数：
        'nList リスト種別。
        '
        '戻り値：オブジェクト種別インデックスを返す。
        */
        public long LIST_OBJ_TYPE_INDEX(long nList)
        {
            long w_LIST_OBJ_TYPE_INDEX;
            switch (nList)
            {
                case (long)LIST_NUM_PANE.LIST_NUM_OBSPNT:       //'観測点。
                    w_LIST_OBJ_TYPE_INDEX = (long)TagLIST_OBJ_TYPE_INDEX.LIST_OBJ_TYPE_INDEX_OBSERVATIONPOINT;
                    break;
                case (long)LIST_NUM_PANE.LIST_NUM_VECTOR:       //'ベクトル。
                    w_LIST_OBJ_TYPE_INDEX = (long)TagLIST_OBJ_TYPE_INDEX.LIST_OBJ_TYPE_INDEX_BASELINEVECTOR;
                    break;
                case (long)LIST_NUM_PANE.LIST_NUM_ZAHYOU:       //'座標。
                    w_LIST_OBJ_TYPE_INDEX = (long)TagLIST_OBJ_TYPE_INDEX.LIST_OBJ_TYPE_INDEX_ZAHYOU;
                    break;
                default:
                    w_LIST_OBJ_TYPE_INDEX = -1;
                    break;
            }
            return w_LIST_OBJ_TYPE_INDEX;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'リストを作成する。
        '
        'nList で指定されるリストを再作成する。
        'カラムを初期化する。行は空になる。
        '行の内容を設定するのは RemakeList 関係のメソッド。
        '
        '引き数：
        'nList リスト種別。
        'grdFlexGrid リストコントロール。
        Public Sub MakeList(ByVal nList As Long, ByVal grdFlexGrid As NSFlexGrid)
            Select Case nList
            Case LIST_NUM_OBSPNT
                Call MakeListObsPnt(grdFlexGrid)
            Case LIST_NUM_VECTOR
                Call MakeListVector(grdFlexGrid)
            End Select
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
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
        public void MakeList(long nList, DataGridView grdFlexGrid)
        {
            switch (nList)
            {
                case (int)LIST_NUM_PANE.LIST_NUM_OBSPNT:        //'観測点。
                    MakeListObsPnt(grdFlexGrid);
                    break;
                case (int)LIST_NUM_PANE.LIST_NUM_VECTOR:        //'ベクトル。
                    MakeListVector(grdFlexGrid);
                    break;
                case (int)LIST_NUM_PANE.LIST_NUM_ZAHYOU:        //'座標。
                    //MakeListVector(grdFlexGrid);
                    break;
                default:
                    break;
            }
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '観測点リストを作成する。
        '
        '引き数：
        'grdFlexGrid リストコントロール。
        Public Sub MakeListObsPnt(ByVal grdFlexGrid As NSFlexGrid)

            'リストのクリア。
            Dim bRedraw As Boolean
            bRedraw = grdFlexGrid.Redraw
            grdFlexGrid.Redraw = False
            grdFlexGrid.Clear
    
            'カラム名称。
            grdFlexGrid.Cols = COL_NUM_OBSPNT_COUNT
            grdFlexGrid.FixedCols = 1
            grdFlexGrid.TextMatrix(0, COL_NUM_OBSPNT_WORK) = COL_NAM_OBSPNT_WORK
            grdFlexGrid.TextMatrix(0, COL_NUM_OBSPNT_ENABLE) = COL_NAM_OBSPNT_ENABLE
            grdFlexGrid.TextMatrix(0, COL_NUM_OBSPNT_TYPE) = COL_NAM_OBSPNT_TYPE
            grdFlexGrid.TextMatrix(0, COL_NUM_OBSPNT_MODE) = COL_NAM_OBSPNT_MODE
            grdFlexGrid.TextMatrix(0, COL_NUM_OBSPNT_SESSION) = COL_NAM_OBSPNT_SESSION
            grdFlexGrid.TextMatrix(0, COL_NUM_OBSPNT_NUMBER) = COL_NAM_OBSPNT_NUMBER
            grdFlexGrid.TextMatrix(0, COL_NUM_OBSPNT_NAME) = COL_NAM_OBSPNT_NAME
            grdFlexGrid.TextMatrix(0, COL_NUM_OBSPNT_STRTIME) = COL_NAM_OBSPNT_STRTIME
            grdFlexGrid.TextMatrix(0, COL_NUM_OBSPNT_ENDTIME) = COL_NAM_OBSPNT_ENDTIME
            grdFlexGrid.TextMatrix(0, COL_NUM_OBSPNT_RECTYPE) = COL_NAM_OBSPNT_RECTYPE
            grdFlexGrid.TextMatrix(0, COL_NUM_OBSPNT_RECNUMBER) = COL_NAM_OBSPNT_RECNUMBER
            grdFlexGrid.TextMatrix(0, COL_NUM_OBSPNT_ANTTYPE) = COL_NAM_OBSPNT_ANTTYPE
            grdFlexGrid.TextMatrix(0, COL_NUM_OBSPNT_ANTNUMBER) = COL_NAM_OBSPNT_ANTNUMBER
            grdFlexGrid.TextMatrix(0, COL_NUM_OBSPNT_ANTMEASUREMENT) = COL_NAM_OBSPNT_ANTMEASUREMENT
            grdFlexGrid.TextMatrix(0, COL_NUM_OBSPNT_ANTHEIGHT) = COL_NAM_OBSPNT_ANTHEIGHT
            grdFlexGrid.TextMatrix(0, COL_NUM_OBSPNT_ANTMOUNT) = COL_NAM_OBSPNT_ANTMOUNT
            grdFlexGrid.TextMatrix(0, COL_NUM_OBSPNT_ANTCENTER) = COL_NAM_OBSPNT_ANTCENTER
            grdFlexGrid.TextMatrix(0, COL_NUM_OBSPNT_INTERVAL) = COL_NAM_OBSPNT_INTERVAL
            grdFlexGrid.TextMatrix(0, COL_NUM_OBSPNT_ELEVATIONMASK) = COL_NAM_OBSPNT_ELEVATIONMASK
            grdFlexGrid.TextMatrix(0, COL_NUM_OBSPNT_SATELLITE) = COL_NAM_OBSPNT_SATELLITE
        '    grdFlexGrid.TextMatrix(0, COL_NUM_OBSPNT_COORDLAT) = COL_NAM_OBSPNT_COORDLAT
        '    grdFlexGrid.TextMatrix(0, COL_NUM_OBSPNT_COORDLON) = COL_NAM_OBSPNT_COORDLON
        '    grdFlexGrid.TextMatrix(0, COL_NUM_OBSPNT_COORDHEIGHT) = COL_NAM_OBSPNT_COORDHEIGHT
        '    grdFlexGrid.TextMatrix(0, COL_NUM_OBSPNT_COORDALT) = COL_NAM_OBSPNT_COORDALT
        '    grdFlexGrid.TextMatrix(0, COL_NUM_OBSPNT_COORDX) = COL_NAM_OBSPNT_COORDX
        '    grdFlexGrid.TextMatrix(0, COL_NUM_OBSPNT_COORDY) = COL_NAM_OBSPNT_COORDY
        '    grdFlexGrid.TextMatrix(0, COL_NUM_OBSPNT_COORDZ) = COL_NAM_OBSPNT_COORDZ
    
            '行数。
            grdFlexGrid.FixedRows = 1
    
            'カラムアライメント。
            grdFlexGrid.ColAlignment(COL_NUM_OBSPNT_WORK) = flexAlignCenterCenter
            grdFlexGrid.ColAlignment(COL_NUM_OBSPNT_ENABLE) = flexAlignCenterCenter
            grdFlexGrid.ColAlignment(COL_NUM_OBSPNT_TYPE) = flexAlignCenterCenter
            grdFlexGrid.ColAlignment(COL_NUM_OBSPNT_MODE) = flexAlignCenterCenter
            grdFlexGrid.ColAlignment(COL_NUM_OBSPNT_SESSION) = flexAlignLeftCenter
            grdFlexGrid.ColAlignment(COL_NUM_OBSPNT_NUMBER) = flexAlignLeftCenter
            grdFlexGrid.ColAlignment(COL_NUM_OBSPNT_NAME) = flexAlignLeftCenter
            grdFlexGrid.ColAlignment(COL_NUM_OBSPNT_STRTIME) = flexAlignLeftCenter
            grdFlexGrid.ColAlignment(COL_NUM_OBSPNT_ENDTIME) = flexAlignLeftCenter
            grdFlexGrid.ColAlignment(COL_NUM_OBSPNT_RECTYPE) = flexAlignLeftCenter
            grdFlexGrid.ColAlignment(COL_NUM_OBSPNT_RECNUMBER) = flexAlignLeftCenter
            grdFlexGrid.ColAlignment(COL_NUM_OBSPNT_ANTTYPE) = flexAlignLeftCenter
            grdFlexGrid.ColAlignment(COL_NUM_OBSPNT_ANTNUMBER) = flexAlignLeftCenter
            grdFlexGrid.ColAlignment(COL_NUM_OBSPNT_ANTMEASUREMENT) = flexAlignLeftCenter
            grdFlexGrid.ColAlignment(COL_NUM_OBSPNT_ANTHEIGHT) = flexAlignRightCenter
            grdFlexGrid.ColAlignment(COL_NUM_OBSPNT_ANTMOUNT) = flexAlignRightCenter
            grdFlexGrid.ColAlignment(COL_NUM_OBSPNT_ANTCENTER) = flexAlignRightCenter
            grdFlexGrid.ColAlignment(COL_NUM_OBSPNT_INTERVAL) = flexAlignRightCenter
            grdFlexGrid.ColAlignment(COL_NUM_OBSPNT_ELEVATIONMASK) = flexAlignRightCenter
            grdFlexGrid.ColAlignment(COL_NUM_OBSPNT_SATELLITE) = flexAlignRightCenter
        '    grdFlexGrid.ColAlignment(COL_NUM_OBSPNT_COORDLAT) = flexAlignRightCenter
        '    grdFlexGrid.ColAlignment(COL_NUM_OBSPNT_COORDLON) = flexAlignRightCenter
        '    grdFlexGrid.ColAlignment(COL_NUM_OBSPNT_COORDHEIGHT) = flexAlignRightCenter
        '    grdFlexGrid.ColAlignment(COL_NUM_OBSPNT_COORDALT) = flexAlignRightCenter
        '    grdFlexGrid.ColAlignment(COL_NUM_OBSPNT_COORDX) = flexAlignRightCenter
        '    grdFlexGrid.ColAlignment(COL_NUM_OBSPNT_COORDY) = flexAlignRightCenter
        '    grdFlexGrid.ColAlignment(COL_NUM_OBSPNT_COORDZ) = flexAlignRightCenter
            '固定行のアライメント。
            grdFlexGrid.Row = 0
            Dim i As Long
            For i = 0 To COL_NUM_OBSPNT_COUNT - 1
                grdFlexGrid.Col = i
                grdFlexGrid.CellAlignment = flexAlignCenterCenter
            Next
    
            'カラム幅。
            grdFlexGrid.ColWidth(COL_NUM_OBSPNT_WORK) = COL_WID_OBSPNT_WORK
            grdFlexGrid.ColWidth(COL_NUM_OBSPNT_ENABLE) = COL_WID_OBSPNT_ENABLE
            grdFlexGrid.ColWidth(COL_NUM_OBSPNT_TYPE) = COL_WID_OBSPNT_TYPE
            grdFlexGrid.ColWidth(COL_NUM_OBSPNT_MODE) = COL_WID_OBSPNT_MODE
            grdFlexGrid.ColWidth(COL_NUM_OBSPNT_SESSION) = COL_WID_OBSPNT_SESSION
            grdFlexGrid.ColWidth(COL_NUM_OBSPNT_NUMBER) = COL_WID_OBSPNT_NUMBER
            grdFlexGrid.ColWidth(COL_NUM_OBSPNT_NAME) = COL_WID_OBSPNT_NAME
            grdFlexGrid.ColWidth(COL_NUM_OBSPNT_STRTIME) = COL_WID_OBSPNT_STRTIME
            grdFlexGrid.ColWidth(COL_NUM_OBSPNT_ENDTIME) = COL_WID_OBSPNT_ENDTIME
            grdFlexGrid.ColWidth(COL_NUM_OBSPNT_RECTYPE) = COL_WID_OBSPNT_RECTYPE
            grdFlexGrid.ColWidth(COL_NUM_OBSPNT_RECNUMBER) = COL_WID_OBSPNT_RECNUMBER
            grdFlexGrid.ColWidth(COL_NUM_OBSPNT_ANTTYPE) = COL_WID_OBSPNT_ANTTYPE
            grdFlexGrid.ColWidth(COL_NUM_OBSPNT_ANTNUMBER) = COL_WID_OBSPNT_ANTNUMBER
            grdFlexGrid.ColWidth(COL_NUM_OBSPNT_ANTMEASUREMENT) = COL_WID_OBSPNT_ANTMEASUREMENT
            grdFlexGrid.ColWidth(COL_NUM_OBSPNT_ANTHEIGHT) = COL_WID_OBSPNT_ANTHEIGHT
            grdFlexGrid.ColWidth(COL_NUM_OBSPNT_ANTMOUNT) = COL_WID_OBSPNT_ANTMOUNT
            grdFlexGrid.ColWidth(COL_NUM_OBSPNT_ANTCENTER) = COL_WID_OBSPNT_ANTCENTER
            grdFlexGrid.ColWidth(COL_NUM_OBSPNT_INTERVAL) = COL_WID_OBSPNT_INTERVAL
            grdFlexGrid.ColWidth(COL_NUM_OBSPNT_ELEVATIONMASK) = COL_WID_OBSPNT_ELEVATIONMASK
            grdFlexGrid.ColWidth(COL_NUM_OBSPNT_SATELLITE) = COL_WID_OBSPNT_SATELLITE
        '    grdFlexGrid.ColWidth(COL_NUM_OBSPNT_COORDLAT) = COL_WID_OBSPNT_LAT
        '    grdFlexGrid.ColWidth(COL_NUM_OBSPNT_COORDLON) = COL_WID_OBSPNT_LON
        '    grdFlexGrid.ColWidth(COL_NUM_OBSPNT_COORDHEIGHT) = COL_WID_OBSPNT_HEIGHT
        '    grdFlexGrid.ColWidth(COL_NUM_OBSPNT_COORDALT) = COL_WID_OBSPNT_ALT
        '    grdFlexGrid.ColWidth(COL_NUM_OBSPNT_COORDX) = COL_WID_OBSPNT_X
        '    grdFlexGrid.ColWidth(COL_NUM_OBSPNT_COORDY) = COL_WID_OBSPNT_Y
        '    grdFlexGrid.ColWidth(COL_NUM_OBSPNT_COORDZ) = COL_WID_OBSPNT_Z
    
            '表示。
            grdFlexGrid.Redraw = bRedraw
    
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
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
        private void MakeListObsPnt(DataGridView grdFlexGrid)
        {
            //'リストのクリア。
            //bool bRedraw;
            //bRedraw = listPane.dataGridView1.Redraw;
            //listPane.dataGridView1.Redraw = false;
            grdFlexGrid.Columns.Clear();
            grdFlexGrid.Rows.Clear();


            int nWidth;             //    Dim nWidth As Long
            long nTotalWidth;       // Dim nTotalWidth As Lon


            //listPane.dataGridView1.View = View.Details;


            //'カラム名称。
            grdFlexGrid.ColumnCount = (int)COL_NUM_OBSPNT.COL_NUM_OBSPNT_COUNT;
            grdFlexGrid.Columns[0].DividerWidth = 1;
            grdFlexGrid.Columns[(int)COL_NUM_OBSPNT.COL_NUM_OBSPNT_WORK].HeaderText = COL_NAM_OBSPNT_WORK;
            grdFlexGrid.Columns[(int)COL_NUM_OBSPNT.COL_NUM_OBSPNT_ENABLE].HeaderText = COL_NAM_OBSPNT_ENABLE;
            grdFlexGrid.Columns[(int)COL_NUM_OBSPNT.COL_NUM_OBSPNT_TYPE].HeaderText = COL_NAM_OBSPNT_TYPE;
            grdFlexGrid.Columns[(int)COL_NUM_OBSPNT.COL_NUM_OBSPNT_MODE].HeaderText = COL_NAM_OBSPNT_MODE;
            grdFlexGrid.Columns[(int)COL_NUM_OBSPNT.COL_NUM_OBSPNT_SESSION].HeaderText = COL_NAM_OBSPNT_SESSION;
            grdFlexGrid.Columns[(int)COL_NUM_OBSPNT.COL_NUM_OBSPNT_NUMBER].HeaderText = COL_NAM_OBSPNT_NUMBER;
            grdFlexGrid.Columns[(int)COL_NUM_OBSPNT.COL_NUM_OBSPNT_NAME].HeaderText = COL_NAM_OBSPNT_NAME;
            grdFlexGrid.Columns[(int)COL_NUM_OBSPNT.COL_NUM_OBSPNT_STRTIME].HeaderText = COL_NAM_OBSPNT_STRTIME;
            grdFlexGrid.Columns[(int)COL_NUM_OBSPNT.COL_NUM_OBSPNT_ENDTIME].HeaderText = COL_NAM_OBSPNT_ENDTIME;
            grdFlexGrid.Columns[(int)COL_NUM_OBSPNT.COL_NUM_OBSPNT_RECTYPE].HeaderText = COL_NAM_OBSPNT_RECTYPE;
            grdFlexGrid.Columns[(int)COL_NUM_OBSPNT.COL_NUM_OBSPNT_RECNUMBER].HeaderText = COL_NAM_OBSPNT_RECNUMBER;
            grdFlexGrid.Columns[(int)COL_NUM_OBSPNT.COL_NUM_OBSPNT_ANTTYPE].HeaderText = COL_NAM_OBSPNT_ANTTYPE;
            grdFlexGrid.Columns[(int)COL_NUM_OBSPNT.COL_NUM_OBSPNT_ANTNUMBER].HeaderText = COL_NAM_OBSPNT_ANTNUMBER;
            grdFlexGrid.Columns[(int)COL_NUM_OBSPNT.COL_NUM_OBSPNT_ANTMEASUREMENT].HeaderText = COL_NAM_OBSPNT_ANTMEASUREMENT;
            grdFlexGrid.Columns[(int)COL_NUM_OBSPNT.COL_NUM_OBSPNT_ANTHEIGHT].HeaderText = COL_NAM_OBSPNT_ANTHEIGHT;
            grdFlexGrid.Columns[(int)COL_NUM_OBSPNT.COL_NUM_OBSPNT_ANTMOUNT].HeaderText = COL_NAM_OBSPNT_ANTMOUNT;
            grdFlexGrid.Columns[(int)COL_NUM_OBSPNT.COL_NUM_OBSPNT_ANTCENTER].HeaderText = COL_NAM_OBSPNT_ANTCENTER;
            grdFlexGrid.Columns[(int)COL_NUM_OBSPNT.COL_NUM_OBSPNT_INTERVAL].HeaderText = COL_NAM_OBSPNT_INTERVAL;
            grdFlexGrid.Columns[(int)COL_NUM_OBSPNT.COL_NUM_OBSPNT_ELEVATIONMASK].HeaderText = COL_NAM_OBSPNT_ELEVATIONMASK;
            grdFlexGrid.Columns[(int)COL_NUM_OBSPNT.COL_NUM_OBSPNT_SATELLITE].HeaderText = COL_NAM_OBSPNT_SATELLITE;
            //'    grdFlexGrid.TextMatrix(0, COL_NUM_OBSPNT_COORDLAT) = COL_NAM_OBSPNT_COORDLAT
            //'    grdFlexGrid.TextMatrix(0, COL_NUM_OBSPNT_COORDLON) = COL_NAM_OBSPNT_COORDLON
            //'    grdFlexGrid.TextMatrix(0, COL_NUM_OBSPNT_COORDHEIGHT) = COL_NAM_OBSPNT_COORDHEIGHT
            //'    grdFlexGrid.TextMatrix(0, COL_NUM_OBSPNT_COORDALT) = COL_NAM_OBSPNT_COORDALT
            //'    grdFlexGrid.TextMatrix(0, COL_NUM_OBSPNT_COORDX) = COL_NAM_OBSPNT_COORDX
            //'    grdFlexGrid.TextMatrix(0, COL_NUM_OBSPNT_COORDY) = COL_NAM_OBSPNT_COORDY
            //'    grdFlexGrid.TextMatrix(0, COL_NUM_OBSPNT_COORDZ) = COL_NAM_OBSPNT_COORDZ


            //'行数。
            grdFlexGrid.Rows[0].DividerHeight = 1;


            //'カラムアライメント。
            grdFlexGrid.Columns[(int)COL_NUM_OBSPNT.COL_NUM_OBSPNT_WORK].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            grdFlexGrid.Columns[(int)COL_NUM_OBSPNT.COL_NUM_OBSPNT_ENABLE].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            grdFlexGrid.Columns[(int)COL_NUM_OBSPNT.COL_NUM_OBSPNT_TYPE].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            grdFlexGrid.Columns[(int)COL_NUM_OBSPNT.COL_NUM_OBSPNT_MODE].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            grdFlexGrid.Columns[(int)COL_NUM_OBSPNT.COL_NUM_OBSPNT_SESSION].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            grdFlexGrid.Columns[(int)COL_NUM_OBSPNT.COL_NUM_OBSPNT_NUMBER].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            grdFlexGrid.Columns[(int)COL_NUM_OBSPNT.COL_NUM_OBSPNT_NAME].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            grdFlexGrid.Columns[(int)COL_NUM_OBSPNT.COL_NUM_OBSPNT_STRTIME].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            grdFlexGrid.Columns[(int)COL_NUM_OBSPNT.COL_NUM_OBSPNT_ENDTIME].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            grdFlexGrid.Columns[(int)COL_NUM_OBSPNT.COL_NUM_OBSPNT_RECTYPE].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            grdFlexGrid.Columns[(int)COL_NUM_OBSPNT.COL_NUM_OBSPNT_RECNUMBER].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            grdFlexGrid.Columns[(int)COL_NUM_OBSPNT.COL_NUM_OBSPNT_ANTTYPE].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            grdFlexGrid.Columns[(int)COL_NUM_OBSPNT.COL_NUM_OBSPNT_ANTNUMBER].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            grdFlexGrid.Columns[(int)COL_NUM_OBSPNT.COL_NUM_OBSPNT_ANTMEASUREMENT].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            grdFlexGrid.Columns[(int)COL_NUM_OBSPNT.COL_NUM_OBSPNT_ANTHEIGHT].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            grdFlexGrid.Columns[(int)COL_NUM_OBSPNT.COL_NUM_OBSPNT_ANTMOUNT].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            grdFlexGrid.Columns[(int)COL_NUM_OBSPNT.COL_NUM_OBSPNT_ANTCENTER].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            grdFlexGrid.Columns[(int)COL_NUM_OBSPNT.COL_NUM_OBSPNT_INTERVAL].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            grdFlexGrid.Columns[(int)COL_NUM_OBSPNT.COL_NUM_OBSPNT_ELEVATIONMASK].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            grdFlexGrid.Columns[(int)COL_NUM_OBSPNT.COL_NUM_OBSPNT_SATELLITE].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            //'    grdFlexGrid.ColAlignment(COL_NUM_OBSPNT_COORDLAT) = flexAlignRightCenter
            //'    grdFlexGrid.ColAlignment(COL_NUM_OBSPNT_COORDLON) = flexAlignRightCenter
            //'    grdFlexGrid.ColAlignment(COL_NUM_OBSPNT_COORDHEIGHT) = flexAlignRightCenter
            //'    grdFlexGrid.ColAlignment(COL_NUM_OBSPNT_COORDALT) = flexAlignRightCenter
            //'    grdFlexGrid.ColAlignment(COL_NUM_OBSPNT_COORDX) = flexAlignRightCenter
            //'    grdFlexGrid.ColAlignment(COL_NUM_OBSPNT_COORDY) = flexAlignRightCenter
            //'    grdFlexGrid.ColAlignment(COL_NUM_OBSPNT_COORDZ) = flexAlignRightCenter
            /*
            //'固定行のアライメント。
            grdFlexGrid.Row = 0
            Dim i As Long
            For i = 0 To COL_NUM_OBSPNT_COUNT -1
                grdFlexGrid.Col = i
                grdFlexGrid.CellAlignment = flexAlignCenterCenter
            Next
            */

            //'カラム幅。
            grdFlexGrid.Columns[(int)COL_NUM_OBSPNT.COL_NUM_OBSPNT_WORK].Width = (int)COL_WID_OBSPNT_WORK;
            grdFlexGrid.Columns[(int)COL_NUM_OBSPNT.COL_NUM_OBSPNT_ENABLE].Width = (int)COL_WID_OBSPNT_ENABLE;
            grdFlexGrid.Columns[(int)COL_NUM_OBSPNT.COL_NUM_OBSPNT_TYPE].Width = (int)COL_WID_OBSPNT_TYPE;
            grdFlexGrid.Columns[(int)COL_NUM_OBSPNT.COL_NUM_OBSPNT_MODE].Width = (int)COL_WID_OBSPNT_MODE;
            grdFlexGrid.Columns[(int)COL_NUM_OBSPNT.COL_NUM_OBSPNT_SESSION].Width = (int)COL_WID_OBSPNT_SESSION;
            grdFlexGrid.Columns[(int)COL_NUM_OBSPNT.COL_NUM_OBSPNT_NUMBER].Width = (int)COL_WID_OBSPNT_NUMBER;
            grdFlexGrid.Columns[(int)COL_NUM_OBSPNT.COL_NUM_OBSPNT_NAME].Width = (int)COL_WID_OBSPNT_NAME;
            grdFlexGrid.Columns[(int)COL_NUM_OBSPNT.COL_NUM_OBSPNT_STRTIME].Width = (int)COL_WID_OBSPNT_STRTIME;
            grdFlexGrid.Columns[(int)COL_NUM_OBSPNT.COL_NUM_OBSPNT_ENDTIME].Width = (int)COL_WID_OBSPNT_ENDTIME;
            grdFlexGrid.Columns[(int)COL_NUM_OBSPNT.COL_NUM_OBSPNT_RECTYPE].Width = (int)COL_WID_OBSPNT_RECTYPE;
            grdFlexGrid.Columns[(int)COL_NUM_OBSPNT.COL_NUM_OBSPNT_RECNUMBER].Width = (int)COL_WID_OBSPNT_RECNUMBER;
            grdFlexGrid.Columns[(int)COL_NUM_OBSPNT.COL_NUM_OBSPNT_ANTTYPE].Width = (int)COL_WID_OBSPNT_ANTTYPE;
            grdFlexGrid.Columns[(int)COL_NUM_OBSPNT.COL_NUM_OBSPNT_ANTNUMBER].Width = (int)COL_WID_OBSPNT_ANTNUMBER;
            grdFlexGrid.Columns[(int)COL_NUM_OBSPNT.COL_NUM_OBSPNT_ANTMEASUREMENT].Width = (int)COL_WID_OBSPNT_ANTMEASUREMENT;
            grdFlexGrid.Columns[(int)COL_NUM_OBSPNT.COL_NUM_OBSPNT_ANTHEIGHT].Width = (int)COL_WID_OBSPNT_ANTHEIGHT;
            grdFlexGrid.Columns[(int)COL_NUM_OBSPNT.COL_NUM_OBSPNT_ANTMOUNT].Width = (int)COL_WID_OBSPNT_ANTMOUNT;
            grdFlexGrid.Columns[(int)COL_NUM_OBSPNT.COL_NUM_OBSPNT_ANTCENTER].Width = (int)COL_WID_OBSPNT_ANTCENTER;
            grdFlexGrid.Columns[(int)COL_NUM_OBSPNT.COL_NUM_OBSPNT_INTERVAL].Width = (int)COL_WID_OBSPNT_INTERVAL;
            grdFlexGrid.Columns[(int)COL_NUM_OBSPNT.COL_NUM_OBSPNT_ELEVATIONMASK].Width = (int)COL_WID_OBSPNT_ELEVATIONMASK;
            grdFlexGrid.Columns[(int)COL_NUM_OBSPNT.COL_NUM_OBSPNT_SATELLITE].Width = (int)COL_WID_OBSPNT_SATELLITE;
            //'    grdFlexGrid.ColWidth(COL_NUM_OBSPNT_COORDLAT) = COL_WID_OBSPNT_LAT
            //'    grdFlexGrid.ColWidth(COL_NUM_OBSPNT_COORDLON) = COL_WID_OBSPNT_LON
            //'    grdFlexGrid.ColWidth(COL_NUM_OBSPNT_COORDHEIGHT) = COL_WID_OBSPNT_HEIGHT
            //'    grdFlexGrid.ColWidth(COL_NUM_OBSPNT_COORDALT) = COL_WID_OBSPNT_ALT
            //'    grdFlexGrid.ColWidth(COL_NUM_OBSPNT_COORDX) = COL_WID_OBSPNT_X
            //'    grdFlexGrid.ColWidth(COL_NUM_OBSPNT_COORDY) = COL_WID_OBSPNT_Y
            //'    grdFlexGrid.ColWidth(COL_NUM_OBSPNT_COORDZ) = COL_WID_OBSPNT_Z


            //'表示。
            grdFlexGrid.Refresh();


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
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '基線ベクトルリストを作成する。
        '
        '引き数：
        'grdFlexGrid リストコントロール。
        Public Sub MakeListVector(ByVal grdFlexGrid As NSFlexGrid)

            'リストのクリア。
            Dim bRedraw As Boolean
            bRedraw = grdFlexGrid.Redraw
            grdFlexGrid.Redraw = False
            grdFlexGrid.Clear
    
            'カラム名称。
            grdFlexGrid.Cols = COL_NUM_VECTOR_COUNT
            grdFlexGrid.FixedCols = 1
            grdFlexGrid.TextMatrix(0, COL_NUM_VECTOR_WORK) = COL_NAM_VECTOR_WORK
            grdFlexGrid.TextMatrix(0, COL_NUM_VECTOR_ENABLE) = COL_NAM_VECTOR_ENABLE
            grdFlexGrid.TextMatrix(0, COL_NUM_VECTOR_ADOPT) = COL_NAM_VECTOR_ADOPT
            grdFlexGrid.TextMatrix(0, COL_NUM_VECTOR_SESSION) = COL_NAM_VECTOR_SESSION
            grdFlexGrid.TextMatrix(0, COL_NUM_VECTOR_STRNUMBER) = COL_NAM_VECTOR_STRNUMBER
            grdFlexGrid.TextMatrix(0, COL_NUM_VECTOR_ENDNUMBER) = COL_NAM_VECTOR_ENDNUMBER
            grdFlexGrid.TextMatrix(0, COL_NUM_VECTOR_STRTIME) = COL_NAM_VECTOR_STRTIME
            grdFlexGrid.TextMatrix(0, COL_NUM_VECTOR_ENDTIME) = COL_NAM_VECTOR_ENDTIME
            grdFlexGrid.TextMatrix(0, COL_NUM_VECTOR_ANALYSIS) = COL_NAM_VECTOR_ANALYSIS
            grdFlexGrid.TextMatrix(0, COL_NUM_VECTOR_FREQUENCY) = COL_NAM_VECTOR_FREQUENCY
            grdFlexGrid.TextMatrix(0, COL_NUM_VECTOR_LC) = COL_NAM_VECTOR_LC
            'grdFlexGrid.TextMatrix(0, COL_NUM_VECTOR_SOLVEMODE) = COL_NAM_VECTOR_SOLVEMODE
            grdFlexGrid.TextMatrix(0, COL_NUM_VECTOR_ANALYSISSTRTIME) = COL_NAM_VECTOR_ANALYSISSTRTIME
            grdFlexGrid.TextMatrix(0, COL_NUM_VECTOR_ANALYSISENDTIME) = COL_NAM_VECTOR_ANALYSISENDTIME
            grdFlexGrid.TextMatrix(0, COL_NUM_VECTOR_DX) = COL_NAM_VECTOR_DX
            grdFlexGrid.TextMatrix(0, COL_NUM_VECTOR_DY) = COL_NAM_VECTOR_DY
            grdFlexGrid.TextMatrix(0, COL_NUM_VECTOR_DZ) = COL_NAM_VECTOR_DZ
            grdFlexGrid.TextMatrix(0, COL_NUM_VECTOR_BIAS) = COL_NAM_VECTOR_BIAS
            'grdFlexGrid.TextMatrix(0, COL_NUM_VECTOR_AMBPERCENTAGE) = COL_NAM_VECTOR_AMBPERCENTAGE
            grdFlexGrid.TextMatrix(0, COL_NUM_VECTOR_REJECTRATIO) = COL_NAM_VECTOR_REJECTRATIO
            'grdFlexGrid.TextMatrix(0, COL_NUM_VECTOR_RMS) = COL_NAM_VECTOR_RMS
            grdFlexGrid.TextMatrix(0, COL_NUM_VECTOR_XX) = COL_NAM_VECTOR_XX
            grdFlexGrid.TextMatrix(0, COL_NUM_VECTOR_YY) = COL_NAM_VECTOR_YY
            grdFlexGrid.TextMatrix(0, COL_NUM_VECTOR_ZZ) = COL_NAM_VECTOR_ZZ
            grdFlexGrid.TextMatrix(0, COL_NUM_VECTOR_XY) = COL_NAM_VECTOR_XY
            grdFlexGrid.TextMatrix(0, COL_NUM_VECTOR_XZ) = COL_NAM_VECTOR_XZ
            grdFlexGrid.TextMatrix(0, COL_NUM_VECTOR_YZ) = COL_NAM_VECTOR_YZ
            grdFlexGrid.TextMatrix(0, COL_NUM_VECTOR_CORRECTX) = COL_NAM_VECTOR_CORRECTX
            grdFlexGrid.TextMatrix(0, COL_NUM_VECTOR_CORRECTY) = COL_NAM_VECTOR_CORRECTY
            grdFlexGrid.TextMatrix(0, COL_NUM_VECTOR_CORRECTZ) = COL_NAM_VECTOR_CORRECTZ
    
            '行数。
            grdFlexGrid.FixedRows = 1
    
            'カラムアライメント。
            grdFlexGrid.ColAlignment(COL_NUM_VECTOR_WORK) = flexAlignCenterCenter
            grdFlexGrid.ColAlignment(COL_NUM_VECTOR_ENABLE) = flexAlignCenterCenter
            grdFlexGrid.ColAlignment(COL_NUM_VECTOR_ADOPT) = flexAlignCenterCenter
            grdFlexGrid.ColAlignment(COL_NUM_VECTOR_SESSION) = flexAlignLeftCenter
            grdFlexGrid.ColAlignment(COL_NUM_VECTOR_STRNUMBER) = flexAlignLeftCenter
            grdFlexGrid.ColAlignment(COL_NUM_VECTOR_ENDNUMBER) = flexAlignLeftCenter
            grdFlexGrid.ColAlignment(COL_NUM_VECTOR_STRTIME) = flexAlignLeftCenter
            grdFlexGrid.ColAlignment(COL_NUM_VECTOR_ENDTIME) = flexAlignLeftCenter
            grdFlexGrid.ColAlignment(COL_NUM_VECTOR_ANALYSIS) = flexAlignCenterCenter
            grdFlexGrid.ColAlignment(COL_NUM_VECTOR_FREQUENCY) = flexAlignCenterCenter
            grdFlexGrid.ColAlignment(COL_NUM_VECTOR_LC) = flexAlignCenterCenter
            'grdFlexGrid.ColAlignment(COL_NUM_VECTOR_SOLVEMODE) = flexAlignCenterCenter
            grdFlexGrid.ColAlignment(COL_NUM_VECTOR_ANALYSISSTRTIME) = flexAlignLeftCenter
            grdFlexGrid.ColAlignment(COL_NUM_VECTOR_ANALYSISENDTIME) = flexAlignLeftCenter
            grdFlexGrid.ColAlignment(COL_NUM_VECTOR_DX) = flexAlignRightCenter
            grdFlexGrid.ColAlignment(COL_NUM_VECTOR_DY) = flexAlignRightCenter
            grdFlexGrid.ColAlignment(COL_NUM_VECTOR_DZ) = flexAlignRightCenter
            grdFlexGrid.ColAlignment(COL_NUM_VECTOR_BIAS) = flexAlignRightCenter
            'grdFlexGrid.ColAlignment(COL_NUM_VECTOR_AMBPERCENTAGE) = flexAlignRightCenter
            grdFlexGrid.ColAlignment(COL_NUM_VECTOR_REJECTRATIO) = flexAlignRightCenter
            'grdFlexGrid.ColAlignment(COL_NUM_VECTOR_RMS) = flexAlignRightCenter
            grdFlexGrid.ColAlignment(COL_NUM_VECTOR_XX) = flexAlignRightCenter
            grdFlexGrid.ColAlignment(COL_NUM_VECTOR_YY) = flexAlignRightCenter
            grdFlexGrid.ColAlignment(COL_NUM_VECTOR_ZZ) = flexAlignRightCenter
            grdFlexGrid.ColAlignment(COL_NUM_VECTOR_XY) = flexAlignRightCenter
            grdFlexGrid.ColAlignment(COL_NUM_VECTOR_XZ) = flexAlignRightCenter
            grdFlexGrid.ColAlignment(COL_NUM_VECTOR_YZ) = flexAlignRightCenter
            grdFlexGrid.ColAlignment(COL_NUM_VECTOR_CORRECTX) = flexAlignRightCenter
            grdFlexGrid.ColAlignment(COL_NUM_VECTOR_CORRECTY) = flexAlignRightCenter
            grdFlexGrid.ColAlignment(COL_NUM_VECTOR_CORRECTZ) = flexAlignRightCenter
            '固定行のアライメント。
            grdFlexGrid.Row = 0
            Dim i As Long
            For i = 0 To COL_NUM_VECTOR_COUNT - 1
                grdFlexGrid.Col = i
                grdFlexGrid.CellAlignment = flexAlignCenterCenter
            Next
    
            'カラム幅。
            grdFlexGrid.ColWidth(COL_NUM_VECTOR_WORK) = COL_WID_VECTOR_WORK
            grdFlexGrid.ColWidth(COL_NUM_VECTOR_ENABLE) = COL_WID_VECTOR_ENABLE
            grdFlexGrid.ColWidth(COL_NUM_VECTOR_ADOPT) = COL_WID_VECTOR_ADOPT
            grdFlexGrid.ColWidth(COL_NUM_VECTOR_SESSION) = COL_WID_VECTOR_SESSION
            grdFlexGrid.ColWidth(COL_NUM_VECTOR_STRNUMBER) = COL_WID_VECTOR_STRNUMBER
            grdFlexGrid.ColWidth(COL_NUM_VECTOR_ENDNUMBER) = COL_WID_VECTOR_ENDNUMBER
            grdFlexGrid.ColWidth(COL_NUM_VECTOR_STRTIME) = COL_WID_VECTOR_STRTIME
            grdFlexGrid.ColWidth(COL_NUM_VECTOR_ENDTIME) = COL_WID_VECTOR_ENDTIME
            grdFlexGrid.ColWidth(COL_NUM_VECTOR_ANALYSIS) = COL_WID_VECTOR_ANALYSIS
            grdFlexGrid.ColWidth(COL_NUM_VECTOR_FREQUENCY) = COL_WID_VECTOR_FREQUENCY
            grdFlexGrid.ColWidth(COL_NUM_VECTOR_LC) = COL_WID_VECTOR_LC
            'grdFlexGrid.ColWidth(COL_NUM_VECTOR_SOLVEMODE) = COL_WID_VECTOR_SOLVEMODE
            grdFlexGrid.ColWidth(COL_NUM_VECTOR_ANALYSISSTRTIME) = COL_WID_VECTOR_ANALYSISSTRTIME
            grdFlexGrid.ColWidth(COL_NUM_VECTOR_ANALYSISENDTIME) = COL_WID_VECTOR_ANALYSISENDTIME
            grdFlexGrid.ColWidth(COL_NUM_VECTOR_DX) = COL_WID_VECTOR_DX
            grdFlexGrid.ColWidth(COL_NUM_VECTOR_DY) = COL_WID_VECTOR_DY
            grdFlexGrid.ColWidth(COL_NUM_VECTOR_DZ) = COL_WID_VECTOR_DZ
            grdFlexGrid.ColWidth(COL_NUM_VECTOR_BIAS) = COL_WID_VECTOR_BIAS
            'grdFlexGrid.ColWidth(COL_NUM_VECTOR_AMBPERCENTAGE) = COL_WID_VECTOR_AMBPERCENTAGE
            grdFlexGrid.ColWidth(COL_NUM_VECTOR_REJECTRATIO) = COL_WID_VECTOR_REJECTRATIO
            'grdFlexGrid.ColWidth(COL_NUM_VECTOR_RMS) = COL_WID_VECTOR_RMS
            grdFlexGrid.ColWidth(COL_NUM_VECTOR_XX) = COL_WID_VECTOR_XX
            grdFlexGrid.ColWidth(COL_NUM_VECTOR_YY) = COL_WID_VECTOR_YY
            grdFlexGrid.ColWidth(COL_NUM_VECTOR_ZZ) = COL_WID_VECTOR_ZZ
            grdFlexGrid.ColWidth(COL_NUM_VECTOR_XY) = COL_WID_VECTOR_XY
            grdFlexGrid.ColWidth(COL_NUM_VECTOR_XZ) = COL_WID_VECTOR_XZ
            grdFlexGrid.ColWidth(COL_NUM_VECTOR_YZ) = COL_WID_VECTOR_YZ
            grdFlexGrid.ColWidth(COL_NUM_VECTOR_CORRECTX) = COL_WID_VECTOR_CORRECTX
            grdFlexGrid.ColWidth(COL_NUM_VECTOR_CORRECTY) = COL_WID_VECTOR_CORRECTY
            grdFlexGrid.ColWidth(COL_NUM_VECTOR_CORRECTZ) = COL_WID_VECTOR_CORRECTZ
    
            '表示。
            grdFlexGrid.Redraw = bRedraw
    
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
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
        private void MakeListVector(DataGridView grdFlexGrid)
        {
            //'リストのクリア。
            //bool bRedraw;
            //bRedraw = listPane.dataGridView3.Redraw;
            //listPane.dataGridView3.Redraw = false;
            grdFlexGrid.Columns.Clear();
            grdFlexGrid.Rows.Clear();


            //'カラム名称。
            grdFlexGrid.ColumnCount = (int)COL_NUM_VECTOR.COL_NUM_VECTOR_COUNT;
            grdFlexGrid.Columns[0].DividerWidth = 1;
            //grdFlexGrid.Columns[(int)COL_NUM_VECTOR.COL_NUM_VECTOR_WORK].HeaderText = COL_NAM_VECTOR_WORK;
            grdFlexGrid.Columns[(int)COL_NUM_VECTOR.COL_NUM_VECTOR_ENABLE].HeaderText = COL_NAM_VECTOR_ENABLE;
            grdFlexGrid.Columns[(int)COL_NUM_VECTOR.COL_NUM_VECTOR_ADOPT].HeaderText = COL_NAM_VECTOR_ADOPT;
            grdFlexGrid.Columns[(int)COL_NUM_VECTOR.COL_NUM_VECTOR_SESSION].HeaderText = COL_NAM_VECTOR_SESSION;
            grdFlexGrid.Columns[(int)COL_NUM_VECTOR.COL_NUM_VECTOR_STRNUMBER].HeaderText = COL_NAM_VECTOR_STRNUMBER;
            grdFlexGrid.Columns[(int)COL_NUM_VECTOR.COL_NUM_VECTOR_ENDNUMBER].HeaderText = COL_NAM_VECTOR_ENDNUMBER;
            grdFlexGrid.Columns[(int)COL_NUM_VECTOR.COL_NUM_VECTOR_STRTIME].HeaderText = COL_NAM_VECTOR_STRTIME;
            grdFlexGrid.Columns[(int)COL_NUM_VECTOR.COL_NUM_VECTOR_ENDTIME].HeaderText = COL_NAM_VECTOR_ENDTIME;
            grdFlexGrid.Columns[(int)COL_NUM_VECTOR.COL_NUM_VECTOR_ANALYSIS].HeaderText = COL_NAM_VECTOR_ANALYSIS;
            grdFlexGrid.Columns[(int)COL_NUM_VECTOR.COL_NUM_VECTOR_FREQUENCY].HeaderText = COL_NAM_VECTOR_FREQUENCY;
            grdFlexGrid.Columns[(int)COL_NUM_VECTOR.COL_NUM_VECTOR_LC].HeaderText = COL_NAM_VECTOR_LC;
            //'grdFlexGrid.TextMatrix(0, COL_NUM_VECTOR_SOLVEMODE) = COL_NAM_VECTOR_SOLVEMODE
            grdFlexGrid.Columns[(int)COL_NUM_VECTOR.COL_NUM_VECTOR_ANALYSISSTRTIME].HeaderText = COL_NAM_VECTOR_ANALYSISSTRTIME;
            grdFlexGrid.Columns[(int)COL_NUM_VECTOR.COL_NUM_VECTOR_ANALYSISENDTIME].HeaderText = COL_NAM_VECTOR_ANALYSISENDTIME;
            grdFlexGrid.Columns[(int)COL_NUM_VECTOR.COL_NUM_VECTOR_DX].HeaderText = COL_NAM_VECTOR_DX;
            grdFlexGrid.Columns[(int)COL_NUM_VECTOR.COL_NUM_VECTOR_DY].HeaderText = COL_NAM_VECTOR_DY;
            grdFlexGrid.Columns[(int)COL_NUM_VECTOR.COL_NUM_VECTOR_DZ].HeaderText = COL_NAM_VECTOR_DZ;
            grdFlexGrid.Columns[(int)COL_NUM_VECTOR.COL_NUM_VECTOR_BIAS].HeaderText = COL_NAM_VECTOR_BIAS;
            //'grdFlexGrid.TextMatrix(0, COL_NUM_VECTOR_AMBPERCENTAGE) = COL_NAM_VECTOR_AMBPERCENTAGE
            grdFlexGrid.Columns[(int)COL_NUM_VECTOR.COL_NUM_VECTOR_REJECTRATIO].HeaderText = COL_NAM_VECTOR_REJECTRATIO;
            //'grdFlexGrid.TextMatrix(0, COL_NUM_VECTOR_RMS) = COL_NAM_VECTOR_RMS
            grdFlexGrid.Columns[(int)COL_NUM_VECTOR.COL_NUM_VECTOR_XX].HeaderText = COL_NAM_VECTOR_XX;
            grdFlexGrid.Columns[(int)COL_NUM_VECTOR.COL_NUM_VECTOR_YY].HeaderText = COL_NAM_VECTOR_YY;
            grdFlexGrid.Columns[(int)COL_NUM_VECTOR.COL_NUM_VECTOR_ZZ].HeaderText = COL_NAM_VECTOR_ZZ;
            grdFlexGrid.Columns[(int)COL_NUM_VECTOR.COL_NUM_VECTOR_XY].HeaderText = COL_NAM_VECTOR_XY;
            grdFlexGrid.Columns[(int)COL_NUM_VECTOR.COL_NUM_VECTOR_XZ].HeaderText = COL_NAM_VECTOR_XZ;
            grdFlexGrid.Columns[(int)COL_NUM_VECTOR.COL_NUM_VECTOR_YZ].HeaderText = COL_NAM_VECTOR_YZ;
            grdFlexGrid.Columns[(int)COL_NUM_VECTOR.COL_NUM_VECTOR_CORRECTX].HeaderText = COL_NAM_VECTOR_CORRECTX;
            grdFlexGrid.Columns[(int)COL_NUM_VECTOR.COL_NUM_VECTOR_CORRECTY].HeaderText = COL_NAM_VECTOR_CORRECTY;
            grdFlexGrid.Columns[(int)COL_NUM_VECTOR.COL_NUM_VECTOR_CORRECTZ].HeaderText = COL_NAM_VECTOR_CORRECTZ;

            //'行数。
            grdFlexGrid.Rows[0].DividerHeight = 1;


            //'カラムアライメント。
            grdFlexGrid.Columns[(int)COL_NUM_VECTOR.COL_NUM_VECTOR_WORK].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            grdFlexGrid.Columns[(int)COL_NUM_VECTOR.COL_NUM_VECTOR_ENABLE].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            grdFlexGrid.Columns[(int)COL_NUM_VECTOR.COL_NUM_VECTOR_ADOPT].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            grdFlexGrid.Columns[(int)COL_NUM_VECTOR.COL_NUM_VECTOR_SESSION].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            grdFlexGrid.Columns[(int)COL_NUM_VECTOR.COL_NUM_VECTOR_STRNUMBER].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            grdFlexGrid.Columns[(int)COL_NUM_VECTOR.COL_NUM_VECTOR_ENDNUMBER].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            grdFlexGrid.Columns[(int)COL_NUM_VECTOR.COL_NUM_VECTOR_STRTIME].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            grdFlexGrid.Columns[(int)COL_NUM_VECTOR.COL_NUM_VECTOR_ENDTIME].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            grdFlexGrid.Columns[(int)COL_NUM_VECTOR.COL_NUM_VECTOR_ANALYSIS].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            grdFlexGrid.Columns[(int)COL_NUM_VECTOR.COL_NUM_VECTOR_FREQUENCY].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            grdFlexGrid.Columns[(int)COL_NUM_VECTOR.COL_NUM_VECTOR_LC].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //'grdFlexGrid.ColAlignment(COL_NUM_VECTOR_SOLVEMODE) = flexAlignCenterCenter
            grdFlexGrid.Columns[(int)COL_NUM_VECTOR.COL_NUM_VECTOR_ANALYSISSTRTIME].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            grdFlexGrid.Columns[(int)COL_NUM_VECTOR.COL_NUM_VECTOR_ANALYSISENDTIME].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            grdFlexGrid.Columns[(int)COL_NUM_VECTOR.COL_NUM_VECTOR_DX].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            grdFlexGrid.Columns[(int)COL_NUM_VECTOR.COL_NUM_VECTOR_DY].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            grdFlexGrid.Columns[(int)COL_NUM_VECTOR.COL_NUM_VECTOR_DZ].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            grdFlexGrid.Columns[(int)COL_NUM_VECTOR.COL_NUM_VECTOR_BIAS].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            //'grdFlexGrid.ColAlignment(COL_NUM_VECTOR_AMBPERCENTAGE) = flexAlignRightCenter
            grdFlexGrid.Columns[(int)COL_NUM_VECTOR.COL_NUM_VECTOR_REJECTRATIO].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            //'grdFlexGrid.ColAlignment(COL_NUM_VECTOR_RMS) = flexAlignRightCenter
            grdFlexGrid.Columns[(int)COL_NUM_VECTOR.COL_NUM_VECTOR_XX].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            grdFlexGrid.Columns[(int)COL_NUM_VECTOR.COL_NUM_VECTOR_YY].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            grdFlexGrid.Columns[(int)COL_NUM_VECTOR.COL_NUM_VECTOR_ZZ].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            grdFlexGrid.Columns[(int)COL_NUM_VECTOR.COL_NUM_VECTOR_XY].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            grdFlexGrid.Columns[(int)COL_NUM_VECTOR.COL_NUM_VECTOR_XZ].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            grdFlexGrid.Columns[(int)COL_NUM_VECTOR.COL_NUM_VECTOR_YZ].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            grdFlexGrid.Columns[(int)COL_NUM_VECTOR.COL_NUM_VECTOR_CORRECTX].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            grdFlexGrid.Columns[(int)COL_NUM_VECTOR.COL_NUM_VECTOR_CORRECTY].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            grdFlexGrid.Columns[(int)COL_NUM_VECTOR.COL_NUM_VECTOR_CORRECTZ].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            /*
            '固定行のアライメント。
            grdFlexGrid.Row = 0
            Dim i As Long
            For i = 0 To COL_NUM_VECTOR_COUNT -1
                grdFlexGrid.Col = i
                grdFlexGrid.CellAlignment = flexAlignCenterCenter
            Next
            */

            //'カラム幅。
            grdFlexGrid.Columns[(int)COL_NUM_VECTOR.COL_NUM_VECTOR_WORK].Width = (int)COL_WID_VECTOR_WORK;
            grdFlexGrid.Columns[(int)COL_NUM_VECTOR.COL_NUM_VECTOR_ENABLE].Width = (int)COL_WID_VECTOR_ENABLE;
            grdFlexGrid.Columns[(int)COL_NUM_VECTOR.COL_NUM_VECTOR_ADOPT].Width = (int)COL_WID_VECTOR_ADOPT;
            grdFlexGrid.Columns[(int)COL_NUM_VECTOR.COL_NUM_VECTOR_SESSION].Width = (int)COL_WID_VECTOR_SESSION;
            grdFlexGrid.Columns[(int)COL_NUM_VECTOR.COL_NUM_VECTOR_STRNUMBER].Width = (int)COL_WID_VECTOR_STRNUMBER;
            grdFlexGrid.Columns[(int)COL_NUM_VECTOR.COL_NUM_VECTOR_ENDNUMBER].Width = (int)COL_WID_VECTOR_ENDNUMBER;
            grdFlexGrid.Columns[(int)COL_NUM_VECTOR.COL_NUM_VECTOR_STRTIME].Width = (int)COL_WID_VECTOR_STRTIME;
            grdFlexGrid.Columns[(int)COL_NUM_VECTOR.COL_NUM_VECTOR_ENDTIME].Width = (int)COL_WID_VECTOR_ENDTIME;
            grdFlexGrid.Columns[(int)COL_NUM_VECTOR.COL_NUM_VECTOR_ANALYSIS].Width = (int)COL_WID_VECTOR_ANALYSIS;
            grdFlexGrid.Columns[(int)COL_NUM_VECTOR.COL_NUM_VECTOR_FREQUENCY].Width = (int)COL_WID_VECTOR_FREQUENCY;
            grdFlexGrid.Columns[(int)COL_NUM_VECTOR.COL_NUM_VECTOR_LC].Width = (int)COL_WID_VECTOR_LC;
            //'grdFlexGrid.ColWidth(COL_NUM_VECTOR_SOLVEMODE) = COL_WID_VECTOR_SOLVEMODE
            grdFlexGrid.Columns[(int)COL_NUM_VECTOR.COL_NUM_VECTOR_ANALYSISSTRTIME].Width = (int)COL_WID_VECTOR_ANALYSISSTRTIME;
            grdFlexGrid.Columns[(int)COL_NUM_VECTOR.COL_NUM_VECTOR_ANALYSISENDTIME].Width = (int)COL_WID_VECTOR_ANALYSISENDTIME;
            grdFlexGrid.Columns[(int)COL_NUM_VECTOR.COL_NUM_VECTOR_DX].Width = (int)COL_WID_VECTOR_DX;
            grdFlexGrid.Columns[(int)COL_NUM_VECTOR.COL_NUM_VECTOR_DY].Width = (int)COL_WID_VECTOR_DY;
            grdFlexGrid.Columns[(int)COL_NUM_VECTOR.COL_NUM_VECTOR_DZ].Width = (int)COL_WID_VECTOR_DZ;
            grdFlexGrid.Columns[(int)COL_NUM_VECTOR.COL_NUM_VECTOR_BIAS].Width = (int)COL_WID_VECTOR_BIAS;
            //'grdFlexGrid.ColWidth(COL_NUM_VECTOR_AMBPERCENTAGE) = COL_WID_VECTOR_AMBPERCENTAGE
            grdFlexGrid.Columns[(int)COL_NUM_VECTOR.COL_NUM_VECTOR_REJECTRATIO].Width = (int)COL_WID_VECTOR_REJECTRATIO;
            //'grdFlexGrid.ColWidth(COL_NUM_VECTOR_RMS) = COL_WID_VECTOR_RMS
            grdFlexGrid.Columns[(int)COL_NUM_VECTOR.COL_NUM_VECTOR_XX].Width = (int)COL_WID_VECTOR_XX;
            grdFlexGrid.Columns[(int)COL_NUM_VECTOR.COL_NUM_VECTOR_YY].Width = (int)COL_WID_VECTOR_YY;
            grdFlexGrid.Columns[(int)COL_NUM_VECTOR.COL_NUM_VECTOR_ZZ].Width = (int)COL_WID_VECTOR_ZZ;
            grdFlexGrid.Columns[(int)COL_NUM_VECTOR.COL_NUM_VECTOR_XY].Width = (int)COL_WID_VECTOR_XY;
            grdFlexGrid.Columns[(int)COL_NUM_VECTOR.COL_NUM_VECTOR_XZ].Width = (int)COL_WID_VECTOR_XZ;
            grdFlexGrid.Columns[(int)COL_NUM_VECTOR.COL_NUM_VECTOR_YZ].Width = (int)COL_WID_VECTOR_YZ;
            grdFlexGrid.Columns[(int)COL_NUM_VECTOR.COL_NUM_VECTOR_CORRECTX].Width = (int)COL_WID_VECTOR_CORRECTX;
            grdFlexGrid.Columns[(int)COL_NUM_VECTOR.COL_NUM_VECTOR_CORRECTY].Width = (int)COL_WID_VECTOR_CORRECTY;
            grdFlexGrid.Columns[(int)COL_NUM_VECTOR.COL_NUM_VECTOR_CORRECTZ].Width = (int)COL_WID_VECTOR_CORRECTZ;

            //'表示。
            grdFlexGrid.Refresh();
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'リストの指定された行を更新する。
        '
        'nList で指定されるリストの、nRow で指定される行の内容を、objElement で指定されるオブジェクトの属性に書き換える。
        '
        '引き数：
        'nList リスト種別。
        'grdFlexGrid リストコントロール。
        'nRow 対象とする行の番号。
        'objElement 対象とするオブジェクト。
        Public Sub UpdateRowList(ByVal nList As Long, ByVal grdFlexGrid As NSFlexGrid, ByVal nRow As Long, ByVal objElement As Object)
            If nList = LIST_NUM_OBSPNT Then
                Call UpdateRowObsPnt(grdFlexGrid, nRow, objElement)
            Else
                Call UpdateRowVector(grdFlexGrid, nRow, objElement)
            End If
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        'リストの指定された行を更新する。
        '
        'nList で指定されるリストの、nRow で指定される行の内容を、objElement で指定されるオブジェクトの属性に書き換える。
        '
        '引き数：
        'nList リスト種別。
        'grdFlexGrid リストコントロール。
        'nRow 対象とする行の番号。
        'objElement 対象とするオブジェクト。
        */
        public void UpdateRowList(long nList, DataGridView grdFlexGrid, long nRow, object objElement)
        {
            if (nList == (long)LIST_NUM_PANE.LIST_NUM_OBSPNT)
            {
                //'観測点。
                UpdateRowObsPnt(grdFlexGrid, nRow, (ObservationPoint)objElement);
            }
            else if (nList == (long)LIST_NUM_PANE.LIST_NUM_VECTOR)
            {
                //'ベクトル。
                UpdateRowVector(grdFlexGrid, nRow, (BaseLineVector)objElement);
            }
            else if (nList == (long)LIST_NUM_PANE.LIST_NUM_ZAHYOU)
            {
                //'座標。
                //UpdateRowZahyou(grdFlexGrid, nRow, (BaseLineVector)objElement);
            }

        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '観測点リストの指定された行を更新する。
        '
        'nRow で指定される行の内容を、clsObservationPoint で指定されるオブジェクトの属性に書き換える。
        '
        '引き数：
        'grdFlexGrid リストコントロール。
        'nRow 対象とする行の番号。
        'clsObservationPoint 対象とするオブジェクト。
        Public Sub UpdateRowObsPnt(ByVal grdFlexGrid As NSFlexGrid, ByVal nRow As Long, ByVal clsObservationPoint As ObservationPoint)
            grdFlexGrid.TextMatrix(nRow, COL_NUM_OBSPNT_ENABLE) = IIf(clsObservationPoint.Enable, "〆", "")
            grdFlexGrid.TextMatrix(nRow, COL_NUM_OBSPNT_TYPE) = IIf(clsObservationPoint.Fixed, "固定", IIf(clsObservationPoint.Genuine, "偏心", ""))
            grdFlexGrid.TextMatrix(nRow, COL_NUM_OBSPNT_NUMBER) = clsObservationPoint.DispNumber
            grdFlexGrid.TextMatrix(nRow, COL_NUM_OBSPNT_NAME) = clsObservationPoint.Name
            If clsObservationPoint.Genuine Then
                grdFlexGrid.TextMatrix(nRow, COL_NUM_OBSPNT_MODE) = ""
                grdFlexGrid.TextMatrix(nRow, COL_NUM_OBSPNT_SESSION) = ""
                grdFlexGrid.TextMatrix(nRow, COL_NUM_OBSPNT_STRTIME) = ""
                grdFlexGrid.TextMatrix(nRow, COL_NUM_OBSPNT_ENDTIME) = ""
                grdFlexGrid.TextMatrix(nRow, COL_NUM_OBSPNT_RECTYPE) = ""
                grdFlexGrid.TextMatrix(nRow, COL_NUM_OBSPNT_RECNUMBER) = ""
                grdFlexGrid.TextMatrix(nRow, COL_NUM_OBSPNT_ANTTYPE) = ""
                grdFlexGrid.TextMatrix(nRow, COL_NUM_OBSPNT_ANTNUMBER) = ""
                grdFlexGrid.TextMatrix(nRow, COL_NUM_OBSPNT_ANTMEASUREMENT) = ""
                grdFlexGrid.TextMatrix(nRow, COL_NUM_OBSPNT_ANTHEIGHT) = ""
                grdFlexGrid.TextMatrix(nRow, COL_NUM_OBSPNT_ANTMOUNT) = ""
                grdFlexGrid.TextMatrix(nRow, COL_NUM_OBSPNT_ANTCENTER) = ""
                grdFlexGrid.TextMatrix(nRow, COL_NUM_OBSPNT_INTERVAL) = ""
                grdFlexGrid.TextMatrix(nRow, COL_NUM_OBSPNT_ELEVATIONMASK) = ""
                grdFlexGrid.TextMatrix(nRow, COL_NUM_OBSPNT_SATELLITE) = ""
            Else
                grdFlexGrid.TextMatrix(nRow, COL_NUM_OBSPNT_MODE) = IIf(clsObservationPoint.Mode = OBJ_MODE_ADOPT, "採用", "点検")
                grdFlexGrid.TextMatrix(nRow, COL_NUM_OBSPNT_SESSION) = clsObservationPoint.Session
                grdFlexGrid.TextMatrix(nRow, COL_NUM_OBSPNT_STRTIME) = IIf(DateDiff("s", clsObservationPoint.StrTimeGPS, MIN_TIME) > 0, "", Format$(clsObservationPoint.StrTimeJST, "yyyy/mm/dd hh:mm:ss"))
                grdFlexGrid.TextMatrix(nRow, COL_NUM_OBSPNT_ENDTIME) = IIf(DateDiff("s", clsObservationPoint.EndTimeGPS, MIN_TIME) > 0, "", Format$(clsObservationPoint.EndTimeJST, "yyyy/mm/dd hh:mm:ss"))
                grdFlexGrid.TextMatrix(nRow, COL_NUM_OBSPNT_RECTYPE) = clsObservationPoint.RecTypeDispJ
                grdFlexGrid.TextMatrix(nRow, COL_NUM_OBSPNT_RECNUMBER) = clsObservationPoint.RecNumber
                grdFlexGrid.TextMatrix(nRow, COL_NUM_OBSPNT_ANTTYPE) = GetAntTypeDispJ(clsObservationPoint.AntType)
                grdFlexGrid.TextMatrix(nRow, COL_NUM_OBSPNT_ANTNUMBER) = clsObservationPoint.AntNumber
                grdFlexGrid.TextMatrix(nRow, COL_NUM_OBSPNT_ANTMEASUREMENT) = clsObservationPoint.DispMeasurement
                grdFlexGrid.TextMatrix(nRow, COL_NUM_OBSPNT_ANTHEIGHT) = FormatRound(clsObservationPoint.AntHeight, GUI_ANTHEIGHT_DECIMAL)
                Dim nHeight As Double
                grdFlexGrid.TextMatrix(nRow, COL_NUM_OBSPNT_ANTMOUNT) = IIf(clsObservationPoint.GetMountVertical(nHeight), FormatRound(nHeight, GUI_ANTHEIGHT_DECIMAL), "")
                grdFlexGrid.TextMatrix(nRow, COL_NUM_OBSPNT_ANTCENTER) = IIf(clsObservationPoint.GetTrueVertical(nHeight), FormatRound(nHeight, GUI_ANTHEIGHT_DECIMAL), "")
                grdFlexGrid.TextMatrix(nRow, COL_NUM_OBSPNT_INTERVAL) = FormatRound(clsObservationPoint.Interval, ACCOUNT_DECIMAL_INTERVAL)
                grdFlexGrid.TextMatrix(nRow, COL_NUM_OBSPNT_ELEVATIONMASK) = FormatRound(clsObservationPoint.ElevationMask, ACCOUNT_DECIMAL_ELEVATIONMASK)
                grdFlexGrid.TextMatrix(nRow, COL_NUM_OBSPNT_SATELLITE) = CStr(clsObservationPoint.NumberOfMinSV)
            End If
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '観測点リストの指定された行を更新する。
        '
        'nRow で指定される行の内容を、clsObservationPoint で指定されるオブジェクトの属性に書き換える。
        '
        '引き数：
        'grdFlexGrid リストコントロール。
        'nRow 対象とする行の番号。
        'clsObservationPoint 対象とするオブジェクト。
        */
        public void UpdateRowObsPnt(DataGridView grdFlexGrid, long nRow, ObservationPoint clsObservationPoint)
        {
            grdFlexGrid.Rows[(int)nRow].Cells[(int)COL_NUM_OBSPNT.COL_NUM_OBSPNT_ENABLE].Value = clsObservationPoint.Enable() ? "〆" : "　";
            grdFlexGrid.Rows[(int)nRow].Cells[(int)COL_NUM_OBSPNT.COL_NUM_OBSPNT_TYPE].Value = clsObservationPoint.Fixed() ? "固定" : clsObservationPoint.Genuine() ? "偏心" : "";
            grdFlexGrid.Rows[(int)nRow].Cells[(int)COL_NUM_OBSPNT.COL_NUM_OBSPNT_NUMBER].Value = clsObservationPoint.DispNumber();
            grdFlexGrid.Rows[(int)nRow].Cells[(int)COL_NUM_OBSPNT.COL_NUM_OBSPNT_NAME].Value = clsObservationPoint.Name();
            if (clsObservationPoint.Genuine())
            {
                grdFlexGrid.Rows[(int)nRow].Cells[(int)COL_NUM_OBSPNT.COL_NUM_OBSPNT_MODE].Value = "";
                grdFlexGrid.Rows[(int)nRow].Cells[(int)COL_NUM_OBSPNT.COL_NUM_OBSPNT_SESSION].Value = "";
                grdFlexGrid.Rows[(int)nRow].Cells[(int)COL_NUM_OBSPNT.COL_NUM_OBSPNT_STRTIME].Value = "";
                grdFlexGrid.Rows[(int)nRow].Cells[(int)COL_NUM_OBSPNT.COL_NUM_OBSPNT_ENDTIME].Value = "";
                grdFlexGrid.Rows[(int)nRow].Cells[(int)COL_NUM_OBSPNT.COL_NUM_OBSPNT_RECTYPE].Value = "";
                grdFlexGrid.Rows[(int)nRow].Cells[(int)COL_NUM_OBSPNT.COL_NUM_OBSPNT_RECNUMBER].Value = "";
                grdFlexGrid.Rows[(int)nRow].Cells[(int)COL_NUM_OBSPNT.COL_NUM_OBSPNT_ANTTYPE].Value = "";
                grdFlexGrid.Rows[(int)nRow].Cells[(int)COL_NUM_OBSPNT.COL_NUM_OBSPNT_ANTNUMBER].Value = "";
                grdFlexGrid.Rows[(int)nRow].Cells[(int)COL_NUM_OBSPNT.COL_NUM_OBSPNT_ANTMEASUREMENT].Value = "";
                grdFlexGrid.Rows[(int)nRow].Cells[(int)COL_NUM_OBSPNT.COL_NUM_OBSPNT_ANTHEIGHT].Value = "";
                grdFlexGrid.Rows[(int)nRow].Cells[(int)COL_NUM_OBSPNT.COL_NUM_OBSPNT_ANTMOUNT].Value = "";
                grdFlexGrid.Rows[(int)nRow].Cells[(int)COL_NUM_OBSPNT.COL_NUM_OBSPNT_ANTCENTER].Value = "";
                grdFlexGrid.Rows[(int)nRow].Cells[(int)COL_NUM_OBSPNT.COL_NUM_OBSPNT_INTERVAL].Value = "";
                grdFlexGrid.Rows[(int)nRow].Cells[(int)COL_NUM_OBSPNT.COL_NUM_OBSPNT_ELEVATIONMASK].Value = "";
                grdFlexGrid.Rows[(int)nRow].Cells[(int)COL_NUM_OBSPNT.COL_NUM_OBSPNT_SATELLITE].Value = "";
            }
            else
            {
                DateTime dt1;
                DateTime dt2;
                TimeSpan ts;
                double nHeight = 0;
                grdFlexGrid.Rows[(int)nRow].Cells[(int)COL_NUM_OBSPNT.COL_NUM_OBSPNT_MODE].Value = clsObservationPoint.Mode() == OBJ_MODE.OBJ_MODE_ADOPT ? "採用" : "点検";
                grdFlexGrid.Rows[(int)nRow].Cells[(int)COL_NUM_OBSPNT.COL_NUM_OBSPNT_SESSION].Value = clsObservationPoint.Session();
                dt1 = clsObservationPoint.StrTimeGPS();
                dt2 = new DateTime(1899, 12, 30, 0, 0, 0);
                ts = dt1.Subtract(dt2);
                grdFlexGrid.Rows[(int)nRow].Cells[(int)COL_NUM_OBSPNT.COL_NUM_OBSPNT_STRTIME].Value = ts.TotalSeconds <= 0 ? "" : Strings.Format(clsObservationPoint.StrTimeJST(), "yyyy/mm/dd hh:mm:ss");
                dt1 = clsObservationPoint.EndTimeGPS();
                dt2 = new DateTime(1899, 12, 30, 0, 0, 0);
                ts = dt1.Subtract(dt2);
                grdFlexGrid.Rows[(int)nRow].Cells[(int)COL_NUM_OBSPNT.COL_NUM_OBSPNT_ENDTIME].Value = ts.TotalSeconds <= 0 ? "" : Strings.Format(clsObservationPoint.EndTimeJST(), "yyyy/mm/dd hh:mm:ss");
                grdFlexGrid.Rows[(int)nRow].Cells[(int)COL_NUM_OBSPNT.COL_NUM_OBSPNT_RECTYPE].Value = clsObservationPoint.RecTypeDispJ();
                grdFlexGrid.Rows[(int)nRow].Cells[(int)COL_NUM_OBSPNT.COL_NUM_OBSPNT_RECNUMBER].Value = clsObservationPoint.RecNumber();
                grdFlexGrid.Rows[(int)nRow].Cells[(int)COL_NUM_OBSPNT.COL_NUM_OBSPNT_ANTTYPE].Value = GetAntTypeDispJ(clsObservationPoint.AntType());
                grdFlexGrid.Rows[(int)nRow].Cells[(int)COL_NUM_OBSPNT.COL_NUM_OBSPNT_ANTNUMBER].Value = clsObservationPoint.AntNumber();
                grdFlexGrid.Rows[(int)nRow].Cells[(int)COL_NUM_OBSPNT.COL_NUM_OBSPNT_ANTMEASUREMENT].Value = clsObservationPoint.DispMeasurement();
                grdFlexGrid.Rows[(int)nRow].Cells[(int)COL_NUM_OBSPNT.COL_NUM_OBSPNT_ANTHEIGHT].Value = FormatRound(clsObservationPoint.AntHeight(), GUI_ANTHEIGHT_DECIMAL);
                grdFlexGrid.Rows[(int)nRow].Cells[(int)COL_NUM_OBSPNT.COL_NUM_OBSPNT_ANTMOUNT].Value = clsObservationPoint.GetMountVertical(ref nHeight) ? FormatRound(nHeight, GUI_ANTHEIGHT_DECIMAL) : "";
                grdFlexGrid.Rows[(int)nRow].Cells[(int)COL_NUM_OBSPNT.COL_NUM_OBSPNT_ANTCENTER].Value = clsObservationPoint.GetTrueVertical(ref nHeight) ? FormatRound(nHeight, GUI_ANTHEIGHT_DECIMAL) : "";
                grdFlexGrid.Rows[(int)nRow].Cells[(int)COL_NUM_OBSPNT.COL_NUM_OBSPNT_INTERVAL].Value = FormatRound(clsObservationPoint.Interval(), ACCOUNT_DECIMAL_INTERVAL);
                grdFlexGrid.Rows[(int)nRow].Cells[(int)COL_NUM_OBSPNT.COL_NUM_OBSPNT_ELEVATIONMASK].Value = FormatRound(clsObservationPoint.ElevationMask(), ACCOUNT_DECIMAL_ELEVATIONMASK);
                grdFlexGrid.Rows[(int)nRow].Cells[(int)COL_NUM_OBSPNT.COL_NUM_OBSPNT_SATELLITE].Value = clsObservationPoint.NumberOfMinSV().ToString();
            }
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '基線ベクトルリストの指定された行を更新する。
        '
        'nRow で指定される行の内容を、clsBaseLineVector で指定されるオブジェクトの属性に書き換える。
        '
        '引き数：
        'grdFlexGrid リストコントロール。
        'nRow 対象とする行の番号。
        'clsBaseLineVector 対象とするオブジェクト。
        Public Sub UpdateRowVector(ByVal grdFlexGrid As NSFlexGrid, ByVal nRow As Long, ByVal clsBaseLineVector As BaseLineVector)
            grdFlexGrid.TextMatrix(nRow, COL_NUM_VECTOR_ENABLE) = IIf(clsBaseLineVector.Enable, "〆", "")
            'grdFlexGrid.TextMatrix(nRow, COL_NUM_VECTOR_ADOPT) = IIf(clsBaseLineVector.Adopt, "採用", IIf(clsBaseLineVector.Check, "点検", "重複"))
            grdFlexGrid.TextMatrix(nRow, COL_NUM_VECTOR_ADOPT) = IIf(clsBaseLineVector.Adopt, "採用", IIf(clsBaseLineVector.Check, "点検", IIf(clsBaseLineVector.Duplicate, "重複", IIf(clsBaseLineVector.HalfFst, "前半", "後半")))) '2023/06/26 Hitz H.Nakamura GNSS水準測量対応。前半後半較差の追加。
            grdFlexGrid.TextMatrix(nRow, COL_NUM_VECTOR_SESSION) = clsBaseLineVector.Session
            grdFlexGrid.TextMatrix(nRow, COL_NUM_VECTOR_STRNUMBER) = clsBaseLineVector.StrPoint.TopParentPoint.Number
            grdFlexGrid.TextMatrix(nRow, COL_NUM_VECTOR_ENDNUMBER) = clsBaseLineVector.EndPoint.TopParentPoint.Number
            grdFlexGrid.TextMatrix(nRow, COL_NUM_VECTOR_STRTIME) = IIf(DateDiff("s", clsBaseLineVector.StrTimeGPS, MIN_TIME) > 0, "", Format$(clsBaseLineVector.StrTimeJST, "yyyy/mm/dd hh:mm:ss"))
            grdFlexGrid.TextMatrix(nRow, COL_NUM_VECTOR_ENDTIME) = IIf(DateDiff("s", clsBaseLineVector.EndTimeGPS, MIN_TIME) > 0, "", Format$(clsBaseLineVector.EndTimeJST, "yyyy/mm/dd hh:mm:ss"))
            grdFlexGrid.TextMatrix(nRow, COL_NUM_VECTOR_ANALYSIS) = DISP_ANALYSIS(clsBaseLineVector.Analysis)
            If clsBaseLineVector.Analysis <= ANALYSIS_STATUS_FIX Then
                grdFlexGrid.TextMatrix(nRow, COL_NUM_VECTOR_FREQUENCY) = DISP_FREQUENCY(clsBaseLineVector.Frequency)
                grdFlexGrid.TextMatrix(nRow, COL_NUM_VECTOR_LC) = DISP_LC(clsBaseLineVector.Frequency)
        '        grdFlexGrid.TextMatrix(nRow, COL_NUM_VECTOR_SOLVEMODE) = DISP_SOLVEMODE(clsBaseLineVector.SolveMode)
                grdFlexGrid.TextMatrix(nRow, COL_NUM_VECTOR_ANALYSISSTRTIME) = Format$(clsBaseLineVector.AnalysisStrTimeJST, "yyyy/mm/dd hh:mm:ss")
                grdFlexGrid.TextMatrix(nRow, COL_NUM_VECTOR_ANALYSISENDTIME) = Format$(clsBaseLineVector.AnalysisEndTimeJST, "yyyy/mm/dd hh:mm:ss")
                Dim clsCoordinatePoint As CoordinatePoint
                Set clsCoordinatePoint = clsBaseLineVector.VectorAnalysis
                grdFlexGrid.TextMatrix(nRow, COL_NUM_VECTOR_DX) = FormatRound(clsCoordinatePoint.RoundX, GUI_XYZ_DECIMAL)
                grdFlexGrid.TextMatrix(nRow, COL_NUM_VECTOR_DY) = FormatRound(clsCoordinatePoint.RoundY, GUI_XYZ_DECIMAL)
                grdFlexGrid.TextMatrix(nRow, COL_NUM_VECTOR_DZ) = FormatRound(clsCoordinatePoint.RoundZ, GUI_XYZ_DECIMAL)
                '2014/10/03 H.Nakamura バイアス決定比が99.9999になることに伴い、四捨五入により100にならないように気をつける。
                'grdFlexGrid.TextMatrix(nRow, COL_NUM_VECTOR_BIAS) = FormatRound(clsBaseLineVector.Bias, GUI_FLOAT_DECIMAL)
                grdFlexGrid.TextMatrix(nRow, COL_NUM_VECTOR_BIAS) = FormatRound(GetDispBias(clsBaseLineVector.Bias, GUI_FLOAT_DECIMAL), GUI_FLOAT_DECIMAL)
                'grdFlexGrid.TextMatrix(nRow, COL_NUM_VECTOR_AMBPERCENTAGE) = FormatRound(clsBaseLineVector.AmbPercentage, GUI_PERCENTAGE_DECIMAL)
                If clsBaseLineVector.ObsAll <= 0 Then
                    grdFlexGrid.TextMatrix(nRow, COL_NUM_VECTOR_REJECTRATIO) = ""
                Else
                    grdFlexGrid.TextMatrix(nRow, COL_NUM_VECTOR_REJECTRATIO) = FormatRound((clsBaseLineVector.ObsAll - clsBaseLineVector.ObsUsed) / clsBaseLineVector.ObsAll * 100, GUI_PERCENTAGE_DECIMAL)
                End If
                'grdFlexGrid.TextMatrix(nRow, COL_NUM_VECTOR_RMS) = FormatRound(clsBaseLineVector.RMS, GUI_FLOAT_DECIMAL)
                If clsBaseLineVector.IsDispersion Then
                    Dim clsDispersion As Dispersion
                    Set clsDispersion = FilterDispersion(clsBaseLineVector.Dispersion)
                    grdFlexGrid.TextMatrix(nRow, COL_NUM_VECTOR_XX) = Format$(clsDispersion.XX, GUI_DISPERSION_FORMAT)
                    grdFlexGrid.TextMatrix(nRow, COL_NUM_VECTOR_YY) = Format$(clsDispersion.YY, GUI_DISPERSION_FORMAT)
                    grdFlexGrid.TextMatrix(nRow, COL_NUM_VECTOR_ZZ) = Format$(clsDispersion.ZZ, GUI_DISPERSION_FORMAT)
                    grdFlexGrid.TextMatrix(nRow, COL_NUM_VECTOR_XY) = Format$(clsDispersion.XY, GUI_DISPERSION_FORMAT)
                    grdFlexGrid.TextMatrix(nRow, COL_NUM_VECTOR_XZ) = Format$(clsDispersion.XZ, GUI_DISPERSION_FORMAT)
                    grdFlexGrid.TextMatrix(nRow, COL_NUM_VECTOR_YZ) = Format$(clsDispersion.YZ, GUI_DISPERSION_FORMAT)
                Else
                    grdFlexGrid.TextMatrix(nRow, COL_NUM_VECTOR_XX) = ""
                    grdFlexGrid.TextMatrix(nRow, COL_NUM_VECTOR_YY) = ""
                    grdFlexGrid.TextMatrix(nRow, COL_NUM_VECTOR_ZZ) = ""
                    grdFlexGrid.TextMatrix(nRow, COL_NUM_VECTOR_XY) = ""
                    grdFlexGrid.TextMatrix(nRow, COL_NUM_VECTOR_XZ) = ""
                    grdFlexGrid.TextMatrix(nRow, COL_NUM_VECTOR_YZ) = ""
                End If
                If clsBaseLineVector.AnalysisStrPoint.EnableEccentric Or clsBaseLineVector.AnalysisEndPoint.EnableEccentric Then
                    Set clsCoordinatePoint = clsBaseLineVector.VectorCorrect
                    grdFlexGrid.TextMatrix(nRow, COL_NUM_VECTOR_CORRECTX) = FormatRound(clsCoordinatePoint.RoundX, GUI_XYZ_DECIMAL)
                    grdFlexGrid.TextMatrix(nRow, COL_NUM_VECTOR_CORRECTY) = FormatRound(clsCoordinatePoint.RoundY, GUI_XYZ_DECIMAL)
                    grdFlexGrid.TextMatrix(nRow, COL_NUM_VECTOR_CORRECTZ) = FormatRound(clsCoordinatePoint.RoundZ, GUI_XYZ_DECIMAL)
                Else
                    grdFlexGrid.TextMatrix(nRow, COL_NUM_VECTOR_CORRECTX) = ""
                    grdFlexGrid.TextMatrix(nRow, COL_NUM_VECTOR_CORRECTY) = ""
                    grdFlexGrid.TextMatrix(nRow, COL_NUM_VECTOR_CORRECTZ) = ""
                End If
            Else
                grdFlexGrid.TextMatrix(nRow, COL_NUM_VECTOR_FREQUENCY) = ""
                grdFlexGrid.TextMatrix(nRow, COL_NUM_VECTOR_LC) = ""
                'grdFlexGrid.TextMatrix(nRow, COL_NUM_VECTOR_SOLVEMODE) = ""
                grdFlexGrid.TextMatrix(nRow, COL_NUM_VECTOR_ANALYSISSTRTIME) = ""
                grdFlexGrid.TextMatrix(nRow, COL_NUM_VECTOR_ANALYSISENDTIME) = ""
                grdFlexGrid.TextMatrix(nRow, COL_NUM_VECTOR_DX) = ""
                grdFlexGrid.TextMatrix(nRow, COL_NUM_VECTOR_DY) = ""
                grdFlexGrid.TextMatrix(nRow, COL_NUM_VECTOR_DZ) = ""
                grdFlexGrid.TextMatrix(nRow, COL_NUM_VECTOR_BIAS) = ""
                'grdFlexGrid.TextMatrix(nRow, COL_NUM_VECTOR_AMBPERCENTAGE) = ""
                grdFlexGrid.TextMatrix(nRow, COL_NUM_VECTOR_REJECTRATIO) = ""
                'grdFlexGrid.TextMatrix(nRow, COL_NUM_VECTOR_RMS) = ""
                grdFlexGrid.TextMatrix(nRow, COL_NUM_VECTOR_XX) = ""
                grdFlexGrid.TextMatrix(nRow, COL_NUM_VECTOR_YY) = ""
                grdFlexGrid.TextMatrix(nRow, COL_NUM_VECTOR_ZZ) = ""
                grdFlexGrid.TextMatrix(nRow, COL_NUM_VECTOR_XY) = ""
                grdFlexGrid.TextMatrix(nRow, COL_NUM_VECTOR_XZ) = ""
                grdFlexGrid.TextMatrix(nRow, COL_NUM_VECTOR_YZ) = ""
                grdFlexGrid.TextMatrix(nRow, COL_NUM_VECTOR_CORRECTX) = ""
                grdFlexGrid.TextMatrix(nRow, COL_NUM_VECTOR_CORRECTY) = ""
                grdFlexGrid.TextMatrix(nRow, COL_NUM_VECTOR_CORRECTZ) = ""
            End If
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '基線ベクトルリストの指定された行を更新する。
        '
        'nRow で指定される行の内容を、clsBaseLineVector で指定されるオブジェクトの属性に書き換える。
        '
        '引き数：
        'grdFlexGrid リストコントロール。
        'nRow 対象とする行の番号。
        'clsBaseLineVector 対象とするオブジェクト。
        */
        public void UpdateRowVector(DataGridView grdFlexGrid, long nRow, BaseLineVector clsBaseLineVector)
        {
            DateTime dt1;
            DateTime dt2;
            TimeSpan ts;
            Dispersion clsDispersion = FilterDispersion(clsBaseLineVector.Dispersion());
            CoordinatePoint clsCoordinatePoint = clsBaseLineVector.VectorAnalysis();
            grdFlexGrid.Rows[(int)nRow].Cells[(int)COL_NUM_VECTOR.COL_NUM_VECTOR_ENABLE].Value = clsBaseLineVector.Enable() ? "〆" : "";
            grdFlexGrid.Rows[(int)nRow].Cells[(int)COL_NUM_VECTOR.COL_NUM_VECTOR_ADOPT].Value = clsBaseLineVector.Adopt() ? "採用" : clsBaseLineVector.Check() ? "点検" : clsBaseLineVector.Duplicate() ? "重複" : clsBaseLineVector.HalfFst() ? "前半" : "後半"; //'2023/06/26 Hitz H.Nakamura GNSS水準測量対応。前半後半較差の追加。
            grdFlexGrid.Rows[(int)nRow].Cells[(int)COL_NUM_VECTOR.COL_NUM_VECTOR_SESSION].Value = clsBaseLineVector.Session;
            grdFlexGrid.Rows[(int)nRow].Cells[(int)COL_NUM_VECTOR.COL_NUM_VECTOR_STRNUMBER].Value = clsBaseLineVector.StrPoint().TopParentPoint().Number();
            grdFlexGrid.Rows[(int)nRow].Cells[(int)COL_NUM_VECTOR.COL_NUM_VECTOR_ENDNUMBER].Value = clsBaseLineVector.EndPoint().TopParentPoint().Number();
            dt1 = clsBaseLineVector.StrTimeGPS;
            dt2 = new DateTime(1899, 12, 30, 0, 0, 0);
            ts = dt1.Subtract(dt2);
            grdFlexGrid.Rows[(int)nRow].Cells[(int)COL_NUM_VECTOR.COL_NUM_VECTOR_STRTIME].Value = ts.TotalSeconds <= 0 ? "" : Strings.Format(clsBaseLineVector.StrTimeJST(), "yyyy/mm/dd hh:mm:ss");
            dt1 = clsBaseLineVector.EndTimeGPS;
            dt2 = new DateTime(1899, 12, 30, 0, 0, 0);
            ts = dt1.Subtract(dt2);
            grdFlexGrid.Rows[(int)nRow].Cells[(int)COL_NUM_VECTOR.COL_NUM_VECTOR_ENDTIME].Value = ts.TotalSeconds <= 0 ? "" : Strings.Format(clsBaseLineVector.EndTimeJST(), "yyyy/mm/dd hh:mm:ss");
            grdFlexGrid.Rows[(int)nRow].Cells[(int)COL_NUM_VECTOR.COL_NUM_VECTOR_ANALYSIS].Value = DISP_ANALYSIS[(int)clsBaseLineVector.Analysis];
            if (clsBaseLineVector.Analysis <= ANALYSIS_STATUS.ANALYSIS_STATUS_FIX)
            {
                grdFlexGrid.Rows[(int)nRow].Cells[(int)COL_NUM_VECTOR.COL_NUM_VECTOR_FREQUENCY].Value = DISP_FREQUENCY[(int)clsBaseLineVector.Frequency];
                grdFlexGrid.Rows[(int)nRow].Cells[(int)COL_NUM_VECTOR.COL_NUM_VECTOR_LC].Value = DISP_LC[(int)clsBaseLineVector.Frequency];
                //'        grdFlexGrid.TextMatrix(nRow, COL_NUM_VECTOR_SOLVEMODE) = DISP_SOLVEMODE(clsBaseLineVector.SolveMode)
                grdFlexGrid.Rows[(int)nRow].Cells[(int)COL_NUM_VECTOR.COL_NUM_VECTOR_ANALYSISSTRTIME].Value = Strings.Format(clsBaseLineVector.AnalysisStrTimeJST(), "yyyy/mm/dd hh:mm:ss");
                grdFlexGrid.Rows[(int)nRow].Cells[(int)COL_NUM_VECTOR.COL_NUM_VECTOR_ANALYSISENDTIME].Value = Strings.Format(clsBaseLineVector.AnalysisEndTimeJST(), "yyyy/mm/dd hh:mm:ss");
                grdFlexGrid.Rows[(int)nRow].Cells[(int)COL_NUM_VECTOR.COL_NUM_VECTOR_DX].Value = FormatRound(clsCoordinatePoint.RoundXX(), GUI_XYZ_DECIMAL);
                grdFlexGrid.Rows[(int)nRow].Cells[(int)COL_NUM_VECTOR.COL_NUM_VECTOR_DY].Value = FormatRound(clsCoordinatePoint.RoundYY(), GUI_XYZ_DECIMAL);
                grdFlexGrid.Rows[(int)nRow].Cells[(int)COL_NUM_VECTOR.COL_NUM_VECTOR_DZ].Value = FormatRound(clsCoordinatePoint.RoundZZ(), GUI_XYZ_DECIMAL);
                //'2014/10/03 H.Nakamura バイアス決定比が99.9999になることに伴い、四捨五入により100にならないように気をつける。
                //'grdFlexGrid.TextMatrix(nRow, COL_NUM_VECTOR_BIAS) = FormatRound(clsBaseLineVector.Bias, GUI_FLOAT_DECIMAL)
                grdFlexGrid.Rows[(int)nRow].Cells[(int)COL_NUM_VECTOR.COL_NUM_VECTOR_BIAS].Value = FormatRound(GetDispBias(clsBaseLineVector.Bias, GUI_FLOAT_DECIMAL), GUI_FLOAT_DECIMAL);
                //'grdFlexGrid.TextMatrix(nRow, COL_NUM_VECTOR_AMBPERCENTAGE) = FormatRound(clsBaseLineVector.AmbPercentage, GUI_PERCENTAGE_DECIMAL)
                if (clsBaseLineVector.ObsAll() <= 0)
                {
                    grdFlexGrid.Rows[(int)nRow].Cells[(int)COL_NUM_VECTOR.COL_NUM_VECTOR_REJECTRATIO].Value = "";
                }
                else
                {
                    grdFlexGrid.Rows[(int)nRow].Cells[(int)COL_NUM_VECTOR.COL_NUM_VECTOR_REJECTRATIO].Value
                        = FormatRound((clsBaseLineVector.ObsAll() - clsBaseLineVector.ObsUsed()) / clsBaseLineVector.ObsAll() * 100, GUI_PERCENTAGE_DECIMAL);
                }
                //'grdFlexGrid.TextMatrix(nRow, COL_NUM_VECTOR_RMS) = FormatRound(clsBaseLineVector.RMS, GUI_FLOAT_DECIMAL)
                if (clsBaseLineVector.IsDispersion)
                {
                    grdFlexGrid.Rows[(int)nRow].Cells[(int)COL_NUM_VECTOR.COL_NUM_VECTOR_XX].Value = Strings.Format(clsDispersion.XX, GUI_DISPERSION_FORMAT);
                    grdFlexGrid.Rows[(int)nRow].Cells[(int)COL_NUM_VECTOR.COL_NUM_VECTOR_YY].Value = Strings.Format(clsDispersion.YY, GUI_DISPERSION_FORMAT);
                    grdFlexGrid.Rows[(int)nRow].Cells[(int)COL_NUM_VECTOR.COL_NUM_VECTOR_ZZ].Value = Strings.Format(clsDispersion.ZZ, GUI_DISPERSION_FORMAT);
                    grdFlexGrid.Rows[(int)nRow].Cells[(int)COL_NUM_VECTOR.COL_NUM_VECTOR_XY].Value = Strings.Format(clsDispersion.XY, GUI_DISPERSION_FORMAT);
                    grdFlexGrid.Rows[(int)nRow].Cells[(int)COL_NUM_VECTOR.COL_NUM_VECTOR_XZ].Value = Strings.Format(clsDispersion.XZ, GUI_DISPERSION_FORMAT);
                    grdFlexGrid.Rows[(int)nRow].Cells[(int)COL_NUM_VECTOR.COL_NUM_VECTOR_YZ].Value = Strings.Format(clsDispersion.YZ, GUI_DISPERSION_FORMAT);
                }
                else
                {
                    grdFlexGrid.Rows[(int)nRow].Cells[(int)COL_NUM_VECTOR.COL_NUM_VECTOR_XX].Value = "";
                    grdFlexGrid.Rows[(int)nRow].Cells[(int)COL_NUM_VECTOR.COL_NUM_VECTOR_YY].Value = "";
                    grdFlexGrid.Rows[(int)nRow].Cells[(int)COL_NUM_VECTOR.COL_NUM_VECTOR_ZZ].Value = "";
                    grdFlexGrid.Rows[(int)nRow].Cells[(int)COL_NUM_VECTOR.COL_NUM_VECTOR_XY].Value = "";
                    grdFlexGrid.Rows[(int)nRow].Cells[(int)COL_NUM_VECTOR.COL_NUM_VECTOR_XZ].Value = "";
                    grdFlexGrid.Rows[(int)nRow].Cells[(int)COL_NUM_VECTOR.COL_NUM_VECTOR_YZ].Value = "";
                }
                if (clsBaseLineVector.AnalysisStrPoint().EnableEccentric() || clsBaseLineVector.AnalysisEndPoint().EnableEccentric())
                {
                    clsCoordinatePoint = clsBaseLineVector.VectorCorrect();
                    grdFlexGrid.Rows[(int)nRow].Cells[(int)COL_NUM_VECTOR.COL_NUM_VECTOR_CORRECTX].Value = FormatRound(clsCoordinatePoint.RoundXX(), GUI_XYZ_DECIMAL);
                    grdFlexGrid.Rows[(int)nRow].Cells[(int)COL_NUM_VECTOR.COL_NUM_VECTOR_CORRECTY].Value = FormatRound(clsCoordinatePoint.RoundYY(), GUI_XYZ_DECIMAL);
                    grdFlexGrid.Rows[(int)nRow].Cells[(int)COL_NUM_VECTOR.COL_NUM_VECTOR_CORRECTZ].Value = FormatRound(clsCoordinatePoint.RoundZZ(), GUI_XYZ_DECIMAL);
                }
                else
                {
                    grdFlexGrid.Rows[(int)nRow].Cells[(int)COL_NUM_VECTOR.COL_NUM_VECTOR_CORRECTX].Value = "";
                    grdFlexGrid.Rows[(int)nRow].Cells[(int)COL_NUM_VECTOR.COL_NUM_VECTOR_CORRECTY].Value = "";
                    grdFlexGrid.Rows[(int)nRow].Cells[(int)COL_NUM_VECTOR.COL_NUM_VECTOR_CORRECTZ].Value = "";
                }
            }
            else
            {
                grdFlexGrid.Rows[(int)nRow].Cells[(int)COL_NUM_VECTOR.COL_NUM_VECTOR_FREQUENCY].Value = "";
                grdFlexGrid.Rows[(int)nRow].Cells[(int)COL_NUM_VECTOR.COL_NUM_VECTOR_LC].Value = "";
                //'grdFlexGrid.TextMatrix(nRow, COL_NUM_VECTOR_SOLVEMODE) = ""
                grdFlexGrid.Rows[(int)nRow].Cells[(int)COL_NUM_VECTOR.COL_NUM_VECTOR_ANALYSISSTRTIME].Value = "";
                grdFlexGrid.Rows[(int)nRow].Cells[(int)COL_NUM_VECTOR.COL_NUM_VECTOR_ANALYSISENDTIME].Value = "";
                grdFlexGrid.Rows[(int)nRow].Cells[(int)COL_NUM_VECTOR.COL_NUM_VECTOR_DX].Value = "";
                grdFlexGrid.Rows[(int)nRow].Cells[(int)COL_NUM_VECTOR.COL_NUM_VECTOR_DY].Value = "";
                grdFlexGrid.Rows[(int)nRow].Cells[(int)COL_NUM_VECTOR.COL_NUM_VECTOR_DZ].Value = "";
                grdFlexGrid.Rows[(int)nRow].Cells[(int)COL_NUM_VECTOR.COL_NUM_VECTOR_BIAS].Value = "";
                //'grdFlexGrid.TextMatrix(nRow, COL_NUM_VECTOR_AMBPERCENTAGE) = ""
                grdFlexGrid.Rows[(int)nRow].Cells[(int)COL_NUM_VECTOR.COL_NUM_VECTOR_REJECTRATIO].Value = "";
                //'grdFlexGrid.TextMatrix(nRow, COL_NUM_VECTOR_RMS) = ""
                grdFlexGrid.Rows[(int)nRow].Cells[(int)COL_NUM_VECTOR.COL_NUM_VECTOR_XX].Value = "";
                grdFlexGrid.Rows[(int)nRow].Cells[(int)COL_NUM_VECTOR.COL_NUM_VECTOR_YY].Value = "";
                grdFlexGrid.Rows[(int)nRow].Cells[(int)COL_NUM_VECTOR.COL_NUM_VECTOR_ZZ].Value = "";
                grdFlexGrid.Rows[(int)nRow].Cells[(int)COL_NUM_VECTOR.COL_NUM_VECTOR_XY].Value = "";
                grdFlexGrid.Rows[(int)nRow].Cells[(int)COL_NUM_VECTOR.COL_NUM_VECTOR_XZ].Value = "";
                grdFlexGrid.Rows[(int)nRow].Cells[(int)COL_NUM_VECTOR.COL_NUM_VECTOR_YZ].Value = "";
                grdFlexGrid.Rows[(int)nRow].Cells[(int)COL_NUM_VECTOR.COL_NUM_VECTOR_CORRECTX].Value = "";
                grdFlexGrid.Rows[(int)nRow].Cells[(int)COL_NUM_VECTOR.COL_NUM_VECTOR_CORRECTY].Value = "";
                grdFlexGrid.Rows[(int)nRow].Cells[(int)COL_NUM_VECTOR.COL_NUM_VECTOR_CORRECTZ].Value = "";
            }
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'リスト更新必要フラグ(IsList)が指定されているオブジェクトの行を更新する。
        '
        'nList で指定されるリストを更新する。
        'IsList が True であるオブジェクトに対応する行だけ更新する。
        '
        '引き数：
        'nList リスト種別。
        'grdFlexGrid リストコントロール。
        'objMap 要素マップ。要素はオブジェクト。キーは要素のポインタ(リストの RowData に設定されている)。
        Public Sub UpdateRowIsListImpl(ByVal nList As Long, ByVal grdFlexGrid As NSFlexGrid, ByVal objMap As Collection)
            '要素が無い場合は何もしない。
            Dim nData As Long
            Dim nRow As Long
            Dim objElement As Object
            If grdFlexGrid.HighLight = flexHighlightAlways Then
                For nRow = 1 To grdFlexGrid.rows - 1
                    nData = grdFlexGrid.RowData(nRow)
                    Set objElement = objMap.Item(Hex$(nData))
                    If objElement.IsList Then Call UpdateRowList(nList, grdFlexGrid, nRow, objElement)
                Next
            End If
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        'リスト更新必要フラグ(IsList)が指定されているオブジェクトの行を更新する。
        '
        'nList で指定されるリストを更新する。
        'IsList が True であるオブジェクトに対応する行だけ更新する。
        '
        '引き数：
        'nList リスト種別。
        'grdFlexGrid リストコントロール。
        'objMap 要素マップ。要素はオブジェクト。キーは要素のポインタ(リストの RowData に設定されている)。
        */
        public void UpdateRowIsListImpl(long nList, DataGridView grdFlexGrid, Dictionary<string, object> objMap)
        {
            //'要素が無い場合は何もしない。
            long nData;
            string sData;
            long nRow;
            object objElement;
            if (grdFlexGrid.SelectedRows.Count > 0)
            {
                for (nRow = 1; nRow < grdFlexGrid.RowCount; nRow++)
                {
#if false
                    sData = grdFlexGrid.Rows[(int)nRow].ToString();
                    objElement = objMap.Item(Hex$(nData));
                    if (objElement.IsList)
                    {
                        UpdateRowList(nList, grdFlexGrid, nRow, objElement);
                    }
#endif
                }
            }
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'リストを再作成する。
        '
        'nList で指定されるリストを再作成する。
        'リストの行を設定する。
        'リストの初期化は MakeList 関係のメソッド。
        '
        '引き数：
        'nList リスト種別。
        'grdFlexGrid リストコントロール。
        'objMap 要素マップ。要素はオブジェクト。キーは要素のポインタ(リストの RowData に設定されている)。
        Public Sub RemakeListImpl(ByVal nList As Long, ByVal grdFlexGrid As NSFlexGrid, ByRef objMap As Collection)
            If nList = LIST_NUM_OBSPNT Then
                Call RemakeListObsPnt(nList, grdFlexGrid, objMap)
            Else
                Call RemakeListVector(nList, grdFlexGrid, objMap)
            End If
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        'リストを再作成する。
        '
        'nList で指定されるリストを再作成する。
        'リストの行を設定する。
        'リストの初期化は MakeList 関係のメソッド。
        '
        '引き数：
        'nList リスト種別。
        'grdFlexGrid リストコントロール。
        'objMap 要素マップ。要素はオブジェクト。キーは要素のポインタ(リストの RowData に設定されている)。
        */
        public void RemakeListImpl(long nList, DataGridView grdFlexGrid, ref Dictionary<string, object> objMap)
        {
            if (nList == (long)LIST_NUM_PANE.LIST_NUM_OBSPNT)
            {
                //'観測点。
                m_clsMdlListProc.RemakeListObsPnt(nList, grdFlexGrid, ref objMap);
            }
            else if (nList == (long)LIST_NUM_PANE.LIST_NUM_VECTOR)
            {
                //'ベクトル。
                m_clsMdlListProc.RemakeListVector(nList, grdFlexGrid, ref objMap);
            }
            else
            {
                //'座標。

            }
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'リストの指定された行をクリアする。
        '
        '引き数：
        'nList リスト種別。
        'grdFlexGrid リストコントロール。
        'nRow 対象とする行の番号。
        Public Sub ClearRowList(ByVal nList As Long, ByVal grdFlexGrid As NSFlexGrid, ByVal nRow As Long)
            Dim nCol As Long
            If nList = LIST_NUM_OBSPNT Then
                For nCol = 0 To LIST_COL_COUNT_OBSPNT - 1
                    grdFlexGrid.TextMatrix(nRow, nCol) = ""
                Next
            Else
                For nCol = 0 To LIST_COL_COUNT_VECTOR - 1
                    grdFlexGrid.TextMatrix(nRow, nCol) = ""
                Next
            End If
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        'リストの指定された行をクリアする。
        '
        '引き数：
        'nList リスト種別。
        'grdFlexGrid リストコントロール。
        'nRow 対象とする行の番号。
        */
        public void ClearRowList(long nList, DataGridView grdFlexGrid, long nRow)
        {
            long nCol;
            if (nList == (long)LIST_NUM_PANE.LIST_NUM_OBSPNT)
            {
                //'観測点。
                for (nCol = 0; nCol < LIST_COL_COUNT_OBSPNT; nCol++)
                {
                    grdFlexGrid[(int)nCol, (int)nRow].Value = "";
                }
            }
            else if (nList == (long)LIST_NUM_PANE.LIST_NUM_VECTOR)
            {
                //'ベクトル。
                for (nCol = 0; nCol < LIST_COL_COUNT_VECTOR; nCol++)
                {
                    grdFlexGrid[(int)nCol, (int)nRow].Value = "";
                }
            }
            else if (nList == (long)LIST_NUM_PANE.LIST_NUM_ZAHYOU)
            {
                //'座標。
            }
        }
        //==========================================================================================

        //==========================================================================================
        //[C#]
        /// <summary>
        /// 
        /// </summary>
        /// <param name="listPane"></param>

        public void ListDataDisp(ListPane listPane)
        {
#if false
            listPane.m_list = 15;

            listPane.g.grdFlexGrid.View = View.Details;
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
        //==========================================================================================

        //24/03/07(--)編集メニュー K.setoguchi@NV---------->>>>>>>>>>
        //新規
        /// <summary>
        ///  編集＞観測点/ベクトル情報の編集
        /// 
        /// nList 0=観測点データ
        /// リストの行を設定する。
        /// リストの初期化は MakeList 関係のメソッド。
        ///'
        /// 引き数：
        /// nList 観測点種別。
        //  'grdFlexGrid リストコントロール。
        //  'objMap 要素マップ。要素はオブジェクト。キーは要素のポインタ(リストの RowData に設定されている)。
        /// </summary>
        /// <param name="nList"></param>
        /// <param name="grdFlexGrid"></param>
        /// <param name="objMap"></param>
        /// <returns>
        /// 戻り値：
        /// 観測点情報の編集
        /// </returns>
        public object SelectedElement(long nList, DataGridView grdFlexGrid, ref Dictionary<string, object> objMap)
        {

            object SelectedElement = null;

            if (nList == (long)LIST_NUM_PANE.LIST_NUM_OBSPNT)
            {
                //'観測点。
                SelectedElement = m_clsMdlListProc.SelectedElement_ObsPnt(nList, grdFlexGrid, ref objMap);

            }
            else if (nList == (long)LIST_NUM_PANE.LIST_NUM_VECTOR)
            {
                //'ベクトル。
                SelectedElement = m_clsMdlListProc.SelectedElement_Vector(nList, grdFlexGrid, ref objMap);
            }
            else
            {
                //'座標。

            }
            return SelectedElement;


        }
        //<<<<<<<<<-----------24/03/07(--)編集メニュー K.setoguchi@NV

        //***********************************************************************************************************
        //新規    //2
        /// <summary>
        /// 選択行の要素取得を開始する。
        /// '
        /// 選択行の位置を示す整数値が取得される。
        /// このメソッドで取得される整数値を GetNextAssoc メソッドに渡すことにより選択行のオブジェクトが取得できる。
        /// GetNextAssoc で 0 が返るまで、繰り返し GetNextAssoc を呼ぶことによりすべての選択行のオブジェクトが取得できる。
        /// '
        /// 引き数：
        /// nList 対象とするリストの番号。省略するとカレントリストが対象になる。
        /// 
        /// </summary>
        /// <param name="nList"></param>
        /// <param name="grdFlexGrid"></param>
        /// <param name="objMap"></param>
        /// <param name="nPos"></param>
        /// <returns>
        /// 戻り値：
        ///     選択行の位置を示す整数値を返す。<--( 0) から XX
        ///     選択行が無い場合は 0 を返す。<----- --(-1)に訂正
        /// </returns>
        public long StartSelectedAssoc(long nList, DataGridView grdFlexGrid, ref Dictionary<string, object> objMap)
        {
            long StartSelectedAssoc = 0;

            if (nList == (long)LIST_NUM_PANE.LIST_NUM_OBSPNT)
            {
                //'観測点。
                StartSelectedAssoc = m_clsMdlListProc.StartSelectedAssoc_ObsPnt(nList, grdFlexGrid, ref objMap);

            }
            else if (nList == (long)LIST_NUM_PANE.LIST_NUM_VECTOR)
            {
                //'ベクトル。
                StartSelectedAssoc = m_clsMdlListProc.StartSelectedAssoc_Vector(nList, grdFlexGrid, ref objMap);
            }
            else
            {
                //'座標。

            }
            return StartSelectedAssoc;

        }
        //===================================================================================================
        //新規    //2
        /// <summary>
        ///  指示れてたｎ行目の観測点/ベクトル情報
        /// 
        /// nList 0=観測点データ
        /// リストの行を設定する。
        /// リストの初期化は MakeList 関係のメソッド。
        ///'
        /// 引き数：
        ///  nList 観測点種別。
        ///  grdFlexGrid リストコントロール。
        ///  objMap 要素マップ。要素はオブジェクト。キーは要素のポインタ(リストの RowData に設定されている)。
        ///  nPos 指示れてた行(0行目---)
        /// </summary>
        /// <param name="nList"></param>
        /// <param name="grdFlexGrid"></param>
        /// <param name="objMap"></param>
        /// <param name="nPos"></param>
        /// <returns>
        /// 戻り値：
        /// 観測点/ベクトル情報
        /// </returns>
        public object GetNextAssoc(long nList, DataGridView grdFlexGrid, ref Dictionary<string, object> objMap, ref long nPos)
        {
            object GetNextAssoc = null;

            if (nList == (long)LIST_NUM_PANE.LIST_NUM_OBSPNT)
            {
                //'観測点。
                GetNextAssoc = m_clsMdlListProc.GetNextAssoc_ObsPnt(nList, grdFlexGrid, ref objMap, ref nPos);

            }
            else if (nList == (long)LIST_NUM_PANE.LIST_NUM_VECTOR)
            {
                //'ベクトル。
                GetNextAssoc = m_clsMdlListProc.GetNextAssoc_Vector(nList, grdFlexGrid, ref objMap, ref nPos);
            }
            else
            {
                //'座標。
            }

            return GetNextAssoc;
        }






        //新規    //2
        public object EditValid(long nList, DataGridView grdFlexGrid, ref Dictionary<string, object> objMap)
        {

            object EditValid = null;

            if (nList == (long)LIST_NUM_PANE.LIST_NUM_OBSPNT)
            {
                //'観測点。
                EditValid = m_clsMdlListProc.Element_ObsPnt(nList, grdFlexGrid, ref objMap);

            }
            else if (nList == (long)LIST_NUM_PANE.LIST_NUM_VECTOR)
            {
                //'ベクトル。
                EditValid = m_clsMdlListProc.Element_Vector(nList, grdFlexGrid, ref objMap);
            }
            else
            {
                //'座標。

            }
            return EditValid;


        }

    }
}
