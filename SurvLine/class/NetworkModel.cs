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
using static SurvLine.mdl.DEFINE;
using static SurvLine.mdl.MdlNSDefine;
using static SurvLine.mdl.MdlUtility;
using static SurvLine.mdl.MdlAccountMake;
using static SurvLine.mdl.MdlNSGUI;
using static SurvLine.mdl.MdlNSSDefine;
using static SurvLine.mdl.MdiVBfunctions;
using static SurvLine.mdl.MdlMain;
using static SurvLine.mdl.MdlGUI;
using static SurvLine.mdl.MdlNSUtility;
using static SurvLine.mdl.MdlRINEXTYPE;
using System.Collections.ObjectModel;
using System.Collections;
using System.Runtime.InteropServices;
using SurvLine.mdl;
using Microsoft.VisualBasic;
//using static System.Net.Mime.MediaTypeNames;
using System.Globalization;
using System.Drawing;
using System.Security.Cryptography;
using static System.Net.Mime.MediaTypeNames;
using System.Net;
using System.Diagnostics.Eventing.Reader;
using static SurvLine.mdl.MdlBaseLineAnalyser;
using System.Linq;
using System.Reflection;

namespace SurvLine
{
    public class NetworkModel
    {
        private MdlMain m_clsMdlMain;   //3
        public NetworkModel(MdlMain mdlMain)
        {
            this.mdlMain = mdlMain;

            m_clsMdlMain = mdlMain;     //3
        }

        /*
        //'*******************************************************************************
        //'*******************************************************************************
        private void Class_Initialize()
        {

        }
        //-------------------------------------------------------------------------------
        */

        //==========================================================================================
        /*[VB]
        'イベント

        '結合距離違反イベント。
        '
        '結合する観測点間の距離が制限を超えている場合に発生する。
        '
        '引き数：
        'clsObservationPoint 違反の対象となる観測点。
        'nDistance 観測点間の距離(ｍ)。
        'nMax 距離の制限値(㎝)。
        'nStyle メッセージスタイル。0=メッセージ無し。1=OKボタンのみ。2=OKボタンとすべてOKボタン。
        'nResult メッセージボックスの結果が設定される。
        Event CombinedDistanceViolation(ByVal clsObservationPoint As ObservationPoint, ByVal nDistance As Double, ByVal nMax As Long, ByVal nStyle As Long, ByRef nResult As Long)

        '上書き通知イベント。
        '
        '同じ観測点番号、セッション名の観測点をインポートしたときに、上書きを確認するために発生する。
        '
        '引き数：
        'clsObservationPoint 対象となる観測点。
        'nStyle メッセージスタイル。0=メッセージ無し。1=OKボタンのみ。2=OKボタンとすべてOKボタン。
        'nResult メッセージボックスの結果が設定される。
        Event OverwriteNotification(ByVal clsObservationPoint As ObservationPoint, ByVal nStyle As Long, ByRef nResult As Long) '上書き通知。

        'インプリメンテーション
        Private m_clsBaseLineVectorHead As New ChainList '基線ベクトルリストのヘッダー。要素は BaseLineVector オブジェクト。
        Private m_clsRepresentPointHead As New ChainList '代表観測点リストのヘッダー。要素は ObservationPoint オブジェクト(代表観測点)。
        Private m_clsIsolatePointHead As New ChainList '孤立観測点リストのヘッダー。要素は ObservationPoint オブジェクト(実観測点)。
        Private m_clsObservationShared As New ObservationShared '観測共有情報。
        Private m_clsDispersion As New Dispersion '分散・共分散(固定重量)。
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'インプリメンテーション
        private readonly ChainList m_clsBaseLineVectorHead = new ChainList();           //'基線ベクトルリストのヘッダー。要素は BaseLineVector オブジェクト。
        private ChainList m_clsRepresentPointHead = new ChainList();                    //'代表観測点リストのヘッダー。要素は ObservationPoint オブジェクト(代表観測点)。
        private ChainList m_clsIsolatePointHead = new ChainList();                      //'孤立観測点リストのヘッダー。要素は ObservationPoint オブジェクト(実観測点)。
        private ObservationShared m_clsObservationShared = new ObservationShared();     //'観測共有情報。
        private Dispersion m_clsDispersion = new Dispersion();                          //'分散・共分散(固定重量)。

        private MdlMain mdlMain;
        private object objObject;
        private List<OPA_STRUCT_SUB> OPA_List;
        private ObservationPoint clsEccentricPoint;
        //private ObservationPoint[] clsObservationPoints;

        //==========================================================================================

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
#if false
        /*
        *************************** 呼ばれていません！ sakai
        */
        public long HeadRepresentPointCount()
        {
            long w_HeadRepresentPointCount = 0;
            ChainList clsChainList;
            clsChainList = m_clsRepresentPointHead.NextList();
            while (clsChainList != null)
            {
                if (clsChainList.Element.PrevPoint() == null)
                {
                    w_HeadRepresentPointCount = w_HeadRepresentPointCount + 1;
                }
            clsChainList = clsChainList.NextList();
            }
            return w_HeadRepresentPointCount;
        }
#endif
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
#if false
        /*
        *************************** 呼ばれていません！ sakai
        */
        public long EnableFixedCount()
        {
            ChainList clsChainList;
            clsChainList = m_clsRepresentPointHead.NextList();
            long w_EnableFixedCount = 0;
            while (clsChainList != null)
            {
                if (clsChainList.Element.Fixed && clsChainList.Element.Enable)
                {
                    w_EnableFixedCount = w_EnableFixedCount + 1;
                }
                clsChainList = clsChainList.NextList();
            }
            return w_EnableFixedCount;
        }
#endif
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
#if false
        /*
        *************************** 呼ばれていません！ sakai
        */
        public long EnablePointCount()
        {
            ChainList clsChainList;
            clsChainList = m_clsRepresentPointHead.NextList();
            long w_EnablePointCount = 0;
            while (clsChainList != null)
            {
                if (clsChainList.Element.Enable)
                {
                    w_EnablePointCount = w_EnablePointCount + 1;
                }
                clsChainList = clsChainList.NextList();
            }
            return w_EnablePointCount;
        }
#endif
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
#if false
        /*
        *************************** 呼ばれていません！ sakai
        */
        public long EnableVectorCount()
        {
            ChainList clsChainList;
            clsChainList = m_clsBaseLineVectorHead.NextList();
            long w_EnableVectorCount = 0;
            while (clsChainList != null)
            {
                if (clsChainList.Element.Enable)
                {
                    w_EnableVectorCount = w_EnableVectorCount + 1;
                }
                clsChainList = clsChainList.NextList();
            }
            return w_EnableVectorCount;
        }
#endif
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
        public ChainList BaseLineVectorHead()
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
        public ChainList RepresentPointHead()
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
        public ChainList IsolatePointHead()
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
        public ObservationShared ObservationShared()
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
                //  string AppPath = Path.GetDirectoryName(Application.ExecutablePath);
#if true
                /*
                 ****************** 修正要 sakai
                */
                string AppTitle = "SurvLine";
#endif
                //  return GetPrivateProfileInt(PROFILE_SAVE_SEC_BASELINE, PROFILE_SAVE_KEY_DEFLEAPSEC, (int)DEF_LEAP_SEC, AppPath + "\\" + AppTitle + PROFILE_SAVE_EXT);
                return 0;
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
        public void Save(BinaryWriter bw)       //0304 //2
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









        //**************************************************************************************
        //==========================================================================================
        //sakai
        /*[VB]
        '読み込み。
        '
        'ObservationPoint の WorkKey を使用する。
        '
        '引き数：
        'nFile ファイル番号。
        'nVersion ファイルバージョン。
        Public Sub Load(ByVal nFile As Integer, ByVal nVersion As Long)
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
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
            //------------------------------------------------------------------------------------------
            /*[VB]
            Call m_clsDispersion.Load(nFile, nVersion)
    
            Dim clsBaseLineVector As BaseLineVector
            Dim clsObservationPoint As ObservationPoint
            Dim clsObservationPoints() As ObservationPoint
            Dim clsChainList As ChainList
            Dim i As Long
            ReDim clsObservationPoints(0)
            Dim nCount As Long
    
            '基線ベクトル読み込み。
            Set clsChainList = m_clsBaseLineVectorHead
            Get #nFile, , nCount
            For i = 0 To nCount - 1
                Set clsBaseLineVector = New BaseLineVector
                Call clsBaseLineVector.Load(nFile, nVersion, clsObservationPoints)
                Set clsChainList = clsChainList.InsertNext(clsBaseLineVector)
            Next
            [VB]*/
            //------------------------------------------------------------------------------------------
            //[C#]
            BaseLineVector clsBaseLineVector;
            ObservationPoint clsObservationPoint;
            ObservationPoint[] clsObservationPoints = new ObservationPoint[0];
            ChainList clsChainList;

            _ = new GENBA_STRUCT_S();

            GENBA_STRUCT_S Genba_S_Init = new GENBA_STRUCT_S();
            GENBA_STRUCT_S Genba_S = Genba_S_Init;

            Dispersion dispersion = new Dispersion();
            //BaseLineVector baseLineVector = new BaseLineVector();

            dispersion.Load(br, ref Genba_S);

            //-----------------------------------------------------
            // 再配置<-読み込みデータ情報
            //-----------------------------------------------------
            List_Genba_S.Add(Genba_S);

            //'基線ベクトル読み込み。
            clsChainList = m_clsBaseLineVectorHead;
            long nCount;
            nCount = br.ReadInt32();
            for (int i = 0; i < nCount; i++)            //87
            {
                BaseLineVector baseLineVector = new BaseLineVector(mdlMain);
                //baseLineVector.Load(br, nVersion, ref Genba_S);
                baseLineVector.Load(br, nVersion, ref Genba_S, ref clsObservationPoints);
                clsChainList = clsChainList.InsertNext(baseLineVector);

                //-----------------------------------------------------
                // 再配置<-読み込みデータ情報
                //-----------------------------------------------------
                List_Genba_S.Add(Genba_S);

                Genba_S = Genba_S_Init;         //構造体の初期化

            }
            //------------------------------------------------------------------------------------------

            //------------------------------------------------------------------------------------------
            /*[VB]
            If nVersion < 1200 Then
                '始点番号、終点番号、セッション名でユニークにする。
                Dim objCollection As New Collection
                Dim objObject As Object
                Set clsChainList = m_clsBaseLineVectorHead.NextList
                Do While Not clsChainList Is Nothing
                    Set clsBaseLineVector = clsChainList.Element
                    Dim sKey As String
                    sKey = clsBaseLineVector.Key
                    Call SetAtCollectionObject(objCollection, clsBaseLineVector, sKey)
                    Set clsChainList = clsChainList.NextList
                Loop
                Set clsChainList = m_clsBaseLineVectorHead.NextList
                Do While Not clsChainList Is Nothing
                    Set clsBaseLineVector = clsChainList.Element
                    sKey = clsBaseLineVector.Key
                    If LookupCollectionObject(objCollection, objObject, sKey) Then
                        If objObject Is Nothing Then
                            For i = 0 To 9999
                                clsBaseLineVector.Session = Format$(i, "0000")
                                sKey = clsBaseLineVector.Key
                                If Not LookupCollectionObject(objCollection, objObject, sKey) Then
                                    Call objCollection.Add(Nothing, sKey)
                                    Exit For
                                End If
                            Next
                            If i > 9999 Then Call Err.Raise(ERR_FATAL, , "この現場は古いバージョンのアプリケーションで生成されています。基線ベクトルが多すぎるため、新しいバージョンへ変換できませんでした。")
                        Else
                            Call objCollection.Remove(sKey)
                            Call objCollection.Add(Nothing, sKey)
                        End If
                    End If
                    Set clsChainList = clsChainList.NextList
                Loop
            End If
            [VB]*/
            //------------------------------------------------------------------------------------------
            //[C#]
            if (nVersion < 1200)
            {
                //'始点番号、終点番号、セッション名でユニークにする。
                Dictionary<string, object> objCollection = new Dictionary<string, object>();
                string sKey;
                //object objObject;
                clsChainList = m_clsBaseLineVectorHead.NextList();
                while (clsChainList != null)
                {
                    clsBaseLineVector = (BaseLineVector)clsChainList.Element;
                    sKey = clsBaseLineVector.Key();
                    SetAtCollectionObject(objCollection, clsBaseLineVector, sKey);
                    clsChainList = clsChainList.NextList();
                }
                clsChainList = m_clsBaseLineVectorHead.NextList();
                while (clsChainList != null)
                {
                    clsBaseLineVector = (BaseLineVector)clsChainList.Element;
                    sKey = clsBaseLineVector.Key();
                    if (LookupCollectionObject(objCollection, ref objObject, sKey))
                    {
                        if (objObject == null)
                        {
                            int i;
                            for (i = 0; i < 9999; i++)
                            {
                                clsBaseLineVector.Session = Strings.Format(i, "0000");
                                sKey = clsBaseLineVector.Key();
                                if (!LookupCollectionObject(objCollection, ref objObject, sKey))
                                {
                                    objCollection.Add(sKey, null);
                                    break;
                                }
                            }
                            if (i > 9999)
                            {
                                //Err.Raise(ERR_FATAL, , "この現場は古いバージョンのアプリケーションで生成されています。基線ベクトルが多すぎるため、新しいバージョンへ変換できませんでした。");
                            }
                        }
                        else
                        {
                            objCollection.Remove(sKey);
                            objCollection.Add(sKey, null);
                        }
                    }
                    clsChainList = clsChainList.NextList();
                }
            }
            //------------------------------------------------------------------------------------------

            //------------------------------------------------------------------------------------------
            /*[VB]
            '孤立観測点読み込み。
            Set clsChainList = m_clsIsolatePointHead
            Get #nFile, , nCount
            For i = 0 To nCount - 1
                Set clsObservationPoint = New ObservationPoint
                Call clsObservationPoint.Load(nFile, nVersion, clsObservationPoints, Nothing)
                Set clsChainList = clsChainList.InsertNext(clsObservationPoint)
            Next
            [VB]*/
            //------------------------------------------------------------------------------------------
            //[C#]
            //'孤立観測点読み込み。
            clsChainList = m_clsIsolatePointHead;
            nCount = br.ReadInt32();
            for (int i = 0; i < nCount; i++)            //87
            {
                clsObservationPoint = new ObservationPoint(mdlMain);
                //clsObservationPoint.Load(br, nVersion, ref Genba_S, ref OPA_List);
                clsObservationPoint.Load(br, nVersion, ref Genba_S, ref OPA_List, ref clsObservationPoints, null);
                clsChainList = clsChainList.InsertNext(clsObservationPoint);
            }
            //------------------------------------------------------------------------------------------

            //------------------------------------------------------------------------------------------
            /*[VB]
            '代表観測点リストを作成する。
            Call ClearWorkKey(0) '汎用作業キーをフラグとして使用する。
            Dim clsRepresentTail As ChainList
            Set clsRepresentTail = m_clsRepresentPointHead.TailList
            '基線ベクトルリストから代表観測点リストに追加。
            Set clsChainList = m_clsBaseLineVectorHead.NextList
            Do While Not clsChainList Is Nothing
                Set clsBaseLineVector = clsChainList.Element
                'フラグが0なら代表観測点として加える。
                Set clsObservationPoint = clsBaseLineVector.StrPoint.TopParentPoint
                If clsObservationPoint.WorkKey = 0 Then
                    Set clsRepresentTail = InsertRepresentPoint(clsRepresentTail, clsObservationPoint)
                    clsObservationPoint.WorkKey = 1
                End If
                Set clsObservationPoint = clsBaseLineVector.EndPoint.TopParentPoint
                If clsObservationPoint.WorkKey = 0 Then
                    Set clsRepresentTail = InsertRepresentPoint(clsRepresentTail, clsObservationPoint)
                    clsObservationPoint.WorkKey = 1
                End If
                Set clsChainList = clsChainList.NextList
            Loop
            [VB]*/
            //------------------------------------------------------------------------------------------
            //[C#]
            //'代表観測点リストを作成する。
            ClearWorkKey(0);                //'汎用作業キーをフラグとして使用する。
            ChainList clsRepresentTail;
            clsRepresentTail = m_clsRepresentPointHead.TailList();
            //'基線ベクトルリストから代表観測点リストに追加。
            clsChainList = m_clsBaseLineVectorHead.NextList();
            int i1 = 0;
            while (clsChainList != null)
            {
                clsBaseLineVector = (BaseLineVector)clsChainList.Element;
                //'フラグが0なら代表観測点として加える。
                clsObservationPoint = clsBaseLineVector.StrPoint().TopParentPoint();
                if (clsObservationPoint.WorkKey == 0)   //6
                {
                    clsRepresentTail = InsertRepresentPoint(clsRepresentTail, clsObservationPoint);
                    clsObservationPoint.WorkKey = 1;
                }
                clsObservationPoint = clsBaseLineVector.EndPoint().TopParentPoint();
                if (clsObservationPoint.WorkKey == 0)   //6
                {
                    clsRepresentTail = InsertRepresentPoint(clsRepresentTail, clsObservationPoint);
                    clsObservationPoint.WorkKey = 1;
                }
                clsChainList = clsChainList.NextList();
            }
            //------------------------------------------------------------------------------------------

            //------------------------------------------------------------------------------------------
            /*[VB]
            '孤立観測点でない筆頭観測点のコレクション。
            Dim objHeadPoints As New Collection
            Set clsChainList = m_clsRepresentPointHead.NextList
            Do While Not clsChainList Is Nothing
                Set clsObservationPoint = clsChainList.Element
                Call SetAtCollectionObject(objHeadPoints, clsObservationPoint.HeadPoint, clsObservationPoint.Number)
                Set clsChainList = clsChainList.NextList
            Loop
            [VB]*/
            //------------------------------------------------------------------------------------------
            //[C#]
            //'孤立観測点でない筆頭観測点のコレクション。
            Dictionary<string, object> objHeadPoints = new Dictionary<string, object>();
            ObservationPoint clsHeadPoint = null;
            clsChainList = m_clsRepresentPointHead.NextList();
            while (clsChainList != null)
            {
                clsObservationPoint = (ObservationPoint)clsChainList.Element;
                clsHeadPoint = clsObservationPoint.HeadPoint();
                SetAtCollectionObject(objHeadPoints, clsHeadPoint, clsObservationPoint.Number());
                clsChainList = clsChainList.NextList();
            }
            //------------------------------------------------------------------------------------------

            //------------------------------------------------------------------------------------------
            /*[VB]
            '孤立観測点リストから代表観測点リストに追加。
            Set clsChainList = m_clsIsolatePointHead.NextList
            Do While Not clsChainList Is Nothing
                Set clsObservationPoint = clsChainList.Element
                Set clsRepresentTail = InsertRepresentPoint(clsRepresentTail, clsObservationPoint.TopParentPoint)
                Call SetAtCollectionObject(objHeadPoints, clsObservationPoint.HeadPoint, clsObservationPoint.Number)
                Set clsChainList = clsChainList.NextList
            Loop
            [VB]*/
            //------------------------------------------------------------------------------------------
            //[C#]
            //'孤立観測点リストから代表観測点リストに追加。
            clsChainList = m_clsIsolatePointHead.NextList();
            while (clsChainList != null)
            {
                clsObservationPoint = (ObservationPoint)clsChainList.Element;
#if false
                if (i1 == 0)
                {
                    m_clsObservationShared.Class_Initialize();
                    i1 = 1;
                }
#endif
                clsRepresentTail = InsertRepresentPoint(clsRepresentTail, clsObservationPoint.TopParentPoint());
                SetAtCollectionObject(objHeadPoints, clsObservationPoint.HeadPoint(), clsObservationPoint.Number());
                clsChainList = clsChainList.NextList();
            }
            //------------------------------------------------------------------------------------------

            //------------------------------------------------------------------------------------------
            /*[VB]
            '属性の共有。
            Set clsChainList = m_clsRepresentPointHead.NextList
            Do While Not clsChainList Is Nothing
                Set clsObservationPoint = clsChainList.Element
                If clsObservationPoint.PrevPoint Is Nothing Then Call ShareAttributesNext(clsObservationPoint, clsObservationPoint.Attributes.Common)
                Set clsChainList = clsChainList.NextList
            Loop
            [VB]*/
            //------------------------------------------------------------------------------------------
            //[C#]
            //'属性の共有。
            clsChainList = m_clsRepresentPointHead.NextList();
            while (clsChainList != null)
            {
                clsObservationPoint = (ObservationPoint)clsChainList.Element;
                if (clsObservationPoint.PrevPoint() == null)
                {
                    ShareAttributesNext(clsObservationPoint, clsObservationPoint.Attributes().Common());
                }
                clsChainList = clsChainList.NextList();
            }
            //------------------------------------------------------------------------------------------

            //------------------------------------------------------------------------------------------
            /*[VB]
            '補正点を接続する。
            Set clsChainList = m_clsIsolatePointHead.NextList
            Do While Not clsChainList Is Nothing
                Set clsObservationPoint = clsChainList.Element
                If clsObservationPoint.Genuine Then
                    Dim clsEccentricPoint As ObservationPoint
                    If LookupCollectionObject(objHeadPoints, clsEccentricPoint, Mid(clsObservationPoint.Number, Len(GENUINE_POINT_SESSION) + 1)) Then
                        Call clsObservationPoint.UpdateCorrectPoint(clsEccentricPoint)
                        Call clsEccentricPoint.UpdateCorrectPoint(clsObservationPoint)
                    End If
                End If
                Set clsChainList = clsChainList.NextList
            Loop
            [VB]*/
            //------------------------------------------------------------------------------------------
            //[C#]
            //'補正点を接続する。
            clsChainList = m_clsIsolatePointHead.NextList();
            while (clsChainList != null)
            {
                clsObservationPoint = (ObservationPoint)clsChainList.Element;
                if (clsObservationPoint.Genuine())
                {
                    if (LookupCollectionObject(objHeadPoints, ref objObject, Mid(clsObservationPoint.Number(), 1, GENUINE_POINT_SESSION.Length + 1)))
                    {
                        clsEccentricPoint = (ObservationPoint)objObject;
                        clsObservationPoint.UpdateCorrectPoint(clsEccentricPoint);
                        clsEccentricPoint.UpdateCorrectPoint(clsObservationPoint);
                    }
                }
                clsChainList = clsChainList.NextList();
            }
            //------------------------------------------------------------------------------------------

            //------------------------------------------------------------------------------------------
            /*[VB]
            End Sub
            [VB]*/
            //------------------------------------------------------------------------------------------
            //[C#]
            //==========================================================================================
        }


        //==========================================================================================
        /*[VB]
        '外部ファイルをインポートする。
        '
        '引き数：
        'nImportType インポートファイル種別。
        'sPath インポートするファイルのパス。参照する配列の要素は(0 To ...)。Ubound 関数を使用する。
        'sObsPointPath 観測点フォルダのパス。
        'nSameTimeMin 最小同時観測時間(秒)。
        'nSatelliteCountMin 最少共通衛星数。
        'clsProgressInterface ProgressInterface オブジェクト。
        '
        '戻り値：
        '全て正常にインポートできた場合は True を返す。
        '無効なデータがあった場合は False を返す。
        Public Function Import(ByVal nImportType As IMPORT_TYPE, ByRef sPath() As String, ByVal sObsPointPath As String, ByVal nSameTimeMin As Long, ByVal nSatelliteCountMin As Long, ByVal clsProgressInterface As ProgressInterface) As Boolean

            Import = True

            Select Case nImportType
            Case IMPORT_TYPE_UNKNOWN
                Import = False
                Exit Function
            Case IMPORT_TYPE_JOB
                Import = False
                Exit Function
            Case IMPORT_TYPE_NVF
                Import = False
                Exit Function
            Case IMPORT_TYPE_DAT
            Case IMPORT_TYPE_RINEX
                Dim sPathO() As String
                Dim sPathN() As String
                Dim sPathG() As String
                '2017/07/05 NS6000対応。'''''''''''''''''''''''''''''''''''''''''''''''''''''''
                'Call ArrangeRinexPath(sPath, sPathO, sPathN, sPathG, clsProgressInterface)
                '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                Dim sPathJ() As String
                Dim sPathL() As String
                Dim sPathC() As String
                Dim sPathP() As String
                '2020/10/23 FDC 暦ファイルの流用'''''''''''''''''''''''''''''''''''''''''''''''
                'Call ArrangeRinexPath(sPath, sPathO, sPathN, sPathG, sPathJ, sPathL, sPathC, sPathP, clsProgressInterface)

                Dim bImportCheckG() As Boolean
                Dim bImportCheckJ() As Boolean
                Dim bImportCheckL() As Boolean
                Dim bImportCheckC() As Boolean
                Dim bNavFile() As Boolean
                Call ArrangeRinexPath(sPath, sPathO, sPathN, sPathG, sPathJ, sPathL, sPathC, sPathP, clsProgressInterface, bImportCheckG, bImportCheckJ, bImportCheckL, bImportCheckC, bNavFile)
                '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                sPath = sPathO
            Case IMPORT_TYPE_DIRECT
            End Select

            'ファイルの読み込み。
            Dim objNewObservationPoints As New Collection
            Dim clsObservationPoint As ObservationPoint
            Dim i As Long

            '2020/10/26 FDC 暦ファイルの流用、衛星種別追加'''''''''''''''''''''''''''''''''
            Dim sFileCheckResult As String
            Dim sDatPathN() As String
            Dim sDatPathP() As String
            Dim sImportPathN() As String
            Dim sImportPathP() As String
            Dim sMessage() As String

            If nImportType = IMPORT_TYPE_DAT Or nImportType = IMPORT_TYPE_DIRECT Then
                ReDim Preserve sPathO(-1 To -1)
            End If
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

            For i = 0 To UBound(sPath)
                'ファイル名文字数のチェック。
                If CheckFileTitle(sPath(i)) Then

                    Do While (True)

                        'ファイル読み込み。
                        Select Case nImportType
                        Case IMPORT_TYPE_DAT, IMPORT_TYPE_DIRECT   '2007/4/10 NGS Yamada
                            Set clsObservationPoint = ReadDatFile(sPath(i), clsProgressInterface)

                            '2020/10/26 FDC 暦ファイルの流用、衛星種別追加'''''''''''''''''''''''''''''''''
                            ReDim Preserve sPathN(-1 To i)
                            ReDim Preserve sPathP(-1 To i)

                            If Not clsObservationPoint Is Nothing Then
                                sFileCheckResult = CheckImportDatFile(sPath(i), sPathN(i), sPathP(i), clsObservationPoint)
                                If sFileCheckResult <> "" Then
                                    Dim nUBound As Long
                                    nUBound = UBound(sPathO) + 1
                                    ReDim Preserve sPathO(-1 To nUBound)
                                    ReDim Preserve sDatPathN(-1 To nUBound)
                                    ReDim Preserve sPathG(-1 To nUBound)
                                    ReDim Preserve sPathJ(-1 To nUBound)
                                    ReDim Preserve sPathL(-1 To nUBound)
                                    ReDim Preserve sPathC(-1 To nUBound)
                                    ReDim Preserve sDatPathP(-1 To nUBound)
                                    ReDim Preserve bImportCheckG(-1 To nUBound)
                                    ReDim Preserve bImportCheckJ(-1 To nUBound)
                                    ReDim Preserve bImportCheckL(-1 To nUBound)
                                    ReDim Preserve bImportCheckC(-1 To nUBound)
                                    ReDim Preserve sMessage(-1 To nUBound)

                                    sPathO(nUBound) = sPath(i)
                                    Call DeleteTempFile(sPath(i))
                                    bImportCheckG(nUBound) = clsObservationPoint.GlonassFlag
                                    bImportCheckJ(nUBound) = clsObservationPoint.QZSSFlag
                                    bImportCheckL(nUBound) = clsObservationPoint.GalileoFlag
                                    bImportCheckC(nUBound) = clsObservationPoint.BeiDouFlag
                                    sMessage(nUBound) = sFileCheckResult

                                    Set clsObservationPoint = Nothing
                                End If
                            End If
                            Exit Do
                            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                        Case IMPORT_TYPE_RINEX
                            '2017/07/05 NS6000対応。'''''''''''''''''''''''''''''''''''''''''''''''''''''''
                            'Set clsObservationPoint = ReadRinexFile(sPath(i), sPathN(i), sPathG(i), clsProgressInterface)
                            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                            '2020/10/26 FDC 暦ファイルの流用、衛星種別追加'''''''''''''''''''''''''''''''''
                            'Set clsObservationPoint = ReadRinexFile(sPath(i), sPathN(i), sPathG(i), sPathJ(i), sPathL(i), sPathC(i), sPathP(i), clsProgressInterface)
                            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                            Set clsObservationPoint = ReadRinexFile(sPath(i), sPathN(i), sPathG(i), sPathJ(i), sPathL(i), sPathC(i), sPathP(i), clsProgressInterface, bImportCheckG(i), bImportCheckJ(i), bImportCheckL(i), bImportCheckC(i), bNavFile(i))

                            If Not clsObservationPoint Is Nothing Then
                                sFileCheckResult = CheckImportRinexFile(sPath(i), clsObservationPoint)
                                If sFileCheckResult = "" Then
                                    Exit Do
                                Else
                                    Call DeleteTempFile(sPath(i))

                                    bImportCheckG(i) = clsObservationPoint.GlonassFlag
                                    bImportCheckJ(i) = clsObservationPoint.QZSSFlag
                                    bImportCheckL(i) = clsObservationPoint.GalileoFlag
                                    bImportCheckC(i) = clsObservationPoint.BeiDouFlag

                                    Call DivertRinexPath(sPath(i), sPathN(i), sPathG(i), sPathJ(i), sPathL(i), sPathC(i), sPathP(i), clsProgressInterface, sPathN, sPathP, bImportCheckG(i), bImportCheckJ(i), bImportCheckL(i), bImportCheckC(i), True, sFileCheckResult, True)
                                    If sPathN(i) = "" And sPathP(i) = "" Then
                                        Set clsObservationPoint = Nothing
                                        Exit Do
                                    End If
                                    bNavFile(i) = True
                                End If
                            ElseIf bNavFile(i) Then
                                sFileCheckResult = "衛星軌道情報の適用に失敗しました。"
                                Call DivertRinexPath(sPath(i), sPathN(i), sPathG(i), sPathJ(i), sPathL(i), sPathC(i), sPathP(i), clsProgressInterface, sPathN, sPathP, bImportCheckG(i), bImportCheckJ(i), bImportCheckL(i), bImportCheckC(i), True, sFileCheckResult, False)
                                If sPathN(i) = "" And sPathP(i) = "" Then
                                    Set clsObservationPoint = Nothing
                                    Exit Do
                                End If
                            Else
                                Exit Do
                            End If

                        Case Else
                            Exit Do
                            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                        End Select
                    Loop

                    If Not clsObservationPoint Is Nothing Then
                        Call objNewObservationPoints.Add(clsObservationPoint, Hex$(GetPointer(clsObservationPoint)))
                    Else
                        '無効なデータがあった場合はFalseを返す。
                        Import = False
                    End If
                Else
                    Call MsgBox(sPath(i) & vbCrLf & "このファイルはファイル名が長すぎます。", vbCritical)
                    '無効なデータがあった場合はFalseを返す。
                    Import = False
                End If

                'プログレス。
                Call clsProgressInterface.CheckCancel
            Next

            '2020/10/26 FDC 暦ファイルの流用、Datの流用''''''''''''''''''''''''''''''''''''
            Dim bAlert As Boolean
            If nImportType = IMPORT_TYPE_DAT Or nImportType = IMPORT_TYPE_DIRECT Then
                For i = 0 To UBound(sPathO)
                    bAlert = True
                    Do While (True)
                        Call DivertRinexPath(sPathO(i), sDatPathN(i), sPathG(i), sPathJ(i), sPathL(i), sPathC(i), sDatPathP(i), clsProgressInterface, sPathN, sPathP, bImportCheckG(i), bImportCheckJ(i), bImportCheckL(i), bImportCheckC(i), False, sMessage(i), bAlert)
                        If sDatPathN(i) = "" And sDatPathP(i) = "" Then
                            Set clsObservationPoint = Nothing
                            Exit Do
                        End If
                        Set clsObservationPoint = ReadRinexFile(sPathO(i), sDatPathN(i), sPathG(i), sPathJ(i), sPathL(i), sPathC(i), sDatPathP(i), clsProgressInterface, bImportCheckG(i), bImportCheckJ(i), bImportCheckL(i), bImportCheckC(i), True)
                        If Not clsObservationPoint Is Nothing Then
                            sFileCheckResult = CheckImportRinexFile(sPathO(i), clsObservationPoint)
                            If sFileCheckResult = "" Then
                                nUBound = UBound(sPathN) + 1
                                ReDim Preserve sPathN(-1 To nUBound)
                                ReDim Preserve sPathP(-1 To nUBound)
                                sPathN(nUBound) = sDatPathN(i)
                                sPathP(nUBound) = sDatPathP(i)
                                Exit Do
                            Else
                                Call DeleteTempFile(sPathO(i))
                                sMessage(i) = sFileCheckResult
                                bAlert = True
                            End If
                        Else
                            sMessage(i) = "衛星軌道情報の適用に失敗しました。"
                            bAlert = False
                        End If
                    Loop
                    If Not clsObservationPoint Is Nothing Then
                        Call objNewObservationPoints.Add(clsObservationPoint, Hex$(GetPointer(clsObservationPoint)))
                    Else
                        '無効なデータがあった場合はFalseを返す。
                        Import = False
                    End If
                    'プログレス。
                    Call clsProgressInterface.CheckCancel
                Next
            End If
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

            'これ以降キャンセルは不可。
            clsProgressInterface.CancelEnable = False
            clsProgressInterface.Prompt = "観測点を登録中･･･"

            '既存の代表観測点のハッシュ。
            Dim objRepresentPointCollection As New Collection
            Dim clsChainList As ChainList
            Set clsChainList = m_clsRepresentPointHead.NextList
            Do While Not clsChainList Is Nothing
                Set clsObservationPoint = clsChainList.Element
                If clsObservationPoint.PrevPoint Is Nothing Then Call objRepresentPointCollection.Add(clsObservationPoint, clsObservationPoint.Number)
                Set clsChainList = clsChainList.NextList
                'プログレス。
                Call clsProgressInterface.CheckCancel
            Loop

            '観測点番号が空のものには自動的に設定。#0000～連番。
            Dim nAutoNumber As Long
            nAutoNumber = 0
            Dim clsSameRepresentPoint As ObservationPoint
            Dim clsNewObservationPoint As ObservationPoint
            For Each clsNewObservationPoint In objNewObservationPoints
                '空？
                If clsNewObservationPoint.Number = "" Then
                    '連番割り当て。重複しないように。
                    Do
                        nAutoNumber = nAutoNumber + 1
                        clsNewObservationPoint.Number = Format$(nAutoNumber, "\#0000")
                        If Not LookupCollectionObject(objRepresentPointCollection, clsSameRepresentPoint, clsNewObservationPoint.Number) Then Exit Do
                    Loop
                End If
            Next

            '結合。同じ観測点番号は１つにつなげる。
            Dim nChainNumber As Long '0=要確認。1=すべてはい。2=すべていいえ。
            nChainNumber = 0
            Dim nChainDistance As Long '0=要確認。1=すべてはい。2=すべていいえ。
            nChainDistance = 0
            Dim nMax As Long
            nMax = GetPrivateProfileInt(PROFILE_DEF_SEC_IMPORT, PROFILE_DEF_KEY_DISTANCE, PROFILE_DEF_DEF_DISTANCE, App.Path & "\" & PROFILE_DEF_NAME)
            Dim sMax As String
            If 100 < nMax Then
                If 0 < nMax Mod 100 Then
                    sMax = CStr(nMax) & "㎝"
                Else
                    sMax = CStr(nMax / 100) & "ｍ"
                End If
            Else
                sMax = CStr(nMax) & "㎝"
            End If
            Dim clsRepresentTail As ChainList
            Dim clsRepresentPoint As ObservationPoint
            Set clsRepresentTail = m_clsRepresentPointHead.TailList
            For Each clsNewObservationPoint In objNewObservationPoints
                '代表観測点を作成。
                Set clsRepresentPoint = CreateRepresentPoint(clsNewObservationPoint)
                '既存の代表観測点の観測点番号と比較する。
                Dim bChain As Boolean
                If LookupCollectionObject(objRepresentPointCollection, clsSameRepresentPoint, clsRepresentPoint.Number) Then
                    Select Case nChainNumber
                    Case 0
                        '確認。
                        Select Case frmMsgBoxYesNo.DoModal("観測点No""" & clsRepresentPoint.Number & """はすでに存在します。結合しますか?" & vbCrLf & "結合しない場合は自動的に他の観測点Noが割り当てられます。")
                        Case vbYes
                            bChain = True
                        Case 8
                            bChain = True
                            nChainNumber = 1
                        Case 16
                            bChain = False
                            nChainNumber = 2
                        Case Else
                            bChain = False
                        End Select
                    Case 1
                        bChain = True
                    Case 2
                        bChain = False
                    End Select

                    If bChain Then
                        '距離を評価する。
                        Dim nDistance As Double
                        nDistance = GetDistanceRound(clsSameRepresentPoint.CoordinateDisplay, clsRepresentPoint.CoordinateDisplay)
                        If nDistance > nMax * 0.01 Then
                            Select Case nChainDistance
                            Case 0
                                '確認。
                                Select Case frmMsgBoxYesNo.DoModal("観測点No""" & clsRepresentPoint.Number & """の観測点同士の距離が" & sMax & "を超えています。結合しますか?" & vbCrLf & "結合しない場合は自動的に他の観測点Noが割り当てられます。")
                                Case vbYes
                                    bChain = True
                                Case 8
                                    bChain = True
                                    nChainDistance = 1
                                Case 16
                                    bChain = False
                                    nChainDistance = 2
                                Case Else
                                    bChain = False
                                End Select
                            Case 1
                                bChain = True
                            Case Else
                                bChain = False
                            End Select
                        Else
                            bChain = True
                        End If
                    End If

                    '連結しない場合、観測点番号を自動的に変更。観測点番号 + "~(n)"。
                    If Not bChain Then
                        Dim sNumber As String
                        sNumber = clsRepresentPoint.Number & "~("
                        Dim nExtNumber As Long
                        nExtNumber = 1
                        Do
                            nExtNumber = nExtNumber + 1
                            clsRepresentPoint.Number = sNumber & CStr(nExtNumber) & ")"
                            If Not LookupCollectionObject(objRepresentPointCollection, clsSameRepresentPoint, clsRepresentPoint.Number) Then Exit Do
                        Loop
                    End If
                Else
                    bChain = False
                End If

                If bChain Then
                    '観測点番号が一致する代表観測点があればつなげる。
                    Call ChainRepresentPoint(clsSameRepresentPoint, clsRepresentPoint, 0, 0)
                    Call clsRepresentPoint.UpdateCorrectPointImpl(clsSameRepresentPoint.CorrectPoint)
                Else
                    '同じ観測点番号の代表観測点がない。
                    Call objRepresentPointCollection.Add(clsRepresentPoint, clsRepresentPoint.Number)
                End If

                '代表観測点リストにつなげる。
                Set clsRepresentTail = InsertRepresentPoint(clsRepresentTail, clsRepresentPoint)
                'プログレス。
                Call clsProgressInterface.CheckCancel
            Next

            '重複(観測点番号、セッション名が一致)する観測点は上書きする。
            Dim objIsolatePointChainCollection As Collection
            Set objIsolatePointChainCollection = MakeIsolatePointChainCollection
            Dim objBaseLineVectorChainCollection As Collection
            Set objBaseLineVectorChainCollection = MakeBaseLineVectorChainCollection
            Dim nResult As Long
            Dim bMsg As Boolean
            bMsg = True
            For Each clsNewObservationPoint In objNewObservationPoints
                'つながってる兄弟全てと比較する。
                Dim clsTargetPoint As ObservationPoint
                Set clsTargetPoint = clsNewObservationPoint.HeadPoint
                Do While Not clsTargetPoint Is Nothing
                    If (Not clsTargetPoint.ChildPoint Is clsNewObservationPoint) And (clsTargetPoint.Session <> "") Then
                        'セッション名が一致したら重複と見なす。
                        If clsTargetPoint.Session = clsNewObservationPoint.Session Then
                            '上書き確認。
                            If bMsg Then
                                Dim nStyle As Long
                                nStyle = 2
                                nResult = vbNo
                                RaiseEvent OverwriteNotification(clsNewObservationPoint, nStyle, nResult)
                                If nResult = vbYes Then
                                    nResult = 8
                                ElseIf nResult = vbNo Then
                                    nResult = 16
                                Else
                                    '次から確認なし。
                                    bMsg = False
                                End If
                            End If
                            If nResult = 8 Then
                                'HeadPoint を削除する場合、本点の CorrectPoint を変更する。
                                If clsTargetPoint.PrevPoint Is Nothing Then
                                    If clsTargetPoint.Eccentric Then Call clsTargetPoint.CorrectPoint.UpdateCorrectPoint(clsTargetPoint.NextPoint) 'NextPoint は必ず存在するはず。
                                End If
                                '既存の観測点を削除する。
                                Call RemoveAtCollection(objRepresentPointCollection, Hex$(GetPointer(clsTargetPoint)))
                                Call RemoveAtCollection(objNewObservationPoints, Hex$(GetPointer(clsTargetPoint.ChildPoint)))
                                Call RemoveRepresentPoint(clsTargetPoint, sObsPointPath, objIsolatePointChainCollection, objBaseLineVectorChainCollection)
                                Exit Do
                            Else
                                '新しい観測点を削除する。
                                Call RemoveAtCollection(objRepresentPointCollection, Hex$(GetPointer(clsNewObservationPoint.TopParentPoint)))
                                Call RemoveAtCollection(objNewObservationPoints, Hex$(GetPointer(clsNewObservationPoint)))
                                Call RemoveRepresentPoint(clsNewObservationPoint.TopParentPoint, sObsPointPath, objIsolatePointChainCollection, objBaseLineVectorChainCollection)
                                Exit Do
                            End If
                        End If
                    End If
                    Set clsTargetPoint = clsTargetPoint.NextPoint
                Loop
                'プログレス。
                Call clsProgressInterface.CheckCancel
            Next

            'ファイル名からセッション名が取得できていなかった場合、｢S｣＋３桁の連番。
            For Each clsNewObservationPoint In objNewObservationPoints
                Set clsRepresentPoint = clsNewObservationPoint.TopParentPoint
                If clsRepresentPoint.Session = "" Then
                    Set clsTargetPoint = clsRepresentPoint.HeadPoint
                    Dim objSessionCollection As Collection
                    Set objSessionCollection = New Collection
                    '既存のセッション名。
                    Do While Not clsTargetPoint Is Nothing
                        If clsTargetPoint.Session <> "" Then Call objSessionCollection.Add(clsTargetPoint, clsTargetPoint.Session)
                        Set clsTargetPoint = clsTargetPoint.NextPoint
                    Loop
                    For i = 1 To 999
                        clsRepresentPoint.Session = "S" & Format$(i, "000")
                        Dim objItem As Object
                        If Not LookupCollectionObject(objSessionCollection, objItem, clsRepresentPoint.Session) Then Exit For
                    Next
                    If 999 < i Then
                        'S999を超えた場合は観測点を削除する。
                        Call RemoveAtCollection(objNewObservationPoints, Hex$(GetPointer(clsRepresentPoint.ChildPoint)))
                        Call RemoveRepresentPoint(clsRepresentPoint, sObsPointPath, objIsolatePointChainCollection, objBaseLineVectorChainCollection)
                    End If
                    '仮のセッション。
                    clsRepresentPoint.ProvisionalSession = True
                End If
                'プログレス。
                Call clsProgressInterface.CheckCancel
            Next

            '基線ベクトルのセッションを決めるためのコレクション。基線ベクトルキーのコレクション。
            Dim objBaseLineVectorKeyCollection As New Collection
            Set clsChainList = m_clsBaseLineVectorHead.NextList
            Do While Not clsChainList Is Nothing
                Call objBaseLineVectorKeyCollection.Add(clsChainList.Element, clsChainList.Element.Key)
                Set clsChainList = clsChainList.NextList
                'プログレス。
                Call clsProgressInterface.CheckCancel
            Loop

            '新規に追加された観測点の基線を評価する。
            Dim objObservationCollection  As New Collection
            Set clsChainList = m_clsRepresentPointHead.NextList
            Do While Not clsChainList Is Nothing
                Set clsObservationPoint = clsChainList.Element.ChildPoint
                '本点以外。
                If Not clsObservationPoint.Genuine Then
                    Call objObservationCollection.Add(clsObservationPoint, Hex$(GetPointer(clsObservationPoint)))
                    '遷移状態の取得。
                    If clsObservationPoint.SatelliteInfo Is Nothing Then
                        Dim clsSatelliteInfoReader As SatelliteInfoReader
                        Set clsSatelliteInfoReader = New SatelliteInfoReader
                        Call clsSatelliteInfoReader.OpenFile(sObsPointPath & clsObservationPoint.FileTitle & "." & RNX_SV_EXTENSION)
                        Set clsObservationPoint.SatelliteInfo = clsSatelliteInfoReader.ReadChanges2(clsObservationPoint.StrTimeGPS, clsObservationPoint.Interval, clsProgressInterface)
                    End If
                End If
                Set clsChainList = clsChainList.NextList
                'プログレス。
                Call clsProgressInterface.CheckCancel
            Loop
            For Each clsNewObservationPoint In objNewObservationPoints
                Call objObservationCollection.Remove(Hex$(GetPointer(clsNewObservationPoint)))
                '総当たり。
                For Each clsTargetPoint In objObservationCollection
                    '観測点番号が異なる点間にのみ基線をむすべる。
                    If clsTargetPoint.Number <> clsNewObservationPoint.Number Then
                        '遷移状態を評価し、条件を満たす場合は基線ベクトルを追加する。
                        Call EstimateSatelliteInfoChanges(nSameTimeMin, nSatelliteCountMin, clsTargetPoint, clsNewObservationPoint, objIsolatePointChainCollection, objBaseLineVectorKeyCollection)
                    End If
                    'プログレス。
                    Call clsProgressInterface.CheckCancel
                Next
                '基線が結べなかったら孤立観測点。
                If clsNewObservationPoint.ChildPoint Is Nothing Then
                    '孤立観測点リストに追加する。
                    Set clsChainList = m_clsIsolatePointHead.TailList.InsertNext(clsNewObservationPoint)
                    Call objIsolatePointChainCollection.Add(clsChainList, Hex$(GetPointer(clsNewObservationPoint)))
                End If
                'プログレス。
                Call clsProgressInterface.CheckCancel
            Next

            For Each clsNewObservationPoint In objNewObservationPoints
                'インポートに成功した観測点ファイルを移動。
                Call clsNewObservationPoint.UpdateFileName(App.Path & TEMPORARY_PATH & IMPORT_TEMPPATH, sObsPointPath)
                'プログレス。
                Call clsProgressInterface.CheckCancel
            Next

            '衛星情報の遷移状態を破棄。
            Set clsChainList = m_clsRepresentPointHead.NextList
            Do While Not clsChainList Is Nothing
                Set clsChainList.Element.ChildPoint.SatelliteInfo = Nothing
                Set clsChainList = clsChainList.NextList
                'プログレス。
                Call clsProgressInterface.CheckCancel
            Loop

            'テンポラリファイルのクリア。
            Call EmptyDir(App.Path & TEMPORARY_PATH & IMPORT_TEMPPATH, True)

        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '外部ファイルをインポートする。
        '
        '引き数：
        'nImportType インポートファイル種別。
        'sPath インポートするファイルのパス。参照する配列の要素は(0 To ...)。Ubound 関数を使用する。
        'sObsPointPath 観測点フォルダのパス。
        'nSameTimeMin 最小同時観測時間(秒)。
        'nSatelliteCountMin 最少共通衛星数。
        'clsProgressInterface ProgressInterface オブジェクト。
        '
        '戻り値：
        '全て正常にインポートできた場合は True を返す。
        '無効なデータがあった場合は False を返す。
        */
        public unsafe bool Import(IMPORT_TYPE nImportType, string* sPath, string sObsPointPath, long nSameTimeMin, long nSatelliteCountMin, ProgressInterface clsProgressInterface)
        {
            return false;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '汎用作業キーを初期化する。
        '
        'nWorkKey で指定される値で WorkKey を初期化する。
        '観測点、基線ベクトルの WorkKey を初期化する。
        '
        '引き数：
        'nWorkKey 初期化する値。
        Public Sub ClearWorkKey(ByVal nWorkKey As Long)
            Dim clsChainList As ChainList
            Set clsChainList = m_clsBaseLineVectorHead.NextList
            Do While Not clsChainList Is Nothing
                clsChainList.Element.WorkKey = nWorkKey
                Call clsChainList.Element.ClearWorkKey(nWorkKey)
                Set clsChainList = clsChainList.NextList
            Loop
            Set clsChainList = m_clsIsolatePointHead.NextList
            Do While Not clsChainList Is Nothing
                Call clsChainList.Element.ClearWorkKey(nWorkKey)
                Set clsChainList = clsChainList.NextList
            Loop
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '汎用作業キーを初期化する。
        '
        'nWorkKey で指定される値で WorkKey を初期化する。
        '観測点、基線ベクトルの WorkKey を初期化する。
        '
        '引き数：
        'nWorkKey 初期化する値。
        */
        public void ClearWorkKey(long nWorkKey)
        {
            ChainList clsChainList;
            clsChainList = m_clsBaseLineVectorHead.NextList();
            while (clsChainList != null)
            {
                ((BaseLineVector)clsChainList.Element).WorkKey = nWorkKey;
                ((BaseLineVector)clsChainList.Element).ClearWorkKey(nWorkKey);
                clsChainList = clsChainList.NextList();
            }
            clsChainList = m_clsIsolatePointHead.NextList();
            while (clsChainList != null)
            {
                ((BaseLineVector)clsChainList.Element).ClearWorkKey(nWorkKey);
                clsChainList = clsChainList.NextList();
            }
            return;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'リスト更新必要フラグを初期化する。
        '
        'IsList をOFFにする。
        '観測点、基線ベクトルの IsList を初期化する。
        Public Sub ClearIsList()
            Dim clsChainList As ChainList
            Set clsChainList = m_clsBaseLineVectorHead.NextList
            Do While Not clsChainList Is Nothing
                clsChainList.Element.IsList = False
                Call clsChainList.Element.ClearIsList
                Set clsChainList = clsChainList.NextList
            Loop
            Set clsChainList = m_clsIsolatePointHead.NextList
            Do While Not clsChainList Is Nothing
                clsChainList.Element.IsList = False
                Set clsChainList = clsChainList.NextList
            Loop
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]  //6
        /*
            'リスト更新必要フラグを初期化する。
            '
            'IsList をOFFにする。
            '観測点、基線ベクトルの IsList を初期化する。
        */
        public void ClearIsList()
        {
            ChainList clsChainList;
            clsChainList = m_clsBaseLineVectorHead.NextList();

            while (clsChainList != null)
            {
              //  clsChainList.Element.IsList = false;
              //  clsChainList.Element.ClearIsList();
                clsChainList = clsChainList.NextList();
            }
            clsChainList = m_clsIsolatePointHead.NextList();

            while (clsChainList != null)
            {
                //  clsChainList.Element.IsList = false;
                clsChainList = clsChainList.NextList();
            }


            return;
        }

        //==========================================================================================

        //==========================================================================================
        /*[VB]
            '指定された基線ベクトルの有効/無効を設定する。
            '
            '設定が変更されたオブジェクトの IsList をONに設定する。
            '
            '引き数：
            'clsBaseLineVector 対象とする基線ベクトル。
            'bEnable 有効フラグ。
            Public Sub EnableBaseLineVector(ByVal clsBaseLineVector As BaseLineVector, ByVal bEnable As Boolean)
                '設定。
                clsBaseLineVector.Enable = bEnable
                clsBaseLineVector.IsList = True
                clsBaseLineVector.StrPoint.TopParentPoint.IsList = True
                clsBaseLineVector.EndPoint.TopParentPoint.IsList = True
                '本点。
                If clsBaseLineVector.StrPoint.Eccentric Then clsBaseLineVector.StrPoint.CorrectPoint.TopParentPoint.IsList = True
                If clsBaseLineVector.EndPoint.Eccentric Then clsBaseLineVector.EndPoint.CorrectPoint.TopParentPoint.IsList = True
            End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]  //3
        /// <summary>
        /// 指定された基線ベクトルの有効/無効を設定する。
        /// '
        /// 設定が変更されたオブジェクトの IsList をONに設定する。
        /// '
        /// 引き数：
        /// clsBaseLineVector 対象とする基線ベクトル。
        /// bEnable 有効フラグ。
        /// 
        /// </summary>
        /// <param name="clsBaseLineVector"></param>
        /// <param name="bEnable"></param>
        public void EnableBaseLineVector(BaseLineVector clsBaseLineVector, bool bEnable)    //3
        {

            clsBaseLineVector.Enable(bEnable);
            clsBaseLineVector.IsList = true;
            clsBaseLineVector.StrPoint().TopParentPoint().IsList = true;
            clsBaseLineVector.EndPoint().TopParentPoint().IsList = true;

            //'本点。
            if (clsBaseLineVector.StrPoint().Eccentric())
            {
                clsBaseLineVector.StrPoint().CorrectPoint().TopParentPoint().IsList = true;
                clsBaseLineVector.EndPoint().CorrectPoint().TopParentPoint().IsList = true;
            }


            return;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
            '指定された観測点の有効/無効を設定する。
            '
            '設定が変更されたオブジェクトの IsList をONに設定する。
            '
            '引き数：
            'clsObservationPoint 対象とする観測点(代表観測点)。
            'bEnable 有効フラグ。
            Public Sub EnableObservationPoint(ByVal clsObservationPoint As ObservationPoint, ByVal bEnable As Boolean)

                '接続している基線ベクトルを取得する。
                Dim clsBaseLineVectors() As BaseLineVector
                ReDim clsBaseLineVectors(-1 To -1)
                Call GetConnectBaseLineVectors(clsObservationPoint, clsBaseLineVectors)
    
                If UBound(clsBaseLineVectors) < 0 Then
                    clsObservationPoint.Enable = bEnable
                    clsObservationPoint.IsList = True
                    '本点。
                    If clsObservationPoint.Eccentric Then clsObservationPoint.CorrectPoint.TopParentPoint.IsList = True
                Else
                    '基線ベクトルを有効/無効化。
                    Dim i As Long
                    For i = 0 To UBound(clsBaseLineVectors)
                        Call EnableBaseLineVector(clsBaseLineVectors(i), bEnable)
                    Next
                End If
    
            End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]  //3
        /// <summary>
        /// 指定された観測点の有効/無効を設定する。
        /// '
        /// 設定が変更されたオブジェクトの IsList をONに設定する。
        /// '
        /// 引き数：
        /// clsObservationPoint 対象とする観測点(代表観測点)。
        /// bEnable 有効フラグ。
        /// 
        /// </summary>
        /// <param name="clsObservationPoint"></param>
        /// <param name="bEnable"></param>
        public void EnableObservationPoint(ObservationPoint clsObservationPoint, bool bEnable)  //3
        {
            //'接続している基線ベクトルを取得する。
            List<BaseLineVector> clsBaseLineVectors = new List<BaseLineVector>();
            //  GetConnectBaseLineVectors(clsObservationPoint, ref clsBaseLineVectors);
            m_clsMdlMain.GetDocument().Session_GetConnectBaseLineVectors(clsObservationPoint, ref clsBaseLineVectors);

            if (clsBaseLineVectors.Count < 0)
            {
                clsObservationPoint.Enable(bEnable);
                clsObservationPoint.IsList = true;
                //'本点。
                if (clsObservationPoint.Eccentric())
                {
                    clsObservationPoint.CorrectPoint().TopParentPoint().IsList = true;
                }
            }
            else
            {
                //'基線ベクトルを有効/無効化。
                for (int i = 0; i < clsBaseLineVectors.Count; i++)
                {
                    EnableBaseLineVector(clsBaseLineVectors[i], bEnable);
                }

            }

            return;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '平面直角座標系における表示観測点の範囲を取得する。
        '
        '描画に使う論理エリアを取得する。
        '観測点群に外接する長方形を取得する。余白は無し。
        'nSwitch は評価に含める観測点を決定するためのスイッチ。NS-Survey では使用しない。
        'bIsEnable も NS-Survey では使用しない。言いかえれば常に False である。
        '
        '引き数：
        'nCoordNum 座標系番号(1～19)。
        'nMinX 最小X座標(ｍ)。
        'nMinY 最小Y座標(ｍ)。
        'nMaxX 最大X座標(ｍ)。
        'nMaxY 最大Y座標(ｍ)。
        'bIsEnable 有効フラグ。True=有効な観測点のみ対象とする。False=全ての観測点を対象とする。
        'nSwitch 評価スイッチ。
        '
        '戻り値：
        '範囲が取得できた場合 True を返す。
        '観測点が１つもない場合 False を返す。
        Public Function GetObservationPointArea(ByVal nCoordNum As Long, ByRef nMinX As Double, ByRef nMinY As Double, ByRef nMaxX As Double, ByRef nMaxY As Double, ByVal bIsEnable As Boolean, ByVal nSwitch As Long) As Boolean

            GetObservationPointArea = False
    
            '代表観測点の極座標を取得する。
            Dim clsObservationPoint As ObservationPoint
            Dim clsChainList As ChainList
            Set clsChainList = m_clsRepresentPointHead.NextList
    
            '観測点が１つもなければ適当に設定する。
            If clsChainList Is Nothing Then
                nMinX = 0
                nMinY = 0
                nMaxX = 1
                nMaxY = 1
                Exit Function
            End If
    
            Dim clsPlanePoint As TwipsPoint
            Set clsObservationPoint = clsChainList.Element
            Set clsPlanePoint = clsObservationPoint.PlanePoint
            nMinX = clsPlanePoint.X
            nMinY = clsPlanePoint.Y
            nMaxX = clsPlanePoint.X
            nMaxY = clsPlanePoint.Y
            Set clsChainList = clsChainList.NextList
    
            '最小最大の取得。
            Do While Not clsChainList Is Nothing
                Set clsObservationPoint = clsChainList.Element
                Set clsPlanePoint = clsObservationPoint.PlanePoint
                If clsPlanePoint.X < nMinX Then
                    nMinX = clsPlanePoint.X
                ElseIf nMaxX < clsPlanePoint.X Then
                    nMaxX = clsPlanePoint.X
                End If
                If clsPlanePoint.Y < nMinY Then
                    nMinY = clsPlanePoint.Y
                ElseIf nMaxY < clsPlanePoint.Y Then
                    nMaxY = clsPlanePoint.Y
                End If
                Set clsChainList = clsChainList.NextList
            Loop
    
            GetObservationPointArea = True
    
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]  //3
        /// <summary>
        /// 平面直角座標系における表示観測点の範囲を取得する。
        /// '
        /// 描画に使う論理エリアを取得する。
        /// 観測点群に外接する長方形を取得する。余白は無し。
        /// nSwitch は評価に含める観測点を決定するためのスイッチ。NS-Survey では使用しない。
        /// bIsEnable も NS-Survey では使用しない。言いかえれば常に False である。
        /// '
        /// 引き数：
        /// nCoordNum 座標系番号(1～19)。
        /// nMinX 最小X座標(ｍ)。
        /// nMinY 最小Y座標(ｍ)。
        /// nMaxX 最大X座標(ｍ)。
        /// nMaxY 最大Y座標(ｍ)。
        /// bIsEnable 有効フラグ。True=有効な観測点のみ対象とする。False=全ての観測点を対象とする。
        /// nSwitch 評価スイッチ。
        /// 
        /// </summary>
        /// <param name="nCoordNum"></param>
        /// <param name="nMinX"></param>
        /// <param name="nMinY"></param>
        /// <param name="nMaxX"></param>
        /// <param name="nMaxY"></param>
        /// <param name="bIsEnable"></param>
        /// <param name="nSwitch"></param>
        /// <returns>
        /// 戻り値：
        /// 範囲が取得できた場合 True を返す。
        /// 観測点が１つもない場合 False を返す。
        /// </returns>
        public bool GetObservationPointArea(long nCoordNum, ref double nMinX, ref double nMinY, ref double nMaxX, ref double nMaxY, bool bIsEnable, long nSwitch)
        {
            //'代表観測点の極座標を取得する。
            ObservationPoint clsObservationPoint;
            ChainList clsChainList;
            clsChainList = m_clsRepresentPointHead.NextList();


            //'観測点が１つもなければ適当に設定する。
            if (clsChainList == null)
            {
                nMinX = 0;
                nMinY = 0;
                nMaxX = 1;
                nMaxY = 1;
                return false;
            }


            TwipsPoint clsPlanePoint;
            clsObservationPoint = (ObservationPoint)clsChainList.Element;
            clsPlanePoint = clsObservationPoint.PlanePoint();
            nMinX = clsPlanePoint.X;
            nMinY = clsPlanePoint.Y;
            nMaxX = clsPlanePoint.X;
            nMaxY = clsPlanePoint.Y;
            clsChainList = clsChainList.NextList();


            //'最小最大の取得。
            while (clsChainList != null)
            {
                clsObservationPoint = (ObservationPoint)clsChainList.Element;
                clsPlanePoint = clsObservationPoint.PlanePoint();
                if (clsPlanePoint.X < nMinX)
                {
                    nMinX = clsPlanePoint.X;
                }
                else if (nMaxX < clsPlanePoint.X)
                {
                    nMaxX = clsPlanePoint.X;
                }
                if (clsPlanePoint.Y < nMinY)
                {
                    nMinY = clsPlanePoint.Y;
                }
                else if (nMaxY < clsPlanePoint.Y)
                {
                    nMaxY = clsPlanePoint.Y;
                }
                clsChainList = clsChainList.NextList();
            }
            return true;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '観測点を結合する。
        '
        '引き数：
        'clsSrcObservationPoint 結合元の観測点(代表観測点)。
        'clsDstObservationPoint 結合先の観測点(代表観測点)。
        Public Sub CombinationObservationPoint(ByVal clsSrcObservationPoint As ObservationPoint, ByVal clsDstObservationPoint As ObservationPoint)

            '結合。
            Dim clsObservationPoint As ObservationPoint
            Set clsObservationPoint = clsSrcObservationPoint.HeadPoint
            Call ChainRepresentPoint(clsDstObservationPoint, clsObservationPoint, 0, 0)
            Call clsObservationPoint.UpdateCorrectPointImpl(clsDstObservationPoint.CorrectPoint)
    
            '観測点ファイル名の更新。
            Dim sDirPath As String
            sDirPath = App.Path & TEMPORARY_PATH & "." & OBSPOINT_PATH
            Do While Not clsObservationPoint Is Nothing
                Call clsObservationPoint.UpdateFileName(sDirPath)
                Set clsObservationPoint = clsObservationPoint.NextPoint
            Loop
    
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '観測点を結合する。
        '
        '引き数：
        'clsSrcObservationPoint 結合元の観測点(代表観測点)。
        'clsDstObservationPoint 結合先の観測点(代表観測点)。
        */
        public void CombinationObservationPoint(ObservationPoint clsSrcObservationPoint, ObservationPoint clsDstObservationPoint)
        {
            return;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '指定された基線ベクトルと重複している基線ベクトルを取得する。
        '
        '引き数：
        'clsBaseLineVector 対象とする基線ベクトル。
        '
        '戻り値：重複基線ベクトルの配列を返す。配列の要素は(-1 To ...)、要素 -1 は未使用。
        Public Function GetDuplicationBaseLineVectors(ByVal clsBaseLineVector As BaseLineVector) As BaseLineVector()
            Dim clsStrPoint As ObservationPoint
            Dim clsEndPoint As ObservationPoint
            Set clsStrPoint = clsBaseLineVector.StrPoint.RootPoint
            Set clsEndPoint = clsBaseLineVector.EndPoint.RootPoint
            '始点に接続している基線ベクトルを取得。
            Dim clsConnectBaseLineVectors() As BaseLineVector
            ReDim clsConnectBaseLineVectors(-1 To -1)
            Call GetConnectBaseLineVectorsEx(clsStrPoint, clsConnectBaseLineVectors)
            '始点と終点が同じ基線ベクトルを抽出。
            Dim clsDuplicationBaseLineVectors() As BaseLineVector
            Dim nCount As Long
            Dim i As Long
            ReDim clsDuplicationBaseLineVectors(-1 To UBound(clsConnectBaseLineVectors))
            nCount = -1
            For i = 0 To UBound(clsConnectBaseLineVectors)
                If Not clsBaseLineVector Is clsConnectBaseLineVectors(i) Then
                    If clsStrPoint Is clsConnectBaseLineVectors(i).StrPoint.RootPoint Then
                        If clsEndPoint Is clsConnectBaseLineVectors(i).EndPoint.RootPoint Then
                            nCount = nCount + 1
                            Set clsDuplicationBaseLineVectors(nCount) = clsConnectBaseLineVectors(i)
                        End If
                    ElseIf clsEndPoint Is clsConnectBaseLineVectors(i).StrPoint.RootPoint Then
                        If clsStrPoint Is clsConnectBaseLineVectors(i).EndPoint.RootPoint Then
                            nCount = nCount + 1
                            Set clsDuplicationBaseLineVectors(nCount) = clsConnectBaseLineVectors(i)
                        End If
                    End If
                End If
            Next
            ReDim Preserve clsDuplicationBaseLineVectors(-1 To nCount)
            GetDuplicationBaseLineVectors = clsDuplicationBaseLineVectors
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '指定された基線ベクトルと重複している基線ベクトルを取得する。
        '
        '引き数：
        'clsBaseLineVector 対象とする基線ベクトル。
        '
        '戻り値：重複基線ベクトルの配列を返す。配列の要素は(-1 To ...)、要素 -1 は未使用。
        */
        public List<BaseLineVector> GetDuplicationBaseLineVectors(BaseLineVector clsBaseLineVector)
        {

            List<BaseLineVector> GetDuplicationBaseLineVectors = new List<BaseLineVector>();

            ObservationPoint clsStrPoint;
            ObservationPoint clsEndPoint;

            clsStrPoint = clsBaseLineVector.StrPoint().RootPoint();
            clsEndPoint = clsBaseLineVector.EndPoint().RootPoint();

            //********************************************
            //'始点に接続している基線ベクトルを取得。
            //********************************************
            List<BaseLineVector> clsConnectBaseLineVectors = new List<BaseLineVector>();
            //      ReDim clsConnectBaseLineVectors(-1 To - 1)
            //      Call GetConnectBaseLineVectorsEx(clsStrPoint, clsConnectBaseLineVectors)
            m_clsMdlMain.GetDocument().Session_GetConnectBaseLineVectors(clsStrPoint, ref clsConnectBaseLineVectors);     //5

            //********************************************
            //'始点と終点が同じ基線ベクトルを抽出。
            //********************************************
            //  Dim clsDuplicationBaseLineVectors() As BaseLineVector
            long nCount;
            int i;
            BaseLineVector[] clsDuplicationBaseLineVectors = new BaseLineVector[clsConnectBaseLineVectors.Count];
            nCount = -1;
            /*-------------------------------------------------------
                For i = 0 To UBound(clsConnectBaseLineVectors)
                    If Not clsBaseLineVector Is clsConnectBaseLineVectors(i) Then
                        If clsStrPoint Is clsConnectBaseLineVectors(i).StrPoint.RootPoint Then
                            If clsEndPoint Is clsConnectBaseLineVectors(i).EndPoint.RootPoint Then
                                nCount = nCount + 1
                                Set clsDuplicationBaseLineVectors(nCount) = clsConnectBaseLineVectors(i)
                            End If
                        ElseIf clsEndPoint Is clsConnectBaseLineVectors(i).StrPoint.RootPoint Then
                            If clsStrPoint Is clsConnectBaseLineVectors(i).EndPoint.RootPoint Then
                                nCount = nCount + 1
                                Set clsDuplicationBaseLineVectors(nCount) = clsConnectBaseLineVectors(i)
                            End If
                        End If
                    End If
                Next
             */
            for (i = 0; i < clsConnectBaseLineVectors.Count; i++)
            {
                if (!ReferenceEquals(clsBaseLineVector, clsConnectBaseLineVectors[i]))
                //BaseLineVector obj99 = clsConnectBaseLineVectors[i];
                //if (clsBaseLineVector is BaseLineVector obj99)
                {
                    if (ReferenceEquals(clsStrPoint, clsConnectBaseLineVectors[i].StrPoint().RootPoint()))
                    {
                        if (ReferenceEquals(clsEndPoint,clsConnectBaseLineVectors[i].EndPoint().RootPoint()))
                        {
                            nCount = nCount + 1;
                            clsDuplicationBaseLineVectors[nCount] = clsConnectBaseLineVectors[i];
                        }
                    }
                    else if (ReferenceEquals(clsEndPoint, clsConnectBaseLineVectors[i].StrPoint().RootPoint()))
                    {
                        if (ReferenceEquals(clsStrPoint, clsConnectBaseLineVectors[i].EndPoint().RootPoint()))
                        {
                            nCount = nCount + 1;
                            clsDuplicationBaseLineVectors[nCount] = clsConnectBaseLineVectors[i];
                        }

                    }

                }//if (clsBaseLineVector != clsConnectBaseLineVectors[(int)i])
            }//for (i = 0; i < clsConnectBaseLineVectors.Count; i++)

            //      ReDim Preserve clsDuplicationBaseLineVectors(-1 To nCount)
            for (i = 0; i < clsDuplicationBaseLineVectors.Length; i++)
            {
                GetDuplicationBaseLineVectors.Add(clsDuplicationBaseLineVectors[i]);
            }
            return GetDuplicationBaseLineVectors;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '指定された観測点と連結している基線ベクトルを取得する。
        '
        'clsObservationPoint を始点、または終点としている基線ベクトルを取得する。
        'さらに、clsObservationPoint と同じ観測点番号である観測点に連結している基線ベクトルも取得する。
        'clsBaseLineVectors は最初に Redim clsBaseLineVectors(-1 to -1) で初期化していること。
        '
        '引き数：
        'clsObservationPoint 対象とする観測点。
        'clsBaseLineVectors clsObservationPoint と連結している基線ベクトルが設定される。配列の要素は(-1 To ...)、要素 -1 は未使用。
        Public Sub GetConnectBaseLineVectorsEx(ByVal clsObservationPoint As ObservationPoint, ByRef clsBaseLineVectors() As BaseLineVector)
            Set clsObservationPoint = clsObservationPoint.HeadPoint
            Do While Not clsObservationPoint Is Nothing
                Call GetConnectBaseLineVectors(clsObservationPoint, clsBaseLineVectors)
                Set clsObservationPoint = clsObservationPoint.NextPoint
            Loop
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '指定された観測点と連結している基線ベクトルを取得する。
        '
        'clsObservationPoint を始点、または終点としている基線ベクトルを取得する。
        'さらに、clsObservationPoint と同じ観測点番号である観測点に連結している基線ベクトルも取得する。
        'clsBaseLineVectors は最初に Redim clsBaseLineVectors(-1 to -1) で初期化していること。
        '
        '引き数：
        'clsObservationPoint 対象とする観測点。
        'clsBaseLineVectors clsObservationPoint と連結している基線ベクトルが設定される。配列の要素は(-1 To ...)、要素 -1 は未使用。
        */
        public void GetConnectBaseLineVectorsEx(ObservationPoint clsObservationPoint, List<BaseLineVector> clsBaseLineVectors)
        {
            m_clsMdlMain.GetDocument().Session_GetConnectBaseLineVectors(clsObservationPoint, ref clsBaseLineVectors);  //6

        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '指定された観測点と連結している基線ベクトルを取得する。
        '
        'clsObservationPoint を始点、または終点としている基線ベクトルを取得する。
        'clsBaseLineVectors は最初に Redim clsBaseLineVectors(-1 to -1) で初期化していること。
        '
        '引き数：
        'clsObservationPoint 対象とする観測点。
        'clsBaseLineVectors clsObservationPoint と連結している基線ベクトルが設定される。配列の要素は(-1 To ...)、要素 -1 は未使用。
        Public Sub GetConnectBaseLineVectors(ByVal clsObservationPoint As ObservationPoint, ByRef clsBaseLineVectors() As BaseLineVector)
            Dim clsChildPoint As ObservationPoint
            Set clsChildPoint = clsObservationPoint.ChildPoint
            If clsChildPoint Is Nothing Then
                '接合観測点の場合、基線ベクトルを追加する。
                If (clsObservationPoint.ObjectType And OBS_TYPE_CONNECT) <> 0 Then
                    Dim nUBound As Long
                    nUBound = UBound(clsBaseLineVectors) + 1
                    ReDim Preserve clsBaseLineVectors(-1 To nUBound)
                    Set clsBaseLineVectors(nUBound) = clsObservationPoint.Owner
                End If
            Else
                '子観測点すべてを巡回する。
                Do While Not clsChildPoint Is Nothing
                    Call GetConnectBaseLineVectors(clsChildPoint, clsBaseLineVectors)
                    Set clsChildPoint = clsChildPoint.NextPoint
                Loop
            End If
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]  //2
        /// <summary>
        /// 指定された観測点と連結している基線ベクトルを取得する。
        /// '
        /// clsObservationPoint を始点、または終点としている基線ベクトルを取得する。
        /// clsBaseLineVectors は最初に Redim clsBaseLineVectors(-1 to -1) で初期化していること。
        /// '
        /// 引き数：
        /// clsObservationPoint 対象とする観測点。
        /// clsBaseLineVectors clsObservationPoint と連結している基線ベクトルが設定される。配列の要素は(-1 To ...)、要素 -1 は未使用。
        /// 
        /// </summary>
        /// <param name="clsObservationPoint"></param>
        /// <param name="clsBaseLineVectors"></param>
        public void GetConnectBaseLineVectors(ObservationPoint clsObservationPoint, ref List<BaseLineVector> clsBaseLineVectors)    //2
        {
            ObservationPoint clsChildPoint;
            clsChildPoint = clsObservationPoint.ChildPoint();


            if (clsChildPoint == null)
            {
                /*----------------------------------------------------------------------
                    '接合観測点の場合、基線ベクトルを追加する。
                    If (clsObservationPoint.ObjectType And OBS_TYPE_CONNECT) <> 0 Then
                        Dim nUBound As Long
                        nUBound = UBound(clsBaseLineVectors) + 1
                        ReDim Preserve clsBaseLineVectors(-1 To nUBound)
                        Set clsBaseLineVectors(nUBound) = clsObservationPoint.Owner
                    End If
                 ---------------------------------------------------------------------*/
                //接合観測点の場合、基線ベクトルを追加する。
                if ((clsObservationPoint.ObjectType & OBS_TYPE_CONNECT) != 0)
                {
                    //  long nUBound = 0;
                    //  nUBound = clsBaseLineVectors.GetLength(0) + 1;
                    //  clsBaseLineVectors = new BaseLineVector[nUBound];
                    clsBaseLineVectors.Add((BaseLineVector)clsObservationPoint.Owner);
                }
            }
            else
            {
                /*----------------------------------------------------------------------
                '子観測点すべてを巡回する。
                Do While Not clsChildPoint Is Nothing
                    Call GetConnectBaseLineVectors(clsChildPoint, clsBaseLineVectors)
                    Set clsChildPoint = clsChildPoint.NextPoint
                Loop
                 ---------------------------------------------------------------------*/
                while (!(clsChildPoint == null))
                {
                    GetConnectBaseLineVectors(clsChildPoint, ref clsBaseLineVectors);
                    clsChildPoint = clsChildPoint.NextPoint();
                }

            }



            return;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '指定された観測点と連結している基線ベクトルを取得する。
        '
        'clsObservationPoint を始点、または終点としている基線ベクトルを取得する。
        'さらに、clsObservationPoint と同じ観測点番号である観測点に連結している基線ベクトルも取得する。
        '
        '引き数：
        'clsObservationPoint 対象とする観測点。
        'objBaseLineVectors clsObservationPoint と連結している基線ベクトルが設定される。要素は BaseLineVector オブジェクト。キーは要素のポインタ。
        Public Sub GetConnectBaseLineVectorsCollectionEx(ByVal clsObservationPoint As ObservationPoint, ByVal objBaseLineVectors As Collection)
            Set clsObservationPoint = clsObservationPoint.HeadPoint
            Do While Not clsObservationPoint Is Nothing
                Call GetConnectBaseLineVectorsCollection(clsObservationPoint, objBaseLineVectors)
                Set clsObservationPoint = clsObservationPoint.NextPoint
            Loop
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '指定された観測点と連結している基線ベクトルを取得する。
        '
        'clsObservationPoint を始点、または終点としている基線ベクトルを取得する。
        'さらに、clsObservationPoint と同じ観測点番号である観測点に連結している基線ベクトルも取得する。
        '
        '引き数：
        'clsObservationPoint 対象とする観測点。
        'objBaseLineVectors clsObservationPoint と連結している基線ベクトルが設定される。要素は BaseLineVector オブジェクト。キーは要素のポインタ。
        */
        public void GetConnectBaseLineVectorsCollectionEx(ObservationPoint clsObservationPoint, ICollection objBaseLineVectors)
        {
            return;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '指定された観測点と連結している基線ベクトルを取得する。
        '
        'clsObservationPoint を始点、または終点としている基線ベクトルを取得する。
        '
        '引き数：
        'clsObservationPoint 対象とする観測点。
        'objBaseLineVectors clsObservationPoint と連結している基線ベクトルが設定される。要素は BaseLineVector オブジェクト。キーは要素のポインタ。
        Public Sub GetConnectBaseLineVectorsCollection(ByVal clsObservationPoint As ObservationPoint, ByVal objBaseLineVectors As Collection)
            Dim clsChildPoint As ObservationPoint
            Set clsChildPoint = clsObservationPoint.ChildPoint
            If clsChildPoint Is Nothing Then
                '接合観測点の場合、基線ベクトルを追加する。
                If (clsObservationPoint.ObjectType And OBS_TYPE_CONNECT) <> 0 Then
                    Call SetAtCollectionObject(objBaseLineVectors, clsObservationPoint.Owner, Hex$(GetPointer(clsObservationPoint.Owner)))
                End If
            Else
                '子観測点すべてを巡回する。
                Do While Not clsChildPoint Is Nothing
                    Call GetConnectBaseLineVectorsCollection(clsChildPoint, objBaseLineVectors)
                    Set clsChildPoint = clsChildPoint.NextPoint
                Loop
            End If
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '指定された観測点と連結している基線ベクトルを取得する。
        '
        'clsObservationPoint を始点、または終点としている基線ベクトルを取得する。
        '
        '引き数：
        'clsObservationPoint 対象とする観測点。
        'objBaseLineVectors clsObservationPoint と連結している基線ベクトルが設定される。要素は BaseLineVector オブジェクト。キーは要素のポインタ。
        */
        public void GetConnectBaseLineVectorsCollection(ObservationPoint clsObservationPoint, ICollection objBaseLineVectors)
        {
            return;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '指定された観測点と連結している基線ベクトルの IsList を設定する。
        '
        'clsObservationPoint を始点、または終点としている基線ベクトルの IsList を設定する。
        'さらに、clsObservationPoint と同じ観測点番号である観測点に連結している基線ベクトルも設定する。
        '
        '引き数：
        'clsObservationPoint 対象とする観測点。
        Public Sub SetConnectBaseLineVectorsIsListEx(ByVal clsObservationPoint As ObservationPoint)
            Set clsObservationPoint = clsObservationPoint.HeadPoint
            Do While Not clsObservationPoint Is Nothing
                Call GetConnectBaseLineVectorsIsList(clsObservationPoint)
                Set clsObservationPoint = clsObservationPoint.NextPoint
            Loop
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]  //3 //6
        /*
        '指定された観測点と連結している基線ベクトルの IsList を設定する。
        '
        'clsObservationPoint を始点、または終点としている基線ベクトルの IsList を設定する。
        'さらに、clsObservationPoint と同じ観測点番号である観測点に連結している基線ベクトルも設定する。
        '
        '引き数：
        'clsObservationPoint 対象とする観測点。
        */
        public void SetConnectBaseLineVectorsIsListEx(ObservationPoint clsObservationPoint)   //2)
        {
            clsObservationPoint = clsObservationPoint.HeadPoint();          //6
            while (clsObservationPoint != null)                             //6
            {
                GetConnectBaseLineVectorsIsList(clsObservationPoint);       //6
                clsObservationPoint = clsObservationPoint.NextPoint();      //6
            }

#if false   //3 追加し、//6で下記を削除************************************
            ChainList clsChainList;
            clsChainList = m_clsMdlMain.GetDocument().NetworkModel().BaseLineVectorHead();
            List<BaseLineVector> clsBaseLineVectors = new List<BaseLineVector>();


            while (clsChainList != null)
            {
                GetConnectBaseLineVectorsIsList(clsObservationPoint);
                m_clsMdlMain.GetDocument().Session_GetConnectBaseLineVectors(clsObservationPoint, ref clsBaseLineVectors);

                clsChainList = clsChainList.NextList();
            }
#endif      //3 で変更********************************************************
        }
        /*
        public void SetConnectBaseLineVectorsIsListEx(ObservationPoint pclsObservationPoint)
        {
            ObservationPoint clsObservationPoint;

            clsObservationPoint = pclsObservationPoint.HeadPoint();

            while (clsObservationPoint == null)
            {
                GetConnectBaseLineVectorsIsList(clsObservationPoint);
                clsObservationPoint = clsObservationPoint.NextPoint();
            }

 
            return;
        }
        */
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '指定された観測点と連結している基線ベクトルの IsList を設定する。
        '
        'clsObservationPoint を始点、または終点としている基線ベクトルの IsList を設定する。
        '
        '引き数：
        'clsObservationPoint 対象とする観測点。
        Public Sub GetConnectBaseLineVectorsIsList(ByVal clsObservationPoint As ObservationPoint)
            Dim clsChildPoint As ObservationPoint
            Set clsChildPoint = clsObservationPoint.ChildPoint
            If clsChildPoint Is Nothing Then
                '接合観測点の場合、基線ベクトルに設定する。
                If (clsObservationPoint.ObjectType And OBS_TYPE_CONNECT) <> 0 Then
                    clsObservationPoint.Owner.IsList = True
                End If
            Else
                '子観測点すべてを巡回する。
                Do While Not clsChildPoint Is Nothing
                    Call GetConnectBaseLineVectorsIsList(clsChildPoint)
                    Set clsChildPoint = clsChildPoint.NextPoint
                Loop
            End If
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]  //6
        /*
        '指定された観測点と連結している基線ベクトルの IsList を設定する。
        '
        'clsObservationPoint を始点、または終点としている基線ベクトルの IsList を設定する。
        '
        '引き数：
        'clsObservationPoint 対象とする観測点。
        */
#if true
        public void GetConnectBaseLineVectorsIsList(ObservationPoint clsObservationPoint)
        {

            ObservationPoint clsChildPoint;
            clsChildPoint = clsObservationPoint.ChildPoint();
            //  List<BaseLineVector> clsBaseLineVectors = new List<BaseLineVector>();

            if (clsChildPoint == null)
            {
                /*-----------------------------------------------
                    '接合観測点の場合、基線ベクトルに設定する。
                    If (clsObservationPoint.ObjectType And OBS_TYPE_CONNECT) <> 0 Then
                        clsObservationPoint.Owner.IsList = True
                    End If
                 */
                //***********************************
                //'接合観測点の場合、基線ベクトルに設定する。
                //***********************************
                if ((clsObservationPoint.ObjectType & OBS_TYPE_CONNECT) != 0)
                {
                    ObservationPoint obj = (ObservationPoint)clsObservationPoint.Owner;
                    //obj.Element.IsList = true;
                }

            }
            else
            {
                /*------------------------------------------------
                    '子観測点すべてを巡回する。
                    Do While Not clsChildPoint Is Nothing
                        Call GetConnectBaseLineVectorsIsList(clsChildPoint)
                        Set clsChildPoint = clsChildPoint.NextPoint
                    Loop
                 */
                //***********************************
                // '子観測点すべてを巡回する。
                //***********************************
                while (clsObservationPoint != null)
                {
                    GetConnectBaseLineVectorsIsList(clsChildPoint);
                    clsChildPoint = clsChildPoint.NextPoint();
                }
            }
        }
#else
        //  m_clsMdlMain.GetDocument().Session_GetConnectBaseLineVectors(clsObservationPoint, ref clsBaseLineVectors);

        public void GetConnectBaseLineVectorsIsList(ObservationPoint clsObservationPoint)
        {
            ObservationPoint clsChildPoint;
            clsChildPoint = clsObservationPoint.ChildPoint();
            if (clsChildPoint == null)
            {
                //'接合観測点の場合、基線ベクトルに設定する。
                if ((clsObservationPoint.ObjectType & OBS_TYPE_CONNECT) != 0)
                {
                    //  clsObservationPoint.Owner.IsList(true);
                    clsObservationPoint.IsList(true);
                }
            }
            else
            {
                //'子観測点すべてを巡回する。
                while (clsChildPoint != null)
                {
                    GetConnectBaseLineVectorsIsList(clsChildPoint);
                    clsChildPoint = clsChildPoint.NextPoint();
                }
            }

            return;
        }
#endif
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '基線ベクトルチェインコレクションを作成する。
        '
        'm_clsBaseLineVectorHead リストの ChainList オブジェクトのコレクションを作成する。
        '先頭である m_clsBaseLineVectorHead オブジェクトは含まない。
        '
        '戻り値：作成した基線ベクトルチェインコレクションを返す。要素は ChainList オブジェクト。キーは BaseLineVector オブジェクトのポインタ。
        Public Function MakeBaseLineVectorChainCollection() As Collection
            Set MakeBaseLineVectorChainCollection = New Collection
            Dim clsChainList As ChainList
            Set clsChainList = m_clsBaseLineVectorHead.NextList
            Do While Not clsChainList Is Nothing
                Call MakeBaseLineVectorChainCollection.Add(clsChainList, Hex$(GetPointer(clsChainList.Element)))
                Set clsChainList = clsChainList.NextList
            Loop
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]  //5
        /// <summary>
        /// 基線ベクトルチェインコレクションを作成する。
        /// '
        /// m_clsBaseLineVectorHead リストの ChainList オブジェクトのコレクションを作成する。
        /// 先頭である m_clsBaseLineVectorHead オブジェクトは含まない。
        /// 
        /// </summary>
        /// <returns>
        /// 戻り値：
        /// 作成した基線ベクトルチェインコレクションを返す。要素は ChainList オブジェクト。キーは BaseLineVector オブジェクトのポインタ。
        /// </returns>
        public Dictionary<string, object> MakeBaseLineVectorChainCollection()       //5
        {
            Dictionary<string, object> MakeBaseLineVectorChainCollection = new Dictionary<string, object>();
            ChainList clsChainList;
            clsChainList = m_clsBaseLineVectorHead.NextList();
            while (clsChainList != null)
            {
                MakeBaseLineVectorChainCollection.Add(GetPointer(clsChainList.Element).ToString(), clsChainList);
                clsChainList = clsChainList.NextList();
            }
            return MakeBaseLineVectorChainCollection;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
            '孤立観測点チェインコレクションを作成する。
            '
            'm_clsIsolatePointHead リストの ChainList オブジェクトのコレクションを作成する。
            '先頭である m_clsIsolatePointHead オブジェクトは含まない。
            '
            '戻り値：作成した孤立観測点チェインコレクションを返す。要素は ChainList オブジェクト。キーは ObservationPoint オブジェクト(実観測点)のポインタ。
            Public Function MakeIsolatePointChainCollection() As Collection
                Set MakeIsolatePointChainCollection = New Collection
                Dim clsChainList As ChainList
                Set clsChainList = m_clsIsolatePointHead.NextList
                Do While Not clsChainList Is Nothing
                    Call MakeIsolatePointChainCollection.Add(clsChainList, Hex$(GetPointer(clsChainList.Element)))
                    Set clsChainList = clsChainList.NextList
                Loop
            End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]  //6
        /// <summary>
        /// 孤立観測点チェインコレクションを作成する。
        /// '
        /// m_clsIsolatePointHead リストの ChainList オブジェクトのコレクションを作成する。
        /// 先頭である m_clsIsolatePointHead オブジェクトは含まない。
        /// 
        /// </summary>
        /// <returns>
        /// 戻り値：作成した孤立観測点チェインコレクションを返す。要素は ChainList オブジェクト。キーは ObservationPoint オブジェクト(実観測点)のポインタ。
        /// </returns>
        public Dictionary<string, object> MakeIsolatePointChainCollection()
        {
            Dictionary<string, object> MakeIsolatePointChainCollection = new Dictionary<string, object>();
            ChainList clsChainList;
            clsChainList = m_clsIsolatePointHead.NextList();
            while (clsChainList != null)
            {
                MakeIsolatePointChainCollection.Add(GetPointer(clsChainList.Element).ToString(), clsChainList);
                clsChainList = clsChainList.NextList();
            }

            return MakeIsolatePointChainCollection;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '代表観測点を削除する。
        '
        '引き数：
        'clsRepresentPoint 対象とする代表観測点。
        'sObsPointPath 観測点フォルダのパス。
        'objIsolatePointChainCollection 孤立観測点チェインコレクション。要素は ChainList オブジェクト。キーは ObservationPoint オブジェクト(実観測点)のポインタ。
        'objBaseLineVectorChainCollection 基線ベクトルチェインコレクション。要素は ChainList オブジェクト。キーは BaseLineVector オブジェクトのポインタ。
        Public Sub RemoveRepresentPoint(ByVal clsRepresentPoint As ObservationPoint, ByVal sObsPointPath As String, ByVal objIsolatePointChainCollection As Collection, ByVal objBaseLineVectorChainCollection As Collection)

            '代表観測点リストから削除する。
            Call clsRepresentPoint.Owner.Remove
            '代表観測点を分離。
            Call clsRepresentPoint.LeaveParentPoint
    
            Dim clsConnectPoint As ObservationPoint
            Set clsConnectPoint = clsRepresentPoint.ChildPoint.ChildPoint
            If clsConnectPoint Is Nothing Then
                '孤立観測点であった場合、孤立観測点リストから削除する。
                Dim sKey As String
                sKey = Hex$(GetPointer(clsRepresentPoint.ChildPoint))
                Dim clsChainList As ChainList
                If LookupCollectionObject(objIsolatePointChainCollection, clsChainList, sKey) Then
                    Call objIsolatePointChainCollection.Remove(sKey)
                    Call clsChainList.Remove
                End If
            Else
                '基線ベクトルを削除する。
                Do While Not clsConnectPoint Is Nothing
                    Dim clsBaseLineVector As BaseLineVector
                    Set clsBaseLineVector = clsConnectPoint.Owner
                    '反対側の接合観測点。
                    Dim clsBuddyPoint As ObservationPoint
                    If clsBaseLineVector.StrPoint Is clsConnectPoint Then
                        Set clsBuddyPoint = clsBaseLineVector.EndPoint
                    Else
                        Set clsBuddyPoint = clsBaseLineVector.StrPoint
                    End If
                    '基線ベクトルを削除する。
                    sKey = Hex$(GetPointer(clsBaseLineVector))
                    If LookupCollectionObject(objBaseLineVectorChainCollection, clsChainList, sKey) Then
                        Call objBaseLineVectorChainCollection.Remove(sKey)
                        Call clsChainList.Remove
                    End If
                    Call clsBaseLineVector.Terminate
                    '反対側の接合観測点を削除する。
                    Dim clsBuddyParent As ObservationPoint
                    Set clsBuddyParent = clsBuddyPoint.ParentPoint
                    Call clsBuddyPoint.LeaveParentPoint
                    Call clsBuddyPoint.Terminate
                    '反対側の観測点が孤立したら孤立観測点リストに追加する。
                    If clsBuddyParent.ChildPoint Is Nothing Then
                        Set clsChainList = m_clsIsolatePointHead.TailList.InsertNext(clsBuddyParent)
                        Call objIsolatePointChainCollection.Add(clsChainList, Hex$(GetPointer(clsBuddyParent)))
                    End If
                    Set clsConnectPoint = clsConnectPoint.NextPoint
                Loop
            End If
    
            '観測点ファイルを削除する。
        On Error Resume Next
            If Not clsRepresentPoint.Genuine Then
                Call RemoveFile(sObsPointPath & clsRepresentPoint.ChildPoint.FileTitle & "." & clsRepresentPoint.ChildPoint.RinexExt & RNX_OBS_EXTENSION)
                Call RemoveFile(sObsPointPath & clsRepresentPoint.ChildPoint.FileTitle & "." & clsRepresentPoint.ChildPoint.RinexExt & RNX_NAV_EXTENSION)
                Call RemoveFile(sObsPointPath & clsRepresentPoint.ChildPoint.FileTitle & "." & clsRepresentPoint.ChildPoint.RinexExt & RNX_GLO_EXTENSION)
                Call RemoveFile(sObsPointPath & clsRepresentPoint.ChildPoint.FileTitle & "." & RNX_SV_EXTENSION)
                '2017/07/05 NS6000対応。'''''''''''''''''''''''''''''''''''''''''''''''''''''''
                Call RemoveFile(sObsPointPath & clsRepresentPoint.ChildPoint.FileTitle & "." & clsRepresentPoint.ChildPoint.RinexExt & RNX_QZS_EXTENSION)
                Call RemoveFile(sObsPointPath & clsRepresentPoint.ChildPoint.FileTitle & "." & clsRepresentPoint.ChildPoint.RinexExt & RNX_GAL_EXTENSION)
                Call RemoveFile(sObsPointPath & clsRepresentPoint.ChildPoint.FileTitle & "." & clsRepresentPoint.ChildPoint.RinexExt & RNX_BEI_EXTENSION)
                Call RemoveFile(sObsPointPath & clsRepresentPoint.ChildPoint.FileTitle & "." & clsRepresentPoint.ChildPoint.RinexExt & RNX_MIX_EXTENSION)
                '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            End If
        On Error GoTo 0
    
            '観測点を削除する。
            Call clsRepresentPoint.Terminate
    
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '代表観測点を削除する。
        '
        '引き数：
        'clsRepresentPoint 対象とする代表観測点。
        'sObsPointPath 観測点フォルダのパス。
        'objIsolatePointChainCollection 孤立観測点チェインコレクション。要素は ChainList オブジェクト。キーは ObservationPoint オブジェクト(実観測点)のポインタ。
        'objBaseLineVectorChainCollection 基線ベクトルチェインコレクション。要素は ChainList オブジェクト。キーは BaseLineVector オブジェクトのポインタ。
        */
        public void RemoveRepresentPoint(ObservationPoint clsRepresentPoint, string sObsPointPath, Dictionary<string, object> objIsolatePointChainCollection, Dictionary<string, object> objBaseLineVectorChainCollection)
        {

            /*------------------------------------------------------------------
                '代表観測点リストから削除する。
                Call clsRepresentPoint.Owner.Remove
                '代表観測点を分離。
                Call clsRepresentPoint.LeaveParentPoint
             */
            //'代表観測点リストから削除する。
            clsRepresentPoint.Remove();
            //'代表観測点を分離。
            clsRepresentPoint.LeaveParentPoint();


            /*------------------------------------------------------------------
                Dim clsConnectPoint As ObservationPoint
                Set clsConnectPoint = clsRepresentPoint.ChildPoint.ChildPoint
                If clsConnectPoint Is Nothing Then
             */
            ObservationPoint clsConnectPoint;
            clsConnectPoint = clsRepresentPoint.ChildPoint().ChildPoint();
            if (clsConnectPoint == null)
            {
                /*-----------------------------------------------------------
                    '孤立観測点であった場合、孤立観測点リストから削除する。
                    Dim sKey As String
                    sKey = Hex$(GetPointer(clsRepresentPoint.ChildPoint))
                    Dim clsChainList As ChainList
                    If LookupCollectionObject(objIsolatePointChainCollection, clsChainList, sKey) Then
                        Call objIsolatePointChainCollection.Remove(sKey)
                        Call clsChainList.Remove
                    End If
                 */
                string sKey;
                sKey = GetPointer(clsRepresentPoint.ChildPoint()).ToString();
                ChainList clsChainList;
                object oChainList = new object();
                if (LookupCollectionObject(objIsolatePointChainCollection, ref oChainList, sKey))
                {
                    _ = objIsolatePointChainCollection.Remove(sKey);

                    clsChainList = (ChainList)oChainList;
                    clsChainList.Remove();

                }
            }
            else
            {
                /*---------------------------------------------------------------------------
                    '基線ベクトルを削除する。
                    Do While Not clsConnectPoint Is Nothing
                        Dim clsBaseLineVector As BaseLineVector
                        Set clsBaseLineVector = clsConnectPoint.Owner
                        '反対側の接合観測点。
                        Dim clsBuddyPoint As ObservationPoint
                        If clsBaseLineVector.StrPoint Is clsConnectPoint Then
                            Set clsBuddyPoint = clsBaseLineVector.EndPoint
                        Else
                            Set clsBuddyPoint = clsBaseLineVector.StrPoint
                        End If
                    :   :   :   :   :   :   :
                    :   :   :   :   :   :   :
                    Loop
                 */

                //'基線ベクトルを削除する。
                while (clsConnectPoint != null)
                {
                    BaseLineVector clsBaseLineVector;
                    clsBaseLineVector = (BaseLineVector)clsConnectPoint.Owner;
                    /*-------------------------------------------------------------
                        '反対側の接合観測点。
                        Dim clsBuddyPoint As ObservationPoint
                        If clsBaseLineVector.StrPoint Is clsConnectPoint Then
                            Set clsBuddyPoint = clsBaseLineVector.EndPoint
                        Else
                            Set clsBuddyPoint = clsBaseLineVector.StrPoint
                        End If
                     */
                    //'反対側の接合観測点。
                    ObservationPoint clsBuddyPoint;
                    //---------------------------------------------------------

                    //  clsBuddyPoint = clsBaseLineVector.EndPoint();
#if false
                    if (ReferenceEquals(clsBaseLineVector.StrPoint(), clsConnectPoint))
                    {
                        clsBuddyPoint = clsBaseLineVector.EndPoint();
                    }
                    else
                    {
                        clsBuddyPoint = clsBaseLineVector.StrPoint();

                    }
#else   //-------------

                    if (clsBaseLineVector.StrPoint() == clsConnectPoint)
                    {
                        clsBuddyPoint = clsBaseLineVector.EndPoint();
                    }
                   else
                    {
                        clsBuddyPoint = clsBaseLineVector.StrPoint();
                    }
#endif
                    /*----------------------------------------------------------------
                        '基線ベクトルを削除する。
                        sKey = Hex$(GetPointer(clsBaseLineVector))
                        If LookupCollectionObject(objBaseLineVectorChainCollection, clsChainList, sKey) Then
                            Call objBaseLineVectorChainCollection.Remove(sKey)
                            Call clsChainList.Remove
                        End If
                        Call clsBaseLineVector.Terminate
                     */

                    //'基線ベクトルを削除する。
                    object objChainList = new object();

                    ChainList clsChainList; //5
                    string sKey = GetPointer(clsBaseLineVector).ToString();
                    if (LookupCollectionObject(objBaseLineVectorChainCollection, ref objChainList, sKey))
                    {

                        clsChainList = (ChainList)objChainList;

                        _ = objBaseLineVectorChainCollection.Remove(sKey);
                        clsChainList.Remove();

                    }
                    clsBaseLineVector.Terminate();

                    /*-------------------------------------------------------------------------------
                        '反対側の接合観測点を削除する。
                        Dim clsBuddyParent As ObservationPoint
                        Set clsBuddyParent = clsBuddyPoint.ParentPoint
                        Call clsBuddyPoint.LeaveParentPoint
                        Call clsBuddyPoint.Terminate
                        '反対側の観測点が孤立したら孤立観測点リストに追加する。
                        If clsBuddyParent.ChildPoint Is Nothing Then
                            Set clsChainList = m_clsIsolatePointHead.TailList.InsertNext(clsBuddyParent)
                            Call objIsolatePointChainCollection.Add(clsChainList, Hex$(GetPointer(clsBuddyParent)))
                        End If
                        Set clsConnectPoint = clsConnectPoint.NextPoint
                     */

                    //反対側の接合観測点を削除する。
                    ObservationPoint clsBuddyParent;
                    clsBuddyParent = clsBuddyPoint.ParentPoint();
                    clsBuddyPoint.LeaveParentPoint();
                    clsBuddyPoint.Terminate();
                    //'反対側の観測点が孤立したら孤立観測点リストに追加する。
                    if (clsBuddyParent.ChildPoint() != null)
                    {
                        clsChainList = m_clsIsolatePointHead.TailList().InsertNext(clsBuddyParent);
                        objIsolatePointChainCollection.Add(GetPointer(clsBuddyParent).ToString(), clsChainList);
                    }
                    clsConnectPoint = clsConnectPoint.NextPoint();


                }//while (clsConnectPoint != null)


            }

            /*-------------------------------------------------------------------------------
                '観測点ファイルを削除する。
            On Error Resume Next
                If Not clsRepresentPoint.Genuine Then
                    Call RemoveFile(sObsPointPath & clsRepresentPoint.ChildPoint.FileTitle & "." & clsRepresentPoint.ChildPoint.RinexExt & RNX_OBS_EXTENSION)
                    Call RemoveFile(sObsPointPath & clsRepresentPoint.ChildPoint.FileTitle & "." & clsRepresentPoint.ChildPoint.RinexExt & RNX_NAV_EXTENSION)
                    Call RemoveFile(sObsPointPath & clsRepresentPoint.ChildPoint.FileTitle & "." & clsRepresentPoint.ChildPoint.RinexExt & RNX_GLO_EXTENSION)
                    Call RemoveFile(sObsPointPath & clsRepresentPoint.ChildPoint.FileTitle & "." & RNX_SV_EXTENSION)
                    '2017/07/05 NS6000対応。'''''''''''''''''''''''''''''''''''''''''''''''''''''''
                    Call RemoveFile(sObsPointPath & clsRepresentPoint.ChildPoint.FileTitle & "." & clsRepresentPoint.ChildPoint.RinexExt & RNX_QZS_EXTENSION)
                    Call RemoveFile(sObsPointPath & clsRepresentPoint.ChildPoint.FileTitle & "." & clsRepresentPoint.ChildPoint.RinexExt & RNX_GAL_EXTENSION)
                    Call RemoveFile(sObsPointPath & clsRepresentPoint.ChildPoint.FileTitle & "." & clsRepresentPoint.ChildPoint.RinexExt & RNX_BEI_EXTENSION)
                    Call RemoveFile(sObsPointPath & clsRepresentPoint.ChildPoint.FileTitle & "." & clsRepresentPoint.ChildPoint.RinexExt & RNX_MIX_EXTENSION)
                    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                End If
            On Error GoTo 0
    
                '観測点を削除する。
                Call clsRepresentPoint.Terminate
             */

            //'観測点ファイルを削除する。
            try
            {
                _ = RemoveFile(sObsPointPath + clsRepresentPoint.ChildPoint().FileTitle() + "." + clsRepresentPoint.ChildPoint().RinexExt() + RNX_OBS_EXTENSION);
            }
            catch (Exception)
            {
            }

            try
            {
                _ = RemoveFile(sObsPointPath + clsRepresentPoint.ChildPoint().FileTitle() + "." + clsRepresentPoint.ChildPoint().RinexExt() + RNX_OBS_EXTENSION);
            }
            catch (Exception)
            {
            }

            try
            {
                _ = RemoveFile(sObsPointPath + clsRepresentPoint.ChildPoint().FileTitle() + "." + clsRepresentPoint.ChildPoint().RinexExt() + RNX_GLO_EXTENSION);
            }
            catch (Exception)
            {
            }

            try
            {
                _ = RemoveFile(sObsPointPath + clsRepresentPoint.ChildPoint().FileTitle() + "." + clsRepresentPoint.ChildPoint().RinexExt() + RNX_SV_EXTENSION);
            }
            catch (Exception)
            {
            }
            //'2017/07/05 NS6000対応。'''''''''''''''''''''''''''''''''''''''''''''''''''''''
            try
            {
                _ = RemoveFile(sObsPointPath + clsRepresentPoint.ChildPoint().FileTitle() + "." + clsRepresentPoint.ChildPoint().RinexExt() + RNX_QZS_EXTENSION);
            }
            catch (Exception)
            {
            }

            try
            {
                _ = RemoveFile(sObsPointPath + clsRepresentPoint.ChildPoint().FileTitle() + "." + clsRepresentPoint.ChildPoint().RinexExt() + RNX_GAL_EXTENSION);
            }
            catch (Exception)
            {
            }

            try
            {
                _ = RemoveFile(sObsPointPath + clsRepresentPoint.ChildPoint().FileTitle() + "." + clsRepresentPoint.ChildPoint().RinexExt() + RNX_BEI_EXTENSION);
            }
            catch (Exception)
            {
            }

            try
            {
                _ = RemoveFile(sObsPointPath + clsRepresentPoint.ChildPoint().FileTitle() + "." + clsRepresentPoint.ChildPoint().RinexExt() + RNX_MIX_EXTENSION);
            }
            catch (Exception)
            {
            }

            //'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

            try
            {
                //'観測点を削除する。
                clsRepresentPoint.Terminate();
            }
            catch (Exception)
            {
            }

            return;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '偏心補正を行なう。
        '
        '引き数：
        'clsObservationPoint 対象とする観測点。
        Public Sub CorrectEccentric(ByVal clsObservationPoint As ObservationPoint)

            Dim clsBaseLineVector As BaseLineVector
            '2009/11 H.Nakamura
        '    If clsObservationPoint.EccentricCorrectionParam.AngleType = ANGLETYPE_HORIZONTAL Then
        '        '方位標ベクトルを取得する。
        '        Set clsBaseLineVector = GetMarkerBaseLineVector(clsObservationPoint, clsObservationPoint.EccentricCorrectionParam)
        '        If clsBaseLineVector Is Nothing Then Exit Sub
        '    End If
            '方位標ベクトル、もしくは偏心に使用する基線を取得する。
            Set clsBaseLineVector = GetMarkerBaseLineVector(clsObservationPoint, clsObservationPoint.EccentricCorrectionParam)
            If clsBaseLineVector Is Nothing Then Exit Sub
    
            '偏心補正計算。
            Dim ⊿XYZ() As Double
            Call mdlEccentricCorrection.CorrectEccentric(clsObservationPoint, clsBaseLineVector, clsObservationPoint.EccentricCorrectionParam, ⊿XYZ)
            clsObservationPoint.VectorEccentric.X = ⊿XYZ(0, 0)
            clsObservationPoint.VectorEccentric.Y = ⊿XYZ(1, 0)
            clsObservationPoint.VectorEccentric.Z = ⊿XYZ(2, 0)
    
            '本点を生成する。
            If Not clsObservationPoint.Eccentric Then Call CreateGenuinePoint(clsObservationPoint)
    
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '偏心補正を行なう。
        '
        '引き数：
        'clsObservationPoint 対象とする観測点。
        */
        public void CorrectEccentric(ObservationPoint clsObservationPoint)
        {
            return;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '方位標、もしくは偏心に使用する基線を取得する。
        '
        '引き数：
        'clsObservationPoint 対象とする偏心点(代表観測点)。
        'clsEccentricCorrectionParam 偏心補正パラメータ。
        '
        '戻り値：方位標となる基線ベクトルを返す。
        Public Function GetMarkerBaseLineVector(ByVal clsObservationPoint As ObservationPoint, ByVal clsEccentricCorrectionParam As EccentricCorrectionParam) As BaseLineVector

            Dim clsBaseLineVector As BaseLineVector
    
            '2009/11 H.Nakamura
        '    If clsEccentricCorrectionParam.AngleType = ANGLETYPE_HORIZONTAL Then
        '        Dim clsBaseLineVectors() As BaseLineVector
        '        ReDim clsBaseLineVectors(-1 To -1)
        '        Call GetConnectBaseLineVectorsEx(clsObservationPoint, clsBaseLineVectors)
        '        Dim i As Long
        '        For i = 0 To UBound(clsBaseLineVectors)
        '            If clsBaseLineVectors(i).Key = clsEccentricCorrectionParam.Marker Then
        '                Set clsBaseLineVector = clsBaseLineVectors(i)
        '                Exit For
        '            End If
        '        Next
        '    End If
            Dim sKey As String
            If clsEccentricCorrectionParam.AngleType = ANGLETYPE_HORIZONTAL Then
                sKey = clsEccentricCorrectionParam.Marker
            Else
                sKey = clsEccentricCorrectionParam.UsePoint
            End If
    
            Dim clsBaseLineVectors() As BaseLineVector
            ReDim clsBaseLineVectors(-1 To -1)
            Call GetConnectBaseLineVectorsEx(clsObservationPoint, clsBaseLineVectors)
            Dim i As Long
            For i = 0 To UBound(clsBaseLineVectors)
                If clsBaseLineVectors(i).Key = sKey Then
                    Set clsBaseLineVector = clsBaseLineVectors(i)
                    Exit For
                End If
            Next
    
            Set GetMarkerBaseLineVector = clsBaseLineVector
    
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '方位標、もしくは偏心に使用する基線を取得する。
        '
        '引き数：
        'clsObservationPoint 対象とする偏心点(代表観測点)。
        'clsEccentricCorrectionParam 偏心補正パラメータ。
        '
        '戻り値：方位標となる基線ベクトルを返す。
        */
        public BaseLineVector GetMarkerBaseLineVector(ObservationPoint clsObservationPoint, EccentricCorrectionParam clsEccentricCorrectionParam)
        {
            return null;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '本点の有効/無効を更新する。
        '
        'clsEccentricPoint で指定される偏心点の本点の有効/無効を評価し、設定する。
        '
        '引き数：
        'clsEccentricPoint 対象とする偏心点(代表観測点)。
        Public Sub EnableGenuinePoint(ByVal clsEccentricPoint As ObservationPoint)

            If Not clsEccentricPoint.Eccentric Then Exit Sub
    
            Dim bEnable As Boolean
    
            '筆頭観測点以下、すべての偏心点が無効なら本点も無効。
            Dim clsObservationPoint As ObservationPoint
            Set clsObservationPoint = clsEccentricPoint.HeadPoint
            Do While Not clsObservationPoint Is Nothing
                If clsObservationPoint.Enable Then Exit Do
                Set clsObservationPoint = clsObservationPoint.NextPoint
            Loop
            bEnable = Not clsObservationPoint Is Nothing
    
            Dim clsBaseLineVectors() As BaseLineVector
            ReDim clsBaseLineVectors(-1 To -1)
            Call GetConnectBaseLineVectorsEx(clsEccentricPoint, clsBaseLineVectors)
    
            If bEnable And clsEccentricPoint.EccentricCorrectionParam.AngleType = ANGLETYPE_HORIZONTAL Then
                '方位標の評価。
                Dim i As Long
                For i = 0 To UBound(clsBaseLineVectors)
                    If clsBaseLineVectors(i).Key = clsEccentricPoint.EccentricCorrectionParam.Marker Then
                        '方位標が無効なら本点も無効。
                        bEnable = bEnable And clsBaseLineVectors(i).Enable
                        '方位標が未解析なら本点は無効。
                        bEnable = bEnable And clsBaseLineVectors(i).Analysis <= ANALYSIS_STATUS_FIX
                        Exit For
                    End If
                Next
                '方位標が削除されていたら本点は無効。
                If i > UBound(clsBaseLineVectors) Then bEnable = False
            End If
    
            '本点の有効/無効を設定する。
            Set clsObservationPoint = clsEccentricPoint.CorrectPoint.TopParentPoint
            clsObservationPoint.Enable = bEnable
    
            '偏心点に繋がっている基線ベクトルのIsListを設定する。
            For i = 0 To UBound(clsBaseLineVectors)
                clsBaseLineVectors(i).IsList = True
            Next
    
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '本点の有効/無効を更新する。
        '
        'clsEccentricPoint で指定される偏心点の本点の有効/無効を評価し、設定する。
        '
        '引き数：
        'clsEccentricPoint 対象とする偏心点(代表観測点)。
        */
        public void EnableGenuinePoint(ObservationPoint clsEccentricPoint)
        {
            return;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '指定された基線ベクトルを方位標としている、もしくは偏心に使用している偏心点を取得する。
        '
        '引き数：
        'clsBaseLineVectors 方位標ベクトル。参照する配列の要素は(0 To ...)。Ubound 関数を使用する。
        '
        '戻り値：偏心点(接合観測点)を返す。
        Public Function GetMarkedEccentricPoints(ByRef clsBaseLineVectors() As BaseLineVector) As Collection
            Dim objEccentricPoints As New Collection
            Dim i As Long
            For i = 0 To UBound(clsBaseLineVectors)
                Call GetMarkedEccentricPoints2(clsBaseLineVectors(i), objEccentricPoints)
            Next
            Set GetMarkedEccentricPoints = objEccentricPoints
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '指定された基線ベクトルを方位標としている、もしくは偏心に使用している偏心点を取得する。
        '
        '引き数：
        'clsBaseLineVectors 方位標ベクトル。参照する配列の要素は(0 To ...)。Ubound 関数を使用する。
        '
        '戻り値：偏心点(接合観測点)を返す。
        */
        public object GetMarkedEccentricPoints(List<BaseLineVector> clsBaseLineVectors)
        {
            object objEccentricPoints;

            int i = 0;
            //  GetMarkedEccentricPoints2(clsBaseLineVectors[i], objEccentricPoints);



            return null;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '指定された基線ベクトルを方位標としている、もしくは偏心に使用している偏心点を取得する。
        '
        '引き数：
        'clsBaseLineVector 方位標ベクトル。
        'objEccentricPoints 取得された偏心点(接合観測点)が設定される。
        Public Sub GetMarkedEccentricPoints2(ByRef clsBaseLineVector As BaseLineVector, ByVal objEccentricPoints As Collection)
            If clsBaseLineVector.StrPoint.Eccentric Then
                If clsBaseLineVector.StrPoint.EccentricCorrectionParam.AngleType = ANGLETYPE_HORIZONTAL Then
                    If clsBaseLineVector.StrPoint.EccentricCorrectionParam.Marker = clsBaseLineVector.Key Then
                        Call objEccentricPoints.Add(clsBaseLineVector.StrPoint)
                    End If
                '偏心に使用している基線も取得する。2009/11 H.Nakamura
                Else
                    If clsBaseLineVector.StrPoint.EccentricCorrectionParam.UsePoint = clsBaseLineVector.Key Then
                        Call objEccentricPoints.Add(clsBaseLineVector.StrPoint)
                    End If
                End If
            End If
            If clsBaseLineVector.EndPoint.Eccentric Then
                If clsBaseLineVector.EndPoint.EccentricCorrectionParam.AngleType = ANGLETYPE_HORIZONTAL Then
                    If clsBaseLineVector.EndPoint.EccentricCorrectionParam.Marker = clsBaseLineVector.Key Then
                        Call objEccentricPoints.Add(clsBaseLineVector.EndPoint)
                    End If
                '偏心に使用している基線も取得する。2009/11 H.Nakamura
                Else
                    If clsBaseLineVector.EndPoint.EccentricCorrectionParam.UsePoint = clsBaseLineVector.Key Then
                        Call objEccentricPoints.Add(clsBaseLineVector.EndPoint)
                    End If
                End If
            End If
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '指定された基線ベクトルを方位標としている、もしくは偏心に使用している偏心点を取得する。
        '
        '引き数：
        'clsBaseLineVector 方位標ベクトル。
        'objEccentricPoints 取得された偏心点(接合観測点)が設定される。
        */
        public void GetMarkedEccentricPoints2(BaseLineVector clsBaseLineVector, object objEccentricPoints)
        {
            return;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '解析結果を削除する。
        '
        '引き数：
        'objBaseLineVectors 対象とする基線ベクトル。要素は BaseLineVector オブジェクト。キーは任意。
        Public Sub DeleteAnalysis(ByVal objBaseLineVectors As Collection)
            Dim clsBaseLineVector As BaseLineVector
            For Each clsBaseLineVector In objBaseLineVectors
                clsBaseLineVector.Analysis = ANALYSIS_STATUS_NOT
            Next
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]  //5
        /*
        '解析結果を削除する。
        '
        '引き数：
        'objBaseLineVectors 対象とする基線ベクトル。要素は BaseLineVector オブジェクト。キーは任意。
        */
        public void DeleteAnalysis(Dictionary<string, object> objBaseLineVectors)
        {
            BaseLineVector clsBaseLineVector;

            foreach (BaseLineVector obj in objBaseLineVectors.Values.Cast<BaseLineVector>())
            {
                clsBaseLineVector = obj;
                clsBaseLineVector.Analysis = ANALYSIS_STATUS.ANALYSIS_STATUS_NOT;
            }

            return;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '既知点の座標候補を取得する。
        '
        '次の優先順位で決定する。
        '①有効な固定点の座標。
        '②無効な固定点の座標。
        '③有効な移動点の座標。
        '④無効な移動点の座標。
        '
        '戻り値：既知点の座標候補。
        Public Function GetKnownPointCoordinate() As CoordinatePoint
            Dim clsCoordinatePoints(3) As CoordinatePoint
            Dim clsChainList As ChainList
            Set clsChainList = m_clsRepresentPointHead.NextList
            Do While Not clsChainList Is Nothing
                Dim clsObservationPoint As ObservationPoint
                Set clsObservationPoint = clsChainList.Element
                If clsObservationPoint.Fixed And clsObservationPoint.Enable And clsCoordinatePoints(0) Is Nothing Then
                    Set clsCoordinatePoints(0) = clsObservationPoint.CoordinateFixed
                    Exit Do
                ElseIf clsObservationPoint.Fixed And Not clsObservationPoint.Enable And clsCoordinatePoints(1) Is Nothing Then
                    Set clsCoordinatePoints(1) = clsObservationPoint.CoordinateFixed
                ElseIf Not clsObservationPoint.Fixed And Not clsObservationPoint.Genuine And clsObservationPoint.Enable And clsCoordinatePoints(2) Is Nothing Then
                    Set clsCoordinatePoints(2) = clsObservationPoint.CoordinateObservation
                ElseIf Not clsObservationPoint.Fixed And Not clsObservationPoint.Genuine And Not clsObservationPoint.Enable And clsCoordinatePoints(3) Is Nothing Then
                    Set clsCoordinatePoints(3) = clsObservationPoint.CoordinateObservation
                End If
                Set clsChainList = clsChainList.NextList
            Loop
            Dim clsCoordinatePoint As CoordinatePoint
            Dim i As Long
            For i = 0 To 3
                If Not clsCoordinatePoints(i) Is Nothing Then
                    Set clsCoordinatePoint = clsCoordinatePoints(i)
                    Exit For
                End If
            Next
            Set GetKnownPointCoordinate = clsCoordinatePoint
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '既知点の座標候補を取得する。
        '
        '次の優先順位で決定する。
        '①有効な固定点の座標。
        '②無効な固定点の座標。
        '③有効な移動点の座標。
        '④無効な移動点の座標。
        '
        '戻り値：既知点の座標候補。
        */
        public CoordinatePoint GetKnownPointCoordinate()
        {
            return null;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '*******************************************************************************
        'インプリメンテーション

        '代表観測点を生成する。
        '
        'clsChildPoint で指定される実観測点の代表観測点を生成する。
        '
        '引き数：
        'clsChildPoint 実観測点。
        '
        '戻り値：生成した ObservationPoint オブジェクト(代表観測点)を返す。
        Private Function CreateRepresentPoint(ByVal clsChildPoint As ObservationPoint) As ObservationPoint
            Set CreateRepresentPoint = CreateParentPoint(clsChildPoint)
            CreateRepresentPoint.ObjectType = CreateRepresentPoint.ObjectType Or OBS_TYPE_REPRESENT
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '*******************************************************************************
        'インプリメンテーション
        
        '代表観測点を生成する。
        '
        'clsChildPoint で指定される実観測点の代表観測点を生成する。
        '
        '引き数：
        'clsChildPoint 実観測点。
        '
        '戻り値：生成した ObservationPoint オブジェクト(代表観測点)を返す。
        */
        private ObservationPoint CreateRepresentPoint(ObservationPoint clsChildPoint)
        {
            return null;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '親観測点を生成する。
        '
        'clsChildPoint で指定される観測点の親観測点を生成する。
        '
        '引き数：
        'clsChildPoint 子観測点。
        '
        '戻り値：生成した ObservationPoint オブジェクトを返す。
        Private Function CreateParentPoint(ByVal clsChildPoint As ObservationPoint) As ObservationPoint
            Set CreateParentPoint = New ObservationPoint
            CreateParentPoint.ObjectType = OBJ_TYPE_OBSERVATIONPOINT Or OBS_TYPE_PARENT
            Set CreateParentPoint.Attributes = clsChildPoint.Attributes
            Call CreateParentPoint.AddChildPoint(clsChildPoint)
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '親観測点を生成する。
        '
        'clsChildPoint で指定される観測点の親観測点を生成する。
        '
        '引き数：
        'clsChildPoint 子観測点。
        '
        '戻り値：生成した ObservationPoint オブジェクトを返す。
        */
        private ObservationPoint CreateParentPoint(ObservationPoint clsChildPoint)
        {
            ObservationPoint CreateParentPoint = new ObservationPoint(mdlMain);
            CreateParentPoint.ObjectType = OBJ_TYPE_OBSERVATIONPOINT | OBS_TYPE_PARENT;
            CreateParentPoint.Attributes(clsChildPoint.Attributes());
            CreateParentPoint.AddChildPoint(clsChildPoint);
            return CreateParentPoint;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '既存の代表観測点に新しい代表観測点を繋げる。
        '
        'clsDstRepresentPoint で指定される観測点に clsNewRepresentPoint で指定される観測点を繋げる。
        'clsDstRepresentPoint の末弟の後ろに clsNewRepresentPoint を繋げる。
        'nStyle が 0 以外であれば距離の評価を行う。
        '距離が開きすぎている場合は警告メッセージを表示する。
        '
        '引き数：
        'clsDstRepresentPoint 繋ぎ先の代表観測点。
        'clsNewRepresentPoint 繋ぎ元の代表観測点。
        'nStyle メッセージスタイル。0=メッセージ無し。1=OKボタンのみ。2=OKボタンとすべてOKボタン。
        'nResult メッセージボックスの結果が設定される。
        Private Sub ChainRepresentPoint(ByVal clsDstRepresentPoint As ObservationPoint, ByVal clsNewRepresentPoint As ObservationPoint, ByVal nStyle As Long, ByRef nResult As Long)

            '代表観測点との距離を評価する。
            If nStyle <> 0 Then
                Dim nDistance As Double
                Dim nMax As Long
                nDistance = GetDistanceRound(clsDstRepresentPoint.CoordinateDisplay, clsNewRepresentPoint.CoordinateDisplay)
                nMax = GetPrivateProfileInt(PROFILE_DEF_SEC_IMPORT, PROFILE_DEF_KEY_DISTANCE, PROFILE_DEF_DEF_DISTANCE, App.Path & "\" & PROFILE_DEF_NAME)
                If nDistance > nMax * 0.01 Then RaiseEvent CombinedDistanceViolation(clsDstRepresentPoint, nDistance, nMax, nStyle, nResult)
            End If
    
            '繋げる。
            Call clsDstRepresentPoint.AddNextPoint(clsNewRepresentPoint)
            Call ShareAttributesNext(clsNewRepresentPoint, clsDstRepresentPoint.Attributes.Common)
    
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '既存の代表観測点に新しい代表観測点を繋げる。
        '
        'clsDstRepresentPoint で指定される観測点に clsNewRepresentPoint で指定される観測点を繋げる。
        'clsDstRepresentPoint の末弟の後ろに clsNewRepresentPoint を繋げる。
        'nStyle が 0 以外であれば距離の評価を行う。
        '距離が開きすぎている場合は警告メッセージを表示する。
        '
        '引き数：
        'clsDstRepresentPoint 繋ぎ先の代表観測点。
        'clsNewRepresentPoint 繋ぎ元の代表観測点。
        'nStyle メッセージスタイル。0=メッセージ無し。1=OKボタンのみ。2=OKボタンとすべてOKボタン。
        'nResult メッセージボックスの結果が設定される。
        */
        private void ChainRepresentPoint(ObservationPoint clsDstRepresentPoint, ObservationPoint clsNewRepresentPoint, long nStyle, long nResult)
        {
            return;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '代表観測点リストへ新しく代表観測点を挿入する。
        '
        'clsRepresentPoint を要素に持つ ChainList オブジェクトを生成し、clsChainList で指定される ChainList オブジェクトの次に繋げる。
        '
        '引き数：
        'clsChainList 挿入先 ChainList オブジェクト。
        'clsRepresentPoint 挿入する要素となる ObservationPoint オブジェクト(代表観測点)。
        '
        '戻り値：生成した ChainList オブジェクトを返す。
        Private Function InsertRepresentPoint(ByVal clsChainList As ChainList, ByVal clsRepresentPoint As ObservationPoint) As ChainList
            Set InsertRepresentPoint = clsChainList.InsertNext(clsRepresentPoint)
            '所有者を設定する。
            Set clsRepresentPoint.Owner = InsertRepresentPoint
            '観測共有情報を設定する。
            Set clsRepresentPoint.ObservationShared = m_clsObservationShared
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '代表観測点リストへ新しく代表観測点を挿入する。
        '
        'clsRepresentPoint を要素に持つ ChainList オブジェクトを生成し、clsChainList で指定される ChainList オブジェクトの次に繋げる。
        '
        '引き数：
        'clsChainList 挿入先 ChainList オブジェクト。
        'clsRepresentPoint 挿入する要素となる ObservationPoint オブジェクト(代表観測点)。
        '
        '戻り値：生成した ChainList オブジェクトを返す。
        */
        /// <summary>
        /// 
        /// </summary>
        /// <param name="clsChainList"></param>
        /// <param name="clsRepresentPoint"></param>
        /// <returns></returns>
        private ChainList InsertRepresentPoint(ChainList clsChainList, ObservationPoint clsRepresentPoint)
        {
            ChainList w_InsertRepresentPoint;
            w_InsertRepresentPoint = clsChainList.InsertNext(clsRepresentPoint);
            //'所有者を設定する。
            clsRepresentPoint.Owner = w_InsertRepresentPoint;
            //'観測共有情報を設定する。
            clsRepresentPoint.ObservationShared(m_clsObservationShared);
            return w_InsertRepresentPoint;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '遷移状態を評価し、条件を満たす場合は基線ベクトルを作成する。
        '
        'clsObservationPoint1 と clsObservationPoint2 で指定される観測点の遷移状態を評価する。
        '新しく基線ベクトルが作成されたら objBaseLineVectorKeyCollection に登録する。
        '
        '引き数：
        'nSameTimeMin 最小同時観測時間(秒)。
        'nSatelliteCountMin 最少共通衛星数。
        'clsObservationPoint1 観測点1(実観測点)。
        'clsObservationPoint2 観測点2(実観測点)。
        'objIsolatePointChainCollection 孤立観測点チェインコレクション。要素は ChainList オブジェクト。キーは ObservationPoint オブジェクト(実観測点)のポインタ。
        'objBaseLineVectorKeyCollection 基線ベクトルのコレクション。要素は BaseLineVector オブジェクト。キーは基線ベクトルのキー。
        Private Sub EstimateSatelliteInfoChanges(ByVal nSameTimeMin As Long, ByVal nSatelliteCountMin As Long, ByVal clsObservationPoint1 As ObservationPoint, ByVal clsObservationPoint2 As ObservationPoint, ByVal objIsolatePointChainCollection As Collection, ByVal objBaseLineVectorKeyCollection As Collection)

            Dim clsSatelliteInfoChanges1 As SatelliteInfoChanges
            Dim clsSatelliteInfoChanges2 As SatelliteInfoChanges
            Set clsSatelliteInfoChanges1 = clsObservationPoint1.SatelliteInfo
            Set clsSatelliteInfoChanges2 = clsObservationPoint2.SatelliteInfo
            Dim nNumbers1() As Long
            Dim nNumbers2() As Long
            ReDim nNumbers1(-1 To -1)
            ReDim nNumbers2(-1 To -1)
            Dim nIndex1 As Long
            Dim nIndex2 As Long
            nIndex1 = 0
            nIndex2 = 0
            Dim bMatch As Boolean
            Dim tStrTimeEpoch As Date
            Dim tStrTimeBaseLine As Date
            Dim nRcvNumbersGps As Long
            Dim nRcvNumbersGlonass As Long
            '2017/06/06 NS6000対応。'''''''''''''''''''''''''''''''''''''''''''''''''''''''
            Dim nRcvNumbersQZSS As Long
        '   Dim nRcvNumbersGalileo As Long  '2018/08/21 Hitz H.Nakamura 衛星数が増えたので64ビットに増やす。
            Dim nRcvNumbersGalileo1 As Long
            Dim nRcvNumbersGalileo2 As Long
            Dim nRcvNumbersBeiDou1 As Long
            Dim nRcvNumbersBeiDou2 As Long
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            bMatch = False
            Dim nStatus As Long '0=データ抜け。1=エポック取得。2=基線条件クリア。
            nStatus = 0
            Dim bImportEnable As Boolean
            bImportEnable = GetPrivateProfileInt(PROFILE_SAVE_SEC_BASELINE, PROFILE_SAVE_KEY_IMPORTENABLE, IIf(DEF_IMPORTENABLE, 1, 0), App.Path & "\" & App.Title & PROFILE_SAVE_EXT) <> 0
    
            Do
                If nIndex1 >= clsSatelliteInfoChanges1.Count Then Exit Do
                If nIndex2 >= clsSatelliteInfoChanges2.Count Then Exit Do
        
                '次の状態変化時刻。
                Dim tTimeGPS As Date
                If DateDiff("s", clsSatelliteInfoChanges1.TimeGPS(nIndex1), clsSatelliteInfoChanges2.TimeGPS(nIndex2)) > 0 Then
                    tTimeGPS = clsSatelliteInfoChanges1.TimeGPS(nIndex1)
                    nNumbers1 = clsSatelliteInfoChanges1.Numbers(nIndex1)
                    nIndex1 = nIndex1 + 1
                ElseIf DateDiff("s", clsSatelliteInfoChanges1.TimeGPS(nIndex1), clsSatelliteInfoChanges2.TimeGPS(nIndex2)) < 0 Then
                    tTimeGPS = clsSatelliteInfoChanges2.TimeGPS(nIndex2)
                    nNumbers2 = clsSatelliteInfoChanges2.Numbers(nIndex2)
                    nIndex2 = nIndex2 + 1
                Else
                    tTimeGPS = clsSatelliteInfoChanges1.TimeGPS(nIndex1)
                    nNumbers1 = clsSatelliteInfoChanges1.Numbers(nIndex1)
                    nNumbers2 = clsSatelliteInfoChanges2.Numbers(nIndex2)
                    nIndex1 = nIndex1 + 1
                    nIndex2 = nIndex2 + 1
                End If
        
                '比較。一致する衛星の数をカウント。
                Dim nSatelliteCount As Long
                nSatelliteCount = 0
                Dim nCurRcvNumbersGps As Long
                Dim nCurRcvNumbersGlonass As Long
                nCurRcvNumbersGps = 0
                nCurRcvNumbersGlonass = 0
                '2017/06/06 NS6000対応。'''''''''''''''''''''''''''''''''''''''''''''''''''''''
                Dim nCurRcvNumbersQZSS As Long
        '       Dim nCurRcvNumbersGalileo As Long   '2018/08/21 Hitz H.Nakamura 衛星数が増えたので64ビットに増やす。
                Dim nCurRcvNumbersGalileo1 As Long
                Dim nCurRcvNumbersGalileo2 As Long
                Dim nCurRcvNumbersBeiDou1 As Long
                Dim nCurRcvNumbersBeiDou2 As Long
                nCurRcvNumbersQZSS = 0
                nCurRcvNumbersGalileo1 = 0
                nCurRcvNumbersGalileo2 = 0
                nCurRcvNumbersBeiDou1 = 0
                nCurRcvNumbersBeiDou2 = 0
                '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                Dim i1 As Long
                Dim i2 As Long
                i1 = 0
                i2 = 0
                Do While i1 <= UBound(nNumbers1)
                    Do While i2 <= UBound(nNumbers2)
                        If nNumbers1(i1) = nNumbers2(i2) Then
                            nSatelliteCount = nSatelliteCount + 1
                            '2017/06/06 NS6000対応。'''''''''''''''''''''''''''''''''''''''''''''''''''''''
                            'If nNumbers1(i1) < 38 Then
                            '    nCurRcvNumbersGps = nCurRcvNumbersGps Or EXCLUDE_SV(nNumbers1(i1) - 1)
                            'Else
                            '    nCurRcvNumbersGlonass = nCurRcvNumbersGlonass Or EXCLUDE_SV(nNumbers1(i1) - 37 - 1)
                            'End If
                            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                            '1～32＝GPSの1～32番
                            '38～63＝GLONASSの1～26番
                            '65～72＝QZSSの1～8番
                            '73～108＝Galileoの1～36番
                            '109～145＝BeiDouの1～37番
                            '146～204＝SBASの1～59番
                            '205≦不明。
                            If nNumbers1(i1) < 38 Then
                                nCurRcvNumbersGps = nCurRcvNumbersGps Or EXCLUDE_SV(nNumbers1(i1) - 1)
                            ElseIf nNumbers1(i1) < 65 Then
                                nCurRcvNumbersGlonass = nCurRcvNumbersGlonass Or EXCLUDE_SV(nNumbers1(i1) - 38)
                            ElseIf nNumbers1(i1) < 73 Then
                                nCurRcvNumbersQZSS = nCurRcvNumbersQZSS Or EXCLUDE_SV(nNumbers1(i1) - 65)
                           'ElseIf nNumbers1(i1) < 105 Then
                           '    nCurRcvNumbersGalileo = nCurRcvNumbersGalileo Or EXCLUDE_SV(nNumbers1(i1) - 73)
                            ElseIf nNumbers1(i1) < 105 Then
                                nCurRcvNumbersGalileo1 = nCurRcvNumbersGalileo1 Or EXCLUDE_SV(nNumbers1(i1) - 73)
                            ElseIf nNumbers1(i1) < 109 Then
                                nCurRcvNumbersGalileo2 = nCurRcvNumbersGalileo2 Or EXCLUDE_SV(nNumbers1(i1) - 105)
                            ElseIf nNumbers1(i1) < 141 Then
                                nCurRcvNumbersBeiDou1 = nCurRcvNumbersBeiDou1 Or EXCLUDE_SV(nNumbers1(i1) - 109)
                            Else
                                nCurRcvNumbersBeiDou2 = nCurRcvNumbersBeiDou2 Or EXCLUDE_SV(nNumbers1(i1) - 141)
                            End If
                            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                            i2 = i2 + 1
                            Exit Do
                        Else
                            If nNumbers1(i1) < nNumbers2(i2) Then Exit Do
                        End If
                        i2 = i2 + 1
                    Loop
                    If i2 > UBound(nNumbers2) Then Exit Do
                    i1 = i1 + 1
                Loop
        
                Dim bEpoch As Boolean
                bEpoch = UBound(nNumbers1) >= 0 And UBound(nNumbers2) >= 0
                If nSatelliteCount < nSatelliteCountMin Then
                    '衛星数不足。
                    If nStatus > 1 Then
                        '有効観測終了。
                        If DateDiff("s", tStrTimeBaseLine, tTimeGPS) < nSameTimeMin Then
                            '時間不足。
                        Else
                            '条件を満たしたので基線ベクトルを追加。
                            bMatch = True
                        End If
                        nStatus = 1
                    ElseIf nStatus < 1 And bEpoch Then
                        'データ抜け終了。
                        tStrTimeEpoch = tTimeGPS
                        nStatus = 1
                    End If
                    If Not bEpoch Then
                        'データ抜け。
                        If bMatch Then
                            bMatch = False
                            '条件を満たしたので基線ベクトルを追加。
                            Dim clsBaseLineVector As BaseLineVector
                            Set clsBaseLineVector = CreateBaseLineVector(clsObservationPoint1, clsObservationPoint2, tStrTimeEpoch, tTimeGPS, nRcvNumbersGps, nRcvNumbersGlonass, nRcvNumbersQZSS, nRcvNumbersGalileo1, nRcvNumbersGalileo2, nRcvNumbersBeiDou1, nRcvNumbersBeiDou2, bImportEnable, objIsolatePointChainCollection, objBaseLineVectorKeyCollection)
                            '重複してたら基線の向きをそろえる。
                            Dim clsDuplicationBaseLineVectors() As BaseLineVector
                            clsDuplicationBaseLineVectors = GetDuplicationBaseLineVectors(clsBaseLineVector)
                            If UBound(clsDuplicationBaseLineVectors) >= 0 Then
                                If Not clsBaseLineVector.StrPoint.RootPoint Is clsDuplicationBaseLineVectors(0).StrPoint.RootPoint Then
                                    Call ReplaceBaseLineVector(clsBaseLineVector)
                                End If
                            End If
                        End If
                        nStatus = 0
                    End If
                Else
                    '衛星数の条件OK。
                    If nStatus > 1 Then
                        '前回も条件を満たしている。
                        nRcvNumbersGps = nRcvNumbersGps Or nCurRcvNumbersGps
                        nRcvNumbersGlonass = nRcvNumbersGlonass Or nCurRcvNumbersGlonass
                        '2017/06/06 NS6000対応。'''''''''''''''''''''''''''''''''''''''''''''''''''''''
                        nRcvNumbersQZSS = nRcvNumbersQZSS Or nCurRcvNumbersQZSS
        '               nRcvNumbersGalileo = nRcvNumbersGalileo Or nCurRcvNumbersGalileo    '2018/08/21 Hitz H.Nakamura 衛星数が増えたので64ビットに増やす。
                        nRcvNumbersGalileo1 = nRcvNumbersGalileo1 Or nCurRcvNumbersGalileo1
                        nRcvNumbersGalileo2 = nRcvNumbersGalileo2 Or nCurRcvNumbersGalileo2
                        nRcvNumbersBeiDou1 = nRcvNumbersBeiDou1 Or nCurRcvNumbersBeiDou1
                        nRcvNumbersBeiDou2 = nRcvNumbersBeiDou2 Or nCurRcvNumbersBeiDou2
                        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                    Else
                        If nStatus < 1 Then
                            'データ抜け終了。
                            tStrTimeEpoch = tTimeGPS
                        End If
                        '有効観測開始。
                        tStrTimeBaseLine = tTimeGPS
                        nRcvNumbersGps = nCurRcvNumbersGps
                        nRcvNumbersGlonass = nCurRcvNumbersGlonass
                        '2017/06/06 NS6000対応。'''''''''''''''''''''''''''''''''''''''''''''''''''''''
                        nRcvNumbersQZSS = nCurRcvNumbersQZSS
        '               nRcvNumbersGalileo = nCurRcvNumbersGalileo  '2018/08/21 Hitz H.Nakamura 衛星数が増えたので64ビットに増やす。
                        nRcvNumbersGalileo1 = nCurRcvNumbersGalileo1
                        nRcvNumbersGalileo2 = nCurRcvNumbersGalileo2
                        nRcvNumbersBeiDou1 = nCurRcvNumbersBeiDou1
                        nRcvNumbersBeiDou2 = nCurRcvNumbersBeiDou2
                        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                        nStatus = 2
                    End If
                End If
            Loop
    
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '遷移状態を評価し、条件を満たす場合は基線ベクトルを作成する。
        '
        'clsObservationPoint1 と clsObservationPoint2 で指定される観測点の遷移状態を評価する。
        '新しく基線ベクトルが作成されたら objBaseLineVectorKeyCollection に登録する。
        '
        '引き数：
        'nSameTimeMin 最小同時観測時間(秒)。
        'nSatelliteCountMin 最少共通衛星数。
        'clsObservationPoint1 観測点1(実観測点)。
        'clsObservationPoint2 観測点2(実観測点)。
        'objIsolatePointChainCollection 孤立観測点チェインコレクション。要素は ChainList オブジェクト。キーは ObservationPoint オブジェクト(実観測点)のポインタ。
        'objBaseLineVectorKeyCollection 基線ベクトルのコレクション。要素は BaseLineVector オブジェクト。キーは基線ベクトルのキー。
        */
        private void EstimateSatelliteInfoChanges(long nSameTimeMin, long nSatelliteCountMin, ObservationPoint clsObservationPoint1, ObservationPoint clsObservationPoint2,
            ICollection objIsolatePointChainCollection, ICollection objBaseLineVectorKeyCollection)
        {
            return;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '基線ベクトルを作成する。
        '
        'clsObservationPoint1 と clsObservationPoint2 の間に基線ベクトルを作成する。
        '
        '引き数：
        'clsObservationPoint1 観測点1(実観測点)。
        'clsObservationPoint2 観測点2(実観測点)。
        'tStrTime 開始日時(JST)。
        'tEndTime 終了日時(JST)。
        'nRcvNumbersGps 受信GPS衛星番号。ビットフラグ。1=受信。0=非受信。ビット0がGPS1番、ビット1がGPS2番。。。。
        'nRcvNumbersGlonass 受信GLONASS衛星番号。ビットフラグ。1=受信。0=非受信。ビット0がR1番、ビット1がR2番。。。。
        'nRcvNumbersQZSS 受信QZSS衛星番号。ビットフラグ。1=受信。0=非受信。ビット0がJ1番、ビット1がJ2番。。。。
        'nRcvNumbersGalileo1 受信Galileo衛星番号。ビットフラグ。1=受信。0=非受信。ビット0がE1番、ビット1がE2番。。。。
        'nRcvNumbersGalileo2 受信Galileo衛星番号。ビットフラグ。1=受信。0=非受信。ビット0がE33番、ビット1がE34番。。。。
        'nRcvNumbersBeiDou1 受信BeiDou1衛星番号。ビットフラグ。1=受信。0=非受信。ビット0がC1番、ビット1がC2番。。。。
        'nRcvNumbersBeiDou2 受信BeiDou2衛星番号。ビットフラグ。1=受信。0=非受信。ビット0がC33番、ビット1がC34番。。。。
        'bEnable 有効フラグ。
        'objIsolatePointChainCollection 孤立観測点チェインコレクション。要素は ChainList オブジェクト。キーは ObservationPoint オブジェクト(実観測点)のポインタ。
        'objBaseLineVectorKeyCollection 基線ベクトルのコレクション。要素は BaseLineVector オブジェクト。キーは基線ベクトルのキー。
        '
        '戻り値：生成した BaseLineVector オブジェクトを返す。
        Private Function CreateBaseLineVector(ByVal clsObservationPoint1 As ObservationPoint, ByVal clsObservationPoint2 As ObservationPoint, ByVal tStrTime As Date, ByVal tEndTime As Date, ByVal nRcvNumbersGps As Long, ByVal nRcvNumbersGlonass As Long, ByVal nRcvNumbersQZSS As Long, ByVal nRcvNumbersGalileo1 As Long, ByVal nRcvNumbersGalileo2 As Long, ByVal nRcvNumbersBeiDou1 As Long, ByVal nRcvNumbersBeiDou2 As Long, ByVal bEnable As Boolean, ByVal objIsolatePointChainCollection As Collection, ByVal objBaseLineVectorKeyCollection As Collection) As BaseLineVector

            Dim clsBaseLineVector As New BaseLineVector
    
            'セッションの評価。新規にインポートされてセッション名が｢S｣＋３桁のものには"\"が付加されている。
            Dim i As Long
            If clsObservationPoint1.Session <> clsObservationPoint2.Session Then
                If clsObservationPoint1.ProvisionalSession = clsObservationPoint2.ProvisionalSession Then
                    '両方セッション無し、もしくは両方セッション有り。
                    '｢S｣＋３桁の連番。
                    clsBaseLineVector.Session = "S001"
                    i = 2
                ElseIf clsObservationPoint1.ProvisionalSession Then
                    'clsObservationPoint1 がセッション無し。clsObservationPoint2 に従う。
                    clsBaseLineVector.Session = clsObservationPoint2.Session
                    i = 1
                Else
                    'clsObservationPoint2 がセッション無し。clsObservationPoint1 に従う。
                    clsBaseLineVector.Session = clsObservationPoint1.Session
                    i = 1
                End If
            Else
                clsBaseLineVector.Session = clsObservationPoint1.Session
                i = 1
            End If
            Dim objObject As Object
            Dim sKey As String
            sKey = GetBaseLineVectorKey(clsObservationPoint1.Number, clsObservationPoint2.Number, clsBaseLineVector.Session)
            Do While LookupCollectionObject(objBaseLineVectorKeyCollection, objObject, sKey)
                clsBaseLineVector.Session = "S" & Format$(i, "000")
                sKey = GetBaseLineVectorKey(clsObservationPoint1.Number, clsObservationPoint2.Number, clsBaseLineVector.Session)
                i = i + 1
                If i > 1000 Then
                    Call MsgBox("基線ベクトルにセッション名を付けられませんでした。", vbCritical)
                    Exit Function
                End If
            Loop
            Call objBaseLineVectorKeyCollection.Add(clsBaseLineVector, sKey)
    
            '孤立観測点であった場合、孤立観測点リストから削除する。
            Dim clsChainList As ChainList
            If clsObservationPoint1.ChildPoint Is Nothing Then
                sKey = Hex$(GetPointer(clsObservationPoint1))
                If LookupCollectionObject(objIsolatePointChainCollection, clsChainList, sKey) Then
                    Call objIsolatePointChainCollection.Remove(sKey)
                    Call clsChainList.Remove
                End If
            End If
            If clsObservationPoint2.ChildPoint Is Nothing Then
                sKey = Hex$(GetPointer(clsObservationPoint2))
                If LookupCollectionObject(objIsolatePointChainCollection, clsChainList, sKey) Then
                    Call objIsolatePointChainCollection.Remove(sKey)
                    Call clsChainList.Remove
                End If
            End If
    
            '属性の設定。
            clsBaseLineVector.StrTimeGPS = tStrTime
            clsBaseLineVector.EndTimeGPS = tEndTime
            clsBaseLineVector.RcvNumbersGps = nRcvNumbersGps
            clsBaseLineVector.RcvNumbersGlonass = nRcvNumbersGlonass
            '2017/06/06 NS6000対応。'''''''''''''''''''''''''''''''''''''''''''''''''''''''
            clsBaseLineVector.RcvNumbersQZSS = nRcvNumbersQZSS
        '   clsBaseLineVector.RcvNumbersGalileo = nRcvNumbersGalileo    '2018/08/21 Hitz H.Nakamura 衛星数が増えたので64ビットに増やす。
            clsBaseLineVector.RcvNumbersGalileo1 = nRcvNumbersGalileo1
            clsBaseLineVector.RcvNumbersGalileo2 = nRcvNumbersGalileo2
            clsBaseLineVector.RcvNumbersBeiDou1 = nRcvNumbersBeiDou1
            clsBaseLineVector.RcvNumbersBeiDou2 = nRcvNumbersBeiDou2
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    
            '接合観測点の作成。
            Dim clsStrPoint As ObservationPoint
            Dim clsEndPoint As ObservationPoint
            If DateDiff("s", clsObservationPoint1.StrTimeGPS, clsObservationPoint2.StrTimeGPS) > 0 Then
                Set clsStrPoint = clsObservationPoint1
                Set clsEndPoint = clsObservationPoint2
            Else
                Set clsStrPoint = clsObservationPoint2
                Set clsEndPoint = clsObservationPoint1
            End If
            Dim clsOldObservationPoint As ObservationPoint
            Set clsOldObservationPoint = clsBaseLineVector.SetStrPoint(CreateConnectPoint(clsStrPoint))
            Call clsOldObservationPoint.Terminate
            Set clsOldObservationPoint = clsBaseLineVector.SetEndPoint(CreateConnectPoint(clsEndPoint))
            Call clsOldObservationPoint.Terminate
            clsBaseLineVector.StrPoint.Enable = bEnable
            clsBaseLineVector.EndPoint.Enable = bEnable
    
            '基線ベクトルリストに追加する。
            Call m_clsBaseLineVectorHead.TailList.InsertNext(clsBaseLineVector)
    
            Set CreateBaseLineVector = clsBaseLineVector
    
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '基線ベクトルを作成する。
        '
        'clsObservationPoint1 と clsObservationPoint2 の間に基線ベクトルを作成する。
        '
        '引き数：
        'clsObservationPoint1 観測点1(実観測点)。
        'clsObservationPoint2 観測点2(実観測点)。
        'tStrTime 開始日時(JST)。
        'tEndTime 終了日時(JST)。
        'nRcvNumbersGps 受信GPS衛星番号。ビットフラグ。1=受信。0=非受信。ビット0がGPS1番、ビット1がGPS2番。。。。
        'nRcvNumbersGlonass 受信GLONASS衛星番号。ビットフラグ。1=受信。0=非受信。ビット0がR1番、ビット1がR2番。。。。
        'nRcvNumbersQZSS 受信QZSS衛星番号。ビットフラグ。1=受信。0=非受信。ビット0がJ1番、ビット1がJ2番。。。。
        'nRcvNumbersGalileo1 受信Galileo衛星番号。ビットフラグ。1=受信。0=非受信。ビット0がE1番、ビット1がE2番。。。。
        'nRcvNumbersGalileo2 受信Galileo衛星番号。ビットフラグ。1=受信。0=非受信。ビット0がE33番、ビット1がE34番。。。。
        'nRcvNumbersBeiDou1 受信BeiDou1衛星番号。ビットフラグ。1=受信。0=非受信。ビット0がC1番、ビット1がC2番。。。。
        'nRcvNumbersBeiDou2 受信BeiDou2衛星番号。ビットフラグ。1=受信。0=非受信。ビット0がC33番、ビット1がC34番。。。。
        'bEnable 有効フラグ。
        'objIsolatePointChainCollection 孤立観測点チェインコレクション。要素は ChainList オブジェクト。キーは ObservationPoint オブジェクト(実観測点)のポインタ。
        'objBaseLineVectorKeyCollection 基線ベクトルのコレクション。要素は BaseLineVector オブジェクト。キーは基線ベクトルのキー。
        '
        '戻り値：生成した BaseLineVector オブジェクトを返す。
        */
        private BaseLineVector CreateBaseLineVector(ObservationPoint clsObservationPoint1, ObservationPoint clsObservationPoint2, DateTime tStrTime, DateTime tEndTime,
            long nRcvNumbersGps, long nRcvNumbersGlonass, long nRcvNumbersQZSS, long nRcvNumbersGalileo1, long nRcvNumbersGalileo2, long nRcvNumbersBeiDou1, long nRcvNumbersBeiDou2,
            bool bEnable, ICollection objIsolatePointChainCollection, ICollection objBaseLineVectorKeyCollection)
        {
            return null;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '接合観測点を作成する。
        '
        'clsObservationPoint で指定された観測点の子観測点として接合観測点を作成する。
        '
        '引き数：
        'clsObservationPoint 観測点(実観測点)。
        '
        '戻り値：生成した ObservationPoint オブジェクト(接合観測点)を返す。
        Private Function CreateConnectPoint(ByVal clsObservationPoint As ObservationPoint) As ObservationPoint
            Set CreateConnectPoint = New ObservationPoint
            CreateConnectPoint.ObjectType = OBJ_TYPE_OBSERVATIONPOINT Or OBS_TYPE_CONNECT
            Set CreateConnectPoint.Attributes = clsObservationPoint.Attributes
            Call CreateConnectPoint.UpdateCorrectPointImpl(clsObservationPoint.CorrectPoint)
            Call clsObservationPoint.AddChildPoint(CreateConnectPoint)
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '接合観測点を作成する。
        '
        'clsObservationPoint で指定された観測点の子観測点として接合観測点を作成する。
        '
        '引き数：
        'clsObservationPoint 観測点(実観測点)。
        '
        '戻り値：生成した ObservationPoint オブジェクト(接合観測点)を返す。
        */
        private ObservationPoint CreateConnectPoint(ObservationPoint clsObservationPoint)
        {
            ObservationPoint CreateConnectPoint = new ObservationPoint(mdlMain);
            CreateConnectPoint.ObjectType = OBJ_TYPE_OBSERVATIONPOINT | OBS_TYPE_CONNECT;
            CreateConnectPoint.Attributes(clsObservationPoint.Attributes());
            CreateConnectPoint.UpdateCorrectPointImpl(clsObservationPoint.CorrectPoint());
            clsObservationPoint.AddChildPoint(CreateConnectPoint);
            return CreateConnectPoint;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '観測点共通属性を共有させる。
        '
        'clsObservationPoint で指定された観測点と、その後続(弟)の観測点に clsCommon を設定する。
        '
        '引き数：
        'clsObservationPoint 観測点(代表観測点)。
        'clsCommon 観測点共通属性。
        Private Sub ShareAttributesNext(ByVal clsObservationPoint As ObservationPoint, ByVal clsCommon As ObservationCommonAttributes)
            Dim clsNextPoint As ObservationPoint
            Set clsNextPoint = clsObservationPoint
            Do While Not clsNextPoint Is Nothing
                Set clsNextPoint.Attributes.Common = clsCommon
                Call ShareAttributesChild(clsNextPoint)
                Set clsNextPoint = clsNextPoint.NextPoint
            Loop
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '観測点共通属性を共有させる。
        '
        'clsObservationPoint で指定された観測点と、その後続(弟)の観測点に clsCommon を設定する。
        '
        '引き数：
        'clsObservationPoint 観測点(代表観測点)。
        'clsCommon 観測点共通属性。
        */
        private void ShareAttributesNext(ObservationPoint clsObservationPoint, ObservationCommonAttributes clsCommon)
        {
            ObservationPoint clsNextPoint;
            clsNextPoint = clsObservationPoint;
            while (clsNextPoint != null)
            {
                clsNextPoint.Attributes().Common(clsCommon);
                ShareAttributesChild(clsNextPoint);
                clsNextPoint = clsNextPoint.NextPoint();
            }
            return;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '観測点属性を子観測点に共有させる。
        '
        'clsObservationPoint で指定された観測点の持つ観測点属性を、その子観測点に設定する。
        '
        '引き数：
        'clsObservationPoint 観測点。
        Private Sub ShareAttributesChild(ByVal clsObservationPoint As ObservationPoint)
            Dim clsChildPoint As ObservationPoint
            Set clsChildPoint = clsObservationPoint.ChildPoint
            Do While Not clsChildPoint Is Nothing
                Set clsChildPoint.Attributes = clsObservationPoint.Attributes
                Call ShareAttributesChild(clsChildPoint)
                Set clsChildPoint = clsChildPoint.NextPoint
            Loop
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '観測点属性を子観測点に共有させる。
        '
        'clsObservationPoint で指定された観測点の持つ観測点属性を、その子観測点に設定する。
        '
        '引き数：
        'clsObservationPoint 観測点。
        */
        private void ShareAttributesChild(ObservationPoint clsObservationPoint)
        {
            ObservationPoint clsChildPoint;
            clsChildPoint = clsObservationPoint.ChildPoint();
            int i1 = 0;
            while (clsChildPoint != null)
            {
                clsChildPoint.Attributes(clsObservationPoint.Attributes());
                ShareAttributesChild(clsChildPoint);
                clsChildPoint = clsChildPoint.NextPoint();
                i1++;
            }
            return;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '本点を生成する。
        '
        'clsEccentricPoint で指定された偏心点の本点を生成する。
        '
        '引き数：
        'clsEccentricPoint 対象とする観測点(HeadPoint)。
        Private Sub CreateGenuinePoint(ByVal clsEccentricPoint As ObservationPoint)

            '新規観測点。
            Dim clsNewObservationPoint As New ObservationPoint
            clsNewObservationPoint.Attributes.Common.ObjectType = clsNewObservationPoint.Attributes.Common.ObjectType Or OBS_TYPE_GENUIE
            clsNewObservationPoint.Session = GENUINE_POINT_SESSION
    
            '代表観測点を作成。
            Dim clsRepresentPoint As ObservationPoint
            Set clsRepresentPoint = CreateRepresentPoint(clsNewObservationPoint)
            '代表観測点リストにつなげる。
            Dim clsRepresentTail As ChainList
            Set clsRepresentTail = m_clsRepresentPointHead.TailList
            Set clsRepresentTail = InsertRepresentPoint(clsRepresentTail, clsRepresentPoint)
    
            '孤立観測点リストに追加する。
            Dim clsChainList As ChainList
            Set clsChainList = m_clsIsolatePointHead.TailList.InsertNext(clsNewObservationPoint)
    
            '補正点の設定。
            Call clsNewObservationPoint.UpdateCorrectPoint(clsEccentricPoint)
            Call clsEccentricPoint.UpdateCorrectPoint(clsNewObservationPoint)
    
            '本点情報を更新する。
            Call clsNewObservationPoint.UpdateGenuineInfo
    
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '本点を生成する。
        '
        'clsEccentricPoint で指定された偏心点の本点を生成する。
        '
        '引き数：
        'clsEccentricPoint 対象とする観測点(HeadPoint)。
        */
        private void CreateGenuinePoint(ObservationPoint clsEccentricPoint)
        {
            //'新規観測点。
            ObservationPoint clsNewObservationPoint = new ObservationPoint(mdlMain);
            clsNewObservationPoint.Attributes().Common().ObjectType = clsNewObservationPoint.Attributes().Common().ObjectType | OBS_TYPE_GENUIE;
            clsNewObservationPoint.Session(GENUINE_POINT_SESSION);


            //'代表観測点を作成。
            ObservationPoint clsRepresentPoint;
            clsRepresentPoint = CreateRepresentPoint(clsNewObservationPoint);
            //'代表観測点リストにつなげる。
            ChainList clsRepresentTail;
            clsRepresentTail = m_clsRepresentPointHead.TailList();
            clsRepresentTail = InsertRepresentPoint(clsRepresentTail, clsRepresentPoint);


            //'孤立観測点リストに追加する。
            ChainList clsChainList;
            clsChainList = m_clsIsolatePointHead.TailList().InsertNext(clsNewObservationPoint);


            //'補正点の設定。
            clsNewObservationPoint.UpdateCorrectPoint(clsEccentricPoint);
            clsEccentricPoint.UpdateCorrectPoint(clsNewObservationPoint);


            //'本点情報を更新する。
            clsNewObservationPoint.UpdateGenuineInfo();
            return;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '指定された観測点のモードを設定する。
        '
        '引き数：
        'clsObservationPoint 対象とする観測点(代表観測点)。
        'nMode 観測点モード。
        Public Sub SetObsPntMode(ByVal clsObservationPoint As ObservationPoint, ByVal nMode As OBJ_MODE)
            '設定。
            clsObservationPoint.Attributes.Mode = nMode
            clsObservationPoint.IsList = True
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]  //4
        /*
        '指定された観測点のモードを設定する。
        '
        '引き数：
        'clsObservationPoint 対象とする観測点(代表観測点)。
        'nMode 観測点モード。
        */
        public void SetObsPntMode(ObservationPoint clsObservationPoint, OBJ_MODE nMode)
        {
            //'設定。
            clsObservationPoint.Mode(nMode);
            clsObservationPoint.IsList = true;
            return;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '指定された基線ベクトルの種類を設定する。
        '
        '引き数：
        'clsBaseLineVector 対象とする基線ベクトル。
        'nLineType 基線ベクトル種類。
        Public Sub SetLineType(ByVal clsBaseLineVector As BaseLineVector, ByVal nLineType As OBJ_MODE)
            '設定。
            Call clsBaseLineVector.SetLineType(nLineType)
            clsBaseLineVector.IsList = True
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '指定された基線ベクトルの種類を設定する。
        '
        '引き数：
        'clsBaseLineVector 対象とする基線ベクトル。
        'nLineType 基線ベクトル種類。
        */
        public void SetLineType(BaseLineVector clsBaseLineVector, OBJ_MODE nLineType)
        {
            clsBaseLineVector.SetLineType(nLineType);
            clsBaseLineVector.IsList = true;
            return;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '指定された基線ベクトルの始点と終点を置き換える。
        '
        '引き数：
        'clsBaseLineVector 対象とする基線ベクトル。
        Public Sub ReplaceBaseLineVector(ByVal clsBaseLineVector As BaseLineVector)
            If clsBaseLineVector.Analysis <= ANALYSIS_STATUS_FIX Then
                '解析結果の削除。
                Dim objElements As New Collection
                Call objElements.Add(clsBaseLineVector, Hex$(GetPointer(clsBaseLineVector)))
                Call DeleteAnalysis(objElements)
            End If
            '反転。
            Call clsBaseLineVector.Replace
            '基線解析向きも反転する。(Reversは常にFalseにする。)
            clsBaseLineVector.Revers = False 'Not clsBaseLineVector.Revers
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]  //5
        /// <summary>
        /// 指定された基線ベクトルの始点と終点を置き換える。
        /// '
        /// 引き数：
        /// clsBaseLineVector 対象とする基線ベクトル。
        /// 
        /// </summary>
        /// <param name="clsBaseLineVector"></param>
        public void ReplaceBaseLineVector(BaseLineVector clsBaseLineVector)
        {
            if (clsBaseLineVector.Analysis <= ANALYSIS_STATUS.ANALYSIS_STATUS_FIX)
            {
                //'解析結果の削除。
                Dictionary<string, object> objElements = new Dictionary<string, object>();
                objElements.Add(GetPointer(clsBaseLineVector).ToString(), (object)clsBaseLineVector);
                DeleteAnalysis(objElements);

            }
            //'反転。
            clsBaseLineVector.Replace();

            //'基線解析向きも反転する。(Reversは常にFalseにする。)
            clsBaseLineVector.Revers(false);    //'Not clsBaseLineVector.Revers

            return;
        }
        //==========================================================================================


    }
}
