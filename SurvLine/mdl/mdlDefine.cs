using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using static System.Collections.Specialized.BitVector32;
using System.Windows.Forms;
using Microsoft.VisualBasic.FileIO;
using System.Runtime.Remoting;
using System.Xml.Linq;
using System.Security.Cryptography;

namespace SurvLine.mdl
{
    public class MdiDefine
    {
    }
    public class DEFINE
    {

        public const long vbObjectError = 3;
        public const long vbAbort = 2;              //ボタン
        public const long vbOK = 1;                 //ボタン(OK:1 / キャンセル:0)
        public const long vbCancel = 0;             //ボタン(OK:1 / キャンセル:0)

        public const long cdlCancel = 32755;        //（エラー番号：32755 (cdlCancel)）

        public const long vbBSNone = 0;              //GroupBox

        public const string vbCrLf = @"\r";           //キャレッジリターン

        //***************************************************
        //  StrConv 関数
        //  StrConv ( string, conversion [, LCID ] )
        //***************************************************
        // 引数 conversion引数 の設定値
        public const int vbUpperCase = 1;       //文字列を大文字に変換します。
        public const int vbLowerCase = 2;       //文字列を小文字に変換します。
        public const int vbProperCase = 3;      //文字列内の各単語の先頭の文字を大文字に変換します。
        public const int vbWide = 4;            //文字列内の半角文字(1 バイト) を全角文字(2 バイト) に変換します。
        public const int vbNarrow = 8;          //文字列内の全角文字(2 バイト) を半角文字(1 バイト) に変換します。
        public const int vbKatakana = 16;       //文字列内のひらがなをカタカナに変換します。
        public const int vbHiragana = 32;       //文字列内のカタカナをひらがなに変換します。
        public const int vbUnicode = 64;        //システムの既定のコード ページを使用して、文字列を Unicode に変換します。 (Macintosh では使用できません。
        public const int vbFromUnicode = 128;   //文字列を Unicode からシステムの既定のコード ページに変換します。 (Macintosh では使用できません。


        //***************************************************
        //'API定数。
        //***************************************************
        public const long MAX_PATH = 260;
        public const long SW_SHOWNORMAL = 1;
        public const long SM_CXVSCROLL = 2;
        public const long SM_CYHSCROLL = 3;
        public const long SM_CXEDGE = 45;
        public const long SM_CYEDGE = 46;
        public const long SM_CXSMICON = 0x31;
        public const long SM_CYSMICON = 0x32;
        public const long SM_CXDRAG = 0x44;
        public const long SM_CYDRAG = 0x45;
        public const long INVALID_HANDLE_VALUE = -1;
        public const double FLT_EPSILON = 1.192092896E-07;
        public const double FLT_MAX = 3.40282346638529E+38;
        public const long LONG_MAX = 2147483647;
        public const long SHRT_MAX = 32767;
        public const long SRCCOPY = 0xCC0020;
        public const long HORZRES = 0x8;
        public const long VERTRES = 0xA;
        public const long LOGPIXELSX = 0x58;
        public const long LOGPIXELSY = 0x5A;
        public const long PHYSICALWIDTH = 0x6E;
        public const long PHYSICALHEIGHT = 0x6F;
        public const long PHYSICALOFFSETX = 0x70;
        public const long PHYSICALOFFSETY = 0x71;
        public const long COLOR_3DFACE = 0xF;
        public const long COLOR_WINDOW = 0x5;
        public const long DT_CENTER = 0x1;
        public const long DT_RIGHT = 0x2;
        public const long DT_VCENTER = 0x4;
        public const long DT_BOTTOM = 0x8;
        public const long DT_SINGLELINE = 0x20;
        public const long DT_NOCLIP = 0x100;
        public const long DT_CALCRECT = 0x400;
        public const long DT_NOPREFIX = 0x800;
        public const long PD_HIDEPRINTTOFILE = 0x100000;
        public const long PD_NONETWORKBUTTON = 0x200000;
        public const long PD_NOSELECTION = 0x4;
        public const long PD_NOPAGENUMS = 0x8;
        public const long PD_RETURNDC = 0x100;
        public const long PD_USEDEVMODECOPIESANDCOLLATE = 0x40000;
        public const long MM_TEXT = 0x1;
        public const long MM_TWIPS = 0x6;
        public const long MM_ISOTROPIC = 0x7;
        public const long R2_COPYPEN = 0xD;
        public const long PS_SOLID = 0x0;
        public const long BS_SOLID = 0x0;
        public const long BS_NULL = 0x1;
        public const long PS_NULL = 0x5;
        public const long OBJ_PEN = 0x1;
        public const long OBJ_BRUSH = 0x2;
        public const long OBJ_FONT = 0x6;
        public const long FW_BOLD = 700;
        public const long NULL_BRUSH = 0x5;
        public const long NULL_PEN = 0x8;
        public const long DEFAULT_CHARSET = 0x1;
        public const long TRANSPARENT = 0x1;
        public const long FLOODFILLSURFACE = 0x1;
        public const long GWL_STYLE = 0xFFFFFFF0;
        public const long GWL_WNDPROC = 0xFFFFFFFC;
        public const long GWL_HINSTANCE = 0xFFFFFFFA;
        public const long GMEM_MOVEABLE = 0x2;
        public const long GMEM_ZEROINIT = 0x40;
        public const long WS_VISIBLE = 0x10000000;
        public const long WH_CALLWNDPROC = 0x4;
        public const long WH_KEYBOARD = 0x2;
        public const long WM_KEYDOWN = 0x100;
        public const long WM_GETFONT = 0x31;
        public const long WM_SIZING = 0x214;
        public const long WM_MOUSELEAVE = 0x2A3;
        public const long HC_ACTION = 0x0;
        public const long KEY_ALL_ACCESS = 0xF003F;
        public const long REG_SZ = 0x1;
        public const long REG_DWORD = 0x4;
        public const long HKEY_CLASSES_ROOT = 0x80000000;
        public const long HKEY_CURRENT_USER = 0x80000001;
        public const long HKEY_LOCAL_MACHINE = 0x80000002;
        public const long RDW_INVALIDATE = 0x1;
        public const long RDW_ERASE = 0x4;
        public const long RDW_UPDATENOW = 0x100;
        public const long LVM_GETCOUNTPERPAGE = 0x1028;

        public const long FILE_ATTRIBUTE_READONLY = 0x01;   //ReadOnly	1 	読み取り専用
        public const long FILE_ATTRIBUTE_HIDDEN = 0x02;     //Hidden	2	隠しファイル
        public const long FILE_ATTRIBUTE_SYSTEM = 0x04;     //System	4	システムファイル
        public const long FILE_ATTRIBUTE_DIRECTORY = 0x10;  //Directory	16	ディレクトリまたはフォルダ。読み取りのみ可能。
                                                            //Archive	32	ファイルが前回のバックアップ以降に変更されているか。
        public const long FILE_ATTRIBUTE_TEMPORARY = 0x100;
        //***************************************************
        public const long GENERIC_WRITE = 0x40000000;
        public const long GENERIC_READ = 0x80000000;
        public const long FILE_SHARE_READ = 0x1;
        public const long OPEN_EXISTING = 0x3;
        //***************************************************
        public const long FILE_ATTRIBUTE_NORMAL = 0x00;     //Normal	0	標準のファイル。他の属性を持たない。
        //***************************************************
        public const long BFFM_INITIALIZED = 1;
        public const long BFFM_SETSELECTIONA = 1126;
        public const long BIF_DEFAULT = 0;
        public const long BIF_NEWDIALOGSTYLE = 0x40;  //'2008/10/11 NGS Yamada
        public const long LF_FACESIZE = 0x20;
        public const long IDI_EXCLAMATION = 32515;
        public const long VK_LBUTTON = 0x1;
        public const long VK_RBUTTON = 0x2;
        public const long VK_MBUTTON = 0x4;
        public const long VK_SHIFT = 0x10;
        public const long VK_CONTROL = 0x11;
        public const long VK_BACK = 0x8;
        public const long FORMAT_MESSAGE_FROM_SYSTEM = 0x1000;
        public const long WAIT_OBJECT_0 = 0x0;
        public const long TME_LEAVE = 0x2;
        public const long TME_CANCEL = 0x80000000;
        public const long ERROR_SUCCESS = 0;
        public const long ERROR_FILE_NOT_FOUND = 0x2;
        public const long CSIDL_PROGRAM_FILES = 0x26;
        public const long S_OK = 0x0;


        public enum TernaryRasterOperations : uint
        {
            SRCCOPY = 0x00CC0020,
            SRCPAINT = 0x00EE0086,
            SRCAND = 0x008800C6,
            SRCINVERT = 0x00660046,
            SRCERASE = 0x00440328,
            NOTSRCCOPY = 0x00330008,
            NOTSRCERASE = 0x001100A6,
            MERGECOPY = 0x00C000CA,
            MERGEPAINT = 0x00BB0226,
            PATCOPY = 0x00F00021,
            PATPAINT = 0x00FB0A09,
            PATINVERT = 0x005A0049,
            DSTINVERT = 0x00550009,
            BLACKNESS = 0x00000042,
            WHITENESS = 0x00FF0062,
            CAPTUREBLT = 0x40000000
        }

        //'API構造体。
        public struct POINT
        {
            public long X;
            public long Y;
        }

        public struct SIZE
        {
            long CX;
            long CY;
        }

        public struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        public struct FILETIME
        {
            public long dwLowDateTime;
            public long dwHighDateTime;
        }

        public struct WIN32_FIND_DATA
        {
            public long dwFileAttributes;
            public FILETIME ftCreationTime;
            public FILETIME ftLastAccessTime;
            public FILETIME ftLastWriteTime;
            public long nFileSizeHigh;
            public long nFileSizeLow;
            public long dwReserved0;
            public long dwReserved1;
            public byte[] cFileName;                           //cFileName(MAX_PATH - 1) As Byte
            public byte[] cAlternateFileName;                  //cAlternateFileName(14 - 1) As Byte
        }

        public struct SYSTEMTIME
        {
            public int wYear;
            public int wMonth;
            public int wDayOfWeek;
            public int wDay;
            public int wHour;
            public int wMinute;
            public int wSecond;
            public int wMilliseconds;
        }

        public struct BROWSEINFO
        {
            public long hwndOwner;
            public long pidlRoot;
            public string pszDisplayName;
            public string lpszTitle;
            public long ulFlags;
            public long lpfn;
            public long lParam;
            public long iImage;
        }

        public struct LOGFONT
        {
            public long lfHeight;
            public long lfWidth;
            public long lfEscapement;
            public long lfOrientation;
            public long lfWeight;
            public byte lfItalic;
            public byte lfUnderline;
            public byte lfStrikeOut;
            public byte lfCharSet;
            public byte lfOutPrecision;
            public byte lfClipPrecision;
            public byte lfQuality;
            public byte lfPitchAndFamily;
            public string lfFaceName;                          //String * LF_FACESIZE
        }

        public struct LOGBRUSH
        {
            public long lbStyle;
            public long lbColor;
            public long lbHatch;
        }

        public struct LOGPEN
        {
            public long lopnStyle;
            public POINT lopnWidth;
            public long lopnColor;
        }

        public struct VS_FIXEDFILEINFO
        {
            public long dwSignature;
            public long dwStrucVersion;
            public long dwFileVersionMS;
            public long dwFileVersionLS;
            public long dwProductVersionMS;
            public long dwProductVersionLS;
            public long dwFileFlagsMask;
            public long dwFileFlags;
            public long dwFileOS;
            public long dwFileType;
            public long dwFileSubtype;
            public long dwFileDateMS;
            public long dwFileDateLS;
        }

        public struct DEVMODE
        {
            public int dmDeviceName;                           //String* 32
            public int dmSpecVersion;
            public int dmDriverVersion;
            public int dmSize;
            public int dmDriverExtra;
            public long dmFields;
            public int dmOrientation;
            public int dmPaperSize;
            public int dmPaperLength;
            public int dmPaperWidth;
            public int dmScale;
            public int dmCopies;
            public int dmDefaultSource;
            public int dmPrintQuality;
            public int dmColor;
            public int dmDuplex;
            public int dmYResolution;
            public int dmTTOption;
            public int dmCollate;
            public string dmFormName;                          //String* 32
            public int dmUnusedPadding;
            public int dmBitsPerPel;
            public long dmPelsWidth;
            public long dmPelsHeight;
            public long dmDisplayFlags;
            public long dmDisplayFrequency;
        }

        public struct DEVNAMES
        {
            public int wDriverOffset;
            public int wDeviceOffset;
            public int wOutputOffset;
            public int wDefault;
        }

        public struct PRINTDLG_TYPE
        {
            public long lStructSize;
            public long hwndOwner;
            public long hDevMode;
            public long hDevNames;
            public long hDC;
            public long flags;
            public int nFromPage;
            public int nToPage;
            public int nMinPage;
            public int nMaxPage;
            public int nCopies;
            public long hInstance;
            public long lCustData;
            public long lpfnPrintHook;
            public long lpfnSetupHook;
            public long lpPrintTemplateName;
            public long lpSetupTemplateName;
            public long hPrintTemplate;
            public long hSetupTemplate;
        }

        public struct DOCINFO
        {
            public long cbSize;
            public string lpszDocName;
            public long lpszOutput;
            public long lpszDatatype;
            public long fwType;
        }

        public struct ST_TRACKMOUSEEVENT
        {
            public long cbSize;
            public long dwFlags;
            public long hwndTrack;
            public long dwHoverTime;
        }



        [DllImport("user32.dll")]
        public static extern bool GetCursorPos(out POINT lpPoint);
        [DllImport("user32.dll")]
        public static extern int GetCapture();
        [DllImport("user32.dll")]
        public static extern int GetSysColor(int nIndex);
        [DllImport("user32.dll")]
        public static extern int GetSystemMetrics(int nIndex);
        [DllImport("user32.dll")]
        public static extern int GetWindowLong(IntPtr hWnd, uint nIndex);
        [DllImport("user32.dll")]
        public static extern bool RedrawWindow(IntPtr hWnd, IntPtr lprcUpdate, IntPtr hrgnUpdate, int flags);


        [DllImport("gdi32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool BitBlt(IntPtr hdc, int nXDest, int nYDest, int nWidth, int nHeight,
                                        IntPtr hdcSrc, int nXSrc, int nYSrc, TernaryRasterOperations dwRop);
        [DllImport("gdi32.dll")]
        public static extern int SelectObject(IntPtr hDC, int hgdiobj);
        [DllImport("gdi32.dll")]
        public static extern int CreateBrushIndirect(ref LOGBRUSH lplb);
        [DllImport("gdi32.dll")]
        public static extern int GetCurrentObject(IntPtr hDC, int uObjectType);
        [DllImport("gdi32.dll")]
        public static extern int SetViewportOrgEx(IntPtr hDC, int X, int Y, out POINT lpPoint);
        [DllImport("gdi32.dll")]
        public static extern int CreatePen(int iStyle, int cWidth, uint color);
        [DllImport("gdi32.dll")]
        public static extern int DeleteObject(IntPtr hDC);
        [DllImport("gdi32.dll")]
        public static extern int GetDeviceCaps(IntPtr hDC, int nIndex);
        [DllImport("gdi32.dll")]
        public static extern int MoveToEx(IntPtr hDC, int X, int Y, ref POINT lpPoint);
        [DllImport("gdi32.dll")]
        public static extern int LineTo(IntPtr hDC, int nXEnd, int nYEnd);
        [DllImport("gdi32.dll")]
        public static extern int Ellipse(IntPtr hDC, int nLeftRect, int nTopRect, int nRightRect, int nBottomRect);
        [DllImport("gdi32.dll")]
        public static extern int SetTextColor(IntPtr hDC, int crColor);


        [DllImport("kernel32.dll")]
        public static extern void MoveMemory(ref byte Dest, ref byte Source, long length);
        //[DllImport("kernel32.dll")]
        //public static extern int GetPrivateProfileStringA(string lpAppName, string lpKeyName, string lpDefault, string lpReturned, int nSize, string lpFileName);
        [DllImport("kernel32.dll")]
        public static extern long WritePrivateProfileString(string lpAppName, string lpKeyName, string lpString, string lpFileName);
        [DllImport("kernel32.dll")]
        public static extern uint GetPrivateProfileString(string lpAppName, string lpKeyName, string lpDefault, StringBuilder lpReturnedString, uint nSize, string lpFileName);
        [DllImport("kernel32.dll")]
        public static extern int GetTempPath_Wrap(int nBufferLength, string lpBuffer);
        [DllImport("kernel32.dll")]
        public static extern int GetPrivateProfileInt(string lpAppName, string lpKeyName, int nDefault, string lpFileName);
        [DllImport("kernel32.dll")]
        public static extern long CreateFile(string lpFileName, long dwDesiredAccess, long dwShareMode, long lpSecurityAttributes, long dwCreationDisposition, long dwFlagsAndAttributes, long hTemplateFile);
        [DllImport("kernel32.dll")]
        public static extern long CloseHandle(long hObject);
        [DllImport("kernel32.dll")]
        public static extern long SetFileTime(long hFile, ref FILETIME lpCreationTime, ref FILETIME lpLastAccessTime, ref FILETIME lpLastWriteTime);
        [DllImport("kernel32.dll")]
        public static extern long GetFileTime(long hFile, ref FILETIME lpCreationTime, ref FILETIME lpLastAccessTime, ref FILETIME lpLastWriteTime);
        [DllImport("kernel32.dll")]
        public static extern bool FileTimeToLocalFileTime(ref FILETIME lpFileTime, ref FILETIME lpLocalFileTime);
        [DllImport("kernel32.dll")]
        public static extern long FileTimeToSystemTime(ref FILETIME lpFileTime, ref SYSTEMTIME lpSystemTime);
        [DllImport("kernel32.dll")]
        public static extern long SystemTimeToFileTime(ref SYSTEMTIME lpSystemTime, ref FILETIME lpFileTime);
        [DllImport("kernel32.dll")]
        public static extern long LocalFileTimeToFileTime(ref FILETIME lpLocalFileTime, ref FILETIME lpFileTime);
        //[DllImport("kernel32.dll")]
        //public static extern int MoveMemory(int Dest, object Src, int SIZE);
        //public static extern int RtlMoveMemory(ref IntPtr Dest, ref IntPtr Src, int SIZE);
        //[DllImport("ntdll.dll", EntryPoint = "RtlMoveMemory", SetLastError = false)]
        [DllImport("ntdll.dll")]
        public static extern void RtlMoveMemory(ref IntPtr dest, ref object src, [MarshalAs(UnmanagedType.U4)] int length);


        [DllImport("user32.dll")]
        public static extern int DrawText(IntPtr hDC, string lpString, int nCount, ref RECT lpRect, int UFormat);
        [DllImport("user32.dll")]
        public static extern int FillRect(IntPtr hDC, ref RECT lprc, int hbr);
        [DllImport("user32.dll")]
        public static extern int FrameRect(IntPtr hDC, ref RECT lprc, int hbr);
        [DllImport("rapi.dll")]
        public static extern int CeGetLastError();

        [DllImport("advapi32.dll")]
        public static extern long RegCreateKeyEx(long hKey, string lpSubKey, long Reserved, long lpClass, long dwOptions, long samDesired, long lpSecurityAttributes, ref long phkResult, ref long lpdwDisposition);
        [DllImport("advapi32.dll")]
        public static extern long RegOpenKeyEx(long hKey, string lpSubKey, long ulOptions, long samDesired, ref long phkResult);
        [DllImport("advapi32.dll")]
        public static extern long RegCloseKey(long hKey);
        [DllImport("advapi32.dll")]
        public static extern long RegSetValueEx(long hKey, string lpValueName, long Reserved, long dwType, ref long lpData, long cbData);
        [DllImport("advapi32.dll")]
        public static extern long RegSetValueString(long hKey, string lpValueName, long Reserved, long dwType, ref string lpData, long cbData);
        [DllImport("advapi32.dll")]
        public static extern long RegQueryValueEx(long hKey, string lpValueName, long lpReserved, ref long lpType, ref long lpData, ref long lpcbData);
        [DllImport("advapi32.dll")]
        public static extern long RegQueryValueString(long hKey, string lpValueName, long lpReserved, long lpType, string lpData, ref long lpcbData);
        [DllImport("advapi32.dll")]
        public static extern long RegEnumKeyEx(long hKey, long dwIndex, string lpName, ref long lpcName, long lpReserved, long lpClass, long lpcClass, ref FILETIME lpftLastWriteTime);

        [DllImport("user32.dll")] //K.S
        public static extern long SendMessageLS(long hWnd, long msg, long wParam, string sParam);
        [DllImport("user32.dll")] //K.S
        public static extern long SetWindowText(long hWnd, string lpString);
        [DllImport("shell32.dll")] //K.S
        public static extern long SHBrowseForFolder(ref BROWSEINFO lpbi);
        [DllImport("shell32.dll")] //K.S
        public static extern long SHGetPathFromIDList(long pidl, string pszPath);

        [DllImport("ole32.dll")] //K.S
        public static extern void CoTaskMemFree(long pv);


        //'定数
        public const double PAI = 3.14159265358979;             //'π。
        public const double CMPERINCH = 2.54;                   //'１インチあたりの㎝。


        //'プロット種別。
        public enum PLOTTYPE_ENUM
        {
            PLOTTYPE_STANDARD = 0,                              //'標準。
            PLOTTYPE_ANGLE,                                     //'閉合差。
            PLOTTYPE_PRINT,                                     //'印刷。
        }




        //==========================================================================================
        /*[VB]
        '*******************************************************************************
        'APIラップ

        Public Function GetTempPath() As String
            Dim sPath As String * MAX_PATH
            Dim nLen As Long
            If GetTempPath_Wrap(MAX_PATH, sPath) = 0 Then Call Err.Raise(vbObjectError Or &H200& Or &H1&, , GetLastErrorMessage())
            GetTempPath = Left$(sPath, InStr(sPath, vbNullChar) - 1)
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '*******************************************************************************
        'APIラップ

        */
        public string GetTempPath()
        {
            string sPath = "";
            if (GetTempPath_Wrap((int)MAX_PATH, sPath) == 0)
            {
                //Err.Raise(vbObjectError | 0x200 | 0x1, , GetLastErrorMessage());
                return "";
            }
            int i1 = sPath.Length;
            return sPath.Substring(0, i1);
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        Public Function GetPrivateProfileString(ByVal lpAppName As String, ByVal lpKeyName As String, ByVal lpDefault As String, ByVal lpFileName As String) As String
            Dim sValue As String * MAX_PATH
            Call GetPrivateProfileString_Wrap(lpAppName, lpKeyName, lpDefault, sValue, MAX_PATH, lpFileName)
            GetPrivateProfileString = Left$(sValue, InStr(sValue, vbNullChar) - 1)
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        public static string GetPrivateProfileString(string lpAppName, string lpKeyName, string lpDefault, string lpFileName)
        {
            /*
            string sValue = "";
            int l1 = GetPrivateProfileStringA(lpAppName, lpKeyName, lpDefault, sValue, (int)MAX_PATH, lpFileName);
            int i1 = sValue.Length;
            return sValue.Substring(0, i1);
            */
            StringBuilder sb = new StringBuilder((int)MAX_PATH);

            uint i1 = GetPrivateProfileString(lpAppName, lpKeyName, lpDefault, sb, Convert.ToUInt32(sb.Capacity), lpFileName);

            return sb.ToString();
        }
        //==========================================================================================

        //==========================================================================================
        //[C#]
        public static long DF_CreateFile(string lpFileName, long dwDesiredAccess, long dwShareMode, long lpSecurityAttributes, long dwCreationDisposition, long dwFlagsAndAttributes, long hTemplateFile)
        {
            return CreateFile(lpFileName, dwDesiredAccess, dwShareMode, lpSecurityAttributes, dwCreationDisposition, dwFlagsAndAttributes, hTemplateFile);
        }

        public static long DF_SetFileTime(long hFile, ref FILETIME lpCreationTime, ref FILETIME lpLastAccessTime, ref FILETIME lpLastWriteTime)
        {
            return SetFileTime(hFile, ref lpCreationTime, ref lpLastAccessTime, ref lpLastWriteTime);
        }

        public static long DF_GetFileTime(long hFile, ref FILETIME lpCreationTime, ref FILETIME lpLastAccessTime, ref FILETIME lpLastWriteTime)
        {
            return DF_GetFileTime(hFile, ref lpCreationTime, ref lpLastAccessTime, ref lpLastWriteTime);
        }

        public static long DF_CloseHandle(long hObject)
        {
            return CloseHandle(hObject);
        }
        //==========================================================================================
    }
}
