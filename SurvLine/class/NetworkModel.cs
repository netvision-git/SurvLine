//'*******************************************************************************
//'ネットワークモデル
//'
//'観測点と基線ベクトルで網を構成する。
//'基線ベクトルは BaseLineVector オブジェクトで表現される。
//'観測点は ObservationPoint オブジェクトで表現されるが、一つの観測点は複数の ObservationPoint オブジェクトで構成される。
//'ObservationPoint オブジェクトはツリー構造をもっている。
//'親子関係(Parent/Child)と兄弟関係(Prev/Next)でツリーを表現する。
//'一つの(インポートした)観測点を表現する ObservationPoint オブジェクトは3階層の親子関係をもつ。
//'一番上の階層は代表観測点と呼ばれる。
//'その下の階層は実観測点と呼ばれる。
//'一番下の階層は接合観測点と呼ばれる。
//'実観測点は実際にインポートした点一つに相当する。
//'代表観測点は配置図やリストペインなどで表現される点一つに相当する。
//'リストの一行は一つの(インポートした)観測点に相当するので、いまのところ代表観測点と実観測点は一対一で対応する。(一つの代表観測点は子観測点として一つの実観測点をもつ。)
//'接合観測点は基線ベクトルの両端に相当する。
//'実観測点は子観測点として複数の接合観測点をもつ。繋がっている基線ベクトルの数だけ接合観測点をもつ。
//'BaseLineVector オブジェクトは始点、終点として、接合観測点の参照を保持する。
//'基線ベクトルと繋がっていない観測点は孤立観測点と呼ばれる。孤立観測点には接合観測点が無い。
//'
//'オブジェクトのつながり：
//'
//'　○代表観測点　　　　　　　　　○代表観測点　　　　　　　　　○代表観測点　　　　　　　　　○代表観測点
//'　│　　　　　　　　　　　　　　│　　　　　　　　　　　　　　│　　　　　　　　　　　　　　│
//'　○実観測点　　　　　　　　　　○実観測点　　　　　　　　　　○実観測点　　　　　　　　　　○実観測点
//'　　＼　　　　　　　　　　　　／　＼　　　　　　　　　　　　／
//'　　　○─〓〓〓〓〓〓〓〓─○　　　○─〓〓〓〓〓〓〓〓─○
//'　　　接　　基線ベクトル　　接　　　接　　基線ベクトル　　接
//'　　　合　　　　　　　　　　合　　　合　　　　　　　　　　合　　　　　　　　　　　　　　　　↑
//'　　　観　　　　　　　　　　観　　　観　　　　　　　　　　観　　　　　　　　　　　　　　　　基線ベクトルと繋がってい
//'　　　測　　　　　　　　　　測　　　測　　　　　　　　　　測　　　　　　　　　　　　　　　　ない単独の観測点。孤立観
//'　　　点　　　　　　　　　　点　　　点　　　　　　　　　　点　　　　　　　　　　　　　　　　測点と呼ばれる。
//'
//'　　　　　　　　　　　　　└─────┘
//'　　　　　　　　　　　　　　配置図やリストで
//'　　　　　　　　　　　　　　表現される一つの点
//'
//'配置図上では複数の点が重なっている場合がある。一つの点に見えても、そこには別々の点が重なっていることに注意。
//'
//'BaseLineVector オブジェクトは m_clsBaseLineVectorHead のリストに保持される。
//'代表観測点は m_clsRepresentPointHead のリストに保持される。
//'孤立観測点はさらに m_clsIsolatePointHead のリストでも保持される。
using SurvLine;
//using static SurvLine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Net.WebRequestMethods;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;
using static SurvLine.mdl.MdlNSDefine;
using static SurvLine.mdl.MdlNSSDefine;
using System.Collections.ObjectModel;
using System.Collections;
using System.Runtime.InteropServices;
using SurvLine.mdl;
using static SurvLine.mdl.DEFINE;

namespace SurvLine
{
    public class NetworkModel
    {

        //Option Explicit
        //
        //'イベント
        //
        //'結合距離違反イベント。
        //'
        //'結合する観測点間の距離が制限を超えている場合に発生する。
        //'
        //'引き数：
        //'clsObservationPoint 違反の対象となる観測点。
        //'nDistance 観測点間の距離(ｍ)。
        //'nMax 距離の制限値(㎝)。
        //'nStyle メッセージスタイル。0=メッセージ無し。1=OKボタンのみ。2=OKボタンとすべてOKボタン。
        //'nResult メッセージボックスの結果が設定される。
        // Event CombinedDistanceViolation(ByVal clsObservationPoint As ObservationPoint, ByVal nDistance As Double, ByVal nMax As Long, ByVal nStyle As Long, ByRef nResult As Long)
        //
        //'上書き通知イベント。
        //'
        //'同じ観測点番号、セッション名の観測点をインポートしたときに、上書きを確認するために発生する。
        //'
        //'引き数：
        //'clsObservationPoint 対象となる観測点。
        //'nStyle メッセージスタイル。0=メッセージ無し。1=OKボタンのみ。2=OKボタンとすべてOKボタン。
        //'nResult メッセージボックスの結果が設定される。
        //Event OverwriteNotification(ByVal clsObservationPoint As ObservationPoint, ByVal nStyle As Long, ByRef nResult As Long) '上書き通知。


        //'インプリメンテーション
        //-----------------------------------------------------------------
        //      Private m_clsBaseLineVectorHead As New ChainList          '基線ベクトルリストのヘッダー。要素は BaseLineVector オブジェクト。
        private ChainList m_clsBaseLineVectorHead = new ChainList();    //'基線ベクトルリストのヘッダー。要素は BaseLineVector オブジェクト。
        //-----------------------------------------------------------------
        //      Private m_clsRepresentPointHead As New ChainList          '代表観測点リストのヘッダー。要素は ObservationPoint オブジェクト(代表観測点)。
        private ChainList m_clsRepresentPointHead = new ChainList();    //'代表観測点リストのヘッダー。要素は ObservationPoint オブジェクト(代表観測点)。
        //-----------------------------------------------------------------
        //      Private m_clsIsolatePointHead As New ChainList            '孤立観測点リストのヘッダー。要素は ObservationPoint オブジェクト(実観測点)。
        private ChainList m_clsIsolatePointHead = new ChainList();      //'孤立観測点リストのヘッダー。要素は ObservationPoint オブジェクト(実観測点)。
        //-----------------------------------------------------------------
        //      Private m_clsObservationShared As New ObservationShared   '観測共有情報。
        private ObservationShared m_clsObservationShared = new ObservationShared();       //'観測共有情報。
        //-----------------------------------------------------------------
        //      Private m_clsDispersion As New Dispersion '分散・共分散(固定重量)。
        private Dispersion m_clsDispersion = new Dispersion();



        //==========================================================================================
        /*[VB]
        '*******************************************************************************
        'プロパティ

        '基線ベクトル数。
        Property Get BaseLineVectorCount() As Long
            BaseLineVectorCount = m_clsBaseLineVectorHead.FollowingCount
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        'プロパティ
        
        '基線ベクトル数。
        */
        public long BaseLineVectorCount()
        {
            return m_clsBaseLineVectorHead.FollowingCount();
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '代表観測点数。
        Property Get RepresentPointCount() As Long
            RepresentPointCount = m_clsRepresentPointHead.FollowingCount
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //'代表観測点数。
        public long RepresentPointCount()
        {
            return m_clsRepresentPointHead.FollowingCount();
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '長男代表観測点数。
        Property Get HeadRepresentPointCount() As Long
            HeadRepresentPointCount = 0
            Dim clsChainList As ChainList
            Set clsChainList = m_clsRepresentPointHead.NextList
            Do While Not clsChainList Is Nothing
                If clsChainList.Element.PrevPoint Is Nothing Then HeadRepresentPointCount = HeadRepresentPointCount + 1
                Set clsChainList = clsChainList.NextList
            Loop
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'長男代表観測点数。
        public long HeadRepresentPointCount()
        {
            long w_HeadRepresentPointCount = 0;
            ChainList clsChainList;
            clsChainList = m_clsRepresentPointHead.NextList();
            while (clsChainList != null)
            {
#if false
                /*
                 *************************** 修正要 sakai
                 */
                if (clsChainList.Element.PrevPoint == null)
                {
                    w_HeadRepresentPointCount = w_HeadRepresentPointCount + 1;
                }
#endif
                clsChainList = clsChainList.NextList();
            }
            return w_HeadRepresentPointCount;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '有効な固定点の数。
        Property Get EnableFixedCount() As Long
            Dim clsChainList As ChainList
            Set clsChainList = m_clsRepresentPointHead.NextList
            EnableFixedCount = 0
            Do While Not clsChainList Is Nothing
                If clsChainList.Element.Fixed And clsChainList.Element.Enable Then EnableFixedCount = EnableFixedCount + 1
                Set clsChainList = clsChainList.NextList
            Loop
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'有効な固定点の数。
        public long EnableFixedCount()
        {
            ChainList clsChainList;
            clsChainList = m_clsRepresentPointHead.NextList();
            long w_EnableFixedCount = 0;
            while (clsChainList != null)
            {
#if false
                /*
                 *************************** 修正要 sakai
                 */
                if (clsChainList.Element.Fixed && clsChainList.Element.Enable)
                {
                    w_EnableFixedCount = w_EnableFixedCount + 1;
                }
#endif
                clsChainList = clsChainList.NextList();
            }
            return w_EnableFixedCount;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '有効な代表観測点の数。
        Property Get EnablePointCount() As Long
            Dim clsChainList As ChainList
            Set clsChainList = m_clsRepresentPointHead.NextList
            EnablePointCount = 0
            Do While Not clsChainList Is Nothing
                If clsChainList.Element.Enable Then EnablePointCount = EnablePointCount + 1
                Set clsChainList = clsChainList.NextList
            Loop
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'有効な代表観測点の数。
        public long EnablePointCount()
        {
            ChainList clsChainList;
            clsChainList = m_clsRepresentPointHead.NextList();
            long w_EnablePointCount = 0;
            while (clsChainList != null)
            {
#if false
                /*
                 *************************** 修正要 sakai
                 */
                if (clsChainList.Element.Enable)
                {
                    w_EnablePointCount = w_EnablePointCount + 1;
                }
#endif
                clsChainList = clsChainList.NextList();
            }
            return w_EnablePointCount;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '有効な基線ベクトルの数。
        Property Get EnableVectorCount() As Long
            Dim clsChainList As ChainList
            Set clsChainList = m_clsBaseLineVectorHead.NextList
            EnableVectorCount = 0
            Do While Not clsChainList Is Nothing
                If clsChainList.Element.Enable Then EnableVectorCount = EnableVectorCount + 1
                Set clsChainList = clsChainList.NextList
            Loop
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'有効な基線ベクトルの数。
        public long EnableVectorCount()
        {
            ChainList clsChainList;
            clsChainList = m_clsBaseLineVectorHead.NextList();
            long w_EnableVectorCount = 0;
            while (clsChainList != null)
            {
#if false
                /*
                 *************************** 修正要 sakai
                 */
                if (clsChainList.Element.Enable)
                {
                    w_EnableVectorCount = w_EnableVectorCount + 1;
                }
#endif
                clsChainList = clsChainList.NextList();
            }
            return w_EnableVectorCount;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '基線ベクトルリストの先頭。
        Property Get BaseLineVectorHead() As ChainList
            Set BaseLineVectorHead = m_clsBaseLineVectorHead.NextList
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'基線ベクトルリストの先頭。
        //  public ChainList BaseLineVectorHead()
        public object BaseLineVectorHead()
        {
            return m_clsBaseLineVectorHead.NextList();
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '代表観測点リストの先頭。
        Property Get RepresentPointHead() As ChainList
            Set RepresentPointHead = m_clsRepresentPointHead.NextList
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'代表観測点リストの先頭。
        //  public ChainList RepresentPointHead()
        public object RepresentPointHead()
        {
            return m_clsRepresentPointHead.NextList();
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '孤立観測点リストの先頭。
        Property Get IsolatePointHead() As ChainList
            Set IsolatePointHead = m_clsIsolatePointHead.NextList
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'孤立観測点リストの先頭。
        //public ChainList IsolatePointHead()
        public object IsolatePointHead()
        {
            return m_clsIsolatePointHead.NextList();
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '観測共有情報。
        Property Get ObservationShared() As ObservationShared
            Set ObservationShared = m_clsObservationShared
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'観測共有情報。
        //public ObservationShared ObservationShared()
        public object ObservationShared()
        {
            return m_clsObservationShared;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '分散・共分散(固定重量)。
        Property Let Dispersion(ByVal clsDispersion As Dispersion)
            Let m_clsDispersion = clsDispersion
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'分散・共分散(固定重量)。
        public Dispersion Dispersion(Dispersion clsDispersion)
        {
            m_clsDispersion = clsDispersion;
            return m_clsDispersion;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '分散・共分散(固定重量)。
        Property Get Dispersion() As Dispersion
            Set Dispersion = m_clsDispersion
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'分散・共分散(固定重量)。
        public Dispersion Dispersion()
        {
            return m_clsDispersion;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'うるう秒。
        Property Get LeapSeconds() As Long
            If m_clsRepresentPointHead.NextList Is Nothing Then
                LeapSeconds = GetPrivateProfileInt(PROFILE_SAVE_SEC_BASELINE, PROFILE_SAVE_KEY_DEFLEAPSEC, DEF_LEAP_SEC, App.Path & "\" & App.Title & PROFILE_SAVE_EXT)
            Else
                LeapSeconds = m_clsRepresentPointHead.NextList.Element.LeapSeconds
            End If
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'うるう秒。
        public long LeapSeconds()
        {
            if (m_clsRepresentPointHead.NextList() == null)
            {
                string AppPath = Path.GetDirectoryName(Application.ExecutablePath);
#if true
                /*
                 ****************** 修正要 sakai
                */
                string AppTitle = "SurvLine";
#endif
                return GetPrivateProfileInt(PROFILE_SAVE_SEC_BASELINE, PROFILE_SAVE_KEY_DEFLEAPSEC, (int)DEF_LEAP_SEC, AppPath + "\\" + AppTitle + PROFILE_SAVE_EXT);
            }
            else
            {
#if false
                /*
                 *************************** 修正要 sakai
                 */
                return m_clsRepresentPointHead.NextList().Element.LeapSeconds;
#else
                return 0;
#endif
            }
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '*******************************************************************************
        'イベント

        '初期化。
        Private Sub Class_Initialize()

            On Error GoTo ErrorHandler
    
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
                return;
            }

            catch
            {
                MdlMain mdlMain = new MdlMain();
                mdlMain.ErrorExit();
                return;
            }

        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '終了。
        Private Sub Class_Terminate()

            On Error GoTo ErrorHandler
    
            Dim clsChainList As ChainList
            Set clsChainList = m_clsBaseLineVectorHead.NextList
            Do While Not clsChainList Is Nothing
                Call clsChainList.Element.Terminate
                Set clsChainList = clsChainList.NextList
            Loop
            Call m_clsBaseLineVectorHead.RemoveAll
            Set m_clsBaseLineVectorHead = Nothing
            Set clsChainList = m_clsRepresentPointHead.NextList
            Do While Not clsChainList Is Nothing
                Call clsChainList.Element.Terminate
                Set clsChainList = clsChainList.NextList
            Loop
            Call m_clsRepresentPointHead.RemoveAll
            Set m_clsRepresentPointHead = Nothing
    
            Call m_clsIsolatePointHead.RemoveAll
            Set m_clsIsolatePointHead = Nothing
    
            Set m_clsObservationShared = Nothing
            Set m_clsDispersion = Nothing
    
            Exit Sub
    
        ErrorHandler:
            Call mdlMain.ErrorExit
    
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'終了。
        private void Class_Terminate()
        {
            try
            {
                ChainList clsChainList = m_clsBaseLineVectorHead.NextList();
                while (clsChainList != null)
                {
#if false
                    /*
                     *************************** 修正要 sakai
                     */
                    clsChainList.Element.Terminate();
#endif
                    clsChainList = clsChainList.NextList();
                }
                m_clsBaseLineVectorHead.RemoveAll();
#if false
                /*
                 *************************** 修正要 sakai
                 */
                m_clsBaseLineVectorHead = null;
#endif
                clsChainList = m_clsRepresentPointHead.NextList();
                while (clsChainList != null)
                {
#if false
                    /*
                     *************************** 修正要 sakai
                     */
                    clsChainList.Element.Terminate();
#endif
                    clsChainList = clsChainList.NextList();
                }
                m_clsRepresentPointHead.RemoveAll();
                m_clsRepresentPointHead = null;


                m_clsIsolatePointHead.RemoveAll();
                m_clsIsolatePointHead = null;


                m_clsObservationShared = null;
                m_clsDispersion = null;

                return;
            }


            catch
            {
                MdlMain mdlMain = new MdlMain();
                mdlMain.ErrorExit();
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
        'ObservationPoint の WorkKey を使用する。
        '
        '引き数：
        'nFile ファイル番号。
        Public Sub Save(ByVal nFile As Integer)

            Call m_clsDispersion.Save(nFile)
    
            '汎用作業キーを結合キーとして使用する。
            Call ClearWorkKey(-1)
    
            '結合キー。各ObservationPointの結合部分に番号付けをする。
            Dim nJointKey As Long
            nJointKey = 0
    
            '基線ベクトル保存。
            Put #nFile, , m_clsBaseLineVectorHead.FollowingCount
            Dim clsChainList As ChainList
            Set clsChainList = m_clsBaseLineVectorHead.NextList
            Do While Not clsChainList Is Nothing
                Call clsChainList.Element.Save(nFile, nJointKey)
                Set clsChainList = clsChainList.NextList
            Loop
    
            '孤立観測点保存。
            Put #nFile, , m_clsIsolatePointHead.FollowingCount
            Set clsChainList = m_clsIsolatePointHead.NextList
            Do While Not clsChainList Is Nothing
                Call clsChainList.Element.Save(nFile, nJointKey)
                Set clsChainList = clsChainList.NextList
            Loop
    
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '*******************************************************************************
        'メソッド
        
        '保存。
        '
        'ObservationPoint の WorkKey を使用する。
        '
        '引き数：
        'nFile ファイル番号。
        */
        public void Save(int nFile)
        {
#if false
            /*
             *************************** 修正要 sakai
             */
            m_clsDispersion.Save(nFile);


            //'汎用作業キーを結合キーとして使用する。
            ClearWorkKey(-1);

            //'結合キー。各ObservationPointの結合部分に番号付けをする。
            long nJointKey;
            nJointKey = 0;


            //'基線ベクトル保存。
            put #nFile, , m_clsBaseLineVectorHead.FollowingCount;
            ChainList clsChainList;
            clsChainList = m_clsBaseLineVectorHead.NextList();
            while (clsChainList != null)
            {
                clsChainList.Element.Save(nFile, nJointKey);
                clsChainList = clsChainList.NextList();
            }


            //'孤立観測点保存。
            put #nFile, , m_clsIsolatePointHead.FollowingCount;
            clsChainList = m_clsIsolatePointHead.NextList();
            while (clsChainList != null)
            {
                clsChainList.Element.Save(nFile, nJointKey);
                clsChainList = clsChainList.NextList();
            }
#endif

            return;
        }
        //==========================================================================================








        //**************************************************************************************
        //**************************************************************************************
        /// <summary>
        /// 読み込み
        /// 引き数：
        ///     'nFile ファイル番号。
        ///     'nVersion ファイルバージョン。
        /// </summary>
        /// <param name="sPath"></param>
        /// <param name="nVersion"></param>
        /// <returns>
        /// </returns>
        //**************************************************************************************
        //        public void Load(BinaryReader br, long nVersion, ref GENBA_STRUCT_S Genba_S)  //23/12/20 K.Setoguchi
        public void Load(BinaryReader br, long nVersion, ref List<GENBA_STRUCT_S> List_Genba_S) //23/12/20 K.Setoguchi
        {
            //-------------------------------------------------------------------------
            GENBA_STRUCT_S Genba_S = new GENBA_STRUCT_S();

             //23/12/20 K.setoguchi@NV---------->>>>>>>>>>>
            GENBA_STRUCT_S Genba_S_Init = new GENBA_STRUCT_S();
            Genba_S = Genba_S_Init;         //構造体の初期化
            //(del)  List_Genba_S.Add(Genba_S);
            //<<<<<<<<<-----------23/12/20 K.setoguchi@NV
            //-------------------------------------------------------------------------

            Dispersion dispersion = new Dispersion();
            BaseLineVector baseLineVector = new BaseLineVector();


            //[VB]------------------------------------------------------------
            //[VB]      Call m_clsDispersion.Load(nFile, nVersion)
            dispersion.Load( br, ref Genba_S);

             //23/12/20 K.setoguchi@NV---------->>>>>>>>>>>
            //-----------------------------------------------------
            // 再配置<-読み込みデータ情報
            //-----------------------------------------------------
            List_Genba_S.Add(Genba_S);
            //<<<<<<<<<-----------23/12/20 K.setoguchi@NV

            //[VB]------------------------------------------------------------
            //[VB]      Dim clsBaseLineVector As BaseLineVector
            //[VB]      Dim clsObservationPoint As ObservationPoint
            //[VB]      Dim clsObservationPoints() As ObservationPoint
            //[VB]      Dim clsChainList As ChainList
            //[VB]      Dim i As Long
            //[VB]      ReDim clsObservationPoints(0)
            //[VB]      Dim nCount As Long
            //[VB]      
            //[VB]      '基線ベクトル読み込み。
            //[VB]      Set clsChainList = m_clsBaseLineVectorHead
            //[VB]      Get #nFile, , nCount
            //[VB]      For i = 0 To nCount - 1
            //[VB]          Set clsBaseLineVector = New BaseLineVector
            //[VB]          Call clsBaseLineVector.Load(nFile, nVersion, clsObservationPoints)
            //[VB]          Set clsChainList = clsChainList.InsertNext(clsBaseLineVector)
            //[VB]      Next
            long nCount;
            nCount = (long)br.ReadInt32();
            for (int i = 0; i < nCount; i++)            //87
            {
                 //23/12/20 K.setoguchi@NV---------->>>>>>>>>>>
                //  //-------------------------------------------------------------------------
                //  List_Genba_S.Add(Genba_S);
                //  //-------------------------------------------------------------------------
                //<<<<<<<<<-----------23/12/20 K.setoguchi@NV

                //------------------------------------------------------------------------------
                //[VB]          Set clsBaseLineVector = New BaseLineVector
                //[VB]          Call clsBaseLineVector.Load(nFile, nVersion, clsObservationPoints)
                //[VB]          Set clsChainList = clsChainList.InsertNext(clsBaseLineVector)
                baseLineVector.Load(br, nVersion, ref Genba_S);

                 //23/12/20 K.setoguchi@NV---------->>>>>>>>>>>
                //-----------------------------------------------------
                // 再配置<-読み込みデータ情報
                //-----------------------------------------------------
                List_Genba_S.Add(Genba_S);

                Genba_S = Genba_S_Init;         //構造体の初期化
                //<<<<<<<<<-----------23/12/20 K.setoguchi@NV

            }

            //------------------------------------------------------------------------------
            //[VB]      If nVersion< 1200 Then
            //[VB]      '始点番号、終点番号、セッション名でユニークにする。
            if (nVersion < 1200)
            {
                //＜＜検討＞＞＞
            }

            //-------------------------------------------------------------------------------------------
            //[VB]      '孤立観測点読み込み。
            //[VB]      Set clsChainList = m_clsIsolatePointHead
            //[VB]      Get #nFile, , nCount
            //[VB]      For i = 0 To nCount - 1
            //[VB]          Set clsObservationPoint = New ObservationPoint
            //[VB]          Call clsObservationPoint.Load(nFile, nVersion, clsObservationPoints, Nothing)
            //[VB]          Set clsChainList = clsChainList.InsertNext(clsObservationPoint)
            //[VB]      Next
            nCount = (long)br.ReadInt32();              //0


        }
        //-------------------------------------------------------------------------------
        //[VB]  '読み込み。
        //[VB]  '
        //[VB]  'ObservationPoint の WorkKey を使用する。
        //[VB]  '
        //[VB]  '引き数：
        //[VB]  'nFile ファイル番号。
        //[VB]  'nVersion ファイルバージョン。
        //[VB]  Public Sub Load(ByVal nFile As Integer, ByVal nVersion As Long)
        //[VB]  
        //[VB]      Call m_clsDispersion.Load(nFile, nVersion)
        //[VB]      
        //[VB]      Dim clsBaseLineVector As BaseLineVector
        //[VB]      Dim clsObservationPoint As ObservationPoint
        //[VB]      Dim clsObservationPoints() As ObservationPoint
        //[VB]      Dim clsChainList As ChainList
        //[VB]      Dim i As Long
        //[VB]      ReDim clsObservationPoints(0)
        //[VB]      Dim nCount As Long
        //[VB]      
        //[VB]      '基線ベクトル読み込み。
        //[VB]      Set clsChainList = m_clsBaseLineVectorHead
        //[VB]      Get #nFile, , nCount
        //[VB]      For i = 0 To nCount - 1
        //[VB]          Set clsBaseLineVector = New BaseLineVector
        //[VB]          Call clsBaseLineVector.Load(nFile, nVersion, clsObservationPoints)
        //[VB]          Set clsChainList = clsChainList.InsertNext(clsBaseLineVector)
        //[VB]      Next
        //[VB]      
        //[VB]      
        //[VB]      If nVersion< 1200 Then
        //[VB]          '始点番号、終点番号、セッション名でユニークにする。
        //[VB]          Dim objCollection As New Collection
        //[VB]          Dim objObject As Object
        //[VB]          Set clsChainList = m_clsBaseLineVectorHead.NextList
        //[VB]          Do While Not clsChainList Is Nothing
        //[VB]              Set clsBaseLineVector = clsChainList.Element
        //[VB]              Dim sKey As String
        //[VB]              sKey = clsBaseLineVector.Key
        //[VB]              Call SetAtCollectionObject(objCollection, clsBaseLineVector, sKey)
        //[VB]              Set clsChainList = clsChainList.NextList
        //[VB]          Loop
        //[VB]          Set clsChainList = m_clsBaseLineVectorHead.NextList
        //[VB]          Do While Not clsChainList Is Nothing
        //[VB]              Set clsBaseLineVector = clsChainList.Element
        //[VB]              sKey = clsBaseLineVector.Key
        //[VB]              If LookupCollectionObject(objCollection, objObject, sKey) Then
        //[VB]                  If objObject Is Nothing Then
        //[VB]                      For i = 0 To 9999
        //[VB]                          clsBaseLineVector.Session = Format$(i, "0000")
        //[VB]                          sKey = clsBaseLineVector.Key
        //[VB]                      If Not LookupCollectionObject(objCollection, objObject, sKey) Then
        //[VB]                          Call objCollection.Add(Nothing, sKey)
        //[VB]                          Exit For
        //[VB]                      End If
        //[VB]                  Next
        //[VB]                  If i > 9999 Then Call Err.Raise(ERR_FATAL, , "この現場は古いバージョンのアプリケーションで生成されています。基線ベクトルが多すぎるため、新しいバージョンへ変換できませんでした。")
        //[VB]                  Else
        //[VB]                      Call objCollection.Remove(sKey)
        //[VB]                      Call objCollection.Add(Nothing, sKey)
        //[VB]                  End If
        //[VB]              End If
        //[VB]              Set clsChainList = clsChainList.NextList
        //[VB]          Loop
        //[VB]      End If
        //[VB]      
        //[VB]      '孤立観測点読み込み。
        //[VB]      Set clsChainList = m_clsIsolatePointHead
        //[VB]      Get #nFile, , nCount
        //[VB]      For i = 0 To nCount - 1
        //[VB]          Set clsObservationPoint = New ObservationPoint
        //[VB]          Call clsObservationPoint.Load(nFile, nVersion, clsObservationPoints, Nothing)
        //[VB]          Set clsChainList = clsChainList.InsertNext(clsObservationPoint)
        //[VB]      Next
        //[VB]      
        //[VB]      '代表観測点リストを作成する。
        //[VB]      Call ClearWorkKey(0) '汎用作業キーをフラグとして使用する。
        //[VB]      Dim clsRepresentTail As ChainList
        //[VB]      Set clsRepresentTail = m_clsRepresentPointHead.TailList
        //[VB]      '基線ベクトルリストから代表観測点リストに追加。
        //[VB]      Set clsChainList = m_clsBaseLineVectorHead.NextList
        //[VB]      Do While Not clsChainList Is Nothing
        //[VB]          Set clsBaseLineVector = clsChainList.Element
        //[VB]          'フラグが0なら代表観測点として加える。
        //[VB]          Set clsObservationPoint = clsBaseLineVector.StrPoint.TopParentPoint
        //[VB]          If clsObservationPoint.WorkKey = 0 Then
        //[VB]              Set clsRepresentTail = InsertRepresentPoint(clsRepresentTail, clsObservationPoint)
        //[VB]              clsObservationPoint.WorkKey = 1
        //[VB]          End If
        //[VB]          Set clsObservationPoint = clsBaseLineVector.EndPoint.TopParentPoint
        //[VB]          If clsObservationPoint.WorkKey = 0 Then
        //[VB]              Set clsRepresentTail = InsertRepresentPoint(clsRepresentTail, clsObservationPoint)
        //[VB]              clsObservationPoint.WorkKey = 1
        //[VB]          End If
        //[VB]          Set clsChainList = clsChainList.NextList
        //[VB]      Loop
        //[VB]      
        //[VB]      '孤立観測点でない筆頭観測点のコレクション。
        //[VB]      Dim objHeadPoints As New Collection
        //[VB]      Set clsChainList = m_clsRepresentPointHead.NextList
        //[VB]      Do While Not clsChainList Is Nothing
        //[VB]      Set clsObservationPoint = clsChainList.Element
        //[VB]      Call SetAtCollectionObject(objHeadPoints, clsObservationPoint.HeadPoint, clsObservationPoint.Number)
        //[VB]      Set clsChainList = clsChainList.NextList
        //[VB]      Loop
        //[VB]      
        //[VB]      '孤立観測点リストから代表観測点リストに追加。
        //[VB]      Set clsChainList = m_clsIsolatePointHead.NextList
        //[VB]      Do While Not clsChainList Is Nothing
        //[VB]          Set clsObservationPoint = clsChainList.Element
        //[VB]          Set clsRepresentTail = InsertRepresentPoint(clsRepresentTail, clsObservationPoint.TopParentPoint)
        //[VB]          Call SetAtCollectionObject(objHeadPoints, clsObservationPoint.HeadPoint, clsObservationPoint.Number)
        //[VB]          Set clsChainList = clsChainList.NextList
        //[VB]      Loop
        //[VB]      
        //[VB]      '属性の共有。
        //[VB]      Set clsChainList = m_clsRepresentPointHead.NextList
        //[VB]      Do While Not clsChainList Is Nothing
        //[VB]          Set clsObservationPoint = clsChainList.Element
        //[VB]          If clsObservationPoint.PrevPoint Is Nothing Then Call ShareAttributesNext(clsObservationPoint, clsObservationPoint.Attributes.Common)
        //[VB]              Set clsChainList = clsChainList.NextList
        //[VB]      Loop
        //[VB]      
        //[VB]      '補正点を接続する。
        //[VB]      Set clsChainList = m_clsIsolatePointHead.NextList
        //[VB]      Do While Not clsChainList Is Nothing
        //[VB]          Set clsObservationPoint = clsChainList.Element
        //[VB]          If clsObservationPoint.Genuine Then
        //[VB]              Dim clsEccentricPoint As ObservationPoint
        //[VB]              If LookupCollectionObject(objHeadPoints, clsEccentricPoint, Mid(clsObservationPoint.Number, Len(GENUINE_POINT_SESSION) + 1)) Then
        //[VB]                  Call clsObservationPoint.UpdateCorrectPoint(clsEccentricPoint)
        //[VB]                  Call clsEccentricPoint.UpdateCorrectPoint(clsObservationPoint)
        //[VB]              End If
        //[VB]          End If
        //[VB]          Set clsChainList = clsChainList.NextList
        //[VB]      Loop
        //[VB]      
        //[VB]      
        //[VB] End Sub
        //***************************************************************************
        //***************************************************************************

        //24/01/04 K.setoguchi@NV---------->>>>>>>>>>
        //***************************************************************************
        /// <summary>
        /// '汎用作業キーを初期化する。
        /// '
        /// 'nWorkKey で指定される値で WorkKey を初期化する。
        /// '観測点、基線ベクトルの WorkKey を初期化する。
        /// '
        /// '引き数：
        /// 'nWorkKey 初期化する値。
        /// 
        /// </summary>
        /// <param name="nWorkKey"></param>
        public void ClearWorkKey(long nWorkKey)
        {
            ChainList clsChainList;

            clsChainList = m_clsBaseLineVectorHead.NextList();
            for (; ; )
            {
                if(clsChainList == null)
                {
                    break;
                }
                //瀬戸口　clsChainList.Element.ClearWorkKey(nWorkKey);

                clsChainList = clsChainList.NextList();
            }


            clsChainList = m_clsIsolatePointHead.NextList();
            for (; ; )
            {
                if (clsChainList == null)
                {
                    break;
                }
                //瀬戸口　clsChainList.Element.ClearWorkKey(nWorkKey);    

                clsChainList = clsChainList.NextList();
            }

        }
        //----------------------------------------------
        //'汎用作業キーを初期化する。
        //'
        //'nWorkKey で指定される値で WorkKey を初期化する。
        //'観測点、基線ベクトルの WorkKey を初期化する。
        //'
        //'引き数：
        //'nWorkKey 初期化する値。
        //Public Sub ClearWorkKey(ByVal nWorkKey As Long)
        //    Dim clsChainList As ChainList
        //    Set clsChainList = m_clsBaseLineVectorHead.NextList
        //    Do While Not clsChainList Is Nothing
        //        clsChainList.Element.WorkKey = nWorkKey
        //        Call clsChainList.Element.ClearWorkKey(nWorkKey)
        //        Set clsChainList = clsChainList.NextList
        //    Loop
        //    Set clsChainList = m_clsIsolatePointHead.NextList
        //    Do While Not clsChainList Is Nothing
        //        Call clsChainList.Element.ClearWorkKey(nWorkKey)
        //        Set clsChainList = clsChainList.NextList
        //    Loop
        //End Sub

        //24/01/04 K.setoguchi@NV---------->>>>>>>>>>
        //***************************************************************************
        public void Save(BinaryWriter bw)
        {
            //---------------------------------------------------
            //    Call m_clsDispersion.Save(nFile)
            Dispersion m_clsDispersion = new Dispersion();
            m_clsDispersion.Save(bw);


            //---------------------------------------------------
            //    '汎用作業キーを結合キーとして使用する。
            //    Call ClearWorkKey(-1)
            ClearWorkKey(-1);

            //---------------------------------------------------
            //    '結合キー。各ObservationPointの結合部分に番号付けをする。
            //    Dim nJointKey As Long
            //    nJointKey = 0
            long nJointKey = 0;


            //---------------------------------------------------
            //    '基線ベクトル保存。
            //    Put #nFile, , m_clsBaseLineVectorHead.FollowingCount
            //    Dim clsChainList As ChainList
            //    Set clsChainList = m_clsBaseLineVectorHead.NextList
            //    Do While Not clsChainList Is Nothing
            //        Call clsChainList.Element.Save(nFile, nJointKey)
            //        Set clsChainList = clsChainList.NextList
            //    Loop

#if false
            bw.Write(m_clsBaseLineVectorHead.FollowingCount);
#else
            long FollowingCount = 0;
            bw.Write(FollowingCount);
#endif

            //---------------------------------------------------
            //'孤立観測点保存。
            //Put #nFile, , m_clsIsolatePointHead.FollowingCount
            //Set clsChainList = m_clsIsolatePointHead.NextList
            //Do While Not clsChainList Is Nothing
            //    Call clsChainList.Element.Save(nFile, nJointKey)
            //    Set clsChainList = clsChainList.NextList
            //Loop

#if false
            bw.Write(m_clsIsolatePointHead.FollowingCount);
#else
            FollowingCount = 0;
            bw.Write(FollowingCount);
#endif

        }
        //--------------------------------------------------------------------
        //'メソッド
        //
        //'保存。
        //'
        //'ObservationPoint の WorkKey を使用する。
        //'
        //'引き数：
        //'nFile ファイル番号。
        //Public Sub Save(ByVal nFile As Integer)
        //
        //    Call m_clsDispersion.Save(nFile)
        //    
        //    '汎用作業キーを結合キーとして使用する。
        //    Call ClearWorkKey(-1)
        //    
        //    '結合キー。各ObservationPointの結合部分に番号付けをする。
        //    Dim nJointKey As Long
        //    nJointKey = 0
        //    
        //    '基線ベクトル保存。
        //    Put #nFile, , m_clsBaseLineVectorHead.FollowingCount
        //    Dim clsChainList As ChainList
        //    Set clsChainList = m_clsBaseLineVectorHead.NextList
        //    Do While Not clsChainList Is Nothing
        //        Call clsChainList.Element.Save(nFile, nJointKey)
        //        Set clsChainList = clsChainList.NextList
        //    Loop
        //    
        //    '孤立観測点保存。
        //    Put #nFile, , m_clsIsolatePointHead.FollowingCount
        //    Set clsChainList = m_clsIsolatePointHead.NextList
        //    Do While Not clsChainList Is Nothing
        //        Call clsChainList.Element.Save(nFile, nJointKey)
        //        Set clsChainList = clsChainList.NextList
        //    Loop
        //
        //
        //End Sub
        //***************************************************************************
        //<<<<<<<<<-----------24/01/04 K.setoguchi@NV

    }
}
