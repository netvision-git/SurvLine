using SurvLine.mdl;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static SurvLine.GENBA_STRUCT_S;
using static SurvLine.mdl.MdlNSDefine;
using static SurvLine.mdl.MdlNSSDefine;
using static System.Collections.Specialized.BitVector32;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace SurvLine
{
    public class ObservationPointAttributes
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
        //    public SatelliteInfo As Object '衛星数情報。
        public bool GlonassFlag;        //As Boolean 'GLONASSフラグ。
        //'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        public bool IsList;             //As Boolean 'リスト更新必要フラグ。
        //'2022/02/07 SattSignal の追加。''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        public int SattSignalGPS;           //'GPS衛星信号。ビットフラグ。0x01＝L1、0x02＝L2、0x04＝L5。
        public int SattSignalGLONASS;       //'GLONASS衛星信号。ビットフラグ。0x01＝G1、0x02＝G2、0x04＝G3。
        public int SattSignalQZSS;          //'QZSS衛星信号。ビットフラグ。0x01＝L1、0x02＝L2、0x04＝L5。
        public int SattSignalGalileo;       //'Galileo衛星信号。ビットフラグ。0x01＝E1、0x02＝E5。
        public int SattSignalBeiDou;        //'BeiDou衛星信号。ビットフラグ。0x01＝B1、0x02＝B2、0x04＝B3。
        //'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''



        //***************************************************************************
        //***************************************************************************
        /// <summary>
        //'読み込み。
        /// （VB GetString関数をVC用に変更）
        ///  '引き数：
        /// バイナリファイル
        /// バージョン番号
        /// 読み込みデータ
        /// </summary>
        /// <param name="BinaryReader br">
        /// <param name="nVersion br">
        /// <param name="Genba_S">
        /// </param>
        /// <returns>
        /// 戻り値:returns = 
        /// </returns>
        //***************************************************************************
        //[VB] Public Sub Load(ByVal nFile As Integer, ByVal nVersion As Long)
        public void Load(BinaryReader br, long nVersion, ref GENBA_STRUCT_S Genba_S)
        {
            MdlUtility mdlUtility = new MdlUtility();
            Document document = new Document();
            //MdlNSDefine mdlNSDefine

            byte[] buf = new byte[8]; int ret;


            if (nVersion < 7000) {
                //Mode = OBJ_MODE_ADOPT
                Genba_S.OPA.Mode = (int)MdlNSSDefine.OBJ_MODE.OBJ_MODE_ADOPT;
            }
            else
            {
                //Get #nFile, , Mode
                Genba_S.OPA.Mode = br.ReadInt32();

            }
            //---------------------------------------
            //[VB]  Session = GetString(nFile)  //(Ex.)"017H"
            Genba_S.OPA.Session = mdlUtility.FileRead_GetString(br);

            if (nVersion < 2000) {
                Genba_S.OPA.ProvisionalSession = false;
            }
            else
            {
                //[VB]Get #nFile, , ProvisionalSession
                Genba_S.OPA.ProvisionalSession = document.GetFileBool(br);
            }
            //---------------------------------------
            //[VB]FileTitle = GetString(nFile)
            Genba_S.OPA.FileTitle = mdlUtility.FileRead_GetString(br);
            //---------------------------------------
            //[VB]RinexExt = GetString(nFile)
            Genba_S.OPA.RinexExt = mdlUtility.FileRead_GetString(br);
            //---------------------------------------
            //[VB]SrcPath = GetString(nFile)
            Genba_S.OPA.SrcPath = mdlUtility.FileRead_GetString(br);
            //---------------------------------------
            //[VB]  Get #nFile, , StrTimeGPS
            ret = br.Read(buf, 0, 8);
            Genba_S.OPA.StrTimeGPS = DateTime.FromOADate(BitConverter.ToDouble(buf, 0));
            //---------------------------------------
            //[VB]  Get #nFile, , EndTimeGPS
            ret = br.Read(buf, 0, 8);
            Genba_S.OPA.EndTimeGPS = DateTime.FromOADate(BitConverter.ToDouble(buf, 0));
            //---------------------------------------
            //[VB]  Get #nFile, , LeapSeconds
            Genba_S.OPA.LeapSeconds = br.ReadInt32();
            //-----------------------------------------------------------------------------------
            //[VB]  Get #nFile, , Interval          public double Interval;         //As Double '間隔(秒)。
            Genba_S.OPA.Interval = br.ReadDouble();
            //-----------------------------------------------------------------------------------
            //[VB]  RecType = GetString(nFile)      public string RecType;          //As String '受信機名称。
            Genba_S.OPA.RecType = mdlUtility.FileRead_GetString(br);
            //-----------------------------------------------------------------------------------
            //[VB]  RecNumber = GetString(nFile)    public string RecNumber;        //As String '受信機シリアル。RecType
            Genba_S.OPA.RecNumber = mdlUtility.FileRead_GetString(br);

            //-----------------------------------------------------------------------------------
            //[VB]  AntType = GetString(nFile)      public string AntType;          //As String 'アンテナ種別。
            Genba_S.OPA.AntType = mdlUtility.FileRead_GetString(br);
            //-----------------------------------------------------------------------------------
            //[VB]  If nVersion < 3800 Then AntType = GetPrivateProfileString(PROFILE_SUB_SEC_SECV1TOSECV2, AntType, GetPrivateProfileString(PROFILE_ANT_SEC_LIST, PROFILE_ANT_KEY_ANT & GetPrivateProfileString(PROFILE_ANT_SEC_LIST, PROFILE_ANT_KEY_UNKNOWN, "0", App.Path & "\" & PROFILE_ANT_FILE), "", App.Path & "\" & PROFILE_ANT_FILE), App.Path & "\" & PROFILE_SUB_FILE)
            ///            if (nVersion < 3800){
            //検討                Genba_S.OPA.AntType = GetPrivateProfileString(PROFILE_SUB_SEC_SECV1TOSECV2, Genba_S.OPA.AntType, GetPrivateProfileString(PROFILE_ANT_SEC_LIST, PROFILE_ANT_KEY_ANT & GetPrivateProfileString(PROFILE_ANT_SEC_LIST, PROFILE_ANT_KEY_UNKNOWN, "0", App.Path & "\" & PROFILE_ANT_FILE), "", App.Path & "\" & PROFILE_ANT_FILE), App.Path & "\" & PROFILE_SUB_FILE)
            //            }
            //-----------------------------------------------------------------------------------
            //[VB]  AntNumber = GetString(nFile)    public string AntNumber;        //As String 'アンテナシリアル番号。
            Genba_S.OPA.AntNumber = mdlUtility.FileRead_GetString(br);

            //-----------------------------------------------------------------------------------
            //[VB]  AntMeasurement = GetString(nFile)   public string AntMeasurement;   //As String 'アンテナ測位方法。
            Genba_S.OPA.AntMeasurement = mdlUtility.FileRead_GetString(br);

            //-----------------------------------------------------------------------------------
            //[VB]  Get #nFile, , AntHeight         public double AntHeight;        //As Double 'アンテナ高(ｍ)。
            Genba_S.OPA.AntHeight = br.ReadDouble();

            //-----------------------------------------------------------------------------------
            //[VB]  Get #nFile, , ElevationMask     public double ElevationMask;    //As Double '仰角マスク(度)。
            Genba_S.OPA.ElevationMask = br.ReadDouble();

            //-----------------------------------------------------------------------------------
            //[VB]  Get #nFile, , NumberOfMinSV     public long NumberOfMinSV;      //As Long '最少衛星数。
            Genba_S.OPA.NumberOfMinSV = br.ReadInt32();


            //********************************************************
            //'インポートファイル種別。 public enum IMPORT_TYPE
            //********************************************************
            //[VB]  If nVersion < 1400 Then
            //[VB]    If StrConv(Right$(SrcPath, 4), vbUpperCase) = ".DAT" Then
            //[VB]        ImportType = IMPORT_TYPE_DAT
            //[VB]    Else
            //[VB]        ImportType = IMPORT_TYPE_UNKNOWN
            //[VB]    End If
            //[VB]  Else
            //[VB]     Get #nFile, , ImportType
            //[VB]  End If
#if true
            string SrcPath = @"C:\Documents and Settings\shuji\デスクトップ\GPSデ－タ\Netsurvデ－タファイル\20120117NGS\受信機1\305017H.DAT";
#endif
            if (nVersion < 1400) {
                if (SrcPath.Contains(".DAT"))
                {
                    Genba_S.OPA.ImportType = (int)MdlNSDefine.IMPORT_TYPE.IMPORT_TYPE_DAT;
                } else {
                    Genba_S.OPA.ImportType = (int)MdlNSDefine.IMPORT_TYPE.IMPORT_TYPE_UNKNOWN;
                }
            } else {
                //-----------------------------------------------------------------------------------
                //[VB]  Get #nFile, , ImportType        public int ImportType;          //As IMPORT_TYPE 'インポートファイルの種別。
                Genba_S.OPA.ImportType = br.ReadInt32();
            }
            //-----------------------------------------------------------------------------------


            //********************************************************
            if (nVersion < 8600) {
                Genba_S.OPA.GlonassFlag = false;
            }
            else {
                //[VB] Get #nFile, , GlonassFlag     public bool GlonassFlag;        //As Boolean 'GLONASSフラグ。
                Genba_S.OPA.GlonassFlag = document.GetFileBool(br);
            }

            //********************************************************
            //'2017/07/06 NS6000対応。'''''''''''''''''''''''''''''''''''''''''''''''''''''''
            if (nVersion < 9100) {
                Genba_S.OPA.QZSSFlag = false;
                Genba_S.OPA.GalileoFlag = false;
                Genba_S.OPA.BeiDouFlag = false;
                Genba_S.OPA.MixedNav = false;
                Genba_S.OPA.RinexVersion = 2110;
            } else {
                //[VB] Get #nFile, , QZSSFlag      public bool QZSSFlag;           //As Boolean 'QZSSフラグ。
                Genba_S.OPA.QZSSFlag = document.GetFileBool(br);
                //[VB] Get #nFile, , GalileoFlag   public bool GalileoFlag;        //As Boolean 'Galileoフラグ。
                Genba_S.OPA.GalileoFlag = document.GetFileBool(br);
                //[VB] Get #nFile, , BeiDouFlag    public bool BeiDouFlag;         //As Boolean 'BeiDouフラグ。
                Genba_S.OPA.BeiDouFlag = document.GetFileBool(br);
                //[VB] Get #nFile, , MixedNav      public bool MixedNav;           //As Boolean '混合タイプの暦ファイルか？
                Genba_S.OPA.MixedNav = document.GetFileBool(br);
                //[VB] Get #nFile, , RinexVersion  public long RinexVersion;       //As Long 'RINEXファイルのバージョン。バージョン番号を1000倍した整数。
                Genba_S.OPA.RinexVersion = br.ReadInt32();
            }


            //********************************************************
            //    Call m_clsCommon.Load(nFile, nVersion)                            OK
            ObservationCommonAttributes observationCommonAttributes = new ObservationCommonAttributes();
            observationCommonAttributes.Load(br, nVersion, ref Genba_S);

            //********************************************************
            //    Call m_clsCoordinateObservation.Load(nFile, nVersion)             OK
            CoordinatePointXYZ coordinatePointXYZ = new CoordinatePointXYZ();
            coordinatePointXYZ.Load(br, nVersion, ref Genba_S);

            //********************************************************
            if (nVersion < 4600) {
                Genba_S.OPA.ElevationMaskHand = -1;
                Genba_S.OPA.NumberOfMinSVHand = 0;
            } else {
                //[VB]  Get #nFile, , ElevationMaskHand   public long ElevationMaskHand;  //As Long '手簿に出力する最低高度角(度)。負の値の場合OFFとする。                          -1
                Genba_S.OPA.ElevationMaskHand = br.ReadInt32();
                //[VB]  Get #nFile, , NumberOfMinSVHand   public long NumberOfMinSVHand;  //As Long '手簿に出力する最少衛星数。0の場合はオプション設定から取得。1～12が最少衛星数。  0
                Genba_S.OPA.NumberOfMinSVHand = br.ReadInt32();
            }

            //    '2022/03/10 Hitz H.Nakamura '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            //    '衛星情報の OBS TYPES は信号が無くても記載されていたので記載しないように修正。
            //    'すでに読み込み済みのデータはあらためて評価する。
            //    ''2022 / 02 / 07 SattSignal の追加。''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            //
            //省略
            //    '    Next
            //    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''


            if (nVersion < 9500){
                if (nVersion < 9400){
                }else
                {
                    //---------------------------------------------
                    //Get #nFile, , SattSignalGPS
                    Genba_S.OPA.SattSignalGPS = br.ReadInt32();

                    //---------------------------------------------
                    //Get #nFile, , SattSignalGLONASS
                    Genba_S.OPA.SattSignalGLONASS = br.ReadInt32();

                    //---------------------------------------------
                    //Get #nFile, , SattSignalQZSS
                    Genba_S.OPA.SattSignalQZSS = br.ReadInt32();

                    //---------------------------------------------
                    //Get #nFile, , SattSignalGalileo
                    Genba_S.OPA.SattSignalGalileo = br.ReadInt32();

                    //---------------------------------------------
                    //Get #nFile, , SattSignalBeiDou
                    Genba_S.OPA.SattSignalBeiDou = br.ReadInt32();
                }


                //'2023/07/26 Hitz H.Nakamura **************************************************
                //'偏心点など衛星情報がない場合に対応できていなかった。
                //        If FileTitle = "" Then
                if (Genba_S.OPA.FileTitle.Length == 0){                     //"017H305"
                    SattSignalGPS = 0;
                    SattSignalGLONASS = 0;
                    SattSignalQZSS = 0;
                    SattSignalGalileo = 0;
                    SattSignalBeiDou = 0;
                }
                else
                {
                    //'*****************************************************************************
                    long nSattSignalGPS = 0;        //As Long
                    long nSattSignalGLONASS = 0;    //As Long
                    long nSattSignalQZSS = 0;       //As Long
                    long nSattSignalGalileo = 0;    // As Long
                    long nSattSignalBeiDou = 0;     // As Long

                    //-------------------------------------------------------------------------------
                    //[VB]  Dim clsSatelliteInfoReader As SatelliteInfoReader
                    //[VB]  Dim clsProgressInterface As New ProgressInterface
                    //[VB]  Set clsSatelliteInfoReader = New SatelliteInfoReader
                    //[VB]  Call clsSatelliteInfoReader.OpenFile(App.Path & TEMPORARY_PATH & "." & OBSPOINT_PATH & FileTitle & "." & RNX_SV_EXTENSION)
                    //[VB]  Call clsSatelliteInfoReader.ReadSattSignal(nSattSignalGPS, nSattSignalGLONASS, nSattSignalQZSS, nSattSignalGalileo, nSattSignalBeiDou, clsProgressInterface)
                    string App_Path = @"C:\Develop\NetSurv\Src\NS-App\NS-Survey";
                    SatelliteInfoReader satelliteInfoReader = new SatelliteInfoReader();
                    //--------------------------------------------------------------------"C:\Develop\NetSurv\Src\NS-App\NS-Survey\Temp\.\ObsPoint\017H305.log"

                    //23/12/24 K.setoguchi@NV---------->>>>>>>>>>
                    //---------------------------------------------------------------------------------------------------
                    //(del)     satelliteInfoReader.OpenFile($"{App_Path}{TEMPORARY_PATH}.{OBSPOINT_PATH}{FileTitle}.RNX_SV_EXTENSION");
                    //      satelliteInfoReader.OpenFile($"{App_Path}{TEMPORARY_PATH}.{OBSPOINT_PATH}{Genba_S.OPA.FileTitle}.RNX_SV_EXTENSION");
                    //---------------------------------------------------------------------------------------------------
                    //
                    //-----------------------------
                    //<<< 衛星信号の読み込み。 >>>
                    //-----------------------------
                    //(del) satelliteInfoReader.ReadSattSignal(nSattSignalGPS, nSattSignalGLONASS, nSattSignalQZSS, nSattSignalGalileo, nSattSignalBeiDou);
                    string SattAatePath = $"{App_Path}{TEMPORARY_PATH}{OBSPOINT_PATH}{Genba_S.OPA.FileTitle}.{MdlRINEXTYPE.RNX_SV_EXTENSION}";
                    satelliteInfoReader.ReadSattSignal(SattAatePath, ref nSattSignalGPS, ref nSattSignalGLONASS, ref nSattSignalQZSS, ref nSattSignalGalileo, ref nSattSignalBeiDou);
                    //---------------------------------------------------------------------------------------------------
                    //<<<<<<<<<-----------23/12/24 K.setoguchi@NV


                    //-------------------------------------------------------------------------------
                    //'        '2022 / 03 / 14 Hitz H.Nakamura ''''''''''''''''''''''''''''''''''''''''
                    //'        'Ver2.8.0 ではまだRE - αへの対応を謳わない。
                    //'        '次のバージョンでRE - αに対応して地理院へのNS - Surveyの登録も更新する。
                    //'        'それまでは「解析に使用した周波数」が今までと変化しないようにする。
                    //'        nSattSignalGPS = &H1 Or &H2
                    //'        nSattSignalGLONASS = nSattSignalGPS
                    //'        nSattSignalQZSS = nSattSignalGPS
                    //'        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

                    Genba_S.OPA.SattSignalGPS = nSattSignalGPS;

                    if(Genba_S.OPA.GlonassFlag){
                        Genba_S.OPA.SattSignalGLONASS = nSattSignalGLONASS;
                    }
                    else
                    {
                        Genba_S.OPA.SattSignalGLONASS = 0;                  //0
                    }
                    if (Genba_S.OPA.QZSSFlag){
                        Genba_S.OPA.SattSignalQZSS = nSattSignalQZSS;
                    }
                    else
                    {
                        Genba_S.OPA.SattSignalQZSS = 0;                     //0
                    }
                    if(Genba_S.OPA.GalileoFlag){
                        Genba_S.OPA.SattSignalGalileo = nSattSignalGalileo;
                    }
                    else
                    {
                        Genba_S.OPA.SattSignalGalileo = 0;                  //0
                    }
                    if (Genba_S.OPA.BeiDouFlag){
                        Genba_S.OPA.SattSignalBeiDou = nSattSignalBeiDou;
                    }
                    else
                    {
                        Genba_S.OPA.SattSignalBeiDou = 0;                   //0
                    }


                } //End If '2023/07/26 Hitz H.Nakamura 偏心点など衛星情報がない場合に対応できていなかった。
                //'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            }
            else
            {
                //---------------------------------------------
                //Get #nFile, , SattSignalGPS
                Genba_S.OPA.SattSignalGPS = br.ReadInt32();

                //---------------------------------------------
                //Get #nFile, , SattSignalGLONASS
                Genba_S.OPA.SattSignalGLONASS = br.ReadInt32();

                //---------------------------------------------
                //Get #nFile, , SattSignalQZSS
                Genba_S.OPA.SattSignalQZSS = br.ReadInt32();

                //---------------------------------------------
                //Get #nFile, , SattSignalGalileo
                Genba_S.OPA.SattSignalGalileo = br.ReadInt32();

                //---------------------------------------------
                //Get #nFile, , SattSignalBeiDou
                Genba_S.OPA.SattSignalBeiDou = br.ReadInt32();

            }
            //'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        }
        //-------------------------------------------------------------------------------
        //'読み込み。
        //'
        //'引き数：
        //'nFile ファイル番号。
        //'nVersion ファイルバージョン。
        //Public Sub Load(ByVal nFile As Integer, ByVal nVersion As Long)
        //    If nVersion< 7000 Then
        //        Mode = OBJ_MODE_ADOPT
        //    Else
        //        Get #nFile, , Mode
        //    End If
        //    Session = GetString(nFile)
        //    If nVersion < 2000 Then
        //        ProvisionalSession = False
        //    Else
        //        Get #nFile, , ProvisionalSession
        //    End If
        //    FileTitle = GetString(nFile)
        //    RinexExt = GetString(nFile)
        //    SrcPath = GetString(nFile)
        //    Get #nFile, , StrTimeGPS
        //    Get #nFile, , EndTimeGPS
        //    Get #nFile, , LeapSeconds
        //    Get #nFile, , Interval
        //    RecType = GetString(nFile)
        //    RecNumber = GetString(nFile)
        //    AntType = GetString(nFile)
        //    If nVersion < 3800 Then AntType = GetPrivateProfileString(PROFILE_SUB_SEC_SECV1TOSECV2, AntType, GetPrivateProfileString(PROFILE_ANT_SEC_LIST, PROFILE_ANT_KEY_ANT & GetPrivateProfileString(PROFILE_ANT_SEC_LIST, PROFILE_ANT_KEY_UNKNOWN, "0", App.Path & "\" & PROFILE_ANT_FILE), "", App.Path & "\" & PROFILE_ANT_FILE), App.Path & "\" & PROFILE_SUB_FILE)
        //    AntNumber = GetString(nFile)
        //    AntMeasurement = GetString(nFile)
        //    Get #nFile, , AntHeight
        //    Get #nFile, , ElevationMask
        //    Get #nFile, , NumberOfMinSV
        //
        //    If nVersion < 1400 Then
        //        If StrConv(Right$(SrcPath, 4), vbUpperCase) = ".DAT" Then
        //            ImportType = IMPORT_TYPE_DAT
        //        Else
        //            ImportType = IMPORT_TYPE_UNKNOWN
        //        End If
        //    Else
        //        Get #nFile, , ImportType
        //    End If
        //    If nVersion < 8600 Then
        //        GlonassFlag = False
        //    Else
        //        Get #nFile, , GlonassFlag
        //    End If
        //    '2017/07/06 NS6000対応。'''''''''''''''''''''''''''''''''''''''''''''''''''''''
        //    If nVersion < 9100 Then
        //        QZSSFlag = False
        //        GalileoFlag = False
        //        BeiDouFlag = False
        //        MixedNav = False
        //        RinexVersion = 2110
        //    Else
        //        Get #nFile, , QZSSFlag
        //        Get #nFile, , GalileoFlag
        //        Get #nFile, , BeiDouFlag
        //        Get #nFile, , MixedNav
        //        Get #nFile, , RinexVersion
        //    End If
        //    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        //    Call m_clsCommon.Load(nFile, nVersion)
        //    Call m_clsCoordinateObservation.Load(nFile, nVersion)
        //    If nVersion < 4600 Then
        //        ElevationMaskHand = -1
        //        NumberOfMinSVHand = 0
        //    Else
        //        Get #nFile, , ElevationMaskHand
        //        Get #nFile, , NumberOfMinSVHand
        //    End If
        //
        //
        //    '2022/03/10 Hitz H.Nakamura '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        //    '衛星情報の OBS TYPES は信号が無くても記載されていたので記載しないように修正。
        //    'すでに読み込み済みのデータはあらためて評価する。
        //    ''2022 / 02 / 07 SattSignal の追加。''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        //
        //省略
        //
        //    '    sTypeOfObserv = clsSatelliteInfoHeader.TypeOfObservC
        //    '    For i = 0 To clsSatelliteInfoHeader.NumberObservC - 1
        //    '        sObs = Left(sTypeOfObserv(i), 2)
        //    '        If StrComp(sObs, "L1") = 0 Then
        //    '            SattSignalBeiDou = SattSignalBeiDou Or &H1
        //    '        ElseIf StrComp(sObs, "L7") = 0 Then
        //    '            SattSignalBeiDou = SattSignalBeiDou Or &H2
        //    '        ElseIf StrComp(sObs, "L6") = 0 Then
        //    '            SattSignalBeiDou = SattSignalBeiDou Or &H4
        //    '        End If
        //    '    Next
        //    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        //    If nVersion < 9500 Then
        //        If nVersion < 9400 Then
        //        Else
        //            Get #nFile, , SattSignalGPS
        //            Get #nFile, , SattSignalGLONASS
        //            Get #nFile, , SattSignalQZSS
        //            Get #nFile, , SattSignalGalileo
        //            Get #nFile, , SattSignalBeiDou
        //        End If
        //
        //
        //        '2023/07/26 Hitz H.Nakamura **************************************************
        //        '偏心点など衛星情報がない場合に対応できていなかった。
        //        If FileTitle = "" Then
        //            SattSignalGPS = 0
        //            SattSignalGLONASS = 0
        //            SattSignalQZSS = 0
        //            SattSignalGalileo = 0
        //            SattSignalBeiDou = 0
        //        Else
        //        '*****************************************************************************
        //
        //            Dim nSattSignalGPS As Long
        //            Dim nSattSignalGLONASS  As Long
        //            Dim nSattSignalQZSS  As Long
        //            Dim nSattSignalGalileo  As Long
        //            Dim nSattSignalBeiDou  As Long
        //
        //
        //            Dim clsSatelliteInfoReader As SatelliteInfoReader
        //            Dim clsProgressInterface As New ProgressInterface
        //            Set clsSatelliteInfoReader = New SatelliteInfoReader
        //            Call clsSatelliteInfoReader.OpenFile(App.Path & TEMPORARY_PATH & "." & OBSPOINT_PATH & FileTitle & "." & RNX_SV_EXTENSION)
        //            Call clsSatelliteInfoReader.ReadSattSignal(nSattSignalGPS, nSattSignalGLONASS, nSattSignalQZSS, nSattSignalGalileo, nSattSignalBeiDou, clsProgressInterface)
        //
        //
        //    '        '2022 / 03 / 14 Hitz H.Nakamura ''''''''''''''''''''''''''''''''''''''''
        //    '        'Ver2.8.0 ではまだRE - αへの対応を謳わない。
        //    '        '次のバージョンでRE - αに対応して地理院へのNS - Surveyの登録も更新する。
        //    '        'それまでは「解析に使用した周波数」が今までと変化しないようにする。
        //    '        nSattSignalGPS = &H1 Or &H2
        //    '        nSattSignalGLONASS = nSattSignalGPS
        //    '        nSattSignalQZSS = nSattSignalGPS
        //    '        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        //
        //            SattSignalGPS = nSattSignalGPS
        //            If GlonassFlag Then
        //                SattSignalGLONASS = nSattSignalGLONASS
        //            Else
        //                SattSignalGLONASS = 0
        //            End If
        //            If QZSSFlag Then
        //                SattSignalQZSS = nSattSignalQZSS
        //            Else
        //                SattSignalQZSS = 0
        //            End If
        //            If GalileoFlag Then
        //                SattSignalGalileo = nSattSignalGalileo
        //            Else
        //                SattSignalGalileo = 0
        //            End If
        //            If BeiDouFlag Then
        //                SattSignalBeiDou = nSattSignalBeiDou
        //            Else
        //                SattSignalBeiDou = 0
        //            End If
        //
        //
        //        End If '2023/07/26 Hitz H.Nakamura 偏心点など衛星情報がない場合に対応できていなかった。
        //    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        //    Else
        //        Get #nFile, , SattSignalGPS
        //        Get #nFile, , SattSignalGLONASS
        //        Get #nFile, , SattSignalQZSS
        //        Get #nFile, , SattSignalGalileo
        //        Get #nFile, , SattSignalBeiDou
        //    End If
        //    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        //End Sub
        //***************************************************************************
        //***************************************************************************




    }
}
