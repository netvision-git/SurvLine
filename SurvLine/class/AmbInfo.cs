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

        //23/12/22 K.setoguchi@NV---------->>>>>>>>>>
        //***************************************************************************
        //public List<MaskInfo> m_tAmbInfos;  //アンビギュイティ情報  //AmbInfo_Load:m_tAmbInfos
        //<<<<<<<<<-----------23/12/22 K.setoguchi@NV
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
            //23/12/22 K.setoguchi@NV---------->>>>>>>>>>
            //----------------------------------------------------
            //(del)     long[] m_tObsInfos = new long[nUBound + 1];
            var m_tAmbInfos_struct = new AMBINF_STRUCT();
            List<AMBINF_STRUCT> m_tAmbInfosA = new List<AMBINF_STRUCT>();
            Genba_S.m_tAmbInfos = m_tAmbInfosA;
            //----------------------------------------------------
            //<<<<<<<<<-----------23/12/22 K.setoguchi@NV


            //----------------------------------------------------
            for (long i = 0; i <= nUBound; i++)
            {
                //23/12/22 K.setoguchi@NV---------->>>>>>>>>>
                //-------------------------------------------
                // 念の為、配列の先頭にデータ数を設定
                m_tAmbInfos_struct.Count = nUBound;                 //23/12/22 K.setoguchi@NV
                //-------------------------------------------
                //<<<<<<<<<-----------23/12/22 K.setoguchi@NV

                //-------------------------------------------
                //        Get #nFile, , m_tAmbInfos(i).Lc
                //(del)     m_tObsInfos[i] = br.ReadInt32();        //23/12/22 K.setoguchi@NV
                m_tAmbInfos_struct.Lc = br.ReadInt32();             //23/12/22 K.setoguchi@NV
                //-------------------------------------------
                //        Get #nFile, , m_tAmbInfos(i).Prn
                //(del)     m_tObsInfos[i] = br.ReadInt32();        //23/12/22 K.setoguchi@NV
                m_tAmbInfos_struct.Prn = br.ReadInt32();            //23/12/22 K.setoguchi@NV
                //-------------------------------------------
                //        Get #nFile, , m_tAmbInfos(i).PrnRef
                //(del)     m_tObsInfos[i] = br.ReadInt32();        //23/12/22 K.setoguchi@NV
                m_tAmbInfos_struct.PrnRef = br.ReadInt32();         //23/12/22 K.setoguchi@NV
                //-------------------------------------------
                //        Get #nFile, , m_tAmbInfos(i).beg.Year
                //(del)     m_tObsInfos[i] = br.ReadInt32();        //23/12/22 K.setoguchi@NV
                m_tAmbInfos_struct.beg.Year = br.ReadInt32();       //23/12/22 K.setoguchi@NV
                //-------------------------------------------
                //        Get #nFile, , m_tAmbInfos(i).beg.Month
                //(del)     m_tObsInfos[i] = br.ReadInt32();
                m_tAmbInfos_struct.beg.Month = br.ReadInt32();       //23/12/22 K.setoguchi@NV
                //-------------------------------------------
                //        Get #nFile, , m_tAmbInfos(i).beg.Day
                //(del)     m_tObsInfos[i] = br.ReadInt32();
                m_tAmbInfos_struct.beg.Day = br.ReadInt32();        //23/12/22 K.setoguchi@NV
                //-------------------------------------------
                //        Get #nFile, , m_tAmbInfos(i).beg.Hour
                //(del)     m_tObsInfos[i] = br.ReadInt32();
                m_tAmbInfos_struct.beg.Hour = br.ReadInt32();       //23/12/22 K.setoguchi@NV
                //-------------------------------------------
                //        Get #nFile, , m_tAmbInfos(i).beg.Min
                //(del)     m_tObsInfos[i] = br.ReadInt32();
                m_tAmbInfos_struct.beg.Min = br.ReadInt32();        //23/12/22 K.setoguchi@NV
                //-------------------------------------------
                //        Get #nFile, , m_tAmbInfos(i).beg.Sec  Double
                //(del)     m_tObsInfos[i] = (long)br.ReadDouble();
                m_tAmbInfos_struct.beg.Sec = br.ReadDouble();        //23/12/22 K.setoguchi@NV
                //-------------------------------------------
                //        Get #nFile, , m_tAmbInfos(i).End.Year
                //(del)     m_tObsInfos[i] = br.ReadInt32();        //23/12/22 K.setoguchi@NV
                m_tAmbInfos_struct.End.Year = br.ReadInt32();       //23/12/22 K.setoguchi@NV
                //-------------------------------------------
                //        Get #nFile, , m_tAmbInfos(i).End.Month    
                //(del)     m_tObsInfos[i] = br.ReadInt32();        //23/12/22 K.setoguchi@NV
                m_tAmbInfos_struct.End.Month = br.ReadInt32();      //23/12/22 K.setoguchi@NV
                //-------------------------------------------
                //        Get #nFile, , m_tAmbInfos(i).End.Day
                //(del)     m_tObsInfos[i] = br.ReadInt32();        //23/12/22 K.setoguchi@NV
                m_tAmbInfos_struct.End.Day = br.ReadInt32();        //23/12/22 K.setoguchi@NV
                //-------------------------------------------
                //        Get #nFile, , m_tAmbInfos(i).End.Hour
                //(del)     m_tObsInfos[i] = br.ReadInt32();        //23/12/22 K.setoguchi@NV
                m_tAmbInfos_struct.End.Hour = br.ReadInt32();       //23/12/22 K.setoguchi@NV
                //-------------------------------------------
                //        Get #nFile, , m_tAmbInfos(i).End.Min
                //(del)     m_tObsInfos[i] = br.ReadInt32();        //23/12/22 K.setoguchi@NV
                m_tAmbInfos_struct.End.Min = br.ReadInt32();        //23/12/22 K.setoguchi@NV
                //-------------------------------------------
                //        Get #nFile, , m_tAmbInfos(i).End.Sec  Double
                //(del)     m_tObsInfos[i] = (long)br.ReadDouble(); //23/12/22 K.setoguchi@NV
                m_tAmbInfos_struct.End.Sec = br.ReadDouble();       //23/12/22 K.setoguchi@NV
                //-------------------------------------------
                //        Get #nFile, , m_tAmbInfos(i).RMS      Double
                //(del)     m_tObsInfos[i] = (long)br.ReadSingle();
                m_tAmbInfos_struct.RMS = br.ReadSingle();           //23/12/22 K.setoguchi@NV
                //-------------------------------------------
                //        Get #nFile, , m_tAmbInfos(i).Fix
                //(del)     m_tObsInfos[i] = br.ReadInt32();        //23/12/22 K.setoguchi@NV
                m_tAmbInfos_struct.Fix = br.ReadInt32();            //23/12/22 K.setoguchi@NV
                //-------------------------------------------
                //        Get #nFile, , m_tAmbInfos(i).ValueFix
                //(del)     m_tObsInfos[i] = br.ReadInt32();        //23/12/22 K.setoguchi@NV
                m_tAmbInfos_struct.ValueFix = br.ReadInt32();       //23/12/22 K.setoguchi@NV
                //-------------------------------------------
                //        Get #nFile, , m_tAmbInfos(i).ValueFlt Double
                //(del)     m_tObsInfos[i] = (long)br.ReadDouble(); //23/12/22 K.setoguchi@NV
                m_tAmbInfos_struct.ValueFlt = br.ReadDouble();      //23/12/22 K.setoguchi@NV
                //-------------------------------------------


                //23/12/22 K.setoguchi@NV---------->>>>>>>>>>
                //<<<<<<<<<-----------23/12/22 K.setoguchi@NV
                //-----------------------------------------------------
                // 配列と再配置
                //-----------------------------------------------------
                Genba_S.m_tAmbInfos.Add(m_tAmbInfos_struct);
                //<<<<<<<<<-----------23/12/22 K.setoguchi@NV


            }
        }
        //-------------------------------------------------------------------------
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

        //} //23/12/22 K.Setoguchi

    }

}
