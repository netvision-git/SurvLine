using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SurvLine.mdl
{
    internal class MdiDefine
    {
    }
    public class DEFINE
    {
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
        public const long TME_CANCEL= 0x80000000;
        public const long ERROR_SUCCESS = 0;
        public const long ERROR_FILE_NOT_FOUND = 0x2;
        public const long CSIDL_PROGRAM_FILES = 0x26;
        public const long S_OK = 0x0;


    }
}
