using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;
using static SurvLine.mdl.MdlAccountMake;
using static SurvLine.mdl.DEFINE;
using static SurvLine.mdl.MdlMath;
using static SurvLine.mdl.MdlNSDefine;
using static SurvLine.mdl.MdlMatrix;
using static SurvLine.mdl.MdlBaseLineAnalyser;

namespace SurvLine.mdl
{
    internal class mdlEccentricCorrection
    {
        //==========================================================================================
        /*[VB]
        '*******************************************************************************
        '偏心補正計算器

        Option Explicit
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '角度種別。
        Public Enum ANGLE_TYPE
            ANGLETYPE_HORIZONTAL = 0 '水平角。
            ANGLETYPE_DIRECTION '方位角。
            ANGLETYPE_MARK      '方位標を手入力、角度は水平角。 2007/3/14 NGS Yamada
        End Enum
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'角度種別。
        public enum ANGLE_TYPE
        {
            ANGLETYPE_HORIZONTAL = 0,   //'水平角。
            ANGLETYPE_DIRECTION,        //'方位角。
            ANGLETYPE_MARK,             //'方位標を手入力、角度は水平角。 2007/3/14 NGS Yamada
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '指定された偏心補正パラメータで計算可能か検査する。
        '
        '引き数：
        'clsEccentricCorrectionParam 偏心補正パラメータ。
        '
        '戻り値：
        '計算が可能である場合 True を返す。
        'それ以外の場合 False を返す。
        Public Function CheckParame(ByVal clsEccentricCorrectionParam As EccentricCorrectionParam) As Boolean

            CheckParame = False
    
            On Error GoTo ErrorHandler
    
            Dim δα〃1 As Double
            Dim δα〃2 As Double
            Dim i1 As Double
            Dim i2 As Double
            Dim f1 As Double
            Dim f2 As Double
            Dim D As Double
            Dim δα1 As Double
            Dim δα2 As Double
            Call CorrectDα(clsEccentricCorrectionParam, δα〃1, δα〃2, i1, i2, f1, f2, D, δα1, δα2)
    
            CheckParame = True
    
        ErrorHandler:
    
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '指定された偏心補正パラメータで計算可能か検査する。
        '
        '引き数：
        'clsEccentricCorrectionParam 偏心補正パラメータ。
        '
        '戻り値：
        '計算が可能である場合 True を返す。
        'それ以外の場合 False を返す。
        */
        public static bool CheckParame(EccentricCorrectionParam clsEccentricCorrectionParam)
        {
            try
            {
                double δαRep1 = 0;
                double δαRep2 = 0;
                double i1 = 0;
                double i2 = 0;
                double f1 = 0;
                double f2 = 0;
                double D = 0;
                double δα1 = 0;
                double δα2 = 0;
                CorrectDα(clsEccentricCorrectionParam, ref δαRep1, ref δαRep2, ref i1, ref i2, ref f1, ref f2, ref D, ref δα1, ref δα2);

                return true;
            }

            catch (Exception e)
            {
                return false;
            }
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '偏心補正計算。
        '
        '引き数：
        'clsObservationPoint 対象とする観測点。
        'clsBaseLineVector 方位標、もしくは偏心に使用する基線。
        'clsEccentricCorrectionParam 偏心補正パラメータ。
        '⊿XYZ 偏心ベクトルのXYZ成分(ｍ)が設定される。配列の要素は(2, 0)。
        Public Sub CorrectEccentric(ByVal clsObservationPoint As ObservationPoint, ByVal clsBaseLineVector As BaseLineVector, ByVal clsEccentricCorrectionParam As EccentricCorrectionParam, ByRef ⊿XYZ() As Double)
            Dim δα〃1 As Double
            Dim δα〃2 As Double
            Dim i1 As Double
            Dim i2 As Double
            Dim f1 As Double
            Dim f2 As Double
            Dim D As Double
            Dim δα1 As Double
            Dim δα2 As Double
            Dim T0 As Double
            Dim clsEccentricPoint As CoordinatePoint
            Call CorrectEccentricDetail(clsObservationPoint, clsBaseLineVector, clsEccentricCorrectionParam, ⊿XYZ, δα〃1, δα〃2, i1, i2, f1, f2, D, δα1, δα2, T0, clsEccentricPoint)
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '偏心補正計算。
        '
        '引き数：
        'clsObservationPoint 対象とする観測点。
        'clsBaseLineVector 方位標、もしくは偏心に使用する基線。
        'clsEccentricCorrectionParam 偏心補正パラメータ。
        '⊿XYZ 偏心ベクトルのXYZ成分(ｍ)が設定される。配列の要素は(2, 0)。
        */
        public void CorrectEccentric(ObservationPoint clsObservationPoint, BaseLineVector clsBaseLineVector, EccentricCorrectionParam clsEccentricCorrectionParam, ref double[,] ΔXYZ)
        {
            double δαRep1 = 0;
            double δαRep2 = 0;
            double i1 = 0;
            double i2 = 0;
            double f1 = 0;
            double f2 = 0;
            double D = 0;
            double δα1 = 0;
            double δα2 = 0;
            double T0 = 0;
            CoordinatePoint clsEccentricPoint = null;
            CorrectEccentricDetail(clsObservationPoint, clsBaseLineVector, clsEccentricCorrectionParam,
                ref ΔXYZ, ref δαRep1, ref δαRep2, ref i1, ref i2, ref f1, ref f2, ref D, ref δα1, ref δα2, ref T0, ref clsEccentricPoint);
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '偏心補正計算。
        '
        '引き数：
        'clsObservationPoint 対象とする観測点。
        'clsBaseLineVector 方位標、もしくは偏心に使用する基線。
        'clsEccentricCorrectionParam 偏心補正パラメータ。
        '⊿XYZ 偏心ベクトルのXYZ成分(ｍ)が設定される。偏心量B→C。配列の要素は(2, 0)。
        'δα〃1 i≠f高度角補正量CB(ラジアン)が設定される。
        'δα〃2 i≠f高度角補正量BC(ラジアン)が設定される。
        'i1 i≠f補正器械高CB(ｍ)が設定される。
        'i2 i≠f補正器械高BC(ｍ)が設定される。
        'f1 i≠f補正目標高BC(ｍ)が設定される。
        'f2 i≠f補正目標高CB(ｍ)が設定される。
        'D 斜距離(ｍ)が設定される。
        'δα1 高度角補正量CB(ラジアン)が設定される。
        'δα2 高度角補正量BC(ラジアン)が設定される。
        'T0 方位標の方位角(ラジアン)が設定される。
        'clsEccentricPoint 偏心座標(偏心元の座標)が設定される。
        Public Sub CorrectEccentricDetail(ByVal clsObservationPoint As ObservationPoint, ByVal clsBaseLineVector As BaseLineVector, ByVal clsEccentricCorrectionParam As EccentricCorrectionParam, ByRef ⊿XYZ() As Double, ByRef δα〃1 As Double, ByRef δα〃2 As Double, ByRef i1 As Double, ByRef i2 As Double, ByRef f1 As Double, ByRef f2 As Double, ByRef D As Double, ByRef δα1 As Double, ByRef δα2 As Double, ByRef T0 As Double, ByRef clsEccentricPoint As CoordinatePoint)

            'CoordinateReference を参照するので一意に決める。
            Dim clsEnablePoint As ObservationPoint
            Set clsEnablePoint = clsObservationPoint.HeadPoint
            Do While Not clsEnablePoint Is Nothing
                If clsEnablePoint.Enable Then Exit Do
                Set clsEnablePoint = clsEnablePoint.NextPoint
            Loop
            If Not clsEnablePoint Is Nothing Then Set clsObservationPoint = clsEnablePoint
    
            '斜距離、高低角補正。
            Call CorrectDα(clsEccentricCorrectionParam, δα〃1, δα〃2, i1, i2, f1, f2, D, δα1, δα2)
            Dim α1 As Double
            Dim α2 As Double
            α1 = clsEccentricCorrectionParam.ElevationCB + δα〃1 + δα1
            α2 = clsEccentricCorrectionParam.ElevationBC + δα〃2 + δα2
            Dim αm As Double
            αm = (α2 - α1) * 0.5
    
            Dim R() As Double
            Dim T As Double
            If clsEccentricCorrectionParam.AngleType = ANGLETYPE_HORIZONTAL Then
                '回転行列。
                R = GetRFromVector(clsObservationPoint, clsBaseLineVector, clsEccentricPoint)
                '方位角。
                T0 = GetT0(clsObservationPoint, clsBaseLineVector, R)
                T = T0 + clsEccentricCorrectionParam.Horizontal
                Do While 360 <= T
                    T = T - 360
                Loop
            ElseIf clsEccentricCorrectionParam.AngleType = ANGLETYPE_MARK Then
        '        Set clsEccentricPoint = clsObservationPoint.CoordinateReference.CreateCopy()
                '回転行列。
                '2009/11 H.Nakamura
                'R = GetRFromVector2(clsObservationPoint, clsEccentricPoint)
                R = GetRFromUsePoint(clsObservationPoint, clsBaseLineVector, clsEccentricPoint)
                '方位角。
                T0 = GetT02(clsObservationPoint, clsEccentricCorrectionParam.MarkLat, clsEccentricCorrectionParam.MarkLon, clsEccentricCorrectionParam.MarkHeight, R)
                T = T0 + clsEccentricCorrectionParam.Horizontal
                Do While 360 <= T
                    T = T - 360
                Loop
            Else
                '2009/11 H.Nakamura
                'Set clsEccentricPoint = clsObservationPoint.CoordinateReference.CreateCopy()
                '回転行列。
                'R = GetR(clsEccentricPoint)
                R = GetRFromUsePoint(clsObservationPoint, clsBaseLineVector, clsEccentricPoint)
                '方位角。
                T = clsEccentricCorrectionParam.Direction
                T0 = -1
            End If
    
            '偏心補正量。
            Dim ⊿NEU(2, 0) As Double
            ⊿NEU(0, 0) = D * Cos(αm) * Cos(T)
            ⊿NEU(1, 0) = D * Cos(αm) * Sin(T)
            ⊿NEU(2, 0) = D * Sin(αm)
            Dim RT() As Double
            RT = Transpose(R)
            ⊿XYZ = Product(RT, ⊿NEU)
    
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '偏心補正計算。
        '
        '引き数：
        'clsObservationPoint 対象とする観測点。
        'clsBaseLineVector 方位標、もしくは偏心に使用する基線。
        'clsEccentricCorrectionParam 偏心補正パラメータ。
        '⊿XYZ 偏心ベクトルのXYZ成分(ｍ)が設定される。偏心量B→C。配列の要素は(2, 0)。
        'δα〃1 i≠f高度角補正量CB(ラジアン)が設定される。
        'δα〃2 i≠f高度角補正量BC(ラジアン)が設定される。
        'i1 i≠f補正器械高CB(ｍ)が設定される。
        'i2 i≠f補正器械高BC(ｍ)が設定される。
        'f1 i≠f補正目標高BC(ｍ)が設定される。
        'f2 i≠f補正目標高CB(ｍ)が設定される。
        'D 斜距離(ｍ)が設定される。
        'δα1 高度角補正量CB(ラジアン)が設定される。
        'δα2 高度角補正量BC(ラジアン)が設定される。
        'T0 方位標の方位角(ラジアン)が設定される。
        'clsEccentricPoint 偏心座標(偏心元の座標)が設定される。
        */
        public void CorrectEccentricDetail(ObservationPoint clsObservationPoint, BaseLineVector clsBaseLineVector, EccentricCorrectionParam clsEccentricCorrectionParam,
            ref double[,] ΔXYZ, ref double δαRep1, ref double δαRep2, ref double i1, ref double i2, ref double f1, ref double f2, ref double D, ref double δα1,
            ref double δα2, ref double T0, ref CoordinatePoint clsEccentricPoint)
        {
            //'CoordinateReference を参照するので一意に決める。
            ObservationPoint clsEnablePoint;
            clsEnablePoint = clsObservationPoint.HeadPoint();
            while (clsEnablePoint != null)
            {
                if (clsEnablePoint.Enable())
                {
                    break;
                }
                clsEnablePoint = clsEnablePoint.NextPoint();
            }
            if (clsEnablePoint != null)
            {
                clsObservationPoint = clsEnablePoint;
            }

            //'斜距離、高低角補正。
            CorrectDα(clsEccentricCorrectionParam, ref δαRep1, ref δαRep2, ref i1, ref i2, ref f1, ref f2, ref D, ref δα1, ref δα2);
            double α1;
            double α2;
            α1 = clsEccentricCorrectionParam.ElevationCB + δαRep1 + δα1;
            α2 = clsEccentricCorrectionParam.ElevationBC + δαRep2 + δα2;
            double αm;
            αm = (α2 - α1) * 0.5;

            double[,] R;
            double T;
            if (clsEccentricCorrectionParam.AngleType == (int)ANGLE_TYPE.ANGLETYPE_HORIZONTAL)
            {
                //'回転行列。
                R = GetRFromVector(clsObservationPoint, clsBaseLineVector, ref clsEccentricPoint);
                //'方位角。
                T0 = GetT0(clsObservationPoint, clsBaseLineVector, ref R);
                T = T0 + clsEccentricCorrectionParam.Horizontal;
                while (360 <= T)
                {
                    T = T - 360;
                }
            }
            else if (clsEccentricCorrectionParam.AngleType == (int)ANGLE_TYPE.ANGLETYPE_MARK)
            {
                //'        Set clsEccentricPoint = clsObservationPoint.CoordinateReference.CreateCopy()
                //'回転行列。
                //'2009/11 H.Nakamura
                //'R = GetRFromVector2(clsObservationPoint, clsEccentricPoint)
                R = GetRFromUsePoint(clsObservationPoint, clsBaseLineVector, ref clsEccentricPoint);
                //'方位角。
                T0 = GetT02(clsObservationPoint, clsEccentricCorrectionParam.MarkLat, clsEccentricCorrectionParam.MarkLon, clsEccentricCorrectionParam.MarkHeight, ref R);
                T = T0 + clsEccentricCorrectionParam.Horizontal;
                while (360 <= T)
                {
                    T -= 360;
                }
            }
            else
            {
                //'2009/11 H.Nakamura
                //'Set clsEccentricPoint = clsObservationPoint.CoordinateReference.CreateCopy()
                //'回転行列。
                //'R = GetR(clsEccentricPoint)
                R = GetRFromUsePoint(clsObservationPoint, clsBaseLineVector, ref clsEccentricPoint);
                //'方位角。
                T = clsEccentricCorrectionParam.Direction;
                T0 = -1;
            }


            //'偏心補正量。
            double[,] ΔNEU = new double[2, 0];
            ΔNEU[0, 0] = D * Math.Cos(αm) * Math.Cos(T);
            ΔNEU[1, 0] = D * Math.Cos(αm) * Math.Sin(T);
            ΔNEU[2, 0] = D * Math.Sin(αm);
            double[,] RT;
            RT = Transpose(ref R);
            ΔXYZ = Product(ref RT, ref ΔNEU);
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '距離と高低角の補正。
        '
        '引き数：
        'clsEccentricCorrectionParam 偏心補正パラメータ。
        'δα〃1 i≠f高度角補正量CB(ラジアン)が設定される。
        'δα〃2 i≠f高度角補正量BC(ラジアン)が設定される。
        'i1 i≠f補正器械高CB(ｍ)が設定される。
        'i2 i≠f補正器械高BC(ｍ)が設定される。
        'f1 i≠f補正目標高BC(ｍ)が設定される。
        'f2 i≠f補正目標高CB(ｍ)が設定される。
        'D 斜距離(ｍ)が設定される。
        'δα1 高度角補正量CB(ラジアン)が設定される。
        'δα2 高度角補正量BC(ラジアン)が設定される。
        Public Sub CorrectDα(ByVal clsEccentricCorrectionParam As EccentricCorrectionParam, ByRef δα〃1 As Double, ByRef δα〃2 As Double, ByRef i1 As Double, ByRef i2 As Double, ByRef f1 As Double, ByRef f2 As Double, ByRef D As Double, ByRef δα1 As Double, ByRef δα2 As Double)
            'i≠f補正。
            Dim h1 As Double
            Dim h2 As Double
            If clsEccentricCorrectionParam.UseTS Then
                h1 = clsEccentricCorrectionParam.ToHeightTS
                h2 = clsEccentricCorrectionParam.FromHeightTS
            Else
                h1 = clsEccentricCorrectionParam.ToHeightBC
                h2 = clsEccentricCorrectionParam.FromHeightBC
            End If
            If Abs(clsEccentricCorrectionParam.Distance) < FLT_EPSILON Then
                δα〃1 = 0
                δα〃2 = 0
            Else
                Dim β1 As Double
                Dim β2 As Double
                β1 = (((clsEccentricCorrectionParam.FromHeightCB - h1) - (clsEccentricCorrectionParam.ToHeightCB - h2)) * Cos(clsEccentricCorrectionParam.ElevationCB)) / clsEccentricCorrectionParam.Distance
                β2 = (((clsEccentricCorrectionParam.ToHeightBC - h1) - (clsEccentricCorrectionParam.FromHeightBC - h2)) * Cos(clsEccentricCorrectionParam.ElevationBC)) / clsEccentricCorrectionParam.Distance
                δα〃1 = Atn2(β1, Sqr(-β1 * β1 + 1))
                δα〃2 = -Atn2(β2, Sqr(-β2 * β2 + 1))
            End If
            Dim α〃1 As Double
            Dim α〃2 As Double
            α〃1 = clsEccentricCorrectionParam.ElevationCB + δα〃1
            α〃2 = clsEccentricCorrectionParam.ElevationBC + δα〃2
            i1 = h1
            f1 = h1
            i2 = h2
            f2 = h2
            '斜距離補正。
            Dim αm〃 As Double
            αm〃 = (α〃1 - α〃2) * 0.5
            D = Sqr((clsEccentricCorrectionParam.Distance * Cos(αm〃)) ^ 2 + (clsEccentricCorrectionParam.Distance * Sin(αm〃) + i1 - f2) ^ 2)
            '高低角補正。
            If Abs(D) < FLT_EPSILON Then
                δα1 = 0
                δα2 = 0
            Else
                β1 = (i1 - f2) * Cos(α〃1) / D
                β2 = (i2 - f1) * Cos(α〃2) / D
                δα1 = Atn2(β1, Sqr(-β1 * β1 + 1))
                δα2 = Atn2(β2, Sqr(-β2 * β2 + 1))
            End If
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '距離と高低角の補正。
        '
        '引き数：
        'clsEccentricCorrectionParam 偏心補正パラメータ。
        'δα〃1 i≠f高度角補正量CB(ラジアン)が設定される。
        'δα〃2 i≠f高度角補正量BC(ラジアン)が設定される。
        'i1 i≠f補正器械高CB(ｍ)が設定される。
        'i2 i≠f補正器械高BC(ｍ)が設定される。
        'f1 i≠f補正目標高BC(ｍ)が設定される。
        'f2 i≠f補正目標高CB(ｍ)が設定される。
        'D 斜距離(ｍ)が設定される。
        'δα1 高度角補正量CB(ラジアン)が設定される。
        'δα2 高度角補正量BC(ラジアン)が設定される。
        */
        public static void CorrectDα(EccentricCorrectionParam clsEccentricCorrectionParam, ref double δαRep1, ref double δαRep2, ref double i1, ref double i2,
            ref double f1, ref double f2, ref double D, ref double δα1, ref double δα2)
        {
            //'i≠f補正。
            double h1;
            double h2;
            double β1;
            double β2;
            if (clsEccentricCorrectionParam.UseTS)
            {
                h1 = clsEccentricCorrectionParam.ToHeightTS;
                h2 = clsEccentricCorrectionParam.FromHeightTS;
            }
            else
            {
                h1 = clsEccentricCorrectionParam.ToHeightBC;
                h2 = clsEccentricCorrectionParam.FromHeightBC;
            }
            if (Math.Abs(clsEccentricCorrectionParam.Distance) < FLT_EPSILON)
            {
                δαRep1 = 0;
                δαRep2 = 0;
            }
            else
            {
                β1 = (clsEccentricCorrectionParam.FromHeightCB - h1 - (clsEccentricCorrectionParam.ToHeightCB - h2)) * Math.Cos(clsEccentricCorrectionParam.ElevationCB)
                    / clsEccentricCorrectionParam.Distance;
                β2 = (clsEccentricCorrectionParam.ToHeightBC - h1 - (clsEccentricCorrectionParam.FromHeightBC - h2)) * Math.Cos(clsEccentricCorrectionParam.ElevationBC)
                    / clsEccentricCorrectionParam.Distance;
                δαRep1 = Atn2(β1, Math.Sqrt(-β1 * β1 + 1));
                δαRep2 = -Atn2(β2, Math.Sqrt(-β2 * β2 + 1));
            }
            double αRep1;
            double αRep2;
            αRep1 = clsEccentricCorrectionParam.ElevationCB + δαRep1;
            αRep2 = clsEccentricCorrectionParam.ElevationBC + δαRep2;
            i1 = h1;
            f1 = h1;
            i2 = h2;
            f2 = h2;
            //'斜距離補正。
            double αmRep;
            αmRep = (αRep1 - αRep2) * 0.5;
            D = Math.Sqrt(Math.Pow(clsEccentricCorrectionParam.Distance * Math.Cos(αmRep), 2) + Math.Pow(clsEccentricCorrectionParam.Distance * Math.Sin(αmRep) + i1 - f2, 2));
            //'高低角補正。
            if (Math.Abs(D) < FLT_EPSILON)
            {
                δα1 = 0;
                δα2 = 0;
            }
            else
            {
                β1 = (i1 - f2) * Math.Cos(αRep1) / D;
                β2 = (i2 - f1) * Math.Cos(αRep2) / D;
                δα1 = Atn2(β1, Math.Sqrt(-β1 * β1 + 1));
                δα2 = Atn2(β2, Math.Sqrt(-β2 * β2 + 1));
            }
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '方位標の方位角の取得（GPSベクトル）。
        '
        '引き数：
        'clsObservationPoint 対象とする観測点(代表観測点)。
        'clsBaseLineVector 方位標ベクトル。
        '
        '戻り値：方位標の方位角(ラジアン)を返す。
        Public Function GetMarkerDirection(ByVal clsObservationPoint As ObservationPoint, ByVal clsBaseLineVector As BaseLineVector) As Double
            '回転行列。
            Dim R() As Double
            Dim clsEccentricPoint As CoordinatePoint
            R = GetRFromVector(clsObservationPoint, clsBaseLineVector, clsEccentricPoint)
            GetMarkerDirection = GetT0(clsObservationPoint, clsBaseLineVector, R)
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '方位標の方位角の取得（GPSベクトル）。
        '
        '引き数：
        'clsObservationPoint 対象とする観測点(代表観測点)。
        'clsBaseLineVector 方位標ベクトル。
        '
        '戻り値：方位標の方位角(ラジアン)を返す。
        */
        public double GetMarkerDirection(ObservationPoint clsObservationPoint, BaseLineVector clsBaseLineVector)
        {
            //'回転行列。
            double[,] R;
            CoordinatePoint clsEccentricPoint = null;
            R = GetRFromVector(clsObservationPoint, clsBaseLineVector, ref clsEccentricPoint);
            return GetT0(clsObservationPoint, clsBaseLineVector, ref R);
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '方位標の方位角の取得（手入力）。
        '
        '引き数：
        'clsObservationPoint 対象とする観測点(代表観測点)。
        'clsBaseLineVector 偏心に使用する基線。'2009/11 H.Nakamura
        'nLat    方位標　緯度(度)。
        'nLon    方位標　経度(度)。
        'nHeight 方位標　楕円体高(m)。
        '
        '戻り値：方位標の方位角(ラジアン)を返す。
        Public Function GetMarkerDirection2(ByVal clsObservationPoint As ObservationPoint, ByVal clsBaseLineVector As BaseLineVector, ByVal nLat As Double, ByVal nLon As Double, ByVal nHeight As Double) As Double
            '回転行列。
            Dim R() As Double
            Dim clsEccentricPoint As CoordinatePoint
            '2009/11 H.Nakamura
            'R = GetRFromVector2(clsObservationPoint, clsEccentricPoint)
            R = GetRFromUsePoint(clsObservationPoint, clsBaseLineVector, clsEccentricPoint)
            GetMarkerDirection2 = GetT02(clsObservationPoint, nLat, nLon, nHeight, R)
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '方位標の方位角の取得（手入力）。
        '
        '引き数：
        'clsObservationPoint 対象とする観測点(代表観測点)。
        'clsBaseLineVector 偏心に使用する基線。'2009/11 H.Nakamura
        'nLat    方位標　緯度(度)。
        'nLon    方位標　経度(度)。
        'nHeight 方位標　楕円体高(m)。
        '
        '戻り値：方位標の方位角(ラジアン)を返す。
        */
        public double GetMarkerDirection2(ObservationPoint clsObservationPoint, BaseLineVector clsBaseLineVector, double nLat, double nLon, double nHeight)
        {
            //'回転行列。
            double[,] R;
            CoordinatePoint clsEccentricPoint = null;
            //'2009/11 H.Nakamura
            //'R = GetRFromVector2(clsObservationPoint, clsEccentricPoint)
            R = GetRFromUsePoint(clsObservationPoint, clsBaseLineVector, ref clsEccentricPoint);
            return GetT02(clsObservationPoint, nLat, nLon, nHeight, ref R);
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '回転行列Rの取得（GPSベクトル）。
        '
        'clsBaseLineVector で指定される方位標ベクトルの座標を偏心座標として回転行列Rを求める。
        '
        '引き数：
        'clsObservationPoint 対象とする観測点(代表観測点)。
        'clsBaseLineVector 方位標ベクトル。
        'clsEccentricPoint 偏心座標(偏心元の座標)が設定される。
        '戻り値：回転行列Rを返す。配列の要素は(2, 2)。
        Private Function GetRFromVector(ByVal clsObservationPoint As ObservationPoint, ByVal clsBaseLineVector As BaseLineVector, ByRef clsEccentricPoint As CoordinatePoint) As Double()

            '偏心点。
            If clsBaseLineVector.Analysis <= ANALYSIS_STATUS_FIX Then
                If clsBaseLineVector.AnalysisStrPoint.Number = clsObservationPoint.Number Then
                    Set clsEccentricPoint = clsBaseLineVector.CoordinateAnalysis.CreateCopy()
                Else
                    Set clsEccentricPoint = New CoordinatePointXYZ
                    clsEccentricPoint.X = clsBaseLineVector.CoordinateAnalysis.RoundX + clsBaseLineVector.VectorAnalysis.RoundX
                    clsEccentricPoint.Y = clsBaseLineVector.CoordinateAnalysis.RoundY + clsBaseLineVector.VectorAnalysis.RoundY
                    clsEccentricPoint.Z = clsBaseLineVector.CoordinateAnalysis.RoundZ + clsBaseLineVector.VectorAnalysis.RoundZ
                End If
            Else
                If clsBaseLineVector.StrPoint.Number = clsObservationPoint.Number Then
                    Set clsEccentricPoint = clsBaseLineVector.StrPoint.CoordinateReference.CreateCopy()
                Else
                    Set clsEccentricPoint = clsBaseLineVector.EndPoint.CoordinateReference.CreateCopy()
                End If
            End If
    
            GetRFromVector = GetR(clsEccentricPoint)
    
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '回転行列Rの取得（GPSベクトル）。
        '
        'clsBaseLineVector で指定される方位標ベクトルの座標を偏心座標として回転行列Rを求める。
        '
        '引き数：
        'clsObservationPoint 対象とする観測点(代表観測点)。
        'clsBaseLineVector 方位標ベクトル。
        'clsEccentricPoint 偏心座標(偏心元の座標)が設定される。
        '戻り値：回転行列Rを返す。配列の要素は(2, 2)。
        */
        private double[,] GetRFromVector(ObservationPoint clsObservationPoint, BaseLineVector clsBaseLineVector, ref CoordinatePoint clsEccentricPoint)
        {
            //'偏心点。
            if (clsBaseLineVector.Analysis <= ANALYSIS_STATUS.ANALYSIS_STATUS_FIX)
            {
                if (clsBaseLineVector.AnalysisStrPoint().Number() == clsObservationPoint.Number())
                {
                    clsEccentricPoint = clsBaseLineVector.CoordinateAnalysis().CreateCopy();
                }
                else
                {
                    clsEccentricPoint = new CoordinatePointXYZ
                    {
                        X = clsBaseLineVector.CoordinateAnalysis().RoundXX() + clsBaseLineVector.VectorAnalysis().RoundXX(),
                        Y = clsBaseLineVector.CoordinateAnalysis().RoundYY() + clsBaseLineVector.VectorAnalysis().RoundYY(),
                        Z = clsBaseLineVector.CoordinateAnalysis().RoundZZ() + clsBaseLineVector.VectorAnalysis().RoundZZ()
                    };
                }
            }
            else
            {
                if (clsBaseLineVector.StrPoint().Number() == clsObservationPoint.Number())
                {
                    clsEccentricPoint = clsBaseLineVector.StrPoint().CoordinateReference().CreateCopy();
                }
                else
                {
                    clsEccentricPoint = clsBaseLineVector.EndPoint().CoordinateReference().CreateCopy();
                }
            }
            return GetR(clsEccentricPoint);
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '2009/11 H.Nakamura
        '回転行列Rの取得（偏心に使用する基線）。
        '
        'clsBaseLineVector で指定される偏心に使用する基線の座標を偏心座標の候補として回転行列Rを求める。
        '
        '引き数：
        'clsObservationPoint 対象とする観測点(代表観測点)。
        'clsBaseLineVector 偏心に使用する基線。
        'clsEccentricPoint 偏心座標(偏心元の座標)が設定される。
        '戻り値：回転行列Rを返す。配列の要素は(2, 2)。
        Private Function GetRFromUsePoint(ByVal clsObservationPoint As ObservationPoint, ByVal clsBaseLineVector As BaseLineVector, ByRef clsEccentricPoint As CoordinatePoint) As Double()

            '偏心点。
            If clsObservationPoint.Fixed Then
                Set clsEccentricPoint = clsObservationPoint.CoordinateFixed.CreateCopy()
            ElseIf clsBaseLineVector.Enable And clsBaseLineVector.Analysis <= ANALYSIS_STATUS_FIX Then
                If clsBaseLineVector.AnalysisStrPoint.Number = clsObservationPoint.Number Then
                    Set clsEccentricPoint = clsBaseLineVector.CoordinateAnalysis.CreateCopy()
                Else
                    Set clsEccentricPoint = New CoordinatePointXYZ
                    clsEccentricPoint.X = clsBaseLineVector.CoordinateAnalysis.RoundX + clsBaseLineVector.VectorAnalysis.RoundX
                    clsEccentricPoint.Y = clsBaseLineVector.CoordinateAnalysis.RoundY + clsBaseLineVector.VectorAnalysis.RoundY
                    clsEccentricPoint.Z = clsBaseLineVector.CoordinateAnalysis.RoundZ + clsBaseLineVector.VectorAnalysis.RoundZ
                End If
            Else
                If clsBaseLineVector.StrPoint.Number = clsObservationPoint.Number Then
                    Set clsEccentricPoint = clsBaseLineVector.StrPoint.CoordinateReference.CreateCopy()
                Else
                    Set clsEccentricPoint = clsBaseLineVector.EndPoint.CoordinateReference.CreateCopy()
                End If
            End If
    
            GetRFromUsePoint = GetR(clsEccentricPoint)
    
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '2009/11 H.Nakamura
        '回転行列Rの取得（偏心に使用する基線）。
        '
        'clsBaseLineVector で指定される偏心に使用する基線の座標を偏心座標の候補として回転行列Rを求める。
        '
        '引き数：
        'clsObservationPoint 対象とする観測点(代表観測点)。
        'clsBaseLineVector 偏心に使用する基線。
        'clsEccentricPoint 偏心座標(偏心元の座標)が設定される。
        '戻り値：回転行列Rを返す。配列の要素は(2, 2)。
        */
        private double[,] GetRFromUsePoint(ObservationPoint clsObservationPoint, BaseLineVector clsBaseLineVector, ref CoordinatePoint clsEccentricPoint)
        {
            //'偏心点。
            if (clsObservationPoint.Fixed())
            {
                clsEccentricPoint = clsObservationPoint.CoordinateFixed().CreateCopy();
            }
            else if (clsBaseLineVector.Enable() && clsBaseLineVector.Analysis <= ANALYSIS_STATUS.ANALYSIS_STATUS_FIX)
            {
                if (clsBaseLineVector.AnalysisStrPoint().Number() == clsObservationPoint.Number())
                {
                    clsEccentricPoint = clsBaseLineVector.CoordinateAnalysis().CreateCopy();
                }
                else
                {
                    clsEccentricPoint = new CoordinatePointXYZ();
                    clsEccentricPoint.X = clsBaseLineVector.CoordinateAnalysis().RoundXX() + clsBaseLineVector.VectorAnalysis().RoundXX();
                    clsEccentricPoint.Y = clsBaseLineVector.CoordinateAnalysis().RoundYY() + clsBaseLineVector.VectorAnalysis().RoundYY();
                    clsEccentricPoint.Z = clsBaseLineVector.CoordinateAnalysis().RoundZZ() + clsBaseLineVector.VectorAnalysis().RoundZZ();
                }
            }
            else
            {
                if (clsBaseLineVector.StrPoint().Number() == clsObservationPoint.Number())
                {
                    clsEccentricPoint = clsBaseLineVector.StrPoint().CoordinateReference().CreateCopy();
                }
                else
                {
                    clsEccentricPoint = clsBaseLineVector.EndPoint().CoordinateReference().CreateCopy();
                }
            }
            return GetR(clsEccentricPoint);
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '削除。'2009/11 H.Nakamura
        '回転行列Rの取得（手入力）。
        '
        'clsObservationPoint の座標を偏心座標として回転行列Rを求める。
        '
        '引き数：
        'clsObservationPoint 対象とする観測点(代表観測点)。
        'clsEccentricPoint 偏心座標(偏心元の座標)が設定される。
        '
        '戻り値：回転行列Rを返す。配列の要素は(2, 2)。
        'Private Function GetRFromVector2(ByVal clsObservationPoint As ObservationPoint, ByRef clsEccentricPoint As CoordinatePoint) As Double()
        '
        '    '偏心点。
        '    If clsObservationPoint.Fixed = True Then
        '        Set clsEccentricPoint = New CoordinatePointFix
        '        clsEccentricPoint.X = clsObservationPoint.CoordinateFixed.X
        '        clsEccentricPoint.Y = clsObservationPoint.CoordinateFixed.Y
        '        clsEccentricPoint.Z = clsObservationPoint.CoordinateFixed.Z
        '    Else
        '        Set clsEccentricPoint = New CoordinatePointXYZ
        '        clsEccentricPoint.X = clsObservationPoint.CoordinateObservation.RoundX
        '        clsEccentricPoint.Y = clsObservationPoint.CoordinateObservation.RoundY
        '        clsEccentricPoint.Z = clsObservationPoint.CoordinateObservation.RoundZ
        '    End If
        '
        '    GetRFromVector2 = GetR(clsEccentricPoint)
        '
        'End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '回転行列Rの取得（GPSベクトル）。
        '
        'clsEccentricPoint で指定される座標を偏心座標として回転行列Rを求める。
        '
        '引き数：
        'clsEccentricPoint 偏心座標。
        '
        '戻り値：回転行列Rを返す。配列の要素は(2, 2)。
        Private Function GetR(ByVal clsEccentricPoint As CoordinatePoint) As Double()

            Dim φ As Double
            Dim λ As Double
            Dim nHeight As Double
            Dim vAlt As Variant
            Call clsEccentricPoint.GetDEG(φ, λ, nHeight, vAlt, "")
            φ = φ / 180 * PAI
            λ = λ / 180 * PAI
    
            Dim R(2, 2) As Double
            R(0, 0) = -Sin(φ) * Cos(λ)
            R(0, 1) = -Sin(φ) * Sin(λ)
            R(0, 2) = Cos(φ)
            R(1, 0) = -Sin(λ)
            R(1, 1) = Cos(λ)
            R(1, 2) = 0
            R(2, 0) = Cos(φ) * Cos(λ)
            R(2, 1) = Cos(φ) * Sin(λ)
            R(2, 2) = Sin(φ)
    
            GetR = R
    
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '回転行列Rの取得（GPSベクトル）。
        '
        'clsEccentricPoint で指定される座標を偏心座標として回転行列Rを求める。
        '
        '引き数：
        'clsEccentricPoint 偏心座標。
        '
        '戻り値：回転行列Rを返す。配列の要素は(2, 2)。
        */
        private double[,] GetR(CoordinatePoint clsEccentricPoint)
        {
            double φ = 0;
            double λ = 0;
            double nHeight = 0;
            //Dim vAlt As Variant
            double vAlt = 0;
            clsEccentricPoint.GetDEG(ref φ, ref λ, ref nHeight, ref vAlt, "");
            φ = φ / (180 * PAI);
            λ = λ / (180 * PAI);

            double[,] R = new double[2, 2];
            R[0, 0] = -Math.Sin(φ) * Math.Cos(λ);
            R[0, 1] = -Math.Sin(φ) * Math.Sin(λ);
            R[0, 2] = Math.Cos(φ);
            R[1, 0] = -Math.Sin(λ);
            R[1, 1] = Math.Cos(λ);
            R[1, 2] = 0;
            R[2, 0] = Math.Cos(φ) * Math.Cos(λ);
            R[2, 1] = Math.Cos(φ) * Math.Sin(λ);
            R[2, 2] = Math.Sin(φ);

            return R;

        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '方位標の方位角の取得（手入力）。
        '
        '引き数：
        'clsObservationPoint 対象とする観測点(代表観測点)。
        'clsBaseLineVector 方位標ベクトル。
        'R 回転行列。配列の要素は(2, 2)。
        '
        '戻り値：方位標の方位角(ラジアン)を返す。
        Private Function GetT0(ByVal clsObservationPoint As ObservationPoint, ByVal clsBaseLineVector As BaseLineVector, ByRef R() As Double) As Double

            '方位標。
            Dim dXYZ(2, 0) As Double
            If clsBaseLineVector.Analysis <= ANALYSIS_STATUS_FIX Then
                dXYZ(0, 0) = clsBaseLineVector.VectorAnalysis.RoundX
                dXYZ(1, 0) = clsBaseLineVector.VectorAnalysis.RoundY
                dXYZ(2, 0) = clsBaseLineVector.VectorAnalysis.RoundZ
                If clsBaseLineVector.AnalysisStrPoint.Number <> clsObservationPoint.Number Then
                    dXYZ(0, 0) = -dXYZ(0, 0)
                    dXYZ(1, 0) = -dXYZ(1, 0)
                    dXYZ(2, 0) = -dXYZ(2, 0)
                End If
            Else
                dXYZ(0, 0) = clsBaseLineVector.VectorObservation.RoundX
                dXYZ(1, 0) = clsBaseLineVector.VectorObservation.RoundY
                dXYZ(2, 0) = clsBaseLineVector.VectorObservation.RoundZ
                If clsBaseLineVector.StrPoint.Number <> clsObservationPoint.Number Then
                    dXYZ(0, 0) = -dXYZ(0, 0)
                    dXYZ(1, 0) = -dXYZ(1, 0)
                    dXYZ(2, 0) = -dXYZ(2, 0)
                End If
            End If
    
            '方位角。
            Dim dNEU() As Double
            dNEU = Product(R, dXYZ)
    
            Dim T0 As Double
            T0 = Atn2(dNEU(1, 0), dNEU(0, 0))
            If T0 < 0 Then T0 = T0 + PAI * 2
    
            GetT0 = T0
    
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '方位標の方位角の取得（手入力）。
        '
        '引き数：
        'clsObservationPoint 対象とする観測点(代表観測点)。
        'clsBaseLineVector 方位標ベクトル。
        'R 回転行列。配列の要素は(2, 2)。
        '
        '戻り値：方位標の方位角(ラジアン)を返す。
        */
        private double GetT0(ObservationPoint clsObservationPoint, BaseLineVector clsBaseLineVector, ref double[,] R)
        {
            //'方位標。
            double[,] dXYZ = new double[2, 0];
            if (clsBaseLineVector.Analysis <= ANALYSIS_STATUS.ANALYSIS_STATUS_FIX)
            {
                dXYZ[0, 0] = clsBaseLineVector.VectorAnalysis().RoundXX();
                dXYZ[1, 0] = clsBaseLineVector.VectorAnalysis().RoundYY();
                dXYZ[2, 0] = clsBaseLineVector.VectorAnalysis().RoundZZ();
                if (clsBaseLineVector.AnalysisStrPoint().Number() != clsObservationPoint.Number())
                {
                    dXYZ[0, 0] = -dXYZ[0, 0];
                    dXYZ[1, 0] = -dXYZ[1, 0];
                    dXYZ[2, 0] = -dXYZ[2, 0];
                }
            }
            else
            {
                dXYZ[0, 0] = clsBaseLineVector.VectorObservation().RoundXX();
                dXYZ[1, 0] = clsBaseLineVector.VectorObservation().RoundYY();
                dXYZ[2, 0] = clsBaseLineVector.VectorObservation().RoundZZ();
                if (clsBaseLineVector.StrPoint().Number() != clsObservationPoint.Number())
                {
                    dXYZ[0, 0] = -dXYZ[0, 0];
                    dXYZ[1, 0] = -dXYZ[1, 0];
                    dXYZ[2, 0] = -dXYZ[2, 0];
                }
            }

            //'方位角。
            double[,] dNEU;
            dNEU = Product(ref R, ref dXYZ);


            double T0;
            T0 = Atn2(dNEU[1, 0], dNEU[0, 0]);
            if (T0 < 0)
            {
                T0 = T0 + PAI * 2;
            }

            return T0;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '方位標の方位角の取得（手入力）。
        '
        '引き数：
        'clsObservationPoint 対象とする観測点(代表観測点)。
        'nLat    方位標　緯度(度)。
        'nLon    方位標　経度(度)。
        'nHeight 方位標　楕円体高(m)。
        'R 回転行列。配列の要素は(2, 2)。
        '
        '戻り値：方位標の方位角(ラジアン)を返す。
        Private Function GetT02(ByVal clsObservationPoint As ObservationPoint, ByVal nLat As Double, ByVal nLon As Double, ByVal nHeight As Double, ByRef R() As Double) As Double

            Dim nX As Double
            Dim nY As Double
            Dim nZ As Double
    
            Call WGS84dms_to_WGS84xyz(nLat, nLon, nHeight, nX, nY, nZ)
    
            '方位標。
            Dim dXYZ(2, 0) As Double
            If clsObservationPoint.Fixed = True Then
                dXYZ(0, 0) = nX - clsObservationPoint.CoordinateFixed.RoundX
                dXYZ(1, 0) = nY - clsObservationPoint.CoordinateFixed.RoundY
                dXYZ(2, 0) = nZ - clsObservationPoint.CoordinateFixed.RoundZ
            Else
                dXYZ(0, 0) = nX - clsObservationPoint.CoordinateObservation.RoundX
                dXYZ(1, 0) = nY - clsObservationPoint.CoordinateObservation.RoundY
                dXYZ(2, 0) = nZ - clsObservationPoint.CoordinateObservation.RoundZ
            End If
    
            '方位角。
            Dim dNEU() As Double
            dNEU = Product(R, dXYZ)
    
            Dim T0 As Double
            T0 = Atn2(dNEU(1, 0), dNEU(0, 0))
            If T0 < 0 Then T0 = T0 + PAI * 2
    
            GetT02 = T0
    
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '方位標の方位角の取得（手入力）。
        '
        '引き数：
        'clsObservationPoint 対象とする観測点(代表観測点)。
        'nLat    方位標　緯度(度)。
        'nLon    方位標　経度(度)。
        'nHeight 方位標　楕円体高(m)。
        'R 回転行列。配列の要素は(2, 2)。
        '
        '戻り値：方位標の方位角(ラジアン)を返す。
        */
        private double GetT02(ObservationPoint clsObservationPoint, double nLat, double nLon, double nHeight, ref double[,] R)
        {
            double nX = 0;
            double nY = 0;
            double nZ = 0;

            WGS84dms_to_WGS84xyz(nLat, nLon, nHeight, ref nX, ref nY, ref nZ);

            //'方位標。
            double[,] dXYZ = new double[2, 0];
            if (clsObservationPoint.Fixed() == true)
            {
                dXYZ[0, 0] = nX - clsObservationPoint.CoordinateFixed().RoundXX();
                dXYZ[1, 0] = nY - clsObservationPoint.CoordinateFixed().RoundYY();
                dXYZ[2, 0] = nZ - clsObservationPoint.CoordinateFixed().RoundZZ();
            }
            else
            {
                dXYZ[0, 0] = nX - clsObservationPoint.CoordinateObservation().RoundXX();
                dXYZ[1, 0] = nY - clsObservationPoint.CoordinateObservation().RoundYY();
                dXYZ[2, 0] = nZ - clsObservationPoint.CoordinateObservation().RoundZZ();
            }

            //'方位角。
            double[,] dNEU;
            dNEU = Product(ref R, ref dXYZ);


            double T0;
            T0 = Atn2(dNEU[1, 0], dNEU[0, 0]);
            if (T0 < 0)
            {
                T0 += PAI * 2;
            }

            return T0;

        }
        //==========================================================================================
    }
}
