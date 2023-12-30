
//23/12/29 K.setoguchi@NV---------->>>>>>>>>>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurvLine
{
    internal class IniFileControl
    {

        //◆.iniファイルを読み込みたい場合は、GetPrivateProfileString()を使用する。

        //インターフェース
        //=======================================================
        //DWORD GetPrivateProfileString(
        //LPCTSTR lpAppName,        // セクション名
        //LPCTSTR lpKeyName,        // キー名
        //LPCTSTR lpDefault,        // 既定の文字列
        //LPTSTR lpReturnedString,  // 情報が格納されるバッファ
        //DWORD nSize,              // 情報バッファのサイズ
        //LPCTSTR lpFileName        // .ini ファイルの名前
        //);
        //=======================================================
        //※MSDNライブラリから引用





        public bool WritePrivateProfileString( string lpAppName, string lpKeyName, string lpString, string lpFileName)
        {

            return false;
        }
        //
        //◆.iniファイルを書き込みたい場合は、WritePrivateProfileString()を使用する。
        //
        //インターフェース
        //=======================================================
        //BOOL WritePrivateProfileString(
        //  LPCTSTR lpAppName,  // セクション名
        //  LPCTSTR lpKeyName,  // キー名
        //  LPCTSTR lpString,   // 追加するべき文字列
        //  LPCTSTR lpFileName  // .ini ファイル
        //);
        //=======================================================
        //※MSDNライブラリから引用



    }
}
//<<<<<<<<<-----------23/12/29 K.setoguchi@NV
