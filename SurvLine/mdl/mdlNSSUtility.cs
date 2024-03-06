using Microsoft.VisualBasic;
using Microsoft.VisualBasic.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using System.Windows.Forms;

namespace SurvLine.mdl
{
    internal class MdlNSSUtility
    {
        //==========================================================================================
        /*[VB]
        '*******************************************************************************
        'NSS用ユーティリティ

        Option Explicit
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '指定された基線ベクトル群の解析期間の取得。
        '
        'objBaseLineVectors で指定される基線ベクトルの中で、最小の解析開始日時と最大の解析終了日時を取得する。
        '
        '引き数：
        'objBaseLineVectors 基線ベクトル。要素は BaseLineVector オブジェクト。キーは任意。
        'tTimeStr 解析開始日時(JST)が設定される。
        'tTimeEnd 解析終了日時(JST)が設定される。
        Public Sub GetAnalisysSpan(ByVal objBaseLineVectors As Collection, ByRef tTimeStr As Date, ByRef tTimeEnd As Date)
            If objBaseLineVectors.Count <= 0 Then
                tTimeStr = MIN_TIME
                tTimeEnd = MIN_TIME
            Else
                Dim clsBaseLineVector As BaseLineVector
                Dim i As Long
                Set clsBaseLineVector = objBaseLineVectors.Item(1)
                tTimeStr = clsBaseLineVector.StrTimeJST
                tTimeEnd = clsBaseLineVector.EndTimeJST
                For i = 2 To objBaseLineVectors.Count
                    Set clsBaseLineVector = objBaseLineVectors.Item(i)
                    Dim tStrTime As Date
                    Dim tEndTime As Date
                    tStrTime = clsBaseLineVector.StrTimeJST
                    tEndTime = clsBaseLineVector.EndTimeJST
                    If DateDiff("s", tStrTime, tTimeStr) > 0 Then tTimeStr = tStrTime
                    If DateDiff("s", tTimeEnd, tEndTime) > 0 Then tTimeEnd = tEndTime
                Next
            End If
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '指定された基線ベクトル群の解析期間の取得。
        '
        'objBaseLineVectors で指定される基線ベクトルの中で、最小の解析開始日時と最大の解析終了日時を取得する。
        '
        '引き数：
        'objBaseLineVectors 基線ベクトル。要素は BaseLineVector オブジェクト。キーは任意。
        'tTimeStr 解析開始日時(JST)が設定される。
        'tTimeEnd 解析終了日時(JST)が設定される。
        */
        public static void GetAnalisysSpan(Collection objBaseLineVectors, ref DateTime tTimeStr, ref DateTime tTimeEnd)
        {
#if false
            if (objBaseLineVectors.Count <= 0)
            {
                tTimeStr = MIN_TIME;
                tTimeEnd = MIN_TIME;
            }
            else
            {
                BaseLineVector clsBaseLineVector;
                long i;
                clsBaseLineVector = objBaseLineVectors.Item(1);
                tTimeStr = clsBaseLineVector.StrTimeJST;
                tTimeEnd = clsBaseLineVector.EndTimeJST;
                for (i = 2; i < objBaseLineVectors.Count; i++)
                {
                    clsBaseLineVector = objBaseLineVectors.Item(i);
                    DateTime tStrTime;
                    DateTime tEndTime;
                    tStrTime = clsBaseLineVector.StrTimeJST;
                    tEndTime = clsBaseLineVector.EndTimeJST;
                    if (DateDiff("s", tStrTime, tTimeStr) > 0)
                    {
                        tTimeStr = tStrTime;
                    }
                    if (DateDiff("s", tTimeEnd, tEndTime) > 0)
                    {
                        tTimeEnd = tEndTime;
                    }
                }
            }
#endif
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '観測点キーを取得する。
        '
        '観測点を一意に識別するキーを計算して返す。
        '
        '引き数：
        'sNumber 観測点番号
        'sSession セッション名。
        '
        '戻り値：観測点キーを返す。
        Public Function GetObservationPointKey(ByVal sNumber As String, ByVal sSession As String) As String
            GetObservationPointKey = sSession & sNumber
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '観測点キーを取得する。
        '
        '観測点を一意に識別するキーを計算して返す。
        '
        '引き数：
        'sNumber 観測点番号
        'sSession セッション名。
        '
        '戻り値：観測点キーを返す。
        */
        public static string GetObservationPointKey(string sNumber, string sSession)
        {
            return sSession + sNumber;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '基線ベクトルキーを取得する。
        '
        '基線ベクトルを一意に識別するキーを計算して返す。
        '
        '引き数：
        'sStrNumber 始点観測点番号。
        'sEndNumber 終点観測点番号。
        'sSession セッション名。
        '
        '戻り値：基線ベクトルキーを返す。
        Public Function GetBaseLineVectorKey(ByVal sStrNumber As String, ByVal sEndNumber As String, ByVal sSession As String) As String
            If StrComp(sStrNumber, sEndNumber) < 0 Then
                GetBaseLineVectorKey = sStrNumber & "\" & sEndNumber & "\" & sSession
            Else
                GetBaseLineVectorKey = sEndNumber & "\" & sStrNumber & "\" & sSession
            End If
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '基線ベクトルキーを取得する。
        '
        '基線ベクトルを一意に識別するキーを計算して返す。
        '
        '引き数：
        'sStrNumber 始点観測点番号。
        'sEndNumber 終点観測点番号。
        'sSession セッション名。
        '
        '戻り値：基線ベクトルキーを返す。
        */
        public static string GetBaseLineVectorKey(string sStrNumber, string sEndNumber, string sSession)
        {
            if (Strings.StrComp(sStrNumber, sEndNumber) < 0)
            {
                return sStrNumber + "\\" + sEndNumber + "\\" + sSession;
            }
            else
            {
                return sEndNumber + "\\" + sStrNumber + "\\" + sSession;
            }
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'NS-Network のバージョンを検査する。
        '
        'ただし、ActiveX.EXE 以降のバージョンを検査する。
        'それ以前のバージョンであった場合は False を返す。
        '
        '引き数：
        'nMajor メジャー。
        'nMinor マイナー。
        'nBuild ビルド。
        'nRevision リビジョン。
        '
        '戻り値：
        'NS-Network のバージョンが指定された値以上なら True を返す。
        'それ以外の場合 False を返す。
        Public Function CheckVersionNSN(Optional ByVal nMajor As Long = 0, Optional ByVal nMinor As Long = 0, Optional ByVal nBuild As Long = 0, Optional ByVal nRevision As Long = 0) As Boolean
            CheckVersionNSN = False
            On Error GoTo ErrorHandler
            Dim clsOperator As New NSN.Operator
            If clsOperator.Major < nMajor Then Exit Function
            If clsOperator.Major = nMajor Then
                If clsOperator.Minor < nMinor Then Exit Function
                If clsOperator.Minor = nMinor Then
                    If clsOperator.Build < nBuild Then Exit Function
                    If clsOperator.Build = nBuild Then
                        If clsOperator.Revision < nRevision Then Exit Function
                    End If
                End If
            End If
            CheckVersionNSN = True
            Exit Function
        ErrorHandler:
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        'NS-Network のバージョンを検査する。
        '
        'ただし、ActiveX.EXE 以降のバージョンを検査する。
        'それ以前のバージョンであった場合は False を返す。
        '
        '引き数：
        'nMajor メジャー。
        'nMinor マイナー。
        'nBuild ビルド。
        'nRevision リビジョン。
        '
        '戻り値：
        'NS-Network のバージョンが指定された値以上なら True を返す。
        'それ以外の場合 False を返す。
        */
        public static bool CheckVersionNSN(long nMajor = 0, long nMinor = 0, long nBuild = 0, long nRevision = 0)
        {
            try
            {
#if false
                NSN.Operator clsOperator = new NSN.Operator();
                if (clsOperator.Major < nMajor)
                {
                    return false;
                }
                if (clsOperator.Major == nMajor)
                {
                    if (clsOperator.Minor < nMinor)
                    {
                        return false;
                    }
                    if (clsOperator.Minor == nMinor)
                    {
                        if (clsOperator.Build < nBuild)
                        {
                            return false;
                        }
                        if (clsOperator.Build == nBuild)
                        {
                            if (clsOperator.Revision < nRevision)
                            {
                                return false;
                            }
                        }
                    }
                }
#endif
                return true;
            }

            catch (Exception)
            {
                return false;
            }
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'PPS衛星情報ファイルのパスを取得する。
        '
        '戻り値：
        'PPS衛星情報ファイルのパスを返す。
        Public Function GetSatInfoFilePath() As String
            Dim sPathDef As String
            Dim sTimeDef As String
            Dim sPathSet As String
            Dim sTimeSet As String
            sPathDef = App.Path & "\" & GetPrivateProfileString(PROFILE_SAVE_SEC_ANALYSIS, PROFILE_SAVE_KEY_SATINFOFILE, "", App.Path & "\" & PROFILE_DEF_NAME)
            sTimeDef = GetPrivateProfileString(PROFILE_SAVE_SEC_ANALYSIS, PROFILE_SAVE_KEY_SATINFOTIME, "2020/01/01 00:00:00", App.Path & "\" & PROFILE_DEF_NAME)
            sPathSet = GetPrivateProfileString(PROFILE_SAVE_SEC_ANALYSIS, PROFILE_SAVE_KEY_SATINFOFILE, sPathDef, App.Path & "\" & App.Title & PROFILE_SAVE_EXT)
            sTimeSet = GetPrivateProfileString(PROFILE_SAVE_SEC_ANALYSIS, PROFILE_SAVE_KEY_SATINFOTIME, sTimeDef, App.Path & "\" & App.Title & PROFILE_SAVE_EXT)
            Dim tTimeDef As Date
            Dim tTimeSet As Date
            tTimeDef = CDate(sTimeDef)
            tTimeSet = CDate(sTimeSet)
            Dim nDiff As Long
            nDiff = DateDiff("s", tTimeDef, tTimeSet)
            If nDiff < 0 Then
                GetSatInfoFilePath = sPathDef
            Else
                GetSatInfoFilePath = sPathSet
            End If
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        'PPS衛星情報ファイルのパスを取得する。
        '
        '戻り値：
        'PPS衛星情報ファイルのパスを返す。
        */
        public static string GetSatInfoFilePath()
        {
            string sPathDe = "";
            string sTimeDef = "";
            string sPathSet = "";
            string sTimeSet = "";
#if false
            sPathDef = AppPath + "\\" + GetPrivateProfileString(PROFILE_SAVE_SEC_ANALYSIS, PROFILE_SAVE_KEY_SATINFOFILE, "", AppPath + "\\" + PROFILE_DEF_NAME);
            sTimeDef = GetPrivateProfileString(PROFILE_SAVE_SEC_ANALYSIS, PROFILE_SAVE_KEY_SATINFOTIME, "2020/01/01 00:00:00", AppPath + "\\" + PROFILE_DEF_NAME);
            sPathSet = GetPrivateProfileString(PROFILE_SAVE_SEC_ANALYSIS, PROFILE_SAVE_KEY_SATINFOFILE, sPathDef, AppPath + "\\" + AppTitle + PROFILE_SAVE_EXT);
            sTimeSet = GetPrivateProfileString(PROFILE_SAVE_SEC_ANALYSIS, PROFILE_SAVE_KEY_SATINFOTIME, sTimeDef, AppPath + "\\" + AppTitle + PROFILE_SAVE_EXT);
            DateTime tTimeDef;
            DateTime tTimeSet;
            tTimeDef = CDate(sTimeDef);
            tTimeSet = CDate(sTimeSet);
            long nDiff;
            nDiff = DateDiff("s", tTimeDef, tTimeSet);
            if (nDiff < 0)
            {
                return sPathDef;
            }
            else
            {
                return sPathSet;
            }
#else
            return "";
#endif
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '表示用のバイアス決定比を取得する。
        '
        '戻り値：
        '表示用のバイアス決定比を返す。
        Public Function GetDispBias(ByVal nBias As Double, ByVal nDecimal As Long) As Double
            Dim nDispBias As Double
            Dim nDispDecimal As Double
            Dim i As Long
            nDispBias = 99
            nDispDecimal = 9
            For i = 1 To nDecimal
                nDispDecimal = nDispDecimal / 10
                nDispBias = nDispBias + nDispDecimal
            Next
            If nBias > nDispBias Then
                GetDispBias = nDispBias
            Else
                GetDispBias = nBias
            End If
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '表示用のバイアス決定比を取得する。
        '
        '戻り値：
        '表示用のバイアス決定比を返す。
        */
        public static double GetDispBias(double nBias, long nDecimal)
        {
            double nDispBias;
            double nDispDecimal;
            long i;
            nDispBias = 99;
            nDispDecimal = 9;
            for (i = 1; i < nDecimal; i++)
            {
                nDispDecimal /= 10;
                nDispBias += nDispDecimal;
            }
            if (nBias > nDispBias)
            {
                return nDispBias;
            }
            else
            {
                return nBias;
            }
        }
        //==========================================================================================
    }
}
