//23/12/24 K.setoguchi@NV---------->>>>>>>>>>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SurvLine
{
    internal class StringTokenizer
    {
        //'プロパティ
        public string Source;          // As String '文字列。
        public string Delimiter;       // As String 'デリミタ。
        public char cDelimiter;        // デリミタの char型。

        //'インプリメンテーション
        private long m_nPos;        // As Long '位置。
        private long m_nLen;        // As Long 'デリミタの長さ。


        //'*******************************************************************************
        //'*******************************************************************************
        /// <summary>
        ///'イベント
        ///
        ///'初期化。
        /// 
        /// </summary>
        private void Class_Initialize()
        {
            Delimiter = ",";

        }
        //--------------------------------------------------------------------------------
        //'イベント
        //
        //'初期化。
        //Private Sub Class_Initialize()
        //    Delimiter = ","
        //End Sub
        //'*******************************************************************************
        //'*******************************************************************************



        //'*******************************************************************************
        //'*******************************************************************************
        /// <summary>
        //'メソッド
        //
        /// 
        /// </summary>
        public void Begin()
        {
            Class_Initialize();

            //m_nPos = 1;
            m_nPos = 0;         //IndexOfは０から
            m_nLen = Delimiter.Length;
            cDelimiter = ',';
        }
        //--------------------------------------------------------------------------------
        //'開始。
        //Public Sub Begin()
        //    m_nPos = 1
        //    m_nLen = Len(Delimiter)
        //End Sub
        //'*******************************************************************************
        //'*******************************************************************************


        //'*******************************************************************************
        //'*******************************************************************************
        /// <summary>
        ///'次のトークン。
        /// 
        /// </summary>
        /// <returns></returns>
        public string NextToken()
        {
            string NextToken;


            if (m_nPos < 0)
            {
                NextToken = "";
            }
            else
            {
                long nFind;


                nFind = Source.IndexOf(cDelimiter, (int)m_nPos);
                if (nFind > 0)
                {
                    NextToken = Source.Substring((int)m_nPos, (int)(nFind - m_nPos));
                    m_nPos = nFind + m_nLen;
                }else{
                    NextToken = Source.Substring((int)m_nPos);
                    m_nPos = 0;
                }
            }

            return NextToken;
        }
        //--------------------------------------------------------------------------------
        //'次のトークン。
        //'
        //'戻り値：
        //'次のトークンが有る場合はそのトークンを返す。
        //'次のトークンが無い場合は空文字を返す。
        //Public Function NextToken() As String
        //    If m_nPos< 1 Then
        //        NextToken = ""
        //    Else
        //        Dim nFind As Long
        //        nFind = InStr(m_nPos, Source, Delimiter)
        //        If nFind > 0 Then
        //            NextToken = Mid$(Source, m_nPos, nFind - m_nPos)
        //            m_nPos = nFind + m_nLen
        //        Else
        //            NextToken = Mid$(Source, m_nPos)
        //            m_nPos = 0
        //        End If
        //    End If
        //End Function
        //'*******************************************************************************
        //'*******************************************************************************

    }
}
//<<<<<<<<<-----------23/12/24 K.setoguchi@NV

