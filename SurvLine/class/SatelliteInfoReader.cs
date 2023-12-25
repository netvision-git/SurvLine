using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.AxHost;

namespace SurvLine
{
    //23/12/24 K.setoguchi@NV---------->>>>>>>>>>
    // 下記の全てを変更
    //
    public class SatelliteInfoReader
    {

        private const string END_OF_HEADER_______ = "END OF HEADER       ";
        private const string COMMENT_____________ = "COMMENT             ";
        private const string FILE_VERSION________ = "FILE VERSION        ";
        private const string FILE_NAME___________ = "FILE NAME           ";
        private const string MARKER_NUMBER_______ = "MARKER NUMBER       ";
        private const string MARKER_NAME_________ = "MARKER NAME         ";
        private const string SESSION_ID__________ = "SESSION ID          ";
        private const string TIME_OF_FIRST_OBS___ = "TIME OF FIRST OBS   ";
        private const string WEEK_OF_FIRST_OBS___ = "WEEK OF FIRST OBS   ";
        private const string TOW_OF_FIRST_OBS____ = "TOW OF FIRST OBS    ";
        private const string TIME_OF_LAST_OBS____ = "TIME OF LAST OBS    ";
        private const string WEEK_OF_LAST_OBS____ = "WEEK OF LAST OBS    ";
        private const string TOW_OF_LAST_OBS_____ = "TOW OF LAST OBS     ";
        private const string TIME_OF_OBS_________ = "TIME OF OBS         ";
        private const string EPOCH_OF_OBS________ = "EPOCH OF OBS        ";
        private const string INTERVAL____________ = "INTERVAL            ";
        private const string ELEVATION_MASK______ = "ELEVATION MASK      ";
        private const string LEAP_SECONDS________ = "LEAP SECONDS        ";
        private const string REC_TYPE____________ = "REC TYPE            ";
        private const string REC_N_______________ = "REC #               ";
        private const string ANT_TYPE____________ = "ANT TYPE            ";
        private const string ANT_N_______________ = "ANT #               ";
        private const string ANT_MEASUREMENT_____ = "ANT MEASUREMENT     ";
        private const string ANT_HEIGHT__________ = "ANT HEIGHT          ";
        private const string ANT_DELTA_H_________ = "ANT DELTA H         ";
        private const string ANT_DELTA_E_________ = "ANT DELTA E         ";
        private const string ANT_DELTA_N_________ = "ANT DELTA N         ";
        private const string APPROX_POSITION_X___ = "APPROX POSITION X   ";
        private const string APPROX_POSITION_Y___ = "APPROX POSITION Y   ";
        private const string APPROX_POSITION_Z___ = "APPROX POSITION Z   ";
        private const string N_OF_OBSERV_________ = "# OF OBSERV         ";
        private const string TYPES_OF_OBSERV_____ = "TYPES OF OBSERV     ";
        private const string AS__________________ = "AS                  ";
        private const string N_OF_MAX_SV_________ = "# OF MAX SV         ";
        private const string N_OF_MIN_SV_________ = "# OF MIN SV         ";
        private const string N_OF_ALL_SV_________ = "# OF ALL SV         ";
        private const string SV_HEALTH___________ = "SV HEALTH           ";

        //'2017/06/23 NS6000対応。'''''''''''''''''''''''''''''''''''''''''''''''''''''''
        private const string OBS_TYPES___________ = "OBS TYPES           ";
        private const string RINEX_VERSION_______ = "RINEX VERSION       ";

        //'2007/08/21 Seillac H.Nakamura
        //'観測データでエポックそのものが存在しない部分を｢欠損｣とする。
        //'欠損が数エポックだけであれば基線ベクトルはつなげるようにする。
        private long EPOCH_LOSSES_MAX;          //As Long = 6 '最大欠損数。

        //'インプリメンテーション
        //Private m_clsFile As New FileNumber 'ファイル番号
        private long m_nLength;                 //As Long 'ファイル長。
        private bool m_bIsHeader;               //As Boolean 'ヘッダー部読み込みフラグ。True=読み込み済み。False=未読み込み。



        //'*******************************************************************************
        //'*******************************************************************************
        public void OpenFile(string SattAatePath)          //"C:\Develop\NetSurv\Src\NS-App\NS-Survey\Temp\.\ObsPoint\017H305.log"
                                                                   //"C:\Develop\NetSurv\Src\NS-App\NS-Survey\Temp\.\ObsPoint\017H305.log"
                                                                   //"C:\Develop\NetSurv\Src\NS-App\NS-Survey\Temp\.\ObsPoint\017H305.log"
        {
            //*********************
            //ファイルOPEN
            //*********************

            try
            {
                using (var fs = System.IO.File.OpenRead($"{SattAatePath}"))
                {
                    using (StreamReader sr = new StreamReader(fs))
                    {
                        return;
                    }

                }
                //System.IO.StreamReader sr = new StreamReader(sPath);
                //var sr = new StreamReader(sPath);
                //------------------------------------------------------
                //ファイルチェック
                //  if (File.Exists(sPath) == false)
                //  {
                //      MessageBox.Show("ファイルが見つかりません");
                //      return;
                //  }
                //------------------------------------------------------
                //  try
                //  {
                //      var sr = new StreamReader(sPath);
                //  }
                //  catch ( Exception ex)
                //  {
                //      MessageBox.Show(ex.Message, "エラー発生");
                //  }
                //------------------------------------------------------

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "エラー発生");
            }
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
        //'*******************************************************************************
        /// <summary>
        //'ヘッダー部のシーク。
        //'
        //'引き数：
        /// 
        /// </summary>
        /// <returns></returns>
        private bool SeekHeader()
        {
            bool m_bIsHeader = false;




            return m_bIsHeader;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sPath"></param>
        /// <returns></returns>
        private bool SeekHeader(string sSrc, ref string sDis, StreamReader sr)
        {
            sDis = sSrc;
            m_bIsHeader = false;
            while (sSrc != null)
            {
                if (sSrc.Contains($"{END_OF_HEADER_______}") == true)
                {
                    sDis = sSrc;
                    m_bIsHeader = true;
                    return m_bIsHeader;

                }
                else
                {
                    sSrc = sr.ReadLine();
                    sDis = sSrc;

                    m_bIsHeader = false;
                }
            }
            return m_bIsHeader;
        }
        //---------------------------------------------------------------------------------
        //'ヘッダー部のシーク。
        //'
        //'引き数：
        //'clsProgressInterface ProgressInterface オブジェクト。
        //Private Sub SeekHeader(ByVal clsProgressInterface As ProgressInterface)
        //    Dim sBuff As String
        //    Dim sMbcs As String
        //    Do While Not IsEOF()
        //        'プログレス。
        //        Call clsProgressInterface.CheckCancel
        //        Line Input #m_clsFile.Number, sBuff
        //        sMbcs = StrConv(sBuff, vbFromUnicode)
        //        If StrComp(MidB$(sMbcs, RNX_STR), END_OF_HEADER_______) = 0 Then Exit Do
        //    Loop
        //    m_bIsHeader = True
        //End Sub


        //'*******************************************************************************
        /// <summary>
        ///'2022/03/10 Hitz H.Nakamura
        ////'衛星信号の読み込み。
        /// </summary>
        /// <param name="nSattSignalGPS"></param>
        /// <param name="nSattSignalGLONASS"></param>
        /// <param name="nSattSignalQZSS"></param>
        /// <param name="nSattSignalGalileo"></param>
        /// <param name="nSattSignalBeiDou"></param>
        public void ReadSattSignal(string SattAatePath, ref long nSattSignalGPS, ref long nSattSignalGLONASS, ref long nSattSignalQZSS, ref long nSattSignalGalileo, ref long nSattSignalBeiDou)
        {

            nSattSignalGPS = 0;
            nSattSignalGLONASS = 0;
            nSattSignalQZSS = 0;
            nSattSignalGalileo = 0;
            nSattSignalBeiDou = 0;

            string sBuff;



            try
            {
                using (var fs = System.IO.File.OpenRead($"{SattAatePath}"))
                {

                    using (var sr = new StreamReader(fs))
                    {
                        //[VB]      'シーク。
                        //[VB]      If IsEOF() Then Call SeekTop
                        //[VB]      If Not m_bIsHeader Then Call SeekHeader(clsProgressInterface)
                        //[VB]          
                        //[VB]      Line Input #m_clsFile.Number, sBuff           //"2012/01/17,06:40:30.0000000,7"   //"2012/01/17,06:40:45.0000000,7"
                        //[VB]          
                        //[VB]      Do While Not IsEOF()
                        //[VB]          //'プログレス。
                        //[VB]          //Call clsProgressInterface.CheckCancel     //DUMMY
                        //[VB]          //        
                        //[VB]          //'ヘッダー。
                        //[VB]          //Dim clsTokenizer As New StringTokenizer
                        //[VB]          //clsTokenizer.Source = sBuff
                        //[VB]          //Call clsTokenizer.Begin
                        //[VB]          //Dim sToken As String
                        //[VB]          //'日付。
                        //[VB]          //

                        if ((sBuff = sr.ReadLine()) != null)
                        {
                            //[VB]  Call SeekTop
                        }
                        if (SeekHeader(sBuff, ref sBuff, sr) == false)
                        {
                            return;
                        }


                        int n = 0;
                        //string line = null;

                        while ((sBuff = sr.ReadLine()) != null)     //  (Ex.)"2012/01/17,06:40:30.0000000,7"  / "2012/01/17,06:40:45.0000000,7"
                        {
                            //--------------------------------------------------
                            //        'プログレス。
                            //        Call clsProgressInterface.CheckCancel     //DUMMY

                            //        'ヘッダー。
                            //        Dim clsTokenizer As New StringTokenizer
                            //        clsTokenizer.Source = sBuff
                            //        Call clsTokenizer.Begin
                            StringTokenizer stringTokenizer = new StringTokenizer();
                            stringTokenizer.Source = sBuff;
                            stringTokenizer.Begin();

                            //--------------------------------------------------
                            //        Dim sToken As String
                            //        '日付。
                            //        sToken = clsTokenizer.NextToken           //"2012/01/17,06"  
                            //        sToken = clsTokenizer.NextToken           //"06:40:30.0000000"
                            string sToken;
                            sToken = stringTokenizer.NextToken();
                            sToken = stringTokenizer.NextToken();


                            //------------------------------------
                            //        '衛星数。
                            //        Dim nCount As Long
                            //        nCount = Val(clsTokenizer.NextToken)      //7                         //7
                            //        If nCount < 0 Then Call Err.Raise(ERR_FILE, , MSG_SATELLITEINFO_INVALID)
                            long nCount;
                            string sCount;
                            sCount = stringTokenizer.NextToken();
                            nCount = long.Parse(sCount);
                            if (nCount < 0 ){
                                //Call Err.Raise(ERR_FILE, , MSG_SATELLITEINFO_INVALID)
                            }

                            //------------------------------------
                            //        
                            //        '有効衛星。
                            //        Do While Not IsEOF()
                            //            'プログレス。
                            //            Call clsProgressInterface.CheckCancel
                            //
                            //            Line Input #m_clsFile.Number, sBuff       //" 2,,,1,121522868.309,92875425.898,48.0,41.0,0,0"
                            //
                            while ((sBuff = sr.ReadLine()) != null)
                            {


                                //            '先頭が空白でなければヘッダーである。
                                //            If Asc(sBuff) <> &H20& Then Exit Do
                                if (sBuff.Substring(0, 1) != " ")
                                {
                                    return;
                                }
                                //
                                //            clsTokenizer.Source = sBuff               //" 2,,,1,121522868.309,92875425.898,48.0,41.0,0,0"
                                //            Call clsTokenizer.Begin
                                //
                                stringTokenizer.Source = sBuff;
                                stringTokenizer.Begin();


                                //            '衛星番号。
                                //            Dim nNumber As Long
                                //            nNumber = Val(clsTokenizer.NextToken)     //2     //5
                                //            nNumber = ConvertNumber(nNumber)          //2     //5
                                //
                                //            sToken = clsTokenizer.NextToken '仰角。  //""    //""
                                //            sToken = clsTokenizer.NextToken '方位角。//""    //""
                                //
                                long nNumber;
                                nNumber = long.Parse(stringTokenizer.NextToken());

                                sToken = stringTokenizer.NextToken();           //'仰角。  //""    //""
                                sToken = stringTokenizer.NextToken();           //'方位角。//""    //""


                                //            '衛星健康状態。
                                //            Dim bHealth As Boolean
                                //            bHealth = (Val(clsTokenizer.NextToken) = 1)
                                //
                                //            If bHealth Then
                                //
                                bool bHealth = false;
                                if (long.Parse(stringTokenizer.NextToken()) >= 1)
                                {
                                    bHealth = true;
                                }

                                if (bHealth) {
                                    sToken = stringTokenizer.NextToken(); //'L1位相。    //"121522868.309"   //"113486554.609"
                                    sToken = stringTokenizer.NextToken(); //'L2位相。    //"92875425.898"    //"86613368.191"
                                    sToken = stringTokenizer.NextToken(); //'L1SNR。       //"48.0"          //"51.0"
                                    sToken = stringTokenizer.NextToken(); //'L2SNR。       //"41.0"          //"46.0"
                                    sToken = stringTokenizer.NextToken(); //'L1SLIP。      //"0"
                                    sToken = stringTokenizer.NextToken(); //'L2SLIP。      //"0"
                                    sToken = stringTokenizer.NextToken(); //'L5位相。        //""
                                    sToken = stringTokenizer.NextToken(); //'L5SNR。       //""
                                    sToken = stringTokenizer.NextToken(); //'L5SLIP。      //""


                                    //                
                                    //                '1～32＝GPSの1～32番
                                    //                '38～63＝GLONASSの1～26番
                                    //                '65～72＝QZSSの1～8番
                                    //                '73～108＝Galileoの1～36番
                                    //                '109～145＝BeiDouの1～37番
                                    //                '146～204＝SBASの1～59番
                                    //                '205≦不明。


                                    string sObs;
                                    //  'L1信号。---------------------------------------------
                                    sObs = stringTokenizer.NextToken();         //""
                                    if (nNumber < 38) {              //2
                                        if (sObs != "") {
                                            nSattSignalGPS = nSattSignalGPS | 0x0L;    //""
                                        }
                                    } else if (nNumber < 65) {
                                        if (sObs != "") {
                                            nSattSignalGLONASS = nSattSignalGLONASS | 0x0L;
                                        }
                                    } else if (nNumber < 73) {
                                        if (sObs != "") {
                                            nSattSignalQZSS = nSattSignalQZSS | 0x0L;
                                        }
                                    } else if (nNumber < 109) {
                                        if (sObs != "") {
                                            nSattSignalGalileo = nSattSignalGalileo | 0x0L;
                                        }
                                    } else if (nNumber < 146) {
                                        if (sObs != "") {
                                            nSattSignalBeiDou = nSattSignalBeiDou | 0x0L;
                                        }
                                    }

                                    //'L2信号。
                                    sObs = stringTokenizer.NextToken();         //""
                                    if (nNumber < 38) {
                                        if (sObs != "") {
                                            nSattSignalGPS = nSattSignalGPS | 0x2L;
                                        }
                                    } else if (nNumber < 65) {
                                        if (sObs != ""){
                                            nSattSignalGLONASS = nSattSignalGLONASS | 0x2L;
                                        }
                                    } else if (nNumber < 73) {
                                        if (sObs != ""){
                                            nSattSignalQZSS = nSattSignalQZSS | 0x2L;
                                        }
                                    }else if (nNumber < 109){
                                    }else if (nNumber < 146){
                                        if (sObs != ""){
                                            nSattSignalBeiDou = nSattSignalBeiDou | 0x2L;
                                        }
                                    }


                                    //'L5信号。
                                    sObs = stringTokenizer.NextToken();
                                    if (nNumber < 38){
                                        if (sObs != ""){
                                            nSattSignalGPS = nSattSignalGPS | 0x4L;
                                        }
                                    }else if (nNumber< 65){
                                        if (sObs != ""){
                                            nSattSignalGLONASS = nSattSignalGLONASS | 0x4L;
                                        }
                                    }else if (nNumber< 73){
                                        if (sObs != ""){
                                            nSattSignalQZSS = nSattSignalQZSS | 0x4L;
                                        }
                                    }else if (nNumber< 109){
                                        if (sObs != ""){
                                            //{VB]      nSattSignalGalileo = nSattSignalGalileo Or &H2
                                            nSattSignalGalileo = nSattSignalGalileo | 0x4L;
                                        }
                                    }else if (nNumber< 146){
                                        if (sObs != ""){
                                            nSattSignalBeiDou = nSattSignalBeiDou | 0x4L;
                                        }
                                    }



                                }   //if( bHealth ){---------------------------------------------

                            }

                        }



                        }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "エラーが発生");
                return;
            }

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
        //    Line Input #m_clsFile.Number, sBuff           //"2012/01/17,06:40:30.0000000,7"   //"2012/01/17,06:40:45.0000000,7"
        //    
        //    Do While Not IsEOF()
        //        'プログレス。
        //        Call clsProgressInterface.CheckCancel     //DUMMY
        //        
        //        'ヘッダー。
        //        Dim clsTokenizer As New StringTokenizer
        //        clsTokenizer.Source = sBuff
        //        Call clsTokenizer.Begin
        //        Dim sToken As String
        //        '日付。
        //        sToken = clsTokenizer.NextToken           //"2012/01/17,06"           //"2012/01/17,06:40:45.0000000,7"
        //        sToken = clsTokenizer.NextToken           //"06:40:30.0000000"        //"06:40:45.0000000"
        //        '衛星数。
        //        Dim nCount As Long
        //        nCount = Val(clsTokenizer.NextToken)      //7                         //7
        //        If nCount < 0 Then Call Err.Raise(ERR_FILE, , MSG_SATELLITEINFO_INVALID)
        //        
        //        '有効衛星。
        //        Do While Not IsEOF()
        //            'プログレス。
        //            Call clsProgressInterface.CheckCancel
        //
        //            Line Input #m_clsFile.Number, sBuff       //" 2,,,1,121522868.309,92875425.898,48.0,41.0,0,0"
        //------------------------------------------------------//" 5,,,1,113486554.609,86613368.191,51.0,46.0,0,0"
        //------------------------------------------------------//" 8,,,1,124173573.785,94940923.879,47.0,39.0,0,0"
        //------------------------------------------------------//" 15,,,1,116025744.602,88591965.926,50.0,44.0,0,0"
        //------------------------------------------------------//" 26,,,1,108597383.398,82803628.523,51.0,46.0,0,0"
        //------------------------------------------------------//" 27,,,1,120677411.598,90982025.316,47.0,39.0,0,0"
        //------------------------------------------------------//" 29,,,1,128767319.328,98520462.227,45.0,38.0,0,0"



        //            '先頭が空白でなければヘッダーである。
        //            If Asc(sBuff) <> &H20& Then Exit Do
        //
        //            clsTokenizer.Source = sBuff               //" 2,,,1,121522868.309,92875425.898,48.0,41.0,0,0"
        //------------------------------------------------------//" 5,,,1,113486554.609,86613368.191,51.0,46.0,0,0"
        //------------------------------------------------------//" 15,,,1,116025744.602,88591965.926,50.0,44.0,0,0"

        //            Call clsTokenizer.Begin
        //            
        //            '衛星番号。
        //            Dim nNumber As Long
        //            nNumber = Val(clsTokenizer.NextToken)     //2     //5
        //            nNumber = ConvertNumber(nNumber)          //2     //5
        //
        //
        //            sToken = clsTokenizer.NextToken '仰角。  //""    //""
        //            sToken = clsTokenizer.NextToken '方位角。//""    //""
        //            
        //            '衛星健康状態。
        //            Dim bHealth As Boolean
        //            bHealth = (Val(clsTokenizer.NextToken) = 1)
        //
        //
        //            If bHealth Then
        //                sToken = clsTokenizer.NextToken 'L1位相。    //"121522868.309"   //"113486554.609"
        //                sToken = clsTokenizer.NextToken 'L2位相。    //"92875425.898"    //"86613368.191"
        //                sToken = clsTokenizer.NextToken 'L1SNR。       //"48.0"          //"51.0"
        //                sToken = clsTokenizer.NextToken 'L2SNR。       //"41.0"          //"46.0"
        //                sToken = clsTokenizer.NextToken 'L1SLIP。      //"0"
        //                sToken = clsTokenizer.NextToken 'L2SLIP。      //"0"
        //                sToken = clsTokenizer.NextToken 'L5位相。        //""
        //                sToken = clsTokenizer.NextToken 'L5SNR。       //""
        //                sToken = clsTokenizer.NextToken 'L5SLIP。      //""
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
        //                sObs = clsTokenizer.NextToken     //""
        //                If nNumber < 38 Then              //2
        //                    If sObs<> "" Then
        //                        nSattSignalGPS = nSattSignalGPS Or &H1    //""
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
    //<<<<<<<<<-----------23/12/24 K.setoguchi@NV

}
