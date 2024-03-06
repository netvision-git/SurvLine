using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SurvLine.mdl;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using static SurvLine.mdl.DEFINE;
using static SurvLine.mdl.MdlMain;

namespace SurvLine
{
    public class ClickVector
    {
        //==========================================================================================
        /*[VB]
        '*******************************************************************************
        'ベクトルクリックオブジェクト
        '
        '基線ベクトルのクリックオブジェクト。

        Option Explicit
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        Implements ClickObjectInterface

        'プロパティ
        Public Object As BaseLineVector '基線ベクトル。
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
        public BaseLineVector Object;   //'基線ベクトル。
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'インプリメンテーション
        Private m_bParallel As Boolean '平行フラグ。
        Private m_nA As Double '傾き。
        Private m_nTC As Double '上辺Ｃ。
        Private m_nBC As Double '下辺Ｃ。
        Private m_nLC As Double '左辺Ｃ。
        Private m_nRC As Double '右辺Ｃ。
        Private m_nTX As Double '上端。
        Private m_nTY As Double '上端。
        Private m_nBX As Double '下端。
        Private m_nBY As Double '下端。
        Private m_nLX As Double '左端。
        Private m_nLY As Double '左端。
        Private m_nRX As Double '右端。
        Private m_nRY As Double '右端。
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'インプリメンテーション
        private bool m_bParallel;   //'平行フラグ。
        private double m_nA;        //'傾き。
        private double m_nTC;       //'上辺Ｃ。
        private double m_nBC;       //'下辺Ｃ。
        private double m_nLC;       //'左辺Ｃ。
        private double m_nRC;       //'右辺Ｃ。
        private double m_nTX;       //'上端。
        private double m_nTY;       //'上端。
        private double m_nBX;       //'下端。
        private double m_nBY;       //'下端。
        private double m_nLX;       //'左端。
        private double m_nLY;       //'左端。
        private double m_nRX;       //'右端。
        private double m_nRY;       //'右端。

        private MdlMain mdlMain;
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '*******************************************************************************
        'イベント

        '初期化。
        Private Sub Class_Initialize()

            On Error GoTo ErrorHandler


            m_bParallel = True
            m_nTC = 0
            m_nBC = 0
            m_nLC = 0
            m_nRC = 0
    
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
                m_bParallel = true;
                m_nTC = 0;
                m_nBC = 0;
                m_nLC = 0;
                m_nRC = 0;
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
        private void ClickObjectInterface_Object(BaseLineVector RHS)
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
        private BaseLineVector ClickObjectInterface_Object()
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

            If m_nTY > nEY Then Exit Function
            If m_nBY<nSY Then Exit Function
            If m_nLX> nEX Then Exit Function
            If m_nRX < nSX Then Exit Function


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
            if (m_nTY > nEY) { return false; }
            if (m_nBY < nSY) { return false; }
            if (m_nLX > nEX) { return false; }
            if (m_nRX < nSX) { return false; }
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
            If m_bParallel Then
                If nY<m_nTC Then Exit Function
                If nY> m_nBC Then Exit Function
                If nX < m_nLC Then Exit Function
                If nX > m_nRC Then Exit Function
            Else
                If nY<m_nA* nX + m_nTC Then Exit Function
                If nY> m_nA * nX + m_nBC Then Exit Function
                If nX < -m_nA* nY + m_nLC Then Exit Function
                If nX > -m_nA* nY + m_nRC Then Exit Function
            End If
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
            if (m_bParallel)
            {
                if (nY < m_nTC) { return false; }
                if (nY > m_nBC) { return false; }
                if (nX < m_nLC) { return false; }
                if (nX > m_nRC) { return false; }
            }
            else
            {
                if (nY < m_nA * nX + m_nTC) { return false; }
                if (nY > m_nA * nX + m_nBC) { return false; }
                if (nX < -m_nA * nY + m_nLC) { return false; }
                if (nX > -m_nA * nY + m_nRC) { return false; }
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
        'nSX 論理エリア上の始点座標(ピクセル)。
        'nSY 論理エリア上の始点座標(ピクセル)。
        'nEX 論理エリア上の終点座標(ピクセル)。
        'nEY 論理エリア上の終点座標(ピクセル)。
        'nLength 矢印長さ(ピクセル)。
        'nWidth 矢印幅(ピクセル)。
        Public Sub SetParam(ByVal nSX As Double, ByVal nSY As Double, ByVal nEX As Double, ByVal nEY As Double, ByVal nLength As Double, ByVal nWidth As Double)
            Dim nDX As Double
            Dim nDY As Double
            nDX = Abs(nEX - nSX)
            nDY = Abs(nEY - nSY)
            If nDX > FLT_EPSILON And nDY > FLT_EPSILON Then
                '軸に並行ではない。
                m_bParallel = False
                Dim nDL As Double
                Dim nWX As Double
                Dim nWY As Double
                nDL = Sqr(nDX ^ 2 + nDY ^ 2)
                nWX = nWidth* nDY / nDL
                nWY = nWidth * nDX / nDL
                If nDL<nLength Then
                    '矢印より短い。
                    nSX = nEX - nLength * (nEX - nSX) / nDL
                    nSY = nEY - nLength * (nEY - nSY) / nDL
                End If
                If nDX < nDY Then
                    'どちらかというとベクトルの向きは上下。
                    m_nA = -(nEX - nSX) / (nEY - nSY)
                    If nSY < nEY Then
                        '下向き。
                        m_nTY = nSY - nWY
                        m_nBY = nEY + nWY
                        m_nTC = nSY - m_nA * nSX
                        m_nBC = nEY - m_nA * nEX
                        If nSX < nEX Then
                            '右向き寄り。
                            m_nTX = nSX + nWX
                            m_nBX = nEX - nWX
                            m_nLX = nSX - nWX
                            m_nRX = nEX + nWX
                            m_nLY = nSY + nWY
                            m_nRY = nEY - nWY
                            m_nLC = m_nLX + m_nA * m_nLY
                            m_nRC = m_nRX + m_nA * m_nRY
                        Else
                            '左向き寄り。
                            m_nTX = nSX - nWX
                            m_nBX = nEX + nWX
                            m_nLX = nEX - nWX
                            m_nRX = nSX + nWX
                            m_nLY = nEY - nWY
                            m_nRY = nSY + nWY
                            m_nLC = m_nLX + m_nA * m_nLY
                            m_nRC = m_nRX + m_nA * m_nRY
                        End If
                    Else
                        '上向き。
                        m_nTY = nEY - nWY
                        m_nBY = nSY + nWY
                        m_nTC = nEY - m_nA * nEX
                        m_nBC = nSY - m_nA * nSX
                        If nSX < nEX Then
                            '右向き寄り。
                            m_nTX = nEX - nWX
                            m_nBX = nSX + nWX
                            m_nLX = nSX - nWX
                            m_nRX = nEX + nWX
                            m_nLY = nSY - nWY
                            m_nRY = nEY + nWY
                            m_nLC = m_nLX + m_nA * m_nLY
                            m_nRC = m_nRX + m_nA * m_nRY
                        Else
                            '左向き寄り。
                            m_nTX = nEX + nWX
                            m_nBX = nSX - nWX
                            m_nLX = nEX - nWX
                            m_nRX = nSX + nWX
                            m_nLY = nEY + nWY
                            m_nRY = nSY - nWY
                            m_nLC = m_nLX + m_nA * m_nLY
                            m_nRC = m_nRX + m_nA * m_nRY
                        End If
                    End If
                Else
                    'どちらかというとベクトルの向きは左右。
                    m_nA = (nEY - nSY) / (nEX - nSX)
                    If nSX < nEX Then
                        '右向き。
                        m_nLX = nSX - nWX
                        m_nRX = nEX + nWX
                        m_nLC = nSX + m_nA * nSY
                        m_nRC = nEX + m_nA * nEY
                        If nSY < nEY Then
                            '下向き寄り。
                            m_nLY = nSY + nWY
                            m_nRY = nEY - nWY
                            m_nTY = nSY - nWY
                            m_nBY = nEY + nWY
                            m_nTX = nSX + nWX
                            m_nBX = nEX - nWX
                            m_nTC = m_nTY - m_nA * m_nTX
                            m_nBC = m_nBY - m_nA * m_nBX
                        Else
                            '上向き寄り。
                            m_nLY = nSY - nWY
                            m_nRY = nEY + nWY
                            m_nTY = nEY - nWY
                            m_nBY = nSY + nWY
                            m_nTX = nEX - nWX
                            m_nBX = nSX + nWX
                            m_nTC = m_nTY - m_nA * m_nTX
                            m_nBC = m_nBY - m_nA * m_nBX
                        End If
                    Else
                        '左向き。
                        m_nLX = nEX - nWX
                        m_nRX = nSX + nWX
                        m_nLC = nEX + m_nA * nEY
                        m_nRC = nSX + m_nA * nSY
                        If nSY < nEY Then
                            '下向き寄り。
                            m_nLY = nEY - nWY
                            m_nRY = nSY + nWY
                            m_nTY = nSY - nWY
                            m_nBY = nEY + nWY
                            m_nTX = nSX - nWX
                            m_nBX = nEX + nWX
                            m_nTC = m_nTY - m_nA * m_nTX
                            m_nBC = m_nBY - m_nA * m_nBX
                        Else
                            '上向き寄り。
                            m_nLY = nEY + nWY
                            m_nRY = nSY - nWY
                            m_nTY = nEY - nWY
                            m_nBY = nSY + nWY
                            m_nTX = nEX + nWX
                            m_nBX = nSX - nWX
                            m_nTC = m_nTY - m_nA * m_nTX
                            m_nBC = m_nBY - m_nA * m_nBX
                        End If
                    End If
                End If
            Else
                '軸に平行である。
                m_bParallel = True
                If nDX < nDY Then
                    'ベクトルの向きは上下。
                    m_nLC = nSX - nWidth
                    m_nRC = nSX + nWidth
                    If nSY < nEY Then
                        '下向き。
                        m_nBC = nEY
                        If nEY - nSY<nLength Then
                            m_nTC = nEY - nLength
                        Else
                            m_nTC = nSY
                        End If
                    Else
                        '上向き。
                        m_nTC = nEY
                        If nSY - nEY<nLength Then
                            m_nBC = nEY + nLength
                        Else
                            m_nBC = nSY
                        End If
                    End If
                Else
                    'ベクトルの向きは左右。
                    m_nTC = nSY - nWidth
                    m_nBC = nSY + nWidth
                    If nSX<nEX Then
                        '右向き。
                        m_nRC = nEX
                        If nEX - nSX<nLength Then
                            m_nLC = nEX - nLength
                        Else
                            m_nLC = nSX
                        End If
                    Else
                        '左向き。
                        m_nLC = nEX
                        If nSX - nEX<nLength Then
                            m_nRC = nEX + nLength
                        Else
                            m_nRC = nSX
                        End If
                    End If
                End If
                m_nTY = m_nTC
                m_nBY = m_nBC
                m_nLX = m_nLC
                m_nRX = m_nRC
            End If
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
        'nSX 論理エリア上の始点座標(ピクセル)。
        'nSY 論理エリア上の始点座標(ピクセル)。
        'nEX 論理エリア上の終点座標(ピクセル)。
        'nEY 論理エリア上の終点座標(ピクセル)。
        'nLength 矢印長さ(ピクセル)。
        'nWidth 矢印幅(ピクセル)。
        */
        public void SetParam(double nSX, double nSY, double nEX, double nEY, double nLength, double nWidth)
        {
            double nDX;
            double nDY;
            nDX = Math.Abs(nEX - nSX);
            nDY = Math.Abs(nEY - nSY);
            if (nDX > FLT_EPSILON && nDY > FLT_EPSILON)
            {
                //'軸に並行ではない。
                m_bParallel = false;
                double nDL;
                double nWX;
                double nWY;

                nDL = Math.Sqrt(Math.Pow(nDX, 2) + Math.Pow(nDY, 2));
                nWX = nWidth * nDY / nDL;
                nWY = nWidth * nDX / nDL;
                if (nDL < nLength)
                {
                    //'矢印より短い。
                    nSX = nEX - nLength * (nEX - nSX) / nDL;
                    nSY = nEY - nLength * (nEY - nSY) / nDL;
                }
                if (nDX < nDY)
                {
                    //'どちらかというとベクトルの向きは上下。
                    m_nA = -(nEX - nSX) / (nEY - nSY);
                    if (nSY < nEY)
                    {
                        //'下向き。
                        m_nTY = nSY - nWY;
                        m_nBY = nEY + nWY;
                        m_nTC = nSY - m_nA * nSX;
                        m_nBC = nEY - m_nA * nEX;
                        if (nSX < nEX)
                        {
                            //'右向き寄り。
                            m_nTX = nSX + nWX;
                            m_nBX = nEX - nWX;
                            m_nLX = nSX - nWX;
                            m_nRX = nEX + nWX;
                            m_nLY = nSY + nWY;
                            m_nRY = nEY - nWY;
                            m_nLC = m_nLX + m_nA * m_nLY;
                            m_nRC = m_nRX + m_nA * m_nRY;
                        }
                        else
                        {
                            //'左向き寄り。
                            m_nTX = nSX - nWX;
                            m_nBX = nEX + nWX;
                            m_nLX = nEX - nWX;
                            m_nRX = nSX + nWX;
                            m_nLY = nEY - nWY;
                            m_nRY = nSY + nWY;
                            m_nLC = m_nLX + m_nA * m_nLY;
                            m_nRC = m_nRX + m_nA * m_nRY;
                        }
                    }
                    else
                    {
                        //'上向き。
                        m_nTY = nEY - nWY;
                        m_nBY = nSY + nWY;
                        m_nTC = nEY - m_nA * nEX;
                        m_nBC = nSY - m_nA * nSX;
                        if (nSX < nEX)
                        {
                            //'右向き寄り。
                            m_nTX = nEX - nWX;
                            m_nBX = nSX + nWX;
                            m_nLX = nSX - nWX;
                            m_nRX = nEX + nWX;
                            m_nLY = nSY - nWY;
                            m_nRY = nEY + nWY;
                            m_nLC = m_nLX + m_nA * m_nLY;
                            m_nRC = m_nRX + m_nA * m_nRY;
                        }
                        else
                        {
                            //'左向き寄り。
                            m_nTX = nEX + nWX;
                            m_nBX = nSX - nWX;
                            m_nLX = nEX - nWX;
                            m_nRX = nSX + nWX;
                            m_nLY = nEY + nWY;
                            m_nRY = nSY - nWY;
                            m_nLC = m_nLX + m_nA * m_nLY;
                            m_nRC = m_nRX + m_nA * m_nRY;
                        }
                    }
                }
                else
                {
                    //'どちらかというとベクトルの向きは左右。
                    m_nA = (nEY - nSY) / (nEX - nSX);
                    if (nSX < nEX)
                    {
                        //'右向き。
                        m_nLX = nSX - nWX;
                        m_nRX = nEX + nWX;
                        m_nLC = nSX + m_nA * nSY;
                        m_nRC = nEX + m_nA * nEY;
                        if (nSY < nEY)
                        {
                            //'下向き寄り。
                            m_nLY = nSY + nWY;
                            m_nRY = nEY - nWY;
                            m_nTY = nSY - nWY;
                            m_nBY = nEY + nWY;
                            m_nTX = nSX + nWX;
                            m_nBX = nEX - nWX;
                            m_nTC = m_nTY - m_nA * m_nTX;
                            m_nBC = m_nBY - m_nA * m_nBX;
                        }
                        else
                        {
                            //'上向き寄り。
                            m_nLY = nSY - nWY;
                            m_nRY = nEY + nWY;
                            m_nTY = nEY - nWY;
                            m_nBY = nSY + nWY;
                            m_nTX = nEX - nWX;
                            m_nBX = nSX + nWX;
                            m_nTC = m_nTY - m_nA * m_nTX;
                            m_nBC = m_nBY - m_nA * m_nBX;
                        }
                    }
                    else
                    {
                        //'左向き。
                        m_nLX = nEX - nWX;
                        m_nRX = nSX + nWX;
                        m_nLC = nEX + m_nA * nEY;
                        m_nRC = nSX + m_nA * nSY;
                        if (nSY < nEY)
                        {
                            //'下向き寄り。
                            m_nLY = nEY - nWY;
                            m_nRY = nSY + nWY;
                            m_nTY = nSY - nWY;
                            m_nBY = nEY + nWY;
                            m_nTX = nSX - nWX;
                            m_nBX = nEX + nWX;
                            m_nTC = m_nTY - m_nA * m_nTX;
                            m_nBC = m_nBY - m_nA * m_nBX;
                        }
                        else
                        {
                            //'上向き寄り。
                            m_nLY = nEY + nWY;
                            m_nRY = nSY - nWY;
                            m_nTY = nEY - nWY;
                            m_nBY = nSY + nWY;
                            m_nTX = nEX + nWX;
                            m_nBX = nSX - nWX;
                            m_nTC = m_nTY - m_nA * m_nTX;
                            m_nBC = m_nBY - m_nA * m_nBX;
                        }
                    }
                }
            }
            else
            {
                //'軸に平行である。
                m_bParallel = true;
                if (nDX < nDY)
                {
                    //'ベクトルの向きは上下。
                    m_nLC = nSX - nWidth;
                    m_nRC = nSX + nWidth;
                    if (nSY < nEY)
                    {
                        //'下向き。
                        m_nBC = nEY;
                        if (nEY - nSY < nLength)
                        {
                            m_nTC = nEY - nLength;
                        }
                        else
                        {
                            m_nTC = nSY;
                        }
                    }
                    else
                    {
                        //'上向き。
                        m_nTC = nEY;
                        if (nSY - nEY < nLength)
                        {
                            m_nBC = nEY + nLength;
                        }
                        else
                        {
                            m_nBC = nSY;
                        }
                    }
                }
                else
                {
                    //'ベクトルの向きは左右。
                    m_nTC = nSY - nWidth;
                    m_nBC = nSY + nWidth;
                    if (nSX < nEX)
                    {
                        //'右向き。
                        m_nRC = nEX;
                        if (nEX - nSX < nLength)
                        {
                            m_nLC = nEX - nLength;
                        }
                        else
                        {
                            m_nLC = nSX;
                        }
                    }
                    else
                    {
                        //'左向き。
                        m_nLC = nEX;
                        if (nSX - nEX < nLength)
                        {
                            m_nRC = nEX + nLength;
                        }
                        else
                        {
                            m_nRC = nSX;
                        }
                    }
                }
                m_nTY = m_nTC;
                m_nBY = m_nBC;
                m_nLX = m_nLC;
                m_nRX = m_nRC;
            }
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
            If m_bParallel Then
                objDevice.Line(m_nLC - nDeviceLeft, m_nTC - nDeviceTop)-(m_nRC - nDeviceLeft, m_nTC - nDeviceTop)
                objDevice.Line(m_nLC - nDeviceLeft, m_nBC - nDeviceTop)-(m_nRC - nDeviceLeft, m_nBC - nDeviceTop)
                objDevice.Line(m_nLC - nDeviceLeft, m_nTC - nDeviceTop)-(m_nLC - nDeviceLeft, m_nBC - nDeviceTop)
                objDevice.Line(m_nRC - nDeviceLeft, m_nTC - nDeviceTop)-(m_nRC - nDeviceLeft, m_nBC - nDeviceTop)
            Else
                objDevice.Line(m_nTX - nDeviceLeft, m_nTY - nDeviceTop)-(m_nLX - nDeviceLeft, m_nLY - nDeviceTop)
                objDevice.Line(m_nLX - nDeviceLeft, m_nLY - nDeviceTop)-(m_nBX - nDeviceLeft, m_nBY - nDeviceTop)
                objDevice.Line(m_nBX - nDeviceLeft, m_nBY - nDeviceTop)-(m_nRX - nDeviceLeft, m_nRY - nDeviceTop)
                objDevice.Line(m_nRX - nDeviceLeft, m_nRY - nDeviceTop)-(m_nTX - nDeviceLeft, m_nTY - nDeviceTop)
            End If
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '*******************************************************************************
        'デバッグ
        
        'デバッグ描画。
        */
        private void ClickObjectInterface_DebugDraw(BaseLineVector objDevice, double nDeviceLeft, double nDeviceTop)
        {
#if false
            /*
             *************************** 修正要 sakai
             */
            if (m_bParallel)
            {
                objDevice.Line(m_nLC - nDeviceLeft, m_nTC - nDeviceTop) - (m_nRC - nDeviceLeft, m_nTC - nDeviceTop);
                objDevice.Line(m_nLC - nDeviceLeft, m_nBC - nDeviceTop) - (m_nRC - nDeviceLeft, m_nBC - nDeviceTop);
                objDevice.Line(m_nLC - nDeviceLeft, m_nTC - nDeviceTop) - (m_nLC - nDeviceLeft, m_nBC - nDeviceTop);
                objDevice.Line(m_nRC - nDeviceLeft, m_nTC - nDeviceTop) - (m_nRC - nDeviceLeft, m_nBC - nDeviceTop);
            }
            else
            {
                objDevice.Line(m_nTX - nDeviceLeft, m_nTY - nDeviceTop) - (m_nLX - nDeviceLeft, m_nLY - nDeviceTop);
                objDevice.Line(m_nLX - nDeviceLeft, m_nLY - nDeviceTop) - (m_nBX - nDeviceLeft, m_nBY - nDeviceTop);
                objDevice.Line(m_nBX - nDeviceLeft, m_nBY - nDeviceTop) - (m_nRX - nDeviceLeft, m_nRY - nDeviceTop);
                objDevice.Line(m_nRX - nDeviceLeft, m_nRY - nDeviceTop) - (m_nTX - nDeviceLeft, m_nTY - nDeviceTop);
            }
#endif

            return;
        }
        //==========================================================================================
    }
}
