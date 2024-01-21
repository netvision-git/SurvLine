//24/01/04 K.setoguchi@NV---------->>>>>>>>>>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace SurvLine
{
    internal class ChainList
    {
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






    }
}
//<<<<<<<<<-----------24/01/04 K.setoguchi@NV
