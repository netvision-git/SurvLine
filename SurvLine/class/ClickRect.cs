using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace SurvLine
{
    public class ClickRect
    {
        //==========================================================================================
        /*[VB]
        '*******************************************************************************
        '長方形クリックオブジェクト
        '
        '長方形をしたオブジェクトのクリックオブジェクト。

        Option Explicit
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        Implements ClickObjectInterface

        'プロパティ
        Public Object As Object 'オブジェクト。
        Public Left As Double '論理エリア上の長方形の左辺(ピクセル)。
        Public Top As Double '論理エリア上の長方形の上辺(ピクセル)。
        Public Width As Double '長方形の幅(ピクセル)。
        Public Height As Double '長方形の高さ(ピクセル)。
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
        public object Object;       //'オブジェクト。
        public double Left;         //'論理エリア上の長方形の左辺(ピクセル)。
        public double Top;          //'論理エリア上の長方形の上辺(ピクセル)。
        public double Width;        //'長方形の幅(ピクセル)。
        public double Height;       //'長方形の高さ(ピクセル)。
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
        private void ClickObjectInterface_Object(object RHS)
        {
            Object = RHS;
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
        private object ClickObjectInterface_Object()
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
            If Left + Width <= nSX Then Exit Function
            If Top + Height <= nSY Then Exit Function
            If nEX<Left Then Exit Function
            If nEY < Top Then Exit Function
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
            if ((Left + Width) <= nSX) { return false; }
            if ((Top + Height) <= nSY) { return false; }
            if (nEX < Left) { return false; }
            if (nEY < Top) { return false; }
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
        Private Function ClickObjectInterface_HitTest(ByVal nX As Double, ByVal nY As Double) As Boolean
            ClickObjectInterface_HitTest = False
            If Left + Width <= nX Then Exit Function
            If Top + Height <= nY Then Exit Function
            If nX<Left Then Exit Function
            If nY < Top Then Exit Function
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
        private bool ClickObjectInterface_HitTest(double nX, double nY)
        {
            if (Left + Width <= nX)
            {
                return false;
            }
            if (Top + Height <= nY)
            {
                return false;
            }
            if (nX < Left)
            {
                return false;
            }
            if (nY < Top)
            {
                return false;
            }
            return true;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '*******************************************************************************
        'メソッド

        'パラメータを設定する。
        '
        '引き数：
        'nLeft 論理エリア上の長方形の左辺(ピクセル)。
        'nTop 論理エリア上の長方形の上辺(ピクセル)。
        'nWidth 長方形の幅(ピクセル)。
        'nHeight 長方形の高さ(ピクセル)。
        Public Sub SetParam(ByVal nLeft As Double, ByVal nTop As Double, ByVal nWidth As Double, ByVal nHeight As Double)
            Left = nLeft
            Top = nTop
            Width = nWidth
            Height = nHeight
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
        'nLeft 論理エリア上の長方形の左辺(ピクセル)。
        'nTop 論理エリア上の長方形の上辺(ピクセル)。
        'nWidth 長方形の幅(ピクセル)。
        'nHeight 長方形の高さ(ピクセル)。
        */
        public void SetParam(double nLeft, double nTop, double nWidth, double nHeight)
        {
            Left = nLeft;
            Top = nTop;
            Width = nWidth;
            Height = nHeight;
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
            objDevice.Line(Left - nDeviceLeft, Top - nDeviceTop)-(Left + Width - nDeviceLeft, Top + Height - nDeviceTop), , B
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
            objDevice.Line(Left - nDeviceLeft, Top - nDeviceTop) - (Left + Width - nDeviceLeft, Top + Height - nDeviceTop), , B;
#endif
        }
        //==========================================================================================
    }
}
