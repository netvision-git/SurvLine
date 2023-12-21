using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using System.Windows.Forms;

namespace SurvLine
{
    public class MdlBaseLineAnalyser
    {

        //'*******************************************************************************
        //'基線解析器
        //'*******************************************************************************

        //'解析状態。
        public enum ANALYSIS_STATUS
        {
            ANALYSIS_STATUS_FLOAT = 0,  //'FLOAT。
            ANALYSIS_STATUS_FIX = 1,    //'FIX。
            ANALYSIS_STATUS_FAILED = 2, //'解析失敗。
            ANALYSIS_STATUS_NOT = 3,    //'未解析。
            ANALYSIS_STATUS_COUNT = 4,  //'数。
        }

        //'軌道暦。
        public enum ORBIT_TYPE
        {
            ORBIT_BROADCAST = 0, // '放送暦。
            ORBIT_PRECISE, // '精密暦。
            ORBIT_COUNT, // '数。
        }

        //'周波数。
        public enum FREQUENCY_TYPE
        {
            FREQUENCY_L1 = 0,   // 'L1。
            FREQUENCY_L2,       // 'L2｡
            FREQUENCY_L1L2,     // 'L1とL2の両方｡
            FREQUENCY_L5,       // 'L1とL2を組み合わせたワイドレーン (Lw)。
            FREQUENCY_L3,       // 'L1とL2を組み合わせた電離層補正 (lc)。
            FREQUENCY_L5L3,     //'ワイドレーンと電離層補正の両方｡
            FREQUENCY_AUTO,     //'自動選択｡
            FREQUENCY_LC_NONE,  //'搬送波位相なし (PPS内部変数)。
            //  '2017/07/13 NS6000対応。'''''''''''''''''''''''''''''''''''''''''''''''''''''''
            //  'FREQUENCY_COUNT '数。
            //'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            FREQUENCY_COUNT_OLD,//'数。
            FREQUENCY_1F = 8,   //'1周波。
            FREQUENCY_2F,       //'2周波(L1+L2)。
            FREQUENCY_L1L5,     //'2周波(L1+L5)。
            FREQUENCY_3F,       //'3周波。
            FREQUENCY_IFMW,     //'iono-free & Melbourne-Wuebbena
            FREQUENCY_COUNT,    //'数。
            //'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        }

        //'解析モード。
        public enum SOLVEMODE_TYPE
        {
            SOLVEMODE_STATIC = 0,   //'静止。
            SOLVEMODE_KINEMATICS,   //'キネマティック。
            SOLVEMODE_COUNT,        //'数。
        }

        //'時間種別。
        public enum TIME_TYPE
        {
            TIME_PROJECT,   //'プロジェクト全体。
            TIME_SPECIFY,   //'指定間隔。
        }

        //'対流圏モデル。
        public enum TROPOSHERE_TYPE
        {
            TROPOSHERE_SAASRTAMOINEN_OLD = 0,   //'ザースタモイネン。
            TROPOSHERE_MARINI_OLD,              //'マリニ。
            TROPOSHERE_HOPFIELD_OLD,            //'ホップフィールド。
            TROPOSHERE_NONE_OLD,                //'モデルなし。
            TROPOSHERE_BASEINDEX = 4,           //'0～3はUNB3Mへ変換する。
            TROPOSHERE_SAASRTAMOINEN = 4,       //'ザースタモイネン。
            TROPOSHERE_MARINI,                  //'マリニ。
            TROPOSHERE_HOPFIELD,                //'ホップフィールド。
            TROPOSHERE_UNB3M,                   //'UNB3M。
            TROPOSHERE_NONE,                    //'モデルなし。
        }

        //'対流圏のマッピング。
        public enum TROPMAP_TYPE
        {
            TROPMAP_COSZ = 0,   //'COS(ZENITH ANGLE)マッピング
            TROPMAP_DRY_NIELL,  // '乾燥地域ニールマッピング
            TROPMAP_WET_NIELL,  // '高湿度地域ニールマッピング
            TROPMAP_HOPF,       // 'ホップフィールドマッピング
            TROPMAP_NONE,       // 'マッピングなし
            TROPMAP_COUNT,      // '数。
        }

        //'オプション。
        public const long PPS_OPT_APPLY_TIME_WINDOW = 0x01;     //'解析開始、終了時間指定。
        public const long PPS_OPT_APPLY_ANT_OFFSETS = 0x02;     //'アンテナL1L2オフセット指定
        public const long PPS_OPT_APPLY_ANT_PATTERN = 0x04;     //'アンテナ仰角、方位角による位相補正指定

        //'解の種類表示文字列。
        public const string DISP_ANALYSIS_FLOAT = "FLOAT";  //'FLOAT。
        public const string DISP_ANALYSIS_FIX = "FIX";      //'FIX。
        public const string DISP_ANALYSIS_FAILED = "失敗";  //'解析失敗。
        public const string DISP_ANALYSIS_NOT = "";         //'未解析。
        public string[] DISP_ANALYSIS = new string[(long)ANALYSIS_STATUS.ANALYSIS_STATUS_COUNT - 1];

        //'軌道暦表示文字列。
        public const string DISP_ORBIT_BROADCAST = "放送暦"; //'放送暦。
        public const string DISP_ORBIT_PRECISE = "精密暦"; //'精密暦。
        //        public const string DISP_ORBIT(ORBIT_COUNT - 1) As String
        public string[] DISP_ORBIT = new string[(long)ORBIT_TYPE.ORBIT_COUNT - 1];

        //'周波数表示文字列。
        public const string DISP_LC_L1 = "L1";      //'L1。
        public const string DISP_LC_L2 = "L2";      //'L2｡
        public const string DISP_LC_L1L2 = "L1+L2"; //'L1とL2の両方｡
        public const string DISP_LC_L5 = "L5";      //'L1とL2を組み合わせたワイドレーン (Lw)。
        public const string DISP_LC_L3 = "L3";      //'L1とL2を組み合わせた電離層補正 (lc)。
        public const string DISP_LC_L5L3 = "L5+L3"; //'ワイドレーンと電離層補正の両方｡
        public const string DISP_LC_AUTO = "自動";  //'自動選択｡
        public const string DISP_LC_NONE = "なし";  //'搬送波位相なし (PPS内部変数)。
        //'2017/07/13 NS6000対応。'''''''''''''''''''''''''''''''''''''''''''''''''''''''
        public const string DISP_LC_1F = "L1";      //'L1
        public const string DISP_LC_2F = "L1+L2";   //'2周波(L1+L2)
        public const string DISP_LC_L1L5 = "L1+L5"; //'2周波(L1+L5)
        public const string DISP_LC_3F = "L1+L2+L5";//'3周波
        public const string DISP_LC_IFMW = "IF&MW"; //'iono-free & Melbourne-Wuebbena
        //'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        //  Public DISP_LC(FREQUENCY_COUNT - 1) As String
        public string[] DISP_LC = new string[(long)FREQUENCY_TYPE.FREQUENCY_COUNT - 1];

        //'2018/04/11 NS6000対応。'''''''''''''''''''''''''''''''''''''''''''''''''''''''
        public const string DISP_LC_R_1F = "G1";        // 'L1
        public const string DISP_LC_R_2F = "G1+G2";     //'L1+L2
        public const string DISP_LC_R_L1L5 = "G1";      //'L1+L5
        public const string DISP_LC_R_3F = "G1+G2";     //'L1+L2+L5
        public const string DISP_LC_R_IFMW = "IF&MW";  　//'iono-free & Melbourne-Wuebbena
        // Public DISP_LC_R(FREQUENCY_COUNT - FREQUENCY_COUNT_OLD - 1) As String
        public string[] DISP_LC_R = new string[(long)FREQUENCY_TYPE.FREQUENCY_COUNT - (long)FREQUENCY_TYPE.FREQUENCY_COUNT_OLD - 1];

        public const string DISP_LC_E_1F = "E1";        //'L1
        public const string DISP_LC_E_2F = "E1";        //'L1+L2
        public const string DISP_LC_E_L1L5 = "E1+E5";   //'L1+L5
        public const string DISP_LC_E_3F = "E1+E5";     //'L1+L2+L5
        public const string DISP_LC_E_IFMW = "IF&MW";   //'iono-free & Melbourne-Wuebbena
        //  Public DISP_LC_E(FREQUENCY_COUNT - FREQUENCY_COUNT_OLD - 1) As String
        public string[] DISP_LC_E = new string[(long)FREQUENCY_TYPE.FREQUENCY_COUNT - (long)FREQUENCY_TYPE.FREQUENCY_COUNT_OLD - 1];

        public const string DISP_LC_C_1F = "B1";        //'L1
        public const string DISP_LC_C_2F = "B1+B2";     //'L1+L2
        public const string DISP_LC_C_L1L5 = "B1";      //'L1+L5
        public const string DISP_LC_C_3F = "B1+B2";     //'L1+L2+L5
        public const string DISP_LC_C_IFMW = "IF&MW";   //'iono-free & Melbourne-Wuebbena
        //  Public DISP_LC_C(FREQUENCY_COUNT - FREQUENCY_COUNT_OLD - 1) As String
        public string[] DISP_LC_C = new string[(long)FREQUENCY_TYPE.FREQUENCY_COUNT - (long)FREQUENCY_TYPE.FREQUENCY_COUNT_OLD - 1];
        //'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

        //'使用した周波数表示文字列。
        public const string DISP_FREQUENCY_L1 = "L1";       //'L1。
        public const string DISP_FREQUENCY_L2 = "L2";       //'L2｡
        public const string DISP_FREQUENCY_L1L2 = "L1,L2";  //'L1とL2の両方｡
        public const string DISP_FREQUENCY_L5 = "L1,L2";    //'L1とL2を組み合わせたワイドレーン (Lw)。
        public const string DISP_FREQUENCY_L3 = "L1,L2";    //'L1とL2を組み合わせた電離層補正 (lc)。
        public const string DISP_FREQUENCY_L5L3 = "L1,L2";  //'ワイドレーンと電離層補正の両方｡
        public const string DISP_FREQUENCY_AUTO = "ERR";    //'自動選択｡
        public const string DISP_FREQUENCY_LC_NONE = "なし";//'搬送波位相なし (PPS内部変数)。
        //'2017/07/13 NS6000対応。'''''''''''''''''''''''''''''''''''''''''''''''''''''''
        public const string DISP_FREQUENCY_1F = "L1";       //'L1
        public const string DISP_FREQUENCY_2F = "L1,L2";    //'L1+L2
        public const string DISP_FREQUENCY_L1L5 = "L1,L5";  //'L1+L5
        public const string DISP_FREQUENCY_3F = "L1,L2,L5"; //'L1+L2+L5
        public const string DISP_FREQUENCY_IFMW = "L1,L2";  //'iono-free & Melbourne-Wuebbena
        //'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        //  Public DISP_FREQUENCY(FREQUENCY_COUNT - 1) As String

        //'2017/07/26 NS6000対応。'''''''''''''''''''''''''''''''''''''''''''''''''''''''
        public const string DISP_FREQUENCY_R_1F = "G1";       //'L1
        public const string DISP_FREQUENCY_R_2F = "G1,G2";    //'L1+L2
        public const string DISP_FREQUENCY_R_L1L5 = "G1";     //'L1+L5
        public const string DISP_FREQUENCY_R_3F = "G1,G2";    //'L1+L2+L5
        public const string DISP_FREQUENCY_R_IFMW = "G1,G2";  //'iono-free & Melbourne-Wuebbena
        //  Public DISP_FREQUENCY_R(FREQUENCY_COUNT - FREQUENCY_COUNT_OLD - 1) As String
        public const string DISP_FREQUENCY_E_1F = "E1";       //'L1
        public const string DISP_FREQUENCY_E_2F = "E1";       //'L1+L2
        public const string DISP_FREQUENCY_E_L1L5 = "E1,E5";  //'L1+L5
        public const string DISP_FREQUENCY_E_3F = "E1,E5";    //'L1+L2+L5
        public const string DISP_FREQUENCY_E_IFMW = "E1,E5";  //'iono-free & Melbourne-Wuebbena
        //  Public DISP_FREQUENCY_E(FREQUENCY_COUNT - FREQUENCY_COUNT_OLD - 1) As String
        public const string DISP_FREQUENCY_C_1F = "B1";       //'L1
        public const string DISP_FREQUENCY_C_2F = "B1,B2";    //'L1+L2
        public const string DISP_FREQUENCY_C_L1L5 = "B1";     //'L1+L5
        public const string DISP_FREQUENCY_C_3F = "B1,B2";    //'L1+L2+L5
        public const string DISP_FREQUENCY_C_IFMW = "B1,B2";  //'iono-free & Melbourne-Wuebbena
        //  Public DISP_FREQUENCY_C(FREQUENCY_COUNT - FREQUENCY_COUNT_OLD - 1) As String
        //'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

        //'基線解析モード表示文字列。
        public const string DISP_SOLVEMODE_STATIC = "静止";       //'静止。
        public const string DISP_SOLVEMODE_KINEMATICS = "キネマティック";       //'キネマティック。
        //  Public DISP_SOLVEMODE(SOLVEMODE_COUNT) As String

        //'対流圏モデル表示文字列。
        public const string DISP_TROPOSHERE_SAASRTAMOINEN = "Saastamoinen";       //'ザースタモイネン。
        public const string DISP_TROPOSHERE_MARINI = "Marini";    //'マリニ。
        public const string DISP_TROPOSHERE_HOPFIELD = "Hopfield";//'ホップフィールド。
        public const string DISP_TROPOSHERE_UNB3M = "UNB3M";      //'UNB3M。
        public const string DISP_TROPOSHERE_NONE = "なし";        //'モデルなし。
        //  Public DISP_TROPOSHERE(8) As String

        //'対流圏のマッピング表示文字列。
        public const string DISP_TROPMAP_COSZ = "COS(ZENITH ANGLE)";
        public const string DISP_TROPMAP_DRY_NIELL = "乾燥地域ニール";
        public const string DISP_TROPMAP_WET_NIELL = "高湿度地域ニール";
        public const string DISP_TROPMAP_HOPF = "ホップフィールド";
        public const string DISP_TROPMAP_NONE = "なし";
        //  Public DISP_TROPMAP(TROPMAP_COUNT - 1) As String

        public const long PPS_MAXPRN = 32;       //'最大衛星数。旧PPS。
        public const long PPS_MAXPRN_SET = 32;       //'設定最大衛星数。GPS、GLONASSのそれぞれの最大数。
        //'2017/06/13 NS6000対応。'''''''''''''''''''''''''''''''''''''''''''''''''''''''
        //'public const long PPS_MAXPRN_CAPA = 64 '保持最大衛星数。GPSは1～32、GLONASSは38～61なので、きりの良い所で64にする。
        //'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        public const long PPS_MAXPRN_CAPA = 256;        //'保持最大衛星数。GPSは1～32、GLONASSは38～63。1～64をGPS、GLONASSとして、+ 8(QZSS 65～72) + 40(Galileo 73～112) + 40(BeiDou 113～152) + 60(SBAS 153～212) + 44(その他 213～256)＝256
        public const long PPS_MAXPRN_GPS = 32;          //'最大衛星数、GPS。
        public const long PPS_MAXPRN_QZSS = 8;          //'最大衛星数、QZSS。
        public const long PPS_MAXPRN_GLONASS = 26;      //'最大衛星数、GLONASS。
        public const long PPS_MAXPRN_GALILEO = 36;      //'最大衛星数、Galileo。 '2018/08/21 Hitz H.Nakamura PPSの変更に伴い 30 > 36
        public const long PPS_MAXPRN_COMPASS = 37;      //'最大衛星数、BeiDou。  '2018/08/21 Hitz H.Nakamura PPSの変更に伴い 35 > 37
        //'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        public const long PPS_MAXPRN2 = 89;             //'最大衛星数。PPSで扱う最大数。
        public const long PPS_MAXPRN3 = PPS_MAXPRN_GPS + PPS_MAXPRN_QZSS + PPS_MAXPRN_GLONASS + PPS_MAXPRN_GALILEO + PPS_MAXPRN_COMPASS; //'最大衛星数。PPSで扱う最大数。

        //'使用衛星ビットフラグ。
        //  public EXCLUDE_SV(PPS_MAXPRN_SET - 1) As Long

        //'初期値。
        public const long DEFAULT_SOLVEMODE = (long)SOLVEMODE_TYPE.SOLVEMODE_STATIC;     //'解析モード。
        public const long DEFAULT_TROPOSHERE_OLD = (long)TROPOSHERE_TYPE.TROPOSHERE_SAASRTAMOINEN; //'対流圏モデル。
        public const long DEFAULT_TROPOSHERE = (long)TROPOSHERE_TYPE.TROPOSHERE_UNB3M;    //'対流圏モデル。
        public const double DEFAULT_TEMPERATURE = 20;       //'気温(℃)。
        public const double DEFAULT_PRESSURE = 1013;        //'気圧(hPa)。
        public const double DEFAULT_HUMIDITY = 50;          //'湿度(％)。
        public const bool DEFAULT_TROPEST = false;          //'対流圏遅延評価フラグ。
        public const long DEFAULT_TROPMAP = (long)TROPMAP_TYPE.TROPMAP_COSZ;   //'対流圏のマッピング。
        public const double DEFAULT_MINWINDOWLENGTH = 1;    //'間隔。
        public const double DEFAULT_PRIORICONSTRAINT = 0.05;    //'初期値。
        public const double DEFAULT_RELATIVECONSTRAINT = 0;      //'相対値。
        public const double DEFAULT_MAXRESIDL1 = 0.05;      //'最大残差L1。
        public const double DEFAULT_MAXRESIDL2 = 0.05;      //'最大残差L2。
        public const double DEFAULT_MAXRESIDL3 = 0.03;      //'最大残差L3。
        public const double DEFAULT_MAXRESIDL5 = 0.1;       //'最大残差L5。
        public const double DEFAULT_MAXRESIDCODE = 10;      //'最大残差(Code)。
        public const double DEFAULT_MAXRESIDPHASE = 0.15;   //'最大残差(Phase)。
        public const double DEFAULT_MINRATIO = 1.5;         //'最小バイアス決定比。
        public const double DEFAULT_MINPS = 0.9999;         //'最小 probability of success。
        //'2018/07/17 H.Nakamura 電離層遅延推定パラメータの追加にともない大気遅延も選択できるようにする。'''''''''''''''''''''''''''''
        //''2016/02/06 H.Nakamura 大気遅延は推定しない。'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        //''public const double DEFAULT_TROPOAPRSIG = 0.1 '対流圏遅延推定に与える初期シグマ。
        //''public const double DEFAULT_TROPONOISE = 0.00001 '対流圏遅延推定に与える毎エポックの分散。
        //'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        //'public const double DEFAULT_TROPOAPRSIG = 0 '対流圏遅延推定に与える初期シグマ。
        //'public const double DEFAULT_TROPONOISE = 0 '対流圏遅延推定に与える毎エポックの分散。
        //'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        public const double DEFAULT_TROPOAPRSIG = 0.1;    //'対流圏遅延推定に与える初期シグマ。
        public const double DEFAULT_TROPONOISE = 0.00001; //'対流圏遅延推定に与える毎エポックの分散。
        //''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

        //'2018/07/17 H.Nakamura 電離層遅延推定パラメータの追加。'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        public const double DEFAULT_IONOAPRSIG = 0.02;    //'電離層遅延推定に与える初期シグマ。
                                                          //''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

        //  Public BASE_LINE_ANALYSER As BaseLineAnalyser '基線解析器。


        //'初期化。
        public void InitializeBaseLineAnalyser()
        {


        }
#if !DEBUG
'初期化。
Public Sub InitializeBaseLineAnalyser()
    '解の種類。
    DISP_ANALYSIS(ANALYSIS_STATUS_FLOAT) = DISP_ANALYSIS_FLOAT
    DISP_ANALYSIS(ANALYSIS_STATUS_FIX) = DISP_ANALYSIS_FIX
    DISP_ANALYSIS(ANALYSIS_STATUS_FAILED) = DISP_ANALYSIS_FAILED
    DISP_ANALYSIS(ANALYSIS_STATUS_NOT) = DISP_ANALYSIS_NOT
    '軌道暦。
    DISP_ORBIT(ORBIT_BROADCAST) = DISP_ORBIT_BROADCAST
    DISP_ORBIT(ORBIT_PRECISE) = DISP_ORBIT_PRECISE
    '周波数。
    DISP_LC(FREQUENCY_L1) = DISP_LC_L1
    DISP_LC(FREQUENCY_L2) = DISP_LC_L2
    DISP_LC(FREQUENCY_L1L2) = DISP_LC_L1L2
    DISP_LC(FREQUENCY_L5) = DISP_LC_L5
    DISP_LC(FREQUENCY_L3) = DISP_LC_L3
    DISP_LC(FREQUENCY_L5L3) = DISP_LC_L5L3
    DISP_LC(FREQUENCY_AUTO) = DISP_LC_AUTO
    DISP_LC(FREQUENCY_LC_NONE) = DISP_LC_NONE
    '2017/07/13 NS6000対応。'''''''''''''''''''''''''''''''''''''''''''''''''''''''
    DISP_LC(FREQUENCY_1F) = DISP_LC_1F
    DISP_LC(FREQUENCY_2F) = DISP_LC_2F
    DISP_LC(FREQUENCY_L1L5) = DISP_LC_L1L5
    DISP_LC(FREQUENCY_3F) = DISP_LC_3F
    DISP_LC(FREQUENCY_IFMW) = DISP_LC_IFMW

    DISP_LC_R(FREQUENCY_1F - FREQUENCY_COUNT_OLD) = DISP_LC_R_1F
    DISP_LC_R(FREQUENCY_2F - FREQUENCY_COUNT_OLD) = DISP_LC_R_2F
    DISP_LC_R(FREQUENCY_L1L5 - FREQUENCY_COUNT_OLD) = DISP_LC_R_L1L5
    DISP_LC_R(FREQUENCY_3F - FREQUENCY_COUNT_OLD) = DISP_LC_R_3F
    DISP_LC_R(FREQUENCY_IFMW - FREQUENCY_COUNT_OLD) = DISP_LC_R_IFMW
    DISP_LC_E(FREQUENCY_1F - FREQUENCY_COUNT_OLD) = DISP_LC_E_1F
    DISP_LC_E(FREQUENCY_2F - FREQUENCY_COUNT_OLD) = DISP_LC_E_2F
    DISP_LC_E(FREQUENCY_L1L5 - FREQUENCY_COUNT_OLD) = DISP_LC_E_L1L5
    DISP_LC_E(FREQUENCY_3F - FREQUENCY_COUNT_OLD) = DISP_LC_E_3F
    DISP_LC_E(FREQUENCY_IFMW - FREQUENCY_COUNT_OLD) = DISP_LC_E_IFMW
    DISP_LC_C(FREQUENCY_1F - FREQUENCY_COUNT_OLD) = DISP_LC_C_1F
    DISP_LC_C(FREQUENCY_2F - FREQUENCY_COUNT_OLD) = DISP_LC_C_2F
    DISP_LC_C(FREQUENCY_L1L5 - FREQUENCY_COUNT_OLD) = DISP_LC_C_L1L5
    DISP_LC_C(FREQUENCY_3F - FREQUENCY_COUNT_OLD) = DISP_LC_C_3F
    DISP_LC_C(FREQUENCY_IFMW - FREQUENCY_COUNT_OLD) = DISP_LC_C_IFMW
    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    
    '使用した周波数。
    DISP_FREQUENCY(FREQUENCY_L1) = DISP_FREQUENCY_L1
    DISP_FREQUENCY(FREQUENCY_L2) = DISP_FREQUENCY_L2
    DISP_FREQUENCY(FREQUENCY_L1L2) = DISP_FREQUENCY_L1L2
    DISP_FREQUENCY(FREQUENCY_L5) = DISP_FREQUENCY_L5
    DISP_FREQUENCY(FREQUENCY_L3) = DISP_FREQUENCY_L3
    DISP_FREQUENCY(FREQUENCY_L5L3) = DISP_FREQUENCY_L5L3
    DISP_FREQUENCY(FREQUENCY_AUTO) = DISP_FREQUENCY_AUTO
    DISP_FREQUENCY(FREQUENCY_LC_NONE) = DISP_FREQUENCY_LC_NONE
    '2017/07/13 NS6000対応。'''''''''''''''''''''''''''''''''''''''''''''''''''''''
    DISP_FREQUENCY(FREQUENCY_1F) = DISP_FREQUENCY_1F
    DISP_FREQUENCY(FREQUENCY_2F) = DISP_FREQUENCY_2F
    DISP_FREQUENCY(FREQUENCY_L1L5) = DISP_FREQUENCY_L1L5
    DISP_FREQUENCY(FREQUENCY_3F) = DISP_FREQUENCY_3F
    DISP_FREQUENCY(FREQUENCY_IFMW) = DISP_FREQUENCY_IFMW

    DISP_FREQUENCY_R(FREQUENCY_1F - FREQUENCY_COUNT_OLD) = DISP_FREQUENCY_R_1F
    DISP_FREQUENCY_R(FREQUENCY_2F - FREQUENCY_COUNT_OLD) = DISP_FREQUENCY_R_2F
    DISP_FREQUENCY_R(FREQUENCY_L1L5 - FREQUENCY_COUNT_OLD) = DISP_FREQUENCY_R_L1L5
    DISP_FREQUENCY_R(FREQUENCY_3F - FREQUENCY_COUNT_OLD) = DISP_FREQUENCY_R_3F
    DISP_FREQUENCY_R(FREQUENCY_IFMW - FREQUENCY_COUNT_OLD) = DISP_FREQUENCY_R_IFMW
    DISP_FREQUENCY_E(FREQUENCY_1F - FREQUENCY_COUNT_OLD) = DISP_FREQUENCY_E_1F
    DISP_FREQUENCY_E(FREQUENCY_2F - FREQUENCY_COUNT_OLD) = DISP_FREQUENCY_E_2F
    DISP_FREQUENCY_E(FREQUENCY_L1L5 - FREQUENCY_COUNT_OLD) = DISP_FREQUENCY_E_L1L5
    DISP_FREQUENCY_E(FREQUENCY_3F - FREQUENCY_COUNT_OLD) = DISP_FREQUENCY_E_3F
    DISP_FREQUENCY_E(FREQUENCY_IFMW - FREQUENCY_COUNT_OLD) = DISP_FREQUENCY_E_IFMW
    DISP_FREQUENCY_C(FREQUENCY_1F - FREQUENCY_COUNT_OLD) = DISP_FREQUENCY_C_1F
    DISP_FREQUENCY_C(FREQUENCY_2F - FREQUENCY_COUNT_OLD) = DISP_FREQUENCY_C_2F
    DISP_FREQUENCY_C(FREQUENCY_L1L5 - FREQUENCY_COUNT_OLD) = DISP_FREQUENCY_C_L1L5
    DISP_FREQUENCY_C(FREQUENCY_3F - FREQUENCY_COUNT_OLD) = DISP_FREQUENCY_C_3F
    DISP_FREQUENCY_C(FREQUENCY_IFMW - FREQUENCY_COUNT_OLD) = DISP_FREQUENCY_C_IFMW
    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    '基線解析モード。
    DISP_SOLVEMODE(SOLVEMODE_STATIC) = DISP_SOLVEMODE_STATIC
    DISP_SOLVEMODE(SOLVEMODE_KINEMATICS) = DISP_SOLVEMODE_KINEMATICS
    '対流圏モデル。
    DISP_TROPOSHERE(TROPOSHERE_SAASRTAMOINEN_OLD) = DISP_TROPOSHERE_SAASRTAMOINEN
    DISP_TROPOSHERE(TROPOSHERE_MARINI_OLD) = DISP_TROPOSHERE_MARINI
    DISP_TROPOSHERE(TROPOSHERE_HOPFIELD_OLD) = DISP_TROPOSHERE_HOPFIELD
    DISP_TROPOSHERE(TROPOSHERE_NONE_OLD) = DISP_TROPOSHERE_NONE
    DISP_TROPOSHERE(TROPOSHERE_SAASRTAMOINEN) = DISP_TROPOSHERE_SAASRTAMOINEN
    DISP_TROPOSHERE(TROPOSHERE_MARINI) = DISP_TROPOSHERE_MARINI
    DISP_TROPOSHERE(TROPOSHERE_HOPFIELD) = DISP_TROPOSHERE_HOPFIELD
    DISP_TROPOSHERE(TROPOSHERE_UNB3M) = DISP_TROPOSHERE_UNB3M
    DISP_TROPOSHERE(TROPOSHERE_NONE) = DISP_TROPOSHERE_NONE
    '対流圏のマッピング。
    DISP_TROPMAP(TROPMAP_COSZ) = DISP_TROPMAP_COSZ
    DISP_TROPMAP(TROPMAP_DRY_NIELL) = DISP_TROPMAP_DRY_NIELL
    DISP_TROPMAP(TROPMAP_WET_NIELL) = DISP_TROPMAP_WET_NIELL
    DISP_TROPMAP(TROPMAP_HOPF) = DISP_TROPMAP_HOPF
    DISP_TROPMAP(TROPMAP_NONE) = DISP_TROPMAP_NONE
    '楕円体モデル。
    DISP_ELLIPSE_MODEL(ELLIPSE_MODEL_GRS80) = DISP_ELLIPSE_MODEL_GRS80
    DISP_ELLIPSE_MODEL(ELLIPSE_MODEL_WGS84) = DISP_ELLIPSE_MODEL_WGS84
    '使用衛星ビットフラグ。
    EXCLUDE_SV(0) = &H1                 'GPS01。
    EXCLUDE_SV(1) = &H2                 'GPS02。
    EXCLUDE_SV(2) = &H4                 'GPS03。
    EXCLUDE_SV(3) = &H8                 'GPS04。
    EXCLUDE_SV(4) = &H10                'GPS05。
    EXCLUDE_SV(5) = &H20                'GPS06。
    EXCLUDE_SV(6) = &H40                'GPS07。
    EXCLUDE_SV(7) = &H80                'GPS08。
    EXCLUDE_SV(8) = &H100               'GPS09。
    EXCLUDE_SV(9) = &H200               'GPS10。
    EXCLUDE_SV(10) = &H400              'GPS11。
    EXCLUDE_SV(11) = &H800              'GPS12。
    EXCLUDE_SV(12) = &H1000             'GPS13。
    EXCLUDE_SV(13) = &H2000             'GPS14。
    EXCLUDE_SV(14) = &H4000             'GPS15。
    EXCLUDE_SV(15) = &H8000&            'GPS16。
    EXCLUDE_SV(16) = &H10000            'GPS17。
    EXCLUDE_SV(17) = &H20000            'GPS18。
    EXCLUDE_SV(18) = &H40000            'GPS19。
    EXCLUDE_SV(19) = &H80000            'GPS20。
    EXCLUDE_SV(20) = &H100000           'GPS21。
    EXCLUDE_SV(21) = &H200000           'GPS22。
    EXCLUDE_SV(22) = &H400000           'GPS23。
    EXCLUDE_SV(23) = &H800000           'GPS24。
    EXCLUDE_SV(24) = &H1000000          'GPS25。
    EXCLUDE_SV(25) = &H2000000          'GPS26。
    EXCLUDE_SV(26) = &H4000000          'GPS27。
    EXCLUDE_SV(27) = &H8000000          'GPS28。
    EXCLUDE_SV(28) = &H10000000         'GPS29。
    EXCLUDE_SV(29) = &H20000000         'GPS30。
    EXCLUDE_SV(30) = &H40000000         'GPS31。
    EXCLUDE_SV(31) = &H80000000         'GPS32。
End Sub
#endif

#if !DEBUG
'解析エンジン進捗コールバック関数。
'
'戻り値：
'処理続行の場合 0 を返す。
'それ以外の場合 1 を返す。
Public Function ProgressCall() As Long
    On Error GoTo ErrorHandler
    'DLLの構造体のアライメントのせいか引き数を受け取れない。
    BASE_LINE_ANALYSER.ProgressInterface.CurPos(0) = (BASE_LINE_ANALYSER.Progress + BASE_LINE_ANALYSER.Index) / BASE_LINE_ANALYSER.Count* 100
    BASE_LINE_ANALYSER.ProgressInterface.CurPos(1) = BASE_LINE_ANALYSER.Progress* 100
    If BASE_LINE_ANALYSER.ProgressInterface.Cancel Then
        BASE_LINE_ANALYSER.Cancel = True
        ProgressCall = 1
    Else
        ProgressCall = 0
    End If
    Exit Function
ErrorHandler:
    ProgressCall = 1
End Function

#endif






    }
}
