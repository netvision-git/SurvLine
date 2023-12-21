using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using System.Windows.Forms;

namespace SurvLine
{
    public class ObsInfo
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


            if (nUBound != -1)         //47回目0xFFFFFFFFF
            {                           //----------------------------------------------------
                long[] m_tObsInfos = new long[nUBound + 1];
                //----------------------------------------------------
                for (long i = 0; i <= nUBound; i++)
                {
                    //        Get #nFile, , m_tObsInfos(i).Number
                    m_tObsInfos[i] = br.ReadInt32();
                    //-----------------------------------------------------
                    //        Get #nFile, , m_tObsInfos(i).Info.All
                    m_tObsInfos[i] = br.ReadInt32();
                    //-----------------------------------------------------
                    //        Get #nFile, , m_tObsInfos(i).Info.Used
                    m_tObsInfos[i] = br.ReadInt32();
                    //-----------------------------------------------------


                    //        If nVersion< 9300 Then
                    //            If m_tObsInfos(i).Number< 105 Then
                    //            ElseIf m_tObsInfos(i).Number< 140 Then
                    //                m_tObsInfos(i).Number = m_tObsInfos(i).Number + 4
                    //            ElseIf m_tObsInfos(i).Number< 199 Then
                    //                m_tObsInfos(i).Number = m_tObsInfos(i).Number + 6
                    //            Else
                    //                m_tObsInfos(i).Number = m_tObsInfos(i).Number + 6   '199 → 205
                    //            End If
                    //        End If
                }
            }

        }
        //[VB]-----------------------------------------------------------------------------
        //'読み込み。
        //'
        //'引き数：
        //'nFile ファイル番号。
        //'nVersion ファイルバージョン。
        //Public Sub Load(ByVal nFile As Integer, ByVal nVersion As Long)
        //    Dim nUBound As Long
        //    Get #nFile, , nUBound
        //    ReDim m_tObsInfos(-1 To nUBound)
        //    Dim i As Long
        //    For i = 0 To nUBound
        //        Get #nFile, , m_tObsInfos(i).Number
        //        Get #nFile, , m_tObsInfos(i).Info.All
        //        Get #nFile, , m_tObsInfos(i).Info.Used
        //        
        //        If nVersion< 9300 Then
        //            If m_tObsInfos(i).Number< 105 Then
        //            ElseIf m_tObsInfos(i).Number< 140 Then
        //                m_tObsInfos(i).Number = m_tObsInfos(i).Number + 4
        //            ElseIf m_tObsInfos(i).Number< 199 Then
        //                m_tObsInfos(i).Number = m_tObsInfos(i).Number + 6
        //            Else
        //                m_tObsInfos(i).Number = m_tObsInfos(i).Number + 6   '199 → 205
        //            End If
        //        End If
        //    Next
        //End Sub
        //***************************************************************************
        //***************************************************************************





    }
}
