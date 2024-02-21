using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using System.Windows.Forms;
using System.Xml.Linq;

namespace SurvLine
{
    internal class ListViewOperator
    {


        //'*******************************************************************************
        //'リストビュー操作
        //'
        //'チェックボックス付きリストビューを操作する。

        //Option Explicit

        //==========================================================================================
        /*[VB]
            'インプリメンテーション
            Private m_nSortIndex As Long 'ソートカラムインデックス。
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'インプリメンテーション
        private long m_nSortIndex;  //'ソートカラムインデックス。


        //'*******************************************************************************
        //'イベント


        public ListViewOperator()
        {
            Class_Initialize();
        }

        //==========================================================================================
        /*[VB]
            '初期化。
            Private Sub Class_Initialize()

                On Error GoTo ErrorHandler


                m_nSortIndex = -1
    
                Exit Sub


            ErrorHandler:
                Call mdlMain.ErrorExit


            End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /// <summary>
        /// '初期化。
        /// 
        /// </summary>
        private void Class_Initialize()
        {
            try
            {
                //On Error GoTo ErrorHandler

                m_nSortIndex = -1;

            }
            catch (Exception ex)
            {
                //ErrorHandler:
                //    Call mdlMain.ErrorExit
            }
        }



        //'*******************************************************************************
        //'メソッド


        //==========================================================================================
        /*[VB]
            'ソート。
            '
            '引き数：
            'lvList リストビュー。
            'nIndex ソートカラムインデックス。
            Public Sub Sort(ByVal lvList As ListView, ByVal nIndex As Long)
                If m_nSortIndex = nIndex Then
                    If lvList.SortOrder = lvwAscending Then
                        lvList.SortOrder = lvwDescending
                    Else
                        lvList.SortOrder = lvwAscending
                    End If
                Else
                    m_nSortIndex = nIndex
                    lvList.SortOrder = lvwAscending
                    End If
                lvList.SortKey = m_nSortIndex - 1
            End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /// <summary>
        /// 'ソート。
        /// '
        /// '引き数：
        ///     'lvList リストビュー。
        ///     'nIndex ソートカラムインデックス。
        /// 
        /// </summary>
        /// <param name="lvList"></param>
        /// <param name="nIndex"></param>
        public void Sort(ListView lvList, long nIndex)
        {

        }


        //==========================================================================================
        /*[VB]
            'アイテムすべてのチェックを設定する。
            '
            '引き数：
            'lvList リストビュー。
            Public Sub CheckAll(ByVal lvList As ListView, ByVal bChecked As Boolean)
                Dim lvItem As ListItem
                For Each lvItem In lvList.ListItems
                    lvItem.Checked = bChecked
                Next
            End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /// <summary>
        /// 'アイテムすべてのチェックを設定する。
        /// '
        /// '引き数：
        ///     'lvList リストビュー。
        /// 
        /// </summary>
        /// <param name="lvList"></param>
        /// <param name="bChecked"></param>
        public void CheckAll(ListView lvList, bool bChecked)
        {
            ListViewItem ListItem = new ListViewItem();

            int i = 0;
            foreach (var lvItem in lvList.Items)
            {
                lvList.Items[i].Checked = bChecked;
                i++;
            }

        }


        //==========================================================================================
        /*[VB]
            'チェックが有るか検査する。
            '
            '引き数：
            'lvList リストビュー。
            '
            '戻り値：
            'リストのアイテムが一つでもチェックONであれば True を返す。
            'それ以外の場合 False を返す。
            Public Function IsChecked(ByVal lvList As ListView) As Boolean
                Dim lvItem As ListItem
                For Each lvItem In lvList.ListItems
                    If lvItem.Checked Then Exit For
                Next
                IsChecked = Not lvItem Is Nothing
            End Function
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /// <summary>
        /// チェックが有るか検査する。
        ///
        /// 引き数：
        ///     lvList リストビュー。
        /// 
        /// </summary>
        /// <param name="lvList"></param>
        /// <returns>
        /// 戻り値：
        ///     'リストのアイテムが一つでもチェックONであれば True を返す。
        ///     'それ以外の場合 False を返す。
        /// </returns>
        public bool IsChecked(ListView lvList)
        {

            return true;

        }



    }
}
