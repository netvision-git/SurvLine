using SurvLine.mdl;
using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static SurvLine.GENBA_STRUCT_S;
using static SurvLine.mdl.DEFINE;
using static SurvLine.mdl.MdlNSDefine;
using static SurvLine.mdl.MdlUtility;
using static SurvLine.mdl.MdlAccountMake;
using static SurvLine.mdl.MdlNSGUI;
using static SurvLine.mdl.MdlNSSDefine;
using static SurvLine.mdl.MdiVBfunctions;
using static System.Collections.Specialized.BitVector32;
using static System.Net.WebRequestMethods;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using System.Windows.Forms;

namespace SurvLine
{
    public class ObservationPointAttributes
    {




        public ObservationPointAttributes(MdlMain mdlMain)
        {
            this.mdlMain = mdlMain;
            document = mdlMain.GetDocument();
            m_clsCommon = new ObservationCommonAttributes(mdlMain);
        }

        //==========================================================================================
        /*[VB]
        '*******************************************************************************
        '観測点属性

        Option Explicit
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'プロパティ
        Public Mode As OBJ_MODE '観測点モード。
        Public Session As String 'セッション名。
        Public ProvisionalSession As Boolean '仮のセッション。
        Public FileTitle As String 'ファイルタイトル。
        Public RinexExt As String 'RINEXファイル拡張子(先頭２文字)。
        Public SrcPath As String 'ソースファイルのパス。
        Public StrTimeGPS As Date '観測開始日時(GPS)。
        Public EndTimeGPS As Date '観測終了日時(GPS)。
        Public LeapSeconds As Long 'うるう秒。
        Public Interval As Double '間隔(秒)。
        Public RecType As String '受信機名称。
        Public RecNumber As String '受信機シリアル。
        Public AntType As String 'アンテナ種別。
        Public AntNumber As String 'アンテナシリアル番号。
        Public AntMeasurement As String 'アンテナ測位方法。
        Public AntHeight As Double 'アンテナ高(ｍ)。
        Public ElevationMask As Double '仰角マスク(度)。
        Public NumberOfMinSV As Long '最少衛星数。
        Public SatelliteInfo As Object '衛星数情報。
        Public ImportType As IMPORT_TYPE 'インポートファイルの種別。
        Public GlonassFlag As Boolean 'GLONASSフラグ。
        '2017/06/06 NS6000対応。'''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Public QZSSFlag As Boolean 'QZSSフラグ。
        Public GalileoFlag As Boolean 'Galileoフラグ。
        Public BeiDouFlag As Boolean 'BeiDouフラグ。
        Public MixedNav As Boolean '混合タイプの暦ファイルか？
        Public RinexVersion As Long 'RINEXファイルのバージョン。バージョン番号を1000倍した整数。
        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Public IsList As Boolean 'リスト更新必要フラグ。
        Public ElevationMaskHand As Long '手簿に出力する最低高度角(度)。負の値の場合OFFとする。
        Public NumberOfMinSVHand As Long '手簿に出力する最少衛星数。0の場合はオプション設定から取得。1～12が最少衛星数。
        '2022/02/07 SattSignal の追加。''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Public SattSignalGPS 'GPS衛星信号。ビットフラグ。0x01＝L1、0x02＝L2、0x04＝L5。
        Public SattSignalGLONASS 'GLONASS衛星信号。ビットフラグ。0x01＝G1、0x02＝G2、0x04＝G3。
        Public SattSignalQZSS 'QZSS衛星信号。ビットフラグ。0x01＝L1、0x02＝L2、0x04＝L5。
        Public SattSignalGalileo 'Galileo衛星信号。ビットフラグ。0x01＝E1、0x02＝E5。
        Public SattSignalBeiDou 'BeiDou衛星信号。ビットフラグ。0x01＝B1、0x02＝B2、0x04＝B3。
        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'プロパティ
        public OBJ_MODE Mode;               //'観測点モード。
        public string Session;              //'セッション名。
        public bool ProvisionalSession;     //'仮のセッション。
        public string FileTitle;            //'ファイルタイトル。
        public string RinexExt;             //'RINEXファイル拡張子(先頭２文字)。
        public string SrcPath;              //'ソースファイルのパス。
        public DateTime StrTimeGPS;         //'観測開始日時(GPS)。
        public DateTime EndTimeGPS;         //'観測終了日時(GPS)。
        public long LeapSeconds;            //'うるう秒。
        public double Interval;             //'間隔(秒)。
        public string RecType;              //'受信機名称。
        public string RecNumber;            //'受信機シリアル。
        public string AntType;              //'アンテナ種別。
        public string AntNumber;            //'アンテナシリアル番号。
        public string AntMeasurement;       //'アンテナ測位方法。
        public double AntHeight;            //'アンテナ高(ｍ)。
        public double ElevationMask;        //'仰角マスク(度)。
        public long NumberOfMinSV;          //'最少衛星数。
        public object SatelliteInfo;        //'衛星数情報。
        public IMPORT_TYPE ImportType;      //'インポートファイルの種別。
        public bool GlonassFlag;            //'GLONASSフラグ。
        //'2017/06/06 NS6000対応。'''''''''''''''''''''''''''''''''''''''''''''''''''''''
        public bool QZSSFlag;               //'QZSSフラグ。
        public bool GalileoFlag;            //'Galileoフラグ。
        public bool BeiDouFlag;             //'BeiDouフラグ。
        public bool MixedNav;               //'混合タイプの暦ファイルか？
        public long RinexVersion;           //'RINEXファイルのバージョン。バージョン番号を1000倍した整数。
        //'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        public bool IsList;                 //'リスト更新必要フラグ。
        public long ElevationMaskHand;      //'手簿に出力する最低高度角(度)。負の値の場合OFFとする。
        public long NumberOfMinSVHand;      //'手簿に出力する最少衛星数。0の場合はオプション設定から取得。1～12が最少衛星数。
        //'2022/02/07 SattSignal の追加。''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        public int SattSignalGPS;           //'GPS衛星信号。ビットフラグ。0x01＝L1、0x02＝L2、0x04＝L5。
        public int SattSignalGLONASS;       //'GLONASS衛星信号。ビットフラグ。0x01＝G1、0x02＝G2、0x04＝G3。
        public int SattSignalQZSS;          //'QZSS衛星信号。ビットフラグ。0x01＝L1、0x02＝L2、0x04＝L5。
        public int SattSignalGalileo;       //'Galileo衛星信号。ビットフラグ。0x01＝E1、0x02＝E5。
        public int SattSignalBeiDou;        //'BeiDou衛星信号。ビットフラグ。0x01＝B1、0x02＝B2、0x04＝B3。
        //'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'インプリメンテーション
        Private m_clsCommon As New ObservationCommonAttributes '観測点共通属性。
        Private m_clsCoordinateObservation As New CoordinatePointXYZ '観測座標。
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'インプリメンテーション
        //private ObservationCommonAttributes m_clsCommon = new ObservationCommonAttributes();    //'観測点共通属性。
        private ObservationCommonAttributes m_clsCommon;                                        //'観測点共通属性。
        private CoordinatePointXYZ m_clsCoordinateObservation = new CoordinatePointXYZ();       //'観測座標。

        private CoordinatePoint m_CoordinatePoint;
        private Document document;
        private MdlMain mdlMain;
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '*******************************************************************************
        'プロパティ

        '観測点共通属性。
        Property Set Common(ByVal clsCommon As ObservationCommonAttributes)
            Set m_clsCommon = clsCommon
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '*******************************************************************************
        'プロパティ

        '観測点共通属性。
        */
        public void Common(ObservationCommonAttributes clsCommon)
        {
            m_clsCommon = clsCommon;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '観測点共通属性。
        Property Get Common() As ObservationCommonAttributes
            Set Common = m_clsCommon
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'観測点共通属性。
        public ObservationCommonAttributes Common()
        {
            return m_clsCommon;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '観測座標。
        Property Let CoordinateObservation(ByVal clsCoordinateObservation As CoordinatePoint)
            Let m_clsCoordinateObservation = clsCoordinateObservation
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'観測座標。
        public void CoordinateObservation(CoordinatePoint clsCoordinateObservation)
        {
            m_clsCoordinateObservation = (CoordinatePointXYZ)clsCoordinateObservation;
            return;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '観測座標。
        Property Get CoordinateObservation() As CoordinatePoint
            Set CoordinateObservation = m_clsCoordinateObservation
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'観測座標。        
        public  CoordinatePoint CoordinateObservation()
        {
#if true
            return m_clsCoordinateObservation;
#else
            //debug
            return new CoordinatePoint();
#endif
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '*******************************************************************************
        'イベント

        '初期化。
        Private Sub Class_Initialize()

            On Error GoTo ErrorHandler


            ElevationMaskHand = -1
    
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
                ElevationMaskHand = -1;
                return;
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

        '保存。
        '
        '引き数：
        'nFile ファイル番号。
        Public Sub Save(ByVal nFile As Integer)
            Put #nFile, , Mode
            Call PutString(nFile, Session)
            Put #nFile, , ProvisionalSession
            Call PutString(nFile, FileTitle)
            Call PutString(nFile, RinexExt)
            Call PutString(nFile, SrcPath)
            Put #nFile, , StrTimeGPS
            Put #nFile, , EndTimeGPS
            Put #nFile, , LeapSeconds
            Put #nFile, , Interval
            Call PutString(nFile, RecType)
            Call PutString(nFile, RecNumber)
            Call PutString(nFile, AntType)
            Call PutString(nFile, AntNumber)
            Call PutString(nFile, AntMeasurement)
            Put #nFile, , AntHeight
            Put #nFile, , ElevationMask
            Put #nFile, , NumberOfMinSV
            Put #nFile, , ImportType
            Put #nFile, , GlonassFlag
            '2017/06/06 NS6000対応。'''''''''''''''''''''''''''''''''''''''''''''''''''''''
            Put #nFile, , QZSSFlag
            Put #nFile, , GalileoFlag
            Put #nFile, , BeiDouFlag
            Put #nFile, , MixedNav
            Put #nFile, , RinexVersion
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            Call m_clsCommon.Save(nFile)
            Call m_clsCoordinateObservation.Save(nFile)
            Put #nFile, , ElevationMaskHand
            Put #nFile, , NumberOfMinSVHand
            '2022/02/07 SattSignal の追加。''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            Put #nFile, , SattSignalGPS
            Put #nFile, , SattSignalGLONASS
            Put #nFile, , SattSignalQZSS
            Put #nFile, , SattSignalGalileo
            Put #nFile, , SattSignalBeiDou
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '*******************************************************************************
        'メソッド

        '保存。
        '
        '引き数：
        'nFile ファイル番号。
        */
        public void Save(int nFil)
        {
            return;
        }
        //==========================================================================================







        /*
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
        */


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
            MdlUtility mdlUtility = new MdlUtility();   //Add K.setoguchi
            //MdlUtility mdlUtility = new MdlUtility();
            ////Document document = new Document();
            //MdlNSDefine mdlNSDefine

            byte[] buf = new byte[8]; int ret;

            string AppPath = Path.GetDirectoryName(Application.ExecutablePath);

            if (nVersion < 7000) {
                //Mode = OBJ_MODE_ADOPT
                Genba_S.OPA.Mode = (int)MdlNSSDefine.OBJ_MODE.OBJ_MODE_ADOPT;
                Mode = (OBJ_MODE)Genba_S.OPA.Mode;
            }
            else
            {
                //Get #nFile, , Mode
                Genba_S.OPA.Mode = br.ReadInt32();
                Mode = (OBJ_MODE)Genba_S.OPA.Mode;

            }
            //---------------------------------------
            //[VB]  Session = GetString(nFile)  //(Ex.)"017H"
            Genba_S.OPA.Session = FileRead_GetString(br);
            Session = Genba_S.OPA.Session;

            if (nVersion < 2000) {
                Genba_S.OPA.ProvisionalSession = false;
                ProvisionalSession = Genba_S.OPA.ProvisionalSession;
            }
            else
            {
                //[VB]Get #nFile, , ProvisionalSession
                Genba_S.OPA.ProvisionalSession = document.GetFileBool(br);
                ProvisionalSession = Genba_S.OPA.ProvisionalSession;
            }
            //---------------------------------------
            //[VB]FileTitle = GetString(nFile)
            Genba_S.OPA.FileTitle = FileRead_GetString(br);
            FileTitle = Genba_S.OPA.FileTitle;
            //---------------------------------------
            //[VB]RinexExt = GetString(nFile)
            Genba_S.OPA.RinexExt = FileRead_GetString(br);
            RinexExt = Genba_S.OPA.RinexExt;
            //---------------------------------------
            //[VB]SrcPath = GetString(nFile)
            Genba_S.OPA.SrcPath = FileRead_GetString(br);
            SrcPath = Genba_S.OPA.SrcPath;
            //---------------------------------------
            //[VB]  Get #nFile, , StrTimeGPS
            ret = br.Read(buf, 0, 8);
            Genba_S.OPA.StrTimeGPS = DateTime.FromOADate(BitConverter.ToDouble(buf, 0));
            StrTimeGPS = Genba_S.OPA.StrTimeGPS;
            //---------------------------------------
            //[VB]  Get #nFile, , EndTimeGPS
            ret = br.Read(buf, 0, 8);
            Genba_S.OPA.EndTimeGPS = DateTime.FromOADate(BitConverter.ToDouble(buf, 0));
            EndTimeGPS = Genba_S.OPA.EndTimeGPS;
            //---------------------------------------
            //[VB]  Get #nFile, , LeapSeconds
            Genba_S.OPA.LeapSeconds = br.ReadInt32();
            LeapSeconds = Genba_S.OPA.LeapSeconds;
            //-----------------------------------------------------------------------------------
            //[VB]  Get #nFile, , Interval          public double Interval;         //As Double '間隔(秒)。
            Genba_S.OPA.Interval = br.ReadDouble();
            Interval = Genba_S.OPA.Interval;
            //-----------------------------------------------------------------------------------
            //[VB]  RecType = GetString(nFile)      public string RecType;          //As String '受信機名称。
            Genba_S.OPA.RecType = FileRead_GetString(br);
            RecType = Genba_S.OPA.RecType;
            //-----------------------------------------------------------------------------------
            //[VB]  RecNumber = GetString(nFile)    public string RecNumber;        //As String '受信機シリアル。RecType
            Genba_S.OPA.RecNumber = FileRead_GetString(br);
            RecNumber = Genba_S.OPA.RecNumber;
            //-----------------------------------------------------------------------------------
            //[VB]  AntType = GetString(nFile)      public string AntType;          //As String 'アンテナ種別。
            Genba_S.OPA.AntType = FileRead_GetString(br);
            AntType = Genba_S.OPA.AntType;
            //-----------------------------------------------------------------------------------
            //[VB]  If nVersion < 3800 Then AntType = GetPrivateProfileString(PROFILE_SUB_SEC_SECV1TOSECV2, AntType, GetPrivateProfileString(PROFILE_ANT_SEC_LIST, PROFILE_ANT_KEY_ANT & GetPrivateProfileString(PROFILE_ANT_SEC_LIST, PROFILE_ANT_KEY_UNKNOWN, "0", App.Path & "\" & PROFILE_ANT_FILE), "", App.Path & "\" & PROFILE_ANT_FILE), App.Path & "\" & PROFILE_SUB_FILE)
            if (nVersion < 3800){
                DEFINE clsDEFINE = new DEFINE();
                string s2 = GetPrivateProfileString(PROFILE_ANT_SEC_LIST, PROFILE_ANT_KEY_UNKNOWN, "0", AppPath + "\\" + PROFILE_ANT_FILE);
                string s3 = GetPrivateProfileString(PROFILE_ANT_SEC_LIST, PROFILE_ANT_KEY_ANT + s2, "", AppPath + "\\" + PROFILE_ANT_FILE);
                Genba_S.OPA.AntType = GetPrivateProfileString(PROFILE_SUB_SEC_SECV1TOSECV2, Genba_S.OPA.AntType, s3, AppPath + "\\" + PROFILE_SUB_FILE);
                AntType = Genba_S.OPA.AntType;
            }
            //-----------------------------------------------------------------------------------
            //[VB]  AntNumber = GetString(nFile)    public string AntNumber;        //As String 'アンテナシリアル番号。
            Genba_S.OPA.AntNumber = FileRead_GetString(br);
            AntNumber = Genba_S.OPA.AntNumber;
            //-----------------------------------------------------------------------------------
            //[VB]  AntMeasurement = GetString(nFile)   public string AntMeasurement;   //As String 'アンテナ測位方法。
            Genba_S.OPA.AntMeasurement = FileRead_GetString(br);
            AntMeasurement = Genba_S.OPA.AntMeasurement;
            //-----------------------------------------------------------------------------------
            //[VB]  Get #nFile, , AntHeight         public double AntHeight;        //As Double 'アンテナ高(ｍ)。
            Genba_S.OPA.AntHeight = br.ReadDouble();
            AntHeight = Genba_S.OPA.AntHeight;
            //-----------------------------------------------------------------------------------
            //[VB]  Get #nFile, , ElevationMask     public double ElevationMask;    //As Double '仰角マスク(度)。
            Genba_S.OPA.ElevationMask = br.ReadDouble();
            ElevationMask = Genba_S.OPA.ElevationMask;
            //-----------------------------------------------------------------------------------
            //[VB]  Get #nFile, , NumberOfMinSV     public long NumberOfMinSV;      //As Long '最少衛星数。
            Genba_S.OPA.NumberOfMinSV = br.ReadInt32();
            NumberOfMinSV = Genba_S.OPA.NumberOfMinSV;

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
            SrcPath = @"C:\Documents and Settings\shuji\デスクトップ\GPSデ－タ\Netsurvデ－タファイル\20120117NGS\受信機1\305017H.DAT";
#endif
            if (nVersion < 1400) {
                if (SrcPath.Contains(".DAT"))
                {
                    Genba_S.OPA.ImportType = (int)IMPORT_TYPE.IMPORT_TYPE_DAT;
                    ImportType = (IMPORT_TYPE)Genba_S.OPA.ImportType;
                } else {
                    Genba_S.OPA.ImportType = (int)IMPORT_TYPE.IMPORT_TYPE_UNKNOWN;
                    ImportType = (IMPORT_TYPE)Genba_S.OPA.ImportType;
                }
            } else {
                //-----------------------------------------------------------------------------------
                //[VB]  Get #nFile, , ImportType        public int ImportType;          //As IMPORT_TYPE 'インポートファイルの種別。
                Genba_S.OPA.ImportType = br.ReadInt32();
                ImportType = (IMPORT_TYPE)Genba_S.OPA.ImportType;
            }
            //-----------------------------------------------------------------------------------


            //********************************************************
            if (nVersion < 8600) {
                Genba_S.OPA.GlonassFlag = false;
                GlonassFlag = Genba_S.OPA.GlonassFlag;
            }
            else {
                //[VB] Get #nFile, , GlonassFlag     public bool GlonassFlag;        //As Boolean 'GLONASSフラグ。
                Genba_S.OPA.GlonassFlag = document.GetFileBool(br);
                GlonassFlag = Genba_S.OPA.GlonassFlag;
            }

            //********************************************************
            //'2017/07/06 NS6000対応。'''''''''''''''''''''''''''''''''''''''''''''''''''''''
            if (nVersion < 9100) {
                Genba_S.OPA.QZSSFlag = false;
                QZSSFlag = Genba_S.OPA.QZSSFlag;
                Genba_S.OPA.GalileoFlag = false;
                GalileoFlag = Genba_S.OPA.GalileoFlag;
                Genba_S.OPA.BeiDouFlag = false;
                BeiDouFlag = Genba_S.OPA.BeiDouFlag;
                Genba_S.OPA.MixedNav = false;
                MixedNav = Genba_S.OPA.MixedNav;
                Genba_S.OPA.RinexVersion = 2110;
                RinexVersion = Genba_S.OPA.RinexVersion;
            }
            else {
                //[VB] Get #nFile, , QZSSFlag      public bool QZSSFlag;           //As Boolean 'QZSSフラグ。
                Genba_S.OPA.QZSSFlag = document.GetFileBool(br);
                QZSSFlag = Genba_S.OPA.QZSSFlag;
                //[VB] Get #nFile, , GalileoFlag   public bool GalileoFlag;        //As Boolean 'Galileoフラグ。
                Genba_S.OPA.GalileoFlag = document.GetFileBool(br);
                GalileoFlag = Genba_S.OPA.GalileoFlag;
                //[VB] Get #nFile, , BeiDouFlag    public bool BeiDouFlag;         //As Boolean 'BeiDouフラグ。
                Genba_S.OPA.BeiDouFlag = document.GetFileBool(br);
                BeiDouFlag = Genba_S.OPA.BeiDouFlag;
                //[VB] Get #nFile, , MixedNav      public bool MixedNav;           //As Boolean '混合タイプの暦ファイルか？
                Genba_S.OPA.MixedNav = document.GetFileBool(br);
                MixedNav = Genba_S.OPA.MixedNav;
                //[VB] Get #nFile, , RinexVersion  public long RinexVersion;       //As Long 'RINEXファイルのバージョン。バージョン番号を1000倍した整数。
                Genba_S.OPA.RinexVersion = br.ReadInt32();
                RinexVersion = Genba_S.OPA.RinexVersion;
            }


            //********************************************************
            //    Call m_clsCommon.Load(nFile, nVersion)                            OK
            //ObservationCommonAttributes observationCommonAttributes = new ObservationCommonAttributes();
            //observationCommonAttributes.Load(br, nVersion, ref Genba_S);
            m_clsCommon.Load(br, nVersion, ref Genba_S);

            //********************************************************
            //    Call m_clsCoordinateObservation.Load(nFile, nVersion)             OK
            //CoordinatePointXYZ coordinatePointXYZ = new CoordinatePointXYZ();
            //coordinatePointXYZ.Load(br, nVersion, ref Genba_S);
            m_clsCoordinateObservation.Load(br, nVersion, ref Genba_S);

            //********************************************************
            if (nVersion < 4600) {
                Genba_S.OPA.ElevationMaskHand = -1;
                ElevationMaskHand = Genba_S.OPA.ElevationMaskHand;
                Genba_S.OPA.NumberOfMinSVHand = 0;
                NumberOfMinSVHand = Genba_S.OPA.NumberOfMinSVHand;
            }
            else {
                //[VB]  Get #nFile, , ElevationMaskHand   public long ElevationMaskHand;  //As Long '手簿に出力する最低高度角(度)。負の値の場合OFFとする。                          -1
                Genba_S.OPA.ElevationMaskHand = br.ReadInt32();
                ElevationMaskHand = Genba_S.OPA.ElevationMaskHand;
                //[VB]  Get #nFile, , NumberOfMinSVHand   public long NumberOfMinSVHand;  //As Long '手簿に出力する最少衛星数。0の場合はオプション設定から取得。1～12が最少衛星数。  0
                Genba_S.OPA.NumberOfMinSVHand = br.ReadInt32();
                NumberOfMinSVHand = Genba_S.OPA.NumberOfMinSVHand;
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
                    SattSignalGPS = (int)Genba_S.OPA.SattSignalGPS;

                    //---------------------------------------------
                    //Get #nFile, , SattSignalGLONASS
                    Genba_S.OPA.SattSignalGLONASS = br.ReadInt32();
                    SattSignalGLONASS = (int)Genba_S.OPA.SattSignalGLONASS;

                    //---------------------------------------------
                    //Get #nFile, , SattSignalQZSS
                    Genba_S.OPA.SattSignalQZSS = br.ReadInt32();
                    SattSignalQZSS = (int)Genba_S.OPA.SattSignalQZSS;

                    //---------------------------------------------
                    //Get #nFile, , SattSignalGalileo
                    Genba_S.OPA.SattSignalGalileo = br.ReadInt32();
                    SattSignalGalileo = (int)Genba_S.OPA.SattSignalGalileo;

                    //---------------------------------------------
                    //Get #nFile, , SattSignalBeiDou
                    Genba_S.OPA.SattSignalBeiDou = br.ReadInt32();
                    SattSignalBeiDou = (int)Genba_S.OPA.SattSignalBeiDou;
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
                    //"C:\Develop\NetSurv\Src\NS-App\NS-Survey\Temp\.\ObsPoint\017H305.log"
                    satelliteInfoReader.OpenFile($"{App_Path}{TEMPORARY_PATH}.{ OBSPOINT_PATH}{ FileTitle}.RNX_SV_EXTENSION");
                    //-----------------------------
                    //<<< 衛星信号の読み込み。 >>>
                    satelliteInfoReader.ReadSattSignal(nSattSignalGPS, nSattSignalGLONASS, nSattSignalQZSS, nSattSignalGalileo, nSattSignalBeiDou);
                    //-----------------------------


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
                    SattSignalGPS = (int)Genba_S.OPA.SattSignalGPS;

                    if (Genba_S.OPA.GlonassFlag)
                    {
                        Genba_S.OPA.SattSignalGLONASS = nSattSignalGLONASS;
                        SattSignalGLONASS = (int)Genba_S.OPA.SattSignalGLONASS;
                    }
                    else
                    {
                        Genba_S.OPA.SattSignalGLONASS = 0;                  //0
                        SattSignalGLONASS = (int)Genba_S.OPA.SattSignalGLONASS;                  //0
                    }
                    if (Genba_S.OPA.QZSSFlag)
                    {
                        Genba_S.OPA.SattSignalQZSS = nSattSignalQZSS;
                        SattSignalQZSS = (int)Genba_S.OPA.SattSignalQZSS;
                    }
                    else
                    {
                        Genba_S.OPA.SattSignalQZSS = 0;                     //0
                        SattSignalQZSS = (int)Genba_S.OPA.SattSignalQZSS;                     //0
                    }
                    if (Genba_S.OPA.GalileoFlag)
                    {
                        Genba_S.OPA.SattSignalGalileo = nSattSignalGalileo;
                        SattSignalGalileo = (int)Genba_S.OPA.SattSignalGalileo;
                    }
                    else
                    {
                        Genba_S.OPA.SattSignalGalileo = 0;                  //0
                        SattSignalGalileo = (int)Genba_S.OPA.SattSignalGalileo;                  //0
                    }
                    if (Genba_S.OPA.BeiDouFlag)
                    {
                        Genba_S.OPA.SattSignalBeiDou = nSattSignalBeiDou;
                        SattSignalBeiDou = (int)Genba_S.OPA.SattSignalBeiDou;
                    }
                    else
                    {
                        Genba_S.OPA.SattSignalBeiDou = 0;                   //0
                        SattSignalBeiDou = (int)Genba_S.OPA.SattSignalBeiDou;                   //0
                    }


                } //End If '2023/07/26 Hitz H.Nakamura 偏心点など衛星情報がない場合に対応できていなかった。
                //'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            }
            else
            {
                //---------------------------------------------
                //Get #nFile, , SattSignalGPS
                Genba_S.OPA.SattSignalGPS = br.ReadInt32();
                SattSignalGPS = (int)Genba_S.OPA.SattSignalGPS;

                //---------------------------------------------
                //Get #nFile, , SattSignalGLONASS
                Genba_S.OPA.SattSignalGLONASS = br.ReadInt32();
                SattSignalGLONASS = (int)Genba_S.OPA.SattSignalGLONASS;

                //---------------------------------------------
                //Get #nFile, , SattSignalQZSS
                Genba_S.OPA.SattSignalQZSS = br.ReadInt32();
                SattSignalQZSS = (int)Genba_S.OPA.SattSignalQZSS;

                //---------------------------------------------
                //Get #nFile, , SattSignalGalileo
                Genba_S.OPA.SattSignalGalileo = br.ReadInt32();
                SattSignalGalileo = (int)Genba_S.OPA.SattSignalGalileo;

                //---------------------------------------------
                //Get #nFile, , SattSignalBeiDou
                Genba_S.OPA.SattSignalBeiDou = br.ReadInt32();
                SattSignalBeiDou = (int)Genba_S.OPA.SattSignalBeiDou;

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
