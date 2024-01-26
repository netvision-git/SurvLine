using SurvLine.mdl;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;
using static SurvLine.mdl.MdlNSDefine;
using static SurvLine.mdl.MdlNSSDefine;
using static System.Collections.Specialized.BitVector32;
using static System.Net.WebRequestMethods;

namespace SurvLine
{
    public class BaseLineVector
    {

        MdlUtility Utility = new MdlUtility();
        Document document = new Document();
        MdlNSSDefine mdlNSSDefine = new MdlNSSDefine();

        //***************************************************************************
        //***************************************************************************
        //'反転フラグ。True=反転。False=反転無し。
        public void Revers(bool bRevers)
        {
        }
        //--------------------------------------------------------------
        //[VB]  '反転フラグ。True=反転。False=反転無し。
        //[VB]  Property Let Revers(ByVal bRevers As Boolean)
        //[VB]      If bRevers Then
        //[VB]          Set m_clsAnalysisStrPoint = m_clsEndPoint
        //[VB]          Set m_clsAnalysisEndPoint = m_clsStrPoint
        //[VB]      Else
        //[VB]          Set m_clsAnalysisStrPoint = m_clsStrPoint
        //[VB]          Set m_clsAnalysisEndPoint = m_clsEndPoint
        //[VB]      End If
        //[VB]  End Property
        //***************************************************************************
        //***************************************************************************


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
        public void Load(BinaryReader br, long nVersion, ref GENBA_STRUCT_S Genba_S)
        {

            byte[] buf = new byte[8]; int ret;

            //--------------------------------------
            //{VB]Session = GetString(nFile)
            Genba_S.Session = Utility.FileRead_GetString(br);

            //--------------------------------------
            //{VB]Get #nFile, , StrTimeGPS
            // 4byte読み取り
            ret = br.Read(buf, 0, 8);
            Genba_S.StrTimeGPS = DateTime.FromOADate(BitConverter.ToDouble(buf, 0));
            //--------------------------------------
            //{VB]Get #nFile, , EndTimeGPS
            ret = br.Read(buf, 0, 8);       // 4byte読み取り
            Genba_S.EndTimeGPS = DateTime.FromOADate(BitConverter.ToDouble(buf, 0));
            //--------------------------------------
            //{VB]Get #nFile, , Exclusion
            Genba_S.Exclusion = document.GetFileBool(br);
            //--------------------------------------
            //{VB]Get #nFile, , Analysis
            Genba_S.Analysis = br.ReadInt32();
            //--------------------------------------
            //{VB]Get #nFile, , Orbit
            Genba_S.Orbit = br.ReadInt32();
            //--------------------------------------
            //{VB]Get #nFile, , Frequency
            Genba_S.Frequency = br.ReadInt32();
            //--------------------------------------
            //{VB]Get #nFile, , SolveMode
            Genba_S.SolveMode = br.ReadInt32();
            //--------------------------------------
            //{VB]Get #nFile, , AnalysisStrTimeGPS
            ret = br.Read(buf, 0, 8);       // 4byte読み取り
            Genba_S.AnalysisStrTimeGPS = DateTime.FromOADate(BitConverter.ToDouble(buf, 0));
            //--------------------------------------
            //{VB]Get #nFile, , AnalysisEndTimeGPS
            ret = br.Read(buf, 0, 8);       // 4byte読み取り
            Genba_S.AnalysisEndTimeGPS = DateTime.FromOADate(BitConverter.ToDouble(buf, 0));

            //------------------------------
            //[VB]Get #nFile, , ElevationMask   public double ElevationMask; 
            Genba_S.ElevationMask = br.ReadDouble();                            //'仰角マスク(度)。

            //---------------------------------------------------------------------------
            //[VB]Get #nFile, , Interval        public double Interval; 
            Genba_S.Interval = br.ReadDouble();                               //'解析間隔(秒)。
            //---------------------------------------------------------------------------
            //[VB]Get #nFile, , Temperature     public double Temperature;
            Genba_S.Temperature = br.ReadDouble();                              //'気温(℃)。
            //---------------------------------------------------------------------------
            //[VB]Get #nFile, , Pressure        public double Pressure;
            Genba_S.Pressure = br.ReadDouble();                                 //'気圧(hPa)。
            //---------------------------------------------------------------------------
            //[VB]Get #nFile, , Humidity        public double Humidity;
            Genba_S.Humidity = br.ReadDouble();                                 //'湿度(％)。
            //---------------------------------------------------------------------------
            //[VB]Get #nFile, , Troposhere      public int Troposhere;           //'対流圏モデル。
            Genba_S.Troposhere = br.ReadInt32();                               //'対流圏モデル。
            //---------------------------------------------------------------------------
            //[VB]If nVersion< 3100 Then
            //[VB]AnalysisFixed = False
            //[VB]Else
            //[VB]Get #nFile, , AnalysisFixed
            //[VB]End If
            //public bool AnalysisFixed;          //'解析始点固定点フラグ。True=解析始点が固定点。False=解析始点は固定点で無い。
            Genba_S.AnalysisFixed = nVersion < 3100 ? false : document.GetFileBool(br);
            //---------------------------------------------------------------------------
            //[VB]    If nVersion< 1700 Then
            //[VB]        AmbPercentage = 0
            //[VB]    Else
            //[VB]        Get #nFile, , AmbPercentage
            //[VB]    End If
            //public long AmbPercentage;            //'FIX率(％)。
            Genba_S.AmbPercentage = nVersion < 1700 ? 0 : br.ReadInt32();   //'FIX率(％)。
            //---------------------------------------------------------------------------
            //[VB] Get #nFile, , Bias       public double Bias;
            Genba_S.Bias = br.ReadDouble();                                 //'バイアス決定比(ｍ)。
            //---------------------------------------------------------------------------
            //[VB]Get #nFile, , EpochUsed       public long EpochUsed;
            Genba_S.EpochUsed = br.ReadInt32();                             //'使用エポック数。
            //---------------------------------------------------------------------------
            //[VB]Get #nFile, , EpochRejected   public long EpochRejected;
            Genba_S.EpochRejected = br.ReadInt32();                         //'棄却エポック数。
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
                Genba_S.nObsUsed = br.ReadInt32();
            }
            //---------------------------------------------------------------------------
            //[VB]Get #nFile, , RMS     public double RMS;
            Genba_S.RMS = br.ReadDouble();                                  //'RMS値(ｍ)。
            //---------------------------------------------------------------------------
            //[VB]Get #nFile, , RDOP    public double RDOP;        
            Genba_S.RDOP = br.ReadDouble();                                 //'RDOP値。
            //---------------------------------------------------------------------------
            //[VB]Get #nFile, , IsDispersion    public bool IsDispersion;
            Genba_S.IsDispersion = document.GetFileBool(br);                //'分散・共分散の有効/無効。True=有効。False=無効。
            //---------------------------------------------------------------------------
            //[VB]Get #nFile, , RcvNumbersGps   public long RcvNumbersGps;
            Genba_S.RcvNumbersGps = br.ReadInt32();                         //'受信GPS衛星番号。ビットフラグ。1=受信。0=非受信。ビット0がGPS1番、ビット1がGPS2番。。。。

            //************************************************************************
            if (nVersion < 8600)
            {
                Genba_S.RcvNumbersGlonass = 0;
                //'2017/06/06 NS6000対応。'''''''''''''''''''''''''''''''''''''''''''''''''''''''
                Genba_S.RcvNumbersQZSS = 0;
                //'       RcvNumbersGalileo = 0   '2018 / 08 / 21 Hitz H.Nakamura 衛星数が増えたので64ビットに増やす。
                Genba_S.RcvNumbersGalileo1 = 0;
                Genba_S.RcvNumbersGalileo2 = 0;
                Genba_S.RcvNumbersBeiDou1 = 0;
                Genba_S.RcvNumbersBeiDou2 = 0;
                //'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            }
            else
            {
                //Get #nFile, , RcvNumbersGlonass
                Genba_S.RcvNumbersGlonass = br.ReadInt32();
                if (nVersion < 9100)
                {
                    Genba_S.RcvNumbersQZSS = 0;
                    //'           RcvNumbersGalileo = 0   '2018 / 08 / 21 Hitz H.Nakamura 衛星数が増えたので64ビットに増やす。
                    Genba_S.RcvNumbersGalileo1 = 0;
                    Genba_S.RcvNumbersGalileo2 = 0;
                    Genba_S.RcvNumbersBeiDou1 = 0;
                    Genba_S.RcvNumbersBeiDou2 = 0;
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
                    //[VB]Get #nFile, , RcvNumbersGalileo1
                    Genba_S.RcvNumbersGalileo1 = br.ReadInt32();
                    //[VB]Get #nFile, , RcvNumbersBeiDou1
                    Genba_S.RcvNumbersBeiDou1 = br.ReadInt32();
                    //[VB]Get #nFile, , RcvNumbersBeiDou2
                    Genba_S.RcvNumbersBeiDou2 = br.ReadInt32();
                    Genba_S.RcvNumbersGalileo2 = 0;
                } else
                {

                    //[VB]Get #nFile, , RcvNumbersQZSS
                    Genba_S.RcvNumbersQZSS = br.ReadInt32();
                    //[VB]Get #nFile, , RcvNumbersGalileo1
                    Genba_S.RcvNumbersGalileo1 = br.ReadInt32();
                    //[VB]Get #nFile, , RcvNumbersGalileo2
                    Genba_S.RcvNumbersGalileo2 = br.ReadInt32();
                    //[VB]Get #nFile, , RcvNumbersBeiDou1
                    Genba_S.RcvNumbersBeiDou1 = br.ReadInt32();
                    //[VB]Get #nFile, , RcvNumbersBeiDou2
                    Genba_S.RcvNumbersBeiDou2 = br.ReadInt32();

                }
            }//if (nVersion < 8600)

            //************************************************************************
            if (nVersion < 1200) {
                Genba_S.MaxResidL1_Legacy = 0;
                Genba_S.MaxResidL2_Legacy = 0;
                Genba_S.MinRatio = 0;
                Genba_S.MinPs = 0;
                Genba_S.ExcludeGPS = 0;
                Genba_S.ExcludeGlonass = 0;
                Genba_S.GlonassFlag = false;
                //'2017/06/06 NS6000対応。'''''''''''''''''''''''''''''''''''''''''''''''''''''''
                //ExcludeQZSS = 0
                //'       ExcludeGalileo = 0  '2018 / 08 / 21 Hitz H.Nakamura 衛星数が増えたので64ビットに増やす。
                Genba_S.ExcludeGalileo1 = 0;
                Genba_S.ExcludeGalileo2 = 0;
                Genba_S.ExcludeBeiDou1 = 0;
                Genba_S.ExcludeBeiDou2 = 0;
                Genba_S.QZSSFlag = false;
                Genba_S.GalileoFlag = false;
                Genba_S.BeiDouFlag = false;
                //'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            }
            else
            {
                //Get #nFile, , MaxResidL1_Legacy
                Genba_S.MaxResidL1_Legacy = br.ReadDouble();
                //Get #nFile, , MaxResidL2_Legacy
                Genba_S.MaxResidL2_Legacy = br.ReadDouble();
                //Get #nFile, , MinRatio
                Genba_S.MinRatio = br.ReadDouble();

                if (nVersion < 8700) {
                    Genba_S.MinPs = 0;
                }
                else {
                    //Get #nFile, , MinPs
                    Genba_S.MinPs = br.ReadDouble();
                }
                //Get #nFile, , ExcludeGPS
                Genba_S.ExcludeGPS = br.ReadInt32();
                if (nVersion < 1300)
                {
                    Genba_S.MaxResidL1_Legacy = Genba_S.MaxResidL1_Legacy / 1000;
                    Genba_S.MaxResidL2_Legacy = Genba_S.MaxResidL2_Legacy / 1000;
                    Genba_S.ExcludeGPS = (long)Genba_S.ExcludeGPS ^ (long)0xFFFFFFFF;
                }//End If
                if (nVersion < 8600)
                {
                    Genba_S.ExcludeGlonass = 0;
                    Genba_S.GlonassFlag = false;
                    //'2017/06/06 NS6000対応。'''''''''''''''''''''''''''''''''''''''''''''''''''''''
                    Genba_S.ExcludeQZSS = 0;
                    //'           ExcludeGalileo = 0  '2018 / 08 / 21 Hitz H.Nakamura 衛星数が増えたので64ビットに増やす。
                    Genba_S.ExcludeGalileo1 = 0;
                    Genba_S.ExcludeGalileo2 = 0;
                    Genba_S.ExcludeBeiDou1 = 0;
                    Genba_S.ExcludeBeiDou2 = 0;
                    Genba_S.QZSSFlag = false;
                    Genba_S.GalileoFlag = false;
                    Genba_S.BeiDouFlag = false;
                    //'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                }
                else
                {
                    //Get #nFile, , ExcludeGlonass
                    Genba_S.ExcludeGlonass = br.ReadInt32();
                    //Get #nFile, , GlonassFlag
                    Genba_S.GlonassFlag = document.GetFileBool(br);
                    //'2017/06/06 NS6000対応。'''''''''''''''''''''''''''''''''''''''''''''''''''''''
                    if (nVersion < 9100)
                    {
                        Genba_S.ExcludeQZSS = 0;
                        //'             ExcludeGalileo = 0  '2018 / 08 / 21 Hitz H.Nakamura 衛星数が増えたので64ビットに増やす。
                        Genba_S.ExcludeGalileo1 = 0;
                        Genba_S.ExcludeGalileo2 = 0;
                        Genba_S.ExcludeBeiDou1 = 0;
                        Genba_S.ExcludeBeiDou2 = 0;
                        Genba_S.QZSSFlag = false;
                        Genba_S.GalileoFlag = false;
                        Genba_S.BeiDouFlag = false;
                    }
                    else
                    {
                        //Get #nFile, , ExcludeQZSS
                        Genba_S.ExcludeQZSS = br.ReadInt32();
                        //'               Get #nFile, , ExcludeGalileo    '2018 / 08 / 21 Hitz H.Nakamura 衛星数が増えたので64ビットに増やす。
                        if (nVersion < 9300)
                        {
                            //Get #nFile, , ExcludeGalileo1
                            Genba_S.ExcludeGalileo1 = br.ReadInt32();
                            Genba_S.ExcludeGalileo2 = 0;
                        }
                        else
                        {
                            //Get #nFile, , ExcludeGalileo1
                            Genba_S.ExcludeGalileo1 = br.ReadInt32();
                            //Get #nFile, , ExcludeGalileo2
                            Genba_S.ExcludeGalileo2 = br.ReadInt32();
                        }
                        //Get #nFile, , ExcludeBeiDou1
                        Genba_S.ExcludeBeiDou1 = br.ReadInt32();
                        //Get #nFile, , ExcludeBeiDou2
                        Genba_S.ExcludeBeiDou2 = br.ReadInt32();
                        //Get #nFile, , QZSSFlag
                        Genba_S.QZSSFlag = document.GetFileBool(br);
                        //Get #nFile, , GalileoFlag
                        Genba_S.GalileoFlag = document.GetFileBool(br);
                        //Get #nFile, , BeiDouFlag
                        Genba_S.BeiDouFlag = document.GetFileBool(br);
                    }
                    //'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                }
            }
            //************************************************************************
            if (nVersion < 2300) {
                Genba_S.m_nLineType = (int)MdlNSSDefine.OBJ_MODE.OBJ_MODE_ADOPT;
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
                    Genba_S.m_nLineType = (int)MdlNSSDefine.OBJ_MODE.OBJ_MODE_ADOPT;
                } else {
                    Genba_S.m_nLineType = (int)MdlNSSDefine.OBJ_MODE.OBJ_MODE_CHECK;
                }
                //--------------------------------
            }
            else
            {
                //Get #nFile, , m_nLineType
                Genba_S.m_nLineType = br.ReadInt32();
            }

            //************************************************************************
            if (nVersion < 3800)
            {
                Genba_S.StrPcvVer = null;
                Genba_S.EndPcvVer = null;
            }
            else
            {
                //Get #nFile, , StrPcvVer
                Genba_S.StrPcvVer = Utility.FileRead_GetString(br);
                //Get #nFile, , EndPcvVer
                Genba_S.EndPcvVer = Utility.FileRead_GetString(br);
            }

            //************************************************************************
            if (nVersion < 4900)
            {
                Genba_S.AnalysisOrder = 0xFFFFFFF;  //'最低順位。
            }
            else
            {
                //Get #nFile, , AnalysisOrder
                Genba_S.AnalysisOrder = br.ReadInt32();
                if (nVersion < 5900)
                {
                    Genba_S.AnalysisOrder = Genba_S.AnalysisOrder & 0xFFFFFFF;  //'上位４ビットは潰しとく。
                    Genba_S.AnalysisOrder = Genba_S.AnalysisOrder | 0x8000000;  //'第２８ビットをONにする(非解析)。
                }
            }
            //************************************************************************
            //[VB]  Call m_clsStrPoint.Load(nFile, nVersion, clsObservationPoints, Nothing)
            //-----------------------------------------------------------------------

             //23/12/20 K.setoguchi@NV---------->>>>>>>>>>>
            //-----------------------------------------------------
            //(del)     ObservationPoint observationPoint = new ObservationPoint();
            //(del)     observationPoint.Load(br, nVersion, ref Genba_S);
            //-----------------------------------------------------
            List<OPA_STRUCT_SUB> OPA_ListStrA = new List<OPA_STRUCT_SUB>();
            Genba_S.OPA_ListStr = OPA_ListStrA;
            //-----------------------------------------------------
            ObservationPoint observationPoint = new ObservationPoint();
            observationPoint.Load(br, nVersion, ref Genba_S, ref Genba_S.OPA_ListStr);
            //-----------------------------------------------------
            //<<<<----23/12/20 K.Setoguchi

            //************************************************************************
            //[VB]  Call m_clsEndPoint.Load(nFile, nVersion, clsObservationPoints, Nothing)
            //------------------------------------------------------------------------

             //23/12/20 K.setoguchi@NV---------->>>>>>>>>>>
            //-----------------------------------------------------
            //(del)     observationPoint.Load(br, nVersion, ref Genba_S);
            //-----------------------------------------------------
            List<OPA_STRUCT_SUB> OPA_ListStrB = new List<OPA_STRUCT_SUB>();
            Genba_S.OPA_ListEnd = OPA_ListStrB;
            //-----------------------------------------------------
            observationPoint.Load(br, nVersion, ref Genba_S, ref Genba_S.OPA_ListEnd);
            //-----------------------------------------------------
            //<<<<<<<<<-----------23/12/20 K.setoguchi@NV


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
                nCoordinateType = (int)MdlNSDefine.COORDINATE_TYPE.COORDINATE_XYZ;
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
            if (nCoordinateType == (int)MdlNSDefine.COORDINATE_TYPE.COORDINATE_FIX)
            {
                coordinatePointFix.Load(br, nVersion, ref Genba_S);
            }
            else
            {
                coordinatePointXYZ.Load(br, nVersion, ref Genba_S);
            }
            //-----------------------------------------------------------------------------------
            //[VB]  Call m_clsVectorAnalysis.Load(nFile, nVersion)
            coordinatePointXYZ.Load(br, nVersion, ref Genba_S);

            //-----------------------------------------------------------------------------------
            //[VB]  Call m_clsDispersion.Load(nFile, nVersion)
            Dispersion dispersion = new Dispersion();
            dispersion.Load(br, ref Genba_S);

            //-----------------------------------------------------------------------------------
            //[VB]  Call m_clsStrDepPattern.Load(nFile, nVersion)
            DepPattern depPattern = new DepPattern();
            depPattern.Load(br, nVersion, ref Genba_S);

            //-----------------------------------------------------------------------------------
            //[VB]  Call m_clsEndDepPattern.Load(nFile, nVersion)
            depPattern.Load(br, nVersion, ref Genba_S);

            //-----------------------------------------------------------------------------------
            //[VB]  If nVersion >= 1700 Then Call m_clsObsInfo.Load(nFile, nVersion)
            if (nVersion >= 1700) {
                ObsInfo obsInfo = new ObsInfo();
                obsInfo.Load(br, nVersion, ref Genba_S);
            }

            //-----------------------------------------------------------------------------------
            //[VB]  Call m_clsAmbInfo.Load(nFile, nVersion)
            AmbInfo ambInfo = new AmbInfo();
            ambInfo.Load(br, nVersion, ref Genba_S);

            //-----------------------------------------------------------------------------------
            //[VB]  Call m_clsStrOffsetL1.Load(nFile, nVersion)
            coordinatePointXYZ.Load(br, nVersion, ref Genba_S);

            //-----------------------------------------------------------------------------------
            //[VB]  Call m_clsStrOffsetL2.Load(nFile, nVersion)
            coordinatePointXYZ.Load(br, nVersion, ref Genba_S);

            //-----------------------------------------------------------------------------------
            //[VB]  If nVersion >= 1800 Then
            //[VB]      Call m_clsEndOffsetL1.Load(nFile, nVersion)
            //[VB]      Call m_clsEndOffsetL2.Load(nFile, nVersion)
            //[VB]  End If
            if (nVersion >= 1800)
            {
                coordinatePointXYZ.Load(br, nVersion, ref Genba_S);
                coordinatePointXYZ.Load(br, nVersion, ref Genba_S);
            }
            //-----------------------------------------------------------------------------------
            //[VB]  If nVersion >= 9300 Then
            //[VB]      Call m_clsStrOffsetL5.Load(nFile, nVersion)
            //[VB]      Call m_clsEndOffsetL5.Load(nFile, nVersion)
            //[VB]  End If
            //
            if (nVersion >= 9300)
            {

                //検討      m_clsObsDataMask.Count = 0;
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

                //検討      m_clsObsDataMask.Count = 0;
            }
            else
            {
                ObsDataMask obsDataMask = new ObsDataMask();
                obsDataMask.Load(br, nVersion, ref Genba_S);
            }



        }

    }





}
