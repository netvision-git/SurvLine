using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using System.Windows.Forms;
using static SurvLine.ClickObjectInterface;

namespace SurvLine
{
    public class ClickArea
    {
        //==========================================================================================
        /*[VB]
        '*******************************************************************************
        'クリックエリア
        '
        '配置図上でのオブジェクトのクリックを評価するためのオブジェクト群。
        'ClickManager が最上位で管理する。
        '全部のオブジェクトをいちいち評価するのは手間なので、配置図の論理エリアをいくつかのクリックエリアに分ける。
        '基線ベクトルや移動点、固定点などそれぞれに対応したクリックオブジェクトを登録する。

        Option Explicit
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'プロパティ
        Public SX As Double '論理エリア上のクリックエリア左辺位置(ピクセル)。
        Public SY As Double '論理エリア上のクリックエリア上辺位置(ピクセル)。
        Public EX As Double '論理エリア上のクリックエリア右辺位置(ピクセル)。
        Public EY As Double '論理エリア上のクリックエリア下辺位置(ピクセル)。
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'プロパティ
        public double SX;               //'論理エリア上のクリックエリア左辺位置(ピクセル)。
        public double SY;               //'論理エリア上のクリックエリア上辺位置(ピクセル)。
        public double EX;               //'論理エリア上のクリックエリア右辺位置(ピクセル)。
        public double EY;               //'論理エリア上のクリックエリア下辺位置(ピクセル)。
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'インプリメンテーション
        Private m_clsClickObject() As ClickObjectInterface 'クリックオブジェクト。使用する配列の要素は(0 To ...)。
        Private m_nCount As Long 'クリックオブジェクト数。
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'インプリメンテーション
        private ClickObjectInterface[] m_clsClickObject;    //'クリックオブジェクト。使用する配列の要素は(0 To ...)。
        private long m_nCount;                              //'クリックオブジェクト数。
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '*******************************************************************************
        'プロパティ

        'クリックオブジェクト数。
        Property Get ClickObjectCount() As Long
            ClickObjectCount = UBound(m_clsClickObject) + 1
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '*******************************************************************************
        'プロパティ
        
        'クリックオブジェクト数。
        */
        public long ClickObjectCount()
        {
            return m_clsClickObject.Count();
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'クリックオブジェクト。
        Property Get ClickObject(ByVal nIndex As Long) As ClickObjectInterface
            Set ClickObject = m_clsClickObject(nIndex)
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'クリックオブジェクト。
        public ClickObjectInterface ClickObject(long nIndex)
        {
            return m_clsClickObject[nIndex];
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '*******************************************************************************
        'メソッド

        'クリックオブジェクト登録の開始。
        '
        'クリックエリアにクリックオブジェクトを登録する準備をする。
        '最初に StartRegistClickObject を呼び、登録が終わったら EndRegistClickObject を呼ぶ。
        '
        '引き数：
        'nTotalCount これから登録するオブジェクトの最大数。
        Public Sub StartRegistClickObject(ByVal nTotalCount As Long)
            If nTotalCount > 0 Then
                ReDim m_clsClickObject(nTotalCount - 1)
            Else
                ReDim m_clsClickObject(-1 To -1)
            End If
            m_nCount = 0
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '*******************************************************************************
        'メソッド
        
        'クリックオブジェクト登録の開始。
        '
        'クリックエリアにクリックオブジェクトを登録する準備をする。
        '最初に StartRegistClickObject を呼び、登録が終わったら EndRegistClickObject を呼ぶ。
        '
        '引き数：
        'nTotalCount これから登録するオブジェクトの最大数。
        */
        public void StartRegistClickObject(long nTotalCount)
        {
            if (nTotalCount > 0)
            {
                ClickObjectInterface[] m_clsClickObject = new ClickObjectInterface[nTotalCount];
            }
            else
            {
                ClickObjectInterface[] m_clsClickObject = new ClickObjectInterface[0];
            }
            //m_nCount = 0;
            m_nCount = nTotalCount;
            return;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'クリックオブジェクトを登録する。
        '
        '引き数：
        'clsClickObject 登録するクリックオブジェクト。
        Public Sub RegistClickObject(ByVal clsClickObject As ClickObjectInterface)
            Set m_clsClickObject(m_nCount) = clsClickObject
            m_nCount = m_nCount + 1
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        'クリックオブジェクトを登録する。
        '
        '引き数：
        'clsClickObject 登録するクリックオブジェクト。
        */
        public void RegistClickObject(ClickObjectInterface clsClickObject)
        {
            m_clsClickObject[m_nCount] = clsClickObject;
            m_nCount++;
            return;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'クリックオブジェクト登録の終了。
        Public Sub EndRegistClickObject()
            If m_nCount > 0 Then
                ReDim Preserve m_clsClickObject(m_nCount - 1)
            Else
                ReDim m_clsClickObject(-1 To -1)
            End If
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'クリックオブジェクト登録の終了。
        public void EndRegistClickObject()
        {
            if (m_nCount > 0)
            {
                ClickObjectInterface[] m_clsClickObject = new ClickObjectInterface[m_nCount];
            }
            else
            {
                ClickObjectInterface[] m_clsClickObject = new ClickObjectInterface[0];
            }
            return;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '指定された座標でクリックされるオブジェクトを取得する。
        '
        '指定された座標にあるオブジェクトのクリックオブジェクトを返す。
        '複数のオブジェクトが重なっている場合、HitObject を繰り返し呼ぶことにより順番にそれらのオブジェクトが取得できる。
        'その場合 objCurrent に前回の HitObject の呼び出しで取得されたクリックオブジェクトを指定する。
        'それにより objCurrent の次のクリックオブジェクトが取得できる。
        'objCurrent が Nothing であれば最初のクリックオブジェクトが取得できる。
        '
        '引き数：
        'nX 論理エリア上のX座標(ピクセル)。
        'nY 論理エリア上のY座標(ピクセル)。
        'objCurrent カレントオブジェクト。
        '
        '戻り値：
        '指定した座標にオブジェクトがある場合はそのクリックオブジェクトを返す。
        'それ以外の場合は Nothing を返す。
        Public Function HitObject(ByVal nX As Double, ByVal nY As Double, ByVal objCurrent As Object) As Object
            Dim i As Long
            '優先度の低いものから登録されているので降順に検索する。
            For i = UBound(m_clsClickObject) To 0 Step -1
                If m_clsClickObject(i).HitTest(nX, nY) Then
                    If objCurrent Is Nothing Then
                        'カレントオブジェクトがなければ最初にヒットしたものを返す。
                        Set HitObject = m_clsClickObject(i).Object
                        Exit Function
                    Else
                        '最初にヒットしたものを記憶して次へ進む。
                        Set HitObject = m_clsClickObject(i).Object
                        Exit For
                    End If
                End If
            Next
            'カレントオブジェクトと一致しているか？
            If Not objCurrent Is HitObject Then
                'カレントオブジェクトと一致するヒットするものを探す。
                For i = i - 1 To 0 Step -1
                    If m_clsClickObject(i).HitTest(nX, nY) Then
                        If objCurrent Is m_clsClickObject(i).Object Then
                            Exit For
                        End If
                    End If
                Next
            End If
            'カレントオブジェクトの次にヒットするものを探す。なければ最初にヒットしたもの。
            For i = i - 1 To 0 Step -1
                If m_clsClickObject(i).HitTest(nX, nY) Then
                    Set HitObject = m_clsClickObject(i).Object
                    Exit For
                End If
            Next
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '指定された座標でクリックされるオブジェクトを取得する。
        '
        '指定された座標にあるオブジェクトのクリックオブジェクトを返す。
        '複数のオブジェクトが重なっている場合、HitObject を繰り返し呼ぶことにより順番にそれらのオブジェクトが取得できる。
        'その場合 objCurrent に前回の HitObject の呼び出しで取得されたクリックオブジェクトを指定する。
        'それにより objCurrent の次のクリックオブジェクトが取得できる。
        'objCurrent が Nothing であれば最初のクリックオブジェクトが取得できる。
        '
        '引き数：
        'nX 論理エリア上のX座標(ピクセル)。
        'nY 論理エリア上のY座標(ピクセル)。
        'objCurrent カレントオブジェクト。
        '
        '戻り値：
        '指定した座標にオブジェクトがある場合はそのクリックオブジェクトを返す。
        'それ以外の場合は Nothing を返す。
        */
        public object HitObject(double nX, double nY, object objCurrent)
        {
            object w_HitObject = null;
            long i;
            //'優先度の低いものから登録されているので降順に検索する。
            for (i = m_clsClickObject.Count(); i > 0; i--)
            {
                if (m_clsClickObject[i].HitTest(nX, nY))
                {
                    if (objCurrent == null)
                    {
                        //'カレントオブジェクトがなければ最初にヒットしたものを返す。
                        return m_clsClickObject[i].Object;
                    }
                    else
                    {
                        //'最初にヒットしたものを記憶して次へ進む。
                        w_HitObject = m_clsClickObject[i].Object;
                        break;
                    }
                }
            }
            //'カレントオブジェクトと一致しているか？
            if (objCurrent != w_HitObject)
            {
                //'カレントオブジェクトと一致するヒットするものを探す。
                for (i = i - 1; i > 0; i--)
                {
                    if (m_clsClickObject[i].HitTest(nX, nY))
                    {
                        if (objCurrent == m_clsClickObject[i].Object)
                        {
                            break;
                        }
                    }
                }
            }
            //'カレントオブジェクトの次にヒットするものを探す。なければ最初にヒットしたもの。
            for (i = i - 1; i > 0; i--)
            {
                if (m_clsClickObject[i].HitTest(nX, nY))
                {
                    w_HitObject = m_clsClickObject[i].Object;
                }
            }
            return w_HitObject;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '指定された座標でクリックされるオブジェクトをすべて取得する。
        '
        '指定された座標にあるオブジェクトのクリックオブジェクトすべてを配列にして返す。
        '
        '引き数：
        'nX 論理エリア上のX座標(ピクセル)。
        'nY 論理エリア上のY座標(ピクセル)。
        '
        '戻り値：指定した座標にあるオブジェクトのクリックオブジェクトの配列を返す。配列の要素は(-1 To ...)、要素 -1 は未使用。
        Public Function HitObjectAll(ByVal nX As Double, ByVal nY As Double) As Object()
            Dim objObjects() As Object
            Dim nCount As Long
            Dim i As Long
            ReDim objObjects(-1 To -1)
            nCount = 0
            '優先度の低いものから登録されているので降順に検索する。
            For i = UBound(m_clsClickObject) To 0 Step -1
                If m_clsClickObject(i).HitTest(nX, nY) Then
                    ReDim Preserve objObjects(-1 To nCount)
                    Set objObjects(nCount) = m_clsClickObject(i).Object
                    nCount = nCount + 1
                End If
            Next
            HitObjectAll = objObjects
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '指定された座標でクリックされるオブジェクトをすべて取得する。
        '
        '指定された座標にあるオブジェクトのクリックオブジェクトすべてを配列にして返す。
        '
        '引き数：
        'nX 論理エリア上のX座標(ピクセル)。
        'nY 論理エリア上のY座標(ピクセル)。
        '
        '戻り値：指定した座標にあるオブジェクトのクリックオブジェクトの配列を返す。配列の要素は(-1 To ...)、要素 -1 は未使用。
        */
        public object[] HitObjectAll(double nX, double nY)
        {
            long nCount;
            long i;
            object[] objObjects = new object[0];
            nCount = 0;
            //'優先度の低いものから登録されているので降順に検索する。
            for (i = m_clsClickObject.Count(); i > 0; i--)
            {
                if (m_clsClickObject[i].HitTest(nX, nY))
                {
                    objObjects = new object[nCount];
                    objObjects[nCount] = m_clsClickObject[i].Object;
                    nCount++;
                }
            }
            return objObjects;
        }
        //==========================================================================================
    }
}
