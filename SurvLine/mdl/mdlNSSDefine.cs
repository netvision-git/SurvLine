using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurvLine.mdl
{
    public class MdlNSSDefine
    {
        //'セキュリティ。
        public const bool SPRO_ENABLE = false;  //'USBキー認証の有無。

        //'フォルダ。
        public const string DATA_FOLDER_NAME = @"\UserData\";   //'データフォルダ名称。
        public const string OBSPOINT_PATH = @"\ObsPoint\";      //'観測点フォルダ名称。
        public const string ANALYSIS_PATH = @"\Analysis\";      //'解析フォルダ名称。
        public const string PPS_PATH = @"PPS\";                 //'PPSテンポラリフォルダ名称。
        public const string NSCAB_PATH = @"NSCAB\";             //'NSCABテンポラリフォルダ名称。
        public const string IMPORT_PATH = @"Import\";           //'受信機からインポート時のテンポラリフォルダ名称。2007/9/5 NGS Yamada
        public const string PHASECC_FILE_NAME = "NSPHASECC.txt";//'アンテナ位相パターンファイル名。
        public const string DATA_FILE_NAME = "data";            //'データ保存ファイル名。
        public const string PRECISE_FILE_NAME = "Precise.sp3";  //'sp3a精密暦ファイル名。
        public const string EXPORT_TEMP_NAME = "temp.nvz";      //'エクスポートファイルのテンポラリファイルの名前。
        //'2020/10/27 FDC 混合ファイルの分割'''''''''''''''''''''''''''''''''''''''''''''
        public const string OUTPUT_PATH = @"Output\";           //'混合ファイル出力時のテンポラリフォルダ
        //'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

        //'2022/10/12 Hitz H.Nakamura ***********************************************************************
        //'エクスポートでRINEXバージョンの選択を追加。
        public const string EXPORT_PATH = @"Export\";           //'エクスポート作業フォルダー。
        public const string DATTORINEX_PATH = @"DatToRinex\";   //'RINEX変換作業フォルダー。
        //'**************************************************************************************************

        //'レジストリキー
        public const string REG_KEY_WIN_EDITBASELINE = "EditBaseLine"; //'基線ベクトルの編集ウィンドウ名。

        //'保存iniファイル。
        public const string PROFILE_SAVE_EXT = ".ini";                          //'拡張子。
        public const string PROFILE_SAVE_SEC_ANALYSIS = "ANALYSIS";             //'基線解析。
        public const string PROFILE_SAVE_KEY_MINSPAN = "MINSPAN";               //'最小期間。
        public const string PROFILE_SAVE_KEY_SATINFOFILE = "SATINFOFILE";       //'PPS衛星情報ファイル。
        public const string PROFILE_SAVE_KEY_SATINFOTIME = "SATINFOTIME";       //'PPS衛星情報ファイルの設定日時。
        public const long PROFILE_SAVE_DEF_MINSPAN = 180;                       //'最小期間のデフォルト値(秒)。
        public const string PROFILE_SAVE_KEY_NUMBEROFMINSV = "NUMBEROFMINSV";   //'最少衛星数。
        public const string PROFILE_SAVE_KEY_SAMETIMEMIN = "SAMETIMEMIN";       //'最小同時観測時間。
        public const string PROFILE_SAVE_KEY_SATELLITECOUNTMIN = "SATELLITECOUNTMIN";   //'最少共通衛星数。
        public const string PROFILE_SAVE_KEY_DEFLEAPSEC = "DEFLEAPSEC";         //'デフォルトの閏秒。
        public const string PROFILE_SAVE_KEY_IMPORTENABLE = "IMPORTENABLE";     //'有効インポートフラグ。
        public const string PROFILE_SAVE_KEY_WRITEORDER = "WRITEORDER";         //'観測記簿出力順。
        public const string PROFILE_SAVE_KEY_CROSSLINE = "CROSSLINE";           //'抹消線。
        public const long PROFILE_SAVE_DEF_CROSSLINE = 0;                       //'抹消線。
        public const string PROFILE_SAVE_KEY_BASELINECHAINCHECK = "BASELINECHAINCHECK"; //'基線ベクトルつながりチェック。
        public const long PROFILE_SAVE_DEF_BASELINECHAINCHECK = 1;              //'基線ベクトルつながりチェック。

        //'2017/07/11 NS6000対応。'''''''''''''''''''''''''''''''''''''''''''''''''''''''
        public const string PROFILE_SAVE_KEY_ANTEXUSE = "ANTEXUSE";             //'ANTEXファイルを使用する。
        public const string PROFILE_SAVE_KEY_ANTEXPATH = "ANTEXPATH";           //'ANTEXファイル。
        //'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

        //'2022/10/12 Hitz H.Nakamura ***********************************************************************
        //'エクスポートでRINEXバージョンの選択を追加。
        public const string PROFILE_SAVE_SEC_EXPORT = "EXPORT";             //'エクスポート。
        public const string PROFILE_SAVE_KEY_DATTORINEX = "DATTORINEX";     //'RINEX変換バージョン。
        public const long PROFILE_SAVE_DEF_DATTORINEX = 1;                  //'RINEX変換バージョン。
        //'**************************************************************************************************

        //'定義iniファイル。
        public const string PROFILE_DEF_NAME = "NSS.ini";                   //'ファイル名。
        public const long PROFILE_DEF_DEF_DISTANCE = 1000;                  //'距離(㎝)。

        //'インポートファイル。
        //public const string IMPORT_FILE_EXT = "dat" '拡張子。
        //public const string IMPORT_FILE_FILTER = "NetSurvデータファイル (*.dat)|*.dat|RINEXファイル (*.??o)|*.??o|すべてのファイル (*.*)|*.*"; //'フィルター。

        //'RINEXファイル文字制限。
        public const long RINEX_STR_OBSNAME = 61;
        public const long RINEX_STR_OBSNUMBER = 21;
        public const long RINEX_STR_RECNAME = 21;
        public const long RINEX_STR_RECNUMBER = 21;
        public const long RINEX_STR_ANTNUMBER = 21;

        //'GUI
        public const long GUI_HEIGHT_DECIMAL = 4;                           //'高さ表示少数点以下桁数。

        public const string GENUINE_POINT_SESSION = @"\GN\";                //'本点のセッション名。

        //        //'DXFファイル種別。
        //        public Enum DXF_TYPE
        //        {
        //            DXF_TYPE_OBS = 0, //'観測図。
        //            DXF_TYPE_COUNT,  //'種別数。
        //        }


        //'DXFファイルタイトル。
        public const string DXF_TITLE_OBS = "観測図";

//'外部出力ファイル種別。
//Public Enum OUTPUT_TYPE
//    OUTPUT_TYPE_NVF = 0 'NVFファイル。
//    OUTPUT_TYPE_NVB 'NVBファイル。
//    OUTPUT_TYPE_JOB 'JOBファイル。
//    OUTPUT_TYPE_RINEX 'RINEXファイル。
//    OUTPUT_TYPE_CSV 'CSVファイル。
//    OUTPUT_TYPE_COUNT '種別数。
//End Enum

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

        //'観測記簿出力順。
        //Public Enum WRITE_ORDER_TYPE
        //    WRITE_ORDER_NUMBER = 0 '観測点番号順。
        public const long WRITE_ORDER_NUMBER = 0;      //'観測点番号順。
        //   WRITE_ORDER_ANALYSIS '基線解析順。
            public const long WRITE_ORDER_ANALYSIS = 1;    //'基線解析順。
        //End Enum

        //'定義デフォルト値。
        public const long EF_SAMETIMEMIN = 180;         //'最小同時観測時間(秒)。
        public const long DEF_SATELLITECOUNTMIN = 4;    //'最少共通衛星数。
        public const bool DEF_IMPORTENABLE = true;                  //'有効インポートフラグ。True=インポートした基線ベクトルは有効にする。False=インポートした基線ベクトルは無効にする。
        public const long DEF_WRITEORDER = WRITE_ORDER_ANALYSIS;    //'観測記簿出力順。

        public const long DEF_IMPORTCOMPORT = 0;        //'受信機を接続するCOMポート。2007/8/10 NGS Yamada
        public const bool DEF_IMPORTCOMPORTTYPE = true; //'受信機を接続するCOMポートの取得方法(True=自動取得、False=手動選択)。2007/8/10 NGS Yamada
        public const bool DEF_IMPORTDATASAVE = true;    //'受信機からインポート時に観測データを保存する（True=保存する、False=保存しない）2007/8/10 NGS Yamada

        //'メッセージ。
        public const string MESSAGE_CONTENT_EMPTY = "出力するデータがありません。";   //'出力する内容が無い。
        public const string MESSAGE_GEOIDO_ESTIMATE = "ジオイドモデルファイルの評価に失敗しました。";

        //'閉合差定数。
        public const long REGISTANGLE_DEF_CONNECTENABLE = 0;    //'繋がっていなくても接続と見なすのデフォルト値。



    }
}
