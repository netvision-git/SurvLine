using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SurvLine.mdl.MdlMain;

namespace SurvLine.mdl
{
    internal class MdlRegistAngle
    {
        //==========================================================================================
        /*[VB]
        '*******************************************************************************
        '閉合登録。

        Option Explicit
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '定数
        Public Const REGISTANGLE_DEF_CONNECTDISTANCE As Long = 20 '繋がっていなくても接続と見なす距離のデフォルト値(㎝)。
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'定数
        public const long REGISTANGLE_DEF_CONNECTDISTANCE = 20;     //'繋がっていなくても接続と見なす距離のデフォルト値(㎝)。
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'インプリメンテーション
        Private m_clsRegistAngle As RegistAngle '閉合登録オブジェクト。
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'インプリメンテーション
        private RegistAngle m_clsRegistAngle;   //'閉合登録オブジェクト。
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '閉合登録オブジェクトの設定。
        '
        '引き数：
        'clsRegistAngle 設定する閉合登録オブジェクト。
        Public Sub SetRegistAngle(ByVal clsRegistAngle As RegistAngle)
            Set m_clsRegistAngle = clsRegistAngle
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '閉合登録オブジェクトの設定。
        '
        '引き数：
        'clsRegistAngle 設定する閉合登録オブジェクト。
        */
        public void SetRegistAngle(RegistAngle clsRegistAngle)
        {
            m_clsRegistAngle = clsRegistAngle;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '閉合登録オブジェクトの解放。
        Public Sub ReleaseRegistAngle()
            Set m_clsRegistAngle = Nothing
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'閉合登録オブジェクトの解放。
        public void ReleaseRegistAngle()
        {
            m_clsRegistAngle = null;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'フックプロシージャ。
        '
        '引き数：
        'code フックコード。
        'wParam 仮想キーコード。
        'lParam キーストロークメッセージの情報。
        '
        '戻り値：
        Public Function KeyboardProc(ByVal code As Long, ByVal wParam As Long, ByVal lParam As Long) As Long

            On Error GoTo ErrorHandler
    
            KeyboardProc = m_clsRegistAngle.KeyborardEvent(code, wParam, lParam)
    
            Exit Function
    
        ErrorHandler:
            Call mdlMain.ErrorExit
    
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        'フックプロシージャ。
        '
        '引き数：
        'code フックコード。
        'wParam 仮想キーコード。
        'lParam キーストロークメッセージの情報。
        '
        '戻り値：
        */
        public long KeyboardProc(long code, long wParam, long lParam)
        {
            try
            {
                return m_clsRegistAngle.KeyborardEvent(code, wParam, lParam);

            }

            catch (Exception)
            {
                //MdlMain.ErrorExit();
                return -1;
            }
        }
        //==========================================================================================
    }
}
