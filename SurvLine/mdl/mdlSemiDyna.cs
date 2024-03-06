using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;

namespace SurvLine.mdl
{
    internal class MdlSemiDyna
    {
        //==========================================================================================
        /*[VB]
        '*******************************************************************************
        'セミ・ダイナミック補正変換

        Option Explicit
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'SemiDynaNGSライブラリインターフェース。
        Public Declare Function SDN_Create Lib "SemiDynaNGS.dll" (ByVal i_pPath As String, ByVal i_HoldRange As Long, ByVal i_LatDigits As Long, ByVal i_LonDigits As Long) As Long
        Public Declare Sub SDN_Destroy Lib "SemiDynaNGS.dll" (ByVal i_SemiDyna As Long)
        Public Declare Function SDN_ConvertGAN2KON Lib "SemiDynaNGS.dll" (ByVal i_SemiDyna As Long, ByVal i_LatGAN As Double, ByVal i_LonGAN As Double, ByVal i_AltGAN As Double, ByRef o_pLatKON As Double, ByRef o_pLonKON As Double, ByRef o_pAltKON As Double) As Long
        Public Declare Function SDN_ConvertKON2GAN Lib "SemiDynaNGS.dll" (ByVal i_SemiDyna As Long, ByVal i_LatKON As Double, ByVal i_LonKON As Double, ByVal i_AltKON As Double, ByRef o_pLatGAN As Double, ByRef o_pLonGAN As Double, ByRef o_pAltGAN As Double) As Long

        Public Const SEMIDYNA_TITLE = "今期座標" '帳票タイトル。

        'メッセージ
        Public Const MESSAGE_SEMIDYNA_FAILED As String = "セミ・ダイナミック補正計算ができない固定座標があります。" & vbCrLf & "これらの座標は元期座標のまま使用されます。" 'セミ・ダイナミック補正計算ができなかった。
        Public Const MESSAGE_SEMIDYNA_FAILED_GAN As String = "セミ・ダイナミック補正計算ができない元期座標があります。" & vbCrLf & "これらの座標は今期座標のまま使用されます。" 'セミ・ダイナミック補正計算ができなかった。
        Public Const MESSAGE_SEMIDYNA_FAILED_KON As String = "セミ・ダイナミック補正計算ができない今期座標があります。" & vbCrLf & "これらの座標は元期座標のまま使用されます。" 'セミ・ダイナミック補正計算ができなかった。

        Public m_nSemiDyna As Long 'セミ・ダイナミックオブジェクト。
        Public m_bEnable As Boolean 'セミ・ダイナミック補正変換の要否。
        Public m_nCounter As Long 'セミ・ダイナミック補正変換番号。
        Public m_bMessage As Boolean 'セミ・ダイナミック補正変換ミスのメーっセージを表示するか？
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'SemiDynaNGSライブラリインターフェース。
        [DllImport("SemiDynaNGS.dll")]
        public static extern long SDN_Create(string i_pPath, long i_HoldRange, long i_LatDigits, long i_LonDigits);
        [DllImport("SemiDynaNGS.dll")]
        public static extern void SDN_Destroy(long i_SemiDyna);
        [DllImport("SemiDynaNGS.dll")]
        public static extern long SDN_ConvertGAN2KON(long i_SemiDyna, double i_LatGAN, double i_LonGAN, double i_AltGAN, ref double o_pLatKON, ref double o_pLonKON, ref double o_pAltKON);
        [DllImport("SemiDynaNGS.dll")]
        public static extern long SDN_ConvertKON2GAN(long i_SemiDyna, double i_LatKON, double i_LonKON, double i_AltKON, ref double o_pLatGAN, ref double o_pLonGAN, ref double o_pAltGAN);

        public const string SEMIDYNA_TITLE = "今期座標";        //'帳票タイトル。

        //'メッセージ
        public const string MESSAGE_SEMIDYNA_FAILED = "セミ・ダイナミック補正計算ができない固定座標があります。" + "\r\n" + "これらの座標は元期座標のまま使用されます。";        //'セミ・ダイナミック補正計算ができなかった。
        public const string MESSAGE_SEMIDYNA_FAILED_GAN = "セミ・ダイナミック補正計算ができない元期座標があります。" + "\r\n" + "これらの座標は今期座標のまま使用されます。";    //'セミ・ダイナミック補正計算ができなかった。
        public const string MESSAGE_SEMIDYNA_FAILED_KON = "セミ・ダイナミック補正計算ができない今期座標があります。" + "\r\n" + "これらの座標は元期座標のまま使用されます。";    //'セミ・ダイナミック補正計算ができなかった。

        public static long m_nSemiDyna;        //'セミ・ダイナミックオブジェクト。
        public static bool m_bEnable;          //'セミ・ダイナミック補正変換の要否。
        public static long m_nCounter;         //'セミ・ダイナミック補正変換番号。
        public static bool m_bMessage;         //'セミ・ダイナミック補正変換ミスのメーっセージを表示するか？
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        Private m_sParameterFileInfo As String 'パラメータファイル情報。

        '初期化。
        '
        '引き数：
        'bCoor 補正ON/OFF。
        'sPath パラメータファイルのパス。
        Public Sub Initialize(ByVal bCorr As Boolean, ByVal sPath As String)

            If m_nSemiDyna <> 0 Then
                Call SDN_Destroy(m_nSemiDyna)
                m_nSemiDyna = 0
            End If
    
            If bCorr Then
                Call SetParameterFileInfo(sPath)
                m_nSemiDyna = SDN_Create(sPath, 2, 5, 5)
                If m_nSemiDyna = 0 Then Call Err.Raise(ERR_FATAL, , GetLastErrorMessage())
                m_nCounter = m_nCounter + 1
            End If
    
            m_bEnable = False
    
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        private string m_sParameterFileInfo;        //'パラメータファイル情報。
        /*
        '初期化。
        '
        '引き数：
        'bCoor 補正ON/OFF。
        'sPath パラメータファイルのパス。
        */
        public void Initialize(bool bCorr, string sPath)
        {
            if (m_nSemiDyna != 0)
            {
                SDN_Destroy(m_nSemiDyna);
                m_nSemiDyna = 0;
            }

            if (bCorr)
            {
                SetParameterFileInfo(sPath);
                m_nSemiDyna = SDN_Create(sPath, 2, 5, 5);
                //if (m_nSemiDyna == 0) { Err.Raise(ERR_FATAL, , GetLastErrorMessage()); }
                if (m_nSemiDyna == 0) { }
                m_nCounter = m_nCounter + 1;
            }

            m_bEnable = false;

        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '終了処理。
        Public Sub Terminate()

            If m_nSemiDyna <> 0 Then
                Call SDN_Destroy(m_nSemiDyna)
                m_nSemiDyna = 0
            End If
    
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '元期座標を今期座標へ変換。
        '
        '引き数：
        'nLatGAN 元期緯度(度)。
        'nLonGAN 元期経度(度)。
        'nHeightGAN 元期楕円体高(ｍ)。
        'nLatKON 今期緯度(度)が設定される。
        'nLonKON 今期経度(度)が設定される。
        'nHeightKON 今期楕円体高(ｍ)が設定される。
        '
        '戻り値：
        '正常終了の場合はセミ・ダイナミック補正変換番号を返す。
        '変換できなかった場合は負の値を返す。変換が行えなかった場合でも補正量を0として今期座標には値を設定する。
        Public Function ConvertGAN2KON(ByVal nLatGAN As Double, ByVal nLonGAN As Double, ByVal nHeightGAN As Double, ByRef nLatKON As Double, ByRef nLonKON As Double, ByRef nHeightKON As Double) As Long

            ConvertGAN2KON = -1
    
            If m_nSemiDyna <> 0 Then
                If 0 < SDN_ConvertGAN2KON(m_nSemiDyna, nLatGAN * 60 * 60, nLonGAN * 60 * 60, nHeightGAN, nLatKON, nLonKON, nHeightKON) Then
                    nLatKON = nLatKON / (60 * 60)
                    nLonKON = nLonKON / (60 * 60)
                    ConvertGAN2KON = m_nCounter
                    Exit Function
                End If
            End If
    
            nLatKON = nLatGAN
            nLonKON = nLonGAN
            nHeightKON = nHeightGAN
    
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '今期座標を元期座標へ変換。
        '
        '引き数：
        'nLatKON 今期緯度(度)。
        'nLonKON 今期経度(度)。
        'nHeightKON 今期楕円体高(ｍ)。
        'nLatGAN 元期緯度(度)が設定される。
        'nLonGAN 元期経度(度)が設定される。
        'nHeightGAN 元期楕円体高(ｍ)が設定される。
        '
        '戻り値：
        '正常終了の場合はセミ・ダイナミック補正変換番号を返す。
        '変換できなかった場合は負の値を返す。変換が行えなかった場合でも補正量を0として元期座標には値を設定する。
        Public Function ConvertKON2GAN(ByVal nLatKON As Double, ByVal nLonKON As Double, ByVal nHeightKON As Double, ByRef nLatGAN As Double, ByRef nLonGAN As Double, ByRef nHeightGAN As Double) As Long

            ConvertKON2GAN = -1
    
            If m_nSemiDyna <> 0 Then
                If 0 < SDN_ConvertKON2GAN(m_nSemiDyna, nLatKON * 60 * 60, nLonKON * 60 * 60, nHeightKON, nLatGAN, nLonGAN, nHeightGAN) Then
                    nLatGAN = nLatGAN / (60 * 60)
                    nLonGAN = nLonGAN / (60 * 60)
                    ConvertKON2GAN = m_nCounter
                    Exit Function
                End If
            End If
    
            nLatGAN = nLatKON
            nLonGAN = nLonKON
            nHeightGAN = nHeightKON
    
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'パラメータファイルの評価。
        '
        '引き数：
        'sPath パラメータファイルのパス。
        '
        '戻り値：
        '正常終了の場合は True を返す。
        'それ以外の場合は False を返す。
        Public Function EstimateFile(ByVal sPath As String) As Boolean

            EstimateFile = False
    
            '一度ためしに変換してみる。
            Dim nSemiDyna As Long
            nSemiDyna = SDN_Create(sPath, 1, 0, 0)
    
            If nSemiDyna = 0 Then Exit Function
    
            Dim nLat As Double
            Dim nLon As Double
            Dim nHeight As Double
            If SDN_ConvertGAN2KON(nSemiDyna, 36# * 60 * 60, 139.833333333333 * 60 * 60, 0, nLat, nLon, nHeight) = 1 Then EstimateFile = True
    
            Call SDN_Destroy(nSemiDyna)
    
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'パラメータファイル情報の設定。
        '
        '引き数：
        'sPath パラメータファイルのパス。
        Private Sub SetParameterFileInfo(ByVal sPath As String)

            Dim sDrive As String
            Dim sDir As String
            Dim sTitle As String
            Dim sExt As String
    
            Call SplitPath(sPath, sDrive, sDir, sTitle, sExt)
    
            Dim clsFile As New FileNumber
            Open sPath For Input Access Read Lock Write As #clsFile.Number
    
            Dim sBuff As String
            Line Input #clsFile.Number, sBuff
    
            m_sParameterFileInfo = sTitle & sExt & " " & Mid(sBuff, InStr(1, sBuff, "Ver"))
    
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        'パラメータファイル情報の設定。
        '
        '引き数：
        'sPath パラメータファイルのパス。
        */
        private void SetParameterFileInfo(string sPath)
        {
            return;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'パラメータファイル情報の取得。
        '
        '戻り値：
        'パラメータファイル情報を返す。
        Public Function GetParameterFileInfo() As String

            GetParameterFileInfo = m_sParameterFileInfo
    
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //==========================================================================================
    }
}
