using SurvLine.mdl;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static SurvLine.mdl.MdlBaseLineAnalyser;
using static SurvLine.mdl.MdlNSDefine;
using static SurvLine.mdl.MdlNSSDefine;
using static SurvLine.mdl.MdlNSUtility;
using static SurvLine.mdl.MdlNSSUtility;
using static SurvLine.mdl.MdlUtility;
using static SurvLine.mdl.MdlAccountMake;
using static SurvLine.mdl.DEFINE;
using static System.Collections.Specialized.BitVector32;
using static System.Net.Mime.MediaTypeNames;
using static System.Net.WebRequestMethods;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace SurvLine
{
    public class BaseLineVector
    {

        ////Document document = new Document();
        //MdlNSSDefine mdlNSSDefine = new MdlNSSDefine();

        public BaseLineVector(MdlMain mdlMain)
        {
            this.mdlMain = mdlMain;
            document = mdlMain.GetDocument();
            m_clsStrPoint = new ObservationPoint(mdlMain);          //'始点(接合観測点)。
            m_clsEndPoint = new ObservationPoint(mdlMain);          //'終点(接合観測点)。
            m_clsObsDataMask = new ObsDataMask(mdlMain);            //'観測データマスク。


            Class_Initialize();
        }


        //==========================================================================================
        /*[VB]
        '*******************************************************************************
        '基線ベクトル

        Option Explicit
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'プロパティ
        Public ObjectType As Long 'オブジェクト種別。
        Public Session As String 'セッション名。
        Public StrTimeGPS As Date '観測開始日時(GPS)。
        Public EndTimeGPS As Date '観測終了日時(GPS)。
        Public WorkKey As Long '汎用作業キー。
        Public WorkObject As Object '汎用作業オブジェクト。
        Public Exclusion As Boolean '解析除外フラグ。True=除外。False=解析。
        Public Analysis As ANALYSIS_STATUS '解析状態。
        Public Orbit As ORBIT_TYPE '軌道暦。
        Public Frequency As FREQUENCY_TYPE '周波数。
        Public SolveMode As SOLVEMODE_TYPE '基線解析モード。
        Public AnalysisStrTimeGPS As Date '解析開始日時(GPS)。
        Public AnalysisEndTimeGPS As Date '解析終了日時(GPS)。
        Public ElevationMask As Double '仰角マスク(度)。
        Public Interval As Double '解析間隔(秒)。
        Public Temperature As Double '気温(℃)。
        Public Pressure As Double '気圧(hPa)。
        Public Humidity As Double '湿度(％)。
        Public Troposhere As TROPOSHERE_TYPE '対流圏モデル。
        Public AnalysisFixed As Boolean '解析始点固定点フラグ。True=解析始点が固定点。False=解析始点は固定点で無い。
        Public AmbPercentage As Long 'FIX率(％)。
        Public Bias As Double 'バイアス決定比(ｍ)。
        Public EpochUsed As Long '使用エポック数。
        Public EpochRejected As Long '棄却エポック数。
        Public RMS As Double 'RMS値(ｍ)。
        Public RDOP As Double 'RDOP値。
        Public IsDispersion As Boolean '分散・共分散の有効/無効。True=有効。False=無効。
        Public RcvNumbersGps As Long '受信GPS衛星番号。ビットフラグ。1=受信。0=非受信。ビット0がGPS1番、ビット1がGPS2番。。。。
        Public RcvNumbersGlonass As Long '受信GLONASS衛星番号。ビットフラグ。1=受信。0=非受信。ビット0がR1番、ビット1がR2番。。。。
        '2017/06/06 NS6000対応。'''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Public RcvNumbersQZSS As Long '受信QZSS衛星番号。ビットフラグ。1=受信。0=非受信。ビット0がJ1番、ビット1がRJ番。。。。
        'Public RcvNumbersGalileo As Long '受信Galileo衛星番号。ビットフラグ。1=受信。0=非受信。ビット0がE1番、ビット1がE2番。。。。    '2018/08/21 Hitz H.Nakamura 衛星数が増えたので64ビットに増やす。
        Public RcvNumbersGalileo1 As Long '受信Galileo衛星番号。ビットフラグ。1=受信。0=非受信。ビット0がE1番、ビット1がE2番。。。。
        Public RcvNumbersGalileo2 As Long '受信Galileo衛星番号。ビットフラグ。1=受信。0=非受信。ビット0がE33番、ビット1がE34番。。。。
        Public RcvNumbersBeiDou1 As Long '受信BeiDou1衛星番号。ビットフラグ。1=受信。0=非受信。ビット0がC1番、ビット1がC2番。。。。
        Public RcvNumbersBeiDou2 As Long '受信BeiDou2衛星番号。ビットフラグ。1=受信。0=非受信。ビット0がC33番、ビット1がC34番。。。。
        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Public MaxResidL1_Legacy As Double '最大残差L1。
        Public MaxResidL2_Legacy As Double '最大残差L2。
        Public MinRatio As Double '最小バイアス決定比。
        Public MinPs As Double '最小 probability of success。
        Public ExcludeGPS As Long '不使用GPS。ビットフラグ。1=使用。0=無使用。ビット0がGPS1番、ビット1がGPS2番。。。。
        Public ExcludeGlonass As Long '不使用GLONASS。ビットフラグ。1=使用。0=無使用。ビット0がR1番、ビット1がR2番。。。。
        Public GlonassFlag As Boolean 'GLONASSフラグ。
        '2017/06/06 NS6000対応。'''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Public ExcludeQZSS As Long '不使用QZSS。ビットフラグ。1=使用。0=無使用。ビット0がJ1番、ビット1がJ2番。。。。
        'Public ExcludeGalileo As Long '不使用Galileo。ビットフラグ。1=使用。0=無使用。ビット0がE1番、ビット1がE2番。。。。 '2018/08/21 Hitz H.Nakamura 衛星数が増えたので64ビットに増やす。
        Public ExcludeGalileo1 As Long '不使用Galileo。ビットフラグ。1=使用。0=無使用。ビット0がE1番、ビット1がE2番。。。。
        Public ExcludeGalileo2 As Long '不使用Galileo。ビットフラグ。1=使用。0=無使用。ビット0がE33番、ビット1がE34番。。。。
        Public ExcludeBeiDou1 As Long '不使用BeiDou。ビットフラグ。1=使用。0=無使用。ビット0がC1番、ビット1がC2番。。。。
        Public ExcludeBeiDou2 As Long '不使用BeiDOu。ビットフラグ。1=使用。0=無使用。ビット0がC33番、ビット1がC34番。。。。
        Public QZSSFlag As Boolean 'QZSSフラグ。
        Public GalileoFlag As Boolean 'Galileoフラグ。
        Public BeiDouFlag As Boolean 'BeiDouフラグ。
        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Public IsList As Boolean 'リスト更新必要フラグ。
        Public StrPcvVer As Variant '始点PCVバージョン。補正無しの場合は空文字。不明の場合は Null。
        Public EndPcvVer As Variant '終点PCVバージョン。補正無しの場合は空文字。不明の場合は Null。
        Public AnalysisOrder As Long '解析順番。より数値が小さい方を後に解析されたこととする。第３２ビットは符号ビットとして、第３１～２９は一時的な順位の重み付けに使う。第２８ビットは解析㊥、非解析の区別に使う。第２７～１ビットで順番をあらわす。つまり、最低順位は &H0FFFFFFF となる。
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'プロパティ
        public long ObjectType;                //'オブジェクト種別。
        public string Session;                 //'セッション名。
        public DateTime StrTimeGPS;            //'観測開始日時(GPS)。
        public DateTime EndTimeGPS;            //'観測終了日時(GPS)。
        public long WorkKey;                   //'汎用作業キー。
        public object WorkObject;              //'汎用作業オブジェクト。
        public bool Exclusion;                 //'解析除外フラグ。True=除外。False=解析。
        public ANALYSIS_STATUS Analysis;       //'解析状態。
        public ORBIT_TYPE Orbit;               //'軌道暦。
        public FREQUENCY_TYPE Frequency;       //'周波数。
        public SOLVEMODE_TYPE SolveMode;       //'基線解析モード。
        public DateTime AnalysisStrTimeGPS;    //'解析開始日時(GPS)。
        public DateTime AnalysisEndTimeGPS;    //'解析終了日時(GPS)。
        public double ElevationMask;           //'仰角マスク(度)。
        public double Interval;                //'解析間隔(秒)。
        public double Temperature;             //'気温(℃)。
        public double Pressure;                //'気圧(hPa)。
        public double Humidity;                //'湿度(％)。
        private TROPOSHERE_TYPE Troposhere;     //'対流圏モデル。
        public bool AnalysisFixed;             //'解析始点固定点フラグ。True=解析始点が固定点。False=解析始点は固定点で無い。
        public long AmbPercentage;             //'FIX率(％)。
        public double Bias;                    //'バイアス決定比(ｍ)。
        public long EpochUsed;                 //'使用エポック数。
        public long EpochRejected;             //'棄却エポック数。
        public double RMS;                     //'RMS値(ｍ)。
        public double RDOP;                    //'RDOP値。
        public bool IsDispersion;              //'分散・共分散の有効/無効。True=有効。False=無効。
        public long RcvNumbersGps;             //'受信GPS衛星番号。ビットフラグ。1=受信。0=非受信。ビット0がGPS1番、ビット1がGPS2番。。。。
        public long RcvNumbersGlonass;         //'受信GLONASS衛星番号。ビットフラグ。1=受信。0=非受信。ビット0がR1番、ビット1がR2番。。。。
        //'2017/06/06 NS6000対応。'''''''''''''''''''''''''''''''''''''''''''''''''''''''
        public long RcvNumbersQZSS;            //'受信QZSS衛星番号。ビットフラグ。1=受信。0=非受信。ビット0がJ1番、ビット1がRJ番。。。。
        //'Public RcvNumbersGalileo As Long '受信Galileo衛星番号。ビットフラグ。1=受信。0=非受信。ビット0がE1番、ビット1がE2番。。。。    '2018/08/21 Hitz H.Nakamura 衛星数が増えたので64ビットに増やす。
        public long RcvNumbersGalileo1;        //'受信Galileo衛星番号。ビットフラグ。1=受信。0=非受信。ビット0がE1番、ビット1がE2番。。。。
        public long RcvNumbersGalileo2;        //'受信Galileo衛星番号。ビットフラグ。1=受信。0=非受信。ビット0がE33番、ビット1がE34番。。。。
        public long RcvNumbersBeiDou1;         //'受信BeiDou1衛星番号。ビットフラグ。1=受信。0=非受信。ビット0がC1番、ビット1がC2番。。。。
        public long RcvNumbersBeiDou2;         //'受信BeiDou2衛星番号。ビットフラグ。1=受信。0=非受信。ビット0がC33番、ビット1がC34番。。。。
        //'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        public double MaxResidL1_Legacy;       //'最大残差L1。
        public double MaxResidL2_Legacy;       //'最大残差L2。
        public double MinRatio;                //'最小バイアス決定比。
        public double MinPs;                   //'最小 probability of success。
        public long ExcludeGPS;                //'不使用GPS。ビットフラグ。1=使用。0=無使用。ビット0がGPS1番、ビット1がGPS2番。。。。
        public long ExcludeGlonass;            //'不使用GLONASS。ビットフラグ。1=使用。0=無使用。ビット0がR1番、ビット1がR2番。。。。
        public bool GlonassFlag;               //'GLONASSフラグ。
        //'2017/06/06 NS6000対応。'''''''''''''''''''''''''''''''''''''''''''''''''''''''
        public long ExcludeQZSS;               //'不使用QZSS。ビットフラグ。1=使用。0=無使用。ビット0がJ1番、ビット1がJ2番。。。。
        //'Public ExcludeGalileo As Long '不使用Galileo。ビットフラグ。1=使用。0=無使用。ビット0がE1番、ビット1がE2番。。。。 '2018/08/21 Hitz H.Nakamura 衛星数が増えたので64ビットに増やす。
        public long ExcludeGalileo1;           //'不使用Galileo。ビットフラグ。1=使用。0=無使用。ビット0がE1番、ビット1がE2番。。。。
        public long ExcludeGalileo2;           //'不使用Galileo。ビットフラグ。1=使用。0=無使用。ビット0がE33番、ビット1がE34番。。。。
        public long ExcludeBeiDou1;            //'不使用BeiDou。ビットフラグ。1=使用。0=無使用。ビット0がC1番、ビット1がC2番。。。。
        public long ExcludeBeiDou2;            //'不使用BeiDOu。ビットフラグ。1=使用。0=無使用。ビット0がC33番、ビット1がC34番。。。。
        public bool QZSSFlag;                  //'QZSSフラグ。
        public bool GalileoFlag;               //'Galileoフラグ。
        public bool BeiDouFlag;                //'BeiDouフラグ。
        //'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        public bool IsList;                     //'リスト更新必要フラグ。
        public string StrPcvVer;                //'始点PCVバージョン。補正無しの場合は空文字。不明の場合は Null。
        public string EndPcvVer;                //'終点PCVバージョン。補正無しの場合は空文字。不明の場合は Null。
        private long AnalysisOrder;             //'解析順番。より数値が小さい方を後に解析されたこととする。第３２ビットは符号ビットとして、第３１～２９は一時的な順位の重み付けに使う。第２８ビットは解析㊥、非解析の区別に使う。第２７～１ビットで順番をあらわす。つまり、最低順位は &H0FFFFFFF となる。
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'インプリメンテーション
        Private m_clsStrPoint As New ObservationPoint '始点(接合観測点)。
        Private m_clsEndPoint As New ObservationPoint '終点(接合観測点)。
        Private m_clsAnalysisStrPoint As ObservationPoint '解析始点(接合観測点)。
        Private m_clsAnalysisEndPoint As ObservationPoint '解析終点(接合観測点)。
        Private m_clsCoordinateAnalysis As CoordinatePoint '解析座標。
        Private m_clsVectorAnalysis As New CoordinatePointXYZ '解析ベクトル。
        Private m_clsDispersion As New Dispersion '分散・共分散。
        Private m_clsStrDepPattern As New DepPattern '始点アンテナ位相補正パターン。
        Private m_clsEndDepPattern As New DepPattern '終点アンテナ位相補正パターン。
        Private m_clsObsInfo As New ObsInfo '観測情報。
        Private m_clsAmbInfo As New AmbInfo 'アンビギュイティ情報。
        Private m_clsStrOffsetL1 As New CoordinatePointXYZ '始点アンテナ位相補正L1。
        Private m_clsStrOffsetL2 As New CoordinatePointXYZ '始点アンテナ位相補正L2。
        Private m_clsStrOffsetL5 As New CoordinatePointXYZ '始点アンテナ位相補正L5。
        Private m_clsEndOffsetL1 As New CoordinatePointXYZ '終点アンテナ位相補正L1。
        Private m_clsEndOffsetL2 As New CoordinatePointXYZ '終点アンテナ位相補正L2。
        Private m_clsEndOffsetL5 As New CoordinatePointXYZ '終点アンテナ位相補正L5。
        Private m_clsObsDataMask As New ObsDataMask '観測データマスク。
        Private m_nLineType As OBJ_MODE '基線ベクトル種類。
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'インプリメンテーション
        //private ObservationPoint m_clsStrPoint = new ObservationPoint();            //'始点(接合観測点)。
        //private ObservationPoint m_clsEndPoint = new ObservationPoint();            //'終点(接合観測点)。
        private ObservationPoint m_clsStrPoint;                                     //'始点(接合観測点)。
        private ObservationPoint m_clsEndPoint;                                     //'終点(接合観測点)。
        private ObservationPoint m_clsAnalysisStrPoint;                             //'解析始点(接合観測点)。
        private ObservationPoint m_clsAnalysisEndPoint;                             //'解析終点(接合観測点)。
        private CoordinatePoint m_clsCoordinateAnalysis;                            //'解析座標。
        private CoordinatePointXYZ m_clsVectorAnalysis = new CoordinatePointXYZ();  //'解析ベクトル。
        private Dispersion m_clsDispersion = new Dispersion();                      //'分散・共分散。
        private DepPattern m_clsStrDepPattern = new DepPattern();                   //'始点アンテナ位相補正パターン。
        private DepPattern m_clsEndDepPattern = new DepPattern();                   //'終点アンテナ位相補正パターン。
        private ObsInfo m_clsObsInfo = new ObsInfo();                               //'観測情報。
        private AmbInfo m_clsAmbInfo = new AmbInfo();                               //'アンビギュイティ情報。
        private CoordinatePointXYZ m_clsStrOffsetL1 = new CoordinatePointXYZ();     //'始点アンテナ位相補正L1。
        private CoordinatePointXYZ m_clsStrOffsetL2 = new CoordinatePointXYZ();     //'始点アンテナ位相補正L2。
        private CoordinatePointXYZ m_clsStrOffsetL5 = new CoordinatePointXYZ();     //'始点アンテナ位相補正L5。
        private CoordinatePointXYZ m_clsEndOffsetL1 = new CoordinatePointXYZ();     //'終点アンテナ位相補正L1。
        private CoordinatePointXYZ m_clsEndOffsetL2 = new CoordinatePointXYZ();     //'終点アンテナ位相補正L2。
        private CoordinatePointXYZ m_clsEndOffsetL5 = new CoordinatePointXYZ();     //'終点アンテナ位相補正L5。
        //private ObsDataMask m_clsObsDataMask = new ObsDataMask();                   //'観測データマスク。
        private ObsDataMask m_clsObsDataMask;                                       //'観測データマスク。
        private OBJ_MODE m_nLineType;                                               //'基線ベクトル種類。
        private Document document;
        private MdlMain mdlMain;
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '*******************************************************************************
        'プロパティ

        'うるう秒。
        Property Get LeapSeconds() As Long
            LeapSeconds = m_clsStrPoint.LeapSeconds
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '*******************************************************************************
        'プロパティ

        'うるう秒。
        */
        public long LeapSeconds()
        {
            return m_clsStrPoint.LeapSeconds();
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '観測開始日時(UTC)。
        Property Get StrTimeUTC() As Date
            StrTimeUTC = GetTimeFromGPS(StrTimeGPS, m_clsStrPoint.LeapSeconds, TIME_ZONE_UTC)
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'観測開始日時(UTC)。
        public DateTime StrTimeUTC()
        {
            return GetTimeFromGPS(StrTimeGPS, m_clsStrPoint.LeapSeconds(), TIME_ZONE.TIME_ZONE_UTC);
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '観測終了日時(UTC)。
        Property Get EndTimeUTC() As Date
            EndTimeUTC = GetTimeFromGPS(EndTimeGPS, m_clsStrPoint.LeapSeconds, TIME_ZONE_UTC)
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'観測終了日時(UTC)。
        public DateTime EndTimeUTC()
        {
            return GetTimeFromGPS(EndTimeGPS, m_clsStrPoint.LeapSeconds(), TIME_ZONE.TIME_ZONE_UTC);
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '観測開始日時(JST)。
        Property Get StrTimeJST() As Date
            StrTimeJST = GetTimeFromGPS(StrTimeGPS, m_clsStrPoint.LeapSeconds, TIME_ZONE_JST)
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'観測開始日時(JST)。
        public DateTime StrTimeJST()
        {
            return GetTimeFromGPS(StrTimeGPS, m_clsStrPoint.LeapSeconds(), TIME_ZONE.TIME_ZONE_JST);
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '観測終了日時(JST)。
        Property Get EndTimeJST() As Date
            EndTimeJST = GetTimeFromGPS(EndTimeGPS, m_clsStrPoint.LeapSeconds, TIME_ZONE_JST)
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'観測終了日時(JST)。
        public DateTime EndTimeJST()
        {
            return GetTimeFromGPS(EndTimeGPS, m_clsStrPoint.LeapSeconds(), TIME_ZONE.TIME_ZONE_JST);
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '解析開始日時(UTC)。
        Property Get AnalysisStrTimeUTC() As Date
            AnalysisStrTimeUTC = GetTimeFromGPS(AnalysisStrTimeGPS, m_clsStrPoint.LeapSeconds, TIME_ZONE_UTC)
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'解析開始日時(UTC)。
        public DateTime AnalysisStrTimeUTC()
        {
            return GetTimeFromGPS(AnalysisStrTimeGPS, m_clsStrPoint.LeapSeconds(), TIME_ZONE.TIME_ZONE_UTC);
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '解析終了日時(UTC)。
        Property Get AnalysisEndTimeUTC() As Date
            AnalysisEndTimeUTC = GetTimeFromGPS(AnalysisEndTimeGPS, m_clsStrPoint.LeapSeconds, TIME_ZONE_UTC)
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'解析終了日時(UTC)。
        public DateTime AnalysisEndTimeUTC()
        {
            return GetTimeFromGPS(AnalysisEndTimeGPS, m_clsStrPoint.LeapSeconds(), TIME_ZONE.TIME_ZONE_UTC);
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '解析開始日時(JST)。
        Property Get AnalysisStrTimeJST() As Date
            AnalysisStrTimeJST = GetTimeFromGPS(AnalysisStrTimeGPS, m_clsStrPoint.LeapSeconds, TIME_ZONE_JST)
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'解析開始日時(JST)。
        public DateTime AnalysisStrTimeJST()
        {
            return GetTimeFromGPS(AnalysisStrTimeGPS, m_clsStrPoint.LeapSeconds(), TIME_ZONE.TIME_ZONE_JST);
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '解析終了日時(JST)。
        Property Get AnalysisEndTimeJST() As Date
            AnalysisEndTimeJST = GetTimeFromGPS(AnalysisEndTimeGPS, m_clsStrPoint.LeapSeconds, TIME_ZONE_JST)
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'解析終了日時(JST)。
        public DateTime AnalysisEndTimeJST()
        {
            return GetTimeFromGPS(AnalysisEndTimeGPS, m_clsStrPoint.LeapSeconds(), TIME_ZONE.TIME_ZONE_JST);
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '出力開始日時(JST)。
        Property Get OutputStrTimeJST() As Date
            OutputStrTimeJST = GetTimeFromGPS(AnalysisStrTimeGPS, m_clsStrPoint.LeapSeconds, TIME_ZONE_JST)
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'出力開始日時(JST)。
        public DateTime OutputStrTimeJST()
        {
            return GetTimeFromGPS(AnalysisStrTimeGPS, m_clsStrPoint.LeapSeconds(), TIME_ZONE.TIME_ZONE_JST);
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '出力終了日時(JST)。
        Property Get OutputEndTimeJST() As Date
            OutputEndTimeJST = GetTimeFromGPS(AnalysisEndTimeGPS, m_clsStrPoint.LeapSeconds, TIME_ZONE_JST)
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'出力終了日時(JST)。
        public DateTime OutputEndTimeJST()
        {
            return GetTimeFromGPS(AnalysisEndTimeGPS, m_clsStrPoint.LeapSeconds(), TIME_ZONE.TIME_ZONE_JST);
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '始点(接合観測点)。
        Property Get StrPoint() As ObservationPoint
            Set StrPoint = m_clsStrPoint
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'始点(接合観測点)。
        public ObservationPoint StrPoint()
        {
            return m_clsStrPoint;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '終点(接合観測点)。
        Property Get EndPoint() As ObservationPoint
            Set EndPoint = m_clsEndPoint
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'終点(接合観測点)。
        public ObservationPoint EndPoint()
        {
            return m_clsEndPoint;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '解析始点(接合観測点)。
        Property Get AnalysisStrPoint() As ObservationPoint
            Set AnalysisStrPoint = m_clsAnalysisStrPoint
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'解析始点(接合観測点)。
        public ObservationPoint AnalysisStrPoint()
        {
            return m_clsAnalysisStrPoint;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '解析終点(接合観測点)。
        Property Get AnalysisEndPoint() As ObservationPoint
            Set AnalysisEndPoint = m_clsAnalysisEndPoint
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'解析終点(接合観測点)。
        public ObservationPoint AnalysisEndPoint()
        {
            return m_clsAnalysisEndPoint;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '出力始点(接合観測点)。
        Property Get OutputStrPoint() As ObservationPoint
            Set OutputStrPoint = m_clsAnalysisStrPoint
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'出力始点(接合観測点)。
        public ObservationPoint OutputStrPoint()
        {
            return m_clsAnalysisStrPoint;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '出力終点(接合観測点)。
        Property Get OutputEndPoint() As ObservationPoint
            Set OutputEndPoint = m_clsAnalysisEndPoint
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'出力終点(接合観測点)。
        public ObservationPoint OutputEndPoint()
        {
            return m_clsAnalysisEndPoint;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '反転フラグ。True=反転。False=反転無し。
        Property Let Revers(ByVal bRevers As Boolean)
            If bRevers Then
                Set m_clsAnalysisStrPoint = m_clsEndPoint
                Set m_clsAnalysisEndPoint = m_clsStrPoint
            Else
                Set m_clsAnalysisStrPoint = m_clsStrPoint
                Set m_clsAnalysisEndPoint = m_clsEndPoint
            End If
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'反転フラグ。True=反転。False=反転無し。
        public void Revers(bool bRevers)
        {
            if (bRevers)
            {
                m_clsAnalysisStrPoint = m_clsEndPoint;
                m_clsAnalysisEndPoint = m_clsStrPoint;
            }
            else
            {
                m_clsAnalysisStrPoint = m_clsStrPoint;
                m_clsAnalysisEndPoint = m_clsEndPoint;
            }
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '反転フラグ。True=反転。False=反転無し。
        Property Get Revers() As Boolean
            Revers = Not m_clsStrPoint Is m_clsAnalysisStrPoint
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'反転フラグ。True=反転。False=反転無し。
        public bool Revers()
        {
            return m_clsStrPoint != m_clsAnalysisStrPoint;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '解析座標。
        Property Set CoordinateAnalysis(ByVal clsCoordinateAnalysis As CoordinatePoint)
            Set m_clsCoordinateAnalysis = clsCoordinateAnalysis
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'解析座標。
        public void CoordinateAnalysis(CoordinatePoint clsCoordinateAnalysis)
        {
            m_clsCoordinateAnalysis = clsCoordinateAnalysis;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '解析座標。
        Property Get CoordinateAnalysis() As CoordinatePoint
            Set CoordinateAnalysis = m_clsCoordinateAnalysis
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'解析座標。
        public CoordinatePoint CoordinateAnalysis()
        {
            return m_clsCoordinateAnalysis;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '偏心補正後座標。
        Property Get CoordinateCorrect() As CoordinatePoint
            Dim clsCoordinateCorrect As CoordinatePoint
            Dim clsStrPoint As ObservationPoint
            If Analysis <= ANALYSIS_STATUS_FIX Then
                Set clsCoordinateCorrect = CoordinateAnalysis
                Set clsStrPoint = AnalysisStrPoint
            Else
                Set clsStrPoint = StrPoint
                If clsStrPoint.Fixed Then
                    Set clsCoordinateCorrect = clsStrPoint.CoordinateFixed
                Else
                    Set clsCoordinateCorrect = clsStrPoint.CoordinateObservation
                End If
            End If
            If clsStrPoint.EnableEccentric Then
                Dim clsCoordinatePoint As New CoordinatePointXYZ
                clsCoordinatePoint.X = clsCoordinateCorrect.RoundX + clsStrPoint.VectorEccentric.RoundX
                clsCoordinatePoint.Y = clsCoordinateCorrect.RoundY + clsStrPoint.VectorEccentric.RoundY
                clsCoordinatePoint.Z = clsCoordinateCorrect.RoundZ + clsStrPoint.VectorEccentric.RoundZ
                Set clsCoordinateCorrect = clsCoordinatePoint
            End If
            Set CoordinateCorrect = clsCoordinateCorrect
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'偏心補正後座標。
        public CoordinatePoint CoordinateCorrect()
        {
            CoordinatePoint clsCoordinateCorrect;
            ObservationPoint clsStrPoint;
            if (Analysis <= ANALYSIS_STATUS.ANALYSIS_STATUS_FIX)
            {
                clsCoordinateCorrect = CoordinateAnalysis();
                clsStrPoint = AnalysisStrPoint();
            }
            else
            {
                clsStrPoint = StrPoint();
                if (clsStrPoint.Fixed())
                {
                    clsCoordinateCorrect = clsStrPoint.CoordinateFixed();
                }
                else
                {
                    clsCoordinateCorrect = clsStrPoint.CoordinateObservation();
                }
            }
            if (clsStrPoint.EnableEccentric())
            {
                CoordinatePointXYZ clsCoordinatePoint = new CoordinatePointXYZ();
                clsCoordinatePoint.X = clsCoordinateCorrect.RoundXX() + clsStrPoint.VectorEccentric().RoundXX();
                clsCoordinatePoint.Y = clsCoordinateCorrect.RoundYY() + clsStrPoint.VectorEccentric().RoundYY();
                clsCoordinatePoint.Z = clsCoordinateCorrect.RoundZZ() + clsStrPoint.VectorEccentric().RoundZZ();
                clsCoordinateCorrect = clsCoordinatePoint;
            }
            return clsCoordinateCorrect;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '出力始点座標。
        Property Get CoordinateOutputStr() As CoordinatePoint
            Set CoordinateOutputStr = CoordinateCorrect
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'出力始点座標。
        public CoordinatePoint CoordinateOutputStr()
        {
            return CoordinateCorrect();
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '出力終点座標。
        Property Get CoordinateOutputEnd() As CoordinatePoint
            Dim clsCoordinatePoint As New CoordinatePointXYZ
            Let clsCoordinatePoint = CoordinateCorrect
            Dim clsVector As CoordinatePoint
            Set clsVector = VectorCorrect
            clsCoordinatePoint.X = clsCoordinatePoint.RoundX + clsVector.RoundX
            clsCoordinatePoint.Y = clsCoordinatePoint.RoundY + clsVector.RoundY
            clsCoordinatePoint.Z = clsCoordinatePoint.RoundZ + clsVector.RoundZ
            Set CoordinateOutputEnd = clsCoordinatePoint
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'出力終点座標。
        public CoordinatePoint CoordinateOutputEnd()
        {
            //CoordinatePointXYZ clsCoordinatePoint = new CoordinatePointXYZ();
            CoordinatePointXYZ clsCoordinatePoint = (CoordinatePointXYZ)CoordinateCorrect();
            CoordinatePoint clsVector;
            clsVector = VectorCorrect();
            clsCoordinatePoint.X = clsCoordinatePoint.RoundXX() + clsVector.RoundXX();
            clsCoordinatePoint.Y = clsCoordinatePoint.RoundYY() + clsVector.RoundYY();
            clsCoordinatePoint.Z = clsCoordinatePoint.RoundZZ() + clsVector.RoundZZ();
            return clsCoordinatePoint;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '偏心補正前始点座標。
        Property Get CoordinateUncorrectStr() As CoordinatePoint
            Dim clsCoordinatePoint As CoordinatePoint
            If Analysis <= ANALYSIS_STATUS_FIX Then
                Set clsCoordinatePoint = CoordinateAnalysis
            Else
                If StrPoint.Fixed Then
                    Set clsCoordinatePoint = StrPoint.CoordinateFixed
                Else
                    Set clsCoordinatePoint = StrPoint.CoordinateObservation
                End If
            End If
            Set CoordinateUncorrectStr = clsCoordinatePoint
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'偏心補正前始点座標。
        public CoordinatePoint CoordinateUncorrectStr()
        {
            CoordinatePoint clsCoordinatePoint;
            if (Analysis <= ANALYSIS_STATUS.ANALYSIS_STATUS_FIX)
            {
                clsCoordinatePoint = CoordinateAnalysis();
            }
            else
            {
                if (StrPoint().Fixed())
                {
                    clsCoordinatePoint = StrPoint().CoordinateFixed();
                }
                else
                {
                    clsCoordinatePoint = StrPoint().CoordinateObservation();
                }
            }
            return clsCoordinatePoint;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '偏心補正前終点座標。
        Property Get CoordinateUncorrectEnd() As CoordinatePoint
            Dim clsCoordinatePoint As New CoordinatePointXYZ
            Let clsCoordinatePoint = CoordinateUncorrectStr
            Dim clsVector As CoordinatePoint
            If Analysis <= ANALYSIS_STATUS_FIX Then
                Set clsVector = VectorAnalysis
            Else
                Set clsVector = VectorObservation
            End If
            clsCoordinatePoint.X = clsCoordinatePoint.RoundX + clsVector.RoundX
            clsCoordinatePoint.Y = clsCoordinatePoint.RoundY + clsVector.RoundY
            clsCoordinatePoint.Z = clsCoordinatePoint.RoundZ + clsVector.RoundZ
            Set CoordinateUncorrectEnd = clsCoordinatePoint
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'偏心補正前終点座標。
        public CoordinatePoint CoordinateUncorrectEnd()
        {
            //CoordinatePointXYZ clsCoordinatePoint = new CoordinatePointXYZ();
            CoordinatePointXYZ clsCoordinatePoint = (CoordinatePointXYZ)CoordinateUncorrectStr();
            CoordinatePoint clsVector;
            if (Analysis <= ANALYSIS_STATUS.ANALYSIS_STATUS_FIX)
            {
                clsVector = VectorAnalysis();
            }
            else
            {
                clsVector = VectorObservation();
            }
            clsCoordinatePoint.X = clsCoordinatePoint.RoundXX() + clsVector.RoundXX();
            clsCoordinatePoint.Y = clsCoordinatePoint.RoundYY() + clsVector.RoundYY();
            clsCoordinatePoint.Z = clsCoordinatePoint.RoundZZ() + clsVector.RoundZZ();
            return clsCoordinatePoint;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '観測ベクトル。
        Property Get VectorObservation() As CoordinatePoint
            Dim clsVector As New CoordinatePointXYZ
            clsVector.X = m_clsEndPoint.CoordinateObservation.X - m_clsStrPoint.CoordinateObservation.X
            clsVector.Y = m_clsEndPoint.CoordinateObservation.Y - m_clsStrPoint.CoordinateObservation.Y
            clsVector.Z = m_clsEndPoint.CoordinateObservation.Z - m_clsStrPoint.CoordinateObservation.Z
            Set VectorObservation = clsVector
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'観測ベクトル。
        public CoordinatePoint VectorObservation()
        {
            CoordinatePointXYZ clsVector = new CoordinatePointXYZ();
            clsVector.X = m_clsEndPoint.CoordinateObservation().X - m_clsStrPoint.CoordinateObservation().X;
            clsVector.Y = m_clsEndPoint.CoordinateObservation().Y - m_clsStrPoint.CoordinateObservation().Y;
            clsVector.Z = m_clsEndPoint.CoordinateObservation().Z - m_clsStrPoint.CoordinateObservation().Z;
            return clsVector;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '解析ベクトル。
        Public Sub SetVectorAnalysis(ByRef VectorXYZ() As Double)
            m_clsVectorAnalysis.X = VectorXYZ(0)
            m_clsVectorAnalysis.Y = VectorXYZ(1)
            m_clsVectorAnalysis.Z = VectorXYZ(2)
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'解析ベクトル。
        public void SetVectorAnalysis(double[] VectorXYZ)
        {
            m_clsVectorAnalysis.X = VectorXYZ[0];
            m_clsVectorAnalysis.Y = VectorXYZ[1];
            m_clsVectorAnalysis.Z = VectorXYZ[2];
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '解析ベクトル。
        Property Get VectorAnalysis() As CoordinatePoint
            Set VectorAnalysis = m_clsVectorAnalysis
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'解析ベクトル。
        public CoordinatePoint VectorAnalysis()
        {
            return m_clsVectorAnalysis;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '解析ベクトル(NEU)。配列の要素は(2, 0)。
        Property Get VectorAnalysisNEU() As Double()
            Dim dXYZ(2, 0) As Double
            dXYZ(0, 0) = m_clsVectorAnalysis.RoundX
            dXYZ(1, 0) = m_clsVectorAnalysis.RoundY
            dXYZ(2, 0) = m_clsVectorAnalysis.RoundZ
            VectorAnalysisNEU = RotateΔXYZ(m_clsCoordinateAnalysis, dXYZ)
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'解析ベクトル(NEU)。配列の要素は(2, 0)。
        public double[,] VectorAnalysisNEU()
        {
            double[,] dXYZ = new double[2, 0];
            dXYZ[0, 0] = m_clsVectorAnalysis.RoundXX();
            dXYZ[1, 0] = m_clsVectorAnalysis.RoundYY();
            dXYZ[2, 0] = m_clsVectorAnalysis.RoundZZ();
            return RotateΔXYZ(m_clsCoordinateAnalysis, ref dXYZ);
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '偏心補正後ベクトル。
        Property Get VectorCorrect() As CoordinatePoint
            Dim clsVector As New CoordinatePointXYZ
            Dim clsStrPoint As ObservationPoint
            Dim clsEndPoint As ObservationPoint
            If Analysis <= ANALYSIS_STATUS_FIX Then
                Let clsVector = VectorAnalysis
                Set clsStrPoint = AnalysisStrPoint
                Set clsEndPoint = AnalysisEndPoint
            Else
                Let clsVector = VectorObservation
                Set clsStrPoint = StrPoint
                Set clsEndPoint = EndPoint
            End If
            If clsStrPoint.EnableEccentric Then
                clsVector.X = clsVector.RoundX - clsStrPoint.VectorEccentric.RoundX
                clsVector.Y = clsVector.RoundY - clsStrPoint.VectorEccentric.RoundY
                clsVector.Z = clsVector.RoundZ - clsStrPoint.VectorEccentric.RoundZ
            End If
            If clsEndPoint.EnableEccentric Then
                clsVector.X = clsVector.RoundX + clsEndPoint.VectorEccentric.RoundX
                clsVector.Y = clsVector.RoundY + clsEndPoint.VectorEccentric.RoundY
                clsVector.Z = clsVector.RoundZ + clsEndPoint.VectorEccentric.RoundZ
            End If
            Set VectorCorrect = clsVector
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'偏心補正後ベクトル。
        public CoordinatePoint VectorCorrect()
        {
            //CoordinatePointXYZ clsVector = new CoordinatePointXYZ();
            CoordinatePointXYZ clsVector;
            ObservationPoint clsStrPoint;
            ObservationPoint clsEndPoint;
            if (Analysis <= ANALYSIS_STATUS.ANALYSIS_STATUS_FIX)
            {
                clsVector = (CoordinatePointXYZ)VectorAnalysis();
                clsStrPoint = AnalysisStrPoint();
                clsEndPoint = AnalysisEndPoint();
            }
            else
            {
                clsVector = (CoordinatePointXYZ)VectorObservation();
                clsStrPoint = StrPoint();
                clsEndPoint = EndPoint();
            }
            if (clsStrPoint.EnableEccentric())
            {
                clsVector.X = clsVector.RoundXX() - clsStrPoint.VectorEccentric().RoundXX();
                clsVector.Y = clsVector.RoundYY() - clsStrPoint.VectorEccentric().RoundYY();
                clsVector.Z = clsVector.RoundZZ() - clsStrPoint.VectorEccentric().RoundZZ();
            }
            if (clsEndPoint.EnableEccentric())
            {
                clsVector.X = clsVector.RoundXX() + clsEndPoint.VectorEccentric().RoundXX();
                clsVector.Y = clsVector.RoundYY() + clsEndPoint.VectorEccentric().RoundYY();
                clsVector.Z = clsVector.RoundZZ() + clsEndPoint.VectorEccentric().RoundZZ();
            }
            return clsVector;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '出力ベクトル。
        Property Get VectorOutput() As CoordinatePoint
            Set VectorOutput = VectorCorrect
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'出力ベクトル。
        public CoordinatePoint VectorOutput()
        {
            return VectorCorrect();
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '偏心補正前ベクトル。
        Property Get VectorUncorrect() As CoordinatePoint
            Dim clsVector As CoordinatePoint
            If Analysis <= ANALYSIS_STATUS_FIX Then
                Set clsVector = VectorAnalysis
            Else
                Set clsVector = VectorObservation
            End If
            Set VectorUncorrect = clsVector
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'偏心補正前ベクトル。
        public CoordinatePoint VectorUncorrect()
        {
            CoordinatePoint clsVector;
            if (Analysis <= ANALYSIS_STATUS.ANALYSIS_STATUS_FIX)
            {
                clsVector = VectorAnalysis();
            }
            else
            {
                clsVector = VectorObservation();
            }
            return clsVector;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '閉合ベクトル。
        Property Get VectorAngleDiff() As CoordinatePoint
            Set VectorAngleDiff = VectorAnalysis
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'閉合ベクトル。
        public CoordinatePoint VectorAngleDiff()
        {
            return VectorAnalysis();
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '分散・共分散。
        Property Let Dispersion(ByVal clsDispersion As Dispersion)
            Let m_clsDispersion = clsDispersion
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'分散・共分散。
        public void Dispersion(Dispersion clsDispersion)
        {
            m_clsDispersion = clsDispersion;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '分散・共分散。
        Property Get Dispersion() As Dispersion
            Set Dispersion = m_clsDispersion
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'分散・共分散。
        public Dispersion Dispersion()
        {
            return m_clsDispersion;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '有効フラグ。
        Property Let Enable(ByVal bEnable As Boolean)
            m_clsStrPoint.Enable = bEnable
            m_clsEndPoint.Enable = bEnable
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'有効フラグ。
        public void Enable(bool bEnable)
        {
            m_clsStrPoint.Enable(bEnable);
            m_clsEndPoint.Enable(bEnable);
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '有効フラグ。
        Property Get Enable() As Boolean
            Enable = m_clsStrPoint.Enable And m_clsEndPoint.Enable And Valid
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'有効フラグ。
        public bool Enable()
        {
            return m_clsStrPoint.Enable() & m_clsEndPoint.Enable() & Valid();
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '無効化した場合、観測点が無効になるか？
        Property Get IfDisable() As Boolean
            IfDisable = m_clsStrPoint.IfDisable Or m_clsEndPoint.IfDisable
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'無効化した場合、観測点が無効になるか？
        public bool IfDisable()
        {
            return m_clsStrPoint.IfDisable() | m_clsEndPoint.IfDisable();
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '有効フラグ。
        Property Get Valid() As Boolean
            Valid = True
        '    '始点と終点が同一のものは無効。
        '    Valid = Not m_clsStrPoint.TopParentPoint Is m_clsEndPoint.TopParentPoint
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'有効フラグ。
        public bool Valid()
        {
            return true;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '基線ベクトルキー。
        Property Get Key() As String
            Key = GetBaseLineVectorKey(StrPoint.Number, EndPoint.Number, Session)
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'基線ベクトルキー。
        public string Key()
        {
            return GetBaseLineVectorKey(StrPoint().Number(), EndPoint().Number(), Session);
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '閉合候補フラグ。
        Property Get Candidate() As Boolean
            Candidate = False
            '無効な基線ベクトルは無効。
            If Not Enable Then Exit Function
            '未解析の基線ベクトルは無効。
            If Analysis = ANALYSIS_STATUS_NOT Then Exit Function
            If Analysis = ANALYSIS_STATUS_FAILED Then Exit Function
            Candidate = True
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'閉合候補フラグ。
        public bool Candidate()
        {
            //'無効な基線ベクトルは無効。
            if (!Enable())
            {
                return false;
            }
            //'未解析の基線ベクトルは無効。
            if (Analysis == ANALYSIS_STATUS.ANALYSIS_STATUS_NOT)
            {
                return false;
            }
            if (Analysis == ANALYSIS_STATUS.ANALYSIS_STATUS_FAILED)
            {
                return false;
            }
            return true;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '始点アンテナ位相補正パターン。
        Property Get StrDepPattern() As DepPattern
            Set StrDepPattern = m_clsStrDepPattern
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'始点アンテナ位相補正パターン。
        public DepPattern StrDepPattern()
        {
            return m_clsStrDepPattern;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '終点アンテナ位相補正パターン。
        Property Get EndDepPattern() As DepPattern
            Set EndDepPattern = m_clsEndDepPattern
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'終点アンテナ位相補正パターン。
        public DepPattern EndDepPattern()
        {
            return m_clsEndDepPattern;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '総エポック数。
        Property Get EpochAll() As Long
            EpochAll = EpochUsed + EpochRejected
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'総エポック数。
        public long EpochAll()
        {
            return EpochUsed + EpochRejected;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '総評価衛星数。
        Property Get ObsAll() As Long
            Dim nObsAll As Long
            nObsAll = 0
            Dim i As Long
            For i = 0 To m_clsObsInfo.Count - 1
                nObsAll = nObsAll + m_clsObsInfo.All(i)
            Next
            ObsAll = nObsAll
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'総評価衛星数。
        public long ObsAll()
        {
            long nObsAll;
            nObsAll = 0;
            long i;
            for (i = 0; i < m_clsObsInfo.Count(); i++)
            {
                nObsAll = nObsAll + m_clsObsInfo.All(i);
            }
            return nObsAll;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '総使用衛星数。
        Property Get ObsUsed() As Long
            Dim nObsUsed As Long
            nObsUsed = 0
            Dim i As Long
            For i = 0 To m_clsObsInfo.Count - 1
                nObsUsed = nObsUsed + m_clsObsInfo.Used(i)
            Next
            ObsUsed = nObsUsed
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'総使用衛星数。
        public long ObsUsed()
        {
            long nObsUsed;
            nObsUsed = 0;
            long i;
            for (i = 0; i < m_clsObsInfo.Count(); i++)
            {
                nObsUsed = nObsUsed + m_clsObsInfo.Used(i);
            }
            return nObsUsed;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '総棄却衛星数。
        Property Get ObsRejected() As Long
            Dim nObsObsRejected As Long
            nObsObsRejected = 0
            Dim i As Long
            For i = 0 To m_clsObsInfo.Count - 1
                nObsObsRejected = nObsObsRejected + m_clsObsInfo.All(i) - m_clsObsInfo.Used(i)
            Next
            ObsRejected = nObsObsRejected
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'総棄却衛星数。
        public long ObsRejected()
        {
            long nObsObsRejected;
            nObsObsRejected = 0;
            long i;
            for (i = 0; i < m_clsObsInfo.Count(); i++)
            {
                nObsObsRejected = nObsObsRejected + m_clsObsInfo.All(i) - m_clsObsInfo.Used(i);
            }
            return nObsObsRejected;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '観測情報。
        Property Get ObsInfo() As ObsInfo
            Set ObsInfo = m_clsObsInfo
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'観測情報。
        public ObsInfo ObsInfo()
        {
            return m_clsObsInfo;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'アンビギュイティ情報。
        Property Get AmbInfo() As AmbInfo
            Set AmbInfo = m_clsAmbInfo
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'アンビギュイティ情報。
        public AmbInfo AmbInfo()
        {
            return m_clsAmbInfo;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '始点アンテナ位相補正L1。
        Property Let StrOffsetL1(ByVal clsStrOffsetL1 As CoordinatePoint)
            Let m_clsStrOffsetL1 = clsStrOffsetL1
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'始点アンテナ位相補正L1。
        public void StrOffsetL1(CoordinatePoint clsStrOffsetL1)
        {
            m_clsStrOffsetL1 = (CoordinatePointXYZ)clsStrOffsetL1;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '始点アンテナ位相補正L1。
        Property Get StrOffsetL1() As CoordinatePoint
            Set StrOffsetL1 = m_clsStrOffsetL1
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'始点アンテナ位相補正L1。
        public CoordinatePoint StrOffsetL1()
        {
            return m_clsStrOffsetL1;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '始点アンテナ位相補正L2。
        Property Let StrOffsetL2(ByVal clsStrOffsetL2 As CoordinatePoint)
            Let m_clsStrOffsetL2 = clsStrOffsetL2
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'始点アンテナ位相補正L2。
        public void StrOffsetL2(CoordinatePoint clsStrOffsetL2)
        {
            m_clsStrOffsetL2 = (CoordinatePointXYZ)clsStrOffsetL2;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '始点アンテナ位相補正L2。
        Property Get StrOffsetL2() As CoordinatePoint
            Set StrOffsetL2 = m_clsStrOffsetL2
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'始点アンテナ位相補正L2。
        public CoordinatePoint StrOffsetL2()
        {
            return m_clsStrOffsetL2;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '始点アンテナ位相補正L5。
        Property Let StrOffsetL5(ByVal clsStrOffsetL5 As CoordinatePoint)
            Let m_clsStrOffsetL5 = clsStrOffsetL5
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'始点アンテナ位相補正L5。
        public void StrOffsetL5(CoordinatePoint clsStrOffsetL5)
        {
            m_clsStrOffsetL5 = (CoordinatePointXYZ)clsStrOffsetL5;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '始点アンテナ位相補正L5。
        Property Get StrOffsetL5() As CoordinatePoint
            Set StrOffsetL5 = m_clsStrOffsetL5
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'始点アンテナ位相補正L5。
        public CoordinatePoint StrOffsetL5()
        {
            return m_clsStrOffsetL5;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '終点アンテナ位相補正L1。
        Property Let EndOffsetL1(ByVal clsEndOffsetL1 As CoordinatePoint)
            Let m_clsEndOffsetL1 = clsEndOffsetL1
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'終点アンテナ位相補正L1。
        public void EndOffsetL1(CoordinatePoint clsEndOffsetL1)
        {
            m_clsEndOffsetL1 = (CoordinatePointXYZ)clsEndOffsetL1;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '終点アンテナ位相補正L1。
        Property Get EndOffsetL1() As CoordinatePoint
            Set EndOffsetL1 = m_clsEndOffsetL1
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'終点アンテナ位相補正L1。
        public CoordinatePoint EndOffsetL1()
        {
            return m_clsEndOffsetL1;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '終点アンテナ位相補正L2。
        Property Let EndOffsetL2(ByVal clsEndOffsetL2 As CoordinatePoint)
            Let m_clsEndOffsetL2 = clsEndOffsetL2
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'終点アンテナ位相補正L2。
        public void EndOffsetL2(CoordinatePoint clsEndOffsetL2)
        {
            m_clsEndOffsetL2 = (CoordinatePointXYZ)clsEndOffsetL2;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '終点アンテナ位相補正L2。
        Property Get EndOffsetL2() As CoordinatePoint
            Set EndOffsetL2 = m_clsEndOffsetL2
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'終点アンテナ位相補正L2。
        public CoordinatePoint EndOffsetL2()
        {
            return m_clsEndOffsetL2;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '終点アンテナ位相補正L5。
        Property Let EndOffsetL5(ByVal clsEndOffsetL5 As CoordinatePoint)
            Let m_clsEndOffsetL5 = clsEndOffsetL5
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'終点アンテナ位相補正L5。
        public void EndOffsetL5(CoordinatePoint clsEndOffsetL5)
        {
            m_clsEndOffsetL5 = (CoordinatePointXYZ)clsEndOffsetL5;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '終点アンテナ位相補正L5。
        Property Get EndOffsetL5() As CoordinatePoint
            Set EndOffsetL5 = m_clsEndOffsetL5
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'終点アンテナ位相補正L5。
        public CoordinatePoint EndOffsetL5()
        {
            return m_clsEndOffsetL5;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '採用基線ベクトル。
        Property Get AdoptVector() As BaseLineVector
            Dim clsBaseLineVector As BaseLineVector
            Dim sKey As String
            sKey = GetDuplicationKey(Me)
            Dim clsObservationPoint As ObservationPoint
            Set clsObservationPoint = StrPoint.HeadPoint
            Do While Not clsObservationPoint Is Nothing
                Set clsBaseLineVector = GetAdoptVector(clsObservationPoint, sKey)
                If Not clsBaseLineVector Is Nothing Then Exit Do
                Set clsObservationPoint = clsObservationPoint.NextPoint
            Loop
            Set AdoptVector = clsBaseLineVector
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'採用基線ベクトル。
        public BaseLineVector AdoptVector()
        {
            BaseLineVector clsBaseLineVector = null;
            string sKey;
            sKey = GetDuplicationKey(this);
            ObservationPoint clsObservationPoint;
            clsObservationPoint = StrPoint().HeadPoint();
            while (clsObservationPoint != null)
            {
                clsBaseLineVector = GetAdoptVector(clsObservationPoint, sKey);
                if (clsBaseLineVector != null)
                {
                    break;
                }
                clsObservationPoint = clsObservationPoint.NextPoint();
            }
            return clsBaseLineVector;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '2023/06/26 Hitz H.Nakamura **************************************************
        'GNSS水準測量対応。
        '前半後半較差の追加。
        '前半基線ベクトル。
        Property Get HalfFstVector() As BaseLineVector
            Dim clsBaseLineVector As BaseLineVector
            Dim sKey As String
            sKey = GetDuplicationKey(Me)
            Dim clsObservationPoint As ObservationPoint
            Set clsObservationPoint = StrPoint.HeadPoint
            Do While Not clsObservationPoint Is Nothing
                Set clsBaseLineVector = GetHalfFstVector(clsObservationPoint, sKey)
                If Not clsBaseLineVector Is Nothing Then Exit Do
                Set clsObservationPoint = clsObservationPoint.NextPoint
            Loop
            Set HalfFstVector = clsBaseLineVector
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '2023/06/26 Hitz H.Nakamura **************************************************
        'GNSS水準測量対応。
        '前半後半較差の追加。
        '前半基線ベクトル。
        */
        public BaseLineVector HalfFstVector()
        {
            BaseLineVector clsBaseLineVector = null;
            string sKey;
            sKey = GetDuplicationKey(this);
            ObservationPoint clsObservationPoint;
            clsObservationPoint = StrPoint().HeadPoint();
            while (clsObservationPoint != null)
            {
                clsBaseLineVector = GetHalfFstVector(clsObservationPoint, sKey);
                if (clsBaseLineVector != null)
                {
                    break;
                }
                clsObservationPoint = clsObservationPoint.NextPoint();
            }
            return clsBaseLineVector;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '後半基線ベクトル。
        Property Get HalfLstVector() As BaseLineVector
            Dim clsBaseLineVector As BaseLineVector
            Dim sKey As String
            sKey = GetDuplicationKey(Me)
            Dim clsObservationPoint As ObservationPoint
            Set clsObservationPoint = StrPoint.HeadPoint
            Do While Not clsObservationPoint Is Nothing
                Set clsBaseLineVector = GetHalfLstVector(clsObservationPoint, sKey)
                If Not clsBaseLineVector Is Nothing Then Exit Do
                Set clsObservationPoint = clsObservationPoint.NextPoint
            Loop
            Set HalfLstVector = clsBaseLineVector
        End Property
        '*****************************************************************************
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'後半基線ベクトル。
        public BaseLineVector HalfLstVector()
        {
            BaseLineVector clsBaseLineVector = null;
            string sKey;
            sKey = GetDuplicationKey(this);
            ObservationPoint clsObservationPoint;
            clsObservationPoint = StrPoint().HeadPoint();
            while (clsObservationPoint != null)
            {
                clsBaseLineVector = GetHalfLstVector(clsObservationPoint, sKey);
                if (clsBaseLineVector != null)
                {
                    break;
                }
                clsObservationPoint = clsObservationPoint.NextPoint();
            }
            return clsBaseLineVector;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'リスト表示フラグ。
        Property Get VisibleList() As Boolean
            VisibleList = True
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'リスト表示フラグ。
        public bool VisibleList()
        {
            return true;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'プロット表示フラグ。
        Property Get VisiblePlot() As Boolean
            VisiblePlot = True
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'プロット表示フラグ。
        public bool VisiblePlot()
        {
            return true;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '観測データマスク。
        Property Let ObsDataMask(ByVal clsObsDataMask As ObsDataMask)
            Let m_clsObsDataMask = clsObsDataMask
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'観測データマスク。
        public void ObsDataMask(ObsDataMask clsObsDataMask)
        {
            m_clsObsDataMask = clsObsDataMask;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '観測データマスク。
        Property Get ObsDataMask() As ObsDataMask
            Set ObsDataMask = m_clsObsDataMask
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'観測データマスク。
        public ObsDataMask ObsDataMask()
        {
            return m_clsObsDataMask;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '基線ベクトル種類。
        Property Get LineType() As OBJ_MODE
            LineType = m_nLineType
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'基線ベクトル種類。
        public OBJ_MODE LineType()
        {
            return m_nLineType;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '採用フラグ。True=採用。False=採用以外。
        Property Get Adopt() As Boolean
            Adopt = (m_nLineType = OBJ_MODE_ADOPT)
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'採用フラグ。True=採用。False=採用以外。
        public bool Adopt()
        {
            return m_nLineType == OBJ_MODE.OBJ_MODE_ADOPT;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '点検フラグ。True=点検。False=点検以外。
        Property Get Check() As Boolean
            Check = (m_nLineType = OBJ_MODE_CHECK)
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'点検フラグ。True=点検。False=点検以外。
        public bool Check()
        {
            return m_nLineType == OBJ_MODE.OBJ_MODE_CHECK;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '重複フラグ。True=重複。False=重複以外。
        Property Get Duplicate() As Boolean
            Duplicate = (m_nLineType = OBJ_MODE_DUPLICATE)
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'重複フラグ。True=重複。False=重複以外。
        public bool Duplicate()
        {
            return m_nLineType == OBJ_MODE.OBJ_MODE_DUPLICATE;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '2023/06/26 Hitz H.Nakamura **************************************************
        'GNSS水準測量対応。
        '前半後半較差の追加。
        '前半フラグ。True=前半。False=前半以外。
        Property Get HalfFst() As Boolean
            HalfFst = (m_nLineType = OBJ_MODE_HALF_FST)
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '2023/06/26 Hitz H.Nakamura **************************************************
        'GNSS水準測量対応。
        '前半後半較差の追加。
        '前半フラグ。True=前半。False=前半以外。
        */
        public bool HalfFst()
        {
            return m_nLineType == OBJ_MODE.OBJ_MODE_HALF_FST;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '後半フラグ。True=後半。False=後半以外。
        Property Get HalfLst() As Boolean
            HalfLst = (m_nLineType = OBJ_MODE_HALF_LST)
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'後半フラグ。True=後半。False=後半以外。
        public bool HalfLst()
        {
            return m_nLineType == OBJ_MODE.OBJ_MODE_HALF_LST;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '前後半フラグ。True=前後半。False=前後半以外。
        Property Get Half() As Boolean
            Half = (m_nLineType = OBJ_MODE_HALF_FST) Or(m_nLineType = OBJ_MODE_HALF_LST)
        End Property
        '*****************************************************************************
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'前後半フラグ。True=前後半。False=前後半以外。
        public bool Half()
        {
            return m_nLineType == OBJ_MODE.OBJ_MODE_HALF_FST || m_nLineType == OBJ_MODE.OBJ_MODE_HALF_LST;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '偏心補正有効フラグ。
        Property Get EnableCorrect() As Boolean
            EnableCorrect = StrPoint.EnableEccentric Or EndPoint.EnableEccentric
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'偏心補正有効フラグ。
        public bool EnableCorrect()
        {
            return StrPoint().EnableEccentric() || EndPoint().EnableEccentric();
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '*******************************************************************************
        'イベント

        '初期化。
        Private Sub Class_Initialize()

            On Error GoTo ErrorHandler


            ObjectType = OBJ_TYPE_BASELINEVECTOR
            Set m_clsStrPoint.Owner = Me
            Set m_clsEndPoint.Owner = Me
            Set m_clsCoordinateAnalysis = New CoordinatePointXYZ
            StrTimeGPS = MIN_TIME - 1
            EndTimeGPS = MIN_TIME - 1
            Analysis = ANALYSIS_STATUS_NOT
            IsDispersion = False
            Set m_clsAnalysisStrPoint = m_clsStrPoint
            Set m_clsAnalysisEndPoint = m_clsEndPoint
            m_nLineType = OBJ_MODE_ADOPT

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

        '初期化。
        */
        private void Class_Initialize()
        {

            try
            {
                ObjectType = OBJ_TYPE_BASELINEVECTOR;
                m_clsStrPoint.Owner = this;
                m_clsEndPoint.Owner = this;
                CoordinatePointXYZ m_clsCoordinateAnalysis = new CoordinatePointXYZ();
                StrTimeGPS = DateTime.Parse(MIN_TIME);
                EndTimeGPS = DateTime.Parse(MIN_TIME);
                Analysis = ANALYSIS_STATUS.ANALYSIS_STATUS_NOT;
                IsDispersion = false;
                m_clsAnalysisStrPoint = m_clsStrPoint;
                m_clsAnalysisEndPoint = m_clsEndPoint;
                m_nLineType = OBJ_MODE.OBJ_MODE_ADOPT;
            }

            catch (Exception)
            {
                mdlMain.ErrorExit();
                return;
            }

        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '*******************************************************************************
        'メソッド

        '終了。
        Public Sub Terminate()
            Set m_clsStrPoint.Owner = Nothing
            Set m_clsEndPoint.Owner = Nothing
            Set m_clsStrPoint = Nothing
            Set m_clsEndPoint = Nothing
            Set m_clsAnalysisStrPoint = Nothing
            Set m_clsAnalysisEndPoint = Nothing
            Set m_clsCoordinateAnalysis = Nothing
            Set m_clsVectorAnalysis = Nothing
            Set m_clsDispersion = Nothing
            Set m_clsObsDataMask = Nothing
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '*******************************************************************************
        'メソッド

        '終了。
        */
        public void Terminate()
        {
            m_clsStrPoint.Owner = null;
            m_clsEndPoint.Owner = null;
            m_clsStrPoint = null;
            m_clsEndPoint = null;
            m_clsAnalysisStrPoint = null;
            m_clsAnalysisEndPoint = null;
            m_clsCoordinateAnalysis = null;
            m_clsVectorAnalysis = null;
            m_clsDispersion = null;
            m_clsObsDataMask = null;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '汎用作業キーを初期化する。
        '
        'nWorkKey で指定される値で WorkKey を初期化する。
        '
        '引き数：
        'nWorkKey 初期化する値。
        Public Sub ClearWorkKey(ByVal nWorkKey As Long)
            Call m_clsStrPoint.ClearWorkKey(nWorkKey)
            Call m_clsEndPoint.ClearWorkKey(nWorkKey)
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '汎用作業キーを初期化する。
        '
        'nWorkKey で指定される値で WorkKey を初期化する。
        '
        '引き数：
        'nWorkKey 初期化する値。
        */
        public void ClearWorkKey(long nWorkKey)
        {
            m_clsStrPoint.ClearWorkKey(nWorkKey);
            m_clsEndPoint.ClearWorkKey(nWorkKey);
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'リスト更新必要フラグを初期化する。
        '
        'IsList をOFFにする。
        Public Sub ClearIsList()
            m_clsStrPoint.IsList = False
            m_clsEndPoint.IsList = False
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        'リスト更新必要フラグを初期化する。
        '
        'IsList をOFFにする。
        */
        public void ClearIsList()
        {
            m_clsStrPoint.IsList = false;
            m_clsEndPoint.IsList = false;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '保存。
        '
        '引き数：
        'nFile ファイル番号。
        'nJointKey 結合キー。
        Public Sub Save(ByVal nFile As Integer, ByRef nJointKey As Long)
            Call PutString(nFile, Session)
            Put #nFile, , StrTimeGPS
            Put #nFile, , EndTimeGPS
            Put #nFile, , Exclusion
            Put #nFile, , Analysis
            Put #nFile, , Orbit
            Put #nFile, , Frequency
            Put #nFile, , SolveMode
            Put #nFile, , AnalysisStrTimeGPS
            Put #nFile, , AnalysisEndTimeGPS
            Put #nFile, , ElevationMask
            Put #nFile, , Interval
            Put #nFile, , Temperature
            Put #nFile, , Pressure
            Put #nFile, , Humidity
            Put #nFile, , Troposhere
            Put #nFile, , AnalysisFixed
            Put #nFile, , AmbPercentage
            Put #nFile, , Bias
            Put #nFile, , EpochUsed
            Put #nFile, , EpochRejected
            Put #nFile, , RMS
            Put #nFile, , RDOP
            Put #nFile, , IsDispersion
            Put #nFile, , RcvNumbersGps
            Put #nFile, , RcvNumbersGlonass
            '2017/06/06 NS6000対応。'''''''''''''''''''''''''''''''''''''''''''''''''''''''
            Put #nFile, , RcvNumbersQZSS
        '   Put #nFile, , RcvNumbersGalileo '2018/08/21 Hitz H.Nakamura 衛星数が増えたので64ビットに増やす。
            Put #nFile, , RcvNumbersGalileo1
            Put #nFile, , RcvNumbersGalileo2
            Put #nFile, , RcvNumbersBeiDou1
            Put #nFile, , RcvNumbersBeiDou2
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            Put #nFile, , MaxResidL1_Legacy
            Put #nFile, , MaxResidL2_Legacy
            Put #nFile, , MinRatio
            Put #nFile, , MinPs
            Put #nFile, , ExcludeGPS
            Put #nFile, , ExcludeGlonass
            Put #nFile, , GlonassFlag
            '2017/06/06 NS6000対応。'''''''''''''''''''''''''''''''''''''''''''''''''''''''
            Put #nFile, , ExcludeQZSS
        '   Put #nFile, , ExcludeGalileo    '2018/08/21 Hitz H.Nakamura 衛星数が増えたので64ビットに増やす。
            Put #nFile, , ExcludeGalileo1
            Put #nFile, , ExcludeGalileo2
            Put #nFile, , ExcludeBeiDou1
            Put #nFile, , ExcludeBeiDou2
            Put #nFile, , QZSSFlag
            Put #nFile, , GalileoFlag
            Put #nFile, , BeiDouFlag
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            Put #nFile, , m_nLineType
            Put #nFile, , StrPcvVer
            Put #nFile, , EndPcvVer
            Put #nFile, , AnalysisOrder
            Call m_clsStrPoint.Save(nFile, nJointKey)
            Call m_clsEndPoint.Save(nFile, nJointKey)
            Put #nFile, , Revers
            Put #nFile, , m_clsCoordinateAnalysis.CoordinateType
            Call m_clsCoordinateAnalysis.Save(nFile)
            Call m_clsVectorAnalysis.Save(nFile)
            Call m_clsDispersion.Save(nFile)
            Call m_clsStrDepPattern.Save(nFile)
            Call m_clsEndDepPattern.Save(nFile)
            Call m_clsObsInfo.Save(nFile)
            Call m_clsAmbInfo.Save(nFile)
            Call m_clsStrOffsetL1.Save(nFile)
            Call m_clsStrOffsetL2.Save(nFile)
            Call m_clsEndOffsetL1.Save(nFile)
            Call m_clsEndOffsetL2.Save(nFile)
            Call m_clsStrOffsetL5.Save(nFile)
            Call m_clsEndOffsetL5.Save(nFile)
            Call m_clsObsDataMask.Save(nFile)
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //==========================================================================================





        //==========================================================================================
        /*[VB]
        '始点を設定する。
        '
        '引き数：
        'clsStrPoint 始点(接合観測点)。
        '
        '戻り値：いままで始点であった ObservationPoint オブジェクト(接合観測点)。
        Public Function SetStrPoint(ByVal clsStrPoint As ObservationPoint) As ObservationPoint
            If Revers Then
                Set m_clsAnalysisEndPoint = clsStrPoint
            Else
                Set m_clsAnalysisStrPoint = clsStrPoint
            End If
            Set SetStrPoint = m_clsStrPoint
            Set m_clsStrPoint = clsStrPoint
            Set m_clsStrPoint.Owner = Me
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '始点を設定する。
        '
        '引き数：
        'clsStrPoint 始点(接合観測点)。
        '
        '戻り値：いままで始点であった ObservationPoint オブジェクト(接合観測点)。
        */
        public ObservationPoint SetStrPoint(ObservationPoint clsStrPoint)
        {
            ObservationPoint w_SetStrPoint;
            if (Revers())
            {
                m_clsAnalysisEndPoint = clsStrPoint;
            }
            else
            {
                m_clsAnalysisStrPoint = clsStrPoint;
            }
            w_SetStrPoint = m_clsStrPoint;
            m_clsStrPoint = clsStrPoint;
            m_clsStrPoint.Owner = this;
            return w_SetStrPoint;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '終点を設定する。
        '
        '引き数：
        'clsStrPoint 終点(接合観測点)。
        '
        '戻り値：いままで終点であった ObservationPoint オブジェクト(接合観測点)。
        Public Function SetEndPoint(ByVal clsEndPoint As ObservationPoint) As ObservationPoint
            If Revers Then
                Set m_clsAnalysisStrPoint = clsEndPoint
            Else
                Set m_clsAnalysisEndPoint = clsEndPoint
            End If
            Set SetEndPoint = m_clsEndPoint
            Set m_clsEndPoint = clsEndPoint
            Set m_clsEndPoint.Owner = Me
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '終点を設定する。
        '
        '引き数：
        'clsStrPoint 終点(接合観測点)。
        '
        '戻り値：いままで終点であった ObservationPoint オブジェクト(接合観測点)。
        */
        public ObservationPoint SetEndPoint(ObservationPoint clsEndPoint)
        {
            ObservationPoint w_SetEndPoint;
            if (Revers())
            {
                m_clsAnalysisStrPoint = clsEndPoint;
            }
            else
            {
                m_clsAnalysisEndPoint = clsEndPoint;
            }
            w_SetEndPoint = m_clsEndPoint;
            m_clsEndPoint = clsEndPoint;
            m_clsEndPoint.Owner = this;
            return w_SetEndPoint;
        }
        //==========================================================================================


        //5==========================================================================================
        /*[VB]
            '始点と終点を入れ替える。
            Public Sub Replace()
                Dim clsObservationPoint As ObservationPoint
                Set clsObservationPoint = m_clsStrPoint
                Set m_clsStrPoint = m_clsEndPoint
                Set m_clsEndPoint = clsObservationPoint
            End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#] //5
        /// <summary>
        /// 始点と終点を入れ替える。
        /// 
        /// </summary>
        public void Replace()
        {
            ObservationPoint clsObservationPoint;
            clsObservationPoint = m_clsStrPoint;
            m_clsStrPoint = m_clsEndPoint;
            m_clsEndPoint = clsObservationPoint;
        }



        //==========================================================================================
        /*[VB]
        '基線ベクトル種類を設定する。
        '
        '引き数：
        'nLineType 基線ベクトル種類。
        Public Sub SetLineType(ByVal nLineType As OBJ_MODE)
            m_nLineType = nLineType
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '基線ベクトル種類を設定する。
        '
        '引き数：
        'nLineType 基線ベクトル種類。
        */
        public void SetLineType(OBJ_MODE nLineType)
        {
            m_nLineType = nLineType;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '2022/02/07 SattSignal の追加。''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '使用した周波数を取得する(GPS)。
        '
        '戻り値：
        '使用した周波数。
        Public Function GetFrequencyUsedGps() As Long
            Dim nSattSignal As Long
            Select Case Frequency
            Case FREQUENCY_L1 'L1
                nSattSignal = &H1
            Case FREQUENCY_L2 'L2｡
                nSattSignal = &H2
            Case FREQUENCY_L1L2 'L1とL2の両方｡
                nSattSignal = &H1 + &H2
            Case FREQUENCY_L5 'L1とL2を組み合わせたワイドレーン (Lw)。
                nSattSignal = &H1 + &H2
            Case FREQUENCY_L3 'L1とL2を組み合わせた電離層補正 (lc)。
                nSattSignal = &H1 + &H2
            Case FREQUENCY_L5L3 'ワイドレーンと電離層補正の両方｡
                nSattSignal = &H1 + &H2
            Case FREQUENCY_1F '1周波。
                nSattSignal = &H1
            Case FREQUENCY_2F '2周波(L1+L2)。
                nSattSignal = &H1 + &H2
            Case FREQUENCY_L1L5 '2周波(L1+L5)。
                nSattSignal = &H1 + &H4
            Case FREQUENCY_3F '3周波。
                nSattSignal = &H1 + &H2 + &H4
            Case FREQUENCY_IFMW 'iono-free & Melbourne-Wuebbena
                nSattSignal = &H1 + &H2
            Case Default
                nSattSignal = 0
            End Select
            GetFrequencyUsedGps = nSattSignal And m_clsStrPoint.SattSignalGPS And m_clsEndPoint.SattSignalGPS
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '2022/02/07 SattSignal の追加。''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '使用した周波数を取得する(GPS)。
        '
        '戻り値：
        '使用した周波数。
        */
        public long GetFrequencyUsedGps()
        {
            long nSattSignal;
            switch (Frequency)
            {
                case FREQUENCY_TYPE.FREQUENCY_L1:      //'L1
                    nSattSignal = 0x01;
                    break;
                case FREQUENCY_TYPE.FREQUENCY_L2:      //'L2｡
                    nSattSignal = 0x02;
                    break;
                case FREQUENCY_TYPE.FREQUENCY_L1L2:    //'L1とL2の両方｡
                    nSattSignal = 0x01 + 0x02;
                    break;
                case FREQUENCY_TYPE.FREQUENCY_L5:      //'L1とL2を組み合わせたワイドレーン (Lw)。
                    nSattSignal = 0x01 + 0x02;
                    break;
                case FREQUENCY_TYPE.FREQUENCY_L3:      //'L1とL2を組み合わせた電離層補正 (lc)。
                    nSattSignal = 0x01 + 0x02;
                    break;
                case FREQUENCY_TYPE.FREQUENCY_L5L3:    //'ワイドレーンと電離層補正の両方｡
                    nSattSignal = 0x01 + 0x02;
                    break;
                case FREQUENCY_TYPE.FREQUENCY_1F:      //'1周波。
                    nSattSignal = 0x01;
                    break;
                case FREQUENCY_TYPE.FREQUENCY_2F:      //'2周波(L1+L2)。
                    nSattSignal = 0x01 + 0x02;
                    break;
                case FREQUENCY_TYPE.FREQUENCY_L1L5:    //'2周波(L1+L5)。
                    nSattSignal = 0x01 + 0x04;
                    break;
                case FREQUENCY_TYPE.FREQUENCY_3F:      //'3周波。
                    nSattSignal = 0x01 + 0x02 + 0x04;
                    break;
                case FREQUENCY_TYPE.FREQUENCY_IFMW:    // 'iono-free & Melbourne-Wuebbena
                    nSattSignal = 0x01 + 0x02;
                    break;
                default:
                    nSattSignal = 0;
                    break;
            }
            return nSattSignal & m_clsStrPoint.SattSignalGPS() & m_clsEndPoint.SattSignalGPS();
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '使用した周波数を取得する(GLONASS)。
        '
        '戻り値：
        '使用した周波数。
        Public Function GetFrequencyUsedGlonass() As Long
            Dim nSattSignal As Long
            Select Case Frequency
            Case FREQUENCY_L1 'L1
                nSattSignal = &H1
            Case FREQUENCY_L2 'L2｡
                nSattSignal = &H2
            Case FREQUENCY_L1L2 'L1とL2の両方｡
                nSattSignal = &H1 + &H2
            Case FREQUENCY_L5 'L1とL2を組み合わせたワイドレーン (Lw)。
                nSattSignal = &H1 + &H2
            Case FREQUENCY_L3 'L1とL2を組み合わせた電離層補正 (lc)。
                nSattSignal = &H1 + &H2
            Case FREQUENCY_L5L3 'ワイドレーンと電離層補正の両方｡
                nSattSignal = &H1 + &H2
            Case FREQUENCY_1F '1周波。
                nSattSignal = &H1
            Case FREQUENCY_2F '2周波(L1+L2)。
                nSattSignal = &H1 + &H2
            Case FREQUENCY_L1L5 '2周波(L1+L5)。
                nSattSignal = &H1 + &H4
            Case FREQUENCY_3F '3周波。
                nSattSignal = &H1 + &H2 + &H4
            Case FREQUENCY_IFMW 'iono-free & Melbourne-Wuebbena
                nSattSignal = &H1 + &H2
            Case Default
                nSattSignal = 0
            End Select
            GetFrequencyUsedGlonass = nSattSignal And m_clsStrPoint.SattSignalGLONASS And m_clsEndPoint.SattSignalGLONASS
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '使用した周波数を取得する(GLONASS)。
        '
        '戻り値：
        '使用した周波数。
        */
        public long GetFrequencyUsedGlonass()
        {
            long nSattSignal;
            switch (Frequency)
            {
                case FREQUENCY_TYPE.FREQUENCY_L1:      //'L1
                    nSattSignal = 0x01;
                    break;
                case FREQUENCY_TYPE.FREQUENCY_L2:      //'L2｡
                    nSattSignal = 0x02;
                    break;
                case FREQUENCY_TYPE.FREQUENCY_L1L2:    //'L1とL2の両方｡
                    nSattSignal = 0x01 + 0x02;
                    break;
                case FREQUENCY_TYPE.FREQUENCY_L5:      //'L1とL2を組み合わせたワイドレーン (Lw)。
                    nSattSignal = 0x01 + 0x02;
                    break;
                case FREQUENCY_TYPE.FREQUENCY_L3:      //'L1とL2を組み合わせた電離層補正 (lc)。
                    nSattSignal = 0x01 + 0x02;
                    break;
                case FREQUENCY_TYPE.FREQUENCY_L5L3:    //'ワイドレーンと電離層補正の両方｡
                    nSattSignal = 0x01 + 0x02;
                    break;
                case FREQUENCY_TYPE.FREQUENCY_1F:      //'1周波。
                    nSattSignal = 0x01;
                    break;
                case FREQUENCY_TYPE.FREQUENCY_2F:      //'2周波(L1+L2)。
                    nSattSignal = 0x01 + 0x02;
                    break;
                case FREQUENCY_TYPE.FREQUENCY_L1L5:    //'2周波(L1+L5)。
                    nSattSignal = 0x01 + 0x04;
                    break;
                case FREQUENCY_TYPE.FREQUENCY_3F:      //'3周波。
                    nSattSignal = 0x01 + 0x02 + 0x04;
                    break;
                case FREQUENCY_TYPE.FREQUENCY_IFMW:    //'iono-free & Melbourne-Wuebbena
                    nSattSignal = 0x01 + 0x02;
                    break;
                default:
                    nSattSignal = 0;
                    break;
            }
            return nSattSignal & m_clsStrPoint.SattSignalGLONASS() & m_clsEndPoint.SattSignalGLONASS();
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '使用した周波数を取得する(QZSS)。
        '
        '戻り値：
        '使用した周波数。
        Public Function GetFrequencyUsedQzss() As Long
            Dim nSattSignal As Long
            Select Case Frequency
            Case FREQUENCY_L1 'L1
                nSattSignal = &H1
            Case FREQUENCY_L2 'L2｡
                nSattSignal = &H2
            Case FREQUENCY_L1L2 'L1とL2の両方｡
                nSattSignal = &H1 + &H2
            Case FREQUENCY_L5 'L1とL2を組み合わせたワイドレーン (Lw)。
                nSattSignal = &H1 + &H2
            Case FREQUENCY_L3 'L1とL2を組み合わせた電離層補正 (lc)。
                nSattSignal = &H1 + &H2
            Case FREQUENCY_L5L3 'ワイドレーンと電離層補正の両方｡
                nSattSignal = &H1 + &H2
            Case FREQUENCY_1F '1周波。
                nSattSignal = &H1
            Case FREQUENCY_2F '2周波(L1+L2)。
                nSattSignal = &H1 + &H2
            Case FREQUENCY_L1L5 '2周波(L1+L5)。
                nSattSignal = &H1 + &H4
            Case FREQUENCY_3F '3周波。
                nSattSignal = &H1 + &H2 + &H4
            Case FREQUENCY_IFMW 'iono-free & Melbourne-Wuebbena
                nSattSignal = &H1 + &H2
            Case Default
                nSattSignal = 0
            End Select
            GetFrequencyUsedQzss = nSattSignal And m_clsStrPoint.SattSignalQZSS And m_clsEndPoint.SattSignalQZSS
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '使用した周波数を取得する(QZSS)。
        '
        '戻り値：
        '使用した周波数。
        */
        public long GetFrequencyUsedQzss()
        {
            long nSattSignal;
            switch (Frequency)
            {
                case FREQUENCY_TYPE.FREQUENCY_L1:      //'L1
                    nSattSignal = 0x01;
                    break;
                case FREQUENCY_TYPE.FREQUENCY_L2:      //'L2｡
                    nSattSignal = 0x02;
                    break;
                case FREQUENCY_TYPE.FREQUENCY_L1L2:    //'L1とL2の両方｡
                    nSattSignal = 0x01 + 0x02;
                    break;
                case FREQUENCY_TYPE.FREQUENCY_L5:      //'L1とL2を組み合わせたワイドレーン (Lw)。
                    nSattSignal = 0x01 + 0x02;
                    break;
                case FREQUENCY_TYPE.FREQUENCY_L3:      //'L1とL2を組み合わせた電離層補正 (lc)。
                    nSattSignal = 0x01 + 0x02;
                    break;
                case FREQUENCY_TYPE.FREQUENCY_L5L3:    //'ワイドレーンと電離層補正の両方｡
                    nSattSignal = 0x01 + 0x02;
                    break;
                case FREQUENCY_TYPE.FREQUENCY_1F:      //'1周波。
                    nSattSignal = 0x01;
                    break;
                case FREQUENCY_TYPE.FREQUENCY_2F:      //'2周波(L1+L2)。
                    nSattSignal = 0x01 + 0x02;
                    break;
                case FREQUENCY_TYPE.FREQUENCY_L1L5:    //'2周波(L1+L5)。
                    nSattSignal = 0x01 + 0x04;
                    break;
                case FREQUENCY_TYPE.FREQUENCY_3F:      //'3周波。
                    nSattSignal = 0x01 + 0x02 + 0x04;
                    break;
                case FREQUENCY_TYPE.FREQUENCY_IFMW:    //'iono-free & Melbourne-Wuebbena
                    nSattSignal = 0x01 + 0x02;
                    break;
                default:
                    nSattSignal = 0;
                    break;
            }
            return nSattSignal & m_clsStrPoint.SattSignalQZSS() & m_clsEndPoint.SattSignalQZSS();
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '使用した周波数を取得する(Galileo)。
        '
        '戻り値：
        '使用した周波数。
        Public Function GetFrequencyUsedGalileo() As Long
            Dim nSattSignal As Long
            Select Case Frequency
            Case FREQUENCY_L1 'L1
                nSattSignal = &H1
            Case FREQUENCY_L2 'L2｡
                nSattSignal = 0
            Case FREQUENCY_L1L2 'L1とL2の両方｡
                nSattSignal = &H1
            Case FREQUENCY_L5 'L1とL2を組み合わせたワイドレーン (Lw)。
                nSattSignal = &H1
            Case FREQUENCY_L3 'L1とL2を組み合わせた電離層補正 (lc)。
                nSattSignal = &H1
            Case FREQUENCY_L5L3 'ワイドレーンと電離層補正の両方｡
                nSattSignal = &H1
            Case FREQUENCY_1F '1周波。
                nSattSignal = &H1
            Case FREQUENCY_2F '2周波(L1+L2)。
                nSattSignal = &H1
            Case FREQUENCY_L1L5 '2周波(L1+L5)。
                nSattSignal = &H1 + &H2
            Case FREQUENCY_3F '3周波。
                nSattSignal = &H1 + &H2
            Case FREQUENCY_IFMW 'iono-free & Melbourne-Wuebbena
                nSattSignal = &H1
            Case Default
                nSattSignal = 0
            End Select
            GetFrequencyUsedGalileo = nSattSignal And m_clsStrPoint.SattSignalGalileo And m_clsEndPoint.SattSignalGalileo
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '使用した周波数を取得する(Galileo)。
        '
        '戻り値：
        '使用した周波数。
        */
        public long GetFrequencyUsedGalileo()
        {
            long nSattSignal;
            switch (Frequency)
            {
                case FREQUENCY_TYPE.FREQUENCY_L1:      //'L1
                    nSattSignal = 0x01;
                    break;
                case FREQUENCY_TYPE.FREQUENCY_L2:      //'L2｡
                    nSattSignal = 0;
                    break;
                case FREQUENCY_TYPE.FREQUENCY_L1L2:    //'L1とL2の両方｡
                    nSattSignal = 0x01;
                    break;
                case FREQUENCY_TYPE.FREQUENCY_L5:      //'L1とL2を組み合わせたワイドレーン (Lw)。
                    nSattSignal = 0x01;
                    break;
                case FREQUENCY_TYPE.FREQUENCY_L3:      //'L1とL2を組み合わせた電離層補正 (lc)。
                    nSattSignal = 0x01;
                    break;
                case FREQUENCY_TYPE.FREQUENCY_L5L3:    //'ワイドレーンと電離層補正の両方｡
                    nSattSignal = 0x01;
                    break;
                case FREQUENCY_TYPE.FREQUENCY_1F:      //'1周波。
                    nSattSignal = 0x01;
                    break;
                case FREQUENCY_TYPE.FREQUENCY_2F:      //'2周波(L1+L2)。
                    nSattSignal = 0x01;
                    break;
                case FREQUENCY_TYPE.FREQUENCY_L1L5:    //'2周波(L1+L5)。
                    nSattSignal = 0x01 + 0x02;
                    break;
                case FREQUENCY_TYPE.FREQUENCY_3F:      //'3周波。
                    nSattSignal = 0x01 + 0x02;
                    break;
                case FREQUENCY_TYPE.FREQUENCY_IFMW:    //'iono-free & Melbourne-Wuebbena
                    nSattSignal = 0x01;
                    break;
                default:
                    nSattSignal = 0;
                    break;
            }
            return nSattSignal & m_clsStrPoint.SattSignalGalileo() & m_clsEndPoint.SattSignalGalileo();
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '使用した周波数を取得する(BeiDou)。
        '
        '戻り値：
        '使用した周波数。
        Public Function GetFrequencyUsedBeiDou() As Long
            Dim nSattSignal As Long
            Select Case Frequency
            Case FREQUENCY_L1 'L1
                nSattSignal = &H1
            Case FREQUENCY_L2 'L2｡
                nSattSignal = &H2
            Case FREQUENCY_L1L2 'L1とL2の両方｡
                nSattSignal = &H1 + &H2
            Case FREQUENCY_L5 'L1とL2を組み合わせたワイドレーン (Lw)。
                nSattSignal = &H1 + &H2
            Case FREQUENCY_L3 'L1とL2を組み合わせた電離層補正 (lc)。
                nSattSignal = &H1 + &H2
            Case FREQUENCY_L5L3 'ワイドレーンと電離層補正の両方｡
                nSattSignal = &H1 + &H2
            Case FREQUENCY_1F '1周波。
                nSattSignal = &H1
            Case FREQUENCY_2F '2周波(L1+L2)。
                nSattSignal = &H1 + &H2
            Case FREQUENCY_L1L5 '2周波(L1+L5)。
                nSattSignal = &H1 + &H4
            Case FREQUENCY_3F '3周波。
                nSattSignal = &H1 + &H2 + &H4
            Case FREQUENCY_IFMW 'iono-free & Melbourne-Wuebbena
                nSattSignal = &H1 + &H2
            Case Default
                nSattSignal = 0
            End Select
            GetFrequencyUsedBeiDou = nSattSignal And m_clsStrPoint.SattSignalBeiDou And m_clsEndPoint.SattSignalBeiDou
        End Function
        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '使用した周波数を取得する(BeiDou)。
        '
        '戻り値：
        '使用した周波数。
        */
        public long GetFrequencyUsedBeiDou()
        {
            long nSattSignal;
            switch (Frequency)
            {
                case FREQUENCY_TYPE.FREQUENCY_L1:          //'L1
                    nSattSignal = 0x01;
                    break;
                case FREQUENCY_TYPE.FREQUENCY_L2:          //'L2｡
                    nSattSignal = 0x02;
                    break;
                case FREQUENCY_TYPE.FREQUENCY_L1L2:        //'L1とL2の両方｡
                    nSattSignal = 0x01 + 0x02;
                    break;
                case FREQUENCY_TYPE.FREQUENCY_L5:          //'L1とL2を組み合わせたワイドレーン (Lw)。
                    nSattSignal = 0x01 + 0x02;
                    break;
                case FREQUENCY_TYPE.FREQUENCY_L3:          //'L1とL2を組み合わせた電離層補正 (lc)。
                    nSattSignal = 0x01 + 0x02;
                    break;
                case FREQUENCY_TYPE.FREQUENCY_L5L3:        //'ワイドレーンと電離層補正の両方｡
                    nSattSignal = 0x01 + 0x02;
                    break;
                case FREQUENCY_TYPE.FREQUENCY_1F:          //'1周波。
                    nSattSignal = 0x01;
                    break;
                case FREQUENCY_TYPE.FREQUENCY_2F:          //'2周波(L1+L2)。
                    nSattSignal = 0x01 + 0x02;
                    break;
                case FREQUENCY_TYPE.FREQUENCY_L1L5:        //'2周波(L1+L5)。
                    nSattSignal = 0x01 + 0x04;
                    break;
                case FREQUENCY_TYPE.FREQUENCY_3F:          //'3周波。
                    nSattSignal = 0x01 + 0x02 + 0x04;
                    break;
                case FREQUENCY_TYPE.FREQUENCY_IFMW:        //'iono-free & Melbourne-Wuebbena
                    nSattSignal = 0x01 + 0x02;
                    break;
                default:
                    nSattSignal = 0;
                    break;
            }
            return nSattSignal & m_clsStrPoint.SattSignalBeiDou() & m_clsEndPoint.SattSignalBeiDou();
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '2023/05/19 Hitz H.Nakamura **************************************************
        '楕円体高の閉合差を追加。
        '楕円体比高の有無。
        Public Function HasHeightDiff() As Boolean
            HasHeightDiff = True
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '2023/05/19 Hitz H.Nakamura **************************************************
        '楕円体高の閉合差を追加。
        '楕円体比高の有無。
        */
        public bool HasHeightDiff()
        {
            return true;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '楕円体比高。
        Public Function GetHeightDiff() As Double

            Dim clsCoordinateAnalysis As CoordinatePoint
            Dim clsVectorAnalysis As CoordinatePoint
            Dim clsCoordinateEnd As CoordinatePoint
            Dim nLat As Double
            Dim nLon As Double
            Dim nStrHeight As Double
            Dim nEndHeight As Double
            Dim vAlt As Variant


            Set clsCoordinateAnalysis = CoordinateAnalysis
            Set clsVectorAnalysis = VectorAnalysis
            Set clsCoordinateEnd = AddCoordinateRound(clsCoordinateAnalysis, clsVectorAnalysis)


            Call clsCoordinateAnalysis.GetDEG(nLat, nLon, nStrHeight, vAlt, "")
            Call clsCoordinateEnd.GetDEG(nLat, nLon, nEndHeight, vAlt, "")


            GetHeightDiff = JpnRound(nEndHeight, ACCOUNT_DECIMAL_HEIGHT) - JpnRound(nStrHeight, ACCOUNT_DECIMAL_HEIGHT)


        End Function
        '*****************************************************************************
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'楕円体比高。
        public double GetHeightDiff()
        {
            CoordinatePoint clsCoordinateAnalysis;
            CoordinatePoint clsVectorAnalysis;
            CoordinatePoint clsCoordinateEnd;
            double nLat = 0;
            double nLon = 0;
            double nStrHeight = 0;
            double nEndHeight = 0;
            double vAlt = 0;


            clsCoordinateAnalysis = CoordinateAnalysis();
            clsVectorAnalysis = VectorAnalysis();
            clsCoordinateEnd = AddCoordinateRound(clsCoordinateAnalysis, clsVectorAnalysis);


            clsCoordinateAnalysis.GetDEG(ref nLat, ref nLon, ref nStrHeight, ref vAlt, "");
            clsCoordinateEnd.GetDEG(ref nLat, ref nLon, ref nEndHeight, ref vAlt, "");


            return JpnRound(nEndHeight, ACCOUNT_DECIMAL_HEIGHT) - JpnRound(nStrHeight, ACCOUNT_DECIMAL_HEIGHT);


        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '*******************************************************************************
        'インプリメンテーション

        '採用基線ベクトルを取得する。
        '
        '自分と重複する基線ベクトルオブジェクトの中で解析済で有効な採用基線ベクトルを取得する。
        '
        '引き数：
        'clsObservationPoint 観測点。
        'sKey 重複キー。
        '
        '戻り値：採用基線ベクトルを返す。
        Private Function GetAdoptVector(ByVal clsObservationPoint As ObservationPoint, ByVal sKey As String) As BaseLineVector
            Dim clsBaseLineVector As BaseLineVector
            Dim clsChildPoint As ObservationPoint
            Set clsChildPoint = clsObservationPoint.ChildPoint
            If clsChildPoint Is Nothing Then
                '接合観測点の場合、基線ベクトルを評価する。
                If(clsObservationPoint.ObjectType And OBS_TYPE_CONNECT) <> 0 Then
                    Set clsBaseLineVector = clsObservationPoint.Owner
                    If clsBaseLineVector.Adopt And clsBaseLineVector.Enable And clsBaseLineVector.Analysis<ANALYSIS_STATUS_FAILED Then
                        If sKey = GetDuplicationKey(clsBaseLineVector) Then
                            Set GetAdoptVector = clsBaseLineVector
                        End If
                    End If
                End If
            Else
                '子観測点すべてを巡回する。
                Do While Not clsChildPoint Is Nothing
                    Set clsBaseLineVector = GetAdoptVector(clsChildPoint, sKey)
                    If Not clsBaseLineVector Is Nothing Then Exit Do
                    Set clsChildPoint = clsChildPoint.NextPoint
                Loop
                Set GetAdoptVector = clsBaseLineVector
            End If
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '*******************************************************************************
        'インプリメンテーション

        '採用基線ベクトルを取得する。
        '
        '自分と重複する基線ベクトルオブジェクトの中で解析済で有効な採用基線ベクトルを取得する。
        '
        '引き数：
        'clsObservationPoint 観測点。
        'sKey 重複キー。
        '
        '戻り値：採用基線ベクトルを返す。
        */
        private BaseLineVector GetAdoptVector(ObservationPoint clsObservationPoint, string sKey)
        {
            BaseLineVector clsBaseLineVector = null;
            ObservationPoint clsChildPoint;
            clsChildPoint = clsObservationPoint.ChildPoint();
            if (clsChildPoint == null)
            {
                //'接合観測点の場合、基線ベクトルを評価する。
                if ((clsObservationPoint.ObjectType & OBS_TYPE_CONNECT) != 0)
                {
                    clsBaseLineVector = (BaseLineVector)clsObservationPoint.Owner;
                    if (clsBaseLineVector.Adopt() && clsBaseLineVector.Enable() && clsBaseLineVector.Analysis < ANALYSIS_STATUS.ANALYSIS_STATUS_FAILED)
                    {
                        if (sKey == GetDuplicationKey(clsBaseLineVector))
                        {
                            return clsBaseLineVector;
                        }
                    }
                }
            }
            else
            {
                //'子観測点すべてを巡回する。
                while (clsChildPoint != null)
                {
                    clsBaseLineVector = GetAdoptVector(clsChildPoint, sKey);
                    if (clsBaseLineVector != null)
                    {
                        break;
                    }
                    clsChildPoint = clsChildPoint.NextPoint();
                }
                return clsBaseLineVector;
            }
            return clsBaseLineVector;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '2023/06/26 Hitz H.Nakamura **************************************************
        'GNSS水準測量対応。
        '前半後半較差の追加。
        '前半基線ベクトルを取得する。
        '
        '自分と重複する基線ベクトルオブジェクトの中で解析済で有効な前半基線ベクトルを取得する。
        '
        '引き数：
        'clsObservationPoint 観測点。
        'sKey 重複キー。
        '
        '戻り値：前半基線ベクトルを返す。
        Private Function GetHalfFstVector(ByVal clsObservationPoint As ObservationPoint, ByVal sKey As String) As BaseLineVector
            Dim clsBaseLineVector As BaseLineVector
            Dim clsChildPoint As ObservationPoint
            Set clsChildPoint = clsObservationPoint.ChildPoint
            If clsChildPoint Is Nothing Then
                '接合観測点の場合、基線ベクトルを評価する。
                If(clsObservationPoint.ObjectType And OBS_TYPE_CONNECT) <> 0 Then
                    Set clsBaseLineVector = clsObservationPoint.Owner
                    If clsBaseLineVector.HalfFst And clsBaseLineVector.Enable And clsBaseLineVector.Analysis<ANALYSIS_STATUS_FAILED Then
                        If sKey = GetDuplicationKey(clsBaseLineVector) Then
                            Set GetHalfFstVector = clsBaseLineVector
                        End If
                    End If
                End If
            Else
                '子観測点すべてを巡回する。
                Do While Not clsChildPoint Is Nothing
                    Set clsBaseLineVector = GetHalfFstVector(clsChildPoint, sKey)
                    If Not clsBaseLineVector Is Nothing Then Exit Do
                    Set clsChildPoint = clsChildPoint.NextPoint
                Loop
                Set GetHalfFstVector = clsBaseLineVector
            End If
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '2023/06/26 Hitz H.Nakamura **************************************************
        'GNSS水準測量対応。
        '前半後半較差の追加。
        '前半基線ベクトルを取得する。
        '
        '自分と重複する基線ベクトルオブジェクトの中で解析済で有効な前半基線ベクトルを取得する。
        '
        '引き数：
        'clsObservationPoint 観測点。
        'sKey 重複キー。
        '
        '戻り値：前半基線ベクトルを返す。
        */
        private BaseLineVector GetHalfFstVector(ObservationPoint clsObservationPoint, string sKey)
        {
            BaseLineVector clsBaseLineVector = null;
            ObservationPoint clsChildPoint;
            clsChildPoint = clsObservationPoint.ChildPoint();
            if (clsChildPoint == null)
            {
                //'接合観測点の場合、基線ベクトルを評価する。
                if ((clsObservationPoint.ObjectType & OBS_TYPE_CONNECT) != 0)
                {
                    clsBaseLineVector = (BaseLineVector)clsObservationPoint.Owner;
                    if (clsBaseLineVector.HalfFst() && clsBaseLineVector.Enable() && clsBaseLineVector.Analysis < ANALYSIS_STATUS.ANALYSIS_STATUS_FAILED)
                    {
                        if (sKey == GetDuplicationKey(clsBaseLineVector))
                        {
                            return clsBaseLineVector;
                        }
                    }
                }
            }
            else
            {
                //'子観測点すべてを巡回する。
                while (clsChildPoint != null)
                {
                    clsBaseLineVector = GetHalfFstVector(clsChildPoint, sKey);
                    if (clsBaseLineVector != null)
                    {
                        break;
                    }
                    clsChildPoint = clsChildPoint.NextPoint();
                }
                return clsBaseLineVector;
            }
            return clsBaseLineVector;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '後半基線ベクトルを取得する。
        '
        '自分と重複する基線ベクトルオブジェクトの中で解析済で有効な前半基線ベクトルを取得する。
        '
        '引き数：
        'clsObservationPoint 観測点。
        'sKey 重複キー。
        '
        '戻り値：後半基線ベクトルを返す。
        Private Function GetHalfLstVector(ByVal clsObservationPoint As ObservationPoint, ByVal sKey As String) As BaseLineVector
            Dim clsBaseLineVector As BaseLineVector
            Dim clsChildPoint As ObservationPoint
            Set clsChildPoint = clsObservationPoint.ChildPoint
            If clsChildPoint Is Nothing Then
                '接合観測点の場合、基線ベクトルを評価する。
                If(clsObservationPoint.ObjectType And OBS_TYPE_CONNECT) <> 0 Then
                    Set clsBaseLineVector = clsObservationPoint.Owner
                    If clsBaseLineVector.HalfLst And clsBaseLineVector.Enable And clsBaseLineVector.Analysis<ANALYSIS_STATUS_FAILED Then
                        If sKey = GetDuplicationKey(clsBaseLineVector) Then
                            Set GetHalfLstVector = clsBaseLineVector
                        End If
                    End If
                End If
            Else
                '子観測点すべてを巡回する。
                Do While Not clsChildPoint Is Nothing
                    Set clsBaseLineVector = GetHalfLstVector(clsChildPoint, sKey)
                    If Not clsBaseLineVector Is Nothing Then Exit Do
                    Set clsChildPoint = clsChildPoint.NextPoint
                Loop
                Set GetHalfLstVector = clsBaseLineVector
            End If
        End Function
        '*****************************************************************************
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '後半基線ベクトルを取得する。
        '
        '自分と重複する基線ベクトルオブジェクトの中で解析済で有効な前半基線ベクトルを取得する。
        '
        '引き数：
        'clsObservationPoint 観測点。
        'sKey 重複キー。
        '
        '戻り値：後半基線ベクトルを返す。
        */
        private BaseLineVector GetHalfLstVector(ObservationPoint clsObservationPoint, string sKey)
        {
            BaseLineVector clsBaseLineVector = null;
            ObservationPoint clsChildPoint;
            clsChildPoint = clsObservationPoint.ChildPoint();
            if (clsChildPoint == null)
            {
                //'接合観測点の場合、基線ベクトルを評価する。
                if ((clsObservationPoint.ObjectType & OBS_TYPE_CONNECT) != 0)
                {
                    clsBaseLineVector = (BaseLineVector)clsObservationPoint.Owner;
                    if (clsBaseLineVector.HalfLst() && clsBaseLineVector.Enable() && clsBaseLineVector.Analysis < ANALYSIS_STATUS.ANALYSIS_STATUS_FAILED)
                    {
                        if (sKey == GetDuplicationKey(clsBaseLineVector))
                        {
                            return clsBaseLineVector;
                        }
                    }
                }
            }
            else
            {
                //'子観測点すべてを巡回する。
                while (clsChildPoint != null)
                {
                    clsBaseLineVector = GetHalfLstVector(clsChildPoint, sKey);
                    if (clsBaseLineVector != null)
                    {
                        break;
                    }
                    clsChildPoint = clsChildPoint.NextPoint();
                }
                return clsBaseLineVector;
            }
            return clsBaseLineVector;
        }
        //==========================================================================================



        //***************************************************************************
        //***************************************************************************
        /// <summary>
        ///'読み込み。
        ///'    長男が親観測点の Load を呼ぶ。
        ///     （VB Load関数をVC用に変更）
        ///'
        ///'引き数：
        ///  '引き数：
        /// バイナリファイル
        /// バージョン番号
        /// 読み込みデータ
        ///     ・ロードした観測点を保持するためのテンポラリ配列。配列の要素は(0 To ...)。Redim clsObservationPoints(0) で初期化されていること。
        ///     ・子観測点。
        /// </summary>
        /// <param name="BinaryReader br">
        /// <param name="nVersion br">
        /// <param name="Genba_S">
        /// </param>
        /// <returns>
        /// 戻り値:returns = Non
        /// </returns>
        //***************************************************************************
        //Public Sub Load(ByVal nFile As Integer, ByVal nVersion As Long, ByRef clsObservationPoints() As ObservationPoint)
        public void Load(BinaryReader br, long nVersion, ref GENBA_STRUCT_S Genba_S, ref ObservationPoint[] clsObservationPoints)
        {

            byte[] buf = new byte[8]; int ret;

            //--------------------------------------
            //{VB]Session = GetString(nFile)
            Genba_S.Session = FileRead_GetString(br);
            Session = Genba_S.Session;
            //--------------------------------------
            //{VB]Get #nFile, , StrTimeGPS
            // 4byte読み取り
            _ = br.Read(buf, 0, 8);
            Genba_S.StrTimeGPS = DateTime.FromOADate(BitConverter.ToDouble(buf, 0));
            StrTimeGPS = Genba_S.StrTimeGPS;
            //--------------------------------------
            //{VB]Get #nFile, , EndTimeGPS
            _ = br.Read(buf, 0, 8);       // 4byte読み取り
            Genba_S.EndTimeGPS = DateTime.FromOADate(BitConverter.ToDouble(buf, 0));
            EndTimeGPS = Genba_S.EndTimeGPS;
            //--------------------------------------
            //{VB]Get #nFile, , Exclusion
            Genba_S.Exclusion = document.GetFileBool(br);
            Exclusion = Genba_S.Exclusion;
            //--------------------------------------
            //{VB]Get #nFile, , Analysis
            Genba_S.Analysis = br.ReadInt32();
            Analysis = (ANALYSIS_STATUS)Genba_S.Analysis;
            //--------------------------------------
            //{VB]Get #nFile, , Orbit
            Genba_S.Orbit = br.ReadInt32();
            Orbit = (ORBIT_TYPE)Genba_S.Orbit;
            //--------------------------------------
            //{VB]Get #nFile, , Frequency
            Genba_S.Frequency = br.ReadInt32();
            Frequency = (FREQUENCY_TYPE)Genba_S.Frequency;
            //--------------------------------------
            //{VB]Get #nFile, , SolveMode
            Genba_S.SolveMode = br.ReadInt32();
            SolveMode = (SOLVEMODE_TYPE)Genba_S.SolveMode;
            //--------------------------------------
            //{VB]Get #nFile, , AnalysisStrTimeGPS
            _ = br.Read(buf, 0, 8);       // 4byte読み取り
            Genba_S.AnalysisStrTimeGPS = DateTime.FromOADate(BitConverter.ToDouble(buf, 0));
            AnalysisStrTimeGPS = Genba_S.AnalysisStrTimeGPS;
            //--------------------------------------
            //{VB]Get #nFile, , AnalysisEndTimeGPS
            _ = br.Read(buf, 0, 8);       // 4byte読み取り
            Genba_S.AnalysisEndTimeGPS = DateTime.FromOADate(BitConverter.ToDouble(buf, 0));
            AnalysisEndTimeGPS = Genba_S.AnalysisEndTimeGPS;
            //------------------------------
            //[VB]Get #nFile, , ElevationMask   public double ElevationMask; 
            Genba_S.ElevationMask = br.ReadDouble();                            //'仰角マスク(度)。
            ElevationMask = Genba_S.ElevationMask;
            //---------------------------------------------------------------------------
            //[VB]Get #nFile, , Interval        public double Interval; 
            Genba_S.Interval = br.ReadDouble();                               //'解析間隔(秒)。
            Interval = Genba_S.Interval;
            //---------------------------------------------------------------------------
            //[VB]Get #nFile, , Temperature     public double Temperature;
            Genba_S.Temperature = br.ReadDouble();                              //'気温(℃)。
            Temperature = Genba_S.Temperature;
            //---------------------------------------------------------------------------
            //[VB]Get #nFile, , Pressure        public double Pressure;
            Genba_S.Pressure = br.ReadDouble();                                 //'気圧(hPa)。
            Pressure = Genba_S.Pressure;
            //---------------------------------------------------------------------------
            //[VB]Get #nFile, , Humidity        public double Humidity;
            Genba_S.Humidity = br.ReadDouble();                                 //'湿度(％)。
            Humidity = Genba_S.Humidity;
            //---------------------------------------------------------------------------
            //[VB]Get #nFile, , Troposhere      public int Troposhere;           //'対流圏モデル。
            Genba_S.Troposhere = br.ReadInt32();                               //'対流圏モデル。
            Troposhere = (TROPOSHERE_TYPE)Genba_S.Troposhere;
            //---------------------------------------------------------------------------
            //[VB]If nVersion< 3100 Then
            //[VB]AnalysisFixed = False
            //[VB]Else
            //[VB]Get #nFile, , AnalysisFixed
            //[VB]End If
            //public bool AnalysisFixed;          //'解析始点固定点フラグ。True=解析始点が固定点。False=解析始点は固定点で無い。
            Genba_S.AnalysisFixed = nVersion < 3100 ? false : document.GetFileBool(br);
            AnalysisFixed = Genba_S.AnalysisFixed;
            //---------------------------------------------------------------------------
            //[VB]    If nVersion< 1700 Then
            //[VB]        AmbPercentage = 0
            //[VB]    Else
            //[VB]        Get #nFile, , AmbPercentage
            //[VB]    End If
            //public long AmbPercentage;            //'FIX率(％)。
            Genba_S.AmbPercentage = nVersion < 1700 ? 0 : br.ReadInt32();   //'FIX率(％)。
            AmbPercentage = Genba_S.AmbPercentage;
            //---------------------------------------------------------------------------
            //[VB] Get #nFile, , Bias       public double Bias;
            Genba_S.Bias = br.ReadDouble();                                 //'バイアス決定比(ｍ)。
            Bias = Genba_S.Bias;
            //---------------------------------------------------------------------------
            //[VB]Get #nFile, , EpochUsed       public long EpochUsed;
            Genba_S.EpochUsed = br.ReadInt32();                             //'使用エポック数。
            EpochUsed = Genba_S.EpochUsed;
            //---------------------------------------------------------------------------
            //[VB]Get #nFile, , EpochRejected   public long EpochRejected;
            Genba_S.EpochRejected = br.ReadInt32();                         //'棄却エポック数。
            EpochRejected = Genba_S.EpochRejected;
            //------------------------------
            //[VB]If 1600 <= nVersion And nVersion< 1700 Then
            //[VB]  Dim nObsAll As Long
            //[VB]  Dim nObsUsed As Long
            //[VB]  Get #nFile, , nObsAll
            //[VB]  Get #nFile, , nObsUsed
            //[VB]End If
            if (nVersion < 1600 && nVersion < 1700)
            {
                Genba_S.nObsAll = br.ReadInt32();
                long nObsAll = Genba_S.nObsAll;
                Genba_S.nObsUsed = br.ReadInt32();
                long nObsUsed = Genba_S.nObsUsed;
            }
            //---------------------------------------------------------------------------
            //[VB]Get #nFile, , RMS     public double RMS;
            Genba_S.RMS = br.ReadDouble();                                  //'RMS値(ｍ)。
            RMS = Genba_S.RMS;
            //---------------------------------------------------------------------------
            //[VB]Get #nFile, , RDOP    public double RDOP;        
            Genba_S.RDOP = br.ReadDouble();                                 //'RDOP値。
            RDOP = Genba_S.RDOP;
            //---------------------------------------------------------------------------
            //[VB]Get #nFile, , IsDispersion    public bool IsDispersion;
            Genba_S.IsDispersion = document.GetFileBool(br);                //'分散・共分散の有効/無効。True=有効。False=無効。
            IsDispersion = Genba_S.IsDispersion;
            //---------------------------------------------------------------------------
            //[VB]Get #nFile, , RcvNumbersGps   public long RcvNumbersGps;
            Genba_S.RcvNumbersGps = br.ReadInt32();                         //'受信GPS衛星番号。ビットフラグ。1=受信。0=非受信。ビット0がGPS1番、ビット1がGPS2番。。。。
            RcvNumbersGps = Genba_S.RcvNumbersGps;
            //************************************************************************
            if (nVersion < 8600)
            {
                Genba_S.RcvNumbersGlonass = 0;
                RcvNumbersGlonass = Genba_S.RcvNumbersGlonass;
                //'2017/06/06 NS6000対応。'''''''''''''''''''''''''''''''''''''''''''''''''''''''
                Genba_S.RcvNumbersQZSS = 0;
                RcvNumbersQZSS = Genba_S.RcvNumbersQZSS;
                //'       RcvNumbersGalileo = 0   '2018 / 08 / 21 Hitz H.Nakamura 衛星数が増えたので64ビットに増やす。
                Genba_S.RcvNumbersGalileo1 = 0;
                RcvNumbersGalileo1 = Genba_S.RcvNumbersGalileo1;
                Genba_S.RcvNumbersGalileo2 = 0;
                RcvNumbersGalileo2 = Genba_S.RcvNumbersGalileo2;
                Genba_S.RcvNumbersBeiDou1 = 0;
                RcvNumbersBeiDou1 = Genba_S.RcvNumbersBeiDou1;
                Genba_S.RcvNumbersBeiDou2 = 0;
                RcvNumbersBeiDou2 = Genba_S.RcvNumbersBeiDou2;
                //'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            }
            else
            {
                //Get #nFile, , RcvNumbersGlonass
                Genba_S.RcvNumbersGlonass = br.ReadInt32();
                if (nVersion < 9100)
                {
                    Genba_S.RcvNumbersQZSS = 0;
                    RcvNumbersQZSS = Genba_S.RcvNumbersQZSS;
                    //'           RcvNumbersGalileo = 0   '2018 / 08 / 21 Hitz H.Nakamura 衛星数が増えたので64ビットに増やす。
                    Genba_S.RcvNumbersGalileo1 = 0;
                    RcvNumbersGalileo1 = Genba_S.RcvNumbersGalileo1;
                    Genba_S.RcvNumbersGalileo2 = 0;
                    RcvNumbersGalileo2 = Genba_S.RcvNumbersGalileo2;
                    Genba_S.RcvNumbersBeiDou1 = 0;
                    RcvNumbersBeiDou1 = Genba_S.RcvNumbersBeiDou1;
                    Genba_S.RcvNumbersBeiDou2 = 0;
                    RcvNumbersBeiDou2 = Genba_S.RcvNumbersBeiDou2;
                    //'2018/08/21 Hitz H.Nakamura 衛星数が増えたので64ビットに増やす。
                    //'Else
                    //'     Get #nFile, , RcvNumbersQZSS
                    //'     Get #nFile, , RcvNumbersGalileo
                    //'     Get #nFile, , RcvNumbersBeiDou1
                    //'     Get #nFile, , RcvNumbersBeiDou2
                    //'End If
                } else if (nVersion < 9300)
                {
                    //[VB]Get #nFile, , RcvNumbersQZSS
                    Genba_S.RcvNumbersQZSS = br.ReadInt32();
                    RcvNumbersQZSS = Genba_S.RcvNumbersQZSS;
                    //[VB]Get #nFile, , RcvNumbersGalileo1
                    Genba_S.RcvNumbersGalileo1 = br.ReadInt32();
                    RcvNumbersGalileo1 = Genba_S.RcvNumbersGalileo1;
                    //[VB]Get #nFile, , RcvNumbersBeiDou1
                    Genba_S.RcvNumbersBeiDou1 = br.ReadInt32();
                    RcvNumbersBeiDou1 = Genba_S.RcvNumbersBeiDou1;
                    //[VB]Get #nFile, , RcvNumbersBeiDou2
                    Genba_S.RcvNumbersBeiDou2 = br.ReadInt32();
                    RcvNumbersBeiDou2 = Genba_S.RcvNumbersBeiDou2;
                    Genba_S.RcvNumbersGalileo2 = 0;
                    RcvNumbersGalileo2 = Genba_S.RcvNumbersGalileo2;
                } else
                {

                    //[VB]Get #nFile, , RcvNumbersQZSS
                    Genba_S.RcvNumbersQZSS = br.ReadInt32();
                    RcvNumbersQZSS = Genba_S.RcvNumbersQZSS;
                    //[VB]Get #nFile, , RcvNumbersGalileo1
                    Genba_S.RcvNumbersGalileo1 = br.ReadInt32();
                    RcvNumbersGalileo1 = Genba_S.RcvNumbersGalileo1;
                    //[VB]Get #nFile, , RcvNumbersGalileo2
                    Genba_S.RcvNumbersGalileo2 = br.ReadInt32();
                    RcvNumbersGalileo2 = Genba_S.RcvNumbersGalileo2;
                    //[VB]Get #nFile, , RcvNumbersBeiDou1
                    Genba_S.RcvNumbersBeiDou1 = br.ReadInt32();
                    RcvNumbersBeiDou1 = Genba_S.RcvNumbersBeiDou1;
                    //[VB]Get #nFile, , RcvNumbersBeiDou2
                    Genba_S.RcvNumbersBeiDou2 = br.ReadInt32();
                    RcvNumbersBeiDou2 = Genba_S.RcvNumbersBeiDou2;

                }
            }//if (nVersion < 8600)

            //************************************************************************
            if (nVersion < 1200) {
                Genba_S.MaxResidL1_Legacy = 0;
                MaxResidL1_Legacy = Genba_S.MaxResidL1_Legacy;
                Genba_S.MaxResidL2_Legacy = 0;
                MaxResidL2_Legacy = Genba_S.MaxResidL2_Legacy;
                Genba_S.MinRatio = 0;
                MinRatio = Genba_S.MinRatio;
                Genba_S.MinPs = 0;
                MinPs = Genba_S.MinPs;
                Genba_S.ExcludeGPS = 0;
                ExcludeGPS = (long)Genba_S.ExcludeGPS;
                Genba_S.ExcludeGlonass = 0;
                ExcludeGlonass = Genba_S.ExcludeGlonass;
                Genba_S.GlonassFlag = false;
                GlonassFlag = Genba_S.GlonassFlag;
                //'2017/06/06 NS6000対応。'''''''''''''''''''''''''''''''''''''''''''''''''''''''
                //ExcludeQZSS = 0
                //'       ExcludeGalileo = 0  '2018 / 08 / 21 Hitz H.Nakamura 衛星数が増えたので64ビットに増やす。
                Genba_S.ExcludeGalileo1 = 0;
                ExcludeGalileo1 = Genba_S.ExcludeGalileo1;
                Genba_S.ExcludeGalileo2 = 0;
                ExcludeGalileo2 = Genba_S.ExcludeGalileo2;
                Genba_S.ExcludeBeiDou1 = 0;
                ExcludeBeiDou1 = Genba_S.ExcludeBeiDou1;
                Genba_S.ExcludeBeiDou2 = 0;
                ExcludeBeiDou2 = Genba_S.ExcludeBeiDou2;
                Genba_S.QZSSFlag = false;
                QZSSFlag = Genba_S.QZSSFlag;
                Genba_S.GalileoFlag = false;
                GalileoFlag = Genba_S.GalileoFlag;
                Genba_S.BeiDouFlag = false;
                BeiDouFlag = Genba_S.BeiDouFlag;
                //'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            }
            else
            {
                //Get #nFile, , MaxResidL1_Legacy
                Genba_S.MaxResidL1_Legacy = br.ReadDouble();
                MaxResidL1_Legacy = Genba_S.MaxResidL1_Legacy;
                //Get #nFile, , MaxResidL2_Legacy
                Genba_S.MaxResidL2_Legacy = br.ReadDouble();
                MaxResidL2_Legacy = Genba_S.MaxResidL2_Legacy;
                //Get #nFile, , MinRatio
                Genba_S.MinRatio = br.ReadDouble();
                MinRatio = Genba_S.MinRatio;

                if (nVersion < 8700) {
                    Genba_S.MinPs = 0;
                    MinPs = Genba_S.MinPs;
                }
                else {
                    //Get #nFile, , MinPs
                    Genba_S.MinPs = br.ReadDouble();
                    MinPs = Genba_S.MinPs;
                }
                //Get #nFile, , ExcludeGPS
                Genba_S.ExcludeGPS = br.ReadInt32();
                ExcludeGPS = (long)Genba_S.ExcludeGPS;
                if (nVersion < 1300)
                {
                    Genba_S.MaxResidL1_Legacy = Genba_S.MaxResidL1_Legacy / 1000;
                    MaxResidL1_Legacy = Genba_S.MaxResidL1_Legacy;
                    Genba_S.MaxResidL2_Legacy = Genba_S.MaxResidL2_Legacy / 1000;
                    MaxResidL2_Legacy = Genba_S.MaxResidL2_Legacy;
                    Genba_S.ExcludeGPS = (long)Genba_S.ExcludeGPS ^ (long)0xFFFFFFFF;
                    ExcludeGPS = (long)Genba_S.ExcludeGPS;
                }//End If
                if (nVersion < 8600)
                {
                    Genba_S.ExcludeGlonass = 0;
                    ExcludeGlonass = Genba_S.ExcludeGlonass;
                    Genba_S.GlonassFlag = false;
                    GlonassFlag = Genba_S.GlonassFlag;
                    //'2017/06/06 NS6000対応。'''''''''''''''''''''''''''''''''''''''''''''''''''''''
                    Genba_S.ExcludeQZSS = 0;
                    ExcludeQZSS = Genba_S.ExcludeQZSS;
                    //'           ExcludeGalileo = 0  '2018 / 08 / 21 Hitz H.Nakamura 衛星数が増えたので64ビットに増やす。
                    Genba_S.ExcludeGalileo1 = 0;
                    ExcludeGalileo1 = Genba_S.ExcludeGalileo1;
                    Genba_S.ExcludeGalileo2 = 0;
                    ExcludeGalileo2 = Genba_S.ExcludeGalileo2;
                    Genba_S.ExcludeBeiDou1 = 0;
                    ExcludeBeiDou1 = Genba_S.ExcludeBeiDou1;
                    Genba_S.ExcludeBeiDou2 = 0;
                    ExcludeBeiDou2 = Genba_S.ExcludeBeiDou2;
                    Genba_S.QZSSFlag = false;
                    QZSSFlag = Genba_S.QZSSFlag;
                    Genba_S.GalileoFlag = false;
                    GalileoFlag = Genba_S.GalileoFlag;
                    Genba_S.BeiDouFlag = false;
                    BeiDouFlag = Genba_S.BeiDouFlag;
                    //'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                }
                else
                {
                    //Get #nFile, , ExcludeGlonass
                    Genba_S.ExcludeGlonass = br.ReadInt32();
                    ExcludeGlonass = Genba_S.ExcludeGlonass;
                    //Get #nFile, , GlonassFlag
                    Genba_S.GlonassFlag = document.GetFileBool(br);
                    GlonassFlag = Genba_S.GlonassFlag;
                    //'2017/06/06 NS6000対応。'''''''''''''''''''''''''''''''''''''''''''''''''''''''
                    if (nVersion < 9100)
                    {
                        Genba_S.ExcludeQZSS = 0;
                        ExcludeQZSS = Genba_S.ExcludeQZSS;
                        //'             ExcludeGalileo = 0  '2018 / 08 / 21 Hitz H.Nakamura 衛星数が増えたので64ビットに増やす。
                        Genba_S.ExcludeGalileo1 = 0;
                        ExcludeGalileo1 = Genba_S.ExcludeGalileo1;
                        Genba_S.ExcludeGalileo2 = 0;
                        ExcludeGalileo2 = Genba_S.ExcludeGalileo2;
                        Genba_S.ExcludeBeiDou1 = 0;
                        ExcludeBeiDou1 = Genba_S.ExcludeBeiDou1;
                        Genba_S.ExcludeBeiDou2 = 0;
                        ExcludeBeiDou2 = Genba_S.ExcludeBeiDou2;
                        Genba_S.QZSSFlag = false;
                        QZSSFlag = Genba_S.QZSSFlag;
                        Genba_S.GalileoFlag = false;
                        GalileoFlag = Genba_S.GalileoFlag;
                        Genba_S.BeiDouFlag = false;
                        BeiDouFlag = Genba_S.BeiDouFlag;
                    }
                    else
                    {
                        //Get #nFile, , ExcludeQZSS
                        Genba_S.ExcludeQZSS = br.ReadInt32();
                        ExcludeQZSS = Genba_S.ExcludeQZSS;
                        //'               Get #nFile, , ExcludeGalileo    '2018 / 08 / 21 Hitz H.Nakamura 衛星数が増えたので64ビットに増やす。
                        if (nVersion < 9300)
                        {
                            //Get #nFile, , ExcludeGalileo1
                            Genba_S.ExcludeGalileo1 = br.ReadInt32();
                            ExcludeGalileo1 = Genba_S.ExcludeGalileo1;
                            Genba_S.ExcludeGalileo2 = 0;
                            ExcludeGalileo2 = Genba_S.ExcludeGalileo2;
                        }
                        else
                        {
                            //Get #nFile, , ExcludeGalileo1
                            Genba_S.ExcludeGalileo1 = br.ReadInt32();
                            ExcludeGalileo1 = Genba_S.ExcludeGalileo1;
                            //Get #nFile, , ExcludeGalileo2
                            Genba_S.ExcludeGalileo2 = br.ReadInt32();
                            ExcludeGalileo2 = Genba_S.ExcludeGalileo2;
                        }
                        //Get #nFile, , ExcludeBeiDou1
                        Genba_S.ExcludeBeiDou1 = br.ReadInt32();
                        ExcludeBeiDou1 = Genba_S.ExcludeBeiDou1;
                        //Get #nFile, , ExcludeBeiDou2
                        Genba_S.ExcludeBeiDou2 = br.ReadInt32();
                        ExcludeBeiDou2 = Genba_S.ExcludeBeiDou2;
                        //Get #nFile, , QZSSFlag
                        Genba_S.QZSSFlag = document.GetFileBool(br);
                        QZSSFlag = Genba_S.QZSSFlag;
                        //Get #nFile, , GalileoFlag
                        Genba_S.GalileoFlag = document.GetFileBool(br);
                        GalileoFlag = Genba_S.GalileoFlag;
                        //Get #nFile, , BeiDouFlag
                        Genba_S.BeiDouFlag = document.GetFileBool(br);
                        BeiDouFlag = Genba_S.BeiDouFlag;
                    }
                    //'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                }
            }
            //************************************************************************
            if (nVersion < 2300) {
                Genba_S.m_nLineType = (int)OBJ_MODE.OBJ_MODE_ADOPT;
                m_nLineType = (OBJ_MODE)Genba_S.m_nLineType;
            }
            else if (nVersion < 5000)
            {
                //--------------------------------
                //Dim bAdopt As Boolean
                bool bAdopt;
                //Get #nFile, , bAdopt
                bAdopt = document.GetFileBool(br);
                //--------------------------------
                if (bAdopt) {
                    Genba_S.m_nLineType = (int)OBJ_MODE.OBJ_MODE_ADOPT;
                    m_nLineType = (OBJ_MODE)Genba_S.m_nLineType;
                } else {
                    Genba_S.m_nLineType = (int)OBJ_MODE.OBJ_MODE_CHECK;
                    m_nLineType = (OBJ_MODE)Genba_S.m_nLineType;
                }
                //--------------------------------
            }
            else
            {
                //Get #nFile, , m_nLineType
                Genba_S.m_nLineType = br.ReadInt32();
                m_nLineType = (OBJ_MODE)Genba_S.m_nLineType;
            }

            //************************************************************************
            if (nVersion < 3800)
            {
                Genba_S.StrPcvVer = null;
                StrPcvVer = Genba_S.StrPcvVer;
                Genba_S.EndPcvVer = null;
                EndPcvVer = Genba_S.EndPcvVer;
            }
            else
            {
                //Get #nFile, , StrPcvVer
                Genba_S.StrPcvVer = FileRead_GetString(br);
                StrPcvVer = Genba_S.StrPcvVer;
                //Get #nFile, , EndPcvVer
                Genba_S.EndPcvVer = FileRead_GetString(br);
                EndPcvVer = Genba_S.EndPcvVer;
            }

            //************************************************************************
            if (nVersion < 4900)
            {
                Genba_S.AnalysisOrder = 0xFFFFFFF;  //'最低順位。
                AnalysisOrder = Genba_S.AnalysisOrder;
            }
            else
            {
                //Get #nFile, , AnalysisOrder
                Genba_S.AnalysisOrder = br.ReadInt32();
                AnalysisOrder = Genba_S.AnalysisOrder;
                if (nVersion < 5900)
                {
                    Genba_S.AnalysisOrder = Genba_S.AnalysisOrder & 0xFFFFFFF;  //'上位４ビットは潰しとく。
                    AnalysisOrder = Genba_S.AnalysisOrder;
                    Genba_S.AnalysisOrder = Genba_S.AnalysisOrder | 0x8000000;  //'第２８ビットをONにする(非解析)。
                    AnalysisOrder = Genba_S.AnalysisOrder;
                }
            }
            //************************************************************************
            //[VB]  Call m_clsStrPoint.Load(nFile, nVersion, clsObservationPoints, Nothing)
            //-----------------------------------------------------------------------

            //23/12/20 K.Setoguchi---->>>>
            //-----------------------------------------------------
            //(del)     ObservationPoint observationPoint = new ObservationPoint();
            //(del)     observationPoint.Load(br, nVersion, ref Genba_S);
            //-----------------------------------------------------
            List<OPA_STRUCT_SUB> OPA_ListStrA = new List<OPA_STRUCT_SUB>();
            Genba_S.OPA_ListStr = OPA_ListStrA;
            //-----------------------------------------------------
            //ObservationPoint observationPoint = new ObservationPoint();
            //observationPoint.Load(br, nVersion, ref Genba_S, ref Genba_S.OPA_ListStr);
            m_clsStrPoint.Load(br, nVersion, ref Genba_S, ref Genba_S.OPA_ListStr, ref clsObservationPoints, null);
            //-----------------------------------------------------
            //<<<<----23/12/20 K.Setoguchi

            //************************************************************************
            //[VB]  Call m_clsEndPoint.Load(nFile, nVersion, clsObservationPoints, Nothing)
            //------------------------------------------------------------------------

            //23/12/20 K.Setoguchi---->>>>
            //-----------------------------------------------------
            //(del)     observationPoint.Load(br, nVersion, ref Genba_S);
            //-----------------------------------------------------
            List<OPA_STRUCT_SUB> OPA_ListStrB = new List<OPA_STRUCT_SUB>();
            Genba_S.OPA_ListEnd = OPA_ListStrB;
            //-----------------------------------------------------
            //observationPoint.Load(br, nVersion, ref Genba_S, ref Genba_S.OPA_ListEnd);
            m_clsEndPoint.Load(br, nVersion, ref Genba_S, ref Genba_S.OPA_ListEnd, ref clsObservationPoints, null);
            //-----------------------------------------------------
            //<<<<----23/12/20 K.Setoguchi


            //----------------------------------------
            //[VB]  Dim bRevers As Boolean                      //false
            //[VB]  Get #nFile, , bRevers
            //[VB]  Revers = bRevers
            bool bRevers = document.GetFileBool(br);
            Revers(bRevers);

            //----------------------------------------
            //[VB]  Dim nCoordinateType As COORDINATE_TYPE
            //[VB]  If nVersion< 6500 Then
            //[VB]      nCoordinateType = COORDINATE_XYZ
            //[VB]  Else
            //[VB]      Get #nFile, , nCoordinateType
            //[VB]  End If
            //
            int nCoordinateType;
            if (nVersion < 6500)
            {
                //'座標値種別。   COORDINATE_TYPE     COORDINATE_XYZ = 0, //'XYZ座標値。
                nCoordinateType = (int)COORDINATE_TYPE.COORDINATE_XYZ;
            }
            else
            {
                nCoordinateType = br.ReadInt32();
            }

            //-----------------------------------------------------------------------------------
            //[VB]  If nCoordinateType = COORDINATE_FIX Then
            //[VB]      Set m_clsCoordinateAnalysis = New CoordinatePointFix
            //[VB]  Else
            //[VB]      Set m_clsCoordinateAnalysis = New CoordinatePointXYZ
            //[VB]  End If
            //[VB]  Call m_clsCoordinateAnalysis.Load(nFile, nVersion)
            //
            CoordinatePointFix coordinatePointFix = new CoordinatePointFix();
            CoordinatePointXYZ coordinatePointXYZ = new CoordinatePointXYZ();
            if (nCoordinateType == (int)COORDINATE_TYPE.COORDINATE_FIX)
            {
                coordinatePointFix.Load(br, nVersion, ref Genba_S);
            }
            else
            {
                coordinatePointXYZ.Load(br, nVersion, ref Genba_S);
            }
            //-----------------------------------------------------------------------------------
            //[VB]  Call m_clsVectorAnalysis.Load(nFile, nVersion)
            //coordinatePointXYZ.Load(br, nVersion, ref Genba_S);
            m_clsVectorAnalysis.Load(br, nVersion, ref Genba_S);

            //-----------------------------------------------------------------------------------
            //[VB]  Call m_clsDispersion.Load(nFile, nVersion)
            //Dispersion dispersion = new Dispersion();
            //dispersion.Load(br, ref Genba_S);
            m_clsDispersion.Load(br, ref Genba_S);

            //-----------------------------------------------------------------------------------
            //[VB]  Call m_clsStrDepPattern.Load(nFile, nVersion)
            //DepPattern depPattern = new DepPattern();
            //depPattern.Load(br, nVersion, ref Genba_S);
            m_clsStrDepPattern.Load(br, nVersion, ref Genba_S);

            //-----------------------------------------------------------------------------------
            //[VB]  Call m_clsEndDepPattern.Load(nFile, nVersion)
            //depPattern.Load(br, nVersion, ref Genba_S);
            m_clsEndDepPattern.Load(br, nVersion, ref Genba_S);

            //-----------------------------------------------------------------------------------
            //[VB]  If nVersion >= 1700 Then Call m_clsObsInfo.Load(nFile, nVersion)
            if (nVersion >= 1700) {
                //ObsInfo obsInfo = new ObsInfo();
                //obsInfo.Load(br, nVersion, ref Genba_S);
                m_clsObsInfo.Load(br, nVersion, ref Genba_S);
            }

            //-----------------------------------------------------------------------------------
            //[VB]  Call m_clsAmbInfo.Load(nFile, nVersion)
            //AmbInfo ambInfo = new AmbInfo();
            //ambInfo.Load(br, nVersion, ref Genba_S);
            m_clsAmbInfo.Load(br, nVersion, ref Genba_S);

            //-----------------------------------------------------------------------------------
            //[VB]  Call m_clsStrOffsetL1.Load(nFile, nVersion)
            //coordinatePointXYZ.Load(br, nVersion, ref Genba_S);
            m_clsStrOffsetL1.Load(br, nVersion, ref Genba_S);

            //-----------------------------------------------------------------------------------
            //[VB]  Call m_clsStrOffsetL2.Load(nFile, nVersion)
            //coordinatePointXYZ.Load(br, nVersion, ref Genba_S);
            m_clsStrOffsetL2.Load(br, nVersion, ref Genba_S);

            //-----------------------------------------------------------------------------------
            //[VB]  If nVersion >= 1800 Then
            //[VB]      Call m_clsEndOffsetL1.Load(nFile, nVersion)
            //[VB]      Call m_clsEndOffsetL2.Load(nFile, nVersion)
            //[VB]  End If
            if (nVersion >= 1800)
            {
                //coordinatePointXYZ.Load(br, nVersion, ref Genba_S);
                m_clsEndOffsetL1.Load(br, nVersion, ref Genba_S);
                //coordinatePointXYZ.Load(br, nVersion, ref Genba_S);
                m_clsEndOffsetL2.Load(br, nVersion, ref Genba_S);
            }
            //-----------------------------------------------------------------------------------
            //[VB]  If nVersion >= 9300 Then
            //[VB]      Call m_clsStrOffsetL5.Load(nFile, nVersion)
            //[VB]      Call m_clsEndOffsetL5.Load(nFile, nVersion)
            //[VB]  End If
            //
            if (nVersion >= 9300)
            {
                m_clsStrOffsetL5.Load(br, nVersion, ref Genba_S);
                m_clsEndOffsetL5.Load(br, nVersion, ref Genba_S);
            }
            //-----------------------------------------------------------------------------------


            //-----------------------------------------------------------------------------------
            //[VB]  If nVersion< 4800 Then
            //[VB]      m_clsObsDataMask.Count = 0
            //[VB]  Else
            //[VB]      Call m_clsObsDataMask.Load(nFile, nVersion)
            //[VB]   End If
            if (nVersion < 4800)
            {
                m_clsObsDataMask.Count(0);
            }
            else
            {
                //ObsDataMask obsDataMask = new ObsDataMask();
                //obsDataMask.Load(br, nVersion, ref Genba_S);
                m_clsObsDataMask.Load(br, nVersion, ref Genba_S);
            }



        }


        //****************************************
        //****************************************
    }

}
