//24/01/04 K.setoguchi@NV---------->>>>>>>>>>
//***************************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using System.Windows.Forms;
using System.IO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using static SurvLine.MdlBaseLineAnalyser;
using static System.Net.WebRequestMethods;
using SurvLine.mdl;

namespace SurvLine
{
    internal class BaseLineAnalysisParam
    {

        MdlUtility mdlUtility = new MdlUtility();

        //24/01/04 K.setoguchi@NV---------->>>>>>>>>>
        //'*******************************************************************************
        //'基線解析パラメータ
        //
        //Option Explicit
        //
        //'プロパティ
        public long ElevationMask;                              // As Long '仰角マスク(度)。
        public long Interval;                                   // As Long '解析間隔(秒)。
        public MdlBaseLineAnalyser.ORBIT_TYPE Orbit;            // As ORBIT_TYPE '軌道暦。
        public MdlBaseLineAnalyser.FREQUENCY_TYPE Frequency;    // As FREQUENCY_TYPE '周波数。
        public long ThresholdLC;                                // As Long 'LCしきい値(ｍ)。
        public MdlBaseLineAnalyser.SOLVEMODE_TYPE SolveMode;     //As SOLVEMODE_TYPE '解析モード。
        public MdlBaseLineAnalyser.TIME_TYPE TimeType;          // As TIME_TYPE '時間種別。
        public DateTime StrTimeGPS;                             // As Date '開始日時(GPS)。
        public DateTime EndTimeGPS;                             // As Date '終了日時(GPS)。
        public MdlBaseLineAnalyser.TROPOSHERE_TYPE Troposhere;  // As TROPOSHERE_TYPE '対流圏モデル。
        public double Temperature;                              // As Double '気温(℃)。
        public double Pressure;                                 // As Double '気圧(hPa)。
        public double Humidity;                                 // As Double '湿度(％)。
        public bool TropEst;                                    // As Boolean '対流圏遅延評価フラグ。
        public MdlBaseLineAnalyser.TROPMAP_TYPE TropMap;        // As TROPMAP_TYPE '対流圏のマッピング。
        public double MinWindowLength;          // As Double '間隔。
        public double PrioriConstraint;         // As Double '初期値。
        public double RelativeConstraint;       // As Double '相対値。
        public double MaxResidL1;               // As Double '最大残差L1。
        public double MaxResidL2;               // As Double '最大残差L2。
        public double MaxResidL3;               // As Double '最大残差L3。
        public double MaxResidL5;               // As Double '最大残差L5。
        public double MaxResidCode;             // As Double '最大残差(Code)。
        public double MaxResidPhase;            // As Double '最大残差(Phase)。
        public double MinRatio;                 // As Double '最小バイアス決定比。
        public double MinPs;                    // As Double '最小 probability of success。
        public int TropoFlag;                   // As Integer '対流圏遅延推定フラグ。0＝使用しない、0≠使用する。'2018/07/17 H.Nakamura 電離層遅延推定パラメータの追加にともない大気遅延も選択できるようにする。
        public double TropoAprSig;              // As Double '対流圏遅延推定に与える初期シグマ。
        public double TropoNoise;               // As Double '対流圏遅延推定に与える毎エポックの分散。
        public int IonoFlag;                    // As Integer '電離層遅延推定フラグ。0＝自動、0≠任意指定。'2018/07/17 H.Nakamura 電離層遅延推定パラメータの追加。
        public double IonoAprSig;               // As Double '電離層遅延推定に与える初期シグマ。'2018/07/17 H.Nakamura 電離層遅延推定パラメータの追加。
        public long ExcludeGPS;                 // As Long '不使用GPS。ビットフラグ。1=使用。0=無使用。ビット0がGPS1番、ビット1がGPS2番。。。。
        public long ExcludeGlonass;             // As Long '不使用GLONASS。ビットフラグ。1=使用。0=無使用。ビット0がR1番、ビット1がR2番。。。。
        public int GlonassFlag;                 // As Integer 'GLONASSフラグ。0＜GLONASS ON、0＝GLONASS OFF、0＞GLONASS ONだけど無効化。
        //'2017/06/13 NS6000対応。'''''''''''''''''''''''''''''''''''''''''''''''''''''''
        public long ExcludeQZSS;                // As Long '不使用QZSS。ビットフラグ。1=使用。0=無使用。ビット0がJ1番、ビット1がJ2番。。。。
        ///  'public ExcludeGalileo As Long '不使用Galileo。ビットフラグ。1=使用。0=無使用。ビット0がE1番、ビット1がE2番。。。。 '2018/08/21 Hitz H.Nakamura 衛星数が増えたので64ビットに増やす
        public long ExcludeGalileo1;            // As Long '不使用Galileo。ビットフラグ。1=使用。0=無使用。ビット0がE1番、ビット1がE2番。。。。
        public long ExcludeGalileo2;            // As Long '不使用Galileo。ビットフラグ。1=使用。0=無使用。ビット0がE33番、ビット1がE34番。。。。
        public long ExcludeBeiDou1;             // As Long '不使用BeiDou。ビットフラグ。1=使用。0=無使用。ビット0がC1番、ビット1がC2番。。。。
        public long ExcludeBeiDou2;             // As Long '不使用BeiDou。ビットフラグ。1=使用。0=無使用。ビット0がC33番、ビット1がC34番。。。。
        public int QZSSFlag;                    // As Integer 'QZSSフラグ。0＜QZSS ON、0＝QZSS OFF、0＞QZSS ONだけど無効化。
        public int GalileoFlag;                 // As Integer 'Galileoフラグ。0＜Galileo ON、0＝Galileo OFF、0＞Galileo ONだけど無効化。
        public int BeiDouFlag;                  // As Integer 'BeiDouフラグ。0＜BeiDou ON、0＝BeiDou OFF、0＞BeiDou ONだけど無効化。
        //'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        public string NavigationPath;           // As String '精密暦ファイルのパス。

        //'インプリメンテーション
        private List<string> m_sOrders;               // ()  As String '基線解析順序。BaseLineVector の Key。配列の要素は(-1 To ...)、要素 -1 は未使用。
        private List<bool> m_bRevers;                 // () As Boolean '反転フラグ。True=反転。False=反転無し。配列の要素は(-1 To ...)、要素 -1 は未使用。
        private List<bool> m_bChecks;                 // () As Boolean 'チェックフラグ。True=基線解析の対象とする。False=基線解析から除外。配列の要素は(-1 To ...)、要素 -1 は未使用。
                                                      //***************************************************************************

        public BaseLineAnalysisParam()
        {
            Class_Initialize();     //InitializeComponent();

        }

        //<<<<<<<<<-----------24/01/04 K.setoguchi@NV


        //24/01/04 K.setoguchi@NV---------->>>>>>>>>>
        //***************************************************************************
        /// <summary>
        //'イベント
        //
        //'初期化。
        /// </summary>
        private void Class_Initialize()
        {
            ElevationMask = 15;
            Interval = -1;
            Orbit = ORBIT_TYPE.ORBIT_BROADCAST; // ORBIT_BROADCAST:
            Frequency = FREQUENCY_TYPE.FREQUENCY_INI;
            ThresholdLC = 10000;
            SolveMode = DEFAULT_SOLVEMODE;
            TimeType = TIME_TYPE.TIME_PROJECT;
            StrTimeGPS = DateTime.Parse(MdlNSDefine.MIN_TIME);  //MIN_TIME;
            EndTimeGPS = DateTime.Parse(MdlNSDefine.MIN_TIME); ///MIN_TIME;
            Troposhere = (TROPOSHERE_TYPE)MdlBaseLineAnalyser.DEFAULT_TROPOSHERE;
            Temperature = DEFAULT_TEMPERATURE;
            Pressure = DEFAULT_PRESSURE;
            Humidity = DEFAULT_HUMIDITY;
            TropEst = DEFAULT_TROPEST;
            TropMap = DEFAULT_TROPMAP;
            MinWindowLength = DEFAULT_MINWINDOWLENGTH;
            PrioriConstraint = DEFAULT_PRIORICONSTRAINT;
            RelativeConstraint = DEFAULT_RELATIVECONSTRAINT;
            MaxResidL1 = DEFAULT_MAXRESIDL1;
            MaxResidL2 = DEFAULT_MAXRESIDL2;
            MaxResidL3 = DEFAULT_MAXRESIDL3;
            MaxResidL5 = DEFAULT_MAXRESIDL5;
            MaxResidCode = DEFAULT_MAXRESIDCODE;
            MaxResidPhase = DEFAULT_MAXRESIDPHASE;
            MinRatio = DEFAULT_MINRATIO;
            MinPs = DEFAULT_MINPS;
            TropoFlag = 0;                      // '2018/07/17 H.Nakamura 電離層遅延推定パラメータの追加にともない大気遅延も選択できるようにする。
            TropoAprSig = DEFAULT_TROPOAPRSIG;
            TropoNoise = DEFAULT_TROPONOISE;
            IonoFlag = 0;                       //'2018/07/17 H.Nakamura 電離層遅延推定パラメータの追加。
            IonoAprSig = DEFAULT_IONOAPRSIG;    // '2018/07/17 H.Nakamura 電離層遅延推定パラメータの追加。
            ExcludeGPS = 0;
            ExcludeGlonass = 0;
            GlonassFlag = -1;
            //'2017/06/06 NS6000対応。'''''''''''''''''''''''''''''''''''''''''''''''''''''''
            ExcludeQZSS = 0;
            //'   ExcludeGalileo = 0  '2018/08/21 Hitz H.Nakamura 衛星数が増えたので64ビットに増やす。
            ExcludeGalileo1 = 0;
            ExcludeGalileo2 = 0;
            ExcludeBeiDou1 = 0;
            ExcludeBeiDou2 = 0;
            QZSSFlag = -1;
            GalileoFlag = -1;
            BeiDouFlag = -1;
            //'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            NavigationPath = "";


            //ReDim m_sOrders(-1 To -1)
            List<string> m_sOrders = new List<string>();
            //ReDim m_bRevers(-1 To -1)
            List<bool> m_bRevers = new List<bool>();
            //ReDim m_bChecks(-1 To -1)
            List<bool> m_bChecks = new List<bool>();

        }
        //------------------------------------------------------------
        //'イベント
        //
        //'初期化。
        //Private Sub Class_Initialize()
        //
        //    On Error GoTo ErrorHandler
        //
        //
        //    ElevationMask = 15
        //    Interval = -1
        //    Orbit = ORBIT_BROADCAST
        //    Frequency = -1
        //    ThresholdLC = 10000
        //    SolveMode = DEFAULT_SOLVEMODE
        //    TimeType = TIME_PROJECT
        //    StrTimeGPS = MIN_TIME
        //    EndTimeGPS = MIN_TIME
        //    Troposhere = DEFAULT_TROPOSHERE
        //    Temperature = DEFAULT_TEMPERATURE
        //    Pressure = DEFAULT_PRESSURE
        //    Humidity = DEFAULT_HUMIDITY
        //    TropEst = DEFAULT_TROPEST
        //    TropMap = DEFAULT_TROPMAP
        //    MinWindowLength = DEFAULT_MINWINDOWLENGTH
        //    PrioriConstraint = DEFAULT_PRIORICONSTRAINT
        //    RelativeConstraint = DEFAULT_RELATIVECONSTRAINT
        //    MaxResidL1 = DEFAULT_MAXRESIDL1
        //    MaxResidL2 = DEFAULT_MAXRESIDL2
        //    MaxResidL3 = DEFAULT_MAXRESIDL3
        //    MaxResidL5 = DEFAULT_MAXRESIDL5
        //    MaxResidCode = DEFAULT_MAXRESIDCODE
        //    MaxResidPhase = DEFAULT_MAXRESIDPHASE
        //    MinRatio = DEFAULT_MINRATIO
        //    MinPs = DEFAULT_MINPS
        //    TropoFlag = 0 '2018/07/17 H.Nakamura 電離層遅延推定パラメータの追加にともない大気遅延も選択できるようにする。
        //    TropoAprSig = DEFAULT_TROPOAPRSIG
        //    TropoNoise = DEFAULT_TROPONOISE
        //    IonoFlag = 0 '2018/07/17 H.Nakamura 電離層遅延推定パラメータの追加。
        //    IonoAprSig = DEFAULT_IONOAPRSIG '2018/07/17 H.Nakamura 電離層遅延推定パラメータの追加。
        //    ExcludeGPS = 0
        //    ExcludeGlonass = 0
        //    GlonassFlag = -1
        //    '2017/06/06 NS6000対応。'''''''''''''''''''''''''''''''''''''''''''''''''''''''
        //    ExcludeQZSS = 0
        //'   ExcludeGalileo = 0  '2018/08/21 Hitz H.Nakamura 衛星数が増えたので64ビットに増やす。
        //    ExcludeGalileo1 = 0
        //    ExcludeGalileo2 = 0
        //    ExcludeBeiDou1 = 0
        //    ExcludeBeiDou2 = 0
        //    QZSSFlag = -1
        //    GalileoFlag = -1
        //    BeiDouFlag = -1
        //    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        //    NavigationPath = ""
        //
        //
        //    ReDim m_sOrders(-1 To -1)
        //    ReDim m_bRevers(-1 To -1)
        //    ReDim m_bChecks(-1 To -1)
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
        //***************************************************************************
        //<<<<<<<<<-----------24/01/04 K.setoguchi@NV


        //24/01/04 K.setoguchi@NV---------->>>>>>>>>>
        //***************************************************************************
        /// <summary>
        /// メソッド
        ///
        /// 保存。
        ///
        /// 引き数：
        ///     BinaryWriter bw：バイナリファイル
        /// </summary>
        /// <param name="bw"></param>
        public void Save(BinaryWriter bw)
        {
            //---- (int)符号付３２Bit整数 -------
            bw.Write((int)ElevationMask);      //    Put #nFile, , ElevationMask   //初期:15(0F 00 00 00)
            bw.Write((int)Interval);           //    Put #nFile, , Interval        //初期:FF FF FF FF
            bw.Write((int)Orbit);              //    Put #nFile, , Orbit           //初期:00 00 00 00
            bw.Write((int)Frequency);          //    Put #nFile, , Frequency       //初期:FF FF FF FF
            bw.Write((int)ThresholdLC);              //    Put #nFile, , ThresholdLC     //初期:ThresholdLC = 10000(0x2710)
            bw.Write((int)SolveMode);          //    Put #nFile, , SolveMode
            bw.Write((int)TimeType);           //    Put #nFile, , TimeType
            //-------------------------------------            
            //bw.Write(StrTimeGPS.ToString());    //    Put #nFile, , StrTimeGPS
            double dWork = StrTimeGPS.ToBinary();
            bw.Write((double)dWork);
            //-------------------------------------            
            //bw.Write(EndTimeGPS.ToString());    //    Put #nFile, , EndTimeGPS
            dWork = EndTimeGPS.ToBinary();
            bw.Write((double)dWork);
            //-------------------------------------            
            bw.Write((int)Troposhere);         //    Put #nFile, , Troposhere
            bw.Write((int)Temperature);              //    Put #nFile, , Temperature
            bw.Write((int)Pressure);                 //    Put #nFile, , Pressure
            bw.Write((int)Humidity);                 //    Put #nFile, , Humidity
            bw.Write(TropEst);                  //    Put #nFile, , TropEst
            bw.Write((int)TropMap);            //    Put #nFile, , TropMap

            bw.Write((int)MinWindowLength);          //    Put #nFile, , MinWindowLength
            bw.Write((int)PrioriConstraint);         //    Put #nFile, , PrioriConstraint
            bw.Write((int)RelativeConstraint);       //    Put #nFile, , RelativeConstraint
            bw.Write((int)MaxResidL1);               //    Put #nFile, , MaxResidL1
            bw.Write((int)MaxResidL2);               //    Put #nFile, , MaxResidL2
            bw.Write((int)MaxResidL3);               //    Put #nFile, , MaxResidL3
            bw.Write((int)MaxResidL5);               //    Put #nFile, , MaxResidL5
            bw.Write((int)MaxResidCode);             //    Put #nFile, , MaxResidCode
            bw.Write((int)MaxResidPhase);            //    Put #nFile, , MaxResidPhase
            bw.Write((int)MinRatio);                 //    Put #nFile, , MinRatio
            bw.Write((int)MinPs);                    //    Put #nFile, , MinPs
            bw.Write((int)TropoFlag);                //    Put #nFile, , TropoFlag '2018/07/17 H.Nakamura 電離層遅延推定パラメータの追加にともない大気遅延も選択できるようにする。
                                                //    //'TropoFlag/02/06 H.Nakamura 大気遅延を推定しない。'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                                                //    //'Put #nFile, , TropoAprSig
                                                //    //'Put #nFile, , TropoNoise
                                                //    //''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

            bw.Write((int)TropoAprSig);              //    Put #nFile, , TropoAprSig 'こっちはダミー。
            bw.Write((int)TropoNoise);               //    Put #nFile, , TropoNoise 'こっちはダミー。
            bw.Write((int)TropoAprSig);              //    Put #nFile, , TropoAprSig
            bw.Write((int)TropoNoise);               //    Put #nFile, , TropoNoise
            //    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            bw.Write((int)IonoFlag);                 //    Put #nFile, , IonoFlag '2018/07/17 H.Nakamura 電離層遅延推定パラメータの追加。
            bw.Write((int)IonoAprSig);               //    Put #nFile, , IonoAprSig '2018/07/17 H.Nakamura 電離層遅延推定パラメータの追加。
            bw.Write((int)ExcludeGPS);               //    Put #nFile, , ExcludeGPS
            bw.Write((int)ExcludeGlonass);           //    Put #nFile, , ExcludeGlonass
            bw.Write((int)GlonassFlag);              //    Put #nFile, , GlonassFlag
            //    '2017/06/06 NS6000対応。'''''''''''''''''''''''''''''''''''''''''''''''''''''''
            bw.Write((int)ExcludeQZSS);              //    Put #nFile, , ExcludeQZSS
            //'   Put #nFile, , ExcludeGalileo    '2018/08/21 Hitz H.Nakamura 衛星数が増えたので64ビットに増やす。
            bw.Write((int)ExcludeGalileo1);          //    Put #nFile, , ExcludeGalileo1
            bw.Write((int)ExcludeGalileo2);          //    Put #nFile, , ExcludeGalileo2
            bw.Write((int)ExcludeBeiDou1);           //    Put #nFile, , ExcludeBeiDou1
            bw.Write((int)ExcludeBeiDou2);           //    Put #nFile, , ExcludeBeiDou2
            bw.Write((int)QZSSFlag);                 //    Put #nFile, , QZSSFlag
            bw.Write((int)GalileoFlag);              //    Put #nFile, , GalileoFlag
            bw.Write((int)BeiDouFlag);               //    Put #nFile, , BeiDouFlag
            //    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

            //--------------------------------------------------
            //[VB]    Call PutString(nFile, NavigationPath)
            //[VB]
            //[VB]    Put #nFile, , UBound(m_sOrders)
            //[VB]    Dim i As Long
            //[VB]    For i = 0 To UBound(m_sOrders)
            //[VB]        Call PutString(nFile, m_sOrders(i))
            //[VB]        Put #nFile, , m_bRevers(i)
            //[VB]        Put #nFile, , m_bChecks(i)
            //[VB]    Next

#if false   //瀬戸口対応
            mdlUtility.FileWrite_PutString(bw, NavigationPath);

            bw.Write(m_sOrders.Count);

            for (int i = 0; i < m_sOrders.Count; i++)
            {
                mdlUtility.FileWrite_PutString(bw, m_sOrders[i]);
                bw.Write(m_bRevers[i]);
                bw.Write(m_bChecks[i]);
            }

#endif   //瀬戸口対応


        }
        //-----------------------------------------------------------------------------
        //'メソッド
        //
        //'保存。
        //'
        //'引き数：
        //'nFile ファイル番号。
        //Public Sub Save(ByVal nFile As Integer)
        //
        //    Put #nFile, , ElevationMask
        //    Put #nFile, , Interval
        //    Put #nFile, , Orbit
        //    Put #nFile, , Frequency
        //    Put #nFile, , ThresholdLC
        //    Put #nFile, , SolveMode
        //    Put #nFile, , TimeType
        //    Put #nFile, , StrTimeGPS
        //    Put #nFile, , EndTimeGPS
        //    Put #nFile, , Troposhere
        //    Put #nFile, , Temperature
        //    Put #nFile, , Pressure
        //    Put #nFile, , Humidity
        //    Put #nFile, , TropEst
        //    Put #nFile, , TropMap
        //    Put #nFile, , MinWindowLength
        //    Put #nFile, , PrioriConstraint
        //    Put #nFile, , RelativeConstraint
        //    Put #nFile, , MaxResidL1
        //    Put #nFile, , MaxResidL2
        //    Put #nFile, , MaxResidL3
        //    Put #nFile, , MaxResidL5
        //    Put #nFile, , MaxResidCode
        //    Put #nFile, , MaxResidPhase
        //    Put #nFile, , MinRatio
        //    Put #nFile, , MinPs
        //    Put #nFile, , TropoFlag '2018/07/17 H.Nakamura 電離層遅延推定パラメータの追加にともない大気遅延も選択できるようにする。
        //    '2016/02/06 H.Nakamura 大気遅延を推定しない。'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        //    'Put #nFile, , TropoAprSig
        //    'Put #nFile, , TropoNoise
        //    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        //    Put #nFile, , TropoAprSig 'こっちはダミー。
        //    Put #nFile, , TropoNoise 'こっちはダミー。
        //    Put #nFile, , TropoAprSig
        //    Put #nFile, , TropoNoise
        //    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        //    Put #nFile, , IonoFlag '2018/07/17 H.Nakamura 電離層遅延推定パラメータの追加。
        //    Put #nFile, , IonoAprSig '2018/07/17 H.Nakamura 電離層遅延推定パラメータの追加。
        //    Put #nFile, , ExcludeGPS
        //    Put #nFile, , ExcludeGlonass
        //    Put #nFile, , GlonassFlag
        //    '2017/06/06 NS6000対応。'''''''''''''''''''''''''''''''''''''''''''''''''''''''
        //    Put #nFile, , ExcludeQZSS
        //'   Put #nFile, , ExcludeGalileo    '2018/08/21 Hitz H.Nakamura 衛星数が増えたので64ビットに増やす。
        //    Put #nFile, , ExcludeGalileo1
        //    Put #nFile, , ExcludeGalileo2
        //    Put #nFile, , ExcludeBeiDou1
        //    Put #nFile, , ExcludeBeiDou2
        //    Put #nFile, , QZSSFlag
        //    Put #nFile, , GalileoFlag
        //    Put #nFile, , BeiDouFlag
        //    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        //    Call PutString(nFile, NavigationPath)
        //
        //    Put #nFile, , UBound(m_sOrders)
        //    Dim i As Long
        //    For i = 0 To UBound(m_sOrders)
        //        Call PutString(nFile, m_sOrders(i))
        //        Put #nFile, , m_bRevers(i)
        //        Put #nFile, , m_bChecks(i)
        //    Next
        //
        //End Sub
        //***************************************************************************
        //<<<<<<<<<-----------24/01/04 K.setoguchi@NV


        //24/01/04 K.setoguchi@NV---------->>>>>>>>>>
        //***************************************************************************
#if false

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

#endif //#if false

        //***************************************************************************
        //<<<<<<<<<-----------24/01/04 K.setoguchi@NV



    }
}
