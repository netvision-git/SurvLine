using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace SurvLine
{
    internal class ProgressInterface
    {

        //'*******************************************************************************
        //'プログレスインターフェース

        //Option Explicit

        //==========================================================================================
        /*[VB]
            'プロパティ
            Public CancelEnable As Boolean 'キャンセルの可否。
            Public Prompt As String 'プロンプト。
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'プロパティ
        public bool CancelEnable;   //'キャンセルの可否。
        public string Prompt;       //'プロンプト。

        //==========================================================================================
        /*[VB]
        'プログレス最小値。
        Property Let MinPos(Optional ByVal nIndex As Long = 0, ByVal nPos As Integer)
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        public void MinPos(long nIndex, int nPos)
        {
            nIndex = 0;
        }


        //==========================================================================================
        /*[VB]
            'プログレス最小値。
            Property Get MinPos(Optional ByVal nIndex As Long = 0) As Integer
            End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        public int MinPos(long nIndex)
        {
            nIndex = 0;
            return 0;
        }

        //==========================================================================================
        /*[VB]
            'プログレス最大値。
            Property Let MaxPos(Optional ByVal nIndex As Long = 0, ByVal nPos As Integer)
            End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        public void MaxPos(long nIndex, int nPos)
        {
            nIndex = 0;
        }

        //==========================================================================================
        /*[VB]
            'プログレス最大値。
            Property Get MaxPos(Optional ByVal nIndex As Long = 0) As Integer
            End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]


        //==========================================================================================
        /*[VB]
            'プログレス現在値。
            Property Let CurPos(Optional ByVal nIndex As Long = 0, ByVal nPos As Integer)
            End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]

        //==========================================================================================
        /*[VB]
            'プログレス現在値。
            Property Get CurPos(Optional ByVal nIndex As Long = 0) As Integer
            End Property
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]



        //==========================================================================================
        /*[VB]
            'キャンセル。
            Property Get Cancel() As Boolean
            End Property
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]

        //==========================================================================================
        /*[VB]
        'キャンセルを設定する。
        '
        '引き数：
        'bCancel キャンセルフラグ。True=キャンセル。False=続行。
        Public Sub SetCancel(Optional ByVal bCancel As Boolean = True)
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        public void SetCancel(bool bCancel)
        {
            bCancel = true;
        }

        //==========================================================================================
        /*[VB]
        'キャンセルをチェックする。
        '
        '戻り値：
        'キャンセルの場合 True を返す。
        'それ以外の場合 False を返す。
        Public Function CheckCancel() As Boolean
        End Function
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        public bool CheckCancel()
        {
            return true;
        }



    }
}
