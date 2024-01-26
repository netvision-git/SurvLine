using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace SurvLine
{
    public class ObservationPoint
    {

        //'*******************************************************************************
        //'観測点
        //
        //Option Explicit
        //
        //'プロパティ
        public long WorkKey;            // As Long '汎用作業キー。
        public object WorkObject;       // As Object '汎用作業オブジェクト。
        public long ObjectTypel;        // As Long 'オブジェクト種別。
        public object Owner;            // As Object '所有者。接合観測点の場合、基線ベクトルが保持される。代表観測点の場合、チェーンリストが保持される。

        //'インプリメンテーション
        private ObservationPointAttributes m_clsAttributes = new ObservationPointAttributes();      // '観測点属性。
        private ObservationShared m_clsObservationShared;               // As ObservationShared '観測共有情報。

        private ObservationPoint m_clsParentPoint;      // As ObservationPoint '親観測点。
        private ObservationPoint m_clsChildPoint;       // As ObservationPoint '子観測点。
        private ObservationPoint m_clsPrevPoint;        // As ObservationPoint '兄観測点。
        private ObservationPoint m_clsNextPoint;        // As ObservationPoint '弟観測点。
        private ObservationPoint m_clsCorrectPoint;     // As ObservationPoint '補正点。自オブジェクトが本点の場合は偏心点(HeadPoint)が設定される。自オブジェクトが偏心点の場合は本点(実観測点)が設定される。※相互参照なので Attributes や CommonAttributes のメンバにしてはいけない。
        private bool m_bEnable;                         // As Boolean '有効フラグ。True=有効。False=無効。



        //'*******************************************************************************
        //'*******************************************************************************


        Document document = new Document();

        //***************************************************************************
        //***************************************************************************
        /// <summary>
        ///'読み込み。
        ///'    長男が親観測点の Load を呼ぶ。
        ///     （VB Load関数をVC用に変更）
        ///'
        ///'引き数：
        ///  '引き数：
        /// バイナリファイル
        /// バージョン番号
        /// 読み込みデータ
        ///     ・ロードした観測点を保持するためのテンポラリ配列。配列の要素は(0 To ...)。Redim clsObservationPoints(0) で初期化されていること。
        ///     ・子観測点。
        /// </summary>
        /// <param name="BinaryReader br">
        /// <param name="nVersion br">
        /// <param name="Genba_S">
        /// </param>
        /// <returns>
        /// 戻り値:returns = Non
        /// </returns>
        //***************************************************************************
        //23/12/22 K.Setoguchi---->>>>
        // 旧処理を全削除
        //<<<<----23/12/22 K.Setoguchi
        //---------------------------------------------------------------------------
         //23/12/20 K.setoguchi@NV---------->>>>>>>>>>>
        public void Load(BinaryReader br, long nVersion, ref GENBA_STRUCT_S Genba_S, ref List<OPA_STRUCT_SUB> OPA_List)
        //<<<<<<<<<-----------23/12/20 K.setoguchi@NV
        {

            //----------------------------------------------------------------    
            //[VB]  Call m_clsAttributes.Load(nFile, nVersion)
            ObservationPointAttributes observationPointAttributes = new ObservationPointAttributes();
            observationPointAttributes.Load(br, nVersion, ref Genba_S);


            //----------------------------------------------------------------    
            //[VB]  Get #nFile, , m_bEnable     public bool m_bEnable;          //As Boolean '有効フラグ。True=有効。False=無効。   //true
            Genba_S.m_bEnable = document.GetFileBool(br);

            //[VB]  Get #nFile, , ObjectType    public long OP_ObjectType;      //As Long 'オブジェクト種別。                        //-2147483520
            Genba_S.OP_ObjectType = br.ReadInt32();

            //----------------------------------------------------------------    
            //    '長男フラグ｡                                                           //true
            //    Dim bFirst As Boolean
            //    Get #nFile, , bFirst
            bool bFirst;
            bFirst = document.GetFileBool(br);

            //----------------------------------------------------------------    
            //    '親フラグ。                                                            //true
            //    Dim bParent As Boolean
            //    Get #nFile, , bParent
            bool bParent;
            bParent = document.GetFileBool(br);



            //----------------------------------------------------------------    
            //    '結合キー。
            //    Dim nPrevJointKey As Long
            //    Dim nNextJointKey As Long
            //    Get #nFile, , nPrevJointKey                                           //0
            //    Get #nFile, , nNextJointKey                                           //1
            long nPrevJointKey;
            long nNextJointKey;
            nPrevJointKey = br.ReadInt32();
            nNextJointKey = br.ReadInt32();

            //-------------------------------------------------------------------------------------------------------------
            //    '配列を拡張する。
            //    Dim nMoreJointKey As Long
            //    If nPrevJointKey<nNextJointKey Then
            //        nMoreJointKey = nNextJointKey
            //    Else
            //        nMoreJointKey = nPrevJointKey
            //    End If
            //    If UBound(clsObservationPoints) < nMoreJointKey Then ReDim Preserve clsObservationPoints(nMoreJointKey)
            long nMoreJointKey;
            if (nPrevJointKey < nNextJointKey)
            {
                nMoreJointKey = nNextJointKey;                                      //1=1
            }
            else
            {
                nMoreJointKey = nPrevJointKey;
            }
            //If UBound(clsObservationPoints) < nMoreJointKey Then ReDim Preserve clsObservationPoints(nMoreJointKey)
            var Genba_S2 = new GENBA_STRUCT_S[nMoreJointKey];



             //23/12/20 K.setoguchi@NV---------->>>>>>>>>>>
            //-----------------------------------------------------
            Genba_S.OPA_ListWork.m_bEnable = Genba_S.m_bEnable;
            Genba_S.OPA_ListWork.OP_ObjectType = Genba_S.OP_ObjectType;
            Genba_S.OPA_ListWork.bFirst = bFirst;                       //'長男フラグ｡ 
            Genba_S.OPA_ListWork.bParent = bParent;                     //    '親フラグ。
            //-----------------------------------------------------
            Genba_S.OPA_ListWork.OPA = Genba_S.OPA;                     //共通OPAデータ（処理後、OPA --> OPA_Str / OPA_End）
            Genba_S.OPA_ListWork.OCA = Genba_S.OCA;                     //ObservationCommonAttributes
            Genba_S.OPA_ListWork.OPFix = Genba_S.OPFix;                 //CoordinatePointFix
            Genba_S.OPA_ListWork.CPXYZ = Genba_S.CPXYZ;                 //CoordinatePointXYZ
            //-----------------------------------------------------
            // 再配置<-読み込みデータ情報
            //-----------------------------------------------------
            OPA_List.Add(Genba_S.OPA_ListWork);
            //-----------------------------------------------------
            //<<<<<<<<<-----------23/12/20 K.setoguchi@NV



            //検討      //[VB]'子がいる場合は加える。
            //検討      //[VB]  If Not clsChildPoint Is Nothing Then Call AddChildPoint(clsChildPoint)

            //-------------------------------------------------------------------------------    
            //    '長男であり親がいる場合は親をロードする。
            //    If bFirst And bParent Then
            //        Dim clsParentPoint As New ObservationPoint
            //        Call clsParentPoint.Load(nFile, nVersion, clsObservationPoints, Me)
            //    End If
            //    
            if (bFirst && bParent)
            {
                ObservationPoint observationPoint = new ObservationPoint();
                //    observationPoint.Load(br, nVersion, ref Genba_S2[0]);
                observationPoint.Load(br, nVersion, ref Genba_S, ref OPA_List);
            }

            //-------------------------------------------------------------------------------    
            //    '兄はロード済みか？
            //    If clsObservationPoints(nPrevJointKey) Is Nothing Then
            //        '兄が未ロードであれば自分を設定する。
            //        Set clsObservationPoints(nPrevJointKey) = Me
            //    Else
            //        '兄がすでにロードされている場合は連結する。
            //        Call clsObservationPoints(nPrevJointKey).AddNextPoint(Me)
            //    End If
            //


            //検討    <<<<<< データを配列にロードする >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>


            //-------------------------------------------------------------------------------    
            //    '弟はロード済みか？
            //    If clsObservationPoints(nNextJointKey) Is Nothing Then
            //        '弟が未ロードであれば自分を設定する。
            //        Set clsObservationPoints(nNextJointKey) = Me
            //        '弟が未ロードなら結合キーは初期化する。
            //        WorkKey = -1
            //    Else
            //        '弟がすでにロードされている場合は結合キーを覚えておく。
            //        WorkKey = nNextJointKey
            //        '弟を連結する。
            //        Call AddNextPoint(clsObservationPoints(nNextJointKey))
            //    End If



            //検討    <<<<<< データを配列にロードする >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>


        }
        //
        //'読み込み。
        //'
        //'長男が親観測点の Load を呼ぶ。
        //'
        //'引き数：
        //'nFile ファイル番号。
        //'nVersion ファイルバージョン。
        //'clsObservationPoints ロードした観測点を保持するためのテンポラリ配列。配列の要素は(0 To ...)。Redim clsObservationPoints(0) で初期化されていること。
        //'clsChildPoint 子観測点。
        //Public Sub Load(ByVal nFile As Integer, ByVal nVersion As Long, ByRef clsObservationPoints() As ObservationPoint, ByVal clsChildPoint As ObservationPoint)
        //
        //    Call m_clsAttributes.Load(nFile, nVersion)
        //    Get #nFile, , m_bEnable
        //    Get #nFile, , ObjectType
        //    
        //    '長男フラグ｡
        //    Dim bFirst As Boolean
        //    Get #nFile, , bFirst
        //    '親フラグ。
        //    Dim bParent As Boolean
        //    Get #nFile, , bParent
        //    
        //    '結合キー。
        //    Dim nPrevJointKey As Long
        //    Dim nNextJointKey As Long
        //    Get #nFile, , nPrevJointKey
        //    Get #nFile, , nNextJointKey
        //    
        //    '配列を拡張する。
        //    Dim nMoreJointKey As Long
        //    If nPrevJointKey<nNextJointKey Then
        //        nMoreJointKey = nNextJointKey
        //    Else
        //        nMoreJointKey = nPrevJointKey
        //    End If
        //    If UBound(clsObservationPoints) < nMoreJointKey Then ReDim Preserve clsObservationPoints(nMoreJointKey)
        //    
        //    '子がいる場合は加える。
        //    If Not clsChildPoint Is Nothing Then Call AddChildPoint(clsChildPoint)
        //    
        //    '長男であり親がいる場合は親をロードする。
        //    If bFirst And bParent Then
        //        Dim clsParentPoint As New ObservationPoint
        //        Call clsParentPoint.Load(nFile, nVersion, clsObservationPoints, Me)
        //    End If
        //    
        //    '兄はロード済みか？
        //    If clsObservationPoints(nPrevJointKey) Is Nothing Then
        //        '兄が未ロードであれば自分を設定する。
        //        Set clsObservationPoints(nPrevJointKey) = Me
        //    Else
        //        '兄がすでにロードされている場合は連結する。
        //        Call clsObservationPoints(nPrevJointKey).AddNextPoint(Me)
        //    End If
        //    
        //    '弟はロード済みか？
        //    If clsObservationPoints(nNextJointKey) Is Nothing Then
        //        '弟が未ロードであれば自分を設定する。
        //        Set clsObservationPoints(nNextJointKey) = Me
        //        '弟が未ロードなら結合キーは初期化する。
        //        WorkKey = -1
        //    Else
        //        '弟がすでにロードされている場合は結合キーを覚えておく。
        //        WorkKey = nNextJointKey
        //        '弟を連結する。
        //        Call AddNextPoint(clsObservationPoints(nNextJointKey))
        //    End If
        //
        //End Sub




    }
}
