using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace SurvLine
{
    public class ObservationPointKey_NSO
    {
        //==========================================================================================
        /*[VB]
        '*******************************************************************************
        '観測点キー
        '
        '観測点を一意に識別できるキーを操作する。

        Option Explicit
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'インプリメンテーション
        Private m_nBaseValue As Date '基準値。
        Private m_nLoadTime As Date 'ロード時間。
        Private m_nLastValue As Date '最終値。
        Private m_nSubValue As Long 'サブ値。
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'インプリメンテーション
        public static TimeSpan m_nBaseValue;      //'基準値。
        public static DateTime m_nLoadTime;       //'ロード時間。
        public static TimeSpan m_nLastValue;      //'最終値。
        public static long m_nSubValue;           //'サブ値。
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '*******************************************************************************
        'イベント

        '初期化。
        Private Sub Class_Initialize()

            m_nLoadTime = Now
    
        'ビルドエラーが発生する。。。。。。。。。。。。。。
        '    On Error GoTo ErrorHandler
        '
        '    m_nLoadTime = Now
        '
        '    Exit Sub
        '
        'ErrorHandler:
        '    Call mdlMain.ErrorExit
    
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '*******************************************************************************
        'イベント

        '初期化。
        */
        public ObservationPointKey_NSO()
        {

            m_nLoadTime = DateTime.Now;
            m_nBaseValue = default;

#if false
// debug
            string dt1 = CreateKey_NSO();
            string dt2 = CreateKey_NSO();
            string dt3 = CreateKey_NSO();
#endif
            /*
            'ビルドエラーが発生する。。。。。。。。。。。。。。
            '    On Error GoTo ErrorHandler
            '
            '    m_nLoadTime = Now
            '
            '    Exit Sub
            '
            'ErrorHandler:
            '    Call mdlMain.ErrorExit
            */
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '*******************************************************************************
        'メソッド

        '保存。
        '
        '引き数：
        'nFile ファイル番号。
        Public Sub Save(ByVal nFile As Integer)
            Put #nFile, , m_nLastValue
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '読み込み。
        '
        '引き数：
        'nFile ファイル番号。
        'nVersion ファイルバージョン。
        Public Sub Load(ByVal nFile As Integer, ByVal nVersion As Long)
            Get #nFile, , m_nBaseValue
            m_nBaseValue = m_nBaseValue + 1 / 24 / 60 / 60
            m_nLoadTime = Now
            m_nLastValue = m_nBaseValue
            m_nSubValue = 0
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'キーの生成。
        '
        '戻り値：生成した観測点キーを返す。
        Public Function CreateKey() As String
            Dim tTime As Date
            tTime = Now
            Dim nValue As Date
            nValue = DateAdd("s", DateDiff("s", m_nLoadTime, Now), m_nBaseValue)
            If DateDiff("s", nValue, m_nLastValue) < 0 Then
                m_nLastValue = nValue
                m_nSubValue = 0
            Else
                m_nSubValue = m_nSubValue + 1
            End If
            CreateKey = Format$(m_nLastValue, "yyyymmddhhnnss") & Hex$(m_nSubValue)
            Debug.Print CreateKey
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        'キーの生成。
        '
        '戻り値：生成した観測点キーを返す。
        */
        public static string CreateKey_NSO()
        {
            string w_CreateKey;
            string w_nLastValue;
            DateTime tTime;
            tTime = DateTime.Now;
            TimeSpan nValue;
            nValue = tTime - m_nLoadTime;
            if ((m_nLastValue.TotalSeconds - nValue.TotalSeconds) < 0)
            {
                m_nLastValue = nValue;
                m_nSubValue = 0;
            }
            else
            {
                m_nSubValue = m_nSubValue + 1;
            }
            w_CreateKey = m_nLastValue.ToString();
            w_nLastValue = m_nSubValue.ToString("X");
            return w_CreateKey + w_nLastValue;
        }
        //==========================================================================================
    }
}
