using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Windows.Forms;

namespace SurvLine
{
    internal class SproObject
    {
        //==========================================================================================
        /*[VB]
        '*******************************************************************************
        'SuperPro オブジェクト

        Option Explicit
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        Private Datain As DATAQUERY
        Private Dataout As DATAQUERY
        Private ApiPack As APIPACKET

        Private Const dID As Integer = &HA2B3
        Private IsInitialized As Byte 'will tell whether the apipkt is initialized or not

        Private m_bFind As Boolean
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
#if false
        private DATAQUERY Datain;
        private DATAQUERY Dataout;
        private APIPACKET ApiPack;
#endif
        private const int dID = 0xA2B3;
        private byte IsInitialized;         //'will tell whether the apipkt is initialized or not

        private bool m_bFind;
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '*******************************************************************************
        'イベント

        Private Sub SproObject()
            Call Destroy
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '*******************************************************************************
        'メソッド

        Public Function SproObject(ByVal clsSproData As SproDataInterface) As Boolean

            If SPRO_ENABLE Then
                Check = False
            Else
                Check = True
                Exit Function
            End If


            Dim i As Long
            For i = 0 To 1
                If Not m_bFind Then
                    'レコードの初期化
                    If Not SproObject() Then Exit Function
                    IsInitialized = 1
                    'パケットの初期化
                    If Not SproObject() Then Exit Function
                    'コンタクトサーバをスタンドアローンに設定
                    If Not SproObject() Then Exit Function
                    'キーの確認
                    '正しいキーが見つかった場合、同じAPIパケットでFindFirstUnitを複数回コールすることは出来ません。
                    If Not SproObject() Then Exit Function

                    m_bFind = True
                End If
        
                '複数のキーをチェックしに行くように変更 2006/11/07 NGS Yamada
                Do
                    Dim strRet As String
                    Dim intRnd As Integer
                    intRnd = clsSproData.GetRnd()
                    Call Query(clsSproData.Addr, clsSproData.Query(intRnd), strRet)
                    If StrComp(strRet, clsSproData.Response(intRnd)) = 0 Then
                        Check = True
                        Exit Function
                    End If
                Loop While FindNext
        
                '再初期化してもう１回ためす。
                m_bFind = False
                If IsInitialized = 1 Then
                    Call ReleaseLicense
                    IsInitialized = 0
                End If
                Call RNBOsproCleanup
            Next

        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '2006/12/1 NGS Yamada
        Public Function SproObject() As String
            Dim i As Integer
            Dim sOut As String


            Serial = ""
    
            If Not m_bFind Then
                'レコードの初期化
                If Not SproObject() Then Exit Function
                IsInitialized = 1
                'パケットの初期化
                If Not SproObject() Then Exit Function
                'コンタクトサーバをスタンドアローンに設定
                If Not SproObject() Then Exit Function
                'キーの確認
                '正しいキーが見つかった場合、同じAPIパケットでFindFirstUnitを複数回コールすることは出来ません。
                If Not SproObject() Then Exit Function

                m_bFind = True
            End If


            sOut = SproObject(Read(&H0))
    
            For i = Len(sOut) To 3  '４桁無い場合は「0」で埋める
                sOut = "0" & sOut
            Next i

            Serial = sOut


            m_bFind = False
            Call RNBOsproCleanup

        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '2006/12/1 NGS Yamada追加
        Private Function SproObject(Addr As Integer) As String
            Dim intRet As Integer
            Dim Datum As Integer


            Read = 0
            intRet = SproObject(ApiPack, Addr, Datum)
            If intRet = 0 Then
                Read = Datum
            End If
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        Public Sub SproObject()
            m_bFind = False
            If IsInitialized = 1 Then
                Call ReleaseLicense
                IsInitialized = 0
            End If
            Call RNBOsproCleanup
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '*******************************************************************************
        'インプリメンテーション

        Private Function SproObject() As Boolean
            'first chk if the pkt has been initialized, if so release the mem
            Dim ApiSize As Integer
            Dim intRet As Integer


            FormatPacket = False

            If IsInitialized = 1 Then
               IsInitialized = 0
               intRet = RNBOsproReleaseLicense%(ApiPack, 0, 0)
            End If


            ApiSize = 4096
            intRet = SproObject(ApiPack, ApiSize)
            If intRet = 0 Then
                FormatPacket = True
            End If
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        Private Function SproObject() As Boolean
            Dim intRet As Integer


            Initialize = False

            intRet = RNBOsproInitialize(ApiPack)
            If intRet = 0 Then
                Initialize = True
            End If
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        Private Function SproObject() As Boolean
            Dim intRet As Integer


            SetContactServer = False

           intRet = RNBOsproSetContactServer(ApiPack, RNBO_SPN_DRIVER)
            If intRet = 0 Then
                SetContactServer = True
            End If
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        Private Function SproObject() As Boolean
            Dim intRet As Integer


            FindFirst = False

           intRet = RNBOsproFindFirstUnit(ApiPack, dID)
            If intRet = 0 Then
                FindFirst = True
            End If
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        ' 2006/11/7 NGS Yamada
        Private Function SproObject() As Boolean
            Dim intRet As Integer


            FindNext = False

           intRet = RNBOsproFindNextUnit(ApiPack)
            If intRet = 0 Then
                FindNext = True
            End If
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        Private Function SproObject() As Boolean
            Dim intRet As Integer


            intRet = SproObject(ApiPack, 0, 0)
            If intRet = 0 Then
                ReleaseLicense = True
            End If
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        Private Function SproObject(Addr As Integer, strIn As String, strOut As String) As Boolean
            Dim i As Integer
            Dim j As Integer
            Dim intRet As Integer
            Dim leng As Integer
            Dim strHex As String

            Query = False

            strIn = UCase(strIn)         'make it a big deal


            j = 0      'now compress ASCII chars to hex (2 to 1)
            For i = 1 To Len(strIn) - 1 Step 2
                j = j + 1     'j=index to result string
                Datain.data(j - 1) = SproObject(Mid$(strIn, i, 2))
            Next

            leng = Len(strIn) / 2


            intRet = SproObject(ApiPack, Addr, Datain, Dataout, 0, leng)


            If intRet = 0 Then 'SP_SUCCESS
                strOut = ""
                For i = 0 To leng - 1
                    strHex = SproObject(Dataout.data(i))
                    strOut = strOut & SproObject(Len(strHex) = 1, "0" & strHex, strHex)
                Next
            End If

            Query = True
        End Function

        Private Function SproObject(Value As String) As Long

            Convert16to10 = SproObject("&H" & Value)

        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //==========================================================================================
    }
}
