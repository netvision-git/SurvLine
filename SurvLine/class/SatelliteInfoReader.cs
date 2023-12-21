using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SurvLine
{
    public class SatelliteInfoReader
    {


        //'*******************************************************************************
        //'*******************************************************************************
        public void OpenFile(string sPath)
        {

        }
        //---------------------------------------------------------------------------------
        //'メソッド
        //
        //'衛星情報ファイルを開く。
        //'
        //'引き数：
        //'sPath 衛星情報ファイルのパス。
        //Public Sub OpenFile(ByVal sPath As String)
        //    Open sPath For Input Access Read Lock Write As #m_clsFile.Number
        //    m_nLength = LOF(m_clsFile.Number)
        //    m_bIsHeader = False
        //End Sub
        //'*******************************************************************************
        //'*******************************************************************************



        //'*******************************************************************************
        public void ReadSattSignal(long nSattSignalGPS, long nSattSignalGLONASS, long nSattSignalQZSS, long nSattSignalGalileo, long nSattSignalBeiDou)
        {

        }
        //'2022/03/10 Hitz H.Nakamura
        //'衛星信号の読み込み。
        //'
        //'引き数：
        //'nSattSignalGPS     GPS衛星信号を設定する。    ビットフラグ。0x01＝L1、0x02＝L2、0x04＝L5。
        //'nSattSignalGLONASS GLONASS衛星信号を設定する。ビットフラグ。0x01＝G1、0x02＝G2、0x04＝G3。
        //'nSattSignalQZSS    QZSS衛星信号を設定する。   ビットフラグ。0x01＝L1、0x02＝L2、0x04＝L5。
        //'nSattSignalGalileo Galileo衛星信号を設定する。ビットフラグ。0x01＝E1、0x02＝E5。
        //'nSattSignalBeiDou  BeiDou衛星信号を設定する。 ビットフラグ。0x01＝B1、0x02＝B2、0x04＝B3。
        //'clsProgressInterface ProgressInterface オブジェクト。
        //Public Sub ReadSattSignal(ByRef nSattSignalGPS As Long, ByRef nSattSignalGLONASS As Long, ByRef nSattSignalQZSS As Long, ByRef nSattSignalGalileo As Long, ByRef nSattSignalBeiDou As Long, ByVal clsProgressInterface As ProgressInterface)
        //
        //    nSattSignalGPS = 0
        //    nSattSignalGLONASS = 0
        //    nSattSignalQZSS = 0
        //    nSattSignalGalileo = 0
        //    nSattSignalBeiDou = 0
        //    
        //    'シーク。
        //    If IsEOF() Then Call SeekTop
        //        If Not m_bIsHeader Then Call SeekHeader(clsProgressInterface)
        //    
        //    '最初の行。
        //    If IsEOF() Then Exit Sub
        //        Dim sBuff As String
        //    Line Input #m_clsFile.Number, sBuff
        //    
        //    Do While Not IsEOF()
        //        'プログレス。
        //        Call clsProgressInterface.CheckCancel
        //        
        //        'ヘッダー。
        //        Dim clsTokenizer As New StringTokenizer
        //        clsTokenizer.Source = sBuff
        //        Call clsTokenizer.Begin
        //        Dim sToken As String
        //        '日付。
        //        sToken = clsTokenizer.NextToken
        //        sToken = clsTokenizer.NextToken
        //        '衛星数。
        //        Dim nCount As Long
        //        nCount = Val(clsTokenizer.NextToken)
        //        If nCount < 0 Then Call Err.Raise(ERR_FILE, , MSG_SATELLITEINFO_INVALID)
        //        
        //        '有効衛星。
        //        Do While Not IsEOF()
        //            'プログレス。
        //            Call clsProgressInterface.CheckCancel
        //
        //            Line Input #m_clsFile.Number, sBuff
        //            '先頭が空白でなければヘッダーである。
        //            If Asc(sBuff) <> &H20& Then Exit Do
        //
        //            clsTokenizer.Source = sBuff
        //            Call clsTokenizer.Begin
        //            
        //            '衛星番号。
        //            Dim nNumber As Long
        //            nNumber = Val(clsTokenizer.NextToken)
        //            nNumber = ConvertNumber(nNumber)
        //
        //
        //            sToken = clsTokenizer.NextToken '仰角。
        //            sToken = clsTokenizer.NextToken '方位角。
        //            
        //            '衛星健康状態。
        //            Dim bHealth As Boolean
        //            bHealth = (Val(clsTokenizer.NextToken) = 1)
        //
        //
        //            If bHealth Then
        //                sToken = clsTokenizer.NextToken 'L1位相。
        //                sToken = clsTokenizer.NextToken 'L2位相。
        //                sToken = clsTokenizer.NextToken 'L1SNR。
        //                sToken = clsTokenizer.NextToken 'L2SNR。
        //                sToken = clsTokenizer.NextToken 'L1SLIP。
        //                sToken = clsTokenizer.NextToken 'L2SLIP。
        //                sToken = clsTokenizer.NextToken 'L5位相。
        //                sToken = clsTokenizer.NextToken 'L5SNR。
        //                sToken = clsTokenizer.NextToken 'L5SLIP。
        //                
        //                '1～32＝GPSの1～32番
        //                '38～63＝GLONASSの1～26番
        //                '65～72＝QZSSの1～8番
        //                '73～108＝Galileoの1～36番
        //                '109～145＝BeiDouの1～37番
        //                '146～204＝SBASの1～59番
        //                '205≦不明。
        //
        //
        //                Dim sObs As String
        //                'L1信号。
        //                sObs = clsTokenizer.NextToken
        //                If nNumber < 38 Then
        //                    If sObs<> "" Then
        //                        nSattSignalGPS = nSattSignalGPS Or &H1
        //                    End If
        //                ElseIf nNumber< 65 Then
        //                    If sObs<> "" Then
        //                        nSattSignalGLONASS = nSattSignalGLONASS Or &H1
        //                    End If
        //                ElseIf nNumber< 73 Then
        //                    If sObs<> "" Then
        //                        nSattSignalQZSS = nSattSignalQZSS Or &H1
        //                    End If
        //                ElseIf nNumber< 109 Then
        //                    If sObs<> "" Then
        //                        nSattSignalGalileo = nSattSignalGalileo Or &H1
        //                    End If
        //                ElseIf nNumber< 146 Then
        //                    If sObs<> "" Then
        //                        nSattSignalBeiDou = nSattSignalBeiDou Or &H1
        //                    End If
        //                End If
        //                'L2信号。
        //                sObs = clsTokenizer.NextToken
        //                If nNumber< 38 Then
        //                    If sObs<> "" Then
        //                        nSattSignalGPS = nSattSignalGPS Or &H2
        //                    End If
        //                ElseIf nNumber< 65 Then
        //                    If sObs<> "" Then
        //                        nSattSignalGLONASS = nSattSignalGLONASS Or &H2
        //                    End If
        //                ElseIf nNumber< 73 Then
        //                    If sObs<> "" Then
        //                        nSattSignalQZSS = nSattSignalQZSS Or &H2
        //                    End If
        //                ElseIf nNumber< 109 Then
        //                ElseIf nNumber< 146 Then
        //                    If sObs<> "" Then
        //                        nSattSignalBeiDou = nSattSignalBeiDou Or &H2
        //                    End If
        //                End If
        //                'L5信号。
        //                sObs = clsTokenizer.NextToken
        //                If nNumber< 38 Then
        //                    If sObs<> "" Then
        //                        nSattSignalGPS = nSattSignalGPS Or &H4
        //                    End If
        //                ElseIf nNumber< 65 Then
        //                    If sObs<> "" Then
        //                        nSattSignalGLONASS = nSattSignalGLONASS Or &H4
        //                    End If
        //                ElseIf nNumber< 73 Then
        //                    If sObs<> "" Then
        //                        nSattSignalQZSS = nSattSignalQZSS Or &H4
        //                    End If
        //                ElseIf nNumber< 109 Then
        //                    If sObs<> "" Then
        //                        nSattSignalGalileo = nSattSignalGalileo Or &H2
        //                    End If
        //                ElseIf nNumber< 146 Then
        //                    If sObs<> "" Then
        //                        nSattSignalBeiDou = nSattSignalBeiDou Or &H4
        //                    End If
        //                End If
        //            End If
        //
        //
        //            nCount = nCount - 1
        //        Loop
        //        If nCount<> 0 Then Call Err.Raise(ERR_FILE, , MSG_SATELLITEINFO_INVALID)
        //    Loop
        //
        //
        //End Sub




    }
}
