using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using System.Windows.Forms;
using System.IO;
using System.Xml.Linq;

namespace SurvLine
{
    public class ObsDataMask
    {

        public void Load(BinaryReader br, long nVersion, ref GENBA_STRUCT_S Genba_S)
        {
            byte[] buf = new byte[8]; int ret;
            Document document = new Document();

            //    Dim nCount As Long
            //    Get #nFile, , nCount
            //    ReDim m_tMaskInfos(-1 To nCount - 1)
            //    Dim i As Long
            long nCount = br.ReadInt32();

            //----------------------------------------------------
            long[] m_tMaskInfos = new long[nCount];

            for (long i = 0; i < nCount; i++)
            {
                //-----------------------------------------
                //        Get #nFile, , m_tMaskInfos(i).Number  long
                m_tMaskInfos[i] = br.ReadInt32();
                //        Get #nFile, , m_tMaskInfos(i).Enabled  bool


                bool dmy;
                dmy = document.GetFileBool(br);
                //m_tMaskInfos[i] = (long)document.GetFileBool(br);



                //-----------------------------------------

                //        Dim nUBound As Long
                //        Get #nFile, , nUBound
                //        ReDim m_tMaskInfos(i).StrTimes(-1 To nUBound)
                //        ReDim m_tMaskInfos(i).EndTimes(-1 To nUBound)
                //        Dim j As Long
                //        For j = 0 To nUBound
                //            Get #nFile, , m_tMaskInfos(i).StrTimes(j)
                //            Get #nFile, , m_tMaskInfos(i).EndTimes(j)
                //        Next
                long nUBound = br.ReadInt32();
                if (nUBound != -1)         //47回目0xFFFFFFFFF
                {
                    DateTime[] m_tMaskInfos_date_StrTimes = new DateTime[nUBound + 1];
                    DateTime[] m_tMaskInfos_date_EndTimes = new DateTime[nUBound + 1];

                    for (long j = 0; j <= nUBound; j++)
                    {
                        // 4byte読み取り
                        ret = br.Read(buf, 0, 8);
                        m_tMaskInfos_date_StrTimes[j] = DateTime.FromOADate(BitConverter.ToDouble(buf, 0));

                        m_tMaskInfos_date_EndTimes[j] = DateTime.FromOADate(BitConverter.ToDouble(buf, 0));
                    }

                }

            }

            //'読み込み。
            //'
            //'引き数：
            //'nFile ファイル番号。
            //'nVersion ファイルバージョン。
            //Public Sub Load(ByVal nFile As Integer, ByVal nVersion As Long)
            //    Dim nCount As Long
            //    Get #nFile, , nCount
            //    ReDim m_tMaskInfos(-1 To nCount - 1)
            //    Dim i As Long
            //    For i = 0 To UBound(m_tMaskInfos)
            //        Get #nFile, , m_tMaskInfos(i).Number
            //        Get #nFile, , m_tMaskInfos(i).Enabled
            //        Dim nUBound As Long
            //        Get #nFile, , nUBound
            //        ReDim m_tMaskInfos(i).StrTimes(-1 To nUBound)
            //        ReDim m_tMaskInfos(i).EndTimes(-1 To nUBound)
            //        Dim j As Long
            //        For j = 0 To nUBound
            //            Get #nFile, , m_tMaskInfos(i).StrTimes(j)
            //            Get #nFile, , m_tMaskInfos(i).EndTimes(j)
            //        Next
            //    Next
            //End Sub

        }
    }
}
