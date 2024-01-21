
//24/01/04 K.setoguchi@NV---------->>>>>>>>>>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace SurvLine
{
    internal class MdlSemiDyna
    {



        //'*******************************************************************************
        //'セミ・ダイナミック補正変換
        //
        //Option Explicit
        //
        //'SemiDynaNGSライブラリインターフェース。
        //Public Declare Function SDN_Create Lib "SemiDynaNGS.dll" (ByVal i_pPath As String, ByVal i_HoldRange As Long, ByVal i_LatDigits As Long, ByVal i_LonDigits As Long) As Long
        //Public Declare Sub SDN_Destroy Lib "SemiDynaNGS.dll" (ByVal i_SemiDyna As Long)
        //Public Declare Function SDN_ConvertGAN2KON Lib "SemiDynaNGS.dll" (ByVal i_SemiDyna As Long, ByVal i_LatGAN As Double, ByVal i_LonGAN As Double, ByVal i_AltGAN As Double, ByRef o_pLatKON As Double, ByRef o_pLonKON As Double, ByRef o_pAltKON As Double) As Long
        //Public Declare Function SDN_ConvertKON2GAN Lib "SemiDynaNGS.dll" (ByVal i_SemiDyna As Long, ByVal i_LatKON As Double, ByVal i_LonKON As Double, ByVal i_AltKON As Double, ByRef o_pLatGAN As Double, ByRef o_pLonGAN As Double, ByRef o_pAltGAN As Double) As Long
        //
        public const string SEMIDYNA_TITLE = "今期座標";    //'帳票タイトル。

        //'メッセージ
        public const string MESSAGE_SEMIDYNA_FAILED = "セミ・ダイナミック補正計算ができない固定座標があります。\n これらの座標は元期座標のまま使用されます。";       //'セミ・ダイナミック補正計算ができなかった。
        public const string MESSAGE_SEMIDYNA_FAILED_GAN = "セミ・ダイナミック補正計算ができない元期座標があります。\n これらの座標は今期座標のまま使用されます。";   //'セミ・ダイナミック補正計算ができなかった。
        public const string MESSAGE_SEMIDYNA_FAILED_KON = "セミ・ダイナミック補正計算ができない今期座標があります。\n これらの座標は元期座標のまま使用されます。";   //'セミ・ダイナミック補正計算ができなかった。

        public long m_nSemiDyna;    //As Long 'セミ・ダイナミックオブジェクト。
        public bool m_bEnable;      //As Boolean 'セミ・ダイナミック補正変換の要否。
        public long m_nCounter;     //As Long 'セミ・ダイナミック補正変換番号。
        public bool m_bMessage;     //As Boolean 'セミ・ダイナミック補正変換ミスのメーっセージを表示するか？

        private string m_sParameterFileInfo;    //As String 'パラメータファイル情報。

        //***************************************************************************
        //***************************************************************************
        /// <summary>
        /// 初期化。
        ///
        /// 引き数：
        ///     bCoor 補正ON/OFF。
        ///     sPath パラメータファイルのパス。
        /// </summary>
        /// <param name="bCorr"></param>
        /// <param name="sPath"></param>
        /// <returns></returns>
        public void Initialize(bool bCorr, string sPath)
        {
            if (m_nSemiDyna != 0)
            {
                //Call SDN_Destroy(m_nSemiDyna)
                m_nSemiDyna = 0;
            }


            if (bCorr)
            {
                //Call SetParameterFileInfo(sPath)
                //m_nSemiDyna = SDN_Create(sPath, 2, 5, 5);
                if (m_nSemiDyna == 0)
                {
                    //Call Err.Raise(ERR_FATAL, , GetLastErrorMessage())
                }
                m_nCounter = m_nCounter + 1;
            }

            m_bEnable = false;

        }
        //------------------------------------------------------------------------------------------
        //'初期化。
        //'
        //'引き数：
        //'bCoor 補正ON/OFF。
        //'sPath パラメータファイルのパス。
        //Public Sub Initialize(ByVal bCorr As Boolean, ByVal sPath As String)
        //
        //    If m_nSemiDyna<> 0 Then
        //        Call SDN_Destroy(m_nSemiDyna)
        //        m_nSemiDyna = 0
        //    End If
        //
        //
        //    If bCorr Then
        //        Call SetParameterFileInfo(sPath)
        //        m_nSemiDyna = SDN_Create(sPath, 2, 5, 5)
        //        If m_nSemiDyna = 0 Then Call Err.Raise(ERR_FATAL, , GetLastErrorMessage())
        //        m_nCounter = m_nCounter + 1
        //    End If
        //
        //
        //    m_bEnable = False
        //
        //End Sub
        //***************************************************************************
        //***************************************************************************

        //'SemiDynaNGSライブラリインターフェース。
        //Public Declare Function SDN_Create Lib "SemiDynaNGS.dll" (ByVal i_pPath As String, ByVal i_HoldRange As Long, ByVal i_LatDigits As Long, ByVal i_LonDigits As Long) As Long
        //Public Declare Sub SDN_Destroy Lib "SemiDynaNGS.dll" (ByVal i_SemiDyna As Long)
        //Public Declare Function SDN_ConvertGAN2KON Lib "SemiDynaNGS.dll" (ByVal i_SemiDyna As Long, ByVal i_LatGAN As Double, ByVal i_LonGAN As Double, ByVal i_AltGAN As Double, ByRef o_pLatKON As Double, ByRef o_pLonKON As Double, ByRef o_pAltKON As Double) As Long
        //Public Declare Function SDN_ConvertKON2GAN Lib "SemiDynaNGS.dll" (ByVal i_SemiDyna As Long, ByVal i_LatKON As Double, ByVal i_LonKON As Double, ByVal i_AltKON As Double, ByRef o_pLatGAN As Double, ByRef o_pLonGAN As Double, ByRef o_pAltGAN As Double) As Long



        /// <summary>
        /// 終了処理。
        /// 
        /// </summary>
        public void Terminate()
        {

            //  var asm = System.Reflection.Assembly.GetExecutingAssembly("SemiDynaNGS.dll");


            if (m_nSemiDyna != 0)
            {
                //  SDN_Destroy(m_nSemiDyna);
                m_nSemiDyna = 0;
            }
        }
        //'終了処理。
        //Public Sub Terminate()
        //
        //  If m_nSemiDyna<> 0 Then
        //      Call SDN_Destroy(m_nSemiDyna)
        //      m_nSemiDyna = 0
        //  End If
        //End Sub
        //***************************************************************************
        //***************************************************************************

    }

}
//<<<<<<<<<-----------24/01/04 K.setoguchi@NV
