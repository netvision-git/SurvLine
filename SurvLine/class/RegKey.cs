using SurvLine.mdl;
using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static SurvLine.mdl.MdiDefine;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;


namespace SurvLine
{
    internal class RegKey
    {


        //'*******************************************************************************
        //'レジストリキー。

        //Option Explicit

        //'インプリメンテーション
        private long m_hKey;        // As Long 'レジストリキーハンドル。

        //'*******************************************************************************
        //'プロパティ

        //==========================================================================================
        /*[VB]
        'レジストリキー。
        Property Get Key() As Long
            Key = m_hKey
        End Property
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        public long Key(){
            long Key = m_hKey;
            return Key;
        }

        //'*******************************************************************************
        //'イベント

        public RegKey()
        {

        }

        //==========================================================================================
        /*[VB]
        '初期化。
        Private Sub Class_Initialize()
            m_hKey = 0
        End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        private void Class_Initialize()
        {
            m_hKey = 0;
        }


        //==========================================================================================
        /*[VB]
        'レジストリキーを閉じる。
        Private Sub Class_Terminate()
            If m_hKey<> 0 Then
                If RegCloseKey(m_hKey) <> ERROR_SUCCESS Then Call mdlMain.ErrorExit
            End If
        End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        private void Class_Terminate()
        {
            if (m_hKey != 0)
            {
                if (DEFINE.RegCloseKey(m_hKey) != DEFINE.ERROR_SUCCESS) {

                    //坂井様へ  Then Call mdlMain.ErrorExit
                }

            }

        }

        //==========================================================================================
        /*[VB]
        '*******************************************************************************
        'メソッド

        'レジストリキーを作成する。
        '
        '指定されたレジストリキーを作成する。
        'そのキーが既に存在している場合そのキーを開く。
        '
        '引き数：
        'hParent 親キーのハンドル。
        'sKey 作成するキーの名前。
        Public Sub CreateKey(ByVal hParent As Long, ByVal sKey As String)
        Dim nDisposition As Long
        Dim nResult As Long
        nResult = RegCreateKeyEx(hParent, sKey, 0, 0, 0, KEY_ALL_ACCESS, 0, m_hKey, nDisposition)
        If nResult<> ERROR_SUCCESS Then Call Err.Raise(vbObjectError Or &H200& Or &H1&, , GetErrorMessage(nResult))
        End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        public void CreateKey(long hParent, string sKey)
        {
            long nDisposition = 0;
            long nResult;

            nResult = DEFINE.RegCreateKeyEx(hParent, sKey, 0, 0, 0, DEFINE.KEY_ALL_ACCESS, 0, ref m_hKey, ref nDisposition);

            if ( nResult != DEFINE.ERROR_SUCCESS)
            {
                //坂井様へ  Then Call Err.Raise(vbObjectError Or & H200 & Or & H1 &, , GetErrorMessage(nResult))
            }


        }


        //==========================================================================================
        /*[VB]
        'レジストリキーを開く。
        '
        '指定されたレジストリキーを開く。
        '
        '引き数：
        'hParent 親キーのハンドル。
        'sKey 開くキーの名前。
        '
        '戻り値：開くことができたら True を返す。
        Public Function OpenKey(ByVal hParent As Long, ByVal sKey As String) As Boolean
        Dim nResult As Long
        nResult = RegOpenKeyEx(hParent, sKey, 0, KEY_ALL_ACCESS, m_hKey)
        If nResult = ERROR_SUCCESS Then OpenKey = True
        End Function
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        public bool OpenKey(long hParent, string sKey)
        {
            bool OpenKey = false;

            long nResult;
            nResult = DEFINE.RegOpenKeyEx(hParent, sKey, 0, DEFINE.KEY_ALL_ACCESS,ref m_hKey);


            OpenKey = true;
            return OpenKey;
        }



        //==========================================================================================
        /*[VB]
        '整数値を設定する。
        '
        '指定されたレジストリエントリに整数値を設定する。
        '
        '引き数：
        'sName レジストリエントリ名。
        'nValue 整数値。
        Public Sub SetInteger(ByVal sName As String, ByVal nValue As Long)
        Dim nResult As Long
        nResult = RegSetValueEx(m_hKey, sName, 0, REG_DWORD, nValue, 4)
        If nResult<> ERROR_SUCCESS Then Call Err.Raise(vbObjectError Or &H200& Or &H1&, , GetErrorMessage(nResult))
        End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        public void SetInteger(string sName, long nValue)
        {
            long nResult;
            nResult = DEFINE.RegSetValueEx(m_hKey, sName, 0, DEFINE.REG_DWORD, ref nValue, 4);
            if (nResult != DEFINE.ERROR_SUCCESS)
            {
                //Then Call Err.Raise(vbObjectError Or &H200 & Or & H1 &, , GetErrorMessage(nResult))
            }

        }


        //==========================================================================================
        /*[VB]
        '整数値を取得する。
        '
        '指定されたレジストリエントリのデータを整数値として取得する。
        '
        '引き数：
        'sName レジストリエントリ名。
        'nDefault レジストリエントリが無かった場合返されるデフォルト値。
        '
        '戻り値：整数値。
        Public Function QueryValue(ByVal sName As String, ByVal nDefault As Long) As Long
        Dim nType As Long
        Dim nValue As Long
        Dim nSize As Long
        Dim nResult As Long
        nSize = 4
        nResult = RegQueryValueEx(m_hKey, sName, 0, nType, nValue, nSize)
        Select Case nResult
        Case ERROR_SUCCESS
            QueryValue = nValue
        Case ERROR_FILE_NOT_FOUND
            QueryValue = nDefault
        Case Else
            Call Err.Raise(vbObjectError Or &H200& Or &H1&, , GetErrorMessage(nResult))
        End Select
        End Function
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        public long QueryValue(string sName, long nDefault)
        {
            long QueryValue = 0;
            long nType = 0;
            long nValue = 0;
            long nSize = 0;
            long nResult;
            nSize = 4;
            nResult = DEFINE.RegQueryValueEx(m_hKey, sName, 0, ref nType, ref nValue, ref nSize);

            switch (nResult)
            {
                case DEFINE.ERROR_SUCCESS:
                    QueryValue = nValue;
                    break;
                case DEFINE.ERROR_FILE_NOT_FOUND:
                    QueryValue = nDefault;
                    break;
                default:
                    //坂井様へ  Call Err.Raise(vbObjectError Or & H200 & Or & H1 &, , GetErrorMessage(nResult))
                    break;

            }
            return QueryValue;
        }




        //==========================================================================================
        /*[VB]
        '文字列値を設定する。
        '
        '指定されたレジストリエントリに文字列値を設定する。
        '
        '引き数：
        'sName レジストリエントリ名。
        'sValue 文字列値。
        Public Sub SetString(ByVal sName As String, ByVal sValue As String)
            Dim nLeng As Long
            nLeng = LenB(StrConv(sValue, vbFromUnicode)) + 1
            Dim nResult As Long
            nResult = RegSetValueString(m_hKey, sName, 0, REG_SZ, ByVal sValue, nLeng)
            If nResult<> ERROR_SUCCESS Then Call Err.Raise(vbObjectError Or &H200& Or &H1&, , GetErrorMessage(nResult))
        End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        public void SetString(string sName, string sValue)
        {
            long nLeng = 0;
//            nLeng = LenB(StrConv(sValue, vbFromUnicode)) + 1;
            long nResult;
            nResult = DEFINE.RegSetValueString(m_hKey, sName, 0, DEFINE.REG_SZ, ref sValue, nLeng);
            if (nResult != DEFINE.ERROR_SUCCESS)
            {
                //  Then Call Err.Raise(vbObjectError Or & H200 & Or & H1 &, , GetErrorMessage(nResult))
            }

        }


        //==========================================================================================
        /*[VB]
        '文字列値を取得する。
        '
        '指定されたレジストリエントリのデータを文字列値として取得する。
        '
        '引き数：
        'sName レジストリエントリ名。
        'sDefault レジストリエントリが無かった場合返されるデフォルト値。
        '
        '戻り値：文字列値。
        Public Function QueryValueString(ByVal sName As String, ByVal sDefault As String) As String
        Dim sValue As String
        Dim nSize As Long
        Dim nResult As Long
        nSize = MAX_PATH
        sValue = String(nSize, " ")
        nResult = RegQueryValueString(m_hKey, sName, 0, 0, sValue, nSize)
        Select Case nResult
        Case ERROR_SUCCESS
        QueryValueString = Left$(sValue, InStr(sValue, Chr(0)) - 1)
        Case ERROR_FILE_NOT_FOUND
        QueryValueString = sDefault
        Case Else
        Call Err.Raise(vbObjectError Or &H200& Or &H1&, , GetErrorMessage(nResult))
        End Select
        End Function
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        public string QueryValueString(string sName, string sDefault)
        {
            string QueryValueString = "";

            string sValue;
            long nSize;
            long nResult;
            nSize = DEFINE.MAX_PATH;

            sValue =nSize.ToString();

            nResult = DEFINE.RegQueryValueString(m_hKey, sName, 0, 0, sValue, ref nSize);

            switch (nResult)
            {
                case DEFINE.ERROR_SUCCESS:
//                    QueryValueString = Left$(sValue, InStr(sValue, Chr(0)) - 1);
                    break;
                case DEFINE.ERROR_FILE_NOT_FOUND:
                    QueryValueString = sDefault;
                    break;
                default:
                    //坂井様へ   Call Err.Raise(vbObjectError Or & H200 & Or & H1 &, , GetErrorMessage(nResult))
                    break;
            }

            return QueryValueString;

        }




        //==========================================================================================
        /*[VB]
        'キーの名前を列挙する。
        '
        '引き数：
        'nIndex 取得するキーのインデックス。
        'nSize 文字数の制限値。
        '
        '戻り値：キーの名前を返す。キーがない場合は空文字を返す。
        Public Function EnumKeyName(ByVal nIndex As Long, ByVal nSize As Long) As String
        Dim sName As String
        sName = Space(nSize + 32)
        Dim tLastWriteTime As FILETIME
        Dim nResult As Long
        nResult = RegEnumKeyEx(m_hKey, nIndex, sName, nSize, 0, 0, 0, tLastWriteTime)
        If nResult = ERROR_SUCCESS Then
        EnumKeyName = Left$(sName, InStr(sName, Chr(0)) - 1)
        Else
        EnumKeyName = ""
        End If
        End Function
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        public string EnumKeyName(long nIndex, long nSize)
        {
            string EnumKeyName = "";

            string sName;
//            sName = Space(nSize + 32);
            //FILETIME tLastWriteTime;
            DateTime tLastWriteTime;
            long nResult = 0;


//            nResult = RegEnumKeyEx(m_hKey, nIndex, sName, nSize, 0, 0, 0, tLastWriteTime);

            if (nResult == DEFINE.ERROR_SUCCESS)
            {
//                EnumKeyName = Left$(sName, InStr(sName, Chr(0)) - 1)
            }
            else
            {
                EnumKeyName = "";
            }


            return EnumKeyName;

        }






    }
}
