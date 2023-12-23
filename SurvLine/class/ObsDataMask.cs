using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using System.Windows.Forms;
using System.IO;
using System.Xml.Linq;
using NTS;
using System.Web;
using static System.Windows.Forms.AxHost;

namespace SurvLine
{
    public class ObsDataMask
    {

        //***************************************************************************
        //***************************************************************************
        /// <summary>
        ///'読み込み。
        ///     （VB Load関数をVC用に変更）
        ///'
        ///'引き数：
        /// バイナリファイル
        /// バージョン番号
        /// 読み込みデータ
        /// </summary>
        /// <param name="BinaryReader br">
        /// <param name="nVersion br">
        /// <param name="Genba_S">
        /// </param>
        /// <returns>
        /// 戻り値:returns = Non
        /// </returns>
        //***************************************************************************
        public void Load(BinaryReader br, long nVersion, ref GENBA_STRUCT_S Genba_S)
        {

             //23/12/20 K.setoguchi@NV---------->>>>>>>>>>>
            Document document = new Document();
            byte[] buf = new byte[8]; int ret;

            //----------------------------------------------------
            long nCount = br.ReadInt32();

            //----------------------------------------------------
            var m_tMaskInfos_struct = new MaskInfo();
            List<MaskInfo> m_tMaskInfosA = new List<MaskInfo>();
            Genba_S.m_tMaskInfos = m_tMaskInfosA;
            //----------------------------------------------------

            //23/12/22 K.setoguchi@NV---------->>>>>>>>>>
            //-------------------------------------------------------------
            List<List<DateTime>> StrList = new List<List<DateTime>>();
            Genba_S.m_tMaskInfos_StrTimes = StrList;
            List<List<DateTime>> EndList = new List<List<DateTime>>();
            Genba_S.m_tMaskInfos_EndTimes = EndList;
            //-------------------------------------------------------------
            //<<<<<<<<<-----------23/12/22 K.setoguchi@NV


            for (long i = 0; i < nCount; i++)
            {

                //-----------------------------------------
                //Number;               //As Long '衛星番号。
                //                      //  '1～32＝GPSの1～32番
                //                      //  '38～63＝GLONASSの1～26番
                //                      //  '65～72＝QZSSの1～8番
                //                      //  '73～102＝Galileoの1～30番
                //                      //  '105～139＝BeiDouの1～35番
                //                      //  '140～198＝SBASの1～59番
                //                      //  '199≦不明。
                //public bool Enabled;  //As Boolean '有効フラグ。
                //                      //---------------------------
                //publiclong nUBound;   //=-1:無し / =1以上は、(MaskInfo_T)マスク開始時間/マスク終了時間
                //-----------------------------------------


                //-----------------------------------------
                //[VB]  Get #nFile, , m_tMaskInfos(i).Number  long
                //衛星番号
                m_tMaskInfos_struct.Number = br.ReadInt32();


                //-----------------------------------------
                //[VB]  Get #nFile, , m_tMaskInfos(i).Enabled  bool
                //-----------------------------------------
                //'有効フラグ
                m_tMaskInfos_struct.Enabled = document.GetFileBool(br);
                //-----------------------------------------


                //-----------------------------------------
                //[VB]  Dim nUBound As Long
                //[VB]  Get #nFile, , nUBound
                //[VB]  ReDim m_tMaskInfos(i).StrTimes(-1 To nUBound)
                //[VB]  ReDim m_tMaskInfos(i).EndTimes(-1 To nUBound)
                //[VB]  Dim j As Long
                //[VB]  For j = 0 To nUBound
                //[VB]        Get #nFile, , m_tMaskInfos(i).StrTimes(j)
                //[VB]        Get #nFile, , m_tMaskInfos(i).EndTimes(j)
                //[VB]  Next
                //----------------------------------------------------

                ////(@NV)=-1:無し / =1以上は、(MaskInfo_T)マスク開始時間/マスク終了時間
                m_tMaskInfos_struct.nUBound = br.ReadInt32();

                //-----------------------------------------------------
                // 再配置<-読み込みデータ情報
                //-----------------------------------------------------
                Genba_S.m_tMaskInfos.Add(m_tMaskInfos_struct);


                //******************************************
                //((MaskInfo_T)マスク開始時間/マスク終了時間
                //******************************************
                var MaskInfo_T_struct = new MaskInfo_T();

                //23/12/22 K.setoguchi@NV---------->>>>>>>>>>
                //(del)     List<MaskInfo_T> m_tMaskInfosT = new List<MaskInfo_T>();
                //(del)     Genba_S.m_tMaskInfos_T = m_tMaskInfosT;
                //<<<<<<<<<-----------23/12/22 K.setoguchi@NV

                //-------------------------------------------------------------
                List<DateTime> Strdate = new List<DateTime>();
                List<DateTime> Enddate = new List<DateTime>();
                //-------------------------------------------------------------

                if (m_tMaskInfos_struct.nUBound != -1)         //47回目0xFFFFFFFFF
                {

                    for (long j = 0; j <= m_tMaskInfos_struct.nUBound; j++)
                    {
                        // 4byte読み取り
                        ret = br.Read(buf, 0, 8);
                        MaskInfo_T_struct.StrTimes = DateTime.FromOADate(BitConverter.ToDouble(buf, 0));

                        MaskInfo_T_struct.EndTimes = DateTime.FromOADate(BitConverter.ToDouble(buf, 0));


                        //23/12/22 K.setoguchi@NV---------->>>>>>>>>>
                        //-----------------------------------------------------
                        //(del)     //-----------------------------------------------------
                        //(del)     // 再配置<-読み込みデータ情報
                        //(del)     //-----------------------------------------------------
                        //(del)     Genba_S.m_tMaskInfos_T.Add(MaskInfo_T_struct);
                        //-----------------------------------------------------
                        Strdate.Add(MaskInfo_T_struct.StrTimes);                    //衛星マスク開始時間２次元配列    //23/12/22 K.setoguchi@NV
                        Enddate.Add(MaskInfo_T_struct.EndTimes);                    //衛星マスク終了時間２次元配列    //23/12/22 K.setoguchi@NV
                        //-----------------------------------------------------
                        //<<<<<<<<<-----------23/12/22 K.setoguchi@NV

                    }
                    //23/12/22 K.setoguchi@NV---------->>>>>>>>>>
                    //-----------------------------------------------------
                    // 再配置<-読み込みデータ情報
                    //-----------------------------------------------------
                    Genba_S.m_tMaskInfos_StrTimes.Add(Strdate);
                    Genba_S.m_tMaskInfos_EndTimes.Add(Enddate);
                    //-----------------------------------------------------
                    //<<<<<<<<<-----------23/12/22 K.setoguchi@NV

                }
                else
                {
                    MaskInfo_T_struct.StrTimes = DateTime.MinValue;
                    MaskInfo_T_struct.EndTimes = DateTime.MinValue;

                    //23/12/22 K.setoguchi@NV---------->>>>>>>>>>
                    //(del)     //-----------------------------------------------------
                    //(del)     // 再配置<-読み込みデータ情報
                    //(del)     //-----------------------------------------------------
                    //(del)     Genba_S.m_tMaskInfos_T.Add(MaskInfo_T_struct);
                    //-----------------------------------------------------
                    Strdate.Add(MaskInfo_T_struct.StrTimes);
                    Enddate.Add(MaskInfo_T_struct.EndTimes);
                    //-----------------------------------------------------
                    Genba_S.m_tMaskInfos_StrTimes.Add(Strdate);
                    Genba_S.m_tMaskInfos_EndTimes.Add(Enddate);
                    //-----------------------------------------------------
                    //<<<<<<<<<-----------23/12/22 K.setoguchi@NV

                }

            }   // for (long i = 0; i < nCount; i++)

            //<<<<<<<<<-----------23/12/20 K.setoguchi@NV

            //<<<<<<<<<-----------23/12/20 K.setoguchi@NV  ＜＜＜　23/12/20以前の処理は削除　＞＞＞
             //23/12/20 K.setoguchi@NV---------->>>>>>>>>>>  ＜＜＜　23/12/20以前の処理は削除　＞＞＞

            //==================================================================
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
            //***************************************************************************
            //***************************************************************************

        }
    }
}
