using SurvLine.mdl;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static SurvLine.mdl.MdlListPane;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using static SurvLine.mdl.DEFINE;
using static SurvLine.mdl.MdlNSDefine;
using static SurvLine.mdl.MdlUtility;
using static SurvLine.mdl.MdlAccountMake;
using static SurvLine.mdl.MdlNSGUI;
using static SurvLine.mdl.MdlNSSDefine;


namespace SurvLine
{
    internal class ListViewBaseLineVector : ListViewInterface
    {


        //'*******************************************************************************
        //'基線ベクトルリストビュー
        //
        //Option Explicit


        public ListViewBaseLineVector() { }

        //==========================================================================================
        /*[VB]
        'インターフェース
        Implements ListViewInterface 'リストビューインターフェース。

        '基線ベクトルリストカラム番号。
        Private Enum COL_NUM_VECTOR
            COL_NUM_VECTOR_CHECK = 0 'チェックボックス。
            COL_NUM_VECTOR_SESSION 'セッション名。
            COL_NUM_VECTOR_STRPOINT '始点。
            COL_NUM_VECTOR_ENDPOINT '終点。
            COL_NUM_VECTOR_STRTIME '観測開始日時。
            COL_NUM_VECTOR_ENDTIME '観測終了日時。
            COL_NUM_VECTOR_COUNT '番号数。
        End Enum
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'インターフェース
        //private interface ListViewInterface;    //'リストビューインターフェース。

        //'基線ベクトルリストカラム番号。
        private enum COL_NUM_VECTOR
        {
            COL_NUM_VECTOR_CHECK = 0,   //'チェックボックス。
            COL_NUM_VECTOR_SESSION,     //'セッション名。
            COL_NUM_VECTOR_STRPOINT,    // '始点。
            COL_NUM_VECTOR_ENDPOINT,    // '終点。
            COL_NUM_VECTOR_STRTIME,     // '観測開始日時。
            COL_NUM_VECTOR_ENDTIME,     // '観測終了日時。
            COL_NUM_VECTOR_COUNT,       //'番号数。
        }


        private const string COL_NAM_VECTOR_CHECK = "";                   //'基線ベクトルリストカラム名称、チェックボックス。
        private const string COL_NAM_VECTOR_SESSION = "セッション名";     //'基線ベクトルリストカラム名称、セッション名。
        private const string COL_NAM_VECTOR_STRPOINT = "始点";            //'基線ベクトルリストカラム名称、始点。
        private const string COL_NAM_VECTOR_ENDPOINT = "終点";            //'基線ベクトルリストカラム名称、終点。
        private const string COL_NAM_VECTOR_STRTIME = "観測開始日時(JST)";    //'基線ベクトルリストカラム名称、観測開始日時。
        private const string COL_NAM_VECTOR_ENDTIME = "観測終了日時(JST)";    //'基線ベクトルリストカラム名称、観測終了日時。

        private const long COL_WID_VECTOR_CHECK = 384;      //'基線ベクトルリストカラム幅(Twips)、チェックボックス。
        private const long COL_WID_VECTOR_SESSION = 1152;   //'基線ベクトルリストカラム幅(Twips)、セッション名。
        private const long COL_WID_VECTOR_STRPOINT = 1152;  //'基線ベクトルリストカラム幅(Twips)、始点。
        private const long COL_WID_VECTOR_ENDPOINT = 1152;  //'基線ベクトルリストカラム幅(Twips)、終点。
        private const long COL_WID_VECTOR_STRTIME = 2304;   //'基線ベクトルリストカラム幅(Twips)、観測開始日時。
        private const long COL_WID_VECTOR_ENDTIME = 2304;   //'基線ベクトルリストカラム幅(Twips)、観測終了日時。

        //'*******************************************************************************
        //'インターフェース

        //==========================================================================================
        /*[VB]
            'SortKey の初期値。
            Property Get ListViewInterface_DefaultSortKey() As Integer
                ListViewInterface_DefaultSortKey = COL_NUM_VECTOR_SESSION
            End Property
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        public int ListViewInterface_DefaultSortKey()
        {
            return (int)COL_NUM_VECTOR.COL_NUM_VECTOR_SESSION;
        }

        //==========================================================================================
        /*[VB]
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
                Set lvColumn = lvList.ColumnHeaders.Add(, , COL_NAM_VECTOR_CHECK)
                nWidth = COL_WID_VECTOR_CHECK
                    nTotalWidth = nTotalWidth + nWidth
                lvColumn.Width = nWidth
                lvColumn.Alignment = lvwColumnLeft
                Set lvColumn = lvList.ColumnHeaders.Add(, , COL_NAM_VECTOR_SESSION)
                nWidth = COL_WID_VECTOR_SESSION
                nTotalWidth = nTotalWidth + nWidth
                lvColumn.Width = nWidth
                lvColumn.Alignment = lvwColumnLeft
                Set lvColumn = lvList.ColumnHeaders.Add(, , COL_NAM_VECTOR_STRPOINT)
                nWidth = COL_WID_VECTOR_STRPOINT
                nTotalWidth = nTotalWidth + nWidth
                lvColumn.Width = nWidth
                lvColumn.Alignment = lvwColumnLeft
                Set lvColumn = lvList.ColumnHeaders.Add(, , COL_NAM_VECTOR_ENDPOINT)
                nWidth = COL_WID_VECTOR_ENDPOINT
                nTotalWidth = nTotalWidth + nWidth
                lvColumn.Width = nWidth
                lvColumn.Alignment = lvwColumnLeft
                Set lvColumn = lvList.ColumnHeaders.Add(, , COL_NAM_VECTOR_STRTIME)
                nWidth = COL_WID_VECTOR_STRTIME
                nTotalWidth = nTotalWidth + nWidth
                lvColumn.Width = nWidth
                lvColumn.Alignment = lvwColumnLeft
                Set lvColumn = lvList.ColumnHeaders.Add(, , COL_NAM_VECTOR_ENDTIME)
                nWidth = COL_WID_VECTOR_ENDTIME
                nTotalWidth = nTotalWidth + nWidth
                lvColumn.Width = nWidth
                lvColumn.Alignment = lvwColumnLeft
            End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        public void ListViewInterface_Initialize(ListView lvList)
        {
            //'カラムの初期化。
            ColumnHeader lvColumn;
            long nWidth;
            long nTotalWidth;


            string sp = "";         //add
            lvList.Columns.Clear(); //add

#if false   //サンプル
    lvMatches.Columns.Add(new ColumnHeader { Text = "Code", Width = 44 });
#endif

            //lvColumn = lvList.ColumnHeaders.Add("{sp} : {sp} : {COL_NUM_VECTOR.COL_NAM_VECTOR_CHECK}");
            lvList.Columns.Add(new ColumnHeader { Text = "{sp} : {sp} : {COL_NUM_VECTOR.COL_NAM_VECTOR_CHECK}" });
            //nWidth = COL_WID_VECTOR_CHECK;
            //nTotalWidth += nWidth;
            //lvColumn.Width = (int)nWidth;
            //lvColumn.Alignment = lvwColumnLeft;


            //    Set lvColumn = lvList.ColumnHeaders.Add(, , COL_NAM_VECTOR_SESSION)
            lvList.Columns.Add(new ColumnHeader { Text = "{sp} : {sp} : {COL_NUM_VECTOR.COL_NAM_VECTOR_SESSION}" });
            //nWidth = COL_WID_VECTOR_SESSION
            //nTotalWidth = nTotalWidth + nWidth
            //lvColumn.Width = nWidth
            //lvColumn.Alignment = lvwColumnLeft

            //Set lvColumn = lvList.ColumnHeaders.Add(, , COL_NAM_VECTOR_STRPOINT)
            lvList.Columns.Add(new ColumnHeader { Text = "{sp} : {sp} : {COL_NUM_VECTOR.COL_NAM_VECTOR_STRPOINT}" });
            //nWidth = COL_WID_VECTOR_STRPOINT
            //nTotalWidth = nTotalWidth + nWidth
            //lvColumn.Width = nWidth
            //lvColumn.Alignment = lvwColumnLeft

            //Set lvColumn = lvList.ColumnHeaders.Add(, , COL_NAM_VECTOR_ENDPOINT)
            lvList.Columns.Add(new ColumnHeader { Text = "{sp} : {sp} : {COL_NUM_VECTOR.COL_NAM_VECTOR_ENDPOINT}" });
            //nWidth = COL_WID_VECTOR_ENDPOINT
            //nTotalWidth = nTotalWidth + nWidth
            //lvColumn.Width = nWidth
            //lvColumn.Alignment = lvwColumnLeft

            //Set lvColumn = lvList.ColumnHeaders.Add(, , COL_NAM_VECTOR_STRTIME)
            lvList.Columns.Add(new ColumnHeader { Text = "{sp} : {sp} : {COL_NUM_VECTOR.COL_NAM_VECTOR_STRTIME}" });
            //nWidth = COL_WID_VECTOR_STRTIME
            //nTotalWidth = nTotalWidth + nWidth
            //lvColumn.Width = nWidth
            //lvColumn.Alignment = lvwColumnLeft

            //Set lvColumn = lvList.ColumnHeaders.Add(, , COL_NAM_VECTOR_ENDTIME)
            lvList.Columns.Add(new ColumnHeader { Text = "{sp} : {sp} : {COL_NUM_VECTOR.COL_NAM_VECTOR_ENDTIME}" });
            //nWidth = COL_WID_VECTOR_ENDTIME
            //nTotalWidth = nTotalWidth + nWidth
            //lvColumn.Width = nWidth
            //lvColumn.Alignment = lvwColumnLeft


        }



        //==========================================================================================
        /*[VB]
            '指定されたオブジェクトがリストのアイテムとして追加するのに有効か評価する。
            '
            '引き数：
            'objElement 対象とするオブジェクト。
            '
            '戻り値：
            'リストのアイテムとして有効な場合 True を返す。
            'それ以外の場合 False を返す。
            Public Function ListViewInterface_IsEnable(ByVal objElement As Object) As Boolean
                ListViewInterface_IsEnable = objElement.Enable And objElement.Analysis <= ANALYSIS_STATUS_FIX
            End Function
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        public bool ListViewInterface_IsEnable(Object objElement)
        {
            //bool ListViewInterface_IsEnable = objElement.Enable & objElement.Analysis <= ANALYSIS_STATUS_FIX;

            return true;
        }

        //==========================================================================================
        /*[VB]
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
        public string ListViewInterface_GetKey(Object objElement)
        {
            //  return objElement.Key;
            return "";
        }

        //==========================================================================================
        /*[VB]
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
        public bool ListViewInterface_IsCheck(Collection objCheckObjects, Object objElement, string sKey)
        {
            bool ListViewInterface_IsCheck = false;
            string vItem = "";



            ListViewInterface_IsCheck = LookupCollectionVariant(objCheckObjects, ref vItem, sKey);


            return ListViewInterface_IsCheck;
        }

        //==========================================================================================
        /*[VB]
            'サブアイテムを追加する。
            '
            '引き数：
            'lvItem 対象とするリストアイテム。
            'objElement 対象とするオブジェクト。
            Public Sub ListViewInterface_AddSubItem(ByVal lvItem As ListItem, ByVal objElement As Object)
                Dim clsBaseLineVector As BaseLineVector
                Set clsBaseLineVector = objElement
                Dim lvSubItem As ListSubItem
                Set lvSubItem = lvItem.ListSubItems.Add(, , clsBaseLineVector.Session)
                Set lvSubItem = lvItem.ListSubItems.Add(, , clsBaseLineVector.StrPoint.DispNumber)
                Set lvSubItem = lvItem.ListSubItems.Add(, , clsBaseLineVector.EndPoint.DispNumber)
                Set lvSubItem = lvItem.ListSubItems.Add(, , Format$(clsBaseLineVector.StrTimeJST, "yyyy/mm/dd hh:nn:ss"))
                Set lvSubItem = lvItem.ListSubItems.Add(, , Format$(clsBaseLineVector.EndTimeJST, "yyyy/mm/dd hh:nn:ss"))
            End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        public void ListViewInterface_AddSubItem(ListView lvItem, Object objElement)
        {
 
            BaseLineVector clsBaseLineVector;

            clsBaseLineVector = (BaseLineVector)objElement;

            ListViewItem lvSubItem = new ListViewItem();

            lvSubItem.SubItems.Add(clsBaseLineVector.Session);
//            lvSubItem.SubItems.Add(clsBaseLineVector.StrPoint.DispNumber);
//            lvSubItem.SubItems.Add(clsBaseLineVector.EndPoint.DispNumber);
//            lvSubItem.SubItems.Add(clsBaseLineVector.StrTimeJST.ToString("yyyy/mm/dd hh:nn:ss"));
//            lvSubItem.SubItems.Add(clsBaseLineVector.EndTimeJST.ToString("yyyy/mm/dd hh:nn:ss"));

            lvItem.Items.Add(lvSubItem);

        }



    }
}
