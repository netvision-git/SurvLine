using SurvLine.mdl;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static SurvLine.mdl.DEFINE;
using static SurvLine.mdl.MdlNSDefine;
using static SurvLine.mdl.MdlUtility;
using static SurvLine.mdl.MdlAccountMake;
using static SurvLine.mdl.MdlNSGUI;
using static SurvLine.mdl.MdlNSSDefine;
using static SurvLine.mdl.MdiVBfunctions;
using static System.Net.WebRequestMethods;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace SurvLine
{
    public class ObservationCommonAttributes
    {

        //EccentricCorrectionParam eccentricCorrectionParam = new EccentricCorrectionParam();
        //CoordinatePointFix coordinatePointFix = new CoordinatePointFix();
        //CoordinatePointXYZ coordinatePointXYZ = new CoordinatePointXYZ();

        public ObservationCommonAttributes(MdlMain mdlMain)
        {
            this.mdlMain = mdlMain;
            m_clsEccentricCorrectionParam = new EccentricCorrectionParam(mdlMain);
        }

        private MdlMain mdlMain;



        //==========================================================================================
        /*[VB]
        '*******************************************************************************
        '観測点共通属性

        Option Explicit
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'プロパティ
        Public Number As String '番号。
        Public Name As String '名称。
        Public GenuineNumber As String '本点番号。
        Public GenuineName As String '本点名称。
        Public ObjectType As Long 'オブジェクト種別。
        Public OldEditCode As EDITCODE_STYLE '旧編集コード。
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'プロパティ
        public string Number;               //'番号。
        public string Name;                 //'名称。
        public string GenuineNumber;        //'本点番号。
        public string GenuineName;          //'本点名称。
        public long ObjectType;             //'オブジェクト種別。
        public EDITCODE_STYLE OldEditCode;  //'旧編集コード。
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'インプリメンテーション
        Private m_clsCoordinateFixed As New CoordinatePointFix '固定座標。
        Private m_clsPlanePoint As New TwipsPoint '平面直角座標(ｍ)。
        Private m_clsDevicePoint As New TwipsPoint 'デバイス座標(ピクセル)。
        Private m_clsEccentricCorrectionParam As New EccentricCorrectionParam '偏心補正パラメータ。
        Private m_clsVectorEccentric As New CoordinatePointXYZ '偏心ベクトル。
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'インプリメンテーション
        private CoordinatePointFix m_clsCoordinateFixed = new CoordinatePointFix();                         //'固定座標。
        private TwipsPoint m_clsPlanePoint = new TwipsPoint();                                              //'平面直角座標(ｍ)。
        private TwipsPoint m_clsDevicePoint = new TwipsPoint();                                             //'デバイス座標(ピクセル)。
        //private EccentricCorrectionParam m_clsEccentricCorrectionParam = new EccentricCorrectionParam();    //'偏心補正パラメータ。
        private EccentricCorrectionParam m_clsEccentricCorrectionParam;                                     //'偏心補正パラメータ。
        private CoordinatePointXYZ m_clsVectorEccentric = new CoordinatePointXYZ();                         //'偏心ベクトル。
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '*******************************************************************************
        'プロパティ

        '固定座標。
        Property Let CoordinateFixed(ByVal clsCoordinateFixed As CoordinatePointFix)
            Let m_clsCoordinateFixed = clsCoordinateFixed
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '*******************************************************************************
        'プロパティ

        '固定座標。
        */
        public void CoordinateFixed(CoordinatePointFix clsCoordinateFixed)
        {
            m_clsCoordinateFixed = clsCoordinateFixed;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '固定座標。
        Property Get CoordinateFixed() As CoordinatePointFix
            Set CoordinateFixed = m_clsCoordinateFixed
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'固定座標。
        public CoordinatePointFix CoordinateFixed()
        {
            return m_clsCoordinateFixed;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '平面直角座標(ｍ)。
        Property Let PlanePoint(ByVal clsPlanePoint As TwipsPoint)
            Let m_clsPlanePoint = clsPlanePoint
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'平面直角座標(ｍ)。
        public void PlanePoint(TwipsPoint clsPlanePoint)
        {
            m_clsPlanePoint = clsPlanePoint;
            return;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '平面直角座標(ｍ)。
        Property Get PlanePoint() As TwipsPoint
            Set PlanePoint = m_clsPlanePoint
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'平面直角座標(ｍ)。
        public TwipsPoint PlanePoint()
        {
            return m_clsPlanePoint;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'デバイス座標(ピクセル)。
        Property Let DevicePoint(ByVal clsDevicePoint As TwipsPoint)
            Let m_clsDevicePoint = clsDevicePoint
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'デバイス座標(ピクセル)。
        public void DevicePoint(TwipsPoint clsDevicePoint)
        {
            m_clsDevicePoint = clsDevicePoint;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'デバイス座標(ピクセル)。
        Property Get DevicePoint() As TwipsPoint
            Set DevicePoint = m_clsDevicePoint
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'デバイス座標(ピクセル)。
        public TwipsPoint DevicePoint()
        {
            return m_clsDevicePoint;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '偏心補正パラメータ。
        Property Let EccentricCorrectionParam(ByVal clsEccentricCorrectionParam As EccentricCorrectionParam)
            Let m_clsEccentricCorrectionParam = clsEccentricCorrectionParam
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'偏心補正パラメータ。
        public void EccentricCorrectionParam(EccentricCorrectionParam clsEccentricCorrectionParam)
        {
            m_clsEccentricCorrectionParam = clsEccentricCorrectionParam;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '偏心補正パラメータ。
        Property Get EccentricCorrectionParam() As EccentricCorrectionParam
            Set EccentricCorrectionParam = m_clsEccentricCorrectionParam
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'偏心補正パラメータ。
        public EccentricCorrectionParam EccentricCorrectionParam()
        {
            return m_clsEccentricCorrectionParam;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '偏心ベクトル。
        Property Let VectorEccentric(ByVal clsVectorEccentric As CoordinatePoint)
            Let m_clsVectorEccentric = clsVectorEccentric
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'偏心ベクトル。
        public void VectorEccentric(CoordinatePoint clsVectorEccentric)
        {
            m_clsVectorEccentric = (CoordinatePointXYZ)clsVectorEccentric;
            return;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '偏心ベクトル。
        Property Get VectorEccentric() As CoordinatePoint
            Set VectorEccentric = m_clsVectorEccentric
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'偏心ベクトル。
        public CoordinatePoint VectorEccentric()
        {
            return m_clsVectorEccentric;
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
            Call PutString(nFile, Number)
            Call PutString(nFile, Name)
            Call PutString(nFile, GenuineNumber)
            Call PutString(nFile, GenuineName)
            Put #nFile, , ObjectType
            Call m_clsCoordinateFixed.Save(nFile)
            Call m_clsEccentricCorrectionParam.Save(nFile)
            Call m_clsVectorEccentric.Save(nFile)
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
        public void Save(int nFile)
        {
            /*
             * 
             * 
             * 
             * 
            Call PutString(nFile, Number)
            Call PutString(nFile, Name)
            Call PutString(nFile, GenuineNumber)
            Call PutString(nFile, GenuineName)
            Put #nFile, , ObjectType;
            Call m_clsCoordinateFixed.Save(nFile)
            Call m_clsEccentricCorrectionParam.Save(nFile)
            Call m_clsVectorEccentric.Save(nFile)
            */
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
        public void Load(BinaryReader br, long nVersion, ref GENBA_STRUCT_S Genba_S)
        {
            //-----------------------------------------------------------------------
            //[VB]  Number = GetString(nFile)   public string Number;               //As String '番号。    "305"
            //(del)     Genba_S.Number = mdiUtility.FileRead_GetString(br); //23/12/20 K.Setoguchi
            Genba_S.OCA.Number = FileRead_GetString(br);         //23/12/20 K.Setoguchi
            Number = Genba_S.OCA.Number;

            //-----------------------------------------------------------------------
            //[VB]  Name = GetString(nFile)                                         "明地"
            //(del)     Genba_S.Name = mdiUtility.FileRead_GetString(br);   //23/12/20 K.Setoguchi
            Genba_S.OCA.Name = FileRead_GetString(br);           //23/12/20 K.Setoguchi
            Name = Genba_S.OCA.Name;

            //-----------------------------------------------------------------------
            if (nVersion < 5600)
            {
                //(del)     Genba_S.GenuineNumber = Genba_S.Number;     //23/12/20 K.Setoguchi
                //(del)     Genba_S.GenuineName = Genba_S.Name;         //23/12/20 K.Setoguchi
                Genba_S.OCA.GenuineNumber = Genba_S.OCA.Number;         //23/12/20 K.Setoguchi
                Genba_S.OCA.GenuineName = Genba_S.OCA.Name;             //23/12/20 K.Setoguchi
                GenuineNumber = Number;
                GenuineName = Name;
            }
            else
            {
                //-----------------------------------------
                //[VB]  GenuineNumber = GetString(nFile)
                //(del)     Genba_S.GenuineNumber = mdiUtility.FileRead_GetString(br);  //23/12/20 K.Setoguchi
                Genba_S.OCA.GenuineNumber = FileRead_GetString(br);          //23/12/20 K.Setoguchi
                GenuineNumber = Genba_S.OCA.GenuineNumber;

                //-----------------------------------------
                //[VB]  GenuineName = GetString(nFile)

                //(del)     Genba_S.Number = mdiUtility.FileRead_GetString(br);         //23/12/20 K.Setoguchi
                Genba_S.OCA.GenuineNumber = FileRead_GetString(br);           //23/12/20 K.Setoguchi
                GenuineName = Genba_S.OCA.GenuineNumber;

            }
            //-----------------------------------------------------------------------
            //[VB]  Get #nFile, , ObjectType
            //(del) Genba_S.ObjectType = br.ReadInt32();            //23/12/20 K.Setoguchi
            Genba_S.OCA.ObjectType = br.ReadInt32();                //23/12/20 K.Setoguchi
            ObjectType = Genba_S.OCA.ObjectType;

            if (nVersion < 6200)
            {
                //[VB]  Get #nFile, , OldEditCode
                //(del)     Genba_S.OldEditCode = br.ReadInt32();   //23/12/20 K.Setoguchi
                Genba_S.OCA.OldEditCode = br.ReadInt32();           //23/12/20 K.Setoguchi
                OldEditCode = (EDITCODE_STYLE)Genba_S.OCA.OldEditCode;
            }
            else
            {
                //(del)     Genba_S.OldEditCode = 0;                //23/12/20 K.Setoguchi
                Genba_S.OCA.OldEditCode = 0;                        //23/12/20 K.Setoguchi
                OldEditCode = 0;
            }

            //------------------------------------
            //[VB]  Call m_clsCoordinateFixed.Load(nFile, nVersion)             OK
            //coordinatePointFix.Load(br, nVersion, ref Genba_S);
            m_clsCoordinateFixed.Load(br, nVersion, ref Genba_S);

            if (nVersion >= 2100)
            {
                //------------------------------------
                //[VB] Call m_clsEccentricCorrectionParam.Load(nFile, nVersion)     OK
                //eccentricCorrectionParam.Load(br, nVersion, ref Genba_S);
                m_clsEccentricCorrectionParam.Load(br, nVersion, ref Genba_S);
                //------------------------------------
                //[VB] Call m_clsVectorEccentric.Load(nFile, nVersion)              OK
                //coordinatePointXYZ.Load(br, nVersion, ref Genba_S);
                m_clsVectorEccentric.Load(br, nVersion, ref Genba_S);
                //------------------------------------
            }
        }
        //---------------------------------------------------------------------
        //'読み込み。
        //'
        //'引き数：
        //'nFile ファイル番号。
        //'nVersion ファイルバージョン。
        //Public Sub Load(ByVal nFile As Integer, ByVal nVersion As Long)
        //    Number = GetString(nFile)
        //    Name = GetString(nFile)
        //    If nVersion< 5600 Then
        //        GenuineNumber = Number
        //        GenuineName = Name
        //    Else
        //        GenuineNumber = GetString(nFile)
        //        GenuineName = GetString(nFile)
        //    End If
        //    Get #nFile, , ObjectType
        //    If nVersion< 6200 Then
        //        Get #nFile, , OldEditCode
        //    Else
        //        OldEditCode = 0
        //    End If
        //    Call m_clsCoordinateFixed.Load(nFile, nVersion)
        //    If nVersion >= 2100 Then
        //        Call m_clsEccentricCorrectionParam.Load(nFile, nVersion)
        //        Call m_clsVectorEccentric.Load(nFile, nVersion)
        //    End If
        //End Sub
        //***************************************************************************


    }
}
