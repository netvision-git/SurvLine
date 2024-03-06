using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using System.Windows.Forms;
using System.Xml.Linq;
using System.IO;
using System.Diagnostics.Eventing.Reader;
using SurvLine.mdl;
using static SurvLine.mdl.DEFINE;
using static SurvLine.mdl.MdlNSDefine;
using static SurvLine.mdl.MdlUtility;
using static SurvLine.mdl.MdlAccountMake;
using static SurvLine.mdl.MdlNSGUI;
using static SurvLine.mdl.MdlNSSDefine;

namespace SurvLine
{
    internal class AngleDiff
    {

        //'*******************************************************************************
        //'閉合差
        //'
        //'閉合差の状態を保持する。
        //
        //Option Explicit
        //

        //'プロパティ
        public string Number;   // As String 'No。
        public string Name;     // As String '名称。
        public bool Ring;       // As Boolean '環フラグ。TRUE=環閉合、FALSE=電子基準点間閉合。
        public bool Exceed;     // As Boolean '制限超過フラグ。

        //'インプリメンテーション
        private List<string> m_sRegistVectors;          //private string m_sRegistVectors() As String '登録基線ベクトル。配列の要素は(-1 To ...)、要素 -1 は未使用。
        //-----------------------------------------------
        private AngleDiffResult m_clsAngleDiffResult;   //As AngleDiffResult '閉合差点検結果。
        //-----------------------------------------------
        List<bool> m_bAccount;                          // private bool m_bAccount As Boolean '帳票選択。帳票出力のチェックON/OFFを記憶する。インデックス 1 以上は地籍などの拡張帳票用。
        //-----------------------------------------------
        private KnownPoint m_clsKnownPoint;             //  private m_clsKnownPoint As New KnownPoint '既知点。


        //'*******************************************************************************
        //'プロパティ
        //'*******************************************************************************

        public AngleDiff()
        {
            Class_Initialize();     //InitializeComponent();
        }



        //'*******************************************************************************
        //'*******************************************************************************
        /// <summary>
        /// このプロパティはデフォルトプロパティである。Letを使えば｢値の代入｣ができる。いわゆるC++で言うところのoperator=である。
        ///
        /// 引き数：
        ///     clsAngleDiff コピー元のオブジェクト。
        /// </summary>
        /// <param name="clsAngleDiff"></param>
        public void AngleDiff_A(object clsAngleDiffA)
        {
            AngleDiff clsAngleDiff = (AngleDiff)clsAngleDiffA;

            Number = clsAngleDiff.Number;
            Name = clsAngleDiff.Name;
            Ring = clsAngleDiff.Ring;
            //    m_sRegistVectors = clsAngleDiff.RegistVectors;

            //    if (clsAngleDiff.AngleDiffResult != null){
            //       m_clsAngleDiffResult = null;
            //   }
            //    else
            //    {
            //       if (m_clsAngleDiffResult != null){
            //            m_clsAngleDiffResult = new AngleDiffResult();
            //            m_clsAngleDiffResult = clsAngleDiff.AngleDiffResult;
            //        }
            //    }
            for (int i = 0; i < m_bAccount.Count; i++)
            {
                //       m_bAccount[i] = clsAngleDiff.Account[i];
            }


        }
        //--------------------------------------------------------------------------------
        //'
        //'このプロパティはデフォルトプロパティである。Letを使えば｢値の代入｣ができる。いわゆるC++で言うところのoperator=である。
        //'
        //'引き数：
        //'clsAngleDiff コピー元のオブジェクト。
        //Property Let AngleDiff(ByVal clsAngleDiff As AngleDiff)
        //
        //    Number = clsAngleDiff.Number
        //    Name = clsAngleDiff.Name
        //    Ring = clsAngleDiff.Ring
        //    m_sRegistVectors = clsAngleDiff.RegistVectors
        //    Let m_clsKnownPoint = clsAngleDiff.KnownPoint
        //
        //
        //    If clsAngleDiff.AngleDiffResult Is Nothing Then
        //        Set m_clsAngleDiffResult = Nothing
        //    Else
        //        If m_clsAngleDiffResult Is Nothing Then Set m_clsAngleDiffResult = New AngleDiffResult
        //        Let m_clsAngleDiffResult = clsAngleDiff.AngleDiffResult
        //    End If
        //
        //
        //    Dim i As Long
        //    For i = 0 To UBound(m_bAccount)
        //        m_bAccount(i) = clsAngleDiff.Account(i)
        //    Next
        //
        //End Property
        //'*******************************************************************************


        //'*******************************************************************************
        //'*******************************************************************************
        /// <summary>
        /// 登録基線ベクトル。配列の要素は(-1 To ...)、要素 -1 は未使用。
        /// </summary>
        /// <param name="sRegistVectors"></param>
        public void RegistVectors(List<string> sRegistVectors)
        {
            m_sRegistVectors = sRegistVectors;
        }
        //'登録基線ベクトル。配列の要素は(-1 To ...)、要素 -1 は未使用。
        //Property Let RegistVectors(ByRef sRegistVectors() As String)
        //    m_sRegistVectors = sRegistVectors
        //End Property
        //'*******************************************************************************


        //'*******************************************************************************
        //'*******************************************************************************
        /// <summary>
        /// 登録基線ベクトル。配列の要素は(-1 To ...)、要素 -1 は未使用。
        /// </summary>
        /// <returns></returns>
        public string RegistVectors()
        {
            string RegistVectors = m_sRegistVectors.ToString();
            return RegistVectors;
        }
        //'登録基線ベクトル。配列の要素は(-1 To ...)、要素 -1 は未使用。
        //Property Get RegistVectors() As String()
        //    RegistVectors = m_sRegistVectors
        //End Property
        //'*******************************************************************************

        //'*******************************************************************************
        //'*******************************************************************************
        /// <summary>
        /// 閉合差点検結果。
        /// </summary>
        /// <param name="clsAngleDiffResult"></param>
        public void AngleDiffResult(AngleDiffResult clsAngleDiffResult)
        {
            m_clsAngleDiffResult = clsAngleDiffResult;
        }
        //--------------------------------------------------------------------------------
        //'閉合差点検結果。
        //Property Set AngleDiffResult(ByVal clsAngleDiffResult As AngleDiffResult)
        //    Set m_clsAngleDiffResult = clsAngleDiffResult
        //End Property
        //'*******************************************************************************


        //'*******************************************************************************
        //'*******************************************************************************
        /// <summary>
        /// 閉合差点検結果。
        /// </summary>
        /// <returns></returns>
        public AngleDiffResult AngleDiffResult()
        {
            AngleDiffResult AngleDiffResult = m_clsAngleDiffResult;
            return AngleDiffResult;
        }
        //--------------------------------------------------------------------------------
        //'閉合差点検結果。
        //Property Get AngleDiffResult() As AngleDiffResult
        //    Set AngleDiffResult = m_clsAngleDiffResult
        //End Property
        //'*******************************************************************************

        //'*******************************************************************************
        //'*******************************************************************************
        /// <summary>
        /// 帳票選択。
        /// </summary>
        /// <param name="nIndex"></param>
        /// <param name="bAccount"></param>
        public void Account(long nIndex, bool bAccount)
        {
            //  nIndex = 0;
            m_bAccount[(int)nIndex] = bAccount;
        }
        //--------------------------------------------------------------------------------
        //'帳票選択。
        //Property Let Account(Optional ByVal nIndex As Long = 0, ByVal bAccount As Boolean)
        //    m_bAccount(nIndex) = bAccount
        //End Property
        //'*******************************************************************************


        //'*******************************************************************************
        //'*******************************************************************************
        /// <summary>
        //'帳票選択。
        /// </summary>
        /// <param name="nIndex"></param>
        /// <returns></returns>
        public bool Account(long nIndex)
        {
            bool Account = m_bAccount[(int)nIndex];
            return Account;
        }
        //--------------------------------------------------------------------------------
        //'帳票選択。
        //Property Get Account(Optional ByVal nIndex As Long = 0) As Boolean
        //    Account = m_bAccount(nIndex)
        //End Property
        //'*******************************************************************************


        //'*******************************************************************************
        //'*******************************************************************************
        /// <summary>
        /// 既知点。
        /// </summary>
        /// <param name="clsKnownPoint"></param>
        public void KnownPoint(KnownPoint clsKnownPoint)
        {
            m_clsKnownPoint = clsKnownPoint;
        }
        //--------------------------------------------------------------------------------
        //'既知点。
        //Property Let KnownPoint(ByVal clsKnownPoint As KnownPoint)
        //    Let m_clsKnownPoint = clsKnownPoint
        //End Property
        //'*******************************************************************************


        //'*******************************************************************************
        //'*******************************************************************************
        /// <summary>
        /// 既知点。
        /// </summary>
        /// <returns></returns>
        public KnownPoint KnownPoint()
        {
            return m_clsKnownPoint;
        }
        //'既知点。
        //Property Get KnownPoint() As KnownPoint
        //    Set KnownPoint = m_clsKnownPoint
        //End Property
        //'*******************************************************************************



        //<<<<<<<<<-----------24/01/04 K.setoguchi@NV



        private void Class_Initialize()
        {
            KnownPoint knownPointA = new KnownPoint();

            m_clsKnownPoint = knownPointA;

            // m_clsKnownPoint = new KnownPoint(); //'既知点。

            m_clsKnownPoint.Coordinate = false;


            //    ReDim m_sRegistVector(-1 To - 1)
        }
        //------------------------------------------------------------------------------
        //'イベント
        //
        //'初期化。
        //Private Sub Class_Initialize()
        //    m_clsKnownPoint.Coordinate = False
        //    ReDim m_sRegistVector(-1 To -1)
        //End Sub
        //'*******************************************************************************
        //'*******************************************************************************


        //'*******************************************************************************
        //'*******************************************************************************
        /// <summary>
        /// 'メソッド
        //
        /// '保存。
        /// '
        /// '引き数：
        ///     bw バイナリファイル
        /// 
        /// </summary>
        /// <param name="bw"></param>
        public void Save(BinaryWriter bw)
        {

            FileWrite_PutString(bw, Number); //    Call PutString(nFile, Number)
            FileWrite_PutString(bw, Name);   //    Call PutString(nFile, Name)

            KnownPoint m_clsKnownPoint = new KnownPoint();

            m_clsKnownPoint.Save(bw);                   //    Call m_clsKnownPoint.Save(nFile)

            PutFileBool(bw, Ring);           //    Put #nFile, , Ring



        }
        //
        //'保存。
        //'
        //'引き数：
        //'nFile ファイル番号。
        //Public Sub Save(ByVal nFile As Integer)
        //
        //    Call PutString(nFile, Number)
        //    Call PutString(nFile, Name)
        //    Call m_clsKnownPoint.Save(nFile)
        //    Put #nFile, , Ring
        //    
        //    Dim i As Long
        //    For i = 0 To UBound(m_bAccount)
        //        Put #nFile, , m_bAccount(i)
        //    Next
        //
        //
        //    Put #nFile, , UBound(m_sRegistVectors)
        //    For i = 0 To UBound(m_sRegistVectors)
        //        Call PutString(nFile, m_sRegistVectors(i))
        //    Next
        //
        //    Put #nFile, , Not m_clsAngleDiffResult Is Nothing
        //    If Not m_clsAngleDiffResult Is Nothing Then Call m_clsAngleDiffResult.Save(nFile)
        //
        //
        //End Sub
        //'*******************************************************************************


        //'読み込み。
        //'
        //'引き数：
        //'nFile ファイル番号。
        //'nVersion ファイルバージョン。
        //Public Sub Load(ByVal nFile As Integer, ByVal nVersion As Long)
        //
        //    Number = GetString(nFile)
        //    Name = GetString(nFile)
        //
        //
        //    If nVersion< 3200 Then
        //        Dim sFixed As String
        //        sFixed = GetString(nFile)
        //        m_clsKnownPoint.Coordinate = False
        //        m_clsKnownPoint.Number = ""
        //    Else
        //        Call m_clsKnownPoint.Load(nFile, nVersion)
        //    End If
        //
        //    Get #nFile, , Ring
        //    
        //    If nVersion < 2400 Then
        //        Get #nFile, , m_bAccount(0)
        //        Dim i As Long
        //        For i = 1 To UBound(m_bAccount)
        //            m_bAccount(i) = False
        //        Next
        //    ElseIf nVersion< 2600 Then
        //        For i = 0 To 1
        //            Get #nFile, , m_bAccount(i)
        //        Next
        //        For i = 2 To UBound(m_bAccount)
        //            m_bAccount(i) = False
        //        Next
        //    ElseIf nVersion< 5300 Then
        //        For i = 0 To 2
        //            Get #nFile, , m_bAccount(i)
        //        Next
        //        For i = 3 To UBound(m_bAccount)
        //            m_bAccount(i) = False
        //        Next
        //    Else
        //        For i = 0 To UBound(m_bAccount)
        //            Get #nFile, , m_bAccount(i)
        //        Next
        //    End If
        //
        //    Dim nCount As Long
        //    Get #nFile, , nCount
        //    ReDim m_sRegistVectors(-1 To nCount)
        //    For i = 0 To nCount
        //        m_sRegistVectors(i) = GetString(nFile)
        //    Next
        //    If nVersion< 1200 Then ReDim m_sRegistVectors(-1 To -1)
        //    
        //    Dim bAngleDiffResult As Boolean
        //    Get #nFile, , bAngleDiffResult
        //    If bAngleDiffResult Then
        //        Set m_clsAngleDiffResult = New AngleDiffResult
        //        Call m_clsAngleDiffResult.Load(nFile, nVersion)
        //    Else
        //        Set m_clsAngleDiffResult = Nothing
        //    End If
        //
        //
        //End Sub


        //'指定されたオブジェクトと比較する。
        //'
        //'引き数：
        //'clsAngleDiff 比較対照オブジェクト。
        //'
        //'戻り値：
        //'一致する場合 True を返す。
        //'それ以外の場合 False を返す。
        //Public Function Compare(ByVal clsAngleDiff As AngleDiff) As Boolean
        //
        //    Compare = False
        //
        //    If StrComp(Number, clsAngleDiff.Number) <> 0 Then Exit Function
        //    If StrComp(Name, clsAngleDiff.Name) <> 0 Then Exit Function
        //    If Ring<> clsAngleDiff.Ring Then Exit Function
        //
        //    If Not m_clsKnownPoint.Compare(clsAngleDiff.KnownPoint) Then Exit Function
        //
        //
        //    Dim i As Long
        //    For i = 0 To UBound(m_bAccount)
        //        If m_bAccount(i) <> clsAngleDiff.Account(i) Then Exit Function
        //    Next
        //
        //    Dim sRegistVectors() As String
        //    sRegistVectors = clsAngleDiff.RegistVectors
        //    If UBound(m_sRegistVectors) <> UBound(sRegistVectors) Then Exit Function
        //    For i = 0 To UBound(m_sRegistVectors)
        //        If StrComp(m_sRegistVectors(i), sRegistVectors(i)) <> 0 Then Exit Function
        //    Next
        //
        //
        //    If(m_clsAngleDiffResult Is Nothing) <> (clsAngleDiff.AngleDiffResult Is Nothing) Then Exit Function
        //    If Not m_clsAngleDiffResult Is Nothing Then
        //        If Not m_clsAngleDiffResult.Compare(clsAngleDiff.AngleDiffResult) Then Exit Function
        //    End If
        //
        //    Compare = True
        //
        //
        //End Function
    }
}
