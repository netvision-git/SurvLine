using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SurvLine
{
    public class AmbInfo
    {

        //***************************************************************************
        //***************************************************************************
        public void Load(BinaryReader br, long nVersion, ref GENBA_STRUCT_S Genba_S)
        {

            //    Dim nUBound As Long
            //    Get #nFile, , nUBound
            //    ReDim m_tObsInfos(-1 To nUBound)
            //    Dim i As Long
            long nUBound;
            nUBound = br.ReadInt32();

            //----------------------------------------------------
            long[] m_tObsInfos = new long[nUBound + 1];
            for (long i = 0; i <= nUBound; i++)
            {
                //        Get #nFile, , m_tAmbInfos(i).Lc
                m_tObsInfos[i] = br.ReadInt32();
                //-------------------------------------------
                //        Get #nFile, , m_tAmbInfos(i).Prn
                m_tObsInfos[i] = br.ReadInt32();
                //-------------------------------------------
                //        Get #nFile, , m_tAmbInfos(i).PrnRef
                m_tObsInfos[i] = br.ReadInt32();
                //-------------------------------------------
                //        Get #nFile, , m_tAmbInfos(i).beg.Year
                m_tObsInfos[i] = br.ReadInt32();
                //-------------------------------------------
                //        Get #nFile, , m_tAmbInfos(i).beg.Month
                m_tObsInfos[i] = br.ReadInt32();
                //-------------------------------------------
                //        Get #nFile, , m_tAmbInfos(i).beg.Day
                m_tObsInfos[i] = br.ReadInt32();
                //-------------------------------------------
                //        Get #nFile, , m_tAmbInfos(i).beg.Hour
                m_tObsInfos[i] = br.ReadInt32();
                //-------------------------------------------
                //        Get #nFile, , m_tAmbInfos(i).beg.Min
                m_tObsInfos[i] = br.ReadInt32();
                //-------------------------------------------
                //        Get #nFile, , m_tAmbInfos(i).beg.Sec      Double
                m_tObsInfos[i] = (long)br.ReadDouble();
                //-------------------------------------------
                //        Get #nFile, , m_tAmbInfos(i).End.Year
                m_tObsInfos[i] = br.ReadInt32();
                //-------------------------------------------
                //        Get #nFile, , m_tAmbInfos(i).End.Month
                m_tObsInfos[i] = br.ReadInt32();
                //-------------------------------------------
                //        Get #nFile, , m_tAmbInfos(i).End.Day
                m_tObsInfos[i] = br.ReadInt32();
                //-------------------------------------------
                //        Get #nFile, , m_tAmbInfos(i).End.Hour
                m_tObsInfos[i] = br.ReadInt32();
                //-------------------------------------------
                //        Get #nFile, , m_tAmbInfos(i).End.Min
                m_tObsInfos[i] = br.ReadInt32();
                //-------------------------------------------
                //        Get #nFile, , m_tAmbInfos(i).End.Sec      Double
                m_tObsInfos[i] = (long)br.ReadDouble();
                //-------------------------------------------
                //        Get #nFile, , m_tAmbInfos(i).RMS      Single
                m_tObsInfos[i] = (long)br.ReadSingle();
                //-------------------------------------------
                //        Get #nFile, , m_tAmbInfos(i).Fix
                m_tObsInfos[i] = br.ReadInt32();
                //-------------------------------------------
                //        Get #nFile, , m_tAmbInfos(i).ValueFix
                m_tObsInfos[i] = br.ReadInt32();
                //-------------------------------------------
                //        Get #nFile, , m_tAmbInfos(i).ValueFlt Double
                m_tObsInfos[i] = (long)br.ReadDouble();
                //-------------------------------------------

            }

            //'読み込み。
            //'
            //'引き数：
            //'nFile ファイル番号。
            //'nVersion ファイルバージョン。
            //Public Sub Load(ByVal nFile As Integer, ByVal nVersion As Long)
            //    Dim nUBound As Long
            //    Get #nFile, , nUBound
            //    ReDim m_tAmbInfos(-1 To nUBound)
            //    Dim i As Long
            //    For i = 0 To nUBound
            //        Get #nFile, , m_tAmbInfos(i).Lc
            //        Get #nFile, , m_tAmbInfos(i).Prn
            //        Get #nFile, , m_tAmbInfos(i).PrnRef
            //        Get #nFile, , m_tAmbInfos(i).beg.Year
            //        Get #nFile, , m_tAmbInfos(i).beg.Month
            //        Get #nFile, , m_tAmbInfos(i).beg.Day
            //        Get #nFile, , m_tAmbInfos(i).beg.Hour
            //        Get #nFile, , m_tAmbInfos(i).beg.Min
            //        Get #nFile, , m_tAmbInfos(i).beg.Sec
            //        Get #nFile, , m_tAmbInfos(i).End.Year
            //        Get #nFile, , m_tAmbInfos(i).End.Month
            //        Get #nFile, , m_tAmbInfos(i).End.Day
            //        Get #nFile, , m_tAmbInfos(i).End.Hour
            //        Get #nFile, , m_tAmbInfos(i).End.Min
            //        Get #nFile, , m_tAmbInfos(i).End.Sec
            //        Get #nFile, , m_tAmbInfos(i).RMS
            //        Get #nFile, , m_tAmbInfos(i).Fix
            //        Get #nFile, , m_tAmbInfos(i).ValueFix
            //        Get #nFile, , m_tAmbInfos(i).ValueFlt
            //    Next
            //End Sub


        }

    }
}
