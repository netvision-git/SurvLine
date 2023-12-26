using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace SurvLine
{
    public class ObsInfo
    {


        //23/12/26 K.setoguchi@NV---------->>>>>>>>>>
        //***************************************************************************
        //***************************************************************************
        /// <summary>
        ///'観測情報    '読み込み。
        ////'
        ///'引き数：
        /// バイナリファイル
        ///'nFile ファイル番号。
        //'nVersion ファイルバージョン。
        /// 読み込みデータ
        /// </summary>
        /// <param name="br"></param>
        /// <param name="nVersion"></param>
        /// <param name="Genba_S"></param>
        /// <returns>
        /// 戻り値:returns = Non
        /// </returns>
        /// -------------------------------------------------------------------------
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


                //23/12/26 K.setoguchi@NV---------->>>>>>>>>>
                //(del)     long[] m_tObsInfos = new long[nUBound + 1];
                OBS_INFO m_tObsInfos_struct = new OBS_INFO();
                List<OBS_INFO> m_tObsInfosA = new List<OBS_INFO>();
                Genba_S.m_tObsInfos = m_tObsInfosA;
                //<<<<<<<<<-----------23/12/26 K.setoguchi@NV


                //----------------------------------------------------
                for (long i = 0; i <= nUBound; i++)
                {
                    //        Get #nFile, , m_tObsInfos(i).Number
                    //(del)  m_tObsInfos[i] = br.ReadInt32();           //23/12/26 K.setoguchi@NV
                    m_tObsInfos_struct.Number = br.ReadInt32();         //23/12/26 K.setoguchi@NV
                    //-----------------------------------------------------
                    //        Get #nFile, , m_tObsInfos(i).Info.All
                    //(del) m_tObsInfos[i] = br.ReadInt32();            //23/12/26 K.setoguchi@NV
                    m_tObsInfos_struct.INFO_All = br.ReadInt32();       //23/12/26 K.setoguchi@NV
                    //-----------------------------------------------------
                    //        Get #nFile, , m_tObsInfos(i).Info.Used
                    //(del) m_tObsInfos[i] = br.ReadInt32();            //23/12/26 K.setoguchi@NV
                    m_tObsInfos_struct.INFO_Used = br.ReadInt32();      //23/12/26 K.setoguchi@NV
                                                                        //-----------------------------------------------------


                    //23/12/26 K.setoguchi@NV---------->>>>>>>>>>

                    if (nVersion < 9300){
                        if (m_tObsInfos_struct.Number < 105)
                        {
                        }
                        else if (m_tObsInfos_struct.Number < 140)
                        {
                            m_tObsInfos_struct.Number += 4;
                        }else if (m_tObsInfos_struct.Number < 199)
                        {
                            m_tObsInfos_struct.Number += 6;
                        }
                        else
                        {
                            m_tObsInfos_struct.Number += 6;   //'199 → 205
                        }
                    }

                    //-----------------------------------------------------
                    // 配列と再配置
                    //-----------------------------------------------------
                    Genba_S.m_tObsInfos.Add(m_tObsInfos_struct);

                    //<<<<<<<<<-----------23/12/26 K.setoguchi@NV
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

        //<<<<<<<<<-----------23/12/26 K.setoguchi@NV




    }
}
