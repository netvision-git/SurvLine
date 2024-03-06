using SurvLine.mdl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;
using static SurvLine.mdl.DEFINE;
using Microsoft.VisualBasic;

namespace SurvLine
{
    public class FileObject
    {
        //==========================================================================================
        /*[VB]
        '*******************************************************************************
        'ファイルオブジェクト。
        '
        'CreateFile 関連。

        Option Explicit
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'インプリメンテーション
        Private m_hFile As Long 'ファイルのハンドル。
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'インプリメンテーション
        private long m_hFile;       //'ファイルのハンドル。
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '*******************************************************************************
        'プロパティ

        'ファイルのハンドル。
        Property Get Handle() As Long
            Handle = m_hFile
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '*******************************************************************************
        'プロパティ

        'ファイルのハンドル。
        */
        public long Handle()
        {
            return m_hFile;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '*******************************************************************************
        'イベント

        '初期化。
        Private Sub Class_Initialize()
            m_hFile = INVALID_HANDLE_VALUE
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
            m_hFile = INVALID_HANDLE_VALUE;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'ファイルを閉じる。
        Private Sub Class_Terminate()
            Call CloseHandle
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'ファイルを閉じる。
        private void Class_Terminate()
        {
            CloseHandle();
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '*******************************************************************************
        'メソッド

        'CreateFile
        '
        '引き数：
        'lpFileName
        'dwDesiredAccess
        'dwShareMode
        'lpSecurityAttributes
        'dwCreationDisposition
        'dwFlagsAndAttributes
        'hTemplateFile
        '
        '戻り値：
        '正常終了の場合 True を返す。
        'それ以外の場合 False を返す。
        Public Function CreateFile(ByVal lpFileName As String, ByVal dwDesiredAccess As Long, ByVal dwShareMode As Long, ByVal lpSecurityAttributes As Long, ByVal dwCreationDisposition As Long, ByVal dwFlagsAndAttributes As Long, ByVal hTemplateFile As Long) As Boolean
            CreateFile = False
            If m_hFile <> INVALID_HANDLE_VALUE Then Exit Function
            m_hFile = mdlDefine.CreateFile(lpFileName, dwDesiredAccess, dwShareMode, lpSecurityAttributes, dwCreationDisposition, dwFlagsAndAttributes, hTemplateFile)
            If m_hFile = INVALID_HANDLE_VALUE Then Exit Function
            CreateFile = True
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '*******************************************************************************
        'メソッド

        'CreateFile
        '
        '引き数：
        'lpFileName
        'dwDesiredAccess
        'dwShareMode
        'lpSecurityAttributes
        'dwCreationDisposition
        'dwFlagsAndAttributes
        'hTemplateFile
        '
        '戻り値：
        '正常終了の場合 True を返す。
        'それ以外の場合 False を返す。
        */
        public bool CreateFile(string lpFileName, long dwDesiredAccess, long dwShareMode, long lpSecurityAttributes, long dwCreationDisposition, long dwFlagsAndAttributes, long hTemplateFile)
        {
            if (m_hFile != INVALID_HANDLE_VALUE)
            {
                return false;
            }
            long rtn = DF_CreateFile(lpFileName, dwDesiredAccess, dwShareMode, lpSecurityAttributes, dwCreationDisposition, dwFlagsAndAttributes, hTemplateFile);
            if (rtn < 0)
            {
                m_hFile = -1;
                return false;
            }
            else
            {
                m_hFile = 0;
            }
            return true;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'CloseHandle
        Public Sub CloseHandle()
            If m_hFile <> INVALID_HANDLE_VALUE Then
                Call mdlDefine.CloseHandle(m_hFile)
                m_hFile = INVALID_HANDLE_VALUE
            End If
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'CloseHandle
        public void CloseHandle()
        {
            if (m_hFile != INVALID_HANDLE_VALUE)
            {
                _ = DF_CloseHandle(m_hFile);
                m_hFile = INVALID_HANDLE_VALUE;
            }
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'SetFileTime
        '
        '引き数：
        'tCreationTime
        'tLastAccessTime
        'tLastWriteTime
        '
        '戻り値：
        '正常終了の場合 True を返す。
        'それ以外の場合 False を返す。
        Public Function SetFileTime(ByVal tCreationTime As Date, ByVal tLastAccessTime As Date, ByVal tLastWriteTime As Date) As Boolean

            SetFileTime = False
    
            Dim tLocalFileTime As FILETIME
            Dim tSystemTime As SYSTEMTIME
            Dim lpCreationTime As FILETIME
            Dim lpLastAccessTime As FILETIME
            Dim lpLastWriteTime As FILETIME
    
            tSystemTime.wYear = Year(tCreationTime)
            tSystemTime.wMonth = Month(tCreationTime)
            tSystemTime.wDay = Day(tCreationTime)
            tSystemTime.wHour = Hour(tCreationTime)
            tSystemTime.wMinute = Minute(tCreationTime)
            tSystemTime.wSecond = Second(tCreationTime)
            tSystemTime.wMilliseconds = 0
            Call SystemTimeToFileTime(tSystemTime, tLocalFileTime)
            Call LocalFileTimeToFileTime(tLocalFileTime, lpCreationTime)
    
            tSystemTime.wYear = Year(tLastAccessTime)
            tSystemTime.wMonth = Month(tLastAccessTime)
            tSystemTime.wDay = Day(tLastAccessTime)
            tSystemTime.wHour = Hour(tLastAccessTime)
            tSystemTime.wMinute = Minute(tLastAccessTime)
            tSystemTime.wSecond = Second(tLastAccessTime)
            tSystemTime.wMilliseconds = 0
            Call SystemTimeToFileTime(tSystemTime, tLocalFileTime)
            Call LocalFileTimeToFileTime(tLocalFileTime, lpLastAccessTime)
    
            tSystemTime.wYear = Year(tLastWriteTime)
            tSystemTime.wMonth = Month(tLastWriteTime)
            tSystemTime.wDay = Day(tLastWriteTime)
            tSystemTime.wHour = Hour(tLastWriteTime)
            tSystemTime.wMinute = Minute(tLastWriteTime)
            tSystemTime.wSecond = Second(tLastWriteTime)
            tSystemTime.wMilliseconds = 0
            Call SystemTimeToFileTime(tSystemTime, tLocalFileTime)
            Call LocalFileTimeToFileTime(tLocalFileTime, lpLastWriteTime)
    
            If mdlDefine.SetFileTime(m_hFile, lpCreationTime, lpLastAccessTime, lpLastWriteTime) = 0 Then Exit Function
    
            SetFileTime = True
    
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        'SetFileTime
        '
        '引き数：
        'tCreationTime
        'tLastAccessTime
        'tLastWriteTime
        '
        '戻り値：
        '正常終了の場合 True を返す。
        'それ以外の場合 False を返す。
        */
        public bool SetFileTime(DateTime tCreationTime, DateTime tLastAccessTime, DateTime tLastWriteTime)
        {

            FILETIME tLocalFileTime = default;
            SYSTEMTIME tSystemTime = default;
            FILETIME lpCreationTime = default;
            FILETIME lpLastAccessTime = default;
            FILETIME lpLastWriteTime = default;

            tSystemTime.wYear = tCreationTime.Year;
            tSystemTime.wMonth = tCreationTime.Month;
            tSystemTime.wDay = tCreationTime.Day;
            tSystemTime.wHour = tCreationTime.Hour;
            tSystemTime.wMinute = tCreationTime.Minute;
            tSystemTime.wSecond = tCreationTime.Second;
            tSystemTime.wMilliseconds = 0;
            _ = SystemTimeToFileTime(ref tSystemTime, ref tLocalFileTime);
            _ = LocalFileTimeToFileTime(ref tLocalFileTime, ref lpCreationTime);

            tSystemTime.wYear = tLastAccessTime.Year;
            tSystemTime.wMonth = tLastAccessTime.Month;
            tSystemTime.wDay = tLastAccessTime.Day;
            tSystemTime.wHour = tLastAccessTime.Hour;
            tSystemTime.wMinute = tLastAccessTime.Minute;
            tSystemTime.wSecond = tLastAccessTime.Second;
            tSystemTime.wMilliseconds = 0;
            _ = SystemTimeToFileTime(ref tSystemTime, ref tLocalFileTime);
            _ = LocalFileTimeToFileTime(ref tLocalFileTime, ref lpLastAccessTime);

            tSystemTime.wYear = tLastWriteTime.Year;
            tSystemTime.wMonth = tLastWriteTime.Month;
            tSystemTime.wDay = tLastWriteTime.Day;
            tSystemTime.wHour = tLastWriteTime.Hour;
            tSystemTime.wMinute = tLastWriteTime.Minute;
            tSystemTime.wSecond = tLastWriteTime.Second;
            tSystemTime.wMilliseconds = 0;
            _ = SystemTimeToFileTime(ref tSystemTime, ref tLocalFileTime);
            _ = LocalFileTimeToFileTime(ref tLocalFileTime, ref lpLastWriteTime);

            if (DF_SetFileTime(m_hFile, ref lpCreationTime, ref lpLastAccessTime, ref lpLastWriteTime) == 0)
            {
                return false;
            }

            return true;

        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'GetFileTime
        '
        '引き数：
        'tCreationTime
        'tLastAccessTime
        'tLastWriteTime
        '
        '戻り値：
        '正常終了の場合 True を返す。
        'それ以外の場合 False を返す。
        Public Function GetFileTime(ByRef tCreationTime As Date, ByRef tLastAccessTime As Date, ByRef tLastWriteTime As Date) As Boolean

            GetFileTime = False
    
            Dim lpCreationTime As FILETIME
            Dim lpLastAccessTime As FILETIME
            Dim lpLastWriteTime As FILETIME
            If mdlDefine.GetFileTime(m_hFile, lpCreationTime, lpLastAccessTime, lpLastWriteTime) = 0 Then Exit Function
    
            Dim tLocalFileTime As FILETIME
            Dim tSystemTime As SYSTEMTIME
            Call FileTimeToLocalFileTime(lpCreationTime, tLocalFileTime)
            Call FileTimeToSystemTime(tLocalFileTime, tSystemTime)
            tCreationTime = DateSerial(tSystemTime.wYear, tSystemTime.wMonth, tSystemTime.wDay) + TimeSerial(tSystemTime.wHour, tSystemTime.wMinute, tSystemTime.wSecond)
            Call FileTimeToLocalFileTime(lpLastAccessTime, tLocalFileTime)
            Call FileTimeToSystemTime(tLocalFileTime, tSystemTime)
            tLastAccessTime = DateSerial(tSystemTime.wYear, tSystemTime.wMonth, tSystemTime.wDay) + TimeSerial(tSystemTime.wHour, tSystemTime.wMinute, tSystemTime.wSecond)
            Call FileTimeToLocalFileTime(lpLastWriteTime, tLocalFileTime)
            Call FileTimeToSystemTime(tLocalFileTime, tSystemTime)
            tLastWriteTime = DateSerial(tSystemTime.wYear, tSystemTime.wMonth, tSystemTime.wDay) + TimeSerial(tSystemTime.wHour, tSystemTime.wMinute, tSystemTime.wSecond)
    
            GetFileTime = True
    
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        'GetFileTime
        '
        '引き数：
        'tCreationTime
        'tLastAccessTime
        'tLastWriteTime
        '
        '戻り値：
        '正常終了の場合 True を返す。
        'それ以外の場合 False を返す。
        */
        public bool GetFileTime(ref DateTime tCreationTime, ref DateTime tLastAccessTime, ref DateTime tLastWriteTime)
        {

            FILETIME lpCreationTime = default;
            FILETIME lpLastAccessTime = default;
            FILETIME lpLastWriteTime = default;
            if (DF_GetFileTime(m_hFile, ref lpCreationTime, ref lpLastAccessTime, ref lpLastWriteTime) == 0)
            {
                return false;
            }

            FILETIME tLocalFileTime = default;
            SYSTEMTIME tSystemTime = default;
            _ = FileTimeToLocalFileTime(ref lpCreationTime, ref tLocalFileTime);
            _ = FileTimeToSystemTime(ref tLocalFileTime, ref tSystemTime);
#if false
            tCreationTime = DateAndTime.DateSerial(tSystemTime.wYear, tSystemTime.wMonth, tSystemTime.wDay) + DateAndTime.TimeSerial(tSystemTime.wHour, tSystemTime.wMinute, tSystemTime.wSecond);
#endif
            _ = FileTimeToLocalFileTime(ref lpLastAccessTime, ref tLocalFileTime);
            _ = FileTimeToSystemTime(ref tLocalFileTime, ref tSystemTime);
#if false
            tLastAccessTime = DateAndTime.DateSerial(tSystemTime.wYear, tSystemTime.wMonth, tSystemTime.wDay) + DateAndTime.TimeSerial(tSystemTime.wHour, tSystemTime.wMinute, tSystemTime.wSecond);
#endif
            _ = FileTimeToLocalFileTime(ref lpLastWriteTime, ref tLocalFileTime);
            _ = FileTimeToSystemTime(ref tLocalFileTime, ref tSystemTime);
#if false
            tLastWriteTime = DateAndTime.DateSerial(tSystemTime.wYear, tSystemTime.wMonth, tSystemTime.wDay) + DateAndTime.TimeSerial(tSystemTime.wHour, tSystemTime.wMinute, tSystemTime.wSecond);
#endif


            return true;

        }
        //==========================================================================================
    }
}
