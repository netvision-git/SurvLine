﻿using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static SurvLine.mdl.MdlUtility;
using static SurvLine.mdl.MdlNSUtility;
using static SurvLine.mdl.MdlNSDefine;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using TextBox = System.Windows.Forms.TextBox;

namespace SurvLine.mdl
{
    internal class MdlNSGUI
    {

        MdlUtility mdlUtility = new MdlUtility();   //K.S 0304

        //==========================================================================================
        /*[VB]
        '*******************************************************************************
        'NS用GUI関連

        Option Explicit
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '定数
        Public Const GUI_TEXT_MAX_LENGTH As Long = 40 '最大文字数。
        Public Const GUI_ANTHEIGHT_DECIMAL As Long = 3 'アンテナ高少数点以下桁数。
        Public Const GUI_XYZ_DECIMAL As Long = 5 '地心直交座標表示少数点以下桁数。
        Public Const GUI_SEC_DECIMAL As Long = 5 '秒表示少数点以下桁数。
        Public Const GUI_JGD_DECIMAL As Long = 3 '平面直角座標表示少数点以下桁数。
        Public Const GUI_FLOAT_DECIMAL As Long = 4 '汎用実数少数点以下桁数。
        Public Const GUI_DISPERSION_FORMAT As String = "0.00000E+00" '分散・共分散表示書式。
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'定数
        public const long GUI_TEXT_MAX_LENGTH = 40;                 //'最大文字数。
        public const long GUI_ANTHEIGHT_DECIMAL = 3;                //'アンテナ高少数点以下桁数。
        public const long GUI_XYZ_DECIMAL = 5;                      //'地心直交座標表示少数点以下桁数。
        public const long GUI_SEC_DECIMAL = 5;                      //'秒表示少数点以下桁数。
        public const long GUI_JGD_DECIMAL = 3;                      //'平面直角座標表示少数点以下桁数。
        public const long GUI_FLOAT_DECIMAL = 4;                    //'汎用実数少数点以下桁数。
        public const string GUI_DISPERSION_FORMAT = "0.00000E+00";  //'分散・共分散表示書式。
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'メッセージ。
        Public Const GUI_MSG_COORDRANGE As String = "の範囲が適切ではありません。"
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'メッセージ。
        public const string GUI_MSG_COORDRANGE = "の範囲が適切ではありません。";
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '入力値の緯度経度の範囲を検査する。
        '
        '引き数：
        'txtLatH 検査対象コントロール、緯度度。
        'txtLatM 検査対象コントロール、緯度分。
        'txtLatS 検査対象コントロール、緯度秒。
        'txtLonH 検査対象コントロール、経度度。
        'txtLonM 検査対象コントロール、経度分。
        'txtLonS 検査対象コントロール、経度秒。
        'sLabel 対象コントロールの名称。
        'bFocus 検査に引っかかった場合フォーカスを移すか？
        '
        '戻り値：
        '入力値が正常である場合 True を返す。
        'それ以外の場合 False を返す。
        Public Function CheckCoordRangeDMS(ByVal txtLatH As Object, ByVal txtLatM As Object, ByVal txtLatS As Object, ByVal txtLonH As Object, ByVal txtLonM As Object, ByVal txtLonS As Object, ByVal sLabel As String, Optional ByVal bFocus As Boolean = True) As Boolean

            CheckCoordRangeDMS = False
    
            Dim nLat As Double
            Dim nLon As Double
            nLat = dms_to_d(Val(txtLatH.Text), Val(txtLatM.Text), Val(txtLatS.Text))
            nLon = dms_to_d(Val(txtLonH.Text), Val(txtLonM.Text), Val(txtLonS.Text))
    
            If Not CheckCoordDMS(nLat, nLon, 0) Then
                Call MsgBox(sLabel & GUI_MSG_COORDRANGE, vbCritical)
                If bFocus Then Call txtLatH.SetFocus
                Exit Function
            End If
    
            CheckCoordRangeDMS = True
    
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#] k.s
        /// <summary>
        /// 入力値の緯度経度の範囲を検査する。
        /// '
        /// 引き数：
        /// txtLatH 検査対象コントロール、緯度度。
        /// txtLatM 検査対象コントロール、緯度分。
        /// txtLatS 検査対象コントロール、緯度秒。
        /// txtLonH 検査対象コントロール、経度度。
        /// txtLonM 検査対象コントロール、経度分。
        /// txtLonS 検査対象コントロール、経度秒。
        /// sLabel 対象コントロールの名称。
        /// bFocus 検査に引っかかった場合フォーカスを移すか？
        /// 
        /// </summary>
        /// <param name="txtLatH"></param>
        /// <param name="txtLatM"></param>
        /// <param name="txtLatS"></param>
        /// <param name="txtLonH"></param>
        /// <param name="txtLonM"></param>
        /// <param name="txtLonS"></param>
        /// <param name="sLabel"></param>
        /// <param name="bFocus"></param>
        /// <returns>
        /// 戻り値：
        /// 入力値が正常である場合 True を返す。
        /// それ以外の場合 False を返す。
        /// </returns>
        public static bool CheckCoordRangeDMS(object txtLatH, object txtLatM, object txtLatS, object txtLonH, object txtLonM, object txtLonS, string sLabel, bool bFocus = true)
        {
            bool CheckCoordRangeDMS = false;

            double nLat;
            double nLon;

            TextBox txtLatH2 = txtLatH as TextBox;
            TextBox txtLatM2 = txtLatM as TextBox;
            TextBox txtLatS2 = txtLatS as TextBox;

            nLat = dms_to_d(int.Parse(txtLatH2.Text), int.Parse(txtLatM2.Text), double.Parse(txtLatS2.Text));

            TextBox txtLonH2 = txtLonH as TextBox;
            TextBox txtLonM2 = txtLonM as TextBox;
            TextBox txtLonS2 = txtLonS as TextBox;

            nLon = dms_to_d(int.Parse(txtLonH2.Text), int.Parse(txtLonM2.Text), double.Parse(txtLonS2.Text));

            if (!CheckCoordDMS(nLat, nLon, 0))
            {
                _ = MessageBox.Show($" {sLabel} {GUI_MSG_COORDRANGE} ", "エラー発生", MessageBoxButtons.OK, MessageBoxIcon.Error);

                if (bFocus)
                {
                    _ = txtLatH2.Focus();
                    return CheckCoordRangeDMS;
                }

            }

            CheckCoordRangeDMS = true;
            return CheckCoordRangeDMS;
        }








        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '入力値の平面直角座標の範囲を検査する。
        '
        '引き数：
        'txtX 検査対象コントロール、X(ｍ)。
        'txtY 検査対象コントロール、Y(ｍ)。
        'sLabel 対象コントロールの名称。
        'bFocus 検査に引っかかった場合フォーカスを移すか？
        '
        '戻り値：
        '入力値が正常である場合 True を返す。
        'それ以外の場合 False を返す。
        Public Function CheckCoordRangeJGD(ByVal txtX As Object, ByVal txtY As Object, ByVal sLabel As String, Optional ByVal bFocus As Boolean = True) As Boolean

            CheckCoordRangeJGD = False
    
            If Not CheckCoordJGD(Val(txtX.Text), Val(txtY.Text), 0, GetDocument().CoordNum) Then
                Call MsgBox(sLabel & GUI_MSG_COORDRANGE, vbCritical)
                If bFocus Then Call txtX.SetFocus
                Exit Function
            End If
    
            CheckCoordRangeJGD = True
    
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#] k.s
        /// <summary>
        /// 
        /// </summary>
        /// <param name="txtX"></param>
        /// <param name="txtY"></param>
        /// <param name="sLabel"></param>
        /// <param name="bFocus"></param>
        /// <returns></returns>
        public static bool CheckCoordRangeJGD(object txtX, object txtY, string sLabel, bool bFocus = true)
        {
            bool CheckCoordRangeJGD = false;
#if false
            If Not CheckCoordJGD(Val(txtX.Text), Val(txtY.Text), 0, GetDocument().CoordNum) Then
                Call MsgBox(sLabel & GUI_MSG_COORDRANGE, vbCritical)
                If bFocus Then Call txtX.SetFocus
                Exit Function
            End If
#endif

            CheckCoordRangeJGD = true;
            return CheckCoordRangeJGD;

        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '入力値の地心直交座標の範囲を検査する。
        '
        '引き数：
        'txtWGSX 検査対象コントロール、X(ｍ)。
        'txtWGSY 検査対象コントロール、Y(ｍ)。
        'txtWGSZ 検査対象コントロール、Z(ｍ)。
        'sLabel 対象コントロールの名称。
        'bFocus 検査に引っかかった場合フォーカスを移すか？
        '
        '戻り値：
        '入力値が正常である場合 True を返す。
        'それ以外の場合 False を返す。
        Public Function CheckCoordRangeXYZ(ByVal txtWGSX As Object, ByVal txtWGSY As Object, ByVal txtWGSZ As Object, ByVal sLabel As String, Optional ByVal bFocus As Boolean = True) As Boolean

            CheckCoordRangeXYZ = False
    
            If Not CheckCoordXYZ(Val(txtWGSX.Text), Val(txtWGSY.Text), Val(txtWGSZ.Text)) Then
                Call MsgBox(sLabel & GUI_MSG_COORDRANGE, vbCritical)
                If bFocus Then Call txtWGSX.SetFocus
                Exit Function
            End If
    
            CheckCoordRangeXYZ = True
    
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        public static bool CheckCoordRangeXYZ(Object txtWGSX, Object txtWGSY, Object txtWGSZ, string sLabel, bool bFocus = true)
        {

            bool CheckCoordRangeXYZ = false;

            TextBox txtWGSX2 = txtWGSX as TextBox;
            TextBox txtWGSY2 = txtWGSY as TextBox;
            TextBox txtWGSZ2 = txtWGSZ as TextBox;


            if (!CheckCoordXYZ(double.Parse(txtWGSX2.Text), double.Parse(txtWGSY2.Text), double.Parse(txtWGSZ2.Text)))
            {
                //  Call MsgBox(sLabel &GUI_MSG_COORDRANGE, vbCritical)
                _ = MessageBox.Show(sLabel + GUI_MSG_COORDRANGE, "エラー発生", MessageBoxButtons.OK, MessageBoxIcon.Error);

                if (bFocus)
                {
                    //Then Call txtWGSX.SetFocus
                    return CheckCoordRangeXYZ;
                }
            }

            CheckCoordRangeXYZ = true;
            return CheckCoordRangeXYZ;


        }



        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '0Trim実数四捨五入書式化。
        '
        '引き数：
        'nValue 実数。
        'nDecimal 四捨五入桁。
        'sFormat 書式。
        '
        '戻り値：書式化された文字列。
        Public Function FormatRound0Trim(ByVal nValue As Double, ByVal nDecimal As Long, Optional ByVal sFormat As String = "0") As String
            FormatRound0Trim = FormatRound(nValue, nDecimal, sFormat)
            FormatRound0Trim = RTrimEx(FormatRound0Trim, "0")
            FormatRound0Trim = RTrimEx(FormatRound0Trim, ".")
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '0Trim実数四捨五入書式化。
        '
        '引き数：
        'nValue 実数。
        'nDecimal 四捨五入桁。
        'sFormat 書式。
        '
        '戻り値：書式化された文字列。
        */
        public static string FormatRound0Trim(double nValue, long nDecimal, string sFormat = "0")
        {
            string w_FormatRound0Trim = "";
            w_FormatRound0Trim = FormatRound(nValue, nDecimal, sFormat);
            w_FormatRound0Trim = RTrimEx(w_FormatRound0Trim, "0");
            w_FormatRound0Trim = RTrimEx(w_FormatRound0Trim, ".");
            return w_FormatRound0Trim;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '実数四捨五入書式化。
        '
        'Format だとシステムによって四捨五入でないバージョンもある。
        '
        '引き数：
        'nValue 実数。
        'nDecimal 四捨五入桁。
        'sFormat 整数部書式。省略した場合0詰め無し。
        '
        '戻り値：書式化された文字列。
        Public Function FormatRound(ByVal nValue As Double, ByVal nDecimal As Long, Optional ByVal sFormat As String = "0", Optional ByVal sFormat2 As String = "", Optional ByVal sFormat3 As String = "", Optional ByVal sFormat4 As String = "") As String
            Dim sDecimal As String
            If nDecimal > 0 Then
                sDecimal = Left$(".00000000000000000000", nDecimal + 1) '64ビットUnsignedまで対応。
                sFormat = sFormat & sDecimal
            Else
                sDecimal = ""
            End If
            If sFormat2 <> "" Then
                sFormat = sFormat & ";" & sFormat2 & sDecimal
                If sFormat3 <> "" Then
                    sFormat = sFormat & ";" & sFormat3 & sDecimal
                    If sFormat4 <> "" Then
                        sFormat = sFormat & ";" & sFormat4 & sDecimal
                    End If
                End If
            End If
            FormatRound = Format$(JpnRound(nValue, nDecimal), sFormat)
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '実数四捨五入書式化。
        '
        'Format だとシステムによって四捨五入でないバージョンもある。
        '
        '引き数：
        'nValue 実数。
        'nDecimal 四捨五入桁。
        'sFormat 整数部書式。省略した場合0詰め無し。
        '
        '戻り値：書式化された文字列。
        */
        public static string FormatRound(double nValue, long nDecimal, string sFormat = "0", string sFormat2 = "", string sFormat3 = "", string sFormat4 = "")
        {
            string sDecimal;
            if (nDecimal > 0)
            {
                sDecimal = Left(".00000000000000000000", (int)nDecimal + 1);     //'64ビットUnsignedまで対応。
                sFormat = sFormat + sDecimal;
            }
            else
            {
                sDecimal = "";
            }
            if (sFormat2 != "")
            {
                sFormat = sFormat + ";" + sFormat2 + sDecimal;
                if (sFormat3 != "")
                {
                    sFormat = sFormat + ";" + sFormat3 + sDecimal;
                    if (sFormat4 != "")
                    {
                        sFormat = sFormat + ";" + sFormat4 + sDecimal;
                    }
                }
            }
            return Strings.Format(JpnRound(nValue, nDecimal), sFormat);
        }
        //==========================================================================================


        public static TextBox ObjectToText(object objText)
        {
            TextBox txt = objText as TextBox;
            return txt;

        }
        public static bool mdl_CheckInputEmpty(object txtTextBox, string sLabel, bool bFocus)
        {
            TextBox txtBox = txtTextBox as TextBox;

            bool mdl_CheckInputEmpty = false;


            //'空であるか？
            if (txtBox.Text.Length <= 0)
            {
                _ = MessageBox.Show($" {sLabel} {txtBox.Text}", "エラー発生", MessageBoxButtons.OK, MessageBoxIcon.Error); //2
                if (bFocus)
                {
                    _ = txtBox.Focus(); //2
                }
                return mdl_CheckInputEmpty;
            }


            mdl_CheckInputEmpty = true;
            return mdl_CheckInputEmpty;

        }
 

    }
}
