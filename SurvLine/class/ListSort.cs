using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace SurvLine
{
    public class ListSort
    {
        //==========================================================================================
        /*[VB]
        '*******************************************************************************
        'リストソート
        '
        'リストのソートオーダーを操作する。

        Option Explicit
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'インプリメンテーション
        Private m_nCol() As Long 'カラム番号。ソートキーとして優先度の高い順に並ぶ。配列の要素は(-1 To ...)、要素 -1 は未使用。
        Private m_bAscending() As Boolean '昇順フラグ。True=昇順。False=降順。配列の要素は(-1 To ...)、要素 -1 は未使用。
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'インプリメンテーション
        private long[] m_nCol;          //'カラム番号。ソートキーとして優先度の高い順に並ぶ。配列の要素は(-1 To ...)、要素 -1 は未使用。
        private bool[] m_bAscending;    //'昇順フラグ。True=昇順。False=降順。配列の要素は(-1 To ...)、要素 -1 は未使用。
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '*******************************************************************************
        'プロパティ

        'カラム番号。
        Property Get Col(ByVal nIndex) As Long
            Col = m_nCol(nIndex)
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '*******************************************************************************
        'プロパティ

        'カラム番号。
        */
        public long Col(int nIndex)
        {
            return m_nCol[nIndex];
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '昇順フラグ。
        Property Let Ascending(ByVal nIndex, ByVal bAscending As Boolean)
            m_bAscending(nIndex) = bAscending
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'昇順フラグ。
        public void Ascending(int nIndex, bool bAscending)
        {
            m_bAscending[nIndex] = bAscending;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '昇順フラグ。
        Property Get Ascending(ByVal nIndex) As Boolean
            Ascending = m_bAscending(nIndex)
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'昇順フラグ。
        public bool Ascending(int nIndex)
        {
            return m_bAscending[nIndex];
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'カラム数。
        Property Get Cols() As Long
            Cols = UBound(m_nCol) + 1
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'カラム数。
        public long Cols()
        {
            return m_nCol.Length;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '*******************************************************************************
        'メソッド

        '初期化。
        '
        '引き数：
        'nCols カラム数。
        Public Sub Initialize(ByVal nCols As Long)
            ReDim m_nCol(-1 To nCols - 1)
            ReDim m_bAscending(-1 To nCols - 1)
            Dim i As Long
            For i = 0 To nCols - 1
                m_nCol(i) = i
                m_bAscending(i) = True
            Next
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '*******************************************************************************
        'メソッド

        '初期化。
        '
        '引き数：
        'nCols カラム数。
        */
        public void Initialize(long nCols)
        {
            m_nCol = new long[nCols];
            m_bAscending = new bool[nCols];
            long i;
            for (i = 0; i < nCols; i++)
            {
                m_nCol[i] = i;
                m_bAscending[i] = true;
            }
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'ソートオーダー入れ替え。
        '
        '指定されたカラムの優先順位を一番高くする。
        '
        '引き数：
        'nCol カラム。
        'bAscending 昇順フラグ。True=昇順。False=降順。
        Public Sub ReplaceSortOrder(ByVal nCol As Long, Optional ByVal bAscending As Boolean = True)
            '順番入れ替え。
            Dim i As Long
            For i = UBound(m_nCol) To 0 Step -1
                If m_nCol(i) = nCol Then Exit For
            Next
            For i = i To 1 Step -1
                m_nCol(i) = m_nCol(i - 1)
                m_bAscending(i) = m_bAscending(i - 1)
            Next
            m_nCol(i) = nCol
            m_bAscending(i) = bAscending
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        'ソートオーダー入れ替え。
        '
        '指定されたカラムの優先順位を一番高くする。
        '
        '引き数：
        'nCol カラム。
        'bAscending 昇順フラグ。True=昇順。False=降順。
        */
        public void ReplaceSortOrder(long nCol, bool bAscending = true)
        {
            //'順番入れ替え。
            long i;
            long ii;
            for (i = m_nCol.Length - 1; i > 0; i--)
            {
                if (m_nCol[i] == nCol)
                {
                    break;
                }
            }
            for (ii = i; ii > 1; ii--)
            {
                m_nCol[ii] = m_nCol[ii - 1];
                m_bAscending[ii] = m_bAscending[ii - 1];
            }
            m_nCol[ii] = nCol;
            m_bAscending[ii] = bAscending;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'ソートオーダー入れ替え。
        '
        '指定されたカラムの優先順位を一番低くする。
        '
        '引き数：
        'nCol カラム。
        'bAscending 昇順フラグ。True=昇順。False=降順。
        Public Sub ReplaceSortOrderBehind(ByVal nCol As Long, Optional ByVal bAscending As Boolean = True)
            '順番入れ替え。
            Dim i As Long
            For i = 0 To UBound(m_nCol)
                If m_nCol(i) = nCol Then Exit For
            Next
            For i = i To UBound(m_nCol) - 1
                m_nCol(i) = m_nCol(i + 1)
                m_bAscending(i) = m_bAscending(i + 1)
            Next
            m_nCol(i) = nCol
            m_bAscending(i) = bAscending
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        'ソートオーダー入れ替え。
        '
        '指定されたカラムの優先順位を一番低くする。
        '
        '引き数：
        'nCol カラム。
        'bAscending 昇順フラグ。True=昇順。False=降順。
        */
        public void ReplaceSortOrderBehind(long nCol, bool bAscending = true)
        {
            //'順番入れ替え。
            long i;
            long ii;
            for (i = 0; i < m_nCol.Length; i++)
            {
                if (m_nCol[i] == nCol)
                {
                    break;
                }
            }
            for (ii = i; ii < m_nCol.Length; ii++)
            {
                m_nCol[i] = m_nCol[i + 1];
                m_bAscending[i] = m_bAscending[i + 1];
            }
            m_nCol[i] = nCol;
            m_bAscending[i] = bAscending;
        }
        //==========================================================================================
    }
}
