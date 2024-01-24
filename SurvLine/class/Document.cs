using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using System.Windows.Forms;
using SurvLine.mdl;
using System.IO;
using static System.Net.WebRequestMethods;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using System.Runtime.Remoting;
using static System.Windows.Forms.AxHost;
using static SurvLine.mdl.MdlNSDefine;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Data.SqlTypes;

namespace SurvLine
{
    public class Document
    {

        //'*******************************************************************************
        //'ドキュメント
        //
        //Option Explicit
        //
        //'イベント
        //
        //'結合距離違反イベント。
        //'
        //'結合する観測点間の距離が制限を超えている場合に発生する。
        //'
        //'引き数：
        //'clsObservationPoint 違反の対象となる観測点。
        //'nDistance 観測点間の距離(ｍ)。
        //'nMax 距離の制限値(㎝)。
        //'nStyle メッセージスタイル。0=メッセージ無し。1=OKボタンのみ。2=OKボタンとすべてOKボタン。
        //'nResult メッセージボックスの結果が設定される。
        //Event CombinedDistanceViolation(ByVal clsObservationPoint As ObservationPoint, ByVal nDistance As Double, ByVal nMax As Long, ByVal nStyle As Long, ByRef nResult As Long)
        //
        //'上書き通知イベント。
        //'
        //'同じ観測点番号、セッション名の観測点をインポートしたときに、上書きを確認するために発生する。
        //'
        //'引き数：
        //'clsObservationPoint 対象となる観測点。
        //'nStyle メッセージスタイル。0=メッセージ無し。1=OKボタンのみ。2=OKボタンとすべてOKボタン。
        //'nResult メッセージボックスの結果が設定される。
        //Event OverwriteNotification(ByVal clsObservationPoint As ObservationPoint, ByVal nStyle As Long, ByRef nResult As Long)
        //
        //'読み込みファイルバージョン拒否イベント。
        //'
        //'Dataファイルのバージョンが読み込みできないバージョンの時に発生する。
        //'
        //'引き数：
        //'sPath Dataファイルのパス。
        //'nVersion ファイルバージョン。
        //Event RejectedFileVersion(ByVal sPath As String, ByVal nVersion As Long)
        //
        //'定数
        private const string TEMP_FILE_NAME = "Document.tmp";   //'保存処理用のテンポラリファイルの名前。          //24/01/04 K.setoguchi@N
        //
        //'恒久プロパティ
        public bool PageNumberVisible;          // As Boolean '帳票ページ番号表示フラグ。True=表示。False=非表示。
        public long NumberOfMinSV;              // As Long '最少衛星数。0の場合は観測データから取得。1～12が固定値。
        public MdlNSDefine.ELLIPSE_MODEL EllipseModel;  // As ELLIPSE_MODEL '記簿に出力する楕円体モデル。
        public long SameTimeMin;                // As Long '最小同時観測時間(秒)。
        public long SatelliteCountMin;          // As Long '最少共通衛星数。
        public string GeoidoPathDef;            // As String 'ジオイドモデルのパスのデフォルト。
        public string SemiDynaPathDef;          // As String 'セミ・ダイナミックパラメータファイルのパスのデフォルト。'2009/11 H.Nakamura
        public long DefLeapSec;                 // As Long 'デフォルトの閏秒。
        //  public TIME_ZONE TimeZone;      // As TIME_ZONE  '時間帯。
        //
        //'2008/10/13 NGS Yamada
        //'ユーザデータを管理するフォルダのパスを追加
        public string UserDataPath;             // As String
        //
        public long ImportComPort;              // As Long '受信機を接続するCOMポート。2007/6/20 NGS Yamada
        public bool ImportComPortType;          // As Boolean '受信機を接続するCOMポートの取得方法(True=自動取得、False=手動選択)。2007/7/2 NGS Yamada
        public bool ImportDataSave;             // As Boolean    '受信機からインポート時に観測データを保存する（True=保存する、False=保存しない）2007/8/10 NGS Yamada
        //
        public long PlotPointLabel;             // As Long 'プロット画面に表示するラベルの種類。0=観測点No,1=観測点名称,2=観測点No(観測点名称)。 2006/10/6 NGS Yamada
        public bool DisableVectorVisible;       // As Boolean '「無効」なベクトルをプロット画面に表示するか？。True=表示,False=非表示。 2006/10/10 NGS Yamada
        //
        //'インプリメンテーション
        public bool m_bEmpty;                   // As Boolean '空フラグ。True=空。False=空でない。
        public string m_sPath;                  // As String '保存ファイルのパス。
        public bool m_bModifyed;                // As Boolean '更新フラグ。True=更新あり。False=更新なし。
        //
        public string m_sJobName;               // As String '現場名。
        public string m_sDistrictName;          // As String '地区名。
        public string m_sSupervisor;            // As String 'プログラム管理者。
        public long m_nCoordNum;                // As Long   '座標系番号(1～19)。
        public bool m_bGeoidoEnable;            // As Boolean 'ジオイド有効/無効。True=有効。False=無効。
        public string m_sGeoidoPath;            // As String 'ジオイドモデルのパス。
        public bool m_bTkyEnable;               // As Boolean '旧日本測地系有効/無効。True=有効。False=無効。
        public string m_sTkyPath;               // As String '旧日本測地系パラメータファイルのパス。
        public bool m_bSemiDynaEnable;          // As Boolean 'セミ・ダイナミック有効/無効。True=有効。False=無効。'2009/11 H.Nakamura
        public string m_sSemiDynaPath;          // As String 'セミ・ダイナミックパラメータファイルのパス。'2009/11 H.Nakamura

        //private WithEvents m_clsNetworkModel As NetworkModel //'ネットワークモデル。
        private NetworkModel m_clsNetworkModel;                     // As NetworkModel 'ネットワークモデル。
        private BaseLineAnalysisParam m_clsBaseLineAnalysisParam;   // As  '基線解析パラメータ。
        private AngleDiffParam m_clsAngleDiffParamRing;             // As AngleDiffParam         '環閉合差パラメータ。
        private AngleDiffParam m_clsAngleDiffParamBetween;          // As AngleDiffParam '電子基準点間の閉合差パラメータ。
        private AngleDiffParam m_clsAngleDiffParamHeight;           // As AngleDiffParam '楕円体高の閉合差パラメータ。'2023/05/19 Hitz H.Nakamura 楕円体高の閉合差を追加。
        private AccountMaker m_clsAccountMaker;                     // As AccountMaker '帳票作成。
        private AccountParam m_clsAccountParamHand;                 // As AccountParam '観測手簿パラメータ。
        private AccountParam m_clsAccountParamWrite;                // As AccountParam '観測記簿パラメータ。
        private AccountParam m_clsAccountParamCoordinate;           // As AccountParam '座標計算簿パラメータ。
        private AccountOverlapParam m_clsAccountOverlapParam;       // As AccountOverlapParam //'点検計算簿(重複基線)パラメータ。
        private AccountParam m_clsAccountParamEccentricCorrect;     // As AccountParam '偏心計算簿パラメータ。
        private AccountParam m_clsAccountParamSemiDyna;             // As AccountParam 'セミ・ダイナミック補正表パラメータ。'2009/11 H.Nakamura
        private AccountCadastralParam m_clsAccountCadastralParam;   // As AccountCadastralParam '地籍図根三角測量精度管理表パラメータ。
        private AccountParam m_clsAccountParamResultBase;           // As AccountParam '座標一覧表パラメータ。2007/7/18 NGS Yamada
        //---------------------------------------------------------
        private List<OutputParam> m_clsOutputParam;                 //private OutputParam m_clsOutputParam[OUTPUT_TYPE_COUNT - 1];    // As OutputParam '外部出力ファイル出力パラメータ。
        //---------------------------------------------------------
        private List<DXFParam> m_clsDXFParam;                       //Private m_clsDXFParam(DXF_TYPE_COUNT - 1) As DXFParam 'DXFファイル出力パラメータ。
        //---------------------------------------------------------
        private AutoOrderVectorParam m_clsAutoOrderVectorParam;     // As AutoOrderVectorParam '基線ベクトルの向きの自動整列パラメータ。
        //'*******************************************************************************



        MdlUtility mdiUtility = new MdlUtility();

        IniFileControl iniFileControl = new IniFileControl();


        //'*******************************************************************************
        /// <summary>
        /// 空であるかどうか。
        /// </summary>
        /// <returns></returns>
        public bool IsEmpty()
        {
            bool IsEmpty = m_bEmpty;
            return IsEmpty;
        }
        //'空であるかどうか。
        //Property Get IsEmpty() As Boolean
        //    IsEmpty = m_bEmpty
        //End Property
        //'*******************************************************************************

        //'*******************************************************************************
        /// <summary>
        /// 現場名。
        /// </summary>
        /// <returns></returns>
        public string JobName()
        {
            string JobName = m_sJobName;
            return JobName;
        }
        //'現場名。
        //Property Get JobName() As String
        //    JobName = m_sJobName
        //End Property
        //'*******************************************************************************

        //'*******************************************************************************
        /// <summary>
        /// 地区名。
        /// </summary>
        /// <returns></returns>
        public string DistrictName()
        {
            string DistrictName = m_sDistrictName;
            return DistrictName;
        }
        //'地区名。
        //Property Get DistrictName() As String
        //    DistrictName = m_sDistrictName
        //End Property
        //'*******************************************************************************

        //'*******************************************************************************
        /// <summary>
        /// プログラム管理者。
        /// </summary>
        /// <returns></returns>
        public string Supervisor()
        {
            string Supervisor = m_sSupervisor;
            return Supervisor;
        }
        //'プログラム管理者。
        //Property Get Supervisor() As String
        //    Supervisor = m_sSupervisor
        //End Property
        //'*******************************************************************************
        //'*******************************************************************************


        //'*******************************************************************************
        //'*******************************************************************************
        /// <summary>
        /// フォルダ名。
        /// </summary>
        /// <param name="Path"></param>
        /// <returns></returns>
        public string Folder()
        {
            string Folder = "";

            string sDrive = "";
            string sDir = "";
            string sTitle = "";
            string sExt = "";

            mdiUtility.SplitPath(mdiUtility.RTrimEx(Path(), "\\"), ref sDrive, ref sDir, ref sTitle, ref sExt);

            Folder = sTitle;
            return Folder;
        }
        //--------------------------------------------------------------------------------
        //'フォルダ名。
        //Property Get Folder() As String
        //    Dim sDrive As String
        //    Dim sDir As String
        //    Dim sTitle As String
        //    Dim sExt As String
        //    Call SplitPath(RTrimEx(Path, "\"), sDrive, sDir, sTitle, sExt)
        //    Folder = sTitle
        //End Property
        //'*******************************************************************************
        //'*******************************************************************************

        //'*******************************************************************************
        //'*******************************************************************************
        /// <summary>
        //  座標系番号(1～19)。
        /// </summary>
        /// <param name="nCoordNum"></param>
        public void CoordNum(long nCoordNum)
        {
            m_nCoordNum = nCoordNum;
            m_bModifyed = true;
        }
        //--------------------------------------------------------------------------------
        //'座標系番号(1～19)。
        //Property Let CoordNum(ByVal nCoordNum As Long)
        //    m_nCoordNum = nCoordNum
        //    m_bModifyed = True
        //End Property
        //'*******************************************************************************


        //'*******************************************************************************
        //'*******************************************************************************
        /// <summary>
        /// 座標系番号(1～19)。
        /// </summary>
        /// <returns></returns>
        public long CoordNum()
        {
            long CoordNum = m_nCoordNum;
            return CoordNum;
        }
        //--------------------------------------------------------------------------------
        //'座標系番号(1～19)。
        //Property Get CoordNum() As Long
        //    CoordNum = m_nCoordNum
        //End Property
        //'*******************************************************************************

        //'*******************************************************************************
        //'*******************************************************************************
        /// <summary>
        /// ジオイド有効/無効。
        /// </summary>
        /// <param name="bGeoidoEnable"></param>
        public void GeoidoEnable(bool bGeoidoEnable)
        {
            m_bGeoidoEnable = bGeoidoEnable;
            m_bModifyed = true;
        }
        //--------------------------------------------------------------------------------
        //'ジオイド有効/無効。
        //Property Let GeoidoEn;;able(ByVal bGeoidoEnable As Boolean)
        //    m_bGeoidoEnable = bGeoidoEnable
        //    m_bModifyed = True
        //End Property
        //'*******************************************************************************

        //'*******************************************************************************
        //'*******************************************************************************
        /// <summary>
        /// ジオイド有効/無効。
        /// </summary>
        /// <returns></returns>
        public bool GeoidoEnable()
        {
            bool GeoidoEnable = m_bGeoidoEnable;
            return GeoidoEnable;
        }
        //--------------------------------------------------------------------------------
        //'ジオイド有効/無効。
        //Property Get GeoidoEnable() As Boolean
        //    GeoidoEnable = m_bGeoidoEnable
        //End Property
        //'*******************************************************************************

        //'*******************************************************************************
        //'*******************************************************************************
        /// <summary>
        /// ジオイドモデルのパス。
        /// </summary>
        /// <param name="GeoidoPath"></param>
        public void GeoidoPath(string GeoidoPath)
        {
            m_sGeoidoPath = GeoidoPath;
            m_bModifyed = true;
        }
        //--------------------------------------------------------------------------------
        //'ジオイドモデルのパス。
        //Property Let GeoidoPath(ByVal GeoidoPath As String)
        //    m_sGeoidoPath = GeoidoPath
        //    m_bModifyed = True
        //End Property
        //'*******************************************************************************

        //'*******************************************************************************
        //'*******************************************************************************
        /// <summary>
        /// ジオイドモデルのパス。
        /// </summary>
        /// <returns></returns>
        public string GeoidoPath()
        {
            string GeoidoPath = m_sGeoidoPath;
            return GeoidoPath;
        }
        //--------------------------------------------------------------------------------
        //    GeoidoPath = m_sGeoidoPath
        //End Property
        //'*******************************************************************************

        //'*******************************************************************************
        //'*******************************************************************************
        /// <summary>
        /// 2009/11 H.Nakamura
        /// セミ・ダイナミック有効/無効。
        /// </summary>
        /// <param name="bSemiDynaEnable"></param>
        public void SemiDynaEnable(bool bSemiDynaEnable)
        {
            m_bSemiDynaEnable = bSemiDynaEnable;
            m_bModifyed = true;
        }
        //--------------------------------------------------------------------------------
        //'2009/11 H.Nakamura
        //'セミ・ダイナミック有効/無効。
        //Property Let SemiDynaEnable(ByVal bSemiDynaEnable As Boolean)
        //    m_bSemiDynaEnable = bSemiDynaEnable
        //    m_bModifyed = True
        //End Property
        //'*******************************************************************************

        //'*******************************************************************************
        //'*******************************************************************************
        /// <summary>
        /// 2009/11 H.Nakamura
        /// セミ・ダイナミック有効/無効。
        /// </summary>
        /// <returns></returns>
        public bool SemiDynaEnable()
        {
            bool SemiDynaEnable = m_bSemiDynaEnable;
            return SemiDynaEnable;
        }
        //--------------------------------------------------------------------------------
        //'2009/11 H.Nakamura
        //'セミ・ダイナミック有効/無効。
        //Property Get SemiDynaEnable() As Boolean
        //    SemiDynaEnable = m_bSemiDynaEnable
        //End Property
        //'*******************************************************************************

        //'*******************************************************************************
        //'*******************************************************************************
        /// <summary>
        /// 2009/11 H.Nakamura
        /// セミ・ダイナミックパラメータファイルのパス。
        /// </summary>
        /// <param name="SemiDynaPath"></param>
        public void SemiDynaPath(string SemiDynaPath)
        {
            m_sSemiDynaPath = SemiDynaPath;
            m_bModifyed = true;
        }
        //--------------------------------------------------------------------------------
        //'2009/11 H.Nakamura
        //'セミ・ダイナミックパラメータファイルのパス。
        //Property Let SemiDynaPath(ByVal SemiDynaPath As String)
        //    m_sSemiDynaPath = SemiDynaPath
        //    m_bModifyed = True
        //End Property
        //'*******************************************************************************


        //'*******************************************************************************
        //'*******************************************************************************
        /// <summary>
        /// 2009/11 H.Nakamura
        /// セミ・ダイナミックパラメータファイルのパス。
        /// </summary>
        /// <returns></returns>
        public string SemiDynaPath()
        {
            string SemiDynaPath = m_sSemiDynaPath;
            return SemiDynaPath;
        }
        //--------------------------------------------------------------------------------
        //'2009/11 H.Nakamura
        //'セミ・ダイナミックパラメータファイルのパス。
        //Property Get SemiDynaPath() As String
        //    SemiDynaPath = m_sSemiDynaPath
        //End Property
        //'*******************************************************************************

        //'*******************************************************************************
        //'*******************************************************************************
        /// <summary>
        /// 保存ファイルのパス。
        /// </summary>
        /// <returns></returns>
        public string Path()
        {
            string Path = m_sPath;
            return Path;
        }
        //'保存ファイルのパス。
        //Property Get Path() As String
        //    Path = m_sPath
        //End Property
        //'*******************************************************************************

        //'*******************************************************************************
        //'*******************************************************************************
        /// <summary>
        /// 更新フラグ。
        /// </summary>
        /// <returns></returns>
        public bool Modifyed()
        {
            bool Modifyed = m_bModifyed;
            return Modifyed;
        }
        //'更新フラグ。
        //Property Get Modifyed() As Boolean
        //    Modifyed = m_bModifyed
        //End Property
        //'*******************************************************************************


        //'*******************************************************************************
        //'*******************************************************************************
        /// <summary>
        /// ネットワークモデル。
        /// </summary>
        /// <returns></returns>
        public NetworkModel NetworkModel()
        {
            NetworkModel NetworkModel = m_clsNetworkModel;
            return NetworkModel;
        }
        //'ネットワークモデル。
        //Property Get NetworkModel() As NetworkModel
        //    Set NetworkModel = m_clsNetworkModel
        //End Property
        //'*******************************************************************************


        //'*******************************************************************************
        //'*******************************************************************************
        /// <summary>
        /// 基線解析パラメータ。
        /// </summary>
        /// <returns></returns>
        private BaseLineAnalysisParam BaseLineAnalysisParam()
        {
            BaseLineAnalysisParam BaseLineAnalysisParam = m_clsBaseLineAnalysisParam;
            return BaseLineAnalysisParam;
        }
        //'基線解析パラメータ。
        //Property Get BaseLineAnalysisParam() As BaseLineAnalysisParam
        //    Set BaseLineAnalysisParam = m_clsBaseLineAnalysisParam
        //End Property
        //'*******************************************************************************


        //'*******************************************************************************
        //'*******************************************************************************
        /// <summary>
        /// 閉合差パラメータの数。'2016/01/07 Hitz H.Nakamura
        /// </summary>
        /// <returns></returns>
        public int AngleDiffParamCount()
        {
            int AngleDiffParamCount = 2;
            return AngleDiffParamCount;
        }
        //'閉合差パラメータの数。'2016/01/07 Hitz H.Nakamura
        //Property Get AngleDiffParamCount() As Integer
        //    AngleDiffParamCount = 2
        //End Property
        //'*******************************************************************************


        //'*******************************************************************************
        //'*******************************************************************************
        /// <summary>
        /// 閉合差パラメータの数。'2016/01/07 Hitz H.Nakamura
        /// 
        /// </summary>
        /// <param name="Index"></param>
        /// <returns></returns>
        private AngleDiffParam AngleDiffParam(int Index)
        {
            AngleDiffParam AngleDiffParam;
            if (Index <= 0)
            {
                AngleDiffParam = m_clsAngleDiffParamRing;
                //'2023/05/19 Hitz H.Nakamura **************************************************
                //'楕円体高の閉合差を追加。
            }
            else if (Index == 1)
            {
                AngleDiffParam = m_clsAngleDiffParamBetween;
            }
            else
            {
                AngleDiffParam = m_clsAngleDiffParamHeight;
            }
            return AngleDiffParam;

        }
        //--------------------------------------------------------------------------------------
        //'閉合差パラメータの数。'2016/01/07 Hitz H.Nakamura
        //Property Get AngleDiffParam(ByVal Index As Integer) As AngleDiffParam
        //    If Index <= 0 Then
        //        Set AngleDiffParam = m_clsAngleDiffParamRing
        //    '2023/05/19 Hitz H.Nakamura **************************************************
        //    '楕円体高の閉合差を追加。
        //    'Else
        //    '    Set AngleDiffParam = m_clsAngleDiffParamBetween
        //    'End If
        //    '*****************************************************************************
        //    ElseIf Index = 1 Then
        //        Set AngleDiffParam = m_clsAngleDiffParamBetween
        //    Else
        //        Set AngleDiffParam = m_clsAngleDiffParamHeight
        //    End If
        //    '*****************************************************************************
        //End Property
        //'*******************************************************************************

        //'*******************************************************************************
        //'*******************************************************************************
        /// <summary>
        /// 環閉合差パラメータ。
        /// 
        /// </summary>
        /// <param name="clsAngleDiffParam"></param>
        private void AngleDiffParamRing(AngleDiffParam clsAngleDiffParam)
        {
            if (m_clsAngleDiffParamRing.Compare(clsAngleDiffParam)) { return; }
            m_clsAngleDiffParamRing = clsAngleDiffParam;
        }
        //'環閉合差パラメータ。
        //Property Let AngleDiffParamRing(ByVal clsAngleDiffParam As AngleDiffParam)
        //    If m_clsAngleDiffParamRing.Compare(clsAngleDiffParam) Then Exit Property
        //    Let m_clsAngleDiffParamRing = clsAngleDiffParam
        //    m_bModifyed = True
        //End Property
        //'*******************************************************************************


        //'*******************************************************************************
        //'*******************************************************************************
        /// <summary>
        /// 環閉合差パラメータ。
        /// 
        /// </summary>
        /// <returns></returns>
        private AngleDiffParam AngleDiffParamRing()
        {
            AngleDiffParam AngleDiffParamRing = m_clsAngleDiffParamRing;
            return AngleDiffParamRing;

        }
        //'環閉合差パラメータ。
        //Property Get AngleDiffParamRing() As AngleDiffParam
        //    Set AngleDiffParamRing = m_clsAngleDiffParamRing
        //End Property
        //'*******************************************************************************


        //'*******************************************************************************
        //'*******************************************************************************
        /// <summary>
        /// 電子基準点間の閉合差パラメータ。
        /// </summary>
        /// <param name="clsAngleDiffParam"></param>
        private void AngleDiffParamBetween( AngleDiffParam clsAngleDiffParam)
        {
            if (m_clsAngleDiffParamBetween.Compare(clsAngleDiffParam))
            {
                return;
            }
            m_clsAngleDiffParamBetween = clsAngleDiffParam;
            m_bModifyed = true;
        }
        //'電子基準点間の閉合差パラメータ。
        //Property Let AngleDiffParamBetween(ByVal clsAngleDiffParam As AngleDiffParam)
        //    If m_clsAngleDiffParamBetween.Compare(clsAngleDiffParam) Then Exit Property
        //    Let m_clsAngleDiffParamBetween = clsAngleDiffParam
        //    m_bModifyed = True
        //End Property
        //'*******************************************************************************


        //'*******************************************************************************
        //'*******************************************************************************
        /// <summary>
        /// 電子基準点間の閉合差パラメータ。
        /// </summary>
        /// <returns></returns>
        private AngleDiffParam AngleDiffParamBetween()
        {
            AngleDiffParam AngleDiffParamBetween = m_clsAngleDiffParamBetween;
            return AngleDiffParamBetween;
        }
        //-------------------------------------------------------------------------------
        //'電子基準点間の閉合差パラメータ。
        //Property Get AngleDiffParamBetween() As AngleDiffParam
        //    Set AngleDiffParamBetween = m_clsAngleDiffParamBetween
        //End Property
        //'*******************************************************************************


        //'*******************************************************************************
        //'*******************************************************************************
        /// <summary>
        /// '2023/05/19 Hitz H.Nakamura **************************************************
        /// '楕円体高の閉合差を追加。
        /// '楕円体高の閉合差パラメータ。
        /// 
        /// </summary>
        /// <param name="clsAngleDiffParam"></param>
        private void AngleDiffParamHeight(AngleDiffParam clsAngleDiffParam)
        {
            if (m_clsAngleDiffParamHeight.Compare(clsAngleDiffParam))
            {
                return;
            }
            m_clsAngleDiffParamHeight = clsAngleDiffParam;
            m_bModifyed = true;
        }
        //--------------------------------------------------------------------------------
        //'2023/05/19 Hitz H.Nakamura **************************************************
        //'楕円体高の閉合差を追加。
        //'楕円体高の閉合差パラメータ。
        //Property Let AngleDiffParamHeight(ByVal clsAngleDiffParam As AngleDiffParam)
        //    If m_clsAngleDiffParamHeight.Compare(clsAngleDiffParam) Then Exit Property
        //    Let m_clsAngleDiffParamHeight = clsAngleDiffParam
        //    m_bModifyed = True
        //End Property
        //'*******************************************************************************


        //'*******************************************************************************
        //'*******************************************************************************
        /// <summary>
        /// 楕円体高の閉合差パラメータ。
        /// 
        /// </summary>
        /// <returns></returns>
        private AngleDiffParam AngleDiffParamHeight()
        {
            AngleDiffParam AngleDiffParamHeight = m_clsAngleDiffParamHeight;
            return AngleDiffParamHeight;
        }
        //--------------------------------------------------------------------------------
        //'楕円体高の閉合差パラメータ。
        //Property Get AngleDiffParamHeight() As AngleDiffParam
        //    Set AngleDiffParamHeight = m_clsAngleDiffParamHeight
        //End Property
        //'*****************************************************************************


        //'*****************************************************************************
        //'*****************************************************************************
        /// <summary>
        /// 観測手簿パラメータ。
        /// 
        /// </summary>
        /// <returns></returns>
        private AccountParam AccountParamHand()
        {
            AccountParam AccountParamHand = m_clsAccountParamHand;
            return AccountParamHand;
        }
        //--------------------------------------------------------------------------------
        //'観測手簿パラメータ。
        //Property Get AccountParamHand() As AccountParam
        //    Set AccountParamHand = m_clsAccountParamHand
        //End Property
        //'*****************************************************************************

        //'*****************************************************************************
        //'*****************************************************************************
        /// <summary>
        /// '観測記簿パラメータ。
        /// 
        /// </summary>
        /// <returns></returns>
        private AccountParam AccountParamWrite()
        {
            AccountParam AccountParamHand = m_clsAccountParamWrite;
            return AccountParamHand;
        }
        //--------------------------------------------------------------------------------
        //'観測記簿パラメータ。
        //Property Get AccountParamWrite() As AccountParam
        //    Set AccountParamWrite = m_clsAccountParamWrite
        //End Property
        //'*****************************************************************************


        //'*****************************************************************************
        //'*****************************************************************************
        /// <summary>
        /// 座標計算簿パラメータ。
        /// 
        /// </summary>
        /// <returns></returns>
        private AccountParam AccountParamCoordinate()
        {
            AccountParam AccountParamCoordinate = m_clsAccountParamCoordinate;
            return AccountParamCoordinate;
        }
        //--------------------------------------------------------------------------------
        //'座標計算簿パラメータ。
        //Property Get AccountParamCoordinate() As AccountParam
        //    Set AccountParamCoordinate = m_clsAccountParamCoordinate
        //End Property
        //'*****************************************************************************


        //'*****************************************************************************
        //'*****************************************************************************
        /// <summary>
        /// 点検計算簿(重複基線)パラメータ。
        /// 
        /// </summary>
        /// <returns></returns>
        private AccountOverlapParam AccountOverlapParam()
        {
            AccountOverlapParam AccountOverlapParam = m_clsAccountOverlapParam;
            return AccountOverlapParam;
        }
        //--------------------------------------------------------------------------------
        //'点検計算簿(重複基線)パラメータ。
        //Property Get AccountOverlapParam() As AccountOverlapParam
        //    Set AccountOverlapParam = m_clsAccountOverlapParam
        //End Property
        //'*****************************************************************************

        //'*****************************************************************************
        //'*****************************************************************************
        /// <summary>
        /// 偏心計算簿パラメータ。
        /// </summary>
        /// <returns></returns>
        private AccountParam AccountParamEccentricCorrect()
        {
            AccountParam AccountParamEccentricCorrect = m_clsAccountParamEccentricCorrect;
            return AccountParamEccentricCorrect;
        }
        //--------------------------------------------------------------------------------
        //'偏心計算簿パラメータ。
        //Property Get AccountParamEccentricCorrect() As AccountParam
        //    Set AccountParamEccentricCorrect = m_clsAccountParamEccentricCorrect
        //End Property
        //'*****************************************************************************

        //'*****************************************************************************
        //'*****************************************************************************
        /// <summary>
        ///  '2009/11 H.Nakamura
        ///  'セミ・ダイナミック補正表パラメータ。
        /// 
        /// </summary>
        /// <returns></returns>
        private AccountParam AccountParamSemiDyna()
        {
            AccountParam AccountParamSemiDyna = m_clsAccountParamSemiDyna;
            return AccountParamSemiDyna;
        }
        //--------------------------------------------------------------------------------
        //'2009/11 H.Nakamura
        //'セミ・ダイナミック補正表パラメータ。
        //Property Get AccountParamSemiDyna() As AccountParam
        //    Set AccountParamSemiDyna = m_clsAccountParamSemiDyna
        //End Property
        //'*****************************************************************************


        //'*****************************************************************************
        //'*****************************************************************************
        /// <summary>
        /// '地籍図根三角測量精度管理表パラメータ。
        /// 
        /// </summary>
        /// <returns></returns>
        private AccountCadastralParam AccountCadastralParam()
        {
            AccountCadastralParam AccountCadastralParam = m_clsAccountCadastralParam;
            return AccountCadastralParam;
        }
        //--------------------------------------------------------------------------------
        //'地籍図根三角測量精度管理表パラメータ。
        //Property Get AccountCadastralParam() As AccountCadastralParam
        //    Set AccountCadastralParam = m_clsAccountCadastralParam
        ///End Property
        //'*****************************************************************************

        //'*****************************************************************************
        //'*****************************************************************************
        /// <summary>
        /// '外部出力ファイル出力パラメータ。
        /// 
        /// </summary>
        /// <returns></returns>
        private OutputParam OutputParam(long nIndex)
        {
            OutputParam OutputParam = m_clsOutputParam[(int)nIndex];
            return OutputParam;
        }
        //--------------------------------------------------------------------------------
        //'外部出力ファイル出力パラメータ。
        //Property Get OutputParam(ByVal nIndex As Long) As OutputParam
        //    Set OutputParam = m_clsOutputParam(nIndex)
        //End Property
        //'*****************************************************************************


        //'*****************************************************************************
        //'*****************************************************************************
        /// <summary>
        /// 'DXFファイル出力パラメータ。
        /// 
        /// </summary>
        /// <param name="nIndex"></param>
        /// <returns></returns>
        private DXFParam DXFParam(long nIndex)
        {
            DXFParam DXFParam = m_clsDXFParam[(int)nIndex];
            return DXFParam;
        }
        //--------------------------------------------------------------------------------
        //'DXFファイル出力パラメータ。
        //Property Get DXFParam(ByVal nIndex As Long) As DXFParam
        //    Set DXFParam = m_clsDXFParam(nIndex)
        //End Property
        //'*****************************************************************************


        //'*****************************************************************************
        //'*****************************************************************************
        /// <summary>
        /// '基線ベクトルの向きの自動整列パラメータ。
        /// 
        /// </summary>
        /// <returns></returns>
        private AutoOrderVectorParam AutoOrderVectorParam()
        {
            AutoOrderVectorParam AutoOrderVectorParam = m_clsAutoOrderVectorParam;
            return AutoOrderVectorParam;
        }
        //--------------------------------------------------------------------------------
        //'基線ベクトルの向きの自動整列パラメータ。
        //Property Get AutoOrderVectorParam() As AutoOrderVectorParam
        //    Set AutoOrderVectorParam = m_clsAutoOrderVectorParam
        //End Property
        //'*****************************************************************************

        //'*****************************************************************************
        //'*****************************************************************************
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
        //--------------------------------------------------------------------------------
        //'座標一覧表パラメータ。 2007/7/18 NGS Yamada
        //Property Get AccountParamResultBase() As AccountParam
        //    Set AccountParamResultBase = m_clsAccountParamResultBase
        //End Property
        //'*****************************************************************************


        //***************************************************************************
        /// <summary>
        /// bool型データ読み出し VB対応
        /// </summary>
        /// <param name="br"></param>
        /// <returns></returns>
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





        //'*******************************************************************************
        //'*******************************************************************************
        /// <summary>
        /// イベント
        //
        /// 初期化
        /// </summary>
        public void Class_Initialize()
        {
            string App_Path = @"C:\Develop\NetSurv\Src\NS-App\NS-Survey";
            string App_Title = "NS-Survey";

            //------------------------------------------------------------------
            //[VB]    PageNumberVisible = GetPrivateProfileInt(PROFILE_SAVE_SEC_ACCOUNT, PROFILE_SAVE_KEY_PAGENUMBERVISIBLE, 1, App.Path & "\" & App.Title & PROFILE_SAVE_EXT) <> 0
            if (iniFileControl.GetPrivateProfileInt(MdlNSDefine.PROFILE_SAVE_SEC_ACCOUNT, MdlNSDefine.PROFILE_SAVE_KEY_COORDNUM, 9, $"{App_Path}\\{App_Title}{MdlNSSDefine.PROFILE_SAVE_EXT}") != 0){
                PageNumberVisible = true;
            }
            else
            {
                PageNumberVisible = false;
            }
            //------------------------------------------------------------------
            NumberOfMinSV = (long)iniFileControl.GetPrivateProfileInt(MdlNSDefine.PROFILE_SAVE_SEC_ACCOUNT, MdlNSSDefine.PROFILE_SAVE_KEY_NUMBEROFMINSV, 0, $"{App_Path}\\{App_Title}{MdlNSSDefine.PROFILE_SAVE_EXT}");
            EllipseModel = (MdlNSDefine.ELLIPSE_MODEL)iniFileControl.GetPrivateProfileInt(MdlNSDefine.PROFILE_SAVE_SEC_ACCOUNT, MdlNSDefine.PROFILE_SAVE_KEY_ELLIPSEMODEL, (int)MdlNSDefine.ELLIPSE_MODEL.ELLIPSE_MODEL_GRS80, $"{App_Path}\\{App_Title}{MdlNSSDefine.PROFILE_SAVE_EXT}");
            SameTimeMin = (long)iniFileControl.GetPrivateProfileInt(MdlNSDefine.PROFILE_SAVE_SEC_BASELINE, MdlNSSDefine.PROFILE_SAVE_KEY_SAMETIMEMIN, (int)MdlNSSDefine.DEF_SAMETIMEMIN, $"{App_Path}\\{App_Title}{MdlNSSDefine.PROFILE_SAVE_EXT}");
            SatelliteCountMin = (long)iniFileControl.GetPrivateProfileInt(MdlNSDefine.PROFILE_SAVE_SEC_BASELINE, MdlNSSDefine.PROFILE_SAVE_KEY_SATELLITECOUNTMIN, (int)MdlNSSDefine.DEF_SATELLITECOUNTMIN, $"{App_Path}\\{App_Title}{MdlNSSDefine.PROFILE_SAVE_EXT}");
            GeoidoPathDef = iniFileControl.GetPrivateProfileString(MdlNSDefine.PROFILE_SAVE_SEC_GEOIDO, MdlNSDefine.PROFILE_SAVE_KEY_PATH, "", $"{App_Path}\\{App_Title}{MdlNSSDefine.PROFILE_SAVE_EXT}");
            SemiDynaPathDef = iniFileControl.GetPrivateProfileString(MdlNSDefine.PROFILE_SAVE_SEC_SEMIDYNA, MdlNSDefine.PROFILE_SAVE_KEY_PATH, "", $"{App_Path}\\{App_Title}{MdlNSSDefine.PROFILE_SAVE_EXT}"); //'セミ・ダイナミック対応。'2009/11 H.Nakamura

            DefLeapSec = (long)iniFileControl.GetPrivateProfileInt(MdlNSDefine.PROFILE_SAVE_SEC_BASELINE, MdlNSSDefine.PROFILE_SAVE_KEY_DEFLEAPSEC, (int)MdlNSDefine.DEF_LEAP_SEC, $"{App_Path}\\{App_Title}{MdlNSSDefine.PROFILE_SAVE_EXT}");
            //        TimeZone = (long)iniFileControl.GetPrivateProfileInt(MdlNSDefine..PROFILE_SAVE_SEC_BASELINE, MdlNSDefine.PROFILE_SAVE_KEY_TIMEZONE, (int)MdlNSDefine.TIME_ZONE.TIME_ZONE_UTC, $"{App_Path}\\{App_Title}{MdlNSSDefine.PROFILE_SAVE_EXT}");
            //----------------------------------------
            //'2006/10/6
            PlotPointLabel = (long)iniFileControl.GetPrivateProfileInt(MdlNSDefine.PROFILE_SAVE_SEC_PLOT, MdlNSDefine.PROFILE_SAVE_KEY_POINTLABEL, (int)MdlNSDefine.PROFILE_SAVE_DEF_POINTLABEL, $"{App_Path}\\{App_Title}{MdlNSSDefine.PROFILE_SAVE_EXT}");
            //----------------------------------------
            //'2006/10/10
            DisableVectorVisible = iniFileControl.GetPrivateProfileBool(MdlNSDefine.PROFILE_SAVE_SEC_PLOT, MdlNSDefine.PROFILE_SAVE_KEY_DISABLEVECTORVISIBLE, 1, $"{App_Path}\\{App_Title}{MdlNSSDefine.PROFILE_SAVE_EXT}");

            //----------------------------------------
            //'2007/6/20 NGS Yamada
            ImportComPort = (long)iniFileControl.GetPrivateProfileInt(MdlNSDefine.PROFILE_SAVE_SEC_BASELINE, MdlNSDefine.PROFILE_SAVE_KEY_IMPORTCOMPORT, (int)MdlNSDefine.PROFILE_SAVE_DEF_IMPORTCOMPORT, $"{App_Path}\\{App_Title}{MdlNSSDefine.PROFILE_SAVE_EXT}");

            //    '2007/7/2 NGS Yamada
            ImportComPortType = iniFileControl.GetPrivateProfileBool(MdlNSDefine.PROFILE_SAVE_SEC_BASELINE, MdlNSDefine.PROFILE_SAVE_KEY_IMPORTCOMPORTTYPE, 1, $"{App_Path}\\{App_Title}{MdlNSSDefine.PROFILE_SAVE_EXT}");


            //----------------------------------------
            //'2007/8/10 NGS Yamada
            ImportDataSave = iniFileControl.GetPrivateProfileBool(MdlNSDefine.PROFILE_SAVE_SEC_BASELINE, MdlNSDefine.PROFILE_SAVE_KEY_IMPORTDATASAVE, 1, $"{App_Path}\\{App_Title}{MdlNSSDefine.PROFILE_SAVE_EXT}");


            //'2008/10/13 NGS Yamada
            UserDataPath = iniFileControl.GetPrivateProfileString(MdlNSDefine.PROFILE_SAVE_SEC_DETAIL, MdlNSDefine.PROFILE_SAVE_KEY_USERDATAPATH, $"{App_Path}{MdlNSSDefine.DATA_FOLDER_NAME}", $"{App_Path}\\{App_Title}{MdlNSSDefine.PROFILE_SAVE_EXT}");

        }
        //'*******************************************************************************
        //'イベント
        //
        //'初期化
        //Private Sub Class_Initialize()
        //
        //    On Error GoTo ErrorHandler
        //
        //
        //    Call Clear
        //
        //
        //    PageNumberVisible = GetPrivateProfileInt(PROFILE_SAVE_SEC_ACCOUNT, PROFILE_SAVE_KEY_PAGENUMBERVISIBLE, 1, App.Path & "\" & App.Title & PROFILE_SAVE_EXT) <> 0
        //    NumberOfMinSV = GetPrivateProfileInt(PROFILE_SAVE_SEC_ACCOUNT, PROFILE_SAVE_KEY_NUMBEROFMINSV, 0, App.Path & "\" & App.Title & PROFILE_SAVE_EXT)
        //    EllipseModel = GetPrivateProfileInt(PROFILE_SAVE_SEC_ACCOUNT, PROFILE_SAVE_KEY_ELLIPSEMODEL, ELLIPSE_MODEL_GRS80, App.Path & "\" & App.Title & PROFILE_SAVE_EXT)
        //    SameTimeMin = GetPrivateProfileInt(PROFILE_SAVE_SEC_BASELINE, PROFILE_SAVE_KEY_SAMETIMEMIN, DEF_SAMETIMEMIN, App.Path & "\" & App.Title & PROFILE_SAVE_EXT)
        //    SatelliteCountMin = GetPrivateProfileInt(PROFILE_SAVE_SEC_BASELINE, PROFILE_SAVE_KEY_SATELLITECOUNTMIN, DEF_SATELLITECOUNTMIN, App.Path & "\" & App.Title & PROFILE_SAVE_EXT)
        //    GeoidoPathDef = GetPrivateProfileString(PROFILE_SAVE_SEC_GEOIDO, PROFILE_SAVE_KEY_PATH, "", App.Path & "\" & App.Title & PROFILE_SAVE_EXT)
        //    SemiDynaPathDef = GetPrivateProfileString(PROFILE_SAVE_SEC_SEMIDYNA, PROFILE_SAVE_KEY_PATH, "", App.Path & "\" & App.Title & PROFILE_SAVE_EXT) 'セミ・ダイナミック対応。'2009/11 H.Nakamura
        //    DefLeapSec = GetPrivateProfileInt(PROFILE_SAVE_SEC_BASELINE, PROFILE_SAVE_KEY_DEFLEAPSEC, DEF_LEAP_SEC, App.Path & "\" & App.Title & PROFILE_SAVE_EXT)
        //    TimeZone = GetPrivateProfileInt(PROFILE_SAVE_SEC_BASELINE, PROFILE_SAVE_KEY_TIMEZONE, TIME_ZONE_UTC, App.Path & "\" & App.Title & PROFILE_SAVE_EXT)
        //    '2006/10/6
        //    PlotPointLabel = GetPrivateProfileInt(PROFILE_SAVE_SEC_PLOT, PROFILE_SAVE_KEY_POINTLABEL, PROFILE_SAVE_DEF_POINTLABEL, App.Path & "\" & App.Title & PROFILE_SAVE_EXT)
        //    '2006/10/10
        //    DisableVectorVisible = GetPrivateProfileInt(PROFILE_SAVE_SEC_PLOT, PROFILE_SAVE_KEY_DISABLEVECTORVISIBLE, 1, App.Path & "\" & App.Title & PROFILE_SAVE_EXT)
        //
        //
        //    '2007/6/20 NGS Yamada
        //    ImportComPort = GetPrivateProfileInt(PROFILE_SAVE_SEC_BASELINE, PROFILE_SAVE_KEY_IMPORTCOMPORT, PROFILE_SAVE_DEF_IMPORTCOMPORT, App.Path & "\" & App.Title & PROFILE_SAVE_EXT)
        //
        //
        //    '2007/7/2 NGS Yamada
        //    ImportComPortType = GetPrivateProfileInt(PROFILE_SAVE_SEC_BASELINE, PROFILE_SAVE_KEY_IMPORTCOMPORTTYPE, 1, App.Path & "\" & App.Title & PROFILE_SAVE_EXT) <> 0
        //
        //
        //    '2007/8/10 NGS Yamada
        //    ImportDataSave = GetPrivateProfileInt(PROFILE_SAVE_SEC_BASELINE, PROFILE_SAVE_KEY_IMPORTDATASAVE, 1, App.Path & "\" & App.Title & PROFILE_SAVE_EXT) <> 0
        //
        //
        //    '2008/10/13 NGS Yamada
        //    UserDataPath = GetPrivateProfileString(PROFILE_SAVE_SEC_DETAIL, PROFILE_SAVE_KEY_USERDATAPATH, App.Path & DATA_FOLDER_NAME, App.Path & "\" & App.Title & PROFILE_SAVE_EXT)
        //
        //
        //    Exit Sub
        //
        //
        //ErrorHandler:
        //    Call mdlMain.ErrorExit
        //
        //
        //End Sub



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
            bool dmy1 = mdiUtility.DeleteDir(sDstObsPointPath, true);

            //-------------------------------------------------------------
            //[VB]  Call CopyDir(sSrcObsPointPath, sDstObsPointPath, True)
            bool dmy2 = mdiUtility.CopyDir(sSrcObsPointPath, sDstObsPointPath, true);


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
                    string sSemiDynaPath;   //'セミ・ダイナミック対応。'2009/11 H.Nakamura

                    //-------------------------------------------------------
                    //[VB]  sJobName = GetString(clsFile.Number)
                    //******************************************
                    //  現場名をグローバル領域に設定
                    //******************************************
                    sJobName = mdiUtility.FileRead_GetString(br);
                    Genba_S.sJobName = sJobName;
                    //-------------------------------------------------------
                    //[VB]  sDistrictName = GetString(clsFile.Number)
                    //******************************************
                    //  地区名をグローバル領域に設定
                    //******************************************
                    sDistrictName = mdiUtility.FileRead_GetString(br);
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
                        sSupervisor = mdiUtility.FileRead_GetString(br);
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
                    sGeoidoPath = mdiUtility.FileRead_GetString(br);   //"C:\Develop\パラメータファイル\gsigeome.ver3" 

                    //-------------------------------------------------------
                    //[VB]  Get #clsFile.Number, , bTkyEnable
                    bTkyEnable = GetFileBool(br);
                    // bTkyEnable = br.ReadBoolean();
                    Genba_S.bTkyEnable = bTkyEnable;    //false

                    //-------------------------------------------------------
                    //[VB]  sTkyPath = GetString(clsFile.Number)
                    sTkyPath = mdiUtility.FileRead_GetString(br);
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
                        bSemiDynaEnable = GetFileBool(br); ;
                        Genba_S.bSemiDynaEnable = bSemiDynaEnable;
                        //----------------------------------------
                        sSemiDynaPath = mdiUtility.FileRead_GetString(br);
                        Genba_S.sSemiDynaPath = sSemiDynaPath;   //"C:\Develop\パラメータファイル\SemiDyna2009.par"
                    }

                    //-------------------------------------------------------
                    //[VB]  Dim clsNetworkModel As New NetworkModel
                    //[VB]  Call clsNetworkModel.Load(clsFile.Number, nVersion)

                    NetworkModel networkModel = new NetworkModel();
                    //  networkModel.Load( br, nVersion, ref Genba_S);
                    networkModel.Load(br, nVersion, ref List_Genba_S);





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




                }

                fs.Close();
            }
        }
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



        //23/12/29 K.setoguchi@NV---------->>>>>>>>>>
        //**************************************************************************************
        //**************************************************************************************
        /// <summary>
        ///現場を設定する。
        ///
        ///引き数：
        /// sJobName 現場名。
        /// sDistrictName 地区名。
        /// nCoordNum 座標系番号(1～19)。
        /// bGeoidoEnable ジオイド有効/無効。True=有効。False=無効。
        /// sGeoidoPath ジオイドモデルのパス。
        /// bSemiDynaEnable セミ・ダイナミック有効/無効。True=有効。False=無効。'2009/11 H.Nakamura
        /// sSemiDynaPath セミ・ダイナミックパラメータファイルのパス。'2009/11 H.Nakamura
        /// </summary>
        /// <param name="sJobName"></param>
        /// <param name="sDistrictName"></param>
        /// <param name="nCoordNum"></param>
        /// <param name="bGeoidoEnable"></param>
        /// <param name="sGeoidoPath"></param>
        /// <param name="bSemiDynaEnable"></param>
        /// <param name="sSemiDynaPath"></param>
        /// 
        public void SetJob( string sJobName, string sDistrictName, long nCoordNum, bool bGeoidoEnable, string sGeoidoPath, bool bSemiDynaEnable, string sSemiDynaPath)
        {
            m_sJobName = sJobName;
            m_sDistrictName = sDistrictName;
            m_nCoordNum = nCoordNum;
            m_bGeoidoEnable = bGeoidoEnable;
            m_sGeoidoPath = sGeoidoPath;
            m_bSemiDynaEnable = bSemiDynaEnable;    //'セミ・ダイナミック対応。'2009/11 H.Nakamura
            m_sSemiDynaPath = sSemiDynaPath;        //'セミ・ダイナミック対応。'2009/11 H.Nakamura
            m_bEmpty = false;
            m_bModifyed = true;
        }
        //--------------------------------------------------------------------------------------
        //'現場を設定する。
        //'
        //'引き数：
        //'sJobName 現場名。
        //'sDistrictName 地区名。
        //'nCoordNum 座標系番号(1～19)。
        //'bGeoidoEnable ジオイド有効/無効。True=有効。False=無効。
        //'sGeoidoPath ジオイドモデルのパス。
        //'bSemiDynaEnable セミ・ダイナミック有効/無効。True=有効。False=無効。'2009/11 H.Nakamura
        //'sSemiDynaPath セミ・ダイナミックパラメータファイルのパス。'2009/11 H.Nakamura
        //Public Function SetJob(ByVal sJobName As String, ByVal sDistrictName As String, ByVal nCoordNum As Long, ByVal bGeoidoEnable As Boolean, ByVal sGeoidoPath As String, ByVal bSemiDynaEnable As Boolean, ByVal sSemiDynaPath As String)
        //    m_sJobName = sJobName
        //    m_sDistrictName = sDistrictName
        //    m_nCoordNum = nCoordNum
        //    m_bGeoidoEnable = bGeoidoEnable
        //    m_sGeoidoPath = sGeoidoPath
        //    m_bSemiDynaEnable = bSemiDynaEnable 'セミ・ダイナミック対応。'2009/11 H.Nakamura
        //    m_sSemiDynaPath = sSemiDynaPath 'セミ・ダイナミック対応。'2009/11 H.Nakamura
        //    m_bEmpty = False
        //    m_bModifyed = True
        //End Function
        //**************************************************************************************
        //**************************************************************************************

        //<<<<<<<<<-----------23/12/29 K.setoguchi@NV


        //24/01/04 K.setoguchi@NV---------->>>>>>>>>>
        //**************************************************************************************
        //**************************************************************************************
        /// <summary>
        /// 保存。
        /// sPath で指定されたファイルに保存する。
        ///
        /// 引き数：
        ///     sPath 保存ファイルのパス。
        /// </summary>
        /// <param name="sPath"></param>
        public void Save(string sPath)          //(Ex.)"C:\Develop\NetSurv\Src\NS-App\NS-Survey\UserData\0274\"
        {

            MdlUtility mdiUtility = new MdlUtility();
            //-------------------------------------------------------
            //    NetworkModel m_clsNetworkModel = new NetworkModel();
            //    BaseLineAnalysisParam m_clsBaseLineAnalysisParam = new BaseLineAnalysisParam;
            //    AngleDiffParam m_clsAngleDiffParamRing = new AngleDiffParam;
            //    AngleDiffParam m_clsAngleDiffParamBetween = new AngleDiffParam:
            //    AngleDiffParam m_clsAngleDiffParamHeight = new AngleDiffParam;         //'2023/05/19 Hitz H.Nakamura 楕円体高の閉合差を追加。
            //    AccountMakert m_clsAccountMaker = new AccountMaker;
            //    AccountParam m_clsAccountParamHand = new AccountParam;
            //    AccountParam m_clsAccountParamWrite = new AccountParam;
            //    AccountParam m_clsAccountParamCoordinate = new AccountParam;
            //    AccountOverlapParamt m_clsAccountOverlapParam = new AccountOverlapParam;
            //    AccountParam m_clsAccountParamEccentricCorrect = new AccountParam;
            //    AccountParam m_clsAccountParamSemiDyna = new AccountParaml          //'セミ・ダイナミック対応。'2009 / 11 H.Nakamura
            //    AccountCadastralParam m_clsAccountCadastralParam = new AccountCadastralParam;
            //    AutoOrderVectorParam m_clsAutoOrderVectorParam = new AutoOrderVectorParam;
            //    AccountParam m_clsAccountParamResultBase = new AccountParam             //'2007/7/18 NGS Yamada


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

            if (mdiUtility.RemoveFile(sTemp, false))
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
                    mdiUtility.FileWrite_PutString(bw, m_sJobName);
                    mdiUtility.FileWrite_PutString(bw, m_sDistrictName);

                    //-------------------------------------------------------
                    //    Call PutString(clsFile.Number, m_sSupervisor)
                    //    Put #clsFile.Number, , m_nCoordNum
                    //    Put #clsFile.Number, , m_bGeoidoEnable
                    //    Call PutString(clsFile.Number, m_sGeoidoPath)
                    //    Put #clsFile.Number, , m_bTkyEnable
                    //    Call PutString(clsFile.Number, m_sTkyPath)
                    mdiUtility.FileWrite_PutString(bw, m_sSupervisor);      //Version対応で""です。
                    bw.Write((uint)m_nCoordNum);                            //nCoordNum = br.ReadInt32();
                    mdiUtility.PutFileBool(bw, m_bGeoidoEnable);            //bGeoidoEnable = GetFileBool(br);
                    mdiUtility.FileWrite_PutString(bw, m_sGeoidoPath);      //sGeoidoPath = mdiUtility.FileRead_GetString(br); 
                    mdiUtility.PutFileBool(bw, m_bTkyEnable);               //bTkyEnable = GetFileBool(br);
                    mdiUtility.FileWrite_PutString(bw, m_sTkyPath);         //sTkyPath = mdiUtility.FileRead_GetString(br);

                    //-------------------------------------------------------
                    //    'セミ・ダイナミック対応。'2009/11 H.Nakamura
                    //    Put #clsFile.Number, , m_bSemiDynaEnable
                    //    Call PutString(clsFile.Number, m_sSemiDynaPath)
                    mdiUtility.PutFileBool(bw, m_bSemiDynaEnable);          //bSemiDynaEnable = GetFileBool(br);
                    mdiUtility.FileWrite_PutString(bw, m_sSemiDynaPath);    //sSemiDynaPath = mdiUtility.FileRead_GetString(br);


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

                    //--------------------------------------------------------
                    //    Call clsFile.CloseFile
                    fs.Close();

                    //--------------------------------------------------------
                    //    'テンポラリファイルと置き換える。
                    //    Call ReplaceFile(sTemp, sPath & DATA_FILE_NAME)       //sTemp = "C:\Develop\NetSurv\Src\NS-App\NS-Survey\Temp\Document.tmp"
                    //                                                          //sPath = "C:\Develop\NetSurv\Src\NS-App\NS-Survey\UserData\0275\"  // data
                    mdiUtility.ReplaceFile(sTemp, $"{sPath}{GENBA_CONST.DATA_FILE_NAME}");

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
                    bool dmy1 = mdiUtility.DeleteDir(sDstObsPointPath, true);
                    bool dmy2 = mdiUtility.CopyDir(sSrcObsPointPath, sDstObsPointPath, true);

                    //--------------------------------------------------------
                    //    m_sPath = sPath 'パスを更新。
                    //    m_bModifyed = False
                    m_sPath = sPath;        //'パスを更新。
                    m_bModifyed = false;

                }

            }
        }
        //----------------------------------------------------------------------------------------------
        //'保存。
        //'
        //'sPath で指定されたファイルに保存する。
        //'
        //'引き数：
        //'sPath 保存ファイルのパス。
        //Public Sub Save(ByVal sPath As String)
        //
        //    'テンポラリファイル。
        //    Dim sTemp As String
        //    sTemp = App.Path & TEMPORARY_PATH & TEMP_FILE_NAME
        //    On Error Resume Next
        //    Call RemoveFile(sTemp)
        //    On Error GoTo 0
        //    
        //    'テンポラリファイルに書き込み。
        //    Dim clsFile As New FileNumber
        //    Open sTemp For Binary Access Write Lock Read Write As #clsFile.Number
        //    
        //    Put #clsFile.Number, , DOCUMENT_FILE_VERSION
        //    Call PutString(clsFile.Number, m_sJobName)
        //    Call PutString(clsFile.Number, m_sDistrictName)
        //    '↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑
        //    '地区名より前の領域は ProjectFileManager 関係に影響するので注意。
        //   
        //    Call PutString(clsFile.Number, m_sSupervisor)
        //    Put #clsFile.Number, , m_nCoordNum
        //    Put #clsFile.Number, , m_bGeoidoEnable
        //    Call PutString(clsFile.Number, m_sGeoidoPath)
        //    Put #clsFile.Number, , m_bTkyEnable
        //    Call PutString(clsFile.Number, m_sTkyPath)
        //    
        //    'セミ・ダイナミック対応。'2009/11 H.Nakamura
        //    Put #clsFile.Number, , m_bSemiDynaEnable
        //    Call PutString(clsFile.Number, m_sSemiDynaPath)
        //
        //
        //    Call m_clsNetworkModel.Save(clsFile.Number)
        //    Call m_clsBaseLineAnalysisParam.Save(clsFile.Number)
        //    Call m_clsAngleDiffParamRing.Save(clsFile.Number)
        //    Call m_clsAngleDiffParamBetween.Save(clsFile.Number)
        //    Call m_clsAngleDiffParamHeight.Save(clsFile.Number) '2023/05/19 Hitz H.Nakamura 楕円体高の閉合差を追加。
        //    Call m_clsAccountParamHand.Save(clsFile.Number)
        //    Call m_clsAccountParamWrite.Save(clsFile.Number)
        //    Call m_clsAccountParamCoordinate.Save(clsFile.Number)
        //    Call m_clsAccountOverlapParam.Save(clsFile.Number)
        //    Call m_clsAccountParamEccentricCorrect.Save(clsFile.Number)
        //    Call m_clsAccountParamSemiDyna.Save(clsFile.Number) 'セミ・ダイナミック対応。'2009/11 H.Nakamura
        //    Call m_clsAccountCadastralParam.Save(clsFile.Number)
        //    Call m_clsAutoOrderVectorParam.Save(clsFile.Number)
        //    Call m_clsAccountParamResultBase.Save(clsFile.Number)   '2007/7/18 NGS Yamada
        //    
        //    Dim i As Long
        //    For i = 0 To OUTPUT_TYPE_COUNT - 1
        //        Call m_clsOutputParam(i).Save(clsFile.Number)
        //    Next
        //    For i = 0 To DXF_TYPE_COUNT - 1
        //        Call m_clsDXFParam(i).Save(clsFile.Number)
        //    Next
        //    
        //    'チェックサム。
        //    Dim nSize As Long
        //    nSize = Loc(clsFile.Number)
        //    Put #clsFile.Number, , nSize
        //    
        //    Call clsFile.CloseFile
        //    
        //    'テンポラリファイルと置き換える。
        //    Call ReplaceFile(sTemp, sPath & DATA_FILE_NAME)
        //    
        //    '観測点ファイルのコピー。
        //    Dim sSrcObsPointPath As String
        //    Dim sDstObsPointPath As String
        //    sSrcObsPointPath = App.Path & TEMPORARY_PATH & "." & OBSPOINT_PATH
        //    sDstObsPointPath = sPath & "." & OBSPOINT_PATH
        //    Call DeleteDir(sDstObsPointPath, True)
        //    Call CopyDir(sSrcObsPointPath, sDstObsPointPath, True)
        //
        //
        //    m_sPath = sPath 'パスを更新。
        //    m_bModifyed = False
        //
        //End Sub
        //**************************************************************************************
        //<<<<<<<<<-----------24/01/04 K.setoguchi@NV



        //**************************************************************************************
        //**************************************************************************************
        /// <summary>
        //'メソッド
        //
        //'オブジェクトをクリアする。
        /// </summary>
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

            m_bSemiDynaEnable = false;      //'セミ・ダイナミック対応。'2009/11 H.Nakamura
            m_sSemiDynaPath = "";           //'セミ・ダイナミック対応。'2009/11 H.Nakamura

            m_clsNetworkModel = new NetworkModel();                     //    Set m_clsNetworkModel = New NetworkModel
            m_clsBaseLineAnalysisParam = new BaseLineAnalysisParam();   //    Set m_clsBaseLineAnalysisParam = New BaseLineAnalysisParam
            m_clsAngleDiffParamRing = new AngleDiffParam();             //    Set m_clsAngleDiffParamRing = New AngleDiffParam
            m_clsAngleDiffParamBetween = new AngleDiffParam();          //    Set m_clsAngleDiffParamBetween = New AngleDiffParam
            m_clsAngleDiffParamHeight = new AngleDiffParam();           //    Set m_clsAngleDiffParamHeight = New AngleDiffParam '2023/05/19 Hitz H.Nakamura 楕円体高の閉合差を追加。
            m_clsAccountMaker = new AccountMaker();                     //    Set m_clsAccountMaker = New AccountMaker
            m_clsAccountParamHand = new AccountParam();                 //    Set m_clsAccountParamHand = New AccountParam
            m_clsAccountParamWrite = new AccountParam();                //    Set m_clsAccountParamWrite = New AccountParam
            m_clsAccountParamCoordinate = new AccountParam();           //    Set m_clsAccountParamCoordinate = New AccountParam
            m_clsAccountOverlapParam = new AccountOverlapParam();       //    Set m_clsAccountOverlapParam = New AccountOverlapParam
            m_clsAccountParamEccentricCorrect = new AccountParam();     //    Set m_clsAccountParamEccentricCorrect = New AccountParam
            m_clsAccountParamSemiDyna = new AccountParam();             //    Set m_clsAccountParamSemiDyna = New AccountParam 'セミ・ダイナミック対応。'2009/11 H.Nakamura
            m_clsAccountCadastralParam = new AccountCadastralParam();   //    Set m_clsAccountCadastralParam = New AccountCadastralParam
            m_clsAutoOrderVectorParam = new AutoOrderVectorParam();     //    Set m_clsAutoOrderVectorParam = New AutoOrderVectorParam
            m_clsAccountParamResultBase = new AccountParam();           //    Set m_clsAccountParamResultBase = New AccountParam  '2007/7/18 NGS Yamada

            //--------------------------------------------------------
            //    Dim i As Long
            //    For i = 0 To OUTPUT_TYPE_COUNT - 1
            //    Set m_clsOutputParam(i) = New OutputParam
            //    Next
            //    For i = 0 To DXF_TYPE_COUNT - 1
            //      Set m_clsDXFParam(i) = New DXFParam
            //    Next
            long i;
            // OutputParam WorkOutputParam;
            for (i = 0; i < (long)MdlNSSDefine.OUTPUT_TYPE.OUTPUT_TYPE_COUNT - 1; i++)
            {
                OutputParam WorkOutputParam = new OutputParam();

                List<OutputParam> m_clsOutputParam = new List<OutputParam>();
                m_clsOutputParam.Add(WorkOutputParam);


            }
            DXFParam WorkDXFParam;
            for (i = 0; i < (long)MdlNSSDefine.DXF_TYPE.DXF_TYPE_COUNT - 1; i++) {

                WorkDXFParam = new DXFParam();      //'DXFファイル種別 > 種別数。
                m_clsDXFParam.Add(WorkDXFParam);
            }



            //--------------------------------------------------------
            //    '観測点ファイルの削除。
            //    Call EmptyDir(App.Path & TEMPORARY_PATH & "." & OBSPOINT_PATH)
            //    'セミ・ダイナミックの終了処理。'2009/11 H.Nakamura
            //     Call mdlSemiDyna.Terminate
            //    End Sub

            //観測点ファイルの削除
            string App_Path = @"C:\Develop\NetSurv\Src\NS-App\NS-Survey";
            bool bDMY = mdiUtility.EmptyDir($"{App_Path}{MdlNSDefine.TEMPORARY_PATH}{MdlNSSDefine.OBSPOINT_PATH}", false);

            //'セミ・ダイナミックの終了処理。'2009/11 H.Nakamura

            MdlSemiDyna mdlSemiDyna = new MdlSemiDyna();
            mdlSemiDyna.Terminate();


        }
        //--------------------------------------------------------------------------------------
        //'メソッド
        //
        //'オブジェクトをクリアする。
        //Public Sub Clear()
        //    m_bEmpty = True
        //    m_sPath = ""
        //    m_bModifyed = False
        //    m_sJobName = ""
        //    m_sDistrictName = ""
        //    m_sSupervisor = ""
        //    m_nCoordNum = 0
        //    m_bGeoidoEnable = False
        //    m_sGeoidoPath = ""
        //    m_bTkyEnable = False
        //    m_sTkyPath = ""
        //    m_bSemiDynaEnable = False 'セミ・ダイナミック対応。'2009/11 H.Nakamura
        //    m_sSemiDynaPath = "" 'セミ・ダイナミック対応。'2009/11 H.Nakamura
        //    Set m_clsNetworkModel = New NetworkModel
        //    Set m_clsBaseLineAnalysisParam = New BaseLineAnalysisParam
        //    Set m_clsAngleDiffParamRing = New AngleDiffParam
        //    Set m_clsAngleDiffParamBetween = New AngleDiffParam
        //    Set m_clsAngleDiffParamHeight = New AngleDiffParam '2023/05/19 Hitz H.Nakamura 楕円体高の閉合差を追加。
        //    Set m_clsAccountMaker = New AccountMaker
        //    Set m_clsAccountParamHand = New AccountParam
        //    Set m_clsAccountParamWrite = New AccountParam
        //    Set m_clsAccountParamCoordinate = New AccountParam
        //    Set m_clsAccountOverlapParam = New AccountOverlapParam
        //    Set m_clsAccountParamEccentricCorrect = New AccountParam
        //    Set m_clsAccountParamSemiDyna = New AccountParam 'セミ・ダイナミック対応。'2009/11 H.Nakamura
        //    Set m_clsAccountCadastralParam = New AccountCadastralParam
        //    Set m_clsAutoOrderVectorParam = New AutoOrderVectorParam
        //    Set m_clsAccountParamResultBase = New AccountParam  '2007/7/18 NGS Yamada
        //    
        //    Dim i As Long
        //    For i = 0 To OUTPUT_TYPE_COUNT - 1
        //    Set m_clsOutputParam(i) = New OutputParam
        //    Next
        //    For i = 0 To DXF_TYPE_COUNT - 1
        //      Set m_clsDXFParam(i) = New DXFParam
        //    Next
        //    '観測点ファイルの削除。
        //    Call EmptyDir(App.Path & TEMPORARY_PATH & "." & OBSPOINT_PATH)
        //    'セミ・ダイナミックの終了処理。'2009/11 H.Nakamura
        //     Call mdlSemiDyna.Terminate
        //    End Sub
        //24/01/04 K.setoguchi@NV---------->>>>>>>>>>
        //***************************************************************************
        //***************************************************************************
        //<<<<<<<<<-----------24/01/04 K.setoguchi@NV

    }
}
