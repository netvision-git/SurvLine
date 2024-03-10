//23/12/29 K.setoguchi@NV---------->>>>>>>>>>

using SurvLine.mdl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;
using static System.Net.Mime.MediaTypeNames;

namespace SurvLine.mdl
{
    public class MdiVBfunctions
    {

        public void DoEvents()
        {
            return;
            throw new NotImplementedException();
        }
        //====================================================================
        public static long InStrRev(string target, string search, int stpot)
        {
            int pot;
            pot = target.IndexOf(search, stpot);
            return pot == -1 ? 0 : pot;
        }
        public static long InStrRev(string target, string search)
        {
            int pot;
            int stpot = 0;
            pot = target.IndexOf(search, stpot);
            return pot == -1 ? 0 : pot;
        }
        //====================================================================

        public static long InStr(int stpot, string target, string search)
        {
            int pot;
            pot = target.IndexOf(search, stpot);
            return pot == -1 ? 0 : pot;

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public static byte[] StrConv(string data, int code)
        {
            byte[] bytes = new byte[data.Length];
            string sGetData = "";


            switch (code)
            {
                case DEFINE.vbUpperCase:        //文字列を大文字に変換します。
                    sGetData = data.ToUpper();
                    break;
                case DEFINE.vbLowerCase:        //文字列を小文字に変換します。
                    sGetData = data.ToLower();
                    break;
                case DEFINE.vbProperCase:       //文字列内の各単語の先頭の文字を大文字に変換します。
                    break;
                case DEFINE.vbWide:             //文字列内の半角文字(1 バイト) を全角文字(2 バイト) に変換します。
                    break;
                case DEFINE.vbNarrow:           //文字列内の全角文字(2 バイト) を半角文字(1 バイト) に変換します。
                    break;
                case DEFINE.vbKatakana:         //文字列内のひらがなをカタカナに変換します。
                    break;
                case DEFINE.vbHiragana:         //文字列内のカタカナをひらがなに変換します。
                    break;
                case DEFINE.vbUnicode:          //システムの既定のコード ページを使用して、文字列を Unicode に変換します。 (Macintosh では使用できません。
                    sGetData = Encoding.Unicode.GetString(bytes);
                    break;
                case DEFINE.vbFromUnicode:      //文字列を Unicode からシステムの既定のコード ページに変換します。 (Macintosh では使用できません。
                    //  sGetData = Encoding.GetEncoding("shift_jis").GetString(bytes);
                    sGetData = Encoding.UTF8.GetString(bytes);
                    break;
                default:
                    sGetData = data;
                    break;
            }

            byte[] bytes2 = new byte[sGetData.Length];

            return bytes2;
        }

        /// <summary>
        /// 文字列の指定した位置から指定した長さを取得する
        /// </summary>
        /// <param name="str">文字列</param>
        /// <param name="start">開始位置</param>
        /// <param name="len">長さ</param>
        /// <returns>取得した文字列</returns>
        public string Mid(string str, int start, int len)
        {
            if (start <= 0)
            {
                throw new ArgumentException("引数'start'は1以上でなければなりません。");
            }
            if (len < 0)
            {
                throw new ArgumentException("引数'len'は0以上でなければなりません。");
            }
            if (str == null || str.Length < start)
            {
                return "";
            }
            if (str.Length < (start + len))
            {
                return str.Substring(start - 1);
            }
            return str.Substring(start - 1, len);
        }
        /// <summary>
        /// 文字列の指定した位置から取得する
        /// </summary>
        /// <param name="str">文字列</param>
        /// <param name="start">開始位置</param>
        /// <returns>取得した文字列</returns>
        public string Mid(string str, long start)
        {
            int pot = (int)start;
            if (pot <= 0)
            {
                throw new ArgumentException("引数'start'は1以上でなければなりません。");
            }
            if (str == null || str.Length < pot)
            {
                return "";
            }
            return str.Length < pot ? str.Substring(pot - 1) : str.Substring(pot - 1);
        }

        /// <summary>
        /// 文字列の指定した位置から末尾までを取得する
        /// </summary>
        /// <param name="str">文字列</param>
        /// <param name="start">開始位置</param>
        /// <returns>取得した文字列</returns>
        public string Mid(string str, int start)
        {
            return Mid(str, start, str.Length);
        }

        /// <summary>
        /// 文字列の先頭から指定した長さの文字列を取得する
        /// </summary>
        /// <param name="str">文字列</param>
        /// <param name="len">長さ</param>
        /// <returns>取得した文字列</returns>
        public string Left(string str, int len)
        {
            if (len < 0)
            {
                throw new ArgumentException("引数'len'は0以上でなければなりません。");
            }
            if (str == null)
            {
                return "";
            }
            if (str.Length <= len)
            {
                return str;
            }
            return str.Substring(0, len);
        }

        /// <summary>
        /// 文字列の末尾から指定した長さの文字列を取得する
        /// </summary>
        /// <param name="str">文字列</param>
        /// <param name="len">長さ</param>
        /// <returns>取得した文字列</returns>
        public string Right(string str, int len)
        {
            return len < 0
                ? throw new ArgumentException("引数'len'は0以上でなければなりません。")
                : str == null ? "" : str.Length <= len ? str : str.Substring(str.Length - len, len);
        }

        /// <summary>
        /// StrComp(文字列1, 文字列2)
        /// 
        /// </summary>
        /// <param name="strData1"></param>
        /// <param name="strData2"></param>
        /// <returns></returns>
        public int StrComp(string strData1, string strData2)
        {

            int StrComp = strData1.IndexOf(strData2);
            return StrComp;
        }

        internal int InStr(string sPath, object value)
        {
            throw new NotImplementedException();
        }
    }
}
//<<<<<<<<<-----------23/12/29 K.setoguchi@NV
