using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurvLine
{
    public class external01
    {
    }
    public class FRM_MAIN  //frmMain
    {
        /// <summary>
        /// frmMainの領域
        /// </summary>
        public string UserDataPath; //"C:\Develop\NetSurv\Src\NS-App\NS-Survey\UserData\"
    }



    public class GENBA_CONST
    {
        /// <summary>
        /// 現場リストの定義
        //  '引き数：
        //  'lvProject 対象とするリストビュー。
        //  'bCheck チェックボックスの有無。True=チェックボックス有り。False=チェックボックス無し。
        /// </summary>
        public const int CS_MAX_STRING = 256;
        public const string CS_ERROR_MSG = "error!";

        public const string DATA_FILE_NAME = "data";
    }

    public class GENBA_AREA
    {
        /// <summary>
        /// 現場リストの定義
        //  '引き数：
        //  'lvProject 対象とするリストビュー。
        //  'bCheck チェックボックスの有無。True=チェックボックス有り。False=チェックボックス無し。
        /// </summary>
        public int sp1;
        public string sp2;
    }
    public struct GENBA_STRUCT
    {
        /// <summary>
        /// 現場リストの定義
        //  '引き数：
        //  'lvProject 対象とするリストビュー。
        //  'bCheck チェックボックスの有無。True=チェックボックス有り。False=チェックボックス無し。
        /// </summary>
        public String sJobNames;           //Dim sJobNames() As String
        public String sDistrictNames;      //Dim sDistrictNames() As String
        public String sFolderNames;        //Dim sFolderNames() As String
        public DateTime tModTime;           //Dim tModTime() As Date
        public DateTime tCreateTime;        //Dim tCreateTime() As Date

    }

    public struct GENBA_STRUCT_S
    {
        /// <summary>
        /// 現場リストの定義
        //  '引き数：
        //  'lvProject 対象とするリストビュー。
        //  'bCheck チェックボックスの有無。True=チェックボックス有り。False=チェックボックス無し。
        /// </summary>
        public string sJobName;
        public string sDistrictName;
        public string sSupervisor;
        public long nCoordNum;
        public bool bGeoidoEnable;
        public string sGeoidoPath;
        public bool bTkyEnable;
        public string sTkyPath;
        public bool bSemiDynaEnable;   //'セミ・ダイナミック対応。'2009/11 H.Nakamura
        public string sSemiDynaPath;   //'セミ・ダイナミック対応。'2009/11 H.Nakamura
        public double XX, YY, XZ, YZ, ZZ;

        //*******************************************************************************************
        //　以降は、networkModel.Load
        //*******************************************************************************************

        //------------------------------
        //'基線ベクトル
        //------------------------------
        public string Session;              //'セッション名。
        public DateTime StrTimeGPS;         //'観測開始日時(GPS)。
        public DateTime EndTimeGPS;         //'観測終了日時(GPS)。
        public bool Exclusion;              //'解析除外フラグ。True=除外。False=解析。
        //------------------------------
        //[VB]  Public Analysis As ANALYSIS_STATUS '解析状態。
        public int Analysis;                //'解析状態                    
        //------------------------------
        //  Public Orbit As ORBIT_TYPE '軌道暦。
        public int Orbit;                   //'軌道暦。                                       
        //------------------------------
        //[VB] Public Frequency As FREQUENCY_TYPE '周波数。
        public int Frequency;               //'周波数。
        //------------------------------
        //[VB]Public SolveMode As SOLVEMODE_TYPE '基線解析モード。
        public int SolveMode;

        //------------------------------
        public DateTime AnalysisStrTimeGPS; //'解析開始日時(GPS)。
        public DateTime AnalysisEndTimeGPS; //'解析終了日時(GPS)。
                                            //------------------------------

        //------------------------------
        //[VB]Get #nFile, , ElevationMask
        public double ElevationMask;        //'仰角マスク(度)。
        //------------------------------
        //[VB]Get #nFile, , Interval
        public double Interval;             //'解析間隔(秒)。
        //------------------------------
        //[VB]Get #nFile, , Temperature
        public double Temperature;           //'気温(℃)。
        //------------------------------
        //[VB]Get #nFile, , Pressure
        public double Pressure;             //'気圧(hPa)。
        //------------------------------
        //[VB]Get #nFile, , Humidity
        public double Humidity;             //'湿度(％)。
        //------------------------------
        //[VB]Get #nFile, , Troposhere
        public int Troposhere;           //'対流圏モデル。
        //------------------------------
        //[VB]If nVersion< 3100 Then
        //[VB]AnalysisFixed = False
        //[VB]Else
        //[VB]Get #nFile, , AnalysisFixed
        //[VB]End If
        public bool AnalysisFixed;          //'解析始点固定点フラグ。True=解析始点が固定点。False=解析始点は固定点で無い。

        //------------------------------
        //[VB]    If nVersion< 1700 Then
        //[VB]        AmbPercentage = 0
        //[VB]    Else
        //[VB]        Get #nFile, , AmbPercentage
        //[VB]    End If
        public long AmbPercentage;           //'FIX率(％)。

        //------------------------------
        //[VB] Get #nFile, , Bias
        public double Bias;                 //'バイアス決定比(ｍ)。
        //------------------------------
        //[VB]Get #nFile, , EpochUsed
        public long EpochUsed;              //'使用エポック数。
        //------------------------------
        //[VB]Get #nFile, , EpochRejected
        public long EpochRejected;           //'棄却エポック数。
        //------------------------------
        //[VB]If 1600 <= nVersion And nVersion< 1700 Then
        //[VB]  Dim nObsAll As Long
        //[VB]  Dim nObsUsed As Long
        //[VB]  Get #nFile, , nObsAll
        //[VB]  Get #nFile, , nObsUsed
        //[VB]End If
        public long nObsAll;
        public long nObsUsed;
        //------------------------------
        //[VB]Get #nFile, , RMS
        public double RMS;          //'RMS値(ｍ)。
        //------------------------------
        //[VB]Get #nFile, , RDOP
        public double RDOP;         //'RDOP値。
        //------------------------------
        //[VB]Get #nFile, , IsDispersion
        public bool IsDispersion;   //'分散・共分散の有効/無効。True=有効。False=無効。
        //------------------------------
        //[VB]Get #nFile, , RcvNumbersGps
        public long RcvNumbersGps;      //'受信GPS衛星番号。ビットフラグ。1=受信。0=非受信。ビット0がGPS1番、ビット1がGPS2番。。。。
        //------------------------------
        //------------------------------
        public long RcvNumbersGlonass;  //'受信GLONASS衛星番号。ビットフラグ。1=受信。0=非受信。ビット0がR1番、ビット1がR2番。。。。
        //------------------------------
        //'2017/06/06 NS6000対応。'''''''''''''''''''''''''''''''''''''''''''''''''''''''
        public long RcvNumbersQZSS;     //'受信QZSS衛星番号。ビットフラグ。1=受信。0=非受信。ビット0がJ1番、ビット1がRJ番。。。。
        //'Public RcvNumbersGalileo As Long '受信Galileo衛星番号。ビットフラグ。1=受信。0=非受信。ビット0がE1番、ビット1がE2番。。。。    '2018/08/21 Hitz H.Nakamura 衛星数が増えたので64ビットに増やす。
        public long RcvNumbersGalileo1; //'受信Galileo衛星番号。ビットフラグ。1=受信。0=非受信。ビット0がE1番、ビット1がE2番。。。。
        public long RcvNumbersGalileo2; //'受信Galileo衛星番号。ビットフラグ。1=受信。0=非受信。ビット0がE33番、ビット1がE34番。。。。
        public long RcvNumbersBeiDou1;  //'受信BeiDou1衛星番号。ビットフラグ。1=受信。0=非受信。ビット0がC1番、ビット1がC2番。。。。
        public long RcvNumbersBeiDou2;  //'受信BeiDou2衛星番号。ビットフラグ。1=受信。0=非受信。ビット0がC33番、ビット1がC34番。。。。
        //'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        public double MaxResidL1_Legacy;    //As Double '最大残差L1。
        public double MaxResidL2_Legacy;    //As Double '最大残差L2。
        public double MinRatio;             //As Double '最小バイアス決定比。
        public double MinPs;                //As Double '最小 probability of success。
        public double ExcludeGPS;           //As Long '不使用GPS。ビットフラグ。1=使用。0=無使用。ビット0がGPS1番、ビット1がGPS2番。。。。
        public long ExcludeGlonass;         //As Long '不使用GLONASS。ビットフラグ。1=使用。0=無使用。ビット0がR1番、ビット1がR2番。。。。
        public bool GlonassFlag;            //As Boolean 'GLONASSフラグ。
        //'2017/06/06 NS6000対応。'''''''''''''''''''''''''''''''''''''''''''''''''''''''
        public long ExcludeQZSS;            //As Long '不使用QZSS。ビットフラグ。1=使用。0=無使用。ビット0がJ1番、ビット1がJ2番。。。。
        //  'Public ExcludeGalileo;         //As Long '不使用Galileo。ビットフラグ。1=使用。0=無使用。ビット0がE1番、ビット1がE2番。。。。 '2018/08/21 Hitz H.Nakamura 衛星数が増えたので64ビットに増やす。
        public long ExcludeGalileo1;        //As Long '不使用Galileo。ビットフラグ。1=使用。0=無使用。ビット0がE1番、ビット1がE2番。。。。
        public long ExcludeGalileo2;        //As Long '不使用Galileo。ビットフラグ。1=使用。0=無使用。ビット0がE33番、ビット1がE34番。。。。
        public long ExcludeBeiDou1;         //As Long '不使用BeiDou。ビットフラグ。1=使用。0=無使用。ビット0がC1番、ビット1がC2番。。。。
        public long ExcludeBeiDou2;         //As Long '不使用BeiDOu。ビットフラグ。1=使用。0=無使用。ビット0がC33番、ビット1がC34番。。。。
        public bool QZSSFlag;               //As Boolean 'QZSSフラグ。
        public bool GalileoFlag;            //As Boolean 'Galileoフラグ。
        public bool BeiDouFlag;             //As Boolean 'BeiDouフラグ。
                                            //'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        public int m_nLineType;             // As OBJ_MODE '基線ベクトル種類。
        public string StrPcvVer;            //As Variant '始点PCVバージョン。補正無しの場合は空文字。不明の場合は Null。
        public string EndPcvVer;            //As Variant '終点PCVバージョン。補正無しの場合は空文字。不明の場合は Null。
        public long AnalysisOrder;          //As Long '解析順番。より数値が小さい方を後に解析されたこととする。第３２ビットは符号ビットとして、第３１～２９は一時的な順位の重み付けに使う。第２８ビットは解析㊥、非解析の区別に使う。第２７～１ビットで順番をあらわす。つまり、最低順位は &H0FFFFFFF となる。









        //'*******************************************************************************
        //'観測点属性
        //'*******************************************************************************

        //---------------------------------------
        public int Mode;                    //As OBJ_MODE '観測点モード。
        public bool ProvisionalSession;     //As Boolean '仮のセッション。
        //---------------------------------------
        public string FileTitle;            //As String 'ファイルタイトル。
        //---------------------------------------
        public string RinexExt;             //As String 'RINEXファイル拡張子(先頭２文字)。
        //---------------------------------------
        public string SrcPath;              //As String 'ソースファイルのパス。
        //---------------------------------------
        public DateTime StrTimeGPS_P;       //As Date '観測開始日時(GPS)。
        public DateTime EndTimeGPS_P;       //As Date '観測終了日時(GPS)。
        //---------------------------------------
        public long LeapSeconds;            //As Long 'うるう秒。
        public double Interval_p;           //As Double '間隔(秒)。
        public string RecType;              //As String '受信機名称。
        public string RecNumber;            //As String '受信機シリアル。
        public string AntType;              //As String 'アンテナ種別。
        //---------------------------------------
        //    If nVersion < 3800 Then AntType = GetPrivateProfileString(PROFILE_SUB_SEC_SECV1TOSECV2, AntType, GetPrivateProfileString(PROFILE_ANT_SEC_LIST, PROFILE_ANT_KEY_ANT & GetPrivateProfileString(PROFILE_ANT_SEC_LIST, PROFILE_ANT_KEY_UNKNOWN, "0", App.Path & "\" & PROFILE_ANT_FILE), "", App.Path & "\" & PROFILE_ANT_FILE), App.Path & "\" & PROFILE_SUB_FILE)
        //---------------------------------------
        public string AntNumber;            //As String 'アンテナシリアル番号。
        public string AntMeasurement;       //As String 'アンテナ測位方法。
        public double AntHeight;            //As Double 'アンテナ高(ｍ)。
        public double ElevationMask_P;      //As Double '仰角マスク(度)。
        public long NumberOfMinSV;          //As Long '最少衛星数。
        public int ImportType;              //As IMPORT_TYPE 'インポートファイルの種別。
        public bool GlonassFlag_P;          //As Boolean 'GLONASSフラグ。
        //---------------------------------------
        public bool QZSSFlag_P;             //As Boolean 'QZSSフラグ。
        public bool GalileoFlag_P;          //As Boolean 'Galileoフラグ。
        public bool BeiDouFlag_P;           //As Boolean 'BeiDouフラグ。
        public bool MixedNav;               //As Boolean '混合タイプの暦ファイルか？
        public long RinexVersion;           //As Long 'RINEXファイルのバージョン。バージョン番号を1000倍した整数。
        //---------------------------------------

        //****************************************************************
        //  ObservationCommonAttributes
        //----------------------------------------------------------------

        //23/12/20 K.Setoguchi---->>>>
        //(del)     public string Number;               //As String '番号。
        //(del)     public string Name;                 //As String '名称。
        //(del)     public string GenuineNumber;        //As String '本点番号。
        //(del)     public string GenuineName;          //As String '本点名称。
        //(del)     public long ObjectType;             //As Long 'オブジェクト種別。
        //(del)     public int OldEditCode;             //As EDITCODE_STYLE '旧編集コード。
        //****************************************************************
        public OCA_STRUCT OCA;                  //ObservationCommonAttributes       //23/12/20 K.Setoguchi
        //<<<<----23/12/20 K.Setoguchi

        //****************************************************************
        //  CoordinatePointFix
        //----------------------------------------------------------------
        //23/12/20 K.Setoguchi---->>>>                  //
        //'インプリメンテーション                      //
        //(del)     public double m_nX;               //As Double 'X。
        //(del)     public double m_nY;               //As Double 'Y。
        //(del)     public double m_nZ;               //As Double 'Z。
        //(del)     public double m_nRoundX;          //As Double '丸めX。
        //(del)     public double m_nRoundY;          //As Double '丸めY。
        //(del)     public double m_nRoundZ;          //As Double '丸めZ。
        //(del)     public double m_nHeight;          //As Double '楕円体高。
        //(del)     public double m_nCoordinateType;  //As COORDINATE_TYPE '座標値種別。
        //(del)     public int m_nEditCode;        //As EDITCODE_STYLE '編集コード。
        //(del)     public string m_sEditX;            //As String '入力X。
        //(del)     public string m_sEditY;            //As String '入力Y。
        //(del)     public string m_sEditZ;            //As String '入力Z。
        //(del)     public string m_sEditLat;          //As String '入力緯度。
        //(del)     public string m_sEditLon;          //As String '入力経度。
        //(del)     public string m_sEditHeight;       //As String '入力高さ(ｍ)。
        //(del)     public long m_nEditCoordNum;       //As Long '入力座標系番号(1～19)。
        //(del)     //'セミ・ダイナミック変換'2009/11 H.Nakamura
        //(del)     public double m_nSemiDynaCounter;  //As Long 'セミ・ダイナミック補正変換番号。0 は未計算。
        //(del)     public double m_nLatKON;           //As Double '今期緯度(度)。
        //(del)     public double m_nLonKON;           //As Double '今期経度(度)。
        //(del)     public double m_nHeightKON;        //As Double '今期楕円体高(ｍ)。
        //(del)     public double m_nXKON;             //As Double '今期X。
        //(del)     public double m_nYKON;             //As Double '今期Y。
        //(del)     public double m_nZKON;             //As Double '今期Z。
        //(del)     public double m_nRoundXKON;        //As Double '今期丸めX。
        //(del)     public double m_nRoundYKON;        //As Double '今期丸めY。
        //(del)     public double m_nRoundZKON;        //As Double '今期丸めZ。
        //****************************************************************
        public OPFix_STRUCT OPFix;                      //CoordinatePointFix           //23/12/20 K.Setoguchi
        //<<<<----23/12/20 K.Setoguchi                  //


        //****************************************************************
        //  EccentricCorrectionParam
        //----------------------------------------------------------------
        //'プロパティ
        public int AngleType;               //As ANGLE_TYPE '角度種別。
        public string Marker;               //As String '方位標(基線ベクトルのキー)。
        public double Horizontal;           //As Double '水平角(ラジアン)。
        public double Direction;            //As Double '方位角(ラジアン)。
        public double Distance;             //As Double '斜距離(ｍ)。
        public bool UseTS;                  //As Boolean '測距儀を使用。
        public double FromHeightTS;         //As Double '測距儀高(ｍ)。
        public double ToHeightTS;           //As Double '反射鏡高(ｍ)。
        public double ElevationBC;          //As Double '高度角B→C(ラジアン)。
        public double FromHeightBC;         //As Double '器械高B→C(ｍ)。
        public double ToHeightBC;           //As Double '目標高B→C(ｍ)。
        public double ElevationCB;          //As Double '高度角C→B(ラジアン)。
        public double FromHeightCB;         //As Double '器械高C→B(ｍ)。
        public double ToHeightCB;           //As Double '目標高C→B(ｍ)。
        //--------------------------------------
        public string UsePoint;             //As String '偏心に使用する基線(基線ベクトルのキー)。'2009/11 H.Nakamura
        //--------------------------------------
        //'2007/3/15 追加 NGS Yamada
        public string MarkNumber;           //As String  '手入力時の方位標のNo
        public string MarkName;             //As String  '手入力時の方位標の名称
        public int MarkEditCode;            //As EDITCODE_STYLE '編集コード。0の時はデフォルト。
        public double MarkLat;              //As Double '緯度(度)。
        public double MarkLon;              //As Double '経度(度)。
        public double MarkX;                //As Double '平面直角X(ｍ)。
        public double MarkY;                //As Double '平面直角Y(ｍ)。
        public double MarkHeight;           //As Double '楕円体高(ｍ)。
        public double MarkAlt;              //As Double    '標高(ｍ)。


        //****************************************************************
        //  CoordinatePointXYZ
        //----------------------------------------------------------------
        //23/12/20 K.Setoguchi---->>>>                  //
        //(del)     'インプリメンテーション
        //(del)     public double CPXYZ_m_nX;                //As Double 'X。
        //(del)     public double CPXYZ_m_nY;                //As Double 'Y。
        //(del)     public double CPXYZ_m_nZ;                //As Double 'Z。
        //(del)     public double CPXYZ_m_nRoundX;           //As Double '丸めX。
        //(del)     public double CPXYZ_m_nRoundY;           //As Double '丸めY。
        //(del)     public double CPXYZ_m_nRoundZ;           //As Double '丸めZ。
        //(del)     public int CPXYZ_m_nCoordinateType;      //As COORDINATE_TYPE '座標値種別。
        //****************************************************************
        public CPXYZ_STRUCT CPXYZ;        //CoordinatePointXYZ
        //<<<<----23/12/20 K.Setoguchi


        //****************************************************************
        //  ObservationPoint
        //----------------------------------------------------------------
        //'プロパティ
        public long WorkKey;                        //As Long '汎用作業キー。
        //  public WorkObject;                      //As Object '汎用作業オブジェクト。
        public long OP_ObjectType;                     //As Long 'オブジェクト種別。
        //  public Owner As Object '所有者。接合観測点の場合、基線ベクトルが保持される。代表観測点の場合、チェーンリストが保持される。

        //'インプリメンテーション
        //public m_clsAttributes As New ObservationPointAttributes '観測点属性。
        //public m_clsObservationShared As ObservationShared '観測共有情報。
        //public m_clsParentPoint As ObservationPoint '親観測点。
        //public m_clsChildPoint As ObservationPoint '子観測点。
        //public m_clsPrevPoint As ObservationPoint '兄観測点。
        //public m_clsNextPoint As ObservationPoint '弟観測点。
        //public m_clsCorrectPoint As ObservationPoint '補正点。自オブジェクトが本点の場合は偏心点(HeadPoint)が設定される。自オブジェクトが偏心点の場合は本点(実観測点)が設定される。※相互参照なので Attributes や CommonAttributes のメンバにしてはいけない。
        public bool m_bEnable;                      //As Boolean '有効フラグ。True=有効。False=無効。

        //'*******************************************************************************
        //  ObservationPointAttributes
        //'*******************************************************************************
        public OPA_STRUCT OPA;                  //共通OPAデータ（処理後、OPA --> OPA_Str / OPA_End）

        //23/12/20 K.Setoguchi---->>>>
        //---------------------------------------                                  
        //        public OPA_STRUCT OPA_Str;                  //開始OPAデータ情報        //23/12/20 K.Setoguchi
        //        public OPA_STRUCT OPA_End;                  //終了OPAデータ情報        //23/12/20 K.Setoguchi
        //---------------------------------------                                  
        public OPA_STRUCT_SUB OPA_ListWork;         //共通OPAデータ詳細情報    //23/12/20 K.Setoguchi
        public List<OPA_STRUCT_SUB> OPA_ListStr;    //->開始OPAデータ詳細情報    //23/12/20 K.Setoguchi  
        public List<OPA_STRUCT_SUB> OPA_ListEnd;    //->終了OPAデータ詳細情報    //23/12/20 K.Setoguchi
        //---------------------------------------                                  
        //<<<<----23/12/20 K.Setoguchi




        //23/12/20 K.Setoguchi---->>>>
        //'*******************************************************************************
        //  DepPattern Load                 位相パターンの標高
        //'*******************************************************************************
        public List<NSPPS3_DEP_PATTERN> m_tDepPattern;
        //<<<<----23/12/20 K.Setoguchi

        //23/12/20 K.Setoguchi---->>>>
        //'*******************************************************************************
        //  DepDataMask Load                衛星情報マスク
        //'*******************************************************************************
        public List<MaskInfo> m_tMaskInfos;         //衛星（衛星番号
        public List<MaskInfo_T> m_tMaskInfos_T;     //衛星（マスク開始時間/マスク終了時間
        //<<<<----23/12/20 K.Setoguchi


    }
    //23/12/20 K.Setoguchi---->>>>
    //'*******************************************************************************      
    //  DepPattern Load 構造体         位相パターンの標高
    //'*******************************************************************************      
    public struct NSPPS3_DEP_PATTERN
    {
        public Single PatternEle;   //PatternEle As Single 'elevations for phase pattern (deg)
        public Single PatternL1;    //PatternL1 As Single 'phase center pattern for L1 (meters)
        public Single PatternL2;    //PatternL2 As Single 'phase center pattern for L2 (meters)
        public Single PatternL5;    //PatternL5 As Single 'phase center pattern for L5 (meters)
    }

    //<<<<----23/12/20 K.Setoguchi


    //23/12/20 K.Setoguchi---->>>>
    //'*******************************************************************************      
    //  DepDataMask Load 構造体        衛星情報マスク（衛星番号・マスク開始時間/マスク終了時間）
    //'*******************************************************************************      
    //'マスク情報。
    public struct MaskInfo
    {
        public long Number;             //As Long '衛星番号。
                                        //  '1～32＝GPSの1～32番
                                        //  '38～63＝GLONASSの1～26番
                                        //  '65～72＝QZSSの1～8番
                                        //  '73～102＝Galileoの1～30番
                                        //  '105～139＝BeiDouの1～35番
                                        //  '140～198＝SBASの1～59番
                                        //  '199≦不明。
        public bool Enabled;            //As Boolean '有効フラグ。
        public DateTime[] StrTimes;     //'マスク開始時間(以上)。配列の要素は(-1 To ...)、要素 -1 は未使用。
        public DateTime[] EndTimes;     //'マスク終了時間(以下)。配列の要素は(-1 To ...)、要素 -1 は未使用。
                                        //---------------------------
        public long nUBound;            //(@NV)=-1:無し / =1以上は、(MaskInfo_T)マスク開始時間/マスク終了時間
    }
    //-----------------------------------
    public struct MaskInfo_T
    {
        public DateTime StrTimes;      //()As Date 'マスク開始時間(以上)。配列の要素は(-1 To ...)、要素 -1 は未使用。
        public DateTime EndTimes;      //()As Date 'マスク終了時間(以下)。配列の要素は(-1 To ...)、要素 -1 は未使用。

//        public static implicit operator MaskInfo_T(int v)
//        {
//            throw new NotImplementedException();
//        }
    }
    //<<<<----23/12/20 K.Setoguchi


    //23/12/20 K.Setoguchi---->>>>
    //'*******************************************************************************
    //  ObservationPointAttributes Load 構造体
    //'*******************************************************************************
    //<<<<----23/12/20 K.Setoguchi
    public struct OPA_STRUCT
    {
        //'*******************************************************************************
        //  ObservationPointAttributes
        //'*******************************************************************************
        //'観測点属性
        //ption Explicit
        //
        //'プロパティ
        public int Mode;                //As OBJ_MODE '観測点モード。
        public string Session;          //As String 'セッション名。
        public bool ProvisionalSession; //As Boolean '仮のセッション。
        public string FileTitle;        //As String 'ファイルタイトル。
        public string RinexExt;         //As String 'RINEXファイル拡張子(先頭２文字)。
        public string SrcPath;          //As String 'ソースファイルのパス。
        public DateTime StrTimeGPS;     //As Date '観測開始日時(GPS)。
        public DateTime EndTimeGPS;     //As Date '観測終了日時(GPS)。
        public long LeapSeconds;        //As Long 'うるう秒。
        public double Interval;         //As Double '間隔(秒)。
        public string RecType;          //As String '受信機名称。
        public string RecNumber;        //As String '受信機シリアル。
        public string AntType;          //As String 'アンテナ種別。
        public string AntNumber;        //As String 'アンテナシリアル番号。
        public string AntMeasurement;   //As String 'アンテナ測位方法。
        public double AntHeight;        //As Double 'アンテナ高(ｍ)。
        public double ElevationMask;    //As Double '仰角マスク(度)。
        public long NumberOfMinSV;      //As Long '最少衛星数。
                                        //    public SatelliteInfo As Object '衛星数情報。
        public int ImportType;          //As IMPORT_TYPE 'インポートファイルの種別。
        public bool GlonassFlag;        //As Boolean 'GLONASSフラグ。
                                        //'2017/06/06 NS6000対応。'''''''''''''''''''''''''''''''''''''''''''''''''''''''
        public bool QZSSFlag;           //As Boolean 'QZSSフラグ。
        public bool GalileoFlag;        //As Boolean 'Galileoフラグ。
        public bool BeiDouFlag;         //As Boolean 'BeiDouフラグ。
        public bool MixedNav;           //As Boolean '混合タイプの暦ファイルか？
        public long RinexVersion;       //As Long 'RINEXファイルのバージョン。バージョン番号を1000倍した整数。
                                        //'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        public bool IsList;             //As Boolean 'リスト更新必要フラグ。
        public long ElevationMaskHand;  //As Long '手簿に出力する最低高度角(度)。負の値の場合OFFとする。
        public long NumberOfMinSVHand;  //As Long '手簿に出力する最少衛星数。0の場合はオプション設定から取得。1～12が最少衛星数。
                                        //'2022/02/07 SattSignal の追加。''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        public long SattSignalGPS;           //'GPS衛星信号。ビットフラグ。0x01＝L1、0x02＝L2、0x04＝L5。
        public long SattSignalGLONASS;       //'GLONASS衛星信号。ビットフラグ。0x01＝G1、0x02＝G2、0x04＝G3。
        public long SattSignalQZSS;          //'QZSS衛星信号。ビットフラグ。0x01＝L1、0x02＝L2、0x04＝L5。
        public long SattSignalGalileo;       //'Galileo衛星信号。ビットフラグ。0x01＝E1、0x02＝E5。
        public long SattSignalBeiDou;        //'BeiDou衛星信号。ビットフラグ。0x01＝B1、0x02＝B2、0x04＝B3。
                                             //'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    }

    //23/12/20 K.Setoguchi---->>>>

    public struct OPA_STRUCT_SUB
    {
        //****************************************************************
        //  ObservationPoint
        //----------------------------------------------------------------

        public OPA_STRUCT OPA;                  //共通OPAデータ（処理後、OPA --> OPA_Str / OPA_End）

        public OCA_STRUCT OCA;                  //ObservationCommonAttributes

        public OPFix_STRUCT OPFix;              //CoordinatePointFix

        public CPXYZ_STRUCT CPXYZ;              //CoordinatePointXYZ

        //'プロパティ
        public long WorkKey;                        //As Long '汎用作業キー。
        //  public WorkObject;                      //As Object '汎用作業オブジェクト。
        public long OP_ObjectType;                     //As Long 'オブジェクト種別。
        //  public Owner As Object '所有者。接合観測点の場合、基線ベクトルが保持される。代表観測点の場合、チェーンリストが保持される。

        //'インプリメンテーション
        //public m_clsAttributes As New ObservationPointAttributes '観測点属性。
        //public m_clsObservationShared As ObservationShared '観測共有情報。
        //public m_clsParentPoint As ObservationPoint '親観測点。
        //public m_clsChildPoint As ObservationPoint '子観測点。
        //public m_clsPrevPoint As ObservationPoint '兄観測点。
        //public m_clsNextPoint As ObservationPoint '弟観測点。
        //public m_clsCorrectPoint As ObservationPoint '補正点。自オブジェクトが本点の場合は偏心点(HeadPoint)が設定される。自オブジェクトが偏心点の場合は本点(実観測点)が設定される。※相互参照なので Attributes や CommonAttributes のメンバにしてはいけない。
        public bool m_bEnable;                      //As Boolean '有効フラグ。True=有効。False=無効。
                                                    //----------------------------------------------------------------    
        public bool bFirst;                         //    '長男フラグ｡ 
        public bool bParent;                        //    '親フラグ。
                                                    //----------------------------------------------------------------    
    }
    //<<<<----23/12/20 K.Setoguchi

    //23/12/20 K.Setoguchi---->>>>
    public struct OCA_STRUCT        //ObservationCommonAttributes
    {
        //****************************************************************
        //  ObservationCommonAttributes
        //----------------------------------------------------------------
        public string Number;               //As String '番号。
        public string Name;                 //As String '名称。
        public string GenuineNumber;        //As String '本点番号。
        public string GenuineName;          //As String '本点名称。
        public long ObjectType;             //As Long 'オブジェクト種別。
        public int OldEditCode;             //As EDITCODE_STYLE '旧編集コード。
                                            //****************************************************************
    }
    //<<<<----23/12/20 K.Setoguchi

    //23/12/20 K.Setoguchi---->>>>
    public struct OPFix_STRUCT        //CoordinatePointFix
    {
        //****************************************************************
        //  CoordinatePointFix
        //----------------------------------------------------------------
        //'インプリメンテーション
        public double m_nX;               //As Double 'X。
        public double m_nY;               //As Double 'Y。
        public double m_nZ;               //As Double 'Z。
        public double m_nRoundX;          //As Double '丸めX。
        public double m_nRoundY;          //As Double '丸めY。
        public double m_nRoundZ;          //As Double '丸めZ。
        public double m_nHeight;          //As Double '楕円体高。
        public double m_nCoordinateType;  //As COORDINATE_TYPE '座標値種別。
        public int m_nEditCode;        //As EDITCODE_STYLE '編集コード。
        public string m_sEditX;            //As String '入力X。
        public string m_sEditY;            //As String '入力Y。
        public string m_sEditZ;            //As String '入力Z。
        public string m_sEditLat;          //As String '入力緯度。
        public string m_sEditLon;          //As String '入力経度。
        public string m_sEditHeight;       //As String '入力高さ(ｍ)。
        public long m_nEditCoordNum;       //As Long '入力座標系番号(1～19)。
                                           //'セミ・ダイナミック変換'2009/11 H.Nakamura
        public double m_nSemiDynaCounter;  //As Long 'セミ・ダイナミック補正変換番号。0 は未計算。
        public double m_nLatKON;           //As Double '今期緯度(度)。
        public double m_nLonKON;           //As Double '今期経度(度)。
        public double m_nHeightKON;        //As Double '今期楕円体高(ｍ)。
        public double m_nXKON;             //As Double '今期X。
        public double m_nYKON;             //As Double '今期Y。
        public double m_nZKON;             //As Double '今期Z。
        public double m_nRoundXKON;        //As Double '今期丸めX。
        public double m_nRoundYKON;        //As Double '今期丸めY。
        public double m_nRoundZKON;        //As Double '今期丸めZ。
    }
    //<<<<----23/12/20 K.Setoguchi

    //23/12/20 K.Setoguchi---->>>>
    public struct CPXYZ_STRUCT        //CoordinatePointXYZ
    {
        //****************************************************************
        //  CoordinatePointXYZ
        //----------------------------------------------------------------
        //'インプリメンテーション
        public double m_nX;                //As Double 'X。
        public double m_nY;                //As Double 'Y。
        public double m_nZ;                //As Double 'Z。
        public double m_nRoundX;           //As Double '丸めX。
        public double m_nRoundY;           //As Double '丸めY。
        public double m_nRoundZ;           //As Double '丸めZ。
        public int m_nCoordinateType;      //As COORDINATE_TYPE '座標値種別。
    }
    //<<<<----23/12/20 K.Setoguchi

}
