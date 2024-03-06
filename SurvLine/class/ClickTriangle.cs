using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SurvLine.mdl;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using static SurvLine.mdl.MdlMain;


namespace SurvLine
{
    public class ClickTriangle
    {
        //==========================================================================================
        /*[VB]
        '*******************************************************************************
        '三角形クリックオブジェクト
        '
        '固定点等の三角形をしたオブジェクトのクリックオブジェクト。

        Option Explicit
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        Implements ClickObjectInterface

        'プロパティ
        Public Object As ObservationPoint '観測点。
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
#if false
        /*
         *************************** 修正要 sakai
         */
        Implements ClickObjectInterface;
#endif

        //'プロパティ
        public ObservationPoint Object;     //'観測点。
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'インプリメンテーション
        Private m_nCX As Double '論理エリア上の三角形の中心座標(ピクセル)。
        Private m_nCY As Double '論理エリア上の三角形の中心座標(ピクセル)。
        Private m_nR As Double '半径(ピクセル)。正三角形に外接する円の半径。
        Private m_nBY As Double '底辺Ｙ座標(ピクセル)。
        Private m_nA As Double '傾き。
        Private m_nLC As Double '左辺Ｃ(ピクセル)。
        Private m_nRC As Double '右辺Ｃ(ピクセル)。
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'インプリメンテーション
        private double m_nCX;               //'論理エリア上の三角形の中心座標(ピクセル)。
        private double m_nCY;               //'論理エリア上の三角形の中心座標(ピクセル)。
        private double m_nR;                //'半径(ピクセル)。正三角形に外接する円の半径。
        private double m_nBY;               //'底辺Ｙ座標(ピクセル)。
        private double m_nA;                //'傾き。
        private double m_nLC;               //'左辺Ｃ(ピクセル)。
        private double m_nRC;               //'右辺Ｃ(ピクセル)。

        private MdlMain mdlMain;
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '*******************************************************************************
        'イベント

        '初期化。
        Private Sub Class_Initialize()

            On Error GoTo ErrorHandler


            m_nCX = 0
            m_nCY = 0
            m_nA = Sqr(3)


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
        private void Class_Initialize()
        {
            try
            {
                m_nCX = 0;
                m_nCY = 0;
                m_nA = Math.Sqrt(3);
            }

            catch (Exception)
            {
                mdlMain.ErrorExit();
            }

        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '*******************************************************************************
        'インターフェース

        'オブジェクト
        Private Property Set ClickObjectInterface_Object(ByVal RHS As Object)
            Set Object = RHS
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '*******************************************************************************
        'インターフェース
        
        'オブジェクト
        */
        private void ClickObjectInterface_Object(ObservationPoint RHS)
        {
            Object = RHS;
            return;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'オブジェクト
        Private Property Get ClickObjectInterface_Object() As Object
            Set ClickObjectInterface_Object = Object
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'オブジェクト
        private ObservationPoint ClickObjectInterface_Object()
        {
            return Object;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '指定されたエリアに所属するか検査する。
        '
        '指定されたエリアの範囲とオブジェクトが重なっていれば所属しているとみなす。
        '
        '引き数：
        'nSX 論理エリア上の指定エリア左辺位置(ピクセル)。
        'nSY 論理エリア上の指定エリア上辺位置(ピクセル)。
        'nEX 論理エリア上の指定エリア右辺位置(ピクセル)。
        'nEY 論理エリア上の指定エリア下辺位置(ピクセル)。
        '
        '戻り値：
        '指定されたエリアに所属する場合は True を返す。
        'それ以外の場合は False を返す。
        Private Function ClickObjectInterface_IsInclude(ByVal nSX As Double, ByVal nSY As Double, ByVal nEX As Double, ByVal nEY As Double) As Boolean
            ClickObjectInterface_IsInclude = False
            If m_nCX + m_nR* 0.5 * m_nA<nSX Then Exit Function
            If m_nBY<nSY Then Exit Function
            If nEX<m_nCX - m_nR* 0.5 * m_nA Then Exit Function
            If nEY<m_nCY - m_nR Then Exit Function
            ClickObjectInterface_IsInclude = True
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '指定されたエリアに所属するか検査する。
        '
        '指定されたエリアの範囲とオブジェクトが重なっていれば所属しているとみなす。
        '
        '引き数：
        'nSX 論理エリア上の指定エリア左辺位置(ピクセル)。
        'nSY 論理エリア上の指定エリア上辺位置(ピクセル)。
        'nEX 論理エリア上の指定エリア右辺位置(ピクセル)。
        'nEY 論理エリア上の指定エリア下辺位置(ピクセル)。
        '
        '戻り値：
        '指定されたエリアに所属する場合は True を返す。
        'それ以外の場合は False を返す。
        */
        private bool ClickObjectInterface_IsInclude(double nSX, double nSY, double nEX, double nEY)
        {
            if ((m_nCX + (m_nR * 0.5 * m_nA)) < nSX){ return false; }
            if (m_nBY < nSY) { return false; }
            if (nEX < (m_nCX - (m_nR * 0.5 * m_nA))) { return false; }
            if (nEY < (m_nCY - m_nR)) { return false; }
            return true;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'ヒットテスト。
        '
        '指定された座標がオブジェクト内であるか検査する。
        '
        '引き数：
        'nX 論理エリア上のX座標(ピクセル)。
        'nY 論理エリア上のY座標(ピクセル)。
        '
        '戻り値：
        '指定された座標がオブジェクト内である場合は True を返す。
        'それ以外の場合は False を返す。
        Public Function ClickObjectInterface_HitTest(ByVal nX As Double, ByVal nY As Double) As Boolean
            ClickObjectInterface_HitTest = False
            If nY > m_nBY Then Exit Function
            If nY< -m_nA* nX + m_nLC Then Exit Function
            If nY < m_nA* nX + m_nRC Then Exit Function
            ClickObjectInterface_HitTest = True
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        'ヒットテスト。
        '
        '指定された座標がオブジェクト内であるか検査する。
        '
        '引き数：
        'nX 論理エリア上のX座標(ピクセル)。
        'nY 論理エリア上のY座標(ピクセル)。
        '
        '戻り値：
        '指定された座標がオブジェクト内である場合は True を返す。
        'それ以外の場合は False を返す。
        */
        public bool ClickObjectInterface_HitTest(double nX, double nY)
        {
            return false;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '*******************************************************************************
        'メソッド

        'パラメータを設定する。
        '
        '引き数：
        'nCX 論理エリア上の三角形の中心座標(ピクセル)。
        'nCY 論理エリア上の三角形の中心座標(ピクセル)。
        'nR 半径(ピクセル)。正三角形に外接する円の半径。
        Public Sub SetParam(ByVal nCX As Double, ByVal nCY As Double, ByVal nR As Double)
            m_nCX = nCX
            m_nCY = nCY
            m_nR = nR
            m_nBY = m_nCY + m_nR * 0.5
            m_nLC = (m_nCY - m_nR) + m_nA* m_nCX
            m_nRC = (m_nCY - m_nR) - m_nA* m_nCX
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '*******************************************************************************
        'メソッド
        
        'パラメータを設定する。
        '
        '引き数：
        'nCX 論理エリア上の三角形の中心座標(ピクセル)。
        'nCY 論理エリア上の三角形の中心座標(ピクセル)。
        'nR 半径(ピクセル)。正三角形に外接する円の半径。
        */
        public void SetParam(double nCX, double nCY, double nR)
        {
            m_nCX = nCX;
            m_nCY = nCY;
            m_nR = nR;
            m_nBY = m_nCY + m_nR * 0.5;
            m_nLC = (m_nCY - m_nR) + m_nA * m_nCX;
            m_nRC = (m_nCY - m_nR) - m_nA * m_nCX;
            return;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '指定されたエリアに所属するか検査する。
        '
        '指定されたエリアの範囲とオブジェクトが重なっていれば所属しているとみなす。
        '
        '引き数：
        'nSX 論理エリア上の指定エリア左辺位置(ピクセル)。
        'nSY 論理エリア上の指定エリア上辺位置(ピクセル)。
        'nEX 論理エリア上の指定エリア右辺位置(ピクセル)。
        'nEY 論理エリア上の指定エリア下辺位置(ピクセル)。
        '
        '戻り値：
        '指定されたエリアに所属する場合は True を返す。
        'それ以外の場合は False を返す。
        Public Function IsInclude(ByVal nSX As Double, ByVal nSY As Double, ByVal nEX As Double, ByVal nEY As Double) As Boolean
            IsInclude = ClickObjectInterface_IsInclude(nSX, nSY, nEX, nEY)
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '指定されたエリアに所属するか検査する。
        '
        '指定されたエリアの範囲とオブジェクトが重なっていれば所属しているとみなす。
        '
        '引き数：
        'nSX 論理エリア上の指定エリア左辺位置(ピクセル)。
        'nSY 論理エリア上の指定エリア上辺位置(ピクセル)。
        'nEX 論理エリア上の指定エリア右辺位置(ピクセル)。
        'nEY 論理エリア上の指定エリア下辺位置(ピクセル)。
        '
        '戻り値：
        '指定されたエリアに所属する場合は True を返す。
        'それ以外の場合は False を返す。
        */
        public bool IsInclude(double nSX, double nSY, double nEX, double nEY)
        {
            return ClickObjectInterface_IsInclude(nSX, nSY, nEX, nEY);
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '*******************************************************************************
        'デバッグ

        'デバッグ描画。
        Private Sub ClickObjectInterface_DebugDraw(ByVal objDevice As Object, ByVal nDeviceLeft As Double, ByVal nDeviceTop As Double)
            objDevice.Circle(m_nCX - nDeviceLeft, m_nCY - nDeviceTop), m_nR
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '*******************************************************************************
        'デバッグ
        
        'デバッグ描画。
        */
        private void ClickObjectInterface_DebugDraw(object objDevice, double nDeviceLeft, double nDeviceTop)
        {
#if false
            /*
             *************************** 修正要 sakai
             */
            objDevice.Circle(m_nCX - nDeviceLeft, m_nCY - nDeviceTop), m_nR;
#endif
            return;
        }
        //==========================================================================================
    }
}
