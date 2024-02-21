using SurvLine.mdl;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace SurvLine
{
    internal class ObjectList : ListViewObservationPoint
    {


        //==========================================================================================
        /*[VB]
            '*******************************************************************************
            'オブジェクトリスト
            '
            'よく使われる一般的なオブジェクトのリストを操作する。

            Option Explicit

            'インプリメンテーション
            Private m_clsListViewInterface As ListViewInterface 'リストビューインターフェース。
            Private m_objObjects As Collection 'オブジェクトコレクション。要素はオブジェクト。キーはオブジェクトのキー。
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'*******************************************************************************
        //'オブジェクトリスト
        //'
        //'よく使われる一般的なオブジェクトのリストを操作する。
        //'インプリメンテーション
        private ListViewInterface m_clsListViewInterface;   //'リストビューインターフェース。
        private Collection m_objObjects;                //'オブジェクトコレクション。要素はオブジェクト。キーはオブジェクトのキー。

        //  public object Nothing { get; private set; }


        public ObjectList()
        {
        }



        //==========================================================================================
        /*[VB]
            '*******************************************************************************
            'プロパティ

            'オブジェクトコレクション。
            Property Get Objects() As Collection
                Set Objects = m_objObjects
            End Property

            '選択オブジェクト。
            Property Get SelectedObject(ByVal lvList As ListView) As Object
                Set SelectedObject = Nothing
                Dim lvItem As ListItem
                Set lvItem = lvList.SelectedItem
                If lvItem Is Nothing Then Exit Function
                Dim sKey As String
                sKey = Mid(lvItem.Key, Len(KEYPREFIX) + 1)
                Set SelectedObject = m_objObjects.Item(sKey)
            End Property
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'*******************************************************************************
        //'プロパティ

        public Collection Objects()
        {
            return m_objObjects;
        }

        public object SelectedObject(ListView lvList)
        {
            object SelectedObject = null;
            ListView lvItem;

            lvItem = lvList;
            if (lvItem.Tag != null)
            {
                return lvItem.Tag;
            }

            string sKey;
//            sKey = MdiVBfunctions.Mid(lvItem.Key, MdlNSDefine.KEYPREFIX.Length + 1);
//            SelectedObject = (object)m_objObjects.Item($"{sKey}");
            return SelectedObject;

        }


        //'*******************************************************************************
        //'メソッド


        //==========================================================================================
        /*[VB]
            'リストの初期化。
            '
            'カラムを作り初期化する。
            '
            '引き数：
            'lvList 対象とするリストビュー。
            'clsListViewInterface ListViewInterface オブジェクト。
            Public Sub Initialize(ByVal lvList As ListView, ByVal clsListViewInterface As ListViewInterface)
                Set m_clsListViewInterface = clsListViewInterface
                Call m_clsListViewInterface.Initialize(lvList)
            End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /// <summary>
        ///    'リストの初期化。
        ///    '
        ///    'カラムを作り初期化する。
        ///    '
        ///    '引き数：
        ///    'lvList 対象とするリストビュー。
        ///    'clsListViewInterface ListViewInterface オブジェクト。
        /// 
        /// </summary>
        /// <param name="lvList"></param>
        /// <param name="clsListViewInterface"></param>
        //public void Initialize(ListView lvList, ListViewInterface clsListViewInterface)
        public void Initialize(ListView lvList, ListViewInterface clsListViewInterface)
        {
            m_clsListViewInterface = clsListViewInterface;

            m_clsListViewInterface.Initialize(lvList);

            ListViewObservationPoint listViewObservationPoint = (ListViewObservationPoint)m_clsListViewInterface;

            listViewObservationPoint.ListViewInterface_Initialize(lvList);


        }

        //==========================================================================================
        /*[VB]
            'リストの作成。
            '
            'clsChainList で指定されるオブジェクトでリストを作成する。
            'objCheckObjects でリストアイテムのチェックの有無を指定する。
            '
            '引き数：
            'lvList 対象とするリストビュー。
            'clsChainList オブジェクトリストのヘッダー。
            'objCheckObjects チェックオブジェクト。
            Public Sub MakeFromChainList(ByVal lvList As ListView, ByVal clsChainList As ChainList, Optional ByVal objCheckObjects As Collection = Nothing)

                Set m_objObjects = New Collection
    
                'リストの作成。
                Set m_objObjects = New Collection
                Do While Not clsChainList Is Nothing
                    If m_clsListViewInterface.IsEnable(clsChainList.Element) Then
                        'キー。
                        Dim sKey As String
                        sKey = m_clsListViewInterface.GetKey(clsChainList.Element)
                        'アイテムの追加。
                        Dim lvItem As ListItem
                        Set lvItem = AddItem(lvList, -1, sKey, clsChainList.Element)
                        'コレクションの追加。
                        Call m_objObjects.Add(clsChainList.Element, sKey)
                        'チェック。
                        lvItem.Checked = m_clsListViewInterface.IsCheck(objCheckObjects, clsChainList.Element, sKey)
                    End If
                    Set clsChainList = clsChainList.NextList
                Loop


                lvList.SortKey = m_clsListViewInterface.DefaultSortKey
                If lvList.ListItems.Count > 0 Then lvList.ListItems(1).Selected = True

            End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        public void MakeFromChainList(ListView lvList, ChainList clsChainList, Collection objCheckObjects)
        {
            objCheckObjects = null;

            //'リストの作成。
            Collection m_objObjects = new Collection();


        }

        //==========================================================================================
        /*[VB]
            'リストの作成。
            '
            'objElements で指定されるオブジェクトでリストを作成する。
            'objCheckObjects でリストアイテムのチェックの有無を指定する。
            '
            '引き数：
            'lvList 対象とするリストビュー。
            'objElements オブジェクトのコレクション。要素は Object。キーは任意。
            'objCheckObjects チェックオブジェクト。
            Public Sub MakeFromCollection(ByVal lvList As ListView, ByVal objElements As Collection, Optional ByVal objCheckObjects As Collection = Nothing)

                Set m_objObjects = New Collection
    
                'リストの作成。
                Set m_objObjects = New Collection
                Dim objElement As Object
                For Each objElement In objElements
                    If m_clsListViewInterface.IsEnable(objElement) Then
                        'キー。
                        Dim sKey As String
                        sKey = m_clsListViewInterface.GetKey(objElement)
                        'アイテムの追加。
                        Dim lvItem As ListItem
                        Set lvItem = AddItem(lvList, -1, sKey, objElement)
                        'コレクションの追加。
                        Call m_objObjects.Add(objElement, sKey)
                        'チェック。
                        lvItem.Checked = m_clsListViewInterface.IsCheck(objCheckObjects, objElement, sKey)
                    End If
                Next


                lvList.SortKey = m_clsListViewInterface.DefaultSortKey
                If lvList.ListItems.Count > 0 Then lvList.ListItems(1).Selected = True

            End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        public void MakeFromCollection(ListView lvList, Collection objElements, Collection objCheckObjects)
        {
            objCheckObjects = null;

            //'リストの作成。
            Collection m_objObjects = new Collection();
            object objElement;




        }


        //==========================================================================================
        /*[VB]
            'チェックオブジェクトを取得する。
            '
            'リストのチェックがONであるオブジェクトをコレクションに設定して返す。
            '
            '引き数：
            'lvList 対象とするリストビュー。
            '
            '戻り値：チェックオブジェクト。要素はオブジェクト、キーはオブジェクトのキー。
            Public Function GetSelectedObjects(ByVal lvList As ListView) As Collection
                Dim objObjects As New Collection
                Dim lvItem As ListItem
                For Each lvItem In lvList.ListItems
                    If lvItem.Checked Then
                        Dim sKey As String
                        sKey = Mid(lvItem.Key, Len(KEYPREFIX) + 1)
                        Call objObjects.Add(m_objObjects.Item(sKey), sKey)
                    End If
                Next
                Set GetSelectedObjects = objObjects
            End Function
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        public Collection GetSelectedObjects(ListView lvList)
        {
            Collection objObjects = new Collection();
            ListView lvItem;




            return objObjects;
        }

        //==========================================================================================
        /*[VB]
            'チェックオブジェクトのキーを取得する。
            '
            'リストのチェックがONであるオブジェクトのキーをコレクションに設定して返す。
            '
            '引き数：
            'lvList 対象とするリストビュー。
            '
            '戻り値：チェックオブジェクトのキー。要素はオブジェクトのキー、キーはオブジェクトのキー。
            Public Function GetSelectedKeys(ByVal lvList As ListView) As Collection
                Dim objObjects As New Collection
                Dim lvItem As ListItem
                For Each lvItem In lvList.ListItems
                    If lvItem.Checked Then
                        Dim sKey As String
                        sKey = Mid(lvItem.Key, Len(KEYPREFIX) + 1)
                        Call objObjects.Add(sKey, sKey)
                    End If
                Next
                Set GetSelectedKeys = objObjects
            End Function
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        public Collection GetSelectedKeys(ListView lvList)
        {
            Collection objObjects = new Collection();
            ListView lvItem;


            return objObjects;
        }

        //==========================================================================================
        /*[VB]
            '*******************************************************************************
            'インプリメンテーション

            'アイテムの追加。
            '
            '引き数：
            'lvList 対象とするリストビュー。
            'nIndex アイテムを追加するリストインデックスの値。最後尾に追加する場合は負の値を指定する。
            'sKey キー。対象とするオブジェクトのキー。
            'objElement 対象とするオブジェクト。
            '
            '戻り値：追加した ListItem オブジェクト。
            Private Function AddItem(ByVal lvList As ListView, ByVal nIndex As Long, ByVal sKey As String, ByVal objElement As Object) As ListItem

                'アイテムの追加。
                Dim lvItem As ListItem
                If nIndex< 0 Then
                    Set lvItem = lvList.ListItems.Add(, KEYPREFIX & sKey, "")
                Else
                    Set lvItem = lvList.ListItems.Add(nIndex, KEYPREFIX & sKey, "")
                End If
    
                'サブアイテムの追加。
                Call m_clsListViewInterface.AddSubItem(lvItem, objElement)

                Set AddItem = lvItem


            End Function
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        private ListView AddItem(ListView lvList, long nIndex, string sKey, Object objElement)
        {

//            ListView lvItem;


            string sp = " ";


            if (nIndex < 0)
            {
                // lvItem =  
                lvList.Items.Add($"{sp}{MdlNSDefine.KEYPREFIX}{sKey}{sp}");
            }
            else
            {
                //lvItem =
                lvList.Items.Add($"{nIndex}:{MdlNSDefine.KEYPREFIX}{sKey}, {sp}");
            }



            //'サブアイテムの追加。
            m_clsListViewInterface.AddSubItem(lvList, objElement);


            return lvList;
        }

    }
}
