using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;
using System.Windows.Forms;
using static SurvLine.ObservationPointKey_NSO;

namespace SurvLine.mdl
{
    internal class MdlListProc
    {
        //==========================================================================================
        /*[VB]
        '*******************************************************************************
        'リスト画面プロシージャ

        Option Explicit
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //==========================================================================================

        //==========================================================================================
        //[C#]
        private MdlMain m_clsMdlMain;                                   //MdlMainインスタンス
        private MdlListPane m_clsMdlListPane;

        public MdlListProc(MdlMain clsMdlMain, MdlListPane clsMdlListPane)
        {
            m_clsMdlMain = clsMdlMain;
            m_clsMdlListPane = clsMdlListPane;
            return;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '観測点リストを再作成する。
        '
        'リストの行を設定する。
        '
        '引き数：
        'nList リスト種別。
        'grdFlexGrid リストコントロール。
        'objMap 要素マップ。要素はオブジェクト。キーは要素のポインタ(リストの RowData に設定する)。
        Public Sub RemakeListObsPnt(ByVal nList As Long, ByVal grdFlexGrid As NSFlexGrid, ByRef objMap As Collection)

            Dim objElements() As Object
            ReDim objElements(-1 To GetDocument().NetworkModel.RepresentPointCount - 1)
    
            Dim nRows As Long
            nRows = 0
            Dim clsChainList As ChainList
            Set clsChainList = GetDocument().NetworkModel.RepresentPointHead
            Do While Not clsChainList Is Nothing
                If clsChainList.Element.VisibleList Then
                    Set objElements(nRows) = clsChainList.Element
                    nRows = nRows + 1
                End If
                Set clsChainList = clsChainList.NextList
            Loop
    
            ReDim Preserve objElements(-1 To nRows - 1)
    
            Call RemakeListProc(nList, grdFlexGrid, objMap, objElements)
    
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '観測点リストを再作成する。
        '
        'リストの行を設定する。
        '
        '引き数：
        'nList リスト種別。
        'grdFlexGrid リストコントロール。
        'objMap 要素マップ。要素はオブジェクト。キーは要素のポインタ(リストの RowData に設定する)。
        */
        public void RemakeListObsPnt(long nList, DataGridView grdFlexGrid, ref Dictionary<string, object> objMap)
        {
            object[] objElements = new object[m_clsMdlMain.GetDocument().NetworkModel().RepresentPointCount()];

            long nRows;
            nRows = 0;
            ChainList clsChainList;
            clsChainList = m_clsMdlMain.GetDocument().NetworkModel().RepresentPointHead();
            while (clsChainList != null)
            {
#if false
                if (clsChainList.Element.VisibleList())
                {
                    objElements[nRows] = clsChainList.Element;
                    nRows++;
                }
#else
                objElements[nRows] = clsChainList.Element;
                nRows++;
#endif
                clsChainList = clsChainList.NextList();
            }

            RemakeListProc(nList, grdFlexGrid, ref objMap, ref objElements);

        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '基線ベクトルリストを再作成する。
        '
        'リストの行を設定する。
        '
        '引き数：
        'nList リスト種別。
        'grdFlexGrid リストコントロール。
        'objMap 要素マップ。要素はオブジェクト。キーは要素のポインタ(リストの RowData に設定する)。
        Public Sub RemakeListVector(ByVal nList As Long, ByVal grdFlexGrid As NSFlexGrid, ByRef objMap As Collection)

            Dim objElements() As Object
            ReDim objElements(-1 To GetDocument().NetworkModel.BaseLineVectorCount - 1)
    
            Dim nRows As Long
            nRows = 0
            Dim clsChainList As ChainList
            Set clsChainList = GetDocument().NetworkModel.BaseLineVectorHead
            Do While Not clsChainList Is Nothing
                If clsChainList.Element.Valid And clsChainList.Element.VisibleList Then
                    Set objElements(nRows) = clsChainList.Element
                    nRows = nRows + 1
                End If
                Set clsChainList = clsChainList.NextList
            Loop
    
            ReDim Preserve objElements(-1 To nRows - 1)
    
            Call RemakeListProc(nList, grdFlexGrid, objMap, objElements)
    
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '基線ベクトルリストを再作成する。
        '
        'リストの行を設定する。
        '
        '引き数：
        'nList リスト種別。
        'grdFlexGrid リストコントロール。
        'objMap 要素マップ。要素はオブジェクト。キーは要素のポインタ(リストの RowData に設定する)。
        */
        public void RemakeListVector(long nList, DataGridView grdFlexGrid, ref Dictionary<string, object> objMap)
        {

            object[] objElements = new object[m_clsMdlMain.GetDocument().NetworkModel().BaseLineVectorCount()];

            long nRows;
            nRows = 0;
            ChainList clsChainList;
            clsChainList = m_clsMdlMain.GetDocument().NetworkModel().BaseLineVectorHead();
            while (clsChainList != null)
            {
#if false
                if (clsChainList.Element.Valid && clsChainList.Element.VisibleList)
                {
                    objElements[nRows] = clsChainList.Element;
                    nRows++;
                }
#else
                objElements[nRows] = clsChainList.Element;
                nRows++;
#endif
                clsChainList = clsChainList.NextList();
            }

            RemakeListProc(nList, grdFlexGrid, ref objMap, ref objElements);

        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'リストを再作成する。
        '
        'nList で指定されるリストの行を設定する。
        'objElements で指定されるオブジェクトでリストを作成する。
        '
        '引き数：
        'nList リスト種別。
        'grdFlexGrid リストコントロール。
        'objMap 要素マップ。要素はオブジェクト。キーは要素のポインタ(リストの RowData に設定する)。
        'objElements リストに追加するオブジェクト。参照する配列の要素は(0 To ...)。Ubound 関数を使用する。
        Public Sub RemakeListProc(ByVal nList As Long, ByVal grdFlexGrid As NSFlexGrid, ByRef objMap As Collection, ByRef objElements() As Object)

            '行数。
            Dim nRows As Long
            nRows = UBound(objElements) + 2
    
            If nRows < 2 Then
                '0行にはできないので、空の行を表示させる。
                grdFlexGrid.rows = nRows + 1
                grdFlexGrid.HighLight = flexHighlightNever
                Call ClearRowList(nList, grdFlexGrid, 1)
            Else
                grdFlexGrid.rows = nRows
                grdFlexGrid.HighLight = flexHighlightAlways
            End If
    
            'カレント行。
            grdFlexGrid.Row = 1
            grdFlexGrid.Col = 0
            grdFlexGrid.ColSel = grdFlexGrid.Cols - 1
    
            '値の設定。
            Dim i As Long
            For i = 0 To UBound(objElements)
                Call UpdateRowList(nList, grdFlexGrid, i + 1, objElements(i))
            Next
    
            '要素マップの設定。
            Dim nData As Long
            Set objMap = New Collection
            For i = 0 To UBound(objElements)
                nData = GetPointer(objElements(i))
                grdFlexGrid.RowData(i + 1) = nData
                Call objMap.Add(objElements(i), Hex$(nData))
            Next
    
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        'リストを再作成する。
        '
        'nList で指定されるリストの行を設定する。
        'objElements で指定されるオブジェクトでリストを作成する。
        '
        '引き数：
        'nList リスト種別。
        'grdFlexGrid リストコントロール。
        'objMap 要素マップ。要素はオブジェクト。キーは要素のポインタ(リストの RowData に設定する)。
        'objElements リストに追加するオブジェクト。参照する配列の要素は(0 To ...)。Ubound 関数を使用する。
        */
        public void RemakeListProc(long nList, DataGridView grdFlexGrid, ref Dictionary<string, object> objMap, ref object[] objElements)
        {
            long i;

            //'行数。
            long nRows = objElements.Length + 1;

            if (nRows < 2)
            {
                //'0行にはできないので、空の行を表示させる。
                //grdFlexGrid.Rows.Add();
                grdFlexGrid.RowCount = (int)nRows;
            }
            else
            {
                grdFlexGrid.RowCount = (int)nRows - 1;
            }

            //'カレント行。
            /*
            grdFlexGrid.Row = 1;
            grdFlexGrid.Col = 0;
            grdFlexGrid.ColSel = grdFlexGrid.Cols - 1;
            */

            //'値の設定。
            for (i = 0; i < objElements.Length; i++)
            {
#if true
                //行を作成
                m_clsMdlListPane.UpdateRowList(nList, grdFlexGrid, i, objElements[i]);
#endif
            }

            //'要素マップの設定。
            long nData;
            objMap = new Dictionary<string, object>();
            string vKey;
            for (i = 0; i < objElements.Length; i++)
            {
#if true
                vKey = CreateKey_NSO();
                objMap.Add(vKey, objElements[i]);
#endif
            }

        }
        //==========================================================================================
    }
}
