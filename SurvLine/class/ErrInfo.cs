using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace SurvLine
{
    internal class ErrInfo
    {

        public readonly Exception Err;


        //'*******************************************************************************
        //'エラー情報。

        //Option Explicit

        //'プロパティ
        public long Number;         //As Long 'エラー番号。
        public string Source;       // As String 'エラーソース。
        public string Description;  // As String 'エラー説明。
        public string HelpFile;     // As String 'エラーヘルプファイル。
        public long HelpContext;    // As Variant  'エラーコンテキスト番号。

        //'*******************************************************************************
        //'イベント

        public ErrInfo()
        {
            Class_Initialize();
        }


        //==========================================================================================
        /*[VB]
        '初期化。
        Private Sub Class_Initialize()
            Call SetErr
        End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        private void Class_Initialize()
        {
//            SetErr();
        }

        //==========================================================================================
        /*[VB]
        '*******************************************************************************
        'メソッド

        'エラー情報の設定。
        '
        '現在の Err オブジェクトのパラメータを設定する。
        Public Sub SetErr()
            Number = Err.Number
            Source = Err.Source
            Description = Err.Description
            HelpFile = Err.HelpFile
            HelpContext = Err.HelpContext
        End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        public void SetErr()
        {
            Number = Err.HResult;
            Source = Err.Source;
            Description = Err.Message;
            HelpFile = Err.HelpLink;
            HelpContext = Err.HResult;
        }


        //==========================================================================================
        /*[VB]
        '実行時エラーの生成。
        Public Sub Raise()
            Call Err.Raise(Number, Source, Description, HelpFile, HelpContext)
        End Sub

            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        public void Raise()
        {
            //  Err.Raise(Number, Source, Description, HelpFile, HelpContext);
        }




    }
}
