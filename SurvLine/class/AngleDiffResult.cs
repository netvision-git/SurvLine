using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using System.Windows.Forms;
using System.Xml.Linq;
using System.IO;
using static System.Net.WebRequestMethods;
using System.Security.Cryptography.X509Certificates;
using SurvLine.mdl;
using static SurvLine.mdl.DEFINE;
using static SurvLine.mdl.MdlNSDefine;
using static SurvLine.mdl.MdlUtility;
using static SurvLine.mdl.MdlAccountMake;
using static SurvLine.mdl.MdlNSGUI;
using static SurvLine.mdl.MdlNSSDefine;


namespace SurvLine
{
    internal class AngleDiffResult
    {


        //'*******************************************************************************
        //'閉合差点検結果
        //'
        //'閉合差の点検結果を保持する。

        //Option Explicit

        //'プロパティ
        public string FromNumber;       // As String '自点観測点番号。
        public string FromName;         // As String '自点観測点名称。
        public string ToNumber;         // As String '至点観測点番号。
        public string ToName;           // As String '至点観測点名称。
        public string FixedNumber;      // As String '固定点観測点番号。
        public string FixedName;        // As String '固定点観測点名称。
        public double SumHeight;        // As Double '合計楕円体高(ｍ)。'2023/05/19 Hitz H.Nakamura 楕円体高の閉合差を追加。
        public double ResultHeight;     // As Double '結果楕円体高(ｍ)。'2023/05/19 Hitz H.Nakamura 楕円体高の閉合差を追加。
        public double PermissionHeight; // As Double '楕円体高の許容範囲(ｍ)。'2023/05/19 Hitz H.Nakamura 楕円体高の閉合差を追加。


        //'インプリメンテーション
        private CoordinatePointFix m_clsFromCoordinate; // Private m_clsFromCoordinate As New CoordinatePointFix '自点座標。
        private CoordinatePointFix m_clsToCoordinate;   // Private m_clsToCoordinate As New CoordinatePointFix '至点座標。
        private CoordinatePointFix m_clsFixedCoordinate;// private m_clsFixedCoordinate As New CoordinatePointFix '固定点座標。
        private List<string> m_sStrNumber;              // Private m_sStrNumber() As String '始点観測点番号。配列の要素は(-1 To ...)、要素 -1 は未使用。
        private List<string> m_sStrName;                // Private m_sStrName() As String '始点観測点名称。配列の要素は(-1 To ...)、要素 -1 は未使用。
        private List<string> m_sEndNumber;              // Private m_sEndNumber() As String '終点観測点番号。配列の要素は(-1 To ...)、要素 -1 は未使用。
        private List<string> m_sEndName;                // Private m_sEndName() As String '終点観測点名称。配列の要素は(-1 To ...)、要素 -1 は未使用。
        private List<CoordinatePoint> m_clsVector;      // Private m_clsVector() As CoordinatePoint 'ベクトル値。配列の要素は(-1 To ...)、要素 -1 は未使用。

        private List<double> m_nHeightEnd;              // Private m_nHeightEnd() As Double '終点楕円体高(ｍ)。配列の要素は(-1 To ...)、要素 -1 は未使用。'2023/05/19 Hitz H.Nakamura 楕円体高の閉合差を追加。
        private List<double> m_nHeightDiff;             // Private m_nHeightDiff() As Double '楕円体比高(ｍ)。配列の要素は(-1 To ...)、要素 -1 は未使用。'2023/05/19 Hitz H.Nakamura 楕円体高の閉合差を追加。
        private List<string> m_sSession;                // Private m_sSession() As String 'セッション名。配列の要素は(-1 To ...)、要素 -1 は未使用。
        private CoordinatePointXYZ m_clsSumXYZ;         // Private m_clsSumXYZ As New CoordinatePointXYZ '合計XYZ。
        private CoordinatePointXYZ m_clsResultXYZ;      // Private m_clsResultXYZ As New CoordinatePointXYZ '結果XYZ。
        private CoordinatePointXYZ m_clsResultNEU;      // Private m_clsResultNEU As New CoordinatePointXYZ '結果NEU。

#if false
        '*******************************************************************************
        'プロパティ

        '帳票パラメータ。
        '
        'このプロパティはデフォルトプロパティである。Letを使えば｢値の代入｣ができる。いわゆるC++で言うところのoperator=である。
        '
        '引き数：
        'clsAngleDiffResult コピー元のオブジェクト。
        Property Let AngleDiffResult(ByVal clsAngleDiffResult As AngleDiffResult)

            FromNumber = clsAngleDiffResult.FromNumber
            FromName = clsAngleDiffResult.FromName
            ToNumber = clsAngleDiffResult.ToNumber
            ToName = clsAngleDiffResult.ToName
            FixedNumber = clsAngleDiffResult.FixedNumber
            FixedName = clsAngleDiffResult.FixedName
            FromCoordinate = clsAngleDiffResult.FromCoordinate
            ToCoordinate = clsAngleDiffResult.ToCoordinate
            FixedCoordinate = clsAngleDiffResult.FixedCoordinate
            Count = clsAngleDiffResult.Count


            Dim i As Long
            For i = 0 To Count - 1
                StrNumber(i) = clsAngleDiffResult.StrNumber(i)
                StrName(i) = clsAngleDiffResult.StrName(i)
                EndNumber(i) = clsAngleDiffResult.EndNumber(i)
                EndName(i) = clsAngleDiffResult.EndName(i)
                Vector(i) = clsAngleDiffResult.Vector(i)
                Session(i) = clsAngleDiffResult.Session(i)
            Next


            SumXYZ = clsAngleDiffResult.SumXYZ
            ResultXYZ = clsAngleDiffResult.ResultXYZ
            ResultNEU = clsAngleDiffResult.ResultNEU
    
            '2023/05/19 Hitz H.Nakamura **************************************************
            '楕円体高の閉合差を追加。
            SumHeight = clsAngleDiffResult.SumHeight
            ResultHeight = clsAngleDiffResult.ResultHeight
            PermissionHeight = clsAngleDiffResult.PermissionHeight

            For i = 0 To Count - 1
                HeightEnd(i) = clsAngleDiffResult.HeightEnd(i)
                HeightDiff(i) = clsAngleDiffResult.HeightDiff(i)
            Next
            '*****************************************************************************


        End Property

'自点座標。
Property Let FromCoordinate(ByVal clsFromCoordinate As CoordinatePointFix)
    Let m_clsFromCoordinate = clsFromCoordinate
End Property

'自点座標。
Property Get FromCoordinate() As CoordinatePointFix
    Set FromCoordinate = m_clsFromCoordinate
End Property

'至点座標。
Property Let ToCoordinate(ByVal clsToCoordinate As CoordinatePointFix)
    Let m_clsToCoordinate = clsToCoordinate
End Property

'至点座標。
Property Get ToCoordinate() As CoordinatePointFix
    Set ToCoordinate = m_clsToCoordinate
End Property

'固定点座標。
Property Let FixedCoordinate(ByVal clsFixedCoordinate As CoordinatePointFix)
    Let m_clsFixedCoordinate = clsFixedCoordinate
End Property

'固定点座標。
Property Get FixedCoordinate() As CoordinatePointFix
    Set FixedCoordinate = m_clsFixedCoordinate
End Property

'基線ベクトル数。
Property Let Count(ByVal nCount As Long)
    ReDim Preserve m_sStrNumber(-1 To nCount - 1)
    ReDim Preserve m_sStrName(-1 To nCount - 1)
    ReDim Preserve m_sEndNumber(-1 To nCount - 1)
    ReDim Preserve m_sEndName(-1 To nCount - 1)
    ReDim Preserve m_clsVector(-1 To nCount - 1)
    ReDim Preserve m_nHeightEnd(-1 To nCount - 1) '2023/05/19 Hitz H.Nakamura 楕円体高の閉合差を追加。
    ReDim Preserve m_nHeightDiff(-1 To nCount - 1) '2023/05/19 Hitz H.Nakamura 楕円体高の閉合差を追加。
    ReDim Preserve m_sSession(-1 To nCount - 1)
End Property

'基線ベクトル数。
Property Get Count() As Long
    Count = UBound(m_clsVector) + 1
End Property

'始点観測点番号。
Property Let StrNumber(ByVal nIndex As Long, ByVal sStrNumber As String)
    m_sStrNumber(nIndex) = sStrNumber
End Property

'始点観測点番号。
Property Get StrNumber(ByVal nIndex As Long) As String
    StrNumber = m_sStrNumber(nIndex)
End Property


#endif

        //'始点観測点名称。
        //Property Let StrName(ByVal nIndex As Long, ByVal sStrName As String)
        //    m_sStrName(nIndex) = sStrName
        //End Property


        //'*****************************************************************************
        //'*****************************************************************************
        public string StrName(long nIndex)
        {
            string StrName = m_sStrName[(int)nIndex];
            return StrName;
        }
        //------------------------------------------------------------------------------
        //'始点観測点名称。
        //Property Get StrName(ByVal nIndex As Long) As String
        //    StrName = m_sStrName(nIndex)
        //End Property
        //'*****************************************************************************


        //'*****************************************************************************
        //'*****************************************************************************
        /// <summary>
        /// 終点観測点番号。
        /// </summary>
        /// <param name="nIndex"></param>
        /// <param name="sEndNumber"></param>
        /// <returns></returns>
        public void EndNumber(long nIndex, string sEndNumber)
        {
            m_sEndNumber[(int)nIndex] = sEndNumber;
        }
        //------------------------------------------------------------------------------
        //'終点観測点番号。
        //Property Let EndNumber(ByVal nIndex As Long, ByVal sEndNumber As String)
        //    m_sEndNumber(nIndex) = sEndNumber
        //End Property
        //'*****************************************************************************


        //'*****************************************************************************
        //'*****************************************************************************
        /// <summary>
        ///     終点観測点番号。
        /// </summary>
        /// <param name="nIndex"></param>
        /// <returns></returns>
        public string EndNumber(long nIndex)
        {
            string EndNumber = m_sEndNumber[(int)nIndex];
            return EndNumber;
        }
        //------------------------------------------------------------------------------
        //'終点観測点番号。
        //Property Get EndNumber(ByVal nIndex As Long) As String
        //    EndNumber = m_sEndNumber(nIndex)
        //End Property
        //'*****************************************************************************

        //'*****************************************************************************
        //'*****************************************************************************
        /// <summary>
        /// 終点観測点名称。
        /// 
        /// </summary>
        /// <param name="nIndex"></param>
        /// <param name="sEndName"></param>
        public void EndName(long nIndex, string sEndName)
        {
            m_sEndName[(int)nIndex] = sEndName;
        }
        //------------------------------------------------------------------------------
        //'終点観測点名称。
        //Property Let EndName(ByVal nIndex As Long, ByVal sEndName As String)
        //    m_sEndName(nIndex) = sEndName
        //End Property
        //'*****************************************************************************

        //'*****************************************************************************
        //'*****************************************************************************
        /// <summary>
        /// 終点観測点名称。
        /// 
        /// </summary>
        /// <param name="nIndex"></param>
        /// <returns></returns>
        public string EndName(long nIndex)
        {
            string EndName = m_sEndName[(int)nIndex];
            return EndName;
        }
        //------------------------------------------------------------------------------
        //'終点観測点名称。
        //Property Get EndName(ByVal nIndex As Long) As String
        //    EndName = m_sEndName(nIndex)
        //End Property
        //'*****************************************************************************

        //public void Vector(long nIndex, CoordinatePoint clsVector)
        //{
        //    if (m_clsVector[(int)nIndex] != null)
        //    {
        //        m_clsVector[(int)nIndex] = (CoordinatePoint) new CoordinatePointXYZ();
        //
        //    }
        //}
        //'ベクトル値。
        //Property Let Vector(ByVal nIndex As Long, ByVal clsVector As CoordinatePoint)
        //    If m_clsVector(nIndex) Is Nothing Then Set m_clsVector(nIndex) = New CoordinatePointXYZ
        //    Let m_clsVector(nIndex) = clsVector
        //End Property



        //'ベクトル値。
        //Property Get Vector(ByVal nIndex As Long) As CoordinatePoint
        //    If m_clsVector(nIndex) Is Nothing Then Set m_clsVector(nIndex) = New CoordinatePointXYZ
        //    Set Vector = m_clsVector(nIndex)
        //End Property


        //'2023/05/19 Hitz H.Nakamura **************************************************
        //'楕円体高の閉合差を追加。
        //'終点楕円体高。
        //Property Let HeightEnd(ByVal nIndex As Long, ByVal nHeightEnd As Double)
        //    m_nHeightEnd(nIndex) = nHeightEnd
        //End Property

        //'終点楕円体高。
        //Property Get HeightEnd(ByVal nIndex As Long) As Double
        //    HeightEnd = m_nHeightEnd(nIndex)
        //End Property

        //'楕円体比高。
        //Property Let HeightDiff(ByVal nIndex As Long, ByVal nHeightDiff As Double)
        //    m_nHeightDiff(nIndex) = nHeightDiff
        //End Property

        //'楕円体比高。
        //Property Get HeightDiff(ByVal nIndex As Long) As Double
        //    HeightDiff = m_nHeightDiff(nIndex)
        //End Property
        //'*****************************************************************************

        //'*****************************************************************************
        //'*****************************************************************************
        private void Session(long nIndex, string sSession)
        {
            m_sSession[(int)nIndex] = sSession;
        }
        //-----------------------------------------------------------------------
        //'セッション名。
        //Property Let Session(ByVal nIndex As Long, ByVal sSession As String)
        //    m_sSession(nIndex) = sSession
        //End Property
        //'*****************************************************************************

        //'*****************************************************************************
        private string Session(long nIndex)
        {
            string Session = m_sSession[(int)nIndex];
            return Session;
        }
        //'セッション名。
        //Property Get Session(ByVal nIndex As Long) As String
        //    Session = m_sSession(nIndex)
        //End Property
        //'*****************************************************************************




        //'合計XYZ。
        //Property Let SumXYZ(ByVal clsSumXYZ As CoordinatePoint)
        //    Let m_clsSumXYZ = clsSumXYZ
        //End Property


        //'合計XYZ。
        //Property Get SumXYZ() As CoordinatePoint
        //    Set SumXYZ = m_clsSumXYZ
        //End Property

        //'結果XYZ。
        //Property Let ResultXYZ(ByVal clsResultXYZ As CoordinatePoint)
        //    Let m_clsResultXYZ = clsResultXYZ
        //End Property

        //'結果XYZ。
        //Property Get ResultXYZ() As CoordinatePoint
        //    Set ResultXYZ = m_clsResultXYZ
        //End Property

        //'結果NEU。
        //Property Let ResultNEU(ByVal clsResultNEU As CoordinatePoint)
        //    Let m_clsResultNEU = clsResultNEU
        //End Property

        //'結果NEU。
        //Property Get ResultNEU() As CoordinatePoint
        //    Set ResultNEU = m_clsResultNEU
        //End Property

        //'*******************************************************************************
        //'メソッド
        //

        //'*******************************************************************************
        //'*******************************************************************************
        /// <summary>
        /// 保存。
        ///
        /// 引き数：
        ///     bw バイナリファイル
        /// </summary>
        /// <param name="bw"></param>
        public void Save(BinaryWriter bw)
        {
            FileWrite_PutString(bw, FromNumber); //    Call PutString(nFile, FromNumber)
            FileWrite_PutString(bw, FromName);   //    Call PutString(nFile, FromName)
            FileWrite_PutString(bw, ToNumber);   //    Call PutString(nFile, ToNumber)
            FileWrite_PutString(bw, ToName);     //    Call PutString(nFile, ToName)
            FileWrite_PutString(bw, FixedNumber);//    Call PutString(nFile, FixedNumber)
            FileWrite_PutString(bw, FixedName);  //    Call PutString(nFile, FixedName)
                                                            //   FromCoordinate.Save(bw);                        //    Call FromCoordinate.Save(nFile)
                                                            //   ToCoordinate.Save(bw);                          //    Call ToCoordinate.Save(nFile)
                                                            //   FixedCoordinate.Save(bw);                       //    Call FixedCoordinate.Save(nFile)


            long Count = 0;//瀬戸口


            bw.Write((int)Count);    //    Put #nFile, , Count


            //    Dim i As Long
            //    For i = 0 To Count - 1
            //        Call PutString(nFile, StrNumber(i))
            //        Call PutString(nFile, StrName(i))
            //        Call PutString(nFile, EndNumber(i))
            //        Call PutString(nFile, EndName(i))
            //        Call Vector(i).Save(nFile)
            //        Call PutString(nFile, Session(i))
            //    Next
            for (long i = 0; i <= (Count - 1); i++)
            {
                FileWrite_PutString(bw, FixedName);      //        Call PutString(nFile, StrNumber(i))
                FileWrite_PutString(bw, StrName(i));     //        Call PutString(nFile, StrName(i))
                FileWrite_PutString(bw, EndNumber(i));   //        Call PutString(nFile, EndNumber(i))
                FileWrite_PutString(bw, EndName(i));     //        Call PutString(nFile, EndName(i))
                                                                    //    Vector(i).Save(nFile);                          //        Call Vector(i).Save(nFile)
                FileWrite_PutString(bw, Session(i));    //        Call PutString(nFile, Session(i))
            }


        }
        //--------------------------------------------------------------------------------
        //
        //'保存。
        //'
        //'引き数：
        //'nFile ファイル番号。
        //Public Sub Save(ByVal nFile As Integer)
        //
        //    Call PutString(nFile, FromNumber)
        //    Call PutString(nFile, FromName)
        //    Call PutString(nFile, ToNumber)
        //    Call PutString(nFile, ToName)
        //    Call PutString(nFile, FixedNumber)
        //    Call PutString(nFile, FixedName)
        //    Call FromCoordinate.Save(nFile)
        //    Call ToCoordinate.Save(nFile)
        //    Call FixedCoordinate.Save(nFile)
        //
        //
        //    Put #nFile, , Count
        //    Dim i As Long
        //    For i = 0 To Count - 1
        //        Call PutString(nFile, StrNumber(i))
        //        Call PutString(nFile, StrName(i))
        //        Call PutString(nFile, EndNumber(i))
        //        Call PutString(nFile, EndName(i))
        //        Call Vector(i).Save(nFile)
        //        Call PutString(nFile, Session(i))
        //    Next
        //
        //    Call SumXYZ.Save(nFile)
        //    Call ResultXYZ.Save(nFile)
        //    Call ResultNEU.Save(nFile)
        //    
        //    '2023/05/19 Hitz H.Nakamura **************************************************
        //    '楕円体高の閉合差を追加。
        //    Put #nFile, , SumHeight
        //    Put #nFile, , ResultHeight
        //    Put #nFile, , PermissionHeight
        //    
        //    For i = 0 To Count - 1
        //        Put #nFile, , m_nHeightEnd(i)
        //        Put #nFile, , m_nHeightDiff(i)
        //    Next
        //    '*****************************************************************************
        //    
        //End Sub
        //'*******************************************************************************
        //'*******************************************************************************




        //'読み込み。
        //'
        //'引き数：
        //'nFile ファイル番号。
        //'nVersion ファイルバージョン。
        //Public Sub Load(ByVal nFile As Integer, ByVal nVersion As Long)
        //
        //    FromNumber = GetString(nFile)
        //    FromName = GetString(nFile)
        //    ToNumber = GetString(nFile)
        //    ToName = GetString(nFile)
        //    FixedNumber = GetString(nFile)
        //    FixedName = GetString(nFile)
        //
        //
        //    If nVersion< 6400 Then
        //        Dim clsCoordinatePoint As New CoordinatePointXYZ
        //        Call clsCoordinatePoint.Load(nFile, nVersion)
        //        Let m_clsFromCoordinate = clsCoordinatePoint
        //        Call clsCoordinatePoint.Load(nFile, nVersion)
        //        Let m_clsToCoordinate = clsCoordinatePoint
        //        Call clsCoordinatePoint.Load(nFile, nVersion)
        //        Let m_clsFixedCoordinate = clsCoordinatePoint
        //    Else
        //        Call FromCoordinate.Load(nFile, nVersion)
        //        Call ToCoordinate.Load(nFile, nVersion)
        //        Call FixedCoordinate.Load(nFile, nVersion)
        //    End If
        //
        //    Dim nCount As Long
        //    Get #nFile, , nCount
        //    Count = nCount
        //    Dim i As Long
        //    For i = 0 To Count - 1
        //        StrNumber(i) = GetString(nFile)
        //        StrName(i) = GetString(nFile)
        //        EndNumber(i) = GetString(nFile)
        //        EndName(i) = GetString(nFile)
        //        Call Vector(i).Load(nFile, nVersion)
        //        Session(i) = GetString(nFile)
        //    Next
        //
        //    Call SumXYZ.Load(nFile, nVersion)
        //    Call ResultXYZ.Load(nFile, nVersion)
        //    Call ResultNEU.Load(nFile, nVersion)
        //
        //    If nVersion< 3200 Then
        //        Dim clsPermission As New CoordinatePointXYZ
        //        Call clsPermission.Load(nFile, nVersion)
        //    End If
        //    
        //    '2023/05/19 Hitz H.Nakamura **************************************************
        //    '楕円体高の閉合差を追加。
        //    If nVersion >= 9700 Then
        //        Dim nTemp As Double
        //        Get #nFile, , SumHeight
        //        Get #nFile, , ResultHeight
        //        Get #nFile, , PermissionHeight
        //        
        //        For i = 0 To Count - 1
        //            Get #nFile, , m_nHeightEnd(i)
        //            Get #nFile, , m_nHeightDiff(i)
        //        Next
        //    End If
        //    '*****************************************************************************
        //
        //
        //End Sub

#if false
'指定されたオブジェクトと比較する。
'
'引き数：
'clsAngleDiffResult 比較対照オブジェクト。
'
'戻り値：
'一致する場合 True を返す。
'それ以外の場合 False を返す。
Public Function Compare(ByVal clsAngleDiffResult As AngleDiffResult) As Boolean

    Compare = False

    If StrComp(FromNumber, clsAngleDiffResult.FromNumber) <> 0 Then Exit Function
    If StrComp(FromName, clsAngleDiffResult.FromName) <> 0 Then Exit Function
    If StrComp(ToNumber, clsAngleDiffResult.ToNumber) <> 0 Then Exit Function
    If StrComp(ToName, clsAngleDiffResult.ToName) <> 0 Then Exit Function
    If StrComp(FixedNumber, clsAngleDiffResult.FixedNumber) <> 0 Then Exit Function
    If StrComp(FixedName, clsAngleDiffResult.FixedName) <> 0 Then Exit Function
    If Not FromCoordinate.Compare(clsAngleDiffResult.FromCoordinate) Then Exit Function
    If Not ToCoordinate.Compare(clsAngleDiffResult.ToCoordinate) Then Exit Function
    If Not FixedCoordinate.Compare(clsAngleDiffResult.FixedCoordinate) Then Exit Function
    If Count<> clsAngleDiffResult.Count Then Exit Function

    Dim i As Long
    For i = 0 To Count - 1
        If StrComp(StrNumber(i), clsAngleDiffResult.StrNumber(i)) <> 0 Then Exit Function
        If StrComp(StrName(i), clsAngleDiffResult.StrName(i)) <> 0 Then Exit Function
        If StrComp(EndNumber(i), clsAngleDiffResult.EndNumber(i)) <> 0 Then Exit Function
        If StrComp(EndName(i), clsAngleDiffResult.EndName(i)) <> 0 Then Exit Function
        If Not Vector(i).Compare(clsAngleDiffResult.Vector(i)) Then Exit Function
        If StrComp(Session(i), clsAngleDiffResult.Session(i)) <> 0 Then Exit Function
    Next


    If Not SumXYZ.Compare(clsAngleDiffResult.SumXYZ) Then Exit Function
    If Not ResultXYZ.Compare(clsAngleDiffResult.ResultXYZ) Then Exit Function
    If Not ResultNEU.Compare(clsAngleDiffResult.ResultNEU) Then Exit Function
    
    '2023/05/19 Hitz H.Nakamura **************************************************
    '楕円体高の閉合差を追加。
    If Abs(SumHeight - clsAngleDiffResult.SumHeight) >= FLT_EPSILON Then Exit Function
    If Abs(ResultHeight - clsAngleDiffResult.ResultHeight) >= FLT_EPSILON Then Exit Function
    If Abs(PermissionHeight - clsAngleDiffResult.PermissionHeight) >= FLT_EPSILON Then Exit Function


    For i = 0 To Count - 1
        If Abs(HeightEnd(i) - clsAngleDiffResult.HeightEnd(i)) >= FLT_EPSILON Then Exit Function
        If Abs(HeightDiff(i) - clsAngleDiffResult.HeightDiff(i)) >= FLT_EPSILON Then Exit Function
    Next
    '*****************************************************************************
    
    Compare = True

End Function
#endif



    }
}
