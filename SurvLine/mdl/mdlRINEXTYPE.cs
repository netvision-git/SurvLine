using Microsoft.VisualBasic.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SurvLine.mdl.MdlRINEXTYPE;

namespace SurvLine.mdl
{
    internal class MdlRINEXTYPE
    {
        //==========================================================================================
        /*[VB]
        '*******************************************************************************
        'RINEXTYPE

        Option Explicit
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'def

        Public Const MAX_BUF As Long = 1024 'buffer size
        Public Const MAX_FNAME As Long = 61 'file name
        Public Const MAX_FNAME_SFX As Long = 4 'file name suffix (extension) <.YY(o,n,m)>

        Public Const MAX_SV_ As Long = 32 'number of satellite
        Public Const MAX_SV_4 As Long = 61 'number of satellite
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'def

        public const long MAX_BUF = 1024;       //'buffer size
        public const long MAX_FNAME = 61;       //'file name
        public const long MAX_FNAME_SFX = 4;    //'file name suffix (extension) <.YY(o,n,m)>

        public const long MAX_SV_ = 32;         //'number of satellite
        public const long MAX_SV_4 = 61;        //'number of satellite
        //==========================================================================================


        //==========================================================================================
        /*[VB]
        'rinex_type

        'RINEX file extension
        '--------------------
        Public Const RNX_OBS_EXTENSION As String = "o"
        Public Const RNX_NAV_EXTENSION As String = "n"
        Public Const RNX_GLO_EXTENSION As String = "g"
        Public Const RNX_SV_EXTENSION As String = "log"

        '2017/07/05 NS6000対応。'''''''''''''''''''''''''''''''''''''''''''''''''''''''
        'Public Const RNX_QZS_EXTENSION As String = "j"
        Public Const RNX_QZS_EXTENSION As String = "q"
        Public Const RNX_GAL_EXTENSION As String = "l"
        Public Const RNX_BEI_EXTENSION As String = "c"
        Public Const RNX_MIX_EXTENSION As String = "p"
        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

        'RINEX format size
        '-----------------
        Public Const RNX_STR As Long = 61
        Public Const RNX_CMT_STR As Long = 21
        Public Const RNX_MAX_STR As Long = (RNX_STR + RNX_CMT_STR - 1)

        'RINEX file name
        Public Const RNX_FILE_NAME_SRC As Long = 0 'use NGS file name
        Public Const RNX_FILE_NAME_STD As Long = 1 'use standard RINEX file name <ssssdddf.yy(o,n,m)>
        Public Const RNX_FILE_NAME_INP As Long = 2 'use input file name given by user

        'obs type
        Public Const RNX_OBS_MAX_TYPE As Long = 12
        Public Const RNX_OBS_TYPE_L1 As Long = &H1&
        Public Const RNX_OBS_TYPE_L2 As Long = &H2&
        Public Const RNX_OBS_TYPE_C1 As Long = &H3&
        Public Const RNX_OBS_TYPE_C2 As Long = &H4&
        Public Const RNX_OBS_TYPE_P1 As Long = &H5&
        Public Const RNX_OBS_TYPE_P2 As Long = &H6&
        Public Const RNX_OBS_TYPE_D1 As Long = &H7&
        Public Const RNX_OBS_TYPE_D2 As Long = &H8&
        Public Const RNX_OBS_TYPE_T1 As Long = &H9&
        Public Const RNX_OBS_TYPE_T2 As Long = &HA&
        Public Const RNX_OBS_TYPE_S1 As Long = &HB&
        Public Const RNX_OBS_TYPE_S2 As Long = &HC&
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'rinex_type

        //'RINEX file extension
        //'--------------------
        public const string RNX_OBS_EXTENSION = "o";
        public const string RNX_NAV_EXTENSION = "n";
        public const string RNX_GLO_EXTENSION = "g";
        public const string RNX_SV_EXTENSION = "log";

        //'2017/07/05 NS6000対応。'''''''''''''''''''''''''''''''''''''''''''''''''''''''
        //'Public Const RNX_QZS_EXTENSION As String = "j"
        public const string RNX_QZS_EXTENSION = "q";
        public const string RNX_GAL_EXTENSION = "l";
        public const string RNX_BEI_EXTENSION = "c";
        public const string RNX_MIX_EXTENSION = "p";
        //'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

        //'RINEX format size
        //'-----------------
        public const long RNX_STR = 61;
        public const long RNX_CMT_STR = 21;
        public const long RNX_MAX_STR = RNX_STR + RNX_CMT_STR - 1;

        //'RINEX file name
        public const long RNX_FILE_NAME_SRC = 0;    //'use NGS file name
        public const long RNX_FILE_NAME_STD = 1;    //'use standard RINEX file name <ssssdddf.yy(o,n,m)>
        public const long RNX_FILE_NAME_INP = 2;    //'use input file name given by user

        //'obs type
        public const long RNX_OBS_MAX_TYPE = 12;
        public const long RNX_OBS_TYPE_L1 = 0x01;
        public const long RNX_OBS_TYPE_L2 = 0x02;
        public const long RNX_OBS_TYPE_C1 = 0x03;
        public const long RNX_OBS_TYPE_C2 = 0x04;
        public const long RNX_OBS_TYPE_P1 = 0x05;
        public const long RNX_OBS_TYPE_P2 = 0x06;
        public const long RNX_OBS_TYPE_D1 = 0x07;
        public const long RNX_OBS_TYPE_D2 = 0x08;
        public const long RNX_OBS_TYPE_T1 = 0x09;
        public const long RNX_OBS_TYPE_T2 = 0x0a;
        public const long RNX_OBS_TYPE_S1 = 0x0b;
        public const long RNX_OBS_TYPE_S2 = 0x0c;
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'RINEX Input info
        '----------------
        Public Type RNX_INP
            src_fname As String * MAX_FNAME
            std_fname As String * MAX_FNAME
            usr_fname As String * MAX_FNAME
            extension As String * MAX_FNAME_SFX
            rnx_name_type As Byte
    
            run_by As String * RNX_STR
            observer As String * RNX_STR
            agency As String * RNX_STR
            mark_name As String * RNX_STR
            mark_num As String * RNX_STR
            rcv_num As String * RNX_STR
            rcv_type As String * RNX_STR
            rcv_ver As String * RNX_STR
            ant_num As String * RNX_STR
            ant_type As String * RNX_STR
            ant_measure As String * RNX_STR
            ant_hgt As Double
            X As Double
            Y As Double
            Z As Double
            leap_sec As Long
            week_num As Integer
            doppl_flag As Byte
            adj_time As Byte
    
            have_run_by As Byte
            have_observer As Byte
            have_agency As Byte
            have_mark_name  As Byte
            have_mark_num As Byte
            have_rcv_num As Byte
            have_rcv_type As Byte
            have_rcv_ver As Byte
            have_ant_num As Byte
            have_ant_type As Byte
            have_ant_measure As Byte
            have_ant_hgt As Byte
            pos_type As Byte
            have_leap_sec As Byte
            have_week_num As Byte
        End Type
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'RINEX Input info
        //'----------------
        public struct RNX_INP
        {
            public string src_fname;
            public string std_fname;
            public string usr_fname;
            public string extension;
            public byte rnx_name_type;


            public string run_by;
            public string observer;
            public string agency;
            public string mark_name;
            public string mark_num;
            public string rcv_num;
            public string rcv_type;
            public string rcv_ver;
            public string ant_num;
            public string ant_type;
            public string ant_measure;
            public double ant_hgt;
            public double X;
            public double Y;
            public double Z;
            public long leap_sec;
            public int week_num;
            public byte doppl_flag;
            public byte adj_time;


            public byte have_run_by;
            public byte have_observer;
            public byte have_agency;
            public byte have_mark_name;
            public byte have_mark_num;
            public byte have_rcv_num;
            public byte have_rcv_type;
            public byte have_rcv_ver;
            public byte have_ant_num;
            public byte have_ant_type;
            public byte have_ant_measure;
            public byte have_ant_hgt;
            public byte pos_type;
            public byte have_leap_sec;
            public byte have_week_num;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'RINEX Observation info
        '----------------------
        Public Type RNX_OBS_INFO
            have_pos As Byte
            Lat As Double
            Lon As Double
            Hgt As Double
            ant_dn As Double
            ant_de As Double
            ant_dh As Double
            elev_mask As Integer
            wl_fact_l1 As Byte
            wl_fact_l2 As Byte
            wl_fact As Byte
            obs_num_rnx As Integer
            obs_num As Integer
            obs_seq(RNX_OBS_MAX_TYPE - 1) As Byte
            session_name As String * RNX_STR
            have_glo As Byte
        End Type
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'RINEX Observation info
        //'----------------------
        public struct RNX_OBS_INFO
        {
            public byte have_pos;
            public double Lat;
            public double Lon;
            public double Hgt;
            public double ant_dn;
            public double ant_de;
            public double ant_dh;
            public int elev_mask;
            public byte wl_fact_l1;
            public byte wl_fact_l2;
            public byte wl_fact;
            public int obs_num_rnx;
            public int obs_num;
            //public byte[] obs_seq = new byte[RNX_OBS_MAX_TYPE - 1];
            public byte[] obs_seq;
            public string session_name;
            public byte have_glo;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'RINEX navigation info
        '---------------------
        Public Type RNX_NAV_INFO
            have_ion As Byte
            wn As Integer
            alpha0 As Double
            alpha1 As Double
            alpha2 As Double
            alpha3 As Double
            beta0 As Double
            beta1 As Double
            beta2 As Double
            beta3 As Double
            a_sub_0 As Double
            a_sub_1 As Double
            t_sub_ot As Double
        End Type
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'RINEX navigation info
        //'---------------------
        public struct RNX_NAV_INFO
        {
            public byte have_ion;
            public int wn;
            public double alpha0;
            public double alpha1;
            public double alpha2;
            public double alpha3;
            public double beta0;
            public double beta1;
            public double beta2;
            public double beta3;
            public double a_sub_0;
            public double a_sub_1;
            public double t_sub_ot;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'RINEX raw information
        '---------------------
        Public Type RNX_RAW_DATA2
            have_week As Byte
        '   week_cnt As Long
            week_cnt As Integer
            ref_week As Integer
            first_week As Integer
            end_week As Integer
            ref_tow As Double
            first_tow As Double
            end_tow As Double
            clk_offset_cnt As Long
            have_samp_t As Byte
            samp_time As Double
            samp_time_max As Double
            samp_time_min As Double
    
            have_l1code As Byte
            have_l2code As Byte
            have_l1phase As Byte
            have_l2phase As Byte
            have_l1snr As Byte
            have_l2snr As Byte
            have_l1doppler As Byte
            have_c1 As Byte
            have_p1 As Byte
            have_c2 As Byte
            have_p2 As Byte
            have_y2 As Byte
            have_as As Byte
            l1code_type As Byte
            l2code_type As Byte
            l1code_cnt(MAX_SV_) As Long
            l2code_cnt(MAX_SV_) As Long
            l1phase_cnt(MAX_SV_) As Long
            l2phase_cnt(MAX_SV_) As Long
            l1slip_cnt(MAX_SV_) As Long
            l2slip_cnt(MAX_SV_) As Long
            l1doppler_cnt(MAX_SV_) As Long
            sv_health(MAX_SV_) As Byte
    
            max_sv As Long
            min_sv As Long
            all_sv As Long
            epoch_cnt As Long
            epoch_time As Double
        End Type
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'RINEX raw information
        //'---------------------
        public struct RNX_RAW_DATA2
        {
            public byte have_week;
            //'   week_cnt As Long
            public int week_cnt;
            public int ref_week;
            public int first_week;
            public int end_week;
            public double ref_tow;
            public double first_tow;
            public double end_tow;
            public long clk_offset_cnt;
            public byte have_samp_t;
            public double samp_time;
            public double samp_time_max;
            public double samp_time_min;


            public byte have_l1code;
            public byte have_l2code;
            public byte have_l1phase;
            public byte have_l2phase;
            public byte have_l1snr;
            public byte have_l2snr;
            public byte have_l1doppler;
            public byte have_c1;
            public byte have_p1;
            public byte have_c2;
            public byte have_p2;
            public byte have_y2;
            public byte have_as;
            public byte l1code_type;
            public byte l2code_type;
            //public long[] l1code_cnt = new long[MAX_SV_];
            public long[] l1code_cnt;
            //public long[] l2code_cnt = new long[MAX_SV_];
            public long[] l2code_cnt;
            //public long[] l1phase_cnt = new long[MAX_SV_];
            public long[] l1phase_cnt;
            //public long[] l2phase_cnt = new long[MAX_SV_];
            public long[] l2phase_cnt;
            //public long[] l1slip_cnt = new long[MAX_SV_];
            public long[] l1slip_cnt;
            //public long[] l2slip_cnt = new long[MAX_SV_];
            public long[] l2slip_cnt;
            //public long[] l1doppler_cnt = new long[MAX_SV_];
            public long[] l1doppler_cnt;
            //public byte[] sv_health = new byte[MAX_SV_];
            public byte[] sv_health;


            public long max_sv;
            public long min_sv;
            public long all_sv;
            public long epoch_cnt;
            public double epoch_time;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'RINEX raw information
        '---------------------
        Public Type RNX_RAW_DATA4
            have_week As Byte
        '   week_cnt As Long
            week_cnt As Integer
            ref_week As Integer
            first_week As Integer
            end_week As Integer
            ref_tow As Double
            first_tow As Double
            end_tow As Double
            clk_offset_cnt As Long
            have_samp_t As Byte
            samp_time As Double
            samp_time_max As Double
            samp_time_min As Double
    
            have_l1code As Byte
            have_l2code As Byte
            have_l1phase As Byte
            have_l2phase As Byte
            have_l1snr As Byte
            have_l2snr As Byte
            have_l1doppler As Byte
            have_l2doppler As Byte
            have_c1 As Byte
            have_p1 As Byte
            have_c2 As Byte
            have_p2 As Byte
            have_y2 As Byte
            have_as As Byte
            l1code_type As Byte
            l2code_type As Byte
            l1code_c1_cnt(MAX_SV_4) As Long
            l1code_p1_cnt(MAX_SV_4) As Long
            l2code_c2_cnt(MAX_SV_4) As Long
            l2code_p2_cnt(MAX_SV_4) As Long
            l1phase_cnt(MAX_SV_4) As Long
            l2phase_cnt(MAX_SV_4) As Long
            l1slip_cnt(MAX_SV_4) As Long
            l2slip_cnt(MAX_SV_4) As Long
            l1doppler_cnt(MAX_SV_4) As Long
            l2doppler_cnt(MAX_SV_4) As Long
            l1snr_cnt(MAX_SV_4) As Long
            l2snr_cnt(MAX_SV_4) As Long
            sv_health(MAX_SV_4) As Byte
    
            max_sv As Long
            min_sv As Long
            all_sv As Long
            epoch_cnt As Long
            epoch_time As Double
        End Type
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'RINEX raw information
        //'---------------------
        public struct RNX_RAW_DATA4
        {
            public byte have_week;
            //'   week_cnt As Long
            public int week_cnt;
            public int ref_week;
            public int first_week;
            public int end_week;
            public double ref_tow;
            public double first_tow;
            public double end_tow;
            public long clk_offset_cnt;
            public byte have_samp_t;
            public double samp_time;
            public double samp_time_max;
            public double samp_time_min;


            public byte have_l1code;
            public byte have_l2code;
            public byte have_l1phase;
            public byte have_l2phase;
            public byte have_l1snr;
            public byte have_l2snr;
            public byte have_l1doppler;
            public byte have_l2doppler;
            public byte have_c1;
            public byte have_p1;
            public byte have_c2;
            public byte have_p2;
            public byte have_y2;
            public byte have_as;
            public byte l1code_type;
            public byte l2code_type;
            //public long[] l1code_c1_cnt[MAX_SV_4];
            public long[] l1code_c1_cnt;
            //public long[] l1code_p1_cnt[MAX_SV_4];
            public long[] l1code_p1_cnt;
            //public long[] l2code_c2_cnt[MAX_SV_4];
            public long[] l2code_c2_cnt;
            //public long[] l2code_p2_cnt[MAX_SV_4];
            public long[] l2code_p2_cnt;
            //public long[] l1phase_cnt[MAX_SV_4];
            public long[] l1phase_cnt;
            //public long[] l2phase_cnt[MAX_SV_4];
            public long[] l2phase_cnt;
            //public long[] l1slip_cnt[MAX_SV_4];
            public long[] l1slip_cnt;
            //public long[] l2slip_cnt[MAX_SV_4];
            public long[] l2slip_cnt;
            //public long[] l1doppler_cnt[MAX_SV_4];
            public long[] l1doppler_cnt;
            //public long[] l2doppler_cnt[MAX_SV_4];
            public long[] l2doppler_cnt;
            //public long[] l1snr_cnt[MAX_SV_4];
            public long[] l1snr_cnt;
            //public long[] l2snr_cnt[MAX_SV_4];
            public long[] l2snr_cnt;
            //public byte[] sv_health[MAX_SV_4];
            public byte[] sv_health;


            public long max_sv;
            public long min_sv;
            public long all_sv;
            public long epoch_cnt;
            public double epoch_time;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'RINEX Ephemeris data
        '--------------------
        Public Type RNX_EPH_DATA2
            have_eph As Byte
            num_sv As Long
            eph_cnt(MAX_SV_) As Long
            eph_all_cnt(MAX_SV_) As Long
            last_iode(MAX_SV_) As Byte
            last_iodc(MAX_SV_) As Integer
            last_wn(MAX_SV_) As Integer
            data_flag(MAX_SV_) As Byte
            ch_code(MAX_SV_) As Byte
            Health(MAX_SV_) As Byte
            ura(MAX_SV_) As Byte
            fit(MAX_SV_) As Byte
        End Type

        Public Type RNX_EPH_DATA4
            have_eph As Byte
            num_sv As Long
            eph_cnt(MAX_SV_4) As Long
            eph_all_cnt(MAX_SV_4) As Long
            last_iode(MAX_SV_4) As Byte
            last_iodc(MAX_SV_4) As Integer
            last_wn(MAX_SV_4) As Integer
            data_flag(MAX_SV_4) As Byte
            ch_code(MAX_SV_4) As Byte
            Health(MAX_SV_4) As Byte
            ura(MAX_SV_4) As Byte
            fit(MAX_SV_4) As Byte
        End Type
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'RINEX Ephemeris data
        //'--------------------
        public struct RNX_EPH_DATA2
        {
            public byte have_eph;
            public long num_sv;
            //public long[] eph_cnt[MAX_SV_];
            public long[] eph_cnt;
            //public long[] eph_all_cnt[MAX_SV_];
            public long[] eph_all_cnt;
            //public byte[] last_iode[MAX_SV_];
            public byte[] last_iode;
            //public int[] last_iodc[MAX_SV_];
            public int[] last_iodc;
            //public int[] last_wn[MAX_SV_];
            public int[] last_wn;
            //public byte[] data_flag[MAX_SV_];
            public byte[] data_flag;
            //public byte[] ch_code[MAX_SV_];
            public byte[] ch_code;
            //public byte[] Health[MAX_SV_];
            public byte[] Health;
            //public byte[] ura[MAX_SV_];
            public byte[] ura;
            //public byte[] fit[MAX_SV_];
            public byte[] fit;
        }

        public struct RNX_EPH_DATA4
        {
            public byte have_eph;
            public long num_sv;
            //public long[] eph_cnt[MAX_SV_4];
            public long[] eph_cnt;
            //public long[] eph_all_cnt[MAX_SV_4];
            public long[] eph_all_cnt;
            //public byte[] last_iode[MAX_SV_4];
            public byte[] last_iode;
            //public int[] last_iodc[MAX_SV_4];
            public int[] last_iodc;
            //public int[] last_wn[MAX_SV_4];
            public int[] last_wn;
            //public byte[] data_flag[MAX_SV_4];
            public byte[] data_flag;
            //public byte[] ch_code[MAX_SV_4];
            public byte[] ch_code;
            //public byte[] Health[MAX_SV_4];
            public byte[] Health;
            //public byte[] ura[MAX_SV_4];
            public byte[] ura;
            //public byte[] fit[MAX_SV_4];
            public byte[] fit;
        }
        //==========================================================================================
    }
}
