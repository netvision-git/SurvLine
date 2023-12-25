
//23/12/24 K.setoguchi@NV---------->>>>>>>>>>


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace SurvLine
{
    public class MdlRINEXTYPE
    {


        ///'*******************************************************************************
        /// <summary>
        /// 'RINEXTYPE
        ///
        /// Option Explicit
        ///
        /// 'def
        //
        /// 
        /// </summary>
        public const long MAX_BUF= 1024;        // As Long 'buffer size
        public const long MAX_FNAME = 61;       // As Long'file name
        public const long MAX_FNAME_SFX = 4;    //'file name suffix (extension) <.YY(o,n,m)>

        public const long MAX_SV_ = 32;           // As Long 'number of satellite
        public const long MAX_SV_4 = 61;          // As Long 'number of satellite



        //'rinex_type

        //'RINEX file extension
        //'--------------------
        public const string RNX_OBS_EXTENSION = "o";        // As String
        public const string RNX_NAV_EXTENSION = "n";        // As String
        public const string RNX_GLO_EXTENSION = "g";        // As String
        public const string RNX_SV_EXTENSION = "log";       // As String

        //'2017/07/05 NS6000対応。'''''''''''''''''''''''''''''''''''''''''''''''''''''''
        //'public const string RNX_QZS_EXTENSION As String = "j"
        public const string RNX_QZS_EXTENSION = "q";        // As String
        public const string RNX_GAL_EXTENSION = "l";        // As String
        public const string RNX_BEI_EXTENSION = "c";        // As String
        public const string RNX_MIX_EXTENSION = "p";        // As String
                                                            //'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                                                            //'RINEX format size
                                                            //'-----------------
        public const long RNX_STR = 61;             // As Long
        public const long RNX_CMT_STR = 21;         // As Long
        public const long RNX_MAX_STR = 61 + 21 - 1;    // RNX_STR + RNX_CMT_STR - 1;

        //'RINEX file name
        public const long RNX_FILE_NAME_SRC = 0;    // As Long  'use NGS file name
        public const long RNX_FILE_NAME_STD = 1;    // As Long  'use standard RINEX file name <ssssdddf.yy(o,n,m)>
        public const long RNX_FILE_NAME_INP = 2;    // As Long  'use input file name given by user

        //'obs type
        public const long RNX_OBS_MAX_TYPE = 12;    // As Long
        public const long RNX_OBS_TYPE_L1 = 0x1;    // As Long &H1&
        public const long RNX_OBS_TYPE_L2 = 0x2;    // As Long &H2&
        public const long RNX_OBS_TYPE_C1 = 0x3;    // As Long &H3&
        public const long RNX_OBS_TYPE_C2 = 0x4;    // As Long &H4&
        public const long RNX_OBS_TYPE_P1 = 0x5;    // As Long &H5&
        public const long RNX_OBS_TYPE_P2 = 0x6;    // As Long &H6&
        public const long RNX_OBS_TYPE_D1 = 0x7;    // As Long &H7&
        public const long RNX_OBS_TYPE_D2 = 0x8;    // As Long &H8&
        public const long RNX_OBS_TYPE_T1 = 0x9;    // As Long &H9&
        public const long RNX_OBS_TYPE_T2 = 0xA;    // As Long &HA&
        public const long RNX_OBS_TYPE_S1 = 0xB;    // As Long &HB&
        public const long RNX_OBS_TYPE_S2 = 0xC;    // As Long &HC&


        public struct aaaaaaaaaaaaAMBINF_T_STRUCT        //AmbInfo_Load
        {
            public long Year;       //beg/End'時間、年。
            public long Month;      //beg/End'時間、月。
            public long Day;        //beg/End'時間、日。
            public long Hour;       //beg/End'時間、時。
            public long Min;        //beg/End'時間、分。
            public double Sec;      //beg/End'時間、秒。
        }
        //-----------------------------------------

        //'RINEX Input info
        //'----------------
        public struct RNX_INP
        {
            public string src_fname;    //src_fname As String* MAX_FNAME
            public string std_fname;    //std_fname As String* MAX_FNAME
            public string usr_fname;    //usr_fname As String* MAX_FNAME
            public string extension;    //extension As String* MAX_FNAME_SFX
            public byte rnx_name_type;  //As Byte
            public string run_by;       //run_by As String* RNX_STR
            public string observer;     //observer As String* RNX_STR
            public string agency;       //agency As String* RNX_STR
            public string mark_name;    //mark_name As String* RNX_STR
            public string mark_num;     //mark_num As String* RNX_STR
            public string rcv_num;      //rcv_num As String* RNX_STR
            public string rcv_type;     //rcv_type As String* RNX_STR
            public string rcv_ver;      //rcv_ver As String* RNX_STR
            public string ant_num;      //ant_num As String* RNX_STR
            public string ant_type;     //ant_type As String* RNX_STR
            public string ant_measure;  //ant_measure As String* RNX_STR

            public double ant_hgt;      //As Double
            public double X;            //As Double
            public double Y;            //As Double
            public double Z;            //As Double
            public long leap_sec;       //As Long
            public int week_num;        //As Integer
            public byte doppl_flag;     //As Byte
            public byte adj_time;       //As Byte

            public byte have_run_by;    //As Byte
            public byte have_observer;  //As Byte
            public byte have_agency;    //As Byte
            public byte have_mark_name; //As Byte
            public byte have_mark_num;  //As Byte
            public byte have_rcv_num;   //As Byte
            public byte have_rcv_type;  //As Byte
            public byte have_rcv_ver;   //As Byte
            public byte have_ant_num;   //As Byte
            public byte have_ant_type;  //As Byte
            public byte have_ant_measure;   //As Byte
            public byte have_ant_hgt;   //As Byte
            public byte pos_type;       //As Byte
            public byte have_leap_sec;  //As Byte
            public byte have_week_num;  //As Byte
        }

        //'RINEX Observation info
        //'----------------------
        public struct RNX_OBS_INFO
        {
            public byte have_pos;       //As Byte
            public double Lat;          //As Double
            public double Lon;          //As Double
            public double Hgt;          //As Double
            public double ant_dn;       //As Double
            public double ant_de;       //As Double
            public double ant_dh;       //As Double
            public int elev_mask;       //As Integer
            public byte wl_fact_l1;     //As Byte
            public byte wl_fact_l2;     //As Byte
            public byte wl_fact;        //As Byte
            public int obs_num_rnx;     //As Integer
            public int obs_num;         //As Integer
            //
            public List<byte> obs_seq;  //obs_seq(RNX_OBS_MAX_TYPE - 1);   //As Byte
            //
            public string session_name; //As String* RNX_STR
            public byte have_glo;       //As Byte
        }

        //'RINEX navigation info
        //'---------------------
        public struct RNX_NAV_INFO
        {
            public byte have_ion;       //As Byte
            public int wn;              //As Integer
            public double alpha0;       //As Double
            public double alpha1;       //As Double
            public double alpha2;       //As Double
            public double alpha3;       //As Double
            public double beta0;        //As Double
            public double beta1;        //As Double
            public double beta2;        //As Double
            public double beta3;        //As Double
            public double a_sub_0;      //As Double
            public double a_sub_1;      //As Double
            public double t_sub_ot;     //As Double
        }

        //'RINEX raw information
        //'---------------------
        public struct RNX_RAW_DATA2
        {
            public byte have_week;      //As Byte
            //'   week_cnt As Long
            public int week_cnt;        //As Integer
            public int ref_week;        //As Integer
            public int first_week;      //As Integer
            public int end_week;        //As Integer
            public double ref_tow;      //As Double
            public double first_tow;    //As Double
            public double end_tow;      //As Double
            public long clk_offset_cnt; //As Long
            public byte have_samp_t;    //As Byte
            public double samp_time;    //As Double
            public double samp_time_max;    //As Double
            public double samp_time_min;    //As Double


            public byte have_l1code;    //As Byte
            public byte have_l2code;    //As Byte
            public byte have_l1phase;   //As Byte
            public byte have_l2phase;   //As Byte
            public byte have_l1snr;     //As Byte
            public byte have_l2snr;     //As Byte
            public byte have_l1doppler; //As Byte
            public byte have_c1;        //As Byte
            public byte have_p1;        //As Byte
            public byte have_c2;        //As Byte
            public byte have_p2;        //As Byte
            public byte have_y2;        //As Byte
            public byte have_as;        //As Byte
            public byte l1code_type;    //As Byte
            public byte l2code_type;    //As Byte
            public List<long> l1code_cnt;    //l1code_cnt(MAX_SV_) As Long
            public List<long> l2code_cnt;    //l2code_cnt(MAX_SV_) As Long
            public List<long> l1phase_cnt;   //l1phase_cnt(MAX_SV_) As Long
            public List<long> l2phase_cnt;   //l2phase_cnt(MAX_SV_) As Long

            public List<long> l1slip_cnt;    //l1slip_cnt(MAX_SV_) As Long
            public List<long> l2slip_cnt;    //l2slip_cnt(MAX_SV_) As Long
            public List<long> l1doppler_cnt; //l1doppler_cnt(MAX_SV_) As Long
            public List<long> sv_health;     //sv_health(MAX_SV_) As Byte


            public long max_sv;         //As Long
            public long min_sv;         //As Long
            public long all_sv;         //As Long
            public long epoch_cnt;      //As Long
            public double epoch_time;   //As Double

        }

        //'RINEX raw information
        //'---------------------
        public struct RNX_RAW_DATA4
        {
            public byte have_week;      //As Byte
            //'   week_cnt As Long
            public int week_cnt;        //As Integer
            public int ref_week;        //As Integer
            public int first_week;      //As Integer
            public int end_week;        //As Integer
            public double ref_tow;      //As Double
            public double first_tow;    //As Double
            public double end_tow;      //As Double
            public long clk_offset_cnt; //As Long
            public byte have_samp_t;    //As Byte
            public double samp_time;    //As Double
            public double samp_time_max;    //As Double
            public double samp_time_min;    //As Double

            public byte have_l1code;    //As Byte
            public byte have_l2code;    //As Byte
            public byte have_l1phase;   //As Byte
            public byte have_l2phase;   //As Byte
            public byte have_l1snr;     //As Byte
            public byte have_l2snr;     //As Byte
            public byte have_l1doppler; //As Byte
            public byte have_l2doppler; //As Byte
            public byte have_c1;        //As Byte
            public byte have_p1;        //As Byte
            public byte have_c2;        //As Byte
            public byte have_p2;        //As Byte
            public byte have_y2;        //As Byte
            public byte have_as;        //As Byte
            public byte l1code_type;    //As Byte
            public byte l2code_type;    //As Byte

            public List<long> l1code_c1_cnt;    //l1code_c1_cnt(MAX_SV_4) As Long


            public List<long> l1code_p1_cnt;    //l1code_p1_cnt(MAX_SV_4) As Long
            public List<long> l2code_c2_cnt;    //l2code_c2_cnt(MAX_SV_4) As Long
            public List<long> l2code_p2_cnt;    //l2code_p2_cnt(MAX_SV_4) As Long
            public List<long> l1phase_cnt;      //l1phase_cnt(MAX_SV_4) As Long
            public List<long> l2phase_cnt;      //l2phase_cnt(MAX_SV_4) As Long
            public List<long> l1slip_cnt;       //l1slip_cnt(MAX_SV_4) As Long
            public List<long> l2slip_cnt;       //l2slip_cnt(MAX_SV_4) As Long
            public List<long> l1doppler_cnt;    //l1doppler_cnt(MAX_SV_4) As Long
            public List<long> l2doppler_cnt;    //l2doppler_cnt(MAX_SV_4) As Long
            public List<long> l1snr_cnt;        //l1snr_cnt(MAX_SV_4) As Long
            public List<long> l2snr_cnt;        //l2snr_cnt(MAX_SV_4) As Long
            public List<byte> sv_health;        //sv_health(MAX_SV_4) As Byte

            public long max_sv;     //As Long
            public long min_sv;     //As Long
            public long all_sv;     //As Long
            public long epoch_cnt;  //As Long
            public double epoch_time;   //As Double
        }


        //'RINEX Ephemeris data
        //'--------------------
        public struct RNX_EPH_DATA2
        {
            public byte have_eph;           //As Byte
            public long num_sv;             //As Long
            public List<long> eph_cnt;      //eph_cnt(MAX_SV_) As Long
            public List<long> eph_all_cnt;  //eph_all_cnt(MAX_SV_) As Long
            public List<byte> last_iode;    //last_iode(MAX_SV_) As Byte
            public List<int> last_iodc;     //last_iodc(MAX_SV_) As Integer
            public List<int> last_wn;       //last_wn(MAX_SV_) As Integer
            public List<byte> data_flag;    //data_flag(MAX_SV_) As Byte
            public List<byte> ch_code;      //ch_code(MAX_SV_) As Byte
            public List<byte> Health;       //Health(MAX_SV_) As Byte
            public List<byte> ura;          //ura(MAX_SV_) As Byte
            public List<byte> fit;          //fit(MAX_SV_) As Byte
        }

        public struct RNX_EPH_DATA4
        {
            public byte have_eph;       //As Byte
            public long num_sv;         //As Long
            public List<long> eph_cnt;      //eph_cnt(MAX_SV_4) As Long
            public List<long> eph_all_cnt;  //eph_all_cnt(MAX_SV_4) As Long
            public List<byte> last_iode;    //last_iode(MAX_SV_4) As Byte
            public List<int> last_iodc;     //last_iodc(MAX_SV_4) As Integer
            public List<int> last_wn;       //last_wn(MAX_SV_4) As Integer
            public List<byte> data_flag;    //data_flag(MAX_SV_4) As Byte
            public List<byte> ch_code;      //ch_code(MAX_SV_4) As Byte
            public List<byte> Health;       //Health(MAX_SV_4) As Byte
            public List<byte> ura;          //ura(MAX_SV_4) As Byte
            public List<byte> fit;          //fit(MAX_SV_4) As Byte
        }


    }
}
//<<<<<<<<<-----------23/12/24 K.setoguchi@NV
