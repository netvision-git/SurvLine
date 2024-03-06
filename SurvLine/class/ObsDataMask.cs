using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using System.Windows.Forms;
using System.IO;
using System.Xml.Linq;
using static SurvLine.MaskInfo;
using SurvLine.mdl;

namespace SurvLine
{
    public class ObsDataMask
    {
        public ObsDataMask(MdlMain mdlMain)
        {
            this.mdlMain = mdlMain;
            document = mdlMain.GetDocument();
        }

        //==========================================================================================
        /*[VB]
        '*******************************************************************************
        '観測データマスク

        Option Explicit
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'ヘッダーラベル。
        Private Const TAG_TIMEOFFIRSTOBS As String = "TIME OF FIRST OBS"
        Private Const TAG_TIMEOFLASTOBS As String = "TIME OF LAST OBS"
        Private Const TAG_NUMTYPESOFOBSERV As String = "# / TYPES OF OBSERV"
        Private Const TAG_RCVCLOCKOFFSAPPL As String = "RCV CLOCK OFFS APPL"
        Private Const TAG_NUMOFSATELLITES As String = "# OF SATELLITES"
        Private Const TAG_PRNNUMOFOBS As String = "PRN / # OF OBS"
        Private Const TAG_ENDOFHEADER As String = "END OF HEADER"

        '2017/07/07 NS6000対応。'''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Private Const TAG_RINEX3_END_OF_HEADER As String = "END OF HEADER"
        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'ヘッダーラベル。
        private const string TAG_TIMEOFFIRSTOBS = "TIME OF FIRST OBS";
        private const string TAG_TIMEOFLASTOBS = "TIME OF LAST OBS";
        private const string TAG_NUMTYPESOFOBSERV = "# / TYPES OF OBSERV";
        private const string TAG_RCVCLOCKOFFSAPPL = "RCV CLOCK OFFS APPL";
        private const string TAG_NUMOFSATELLITES = "# OF SATELLITES";
        private const string TAG_PRNNUMOFOBS = "PRN / # OF OBS";
        private const string TAG_ENDOFHEADER = "END OF HEADER";

        //'2017/07/07 NS6000対応。'''''''''''''''''''''''''''''''''''''''''''''''''''''''
        private const string TAG_RINEX3_END_OF_HEADER = "END OF HEADER";
        //'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        private Document document;
        private MdlMain mdlMain;
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'マスク情報。
        Private Type MaskInfo
            Number As Long '衛星番号。
            '1～32＝GPSの1～32番
            '38～63＝GLONASSの1～26番
            '65～72＝QZSSの1～8番
            '73～102＝Galileoの1～30番
            '105～139＝BeiDouの1～35番
            '140～198＝SBASの1～59番
            '199≦不明。
            Enabled As Boolean '有効フラグ。
            StrTimes() As Date 'マスク開始時間(以上)。配列の要素は(-1 To ...)、要素 -1 は未使用。
            EndTimes() As Date 'マスク終了時間(以下)。配列の要素は(-1 To ...)、要素 -1 は未使用。
        End Type
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'マスク情報。
        /*
        private struct MaskInfo
        {
            public long Number;            //'衛星番号。
                                           //'1～32＝GPSの1～32番
                                           //'38～63＝GLONASSの1～26番
                                           //'65～72＝QZSSの1～8番
                                           //'73～102＝Galileoの1～30番
                                           //'105～139＝BeiDouの1～35番
                                           //'140～198＝SBASの1～59番
                                           //'199≦不明。
            public bool Enabled;           //'有効フラグ。
            public DateTime[] StrTimes;    //'マスク開始時間(以上)。配列の要素は(-1 To ...)、要素 -1 は未使用。
            public DateTime[] EndTimes;    //'マスク終了時間(以下)。配列の要素は(-1 To ...)、要素 -1 は未使用。
        }
        */
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'インプリメンテーション
        Private m_tMaskInfos() As MaskInfo 'マスク情報。
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'インプリメンテーション
        public MaskInfo[] m_tMaskInfos2;    //'マスク情報。

        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '*******************************************************************************
        'プロパティ

        '観測データマスク。
        '
        'このプロパティはデフォルトプロパティである。Letを使えば｢値の代入｣ができる。いわゆるC++で言うところのoperator=である。
        '
        '引き数：
        'clsCoordinatePoint コピー元のオブジェクト。
        Property Let ObsDataMask(ByVal clsObsDataMask As ObsDataMask)
            ReDim m_tMaskInfos(-1 To clsObsDataMask.Count - 1)
            Dim i As Long
            For i = 0 To UBound(m_tMaskInfos)
                m_tMaskInfos(i).Number = clsObsDataMask.Number(i)
                m_tMaskInfos(i).Enabled = clsObsDataMask.Enabled(i)
                m_tMaskInfos(i).StrTimes = clsObsDataMask.StrTimes(i)
                m_tMaskInfos(i).EndTimes = clsObsDataMask.EndTimes(i)
            Next
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'マスク情報数。
        Property Let Count(ByVal nCount As Long)
            Dim i As Long
            i = UBound(m_tMaskInfos) + 1
            ReDim Preserve m_tMaskInfos(-1 To nCount - 1)
            For i = i To UBound(m_tMaskInfos)
                m_tMaskInfos(i).Enabled = True
                ReDim m_tMaskInfos(i).StrTimes(-1 To 1)
                ReDim m_tMaskInfos(i).EndTimes(-1 To 1)
            Next
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'マスク情報数。
        public void Count(long nCount)
        {
#if false
            /*
                *************************** 修正要 sakai
            */
            int i1 = 0;
            int i2 = (int)Count();
            MaskInfo[] m_tMaskInfos2 = new MaskInfo[nCount - 1];
            for (i1 = i2; i1 < m_tMaskInfos2.Length; i1++)
            {
                m_tMaskInfos2[i1].Enabled = true;
                MaskInfo[] m_tMaskInfos2[i1].StrTimes = new MaskInfo[1];
                MaskInfo[] m_tMaskInfos2[i1].EndTimes = new MaskInfo[1];
            }
#endif
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'マスク情報数。
        Property Get Count() As Long
            Count = UBound(m_tMaskInfos) + 1
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'マスク情報数。
        public long Count()
        {
            return m_tMaskInfos2.Length + 1;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '衛星番号。
        Property Let Number(ByVal nIndex As Long, ByVal nNumber As Long)
            m_tMaskInfos(nIndex).Number = nNumber
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '衛星番号。
        Property Get Number(ByVal nIndex As Long) As Long
            Number = m_tMaskInfos(nIndex).Number
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '有効フラグ。
        Property Get Enabled(ByVal nIndex As Long) As Boolean
            If nIndex < 0 Or UBound(m_tMaskInfos) < nIndex Then
                Enabled = True
            Else
                Enabled = m_tMaskInfos(nIndex).Enabled
            End If
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'マスク開始時間(以上)。配列の要素は(-1 To ...)、要素 -1 は未使用。
        Property Get StrTimes(ByVal nIndex As Long) As Date()
            If nIndex < 0 Or UBound(m_tMaskInfos) < nIndex Then
                Dim dStrTimes() As Date
                ReDim dStrTimes(-1 To -1)
                StrTimes = dStrTimes
            Else
                StrTimes = m_tMaskInfos(nIndex).StrTimes
            End If
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'マスク終了時間(以下)。配列の要素は(-1 To ...)、要素 -1 は未使用。
        Property Get EndTimes(ByVal nIndex As Long) As Date()
            If nIndex < 0 Or UBound(m_tMaskInfos) < nIndex Then
                Dim dEndTimes() As Date
                ReDim dEndTimes(-1 To -1)
                EndTimes = dEndTimes
            Else
                EndTimes = m_tMaskInfos(nIndex).EndTimes
            End If
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '*******************************************************************************
        'イベント

        '初期化。
        Private Sub Class_Initialize()
            ReDim m_tMaskInfos(-1 To -1)
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
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
            Put #nFile, , Count
            Dim i As Long
            For i = 0 To Count - 1
                Put #nFile, , m_tMaskInfos(i).Number
                Put #nFile, , m_tMaskInfos(i).Enabled
                Dim nUBound As Long
                nUBound = UBound(m_tMaskInfos(i).StrTimes)
                Put #nFile, , nUBound
                Dim j As Long
                For j = 0 To nUBound
                    Put #nFile, , m_tMaskInfos(i).StrTimes(j)
                    Put #nFile, , m_tMaskInfos(i).EndTimes(j)
                Next
            Next
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //==========================================================================================


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

            //23/12/20 K.Setoguchi---->>>>
            ////Document document = new Document();
            byte[] buf = new byte[8]; int ret;

            //----------------------------------------------------
            long nCount = br.ReadInt32();

            //----------------------------------------------------
            var m_tMaskInfos_struct = new MaskInfo();
            List<MaskInfo> m_tMaskInfosA = new List<MaskInfo>();
            Genba_S.m_tMaskInfos = m_tMaskInfosA;
            //----------------------------------------------------

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
                List<MaskInfo_T> m_tMaskInfosT = new List<MaskInfo_T>();
                Genba_S.m_tMaskInfos_T = m_tMaskInfosT;

                if (m_tMaskInfos_struct.nUBound != -1)         //47回目0xFFFFFFFFF
                {

                    for (long j = 0; j <= m_tMaskInfos_struct.nUBound; j++)
                    {
                        // 4byte読み取り
                        ret = br.Read(buf, 0, 8);
                        MaskInfo_T_struct.StrTimes = DateTime.FromOADate(BitConverter.ToDouble(buf, 0));

                        MaskInfo_T_struct.EndTimes = DateTime.FromOADate(BitConverter.ToDouble(buf, 0));

                        //-----------------------------------------------------
                        // 再配置<-読み込みデータ情報
                        //-----------------------------------------------------
                        Genba_S.m_tMaskInfos_T.Add(MaskInfo_T_struct);
                    }

                }
                else
                {
                    MaskInfo_T_struct.StrTimes = DateTime.MinValue;
                    MaskInfo_T_struct.EndTimes = DateTime.MinValue;
                    //-----------------------------------------------------
                    // 再配置<-読み込みデータ情報
                    //-----------------------------------------------------
                    Genba_S.m_tMaskInfos_T.Add(MaskInfo_T_struct);

                }


            }   // for (long i = 0; i < nCount; i++)

            //<<<<----23/12/20 K.Setoguchi

            //<<<<----23/12/20 K.Setoguchi  ＜＜＜　23/12/20以前の処理は削除　＞＞＞
            //23/12/20 K.Setoguchi---->>>>  ＜＜＜　23/12/20以前の処理は削除　＞＞＞

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


        //==========================================================================================
        /*[VB]
        'マスク情報の設定。
        '
        '引き数：
        'nIndex マスク情報のインデックス。
        'bEnabled 有効フラグ。
        'dStrTimes マスク開始時間(以上)。配列の要素は(-1 To ...)、要素 -1 は未使用。
        'dEndTimes マスク終了時間(以下)。配列の要素は(-1 To ...)、要素 -1 は未使用。
        Public Sub SetMaskInfo(ByVal nIndex As Long, ByVal bEnabled As Boolean, ByRef dStrTimes() As Date, ByRef dEndTimes() As Date)
            m_tMaskInfos(nIndex).Enabled = bEnabled
            m_tMaskInfos(nIndex).StrTimes = dStrTimes
            m_tMaskInfos(nIndex).EndTimes = dEndTimes
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'マスク情報のインデックスを取得する。
        '
        '指定された衛星番号に対応するインデックスを取得する。
        '
        '引き数：
        'nNumber 衛星番号。
        '
        '戻り値：マスク情報のインデックス。
        Public Function GetIndex(ByVal nNumber As Long) As Long
            GetIndex = -1
            Dim i As Long
            For i = 0 To UBound(m_tMaskInfos)
                If m_tMaskInfos(i).Number = nNumber Then
                    GetIndex = i
                    Exit Function
                End If
            Next
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '観測データファイルをマスクする。
        '
        '引き数：
        'sSrcPath マスクする観測データファイルのパス。
        'sDstPath マスクした観測データファイルのパス。
        'clsProgressInterface ProgressInterface オブジェクト。
        '
        '戻り値：
        '正常終了の場合 True を返す。
        'それ以外の場合 False を返す。
        Public Function MaskFile(ByVal sSrcPath As String, ByVal sDstPath As String, ByVal clsProgressInterface As ProgressInterface) As Boolean

            MaskFile = False
    
            On Error GoTo FileErrorHandler
    
            Dim clsSrcFile As New LineTextReader
            Call clsSrcFile.OpenFile(sSrcPath)
            Dim clsDstFile As New FileNumber
            Open sDstPath For Output Access Write Lock Write As #clsDstFile.Number
    
            '衛星番号→インデックス、変換テーブル。
            Dim objSattToIndex As New Collection
            Dim vIndex As Variant
            For vIndex = 0 To UBound(m_tMaskInfos)
                '2017/06/29 NS6000対応。'''''''''''''''''''''''''''''''''''''''''''''''''''''''
                'If m_tMaskInfos(vIndex).Number < 10 Then
                '    Call objSattToIndex.Add(vIndex, Format$(m_tMaskInfos(vIndex).Number, "\G@@"))
                '    Call objSattToIndex.Add(vIndex, Format$(m_tMaskInfos(vIndex).Number, " @@"))
                '    Call objSattToIndex.Add(vIndex, Format$(m_tMaskInfos(vIndex).Number, "\G00"))
                '    Call objSattToIndex.Add(vIndex, Format$(m_tMaskInfos(vIndex).Number, " 00"))
                'ElseIf m_tMaskInfos(vIndex).Number < 38 Then
                '    Call objSattToIndex.Add(vIndex, Format$(m_tMaskInfos(vIndex).Number, "\G@@"))
                '    Call objSattToIndex.Add(vIndex, Format$(m_tMaskInfos(vIndex).Number, " @@"))
                'ElseIf m_tMaskInfos(vIndex).Number < 47 Then
                '    Call objSattToIndex.Add(vIndex, Format$(m_tMaskInfos(vIndex).Number - 37, "\R@@"))
                '    Call objSattToIndex.Add(vIndex, Format$(m_tMaskInfos(vIndex).Number - 37, "\R00"))
                'Else
                '    Call objSattToIndex.Add(vIndex, Format$(m_tMaskInfos(vIndex).Number - 37, "\R@@"))
                'End If
                '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                If m_tMaskInfos(vIndex).Number < 10 Then
                    Call objSattToIndex.Add(vIndex, Format$(m_tMaskInfos(vIndex).Number, "\G@@"))
                    Call objSattToIndex.Add(vIndex, Format$(m_tMaskInfos(vIndex).Number, " @@"))
                    Call objSattToIndex.Add(vIndex, Format$(m_tMaskInfos(vIndex).Number, "\G00"))
                    Call objSattToIndex.Add(vIndex, Format$(m_tMaskInfos(vIndex).Number, " 00"))
                ElseIf m_tMaskInfos(vIndex).Number < 38 Then
                    Call objSattToIndex.Add(vIndex, Format$(m_tMaskInfos(vIndex).Number, "\G@@"))
                    Call objSattToIndex.Add(vIndex, Format$(m_tMaskInfos(vIndex).Number, " @@"))
                ElseIf m_tMaskInfos(vIndex).Number < 47 Then
                    Call objSattToIndex.Add(vIndex, Format$(m_tMaskInfos(vIndex).Number - 37, "\R@@"))
                    Call objSattToIndex.Add(vIndex, Format$(m_tMaskInfos(vIndex).Number - 37, "\R00"))
                ElseIf m_tMaskInfos(vIndex).Number < 65 Then
                    Call objSattToIndex.Add(vIndex, Format$(m_tMaskInfos(vIndex).Number - 37, "\R@@"))
                ElseIf m_tMaskInfos(vIndex).Number < 73 Then
                    Call objSattToIndex.Add(vIndex, Format$(m_tMaskInfos(vIndex).Number - 64, "\J@@"))
                    Call objSattToIndex.Add(vIndex, Format$(m_tMaskInfos(vIndex).Number - 64, "\J00"))
                ElseIf m_tMaskInfos(vIndex).Number < 82 Then
                    Call objSattToIndex.Add(vIndex, Format$(m_tMaskInfos(vIndex).Number - 72, "\E@@"))
                    Call objSattToIndex.Add(vIndex, Format$(m_tMaskInfos(vIndex).Number - 72, "\E00"))
                ElseIf m_tMaskInfos(vIndex).Number < 109 Then
                    Call objSattToIndex.Add(vIndex, Format$(m_tMaskInfos(vIndex).Number - 72, "\E@@"))
                ElseIf m_tMaskInfos(vIndex).Number < 118 Then
                    Call objSattToIndex.Add(vIndex, Format$(m_tMaskInfos(vIndex).Number - 108, "\C@@"))
                    Call objSattToIndex.Add(vIndex, Format$(m_tMaskInfos(vIndex).Number - 108, "\C00"))
                ElseIf m_tMaskInfos(vIndex).Number < 146 Then
                    Call objSattToIndex.Add(vIndex, Format$(m_tMaskInfos(vIndex).Number - 108, "\C@@"))
                ElseIf m_tMaskInfos(vIndex).Number < 155 Then
                    Call objSattToIndex.Add(vIndex, Format$(m_tMaskInfos(vIndex).Number - 145, "\S@@"))
                    Call objSattToIndex.Add(vIndex, Format$(m_tMaskInfos(vIndex).Number - 145, "\S00"))
                ElseIf m_tMaskInfos(vIndex).Number < 205 Then
                    Call objSattToIndex.Add(vIndex, Format$(m_tMaskInfos(vIndex).Number - 145, "\S@@"))
                End If
                '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            Next
    
            'ヘッダー部。
            Dim objHeaderSeek As New Collection
            Dim nYearOfFirstObs As Long
            Dim nFrequency As Long
            Dim bClockOffset As Boolean 'クロックオフセットフラグ。True=補正あり、False=補正なし。
            Call CopyHeader(clsSrcFile, clsDstFile.Number, objHeaderSeek, nYearOfFirstObs, nFrequency, bClockOffset, clsProgressInterface)
    
            'データ部。
            Dim dTimeOfFirstObs As Date
            Dim sSecondOfFirstObs As String
            Call CopyEpoch(clsSrcFile, clsDstFile.Number, objSattToIndex, nYearOfFirstObs, dTimeOfFirstObs, sSecondOfFirstObs, nFrequency, bClockOffset, clsProgressInterface)
    
            Call clsSrcFile.CloseFile
            Call clsDstFile.CloseFile
            Set clsDstFile = New FileNumber
            Open sDstPath For Binary Access Write Lock Write As #clsDstFile.Number
    
            'ヘッダー部の変更。
            Call ChangeHeader(clsDstFile.Number, objHeaderSeek, dTimeOfFirstObs, sSecondOfFirstObs, clsProgressInterface)
    
            MaskFile = True
    
            Exit Function
    
        FileErrorHandler:
            If Err.Number <> cdlCancel Then
                Call MsgBox(Err.Description, vbCritical)
            Else
                Call Err.Raise(Err.Number, Err.Source, Err.Description, Err.HelpFile, Err.HelpContext)
            End If
    
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'ヘッダー部のコピー。
        '
        'あとで必要になる行のシーク位置をチェックしながらコピーする。
        '
        '引き数：
        'clsSrcFile マスクする観測データファイル。
        'nDstFile マスクした観測データファイル。
        'objHeaderSeek ヘッダー項目のシーク位置が設定される。要素はシーク位置。キーはヘッダーラベル。
        'nYearOfFirstObs 観測開始日時の年が設定される。
        'nFrequency 周波数の数が設定される。
        'bClockOffset クロックオフセットフラグ。True=補正あり、False=補正なし。
        'clsProgressInterface ProgressInterface オブジェクト。
        Public Function CopyHeader(ByVal clsSrcFile As LineTextReader, ByVal nDstFile As Integer, ByVal objHeaderSeek As Collection, ByRef nYearOfFirstObs As Long, ByRef nFrequency As Long, ByRef bClockOffset As Boolean, ByVal clsProgressInterface As ProgressInterface) As Collection

            Dim nLenTIMEOFFIRSTOBS As Long
            Dim nLenTIMEOFLASTOBS As Long
            Dim nLenNUMTYPESOFOBSERV As Long
            Dim nLenRCVCLOCKOFFSAPPL As Long
            Dim nLenNUMOFSATELLITES As Long
            Dim nLenPRNNUMOFOBS As Long
            Dim nLenENDOFHEADER As Long
            nLenTIMEOFFIRSTOBS = Len(TAG_TIMEOFFIRSTOBS)
            nLenTIMEOFLASTOBS = Len(TAG_TIMEOFLASTOBS)
            nLenNUMTYPESOFOBSERV = Len(TAG_NUMTYPESOFOBSERV)
            nLenRCVCLOCKOFFSAPPL = Len(TAG_RCVCLOCKOFFSAPPL)
            nLenNUMOFSATELLITES = Len(TAG_NUMOFSATELLITES)
            nLenPRNNUMOFOBS = Len(TAG_PRNNUMOFOBS)
            nLenENDOFHEADER = Len(TAG_ENDOFHEADER)
    
            nFrequency = -1
            bClockOffset = False
    
            Do While Not clsSrcFile.IsEOF
                Dim sBuff As String
                sBuff = clsSrcFile.ReadLine()
                Dim vSeek As Variant
                vSeek = Seek(nDstFile)
        
                If Mid(sBuff, 61, nLenTIMEOFFIRSTOBS) = TAG_TIMEOFFIRSTOBS Then
                    nYearOfFirstObs = Val(Mid(sBuff, 1, 6))
                    Call objHeaderSeek.Add(vSeek, TAG_TIMEOFFIRSTOBS)
                    Print #nDstFile, sBuff
                ElseIf Mid(sBuff, 61, nLenTIMEOFLASTOBS) = TAG_TIMEOFLASTOBS Then
                    'オプション項目は削除。
                ElseIf Mid(sBuff, 61, nLenNUMTYPESOFOBSERV) = TAG_NUMTYPESOFOBSERV Then
                    If nFrequency < 0 Then
                        nFrequency = Val(Left(sBuff, 6))
                        Call objHeaderSeek.Add(vSeek, TAG_NUMTYPESOFOBSERV)
                    End If
                    Print #nDstFile, sBuff
                ElseIf Mid(sBuff, 61, nLenRCVCLOCKOFFSAPPL) = TAG_RCVCLOCKOFFSAPPL Then
                    bClockOffset = (Val(Left(sBuff, 6)) = 1)
                    Print #nDstFile, sBuff
                ElseIf Mid(sBuff, 61, nLenNUMOFSATELLITES) = TAG_NUMOFSATELLITES Then
                    'オプション項目は削除。
                ElseIf Mid(sBuff, 61, nLenPRNNUMOFOBS) = TAG_PRNNUMOFOBS Then
                    'オプション項目は削除。
                ElseIf Mid(sBuff, 61, nLenENDOFHEADER) = TAG_ENDOFHEADER Then
                    Print #nDstFile, sBuff
                    Exit Do
                Else
                    Print #nDstFile, sBuff
                End If
        
                'プログレス。
                Call clsProgressInterface.CheckCancel
            Loop
    
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'データ部のコピー。
        '
        'マスクしながらコピーする。
        '
        '引き数：
        'clsSrcFile マスクする観測データファイル。
        'nDstFile マスクした観測データファイル。
        'objSattToIndex 衛星番号→配列インデックス変換。要素は配列インデックス。キーは衛星番号(G**)。
        'nYearOfFirstObs 観測開始日時の年。
        'dTimeOfFirstObs 観測開始日時が設定される。
        'sSecondOfFirstObs 観測終了日時の少数点以下秒が設定される。
        'nFrequency 周波数の数。
        'bClockOffset クロックオフセットフラグ。True=補正あり、False=補正なし。
        'clsProgressInterface ProgressInterface オブジェクト。
        Public Sub CopyEpoch(ByVal clsSrcFile As LineTextReader, ByVal nDstFile As Integer, ByVal objSattToIndex As Collection, ByVal nYearOfFirstObs As Long, ByRef dTimeOfFirstObs As Date, ByRef sSecondOfFirstObs As String, ByVal nFrequency As Long, ByVal bClockOffset As Boolean, ByVal clsProgressInterface As ProgressInterface)

            '周波数レコードの行数。周波数が6個以上だと複数行になる。
            Dim nFrequencyLine As Long
            Do
                nFrequencyLine = nFrequencyLine + 1
                If nFrequency <= nFrequencyLine * 5 Then Exit Do
            Loop
            '周波数レコード読み込みバッファ。
            Dim sFrequencys() As String
            ReDim sFrequencys(-1 To 48 * nFrequencyLine - 1) '適当に多め。
            'マスク時間のインデックス。
            Dim nTimeIndexs() As Long
            ReDim nTimeIndexs(-1 To UBound(m_tMaskInfos))
            '観測開始日時の年。
            Dim nYearLo As Long
            Dim nYearHi As Long
            nYearLo = nYearOfFirstObs Mod 100
            nYearHi = Fix(nYearOfFirstObs / 100) * 100
    
            Dim bTime As Boolean
            Do While Not clsSrcFile.IsEOF
                Dim sBuff As String
                sBuff = clsSrcFile.ReadLine()
        
                'エポックフラグ。
                Dim nFlag As Long
                nFlag = Val(Mid(sBuff, 27, 3))
        
                If 2 <= nFlag And nFlag <= 5 Then
                    'イベント(コメント)。
                    Dim nLines As Long
                    nLines = Val(Mid(sBuff, 30, 3))
                    Print #nDstFile, sBuff
                    Dim i As Long
                    For i = 1 To nLines
                        sBuff = clsSrcFile.ReadLine()
                        Print #nDstFile, sBuff
                    Next
                Else
                    '日時。
                    Dim nYear As Long
                    Dim nMon As Long
                    Dim nDay As Long
                    Dim nHour As Long
                    Dim nMin As Long
                    Dim nSec As Long
                    nYear = Val(Mid(sBuff, 1, 3))
                    nMon = Val(Mid(sBuff, 4, 3))
                    nDay = Val(Mid(sBuff, 7, 3))
                    nHour = Val(Mid(sBuff, 10, 3))
                    nMin = Val(Mid(sBuff, 13, 3))
                    nSec = Val(Mid(sBuff, 16, 11))
                    '年の変換。
                    If nYear < nYearLo Then
                        nYear = nYearHi + 100 + nYear
                    ElseIf (nYear - nYearLo) > 50 Then '観測開始日時より前の日時はありえないけど。。。
                        nYear = nYearHi - 100 + nYear
                    Else
                        nYear = nYearHi + nYear
                    End If
                    '日時。
                    Dim dTime As Date
                    If nSec < 60 Then
                        dTime = DateSerial(nYear, nMon, nDay) + TimeSerial(nHour, nMin, nSec)
                    Else
                        dTime = DateSerial(nYear, nMon, nDay) + TimeSerial(nHour, nMin, 59)
                        dTime = DateAdd("s", 1, dTime)
                    End If
                    '衛星数。
                    Dim nCount As Long
                    nCount = Val(Mid(sBuff, 30, 3))
                    '衛星番号。
                    Dim sNumbers As String
                    sNumbers = RTrim(Mid(sBuff, 33, 36))
                    For i = 1 To Fix((nCount - 1) / 12)
                        Dim sTemp As String
                        sTemp = clsSrcFile.ReadLine()
                        sNumbers = sNumbers & RTrim(Mid(sTemp, 33, 36))
                    Next
            
                    Dim nNewCount As Long
                    Dim sNewNumbers As String
                    nNewCount = 0
                    sNewNumbers = ""
                    Dim nFrequencysIndex As Long
                    nFrequencysIndex = -1
                    Dim bCrLf As Boolean
                    bCrLf = False
            
                    For i = 0 To nCount - 1
                        Dim sNumber As String
                        sNumber = Mid(sNumbers, i * 3 + 1, 3)
                
                        'マスク評価。
                        Dim bMask As Boolean
                
        '32番衛星対応のPPS(1.04)に対応するために、マスク処理を解除　2008/2/22 NGS Yamada
        '                If sNumber = "G32" Then    '32番衛星をPPSに送るとエラーになるので強制的にマスクする　2007/9/27 NGS Yamada
        '                    bMask = True
        '                Else
                        Dim vIndex As Variant
                        If LookupCollectionVariant(objSattToIndex, vIndex, sNumber) Then
                            If m_tMaskInfos(vIndex).Enabled Then
                                Do
                                    If UBound(m_tMaskInfos(vIndex).StrTimes) < nTimeIndexs(vIndex) Then
                                        bMask = False
                                        Exit Do
                                    ElseIf DateDiff("s", dTime, m_tMaskInfos(vIndex).StrTimes(nTimeIndexs(vIndex))) > 0 Then
                                        bMask = False
                                        Exit Do
                                    ElseIf DateDiff("s", dTime, m_tMaskInfos(vIndex).EndTimes(nTimeIndexs(vIndex))) >= 0 Then
                                        bMask = True
                                        Exit Do
                                    Else
                                        nTimeIndexs(vIndex) = nTimeIndexs(vIndex) + 1
                                    End If
                                Loop
                            Else
                                bMask = True
                            End If
                        Else
                            bMask = False
                        End If
        '                End If
                
                        If bMask Then
                            '読み飛ばす。
                            Dim j As Long
                            For j = 1 To nFrequencyLine
                                sTemp = clsSrcFile.ReadLine()
                            Next
                        Else
                            nNewCount = nNewCount + 1
                            If bCrLf Then
                                bCrLf = False
                                sNewNumbers = sNewNumbers & vbCrLf & Space(32) & sNumber
                            Else
                                sNewNumbers = sNewNumbers & sNumber
                                If (nNewCount Mod 12) = 0 Then
                                    If bClockOffset Then sNewNumbers = sNewNumbers & Mid(sBuff, 69, 12) 'クロックオフセットを挿入してから改行。
                                    bCrLf = True
                                End If
                            End If
                            For j = 1 To nFrequencyLine
                                nFrequencysIndex = nFrequencysIndex + 1
                                sFrequencys(nFrequencysIndex) = clsSrcFile.ReadLine()
                            Next
                        End If
                    Next
                    'クロックオフセットを挿入。
                    If bClockOffset Then
                        '1行目だけに挿入する。
                        If Len(sNewNumbers) < 48 Then sNewNumbers = sNewNumbers & Space(68 - 32 - (nNewCount Mod 12) * 3) & Mid(sBuff, 69, 12)
                    End If
            
                    If nNewCount > 0 Then
                        If Not bTime Then
                            '観測開始日時。
                            bTime = True
                            nSec = Val(Mid(sBuff, 16, 3))
                            dTimeOfFirstObs = DateSerial(nYear, nMon, nDay) + TimeSerial(nHour, nMin, nSec)
                            sSecondOfFirstObs = Mid(sBuff, 19, 8)
                        End If
                        'マスクされなかった衛星の情報を書き込む。
                        Print #nDstFile, Left(sBuff, 29) & Format$(nNewCount, "@@@") & sNewNumbers
                        For i = 0 To nFrequencysIndex
                            Print #nDstFile, sFrequencys(i)
                        Next
                    Else
                        '衛星全部がマスクされた。
                    End If
                End If
        
                'プログレス。
                Call clsProgressInterface.CheckCancel
            Loop
    
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'ヘッダー部の変更。
        '
        '引き数：
        'nDstFile マスクした観測データファイル。
        'objHeaderSeek ヘッダー項目のシーク位置。要素はシーク位置。キーはヘッダーラベル。
        'dTimeOfFirstObs 観測開始日時。
        'sSecondOfFirstObs 観測終了日時の少数点以下秒。
        'clsProgressInterface ProgressInterface オブジェクト。
        Public Sub ChangeHeader(ByVal nDstFile As Integer, ByVal objHeaderSeek As Collection, ByRef dTimeOfFirstObs As Date, ByRef sSecondOfFirstObs As String, ByVal clsProgressInterface As ProgressInterface)

            Dim sBuff As String
            Dim nBuff() As Byte
    
            '観測開始日時。
            sBuff = Format$(Year(dTimeOfFirstObs), "@@@@@@") & Format$(Month(dTimeOfFirstObs), "@@@@@@") & Format$(Day(dTimeOfFirstObs), "@@@@@@") & Format$(Hour(dTimeOfFirstObs), "@@@@@@") & Format$(Minute(dTimeOfFirstObs), "@@@@@@") & Format$(Second(dTimeOfFirstObs), "@@@@@") & sSecondOfFirstObs
            nBuff = StrConv(sBuff, vbFromUnicode)
            Seek #nDstFile, objHeaderSeek.Item(TAG_TIMEOFFIRSTOBS)
            Put #nDstFile, , nBuff
    
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '2017/07/06 NS6000対応。'''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '観測データファイルをマスクする。RINEX3.00
        '
        '引き数：
        'sSrcPath マスクする観測データファイルのパス。
        'sDstPath マスクした観測データファイルのパス。
        'clsProgressInterface ProgressInterface オブジェクト。
        '
        '戻り値：
        '正常終了の場合 True を返す。
        'それ以外の場合 False を返す。
        Public Function MaskFile3(ByVal sSrcPath As String, ByVal sDstPath As String, ByVal clsProgressInterface As ProgressInterface) As Boolean

            MaskFile3 = False
    
            On Error GoTo FileErrorHandler
    
            'ヘッダーは加工せずにそのまま転記。
            'データ部は単純にマスクする衛星を削除するだけ。
            'イベントも加工せずにそのまま転記。
    
            Dim clsSrcFile As New LineTextReader
            Call clsSrcFile.OpenFile(sSrcPath)
            Dim clsDstFile As New FileNumber
            Open sDstPath For Output Access Write Lock Write As #clsDstFile.Number
    
            '衛星番号→インデックス、変換テーブル。
            Dim objSattToIndex As New Collection
            Dim vIndex As Variant
            For vIndex = 0 To UBound(m_tMaskInfos)
                If m_tMaskInfos(vIndex).Number < 10 Then
                    Call objSattToIndex.Add(vIndex, Format$(m_tMaskInfos(vIndex).Number, "\G@@"))
                    Call objSattToIndex.Add(vIndex, Format$(m_tMaskInfos(vIndex).Number, "\G00"))
                ElseIf m_tMaskInfos(vIndex).Number < 38 Then
                    Call objSattToIndex.Add(vIndex, Format$(m_tMaskInfos(vIndex).Number, "\G@@"))
                ElseIf m_tMaskInfos(vIndex).Number < 47 Then
                    Call objSattToIndex.Add(vIndex, Format$(m_tMaskInfos(vIndex).Number - 37, "\R@@"))
                    Call objSattToIndex.Add(vIndex, Format$(m_tMaskInfos(vIndex).Number - 37, "\R00"))
                ElseIf m_tMaskInfos(vIndex).Number < 65 Then
                    Call objSattToIndex.Add(vIndex, Format$(m_tMaskInfos(vIndex).Number - 37, "\R@@"))
                ElseIf m_tMaskInfos(vIndex).Number < 73 Then
                    Call objSattToIndex.Add(vIndex, Format$(m_tMaskInfos(vIndex).Number - 64, "\J@@"))
                    Call objSattToIndex.Add(vIndex, Format$(m_tMaskInfos(vIndex).Number - 64, "\J00"))
                ElseIf m_tMaskInfos(vIndex).Number < 82 Then
                    Call objSattToIndex.Add(vIndex, Format$(m_tMaskInfos(vIndex).Number - 72, "\E@@"))
                    Call objSattToIndex.Add(vIndex, Format$(m_tMaskInfos(vIndex).Number - 72, "\E00"))
                ElseIf m_tMaskInfos(vIndex).Number < 109 Then
                    Call objSattToIndex.Add(vIndex, Format$(m_tMaskInfos(vIndex).Number - 72, "\E@@"))
                ElseIf m_tMaskInfos(vIndex).Number < 118 Then
                    Call objSattToIndex.Add(vIndex, Format$(m_tMaskInfos(vIndex).Number - 108, "\C@@"))
                    Call objSattToIndex.Add(vIndex, Format$(m_tMaskInfos(vIndex).Number - 108, "\C00"))
                ElseIf m_tMaskInfos(vIndex).Number < 146 Then
                    Call objSattToIndex.Add(vIndex, Format$(m_tMaskInfos(vIndex).Number - 108, "\C@@"))
                ElseIf m_tMaskInfos(vIndex).Number < 155 Then
                    Call objSattToIndex.Add(vIndex, Format$(m_tMaskInfos(vIndex).Number - 145, "\S@@"))
                    Call objSattToIndex.Add(vIndex, Format$(m_tMaskInfos(vIndex).Number - 145, "\S00"))
                ElseIf m_tMaskInfos(vIndex).Number < 205 Then
                    Call objSattToIndex.Add(vIndex, Format$(m_tMaskInfos(vIndex).Number - 145, "\S@@"))
                End If
            Next
    
            'ヘッダー部。
            Do While Not clsSrcFile.IsEOF
                Dim sBuff As String
                sBuff = clsSrcFile.ReadLine()
                Print #clsDstFile.Number, sBuff
        
                Dim sTag As String
                sTag = RTrim(Mid(sBuff, 61, 64))
                If sTag = TAG_RINEX3_END_OF_HEADER Then
                    Exit Do
                End If
        
                'プログレス。
                Call clsProgressInterface.CheckCancel
            Loop
    
            'マスク時間のインデックス。
            Dim nTimeIndexs() As Long
            ReDim nTimeIndexs(-1 To UBound(m_tMaskInfos))
    
            'データ部。
            Dim i As Long
            Do While Not clsSrcFile.IsEOF
                sBuff = clsSrcFile.ReadLine()
        
                'Record identifier
                If Left(sBuff, 1) = ">" Then
                    Dim nFlag As Long
                    Dim nCount As Long
                    nFlag = Val(Mid(sBuff, 30, 3))
                    nCount = Val(Mid(sBuff, 33, 3))
                    'エポックフラグが0か1なら観測データ。
                    If nFlag = 0 Or nFlag = 1 Then
                        '観測データ。
                        '日時。
                        Dim nYear As Long
                        Dim nMon As Long
                        Dim nDay As Long
                        Dim nHour As Long
                        Dim nMin As Long
                        Dim nSec As Long
                        nYear = Val(Mid(sBuff, 3, 4))
                        nMon = Val(Mid(sBuff, 7, 3))
                        nDay = Val(Mid(sBuff, 10, 3))
                        nHour = Val(Mid(sBuff, 13, 3))
                        nMin = Val(Mid(sBuff, 16, 3))
                        nSec = Val(Mid(sBuff, 19, 11))
                        Dim dTime As Date
                        If nSec < 60 Then
                            dTime = DateSerial(nYear, nMon, nDay) + TimeSerial(nHour, nMin, nSec)
                        Else
                            dTime = DateSerial(nYear, nMon, nDay) + TimeSerial(nHour, nMin, 59)
                            dTime = DateAdd("s", 1, dTime)
                        End If
                
                        Dim sObsRecord() As String
                        ReDim sObsRecord(nCount - 1)
                        Dim nObsRecord As Long
                        nObsRecord = 0
                
                        For i = 1 To nCount
                            If clsSrcFile.IsEOF Then
                                Exit For
                            End If
                            sObsRecord(nObsRecord) = clsSrcFile.ReadLine()
                    
                            Dim sNumber As String
                            sNumber = Left(sObsRecord(nObsRecord), 3)
                    
                            Dim bMask As Boolean
                            If LookupCollectionVariant(objSattToIndex, vIndex, sNumber) Then
                                If m_tMaskInfos(vIndex).Enabled Then
                                    Do
                                        If UBound(m_tMaskInfos(vIndex).StrTimes) < nTimeIndexs(vIndex) Then
                                            bMask = False
                                            Exit Do
                                        ElseIf DateDiff("s", dTime, m_tMaskInfos(vIndex).StrTimes(nTimeIndexs(vIndex))) > 0 Then
                                            bMask = False
                                            Exit Do
                                        ElseIf DateDiff("s", dTime, m_tMaskInfos(vIndex).EndTimes(nTimeIndexs(vIndex))) >= 0 Then
                                            bMask = True
                                            Exit Do
                                        Else
                                            nTimeIndexs(vIndex) = nTimeIndexs(vIndex) + 1
                                        End If
                                    Loop
                                Else
                                    bMask = True
                                End If
                            Else
                                bMask = False
                            End If
                            If bMask Then
                                '除外する。
                            Else
                                nObsRecord = nObsRecord + 1
                            End If
                            'プログレス。
                            Call clsProgressInterface.CheckCancel
                        Next
                        '書き込み。
                        Print #clsDstFile.Number, Left(sBuff, 32) & Format$(nObsRecord, "@@@") & Mid(sBuff, 36, 21)
                        For i = 0 To nObsRecord - 1
                            Print #clsDstFile.Number, sObsRecord(i)
                        Next
                    Else
                        'その他イベント等。
                        '単純にレコード数だけ転記。
                        Print #clsDstFile.Number, sBuff
                        For i = 1 To nCount
                            If clsSrcFile.IsEOF Then
                                Exit For
                            End If
                            sBuff = clsSrcFile.ReadLine()
                            Print #clsDstFile.Number, sBuff
                            'プログレス。
                            Call clsProgressInterface.CheckCancel
                        Next
                    End If
                End If
        
                'プログレス。
                Call clsProgressInterface.CheckCancel
            Loop
    
            MaskFile3 = True
    
            Exit Function
    
        FileErrorHandler:
            If Err.Number <> cdlCancel Then
                Call MsgBox(Err.Description, vbCritical)
            Else
                Call Err.Raise(Err.Number, Err.Source, Err.Description, Err.HelpFile, Err.HelpContext)
            End If
    
        End Function
        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //==========================================================================================
    }
}
