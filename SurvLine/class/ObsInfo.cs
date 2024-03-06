using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using System.Windows.Forms;
using static SurvLine.mdl.MdlNSPPS2;
using System.Reflection;
using Microsoft.VisualBasic.ApplicationServices;
using SurvLine.mdl;
using Microsoft.VisualBasic.Logging;

namespace SurvLine
{
    public class ObsInfo
    {
        //==========================================================================================
        /*[VB]
        '*******************************************************************************
        '観測情報

        Option Explicit
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        Private Type OBS_INFO '観測情報。
            Number As Long '衛星番号。
            Info As NSPPS2_OBS_INFO '情報。
        End Type
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        private struct OBS_INFO             //'観測情報。
        {
            public long Number;             //'衛星番号。
            public NSPPS2_OBS_INFO Info;    //'情報。
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'インプリメンテーション。
        Private m_tObsInfos() As OBS_INFO '観測情報。配列の要素は(-1 To ...)、要素 -1 は未使用。
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'インプリメンテーション。
        private OBS_INFO[] m_tObsInfos;     //'観測情報。配列の要素は(-1 To ...)、要素 -1 は未使用。
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '*******************************************************************************
        'プロパティ

        '観測情報数。
        Property Let Count(ByVal nCount As Long)
            ReDim Preserve m_tObsInfos(-1 To nCount - 1)
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '*******************************************************************************
        'プロパティ

        '観測情報数。
        */
        public void Count(long nCount)
        {
            m_tObsInfos = new OBS_INFO[nCount];
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '観測情報数。
        Property Get Count() As Long
            Count = UBound(m_tObsInfos) + 1
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'観測情報数。
        public long Count()
        {
            return m_tObsInfos.Length;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '衛星番号。
        Property Let Number(ByVal nIndex As Long, ByVal nNumber As Long)
            m_tObsInfos(nIndex).Number = nNumber
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'衛星番号。
        public void Number(long nIndex, long nNumber)
        {
            m_tObsInfos[nIndex].Number = nNumber;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '衛星番号。
        Property Get Number(ByVal nIndex As Long) As Long
            Number = m_tObsInfos(nIndex).Number
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'衛星番号。
        public long Number(long nIndex)
        {
            return m_tObsInfos[nIndex].Number;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '総数。
        Property Let All(ByVal nIndex As Long, ByVal nAll As Long)
            m_tObsInfos(nIndex).Info.All = nAll
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'総数。
        public void All(long nIndex, long nAll)
        {
            m_tObsInfos[nIndex].Info.All = nAll;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '総数。
        Property Get All(ByVal nIndex As Long) As Long
            All = m_tObsInfos(nIndex).Info.All
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'総数。
        public long All(long nIndex)
        {
            return m_tObsInfos[nIndex].Info.All;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '使用数。
        Property Let Used(ByVal nIndex As Long, ByVal nUsed As Long)
            m_tObsInfos(nIndex).Info.Used = nUsed
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'使用数。
        public void Used(long nIndex, long nUsed)
        {
            m_tObsInfos[nIndex].Info.Used = nUsed;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '使用数。
        Property Get Used(ByVal nIndex As Long) As Long
            Used = m_tObsInfos(nIndex).Info.Used
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'使用数。
        public long Used(long nIndex)
        {
            return m_tObsInfos[nIndex].Info.Used;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '棄却数。
        Property Get Rejected(ByVal nIndex As Long) As Long
            Rejected = m_tObsInfos(nIndex).Info.All - m_tObsInfos(nIndex).Info.Used
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'棄却数。
        public long Rejected(long nIndex)
        {
            return m_tObsInfos[nIndex].Info.All - m_tObsInfos[nIndex].Info.Used;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '*******************************************************************************
        'イベント

        '初期化。
        Private Sub Class_Initialize()

            On Error GoTo ErrorHandler
    
            ReDim m_tObsInfos(-1 To -1)
    
            Exit Sub
    
        ErrorHandler:
            Call mdlMain.ErrorExit
    
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '*******************************************************************************
        'イベント

        '初期化。
        */
        private void Class_Initialize()
        {

            try
            {

                m_tObsInfos = new OBS_INFO[0];
                return;

            }


            catch (Exception)
            {
                //mdlMain.ErrorExit;
                return;
            }


        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '*******************************************************************************
        'メソッド

        '保存。
        '
        '引き数：
        'nFile ファイル番号。
        Public Sub Save(ByVal nFile As Integer)
            Put #nFile, , UBound(m_tObsInfos)
            Dim i As Long
            For i = 0 To UBound(m_tObsInfos)
                Put #nFile, , m_tObsInfos(i).Number
                Put #nFile, , m_tObsInfos(i).Info.All
                Put #nFile, , m_tObsInfos(i).Info.Used
            Next
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '*******************************************************************************
        'メソッド

        '保存。
        '
        '引き数：
        'nFile ファイル番号。
        */
        public void Save(int nFile)
        {
#if false
            Put #nFile, , UBound(m_tObsInfos)
            Dim i As Long
            For i = 0 To UBound(m_tObsInfos)
                Put #nFile, , m_tObsInfos(i).Number
                Put #nFile, , m_tObsInfos(i).Info.All
                Put #nFile, , m_tObsInfos(i).Info.Used
            Next
#endif
        }
        //==========================================================================================

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
                m_tObsInfos = new OBS_INFO[nUBound + 1];
                //----------------------------------------------------
                for (long i = 0; i <= nUBound; i++)
                {
                    //        Get #nFile, , m_tObsInfos(i).Number
                    m_tObsInfos[i].Number = br.ReadInt32();
                    //-----------------------------------------------------
                    //        Get #nFile, , m_tObsInfos(i).Info.All
                    m_tObsInfos[i].Info.All = br.ReadInt32();
                    //-----------------------------------------------------
                    //        Get #nFile, , m_tObsInfos(i).Info.Used
                    m_tObsInfos[i].Info.Used = br.ReadInt32();
                    //-----------------------------------------------------


                    if (nVersion < 9300)
                    {
                        if (m_tObsInfos[i].Number < 105)
                        {
                        }
                        else if (m_tObsInfos[i].Number < 140)
                        {
                            m_tObsInfos[i].Number = m_tObsInfos[i].Number + 4;
                        }
                        else if (m_tObsInfos[i].Number < 199)
                        {
                            m_tObsInfos[i].Number = m_tObsInfos[i].Number + 6;
                        }
                        else
                        {
                            m_tObsInfos[i].Number = m_tObsInfos[i].Number + 6;   //'199 → 205
                        }
                    }
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
