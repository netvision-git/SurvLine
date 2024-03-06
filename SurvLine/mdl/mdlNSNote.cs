using SurvLine.Properties;
using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using static SurvLine.mdl.DEFINE;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using Microsoft.VisualBasic;

namespace SurvLine.mdl
{
    internal class mdlNSNote
    {
        //==========================================================================================
        /*[VB]
        '*******************************************************************************
        '旧NS-Note用ユーティリティ

        Option Explicit
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'レジストリへ文字列値を書き込む。
        '
        '引き数：
        'StrName レジストリエントリ名。
        'strValue 文字列値。
        '
        '戻り値：
        '正常終了の場合 True を返す。
        'それ以外の場合 False を返す。
        Public Function SetRegValue(StrName As String, strValue As String) As Boolean

            SetRegValue = False

            Dim clsRegNsNetwork As New RegKey
            Set clsRegNsNetwork = OpenRegistry()


            Dim nLeng As Long
             '値を設定する
            If Len(strValue) > 0 Then
                nLeng = CLng(LenB(StrConv(strValue, vbFromUnicode))) + 1
            Else
                nLeng = 0
                strValue = vbNull
            End If
            Dim ret As Long
            ret = RegSetValueString(clsRegNsNetwork.Key, StrName, 0, REG_SZ, ByVal strValue, nLeng)
            If ret<> ERROR_SUCCESS Then Exit Function


            SetRegValue = True


        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        'レジストリへ文字列値を書き込む。
        '
        '引き数：
        'StrName レジストリエントリ名。
        'strValue 文字列値。
        '
        '戻り値：
        '正常終了の場合 True を返す。
        'それ以外の場合 False を返す。
        */
        public bool SetRegValue(string StrName, ref string strValue)
        {
#if false
            RegKey clsRegNsNetwork = new RegKey();
            clsRegNsNetwork = OpenRegistry();

            long nLeng;
            //'値を設定する
            if (strValue.Length > 0)
            {
                nLeng = long.Parse(Strings.StrConv(strValue, VbStrConv.Wide)) + 1;
            }
            else
            {
                nLeng = 0;
                strValue = null;
            }
            long ret;
            ret = RegSetValueString(clsRegNsNetwork.Key, StrName, 0, REG_SZ, ref strValue, nLeng);
            if (ret != ERROR_SUCCESS)
            {
                return false;
            }
#endif
            return true;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'レジストリから文字列値を読み込む。
        '
        '引き数：
        'StrName レジストリエントリ名。
        'strValue 読み込んだ文字列値が設定される。
        '
        '戻り値：
        '正常終了の場合 True を返す。
        'それ以外の場合 False を返す。
        Public Function GetRegValue(StrName As String, strValue As String) As Boolean

            GetRegValue = False

            Dim clsRegNsNetwork As New RegKey
            Set clsRegNsNetwork = OpenRegistry()
    
            'バッファを確保する
            Dim sTemp As String
            Dim nLeng As Long
            sTemp = String(255, " ")
            nLeng = Len(sTemp)
    
            '値を取得する
            Dim ret As Long
            ret = RegQueryValueString(clsRegNsNetwork.Key, StrName, 0, 0, sTemp, nLeng)
            If ret<> ERROR_SUCCESS Then Exit Function


            strValue = Left$(sTemp, InStr(sTemp, Chr(0)) - 1)
    
            GetRegValue = True

        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        'レジストリから文字列値を読み込む。
        '
        '引き数：
        'StrName レジストリエントリ名。
        'strValue 読み込んだ文字列値が設定される。
        '
        '戻り値：
        '正常終了の場合 True を返す。
        'それ以外の場合 False を返す。
        */
        public bool GetRegValue(string StrName, ref string strValue)
        {
#if false
            RegKey clsRegNsNetwork = new RegKey();
            clsRegNsNetwork = OpenRegistry();


            //'バッファを確保する
            string sTemp;
            long nLeng;
            sTemp = "                                                                                                    ";
            nLeng = sTemp.Length;


            //'値を取得する
            long ret;
            ret = RegQueryValueString(clsRegNsNetwork.Key, StrName, 0, 0, sTemp, nLeng);
            if (ret != ERROR_SUCCESS)
            {
                return false;
            }

            strValue = MdlUtility.Left(sTemp, sTemp.IndexOf("\\") - 1);
#endif
            return true;

        }
        //==========================================================================================
    }
}
