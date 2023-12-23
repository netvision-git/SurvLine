﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using System.Windows.Forms;
using System.IO;

namespace SurvLine
{
    internal class DepPattern
    {
        //23/12/22 K.setoguchi@NV---------->>>>>>>>>
        //   extrnalに移動
        //(del)     public struct NSPPS3_DEP_PATTERN
        //(del)     {
        //(del)     public Single PatternEle;
        //(del)     public Single PatternL1;
        //(del)     public Single PatternL2;
        //(del)     }
        //<<<<<<<<<-----------23/12/22 K.setoguchi@NV
        //----------------------------------------------------------------------------------

        public void Load(BinaryReader br, long nVersion, ref GENBA_STRUCT_S Genba_S)
        {

            MdlUtility mdlUtility = new MdlUtility();

            //[VB]    Dim nUBound As Long
            //[VB]    Get #nFile, , nUBound
            //[VB]    ReDim m_tDepPattern(-1 To nUBound)
            long nUBound;
            nUBound = br.ReadInt32();


            var m_tDepPattern_struct = new NSPPS3_DEP_PATTERN();

            //23/12/22 K.setoguchi@NV---------->>>>>>>>>
            //(del) List<NSPPS3_DEP_PATTERN> m_tDepPattern = new List<NSPPS3_DEP_PATTERN>();
            List<NSPPS3_DEP_PATTERN> m_tDepPatternA = new List<NSPPS3_DEP_PATTERN>();
            Genba_S.m_tDepPattern = m_tDepPatternA;
            //<<<<<<<<<-----------23/12/22 K.setoguchi@NV

            for (long i = 0; i <= nUBound; i++)
            {
                //23/12/22 K.setoguchi@NV---------->>>>>>>>>
                //   下位に移動
                //(del) //-----------------------------------------------------
                //(del) // 配列と再配置
                //(del) //-----------------------------------------------------
                //(del) m_tDepPattern.Add(m_tDepPattern_struct);
                //<<<<<<<<<-----------23/12/22 K.setoguchi@NV


                //-----------------------------------------------------
                // ファイル読み込み
                //-----------------------------------------------------
                //[VB]  Get #nFile, , m_tDepPattern(i).PatternEle
                m_tDepPattern_struct.PatternEle = br.ReadSingle();

                //-----------------------------------------------------
                //[VB]  Get #nFile, , m_tDepPattern(i).PatternL1
                m_tDepPattern_struct.PatternL1 = br.ReadSingle();
                //-----------------------------------------------------
                //[VB]  Get #nFile, , m_tDepPattern(i).PatternL2
                m_tDepPattern_struct.PatternL2 = br.ReadSingle();

                //        If nVersion< 9300 Then
                //            m_tDepPattern(i).PatternL5 = 0
                //        Else
                //            Get #nFile, , m_tDepPattern(i).PatternL5
                //        End If
                //23/12/22 K.setoguchi@NV---------->>>>>>>>>>
                if (nVersion < 9300)
                {
                    m_tDepPattern_struct.PatternL5 = 0;
                }
                else
                {
                    m_tDepPattern_struct.PatternL5 = br.ReadSingle();
                }
                //-----------------------------------------------------
                // 配列と再配置
                //-----------------------------------------------------
                Genba_S.m_tDepPattern.Add(m_tDepPattern_struct);
                //<<<<<<<<<-----------23/12/22 K.setoguchi@NV
            }
            //23/12/22 K.setoguchi@NV---------->>>>>>>>>>
            //サンプルプログラム 全削除
            //<<<<<<<<<-----------23/12/22 K.setoguchi@NV
        }
        //---------------------------------------------------------------
        //'読み込み。
        //'
        //'引き数：
        //'nFile ファイル番号。
        //'nVersion ファイルバージョン。
        //Public Sub Load(ByVal nFile As Integer, ByVal nVersion As Long)
        //    Dim nUBound As Long
        //    Get #nFile, , nUBound
        //    ReDim m_tDepPattern(-1 To nUBound)
        //    Dim i As Long
        //    For i = 0 To nUBound
        //        Get #nFile, , m_tDepPattern(i).PatternEle
        //        Get #nFile, , m_tDepPattern(i).PatternL1
        //        Get #nFile, , m_tDepPattern(i).PatternL2
        //        If nVersion< 9300 Then
        //            m_tDepPattern(i).PatternL5 = 0
        //        Else
        //            Get #nFile, , m_tDepPattern(i).PatternL5
        //        End If
        //    Next
        //End Sub
        //Public Type NSPPS3_DEP_PATTERN
        //  PatternEle As Single 'elevations for phase pattern (deg)
        //  PatternL1 As Single 'phase center pattern for L1 (meters)
        //  PatternL2 As Single 'phase center pattern for L2 (meters)
        //  PatternL5 As Single 'phase center pattern for L5 (meters)
        //End Type



    }
}
