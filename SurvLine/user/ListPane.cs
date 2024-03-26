using Microsoft.VisualBasic.Logging;
using SurvLine.mdl;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static SurvLine.mdl.MdlListPane;
using static SurvLine.mdl.MdlUtility;
using static SurvLine.ObservationPointKey_NSO;
using System.Xml.Linq;
using Microsoft.VisualBasic.Devices;
using static System.Windows.Forms.AxHost;
using System.Reflection;
using System.Runtime.Remoting;

namespace SurvLine
{

    public partial class ListPane : UserControl
    {

#if false
        public MdlListPane m_clsmdilistPane = new MdlListPane();
#else
        public MdlListPane m_clsmdilistPane;
#endif
        private MdlMain m_clsMdlMain;

        public long m_list;

        //コンストラクタ
        public ListPane()
        {
            //m_objHideSelected = new Dictionary<string, object>[(int)TagLIST_OBJ_TYPE_INDEX.LIST_OBJ_TYPE_INDEX_COUNT];
            //m_clsListSort = new ListSort[(int)LIST_NUM_PANE.LIST_NUM_COUNT];

            InitializeComponent();

        }


        //==========================================================================================
        /*[VB]
        '*******************************************************************************
        'リストペイン
        '
        'リストの行はオブジェクト一つに対応する。
        'リストに追加されたオブジェクトは m_objMap に保持される。要素がオブジェクトであり、キーはオブジェクトのポインタ。
        'リストの RowData にキーであるオブジェクトのポインタを設定するので、行→オブジェクト、の検索は可能。

        Option Explicit
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '定数
        Private Const EDGE_MASK_HEIGHT As Long = 2  'エッジマスクの高さ(ピクセル)。
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'定数
        private const long EDGE_MASK_HEIGHT = 2;    //'エッジマスクの高さ(ピクセル)。
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'イベント
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '選択行変更イベント。
        '
        'リストの選択行が変化した時に発生する。
        '
        '引き数：
        'nList 対象とするリストの番号。
        Public Event ChangeSelRow(ByVal nList As Long)
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '右クリックイベント。
        '
        'リストの選択行が右クリックされたときに発生する。
        '
        '引き数：
        'nList 対象とするリストの番号。
        Public Event RightDown(ByVal nList As Long)
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'リストペインクリックイベント。
        '
        'タブがクリックされたときに発生する。
        'つまりページが切り替わったときに発生する。
        Public Event ListClick()
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '表示イベント。
        '
        'リストペインが表示されたときに発生する。
        Public Event Showed()
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'インプリメンテーション
        Private m_objMap() As Collection '要素マップ。リストの行の内容となったオブジェクトを保持するコレクション。要素はオブジェクト。キーは要素のポインタ(リストの RowData に設定されている)。配列の要素は(0 To LIST_NUM_COUNT - 1)。
        Private m_clsListSort(LIST_NUM_COUNT - 1) As ListSort 'リストソート。
        Private m_nSelectedRows() As Long '選択行。配列の要素は(-1 To ...)、要素 -1 は未使用。
        Private m_nSelectedList As Long '選択行取得リスト。
        Private m_bRowLock As Boolean '行ロック。
        Private m_objHideSelected(LIST_OBJ_TYPE_INDEX_COUNT - 1) As Collection '非表示リスト選択オブジェクトコレクション。要素はオブジェクト。キーは要素のポインタ。
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'インプリメンテーション
        private Dictionary<string, object>[] m_objMap;                  //'要素マップ。リストの行の内容となったオブジェクトを保持するコレクション。要素はオブジェクト。キーは要素のポインタ(リストの RowData に設定されている)。配列の要素は(0 To LIST_NUM_COUNT - 1)。
        private ListSort[] m_clsListSort;                               //'リストソート。
        private long[] m_nSelectedRows;                                 //'選択行。配列の要素は(-1 To ...)、要素 -1 は未使用。
        private long m_nSelectedList;                                   //'選択行取得リスト。
        private bool m_bRowLock;                                        //'行ロック。
        private Dictionary<string, object>[] m_objHideSelected;         //'非表示リスト選択オブジェクトコレクション。要素はオブジェクト。キーは要素のポインタ。
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '*******************************************************************************
        'プロパティ

        'カレントリスト。
        Property Get List() As Long
            List = objTab.Tab
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '*******************************************************************************
        'プロパティ

        'カレントリスト。
        */
        public long List()
        {
            return objTab.SelectedIndex;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'オブジェクト種別。
        Property Get ListObjType(ByVal nList As Long) As Long
            ListObjType = LIST_OBJ_TYPE(nList)
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'オブジェクト種別。
        public long ListObjType(long nList)
        {
            return m_clsmdilistPane.LIST_OBJ_TYPE(nList);
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '選択行数。
        Property Get SelectedCount(Optional ByVal nList As Long = -1) As Long
            If nList < 0 Or nList = objTab.Tab Then
                '要素が一つもない場合、HighLight が flexHighlightNever に設定されている。
                If grdFlexGrid(objTab.Tab).HighLight <> flexHighlightAlways Then
                    SelectedCount = 0
                Else
                    SelectedCount = grdFlexGrid(objTab.Tab).SelectedCount()
                End If
            Else
                SelectedCount = m_objHideSelected(LIST_OBJ_TYPE_INDEX(nList)).Count
            End If
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'選択行数。
        public long SelectedCount(long nList = -1)
        {
            DataGridView DG = GetDataGridView(nList);
            if (nList < 0 | nList == objTab.SelectedIndex)
            {
                //'要素が一つもない場合、HighLight が flexHighlightNever に設定されている。
                if (DG.SelectedRows.Count <= 0)
                {
                    return 0;
                }
                else
                {
                    return DG.SelectedRows.Count;
                }
            }
            else
            {
                return DG.SelectedRows.Count;
            }
        }

        public DataGridView GetDataGridView(long nList)
        {
            switch (nList)
            {
                case (long)TagLIST_OBJ_TYPE_INDEX.LIST_OBJ_TYPE_INDEX_OBSERVATIONPOINT:
                    return dataGridView1;
                case (long)TagLIST_OBJ_TYPE_INDEX.LIST_OBJ_TYPE_INDEX_ZAHYOU:
                    return dataGridView2;
                case (long)TagLIST_OBJ_TYPE_INDEX.LIST_OBJ_TYPE_INDEX_BASELINEVECTOR:
                    return dataGridView3;
                default:
                    return null;
            }
        }

        public DataGridView GetDataGridView()
        {
            switch (objTab.SelectedIndex)
            {
                case (int)TagLIST_OBJ_TYPE_INDEX.LIST_OBJ_TYPE_INDEX_OBSERVATIONPOINT:
                    return dataGridView1;
                case (int)TagLIST_OBJ_TYPE_INDEX.LIST_OBJ_TYPE_INDEX_ZAHYOU:
                    return dataGridView2;
                case (int)TagLIST_OBJ_TYPE_INDEX.LIST_OBJ_TYPE_INDEX_BASELINEVECTOR:
                    return dataGridView3;
                default:
                    return null;
            }
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '選択行を取得する。
        Property Get SelectedRow() As Long
            '要素が一つもない場合、HighLight が flexHighlightNever に設定されている。
            If grdFlexGrid(objTab.Tab).HighLight = flexHighlightAlways Then SelectedRow = grdFlexGrid(objTab.Tab).SelectedRow()
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'選択行を取得する。
        public long SelectedRow()
        {
            DataGridView DG = GetDataGridView();
            //'要素が一つもない場合、HighLight が flexHighlightNever に設定されている。
            foreach (DataGridViewRow row in DG.SelectedRows)
            {
                return row.Index;
            }
            return 0;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '選択要素を取得する。
        Property Get SelectedElement(Optional ByVal nList As Long = -1) As Object
            If nList < 0 Or nList = objTab.Tab Then
                '要素が一つもない場合、HighLight が flexHighlightNever に設定されている。
                If grdFlexGrid(objTab.Tab).HighLight <> flexHighlightAlways Then Exit Property
                Dim nRow As Long
                nRow = SelectedRow
                If nRow <= 0 Then Exit Property
                Dim nData As Long
                nData = grdFlexGrid(objTab.Tab).RowData(nRow)
                Set SelectedElement = m_objMap(objTab.Tab).Item(Hex$(nData))
            Else
                Dim objSelected As Collection
                Set objSelected = m_objHideSelected(LIST_OBJ_TYPE_INDEX(nList))
                If objSelected.Count <= 0 Then Exit Property
                Set SelectedElement = objSelected.Item(1)
            End If
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'選択要素を取得する。
        public object SelectedElement(long nList = -1)  //2
        {
            object[] SelectedElement = new object[(int)LIST_NUM_PANE.LIST_NUM_COUNT];

            long i;
            for (i = 0; i < (long)LIST_NUM_PANE.LIST_NUM_COUNT; i++)
            {
                SelectedElement[i] = SelectedElement2(i);
            }

            return SelectedElement[nList];
        }
        //------------------------------------------------------------------------------------------
        public object SelectedElement2(long nList = -1) //2
        {

            DataGridView DG = GetDataGridView(nList);

            object SelectedElement = null;

            SelectedElement = m_clsmdilistPane.SelectedElement(nList, DG, ref m_objMap[nList]);

            return SelectedElement;

#if false   //24/03/07(--)編集メニュー K.setoguchi@NV---------->>>>>>>>>>

            if (nList < 0 | nList == objTab.SelectedIndex)
            {
                //'要素が一つもない場合、HighLight が flexHighlightNever に設定されている。
                long nRow = SelectedRow();
                if (nRow <= 0)
                {
                    return null;
                }
#if false
                    long nData;
                    nData = grdFlexGrid(objTab.Tab).RowData(nRow);
                    return m_objMap(objTab.Tab).Item(Hex$(nData));
#else
                    return null;
#endif
                }
                else
                {
#if false
                    Dictionary<string, object> objSelected;
                    objSelected = m_objHideSelected(m_clsmdilistPane.LIST_OBJ_TYPE_INDEX(nList));
                    if (objSelected.Count <= 0)
                    {
                        return null;
                    }
                    return objSelected.Item(1);
#else
                    return null;
#endif
            }
#endif      //<<<<<<<<<-----------24/03/07(--)編集メニュー K.setoguchi@NV
        }
        //==========================================================================================


        public object EditValid(long nList = -1)  //2
        {
            object[] Elements = new object[(int)LIST_NUM_PANE.LIST_NUM_COUNT];

            long i;
            //for (i = 0; i < (long)LIST_NUM_PANE.LIST_NUM_COUNT; i++)
            for (i = 0; i < 1; i++)
            {
                Elements[i] = EditValid2(i);
            }

            return Elements[nList];
        }

        public object EditValid2(long nList = -1) //2
        {

            DataGridView DG = GetDataGridView(nList);

            object Element = null;

            Element = m_clsmdilistPane.EditValid(nList, DG, ref m_objMap[nList]);

            return Element;
        }

        //==========================================================================================
        /*[VB]
        '選択要素を取得する。配列の要素は(-1 To ...)、要素 -1 は未使用。
        Property Get SelectedElements(Optional ByVal nList As Long = -1) As Object()
            Dim objElements() As Object
            If nList < 0 Or nList = objTab.Tab Then
                '要素が一つもない場合、HighLight が flexHighlightNever に設定されている。
                If grdFlexGrid(objTab.Tab).HighLight <> flexHighlightAlways Then
                    ReDim objElements(-1 To -1)
                Else
                    Dim nRows() As Long
                    nRows = grdFlexGrid(objTab.Tab).SelectedRows()
                    ReDim objElements(-1 To UBound(nRows))
                    Dim nData As Long
                    Dim i As Long
                    For i = 0 To UBound(nRows)
                        nData = grdFlexGrid(objTab.Tab).RowData(nRows(i))
                        Set objElements(i) = m_objMap(objTab.Tab).Item(Hex$(nData))
                    Next
                End If
            Else
                Dim objSelected As Collection
                Set objSelected = m_objHideSelected(LIST_OBJ_TYPE_INDEX(nList))
                If objSelected.Count <= 0 Then
                    ReDim objElements(-1 To -1)
                Else
                    ReDim objElements(-1 To objSelected.Count - 1)
                    Dim objElement As Object
                    i = 0
                    For Each objElement In objSelected
                        Set objElements(i) = objElement
                        i = i + 1
                    Next
                End If
            End If
            SelectedElements = objElements
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'選択要素を取得する。配列の要素は(-1 To ...)、要素 -1 は未使用。
        public object[] SelectedElements(long nList = -1)
        {
            long nData;
            long i;
            long[] nRows;
            object[] objElements;
            DataGridView DG = GetDataGridView(nList);
            if (nList < 0 | nList == objTab.SelectedIndex)
            {
                //'要素が一つもない場合、HighLight が flexHighlightNever に設定されている。
                long nRow = SelectedRow();
                if (nRow <= 0)
                {
                    objElements = new object[0];
                }
                else
                {
#if false
                    nRows = grdFlexGrid(objTab.Tab).SelectedRows();
                    objElements = new object[nRows.Length];
                    for (i = 0; i < nRows.Length; i++)
                    {
                        nData = grdFlexGrid(objTab.Tab).RowData(nRows(i));
                        objElements[i] = m_objMap(objTab.Tab).Item(Hex$(nData));
                    }
#endif
                }
            }
            else
            {
#if false
                Dictionary<string, object> objSelected;
                objSelected = m_objHideSelected(LIST_OBJ_TYPE_INDEX(nList));
                if (objSelected.Count <= 0)
                {
                    objElements = new object[0];
                }
                else
                {
                    objElements = new object[objSelected.Count];
                    object objElement;
                    i = 0;
                    foreach (objElement In objSelected)
                    {
                        objElements[i] = objElement;
                        i = i + 1;
                    }
                }
#endif
            }
#if false
            return objElements;
#else
            return null;
#endif
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '行のロック。
        Property Let RowLock(ByVal bLock As Boolean)
            Static nRow(LIST_NUM_COUNT - 1) As Long
            Static nRowSel(LIST_NUM_COUNT - 1) As Long
            Static nTopRow(LIST_NUM_COUNT - 1) As Long
            If Not m_bRowLock And bLock Then
                'スクロールしないようにする。
                Dim i As Long
                For i = 0 To LIST_NUM_COUNT - 1
                    nRow(i) = grdFlexGrid(i).Row
                    nRowSel(i) = grdFlexGrid(i).RowSel
                    nTopRow(i) = grdFlexGrid(i).TopRow
                    'ちらつき防止。
                    grdFlexGrid(i).Redraw = False
                Next
                m_bRowLock = True
            ElseIf m_bRowLock And Not bLock Then
                'スクロールを元に戻す。
                For i = 0 To LIST_NUM_COUNT - 1
                    grdFlexGrid(i).TopRow = nTopRow(i)
                    grdFlexGrid(i).Row = nRow(i)
                    grdFlexGrid(i).RowSel = nRowSel(i)
                    grdFlexGrid(i).ColSel = grdFlexGrid(i).Cols - 1
                    'ちらつき防止。
                    grdFlexGrid(i).Redraw = True
                    grdFlexGrid(i).TopRow = nTopRow(i) '一番下までスクロールするにはRedrawをONにしないとだめらしい。一番下でなければ問題ないのだが。
                Next
                m_bRowLock = False
            End If
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'行のロック。
        public void RowLock(bool bLock)
        {
            long i;
            long[] nRow;
            nRow = new long[(int)LIST_NUM_PANE.LIST_NUM_COUNT];
            long[] nRowSel;
            nRowSel = new long[(int)LIST_NUM_PANE.LIST_NUM_COUNT];
            long[] nTopRow;
            nTopRow = new long[(int)LIST_NUM_PANE.LIST_NUM_COUNT];
            DataGridView DG = GetDataGridView();
            if (!m_bRowLock && bLock)
            {
                //'スクロールしないようにする。
                for (i = 0; i < (long)LIST_NUM_PANE.LIST_NUM_COUNT; i++)
                {
#if false
                    nRow[i] = grdFlexGrid(i).Row;
                    nRowSel[i] = grdFlexGrid(i).RowSel;
                    nTopRow[i] = grdFlexGrid(i).TopRow;
                    //'ちらつき防止。
                    grdFlexGrid(i).Redraw = false;
#endif
                }
                m_bRowLock = true;
            }
            else if (m_bRowLock && !bLock)
            {
                //'スクロールを元に戻す。
                for (i = 0; i < (long)LIST_NUM_PANE.LIST_NUM_COUNT; i++)
                {
#if false
                    grdFlexGrid(i).TopRow = nTopRow[i];
                    grdFlexGrid(i).Row = nRow[i];
                    grdFlexGrid(i).RowSel = nRowSel[i];
                    grdFlexGrid(i).ColSel = grd]lexGrid(i).Cols - 1;
                    //'ちらつき防止。
                    grdFlexGrid(i).Redraw = true;
                    grdFlexGrid(i).TopRow = nTopRow[i];     //'一番下までスクロールするにはRedrawをONにしないとだめらしい。一番下でなければ問題ないのだが。
#endif
                }
                m_bRowLock = false;
            }
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '行のロック。
        Property Get RowLock() As Boolean
            RowLock = m_bRowLock
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'行のロック。
        public bool RowLock()
        {
            return m_bRowLock;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'リストの表示。
        Property Let ListVisible(ByVal nList As Long, ByVal bVisible As Boolean)
            objTab.TabVisible(nList) = bVisible
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'リストの表示。
        public void ListVisible(long nList, bool bVisible)
        {
            if (bVisible)
            {
                switch (nList)
                {
                    case 0:
                        objTab.TabPages.Insert((int)nList, tabPage1);
                        break;
                    case 1:
                        objTab.TabPages.Insert((int)nList, tabPage2);
                        break;
                    case 2:
                        objTab.TabPages.Insert((int)nList, tabPage3);
                        break;
                    default:
                        break;
                }
            }
            else
            {
                switch (nList)
                {
                    case 0:
                        objTab.TabPages.Remove(tabPage1);
                        break;
                    case 1:
                        objTab.TabPages.Remove(tabPage2);
                        break;
                    case 2:
                        objTab.TabPages.Remove(tabPage3);
                        break;
                    default:
                        break;
                }

            }
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'リストの表示。
        Property Get ListVisible(ByVal nList As Long) As Boolean
            ListVisible = objTab.TabVisible(nList)
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'リストの表示。
        public bool ListVisible(long nList)
        {
            return true;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '*******************************************************************************
        'イベント

        '初期化。
        Private Sub UserControl_Initialize()

        '   ユーザーコントロールの中でユーザーコントロールをLoadすると、コンパイル時に実行時エラー7が発生する。
        '   コンパイル時にEndステートメントで終了するとexeファイルが生成されない。
        '   しょうがないのでエラーを無視する。
        '    On Error GoTo ErrorHandler
            On Error Resume Next
    
            '非表示リスト選択オブジェクトコレクション。
            Dim i As Long
            For i = 0 To LIST_OBJ_TYPE_INDEX_COUNT - 1
                Set m_objHideSelected(i) = New Collection
            Next
    
            'ソートオーダー。
            For i = 0 To LIST_NUM_COUNT - 1
                Set m_clsListSort(i) = New ListSort
            Next
            Call InitializeSortOrder(m_clsListSort)
    
            'タブコントロール。
            objTab.Left = ScaleLeft
            objTab.Top = ScaleTop
            objTab.TabsPerRow = LIST_NUM_COUNT
            objTab.Tabs = LIST_NUM_COUNT
    
            'リストのロード
            For i = 1 To LIST_NUM_COUNT - 1
                Load picEdgeMask(i)
                Load grdFlexGrid(i)
                picEdgeMask(i).Visible = True
                grdFlexGrid(i).Visible = True
            Next
    
            For i = 0 To LIST_NUM_COUNT - 1
                'エッジマスク。
                picEdgeMask(i).BorderStyle = vbBSNone
        '        picEdgeMask(i).Left = 0 'Left を設定するとタブ切り替えによる表示/非表示がうまく働かない・・・。リソースエディタで設定すること！
                picEdgeMask(i).Height = EDGE_MASK_HEIGHT * Screen.TwipsPerPixelY
                'グリッド。
        '        grdFlexGrid(i).Left = 0 'Left を設定するとタブ切り替えによる表示/非表示がうまく働かない・・・。リソースエディタで設定すること！
                grdFlexGrid(i).Top = 0
                grdFlexGrid(i).TabStop = False
            Next
    
            'タブストップ。
            grdFlexGrid(objTab.Tab).TabStop = True
    
            ReDim m_objMap(LIST_NUM_COUNT - 1)
    
            'リストの作成。
            For i = 0 To LIST_NUM_COUNT - 1
                Call MakeList(i, grdFlexGrid(i))
            Next
    
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
        public void UserControl_Initialize(MdlMain clsMdlMain)
        {
            /*
            '   ユーザーコントロールの中でユーザーコントロールをLoadすると、コンパイル時に実行時エラー7が発生する。
            '   コンパイル時にEndステートメントで終了するとexeファイルが生成されない。
            '   しょうがないのでエラーを無視する。
            '    On Error GoTo ErrorHandler
            */
            try
            {
                m_clsMdlMain = clsMdlMain;
                m_clsmdilistPane = new MdlListPane(clsMdlMain);

                //'非表示リスト選択オブジェクトコレクション。
                long i;
                m_objHideSelected = new Dictionary<string, object>[(long)TagLIST_OBJ_TYPE_INDEX.LIST_OBJ_TYPE_INDEX_COUNT];
                for (i = 0; i < (long)TagLIST_OBJ_TYPE_INDEX.LIST_OBJ_TYPE_INDEX_COUNT; i++)
                {
                    m_objHideSelected[i] = new Dictionary<string, object>();
                }

                //'ソートオーダー。
                m_clsListSort = new ListSort[(int)LIST_NUM_PANE.LIST_NUM_COUNT];
                for (i = 0; i < (long)LIST_NUM_PANE.LIST_NUM_COUNT; i++)
                {
                    m_clsListSort[i] = new ListSort();
                }
                m_clsmdilistPane.InitializeSortOrder(ref m_clsListSort);


                //'タブコントロール。
#if false
                objTab.Left = ScaleLeft;
                objTab.Top = ScaleTop;
                objTab.TabsPerRow = LIST_NUM_PANE.LIST_NUM_COUNT;
                objTab.Tabs = LIST_NUM_PANE.LIST_NUM_COUNT;
#endif

                //'リストのロード
                for (i = 1; i < (long)LIST_NUM_PANE.LIST_NUM_COUNT; i++)
                {
#if false
                    Load picEdgeMask(i);
                    Load grdFlexGrid(i);
                    picEdgeMask(i).Visible = true;
                    grdFlexGrid(i).Visible = true;
#endif
                }

                for (i = 0; i < (long)LIST_NUM_PANE.LIST_NUM_COUNT; i++)
                {
#if false
                    //'エッジマスク。
                    picEdgeMask(i).BorderStyle = vbBSNone;
                    //'        picEdgeMask(i).Left = 0 'Left を設定するとタブ切り替えによる表示/ 非表示がうまく働かない・・・。リソースエディタで設定すること！
                    picEdgeMask(i).Height = EDGE_MASK_HEIGHT * Screen.TwipsPerPixelY;
                    //'グリッド。
                    //'        grdFlexGrid(i).Left = 0 'Left を設定するとタブ切り替えによる表示/ 非表示がうまく働かない・・・。リソースエディタで設定すること！
                    grdFlexGrid(i).Top = 0;
                    grdFlexGrid(i).TabStop = false;
#endif
                }


                //'タブストップ。
#if false
                grdFlexGrid(objTab.Tab).TabStop = true;
#endif


                m_objMap = new Dictionary<string, object>[(int)LIST_NUM_PANE.LIST_NUM_COUNT];
                for (i = 0; i < (long)LIST_NUM_PANE.LIST_NUM_COUNT; i++)
                {
                    m_objMap[i] = new Dictionary<string, object>();
                }

                //'リストの作成。
#if false
                for (i = 0; i < (long)LIST_NUM_PANE.LIST_NUM_COUNT; i++)
                {
                    MakeList(i, grdFlexGrid(i));
                }
#else
                m_clsmdilistPane.MakeList((long)LIST_NUM_PANE.LIST_NUM_OBSPNT, dataGridView1);   //観測点。
                m_clsmdilistPane.MakeList((long)LIST_NUM_PANE.LIST_NUM_ZAHYOU, dataGridView2);   //座標。
                m_clsmdilistPane.MakeList((long)LIST_NUM_PANE.LIST_NUM_VECTOR, dataGridView3);   //ベクトル。
#endif
                return;
            }

            catch
            {
                m_clsMdlMain.ErrorExit();
                return;
            }


        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'リストをタブに配置。
        Private Sub UserControl_Show()

            On Error GoTo ErrorHandler
    
            Dim i As Long
            For i = LIST_NUM_COUNT - 1 To 0 Step -1
                objTab.Tab = i
                objTab.Caption = LIST_NAM(i)
                Set picEdgeMask(i).Container = objTab
                Set grdFlexGrid(i).Container = objTab
            Next
    
            RaiseEvent Showed
    
            Exit Sub
    
        ErrorHandler:
            Call mdlMain.ErrorExit
    
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'リストをタブに配置。
        private void UserControl_Show()
        {

            try
            {
                long i;
                for (i = (long)LIST_NUM_PANE.LIST_NUM_COUNT; i > 0; i--)
                {
#if false
                    objTab.Tab = i;
                    objTab.Caption = LIST_NAM(i);
                    Set picEdgeMask(i).Container = objTab;
                    Set grdFlexGrid(i).Container = objTab;
#endif
                }

#if false
                RaiseEvent Showed;
#endif
                return;
            }

            catch
            {
                m_clsMdlMain.ErrorExit();
                return;
            }

        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'リサイズ。
        Private Sub UserControl_Resize()

            On Error GoTo ErrorHandler
    
            'タブコントロール。
            objTab.Width = ScaleWidth
            objTab.Height = ScaleHeight
    
            'タブ内のリサイズ。
            Call ResizeTab(objTab.Tab)
    
            Exit Sub
    
        ErrorHandler:
            Call mdlMain.ErrorExit
    
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'リサイズ。
        private void UserControl_Resize()
        {
            try
            {
                //'タブコントロール。
#if false
                objTab.Width = ScaleWidth;
                objTab.Height = ScaleHeight;
#endif
                //'タブ内のリサイズ。
#if false
                ResizeTab(objTab.Tab);
#endif
                return;
            }

            catch
            {
                m_clsMdlMain.ErrorExit();
                return;
            }
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'ページ切り替え。
        Private Sub objTab_Click(PreviousTab As Integer)

            On Error GoTo ErrorHandler
    
            'リストクリックイベント。
            RaiseEvent ListClick
    
            'タブ内のリサイズ。
            Call ResizeTab(objTab.Tab)
    
            'タブストップ。
            grdFlexGrid(PreviousTab).TabStop = False
            grdFlexGrid(objTab.Tab).TabStop = True
    
            '前の選択行を記憶する。
            Dim objElements As New Collection
            '要素が一つもない場合、HighLight が flexHighlightNever に設定されている。
            If grdFlexGrid(PreviousTab).HighLight = flexHighlightAlways Then
                Dim nRows() As Long
                nRows = grdFlexGrid(PreviousTab).SelectedRows()
                Dim i As Long
                For i = 0 To UBound(nRows)
                    Dim sKey As String
                    sKey = Hex$(grdFlexGrid(PreviousTab).RowData(nRows(i)))
                    Call objElements.Add(m_objMap(PreviousTab).Item(sKey), sKey)
                Next
            End If
            Set m_objHideSelected(LIST_OBJ_TYPE_INDEX(PreviousTab)) = objElements
            '次の選択行の設定。
            Call SelectElementsByCollection(m_objHideSelected(LIST_OBJ_TYPE_INDEX(objTab.Tab)))
    
            Exit Sub
    
        ErrorHandler:
            Call mdlMain.ErrorExit
    
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'ページ切り替え。
        private void objTab_Click(int PreviousTab)
        {
            try
            {
#if false
                //'リストクリックイベント。
                RaiseEvent ListClick;

                //'タブ内のリサイズ。
                ResizeTab(objTab.Tab);

                //'タブストップ。
                grdFlexGrid(PreviousTab).TabStop = false;
                grdFlexGrid(objTab.Tab).TabStop = true;

                //'前の選択行を記憶する。
                Dictionary<string, object> objElements = new Dictionary<string, object>();
                //'要素が一つもない場合、HighLight が flexHighlightNever に設定されている。
                if (grdFlexGrid(PreviousTab).HighLight == flexHighlightAlways)
                {
                    long[] nRows;
                    nRows = grdFlexGrid(PreviousTab).SelectedRows();
                    long i;
                    for (i = 0; i < nRows.Length; i++)
                    {
                        string sKey;
                        sKey = Hex$(grdFlexGrid(PreviousTab).RowData(nRows(i)));
                        objElements.Add(m_objMap(PreviousTab).Item(sKey), sKey);
                    }
                }
                m_objHideSelected(LIST_OBJ_TYPE_INDEX(PreviousTab)) = objElements;
                //'次の選択行の設定。
                SelectElementsByCollection(m_objHideSelected(LIST_OBJ_TYPE_INDEX(objTab.Tab)));
#endif
                return;
            }

            catch
            {
                m_clsMdlMain.ErrorExit();
                return;
            }
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '選択行の変更。
        Private Sub grdFlexGrid_SelChange(Index As Integer, ByVal bCtrl As Boolean)

            On Error GoTo ErrorHandler
    
            If Not bCtrl Then
                '選択オブジェクトのクリア。
                Dim i As Long
                For i = 0 To LIST_OBJ_TYPE_INDEX_COUNT - 1
                    Set m_objHideSelected(i) = New Collection
                Next
            End If
    
            '選択行変更イベントを通知する。
            RaiseEvent ChangeSelRow(objTab.Tab)
    
            Exit Sub
    
        ErrorHandler:
            Call mdlMain.ErrorExit
    
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'選択行の変更。
        private void grdFlexGrid_SelChange(int Index, bool bCtrl)
        {
            try
            {
                if (!bCtrl)
                {
                    //'選択オブジェクトのクリア。
                    long i;
                    for (i = 0; i < (long)TagLIST_OBJ_TYPE_INDEX.LIST_OBJ_TYPE_INDEX_COUNT; i++)
                    {
                        m_objHideSelected = new Dictionary<string, object>[i];
                    }
                }
#if false
                //'選択行変更イベントを通知する。
                RaiseEvent ChangeSelRow(objTab.Tab);
#endif
                return;
            }

            catch
            {
                m_clsMdlMain.ErrorExit();
                return;
            }
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'クリック。
        Private Sub grdFlexGrid_MouseDown(Index As Integer, Button As Integer, Shift As Integer, X As Single, Y As Single)

            On Error GoTo ErrorHandler
    
            'リストクリックイベント。
            RaiseEvent ListClick
    
            '左ボタンならソート。
            If (Button And vbLeftButton) <> 0 Then
                '固定行でなければソートしない。
                If grdFlexGrid(Index).MouseRow > 0 Then Exit Sub
                'ソート条件。
                Dim nCol As Long
                nCol = grdFlexGrid(Index).MouseCol
                If m_clsListSort(Index).Col(0) = nCol Then
                    m_clsListSort(Index).Ascending(0) = Not m_clsListSort(Index).Ascending(0)
                Else
                    '順番入れ替え。
                    Call m_clsListSort(Index).ReplaceSortOrder(nCol)
                End If
                'ソート。
                Call Sort(Index, False)
            '右ボタンならカレント行の変更、ならびにポップアップメニューを表示。
            ElseIf (Button And vbRightButton) <> 0 Then
                Dim nRow As Long
                nRow = grdFlexGrid(Index).MouseRow
                '固定行の場合なにもしない。
                If nRow <= 0 Then Exit Sub
                'カレント行の変更。
                If Not grdFlexGrid(Index).IsSelected(nRow) Then
                    Dim bCtrl As Boolean
                    bCtrl = (Shift And &H2&) <> 0
                    If Not bCtrl Then
                        '選択オブジェクトのクリア。
                        Dim i As Long
                        For i = 0 To LIST_OBJ_TYPE_INDEX_COUNT - 1
                            Set m_objHideSelected(i) = New Collection
                        Next
                    End If
                    Call grdFlexGrid(Index).SelectRows(nRow, nRow, bCtrl, True)
                    '選択行変更イベントを通知する。
                    RaiseEvent ChangeSelRow(Index)
                End If
                '要素が一つもない場合、HighLight が flexHighlightNever に設定されている。
                If grdFlexGrid(objTab.Tab).HighLight <> flexHighlightAlways Then Exit Sub
                '選択行が一つもない場合終了。
                If grdFlexGrid(objTab.Tab).SelectedCount() <= 0 Then Exit Sub
                '起動。
                RaiseEvent RightDown(objTab.Tab)
            End If
    
            Exit Sub
    
        ErrorHandler:
            Call mdlMain.ErrorExit
    
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'クリック。
        private void grdFlexGrid_MouseDown(int Index, int Button, int Shift, float X, float Y)
        {
            try
            {
#if false
                //'リストクリックイベント。
                RaiseEvent ListClick;

                //'左ボタンならソート。
                if ((Button & vbLeftButton) != 0)
                {
                    //'固定行でなければソートしない。
                    if (grdFlexGrid(Index).MouseRow > 0)
                    {
                        return;
                    }
                    //'ソート条件。
                    long nCol;
                    nCol = grdFlexGrid(Index).MouseCol;
                    if (m_clsListSort(Index).Col(0) == nCol)
                    {
                        m_clsListSort(Index).Ascending(0) = Not m_clsListSort(Index).Ascending(0);
                    }
                    else
                    {
                        //'順番入れ替え。
                        m_clsListSort(Index).ReplaceSortOrder(nCol);
                    }
                    //'ソート。
                    Sort(Index, false);
                }
                //'右ボタンならカレント行の変更、ならびにポップアップメニューを表示。
                else if ((Button & vbRightButton) != 0)
                {
                    long nRow;
                    nRow = grdFlexGrid(Index).MouseRow;
                    //'固定行の場合なにもしない。
                    if (nRow <= 0)
                    {
                        return;
                    }
                    //'カレント行の変更。
                    if (!grdFlexGrid(Index).IsSelected(nRow))
                    {
                        bool bCtrl;
                        bCtrl = (Shift & 0x02) != 0;
                        if (!bCtrl)
                        {
                            //'選択オブジェクトのクリア。
                            long i;
                            for (i = 0; i < (long)TagLIST_OBJ_TYPE_INDEX.LIST_OBJ_TYPE_INDEX_COUNT; i++)
                            {
                                m_objHideSelected = new Dictionary<string, object>[i];
                            }
                        }
                        grdFlexGrid(Index).SelectRows(nRow, nRow, bCtrl, true);
                        //'選択行変更イベントを通知する。
                        RaiseEvent ChangeSelRow(Index);
                    }
                    //'要素が一つもない場合、HighLight が flexHighlightNever に設定されている。
                    if (grdFlexGrid(objTab.Tab).HighLight != flexHighlightAlways)
                    {
                        return;
                    }
                    //'選択行が一つもない場合終了。
                    if (grdFlexGrid(objTab.Tab).SelectedCount() <= 0)
                    {
                        return;
                    }
                    //'起動。
                    RaiseEvent RightDown(objTab.Tab);
                }
#endif
                return;
            }

            catch
            {
                m_clsMdlMain.ErrorExit();
                return;
            }
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'ソートイベント。
        Private Sub grdFlexGrid_Compare(Index As Integer, ByVal Row1 As Long, ByVal Row2 As Long, Cmp As Integer)

            On Error GoTo ErrorHandler
    
            Call CompareSortList(Index, grdFlexGrid(Index), m_clsListSort(Index), Row1, Row2, Cmp)
    
            Exit Sub
    
        ErrorHandler:
            Call mdlMain.ErrorExit
    
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'ソートイベント。
        private void grdFlexGrid_Compare(int Index, long Row1, long Row2, int Cmp)
        {

            try
            {
#if false
                CompareSortList(Index, grdFlexGrid(Index), m_clsListSort(Index), Row1, Row2, Cmp);
#endif
                return;
            }

            catch
            {
                m_clsMdlMain.ErrorExit();
                return;
            }
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '*******************************************************************************
        'メソッド

        'リストを再作成する｡
        '
        '引き数：
        'bOrder 行の順番を保持するか？
        Public Sub RemakeList(ByVal bOrder As Boolean)
            Dim i As Long
            For i = 0 To LIST_NUM_COUNT - 1
                Call RemakeListAt(i, bOrder)
            Next
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '*******************************************************************************
        'メソッド

        'リストを再作成する｡
        '
        '引き数：
        'bOrder 行の順番を保持するか？
        */
        public void RemakeList(bool bOrder)
        {
            long i;
            for (i = 0; i < (long)LIST_NUM_PANE.LIST_NUM_COUNT; i++)
            {
                RemakeListAt(i, bOrder);
            }
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'リストを再作成する。
        '
        'nList で指定されたリストを再作成する。
        'bCtrl の値は Sort メソッドに渡すための値である。
        '結局のところ bCtrl を True にするということは、再作成する nList で指定されたリスト以外の選択行を保持するのが目的。
        '再作成するリストの選択行は破棄されることには変わりない。
        '
        '引き数：
        'nList 対象とするリストの番号。
        'bCtrl Ctrlキーの有無。
        Public Sub RemakeListAt(ByVal nList As Long, ByVal bOrder As Boolean, Optional ByVal bCtrl As Boolean = False)

            '非表示。
            Dim bRedraw As Boolean
            bRedraw = grdFlexGrid(nList).Redraw
            grdFlexGrid(nList).Redraw = False
    
            '順番は保持？
            Dim nRow As Long
            If bOrder Then
                Dim objOrder As Collection
                Set objOrder = New Collection
                For nRow = 1 To grdFlexGrid(nList).rows - 1
                    Dim vItem As Variant
                    vItem = grdFlexGrid(nList).rows - nRow
                    Call objOrder.Add(vItem, Hex$(grdFlexGrid(nList).RowData(nRow)))
                Next
            End If
    
            '作成。
            Call RemakeListImpl(nList, grdFlexGrid(nList), m_objMap(nList))
    
            '順番は保持？
            If bOrder Then
                For nRow = 1 To grdFlexGrid(nList).rows - 1
                    If LookupCollectionVariant(objOrder, vItem, Hex$(grdFlexGrid(nList).RowData(nRow))) Then
                        grdFlexGrid(nList).TextMatrix(nRow, 0) = Format$(vItem, "0000000000")
                    Else
                        grdFlexGrid(nList).TextMatrix(nRow, 0) = ""
                    End If
                Next
                Call m_clsListSort(nList).ReplaceSortOrder(0, False)
            End If
    
            'ソート。
            Call Sort(nList, bCtrl)
    
            'ソートオーダーを元に戻す。
            If bOrder Then
                Call m_clsListSort(nList).ReplaceSortOrderBehind(0)
            End If
    
            '表示。
            grdFlexGrid(nList).Redraw = bRedraw
    
            '選択行変更イベントを通知する。
            If nList = objTab.Tab Then RaiseEvent ChangeSelRow(objTab.Tab)
    
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        'リストを再作成する。
        '
        'nList で指定されたリストを再作成する。
        'bCtrl の値は Sort メソッドに渡すための値である。
        '結局のところ bCtrl を True にするということは、再作成する nList で指定されたリスト以外の選択行を保持するのが目的。
        '再作成するリストの選択行は破棄されることには変わりない。
        '
        '引き数：
        'nList 対象とするリストの番号。
        'bCtrl Ctrlキーの有無。
        */
        public void RemakeListAt(long nList, bool bOrder, bool bCtrl = false)
        {
#if true
            DataGridView DG = GetDataGridView(nList);

            //'非表示。
#if false
            bool bRedraw;
            bRedraw = grdFlexGrid(nList).Redraw;
            grdFlexGrid(nList).Redraw = false;
#endif
            //'順番は保持？
            long nRow;
            string vItem;
            Dictionary<string, object> objOrder = new Dictionary<string, object>();
            if (bOrder)
            {
                for (nRow = 1; nRow < DG.Rows.Count; nRow++)
                {
                    vItem = CreateKey_NSO();
                    objOrder.Add(vItem, DG.Rows.SharedRow((int)nRow));
                }
            }

            //'作成。
            m_clsmdilistPane.RemakeListImpl(nList, DG, ref m_objMap[nList]);

            //'順番は保持？
            if (bOrder)
            {
                string vKey;
                object vObj = null;
                for (nRow = 1; nRow < DG.Rows.Count; nRow++)
                {
                    vKey = CreateKey_NSO();
                    if (LookupCollectionVariant_so(objOrder, ref vObj, vKey))
                    {
                        DG[(int)nRow, 0].Value = vObj.ToString();
                    }
                    else
                    {
                        DG[(int)nRow, 0].Value = "";
                    }
                }
                m_clsListSort[nList].ReplaceSortOrder(0, false);
            }

            //'ソート。
            Sort((int)nList, bCtrl);

            //'ソートオーダーを元に戻す。
            if (bOrder)
            {
                m_clsListSort[nList].ReplaceSortOrderBehind(0);
            }

            //'表示。
            DG.Refresh();

            //'選択行変更イベントを通知する。
            if (nList == objTab.SelectedIndex)
            {
                //RaiseEvent ChangeSelRow(objTab.Tab);
            }
#endif
            return;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '指定された行を削除する。
        '
        '引き数：
        'nRow 対象とする行の番号。
        Public Sub RemoveRow(ByVal nRow As Long)
            Dim nData As Long
            nData = grdFlexGrid(objTab.Tab).RowData(nRow)
            Call m_objMap(objTab.Tab).Remove(Hex$(nData))
            Call grdFlexGrid(objTab.Tab).RemoveItem(nRow)
            '選択行変更イベントを通知する。
            RaiseEvent ChangeSelRow(objTab.Tab)
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '指定された行を削除する。
        '
        '引き数：
        'nRow 対象とする行の番号。
        */
        public void RemoveRow(long nRow)
        {
#if false
            long nData;
            nData = grdFlexGrid(objTab.Tab).RowData(nRow);
            m_objMap(objTab.Tab).Remove(Hex$(nData));
            grdFlexGrid(objTab.Tab).RemoveItem(nRow);
            //'選択行変更イベントを通知する。
            RaiseEvent ChangeSelRow(objTab.Tab);
#endif
            return;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '選択行の要素取得を開始する。
        '
        '選択行の位置を示す整数値が取得される。
        'このメソッドで取得される整数値を GetNextAssoc メソッドに渡すことにより選択行のオブジェクトが取得できる。
        'GetNextAssoc で 0 が返るまで、繰り返し GetNextAssoc を呼ぶことによりすべての選択行のオブジェクトが取得できる。
        '
        '引き数：
        'nList 対象とするリストの番号。省略するとカレントリストが対象になる。
        '
        '戻り値：
        '選択行の位置を示す整数値を返す。
        '選択行が無い場合は 0 を返す。
        Public Function StartSelectedAssoc(Optional ByVal nList As Long = -1) As Long
            If nList < 0 Or nList = objTab.Tab Then
                m_nSelectedList = objTab.Tab
                '要素が一つもない場合、HighLight が flexHighlightNever に設定されている。
                If grdFlexGrid(objTab.Tab).HighLight <> flexHighlightAlways Then
                    StartSelectedAssoc = 0
                Else
                    m_nSelectedRows = grdFlexGrid(objTab.Tab).SelectedRows()
                    StartSelectedAssoc = UBound(m_nSelectedRows) + 1 '選択行無しが0なので一つずらす。
                End If
            Else
                m_nSelectedList = nList
                'インデックス＋１を設定。
                StartSelectedAssoc = m_objHideSelected(LIST_OBJ_TYPE_INDEX(nList)).Count
            End If
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]  //2
        /// <summary>
        /// 選択行の要素取得を開始する。
        /// '
        /// 選択行の位置を示す整数値が取得される。
        /// このメソッドで取得される整数値を GetNextAssoc メソッドに渡すことにより選択行のオブジェクトが取得できる。
        /// GetNextAssoc で 0 が返るまで、繰り返し GetNextAssoc を呼ぶことによりすべての選択行のオブジェクトが取得できる。
        /// '
        /// 引き数：
        /// nList 対象とするリストの番号。省略するとカレントリストが対象になる。
        /// 
        /// </summary>
        /// <param name="nList"></param>
        /// <returns>
        /// 戻り値：
        ///     選択行の位置を示す整数値を返す。<--( 0) から XX
        ///     選択行が無い場合は 0 を返す。<----- --(-1)に訂正
        /// </returns>
        public long StartSelectedAssoc(long nList = -1)  //2
        {

            long StartSelectedAssoc = 0;

            DataGridView DG = GetDataGridView(nList);

            StartSelectedAssoc = m_clsmdilistPane.StartSelectedAssoc(nList, DG, ref m_objMap[nList]);

            return StartSelectedAssoc;

        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '選択行の次の要素を取得する。
        '
        'nPos で指定される選択行のオブジェクトを取得する。
        'GetNextAssoc で 0 が返るまで、繰り返し GetNextAssoc を呼ぶことによりすべての選択行のオブジェクトが取得できる。
        '
        '引き数：
        'nPos 選択行の位置を示す整数値。
        '
        '戻り値：
        '次の選択行の位置を示す整数値を返す。
        '選択行が無い場合は 0 を返す。   <----- --(-1)に訂正
        Public Function GetNextAssoc(ByRef nPos As Long) As Object
            If m_nSelectedList = objTab.Tab Then
                Dim nData As Long
                nPos = nPos - 1 '先にデクリメント。
                nData = grdFlexGrid(objTab.Tab).RowData(m_nSelectedRows(nPos))
                Set GetNextAssoc = m_objMap(objTab.Tab).Item(Hex$(nData))
            Else
                Set GetNextAssoc = m_objHideSelected(LIST_OBJ_TYPE_INDEX(m_nSelectedList)).Item(nPos)
                nPos = nPos - 1
            End If
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        public object GetNextAssoc(ref long nPos, long nList = -1)   //2
        {
            DataGridView DG = GetDataGridView(nList);

            object StartSelectedAssoc;

            StartSelectedAssoc = m_clsmdilistPane.GetNextAssoc(nList, DG, ref m_objMap[nList], ref nPos);

            return StartSelectedAssoc;

        }

        /*
        '選択行の次の要素を取得する。
        '
        'nPos で指定される選択行のオブジェクトを取得する。
        'GetNextAssoc で 0 が返るまで、繰り返し GetNextAssoc を呼ぶことによりすべての選択行のオブジェクトが取得できる。
        '
        '引き数：
        'nPos 選択行の位置を示す整数値。
        '
        '戻り値：
        '次の選択行の位置を示す整数値を返す。
        '選択行が無い場合は 0 を返す。
        */
        public object GetNextAssoc(ref long nPos)
        {
            object w_GetNextAssoc = null;
#if false
            if (m_nSelectedList == objTab.Tab)
            {
                long nData;
                nPos = nPos - 1;        //'先にデクリメント。
                nData = grdFlexGrid(objTab.Tab).RowData(m_nSelectedRows(nPos));
                w_GetNextAssoc = m_objMap(objTab.Tab).Item(Hex$(nData));
            }
            else
            {
                w_GetNextAssoc = m_objHideSelected(LIST_OBJ_TYPE_INDEX(m_nSelectedList)).Item(nPos);
                nPos--;
            }
#endif
            return w_GetNextAssoc;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '指定された行の内容を更新する。
        '
        '引き数：
        'nRow 対象とする行の番号。
        'nList 対象とするリストの番号。
        Public Sub UpdateRow(ByVal nRow As Long, ByVal nList As Long)
            Dim nData As Long
            nData = grdFlexGrid(nList).RowData(nRow)
            Call UpdateRowList(nList, grdFlexGrid(nList), nRow, m_objMap(nList).Item(Hex$(nData)))
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '指定された行の内容を更新する。
        '
        '引き数：
        'nRow 対象とする行の番号。
        'nList 対象とするリストの番号。
        */
        public void UpdateRow(long nRow, long nList)
        {
#if false
            long nData;
            nData = grdFlexGrid(nList).RowData(nRow);
            UpdateRowList(nList, grdFlexGrid(nList), nRow, m_objMap(nList).Item(Hex$(nData)));
#endif
            return;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '指定された要素の内容を設定する。
        '
        'objElement で指定されるオブジェクトに対応する行の内容を更新する。
        '
        '引き数：
        'objElement 対象とするオブジェクト。
        'nList 対象とするリストの番号。
        Public Sub UpdateElementRow(ByVal objElement As Object, ByVal nList As Long)
            Dim nRow As Long
            Dim nData As Long
            nData = GetPointer(objElement)
            For nRow = 1 To grdFlexGrid(nList).rows - 1
                If grdFlexGrid(nList).RowData(nRow) = nData Then
                    Call UpdateRowList(nList, grdFlexGrid(nList), nRow, objElement)
                    Exit For
                End If
            Next
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '指定された要素の内容を設定する。
        '
        'objElement で指定されるオブジェクトに対応する行の内容を更新する。
        '
        '引き数：
        'objElement 対象とするオブジェクト。
        'nList 対象とするリストの番号。
        */
        public void UpdateElementRow(object objElement, long nList)
        {
#if false
            long nRow;
            long nData;
            nData = GetPointer(objElement);
            for (nRow = 1; i < grdFlexGrid(nList).rows; indexer++)
            {
                if (grdFlexGrid(nList).RowData(nRow) == nData)
                {
                    UpdateRowList(nList, grdFlexGrid(nList), nRow, objElement);
                    break;
                }
            }
#endif
            return;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '選択行の内容を更新する。
        '
        'すべてのリストが対象。
        Public Sub UpdateSelectedRow()
            Dim nRows() As Long
            Dim i As Long
            nRows = grdFlexGrid(objTab.Tab).SelectedRows()
            For i = 0 To UBound(nRows)
                Call UpdateRow(nRows(i), objTab.Tab)
            Next
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '選択行の内容を更新する。
        '
        'すべてのリストが対象。
        */
        public void UpdateSelectedRow()
        {
#if false
            long[] nRows;
            long i;
            nRows = grdFlexGrid(objTab.Tab).SelectedRows();
            for (i = 0; i < nRows.Length; i++)
            {
                UpdateRow(nRows(i), objTab.Tab);
            }
#endif
            return;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '全行の内容を更新する。
        '
        '引き数：
        'nList 対象とするリストの番号。
        Public Sub UpdateAllRow(ByVal nList As Long)
            '要素が一つもない場合、HighLight が flexHighlightNever に設定されている。
            If grdFlexGrid(objTab.Tab).HighLight <> flexHighlightAlways Then Exit Sub
            Dim nRow As Long
            For nRow = 1 To grdFlexGrid(nList).rows - 1
                Call UpdateRow(nRow, nList)
            Next
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '全行の内容を更新する。
        '
        '引き数：
        'nList 対象とするリストの番号。
        */
        public void UpdateAllRow(long nList)
        {
#if false
            //'要素が一つもない場合、HighLight が flexHighlightNever に設定されている。
            if (grdFlexGrid(objTab.Tab).HighLight != flexHighlightAlways)
            {
                return;
            }
            long nRow;
            for (nRow = 1; nRow < grdFlexGrid(nList).rows; nRow++)
            {
                UpdateRow(nRow, nList);
            }
#endif
            return;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '汎用作業キー(WorkKey)が指定された値に設定されているオブジェクトの行を更新する。
        '
        'すべてのリストが対象。
        '
        '引き数：
        'nWorkKey 汎用作業キーの値。
        Public Sub UpdateRowWorkKey(ByVal nWorkKey As Long)

            '再描画無効。
            Dim bRedraw(LIST_NUM_COUNT - 1) As Boolean
            Dim i As Long
            For i = 0 To LIST_NUM_COUNT - 1
                bRedraw(i) = grdFlexGrid(i).Redraw
        '        grdFlexGrid(i).Redraw = False
            Next
    
            '要素が無い場合は何もしない。
            Dim nData As Long
            Dim nRow As Long
            Dim objElement As Object
            For i = 0 To LIST_NUM_COUNT - 1
                If grdFlexGrid(i).HighLight = flexHighlightAlways Then
                    For nRow = 1 To grdFlexGrid(i).rows - 1
                        nData = grdFlexGrid(i).RowData(nRow)
                        Set objElement = m_objMap(i).Item(Hex$(nData))
                        If objElement.WorkKey = nWorkKey Then Call UpdateRowList(i, grdFlexGrid(i), nRow, objElement)
                    Next
                End If
            Next
    
            '表示。
            For i = 0 To LIST_NUM_COUNT - 1
                grdFlexGrid(i).Redraw = bRedraw(i)
            Next
    
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '汎用作業キー(WorkKey)が指定された値に設定されているオブジェクトの行を更新する。
        '
        'すべてのリストが対象。
        '
        '引き数：
        'nWorkKey 汎用作業キーの値。
        */
        public void UpdateRowWorkKey(long nWorkKey)
        {
#if false
            //'再描画無効。
            bool[] bRedraw = new bool[(int)LIST_NUM_PANE.LIST_NUM_COUNT];
            long i;
            for (i = 0; i < (long)LIST_NUM_PANE.LIST_NUM_COUNT; i++)
            {
                bRedraw[i] = grdFlexGrid(i).Redraw;
                //'        grdFlexGrid(i).Redraw = False
            }

            //'要素が無い場合は何もしない。
            long nData;
            long nRow;
            object objElement;
            for (i = 0; i < (long)LIST_NUM_PANE.LIST_NUM_COUNT; i++)
            {
                if (grdFlexGrid(i).HighLight == flexHighlightAlways)
                {
                    for (nRow = 1; nRow < grdFlexGrid(i).rows; nRow++)
                    {
                        nData = grdFlexGrid(i).RowData(nRow);
                        objElement = m_objMap(i).Item(Hex$(nData));
                        if (objElement.WorkKey == nWorkKey)
                        {
                            UpdateRowList(i, grdFlexGrid(i), nRow, objElement);
                        }
                    }
                }
            }

            //'表示。
            for (i = 0; i < (long)LIST_NUM_PANE.LIST_NUM_COUNT; i++)
            {
                grdFlexGrid(i).Redraw = bRedraw(i);
            }
#endif
            return;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'リスト更新必要フラグ(IsList)が指定されているオブジェクトの行を更新する。
        '
        'すべてのリストが対象。
        Public Sub UpdateRowIsList()

            '再描画無効。
            Dim bRedraw(LIST_NUM_COUNT - 1) As Boolean
            Dim i As Long
            For i = 0 To LIST_NUM_COUNT - 1
                bRedraw(i) = grdFlexGrid(i).Redraw
        '        grdFlexGrid(i).Redraw = False
            Next
    
            '更新。
            For i = 0 To LIST_NUM_COUNT - 1
                Call UpdateRowIsListImpl(i, grdFlexGrid(i), m_objMap(i))
            Next
    
            '表示。
            For i = 0 To LIST_NUM_COUNT - 1
                grdFlexGrid(i).Redraw = bRedraw(i)
            Next
    
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        'リスト更新必要フラグ(IsList)が指定されているオブジェクトの行を更新する。
        '
        'すべてのリストが対象。
        */
        public void UpdateRowIsList()
        {
#if false
            //'再描画無効。
            bool[] bRedraw = new bool[(long)LIST_NUM_PANE.LIST_NUM_COUNT];
            long i;
            for (i = 0; i < (long)LIST_NUM_PANE.LIST_NUM_COUNT; i++)
            {
                bRedraw(i) = grdFlexGrid(i).Redraw;
                //'        grdFlexGrid(i).Redraw = False
            }


            //'更新。
            for (i = 0; i < (long)LIST_NUM_PANE.LIST_NUM_COUNT; i++)
            {
                UpdateRowIsListImpl(i, grdFlexGrid(i), m_objMap(i));
            }


            //'表示。
            for (i = 0; i < (long)LIST_NUM_PANE.LIST_NUM_COUNT; i++)
            {
                grdFlexGrid(i).Redraw = bRedraw(i);
            }
#endif
            return;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '指定された要素の行を選択する。
        '
        'objElements で指定されたオブジェクトに対応する行を選択する。
        'カレントリストが対象。
        'bCtrl が True の場合、既存の選択行は破棄せずに選択されたままになる。
        'bCtrl が False の場合、既存の選択行は破棄される。
        'bNotInverse が True の場合、既に選択されている行を選択しようとした時はそのまま選択状態にする。
        'bNotInverse が False の場合、既に選択されている行を選択しようとした時は反対に非選択にする。
        '
        '引き数：
        'objElements 対象とするオブジェクト。参照する配列の要素は(0 To ...)。Ubound 関数を使用する。
        'bCtrl Ctrlキーの有無。
        'bNotInverse 選択反転の有無。
        Public Sub SelectElements(ByRef objElements() As Object, Optional ByVal bCtrl As Boolean = False, Optional ByVal bNotInverse As Boolean = True)
            Dim nUBound As Long
            nUBound = UBound(objElements)
            '要素コレクション。
            Dim objElementsCollection As New Collection
            Dim nData As Long
            Dim i As Long
            For i = 0 To nUBound
                nData = GetPointer(objElements(i))
                Call objElementsCollection.Add(objElements(i), Hex$(nData))
            Next
            '行の選択。
            Call SelectElementsByCollection(objElementsCollection, bCtrl, bNotInverse)
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '指定された要素の行を選択する。
        '
        'objElements で指定されたオブジェクトに対応する行を選択する。
        'カレントリストが対象。
        'bCtrl が True の場合、既存の選択行は破棄せずに選択されたままになる。
        'bCtrl が False の場合、既存の選択行は破棄される。
        'bNotInverse が True の場合、既に選択されている行を選択しようとした時はそのまま選択状態にする。
        'bNotInverse が False の場合、既に選択されている行を選択しようとした時は反対に非選択にする。
        '
        '引き数：
        'objElements 対象とするオブジェクト。参照する配列の要素は(0 To ...)。Ubound 関数を使用する。
        'bCtrl Ctrlキーの有無。
        'bNotInverse 選択反転の有無。
        */
        public void SelectElements(ref object[] objElements, bool bCtrl = false, bool bNotInverse = true)
        {
#if false
            long nUBound;
            nUBound = objElements.Length;
            //'要素コレクション。
            Dictionary<string, object> objElementsCollection = new Dictionary<string, object>();
            long nData;
            long i;
            for (i = 0; i < nUBound; i++)
            {
                nData = GetPointer(objElements(i));
                objElementsCollection.Add(objElements(i), Hex$(nData));
            }
            //'行の選択。
            SelectElementsByCollection(objElementsCollection, bCtrl, bNotInverse);
#endif
            return;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '指定された要素の行を選択する。
        '
        'objElementsCollection で指定されたオブジェクトに対応する行を選択する。
        'カレントリストが対象。
        'bCtrl が True の場合、既存の選択行は破棄せずに選択されたままになる。
        'bCtrl が False の場合、既存の選択行は破棄される。
        'bNotInverse が True の場合、既に選択されている行を選択しようとした時はそのまま選択状態にする。
        'bNotInverse が False の場合、既に選択されている行を選択しようとした時は反対に非選択にする。
        '
        '引き数：
        'objElementsCollection 対象とするオブジェクト。
        'bCtrl Ctrlキーの有無。
        'bNotInverse 選択反転の有無。
        Public Sub SelectElementsByCollection(ByVal objElementsCollection As Collection, Optional ByVal bCtrl As Boolean = False, Optional ByVal bNotInverse As Boolean = True)
        'これはまずいかも。
        '    If Not bCtrl Then
        '        '選択オブジェクトのクリア。
        '        Dim i As Long
        '        For i = 0 To LIST_OBJ_TYPE_INDEX_COUNT - 1
        '            Set m_objHideSelected(i) = New Collection
        '        Next
        '    End If
            If Not bCtrl Then Call grdFlexGrid(objTab.Tab).ReleaseRows(False, False)
            '行の選択。
            Dim nRow As Long
            Dim nCount As Long
            Dim objElement As Object
            nCount = 0
            For nRow = 1 To grdFlexGrid(objTab.Tab).rows - 1
                If LookupCollectionObject(objElementsCollection, objElement, Hex$(grdFlexGrid(objTab.Tab).RowData(nRow))) Then
                    '行の選択。
                    Call grdFlexGrid(objTab.Tab).SelectRows(nRow, nRow, IIf(nCount = 0, bCtrl, True), bNotInverse)
                    'すべて選択されたら終了。
                    nCount = nCount + 1
                    If objElementsCollection.Count <= nCount Then Exit For
                End If
            Next
            '選択行変更イベントを通知する。
            RaiseEvent ChangeSelRow(objTab.Tab)
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '指定された要素の行を選択する。
        '
        'objElementsCollection で指定されたオブジェクトに対応する行を選択する。
        'カレントリストが対象。
        'bCtrl が True の場合、既存の選択行は破棄せずに選択されたままになる。
        'bCtrl が False の場合、既存の選択行は破棄される。
        'bNotInverse が True の場合、既に選択されている行を選択しようとした時はそのまま選択状態にする。
        'bNotInverse が False の場合、既に選択されている行を選択しようとした時は反対に非選択にする。
        '
        '引き数：
        'objElementsCollection 対象とするオブジェクト。
        'bCtrl Ctrlキーの有無。
        'bNotInverse 選択反転の有無。
        */
        public void SelectElementsByCollection(Dictionary<string, object> objElementsCollection, bool bCtrl = false, bool bNotInverse = true)
        {
            /*
            'これはまずいかも。
            '    If Not bCtrl Then
            '        '選択オブジェクトのクリア。
            '        Dim i As Long
            '        For i = 0 To LIST_OBJ_TYPE_INDEX_COUNT - 1
            '            Set m_objHideSelected(i) = New Collection
            '        Next
            '    End If
            */
#if false
            if (!bCtrl)
            {
                grdFlexGrid(objTab.Tab).ReleaseRows(false, false);
            }
            //'行の選択。
            long nRow;
            long nCount;
            object objElement;
            nCount = 0;
            for (nRow = 1l nRow < grdFlexGrid(objTab.Tab).rows; nRow++)
            {
                if (LookupCollectionObject(objElementsCollection, objElement, Hex$(grdFlexGrid(objTab.Tab).RowData(nRow))))
                {
                    //'行の選択。
                    grdFlexGrid(objTab.Tab).SelectRows(nRow, nRow, IIf(nCount = 0, bCtrl, True), bNotInverse);
                    //'すべて選択されたら終了。
                    nCount = nCount + 1;
                    if (objElementsCollection.Count <= nCount)
                    {
                        break;
                    }
                }
            }
            //'選択行変更イベントを通知する。
            RaiseEvent ChangeSelRow(objTab.Tab);
#else
            return;
#endif
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '指定された要素の行を選択する。
        '
        'objElement で指定されたオブジェクトに対応する行を選択する。
        'bCtrl が True の場合、既存の選択行は破棄せずに選択されたままになる。
        'bCtrl が False の場合、既存の選択行は破棄される。
        'bNotInverse が True の場合、既に選択されている行を選択しようとした時はそのまま選択状態にする。
        'bNotInverse が False の場合、既に選択されている行を選択しようとした時は反対に非選択にする。
        '
        '引き数：
        'objElement 対象とするオブジェクト。
        'bCtrl Ctrlキーの有無。
        'bNotInverse 選択反転の有無。
        'nList 対象とするリストの番号。省略するとカレントリストが対象になる。
        Public Sub SelectElement(ByVal objElement As Object, Optional ByVal bCtrl As Boolean = False, Optional ByVal bNotInverse As Boolean = True, Optional ByVal nList As Long = -1)
            If nList < 0 Or nList = objTab.Tab Then
                If Not bCtrl Then
                    '選択オブジェクトのクリア。
                    Dim i As Long
                    For i = 0 To LIST_OBJ_TYPE_INDEX_COUNT - 1
                        Set m_objHideSelected(i) = New Collection
                    Next
                End If
                If objElement Is Nothing Then
                    If Not bCtrl Then Call grdFlexGrid(objTab.Tab).ReleaseRows(False, bCtrl)
                Else
                    Dim nRow As Long
                    Dim nData As Long
                    nData = GetPointer(objElement)
                    For nRow = 1 To grdFlexGrid(objTab.Tab).rows - 1
                        If grdFlexGrid(objTab.Tab).RowData(nRow) = nData Then
                            '行の選択。
                            Call grdFlexGrid(objTab.Tab).SelectRows(nRow, nRow, bCtrl, bNotInverse)
                            Exit For
                        End If
                    Next
                End If
            Else
                If Not bCtrl Then
                    '選択オブジェクトのクリア。
                    For i = 0 To LIST_OBJ_TYPE_INDEX_COUNT - 1
                        Set m_objHideSelected(i) = New Collection
                    Next
                    Call grdFlexGrid(objTab.Tab).ReleaseRows(False, bCtrl)
                End If
                If Not objElement Is Nothing Then
                    Dim objSelected As Collection
                    Set objSelected = m_objHideSelected(LIST_OBJ_TYPE_INDEX(nList))
                    Dim sKey As String
                    sKey = Hex$(GetPointer(objElement))
                    If bCtrl Then
                        'Ctrlキーが押されていた場合は反転を評価する。
                        If LookupCollectionObject(objSelected, objElement, sKey) Then
                            '選択解除。
                            Call objSelected.Remove(sKey)
                        Else
                            '通常の選択。
                            Call objSelected.Add(objElement, sKey)
                        End If
                    Else
                        Call SetAtCollectionObject(m_objHideSelected(LIST_OBJ_TYPE_INDEX(nList)), objElement, sKey)
                    End If
                End If
            End If
            '選択行変更イベントを通知する。
            RaiseEvent ChangeSelRow(objTab.Tab)
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '指定された要素の行を選択する。
        '
        'objElement で指定されたオブジェクトに対応する行を選択する。
        'bCtrl が True の場合、既存の選択行は破棄せずに選択されたままになる。
        'bCtrl が False の場合、既存の選択行は破棄される。
        'bNotInverse が True の場合、既に選択されている行を選択しようとした時はそのまま選択状態にする。
        'bNotInverse が False の場合、既に選択されている行を選択しようとした時は反対に非選択にする。
        '
        '引き数：
        'objElement 対象とするオブジェクト。
        'bCtrl Ctrlキーの有無。
        'bNotInverse 選択反転の有無。
        'nList 対象とするリストの番号。省略するとカレントリストが対象になる。
        */
        public void SelectElement(object objElement, bool bCtrl = false, bool bNotInverse = true, long nList = -1)
        {
#if false
            long i;
            DataGridView DG = GetDataGridView();
            if (nList < 0 | nList == objTab.SelectedIndex)
            {
                if (!bCtrl)
                {
                    //'選択オブジェクトのクリア。
                    m_objHideSelected = new Dictionary<string, object>[(long)TagLIST_OBJ_TYPE_INDEX.LIST_OBJ_TYPE_INDEX_COUNT];
                    for (i = 0; i < (long)TagLIST_OBJ_TYPE_INDEX.LIST_OBJ_TYPE_INDEX_COUNT; i++)
                    {
                        m_objHideSelected[i] = new Dictionary<string, object>();
                    }
                }
                if (objElement == null)
                {
                    if (!bCtrl)
                    {
                        DG.Rows.Clear();
                    }
                }
                else
                {
                    long nRow;
                    long nData;
                    nData = GetPointer(objElement)
                    for (nRow = 1; nRow < grdFlexGrid(objTab.Tab).rows; nRow++)
                    {
                        if (grdFlexGrid(objTab.Tab).RowData(nRow) == nData)
                        {
                            //'行の選択。
                            grdFlexGrid(objTab.Tab).SelectRows(nRow, nRow, bCtrl, bNotInverse);
                            break;
                        }
                    }
                }
            }
            else
            {
                if (!bCtrl)
                {
                    //'選択オブジェクトのクリア。
                    for (i = 0; i < (long)TagLIST_OBJ_TYPE_INDEX.LIST_OBJ_TYPE_INDEX_COUNT; i++)
                    {
                        m_objHideSelected = new Dictionary<string, object>[i];
                    }
                    grdFlexGrid(objTab.Tab).ReleaseRows(false, bCtrl);
                }
                if (objElement != null)
                {
                    Dictionary<string, object> objSelected = new Dictionary<string, object>();
                    objSelected = m_objHideSelected(LIST_OBJ_TYPE_INDEX(nList));
                    string sKey;
                    sKey = Hex$(GetPointer(objElement));
                    if (bCtrl)
                    {
                        //'Ctrlキーが押されていた場合は反転を評価する。
                        if (LookupCollectionObject(objSelected, objElement, sKey))
                        {
                            //'選択解除。
                            objSelected.Remove(sKey);
                        }
                        else
                        {
                            //'通常の選択。
                            objSelected.Add(objElement, sKey);
                        }
                    }
                    else
                    {
                        Call SetAtCollectionObject(m_objHideSelected(LIST_OBJ_TYPE_INDEX(nList)), objElement, sKey)
                    }
                }
            }
            //'選択行変更イベントを通知する。
            RaiseEvent ChangeSelRow(objTab.Tab);
#endif
            return;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '*******************************************************************************
        'インプリメンテーション

        'タブ内のリサイズ。
        '
        '引き数：
        'nList 対象とするリストの番号。
        Private Sub ResizeTab(ByVal nList As Long)

            'エッジマスク。
            Dim nEdgeHeight As Single
            nEdgeHeight = GetSystemMetrics(SM_CYEDGE) * Screen.TwipsPerPixelY
            picEdgeMask(nList).Width = objTab.Width
            picEdgeMask(nList).Top = objTab.Height - objTab.TabHeight - EDGE_MASK_HEIGHT * Screen.TwipsPerPixelY - nEdgeHeight
    
            'グリッド。
            grdFlexGrid(nList).Width = objTab.Width
            grdFlexGrid(nList).Height = IIf(picEdgeMask(nList).Top < 1, 1, picEdgeMask(nList).Top)
    
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '*******************************************************************************
        'インプリメンテーション

        'タブ内のリサイズ。
        '
        '引き数：
        'nList 対象とするリストの番号。
        */
        private void ResizeTab(long nList)
        {
#if false
            //'エッジマスク。
            string nEdgeHeight;
            nEdgeHeight = GetSystemMetrics(SM_CYEDGE) * Screen.TwipsPerPixelY;
            picEdgeMask(nList).Width = objTab.Width;
            picEdgeMask(nList).Top = objTab.Height - objTab.TabHeight - EDGE_MASK_HEIGHT * Screen.TwipsPerPixelY - nEdgeHeight;

            //'グリッド。
            grdFlexGrid(nList).Width = objTab.Width;
            grdFlexGrid(nList).Height = IIf(picEdgeMask(nList).Top < 1, 1, picEdgeMask(nList).Top);
#endif
            return;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'リストをソートする。
        '
        'bCtrl の値は grdFlexGrid_SelChange イベントハンドラに渡すための値である。
        '結局のところ bCtrl を True にするということは、ソートする nList で指定されたリスト以外の選択行を保持するのが目的。
        'ソートするリストの選択行は破棄されることには変わりない。
        '
        '引き数：
        'nList 対象とするリストの番号。
        'bCtrl Ctrlキーの有無。
        Private Sub Sort(ByVal nList As Integer, ByVal bCtrl As Boolean)

            '再描画OFF。
            Dim bRedraw As Boolean
            bRedraw = grdFlexGrid(nList).Redraw
            grdFlexGrid(nList).Redraw = False
    
            'ソート。
            grdFlexGrid(nList).Sort = 9
    
            '選択行を戻す。
            grdFlexGrid(nList).TopRow = 1
            grdFlexGrid(nList).Col = 0
            grdFlexGrid(nList).ColSel = grdFlexGrid(nList).Cols - 1
    
            '再描画。
            grdFlexGrid(nList).Redraw = bRedraw
    
            '選択行更新イベント。
            Call grdFlexGrid_SelChange(nList, bCtrl)
    
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        'リストをソートする。
        '
        'bCtrl の値は grdFlexGrid_SelChange イベントハンドラに渡すための値である。
        '結局のところ bCtrl を True にするということは、ソートする nList で指定されたリスト以外の選択行を保持するのが目的。
        'ソートするリストの選択行は破棄されることには変わりない。
        '
        '引き数：
        'nList 対象とするリストの番号。
        'bCtrl Ctrlキーの有無。
        */
        private void Sort(int nList, bool bCtrl)
        {
#if false
            //'再描画OFF。
            bool bRedraw;
            bRedraw = grdFlexGrid(nList).Redraw;
            grdFlexGrid(nList).Redraw = false;

            //'ソート。
            grdFlexGrid(nList).Sort = 9;

            //'選択行を戻す。
            grdFlexGrid(nList).TopRow = 1;
            grdFlexGrid(nList).Col = 0;
            grdFlexGrid(nList).ColSel = grdFlexGrid(nList).Cols - 1;

            //'再描画。
            grdFlexGrid(nList).Redraw = bRedraw;

            //'選択行更新イベント。
            Call grdFlexGrid_SelChange(nList, bCtrl);
#endif
            return;
        }
        //==========================================================================================





        //**************************************************************************************
        //**************************************************************************************
        /// <summary>
        /// '初期化 ListPane
        /// 引き数：
        //  ListPane：観測点＆ベクトル表示
        /// </summary>
        /// <param name="ListPane"></param>
        /// <returns>
        /// </returns>
        //**************************************************************************************
#if false
        public void UserControl_Initialize(ListPane listPane)
        {


            listPane.Height = 600;
            listPane.objTab.Width = 800;
            listPane.objTab.Height = 500;



            //[VB]  'リストの作成。
            //[VB]  For i = 0 To LIST_NUM_COUNT - 1
            //[VB]      Call MakeList(i, grdFlexGrid(i))
            //[VB]  Next
            //'リストの作成.
            for (int i = 0; i < (int)MdlListPane.LIST_NUM_PANE.LIST_NUM_COUNT; i++)
            {
                mdilistPane.MakeList((long)i, listPane);
            }

        }
        //--------------------------------------------------------------------------------
        //[VB]  '初期化。
        //[VB]  Private Sub UserControl_Initialize()
        //[VB]  
        //[VB]  '   ユーザーコントロールの中でユーザーコントロールをLoadすると、コンパイル時に実行時エラー7が発生する。
        //[VB]  '   コンパイル時にEndステートメントで終了するとexeファイルが生成されない。
        //[VB]  '   しょうがないのでエラーを無視する。
        //[VB]  '    On Error GoTo ErrorHandler
        //[VB]  On Error Resume Next
        //[VB]  
        //[VB]  '非表示リスト選択オブジェクトコレクション。
        //[VB]  Dim i As Long
        //[VB]  For i = 0 To LIST_OBJ_TYPE_INDEX_COUNT - 1
        //[VB]  Set m_objHideSelected(i) = New Collection
        //[VB]  Next
        //[VB]  
        //[VB]  'ソートオーダー。
        //[VB]  For i = 0 To LIST_NUM_COUNT - 1
        //[VB]      Set m_clsListSort(i) = New ListSort
        //[VB]  Next
        //[VB]  Call InitializeSortOrder(m_clsListSort)
        //[VB]  
        //[VB]  'タブコントロール。
        //[VB]  objTab.Left = ScaleLeft
        //[VB]  objTab.Top = ScaleTop
        //[VB]  objTab.TabsPerRow = LIST_NUM_COUNT
        //[VB]  objTab.Tabs = LIST_NUM_COUNT
        //[VB]  
        //[VB]  'リストのロード
        //[VB]  For i = 1 To LIST_NUM_COUNT - 1
        //[VB]  Load picEdgeMask(i)
        //[VB]  Load grdFlexGrid(i)
        //[VB]  picEdgeMask(i).Visible = True
        //[VB]  grdFlexGrid(i).Visible = True
        //[VB]  Next
        //[VB]  
        //[VB]  For i = 0 To LIST_NUM_COUNT - 1
        //[VB]  'エッジマスク。
        //[VB]  picEdgeMask(i).BorderStyle = vbBSNone
        //[VB]  '        picEdgeMask(i).Left = 0 'Left を設定するとタブ切り替えによる表示/非表示がうまく働かない・・・。リソースエディタで設定すること！
        //[VB]  picEdgeMask(i).Height = EDGE_MASK_HEIGHT * Screen.TwipsPerPixelY
        //[VB]  'グリッド。
        //[VB]  '        grdFlexGrid(i).Left = 0 'Left を設定するとタブ切り替えによる表示/非表示がうまく働かない・・・。リソースエディタで設定すること！
        //[VB]  grdFlexGrid(i).Top = 0
        //[VB]  grdFlexGrid(i).TabStop = False
        //[VB]  Next
        //[VB]  
        //[VB]  'タブストップ。
        //[VB]  grdFlexGrid(objTab.Tab).TabStop = True
        //[VB]  
        //[VB]  
        //[VB]  ReDim m_objMap(LIST_NUM_COUNT - 1)
        //[VB]  
        //[VB]  'リストの作成。
        //[VB]  For i = 0 To LIST_NUM_COUNT - 1
        //[VB]      Call MakeList(i, grdFlexGrid(i))
        //[VB]  Next
        //[VB]  
        //[VB]  Exit Sub
        //[VB]  
        //[VB]  ErrorHandler:
        //[VB]  Call mdlMain.ErrorExit
        //[VB]  
        //[VB]  
        //[VB]  End Sub
        //'*******************************************************************************
        //'*******************************************************************************
#endif

        public void ListDataDispMain()
        {
            ListPane listPane = new ListPane();

            m_clsmdilistPane.ListDataDisp(listPane);

        }
        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void ListPane_Load(object sender, EventArgs e)
        {
#if false
            ListPane listPane = (ListPane)sender;

            m_list = 12;


            UserControl_Initialize(listPane);
#endif
        }

        private void grdFlexGrid1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            return;
        }

#if false
        public long M_list
        {
            set
            {
                m_list = value;
            }
            get
            {
                return m_list;
            }
        }
#endif
    }
}
