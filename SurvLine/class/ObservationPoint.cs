using SurvLine.mdl;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static SurvLine.mdl.DEFINE;
using static SurvLine.mdl.MdlNSDefine;
using static SurvLine.mdl.MdlNSSDefine;
using static SurvLine.mdl.mdlEccentricCorrection;
using static SurvLine.mdl.MdlUtility;
using static SurvLine.mdl.MdlNSUtility;
using static SurvLine.mdl.MdlNSSUtility;
using static SurvLine.mdl.MdlRINEXTYPE;
using static SurvLine.mdl.MdlBaseLineAnalyser;
using static System.Collections.Specialized.BitVector32;
using static System.Net.Mime.MediaTypeNames;
using static System.Net.WebRequestMethods;
using static System.Windows.Forms.AxHost;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using System.Security.Policy;

namespace SurvLine
{
    public class ObservationPoint : ChainList
    {

        ////Document document = new Document();
        public ObservationPoint(MdlMain mdlMain)
        {
            this.mdlMain = mdlMain;
            document = mdlMain.GetDocument();
            m_clsAttributes = new ObservationPointAttributes(mdlMain);  //'観測点属性。
            Class_Initialize();
        }
#if true   //K.S 0304
        public ObservationPoint()
        {
            this.mdlMain = mdlMain;
            document = mdlMain.GetDocument();
            m_clsAttributes = new ObservationPointAttributes(mdlMain);  //'観測点属性。
            Class_Initialize();
        }

#endif

        //==========================================================================================
        /*[VB]
        '*******************************************************************************
        '観測点

        Option Explicit
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'プロパティ
        Public WorkKey As Long '汎用作業キー。
        Public WorkObject As Object '汎用作業オブジェクト。
        Public ObjectType As Long 'オブジェクト種別。
        Public Owner As Object '所有者。接合観測点の場合、基線ベクトルが保持される。代表観測点の場合、チェーンリストが保持される。
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'プロパティ
        public long WorkKey;            //'汎用作業キー。
        public object WorkObject;       //'汎用作業オブジェクト。
        public long ObjectType;         //'オブジェクト種別。
        public object Owner;            //'所有者。接合観測点の場合、基線ベクトルが保持される。代表観測点の場合、チェーンリストが保持される。
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'インプリメンテーション
        Private m_clsAttributes As New ObservationPointAttributes '観測点属性。
        Private m_clsObservationShared As ObservationShared '観測共有情報。
        Private m_clsParentPoint As ObservationPoint '親観測点。
        Private m_clsChildPoint As ObservationPoint '子観測点。
        Private m_clsPrevPoint As ObservationPoint '兄観測点。
        Private m_clsNextPoint As ObservationPoint '弟観測点。
        Private m_clsCorrectPoint As ObservationPoint '補正点。自オブジェクトが本点の場合は偏心点(HeadPoint)が設定される。自オブジェクトが偏心点の場合は本点(実観測点)が設定される。※相互参照なので Attributes や CommonAttributes のメンバにしてはいけない。
        Private m_bEnable As Boolean '有効フラグ。True=有効。False=無効。
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'インプリメンテーション
        //private ObservationPointAttributes m_clsAttributes = new ObservationPointAttributes();  //'観測点属性。
        private ObservationPointAttributes m_clsAttributes;                                     //'観測点属性。
        private ObservationShared m_clsObservationShared;                                       //'観測共有情報。
        private ObservationPoint m_clsParentPoint;                                              //'親観測点。
        private ObservationPoint m_clsChildPoint;                                               //'子観測点。
        private ObservationPoint m_clsPrevPoint;                                                //'兄観測点。
        private ObservationPoint m_clsNextPoint;                                                //'弟観測点。
        private ObservationPoint m_clsCorrectPoint;                                             //'補正点。自オブジェクトが本点の場合は偏心点(HeadPoint)が設定される。自オブジェクトが偏心点の場合は本点(実観測点)が設定される。※相互参照なので Attributes や CommonAttributes のメンバにしてはいけない。
        private bool m_bEnable;                                                                 //'有効フラグ。True=有効。False=無効。
        private Document document;
        MdlMain mdlMain;
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '*******************************************************************************
        'プロパティ

        '観測点番号。
        Property Let Number(ByVal sNumber As String)
            m_clsAttributes.Common.Number = sNumber
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '*******************************************************************************
        'プロパティ

        '観測点番号。
        */
        public void Number(string sNumber)
        {
            m_clsAttributes.Common().Number = sNumber;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '観測点番号。
        Property Get Number() As String
            Number = m_clsAttributes.Common.Number
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'観測点番号。
        public string Number()
        {
            string w_Number = m_clsAttributes.Common().Number;
            return w_Number;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '観測点番号。
        Property Get DispNumber() As String
            If Genuine Then
                DispNumber = m_clsCorrectPoint.Attributes.Common.GenuineNumber
            Else
                DispNumber = m_clsAttributes.Common.Number
            End If
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'観測点番号。
        public string DispNumber()
        {
            string w_DispNumber;
            if (Genuine())
            {
                w_DispNumber = m_clsCorrectPoint.Attributes().Common().GenuineNumber;
            }
            else
            {
                w_DispNumber = m_clsAttributes.Common().Number;
            }
            return w_DispNumber;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '出力観測点番号。
        Property Get OutputNumber() As String
            If Genuine Then
                OutputNumber = m_clsCorrectPoint.Attributes.Common.GenuineNumber
            ElseIf Eccentric Then
                OutputNumber = m_clsAttributes.Common.GenuineNumber
            Else
                OutputNumber = m_clsAttributes.Common.Number
            End If
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'出力観測点番号。
        public string OutputNumber()
        {
            if (Genuine())
            {
                return m_clsCorrectPoint.Attributes().Common().GenuineNumber;
            }
            else if (Eccentric())
            {
                return m_clsAttributes.Common().GenuineNumber;
            }
            else
            {
                return m_clsAttributes.Common().Number;
            }
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '本点番号。
        Property Get GenuineNumber() As String
            If Genuine Then
                GenuineNumber = m_clsCorrectPoint.Attributes.Common.GenuineNumber
            ElseIf Eccentric Then
                GenuineNumber = m_clsAttributes.Common.GenuineNumber
            Else
                GenuineNumber = m_clsAttributes.Common.Number
            End If
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'本点番号。
        public string GenuineNumber()
        {
            string w_GenuineNumber;
            if (Genuine())
            {
                w_GenuineNumber = m_clsCorrectPoint.Attributes().Common().GenuineNumber;
            }
            else if (Eccentric())
            {
                w_GenuineNumber = m_clsAttributes.Common().GenuineNumber;
            }
            else
            {
                w_GenuineNumber = m_clsAttributes.Common().Number;
            }
            return w_GenuineNumber;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '観測点名称。
        Property Let Name(ByVal sName As String)
            m_clsAttributes.Common.Name = sName
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'観測点名称。
        public void Name(string sName)
        {
            m_clsAttributes.Common().Name = sName;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '観測点名称。
        Property Get Name() As String
            If Genuine Then
                Name = m_clsCorrectPoint.Attributes.Common.GenuineName
            Else
                Name = m_clsAttributes.Common.Name
            End If
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'観測点名称。
        public string Name()
        {
            string w_Name;
            if (Genuine())
            {
                w_Name = m_clsCorrectPoint.Attributes().Common().GenuineName;
            }
            else
            {
                w_Name = m_clsAttributes.Common().Name;
            }
            return w_Name;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '出力観測点名称。
        Property Get OutputName() As String
            If Genuine Then
                OutputName = m_clsCorrectPoint.Attributes.Common.GenuineName
            ElseIf Eccentric Then
                OutputName = m_clsAttributes.Common.GenuineName
            Else
                OutputName = m_clsAttributes.Common.Name
            End If
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'出力観測点名称。
        public string OutputName()
        {
            if (Genuine())
            {
                return m_clsCorrectPoint.Attributes().Common().GenuineName;
            }
            else if (Eccentric())
            {
                return m_clsAttributes.Common().GenuineName;
            }
            else
            {
                return m_clsAttributes.Common().Name;
            }
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '本点名称。
        Property Get GenuineName() As String
            If Genuine Then
                GenuineName = m_clsCorrectPoint.Attributes.Common.GenuineName
            ElseIf Eccentric Then
                GenuineName = m_clsAttributes.Common.GenuineName
            Else
                GenuineName = m_clsAttributes.Common.Name
            End If
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'本点名称。
        public string GenuineName()
        {
            string w_GenuineName;
            if (Genuine())
            {
                w_GenuineName = m_clsCorrectPoint.Attributes().Common().GenuineName;
            }
            else if (Eccentric())
            {
                w_GenuineName = m_clsAttributes.Common().GenuineName;
            }
            else
            {
                w_GenuineName = m_clsAttributes.Common().Name;
            }
            return w_GenuineName;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '固定座標。
        Property Let CoordinateFixed(ByVal clsCoordinateFixed As CoordinatePointFix)
            Let m_clsAttributes.Common.CoordinateFixed = clsCoordinateFixed
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'固定座標。
        public void CoordinateFixed(CoordinatePointFix clsCoordinateFixed)
        {
            m_clsAttributes.Common().CoordinateFixed(clsCoordinateFixed);
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '固定座標。
        Property Get CoordinateFixed() As CoordinatePointFix
            Set CoordinateFixed = m_clsAttributes.Common.CoordinateFixed
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'固定座標。
        public CoordinatePointFix CoordinateFixed()
        {
            return m_clsAttributes.Common().CoordinateFixed();
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '代表座標。
        Property Get CoordinateRepresent() As CoordinatePoint
            If Fixed Then
                Set CoordinateRepresent = m_clsAttributes.Common.CoordinateFixed
            Else
                Set CoordinateRepresent = m_clsAttributes.CoordinateObservation
            End If
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'代表座標。
        public CoordinatePoint CoordinateRepresent()
        {
            if (Fixed())
            {
                return m_clsAttributes.Common().CoordinateFixed();
            }
            else
            {
                return m_clsAttributes.CoordinateObservation();
            }
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '参照座標。
        '2006/10/5 NGS Yamada
        '解析の始点座標が「固定点＆固定点を偏心＆角度を方位角」の場合、固定座標に偏心補正量を引いたものにする。
        '→始点が固定点の場合、固定点が偏心 &&（方位標を手入力 || 角度が方位角)の場合に変更。2007/7/28 NGS Yamada
        Property Get CoordinateAnalysisStr() As CoordinatePoint
            If Fixed And Eccentric And (m_clsAttributes.Common.EccentricCorrectionParam.AngleType = ANGLETYPE_MARK Or m_clsAttributes.Common.EccentricCorrectionParam.AngleType = ANGLETYPE_DIRECTION) Then
                Dim clsCoordinatePoint As New CoordinatePointXYZ
                clsCoordinatePoint.X = m_clsAttributes.Common.CoordinateFixed.RoundX - m_clsAttributes.Common.VectorEccentric.RoundX
                clsCoordinatePoint.Y = m_clsAttributes.Common.CoordinateFixed.RoundY - m_clsAttributes.Common.VectorEccentric.RoundY
                clsCoordinatePoint.Z = m_clsAttributes.Common.CoordinateFixed.RoundZ - m_clsAttributes.Common.VectorEccentric.RoundZ
                Set CoordinateAnalysisStr = clsCoordinatePoint
                '固定点＆固定点を偏心＆角度を方位角の場合はこちら。
        '        Set CoordinateAnalysisStr = ?????
        '        m_clsAttributes.Common.CoordinateFixed （固定座標）と
        '        m_clsAttributes.Common.VectorEccentric （偏心補正量）を足したもの（引いたもの？）。
            Else
                '通常の解析始点。
                Set CoordinateAnalysisStr = CoordinateReference
            End If
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '参照座標。
        '2006/10/5 NGS Yamada
        '解析の始点座標が「固定点＆固定点を偏心＆角度を方位角」の場合、固定座標に偏心補正量を引いたものにする。
        '→始点が固定点の場合、固定点が偏心 &&（方位標を手入力 || 角度が方位角)の場合に変更。2007/7/28 NGS Yamada
        */
        public CoordinatePoint CoordinateAnalysisStr()
        {
            if (Fixed() && Eccentric() &&
                (m_clsAttributes.Common().EccentricCorrectionParam().AngleType == (int)ANGLE_TYPE.ANGLETYPE_MARK
                    || m_clsAttributes.Common().EccentricCorrectionParam().AngleType == (int)ANGLE_TYPE.ANGLETYPE_DIRECTION))
            {
                CoordinatePointXYZ clsCoordinatePoint = new CoordinatePointXYZ();
                clsCoordinatePoint.X = m_clsAttributes.Common().CoordinateFixed().RoundXX() - m_clsAttributes.Common().VectorEccentric().RoundXX();
                clsCoordinatePoint.Y = m_clsAttributes.Common().CoordinateFixed().RoundYY() - m_clsAttributes.Common().VectorEccentric().RoundYY();
                clsCoordinatePoint.Z = m_clsAttributes.Common().CoordinateFixed().RoundZZ() - m_clsAttributes.Common().VectorEccentric().RoundZZ();
                return clsCoordinatePoint;
                //'固定点＆固定点を偏心＆角度を方位角の場合はこちら。
                //'        Set CoordinateAnalysisStr = ?????
                //'        m_clsAttributes.Common.CoordinateFixed （固定座標）と
                //'        m_clsAttributes.Common.VectorEccentric （偏心補正量）を足したもの（引いたもの？）。
            }
            else
            {
                //'通常の解析始点。
                return CoordinateReference();
            }
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '参照座標。
        Property Get CoordinateReference() As CoordinatePoint
            If Fixed Then
                Set CoordinateReference = m_clsAttributes.Common.CoordinateFixed
            Else
                Dim objOwner As BaseLineVector
                Dim nResult As Long
                nResult = 0


                Dim clsObservationPoint As ObservationPoint
                Set clsObservationPoint = HeadPoint
                Do While Not clsObservationPoint Is Nothing
                    Dim objOwnerTmp As BaseLineVector
                    Dim nResultTmp As Long
                    nResultTmp = clsObservationPoint.GetCoordinateAnalysisEnd(objOwnerTmp, False)
                    If nResult > nResultTmp Then
                        Set objOwner = objOwnerTmp
                        nResult = nResultTmp
                    End If
                    Set clsObservationPoint = clsObservationPoint.NextPoint
                Loop

                If 0 > nResult Then
                    Dim clsCoordinateAnalysis As CoordinatePoint
                    Dim clsVectorAnalysis As CoordinatePoint
                    Set clsCoordinateAnalysis = objOwner.CoordinateAnalysis
                    Set clsVectorAnalysis = objOwner.VectorAnalysis
                    Dim clsCoordinatePoint As New CoordinatePointXYZ
                    clsCoordinatePoint.X = clsCoordinateAnalysis.RoundX + clsVectorAnalysis.RoundX
                    clsCoordinatePoint.Y = clsCoordinateAnalysis.RoundY + clsVectorAnalysis.RoundY
                    clsCoordinatePoint.Z = clsCoordinateAnalysis.RoundZ + clsVectorAnalysis.RoundZ
                    Set CoordinateReference = clsCoordinatePoint
                Else
                    Set CoordinateReference = m_clsAttributes.CoordinateObservation
                End If
            End If
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'参照座標。
        public CoordinatePoint CoordinateReference()
        {
#if false
            if (Fixed())
            {
                return m_clsAttributes.Common().CoordinateFixed();
            }
            else
            {
                BaseLineVector objOwner = null;
                long nResult;
                nResult = 0;


                ObservationPoint clsObservationPoint;
                clsObservationPoint = HeadPoint();
                BaseLineVector objOwnerTmp;
                long nResultTmp;
                while (clsObservationPoint != null)
                {
                    nResultTmp = clsObservationPoint.GetCoordinateAnalysisEnd(ref (object)objOwnerTmp, false);
                    if (nResult > nResultTmp)
                    {
                        objOwner = objOwnerTmp;
                        nResult = nResultTmp;
                    }
                    clsObservationPoint = clsObservationPoint.NextPoint();
                }

                if (0 > nResult)
                {
                    CoordinatePoint clsCoordinateAnalysis;
                    CoordinatePoint clsVectorAnalysis;
                    clsCoordinateAnalysis = objOwner.CoordinateAnalysis();
                    clsVectorAnalysis = objOwner.VectorAnalysis();
                    CoordinatePointXYZ clsCoordinatePoint = new CoordinatePointXYZ();
                    clsCoordinatePoint.X = clsCoordinateAnalysis.RoundXX() + clsVectorAnalysis.RoundXX();
                    clsCoordinatePoint.Y = clsCoordinateAnalysis.RoundYY() + clsVectorAnalysis.RoundYY();
                    clsCoordinatePoint.Z = clsCoordinateAnalysis.RoundZZ() + clsVectorAnalysis.RoundZZ();
                    return clsCoordinatePoint;
                }
                else
                {
                    return m_clsAttributes.CoordinateObservation();
                }
            }
#else
            return null;
#endif
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '閉合差座標。
        Property Get CoordinateAngleDiff() As CoordinatePoint
            Set CoordinateAngleDiff = CoordinateRepresent
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'閉合差座標。
        public CoordinatePoint CoordinateAngleDiff()
        {
            return CoordinateRepresent();
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '平面直角座標(ｍ)。
        Property Let PlanePoint(ByVal clsPlanePoint As TwipsPoint)
            Let m_clsAttributes.Common.PlanePoint = clsPlanePoint
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'平面直角座標(ｍ)。
        public void PlanePoint(TwipsPoint clsPlanePoint)
        {
            m_clsAttributes.Common().PlanePoint(clsPlanePoint);
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '平面直角座標(ｍ)。
        Property Get PlanePoint() As TwipsPoint
            Set PlanePoint = m_clsAttributes.Common.PlanePoint
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'平面直角座標(ｍ)。
        public TwipsPoint PlanePoint()
        {
            return m_clsAttributes.Common().PlanePoint();
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'デバイス座標(ピクセル)。
        Property Let DevicePoint(ByVal clsDevicePoint As TwipsPoint)
            Let m_clsAttributes.Common.DevicePoint = clsDevicePoint
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'デバイス座標(ピクセル)。
        public void DevicePoint(TwipsPoint clsDevicePoint)
        {
            m_clsAttributes.Common().DevicePoint().X = clsDevicePoint.X;
            m_clsAttributes.Common().DevicePoint().Y = clsDevicePoint.Y;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'デバイス座標(ピクセル)。
        Property Get DevicePoint() As TwipsPoint
            Set DevicePoint = m_clsAttributes.Common.DevicePoint
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'デバイス座標(ピクセル)。
        public TwipsPoint DevicePoint()
        {
            return m_clsAttributes.Common().DevicePoint();
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'セッション名。
        Property Let Session(ByVal sSession As String)
            m_clsAttributes.Session = sSession
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'セッション名。
        public void Session(string sSession)
        {
            m_clsAttributes.Session = sSession;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'セッション名。
        Property Get Session() As String
            Session = m_clsAttributes.Session
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'セッション名。
        public string Session()
        {
            return m_clsAttributes.Session;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '仮のセッション。
        Property Let ProvisionalSession(ByVal bProvisionalSession As Boolean)
            m_clsAttributes.ProvisionalSession = bProvisionalSession
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'仮のセッション。
        public void ProvisionalSession(bool bProvisionalSession)
        {
            m_clsAttributes.ProvisionalSession = bProvisionalSession;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '仮のセッション。
        Property Get ProvisionalSession() As Boolean
            ProvisionalSession = m_clsAttributes.ProvisionalSession
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'仮のセッション。
        public bool ProvisionalSession()
        {
            return m_clsAttributes.ProvisionalSession;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'ファイルタイトル。
        Property Let FileTitle(ByVal sFileTitle As String)
            m_clsAttributes.FileTitle = sFileTitle
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'ファイルタイトル。
        public void FileTitle(string sFileTitle)
        {
            m_clsAttributes.FileTitle = sFileTitle;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'ファイルタイトル。
        Property Get FileTitle() As String
            FileTitle = m_clsAttributes.FileTitle
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'ファイルタイトル。
        public string FileTitle()
        {
            return m_clsAttributes.FileTitle;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'RINEXファイル拡張子(先頭２文字)。
        Property Let RinexExt(ByVal sRinexExt As String)
            m_clsAttributes.RinexExt = sRinexExt
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'RINEXファイル拡張子(先頭２文字)。
        public void RinexExt(string sRinexExt)
        {
            m_clsAttributes.RinexExt = sRinexExt;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'RINEXファイル拡張子(先頭２文字)。
        Property Get RinexExt() As String
            RinexExt = m_clsAttributes.RinexExt
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'RINEXファイル拡張子(先頭２文字)。
        public string RinexExt()
        {
            return m_clsAttributes.RinexExt;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'ソースファイルのパス。
        Property Let SrcPath(ByVal sSrcPath As String)
            m_clsAttributes.SrcPath = sSrcPath
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'ソースファイルのパス。
        public void SrcPath(string sSrcPath)
        {
            m_clsAttributes.SrcPath = sSrcPath;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'ソースファイルのパス。
        Property Get SrcPath() As String
            SrcPath = m_clsAttributes.SrcPath
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'ソースファイルのパス。
        public string SrcPath()
        {
            return m_clsAttributes.SrcPath;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'ソースファイルのファイルタイトル。
        Property Get SrcTitle() As String
            Dim sDrive As String
            Dim sDir As String
            Dim sTitle As String
            Dim sExt As String
            Call SplitPath(m_clsAttributes.SrcPath, sDrive, sDir, sTitle, sExt)
            SrcTitle = sTitle
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'ソースファイルのファイルタイトル。
        public string SrcTitle()
        {
            string sDrive = null;
            string sDir = null;
            string sTitle = null;
            string sExt = null;
            SplitPath(m_clsAttributes.SrcPath, ref sDrive, ref sDir, ref sTitle, ref sExt);
            return sTitle;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'RINEXファイルのファイルタイトル。
        Property Get RinexTitle() As String
            RinexTitle = DispNumber & Session
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'RINEXファイルのファイルタイトル。
        public string RinexTitle()
        {
            return DispNumber() + Session();
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '観測開始日時(GPS)。
        Property Get StrTimeGPS() As Date
            StrTimeGPS = m_clsAttributes.StrTimeGPS
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'観測開始日時(GPS)。
        public DateTime StrTimeGPS()
        {
            return m_clsAttributes.StrTimeGPS;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '観測終了日時(GPS)。
        Property Get EndTimeGPS() As Date
            EndTimeGPS = m_clsAttributes.EndTimeGPS
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'観測終了日時(GPS)。
        public DateTime EndTimeGPS()
        {
            return m_clsAttributes.EndTimeGPS;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '観測開始日時(UTC)。
        Property Get StrTimeUTC() As Date
            StrTimeUTC = GetTimeFromGPS(m_clsAttributes.StrTimeGPS, m_clsAttributes.LeapSeconds, TIME_ZONE_UTC)
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'観測開始日時(UTC)。
        public DateTime StrTimeUTC()
        {
            return GetTimeFromGPS(m_clsAttributes.StrTimeGPS, m_clsAttributes.LeapSeconds, TIME_ZONE.TIME_ZONE_UTC);
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '観測終了日時(UTC)。
        Property Get EndTimeUTC() As Date
            EndTimeUTC = GetTimeFromGPS(m_clsAttributes.EndTimeGPS, m_clsAttributes.LeapSeconds, TIME_ZONE_UTC)
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'観測終了日時(UTC)。
        public DateTime EndTimeUTC()
        {
            return GetTimeFromGPS(m_clsAttributes.EndTimeGPS, m_clsAttributes.LeapSeconds, TIME_ZONE.TIME_ZONE_UTC);
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '観測開始日時(JST)。
        Property Get StrTimeJST() As Date
            StrTimeJST = GetTimeFromGPS(m_clsAttributes.StrTimeGPS, m_clsAttributes.LeapSeconds, TIME_ZONE_JST)
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'観測開始日時(JST)。
        public DateTime StrTimeJST()
        {
            return GetTimeFromGPS(m_clsAttributes.StrTimeGPS, m_clsAttributes.LeapSeconds, TIME_ZONE.TIME_ZONE_JST);
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '観測終了日時(JST)。
        Property Get EndTimeJST() As Date
            EndTimeJST = GetTimeFromGPS(m_clsAttributes.EndTimeGPS, m_clsAttributes.LeapSeconds, TIME_ZONE_JST)
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'観測終了日時(JST)。
        public DateTime EndTimeJST()
        {
            return GetTimeFromGPS(m_clsAttributes.EndTimeGPS, m_clsAttributes.LeapSeconds, TIME_ZONE.TIME_ZONE_JST);
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'うるう秒。
        Property Get LeapSeconds() As Long
            If Genuine Then
                LeapSeconds = m_clsCorrectPoint.LeapSeconds
            Else
                LeapSeconds = m_clsAttributes.LeapSeconds
            End If
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'うるう秒。
        public long LeapSeconds()
        {
            if (Genuine())
            {
                return m_clsCorrectPoint.LeapSeconds();
            }
            else
            {
                return m_clsAttributes.LeapSeconds;
            }
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '間隔(秒)。
        Property Let Interval(ByVal nInterval As Double)
            m_clsAttributes.Interval = nInterval
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'間隔(秒)。
        public void Interval(double nInterval)
        {
            m_clsAttributes.Interval = nInterval;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '間隔(秒)。
        Property Get Interval() As Double
            Interval = m_clsAttributes.Interval
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'間隔(秒)。
        public double Interval()
        {
            return m_clsAttributes.Interval;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '受信機名称。
        Property Let RecType(ByVal sRecType As String)
            m_clsAttributes.RecType = sRecType
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'受信機名称。
        public void RecType(string sRecType)
        {
            m_clsAttributes.RecType = sRecType;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '受信機名称。
        Property Get RecType() As String
            RecType = m_clsAttributes.RecType
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'受信機名称。
        public string RecType()
        {
            return m_clsAttributes.RecType;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '受信機表示名称(日本語)。
        Property Get RecTypeDispJ() As String
            RecTypeDispJ = GetRecTypeDispJ(m_clsAttributes.RecType)
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'受信機表示名称(日本語)。
        public string RecTypeDispJ()
        {
            return GetRecTypeDispJ(m_clsAttributes.RecType);
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '受信機表示名称(英語)。
        Property Get RecTypeDispE() As String
            RecTypeDispE = GetRecTypeDispE(m_clsAttributes.RecType)
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'受信機表示名称(英語)。
        public string RecTypeDispE()
        {
            return GetRecTypeDispE(m_clsAttributes.RecType);
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '受信機シリアル。
        Property Let RecNumber(ByVal sRecNumber As String)
            m_clsAttributes.RecNumber = sRecNumber
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'受信機シリアル。
        public void RecNumber(string sRecNumber)
        {
            m_clsAttributes.RecNumber = sRecNumber;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '受信機シリアル。
        Property Get RecNumber() As String
            RecNumber = m_clsAttributes.RecNumber
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'受信機シリアル。
        public string RecNumber()
        {
            return m_clsAttributes.RecNumber;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'アンテナ種別。
        Property Let AntType(ByVal sAntType As String)
            m_clsAttributes.AntType = sAntType
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'アンテナ種別。
        public void AntType(string sAntType)
        {
            m_clsAttributes.AntType = sAntType;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'アンテナ種別。
        Property Get AntType() As String
            AntType = m_clsAttributes.AntType
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'アンテナ種別。
        public string AntType()
        {
            return m_clsAttributes.AntType;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'アンテナ表示名称。
        Property Get AntTypeDisp() As String
            AntTypeDisp = GetPrivateProfileString(m_clsAttributes.AntType, PROFILE_ANT_KEY_TYPE, m_clsAttributes.AntType, App.Path & "\" & PROFILE_ANT_FILE)
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'アンテナ表示名称。
        public string AntTypeDisp()
        {
            return GetPrivateProfileString(m_clsAttributes.AntType, PROFILE_ANT_KEY_TYPE, m_clsAttributes.AntType, AppPath + "\\" + PROFILE_ANT_FILE);
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'アンテナシリアル番号。
        Property Let AntNumber(ByVal sAntNumber As String)
            m_clsAttributes.AntNumber = sAntNumber
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'アンテナシリアル番号。
        public void AntNumber(string sAntNumber)
        {
            m_clsAttributes.AntNumber = sAntNumber;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'アンテナシリアル番号。
        Property Get AntNumber() As String
            AntNumber = m_clsAttributes.AntNumber
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'アンテナシリアル番号。
        public string AntNumber()
        {
            return m_clsAttributes.AntNumber;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'アンテナ測位方法。
        Property Let AntMeasurement(ByVal sAntMeasurement As String)
            m_clsAttributes.AntMeasurement = sAntMeasurement
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'アンテナ測位方法。
        public void AntMeasurement(string sAntMeasurement)
        {
            m_clsAttributes.AntMeasurement = sAntMeasurement;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'アンテナ測位方法。
        Property Get AntMeasurement() As String
            AntMeasurement = m_clsAttributes.AntMeasurement
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'アンテナ測位方法。
        public string AntMeasurement()
        {
            return m_clsAttributes.AntMeasurement;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'アンテナ測位方法表示文字列。
        Property Get DispMeasurement() As String
            If m_clsObservationShared Is Nothing Then
                DispMeasurement = ""
            Else
                DispMeasurement = m_clsObservationShared.DispMeasurement(m_clsAttributes.AntType, m_clsAttributes.AntMeasurement)
            End If
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'アンテナ測位方法表示文字列。
        public string DispMeasurement()
        {
            if (m_clsObservationShared == null)
            {
                return "";
            }
            else
            {
                return m_clsObservationShared.DispMeasurement(m_clsAttributes.AntType, m_clsAttributes.AntMeasurement);
            }
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'アンテナ高(ｍ)。
        Property Let AntHeight(ByVal nAntHeight As Double)
            m_clsAttributes.AntHeight = nAntHeight
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'アンテナ高(ｍ)。
        public void AntHeight(double nAntHeight)
        {
            m_clsAttributes.AntHeight = nAntHeight;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'アンテナ高(ｍ)。
        Property Get AntHeight() As Double
            AntHeight = m_clsAttributes.AntHeight
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'アンテナ高(ｍ)。
        public double AntHeight()
        {
            return m_clsAttributes.AntHeight;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '仰角マスク(度)。
        Property Let ElevationMask(ByVal nElevationMask As Double)
            m_clsAttributes.ElevationMask = nElevationMask
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'仰角マスク(度)。
        public void ElevationMask(double nElevationMask)
        {
            m_clsAttributes.ElevationMask = nElevationMask;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '仰角マスク(度)。
        Property Get ElevationMask() As Double
            ElevationMask = m_clsAttributes.ElevationMask
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'仰角マスク(度)。
        public double ElevationMask()
        {
            return m_clsAttributes.ElevationMask;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '最少衛星数。0の場合は観測データから取得。1～12が固定値。
        Property Let NumberOfMinSV(ByVal nNumberOfMinSV As Long)
            m_clsAttributes.NumberOfMinSV = nNumberOfMinSV
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'最少衛星数。0の場合は観測データから取得。1～12が固定値。
        public void NumberOfMinSV(long nNumberOfMinSV)
        {
            m_clsAttributes.NumberOfMinSV = nNumberOfMinSV;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '最少衛星数。0の場合は観測データから取得。1～12が固定値。
        Property Get NumberOfMinSV() As Long
            NumberOfMinSV = m_clsAttributes.NumberOfMinSV
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'最少衛星数。0の場合は観測データから取得。1～12が固定値。
        public long NumberOfMinSV()
        {
            return m_clsAttributes.NumberOfMinSV;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '衛星情報。
        Property Set SatelliteInfo(ByVal clsSatelliteInfo As Object)
            Set m_clsAttributes.SatelliteInfo = clsSatelliteInfo
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'衛星情報。
        public void SatelliteInfo(object clsSatelliteInfo)
        {
            m_clsAttributes.SatelliteInfo = clsSatelliteInfo;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '衛星情報。
        Property Get SatelliteInfo() As Object
            Set SatelliteInfo = m_clsAttributes.SatelliteInfo
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'衛星情報。
        public object SatelliteInfo()
        {
            return m_clsAttributes.SatelliteInfo;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'インポートファイルの種別。
        Property Let ImportType(ByVal nImportType As IMPORT_TYPE)
            m_clsAttributes.ImportType = nImportType
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'インポートファイルの種別。
        public void ImportType(IMPORT_TYPE nImportType)
        {
            m_clsAttributes.ImportType = nImportType;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'インポートファイルの種別。
        Property Get ImportType() As Long
            ImportType = m_clsAttributes.ImportType
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'インポートファイルの種別。
        public long ImportType()
        {
            return (long)m_clsAttributes.ImportType;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'GLONASSフラグ。
        Property Let GlonassFlag(ByVal bGlonassFlag As Boolean)
            m_clsAttributes.GlonassFlag = bGlonassFlag
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'GLONASSフラグ。
        public void GlonassFlag(bool bGlonassFlag)
        {
            m_clsAttributes.GlonassFlag = bGlonassFlag;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'GLONASSフラグ。
        Property Get GlonassFlag() As Boolean
            GlonassFlag = m_clsAttributes.GlonassFlag
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'GLONASSフラグ。
        public bool GlonassFlag()
        {
            return m_clsAttributes.GlonassFlag;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '2017/06/22 NS6000対応。'''''''''''''''''''''''''''''''''''''''''''''''''''''''
        'QZSSフラグ。
        Property Let QZSSFlag(ByVal bQZSSFlag As Boolean)
            m_clsAttributes.QZSSFlag = bQZSSFlag
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'2017/06/22 NS6000対応。'''''''''''''''''''''''''''''''''''''''''''''''''''''''
        //'QZSSフラグ。
        public void QZSSFlag(bool bQZSSFlag)
        {
            m_clsAttributes.QZSSFlag = bQZSSFlag;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'QZSSフラグ。
        Property Get QZSSFlag() As Boolean
            QZSSFlag = m_clsAttributes.QZSSFlag
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'QZSSフラグ。
        public bool QZSSFlag()
        {
            return m_clsAttributes.QZSSFlag;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'Galileoフラグ。
        Property Let GalileoFlag(ByVal bGalileoFlag As Boolean)
            m_clsAttributes.GalileoFlag = bGalileoFlag
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'Galileoフラグ。
        public void GalileoFlag(bool bGalileoFlag)
        {
            m_clsAttributes.GalileoFlag = bGalileoFlag;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'Galileoフラグ。
        Property Get GalileoFlag() As Boolean
            GalileoFlag = m_clsAttributes.GalileoFlag
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'Galileoフラグ。
        public bool GalileoFlag()
        {
            return m_clsAttributes.GalileoFlag;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'BeiDouフラグ。
        Property Let BeiDouFlag(ByVal bBeiDouFlag As Boolean)
            m_clsAttributes.BeiDouFlag = bBeiDouFlag
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'BeiDouフラグ。
        public void BeiDouFlag(bool bBeiDouFlag)
        {
            m_clsAttributes.BeiDouFlag = bBeiDouFlag;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'BeiDouフラグ。
        Property Get BeiDouFlag() As Boolean
            BeiDouFlag = m_clsAttributes.BeiDouFlag
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'BeiDouフラグ。
        public bool BeiDouFlag()
        {
            return m_clsAttributes.BeiDouFlag;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '混合タイプの暦ファイルか？
        Property Let MixedNav(ByVal bMixedNav As Boolean)
            m_clsAttributes.MixedNav = bMixedNav
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'混合タイプの暦ファイルか？
        public void MixedNav(bool bMixedNav)
        {
            m_clsAttributes.MixedNav = bMixedNav;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '混合タイプの暦ファイルか？
        Property Get MixedNav() As Boolean
            MixedNav = m_clsAttributes.MixedNav
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'混合タイプの暦ファイルか？
        public bool MixedNav()
        {
            return m_clsAttributes.MixedNav;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'RINEXファイルのバージョン。
        Property Let RinexVersion(ByVal nRinexVersion As Long)
            m_clsAttributes.RinexVersion = nRinexVersion
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'RINEXファイルのバージョン。
        public void RinexVersion(long nRinexVersion)
        {
            m_clsAttributes.RinexVersion = nRinexVersion;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'RINEXファイルのバージョン。
        Property Get RinexVersion() As Long
            RinexVersion = m_clsAttributes.RinexVersion
        End Property
        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'RINEXファイルのバージョン。
        public long RinexVersion()
        {
            return m_clsAttributes.RinexVersion;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '2022/02/07 SattSignal の追加。''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        'GPS衛星信号。
        Property Let SattSignalGPS(ByVal nSattSignalGPS As Long)
            m_clsAttributes.SattSignalGPS = nSattSignalGPS
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'2022/02/07 SattSignal の追加。''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        //'GPS衛星信号。
        public void SattSignalGPS(long nSattSignalGPS)
        {
            m_clsAttributes.SattSignalGPS = (int)nSattSignalGPS;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'GPS衛星信号。
        Property Get SattSignalGPS() As Long
            SattSignalGPS = m_clsAttributes.SattSignalGPS
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'GPS衛星信号。
        public long SattSignalGPS()
        {
            return m_clsAttributes.SattSignalGPS;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'GLONASS衛星信号。
        Property Let SattSignalGLONASS(ByVal nSattSignalGLONASS As Long)
            m_clsAttributes.SattSignalGLONASS = nSattSignalGLONASS
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'GLONASS衛星信号。
        public void SattSignalGLONASS(long nSattSignalGLONASS)
        {
            m_clsAttributes.SattSignalGLONASS = (int)nSattSignalGLONASS;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'GLONASS衛星信号。
        Property Get SattSignalGLONASS() As Long
            SattSignalGLONASS = m_clsAttributes.SattSignalGLONASS
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'GLONASS衛星信号。
        public long SattSignalGLONASS()
        {
            return m_clsAttributes.SattSignalGLONASS;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'QZSS衛星信号。
        Property Let SattSignalQZSS(ByVal nSattSignalQZSS As Long)
            m_clsAttributes.SattSignalQZSS = nSattSignalQZSS
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'QZSS衛星信号。
        public void SattSignalQZSS(long nSattSignalQZSS)
        {
            m_clsAttributes.SattSignalQZSS = (int)nSattSignalQZSS;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'QZSS衛星信号。
        Property Get SattSignalQZSS() As Long
            SattSignalQZSS = m_clsAttributes.SattSignalQZSS
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'QZSS衛星信号。
        public long SattSignalQZSS()
        {
            return m_clsAttributes.SattSignalQZSS;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'Galileo衛星信号。
        Property Let SattSignalGalileo(ByVal nSattSignalGalileo As Long)
            m_clsAttributes.SattSignalGalileo = nSattSignalGalileo
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'Galileo衛星信号。
        public void SattSignalGalileo(long nSattSignalGalileo)
        {
            m_clsAttributes.SattSignalGalileo = (int)nSattSignalGalileo;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'Galileo衛星信号。
        Property Get SattSignalGalileo() As Long
            SattSignalGalileo = m_clsAttributes.SattSignalGalileo
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'Galileo衛星信号。
        public long SattSignalGalileo()
        {
            return m_clsAttributes.SattSignalGalileo;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'BeiDou衛星信号。
        Property Let SattSignalBeiDou(ByVal nSattSignalBeiDou As Long)
            m_clsAttributes.SattSignalBeiDou = nSattSignalBeiDou
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'BeiDou衛星信号。
        public void SattSignalBeiDou(long nSattSignalBeiDou)
        {
            m_clsAttributes.SattSignalBeiDou = (int)nSattSignalBeiDou;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'BeiDou衛星信号。
        Property Get SattSignalBeiDou() As Long
            SattSignalBeiDou = m_clsAttributes.SattSignalBeiDou
        End Property
        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'BeiDou衛星信号。
        public long SattSignalBeiDou()
        {
            return m_clsAttributes.SattSignalBeiDou;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '観測座標。
        Property Let CoordinateObservation(ByVal clsCoordinateObservation As CoordinatePoint)
            Let m_clsAttributes.CoordinateObservation = clsCoordinateObservation
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'観測座標。
        public void CoordinateObservation(CoordinatePoint clsCoordinateObservation)
        {
            m_clsAttributes.CoordinateObservation(clsCoordinateObservation);
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '観測座標。
        Property Get CoordinateObservation() As CoordinatePoint
            Set CoordinateObservation = m_clsAttributes.CoordinateObservation
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'観測座標。
        public CoordinatePoint CoordinateObservation()
        {
            return m_clsAttributes.CoordinateObservation();
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '表示座標。
        Property Get CoordinateDisplay() As CoordinatePoint
            If Genuine Then
                Set CoordinateDisplay = New CoordinatePointXYZ
                CoordinateDisplay = m_clsCorrectPoint.CoordinateDisplay
                CoordinateDisplay.X = CoordinateDisplay.RoundX + m_clsCorrectPoint.VectorEccentric.RoundX
                CoordinateDisplay.Y = CoordinateDisplay.RoundY + m_clsCorrectPoint.VectorEccentric.RoundY
                CoordinateDisplay.Z = CoordinateDisplay.RoundZ + m_clsCorrectPoint.VectorEccentric.RoundZ
            Else
                If Fixed Then
                    '固定座標。
                    Set CoordinateDisplay = m_clsAttributes.Common.CoordinateFixed
                Else
                    '平均座標。
                    Dim clsObservationPoint As ObservationPoint
                    Set clsObservationPoint = HeadPoint
                    Set CoordinateDisplay = New CoordinatePointXYZ
                    Dim nCount As Long
                    Do While Not clsObservationPoint Is Nothing
                        CoordinateDisplay.X = CoordinateDisplay.X + clsObservationPoint.ChildPoint.CoordinateObservation.X
                        CoordinateDisplay.Y = CoordinateDisplay.Y + clsObservationPoint.ChildPoint.CoordinateObservation.Y
                        CoordinateDisplay.Z = CoordinateDisplay.Z + clsObservationPoint.ChildPoint.CoordinateObservation.Z
                        nCount = nCount + 1
                        Set clsObservationPoint = clsObservationPoint.NextPoint
                    Loop
                    CoordinateDisplay.X = CoordinateDisplay.X / nCount
                    CoordinateDisplay.Y = CoordinateDisplay.Y / nCount
                    CoordinateDisplay.Z = CoordinateDisplay.Z / nCount
                End If
            End If
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'表示座標。
        public CoordinatePoint CoordinateDisplay()
        {
            if (Genuine())
            {
                CoordinatePointXYZ CoordinateDisplay = new CoordinatePointXYZ();
                CoordinateDisplay.CoordinatePointXYZcpy(m_clsCorrectPoint.CoordinateDisplay());
                CoordinateDisplay.XX(CoordinateDisplay.RoundXX() + Math.Round(m_clsCorrectPoint.VectorEccentric().X));
                CoordinateDisplay.YY(CoordinateDisplay.RoundYY() + Math.Round(m_clsCorrectPoint.VectorEccentric().Y));
                CoordinateDisplay.ZZ(CoordinateDisplay.RoundZZ() + Math.Round(m_clsCorrectPoint.VectorEccentric().Z));
                return CoordinateDisplay;
            }
            else
            {
                if (Fixed())
                {
                    //'固定座標。
                    return m_clsAttributes.Common().CoordinateFixed();
                }
                else
                {
                    //'平均座標。
                    ObservationPoint clsObservationPoint;
                    clsObservationPoint = HeadPoint();
                    CoordinatePointXYZ CoordinateDisplay = new CoordinatePointXYZ();
                    CoordinatePoint clsCoordinatePoint = clsObservationPoint.ChildPoint().CoordinateObservation();
                    long nCount = 0;
                    double XX;
                    double YY;
                    double ZZ;
                    while (clsObservationPoint != null)
                    {
                        XX = clsObservationPoint.ChildPoint().CoordinateObservation().XX();
                        YY = clsObservationPoint.ChildPoint().CoordinateObservation().YY();
                        ZZ = clsObservationPoint.ChildPoint().CoordinateObservation().ZZ();
                        CoordinateDisplay.XX(CoordinateDisplay.XX() + XX);
                        CoordinateDisplay.YY(CoordinateDisplay.YY() + YY);
                        CoordinateDisplay.ZZ(CoordinateDisplay.ZZ() + ZZ);
                        nCount++;
                        clsObservationPoint = clsObservationPoint.NextPoint();
                    }
                    CoordinateDisplay.XX(CoordinateDisplay.XX() / nCount);
                    CoordinateDisplay.YY(CoordinateDisplay.YY() / nCount);
                    CoordinateDisplay.ZZ(CoordinateDisplay.ZZ() / nCount);
                    CoordinateDisplay.X = CoordinateDisplay.XX() / nCount;
                    CoordinateDisplay.Y = CoordinateDisplay.YY() / nCount;
                    CoordinateDisplay.Z = CoordinateDisplay.ZZ() / nCount;
                    clsCoordinatePoint.X = CoordinateDisplay.X;
                    clsCoordinatePoint.Y = CoordinateDisplay.Y;
                    clsCoordinatePoint.Z = CoordinateDisplay.Z;
                    return CoordinateDisplay;
                }
            }
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'DXF座標。
        Property Get CoordinateDXF() As CoordinatePoint
            Set CoordinateDXF = CoordinateDisplay
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'DXF座標。
        public CoordinatePoint CoordinateDXF()
        {
            return CoordinateDisplay();
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '観測点属性。
        Property Set Attributes(ByVal clsAttributes As ObservationPointAttributes)
            Set m_clsAttributes = clsAttributes
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'観測点属性。
        public void Attributes(ObservationPointAttributes clsAttributes)
        {
            m_clsAttributes = clsAttributes;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '観測点属性。
        Property Get Attributes() As ObservationPointAttributes
            Set Attributes = m_clsAttributes
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'観測点属性。
        public ObservationPointAttributes Attributes()
        {
            return m_clsAttributes;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '観測共有情報｡
        Property Set ObservationShared(ByVal clsObservationShared As ObservationShared)
            Set m_clsObservationShared = clsObservationShared
            '子観測点すべてに反映させる。
            Dim clsChildPoint As ObservationPoint
            Set clsChildPoint = m_clsChildPoint
            Do While Not clsChildPoint Is Nothing
                Set clsChildPoint.ObservationShared = clsObservationShared
                Set clsChildPoint = clsChildPoint.NextPoint
            Loop
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'観測共有情報｡
        /// <summary>
        /// 
        /// </summary>
        /// <param name="clsObservationShared"></param>
        public void ObservationShared(ObservationShared clsObservationShared)
        {
            m_clsObservationShared = clsObservationShared;
            //'子観測点すべてに反映させる。
            ObservationPoint clsChildPoint;
            clsChildPoint = m_clsChildPoint;
            while (clsChildPoint != null)
            {
                clsChildPoint.ObservationShared(clsObservationShared);
                clsChildPoint = clsChildPoint.NextPoint();
            }
            return;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '親観測点。
        Property Set ParentPoint(ByVal clsParentPoint As ObservationPoint)
            Set m_clsParentPoint = clsParentPoint
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'親観測点。
        public void ParentPoint(ObservationPoint clsParentPoint)
        {
            m_clsParentPoint = clsParentPoint;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '親観測点。
        Property Get ParentPoint() As ObservationPoint
            Set ParentPoint = m_clsParentPoint
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'親観測点。
        public ObservationPoint ParentPoint()
        {
            return m_clsParentPoint;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '最上位親観測点。
        Property Get TopParentPoint() As ObservationPoint
            If m_clsParentPoint Is Nothing Then
                Set TopParentPoint = Me
            Else
                Set TopParentPoint = m_clsParentPoint.TopParentPoint
            End If
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'最上位親観測点。
        public ObservationPoint TopParentPoint()
        {
            if (m_clsParentPoint == null)
            {
                return this;
            }
            else
            {
                return m_clsParentPoint.TopParentPoint();
            }
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '筆頭観測点。
        Property Get HeadPoint() As ObservationPoint
            Set HeadPoint = TopParentPoint.OldestPoint
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'筆頭観測点。
        public ObservationPoint HeadPoint()
        {
            return TopParentPoint().OldestPoint();
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'ルート観測点。
        Property Get RootPoint() As ObservationPoint
            Set RootPoint = HeadPoint
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'ルート観測点。
        public ObservationPoint RootPoint()
        {
            return HeadPoint();
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '子観測点。
        Property Set ChildPoint(ByVal clsChildPoint As ObservationPoint)
            Set m_clsChildPoint = clsChildPoint
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'子観測点。
        public void ChildPoint(ObservationPoint clsChildPoint)
        {
            m_clsChildPoint = clsChildPoint;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '子観測点。
        Property Get ChildPoint() As ObservationPoint
            Set ChildPoint = m_clsChildPoint
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'子観測点。
        public ObservationPoint ChildPoint()
        {
            return m_clsChildPoint;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '最下位親観測点。
        Property Get BottomChildPoint() As ObservationPoint
            If m_clsChildPoint Is Nothing Then
                Set BottomChildPoint = Me
            Else
                Set BottomChildPoint = m_clsChildPoint.BottomChildPoint
            End If
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'最下位親観測点。
        public ObservationPoint BottomChildPoint()
        {
            if (m_clsChildPoint == null)
            {
                return this;
            }
            else
            {
                return m_clsChildPoint.BottomChildPoint();
            }
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '兄観測点。
        Property Set PrevPoint(ByVal clsPrevPoint As ObservationPoint)
            Set m_clsPrevPoint = clsPrevPoint
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'兄観測点。
        public void PrevPoint(ObservationPoint clsPrevPoint)
        {
            m_clsPrevPoint = clsPrevPoint;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '兄観測点。
        Property Get PrevPoint() As ObservationPoint
            Set PrevPoint = m_clsPrevPoint
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'兄観測点。
        public ObservationPoint PrevPoint()
        {
            return m_clsPrevPoint;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '弟観測点。
        Property Set NextPoint(ByVal clsNextPoint As ObservationPoint)
            Set m_clsNextPoint = clsNextPoint
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'弟観測点。
        public void NextPoint(ObservationPoint clsNextPoint)
        {
            m_clsNextPoint = clsNextPoint;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '弟観測点。
        Property Get NextPoint() As ObservationPoint
            Set NextPoint = m_clsNextPoint
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'弟観測点。
        public ObservationPoint NextPoint()
        {
            return m_clsNextPoint;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '長男観測点。
        Property Get OldestPoint() As ObservationPoint
            Set OldestPoint = Me
            Do While Not OldestPoint.PrevPoint Is Nothing
                Set OldestPoint = OldestPoint.PrevPoint
            Loop
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'長男観測点。
        public ObservationPoint OldestPoint()
        {
            ObservationPoint w_OldestPoint;
            w_OldestPoint = this;
            while (w_OldestPoint.PrevPoint() != null)
            {
                w_OldestPoint = w_OldestPoint.PrevPoint();
            }
            return w_OldestPoint;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '補正点。
        Property Get CorrectPoint() As ObservationPoint
            Set CorrectPoint = m_clsCorrectPoint
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'補正点。
        public ObservationPoint CorrectPoint()
        {
            return m_clsCorrectPoint;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '有効フラグ。
        Property Let Enable(ByVal bEnable As Boolean)
            m_bEnable = bEnable
            '子観測点すべてに反映させる。
            Dim clsChildPoint As ObservationPoint
            Set clsChildPoint = m_clsChildPoint
            Do While Not clsChildPoint Is Nothing
                clsChildPoint.Enable = bEnable
                Set clsChildPoint = clsChildPoint.NextPoint
            Loop
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'有効フラグ。
        public void Enable(bool bEnable)
        {
            m_bEnable = bEnable;
            //'子観測点すべてに反映させる。
            ObservationPoint clsChildPoint;
            clsChildPoint = m_clsChildPoint;
            while (clsChildPoint != null)
            {
                clsChildPoint.Enable(bEnable);
                clsChildPoint = clsChildPoint.NextPoint();
            }
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '有効フラグ。
        Property Get Enable() As Boolean
            If m_clsChildPoint Is Nothing Then
                Enable = m_bEnable
            Else
                '子観測点すべてが無効なら無効とする。
                Enable = True
                Dim clsChildPoint As ObservationPoint
                Set clsChildPoint = m_clsChildPoint
                Do While Not clsChildPoint Is Nothing
                    If clsChildPoint.Enable Then Exit Property
                    Set clsChildPoint = clsChildPoint.NextPoint
                Loop
                Enable = False
            End If
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'有効フラグ。
        public bool Enable()
        {
            if (m_clsChildPoint == null)
            {
                return m_bEnable;
            }
            else
            {
                //'子観測点すべてが無効なら無効とする。
                ObservationPoint clsChildPoint;
                clsChildPoint = m_clsChildPoint;
                while (clsChildPoint != null)
                {
                    if (clsChildPoint.Enable())
                    {
                        return true;
                    }
                    clsChildPoint = clsChildPoint.NextPoint();
                }
                return false;
            }
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '有効フラグ。弟観測点全てが無効なら無効とする。
        Property Get EnableHead() As Boolean
            EnableHead = True
            Dim clsObservationPoint As ObservationPoint
            Set clsObservationPoint = Me
            Do While Not clsObservationPoint Is Nothing
                If clsObservationPoint.Enable Then Exit Property
                Set clsObservationPoint = clsObservationPoint.NextPoint
            Loop
            EnableHead = False
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'有効フラグ。弟観測点全てが無効なら無効とする。
        public bool EnableHead()
        {
            ObservationPoint clsObservationPoint = null;
            clsObservationPoint = this;
            while (clsObservationPoint != null)
            {
                if (clsObservationPoint.Enable())
                {
                    return true;
                }
                clsObservationPoint = clsObservationPoint.NextPoint();
            }
            return false;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '無効化した場合、最上位親観測点が無効になるか？
        Property Get IfDisable() As Boolean
            If m_clsParentPoint Is Nothing Then
                IfDisable = True
            Else
                IfDisable = False
                Dim clsChildPoint As ObservationPoint
                Set clsChildPoint = m_clsParentPoint.ChildPoint
                Do While Not clsChildPoint Is Nothing
                    If clsChildPoint.Enable Then
                        If Not (clsChildPoint Is Me) Then Exit Property
                    End If
                    Set clsChildPoint = clsChildPoint.NextPoint
                Loop
                IfDisable = m_clsParentPoint.IfDisable
            End If
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'無効化した場合、最上位親観測点が無効になるか？
        public bool IfDisable()
        {
            if (m_clsParentPoint == null)
            {
                return true;
            }
            else
            {
                ObservationPoint clsChildPoint;
                clsChildPoint = m_clsParentPoint.ChildPoint();
                while (clsChildPoint != null)
                {
                    if (clsChildPoint.Enable())
                    {
                        if (clsChildPoint != this)
                        {
                            return false;
                        }
                    }
                    clsChildPoint = clsChildPoint.NextPoint();
                }
                return m_clsParentPoint.IfDisable();
            }
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '固定点フラグ。
        Property Let Fixed(ByVal bFixed As Boolean)
            If bFixed Then
                m_clsAttributes.Common.ObjectType = m_clsAttributes.Common.ObjectType Or OBS_TYPE_FIXED
            Else
                m_clsAttributes.Common.ObjectType = m_clsAttributes.Common.ObjectType And (Not OBS_TYPE_FIXED)
            End If
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'固定点フラグ。
        public void Fixed(bool bFixed)
        {
            if (bFixed)
            {
                m_clsAttributes.Common().ObjectType = m_clsAttributes.Common().ObjectType | OBS_TYPE_FIXED;
            }
            else
            {
                m_clsAttributes.Common().ObjectType = m_clsAttributes.Common().ObjectType & (~OBS_TYPE_FIXED);  //反転
            }
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '固定点フラグ。
        Property Get Fixed() As Boolean
            Fixed = ((m_clsAttributes.Common.ObjectType And OBS_TYPE_FIXED) <> 0)
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'固定点フラグ。
        public bool Fixed()
        {
            return (m_clsAttributes.Common().ObjectType & OBS_TYPE_FIXED) != 0;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '基準点フラグ。
        Property Get Based() As Boolean
            Based = False
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'基準点フラグ。
        public bool Based()
        {
            return false;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '観測点キー。
        Property Get Key() As String
            Key = GetObservationPointKey(Number, Session)
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'観測点キー。
        public string Key()
        {
            return GetObservationPointKey(Number(), Session());
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '本点フラグ。
        Property Get Genuine() As Boolean
            Genuine = ((m_clsAttributes.Common.ObjectType And OBS_TYPE_GENUIE) <> 0)
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'本点フラグ。
        public bool Genuine()
        {
            return (m_clsAttributes.Common().ObjectType & OBS_TYPE_GENUIE) != 0;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '偏心補正フラグ。
        Property Get Eccentric() As Boolean
            If Genuine Then
                Eccentric = False
            Else
                Eccentric = Not m_clsCorrectPoint Is Nothing
            End If
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'偏心補正フラグ。
        public bool Eccentric()
        {
            if (Genuine())
            {
                return false;
            }
            else
            {
                return m_clsCorrectPoint != null;
            }
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '偏心補正有効フラグ。
        Property Get EnableEccentric() As Boolean
            If Eccentric Then
                EnableEccentric = m_clsCorrectPoint.Enable
            Else
                EnableEccentric = False
            End If
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'偏心補正有効フラグ。
        public bool EnableEccentric()
        {
            if (Eccentric())
            {
                return m_clsCorrectPoint.Enable();
            }
            else
            {
                return false;
            }
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '偏心補正パラメータ。
        Property Let EccentricCorrectionParam(ByVal clsEccentricCorrectionParam As EccentricCorrectionParam)
            Let m_clsAttributes.Common.EccentricCorrectionParam = clsEccentricCorrectionParam
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'偏心補正パラメータ。
        public void EccentricCorrectionParam(EccentricCorrectionParam clsEccentricCorrectionParam)
        {
            m_clsAttributes.Common().EccentricCorrectionParam(clsEccentricCorrectionParam);
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '偏心補正パラメータ。
        Property Get EccentricCorrectionParam() As EccentricCorrectionParam
            Set EccentricCorrectionParam = m_clsAttributes.Common.EccentricCorrectionParam
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'偏心補正パラメータ。
        public EccentricCorrectionParam EccentricCorrectionParam()
        {
            return m_clsAttributes.Common().EccentricCorrectionParam();
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '偏心ベクトル。
        Property Get VectorEccentric() As CoordinatePoint
            Set VectorEccentric = m_clsAttributes.Common.VectorEccentric
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'偏心ベクトル。
        public CoordinatePoint VectorEccentric()
        {
            return m_clsAttributes.Common().VectorEccentric();
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'DXF観測点番号。
        Property Get DXFNumber() As String
            DXFNumber = DispNumber
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'DXF観測点番号。
        public string DXFNumber()
        {
            return DispNumber();
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'DXFサブテキスト。
        Property Get DXFSubText() As String
        '    If Genuine Then
        '        DXFSubText = "本点"
        '    ElseIf Eccentric Then
        '        DXFSubText = "偏心点"
        '    Else
        '        DXFSubText = ""
        '    End If
            DXFSubText = ""
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'DXFサブテキスト。
        public string DXFSubText()
        {
            /*
            '    If Genuine Then
            '        DXFSubText = "本点"
            '    ElseIf Eccentric Then
            '        DXFSubText = "偏心点"
            '    Else
            '        DXFSubText = ""
            '    End If
            */
            return "";
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'リスト更新必要フラグ。
        Property Let IsList(ByVal bIsList As Boolean)
            m_clsAttributes.IsList = bIsList
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'リスト更新必要フラグ。
        public void IsList(bool bIsList)
        {
            m_clsAttributes.IsList = bIsList;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'リスト更新必要フラグ。
        Property Get IsList() As Boolean
            IsList = m_clsAttributes.IsList
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'リスト更新必要フラグ。
        public bool IsList()
        {
            return m_clsAttributes.IsList;
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
        '観測点モード。
        Property Get Mode() As OBJ_MODE
            Mode = m_clsAttributes.Mode
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'観測点モード。
        public OBJ_MODE Mode()
        {
            return m_clsAttributes.Mode;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '*******************************************************************************
        'イベント

        '初期化。
        Private Sub Class_Initialize()

            On Error GoTo ErrorHandler


            m_bEnable = True
            ObjectType = OBJ_TYPE_OBSERVATIONPOINT Or OBS_TYPE_REAL


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
                m_bEnable = true;
                ObjectType = OBJ_TYPE_OBSERVATIONPOINT | OBS_TYPE_REAL;
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

        '終了。
        Public Sub Terminate()

            Set Owner = Nothing
            Set m_clsObservationShared = Nothing


            Dim clsChildPoint As ObservationPoint
            Dim clsObservationPoint As ObservationPoint
            Set clsChildPoint = m_clsChildPoint
            Do While Not clsChildPoint Is Nothing
                Set clsObservationPoint = clsChildPoint
                Set clsChildPoint = clsChildPoint.NextPoint
                Call clsObservationPoint.Terminate
            Loop


            Set m_clsParentPoint = Nothing
            Set m_clsChildPoint = Nothing
            Set m_clsPrevPoint = Nothing
            Set m_clsNextPoint = Nothing
            Set m_clsCorrectPoint = Nothing


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

            Owner = null;
            m_clsObservationShared = null;


            ObservationPoint clsChildPoint;
            ObservationPoint clsObservationPoint;
            clsChildPoint = m_clsChildPoint;
            while (clsChildPoint != null)
            {
                clsObservationPoint = clsChildPoint;
                clsChildPoint = clsChildPoint.NextPoint();
                clsObservationPoint.Terminate();
            }


            m_clsParentPoint = null;
            m_clsChildPoint = null;
            m_clsPrevPoint = null;
            m_clsNextPoint = null;
            m_clsCorrectPoint = null;


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
            WorkKey = nWorkKey
            '長男の場合は親も初期化する。
            If(m_clsPrevPoint Is Nothing) And(Not m_clsParentPoint Is Nothing) Then Call m_clsParentPoint.ClearWorkKey(nWorkKey)
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
            WorkKey = nWorkKey;
            //'長男の場合は親も初期化する。
            if ((m_clsPrevPoint == null) && (m_clsParentPoint != null))
            {
                m_clsParentPoint.ClearWorkKey(nWorkKey);
            }
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '保存。
        '
        '長男が親観測点の Save を呼ぶ。
        '
        '引き数：
        'nFile ファイル番号。
        'nJointKey 結合キー。
        Public Sub Save(ByVal nFile As Integer, ByRef nJointKey As Long)

            Call m_clsAttributes.Save(nFile)
            Put #nFile, , m_bEnable
            Put #nFile, , ObjectType
    
            '長男フラグ。
            Dim bFirst As Boolean
            bFirst = m_clsPrevPoint Is Nothing
            Put #nFile, , bFirst
            '親フラグ。
            Dim bParent As Boolean
            bParent = Not m_clsParentPoint Is Nothing
            Put #nFile, , bParent
    
            '結合キーがまだ設定されていない場合は設定する。
            If WorkKey< 0 Then
                WorkKey = nJointKey
                nJointKey = nJointKey + 1
            End If
            '弟の結合キーも設定されていなければ設定する。
            Dim nNextJointKey As Long
            If Not m_clsNextPoint Is Nothing Then
                If m_clsNextPoint.WorkKey< 0 Then
                    m_clsNextPoint.WorkKey = nJointKey
                    nJointKey = nJointKey + 1
                End If
                nNextJointKey = m_clsNextPoint.WorkKey
            Else
                nNextJointKey = nJointKey
                nJointKey = nJointKey + 1
            End If
            Put #nFile, , WorkKey 'Prev結合キー。
            Put #nFile, , nNextJointKey 'Next結合キー。
    
            '長男であり親がいる場合は親を保存する。
            If bFirst And bParent Then Call m_clsParentPoint.Save(nFile, nJointKey)

        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '保存。
        '
        '長男が親観測点の Save を呼ぶ。
        '
        '引き数：
        'nFile ファイル番号。
        'nJointKey 結合キー。
        */
        public void Save(int nFile, long nJointKey)
        {
            /*
            Call m_clsAttributes.Save(nFile)
            Put #nFile, , m_bEnable
            Put #nFile, , ObjectType
    
            '長男フラグ。
            Dim bFirst As Boolean
            bFirst = m_clsPrevPoint Is Nothing
            Put #nFile, , bFirst
            '親フラグ。
            Dim bParent As Boolean
            bParent = Not m_clsParentPoint Is Nothing
            Put #nFile, , bParent
    
            '結合キーがまだ設定されていない場合は設定する。
            If WorkKey< 0 Then
                WorkKey = nJointKey
                nJointKey = nJointKey + 1
            End If
            '弟の結合キーも設定されていなければ設定する。
            Dim nNextJointKey As Long
            If Not m_clsNextPoint Is Nothing Then
                If m_clsNextPoint.WorkKey < 0 Then
                    m_clsNextPoint.WorkKey = nJointKey
                    nJointKey = nJointKey + 1
                End If
                nNextJointKey = m_clsNextPoint.WorkKey
            Else
                nNextJointKey = nJointKey
                nJointKey = nJointKey + 1
            End If
            Put #nFile, , WorkKey 'Prev結合キー。
            Put #nFile, , nNextJointKey 'Next結合キー。
    
            '長男であり親がいる場合は親を保存する。
            If bFirst And bParent Then Call m_clsParentPoint.Save(nFile, nJointKey)
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
        //23/12/22 K.Setoguchi---->>>>
        // 旧処理を全削除
        //<<<<----23/12/22 K.Setoguchi
        //---------------------------------------------------------------------------
        //23/12/20 K.setoguchi@NV---------->>>>>>>>>>>
        //Public Sub Load(ByVal nFile As Integer, ByVal nVersion As Long, ByRef clsObservationPoints() As ObservationPoint, ByVal clsChildPoint As ObservationPoint)
        public void Load(BinaryReader br, long nVersion, ref GENBA_STRUCT_S Genba_S, ref List<OPA_STRUCT_SUB> OPA_List,
            ref ObservationPoint[] clsObservationPoints, ObservationPoint clsChildPoint = null)
        //<<<<<<<<<-----------23/12/20 K.setoguchi@NV
        {

            //----------------------------------------------------------------    
            //[VB]  Call m_clsAttributes.Load(nFile, nVersion)
            //ObservationPointAttributes observationPointAttributes = new ObservationPointAttributes();
            //observationPointAttributes.Load(br, nVersion, ref Genba_S);
            m_clsAttributes.Load(br, nVersion, ref Genba_S);


            //----------------------------------------------------------------    
            //[VB]  Get #nFile, , m_bEnable     public bool m_bEnable;          //As Boolean '有効フラグ。True=有効。False=無効。   //true
            Genba_S.m_bEnable = document.GetFileBool(br);
            m_bEnable = Genba_S.m_bEnable;

            //[VB]  Get #nFile, , ObjectType    public long OP_ObjectType;      //As Long 'オブジェクト種別。                        //-2147483520
            Genba_S.OP_ObjectType = br.ReadInt32();
            ObjectType = Genba_S.OP_ObjectType;

            //----------------------------------------------------------------    
            //    '長男フラグ｡                                                           //true
            //    Dim bFirst As Boolean
            //    Get #nFile, , bFirst
            bool bFirst;
            bFirst = document.GetFileBool(br);

            //----------------------------------------------------------------    
            //    '親フラグ。                                                            //true
            //    Dim bParent As Boolean
            //    Get #nFile, , bParent
            bool bParent;
            bParent = document.GetFileBool(br);



            //----------------------------------------------------------------    
            //    '結合キー。
            //    Dim nPrevJointKey As Long
            //    Dim nNextJointKey As Long
            //    Get #nFile, , nPrevJointKey                                           //0
            //    Get #nFile, , nNextJointKey                                           //1
            long nPrevJointKey;
            long nNextJointKey;
            nPrevJointKey = br.ReadInt32();
            nNextJointKey = br.ReadInt32();

            //-------------------------------------------------------------------------------------------------------------
            //    '配列を拡張する。
            //    Dim nMoreJointKey As Long
            //    If nPrevJointKey<nNextJointKey Then
            //        nMoreJointKey = nNextJointKey
            //    Else
            //        nMoreJointKey = nPrevJointKey
            //    End If
            //    If UBound(clsObservationPoints) < nMoreJointKey Then ReDim Preserve clsObservationPoints(nMoreJointKey)
            long nMoreJointKey;
            if (nPrevJointKey < nNextJointKey)
            {
                nMoreJointKey = nNextJointKey;                                      //1=1
            }
            else
            {
                nMoreJointKey = nPrevJointKey;
            }
            if (clsObservationPoints.Length < nMoreJointKey + 1)
            {
                //clsObservationPoints = new ObservationPoint[nMoreJointKey + 1];
                ObservationPoint[] wk = new ObservationPoint[nMoreJointKey + 1];
                Array.Copy(clsObservationPoints, wk, Math.Min(clsObservationPoints.Length, wk.Length));
                clsObservationPoints = wk;
            }

            var Genba_S2 = new GENBA_STRUCT_S[nMoreJointKey];



             //23/12/20 K.setoguchi@NV---------->>>>>>>>>>>
            //-----------------------------------------------------
            Genba_S.OPA_ListWork.m_bEnable = Genba_S.m_bEnable;
            Genba_S.OPA_ListWork.OP_ObjectType = Genba_S.OP_ObjectType;
            Genba_S.OPA_ListWork.bFirst = bFirst;                       //'長男フラグ｡ 
            Genba_S.OPA_ListWork.bParent = bParent;                     //    '親フラグ。
            //-----------------------------------------------------
            Genba_S.OPA_ListWork.OPA = Genba_S.OPA;                     //共通OPAデータ（処理後、OPA --> OPA_Str / OPA_End）
            Genba_S.OPA_ListWork.OCA = Genba_S.OCA;                     //ObservationCommonAttributes
            Genba_S.OPA_ListWork.OPFix = Genba_S.OPFix;                 //CoordinatePointFix
            Genba_S.OPA_ListWork.CPXYZ = Genba_S.CPXYZ;                 //CoordinatePointXYZ
            //-----------------------------------------------------
            // 再配置<-読み込みデータ情報
            //-----------------------------------------------------
            OPA_List.Add(Genba_S.OPA_ListWork);
            //-----------------------------------------------------
            //<<<<<<<<<-----------23/12/20 K.setoguchi@NV



            //[VB]'子がいる場合は加える。
            if (clsChildPoint != null)
            {
                AddChildPoint(clsChildPoint);
            }

            //-------------------------------------------------------------------------------    
            //    '長男であり親がいる場合は親をロードする。
            //    If bFirst And bParent Then
            //        Dim clsParentPoint As New ObservationPoint
            //        Call clsParentPoint.Load(nFile, nVersion, clsObservationPoints, Me)
            //    End If
            //    
            if (bFirst && bParent)
            {
                ObservationPoint observationPoint = new ObservationPoint(mdlMain);
                //    observationPoint.Load(br, nVersion, ref Genba_S2[0]);
                //observationPoint.Load(br, nVersion, ref Genba_S, ref OPA_List);
                observationPoint.Load(br, nVersion, ref Genba_S, ref OPA_List, ref clsObservationPoints, this);
            }

            //-------------------------------------------------------------------------------    
            //    '兄はロード済みか？
            //    If clsObservationPoints(nPrevJointKey) Is Nothing Then
            //        '兄が未ロードであれば自分を設定する。
            //        Set clsObservationPoints(nPrevJointKey) = Me
            //    Else
            //        '兄がすでにロードされている場合は連結する。
            //        Call clsObservationPoints(nPrevJointKey).AddNextPoint(Me)
            //    End If
            //
            if (clsObservationPoints[nPrevJointKey] == null)
            {
                //'兄が未ロードであれば自分を設定する。
                clsObservationPoints[nPrevJointKey] = this;
            }
            else
            {
                //'兄がすでにロードされている場合は連結する。
                clsObservationPoints[nPrevJointKey].AddNextPoint(this);
            }


            //-------------------------------------------------------------------------------    
            //    '弟はロード済みか？
            //    If clsObservationPoints(nNextJointKey) Is Nothing Then
            //        '弟が未ロードであれば自分を設定する。
            //        Set clsObservationPoints(nNextJointKey) = Me
            //        '弟が未ロードなら結合キーは初期化する。
            //        WorkKey = -1
            //    Else
            //        '弟がすでにロードされている場合は結合キーを覚えておく。
            //        WorkKey = nNextJointKey
            //        '弟を連結する。
            //        Call AddNextPoint(clsObservationPoints(nNextJointKey))
            //    End If
            if (clsObservationPoints[nNextJointKey] == null)
            {
                //'弟が未ロードであれば自分を設定する。
                clsObservationPoints[nNextJointKey] = this;
                //'弟が未ロードなら結合キーは初期化する。
                WorkKey = -1;
            }
            else
            {
                //'弟がすでにロードされている場合は結合キーを覚えておく。
                WorkKey = nNextJointKey;
                //'弟を連結する。
                AddNextPoint(clsObservationPoints[nNextJointKey]);
            }


        }
        //
        //'読み込み。
        //'
        //'長男が親観測点の Load を呼ぶ。
        //'
        //'引き数：
        //'nFile ファイル番号。
        //'nVersion ファイルバージョン。
        //'clsObservationPoints ロードした観測点を保持するためのテンポラリ配列。配列の要素は(0 To ...)。Redim clsObservationPoints(0) で初期化されていること。
        //'clsChildPoint 子観測点。
        //Public Sub Load(ByVal nFile As Integer, ByVal nVersion As Long, ByRef clsObservationPoints() As ObservationPoint, ByVal clsChildPoint As ObservationPoint)
        //
        //    Call m_clsAttributes.Load(nFile, nVersion)
        //    Get #nFile, , m_bEnable
        //    Get #nFile, , ObjectType
        //    
        //    '長男フラグ｡
        //    Dim bFirst As Boolean
        //    Get #nFile, , bFirst
        //    '親フラグ。
        //    Dim bParent As Boolean
        //    Get #nFile, , bParent
        //    
        //    '結合キー。
        //    Dim nPrevJointKey As Long
        //    Dim nNextJointKey As Long
        //    Get #nFile, , nPrevJointKey
        //    Get #nFile, , nNextJointKey
        //    
        //    '配列を拡張する。
        //    Dim nMoreJointKey As Long
        //    If nPrevJointKey<nNextJointKey Then
        //        nMoreJointKey = nNextJointKey
        //    Else
        //        nMoreJointKey = nPrevJointKey
        //    End If
        //    If UBound(clsObservationPoints) < nMoreJointKey Then ReDim Preserve clsObservationPoints(nMoreJointKey)
        //    
        //    '子がいる場合は加える。
        //    If Not clsChildPoint Is Nothing Then Call AddChildPoint(clsChildPoint)
        //    
        //    '長男であり親がいる場合は親をロードする。
        //    If bFirst And bParent Then
        //        Dim clsParentPoint As New ObservationPoint
        //        Call clsParentPoint.Load(nFile, nVersion, clsObservationPoints, Me)
        //    End If
        //    
        //    '兄はロード済みか？
        //    If clsObservationPoints(nPrevJointKey) Is Nothing Then
        //        '兄が未ロードであれば自分を設定する。
        //        Set clsObservationPoints(nPrevJointKey) = Me
        //    Else
        //        '兄がすでにロードされている場合は連結する。
        //        Call clsObservationPoints(nPrevJointKey).AddNextPoint(Me)
        //    End If
        //    
        //    '弟はロード済みか？
        //    If clsObservationPoints(nNextJointKey) Is Nothing Then
        //        '弟が未ロードであれば自分を設定する。
        //        Set clsObservationPoints(nNextJointKey) = Me
        //        '弟が未ロードなら結合キーは初期化する。
        //        WorkKey = -1
        //    Else
        //        '弟がすでにロードされている場合は結合キーを覚えておく。
        //        WorkKey = nNextJointKey
        //        '弟を連結する。
        //        Call AddNextPoint(clsObservationPoints(nNextJointKey))
        //    End If
        //
        //End Sub






        //==========================================================================================
        /*[VB]
        '子観測点を追加する。
        '
        'clsNewChildPoint で指定される観測点を、子観測点リストの最後尾に連結する。
        '
        '引き数：
        'clsNewChildPoint 追加する子観測点。
        Public Sub AddChildPoint(ByVal clsNewChildPoint As ObservationPoint)

            Dim clsPrevPoint As ObservationPoint
    
            '現在の末弟観測点を兄観測点とする。
            If m_clsChildPoint Is Nothing Then
                '子観測点がない。
                Set m_clsChildPoint = clsNewChildPoint
            Else
                Set clsPrevPoint = m_clsChildPoint
                Do While Not clsPrevPoint.NextPoint Is Nothing
                    Set clsPrevPoint = clsPrevPoint.NextPoint
                Loop
                Set clsPrevPoint.NextPoint = clsNewChildPoint
            End If
            Set clsNewChildPoint.PrevPoint = clsPrevPoint
    
            '後続全てに設定。
            Dim clsNextPoint As ObservationPoint
            Set clsNextPoint = clsNewChildPoint
            Do While Not clsNextPoint Is Nothing
                Set clsNextPoint.ParentPoint = Me
                Set clsNextPoint.ObservationShared = m_clsObservationShared
                Set clsNextPoint = clsNextPoint.NextPoint
            Loop

        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        private ObservationPoint clsPrevPoint;
        /*
        '子観測点を追加する。
        '
        'clsNewChildPoint で指定される観測点を、子観測点リストの最後尾に連結する。
        '
        '引き数：
        'clsNewChildPoint 追加する子観測点。
        */
        public void AddChildPoint(ObservationPoint clsNewChildPoint)
        {

            //ObservationPoint clsPrevPoint = new ObservationPoint();

            //'現在の末弟観測点を兄観測点とする。
            if (m_clsChildPoint == null)
            {
                //'子観測点がない。
                m_clsChildPoint = clsNewChildPoint;
            }
            else
            {
                clsPrevPoint = m_clsChildPoint;
                while (clsPrevPoint.NextPoint() != null)
                {
                    clsPrevPoint = clsPrevPoint.NextPoint();
                }
                clsPrevPoint.NextPoint(clsNewChildPoint);
            }
            clsNewChildPoint.PrevPoint(clsPrevPoint);


            //'後続全てに設定。
            ObservationPoint clsNextPoint;
            clsNextPoint = clsNewChildPoint;
            while (clsNextPoint != null)
            {
                clsNextPoint.ParentPoint(this);
                clsNextPoint.ObservationShared(m_clsObservationShared);
                clsNextPoint = clsNextPoint.NextPoint();
            }

        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '弟観測点を追加する。
        '
        'clsNewNextPoint で指定される観測点を、リストの最後尾に連結する。
        '
        '引き数：
        'clsNewNextPoint 追加する子観測点。
        Public Sub AddNextPoint(ByVal clsNewNextPoint As ObservationPoint)

            '現在の末弟観測点を兄観測点とする。
            Dim clsPrevPoint As ObservationPoint
            Set clsPrevPoint = Me
            Do While Not clsPrevPoint.NextPoint Is Nothing
                Set clsPrevPoint = clsPrevPoint.NextPoint
            Loop

            Set clsPrevPoint.NextPoint = clsNewNextPoint
            Set clsNewNextPoint.PrevPoint = clsPrevPoint
    
            '後続全てに設定。
            Dim clsNextPoint As ObservationPoint
            Set clsNextPoint = clsNewNextPoint
            Do While Not clsNextPoint Is Nothing
                Set clsNextPoint.ParentPoint = m_clsParentPoint
                Set clsNextPoint.ObservationShared = m_clsObservationShared
                Set clsNextPoint = clsNextPoint.NextPoint
            Loop

        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '弟観測点を追加する。
        '
        'clsNewNextPoint で指定される観測点を、リストの最後尾に連結する。
        '
        '引き数：
        'clsNewNextPoint 追加する子観測点。
        */
        public void AddNextPoint(ObservationPoint clsNewNextPoint)
        {
            //'現在の末弟観測点を兄観測点とする。
            ObservationPoint clsPrevPoint;
            clsPrevPoint = this;
            while (clsPrevPoint.NextPoint() != null)
            {
                clsPrevPoint = clsPrevPoint.NextPoint();
            }

            clsPrevPoint.NextPoint(clsNewNextPoint);
            clsNewNextPoint.PrevPoint(clsPrevPoint);


            //'後続全てに設定。
            ObservationPoint clsNextPoint;
            clsNextPoint = clsNewNextPoint;
            while (clsNextPoint != null)
            {
                clsNextPoint.ParentPoint(m_clsParentPoint);
                clsNextPoint.ObservationShared(m_clsObservationShared);
                clsNextPoint = clsNextPoint.NextPoint();
            }

        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '親観測点から分離する。
        Public Sub LeaveParentPoint()

            If m_clsPrevPoint Is Nothing Then
                If Not m_clsParentPoint Is Nothing Then Set m_clsParentPoint.ChildPoint = m_clsNextPoint
            Else
                Set m_clsPrevPoint.NextPoint = m_clsNextPoint
            End If
            If Not m_clsNextPoint Is Nothing Then Set m_clsNextPoint.PrevPoint = m_clsPrevPoint


            Set m_clsParentPoint = Nothing
            Set m_clsPrevPoint = Nothing
            Set m_clsNextPoint = Nothing


        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'親観測点から分離する。
        public void LeaveParentPoint()
        {
            if (m_clsPrevPoint == null)
            {
                if (m_clsParentPoint != null)
                {
                    m_clsParentPoint.ChildPoint(m_clsNextPoint);
                }
            }
            else
            {
                m_clsPrevPoint.NextPoint(m_clsNextPoint);
            }
            if (m_clsNextPoint != null)
            {
                m_clsNextPoint.PrevPoint(m_clsPrevPoint);
            }

            m_clsParentPoint = null;
            m_clsPrevPoint = null;
            m_clsNextPoint = null;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '所属する実観測点を取得する。
        '
        '自分の子孫として所属している実観測点を取得する。
        '自分が実観測点であれば自分を返す。
        '
        '戻り値：取得された実観測点を返す。配列の要素は(0 to UBound)。
        Public Function GetRealPoints() As ObservationPoint()
            Dim clsObservationPoints() As ObservationPoint
            Dim nUBound As Long
            nUBound = -1
            Call GetRealPointsImpl(clsObservationPoints, nUBound)
            GetRealPoints = clsObservationPoints
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '所属する実観測点を取得する。
        '
        '自分の子孫として所属している実観測点を取得する。
        '自分が実観測点であれば自分を返す。
        '
        '戻り値：取得された実観測点を返す。配列の要素は(0 to UBound)。
        */
        public ObservationPoint[] GetRealPoints()
        {
            ObservationPoint[] clsObservationPoints = null;
            long nUBound;
            nUBound = -1;
            GetRealPointsImpl(ref clsObservationPoints, ref nUBound);
            return clsObservationPoints;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'ファイル名を更新する。
        '
        '観測点ファイル(RINEXファイルや衛星情報ファイル)のファイル名を更新する。
        'sDstDirPath が空文字で無い場合、ファイルの移動も行われる。
        '
        '引き数：
        'sSrcDirPath 観測点フォルダのパス。
        'sDstDirPath 更新後の観測点フォルダのパス。ファイルの移動が必要なければ空文字を指定する。
        Public Sub UpdateFileName(ByVal sSrcDirPath As String, Optional ByVal sDstDirPath As String = "")
            If sDstDirPath = "" Then sDstDirPath = sSrcDirPath
            Dim sFileTitle As String
            sFileTitle = Key
            Name sSrcDirPath & FileTitle & "." & RinexExt & RNX_OBS_EXTENSION As sDstDirPath & sFileTitle & "." & RinexExt & RNX_OBS_EXTENSION
            '2017/07/05 NS6000対応。'''''''''''''''''''''''''''''''''''''''''''''''''''''''
            'Name sSrcDirPath & FileTitle & "." & RinexExt & RNX_NAV_EXTENSION As sDstDirPath & sFileTitle & "." & RinexExt & RNX_NAV_EXTENSION
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            Name sSrcDirPath & FileTitle & "." & RNX_SV_EXTENSION As sDstDirPath & sFileTitle & "." & RNX_SV_EXTENSION
            '2017/07/05 NS6000対応。'''''''''''''''''''''''''''''''''''''''''''''''''''''''
            'If m_clsAttributes.GlonassFlag Then
            '    Name sSrcDirPath & FileTitle & "." & RinexExt & RNX_GLO_EXTENSION As sDstDirPath & sFileTitle & "." & RinexExt & RNX_GLO_EXTENSION
            'End If
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            If m_clsAttributes.MixedNav Then
                Name sSrcDirPath & FileTitle & "." & RinexExt & RNX_MIX_EXTENSION As sDstDirPath & sFileTitle & "." & RinexExt & RNX_MIX_EXTENSION
            Else
                Name sSrcDirPath & FileTitle & "." & RinexExt & RNX_NAV_EXTENSION As sDstDirPath & sFileTitle & "." & RinexExt & RNX_NAV_EXTENSION
                If m_clsAttributes.GlonassFlag Then
                    Name sSrcDirPath & FileTitle & "." & RinexExt & RNX_GLO_EXTENSION As sDstDirPath & sFileTitle & "." & RinexExt & RNX_GLO_EXTENSION
                End If
                If m_clsAttributes.QZSSFlag Then
                    Name sSrcDirPath & FileTitle & "." & RinexExt & RNX_QZS_EXTENSION As sDstDirPath & sFileTitle & "." & RinexExt & RNX_QZS_EXTENSION
                End If
                If m_clsAttributes.GalileoFlag Then
                    Name sSrcDirPath & FileTitle & "." & RinexExt & RNX_GAL_EXTENSION As sDstDirPath & sFileTitle & "." & RinexExt & RNX_GAL_EXTENSION
                End If
                If m_clsAttributes.BeiDouFlag Then
                    Name sSrcDirPath & FileTitle & "." & RinexExt & RNX_BEI_EXTENSION As sDstDirPath & sFileTitle & "." & RinexExt & RNX_BEI_EXTENSION
                End If
            End If
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            FileTitle = sFileTitle
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        'ファイル名を更新する。
        '
        '観測点ファイル(RINEXファイルや衛星情報ファイル)のファイル名を更新する。
        'sDstDirPath が空文字で無い場合、ファイルの移動も行われる。
        '
        '引き数：
        'sSrcDirPath 観測点フォルダのパス。
        'sDstDirPath 更新後の観測点フォルダのパス。ファイルの移動が必要なければ空文字を指定する。
        */
        public void UpdateFileName(string sSrcDirPath, string sDstDirPath = "")
        {
            if (sDstDirPath == "")
            {
                sDstDirPath = sSrcDirPath;
            }
            string sFileTitle;
            sFileTitle = Key();
            Name(sSrcDirPath + FileTitle() + "." + RinexExt() + RNX_OBS_EXTENSION + sDstDirPath + sFileTitle + "." + RinexExt() + RNX_OBS_EXTENSION);
            //'2017/07/05 NS6000対応。'''''''''''''''''''''''''''''''''''''''''''''''''''''''
            //'Name sSrcDirPath & FileTitle & "." & RinexExt & RNX_NAV_EXTENSION As sDstDirPath & sFileTitle & "." & RinexExt & RNX_NAV_EXTENSION
            //'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            Name(sSrcDirPath + FileTitle() + "." + RNX_SV_EXTENSION + sDstDirPath + sFileTitle + "." + RNX_SV_EXTENSION);
            //'2017/07/05 NS6000対応。'''''''''''''''''''''''''''''''''''''''''''''''''''''''
            //'If m_clsAttributes.GlonassFlag Then
            //'    Name sSrcDirPath & FileTitle & "." & RinexExt & RNX_GLO_EXTENSION As sDstDirPath & sFileTitle & "." & RinexExt & RNX_GLO_EXTENSION
            //'End If
            //'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            if (m_clsAttributes.MixedNav)
            {
                Name(sSrcDirPath + FileTitle() + "." + RinexExt() + RNX_MIX_EXTENSION + sDstDirPath + sFileTitle + "." + RinexExt() + RNX_MIX_EXTENSION);
            }
            else
            {
                Name(sSrcDirPath + FileTitle() + "." + RinexExt() + RNX_NAV_EXTENSION + sDstDirPath + sFileTitle + "." + RinexExt() + RNX_NAV_EXTENSION);
                if (m_clsAttributes.GlonassFlag)
                {
                    Name(sSrcDirPath + FileTitle() + "." + RinexExt() + RNX_GLO_EXTENSION + sDstDirPath + sFileTitle + "." + RinexExt() + RNX_GLO_EXTENSION);
                }
                if (m_clsAttributes.QZSSFlag)
                {
                    Name(sSrcDirPath + FileTitle() + "." + RinexExt() + RNX_QZS_EXTENSION + sDstDirPath + sFileTitle + "." + RinexExt() + RNX_QZS_EXTENSION);
                }
                if (m_clsAttributes.GalileoFlag)
                {
                    Name(sSrcDirPath + FileTitle() + "." + RinexExt() + RNX_GAL_EXTENSION + sDstDirPath + sFileTitle + "." + RinexExt() + RNX_GAL_EXTENSION);
                }
                if (m_clsAttributes.BeiDouFlag)
                {
                    Name(sSrcDirPath + FileTitle() + "." + RinexExt() + RNX_BEI_EXTENSION + sDstDirPath + sFileTitle + "." + RinexExt() + RNX_BEI_EXTENSION);
                }
            }
            //'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            FileTitle(sFileTitle);
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '位相中心高を取得する。
        '
        '引き数：
        'nTrueVertical 位相中心高(ｍ)が設定される。
        '
        '戻り値：
        '位相中心高が取得できた場合は True を返す。
        '位相中心高が取得できなかった場合は False を返す。
        Public Function GetTrueVertical(ByRef nTrueVertical As Double) As Boolean
            If m_clsObservationShared Is Nothing Then
                GetTrueVertical = False
            Else
                GetTrueVertical = m_clsObservationShared.GetTrueVertical(m_clsAttributes.AntType, m_clsAttributes.AntMeasurement, m_clsAttributes.AntHeight, nTrueVertical)
            End If
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '位相中心高を取得する。
        '
        '引き数：
        'nTrueVertical 位相中心高(ｍ)が設定される。
        '
        '戻り値：
        '位相中心高が取得できた場合は True を返す。
        '位相中心高が取得できなかった場合は False を返す。
        */
        public bool GetTrueVertical(ref double nTrueVertical)
        {
            if (m_clsObservationShared == null)
            {
                return false;
            }
            else
            {
                return m_clsObservationShared.GetTrueVertical(m_clsAttributes.AntType, m_clsAttributes.AntMeasurement, m_clsAttributes.AntHeight, ref nTrueVertical);
            }
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'アンテナマウント高を取得する。
        '
        '引き数：
        'nMountVertical アンテナマウント高(ｍ)が設定される。
        '
        '戻り値：
        'アンテナマウント高が取得できた場合は True を返す。
        'アンテナマウント高が取得できなかった場合は False を返す。
        Public Function GetMountVertical(ByRef nMountVertical As Double) As Boolean
            If m_clsObservationShared Is Nothing Then
                GetMountVertical = False
            Else
                GetMountVertical = m_clsObservationShared.GetMountVertical(m_clsAttributes.AntType, m_clsAttributes.AntMeasurement, m_clsAttributes.AntHeight, nMountVertical)
            End If
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        'アンテナマウント高を取得する。
        '
        '引き数：
        'nMountVertical アンテナマウント高(ｍ)が設定される。
        '
        '戻り値：
        'アンテナマウント高が取得できた場合は True を返す。
        'アンテナマウント高が取得できなかった場合は False を返す。
        */
        public bool GetMountVertical(ref double nMountVertical)
        {
            if (m_clsObservationShared == null)
            {
                return false;
            }
            else
            {
                return m_clsObservationShared.GetMountVertical(m_clsAttributes.AntType, m_clsAttributes.AntMeasurement, m_clsAttributes.AntHeight, ref nMountVertical);
            }
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '補正点を更新する。
        '
        '補正点を clsCorrectPoint で指定される補正点で上書きする。
        'HeadPoint 以下全ての観測点を更新する。
        '本点に偏心点を設定する場合は clsCorrectPoint は HeadPoint。
        '偏心点に本点を設定する場合は clsCorrectPoint は 実観測点。
        '
        '引き数：
        'clsCorrectPoint 新しい補正点。
        Public Sub UpdateCorrectPoint(ByVal clsCorrectPoint As ObservationPoint)
            Call HeadPoint.UpdateCorrectPointImpl(clsCorrectPoint)
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '補正点を更新する。
        '
        '補正点を clsCorrectPoint で指定される補正点で上書きする。
        'HeadPoint 以下全ての観測点を更新する。
        '本点に偏心点を設定する場合は clsCorrectPoint は HeadPoint。
        '偏心点に本点を設定する場合は clsCorrectPoint は 実観測点。
        '
        '引き数：
        'clsCorrectPoint 新しい補正点。
        */
        public void UpdateCorrectPoint(ObservationPoint clsCorrectPoint)
        {
            HeadPoint().UpdateCorrectPointImpl(clsCorrectPoint);
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '補正点を更新する。
        '
        '補正点を clsCorrectPoint で指定される補正点で上書きする。
        '子観測点、弟観測点も更新する。
        '
        '引き数：
        'clsCorrectPoint 新しい補正点。
        Public Sub UpdateCorrectPointImpl(ByVal clsCorrectPoint As ObservationPoint)
            Set m_clsCorrectPoint = clsCorrectPoint
            If Not m_clsChildPoint Is Nothing Then Call m_clsChildPoint.UpdateCorrectPointImpl(clsCorrectPoint)
            If Not m_clsNextPoint Is Nothing Then Call m_clsNextPoint.UpdateCorrectPointImpl(clsCorrectPoint)
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '補正点を更新する。
        '
        '補正点を clsCorrectPoint で指定される補正点で上書きする。
        '子観測点、弟観測点も更新する。
        '
        '引き数：
        'clsCorrectPoint 新しい補正点。
        */
        public void UpdateCorrectPointImpl(ObservationPoint clsCorrectPoint)
        {
            m_clsCorrectPoint = clsCorrectPoint;
            if (m_clsChildPoint != null)
            {
                m_clsChildPoint.UpdateCorrectPointImpl(clsCorrectPoint);
            }
            if (m_clsNextPoint != null)
            {
                m_clsNextPoint.UpdateCorrectPointImpl(clsCorrectPoint);
            }
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '本点情報を更新する。
        '
        '現在の属性に合わせて本点の情報を新しく更新する。
        Public Sub UpdateGenuineInfo()
            'Commonでない情報を更新する時にはHeadからすべて変更するのを忘れないように。
            Number = GENUINE_POINT_SESSION & m_clsCorrectPoint.Number
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '本点情報を更新する。
        '
        '現在の属性に合わせて本点の情報を新しく更新する。
        */
        public void UpdateGenuineInfo()
        {
            //'Commonでない情報を更新する時にはHeadからすべて変更するのを忘れないように。
            Number(GENUINE_POINT_SESSION + m_clsCorrectPoint.Number());
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '*******************************************************************************
        'インプリメンテーション

        '代表子観測点を取得する。
        '
        '所属する適当な子観測点を取得する。
        '最下層の ChildPoint を取得する。
        '自分が最下層なら自分を返す。
        '有効な観測点が優先される。
        '有効な観測点が無ければ無効な観測点を返す。
        '
        '引き数：
        'bEnable 取得された観測点の有効フラグが設定される。
        '
        '戻り値：取得された代表子観測点を返す。
        Public Function ChildPointRepresent(ByRef bEnable As Boolean) As ObservationPoint
            If m_clsChildPoint Is Nothing Then
                '最下位なら無効でもかまわないので自分を返す。
                bEnable = m_bEnable
                Set ChildPointRepresent = Me
            Else
                Dim clsChildPoint As ObservationPoint
                Set clsChildPoint = m_clsChildPoint
                Do While Not clsChildPoint Is Nothing
                    Set ChildPointRepresent = clsChildPoint.ChildPointRepresent(bEnable)
                    '有効な子観測点が見つかったか？
                    If bEnable Then Exit Function
                    Set clsChildPoint = clsChildPoint.NextPoint
                Loop
                '有効な子観測点が見つからなかった場合、無効でもかまわないので最下位子観測点を返す。
                Set ChildPointRepresent = m_clsChildPoint.BottomChildPoint
            End If
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '*******************************************************************************
        'インプリメンテーション

        '代表子観測点を取得する。
        '
        '所属する適当な子観測点を取得する。
        '最下層の ChildPoint を取得する。
        '自分が最下層なら自分を返す。
        '有効な観測点が優先される。
        '有効な観測点が無ければ無効な観測点を返す。
        '
        '引き数：
        'bEnable 取得された観測点の有効フラグが設定される。
        '
        '戻り値：取得された代表子観測点を返す。
        */
        public ObservationPoint ChildPointRepresent(bool bEnable)
        {
            ObservationPoint w_ChildPointRepresent;
            if (m_clsChildPoint == null)
            {
                //'最下位なら無効でもかまわないので自分を返す。
                bEnable = m_bEnable;
                w_ChildPointRepresent = this;
                return w_ChildPointRepresent;
            }
            else
            {
                ObservationPoint clsChildPoint;
                clsChildPoint = m_clsChildPoint;
                while (clsChildPoint != null)
                {
                    w_ChildPointRepresent = clsChildPoint.ChildPointRepresent(bEnable);
                    //'有効な子観測点が見つかったか？
                    if (bEnable)
                    {
                        return w_ChildPointRepresent;
                    }
                    clsChildPoint = clsChildPoint.NextPoint();
                }
                //'有効な子観測点が見つからなかった場合、無効でもかまわないので最下位子観測点を返す。
                return m_clsChildPoint.BottomChildPoint();
            }
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '所属する実観測点を取得する。
        '
        '自分の子孫として所属している実観測点を取得する。
        '自分が実観測点であれば自分を設定する。
        '
        '引き数：
        'clsObservationPoints 取得された実観測点が設定される。配列の要素は(0 to UBound)。
        'nUBound clsObservationPoints の UBound。
        Public Sub GetRealPointsImpl(ByRef clsObservationPoints() As ObservationPoint, ByRef nUBound As Long)
            '自分が実観測点であるか？
            If(ObjectType And OBS_TYPE_WORD) = 0 Then
                nUBound = nUBound + 1
                ReDim Preserve clsObservationPoints(nUBound)
                Set clsObservationPoints(nUBound) = Me
                Exit Sub
            End If
            '子観測点の数だけ繰り返す。
            Dim clsChildPoint As ObservationPoint
            Set clsChildPoint = ChildPoint
            Do While Not clsChildPoint Is Nothing
                If(clsChildPoint.ObjectType And OBS_TYPE_WORD) = 0 Then
                    nUBound = nUBound + 1
                    ReDim Preserve clsObservationPoints(nUBound)
                    Set clsObservationPoints(nUBound) = clsChildPoint
                Else
                    '再帰。
                    Call clsChildPoint.GetRealPointsImpl(clsObservationPoints, nUBound)
                End If
                Set clsChildPoint = clsChildPoint.NextPoint
            Loop
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '所属する実観測点を取得する。
        '
        '自分の子孫として所属している実観測点を取得する。
        '自分が実観測点であれば自分を設定する。
        '
        '引き数：
        'clsObservationPoints 取得された実観測点が設定される。配列の要素は(0 to UBound)。
        'nUBound clsObservationPoints の UBound。
        */
        public void GetRealPointsImpl(ref ObservationPoint[] clsObservationPoints, ref long nUBound)
        {
            //'自分が実観測点であるか？
            if ((ObjectType & OBS_TYPE_WORD) == 0)
            {
                nUBound++;
                clsObservationPoints = new ObservationPoint[nUBound];
                clsObservationPoints[nUBound] = this;
                return;
            }
            //'子観測点の数だけ繰り返す。
            ObservationPoint clsChildPoint;
            clsChildPoint = ChildPoint();
            while (clsChildPoint != null)
            {
                if ((clsChildPoint.ObjectType & OBS_TYPE_WORD) == 0)
                {
                    nUBound++;
                    clsObservationPoints = new ObservationPoint[nUBound];
                    clsObservationPoints[nUBound] = clsChildPoint;
                }
                else
                {
                    //'再帰。
                    clsChildPoint.GetRealPointsImpl(ref clsObservationPoints, ref nUBound);
                }
                clsChildPoint = clsChildPoint.NextPoint();
            }
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '解析終点座標を取得する。
        '
        '自分が、解析済基線ベクトルの終点となっている基線ベクトルを取得する。
        '優先順位は次の通り。
        '①FIX解である基線ベクトル。
        '②解析済である基線ベクトル。
        '※条件が同じ場合はより後に解析した基線ベクトルを優先する。
        '
        '引き数：
        'objOwner 取得された Owner オブジェクトが設定される。
        'bWork Owner の代わりに WorkObject を使用する。
        '
        '戻り値：
        '優先度を返す。優先度が高いほど戻り値は低い(負の値)。
        '解析済基線ベクトルが無い場合は 0 を返す。
        Public Function GetCoordinateAnalysisEnd(ByRef objOwner As Object, ByVal bWork As Boolean) As Long

            Dim nResult As Long
            nResult = 0
    
            If(ObjectType And OBS_TYPE_CONNECT) <> 0 Then
                If bWork Then
                    Set objOwner = WorkObject
                Else
                    Set objOwner = Owner
                End If
                '解析済基線ベクトルの終点であれば候補とする。
                If objOwner.AnalysisEndPoint Is Me And objOwner.Enable Then
                    If objOwner.Analysis = ANALYSIS_STATUS_FIX Then
                        '① FIX解が、最優先。
                        nResult = &H80000000 Or objOwner.AnalysisOrder
                    ElseIf objOwner.Analysis = ANALYSIS_STATUS_FLOAT Then
                        '② 有効であり、解析済みが、最低条件。
                        nResult = &H90000000 Or objOwner.AnalysisOrder
                    End If
                    '参考：
                    '① nResult = &H80000000 Or Owner.AnalysisOrder
                    '② nResult = &H90000000 Or Owner.AnalysisOrder
                    '③ nResult = &HA0000000 Or Owner.AnalysisOrder
                    '④ nResult = &HB0000000 Or Owner.AnalysisOrder
                    '                  ：
                    '⑧ nResult = &HF0000000 Or Owner.AnalysisOrder
                End If
            Else
                Dim clsNextPoint As ObservationPoint
                Set clsNextPoint = m_clsChildPoint
                Do While Not clsNextPoint Is Nothing
                    Dim objOwnerTmp As Object
                    Dim nResultTmp As Long
                    nResultTmp = clsNextPoint.GetCoordinateAnalysisEnd(objOwnerTmp, bWork)
                    If nResult > nResultTmp Then
                        Set objOwner = objOwnerTmp
                        nResult = nResultTmp
                    End If
                    Set clsNextPoint = clsNextPoint.NextPoint
                Loop
            End If

            GetCoordinateAnalysisEnd = nResult


        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '解析終点座標を取得する。
        '
        '自分が、解析済基線ベクトルの終点となっている基線ベクトルを取得する。
        '優先順位は次の通り。
        '①FIX解である基線ベクトル。
        '②解析済である基線ベクトル。
        '※条件が同じ場合はより後に解析した基線ベクトルを優先する。
        '
        '引き数：
        'objOwner 取得された Owner オブジェクトが設定される。
        'bWork Owner の代わりに WorkObject を使用する。
        '
        '戻り値：
        '優先度を返す。優先度が高いほど戻り値は低い(負の値)。
        '解析済基線ベクトルが無い場合は 0 を返す。
        */
        public long GetCoordinateAnalysisEnd(ref object objOwner, bool bWork)
        {
#if false
            long nResult;
            nResult = 0;

            if ((ObjectType & OBS_TYPE_CONNECT) != 0)
            {
                if (bWork)
                {
                    objOwner = WorkObject;
                }
                else
                {
                    objOwner = Owner;
                }
                //'解析済基線ベクトルの終点であれば候補とする。
                if (objOwner.AnalysisEndPoint() == this && objOwner.Enable() == true)
                {
                    if (objOwner.Analysis == ANALYSIS_STATUS.ANALYSIS_STATUS_FIX)
                    {
                        //'① FIX解が、最優先。
                        nResult = 0x80000000 | objOwner.AnalysisOrder;
                    }
                    else if (objOwner.Analysis == ANALYSIS_STATUS.ANALYSIS_STATUS_FLOAT)
                    {
                        //'② 有効であり、解析済みが、最低条件。
                        nResult = 0x90000000 | objOwner.AnalysisOrder;
                    }
                    /*
                    '参考：
                    '① nResult = &H80000000 Or Owner.AnalysisOrder
                    '② nResult = &H90000000 Or Owner.AnalysisOrder
                    '③ nResult = &HA0000000 Or Owner.AnalysisOrder
                    '④ nResult = &HB0000000 Or Owner.AnalysisOrder
                    '                  ：
                    '⑧ nResult = &HF0000000 Or Owner.AnalysisOrder
                    */
                }
            }
            else
            {
                ObservationPoint clsNextPoint;
                clsNextPoint = m_clsChildPoint;
                while (clsNextPoint != null)
                {
                    object objOwnerTmp;
                    long nResultTmp;
                    nResultTmp = clsNextPoint.GetCoordinateAnalysisEnd(objOwnerTmp, bWork);
                    if (nResult > nResultTmp)
                    {
                        objOwner = objOwnerTmp;
                        nResult = nResultTmp;
                    }
                    clsNextPoint = clsNextPoint.NextPoint();
                }
            }
            return nResult;
#else
            objOwner = null;
            return -1;
#endif
        }
        //==========================================================================================
    }
}
