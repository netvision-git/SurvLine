using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using System.Windows.Forms;
using System.IO;

namespace SurvLine
{
    public class AngleDiffParam
    {
        //==========================================================================================
        /*[VB]
        '*******************************************************************************
        '閉合差パラメータ
        '
        '閉合差オブジェクトを保持する。

        Option Explicit
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'インプリメンテーション
        Private m_clsAngleDiffs() As AngleDiff '閉合差オブジェクトの配列。配列の要素は(-1 To ...)、要素 -1 は未使用。
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '*******************************************************************************
        'プロパティ

        '帳票パラメータ。
        '
        'このプロパティはデフォルトプロパティである。Letを使えば｢値の代入｣ができる。いわゆるC++で言うところのoperator=である。
        '
        '引き数：
        'clsAngleDiffParam コピー元のオブジェクト。
        Property Let AngleDiffParam(ByVal clsAngleDiffParam As AngleDiffParam)
            ReDim m_clsAngleDiffs(-1 To clsAngleDiffParam.Count - 1)
            Dim i As Long
            For i = 0 To UBound(m_clsAngleDiffs)
                Set m_clsAngleDiffs(i) = New AngleDiff
                Let m_clsAngleDiffs(i) = clsAngleDiffParam.AngleDiffs(i)
            Next
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '閉合差数。
        Property Get Count() As Long
            Count = UBound(m_clsAngleDiffs) + 1
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '閉合差。
        Property Get AngleDiffs(ByVal nIndex As Long) As AngleDiff
            Set AngleDiffs = m_clsAngleDiffs(nIndex)
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
            ReDim m_clsAngleDiffs(-1 To -1)
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

            Put #nFile, , UBound(m_clsAngleDiffs)
            Dim i As Long
            For i = 0 To UBound(m_clsAngleDiffs)
                Call m_clsAngleDiffs(i).Save(nFile)
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

            Dim nCount As Long
            Get #nFile, , nCount
            ReDim m_clsAngleDiffs(-1 To nCount)
            Dim i As Long
            For i = 0 To nCount
                Set m_clsAngleDiffs(i) = New AngleDiff
                Call m_clsAngleDiffs(i).Load(nFile, nVersion)
            Next

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
        public void Load(BinaryReader br, long nVersion)
        {
            return;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '閉合差を設定する。
        '
        '閉合差オブジェクトの配列を設定する。
        '閉合差オブジェクトは参照を設定するのではなく、コピーを作成して設定する。
        '
        '引き数：
        'clsAngleDiffs 設定する閉合差オブジェクトの配列。参照する配列の要素は(0 To ...)。Ubound 関数を使用する。
        Public Sub SetAngleDiffs(ByRef clsAngleDiffs() As AngleDiff)
            ReDim m_clsAngleDiffs(-1 To UBound(clsAngleDiffs))
            Dim i As Long
            For i = 0 To UBound(m_clsAngleDiffs)
                Set m_clsAngleDiffs(i) = New AngleDiff
                Let m_clsAngleDiffs(i) = clsAngleDiffs(i)
            Next
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '指定されたオブジェクトと比較する。
        '
        '引き数：
        'clsAngleDiffParam 比較対照オブジェクト。
        '
        '戻り値：
        '一致する場合 True を返す。
        'それ以外の場合 False を返す。
        Public Function Compare(ByVal clsAngleDiffParam As AngleDiffParam) As Boolean

            Compare = False

            If UBound(m_clsAngleDiffs) <> (clsAngleDiffParam.Count - 1) Then Exit Function
            Dim i As Long
            For i = 0 To UBound(m_clsAngleDiffs)
                If Not m_clsAngleDiffs(i).Compare(clsAngleDiffParam.AngleDiffs(i)) Then Exit Function
            Next


            Compare = True

        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '指定されたオブジェクトと比較する。
        '
        '引き数：
        'clsAngleDiffParam 比較対照オブジェクト。
        '
        '戻り値：
        '一致する場合 True を返す。
        'それ以外の場合 False を返す。
        */
        public bool Compare(AngleDiffParam clsAngleDiffParam)
        {
            return true;
        }
        //==========================================================================================
    }
}
