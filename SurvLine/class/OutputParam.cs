using SurvLine.mdl;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;
using static System.Net.WebRequestMethods;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using static SurvLine.mdl.DEFINE;
using static SurvLine.mdl.MdlNSDefine;
using static SurvLine.mdl.MdlUtility;
using static SurvLine.mdl.MdlAccountMake;
using static SurvLine.mdl.MdlNSGUI;
using static SurvLine.mdl.MdlNSSDefine;
using static SurvLine.mdl.MdiVBfunctions;

namespace SurvLine
{
    internal class OutputParam
    {

        //'*******************************************************************************
        //'外部出力ファイル出力パラメータ

        //Option Explicit


        //==========================================================================================
        /*[VB]
            'プロパティ
            Public Path As String 'パス。
            Public Automation As Boolean 'OLEオートメーションフラグ。
            Public Fixed As String '既知点。テンポラリなので注意。'2023/06/26 Hitz H.Nakamura GNSS水準測量対応。前半後半較差の追加。
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'プロパティ
        public string Path;             //'パス。
        public bool Automation;         //'OLEオートメーションフラグ。
        public string Fixed;            //'既知点。テンポラリなので注意。'2023/06/26 Hitz H.Nakamura GNSS水準測量対応。前半後半較差の追加。

        //==========================================================================================
        /*[VB]
            'インプリメンテーション
            Private m_clsAccountParam As New AccountParam '帳票パラメータ。
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'インプリメンテーション
        private AccountParam m_clsAccountParam = new AccountParam();    //'帳票パラメータ。


        //'*******************************************************************************
        //'プロパティ

        //==========================================================================================
        /*[VB]
            '外部出力ファイル出力パラメータ。
            '
            'このプロパティはデフォルトプロパティである。Letを使えば｢値の代入｣ができる。いわゆるC++で言うところのoperator=である。
            '
            '引き数：
            'clsOutputParam コピー元のオブジェクト。
            Property Let OutputParam(ByVal clsOutputParam As OutputParam)
                Path = clsOutputParam.Path
                Automation = clsOutputParam.Automation
                Let m_clsAccountParam = clsOutputParam.AccountParam
            End Property

            '帳票パラメータ。
            Property Let AccountParam(ByVal clsAccountParam As AccountParam)
                Let m_clsAccountParam = clsAccountParam
            End Property

            '帳票パラメータ。
            Property Get AccountParam() As AccountParam
                Set AccountParam = m_clsAccountParam
            End Property
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /// <summary>
        ///    '外部出力ファイル出力パラメータ。
        ///    '
        ///    'このプロパティはデフォルトプロパティである。Letを使えば｢値の代入｣ができる。いわゆるC++で言うところのoperator=である。
        ///    '
        ///    '引き数：
        ///    'clsOutputParam コピー元のオブジェクト。
        /// 
        /// </summary>
        /// <param name="clsOutputParam"></param>
        public void _OutputParam(OutputParam clsOutputParam)
        {
            Path = clsOutputParam.Path;
            Automation = clsOutputParam.Automation;
            m_clsAccountParam = clsOutputParam.AccountParam();

        }
        /// <summary>
        /// '帳票パラメータ。
        /// 
        /// </summary>
        /// <param name="clsAccountParam"></param>
        public void AccountParam(AccountParam clsAccountParam)
        {
            m_clsAccountParam = clsAccountParam;
        }
        /// <summary>
        /// '帳票パラメータ。
        /// </summary>
        /// <returns></returns>
        public AccountParam AccountParam()
        {
            AccountParam AccountParam = m_clsAccountParam;
            return AccountParam;
        }


        //'*******************************************************************************
        //'イベント


        //==========================================================================================
        /*[VB]
            '初期化
            Private Sub Class_Initialize()
                m_clsAccountParam.RangeType = DEF_OUTPUT_RANGE_TYPE
                m_clsAccountParam.ObjectType = DEF_OUTPUT_SELECTED_OBJECT_TYPE
            End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        private void Class_Initialize()
        {
            m_clsAccountParam.RangeType = MdlAccountMakeNSS.DEF_OUTPUT_RANGE_TYPE;
            m_clsAccountParam.ObjectType = (MdlAccountMake.SELECTED_OBJECT_TYPE)MdlAccountMakeNSS.DEF_OUTPUT_SELECTED_OBJECT_TYPE;
        }


        //'*******************************************************************************
        //'メソッド


        //==========================================================================================
        /*[VB]
            '保存。
            '
            '引き数：
            'nFile ファイル番号。
            Public Sub Save(ByVal nFile As Integer)
                Call PutString(nFile, Path)
                Put #nFile, , Automation
                Call m_clsAccountParam.Save(nFile)
            End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        public void Save(BinaryWriter bw)
        {

            FileWrite_PutString(bw, Path);
            PutFileBool(bw, Automation);
            m_clsAccountParam.Save(bw);
        }


        //==========================================================================================
        /*[VB]
            '読み込み。
            '
            '引き数：
            'nFile ファイル番号。
            'nVersion ファイルバージョン。
            Public Sub Load(ByVal nFile As Integer, ByVal nVersion As Long)
                Path = GetString(nFile)
                If nVersion< 3900 Then
                    Automation = False
                Else
                    Get #nFile, , Automation
                End If
                Call m_clsAccountParam.Load(nFile, nVersion)
            End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        public void Load(BinaryReader br, long nVersion)
        {
            //Path = GetString(nFile)
            if (nVersion < 3900)
            {
                Automation = false;
            }

            m_clsAccountParam.Load(br, nVersion);
        }


        //==========================================================================================
        /*[VB]
            '指定されたオブジェクトと比較する。
            '
            '引き数：
            'clsOutputParam 比較対照オブジェクト。
            '
            '戻り値：
            '一致する場合 True を返す。
            'それ以外の場合 False を返す。
            Public Function Compare(ByVal clsOutputParam As OutputParam) As Boolean

                Compare = False

                If Path<> clsOutputParam.Path Then Exit Function
                If Automation<> clsOutputParam.Automation Then Exit Function
                If Not m_clsAccountParam.Compare(clsOutputParam.AccountParam) Then Exit Function


                Compare = True


            End Function
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        public bool Compare(OutputParam clsOutputParam)
        {
            bool Compare = false;

            if (Path != clsOutputParam.Path)
            {
                return Compare;
            }
            if (Automation != clsOutputParam.Automation)
            {
                return Compare;
            }
            if (!m_clsAccountParam.Compare(clsOutputParam.AccountParam()))
            {
                return Compare;
            }

            Compare = true;
            return Compare;

        }





    }
}
