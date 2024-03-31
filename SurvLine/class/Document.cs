using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using System.Windows.Forms;
using SurvLine.mdl;
using static System.Net.WebRequestMethods;
using static System.Windows.Forms.AxHost;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using System.Drawing;
using System.Reflection;
using System.Runtime.Remoting;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;
using System.Collections.ObjectModel;
using System.Net.NetworkInformation;
using System.Net;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Xml.Linq;
using System.Collections;
using static SurvLine.mdl.MdlNSDefine;
using static SurvLine.mdl.MdlNSSDefine;
using static SurvLine.mdl.MdlUtility;
using static SurvLine.mdl.DEFINE;
using static SurvLine.mdl.MdlSession;   //2
using System.IO;
using static SurvLine.mdl.MdlListPane;
using static SurvLine.mdl.MdlBaseLineAnalyser;
using static SurvLine.mdl.MdlAutoOrderVector;

namespace SurvLine
{
    public class Document
    {
        MdlMain m_clsMdlMain;   //2

        public Document(MdlMain mdlMain)
        {
            this.mdlMain = mdlMain;

            m_clsMdlMain = mdlMain;   //2

            OutputParam oOutputParam = new OutputParam();

            for (int i = 0; i < (int)MdlNSSDefine.OUTPUT_TYPE.OUTPUT_TYPE_COUNT; i++)
            {
                m_clsOutputParam.Add(oOutputParam);
            }

        }
        public Document()
        {
            OutputParam oOutputParam = new OutputParam();

            for (int i = 0; i < (int)MdlNSSDefine.OUTPUT_TYPE.OUTPUT_TYPE_COUNT; i++)
            {
                m_clsOutputParam.Add(oOutputParam);
            }
        }
        public bool GetFileBool(BinaryReader br)
        {
            bool bBool = false;

            ushort uWork;
            uWork = br.ReadUInt16();
            if (uWork == 0xffff)
            {
                bBool = true;
            }
            else
            {
                bBool = false;
            }
            return bBool;
        }

        //==========================================================================================
        /*[VB]
        '*******************************************************************************
        'ドキュメント

        Option Explicit
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'イベント

        '結合距離違反イベント。
        '
        '結合する観測点間の距離が制限を超えている場合に発生する。
        '
        '引き数：
        'clsObservationPoint 違反の対象となる観測点。
        'nDistance 観測点間の距離(ｍ)。
        'nMax 距離の制限値(㎝)。
        'nStyle メッセージスタイル。0=メッセージ無し。1=OKボタンのみ。2=OKボタンとすべてOKボタン。
        'nResult メッセージボックスの結果が設定される。
        Event CombinedDistanceViolation(ByVal clsObservationPoint As ObservationPoint, ByVal nDistance As Double, ByVal nMax As Long, ByVal nStyle As Long, ByRef nResult As Long)
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        'イベント
        
        '結合距離違反イベント。
        '
        '結合する観測点間の距離が制限を超えている場合に発生する。
        '
        '引き数：
        'clsObservationPoint 違反の対象となる観測点。
        'nDistance 観測点間の距離(ｍ)。
        'nMax 距離の制限値(㎝)。
        'nStyle メッセージスタイル。0=メッセージ無し。1=OKボタンのみ。2=OKボタンとすべてOKボタン。
        'nResult メッセージボックスの結果が設定される。
        */
#if false
        /*
         *************************** 修正要 sakai
        */
        event CombinedDistanceViolation(ObservationPoint clsObservationPoin, double nDistance, long nMax, long nStyle, long nResult)
#endif
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '上書き通知イベント。
        '
        '同じ観測点番号、セッション名の観測点をインポートしたときに、上書きを確認するために発生する。
        '
        '引き数：
        'clsObservationPoint 対象となる観測点。
        'nStyle メッセージスタイル。0=メッセージ無し。1=OKボタンのみ。2=OKボタンとすべてOKボタン。
        'nResult メッセージボックスの結果が設定される。
        Event OverwriteNotification(ByVal clsObservationPoint As ObservationPoint, ByVal nStyle As Long, ByRef nResult As Long)
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '上書き通知イベント。
        '
        '同じ観測点番号、セッション名の観測点をインポートしたときに、上書きを確認するために発生する。
        '
        '引き数：
        'clsObservationPoint 対象となる観測点。
        'nStyle メッセージスタイル。0=メッセージ無し。1=OKボタンのみ。2=OKボタンとすべてOKボタン。
        'nResult メッセージボックスの結果が設定される。
        */
#if false
        /*
         *************************** 修正要 sakai
        */ 
        event OverwriteNotification(ObservationPoint clsObservationPoint, long nStyle, long nResult)
#endif
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '読み込みファイルバージョン拒否イベント。
        '
        'Dataファイルのバージョンが読み込みできないバージョンの時に発生する。
        '
        '引き数：
        'sPath Dataファイルのパス。
        'nVersion ファイルバージョン。
        Event RejectedFileVersion(ByVal sPath As String, ByVal nVersion As Long)
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '読み込みファイルバージョン拒否イベント。
        '
        'Dataファイルのバージョンが読み込みできないバージョンの時に発生する。
        '
        '引き数：
        'sPath Dataファイルのパス。
        'nVersion ファイルバージョン。
        */
#if false
        /*
         *************************** 修正要 sakai
        */ 
        event RejectedFileVersion(string sPath, long nVersion)
#endif
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '定数
        Private Const TEMP_FILE_NAME As String = "Document.tmp" '保存処理用のテンポラリファイルの名前。
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'定数
        private const string TEMP_FILE_NAME = "Document.tmp";        //'保存処理用のテンポラリファイルの名前。


        //==========================================================================================
        /*[VB]
        '恒久プロパティ
        Public PageNumberVisible As Boolean '帳票ページ番号表示フラグ。True=表示。False=非表示。
        Public NumberOfMinSV As Long '最少衛星数。0の場合は観測データから取得。1～12が固定値。
        Public EllipseModel As ELLIPSE_MODEL '記簿に出力する楕円体モデル。
        Public SameTimeMin As Long '最小同時観測時間(秒)。
        Public SatelliteCountMin As Long '最少共通衛星数。
        Public GeoidoPathDef As String 'ジオイドモデルのパスのデフォルト。
        Public SemiDynaPathDef As String 'セミ・ダイナミックパラメータファイルのパスのデフォルト。'2009/11 H.Nakamura
        Public DefLeapSec As Long 'デフォルトの閏秒。
        Public TimeZone As TIME_ZONE '時間帯。
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'恒久プロパティ
        public bool PageNumberVisible;                  //'帳票ページ番号表示フラグ。True=表示。False=非表示。
        public long NumberOfMinSV;                      //'最少衛星数。0の場合は観測データから取得。1～12が固定値。
        public ELLIPSE_MODEL EllipseModel;              //'記簿に出力する楕円体モデル。
        public long SameTimeMin;                        //'最小同時観測時間(秒)。
        public long SatelliteCountMin;                  //'最少共通衛星数。
        public string GeoidoPathDef;                    //'ジオイドモデルのパスのデフォルト。
        public string SemiDynaPathDef;                  //'セミ・ダイナミックパラメータファイルのパスのデフォルト。'2009/11 H.Nakamura
        public long DefLeapSec;                         //'デフォルトの閏秒。
        public TIME_ZONE TimeZone;                      //'時間帯。
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '2008/10/13 NGS Yamada
        'ユーザデータを管理するフォルダのパスを追加
        Public UserDataPath As String

        Public ImportComPort As Long '受信機を接続するCOMポート。2007/6/20 NGS Yamada
        Public ImportComPortType As Boolean '受信機を接続するCOMポートの取得方法(True=自動取得、False=手動選択)。2007/7/2 NGS Yamada
        Public ImportDataSave As Boolean    '受信機からインポート時に観測データを保存する（True=保存する、False=保存しない）2007/8/10 NGS Yamada

        Public PlotPointLabel As Long 'プロット画面に表示するラベルの種類。0=観測点No,1=観測点名称,2=観測点No(観測点名称)。 2006/10/6 NGS Yamada
        Public DisableVectorVisible As Boolean '「無効」なベクトルをプロット画面に表示するか？。True=表示,False=非表示。 2006/10/10 NGS Yamada
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'ユーザデータを管理するフォルダのパスを追加
        public string UserDataPath;

        public long ImportComPort;                      //'受信機を接続するCOMポート。2007/6/20 NGS Yamada
        public bool ImportComPortType;                  //'受信機を接続するCOMポートの取得方法(True=自動取得、False=手動選択)。2007/7/2 NGS Yamada
        public bool ImportDataSave;                     //'受信機からインポート時に観測データを保存する（True=保存する、False=保存しない）2007/8/10 NGS Yamada

        public long PlotPointLabel;                     //'プロット画面に表示するラベルの種類。0=観測点No,1=観測点名称,2=観測点No(観測点名称)。 2006/10/6 NGS Yamada
        public bool DisableVectorVisible;               //'「無効」なベクトルをプロット画面に表示するか？。True=表示,False=非表示。 2006/10/10 NGS Yamada
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'インプリメンテーション
        Private m_bEmpty As Boolean '空フラグ。True=空。False=空でない。
        Private m_sPath As String '保存ファイルのパス。
        Private m_bModifyed As Boolean '更新フラグ。True=更新あり。False=更新なし。

        Private m_sJobName As String '現場名。
        Private m_sDistrictName As String '地区名。
        Private m_sSupervisor As String 'プログラム管理者。
        Private m_nCoordNum As Long '座標系番号(1～19)。
        Private m_bGeoidoEnable As Boolean 'ジオイド有効/無効。True=有効。False=無効。
        Private m_sGeoidoPath As String 'ジオイドモデルのパス。
        Private m_bTkyEnable As Boolean '旧日本測地系有効/無効。True=有効。False=無効。
        Private m_sTkyPath As String '旧日本測地系パラメータファイルのパス。
        Private m_bSemiDynaEnable As Boolean 'セミ・ダイナミック有効/無効。True=有効。False=無効。'2009/11 H.Nakamura
        Private m_sSemiDynaPath As String 'セミ・ダイナミックパラメータファイルのパス。'2009/11 H.Nakamura
        Private WithEvents m_clsNetworkModel As NetworkModel 'ネットワークモデル。
        Private m_clsBaseLineAnalysisParam As BaseLineAnalysisParam '基線解析パラメータ。
        Private m_clsAngleDiffParamRing As AngleDiffParam '環閉合差パラメータ。
        Private m_clsAngleDiffParamBetween As AngleDiffParam '電子基準点間の閉合差パラメータ。
        Private m_clsAngleDiffParamHeight As AngleDiffParam '楕円体高の閉合差パラメータ。'2023/05/19 Hitz H.Nakamura 楕円体高の閉合差を追加。
        Private m_clsAccountMaker As AccountMaker '帳票作成。
        Private m_clsAccountParamHand As AccountParam '観測手簿パラメータ。
        Private m_clsAccountParamWrite As AccountParam '観測記簿パラメータ。
        Private m_clsAccountParamCoordinate As AccountParam '座標計算簿パラメータ。
        Private m_clsAccountOverlapParam As AccountOverlapParam '点検計算簿(重複基線)パラメータ。
        Private m_clsAccountParamEccentricCorrect As AccountParam '偏心計算簿パラメータ。
        Private m_clsAccountParamSemiDyna As AccountParam 'セミ・ダイナミック補正表パラメータ。'2009/11 H.Nakamura
        Private m_clsAccountCadastralParam As AccountCadastralParam '地籍図根三角測量精度管理表パラメータ。
        Private m_clsAccountParamResultBase As AccountParam '座標一覧表パラメータ。2007/7/18 NGS Yamada
        Private m_clsOutputParam(OUTPUT_TYPE_COUNT - 1) As OutputParam '外部出力ファイル出力パラメータ。
        Private m_clsDXFParam(DXF_TYPE_COUNT - 1) As DXFParam 'DXFファイル出力パラメータ。
        Private m_clsAutoOrderVectorParam As AutoOrderVectorParam '基線ベクトルの向きの自動整列パラメータ。
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'インプリメンテーション
        private bool m_bEmpty;                                          //'空フラグ。True=空。False=空でない。
        private string m_sPath;                                         //'保存ファイルのパス。
        private bool m_bModifyed;                                       //'更新フラグ。True=更新あり。False=更新なし。

        private string m_sJobName;                                      //'現場名。
        private string m_sDistrictName;                                 //'地区名。
        private string m_sSupervisor;                                   //'プログラム管理者。
        private long m_nCoordNum;                                       //'座標系番号(1～19)。
        private bool m_bGeoidoEnable;                                   //'ジオイド有効/無効。True=有効。False=無効。
        private string m_sGeoidoPath;                                   //'ジオイドモデルのパス。
        private bool m_bTkyEnable;                                      //'旧日本測地系有効/無効。True=有効。False=無効。
        private string m_sTkyPath;                                      //'旧日本測地系パラメータファイルのパス。
        public bool m_bSemiDynaEnable;                                 //'セミ・ダイナミック有効/無効。True=有効。False=無効。'2009/11 H.Nakamura
        public string m_sSemiDynaPath;                                 //'セミ・ダイナミックパラメータファイルのパス。'2009/11 H.Nakamura
        private NetworkModel WithEvents;                                //'ネットワークモデル。
        private NetworkModel m_clsNetworkModel;                         //'ネットワークモデル。
        private BaseLineAnalysisParam m_clsBaseLineAnalysisParam;       //'基線解析パラメータ。
        private AngleDiffParam m_clsAngleDiffParamRing;                 //'環閉合差パラメータ。
        private AngleDiffParam m_clsAngleDiffParamBetween;              //'電子基準点間の閉合差パラメータ。
        private AngleDiffParam m_clsAngleDiffParamHeight;               //'楕円体高の閉合差パラメータ。'2023/05/19 Hitz H.Nakamura 楕円体高の閉合差を追加。


        //K.S
        private AccountMaker m_clsAccountMaker;                     // As AccountMaker '帳票作成。
        private AccountParam m_clsAccountParamHand;                 // As AccountParam '観測手簿パラメータ。
        private AccountParam m_clsAccountParamWrite;                // As AccountParam '観測記簿パラメータ。
        private AccountParam m_clsAccountParamCoordinate;           // As AccountParam '座標計算簿パラメータ。
        private AccountOverlapParam m_clsAccountOverlapParam;       // As AccountOverlapParam //'点検計算簿(重複基線)パラメータ。
        private AccountParam m_clsAccountParamEccentricCorrect;     // As AccountParam '偏心計算簿パラメータ。
        private AccountParam m_clsAccountParamSemiDyna;             // As AccountParam 'セミ・ダイナミック補正表パラメータ。'2009/11 H.Nakamura
        private AccountCadastralParam m_clsAccountCadastralParam;   // As AccountCadastralParam '地籍図根三角測量精度管理表パラメータ。
        private AccountParam m_clsAccountParamResultBase;           // As AccountParam '座標一覧表パラメータ。2007/7/18 NGS Yamada

        private MdlSession m_clsMdlSession; //2

        private MdlAutoOrderVector m_clsMdlAutoOrderVector; //5


        //---------------------------------------------------------
        //private List<OutputParam> m_clsOutputParam;                 //private OutputParam m_clsOutputParam[OUTPUT_TYPE_COUNT - 1];    // As OutputParam '外部出力ファイル出力パラメータ。
        List<OutputParam> m_clsOutputParam = new List<OutputParam>((int)MdlNSSDefine.OUTPUT_TYPE.OUTPUT_TYPE_COUNT - 1);

        //---------------------------------------------------------
        private List<DXFParam> m_clsDXFParam;                       //Private m_clsDXFParam(DXF_TYPE_COUNT - 1) As DXFParam 'DXFファイル出力パラメータ。
        //---------------------------------------------------------
        private AutoOrderVectorParam m_clsAutoOrderVectorParam;     // As AutoOrderVectorParam '基線ベクトルの向きの自動整列パラメータ。
        //'*******************************************************************************

        private MdlMain mdlMain;
        private DEFINE mdlDefine;
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '*******************************************************************************
        'プロパティ

        '空であるかどうか。
        Property Get IsEmpty() As Boolean
            IsEmpty = m_bEmpty
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '*******************************************************************************
        'プロパティ
        
        '空であるかどうか。
        */
        public bool IsEmpty()
        {
            return m_bEmpty;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '現場名。
        Property Get JobName() As String
            JobName = m_sJobName
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'現場名。
        public string JobName()
        {
            return m_sJobName;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '地区名。
        Property Get DistrictName() As String
            DistrictName = m_sDistrictName
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'地区名。
        public string DistrictName()
        {
            return m_sDistrictName;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'プログラム管理者。
        Property Get Supervisor() As String
            Supervisor = m_sSupervisor
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'プログラム管理者。
        public string Supervisor()
        {
            return m_sSupervisor;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'フォルダ名。
        Property Get Folder() As String
            Dim sDrive As String
            Dim sDir As String
            Dim sTitle As String
            Dim sExt As String
            Call SplitPath(RTrimEx(Path, "\"), sDrive, sDir, sTitle, sExt)
            Folder = sTitle
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'フォルダ名。
        public string Folder()
        {
            return "";
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '座標系番号(1～19)。
        Property Let CoordNum(ByVal nCoordNum As Long)
            m_nCoordNum = nCoordNum
            m_bModifyed = True
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'座標系番号(1～19)。
        public void CoordNum(long nCoordNum)
        {
            m_nCoordNum = nCoordNum;
            m_bModifyed = true;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '座標系番号(1～19)。
        Property Get CoordNum() As Long
            CoordNum = m_nCoordNum
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'座標系番号(1～19)。
        public long CoordNum()
        {
            return m_nCoordNum;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'ジオイド有効/無効。
        Property Let GeoidoEnable(ByVal bGeoidoEnable As Boolean)
            m_bGeoidoEnable = bGeoidoEnable
            m_bModifyed = True
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'ジオイド有効/無効。
        public void GeoidoEnable(bool bGeoidoEnable)
        {
            m_bGeoidoEnable = bGeoidoEnable;
            m_bModifyed = true;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'ジオイド有効/無効。
        Property Get GeoidoEnable() As Boolean
            GeoidoEnable = m_bGeoidoEnable
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'ジオイド有効/無効。
        public bool GeoidoEnable()
        {
            return m_bGeoidoEnable;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'ジオイドモデルのパス。
        Property Let GeoidoPath(ByVal GeoidoPath As String)
            m_sGeoidoPath = GeoidoPath
            m_bModifyed = True
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'ジオイドモデルのパス。
        public void GeoidoPath(string GeoidoPath)
        {
            m_sGeoidoPath = GeoidoPath;
            m_bModifyed = true;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'ジオイドモデルのパス。
        Property Get GeoidoPath() As String
            GeoidoPath = m_sGeoidoPath
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'ジオイドモデルのパス。
        public string GeoidoPath()
        {
            return m_sGeoidoPath;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '2009/11 H.Nakamura
        'セミ・ダイナミック有効/無効。
        Property Let SemiDynaEnable(ByVal bSemiDynaEnable As Boolean)
            m_bSemiDynaEnable = bSemiDynaEnable
            m_bModifyed = True
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'セミ・ダイナミック有効/無効。
        public void SemiDynaEnable(bool bSemiDynaEnable)
        {
            m_bSemiDynaEnable = bSemiDynaEnable;
            m_bModifyed = true;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '2009/11 H.Nakamura
        'セミ・ダイナミック有効/無効。
        Property Get SemiDynaEnable() As Boolean
            SemiDynaEnable = m_bSemiDynaEnable
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'セミ・ダイナミック有効/無効。
        public bool SemiDynaEnable()
        {
            return m_bSemiDynaEnable;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '2009/11 H.Nakamura
        'セミ・ダイナミックパラメータファイルのパス。
        Property Let SemiDynaPath(ByVal SemiDynaPath As String)
            m_sSemiDynaPath = SemiDynaPath
            m_bModifyed = True
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'セミ・ダイナミックパラメータファイルのパス。
        public void SemiDynaPath(string SemiDynaPath)
        {
            m_sSemiDynaPath = SemiDynaPath;
            m_bModifyed = true;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '2009/11 H.Nakamura
        'セミ・ダイナミックパラメータファイルのパス。
        Property Get SemiDynaPath() As String
            SemiDynaPath = m_sSemiDynaPath
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'セミ・ダイナミックパラメータファイルのパス。
        public string SemiDynaPath()
        {
            return m_sSemiDynaPath;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '保存ファイルのパス。
        Property Get Path() As String
            Path = m_sPath
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'保存ファイルのパス。
        public string Path()
        {
            return m_sPath;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '更新フラグ。
        Property Get Modifyed() As Boolean
            Modifyed = m_bModifyed
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'更新フラグ。
        public bool Modifyed()
        {
            return m_bModifyed;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'ネットワークモデル。
        Property Get NetworkModel() As NetworkModel
            Set NetworkModel = m_clsNetworkModel
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'ネットワークモデル。
        public NetworkModel NetworkModel()
        {
            if(m_clsNetworkModel == null)
            {
                m_clsNetworkModel = new NetworkModel(mdlMain);
            }
            return m_clsNetworkModel;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '基線解析パラメータ。
        Property Get BaseLineAnalysisParam() As BaseLineAnalysisParam
            Set BaseLineAnalysisParam = m_clsBaseLineAnalysisParam
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'基線解析パラメータ。
        public BaseLineAnalysisParam BaseLineAnalysisParam()
        {
            return m_clsBaseLineAnalysisParam;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '閉合差パラメータの数。'2016/01/07 Hitz H.Nakamura
        Property Get AngleDiffParamCount() As Integer
            AngleDiffParamCount = 2
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'閉合差パラメータの数。'2016/01/07 Hitz H.Nakamura
        public int AngleDiffParamCount()
        {
            return 2;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '閉合差パラメータの数。'2016/01/07 Hitz H.Nakamura
        Property Get AngleDiffParam(ByVal Index As Integer) As AngleDiffParam
            If Index <= 0 Then
                Set AngleDiffParam = m_clsAngleDiffParamRing
            '2023/05/19 Hitz H.Nakamura **************************************************
            '楕円体高の閉合差を追加。
            'Else
            '    Set AngleDiffParam = m_clsAngleDiffParamBetween
            'End If
            '*****************************************************************************
            ElseIf Index = 1 Then
                Set AngleDiffParam = m_clsAngleDiffParamBetween
            Else
                Set AngleDiffParam = m_clsAngleDiffParamHeight
            End If
            '*****************************************************************************
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'閉合差パラメータの数。'2016/01/07 Hitz H.Nakamura
        public AngleDiffParam AngleDiffParam(int Index)
        {
            if (Index <= 0)
            {
                return m_clsAngleDiffParamRing;
            }
            else if (Index == 1)
            {
                return m_clsAngleDiffParamBetween;
            }
            else
            {
                return m_clsAngleDiffParamHeight;
            }
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '環閉合差パラメータ。
        Property Let AngleDiffParamRing(ByVal clsAngleDiffParam As AngleDiffParam)
            If m_clsAngleDiffParamRing.Compare(clsAngleDiffParam) Then Exit Property
            Let m_clsAngleDiffParamRing = clsAngleDiffParam
            m_bModifyed = True
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'環閉合差パラメータ。
        public void AngleDiffParamRing(AngleDiffParam clsAngleDiffParam)
        {
            if (m_clsAngleDiffParamRing.Compare(clsAngleDiffParam))
            {
                return;
            }
            m_clsAngleDiffParamRing = clsAngleDiffParam;
            m_bModifyed = true;
            return;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '環閉合差パラメータ。
        Property Get AngleDiffParamRing() As AngleDiffParam
            Set AngleDiffParamRing = m_clsAngleDiffParamRing
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'環閉合差パラメータ。
        public AngleDiffParam AngleDiffParamRing()
        {
            return m_clsAngleDiffParamRing;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '電子基準点間の閉合差パラメータ。
        Property Let AngleDiffParamBetween(ByVal clsAngleDiffParam As AngleDiffParam)
            If m_clsAngleDiffParamBetween.Compare(clsAngleDiffParam) Then Exit Property
            Let m_clsAngleDiffParamBetween = clsAngleDiffParam
            m_bModifyed = True
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'電子基準点間の閉合差パラメータ。
        public void AngleDiffParamBetween(AngleDiffParam clsAngleDiffParam)
        {
            if (m_clsAngleDiffParamBetween.Compare(clsAngleDiffParam))
            {
                return;
            }
            m_clsAngleDiffParamBetween = clsAngleDiffParam;
            m_bModifyed = true;
            return;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '電子基準点間の閉合差パラメータ。
        Property Get AngleDiffParamBetween() As AngleDiffParam
            Set AngleDiffParamBetween = m_clsAngleDiffParamBetween
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'電子基準点間の閉合差パラメータ。
        public AngleDiffParam AngleDiffParamBetween()
        {
            return m_clsAngleDiffParamBetween;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '2023/05/19 Hitz H.Nakamura **************************************************
        '楕円体高の閉合差を追加。
        '楕円体高の閉合差パラメータ。
        Property Let AngleDiffParamHeight(ByVal clsAngleDiffParam As AngleDiffParam)
            If m_clsAngleDiffParamHeight.Compare(clsAngleDiffParam) Then Exit Property
            Let m_clsAngleDiffParamHeight = clsAngleDiffParam
            m_bModifyed = True
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'楕円体高の閉合差パラメータ。
        public void AngleDiffParamHeight(AngleDiffParam clsAngleDiffParam)
        {
            if (m_clsAngleDiffParamHeight.Compare(clsAngleDiffParam))
            {
                return;
            }
            m_clsAngleDiffParamHeight = clsAngleDiffParam;
            m_bModifyed = true;
            return;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '楕円体高の閉合差パラメータ。
        Property Get AngleDiffParamHeight() As AngleDiffParam
            Set AngleDiffParamHeight = m_clsAngleDiffParamHeight
        End Property
        '*****************************************************************************
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'楕円体高の閉合差パラメータ。
        public AngleDiffParam AngleDiffParamHeight()
        {
            return m_clsAngleDiffParamHeight;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '観測手簿パラメータ。
        Property Get AccountParamHand() As AccountParam
            Set AccountParamHand = m_clsAccountParamHand
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
#if false
        /*
         *************************** 修正要 sakai
        */ 
        //'観測手簿パラメータ。
        public AccountParam AccountParamHand()
        {
            return m_clsAccountParamHand;
        }
#endif
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '観測記簿パラメータ。
        Property Get AccountParamWrite() As AccountParam
            Set AccountParamWrite = m_clsAccountParamWrite
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'観測記簿パラメータ。
#if false
        /*
         *************************** 修正要 sakai
         */
        public AccountParam AccountParamWrite()
        {
            return m_clsAccountParamWrite;
        }
#endif
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '座標計算簿パラメータ。
        Property Get AccountParamCoordinate() As AccountParam
            Set AccountParamCoordinate = m_clsAccountParamCoordinate
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
#if false
        /*
         *************************** 修正要 sakai
         */
        //'座標計算簿パラメータ。
        public AccountParam AccountParamCoordinate()
        {
            return m_clsAccountParamCoordinate;
        }
#endif
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '点検計算簿(重複基線)パラメータ。
        Property Get AccountOverlapParam() As AccountOverlapParam
            Set AccountOverlapParam = m_clsAccountOverlapParam
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
#if false
        /*
         *************************** 修正要 sakai
         */
        //'点検計算簿(重複基線)パラメータ。
        public AccountOverlapParam AccountOverlapParam()
        {
            return m_clsAccountOverlapParam;
        }
#endif
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '偏心計算簿パラメータ。
        Property Get AccountParamEccentricCorrect() As AccountParam
            Set AccountParamEccentricCorrect = m_clsAccountParamEccentricCorrect
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
#if false
        /*
         *************************** 修正要 sakai
         */
        //'偏心計算簿パラメータ。
        public AccountParam AccountParamEccentricCorrect()
        {
            return m_clsAccountParamEccentricCorrect;
        }
#endif
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '2009/11 H.Nakamura
        'セミ・ダイナミック補正表パラメータ。
        Property Get AccountParamSemiDyna() As AccountParam
            Set AccountParamSemiDyna = m_clsAccountParamSemiDyna
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
#if false
        /*
         *************************** 修正要 sakai
         */
        //'セミ・ダイナミック補正表パラメータ。
        public AccountParam AccountParamSemiDyna()
        {
            return m_clsAccountParamSemiDyna;
        }
#endif
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '地籍図根三角測量精度管理表パラメータ。
        Property Get AccountCadastralParam() As AccountCadastralParam
            Set AccountCadastralParam = m_clsAccountCadastralParam
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
#if false
        /*
         *************************** 修正要 sakai
         */
        //'地籍図根三角測量精度管理表パラメータ。
        public AccountCadastralParam AccountCadastralParam()
        {
            return m_clsAccountCadastralParam;
        }
#endif
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '外部出力ファイル出力パラメータ。
        Property Get OutputParam(ByVal nIndex As Long) As OutputParam
            Set OutputParam = m_clsOutputParam(nIndex)
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //修正要 saka -> K.Setoguchi
        //[C#]
        /// <summary>
        /// 外部出力ファイル出力パラメータ。
        /// 
        /// </summary>
        /// <param name="nIndex"></param>
        /// <returns></returns>
        public object OutputParam(long nIndex)
        {
            return m_clsOutputParam[(int)nIndex];
        }

        //==========================================================================================

        //==========================================================================================
        /*[VB]
            'DXFファイル出力パラメータ。
            Property Get DXFParam(ByVal nIndex As Long) As DXFParam
                Set DXFParam = m_clsDXFParam(nIndex)
            End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //修正要 saka -> K.Setoguchi
        //[C#]
        /// <summary>
        /// DXFファイル出力パラメータ。
        /// 
        /// </summary>
        /// <param name="nIndex"></param>
        /// <returns></returns>
        private DXFParam DXFParam(long nIndex)
        {
            DXFParam DXFParam = m_clsDXFParam[(int)nIndex];
            return DXFParam;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '基線ベクトルの向きの自動整列パラメータ。
        Property Get AutoOrderVectorParam() As AutoOrderVectorParam
            Set AutoOrderVectorParam = m_clsAutoOrderVectorParam
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //修正要 saka -> K.Setoguchi
        //[C#]
        /// <summary>
        /// '基線ベクトルの向きの自動整列パラメータ。
        /// 
        /// </summary>
        /// <returns></returns>
        public object AutoOrderVectorParam()
        {
            AutoOrderVectorParam AutoOrderVectorParam = m_clsAutoOrderVectorParam;
            return AutoOrderVectorParam;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '座標一覧表パラメータ。 2007/7/18 NGS Yamada
        Property Get AccountParamResultBase() As AccountParam
            Set AccountParamResultBase = m_clsAccountParamResultBase
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //修正要 saka -> K.Setoguchi
        //[C#]
        /// <summary>
        /// '座標一覧表パラメータ。 2007/7/18 NGS Yamada
        /// 
        /// </summary>
        /// <returns></returns>
        private AccountParam AccountParamResultBase()
        {
            AccountParam AccountParamResultBase = m_clsAccountParamResultBase;
            return AccountParamResultBase;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '*******************************************************************************
        'イベント

        '初期化
        Private Sub Class_Initialize()

            On Error GoTo ErrorHandler


            Call Clear


            PageNumberVisible = GetPrivateProfileInt(PROFILE_SAVE_SEC_ACCOUNT, PROFILE_SAVE_KEY_PAGENUMBERVISIBLE, 1, App.Path & "\" & App.Title & PROFILE_SAVE_EXT) <> 0
            NumberOfMinSV = GetPrivateProfileInt(PROFILE_SAVE_SEC_ACCOUNT, PROFILE_SAVE_KEY_NUMBEROFMINSV, 0, App.Path & "\" & App.Title & PROFILE_SAVE_EXT)
            EllipseModel = GetPrivateProfileInt(PROFILE_SAVE_SEC_ACCOUNT, PROFILE_SAVE_KEY_ELLIPSEMODEL, ELLIPSE_MODEL_GRS80, App.Path & "\" & App.Title & PROFILE_SAVE_EXT)
            SameTimeMin = GetPrivateProfileInt(PROFILE_SAVE_SEC_BASELINE, PROFILE_SAVE_KEY_SAMETIMEMIN, DEF_SAMETIMEMIN, App.Path & "\" & App.Title & PROFILE_SAVE_EXT)
            SatelliteCountMin = GetPrivateProfileInt(PROFILE_SAVE_SEC_BASELINE, PROFILE_SAVE_KEY_SATELLITECOUNTMIN, DEF_SATELLITECOUNTMIN, App.Path & "\" & App.Title & PROFILE_SAVE_EXT)
            GeoidoPathDef = GetPrivateProfileString(PROFILE_SAVE_SEC_GEOIDO, PROFILE_SAVE_KEY_PATH, "", App.Path & "\" & App.Title & PROFILE_SAVE_EXT)
            SemiDynaPathDef = GetPrivateProfileString(PROFILE_SAVE_SEC_SEMIDYNA, PROFILE_SAVE_KEY_PATH, "", App.Path & "\" & App.Title & PROFILE_SAVE_EXT) 'セミ・ダイナミック対応。'2009/11 H.Nakamura
            DefLeapSec = GetPrivateProfileInt(PROFILE_SAVE_SEC_BASELINE, PROFILE_SAVE_KEY_DEFLEAPSEC, DEF_LEAP_SEC, App.Path & "\" & App.Title & PROFILE_SAVE_EXT)
            TimeZone = GetPrivateProfileInt(PROFILE_SAVE_SEC_BASELINE, PROFILE_SAVE_KEY_TIMEZONE, TIME_ZONE_UTC, App.Path & "\" & App.Title & PROFILE_SAVE_EXT)
            '2006/10/6
            PlotPointLabel = GetPrivateProfileInt(PROFILE_SAVE_SEC_PLOT, PROFILE_SAVE_KEY_POINTLABEL, PROFILE_SAVE_DEF_POINTLABEL, App.Path & "\" & App.Title & PROFILE_SAVE_EXT)
            '2006/10/10
            DisableVectorVisible = GetPrivateProfileInt(PROFILE_SAVE_SEC_PLOT, PROFILE_SAVE_KEY_DISABLEVECTORVISIBLE, 1, App.Path & "\" & App.Title & PROFILE_SAVE_EXT)


            '2007/6/20 NGS Yamada
            ImportComPort = GetPrivateProfileInt(PROFILE_SAVE_SEC_BASELINE, PROFILE_SAVE_KEY_IMPORTCOMPORT, PROFILE_SAVE_DEF_IMPORTCOMPORT, App.Path & "\" & App.Title & PROFILE_SAVE_EXT)


            '2007/7/2 NGS Yamada
            ImportComPortType = GetPrivateProfileInt(PROFILE_SAVE_SEC_BASELINE, PROFILE_SAVE_KEY_IMPORTCOMPORTTYPE, 1, App.Path & "\" & App.Title & PROFILE_SAVE_EXT) <> 0


            '2007/8/10 NGS Yamada
            ImportDataSave = GetPrivateProfileInt(PROFILE_SAVE_SEC_BASELINE, PROFILE_SAVE_KEY_IMPORTDATASAVE, 1, App.Path & "\" & App.Title & PROFILE_SAVE_EXT) <> 0


            '2008/10/13 NGS Yamada
            UserDataPath = GetPrivateProfileString(PROFILE_SAVE_SEC_DETAIL, PROFILE_SAVE_KEY_USERDATAPATH, App.Path & DATA_FOLDER_NAME, App.Path & "\" & App.Title & PROFILE_SAVE_EXT)


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
        
        '初期化
        */
        public void Class_Initialize()
        {
            try
            {
                Clear();

                //mdlDefine = new DEFINE();

                string AppPath = System.IO.Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath);
#if true
                /*
                 ****************** 修正要？ sakai
                */
                //string AppTitle = "SurvLine";
                string AppTitle = "NS-Survey";
#endif
                PageNumberVisible = GetPrivateProfileInt(PROFILE_SAVE_SEC_ACCOUNT, PROFILE_SAVE_KEY_PAGENUMBERVISIBLE, 1, AppPath + "\\" + AppTitle + PROFILE_SAVE_EXT) != 0;
                NumberOfMinSV = GetPrivateProfileInt(PROFILE_SAVE_SEC_ACCOUNT, PROFILE_SAVE_KEY_NUMBEROFMINSV, 0, AppPath + "\\" + AppTitle + PROFILE_SAVE_EXT);
                EllipseModel = (ELLIPSE_MODEL)GetPrivateProfileInt(PROFILE_SAVE_SEC_ACCOUNT, PROFILE_SAVE_KEY_ELLIPSEMODEL, 0/*ELLIPSE_MODEL_GRS80*/, AppPath + "\\" + AppTitle + PROFILE_SAVE_EXT);
                SameTimeMin = GetPrivateProfileInt(PROFILE_SAVE_SEC_BASELINE, PROFILE_SAVE_KEY_SAMETIMEMIN, (int)DEF_SAMETIMEMIN, AppPath + "\\" + AppTitle + PROFILE_SAVE_EXT);
                SatelliteCountMin = GetPrivateProfileInt(PROFILE_SAVE_SEC_BASELINE, PROFILE_SAVE_KEY_SATELLITECOUNTMIN, (int)DEF_SATELLITECOUNTMIN, AppPath + "\\" + AppTitle + PROFILE_SAVE_EXT);
#if true
                /*
                 *************************** 修正要？ sakai
                */
                GeoidoPathDef = GetPrivateProfileString(PROFILE_SAVE_SEC_GEOIDO, PROFILE_SAVE_KEY_PATH, "", AppPath + "\\" + AppTitle + PROFILE_SAVE_EXT);
                SemiDynaPathDef = GetPrivateProfileString(PROFILE_SAVE_SEC_SEMIDYNA, PROFILE_SAVE_KEY_PATH, "", AppPath + "\\" + AppTitle + PROFILE_SAVE_EXT); //'セミ・ダイナミック対応。'2009/11 H.Nakamura
#endif
                DefLeapSec = GetPrivateProfileInt(PROFILE_SAVE_SEC_BASELINE, PROFILE_SAVE_KEY_DEFLEAPSEC, (int)DEF_LEAP_SEC, AppPath + "\\" + AppTitle + PROFILE_SAVE_EXT);
                TimeZone = (TIME_ZONE)GetPrivateProfileInt(PROFILE_SAVE_SEC_BASELINE, PROFILE_SAVE_KEY_TIMEZONE, 0 /*TIME_ZONE_UTC*/, AppPath + "\\" + AppTitle + PROFILE_SAVE_EXT);
                //'2006/10/6
                PlotPointLabel = GetPrivateProfileInt(PROFILE_SAVE_SEC_PLOT, PROFILE_SAVE_KEY_POINTLABEL, (int)PROFILE_SAVE_DEF_POINTLABEL, AppPath + "\\" + AppTitle + PROFILE_SAVE_EXT);
                //'2006/10/10
                DisableVectorVisible = GetPrivateProfileInt(PROFILE_SAVE_SEC_PLOT, PROFILE_SAVE_KEY_DISABLEVECTORVISIBLE, 1, AppPath + "\\" + AppTitle + PROFILE_SAVE_EXT) != 0;

                //'2007/6/20 NGS Yamada
                ImportComPort = GetPrivateProfileInt(PROFILE_SAVE_SEC_BASELINE, PROFILE_SAVE_KEY_IMPORTCOMPORT, (int)PROFILE_SAVE_DEF_IMPORTCOMPORT, AppPath + "\\" + AppTitle + PROFILE_SAVE_EXT);

                //'2007/7/2 NGS Yamada
                ImportComPortType = GetPrivateProfileInt(PROFILE_SAVE_SEC_BASELINE, PROFILE_SAVE_KEY_IMPORTCOMPORTTYPE, 1, AppPath + "\\" + AppTitle + PROFILE_SAVE_EXT) != 0;

                //'2007/8/10 NGS Yamada
                ImportDataSave = GetPrivateProfileInt(PROFILE_SAVE_SEC_BASELINE, PROFILE_SAVE_KEY_IMPORTDATASAVE, 1, AppPath + "\\" + AppTitle + PROFILE_SAVE_EXT) != 0;

#if true
                /*
                 *************************** 修正要？ sakai
                */
                //'2008/10/13 NGS Yamada
                UserDataPath = GetPrivateProfileString(PROFILE_SAVE_SEC_DETAIL, PROFILE_SAVE_KEY_USERDATAPATH, AppPath + DATA_FOLDER_NAME, AppPath + "\\" + AppTitle + PROFILE_SAVE_EXT);
#endif

                return;
            }


            catch
            {
                mdlMain.ErrorExit();
                return;
            }
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '終了
        Private Sub Class_Terminate()

            On Error GoTo ErrorHandler


            Call Clear


            Exit Sub


        ErrorHandler:
            Call mdlMain.ErrorExit


        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'終了
        private void Class_Terminate()
        {
            try
            {
                Clear();
                return;
            }
            catch
            {
                mdlMain.ErrorExit();
                return;
            }
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '結合距離違反。
        Private Sub m_clsNetworkModel_CombinedDistanceViolation(ByVal clsObservationPoint As ObservationPoint, ByVal nDistance As Double, ByVal nMax As Long, ByVal nStyle As Long, ByRef nResult As Long)
            RaiseEvent CombinedDistanceViolation(clsObservationPoint, nDistance, nMax, nStyle, nResult)
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'結合距離違反。
        private void m_clsNetworkModel_CombinedDistanceViolation(ObservationPoint clsObservationPoint, double nDistance, long nMax, long nStyle, long nResult)
        {
#if false
            /*
             *************************** 修正要 sakai
            */
            RaiseEvent CombinedDistanceViolation(clsObservationPoint, nDistance, nMax, nStyle, nResult);
#endif
            return;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '結合距離違反。
        Private Sub m_clsNetworkModel_OverwriteNotification(ByVal clsObservationPoint As ObservationPoint, ByVal nStyle As Long, ByRef nResult As Long)
            RaiseEvent OverwriteNotification(clsObservationPoint, nStyle, nResult)
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'結合距離違反。
        private void m_clsNetworkModel_OverwriteNotification(ObservationPoint clsObservationPoint, long nStyle, long nResult)
        {
#if false
            /*
             *************************** 修正要 sakai
            */
            RaiseEvent OverwriteNotification(clsObservationPoint, nStyle, nResult);
#endif
            return;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '*******************************************************************************
        'メソッド

        'オブジェクトをクリアする。
        Public Sub Clear()
            m_bEmpty = True
            m_sPath = ""
            m_bModifyed = False
            m_sJobName = ""
            m_sDistrictName = ""
            m_sSupervisor = ""
            m_nCoordNum = 0
            m_bGeoidoEnable = False
            m_sGeoidoPath = ""
            m_bTkyEnable = False
            m_sTkyPath = ""
            m_bSemiDynaEnable = False 'セミ・ダイナミック対応。'2009 / 11 H.Nakamura
            m_sSemiDynaPath = "" 'セミ・ダイナミック対応。'2009 / 11 H.Nakamura
            Set m_clsNetworkModel = New NetworkModel
            Set m_clsBaseLineAnalysisParam = New BaseLineAnalysisParam
            Set m_clsAngleDiffParamRing = New AngleDiffParam
            Set m_clsAngleDiffParamBetween = New AngleDiffParam
            Set m_clsAngleDiffParamHeight = New AngleDiffParam '2023/05/19 Hitz H.Nakamura 楕円体高の閉合差を追加。
            Set m_clsAccountMaker = New AccountMaker
            Set m_clsAccountParamHand = New AccountParam
            Set m_clsAccountParamWrite = New AccountParam
            Set m_clsAccountParamCoordinate = New AccountParam
            Set m_clsAccountOverlapParam = New AccountOverlapParam
            Set m_clsAccountParamEccentricCorrect = New AccountParam
            Set m_clsAccountParamSemiDyna = New AccountParam 'セミ・ダイナミック対応。'2009 / 11 H.Nakamura
            Set m_clsAccountCadastralParam = New AccountCadastralParam
            Set m_clsAutoOrderVectorParam = New AutoOrderVectorParam
            Set m_clsAccountParamResultBase = New AccountParam  '2007/7/18 NGS Yamada

            Dim i As Long
            For i = 0 To OUTPUT_TYPE_COUNT - 1
                Set m_clsOutputParam(i) = New OutputParam
            Next
            For i = 0 To DXF_TYPE_COUNT - 1
                Set m_clsDXFParam(i) = New DXFParam
            Next
            '観測点ファイルの削除。
            Call EmptyDir(App.Path & TEMPORARY_PATH & "." & OBSPOINT_PATH)
            'セミ・ダイナミックの終了処理。'2009 / 11 H.Nakamura
            Call mdlSemiDyna.Terminate
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '*******************************************************************************
        'メソッド
        
        'オブジェクトをクリアする。
        */
        public void Clear()
        {
            m_bEmpty = true;
            m_sPath = "";
            m_bModifyed = false;
            m_sJobName = "";
            m_sDistrictName = "";
            m_sSupervisor = "";
            m_nCoordNum = 0;
            m_bGeoidoEnable = false;
            m_sGeoidoPath = "";
            m_bTkyEnable = false;
            m_sTkyPath = "";
            m_bSemiDynaEnable = false;                                          //'セミ・ダイナミック対応。'2009 / 11 H.Nakamura
            m_sSemiDynaPath = "";                                               //'セミ・ダイナミック対応。'2009 / 11 H.Nakamura
            m_clsNetworkModel = new NetworkModel(mdlMain);
            m_clsBaseLineAnalysisParam = new BaseLineAnalysisParam();
            m_clsAngleDiffParamRing = new AngleDiffParam();
            m_clsAngleDiffParamBetween = new AngleDiffParam();
            m_clsAngleDiffParamHeight = new AngleDiffParam();    //'2023/05/19 Hitz H.Nakamura 楕円体高の閉合差を追加。

            m_clsMdlSession = new MdlSession(mdlMain);

            m_clsMdlAutoOrderVector = new MdlAutoOrderVector(mdlMain);  //5


            m_clsAutoOrderVectorParam = new AutoOrderVectorParam();     //5  //基線ベクトルの向きの自動整列パラメータ。



#if false
            /*
             *************************** 修正要 sakai
             */
            AccountMaker m_clsAccountMaker = new AccountMaker();
            AccountParam m_clsAccountParamHand = new AccountParam();
            AccountParam m_clsAccountParamWrite = new AccountParam();
            AccountParam m_clsAccountParamCoordinate = new AccountParam();
            AccountOverlapParam m_clsAccountOverlapParam = new AccountOverlapParam();
            AccountParam m_clsAccountParamEccentricCorrect = new AccountParam();
            AccountParam m_clsAccountParamSemiDyna = new AccountParam();        //'セミ・ダイナミック対応。'2009 / 11 H.Nakamura
            AccountCadastralParam m_clsAccountCadastralParam = new AccountCadastralParam();
            AutoOrderVectorParam m_clsAutoOrderVectorParam = new AutoOrderVectorParam();
            AccountParam m_clsAccountParamResultBase = new AccountParam();      //'2007/7/18 NGS Yamada
#endif

        long i;
            long i2 = (long)OUTPUT_TYPE.OUTPUT_TYPE_COUNT;
            for (i = 0; i < i2; i++)
            {
#if false
                /*
                 *************************** 修正要 sakai
                 */
                OutputParam m_clsOutputParam[i] = new OutputParam();
#endif
            }
            i2 = (long)DXF_TYPE.DXF_TYPE_COUNT;
            for (i = 0; i < i2; i++)
            {
#if false
                /*
                 *************************** 修正要 sakai
                 */
                DXFParam m_clsDXFParam[i] = new DXFParam();
#endif
            }
            //'観測点ファイルの削除。
#if false
            /*
             *************************** 修正要 sakai
             */
            EmptyDir(App.Path & TEMPORARY_PATH & "." & OBSPOINT_PATH);
#endif
            //'セミ・ダイナミックの終了処理。'2009 / 11 H.Nakamura
#if false
            /*
             *************************** 修正要 sakai
             */
            mdlSemiDyna.Terminate;
#endif

            ObservationPointKey_NSO clsObservationPointKey_NSO = new ObservationPointKey_NSO();

            return;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '保存。
        '
        'sPath で指定されたファイルに保存する。
        '
        '引き数：
        'sPath 保存ファイルのパス。
        Public Sub Save(ByVal sPath As String)

            'テンポラリファイル。
            Dim sTemp As String
            sTemp = App.Path & TEMPORARY_PATH & TEMP_FILE_NAME
            On Error Resume Next
            Call RemoveFile(sTemp)
            On Error GoTo 0


            'テンポラリファイルに書き込み。
            Dim clsFile As New FileNumber
            Open sTemp For Binary Access Write Lock Read Write As #clsFile.Number
    
            Put #clsFile.Number, , DOCUMENT_FILE_VERSION
            Call PutString(clsFile.Number, m_sJobName)
            Call PutString(clsFile.Number, m_sDistrictName)
            '↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑
            '地区名より前の領域は ProjectFileManager 関係に影響するので注意。


            Call PutString(clsFile.Number, m_sSupervisor)
            Put #clsFile.Number, , m_nCoordNum
            Put #clsFile.Number, , m_bGeoidoEnable
            Call PutString(clsFile.Number, m_sGeoidoPath)
            Put #clsFile.Number, , m_bTkyEnable
            Call PutString(clsFile.Number, m_sTkyPath)


            'セミ・ダイナミック対応。'2009 / 11 H.Nakamura
            Put #clsFile.Number, , m_bSemiDynaEnable
            Call PutString(clsFile.Number, m_sSemiDynaPath)


            Call m_clsNetworkModel.Save(clsFile.Number)
            Call m_clsBaseLineAnalysisParam.Save(clsFile.Number)
            Call m_clsAngleDiffParamRing.Save(clsFile.Number)
            Call m_clsAngleDiffParamBetween.Save(clsFile.Number)
            Call m_clsAngleDiffParamHeight.Save(clsFile.Number) '2023/05/19 Hitz H.Nakamura 楕円体高の閉合差を追加。
            Call m_clsAccountParamHand.Save(clsFile.Number)
            Call m_clsAccountParamWrite.Save(clsFile.Number)
            Call m_clsAccountParamCoordinate.Save(clsFile.Number)
            Call m_clsAccountOverlapParam.Save(clsFile.Number)
            Call m_clsAccountParamEccentricCorrect.Save(clsFile.Number)
            Call m_clsAccountParamSemiDyna.Save(clsFile.Number) 'セミ・ダイナミック対応。'2009 / 11 H.Nakamura
            Call m_clsAccountCadastralParam.Save(clsFile.Number)
            Call m_clsAutoOrderVectorParam.Save(clsFile.Number)
            Call m_clsAccountParamResultBase.Save(clsFile.Number)   '2007/7/18 NGS Yamada


            Dim i As Long
            For i = 0 To OUTPUT_TYPE_COUNT - 1
                Call m_clsOutputParam(i).Save(clsFile.Number)
            Next
            For i = 0 To DXF_TYPE_COUNT - 1
                Call m_clsDXFParam(i).Save(clsFile.Number)
            Next


            'チェックサム。
            Dim nSize As Long
            nSize = Loc(clsFile.Number)
            Put #clsFile.Number, , nSize
    
            Call clsFile.CloseFile


            'テンポラリファイルと置き換える。
            Call ReplaceFile(sTemp, sPath & DATA_FILE_NAME)


            '観測点ファイルのコピー。
            Dim sSrcObsPointPath As String
            Dim sDstObsPointPath As String
            sSrcObsPointPath = App.Path & TEMPORARY_PATH & "." & OBSPOINT_PATH
            sDstObsPointPath = sPath & "." & OBSPOINT_PATH
            Call DeleteDir(sDstObsPointPath, True)
            Call CopyDir(sSrcObsPointPath, sDstObsPointPath, True)


            m_sPath = sPath 'パスを更新。
            m_bModifyed = False


        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]  //0304  //2
        /// <summary>
        /// 保存。
        /// '
        /// sPath で指定されたファイルに保存する。
        /// '
        /// 引き数：
        /// sPath 保存ファイルのパス。
        /// 
        /// </summary>
        /// <param name="sPath"></param>
        /*
        '保存。
        '
        'sPath で指定されたファイルに保存する。
        '
        '引き数：
        'sPath 保存ファイルのパス。
        */
        public void Save(string sPath)      //0304  //2
        {
            MdlUtility mdiUtility = new MdlUtility();

            //---------------------------------------------------------------
            //    'テンポラリファイル。
            //    Dim sTemp As String
            //    sTemp = App.Path & TEMPORARY_PATH & TEMP_FILE_NAME
            //    On Error Resume Next
            //    Call RemoveFile(sTemp)
            //    On Error GoTo 0

            string App_Path = @"C:\Develop\NetSurv\Src\NS-App\NS-Survey";

            string sTemp;
            sTemp = $"{App_Path}{MdlNSDefine.TEMPORARY_PATH}{TEMP_FILE_NAME}";      //(Ex.)Temp  (Ex.)Document.tmp   (Ex.)"C:\Develop\NetSurv\Src\NS-App\NS-Survey\Temp\Document.tmp"

            if (RemoveFile(sTemp, false))
            {
                //結果は、無視すること
            }

            //---------------------------------------------------------------
            //    'テンポラリファイルに書き込み。
            //    Dim clsFile As New FileNumber
            //    Open sTemp For Binary Access Write Lock Read Write As #clsFile.Number
            //    
            using (var fs = System.IO.File.OpenWrite(sTemp))
            {
                using (var bw = new BinaryWriter(fs))
                {

#if false   //2 ********************************************************************************

                    //-------------------------------------------------------
                    //    Put #clsFile.Number, , DOCUMENT_FILE_VERSION
                    //    Call PutString(clsFile.Number, m_sJobName)
                    //    Call PutString(clsFile.Number, m_sDistrictName)
                    //    '↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑
                    //    '地区名より前の領域は ProjectFileManager 関係に影響するので注意。
                    bw.Write((uint)MdlNSDefine.DOCUMENT_FILE_VERSION);

#if false   //--------------------------------------------------------------------        
                    string shiftJisString = "こんにちは、世界！";
                    byte[] shiftJisBytes = Encoding.GetEncoding("Shift-JIS").GetBytes(shiftJisString);
                    bw.Write(shiftJisBytes);
#endif      //--------------------------------------------------------------------
                    FileWrite_PutString(bw, m_sJobName);
                    FileWrite_PutString(bw, m_sDistrictName);

                    //-------------------------------------------------------
                    //    Call PutString(clsFile.Number, m_sSupervisor)
                    //    Put #clsFile.Number, , m_nCoordNum
                    //    Put #clsFile.Number, , m_bGeoidoEnable
                    //    Call PutString(clsFile.Number, m_sGeoidoPath)
                    //    Put #clsFile.Number, , m_bTkyEnable
                    //    Call PutString(clsFile.Number, m_sTkyPath)
                    FileWrite_PutString(bw, m_sSupervisor);      //Version対応で""です。
                    bw.Write((uint)m_nCoordNum);                            //nCoordNum = br.ReadInt32();
                    PutFileBool(bw, m_bGeoidoEnable);            //bGeoidoEnable = GetFileBool(br);
                    FileWrite_PutString(bw, m_sGeoidoPath);      //sGeoidoPath = mdiUtility.FileRead_GetString(br); 
                    PutFileBool(bw, m_bTkyEnable);               //bTkyEnable = GetFileBool(br);
                    FileWrite_PutString(bw, m_sTkyPath);         //sTkyPath = mdiUtility.FileRead_GetString(br);

                    //-------------------------------------------------------
                    //    'セミ・ダイナミック対応。'2009/11 H.Nakamura
                    //    Put #clsFile.Number, , m_bSemiDynaEnable
                    //    Call PutString(clsFile.Number, m_sSemiDynaPath)
                    PutFileBool(bw, m_bSemiDynaEnable);          //bSemiDynaEnable = GetFileBool(br);
                    FileWrite_PutString(bw, m_sSemiDynaPath);    //sSemiDynaPath = mdiUtility.FileRead_GetString(br);


#if false
                    //-------------------------------------------------------
                    //    Call m_clsNetworkModel.Save(clsFile.Number)
                    m_clsNetworkModel.Save(bw);

                    //    Call m_clsBaseLineAnalysisParam.Save(clsFile.Number)
                    m_clsBaseLineAnalysisParam.Save(bw);

                    //    Call m_clsAngleDiffParamRing.Save(clsFile.Number)
                    m_clsAngleDiffParamRing.Save(bw);

                    //    Call m_clsAngleDiffParamBetween.Save(clsFile.Number)
                    m_clsAngleDiffParamBetween.Save(bw);

                    //    Call m_clsAngleDiffParamHeight.Save(clsFile.Number) '2023/05/19 Hitz H.Nakamura 楕円体高の閉合差を追加。
                    m_clsAngleDiffParamHeight.Save(bw);

                    //    Call m_clsAccountParamHand.Save(clsFile.Number)
                    m_clsAccountParamHand.Save(bw);

                    //    Call m_clsAccountParamWrite.Save(clsFile.Number)

                    //    Call m_clsAccountParamCoordinate.Save(clsFile.Number)

                    //    Call m_clsAccountOverlapParam.Save(clsFile.Number)

                    //    Call m_clsAccountParamEccentricCorrect.Save(clsFile.Number)

                    //    Call m_clsAccountParamSemiDyna.Save(clsFile.Number) 'セミ・ダイナミック対応。'2009/11 H.Nakamura

                    //    Call m_clsAccountCadastralParam.Save(clsFile.Number)

                    //    Call m_clsAutoOrderVectorParam.Save(clsFile.Number)

                    //    Call m_clsAccountParamResultBase.Save(clsFile.Number)   '2007/7/18 NGS Yamada

#endif

                    //--------------------------------------------------------
                    //    Dim i As Long
                    //    For i = 0 To OUTPUT_TYPE_COUNT - 1
                    //        Call m_clsOutputParam(i).Save(clsFile.Number)
                    //    Next
                    //    For i = 0 To DXF_TYPE_COUNT - 1
                    //        Call m_clsDXFParam(i).Save(clsFile.Number)
                    //    Next


                    //--------------------------------------------------------
                    //    'チェックサム。
                    //    Dim nSize As Long
                    //    nSize = Loc(clsFile.Number)
                    //    Put #clsFile.Number, , nSize
                    //
                    long nSize = 0;
                    //        nSize = Loc(bw);
                    bw.Write(nSize);

#endif               //2 ********************************************************************************


                    //--------------------------------------------------------
                    //    Call clsFile.CloseFile
                    fs.Close();

#if false           //2 ********************************************************************************

                    //--------------------------------------------------------
                    //    'テンポラリファイルと置き換える。
                    //    Call ReplaceFile(sTemp, sPath & DATA_FILE_NAME)       //sTemp = "C:\Develop\NetSurv\Src\NS-App\NS-Survey\Temp\Document.tmp"
                    //                                                          //sPath = "C:\Develop\NetSurv\Src\NS-App\NS-Survey\UserData\0275\"  // data
                    mdiUtility.ReplaceFile(sTemp, $"{sPath}{GENBA_CONST.DATA_FILE_NAME}");

#endif               //2 ********************************************************************************


                    //--------------------------------------------------------
                    //    '観測点ファイルのコピー。
                    //    Dim sSrcObsPointPath As String
                    //    Dim sDstObsPointPath As String
                    //    sSrcObsPointPath = App.Path & TEMPORARY_PATH & "." & OBSPOINT_PATH
                    //    sDstObsPointPath = sPath & "." & OBSPOINT_PATH
                    //    Call DeleteDir(sDstObsPointPath, True)
                    //    Call CopyDir(sSrcObsPointPath, sDstObsPointPath, True)
                    //
                    //
                    string sSrcObsPointPath;
                    string sDstObsPointPath;
                    sSrcObsPointPath = $"{App_Path}{MdlNSDefine.TEMPORARY_PATH}.{MdlNSSDefine.OBSPOINT_PATH}";  //"C:\Develop\NetSurv\Src\NS-App\NS-Survey\Temp\.\ObsPoint\"
                    sDstObsPointPath = $"{sPath}.{MdlNSSDefine.OBSPOINT_PATH}";                                 //"C:\Develop\NetSurv\Src\NS-App\NS-Survey\UserData\0275\.\ObsPoint\"    
                    bool dmy1 = DeleteDir(sDstObsPointPath, true);
                    bool dmy2 = CopyDir(sSrcObsPointPath, sDstObsPointPath, true);

                    //--------------------------------------------------------
                    //    m_sPath = sPath 'パスを更新。
                    //    m_bModifyed = False
                    m_sPath = sPath;        //'パスを更新。
                    m_bModifyed = false;

                }

            }
        }
        //==========================================================================================



        //**************************************************************************************
        //**************************************************************************************
        /// <summary>
        /// sPath で指定されたファイルから読み込む。
        /// 引き数：
        //  sPath：保存ファイルのパス。
        /// </summary>
        /// <param name="sPath"></param>
        /// <returns>
        /// </returns>
        //**************************************************************************************
        //      public void Load(string sPath, ref GENBA_STRUCT_S Genba_S)
        public void Load(string sPath, ref List<GENBA_STRUCT_S> List_Genba_S)
        {

            //-------------------------------------------------------------------------
            GENBA_STRUCT_S Genba_S = new GENBA_STRUCT_S();
            List_Genba_S.Add(Genba_S);
            //-------------------------------------------------------------------------


            //sPath ="C:\Develop\NetSurv\Src\NS-App\NS-Survey\UserData\0006\"
            //-------------------------------------------------------------------------
            //[VB]  '観測点ファイルのコピー。
            //[VB]  Dim sSrcObsPointPath As String
            //[VB]  Dim sDstObsPointPath As String
            string sSrcObsPointPath;
            string sDstObsPointPath;

            //-------------------------------------------------------------
            //[VB]  sSrcObsPointPath = sPath & "." & OBSPOINT_PATH
            //sSrcObsPointPath = $"{sPath}.{MdlNSSDefine.OBSPOINT_PATH}";     //"C:\Develop\NetSurv\Src\NS-App\NS-Survey\UserData\0006\.\ObsPoint\"
            sSrcObsPointPath = $"{sPath}{MdlNSSDefine.OBSPOINT_PATH}";     //"C:\Develop\NetSurv\Src\NS-App\NS-Survey\UserData\0006\.\ObsPoint\"

            //-------------------------------------------------------------
            //[VB]  sDstObsPointPath = App.Path & TEMPORARY_PATH & "." & OBSPOINT_PATH
            string App_Path = @"C:\Develop\NetSurv\Src\NS-App\NS-Survey";
            //sDstObsPointPath = $"{App_Path}{MdlNSDefine.TEMPORARY_PATH}.{MdlNSSDefine.OBSPOINT_PATH}";  //"C:\Develop\NetSurv\Src\NS-App\NS-Survey\Temp\.\ObsPoint\"
            sDstObsPointPath = $"{App_Path}{MdlNSDefine.TEMPORARY_PATH}{MdlNSSDefine.OBSPOINT_PATH}";  //"C:\Develop\NetSurv\Src\NS-App\NS-Survey\Temp\.\ObsPoint\"

            //-------------------------------------------------------------
            //[VB]  Call DeleteDir(sDstObsPointPath, True)
            bool dmy1 = DeleteDir(sDstObsPointPath, true);

            //-------------------------------------------------------------
            //[VB]  Call CopyDir(sSrcObsPointPath, sDstObsPointPath, True)
            bool dmy2 = CopyDir(sSrcObsPointPath, sDstObsPointPath, true);


            //-------------------------------------------------------------
            //[VB]  'テンポラリ変数に読み込む。
            //[VB]  Dim clsFile As New FileNumber
            //[VB]  Open sPath & DATA_FILE_NAME For Binary Access Read Lock Write As #clsFile.Number
            //[VB]      
            long nVersion;
            using (var fs = System.IO.File.OpenRead($"{sPath}{GENBA_CONST.DATA_FILE_NAME}"))    //sPath:"C:\\Develop\\NetSurv\\Src\\NS-App\\NS-Survey\\UserData\\0006\\"
            {
                using (BinaryReader br = new BinaryReader(fs))
                {
                    //---------------------------------------------------
                    //[VB]      'ファイルバージョン。
                    //[VB]  Dim nVersion As Long
                    //[VB]  Get #clsFile.Number, , nVersion
                    //[VB]  If nVersion< 1100 Or DOCUMENT_FILE_VERSION<nVersion Then
                    //[VB]      RaiseEvent RejectedFileVersion(sPath, nVersion)
                    //[VB]      Exit Sub
                    //[VB]  End If
                    nVersion = br.ReadInt32();
                    if ((nVersion < 1100) || (MdlNSDefine.DOCUMENT_FILE_VERSION < nVersion))
                    {
                        return;
                    }

                    //-------------------------------------------------------
                    //[VB]  Dim sJobName As String
                    //[VB]  Dim sDistrictName As String
                    //[VB]  Dim sSupervisor As String
                    //[VB]  Dim nCoordNum As Long
                    //[VB]  Dim bGeoidoEnable As Boolean
                    //[VB]  Dim sGeoidoPath As String
                    //[VB]  Dim bTkyEnable As Boolean
                    //[VB]  Dim sTkyPath As String
                    //[VB]  Dim bSemiDynaEnable As Boolean 'セミ・ダイナミック対応。'2009/11 H.Nakamura
                    //[VB]  Dim sSemiDynaPath As String 'セミ・ダイナミック対応。'2009/11 H.Nakamura
                    string sJobName;
                    string sDistrictName;
                    string sSupervisor;
                    long nCoordNum;
                    bool bGeoidoEnable;
                    string sGeoidoPath;
                    bool bTkyEnable;
                    string sTkyPath;
                    bool bSemiDynaEnable;   //'セミ・ダイナミック対応。'2009/11 H.Nakamura
                    string sSemiDynaPath= "";   //'セミ・ダイナミック対応。'2009/11 H.Nakamura

                    //-------------------------------------------------------
                    //[VB]  sJobName = GetString(clsFile.Number)
                    //******************************************
                    //  現場名をグローバル領域に設定
                    //******************************************
                    sJobName = FileRead_GetString(br);
                    Genba_S.sJobName = sJobName;
                    //-------------------------------------------------------
                    //[VB]  sDistrictName = GetString(clsFile.Number)
                    //******************************************
                    //  地区名をグローバル領域に設定
                    //******************************************
                    sDistrictName = FileRead_GetString(br);
                    Genba_S.sDistrictName = sDistrictName;

                    //[VB]  '↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑
                    //[VB]  '地区名より前の領域は ProjectFileManager 関係に影響するので注意。
                    //-------------------------------------------------------

                    //-------------------------------------------------------
                    //[VB]  If nVersion< 2300 Then
                    //[VB]  sSupervisor = ""
                    //[VB]  Else
                    //[VB]      sSupervisor = GetString(clsFile.Number)
                    //[VB]  End If
                    if (nVersion < 2300)    //8500
                    {
                        sSupervisor = "";
                    }
                    else
                    {
                        sSupervisor = FileRead_GetString(br);
                    }
                    Genba_S.sSupervisor = sSupervisor;  //""

                    //-------------------------------------------------------
                    //[VB]  Get #clsFile.Number, , nCoordNum
                    nCoordNum = br.ReadInt32();
                    Genba_S.nCoordNum = nCoordNum;      //7

                    //-------------------------------------------------------
                    //[VB]  Get #clsFile.Number, , bGeoidoEnable
                    bGeoidoEnable = GetFileBool(br);
                    Genba_S.bGeoidoEnable = bGeoidoEnable;  //true

                    //-------------------------------------------------------
                    //[VB]  sGeoidoPath = GetString(clsFile.Number)
                    sGeoidoPath = FileRead_GetString(br);   //"C:\Develop\パラメータファイル\gsigeome.ver3" 

                    //-------------------------------------------------------
                    //[VB]  Get #clsFile.Number, , bTkyEnable
                    bTkyEnable = GetFileBool(br);
                    // bTkyEnable = br.ReadBoolean();
                    Genba_S.bTkyEnable = bTkyEnable;    //false

                    //-------------------------------------------------------
                    //[VB]  sTkyPath = GetString(clsFile.Number)
                    sTkyPath = FileRead_GetString(br);
                    Genba_S.sTkyPath = sTkyPath;    ///""                   

                    //-------------------------------------------------------
                    //[VB]      'セミ・ダイナミック対応。'2009/11 H.Nakamura
                    //[VB]  If nVersion< 8100 Then
                    //[VB]      bSemiDynaEnable = False
                    //[VB]      sSemiDynaPath = SemiDynaPathDef
                    //[VB]  Else
                    //[VB]      Get #clsFile.Number, , bSemiDynaEnable
                    //[VB]      sSemiDynaPath = GetString(clsFile.Number)
                    //[VB]  End If
                    if (nVersion < 8100)
                    {
                        bSemiDynaEnable = false;
                        Genba_S.bSemiDynaEnable = bSemiDynaEnable;
                        //-------------->>>>>>>>>>>>>>>>>>  SemiDynaPath = SemiDynaPathDef;
                        //-------------->>>>>>>>>>>>>>>>>>   Genba_S.sSemiDynaPath = sSemiDynaPath;
                    }
                    else
                    {
                        bSemiDynaEnable = GetFileBool(br);
                        Genba_S.bSemiDynaEnable = bSemiDynaEnable;
                        //----------------------------------------
                        sSemiDynaPath = FileRead_GetString(br);
                        Genba_S.sSemiDynaPath = sSemiDynaPath;   //"C:\Develop\パラメータファイル\SemiDyna2009.par"
                    }

                    //-------------------------------------------------------
                    //[VB]  Dim clsNetworkModel As New NetworkModel
                    //[VB]  Call clsNetworkModel.Load(clsFile.Number, nVersion)
#if false
                    NetworkModel networkModel = new NetworkModel(mdlMain);
                    //  networkModel.Load( br, nVersion, ref Genba_S);
                    networkModel.Load(br, nVersion, ref List_Genba_S);
#else
                    m_clsNetworkModel.Load(br, nVersion, ref List_Genba_S);   //sakai
#endif

                    //[VB]  Dim clsBaseLineAnalysisParam As New BaseLineAnalysisParam
                    //[VB]  Call clsBaseLineAnalysisParam.Load(clsFile.Number, nVersion)
                    BaseLineAnalysisParam clsBaseLineAnalysisParam = new BaseLineAnalysisParam();
                    clsBaseLineAnalysisParam.Load(br, nVersion);

                    //[VB]  Dim clsAngleDiffParamRing As New AngleDiffParam
                    //[VB]  Call clsAngleDiffParamRing.Load(clsFile.Number, nVersion)
                    AngleDiffParam clsAngleDiffParamRing = new AngleDiffParam();
                    clsAngleDiffParamRing.Load(br, nVersion);

                    //[VB]  Dim clsAngleDiffParamBetween As New AngleDiffParam
                    //[VB]  Call clsAngleDiffParamBetween.Load(clsFile.Number, nVersion)
                    AngleDiffParam clsAngleDiffParamBetween = new AngleDiffParam();
                    clsAngleDiffParamBetween.Load(br, nVersion);

                    //[VB]  Dim clsAngleDiffParamHeight As New AngleDiffParam '2023/05/19 Hitz H.Nakamura 楕円体高の閉合差を追加。
                    //[VB]  If nVersion >= 9700 Then Call clsAngleDiffParamHeight.Load(clsFile.Number, nVersion)
                    AngleDiffParam clsAngleDiffParamHeight = new AngleDiffParam();       //'2023/05/19 Hitz H.Nakamura 楕円体高の閉合差を追加。
                    if (nVersion >= 9700)
                    {
                        clsAngleDiffParamHeight.Load(br, nVersion);
                    }

#if false
                    //[VB]  Dim clsAccountParamHand As New AccountParam
                    //[VB]  Call clsAccountParamHand.Load(clsFile.Number, nVersion)
                    AccountParam clsAccountParamHand = new AccountParam();
                    clsAccountParamHand.Load(br, nVersion);

                    //[VB]  Dim clsAccountParamWrite As New AccountParam
                    //[VB]  Call clsAccountParamWrite.Load(clsFile.Number, nVersion)
                    AccountParam clsAccountParamWrite = new AccountParam();
                    clsAccountParamWrite.Load(br, nVersion);

                    //[VB]  Dim clsAccountParamCoordinate As New AccountParam
                    //[VB]  Call clsAccountParamCoordinate.Load(clsFile.Number, nVersion)
                    AccountParam clsAccountParamCoordinate = new AccountParam();
                    clsAccountParamCoordinate.Load(br, nVersion);

                    //[VB]  Dim clsAccountOverlapParam As New AccountOverlapParam
                    //[VB]  Call clsAccountOverlapParam.Load(clsFile.Number, nVersion)
                    AccountOverlapParam clsAccountOverlapParam = new AccountOverlapParam();
                    clsAccountOverlapParam.Load(br, nVersion);

                    //[VB]  Dim clsAccountParamEccentricCorrect As New AccountParam
                    //[VB]  If nVersion >= 2200 Then Call clsAccountParamEccentricCorrect.Load(clsFile.Number, nVersion)
                    AccountParam clsAccountParamEccentricCorrect = new AccountParam();
                    if (nVersion >= 2200)
                    {
                        clsAccountParamEccentricCorrect.Load(br, nVersion);
                    }

                    //[VB]      'セミ・ダイナミック対応。'2009/11 H.Nakamura
                    //[VB]   Dim clsAccountParamSemiDyna As New AccountParam
                    //[VB]  If nVersion >= 8101 Then Call clsAccountParamSemiDyna.Load(clsFile.Number, nVersion)
                    AccountParam clsAccountParamSemiDyna = new AccountParam();
                    if (nVersion >= 8101)
                    {
                        clsAccountParamSemiDyna.Load(br, nVersion);
                    }

                    //[VB]  Dim clsAccountCadastralParam As New AccountCadastralParam
                    //[VB]  If nVersion >= 2400 Then Call clsAccountCadastralParam.Load(clsFile.Number, nVersion)
                    AccountCadastralParam clsAccountCadastralParam = new AccountCadastralParam();
                    if (nVersion >= 2400)
                    {
                        clsAccountCadastralParam.Load(br, nVersion);
                    }

                    //[VB]  Dim clsAutoOrderVectorParam As New AutoOrderVectorParam
                    //[VB]  If nVersion >= 2500 Then Call clsAutoOrderVectorParam.Load(clsFile.Number, nVersion)
                    AutoOrderVectorParam clsAutoOrderVectorParam = new AutoOrderVectorParam();
                    if (nVersion >= 2500)
                    {
                        clsAutoOrderVectorParam.Load(br, nVersion);
                    }

                    //[VB]  Dim clsAccountParamResultBase As New AccountParam   '2007/7/18 NGS Yamada
                    //[VB]  If nVersion >= 7900 Then Call clsAccountParamResultBase.Load(clsFile.Number, nVersion)
                    AccountParam clsAccountParamResultBase = new AccountParam();    //'2007/7/18 NGS Yamada
                    if (nVersion >= 7900)
                    {
                        clsAccountParamResultBase.Load(br, nVersion);
                    }
#endif
                    /*[VB]
                    Dim i As Long
                    Dim clsOutputParam(OUTPUT_TYPE_COUNT -1) As OutputParam
                    If nVersion< 3300 Then
                        For i = 0 To OUTPUT_TYPE_COUNT -1
                            Set clsOutputParam(i) = New OutputParam
                        Next
                        Call clsOutputParam(OUTPUT_TYPE_NVF).Load(clsFile.Number, nVersion)
                        Call clsOutputParam(OUTPUT_TYPE_JOB).Load(clsFile.Number, nVersion)
                        If nVersion >= 1500 Then Call clsOutputParam(OUTPUT_TYPE_RINEX).Load(clsFile.Number, nVersion)
                    ElseIf nVersion< 7100 Then
                        For i = 0 To OUTPUT_TYPE_CSV -1
                            Set clsOutputParam(i) = New OutputParam
                            Call clsOutputParam(i).Load(clsFile.Number, nVersion)
                        Next
                        Set clsOutputParam(OUTPUT_TYPE_CSV) = New OutputParam
                    Else
                        For i = 0 To OUTPUT_TYPE_COUNT -1
                            Set clsOutputParam(i) = New OutputParam
                            Call clsOutputParam(i).Load(clsFile.Number, nVersion)
                        Next
                    End If
                    [VB]*/
#if false
                    //[C#]
                    long i;
                    OutputParam[] clsOutputParam;
                    OutputParam[] clsOutputParam = new OutputParam[OUTPUT_TYPE_COUNT -1];

                    if (nVersion < 3300)
                    {
                        for (int i1 = 0; i1 < OUTPUT_TYPE_COUNT; i1++)
                        {
                            clsOutputParam[i1] = new OutputParam;
                        }
                        clsOutputParam(OUTPUT_TYPE_NVF).Load(clsFile.Number, nVersion);
                        clsOutputParam(OUTPUT_TYPE_JOB).Load(clsFile.Number, nVersion);
                        if (nVersion >= 1500)
                        {
                            clsOutputParam(OUTPUT_TYPE_RINEX).Load(clsFile.Number, nVersion);
                        }
                    }
                    else if (nVersion < 7100)
                    {
                        for (int i1 = 0; i1 < OUTPUT_TYPE_CSV; i1++)
                        {
                            clsOutputParam[i1] = new OutputParam;
                            clsOutputParam(i).Load(clsFile.Number, nVersion);
                        }
                        clsOutputParam[OUTPUT_TYPE_CSV] = new OutputParam;
                    }
                    else
                    {
                        for (int i1 = 0; i1 < OUTPUT_TYPE_COUNT; i1++)
                        {
                            clsOutputParam[i] = new OutputParam;
                            clsOutputParam(i).Load(clsFile.Number, nVersion);
                        }
                    }
#endif

                    /*[VB]
                    Dim clsDXFParam(DXF_TYPE_COUNT - 1) As DXFParam
                    For i = 0 To DXF_TYPE_COUNT -1
                        Set clsDXFParam(i) = New DXFParam
                        Call clsDXFParam(i).Load(clsFile.Number, nVersion)
                    Next
                    [VB]*/
                    //[C#]

                    /*[VB]
                    If nVersion >= 3200 Then
                        'チェックサム。
                        Dim nLoc As Long
                        nLoc = Loc(clsFile.Number)
                        Dim nSize As Long
                        Get #clsFile.Number, , nSize
                        If nLoc<> nSize Then Call Err.Raise(ERR_FILE, , "プロジェクトファイルの読み込みに失敗しました。")
                    End If
                    [VB]*/
                    //[C#]

                    /*[VB]
                    Call clsFile.CloseFile
                    [VB]*/
                    //[C#]

                    /*[VB]
                    '実変数へ代入。
                    m_sJobName = sJobName
                    m_sDistrictName = sDistrictName
                    m_sSupervisor = sSupervisor
                    m_nCoordNum = nCoordNum
                    m_bGeoidoEnable = bGeoidoEnable
                    m_sGeoidoPath = sGeoidoPath
                    m_bTkyEnable = bTkyEnable
                    m_sTkyPath = sTkyPath
                    m_bSemiDynaEnable = bSemiDynaEnable 'セミ・ダイナミック対応。'2009 / 11 H.Nakamura
                    m_sSemiDynaPath = sSemiDynaPath 'セミ・ダイナミック対応。'2009 / 11 H.Nakamura
                    Set m_clsNetworkModel = clsNetworkModel
                    Set m_clsBaseLineAnalysisParam = clsBaseLineAnalysisParam
                    Set m_clsAngleDiffParamRing = clsAngleDiffParamRing
                    Set m_clsAngleDiffParamBetween = clsAngleDiffParamBetween
                    Set m_clsAngleDiffParamHeight = clsAngleDiffParamHeight '2023/05/19 Hitz H.Nakamura 楕円体高の閉合差を追加。
                    Set m_clsAccountParamHand = clsAccountParamHand
                    Set m_clsAccountParamWrite = clsAccountParamWrite
                    Set m_clsAccountParamCoordinate = clsAccountParamCoordinate
                    Set m_clsAccountOverlapParam = clsAccountOverlapParam
                    Set m_clsAccountParamEccentricCorrect = clsAccountParamEccentricCorrect
                    Set m_clsAccountParamSemiDyna = clsAccountParamSemiDyna 'セミ・ダイナミック対応。'2009 / 11 H.Nakamura
                    Set m_clsAccountCadastralParam = clsAccountCadastralParam
                    Set m_clsAutoOrderVectorParam = clsAutoOrderVectorParam
                    Set m_clsAccountParamResultBase = clsAccountParamResultBase '2007/7/18 NGS Yamada
                    [VB]*/
                    //[C#]
                    //'実変数へ代入。
                    m_sJobName = sJobName;
                    m_sDistrictName = sDistrictName;
                    m_sSupervisor = sSupervisor;
                    m_nCoordNum = nCoordNum;
                    m_bGeoidoEnable = bGeoidoEnable;
                    m_sGeoidoPath = sGeoidoPath;
                    m_bTkyEnable = bTkyEnable;
                    m_sTkyPath = sTkyPath;
                    m_bSemiDynaEnable = bSemiDynaEnable;                                //'セミ・ダイナミック対応。'2009 / 11 H.Nakamura
                    m_sSemiDynaPath = sSemiDynaPath;                                    // 'セミ・ダイナミック対応。'2009 / 11 H.Nakamura
                    //m_clsNetworkModel = clsNetworkModel;
                    m_clsBaseLineAnalysisParam = clsBaseLineAnalysisParam;
                    m_clsAngleDiffParamRing = clsAngleDiffParamRing;
                    m_clsAngleDiffParamBetween = clsAngleDiffParamBetween;
                    m_clsAngleDiffParamHeight = clsAngleDiffParamHeight;                //'2023/05/19 Hitz H.Nakamura 楕円体高の閉合差を追加。
#if false
                    m_clsAccountParamHand = clsAccountParamHand;
                    m_clsAccountParamWrite = clsAccountParamWrite;
                    m_clsAccountParamCoordinate = clsAccountParamCoordinate;
                    m_clsAccountOverlapParam = clsAccountOverlapParam;
                    m_clsAccountParamEccentricCorrect = clsAccountParamEccentricCorrect;
                    m_clsAccountParamSemiDyna = clsAccountParamSemiDyna;                //'セミ・ダイナミック対応。'2009 / 11 H.Nakamura
                    m_clsAccountCadastralParam = clsAccountCadastralParam;

                    m_clsAutoOrderVectorParam = clsAutoOrderVectorParam;

                    m_clsAccountParamResultBase = clsAccountParamResultBase;            //'2007/7/18 NGS Yamada
#endif
                    /*[VB]
                    For i = 0 To OUTPUT_TYPE_COUNT -1
                        Set m_clsOutputParam(i) = clsOutputParam(i)
                    Next
                    For i = 0 To DXF_TYPE_COUNT -1
                        Set m_clsDXFParam(i) = clsDXFParam(i)
                    Next
                    [VB]*/
                    //[C#]
#if false
                    int i;
                    for (i = 0; i < (int)OUTPUT_TYPE.OUTPUT_TYPE_COUNT; i++)
                    {
                        m_clsOutputParam[i] = clsOutputParam[i];
                    }
                    for (i = 0; i < (int)DXF_TYPE.DXF_TYPE_COUNT; i++)
                    {
                        m_clsDXFParam[i] = clsDXFParam[i];
                    }
#endif
                    /*[VB]
                    m_sPath = sPath 'パスの更新。
                    m_bEmpty = False
                    m_bModifyed = False
                    [VB]*/
                    //[C#]
                    m_sPath = sPath;                //'パスの更新。
                    m_bEmpty = false;
                    m_bModifyed = false;
                }

                fs.Close();
            }
        }
        //--------------------------------------------------------------------------------------
        // K.setoguchi
        //--------------------------------------------------------------------------------------
        /// <summary>
        ///  '読み込み。 継承
        ///  '
        ///  'sPath で指定されたファイルから読み込む。
        ///  '
        ///  '引き数：
        ///  'sPath 保存ファイルのパス。
        /// 
        /// </summary>
        /// <param name="sPath"></param>
        public void Load(string sPath)
        {
            //'観測点ファイルのコピー。
            string sSrcObsPointPath;
            string sDstObsPointPath;
            sSrcObsPointPath = $"{sPath}{MdlNSSDefine.OBSPOINT_PATH}";     //"C:\Develop\NetSurv\Src\NS-App\NS-Survey\UserData\0006\.\ObsPoint\"
            string App_Path = @"C:\Develop\NetSurv\Src\NS-App\NS-Survey";
            sDstObsPointPath = $"{App_Path}{MdlNSDefine.TEMPORARY_PATH}{MdlNSSDefine.OBSPOINT_PATH}";  //"C:\Develop\NetSurv\Src\NS-App\NS-Survey\Temp\.\ObsPoint\"
            bool dmy1 = DeleteDir(sDstObsPointPath, true);
            bool dmy2 = CopyDir(sSrcObsPointPath, sDstObsPointPath, true);

            //-------------------------------------------------------------
            //[VB]  'テンポラリ変数に読み込む。
            //[VB]  Dim clsFile As New FileNumber
            //[VB]  Open sPath & DATA_FILE_NAME For Binary Access Read Lock Write As #clsFile.Number
            //[VB]      
            long nVersion;
            //-------------------------------------------------------
            string sJobName;
            string sDistrictName;
            string sSupervisor;
            long nCoordNum;
            bool bGeoidoEnable;
            string sGeoidoPath;
            bool bTkyEnable;
            string sTkyPath;
            bool bSemiDynaEnable;   //'セミ・ダイナミック対応。'2009/11 H.Nakamura
            string sSemiDynaPath;   //'セミ・ダイナミック対応。'2009/11 H.Nakamura

            using (var fs = System.IO.File.OpenRead($"{sPath}{GENBA_CONST.DATA_FILE_NAME}"))    //sPath:"C:\\Develop\\NetSurv\\Src\\NS-App\\NS-Survey\\UserData\\0006\\"
            {
                using (BinaryReader br = new BinaryReader(fs))
                {
                    //---------------------------------------------------
                    //[VB]      'ファイルバージョン。
                    nVersion = br.ReadInt32();
                    if ((nVersion < 1100) || (MdlNSDefine.DOCUMENT_FILE_VERSION < nVersion))
                    {
                        return;
                    }

                    //******************************************
                    //  現場名をグローバル領域に設定
                    //******************************************
                    sJobName = FileRead_GetString(br);
                    //-------------------------------------------------------
                    //[VB]  sDistrictName = GetString(clsFile.Number)
                    //******************************************
                    //  地区名をグローバル領域に設定
                    //******************************************
                    sDistrictName = FileRead_GetString(br);
                    //'↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑
                    //'地区名より前の領域は ProjectFileManager 関係に影響するので注意。
                    //-------------------------------------------------------

                    //-------------------------------------------------------
                    sSupervisor = nVersion < 2300 ? "" : FileRead_GetString(br);

                    //-------------------------------------------------------
                    nCoordNum = br.ReadInt32();
                    nCoordNum = nCoordNum;      //7

                    //-------------------------------------------------------
                    //[VB]  Get #clsFile.Number, , bGeoidoEnable
                    bGeoidoEnable = GetFileBool(br);
                    bGeoidoEnable = bGeoidoEnable;  //true

                    //-------------------------------------------------------
                    //[VB]  sGeoidoPath = GetString(clsFile.Number)
                    sGeoidoPath = FileRead_GetString(br);   //"C:\Develop\パラメータファイル\gsigeome.ver3" 

                    //-------------------------------------------------------
                    //[VB]  Get #clsFile.Number, , bTkyEnable
                    bTkyEnable = GetFileBool(br);
                    // bTkyEnable = br.ReadBoolean();
                    bTkyEnable = bTkyEnable;    //false

                    //-------------------------------------------------------
                    //[VB]  sTkyPath = GetString(clsFile.Number)
                    sTkyPath = FileRead_GetString(br);
                    sTkyPath = sTkyPath;    ///""                   

                    //-------------------------------------------------------
                    //[VB]      'セミ・ダイナミック対応。'2009/11 H.Nakamura
                    if (nVersion < 8100)
                    {
                        bSemiDynaEnable = false;
                        bSemiDynaEnable = bSemiDynaEnable;
                        sSemiDynaPath = SemiDynaPathDef;
                        sSemiDynaPath = sSemiDynaPath;
                    }
                    else
                    {
                        bSemiDynaEnable = GetFileBool(br); ;
                        bSemiDynaEnable = bSemiDynaEnable;
                        //----------------------------------------
                        sSemiDynaPath = FileRead_GetString(br);
                        sSemiDynaPath = sSemiDynaPath;   //"C:\Develop\パラメータファイル\SemiDyna2009.par"
                    }
                }
            }
            //[VB]  Dim clsNetworkModel As New NetworkModel
            //[VB]  Call clsNetworkModel.Load(clsFile.Number, nVersion)
            //[VB]  Dim clsBaseLineAnalysisParam As New BaseLineAnalysisParam
            //[VB]  Call clsBaseLineAnalysisParam.Load(clsFile.Number, nVersion)
            //[VB]  Dim clsAngleDiffParamRing As New AngleDiffParam
            //[VB]  Call clsAngleDiffParamRing.Load(clsFile.Number, nVersion)
            //[VB]  Dim clsAngleDiffParamBetween As New AngleDiffParam
            //[VB]  Call clsAngleDiffParamBetween.Load(clsFile.Number, nVersion)
            //[VB]  Dim clsAngleDiffParamHeight As New AngleDiffParam '2023/05/19 Hitz H.Nakamura 楕円体高の閉合差を追加。
            //[VB]  If nVersion >= 9700 Then Call clsAngleDiffParamHeight.Load(clsFile.Number, nVersion)
            //[VB]  Dim clsAccountParamHand As New AccountParam
            //[VB]  Call clsAccountParamHand.Load(clsFile.Number, nVersion)
            //[VB]  Dim clsAccountParamWrite As New AccountParam
            //[VB]  Call clsAccountParamWrite.Load(clsFile.Number, nVersion)
            //[VB]  Dim clsAccountParamCoordinate As New AccountParam
            //[VB]  Call clsAccountParamCoordinate.Load(clsFile.Number, nVersion)
            //[VB]  Dim clsAccountOverlapParam As New AccountOverlapParam
            //[VB]  Call clsAccountOverlapParam.Load(clsFile.Number, nVersion)
            //[VB]  Dim clsAccountParamEccentricCorrect As New AccountParam
            //[VB]  If nVersion >= 2200 Then Call clsAccountParamEccentricCorrect.Load(clsFile.Number, nVersion)
            //[VB]      'セミ・ダイナミック対応。'2009/11 H.Nakamura
            //[VB]   Dim clsAccountParamSemiDyna As New AccountParam
            //[VB]  If nVersion >= 8101 Then Call clsAccountParamSemiDyna.Load(clsFile.Number, nVersion)
            //[VB]  Dim clsAccountCadastralParam As New AccountCadastralParam
            //[VB]  If nVersion >= 2400 Then Call clsAccountCadastralParam.Load(clsFile.Number, nVersion)
            //[VB]  Dim clsAutoOrderVectorParam As New AutoOrderVectorParam
            //[VB]  If nVersion >= 2500 Then Call clsAutoOrderVectorParam.Load(clsFile.Number, nVersion)
            //[VB]  Dim clsAccountParamResultBase As New AccountParam   '2007/7/18 NGS Yamada
            //[VB]  If nVersion >= 7900 Then Call clsAccountParamResultBase.Load(clsFile.Number, nVersion)
            //[VB]  
            //[VB]  Dim i As Long
            //[VB]  Dim clsOutputParam(OUTPUT_TYPE_COUNT - 1) As OutputParam
            //[VB]  If nVersion< 3300 Then
            //[VB]      For i = 0 To OUTPUT_TYPE_COUNT - 1
            //[VB]          Set clsOutputParam(i) = New OutputParam
            //[VB]      Next
            //[VB]      Call clsOutputParam(OUTPUT_TYPE_NVF).Load(clsFile.Number, nVersion)
            //[VB]      Call clsOutputParam(OUTPUT_TYPE_JOB).Load(clsFile.Number, nVersion)
            //[VB]      If nVersion >= 1500 Then Call clsOutputParam(OUTPUT_TYPE_RINEX).Load(clsFile.Number, nVersion)
            //[VB]      ElseIf nVersion< 7100 Then
            //[VB]      For i = 0 To OUTPUT_TYPE_CSV - 1
            //[VB]          Set clsOutputParam(i) = New OutputParam
            //[VB]          Call clsOutputParam(i).Load(clsFile.Number, nVersion)
            //[VB]      Next
            //[VB]      Set clsOutputParam(OUTPUT_TYPE_CSV) = New OutputParam
            //[VB]  Else
            //[VB]      For i = 0 To OUTPUT_TYPE_COUNT - 1
            //[VB]      Set clsOutputParam(i) = New OutputParam
            //[VB]      Call clsOutputParam(i).Load(clsFile.Number, nVersion)
            //[VB]      Next
            //[VB]  End If
            //[VB]  Dim clsDXFParam(DXF_TYPE_COUNT - 1) As DXFParam
            //[VB]  For i = 0 To DXF_TYPE_COUNT - 1
            //[VB]      Set clsDXFParam(i) = New DXFParam
            //[VB]      Call clsDXFParam(i).Load(clsFile.Number, nVersion)
            //[VB]  Next
            //[VB]  
            //[VB]  If nVersion >= 3200 Then
            //[VB]  'チェックサム。
            //[VB]  Dim nLoc As Long
            //[VB]  nLoc = Loc(clsFile.Number)
            //[VB]  Dim nSize As Long
            //[VB]  Get #clsFile.Number, , nSize
            //[VB]  If nLoc<> nSize Then Call Err.Raise(ERR_FILE, , "プロジェクトファイルの読み込みに失敗しました。")
            //[VB]  End If
            //[VB]  
            //[VB]  
            //[VB]  Call clsFile.CloseFile

            //'実変数へ代入。
            m_sJobName = sJobName;
            m_sDistrictName = sDistrictName;
            m_sSupervisor = sSupervisor;
            m_nCoordNum = nCoordNum;
            m_bGeoidoEnable = bGeoidoEnable;
            m_sGeoidoPath = sGeoidoPath;
            m_bTkyEnable = bTkyEnable;
            m_sTkyPath = sTkyPath;
            m_bSemiDynaEnable = bSemiDynaEnable;        //'セミ・ダイナミック対応。'2009/11 H.Nakamura
            m_sSemiDynaPath = sSemiDynaPath;            //'セミ・ダイナミック対応。'2009/11 H.Nakamura
            //[VB]  Set m_clsNetworkModel = clsNetworkModel
            //[VB]  Set m_clsBaseLineAnalysisParam = clsBaseLineAnalysisParam
            //[VB]  Set m_clsAngleDiffParamRing = clsAngleDiffParamRing
            //[VB]  Set m_clsAngleDiffParamBetween = clsAngleDiffParamBetween
            //[VB]  Set m_clsAngleDiffParamHeight = clsAngleDiffParamHeight '2023/05/19 Hitz H.Nakamura 楕円体高の閉合差を追加。
            //[VB]  Set m_clsAccountParamHand = clsAccountParamHand
            //[VB]  Set m_clsAccountParamWrite = clsAccountParamWrite
            //[VB]  Set m_clsAccountParamCoordinate = clsAccountParamCoordinate
            //[VB]  Set m_clsAccountOverlapParam = clsAccountOverlapParam
            //[VB]  Set m_clsAccountParamEccentricCorrect = clsAccountParamEccentricCorrect
            //[VB]  Set m_clsAccountParamSemiDyna = clsAccountParamSemiDyna 'セミ・ダイナミック対応。'2009/11 H.Nakamura
            //[VB]  Set m_clsAccountCadastralParam = clsAccountCadastralParam
            //[VB]  Set m_clsAutoOrderVectorParam = clsAutoOrderVectorParam
            //[VB]  Set m_clsAccountParamResultBase = clsAccountParamResultBase '2007/7/18 NGS Yamada
            //[VB]  
            //[VB]  
            //[VB]  For i = 0 To OUTPUT_TYPE_COUNT - 1
            //[VB]      Set m_clsOutputParam(i) = clsOutputParam(i)
            //[VB]  Next
            //[VB]  For i = 0 To DXF_TYPE_COUNT - 1
            //[VB]      Set m_clsDXFParam(i) = clsDXFParam(i)
            //[VB]  Next
            //[VB]  
            m_sPath = sPath;    //'パスの更新。
            m_bEmpty = false;
            m_bModifyed = false;

        }
        //--------------------------------------------------------------------------------------

        //--------------------------------------------------------------------------------------
        //[VB]  '読み込み。
        //[VB]  '
        //[VB]  'sPath で指定されたファイルから読み込む。
        //[VB]  '
        //[VB]  '引き数：
        //[VB]  'sPath 保存ファイルのパス。
        //[VB]   Public Sub Load(ByVal sPath As String)
        //[VB]  
        //[VB]  '観測点ファイルのコピー。
        //[VB]  Dim sSrcObsPointPath As String
        //[VB]  Dim sDstObsPointPath As String
        //[VB]  sSrcObsPointPath = sPath & "." & OBSPOINT_PATH
        //[VB]  sDstObsPointPath = App.Path & TEMPORARY_PATH & "." & OBSPOINT_PATH
        //[VB]  Call DeleteDir(sDstObsPointPath, True)
        //[VB]  Call CopyDir(sSrcObsPointPath, sDstObsPointPath, True)
        //[VB]
        //[VB]      'テンポラリ変数に読み込む。
        //[VB]  Dim clsFile As New FileNumber
        //[VB]  Open sPath & DATA_FILE_NAME For Binary Access Read Lock Write As #clsFile.Number
        //[VB]      
        //[VB]      'ファイルバージョン。
        //[VB]  Dim nVersion As Long
        //[VB]  Get #clsFile.Number, , nVersion
        //[VB]  If nVersion< 1100 Or DOCUMENT_FILE_VERSION<nVersion Then
        //[VB]      RaiseEvent RejectedFileVersion(sPath, nVersion)
        //[VB]      Exit Sub
        //[VB]  End If
        //[VB]
        //[VB]  Dim sJobName As String
        //[VB]  Dim sDistrictName As String
        //[VB]  Dim sSupervisor As String
        //[VB]  Dim nCoordNum As Long
        //[VB]  Dim bGeoidoEnable As Boolean
        //[VB]  Dim sGeoidoPath As String
        //[VB]  Dim bTkyEnable As Boolean
        //[VB]  Dim sTkyPath As String
        //[VB]  Dim bSemiDynaEnable As Boolean 'セミ・ダイナミック対応。'2009/11 H.Nakamura
        //[VB]  Dim sSemiDynaPath As String 'セミ・ダイナミック対応。'2009/11 H.Nakamura
        //[VB]  
        //[VB]  sJobName = GetString(clsFile.Number)
        //[VB]  sDistrictName = GetString(clsFile.Number)
        //[VB]  '↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑
        //[VB]  '地区名より前の領域は ProjectFileManager 関係に影響するので注意。
        //[VB]
        //[VB]  If nVersion< 2300 Then
        //[VB]  sSupervisor = ""
        //[VB]  Else
        //[VB]      sSupervisor = GetString(clsFile.Number)
        //[VB]  End If
        //[VB]  Get #clsFile.Number, , nCoordNum
        //[VB]  Get #clsFile.Number, , bGeoidoEnable
        //[VB]  sGeoidoPath = GetString(clsFile.Number)
        //[VB]  Get #clsFile.Number, , bTkyEnable
        //[VB]  sTkyPath = GetString(clsFile.Number)
        //[VB]  
        //[VB]      'セミ・ダイナミック対応。'2009/11 H.Nakamura
        //[VB]  If nVersion< 8100 Then
        //[VB]      bSemiDynaEnable = False
        //[VB]      sSemiDynaPath = SemiDynaPathDef
        //[VB]  Else
        //[VB]      Get #clsFile.Number, , bSemiDynaEnable
        //[VB]  sSemiDynaPath = GetString(clsFile.Number)
        //[VB]  End If
        //[VB]  
        //[VB]  Dim clsNetworkModel As New NetworkModel
        //[VB]  Call clsNetworkModel.Load(clsFile.Number, nVersion)
        //[VB]  Dim clsBaseLineAnalysisParam As New BaseLineAnalysisParam
        //[VB]  Call clsBaseLineAnalysisParam.Load(clsFile.Number, nVersion)
        //[VB]  Dim clsAngleDiffParamRing As New AngleDiffParam
        //[VB]  Call clsAngleDiffParamRing.Load(clsFile.Number, nVersion)
        //[VB]  Dim clsAngleDiffParamBetween As New AngleDiffParam
        //[VB]  Call clsAngleDiffParamBetween.Load(clsFile.Number, nVersion)
        //[VB]  Dim clsAngleDiffParamHeight As New AngleDiffParam '2023/05/19 Hitz H.Nakamura 楕円体高の閉合差を追加。
        //[VB]  If nVersion >= 9700 Then Call clsAngleDiffParamHeight.Load(clsFile.Number, nVersion)
        //[VB]  Dim clsAccountParamHand As New AccountParam
        //[VB]  Call clsAccountParamHand.Load(clsFile.Number, nVersion)
        //[VB]  Dim clsAccountParamWrite As New AccountParam
        //[VB]  Call clsAccountParamWrite.Load(clsFile.Number, nVersion)
        //[VB]  Dim clsAccountParamCoordinate As New AccountParam
        //[VB]  Call clsAccountParamCoordinate.Load(clsFile.Number, nVersion)
        //[VB]  Dim clsAccountOverlapParam As New AccountOverlapParam
        //[VB]  Call clsAccountOverlapParam.Load(clsFile.Number, nVersion)
        //[VB]  Dim clsAccountParamEccentricCorrect As New AccountParam
        //[VB]  If nVersion >= 2200 Then Call clsAccountParamEccentricCorrect.Load(clsFile.Number, nVersion)
        //[VB]      'セミ・ダイナミック対応。'2009/11 H.Nakamura
        //[VB]   Dim clsAccountParamSemiDyna As New AccountParam
        //[VB]  If nVersion >= 8101 Then Call clsAccountParamSemiDyna.Load(clsFile.Number, nVersion)
        //[VB]  Dim clsAccountCadastralParam As New AccountCadastralParam
        //[VB]  If nVersion >= 2400 Then Call clsAccountCadastralParam.Load(clsFile.Number, nVersion)
        //[VB]  Dim clsAutoOrderVectorParam As New AutoOrderVectorParam
        //[VB]  If nVersion >= 2500 Then Call clsAutoOrderVectorParam.Load(clsFile.Number, nVersion)
        //[VB]  Dim clsAccountParamResultBase As New AccountParam   '2007/7/18 NGS Yamada
        //[VB]  If nVersion >= 7900 Then Call clsAccountParamResultBase.Load(clsFile.Number, nVersion)
        //[VB]  
        //[VB]  Dim i As Long
        //[VB]  Dim clsOutputParam(OUTPUT_TYPE_COUNT - 1) As OutputParam
        //[VB]  If nVersion< 3300 Then
        //[VB]      For i = 0 To OUTPUT_TYPE_COUNT - 1
        //[VB]          Set clsOutputParam(i) = New OutputParam
        //[VB]      Next
        //[VB]      Call clsOutputParam(OUTPUT_TYPE_NVF).Load(clsFile.Number, nVersion)
        //[VB]      Call clsOutputParam(OUTPUT_TYPE_JOB).Load(clsFile.Number, nVersion)
        //[VB]      If nVersion >= 1500 Then Call clsOutputParam(OUTPUT_TYPE_RINEX).Load(clsFile.Number, nVersion)
        //[VB]      ElseIf nVersion< 7100 Then
        //[VB]      For i = 0 To OUTPUT_TYPE_CSV - 1
        //[VB]          Set clsOutputParam(i) = New OutputParam
        //[VB]          Call clsOutputParam(i).Load(clsFile.Number, nVersion)
        //[VB]      Next
        //[VB]      Set clsOutputParam(OUTPUT_TYPE_CSV) = New OutputParam
        //[VB]  Else
        //[VB]      For i = 0 To OUTPUT_TYPE_COUNT - 1
        //[VB]      Set clsOutputParam(i) = New OutputParam
        //[VB]      Call clsOutputParam(i).Load(clsFile.Number, nVersion)
        //[VB]      Next
        //[VB]  End If
        //[VB]  Dim clsDXFParam(DXF_TYPE_COUNT - 1) As DXFParam
        //[VB]  For i = 0 To DXF_TYPE_COUNT - 1
        //[VB]      Set clsDXFParam(i) = New DXFParam
        //[VB]      Call clsDXFParam(i).Load(clsFile.Number, nVersion)
        //[VB]  Next
        //[VB]  
        //[VB]  If nVersion >= 3200 Then
        //[VB]  'チェックサム。
        //[VB]  Dim nLoc As Long
        //[VB]  nLoc = Loc(clsFile.Number)
        //[VB]  Dim nSize As Long
        //[VB]  Get #clsFile.Number, , nSize
        //[VB]  If nLoc<> nSize Then Call Err.Raise(ERR_FILE, , "プロジェクトファイルの読み込みに失敗しました。")
        //[VB]  End If
        //[VB]  
        //[VB]  
        //[VB]  Call clsFile.CloseFile
        //[VB]  
        //[VB]  '実変数へ代入。
        //[VB]  m_sJobName = sJobName
        //[VB]  m_sDistrictName = sDistrictName
        //[VB]  m_sSupervisor = sSupervisor
        //[VB]  m_nCoordNum = nCoordNum
        //[VB]  m_bGeoidoEnable = bGeoidoEnable
        //[VB]  m_sGeoidoPath = sGeoidoPath
        //[VB]  m_bTkyEnable = bTkyEnable
        //[VB]  m_sTkyPath = sTkyPath
        //[VB]  m_bSemiDynaEnable = bSemiDynaEnable 'セミ・ダイナミック対応。'2009/11 H.Nakamura
        //[VB]  m_sSemiDynaPath = sSemiDynaPath 'セミ・ダイナミック対応。'2009/11 H.Nakamura
        //[VB]  Set m_clsNetworkModel = clsNetworkModel
        //[VB]  Set m_clsBaseLineAnalysisParam = clsBaseLineAnalysisParam
        //[VB]  Set m_clsAngleDiffParamRing = clsAngleDiffParamRing
        //[VB]  Set m_clsAngleDiffParamBetween = clsAngleDiffParamBetween
        //[VB]  Set m_clsAngleDiffParamHeight = clsAngleDiffParamHeight '2023/05/19 Hitz H.Nakamura 楕円体高の閉合差を追加。
        //[VB]  Set m_clsAccountParamHand = clsAccountParamHand
        //[VB]  Set m_clsAccountParamWrite = clsAccountParamWrite
        //[VB]  Set m_clsAccountParamCoordinate = clsAccountParamCoordinate
        //[VB]  Set m_clsAccountOverlapParam = clsAccountOverlapParam
        //[VB]  Set m_clsAccountParamEccentricCorrect = clsAccountParamEccentricCorrect
        //[VB]  Set m_clsAccountParamSemiDyna = clsAccountParamSemiDyna 'セミ・ダイナミック対応。'2009/11 H.Nakamura
        //[VB]  Set m_clsAccountCadastralParam = clsAccountCadastralParam
        //[VB]  Set m_clsAutoOrderVectorParam = clsAutoOrderVectorParam
        //[VB]  Set m_clsAccountParamResultBase = clsAccountParamResultBase '2007/7/18 NGS Yamada
        //[VB]  
        //[VB]  
        //[VB]  For i = 0 To OUTPUT_TYPE_COUNT - 1
        //[VB]      Set m_clsOutputParam(i) = clsOutputParam(i)
        //[VB]  Next
        //[VB]  For i = 0 To DXF_TYPE_COUNT - 1
        //[VB]      Set m_clsDXFParam(i) = clsDXFParam(i)
        //[VB]  Next
        //[VB]  
        //[VB]  m_sPath = sPath 'パスの更新。
        //[VB]  m_bEmpty = False
        //[VB]  m_bModifyed = False
        //[VB]  
        //[VB]  
        //[VB]  End Sub
        //[VB]  
        //**************************************************************************************
        //**************************************************************************************








        //==========================================================================================
        /*[VB]
        '現場を設定する。
        '
        '引き数：
        'sJobName 現場名。
        'sDistrictName 地区名。
        'nCoordNum 座標系番号(1～19)。
        'bGeoidoEnable ジオイド有効/無効。True=有効。False=無効。
        'sGeoidoPath ジオイドモデルのパス。
        'bSemiDynaEnable セミ・ダイナミック有効/無効。True=有効。False=無効。'2009/11 H.Nakamura
        'sSemiDynaPath セミ・ダイナミックパラメータファイルのパス。'2009/11 H.Nakamura
        Public Function SetJob(ByVal sJobName As String, ByVal sDistrictName As String, ByVal nCoordNum As Long, ByVal bGeoidoEnable As Boolean, ByVal sGeoidoPath As String, ByVal bSemiDynaEnable As Boolean, ByVal sSemiDynaPath As String)
            m_sJobName = sJobName
            m_sDistrictName = sDistrictName
            m_nCoordNum = nCoordNum
            m_bGeoidoEnable = bGeoidoEnable
            m_sGeoidoPath = sGeoidoPath
            m_bSemiDynaEnable = bSemiDynaEnable 'セミ・ダイナミック対応。'2009/11 H.Nakamura
            m_sSemiDynaPath = sSemiDynaPath 'セミ・ダイナミック対応。'2009/11 H.Nakamura
            m_bEmpty = False
            m_bModifyed = True
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '現場を設定する。
        '
        '引き数：
        'sJobName 現場名。
        'sDistrictName 地区名。
        'nCoordNum 座標系番号(1～19)。
        'bGeoidoEnable ジオイド有効/無効。True=有効。False=無効。
        'sGeoidoPath ジオイドモデルのパス。
        'bSemiDynaEnable セミ・ダイナミック有効/無効。True=有効。False=無効。'2009/11 H.Nakamura
        'sSemiDynaPath セミ・ダイナミックパラメータファイルのパス。'2009/11 H.Nakamura
        */
        public bool SetJob(string sJobName, string sDistrictName, long nCoordNum, bool bGeoidoEnable, string sGeoidoPath, bool bSemiDynaEnable, string sSemiDynaPath)
        {
            m_sJobName = sJobName;
            m_sDistrictName = sDistrictName;
            m_nCoordNum = nCoordNum;
            m_bGeoidoEnable = bGeoidoEnable;
            m_sGeoidoPath = sGeoidoPath;
            m_bSemiDynaEnable = bSemiDynaEnable;        //'セミ・ダイナミック対応。'2009 / 11 H.Nakamura
            m_sSemiDynaPath = sSemiDynaPath;            //'セミ・ダイナミック対応。'2009 / 11 H.Nakamura
            m_bEmpty = false;
            m_bModifyed = true;
            return true;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '外部ファイルをインポートする。
        '
        '引き数：
        'nImportType インポートファイル種別。
        'sPath インポートするファイルのパス。参照する配列の要素は(0 To ...)。Ubound 関数を使用する。
        'clsProgressInterface ProgressInterface オブジェクト。
        '
        '戻り値：
        '全て正常にインポートできた場合は True を返す。
        '無効なデータがあった場合は False を返す。
        Public Function Import(ByVal nImportType As IMPORT_TYPE, ByRef sPath() As String, ByVal clsProgressInterface As ProgressInterface) As Boolean

            Import = False
    
            '観測点フォルダ。
            Dim sObsPointPath As String
            sObsPointPath = App.Path & TEMPORARY_PATH & "." & OBSPOINT_PATH
    
            '読み込み。
            Import = m_clsNetworkModel.Import(nImportType, sPath, sObsPointPath, SameTimeMin, SatelliteCountMin, clsProgressInterface)
    
            '全ての偏心補正を再計算する。
            Dim clsChainList As ChainList
            Set clsChainList = m_clsNetworkModel.RepresentPointHead
            Do While Not clsChainList Is Nothing
                Dim clsObservationPoint As ObservationPoint
                Set clsObservationPoint = clsChainList.Element
                If clsObservationPoint.Eccentric And clsObservationPoint.PrevPoint Is Nothing Then
                    Call m_clsNetworkModel.CorrectEccentric(clsObservationPoint)
                    Call m_clsNetworkModel.EnableGenuinePoint(clsObservationPoint)
                End If
                Set clsChainList = clsChainList.NextList
            Loop

            m_bModifyed = True


        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '外部ファイルをインポートする。
        '
        '引き数：
        'nImportType インポートファイル種別。
        'sPath インポートするファイルのパス。参照する配列の要素は(0 To ...)。Ubound 関数を使用する。
        'clsProgressInterface ProgressInterface オブジェクト。
        '
        '戻り値：
        '全て正常にインポートできた場合は True を返す。
        '無効なデータがあった場合は False を返す。
        */
        public bool Import(IMPORT_TYPE nImportType, string sPath, ProgressInterface clsProgressInterface)
        {
            /*
            bool w_Import = false;

            //'観測点フォルダ。
            string sObsPointPath;
            sObsPointPath = app.Path & TEMPORARY_PATH & "." & OBSPOINT_PATH;


            //'読み込み。
            w_Import = m_clsNetworkModel.Import(nImportType, sPath, sObsPointPath, SameTimeMin, SatelliteCountMin, clsProgressInterface);


            //'全ての偏心補正を再計算する。
            ChainList clsChainList;
            clsChainList = m_clsNetworkModel.RepresentPointHead;
            do
            {
                ObservationPoint clsObservationPoint;
                clsObservationPoint = clsChainList.Element;
                if (clsObservationPoint.Eccentric && clsObservationPoint.PrevPoint == null)
                {
                    clsNetworkModel.CorrectEccentric(clsObservationPoint);
                    m_clsNetworkModel.EnableGenuinePoint(clsObservationPoint);
                }
                clsChainList = clsChainList.NextList();
            } while (clsChainList != null);

            m_bModifyed = true;
            */

            return false;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '指定された基線ベクトルの有効/無効を設定する。
        '
        '設定が変更されたオブジェクトの IsList をONに設定する。
        '
        '引き数：
        'clsBaseLineVector 対象とする基線ベクトル。
        'bEnable 有効フラグ。
        Public Sub EnableBaseLineVector(ByVal clsBaseLineVector As BaseLineVector, ByVal bEnable As Boolean)

            '有効/無効の設定。
            Call m_clsNetworkModel.EnableBaseLineVector(clsBaseLineVector, bEnable)
    
            '偏心補正の再計算。
            If clsBaseLineVector.StrPoint.Eccentric Then
                Call m_clsNetworkModel.CorrectEccentric(clsBaseLineVector.StrPoint)
                Call m_clsNetworkModel.EnableGenuinePoint(clsBaseLineVector.StrPoint)
                Call m_clsNetworkModel.SetConnectBaseLineVectorsIsListEx(clsBaseLineVector.StrPoint)
            End If
            If clsBaseLineVector.EndPoint.Eccentric Then
                Call m_clsNetworkModel.CorrectEccentric(clsBaseLineVector.EndPoint)
                Call m_clsNetworkModel.EnableGenuinePoint(clsBaseLineVector.EndPoint)
                Call m_clsNetworkModel.SetConnectBaseLineVectorsIsListEx(clsBaseLineVector.EndPoint)
            End If

            m_bModifyed = True


        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '指定された基線ベクトルの有効/無効を設定する。
        '
        '設定が変更されたオブジェクトの IsList をONに設定する。
        '
        '引き数：
        'clsBaseLineVector 対象とする基線ベクトル。
        'bEnable 有効フラグ。
        */
        public void EnableBaseLineVector(BaseLineVector clsBaseLineVector, bool bEnable)
        {

            //'有効/無効の設定。
            m_clsNetworkModel.EnableBaseLineVector(clsBaseLineVector, bEnable);


            //'偏心補正の再計算。
            if (clsBaseLineVector.StrPoint().Eccentric())
            {
                m_clsNetworkModel.CorrectEccentric(clsBaseLineVector.StrPoint());
                m_clsNetworkModel.EnableGenuinePoint(clsBaseLineVector.StrPoint());
                m_clsNetworkModel.SetConnectBaseLineVectorsIsListEx(clsBaseLineVector.StrPoint());
            }
            if (clsBaseLineVector.EndPoint().Eccentric())
            {
                m_clsNetworkModel.CorrectEccentric(clsBaseLineVector.EndPoint());
                m_clsNetworkModel.EnableGenuinePoint(clsBaseLineVector.EndPoint());
                m_clsNetworkModel.SetConnectBaseLineVectorsIsListEx(clsBaseLineVector.EndPoint());
            }

            m_bModifyed = true;

            return;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '指定された観測点の有効/無効を設定する。
        '
        '設定が変更されたオブジェクトの IsList をONに設定する。
        '
        '引き数：
        'clsObservationPoint 対象とする観測点(代表観測点)。
        'bEnable 有効フラグ。
        Public Sub EnableObservationPoint(ByVal clsObservationPoint As ObservationPoint, ByVal bEnable As Boolean)

            '接続している基線ベクトルを取得する。
            Dim clsBaseLineVectors() As BaseLineVector
            ReDim clsBaseLineVectors(-1 To -1)
            Call m_clsNetworkModel.GetConnectBaseLineVectors(clsObservationPoint, clsBaseLineVectors)
            '偏心補正の再計算が必要な偏心点。
            Dim objEccentricPoints As New Collection
            If clsObservationPoint.Eccentric Then Call SetAtCollectionObject(objEccentricPoints, clsObservationPoint.HeadPoint, clsObservationPoint.Number)
            Dim i As Long
            For i = 0 To UBound(clsBaseLineVectors)
                If clsBaseLineVectors(i).StrPoint.Eccentric Then Call SetAtCollectionObject(objEccentricPoints, clsBaseLineVectors(i).StrPoint.HeadPoint, clsBaseLineVectors(i).StrPoint.Number)
                If clsBaseLineVectors(i).EndPoint.Eccentric Then Call SetAtCollectionObject(objEccentricPoints, clsBaseLineVectors(i).EndPoint.HeadPoint, clsBaseLineVectors(i).EndPoint.Number)
            Next
    
            '有効/無効の設定。
            Call m_clsNetworkModel.EnableObservationPoint(clsObservationPoint, bEnable)
    
            '偏心補正の再計算。
            Dim clsEccentricPoint As ObservationPoint
            For Each clsEccentricPoint In objEccentricPoints
                Call m_clsNetworkModel.CorrectEccentric(clsEccentricPoint)
                Call m_clsNetworkModel.EnableGenuinePoint(clsEccentricPoint)
                Call m_clsNetworkModel.SetConnectBaseLineVectorsIsListEx(clsEccentricPoint)
            Next


            m_bModifyed = True

        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]          //修正要 saka -> K.Setoguchi(3)
        /*
        '指定された観測点の有効/無効を設定する。
        '
        '設定が変更されたオブジェクトの IsList をONに設定する。
        '
        '引き数：
        'clsObservationPoint 対象とする観測点(代表観測点)。
        'bEnable 有効フラグ。
        */
        public void EnableObservationPoint(ObservationPoint clsObservationPoint, bool bEnable)
        {
            /*---------------------------------------------------------------------------------------------------------------
                '接続している基線ベクトルを取得する。
                Dim clsBaseLineVectors() As BaseLineVector
                ReDim clsBaseLineVectors(-1 To - 1)
                Call m_clsNetworkModel.GetConnectBaseLineVectors(clsObservationPoint, clsBaseLineVectors)
            */

            List<BaseLineVector> clsBaseLineVectors = new List<BaseLineVector>();
            //  m_clsNetworkModel.GetConnectBaseLineVectors(clsObservationPoint, ref clsBaseLineVectors);
            Session_GetConnectBaseLineVectors(clsObservationPoint, ref clsBaseLineVectors);

            /*---------------------------------------------------------------------------------------------------------------
                '偏心補正の再計算が必要な偏心点。
                Dim objEccentricPoints As New Collection
                If clsObservationPoint.Eccentric Then Call SetAtCollectionObject(objEccentricPoints, clsObservationPoint.HeadPoint, clsObservationPoint.Number)
                Dim i As Long
                For i = 0 To UBound(clsBaseLineVectors)
                    If clsBaseLineVectors(i).StrPoint.Eccentric Then Call SetAtCollectionObject(objEccentricPoints, clsBaseLineVectors(i).StrPoint.HeadPoint, clsBaseLineVectors(i).StrPoint.Number)
                    If clsBaseLineVectors(i).EndPoint.Eccentric Then Call SetAtCollectionObject(objEccentricPoints, clsBaseLineVectors(i).EndPoint.HeadPoint, clsBaseLineVectors(i).EndPoint.Number)
                Next
            -----------------------------------------------------------------------------------------------------------------*/
            //'偏心補正の再計算が必要な偏心点。
            //      Dim objEccentricPoints As New Collection
            Dictionary<string, object> objEccentricPoints = new Dictionary<string, object>();
            if (clsObservationPoint.Eccentric())
            {
                SetAtCollectionObject(objEccentricPoints, clsObservationPoint.HeadPoint(), clsObservationPoint.Number());

                for (int i = 0; i < clsBaseLineVectors.Count; i++)
                {
                    if (clsBaseLineVectors[i].StrPoint().Eccentric())
                    {
                        SetAtCollectionObject(objEccentricPoints, clsBaseLineVectors[i].StrPoint().HeadPoint(), clsBaseLineVectors[i].StrPoint().Number());
                    }

                    if (clsBaseLineVectors[i].EndPoint().Eccentric())
                    {
                        SetAtCollectionObject(objEccentricPoints, clsBaseLineVectors[i].EndPoint().HeadPoint(), clsBaseLineVectors[i].EndPoint().Number());
                    }
                }
            }

            //有効/無効の設定。
            m_clsNetworkModel.EnableObservationPoint(clsObservationPoint, bEnable);

            //'偏心補正の再計算。
            ObservationPoint clsEccentricPoint;
            foreach (object Point in objEccentricPoints)
            {
                clsEccentricPoint = (ObservationPoint)Point;
                m_clsNetworkModel.CorrectEccentric(clsEccentricPoint);
                m_clsNetworkModel.EnableGenuinePoint(clsEccentricPoint);
                m_clsNetworkModel.SetConnectBaseLineVectorsIsListEx(clsEccentricPoint);
            }

            m_bModifyed = true;


        }

        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '観測点を結合する。
        '
        '引き数：
        'clsSrcObservationPoint 結合元の観測点(代表観測点)。
        'clsDstObservationPoint 結合先の観測点(代表観測点)。
        Public Sub CombinationObservationPoint(ByVal clsSrcObservationPoint As ObservationPoint, ByVal clsDstObservationPoint As ObservationPoint)

            '結合元が偏心点である場合、偏心補正をOFFにする。
            If clsSrcObservationPoint.Eccentric Then Call RemoveGenuinePoint(clsSrcObservationPoint.CorrectPoint)

            '接続基線ベクトルが方位標である観測点を取得する。
            Dim clsBaseLineVectors() As BaseLineVector
            ReDim clsBaseLineVectors(-1 To -1)
            Call m_clsNetworkModel.GetConnectBaseLineVectorsEx(clsSrcObservationPoint, clsBaseLineVectors)
            Dim objEccentricPoints As Collection
            Set objEccentricPoints = m_clsNetworkModel.GetMarkedEccentricPoints(clsBaseLineVectors)


            Call m_clsNetworkModel.CombinationObservationPoint(clsSrcObservationPoint, clsDstObservationPoint)

            '方位標を反映させる。
            Dim clsEccentricPoint As ObservationPoint
            For Each clsEccentricPoint In objEccentricPoints
                '2009/11 H.Nakamura
                'clsEccentricPoint.EccentricCorrectionParam.Marker = clsEccentricPoint.Owner.Key
                If clsEccentricPoint.EccentricCorrectionParam.AngleType = ANGLETYPE_HORIZONTAL Then
                    clsEccentricPoint.EccentricCorrectionParam.Marker = clsEccentricPoint.Owner.Key
                Else
                    clsEccentricPoint.EccentricCorrectionParam.UsePoint = clsEccentricPoint.Owner.Key
                End If
            Next

            '結合先が偏心点である場合、偏心補正を再計算する。
            If clsDstObservationPoint.Eccentric Then
                Call m_clsNetworkModel.CorrectEccentric(clsDstObservationPoint)
                Call m_clsNetworkModel.EnableGenuinePoint(clsDstObservationPoint)
            End If

            m_bModifyed = True


        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '観測点を結合する。
        '
        '引き数：
        'clsSrcObservationPoint 結合元の観測点(代表観測点)。
        'clsDstObservationPoint 結合先の観測点(代表観測点)。
        */
        public void CombinationObservationPoint(ObservationPoint clsSrcObservationPoint, ObservationPoint clsDstObservationPoint)
        {
            return;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '観測点の共通属性を設定する。
        '
        '引き数：
        'clsObservationPoint 対象とする観測点(代表観測点)。
        'sNumber 観測点番号。
        'sName 観測点名称。
        'bFixed 固定点フラグ。
        'clsCoordinatePointFix 固定座標。
        Public Sub SetAttributeCommon(ByVal clsObservationPoint As ObservationPoint, ByVal sNumber As String, ByVal sName As String, ByVal bFixed As Boolean, ByVal clsCoordinatePointFix As CoordinatePointFix)

            '接続基線ベクトルが方位標である観測点を取得する。
            Dim clsBaseLineVectors() As BaseLineVector
            ReDim clsBaseLineVectors(-1 To -1)
            Call m_clsNetworkModel.GetConnectBaseLineVectorsEx(clsObservationPoint, clsBaseLineVectors)
            Dim objEccentricPoints As Collection
            Set objEccentricPoints = m_clsNetworkModel.GetMarkedEccentricPoints(clsBaseLineVectors)
    
            '設定。
            clsObservationPoint.Number = sNumber
            clsObservationPoint.Name = sName
            clsObservationPoint.Fixed = bFixed
            If clsObservationPoint.Fixed Then Let clsObservationPoint.CoordinateFixed = clsCoordinatePointFix
    
            '偏心点なら本点の情報も更新する。
            If clsObservationPoint.Eccentric Then Call clsObservationPoint.CorrectPoint.UpdateGenuineInfo
    
            '方位標を反映させる。
            Dim clsEccentricPoint As ObservationPoint
            For Each clsEccentricPoint In objEccentricPoints
                '2009/11 H.Nakamura
                'clsEccentricPoint.EccentricCorrectionParam.Marker = clsEccentricPoint.Owner.Key
                If clsEccentricPoint.EccentricCorrectionParam.AngleType = ANGLETYPE_HORIZONTAL Then
                    clsEccentricPoint.EccentricCorrectionParam.Marker = clsEccentricPoint.Owner.Key
                Else
                    clsEccentricPoint.EccentricCorrectionParam.UsePoint = clsEccentricPoint.Owner.Key
                End If
            Next
    
            '観測点ファイル名の更新。
            Dim sDirPath As String
            sDirPath = App.Path & TEMPORARY_PATH & "." & OBSPOINT_PATH
            Dim clsRepresentPoint As ObservationPoint
            Set clsRepresentPoint = clsObservationPoint.HeadPoint
            Do While Not clsRepresentPoint Is Nothing
                Call clsRepresentPoint.UpdateFileName(sDirPath)
                Set clsRepresentPoint = clsRepresentPoint.NextPoint
            Loop
    
            '偏心補正の再計算。
            If clsObservationPoint.Eccentric Then Call m_clsNetworkModel.CorrectEccentric(clsObservationPoint)

            m_bModifyed = True


        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]      //2
        /*
        '観測点の共通属性を設定する。
        '
        '引き数：
        'clsObservationPoint 対象とする観測点(代表観測点)。
        'sNumber 観測点番号。
        'sName 観測点名称。
        'bFixed 固定点フラグ。
        'clsCoordinatePointFix 固定座標。
        */
        public void SetAttributeCommon(ObservationPoint clsObservationPoint, string sNumber, string sName, bool bFixed, CoordinatePointFix clsCoordinatePointFix)
        {

            //'接続基線ベクトルが方位標である観測点を取得する。
            List<BaseLineVector> clsBaseLineVectors = new List<BaseLineVector>();
            clsBaseLineVectors.Clear();
            m_clsNetworkModel.GetConnectBaseLineVectorsEx(clsObservationPoint, clsBaseLineVectors);
            List<ObservationPoint> objEccentricPoints = new List<ObservationPoint>();
            objEccentricPoints = (List<ObservationPoint>)m_clsNetworkModel.GetMarkedEccentricPoints(clsBaseLineVectors);

            //'設定。
            clsObservationPoint.Number(sNumber);
            clsObservationPoint.Name(sName);
            clsObservationPoint.Fixed(bFixed);
            if(clsObservationPoint.Fixed())
            {
                clsObservationPoint.CoordinateFixed(clsCoordinatePointFix);
            }
            //'偏心点なら本点の情報も更新する。
            if (clsObservationPoint.Eccentric())
            {
                clsObservationPoint.CorrectPoint().UpdateGenuineInfo();

            }

#if false
            //'方位標を反映させる。
            foreach (ObservationPoint clsEccentricPoint in objEccentricPoints)
            {
                //  '2009/11 H.Nakamura
                //  'clsEccentricPoint.EccentricCorrectionParam.Marker = clsEccentricPoint.Owner.Key
                if (clsEccentricPoint.EccentricCorrectionParam().AngleType() == ANGLETYPE_HORIZONTAL)
                {
                    clsEccentricPoint.EccentricCorrectionParam().Marker = clsEccentricPoint.Owner().Key();
                }
                else
                {
                    clsEccentricPoint.EccentricCorrectionParam().UsePoint = clsEccentricPoint.Owner().Key()
                }
            }
#endif

            //'観測点ファイル名の更新。
            //2     string AppPath = System.IO.Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath);
            string App_Path = @"C:\Develop\NetSurv\Src\NS-App\NS-Survey";   //2

            string sDirPath;
            //2 sDirPath = AppPath + TEMPORARY_PATH + "." + OBSPOINT_PATH;
            sDirPath = App_Path + TEMPORARY_PATH + "." + OBSPOINT_PATH;

            ObservationPoint clsRepresentPoint;
            clsRepresentPoint = clsObservationPoint.HeadPoint();

            while (clsRepresentPoint != null)
            {
                clsRepresentPoint.UpdateFileName(sDirPath);
                clsRepresentPoint = clsRepresentPoint.NextPoint();
            }

            //'偏心補正の再計算。
            if (clsObservationPoint.Eccentric())
            {
                m_clsNetworkModel.CorrectEccentric(clsObservationPoint);
            }

            m_bModifyed = true;

        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '観測点の属性を設定する。
        '
        '引き数：
        'clsObservationPoint 対象とする観測点(代表観測点)。
        'sRecType 受信機名称。
        'sRecNumber 受信機シリアル番号。
        'sAntType アンテナ種別。
        'sAntNumber アンテナシリアル番号。
        'sAntMeasurement アンテナ測位方法。
        'nAntHeight アンテナ高(ｍ)。
        'nElevationMaskHand 手簿に出力する最低高度角(度)。負の値の場合OFFとする。
        'nNumberOfMinSVHand 手簿に出力する最少衛星数。0の場合はオプション設定から取得。1～12が最少衛星数。
        Public Sub SetAttribute(ByVal clsObservationPoint As ObservationPoint, ByVal sRecType As String, ByVal sRecNumber As String, ByVal sAntType As String, ByVal sAntNumber As String, ByVal sAntMeasurement As String, ByVal nAntHeight As Double, ByVal nElevationMaskHand As Long, ByVal nNumberOfMinSVHand As Long)

            '解析結果の有効無効。
            Dim bAnalysis As Boolean
            bAnalysis = bAnalysis Or(Abs(clsObservationPoint.AntHeight - nAntHeight) >= FLT_EPSILON)
            bAnalysis = bAnalysis Or(clsObservationPoint.AntType<> sAntType)
            bAnalysis = bAnalysis Or(clsObservationPoint.AntMeasurement<> sAntMeasurement)
    
            '設定。
            clsObservationPoint.RecType = sRecType
            clsObservationPoint.RecNumber = sRecNumber
            clsObservationPoint.AntType = sAntType
            clsObservationPoint.AntNumber = sAntNumber
            clsObservationPoint.AntMeasurement = sAntMeasurement
            clsObservationPoint.AntHeight = nAntHeight
            clsObservationPoint.Attributes.ElevationMaskHand = nElevationMaskHand
            clsObservationPoint.Attributes.NumberOfMinSVHand = nNumberOfMinSVHand


            If bAnalysis Then
                '偏心補正の再計算が必要な偏心点。
                Dim objEccentricPoints As New Collection
                '解析結果を削除する基線ベクトル。
                Dim objElements As New Collection
        
                'つながっている基線ベクトルは解析結果を破棄する。
                Dim clsBaseLineVectors() As BaseLineVector
                ReDim clsBaseLineVectors(-1 To -1)
                Call m_clsNetworkModel.GetConnectBaseLineVectors(clsObservationPoint, clsBaseLineVectors)
                Dim i As Long
                For i = 0 To UBound(clsBaseLineVectors)
                    If clsBaseLineVectors(i).Analysis <= ANALYSIS_STATUS_FAILED Then
                        '偏心補正の再計算が必要な偏心点を取得する。
                        If clsBaseLineVectors(i).Analysis <= ANALYSIS_STATUS_FIX Then
                            '基線ベクトルの終点となっている偏心点は再計算する。
                            If clsBaseLineVectors(i).AnalysisEndPoint.Eccentric Then Call SetAtCollectionObject(objEccentricPoints, clsBaseLineVectors(i).AnalysisEndPoint.HeadPoint, clsBaseLineVectors(i).AnalysisEndPoint.Number)
                            '基線ベクトルを方位標としている偏心点は再計算する。
                            Dim objEccentricPointsTemp As Collection
                            Set objEccentricPointsTemp = New Collection
                            Call m_clsNetworkModel.GetMarkedEccentricPoints2(clsBaseLineVectors(i), objEccentricPointsTemp)
                            Dim clsEccentricPoint As ObservationPoint
                            For Each clsEccentricPoint In objEccentricPointsTemp
                                Call SetAtCollectionObject(objEccentricPoints, clsEccentricPoint.HeadPoint, clsEccentricPoint.Number)
                            Next
                        End If
                
                        'つながっている基線ベクトルは解析結果を破棄する。
                        Call objElements.Add(clsBaseLineVectors(i), Hex$(GetPointer(clsBaseLineVectors(i))))
                    End If
                Next
        
                '解析結果の削除。
                Call m_clsNetworkModel.DeleteAnalysis(objElements)
        
                '偏心補正の再計算。
                For Each clsEccentricPoint In objEccentricPoints
                    Call m_clsNetworkModel.CorrectEccentric(clsEccentricPoint)
                    Call m_clsNetworkModel.EnableGenuinePoint(clsEccentricPoint)
                Next
            End If


            m_bModifyed = True

        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '観測点の属性を設定する。
        '
        '引き数：
        'clsObservationPoint 対象とする観測点(代表観測点)。
        'sRecType 受信機名称。
        'sRecNumber 受信機シリアル番号。
        'sAntType アンテナ種別。
        'sAntNumber アンテナシリアル番号。
        'sAntMeasurement アンテナ測位方法。
        'nAntHeight アンテナ高(ｍ)。
        'nElevationMaskHand 手簿に出力する最低高度角(度)。負の値の場合OFFとする。
        'nNumberOfMinSVHand 手簿に出力する最少衛星数。0の場合はオプション設定から取得。1～12が最少衛星数。
        */
        public void SetAttribute(ObservationPoint clsObservationPoint, string sRecType, string sRecNumber, string sAntType, string sAntNumber, string sAntMeasurement,
            double nAntHeight, long nElevationMaskHand, long nNumberOfMinSVHand)
        {
            return;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'セッションを変更する。
        '
        'sSession で指定されるセッション名を objElements で指定されるオブジェクトと、その関連するオブジェクトに設定する。
        '関連するオブジェクトとは、
        'objElements が基線ベクトルであった場合はその両端の観測点(代表観測点)。
        'objElements が観測点(代表観測点)であった場合はその接続基線ベクトル。
        '
        '引き数：
        'objElements 対象とするオブジェクト。要素はオブジェクト。キーは任意。
        'sSession セッション名。
        Public Sub SetSessionExtend(ByVal objElements As Collection, ByVal sSession As String)
            '拡張設定対象オブジェクト。
            Dim objExtends As New Collection
            Set objExtends = GetSessionExtend(objElements)
            '設定。
            Call SetSession(objExtends, sSession)
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]  //2
        /// <summary>
        /// セッションを変更する。
        /// '
        /// sSession で指定されるセッション名を objElements で指定されるオブジェクトと、その関連するオブジェクトに設定する。
        /// 関連するオブジェクトとは、
        /// objElements が基線ベクトルであった場合はその両端の観測点(代表観測点)。
        /// objElements が観測点(代表観測点)であった場合はその接続基線ベクトル。
        /// '
        /// 引き数：
        /// objElements 対象とするオブジェクト。要素はオブジェクト。キーは任意。
        /// sSession セッション名。
        /// 
        /// </summary>
        /// <param name="objElements"></param>
        /// <param name="sSession"></param>
        public void SetSessionExtend(Dictionary<string, object> objElements, string sSession)   //2
        {

            //'拡張設定対象オブジェクト。
            //  Dictionary<string, object> objExtends = new Dictionary<string, object>();
            //  objExtends = GetSessionExtend(objElements);

            //'設定。
            SetSession_Extends(objElements, sSession);  //2　新規

        }
#if false
        /*
         *************************** 修正要 sakai
         */
        public void SetSessionExtend(Collection objElements, string sSession)
        {
            //'拡張設定対象オブジェクト。
            Collection objExtends = new Collection();
            objExtends = GetSessionExtend(objElements);
            //'設定。
            SetSession(objExtends, sSession);
            return;
        }
#endif
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'セッションを変更する。
        '
        'sSession で指定されるセッション名を objElements で指定されるオブジェクトに設定する。
        '
        '引き数：
        'objElements 対象とするオブジェクト。要素はオブジェクト。キーは任意。
        'sSession セッション名。
        Public Sub SetSession(ByVal objElements As Collection, ByVal sSession As String)

            Dim sDirPath As String
            sDirPath = App.Path & TEMPORARY_PATH & "." & OBSPOINT_PATH

            Dim objElement As Object
            For Each objElement In objElements
                '観測点の場合、偏心補正の方位標を評価する。
                If(objElement.ObjectType And OBJ_TYPE_OBSERVATIONPOINT) = 0 Then
                    '接続基線ベクトルが方位標である観測点を取得する。
                    Dim objEccentricPoints As New Collection
                    Call m_clsNetworkModel.GetMarkedEccentricPoints2(objElement, objEccentricPoints)
                End If

                objElement.Session = sSession

                If (objElement.ObjectType And OBJ_TYPE_OBSERVATIONPOINT) = 0 Then
                    '方位標を反映させる。
                    Dim clsEccentricPoint As ObservationPoint
                    For Each clsEccentricPoint In objEccentricPoints
                        '2009/11 H.Nakamura
                        'clsEccentricPoint.EccentricCorrectionParam.Marker = clsEccentricPoint.Owner.Key
                        If clsEccentricPoint.EccentricCorrectionParam.AngleType = ANGLETYPE_HORIZONTAL Then
                            clsEccentricPoint.EccentricCorrectionParam.Marker = clsEccentricPoint.Owner.Key
                        Else
                            clsEccentricPoint.EccentricCorrectionParam.UsePoint = clsEccentricPoint.Owner.Key
                        End If
                    Next
                Else
                    '観測点の場合。仮のセッションをOFF。
                    objElement.ProvisionalSession = False
                    '観測点ファイル名の更新。
                    Call objElement.UpdateFileName(sDirPath)
                End If
            Next


            m_bModifyed = True


        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]  //2
        /// <summary>
        /// セッションを変更する。
        /// '
        /// sSession で指定されるセッション名を objElements で指定されるオブジェクトに設定する。
        /// 
        /// 引き数：
        /// objElements 対象とするオブジェクト。要素はオブジェクト。キーは任意。
        /// sSession セッション名。
        /// 
        /// </summary>
        /// <param name="objElements"></param>
        /// <param name="sSession"></param>
        public void SetSession(Dictionary<string, object> objElements, string sSession)
        {
            string sDirPath;
#if false
            string AppPath = System.IO.Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath);
            sDirPath = AppPath + TEMPORARY_PATH + "." + OBSPOINT_PATH;      //"C:\Develop\NetSurv\Src\NS-App\NS-Survey\Temp\.\ObsPoint\"
#else
            string App_Path = @"C:\Develop\NetSurv\Src\NS-App\NS-Survey";
            sDirPath = App_Path + TEMPORARY_PATH + "." + OBSPOINT_PATH;      //"C:\Develop\NetSurv\Src\NS-App\NS-Survey\Temp\.\ObsPoint\"
#endif

            long SelectLine = m_clsMdlMain.PaneSelcttedNo;


            ChainList clsChainList;

            //===========================================================================
            if (m_clsMdlMain.PaneSelectedTab == (long)LIST_NUM_PANE.LIST_NUM_OBSPNT)
            //===========================================================================
            {
                //***************************
                //TAB：観測点　選択の場合
                //***************************

                clsChainList = m_clsMdlMain.GetDocument().NetworkModel().RepresentPointHead();
                long nRows = 0;
                ObservationPoint clsObservationPoint;
                while (clsChainList != null)
                {
                    clsObservationPoint = (ObservationPoint)clsChainList.Element;

                    if (nRows == SelectLine)
                    {
                        break;
                    }
                    nRows++;
                    clsChainList = clsChainList.NextList();

                }
                clsObservationPoint = (ObservationPoint)clsChainList.Element;


                foreach (object objElement in objElements)
                {
                    //'観測点の場合、偏心補正の方位標を評価する。
                    if ((clsObservationPoint.ObjectType & OBJ_TYPE_OBSERVATIONPOINT) == 0)
                    {
                        //'接続基線ベクトルが方位標である観測点を取得する。
                        object objEccentricPoints = new object();
                        m_clsNetworkModel.GetMarkedEccentricPoints2((BaseLineVector)objElement, objEccentricPoints);
                    }

                    clsObservationPoint.Session(sSession);       //Ex)objElement.Session( S001 <- 134F)

                    if ((clsObservationPoint.ObjectType & OBJ_TYPE_OBSERVATIONPOINT) == 0)       //objElement.ObjectType = -2147483631
                    {
                        //  // 方位標を反映させる。
                        //  ObservationPoint clsEccentricPoint; 
                        //  foreach ( clsEccentricPoint in objEccentricPoints)
                        //  {
                        //      //'2009/11 H.Nakamura
                        //      //    'clsEccentricPoint.EccentricCorrectionParam.Marker = clsEccentricPoint.Owner.Key
                        //      if (clsEccentricPoint.EccentricCorrectionParam.AngleType == ANGLETYPE_HORIZONTAL)
                        //      {
                        //          clsEccentricPoint.EccentricCorrectionParam.Marker = clsEccentricPoint.Owner.Key;
                        //      }
                        //      else
                        //      {
                        //        clsEccentricPoint.EccentricCorrectionParam.UsePoint = clsEccentricPoint.Owner.Key;
                        //    }
                        //  }   //foreach ( clsEccentricPoint in objEccentricPoints)
                    }
                    else
                    {
                        //'観測点の場合。仮のセッションをOFF。
                        clsObservationPoint.ProvisionalSession(false);
                        //'観測点ファイル名の更新。
                        clsObservationPoint.UpdateFileName(sDirPath);
                    }
                }

            }
            //===========================================================================
            else
            //===========================================================================
            {

                //***************************
                //TAB：ベクトル　選択の場合
                //***************************

                //******************************************************************************
                clsChainList = m_clsMdlMain.GetDocument().NetworkModel().RepresentPointHead();
                long nRows = 0;
                ObservationPoint clsObservationPoint;
                while (clsChainList != null)
                {
                    clsObservationPoint = (ObservationPoint)clsChainList.Element;

                    if (nRows == SelectLine)
                    {
                        break;
                    }
                    nRows++;
                    clsChainList = clsChainList.NextList();

                }
                clsObservationPoint = (ObservationPoint)clsChainList.Element;

                //******************************************************************************

                clsChainList = m_clsMdlMain.GetDocument().NetworkModel().BaseLineVectorHead();
                nRows = 0;
                BaseLineVector clsBaseLineVector;
                while (clsChainList != null)
                {
                    clsBaseLineVector = (BaseLineVector)clsChainList.Element;

                    if (nRows == SelectLine)
                    {
                        break;
                    }
                    nRows++;
                    clsChainList = clsChainList.NextList();

                }
                clsBaseLineVector = (BaseLineVector)clsChainList.Element;


                foreach (object objElement in objElements)
                {
                    //  //'観測点の場合、偏心補正の方位標を評価する。
                    //  if ((clsBaseLineVector.ObjectType & OBJ_TYPE_OBSERVATIONPOINT) == 0)
                    //  {
                    //      //'接続基線ベクトルが方位標である観測点を取得する。
                    //      object objEccentricPoints = new object();
                    //      m_clsNetworkModel.GetMarkedEccentricPoints2((BaseLineVector)objElement, objEccentricPoints);
                    //  }

                    clsBaseLineVector.Session = sSession;       //Ex)objElement.Session( S001 <- 134F)

                    if ((clsBaseLineVector.ObjectType & OBJ_TYPE_OBSERVATIONPOINT) == 0)       //objElement.ObjectType = -2147483631
                    {
                        //  // 方位標を反映させる。
                        //  ObservationPoint clsEccentricPoint; 
                        //  foreach ( clsEccentricPoint in objEccentricPoints)
                        //  {
                        //      //'2009/11 H.Nakamura
                        //      //    'clsEccentricPoint.EccentricCorrectionParam.Marker = clsEccentricPoint.Owner.Key
                        //      if (clsEccentricPoint.EccentricCorrectionParam.AngleType == ANGLETYPE_HORIZONTAL)
                        //      {
                        //          clsEccentricPoint.EccentricCorrectionParam.Marker = clsEccentricPoint.Owner.Key;
                        //      }
                        //      else
                        //      {
                        //        clsEccentricPoint.EccentricCorrectionParam.UsePoint = clsEccentricPoint.Owner.Key;
                        //    }
                        //  }   //foreach ( clsEccentricPoint in objEccentricPoints)
                    }
                    else
                    {
                        //'観測点の場合。仮のセッションをOFF。
                        clsObservationPoint.ProvisionalSession(false);
                        //'観測点ファイル名の更新。
                        clsObservationPoint.UpdateFileName(sDirPath);
                    }
                }

                //===========================================================================
            }
            //===========================================================================

        }
        //------------------------------------------------------------------------------------------
        //[C#] 2
        /// <summary>
        /// セッションを変更する。
        /// '
        /// sSession で指定されるセッション名を objElements で指定されるオブジェクトに設定する。
        /// '
        /// 引き数：
        /// objElements 対象とするオブジェクト。要素はオブジェクト。キーは任意。
        /// sSession セッション名。
        /// 
        /// </summary>
        /// <param name="objElements"></param>
        /// <param name="sSession"></param>
        public void SetSession_Extends(Dictionary<string, object> objElements, string sSession)
        {
            string sDirPath;
#if false
            string AppPath = System.IO.Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath);
            sDirPath = AppPath + TEMPORARY_PATH + "." + OBSPOINT_PATH;      //"C:\Develop\NetSurv\Src\NS-App\NS-Survey\Temp\.\ObsPoint\"
#else
            string App_Path = @"C:\Develop\NetSurv\Src\NS-App\NS-Survey";
            sDirPath = App_Path + TEMPORARY_PATH + "." + OBSPOINT_PATH;      //"C:\Develop\NetSurv\Src\NS-App\NS-Survey\Temp\.\ObsPoint\"
#endif

            long SelectLine = m_clsMdlMain.PaneSelcttedNo;


            //-------------------------------------------------------------------
            ChainList clsChainList;
            //===========================================================================
            if (m_clsMdlMain.PaneSelectedTab == (long)LIST_NUM_PANE.LIST_NUM_OBSPNT)
            //===========================================================================
            {
                //**************************
                //TAB：観測点   選択の場合
                //**************************

                clsChainList = m_clsMdlMain.GetDocument().NetworkModel().RepresentPointHead();
                long nRows = 0;
                ObservationPoint clsObservationPoint;
                //-------------------------------------------------------------------
                while (clsChainList != null)
                {
                    clsObservationPoint = (ObservationPoint)clsChainList.Element;

                    if (nRows == SelectLine)
                    {
                        break;
                    }
                    nRows++;
                    clsChainList = clsChainList.NextList();

                }
                //-------------------------------------------------------------------
                clsObservationPoint = (ObservationPoint)clsChainList.Element;

                foreach (object objElement in objElements)
                {
                    //'観測点の場合、偏心補正の方位標を評価する。
                    if ((clsObservationPoint.ObjectType & OBJ_TYPE_OBSERVATIONPOINT) == 0)
                    {
                        //'接続基線ベクトルが方位標である観測点を取得する。
                        object objEccentricPoints = new object();
                        m_clsNetworkModel.GetMarkedEccentricPoints2((BaseLineVector)objElement, objEccentricPoints);
                    }

                    List<BaseLineVector> clsBaseLineVectors = new List<BaseLineVector>();

                    Session_GetConnectBaseLineVectors(clsObservationPoint, ref clsBaseLineVectors);     //2)
                    int v = 0;
                    foreach (object obj in clsBaseLineVectors)
                    {
                        clsBaseLineVectors[v].Session = sSession;
                        v++;
                    }


                    if ((clsObservationPoint.ObjectType & OBJ_TYPE_OBSERVATIONPOINT) == 0)       //objElement.ObjectType = -2147483631
                    {
                        //  // 方位標を反映させる。
                        //  ObservationPoint clsEccentricPoint; 
                        //  foreach ( clsEccentricPoint in objEccentricPoints)
                        //  {
                        //      //'2009/11 H.Nakamura
                        //      //    'clsEccentricPoint.EccentricCorrectionParam.Marker = clsEccentricPoint.Owner.Key
                        //      if (clsEccentricPoint.EccentricCorrectionParam.AngleType == ANGLETYPE_HORIZONTAL)
                        //      {
                        //          clsEccentricPoint.EccentricCorrectionParam.Marker = clsEccentricPoint.Owner.Key;
                        //      }
                        //      else
                        //      {
                        //        clsEccentricPoint.EccentricCorrectionParam.UsePoint = clsEccentricPoint.Owner.Key;
                        //    }
                        //  }   //foreach ( clsEccentricPoint in objEccentricPoints)
                    }
                    else
                    {
                        //'観測点の場合。仮のセッションをOFF。
                        clsObservationPoint.ProvisionalSession(false);
                        //'観測点ファイル名の更新。
                        clsObservationPoint.UpdateFileName(sDirPath);
                    }
                }
            }
            //===========================================================================
            else  //if (m_clsMdlMain.PaneSelectedTab != (long)LIST_NUM_PANE.LIST_NUM_OBSPNT)
            //===========================================================================
            {

                //**************************
                //TAB：ベクトル選択の場合
                //**************************

                long nRows = 0;

                clsChainList = m_clsMdlMain.GetDocument().NetworkModel().BaseLineVectorHead();
                BaseLineVector clsBaseLineVector;

                //-------------------------------------------------------------------
                while (clsChainList != null)
                {
                    clsBaseLineVector = (BaseLineVector)clsChainList.Element;

                    if (nRows == SelectLine)
                    {
                        break;
                    }
                    nRows++;
                    clsChainList = clsChainList.NextList();

                }
                //-------------------------------------------------------------------
                clsBaseLineVector = (BaseLineVector)clsChainList.Element;

                //-------------------------------------------------------------------
                //TABベクトル選択された、開始点と終点の情報を取得
                string Str = clsBaseLineVector.StrPoint().TopParentPoint().Number();
                string End = clsBaseLineVector.EndPoint().TopParentPoint().Number();

                //-------------------------------------------------------------------
                //観測点側の更新
                clsChainList = m_clsMdlMain.GetDocument().NetworkModel().RepresentPointHead();
                long Rows = 0;
                ObservationPoint clsObservationPoint;
                //-------------------------------------------------------------------
                while (clsChainList != null)
                {
                    clsObservationPoint = (ObservationPoint)clsChainList.Element;

                    string sCheck = clsObservationPoint.DispNumber();   //観測点No.

                    if (Str == sCheck || End == sCheck)                 //ベクトル選択の始点と終点＝観測点No.
                    {
                        clsObservationPoint.Session(sSession);
                    }

                    nRows++;
                    clsChainList = clsChainList.NextList();

                }
                //-------------------------------------------------------------------





                //===========================================================================
            }
            //===========================================================================









        }
        //---------------------------------------------------------------------------------------------------------------------------------------------
        public void Session_GetConnectBaseLineVectors(ObservationPoint clsObservationPoint, ref List<BaseLineVector> clsBaseLineVectors)   //2)
        {
            ChainList clsChainList;
            clsChainList = m_clsMdlMain.GetDocument().NetworkModel().BaseLineVectorHead();
            BaseLineVector dataBaseLineVector;

            string sCheck = clsObservationPoint.DispNumber();

            while (clsChainList != null)
            {
                dataBaseLineVector = (BaseLineVector)clsChainList.Element;

                if (dataBaseLineVector.StrPoint().TopParentPoint().Number() == sCheck || dataBaseLineVector.EndPoint().TopParentPoint().Number() == sCheck)
                {
                    clsBaseLineVectors.Add(dataBaseLineVector);
                }

                clsChainList = clsChainList.NextList();
            }

        }

        //===========================================================================================
        //===========================================================================================

        //==========================================================================================
        /*[VB]
        '指定された基線ベクトルの始点と終点を置き換える。
        '
        '引き数：
        'clsBaseLineVector 対象とする基線ベクトル。
        Public Sub ReplaceBaseLineVector(ByVal clsBaseLineVector As BaseLineVector)

            '偏心補正の再計算が必要な偏心点。
            Dim objEccentricPoints As New Collection
            If clsBaseLineVector.Analysis <= ANALYSIS_STATUS_FIX Then
                '基線ベクトルの終点となっている偏心点は再計算する。
                If clsBaseLineVector.AnalysisEndPoint.Eccentric Then Call SetAtCollectionObject(objEccentricPoints, clsBaseLineVector.AnalysisEndPoint.HeadPoint, clsBaseLineVector.AnalysisEndPoint.Number)
                '基線ベクトルを方位標としている偏心点は再計算する。
                Dim objEccentricPointsTemp As New Collection
                Call m_clsNetworkModel.GetMarkedEccentricPoints2(clsBaseLineVector, objEccentricPointsTemp)
                Dim clsEccentricPoint As ObservationPoint
                For Each clsEccentricPoint In objEccentricPointsTemp
                    Call SetAtCollectionObject(objEccentricPoints, clsEccentricPoint.HeadPoint, clsEccentricPoint.Number)
                Next
            End If

            '反転。
            Call m_clsNetworkModel.ReplaceBaseLineVector(clsBaseLineVector)

            '偏心補正の再計算。
            For Each clsEccentricPoint In objEccentricPoints
                Call m_clsNetworkModel.CorrectEccentric(clsEccentricPoint)
                Call m_clsNetworkModel.EnableGenuinePoint(clsEccentricPoint)
            Next


            m_bModifyed = True


        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '指定された基線ベクトルの始点と終点を置き換える。
        '
        '引き数：
        'clsBaseLineVector 対象とする基線ベクトル。
        */
        public void ReplaceBaseLineVector(BaseLineVector clsBaseLineVector)
        {
            return;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '基線解析を行う。
        '
        '引き数：
        'clsBaseLineAnalysisParam 基線解析パラメータ。
        'objAnalysisBaseLineVectors 対象とする基線ベクトル。要素は BaseLineVector オブジェクト。キーは任意。
        'bOrderModify 解析順更新許可フラグ。True=解析順は更新する。False=解析順は更新しない。
        'nFailedCount 基線解析に失敗した基線ベクトルの数が設定される。
        'clsProgressInterface ProgressInterface オブジェクト。
        Public Sub BaseLineAnalysis(ByVal clsBaseLineAnalysisParam As BaseLineAnalysisParam, ByVal objAnalysisBaseLineVectors As Collection, ByVal bOrderModify As Boolean, ByRef nFailedCount As Long, ByVal clsProgressInterface As ProgressInterface)

            m_bModifyed = True

            If Not bOrderModify Then
                '解析順は更新しない。
                Dim sOrders() As String
                Dim bRevers() As Boolean
                Dim bChecks() As Boolean
                sOrders = m_clsBaseLineAnalysisParam.Orders
                bRevers = m_clsBaseLineAnalysisParam.Revers
                bChecks = m_clsBaseLineAnalysisParam.Checks
            End If

            Let m_clsBaseLineAnalysisParam = clsBaseLineAnalysisParam

            If Not bOrderModify Then Call m_clsBaseLineAnalysisParam.SetOrders(sOrders, bRevers, bChecks) '解析順は更新しない。
    
            '解析。
            On Error GoTo CancelHandler
            Dim clsBaseLineAnalyser As New BaseLineAnalyser
            Call clsBaseLineAnalyser.Analyse(m_clsNetworkModel.BaseLineVectorHead, clsBaseLineAnalysisParam, bOrderModify, nFailedCount, clsProgressInterface)
        CancelHandler:
            If Err.Number<> 0 And Err.Number<> cdlCancel Then Call Err.Raise(Err.Number, Err.Source, Err.Description, Err.HelpFile, Err.HelpContext)
    
            'これ以降キャンセルは不可。
            Call clsProgressInterface.SetCancel(False)
            clsProgressInterface.CancelEnable = False
    
            '解析順番の設定。
            Call clsBaseLineAnalyser.SetAnalysisOrder(m_clsNetworkModel.BaseLineVectorHead, clsProgressInterface)

            clsProgressInterface.Prompt = "偏心補正を再計算中･･･"
    
            '帳票のチェックをONにする。
            Dim objSelectedObjects As Collection
            Set objSelectedObjects = m_clsAccountParamCoordinate.SelectedObjects
    
            '偏心補正の再計算が必要な偏心点。
            Dim objEccentricPoints As New Collection
            Dim clsBaseLineVector As BaseLineVector
            For Each clsBaseLineVector In objAnalysisBaseLineVectors
                '基線ベクトルの始点終点となっている偏心点は再計算する。厳密には余分なものが含まれる。
                If clsBaseLineVector.StrPoint.Eccentric Then Call SetAtCollectionObject(objEccentricPoints, clsBaseLineVector.StrPoint.HeadPoint, clsBaseLineVector.StrPoint.Number)
                If clsBaseLineVector.EndPoint.Eccentric Then Call SetAtCollectionObject(objEccentricPoints, clsBaseLineVector.EndPoint.HeadPoint, clsBaseLineVector.EndPoint.Number)
                '帳票のチェックをONにする。
                Dim vKey As Variant
                vKey = clsBaseLineVector.Key
                Call SetAtCollectionVariant(objSelectedObjects, vKey, vKey)
                'プログレス。
                Call clsProgressInterface.CheckCancel
            Next
    
            '偏心補正の再計算。
            Dim clsEccentricPoint As ObservationPoint
            For Each clsEccentricPoint In objEccentricPoints
                Call m_clsNetworkModel.CorrectEccentric(clsEccentricPoint)
                Call m_clsNetworkModel.EnableGenuinePoint(clsEccentricPoint)
                'プログレス。
                Call clsProgressInterface.CheckCancel
            Next


        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '基線解析を行う。
        '
        '引き数：
        'clsBaseLineAnalysisParam 基線解析パラメータ。
        'objAnalysisBaseLineVectors 対象とする基線ベクトル。要素は BaseLineVector オブジェクト。キーは任意。
        'bOrderModify 解析順更新許可フラグ。True=解析順は更新する。False=解析順は更新しない。
        'nFailedCount 基線解析に失敗した基線ベクトルの数が設定される。
        'clsProgressInterface ProgressInterface オブジェクト。
        */
#if false
        /*
         *************************** 修正要 sakai
         */
        public void BaseLineAnalysis(BaseLineAnalysisParam clsBaseLineAnalysisParam, Collection objAnalysisBaseLineVectors,
            bool bOrderModify, long nFailedCount, ProgressInterface clsProgressInterface)
        {
            return;
        }
#endif
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '解析結果を削除する。
        '
        '引き数：
        'objElements 対象とする基線ベクトル。要素は BaseLineVector オブジェクト。キーは任意。
        Public Sub DeleteAnalysis(ByVal objElements As Collection)

            '偏心補正の再計算が必要な偏心点。
            Dim objEccentricPoints As New Collection
            Dim clsBaseLineVector As BaseLineVector
            For Each clsBaseLineVector In objElements
                If clsBaseLineVector.Analysis <= ANALYSIS_STATUS_FIX Then
                    '基線ベクトルの終点となっている偏心点は再計算する。
                    If clsBaseLineVector.AnalysisEndPoint.Eccentric Then Call SetAtCollectionObject(objEccentricPoints, clsBaseLineVector.AnalysisEndPoint.HeadPoint, clsBaseLineVector.AnalysisEndPoint.Number)
                    '基線ベクトルを方位標としている偏心点は再計算する。
                    Dim objEccentricPointsTemp As New Collection
                    Call m_clsNetworkModel.GetMarkedEccentricPoints2(clsBaseLineVector, objEccentricPointsTemp)
                    Dim clsEccentricPoint As ObservationPoint
                    For Each clsEccentricPoint In objEccentricPointsTemp
                        Call SetAtCollectionObject(objEccentricPoints, clsEccentricPoint.HeadPoint, clsEccentricPoint.Number)
                    Next
                End If
            Next
    
            '解析結果の削除。
            Call m_clsNetworkModel.DeleteAnalysis(objElements)
    
            '偏心補正の再計算。
            For Each clsEccentricPoint In objEccentricPoints
                Call m_clsNetworkModel.CorrectEccentric(clsEccentricPoint)
                Call m_clsNetworkModel.EnableGenuinePoint(clsEccentricPoint)
            Next


            m_bModifyed = True


        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '解析結果を削除する。
        '
        '引き数：
        'objElements 対象とする基線ベクトル。要素は BaseLineVector オブジェクト。キーは任意。
        */
#if false
        /*
         *************************** 修正要 sakai
         */
        public void DeleteAnalysis(Collection objElements)
        {
            return;
        }
#endif
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'データ表示。
        '
        '引き数：
        'objElement 対象とするオブジェクト。
        '
        '戻り値：作成した ActiveReport オブジェクトを返す。
        Public Function MakeAccountDataView(ByVal objElement As Object) As ActiveReport

            '作成。
            If(objElement.ObjectType And OBJ_TYPE_OBSERVATIONPOINT) <> 0 Then
                Set MakeAccountDataView = m_clsAccountMaker.MakeDataViewObservation(objElement)
            Else
                Set MakeAccountDataView = m_clsAccountMaker.MakeDataViewBaseLineVector(objElement)
            End If


        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        'データ表示。
        '
        '引き数：
        'objElement 対象とするオブジェクト。
        '
        '戻り値：作成した ActiveReport オブジェクトを返す。
        */
#if false
        /*
         *************************** 修正要 sakai
         */
        public ActiveReport MakeAccountDataView(object objElement)
        {
            //'作成。
            if ((objElement.ObjectType && OBJ_TYPE_OBSERVATIONPOINT) != 0)
            {
                return m_clsAccountMaker.MakeDataViewObservation(objElement);
            }
            else
            {
                return m_clsAccountMaker.MakeDataViewBaseLineVector(objElement);
            }
        }
#endif
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '観測手簿を作成する。
        '
        '引き数：
        'clsAccountParam 観測手簿パラメータ。
        'bModify 更新許可フラグ。True=パラメータを更新することを許可する。False=パラメータの更新は不可。
        '
        '戻り値：作成した ActiveReport オブジェクトを返す。
        Public Function MakeAccountHand(ByVal clsAccountParam As AccountParam, ByVal bModify As Boolean) As ActiveReport

            If bModify Then
                '一致しない場合、更新フラグをONにする。
                If Not m_clsAccountParamHand.Compare(clsAccountParam) Then m_bModifyed = True
                Let m_clsAccountParamHand = clsAccountParam
            End If
    
            '作成。
            Set MakeAccountHand = m_clsAccountMaker.MakeHand(PageNumberVisible, NumberOfMinSV, clsAccountParam)


        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '観測手簿を作成する。
        '
        '引き数：
        'clsAccountParam 観測手簿パラメータ。
        'bModify 更新許可フラグ。True=パラメータを更新することを許可する。False=パラメータの更新は不可。
        '
        '戻り値：作成した ActiveReport オブジェクトを返す。
        */
#if false
        /*
         *************************** 修正要 sakai
         */
        public ActiveReport MakeAccountHand(AccountParam clsAccountParam, bool bModify)
        {
            if (bModify)
            {
                //'一致しない場合、更新フラグをONにする。
                if (!m_clsAccountParamHand.Compare(clsAccountParam))
                {
                    m_bModifyed = true;
                    m_clsAccountParamHand = clsAccountParam;
                }
            }
            //'作成。
            return m_clsAccountMaker.MakeHand(PageNumberVisible, NumberOfMinSV, clsAccountParam);
        }
#endif
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '観測記簿を作成する。
        '
        '引き数：
        'clsAccountParam 観測記簿パラメータ。
        'bModify 更新許可フラグ。True=パラメータを更新することを許可する。False=パラメータの更新は不可。
        'hWnd オーナーウィンドウ。メッセージボックス表示後、再描画するウィンドウのハンドル。
        '
        '戻り値：作成した ActiveReport オブジェクトを返す。
        '2020/10/20 FDC 既知点の統一'''''''''''''''''''''''''''''''''''''''''''
        'Public Function MakeAccountWrite(ByVal clsAccountParam As AccountParam, ByVal bModify As Boolean, ByVal hWnd As Long) As ActiveReport
        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Public Function MakeAccountWrite(ByVal clsAccountParam As AccountParam, ByVal bModify As Boolean, ByVal hWnd As Long, ByVal clsAccountOverlapParam As AccountOverlapParam) As ActiveReport
        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            If bModify Then
                '一致しない場合、更新フラグをONにする。
                If Not m_clsAccountParamWrite.Compare(clsAccountParam) Then m_bModifyed = True
                Let m_clsAccountParamWrite = clsAccountParam
                '2020/10/20 FDC 既知点の統一'''''''''''''''''''''''''''''''''''''''''''
                Let m_clsAccountOverlapParam = clsAccountOverlapParam
                '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            End If
    
            '作成。
            '2020/10/20 FDC 既知点の統一'''''''''''''''''''''''''''''''''''''''''''
            'Set MakeAccountWrite = m_clsAccountMaker.MakeWrite(PageNumberVisible, IIf(m_bGeoidoEnable, m_sGeoidoPath, ""), clsAccountParam, hWnd)
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            Set MakeAccountWrite = m_clsAccountMaker.MakeWrite(PageNumberVisible, IIf(m_bGeoidoEnable, m_sGeoidoPath, ""), clsAccountParam, hWnd, clsAccountOverlapParam)
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''


        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '観測記簿を作成する。
        '
        '引き数：
        'clsAccountParam 観測記簿パラメータ。
        'bModify 更新許可フラグ。True=パラメータを更新することを許可する。False=パラメータの更新は不可。
        'hWnd オーナーウィンドウ。メッセージボックス表示後、再描画するウィンドウのハンドル。
        '
        '戻り値：作成した ActiveReport オブジェクトを返す。
        */
#if false
        /*
         *************************** 修正要 sakai
         */
        public ActiveReport MakeAccountWrite(AccountParam clsAccountParam, bool bModify, long hWnd, AccountOverlapParam clsAccountOverlapParam)
        {
            return null;
        }
#endif
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '座標計算簿を作成する。
        '
        '引き数：
        'clsAccountParam 座標計算簿パラメータ。
        'bModify 更新許可フラグ。True=パラメータを更新することを許可する。False=パラメータの更新は不可。
        '
        '戻り値：作成した ActiveReport オブジェクトを返す。
        Public Function MakeAccountCoordinate(ByVal clsAccountParam As AccountParam, ByVal bModify As Boolean) As ActiveReport

            If bModify Then
                '一致しない場合、更新フラグをONにする。
                If Not m_clsAccountParamCoordinate.Compare(clsAccountParam) Then m_bModifyed = True
                Let m_clsAccountParamCoordinate = clsAccountParam
            End If
    
            '作成。
            Set MakeAccountCoordinate = m_clsAccountMaker.MakeCoordinate(PageNumberVisible, IIf(m_bGeoidoEnable, m_sGeoidoPath, ""), clsAccountParam)


        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '座標計算簿を作成する。
        '
        '引き数：
        'clsAccountParam 座標計算簿パラメータ。
        'bModify 更新許可フラグ。True=パラメータを更新することを許可する。False=パラメータの更新は不可。
        '
        '戻り値：作成した ActiveReport オブジェクトを返す。
        */
#if false
        /*
         *************************** 修正要 sakai
         */
        public ActiveReport MakeAccountCoordinate(AccountParam clsAccountParam, bool bModify)
        {
            return null;
        }
#endif
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '点検計算簿(重複基線)を作成する。
        '
        '引き数：
        'clsAccountOverlapParam 点検計算簿(重複基線)パラメータ。
        'bCheck 点検測量フラグ。True=点検測量。False=標準(点検計算簿)。
        'bMax 最大値表示フラグ。
        '
        '戻り値：作成した ActiveReport オブジェクト。
        Public Function MakeAccountOverlap(ByVal clsAccountOverlapParam As AccountOverlapParam, ByVal bCheck As Boolean, ByVal bMax As Boolean) As ActiveReport

            '一致しない場合、更新フラグをONにする。
            If Not m_clsAccountOverlapParam.Compare(clsAccountOverlapParam) Then m_bModifyed = True
            Let m_clsAccountOverlapParam = clsAccountOverlapParam
    
            '作成。
            Set MakeAccountOverlap = m_clsAccountMaker.MakeOverlap(PageNumberVisible, m_clsAccountOverlapParam, IIf(bCheck, 1, 0), bMax)


        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '点検計算簿(重複基線)を作成する。
        '
        '引き数：
        'clsAccountOverlapParam 点検計算簿(重複基線)パラメータ。
        'bCheck 点検測量フラグ。True=点検測量。False=標準(点検計算簿)。
        'bMax 最大値表示フラグ。
        '
        '戻り値：作成した ActiveReport オブジェクト。
        */
#if false
        /*
         *************************** 修正要 sakai
         */
        public ActiveReport MakeAccountOverlap(AccountOverlapParam clsAccountOverlapParam, bool bCheck, bool bMax)
        {
            return null;
        }
#endif
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '点検計算簿(環閉合差)を作成する。
        '
        'objSelectedAngleDiffs で指定された閉合差の帳票を作成する。
        '
        '引き数：
        'objSelectedAngleDiffs 出力する閉合差。要素は AngleDiff オブジェクト。キーは AngleDiff オブジェクトの Number。
        '
        '戻り値：作成した ActiveReport オブジェクトを返す。
        Public Function MakeAccountRing(ByVal objSelectedAngleDiffs As Collection) As ActiveReport

            ReDim clsAngleDiffs(-1 To -1)
            Dim i As Long
            For i = 0 To m_clsAngleDiffParamRing.Count - 1
                Dim clsAngleDiff As AngleDiff
                If LookupCollectionObject(objSelectedAngleDiffs, clsAngleDiff, m_clsAngleDiffParamRing.AngleDiffs(i).Number) Then
                    '一致しない場合、更新フラグをONにする。
                    If Not m_clsAngleDiffParamRing.AngleDiffs(i).Account Then m_bModifyed = True
                    m_clsAngleDiffParamRing.AngleDiffs(i).Account = True
                Else
                    '一致しない場合、更新フラグをONにする。
                    If m_clsAngleDiffParamRing.AngleDiffs(i).Account Then m_bModifyed = True
                    m_clsAngleDiffParamRing.AngleDiffs(i).Account = False
                End If
            Next
    
            '作成。
            Set MakeAccountRing = m_clsAccountMaker.MakeRing(PageNumberVisible, IIf(m_bGeoidoEnable, m_sGeoidoPath, ""), objSelectedAngleDiffs)


        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '点検計算簿(環閉合差)を作成する。
        '
        'objSelectedAngleDiffs で指定された閉合差の帳票を作成する。
        '
        '引き数：
        'objSelectedAngleDiffs 出力する閉合差。要素は AngleDiff オブジェクト。キーは AngleDiff オブジェクトの Number。
        '
        '戻り値：作成した ActiveReport オブジェクトを返す。
        */
#if false
        /*
         *************************** 修正要 sakai
         */
        public ActiveReport MakeAccountRing(Collection objSelectedAngleDiffs)
        {
            return null;
        }
#endif
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '点検計算簿(電子基準点間の閉合差)を作成する。
        '
        'objSelectedAngleDiffs で指定された閉合差の帳票を作成する。
        '
        '引き数：
        'objSelectedAngleDiffs 出力する閉合差。要素は AngleDiff オブジェクト。キーは AngleDiff オブジェクトの Number。
        '
        '戻り値：作成した ActiveReport オブジェクトを返す。
        Public Function MakeAccountBetween(ByVal objSelectedAngleDiffs As Collection) As ActiveReport

            ReDim clsAngleDiffs(-1 To -1)
            Dim i As Long
            For i = 0 To m_clsAngleDiffParamBetween.Count - 1
                Dim clsAngleDiff As AngleDiff
                If LookupCollectionObject(objSelectedAngleDiffs, clsAngleDiff, m_clsAngleDiffParamBetween.AngleDiffs(i).Number) Then
                    '一致しない場合、更新フラグをONにする。
                    If Not m_clsAngleDiffParamBetween.AngleDiffs(i).Account Then m_bModifyed = True
                    m_clsAngleDiffParamBetween.AngleDiffs(i).Account = True
                Else
                    '一致しない場合、更新フラグをONにする。
                    If m_clsAngleDiffParamBetween.AngleDiffs(i).Account Then m_bModifyed = True
                    m_clsAngleDiffParamBetween.AngleDiffs(i).Account = False
                End If
            Next
    
            '作成。
            Set MakeAccountBetween = m_clsAccountMaker.MakeBetween(PageNumberVisible, IIf(m_bGeoidoEnable, m_sGeoidoPath, ""), m_bSemiDynaEnable, objSelectedAngleDiffs)


        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '点検計算簿(電子基準点間の閉合差)を作成する。
        '
        'objSelectedAngleDiffs で指定された閉合差の帳票を作成する。
        '
        '引き数：
        'objSelectedAngleDiffs 出力する閉合差。要素は AngleDiff オブジェクト。キーは AngleDiff オブジェクトの Number。
        '
        '戻り値：作成した ActiveReport オブジェクトを返す。
        */
#if false
        /*
         *************************** 修正要 sakai
         */
        public ActiveReport MakeAccountBetween(Collection objSelectedAngleDiffs)
        {
            return null;
        }
#endif
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '2023/05/19 Hitz H.Nakamura **************************************************
        '楕円体高の閉合差を追加。
        '点検計算簿(楕円体高の閉合差)を作成する。
        '
        'objSelectedAngleDiffs で指定された閉合差の帳票を作成する。
        '
        '引き数：
        'objSelectedAngleDiffs 出力する閉合差。要素は AngleDiff オブジェクト。キーは AngleDiff オブジェクトの Number。
        '
        '戻り値：作成した ActiveReport オブジェクトを返す。
        Public Function MakeAccountAngleDiffHeight(ByVal objSelectedAngleDiffs As Collection) As ActiveReport

            ReDim clsAngleDiffs(-1 To -1)
            Dim i As Long
            For i = 0 To m_clsAngleDiffParamHeight.Count - 1
                Dim clsAngleDiff As AngleDiff
                If LookupCollectionObject(objSelectedAngleDiffs, clsAngleDiff, m_clsAngleDiffParamHeight.AngleDiffs(i).Number) Then
                    '一致しない場合、更新フラグをONにする。
                    If Not m_clsAngleDiffParamHeight.AngleDiffs(i).Account Then m_bModifyed = True
                    m_clsAngleDiffParamHeight.AngleDiffs(i).Account = True
                Else
                    '一致しない場合、更新フラグをONにする。
                    If m_clsAngleDiffParamHeight.AngleDiffs(i).Account Then m_bModifyed = True
                    m_clsAngleDiffParamHeight.AngleDiffs(i).Account = False
                End If
            Next
    
            '作成。
            Set MakeAccountAngleDiffHeight = m_clsAccountMaker.MakeAngleDiffHeight(PageNumberVisible, IIf(m_bGeoidoEnable, m_sGeoidoPath, ""), m_bSemiDynaEnable, objSelectedAngleDiffs)


        End Function
        '*****************************************************************************
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '点検計算簿(楕円体高の閉合差)を作成する。
        '
        'objSelectedAngleDiffs で指定された閉合差の帳票を作成する。
        '
        '引き数：
        'objSelectedAngleDiffs 出力する閉合差。要素は AngleDiff オブジェクト。キーは AngleDiff オブジェクトの Number。
        '
        '戻り値：作成した ActiveReport オブジェクトを返す。
        */
#if false
        /*
         *************************** 修正要 sakai
         */
        public ActiveReport MakeAccountAngleDiffHeight(Collection objSelectedAngleDiffs)
        {
            return null;
        }
#endif
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '2023/07/12 Hitz H.Nakamura **************************************************
        'GNSS水準測量対応。
        '点検計算簿(前後半基線較差)の追加。
        '点検計算簿(前後半基線較差)を作成する。
        '
        '引き数：
        'clsAccountOverlapParam 点検計算簿(前後半基線較差)パラメータ。
        '
        '戻り値：作成した ActiveReport オブジェクト。
        Public Function MakeAccountOverlapHalf(ByVal clsAccountOverlapParam As AccountOverlapParam) As ActiveReport

            '一致しない場合、更新フラグをONにする。
            If StrComp(m_clsAccountOverlapParam.Fixed, clsAccountOverlapParam.Fixed) <> 0 Then m_bModifyed = True
            m_clsAccountOverlapParam.Fixed = clsAccountOverlapParam.Fixed
    
            '作成。
            Set MakeAccountOverlapHalf = m_clsAccountMaker.MakeOverlap(PageNumberVisible, m_clsAccountOverlapParam, 2, True)


        End Function
        '*****************************************************************************
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '点検計算簿(前後半基線較差)を作成する。
        '
        '引き数：
        'clsAccountOverlapParam 点検計算簿(前後半基線較差)パラメータ。
        '
        '戻り値：作成した ActiveReport オブジェクト。
        */
#if false
        /*
         *************************** 修正要 sakai
         */
        public ActiveReport MakeAccountOverlapHalf(AccountOverlapParam clsAccountOverlapParam)
        {
            return null;
        }
#endif
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '偏心計算簿を作成する。
        '
        '引き数：
        'clsAccountParam 偏心計算簿パラメータ。
        'bModify 更新許可フラグ。True=パラメータを更新することを許可する。False=パラメータの更新は不可。
        '
        '戻り値：作成した ActiveReport オブジェクトを返す。
        Public Function MakeAccountEccentricCorrect(ByVal clsAccountParam As AccountParam, ByVal bModify As Boolean) As ActiveReport

            If bModify Then
                '一致しない場合、更新フラグをONにする。
                If Not m_clsAccountParamEccentricCorrect.Compare(clsAccountParam) Then m_bModifyed = True
                Let m_clsAccountParamEccentricCorrect = clsAccountParam
            End If
    
            '作成。
            Set MakeAccountEccentricCorrect = m_clsAccountMaker.MakeEccentricCorrect(PageNumberVisible, IIf(m_bGeoidoEnable, m_sGeoidoPath, ""), clsAccountParam)


        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '偏心計算簿を作成する。
        '
        '引き数：
        'clsAccountParam 偏心計算簿パラメータ。
        'bModify 更新許可フラグ。True=パラメータを更新することを許可する。False=パラメータの更新は不可。
        '
        '戻り値：作成した ActiveReport オブジェクトを返す。
        */
#if false
        /*
         *************************** 修正要 sakai
         */
        public ActiveReport MakeAccountEccentricCorrect(AccountParam clsAccountParam, bool bModify)
        {
            return null;
        }
#endif
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '2009/11 H.Nakamura
        '元期→今期補正表を作成する。
        '
        '引き数：
        'clsAccountParam セミ・ダイナミック補正表パラメータ。
        'bModify 更新許可フラグ。True=パラメータを更新することを許可する。False=パラメータの更新は不可。
        '
        '戻り値：作成した ActiveReport オブジェクトを返す。
        Public Function MakeAccountSemiDynaGAN2KON(ByVal clsAccountParam As AccountParam, ByVal bModify As Boolean) As ActiveReport

            If bModify Then
                '一致しない場合、更新フラグをONにする。
                If Not m_clsAccountParamSemiDyna.Compare(clsAccountParam) Then m_bModifyed = True
                Let m_clsAccountParamSemiDyna = clsAccountParam
            End If
    
            '作成。
            Set MakeAccountSemiDynaGAN2KON = m_clsAccountMaker.MakeSemiDynaGAN2KON(PageNumberVisible, IIf(m_bGeoidoEnable, m_sGeoidoPath, ""), m_bSemiDynaEnable, clsAccountParam)


        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '元期→今期補正表を作成する。
        '
        '引き数：
        'clsAccountParam セミ・ダイナミック補正表パラメータ。
        'bModify 更新許可フラグ。True=パラメータを更新することを許可する。False=パラメータの更新は不可。
        '
        '戻り値：作成した ActiveReport オブジェクトを返す。
        */
#if false
        /*
         *************************** 修正要 sakai
         */
        public ActiveReport MakeAccountSemiDynaGAN2KON(AccountParam clsAccountParam, bool bModify)
        {
            return null;
        }
#endif
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '地籍図根三角測量精度管理表(環閉合差)を作成する。
        '
        'objSelectedAngleDiffs で指定された閉合差の帳票を作成する。
        '
        '引き数：
        'objSelectedAngleDiffs 出力する閉合差。要素は AngleDiff オブジェクト。キーは AngleDiff オブジェクトの Number。
        'clsAccountCadastralParam 地籍図根三角測量精度管理表パラメータ。
        '
        '戻り値：作成した ActiveReport オブジェクトを返す。
        Public Function MakeAccountCadastralAccuracyRing(ByVal objSelectedAngleDiffs As Collection, ByVal clsAccountCadastralParam As AccountCadastralParam) As ActiveReport

            ReDim clsAngleDiffs(-1 To -1)
            Dim i As Long
            For i = 0 To m_clsAngleDiffParamRing.Count - 1
                Dim clsAngleDiff As AngleDiff
                If LookupCollectionObject(objSelectedAngleDiffs, clsAngleDiff, m_clsAngleDiffParamRing.AngleDiffs(i).Number) Then
                    '一致しない場合、更新フラグをONにする。
                    If Not m_clsAngleDiffParamRing.AngleDiffs(i).Account(1) Then m_bModifyed = True
                    m_clsAngleDiffParamRing.AngleDiffs(i).Account(1) = True
                Else
                    '一致しない場合、更新フラグをONにする。
                    If m_clsAngleDiffParamRing.AngleDiffs(i).Account(1) Then m_bModifyed = True
                    m_clsAngleDiffParamRing.AngleDiffs(i).Account(1) = False
                End If
            Next
            '一致しない場合、更新フラグをONにする。
            If Not m_clsAccountCadastralParam.Compare(clsAccountCadastralParam) Then m_bModifyed = True
            Let m_clsAccountCadastralParam = clsAccountCadastralParam
    
            '作成。
            Set MakeAccountCadastralAccuracyRing = m_clsAccountMaker.MakeCadastralAccuracyRing(PageNumberVisible, objSelectedAngleDiffs, clsAccountCadastralParam)


        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '地籍図根三角測量精度管理表(環閉合差)を作成する。
        '
        'objSelectedAngleDiffs で指定された閉合差の帳票を作成する。
        '
        '引き数：
        'objSelectedAngleDiffs 出力する閉合差。要素は AngleDiff オブジェクト。キーは AngleDiff オブジェクトの Number。
        'clsAccountCadastralParam 地籍図根三角測量精度管理表パラメータ。
        '
        '戻り値：作成した ActiveReport オブジェクトを返す。
        */
#if false
        /*
         *************************** 修正要 sakai
         */
        public ActiveReport MakeAccountCadastralAccuracyRing(Collection objSelectedAngleDiffs, AccountCadastralParam clsAccountCadastralParam)
        {
            return null;
        }
#endif
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '地籍図根三角測量精度管理表(重複辺の較差)を作成する。
        '
        '引き数：
        'clsAccountOverlapParam 点検計算簿(重複基線)パラメータ。
        'clsAccountCadastralParam 地籍図根三角測量精度管理表パラメータ。
        '
        '戻り値：作成した ActiveReport オブジェクトを返す。
        Public Function MakeAccountCadastralAccuracyOverlap(ByVal clsAccountOverlapParam As AccountOverlapParam, ByVal clsAccountCadastralParam As AccountCadastralParam) As ActiveReport

            '一致しない場合、更新フラグをONにする。
            If Not m_clsAccountOverlapParam.Compare(clsAccountOverlapParam) Then m_bModifyed = True
            Let m_clsAccountOverlapParam = clsAccountOverlapParam
            If Not m_clsAccountCadastralParam.Compare(clsAccountCadastralParam) Then m_bModifyed = True
            Let m_clsAccountCadastralParam = clsAccountCadastralParam
    
            '作成。
            Set MakeAccountCadastralAccuracyOverlap = m_clsAccountMaker.MakeCadastralAccuracyOverlap(PageNumberVisible, m_clsAccountOverlapParam, clsAccountCadastralParam)


        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '地籍図根三角測量精度管理表(重複辺の較差)を作成する。
        '
        '引き数：
        'clsAccountOverlapParam 点検計算簿(重複基線)パラメータ。
        'clsAccountCadastralParam 地籍図根三角測量精度管理表パラメータ。
        '
        '戻り値：作成した ActiveReport オブジェクトを返す。
        */
#if false
        /*
         *************************** 修正要 sakai
         */
        public ActiveReport MakeAccountCadastralAccuracyOverlap(AccountOverlapParam clsAccountOverlapParam, AccountCadastralParam clsAccountCadastralParam)
        {
            return null;
        }
#endif
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '地籍測量、点検計算簿(環閉合差)を作成する。
        '
        'objSelectedAngleDiffs で指定された閉合差の帳票を作成する。
        '
        '引き数：
        'objSelectedAngleDiffs 出力する閉合差。要素は AngleDiff オブジェクト。キーは AngleDiff オブジェクトの Number。
        '
        '戻り値：作成した ActiveReport オブジェクトを返す。
        Public Function MakeAccountCadastralRing(ByVal objSelectedAngleDiffs As Collection) As ActiveReport

            ReDim clsAngleDiffs(-1 To -1)
            Dim i As Long
            For i = 0 To m_clsAngleDiffParamRing.Count - 1
                Dim clsAngleDiff As AngleDiff
                If LookupCollectionObject(objSelectedAngleDiffs, clsAngleDiff, m_clsAngleDiffParamRing.AngleDiffs(i).Number) Then
                    '一致しない場合、更新フラグをONにする。
                    If Not m_clsAngleDiffParamRing.AngleDiffs(i).Account(3) Then m_bModifyed = True
                    m_clsAngleDiffParamRing.AngleDiffs(i).Account(3) = True
                Else
                    '一致しない場合、更新フラグをONにする。
                    If m_clsAngleDiffParamRing.AngleDiffs(i).Account(3) Then m_bModifyed = True
                    m_clsAngleDiffParamRing.AngleDiffs(i).Account(3) = False
                End If
            Next
    
            '作成。
            Set MakeAccountCadastralRing = m_clsAccountMaker.MakeCadastralRing(PageNumberVisible, objSelectedAngleDiffs)


        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '地籍測量、点検計算簿(環閉合差)を作成する。
        '
        'objSelectedAngleDiffs で指定された閉合差の帳票を作成する。
        '
        '引き数：
        'objSelectedAngleDiffs 出力する閉合差。要素は AngleDiff オブジェクト。キーは AngleDiff オブジェクトの Number。
        '
        '戻り値：作成した ActiveReport オブジェクトを返す。
        */
#if false
        /*
         *************************** 修正要 sakai
         */
        public ActiveReport MakeAccountCadastralRing(Collection objSelectedAngleDiffs)
        {
            return null;
        }
#endif
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '地籍測量、点検計算簿(電子基準点間の閉合差)を作成する。
        '
        'objSelectedAngleDiffs で指定された閉合差の帳票を作成する。
        '
        '引き数：
        'objSelectedAngleDiffs 出力する閉合差。要素は AngleDiff オブジェクト。キーは AngleDiff オブジェクトの Number。
        '
        '戻り値：作成した ActiveReport オブジェクトを返す。
        Public Function MakeAccountCadastralBetween(ByVal objSelectedAngleDiffs As Collection) As ActiveReport

            ReDim clsAngleDiffs(-1 To -1)
            Dim i As Long
            For i = 0 To m_clsAngleDiffParamBetween.Count - 1
                Dim clsAngleDiff As AngleDiff
                If LookupCollectionObject(objSelectedAngleDiffs, clsAngleDiff, m_clsAngleDiffParamBetween.AngleDiffs(i).Number) Then
                    '一致しない場合、更新フラグをONにする。
                    If Not m_clsAngleDiffParamBetween.AngleDiffs(i).Account(3) Then m_bModifyed = True
                    m_clsAngleDiffParamBetween.AngleDiffs(i).Account(3) = True
                Else
                    '一致しない場合、更新フラグをONにする。
                    If m_clsAngleDiffParamBetween.AngleDiffs(i).Account(3) Then m_bModifyed = True
                    m_clsAngleDiffParamBetween.AngleDiffs(i).Account(3) = False
                End If
            Next
    
            '作成。
            Set MakeAccountCadastralBetween = m_clsAccountMaker.MakeCadastralBetween(PageNumberVisible, IIf(m_bGeoidoEnable, m_sGeoidoPath, ""), m_bSemiDynaEnable, objSelectedAngleDiffs)


        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '地籍測量、点検計算簿(電子基準点間の閉合差)を作成する。
        '
        'objSelectedAngleDiffs で指定された閉合差の帳票を作成する。
        '
        '引き数：
        'objSelectedAngleDiffs 出力する閉合差。要素は AngleDiff オブジェクト。キーは AngleDiff オブジェクトの Number。
        '
        '戻り値：作成した ActiveReport オブジェクトを返す。
        */
#if false
        /*
         *************************** 修正要 sakai
         */
        public ActiveReport MakeAccountCadastralBetween(Collection objSelectedAngleDiffs)
        {
            return null;
        }
#endif
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'ΔXYZからΔNEUへの変換(環閉合差)を作成する。
        '
        'objSelectedAngleDiffs で指定された閉合差の帳票を作成する。
        '
        '引き数：
        'objSelectedAngleDiffs 出力する閉合差。要素は AngleDiff オブジェクト。キーは AngleDiff オブジェクトの Number。
        '
        '戻り値：作成した ActiveReport オブジェクトを返す。
        Public Function MakeAccount⊿XYZ2⊿NEURing(ByVal objSelectedAngleDiffs As Collection) As ActiveReport

            ReDim clsAngleDiffs(-1 To -1)
            Dim i As Long
            For i = 0 To m_clsAngleDiffParamRing.Count - 1
                Dim clsAngleDiff As AngleDiff
                If LookupCollectionObject(objSelectedAngleDiffs, clsAngleDiff, m_clsAngleDiffParamRing.AngleDiffs(i).Number) Then
                    '一致しない場合、更新フラグをONにする。
                    If Not m_clsAngleDiffParamRing.AngleDiffs(i).Account(2) Then m_bModifyed = True
                    m_clsAngleDiffParamRing.AngleDiffs(i).Account(2) = True
                Else
                    '一致しない場合、更新フラグをONにする。
                    If m_clsAngleDiffParamRing.AngleDiffs(i).Account(2) Then m_bModifyed = True
                    m_clsAngleDiffParamRing.AngleDiffs(i).Account(2) = False
                End If
            Next
    
            '作成。
            Set MakeAccount⊿XYZ2⊿NEURing = m_clsAccountMaker.Make⊿XYZ2⊿NEURing(PageNumberVisible, objSelectedAngleDiffs)


        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        'ΔXYZからΔNEUへの変換(環閉合差)を作成する。
        '
        'objSelectedAngleDiffs で指定された閉合差の帳票を作成する。
        '
        '引き数：
        'objSelectedAngleDiffs 出力する閉合差。要素は AngleDiff オブジェクト。キーは AngleDiff オブジェクトの Number。
        '
        '戻り値：作成した ActiveReport オブジェクトを返す。
        */
#if false
        /*
         *************************** 修正要 sakai
         */
        public ActiveReport MakeAccount_XYZ2_NEURing(Collection objSelectedAngleDiffs)
        {
            return null;
        }
#endif
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'ΔXYZからΔNEUへの変換(電子基準点間の閉合差)を作成する。
        '
        'objSelectedAngleDiffs で指定された閉合差の帳票を作成する。
        '
        '引き数：
        'objSelectedAngleDiffs 出力する閉合差。要素は AngleDiff オブジェクト。キーは AngleDiff オブジェクトの Number。
        '
        '戻り値：作成した ActiveReport オブジェクトを返す。
        Public Function MakeAccount⊿XYZ2⊿NEUBetween(ByVal objSelectedAngleDiffs As Collection) As ActiveReport

            ReDim clsAngleDiffs(-1 To -1)
            Dim i As Long
            For i = 0 To m_clsAngleDiffParamBetween.Count - 1
                Dim clsAngleDiff As AngleDiff
                If LookupCollectionObject(objSelectedAngleDiffs, clsAngleDiff, m_clsAngleDiffParamBetween.AngleDiffs(i).Number) Then
                    '一致しない場合、更新フラグをONにする。
                    If Not m_clsAngleDiffParamBetween.AngleDiffs(i).Account(2) Then m_bModifyed = True
                    m_clsAngleDiffParamBetween.AngleDiffs(i).Account(2) = True
                Else
                    '一致しない場合、更新フラグをONにする。
                    If m_clsAngleDiffParamBetween.AngleDiffs(i).Account(2) Then m_bModifyed = True
                    m_clsAngleDiffParamBetween.AngleDiffs(i).Account(2) = False
                End If
            Next

            '作成。
            Set MakeAccount⊿XYZ2⊿NEUBetween = m_clsAccountMaker.Make⊿XYZ2⊿NEUBetween(PageNumberVisible, objSelectedAngleDiffs)


        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        'ΔXYZからΔNEUへの変換(電子基準点間の閉合差)を作成する。
        '
        'objSelectedAngleDiffs で指定された閉合差の帳票を作成する。
        '
        '引き数：
        'objSelectedAngleDiffs 出力する閉合差。要素は AngleDiff オブジェクト。キーは AngleDiff オブジェクトの Number。
        '
        '戻り値：作成した ActiveReport オブジェクトを返す。
        */
#if false
        /*
         *************************** 修正要 sakai
         */
        public ActiveReport MakeAccount_XYZ2_NEUBetween(Collection objSelectedAngleDiffs)
        {
            return null;
        }
#endif
        //==========================================================================================


        //==========================================================================================
        /*[VB]
        '座標一覧表を作成する。   2007/7/18 NGS Yamada
        '
        '引き数：
        'clsAccountParam 観測記簿パラメータ。
        'bModify 更新許可フラグ。True=パラメータを更新することを許可する。False=パラメータの更新は不可。
        'hWnd オーナーウィンドウ。メッセージボックス表示後、再描画するウィンドウのハンドル。
        '
        '戻り値：作成した ActiveReport オブジェクトを返す。
        Public Function MakeAccountResultBase(ByVal clsAccountParam As AccountParam, ByVal bModify As Boolean, ByVal hWnd As Long) As ActiveReport

            If bModify Then
                '一致しない場合、更新フラグをONにする。
                If Not m_clsAccountParamResultBase.Compare(clsAccountParam) Then m_bModifyed = True
                Let m_clsAccountParamResultBase = clsAccountParam
            End If
    
            '作成。
            Set MakeAccountResultBase = m_clsAccountMaker.MakeResultBase(PageNumberVisible, m_nCoordNum, IIf(m_bGeoidoEnable, m_sGeoidoPath, ""), m_bSemiDynaEnable, clsAccountParam, hWnd)


        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '座標一覧表を作成する。   2007/7/18 NGS Yamada
        '
        '引き数：
        'clsAccountParam 観測記簿パラメータ。
        'bModify 更新許可フラグ。True=パラメータを更新することを許可する。False=パラメータの更新は不可。
        'hWnd オーナーウィンドウ。メッセージボックス表示後、再描画するウィンドウのハンドル。
        '
        '戻り値：作成した ActiveReport オブジェクトを返す。
        */
#if false
        /*
         *************************** 修正要 sakai
         */
        public ActiveReport MakeAccountResultBase(AccountParam clsAccountParam, bool bModify, long hWnd)
        {
            return null;
        }
#endif
        //==========================================================================================



        //==========================================================================================
        /*[VB]
        '外部出力ファイルを出力する。
        '
        '引き数：
        'nType 外部出力ファイル種別。
        'clsOutputParam 外部出力ファイル出力パラメータ。
        'bModify 更新許可フラグ。True=パラメータを更新することを許可する。False=パラメータの更新は不可。
        'clsOutputInterface 外部ファイル出力インターフェース。
        'clsProgressInterface ProgressInterface オブジェクト。
        Public Sub GenerateOutput(ByVal nType As OUTPUT_TYPE, ByVal clsOutputParam As OutputParam, ByVal bModify As Boolean, ByVal clsOutputInterface As OutputInterface, ByVal clsProgressInterface As ProgressInterface)

            'NVB は NVF に書き込む。
            If nType = OUTPUT_TYPE_NVB Then nType = OUTPUT_TYPE_NVF
    
            '出力。
            Call GenerateOutputFile(clsOutputParam, Me, clsOutputInterface, clsProgressInterface)
    
            '更新許可されていない場合、帳票パラメータは更新しない。パスは更新する。
            If Not bModify Then Let clsOutputParam.AccountParam = m_clsOutputParam(nType).AccountParam
    
            'OLEオートメーションがONの場合、パスは更新しない。
            If clsOutputParam.Automation Then Let clsOutputParam.Path = m_clsOutputParam(nType).Path
    
            '一致しない場合、更新フラグをONにする。
            If Not m_clsOutputParam(nType).Compare(clsOutputParam) Then m_bModifyed = True
            Let m_clsOutputParam(nType) = clsOutputParam
    
            '2023/06/26 Hitz H.Nakamura **************************************************
            'GNSS水準測量対応。
            '前半後半較差の追加。
            If m_clsAccountOverlapParam.Fixed<> clsOutputParam.Fixed Then
                m_clsAccountOverlapParam.Fixed = clsOutputParam.Fixed
                m_bModifyed = True
            End If
            '*****************************************************************************


        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '外部出力ファイルを出力する。
        '
        '引き数：
        'nType 外部出力ファイル種別。
        'clsOutputParam 外部出力ファイル出力パラメータ。
        'bModify 更新許可フラグ。True=パラメータを更新することを許可する。False=パラメータの更新は不可。
        'clsOutputInterface 外部ファイル出力インターフェース。
        'clsProgressInterface ProgressInterface オブジェクト。
        */
#if false
        /*
         *************************** 修正要 sakai
         */
        public void GenerateOutput(OUTPUT_TYPE nType, OutputParam clsOutputParam, bool bModify, OutputInterface clsOutputInterface, ProgressInterface clsProgressInterface)
        {
            return;
        }
#endif
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'DXFファイルを出力する。
        '
        '引き数：
        'nType DXFファイル種別。
        'sDefPath デフォルトのパス。
        Public Sub GenerateDXF(ByVal nType As DXF_TYPE, ByVal sDefPath As String)

            Dim sTitle As String
            Select Case nType
            Case DXF_TYPE_OBS
                sTitle = DXF_TITLE_OBS
            End Select


            Dim sPath As String
            Dim sSectionName As String
            Dim sWorkYear As String
            Dim sWorkType As String
            Dim sSurveyMethod As String
            Dim sSurveyPlanOrganization As String
            Dim sSurveyWorkOrganization As String
            sPath = IIf(m_clsDXFParam(nType).Path<> "", m_clsDXFParam(nType).Path, sDefPath)
            sSectionName = m_sDistrictName
            'iniファイルを使わずに標準機能を使うように変更 2006/10/12 NGS Yamada
            sWorkYear = Format(Now, "ggge") + "年度"
        '    sWorkYear = GetPrivateProfileString(PROFILE_DEF_SEC_ERA, PROFILE_DEF_KEY_NAME, "", App.Path & "\" & PROFILE_DEF_NAME) & CStr(GetEraFiscal(Now)) & "年度"
            sWorkType = m_clsDXFParam(nType).WorkType
            sSurveyMethod = m_clsDXFParam(nType).SurveyMethod
            sSurveyPlanOrganization = m_clsDXFParam(nType).SurveyPlanOrganization
            sSurveyWorkOrganization = m_clsDXFParam(nType).SurveyWorkOrganization
    
            '出力。
            Dim objVectors As New Collection
            Dim objIsolatePoints As New Collection
            Call GetNSDVectorsNSS(NetworkModel, CoordNum, ACCOUNT_DECIMAL_XYZ, objVectors, objIsolatePoints)
            On Error GoTo CancelHandler
            Dim objNSD As New NSDObject
            Call objNSD.Make2(sPath, -1, sSectionName, sWorkYear, sWorkType, sSurveyMethod, sSurveyPlanOrganization, sSurveyWorkOrganization, sTitle, m_nCoordNum, objVectors, objIsolatePoints, True, True)
            On Error GoTo 0
    
            '一致しない場合、更新フラグをONにする。
            Dim clsDXFParam As New DXFParam
            Let clsDXFParam = m_clsDXFParam(nType)
            clsDXFParam.Path = sPath
            'clsDXFParam.WorkYear = sWorkYear '作業年度は記憶しない。
            clsDXFParam.WorkType = sWorkType
            clsDXFParam.SurveyMethod = sSurveyMethod
            clsDXFParam.SurveyPlanOrganization = sSurveyPlanOrganization
            clsDXFParam.SurveyWorkOrganization = sSurveyWorkOrganization
            If Not m_clsDXFParam(nType).Compare(clsDXFParam) Then m_bModifyed = True
            Let m_clsDXFParam(nType) = clsDXFParam


            Exit Sub


        CancelHandler:
            If Err.Number<> cdlCancel Then Call Err.Raise(Err.Number, Err.Source, Err.Description, Err.HelpFile, Err.HelpContext)

        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        'DXFファイルを出力する。
        '
        '引き数：
        'nType DXFファイル種別。
        'sDefPath デフォルトのパス。
        */
#if false
        /*
         *************************** 修正要 sakai
         */
        public void GenerateDXF(DXF_TYPE nType, string sDefPath)
        {
            return;
        }
#endif
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '本点を削除する。
        '
        '引き数：
        'clsGenuinePoints 本点(実観測点)。
        Public Sub RemoveGenuinePoint(ByVal clsGenuinePoints As ObservationPoint)
            Call clsGenuinePoints.CorrectPoint.UpdateCorrectPoint(Nothing)
            Dim objObservationPoints As New Collection
            Call objObservationPoints.Add(clsGenuinePoints.TopParentPoint, Hex$(GetPointer(clsGenuinePoints.TopParentPoint)))
            Call RemoveObservationPoint(objObservationPoints)
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '本点を削除する。
        '
        '引き数：
        'clsGenuinePoints 本点(実観測点)。
        */
#if false
        /*
         *************************** 修正要 sakai
         */
        public void RemoveGenuinePoint(ObservationPoint clsGenuinePoints)
        {
            return;
        }
#endif
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '観測点を削除する。
        '
        '引き数：
        'objObservationPoints 対象とする観測点。要素は ObservationPoint オブジェクト(代表観測点)。キーは任意。
        Public Sub RemoveObservationPoint(ByVal objObservationPoints As Collection)

            '観測点フォルダ。
            Dim sObsPointPath As String
            sObsPointPath = App.Path & TEMPORARY_PATH & "." & OBSPOINT_PATH

            Dim objIsolatePointChainCollection As Collection
            Set objIsolatePointChainCollection = m_clsNetworkModel.MakeIsolatePointChainCollection
            Dim objBaseLineVectorChainCollection As Collection
            Set objBaseLineVectorChainCollection = m_clsNetworkModel.MakeBaseLineVectorChainCollection

            Dim objObservationPoints2 As New Collection
            Dim clsObservationPoint As ObservationPoint
            '本点の削除。
            For Each clsObservationPoint In objObservationPoints
                m_bModifyed = True
                If clsObservationPoint.Genuine Then
                    '偏心補正をOFFにする。
                    Call clsObservationPoint.CorrectPoint.UpdateCorrectPoint(Nothing)
                    '削除。
                    Call m_clsNetworkModel.RemoveRepresentPoint(clsObservationPoint.TopParentPoint, sObsPointPath, objIsolatePointChainCollection, objBaseLineVectorChainCollection)
                Else
                    Call objObservationPoints2.Add(clsObservationPoint)
                End If
            Next
    
            '偏心補正の再計算が必要な本点。
            Dim objGenuinePoints As New Collection
    
            '残りの削除。
            For Each clsObservationPoint In objObservationPoints2
                m_bModifyed = True
                Dim clsEccentricPoint As ObservationPoint
                Set clsEccentricPoint = Nothing
                If clsObservationPoint.Eccentric Then
                    '偏心点の場合、リンクを変更する。
                    If clsObservationPoint.TopParentPoint.PrevPoint Is Nothing Then
                        'リンクを変更。
                        Set clsEccentricPoint = clsObservationPoint.TopParentPoint.NextPoint
                    Else
                        Set clsEccentricPoint = clsObservationPoint.HeadPoint
                    End If
                    If clsEccentricPoint Is Nothing Then
                        '観測点がなくなる場合は、本点も削除する。
                        Call RemoveAtCollection(objGenuinePoints, clsObservationPoint.Number)
                        Call m_clsNetworkModel.RemoveRepresentPoint(clsObservationPoint.CorrectPoint.TopParentPoint, sObsPointPath, objIsolatePointChainCollection, objBaseLineVectorChainCollection)
                    Else
                        Call SetAtCollectionObject(objGenuinePoints, clsEccentricPoint.CorrectPoint, clsEccentricPoint.Number)
                        Call clsObservationPoint.CorrectPoint.UpdateCorrectPoint(clsEccentricPoint)
                    End If
                End If
                '基線ベクトルの反対側の観測点。
                Dim clsBaseLineVectors() As BaseLineVector
                ReDim clsBaseLineVectors(-1 To -1)
                Call m_clsNetworkModel.GetConnectBaseLineVectors(clsObservationPoint.TopParentPoint, clsBaseLineVectors)
                Dim clsBuddyPoints() As ObservationPoint
                ReDim clsBuddyPoints(-1 To UBound(clsBaseLineVectors))
                Dim i As Long
                For i = 0 To UBound(clsBaseLineVectors)
                    '基線ベクトルの反対側の観測点。
                    If clsBaseLineVectors(i).StrPoint.Number = clsObservationPoint.Number Then
                        Set clsBuddyPoints(i) = clsBaseLineVectors(i).EndPoint.TopParentPoint
                    Else
                        Set clsBuddyPoints(i) = clsBaseLineVectors(i).StrPoint.TopParentPoint
                    End If
                    '解析済み基線ベクトルの終点が反対側であり、偏心点である場合、その偏心補正を再計算する。
                    If clsBaseLineVectors(i).Analysis <= ANALYSIS_STATUS_FIX Then
                        If clsBaseLineVectors(i).AnalysisStrPoint.Number = clsObservationPoint.Number Then
                            If clsBaseLineVectors(i).AnalysisEndPoint.Eccentric Then
                                Set clsEccentricPoint = clsBaseLineVectors(i).AnalysisEndPoint
                                Call SetAtCollectionObject(objGenuinePoints, clsEccentricPoint.CorrectPoint, clsEccentricPoint.Number)
                            End If
                        End If
                    End If
                Next
                '削除。
                Call m_clsNetworkModel.RemoveRepresentPoint(clsObservationPoint.TopParentPoint, sObsPointPath, objIsolatePointChainCollection, objBaseLineVectorChainCollection)
                '本点の有効/無効を更新する。
                For i = 0 To UBound(clsBuddyPoints)
                    If clsBuddyPoints(i).Eccentric Then Call m_clsNetworkModel.EnableGenuinePoint(clsBuddyPoints(i))
                Next
            Next
    
            '偏心補正の再計算。
            For Each clsEccentricPoint In objGenuinePoints
                Set clsEccentricPoint = clsEccentricPoint.CorrectPoint.HeadPoint
                Call m_clsNetworkModel.CorrectEccentric(clsEccentricPoint)
                Call m_clsNetworkModel.EnableGenuinePoint(clsEccentricPoint)
            Next


        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]  //5
        /*
        '観測点を削除する。
        '
        '引き数：
        'objObservationPoints 対象とする観測点。要素は ObservationPoint オブジェクト(代表観測点)。キーは任意。
        */
        public void RemoveObservationPoint(Dictionary<string, object> objObservationPoints)
        {

            //'観測点フォルダ。
            /*-----------------------------------------------------------------------------------------------------------------------
                Dim sObsPointPath As String
                sObsPointPath = App.Path & TEMPORARY_PATH & "." & OBSPOINT_PATH

                Dim objIsolatePointChainCollection As Collection
                Set objIsolatePointChainCollection = m_clsNetworkModel.MakeIsolatePointChainCollection
                Dim objBaseLineVectorChainCollection As Collection
                Set objBaseLineVectorChainCollection = m_clsNetworkModel.MakeBaseLineVectorChainCollection
             */
            string App_Path = @"C:\Develop\NetSurv\Src\NS-App\NS-Survey";
            string sObsPointPath = App_Path + TEMPORARY_PATH + "." + OBSPOINT_PATH;

            Dictionary<string, object> objIsolatePointChainCollection = new Dictionary<string, object>();
            objIsolatePointChainCollection = m_clsNetworkModel.MakeIsolatePointChainCollection();

            Dictionary<string, object> objBaseLineVectorChainCollection = new Dictionary<string, object>();
            objBaseLineVectorChainCollection = m_clsNetworkModel.MakeBaseLineVectorChainCollection();


            /*-----------------------------------------------------------------------------------------------------------------------
                Dim objObservationPoints2 As New Collection
                Dim clsObservationPoint As ObservationPoint
                '本点の削除。
                For Each clsObservationPoint In objObservationPoints
                    m_bModifyed = True
                    If clsObservationPoint.Genuine Then
                        '偏心補正をOFFにする。
                        Call clsObservationPoint.CorrectPoint.UpdateCorrectPoint(Nothing)
                        '削除。
                        Call m_clsNetworkModel.RemoveRepresentPoint(clsObservationPoint.TopParentPoint, sObsPointPath, objIsolatePointChainCollection, objBaseLineVectorChainCollection)
                    Else
                        Call objObservationPoints2.Add(clsObservationPoint)
                    End If
                Next
             */
            Dictionary<string, object> objObservationPoints2 = new Dictionary<string, object>();
            ObservationPoint clsObservationPoint;
            //'本点の削除。
            foreach (ObservationPoint obj in objObservationPoints.Values.Cast<ObservationPoint>())
            //  foreach (object obj in objObservationPoints)
            {

                clsObservationPoint = (ObservationPoint)obj;

                m_bModifyed = true;

                if (clsObservationPoint.Genuine())
                {
                    //'偏心補正をOFFにする。
                    clsObservationPoint.CorrectPoint().UpdateCorrectPoint(null);

                    //'削除。
                    m_clsNetworkModel.RemoveRepresentPoint(clsObservationPoint.TopParentPoint(), sObsPointPath, objIsolatePointChainCollection, objBaseLineVectorChainCollection);

                }
                else
                {
                    objObservationPoints2.Add("", clsObservationPoint);
                }

                /*-----------------------------------------------------------------------------------------------------------------------
                    '基線ベクトルの反対側の観測点。
                    Dim clsBaseLineVectors() As BaseLineVector
                    ReDim clsBaseLineVectors(-1 To -1)
                    Call m_clsNetworkModel.GetConnectBaseLineVectors(clsObservationPoint.TopParentPoint, clsBaseLineVectors)
                    Dim clsBuddyPoints() As ObservationPoint
                    ReDim clsBuddyPoints(-1 To UBound(clsBaseLineVectors))
                    Dim i As Long
                    For i = 0 To UBound(clsBaseLineVectors)
                        '基線ベクトルの反対側の観測点。
                        If clsBaseLineVectors(i).StrPoint.Number = clsObservationPoint.Number Then
                            Set clsBuddyPoints(i) = clsBaseLineVectors(i).EndPoint.TopParentPoint
                        Else
                            Set clsBuddyPoints(i) = clsBaseLineVectors(i).StrPoint.TopParentPoint
                        End If
                        '解析済み基線ベクトルの終点が反対側であり、偏心点である場合、その偏心補正を再計算する。
                        If clsBaseLineVectors(i).Analysis <= ANALYSIS_STATUS_FIX Then
                            If clsBaseLineVectors(i).AnalysisStrPoint.Number = clsObservationPoint.Number Then
                                If clsBaseLineVectors(i).AnalysisEndPoint.Eccentric Then
                                    Set clsEccentricPoint = clsBaseLineVectors(i).AnalysisEndPoint
                                    Call SetAtCollectionObject(objGenuinePoints, clsEccentricPoint.CorrectPoint, clsEccentricPoint.Number)
                                End If
                            End If
                        End If
                    Next
                 */
                //基線ベクトルの反対側の観測点。
                //      BaseLineVector oBaseLineVectors; 
                //      ReDim clsBaseLineVectors(-1 To - 1)
                List<BaseLineVector> clsBaseLineVectors = new List<BaseLineVector>();

                //5     m_clsNetworkModel.GetConnectBaseLineVectors(clsObservationPoint.TopParentPoint(), ref clsBaseLineVectors);
                Session_GetConnectBaseLineVectors(clsObservationPoint, ref clsBaseLineVectors);     //5
                //---------------------------------------------------
                //      Dim clsBuddyPoints() As ObservationPoint
                //      ReDim clsBuddyPoints(-1 To UBound(clsBaseLineVectors))
                ObservationPoint[] clsBuddyPoints = new ObservationPoint[clsBaseLineVectors.Count];
                for (int i = 0; i < clsBaseLineVectors.Count; i++)
                {
                    //基線ベクトルの反対側の観測点。
                    if (clsBaseLineVectors[i].StrPoint().Number() == clsObservationPoint.Number())
                    {
                        clsBuddyPoints[i] = clsBaseLineVectors[i].EndPoint().TopParentPoint();
                    }
                    else
                    {
                        clsBuddyPoints[i] = clsBaseLineVectors[i].StrPoint().TopParentPoint();
                    }

                    /*
                        '解析済み基線ベクトルの終点が反対側であり、偏心点である場合、その偏心補正を再計算する。
                        If clsBaseLineVectors(i).Analysis <= ANALYSIS_STATUS_FIX Then
                            If clsBaseLineVectors(i).AnalysisStrPoint.Number = clsObservationPoint.Number Then
                                If clsBaseLineVectors(i).AnalysisEndPoint.Eccentric Then
                                    Set clsEccentricPoint = clsBaseLineVectors(i).AnalysisEndPoint
                                    Call SetAtCollectionObject(objGenuinePoints, clsEccentricPoint.CorrectPoint, clsEccentricPoint.Number)
                                End If
                            End If
                        End If
                     */
                    //'解析済み基線ベクトルの終点が反対側であり、偏心点である場合、その偏心補正を再計算する。
                    if (clsBaseLineVectors[i].Analysis <= ANALYSIS_STATUS.ANALYSIS_STATUS_FIX)
                    {
                        if (clsBaseLineVectors[i].AnalysisStrPoint().Number() == clsObservationPoint.Number())
                        {
                            //  if (clsBaseLineVectors[i].AnalysisEndPoint().Eccentric()){
                            //      clsEccentricPoint = clsBaseLineVectors[i].AnalysisEndPoint;
                            //    SetAtCollectionObject(objGenuinePoints, clsEccentricPoint.CorrectPoint, clsEccentricPoint.Number);
                            //  }
                        }
                    }

                }//for (int i = 0; i <clsBaseLineVectors.Count; i++)


                /*-----------------------------------------------------------------------------------------------------------------------
                    '削除。
                    Call m_clsNetworkModel.RemoveRepresentPoint(clsObservationPoint.TopParentPoint, sObsPointPath, objIsolatePointChainCollection, objBaseLineVectorChainCollection)
                    '本点の有効/無効を更新する。
                    For i = 0 To UBound(clsBuddyPoints)
                        If clsBuddyPoints(i).Eccentric Then Call m_clsNetworkModel.EnableGenuinePoint(clsBuddyPoints(i))
                    Next
                 */
                //削除。
                m_clsNetworkModel.RemoveRepresentPoint(clsObservationPoint.TopParentPoint(), sObsPointPath, objIsolatePointChainCollection, objBaseLineVectorChainCollection);

                //'本点の有効/無効を更新する。
                for(int i = 0; i < clsBuddyPoints.Length; i++)
                {
                    if (clsBuddyPoints[i].Eccentric())
                    {
                        m_clsNetworkModel.EnableGenuinePoint(clsBuddyPoints[i]);
                    }
                }


            }//foreach (object obj in objObservationPoints)


            /*--------------------------------------------------------------------------------------------------
                '偏心補正の再計算。
                For Each clsEccentricPoint In objGenuinePoints
                    Set clsEccentricPoint = clsEccentricPoint.CorrectPoint.HeadPoint
                    Call m_clsNetworkModel.CorrectEccentric(clsEccentricPoint)
                    Call m_clsNetworkModel.EnableGenuinePoint(clsEccentricPoint)
                Next
             */




            return;
        }



#if false
        /*
         *************************** 修正要 sakai
         */
        public void RemoveObservationPoint(Collection objObservationPoints)
        {
            return;
        }
#endif
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'RINEXファイルのエクスポート。
        '
        'clsObservationPoint で指定される観測点の内容をRINEXファイルとして出力する。
        '
        '引き数：
        'clsOutputParam 外部出力ファイル出力パラメータ。
        'sFileName 出力ファイル名。タイトル＋拡張子の年部分(先頭2文字)。例："AAAA.00"
        'clsObservationPoint 対象とする観測点(代表観測点)。
        'clsProgressInterface ProgressInterface オブジェクト。
        Public Sub ExportRinexSingle(ByVal clsOutputParam As OutputParam, ByVal sFileName As String, ByVal clsObservationPoint As ObservationPoint, ByVal clsProgressInterface As ProgressInterface)

            Call CreateRinexFileSingle(clsOutputParam.Path & "\" & sFileName, clsObservationPoint, clsProgressInterface)
    
            '一致しない場合、更新フラグをONにする。
            If Not m_clsOutputParam(OUTPUT_TYPE_RINEX).Compare(clsOutputParam) Then m_bModifyed = True
            Let m_clsOutputParam(OUTPUT_TYPE_RINEX) = clsOutputParam


        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        'RINEXファイルのエクスポート。
        '
        'clsObservationPoint で指定される観測点の内容をRINEXファイルとして出力する。
        '
        '引き数：
        'clsOutputParam 外部出力ファイル出力パラメータ。
        'sFileName 出力ファイル名。タイトル＋拡張子の年部分(先頭2文字)。例："AAAA.00"
        'clsObservationPoint 対象とする観測点(代表観測点)。
        'clsProgressInterface ProgressInterface オブジェクト。
        */
#if false
        /*
         *************************** 修正要 sakai
         */
        public void ExportRinexSingle(OutputParam clsOutputParam, string sFileName, ObservationPoint clsObservationPoint, ProgressInterface clsProgressInterface)
        {
            return;
        }
#endif
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'RINEXファイルのエクスポート。
        '
        'clsOutputParam で指定される観測点(複数)の内容をRINEXファイルとして出力する。
        '
        '引き数：
        'clsOutputParam 外部出力ファイル出力パラメータ。
        'clsProgressInterface ProgressInterface オブジェクト。
        Public Sub ExportRinexMulti(ByVal clsOutputParam As OutputParam, ByVal clsProgressInterface As ProgressInterface)

            Call CreateRinexFileMulti(clsOutputParam, clsProgressInterface)
    
            '一致しない場合、更新フラグをONにする。
            If Not m_clsOutputParam(OUTPUT_TYPE_RINEX).Compare(clsOutputParam) Then m_bModifyed = True
            Let m_clsOutputParam(OUTPUT_TYPE_RINEX) = clsOutputParam

        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        'RINEXファイルのエクスポート。
        '
        'clsOutputParam で指定される観測点(複数)の内容をRINEXファイルとして出力する。
        '
        '引き数：
        'clsOutputParam 外部出力ファイル出力パラメータ。
        'clsProgressInterface ProgressInterface オブジェクト。
        */
#if false
        /*
         *************************** 修正要 sakai
         */
        public void ExportRinexMulti(OutputParam clsOutputParam, ProgressInterface clsProgressInterface)
        {
            return;
        }
#endif
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '偏心補正を行なう。
        '
        '引き数：
        'clsObservationPoint 対象とする観測点(HeadPoint)。
        'clsEccentricCorrectionParam 偏心補正パラメータ。
        'sGenuineNumber 本点番号。
        'sGenuineName 本点名称。
        Public Sub CorrectEccentric(ByVal clsObservationPoint As ObservationPoint, ByVal clsEccentricCorrectionParam As EccentricCorrectionParam, ByVal sGenuineNumber As String, ByVal sGenuineName As String)

            m_bModifyed = True
    
            '偏心補正計算。
            Let clsObservationPoint.Attributes.Common.GenuineNumber = sGenuineNumber
            Let clsObservationPoint.Attributes.Common.GenuineName = sGenuineName
            Let clsObservationPoint.EccentricCorrectionParam = clsEccentricCorrectionParam
            Call m_clsNetworkModel.CorrectEccentric(clsObservationPoint)
            Call m_clsNetworkModel.EnableGenuinePoint(clsObservationPoint)

            If clsObservationPoint.Eccentric Then
                '帳票のチェックをONにする。
                Dim vKey As Variant
                vKey = clsObservationPoint.CorrectPoint.Key
                Call SetAtCollectionVariant(m_clsAccountParamEccentricCorrect.SelectedObjects, vKey, vKey)
            End If

        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '偏心補正を行なう。
        '
        '引き数：
        'clsObservationPoint 対象とする観測点(HeadPoint)。
        'clsEccentricCorrectionParam 偏心補正パラメータ。
        'sGenuineNumber 本点番号。
        'sGenuineName 本点名称。
        */
#if false
        /*
         *************************** 修正要 sakai
         */
        public void CorrectEccentric(ObservationPoint clsObservationPoint, EccentricCorrectionParam clsEccentricCorrectionParam, string sGenuineNumber, string sGenuineName)
        {
            return;
        }
#endif
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '指定された観測点のモードを設定する。
        '
        '設定が変更されたオブジェクトの IsList をONに設定する。
        '
        '引き数：
        'clsObservationPoint 対象とする観測点(代表観測点)。
        'nMode 観測点モード。
        Public Sub SetObsPntMode(ByVal clsObservationPoint As ObservationPoint, ByVal nMode As OBJ_MODE)
            Call m_clsNetworkModel.SetObsPntMode(clsObservationPoint, nMode)
            m_bModifyed = True
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '指定された観測点のモードを設定する。
        '
        '設定が変更されたオブジェクトの IsList をONに設定する。
        '
        '引き数：
        'clsObservationPoint 対象とする観測点(代表観測点)。
        'nMode 観測点モード。
        */
        public void SetObsPntMode(ObservationPoint clsObservationPoint, OBJ_MODE nMode) //4
        {
            m_clsNetworkModel.SetObsPntMode(clsObservationPoint, nMode);
            m_bModifyed = true;
        }

#if false
        /*
         *************************** 修正要 sakai
         */
        public void SetObsPntMode(ObservationPoint clsObservationPoint, OBJ_MODE nMode)
        {
            m_clsNetworkModel.SetObsPntMode(clsObservationPoint, nMode);
            m_bModifyed = true;
            return;
        }
#endif
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '指定された基線ベクトルの種類を設定する。
        '
        '設定が変更されたオブジェクトの IsList をONに設定する。
        '
        '引き数：
        'clsBaseLineVector 対象とする基線ベクトル。
        'nLineType 基線ベクトル種類。
        Public Sub SetLineType(ByVal clsBaseLineVector As BaseLineVector, ByVal nLineType As OBJ_MODE)

            Call m_clsNetworkModel.SetLineType(clsBaseLineVector, nLineType)
    
            '偏心補正の再計算。
            If clsBaseLineVector.Analysis <= ANALYSIS_STATUS_FIX Then
                If clsBaseLineVector.AnalysisEndPoint.Eccentric Then
                    Call m_clsNetworkModel.CorrectEccentric(clsBaseLineVector.AnalysisEndPoint)
                    Call m_clsNetworkModel.EnableGenuinePoint(clsBaseLineVector.AnalysisEndPoint)
                    Call m_clsNetworkModel.SetConnectBaseLineVectorsIsListEx(clsBaseLineVector.AnalysisEndPoint)
                End If
            End If

            m_bModifyed = True


        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '指定された基線ベクトルの種類を設定する。
        '
        '設定が変更されたオブジェクトの IsList をONに設定する。
        '
        '引き数：
        'clsBaseLineVector 対象とする基線ベクトル。
        'nLineType 基線ベクトル種類。
        */
        public void SetLineType(BaseLineVector clsBaseLineVector, OBJ_MODE nLineType)   //4
        {
            m_clsNetworkModel.SetLineType(clsBaseLineVector, nLineType);

            /*
            '偏心補正の再計算。
            If clsBaseLineVector.Analysis <= ANALYSIS_STATUS_FIX Then
                If clsBaseLineVector.AnalysisEndPoint.Eccentric Then
                    Call m_clsNetworkModel.CorrectEccentric(clsBaseLineVector.AnalysisEndPoint)
                    Call m_clsNetworkModel.EnableGenuinePoint(clsBaseLineVector.AnalysisEndPoint)
                    Call m_clsNetworkModel.SetConnectBaseLineVectorsIsListEx(clsBaseLineVector.AnalysisEndPoint)
                End If
            End If
            */

            return;
        }
#if false
        /*
         *************************** 修正要 sakai
         */
        public void SetLineType(BaseLineVector clsBaseLineVector, OBJ_MODE nLineType)
        {
            return;
        }
#endif
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '基線ベクトルの向きの自動整列。
        '
        '引き数：
        'clsAutoOrderVectorParam 基線ベクトルの向きの自動整列パラメータ。
        Public Sub AutoOrderVector(ByVal clsAutoOrderVectorParam As AutoOrderVectorParam)

            '偏心補正の再計算が必要な偏心点。
            Dim objEccentricPoints As New Collection
            '全ての偏心補正を再計算する。
            Dim clsChainList As ChainList
            Set clsChainList = m_clsNetworkModel.RepresentPointHead
            Do While Not clsChainList Is Nothing
                Dim clsObservationPoint As ObservationPoint
                Set clsObservationPoint = clsChainList.Element
                If clsObservationPoint.Eccentric And clsObservationPoint.PrevPoint Is Nothing Then
                    Call objEccentricPoints.Add(clsObservationPoint)
                End If
                Set clsChainList = clsChainList.NextList
            Loop
    
            '基線ベクトルの向きの自動整列。
            Call mdlAutoOrderVector.Order(m_clsNetworkModel, clsAutoOrderVectorParam)
            Let m_clsAutoOrderVectorParam = clsAutoOrderVectorParam
    
            '偏心補正の再計算。
            Dim clsEccentricPoint As ObservationPoint
            For Each clsEccentricPoint In objEccentricPoints
                Call m_clsNetworkModel.CorrectEccentric(clsEccentricPoint)
                Call m_clsNetworkModel.EnableGenuinePoint(clsEccentricPoint)
            Next


            m_bModifyed = True


        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '基線ベクトルの向きの自動整列。
        '
        '引き数：
        'clsAutoOrderVectorParam 基線ベクトルの向きの自動整列パラメータ。
        */
        public void AutoOrderVector(object oAutoOrderVectorParam)
        {

            AutoOrderVectorParam clsAutoOrderVectorParam = (AutoOrderVectorParam)oAutoOrderVectorParam;

            /*-----------------------------------------------------------------
                '偏心補正の再計算が必要な偏心点。
                Dim objEccentricPoints As New Collection
                '全ての偏心補正を再計算する。
                Dim clsChainList As ChainList
                Set clsChainList = m_clsNetworkModel.RepresentPointHead
                Do While Not clsChainList Is Nothing
                    Dim clsObservationPoint As ObservationPoint
                    Set clsObservationPoint = clsChainList.Element
                    If clsObservationPoint.Eccentric And clsObservationPoint.PrevPoint Is Nothing Then
                        Call objEccentricPoints.Add(clsObservationPoint)
                    End If
                    Set clsChainList = clsChainList.NextList
                Loop
             */

            //'偏心補正の再計算が必要な偏心点。
            Dictionary<string, object> objEccentricPoints = new Dictionary<string, object>();

            //'全ての偏心補正を再計算する。
            ChainList clsChainList;
            clsChainList = m_clsNetworkModel.RepresentPointHead();
            while (clsChainList != null)
            {
                ObservationPoint clsObservationPoint;
                clsObservationPoint = (ObservationPoint)clsChainList.Element;
                if (clsObservationPoint.Eccentric() && clsObservationPoint.PrevPoint() == null)
                {
                    objEccentricPoints.Add( GetPointer(clsObservationPoint).ToString(), clsObservationPoint);
                }
                clsChainList = clsChainList.NextList();
            }


            /*-----------------------------------------------------------------
                '基線ベクトルの向きの自動整列。
                Call mdlAutoOrderVector.Order(m_clsNetworkModel, clsAutoOrderVectorParam)
                Let m_clsAutoOrderVectorParam = clsAutoOrderVectorParam
    
                '偏心補正の再計算。
                Dim clsEccentricPoint As ObservationPoint
                For Each clsEccentricPoint In objEccentricPoints
                    Call m_clsNetworkModel.CorrectEccentric(clsEccentricPoint)
                    Call m_clsNetworkModel.EnableGenuinePoint(clsEccentricPoint)
                Next

                m_bModifyed = True
             */

            //基線ベクトルの向きの自動整列。
            m_clsMdlAutoOrderVector.Order(m_clsNetworkModel, clsAutoOrderVectorParam);
            m_clsAutoOrderVectorParam = clsAutoOrderVectorParam;

            //偏心補正の再計算。
            ObservationPoint clsEccentricPoint;
            foreach (object obj in objEccentricPoints)
            {
                clsEccentricPoint = (ObservationPoint)obj;
                m_clsNetworkModel.CorrectEccentric(clsEccentricPoint);
                m_clsNetworkModel.EnableGenuinePoint(clsEccentricPoint);
            }


            m_bModifyed = true;

            return;
        }
#if false
        /*
         *************************** 修正要 sakai
         */
        public void AutoOrderVector(AutoOrderVectorParam clsAutoOrderVectorParam)
        {
            return;
        }
#endif
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '指定された基線ベクトルの観測データマスクを設定する。
        '
        '引き数：
        'clsBaseLineVector 対象とする基線ベクトル。
        'clsObsDataMask 観測データマスク。
        Public Sub SetObsDataMask(ByVal clsBaseLineVector As BaseLineVector, ByVal clsObsDataMask As ObsDataMask)
            Let clsBaseLineVector.ObsDataMask = clsObsDataMask
            m_bModifyed = True
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '指定された基線ベクトルの観測データマスクを設定する。
        '
        '引き数：
        'clsBaseLineVector 対象とする基線ベクトル。
        'clsObsDataMask 観測データマスク。
        */
#if false
        /*
         *************************** 修正要 sakai
         */
        public void SetObsDataMask(BaseLineVector clsBaseLineVector, ObsDataMask clsObsDataMask)
        {
            clsBaseLineVector.ObsDataMask = clsObsDataMask;
            m_bModifyed = true;
            return;
        }
#endif
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '点検計算結果データセクションの出力。'2016/01/12 Hitz H.Nakamura
        '
        '引き数：
        'nFile ファイル番号。
        Public Sub OutputWriteCheckResult(ByVal nFile As Integer)
    
            '点検計算結果データ開始。
            Print #nFile, vbCrLf & "400="
    
            '重複基線較差。
    
            '重複基線ベクトルを集める。
            Dim clsOverlapCheck As New OverlapCheck
            Call clsOverlapCheck.GatherAll(m_clsNetworkModel.BaseLineVectorHead, False)
    
            '出力するものがなければスキップする。
            If clsOverlapCheck.Count > 0 Then
                '既知点を選ぶ。
                Dim clsObservationPoint As ObservationPoint
                Dim clsChainList As ChainList
                Set clsChainList = m_clsNetworkModel.RepresentPointHead
                Do While Not clsChainList Is Nothing
                    Set clsObservationPoint = clsChainList.Element
                    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                    '2018/02/19 Hitz H.Nakamura
                    'm_clsAccountOverlapParam.Fixed がONになる(つまり点検計算簿(重複基線較差)が表示された)後に、固定点をOFFにされると破綻する。
                    'If clsObservationPoint.Number = m_clsAccountOverlapParam.Fixed And Not clsObservationPoint.Genuine Then Exit Do
                    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                    If clsObservationPoint.Number = m_clsAccountOverlapParam.Fixed And Not clsObservationPoint.Genuine Then
                        If clsObservationPoint.Fixed Then
                            Exit Do
                        End If
                    End If
                    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                    Set clsChainList = clsChainList.NextList
                Loop

                If Not clsChainList Is Nothing Then
                    Set clsObservationPoint = clsObservationPoint.HeadPoint
                    Dim nIndex As Long 'インデックス。
                    nIndex = 0
                    Dim nMaxN As Double
                    Dim nMaxE As Double
                    Dim nMaxU As Double
                    Dim nMaxNAbs As Double
                    Dim nMaxEAbs As Double
                    Dim nMaxUAbs As Double
                    Dim nValue As Double
                    nMaxN = 0
                    nMaxE = 0
                    nMaxU = 0
                    nMaxNAbs = 0
                    nMaxEAbs = 0
                    nMaxUAbs = 0


                    Do While nIndex<clsOverlapCheck.Count
                        Dim clsBaseLineVector1 As BaseLineVector
                        Dim clsBaseLineVector2 As BaseLineVector
                        Set clsBaseLineVector1 = clsOverlapCheck.BaseLineVector1(nIndex)
                        Set clsBaseLineVector2 = clsOverlapCheck.BaseLineVector2(nIndex)
                        Dim clsOverlapPoint As OverlapPoint
                        Set clsOverlapPoint = clsBaseLineVector1.WorkObject
                
                        'ベクトル値。セット１の向きに合わせる。
                        Dim clsVectorAnalysis As CoordinatePoint
                        Set clsVectorAnalysis = clsBaseLineVector1.VectorAnalysis
                        Dim nDX1 As Double
                        Dim nDY1 As Double
                        Dim nDZ1 As Double
                        nDX1 = clsVectorAnalysis.RoundX
                        nDY1 = clsVectorAnalysis.RoundY
                        nDZ1 = clsVectorAnalysis.RoundZ
                        Set clsVectorAnalysis = clsBaseLineVector2.VectorAnalysis
                        Dim nDX2 As Double
                        Dim nDY2 As Double
                        Dim nDZ2 As Double
                        If clsBaseLineVector1.AnalysisStrPoint.HeadPoint Is clsBaseLineVector2.AnalysisStrPoint.HeadPoint Then
                            nDX2 = clsVectorAnalysis.RoundX
                            nDY2 = clsVectorAnalysis.RoundY
                            nDZ2 = clsVectorAnalysis.RoundZ
                        Else
                            nDX2 = -clsVectorAnalysis.RoundX
                            nDY2 = -clsVectorAnalysis.RoundY
                            nDZ2 = -clsVectorAnalysis.RoundZ
                        End If
                
                        '較差XYZ。
                        Dim dXYZ(2, 0) As Double
                        dXYZ(0, 0) = nDX1 - nDX2
                        dXYZ(1, 0) = nDY1 - nDY2
                        dXYZ(2, 0) = nDZ1 - nDZ2
                
                        '較差NEU。
                        Dim dNEU() As Double
                        dNEU = RotateΔXYZ(clsObservationPoint.CoordinateFixed, dXYZ)


                        nValue = Abs(dNEU(0, 0))
                        If nValue > nMaxNAbs Then
                            nMaxN = dNEU(0, 0)
                            nMaxNAbs = nValue
                        End If
                        nValue = Abs(dNEU(1, 0))
                        If nValue > nMaxEAbs Then
                            nMaxE = dNEU(1, 0)
                            nMaxEAbs = nValue
                        End If
                        nValue = Abs(dNEU(2, 0))
                        If nValue > nMaxUAbs Then
                            nMaxU = dNEU(2, 0)
                            nMaxUAbs = nValue
                End If

                        nIndex = nIndex + 1
                    Loop
            
                    '最大重複基線較差N。
                    Print #nFile, "401=" & Format$(nMaxN, "0.0########################")
                    '最大重複基線較差E。
                    Print #nFile, "402=" & Format$(nMaxE, "0.0########################")
                    '最大重複基線較差U。
                    Print #nFile, "403=" & Format$(nMaxU, "0.0########################")
                End If
            End If
    
            '閉合差。
            Dim nDivn(1) As Integer
            nDivn(0) = 404
            nDivn(1) = 410
            Dim j As Integer


            For j = 0 To 1
                Dim clsAngleDiffParam As AngleDiffParam
                Set clsAngleDiffParam = AngleDiffParam(j)
                If Not clsAngleDiffParam Is Nothing Then
                    Dim nUBound As Long
                    Dim bValid As Boolean
                    nUBound = clsAngleDiffParam.Count - 1
                    bValid = False
                    '
                    Dim nCountN As Double
                    Dim nCountE As Double
                    Dim nCountU As Double
                    Dim i As Integer
                    Dim clsAngleDiffResult As AngleDiffResult
                    nMaxN = 0
                    nMaxE = 0
                    nMaxU = 0
                    nMaxNAbs = 0
                    nMaxEAbs = 0
                    nMaxUAbs = 0
                    nCountN = 0
                    nCountE = 0
                    nCountU = 0
                    For i = 0 To nUBound
                        Set clsAngleDiffResult = clsAngleDiffParam.AngleDiffs(i).AngleDiffResult
                        If Not clsAngleDiffResult Is Nothing Then
                            bValid = True
                            nValue = Abs(clsAngleDiffResult.ResultNEU.X)
                            If nValue > nMaxNAbs Then
                                nMaxN = clsAngleDiffResult.ResultNEU.X
                                nMaxNAbs = nValue
                                nCountN = clsAngleDiffResult.Count
                            End If
                            nValue = Abs(clsAngleDiffResult.ResultNEU.Y)
                            If nValue > nMaxEAbs Then
                                nMaxE = clsAngleDiffResult.ResultNEU.Y
                                nMaxEAbs = nValue
                                nCountE = clsAngleDiffResult.Count
                            End If
                            nValue = Abs(clsAngleDiffResult.ResultNEU.Z)
                            If nValue > nMaxUAbs Then
                                nMaxU = clsAngleDiffResult.ResultNEU.Z
                                nMaxUAbs = nValue
                                nCountU = clsAngleDiffResult.Count
                            End If
                        End If
                    Next
                    '出力するものがなければスキップする。
                    If bValid Then
                        '最大閉合差N。
                        Print #nFile, Format$(nDivn(j) + 0, "0") & "=" & Format$(nMaxN, "0.0########################")
                        '最大閉合差E。
                        Print #nFile, Format$(nDivn(j) + 1, "0") & "=" & Format$(nMaxE, "0.0########################")
                        '最大閉合差U。
                        Print #nFile, Format$(nDivn(j) + 2, "0") & "=" & Format$(nMaxU, "0.0########################")
                        '最大閉合差Nの辺数。
                        Print #nFile, Format$(nDivn(j) + 3, "0") & "=" & Format$(nCountN, "0")
                        '最大閉合差Eの辺数。
                        Print #nFile, Format$(nDivn(j) + 4, "0") & "=" & Format$(nCountE, "0")
                        '最大閉合差Uの辺数。
                        Print #nFile, Format$(nDivn(j) + 5, "0") & "=" & Format$(nCountU, "0")
                    End If
                End If
            Next
    
            '点検計算結果データ終了。
            Print #nFile, "499="
    
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '点検計算結果データセクションの出力。'2016/01/12 Hitz H.Nakamura
        '
        '引き数：
        'nFile ファイル番号。
        */
        public void OutputWriteCheckResult(int nFile)
        {
            return;
        }
        //==========================================================================================

    }
}
