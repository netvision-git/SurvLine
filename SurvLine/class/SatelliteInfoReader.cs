using SurvLine.mdl;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Remoting;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.AxHost;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace SurvLine
{
    //23/12/24 K.setoguchi@NV---------->>>>>>>>>>
    // 下記の全てを変更
    //
    public class SatelliteInfoReader
    {
        //24/02/21 K.setoguchi@NV---------->>>>>>>>>>
        //***************************************************************************

        MdiVBfunctions mdiVBfunctions = new MdiVBfunctions();

        //'*******************************************************************************
        //'衛星情報読み込み
        //
        //Option Explicit
        //


        //==========================================================================================
        /*[VB]
            Private Const MSG_SATELLITEINFO_INVALID = "衛星情報ファイルの内容が不正です。"

            Private END_OF_HEADER_______ As String
            Private COMMENT_____________ As String
            Private FILE_VERSION________ As String
            Private FILE_NAME___________ As String
            Private MARKER_NUMBER_______ As String
            Private MARKER_NAME_________ As String
            Private SESSION_ID__________ As String
            Private TIME_OF_FIRST_OBS___ As String
            Private WEEK_OF_FIRST_OBS___ As String
            Private TOW_OF_FIRST_OBS____ As String
            Private TIME_OF_LAST_OBS____ As String
            Private WEEK_OF_LAST_OBS____ As String
            Private TOW_OF_LAST_OBS_____ As String
            Private TIME_OF_OBS_________ As String
            Private EPOCH_OF_OBS________ As String
            Private INTERVAL____________ As String
            Private ELEVATION_MASK______ As String
            Private LEAP_SECONDS________ As String
            Private REC_TYPE____________ As String
            Private REC_N_______________ As String
            Private ANT_TYPE____________ As String
            Private ANT_N_______________ As String
            Private ANT_MEASUREMENT_____ As String
            Private ANT_HEIGHT__________ As String
            Private ANT_DELTA_H_________ As String
            Private ANT_DELTA_E_________ As String
            Private ANT_DELTA_N_________ As String
            Private APPROX_POSITION_X___ As String
            Private APPROX_POSITION_Y___ As String
            Private APPROX_POSITION_Z___ As String
            Private N_OF_OBSERV_________ As String
            Private TYPES_OF_OBSERV_____ As String
            Private AS__________________ As String
            Private N_OF_MAX_SV_________ As String
            Private N_OF_MIN_SV_________ As String
            Private N_OF_ALL_SV_________ As String
            Private SV_HEALTH___________ As String

            '2017/06/23 NS6000対応。'''''''''''''''''''''''''''''''''''''''''''''''''''''''
            Private OBS_TYPES___________ As String
            Private RINEX_VERSION_______ As String
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

            '2007/08/21 Seillac H.Nakamura
            '観測データでエポックそのものが存在しない部分を｢欠損｣とする。
            '欠損が数エポックだけであれば基線ベクトルはつなげるようにする。
            Private Const EPOCH_LOSSES_MAX As Long = 6 '最大欠損数。

            'インプリメンテーション
            Private m_clsFile As New FileNumber 'ファイル番号。
            Private m_nLength As Long 'ファイル長。
            Private m_bIsHeader As Boolean 'ヘッダー部読み込みフラグ。True=読み込み済み。False=未読み込み。

            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]

        private string END_OF_HEADER_______ = "END OF HEADER       ";
        private string COMMENT_____________ = "COMMENT             ";
        private string FILE_VERSION________ = "FILE VERSION        ";
        private string FILE_NAME___________ = "FILE NAME           ";
        private string MARKER_NUMBER_______ = "MARKER NUMBER       ";
        private string MARKER_NAME_________ = "MARKER NAME         ";
        private string SESSION_ID__________ = "SESSION ID          ";
        private string TIME_OF_FIRST_OBS___ = "TIME OF FIRST OBS   ";
        private string WEEK_OF_FIRST_OBS___ = "WEEK OF FIRST OBS   ";
        private string TOW_OF_FIRST_OBS____ = "TOW OF FIRST OBS    ";
        private string TIME_OF_LAST_OBS____ = "TIME OF LAST OBS    ";
        private string WEEK_OF_LAST_OBS____ = "WEEK OF LAST OBS    ";
        private string TOW_OF_LAST_OBS_____ = "TOW OF LAST OBS     ";
        private string TIME_OF_OBS_________ = "TIME OF OBS         ";
        private string EPOCH_OF_OBS________ = "EPOCH OF OBS        ";
        private string INTERVAL____________ = "INTERVAL            ";
        private string ELEVATION_MASK______ = "ELEVATION MASK      ";
        private string LEAP_SECONDS________ = "LEAP SECONDS        ";
        private string REC_TYPE____________ = "REC TYPE            ";
        private string REC_N_______________ = "REC #               ";
        private string ANT_TYPE____________ = "ANT TYPE            ";
        private string ANT_N_______________ = "ANT #               ";
        private string ANT_MEASUREMENT_____ = "ANT MEASUREMENT     ";
        private string ANT_HEIGHT__________ = "ANT HEIGHT          ";
        private string ANT_DELTA_H_________ = "ANT DELTA H         ";
        private string ANT_DELTA_E_________ = "ANT DELTA E         ";
        private string ANT_DELTA_N_________ = "ANT DELTA N         ";
        private string APPROX_POSITION_X___ = "APPROX POSITION X   ";
        private string APPROX_POSITION_Y___ = "APPROX POSITION Y   ";
        private string APPROX_POSITION_Z___ = "APPROX POSITION Z   ";
        private string N_OF_OBSERV_________ = "# OF OBSERV         ";
        private string TYPES_OF_OBSERV_____ = "TYPES OF OBSERV     ";
        private string AS__________________ = "AS                  ";
        private string N_OF_MAX_SV_________ = "# OF MAX SV         ";
        private string N_OF_MIN_SV_________ = "# OF MIN SV         ";
        private string N_OF_ALL_SV_________ = "# OF ALL SV         ";
        private string SV_HEALTH___________ = "SV HEALTH           ";

        //'2017/06/23 NS6000対応。'''''''''''''''''''''''''''''''''''''''''''''''''''''''
        private string OBS_TYPES___________ = "OBS TYPES           ";
        private string RINEX_VERSION_______ = "RINEX VERSION       ";

        //'2007/08/21 Seillac H.Nakamura
        //'観測データでエポックそのものが存在しない部分を｢欠損｣とする。
        //'欠損が数エポックだけであれば基線ベクトルはつなげるようにする。
        private long EPOCH_LOSSES_MAX;          //As Long = 6 '最大欠損数。

        //'インプリメンテーション
        //Private m_clsFile As New FileNumber 'ファイル番号
        private long m_nLength;                 //As Long 'ファイル長。
        private bool m_bIsHeader;               //As Boolean 'ヘッダー部読み込みフラグ。True=読み込み済み。False=未読み込み。



        public SatelliteInfoReader()
        {
            Class_Initialize();
        }

        //'*******************************************************************************
        //'イベント


        //==========================================================================================
        /*[VB]
              '初期化。
            Sub Class_Initialize()

                On Error GoTo ErrorHandler
    
                END_OF_HEADER_______ = StrConv("END OF HEADER       ", vbFromUnicode)
                COMMENT_____________ = StrConv("COMMENT             ", vbFromUnicode)
                FILE_VERSION________ = StrConv("FILE VERSION        ", vbFromUnicode)
                FILE_NAME___________ = StrConv("FILE NAME           ", vbFromUnicode)
                MARKER_NUMBER_______ = StrConv("MARKER NUMBER       ", vbFromUnicode)
                MARKER_NAME_________ = StrConv("MARKER NAME         ", vbFromUnicode)
                SESSION_ID__________ = StrConv("SESSION ID          ", vbFromUnicode)
                TIME_OF_FIRST_OBS___ = StrConv("TIME OF FIRST OBS   ", vbFromUnicode)
                WEEK_OF_FIRST_OBS___ = StrConv("WEEK OF FIRST OBS   ", vbFromUnicode)
                TOW_OF_FIRST_OBS____ = StrConv("TOW OF FIRST OBS    ", vbFromUnicode)
                TIME_OF_LAST_OBS____ = StrConv("TIME OF LAST OBS    ", vbFromUnicode)
                WEEK_OF_LAST_OBS____ = StrConv("WEEK OF LAST OBS    ", vbFromUnicode)
                TOW_OF_LAST_OBS_____ = StrConv("TOW OF LAST OBS     ", vbFromUnicode)
                TIME_OF_OBS_________ = StrConv("TIME OF OBS         ", vbFromUnicode)
                EPOCH_OF_OBS________ = StrConv("EPOCH OF OBS        ", vbFromUnicode)
                INTERVAL____________ = StrConv("INTERVAL            ", vbFromUnicode)
                ELEVATION_MASK______ = StrConv("ELEVATION MASK      ", vbFromUnicode)
                LEAP_SECONDS________ = StrConv("LEAP SECONDS        ", vbFromUnicode)
                REC_TYPE____________ = StrConv("REC TYPE            ", vbFromUnicode)
                REC_N_______________ = StrConv("REC #               ", vbFromUnicode)
                ANT_TYPE____________ = StrConv("ANT TYPE            ", vbFromUnicode)
                ANT_N_______________ = StrConv("ANT #               ", vbFromUnicode)
                ANT_MEASUREMENT_____ = StrConv("ANT MEASUREMENT     ", vbFromUnicode)
                ANT_HEIGHT__________ = StrConv("ANT HEIGHT          ", vbFromUnicode)
                ANT_DELTA_H_________ = StrConv("ANT DELTA H         ", vbFromUnicode)
                ANT_DELTA_E_________ = StrConv("ANT DELTA E         ", vbFromUnicode)
                ANT_DELTA_N_________ = StrConv("ANT DELTA N         ", vbFromUnicode)
                APPROX_POSITION_X___ = StrConv("APPROX POSITION X   ", vbFromUnicode)
                APPROX_POSITION_Y___ = StrConv("APPROX POSITION Y   ", vbFromUnicode)
                APPROX_POSITION_Z___ = StrConv("APPROX POSITION Z   ", vbFromUnicode)
                N_OF_OBSERV_________ = StrConv("# OF OBSERV         ", vbFromUnicode)
                TYPES_OF_OBSERV_____ = StrConv("TYPES OF OBSERV     ", vbFromUnicode)
                AS__________________ = StrConv("AS                  ", vbFromUnicode)
                N_OF_MAX_SV_________ = StrConv("# OF MAX SV         ", vbFromUnicode)
                N_OF_MIN_SV_________ = StrConv("# OF MIN SV         ", vbFromUnicode)
                N_OF_ALL_SV_________ = StrConv("# OF ALL SV         ", vbFromUnicode)
                SV_HEALTH___________ = StrConv("SV HEALTH           ", vbFromUnicode)
    
                '2017/06/23 NS6000対応。'''''''''''''''''''''''''''''''''''''''''''''''''''''''
                OBS_TYPES___________ = StrConv("OBS TYPES           ", vbFromUnicode)
                RINEX_VERSION_______ = StrConv("RINEX VERSION       ", vbFromUnicode)
                '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                Exit Sub
    
            ErrorHandler:
                Call mdlMain.ErrorExit
    
            End Sub
          [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        private void Class_Initialize()
        {
            try
            {
                //-----------------------------------------------------------------------------    
                byte[] nCode;
                //-----------------------------------------------------------------------------    
                nCode = mdiVBfunctions.StrConv("END OF HEADER       ", DEFINE.vbFromUnicode);
                END_OF_HEADER_______ = nCode.ToString();
                //-----------------------------------------------------------------------------    
                //  COMMENT_____________ = StrConv("COMMENT             ", vbFromUnicode)
                nCode = mdiVBfunctions.StrConv("COMMENT             ", DEFINE.vbFromUnicode);
                COMMENT_____________ = nCode.ToString();
                //-----------------------------------------------------------------------------    
                //  FILE_VERSION________ = StrConv("FILE VERSION        ", vbFromUnicode)
                nCode = mdiVBfunctions.StrConv("FILE VERSION        ", DEFINE.vbFromUnicode);
                FILE_VERSION________ = nCode.ToString();
                //-----------------------------------------------------------------------------    
                // FILE_NAME___________ = StrConv("FILE NAME           ", vbFromUnicode)
                nCode = mdiVBfunctions.StrConv("FILE NAME           ", DEFINE.vbFromUnicode);
                FILE_NAME___________ = nCode.ToString();
                //-----------------------------------------------------------------------------    
                //  MARKER_NUMBER_______ = StrConv("MARKER NUMBER       ", vbFromUnicode)
                nCode = mdiVBfunctions.StrConv("MARKER NUMBER       ", DEFINE.vbFromUnicode);
                MARKER_NUMBER_______ = nCode.ToString();
                //-----------------------------------------------------------------------------    
                // MARKER_NAME_________ = StrConv("MARKER NAME         ", vbFromUnicode)
                nCode = mdiVBfunctions.StrConv("MARKER NAME         ", DEFINE.vbFromUnicode);
                MARKER_NAME_________ = nCode.ToString();
                //-----------------------------------------------------------------------------    
                //  SESSION_ID__________ = StrConv("SESSION ID          ", vbFromUnicode)
                nCode = mdiVBfunctions.StrConv("SESSION ID          ", DEFINE.vbFromUnicode);
                SESSION_ID__________ = nCode.ToString();
                //-----------------------------------------------------------------------------    
                //  TIME_OF_FIRST_OBS___ = StrConv("TIME OF FIRST OBS   ", vbFromUnicode)
                nCode = mdiVBfunctions.StrConv("TIME OF FIRST OBS   ", DEFINE.vbFromUnicode);
                TIME_OF_FIRST_OBS___ = nCode.ToString();
                //-----------------------------------------------------------------------------    
                //  WEEK_OF_FIRST_OBS___ = StrConv("WEEK OF FIRST OBS   ", vbFromUnicode)
                nCode = mdiVBfunctions.StrConv("WEEK OF FIRST OBS   ", DEFINE.vbFromUnicode);
                WEEK_OF_FIRST_OBS___ = nCode.ToString();
                //-----------------------------------------------------------------------------    
                //  TOW_OF_FIRST_OBS____ = StrConv("TOW OF FIRST OBS    ", vbFromUnicode)
                nCode = mdiVBfunctions.StrConv("TOW OF FIRST OBS    ", DEFINE.vbFromUnicode);
                TOW_OF_FIRST_OBS____ = nCode.ToString();
                //-----------------------------------------------------------------------------    
                //  TIME_OF_LAST_OBS____ = StrConv("TIME OF LAST OBS    ", vbFromUnicode)
                nCode = mdiVBfunctions.StrConv("TIME OF LAST OBS    ", DEFINE.vbFromUnicode);
                TIME_OF_LAST_OBS____ = nCode.ToString();
                //-----------------------------------------------------------------------------    
                //  WEEK_OF_LAST_OBS____ = StrConv("WEEK OF LAST OBS    ", vbFromUnicode)
                nCode = mdiVBfunctions.StrConv("WEEK OF LAST OBS    ", DEFINE.vbFromUnicode);
                WEEK_OF_LAST_OBS____ = nCode.ToString();
                //-----------------------------------------------------------------------------    
                //  TOW_OF_LAST_OBS_____ = StrConv("TOW OF LAST OBS     ", vbFromUnicode)
                nCode = mdiVBfunctions.StrConv("TOW OF LAST OBS     ", DEFINE.vbFromUnicode);
                TOW_OF_LAST_OBS_____ = nCode.ToString();
                //-----------------------------------------------------------------------------    
                //  TIME_OF_OBS_________ = StrConv("TIME OF OBS         ", vbFromUnicode)
                nCode = mdiVBfunctions.StrConv("TIME OF OBS         ", DEFINE.vbFromUnicode);
                TIME_OF_OBS_________ = nCode.ToString();
                //-----------------------------------------------------------------------------    
                //  EPOCH_OF_OBS________ = StrConv("EPOCH OF OBS        ", vbFromUnicode)
                nCode = mdiVBfunctions.StrConv("EPOCH OF OBS        ", DEFINE.vbFromUnicode);
                EPOCH_OF_OBS________ = nCode.ToString();
                //-----------------------------------------------------------------------------    
                //  INTERVAL____________ = StrConv("INTERVAL            ", vbFromUnicode)
                nCode = mdiVBfunctions.StrConv("INTERVAL            ", DEFINE.vbFromUnicode);
                INTERVAL____________ = nCode.ToString();
                //-----------------------------------------------------------------------------    
                //  ELEVATION_MASK______ = StrConv("ELEVATION MASK      ", vbFromUnicode)
                nCode = mdiVBfunctions.StrConv("ELEVATION MASK      ", DEFINE.vbFromUnicode);
                ELEVATION_MASK______ = nCode.ToString();
                //-----------------------------------------------------------------------------    
                //  LEAP_SECONDS________ = StrConv("LEAP SECONDS        ", vbFromUnicode)
                nCode = mdiVBfunctions.StrConv("LEAP SECONDS        ", DEFINE.vbFromUnicode);
                LEAP_SECONDS________ = nCode.ToString();
                //-----------------------------------------------------------------------------    
                //  REC_TYPE____________ = StrConv("REC TYPE            ", vbFromUnicode)
                nCode = mdiVBfunctions.StrConv("REC TYPE            ", DEFINE.vbFromUnicode);
                REC_TYPE____________ = nCode.ToString();
                //-----------------------------------------------------------------------------    
                //  REC_N_______________ = StrConv("REC #               ", vbFromUnicode)
                nCode = mdiVBfunctions.StrConv("REC #               ", DEFINE.vbFromUnicode);
                REC_N_______________ = nCode.ToString();
                //-----------------------------------------------------------------------------    
                //  ANT_TYPE____________ = StrConv("ANT TYPE            ", vbFromUnicode)
                nCode = mdiVBfunctions.StrConv("ANT TYPE            ", DEFINE.vbFromUnicode);
                ANT_TYPE____________ = nCode.ToString();
                //-----------------------------------------------------------------------------    
                //  ANT_N_______________ = StrConv("ANT #               ", vbFromUnicode)
                nCode = mdiVBfunctions.StrConv("ANT #               ", DEFINE.vbFromUnicode);
                ANT_N_______________ = nCode.ToString();
                //-----------------------------------------------------------------------------    
                //  ANT_MEASUREMENT_____ = StrConv("ANT MEASUREMENT     ", vbFromUnicode)
                nCode = mdiVBfunctions.StrConv("ANT MEASUREMENT     ", DEFINE.vbFromUnicode);
                ANT_MEASUREMENT_____ = nCode.ToString();
                //-----------------------------------------------------------------------------    
                //  ANT_HEIGHT__________ = StrConv("ANT HEIGHT          ", vbFromUnicode)
                nCode = mdiVBfunctions.StrConv("ANT HEIGHT          ", DEFINE.vbFromUnicode);
                ANT_HEIGHT__________ = nCode.ToString();
                //-----------------------------------------------------------------------------    
                //  ANT_DELTA_H_________ = StrConv("ANT DELTA H         ", vbFromUnicode)
                nCode = mdiVBfunctions.StrConv("ANT DELTA H         ", DEFINE.vbFromUnicode);
                ANT_DELTA_H_________ = nCode.ToString();
                //-----------------------------------------------------------------------------    
                //  ANT_DELTA_E_________ = StrConv("ANT DELTA E         ", vbFromUnicode)
                nCode = mdiVBfunctions.StrConv("ANT DELTA E         ", DEFINE.vbFromUnicode);
                ANT_DELTA_E_________ = nCode.ToString();
                //-----------------------------------------------------------------------------    
                //  ANT_DELTA_N_________ = StrConv("ANT DELTA N         ", vbFromUnicode)
                nCode = mdiVBfunctions.StrConv("ANT DELTA N         ", DEFINE.vbFromUnicode);
                ANT_DELTA_N_________ = nCode.ToString();
                //-----------------------------------------------------------------------------    
                //  APPROX_POSITION_X___ = StrConv("APPROX POSITION X   ", vbFromUnicode)
                nCode = mdiVBfunctions.StrConv("APPROX POSITION X   ", DEFINE.vbFromUnicode);
                APPROX_POSITION_X___ = nCode.ToString();
                //-----------------------------------------------------------------------------    
                //  APPROX_POSITION_Y___ = StrConv("APPROX POSITION Y   ", vbFromUnicode)
                nCode = mdiVBfunctions.StrConv("APPROX POSITION Y   ", DEFINE.vbFromUnicode);
                APPROX_POSITION_Y___ = nCode.ToString();
                //-----------------------------------------------------------------------------    
                //  APPROX_POSITION_Z___ = StrConv("APPROX POSITION Z   ", vbFromUnicode)
                nCode = mdiVBfunctions.StrConv("APPROX POSITION Z   ", DEFINE.vbFromUnicode);
                APPROX_POSITION_Z___ = nCode.ToString();
                //-----------------------------------------------------------------------------    
                //  N_OF_OBSERV_________ = StrConv("# OF OBSERV         ", vbFromUnicode)
                nCode = mdiVBfunctions.StrConv("# OF OBSERV         ", DEFINE.vbFromUnicode);
                N_OF_OBSERV_________ = nCode.ToString();
                //-----------------------------------------------------------------------------    
                //  TYPES_OF_OBSERV_____ = StrConv("TYPES OF OBSERV     ", vbFromUnicode)
                nCode = mdiVBfunctions.StrConv("TYPES OF OBSERV     ", DEFINE.vbFromUnicode);
                TYPES_OF_OBSERV_____ = nCode.ToString();
                //-----------------------------------------------------------------------------    
                //  AS__________________ = StrConv("AS                  ", vbFromUnicode)
                nCode = mdiVBfunctions.StrConv("AS                  ", DEFINE.vbFromUnicode);
                AS__________________ = nCode.ToString();
                //-----------------------------------------------------------------------------    
                //  N_OF_MAX_SV_________ = StrConv("# OF MAX SV         ", vbFromUnicode)
                nCode = mdiVBfunctions.StrConv("# OF MAX SV         ", DEFINE.vbFromUnicode);
                N_OF_MAX_SV_________ = nCode.ToString();
                //-----------------------------------------------------------------------------    
                //  N_OF_MIN_SV_________ = StrConv("# OF MIN SV         ", vbFromUnicode)
                nCode = mdiVBfunctions.StrConv("# OF MIN SV         ", DEFINE.vbFromUnicode);
                N_OF_MIN_SV_________ = nCode.ToString();
                //-----------------------------------------------------------------------------    
                //  N_OF_ALL_SV_________ = StrConv("# OF ALL SV         ", vbFromUnicode)
                nCode = mdiVBfunctions.StrConv("# OF ALL SV         ", DEFINE.vbFromUnicode);
                N_OF_ALL_SV_________ = nCode.ToString();
                //-----------------------------------------------------------------------------    
                //  SV_HEALTH___________ = StrConv("SV HEALTH           ", vbFromUnicode)
                nCode = mdiVBfunctions.StrConv("SV HEALTH           ", DEFINE.vbFromUnicode);
                SV_HEALTH___________ = nCode.ToString();
                //-----------------------------------------------------------------------------    

                //'2017/06/23 NS6000対応。'''''''''''''''''''''''''''''''''''''''''''''''''''''''
                //  OBS_TYPES___________ = StrConv("OBS TYPES           ", vbFromUnicode)
                nCode = mdiVBfunctions.StrConv("OBS TYPES           ", DEFINE.vbFromUnicode);
                OBS_TYPES___________ = nCode.ToString();
                //-----------------------------------------------------------------------------    
                //  RINEX_VERSION_______ = StrConv("RINEX VERSION       ", vbFromUnicode)
                nCode = mdiVBfunctions.StrConv("RINEX VERSION       ", DEFINE.vbFromUnicode);
                RINEX_VERSION_______ = nCode.ToString();
                //'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

            }
            catch (Exception ex)
            {
                //ErrorHandler:
                //    Call mdlMain.ErrorExit
                MessageBox.Show(ex.Message, "エラー発生", MessageBoxButtons.OK, MessageBoxIcon.Error);


            }
        }


        //'*******************************************************************************
        //'メソッド

        //==========================================================================================
        /*[VB]
            '衛星情報ファイルを開く。
            '
            '引き数：
            'sPath 衛星情報ファイルのパス。
            Public Sub OpenFile(ByVal sPath As String)
                Open sPath For Input Access Read Lock Write As #m_clsFile.Number
                m_nLength = LOF(m_clsFile.Number)
                m_bIsHeader = False
            End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
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


        //==========================================================================================
        /*[VB]
            'ヘッダー部の読み込み。
            '
            '引き数：
            'clsProgressInterface ProgressInterface オブジェクト。
            '
            '戻り値：読み込んだヘッダー部を返す。
            Public Function ReadHeader(ByVal clsProgressInterface As ProgressInterface) As SatelliteInfoHeader

                'シーク。
                If m_bIsHeader Then Call SeekTop
    
                Dim clsSatelliteInfoHeader As New SatelliteInfoHeader
    
                Dim sTypeOfObserv() As String
                Dim nSVHealth() As Long
                Dim nSVCount As Long
                ReDim sTypeOfObserv(-1 To -1)
                ReDim nSVHealth(-1 To -1)
            '    ReDim nSVHealth(-1 To 32)
                nSVCount = 0
    
                '2017/06/23 NS6000対応。'''''''''''''''''''''''''''''''''''''''''''''''''''''''
                Dim sValue As String
                Dim nNumberObserv As Long
                Dim sObsType As String
                Dim sTypeOfObservR() As String
                Dim sTypeOfObservJ() As String
                Dim sTypeOfObservE() As String
                Dim sTypeOfObservC() As String
                Dim sTypeOfObservS() As String
                ReDim sTypeOfObservR(-1 To -1)
                ReDim sTypeOfObservJ(-1 To -1)
                ReDim sTypeOfObservE(-1 To -1)
                ReDim sTypeOfObservC(-1 To -1)
                ReDim sTypeOfObservS(-1 To -1)
                clsSatelliteInfoHeader.Version = 1
                '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    
                Do While Not IsEOF()
                    'プログレス。
                    Call clsProgressInterface.CheckCancel
        
                    Dim sBuff As String
                    Line Input #m_clsFile.Number, sBuff
                    Dim sMbcs As String
                    sMbcs = StrConv(sBuff, vbFromUnicode)
        
                    If StrComp(MidB$(sMbcs, RNX_STR), END_OF_HEADER_______) = 0 Then
                        Exit Do
                    ElseIf StrComp(MidB$(sMbcs, RNX_STR), COMMENT_____________) = 0 Then
                        GoTo ContinueHandler
                    ElseIf StrComp(MidB$(sMbcs, RNX_STR), FILE_VERSION________) = 0 Then
                        clsSatelliteInfoHeader.Version = RTrim(StrConv(LeftB$(sMbcs, RNX_STR - 1), vbUnicode))
                    ElseIf StrComp(MidB$(sMbcs, RNX_STR), FILE_NAME___________) = 0 Then
                        clsSatelliteInfoHeader.FileName = RTrim(StrConv(LeftB$(sMbcs, RNX_STR - 1), vbUnicode))
                    ElseIf StrComp(MidB$(sMbcs, RNX_STR), MARKER_NUMBER_______) = 0 Then
                        clsSatelliteInfoHeader.MarkerNumber = RTrim(StrConv(LeftB$(sMbcs, RNX_STR - 1), vbUnicode))
                    ElseIf StrComp(MidB$(sMbcs, RNX_STR), MARKER_NAME_________) = 0 Then
                        clsSatelliteInfoHeader.MarkerName = RTrim(StrConv(LeftB$(sMbcs, RNX_STR - 1), vbUnicode))
                    ElseIf StrComp(MidB$(sMbcs, RNX_STR), SESSION_ID__________) = 0 Then
                        clsSatelliteInfoHeader.SessionID = RTrim(StrConv(LeftB$(sMbcs, RNX_STR - 1), vbUnicode))
                    ElseIf StrComp(MidB$(sMbcs, RNX_STR), TIME_OF_FIRST_OBS___) = 0 Then
                        clsSatelliteInfoHeader.TimeOfFirstObs = GetDateFromString(sBuff)
                    ElseIf StrComp(MidB$(sMbcs, RNX_STR), WEEK_OF_FIRST_OBS___) = 0 Then
                        clsSatelliteInfoHeader.WeekOfFirstObs = Val(StrConv(LeftB$(sMbcs, RNX_STR - 1), vbUnicode))
                    ElseIf StrComp(MidB$(sMbcs, RNX_STR), TOW_OF_FIRST_OBS____) = 0 Then
                        clsSatelliteInfoHeader.TowOfFirstObs = Val(StrConv(LeftB$(sMbcs, RNX_STR - 1), vbUnicode))
                    ElseIf StrComp(MidB$(sMbcs, RNX_STR), TIME_OF_LAST_OBS____) = 0 Then
                        clsSatelliteInfoHeader.TimeOfLastObs = GetDateFromString(sBuff)
                    ElseIf StrComp(MidB$(sMbcs, RNX_STR), WEEK_OF_LAST_OBS____) = 0 Then
                        clsSatelliteInfoHeader.WeekOfLastObs = Val(StrConv(LeftB$(sMbcs, RNX_STR - 1), vbUnicode))
                    ElseIf StrComp(MidB$(sMbcs, RNX_STR), TOW_OF_LAST_OBS_____) = 0 Then
                        clsSatelliteInfoHeader.TowOfLastObs = Val(StrConv(LeftB$(sMbcs, RNX_STR - 1), vbUnicode))
                    ElseIf StrComp(MidB$(sMbcs, RNX_STR), TIME_OF_OBS_________) = 0 Then
                        Dim clsStringTokenizer As StringTokenizer
                        Set clsStringTokenizer = New StringTokenizer
                        clsStringTokenizer.Delimiter = ":"
                        clsStringTokenizer.Source = sBuff
                        clsStringTokenizer.Begin
                        clsSatelliteInfoHeader.TimeOfObs = Val(clsStringTokenizer.NextToken) * 60 * 60
                        clsSatelliteInfoHeader.TimeOfObs = clsSatelliteInfoHeader.TimeOfObs + Val(clsStringTokenizer.NextToken) * 60
                        clsSatelliteInfoHeader.TimeOfObs = clsSatelliteInfoHeader.TimeOfObs + Val(clsStringTokenizer.NextToken)
                    ElseIf StrComp(MidB$(sMbcs, RNX_STR), EPOCH_OF_OBS________) = 0 Then
                        clsSatelliteInfoHeader.EpochOfObs = Val(StrConv(LeftB$(sMbcs, RNX_STR - 1), vbUnicode))
                    ElseIf StrComp(MidB$(sMbcs, RNX_STR), INTERVAL____________) = 0 Then
                        clsSatelliteInfoHeader.Interval = Val(StrConv(LeftB$(sMbcs, RNX_STR - 1), vbUnicode))
                    ElseIf StrComp(MidB$(sMbcs, RNX_STR), ELEVATION_MASK______) = 0 Then
                        clsSatelliteInfoHeader.ElevationMask = Val(StrConv(LeftB$(sMbcs, RNX_STR - 1), vbUnicode))
                    ElseIf StrComp(MidB$(sMbcs, RNX_STR), LEAP_SECONDS________) = 0 Then
                        If Asc(sBuff) = &H20& Then
                            clsSatelliteInfoHeader.LeapSeconds = GetDocument().DefLeapSec
                        Else
                            clsSatelliteInfoHeader.LeapSeconds = Val(StrConv(LeftB$(sMbcs, RNX_STR - 1), vbUnicode))
                        End If
                    ElseIf StrComp(MidB$(sMbcs, RNX_STR), REC_TYPE____________) = 0 Then
                        clsSatelliteInfoHeader.RecType = RTrim(StrConv(LeftB$(sMbcs, RNX_STR - 1), vbUnicode))
                    ElseIf StrComp(MidB$(sMbcs, RNX_STR), REC_N_______________) = 0 Then
                        clsSatelliteInfoHeader.RecNumber = RTrim(StrConv(LeftB$(sMbcs, RNX_STR - 1), vbUnicode))
                    ElseIf StrComp(MidB$(sMbcs, RNX_STR), ANT_TYPE____________) = 0 Then
                        clsSatelliteInfoHeader.AntType = RTrim(StrConv(LeftB$(sMbcs, RNX_STR - 1), vbUnicode))
                    ElseIf StrComp(MidB$(sMbcs, RNX_STR), ANT_N_______________) = 0 Then
                        clsSatelliteInfoHeader.AntNumber = RTrim(StrConv(LeftB$(sMbcs, RNX_STR - 1), vbUnicode))
                    ElseIf StrComp(MidB$(sMbcs, RNX_STR), ANT_MEASUREMENT_____) = 0 Then
                        clsSatelliteInfoHeader.AntMeasurement = RTrim(StrConv(LeftB$(sMbcs, RNX_STR - 1), vbUnicode))
                    ElseIf StrComp(MidB$(sMbcs, RNX_STR), ANT_HEIGHT__________) = 0 Then
                        clsSatelliteInfoHeader.AntHeight = Val(StrConv(LeftB$(sMbcs, RNX_STR - 1), vbUnicode))
                    ElseIf StrComp(MidB$(sMbcs, RNX_STR), ANT_DELTA_H_________) = 0 Then
                        clsSatelliteInfoHeader.AntDeltaH = Val(StrConv(LeftB$(sMbcs, RNX_STR - 1), vbUnicode))
                    ElseIf StrComp(MidB$(sMbcs, RNX_STR), ANT_DELTA_E_________) = 0 Then
                        clsSatelliteInfoHeader.AntDeltaE = Val(StrConv(LeftB$(sMbcs, RNX_STR - 1), vbUnicode))
                    ElseIf StrComp(MidB$(sMbcs, RNX_STR), ANT_DELTA_N_________) = 0 Then
                        clsSatelliteInfoHeader.AntDeltaN = Val(StrConv(LeftB$(sMbcs, RNX_STR - 1), vbUnicode))
                    ElseIf StrComp(MidB$(sMbcs, RNX_STR), APPROX_POSITION_X___) = 0 Then
                        clsSatelliteInfoHeader.ApproxPosX = Val(StrConv(LeftB$(sMbcs, RNX_STR - 1), vbUnicode))
                    ElseIf StrComp(MidB$(sMbcs, RNX_STR), APPROX_POSITION_Y___) = 0 Then
                        clsSatelliteInfoHeader.ApproxPosY = Val(StrConv(LeftB$(sMbcs, RNX_STR - 1), vbUnicode))
                    ElseIf StrComp(MidB$(sMbcs, RNX_STR), APPROX_POSITION_Z___) = 0 Then
                        clsSatelliteInfoHeader.ApproxPosZ = Val(StrConv(LeftB$(sMbcs, RNX_STR - 1), vbUnicode))
                    ElseIf StrComp(MidB$(sMbcs, RNX_STR), N_OF_OBSERV_________) = 0 Then
                        '2017/06/23 NS6000対応。'''''''''''''''''''''''''''''''''''''''''''''''''''''''
                        'clsSatelliteInfoHeader.NumberObserv = Val(StrConv(LeftB$(sMbcs, RNX_STR - 1), vbUnicode))
                        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                        If clsSatelliteInfoHeader.Version < 100 Then
                            clsSatelliteInfoHeader.NumberObserv = Val(StrConv(LeftB$(sMbcs, RNX_STR - 1), vbUnicode))
                        Else
                            sValue = RTrim(StrConv(LeftB$(sMbcs, RNX_STR - 1), vbUnicode))
                            nNumberObserv = Val(Mid(sValue, 3))
                            Select Case Left(sValue, 1)
                            Case "G"
                                clsSatelliteInfoHeader.NumberObserv = nNumberObserv
                            Case "R"
                                clsSatelliteInfoHeader.NumberObservR = nNumberObserv
                            Case "J"
                                clsSatelliteInfoHeader.NumberObservJ = nNumberObserv
                            Case "Q"
                                clsSatelliteInfoHeader.NumberObservJ = nNumberObserv
                            Case "E"
                                clsSatelliteInfoHeader.NumberObservE = nNumberObserv
                            Case "C"
                                clsSatelliteInfoHeader.NumberObservC = nNumberObserv
                            Case "S"
                                clsSatelliteInfoHeader.NumberObservS = nNumberObserv
                            End Select
                        End If
                        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                    ElseIf StrComp(MidB$(sMbcs, RNX_STR), TYPES_OF_OBSERV_____) = 0 Then
                        ReDim Preserve sTypeOfObserv(-1 To UBound(sTypeOfObserv) + 1)
                        sTypeOfObserv(UBound(sTypeOfObserv)) = RTrim(StrConv(LeftB$(sMbcs, RNX_STR - 1), vbUnicode))
                    '2017/06/23 NS6000対応。'''''''''''''''''''''''''''''''''''''''''''''''''''''''
                    ElseIf StrComp(MidB$(sMbcs, RNX_STR), OBS_TYPES___________) = 0 Then
                        sValue = RTrim(StrConv(LeftB$(sMbcs, RNX_STR - 1), vbUnicode))
                        sObsType = Mid(sValue, 3)
                        Select Case Left(sValue, 1)
                        Case "G"
                            ReDim Preserve sTypeOfObserv(-1 To UBound(sTypeOfObserv) + 1)
                            sTypeOfObserv(UBound(sTypeOfObserv)) = sObsType
                        Case "R"
                            ReDim Preserve sTypeOfObservR(-1 To UBound(sTypeOfObservR) + 1)
                            sTypeOfObservR(UBound(sTypeOfObservR)) = sObsType
                        Case "J"
                            ReDim Preserve sTypeOfObservJ(-1 To UBound(sTypeOfObservJ) + 1)
                            sTypeOfObservJ(UBound(sTypeOfObservJ)) = sObsType
                        Case "Q"
                            ReDim Preserve sTypeOfObservJ(-1 To UBound(sTypeOfObservJ) + 1)
                            sTypeOfObservJ(UBound(sTypeOfObservJ)) = sObsType
                        Case "E"
                            ReDim Preserve sTypeOfObservE(-1 To UBound(sTypeOfObservE) + 1)
                            sTypeOfObservE(UBound(sTypeOfObservE)) = sObsType
                        Case "C"
                            ReDim Preserve sTypeOfObservC(-1 To UBound(sTypeOfObservC) + 1)
                            sTypeOfObservC(UBound(sTypeOfObservC)) = sObsType
                        Case "S"
                            ReDim Preserve sTypeOfObservS(-1 To UBound(sTypeOfObservS) + 1)
                            sTypeOfObservS(UBound(sTypeOfObservS)) = sObsType
                        End Select
                    ElseIf StrComp(MidB$(sMbcs, RNX_STR), RINEX_VERSION_______) = 0 Then
                        clsSatelliteInfoHeader.RinexVersion = Val(StrConv(LeftB$(sMbcs, RNX_STR - 1), vbUnicode)) * 1000
                    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                    ElseIf StrComp(MidB$(sMbcs, RNX_STR), AS__________________) = 0 Then
                        clsSatelliteInfoHeader.As_ = Val(StrConv(LeftB$(sMbcs, RNX_STR - 1), vbUnicode))
                    ElseIf StrComp(MidB$(sMbcs, RNX_STR), N_OF_MAX_SV_________) = 0 Then
                        clsSatelliteInfoHeader.NumberOfMaxSV = Val(StrConv(LeftB$(sMbcs, RNX_STR - 1), vbUnicode))
                    ElseIf StrComp(MidB$(sMbcs, RNX_STR), N_OF_MIN_SV_________) = 0 Then
                        clsSatelliteInfoHeader.NumberOfMinSV = Val(StrConv(LeftB$(sMbcs, RNX_STR - 1), vbUnicode))
                    ElseIf StrComp(MidB$(sMbcs, RNX_STR), N_OF_ALL_SV_________) = 0 Then
                        clsSatelliteInfoHeader.NumberOfAllSV = Val(StrConv(LeftB$(sMbcs, RNX_STR - 1), vbUnicode))
                    ElseIf StrComp(MidB$(sMbcs, RNX_STR), SV_HEALTH___________) = 0 Then
                        Dim nE As Long
                        nE = InStr(sBuff, "=")
                        If nE > 1 Then
                            nSVCount = nSVCount + 1
                            Dim nSV As Long
                            nSV = Val(sBuff)
                            '2017/06/29 NS6000対応。'''''''''''''''''''''''''''''''''''''''''''''''''''''''
                            'ReDim Preserve nSVHealth(-1 To nSV)
                            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                            nSV = ConvertNumber(nSV)
                            If nSV > UBound(nSVHealth) Then
                                ReDim Preserve nSVHealth(-1 To nSV)
                            End If
                            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                            nSVHealth(nSV) = IIf(Val(Mid$(sBuff, nE + 1)) = 1, 1, -1)
                        End If
                    End If
            ContinueHandler:
                Loop
    
                If clsSatelliteInfoHeader.NumberObserv <> (UBound(sTypeOfObserv) + 1) Then Call Err.Raise(ERR_FILE, , MSG_SATELLITEINFO_INVALID)
                If clsSatelliteInfoHeader.NumberOfAllSV <> nSVCount Then Call Err.Raise(ERR_FILE, , MSG_SATELLITEINFO_INVALID)
    
                clsSatelliteInfoHeader.TypeOfObserv = sTypeOfObserv
                clsSatelliteInfoHeader.SVHealth = nSVHealth
    
                '2017/06/23 NS6000対応。'''''''''''''''''''''''''''''''''''''''''''''''''''''''
                If clsSatelliteInfoHeader.NumberObservR <> (UBound(sTypeOfObservR) + 1) Then Call Err.Raise(ERR_FILE, , MSG_SATELLITEINFO_INVALID)
                If clsSatelliteInfoHeader.NumberObservJ <> (UBound(sTypeOfObservJ) + 1) Then Call Err.Raise(ERR_FILE, , MSG_SATELLITEINFO_INVALID)
                If clsSatelliteInfoHeader.NumberObservE <> (UBound(sTypeOfObservE) + 1) Then Call Err.Raise(ERR_FILE, , MSG_SATELLITEINFO_INVALID)
                If clsSatelliteInfoHeader.NumberObservC <> (UBound(sTypeOfObservC) + 1) Then Call Err.Raise(ERR_FILE, , MSG_SATELLITEINFO_INVALID)
                If clsSatelliteInfoHeader.NumberObservS <> (UBound(sTypeOfObservS) + 1) Then Call Err.Raise(ERR_FILE, , MSG_SATELLITEINFO_INVALID)
                clsSatelliteInfoHeader.TypeOfObservR = sTypeOfObservR
                clsSatelliteInfoHeader.TypeOfObservJ = sTypeOfObservJ
                clsSatelliteInfoHeader.TypeOfObservE = sTypeOfObservE
                clsSatelliteInfoHeader.TypeOfObservC = sTypeOfObservC
                clsSatelliteInfoHeader.TypeOfObservS = sTypeOfObservS
                '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    
                m_bIsHeader = True
    
                Set ReadHeader = clsSatelliteInfoHeader
    
            End Function
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //  public SatelliteInfoHeader ReadHeader(ProgressInterface clsProgressInterface)
        public object ReadHeader(object clsProgressInterface)
        {

            SatelliteInfoHeader ReadHeader;

            SatelliteInfoHeader clsSatelliteInfoHeader = new SatelliteInfoHeader();

            //'シーク。
            if (m_bIsHeader)
            {
                //Then Call SeekTop
            }


            ReadHeader = clsSatelliteInfoHeader;
            return (object)ReadHeader;
        }

        //==========================================================================================
        /*[VB]
            '遷移状態の読み込み。
            '
            '健康状態、位相は評価しない。
            '
            '引き数：
            'tTimeOfFirstObs 観測開始日時。
            'nInterval 観測間隔(秒)。
            'clsProgressInterface ProgressInterface オブジェクト。
            '
            '戻り値：読み込んだ遷移状態を返す。
            Public Function ReadChanges2(ByVal tTimeOfFirstObs As Date, ByVal nInterval As Long, ByVal clsProgressInterface As ProgressInterface) As SatelliteInfoChanges

                'シーク。
                If IsEOF() Then Call SeekTop
                If Not m_bIsHeader Then Call SeekHeader(clsProgressInterface)
    
                Set ReadChanges2 = New SatelliteInfoChanges
    
                Dim bPrevNumbers() As Boolean
                ReDim bPrevNumbers(-1 To PPS_MAXPRN_CAPA)
                Dim tPrev As Date
                tPrev = DateAdd("s", -nInterval, tTimeOfFirstObs)
    
                '2007/08/21 Seillac H.Nakamura
                '最大間隔。エポックの欠損が最大欠損数以下であれば衛星は捕捉し続けているものとする。
                Dim nMaxInterval As Long
                nMaxInterval = nInterval * (EPOCH_LOSSES_MAX + 1)
    
                '最初の行。
                If IsEOF() Then Exit Function
                Dim sBuff As String
                Line Input #m_clsFile.Number, sBuff
    
                Do While Not IsEOF()
                    'プログレス。
                    Call clsProgressInterface.CheckCancel
        
                    'ヘッダー。
                    Dim clsTokenizer As New StringTokenizer
                    clsTokenizer.Source = sBuff
                    Call clsTokenizer.Begin
                    Dim sToken As String
                    '日付。
                    Dim tTime As Date
                    tTime = DateValue(clsTokenizer.NextToken)
                    tTime = tTime + GetDateFromString(clsTokenizer.NextToken)
                    If DateDiff("s", tTime, tPrev) >= 0 Then Call Err.Raise(ERR_FILE, , MSG_SATELLITEINFO_INVALID)
                    'データ抜け？
                    'If DateDiff("s", tPrev, tTime) > nInterval Then
                    If DateDiff("s", tPrev, tTime) > nMaxInterval Then '2007/08/21 Seillac H.Nakamura
                        '衛星数0個として評価。
                        Dim bNextNumbers() As Boolean
                        ReDim bNextNumbers(-1 To PPS_MAXPRN_CAPA)
                        Call ReadChanges2.AddEvaluation(tPrev, bPrevNumbers, bNextNumbers)
                        bPrevNumbers = bNextNumbers
                    End If
                    '衛星数。
                    Dim nCount As Long
                    nCount = Val(clsTokenizer.NextToken)
                    If nCount < 0 Then Call Err.Raise(ERR_FILE, , MSG_SATELLITEINFO_INVALID)
        
                    '有効衛星。
                    ReDim bNextNumbers(-1 To PPS_MAXPRN_CAPA)
                    Do While Not IsEOF()
                        'プログレス。
                        Call clsProgressInterface.CheckCancel
            
                        Line Input #m_clsFile.Number, sBuff
                        '先頭が空白でなければヘッダーである。
                        If Asc(sBuff) <> &H20& Then Exit Do
            
                        clsTokenizer.Source = sBuff
                        Call clsTokenizer.Begin
            
                        '衛星番号。
                        Dim nNumber As Long
                        nNumber = Val(clsTokenizer.NextToken)
            
                        '2017/06/06 NS6000対応。'''''''''''''''''''''''''''''''''''''''''''''''''''''''
                        nNumber = ConvertNumber(nNumber)
                        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            
                        bNextNumbers(nNumber) = True
            
                        nCount = nCount - 1
                    Loop
                    If nCount <> 0 Then Call Err.Raise(ERR_FILE, , MSG_SATELLITEINFO_INVALID)
        
                    '評価。
                    Call ReadChanges2.AddEvaluation(tPrev, bPrevNumbers, bNextNumbers)
        
                    tPrev = tTime
                    bPrevNumbers = bNextNumbers
                Loop
    
                '最後。
                ReDim bNextNumbers(-1 To PPS_MAXPRN_CAPA)
                Call ReadChanges2.AddEvaluation(tPrev, bPrevNumbers, bNextNumbers)
    
            End Function
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //public SatelliteInfoChanges ReadChanges2(DateTime tTimeOfFirstObs, long nInterval, ProgressInterface clsProgressInterface)
        public object ReadChanges2(DateTime tTimeOfFirstObs, long nInterval, object clsProgressInterface)
        {

            SatelliteInfoChanges ReadChanges2 = new SatelliteInfoChanges();


            return (object)ReadChanges2;
        }






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
