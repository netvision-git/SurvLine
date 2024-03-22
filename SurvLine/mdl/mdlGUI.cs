using Microsoft.VisualBasic.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static SurvLine.mdl.DEFINE;
using static SurvLine.mdl.MdiVBfunctions;
using static SurvLine.mdl.MdlNSGUI;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Runtime.CompilerServices;
using TextBox = System.Windows.Forms.TextBox;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace SurvLine.mdl
{
    internal class MdlGUI
    {
        //==========================================================================================
        /*[VB]
        '*******************************************************************************
        'GUI関連

        Option Explicit
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '定数
        Public Const GUI_MANUAL_LISTINDEX As String = "GUI_MANUAL_LISTINDEX" 'リストインデックスのマニュアル設定。

        'メッセージ。
        Public Const GUI_MSG_EMPTY As String = "を入力してください｡"
        Public Const GUI_MSG_DISHONESTY As String = "の入力値が不正です。"
        Public Const GUI_MSG_NOSELECT As String = "を選択してください｡"
        Public Const GUI_MSG_LENGTH As String = "ﾊﾞｲﾄ以上は入力できません｡"
        Public Const GUI_MSG_OVERFLOW As String = "の入力値がオーバーフローしました。"
        Public Const GUI_MSG_INTEGER As String = "には整数を入力してください。"
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'定数
        public const string GUI_MANUAL_LISTINDEX = "GUI_MANUAL_LISTINDEX";      //'リストインデックスのマニュアル設定。

        //'メッセージ。
        public const string GUI_MSG_EMPTY = "を入力してください｡";
        public const string GUI_MSG_DISHONESTY = "の入力値が不正です。";
        public const string GUI_MSG_NOSELECT = "を選択してください｡";
        public const string GUI_MSG_LENGTH = "ﾊﾞｲﾄ以上は入力できません｡";
        public const string GUI_MSG_OVERFLOW = "の入力値がオーバーフローしました。";
        public const string GUI_MSG_INTEGER = "には整数を入力してください。";
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'テキストボックスのテキストすべてを選択状態にする。
        '
        '引き数：
        'txtTextBox 対象コントロール。
        Public Sub SelectText(ByVal txtTextBox As Object)
            txtTextBox.SelStart = 0
            txtTextBox.SelLength = Len(txtTextBox.Text)
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        public static void SelectText(object txtTextBox)    //2
        {
            //不要
        }

        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'リストインデックスのマニュアル設定。
        '
        '引き数：
        'objList 対象オブジェクト。
        'nListIndex ListIndexの値。
        Public Sub SetListIndexManual(ByVal objList As Object, ByVal nListIndex As Long)

            'クリックイベントで Tag プロパティを参照し、GUI_MANUAL_LISTINDEX であるか評価する。
            Dim sTag As String
            sTag = objList.Tag
            objList.Tag = GUI_MANUAL_LISTINDEX
            objList.ListIndex = nListIndex
            objList.Tag = sTag
    
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '文字列が整数であるか検査する。
        '
        '引き数：
        'sValue 文字列。
        '
        '戻り値：
        '文字列が整数である場合 True を返す。
        'それ以外の場合 False を返す。
        Public Function CheckInteger(ByVal sValue As String) As Boolean

            CheckInteger = False
    
            Dim nCode() As Byte
            Dim i As Long
    
            'ASCIIコード。
            nCode = StrConv(sValue, vbFromUnicode)
    
            '１文字ずつ検査。
            For i = 0 To UBound(nCode)
                '0～9以外？
                If nCode(i) < &H30& Or &H39& < nCode(i) Then
                    'マイナス？
                    If nCode(i) = &H2D& Then
                        '最初の文字？
                        If i <> 0 Then Exit Function
                    Else
                        Exit Function
                    End If
                End If
            Next
    
            CheckInteger = True
    
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]  2
        /// <summary>
        /// 文字列が整数であるか検査する。
        /// '
        /// 引き数：
        /// sValue 文字列。
        /// 
        /// </summary>
        /// <param name="sValue"></param>
        /// <returns>
        /// 戻り値：
        /// 文字列が整数である場合 True を返す。
        /// それ以外の場合 False を返す。
        /// </returns>
        public static bool CheckInteger(string sValue)
        {
            bool CheckInteger = false;

            if (!long.TryParse(sValue, out _))
            {
                return CheckInteger;
            }

            CheckInteger = true;
            return CheckInteger;

        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '文字列が実数であるか検査する。
        '
        '引き数：
        'sValue 文字列。
        '
        '戻り値：
        '文字列が実数である場合 True を返す。
        'それ以外の場合 False を返す。
        Public Function CheckFloat(ByVal sValue As String) As Boolean

            CheckFloat = False
    
            Dim nCode() As Byte
            Dim bDot As Boolean
            Dim i As Long
    
            'ASCIIコード。
            nCode = StrConv(sValue, vbFromUnicode)
            bDot = False
    
            '１文字ずつ検査。
            For i = 0 To UBound(nCode)
                '0～9以外？
                If nCode(i) < &H30& Or &H39& < nCode(i) Then
                    'マイナス？
                    If nCode(i) = &H2D& Then
                        '最初の文字？
                        If i <> 0 Then Exit Function
                    'ドット？
                    ElseIf nCode(i) = &H2E& Then
                        '２個目のドットは許可しない。
                        If bDot Then Exit Function
                        bDot = True
                    Else
                        Exit Function
                    End If
                End If
            Next
    
            CheckFloat = True
    
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#] k.s  //2
        /// <summary>
        /// 文字列が実数であるか検査する。
        /// 
        ///引き数：
        ///sValue 文字列。
        /// 
        /// </summary>
        /// <param name="sValue"></param>
        /// <returns>
        /// 戻り値：
        /// 文字列が実数である場合 True を返す。
        /// それ以外の場合 False を返す。
        /// </returns>
        public static bool CheckFloat(string sValue)
        {
            bool CheckFloat = false;

            double ret;
            if (!double.TryParse(sValue, out ret))
            {
                return CheckFloat;
            }

            CheckFloat = true;
            return CheckFloat;

        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '文字列に無効な文字が含まれているか検査する。
        '
        '引き数：
        'sValue 文字列。
        '
        '戻り値：
        '文字列に無効な文字が含まれていない場合 True を返す。
        'それ以外の場合 False を返す。
        Public Function CheckString(ByVal sValue As String) As Boolean

            CheckString = False
    
            Dim i As Long
            For i = 1 To Len(sValue)
                Select Case Asc(Mid(sValue, i))
                Case &H22& 'ダブルクォーテーション。
                    Exit Function
                Case &H2C& 'カンマ。
                    Exit Function
                End Select
            Next
    
            CheckString = True
    
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]  2
        /// <summary>
        /// 文字列に無効な文字が含まれているか検査する。
        /// '
        /// 引き数：
        /// sValue 文字列。
        /// 
        /// </summary>
        /// <param name="sValue"></param>
        /// <returns>
        /// 戻り値：
        /// 文字列に無効な文字が含まれていない場合 True を返す。
        /// それ以外の場合 False を返す。
        /// </returns>
        public static bool CheckString(string sValue)   //
        {

            bool CheckString = false;

            for (int i = 0; i < sValue.Length; i++)
            {
                string sWork = sValue.Substring(i, 1);
                byte[] data;
                data = System.Text.Encoding.GetEncoding("ascii").GetBytes(sWork);

                long nChar = (long)data[0];

                switch (nChar)
                {
                    case 0x22:      //ダブルクォーテーション。
                        return CheckString;
                    case 0x2C:     //'カンマ。
                        return CheckString;
                }
            }
            CheckString = true;
            return CheckString;

        }

        //==========================================================================================


        //==========================================================================================
        /*[VB]
        '文字数を検査する。
        '
        'バイト数を評価する。
        'nMin以上、nMax以下で許可。
        '
        '引き数：
        'sValue 文字列。
        'nMax 最大文字数。
        'nMin 最小文字数。
        '
        '戻り値：
        '文字数が範囲内の場合 True を返す。
        'それ以外の場合 False を返す。
        Public Function CheckLength(ByVal sValue As String, ByVal nMax As Long, Optional ByVal nMin As Long = 0) As Boolean

            CheckLength = False
    
            Dim nCode() As Byte
    
            'ASCIIコード。
            nCode = StrConv(sValue, vbFromUnicode)
    
            If UBound(nCode) + 1 > nMax Then Exit Function
            If UBound(nCode) + 1 < nMin Then Exit Function
    
            CheckLength = True
    
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]  //2
        /// <summary>
        /// 文字数を検査する。
        /// '
        /// バイト数を評価する。
        /// nMin以上、nMax以下で許可。
        /// '
        /// 引き数：
        /// sValue 文字列。
        /// nMax 最大文字数。
        /// nMin 最小文字数。
        /// 
        /// </summary>
        /// <param name="sValue"></param>
        /// <param name="nMax"></param>
        /// <param name="nMin"></param>
        /// <returns>
        /// 戻り値：
        /// 文字数が範囲内の場合 True を返す。
        /// それ以外の場合 False を返す。
        /// </returns>
        public static bool CheckLength(string sValue, long nMax, long nMin = 0)
        {
            bool CheckLength = false;

            byte[] nCode;

            //'ASCIIコード。
            nCode = StrConv(sValue, vbFromUnicode);

            if (nCode.Length > nMax)
            {
                return CheckLength;
            }
            if (nCode.Length < nMin)
            {
                return CheckLength;
            }

            CheckLength = true;
            return CheckLength;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '範囲違反のメッセージを作成する。
        '
        'nMin以上、nMax未満を許可。
        '
        '引き数：
        'sMin 最小値。
        'sMax 最大値。
        'sLabel 対象コントロールの名称。
        '
        '戻り値：範囲違反のメッセージ。
        Public Function MakeRangeMessage(ByVal sMin As String, ByVal sMax As String, ByVal sLabel As String) As String
            MakeRangeMessage = sLabel & "の入力値が範囲外です。" & vbCrLf & sMin & "以上" & sMax & "未満の値を入力してください。"
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]k.s
        public static string MakeRangeMessage(string sMin, string sMax, string sLabel)
        {
            string MakeRangeMessage = sLabel + "の入力値が範囲外です。" + vbCrLf + sMin + "以上" + sMax + "未満の値を入力してください。";

            return MakeRangeMessage;
        }

        //==========================================================================================

        //==========================================================================================
        /*[VB]
            '入力値が空であるか検査する。
            '
            '引き数：
            'txtTextBox 検査対象コントロール。
            'sLabel 対象コントロールの名称。
            'bFocus 検査に引っかかった場合フォーカスを移すか？
            '
            '戻り値：
            '入力値が正常である場合 True を返す。
            'それ以外の場合 False を返す。
            Public Function CheckInputEmpty(ByVal txtTextBox As Object, ByVal sLabel As String, Optional ByVal bFocus As Boolean = True) As Boolean

                CheckInputEmpty = False
    
                '空であるか？
                If Len(txtTextBox.Text) <= 0 Then
                    Call MsgBox(sLabel & GUI_MSG_EMPTY, vbCritical)
                    If bFocus Then Call txtTextBox.SetFocus
                    Exit Function
                End If
    
                CheckInputEmpty = True
    
            End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#] k.s
        /// <summary>
        /// 入力値が空であるか検査する。
        ///
        /// 引き数：
        /// txtTextBox 検査対象コントロール。
        /// sLabel 対象コントロールの名称。
        /// bFocus 検査に引っかかった場合フォーカスを移すか？
        ///
        /// </summary>
        /// <param name="txtTextBox"></param>
        /// <param name="sLabel"></param>
        /// <param name="bFocus"></param>
        /// <returns></returns>
        public static bool CheckInputEmpty(object txtTextBox, string sLabel, bool bFocus)
        {

            bool CheckInputEmpty = false;

#if false
            TextBox txtBox = (TextBox)txtTextBox;

            TextBox txtBox = new TextBox();
            txtTextBox.
            txtBox = txtTextBox as TextBox;
#endif
            string txtBox = txtTextBox.ToString().Trim();

            //'空であるか？
            //  if (txtBox.Text.Length <= 0)
            if (txtBox.Length <= 0)
            {
                _ = MessageBox.Show($" {sLabel} {GUI_MSG_EMPTY}", "エラー発生", MessageBoxButtons.OK, MessageBoxIcon.Error); //2
                if (bFocus)
                {
                    //  _ = txtBox.Focus(); //2
                }
                return CheckInputEmpty;
            }

            CheckInputEmpty = true;
            return CheckInputEmpty;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '入力値の文字数を検査する。
        '
        'バイト数を評価する。
        'nMax以下を許可。
        '
        '引き数：
        'txtTextBox 検査対象コントロール。
        'sLabel 対象コントロールの名称。
        'nMax 最大文字数。
        'bFocus 検査に引っかかった場合フォーカスを移すか？
        '
        '戻り値：
        '入力値が正常である場合 True を返す。
        'それ以外の場合 False を返す。
        Public Function CheckInputLength(ByVal txtTextBox As Object, ByVal sLabel As String, ByVal nMax As Long, Optional ByVal bFocus As Boolean = True) As Boolean

            CheckInputLength = False
    
            '文字数検査。
            If Not CheckLength(txtTextBox.Text, nMax) Then
                Call MsgBox(sLabel & "に" & CStr(nMax + 1) & GUI_MSG_LENGTH, vbCritical)
                If bFocus Then Call txtTextBox.SetFocus
                Exit Function
            End If
    
            CheckInputLength = True
    
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]  //2
        /// <summary>
        /// 入力値の文字数を検査する。
        /// '
        /// バイト数を評価する。
        /// nMax以下を許可。
        /// '
        /// 引き数：
        /// txtTextBox 検査対象コントロール。
        /// sLabel 対象コントロールの名称。
        /// nMax 最大文字数。
        /// bFocus 検査に引っかかった場合フォーカスを移すか？
        /// 
        /// </summary>
        /// <param name="txtTextBox"></param>
        /// <param name="sLabel"></param>
        /// <param name="nMax"></param>
        /// <param name="bFocus"></param>
        /// <returns>
        /// 戻り値：
        /// 入力値が正常である場合 True を返す。
        /// それ以外の場合 False を返す。
        /// </returns>
        public static bool CheckInputLength(object txtTextBox, string sLabel, long nMax, bool bFocus = true)
        {
            bool CheckInputLength = false;

            _ = new System.Windows.Forms.TextBox();
            System.Windows.Forms.TextBox txtTextBox2 = (System.Windows.Forms.TextBox)txtTextBox;

            //'文字数検査。
            if (CheckLength(txtTextBox2.Text, nMax))
            {
                _ = MessageBox.Show($" {sLabel} に {nMax + 1} {GUI_MSG_LENGTH}", "エラー発生", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (bFocus)
                {
                    _ = txtTextBox2.Focus();
                    return CheckInputLength;
                }
            }

            CheckInputLength = true;
            return CheckInputLength;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '整数入力値を検査する。
        '
        '引き数：
        'txtTextBox 検査対象コントロール。
        'sLabel 対象コントロールの名称。
        'bFocus 検査に引っかかった場合フォーカスを移すか？
        '
        '戻り値：
        '入力値が正常である場合 True を返す。
        'それ以外の場合 False を返す。
        Public Function CheckIntegerInput(ByVal txtTextBox As Object, ByVal sLabel As String, Optional ByVal bFocus As Boolean = True) As Boolean

            On Error GoTo ErrorHandler:
            CheckIntegerInput = False
    
            '空であるか？
            If Not CheckInputEmpty(txtTextBox, sLabel, bFocus) Then Exit Function
    
            '数字であるか？
            If Not CheckInteger(txtTextBox.Text) Then
                Call MsgBox(sLabel & GUI_MSG_INTEGER, vbCritical)
                If bFocus Then Call txtTextBox.SetFocus
                Exit Function
            End If
    
            Dim nTest As Long
            nTest = Val(txtTextBox.Text)
    
            CheckIntegerInput = True
            Exit Function
    
        ErrorHandler:
            If Err.Number = 6 Then
                Call MsgBox(sLabel & GUI_MSG_OVERFLOW, vbCritical)
                If bFocus Then Call txtTextBox.SetFocus
            Else
                Call Err.Raise(Err.Number, Err.Source, Err.Description, Err.HelpFile, Err.HelpContext)
            End If
    
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#] k.s
        /// <summary>
        /// 整数入力値を検査する。
        /// '
        /// 引き数：
        /// txtTextBox 検査対象コントロール。
        /// sLabel 対象コントロールの名称。
        /// bFocus 検査に引っかかった場合フォーカスを移すか？
        /// 
        /// </summary>
        /// <param name="txtTextBox"></param>
        /// <param name="sLabel"></param>
        /// <param name="bFocus"></param>
        /// <returns>
        /// 戻り値：
        /// 入力値が正常である場合 True を返す。
        /// それ以外の場合 False を返す。
        /// </returns>
        public static bool CheckIntegerInput(object txtTextBox, string sLabel, bool bFocus = true)
        {
            bool CheckIntegerInput = false;
            _ = new System.Windows.Forms.TextBox();
            System.Windows.Forms.TextBox txtTextBox2 = (System.Windows.Forms.TextBox)txtTextBox;

            try
            {
                //'空であるか？
                if (!CheckInputEmpty(txtTextBox2, sLabel, bFocus))
                {
                    return CheckIntegerInput;
                }


                //'数字であるか？
                if (!CheckInteger(txtTextBox2.Text))
                {
                    _ = MessageBox.Show($" {sLabel} {GUI_MSG_INTEGER}", "エラー発生", MessageBoxButtons.OK, MessageBoxIcon.Error); //2

                    if (bFocus)
                    {
                        _ = txtTextBox2.Focus();
                        return CheckIntegerInput;
                    }
                }

                long nTest;
                nTest = long.Parse(txtTextBox2.Text);

                CheckIntegerInput = true;

            }
            catch (Exception ex)
            {
                //If Err.Number = 6 Then
                //    Call MsgBox(sLabel & GUI_MSG_OVERFLOW, vbCritical)
                //    If bFocus Then Call txtTextBox.SetFocus
                //Else
                //    Call Err.Raise(Err.Number, Err.Source, Err.Description, Err.HelpFile, Err.HelpContext)
                //End If
                _ = MessageBox.Show(ex.Message, "エラー発生", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return CheckIntegerInput;

        }



        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '範囲付きで整数入力値を検査する。
        '
        'nMin以上、nMax未満を許可。
        '
        '引き数：
        'txtTextBox 検査対象コントロール。
        'sLabel 対象コントロールの名称。
        'nMin 最小値。
        'nMax 最大値。
        'bFocus 検査に引っかかった場合フォーカスを移すか？
        '
        '戻り値：
        '入力値が正常である場合 True を返す。
        'それ以外の場合 False を返す。
        Public Function CheckIntegerInputRange(ByVal txtTextBox As Object, ByVal sLabel As String, ByVal nMin As Long, ByVal nMax As Long, Optional ByVal bFocus As Boolean = True) As Boolean

            CheckIntegerInputRange = False
    
            '整数入力であるか？
            If Not CheckIntegerInput(txtTextBox, sLabel, bFocus) Then Exit Function
    
            '範囲内か？
            Dim nValue As Long
            nValue = Val(txtTextBox.Text)
            If nValue < nMin Or nMax <= nValue Then
                Call MsgBox(MakeRangeMessage(CStr(nMin), CStr(nMax), sLabel), vbCritical)
                If bFocus Then Call txtTextBox.SetFocus
                Exit Function
            End If
    
            CheckIntegerInputRange = True
    
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#] k.s
        /// <summary>
        /// 範囲付きで整数入力値を検査する。
        /// 
        /// nMin以上、nMax未満を許可。
        /// 
        /// 引き数：
        /// txtTextBox 検査対象コントロール。
        /// sLabel 対象コントロールの名称。
        /// nMin 最小値。
        /// nMax 最大値。
        /// bFocus 検査に引っかかった場合フォーカスを移すか？
        /// 
        /// </summary>
        /// <param name="txtTextBox"></param>
        /// <param name="sLabel"></param>
        /// <param name="nMin"></param>
        /// <param name="nMax"></param>
        /// <param name="bFocus"></param>
        /// <returns>
        /// 戻り値：
        /// 入力値が正常である場合 True を返す。
        /// それ以外の場合 False を返す。
        /// </returns>
        public static bool CheckIntegerInputRange(object txtTextBox, string sLabel, long nMin, long nMax, bool bFocus = true)
        {
            bool CheckIntegerInputRange = false;


            //'整数入力であるか？
            if (!CheckIntegerInput(txtTextBox, sLabel, bFocus))
            {
                return CheckIntegerInputRange;
            }

            //'範囲内か？
            _ = new System.Windows.Forms.TextBox();
            System.Windows.Forms.TextBox txtTextBox2 = (System.Windows.Forms.TextBox)txtTextBox;

            long nValue;
            nValue = long.Parse(txtTextBox2.Text);
            if (nValue < nMin || nMax <= nValue)
            {
                _ = MessageBox.Show(MakeRangeMessage(nMin.ToString(), nMax.ToString(), sLabel), "エラー発生", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (bFocus)
                {
                    _ = txtTextBox2.Focus();
                    return CheckIntegerInputRange;
                }
            }

            CheckIntegerInputRange = true;
            return CheckIntegerInputRange;

        }

        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '実数入力値を検査する。
        '
        '引き数：
        'txtTextBox 検査対象コントロール。
        'sLabel 対象コントロールの名称。
        'bFocus 検査に引っかかった場合フォーカスを移すか？
        '
        '戻り値：
        '入力値が正常である場合 True を返す。
        'それ以外の場合 False を返す。
        Public Function CheckFloatInput(ByVal txtTextBox As Object, ByVal sLabel As String, Optional ByVal bFocus As Boolean = True) As Boolean

            On Error GoTo ErrorHandler:
            CheckFloatInput = False
    
            '空であるか？
            If Not CheckInputEmpty(txtTextBox, sLabel, bFocus) Then Exit Function
    
            '数字であるか？
            If Not CheckFloat(txtTextBox.Text) Then
                Call MsgBox(sLabel & GUI_MSG_DISHONESTY, vbCritical)
                If bFocus Then Call txtTextBox.SetFocus
                Exit Function
            End If
    
            Dim nTest As Single
            nTest = Val(txtTextBox.Text)
    
            CheckFloatInput = True
            Exit Function
    
        ErrorHandler:
            If Err.Number = 6 Then
                Call MsgBox(sLabel & GUI_MSG_OVERFLOW, vbCritical)
                If bFocus Then Call txtTextBox.SetFocus
            Else
                Call Err.Raise(Err.Number, Err.Source, Err.Description, Err.HelpFile, Err.HelpContext)
            End If
    
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#] k.s
        /// <summary>
        /// 実数入力値を検査する。
        /// '
        /// 引き数：
        /// txtTextBox 検査対象コントロール。
        /// sLabel 対象コントロールの名称。
        /// bFocus 検査に引っかかった場合フォーカスを移すか？
        /// 
        /// </summary>
        /// <param name="txtTextBox"></param>
        /// <param name="sLabel"></param>
        /// <param name="bFocus"></param>
        /// <returns>
        /// 戻り値：
        /// 入力値が正常である場合 True を返す。
        /// それ以外の場合 False を返す。
        /// </returns>
        public static bool CheckFloatInput(object txtTextBox, string sLabel, bool bFocus = true)
        {
            bool CheckFloatInput = false;

            System.Windows.Forms.TextBox txtBox = new System.Windows.Forms.TextBox();
            txtBox = (System.Windows.Forms.TextBox)txtTextBox;

            try
            {
                //'空であるか？
                if (!CheckInputEmpty(txtBox, sLabel, bFocus))
                {
                    return CheckFloatInput;
                }

                //'数字であるか？
                if (!CheckFloat(txtBox.Text))
                {
                    _ = MessageBox.Show($" {sLabel} {GUI_MSG_DISHONESTY}", "エラー発生", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    if (bFocus)
                    {
                        _ = txtBox.Focus();
                        return CheckFloatInput;
                    }
                }

                float nTest;
                nTest = float.Parse(txtBox.Text);

                CheckFloatInput = true;

            }
            catch (Exception ex)
            {
                //ErrorHandler:
                //    If Err.Number = 6 Then
                //        Call MsgBox(sLabel & GUI_MSG_OVERFLOW, vbCritical)
                //    If bFocus Then Call txtTextBox.SetFocus
                //Else
                //    Call Err.Raise(Err.Number, Err.Source, Err.Description, Err.HelpFile, Err.HelpContext)
                //End If
                _ = MessageBox.Show(ex.Message, "エラー発生", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

            return CheckFloatInput;

        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '範囲付きで実数入力値を検査する。
        '
        'nMin以上、nMax未満を許可。
        '
        '引き数：
        'txtTextBox 検査対象コントロール。
        'sLabel 対象コントロールの名称。
        'nMin 最小値。
        'nMax 最大値。
        'bFocus 検査に引っかかった場合フォーカスを移すか？
        '
        '戻り値：
        '入力値が正常である場合 True を返す。
        'それ以外の場合 False を返す。
        Public Function CheckFloatInputRange(ByVal txtTextBox As Object, ByVal sLabel As String, ByVal nMin As Double, ByVal nMax As Double, Optional ByVal bFocus As Boolean = True) As Boolean

            CheckFloatInputRange = False
    
            '実数入力であるか？
            If Not CheckFloatInput(txtTextBox, sLabel, bFocus) Then Exit Function
    
            '範囲内か？
            Dim nValue As Double
            nValue = Val(txtTextBox.Text)
            If nValue < nMin Or nMax <= nValue Then
                Call MsgBox(MakeRangeMessage(CStr(nMin), CStr(nMax), sLabel), vbCritical)
                If bFocus Then Call txtTextBox.SetFocus
                Exit Function
            End If
    
            CheckFloatInputRange = True
    
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]  K.S
        /// <summary>
        /// 範囲付きで実数入力値を検査する。
        /// 
        /// nMin以上、nMax未満を許可。
        /// 
        /// 引き数：
        /// txtTextBox 検査対象コントロール。
        /// sLabel 対象コントロールの名称。
        /// nMin 最小値。
        /// nMax 最大値。
        /// bFocus 検査に引っかかった場合フォーカスを移すか？
        /// 
        /// </summary>
        /// <param name="txtTextBox"></param>
        /// <param name="sLabel"></param>
        /// <param name="nMin"></param>
        /// <param name="nMax"></param>
        /// <param name="bFocus"></param>
        /// <returns>
        /// 戻り値：
        /// 入力値が正常である場合 True を返す。
        /// それ以外の場合 False を返す。
        /// </returns>
        public static bool CheckFloatInputRange(object txtTextBox, string sLabel, double nMin, double nMax, bool bFocus = true)
        {
            return CheckFloatInputRange(txtTextBox, sLabel, nMin, nMax);
        }
        public static bool CheckFloatInputRange(object txtTextBox, string sLabel, double nMin, double nMax)
        {
            bool CheckFloatInputRange = false;
            bool bFocus = true;
            _ = new System.Windows.Forms.TextBox();
            System.Windows.Forms.TextBox txtBox = (System.Windows.Forms.TextBox)txtTextBox;


            //'実数入力であるか？
            if (!CheckFloatInput(txtBox, sLabel, bFocus))   //2
            {
                return CheckFloatInputRange;
            }

            //'範囲内か？
            double nValue;
            nValue = double.Parse(txtBox.Text);

            if (nValue < nMin || nMax <= nValue)
            {
                _ = MessageBox.Show(MakeRangeMessage(nMin.ToString(), nMax.ToString(), sLabel), "エラー発生", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (bFocus)
                {
                    _ = txtBox.Focus();
                    return CheckFloatInputRange;
                }
            }

            CheckFloatInputRange = true;
            return CheckFloatInputRange;
        }



        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '文字列入力値が無効であるか検査する。
        '
        '引き数：
        'txtTextBox 検査対象コントロール。
        'sLabel 対象コントロールの名称。
        'bFocus 検査に引っかかった場合フォーカスを移すか？
        '
        '戻り値：
        '入力値が正常である場合 True を返す。
        'それ以外の場合 False を返す。
        Public Function CheckStringInputInvalid(ByVal txtTextBox As Object, ByVal sLabel As String, Optional ByVal bFocus As Boolean = True) As Boolean

            CheckStringInputInvalid = False
    
            '無効な文字は含まれているか？
            If Not CheckString(txtTextBox.Text) Then
                Call MsgBox(sLabel & GUI_MSG_DISHONESTY, vbCritical)
                If bFocus Then Call txtTextBox.SetFocus
                Exit Function
            End If
    
            CheckStringInputInvalid = True
    
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /// <summary>
        /// 文字列入力値が無効であるか検査する。
        ///
        /// 引き数：
        ///     txtTextBox 検査対象コントロール。
        ///     sLabel 対象コントロールの名称。
        ///     bFocus 検査に引っかかった場合フォーカスを移すか？
        /// </summary>
        /// <param name="txtTextBox"></param>
        /// <param name="sLabe"></param>
        /// <param name="bFocus"></param>
        /// <returns>
        /// 戻り値：
        ///     入力値が正常である場合 True を返す。
        ///     それ以外の場合 False を返す。
        /// </returns>
        public static bool CheckStringInputInvalid(object txtTextBox, string sLabe, bool bFocus)
        {
            bool CheckStringInputInvalid = false;


            //無効な文字は含まれているか？
            if (!CheckString(txtTextBox.ToString()))
            {
                _ =MessageBox.Show($" {sLabe} {GUI_MSG_EMPTY}", "エラー発生", MessageBoxButtons.OK, MessageBoxIcon.Error);

                if (bFocus)
                {
                    //  _ = txtBox.Focus();     //2
                    return CheckStringInputInvalid;
                }
                return CheckStringInputInvalid;

            }

            CheckStringInputInvalid = true;
            return CheckStringInputInvalid;
        }
        //==========================================================================================


        //==========================================================================================
        /*[VB]
                '文字列にファイル名として無効な文字が含まれているか検査する。
                '
                '引き数：
                'sValue 文字列。
                '
                '戻り値：
                '文字列にファイル名として無効な文字が含まれていない場合 True を返す。
                'それ以外の場合 False を返す。
                Public Function CheckFileName(ByVal sValue As String) As Boolean
                
                    CheckFileName = False
                
                    Dim i As Long
                    For i = 1 To Len(sValue)
                        Dim nChar As Long
                        nChar = Asc(Mid(sValue, i))
                        If &H0& <= nChar And nChar< &H20& Then Exit Function '制御コード。
                        Select Case nChar
                        Case &H22& '”
                            Exit Function
                        Case &H2A& '＊
                            Exit Function
                        Case &H2C& '，
                            Exit Function
                        Case &H2F& '／
                            Exit Function
                        Case &H3A& '：
                            Exit Function
                        Case &H3C& '＜
                            Exit Function
                        Case &H3E& '＞
                            Exit Function
                        Case &H3F& '？
                            Exit Function
                        Case &H5C& '￥
                            Exit Function
                        Case &H7C& '｜
                            Exit Function
                        Case &H7F& 'DEL
                            Exit Function
                        End Select
                    Next
                
                
                    CheckFileName = True
                
                
                End Function
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]  //2
        /// <summary>
        /// '文字列にファイル名として無効な文字が含まれているか検査する。
        /// '
        /// 引き数：
        ///     sValue 文字列。
        /// </summary>
        /// <param name="sValue"></param>
        /// <returns>
        /// 戻り値：
        ///     文字列にファイル名として無効な文字が含まれていない場合 True を返す。
        ///     それ以外の場合 False を返す。
        /// </returns>
        public static bool CheckFileName(string sValue)
        {

            bool CheckFileName = false;

            if (sValue == null)
            {
                return CheckFileName;
            }

            long nChar;
            for (int i = 0; i < sValue.Length; i++)
            {
                string sWork = sValue.Substring(i, 1);
                // nChar = long.Parse(sWork);
                byte[] data;
                data = System.Text.Encoding.GetEncoding("ascii").GetBytes(sWork);

                nChar = (long)data[0];
                if (0x00 <= nChar && nChar < 0x20) { return CheckFileName; }    //'制御コード。

            }

            if (sValue.Contains("\"") == true) { return CheckFileName; }    //case 0x22:      //”
            if (sValue.Contains("*") == true) { return CheckFileName; }     //case 0x2A:      //'＊
            if (sValue.Contains(",") == true) { return CheckFileName; }     //case 0x2C:      //'，
            if (sValue.Contains("/") == true) { return CheckFileName; }     //case 0x2F:      //'／
            if (sValue.Contains(":") == true) { return CheckFileName; }     //case 0x3A:      //'：
            if (sValue.Contains("<") == true) { return CheckFileName; }     //case 0x3C:      //'＜
            if (sValue.Contains(">") == true) { return CheckFileName; }     //case 0x3E:      //'＞
            if (sValue.Contains("?") == true) { return CheckFileName; }     //case 0x3F:      //'？
            if (sValue.Contains("\\") == true) { return CheckFileName; }     //case 0x5C:      //'￥
            if (sValue.Contains("|") == true) { return CheckFileName; }     //case 0x7C:      //'｜
            if (sValue.Contains("\b") == true) { return CheckFileName; }     //case 0x7F:      //'DEL


            CheckFileName = true;
            return CheckFileName;

        }

        //==========================================================================================
        /*[VB]
            'ファイル名入力値が無効であるか検査する。
            '
            '引き数：
            'txtTextBox 検査対象コントロール。
            'sLabel 対象コントロールの名称。
            'bFocus 検査に引っかかった場合フォーカスを移すか？
            '
            '戻り値：
            '入力値が正常である場合 True を返す。
            'それ以外の場合 False を返す。
            Public Function CheckFileNameInputInvalid(ByVal txtTextBox As Object, ByVal sLabel As String, Optional ByVal bFocus As Boolean = True) As Boolean

                CheckFileNameInputInvalid = False
    
                '無効な文字は含まれているか？
                If Not CheckFileName(txtTextBox.Text) Then
                    Call MsgBox(sLabel & GUI_MSG_DISHONESTY, vbCritical)
                    If bFocus Then Call txtTextBox.SetFocus
                    Exit Function
                End If
    
                CheckFileNameInputInvalid = True
    
            End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /// <summary>
        /// ファイル名入力値が無効であるか検査する。
        ///
        /// 引き数：
        ///     txtTextBox 検査対象コントロール。
        ///     sLabel 対象コントロールの名称。
        ///     bFocus 検査に引っかかった場合フォーカスを移すか？
        /// 
        /// </summary>
        /// <param name="txtTextBox"></param>
        /// <param name="sLabel"></param>
        /// <param name="bFocus"></param>
        /// <returns></returns>
        public static bool CheckFileNameInputInvalid(object txtTextBox, string sLabel, bool bFocus)
        {
            bool CheckFileNameInputInvalid = false;


            string txtTextBox2 = txtTextBox.ToString();

            //無効な文字は含まれているか？
            //2     if (!CheckFileName(txtTextBox2.Text))
            if (!CheckFileName(txtTextBox2))
            {
                _ = MessageBox.Show($" {sLabel} {GUI_MSG_DISHONESTY}", "エラー発生", MessageBoxButtons.OK, MessageBoxIcon.Error);

                if (bFocus)
                {
                    //2  _ = txtTextBox2.Focus();
                }
                return CheckFileNameInputInvalid;
            }

            CheckFileNameInputInvalid = true;
            return CheckFileNameInputInvalid;


        }

        //2==========================================================================================
        /*[VB]
            'ファイル名入力値を検査する。
            '
            '引き数：
            'txtTextBox 検査対象コントロール。
            'sLabel 対象コントロールの名称。
            'bFocus 検査に引っかかった場合フォーカスを移すか？
            '
            '戻り値：
            '入力値が正常である場合 True を返す。
            'それ以外の場合 False を返す。
            Public Function CheckFileNameInput(ByVal txtTextBox As Object, ByVal sLabel As String, Optional ByVal bFocus As Boolean = True) As Boolean

                CheckFileNameInput = False
    
                '空であるか？
                If Not CheckInputEmpty(txtTextBox, sLabel, bFocus) Then Exit Function
    
                '無効な文字は含まれているか？
                If Not CheckFileNameInputInvalid(txtTextBox, sLabel, bFocus) Then Exit Function
    
                CheckFileNameInput = True
    
            End Function
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#] 2
        /// <summary>
        /// 'ファイル名入力値を検査する。
        /// '
        /// '引き数：
        /// 'txtTextBox 検査対象コントロール。
        /// 'sLabel 対象コントロールの名称。
        /// 'bFocus 検査に引っかかった場合フォーカスを移すか？
        /// 
        /// </summary>
        /// <param name="txtTextBox"></param>
        /// <param name="sLabel"></param>
        /// <param name="bFocus"></param>
        /// <returns>
        /// 戻り値：
        /// 入力値が正常である場合 True を返す。
        /// それ以外の場合 False を返す。
        /// </returns>
        public static bool CheckFileNameInput(string txtTextBox, string sLabel, bool bFocus = true)
        {
            bool CheckFileNameInput = false;

            //    '空であるか？
            if (!CheckInputEmpty(txtTextBox, sLabel, bFocus)) { return CheckFileNameInput; }

            //    '無効な文字は含まれているか？
            if (!CheckFileNameInputInvalid(txtTextBox, sLabel, bFocus)) { return CheckFileNameInput; }

            CheckFileNameInput = true;
            return CheckFileNameInput;


        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '文字列入力値を検査する。
        '
        '引き数：
        'txtTextBox 検査対象コントロール。
        'sLabel 対象コントロールの名称。
        'bFocus 検査に引っかかった場合フォーカスを移すか？
        '
        '戻り値：
        '入力値が正常である場合 True を返す。
        'それ以外の場合 False を返す。
        Public Function CheckStringInput(ByVal txtTextBox As Object, ByVal sLabel As String, Optional ByVal bFocus As Boolean = True) As Boolean

            CheckStringInput = False
    
            '空であるか？
            If Not CheckInputEmpty(txtTextBox, sLabel, bFocus) Then Exit Function
    
            '無効な文字は含まれているか？
            If Not CheckStringInputInvalid(txtTextBox, sLabel, bFocus) Then Exit Function
    
            CheckStringInput = True
    
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'ファイル名入力値を検査する。
        '
        '引き数：
        'txtTextBox 検査対象コントロール。
        'sLabel 対象コントロールの名称。
        'bFocus 検査に引っかかった場合フォーカスを移すか？
        '
        '戻り値：
        '入力値が正常である場合 True を返す。
        'それ以外の場合 False を返す。
        Public Function CheckFileNameInput(ByVal txtTextBox As Object, ByVal sLabel As String, Optional ByVal bFocus As Boolean = True) As Boolean

            CheckFileNameInput = False
    
            '空であるか？
            If Not CheckInputEmpty(txtTextBox, sLabel, bFocus) Then Exit Function
    
            '無効な文字は含まれているか？
            If Not CheckFileNameInputInvalid(txtTextBox, sLabel, bFocus) Then Exit Function
    
            CheckFileNameInput = True
    
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '文字数制限付きで文字列入力値を検査する。
        '
        'バイト数を評価する。
        'nMax以下を許可。
        '
        '引き数：
        'txtTextBox 検査対象コントロール。
        'sLabel 対象コントロールの名称。
        'nMax 最大文字数。
        'bFocus 検査に引っかかった場合フォーカスを移すか？
        '
        '戻り値：
        '入力値が正常である場合 True を返す。
        'それ以外の場合 False を返す。
        Public Function CheckStringInputLength(ByVal txtTextBox As Object, ByVal sLabel As String, ByVal nMax As Long, Optional ByVal bFocus As Boolean = True) As Boolean

            CheckStringInputLength = False
    
            '文字数検査。
            If Not CheckInputLength(txtTextBox, sLabel, nMax, bFocus) Then Exit Function
    
            '文字列入力値検査。
            If Not CheckStringInput(txtTextBox, sLabel, bFocus) Then Exit Function
    
            CheckStringInputLength = True
    
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '文字数制限付きでファイル名入力値を検査する。
        '
        'バイト数を評価する。
        'nMax以下を許可。
        '
        '引き数：
        'txtTextBox 検査対象コントロール。
        'sLabel 対象コントロールの名称。
        'nMax 最大文字数。
        'bFocus 検査に引っかかった場合フォーカスを移すか？
        '
        '戻り値：
        '入力値が正常である場合 True を返す。
        'それ以外の場合 False を返す。
        Public Function CheckFileNameInputLength(ByVal txtTextBox As Object, ByVal sLabel As String, ByVal nMax As Long, Optional ByVal bFocus As Boolean = True) As Boolean

            CheckFileNameInputLength = False
    
            '文字数検査。
            If Not CheckInputLength(txtTextBox, sLabel, nMax, bFocus) Then Exit Function
    
            'ファイル名入力値検査。
            If Not CheckFileNameInput(txtTextBox, sLabel, bFocus) Then Exit Function
    
            CheckFileNameInputLength = True
    
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]  //2
        /// <summary>
        /// 文字数制限付きでファイル名入力値を検査する。
        /// '
        /// バイト数を評価する。
        /// nMax以下を許可。
        /// '
        /// 引き数：
        /// txtTextBox 検査対象コントロール。
        /// sLabel 対象コントロールの名称。
        /// nMax 最大文字数。
        /// bFocus 検査に引っかかった場合フォーカスを移すか？
        /// 
        /// </summary>
        /// <param name="txtTextBox"></param>
        /// <param name="sLabel"></param>
        /// <param name="nMax"></param>
        /// <param name="bFocus"></param>
        /// <returns>
        /// 戻り値：
        /// 入力値が正常である場合 True を返す。
        /// それ以外の場合 False を返す。
        /// </returns>
        public static bool CheckFileNameInputLength(object txtTextBox, string sLabel, long nMax, bool bFocus = true)
        {
            bool CheckFileNameInputLength = false;

            _ = new System.Windows.Forms.TextBox();
            System.Windows.Forms.TextBox txtTextBox2 = (System.Windows.Forms.TextBox)txtTextBox;

            //'文字数検査。
            if (!CheckInputLength(txtTextBox2, sLabel, nMax, bFocus))
            {
                return CheckFileNameInputLength;

            }

            //'ファイル名入力値検査。
            if (!CheckFileNameInput(txtTextBox2.Text, sLabel, bFocus))
            {
                return CheckFileNameInputLength;
            }

            CheckFileNameInputLength = true;
            return CheckFileNameInputLength;

        }
        //==========================================================================================




        //==========================================================================================
        /*[VB]
        '0Trim実数書式化。
        '
        '引き数：
        'nValue 実数。
        'sFormat 書式。
        '
        '戻り値：書式化された文字列。
        Public Function Format0Trim(ByVal nValue As Double, ByVal sFormat As String) As String
            Format0Trim = Format$(nValue, sFormat)
            Format0Trim = RTrimEx(Format0Trim, "0")
            Format0Trim = RTrimEx(Format0Trim, ".")
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'ファイルを開くダイアログで取得された複数ファイル文字列を、文字列配列に変換する。
        '
        '引き数：
        'sList ファイルを開くダイアログで取得された文字列。
        'sPath ファイル毎に分割した文字列。配列の要素は(0 To ...)。
        Public Sub ConvertOpenDialogMultiList(ByVal sList As String, ByRef sPath() As String)
            '区切り文字。
            Dim sDelimiter As String
            sDelimiter = Chr(0)
            '複数選択で無い場合、区切り文字がない。
            Dim nPos As Long
            nPos = InStr(1, sList, sDelimiter)
            If nPos <= 0 Then
                ReDim sPath(0)
                sPath(0) = sList
                Exit Sub
            End If
            'フォルダのパスを取得。
            Dim sDir As String
            Dim nStr As Long
            sDir = Left$(sList, nPos - 1)
            nStr = nPos + 1
            'ファイル名の取得。
            Dim nUBound As Long
            Dim nLen As Long
            nUBound = 0
            nLen = Len(sList)
            Do While nStr <= nLen
                ReDim Preserve sPath(nUBound)
                nPos = InStr(nStr, sList, sDelimiter)
                If nPos > 0 Then
                    sPath(nUBound) = sDir & "\" & Mid$(sList, nStr, nPos - nStr)
                    nStr = nPos + 1
                Else
                    sPath(nUBound) = sDir & "\" & Mid$(sList, nStr)
                    nStr = nLen + 1
                End If
                nUBound = nUBound + 1
            Loop
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        public void ConvertOpenDialogMultiList(OpenFileDialog dlgCommonDialog, ref List<string> sPath)
        {
            for (int i = 0; i < dlgCommonDialog.FileNames.Count(); i++)
            {
                if (dlgCommonDialog.FileNames[i] != null)
                {
                    sPath.Add(dlgCommonDialog.FileNames[i]);
                }

            }

#if false   //以下は、VBと同様の処理の場合

            //'複数選択で無い場合、区切り文字がない。
            long nPos;
            nPos = mdiVBfunctions.InStr(1, dlgCommonDialog.FileNames[0], sDelimiter);
            if (nPos <= 0)
            {
                sPath.Add(sList);
                return;
            }
            //'フォルダのパスを取得。
            string sDir;
            long nStr;
            sDir = mdiVBfunctions.Left(sList, (int)(nPos - 1));
            nStr = nPos + 1;
            //'ファイル名の取得。
            long nUBound;
            long nLen;
            nUBound = 0;
            nLen = sList.Length;
            while (nStr <= nLen)
            {
                nPos = mdiVBfunctions.InStr((int)nStr, sList, sDelimiter);
                if (nPos > 0)
                {
                    sPath.Add($"{sDir}\\{mdiVBfunctions.Mid(sList, (int)nStr, (int)(nPos - nStr))}");
                    nStr = nPos + 1;
                }
                else
                {
                    sPath.Add($"{sDir}\\{mdiVBfunctions.Mid(sList, (int)nStr)}");
                    nStr = nLen + 1;

                }
            }
#endif   //以下は、VBと同様の処理の場合

        }

        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'ツールバーメニューをポップアップする。
        '
        '引き数：
        'objForm ツールバーの親フォーム。
        'objToolBar ツールバー。
        'sKey ボタンの指定。
        'objMenu ポップアップするメニュー。
        Public Sub PopupToolBarMenu(ByVal objForm As Form, ByVal objToolBar As Toolbar, ByVal sKey As String, ByVal objMenu As Menu)
            Dim tRect As RECT
            Call GetWindowRect(objToolBar.hWnd, tRect)
            Dim tPoint As POINT
            tPoint.X = tRect.Left
            tPoint.Y = tRect.Top
            Call ScreenToClient(objForm.hWnd, tPoint)
            Dim btn As Button
            Set btn = objToolBar.Buttons(sKey)
            tPoint.X = tPoint.X * Screen.TwipsPerPixelX + btn.Left
            tPoint.Y = tPoint.Y * Screen.TwipsPerPixelY + btn.Top + btn.Height
            Call objForm.PopupMenu(objMenu, , tPoint.X, tPoint.Y)
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'タブの切り替え。
        '
        '各タブに対応したコンテナオブジェクトの表示/非表示を、選択タブによって変更する。
        '
        '引き数：
        'tsTab タブコントロール。
        'objContainers コンテナオブジェクト配列。
        Public Sub ChangeTab(ByVal tsTab As TabStrip, ByVal objContainers As Object)
            Dim objTab As Object
            For Each objTab In tsTab.Tabs
                objContainers(objTab.Index - 1).Visible = objTab.Selected
            Next
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]  //2
        public static void ChangeTab(TabControl tsTab, GroupBox objContainers)
        {
            objContainers.Visible = true;
        }


        //==========================================================================================
    }
}
