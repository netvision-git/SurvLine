using Microsoft.VisualBasic;
using Microsoft.VisualBasic.ApplicationServices;
using Microsoft.VisualBasic.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static SurvLine.DepPattern;
using static SurvLine.mdl.MdlNSPPS;

namespace SurvLine.mdl
{
    internal class MdlNSPPS2
    {
        //==========================================================================================
        /*[VB]
        '*******************************************************************************
        'NSPPS2

        Option Explicit
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '2017/06/08 間違って使用しないようにコメントアウトする。
        'Public Declare Function NSAnalysis2 Lib "NSPPS2" (ByRef pNsPpsIn As NSPPS2_IN, ByRef pNsPpsOut As NSPPS2_OUT) As Long
        'Public Declare Sub NSGetDepPattern2 Lib "NSPPS2" (ByVal bStrPoint As Long, ByVal nIndex As Long, ByRef pDepPatterns As NSPPS2_DEP_PATTERN)
        'Public Declare Sub NSTerminate2 Lib "NSPPS2" ()
        'Public Declare Function NSGetVersion2 Lib "NSPPS2" () As Single
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'Linear Combinations for Solutions
        Public Const PPS_L1 As Long = 0
        Public Const PPS_P1L1 As Long = 1
        Public Const PPS_L1L2 As Long = 2
        Public Const PPS_P1L1P2L2 As Long = 3
        Public Const PPS_LC_NONE As Long = 8
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'Linear Combinations for Solutions
        public const long PPS_L1 = 0;
        public const long PPS_P1L1 = 1;
        public const long PPS_L1L2 = 2;
        public const long PPS_P1L1P2L2 = 3;
        public const long PPS_LC_NONE = 8;
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        Public Type NSPPS2_OBS_INFO
            All As Long 'number of all observations
            Used As Long 'number of observations used
        End Type
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        public struct NSPPS2_OBS_INFO
        {
            public long All;    //'number of all observations
            public long Used;   //'number of observations used
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        Public Type NSPPS2_STA_INFO
            posXYZSet As Long 'if the posXYZ value should be used for a priori coordinate
            Reserved As Long '
            posXYZ(2) As Double 'a priori coordinate of marker XYZ (meters)
            antName As String * MAX_PATH 'name of antenna if not NULL
            antHENSet As Long 'if antHEN should be used for offset of antenna from marker
            antHEN(2) As Double 'offset of the antenna from the marker Height,East,North (meters)
        End Type
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        public struct NSPPS2_STA_INFO
        {
            public long posXYZSet;              //'if the posXYZ value should be used for a priori coordinate
            public long Reserved;               //'
            public double[] posXYZ;             //'a priori coordinate of marker XYZ (meters)
            public string antName;              //'name of antenna if not NULL
            public long antHENSet;              //'if antHEN should be used for offset of antenna from marker
            public double[] antHEN;             //'offset of the antenna from the marker Height,East,North (meters)
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        Public Type NSPPS2_ANT_INFO
            Name As String * MAX_PATH 'name of antenna used to lookup antenna in phasecc file
            HEN(2) As Single 'Antenna offset from marker to ARP (HEN)
            OffsetL1(2) As Single 'Phase center offset for L1 (HEN meters)
            OffsetL2(2) As Single 'PHase cetner offset for L1 (HEN meters)
            numEle As Long 'number of elevation bins for elevation dep pattern
            Reserved As Long '
        End Type
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        public struct NSPPS2_ANT_INFO
        {
            public string Name;             //'name of antenna used to lookup antenna in phasecc file
            public float[] HEN;             //'Antenna offset from marker to ARP (HEN)
            public float[] OffsetL1;        //'Phase center offset for L1 (HEN meters)
            public float[] OffsetL2;        //'PHase cetner offset for L1 (HEN meters)
            public long numEle;             //'number of elevation bins for elevation dep pattern
            public long Reserved;           //'
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        Public Type NSPPS2_DEP_PATTERN
            PatternEle As Single 'elevations for phase pattern (deg)
            PatternL1 As Single 'phase center pattern for L1 (meters)
            PatternL2 As Single ' phase center pattern for L2 (meters)
            Reserved As Long '
        End Type
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        public struct NSPPS2_DEP_PATTERN
        {
            public float PatternEle;        //'elevations for phase pattern (deg)
            public float PatternL1;         //'phase center pattern for L1 (meters)
            public float PatternL2;         //' phase center pattern for L2 (meters)
            public long Reserved;           //'
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        Public Type NSPPS2_IN
            ObsFileRef As String * MAX_PATH 'full path to RINEX file to use for reference station
            ObsFileRov As String * MAX_PATH 'full path to RINEX file to use for estimated station
            'tmpPath As String * MAX_PATH 'directory for temporary files
            LogPath As String * MAX_PATH 'directory for log folder
            BrdFile As String * MAX_PATH 'full path to RINEX Navigation file if not NULL
            BrdFile2 As String * MAX_PATH 'full path to RINEX Navigation file (GLONASS) if not NULL
            PreFile As String * MAX_PATH 'full path to precise orbit file if not NULL
            AntFile As String * MAX_PATH 'full path to antenna phase pattern file
            Reserved As Long '
            BegTime As NSPPS_DATE 'time window beginning
            EndTime As NSPPS_DATE 'time window ending
            RefStaInfo As NSPPS2_STA_INFO 'Information about the reference station
            RovStaInfo As NSPPS2_STA_INFO 'Information about the estimated station
            Sampling As Single 'sampling rate to use (seconds)
            EleMask As Single 'elevation mask to use in processing data
            'maxResid_1 As Single 'maximal residuum (first LC)
            'maxResid_2 As Single 'maximal residuum (second LC)
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
            SatInfoFile As String * MAX_PATH 'satellite info (GLONASS channels)
        End Type
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        public struct NSPPS2_IN
        {
            public string ObsFileRef;               //'full path to RINEX file to use for reference station
            public string ObsFileRov;               //'full path to RINEX file to use for estimated station
            //'tmpPath As String * MAX_PATH 'directory for temporary files
            public string LogPath;                  //'directory for log folder
            public string BrdFile;                  //'full path to RINEX Navigation file if not NULL
            public string BrdFile2;                 //'full path to RINEX Navigation file (GLONASS) if not NULL
            public string PreFile;                  //'full path to precise orbit file if not NULL
            public string AntFile;                  //'full path to antenna phase pattern file
            public long Reserved;                   //'
            public NSPPS_DATE BegTime;              //'time window beginning
            public NSPPS_DATE EndTime;              //'time window ending
            public NSPPS2_STA_INFO RefStaInfo;      //'Information about the reference station
            public NSPPS2_STA_INFO RovStaInfo;      //'Information about the estimated station
            public float Sampling;                  //'sampling rate to use (seconds)
            public float EleMask;                   //'elevation mask to use in processing data
            //'maxResid_1 As Single 'maximal residuum(first LC)
            //'maxResid_2 As Single 'maximal residuum(second LC)
            public float MaxResidCode;              //'maximal residuum (code)
            public float MaxResidPhase;             //'maximal residuum (phase)
            public double MinPs;                    //'minimal probability of success in LAMBDA
            public float MinRatio;                  //'minimal ratio in LAMBDA
            public long Lc;                         //'linear combination to use in processing
            //'trpModel As NSPPS2_TROP 'troposphere model parameters
            //'trpEst As NSPPS2_TROP_EST_PARAM 'troposphere estimation parameters
            public long TropoModel;                 //'troposphere model
            public float TropoAprSig;               //'troposphere a priori sigma
            public float TropoNoise;                //'troposphere noise
            public float IonoAprSig;                //'iono a priori sigma
            public long ExcludeSV_gps;              //'SVs to exclude from processing
            public long ExcludeSV_glo;              //'SVs (GLONASS) to exclude from processing
            public long StatusFunc;                 //'function pointer to status routine
            public long pProgress;                  //'
            public long KinFlag;                    //'moving rover
            public long FixAmbs;                    //'fix ambiguities?
            public long Glonass;                    //'process GLONASS data
            public string SatInfoFile;              //'satellite info (GLONASS channels)
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
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
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        public struct NSPPS2_OUT
        {
            public NSPPS_DATE BegTime;
            public NSPPS_DATE EndTime;
            public long NumEpochUsed;
            public long NumEpochRejected;
            public NSPPS2_OBS_INFO[] ObsInfo;
            public float Sampling;
            public float EleMask;
            //'maxResid_1 As Single
            //'maxResid_2 As Single
            public double MinPs;                //'minimal probability of success in LAMBDA
            public float MinRatio;
            public long Lc;
            public long SolType;
            //'AmbPercentage As Long
            public long Reserved;
            public NSPPS2_ANT_INFO RefAntenna;
            public NSPPS2_ANT_INFO RovAntenna;
            public double[] ReferenceXYZ;
            public double[] VectorXYZ;
            public float[] Cov;
            public float[] CovNEU;
            public float RMS;
            //'ratio_fixfloat As Single
            public float Ratio_lambda;
            //'RDOP As Single
            //'numAmb As Long
            //'numTrpResult As Long
            //'trpModel As NSPPS2_TROP
            //'trpEst As NSPPS2_TROP_EST_PARAM
            public long NumKinResult;
            public long reserved2;
        }
        //==========================================================================================
    }
}
