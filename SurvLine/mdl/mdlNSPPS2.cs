//23/12/26 K.setoguchi@NV---------->>>>>>>>>>
//'NSPPS2

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace SurvLine
{
    internal class MdlNSPPS2
    {
        ///Option Explicit

        //  '2017/06/08 間違って使用しないようにコメントアウトする。
        //  'Public Declare Function NSAnalysis2 Lib "NSPPS2" (ByRef pNsPpsIn As NSPPS2_IN, ByRef pNsPpsOut As NSPPS2_OUT) As Long
        //  'Public Declare Sub NSGetDepPattern2 Lib "NSPPS2" (ByVal bStrPoint As Long, ByVal nIndex As Long, ByRef pDepPatterns As NSPPS2_DEP_PATTERN)
        //  'Public Declare Sub NSTerminate2 Lib "NSPPS2" ()
        //  'Public Declare Function NSGetVersion2 Lib "NSPPS2" () As Single

        //'Linear Combinations for Solutions
        public const long PPS_L1 = 0;       // As Long
        public const long PPS_P1L1 = 1;     // As Long
        public const long PPS_L1L2 = 2;     // As Long
        public const long PPS_P1L1P2L2 = 3; //As Long
        public const long PPS_LC_NONE = 8;  // As Long

        public struct NSPPS2_OBS_INFO
        {
            public long All;            // As Long  'number of all observations
            public long Used;           // As Long  'number of observations used
        }

#if false
Public Type NSPPS2_STA_INFO
    posXYZSet As Long 'if the posXYZ value should be used for a priori coordinate
    Reserved As Long '
    posXYZ(2) As Double 'a priori coordinate of marker XYZ (meters)
    antName As String* MAX_PATH 'name of antenna if not NULL
    antHENSet As Long 'if antHEN should be used for offset of antenna from marker
    antHEN(2) As Double 'offset of the antenna from the marker Height,East,North (meters)
End Type

Public Type NSPPS2_ANT_INFO
    Name As String * MAX_PATH 'name of antenna used to lookup antenna in phasecc file
    HEN(2) As Single 'Antenna offset from marker to ARP (HEN)
    OffsetL1(2) As Single 'Phase center offset for L1 (HEN meters)
    OffsetL2(2) As Single 'PHase cetner offset for L1 (HEN meters)
    numEle As Long 'number of elevation bins for elevation dep pattern
    Reserved As Long '
End Type

Public Type NSPPS2_DEP_PATTERN
    PatternEle As Single 'elevations for phase pattern (deg)
    PatternL1 As Single 'phase center pattern for L1 (meters)
    PatternL2 As Single ' phase center pattern for L2 (meters)
    Reserved As Long '
End Type

Public Type NSPPS2_IN
    ObsFileRef As String * MAX_PATH 'full path to RINEX file to use for reference station
    ObsFileRov As String* MAX_PATH 'full path to RINEX file to use for estimated station
    'tmpPath As String * MAX_PATH 'directory for temporary files
    LogPath As String* MAX_PATH 'directory for log folder
    BrdFile As String* MAX_PATH 'full path to RINEX Navigation file if not NULL
    BrdFile2 As String* MAX_PATH 'full path to RINEX Navigation file (GLONASS) if not NULL
    PreFile As String* MAX_PATH 'full path to precise orbit file if not NULL
    AntFile As String* MAX_PATH 'full path to antenna phase pattern file
    Reserved As Long '
    BegTime As NSPPS_DATE 'time window beginning
    EndTime As NSPPS_DATE 'time window ending
    RefStaInfo As NSPPS2_STA_INFO 'Information about the reference station
    RovStaInfo As NSPPS2_STA_INFO 'Information about the estimated station
    Sampling As Single 'sampling rate to use (seconds)
    EleMask As Single 'elevation mask to use in processing data
    'maxResid_1 As Single 'maximal residuum(first LC)
    'maxResid_2 As Single 'maximal residuum(second LC)
    MaxResidCode As Single 'maximal residuum (code)
    MaxResidPhase As Single 'maximal residuum (phase)
    MinPs As Double 'minimal probability of success in LAMBDA
    MinRatio As Single 'minimal ratio in LAMBDA
    Lc As Long 'linear combination to use in processing
    'trpModel As NSPPS2_TROP 'troposphere model parameters
    'trpEst As NSPPS2_TROP_EST_PARAM 'troposphere estimation parameters
    TropoModel As Long 'troposphere model
    TropoAprSig As Single 'troposphere a priori sigma
    TropoNoise As Single 'troposphere noise
    IonoAprSig As Single 'iono a priori sigma
    ExcludeSV_gps As Long 'SVs to exclude from processing
    ExcludeSV_glo As Long 'SVs (GLONASS) to exclude from processing
    StatusFunc As Long 'function pointer to status routine
    pProgress As Long '
    KinFlag As Long 'moving rover
    FixAmbs As Long 'fix ambiguities?
    Glonass As Long 'process GLONASS data
    SatInfoFile As String* MAX_PATH 'satellite info (GLONASS channels)
End Type

Public Type NSPPS2_OUT
    BegTime As NSPPS_DATE
    EndTime As NSPPS_DATE
    NumEpochUsed As Long
    NumEpochRejected As Long
    ObsInfo(PPS_MAXPRN2) As NSPPS2_OBS_INFO
    Sampling As Single
    EleMask As Single
    'maxResid_1 As Single
    'maxResid_2 As Single
    MinPs As Double 'minimal probability of success in LAMBDA
    MinRatio As Single
    Lc As Long
    SolType As Long
    'AmbPercentage As Long
    Reserved As Long
    RefAntenna As NSPPS2_ANT_INFO
    RovAntenna As NSPPS2_ANT_INFO
    ReferenceXYZ(2) As Double
    VectorXYZ(2) As Double
    Cov(5) As Single
    CovNEU(5) As Single
    RMS As Single
    'ratio_fixfloat As Single
    Ratio_lambda As Single
    'RDOP As Single
    'numAmb As Long
    'numTrpResult As Long
    'trpModel As NSPPS2_TROP
    'trpEst As NSPPS2_TROP_EST_PARAM
    NumKinResult As Long
    reserved2 As Long
End Type

#endif


    }
}
//<<<<<<<<<-----------23/12/26 K.setoguchi@NV
