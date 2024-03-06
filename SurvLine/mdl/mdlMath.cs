using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SurvLine.mdl.DEFINE;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace SurvLine.mdl
{
    internal class MdlMath
    {
        //==========================================================================================
        /*[VB]
        '*******************************************************************************
        'Mathematic

        Option Explicit
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '最小公倍数。
        '
        'M、Nは自然数。
        '
        '引き数：
        'M 値。
        'N 値。
        '
        '戻り値：M、Nの最小公倍数。
        Public Function Lcm(ByVal M As Long, ByVal N As Long) As Long
            Lcm = M * N / Gcm(M, N)
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '最小公倍数。
        '
        'M、Nは自然数。
        '
        '引き数：
        'M 値。
        'N 値。
        '
        '戻り値：M、Nの最小公倍数。
        */
        public static long Lcm(long M, long N)
        {
            return M * N / Gcm(M, N);
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '最大公約数。
        '
        'M、Nは自然数。
        '
        '引き数：
        'M 値。
        'N 値。
        '
        '戻り値：M、Nの最小公約数。
        Public Function Gcm(ByVal M As Long, ByVal N As Long) As Long
            Do While N <> 0
                Dim R As Long
                R = M Mod N
                M = N
                N = R
            Loop
            Gcm = M
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '最大公約数。
        '
        'M、Nは自然数。
        '
        '引き数：
        'M 値。
        'N 値。
        '
        '戻り値：M、Nの最小公約数。
        */
        public static long Gcm(long M, long N)
        {
            long R;
            while (N != 0)
            {
                R = M % N;
                M = N;
                N = R;
            }
            return M;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'アークタンジェント。
        '
        'Y/X のアークタンジェント。
        '
        '引き数：
        'Y 値。
        'X 値。
        '
        '戻り値：アークタンジェント。
        Public Function Atn2(ByVal Y As Double, ByVal X As Double) As Double
            If X >= FLT_EPSILON Then
                Atn2 = Atn(Y / X)
            ElseIf X <= -FLT_EPSILON Then
                Atn2 = Atn(Y / X)
                If Atn2 > 0 Then
                    Atn2 = Atn2 - PAI
                Else
                    Atn2 = Atn2 + PAI
                End If
            Else
                If Y > 0 Then
                    Atn2 = PAI * 0.5
                ElseIf Y < 0 Then
                    Atn2 = -PAI * 0.5
                Else
                    Atn2 = 0
                End If
            End If
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        'アークタンジェント。
        '
        'Y/X のアークタンジェント。
        '
        '引き数：
        'Y 値。
        'X 値。
        '
        '戻り値：アークタンジェント。
        */
        public static double Atn2(double Y, double X)
        {
            double w_Atn2;
            if (X >= FLT_EPSILON)
            {
                w_Atn2 = Math.Atan(Y / X);
            }
            else if (X <= -FLT_EPSILON)
            {
                w_Atn2 = Math.Atan(Y / X);
                if (w_Atn2 > 0)
                {
                    w_Atn2 = w_Atn2 - PAI;
                }
                else
                {
                    w_Atn2 = w_Atn2 + PAI;
                }
            }
            else
            {
                if (Y > 0)
                {
                    w_Atn2 = PAI * 0.5;
                }
                else if (Y < 0)
                {
                    w_Atn2 = -PAI * 0.5;
                }
                else
                {
                    w_Atn2 = 0;
                }
            }
            return w_Atn2;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '繰り上げ。
        '
        'bInt が True の場合、値が大きくなるように繰り上げる。つまり、-4.3 は -4 になる。
        'bInt が False の場合、0 から遠ざかるように繰り上げる。つまり、-4.3 は -5 になる。
        '
        '引き数：
        'N 値。
        'bInt 動作フラグ。
        '
        '戻り値：Nを繰り上げた値。
        Public Function MoveUp(ByVal N As Double, Optional bInt As Boolean = False) As Long
            If N < 0 And bInt Then
                MoveUp = Fix(N)
            Else
                MoveUp = -Int(-Abs(N)) * Sgn(N)
            End If
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '繰り上げ。
        '
        'bInt が True の場合、値が大きくなるように繰り上げる。つまり、-4.3 は -4 になる。
        'bInt が False の場合、0 から遠ざかるように繰り上げる。つまり、-4.3 は -5 になる。
        '
        '引き数：
        'N 値。
        'bInt 動作フラグ。
        '
        '戻り値：Nを繰り上げた値。
        */
        public static long MoveUp(double N, bool bInt = false)
        {
            if (N < 0 && bInt)
            {
                return (long)Math.Truncate(N);
            }
            else
            {
                return -(int)(-Math.Abs(N) * Math.Sign(N));
            }
        }
        //==========================================================================================
    }
}
