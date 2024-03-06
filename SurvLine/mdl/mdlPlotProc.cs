using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;
using static SurvLine.PlotPane;
using static SurvLine.mdl.DEFINE;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace SurvLine.mdl
{
    public class MdlPlotProc
    {
        //==========================================================================================
        /*[VB]
        '*******************************************************************************
        'プロット画面プロシージャ
        '
        '平面直角座標→実座標、ｍ。
        '論理エリア　→プロジェクト全体の範囲、ピクセル。左上が(0,0)。
        'デバイス　　→デバイスコンテキストの範囲、ピクセル。論理エリア内の実際に描画する範囲。
        'ビュー　　　→ビューの範囲、ピクセル。論理エリア内で画面や印刷で実際に見える範囲。
        '
        '位置(座標)は基本的に論理エリア座標系である。単位はピクセル。

        Option Explicit
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '定数
        Public Const DRAW_SPACE As Double = 0.1 '描画余白割合。
        Public Const MIN_LOGICALSIZE As Double = 0.001 '最小論理領域サイズ(ｍ)。
        Public Const PLOT_MEMDC_SIZE = 1 'バックバッファのサイズ(係数)。

        Public Const PLOT_SCALEBAR_HEIGHT As Long = 228 'スケールバーの高さ(Twips)。
        Public Const PLOT_SCALEBAR_COLOR_1 As Long = &HFFFFFF 'スケールバー色その１。
        Public Const PLOT_SCALEBAR_COLOR_2 As Long = &HFF& 'スケールバー色その２。

        Public Const PLOT_OBSPNT_RADIUS As Single = 100 '観測点半径(Twips)。
        Public Const PLOT_OBSPNT_RADIUS_FIX As Single = 130 '固定点半径(Twips)。
        Public Const PLOT_OBSPNT_RADIUS_BASE As Single = 100 '基準点半径(Twips)。
        Public Const PLOT_OBSPNT_SYMBOL_SPACE As Long = 150 '観測点の中心から記号までのＹ軸距離(Twips)。

        Public Const PLOT_VECTOR_ARROW_LENGTH As Long = 150 '矢印長さ(Twips)。
        Public Const PLOT_VECTOR_ARROW_WIDTH As Long = 150 '矢印幅(Twips)。

        '数学定数
        Public MATH_S1 As Double 'sin270°
        Public MATH_S2 As Double 'sin150°
        Public MATH_S3 As Double 'sin30°
        Public MATH_C1 As Double 'cos270°
        Public MATH_C2 As Double 'cos150°
        Public MATH_C3 As Double 'cos30°
        Public MATH_SQR3 As Double '√3

        'プロット種別。
        Enum PLOTTYPE_ENUM
            PLOTTYPE_STANDARD '標準。
            PLOTTYPE_ANGLE '閉合差。
            PLOTTYPE_PRINT '印刷。
        End Enum
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'定数
        public const double DRAW_SPACE = 0.1;                   //'描画余白割合。
        public const double MIN_LOGICALSIZE = 0.001;            //'最小論理領域サイズ(ｍ)。
        public const long PLOT_MEMDC_SIZE = 1;                  //'バックバッファのサイズ(係数)。

        public const long PLOT_SCALEBAR_HEIGHT = 228;           //'スケールバーの高さ(Twips)。
        public const long PLOT_SCALEBAR_COLOR_1 = 0xFFFFFF;     //'スケールバー色その１。
        public const long PLOT_SCALEBAR_COLOR_2 = 0xFF;         //'スケールバー色その２。

        public const float PLOT_OBSPNT_RADIUS = 100;            //'観測点半径(Twips)。
        public const float PLOT_OBSPNT_RADIUS_FIX = 130;        //'固定点半径(Twips)。
        public const float PLOT_OBSPNT_RADIUS_BASE = 100;       //'基準点半径(Twips)。
        public const long PLOT_OBSPNT_SYMBOL_SPACE = 150;       //'観測点の中心から記号までのＹ軸距離(Twips)。

        public const long PLOT_VECTOR_ARROW_LENGTH = 150;       //'矢印長さ(Twips)。
        public const long PLOT_VECTOR_ARROW_WIDTH = 150;        //'矢印幅(Twips)。

        //'数学定数
        public double MATH_S1;                                  //'sin270°
        public double MATH_S2;                                  //'sin150°
        public double MATH_S3;                                  //'sin30°
        public double MATH_C1;                                  //'cos270°
        public double MATH_C2;                                  //'cos150°
        public double MATH_C3;                                  //'cos30°
        public double MATH_SQR3;                                //'√3




        /*
         * mdlDefine.cs へ移動
         * 
        //'プロット種別。
        public enum PLOTTYPE_ENUM
        {
            PLOTTYPE_STANDARD,                                  //'標準。
            PLOTTYPE_ANGLE,                                     //'閉合差。
            PLOTTYPE_PRINT,                                     //'印刷。
        }
        */
        //==========================================================================================

        //==========================================================================================
        //[C#]
        public MdlPlotProc()
        {

        }
        //==========================================================================================

        //==========================================================================================
        //[C#]
        //'sin270°
        public void set_MATH_S1(double MS1)
        {
            MATH_S1 = MS1;
        }
        //'sin150°
        public void set_MATH_S2(double MS2)
        {
            MATH_S2 = MS2;
        }
        //'sin30°
        public void set_MATH_S3(double MS3)
        {
            MATH_S3 = MS3;
        }
        //'cos270°
        public void set_MATH_C1(double MC1)
        {
            MATH_C1 = MC1;
        }
        //'cos150°
        public void set_MATH_C2(double MC2)
        {
            MATH_C2 = MC2;
        }
        //'cos30°
        public void set_MATH_C3(double MC3)
        {
            MATH_C3 = MC3;
        }
        //'√3
        public void set_MATH_SQR3(double MSQ3)
        {
            MATH_SQR3 = MSQ3;
        }
        //==========================================================================================


        //==========================================================================================
        /*[VB]
        '基線ベクトルを描画する。
        '
        '引き数：
        'hDC デバイスコンテキストのハンドル。
        'nDeviceLeft 論理エリア上のデバイス左辺位置(ピクセル)。
        'nDeviceTop 論理エリア上のデバイス上辺位置(ピクセル)。
        'nMapScale Twips→ピクセル、の係数。
        'clsBaseLineVector 基線ベクトル。
        'hPenLine 線のペン。
        'hPenArrow 矢印のペン。
        'bArrow 矢印描画の指定。True=描画する。False=描画しない。
        Public Sub DrawBaseLineVector(ByVal hDC As Long, ByVal nDeviceLeft As Double, ByVal nDeviceTop As Double, ByVal nMapScale As Double, ByVal clsBaseLineVector As BaseLineVector, ByVal hPenLine As Long, ByVal hPenArrow As Long, ByVal bArrow As Boolean)

            '定数。
            Dim nPLOT_OBSPNT_RADIUS_FIX As Double
            Dim nPLOT_OBSPNT_RADIUS As Double
            Dim nPLOT_VECTOR_ARROW_LENGTH As Double
            Dim nPLOT_VECTOR_ARROW_WIDTH As Double
            nPLOT_OBSPNT_RADIUS_FIX = PLOT_OBSPNT_RADIUS_FIX* nMapScale
            nPLOT_OBSPNT_RADIUS = PLOT_OBSPNT_RADIUS* nMapScale
            nPLOT_VECTOR_ARROW_LENGTH = PLOT_VECTOR_ARROW_LENGTH* nMapScale
            nPLOT_VECTOR_ARROW_WIDTH = PLOT_VECTOR_ARROW_WIDTH* nMapScale


            Call SelectObject(hDC, hPenLine)

            '方向余弦を求める
            Dim clsStrDevicePoint As TwipsPoint
            Dim clsEndDevicePoint As TwipsPoint
            Set clsStrDevicePoint = clsBaseLineVector.StrPoint.DevicePoint
            Set clsEndDevicePoint = clsBaseLineVector.EndPoint.DevicePoint
            Dim L As Double, M As Double
            If Not GetDirectionCosines(clsStrDevicePoint, clsEndDevicePoint, L, M) Then
                '始点と終点の座標が同じ場合、点を打っておく。
                Call Ellipse(hDC, clsStrDevicePoint.X - nDeviceLeft, clsStrDevicePoint.Y - nDeviceTop, clsStrDevicePoint.X - nDeviceLeft + 1, clsStrDevicePoint.Y - nDeviceTop + 1)
                Exit Sub
            End If


            Dim xp1 As Double, yp1 As Double, xp2 As Double, yp2 As Double
            If clsBaseLineVector.StrPoint.TopParentPoint.Based Then
                '中心
                xp1 = 0
                yp1 = 0
            ElseIf clsBaseLineVector.StrPoint.TopParentPoint.Fixed Then
                'P1(三角との交点)を求める
                Call GetTriangleIntersection(M, L, xp1, yp1, nPLOT_OBSPNT_RADIUS_FIX)
            Else
                'P1(X1の円周点)を求める
                xp1 = nPLOT_OBSPNT_RADIUS * L
                yp1 = nPLOT_OBSPNT_RADIUS * M
            End If
            xp1 = xp1 + clsStrDevicePoint.X
            yp1 = yp1 + clsStrDevicePoint.Y


            Dim dR As Double
            If clsBaseLineVector.EndPoint.TopParentPoint.Based Then
                '中心
                xp2 = 0
                yp2 = 0
            ElseIf clsBaseLineVector.EndPoint.TopParentPoint.Fixed Then
                'P2(三角との交点)を求める
                Call GetTriangleIntersection(-M, -L, xp2, yp2, nPLOT_OBSPNT_RADIUS_FIX)
                dR = Sqr(xp2* xp2 + yp2* yp2)
            Else
                'P2(X2の円周点)を求める
                xp2 = nPLOT_OBSPNT_RADIUS* -L
                yp2 = nPLOT_OBSPNT_RADIUS * -M
                dR = nPLOT_OBSPNT_RADIUS
            End If
            xp2 = xp2 + clsEndDevicePoint.X
            yp2 = yp2 + clsEndDevicePoint.Y

            Dim tPoint As POINT
            Call MoveToEx(hDC, xp1 - nDeviceLeft, yp1 - nDeviceTop, tPoint)
            Call LineTo(hDC, xp2 - nDeviceLeft, yp2 - nDeviceTop)


            If bArrow Then
                Dim xp3 As Double, yp3 As Double, xp4 As Double, yp4 As Double
                Dim xm As Double, ym As Double

                'P3(左矢印の先) と P4(右矢印の先) を求める
                xm = clsEndDevicePoint.X - (dR + nPLOT_VECTOR_ARROW_LENGTH) * L
                xp3 = xm - nPLOT_VECTOR_ARROW_WIDTH * M
                xp4 = xm + nPLOT_VECTOR_ARROW_WIDTH* M
                ym = clsEndDevicePoint.Y - (dR + nPLOT_VECTOR_ARROW_LENGTH) * M
                yp3 = ym + nPLOT_VECTOR_ARROW_WIDTH * L
                yp4 = ym - nPLOT_VECTOR_ARROW_WIDTH* L


                Call SelectObject(hDC, hPenArrow)
                Call MoveToEx(hDC, xp3 - nDeviceLeft, yp3 - nDeviceTop, tPoint)
                Call LineTo(hDC, xp2 - nDeviceLeft, yp2 - nDeviceTop)
                Call LineTo(hDC, xp4 - nDeviceLeft, yp4 - nDeviceTop)
            End If


        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '基線ベクトルを描画する。
        '
        '引き数：
        'hDC デバイスコンテキストのハンドル。
        'nDeviceLeft 論理エリア上のデバイス左辺位置(ピクセル)。
        'nDeviceTop 論理エリア上のデバイス上辺位置(ピクセル)。
        'nMapScale Twips→ピクセル、の係数。
        'clsBaseLineVector 基線ベクトル。
        'hPenLine 線のペン。
        'hPenArrow 矢印のペン。
        'bArrow 矢印描画の指定。True=描画する。False=描画しない。
        */
#if false
        public void DrawBaseLineVector(long hDC, double nDeviceLeft, double nDeviceTop, double nMapScale, BaseLineVector clsBaseLineVector, long hPenLine, long hPenArrow, bool bArrow)
        {
#else
        public Bitmap DrawBaseLineVector(Bitmap hDC, double nDeviceLeft, double nDeviceTop, double nMapScale, BaseLineVector clsBaseLineVector, long hPenLine, long hPenArrow, bool bArrow)
        {
            Bitmap btmp = hDC;
            int L_SOLID = (int)(hPenLine >> 56);
            int L_Width = (int)((hPenLine >> 32) & 0xFFFFFF);
            int L_RGB = (int)(hPenLine & 0xFFFFFF);
            int A_SOLID = (int)(hPenArrow >> 56);
            int A_Width = (int)((hPenArrow >> 32) & 0xFFFFFF);
            int A_RGB = (int)(hPenArrow & 0xFFFFFF);
#endif
            //'定数。
            double nPLOT_OBSPNT_RADIUS_FIX;
            double nPLOT_OBSPNT_RADIUS;
            double nPLOT_VECTOR_ARROW_LENGTH;
            double nPLOT_VECTOR_ARROW_WIDTH;
            nPLOT_OBSPNT_RADIUS_FIX = PLOT_OBSPNT_RADIUS_FIX * nMapScale;
            nPLOT_OBSPNT_RADIUS = PLOT_OBSPNT_RADIUS * nMapScale;
            nPLOT_VECTOR_ARROW_LENGTH = PLOT_VECTOR_ARROW_LENGTH * nMapScale;
            nPLOT_VECTOR_ARROW_WIDTH = PLOT_VECTOR_ARROW_WIDTH * nMapScale;

#if false
            _ = SelectObject((IntPtr)hDC, (int)hPenLine);
#endif

            //'方向余弦を求める
            TwipsPoint clsStrDevicePoint;
            TwipsPoint clsEndDevicePoint;
            clsStrDevicePoint = clsBaseLineVector.StrPoint().DevicePoint();
            clsEndDevicePoint = clsBaseLineVector.EndPoint().DevicePoint();
            double L = 0;
            double M = 0;
            if (!GetDirectionCosines(clsStrDevicePoint, clsEndDevicePoint, ref L, ref M))
            {
#if false
                //'始点と終点の座標が同じ場合、点を打っておく。
                _ = Ellipse((IntPtr)hDC, (int)(clsStrDevicePoint.X - nDeviceLeft), (int)(clsStrDevicePoint.Y - nDeviceTop), (int)(clsStrDevicePoint.X - nDeviceLeft + 1),
                    (int)(clsStrDevicePoint.Y - nDeviceTop + 1));
                return;
#else
                //１.Bitmapクラスを作成(大きさはPictureBoxのサイズ)
                //Bitmap btmp1 = hDC;
                //２.Graphicクラスを生成する(先ほどのBitmap(Imageオフジェクト)クラスを使う)
                Graphics grph1 = Graphics.FromImage(btmp);
                //３.Penオブジェクト生成(今回は白の太さ２)
                //Color Clr1 = Color.White;
                Color Clr1 = Color.FromArgb((int)(L_RGB | 0xFF000000));
                Pen pen1 = new Pen(Clr1, L_Width);
                //４.SmoothingModeを設定
                //(高品質で低速なレンダリング)を指定する
                grph1.SmoothingMode = SmoothingMode.HighQuality;
                //５.線を引く
                int sx;
                int sy;
                int ex;
                int ey;
                int esx;
                int esy;
                sx = (int)(clsStrDevicePoint.X - nDeviceLeft);
                sy = (int)(clsStrDevicePoint.Y - nDeviceTop);
                ex = (int)(clsStrDevicePoint.X - nDeviceLeft + 1);
                ey = (int)(clsStrDevicePoint.Y - nDeviceTop + 1);
                esx = ex - sx;
                esy = ey - sy;
                grph1.DrawEllipse(pen1, sx, sy, esx, esy);
                //６.解放する
                pen1.Dispose();
                grph1.Dispose();
                //７.表示を渡す
                return btmp;
#endif
            }


            double xp1 = 0;
            double yp1 = 0;
            double xp2 = 0;
            double yp2 = 0;
            if (clsBaseLineVector.StrPoint().TopParentPoint().Based())
            {
                //'中心
                xp1 = 0;
                yp1 = 0;
            }
            else if (clsBaseLineVector.StrPoint().TopParentPoint().Fixed())
            {
                //'P1(三角との交点)を求める
                GetTriangleIntersection(M, L, ref xp1, ref yp1, (long)nPLOT_OBSPNT_RADIUS_FIX);
            }
            else
            {
                //'P1(X1の円周点)を求める
                xp1 = nPLOT_OBSPNT_RADIUS * L;
                yp1 = nPLOT_OBSPNT_RADIUS * M;
            }
            xp1 += clsStrDevicePoint.X;
            yp1 += clsStrDevicePoint.Y;


            double dR = 0;
            if (clsBaseLineVector.EndPoint().TopParentPoint().Based())
            {
                //'中心
                xp2 = 0;
                yp2 = 0;
            }
            else if (clsBaseLineVector.EndPoint().TopParentPoint().Fixed())
            {
                //'P2(三角との交点)を求める
                GetTriangleIntersection(-M, -L, ref xp2, ref yp2, (long)nPLOT_OBSPNT_RADIUS_FIX);
                dR = Math.Sqrt((xp2 * xp2) + (yp2 * yp2));
            }
            else
            {
                //'P2(X2の円周点)を求める
                xp2 = nPLOT_OBSPNT_RADIUS * -L;
                yp2 = nPLOT_OBSPNT_RADIUS * -M;
                dR = nPLOT_OBSPNT_RADIUS;
            }
            xp2 += clsEndDevicePoint.X;
            yp2 += clsEndDevicePoint.Y;

#if false
            POINT tPoint = default;
            _ = MoveToEx((IntPtr)hDC, (int)(xp1 - nDeviceLeft), (int)(yp1 - nDeviceTop), ref tPoint);
            _ = LineTo((IntPtr)hDC, (int)(xp2 - nDeviceLeft), (int)(yp2 - nDeviceTop));
#else
            //１.Bitmapクラスを作成(大きさはPictureBoxのサイズ)
            //Bitmap btmp = hDC;
            //２.Graphicクラスを生成する(先ほどのBitmap(Imageオフジェクト)クラスを使う)
            Graphics grph = Graphics.FromImage(btmp);
            //３.Penオブジェクト生成(今回は緑の太さ１)
            //Color Clr = Color.LightGreen;
            Color Clr = Color.FromArgb((int)(L_RGB | 0xFF000000));
            Pen pen = new Pen(Clr, L_Width);
            //４.SmoothingModeを設定
            //(高品質で低速なレンダリング)を指定する
            grph.SmoothingMode = SmoothingMode.HighQuality;
            //５.線を引く
            grph.DrawLine(pen, new Point((int)(xp1 - nDeviceLeft), (int)(yp1 - nDeviceTop)), new Point((int)(xp2 - nDeviceLeft), (int)(yp2 - nDeviceTop)));
            //６.解放する
            pen.Dispose();
            //grph.Dispose();
            //７.表示を渡す
            //return btmp;
#endif

            if (bArrow)
            {
                double xp3, yp3, xp4, yp4;
                double xm, ym;


                //'P3(左矢印の先) と P4(右矢印の先) を求める
                xm = clsEndDevicePoint.X - (dR + nPLOT_VECTOR_ARROW_LENGTH) * L;
                xp3 = xm - nPLOT_VECTOR_ARROW_WIDTH * M;
                xp4 = xm + nPLOT_VECTOR_ARROW_WIDTH * M;
                ym = clsEndDevicePoint.Y - (dR + nPLOT_VECTOR_ARROW_LENGTH) * M;
                yp3 = ym + nPLOT_VECTOR_ARROW_WIDTH * L;
                yp4 = ym - nPLOT_VECTOR_ARROW_WIDTH * L;

#if false
                _ = SelectObject((IntPtr)hDC, (int)hPenArrow);
                _ = MoveToEx((IntPtr)hDC, (int)(xp3 - nDeviceLeft), (int)(yp3 - nDeviceTop), ref tPoint);
                _ = LineTo((IntPtr)hDC, (int)(xp2 - nDeviceLeft), (int)(yp2 - nDeviceTop));
                _ = LineTo((IntPtr)hDC, (int)(xp4 - nDeviceLeft), (int)(yp4 - nDeviceTop));
#else
                //１.Bitmapクラスを作成(大きさはPictureBoxのサイズ)
                //Bitmap btmp = (Bitmap)hDC;
                //２.Graphicクラスを生成する(先ほどのBitmap(Imageオフジェクト)クラスを使う)
                //Graphics grph = Graphics.FromImage(btmp);
                //３.Penオブジェクト生成(今回は白の太さ２)
                //Color Clr = Color.White;
                Color Clr2 = Color.FromArgb((int)(A_RGB | 0xFF000000));
                Pen pen2 = new Pen(Clr2, A_Width);
                //４.SmoothingModeを設定
                //(高品質で低速なレンダリング)を指定する
                //grph.SmoothingMode = SmoothingMode.HighQuality;
                //５.線を引く
                grph.DrawLine(pen2, new Point((int)(xp2 - nDeviceLeft), (int)(yp2 - nDeviceTop)), new Point((int)(xp3 - nDeviceLeft), (int)(yp3 - nDeviceTop)));
                grph.DrawLine(pen2, new Point((int)(xp2 - nDeviceLeft), (int)(yp2 - nDeviceTop)), new Point((int)(xp4 - nDeviceLeft), (int)(yp4 - nDeviceTop)));
                //６.解放する
                pen2.Dispose();
                //grph.Dispose();
                //７.表示を渡す
                //return btmp;
#endif
            }
#if false
            return;
#else
            //６.解放する
            //pen.Dispose();
            grph.Dispose();
            //７.表示を渡す
            return btmp;
#endif
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '方向余弦を求める。
        '
        '引き数：
        'clsStrDevicePoint 始点座標。
        'clsEndDevicePoint 終点座標。
        'L X方向(正弦)。
        'M Y方向(余弦)。
        '
        '戻り値：
        '正常終了の場合 True を返す。
        'それ以外の場合 False を返す。
        Public Function GetDirectionCosines(ByVal clsStrDevicePoint As TwipsPoint, ByVal clsEndDevicePoint As TwipsPoint, ByRef L As Double, ByRef M As Double) As Boolean

            GetDirectionCosines = False

            Dim DX As Double
            Dim DY As Double
            Dim dD As Double
            DX = clsEndDevicePoint.X - clsStrDevicePoint.X
            DY = clsEndDevicePoint.Y - clsStrDevicePoint.Y
            dD = Sqr((DX * DX) + (DY * DY))
            If dD<FLT_EPSILON Then Exit Function
            L = DX / dD
            M = DY / dD


            GetDirectionCosines = True


        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '方向余弦を求める。
        '
        '引き数：
        'clsStrDevicePoint 始点座標。
        'clsEndDevicePoint 終点座標。
        'L X方向(正弦)。
        'M Y方向(余弦)。
        '
        '戻り値：
        '正常終了の場合 True を返す。
        'それ以外の場合 False を返す。
        */
        public bool GetDirectionCosines(TwipsPoint clsStrDevicePoint, TwipsPoint clsEndDevicePoint, ref double L, ref double M)
        {
            double DX;
            double DY;
            double dD;
            DX = clsEndDevicePoint.X - clsStrDevicePoint.X;
            DY = clsEndDevicePoint.Y - clsStrDevicePoint.Y;
            dD = Math.Sqrt((DX * DX) + (DY * DY));
            if (dD < FLT_EPSILON)
            {
                return false;
            }
            L = DX / dD;
            M = DY / dD;

            return true;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '線分の始点終点座標を取得する。
        '
        '基線ベクトルと観測点の接点を求める。
        '固定点なら三角形との接点。
        'それ以外なら円との接点。
        '
        '引き数：
        'clsDevicePoint 中心座標(ピクセル)。
        'L ベクトルの向き。X方向(正弦)。
        'M ベクトルの向き。Y方向(余弦)。
        'nRadius 半径(ピクセル)。
        'bFix 固定点フラグ。
        'xp 求まった接点X(ピクセル)。
        'yp 求まった接点Y(ピクセル)。
        Public Sub GetSegmentPoint(ByVal clsDevicePoint As TwipsPoint, ByVal L As Double, ByVal M As Double, ByVal nRadius As Long, ByVal bFix As Boolean, ByRef xp As Double, ByRef yp As Double)
            If bFix Then
                '三角との交点を求める。
                Call GetTriangleIntersection(M, L, xp, yp, nRadius)
            Else
                '円周点を求める。
                xp = nRadius* L
                yp = nRadius* M
            End If
            xp = xp + clsDevicePoint.X
            yp = yp + clsDevicePoint.Y
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '線分の始点終点座標を取得する。
        '
        '基線ベクトルと観測点の接点を求める。
        '固定点なら三角形との接点。
        'それ以外なら円との接点。
        '
        '引き数：
        'clsDevicePoint 中心座標(ピクセル)。
        'L ベクトルの向き。X方向(正弦)。
        'M ベクトルの向き。Y方向(余弦)。
        'nRadius 半径(ピクセル)。
        'bFix 固定点フラグ。
        'xp 求まった接点X(ピクセル)。
        'yp 求まった接点Y(ピクセル)。
        */
        public void GetSegmentPoint(TwipsPoint clsDevicePoint, double L, double M, long nRadius, bool bFix, ref double xp, ref double yp)
        {
            if (bFix)
            {
                //'三角との交点を求める。
                GetTriangleIntersection(M, L, ref xp, ref yp, nRadius);
            }
            else
            {
                //'円周点を求める。
                xp = nRadius * L;
                yp = nRadius * M;
            }
            xp += clsDevicePoint.X;
            yp += clsDevicePoint.Y;
            return;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '三角との交点を求める。
        '
        '直線と三角形の交点。
        '
        '引き数：
        'nS ベクトルの向き。正弦。
        'nC ベクトルの向き。余弦。
        'xp 求まった接点X(ピクセル)。
        'yp 求まった接点Y(ピクセル)。
        'nRadius 半径(ピクセル)。正三角形に外接する円の半径。
        Public Sub GetTriangleIntersection(ByVal nS As Double, ByVal nC As Double, ByRef xp As Double, ByRef yp As Double, ByVal nRadius As Long)

            If Abs(nC) <= FLT_EPSILON Then
                xp = 0
                If nS< 0 Then
                    yp = MATH_S1 * nRadius
                Else
                    yp = MATH_S3 * nRadius
                End If
            Else
                Dim vA As Double
                Dim vC As Double
                vA = nS / nC
                vC = 0 - vA * 0


                If nS >= MATH_S3 Then
                    yp = MATH_S3 * nRadius
                    xp = (yp - vC) / vA
                ElseIf (nS >= 0 And nC <= MATH_C2) Or(nS <= 0 And nC <= 0) Then
                    xp = (MATH_S1 * nRadius - vC) / (vA + MATH_SQR3)
                    yp = -MATH_SQR3* xp + MATH_S1* nRadius
                ElseIf(nS <= 0 And nC >= 0) Or(nS >= 0 And nC >= MATH_C3) Then
                    xp = (MATH_S1 * nRadius - vC) / (vA - MATH_SQR3)
                    yp = MATH_SQR3* xp + MATH_S1* nRadius
                ElseIf nC > 0 Then
                    xp = MATH_C3 * nRadius
                    yp = MATH_S3* nRadius
                Else
                    xp = MATH_C2 * nRadius
                    yp = MATH_S2* nRadius
                End If
            End If


        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '三角との交点を求める。
        '
        '直線と三角形の交点。
        '
        '引き数：
        'nS ベクトルの向き。正弦。
        'nC ベクトルの向き。余弦。
        'xp 求まった接点X(ピクセル)。
        'yp 求まった接点Y(ピクセル)。
        'nRadius 半径(ピクセル)。正三角形に外接する円の半径。
        */
        public void GetTriangleIntersection(double nS, double nC, ref double xp, ref double yp, long nRadius)
        {
            if (Math.Abs(nC) <= FLT_EPSILON)
            {
                xp = 0;
                if (nS < 0)
                {
                    yp = MATH_S1 * nRadius;
                }
                else
                {
                    yp = MATH_S3 * nRadius;
                }
            }
            else
            {
                double vA;
                double vC;
                vA = nS / nC;
                vC = 0 - vA * 0;


                if (nS >= MATH_S3)
                {
                    yp = MATH_S3 * nRadius;
                    xp = (yp - vC) / vA;
                }
                else if ((nS >= 0 && nC <= MATH_C2) || (nS <= 0 && nC <= 0))
                {
                    xp = (MATH_S1 * nRadius - vC) / (vA + MATH_SQR3);
                    yp = ((0 - MATH_SQR3) * xp) + (MATH_S1 * nRadius);
                }
                else if ((nS <= 0 && nC >= 0) || (nS >= 0 && nC >= MATH_C3))
                {
                    xp = (MATH_S1 * nRadius - vC) / (vA - MATH_SQR3);
                    yp = MATH_SQR3 * xp + MATH_S1 * nRadius;
                }
                else if (nC > 0)
                {
                    xp = MATH_C3 * nRadius;
                    yp = MATH_S3 * nRadius;
                }
                else
                {
                    xp = MATH_C2 * nRadius;
                    yp = MATH_S2 * nRadius;
                }
            }
            return;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '観測点を描画する。
        '
        'nClip～は記号を描画するためのクリップ領域。
        '記号はクリップ領域をはみ出さないように描画する。
        '記号を描画したエリアを nX、nY、nW、nH に設定する。
        '
        '引き数：
        'hDC デバイスコンテキストのハンドル。
        'nDeviceLeft 論理エリア上のデバイス左辺位置(ピクセル)。
        'nDeviceTop 論理エリア上のデバイス上辺位置(ピクセル)。
        'nClipLeft 論理エリア上のクリップ領域左辺(ピクセル)。
        'nClipTop 論理エリア上のクリップ領域上辺(ピクセル)。
        'nClipWidth 論理エリア上のクリップ領域幅(ピクセル)。
        'nClipHeight 論理エリア上のクリップ領域高さ(ピクセル)。
        'nMapScale Twips→ピクセル、の係数。
        'clsObservationPoint 観測点。
        'nPen ペン。
        'hBrush 塗り潰しブラシ。
        'nSymbol 記号の色。
        'nX 記号を描画したエリア、左辺(ピクセル)。
        'nY 記号を描画したエリア、上辺(ピクセル)。
        'nW 記号を描画したエリア、幅(ピクセル)。
        'nH 記号を描画したエリア、高さ(ピクセル)。
        Public Sub DrawObservationPoint(ByVal hDC As Long, ByVal nDeviceLeft As Double, ByVal nDeviceTop As Double, ByVal nClipLeft As Double, ByVal nClipTop As Double, ByVal nClipWidth As Double, ByVal nClipHeight As Double, ByVal nMapScale As Double, ByVal clsObservationPoint As ObservationPoint, ByVal nPen As Long, ByVal hBrush As Long, ByVal nSymbol As Long, PlotPointLabel As Long, Optional ByRef nX As Double, Optional ByRef nY As Double, Optional ByRef nW As Double, Optional ByRef nH As Double)
            '描画。
            If clsObservationPoint.Based Then
                Call DrawObservationPointBase(hDC, nDeviceLeft, nDeviceTop, clsObservationPoint.DevicePoint, nPen, PLOT_OBSPNT_RADIUS_BASE* nMapScale)
                ElseIf clsObservationPoint.Fixed Then
                Call DrawObservationPointFix(hDC, nDeviceLeft, nDeviceTop, clsObservationPoint.DevicePoint, nPen, PLOT_OBSPNT_RADIUS_FIX* nMapScale)
            Else
                Call DrawObservationPointRover(hDC, nDeviceLeft, nDeviceTop, clsObservationPoint.DevicePoint, nPen, hBrush, PLOT_OBSPNT_RADIUS* nMapScale)
            End If

            '記号。
            'オプションの「観測点のラベル」に合わせて表示項目を変更 2006/10/6 NGS Yamada
            Dim strPlotPointLabel As String
            If PlotPointLabel = 0 Then
                strPlotPointLabel = clsObservationPoint.Number
            ElseIf PlotPointLabel = 1 Then
                strPlotPointLabel = clsObservationPoint.Name
            Else
                strPlotPointLabel = clsObservationPoint.Number & "(" & clsObservationPoint.Name & ")"
            End If
            Call DrawObservationPointSymbol(hDC, nDeviceLeft, nDeviceTop, nClipLeft, nClipTop, nClipWidth, nClipHeight, clsObservationPoint.DevicePoint, strPlotPointLabel, nSymbol, PLOT_OBSPNT_SYMBOL_SPACE* nMapScale, nX, nY, nW, nH)
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '観測点を描画する。
        '
        'nClip～は記号を描画するためのクリップ領域。
        '記号はクリップ領域をはみ出さないように描画する。
        '記号を描画したエリアを nX、nY、nW、nH に設定する。
        '
        '引き数：
        'hDC デバイスコンテキストのハンドル。
        'nDeviceLeft 論理エリア上のデバイス左辺位置(ピクセル)。
        'nDeviceTop 論理エリア上のデバイス上辺位置(ピクセル)。
        'nClipLeft 論理エリア上のクリップ領域左辺(ピクセル)。
        'nClipTop 論理エリア上のクリップ領域上辺(ピクセル)。
        'nClipWidth 論理エリア上のクリップ領域幅(ピクセル)。
        'nClipHeight 論理エリア上のクリップ領域高さ(ピクセル)。
        'nMapScale Twips→ピクセル、の係数。
        'clsObservationPoint 観測点。
        'nPen ペン。
        'hBrush 塗り潰しブラシ。
        'nSymbol 記号の色。
        'nX 記号を描画したエリア、左辺(ピクセル)。
        'nY 記号を描画したエリア、上辺(ピクセル)。
        'nW 記号を描画したエリア、幅(ピクセル)。
        'nH 記号を描画したエリア、高さ(ピクセル)。
        */
#if false
        public void DrawObservationPoint(long hDC, double nDeviceLeft, double nDeviceTop, double nClipLeft, double nClipTop, double nClipWidth, double nClipHeight, double nMapScale,
                                ObservationPoint clsObservationPoint, long nPen, long hBrush, long nSymbol, long PlotPointLabel, ref double nX, ref double nY, ref double nW, ref double nH)
        {
#else
        public Bitmap DrawObservationPoint(Bitmap hDC, double nDeviceLeft, double nDeviceTop, double nClipLeft, double nClipTop, double nClipWidth, double nClipHeight, double nMapScale,
                                ObservationPoint clsObservationPoint, long nPen, long hBrush, long nSymbol, long PlotPointLabel, ref double nX, ref double nY, ref double nW, ref double nH)
        {
            Bitmap btmp = hDC;
#endif
#if false
            //'描画。
            if (clsObservationPoint.Based())
            {
                DrawObservationPointBase(hDC, nDeviceLeft, nDeviceTop, clsObservationPoint.DevicePoint(), nPen, (float)(PLOT_OBSPNT_RADIUS_BASE * nMapScale));
            }
            else if (clsObservationPoint.Fixed())
            {
                DrawObservationPointFix(hDC, nDeviceLeft, nDeviceTop, clsObservationPoint.DevicePoint(), nPen, (float)(PLOT_OBSPNT_RADIUS_FIX * nMapScale));
            }
            else
            {
                DrawObservationPointRover(hDC, nDeviceLeft, nDeviceTop, clsObservationPoint.DevicePoint(), nPen, hBrush, (float)(PLOT_OBSPNT_RADIUS * nMapScale));
            }
#else
            //'描画。
            if (clsObservationPoint.Based())
            {
                btmp = DrawObservationPointBase(btmp, nDeviceLeft, nDeviceTop, clsObservationPoint.DevicePoint(), nPen, (float)(PLOT_OBSPNT_RADIUS_BASE * nMapScale));
            }
            else if (clsObservationPoint.Fixed())
            {
                btmp = DrawObservationPointFix(btmp, nDeviceLeft, nDeviceTop, clsObservationPoint.DevicePoint(), nPen, (float)(PLOT_OBSPNT_RADIUS_FIX * nMapScale));
            }
            else
            {
                btmp = DrawObservationPointRover(btmp, nDeviceLeft, nDeviceTop, clsObservationPoint.DevicePoint(), nPen, hBrush, (float)(PLOT_OBSPNT_RADIUS * nMapScale));
            }
#endif

            //'記号。
            //'オプションの「観測点のラベル」に合わせて表示項目を変更 2006/10/6 NGS Yamada
            string strPlotPointLabel;
            if (PlotPointLabel == 0)
            {
                strPlotPointLabel = clsObservationPoint.Number();
            }
            else if (PlotPointLabel == 1)
            {
                strPlotPointLabel = clsObservationPoint.Name();
            }
            else
            {
                strPlotPointLabel = clsObservationPoint.Number() + "(" + clsObservationPoint.Name() + ")";
            }
#if false
            DrawObservationPointSymbol(hDC, nDeviceLeft, nDeviceTop, nClipLeft, nClipTop, nClipWidth, nClipHeight, clsObservationPoint.DevicePoint(), strPlotPointLabel,
                nSymbol, (long)(PLOT_OBSPNT_SYMBOL_SPACE * nMapScale), ref nX, ref nY, ref nW, ref nH);
            return;
#else
            btmp = DrawObservationPointSymbol(btmp, nDeviceLeft, nDeviceTop, nClipLeft, nClipTop, nClipWidth, nClipHeight, clsObservationPoint.DevicePoint(), strPlotPointLabel,
                nSymbol, (long)(PLOT_OBSPNT_SYMBOL_SPACE * nMapScale), ref nX, ref nY, ref nW, ref nH);
            return btmp;
#endif
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '移動点を描画する。
        '
        '円を描画する。
        '
        '引き数：
        'hDC デバイスコンテキストのハンドル。
        'nDeviceLeft 論理エリア上のデバイス左辺位置(ピクセル)。
        'nDeviceTop 論理エリア上のデバイス上辺位置(ピクセル)。
        'clsDevicePoint 論理エリア上の観測点の座標(ピクセル)。円の中心座標。
        'nPen ペン。
        'hBrush 塗り潰しブラシ。
        'nRadius 半径(ピクセル)。
        Public Sub DrawObservationPointRover(ByVal hDC As Long, ByVal nDeviceLeft As Double, ByVal nDeviceTop As Double, ByVal clsDevicePoint As TwipsPoint, ByVal hPen As Long, ByVal hBrush As Long, ByVal nRadius As Single)
            Call SelectObject(hDC, hPen)
            Call SelectObject(hDC, hBrush)
            Call Ellipse(hDC, clsDevicePoint.X - nRadius - nDeviceLeft, clsDevicePoint.Y - nRadius - nDeviceTop, clsDevicePoint.X + nRadius - nDeviceLeft + 1, clsDevicePoint.Y + nRadius - nDeviceTop + 1)
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '移動点を描画する。
        '
        '円を描画する。
        '
        '引き数：
        'hDC デバイスコンテキストのハンドル。
        'nDeviceLeft 論理エリア上のデバイス左辺位置(ピクセル)。
        'nDeviceTop 論理エリア上のデバイス上辺位置(ピクセル)。
        'clsDevicePoint 論理エリア上の観測点の座標(ピクセル)。円の中心座標。
        'nPen ペン。
        'hBrush 塗り潰しブラシ。
        'nRadius 半径(ピクセル)。
        */
#if false
        public void DrawObservationPointRover(long hDC, double nDeviceLeft, double nDeviceTop, TwipsPoint clsDevicePoint, long hPen, long hBrush, float nRadius)
#else
        public Bitmap DrawObservationPointRover(Bitmap hDC, double nDeviceLeft, double nDeviceTop, TwipsPoint clsDevicePoint, long hPen, long hBrush, float nRadius)
#endif
        {
            int L_SOLID = (int)(hPen >> 56);
            int L_Width = (int)((hPen >> 32) & 0xFFFFFF);
            int L_RGB = (int)(hPen & 0xFFFFFF);
#if false
            _ = SelectObject((IntPtr)hDC, (int)hPen);
            _ = SelectObject((IntPtr)hDC, (int)hBrush);
            _ = Ellipse((IntPtr)hDC, (int)(clsDevicePoint.X - nRadius - nDeviceLeft), (int)(clsDevicePoint.Y - nRadius - nDeviceTop),
                        (int)(clsDevicePoint.X + nRadius - nDeviceLeft + 1), (int)(clsDevicePoint.Y + nRadius - nDeviceTop + 1));
            return;
#else
            //１.Bitmapクラスを作成(大きさはPictureBoxのサイズ)
            Bitmap btmp = hDC;
            //２.Graphicクラスを生成する(先ほどのBitmap(Imageオフジェクト)クラスを使う)
            Graphics grph = Graphics.FromImage(btmp);
            //３.Penオブジェクト生成(今回は白の太さ１)
            //Color Clr = Color.White;
            Color Clr = Color.FromArgb((int)(L_RGB | 0xFF000000));
            Pen pen = new Pen(Clr, L_Width);
            //４.SmoothingModeを設定
            //(高品質で低速なレンダリング)を指定する
            grph.SmoothingMode = SmoothingMode.HighQuality;
            //５.線を引く
            int sx;
            int sy;
            int ex;
            int ey;
            int esx;
            int esy;
            sx = (int)(clsDevicePoint.X - nRadius - nDeviceLeft);
            sy = (int)(clsDevicePoint.Y - nRadius - nDeviceTop);
            ex = (int)(clsDevicePoint.X + nRadius - nDeviceLeft + 1);
            ey = (int)(clsDevicePoint.Y + nRadius - nDeviceTop + 1);
            esx = ex - sx;
            esy = ey - sy;
            grph.DrawEllipse(pen, sx, sy, esx, esy);
            //６.解放する
            pen.Dispose();
            grph.Dispose();
            //７.表示を渡す
            return btmp;
#endif
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '固定点を描画する。
        '
        '三角形を描画する。
        '
        '引き数：
        'hDC デバイスコンテキストのハンドル。
        'nDeviceLeft 論理エリア上のデバイス左辺位置(ピクセル)。
        'nDeviceTop 論理エリア上のデバイス上辺位置(ピクセル)。
        'clsDevicePoint 論理エリア上の観測点の座標(ピクセル)。三角形の中心座標。
        'nPen ペン。
        'nRadius 半径(ピクセル)。正三角形に外接する円の半径。
        Public Sub DrawObservationPointFix(ByVal hDC As Long, ByVal nDeviceLeft As Double, ByVal nDeviceTop As Double, ByVal clsDevicePoint As TwipsPoint, ByVal hPen As Long, ByVal nRadius As Single)
            Call SelectObject(hDC, hPen)
            Dim tPoint As POINT
            Call MoveToEx(hDC, MATH_C1* nRadius + clsDevicePoint.X - nDeviceLeft, MATH_S1* nRadius + clsDevicePoint.Y - nDeviceTop, tPoint)
            Call LineTo(hDC, MATH_C2* nRadius + clsDevicePoint.X - nDeviceLeft, MATH_S2* nRadius + clsDevicePoint.Y - nDeviceTop)
            Call LineTo(hDC, MATH_C3* nRadius + clsDevicePoint.X - nDeviceLeft, MATH_S3* nRadius + clsDevicePoint.Y - nDeviceTop)
            Call LineTo(hDC, MATH_C1* nRadius + clsDevicePoint.X - nDeviceLeft, MATH_S1* nRadius + clsDevicePoint.Y - nDeviceTop)
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '固定点を描画する。
        '
        '三角形を描画する。
        '
        '引き数：
        'hDC デバイスコンテキストのハンドル。
        'nDeviceLeft 論理エリア上のデバイス左辺位置(ピクセル)。
        'nDeviceTop 論理エリア上のデバイス上辺位置(ピクセル)。
        'clsDevicePoint 論理エリア上の観測点の座標(ピクセル)。三角形の中心座標。
        'nPen ペン。
        'nRadius 半径(ピクセル)。正三角形に外接する円の半径。
        */
#if false
        public void DrawObservationPointFix(long hDC, double nDeviceLeft, double nDeviceTop, TwipsPoint clsDevicePoint, long hPen, float nRadius)
        {
#else
        public Bitmap DrawObservationPointFix(Bitmap hDC, double nDeviceLeft, double nDeviceTop, TwipsPoint clsDevicePoint, long hPen, float nRadius)
        {
            Bitmap btmp = hDC;
            int L_SOLID = (int)(hPen >> 56);
            int L_Width = (int)((hPen >> 32) & 0xFFFFFF);
            int L_RGB = (int)(hPen & 0xFFFFFF);
#endif
#if false
            _ = SelectObject((IntPtr)hDC, (int)hPen);
            POINT tPoint = default;
            _ = MoveToEx((IntPtr)hDC, (int)(MATH_C1 * nRadius + clsDevicePoint.X - nDeviceLeft), (int)(MATH_S1 * nRadius + clsDevicePoint.Y - nDeviceTop), ref tPoint);
            _ = LineTo((IntPtr)hDC, (int)(MATH_C2 * nRadius + clsDevicePoint.X - nDeviceLeft), (int)(MATH_S2 * nRadius + clsDevicePoint.Y - nDeviceTop));
            _ = LineTo((IntPtr)hDC, (int)(MATH_C3 * nRadius + clsDevicePoint.X - nDeviceLeft), (int)(MATH_S3 * nRadius + clsDevicePoint.Y - nDeviceTop));
            _ = LineTo((IntPtr)hDC, (int)(MATH_C1 * nRadius + clsDevicePoint.X - nDeviceLeft), (int)(MATH_S1 * nRadius + clsDevicePoint.Y - nDeviceTop));
            return;
            Call MoveToEx(hDC, MATH_C1 * nRadius + clsDevicePoint.X - nDeviceLeft, MATH_S1 * nRadius + clsDevicePoint.Y - nDeviceTop, tPoint)
            Call LineTo(hDC, MATH_C2 * nRadius + clsDevicePoint.X - nDeviceLeft, MATH_S2 * nRadius + clsDevicePoint.Y - nDeviceTop)
            Call LineTo(hDC, MATH_C3 * nRadius + clsDevicePoint.X - nDeviceLeft, MATH_S3 * nRadius + clsDevicePoint.Y - nDeviceTop)
            Call LineTo(hDC, MATH_C1 * nRadius + clsDevicePoint.X - nDeviceLeft, MATH_S1 * nRadius + clsDevicePoint.Y - nDeviceTop)
#else
            //１.Bitmapクラスを作成(大きさはPictureBoxのサイズ)
            //Bitmap btmp = (Bitmap)hDC;
            //２.Graphicクラスを生成する(先ほどのBitmap(Imageオフジェクト)クラスを使う)
            Graphics grph = Graphics.FromImage(btmp);
            //３.Penオブジェクト生成(今回は白の太さ２)
            //Color Clr = Color.White;
            //Pen pen = new Pen(Clr, 1);
            Color Clr = Color.FromArgb((int)(L_RGB | 0xFF000000));
            Pen pen = new Pen(Clr, L_Width);
            //４.SmoothingModeを設定
            //(高品質で低速なレンダリング)を指定する
            grph.SmoothingMode = SmoothingMode.HighQuality;
            //５.線を引く
            grph.DrawLine(pen, new Point((int)(MATH_C1 * nRadius + clsDevicePoint.X - nDeviceLeft), (int)(MATH_S1 * nRadius + clsDevicePoint.Y - nDeviceTop)),
                                new Point((int)(MATH_C2 * nRadius + clsDevicePoint.X - nDeviceLeft), (int)(MATH_S2 * nRadius + clsDevicePoint.Y - nDeviceTop)));
            grph.DrawLine(pen, new Point((int)(MATH_C2 * nRadius + clsDevicePoint.X - nDeviceLeft), (int)(MATH_S2 * nRadius + clsDevicePoint.Y - nDeviceTop)),
                                new Point((int)(MATH_C3 * nRadius + clsDevicePoint.X - nDeviceLeft), (int)(MATH_S3 * nRadius + clsDevicePoint.Y - nDeviceTop)));
            grph.DrawLine(pen, new Point((int)(MATH_C3 * nRadius + clsDevicePoint.X - nDeviceLeft), (int)(MATH_S3 * nRadius + clsDevicePoint.Y - nDeviceTop)),
                                new Point((int)(MATH_C1 * nRadius + clsDevicePoint.X - nDeviceLeft), (int)(MATH_S1 * nRadius + clsDevicePoint.Y - nDeviceTop)));
            //６.解放する
            pen.Dispose();
            grph.Dispose();
            //７.表示を渡す
            return btmp;
#endif
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '基準点を描画する。
        '
        '×を描画する。
        '
        '引き数：
        'hDC デバイスコンテキストのハンドル。
        'nDeviceLeft 論理エリア上のデバイス左辺位置(ピクセル)。
        'nDeviceTop 論理エリア上のデバイス上辺位置(ピクセル)。
        'clsDevicePoint 論理エリア上の観測点の座標(ピクセル)。×の中心座標。
        'nPen ペン。
        'nRadius 半径(ピクセル)。×に外接する正方形の大きさ(の半分)。
        Public Sub DrawObservationPointBase(ByVal hDC As Long, ByVal nDeviceLeft As Double, ByVal nDeviceTop As Double, ByVal clsDevicePoint As TwipsPoint, ByVal hPen As Long, ByVal nRadius As Single)
            Dim nSX As Double
            Dim nEX As Double
            Dim nSY As Double
            Dim nEY As Double
            nSX = clsDevicePoint.X - nRadius - nDeviceLeft
            nEX = clsDevicePoint.X + nRadius - nDeviceLeft
            nSY = clsDevicePoint.Y - nRadius - nDeviceTop
            nEY = clsDevicePoint.Y + nRadius - nDeviceTop


            Call SelectObject(hDC, hPen)
            Dim tPoint As POINT
            Call MoveToEx(hDC, nSX, nSY, tPoint)
            Call LineTo(hDC, nEX, nEY)
            Call MoveToEx(hDC, nSX, nEY, tPoint)
            Call LineTo(hDC, nEX, nSY)
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '基準点を描画する。
        '
        '×を描画する。
        '
        '引き数：
        'hDC デバイスコンテキストのハンドル。
        'nDeviceLeft 論理エリア上のデバイス左辺位置(ピクセル)。
        'nDeviceTop 論理エリア上のデバイス上辺位置(ピクセル)。
        'clsDevicePoint 論理エリア上の観測点の座標(ピクセル)。×の中心座標。
        'nPen ペン。
        'nRadius 半径(ピクセル)。×に外接する正方形の大きさ(の半分)。
        */
#if false
        public void DrawObservationPointBase(long hDC, double nDeviceLeft, double nDeviceTop, TwipsPoint clsDevicePoint, long hPen, float nRadius)
        {
#else
        public Bitmap DrawObservationPointBase(Bitmap hDC, double nDeviceLeft, double nDeviceTop, TwipsPoint clsDevicePoint, long hPen, float nRadius)
        {
            Bitmap btmp = hDC;
            int L_SOLID = (int)(hPen >> 56);
            int L_Width = (int)((hPen >> 32) & 0xFFFFFF);
            int L_RGB = (int)(hPen & 0xFFFFFF);
#endif
            double nSX;
            double nEX;
            double nSY;
            double nEY;
            nSX = clsDevicePoint.X - nRadius - nDeviceLeft;
            nEX = clsDevicePoint.X + nRadius - nDeviceLeft;
            nSY = clsDevicePoint.Y - nRadius - nDeviceTop;
            nEY = clsDevicePoint.Y + nRadius - nDeviceTop;

#if false
            _ = SelectObject((IntPtr)hDC, (int)hPen);
            POINT tPoint = default;
            _ = MoveToEx((IntPtr)hDC, (int)nSX, (int)nSY, ref tPoint);
            _ = LineTo((IntPtr)hDC, (int)nEX, (int)nEY);
            _ = MoveToEx((IntPtr)hDC, (int)nSX, (int)nEY, ref tPoint);
            _ = LineTo((IntPtr)hDC, (int)nEX, (int)nSY);
            return;
#else
            //１.Bitmapクラスを作成(大きさはPictureBoxのサイズ)
            //Bitmap btmp = (Bitmap)hDC;
            //２.Graphicクラスを生成する(先ほどのBitmap(Imageオフジェクト)クラスを使う)
            Graphics grph = Graphics.FromImage(btmp);
            //３.Penオブジェクト生成(今回は白の太さ２)
            //Color Clr = Color.White;
            //Pen pen = new Pen(Clr, 1);
            Color Clr = Color.FromArgb((int)(L_RGB | 0xFF000000));
            Pen pen = new Pen(Clr, L_Width);
            //４.SmoothingModeを設定
            //(高品質で低速なレンダリング)を指定する
            grph.SmoothingMode = SmoothingMode.HighQuality;
            //５.線を引く
            grph.DrawLine(pen, new Point((int)nSX, (int)nSY), new Point((int)nEX, (int)nEY));
            grph.DrawLine(pen, new Point((int)nSX, (int)nSY), new Point((int)nEX, (int)nSY));
            //６.解放する
            pen.Dispose();
            grph.Dispose();
            //７.表示を渡す
            return btmp;
#endif
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '観測点の記号を描画する。
        '
        'nClip～は記号を描画するためのクリップ領域。
        '記号はクリップ領域をはみ出さないように描画する。
        '記号を描画したエリアを nX、nY、nW、nH に設定する。
        '
        '引き数：
        'hDC デバイスコンテキストのハンドル。
        'nDeviceLeft 論理エリア上のデバイス左辺位置(ピクセル)。
        'nDeviceTop 論理エリア上のデバイス上辺位置(ピクセル)。
        'nClipLeft 論理エリア上のクリップ領域左辺(ピクセル)。
        'nClipTop 論理エリア上のクリップ領域上辺(ピクセル)。
        'nClipWidth 論理エリア上のクリップ領域幅(ピクセル)。
        'nClipHeight 論理エリア上のクリップ領域高さ(ピクセル)。
        'clsDevicePoint 論理エリア上の観測点の座標(ピクセル)。中心座標。
        'sSymbol 記号。
        'nSymbol 記号の色。
        'nSpace 観測点の中心から記号上辺までの距離(ピクセル)。
        'nX 記号を描画したエリア、左辺(ピクセル)。
        'nY 記号を描画したエリア、上辺(ピクセル)。
        'nW 記号を描画したエリア、幅(ピクセル)。
        'nH 記号を描画したエリア、高さ(ピクセル)。
        Public Sub DrawObservationPointSymbol(ByVal hDC As Long, ByVal nDeviceLeft As Double, ByVal nDeviceTop As Double, ByVal nClipLeft As Double, ByVal nClipTop As Double, ByVal nClipWidth As Double, ByVal nClipHeight As Double, ByVal clsDevicePoint As TwipsPoint, ByVal sSymbol As String, ByVal nSymbol As Long, ByVal nSpace As Long, Optional ByRef nX As Double, Optional ByRef nY As Double, Optional ByRef nW As Double, Optional ByRef nH As Double)
            Call SetTextColor(hDC, nSymbol)
            Dim tRect As RECT
            Call DrawText(hDC, sSymbol, -1, tRect, DT_CALCRECT Or DT_NOPREFIX Or DT_SINGLELINE)


            nW = tRect.Right - tRect.Left
            nH = tRect.Bottom - tRect.Top
            nX = clsDevicePoint.X - nW* 0.5
            nY = clsDevicePoint.Y + nSpace
            If nClipLeft <= clsDevicePoint.X And nClipTop <= clsDevicePoint.Y And clsDevicePoint.X<nClipLeft + nClipWidth And clsDevicePoint.Y<nClipTop + nClipHeight Then
                '画面からはみ出さないように補正。
                If nX + nW> nClipLeft + nClipWidth Then nX = nClipLeft + nClipWidth - nW
                If nX<nClipLeft Then nX = nClipLeft
                If nY + nH> nClipTop + nClipHeight Then nY = clsDevicePoint.Y - nSpace - nH
                If nY<nClipTop Then nY = nClipTop
            End If


            tRect.Left = nX - nDeviceLeft
            tRect.Top = nY - nDeviceTop
            tRect.Right = tRect.Left + nW
            tRect.Bottom = tRect.Top + nH
            Call DrawText(hDC, sSymbol, -1, tRect, DT_NOCLIP Or DT_RIGHT Or DT_NOPREFIX Or DT_SINGLELINE Or DT_VCENTER)
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '観測点の記号を描画する。
        '
        'nClip～は記号を描画するためのクリップ領域。
        '記号はクリップ領域をはみ出さないように描画する。
        '記号を描画したエリアを nX、nY、nW、nH に設定する。
        '
        '引き数：
        'hDC デバイスコンテキストのハンドル。
        'nDeviceLeft 論理エリア上のデバイス左辺位置(ピクセル)。
        'nDeviceTop 論理エリア上のデバイス上辺位置(ピクセル)。
        'nClipLeft 論理エリア上のクリップ領域左辺(ピクセル)。
        'nClipTop 論理エリア上のクリップ領域上辺(ピクセル)。
        'nClipWidth 論理エリア上のクリップ領域幅(ピクセル)。
        'nClipHeight 論理エリア上のクリップ領域高さ(ピクセル)。
        'clsDevicePoint 論理エリア上の観測点の座標(ピクセル)。中心座標。
        'sSymbol 記号。
        'nSymbol 記号の色。
        'nSpace 観測点の中心から記号上辺までの距離(ピクセル)。
        'nX 記号を描画したエリア、左辺(ピクセル)。
        'nY 記号を描画したエリア、上辺(ピクセル)。
        'nW 記号を描画したエリア、幅(ピクセル)。
        'nH 記号を描画したエリア、高さ(ピクセル)。
        */
#if false
        public void DrawObservationPointSymbol(long hDC, double nDeviceLeft, double nDeviceTop, double nClipLeft, double nClipTop, double nClipWidth, double nClipHeight,
                        TwipsPoint clsDevicePoint,string sSymbol, long nSymbol, long nSpace, ref double nX, ref double nY, ref double nW, ref double nH)
        {
#else
        public Bitmap DrawObservationPointSymbol(Bitmap hDC, double nDeviceLeft, double nDeviceTop, double nClipLeft, double nClipTop, double nClipWidth, double nClipHeight,
                        TwipsPoint clsDevicePoint, string sSymbol, long nSymbol, long nSpace, ref double nX, ref double nY, ref double nW, ref double nH)
        {
            Bitmap btmp = hDC;
            int L_SOLID = (int)(nSymbol >> 56);
            int L_Width = (int)((nSymbol >> 32) & 0xFFFFFF);
            int L_RGB = (int)(nSymbol & 0xFFFFFF);
#endif
#if false
            _ = SetTextColor((IntPtr)hDC, (int)nSymbol);
            RECT tRect = default;
            _ = DrawText((IntPtr)hDC, sSymbol, -1, ref tRect, (int)(DT_CALCRECT | DT_NOPREFIX | DT_SINGLELINE));
#else
            RECT tRect = default;
            //１.Bitmapクラスを作成(大きさはPictureBoxのサイズ)
            //Bitmap btmp = (Bitmap)hDC;
            //２.Graphicクラスを生成する(先ほどのBitmap(Imageオフジェクト)クラスを使う)
            Graphics grph = Graphics.FromImage(btmp);
            //３.Penオブジェクト生成(今回は白の太さ２)
            //Color Clr = Color.White;
            //Pen pen = new Pen(Clr, 2);
            //４.SmoothingModeを設定
            //(高品質で低速なレンダリング)を指定する
            grph.SmoothingMode = SmoothingMode.HighQuality;
            //５.線を引く
            //６.文字を書く
            Font fnt = new Font("Arial", 9, FontStyle.Regular);
            //grph.DrawString(sSymbol, fnt, Brushes.Red, tRect.Left, tRect.Top);
            //grph.DrawString(sSymbol, fnt, new SolidBrush(Color.FromArgb((int)(L_RGB | 0xFF000000))), tRect.Left, tRect.Top);
            //７.解放する
            //pen.Dispose();
            //grph.Dispose();
            //８.表示を渡す
            //return btmp;
#endif
            nW = tRect.Right - tRect.Left;
            nH = tRect.Bottom - tRect.Top;
            nX = clsDevicePoint.X - nW * 0.5;
            nY = clsDevicePoint.Y + nSpace;
            if (nClipLeft <= clsDevicePoint.X && nClipTop <= clsDevicePoint.Y && clsDevicePoint.X < nClipLeft + nClipWidth && clsDevicePoint.Y < nClipTop + nClipHeight)
            {
                //'画面からはみ出さないように補正。
                if (nX + nW > nClipLeft + nClipWidth)
                {
                    nX = nClipLeft + nClipWidth - nW;
                }
                if (nX < nClipLeft)
                {
                    nX = nClipLeft;
                }
                if (nY + nH > nClipTop + nClipHeight)
                {
                    nY = clsDevicePoint.Y - nSpace - nH;
                }
                if (nY < nClipTop)
                {
                    nY = nClipTop;
                }
            }

#if false
            tRect.Left = (int)(nX - nDeviceLeft);
            tRect.Top = (int)(nY - nDeviceTop);
            tRect.Right = (int)(tRect.Left + nW);
            tRect.Bottom = (int)(tRect.Top + nH);
            _ = DrawText((IntPtr)hDC, sSymbol, -1, ref tRect, (int)(DT_NOCLIP | DT_RIGHT | DT_NOPREFIX | DT_SINGLELINE | DT_VCENTER));
            return;
#else
            tRect.Left = (int)(nX - nDeviceLeft);
            tRect.Top = (int)(nY - nDeviceTop);
            tRect.Right = (int)(tRect.Left + nW);
            tRect.Bottom = (int)(tRect.Top + nH);
            //１.Bitmapクラスを作成(大きさはPictureBoxのサイズ)
            //Bitmap btmp = (Bitmap)hDC;
            //２.Graphicクラスを生成する(先ほどのBitmap(Imageオフジェクト)クラスを使う)
            //Graphics grph = Graphics.FromImage(btmp);
            //３.Penオブジェクト生成(今回は白の太さ２)
            //Color Clr = Color.White;
            //Pen pen = new Pen(Clr, 2);
            //４.SmoothingModeを設定
            //(高品質で低速なレンダリング)を指定する
            grph.SmoothingMode = SmoothingMode.HighQuality;
            //５.線を引く
            //６.文字を書く
            Font fnt1 = new Font("Arial", 9, FontStyle.Regular);
            grph.DrawString(sSymbol, fnt1, new SolidBrush(Color.FromArgb((int)(L_RGB | 0xFF000000))), tRect.Left, tRect.Top);
            //７.解放する
            //pen.Dispose();
            grph.Dispose();
            //８.表示を渡す
            return btmp;
#endif
        }

        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'ノースアローを描画する。
        '
        'DX、DY は論理座標ではなくデバイス座標。
        '
        '引き数：
        'hDC デバイスコンテキストのハンドル。
        'DX 描画位置左端(デバイス座標、ピクセル)。
        'DY 描画位置上端(デバイス座標、ピクセル)。
        'nMapScale Twips→ピクセル、の係数。
        'hPen ペン。
        'lColorN 記号色。
        Public Sub DrawNorthArrow(ByVal hDC As Long, ByVal DX As Double, ByVal DY As Double, ByVal nMapScale As Double, ByVal hPen As Long, ByVal lColorN As Long)

            '定数。
            Dim C1000 As Long
            Dim C700 As Long
            Dim C600 As Long
            Dim C550 As Long
            Dim C400 As Long
            Dim C300 As Long
            Dim C200 As Long
            Dim C100 As Long
            Dim C20 As Long
            C1000 = 1000 * nMapScale
            C700 = 700 * nMapScale
            C600 = 600 * nMapScale
            C550 = 550 * nMapScale
            C400 = 400 * nMapScale
            C300 = 300 * nMapScale
            C200 = 200 * nMapScale
            C100 = 100 * nMapScale
            C20 = 20 * nMapScale

            Call SelectObject(hDC, hPen)
            Dim tPoint As POINT
            Call MoveToEx(hDC, DX + C400, DY + C1000, tPoint)
            Call LineTo(hDC, DX + C400, DY + C200)
            Call LineTo(hDC, DX + C200, DY + C400)
            Call LineTo(hDC, DX + C550, DY + C400)
            Call MoveToEx(hDC, DX + C100, DY + C600, tPoint)
            Call LineTo(hDC, DX + C700, DY + C600)


            Call SetTextColor(hDC, lColorN)
            Dim tRect As RECT
            tRect.Left = DX + C300
            tRect.Top = DY + C20
            tRect.Right = tRect.Left
            tRect.Bottom = tRect.Top
            Call DrawText(hDC, "Ｎ", -1, tRect, DT_NOCLIP Or DT_SINGLELINE Or DT_NOPREFIX)


        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        'ノースアローを描画する。
        '
        'DX、DY は論理座標ではなくデバイス座標。
        '
        '引き数：
        'hDC デバイスコンテキストのハンドル。
        'DX 描画位置左端(デバイス座標、ピクセル)。
        'DY 描画位置上端(デバイス座標、ピクセル)。
        'nMapScale Twips→ピクセル、の係数。
        'hPen ペン。
        'lColorN 記号色。
        */
#if false
        public void DrawNorthArrow(IntPtr hDC, double DX, double DY, double nMapScale, long hPen, long lColorN)
#else
        //        public Bitmap DrawNorthArrow(object imag, double DX, double DY, double nMapScale, long hPen, long lColorN, int picWidth, int picHeight)
        public Bitmap DrawNorthArrow(Bitmap imag, double DX, double DY, double nMapScale, long hPen, long lColorN, int picWidth, int picHeight)
#endif
        {
            //'定数。
            long C1000;
            long C700;
            long C600;
            long C550;
            long C400;
            long C300;
            long C200;
            long C100;
            long C20;
            C1000 = (long)(1000 * nMapScale);
            C700 = (long)(700 * nMapScale);
            C600 = (long)(600 * nMapScale);
            C550 = (long)(550 * nMapScale);
            C400 = (long)(400 * nMapScale);
            C300 = (long)(300 * nMapScale);
            C200 = (long)(200 * nMapScale);
            C100 = (long)(100 * nMapScale);
            C20 = (long)(20 * nMapScale);
#if false
            _ = SelectObject(hDC, hPen);
            //POINT tPoint = default;
            POINT tPoint = new POINT
            {
                X = 0,
                Y = 0
            };
            _ = MoveToEx(hDC, (long)DX + C400, (long)DY + C1000, ref tPoint);
            _ = LineTo(hDC, (long)DX + C400, (long)DY + C200);
            _ = LineTo(hDC, (long)DX + C200, (long)DY + C400);
            _ = LineTo(hDC, (long)DX + C550, (long)DY + C400);
            _ = MoveToEx(hDC, (long)DX + C100, (long)DY + C600, ref tPoint);
            _ = LineTo(hDC, (long)DX + C700, (long)DY + C600);

            _ = SetTextColor(hDC, lColorN);
            RECT tRect = new RECT();
            tRect.Left = (int)(DX + C300);
            tRect.Top = (int)(DY + C20);
            tRect.Right = tRect.Left;
            tRect.Bottom = tRect.Top;
            _ = DrawText(hDC, "Ｎ", -1, ref tRect, DT_NOCLIP | DT_SINGLELINE | DT_NOPREFIX);
            return;
#else
            //１.Bitmapクラスを作成(大きさはPictureBoxのサイズ)
            //Bitmap btmp = new Bitmap(picWidth, picHeight);
            Bitmap btmp = imag;
            //２.Graphicクラスを生成する(先ほどのBitmap(Imageオフジェクト)クラスを使う)
            Graphics grph = Graphics.FromImage(btmp);
            //Graphics grph = Graphics.FromImage(imag);
#if false
            //３.Penオブジェクト生成(今回は黒の太さ１)
            Color Clr = Color.FromArgb((int)(lColorN | 0xFF000000));
            Pen pen = new Pen(Clr, 2);
#else
            //３.Penオブジェクト生成(今回は白の太さ２)
            Color Clr = Color.White;
            Pen pen = new Pen(Clr, 2);
#endif
#if false
            //４.SmoothingModeを設定
            //(アンチエイリアス処理されたレタリング)を指定する
            //grph.SmoothingMode = SmoothingMode.AntiAlias;
            //(既定のモード)を指定する
            //grph.SmoothingMode = SmoothingMode.Default;
            //(高品質で低速なレンダリング)を指定する
            //grph.SmoothingMode = SmoothingMode.HighQuality;
            //(高速で、低品質のレンダリング)を指定する
            //grph.SmoothingMode = SmoothingMode.HighSpeed;
            //(アンチエイリアス処理しない)を指定する
            //grph.SmoothingMode = SmoothingMode.None;
#else
            //４.SmoothingModeを設定
            //(高品質で低速なレンダリング)を指定する
            grph.SmoothingMode = SmoothingMode.HighQuality;
#endif
            //５.線を引く
            grph.DrawLine(pen, new Point((int)(DX + C400), (int)(DY + C1000)), new Point((int)(DX + C400), (int)(DY + C200)));
            grph.DrawLine(pen, new Point((int)(DX + C400), (int)(DY + C200)), new Point((int)(DX + C200), (int)(DY + C400)));
            grph.DrawLine(pen, new Point((int)(DX + C200), (int)(DY + C400)), new Point((int)(DX + C550), (int)(DY + C400)));
            grph.DrawLine(pen, new Point((int)(DX + C100), (int)(DY + C600)), new Point((int)(DX + C700), (int)(DY + C600)));
#if false
            //６.文字を書く
            Font fnt = new Font("MS UI Gothic", 10);
            grph.DrawString("Ｎ", fnt, Brushes.Red, (float)(DX + C300), (float)(DY + C20));
#else
            //６.文字を書く
            Font fnt = new Font("Arial", 10, FontStyle.Bold);
            grph.DrawString("Ｎ", fnt, Brushes.Red, (float)(DX + C300), (float)(DY + C20));
#endif
            //７.解放する
            pen.Dispose();
            grph.Dispose();
            //８.表示を渡す
            return btmp;
            //return imag;
#endif
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'スケールバーを描画する。
        '
        'nViewLeft、nViewTop は論理座標ではなくデバイス座標。
        '
        '引き数：
        'hDC デバイスコンテキストのハンドル。
        'nViewLeft デバイス上のビュー左辺位置(デバイス座標、ピクセル)。
        'nViewTop デバイス上のビュー上辺位置(デバイス座標、ピクセル)。
        'nViewWidth ビューの幅(ピクセル)。
        'nViewHeight ビューの高さ(ピクセル)。
        'nMapScale Twips→ピクセル、の係数。
        'hFrame 枠ブラシ。
        'hBrush1 色1ブラシ。
        'hBrush2 色2ブラシ。
        'nFontColor 記号色。
        'nDrawScale ｍ→ピクセル、の係数。
        Public Sub DrawScalebar(ByVal hDC As Long, ByVal nViewLeft As Double, ByVal nViewTop As Double, ByVal nViewWidth As Double, ByVal nViewHeight As Double, ByVal nMapScale As Double, ByVal hFrame As Long, ByVal hBrush1 As Long, ByVal hBrush2 As Long, ByVal nFontColor As Long, ByVal nDrawScale As Double)

            Dim dSXLength As Double, dSYLength As Double
            Dim DX As Double, DY As Double
    
            '### スケールバー
            '--- 横 ---
            If nDrawScale<FLT_EPSILON Then Exit Sub
            dSXLength = GetScale(nViewWidth / nDrawScale)
            '--- 縦 ---
            dSYLength = nViewHeight - CInt(PLOT_SCALEBAR_HEIGHT* nMapScale)


            DX = CInt(dSXLength* nDrawScale)
            DY = dSYLength

            Dim tRect As RECT
            tRect.Left = nViewLeft
            tRect.Top = nViewTop + DY
            tRect.Right = nViewLeft + CInt(DX / 4!)
            tRect.Bottom = nViewTop + nViewHeight
            Call FillRect(hDC, tRect, hBrush1)
            tRect.Left = nViewLeft + CInt(DX / 4!)
            tRect.Right = nViewLeft + CInt(DX / 2!)
            Call FillRect(hDC, tRect, hBrush2)
            tRect.Left = nViewLeft + CInt(DX / 2!)
            tRect.Right = nViewLeft + CInt(DX * 3! / 4!)
            Call FillRect(hDC, tRect, hBrush1)
            tRect.Left = nViewLeft + CInt(DX * 3! / 4!)
            tRect.Right = nViewLeft + DX
            Call FillRect(hDC, tRect, hBrush2)
            If hFrame<> 0 Then
                tRect.Left = nViewLeft
                tRect.Right = nViewLeft + DX
                Call FrameRect(hDC, tRect, hFrame)
            End If

            Call SetTextColor(hDC, nFontColor)
            tRect.Left = nViewLeft + DX + CInt(100 & *nMapScale)
            tRect.Top = nViewTop + DY - CInt(150 & *nMapScale)
            tRect.Right = tRect.Left
            tRect.Bottom = tRect.Top


            Dim sText As String
            If dSXLength >= 1000# Then
                sText = dSXLength / 1000# & "km"
            ElseIf dSXLength >= 1# Then
                sText = dSXLength & "m"
            ElseIf dSXLength >= 0.01 Then
                sText = dSXLength * 100# & "cm"
            Else
                sText = dSXLength * 1000# & "mm"
            End If
            Call DrawText(hDC, sText, -1, tRect, DT_NOCLIP Or DT_SINGLELINE Or DT_NOPREFIX)


        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        'スケールバーを描画する。
        '
        'nViewLeft、nViewTop は論理座標ではなくデバイス座標。
        '
        '引き数：
        'hDC デバイスコンテキストのハンドル。
        'nViewLeft デバイス上のビュー左辺位置(デバイス座標、ピクセル)。
        'nViewTop デバイス上のビュー上辺位置(デバイス座標、ピクセル)。
        'nViewWidth ビューの幅(ピクセル)。
        'nViewHeight ビューの高さ(ピクセル)。
        'nMapScale Twips→ピクセル、の係数。
        'hFrame 枠ブラシ。
        'hBrush1 色1ブラシ。
        'hBrush2 色2ブラシ。
        'nFontColor 記号色。
        'nDrawScale ｍ→ピクセル、の係数。
        */
#if false
        public void DrawScalebar(IntPtr hDC, double nViewLeft, double nViewTop, double nViewWidth, double nViewHeight, double nMapScale, long hFrame, long hBrush1, long hBrush2,
            long nFontColor, double nDrawScale)
#else
        //public Bitmap DrawScalebar(object imag, double nViewLeft, double nViewTop, double nViewWidth, double nViewHeight, double nMapScale, long hFrame, long hBrush1, long hBrush2,
        //    long nFontColor, double nDrawScale)
        public Bitmap DrawScalebar(Bitmap imag, double nViewLeft, double nViewTop, double nViewWidth, double nViewHeight, double nMapScale, long hFrame, long hBrush1, long hBrush2,
            long nFontColor, double nDrawScale)
#endif
        {
            double dSXLength;
            double dSYLength;
            double DX;
            double DY;


#if false
            //'### スケールバー
            //'--- 横 ---
            if (nDrawScale < FLT_EPSILON)
            {
                return;
            }
            dSXLength = GetScale(nViewWidth / nDrawScale);
            //'--- 縦 ---
            dSYLength = nViewHeight - (int)(PLOT_SCALEBAR_HEIGHT * nMapScale);


            DX = (int)(dSXLength * nDrawScale);
            DY = dSYLength;

            RECT tRect = default;

#if false
            tRect.Left = (int)nViewLeft;
            tRect.Top = (int)(nViewTop + DY);
            tRect.Right = (int)(nViewLeft + (int)(DX / 4));
            tRect.Bottom = (int)(nViewTop + nViewHeight);
            _ = FillRect(hDC, ref tRect, hBrush1);
            tRect.Left = (int)(nViewLeft + (int)(DX / 4));
            tRect.Right = (int)(nViewLeft + (int)(DX / 2));
            _ = FillRect(hDC, ref tRect, hBrush2);
            tRect.Left = (int)(nViewLeft + (int)(DX / 2));
            tRect.Right = (int)(nViewLeft + (int)(DX * 3 / 4));
            _ = FillRect(hDC, ref tRect, hBrush1);
            tRect.Left = (int)(nViewLeft + (int)(DX * 3 / 4));
            tRect.Right = (int)(nViewLeft + DX);
            _ = FillRect(hDC, ref tRect, hBrush2);

            if (hFrame != 0)
            {
                tRect.Left = (int)nViewLeft;
                tRect.Right = (int)(nViewLeft + DX);
                _ = FrameRect(hDC, ref tRect, hFrame);
            }
#else
            int wid = (int)(DX / 4);
            int hig = (int)(nViewHeight - (nViewTop + DY));
            tRect.Left = (int)(nViewLeft + (wid * 0));
            tRect.Top = (int)(nViewTop + DY);
            tRect.Right = wid;
            tRect.Bottom = hig;
            _ = FillRect(hDC, ref tRect, hBrush1);
            tRect.Left = (int)(nViewLeft + (wid * 1));
            _ = FillRect(hDC, ref tRect, hBrush2);
            tRect.Left = (int)(nViewLeft + (wid * 2));
            _ = FillRect(hDC, ref tRect, hBrush1);
            tRect.Left = (int)(nViewLeft + (wid * 3));
            _ = FillRect(hDC, ref tRect, hBrush2);

            if (hFrame != 0)
            {
                tRect.Left = (int)nViewLeft;
                tRect.Right = (int)(nViewLeft + DX);
                _ = FrameRect(hDC, ref tRect, hFrame);
            }
#endif
            _ = SetTextColor(hDC, nFontColor);
            tRect.Left = (int)(nViewLeft + DX + (int)(100 * nMapScale));
            tRect.Top = (int)(nViewTop + DY - (int)(150 * nMapScale));
            tRect.Right = tRect.Left;
            tRect.Bottom = tRect.Top;


            string sText;
            if (dSXLength >= 1000)
            {
                sText = (dSXLength / 1000) + "km";
            }
            else if (dSXLength >= 1)
            {
                sText = dSXLength + "m";
            }
            else if (dSXLength >= 0.01)
            {
                sText = (dSXLength * 100) + "cm";
            }
            else
            {
                sText = (dSXLength * 1000) + "mm";
            }
            _ = DrawText(hDC, sText, -1, ref tRect, DT_NOCLIP | DT_SINGLELINE | DT_NOPREFIX);

            return;
#else
            //'### スケールバー
            //'--- 横 ---
            if (nDrawScale < FLT_EPSILON)
            {
                return imag;
            }
            dSXLength = GetScale(nViewWidth / nDrawScale);
            //'--- 縦 ---
            dSYLength = nViewHeight - (int)(PLOT_SCALEBAR_HEIGHT * nMapScale);


            DX = (int)(dSXLength * nDrawScale);
            DY = dSYLength;

            //１.Bitmapクラスを作成(大きさはPictureBoxのサイズ)
            //Bitmap btmp = new Bitmap((int)nViewWidth, (int)nViewHeight);
            //Bitmap btmp = (Bitmap)imag;
            //２.Graphicクラスを生成する(先ほどのBitmap(Imageオフジェクト)クラスを使う)
            //Graphics grph = Graphics.FromImage(btmp);
            Graphics grph = Graphics.FromImage(imag);
#if false
            //３.Penオブジェクト生成(今回は黒の太さ１)
            Color Clr = Color.FromArgb((int)(lColorN | 0xFF000000));
#else
            //３.Penオブジェクト生成(今回は白の太さ２)
            Color Clr = Color.White;
#endif
            Pen pen = new Pen(Clr, 2);
            //４.SmoothingModeを設定
            //(高品質で低速なレンダリング)を指定する
            grph.SmoothingMode = SmoothingMode.HighQuality;
            //５.四角を書く
            int wid = (int)(DX / 4);
            int hig = (int)(nViewHeight - (nViewTop + DY));
            grph.FillRectangle(Brushes.White, (int)(nViewLeft + (wid * 0)), (int)(nViewTop + DY), wid, hig);
            grph.FillRectangle(Brushes.Red, (int)(nViewLeft + (wid * 1)), (int)(nViewTop + DY), wid, hig);
            grph.FillRectangle(Brushes.White, (int)(nViewLeft + (wid * 2)), (int)(nViewTop + DY), wid, hig);
            grph.FillRectangle(Brushes.Red, (int)(nViewLeft + (wid * 3)), (int)(nViewTop + DY), wid, hig);
            //６.文字を書く
            string sText;
            if (dSXLength >= 1000) { sText = (dSXLength / 1000) + "km"; }
            else if (dSXLength >= 1) { sText = dSXLength + "m"; }
            else if (dSXLength >= 0.01) { sText = (dSXLength * 100) + "cm"; }
            else { sText = (dSXLength * 1000) + "mm"; }
#if false
            Font fnt = new Font("MS UI Gothic", 10);
#else
            Font fnt = new Font("Arial", 10, FontStyle.Bold);
#endif
            //grph.DrawString(sText, fnt, Brushes.Green, (float)(nViewLeft + DX + (double)(100 * nMapScale)), (float)(nViewTop + DY - (double)(150 * nMapScale)));
            grph.DrawString(sText, fnt, Brushes.LightGreen, (float)(nViewLeft + DX + (double)(100 * nMapScale)), (float)(nViewTop + DY - (double)(150 * nMapScale)));
            //７.解放する
            pen.Dispose();
            grph.Dispose();
            //８.表示を渡す
            //return btmp;
            return imag;
#endif
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'スケールを取得する。
        '
        'スケールバーの横幅(ｍ)を取得する。
        '
        '引き数：
        'dArg ビューの幅(ｍ)。
        '
        '戻り値：スケールバーの横幅(ｍ)。
        Private Function GetScale(ByVal dArg As Double) As Double

            Dim i As Integer
            Dim dqArg As Double


            dqArg = dArg / 4#
    
            '[0.001mm未満の場合]
            If dqArg< 0.000001 Then
                GetScale = dqArg
            '[0.01mm未満の場合]
            ElseIf dqArg < 0.00001 Then
                GetScale = CDbl(Fix(dqArg * 1000000) * 0.000001)
            '[0.1mm未満の場合]
            ElseIf dqArg < 0.0001 Then
                GetScale = CDbl(Fix(dqArg * 100000) * 0.00001)
            '[1mm未満の場合]
            ElseIf dqArg < 0.001 Then
                GetScale = CDbl(Fix(dqArg * 10000) * 0.0001)
            '[1cm未満の場合]
            ElseIf dqArg < 0.01 Then
                GetScale = CDbl(Fix(dqArg * 1000) * 0.001)
            '[10cm未満の場合]
            ElseIf dqArg < 0.1 Then
                GetScale = CDbl(Fix(dqArg * 100) * 0.01)
            '[1m未満の場合]
            ElseIf dqArg < 1 Then
                GetScale = CDbl(Fix(dqArg * 10) * 0.1)
            '[10m未満の場合]
            ElseIf dqArg < 10 Then
                GetScale = CDbl(Fix(dqArg * 1) * 1 &)
            '[100m未満の場合]
            ElseIf dqArg < 100 Then
                GetScale = CDbl(Fix(dqArg / 10) * 10 &)
            '[1km未満の場合]
            ElseIf dqArg < 1000 Then
                GetScale = CDbl(Fix(dqArg / 100) * 100 &)
            '[10km未満の場合]
            ElseIf dqArg < 10000 Then
                GetScale = CDbl(Fix(dqArg / 1000) * 1000 &)
            '[100km未満の場合]
            ElseIf dqArg < 100000 Then
                GetScale = CDbl(Fix(dqArg / 10000) * 10000 &)
            '[100km以上の場合]
            Else
                GetScale = CDbl(Fix(dqArg / 100000) * 100000)
            End If
    
    
        '    dqArg = dArg / 4
        '
        '    If dqArg < 0.0003 Then
        '        GetScale = 0.0001
        '    ElseIf dqArg >= 0.0003 And dqArg < 0.0008 Then '[0.0003～0.0008]
        '        GetScale = 0.0005
        '    ElseIf dqArg >= 0.0008 And dqArg < 0.003 Then '[0.0008～0.0003]
        '        GetScale = 0.001
        '    ElseIf dqArg >= 0.003 And dqArg < 0.008 Then '[0.003～0.008]
        '        GetScale = 0.005
        '    ElseIf dqArg >= 0.008 And dqArg < 0.003 Then '[0.008～0.003]
        '        GetScale = 0.01
        '    ElseIf dqArg >= 0.03 And dqArg < 0.08 Then '[0.03～0.08]
        '        GetScale = 0.05
        '    ElseIf dqArg >= 0.08 And dqArg < 0.3 Then
        '        GetScale = 0.1
        '    ElseIf dqArg >= 0.3 And dqArg < 0.8 Then
        '        GetScale = 0.5
        '    ElseIf dqArg >= 0.8 And dqArg < 3 Then
        '        GetScale = 1
        '    ElseIf dqArg >= 3 And dqArg < 8 Then
        '        GetScale = 5
        '    ElseIf dqArg >= 8 And dqArg < 30 Then
        '        GetScale = 10
        '    ElseIf dqArg >= 30 And dqArg < 80 Then
        '        GetScale = 50
        '    ElseIf dqArg >= 80 And dqArg < 300 Then
        '        GetScale = 100
        '    ElseIf dqArg >= 300 And dqArg < 800 Then
        '        GetScale = 500
        '    ElseIf dqArg >= 800 And dqArg < 3000 Then
        '        GetScale = 1000
        '    ElseIf dqArg >= 3000 And dqArg < 8000 Then
        '        GetScale = 5000
        '    Else
        '        GetScale = 10000
        '    End If
    
        '    If dqArg < 0.003 Then
        '        GetScale = 0.001
        '    ElseIf dqArg >= 0.003 And dqArg < 0.008 Then
        '        GetScale = 0.005
        '    ElseIf dqArg >= 0.008 And dqArg < 0.003 Then
        '        GetScale = 0.01
        '    ElseIf dqArg >= 0.03 And dqArg < 0.08 Then
        '        GetScale = 0.05
        '    ElseIf dqArg >= 0.08 And dqArg < 0.3 Then
        '        GetScale = 0.1
        '    ElseIf dqArg >= 0.3 And dqArg < 0.8 Then
        '        GetScale = 0.5
        '    ElseIf dqArg >= 0.8 And dqArg < 3 Then
        '        GetScale = 1
        '    ElseIf dqArg >= 3 And dqArg < 8 Then
        '        GetScale = 5
        '    ElseIf dqArg >= 8 And dqArg < 30 Then
        '        GetScale = 10
        '    ElseIf dqArg >= 30 And dqArg < 80 Then
        '        GetScale = 50
        '    ElseIf dqArg >= 80 And dqArg < 300 Then
        '        GetScale = 100
        '    ElseIf dqArg >= 300 And dqArg < 800 Then
        '        GetScale = 500
        '    ElseIf dqArg >= 800 And dqArg < 3000 Then
        '        GetScale = 1000
        '    ElseIf dqArg >= 3000 And dqArg < 8000 Then
        '        GetScale = 5000
        '    Else
        '        GetScale = 10000
        '    End If


        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        'スケールを取得する。
        '
        'スケールバーの横幅(ｍ)を取得する。
        '
        '引き数：
        'dArg ビューの幅(ｍ)。
        '
        '戻り値：スケールバーの横幅(ｍ)。
        */
        private double GetScale(double dArg)
        {
            int i;
            double dqArg;


            dqArg = dArg / 4;

            //'[0.001mm未満の場合]
            if (dqArg < 0.000001)
            {
                return dqArg;
            }
            //'[0.01mm未満の場合]
            else if (dqArg < 0.00001)
            {
                return (double)(Math.Truncate(dqArg * 1000000) * 0.000001);
            }
            //'[0.1mm未満の場合]
            else if (dqArg < 0.0001)
            {
                return (double)(Math.Truncate(dqArg * 100000) * 0.00001);
            }
            //'[1mm未満の場合]
            else if (dqArg < 0.001)
            {
                return (double)(Math.Truncate(dqArg * 10000) * 0.0001);
            }
            //'[1cm未満の場合]
            else if (dqArg < 0.01)
            {
                return (double)(Math.Truncate(dqArg * 1000) * 0.001);
            }
            //'[10cm未満の場合]
            else if (dqArg < 0.1)
            {
                return (double)(Math.Truncate(dqArg * 100) * 0.01);
            }
            //'[1m未満の場合]
            else if (dqArg < 1)
            {
                return (double)(Math.Truncate(dqArg * 10) * 0.1);
            }
            //'[10m未満の場合]
            else if (dqArg < 10)
            {
                return (double)(Math.Truncate(dqArg * 1) * 1);
            }
            //'[100m未満の場合]
            else if (dqArg < 100)
            {
                return (double)(Math.Truncate(dqArg / 10) * 10);
            }
            //'[1km未満の場合]
            else if (dqArg < 1000)
            {
                return (double)(Math.Truncate(dqArg / 100) * 100);
            }
            //'[10km未満の場合]
            else if (dqArg < 10000)
            {
                return (double)(Math.Truncate(dqArg / 1000) * 1000);
            }
            //'[100km未満の場合]
            else if (dqArg < 100000)
            {
                return (double)(Math.Truncate(dqArg / 10000) * 10000);
            }
            //'[100km以上の場合]
            else
            {
                return (double)(Math.Truncate(dqArg / 100000) * 100000);
            }
        }
        //==========================================================================================

    }
}
