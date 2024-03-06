using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace SurvLine
{
    public class ClickCircle
    {
        //==========================================================================================
        /*[VB]
        '*******************************************************************************
        '円形クリックオブジェクト
        '
        '移動点等の円形をしたオブジェクトのクリックオブジェクト。

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
        Public CX As Double '論理エリア上の中心座標(ピクセル)。
        Public CY As Double '論理エリア上の中心座標(ピクセル)。
        Public R As Double '半径(ピクセル)。
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
        public ObservationPoint Object;         //'観測点。
        public double CX;                       //'論理エリア上の中心座標(ピクセル)。
        public double CY;                       //'論理エリア上の中心座標(ピクセル)。
        public double R;                        //'半径(ピクセル)。
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
            If CX + R<nSX Then Exit Function
            If CY + R<nSY Then Exit Function
            If nEX<CX - R Then Exit Function
            If nEY<CY - R Then Exit Function
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
            if (CX + R < nSX) { return false; }
            if (CY + R < nSY) { return false; }
            if (nEX < CX - R) { return false; }
            if (nEY < CY - R) { return false; }
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
            ClickObjectInterface_HitTest = ((nX - CX) ^ 2 + (nY - CY) ^ 2 <= R ^ 2)
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
            return (Math.Pow(nX - CX, 2) + Math.Pow(nY - CY, 2)) <= Math.Pow(R, 2);
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '*******************************************************************************
        'メソッド

        'パラメータを設定する。
        '
        '引き数：
        'nCX 論理エリア上の円の中心座標(ピクセル)。
        'nCY 論理エリア上の円の中心座標(ピクセル)。
        'nR 半径(ピクセル)。
        Public Sub SetParam(ByVal nCX As Double, ByVal nCY As Double, ByVal nR As Double)
            CX = nCX
            CY = nCY
            R = nR
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
        'nCX 論理エリア上の円の中心座標(ピクセル)。
        'nCY 論理エリア上の円の中心座標(ピクセル)。
        'nR 半径(ピクセル)。
        */
        public void SetParam(double nCX, double nCY, double nR)
        {
            CX = nCX;
            CY = nCY;
            R = nR;
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
            objDevice.Circle(CX - nDeviceLeft, CY - nDeviceTop), R
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '*******************************************************************************
        'デバッグ
        
        'デバッグ描画。
        */
        private void ClickObjectInterface_DebugDraw(ObservationPoint objDevice, double nDeviceLeft, double nDeviceTop)
        {
#if false
            /*
             *************************** 修正要 sakai
             */
            objDevice.Circle(CX - nDeviceLeft, CY - nDeviceTop), R;
#endif
        }
        //==========================================================================================
    }
}
