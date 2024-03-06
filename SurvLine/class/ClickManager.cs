using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using System.Windows.Forms;
using System.Collections;
using System.Collections.Generic;

namespace SurvLine
{
    public class ClickManager : ClickObjectInterface
    {
        //==========================================================================================
        /*[VB]
        '*******************************************************************************
        'クリックマネージャ
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
        Public BasedRadius As Double '基準点半径(ピクセル)。×に外接する正方形の大きさ(の半分)。
        Public FixedRadius As Double '固定点半径(ピクセル)。正三角形に外接する円の半径。
        Public PointRadius As Double '移動点半径(ピクセル)。
        Public ArrowLength As Double '基線ベクトルの矢印長さ(ピクセル)。
        Public ArrowWidth As Double '基線ベクトルの矢印幅(ピクセル)。
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'プロパティ
        public double BasedRadius;              //'基準点半径(ピクセル)。×に外接する正方形の大きさ(の半分)。
        public double FixedRadius;              //'固定点半径(ピクセル)。正三角形に外接する円の半径。
        public double PointRadius;              //'移動点半径(ピクセル)。
        public double ArrowLength;              //'基線ベクトルの矢印長さ(ピクセル)。
        public double ArrowWidth;               //'基線ベクトルの矢印幅(ピクセル)。
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'インプリメンテーション
        Private m_clsClickAreas() As ClickArea 'クリックエリア。配列の要素は(-1 To ...)、要素 -1 は未使用。
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'インプリメンテーション
        private ClickArea m_clsClickAreas;    //'クリックエリア。配列の要素は(-1 To ...)、要素 -1 は未使用。
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '*******************************************************************************
        'イベント

        '初期化。
        Private Sub Class_Initialize()

            On Error GoTo ErrorHandler


            ReDim m_clsClickAreas(-1 To -1)


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
            return;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '*******************************************************************************
        'メソッド

        '登録の開始。
        '
        'クリックオブジェクトを登録する準備をする。
        '最初に StartRegist を呼び、登録が終わったら EndRegist を呼ぶ。
        '
        '引き数：
        'nClipWidth 論理エリアの幅(ピクセル)。
        'nClipHeight 論理エリアの高さ(ピクセル)。
        'nVectorCount これから登録する基線ベクトルの最大数。
        'nPointCount これから登録する観測点の最大数。
        Public Sub StartRegist(ByVal nClipWidth As Double, ByVal nClipHeight As Double, ByVal nVectorCount As Long, ByVal nPointCount As Long)

            Dim nLeft As Double
            Dim nTop As Double
            Dim nWidth As Double
            Dim nHeight As Double


            nLeft = 0
            nTop = 0
            nWidth = nClipWidth
            nHeight = nClipHeight


            Dim nBlock As Long
            nBlock = Fix(Sqr(nPointCount / 16)) + 1
            ReDim m_clsClickAreas(nBlock - 1, nBlock - 1)


            Dim X As Long
            Dim Y As Long
            For X = 0 To UBound(m_clsClickAreas, 1)
                For Y = 0 To UBound(m_clsClickAreas, 2)
                    Set m_clsClickAreas(X, Y) = New ClickArea
                    m_clsClickAreas(X, Y).SX = nLeft + nWidth / nBlock* X
                    m_clsClickAreas(X, Y).SY = nTop + nHeight / nBlock* Y
                    m_clsClickAreas(X, Y).EX = m_clsClickAreas(X, Y).SX + nWidth / nBlock
                    m_clsClickAreas(X, Y).EY = m_clsClickAreas(X, Y).SY + nHeight / nBlock
                    Call m_clsClickAreas(X, Y).StartRegistClickObject(nVectorCount + nPointCount)
                Next
            Next


        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '*******************************************************************************
        'メソッド
        
        '登録の開始。
        '
        'クリックオブジェクトを登録する準備をする。
        '最初に StartRegist を呼び、登録が終わったら EndRegist を呼ぶ。
        '
        '引き数：
        'nClipWidth 論理エリアの幅(ピクセル)。
        'nClipHeight 論理エリアの高さ(ピクセル)。
        'nVectorCount これから登録する基線ベクトルの最大数。
        'nPointCount これから登録する観測点の最大数。
        */
        public void StartRegist(double nClipWidth, double nClipHeight, long nVectorCount, long nPointCount)
        {
            return;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '登録の完了。
        Public Sub EndRegist()
            Dim X As Long
            Dim Y As Long
            For X = 0 To UBound(m_clsClickAreas, 1)
                For Y = 0 To UBound(m_clsClickAreas, 2)
                    Call m_clsClickAreas(X, Y).EndRegistClickObject
                Next
            Next
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'登録の完了。
        public void EndRegist()
        {
            return;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '基準点を登録する。
        '
        '指定された基準点のクリックオブジェクトを生成し登録する。
        '基準点は長方形クリックオブジェクト。
        '
        '引き数：
        'clsObservationPoint 登録する観測点。
        '
        '戻り値：登録したクリックオブジェクト。
        Public Function RegistBased(ByVal clsObservationPoint As ObservationPoint) As ClickRect
            Dim nX As Double
            Dim nY As Double
            nX = clsObservationPoint.DevicePoint.X
            nY = clsObservationPoint.DevicePoint.Y
            Dim clsClickObject As New ClickRect
            Set clsClickObject.Object = clsObservationPoint
            Call clsClickObject.SetParam(nX - BasedRadius, nY - BasedRadius, BasedRadius* 2, BasedRadius* 2)
            Dim X As Long
            Dim Y As Long
            For X = 0 To UBound(m_clsClickAreas, 1)
                For Y = 0 To UBound(m_clsClickAreas, 2)
                    If clsClickObject.IsInclude(m_clsClickAreas(X, Y).SX, m_clsClickAreas(X, Y).SY, m_clsClickAreas(X, Y).EX, m_clsClickAreas(X, Y).EY) Then
                        Call m_clsClickAreas(X, Y).RegistClickObject(clsClickObject)
                    End If
                Next
            Next
            Set RegistBased = clsClickObject
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '基準点を登録する。
        '
        '指定された基準点のクリックオブジェクトを生成し登録する。
        '基準点は長方形クリックオブジェクト。
        '
        '引き数：
        'clsObservationPoint 登録する観測点。
        '
        '戻り値：登録したクリックオブジェクト。
        */
        public ClickRect RegistBased(ObservationPoint clsObservationPoint)
        {
            double nX;
            double nY;
            nX = clsObservationPoint.DevicePoint().X;
            nY = clsObservationPoint.DevicePoint().Y;
            ClickRect clsClickObject = new ClickRect();
            clsClickObject.Object = clsObservationPoint;
            clsClickObject.SetParam(nX - BasedRadius, nY - BasedRadius, BasedRadius * 2, BasedRadius * 2);
#if false
            Dim X As Long
            Dim Y As Long
            For X = 0 To UBound(m_clsClickAreas, 1)
                For Y = 0 To UBound(m_clsClickAreas, 2)
                    If clsClickObject.IsInclude(m_clsClickAreas(X, Y).SX, m_clsClickAreas(X, Y).SY, m_clsClickAreas(X, Y).EX, m_clsClickAreas(X, Y).EY) Then
                        Call m_clsClickAreas(X, Y).RegistClickObject(clsClickObject)
                    End If
                Next
            Next
#endif
            return clsClickObject;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '固定点を登録する。
        '
        '指定された固定点のクリックオブジェクトを生成し登録する。
        '固定点は三角形クリックオブジェクト。
        '
        '引き数：
        'clsObservationPoint 登録する観測点。
        '
        '戻り値：登録したクリックオブジェクト。
        Public Function RegistFixed(ByVal clsObservationPoint As ObservationPoint) As ClickTriangle
            Dim nX As Double
            Dim nY As Double
            nX = clsObservationPoint.DevicePoint.X
            nY = clsObservationPoint.DevicePoint.Y
            Dim clsClickObject As New ClickTriangle
            Set clsClickObject.Object = clsObservationPoint
            Call clsClickObject.SetParam(nX, nY, FixedRadius)
            Dim X As Long
            Dim Y As Long
            For X = 0 To UBound(m_clsClickAreas, 1)
                For Y = 0 To UBound(m_clsClickAreas, 2)
                    If clsClickObject.IsInclude(m_clsClickAreas(X, Y).SX, m_clsClickAreas(X, Y).SY, m_clsClickAreas(X, Y).EX, m_clsClickAreas(X, Y).EY) Then
                        Call m_clsClickAreas(X, Y).RegistClickObject(clsClickObject)
                    End If
                Next
            Next
            Set RegistFixed = clsClickObject
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '固定点を登録する。
        '
        '指定された固定点のクリックオブジェクトを生成し登録する。
        '固定点は三角形クリックオブジェクト。
        '
        '引き数：
        'clsObservationPoint 登録する観測点。
        '
        '戻り値：登録したクリックオブジェクト。
        */
        public ClickTriangle RegistFixed(ObservationPoint clsObservationPoint)
        {
            double nX;
            double nY;
            nX = clsObservationPoint.DevicePoint().X;
            nY = clsObservationPoint.DevicePoint().Y;
            ClickTriangle clsClickObject = new ClickTriangle();
            clsClickObject.Object = clsObservationPoint;
            clsClickObject.SetParam(nX, nY, FixedRadius);
#if false
            long X;
            long Y;
            For X = 0 To UBound(m_clsClickAreas, 1)
                For Y = 0 To UBound(m_clsClickAreas, 2)
                    If clsClickObject.IsInclude(m_clsClickAreas(X, Y).SX, m_clsClickAreas(X, Y).SY, m_clsClickAreas(X, Y).EX, m_clsClickAreas(X, Y).EY) Then
                        Call m_clsClickAreas(X, Y).RegistClickObject(clsClickObject)
                    End If
                Next
            Next
#endif
            return clsClickObject;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '移動点を登録する。
        '
        '指定された移動点(または偏心点等)のクリックオブジェクトを生成し登録する。
        '移動点は円形クリックオブジェクト。
        '
        '引き数：
        'clsObservationPoint 登録する観測点。
        '
        '戻り値：登録したクリックオブジェクト。
        Public Function RegistPoint(ByVal clsObservationPoint As ObservationPoint) As ClickCircle
            Dim nX As Double
            Dim nY As Double
            nX = clsObservationPoint.DevicePoint.X
            nY = clsObservationPoint.DevicePoint.Y
            Dim clsClickObject As New ClickCircle
            Set clsClickObject.Object = clsObservationPoint
            Call clsClickObject.SetParam(nX, nY, PointRadius)
            Dim X As Long
            Dim Y As Long
            For X = 0 To UBound(m_clsClickAreas, 1)
                For Y = 0 To UBound(m_clsClickAreas, 2)
                    If clsClickObject.IsInclude(m_clsClickAreas(X, Y).SX, m_clsClickAreas(X, Y).SY, m_clsClickAreas(X, Y).EX, m_clsClickAreas(X, Y).EY) Then
                        Call m_clsClickAreas(X, Y).RegistClickObject(clsClickObject)
                    End If
                Next
            Next
            Set RegistPoint = clsClickObject
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '移動点を登録する。
        '
        '指定された移動点(または偏心点等)のクリックオブジェクトを生成し登録する。
        '移動点は円形クリックオブジェクト。
        '
        '引き数：
        'clsObservationPoint 登録する観測点。
        '
        '戻り値：登録したクリックオブジェクト。
        */
        public ClickCircle RegistPoint(ObservationPoint clsObservationPoint)
        {
            double nX;
            double nY;
            nX = clsObservationPoint.DevicePoint().X;
            nY = clsObservationPoint.DevicePoint().Y;
            ClickCircle clsClickObject = new ClickCircle();
            clsClickObject.Object = clsObservationPoint;
            clsClickObject.SetParam(nX, nY, PointRadius);
#if false
            Dim X As Long
            Dim Y As Long
            For X = 0 To UBound(m_clsClickAreas, 1)
                For Y = 0 To UBound(m_clsClickAreas, 2)
                    If clsClickObject.IsInclude(m_clsClickAreas(X, Y).SX, m_clsClickAreas(X, Y).SY, m_clsClickAreas(X, Y).EX, m_clsClickAreas(X, Y).EY) Then
                        Call m_clsClickAreas(X, Y).RegistClickObject(clsClickObject)
                    End If
                Next
            Next
#endif
            return clsClickObject;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '基線ベクトルを登録する。
        '
        '指定された基線ベクトルのクリックオブジェクトを生成し登録する。
        '
        '引き数：
        'clsBaseLineVector 登録する基線ベクトル。
        '
        '戻り値：登録したクリックオブジェクト。
        Public Function RegistVector(ByVal clsBaseLineVector As BaseLineVector) As ClickVector
            Dim clsTwipsPoint As TwipsPoint
            Dim nSX As Double
            Dim nSY As Double
            Dim nEX As Double
            Dim nEY As Double
            Set clsTwipsPoint = clsBaseLineVector.StrPoint.TopParentPoint.DevicePoint
            nSX = clsTwipsPoint.X
            nSY = clsTwipsPoint.Y
            Set clsTwipsPoint = clsBaseLineVector.EndPoint.TopParentPoint.DevicePoint
            nEX = clsTwipsPoint.X
            nEY = clsTwipsPoint.Y
            Dim clsClickObject As New ClickVector
            Set clsClickObject.Object = clsBaseLineVector
            Call clsClickObject.SetParam(nSX, nSY, nEX, nEY, (ArrowLength + PointRadius), ArrowWidth)
            Dim X As Long
            Dim Y As Long
            For X = 0 To UBound(m_clsClickAreas, 1)
                For Y = 0 To UBound(m_clsClickAreas, 2)
                    If clsClickObject.IsInclude(m_clsClickAreas(X, Y).SX, m_clsClickAreas(X, Y).SY, m_clsClickAreas(X, Y).EX, m_clsClickAreas(X, Y).EY) Then
                        Call m_clsClickAreas(X, Y).RegistClickObject(clsClickObject)
                    End If
                Next
            Next
            Set RegistVector = clsClickObject
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '基線ベクトルを登録する。
        '
        '指定された基線ベクトルのクリックオブジェクトを生成し登録する。
        '
        '引き数：
        'clsBaseLineVector 登録する基線ベクトル。
        '
        '戻り値：登録したクリックオブジェクト。
        */
        public ClickVector RegistVector(BaseLineVector clsBaseLineVector)
        {
            TwipsPoint clsTwipsPoint;
            double nSX;
            double nSY;
            double nEX;
            double nEY;
            clsTwipsPoint = clsBaseLineVector.StrPoint().TopParentPoint().DevicePoint();
            nSX = clsTwipsPoint.X;
            nSY = clsTwipsPoint.Y;
            clsTwipsPoint = clsBaseLineVector.EndPoint().TopParentPoint().DevicePoint();
            nEX = clsTwipsPoint.X;
            nEY = clsTwipsPoint.Y;
            ClickVector clsClickObject = new ClickVector();
            clsClickObject.Object = clsBaseLineVector;
            clsClickObject.SetParam(nSX, nSY, nEX, nEY, (ArrowLength + PointRadius), ArrowWidth);
#if false
            Dim X As Long
            Dim Y As Long
            For X = 0 To UBound(m_clsClickAreas, 1)
                For Y = 0 To UBound(m_clsClickAreas, 2)
                    If clsClickObject.IsInclude(m_clsClickAreas(X, Y).SX, m_clsClickAreas(X, Y).SY, m_clsClickAreas(X, Y).EX, m_clsClickAreas(X, Y).EY) Then
                        Call m_clsClickAreas(X, Y).RegistClickObject(clsClickObject)
                    End If
                Next
            Next
#endif
            return clsClickObject;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '観測点記号を登録する。
        '
        '指定された観測点記号のクリックオブジェクトを生成し登録する。
        '観測点記号は長方形クリックオブジェクト。
        '
        '引き数：
        'clsObservationPoint 登録する観測点。
        'nLeft 論理エリア上の観測点記号左辺(ピクセル)。
        'nTop 論理エリア上の観測点記号上辺(ピクセル)。
        'nWidth 観測点記号幅(ピクセル)。
        'nHeight 観測点記号高さ(ピクセル)。
        '
        '戻り値：登録したクリックオブジェクト。
        Public Function RegistSymbol(ByVal clsObservationPoint As ObservationPoint, ByVal nLeft As Double, ByVal nTop As Double, ByVal nWidth As Double, ByVal nHeight As Double) As ClickRect
            Dim clsClickObject As New ClickRect
            Set clsClickObject.Object = clsObservationPoint
            Call clsClickObject.SetParam(nLeft, nTop, nWidth, nHeight)
            Dim X As Long
            Dim Y As Long
            For X = 0 To UBound(m_clsClickAreas, 1)
                For Y = 0 To UBound(m_clsClickAreas, 2)
                    If clsClickObject.IsInclude(m_clsClickAreas(X, Y).SX, m_clsClickAreas(X, Y).SY, m_clsClickAreas(X, Y).EX, m_clsClickAreas(X, Y).EY) Then
                        Call m_clsClickAreas(X, Y).RegistClickObject(clsClickObject)
                    End If
                Next
            Next
            Set RegistSymbol = clsClickObject
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '観測点記号を登録する。
        '
        '指定された観測点記号のクリックオブジェクトを生成し登録する。
        '観測点記号は長方形クリックオブジェクト。
        '
        '引き数：
        'clsObservationPoint 登録する観測点。
        'nLeft 論理エリア上の観測点記号左辺(ピクセル)。
        'nTop 論理エリア上の観測点記号上辺(ピクセル)。
        'nWidth 観測点記号幅(ピクセル)。
        'nHeight 観測点記号高さ(ピクセル)。
        '
        '戻り値：登録したクリックオブジェクト。
        */
        public ClickRect RegistSymbol(ObservationPoint clsObservationPoint, double nLeft, double nTop, double nWidth, double nHeight)
        {
            ClickRect clsClickObject = new ClickRect();
            clsClickObject.Object = clsObservationPoint;
            clsClickObject.SetParam(nLeft, nTop, nWidth, nHeight);
#if false
            Dim X As Long
            Dim Y As Long
            For X = 0 To UBound(m_clsClickAreas, 1)
                For Y = 0 To UBound(m_clsClickAreas, 2)
                    If clsClickObject.IsInclude(m_clsClickAreas(X, Y).SX, m_clsClickAreas(X, Y).SY, m_clsClickAreas(X, Y).EX, m_clsClickAreas(X, Y).EY) Then
                        Call m_clsClickAreas(X, Y).RegistClickObject(clsClickObject)
                    End If
                Next
            Next
#endif
            return clsClickObject;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '指定された座標でクリックされるオブジェクトを取得する。
        '
        '指定された座標にあるオブジェクトのクリックオブジェクトを返す。
        '複数のオブジェクトが重なっている場合、HitPoint を繰り返し呼ぶことにより順番にそれらのオブジェクトが取得できる。
        'その場合 objCurrent に前回の HitPoint の呼び出しで取得されたクリックオブジェクトを指定する。
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
            Set HitObject = GetClickArea(m_clsClickAreas, nX, nY).HitObject(nX, nY, objCurrent)
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '指定された座標でクリックされるオブジェクトを取得する。
        '
        '指定された座標にあるオブジェクトのクリックオブジェクトを返す。
        '複数のオブジェクトが重なっている場合、HitPoint を繰り返し呼ぶことにより順番にそれらのオブジェクトが取得できる。
        'その場合 objCurrent に前回の HitPoint の呼び出しで取得されたクリックオブジェクトを指定する。
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
            return GetClickArea(m_clsClickAreas, nX, nY).HitObject(nX, nY, objCurrent);
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
        'objFirst 取得されたコレクションの中で順位が一番上のオブジェクトを設定する。
        '
        '戻り値：指定した座標にあるオブジェクトのクリックオブジェクトのコレクションを返す。要素はオブジェクト。キーは要素のポインタ。
        Public Function HitObjectAll(ByVal nX As Double, ByVal nY As Double, Optional ByRef objFirst As Object) As Collection
            Dim clsObjects() As Object
            clsObjects = GetClickArea(m_clsClickAreas, nX, nY).HitObjectAll(nX, nY)
            Dim i As Long
            Set HitObjectAll = New Collection
            For i = UBound(clsObjects) To 0 Step -1
                Call HitObjectAll.Add(clsObjects(i), Hex$(GetPointer(clsObjects(i))))
            Next
            If UBound(clsObjects) >= 0 Then Set objFirst = clsObjects(0)
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
        'objFirst 取得されたコレクションの中で順位が一番上のオブジェクトを設定する。
        '
        '戻り値：指定した座標にあるオブジェクトのクリックオブジェクトのコレクションを返す。要素はオブジェクト。キーは要素のポインタ。
        */
        public ICollection HitObjectAll(double nX, double nY, ref object objFirst)
        {
            return null;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '*******************************************************************************
        'インプリメンテーション

        '指定された座標でクリックされるエリアを取得する。
        '
        '指定されたクリックエリアの配列の中から、指定された座標にあるクリックエリアを返す。
        '
        '引き数：
        'clsClickArea クリックエリアの配列。配列の要素は(-1 To ...)、要素 -1 は未使用。
        'nX 論理エリア上のX座標(ピクセル)。
        'nY 論理エリア上のY座標(ピクセル)。
        '
        '戻り値：指定された座標でクリックされるクリックエリアを返す。
        Public Function GetClickArea(ByRef clsClickArea() As ClickArea, ByVal nX As Double, ByVal nY As Double) As ClickArea
            Dim X As Long
            Dim Y As Long
            For X = 0 To UBound(clsClickArea, 1)
                If nX<clsClickArea(X, 0).EX Then Exit For
            Next
            For Y = 0 To UBound(clsClickArea, 2)
                If nY<clsClickArea(0, Y).EY Then Exit For
            Next
            If X > UBound(clsClickArea, 1) Then X = UBound(clsClickArea, 1)
            If Y > UBound(clsClickArea, 2) Then Y = UBound(clsClickArea, 2)
            Set GetClickArea = clsClickArea(X, Y)
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '*******************************************************************************
        'インプリメンテーション
        
        '指定された座標でクリックされるエリアを取得する。
        '
        '指定されたクリックエリアの配列の中から、指定された座標にあるクリックエリアを返す。
        '
        '引き数：
        'clsClickArea クリックエリアの配列。配列の要素は(-1 To ...)、要素 -1 は未使用。
        'nX 論理エリア上のX座標(ピクセル)。
        'nY 論理エリア上のY座標(ピクセル)。
        '
        '戻り値：指定された座標でクリックされるクリックエリアを返す。
        */
        public ClickArea GetClickArea(ClickArea clsClickArea, double nX, double nY)
        {
            return null;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '*******************************************************************************
        'デバッグ

        'クリックエリアを描画する。
        Public Sub DebugDrawArea(ByVal objDevice As Object, ByVal nDeviceLeft As Double, ByVal nDeviceTop As Double)
            Dim X As Long
            Dim Y As Long
            For X = 0 To UBound(m_clsClickAreas, 1)
                For Y = 0 To UBound(m_clsClickAreas, 2)
                    objDevice.Line(m_clsClickAreas(X, Y).SX - nDeviceLeft, m_clsClickAreas(X, Y).SY - nDeviceTop)-(m_clsClickAreas(X, Y).SX - nDeviceLeft, m_clsClickAreas(X, Y).EY - nDeviceTop)
                    objDevice.Line(m_clsClickAreas(X, Y).SX - nDeviceLeft, m_clsClickAreas(X, Y).EY - nDeviceTop)-(m_clsClickAreas(X, Y).EX - nDeviceLeft, m_clsClickAreas(X, Y).EY - nDeviceTop)
                    objDevice.Line(m_clsClickAreas(X, Y).EX - nDeviceLeft, m_clsClickAreas(X, Y).EY - nDeviceTop)-(m_clsClickAreas(X, Y).EX - nDeviceLeft, m_clsClickAreas(X, Y).SY - nDeviceTop)
                    objDevice.Line(m_clsClickAreas(X, Y).EX - nDeviceLeft, m_clsClickAreas(X, Y).SY - nDeviceTop)-(m_clsClickAreas(X, Y).SX - nDeviceLeft, m_clsClickAreas(X, Y).SY - nDeviceTop)
                Next
            Next
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '*******************************************************************************
        'デバッグ
        
        'クリックエリアを描画する。
        */
        public void DebugDrawArea(object objDevice, double nDeviceLeft, double nDeviceTop)
        {
            return;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'クリックオブジェクトを描画する。
        Public Sub DebugDrawObject(ByVal objDevice As Object, ByVal nDeviceLeft As Double, ByVal nDeviceTop As Double, ByVal nX As Double, ByVal nY As Double)
            nX = nX + nDeviceLeft
            nY = nY + nDeviceTop
            Dim X As Long
            Dim Y As Long
            For X = 0 To UBound(m_clsClickAreas, 1)
                If nX<m_clsClickAreas(X, 0).EX Then Exit For
            Next
            For Y = 0 To UBound(m_clsClickAreas, 2)
                If nY<m_clsClickAreas(0, Y).EY Then Exit For
            Next
            If X > UBound(m_clsClickAreas, 1) Then X = UBound(m_clsClickAreas, 1)
            If Y > UBound(m_clsClickAreas, 2) Then Y = UBound(m_clsClickAreas, 2)
            Dim i As Long
            For i = 0 To m_clsClickAreas(X, Y).ClickObjectCount - 1
                Call m_clsClickAreas(X, Y).ClickObject(i).DebugDraw(objDevice, nDeviceLeft, nDeviceTop)
            Next
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'クリックオブジェクトを描画する。
        public void DebugDrawObject(object objDevice, double nDeviceLeft, double nDeviceTop, double nX, double nY)
        {
            return;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'クリックオブジェクトを描画する。
        Public Sub DebugDrawObjectAll(ByVal objDevice As Object, ByVal nDeviceLeft As Double, ByVal nDeviceTop As Double)
            Dim X As Long
            Dim Y As Long
            For X = 0 To UBound(m_clsClickAreas, 1)
                For Y = 0 To UBound(m_clsClickAreas, 2)
                    Dim i As Long
                    For i = 0 To m_clsClickAreas(X, Y).ClickObjectCount - 1
                        Call m_clsClickAreas(X, Y).ClickObject(i).DebugDraw(objDevice, nDeviceLeft, nDeviceTop)
                    Next
                Next
            Next
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'クリックオブジェクトを描画する。
        public void DebugDrawObjectAll(object objDevice, double nDeviceLeft, double nDeviceTop)
        {
            return;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'クリックエリアに所属しているオブジェクトを列挙する。
        Public Sub DebugPrint()
            Dim X As Long
            Dim Y As Long
            For X = 0 To UBound(m_clsClickAreas, 1)
                For Y = 0 To UBound(m_clsClickAreas, 2)
                    Dim i As Long
                    For i = 0 To m_clsClickAreas(X, Y).ClickObjectCount - 1
                        Debug.Print "(" & CStr(X) & "," & CStr(Y) & ")=" & m_clsClickAreas(X, Y).ClickObject(i).Object.Number
                    Next
                Next
            Next
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'クリックエリアに所属しているオブジェクトを列挙する。
        public void DebugPrint()
        {
            return;
        }
        //==========================================================================================
    }
}
