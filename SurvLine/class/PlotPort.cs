using SurvLine.mdl;
using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static SurvLine.mdl.MdlNSDefine;

namespace SurvLine
{
    public class PlotPort
    {
        //==========================================================================================
        /*[VB]
        '*******************************************************************************
        'プロット領域
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
        'プロパティ
        Public LogicalLeft As Double '論理エリア左辺(平面直角、最小Ｙ、ｍ)。
        Public LogicalBottom As Double '論理エリア下辺(平面直角、最小Ｘ、ｍ)。
        Public LogicalWidth As Double '論理エリア幅(平面直角、Ｙ軸長さ、ｍ)。
        Public LogicalHeight As Double '論理エリア高さ(平面直角、Ｘ軸長さ、ｍ)。
        Public DeviceLeft As Double '論理エリア上のデバイス左辺位置(ピクセル)。
        Public DeviceTop As Double '論理エリア上のデバイス上辺位置(ピクセル)。
        Public DeviceWidth As Double 'デバイス幅(ピクセル)。
        Public DeviceHeight As Double 'デバイス高さ(ピクセル)。
        Public ViewLeft As Double '論理エリア上のビュー左辺位置(ピクセル)。
        Public ViewTop As Double '論理エリア上のビュー上辺位置(ピクセル)。
        Public ViewWidth As Double 'ビュー幅(ピクセル)。
        Public ViewHeight As Double 'ビュー高さ(ピクセル)。
        Public DrawScale As Double '描画スケール。平面直角座標(実座標、ｍ)をデバイス座標(ピクセル)へ変換する係数。
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'プロパティ
        public double LogicalLeft;          //'論理エリア左辺(平面直角、最小Ｙ、ｍ)。
        public double LogicalBottom;        //'論理エリア下辺(平面直角、最小Ｘ、ｍ)。
        public double LogicalWidth;         //'論理エリア幅(平面直角、Ｙ軸長さ、ｍ)。
        public double LogicalHeight;        //'論理エリア高さ(平面直角、Ｘ軸長さ、ｍ)。
        public double DeviceLeft;           //'論理エリア上のデバイス左辺位置(ピクセル)。
        public double DeviceTop;            //'論理エリア上のデバイス上辺位置(ピクセル)。
        public double DeviceWidth;          //'デバイス幅(ピクセル)。
        public double DeviceHeight;         //'デバイス高さ(ピクセル)。
        public double ViewLeft;             //'論理エリア上のビュー左辺位置(ピクセル)。
        public double ViewTop;              //'論理エリア上のビュー上辺位置(ピクセル)。
        public double ViewWidth;            //'ビュー幅(ピクセル)。
        public double ViewHeight;           //'ビュー高さ(ピクセル)。
        public double DrawScale;            //'描画スケール。平面直角座標(実座標、ｍ)をデバイス座標(ピクセル)へ変換する係数。
                                            //==========================================================================================

        //==========================================================================================
        //[C#]
        private MdlMain m_clsMdlMain;                                   //MdlMainインスタンス
        private MdlPlotProc m_clsMdlPlotProc;                           //MdlPlotProcインスタンス

        public PlotPort(MdlMain clsMdlMain, MdlPlotProc clsMdlPlotProc)
        {
            m_clsMdlMain = clsMdlMain;
            m_clsMdlPlotProc = clsMdlPlotProc;
            return;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '*******************************************************************************
        'イベント

        '初期化。
        Private Sub Class_Initialize()

            On Error GoTo ErrorHandler


            DrawScale = 1
    
            '描画範囲。
            LogicalLeft = 0
            LogicalBottom = 0
            LogicalWidth = 1
            LogicalHeight = 1
    
            '定数。
            MATH_S1 = Sin(270# / 180# * PAI)
            MATH_S2 = Sin(150# / 180# * PAI)
            MATH_S3 = Sin(30# / 180# * PAI)
            MATH_C1 = Cos(270# / 180# * PAI)
            MATH_C2 = Cos(150# / 180# * PAI)
            MATH_C3 = Cos(30# / 180# * PAI)
            MATH_SQR3 = Sqr(3)


            Exit Sub


        ErrorHandler:
            Call mdlMain.ErrorExit


        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'初期化。
        public void Class_Initialize()
        {
            try
            {
                DrawScale = 1;

                //'描画範囲。
                LogicalLeft = 0;
                LogicalBottom = 0;
                LogicalWidth = 1;
                LogicalHeight = 1;

                //'定数。
                m_clsMdlPlotProc.set_MATH_S1(Math.Sin(270 * (DEFINE.PAI / 180)));
                m_clsMdlPlotProc.set_MATH_S2(Math.Sin(150 * (DEFINE.PAI / 180)));
                m_clsMdlPlotProc.set_MATH_S3(Math.Sin(30 * (DEFINE.PAI / 180)));
                m_clsMdlPlotProc.set_MATH_C1(Math.Cos(270 * (DEFINE.PAI / 180)));
                m_clsMdlPlotProc.set_MATH_C2(Math.Cos(150 * (DEFINE.PAI / 180)));
                m_clsMdlPlotProc.set_MATH_C3(Math.Cos(30 * (DEFINE.PAI / 180)));
                m_clsMdlPlotProc.set_MATH_SQR3(Math.Sqrt(3));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "エラーが発生");
                return;
            }
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '*******************************************************************************
        'メソッド

        '論理エリアを更新する。
        '
        '配置図の描画するべき領域として、観測点や基線ベクトル等のオブジェクト群に外接する四角形を論理範囲として設定する。外接といってもすこし余白を持たせる。
        'bIsEnable が True の場合、有効なオブジェクトのみ評価して範囲を決める。
        'bIsEnable が False の場合、無効なオブジェクトも含めて評価して範囲を決める。
        'nSwitch は評価に含めるオブジェクトを決定するためのスイッチであり、アプリごとに固有の内容を設定する。
        '
        '引き数：
        'bIsEnable 有効/無効評価フラグ。
        'nSwitch 評価スイッチ。
        Public Sub UpdateLogicalDrawArea(Optional ByVal bIsEnable As Boolean = False, Optional ByVal nSwitch As Long = &HFFFFFFFF)

            '平面直角座標を設定する。
            Dim clsObservationPoint As ObservationPoint
            Dim clsChainList As ChainList
            Set clsChainList = GetDocument().NetworkModel.RepresentPointHead
            Do While Not clsChainList Is Nothing
                Set clsObservationPoint = clsChainList.Element
                Call SetPlanePoint(clsObservationPoint)
                Set clsChainList = clsChainList.NextList
            Loop


            '最小最大。
            Dim nMinX As Double
            Dim nMinY As Double
            Dim nMaxX As Double
            Dim nMaxY As Double
            Call GetDocument().NetworkModel.GetObservationPointArea(GetDocument().CoordNum, nMinX, nMinY, nMaxX, nMaxY, bIsEnable, nSwitch)


            LogicalWidth = nMaxY - nMinY
            LogicalHeight = nMaxX - nMinX
            If Abs(LogicalWidth) < MIN_LOGICALSIZE Then
                nMinY = nMinY - (MIN_LOGICALSIZE - LogicalWidth) * 0.5
                LogicalWidth = MIN_LOGICALSIZE
            End If
            If Abs(LogicalHeight) < MIN_LOGICALSIZE Then
                nMinX = nMinX - (MIN_LOGICALSIZE - LogicalHeight) * 0.5
                LogicalHeight = MIN_LOGICALSIZE
            End If
            LogicalLeft = nMinY - LogicalWidth * DRAW_SPACE
            LogicalBottom = nMinX - LogicalHeight * DRAW_SPACE
            LogicalWidth = LogicalWidth * (1 + DRAW_SPACE * 2!)
            LogicalHeight = LogicalHeight * (1 + DRAW_SPACE * 2!)


        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        'メソッド
        
        '論理エリアを更新する。
        '
        '配置図の描画するべき領域として、観測点や基線ベクトル等のオブジェクト群に外接する四角形を論理範囲として設定する。外接といってもすこし余白を持たせる。
        'bIsEnable が True の場合、有効なオブジェクトのみ評価して範囲を決める。
        'bIsEnable が False の場合、無効なオブジェクトも含めて評価して範囲を決める。
        'nSwitch は評価に含めるオブジェクトを決定するためのスイッチであり、アプリごとに固有の内容を設定する。
        '
        '引き数：
        'bIsEnable 有効/無効評価フラグ。
        'nSwitch 評価スイッチ。
        */
        public void UpdateLogicalDrawArea(bool bIsEnable = false, long nSwitch = -1)
        {
            //'平面直角座標を設定する。
            ObservationPoint clsObservationPoint;
            ChainList clsChainList;
            clsChainList = m_clsMdlMain.GetDocument().NetworkModel().RepresentPointHead();
            int i1 = 0;
            while (clsChainList != null)
            {
                clsObservationPoint = (ObservationPoint)clsChainList.Element;
                SetPlanePoint(clsObservationPoint);
                clsChainList = clsChainList.NextList();
                i1++;
            }


            //'最小最大。
            double nMinX = 0;
            double nMinY = 0;
            double nMaxX = 0;
            double nMaxY = 0;
            _ = m_clsMdlMain.GetDocument().NetworkModel().GetObservationPointArea(m_clsMdlMain.GetDocument().CoordNum(),
                                                            ref nMinX, ref nMinY, ref nMaxX, ref nMaxY, bIsEnable, nSwitch);

            LogicalWidth = nMaxY - nMinY;
            LogicalHeight = nMaxX - nMinX;
            if (Math.Abs(LogicalWidth) < MdlPlotProc.MIN_LOGICALSIZE)
            {
                nMinY = nMinY - (MdlPlotProc.MIN_LOGICALSIZE - LogicalWidth) * 0.5;
                LogicalWidth = MdlPlotProc.MIN_LOGICALSIZE;
            }
            if (Math.Abs(LogicalHeight) < MdlPlotProc.MIN_LOGICALSIZE)
            {
                nMinX = nMinX - (MdlPlotProc.MIN_LOGICALSIZE - LogicalHeight) * 0.5;
                LogicalHeight = MdlPlotProc.MIN_LOGICALSIZE;
            }
            LogicalLeft = nMinY - LogicalWidth * MdlPlotProc.DRAW_SPACE;
            LogicalBottom = nMinX - LogicalHeight * MdlPlotProc.DRAW_SPACE;
            LogicalWidth = LogicalWidth * (1 + MdlPlotProc.DRAW_SPACE * 2);
            LogicalHeight = LogicalHeight * (1 + MdlPlotProc.DRAW_SPACE * 2);

        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '描画範囲の補正。
        '
        'デバイスの範囲とビューの範囲を補正する。
        '論理エリアをスケーリングした時に小さすぎる場合は補正する。
        Public Sub CorrectDrawArea()
            If LogicalWidth * DrawScale < DeviceWidth Then
                DeviceLeft = (LogicalWidth * DrawScale - DeviceWidth) * 0.5
            Else
                If LogicalWidth * DrawScale < DeviceLeft + DeviceWidth Then DeviceLeft = LogicalWidth * DrawScale - DeviceWidth
                If DeviceLeft < 0 Then DeviceLeft = 0
            End If
            If LogicalHeight * DrawScale < DeviceHeight Then
                DeviceTop = (LogicalHeight * DrawScale - DeviceHeight) * 0.5
            Else
                If LogicalHeight * DrawScale < DeviceTop + DeviceHeight Then DeviceTop = LogicalHeight * DrawScale - DeviceHeight
                If DeviceTop < 0 Then DeviceTop = 0
            End If
            If LogicalWidth * DrawScale < ViewWidth Then
                ViewLeft = (LogicalWidth * DrawScale - ViewWidth) * 0.5
            Else
                If LogicalWidth * DrawScale < ViewLeft + ViewWidth Then ViewLeft = LogicalWidth * DrawScale - ViewWidth
                If ViewLeft < 0 Then ViewLeft = 0
            End If
            If LogicalHeight * DrawScale < ViewHeight Then
                ViewTop = (LogicalHeight * DrawScale - ViewHeight) * 0.5
            Else
                If LogicalHeight * DrawScale < ViewTop + ViewHeight Then ViewTop = LogicalHeight * DrawScale - ViewHeight
                If ViewTop < 0 Then ViewTop = 0
            End If
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '描画範囲の補正。
        '
        'デバイスの範囲とビューの範囲を補正する。
        '論理エリアをスケーリングした時に小さすぎる場合は補正する。
        */
        public void CorrectDrawArea()
        {
            if (LogicalWidth * DrawScale < DeviceWidth)
            {
                DeviceLeft = ((LogicalWidth * DrawScale) - DeviceWidth) * 0.5;
            }
            else
            {
                if (LogicalWidth * DrawScale < DeviceLeft + DeviceWidth) { DeviceLeft = (LogicalWidth * DrawScale) - DeviceWidth; }
                if (DeviceLeft < 0) { DeviceLeft = 0; }
            }
            if (LogicalHeight * DrawScale < DeviceHeight)
            {
                DeviceTop = ((LogicalHeight * DrawScale) - DeviceHeight) * 0.5;
            }
            else
            {
                if (LogicalHeight * DrawScale < DeviceTop + DeviceHeight) { DeviceTop = (LogicalHeight * DrawScale) - DeviceHeight; }
                if (DeviceTop < 0) { DeviceTop = 0; }
            }
            if (LogicalWidth * DrawScale < ViewWidth)
            {
                ViewLeft = ((LogicalWidth * DrawScale) - ViewWidth) * 0.5;
            }
            else
            {
                if (LogicalWidth * DrawScale < ViewLeft + ViewWidth) { ViewLeft = (LogicalWidth * DrawScale) - ViewWidth; }
                if (ViewLeft < 0) { ViewLeft = 0; }
            }
            if (LogicalHeight * DrawScale < ViewHeight)
            {
                ViewTop = ((LogicalHeight * DrawScale) - ViewHeight) * 0.5;
            }
            else
            {
                if (LogicalHeight * DrawScale < ViewTop + ViewHeight) { ViewTop = (LogicalHeight * DrawScale) - ViewHeight; }
                if (ViewTop < 0) { ViewTop = 0; }
            }
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'クリップ領域を取得する。
        '
        'クリップ領域は観測点記号等の文字列を描画するときにはみ出さないように制限するための領域。
        '通常クリップ領域は論理エリアと重なる。
        '論理エリアが画面サイズより小さい場合、クリップ領域は画面に重なるように変更する。
        '
        '引き数：
        'nClipLeft 論理エリア上のクリップ領域左辺(ピクセル)が設定される。
        'nClipTop 論理エリア上のクリップ領域上辺(ピクセル)が設定される。
        'nClipWidth 論理エリア上のクリップ領域幅(ピクセル)が設定される。
        'nClipHeight 論理エリア上のクリップ領域高さ(ピクセル)が設定される。
        Public Sub GetClipArea(ByRef nClipLeft As Double, ByRef nClipTop As Double, ByRef nClipWidth As Double, ByRef nClipHeight As Double)
            If ViewWidth < LogicalWidth * DrawScale Then
                nClipLeft = 0
                nClipWidth = LogicalWidth * DrawScale
            Else
                nClipLeft = (LogicalWidth * DrawScale - ViewWidth) * 0.5
                nClipWidth = ViewWidth
            End If
            If ViewHeight < LogicalHeight * DrawScale Then
                nClipTop = 0
                nClipHeight = LogicalHeight * DrawScale
            Else
                nClipTop = (LogicalHeight * DrawScale - ViewHeight) * 0.5
                nClipHeight = ViewHeight
            End If
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        'クリップ領域を取得する。
        '
        'クリップ領域は観測点記号等の文字列を描画するときにはみ出さないように制限するための領域。
        '通常クリップ領域は論理エリアと重なる。
        '論理エリアが画面サイズより小さい場合、クリップ領域は画面に重なるように変更する。
        '
        '引き数：
        'nClipLeft 論理エリア上のクリップ領域左辺(ピクセル)が設定される。
        'nClipTop 論理エリア上のクリップ領域上辺(ピクセル)が設定される。
        'nClipWidth 論理エリア上のクリップ領域幅(ピクセル)が設定される。
        'nClipHeight 論理エリア上のクリップ領域高さ(ピクセル)が設定される。
        */
        public void GetClipArea(ref double nClipLeft, ref double nClipTop, ref double nClipWidth, ref double nClipHeight)
        {
            if ((long)ViewWidth < (long)(LogicalWidth * DrawScale))
            {
                nClipLeft = 0;
                nClipWidth = LogicalWidth * DrawScale;
            }
            else
            {
                nClipLeft = (LogicalWidth * DrawScale - ViewWidth) * 0.5;
                nClipWidth = ViewWidth;
            }
            if ((long)ViewHeight < (long)(LogicalHeight * DrawScale))
            {
                nClipTop = 0;
                nClipHeight = LogicalHeight * DrawScale;
            }
            else
            {
                nClipTop = (LogicalHeight * DrawScale - ViewHeight) * 0.5;
                nClipHeight = ViewHeight;
            }
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '観測点の平面直角座標を設定する。
        '
        '現在の座標系番号に従い平面直角座標を計算する。
        '
        '引き数：
        'clsObservationPoint 平面直角座標を設定する観測点。
        Public Sub SetPlanePoint(ByVal clsObservationPoint As ObservationPoint)

            '平面直角座標。
            Dim clsCoordinatePoint As CoordinatePoint
            Dim nX As Double
            Dim nY As Double
            Dim nZ As Double
            Set clsCoordinatePoint = clsObservationPoint.CoordinateDisplay
            Call WGS84xyz_to_JGDxyz(GetDocument().CoordNum, clsCoordinatePoint.RoundX, clsCoordinatePoint.RoundY, clsCoordinatePoint.RoundZ, nX, nY, nZ)


            Dim clsPlanePoint As TwipsPoint
            Set clsPlanePoint = clsObservationPoint.PlanePoint
            clsPlanePoint.X = nX
            clsPlanePoint.Y = nY


        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '観測点の平面直角座標を設定する。
        '
        '現在の座標系番号に従い平面直角座標を計算する。
        '
        '引き数：
        'clsObservationPoint 平面直角座標を設定する観測点。
        */
        public void SetPlanePoint(ObservationPoint clsObservationPoint)
        {
            //MdlNSDefine clsMdlNSDefine = new MdlNSDefine();

            //'平面直角座標。
            CoordinatePoint clsCoordinatePoint;
            double nX = 0;
            double nY = 0;
            double nZ = 0;
            clsCoordinatePoint = clsObservationPoint.CoordinateDisplay();
#if false
            clsMdlNSDefine.WGS84xyz_to_JGDxyz(m_clsMdlMain.GetDocument().CoordNum(), (double)Math.Round(clsCoordinatePoint.X), (double)Math.Round(clsCoordinatePoint.Y), (double)Math.Round(clsCoordinatePoint.Z),
                ref nX, ref nY, ref nZ);
#else
            WGS84xyz_to_JGDxyz(m_clsMdlMain.GetDocument().CoordNum(), clsCoordinatePoint.RoundXX(), clsCoordinatePoint.YY(), clsCoordinatePoint.ZZ(),
                ref nX, ref nY, ref nZ);
#endif

            TwipsPoint clsPlanePoint;
            clsPlanePoint = clsObservationPoint.PlanePoint();
            clsPlanePoint.X = nX;
            clsPlanePoint.Y = nY;

        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '観測点の描画座標を設定する。
        '
        '現在の論理座標と描画スケールに従い描画座標を計算する。
        '
        '引き数：
        'clsObservationPoint 描画座標を設定する観測点。
        Public Sub SetDevicePoint(ByVal clsObservationPoint As ObservationPoint)

            '平面直角座標。
            Dim clsPlanePoint As TwipsPoint
            Set clsPlanePoint = clsObservationPoint.PlanePoint

            '描画座標。
            Dim clsDevicePoint As TwipsPoint
            Set clsDevicePoint = clsObservationPoint.DevicePoint
            clsDevicePoint.X = (clsPlanePoint.Y - LogicalLeft) * DrawScale
            clsDevicePoint.Y = (LogicalHeight - (clsPlanePoint.X - LogicalBottom)) * DrawScale


        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '観測点の描画座標を設定する。
        '
        '現在の論理座標と描画スケールに従い描画座標を計算する。
        '
        '引き数：
        'clsObservationPoint 描画座標を設定する観測点。
        */
        public void SetDevicePoint(ObservationPoint clsObservationPoint)
        {

            //'平面直角座標。
            TwipsPoint clsPlanePoint;
            clsPlanePoint =clsObservationPoint.PlanePoint();

            //'描画座標。
            TwipsPoint clsDevicePoint;
            clsDevicePoint = clsObservationPoint.DevicePoint();
            clsDevicePoint.X = (clsPlanePoint.Y - LogicalLeft) * DrawScale;
            clsDevicePoint.Y = (LogicalHeight - (clsPlanePoint.X - LogicalBottom)) * DrawScale;


        }
        //==========================================================================================
    }
}
