using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SurvLine
{
    internal class ListViewInterface
    {

        //'*******************************************************************************
        //'リストビューインターフェース

        //Option Explicit

        //'*******************************************************************************

        //==========================================================================================
        /*[VB]
            'プロパティ
            'SortKey の初期値。
            Property Get DefaultSortKey() As Integer
            End Property
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]

        //'プロパティ

        //'SortKey の初期値。
        public int DefaultSortKey()
        {
            return 0;
        }

        //==========================================================================================
        /*[VB]
            '*******************************************************************************
            'メソッド

            'リストの初期化。
            '
            'カラムを作り初期化する。
            '
            '引き数：
            'lvList 対象リストビュー。
            Public Sub Initialize(ByVal lvList As ListView)
            End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        public void Initialize(ListView lvList)
        {
            return;
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
            Public Function IsEnable(ByVal objElement As Object) As Boolean
            End Function
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /// <summary>
        ///     '指定されたオブジェクトがリストのアイテムとして追加するのに有効か評価する。
        ///     '
        ///     '引き数：
        ///     'objElement 対象とするオブジェクト。
        /// 
        /// </summary>
        /// <param name="objElement"></param>
        /// <returns>
        ///     '戻り値：
        ///     'リストのアイテムとして有効な場合 True を返す。
        ///     'それ以外の場合 False を返す。
        /// </returns>
        public bool IsEnable(Object objElement)
        {
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
            Public Function GetKey(ByVal objElement As Object) As String
            End Function
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        public string GetKey(Object objElement)
        {
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
            Public Function IsCheck(ByVal objCheckObjects As Collection, ByVal objElement As Object, ByVal sKey As String) As Boolean
            End Function
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /// <summary>
        /// 'リストアイテムのチェック値を取得する。
        /// '
        /// '引き数：
        /// 'objCheckObjects チェックオブジェクト。
        /// 'objElement 対象とするオブジェクト。
        /// 'sKey オブジェクトのキー。
        /// 
        /// </summary>
        /// <param name="objCheckObjects"></param>
        /// <param name="objElement"></param>
        /// <param name="sKey"></param>
        /// <returns>
        ///    '戻り値：リストアイテムのチェック値を返す。
        /// </returns>
        public bool IsCheck(Collection objCheckObjects, object objElement, string sKey)
        {
            return true;
        }

        //==========================================================================================
        /*[VB]
            'サブアイテムを追加する。
            '
            '引き数：
            'lvItem 対象とするリストアイテム。
            'objElement 対象とするオブジェクト。
            Public Sub AddSubItem(ByVal lvItem As ListItem, ByVal objElement As Object)
            End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /// <summary>
        /// 'サブアイテムを追加する。
        /// '
        /// '引き数：
        /// 'lvItem 対象とするリストアイテム。
        /// 'objElement 対象とするオブジェクト。
        /// </summary>
        /// <param name="lvItem"></param>
        /// <param name="objElement"></param>
        public void AddSubItem(ListView lvItem, Object objElement)
        {
            return;
        }

    }
}
