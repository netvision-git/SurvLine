using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using System.Windows.Forms;
using static SurvLine.mdl.MdlBaseLineAnalyser;
using System.IO;

namespace SurvLine
{
    public class BaseLineAnalysisParam
    {
        //==========================================================================================
        /*[VB]
        '*******************************************************************************
        '基線解析パラメータ

        Option Explicit
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'プロパティ
        Public ElevationMask As Long '仰角マスク(度)。
        Public Interval As Long '解析間隔(秒)。
        Public Orbit As ORBIT_TYPE '軌道暦。
        Public Frequency As FREQUENCY_TYPE '周波数。
        Public ThresholdLC As Long 'LCしきい値(ｍ)。
        Public SolveMode As SOLVEMODE_TYPE '解析モード。
        Public TimeType As TIME_TYPE '時間種別。
        Public StrTimeGPS As Date '開始日時(GPS)。
        Public EndTimeGPS As Date '終了日時(GPS)。
        Public Troposhere As TROPOSHERE_TYPE '対流圏モデル。
        Public Temperature As Double '気温(℃)。
        Public Pressure As Double '気圧(hPa)。
        Public Humidity As Double '湿度(％)。
        Public TropEst As Boolean '対流圏遅延評価フラグ。
        Public TropMap As TROPMAP_TYPE '対流圏のマッピング。
        Public MinWindowLength As Double '間隔。
        Public PrioriConstraint As Double '初期値。
        Public RelativeConstraint As Double '相対値。
        Public MaxResidL1 As Double '最大残差L1。
        Public MaxResidL2 As Double '最大残差L2。
        Public MaxResidL3 As Double '最大残差L3。
        Public MaxResidL5 As Double '最大残差L5。
        Public MaxResidCode As Double '最大残差(Code)。
        Public MaxResidPhase As Double '最大残差(Phase)。
        Public MinRatio As Double '最小バイアス決定比。
        Public MinPs As Double '最小 probability of success。
        Public TropoFlag As Integer '対流圏遅延推定フラグ。0＝使用しない、0≠使用する。'2018/07/17 H.Nakamura 電離層遅延推定パラメータの追加にともない大気遅延も選択できるようにする。
        Public TropoAprSig As Double '対流圏遅延推定に与える初期シグマ。
        Public TropoNoise As Double '対流圏遅延推定に与える毎エポックの分散。
        Public IonoFlag As Integer '電離層遅延推定フラグ。0＝自動、0≠任意指定。'2018/07/17 H.Nakamura 電離層遅延推定パラメータの追加。
        Public IonoAprSig As Double '電離層遅延推定に与える初期シグマ。'2018/07/17 H.Nakamura 電離層遅延推定パラメータの追加。
        Public ExcludeGPS As Long '不使用GPS。ビットフラグ。1=使用。0=無使用。ビット0がGPS1番、ビット1がGPS2番。。。。
        Public ExcludeGlonass As Long '不使用GLONASS。ビットフラグ。1=使用。0=無使用。ビット0がR1番、ビット1がR2番。。。。
        Public GlonassFlag As Integer 'GLONASSフラグ。0＜GLONASS ON、0＝GLONASS OFF、0＞GLONASS ONだけど無効化。
        '2017/06/13 NS6000対応。'''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Public ExcludeQZSS As Long '不使用QZSS。ビットフラグ。1=使用。0=無使用。ビット0がJ1番、ビット1がJ2番。。。。
        'Public ExcludeGalileo As Long '不使用Galileo。ビットフラグ。1=使用。0=無使用。ビット0がE1番、ビット1がE2番。。。。 '2018/08/21 Hitz H.Nakamura 衛星数が増えたので64ビットに増やす。
        Public ExcludeGalileo1 As Long '不使用Galileo。ビットフラグ。1=使用。0=無使用。ビット0がE1番、ビット1がE2番。。。。
        Public ExcludeGalileo2 As Long '不使用Galileo。ビットフラグ。1=使用。0=無使用。ビット0がE33番、ビット1がE34番。。。。
        Public ExcludeBeiDou1 As Long '不使用BeiDou。ビットフラグ。1=使用。0=無使用。ビット0がC1番、ビット1がC2番。。。。
        Public ExcludeBeiDou2 As Long '不使用BeiDou。ビットフラグ。1=使用。0=無使用。ビット0がC33番、ビット1がC34番。。。。
        Public QZSSFlag As Integer 'QZSSフラグ。0＜QZSS ON、0＝QZSS OFF、0＞QZSS ONだけど無効化。
        Public GalileoFlag As Integer 'Galileoフラグ。0＜Galileo ON、0＝Galileo OFF、0＞Galileo ONだけど無効化。
        Public BeiDouFlag As Integer 'BeiDouフラグ。0＜BeiDou ON、0＝BeiDou OFF、0＞BeiDou ONだけど無効化。
        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Public NavigationPath As String '精密暦ファイルのパス。
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'プロパティ
        public long ElevationMask;          //仰角マスク(度)。
        public long Interval;               //解析間隔(秒)。
        public ORBIT_TYPE Orbit;            //軌道暦。
#if false
        /*
         *************************** 修正要 sakai
         */
        public FREQUENCY_TYP EFrequency;    //周波数。
#endif
        public long ThresholdLC;            //LCしきい値(ｍ)。
        public SOLVEMODE_TYPE SolveMode;    //解析モード。
        public TIME_TYPE TimeType;          //時間種別。
        public DateTime StrTimeGPS;         //開始日時(GPS)。
        public DateTime EndTimeGPS;         //終了日時(GPS)。
        public TROPOSHERE_TYPE Troposhere;  //対流圏モデル。
        public double Temperature;          //気温(℃)。
        public double Pressure;             //気圧(hPa)。
        public double Humidity;             //湿度(％)。
        public double TropEst;              //対流圏遅延評価フラグ。
        public TROPMAP_TYPE TropMap;        //対流圏のマッピング。
        public double MinWindowLength;      //間隔。
        public double PrioriConstraint;     //初期値。
        public double RelativeConstraint;   //相対値。
        public double MaxResidL1;           //最大残差L1。
        public double MaxResidL2;           //最大残差L2。
        public double MaxResidL3;           //最大残差L3。
        public double MaxResidL5;           //最大残差L5。
        public double MaxResidCode;         //最大残差(Code)。
        public double MaxResidPhase;        //最大残差(Phase)。
        public double MinRatio;             //最小バイアス決定比。
        public double MinPs;                //最小 probability of success。
        public int TropoFlag;               //対流圏遅延推定フラグ。0＝使用しない、0≠使用する。'2018/07/17 H.Nakamura 電離層遅延推定パラメータの追加にともない大気遅延も選択できるようにする。
        public double TropoAprSig;          //対流圏遅延推定に与える初期シグマ。
        public double TropoNoise;           //対流圏遅延推定に与える毎エポックの分散。
        public int IonoFlag;                //電離層遅延推定フラグ。0＝自動、0≠任意指定。'2018/07/17 H.Nakamura 電離層遅延推定パラメータの追加。
        public double IonoAprSig;           //電離層遅延推定に与える初期シグマ。'2018/07/17 H.Nakamura 電離層遅延推定パラメータの追加。
        public long ExcludeGPS;             //不使用GPS。ビットフラグ。1=使用。0=無使用。ビット0がGPS1番、ビット1がGPS2番。。。。
        public long ExcludeGlonass;         //不使用GLONASS。ビットフラグ。1=使用。0=無使用。ビット0がR1番、ビット1がR2番。。。。
        public int GlonassFlag;             //GLONASSフラグ。0＜GLONASS ON、0＝GLONASS OFF、0＞GLONASS ONだけど無効化。
        //2017/06/13 NS6000対応。'''''''''''''''''''''''''''''''''''''''''''''''''''''''
        public long ExcludeQZSS;            //不使用QZSS。ビットフラグ。1=使用。0=無使用。ビット0がJ1番、ビット1がJ2番。。。。
        //public ExcludeGalileo;Long        //不使用Galileo。ビットフラグ。1=使用。0=無使用。ビット0がE1番、ビット1がE2番。。。。 //2018/08/21 Hitz H.Nakamura 衛星数が増えたので64ビットに増やす。
        public long ExcludeGalileo1;        //不使用Galileo。ビットフラグ。1=使用。0=無使用。ビット0がE1番、ビット1がE2番。。。。
        public long ExcludeGalileo2;        //不使用Galileo。ビットフラグ。1=使用。0=無使用。ビット0がE33番、ビット1がE34番。。。。
        public long ExcludeBeiDou1;         //不使用BeiDou。ビットフラグ。1=使用。0=無使用。ビット0がC1番、ビット1がC2番。。。。
        public long ExcludeBeiDou2;         //不使用BeiDou。ビットフラグ。1=使用。0=無使用。ビット0がC33番、ビット1がC34番。。。。
        public int QZSSFlag;                //QZSSフラグ。0＜QZSS ON、0＝QZSS OFF、0＞QZSS ONだけど無効化。
        public int GalileoFlag;             //Galileoフラグ。0＜Galileo ON、0＝Galileo OFF、0＞Galileo ONだけど無効化。
        public int BeiDouFlag;              //BeiDouフラグ。0＜BeiDou ON、0＝BeiDou OFF、0＞BeiDou ONだけど無効化。
        //''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        public string NavigationPath;       //精密暦ファイルのパス。
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'インプリメンテーション
        Private m_sOrders() As String '基線解析順序。BaseLineVector の Key。配列の要素は(-1 To ...)、要素 -1 は未使用。
        Private m_bRevers() As Boolean '反転フラグ。True=反転。False=反転無し。配列の要素は(-1 To ...)、要素 -1 は未使用。
        Private m_bChecks() As Boolean 'チェックフラグ。True=基線解析の対象とする。False=基線解析から除外。配列の要素は(-1 To ...)、要素 -1 は未使用。
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'インプリメンテーション
        private string[] m_sOrders;         //'基線解析順序。BaseLineVector の Key。配列の要素は(-1 To ...)、要素 -1 は未使用。
        private bool[] m_bRevers;           //'反転フラグ。True=反転。False=反転無し。配列の要素は(-1 To ...)、要素 -1 は未使用。
        private bool[] m_bChecks;           //'チェックフラグ。True=基線解析の対象とする。False=基線解析から除外。配列の要素は(-1 To ...)、要素 -1 は未使用。
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '*******************************************************************************
        'プロパティ

        '基線解析パラメータ。
        '
        'このプロパティはデフォルトプロパティである。Letを使えば｢値の代入｣ができる。いわゆるC++で言うところのoperator=である。
        '
        '引き数：
        'clsBaseLineAnalysisParam コピー元のオブジェクト。
        Property Let BaseLineAnalysisParam(ByVal clsBaseLineAnalysisParam As BaseLineAnalysisParam)

            ElevationMask = clsBaseLineAnalysisParam.ElevationMask
            Interval = clsBaseLineAnalysisParam.Interval
            Orbit = clsBaseLineAnalysisParam.Orbit
            Frequency = clsBaseLineAnalysisParam.Frequency
            ThresholdLC = clsBaseLineAnalysisParam.ThresholdLC
            SolveMode = clsBaseLineAnalysisParam.SolveMode
            TimeType = clsBaseLineAnalysisParam.TimeType
            StrTimeGPS = clsBaseLineAnalysisParam.StrTimeGPS
            EndTimeGPS = clsBaseLineAnalysisParam.EndTimeGPS
            Troposhere = clsBaseLineAnalysisParam.Troposhere
            Temperature = clsBaseLineAnalysisParam.Temperature
            Pressure = clsBaseLineAnalysisParam.Pressure
            Humidity = clsBaseLineAnalysisParam.Humidity
            TropEst = clsBaseLineAnalysisParam.TropEst
            TropMap = clsBaseLineAnalysisParam.TropMap
            MinWindowLength = clsBaseLineAnalysisParam.MinWindowLength
            PrioriConstraint = clsBaseLineAnalysisParam.PrioriConstraint
            RelativeConstraint = clsBaseLineAnalysisParam.RelativeConstraint
            MaxResidL1 = clsBaseLineAnalysisParam.MaxResidL1
            MaxResidL2 = clsBaseLineAnalysisParam.MaxResidL2
            MaxResidL3 = clsBaseLineAnalysisParam.MaxResidL3
            MaxResidL5 = clsBaseLineAnalysisParam.MaxResidL5
            MaxResidCode = clsBaseLineAnalysisParam.MaxResidCode
            MaxResidPhase = clsBaseLineAnalysisParam.MaxResidPhase
            MinRatio = clsBaseLineAnalysisParam.MinRatio
            MinPs = clsBaseLineAnalysisParam.MinPs
            TropoFlag = clsBaseLineAnalysisParam.TropoFlag '2018/07/17 H.Nakamura 電離層遅延推定パラメータの追加にともない大気遅延も選択できるようにする。
            TropoAprSig = clsBaseLineAnalysisParam.TropoAprSig
            TropoNoise = clsBaseLineAnalysisParam.TropoNoise
            IonoFlag = clsBaseLineAnalysisParam.IonoFlag '2018/07/17 H.Nakamura 電離層遅延推定パラメータの追加。
            IonoAprSig = clsBaseLineAnalysisParam.IonoAprSig '2018/07/17 H.Nakamura 電離層遅延推定パラメータの追加。
            ExcludeGPS = clsBaseLineAnalysisParam.ExcludeGPS
            ExcludeGlonass = clsBaseLineAnalysisParam.ExcludeGlonass
            GlonassFlag = clsBaseLineAnalysisParam.GlonassFlag
            '2017/06/06 NS6000対応。'''''''''''''''''''''''''''''''''''''''''''''''''''''''
            ExcludeQZSS = clsBaseLineAnalysisParam.ExcludeQZSS
        '   ExcludeGalileo = clsBaseLineAnalysisParam.ExcludeGalileo    '2018/08/21 Hitz H.Nakamura 衛星数が増えたので64ビットに増やす。
            ExcludeGalileo1 = clsBaseLineAnalysisParam.ExcludeGalileo1
            ExcludeGalileo2 = clsBaseLineAnalysisParam.ExcludeGalileo2
            ExcludeBeiDou1 = clsBaseLineAnalysisParam.ExcludeBeiDou1
            ExcludeBeiDou2 = clsBaseLineAnalysisParam.ExcludeBeiDou2
            QZSSFlag = clsBaseLineAnalysisParam.QZSSFlag
            GalileoFlag = clsBaseLineAnalysisParam.GalileoFlag
            BeiDouFlag = clsBaseLineAnalysisParam.BeiDouFlag
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            NavigationPath = clsBaseLineAnalysisParam.NavigationPath


            Dim sOrders() As String
            Dim bRevers() As Boolean
            Dim bChecks() As Boolean
            sOrders = clsBaseLineAnalysisParam.Orders
            bRevers = clsBaseLineAnalysisParam.Revers
            bChecks = clsBaseLineAnalysisParam.Checks
            ReDim m_sOrders(-1 To UBound(sOrders))
            ReDim m_bRevers(-1 To UBound(sOrders))
            ReDim m_bChecks(-1 To UBound(sOrders))
            Dim i As Long
            For i = 0 To UBound(m_sOrders)
                m_sOrders(i) = sOrders(i)
                m_bRevers(i) = bRevers(i)
                m_bChecks(i) = bChecks(i)
            Next

        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '基線解析順序。要素は KEYPREFIX ＋ BaseLineVector オブジェクトのポインタ。配列の要素は(-1 To ...)、要素 -1 は未使用。
        Property Get Orders() As String()
            Orders = m_sOrders
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '反転フラグ。True=反転。False=反転無し。配列の要素は(-1 To ...)、要素 -1 は未使用。
        Property Get Revers() As Boolean()
            Revers = m_bRevers
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'チェックフラグ。True=基線解析の対象とする。False=基線解析から除外。配列の要素は(-1 To ...)、要素 -1 は未使用。
        Property Get Checks() As Boolean()
            Checks = m_bChecks
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '*******************************************************************************
        'イベント

        '初期化。
        Private Sub Class_Initialize()

            On Error GoTo ErrorHandler


            ElevationMask = 15
            Interval = -1
            Orbit = ORBIT_BROADCAST
            Frequency = -1
            ThresholdLC = 10000
            SolveMode = DEFAULT_SOLVEMODE
            TimeType = TIME_PROJECT
            StrTimeGPS = MIN_TIME
            EndTimeGPS = MIN_TIME
            Troposhere = DEFAULT_TROPOSHERE
            Temperature = DEFAULT_TEMPERATURE
            Pressure = DEFAULT_PRESSURE
            Humidity = DEFAULT_HUMIDITY
            TropEst = DEFAULT_TROPEST
            TropMap = DEFAULT_TROPMAP
            MinWindowLength = DEFAULT_MINWINDOWLENGTH
            PrioriConstraint = DEFAULT_PRIORICONSTRAINT
            RelativeConstraint = DEFAULT_RELATIVECONSTRAINT
            MaxResidL1 = DEFAULT_MAXRESIDL1
            MaxResidL2 = DEFAULT_MAXRESIDL2
            MaxResidL3 = DEFAULT_MAXRESIDL3
            MaxResidL5 = DEFAULT_MAXRESIDL5
            MaxResidCode = DEFAULT_MAXRESIDCODE
            MaxResidPhase = DEFAULT_MAXRESIDPHASE
            MinRatio = DEFAULT_MINRATIO
            MinPs = DEFAULT_MINPS
            TropoFlag = 0 '2018/07/17 H.Nakamura 電離層遅延推定パラメータの追加にともない大気遅延も選択できるようにする。
            TropoAprSig = DEFAULT_TROPOAPRSIG
            TropoNoise = DEFAULT_TROPONOISE
            IonoFlag = 0 '2018/07/17 H.Nakamura 電離層遅延推定パラメータの追加。
            IonoAprSig = DEFAULT_IONOAPRSIG '2018/07/17 H.Nakamura 電離層遅延推定パラメータの追加。
            ExcludeGPS = 0
            ExcludeGlonass = 0
            GlonassFlag = -1
            '2017/06/06 NS6000対応。'''''''''''''''''''''''''''''''''''''''''''''''''''''''
            ExcludeQZSS = 0
        '   ExcludeGalileo = 0  '2018/08/21 Hitz H.Nakamura 衛星数が増えたので64ビットに増やす。
            ExcludeGalileo1 = 0
            ExcludeGalileo2 = 0
            ExcludeBeiDou1 = 0
            ExcludeBeiDou2 = 0
            QZSSFlag = -1
            GalileoFlag = -1
            BeiDouFlag = -1
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            NavigationPath = ""


            ReDim m_sOrders(-1 To -1)
            ReDim m_bRevers(-1 To -1)
            ReDim m_bChecks(-1 To -1)


            Exit Sub


        ErrorHandler:
            Call mdlMain.ErrorExit


        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '*******************************************************************************
        'メソッド

        '保存。
        '
        '引き数：
        'nFile ファイル番号。
        Public Sub Save(ByVal nFile As Integer)

            Put #nFile, , ElevationMask
            Put #nFile, , Interval
            Put #nFile, , Orbit
            Put #nFile, , Frequency
            Put #nFile, , ThresholdLC
            Put #nFile, , SolveMode
            Put #nFile, , TimeType
            Put #nFile, , StrTimeGPS
            Put #nFile, , EndTimeGPS
            Put #nFile, , Troposhere
            Put #nFile, , Temperature
            Put #nFile, , Pressure
            Put #nFile, , Humidity
            Put #nFile, , TropEst
            Put #nFile, , TropMap
            Put #nFile, , MinWindowLength
            Put #nFile, , PrioriConstraint
            Put #nFile, , RelativeConstraint
            Put #nFile, , MaxResidL1
            Put #nFile, , MaxResidL2
            Put #nFile, , MaxResidL3
            Put #nFile, , MaxResidL5
            Put #nFile, , MaxResidCode
            Put #nFile, , MaxResidPhase
            Put #nFile, , MinRatio
            Put #nFile, , MinPs
            Put #nFile, , TropoFlag '2018/07/17 H.Nakamura 電離層遅延推定パラメータの追加にともない大気遅延も選択できるようにする。
            '2016/02/06 H.Nakamura 大気遅延を推定しない。'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            'Put #nFile, , TropoAprSig
            'Put #nFile, , TropoNoise
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            Put #nFile, , TropoAprSig 'こっちはダミー。
            Put #nFile, , TropoNoise 'こっちはダミー。
            Put #nFile, , TropoAprSig
            Put #nFile, , TropoNoise
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            Put #nFile, , IonoFlag '2018/07/17 H.Nakamura 電離層遅延推定パラメータの追加。
            Put #nFile, , IonoAprSig '2018/07/17 H.Nakamura 電離層遅延推定パラメータの追加。
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
            Call PutString(nFile, NavigationPath)

            Put #nFile, , UBound(m_sOrders)
            Dim i As Long
            For i = 0 To UBound(m_sOrders)
                Call PutString(nFile, m_sOrders(i))
                Put #nFile, , m_bRevers(i)
                Put #nFile, , m_bChecks(i)
            Next

        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '読み込み。
        '
        '引き数：
        'nFile ファイル番号。
        'nVersion ファイルバージョン。
        Public Sub Load(ByVal nFile As Integer, ByVal nVersion As Long)

            Get #nFile, , ElevationMask
            Get #nFile, , Interval
            Get #nFile, , Orbit
            Get #nFile, , Frequency
            Get #nFile, , ThresholdLC
            Get #nFile, , SolveMode
            Get #nFile, , TimeType
            Get #nFile, , StrTimeGPS
            Get #nFile, , EndTimeGPS
            Get #nFile, , Troposhere
            Get #nFile, , Temperature
            Get #nFile, , Pressure
            Get #nFile, , Humidity
            If nVersion< 1700 Then
                TropEst = DEFAULT_TROPEST
                TropMap = DEFAULT_TROPMAP
                MinWindowLength = DEFAULT_MINWINDOWLENGTH
                PrioriConstraint = DEFAULT_PRIORICONSTRAINT
                RelativeConstraint = DEFAULT_RELATIVECONSTRAINT
            Else
                Get #nFile, , TropEst
                Get #nFile, , TropMap
                Get #nFile, , MinWindowLength
                Get #nFile, , PrioriConstraint
                Get #nFile, , RelativeConstraint
            End If
            Get #nFile, , MaxResidL1
            Get #nFile, , MaxResidL2
            If nVersion< 1900 Then
                MaxResidL3 = DEFAULT_MAXRESIDL3
                MaxResidL5 = DEFAULT_MAXRESIDL5
            Else
                Get #nFile, , MaxResidL3
                Get #nFile, , MaxResidL5
            End If
            If nVersion< 8600 Then
                MaxResidCode = DEFAULT_MAXRESIDCODE
                MaxResidPhase = DEFAULT_MAXRESIDPHASE
            Else
                Get #nFile, , MaxResidCode
                Get #nFile, , MaxResidPhase
            End If
            Get #nFile, , MinRatio
            If nVersion< 8700 Then
                MinPs = DEFAULT_MINPS
            Else
                Get #nFile, , MinPs
            End If
            '2018/07/17 H.Nakamura 電離層遅延推定パラメータの追加にともない大気遅延も選択できるようにする。'''''''''''''''''''''''''''''
            If nVersion< 9200 Then
                TropoFlag = 0
            Else
                Get #nFile, , TropoFlag
            End If
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            If nVersion< 8600 Then
                TropoAprSig = DEFAULT_TROPOAPRSIG
                TropoNoise = DEFAULT_TROPONOISE
            '2016/02/06 H.Nakamura 大気遅延を推定しない。'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            ElseIf nVersion< 9000 Then
                Get #nFile, , TropoAprSig
                Get #nFile, , TropoNoise
                TropoAprSig = DEFAULT_TROPOAPRSIG
                TropoNoise = DEFAULT_TROPONOISE
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            Else
                Get #nFile, , TropoAprSig 'こっちはダミー。
                Get #nFile, , TropoNoise 'こっちはダミー。
                Get #nFile, , TropoAprSig
                Get #nFile, , TropoNoise
            End If
            '2018/07/17 H.Nakamura 電離層遅延推定パラメータの追加。'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            If nVersion< 9200 Then
                IonoFlag = 0
                IonoAprSig = DEFAULT_IONOAPRSIG
            Else
                Get #nFile, , IonoFlag
                Get #nFile, , IonoAprSig
            End If
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

            If nVersion >= 1200 Then
                Get #nFile, , ExcludeGPS
                If nVersion< 1300 Then ExcludeGPS = ExcludeGPS Xor &HFFFFFFFF
            End If
            If nVersion< 8600 Then
                ExcludeGlonass = 0
                GlonassFlag = -1
                '2017/06/06 NS6000対応。'''''''''''''''''''''''''''''''''''''''''''''''''''''''
                ExcludeQZSS = 0
        '       ExcludeGalileo = 0  '2018/08/21 Hitz H.Nakamura 衛星数が増えたので64ビットに増やす。
                ExcludeGalileo1 = 0
                ExcludeGalileo2 = 0
                ExcludeBeiDou1 = 0
                ExcludeBeiDou2 = 0
                QZSSFlag = -1
                GalileoFlag = -1
                BeiDouFlag = -1
                '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            Else
                Get #nFile, , ExcludeGlonass
                Get #nFile, , GlonassFlag
                '2017/06/06 NS6000対応。'''''''''''''''''''''''''''''''''''''''''''''''''''''''
                If nVersion< 9100 Then
                    ExcludeQZSS = 0
        '           ExcludeGalileo = 0  '2018/08/21 Hitz H.Nakamura 衛星数が増えたので64ビットに増やす。
                    ExcludeGalileo1 = 0
                    ExcludeGalileo2 = 0
                    ExcludeBeiDou1 = 0
                    ExcludeBeiDou2 = 0
                    QZSSFlag = -1
                    GalileoFlag = -1
                    BeiDouFlag = -1
                Else
                    Get #nFile, , ExcludeQZSS
        '           Get #nFile, , ExcludeGalileo    '2018/08/21 Hitz H.Nakamura 衛星数が増えたので64ビットに増やす。
                    If nVersion< 9300 Then
                        Get #nFile, , ExcludeGalileo1
                        ExcludeGalileo2 = 0
                    Else
                        Get #nFile, , ExcludeGalileo1
                        Get #nFile, , ExcludeGalileo2
                    End If
                    Get #nFile, , ExcludeBeiDou1
                    Get #nFile, , ExcludeBeiDou2
                    Get #nFile, , QZSSFlag
                    Get #nFile, , GalileoFlag
                    Get #nFile, , BeiDouFlag
                End If
                '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            End If
            If nVersion< 4700 Then
                NavigationPath = ""
            Else
                NavigationPath = GetString(nFile)
            End If

            Dim nCount As Long
            Get #nFile, , nCount
            ReDim m_sOrders(-1 To nCount)
            ReDim m_bRevers(-1 To nCount)
            ReDim m_bChecks(-1 To nCount)
            Dim i As Long
            For i = 0 To nCount
                m_sOrders(i) = GetString(nFile)
                Get #nFile, , m_bRevers(i)
                Get #nFile, , m_bChecks(i)
            Next
            If nVersion< 1200 Then
                ReDim m_sOrders(-1 To -1)
                ReDim m_bRevers(-1 To -1)
                ReDim m_bChecks(-1 To -1)
            End If


            If nVersion< 1200 Then
                ExcludeGPS = 0
                Dim bGPS As Boolean
                For i = 0 To PPS_MAXPRN_SET - 1
                    Get #nFile, , bGPS
                    If Not bGPS Then ExcludeGPS = ExcludeGPS Or EXCLUDE_SV(i)
                Next
            End If


            If nVersion < 1300 Then
                MaxResidL1 = MaxResidL1 / 1000
                MaxResidL2 = MaxResidL2 / 1000
            End If


        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '読み込み。
        '
        '引き数：
        'nFile ファイル番号。
        'nVersion ファイルバージョン。
        */
        public void Load(BinaryReader nFile, long nVersion)
        {
            return;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '指定されたオブジェクトと比較する。
        '
        '引き数：
        'clsBaseLineAnalysisParam 比較対照オブジェクト。
        '
        '戻り値：
        '一致する場合 True を返す。
        'それ以外の場合 False を返す。
        Public Function Compare(ByVal clsBaseLineAnalysisParam As BaseLineAnalysisParam) As Boolean

            Compare = False

            If ElevationMask<> clsBaseLineAnalysisParam.ElevationMask Then Exit Function
            If Interval<> clsBaseLineAnalysisParam.Interval Then Exit Function
            If Orbit <> clsBaseLineAnalysisParam.Orbit Then Exit Function
            If Frequency <> clsBaseLineAnalysisParam.Frequency Then Exit Function
            If ThresholdLC <> clsBaseLineAnalysisParam.ThresholdLC Then Exit Function
            If SolveMode <> clsBaseLineAnalysisParam.SolveMode Then Exit Function
            If TimeType <> clsBaseLineAnalysisParam.TimeType Then Exit Function
            If DateDiff("s", StrTimeGPS, clsBaseLineAnalysisParam.StrTimeGPS) <> 0 Then Exit Function
            If DateDiff("s", EndTimeGPS, clsBaseLineAnalysisParam.EndTimeGPS) <> 0 Then Exit Function
            If Troposhere<> clsBaseLineAnalysisParam.Troposhere Then Exit Function
            If FLT_EPSILON <= Abs(Temperature - clsBaseLineAnalysisParam.Temperature) Then Exit Function
            If FLT_EPSILON <= Abs(Pressure - clsBaseLineAnalysisParam.Pressure) Then Exit Function
            If FLT_EPSILON <= Abs(Humidity - clsBaseLineAnalysisParam.Humidity) Then Exit Function
            If TropEst<> clsBaseLineAnalysisParam.TropEst Then Exit Function
            If TropMap<> clsBaseLineAnalysisParam.TropMap Then Exit Function
            If FLT_EPSILON <= Abs(MinWindowLength - clsBaseLineAnalysisParam.MinWindowLength) Then Exit Function
            If FLT_EPSILON <= Abs(PrioriConstraint - clsBaseLineAnalysisParam.PrioriConstraint) Then Exit Function
            If FLT_EPSILON <= Abs(RelativeConstraint - clsBaseLineAnalysisParam.RelativeConstraint) Then Exit Function
            If FLT_EPSILON <= Abs(MaxResidL1 - clsBaseLineAnalysisParam.MaxResidL1) Then Exit Function
            If FLT_EPSILON <= Abs(MaxResidL2 - clsBaseLineAnalysisParam.MaxResidL2) Then Exit Function
            If FLT_EPSILON <= Abs(MaxResidL3 - clsBaseLineAnalysisParam.MaxResidL3) Then Exit Function
            If FLT_EPSILON <= Abs(MaxResidL5 - clsBaseLineAnalysisParam.MaxResidL5) Then Exit Function
            If FLT_EPSILON <= Abs(MaxResidCode - clsBaseLineAnalysisParam.MaxResidCode) Then Exit Function
            If FLT_EPSILON <= Abs(MaxResidPhase - clsBaseLineAnalysisParam.MaxResidPhase) Then Exit Function
            If FLT_EPSILON <= Abs(MinRatio - clsBaseLineAnalysisParam.MinRatio) Then Exit Function
                If FLT_EPSILON <= Abs(MinPs - clsBaseLineAnalysisParam.MinPs) Then Exit Function
            If TropoFlag<> clsBaseLineAnalysisParam.TropoFlag Then Exit Function '2018/07/17 H.Nakamura 電離層遅延推定パラメータの追加にともない大気遅延も選択できるようにする。
            If FLT_EPSILON <= Abs(TropoAprSig - clsBaseLineAnalysisParam.TropoAprSig) Then Exit Function
                If FLT_EPSILON <= Abs(TropoNoise - clsBaseLineAnalysisParam.TropoNoise) Then Exit Function
            If IonoFlag<> clsBaseLineAnalysisParam.IonoFlag Then Exit Function '2018/07/17 H.Nakamura 電離層遅延推定パラメータの追加。
            If FLT_EPSILON <= Abs(IonoAprSig - clsBaseLineAnalysisParam.IonoAprSig) Then Exit Function '2018/07/17 H.Nakamura 電離層遅延推定パラメータの追加。
            If ExcludeGPS<> clsBaseLineAnalysisParam.ExcludeGPS Then Exit Function
            If ExcludeGlonass <> clsBaseLineAnalysisParam.ExcludeGlonass Then Exit Function
            If GlonassFlag <> clsBaseLineAnalysisParam.GlonassFlag Then Exit Function
            '2017/06/06 NS6000対応。'''''''''''''''''''''''''''''''''''''''''''''''''''''''
            If ExcludeQZSS <> clsBaseLineAnalysisParam.ExcludeQZSS Then Exit Function
        '   If ExcludeGalileo <> clsBaseLineAnalysisParam.ExcludeGalileo Then Exit Function '2018/08/21 Hitz H.Nakamura 衛星数が増えたので64ビットに増やす。
            If ExcludeGalileo1 <> clsBaseLineAnalysisParam.ExcludeGalileo1 Then Exit Function
            If ExcludeGalileo2 <> clsBaseLineAnalysisParam.ExcludeGalileo2 Then Exit Function
            If ExcludeBeiDou1 <> clsBaseLineAnalysisParam.ExcludeBeiDou1 Then Exit Function
            If ExcludeBeiDou2 <> clsBaseLineAnalysisParam.ExcludeBeiDou2 Then Exit Function
            If QZSSFlag <> clsBaseLineAnalysisParam.QZSSFlag Then Exit Function
            If GalileoFlag <> clsBaseLineAnalysisParam.GalileoFlag Then Exit Function
            If BeiDouFlag <> clsBaseLineAnalysisParam.BeiDouFlag Then Exit Function
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            If NavigationPath <> clsBaseLineAnalysisParam.NavigationPath Then Exit Function


            Dim sOrders() As String
            Dim bRevers() As Boolean
            Dim bChecks() As Boolean
            sOrders = clsBaseLineAnalysisParam.Orders
            bRevers = clsBaseLineAnalysisParam.Revers
            bChecks = clsBaseLineAnalysisParam.Checks
            If UBound(m_sOrders) <> UBound(sOrders) Then Exit Function
            Dim i As Long
            For i = 0 To UBound(m_sOrders)
                If m_sOrders(i) <> sOrders(i) Then Exit Function
                If m_bRevers(i) <> bRevers(i) Then Exit Function
                If m_bChecks(i) <> bChecks(i) Then Exit Function
            Next

            Compare = True


        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '基線解析順序を設定する。
        '
        '引き数：
        'sOrders 基線解析順序。要素は BaseLineVector の Key。参照する配列の要素は(0 To ...)。Ubound 関数を使用する。
        'bChecks チェックフラグ。True=基線解析の対象とする。False=基線解析から除外。参照する配列の要素は(0 To ...)。Ubound 関数を使用する。
        'bRevers 反転フラグ。True=反転。False=反転無し。参照する配列の要素は(0 To ...)。Ubound 関数を使用する。
        Public Sub SetOrders(ByRef sOrders() As String, ByRef bRevers() As Boolean, ByRef bChecks() As Boolean)
            ReDim m_sOrders(-1 To UBound(sOrders))
            ReDim m_bRevers(-1 To UBound(sOrders))
            ReDim m_bChecks(-1 To UBound(sOrders))
            Dim i As Long
            For i = 0 To UBound(m_sOrders)
                m_sOrders(i) = sOrders(i)
                m_bRevers(i) = bRevers(i)
                m_bChecks(i) = bChecks(i)
            Next
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //==========================================================================================
    }
}
