using Microsoft.VisualBasic.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static SurvLine.mdl.DEFINE;
using static SurvLine.mdl.MdlNSDefine;
using static SurvLine.mdl.MdlAccountMake;

namespace SurvLine.mdl
{
    internal class MdlSurveyParam
    {
        //==========================================================================================
        /*[VB]
        '*******************************************************************************
        '測量パラメータ

        Option Explicit
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        Public Const SP_a As Double = 6378137 '長半径(ｍ)。
        Public Const SP_b As Double = 6356752.31414036 '短半径(ｍ)。
        Public Const SP_c As Double = 6399593.62586402 '極での曲率半径(ｍ)。
        Public Const SP_f As Double = 3.35281068118232E-03 '扁平率。
        Public Const SP_F_ As Double = 298.257222101 '逆扁平率。
        Public Const SP_e As Double = 8.18191910428158E-02 '第一離心率。
        Public Const SP_e_dash As Double = 8.20944381519172E-02 '第二離心率。
        Public Const SP_m0 As Double = 0.9999 '座標系の原点における縮尺係数。
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        public const double SP_a = 6378137;                     //'長半径(ｍ)。
        public const double SP_b = 6356752.31414036;            //'短半径(ｍ)。
        public const double SP_c = 6399593.62586402;            //'極での曲率半径(ｍ)。
        public const double SP_f = 3.35281068118232E-03;        //'扁平率。
        public const double SP_F_ = 298.257222101;              //'逆扁平率。
        public const double SP_e = 8.18191910428158E-02;        //'第一離心率。
        public const double SP_e_dash = 8.20944381519172E-02;   //'第二離心率。
        public const double SP_m0 = 0.9999;                     //'座標系の原点における縮尺係数。
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '定数でも精度は同じ。
        '
        ''短半径。
        'Public Function SP_b() As Double
        '    SP_b = SP_a * (1 - SP_f)
        'End Function
        '
        ''極での曲率半径。
        'Public Function SP_c() As Double
        '    SP_c = SP_a / (1 - SP_f)
        'End Function
        '
        ''逆扁平率。
        'Public Function SP_F_() As Double
        '    SP_F_ = 1 / SP_f
        'End Function
        '
        ''第一離心率。
        'Public Function SP_e() As Double
        '    SP_e = Sqr(2 * SP_f - SP_f ^ 2)
        'End Function
        '
        ''第二離心率。
        'Public Function SP_e_dash() As Double
        '    SP_e_dash = Sqr(2 / SP_f - 1) / (1 / SP_f - 1)
        'End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '子午線曲率半径(ｍ)。
        '
        '引き数：
        'φ 緯度(ラジアン)。
        '
        '戻り値：子午線曲率半径(ｍ)。
        Public Function SP_M(ByVal φ As Double) As Double
            Dim VV As Double
            VV = SP_VV(φ)
            SP_M = SP_c / (VV * Sqr(VV))
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '子午線曲率半径(ｍ)。
        '
        '引き数：
        'φ 緯度(ラジアン)。
        '
        '戻り値：子午線曲率半径(ｍ)。
        */
        public static double SP_M(double φ)
        {
            double VV;
            VV = SP_VV(φ);
            return SP_c / (VV * Math.Sqrt(VV));
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '卯酉線曲率半径(ｍ)。
        '
        '引き数：
        'φ 緯度(ラジアン)。
        '
        '戻り値：卯酉線曲率半径(ｍ)。
        Public Function SP_N(ByVal φ As Double) As Double
            SP_N = SP_a / Sqr(SP_WW(φ))
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '卯酉線曲率半径(ｍ)。
        '
        '引き数：
        'φ 緯度(ラジアン)。
        '
        '戻り値：卯酉線曲率半径(ｍ)。
        */
        public static double SP_N(double φ)
        {
            return SP_a / Math.Sqrt(SP_WW(φ));
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'M^2
        '
        '子午線曲率半径の平方。
        '
        '引き数：
        'φ 緯度(ラジアン)。
        '
        '戻り値：子午線曲率半径(ｍ)の平方。
        Public Function SP_MM(ByVal φ As Double) As Double
            SP_MM = SP_c ^ 2 / SP_VV(φ) ^ 3
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        'M^2
        '
        '子午線曲率半径の平方。
        '
        '引き数：
        'φ 緯度(ラジアン)。
        '
        '戻り値：子午線曲率半径(ｍ)の平方。
        */
        public static double SP_MM(double φ)
        {
            return Math.Pow(SP_c, 2) / Math.Pow(SP_VV(φ), 3);
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'N^2
        '
        '卯酉線曲率半径の平方。
        '
        '引き数：
        'φ 緯度(ラジアン)。
        '
        '戻り値：卯酉線曲率半径(ｍ)の平方。
        Public Function SP_NN(ByVal φ As Double) As Double
            SP_NN = SP_a ^ 2 / SP_WW(φ)
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        'N^2
        '
        '卯酉線曲率半径の平方。
        '
        '引き数：
        'φ 緯度(ラジアン)。
        '
        '戻り値：卯酉線曲率半径(ｍ)の平方。
        */
        public static double SP_NN(double φ)
        {
            return Math.Pow(SP_a, 2) / SP_WW(φ);
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'M*N
        '
        '子午線曲率半径と卯酉線曲率半径の積。
        '
        '引き数：
        'φ 緯度(ラジアン)。
        '
        '戻り値：子午線曲率半径(ｍ)と卯酉線曲率半径(ｍ)の積。
        Public Function SP_MN(ByVal φ As Double) As Double
            SP_MN = (SP_c / SP_VV(φ)) ^ 2
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        'M*N
        '
        '子午線曲率半径と卯酉線曲率半径の積。
        '
        '引き数：
        'φ 緯度(ラジアン)。
        '
        '戻り値：子午線曲率半径(ｍ)と卯酉線曲率半径(ｍ)の積。
        */
        public static double SP_MN(double φ)
        {
            return Math.Pow(SP_c / SP_VV(φ), 2);
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '平均曲率半径(ｍ)。
        '
        '引き数：
        'φ 緯度(ラジアン)。
        '
        '戻り値：平均曲率半径(ｍ)。
        Public Function SP_R(ByVal φ As Double) As Double
            SP_R = SP_c / SP_VV(φ)
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '平均曲率半径(ｍ)。
        '
        '引き数：
        'φ 緯度(ラジアン)。
        '
        '戻り値：平均曲率半径(ｍ)。
        */
        public static double SP_R(double φ)
        {
            return SP_c / SP_VV(φ);
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '平面直角座標(X,Y)の点における縮尺係数。
        '
        '引き数：
        'X 平面直角X座標(ｍ)。
        'Y 平面直角Y座標(ｍ)。
        'φ (X,Y)の緯度(ラジアン)。
        '
        '戻り値：縮尺係数。
        Public Function SP_m_(ByVal X As Double, ByVal Y As Double, ByVal φ As Double) As Double
            Dim MN As Double
            MN = SP_MN(φ)
            SP_m_ = SP_m0 * (1 + Y ^ 2 / (2# * MN * SP_m0 ^ 2) + Y ^ 4 / (24# * MN ^ 2 * SP_m0 ^ 4))
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '平面直角座標(X,Y)の点における縮尺係数。
        '
        '引き数：
        'X 平面直角X座標(ｍ)。
        'Y 平面直角Y座標(ｍ)。
        'φ (X,Y)の緯度(ラジアン)。
        '
        '戻り値：縮尺係数。
        */
        public static double SP_m_(double X, double Y, double φ)
        {
            double MN;
            MN = SP_MN(φ);
            return SP_m0 * (1 + Math.Pow(Y, 2) / (2 * MN * Math.Pow(SP_m0, 2)) + Math.Pow(Y, 4) / (24 * Math.Pow(MN, 2) * Math.Pow(SP_m0, 4)));
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '測地線長(ｍ)。
        '
        '2点の平面直角座標の測地線長。
        '
        '引き数：
        'X1 平面直角X座標(ｍ)。
        'Y1 平面直角Y座標(ｍ)。
        'X2 平面直角X座標(ｍ)。
        'Y2 平面直角Y座標(ｍ)。
        'φ0 座標系原点の緯度(ラジアン)。
        '
        '戻り値：測地線長(ｍ)。
        Public Function SP_S(ByVal X1 As Double, ByVal Y1 As Double, ByVal X2 As Double, ByVal Y2 As Double, ByVal φ0 As Double) As Double
            SP_S = Sqr((X2 - X1) ^ 2 + (Y2 - Y1) ^ 2) / (1 + 1# / (6# * SP_R(φ0) ^ 2 * SP_m0 ^ 2) * (Y1 ^ 2 + Y1 * Y2 + Y2 ^ 2)) / SP_m0
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '測地線長(ｍ)。
        '
        '2点の平面直角座標の測地線長。
        '
        '引き数：
        'X1 平面直角X座標(ｍ)。
        'Y1 平面直角Y座標(ｍ)。
        'X2 平面直角X座標(ｍ)。
        'Y2 平面直角Y座標(ｍ)。
        'φ0 座標系原点の緯度(ラジアン)。
        '
        '戻り値：測地線長(ｍ)。
        */
        public static double SP_S(double X1, double Y1, double X2, double Y2, double φ0)
        {
            //SP_S = Sqr((X2 - X1) ^ 2 + (Y2 - Y1) ^ 2)
            //  / (1 + 1# / (6# * SP_R(φ0) ^ 2 * SP_m0 ^ 2) * (Y1 ^ 2 + Y1 * Y2 + Y2 ^ 2))
            //  / SP_m0
            return Math.Sqrt(Math.Pow(X2 - X1, 2) + Math.Pow(Y2 - Y1, 2))
                / (1 + 1 / (6 * Math.Pow(SP_R(φ0), 2) * Math.Pow(SP_m0, 2)) * (Math.Pow(Y1, 2) + Y1 * Y2 + Math.Pow(Y2, 2)))
                / SP_m0;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '測点1(X1,Y1)における測点2(X2,Y2)の方向角。
        '
        '引き数：
        'X1 平面直角X座標(ｍ)。
        'Y1 平面直角Y座標(ｍ)。
        'X2 平面直角X座標(ｍ)。
        'Y2 平面直角Y座標(ｍ)。
        'φ0 座標系原点の緯度(ラジアン)。
        'nDec 秒の小数点以下表示数　2007/9/10 NGS Yamada
        '
        '戻り値：方向角(ラジアン)。-π～π。
        Public Function SP_T(ByVal X1 As Double, ByVal Y1 As Double, ByVal X2 As Double, ByVal Y2 As Double, ByVal φ0 As Double, Optional nDec As Long = 0) As Double
            Dim DX As Double
            Dim DY As Double
            Dim R0 As Double
            DX = X2 - X1
            DY = Y2 - Y1
            R0 = SP_R(φ0)
            If DX > FLT_EPSILON Then
                SP_T = Atn((DY) / (DX)) - (-(1 / (4# * SP_m0 ^ 2 * R0 ^ 2) * (Y2 + Y1) * (DX)) + (1 / (12# * SP_m0 ^ 2 * R0 ^ 2) * (DX) * (DY)))
            ElseIf DX < -FLT_EPSILON Then
                SP_T = Atn((DY) / (DX)) + PAI - (-(1 / (4# * SP_m0 ^ 2 * R0 ^ 2) * (Y2 + Y1) * (DX)) + (1 / (12# * SP_m0 ^ 2 * R0 ^ 2) * (DX) * (DY)))
                If SP_T > PAI Then SP_T = SP_T - PAI * 2
            Else
                If DY < 0 Then
                    SP_T = -PAI / 2#
                Else
                    SP_T = PAI / 2#
                End If
            End If
    
            '丸め処理を追加　2007/9/10 NGS Yamda
            If nDec <> 0 Then
                Dim nH As Long
                Dim nM As Long
                Dim nS As Double
                Dim nResult As Double
        
                nResult = SP_T / PAI * 180
                Call d_to_dms_decimal(Abs(nResult), nH, nM, nS, ACCOUNT_DECIMAL_ANGLE)
                nResult = dms_to_d(nH, nM, nS)
                If SP_T < 0 Then
                    nResult = nResult * -1
                End If
                SP_T = nResult * PAI / 180
            End If
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '測点1(X1,Y1)における測点2(X2,Y2)の方向角。
        '
        '引き数：
        'X1 平面直角X座標(ｍ)。
        'Y1 平面直角Y座標(ｍ)。
        'X2 平面直角X座標(ｍ)。
        'Y2 平面直角Y座標(ｍ)。
        'φ0 座標系原点の緯度(ラジアン)。
        'nDec 秒の小数点以下表示数　2007/9/10 NGS Yamada
        '
        '戻り値：方向角(ラジアン)。-π～π。
        */
        public static double SP_T(double X1, double Y1, double X2, double Y2, double φ0, long nDec = 0)
        {
            double DX;
            double DY;
            double R0;
            long nH = 0;
            long nM = 0;
            double nS = 0;
            double nResult = 0;
            double w_SP_T;
            DX = X2 - X1;
            DY = Y2 - Y1;
            R0 = SP_R(φ0);
            if (DX > FLT_EPSILON)
            {
                w_SP_T = Math.Atan(DY / DX) - (-(1 / (4 * Math.Pow(SP_m0, 2) * Math.Pow(R0, 2)) * (Y2 + Y1) * DX) + (1 / (12 * Math.Pow(SP_m0, 2) * Math.Pow(R0, 2)) * DX * DY));
            }
            else if (DX < -FLT_EPSILON)
            {
                w_SP_T = Math.Atan(DY / DX) + PAI - (-(1 / (4 * Math.Pow(SP_m0, 2) * Math.Pow(R0, 2)) * (Y2 + Y1) * DX) + (1 / (12 * Math.Pow(SP_m0, 2) * Math.Pow(R0, 2)) * DX * DY));
                if (w_SP_T > PAI)
                {
                    w_SP_T -= PAI * 2;
                }
            }
            else
            {
                if (DY < 0)
                {
                    w_SP_T = -PAI / 2;
                }
                else
                {
                    w_SP_T = PAI / 2;
                }
            }

            //'丸め処理を追加　2007/9/10 NGS Yamda
            if (nDec != 0)
            {
                nResult = w_SP_T / (PAI * 180);
                //d_to_dms_decimal(Math.Abs(nResult), ref nH, ref nM, ref nS, (int)ACCOUNT_DECIMAL_ANGLE);
                D_to_Dms_decimal(Math.Abs(nResult), ref nH, ref nM, ref nS, ACCOUNT_DECIMAL_ANGLE);
                nResult = dms_to_d((int)nH, (int)nM, nS);
                if (w_SP_T < 0)
                {
                    nResult = nResult * -1;
                }
                w_SP_T = nResult * (PAI / 180);
            }
            return w_SP_T;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'W^2
        '
        'Wの平方。
        '
        '引き数：
        'φ 緯度(ラジアン)。
        '
        '戻り値：Wの平方。
        Private Function SP_WW(ByVal φ As Double) As Double
            SP_WW = 1# - SP_e ^ 2 * Sin(φ) ^ 2
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        'W^2
        '
        'Wの平方。
        '
        '引き数：
        'φ 緯度(ラジアン)。
        '
        '戻り値：Wの平方。
        */
        private static double SP_WW(double φ)
        {
            return 1 - Math.Pow(SP_e, 2) * Math.Pow(Math.Sin(φ), 2);
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'V^2
        '
        'Vの平方。
        '
        '引き数：
        'φ 緯度(ラジアン)。
        '
        '戻り値：Vの平方。
        Private Function SP_VV(ByVal φ As Double) As Double
            SP_VV = 1# + SP_e_dash ^ 2 * Cos(φ) ^ 2
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        'V^2
        '
        'Vの平方。
        '
        '引き数：
        'φ 緯度(ラジアン)。
        '
        '戻り値：Vの平方。
        */
        private static double SP_VV(double φ)
        {
            return 1 + Math.Pow(SP_e_dash, 2) * Math.Pow(Math.Cos(φ), 2);
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '子午線収差角。
        '
        '引き数：
        'φ 緯度(ラジアン)。
        'λ 経度(ラジアン)。
        'λ0 座標系原点の経度(ラジアン)。
        'nDec 秒の小数点以下表示数　2007/9/10 NGS Yamada
        '戻り値：子午線収差角(ラジアン)。
        Public Function SP_γ(ByVal φ As Double, ByVal λ As Double, ByVal λ0 As Double, Optional nDec As Long = 0) As Double
            Dim Δλ As Double
            Dim η2 As Double
            Dim T As Double
            Δλ = λ - λ0
            η2 = (SP_e_dash * Cos(φ)) ^ 2
            T = Tan(φ)
            SP_γ = Cos(φ) * T * Δλ + 1# / 3# * Cos(φ) ^ 3 * T * (1 + 3 * η2 + 2 * η2 ^ 2) * Δλ ^ 3 + 1# / 15# * Cos(φ) ^ 5 * T * (2 - T ^ 2) * Δλ ^ 5
    
            '丸め処理を追加　2007/9/10 NGS Yamda
            If nDec <> 0 Then
                Dim nH As Long
                Dim nM As Long
                Dim nS As Double
                Dim nResult As Double
        
                nResult = SP_γ / PAI * 180
                Call d_to_dms_decimal(Abs(nResult), nH, nM, nS, ACCOUNT_DECIMAL_ANGLE)
                nResult = dms_to_d(nH, nM, nS)
                If SP_γ < 0 Then
                    nResult = nResult * -1
                End If
                SP_γ = nResult * PAI / 180
            End If
        
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '子午線収差角。
        '
        '引き数：
        'φ 緯度(ラジアン)。
        'λ 経度(ラジアン)。
        'λ0 座標系原点の経度(ラジアン)。
        'nDec 秒の小数点以下表示数　2007/9/10 NGS Yamada
        '戻り値：子午線収差角(ラジアン)。
        */
        public static double SP_γ(double φ, double λ, double λ0, double nDec = 0)
        {
            double Δλ = 0;
            double η2 = 0;
            double T = 0;
            double w_SP_γ;
            Δλ = λ - λ0;
            η2 = Math.Pow(SP_e_dash * Math.Cos(φ), 2);
            T = Math.Tan(φ);
            w_SP_γ = Math.Cos(φ) * T * Δλ + 1 / 3 * Math.Pow(Math.Cos(φ), 3) * T * (1 + 3 * η2 + 2 * Math.Pow(η2, 2)) * Math.Pow(Δλ, 3)
                + 1 / 15 * Math.Pow(Math.Cos(φ), 5) * T * (2 - Math.Pow(T, 2)) * Math.Pow(Δλ, 5);

            //'丸め処理を追加　2007/9/10 NGS Yamda
            if (nDec != 0)
            {
                long nH = 0;
                long nM = 0;
                double nS = 0;
                double nResult = 0;

                nResult = w_SP_γ / (PAI * 180);
                //d_to_dms_decimal(Math.Abs(nResult), ref nH, ref nM, ref nS, (int)ACCOUNT_DECIMAL_ANGLE);
                D_to_Dms_decimal(Math.Abs(nResult), ref nH, ref nM, ref nS, ACCOUNT_DECIMAL_ANGLE);
                nResult = dms_to_d((int)nH, (int)nM, nS);
                if (w_SP_γ < 0)
                {
                    nResult = nResult * -1;
                }
                w_SP_γ = nResult * (PAI / 180);
            }
            return w_SP_γ;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '測点1(X1,Y1)における測点2(X2,Y2)の方位角。
        '
        '引き数：
        'X1 平面直角X座標(ｍ)。
        'Y1 平面直角Y座標(ｍ)。
        'X2 平面直角X座標(ｍ)。
        'Y2 平面直角Y座標(ｍ)。
        'φ (X1,Y1)の緯度(ラジアン)。
        'λ (X1,Y1)の経度(ラジアン)。
        'φ0 座標系原点の緯度(ラジアン)。
        'λ0 座標系原点の経度(ラジアン)。
        'nDec 秒の小数点以下表示数　2007/9/10 NGS Yamada
        '
        '戻り値：方位角(ラジアン)。0～2π。
        Public Function SP_α(ByVal X1 As Double, ByVal Y1 As Double, ByVal X2 As Double, ByVal Y2 As Double, ByVal φ As Double, ByVal λ As Double, ByVal φ0 As Double, ByVal λ0 As Double, Optional nDec As Long = 0) As Double
            If nDec <> 0 Then
                SP_α = SP_T(X1, Y1, X2, Y2, φ0, nDec) + SP_γ(φ, λ, λ0, nDec)
            Else
                SP_α = SP_T(X1, Y1, X2, Y2, φ0) + SP_γ(φ, λ, λ0)
            End If
            If SP_α < 0 Then SP_α = SP_α + PAI * 2
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '測点1(X1,Y1)における測点2(X2,Y2)の方位角。
        '
        '引き数：
        'X1 平面直角X座標(ｍ)。
        'Y1 平面直角Y座標(ｍ)。
        'X2 平面直角X座標(ｍ)。
        'Y2 平面直角Y座標(ｍ)。
        'φ (X1,Y1)の緯度(ラジアン)。
        'λ (X1,Y1)の経度(ラジアン)。
        'φ0 座標系原点の緯度(ラジアン)。
        'λ0 座標系原点の経度(ラジアン)。
        'nDec 秒の小数点以下表示数　2007/9/10 NGS Yamada
        '
        '戻り値：方位角(ラジアン)。0～2π。
        */
        public static double SP_α(double X1, double Y1, double X2, double Y2, double φ, double λ, double φ0, double λ0, double nDec = 0)
        {
            double w_SP_α;
            if (nDec != 0)
            {
                w_SP_α = SP_T(X1, Y1, X2, Y2, φ0, (long)nDec) + SP_γ(φ, λ, λ0, nDec);
            }
            else
            {
                w_SP_α = SP_T(X1, Y1, X2, Y2, φ0) + SP_γ(φ, λ, λ0);
            }
            if (w_SP_α < 0)
            {
                w_SP_α += PAI * 2;
            }
            return w_SP_α;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '高度角。
        '
        'ベクトルの始点における終点の高度角。
        '
        '引き数：
        'DX ベクトルX(ｍ)。
        'DY ベクトルY(ｍ)。
        'DZ ベクトルZ(ｍ)。
        'φ 始点の緯度(ラジアン)。
        'λ 始点の経度(ラジアン)。
        '
        '戻り値：高度角(ラジアン)。-π～π。
        Public Function SP_EA(ByVal DX As Double, ByVal DY As Double, ByVal DZ As Double, ByVal φ As Double, ByVal λ As Double) As Double
        '    Dim N As Double
        '    Dim E As Double
            Dim H As Double
        '    N = -DX * Sin(φ) * Cos(λ) - DY * Sin(φ) * Sin(λ) + DZ * Cos(φ)
        '    E = -DX * Sin(λ) + DY * Cos(λ)
            H = DX * Cos(φ) * Cos(λ) + DY * Cos(φ) * Sin(λ) + DZ * Sin(φ)
    
            '距離が０のときは０を返すように変更 2008/3/4 NGS Yamada
            Dim Dist As Double
            Dist = Sqr(DX ^ 2 + DY ^ 2 + DZ ^ 2)
            If Dist > FLT_EPSILON Then
                Dim ○ As Double
                ○ = H / Dist
                SP_EA = Atn2(○, Sqr(-○ * ○ + 1))
            Else
                SP_EA = 0
            End If
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '高度角。
        '
        'ベクトルの始点における終点の高度角。
        '
        '引き数：
        'DX ベクトルX(ｍ)。
        'DY ベクトルY(ｍ)。
        'DZ ベクトルZ(ｍ)。
        'φ 始点の緯度(ラジアン)。
        'λ 始点の経度(ラジアン)。
        '
        '戻り値：高度角(ラジアン)。-π～π。
        */
        public static double SP_EA(double DX, double DY, double DZ, double φ, double λ)
        {
            double w_SP_EA;
            //'    Dim N As Double
            //'    Dim E As Double
            double H = 0;
            //'    N = -DX * Sin(φ) * Cos(λ) - DY * Sin(φ) * Sin(λ) + DZ * Cos(φ)
            //'    E = -DX * Sin(λ) + DY * Cos(λ)
            H = DX * Math.Cos(φ) * Math.Cos(λ) + DY * Math.Cos(φ) * Math.Sin(λ) + DZ * Math.Sin(φ);


            //'距離が０のときは０を返すように変更 2008/3/4 NGS Yamada
            double Dist;
            Dist = Math.Sqrt(Math.Pow(DX, 2) + Math.Pow(DY, 2) + Math.Pow(DZ, 2));
            if (Dist > FLT_EPSILON)
            {
                double Maru;
                Maru = H / Dist;
                w_SP_EA = Math.Atan2(Maru, Math.Sqrt(-Maru * Maru + 1));
            }
            else
            {
                w_SP_EA = 0;
            }
            return w_SP_EA;
        }
        //==========================================================================================
    }
}
