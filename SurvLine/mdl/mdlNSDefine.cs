using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SurvLine.mdl
{
    public class MdlNSDefine
    {

        //'定数
        //public const DateTime MIN_TIME = (DateTime)"1899/12/30 00:00:00";     //'アプリ最小時刻。
        public const string MIN_TIME = "1899/12/30 00:00:00";     //'アプリ最小時刻。

        public const long DEF_LEAP_SEC = 16;                        //'デフォルトのうるう秒。

        public const long DOCUMENT_FILE_VERSION = 9700;              //'ドキュメントファイルバージョン。
                                                  //'7800 2008/10/11 精度管理表（別表）にパラメータを追加 NS-Network
                                                  //'7800 2007/3/22 座標計算簿２を追加　NGS Yamada NS-Note
                                                  //'7800 偏心設定に「手入力」を追加　2007/3/15 NGS Yamada NS-Survey
                                                  //'7900 2007/8/14 補正量を追加　NGS Yamada NS-Note
                                                  //'7900 帳票「座標一覧表」を追加　　2007/7/18 NGS Yamada NS-Survey
                                                  //'8000 2008/10/16 基準点フラグ、ローカル座標追加　2008/10/16 NGS Yamada NS-Note
                                                  //'8202 2009/11 H.Nakamura
                                                  //'8300 2011/08 GLONASS対応。
                                                  //'8400 2012/05 重み付け補間対応。
                                                  //'8500 2012/06/05 平均座標変換計算簿の追加。
                                                  //'8600 2013/11/07 静止測量のGLONASS対応。
                                                  //'8700 2015/12/16 最小 probability of success 追加。
                                                  //'8800 2016/01/07 電子基準点のみを既知点とした基準点測量を追加。
                                                  //'8900 2016/01/08 点検計算結果を追加。
                                                  //'9000 2016/02/06 大気遅延を推定しない。
                                                  //'9100 2017/06/05 NS6000対応。
                                                  //'9200 2018/07/17 電離層遅延推定パラメータの追加。
                                                  //'9300 2018/08/21 最大衛星数の拡張(E 30 > 36、C 35 > 37)
                                                  //'9400 2022/02/07 衛星信号の追加。
                                                  //'9500 2022/03/10 衛星情報の OBS TYPES は信号が無くても記載されていたので記載しないように修正。すでに読み込み済みのデータはあらためて評価する。
                                                  //'9600 2022/10/20 精度管理表を別表へ分けない、を追加。
                                                  //'9700 2023/06/27 楕円体高の閉合差を追加。平均成果表を追加。前半後半較差の追加。
        public const long FILEIOMEMSIZE = 16384;  //'ファイル読み書きバッファサイズ。

        //'座標系原点座標
        public const double COORDORIGIN_LAT_01 = 33;
        public const double COORDORIGIN_LON_01 = 129d + (30d / 60d);         //129 + 30 / 60#
        public const double COORDORIGIN_LAT_02 = 33;
        public const double COORDORIGIN_LON_02 = 131;
        public const double COORDORIGIN_LAT_03 = 36;
        public const double COORDORIGIN_LON_03 = 132d + (10d / 60d);         //132 + 10 / 60#
        public const double COORDORIGIN_LAT_04 = 33;
        public const double COORDORIGIN_LON_04 = 133d + (30d / 60d);         //133 + 30 / 60#
        public const double COORDORIGIN_LAT_05 = 36;
        public const double COORDORIGIN_LON_05 = 134d + (20d / 60d);
        public const double COORDORIGIN_LAT_06 = 36;
        public const double COORDORIGIN_LON_06 = 136;
        public const double COORDORIGIN_LAT_07 = 36;
        public const double COORDORIGIN_LON_07 = 137d + (10d / 60d);
        public const double COORDORIGIN_LAT_08 = 36;
        public const double COORDORIGIN_LON_08 = 138d + (30d / 60d);
        public const double COORDORIGIN_LAT_09 = 36;
        public const double COORDORIGIN_LON_09 = 139d + (50d / 60d);
        public const double COORDORIGIN_LAT_10 = 40;
        public const double COORDORIGIN_LON_10 = 140d + (50d / 60d);
        public const double COORDORIGIN_LAT_11 = 44;
        public const double COORDORIGIN_LON_11 = 140 + (15d / 60d);
        public const double COORDORIGIN_LAT_12 = 44;
        public const double COORDORIGIN_LON_12 = 142 + (15d / 60d);
        public const double COORDORIGIN_LAT_13 = 44;
        public const double COORDORIGIN_LON_13 = 144d + (15d / 60d);
        public const double COORDORIGIN_LAT_14 = 26;
        public const double COORDORIGIN_LON_14 = 142;
        public const double COORDORIGIN_LAT_15 = 26;
        public const double COORDORIGIN_LON_15 = 127d + (30d / 60d);
        public const double COORDORIGIN_LAT_16 = 26;
        public const double COORDORIGIN_LON_16 = 124;
        public const double COORDORIGIN_LAT_17 = 26;
        public const double COORDORIGIN_LON_17 = 131;
        public const double COORDORIGIN_LAT_18 = 20;
        public const double COORDORIGIN_LON_18 = 136;
        public const double COORDORIGIN_LAT_19 = 26;
        public const double COORDORIGIN_LON_19 = 154;

        //'エラー
        public const long vbObjectError = 0x01;
        public const long ERR_FATAL = vbObjectError | 0x200 | 0x1000;                       //'致命的エラー。
        public const long ERR_RESUME = vbObjectError | 0x200 | 0x100;                       //'継続指示エラー。
        public const long ERR_NOINVERSE = vbObjectError | 0x200 | 0x1003;                   //'逆行列無し。
        public const long ERR_MATRIX = vbObjectError | 0x200 | 0x1004;                      //'行列エラー。
        public const long ERR_FILE = vbObjectError | 0x200 | 0x1005;                        //'ファイルエラー。
        public const long ERR_RINEX = vbObjectError | 0x200 | 0x1006;                       //'RINEX変換エラー。
        public const long ERR_PROCESS = vbObjectError | 0x200 | 0x1007;                     //'処理エラー。
        public const long ERR_RESULTDATAFILE_RECORDOVER = vbObjectError | 0x200 | 0x1100;   //'成果数値データファイルエラー、レコード長オーバー。
        public const long ERR_RESULTDATAFILE_FILEOVER = vbObjectError | 0x200 | 0x1101;     //'成果数値データファイルエラー、ファイル長オーバー。
        public const long ERR_RESULTDATAFILE_NUMBERSTRING = vbObjectError | 0x200 | 0x1102; //'成果数値データファイルエラー、観測点番号文字列混入。
        public const long ERR_RESULTDATAFILE_NUMBEROVER = vbObjectError | 0x200 | 0x1103;   //'成果数値データファイルエラー、観測点番号文字数オーバー。
        public const long ERR_RESULTDATAFILE_NAMEOVER = vbObjectError | 0x200 | 0x1104;     //'成果数値データファイルエラー、観測点名称文字数オーバー。
        public const long ERR_RESULTDATAFILE_RANKEMPTY = vbObjectError | 0x200 | 0x1105;    //'成果数値データファイルエラー、測量等級空。
        public const long ERR_RESULTDATAFILE_SIGNNUMBEREMPTY = vbObjectError | 0x200 | 0x1106;  //'成果数値データファイルエラー、標識番号空。
        public const long ERR_RESULTDATAFILE_SIGNNUMBERSTRING = vbObjectError | 0x200 | 0x1107; //'成果数値データファイルエラー、標識番号不正文字。
        public const long ERR_RESULTDATAFILE_SIGNNUMBEROVER = vbObjectError | 0x200 | 0x1108;   //'成果数値データファイルエラー、標識番号文字数オーバー。
        public const long ERR_RESULTDATAFILE_RECOGNITIONCOUNT = vbObjectError | 0x200 | 0x1109; //'成果数値データファイルエラー、視準点数オーバー。
        public const long ERR_RESULTDATAFILE_BURYEMPTY = vbObjectError | 0x200 | 0x110A;        //'成果数値データファイルエラー、埋標型式空。
        public const long ERR_RESULTDATAFILE_ANTHEIGHTEMPTY = vbObjectError | 0x200 | 0x110B;   //'成果数値データファイルエラー、アンテナ高空。
        public const long ERR_CASFILE_OPEN = vbObjectError | 0x200 | 0x1200;                    //'CSV、APA-SIMA、SIMAファイルエラー、ファイルオープン失敗。
        public const long ERR_CASFILE_WRITE = vbObjectError | 0x200 | 0x1201;                   //'CSV、APA-SIMA、SIMAファイルエラー、ファイル書き込み失敗。


        //'レジストリキー
        public const string REG_KEY_ROOT = "NGS";           //'ルートセクション。
        public const string REG_KEY_WINLEFT = "Left";       //'ウィンドウ左位置。
        public const string REG_KEY_WINTOP = "Top";         //'ウィンドウ上位置。
        public const string REG_KEY_WINWIDTH = "Width";     //'ウィンドウ幅。
        public const string REG_KEY_WINHEIGHT = "Height";   //'ウィンドウ高さ。
        public const string REG_KEY_WINSTATE = "State";     //'ウィンドウ状態。
        public const string REG_KEY_PANEPLOT = "PanePlot";  //'プロットペインの表示状態。
        public const string REG_KEY_PANEOUTPUT = "PaneOutput";      //'アウトプットペインの表示状態。
        public const string REG_KEY_PANESESSION = "PaneSession";    //'セッションリストペインの表示状態。
        public const string REG_KEY_SPLITHORI = "SplitHori";        //'水平スプリッターの位置。
        public const string REG_KEY_SPLITVERT = "SplitVert";        //'垂直スプリッターの位置。
        public const string REG_KEY_SPLITSESSION = "SplitSession";  //'セッションリストスプリッターの位置。
        public const string REG_KEY_TOOLBAR = "ToolBar";            //'ツールバー状態。
        public const string REG_KEY_FOLDERPATH = "FolderPath";      //'フォルダーパス。
        public const string REG_KEY_WIN_MAIN = "Main";              //'メインウィンドウ名。
        public const string REG_KEY_WIN_ACCOUNT = "Account";        //'帳票ウィンドウ名。
        public const string REG_KEY_WIN_ANGLEDIFF = "AngleDiff";    //'閉合差ウィンドウ名。
        public const string REG_KEY_TOOLVISIBLE = "ToolVisible";    //'ツールバー表示状態。
        public const string REG_KEY_STATUSVISIBLE = "StatusVisible";//'ステータスバー表示状態。
        public const string REG_KEY_LICENSECODE = "LicenseCode";    //'ライセンスコード。

        //'フォルダ。
        public const string TEMPLATE_PATH = @"\Template\";  //'テンプレートフォルダのパス。
        public const string TEMPORARY_PATH = @"\Temp\";     //'テンポラリフォルダのパス。

        //'保存iniファイル。
        public const string PROFILE_SAVE_SEC_ACCOUNT = "ACCOUNT";                       //'帳票。
        public const string PROFILE_SAVE_SEC_GEOIDO = "GEOIDO";                         //'ジオイドオプション。
        public const string PROFILE_SAVE_SEC_TKY = "TKY";                               //'旧日本測地系オプション。
        public const string PROFILE_SAVE_SEC_PATCHJGD = "PATCHJGD";                     //'座標補正(PatchJGD)オプション。※2016/03/15 XEBEC dohki
        public const string PROFILE_SAVE_SEC_SEMIDYNA = "SEMIDYNA";                     //'セミ・ダイナミックオプション。'2009/11 H.Nakamura
        public const string PROFILE_SAVE_SEC_BASELINE = "BASELINE";                     //'基線ベクトル。
        public const string PROFILE_SAVE_SEC_PERMISSIBLE = "PERMISSIBLE";               //'許容値。
        public const string PROFILE_SAVE_SEC_CHECKCONNECT = "CHECKCONNECT";             //'点検計算接続条件。
        public const string PROFILE_SAVE_SEC_DXF = "DXF";                               //'DXFファイル。
        public const string PROFILE_SAVE_SEC_CAS = "CAS";                               //'CASファイル。
        public const string PROFILE_SAVE_SEC_DETAIL = "DETAIL";                         //'詳細設定。2008/10/13 NGS Yamada
        public const string PROFILE_SAVE_KEY_ENABLE = "ENABLE";                         //'有効/無効。
        public const string PROFILE_SAVE_KEY_PATH = "PATH";                             //'パス。
        public const string PROFILE_SAVE_KEY_PAGENUMBERVISIBLE = "PAGENUMBERVISIBLE";   //'ページ番号表示/非表示。
        public const string PROFILE_SAVE_KEY_SOKUCHISEIKA2011 = "SOKUCHISEIKA2011";     //'測地成果2011表示/非表示。'2012/06/04 H.Nakamura 【測地成果2011対応】---'
        public const string PROFILE_SAVE_KEY_MIN = "MIN";                               //'最小。
        public const string PROFILE_SAVE_KEY_MAX = "MAX";                               //'最大。
        public const string PROFILE_SAVE_KEY_DISTANCE = "DISTANCE";                     //'距離。
        public const string PROFILE_SAVE_KEY_RINGΔN = "RINGΔN";                       //'環閉合差ΔN。
        public const string PROFILE_SAVE_KEY_RINGΔE = "RINGΔE";                     //'環閉合差ΔE。
        public const string PROFILE_SAVE_KEY_RINGΔU = "RINGΔU";                     //'環閉合差ΔU。
        public const string PROFILE_SAVE_KEY_BETWEENΔN = "BETWEENΔN";               //'電子基準点間の閉合差ΔN。
        public const string PROFILE_SAVE_KEY_BETWEENΔE = "BETWEENΔE";               //'電子基準点間の閉合差ΔE。
        public const string PROFILE_SAVE_KEY_BETWEENΔU = "BETWEENΔU";               //'電子基準点間の閉合差ΔU。
        public const string PROFILE_SAVE_KEY_OVERLAPΔN = "OVERLAPΔN";               //'重複基線較差ΔN。
        public const string PROFILE_SAVE_KEY_OVERLAPΔE = "OVERLAPΔE";               //'重複基線較差ΔE。
        public const string PROFILE_SAVE_KEY_OVERLAPΔU = "OVERLAPΔU";               //'重複基線較差ΔU。
        public const string PROFILE_SAVE_KEY_CHECKVISIBLE = "CHECKVISIBLE";             //'点検測量表示フラグ。
        public const string PROFILE_SAVE_KEY_CHECKΔN = "CHECKΔN";                   //'点検測量ΔN。
        public const string PROFILE_SAVE_KEY_CHECKΔE = "CHECKΔE";                   //'点検測量ΔE。
        public const string PROFILE_SAVE_KEY_CHECKΔU = "CHECKΔU";                   //'点検測量ΔU。
        public const string PROFILE_SAVE_KEY_ROUND = "ROUND";                           //'丸め方法。
        public const string PROFILE_SAVE_KEY_BASE = "BASE";                             //'基準点。
        public const string PROFILE_SAVE_KEY_TIMEZONE = "TIMEZONE";                     //'時間帯。
        public const string PROFILE_SAVE_KEY_ELLIPSEMODEL = "ELLIPSEMODEL";             //'楕円体モデル。
        public const string PROFILE_SAVE_KEY_ANALYSISMETHOD = "ANALYSISMETHOD";         //'基線解析方法。
        public const string PROFILE_SAVE_KEY_COORDNUM = "COORDNUM";                     //'座標系番号。
        public const string PROFILE_SAVE_KEY_NAME = "NAME";                             //'名称。

        //'2006/10/6 NGS Yamada
        public const string PROFILE_SAVE_SEC_PLOT = "PLOT";                             //'画面プロット。
        public const string PROFILE_SAVE_KEY_POINTLABEL = "POINTLABEL";                 //'プロット画面に表示するラベルの種類。
        public const long PROFILE_SAVE_DEF_POINTLABEL = 0;                              //'プロット画面に表示するラベルの種類。
        //'2006/10/10 NGS Yamada
        public const string PROFILE_SAVE_KEY_DISABLEVECTORVISIBLE = "DISABLEVECTORVISIBLE"; //'「無効」なベクトルをプロット画面に表示するか？
        //'2007/6/20 NGS Yamada
        public const string PROFILE_SAVE_KEY_IMPORTCOMPORT = "IMPORTCOMPORT";           //'受信機からインポートするときのCOMポート
        public const long PROFILE_SAVE_DEF_IMPORTCOMPORT = 0;                           //'デフォルトはCOM1

        //'2007/7/2 NGS Yamada
        public const string PROFILE_SAVE_KEY_IMPORTCOMPORTTYPE = "IMPORTCOMPORTTYPE";   //'受信機からインポートするときのCOMポートの取得方法(True=自動取得、False=手動選択)。

        //'2007/8/10 NGS Yamada
        public const string PROFILE_SAVE_KEY_IMPORTDATASAVE = "IMPORTDATASAVE";         //'受信機からインポート時に観測データを保存する（True=保存する、False=保存しない）2007/8/10 NGS Yamada

        //'2007/8/10 NGS Yamada
        public const string PROFILE_SAVE_KEY_USERDATAPATH = "USERDATAPATH";             //'ユーザデータを管理するフォルダのパス 2008/10/13 NGS Yamada
        //'public const string PROFILE_SAVE_DEF_USERDATAPATH = App.Path & DATA_FOLDER_NAME 'ユーザデータを管理するフォルダのデフォルトパス 2008/10/13 NGS Yamada


        //'定義iniファイル。
        public const string PROFILE_DEF_SEC_IMPORT = "IMPORT";          //'インポート。
        public const string PROFILE_DEF_SEC_ERA = "ERA";                //'年号。
        public const string PROFILE_DEF_SEC_HELP = "HELP";              //'ヘルプ。
        public const string PROFILE_DEF_SEC_GEOIDO = "GEOIDO";          //'ジオイド。
        public const string PROFILE_DEF_KEY_DISTANCE = "DISTANCE";      //'距離。
        public const string PROFILE_DEF_KEY_NAME = "NAME";              //'名前。
        public const string PROFILE_DEF_KEY_BASE = "BASE";              //'基準。
        public const string PROFILE_DEF_KEY_MANUAL = "MANUAL";          //'マニュアル。
        public const string PROFILE_DEF_KEY_INSTALLFOLDER = "INSTALLFOLDER";    //'既定のインストールフォルダー。
        public const string PROFILE_DEF_KEY_SUPPORT = "SUPPORT";        //'サポートサイト。

        //'テキストファイル。
        public const string TXT_FILE_EXT = "txt";       //'拡張子。
        public const string TXT_FILE_FILTER = "テキストファイル (*.txt)|*.txt|すべてのファイル (*.*)|*.*";  //'フィルター。
        //'JOBファイル。
        public const string JOB_FILE_EXT = "job";       //'拡張子。
        public const string JOB_FILE_FILTER = "Geodimeterジョブファイル (*.job)|*.job|すべてのファイル (*.*)|*.*"; //'フィルター。
        //'NVFファイル。
        public const string NVF_FILE_EXT = "nvf";       //'拡張子。
        public const string NVF_FILE_FILTER = "Netsurvベクトルデータファイル (*.nvf)|*.nvf|すべてのファイル (*.*)|*.*";    //'フィルター。
        //'NVBファイル。
        public const string NVB_FILE_EXT = "nvb";       //'拡張子。
        public const string NVB_FILE_FILTER = "Netsurvベクトルバイナリファイル (*.nvb)|*.nvb|すべてのファイル (*.*)|*.*";   //'フィルター。
        //'DATファイル。
        public const string DAT_FILE_EXT = "dat";       //'拡張子。
        public const string DAT_FILE_FILTER = "NetSurvデータファイル (*.dat)|*.dat|すべてのファイル (*.*)|*.*";    //'フィルター。
        //'RINEXファイル。
        public const string RINEX_FILE_EXT = "";        //'拡張子。
        public const string RINEX_FILE_FILTER = "RINEXファイル (*.??o)|*.??o|すべてのファイル (*.*)|*.*";   //'フィルター。
        //'NAVファイル。
        public const string NAV_FILE_EXT = "";          //'拡張子。
        public const string NAV_FILE_FILTER = "衛星軌道情報ファイル (*.??n)|*.??n|すべてのファイル (*.*)|*.*";    //'フィルター。
        //'DXFファイル。
        public const string DXF_FILE_EXT = "dxf";       //'拡張子。
        public const string DXF_FILE_FILTER = "DXF ファイル (*.dxf)|*.dxf|すべてのファイル (*.*)|*.*";  //'フィルター。
        //'旧日本測地系パラメータファイル。
        public const string TKY_FILE_EXT = "par";       //'拡張子。
        public const string TKY_FILE_FILTER = "旧日本測地系パラメータファイル (*.par)|*.par|すべてのファイル (*.*)|*.*";   //'フィルター。
        //'CSVファイル。
        public const string CSV_FILE_EXT = "csv";       //'拡張子。
        public const string CSV_FILE_FILTER = "CSVファイル (*.csv)|*.csv|すべてのファイル (*.*)|*.*";   //'フィルター。
        //'APA-SIMAファイル。
        public const string APA_FILE_EXT = "apa";       //'拡張子。
        public const string APA_FILE_FILTER = "APA-SIMAファイル (*.apa)|*.apa|すべてのファイル (*.*)|*.*";  //'フィルター。
        //'SIMAファイル。
        public const string SIMA_FILE_EXT = "sim";      //'拡張子。
        public const string SIMA_FILE_FILTER = "SIMAファイル (*.sim)|*.sim|すべてのファイル (*.*)|*.*";     //'フィルター。
        //'NS-Surveyエクスポートファイル。
        public const string NSSEXP_FILE_EXT = "nvz";    //'拡張子。
        public const string NSSEXP_FILE_FILTER = "NS-Surveエクスポートファイル (*.nvz)|*.nvz|すべてのファイル (*.*)|*.*";     //'フィルター。
        //'CSVベクトルデータファイル。
        public const string CSVVD_FILE_EXT = "csv";     //'拡張子。
        public const string CSVVD_FILE_FILTER = "CSVベクトルデータファイル (*.csv)|*.csv|すべてのファイル (*.*)|*.*";  //'フィルター。
        //'セミ・ダイナミックパラメータファイル。'2009/11 H.Nakamura
        public const string SEMIDYNA_FILE_EXT = "par";  //'拡張子。
        public const string SEMIDYNA_FILE_FILTER = "セミ・ダイナミックパラメータファイル (*.par)|*.par|すべてのファイル (*.*)|*.*"; //'フィルター。
        //'汎用。
        public const string GENERAL_FILE_FILTER = "すべてのファイル (*.*)|*.*";     //'フィルター。

        //'オブジェクト種別。
        public const long OBJ_TYPE_WORD = 0xFFFF0000;               //'オブジェクト種別のビット。
        public const long OBJ_TYPE_OBSERVATIONPOINT = 0x80000000;   //'観測点。
        public const long OBJ_TYPE_BASELINEVECTOR = 0x40000000;     //'基線ベクトル。
        public const long OBJ_TYPE_OBSERVATIONDISTANCE = 0x20000000;//'観測距離。
        //'観測点種別。
        public const long OBS_TYPE_WORD = 0xFFFF;       //'観測点種別のビット。
        public const long OBS_TYPE_REAL = 0x0;          //'実観測点。
        public const long OBS_TYPE_PARENT = 0x1;        //'親観測点。
        public const long OBS_TYPE_FIXED = 0x2;         //'固定点。
        public const long OBS_TYPE_IMPORT = 0x4;        //'インポート結合点。
        public const long OBS_TYPE_EDIT = 0x8;          //'編集結合点。
        public const long OBS_TYPE_REPRESENT = 0x10;    //'代表観測点。
        public const long OBS_TYPE_KNOWN = 0x20;        //'既知点。
        public const long OBS_TYPE_ACTIVEKNOWN = 0x40;   //'アクティブ既知点。
        public const long OBS_TYPE_CONNECT = 0x80;       //'接合観測点。
        public const long OBS_TYPE_GENUIE = 0x100;      //'本点。
        public const long OBS_TYPE_BASED = 0x200;       //'基準点。
        public const long OBS_TYPE_USEKNOWN = 0x400;    //'使用した既知点。


        //'座標値種別。
        public enum COORDINATE_TYPE
        {
            COORDINATE_XYZ = 0, //'XYZ座標値。
            COORDINATE_FIX,     //'固定座標値
            COORDINATE_KNOWN,   //'既知座標値。
        }

        //'編集コード。
        public enum EDITCODE_STYLE
        {
            EDITCODE_COORD_DMS = 0x1,   // & '緯度経度(度分秒)入力。
            EDITCODE_COORD_JGD = 0x2,   //'平面直角入力。
            EDITCODE_COORD_XYZ = 0x4,   //'地心直角入力。
            EDITCODE_VERT_HEIGHT = 0x8, //'楕円体高入力。
            EDITCODE_VERT_ALT = 0x10,   //'標高入力。
            EDITCODE_COORD_DEG = 0x20,  //'緯度経度(度)入力。

        }

        //'インポートファイル種別。
        public enum IMPORT_TYPE
        {
            IMPORT_TYPE_UNKNOWN = 0,    //'不明。
            IMPORT_TYPE_JOB,            //'JOBファイル。
            IMPORT_TYPE_NVF,            //'NVFファイル。
            IMPORT_TYPE_DAT,            //'DATファイル。
            IMPORT_TYPE_NS3,            //'NS3、DATファイル。
            IMPORT_TYPE_NS5,            //'NS5、DATファイル。
            IMPORT_TYPE_NS51,           //'NS5 BINEX版、DATファイル。
            IMPORT_TYPE_NS6,            //'NS6、DATファイル。
            IMPORT_TYPE_RINEX,          //'RINEXファイル。
            IMPORT_TYPE_PC,             //'PCから観測データファイル。
            IMPORT_TYPE_PDA,            //'PDAから観測データファイル。
            IMPORT_TYPE_ANDROID,        //'スマートフォンから観測データファイル。
            IMPORT_TYPE_NVB,            //'NVBファイル。
            IMPORT_TYPE_CSV,            //'CSVファイル。
            IMPORT_TYPE_DIRECT,         //'受信機からDATファイルをインポート。2007/4/10 NGS Yamada
            IMPORT_TYPE_COUNT,          //'種別数。
        }

        //'スケールモード。
        public enum SCALE_MODE
        {
            SCALE_MODE_NOSCALE,     //'スケール無し。
            SCALE_MODE_ENLARGE,     //'拡大。
            SCALE_MODE_REDUCE,      //'縮小。
            SCALE_MODE_MULTIPLE,    //'等倍スケール。
            SCALE_MODE_RANGE,       //'範囲指定スケール。
            SCALE_MODE_SLIDE,       //'スライド。
            SCALE_MODE_COUNT,       //'種別数。
        }

        //'時間帯。
        public enum TIME_ZONE
        {
            TIME_ZONE_UTC = 0,  //'UTC。
            TIME_ZONE_JST,      //'JST。
            TIME_ZONE_COUNT,    //'数。
        }

        //'楕円体モデル。
        public enum ELLIPSE_MODEL
        {
            ELLIPSE_MODEL_GRS80 = 0,    //'GRS80。
            ELLIPSE_MODEL_WGS84,        //'WGS-84。
            ELLIPSE_MODEL_COUNT,        //'数。
        }

        //'許容範囲丸め方法。
        public enum PERMISSIBLE_ROUND_METHOD
        {
            PERMISSIBLE_ROUND_JPNROUND = 0, //'四捨五入。
            PERMISSIBLE_ROUND_ROUNDDOWN,    //'切り捨て。
        }

        //'楕円体モデル。
        public const string DISP_ELLIPSE_MODEL_GRS80 = "GRS80";         //'GRS80。
        public const string DISP_ELLIPSE_MODEL_WGS84 = "WGS-84";        //'WGS-84。

        //   Public DISP_ELLIPSE_MODEL(ELLIPSE_MODEL_COUNT - 1) As String
        public const string DISP_ELLIPSE_MODEL = DISP_ELLIPSE_MODEL_WGS84;



        //'アンテナiniファイル。
        public const string PROFILE_ANT_FILE = "NSAntenna.ini";         //'アンテナ情報ファイル。
        public const string PROFILE_ANT_SEC_VERSION = "File Version";   //'セクション、File Version。
        public const string PROFILE_ANT_SEC_LIST = "Antenna List";      //'セクション、NetSurv。
        public const string PROFILE_ANT_KEY_JSIMAVER = "JSIMA_File_Version"; //'キー、アンテナ位相特性データバージョン。
        public const string PROFILE_ANT_KEY_JSIMADATE = "JSIMA_LAST_UPDATE"; //'キー、アンテナ位相特性データ更新日。
        public const string PROFILE_ANT_KEY_NSANTVER = "NSANT_FILE_VERSION"; //'キー、アンテナ情報ファイルバージョン。
        public const string PROFILE_ANT_KEY_COUNT = "ANT_COUNT";            //'キー、数。
        public const string PROFILE_ANT_KEY_UNKNOWN = "ANT_UNKNOWN";        //'キー、不明項目番号。
        public const string PROFILE_ANT_KEY_ANT = "ANT";                //'キー、アンテナセクション。
        public const string PROFILE_ANT_KEY_TYPE = "OFFICIAL_NAME";     //'キー、アンテナ種別。
        public const string PROFILE_ANT_KEY_SUB = "ANTENNA_TYPE";       //'キー、アンテナ詳細。
        public const string PROFILE_ANT_KEY_MEASUREMENT = "ANTENNA_MEASUREMENT";    //'キー、測位方法。
        public const string PROFILE_ANT_KEY_MANUFACTURE = "Manufacturer";           //'キー、メーカー。
        public const string PROFILE_ANT_KEY_PPSNAME = "PPS_NAME";           //'PPS名称。
        public const string PROFILE_ANT_KEY_PCVVER = "PCV_VERSION";         //'PCVバージョン番号。

        public const string TRUE_VERTICAL = "TRUE_VERTICAL";    //'位相中心。

        //'受信機iniファイル。
        public const string PROFILE_RCV_FILE = "NSReceiver.ini";        //'受信機情報ファイル。
        public const string PROFILE_RCV_SEC_LIST = "Receiver List";     //'セクション、Receiver List。
        public const string PROFILE_RCV_KEY_COUNT = "RCV_COUNT";        //'キー、数。
        public const string PROFILE_RCV_KEY_UNKNOWN = "RCV_UNKNOWN";    //'キー、不明項目番号。
        public const string PROFILE_RCV_KEY_RCV = "RCV";                //'キー、受信機セクション。
        public const string PROFILE_RCV_KEY_TYPE = "RECEIVER_TYPE";     //'キー、受信機種別。
        public const string PROFILE_RCV_KEY_MANUFACTURE = "Manufacturer";  //'キー、メーカー。

        //'補助iniファイル。
        public const string PROFILE_SUB_FILE = "NSAntRcvConv.ini";      //'補助情報ファイル。
        public const string PROFILE_SUB_SEC_MANUCODETOMANUNAME = "ManuCodeToManuName";      //'セクション、ManuCodeToManuName。
        public const string PROFILE_SUB_SEC_SECV1TOSECV2 = "SectionV1.0ToSectionV2.0";      //'セクション、SectionV1.0ToSectionV2.0。

        public const string KEYPREFIX = "A";    //'リストビューアイテムキープレフィックス。

        //'NVF情報。
        //'public const string NVF_VERSION = "1.3" 'ファイルバージョン。
        public const string NVF_VERSION = "1.4";    //'ファイルバージョン。    '2023/05/19 Hitz H.Nakamura 楕円体高の閉合差を追加。

        //'NVB定義。
        public const long NVB_STARTKEY = 9999;      //'NVBファイル開始番号。
        public const long NVB_VERSION = 300;        //'NVBファイルバージョン。
        public const string NVB_VECTOR_STR = "VECTORSTR";   //'基線ベクトル開始。
        public const string NVB_VECTOR_END = "VECTOREND";   //'基線ベクトル終了。
        public const string NVB_EOF = "EOF";        //'ファイル終端。

        //'VRS印。
        public const string VRS_NUMBER = "VRS";
        public const string VRS_MARK = ",\\";
        public const string VRS_HEAD = @"{VRS_NUMBER}{VRS_MARK}";

        //'ファイル出力時、観測点Noが"VRS"の場合は、「VRS_NO+連番」とする 2006/11/10 NGS Yamada
        public const long VRS_NO = 99000;

        //'定義デフォルト値。
        public const long DEF_PERMISSIBLEROUND = (long)PERMISSIBLE_ROUND_METHOD.PERMISSIBLE_ROUND_ROUNDDOWN;   //'許容範囲丸め方法。

        //'メッセージ。
        public const string NS_MSG_GEOHEIGHT = "ジオイド高の取得に失敗しました。";

        //'セキュリティチェック。
        public const long SRCURITYCHECK_TIMER_INTERVAL = 30000;         //'タイマー間隔。

        [DllImport("GpsConv.dll")]
        public static extern void WGS84xyz_to_WGS84dms(double X, double Y, double Z, ref double Lat, ref double Lon, double Height);
        [DllImport("GpsConv.dll")]
        public static extern void WGS84dms_to_JGDxyz(long zone, double Lat, double Lon, double Height, ref double X, ref double Y, ref double Z);
        [DllImport("GpsConv.dll")]
        public static extern void JGDxyz_to_WGS84dms(long zone, double X, double LoYn, double Z, ref double Lat, ref double Lon, ref double Height);
        [DllImport("GpsConv.dll")]
        public static extern void WGS84dms_to_WGS84xyz(double Lat, double Lon, double Height, ref double X, ref double Y, ref double Z);
        [DllImport("GpsConv.dll")]
        public static extern void WGS84xyz_to_WGS84dms(double X, double Y, double Z, ref double Lat, ref double Lon, ref double Height);
        [DllImport("GpsConv.dll")]
        public static extern void d_to_dms_decimal(double dD, ref long deg, ref long Min, ref double Sec, long deci);
        [DllImport("GpsConv.dll")]
        public static extern double dms_to_d(long deg, long Min, double Sec);
        [DllImport("GpsConv.dll")]
        public static extern long get_geo_height(string Path, double LatJGD, double LonJGD, ref double GeoHeight);


        //==========================================================================================
        /*[VB]
        '*******************************************************************************
        '関数ラップ

        Public Sub WGS84xyz_to_JGDxyz(ByVal zone As Long, ByVal wgs84X As Double, ByVal wgs84Y As Double, ByVal wgs84Z As Double, ByRef X As Double, ByRef Y As Double, ByRef Z As Double)
            Dim nLat As Double
            Dim nLon As Double
            Dim nHeight As Double
            Call WGS84xyz_to_WGS84dms(wgs84X, wgs84Y, wgs84Z, nLat, nLon, nHeight)
            Call WGS84dms_to_JGDxyz(zone, nLat, nLon, nHeight, X, Y, Z)
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '*******************************************************************************
        '関数ラップ

        */
        public void WGS84xyz_to_JGDxyz(long zone, double wgs84X, double wgs84Y, double wgs84Z, ref double X, ref double Y, ref double Z)
        {
            double nLat = 0;
            double nLon = 0;
            double nHeight = 0;
            WGS84xyz_to_WGS84dms(wgs84X, wgs84Y, wgs84Z, ref nLat, ref nLon, nHeight);
            WGS84dms_to_JGDxyz(zone, nLat, nLon, nHeight, ref X, ref Y, ref Z);
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        Public Sub JGDxyz_to_WGS84xyz(ByVal zone As Long, ByVal X As Double, ByVal Y As Double, ByVal Z As Double, ByRef wgs84X As Double, ByRef wgs84Y As Double, ByRef wgs84Z As Double)
            Dim nLat As Double
            Dim nLon As Double
            Dim nHeight As Double
            Call JGDxyz_to_WGS84dms(zone, X, Y, Z, nLat, nLon, nHeight)
            Call WGS84dms_to_WGS84xyz(nLat, nLon, nHeight, wgs84X, wgs84Y, wgs84Z)
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        public void JGDxyz_to_WGS84xyz(long zone, double X, double Y, double Z, ref double wgs84X, ref double wgs84Y, ref double wgs84Z)
        {
            double nLat = 0;
            double nLon = 0;
            double nHeight = 0;
            JGDxyz_to_WGS84dms(zone, X, Y, Z, ref nLat, ref nLon, ref nHeight);
            WGS84dms_to_WGS84xyz(nLat, nLon, nHeight, ref wgs84X, ref wgs84Y, ref wgs84Z);
        }
        //==========================================================================================


    }
}
