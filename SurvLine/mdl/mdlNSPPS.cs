using Microsoft.VisualBasic.ApplicationServices;
using Microsoft.VisualBasic.Logging;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static SurvLine.DepPattern;
using static SurvLine.mdl.MdlNSPPS;

namespace SurvLine.mdl
{
    internal class MdlNSPPS
    {
        //==========================================================================================
        /*[VB]
        '*******************************************************************************
        'NSPPS

        Option Explicit
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '2017/06/08 間違って使用しないようにコメントアウトする。
        'Public Declare Function NSAnalysis Lib "NSPPS" (ByRef pNsPpsIn As NSPPS_IN, ByRef pNsPpsOut As NSPPS_OUT) As Long
        'Public Declare Sub NSGetDepPattern Lib "NSPPS" (ByVal bStrPoint As Long, ByVal nIndex As Long, ByRef pDepPatterns As NSPPS_DEP_PATTERN)
        'Public Declare Sub NSGetAmbInfo Lib "NSPPS" (ByVal nIndex As Long, ByRef pAmbInfo As NSPPS_AMB_INFO)
        'Public Declare Sub NSTerminate Lib "NSPPS" ()
        'Public Declare Function NSGetVersion Lib "NSPPS" () As Single
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        Public Type NSPPS_DATE
            Year As Long '4 digit year
            Month As Long 'month of the year
            Day As Long 'day of the month
            Hour As Long 'hour of the day
            Min As Long 'minutes of the hour
            Reserved As Long '
            Sec As Double 'seconds of the minute
        End Type
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        public struct NSPPS_DATE
        {
            public long Year;       //'4 digit year
            public long Month;      //'month of the year
            public long Day;        //'day of the month
            public long Hour;       //'hour of the day
            public long Min;        //'minutes of the hour
            public long Reserved;   //'
            public double Sec;      //'seconds of the minute
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        Public Type NSPPS_TROP
            pres As Single 'barometric pressure (mbar)
            temp As Single 'temperature (deg C)
            rh As Single 'relative humidity (%)
            model As Long 'troposphere model to use (pps_trop_mdl)
        End Type
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        public struct NSPPS_TROP
        {
            public string pres;     //'barometric pressure (mbar)
            public string temp;     //'temperature (deg C)
            public string rh;       //'relative humidity (%)
            public long model;      //'troposphere model to use (pps_trop_mdl)
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        Public Type NSPPS_TROP_EST_PARAM
            MinWindowLength As Single 'minimum time window for each troposphere parameter [hrs]
            aPrioriConstraint As Single 'constraint on the a priori [m]
            RelativeConstraint As Single 'constraint between troposphere values [m]
            mapping As Long 'mapping function to use
        End Type
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        public struct NSPPS_TROP_EST_PARAM
        {
            public float MinWindowLength;       //'minimum time window for each troposphere parameter [hrs]
            public float aPrioriConstraint;     //'constraint on the a priori [m]
            public float RelativeConstraint;    //'constraint between troposphere values [m]
            public long mapping;                //'mapping function to use
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        Public Type NSPPS_TROP_EST_RESULT
            BegTime As NSPPS_DATE 'start time of the estimated parameters
            EndTime As NSPPS_DATE 'end time of the estimated parameters
            lenHours As Single 'length of time in hours for parameter
            tropAPriori As Single 'a priori value for troposphere
            tropCorrection As Single 'estimated correction to a priori
            tropSigma As Single 'RMS of the troposphere estimate
        End Type
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        public struct NSPPS_TROP_EST_RESULT
        {
            public NSPPS_DATE BegTime;          //'start time of the estimated parameters
            public NSPPS_DATE EndTime;          //'end time of the estimated parameters
            public float lenHours;              //'length of time in hours for parameter
            public float tropAPriori;           //'a priori value for troposphere
            public float tropCorrection;        //'estimated correction to a priori
            public float tropSigma;             //'RMS of the troposphere estimate
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        Public Type NSPPS_OBS_INFO
            All As Long 'number of all observations
            Used As Long 'number of observations used
        End Type
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        public struct NSPPS_OBS_INFO
        {
            public long All;                //'number of all observations
            public long Used;               //'number of observations used
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        Public Type NSPPS_STA_INFO
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
        public struct NSPPS_STA_INFO
        {
            public long posXYZSet;          //'if the posXYZ value should be used for a priori coordinate
            public long Reserved;           //'
            public double[] posXYZ;         //'a priori coordinate of marker XYZ (meters)
            public string antName;          //'name of antenna if not NULL
            public long antHENSet;          //'if antHEN should be used for offset of antenna from marker
            public double[] antHEN;         //'offset of the antenna from the marker Height,East,North (meters)
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        Public Type NSPPS_ANT_INFO
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
        public struct NSPPS_ANT_INFO
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
        Public Type NSPPS_DEP_PATTERN
            PatternEle As Single 'elevations for phase pattern (deg)
            PatternL1 As Single 'phase center pattern for L1 (meters)
            PatternL2 As Single ' phase center pattern for L2 (meters)
            Reserved As Long '
        End Type
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        public struct NSPPS_DEP_PATTERN
        {
            public float PatternEle;        //'elevations for phase pattern (deg)
            public float PatternL1;         //'phase center pattern for L1 (meters)
            public float PatternL2;         //' phase center pattern for L2 (meters)
            public long Reserved;           //'
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        Public Type NSPPS_AMB_INFO
            Lc As Long 'linear combinuation of the ambiguity
            Prn As Long 'PRN ID of the satellite
            PrnRef As Long 'PRN ID of the reference SV for DD ambiguity
            Reserved As Long '
            beg As NSPPS_DATE 'Beginning time of validity for ambiguity
            End As NSPPS_DATE 'Ending time of validity for ambiguity
            ValueFlt As Double 'Float value (real) for ambiguity
            RMS As Single 'formal error of float ambiguity
            Fix As Long 'If the ambiguity was resolved (fixed)
            ValueFix As Long 'Final integer value for the ambiguity if fixed
            reserved2 As Long '
        End Type
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        public struct NSPPS_AMB_INFO
        {
            public long Lc;                 //'linear combinuation of the ambiguity
            public long Prn;                //'PRN ID of the satellite
            public long PrnRef;             //'PRN ID of the reference SV for DD ambiguity
            public long Reserved;           //'
            public NSPPS_DATE beg;          //'Beginning time of validity for ambiguity
            public NSPPS_DATE End;          //'Ending time of validity for ambiguity
            public double ValueFlt;         //'Float value (real) for ambiguity
            public float RMS;               //'formal error of float ambiguity
            public long Fix;                //'If the ambiguity was resolved (fixed)
            public long ValueFix;           //'Final integer value for the ambiguity if fixed
            public long reserved2;          //'
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        Public Type NSPPS_IN
            ObsFileRef As String * MAX_PATH 'full path to RINEX file to use for reference station
            ObsFileRov As String * MAX_PATH 'full path to RINEX file to use for estimated station
            tmpPath As String * MAX_PATH 'directory for temporary files
            BrdFile As String * MAX_PATH 'full path to RINEX Navigation file if not NULL
            PreFile As String * MAX_PATH 'full path to precise orbit file if not NULL
            AntFile As String * MAX_PATH 'full path to antenna phase pattern file
            BegTime As NSPPS_DATE 'time window beginning
            EndTime As NSPPS_DATE 'time window ending
            RefStaInfo As NSPPS_STA_INFO 'Information about the reference station
            RovStaInfo As NSPPS_STA_INFO 'Information about the estimated station
            Sampling As Single 'sampling rate to use (seconds)
            EleMask As Single 'elevation mask to use in processing data
            maxResid_1 As Single 'maximal residuum (first LC)
            maxResid_2 As Single 'maximal residuum (second LC)
            MinRatio As Single 'minimal ratio in LAMBDA
            Lc As Long 'linear combination to use in processing
            trpModel As NSPPS_TROP 'troposphere model parameters
            trpEst As NSPPS_TROP_EST_PARAM 'troposphere estimation parameters
            excludeSV As Long 'SVs to exclude from processing
            StatusFunc As Long 'function pointer to status routine
            pProgress As Long '
            reserved2 As Long '
        End Type
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        public struct NSPPS_IN
        {
            public string ObsFileRef;               //'full path to RINEX file to use for reference station
            public string ObsFileRov;               //'full path to RINEX file to use for estimated station
            public string tmpPath;                  //'directory for temporary files
            public string BrdFile;                  //'full path to RINEX Navigation file if not NULL
            public string PreFile;                  //'full path to precise orbit file if not NULL
            public string AntFile;                  //'full path to antenna phase pattern file
            public NSPPS_DATE BegTime;              //'time window beginning
            public NSPPS_DATE EndTime;              //'time window ending
            public NSPPS_STA_INFO RefStaInfo;       //'Information about the reference station
            public NSPPS_STA_INFO RovStaInfo;       //'Information about the estimated station
            public float Sampling;                  //'sampling rate to use (seconds)
            public float EleMask;                   //'elevation mask to use in processing data
            public float maxResid_1;                //'maximal residuum (first LC)
            public float maxResid_2;                //'maximal residuum (second LC)
            public float MinRatio;                  //'minimal ratio in LAMBDA
            public long Lc;                         //'linear combination to use in processing
            public NSPPS_TROP trpModel;             //'troposphere model parameters
            public NSPPS_TROP_EST_PARAM trpEst;     //'troposphere estimation parameters
            public long excludeSV;                  //'SVs to exclude from processing
            public long StatusFunc;                 //'function pointer to status routine
            public long pProgress;                  //'
            public long reserved2;                  //'
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        Public Type NSPPS_OUT
            BegTime As NSPPS_DATE
            EndTime As NSPPS_DATE
            NumEpochUsed As Long
            NumEpochRejected As Long
            ObsInfo(PPS_MAXPRN) As NSPPS_OBS_INFO
            Sampling As Single
            EleMask As Single
            maxResid_1 As Single
            maxResid_2 As Single
            MinRatio As Single
            Lc As Long
            SolType As Long
            AmbPercentage As Long
            RefAntenna As NSPPS_ANT_INFO
            RovAntenna As NSPPS_ANT_INFO
            ReferenceXYZ(2) As Double
            VectorXYZ(2) As Double
            Cov(5) As Single
            CovNEU(5) As Single
            RMS As Single
            ratio_fixfloat As Single
            Ratio_lambda As Single
            RDOP As Single
            numAmb As Long
            numTrpResult As Long
            trpModel As NSPPS_TROP
            trpEst As NSPPS_TROP_EST_PARAM
        End Type
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        public struct NSPPS_OUT
        {
            public NSPPS_DATE BegTime;
            public NSPPS_DATE EndTime;
            public long NumEpochUsed;
            public long NumEpochRejected;
            public NSPPS_OBS_INFO[] ObsInfo;
            public float Sampling;
            public float EleMask;
            public float maxResid_1;
            public float maxResid_2;
            public float MinRatio;
            public long Lc;
            public long SolType;
            public long AmbPercentage;
            public NSPPS_ANT_INFO RefAntenna;
            public NSPPS_ANT_INFO RovAntenna;
            public double[] ReferenceXYZ;
            public double[] VectorXYZ;
            public float[] Cov;
            public float[] CovNEU;
            public float RMS;
            public float ratio_fixfloat;
            public float Ratio_lambda;
            public float RDOP;
            public long numAmb;
            public long numTrpResult;
            public NSPPS_TROP trpModel;
            public NSPPS_TROP_EST_PARAM trpEst;
        }
        //==========================================================================================
    }
}
