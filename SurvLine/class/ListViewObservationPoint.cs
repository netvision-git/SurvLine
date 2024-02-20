using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static SurvLine.mdl.DEFINE;
using static SurvLine.MdlListPane;
using static System.Collections.Specialized.BitVector32;
using static System.Net.WebRequestMethods;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace SurvLine
{
    internal class ListViewObservationPoint : ListViewInterface
    {



        //'*******************************************************************************
        //'観測点リストビュー
        //
        //Option Explicit


        //==========================================================================================
        /*[VB]
            'インターフェース
            Implements ListViewInterface 'リストビューインターフェース。

            '観測点リストカラム番号。
            Private Enum COL_NUM_OBSPNT
                COL_NUM_OBSPNT_CHECK = 0 'チェックボックス。
                COL_NUM_OBSPNT_SESSION 'セッション名。
                COL_NUM_OBSPNT_NUMBER '観測点番号。
                COL_NUM_OBSPNT_NAME '観測点名称。
                COL_NUM_OBSPNT_STRTIME '観測開始日時。
                COL_NUM_OBSPNT_ENDTIME '観測終了日時。
                COL_NUM_OBSPNT_LAT '緯度。
                COL_NUM_OBSPNT_LON '経度。
                COL_NUM_OBSPNT_HEIGHT '楕円体高。
                COL_NUM_OBSPNT_COUNT '番号数。
            End Enum
            Private Const COL_NAM_OBSPNT_CHECK As String = "" '観測点リストリストカラム名称、チェックボックス。
            Private Const COL_NAM_OBSPNT_SESSION As String = "セッション名" '観測点リストリストカラム名称、セッション名。
            Private Const COL_NAM_OBSPNT_NUMBER As String = "観測点No" '観測点リストリストカラム名称、観測点番号。
            Private Const COL_NAM_OBSPNT_NAME As String = "観測点名称" '観測点リストリストカラム名称、観測点名称。
            Private Const COL_NAM_OBSPNT_STRTIME As String = "観測開始日時(JST)" '観測点リストリストカラム名称、観測開始日時。
            Private Const COL_NAM_OBSPNT_ENDTIME As String = "観測終了日時(JST)" '観測点リストリストカラム名称、観測終了日時。
            Private Const COL_NAM_OBSPNT_LAT As String = "緯度" '観測点リストリストカラム名称、緯度。
            Private Const COL_NAM_OBSPNT_LON As String = "経度" '観測点リストリストカラム名称、経度。
            Private Const COL_NAM_OBSPNT_HEIGHT As String = "楕円体高" '観測点リストリストカラム名称、楕円体高。

            Private Const COL_WID_OBSPNT_CHECK As Long = 384 '観測点リストカラム幅(Twips)、チェックボックス。
            Private Const COL_WID_OBSPNT_SESSION As Long = 1152 '観測点リストカラム幅(Twips)、セッション名。
            Private Const COL_WID_OBSPNT_NUMBER As Long = 1152 '観測点リストカラム幅(Twips)、観測点番号。
            Private Const COL_WID_OBSPNT_NAME As Long = 1152 '観測点リストカラム幅(Twips)、観測点名称。
            Private Const COL_WID_OBSPNT_STRTIME As Long = 2304 '観測点リストカラム幅(Twips)、観測開始日時。
            Private Const COL_WID_OBSPNT_ENDTIME As Long = 2304 '観測点リストカラム幅(Twips)、観測終了日時。
            Private Const COL_WID_OBSPNT_LAT As Long = 2048 '観測点リストカラム幅(Twips)、緯度。
            Private Const COL_WID_OBSPNT_LON As Long = 2048 '観測点リストカラム幅(Twips)、経度。
            Private Const COL_WID_OBSPNT_HEIGHT As Long = 1152 '観測点リストカラム幅(Twips)、楕円体高。
            '2009/11 H.Nakamura
            'リスト種別。
            Public Enum LIST_VIEW_OBSERVATION_POINT_TYPE
                LIST_VIEW_OBSERVATION_POINT_STANDARD = 0 '通常リスト。
                LIST_VIEW_OBSERVATION_POINT_ECCENTRIC_CORRECT = 1 '偏心補正。
                LIST_VIEW_OBSERVATION_POINT_FIXED_LIST = 2 '固定点リスト。
            End Enum

            'プロパティ
            Public SelectSession As String 'セッション評価。IsEnable で評価するセッション。空文字の場合はセッションは評価しない。

            'インプリメンテーション
            Private m_nListType As Long '種別。'2009/11 H.Nakamura
            'Private m_bEccentricCorrect As Boolean '偏心補正フラグ。True=偏心補正計算簿である。False=偏心補正計算簿以外。
            Private m_bSession As Boolean 'セッション表示フラグ。
            Private m_bTime As Boolean '観測日時表示フラグ。
            Private m_bCoord As Boolean '座標表示フラグ。
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]

        //'インターフェース
        //  interface ListViewInterface;    //'リストビューインターフェース。

        //'観測点リストカラム番号。
        private enum COL_NUM_OBSPNT
        {
            COL_NUM_OBSPNT_CHECK = 0,   //'チェックボックス。
            COL_NUM_OBSPNT_SESSION,     //'セッション名。
            COL_NUM_OBSPNT_NUMBER,      //'観測点番号。
            COL_NUM_OBSPNT_NAME,        //'観測点名称。
            COL_NUM_OBSPNT_STRTIME,     //'観測開始日時。
            COL_NUM_OBSPNT_ENDTIME,     //'観測終了日時。
            COL_NUM_OBSPNT_LAT,         //'緯度。
            COL_NUM_OBSPNT_LON,         //'経度。
            COL_NUM_OBSPNT_HEIGHT,      //'楕円体高。
            COL_NUM_OBSPNT_COUNT,       //'番号数。
        }

        public const string COL_NAM_OBSPNT_CHECK = "";                  //'観測点リストリストカラム名称、チェックボックス。
        public const string COL_NAM_OBSPNT_SESSION = "セッション名";    //'観測点リストリストカラム名称、セッション名。
        public const string COL_NAM_OBSPNT_NUMBER = "観測点No";         //'観測点リストリストカラム名称、観測点番号。
        public const string COL_NAM_OBSPNT_NAME = "観測点名称";         //'観測点リストリストカラム名称、観測点名称。
        public const string COL_NAM_OBSPNT_STRTIME = "観測開始日時(JST)"; //'観測点リストリストカラム名称、観測開始日時。
        public const string COL_NAM_OBSPNT_ENDTIME = "観測終了日時(JST)"; //'観測点リストリストカラム名称、観測終了日時。
        public const string COL_NAM_OBSPNT_LAT = "緯度";                 //'観測点リストリストカラム名称、緯度。
        public const string COL_NAM_OBSPNT_LON = "経度";                 //'観測点リストリストカラム名称、経度。
        public const string COL_NAM_OBSPNT_HEIGHT = "楕円体高";          //'観測点リストリストカラム名称、楕円体高。

        private const long COL_WID_OBSPNT_CHECK = 384;              //'観測点リストカラム幅(Twips)、チェックボックス。
        private const long COL_WID_OBSPNT_SESSION = 1152;           //'観測点リストカラム幅(Twips)、セッション名。
        private const long COL_WID_OBSPNT_NUMBER = 1152;            //'観測点リストカラム幅(Twips)、観測点番号。
        private const long COL_WID_OBSPNT_NAME = 1152;              //'観測点リストカラム幅(Twips)、観測点名称。
        private const long COL_WID_OBSPNT_STRTIME = 2304;           //'観測点リストカラム幅(Twips)、観測開始日時。
        private const long COL_WID_OBSPNT_ENDTIME = 2304;           //'観測点リストカラム幅(Twips)、観測終了日時。
        private const long COL_WID_OBSPNT_LAT = 2048;               //'観測点リストカラム幅(Twips)、緯度。
        private const long COL_WID_OBSPNT_LON = 2048;               //'観測点リストカラム幅(Twips)、経度。
        private const long COL_WID_OBSPNT_HEIGHT = 1152;            //'観測点リストカラム幅(Twips)、楕円体高。

        //'2009/11 H.Nakamura
        //'リスト種別。
        public enum LIST_VIEW_OBSERVATION_POINT_TYPE
        {
            LIST_VIEW_OBSERVATION_POINT_STANDARD = 0,           //'通常リスト。
            LIST_VIEW_OBSERVATION_POINT_ECCENTRIC_CORRECT = 1,  //'偏心補正。
            LIST_VIEW_OBSERVATION_POINT_FIXED_LIST = 2,         //'固定点リスト。
        }

        //'プロパティ
        public string SelectSession;    //'セッション評価。IsEnable で評価するセッション。空文字の場合はセッションは評価しない。

        //'インプリメンテーション
        private long m_nListType;       //'種別。'2009/11 H.Nakamura
        //'Private m_bEccentricCorrect As Boolean '偏心補正フラグ。True=偏心補正計算簿である。False=偏心補正計算簿以外。
        private bool m_bSession;        //'セッション表示フラグ。
        private bool m_bTime;           //'観測日時表示フラグ。
        private bool m_bCoord;          //'座標表示フラグ。



        public ListViewObservationPoint()
        {
            Class_Initialize();

        }


        //'*******************************************************************************
        //'プロパティ

        //==========================================================================================
        /*[VB]
            '2009/11 H.Nakamura
            'リスト種別。
            Property Let ListType(ByVal nListType As LIST_VIEW_OBSERVATION_POINT_TYPE)
                m_nListType = nListType
                If m_nListType = LIST_VIEW_OBSERVATION_POINT_ECCENTRIC_CORRECT Or m_nListType = LIST_VIEW_OBSERVATION_POINT_FIXED_LIST Then
                    m_bSession = False
                    m_bTime = False
                Else
                    m_bSession = True
                    m_bTime = True
                End If
            End Property
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /// <summary>
        ///    '2009/11 H.Nakamura
        ///    'リスト種別。
        /// 
        /// </summary>
        /// <param name="nListType"></param>
        public void ListType(long nListType)
        {
            m_nListType = (long)nListType;
            if (m_nListType == (long)LIST_VIEW_OBSERVATION_POINT_TYPE.LIST_VIEW_OBSERVATION_POINT_ECCENTRIC_CORRECT || m_nListType == (long)LIST_VIEW_OBSERVATION_POINT_TYPE.LIST_VIEW_OBSERVATION_POINT_FIXED_LIST)
            {
                m_bSession = false;
                m_bTime = false;
            }
            else
            {
                m_bSession = true;
                m_bTime = true;
            }

        }

        //==========================================================================================
        /*[VB]
        '2009/11 H.Nakamura
        '偏心補正フラグ｡True = 偏心補正計算簿である｡False = 偏心補正計算簿以外｡
        'Property Let EccentricCorrect(ByVal bEccentricCorrect As Boolean)
        '    m_bEccentricCorrect = bEccentricCorrect
        '    m_bSession = Not m_bEccentricCorrect
        '    m_bTime = Not m_bEccentricCorrect
        '    m_bCoord = False
        'End Property
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]

        //==========================================================================================
        /*[VB]
            '座標表示フラグ。
            Property Let Coord(ByVal bCoord As Boolean)
                m_bCoord = bCoord
            End Property
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /// <summary>
        ///    '座標表示フラグ。
        /// 
        /// </summary>
        /// <param name="bCoord"></param>
        public void Coord(bool bCoord)
        {
            m_bCoord = bCoord;
        }


        //'*******************************************************************************
        //'イベント

        //==========================================================================================
        /*[VB]
            '2009/11 H.Nakamura
            '初期化。
            Private Sub Class_Initialize()
                SelectSession = ""
                m_nListType = LIST_VIEW_OBSERVATION_POINT_STANDARD '2009/11 H.Nakamura
                'm_bEccentricCorrect = False
                m_bSession = True
                m_bTime = True
                m_bCoord = False
            End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /// <summary>
        ///    '2009/11 H.Nakamura
        ///    '初期化。
        /// 
        /// </summary>
        private void Class_Initialize()
        {
            SelectSession = "";
            m_nListType = (long)LIST_VIEW_OBSERVATION_POINT_TYPE.LIST_VIEW_OBSERVATION_POINT_STANDARD;     //'2009/11 H.Nakamura
            //'m_bEccentricCorrect = false
            m_bSession = true;
            m_bTime = true;
            m_bCoord = false;
        }

        //'*******************************************************************************
        //'インターフェース



        //==========================================================================================
        /*[VB]
            '2009/11 H.Nakamura
            'SortKey の初期値。
            Property Get ListViewInterface_DefaultSortKey() As Integer
                If Not m_bSession Then
                    ListViewInterface_DefaultSortKey = COL_NUM_OBSPNT_NUMBER - 1
                Else
                    ListViewInterface_DefaultSortKey = COL_NUM_OBSPNT_NUMBER
                End If
            End Property
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /// <summary>
        ///    '2009/11 H.Nakamura
        ///    'SortKey の初期値。
        /// 
        /// </summary>
        /// <returns></returns>
        public int ListViewInterface_DefaultSortKey()
        {
            if (!m_bSession)
            {
                return (int)COL_NUM_OBSPNT.COL_NUM_OBSPNT_NUMBER - 1;
            }
            else
            {
                return (int)COL_NUM_OBSPNT.COL_NUM_OBSPNT_NUMBER;
            }

        }

        //==========================================================================================
        /*[VB]
            '2009/11 H.Nakamura
            'リストの初期化。
            '
            'カラムを作り初期化する。
            '
            '引き数：
            'lvList 対象リストビュー。
            Public Sub ListViewInterface_Initialize(ByVal lvList As ListView)
                'カラムの初期化。
                Dim lvColumn As ColumnHeader
                Dim nWidth As Long
                Dim nTotalWidth As Long
                Set lvColumn = lvList.ColumnHeaders.Add(, , COL_NAM_OBSPNT_CHECK)
                nWidth = COL_WID_OBSPNT_CHECK
                nTotalWidth = nTotalWidth + nWidth
                lvColumn.Width = nWidth
                lvColumn.Alignment = lvwColumnLeft
                If m_bSession Then
                    Set lvColumn = lvList.ColumnHeaders.Add(, , COL_NAM_OBSPNT_SESSION)
                    nWidth = COL_WID_OBSPNT_SESSION
                    nTotalWidth = nTotalWidth + nWidth
                    lvColumn.Width = nWidth
                    lvColumn.Alignment = lvwColumnLeft
                End If
                Set lvColumn = lvList.ColumnHeaders.Add(, , COL_NAM_OBSPNT_NUMBER)
                nWidth = COL_WID_OBSPNT_NUMBER
                nTotalWidth = nTotalWidth + nWidth
                lvColumn.Width = nWidth
                lvColumn.Alignment = lvwColumnLeft
                Set lvColumn = lvList.ColumnHeaders.Add(, , COL_NAM_OBSPNT_NAME)
                If m_bTime Or m_bCoord Then
                    nWidth = COL_WID_OBSPNT_NAME
                    nTotalWidth = nTotalWidth + nWidth
                    lvColumn.Width = nWidth
                    lvColumn.Alignment = lvwColumnLeft
                    If m_bTime Then
                        Set lvColumn = lvList.ColumnHeaders.Add(, , COL_NAM_OBSPNT_STRTIME)
                        nWidth = COL_WID_OBSPNT_STRTIME
                        nTotalWidth = nTotalWidth + nWidth
                        lvColumn.Width = nWidth
                        lvColumn.Alignment = lvwColumnLeft
                        Set lvColumn = lvList.ColumnHeaders.Add(, , COL_NAM_OBSPNT_ENDTIME)
                        nWidth = COL_WID_OBSPNT_ENDTIME
                        nTotalWidth = nTotalWidth + nWidth
                        lvColumn.Width = nWidth
                        lvColumn.Alignment = lvwColumnLeft
                    End If
                    If m_bCoord Then
                        Set lvColumn = lvList.ColumnHeaders.Add(, , COL_NAM_OBSPNT_LAT)
                        nWidth = COL_WID_OBSPNT_LAT
                        nTotalWidth = nTotalWidth + nWidth
                        lvColumn.Width = nWidth
                        lvColumn.Alignment = lvwColumnRight
                        Set lvColumn = lvList.ColumnHeaders.Add(, , COL_NAM_OBSPNT_LON)
                        nWidth = COL_WID_OBSPNT_LON
                        nTotalWidth = nTotalWidth + nWidth
                        lvColumn.Width = nWidth
                        lvColumn.Alignment = lvwColumnRight
                        Set lvColumn = lvList.ColumnHeaders.Add(, , COL_NAM_OBSPNT_HEIGHT)
                        nWidth = COL_WID_OBSPNT_HEIGHT
                        nTotalWidth = nTotalWidth + nWidth
                        lvColumn.Width = nWidth
                        lvColumn.Alignment = lvwColumnRight
                    End If
                Else
                    Dim tRect As RECT
                    Dim nWidthScrollBar As Long
                    nWidthScrollBar = GetSystemMetrics(SM_CXVSCROLL) + 1 'チョット余裕をもたせるために1だけ足しとく。ギリギリだとはみ出す。四捨五入だから？
                    If GetClientRect(lvList.hWnd, tRect) = 0 Then Call Err.Raise(ERR_FATAL, , GetLastErrorMessage())
                    lvColumn.Width = CheckReturnMin((tRect.Right - tRect.Left - nWidthScrollBar) * Screen.TwipsPerPixelX - nTotalWidth, 0)
                    lvColumn.Alignment = lvwColumnLeft
                End If
            End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        public void ListViewInterface_Initialize(ListView lvList)
        {
            //'カラムの初期化。
            ColumnHeader lvColumn;
            long nWidth;
            long nTotalWidth = 0;
#if false
            nWidth = COL_WID_OBSPNT_CHECK;
            nTotalWidth = nTotalWidth + nWidth
            lvColumn = lvList.Columns.Add(COL_NAM_OBSPNT_CHECK, (int)nWidth);

            //  lvColumn.Alignment = lvwColumnLeft;

            lvColumn.ImageList;


                If m_bSession Then
                    Set lvColumn = lvList.ColumnHeaders.Add(, , COL_NAM_OBSPNT_SESSION)
                    nWidth = COL_WID_OBSPNT_SESSION
                    nTotalWidth = nTotalWidth + nWidth
                    lvColumn.Width = nWidth
                    lvColumn.Alignment = lvwColumnLeft
                End If
                Set lvColumn = lvList.ColumnHeaders.Add(, , COL_NAM_OBSPNT_NUMBER)
                nWidth = COL_WID_OBSPNT_NUMBER
                nTotalWidth = nTotalWidth + nWidth
                lvColumn.Width = nWidth
                lvColumn.Alignment = lvwColumnLeft
                Set lvColumn = lvList.ColumnHeaders.Add(, , COL_NAM_OBSPNT_NAME)
                If m_bTime Or m_bCoord Then
                    nWidth = COL_WID_OBSPNT_NAME
                    nTotalWidth = nTotalWidth + nWidth
                    lvColumn.Width = nWidth
                    lvColumn.Alignment = lvwColumnLeft
                    If m_bTime Then
                        Set lvColumn = lvList.ColumnHeaders.Add(, , COL_NAM_OBSPNT_STRTIME)
                        nWidth = COL_WID_OBSPNT_STRTIME
                        nTotalWidth = nTotalWidth + nWidth
                        lvColumn.Width = nWidth
                        lvColumn.Alignment = lvwColumnLeft
                        Set lvColumn = lvList.ColumnHeaders.Add(, , COL_NAM_OBSPNT_ENDTIME)
                        nWidth = COL_WID_OBSPNT_ENDTIME
                        nTotalWidth = nTotalWidth + nWidth
                        lvColumn.Width = nWidth
                        lvColumn.Alignment = lvwColumnLeft
                    End If
                    If m_bCoord Then
                        Set lvColumn = lvList.ColumnHeaders.Add(, , COL_NAM_OBSPNT_LAT)
                        nWidth = COL_WID_OBSPNT_LAT
                        nTotalWidth = nTotalWidth + nWidth
                        lvColumn.Width = nWidth
                        lvColumn.Alignment = lvwColumnRight
                        Set lvColumn = lvList.ColumnHeaders.Add(, , COL_NAM_OBSPNT_LON)
                        nWidth = COL_WID_OBSPNT_LON
                        nTotalWidth = nTotalWidth + nWidth
                        lvColumn.Width = nWidth
                        lvColumn.Alignment = lvwColumnRight
                        Set lvColumn = lvList.ColumnHeaders.Add(, , COL_NAM_OBSPNT_HEIGHT)
                        nWidth = COL_WID_OBSPNT_HEIGHT
                        nTotalWidth = nTotalWidth + nWidth
                        lvColumn.Width = nWidth
                        lvColumn.Alignment = lvwColumnRight
                    End If
                Else
                    Dim tRect As RECT
                    Dim nWidthScrollBar As Long
                    nWidthScrollBar = GetSystemMetrics(SM_CXVSCROLL) + 1 'チョット余裕をもたせるために1だけ足しとく。ギリギリだとはみ出す。四捨五入だから？
                    If GetClientRect(lvList.hWnd, tRect) = 0 Then Call Err.Raise(ERR_FATAL, , GetLastErrorMessage())
                    lvColumn.Width = CheckReturnMin((tRect.Right - tRect.Left - nWidthScrollBar) * Screen.TwipsPerPixelX - nTotalWidth, 0)
                    lvColumn.Alignment = lvwColumnLeft
                End If
#endif
        }


        //==========================================================================================
        /*[VB]
'2009/11 H.Nakamura
'指定されたオブジェクトがリストのアイテムとして追加するのに有効か評価する。
'
'引き数：
'objElement 対象とするオブジェクト。
'
'戻り値：
'リストのアイテムとして有効な場合 True を返す。
'それ以外の場合 False を返す。
Public Function ListViewInterface_IsEnable(ByVal objElement As Object) As Boolean
    ListViewInterface_IsEnable = False
    If m_nListType = LIST_VIEW_OBSERVATION_POINT_ECCENTRIC_CORRECT Then
        '有効な本点のみ。
        If Not objElement.Enable Or Not objElement.Genuine Then Exit Function
    '2009/11 H.Nakamura
    ElseIf m_nListType = LIST_VIEW_OBSERVATION_POINT_FIXED_LIST Then
        '有効な固定点のみ。
        If Not objElement.Enable Or Not objElement.Fixed Then Exit Function
    Else
        '本点は除外。
        If Not objElement.Enable Or objElement.Genuine Or objElement.Based Then Exit Function
    End If
    If SelectSession <> "" Then
        If SelectSession<> objElement.Session Then Exit Function
    End If
    ListViewInterface_IsEnable = True
End Function
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]

        //==========================================================================================
        /*[VB]
'2009/11 H.Nakamura
'オブジェクトを一意に識別するキーを取得する。
'
'引き数：
'objElement 対象とするオブジェクト。
'
'戻り値：オブジェクトを一意に識別するキーを返す。
Public Function ListViewInterface_GetKey(ByVal objElement As Object) As String
    ListViewInterface_GetKey = objElement.Key
End Function
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]

        //==========================================================================================
        /*[VB]
'2009/11 H.Nakamura
'リストアイテムのチェック値を取得する。
'
'引き数：
'objCheckObjects チェックオブジェクト。
'objElement 対象とするオブジェクト。
'sKey オブジェクトのキー。
'
'戻り値：リストアイテムのチェック値を返す。
Public Function ListViewInterface_IsCheck(ByVal objCheckObjects As Collection, ByVal objElement As Object, ByVal sKey As String) As Boolean
    Dim vItem As Variant
    ListViewInterface_IsCheck = LookupCollectionVariant(objCheckObjects, vItem, sKey)
End Function
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]

        //==========================================================================================
        /*[VB]
'2009/11 H.Nakamura
'サブアイテムを追加する。
'
'引き数：
'lvItem 対象とするリストアイテム。
'objElement 対象とするオブジェクト。
Public Sub ListViewInterface_AddSubItem(ByVal lvItem As ListItem, ByVal objElement As Object)
    Dim clsObservationPoint As ObservationPoint
    Set clsObservationPoint = objElement
    Dim lvSubItem As ListSubItem
    If m_bSession Then Set lvSubItem = lvItem.ListSubItems.Add(, , clsObservationPoint.Session)
    Set lvSubItem = lvItem.ListSubItems.Add(, , clsObservationPoint.DispNumber)
    Set lvSubItem = lvItem.ListSubItems.Add(, , clsObservationPoint.Name)
    If m_bTime Then
        Set lvSubItem = lvItem.ListSubItems.Add(, , Format$(clsObservationPoint.StrTimeJST, "yyyy/mm/dd hh:nn:ss"))
        Set lvSubItem = lvItem.ListSubItems.Add(, , Format$(clsObservationPoint.EndTimeJST, "yyyy/mm/dd hh:nn:ss"))
    End If
    If m_bCoord Then
        Dim clsCoordinatePoint As CoordinatePoint
        Set clsCoordinatePoint = clsObservationPoint.CoordinateObservation
        Dim nLatH As Long
        Dim nLatM As Long
        Dim nLatS As Double
        Dim nLonH As Long
        Dim nLonM As Long
        Dim nLonS As Double
        Dim nHeight As Double
        Dim vAlt As Variant
        Call clsCoordinatePoint.GetDMS(nLatH, nLatM, nLatS, nLonH, nLonM, nLonS, nHeight, vAlt, ACCOUNT_DECIMAL_SEC, "")
        Set lvSubItem = lvItem.ListSubItems.Add(, , CStr(nLatH) & "°" & Format$(nLatM, ACCOUNT_FORMAT_MIN) & "′" & FormatRound(nLatS, ACCOUNT_DECIMAL_SEC, ACCOUNT_FORMAT_SEC) & "″")
        Set lvSubItem = lvItem.ListSubItems.Add(, , CStr(nLonH) & "°" & Format$(nLonM, ACCOUNT_FORMAT_MIN) & "′" & FormatRound(nLonS, ACCOUNT_DECIMAL_SEC, ACCOUNT_FORMAT_SEC) & "″")
        Set lvSubItem = lvItem.ListSubItems.Add(, , FormatRound(nHeight, ACCOUNT_DECIMAL_HEIGHT))
    End If
End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]




    }
}
