//23/12/26 K.setoguchi@NV---------->>>>>>>>>>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace SurvLine
{
    public class MdlGUI
    {

        //'*******************************************************************************
        //'GUI関連
        //
        //Option Explicit
        //
        //'定数
        public const string GUI_MANUAL_LISTINDEX = "GUI_MANUAL_LISTINDEX";  //'As Stringリストインデックスのマニュアル設定。   

        //'メッセージ。
        public const string GUI_MSG_EMPTY = "を入力してください｡";
        public const string GUI_MSG_DISHONESTY = "の入力値が不正です。";
        public const string GUI_MSG_NOSELECT = "を選択してください｡";
        public const string GUI_MSG_LENGTH = "ﾊﾞｲﾄ以上は入力できません｡";
        public const string GUI_MSG_OVERFLOW = "の入力値がオーバーフローしました。";
        public const string GUI_MSG_INTEGER = "には整数を入力してください。";


        //'*******************************************************************************
        //'*******************************************************************************
        /// <summary>
        /// リストインデックスのマニュアル設定。
        ///
        /// 引き数：
        ///     objList 対象オブジェクト。
        ///     nListIndex ListIndexの値。
        /// 
        /// </summary>
        /// <param name="objList"></param>
        /// <param name="nListIndex"></param>
        public void SetListIndexManual(Object objList, long nListIndex)
        {

            //    'クリックイベントで Tag プロパティを参照し、GUI_MANUAL_LISTINDEX であるか評価する。
            string sTag;
        //    sTag = objList.Tag
        //    objList.ListIndex = nListIndex
        //    objList.Tag = sTag

        }
        //---------------------------------------------------------------------------------------
        //'リストインデックスのマニュアル設定。
        //'
        //'引き数：
        //'objList 対象オブジェクト。
        //'nListIndex ListIndexの値。
        //Public Sub SetListIndexManual(ByVal objList As Object, ByVal nListIndex As Long)
        //
        //    'クリックイベントで Tag プロパティを参照し、GUI_MANUAL_LISTINDEX であるか評価する。
        //    Dim sTag As String
        //    sTag = objList.Tag
        //    objList.Tag = GUI_MANUAL_LISTINDEX
        //    objList.ListIndex = nListIndex
        //    objList.Tag = sTag
        //
        //
        //End Sub


        //***************************************************************************
        //***************************************************************************
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
        public bool CheckInputEmpty(string txtTextBox, string sLabel, bool bFocus)
        {

            bool CheckInputEmpty = false;

            //    '空であるか？
            //    If Len(txtTextBox.Text) <= 0 Then
            //        Call MsgBox(sLabel & GUI_MSG_EMPTY, vbCritical)
            //        If bFocus Then Call txtTextBox.SetFocus
            //        Exit Function
            //    End If
            if (txtTextBox.Length <= 0)
            {
                MessageBox.Show($"{sLabel}{GUI_MSG_EMPTY}");
                return CheckInputEmpty;
            }

            //    CheckInputEmpty = True
            CheckInputEmpty = true;
            return CheckInputEmpty;
        }
        //-----------------------------------------------------------------------
        //'入力値が空であるか検査する。
        //'
        //'引き数：
        //'txtTextBox 検査対象コントロール。
        //'sLabel 対象コントロールの名称。
        //'bFocus 検査に引っかかった場合フォーカスを移すか？
        //'
        //'戻り値：
        //'入力値が正常である場合 True を返す。
        //'それ以外の場合 False を返す。
        //Public Function CheckInputEmpty(ByVal txtTextBox As Object, ByVal sLabel As String, Optional ByVal bFocus As Boolean = True) As Boolean
        //
        //    CheckInputEmpty = False
        //    
        //    '空であるか？
        //    If Len(txtTextBox.Text) <= 0 Then
        //        Call MsgBox(sLabel & GUI_MSG_EMPTY, vbCritical)
        //        If bFocus Then Call txtTextBox.SetFocus
        //        Exit Function
        //    End If
        //
        //    CheckInputEmpty = True
        //
        //
        //End Function
        //***************************************************************************
        //***************************************************************************




        //***************************************************************************
        //***************************************************************************
        /// <summary>
        /// 
        /// </summary>
        /// <param name="txtTextBox"></param>
        /// <param name="sLabel"></param>
        /// <param name="bFocus"></param>
        /// <returns></returns>
        public bool CheckFileNameInput(string txtTextBox, string sLabel, bool bFocus)
        {
            bool CheckFileNameInput = false;

            //    '空であるか？
            //    If Not CheckInputEmpty(txtTextBox, sLabel, bFocus) Then Exit Function
            if (!CheckInputEmpty(txtTextBox, sLabel, bFocus)){ return CheckFileNameInput; }

            //    '無効な文字は含まれているか？
            //    If Not CheckFileNameInputInvalid(txtTextBox, sLabel, bFocus) Then Exit Function
            if (!CheckFileNameInputInvalid(txtTextBox, sLabel, bFocus)) { return CheckFileNameInput; }



            //    CheckFileNameInput = True
            CheckFileNameInput = true;
            return CheckFileNameInput;


        }
        //-----------------------------------------------------------------------
        //'ファイル名入力値を検査する。
        //'
        //'引き数：
        //'txtTextBox 検査対象コントロール。
        //'sLabel 対象コントロールの名称。
        //'bFocus 検査に引っかかった場合フォーカスを移すか？
        //'
        //'戻り値：
        //'入力値が正常である場合 True を返す。
        //'それ以外の場合 False を返す。
        //Public Function CheckFileNameInput(ByVal txtTextBox As Object, ByVal sLabel As String, Optional ByVal bFocus As Boolean = True) As Boolean
        //
        //    CheckFileNameInput = False
        //    
        //    '空であるか？
        //    If Not CheckInputEmpty(txtTextBox, sLabel, bFocus) Then Exit Function
        //    
        //    '無効な文字は含まれているか？
        //    If Not CheckFileNameInputInvalid(txtTextBox, sLabel, bFocus) Then Exit Function
        //
        //    CheckFileNameInput = True
        //
        //
        //End Function
        //***************************************************************************
        //***************************************************************************



        //***************************************************************************
        //***************************************************************************
        public bool CheckString(string sValue)
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
        //---------------------------------------------------------------------------
        //'文字列に無効な文字が含まれているか検査する。
        //'
        //'引き数：
        //'sValue 文字列。
        //'
        //'戻り値：
        //'文字列に無効な文字が含まれていない場合 True を返す。
        //'それ以外の場合 False を返す。
        //Public Function CheckString(ByVal sValue As String) As Boolean
        //
        //    CheckString = False
        //
        //    Dim i As Long
        //    For i = 1 To Len(sValue)
        //        Select Case Asc(Mid(sValue, i))
        //        Case &H22& 'ダブルクォーテーション。
        //            Exit Function
        //        Case &H2C& 'カンマ。
        //            Exit Function
        //        End Select
        //    Next
        //
        //    CheckString = True
        //
        //
        //End Function
        //***************************************************************************
        //***************************************************************************


        //***************************************************************************
        //***************************************************************************
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
        public bool CheckStringInputInvalid( string txtTextBox, string sLabe, bool bFocus)
        {
            bool CheckStringInputInvalid = false;


            //    '無効な文字は含まれているか？
            //    If Not CheckString(txtTextBox.Text) Then
            //        Call MsgBox(sLabel & GUI_MSG_DISHONESTY, vbCritical)
            //        If bFocus Then Call txtTextBox.SetFocus
            //        Exit Function
            //    End If
            if (!CheckString(txtTextBox))
            {

                MessageBox.Show($"{sLabe}{GUI_MSG_EMPTY}");
                return CheckStringInputInvalid;

            }

            CheckStringInputInvalid = true;
            return CheckStringInputInvalid;
        }
        //-----------------------------------------------------------------------------
        //'文字列入力値が無効であるか検査する。
        //'
        //'引き数：
        //'txtTextBox 検査対象コントロール。
        //'sLabel 対象コントロールの名称。
        //'bFocus 検査に引っかかった場合フォーカスを移すか？
        //'
        //'戻り値：
        //'入力値が正常である場合 True を返す。
        //'それ以外の場合 False を返す。
        //Public Function CheckStringInputInvalid(ByVal txtTextBox As Object, ByVal sLabel As String, Optional ByVal bFocus As Boolean = True) As Boolean
        //
        //    CheckStringInputInvalid = False
        //
        //    '無効な文字は含まれているか？
        //    If Not CheckString(txtTextBox.Text) Then
        //        Call MsgBox(sLabel & GUI_MSG_DISHONESTY, vbCritical)
        //        If bFocus Then Call txtTextBox.SetFocus
        //        Exit Function
        //    End If
        //
        //   CheckStringInputInvalid = True
        //
        //
        // End Function
        //***************************************************************************
        //***************************************************************************


        //***************************************************************************
        //***************************************************************************
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
        public bool CheckFileName(string sValue)
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

#if false   ////////////////////////////////////////////////////////////////////////////////////

            long nChar;
            //    Dim i As Long
            //    For i = 1 To Len(sValue)
            for (int i = 0; i < sValue.Length; i++)
            {
                //        Dim nChar As Long
                //        nChar = Asc(Mid(sValue, i))
                //        If &H0& <= nChar And nChar< &H20& Then Exit Function '制御コード。
                string sWork = sValue.Substring(i, 1);
                // nChar = long.Parse(sWork);
                byte[] data;
                data = System.Text.Encoding.GetEncoding("ascii").GetBytes(sWork);

                nChar = (long)data[0];


                if (0x00 <= nChar && nChar < 0x20) { return CheckFileName; }    //'制御コード。

                switch (nChar)
                {
                    case 0x22:      //”
                        return CheckFileName;
                    case 0x2A:      //'＊
                        return CheckFileName;
                    case 0x2C:      //'，
                        return CheckFileName;
                    case 0x2F:      //'／
                        return CheckFileName;
                    case 0x3A:      //'：
                        return CheckFileName;
                    case 0x3C:      //'＜
                        return CheckFileName;
                    case 0x3E:      //'＞
                        return CheckFileName;
                    case 0x3F:      //'？
                        return CheckFileName;
                    case 0x5C:      //'￥
                        return CheckFileName;
                    case 0x7C:      //'｜
                        return CheckFileName;
                    case 0x7F:      //'DEL
                        return CheckFileName;
                }

            }
            CheckFileName = true;
            return CheckFileName;
#endif  //#if false   ////////////////////////////////////////////////////////////////////////////////////

        }
        //-----------------------------------------------------------------------
        //'文字列にファイル名として無効な文字が含まれているか検査する。
        //'
        //'引き数：
        //'sValue 文字列。
        //'
        //'戻り値：
        //'文字列にファイル名として無効な文字が含まれていない場合 True を返す。
        //'それ以外の場合 False を返す。
        //Public Function CheckFileName(ByVal sValue As String) As Boolean
        //
        //    CheckFileName = False
        //
        //    Dim i As Long
        //    For i = 1 To Len(sValue)
        //        Dim nChar As Long
        //        nChar = Asc(Mid(sValue, i))
        //        If &H0& <= nChar And nChar< &H20& Then Exit Function '制御コード。
        //        Select Case nChar
        //        Case &H22& '”
        //            Exit Function
        //        Case &H2A& '＊
        //            Exit Function
        //        Case &H2C& '，
        //            Exit Function
        //        Case &H2F& '／
        //            Exit Function
        //        Case &H3A& '：
        //            Exit Function
        //        Case &H3C& '＜
        //            Exit Function
        //        Case &H3E& '＞
        //            Exit Function
        //        Case &H3F& '？
        //            Exit Function
        //        Case &H5C& '￥
        //            Exit Function
        //        Case &H7C& '｜
        //            Exit Function
        //        Case &H7F& 'DEL
        //            Exit Function
        //        End Select
        //    Next
        //
        //
        //    CheckFileName = True
        //
        //
        //End Function
        //***************************************************************************
        //***************************************************************************



        //***************************************************************************
        //***************************************************************************
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
        public bool CheckFileNameInputInvalid(string txtTextBox, string sLabel, bool bFocus)
        {
            //    CheckFileNameInputInvalid = False
            bool CheckFileNameInputInvalid = false;


            //    '無効な文字は含まれているか？
            //    If Not CheckFileName(txtTextBox.Text) Then
            //        Call MsgBox(sLabel & GUI_MSG_DISHONESTY, vbCritical)
            //        If bFocus Then Call txtTextBox.SetFocus
            //        Exit Function
            //    End If
            if (!CheckFileName(txtTextBox))
            {
                return CheckFileNameInputInvalid;
            }


            //    CheckFileNameInputInvalid = True
            CheckFileNameInputInvalid = true;
            return CheckFileNameInputInvalid;


        }
        //-----------------------------------------------------------------------
        //'ファイル名入力値が無効であるか検査する。
        //'
        //'引き数：
        //'txtTextBox 検査対象コントロール。
        //'sLabel 対象コントロールの名称。
        //'bFocus 検査に引っかかった場合フォーカスを移すか？
        //'
        //'戻り値：
        //'入力値が正常である場合 True を返す。
        //'それ以外の場合 False を返す。
        //Public Function CheckFileNameInputInvalid(ByVal txtTextBox As Object, ByVal sLabel As String, Optional ByVal bFocus As Boolean = True) As Boolean
        //
        //    CheckFileNameInputInvalid = False
        //    
        //    '無効な文字は含まれているか？
        //    If Not CheckFileName(txtTextBox.Text) Then
        //        Call MsgBox(sLabel & GUI_MSG_DISHONESTY, vbCritical)
        //        If bFocus Then Call txtTextBox.SetFocus
        //        Exit Function
        //    End If
        //
        //    CheckFileNameInputInvalid = True
        //
        //
        //End Function
    }

}
//<<<<<<<<<-----------23/12/26 K.setoguchi@NV
