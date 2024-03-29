﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace SurvLine
{
    public class ClickObjectInterface : ClickCircle
    {
        //==========================================================================================
        /*[VB]
        '*******************************************************************************
        'クリックオブジェクトインターフェース

        Option Explicit
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'プロパティ
        Public Object As Object 'オブジェクト。
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'プロパティ
        public object Object;               //'オブジェクト。
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '*******************************************************************************
        'メソッド

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
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '*******************************************************************************
        'メソッド
        
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
            return false;
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
        Public Function HitTest(ByVal X As Double, ByVal Y As Double) As Boolean
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
        public bool HitTest(double X, double Y)
        {
            return false;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '*******************************************************************************
        'デバッグ

        '描画。
        Public Sub DebugDraw(ByVal objDevice As Object, ByVal nDeviceLeft As Double, ByVal nDeviceTop As Double)
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '*******************************************************************************
        'デバッグ
        
        '描画。
        */
        public void DebugDraw(object objDevice, double nDeviceLeft, double nDeviceTop)
        {
            return;
        }
        //==========================================================================================
    }
}
