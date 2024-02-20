//24/01/04 K.setoguchi@NV---------->>>>>>>>>>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace SurvLine
{
    internal class ChainList
    {
        //==========================================================================================
        /*[VB]
        '*******************************************************************************
        'チェーンリスト
        '
        'オブジェクトのチェーンリスト。

        Option Explicit
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'プロパティ
        Public Element As Object '要素。

        'インプリメンテーション
        Private m_clsPrevList As ChainList 'リストの前。
        Private m_clsNextList As ChainList 'リストの次。
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        'プロパティ
        */
        public object Element;                  //'要素。

        //'インプリメンテーション
        private ChainList m_clsPrevList;         //'リストの前。
        private ChainList m_clsNextList;         //'リストの次。
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '*******************************************************************************
        'プロパティ

        'リストの前。
        Property Set PrevList(ByVal clsPrevList As ChainList)
            Set m_clsPrevList = clsPrevList
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '*******************************************************************************
        'プロパティ

        'リストの前。
        */
        public ChainList PrevList(ChainList clsPrevList)
        {
            m_clsPrevList = clsPrevList;
            return this;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'リストの前。
        Property Get PrevList() As ChainList
            Set PrevList = m_clsPrevList
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'リストの前。
        public ChainList PrevList()
        {
            return m_clsPrevList;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'リストの次。
        Property Set NextList(ByVal clsNextList As ChainList)
            Set m_clsNextList = clsNextList
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'リストの次。
        public ChainList NextList(ChainList clsNextList)
        {
            m_clsNextList = clsNextList;
            return this;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'リストの次。
        Property Get NextList() As ChainList
            Set NextList = m_clsNextList
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'リストの次。
        public ChainList NextList()
        {
            return m_clsNextList;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'リストの先頭。
        Property Get HeadList() As ChainList
            Set HeadList = Me
            Do While Not HeadList.PrevList Is Nothing
                Set HeadList = HeadList.PrevList
            Loop
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'リストの先頭。
        public ChainList HeadList()
        {
            ChainList w_HeadList = (ChainList)Element;
            ChainList clsPrevList = m_clsPrevList;
            while (clsPrevList.NextList() != null)
            {
                w_HeadList = clsPrevList.NextList();
            }
            return w_HeadList;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'リストの最後尾。
        Property Get TailList() As ChainList
            Set TailList = Me
            Do While Not TailList.NextList Is Nothing
                Set TailList = TailList.NextList
            Loop
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'リストの最後尾。
        public ChainList TailList()
        {
            ChainList w_TailList = (ChainList)Element;
            ChainList clsChainList = m_clsNextList;
            while (clsChainList.NextList() != null)
            {
                w_TailList = clsChainList.NextList();
            }
            return w_TailList;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '後続要素数。
        Property Get FollowingCount() As Long

            FollowingCount = 0


            Dim clsChainList As ChainList
            Set clsChainList = m_clsNextList


            Do While Not clsChainList Is Nothing
                Set clsChainList = clsChainList.NextList
                FollowingCount = FollowingCount + 1
            Loop

        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'後続要素数。
        public long FollowingCount()
        {

            long w_FollowingCount = 0;


            ChainList clsChainList = m_clsNextList;


            while (clsChainList != null)
            {
                clsChainList = clsChainList.NextList();
                w_FollowingCount++;
            }

            return w_FollowingCount;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '*******************************************************************************
        'メソッド

        '終了。
        '
        'オブジェクトの参照を破棄する。
        Public Sub Terminate()
            Set Element = Nothing
            Set m_clsPrevList = Nothing
            Set m_clsNextList = Nothing
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '*******************************************************************************
        'メソッド
        
        '終了。
        '
        'オブジェクトの参照を破棄する。
        */
        public void Terminate()
        {
            Element = null;
            m_clsPrevList = null;
            m_clsNextList = null;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'リストの前に要素を挿入する。
        '
        '引き数：
        'objElement 挿入するオブジェクト。
        '
        '戻り値：新しく挿入した ChainList オブジェクト。
        Public Function InsertPrev(ByVal objElement As Object) As ChainList
            Dim clsChainList As New ChainList
            Set clsChainList.Element = objElement
            If Not m_clsPrevList Is Nothing Then Set m_clsPrevList.NextList = clsChainList
            Set clsChainList.PrevList = m_clsPrevList
            Set clsChainList.NextList = Me
            Set m_clsPrevList = clsChainList
            Set InsertPrev = clsChainList
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        'リストの前に要素を挿入する。
        '
        '引き数：
        'objElement 挿入するオブジェクト。
        '
        '戻り値：新しく挿入した ChainList オブジェクト。
        */
        public ChainList InsertPrev(object objElement)
        {
            ChainList w_clsPrevList = m_clsPrevList;
            ChainList clsChainList = new ChainList();
            clsChainList.Element = (ChainList)objElement;
            if (m_clsPrevList != null)
            {
                _ = m_clsPrevList.NextList(clsChainList);
            }
            _ = clsChainList.PrevList(m_clsPrevList);
            _ = clsChainList.NextList((ChainList)Element);
            m_clsPrevList = clsChainList;
            return clsChainList;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'リストの次に要素を挿入する。
        '
        '引き数：
        'objElement 挿入するオブジェクト。
        '
        '戻り値：新しく挿入した ChainList オブジェクト。
        Public Function InsertNext(ByVal objElement As Object) As ChainList
            Dim clsChainList As New ChainList
            Set clsChainList.Element = objElement
            If Not m_clsNextList Is Nothing Then Set m_clsNextList.PrevList = clsChainList
            Set clsChainList.PrevList = Me
            Set clsChainList.NextList = m_clsNextList
            Set m_clsNextList = clsChainList
            Set InsertNext = clsChainList
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        'リストの次に要素を挿入する。
        '
        '引き数：
        'objElement 挿入するオブジェクト。
        '
        '戻り値：新しく挿入した ChainList オブジェクト。
        */
        public ChainList InsertNext(object objElement)
        {
            ChainList clsChainList = new ChainList();
            clsChainList.Element = (ChainList)objElement;
            if (m_clsNextList != null)
            {
                _ = m_clsNextList.PrevList(clsChainList);
            }
            _ = clsChainList.PrevList((ChainList)Element);
            _ = clsChainList.NextList(m_clsNextList);
            m_clsNextList = clsChainList;
            return clsChainList;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'リストを削除する。
        '
        '自オブジェクトを破棄する。
        Public Sub Remove()
            If Not m_clsPrevList Is Nothing Then Set m_clsPrevList.NextList = m_clsNextList
            If Not m_clsNextList Is Nothing Then Set m_clsNextList.PrevList = m_clsPrevList
            Call Terminate
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        'リストを削除する。
        '
        '自オブジェクトを破棄する。
        */
        public void Remove()
        {
            if (m_clsPrevList != null)
            {
                _ = m_clsPrevList.NextList(m_clsNextList);
            }
            if (m_clsNextList != null)
            {
                _ = m_clsNextList.PrevList(m_clsPrevList);
            }
            Terminate();
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'リストの前を削除する。
        Public Sub RemovePrev()
            If Not m_clsPrevList Is Nothing Then Call m_clsPrevList.Remove
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'リストの前を削除する。
        public void RemovePrev()
        {
            if (m_clsPrevList != null)
            {
                m_clsPrevList.Remove();
            }
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'リストの次を削除する。
        Public Sub RemoveNext()
            If Not m_clsNextList Is Nothing Then Call m_clsNextList.Remove
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'リストの次を削除する。
        public void RemoveNext()
        {
            if (m_clsNextList != null)
            {
                m_clsNextList.Remove();
            }
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'リストすべてを破棄する。
        '
        '前後すべてのリストを破棄する。
        Public Sub RemoveAll()
            Dim clsChainList As ChainList
            Dim clsRemove As ChainList
            '前方を削除。
            Set clsChainList = m_clsPrevList
            Do While Not clsChainList Is Nothing
                Set clsRemove = clsChainList
                Set clsChainList = clsRemove.PrevList
                Call clsRemove.Terminate
            Loop
            '後方を削除。
            Set clsChainList = m_clsNextList
            Do While Not clsChainList Is Nothing
                Set clsRemove = clsChainList
                Set clsChainList = clsRemove.NextList
                Call clsRemove.Terminate
            Loop
            '自分をクリア。
            Call Terminate
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        'リストすべてを破棄する。
        '
        '前後すべてのリストを破棄する。
        */
        public void RemoveAll()
        {
            ChainList clsChainList;
            ChainList clsRemove;
            //'前方を削除。
            clsChainList = m_clsPrevList;
            while (clsChainList != null)
            {
                clsRemove = clsChainList;
                clsChainList = clsRemove.PrevList();
                clsRemove.Terminate();
            }
            //'後方を削除。
            clsChainList = m_clsNextList;
            while (clsChainList != null)
            {
                clsRemove = clsChainList;
                clsChainList = clsRemove.NextList();
                clsRemove.Terminate();
            }
            //'自分をクリア。
            Terminate();
            return;
        }
        //==========================================================================================

#if false

        //24/01/04 K.setoguchi@NV---------->>>>>>>>>>
        //'*******************************************************************************
        //'チェーンリスト
        //'
        //'オブジェクトのチェーンリスト。
        //
        //Option Explicit
        //
        //'プロパティ
        public Object Element;          // As Object '要素。
        //
        //'インプリメンテーション
        public ChainList m_clsPrevList; //As ChainList 'リストの前。
        public ChainList m_clsNextList; //As ChainList 'リストの次。

        //'*******************************************************************************
        //'プロパティ
        //
        //***************************************************
        //'リストの前。
        //Property Set PrevList(ByVal clsPrevList As ChainList)
        //    Set m_clsPrevList = clsPrevList
        //End Property
        //
        //'リストの前。
        //Property Get PrevList() As ChainList
        //    Set PrevList = m_clsPrevList
        //End Property
        //-------------------------------
        public ChainList PrevList
        {
            get{return m_clsPrevList; }
            set{m_clsPrevList = value; }
        }

        //***************************************************
        //'リストの次。
        //Property Set NextList(ByVal clsNextList As ChainList)
        //    Set m_clsNextList = clsNextList
        //End Property
        //
        //'リストの次。
        //Property Get NextList() As ChainList
        //    Set NextList = m_clsNextList
        //End Property
        //
        public ChainList NextList
        {
            set { m_clsPrevList = value; }
            get { return m_clsPrevList; }
        }

        //***************************************************
        //'リストの先頭。
        //Property Get HeadList() As ChainList
        //    Set HeadList = Me
        //    Do While Not HeadList.PrevList Is Nothing
        //        Set HeadList = HeadList.PrevList
        //    Loop
        //End Property
        public ChainList HeadList
        {
            get
            {
                ChainList HeadList = this;
                for (; ; )
                {
                    if (HeadList.PrevList != null)
                    {
                        HeadList = HeadList.PrevList;
                    }
                    else
                    {
                        break;
                    }
                }
                return HeadList;
            }
        }

        //***************************************************
        //'リストの最後尾。
        //Property Get TailList() As ChainList
        //    Set TailList = Me
        //    Do While Not TailList.NextList Is Nothing
        //        Set TailList = TailList.NextList
        //    Loop
        //End Property
        //
        private ChainList TailList
        {
            get
            {
                ChainList TailList = this;
                for (; ; )
                {
                    if (TailList.NextList != null)
                    {
                        TailList = TailList.NextList;
                    }
                    else
                    {
                        break;
                    }
                }
                return TailList;
            }
        }

        //***************************************************
        //'後続要素数。
        //Property Get FollowingCount() As Long
        //
        //    FollowingCount = 0
        //
        //
        //    Dim clsChainList As ChainList
        //    Set clsChainList = m_clsNextList
        //
        //
        //    Do While Not clsChainList Is Nothing
        //        Set clsChainList = clsChainList.NextList
        //        FollowingCount = FollowingCount + 1
        //    Loop
        //
        //End Property
        //
        private long FollowingCount
        {
            get
            {
                long FollowingCount = 0;
                ChainList clsChainList;
                clsChainList = m_clsNextList;

                for (; ; )
                {
                    if (clsChainList != null)
                    {
                        clsChainList = clsChainList.NextList;
                        FollowingCount += 1;
                    }
                    else
                    {
                        break;
                    }
                }
                return FollowingCount;
            }
        }

        //<<<<<<<<<-----------24/01/04 K.setoguchi@NV
#endif

    }
}
//<<<<<<<<<-----------24/01/04 K.setoguchi@NV
