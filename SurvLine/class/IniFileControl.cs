
//23/12/29 K.setoguchi@NV---------->>>>>>>>>>

using SurvLine.mdl;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.AxHost;

namespace SurvLine
{
    internal class IniFileControl
    {

        MdiVBfunctions mdiVBfunctions = new MdiVBfunctions();
        public List<string> ProfileList;

        private bool PutPrivateProfile_All(string sPath, ref List<string> ProfileList)
        {
            //  App_Path = C:\Hitz\NetSurv\Prog\Src
            //  App_Title =  "NS-Survey"  
            bool PutPrivateProfile_All = false;

            //------------------------------------------------------------------------
            //System.IO.StreamReader sr = new System.IO.StreamReader(
            //  @"C:\test\1.txt",
            //  System.Text.Encoding.GetEncoding("shift_jis"));
            //------------------------------------------------------------------------
            using (var sr = new StreamWriter(sPath,false, System.Text.Encoding.GetEncoding("shift_jis")))
            {

                for(int i = 0; i < ProfileList.Count; i++)
                {
                    sr.WriteLine(ProfileList[i]);

                }
                PutPrivateProfile_All = true;
            }
            return PutPrivateProfile_All;
        }


        private bool GetPrivateProfile_All(string sPath, ref List<string> ProfileList)
        {
            //  App_Path = C:\Hitz\NetSurv\Prog\Src
            //  App_Title =  "NS-Survey"  
            bool GetPrivateProfile_All = false;

            //List<string> ProfileList = new List<string>();

            if(File.Exists(sPath) == false)
            {
                MessageBox.Show($"{sPath} ファイルが見つかりません。", "エラー発生");
            }


            //------------------------------------------------------------------------
            //System.IO.StreamReader sr = new System.IO.StreamReader(
            //  @"C:\test\1.txt",
            //  System.Text.Encoding.GetEncoding("shift_jis"));
            //------------------------------------------------------------------------
            using (var sr = new StreamReader(sPath, System.Text.Encoding.GetEncoding("shift_jis")))
            {
                string line = null;
                while ((line = sr.ReadLine()) != null)
                {

                    ProfileList.Add(line);

                }
                GetPrivateProfile_All = true;
            }
            return GetPrivateProfile_All;
        }


        //インターフェース
        //=======================================================
        //UINT GetPrivateProfileInt(
        //  [in] LPCTSTR lpAppName,
        //  [in] LPCTSTR lpKeyName,
        //  [in] INT nDefault,
        //  [in] LPCTSTR lpFileName
        //);

        //***************************************************************************
        //***************************************************************************
        /// <summary>
        /// ◆.iniファイルを読み込み（int型）
        ///  lpAppName,        // セクション名
        ///  pKeyName,        // キー名
        ///  lpDefault,        // 既定の文字列
        ///  lpFileName        // .ini ファイルの名前
        /// 
        /// </summary>
        /// <param name="lpAppName"></param>
        /// <param name="lpKeyName"></param>
        /// <param name="nDefault"></param>
        /// <param name="lpFileName"></param>
        /// <returns>
        /// .iniファイルを読み込みデータ（int型）
        /// </returns>
        public int GetPrivateProfileInt(string lpAppName, string lpKeyName, int nDefault, string lpFileName)
        {

            int GetPrivateProfileInt = nDefault;

            List<string> ProfileList = new List<string>();


            if (GetPrivateProfile_All(lpFileName, ref ProfileList))
            {
                int step = 0;

                for (int i = 0; i < ProfileList.Count; i++)
                {
                    if (step == 0)
                    {
                        if (ProfileList[i].Contains(lpAppName))
                        {
                            step = 1;
                        }
                    }
                    else if (step == 1)
                    {
                        if (ProfileList[i].Contains(lpKeyName))
                        {

                            int pot = ProfileList[i].IndexOf('=', 1);
                            string edit;
                            edit = mdiVBfunctions.Mid(ProfileList[i], pot + 2);
                            Int32.TryParse(edit, out GetPrivateProfileInt);
                            break;

                        } //if (ProfileList[i].Contains(lpKeyName))

                    }
                    else
                    {
                        break;
                    }
                }   //for (int i = 0; i < ProfileList.Count || ii > 0; i++)
            }

            return GetPrivateProfileInt;
        }


        //***************************************************************************
        //***************************************************************************
        /// <summary>
        /// ◆.iniファイルを読み込み（bool型）
        ///    ※GetPrivateProfileInt関数のbool型
        /// 
        ///  lpAppName,        // セクション名
        ///  pKeyName,        // キー名
        ///  lpDefault,        // 既定の文字列
        ///  lpFileName        // .ini ファイルの名前
        /// 
        /// </summary>
        /// <param name="lpAppName"></param>
        /// <param name="lpKeyName"></param>
        /// <param name="nDefault"></param>
        /// <param name="lpFileName"></param>
        /// <returns>
        /// .iniファイルを読み込みデータ（bool型）
        /// </returns>
        public bool GetPrivateProfileBool(string lpAppName, string lpKeyName, int nDefault, string lpFileName)
        {

            bool GetPrivateProfileBool = false;

            int GetPrivateProfileInt = nDefault;

            List<string> ProfileList = new List<string>();


            if (GetPrivateProfile_All(lpFileName, ref ProfileList))
            {
                int step = 0;

                for (int i = 0; i < ProfileList.Count; i++)
                {
                    if (step == 0)
                    {
                        if (ProfileList[i].Contains(lpAppName))
                        {
                            step = 1;
                        }
                    }
                    else if (step == 1)
                    {
                        if (ProfileList[i].Contains(lpKeyName))
                        {

                            int pot = ProfileList[i].IndexOf('=', 1);
                            string edit;
                            edit = mdiVBfunctions.Mid(ProfileList[i], pot + 2);
                            Int32.TryParse(edit, out GetPrivateProfileInt);

                            if(GetPrivateProfileInt != 0)
                            {
                                GetPrivateProfileBool = true;
                            }
                            else
                            {
                                GetPrivateProfileBool = false;
                            }

                            break;

                        } //if (ProfileList[i].Contains(lpKeyName))

                    }
                    else
                    {
                        break;
                    }
                }   //for (int i = 0; i < ProfileList.Count || ii > 0; i++)
            }

            return GetPrivateProfileBool;
        }


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

        //***************************************************************************
        //***************************************************************************
        /// <summary>
        /// ◆.iniファイルを読み込み（文字列）
        ///  lpAppName,        // セクション名
        ///  pKeyName,        // キー名
        ///  lpDefault,        // 既定の文字列
        ///  nSize,              // 情報バッファのサイズ
        ///  lpFileName        // .ini ファイルの名前
        /// </summary>
        /// <param name="lpAppName"></param>
        /// <param name="lpKeyName"></param>
        /// <param name="lpDefault"></param>
        /// <param name="lpFileName"></param>
        /// <returns>
        /// .iniファイルを読み込みデータ（文字列）
        ///  lpReturnedString,  // 情報が格納されるバッファ
        /// </returns>
        public string GetPrivateProfileString(string lpAppName, string lpKeyName, string lpDefault, string lpFileName)
        {
            string GetProfileString = lpDefault;    //lpReturnedString
            long nSize = 0;

            List<string> ProfileList = new List<string>();

            if (GetPrivateProfile_All(lpFileName, ref ProfileList))
            {
                int step = 0;

                for (int i = 0; i < ProfileList.Count; i++)
                {
                    if (step == 0)
                    {
                        if (ProfileList[i].Contains($"[{lpAppName}]"))
                        {
                            step = 1;
                        }
                    }
                    else if (step == 1)
                    {
                        if (ProfileList[i].Contains(lpKeyName))
                        {
                            //step = 2;

                            int pot = ProfileList[i].IndexOf('=', 1);
                            string edit;

                            nSize = ProfileList[i].Length;
                            edit = mdiVBfunctions.Mid(ProfileList[i], pot + 2, (int)nSize);

                            GetProfileString = edit;

                            break;

                        } //if (ProfileList[i].Contains(lpKeyName))

                    }
                    else
                    {
                        break;
                    }
                }   //for (int i = 0; i < ProfileList.Count || ii > 0; i++)
            }

            return GetProfileString;
        }
        //***************************************************************************
        //***************************************************************************


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
        //***************************************************************************
        //***************************************************************************
        /// ◆.iniファイルに書き込み（文字列）
        /// <summary>
        ///  lpAppName,        // セクション名
        ///  pKeyName,        // キー名
        ///  lpString,        // 書き込み文字列
        ///  lpFileName        // .ini ファイルの名前
        /// 
        /// </summary>
        /// <param name="lpAppName"></param>
        /// <param name="lpKeyName"></param>
        /// <param name="lpString"></param>
        /// <param name="lpFileName"></param>
        /// <returns>
        /// 戻り値：
        ///     正常終了の場合 True を返す。
        ///     それ以外の場合 False を返す。
        /// </returns>
        public bool WritePrivateProfileString( string lpAppName, string lpKeyName, string lpString, string lpFileName)
        {

            //DEBUG>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
            //lpFileName = @"C:\Hitz\NetSurv\Prog\Src\NS-Survey.ini";
            //DEBUG<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<

            bool WritePrivateProfileString = false;

            List<string> ProfileList = new List<string>();


            if (GetPrivateProfile_All(lpFileName, ref ProfileList))
            {
                int step = 0;

                for (int i = 0; i < ProfileList.Count; i++)
                {
                    if (step == 0)
                    {
                        if (ProfileList[i].Contains($"[{lpAppName}]"))
                        {
                            step = 1;
                        }
                    }
                    else if (step == 1)
                    {
                        if (ProfileList[i].Contains(lpKeyName))
                        {
                            //step = 2;

                            int pot = ProfileList[i].IndexOf('=', 1);
                            string edit;
                            edit = mdiVBfunctions.Mid(ProfileList[i], 1, pot + 1);
                            edit = edit + lpString;

                            ProfileList[i] = edit;


                            if (PutPrivateProfile_All(lpFileName, ref ProfileList))
                            {
                                //iniファイル書き込み正常終了
                                WritePrivateProfileString = true;
                                break;
                            }
                            else
                            {
                                MessageBox.Show($"{lpFileName} ファイルの書き込みに失敗した", "エラー発生");

                                WritePrivateProfileString = false;
                                break;
                            }
                        } //if (ProfileList[i].Contains(lpKeyName))

                    }
                    else
                    {
                        WritePrivateProfileString = false;
                        break;
                    }
                }   //for (int i = 0; i < ProfileList.Count || ii > 0; i++)
            }
            return WritePrivateProfileString;
        }



    }
}
//<<<<<<<<<-----------23/12/29 K.setoguchi@NV
