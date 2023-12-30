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
        //private Const TEMP_FILE_NAME As String = "Document.tmp" '保存処理用のテンポラリファイルの名前。
        //
        //'恒久プロパティ
        public bool PageNumberVisible;          // As Boolean '帳票ページ番号表示フラグ。True=表示。False=非表示。
        public long NumberOfMinSV;              // As Long '最少衛星数。0の場合は観測データから取得。1～12が固定値。
        //Public EllipseModel As ELLIPSE_MODEL '記簿に出力する楕円体モデル。
        public long SameTimeMin;                // As Long '最小同時観測時間(秒)。
        public long SatelliteCountMin;          // As Long '最少共通衛星数。
        public string GeoidoPathDef;            // As String 'ジオイドモデルのパスのデフォルト。
        public string SemiDynaPathDef;          // As String 'セミ・ダイナミックパラメータファイルのパスのデフォルト。'2009/11 H.Nakamura
        public long DefLeapSec;                 // As Long 'デフォルトの閏秒。
        //  public TIME_ZONE TimeZone;      // As TIME_ZONE  '時間帯。
        //
        //'2008/10/13 NGS Yamada
        //'ユーザデータを管理するフォルダのパスを追加
        //Public UserDataPath As String
        //
        //Public ImportComPort As Long '受信機を接続するCOMポート。2007/6/20 NGS Yamada
        //Public ImportComPortType As Boolean '受信機を接続するCOMポートの取得方法(True=自動取得、False=手動選択)。2007/7/2 NGS Yamada
        //Public ImportDataSave As Boolean    '受信機からインポート時に観測データを保存する（True=保存する、False=保存しない）2007/8/10 NGS Yamada
        //
        //Public PlotPointLabel As Long 'プロット画面に表示するラベルの種類。0=観測点No,1=観測点名称,2=観測点No(観測点名称)。 2006/10/6 NGS Yamada
        //Public DisableVectorVisible As Boolean '「無効」なベクトルをプロット画面に表示するか？。True=表示,False=非表示。 2006/10/10 NGS Yamada
        //
        //'インプリメンテーション
        public bool m_bEmpty;                  // As Boolean '空フラグ。True=空。False=空でない。
        public string m_sPath;                 // As String '保存ファイルのパス。
        public bool m_bModifyed;               // As Boolean '更新フラグ。True=更新あり。False=更新なし。
        //
        public string m_sJobName;              // As String '現場名。
        public string m_sDistrictName;         // As String '地区名。
        public string m_sSupervisor;           // As String 'プログラム管理者。
        public long m_nCoordNum;               // As Long   '座標系番号(1～19)。
        public bool m_bGeoidoEnable;           // As Boolean 'ジオイド有効/無効。True=有効。False=無効。
        public string m_sGeoidoPath;           // As String 'ジオイドモデルのパス。
        public bool m_bTkyEnable;              // As Boolean '旧日本測地系有効/無効。True=有効。False=無効。
        public string m_sTkyPath;              // As String '旧日本測地系パラメータファイルのパス。
        public bool m_bSemiDynaEnable;         // As Boolean 'セミ・ダイナミック有効/無効。True=有効。False=無効。'2009/11 H.Nakamura
        public string m_sSemiDynaPath;         // As String 'セミ・ダイナミックパラメータファイルのパス。'2009/11 H.Nakamura
        //Private WithEvents m_clsNetworkModel As NetworkModel 'ネットワークモデル。
        //Private m_clsBaseLineAnalysisParam As BaseLineAnalysisParam '基線解析パラメータ。
        //Private m_clsAngleDiffParamRing As AngleDiffParam '環閉合差パラメータ。
        //Private m_clsAngleDiffParamBetween As AngleDiffParam '電子基準点間の閉合差パラメータ。
        //Private m_clsAngleDiffParamHeight As AngleDiffParam '楕円体高の閉合差パラメータ。'2023/05/19 Hitz H.Nakamura 楕円体高の閉合差を追加。
        //Private m_clsAccountMaker As AccountMaker '帳票作成。
        //Private m_clsAccountParamHand As AccountParam '観測手簿パラメータ。
        //Private m_clsAccountParamWrite As AccountParam '観測記簿パラメータ。
        //Private m_clsAccountParamCoordinate As AccountParam '座標計算簿パラメータ。
        //Private m_clsAccountOverlapParam As AccountOverlapParam '点検計算簿(重複基線)パラメータ。
        //Private m_clsAccountParamEccentricCorrect As AccountParam '偏心計算簿パラメータ。
        //Private m_clsAccountParamSemiDyna As AccountParam 'セミ・ダイナミック補正表パラメータ。'2009/11 H.Nakamura
        //Private m_clsAccountCadastralParam As AccountCadastralParam '地籍図根三角測量精度管理表パラメータ。
        //Private m_clsAccountParamResultBase As AccountParam '座標一覧表パラメータ。2007/7/18 NGS Yamada
        //Private m_clsOutputParam(OUTPUT_TYPE_COUNT - 1) As OutputParam '外部出力ファイル出力パラメータ。
        //Private m_clsDXFParam(DXF_TYPE_COUNT - 1) As DXFParam 'DXFファイル出力パラメータ。
        //Private m_clsAutoOrderVectorParam As AutoOrderVectorParam '基線ベクトルの向きの自動整列パラメータ。
        //'*******************************************************************************


        MdlUtility mdiUtility = new MdlUtility();

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

    }
}
