using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurvLine
{
    internal class ProcessInterface
    {
        public ProgressInterface clsProgressInterface;


        //'*******************************************************************************
        //'処理インターフェース

        //Option Explicit

        //'*******************************************************************************
        //'メソッド


        //==========================================================================================
        /*[VB]
        '処理。
        '
        '引き数：
        'clsProgressInterface ProgressInterface オブジェクト。
        Public Sub Process(ByVal clsProgressInterface As ProgressInterface)
        End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /// <summary>
        /// 処理。
        ///
        /// 引き数：
        ///     clsProgressInterface ProgressInterface オブジェクト。
        /// 
        /// </summary>
        /// <param name="oProgressInterface"></param>
        public void Process(ProgressInterface oProgressInterface)
        {
            //  clsProgressInterface = oProgressInterface;
            return;
        }


        //==========================================================================================
        /*[VB]
        'キャンセル確認。
        '
        '戻り値：
        'キャンセルが指定されている場合は True を返す。
        'それ以外の場合は False を返す。
        Public Function ConfirmCancel() As Boolean
        End Function
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /// <summary>
        /// キャンセル確認。
        /// 
        /// </summary>
        /// <returns>
        /// 戻り値：
        ///     キャンセルが指定されている場合は True を返す。
        ///     それ以外の場合は False を返す。
        /// </returns>
        public bool ConfirmCancel()
        {
            return true;
        }
    }
}
