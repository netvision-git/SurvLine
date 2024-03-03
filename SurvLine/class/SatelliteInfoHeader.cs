using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.ConstrainedExecution;
using System.Runtime.Remoting;
using System.Security.AccessControl;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace SurvLine
{
    internal class SatelliteInfoHeader
    {


        //'*******************************************************************************
        //'衛星情報ヘッダー部
        //
        //  Option Explicit



        //==========================================================================================
        /*[VB]
            'プロパティ
            Public Version As String 'ファイルバージョン。
            Public FileName As String 'ファイル名。
            Public MarkerNumber As String '観測点番号。
            Public MarkerName As String '観測点名称。
            Public SessionID As String 'セッション番号。
            Public TimeOfFirstObs As Date '観測開始日時。
            Public WeekOfFirstObs As Long '観測開始週。
            Public TowOfFirstObs As Double '
            Public TimeOfLastObs As Date '観測終了日時。
            Public WeekOfLastObs As Long '観測終了週。
            Public TowOfLastObs As Double '
            Public TimeOfObs As Long '観測時間(秒)。
            Public EpochOfObs As Long '観測エポック数。
            Public Interval As Double '観測間隔(秒)。
            Public ElevationMask As Double '仰角マスク(度)。
            Public LeapSeconds As Long '
            Public RecType As String '受信機名称。
            Public RecNumber As String '受信機シリアル番号。
            Public AntType As String 'アンテナ種別。
            Public AntNumber As String 'アンテナシリアル番号。
            Public AntMeasurement As String 'アンテナ測位方法。
            Public AntHeight As Double 'アンテナ高(ｍ)。
            Public AntDeltaH As Double 'アンテナオフセット(ｍ)。
            Public AntDeltaE As Double 'アンテナオフセット(ｍ)。
            Public AntDeltaN As Double 'アンテナオフセット(ｍ)。
            Public ApproxPosX As Double '観測座標X。
            Public ApproxPosY As Double '観測座標Y。
            Public ApproxPosZ As Double '観測座標Z。
            Public NumberObserv As Long '観測種別数。
            Public As_ As Long 'AS
            Public NumberOfMaxSV As Long '最大衛星数。
            Public NumberOfMinSV As Long '最少衛星数。
            Public NumberOfAllSV As Long '全衛星数。

            'インプリメンテーション
            Private m_sTypeOfObserv() As String '観測種別。配列の要素は(-1 To ...)、要素 -1 は未使用。
            Private m_nSVHealth() As Long '有効衛星。配列のインデックスが衛星番号に対応する。1=有効、-1=無効。配列の要素は(-1 To ...)、要素 -1 は未使用。

            '2017/06/23 NS6000対応。'''''''''''''''''''''''''''''''''''''''''''''''''''''''
            Public NumberObservR As Long '観測種別数(GLONASS)。
            Public NumberObservJ As Long '観測種別数(QZSS)。
            Public NumberObservE As Long '観測種別数(Galileo)。
            Public NumberObservC As Long '観測種別数(BeiDou)。
            Public NumberObservS As Long '観測種別数(SBAS)。
            Public RinexVersion As Long 'RINEXファイルのバージョン。バージョン番号を1000倍した整数。
            Private m_sTypeOfObservR() As String '観測種別(GLOASS)。配列の要素は(-1 To ...)、要素 -1 は未使用。
            Private m_sTypeOfObservJ() As String '観測種別(QZSS)。配列の要素は(-1 To ...)、要素 -1 は未使用。
            Private m_sTypeOfObservE() As String '観測種別(Galileo)。配列の要素は(-1 To ...)、要素 -1 は未使用。
            Private m_sTypeOfObservC() As String '観測種別(BeiDou)。配列の要素は(-1 To ...)、要素 -1 は未使用。
            Private m_sTypeOfObservS() As String '観測種別(SBAS)。配列の要素は(-1 To ...)、要素 -1 は未使用。
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'プロパティ
        public string Version;          // As String 'ファイルバージョン。
        public string FileName;         // As String 'ファイル名。
        public string MarkerNumber;     // As String '観測点番号。
        public string MarkerName;       // As String '観測点名称。
        public string SessionID;        // As String 'セッション番号。
        public DateTime TimeOfFirstObs; // As Date '観測開始日時。
        public long WeekOfFirstObs;     // As Long '観測開始週。
        public double TowOfFirstObs;    // As Double '
        public DateTime TimeOfLastObs;  // As Date '観測終了日時。
        public long WeekOfLastObs;      // As Long '観測終了週。
        public double TowOfLastObs;     // As Double '
        public long TimeOfObs;          // As Long '観測時間(秒)。
        public long EpochOfObs;         // As Long '観測エポック数。
        public double Interval;         // As Double '観測間隔(秒)。
        public double ElevationMask;    // As Double '仰角マスク(度)。
        public long LeapSeconds;        // As Long '
        public string RecType;          // As String '受信機名称。
        public string RecNumber;        // As String '受信機シリアル番号。
        public string AntType;          // As String 'アンテナ種別。
        public string AntNumber;        // As String 'アンテナシリアル番号。
        public string AntMeasurement;   // As String 'アンテナ測位方法。
        public double AntHeight;        // As Double 'アンテナ高(ｍ)。
        public double AntDeltaH;        // As Double 'アンテナオフセット(ｍ)。
        public double AntDeltaE;        // As Double 'アンテナオフセット(ｍ)。
        public double AntDeltaN;        // As Double 'アンテナオフセット(ｍ)。
        public double ApproxPosX;       // As Double '観測座標X。
        public double ApproxPosY;       // As Double '観測座標Y。
        public double ApproxPosZ;       // As Double '観測座標Z。
        public long NumberObserv;       // As Long '観測種別数。
        public long As_;                // As Long 'AS
        public long NumberOfMaxSV;      // As Long '最大衛星数。
        public long NumberOfMinSV;      // As Long '最少衛星数。
        public long NumberOfAllSV;      // As Long '全衛星数。

        //'インプリメンテーション
        //private m_sTypeOfObserv() As String                   '観測種別。配列の要素は(-1 To ...)、要素 -1 は未使用。
        private List<string> m_sTypeOfObserv;   // () As String '観測種別。配列の要素は(-1 To ...)、要素 -1 は未使用。
        //private m_nSVHealth() As Long                      '有効衛星。配列のインデックスが衛星番号に対応する。1=有効、-1=無効。配列の要素は(-1 To ...)、要素 -1 は未使用。
        private List<long> m_nSVHealth;         //() As Long '有効衛星。配列のインデックスが衛星番号に対応する。1=有効、-1=無効。配列の要素は(-1 To ...)、要素 -1 は未使用。

        //'2017/06/23 NS6000対応。'''''''''''''''''''''''''''''''''''''''''''''''''''''''
        public long NumberObservR;          // As Long '観測種別数(GLONASS)。
        public long NumberObservJ;          // As Long '観測種別数(QZSS)。
        public long NumberObservE;          // As Long '観測種別数(Galileo)。
        public long NumberObservC;          // As Long '観測種別数(BeiDou)。
        public long NumberObservS;          // As Long '観測種別数(SBAS)。
        public long RinexVersion;           // As Long 'RINEXファイルのバージョン。バージョン番号を1000倍した整数。

        private List<string> m_sTypeOfObservR;  //() As String  '観測種別(GLOASS)。配列の要素は(-1 To ...)、要素 -1 は未使用。
        private List<string> m_sTypeOfObservJ;  //() As String '観測種別(QZSS)。配列の要素は(-1 To ...)、要素 -1 は未使用。
        private List<string> m_sTypeOfObservE;  //() As String '観測種別(Galileo)。配列の要素は(-1 To ...)、要素 -1 は未使用。
        private List<string> m_sTypeOfObservC;  //() As String '観測種別(BeiDou)。配列の要素は(-1 To ...)、要素 -1 は未使用。
        private List<string> m_sTypeOfObservS;  //() As String '観測種別(SBAS)。配列の要素は(-1 To ...)、要素 -1 は未使用。
                                                //'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''


        //'*******************************************************************************
        //'プロパティ


        //==========================================================================================
        /*[VB]
            '観測種別｡配列の要素は(-1 To ...)、要素 -1 は未使用。
            Property Let TypeOfObserv(ByRef sTypeOfObserv() As String)
            m_sTypeOfObserv = sTypeOfObserv
            End Property
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /// <summary>
        /// '観測種別｡配列の要素は(-1 To ...)、要素 -1 は未使用。
        /// </summary>
        /// <param name="sTypeOfObserv"></param>
        public void TypeOfObserv(ref List<string> sTypeOfObserv)
        {
            m_sTypeOfObserv = sTypeOfObserv;
        }
        //==========================================================================================
        /*[VB]
            '観測種別｡配列の要素は(-1 To ...)、要素 -1 は未使用。
            Property Get TypeOfObserv() As String()
            TypeOfObserv = m_sTypeOfObserv
            End Property
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /// <summary>
        /// '観測種別｡配列の要素は(-1 To ...)、要素 -1 は未使用。
        /// 
        /// </summary>
        /// <returns>
        /// '観測種別｡
        /// </returns>
        public List<string> TypeOfObserv()
        {
            return m_sTypeOfObserv;
        }
        //==========================================================================================
        /*[VB]
            '有効衛星。配列のインデックスが衛星番号に対応する。1=有効、-1=無効。配列の要素は(-1 To ...)、要素 -1 は未使用。
            Property Let SVHealth(ByRef nSVHealth() As Long)
            m_nSVHealth = nSVHealth
            End Property
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /// <summary>
        /// '有効衛星。配列のインデックスが衛星番号に対応する。1=有効、-1=無効。配列の要素は(-1 To ...)、要素 -1 は未使用。
        /// </summary>
        /// <param name="nSVHealth"></param>
        public void SVHealth(ref List<long> nSVHealth)
        {
            m_nSVHealth = nSVHealth;
        }
        //==========================================================================================
        /*[VB]
            '有効衛星。配列のインデックスが衛星番号に対応する。1=有効、-1=無効。配列の要素は(-1 To ...)、要素 -1 は未使用。
            Property Get SVHealth() As Long()
            SVHealth = m_nSVHealth
            End Property
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /// <summary>
        /// '有効衛星。配列のインデックスが衛星番号に対応する。1=有効、-1=無効。配列の要素は(-1 To ...)、要素 -1 は未使用。
        /// 
        /// </summary>
        /// <returns>
        /// '有効衛星。
        /// </returns>
        public List<long> SVHealth()
        {
            return m_nSVHealth;
        }
        //==========================================================================================
        /*[VB]
            '2017/06/23 NS6000対応。'''''''''''''''''''''''''''''''''''''''''''''''''''''''
            '観測種別(GLONASS)｡配列の要素は(-1 To ...)、要素 -1 は未使用。
            Property Let TypeOfObservR(ByRef sTypeOfObservR() As String)
            m_sTypeOfObservR = sTypeOfObservR
            End Property
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'2017/06/23 NS6000対応。'''''''''''''''''''''''''''''''''''''''''''''''''''''''
        /// <summary>
        /// '観測種別(GLONASS)｡配列の要素は(-1 To ...)、要素 -1 は未使用。
        /// 
        /// </summary>
        public void TypeOfObservR(ref List<string> sTypeOfObservR)
        {
            m_sTypeOfObservR = sTypeOfObservR;
        }
        //==========================================================================================
        /*[VB]
            '観測種別(GLONASS)｡配列の要素は(-1 To ...)、要素 -1 は未使用。
            Property Get TypeOfObservR() As String()
            TypeOfObservR = m_sTypeOfObservR
            End Property
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /// <summary>
        /// '観測種別(GLONASS)｡配列の要素は(-1 To ...)、要素 -1 は未使用。
        /// 
        /// </summary>
        /// <returns>
        /// '観測種別(GLONASS)｡
        /// </returns>
        public List<string> TypeOfObservR()
        {
            return m_sTypeOfObservR;
        }
        //==========================================================================================
        /*[VB]
            '観測種別(QZSS)｡配列の要素は(-1 To ...)、要素 -1 は未使用。
            Property Let TypeOfObservJ(ByRef sTypeOfObservJ() As String)
            m_sTypeOfObservJ = sTypeOfObservJ
            End Property
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /// <summary>
        /// '観測種別(QZSS)｡配列の要素は(-1 To ...)、要素 -1 は未使用。
        /// </summary>
        /// <param name="sTypeOfObservJ"></param>
        /// <param name=""></param>
        public void TypeOfObservJ(ref List<string> sTypeOfObservJ){
            m_sTypeOfObservJ = sTypeOfObservJ;
        }
        //==========================================================================================
        /*[VB]
            '観測種別(QZSS)｡配列の要素は(-1 To ...)、要素 -1 は未使用。
            Property Get TypeOfObservJ() As String()
            TypeOfObservJ = m_sTypeOfObservJ
            End Property
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /// <summary>
        /// '観測種別(QZSS)｡配列の要素は(-1 To ...)、要素 -1 は未使用。
        /// 
        /// </summary>
        /// <returns>
        /// '観測種別(QZSS)｡
        /// </returns>
        public List<string> TypeOfObservJ()
        {
            return m_sTypeOfObservJ;
        }
        //==========================================================================================
        /*[VB]
            '観測種別(Galileo)｡配列の要素は(-1 To ...)、要素 -1 は未使用。
            Property Let TypeOfObservE(ByRef sTypeOfObservE() As String)
            m_sTypeOfObservE = sTypeOfObservE
            End Property
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /// <summary>
        /// '観測種別(Galileo)｡配列の要素は(-1 To ...)、要素 -1 は未使用。
        /// 
        /// </summary>
        /// <param name="sTypeOfObservE"></param>
        /// <param name=""></param>
        public void TypeOfObservE(ref List<string> sTypeOfObservE){
            m_sTypeOfObservE = sTypeOfObservE;
        }
        //==========================================================================================
        /*[VB]
            '観測種別(Galileo)｡配列の要素は(-1 To ...)、要素 -1 は未使用。
            Property Get TypeOfObservE() As String()
            TypeOfObservE = m_sTypeOfObservE
            End Property
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /// <summary>
        /// '観測種別(Galileo)｡配列の要素は(-1 To ...)、要素 -1 は未使用。
        /// 
        /// </summary>
        /// <returns>
        /// '観測種別(Galileo)｡
        /// </returns>
        public List<string> TypeOfObservE()
        {
            return m_sTypeOfObservE;
        }
        //==========================================================================================
        /*[VB]
            '観測種別(BeiDou)｡配列の要素は(-1 To ...)、要素 -1 は未使用。
            Property Let TypeOfObservC(ByRef sTypeOfObservC() As String)
            m_sTypeOfObservC = sTypeOfObservC
            End Property
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /// <summary>
        /// '観測種別(BeiDou)｡配列の要素は(-1 To ...)、要素 -1 は未使用。
        /// </summary>
        /// <param name="sTypeOfObservC"></param>
        public void TypeOfObservC(ref List<string> sTypeOfObservC)
        {
            m_sTypeOfObservC = sTypeOfObservC;
        }
        //==========================================================================================
        /*[VB]
            '観測種別(BeiDou)｡配列の要素は(-1 To ...)、要素 -1 は未使用。
            Property Get TypeOfObservC() As String()
            TypeOfObservC = m_sTypeOfObservC
            End Property
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /// <summary>
        /// '観測種別(BeiDou)｡配列の要素は(-1 To ...)、要素 -1 は未使用。
        /// 
        /// </summary>
        /// '観測種別(BeiDou)｡
        /// <returns></returns>
        public List<string> TypeOfObservC()
        {
            return m_sTypeOfObservC;
        }
        //==========================================================================================
        /*[VB]
            '観測種別(SBAS)｡配列の要素は(-1 To ...)、要素 -1 は未使用。
            Property Let TypeOfObservS(ByRef sTypeOfObservS() As String)
            m_sTypeOfObservS = sTypeOfObservS
            End Property
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /// <summary>
        /// '観測種別(SBAS)｡配列の要素は(-1 To ...)、要素 -1 は未使用。
        /// 
        /// </summary>
        /// <param name="sTypeOfObservS"></param>
        public void TypeOfObservS(ref List<string> sTypeOfObservS)
        {
            m_sTypeOfObservS = sTypeOfObservS;
        }
        //==========================================================================================
        /*[VB]
            '観測種別(SBAS)｡配列の要素は(-1 To ...)、要素 -1 は未使用。
            Property Get TypeOfObservS() As String()
            TypeOfObservS = m_sTypeOfObservS
            End Property
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /// <summary>
        /// '観測種別(SBAS)｡配列の要素は(-1 To ...)、要素 -1 は未使用。
        /// 
        /// </summary>
        /// <returns>
        /// '観測種別(SBAS)｡
        /// </returns>
        public List<string> TypeOfObservS()
        {
            return m_sTypeOfObservS;
        }



        //'*******************************************************************************
        //'イベント


        //==========================================================================================
        /*[VB]
            '初期化。
            Private Sub Class_Initialize()

                On Error GoTo ErrorHandler


                Call Clear


                Exit Sub


            ErrorHandler:
                Call mdlMain.ErrorExit


            End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        private void Class_Initialize()
        {
            try
            {
                //On Error GoTo ErrorHandler

                Clear();

                return;
            }
            catch (Exception ex)
            {
                ///ErrorHandler:
                //    Call mdlMain.ErrorExit
            }
        }


        //'*******************************************************************************
        //'メソッド


        //==========================================================================================
        /*[VB]
            'クリア。
            Public Sub Clear()
                Version = "1.0"
                FileName = ""
                MarkerNumber = ""
                MarkerName = ""
                SessionID = ""
                TimeOfFirstObs = 0
                WeekOfFirstObs = 0
                TowOfFirstObs = 0
                TimeOfLastObs = 0
                WeekOfLastObs = 0
                TowOfLastObs = 0
                TimeOfObs = 0
                EpochOfObs = 0
                Interval = 0
                ElevationMask = 0
                LeapSeconds = 0
                RecType = ""
                RecNumber = ""
                AntType = ""
                AntNumber = ""
                AntMeasurement = ""
                AntHeight = 0
                AntDeltaH = 0
                AntDeltaE = 0
                AntDeltaN = 0
                ApproxPosX = 1
                ApproxPosY = 1
                ApproxPosZ = 1
                NumberObserv = 0
                As_ = 0
                NumberOfMaxSV = 0
                NumberOfMinSV = 0
                NumberOfAllSV = 0
                ReDim m_sTypeOfObserv(-1 To -1)
                ReDim m_nSVHealth(-1 To -1)
                '2017/06/23 NS6000対応。'''''''''''''''''''''''''''''''''''''''''''''''''''''''
                NumberObservR = 0
                NumberObservJ = 0
                NumberObservE = 0
                NumberObservC = 0
                NumberObservS = 0
                RinexVersion = 2110
                ReDim m_sTypeOfObservR(-1 To -1)
                ReDim m_sTypeOfObservJ(-1 To -1)
                ReDim m_sTypeOfObservE(-1 To -1)
                ReDim m_sTypeOfObservC(-1 To -1)
                ReDim m_sTypeOfObservS(-1 To -1)
                '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        public void Clear()
        {
            Version = "1.0";
            FileName = "";
            MarkerNumber = "";
            MarkerName = "";
            SessionID = "";
            TimeOfFirstObs = DateTime.MinValue;
            WeekOfFirstObs = 0;
            TowOfFirstObs = 0;
            TimeOfLastObs = DateTime.MinValue;
            WeekOfLastObs = 0;
            TowOfLastObs = 0;
            TimeOfObs = 0;
            EpochOfObs = 0;
            Interval = 0;
            ElevationMask = 0;
            LeapSeconds = 0;
            RecType = "";
            RecNumber = "";
            AntType = "";
            AntNumber = "";
            AntMeasurement = "";
            AntHeight = 0;
            AntDeltaH = 0;
            AntDeltaE = 0;
            AntDeltaN = 0;
            ApproxPosX = 1;
            ApproxPosY = 1;
            ApproxPosZ = 1;
            NumberObserv = 0;
            As_ = 0;
            NumberOfMaxSV = 0;
            NumberOfMinSV = 0;
            NumberOfAllSV = 0;

            //ReDim m_sTypeOfObserv(-1 To - 1)
            m_sTypeOfObserv.Clear();

            //ReDim m_nSVHealth(-1 To - 1)
            m_nSVHealth.Clear();



            //'2017/06/23 NS6000対応。'''''''''''''''''''''''''''''''''''''''''''''''''''''''
            NumberObservR = 0;
            NumberObservJ = 0;
            NumberObservE = 0;
            NumberObservC = 0;
            NumberObservS = 0;
            RinexVersion = 2110;
            //ReDim m_sTypeOfObservR(-1 To - 1)
            m_sTypeOfObservR.Clear();
            //ReDim m_sTypeOfObservJ(-1 To - 1)
            m_sTypeOfObservJ.Clear();
            //ReDim m_sTypeOfObservE(-1 To - 1)
            m_sTypeOfObservE.Clear();
            //ReDim m_sTypeOfObservC(-1 To - 1)
            m_sTypeOfObservC.Clear();
            //ReDim m_sTypeOfObservS(-1 To - 1)
            m_sTypeOfObservS.Clear();
            //'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

        }



        public SatelliteInfoHeader()
        {
            Class_Initialize();
        }

    }
}
