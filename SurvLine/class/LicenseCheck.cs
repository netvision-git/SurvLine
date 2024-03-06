using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.AxHost;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Windows.Forms;

namespace SurvLine
{
    internal class LicenseCheck
    {
        //==========================================================================================
        /*[VB]
        Option Explicit
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        Private Const RSA_P As String = "4242518773"
        Private Const RSA_Q As String = "3797596837"
        Private Const RSA_N As String = "16111375873257921001"
        Private Const RSA_E As String = "3891413257"
        Private Const RSA_D As String = "1177725921494746549"
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        private const string RSA_P = "4242518773";
        private const string RSA_Q = "3797596837";
        private const string RSA_N = "16111375873257921001";
        private const string RSA_E = "3891413257";
        private const string RSA_D = "1177725921494746549";
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        Public Function Encode(sPlain As String) As String

            Dim lN As LINT
            Dim lE As LINT
            Dim sCipher As String    '暗号文（16進文字列）
    
        '    Dim lP As LINT
        '    Dim lPM As LINT

            Encode = ""

            lN = LINTread(RSA_N)
            lE = LINTread(RSA_E)
    
        '    lP = LINTConvert16to10(sPlain)
        '    Debug.Print "LP = " & LINTsset(lP)
        '    lPM = LINTpwrmod(lP, lE, lN)
        '    Debug.Print "LPM = " & LINTsset(lPM)
        '    sCipher = LINTConvert10to16(lPM, 16)
    
            '暗号化
            sCipher = LINTConvert10to16(LINTpwrmod2(LINTConvert16to10(sPlain), lE, lN), 16)

            '念のため、文字列の並びを逆にする
            sCipher = StrReverse(sCipher)


            Encode = sCipher

        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        Public Function Decode(sS As String) As String

            Dim lN As LINT
            Dim lD As LINT
            Dim sCipher As String    '暗号文（16進文字列）
            Dim sPlain As String    '暗号文（16進文字列）
    
        '    Dim lP As LINT
        '    Dim lPM As LINT

            Decode = ""

            sCipher = sS

            lN = LINTread(RSA_N)
            lD = LINTread(RSA_D)
    
            '文字列の並びを逆にする
            sCipher = StrReverse(sCipher)
    
        '    Debug.Print "sCipher = " & sCipher
        '    lPM = LINTConvert16to10(sCipher)
        '    Debug.Print "LPM = " & LINTsset(lPM)
        '    lP = LINTpwrmod(lPM, lD, lN)
        '    Debug.Print "LP = " & LINTsset(lP)
        '    sPlain = LINTConvert10to16(lP, 16)
    
            '復号
            sPlain = LINTConvert10to16(LINTpwrmod2(LINTConvert16to10(sCipher), lD, lN), 16)
 
            Decode = sPlain

        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        Public Function Check(sCode As String, sInApp As String, iInMajor As Integer, iInMinor As Integer, sInKeySerial As String, ByRef sResult As String) As Boolean
            Dim sApp As String          'アプリケーションの種類(0=NS-Note,1=NS-Network,2=NS-Survey)
            Dim iMajor As Integer        'メジャーバージョン(0～255)
            Dim iMinor As Integer        'マイナーバージョン(0～255)
            Dim iRevision As Integer     'リビジョン(0～255)
            Dim sKeySerial As String    'Sentinelキーのシリアル
            Dim iDateFlag As Integer    'アプリの使用期限(0=無期限、1=期限付)
            Dim dDate As Date         '日付(Sentinel形式16進数4桁)


            Dim sPlain As String    '平文（16進文字列）

            Check = False

            '復号
            sPlain = Decode(sCode)


            If AnalyzePlain(sPlain, sApp, iMajor, iMinor, iRevision, sKeySerial, iDateFlag, dDate) = True Then
                If sKeySerial<> sInKeySerial Then
                    sResult = "ハードウェアキーが一致しません。"
                ElseIf sApp <> sInApp Then
                    sResult = "アプリケーションが一致しません。"
                ElseIf iMajor <> iInMajor Or iMinor<> iInMinor Then
                    sResult = "アプリケーションのバージョンが一致しません。"
                Else
                    Check = True
                End If
            Else
                sResult = "ライセンスコードが不正です。"
            End If
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        Public Function MakePlain(iApp As Integer, iMajor As Integer, iMinor As Integer, iRevision As Integer, sKeySerial As String, iDateFlag As Integer, dDate As Date) As String

            Dim sApp As String          'アプリケーションの種類(0=NS-Note,1=NS-Network,2=NS-Survey)
            Dim sMajor As String        'メジャーバージョン(0～255)
            Dim sMinor As String        'マイナーバージョン(0～255)
            Dim sRevision As String     'リビジョン(0～255)
            Dim sDate As String         '日付(Sentinel形式16進数4桁)「0000」は無期限


            Dim sCheckSum As String          'チェックサム２バイト

            Dim sPlain As String    '平文（16進文字列）

            MakePlain = ""

            sApp = Convert10to16(iApp, 1)   'アプリケーションの種類
            sMajor = Convert10to16(iMajor, 2) 'アプリケーションのバージョン
            sMinor = Convert10to16(iMinor, 2)
            sRevision = Convert10to16(iRevision, 1)
            If iDateFlag = 0 Then
                sDate = "0000"
            Else
                sDate = MakeDate(dDate)
            End If


            sPlain = sDate & sMajor & sMinor & sRevision & sKeySerial & sApp
                sCheckSum = Convert10to16(CalcCheckSum(sPlain), 2)
            '平文生成
            MakePlain = sPlain & sCheckSum
                End Function
                Public Function AnalyzePlain(sPlain As String, ByRef sApp As String, ByRef iMajor As Integer, ByRef iMinor As Integer, ByRef iRevision As Integer, ByRef sKeySerial As String, ByRef iDateFlag As Integer, ByRef dDate As Date) As Boolean

            Dim sDate As String         '日付(Sentinel形式16進数4桁)
            Dim sCheckSum As String     'チェックサム２バイト

            AnalyzePlain = False

            'アプリの使用期限
            sDate = Mid(sPlain, 1, 4)
            If sDate = "0000" Then
                iDateFlag = 0
                dDate = "1980/1/1"
            Else
                iDateFlag = 1
                dDate = CalcDate(Mid(sPlain, 1, 4))
                If dDate = "1980/1/1" Then
                Exit Function
                End If
            End If
    
            'アプリのバージョン
            iMajor = Convert16to10(Mid(sPlain, 5, 2))
            iMinor = Convert16to10(Mid(sPlain, 7, 2))
            iRevision = Convert16to10(Mid(sPlain, 9, 1))
    
            'アプリのシリアル
            sKeySerial = Mid(sPlain, 10, 4)
    
            'アプリケーションの種類(0=NS-Note,1=NS-Network,2=NS-Survey)
            Select Case Convert16to10(Mid(sPlain, 14, 1))
                Case 0
                    sApp = "NS-Note"
                Case 1
                    sApp = "NS-Network"
                Case 2
                    sApp = "NS-Survey"
                Case Else
                    sApp = ""
            End Select
    
            'チェックサム
            sCheckSum = Mid(sPlain, 15, 2)
            If CalcCheckSum(Mid(sPlain, 1, 14)) = Convert16to10(sCheckSum) Then
                AnalyzePlain = True
            End If
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        Private Function Convert16to10(S As String) As Integer
        '    Dim i As Integer
        '    Dim iValue As Integer
        '    Dim a As Integer
        '    Dim bFlag As Boolean
        '
        '    Dim cs As String


            Convert16to10 = Val("&H" & S)
    
        '    a = 0
        '
        '    For i = 1 To Len(s)
        '        bFlag = False
        '        cs = Mid(s, i, 1)
        '        If Asc(cs) >= Asc("0") And Asc(cs) <= Asc("9") Then
        '            iValue = Asc(cs) - Asc("0")
        '            bFlag = True
        '        ElseIf Asc(cs) >= Asc("A") And Asc(cs) <= Asc("Z") Then
        '            iValue = Asc(cs) - Asc("A") + 10
        '            bFlag = True
        '        ElseIf Asc(cs) >= Asc("a") And Asc(cs) <= Asc("z") Then
        '            iValue = Asc(cs) - Asc("a") + 10
        '            bFlag = True
        '        End If
        '        If bFlag = True Then
        '            a = a * 16 + iValue
        '        End If
        '    Next i
        '
        '    Convert16to10 = a
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        Private Function Convert10to16(A As Integer, Optional Order As Integer = 0) As String
            Dim B As Integer
            Dim M As Integer
            Dim i As Integer
            Dim sOut As String


            B = A
            sOut = ""
            Do While(B<> 0)
                M = B Mod 16
                sOut = sOut & Hex(M)
                B = B \ 16
            Loop

            If Order<> 0 Then
                For i = Len(sOut) To Order - 1
                    sOut = sOut & "0"
                Next i
            End If


            Convert10to16 = StrReverse(sOut)


        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        Private Function CalcCheckSum(S As String) As Integer
            Dim i As Integer
            Dim iSum As Integer


            iSum = 0
    
            For i = 1 To Len(S)
                iSum = iSum + Convert16to10(Mid(S, i, 1))
            Next i


            CalcCheckSum = iSum Mod 256
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        Private Function MakeDate(dDate As Date) As String
            Dim iYear As Integer
            Dim iMonth As Integer
            Dim iDay As Integer


            Dim A As Integer
            Dim B As Integer
            Dim C As Integer
            Dim D As Integer
            Dim E As Integer


            Dim sYear As String
            Dim sMonth As String
            Dim sDay As String


            MakeDate = ""
    
            iYear = Year(dDate)
            iMonth = Month(dDate)
            iDay = Day(dDate)


            A = (iYear - 1980) * 2
            B = iMonth \ 8
            C = iMonth Mod 8
            D = iDay \ 16
            E = iDay Mod 16
            sYear = Hex(A + B)
            sMonth = Hex(C* 2 + D)
            sDay = Hex(E)
            MakeDate = sYear & sMonth & sDay
                End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        Private Function CalcDate(sDate As String) As Date
        On Error GoTo ErrorHandler
            Dim iYear As Integer
            Dim iMonth As Integer
            Dim iDay As Integer


            Dim sYear As String
            Dim sMonth As String
            Dim sDay As String


            CalcDate = "1980/1/1"
    
            iYear = Val("&H" & Mid(sDate, 1, 2))
            iMonth = Val("&H" & Mid(sDate, 3, 1))
            iDay = Val("&H" & Mid(sDate, 4, 1))
    
            sYear = 1980 + iYear \ 2
            sMonth = iMonth \ 2 + (iYear Mod 2) * 8
            sDay = iDay + (iMonth Mod 2) * 16
            CalcDate = sYear & "/" & sMonth & "/" & sDay
        ErrorHandler:
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //==========================================================================================


    }
}
