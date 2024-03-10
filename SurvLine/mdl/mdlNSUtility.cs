using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Windows.Forms;
using static SurvLine.mdl.DEFINE;
using static SurvLine.mdl.MdlNSDefine;
using static SurvLine.mdl.MdlNSSDefine;
using static SurvLine.mdl.MdlMatrix;
using static SurvLine.mdl.MdlUtility;
using static SurvLine.mdl.MdlSurveyParam;
using Microsoft.VisualBasic;
using static System.Net.WebRequestMethods;
using System.Text.RegularExpressions;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;
using System.Security.Policy;
using Microsoft.VisualBasic.Logging;
using System.Drawing;
using static System.Windows.Forms.AxHost;
using System.Xml.Linq;
using Microsoft.VisualBasic.Devices;
using System.Diagnostics;

namespace SurvLine.mdl
{
    internal class MdlNSUtility
    {
        //==========================================================================================
        /*[VB]
        '*******************************************************************************
        'NS用ユーティリティ

        Option Explicit
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //==========================================================================================

        //==========================================================================================
        //[C#]
        public MdlNSUtility()
        {
            ID = 1;
            AppPath = Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath);
            AppTitle = "NS-Survey";
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '定数。
        Public Const COORD_MIN_LAT As Long = 0 '緯度最小値(度、以上)。
        Public Const COORD_MAX_LAT As Long = 90 '緯度最大値(度、未満)。
        Public Const COORD_MIN_LON As Long = 90 '経度最小値(度、以上)。
        Public Const COORD_MAX_LON As Long = 180 '経度最大値(度、未満)。
        Public Const COORD_MIN_HEIGHT As Long = -32767 '楕円体高最小値(ｍ、以上)。
        Public Const COORD_MAX_HEIGHT As Long = 32768 '楕円体高最大値(ｍ、未満)。
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'定数。
        public const long COORD_MIN_LAT = 0;            //'緯度最小値(度、以上)。
        public const long COORD_MAX_LAT = 90;           //'緯度最大値(度、未満)。
        public const long COORD_MIN_LON = 90;           //'経度最小値(度、以上)。
        public const long COORD_MAX_LON = 180;          //'経度最大値(度、未満)。
        public const long COORD_MIN_HEIGHT = -32767;    //'楕円体高最小値(ｍ、以上)。
        public const long COORD_MAX_HEIGHT = 32768;     //'楕円体高最大値(ｍ、未満)。

        public static int ID;
        public static string AppPath;
        public static string AppTitle;
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'ベクトルの長さを求める。
        '
        '引き数：
        'V ベクトル。
        '
        '戻り値：ベクトルの長さ。
        Public Function GetVectorLength(ByVal V As CoordinatePoint) As Double
            GetVectorLength = Sqr(V.X* V.X + V.Y* V.Y + V.Z* V.Z)
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        'ベクトルの長さを求める。
        '
        '引き数：
        'V ベクトル。
        '
        '戻り値：ベクトルの長さ。
        */
        public double GetVectorLength(CoordinatePoint V)
        {
            return Math.Sqrt((V.X * V.X) + (V.Y * V.Y) + (V.Z * V.Z));
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'ベクトルの長さを求める。
        '
        '丸めてから計算。
        '
        '引き数：
        'V ベクトル。
        '
        '戻り値：ベクトルの長さ。
        Public Function GetVectorLengthRound(ByVal V As CoordinatePoint) As Double
            GetVectorLengthRound = Sqr(V.RoundX* V.RoundX + V.RoundY* V.RoundY + V.RoundZ* V.RoundZ)
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        'ベクトルの長さを求める。
        '
        '丸めてから計算。
        '
        '引き数：
        'V ベクトル。
        '
        '戻り値：ベクトルの長さ。
        */
        public double GetVectorLengthRound(CoordinatePoint V)
        {
            return Math.Sqrt((V.RoundXX() * V.RoundXX()) + (V.RoundYY() * V.RoundYY()) + (V.RoundZZ() * V.RoundZZ()));
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '２点間のベクトルを取得する。
        '
        '引き数：
        'S 始点座標。
        'E 終点座標。
        '
        '戻り値：ベクトル。
        Public Function GetVector(ByVal S As CoordinatePoint, ByVal E As CoordinatePoint) As CoordinatePoint
            Dim clsVector As New CoordinatePointXYZ
            clsVector.X = E.X - S.X
            clsVector.Y = E.Y - S.Y
            clsVector.Z = E.Z - S.Z
            Set GetVector = clsVector
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '２点間のベクトルを取得する。
        '
        '引き数：
        'S 始点座標。
        'E 終点座標。
        '
        '戻り値：ベクトル。
        */
        public CoordinatePoint GetVector(CoordinatePoint S, CoordinatePoint E)
        {
            CoordinatePointXYZ clsVector = new CoordinatePointXYZ();
            clsVector.X = E.X - S.X;
            clsVector.Y = E.Y - S.Y;
            clsVector.Z = E.Z - S.Z;
            return clsVector;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '２点間のベクトルを取得する。
        '
        '丸めてから計算。
        '
        '引き数：
        'S 始点座標。
        'E 終点座標。
        '
        '戻り値：ベクトル。
        Public Function GetVectorRound(ByVal S As CoordinatePoint, ByVal E As CoordinatePoint) As CoordinatePoint
            Dim clsVector As New CoordinatePointXYZ
            clsVector.X = E.RoundX - S.RoundX
            clsVector.Y = E.RoundY - S.RoundY
            clsVector.Z = E.RoundZ - S.RoundZ
            Set GetVectorRound = clsVector
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '２点間のベクトルを取得する。
        '
        '丸めてから計算。
        '
        '引き数：
        'S 始点座標。
        'E 終点座標。
        '
        '戻り値：ベクトル。
        */
        public CoordinatePoint GetVectorRound(CoordinatePoint S, CoordinatePoint E)
        {
            CoordinatePointXYZ clsVector = new CoordinatePointXYZ();
            clsVector.X = E.RoundXX() - S.RoundXX();
            clsVector.Y = E.RoundYY() - S.RoundYY();
            clsVector.Z = E.RoundZZ() - S.RoundZZ();
            return clsVector;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '２点間の距離を求める。
        '
        '引き数：
        'p0 座標。
        'p1 座標。
        '
        '戻り値：２点間の距離。
        Public Function GetDistance(ByVal p0 As CoordinatePoint, ByVal p1 As CoordinatePoint) As Double
            Dim nX As Double
            Dim nY As Double
            Dim nZ As Double
            nX = p0.X - p1.X
            nY = p0.Y - p1.Y
            nZ = p0.Z - p1.Z
            GetDistance = Sqr(nX * nX + nY * nY + nZ * nZ)
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '２点間の距離を求める。
        '
        '引き数：
        'p0 座標。
        'p1 座標。
        '
        '戻り値：２点間の距離。
        */
        public double GetDistance(CoordinatePoint p0, CoordinatePoint p1)
        {
            double nX;
            double nY;
            double nZ;
            nX = p0.X - p1.X;
            nY = p0.Y - p1.Y;
            nZ = p0.Z - p1.Z;
            return Math.Sqrt((nX * nX) + (nY * nY) + (nZ * nZ));
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '２点間の距離を求める。
        '
        '丸めてから計算。
        '
        '引き数：
        'p0 座標。
        'p1 座標。
        '
        '戻り値：２点間の距離。
        Public Function GetDistanceRound(ByVal p0 As CoordinatePoint, ByVal p1 As CoordinatePoint) As Double
            Dim nX As Double
            Dim nY As Double
            Dim nZ As Double
            nX = p0.RoundX - p1.RoundX
            nY = p0.RoundY - p1.RoundY
            nZ = p0.RoundZ - p1.RoundZ
            GetDistanceRound = Sqr(nX * nX + nY * nY + nZ * nZ)
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '２点間の距離を求める。
        '
        '丸めてから計算。
        '
        '引き数：
        'p0 座標。
        'p1 座標。
        '
        '戻り値：２点間の距離。
        */
        public double GetDistanceRound(CoordinatePoint p0, CoordinatePoint p1)
        {
            double nX;
            double nY;
            double nZ;
            nX = p0.RoundXX() - p1.RoundXX();
            nY = p0.RoundYY() - p1.RoundYY();
            nZ = p0.RoundZZ() - p1.RoundZZ();
            return Math.Sqrt(nX * nX + nY * nY + nZ * nZ);
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '２つの座標を加算する。
        '
        '引き数：
        'S 始点座標。
        'V ベクトル。
        '
        '戻り値：終点座標。
        Public Function AddCoordinate(ByVal S As CoordinatePoint, ByVal V As CoordinatePoint) As CoordinatePoint
            Dim clsCoordinatePoint As New CoordinatePointXYZ
            clsCoordinatePoint.X = S.X + V.X
            clsCoordinatePoint.Y = S.Y + V.Y
            clsCoordinatePoint.Z = S.Z + V.Z
            Set AddCoordinate = clsCoordinatePoint
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '２つの座標を加算する。
        '
        '引き数：
        'S 始点座標。
        'V ベクトル。
        '
        '戻り値：終点座標。
        */
        public CoordinatePoint AddCoordinate(CoordinatePoint S, CoordinatePoint V)
        {
            CoordinatePointXYZ clsCoordinatePoint = new CoordinatePointXYZ();
            clsCoordinatePoint.X = S.X + V.X;
            clsCoordinatePoint.Y = S.Y + V.Y;
            clsCoordinatePoint.Z = S.Z + V.Z;
            return clsCoordinatePoint;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '２つの座標を加算する。
        '
        '丸めてから計算。
        '
        '引き数：
        'S 始点座標。
        'V ベクトル。
        '
        '戻り値：終点座標。
        Public Function AddCoordinateRound(ByVal S As CoordinatePoint, ByVal V As CoordinatePoint) As CoordinatePoint
            Dim clsCoordinatePoint As New CoordinatePointXYZ
            clsCoordinatePoint.X = S.RoundX + V.RoundX
            clsCoordinatePoint.Y = S.RoundY + V.RoundY
            clsCoordinatePoint.Z = S.RoundZ + V.RoundZ
            Set AddCoordinateRound = clsCoordinatePoint
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '２つの座標を加算する。
        '
        '丸めてから計算。
        '
        '引き数：
        'S 始点座標。
        'V ベクトル。
        '
        '戻り値：終点座標。
        */
        public static CoordinatePoint AddCoordinateRound(CoordinatePoint S, CoordinatePoint V)
        {
            CoordinatePointXYZ clsCoordinatePoint = new CoordinatePointXYZ();
            clsCoordinatePoint.X = S.RoundXX() + V.RoundXX();
            clsCoordinatePoint.Y = S.RoundYY() + V.RoundYY();
            clsCoordinatePoint.Z = S.RoundZZ() + V.RoundZZ();
            return clsCoordinatePoint;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '斜距離の標準偏差。
        '
        '引き数：
        'V ベクトル。
        'D 分散･共分散。
        '
        '戻り値：斜距離の標準偏差。
        Public Function GetSlantStddev(ByVal V As CoordinatePoint, ByVal D As Dispersion) As Double
            Dim L As Double
            L = GetVectorLength(V)
            If Abs(L) <= FLT_EPSILON Then
                GetSlantStddev = -1
                Exit Function
            End If
            Dim G(0, 2) As Double
            G(0, 0) = V.X / L
            G(0, 1) = V.Y / L
            G(0, 2) = V.Z / L
            Dim P(2, 2) As Double
            P(0, 0) = D.XX
            P(0, 1) = D.XY
            P(0, 2) = D.XZ
                P(1, 0) = D.XY
                P(1, 1) = D.YY
                P(1, 2) = D.YZ
                P(2, 0) = D.XZ
            P(2, 1) = D.YZ
            P(2, 2) = D.ZZ
            Dim Qd() As Double
            Qd = Product(G, P)
            Qd = Product(Qd, Transpose(G))
            GetSlantStddev = Sqr(Qd(0, 0))
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '斜距離の標準偏差。
        '
        '引き数：
        'V ベクトル。
        'D 分散･共分散。
        '
        '戻り値：斜距離の標準偏差。
        */
        public double GetSlantStddev(CoordinatePoint V, Dispersion D)
        {
            double L;
            L = GetVectorLength(V);
            if (Math.Abs(L) <= FLT_EPSILON)
            {
                return -1;
            }
            double[,] G = new double[0, 2];
            G[0, 0] = V.X / L;
            G[0, 1] = V.Y / L;
            G[0, 2] = V.Z / L;
            double[,] P = new double[2, 2];
            P[0, 0] = D.XX;
            P[0, 1] = D.XY;
            P[0, 2] = D.XZ;
            P[1, 0] = D.XY;
            P[1, 1] = D.YY;
            P[1, 2] = D.YZ;
            P[2, 0] = D.XZ;
            P[2, 1] = D.YZ;
            P[2, 2] = D.ZZ;
            double[,] Qd = Product(ref G, ref P);
            double[,] Tr = Transpose(ref G);
            //Qd = Product(ref Qd, ref Transpose(ref G));
            Qd = Product(ref Qd, ref Tr);
            return Math.Sqrt(Qd[0, 0]);
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '回転。
        '
        'XYZをNEUに回転する。
        '
        '引き数：
        'clsCoordinatePoint 既知点の座標。
        'ΣdXYZ 回転させる座標。配列の要素は(2, 0)。
        '
        '戻り値：NEU行列。配列の要素は(2, 0)。
        Public Function RotateΔXYZ(ByVal clsCoordinatePoint As CoordinatePoint, ByRef ΣdXYZ() As Double) As Double()

            '既知点の緯度経度。
            Dim nLat As Double
            Dim nLon As Double
            Dim nHeight As Double
            Dim vAlt As Variant
            Call clsCoordinatePoint.GetDEG(nLat, nLon, nHeight, vAlt, "")
    
            'ΔNEU。
            RotateΔXYZ = RotateΔXYZByLatLon(nLat, nLon, ΣdXYZ)


        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '回転。
        '
        'XYZをNEUに回転する。
        '
        '引き数：
        'clsCoordinatePoint 既知点の座標。
        'ΣdXYZ 回転させる座標。配列の要素は(2, 0)。
        '
        '戻り値：NEU行列。配列の要素は(2, 0)。
        */
        public static double[,] RotateΔXYZ(CoordinatePoint clsCoordinatePoint, ref double[,] ΣdXYZ)
        {
            //'既知点の緯度経度。
            double nLat = 0;
            double nLon = 0;
            double nHeight = 0;
            double vAlt = 0;
            clsCoordinatePoint.GetDEG(ref nLat, ref nLon, ref nHeight, ref vAlt, "");

            //'ΔNEU。
            return RotateΔXYZByLatLon(nLat, nLon, ref ΣdXYZ);


        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '回転。
        '
        'XYZをNEUに回転する。
        '
        '引き数：
        'nLat 既知点の緯度。
        'nLon 既知点の経度。
        'ΣdXYZ 回転させる座標。配列の要素は(2, 0)。
        '
        '戻り値：NEU行列。配列の要素は(2, 0)。
        Public Function RotateΔXYZByLatLon(ByVal nLat As Double, ByVal nLon As Double, ByRef ΣdXYZ() As Double) As Double()

            '既知点の緯度経度。
            Dim φ As Double
            Dim λ As Double
            φ = nLat / 180# * PAI
            λ = nLon / 180# * PAI
    
            'R
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
    
            'ΔNEU。
            RotateΔXYZByLatLon = Product(R, ΣdXYZ)


        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '回転。
        '
        'XYZをNEUに回転する。
        '
        '引き数：
        'nLat 既知点の緯度。
        'nLon 既知点の経度。
        'ΣdXYZ 回転させる座標。配列の要素は(2, 0)。
        '
        '戻り値：NEU行列。配列の要素は(2, 0)。
        */
        public static double[,] RotateΔXYZByLatLon(double nLat, double nLon, ref double[,] ΣdXYZ)
        {
            //'既知点の緯度経度。
            double φ;
            double λ;
            φ = nLat / (180 * PAI);
            λ = nLon / (180 * PAI);

            //'R
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

            //'ΔNEU。
            return Product(ref R, ref ΣdXYZ);

        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'ジオイドモデルのバージョンを取得する。
        '
        '引き数：
        'sPath ジオイドモデルファイルのパス。
        '
        '戻り値：バージョン文字列。
        Public Function GetGeoidoVersion(ByVal sPath As String) As String
            'メッシュファイルから１行読み込む。
            On Error GoTo FileErrorHandler
            Dim clsFile As New FileNumber
            Open sPath For Input Access Read Lock Write As clsFile.Number
            Dim sBuff As String
            Line Input #clsFile.Number, sBuff
            Call clsFile.CloseFile
            'バージョン数値を取得する。
            Dim nLen As Long
            Dim nChr As Long
            Dim i As Long
            nLen = Len(sBuff)
            For i = 51 To nLen
                '0～9？
                nChr = Asc(Mid$(sBuff, i, 1))
                If &H30& <= nChr And nChr <= &H39& Then Exit For
            Next
            If i <= nLen Then
                GetGeoidoVersion = RTrim$(Mid$(sBuff, i, 59 - i))
                Exit Function
            End If
        FileErrorHandler:
            GetGeoidoVersion = "不明"
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        'ジオイドモデルのバージョンを取得する。
        '
        '引き数：
        'sPath ジオイドモデルファイルのパス。
        '
        '戻り値：バージョン文字列。
        */
        public string GetGeoidoVersion(string sPath)
        {
#if false
            //'メッシュファイルから１行読み込む。
            try
            {
                /*
                Dim clsFile As New FileNumber
                Open sPath For Input Access Read Lock Write As clsFile.Number
                */
                string sBuff = null;
                /*
                Line Input #clsFile.Number, sBuff
                Call clsFile.CloseFile
                */
                //'バージョン数値を取得する。
                long nLen;
                long nChr;
                long i;
                nLen = sBuff.Length;
                for (i = 51; i < nLen; i++)
                {
                    //'0～9？
                    nChr = Convert.ToInt32(Mid(sBuff, (int)i, 1));
                    if (0x30 <= nChr && nChr <= 0x39)
                    {
                        break;
                    }
                }
                if (i <= nLen)
                {
                    string dt = Mid(sBuff, (int)i, 59 - (int)i);
                    return dt.Trim();
                }
                return "";
            }

            catch (Exception e)
            {
                return "不明";
            }
#else
            int aa = ID;
            return aa.ToString();
#endif
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'ジオイドモデル名称を取得する。
        '
        '引き数：
        'sPath ジオイドモデルファイルのパス。
        '
        '戻り値：ジオイドモデル名称文字列。
        Public Function GetGeoidoModelName(ByVal sPath As String, ByVal sDefName As String) As String
            '2015/05/14 新しいジオイドモデルファイルが正式に公開されるまでは機能を隠す。
            GetGeoidoModelName = sDefName
        '    'メッシュファイルから１行読み込む。
        '    On Error GoTo FileErrorHandler
        '    Dim clsFile As New FileNumber
        '    Open sPath For Input Access Read Lock Write As clsFile.Number
        '    Dim sBuff As String
        '    Line Input #clsFile.Number, sBuff
        '    Call clsFile.CloseFile
        '    'バージョン数値を取得する。
        '    Dim nLen As Long
        '    Dim nChr As Long
        '    Dim i As Long
        '    nLen = Len(sBuff)
        '    For i = 59 To nLen
        '        '空白？
        '        nChr = Asc(Mid$(sBuff, i, 1))
        '        If &H20& <> nChr Then Exit For
        '    Next
        '    If i <= nLen Then
        '        GetGeoidoModelName = "日本のジオイド" & RTrim$(Mid$(sBuff, i, 5))
        '    Else
        '        GetGeoidoModelName = sDefName
        '    End If
            Exit Function
        FileErrorHandler:
            GetGeoidoModelName = ""
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        'ジオイドモデル名称を取得する。
        '
        '引き数：
        'sPath ジオイドモデルファイルのパス。
        '
        '戻り値：ジオイドモデル名称文字列。
        */
        public string GetGeoidoModelName(string sPath, string sDefName)
        {
            //'2015/05/14 新しいジオイドモデルファイルが正式に公開されるまでは機能を隠す。
            return sDefName;
            /*
            '    'メッシュファイルから１行読み込む。
            '    On Error GoTo FileErrorHandler
            '    Dim clsFile As New FileNumber
            '    Open sPath For Input Access Read Lock Write As clsFile.Number
            '    Dim sBuff As String
            '    Line Input #clsFile.Number, sBuff
            '    Call clsFile.CloseFile
            '    'バージョン数値を取得する。
            '    Dim nLen As Long
            '    Dim nChr As Long
            '    Dim i As Long
            '    nLen = Len(sBuff)
            '    For i = 59 To nLen
            '        '空白？
            '        nChr = Asc(Mid$(sBuff, i, 1))
            '        If &H20& <> nChr Then Exit For
            '    Next
            '    If i <= nLen Then
            '        GetGeoidoModelName = "日本のジオイド" & RTrim$(Mid$(sBuff, i, 5))
            '    Else
            '        GetGeoidoModelName = sDefName
            '    End If
                Exit Function
            FileErrorHandler:
                GetGeoidoModelName = ""
            */
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '座標系原点緯度。
        '
        '引き数：
        'nCoordNum 座標系番号(1～19)。
        '
        '戻り値：座標系原点緯度(度)。
        Public Function COORDORIGIN_LAT(ByVal nCoordNum As Long) As Double
            Select Case nCoordNum
            Case 1
                COORDORIGIN_LAT = COORDORIGIN_LAT_01
            Case 2
                COORDORIGIN_LAT = COORDORIGIN_LAT_02
            Case 3
                COORDORIGIN_LAT = COORDORIGIN_LAT_03
            Case 4
                COORDORIGIN_LAT = COORDORIGIN_LAT_04
            Case 5
                COORDORIGIN_LAT = COORDORIGIN_LAT_05
            Case 6
                COORDORIGIN_LAT = COORDORIGIN_LAT_06
            Case 7
                COORDORIGIN_LAT = COORDORIGIN_LAT_07
            Case 8
                COORDORIGIN_LAT = COORDORIGIN_LAT_08
            Case 9
                COORDORIGIN_LAT = COORDORIGIN_LAT_09
            Case 10
                COORDORIGIN_LAT = COORDORIGIN_LAT_10
            Case 11
                COORDORIGIN_LAT = COORDORIGIN_LAT_11
            Case 12
                COORDORIGIN_LAT = COORDORIGIN_LAT_12
            Case 13
                COORDORIGIN_LAT = COORDORIGIN_LAT_13
            Case 14
                COORDORIGIN_LAT = COORDORIGIN_LAT_14
            Case 15
                COORDORIGIN_LAT = COORDORIGIN_LAT_15
            Case 16
                COORDORIGIN_LAT = COORDORIGIN_LAT_16
            Case 17
                COORDORIGIN_LAT = COORDORIGIN_LAT_17
            Case 18
                COORDORIGIN_LAT = COORDORIGIN_LAT_18
            Case 19
                COORDORIGIN_LAT = COORDORIGIN_LAT_19
            Case Else
                COORDORIGIN_LAT = 0
            End Select
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '座標系原点緯度。
        '
        '引き数：
        'nCoordNum 座標系番号(1～19)。
        '
        '戻り値：座標系原点緯度(度)。
        */
        public double COORDORIGIN_LAT(long nCoordNum)
        {
            switch (nCoordNum)
            {
                case 1:
                    return COORDORIGIN_LAT_01;
                case 2:
                    return COORDORIGIN_LAT_02;
                case 3:
                    return COORDORIGIN_LAT_03;
                case 4:
                    return COORDORIGIN_LAT_04;
                case 5:
                    return COORDORIGIN_LAT_05;
                case 6:
                    return COORDORIGIN_LAT_06;
                case 7:
                    return COORDORIGIN_LAT_07;
                case 8:
                    return COORDORIGIN_LAT_08;
                case 9:
                    return COORDORIGIN_LAT_09;
                case 10:
                    return COORDORIGIN_LAT_10;
                case 11:
                    return COORDORIGIN_LAT_11;
                case 12:
                    return COORDORIGIN_LAT_12;
                case 13:
                    return COORDORIGIN_LAT_13;
                case 14:
                    return COORDORIGIN_LAT_14;
                case 15:
                    return COORDORIGIN_LAT_15;
                case 16:
                    return COORDORIGIN_LAT_16;
                case 17:
                    return COORDORIGIN_LAT_17;
                case 18:
                    return COORDORIGIN_LAT_18;
                case 19:
                    return COORDORIGIN_LAT_19;
                default:
                    return 0;
            }
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '座標系原点経度。
        '
        '引き数：
        'nCoordNum 座標系番号(1～19)。
        '
        '戻り値：座標系原点経度(度)。
        Public Function COORDORIGIN_LON(ByVal nCoordNum As Long) As Double
            Select Case nCoordNum
            Case 1
                COORDORIGIN_LON = COORDORIGIN_LON_01
            Case 2
                COORDORIGIN_LON = COORDORIGIN_LON_02
            Case 3
                COORDORIGIN_LON = COORDORIGIN_LON_03
            Case 4
                COORDORIGIN_LON = COORDORIGIN_LON_04
            Case 5
                COORDORIGIN_LON = COORDORIGIN_LON_05
            Case 6
                COORDORIGIN_LON = COORDORIGIN_LON_06
            Case 7
                COORDORIGIN_LON = COORDORIGIN_LON_07
            Case 8
                COORDORIGIN_LON = COORDORIGIN_LON_08
            Case 9
                COORDORIGIN_LON = COORDORIGIN_LON_09
            Case 10
                COORDORIGIN_LON = COORDORIGIN_LON_10
            Case 11
                COORDORIGIN_LON = COORDORIGIN_LON_11
            Case 12
                COORDORIGIN_LON = COORDORIGIN_LON_12
            Case 13
                COORDORIGIN_LON = COORDORIGIN_LON_13
            Case 14
                COORDORIGIN_LON = COORDORIGIN_LON_14
            Case 15
                COORDORIGIN_LON = COORDORIGIN_LON_15
            Case 16
                COORDORIGIN_LON = COORDORIGIN_LON_16
            Case 17
                COORDORIGIN_LON = COORDORIGIN_LON_17
            Case 18
                COORDORIGIN_LON = COORDORIGIN_LON_18
            Case 19
                COORDORIGIN_LON = COORDORIGIN_LON_19
            Case Else
                COORDORIGIN_LON = 0
            End Select
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '座標系原点経度。
        '
        '引き数：
        'nCoordNum 座標系番号(1～19)。
        '
        '戻り値：座標系原点経度(度)。
        */
        public double COORDORIGIN_LON(long nCoordNum)
        {
            switch (nCoordNum)
            {
                case 1:
                    return COORDORIGIN_LON_01;
                case 2:
                    return COORDORIGIN_LON_02;
                case 3:
                    return COORDORIGIN_LON_03;
                case 4:
                    return COORDORIGIN_LON_04;
                case 5:
                    return COORDORIGIN_LON_05;
                case 6:
                    return COORDORIGIN_LON_06;
                case 7:
                    return COORDORIGIN_LON_07;
                case 8:
                    return COORDORIGIN_LON_08;
                case 9:
                    return COORDORIGIN_LON_09;
                case 10:
                    return COORDORIGIN_LON_10;
                case 11:
                    return COORDORIGIN_LON_11;
                case 12:
                    return COORDORIGIN_LON_12;
                case 13:
                    return COORDORIGIN_LON_13;
                case 14:
                    return COORDORIGIN_LON_14;
                case 15:
                    return COORDORIGIN_LON_15;
                case 16:
                    return COORDORIGIN_LON_16;
                case 17:
                    return COORDORIGIN_LON_17;
                case 18:
                    return COORDORIGIN_LON_18;
                case 19:
                    return COORDORIGIN_LON_19;
                default:
                    return 0;
            }
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'レジストリを開く。
        '
        'HKEY_CURRENT_USER\Software\NGS\アプリ名、を開く。
        '
        '戻り値：RegKey オブジェクト。
        Public Function OpenRegistry() As RegKey
            Dim clsRegSoftware As New RegKey
            Call clsRegSoftware.CreateKey(HKEY_CURRENT_USER, "Software")
            Dim clsRegNGS As New RegKey
            Call clsRegNGS.CreateKey(clsRegSoftware.Key, REG_KEY_ROOT)
            Dim clsRegNsNetwork As New RegKey
            Call clsRegNsNetwork.CreateKey(clsRegNGS.Key, App.Title)
            Set OpenRegistry = clsRegNsNetwork
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        'レジストリを開く。
        '
        'HKEY_CURRENT_USER\Software\NGS\アプリ名、を開く。
        '
        '戻り値：RegKey オブジェクト。
        */
        public RegKey OpenRegistry()
        {
            RegKey clsRegSoftware = new RegKey();
            clsRegSoftware.CreateKey(HKEY_CURRENT_USER, "Software");
            RegKey clsRegNGS = new RegKey();
            clsRegNGS.CreateKey(clsRegSoftware.Key(), REG_KEY_ROOT);
            RegKey clsRegNsNetwork = new RegKey();
            clsRegNsNetwork.CreateKey(clsRegNGS.Key(), AppTitle);
            return clsRegNsNetwork;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'ウィンドウサイズを保存する。
        '
        'レジストリにウィンドウサイズを書き込む。
        '
        '引き数：
        'clsRegNsNetwork RegKey オブジェクト。
        'sWindow キーの名称(接頭文字)。
        'objForm サイズを保存するウィンドウ。
        Public Sub SaveWindowPos(ByVal clsRegNsNetwork As RegKey, ByVal sWindow As String, ByVal objForm As Object)
            If objForm.WindowState = vbNormal Then
                Call clsRegNsNetwork.SetInteger(sWindow & REG_KEY_WINLEFT, objForm.Left / Screen.TwipsPerPixelX)
                Call clsRegNsNetwork.SetInteger(sWindow & REG_KEY_WINTOP, objForm.Top / Screen.TwipsPerPixelY)
                Call clsRegNsNetwork.SetInteger(sWindow & REG_KEY_WINWIDTH, objForm.Width / Screen.TwipsPerPixelX)
                Call clsRegNsNetwork.SetInteger(sWindow & REG_KEY_WINHEIGHT, objForm.Height / Screen.TwipsPerPixelY)
            End If
            Call clsRegNsNetwork.SetInteger(sWindow & REG_KEY_WINSTATE, objForm.WindowState)
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        'ウィンドウサイズを保存する。
        '
        'レジストリにウィンドウサイズを書き込む。
        '
        '引き数：
        'clsRegNsNetwork RegKey オブジェクト。
        'sWindow キーの名称(接頭文字)。
        'objForm サイズを保存するウィンドウ。
        */
        public void SaveWindowPos(RegKey clsRegNsNetwork, string sWindow, object objForm)
        {
#if false
//要修正 sakai
            if (objForm.WindowState == vbNormal)
            {
                clsRegNsNetwork.SetInteger(sWindow + REG_KEY_WINLEFT, objForm.Left / Screen.TwipsPerPixelX);
                clsRegNsNetwork.SetInteger(sWindow + REG_KEY_WINTOP, objForm.Top / Screen.TwipsPerPixelY);
                clsRegNsNetwork.SetInteger(sWindow + REG_KEY_WINWIDTH, objForm.Width / Screen.TwipsPerPixelX);
                clsRegNsNetwork.SetInteger(sWindow + REG_KEY_WINHEIGHT, objForm.Height / Screen.TwipsPerPixelY);
            }
            clsRegNsNetwork.SetInteger(sWindow & REG_KEY_WINSTATE, objForm.WindowState);
#endif
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'ウィンドウサイズを読み込む。
        '
        'レジストリからウィンドウサイズを読み込みサイズを設定する。
        '
        '引き数：
        'clsRegNsNetwork RegKey オブジェクト。
        'sWindow キーの名称(接頭文字)。
        'objForm サイズを設定するウィンドウ。
        'nMinWidth 最小幅(Twips)。
        'nMinHeight 最小高さ(Twips)。
        Public Sub LoadWindowPos(ByVal clsRegNsNetwork As RegKey, ByVal sWindow As String, ByVal objForm As Object, Optional ByVal nMinWidth As Long = 0, Optional ByVal nMinHeight As Long = 0)
            Dim nWidth As Long
            Dim nHeight As Long
            nWidth = clsRegNsNetwork.QueryValue(sWindow & REG_KEY_WINWIDTH, 800) * Screen.TwipsPerPixelX
            nHeight = clsRegNsNetwork.QueryValue(sWindow & REG_KEY_WINHEIGHT, 600) * Screen.TwipsPerPixelY
            If nWidth<nMinWidth Then nWidth = nMinWidth
            If nHeight<nMinHeight Then nHeight = nMinHeight
            objForm.Width = nWidth
            objForm.Height = nHeight
            objForm.Left = clsRegNsNetwork.QueryValue(sWindow & REG_KEY_WINLEFT, (Screen.Width - objForm.Width) * 0.5 / Screen.TwipsPerPixelX) * Screen.TwipsPerPixelX
            objForm.Top = clsRegNsNetwork.QueryValue(sWindow & REG_KEY_WINTOP, (Screen.Height - objForm.Height) * 0.5 / Screen.TwipsPerPixelY) * Screen.TwipsPerPixelY
            Dim nWindowState As Long
            nWindowState = clsRegNsNetwork.QueryValue(sWindow & REG_KEY_WINSTATE, vbNormal)
            If nWindowState = vbMinimized Then nWindowState = vbNormal
            objForm.WindowState = nWindowState
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        'ウィンドウサイズを読み込む。
        '
        'レジストリからウィンドウサイズを読み込みサイズを設定する。
        '
        '引き数：
        'clsRegNsNetwork RegKey オブジェクト。
        'sWindow キーの名称(接頭文字)。
        'objForm サイズを設定するウィンドウ。
        'nMinWidth 最小幅(Twips)。
        'nMinHeight 最小高さ(Twips)。
        */
        public void LoadWindowPos(RegKey clsRegNsNetwork, string sWindow, object objForm, long nMinWidth = 0, long nMinHeight = 0)
        {
#if false
//要修正 sakai
            long nWidth;
            long nHeight;
            nWidth = clsRegNsNetwork.QueryValue(sWindow & REG_KEY_WINWIDTH, 800) * Screen.TwipsPerPixelX;
            nHeight = clsRegNsNetwork.QueryValue(sWindow & REG_KEY_WINHEIGHT, 600) * Screen.TwipsPerPixelY;
            if (nWidth < nMinWidth)
            {
                nWidth = nMinWidth;
            }
            if (nHeight < nMinHeight)
            {
                nHeight = nMinHeight;
            }
            objForm.Width = nWidth;
            objForm.Height = nHeight;
            objForm.Left = clsRegNsNetwork.QueryValue(sWindow & REG_KEY_WINLEFT, (Screen.Width - objForm.Width) * 0.5 / Screen.TwipsPerPixelX) * Screen.TwipsPerPixelX;
            objForm.Top = clsRegNsNetwork.QueryValue(sWindow & REG_KEY_WINTOP, (Screen.Height - objForm.Height) * 0.5 / Screen.TwipsPerPixelY) * Screen.TwipsPerPixelY;
            long nWindowState;
            nWindowState = clsRegNsNetwork.QueryValue(sWindow & REG_KEY_WINSTATE, vbNormal);
            if (nWindowState == vbMinimized)
            {
                nWindowState = vbNormal;
            }
            objForm.WindowState = nWindowState;
#endif
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '重複キーを取得する。
        '
        '基線ベクトルの重複を評価するためのキーを取得する。
        'キーが一致する基線ベクトルは重複するとみなせる。
        '
        '引き数：
        'clsBaseLineVector 基線ベクトル。
        '
        '戻り値：重複キー。
        Public Function GetDuplicationKey(ByVal clsBaseLineVector As BaseLineVector) As String
            Dim nStrPoint As Long
            Dim nEndPoint As Long
            Dim sNumber0 As String * 8
            Dim sNumber1 As String * 8
            nStrPoint = GetPointer(clsBaseLineVector.StrPoint.RootPoint)
            nEndPoint = GetPointer(clsBaseLineVector.EndPoint.RootPoint)
            If nStrPoint<nEndPoint Then
                sNumber0 = Hex$(nStrPoint)
                sNumber1 = Hex$(nEndPoint)
            Else
                sNumber0 = Hex$(nEndPoint)
                sNumber1 = Hex$(nStrPoint)
            End If
            GetDuplicationKey = sNumber0 & sNumber1
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '重複キーを取得する。
        '
        '基線ベクトルの重複を評価するためのキーを取得する。
        'キーが一致する基線ベクトルは重複するとみなせる。
        '
        '引き数：
        'clsBaseLineVector 基線ベクトル。
        '
        '戻り値：重複キー。
        */
        public static string GetDuplicationKey(BaseLineVector clsBaseLineVector)
        {
#if false
            long nStrPoint;
            long nEndPoint;
            string[] sNumber0;
            string[] sNumber1;

            nStrPoint = GetPointer(clsBaseLineVector.StrPoint().RootPoint());
            nEndPoint = GetPointer(clsBaseLineVector.EndPoint().RootPoint());
            sNumber0 = new string[1];
            sNumber1 = new string[1];
            if (nStrPoint < nEndPoint)
            {
                sNumber0[0] = Convert.ToString(nStrPoint);
                sNumber1[0] = Convert.ToString(nEndPoint);
            }
            else
            {
                sNumber0[0] = Convert.ToString(nEndPoint);
                sNumber1[0] = Convert.ToString(nStrPoint);
            }
#if true
            return sNumber0[0] + " " + sNumber1[0];
#endif
            return sNumber0[0];
#else
            long nStrPoint;
            long nEndPoint;
            string[] sNumber0;
            string[] sNumber1;

            nStrPoint = ID;
            ID++;
            nEndPoint = ID;
            ID++;
            sNumber0 = new string[1];
            sNumber1 = new string[1];
            if (nStrPoint < nEndPoint)
            {
                sNumber0[0] = nStrPoint.ToString("x8");
                sNumber1[0] = nEndPoint.ToString("x8");
            }
            else
            {
                sNumber0[0] = nEndPoint.ToString("x8");
                sNumber1[0] = nStrPoint.ToString("x8");
            }
            return sNumber0[0] + " " + sNumber1[0];
#endif
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '度に対する表示文字列を取得する。
        '
        'H°MM'SS".00000 に書式化する。
        '
        '引き数：
        'nD 角度(度)。
        '
        '戻り値：表示文字列。
        'Public Function GetDMSString(ByVal nD As Double) As String
        '    Dim nH As Long
        '    Dim nM As Long
        '    Dim nS As Double
        '    Call d_to_dms_decimal(Abs(nD), nH, nM, nS, 5)
        '    Dim sS As String
        '    sS = Format$(JpnRound(nS, 5), "00.00000")
        '    GetDMSString = IIf(nD < 0, "-", "") & CStr(nH) & "°" & Format$(nM, "00'") & Left$(sS, 2) & """" & Mid$(sS, 3)
        'End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '緯度経度表示文字列を取得する。
        '
        'H°MM'SS".00000 に書式化する。
        '
        '引き数：
        'clsCoordinatePoint 座標。
        'sLat 緯度表示文字列が設定される。
        'sLon 経度表示文字列が設定される。
        Public Sub GetDMSString(ByVal clsCoordinatePoint As CoordinatePoint, ByRef sLat As String, ByRef sLon As String)
            Dim nLatH As Long
            Dim nLatM As Long
            Dim nLatS As Double
            Dim nLonH As Long
            Dim nLonM As Long
            Dim nLonS As Double
            Dim nHeight As Double
            Dim vAlt As Variant
            Call clsCoordinatePoint.GetDMS(nLatH, nLatM, nLatS, nLonH, nLonM, nLonS, nHeight, vAlt, 5, "")
            Dim sS As String
            sS = Format$(Abs(nLatS), "00.00000")
            sLat = CStr(nLatH) & "°" & Format$(Abs(nLatM), "00'") & Left$(sS, 2) & """" & Mid$(sS, 3)
            sS = Format$(Abs(nLonS), "00.00000")
            sLon = CStr(nLonH) & "°" & Format$(Abs(nLonM), "00'") & Left$(sS, 2) & """" & Mid$(sS, 3)
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '緯度経度表示文字列を取得する。
        '
        'H°MM'SS".00000 に書式化する。
        '
        '引き数：
        'clsCoordinatePoint 座標。
        'sLat 緯度表示文字列が設定される。
        'sLon 経度表示文字列が設定される。
        */
        public void GetDMSString(CoordinatePoint clsCoordinatePoint, ref string sLat, ref string sLon)
        {
            long nLatH = 0;
            long nLatM = 0;
            double nLatS = 0;
            long nLonH = 0;
            long nLonM = 0;
            double nLonS = 0;
            double nHeight = 0;
            double vAlt = 0;
            clsCoordinatePoint.GetDMS(ref nLatH, ref nLatM, ref nLatS, ref nLonH, ref nLonM, ref nLonS, ref nHeight, ref vAlt, 5, "");
            string sS;
            sS = string.Format("00.00000", Math.Abs(nLatS));
            sLat = nLatH.ToString() + "°" + string.Format("00'", Math.Abs(nLatM)) + Left(sS, 2) + (char)34 + Mid(sS, 3, 99);    //(char)34 = "
            sS = string.Format("00.00000", Math.Abs(nLonS));
            sLon = nLonH.ToString() + "°" + string.Format("00'", Math.Abs(nLonM)) + Left(sS, 2) + (char)34 + Mid(sS, 3, 99);    //(char)34 = "
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '方位角。
        '
        '地心直交座標から方位角を求める。
        '
        '引き数：
        'nCoordNum 座標系番号(1～19)。
        'nX1 始点地心直交X。
        'nY1 始点地心直交Y。
        'nZ1 始点地心直交Z。
        'nX2 終点地心直交X。
        'nY2 終点地心直交Y。
        'nZ2 終点地心直交Z。
        '
        '戻り値：方位角(ラジアン)。
        Public Function SP_α_Ex(ByVal nCoordNum As Long, ByVal nX1 As Double, ByVal nY1 As Double, ByVal nZ1 As Double, ByVal nX2 As Double, ByVal nY2 As Double, ByVal nZ2 As Double) As Double
            Dim nSX As Double
            Dim nSY As Double
            Dim nEX As Double
            Dim nEY As Double
            Dim nZ As Double
            Call WGS84xyz_to_JGDxyz(nCoordNum, nX1, nY1, nZ1, nSX, nSY, nZ)
            Call WGS84xyz_to_JGDxyz(nCoordNum, nX2, nY2, nZ2, nEX, nEY, nZ)
            Dim nLat As Double
            Dim nLon As Double
            Dim nHeight As Double
            Call WGS84xyz_to_WGS84dms(nX1, nY1, nZ1, nLat, nLon, nHeight)
            SP_α_Ex = SP_α(nSX, nSY, nEX, nEY, nLat / 180# * PAI, nLon / 180# * PAI, COORDORIGIN_LAT(nCoordNum) / 180# * PAI, COORDORIGIN_LON(nCoordNum) / 180# * PAI)
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '方位角。
        '
        '地心直交座標から方位角を求める。
        '
        '引き数：
        'nCoordNum 座標系番号(1～19)。
        'nX1 始点地心直交X。
        'nY1 始点地心直交Y。
        'nZ1 始点地心直交Z。
        'nX2 終点地心直交X。
        'nY2 終点地心直交Y。
        'nZ2 終点地心直交Z。
        '
        '戻り値：方位角(ラジアン)。
        */
        public double SP_α_Ex(long nCoordNum, double nX1, double nY1, double nZ1, double nX2, double nY2, double nZ2)
        {
            double nSX = 0;
            double nSY = 0;
            double nEX = 0;
            double nEY = 0;
            double nZ = 0;
            WGS84xyz_to_JGDxyz(nCoordNum, nX1, nY1, nZ1, ref nSX, ref nSY, ref nZ);
            WGS84xyz_to_JGDxyz(nCoordNum, nX2, nY2, nZ2, ref nEX, ref nEY, ref nZ);
            double nLat = 0;
            double nLon = 0;
            double nHeight = 0;
            WGS84xyz_to_WGS84dms(nX1, nY1, nZ1, ref nLat, ref nLon, ref nHeight);
            return SP_α(nSX, nSY, nEX, nEY, nLat / 180 * PAI, nLon / 180 * PAI, COORDORIGIN_LAT(nCoordNum) / 180 * PAI, COORDORIGIN_LON(nCoordNum) / 180 * PAI);
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'GPS時間から日時を取得する。
        '
        '引き数：
        'tDate GPS時間。
        'nLeapSeconds うるう秒。
        'nZone 取得するタイムゾーンの指定。TIME_ZONE_UTC or TIME_ZONE_JST。
        '
        '戻り値：日時。
        Public Function GetTimeFromGPS(ByVal tDate As Date, ByVal nLeapSeconds As Long, ByVal nZone As TIME_ZONE) As Date
            If nZone = TIME_ZONE_UTC Then
                GetTimeFromGPS = tDate
            Else
                GetTimeFromGPS = DateAdd("s", 60 * 60 * 9, tDate)
            End If
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        'GPS時間から日時を取得する。
        '
        '引き数：
        'tDate GPS時間。
        'nLeapSeconds うるう秒。
        'nZone 取得するタイムゾーンの指定。TIME_ZONE_UTC or TIME_ZONE_JST。
        '
        '戻り値：日時。
        */
        public static DateTime GetTimeFromGPS(DateTime tDate, long nLeapSeconds, TIME_ZONE nZone)
        {
            if (nZone == TIME_ZONE.TIME_ZONE_UTC)
            {
                return tDate;
            }
            else
            {
                //return DateAdd("s", 60 * 60 * 9, tDate);
                return tDate.AddHours(9);   //tDate + 9h
            }
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '日時からGPS時間を取得する。
        '
        '引き数：
        'tDate 日時。
        'nLeapSeconds うるう秒。
        'nZone tDate のタイムゾーン。TIME_ZONE_UTC or TIME_ZONE_JST。
        '
        '戻り値：GPS時間。
        Public Function GetTimeToGPS(ByVal tDate As Date, ByVal nLeapSeconds As Long, ByVal nZone As TIME_ZONE) As Date
            If nZone = TIME_ZONE_UTC Then
                GetTimeToGPS = tDate
            Else
                GetTimeToGPS = DateAdd("s", -60 * 60 * 9, tDate)
            End If
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '日時からGPS時間を取得する。
        '
        '引き数：
        'tDate 日時。
        'nLeapSeconds うるう秒。
        'nZone tDate のタイムゾーン。TIME_ZONE_UTC or TIME_ZONE_JST。
        '
        '戻り値：GPS時間。
        */
        public DateTime GetTimeToGPS(DateTime tDate, long nLeapSeconds, TIME_ZONE nZone)
        {
            if (nZone == TIME_ZONE.TIME_ZONE_UTC)
            {
                return tDate;
            }
            else
            {
                //return DateAdd("s", -60 * 60 * 9, tDate);
                return tDate.AddHours(-9);   //tDate - 9h
            }
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '受信機表示名称。
        '
        '受信機名(メーカー名)、の書式で取得できる。
        '日本語。
        '
        '引き数：
        'sRecType 受信機種別。
        '
        '戻り値：受信機表示名称。
        Public Function GetRecTypeDispJ(ByVal sRecType As String) As String
            Dim sName As String
            Dim sManufacture As String
            If sRecType = "" Then
                sName = ""
                sManufacture = ""
            Else
                sName = GetPrivateProfileString(sRecType, PROFILE_RCV_KEY_TYPE, "", App.Path & "\" & PROFILE_RCV_FILE)
                sManufacture = GetManufacturerDispJ(GetPrivateProfileString(sRecType, PROFILE_RCV_KEY_MANUFACTURE, "", App.Path & "\" & PROFILE_RCV_FILE))
            End If
            If sManufacture<> "" Then sName = sName & "(" & sManufacture & ")"
            If sName<> "" Then
                GetRecTypeDispJ = sName
            Else
                GetRecTypeDispJ = sRecType
            End If
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '受信機表示名称。
        '
        '受信機名(メーカー名)、の書式で取得できる。
        '日本語。
        '
        '引き数：
        'sRecType 受信機種別。
        '
        '戻り値：受信機表示名称。
        */
        public static string GetRecTypeDispJ(string sRecType)
        {
            string sName;
            string sManufacture;
            if (sRecType == "")
            {
                sName = "";
                sManufacture = "";
            }
            else
            {
                sName = GetPrivateProfileString(sRecType, PROFILE_RCV_KEY_TYPE, "", AppPath + "\\" + PROFILE_RCV_FILE);
                sManufacture = GetManufacturerDispJ(GetPrivateProfileString(sRecType, PROFILE_RCV_KEY_MANUFACTURE, "", AppPath + "\\" + PROFILE_RCV_FILE));
            }
            if (sManufacture != "")
            {
                sName = sName + "(" + sManufacture + ")";
            }
            if (sName != "")
            {
                return sName;
            }
            else
            {
                return sRecType;
            }
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '受信機表示名称。
        '
        '受信機名(メーカー名)、の書式で取得できる。
        '英語。
        '
        '引き数：
        'sRecType 受信機種別。
        '
        '戻り値：受信機表示名称。
        Public Function GetRecTypeDispE(ByVal sRecType As String) As String
            Dim sName As String
            Dim sManufacture As String
            If sRecType = "" Then
                sName = ""
                sManufacture = ""
            Else
                sName = GetPrivateProfileString(sRecType, PROFILE_RCV_KEY_TYPE, "", App.Path & "\" & PROFILE_RCV_FILE)
                sManufacture = GetManufacturerDispE(GetPrivateProfileString(sRecType, PROFILE_RCV_KEY_MANUFACTURE, "", App.Path & "\" & PROFILE_RCV_FILE))
            End If
            If sManufacture<> "" Then sName = sName & "(" & sManufacture & ")"
            If sName<> "" Then
                GetRecTypeDispE = sName
            Else
                GetRecTypeDispE = sRecType
            End If
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '受信機表示名称。
        '
        '受信機名(メーカー名)、の書式で取得できる。
        '英語。
        '
        '引き数：
        'sRecType 受信機種別。
        '
        '戻り値：受信機表示名称。
        */
        public static string GetRecTypeDispE(string sRecType)
        {
            string sName;
            string sManufacture;
            if (sRecType == "")
            {
                sName = "";
                sManufacture = "";
            }
            else
            {
                sName = GetPrivateProfileString(sRecType, PROFILE_RCV_KEY_TYPE, "", AppPath + "\\" + PROFILE_RCV_FILE);
                sManufacture = GetManufacturerDispE(GetPrivateProfileString(sRecType, PROFILE_RCV_KEY_MANUFACTURE, "", AppPath + "\\" + PROFILE_RCV_FILE));
            }
            if (sManufacture != "")
            {
                sName = sName + "(" + sManufacture + ")";
            }
            if (sName != "")
            {
                return sName;
            }
            else
            {
                return sRecType;
            }
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'アンテナ表示名称。
        '
        'アンテナ名(メーカー名)、の書式で取得できる。
        '日本語。
        '
        '引き数：
        'sAntType アンテナ種別。
        '
        '戻り値：アンテナ表示名称。
        Public Function GetAntTypeDispJ(ByVal sAntType As String) As String
            Dim sName As String
            Dim sManufacture As String
            If sAntType = "" Then
                sName = ""
                sManufacture = ""
            Else
                sName = GetPrivateProfileString(sAntType, PROFILE_ANT_KEY_TYPE, "", App.Path & "\" & PROFILE_ANT_FILE)
                sManufacture = GetManufacturerDispJ(GetPrivateProfileString(sAntType, PROFILE_ANT_KEY_MANUFACTURE, "", App.Path & "\" & PROFILE_ANT_FILE))
            End If
            If sManufacture<> "" Then sName = sName & "(" & sManufacture & ")"
            If sName<> "" Then
                GetAntTypeDispJ = sName
            Else
                GetAntTypeDispJ = sAntType
            End If
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        'アンテナ表示名称。
        '
        'アンテナ名(メーカー名)、の書式で取得できる。
        '日本語。
        '
        '引き数：
        'sAntType アンテナ種別。
        '
        '戻り値：アンテナ表示名称。
        */
        public static string GetAntTypeDispJ(string sAntType)
        {
            string sName;
            string sManufacture;
            if (sAntType == "")
            {
                sName = "";
                sManufacture = "";
            }
            else
            {
                sName = GetPrivateProfileString(sAntType, PROFILE_ANT_KEY_TYPE, "", AppPath + "\\" + PROFILE_ANT_FILE);
                sManufacture = GetManufacturerDispJ(GetPrivateProfileString(sAntType, PROFILE_ANT_KEY_MANUFACTURE, "", AppPath + "\\" + PROFILE_ANT_FILE));
            }
            if (sManufacture != "")
            {
                sName = sName + "(" + sManufacture + ")";
            }
            if (sName != "")
            {
                return sName;
            }
            else
            {
                return sAntType;
            }
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'アンテナ表示名称。
        '
        'アンテナ名(メーカー名)、の書式で取得できる。
        '英語。
        '
        '引き数：
        'sAntType アンテナ種別。
        '
        '戻り値：アンテナ表示名称。
        Public Function GetAntTypeDispE(ByVal sAntType As String) As String
            Dim sName As String
            Dim sManufacture As String
            If sAntType = "" Then
                sName = ""
                sManufacture = ""
            Else
                sName = GetPrivateProfileString(sAntType, PROFILE_ANT_KEY_TYPE, "", App.Path & "\" & PROFILE_ANT_FILE)
                sManufacture = GetManufacturerDispE(GetPrivateProfileString(sAntType, PROFILE_ANT_KEY_MANUFACTURE, "", App.Path & "\" & PROFILE_ANT_FILE))
            End If
            If sManufacture<> "" Then sName = sName & "(" & sManufacture & ")"
            If sName<> "" Then
                GetAntTypeDispE = sName
            Else
                GetAntTypeDispE = sAntType
            End If
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        'アンテナ表示名称。
        '
        'アンテナ名(メーカー名)、の書式で取得できる。
        '英語。
        '
        '引き数：
        'sAntType アンテナ種別。
        '
        '戻り値：アンテナ表示名称。
        */
        public string GetAntTypeDispE(string sAntType)
        {
            string sName;
            string sManufacture;
            if (sAntType == "")
            {
                sName = "";
                sManufacture = "";
            }
            else
            {
                sName = GetPrivateProfileString(sAntType, PROFILE_ANT_KEY_TYPE, "", AppPath + "\\" + PROFILE_ANT_FILE);
                sManufacture = GetManufacturerDispE(GetPrivateProfileString(sAntType, PROFILE_ANT_KEY_MANUFACTURE, "", AppPath + "\\" + PROFILE_ANT_FILE));
            }
            if (sManufacture != "")
            {
                sName = sName + "(" + sManufacture + ")";
            }
            if (sName != "")
            {
                return sName;
            }
            else
            {
                return sAntType;
            }
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '日本語メーカー名を取得する。
        '
        '引き数：
        'sManufacturer メーカーコード。
        '
        '戻り値：メーカー名。
        Public Function GetManufacturerDispJ(ByVal sManufacturer As String) As String
            Dim clsStringTokenizer As New StringTokenizer
            clsStringTokenizer.Source = GetPrivateProfileString(PROFILE_SUB_SEC_MANUCODETOMANUNAME, sManufacturer, "", App.Path & "\" & PROFILE_SUB_FILE)
            Call clsStringTokenizer.Begin
            GetManufacturerDispJ = clsStringTokenizer.NextToken
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '日本語メーカー名を取得する。
        '
        '引き数：
        'sManufacturer メーカーコード。
        '
        '戻り値：メーカー名。
        */
        public static string GetManufacturerDispJ(string sManufacturer)
        {
            StringTokenizer clsStringTokenizer = new StringTokenizer();
            _ = clsStringTokenizer.Source = GetPrivateProfileString(PROFILE_SUB_SEC_MANUCODETOMANUNAME, sManufacturer, "", AppPath + "\\" + PROFILE_SUB_FILE);
            clsStringTokenizer.Begin();
            return clsStringTokenizer.NextToken();
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '英語メーカー名を取得する。
        '
        '引き数：
        'sManufacturer メーカーコード。
        '
        '戻り値：メーカー名。
        Public Function GetManufacturerDispE(ByVal sManufacturer As String) As String
            Dim clsStringTokenizer As New StringTokenizer
            clsStringTokenizer.Source = GetPrivateProfileString(PROFILE_SUB_SEC_MANUCODETOMANUNAME, sManufacturer, "", App.Path & "\" & PROFILE_SUB_FILE)
            Call clsStringTokenizer.Begin
            Call clsStringTokenizer.NextToken
            GetManufacturerDispE = clsStringTokenizer.NextToken
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '英語メーカー名を取得する。
        '
        '引き数：
        'sManufacturer メーカーコード。
        '
        '戻り値：メーカー名。
        */
        public static string GetManufacturerDispE(string sManufacturer)
        {
            StringTokenizer clsStringTokenizer = new StringTokenizer();
            _ = clsStringTokenizer.Source = GetPrivateProfileString(PROFILE_SUB_SEC_MANUCODETOMANUNAME, sManufacturer, "", AppPath + "\\" + PROFILE_SUB_FILE);
            clsStringTokenizer.Begin();
            _ = clsStringTokenizer.NextToken();
            return clsStringTokenizer.NextToken();
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'メーカー並び順番号を取得する。
        '
        '引き数：
        'sManufacturer メーカーコード。
        '
        '戻り値：並び順番号。
        Public Function GetManufacturerSortIndex(ByVal sManufacturer As String) As Long
            Dim clsStringTokenizer As New StringTokenizer
            clsStringTokenizer.Source = GetPrivateProfileString(PROFILE_SUB_SEC_MANUCODETOMANUNAME, sManufacturer, "", App.Path & "\" & PROFILE_SUB_FILE)
            If clsStringTokenizer.Source = "" Then
                GetManufacturerSortIndex = LONG_MAX
            Else
                Call clsStringTokenizer.Begin
                Call clsStringTokenizer.NextToken
                Call clsStringTokenizer.NextToken
                GetManufacturerSortIndex = Val(clsStringTokenizer.NextToken)
            End If
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        'メーカー並び順番号を取得する。
        '
        '引き数：
        'sManufacturer メーカーコード。
        '
        '戻り値：並び順番号。
        */
        public long GetManufacturerSortIndex(string sManufacturer)
        {
            StringTokenizer clsStringTokenizer = new StringTokenizer();
            _ = clsStringTokenizer.Source = GetPrivateProfileString(PROFILE_SUB_SEC_MANUCODETOMANUNAME, sManufacturer, "", AppPath + "\\" + PROFILE_SUB_FILE);
            if (clsStringTokenizer.Source == "")
            {
                return LONG_MAX;
            }
            else
            {
                clsStringTokenizer.Begin();
                _ = clsStringTokenizer.NextToken();
                _ = clsStringTokenizer.NextToken();
                return Convert.ToInt32(clsStringTokenizer.NextToken());
            }
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '測位方法表示名称から測位方法を取得する。
        '
        '引き数：
        'sAntType アンテナ種別。
        'sDispMeasurement 測位方法表示名称。
        '
        '戻り値：測位方法。
        Public Function GetAntMeasurementFromDisp(ByVal sAntType As String, ByVal sDispMeasurement As String) As String
            If sAntType<> "" Then
                sDispMeasurement = """" & sDispMeasurement & """"
                Dim nIndex As Long
                nIndex = 0
                Do
                    Dim clsStringTokenizer As New StringTokenizer
                    clsStringTokenizer.Source = GetPrivateProfileString(sAntType, PROFILE_ANT_KEY_MEASUREMENT & CStr(nIndex), "", App.Path & "\" & PROFILE_ANT_FILE)
                    Call clsStringTokenizer.Begin
                    Dim sToken As String
                    sToken = clsStringTokenizer.NextToken
                    If sToken = "" Then Exit Do
                    If sToken = sDispMeasurement Then
                        GetAntMeasurementFromDisp = clsStringTokenizer.NextToken
                        Exit Do
                    End If
                    nIndex = nIndex + 1
                Loop
            End If
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '測位方法表示名称から測位方法を取得する。
        '
        '引き数：
        'sAntType アンテナ種別。
        'sDispMeasurement 測位方法表示名称。
        '
        '戻り値：測位方法。
        */
        public string GetAntMeasurementFromDisp(string sAntType, string sDispMeasurement)
        {
            if (sAntType != "")
            {
                sDispMeasurement = (char)34 + sDispMeasurement + (char)34;      //(char)34 = "
                long nIndex;
                nIndex = 0;
                do
                {
                    StringTokenizer clsStringTokenizer = new StringTokenizer();
                    _ = clsStringTokenizer.Source = GetPrivateProfileString(sAntType, PROFILE_ANT_KEY_MEASUREMENT + nIndex.ToString(), "", AppPath + "\\" + PROFILE_ANT_FILE);
                    clsStringTokenizer.Begin();
                    string sToken;
                    sToken = clsStringTokenizer.NextToken();
                    if (sToken == "")
                    {
                        break;
                    }
                    if (sToken == sDispMeasurement)
                    {
                        return clsStringTokenizer.NextToken();
                    }
                    nIndex = nIndex + 1;
                } while (true);
            }
            return "";
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '指定された緯度経度座標の範囲を検査する。
        '
        '引き数：
        'nLat 緯度(度)。
        'nLon 経度(度)。
        'nHeight 楕円体高(ｍ、標高でもさほど問題でない)。
        '
        '戻り値：
        '座標が正常である場合 True を返す。
        'それ以外の場合 False を返す。
        Public Function CheckCoordDMS(ByVal nLat As Double, ByVal nLon As Double, ByVal nHeight As Double) As Boolean
            CheckCoordDMS = False
            On Error GoTo ErrorHandler
            If nLat < COORD_MIN_LAT Then Exit Function
            If COORD_MAX_LAT <= nLat Then Exit Function
            If nLon < COORD_MIN_LON Then Exit Function
            If COORD_MAX_LON <= nLon Then Exit Function
            If nHeight < COORD_MIN_HEIGHT Then Exit Function
            If COORD_MAX_HEIGHT <= nHeight Then Exit Function
            CheckCoordDMS = True
            Exit Function
        ErrorHandler:
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '指定された緯度経度座標の範囲を検査する。
        '
        '引き数：
        'nLat 緯度(度)。
        'nLon 経度(度)。
        'nHeight 楕円体高(ｍ、標高でもさほど問題でない)。
        '
        '戻り値：
        '座標が正常である場合 True を返す。
        'それ以外の場合 False を返す。
        */
        public static bool CheckCoordDMS(double nLat, double nLon, double nHeight)
        {
            try
            {
                if (nLat < COORD_MIN_LAT){ return false; }
                if (COORD_MAX_LAT <= nLat){ return false; }
                if (nLon < COORD_MIN_LON){ return false; }
                if (COORD_MAX_LON <= nLon){ return false; }
                if (nHeight < COORD_MIN_HEIGHT){ return false; }
                if (COORD_MAX_HEIGHT <= nHeight){ return false; }
                return true;
            }

            catch
            {
                return false;
            }
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '指定された地心直交座標の範囲を検査する。
        '
        '引き数：
        'nX X(ｍ)。
        'nY Y(ｍ)。
        'nZ Z(ｍ)。
        '
        '戻り値：
        '座標が正常である場合 True を返す。
        'それ以外の場合 False を返す。
        Public Function CheckCoordXYZ(ByVal nX As Double, ByVal nY As Double, ByVal nZ As Double) As Boolean
            CheckCoordXYZ = False
            Dim nLat As Double
            Dim nLon As Double
            Dim nHeight As Double
            Call WGS84xyz_to_WGS84dms(nX, nY, nZ, nLat, nLon, nHeight)
            CheckCoordXYZ = CheckCoordDMS(nLat, nLon, nHeight)
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '指定された地心直交座標の範囲を検査する。
        '
        '引き数：
        'nX X(ｍ)。
        'nY Y(ｍ)。
        'nZ Z(ｍ)。
        '
        '戻り値：
        '座標が正常である場合 True を返す。
        'それ以外の場合 False を返す。
        */
        //public bool CheckCoordXYZ(double nX, double nY, double nZ)
        public static bool CheckCoordXYZ(double nX, double nY, double nZ)
        {
            double nLat = 0;
            double nLon = 0;
            double nHeight = 0;
            WGS84xyz_to_WGS84dms(nX, nY, nZ, ref nLat, ref nLon, ref nHeight);
            return CheckCoordDMS(nLat, nLon, nHeight);
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '指定された平面直角座標の範囲を検査する。
        '
        '引き数：
        'nX X(ｍ)。
        'nY Y(ｍ)。
        'nH 楕円体高(ｍ、標高でもさほど問題でない)。
        'nCoordNum 座標系番号(1～19)。
        '
        '戻り値：
        '座標が正常である場合 True を返す。
        'それ以外の場合 False を返す。
        Public Function CheckCoordJGD(ByVal nX As Double, ByVal nY As Double, ByVal nH As Double, ByVal nCoordNum As Long) As Boolean
            CheckCoordJGD = False
            Dim nLat As Double
            Dim nLon As Double
            Dim nHeight As Double
            Call JGDxyz_to_WGS84dms(nCoordNum, nX, nY, nH, nLat, nLon, nHeight)
            CheckCoordJGD = CheckCoordDMS(nLat, nLon, nHeight)
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '指定された平面直角座標の範囲を検査する。
        '
        '引き数：
        'nX X(ｍ)。
        'nY Y(ｍ)。
        'nH 楕円体高(ｍ、標高でもさほど問題でない)。
        'nCoordNum 座標系番号(1～19)。
        '
        '戻り値：
        '座標が正常である場合 True を返す。
        'それ以外の場合 False を返す。
        */
        public bool CheckCoordJGD(double nX, double nY, double nH, double nCoordNum)
        {
            double nLat = 0;
            double nLon = 0;
            double nHeight = 0;
            JGDxyz_to_WGS84dms((int)nCoordNum, nX, nY, nH, ref nLat, ref nLon, ref nHeight);
            return CheckCoordDMS(nLat, nLon, nHeight);
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'アプリケーションセキュリティチェック。
        '
        'アプリケーションのライセンスを確認する。
        '
        '引き数：
        'clsSproObject SproObject
        'clsSproData SuperPro データ。
        '
        '戻り値：
        'アプリケーションのライセンスが有効である場合 True を返す。
        'それ以外の場合 False を返す。
        Public Function CheckSecurityApp(ByVal clsSproObject As SproObject, ByVal clsSproData As SproDataInterface) As Boolean
            CheckSecurityApp = False
            On Error GoTo ErrorHandler
            Dim bCheck As Boolean
            Do
                bCheck = clsSproObject.Check(clsSproData)
                If bCheck Then Exit Do
                If MsgBox("ハードウェアキーの認証に失敗しました。" & vbCrLf & "ハードウェアキーを確認してください。", vbCritical Or vbRetryCancel) <> vbRetry Then Exit Function
            Loop
            CheckSecurityApp = True
            Exit Function
        ErrorHandler:
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        'アプリケーションセキュリティチェック。
        '
        'アプリケーションのライセンスを確認する。
        '
        '引き数：
        'clsSproObject SproObject
        'clsSproData SuperPro データ。
        '
        '戻り値：
        'アプリケーションのライセンスが有効である場合 True を返す。
        'それ以外の場合 False を返す。
        */
        public bool CheckSecurityApp(SproObject clsSproObject, SproDataInterface clsSproData)
        {
            try
            {
                bool bCheck = false;
                do
                {
#if false
//要修正 sakai
                    bCheck = clsSproObject.Check(clsSproData);
#else
                    bCheck = true;
#endif
                    if (bCheck)
                    {
                        break;
                    }
                    //if MsgBox("ハードウェアキーの認証に失敗しました。" & vbCrLf & "ハードウェアキーを確認してください。", vbCritical Or vbRetryCancel) <> vbRetry Then Exit Function
                    int rtn = (int)MessageBox.Show("ハードウェアキーの認証に失敗しました。" + "\r\n" + "ハードウェアキーを確認してください。");
                    if (rtn != 4/*vbRetry*/)
                    {
                        return false;
                    }
                } while (true);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '2006/12/7 NGS Yamada
        'ライセンスコードチェック。
        '
        'アプリケーションのライセンスコードを確認する。
        '
        '引き数：
        'clsSproObject SproObject
        'clsLicenseCheck LicenseCheck
        '
        '戻り値：
        'アプリケーションのライセンスコードが有効である場合 True を返す。
        'それ以外の場合 False を返す。
        Public Function CheckLicenseApp(ByVal clsSproObject As SproObject, ByRef clsLicenseCheck As LicenseCheck) As Boolean
            On Error GoTo ErrorHandler


            Dim sLicense As String
            Dim sResult As String


            CheckLicenseApp = False


            Dim clsRegNsNetwork As New RegKey
            Set clsRegNsNetwork = OpenRegistry()


            sLicense = clsRegNsNetwork.QueryValueString(REG_KEY_LICENSECODE, "")


            If sLicense<> "" Then
                If clsLicenseCheck.Check(sLicense, App.Title, App.Major, GetAppMinorVersion(), clsSproObject.Serial, sResult) = True Then
                    CheckLicenseApp = True
                    Exit Function
                End If
            End If


            frmInputLicense.ErrorMessage = sResult
            frmInputLicense.Show vbModal
            If frmInputLicense.Result = vbOK Then
                Call clsRegNsNetwork.SetString(REG_KEY_LICENSECODE, frmInputLicense.LicenseCode)
                CheckLicenseApp = True
                Exit Function
            End If


            Exit Function

        ErrorHandler:
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '2006/12/7 NGS Yamada
        'ライセンスコードチェック。
        '
        'アプリケーションのライセンスコードを確認する。
        '
        '引き数：
        'clsSproObject SproObject
        'clsLicenseCheck LicenseCheck
        '
        '戻り値：
        'アプリケーションのライセンスコードが有効である場合 True を返す。
        'それ以外の場合 False を返す。
        */
        public bool CheckLicenseApp(SproObject clsSproObject, ref LicenseCheck clsLicenseCheck)
        {
            try
            {
#if false
//要修正 sakai
                string sLicense;
                string sResult;
                RegKey clsRegNsNetwork = new RegKey();
                clsRegNsNetwork = OpenRegistry();

                sLicense = clsRegNsNetwork.QueryValueString(REG_KEY_LICENSECODE, "");


                if (sLicense != "")
                {
                    if (clsLicenseCheck.Check(sLicense, App.Title, App.Major, GetAppMinorVersion(), clsSproObject.Serial, sResult) == true)
                    {
                        return true;
                    }
                }

                frmInputLicense.ErrorMessage = sResult;
                frmInputLicense.Show vbModal;
                if (frmInputLicense.Result == vbOK)
                {
                    clsRegNsNetwork.SetString(REG_KEY_LICENSECODE, frmInputLicense.LicenseCode);
                    return true;
                }
#endif
                return false;
            }

            catch
            {
                return false;
            }
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'コマンドライン引き数を解析する。
        '
        '戻り値：ファイルのパス。
        Public Function ParseCommandLine() As String
            Dim sPath As String
            sPath = Command()
            sPath = LTrim$(sPath)
            If Left$(sPath, 1) = """" Then
                Dim nLen As Long
                nLen = InStr(2, sPath, """") - 2
                If 0 < nLen Then sPath = Mid$(sPath, 2, nLen)
            Else
                nLen = InStr(sPath, " ") - 1
                If 0 < nLen Then sPath = Left$(sPath, nLen)
            End If
            ParseCommandLine = sPath
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        'コマンドライン引き数を解析する。
        '
        '戻り値：ファイルのパス。
        */
        public string ParseCommandLine()
        {
            long nLen;
            string sPath = "";
            string dt;
#if false
            sPath = Command();
            sPath = sPath.TrimStart();
            if (Left(sPath, 1) == 34.ToString())
            {
                //nLen = InStr(2, sPath, """") - 2
                dt = Mid(sPath, 2, 99);
                nLen = Strings.InStr(dt, 34.ToString()) - 2;
                if (0 < nLen)
                {
                    sPath = Mid(sPath, 2, (int)nLen);
                }
            }
            else
            {
                nLen = Strings.InStr(sPath, " ") - 1;
                if (0 < nLen)
                {
                    sPath = Left(sPath, (int)nLen);
                }
            }
#endif
            return sPath;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'ファイルタイプの登録。
        Public Sub RegisterShellFileTypes(ByVal sExt As String, ByVal sFileTypeId As String, ByVal sDescription As String, ByVal nDefaultIcon As Long)
            If Left$(sExt, 1) <> "." Then sExt = "." & sExt
            Dim clsRegExt As New RegKey
            Call clsRegExt.CreateKey(HKEY_CLASSES_ROOT, sExt)
            sFileTypeId = clsRegExt.QueryValueString("", sFileTypeId)
            Call clsRegExt.SetString("", sFileTypeId)
            Dim clsRegFileTypeId As New RegKey
            Call clsRegFileTypeId.CreateKey(HKEY_CLASSES_ROOT, sFileTypeId)
            sDescription = clsRegFileTypeId.QueryValueString("", sDescription)
            Call clsRegFileTypeId.SetString("", sDescription)
            Dim clsRegDefaultIcon As New RegKey
            Call clsRegDefaultIcon.CreateKey(clsRegFileTypeId.Key, "DefaultIcon")
            Dim sText As String * MAX_PATH
            sText = ""
            Call GetModuleFileName(App.hInstance, sText, Len(sText) - 1)
            Dim sModulePath As String
            sModulePath = Left$(sText, InStr(sText, vbNullChar) - 1)
            Dim sDefaultIcon As String
            sDefaultIcon = clsRegDefaultIcon.QueryValueString("", sModulePath & "," & CStr(nDefaultIcon))
            Call clsRegDefaultIcon.SetString("", sDefaultIcon)
            Dim clsRegShell As New RegKey
            Call clsRegShell.CreateKey(clsRegFileTypeId.Key, "shell")
            Dim clsRegOpen As New RegKey
            Call clsRegOpen.CreateKey(clsRegShell.Key, "open")
            Dim clsRegCommand As New RegKey
            Call clsRegCommand.CreateKey(clsRegOpen.Key, "command")
            Dim sCommand As String
            sCommand = clsRegCommand.QueryValueString("", sModulePath & " ""%1""")
            Call clsRegCommand.SetString("", sCommand)
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'ファイルタイプの登録。
        public void RegisterShellFileTypes(string sExt, string sFileTypeId, string sDescription, long nDefaultIcon)
        {
            if (Left(sExt, 1) != ".") { sExt = "." + sExt; }
            RegKey clsRegExt = new RegKey();
            clsRegExt.CreateKey(HKEY_CLASSES_ROOT, sExt);
            sFileTypeId = clsRegExt.QueryValueString("", sFileTypeId);
            clsRegExt.SetString("", sFileTypeId);
            RegKey clsRegFileTypeId = new RegKey();
            clsRegFileTypeId.CreateKey(HKEY_CLASSES_ROOT, sFileTypeId);
            sDescription = clsRegFileTypeId.QueryValueString("", sDescription);
            clsRegFileTypeId.SetString("", sDescription);
            RegKey clsRegDefaultIcon = new RegKey();
            clsRegDefaultIcon.CreateKey(clsRegFileTypeId.Key(), "DefaultIcon");
            string sText;
            sText = "";
#if false
//要修正 sakai
            GetModuleFileName(ApphInstance, sText, sText.Length - 1);
#endif
            string sModulePath;
            //sModulePath = Left(sText, Strings.InStr(sText, vbNullChar) - 1);
            sModulePath = Left(sText, Strings.InStr(sText, null) - 1);
            string sDefaultIcon;
            sDefaultIcon = clsRegDefaultIcon.QueryValueString("", sModulePath + "," + nDefaultIcon.ToString());
            clsRegDefaultIcon.SetString("", sDefaultIcon);
            RegKey clsRegShell = new RegKey();
            clsRegShell.CreateKey(clsRegFileTypeId.Key(), "shell");
            RegKey clsRegOpen = new RegKey();
            clsRegOpen.CreateKey(clsRegShell.Key(), "open");
            RegKey clsRegCommand = new RegKey();
            clsRegCommand.CreateKey(clsRegOpen.Key(), "command");
            string sCommand = "";
#if false
//要修正 sakai
            sCommand = clsRegCommand.QueryValueString("", sModulePath + " ""%1""");
#endif
            clsRegCommand.SetString("", sCommand);
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'ファイルの内容をファイルにコピーする。
        '
        '引き数：
        'nSrcFile コピー元ファイル番号。
        'nDstFile コピー先ファイル番号。
        'nSize コピーサイズ。
        'nBlock 読み書きバッファのサイズ。
        'clsProgressInterface ProgressInterface オブジェクト。
        Public Sub CopyFileContents(ByVal nSrcFile As Integer, ByVal nDstFile As Integer, ByVal nSize As Long, ByVal nBlock As Long, ByVal clsProgressInterface As ProgressInterface)

            '読み書きバッファ。
            Dim nBuff() As Byte
            ReDim nBuff(nBlock - 1)


            Do While(nBlock <= nSize)
                Get #nSrcFile, , nBuff
                Put #nDstFile, , nBuff
                nSize = nSize - nBlock
                'プログレス。
                Call clsProgressInterface.CheckCancel
            Loop
            If 0 < nSize Then
                ReDim nBuff(nSize - 1)
                Get #nSrcFile, , nBuff
                Put #nDstFile, , nBuff
            End If


        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        'ファイルの内容をファイルにコピーする。
        '
        '引き数：
        'nSrcFile コピー元ファイル番号。
        'nDstFile コピー先ファイル番号。
        'nSize コピーサイズ。
        'nBlock 読み書きバッファのサイズ。
        'clsProgressInterface ProgressInterface オブジェクト。
        */
        public void CopyFileContents(int nSrcFile, int nDstFile, long nSize, long nBlock, ProgressInterface clsProgressInterface)
        {
            //'読み書きバッファ。
            byte[] nBuff = new byte[nBlock];


            while (nBlock <= nSize)
            {
#if false
                Get #nSrcFile, , nBuff
                Put #nDstFile, , nBuff
#endif
                nSize = nSize - nBlock;
                //'プログレス。
                clsProgressInterface.CheckCancel();
            }
            if (0 < nSize)
            {
                nBuff = new byte[nBlock];
#if false
                Get #nSrcFile, , nBuff
                Put #nDstFile, , nBuff
#endif
            }
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '年(年号)を取得する。
        '
        '元年未満は負の値になる。
        '
        '引き数：
        'tDate 日時。
        '
        '戻り値：年を返す。
        Public Function GetEraYear(ByVal tDate As Date) As Long
            Dim nEraBase As Long
            nEraBase = GetPrivateProfileInt(PROFILE_DEF_SEC_ERA, PROFILE_DEF_KEY_BASE, 0, App.Path & "\" & PROFILE_DEF_NAME)
            Dim nYear As Long
            nYear = Year(tDate) - nEraBase
            GetEraYear = nYear
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '年(年号)を取得する。
        '
        '元年未満は負の値になる。
        '
        '引き数：
        'tDate 日時。
        '
        '戻り値：年を返す。
        */
        public long GetEraYear(DateTime tDate)
        {
            long nEraBase;
            nEraBase = GetPrivateProfileInt(PROFILE_DEF_SEC_ERA, PROFILE_DEF_KEY_BASE, 0, AppPath + "\\" + PROFILE_DEF_NAME);
            long nYear;
            nYear = tDate.Year - nEraBase;
            return nYear;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '年度(年号)を取得する。
        '
        '元年未満は負の値になる。
        '
        '引き数：
        'tDate 日時。
        '
        '戻り値：年度を返す。
        Public Function GetEraFiscal(ByVal tDate As Date) As Long
            Dim nYear As Long
            nYear = GetEraYear(tDate)
            If Month(tDate) < 4 Then nYear = nYear - 1
            GetEraFiscal = nYear
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '年度(年号)を取得する。
        '
        '元年未満は負の値になる。
        '
        '引き数：
        'tDate 日時。
        '
        '戻り値：年度を返す。
        */
        public long GetEraFiscal(DateTime tDate)
        {
            long nYear;
            nYear = GetEraYear(tDate);
            if (tDate.Month < 4)
            {
                nYear = nYear - 1;
            }
            return nYear;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'ファイルのタイムスタンプを評価する。
        Function CheckModuleTimeStamp(ByRef sFile() As String, ByRef tDate() As Date, ByVal nCount As Long) As Boolean
            CheckModuleTimeStamp = False
            Dim clsFileObj As FileObject
            Dim sPath As String
            Dim i As Long
            For i = 0 To nCount - 1
                Set clsFileObj = New FileObject
                sPath = App.Path & "\" & sFile(i)
                If clsFileObj.CreateFile(sPath, GENERIC_READ, FILE_SHARE_READ, 0, OPEN_EXISTING, FILE_ATTRIBUTE_NORMAL, 0) Then
                    Dim tCreationTime As Date
                    Dim tLastAccessTime As Date
                    Dim tLastWriteTime As Date
                    If Not clsFileObj.GetFileTime(tCreationTime, tLastAccessTime, tLastWriteTime) Then Call Err.Raise(ERR_FILE, , sPath & vbCrLf & "にアクセスできません。")
                    'ファイルが古い場合は False を返す。
                    If tDate(i) > tLastWriteTime Then Exit Function
                Else
                    'ファイルが存在しない場合はOKとする。ファイルが足りなければ他でエラーとなるだろう。
                End If
            Next
            CheckModuleTimeStamp = True
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'ファイルのタイムスタンプを評価する。
        public bool CheckModuleTimeStamp(ref string[] sFile, ref DateTime[] tDate, long nCount)
        {
            FileObject clsFileObj;
            string sPath;
            long i;
            for (i = 0; i < nCount; i++)
            {
                clsFileObj = new FileObject();
                sPath = AppPath + "\\" + sFile[i];
                if (clsFileObj.CreateFile(sPath, GENERIC_READ, FILE_SHARE_READ, 0, OPEN_EXISTING, FILE_ATTRIBUTE_NORMAL, 0))
                {
                    DateTime tCreationTime = default;
                    DateTime tLastAccessTime = default;
                    DateTime tLastWriteTime = default;
                    if (!clsFileObj.GetFileTime(ref tCreationTime, ref tLastAccessTime, ref tLastWriteTime))
                    {
                       // Err.Raise(ERR_FILE, , sPath + "\r\n" + "にアクセスできません。");
                    }
                    //'ファイルが古い場合は False を返す。
                    if (tDate[i] > tLastWriteTime)
                    {
                        return false;
                    }
                }
                else
                {
                    //'ファイルが存在しない場合はOKとする。ファイルが足りなければ他でエラーとなるだろう。
                }
            }
            return true;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '解析に使用した周波数の表示文字列の取得。
        '
        '引き数：
        'nSattSignalGPS     GPS衛星信号。ビットフラグ。0x01＝L1、0x02＝L2、0x04＝L5。0の場合は衛星無しを意味する。
        'nSattSignalGLONASS GLONASS衛星信号。ビットフラグ。0x01＝G1、0x02＝G2、0x04＝G3。0の場合は衛星無しを意味する。
        'nSattSignalGalileo Galileo衛星信号。ビットフラグ。0x01＝E1、0x02＝E5。0の場合は衛星無しを意味する。
        'nSattSignalQZSS    QZSS衛星信号。ビットフラグ。0x01＝L1、0x02＝L2、0x04＝L5。0の場合は衛星無しを意味する。
        'nSattSignalBeiDou  BeiDou衛星信号。ビットフラグ。0x01＝B1、0x02＝B2、0x04＝B3。0の場合は衛星無しを意味する。
        '
        '戻り値：表示文字列。
        Function GetDispFrequency(ByVal nSattSignalGPS As Long, ByVal nSattSignalGLONASS As Long, ByVal nSattSignalGalileo As Long, ByVal nSattSignalQZSS As Long, ByVal nSattSignalBeiDou As Long) As String

            Dim sFrequency As String
            Dim sSeparateSatt As String
            '2022/02/07 SattSignal の追加。''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            'Dim sSeparateSignal As String
            'If 0 < nSattSignalGPS Then
            '    sFrequency = sFrequency & sSeparateSatt & "GPS "
            '    sSeparateSatt = "/"
            '    sSeparateSignal = ""
            '    If (nSattSignalGPS And &H1) = &H1 Then
            '        sFrequency = sFrequency & sSeparateSignal & "L1"
            '        sSeparateSignal = ","
            '    End If
            '    If (nSattSignalGPS And &H2) = &H2 Then
            '        sFrequency = sFrequency & sSeparateSignal & "L2"
            '        sSeparateSignal = ","
            '    End If
            '    If (nSattSignalGPS And &H4) = &H4 Then
            '        sFrequency = sFrequency & sSeparateSignal & "L5"
            '        sSeparateSignal = ","
            '    End If
            'End If
            'If 0 < nSattSignalGLONASS Then
            '    sFrequency = sFrequency & sSeparateSatt & "GLONASS "
            '    sSeparateSatt = "/"
            '    sSeparateSignal = ""
            '    If (nSattSignalGLONASS And &H1) = &H1 Then
            '        sFrequency = sFrequency & sSeparateSignal & "L1"
            '        sSeparateSignal = ","
            '    End If
            '    If (nSattSignalGLONASS And &H2) = &H2 Then
            '        sFrequency = sFrequency & sSeparateSignal & "L2"
            '        sSeparateSignal = ","
            '    End If
            'End If
            'If 0 < nSattSignalGalileo Then
            '    sFrequency = sFrequency & sSeparateSatt & "Galileo "
            '    sSeparateSatt = "/"
            '    sSeparateSignal = ""
            '    If (nSattSignalGalileo And &H1) = &H1 Then
            '        sFrequency = sFrequency & sSeparateSignal & "E1"
            '        sSeparateSignal = ","
            '    End If
            '    If (nSattSignalGalileo And &H2) = &H2 Then
            '        sFrequency = sFrequency & sSeparateSignal & "E5"
            '        sSeparateSignal = ","
            '    End If
            'End If
            'If 0 < nSattSignalQZSS Then
            '    sFrequency = sFrequency & sSeparateSatt & "QZSS "
            '    sSeparateSatt = "/"
            '    sSeparateSignal = ""
            '    If (nSattSignalQZSS And &H1) = &H1 Then
            '        sFrequency = sFrequency & sSeparateSignal & "L1"
            '        sSeparateSignal = ","
            '    End If
            '    If (nSattSignalQZSS And &H2) = &H2 Then
            '        sFrequency = sFrequency & sSeparateSignal & "L2"
            '        sSeparateSignal = ","
            '    End If
            '    If (nSattSignalQZSS And &H4) = &H4 Then
            '        sFrequency = sFrequency & sSeparateSignal & "L5"
            '        sSeparateSignal = ","
            '    End If
            'End If
            'If 0 < nSattSignalBeiDou Then
            '    sFrequency = sFrequency & sSeparateSatt & "BeiDou "
            '    sSeparateSatt = "/"
            '    sSeparateSignal = ""
            '    If (nSattSignalBeiDou And &H1) = &H1 Then
            '        sFrequency = sFrequency & sSeparateSignal & "B1"
            '        sSeparateSignal = ","
            '    End If
            '    If (nSattSignalBeiDou And &H2) = &H2 Then
            '        sFrequency = sFrequency & sSeparateSignal & "B2"
            '        sSeparateSignal = ","
            '    End If
            'End If
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            If 0 < nSattSignalGPS Then
                sFrequency = sFrequency & sSeparateSatt & "GPS "
                sSeparateSatt = " / "
                sFrequency = sFrequency & GetDispFrequencyGps(nSattSignalGPS)
            End If
            If 0 < nSattSignalGLONASS Then
                sFrequency = sFrequency & sSeparateSatt & "GLONASS "
                sSeparateSatt = " / "
                sFrequency = sFrequency & GetDispFrequencyGlonass(nSattSignalGLONASS)
            End If
            If 0 < nSattSignalGalileo Then
                sFrequency = sFrequency & sSeparateSatt & "Galileo "
                sSeparateSatt = " / "
                sFrequency = sFrequency & GetDispFrequencyGalileo(nSattSignalGalileo)
            End If
            If 0 < nSattSignalQZSS Then
                sFrequency = sFrequency & sSeparateSatt & "QZSS "
                sSeparateSatt = " / "
                sFrequency = sFrequency & GetDispFrequencyQzss(nSattSignalQZSS)
            End If
            If 0 < nSattSignalBeiDou Then
                sFrequency = sFrequency & sSeparateSatt & "BeiDou "
                sSeparateSatt = " / "
                sFrequency = sFrequency & GetDispFrequencyBeiDou(nSattSignalBeiDou)
            End If
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''


            GetDispFrequency = sFrequency

        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '解析に使用した周波数の表示文字列の取得。
        '
        '引き数：
        'nSattSignalGPS     GPS衛星信号。ビットフラグ。0x01＝L1、0x02＝L2、0x04＝L5。0の場合は衛星無しを意味する。
        'nSattSignalGLONASS GLONASS衛星信号。ビットフラグ。0x01＝G1、0x02＝G2、0x04＝G3。0の場合は衛星無しを意味する。
        'nSattSignalGalileo Galileo衛星信号。ビットフラグ。0x01＝E1、0x02＝E5。0の場合は衛星無しを意味する。
        'nSattSignalQZSS    QZSS衛星信号。ビットフラグ。0x01＝L1、0x02＝L2、0x04＝L5。0の場合は衛星無しを意味する。
        'nSattSignalBeiDou  BeiDou衛星信号。ビットフラグ。0x01＝B1、0x02＝B2、0x04＝B3。0の場合は衛星無しを意味する。
        '
        '戻り値：表示文字列。
        */
        public string GetDispFrequency(long nSattSignalGPS, long nSattSignalGLONASS, long nSattSignalGalileo, long nSattSignalQZSS, long nSattSignalBeiDou)
        {
            string sFrequency = "";
            string sSeparateSatt = "";
            /*
            '2022/02/07 SattSignal の追加。''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            'Dim sSeparateSignal As String
            'If 0 < nSattSignalGPS Then
            '    sFrequency = sFrequency & sSeparateSatt & "GPS "
            '    sSeparateSatt = "/"
            '    sSeparateSignal = ""
            '    If (nSattSignalGPS And &H1) = &H1 Then
            '        sFrequency = sFrequency & sSeparateSignal & "L1"
            '        sSeparateSignal = ","
            '    End If
            '    If (nSattSignalGPS And &H2) = &H2 Then
            '        sFrequency = sFrequency & sSeparateSignal & "L2"
            '        sSeparateSignal = ","
            '    End If
            '    If (nSattSignalGPS And &H4) = &H4 Then
            '        sFrequency = sFrequency & sSeparateSignal & "L5"
            '        sSeparateSignal = ","
            '    End If
            'End If
            'If 0 < nSattSignalGLONASS Then
            '    sFrequency = sFrequency & sSeparateSatt & "GLONASS "
            '    sSeparateSatt = "/"
            '    sSeparateSignal = ""
            '    If (nSattSignalGLONASS And &H1) = &H1 Then
            '        sFrequency = sFrequency & sSeparateSignal & "L1"
            '        sSeparateSignal = ","
            '    End If
            '    If (nSattSignalGLONASS And &H2) = &H2 Then
            '        sFrequency = sFrequency & sSeparateSignal & "L2"
            '        sSeparateSignal = ","
            '    End If
            'End If
            'If 0 < nSattSignalGalileo Then
            '    sFrequency = sFrequency & sSeparateSatt & "Galileo "
            '    sSeparateSatt = "/"
            '    sSeparateSignal = ""
            '    If (nSattSignalGalileo And &H1) = &H1 Then
            '        sFrequency = sFrequency & sSeparateSignal & "E1"
            '        sSeparateSignal = ","
            '    End If
            '    If (nSattSignalGalileo And &H2) = &H2 Then
            '        sFrequency = sFrequency & sSeparateSignal & "E5"
            '        sSeparateSignal = ","
            '    End If
            'End If
            'If 0 < nSattSignalQZSS Then
            '    sFrequency = sFrequency & sSeparateSatt & "QZSS "
            '    sSeparateSatt = "/"
            '    sSeparateSignal = ""
            '    If (nSattSignalQZSS And &H1) = &H1 Then
            '        sFrequency = sFrequency & sSeparateSignal & "L1"
            '        sSeparateSignal = ","
            '    End If
            '    If (nSattSignalQZSS And &H2) = &H2 Then
            '        sFrequency = sFrequency & sSeparateSignal & "L2"
            '        sSeparateSignal = ","
            '    End If
            '    If (nSattSignalQZSS And &H4) = &H4 Then
            '        sFrequency = sFrequency & sSeparateSignal & "L5"
            '        sSeparateSignal = ","
            '    End If
            'End If
            'If 0 < nSattSignalBeiDou Then
            '    sFrequency = sFrequency & sSeparateSatt & "BeiDou "
            '    sSeparateSatt = "/"
            '    sSeparateSignal = ""
            '    If (nSattSignalBeiDou And &H1) = &H1 Then
            '        sFrequency = sFrequency & sSeparateSignal & "B1"
            '        sSeparateSignal = ","
            '    End If
            '    If (nSattSignalBeiDou And &H2) = &H2 Then
            '        sFrequency = sFrequency & sSeparateSignal & "B2"
            '        sSeparateSignal = ","
            '    End If
            'End If
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            */
            if (0 < nSattSignalGPS)
            {
                sFrequency = sFrequency + sSeparateSatt + "GPS ";
                sSeparateSatt = " / ";
                sFrequency = sFrequency + GetDispFrequencyGps(nSattSignalGPS);
            }
            if (0 < nSattSignalGLONASS)
            {
                sFrequency = sFrequency + sSeparateSatt + "GLONASS ";
                sSeparateSatt = " / ";
                sFrequency = sFrequency + GetDispFrequencyGlonass(nSattSignalGLONASS);
            }
            if (0 < nSattSignalGalileo)
            {
                sFrequency = sFrequency + sSeparateSatt + "Galileo ";
                sSeparateSatt = " / ";
                sFrequency = sFrequency + GetDispFrequencyGalileo(nSattSignalGalileo);
            }
            if (0 < nSattSignalQZSS)
            {
                sFrequency = sFrequency + sSeparateSatt + "QZSS ";
                sSeparateSatt = " / ";
                sFrequency = sFrequency + GetDispFrequencyQzss(nSattSignalQZSS);
            }
            if (0 < nSattSignalBeiDou)
            {
                sFrequency = sFrequency + sSeparateSatt + "BeiDou ";
                sSeparateSatt = " / ";
                sFrequency = sFrequency + GetDispFrequencyBeiDou(nSattSignalBeiDou);
            }
            //'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''


            return sFrequency;

        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '2022/02/07 SattSignal の追加。''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '解析に使用した周波数の表示文字列の取得(GPS)。
        '
        '引き数：
        'nSattSignalGPS     GPS衛星信号。ビットフラグ。0x01＝L1、0x02＝L2、0x04＝L5。
        '
        '戻り値：表示文字列。
        Function GetDispFrequencyGps(ByVal nSattSignalGPS As Long) As String

            Dim sFrequency As String
            Dim sSeparateSignal As String


            sFrequency = ""
            sSeparateSignal = ""


            If(nSattSignalGPS And & H1) = &H1 Then
                sFrequency = sFrequency & sSeparateSignal & "L1"
                sSeparateSignal = ","
            End If
            If(nSattSignalGPS And & H2) = &H2 Then
                sFrequency = sFrequency & sSeparateSignal & "L2"
                sSeparateSignal = ","
            End If
            If(nSattSignalGPS And & H4) = &H4 Then
                sFrequency = sFrequency & sSeparateSignal & "L5"
                sSeparateSignal = ","
            End If


            GetDispFrequencyGps = sFrequency

        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '2022/02/07 SattSignal の追加。''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '解析に使用した周波数の表示文字列の取得(GPS)。
        '
        '引き数：
        'nSattSignalGPS     GPS衛星信号。ビットフラグ。0x01＝L1、0x02＝L2、0x04＝L5。
        '
        '戻り値：表示文字列。
        */
        public string GetDispFrequencyGps(long nSattSignalGPS)
        {

            string sFrequency;
            string sSeparateSignal;


            sFrequency = "";
            sSeparateSignal = "";


            if ((nSattSignalGPS & 0x01) == 0x01)
            {
                sFrequency = sFrequency + sSeparateSignal + "L1";
                sSeparateSignal = ",";
            }
            if ((nSattSignalGPS & 0x02) == 0x02)
            {
                sFrequency = sFrequency + sSeparateSignal + "L2";
                sSeparateSignal = ",";
            }
            if ((nSattSignalGPS & 0x04) == 0x04)
            {
                sFrequency = sFrequency + sSeparateSignal + "L5";
                sSeparateSignal = ",";
            }

            return sFrequency;

        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '解析に使用した周波数の表示文字列の取得(GLONASS)。
        '
        '引き数：
        'nSattSignalGLONASS GLONASS衛星信号。ビットフラグ。0x01＝G1、0x02＝G2、0x04＝G3。
        '
        '戻り値：表示文字列。
        Function GetDispFrequencyGlonass(ByVal nSattSignalGLONASS As Long) As String

            Dim sFrequency As String
            Dim sSeparateSignal As String


            sFrequency = ""
            sSeparateSignal = ""


            If(nSattSignalGLONASS And & H1) = &H1 Then
                sFrequency = sFrequency & sSeparateSignal & "G1"
                sSeparateSignal = ","
            End If
            If(nSattSignalGLONASS And & H2) = &H2 Then
                sFrequency = sFrequency & sSeparateSignal & "G2"
                sSeparateSignal = ","
            End If
            If(nSattSignalGLONASS And & H4) = &H4 Then
                sFrequency = sFrequency & sSeparateSignal & "G3"
                sSeparateSignal = ","
            End If


            GetDispFrequencyGlonass = sFrequency

        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '解析に使用した周波数の表示文字列の取得(GLONASS)。
        '
        '引き数：
        'nSattSignalGLONASS GLONASS衛星信号。ビットフラグ。0x01＝G1、0x02＝G2、0x04＝G3。
        '
        '戻り値：表示文字列。
        */
        public string GetDispFrequencyGlonass(long nSattSignalGLONASS)
        {

            string sFrequency;
            string sSeparateSignal;


            sFrequency = "";
            sSeparateSignal = "";


            if ((nSattSignalGLONASS & 0x01) == 0x01)
            {
                sFrequency = sFrequency + sSeparateSignal + "G1";
                sSeparateSignal = ",";
            }
            if ((nSattSignalGLONASS & 0x02) == 0x02)
            {
                sFrequency = sFrequency + sSeparateSignal + "G2";
                sSeparateSignal = ",";
            }
            if ((nSattSignalGLONASS & 0x04) == 0x04)
            {
                sFrequency = sFrequency + sSeparateSignal + "G3";
                sSeparateSignal = ",";
            }

            return sFrequency;

        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '解析に使用した周波数の表示文字列の取得(Galileo)。
        '
        '引き数：
        'nSattSignalGalileo Galileo衛星信号。ビットフラグ。0x01＝E1、0x02＝E5。
        '
        '戻り値：表示文字列。
        Function GetDispFrequencyGalileo(ByVal nSattSignalGalileo As Long) As String

            Dim sFrequency As String
            Dim sSeparateSignal As String


            sFrequency = ""
            sSeparateSignal = ""


            If(nSattSignalGalileo And & H1) = &H1 Then
                sFrequency = sFrequency & sSeparateSignal & "E1"
                sSeparateSignal = ","
            End If
            If(nSattSignalGalileo And & H2) = &H2 Then
                sFrequency = sFrequency & sSeparateSignal & "E5"
                sSeparateSignal = ","
            End If


            GetDispFrequencyGalileo = sFrequency

        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '解析に使用した周波数の表示文字列の取得(Galileo)。
        '
        '引き数：
        'nSattSignalGalileo Galileo衛星信号。ビットフラグ。0x01＝E1、0x02＝E5。
        '
        '戻り値：表示文字列。
        */
        public string GetDispFrequencyGalileo(long nSattSignalGalileo)
        {

            string sFrequency;
            string sSeparateSignal;

            sFrequency = "";
            sSeparateSignal = "";

            if ((nSattSignalGalileo & 0x01) == 0x01)
            {
                sFrequency = sFrequency + sSeparateSignal + "E1";
                sSeparateSignal = ",";
            }
            if ((nSattSignalGalileo & 0x02) == 0x02)
            {
                sFrequency = sFrequency + sSeparateSignal + "E5";
                sSeparateSignal = ",";
            }

            return sFrequency;

        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '解析に使用した周波数の表示文字列の取得(QZSS)。
        '
        '引き数：
        'nSattSignalQZSS    QZSS衛星信号。ビットフラグ。0x01＝L1、0x02＝L2、0x04＝L5。
        '
        '戻り値：表示文字列。
        Function GetDispFrequencyQzss(ByVal nSattSignalQZSS As Long) As String

            Dim sFrequency As String
            Dim sSeparateSignal As String


            sFrequency = ""
            sSeparateSignal = ""


            If(nSattSignalQZSS And & H1) = &H1 Then
                sFrequency = sFrequency & sSeparateSignal & "L1"
                sSeparateSignal = ","
            End If
            If(nSattSignalQZSS And & H2) = &H2 Then
                sFrequency = sFrequency & sSeparateSignal & "L2"
                sSeparateSignal = ","
            End If
            If(nSattSignalQZSS And & H4) = &H4 Then
                sFrequency = sFrequency & sSeparateSignal & "L5"
                sSeparateSignal = ","
            End If


            GetDispFrequencyQzss = sFrequency

        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '解析に使用した周波数の表示文字列の取得(QZSS)。
        '
        '引き数：
        'nSattSignalQZSS    QZSS衛星信号。ビットフラグ。0x01＝L1、0x02＝L2、0x04＝L5。
        '
        '戻り値：表示文字列。
        */
        public string GetDispFrequencyQzss(long nSattSignalQZSS)
        {

            string sFrequency;
            string sSeparateSignal;

            sFrequency = "";
            sSeparateSignal = "";

            if ((nSattSignalQZSS & 0x01) == 0x01)
            {
                sFrequency = sFrequency + sSeparateSignal + "L1";
                sSeparateSignal = ",";
            }
            if ((nSattSignalQZSS & 0x02) == 0x02)
            {
                sFrequency = sFrequency + sSeparateSignal + "L2";
                sSeparateSignal = ",";
            }
            if ((nSattSignalQZSS & 0x04) == 0x04)
            {
                sFrequency = sFrequency + sSeparateSignal + "L5";
                sSeparateSignal = ",";
            }

            return sFrequency;

        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '解析に使用した周波数の表示文字列の取得(BeiDou)。
        '
        '引き数：
        'nSattSignalBeiDou  BeiDou衛星信号。ビットフラグ。0x01＝B1、0x02＝B2、0x04＝B3。
        '
        '戻り値：表示文字列。
        Function GetDispFrequencyBeiDou(ByVal nSattSignalBeiDou As Long) As String

            Dim sFrequency As String
            Dim sSeparateSignal As String


            sFrequency = ""
            sSeparateSignal = ""


            If(nSattSignalBeiDou And & H1) = &H1 Then
                sFrequency = sFrequency & sSeparateSignal & "B1"
                sSeparateSignal = ","
            End If
            If(nSattSignalBeiDou And & H2) = &H2 Then
                sFrequency = sFrequency & sSeparateSignal & "B2"
                sSeparateSignal = ","
            End If
            If(nSattSignalBeiDou And & H4) = &H4 Then
                sFrequency = sFrequency & sSeparateSignal & "B3"
                sSeparateSignal = ","
            End If


            GetDispFrequencyBeiDou = sFrequency

        End Function
        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '解析に使用した周波数の表示文字列の取得(BeiDou)。
        '
        '引き数：
        'nSattSignalBeiDou  BeiDou衛星信号。ビットフラグ。0x01＝B1、0x02＝B2、0x04＝B3。
        '
        '戻り値：表示文字列。
        */
        public string GetDispFrequencyBeiDou(long nSattSignalBeiDou)
        {
            string sFrequency;
            string sSeparateSignal;


            sFrequency = "";
            sSeparateSignal = "";


            if ((nSattSignalBeiDou & 0x01) == 0x01)
            {
                sFrequency = sFrequency + sSeparateSignal + "B1";
                sSeparateSignal = ",";
            }
            if ((nSattSignalBeiDou & 0x02) == 0x02)
            {
                sFrequency = sFrequency + sSeparateSignal + "B2";
                sSeparateSignal = ",";
            }
            if ((nSattSignalBeiDou & 0x04) == 0x04)
            {
                sFrequency = sFrequency + sSeparateSignal + "B3";
                sSeparateSignal = ",";
            }


            return sFrequency;

        }
        //==========================================================================================
    }
}
