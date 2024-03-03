using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static SurvLine.mdl.DEFINE;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace SurvLine
{
    internal class MdlNGSRDRIN
    {
        //'*******************************************************************************
        //'NGS2RIN
        //
        // Option Explicit
        //


        //==========================================================================================
        /*[VB]

            'ngs_rdrin

            Public Declare Function NGS_RDRINgetVersion Lib "ngs_rdrin" () As Single
            Public Declare Sub InitSVDataRNX Lib "ngs_rdrin" ()
            Public Declare Sub ExitSVDataRNX Lib "ngs_rdrin" ()
            Public Declare Function MakeSVInfoRNX Lib "ngs_rdrin" (ByVal rnx_o_name As String, ByVal rnx_n_name As String, ByVal svinfo_name As String, ByVal ant_list As String, ByVal rcv_list As String, ByVal have_log As Byte, ByVal log_name As String) As Long
            Public Declare Function MakeSVInfo2RNX Lib "ngs_rdrin" (ByVal rnx_o_name As String, ByVal rnx_n_name As String, ByVal rnx_g_name As String, ByVal svinfo_name As String, ByVal ant_list As String, ByVal rcv_list As String, ByVal have_log As Byte, ByVal log_name As String) As Long
            Public Declare Function EditHeaderRNX Lib "ngs_rdrin" (ByVal rnx_o_name As String, ByVal rnx_eo_name As String, ByVal ant_list As String, ByVal rcv_list As String, ByVal have_log As Byte, ByVal log_name As String, ByRef rnx_inp_edit As RNX_INP) As Long

            '2017/07/05 NS6000対応。'''''''''''''''''''''''''''''''''''''''''''''''''''''''
            Public Declare Function NGS_RDRIN302getVersion Lib "ngs_rdrin302" () As Single
            Public Declare Function MakeSVInfo302RNX Lib "ngs_rdrin302" (ByVal rnx_o_name As String, ByVal rnx_n_name As String, ByVal rnx_g_name As String, ByVal rnx_j_name As String, ByVal rnx_l_name As String, ByVal rnx_c_name As String, ByVal rnx_p_name As String, ByVal svinfo_name As String, ByVal ant_list As String, ByVal rcv_list As String, ByVal have_log As Byte, ByVal log_name As String, ByVal tmp_dir As String) As Long
            Public Declare Function MixNavigation Lib "ngs_rdrin302" (ByVal path_n As String, ByVal path_g As String, ByVal path_j As String, ByVal path_l As String, ByVal path_c As String, ByVal path_p As String) As Long
            Public Declare Function CheckVersionRNX Lib "ngs_rdrin302" (ByVal Path As String) As Single
            Public Declare Function CorrectionSignal Lib "ngs_rdrin302" (ByVal rnx_o_name As String) As Long

            Public Declare Function EditHeaderExRNX Lib "ngs_rdrin" (ByVal rnx_o_name As String, ByVal rnx_eo_name As String, ByVal ant_list As String, ByVal rcv_list As String, ByVal have_log As Byte, ByVal log_name As String, ByVal mark_name As String, ByVal mark_num As String, ByVal rcv_num As String, ByVal rcv_type As String, ByVal ant_num As String, ByVal ant_type As String, ByVal ant_measure As String, ByVal ant_hgt As Double) As Long
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            '2020/10/28 FDC 混合暦ファイルの分割'''''''''''''''''''''''''''''''''''''''''''
            Public Declare Function SplitRNX Lib "ngs_rdrin302" (ByVal rnx_p_name As String, ByVal rnx_n_name As String, ByVal rnx_g_name As String, ByVal rnx_j_name As String, ByVal rnx_l_name As String, ByVal rnx_c_name As String) As Long
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

            '2022/10/11 Hitz H.Nakamura **************************************************
            'RINEXのバージョン変換を追加。
            Public Declare Function NGS_CONVRINgetVersion Lib "ngs_convrin" () As Single
            Public Declare Function ConvertTo302 Lib "ngs_convrin" (ByVal rnx_path As String, ByVal out_path As String, ByVal tmp_path As String, ByVal receiver As Long, ByVal pgm As String, ByVal run_by As String, ByVal date_ As String) As Long
            Public Declare Function ConvertTo211 Lib "ngs_convrin" (ByVal rnx_path As String, ByVal out_path As String, ByVal tmp_path As String, ByVal receiver As Long, ByVal pgm As String, ByVal run_by As String, ByVal date_ As String) As Long
            '*****************************************************************************

            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]

        //'ngs_rdrin

#if false
        Public Declare Function NGS_RDRINgetVersion Lib "ngs_rdrin" () As Single
        Public Declare Sub InitSVDataRNX Lib "ngs_rdrin" ()
        Public Declare Sub ExitSVDataRNX Lib "ngs_rdrin" ()
        Public Declare Function MakeSVInfoRNX Lib "ngs_rdrin" (ByVal rnx_o_name As String, ByVal rnx_n_name As String, ByVal svinfo_name As String, ByVal ant_list As String, ByVal rcv_list As String, ByVal have_log As Byte, ByVal log_name As String) As Long
#endif
        //--------------------------------------------------------------------------------------------------------------
        //  Public Declare Function MakeSVInfo2RNX Lib "ngs_rdrin" (ByVal rnx_o_name As String, ByVal rnx_n_name As String, ByVal rnx_g_name As String, ByVal svinfo_name As String, ByVal ant_list As String, ByVal rcv_list As String, ByVal have_log As Byte, ByVal log_name As String) As Long
        [DllImport("ngs_rdrin")]
        public static extern long MakeSVInfo2RNX(string rnx_o_name, string rnx_n_name, string rnx_g_name, string svinfo_name, string ant_list, string rcv_list, Byte have_log, string log_name);

#if false
        Public Declare Function EditHeaderRNX Lib "ngs_rdrin" (ByVal rnx_o_name As String, ByVal rnx_eo_name As String, ByVal ant_list As String, ByVal rcv_list As String, ByVal have_log As Byte, ByVal log_name As String, ByRef rnx_inp_edit As RNX_INP) As Long

        '2017/07/05 NS6000対応。'''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Public Declare Function NGS_RDRIN302getVersion Lib "ngs_rdrin302" () As Single
#endif
        //--------------------------------------------------------------------------------------------------------------
        //  Public Declare Function MakeSVInfo302RNX Lib "ngs_rdrin302" (ByVal rnx_o_name As String, ByVal rnx_n_name As String, ByVal rnx_g_name As String, ByVal rnx_j_name As String, ByVal rnx_l_name As String, ByVal rnx_c_name As String, ByVal rnx_p_name As String, ByVal svinfo_name As String, ByVal ant_list As String, ByVal rcv_list As String, ByVal have_log As Byte, ByVal log_name As String, ByVal tmp_dir As String) As Long
        [DllImport("ngs_rdrin302")]
        public static extern long MakeSVInfo302RNX(string rnx_o_nam, string rnx_n_name, string rnx_g_name, string rnx_j_name, string rnx_l_name, string rnx_c_name, string rnx_p_name, string svinfo_name, string ant_list, string rcv_list, Byte have_log, string log_name, string tmp_dir);
        //--------------------------------------------------------------------------------------------------------------
        //  Public Declare Function MixNavigation Lib "ngs_rdrin302" (ByVal path_n As String, ByVal path_g As String, ByVal path_j As String, ByVal path_l As String, ByVal path_c As String, ByVal path_p As String) As Long
        [DllImport("ngs_rdrin302")]
        public static extern long MixNavigation(string rnx_o_nam, string rnx_n_name, string rnx_g_name, string rnx_j_name, string rnx_l_name, string rnx_c_name, string rnx_p_name, string svinfo_name, string ant_list, string rcv_list, Byte have_log, string log_name, string tmp_dir);
        //--------------------------------------------------------------------------------------------------------------
        //  Public Declare Function CheckVersionRNX Lib "ngs_rdrin302" (ByVal Path As String) As Single
        [DllImport("ngs_rdrin302")]
        public static extern float CheckVersionRNX(string Path);
        //--------------------------------------------------------------------------------------------------------------
        //  Public Declare Function CorrectionSignal Lib "ngs_rdrin302" (ByVal rnx_o_name As String) As Long
        [DllImport("ngs_rdrin302")]
        public static extern long CorrectionSignal(string rnx_o_name);
        //--------------------------------------------------------------------------------------------------------------
        //  Public Declare Function EditHeaderExRNX Lib "ngs_rdrin" (ByVal rnx_o_name As String, ByVal rnx_eo_name As String, ByVal ant_list As String, ByVal rcv_list As String, ByVal have_log As Byte, ByVal log_name As String, ByVal mark_name As String, ByVal mark_num As String, ByVal rcv_num As String, ByVal rcv_type As String, ByVal ant_num As String, ByVal ant_type As String, ByVal ant_measure As String, ByVal ant_hgt As Double) As Long
        [DllImport("ngs_rdrin")]
        public static extern long EditHeaderExRNX(string rnx_o_name, string rnx_eo_name, string ant_list, string rcv_list, Byte have_log, string log_name, string mark_name, string mark_num, string rcv_num, string rcv_type, string ant_num,string ant_type, string ant_measure, double ant_hgt);
        //'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        //'2020/10/28 FDC 混合暦ファイルの分割'''''''''''''''''''''''''''''''''''''''''''
        //  Public Declare Function SplitRNX Lib "ngs_rdrin302" (ByVal rnx_p_name As String, ByVal rnx_n_name As String, ByVal rnx_g_name As String, ByVal rnx_j_name As String, ByVal rnx_l_name As String, ByVal rnx_c_name As String) As Long
        [DllImport("ngs_rdrin302")]
        public static extern long SplitRNX(string rnx_p_name, string rnx_n_name, string rnx_g_name, string rnx_j_name, string rnx_l_name, string rnx_c_name);


#if false
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

            '2022/10/11 Hitz H.Nakamura **************************************************
            'RINEXのバージョン変換を追加。
            Public Declare Function NGS_CONVRINgetVersion Lib "ngs_convrin" () As Single
            Public Declare Function ConvertTo302 Lib "ngs_convrin" (ByVal rnx_path As String, ByVal out_path As String, ByVal tmp_path As String, ByVal receiver As Long, ByVal pgm As String, ByVal run_by As String, ByVal date_ As String) As Long
            Public Declare Function ConvertTo211 Lib "ngs_convrin" (ByVal rnx_path As String, ByVal out_path As String, ByVal tmp_path As String, ByVal receiver As Long, ByVal pgm As String, ByVal run_by As String, ByVal date_ As String) As Long
            '*****************************************************************************
#endif


    }
}
