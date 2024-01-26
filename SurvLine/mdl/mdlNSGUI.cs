using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace SurvLine
{
    internal class MdlNSGUI
    {

        //24/01/25 K.setoguchi@NV---------->>>>>>>>>>
        //<<<<<<<<<-----------24/01/25 K.setoguchi@NV
        //***************************************************************************
        //***************************************************************************

        //==========================================================================================
        /*[VB]
        '*******************************************************************************
        'NS用GUI関連

        Option Explicit

        '定数
        Public Const GUI_TEXT_MAX_LENGTH As Long = 40 '最大文字数。
        Public Const GUI_ANTHEIGHT_DECIMAL As Long = 3 'アンテナ高少数点以下桁数。
        Public Const GUI_XYZ_DECIMAL As Long = 5 '地心直交座標表示少数点以下桁数。
        Public Const GUI_SEC_DECIMAL As Long = 5 '秒表示少数点以下桁数。
        Public Const GUI_JGD_DECIMAL As Long = 3 '平面直角座標表示少数点以下桁数。
        Public Const GUI_FLOAT_DECIMAL As Long = 4 '汎用実数少数点以下桁数。
        Public Const GUI_DISPERSION_FORMAT As String = "0.00000E+00" '分散・共分散表示書式。

        'メッセージ。
        Public Const GUI_MSG_COORDRANGE As String = "の範囲が適切ではありません。"
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'*******************************************************************************
        //'NS用GUI関連
        //
        //Option Explicit
        //
        //'定数
        public const long GUI_TEXT_MAX_LENGTH = 40;     //'最大文字数。
        public const long GUI_ANTHEIGHT_DECIMAL = 3;    //'アンテナ高少数点以下桁数。
        public const long GUI_XYZ_DECIMAL = 5;          //'地心直交座標表示少数点以下桁数。
        public const long GUI_SEC_DECIMAL = 5;          //'秒表示少数点以下桁数。
        public const long GUI_JGD_DECIMAL = 3;          //'平面直角座標表示少数点以下桁数。
        public const long GUI_FLOAT_DECIMAL = 4;        //'汎用実数少数点以下桁数。
        public const string GUI_DISPERSION_FORMAT = "0.00000E+00";  //'分散・共分散表示書式。

        //'メッセージ。
        public const string GUI_MSG_COORDRANGE = "の範囲が適切ではありません。";




    }
}
