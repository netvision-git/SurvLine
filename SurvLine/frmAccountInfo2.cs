using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static SurvLine.MdlAccountMakeNSS;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using System.Xml.Linq;
using SurvLine.mdl;
using static SurvLine.MdlAccountMake;
using System.Diagnostics.Eventing.Reader;
using static SurvLine.ListViewObservationPoint;
using System.Collections;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Data.Common;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace SurvLine
{
    public partial class frmAccountInfo2 : Form
    {

        MdlMain mdlMain = new MdlMain();



        //==========================================================================================
        /*[VB]
            '*******************************************************************************
            '帳票情報画面

            Option Explicit

            'プロパティ
            Public Result As Long 'ダイアログの結果。
            Public AccountType As ACCOUNT_TYPE '帳票種別。
            Public EnableAutomation As Boolean 'OLEオートメーション有効フラグ。True=OLEオートメーションは有効。False=OLEオートメーションは無し。
            Public Automation As Boolean 'OLEオートメーションフラグ。

            'インプリメンテーション
            Private m_clsAccountParam As New AccountParam '帳票パラメータ。
            Private m_clsListViewOperator As ListViewOperator 'リストビュー操作。
            Private m_clsObjectList As ObjectList 'オブジェクトリスト。
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'*******************************************************************************
        //'帳票情報画面

        //Option Explicit


        //'プロパティ
        public long Result;                 //'ダイアログの結果。
        public long AccountType;            //'帳票種別。
        public bool EnableAutomation;       //'OLEオートメーション有効フラグ。True=OLEオートメーションは有効。False=OLEオートメーションは無し。
        public bool Automation;             //'OLEオートメーションフラグ。

        //'インプリメンテーション
        private AccountParam m_clsAccountParam = new AccountParam();    //'帳票パラメータ。


        private ListViewOperator m_clsListViewOperator;     //'リストビュー操作。
        private ObjectList m_clsObjectList;                 //'オブジェクトリスト。


        //==========================================================================================
        //[C#]
        public frmAccountInfo2()
        {
            InitializeComponent();
            this.Load += Form_Load;
        }

        //==========================================================================================
        /*[VB]
            '*******************************************************************************
            'プロパティ

            '帳票パラメータ。
            Property Let AccountParam(ByVal clsAccountParam As AccountParam)
                Let m_clsAccountParam = clsAccountParam
            End Property

            '帳票パラメータ。
            Property Get AccountParam() As AccountParam
                Set AccountParam = m_clsAccountParam
            End Property
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'*******************************************************************************
        //'プロパティ


        //'帳票パラメータ。
        public void AccountParam(object clsAccountParam)
        {
            m_clsAccountParam = (AccountParam)clsAccountParam;
        }
        //'帳票パラメータ。
        public object AccountParam()
        {
            return m_clsAccountParam;
        }


        //'*******************************************************************************
        //'イベント


        //==========================================================================================
        /*[VB]
            '初期化。
            Private Sub Form_Load()

                On Error GoTo ErrorHandler
    
                '変数初期化。
                Result = vbCancel

                Set m_clsListViewOperator = New ListViewOperator
    
                'フォームの初期化。
                fraCtrls1.BorderStyle = vbBSNone
                fraCtrls2.BorderStyle = vbBSNone
                fraCtrls3.BorderStyle = vbBSNone
    
                '値の設定。
                chkAutomation.Value = IIf(Automation, 1, 0)


                Select Case AccountType
                Case ACCOUNT_TYPE_HAND
                    Caption = "観測手簿"
                Case ACCOUNT_TYPE_WRITE
                    Caption = "観測記簿"
                Case ACCOUNT_TYPE_COORDINATE
                    Caption = "座標計算簿"
                Case ACCOUNT_TYPE_ECCENTRICCORRECT
                    Caption = "偏心計算簿"
                    m_clsAccountParam.RangeType = RANGE_TYPE_OBJECT
                Case ACCOUNT_TYPE_SEMIDYNA_GAN2KON '元期→今期補正表。'2009/11 H.Nakamura
                    Caption = "元期→今期補正表"
                    m_clsAccountParam.RangeType = RANGE_TYPE_OBJECT
                Case ACCOUNT_TYPE_RESULTBASE    '2007/7/18 NGS Yamada
                    Caption = "座標一覧表"
                Case ACCOUNT_TYPE_NVF
                    Caption = "NetSurvベクトルデータファイル"
                Case ACCOUNT_TYPE_NVB
                    Caption = "NetSurvベクトルバイナリファイル"
                Case ACCOUNT_TYPE_JOB
                    Caption = "Geodimeterジョブファイル"
                Case ACCOUNT_TYPE_RINEX
                    Caption = "エクスポート"
                Case ACCOUNT_TYPE_CSV
                    Caption = "CSVファイルの出力"
                End Select


                If AccountType = ACCOUNT_TYPE_ECCENTRICCORRECT Then
                    lblDescription.Caption = Replace(lblDescription.Caption, "%s", "結果")
                ElseIf AccountType = ACCOUNT_TYPE_SEMIDYNA_GAN2KON Then 'セミ・ダイナミック対応。'2009/11 H.Nakamura
                    lblDescription.Caption = Replace(lblDescription.Caption, "%s", "固定点")
                Else
                    lblDescription.Caption = Replace(lblDescription.Caption, "%s", "範囲")
                End If


                If AccountType = ACCOUNT_TYPE_ECCENTRICCORRECT Or AccountType = ACCOUNT_TYPE_SEMIDYNA_GAN2KON Then
                    optAll.Visible = False
                    optSession.Visible = False
                    cmbSession.Visible = False
                    optObject.Visible = False
                    Dim nOffset As Single
                    nOffset = lvObject.Top - fraCtrls1.Top
                    lvObject.Top = lvObject.Top - nOffset
                    nOffset = nOffset + fraCtrls3.Height
                    fraCtrls3.Visible = False
                    cmdSelect.Top = cmdSelect.Top - nOffset
                    cmdUnselect.Top = cmdUnselect.Top - nOffset
                    fraCtrls2.Top = fraCtrls2.Top - nOffset
                    Height = Height - nOffset
                ElseIf AccountType <> ACCOUNT_TYPE_COORDINATE And AccountType<> ACCOUNT_TYPE_RINEX And AccountType <> ACCOUNT_TYPE_CSV Then
                    optObject.Visible = False
                    lvObject.Visible = False
                    cmdSelect.Visible = False
                    cmdUnselect.Visible = False
                    nOffset = fraCtrls1.Left
                    fraCtrls2.Left = nOffset + (fraCtrls3.Width - fraCtrls2.Width)
                    Width = Width - (ScaleWidth - (fraCtrls3.Left + fraCtrls3.Width + nOffset))
                    nOffset = fraCtrls3.Top - (fraCtrls1.Top + optObject.Top + nOffset)
                    If EnableAutomation And (AccountType = ACCOUNT_TYPE_NVF Or AccountType = ACCOUNT_TYPE_NVB Or AccountType = ACCOUNT_TYPE_JOB) Then
                        fraCtrls3.Top = fraCtrls3.Top - nOffset - fraCtrls1.Left
                    Else
                        nOffset = nOffset + fraCtrls3.Height
                        fraCtrls3.Visible = False
                    End If
                    fraCtrls2.Top = fraCtrls2.Top - nOffset
                    Height = Height - nOffset
                Else
                    nOffset = fraCtrls3.Height
                    fraCtrls3.Visible = False
                    cmdSelect.Top = cmdSelect.Top - nOffset
                    cmdUnselect.Top = cmdUnselect.Top - nOffset
                    fraCtrls2.Top = fraCtrls2.Top - nOffset
                    Height = Height - nOffset
                End If
    
                'セッションリストの作成。
                Dim objSessionCollection As New Collection
                Dim clsChainList As ChainList
                If AccountType = ACCOUNT_TYPE_HAND Or AccountType = ACCOUNT_TYPE_RINEX Then
                    Set clsChainList = GetDocument().NetworkModel.RepresentPointHead
                    Do While Not clsChainList Is Nothing
                        If Not clsChainList.Element.Genuine Then
                            Dim clsObservationPoint As ObservationPoint
                            If Not LookupCollectionObject(objSessionCollection, clsObservationPoint, clsChainList.Element.Session) Then
                                Call objSessionCollection.Add(clsObservationPoint, clsChainList.Element.Session)
                                Call cmbSession.AddItem(clsChainList.Element.Session)
                                If StrComp(clsChainList.Element.Session, m_clsAccountParam.Session) = 0 Then cmbSession.ListIndex = cmbSession.NewIndex
                            End If
                        End If
                        Set clsChainList = clsChainList.NextList
                    Loop
                ElseIf AccountType<> ACCOUNT_TYPE_ECCENTRICCORRECT And AccountType <> ACCOUNT_TYPE_SEMIDYNA_GAN2KON Then
                    Set clsChainList = GetDocument().NetworkModel.BaseLineVectorHead
                    Do While Not clsChainList Is Nothing
                        Dim clsBaseLineVector As BaseLineVector
                        If Not LookupCollectionObject(objSessionCollection, clsBaseLineVector, clsChainList.Element.Session) Then
                            Call objSessionCollection.Add(clsBaseLineVector, clsChainList.Element.Session)
                            Call cmbSession.AddItem(clsChainList.Element.Session)
                            If StrComp(clsChainList.Element.Session, m_clsAccountParam.Session) = 0 Then cmbSession.ListIndex = cmbSession.NewIndex
                        End If
                        Set clsChainList = clsChainList.NextList
                    Loop
                End If
    
                'リストの初期化。
                lvObject.View = lvwReport
                lvObject.Checkboxes = True
                lvObject.FullRowSelect = True
                lvObject.HideSelection = True
                lvObject.LabelEdit = lvwManual
                lvObject.Sorted = True


                Set m_clsObjectList = New ObjectList
                If AccountType = ACCOUNT_TYPE_COORDINATE Or AccountType = ACCOUNT_TYPE_CSV Then
                    optObject.Caption = "基線"
                    '基線ベクトルリスト。
                    Dim clsListViewBaseLineVector As New ListViewBaseLineVector
                    Call m_clsObjectList.Initialize(lvObject, clsListViewBaseLineVector)
                    Call m_clsObjectList.MakeFromChainList(lvObject, GetDocument().NetworkModel.BaseLineVectorHead, m_clsAccountParam.SelectedObjects)
                ElseIf AccountType = ACCOUNT_TYPE_ECCENTRICCORRECT Or AccountType = ACCOUNT_TYPE_SEMIDYNA_GAN2KON Or AccountType = ACCOUNT_TYPE_RINEX Then
                    optObject.Caption = "観測点"
                    '観測点リスト。
                    Dim clsListViewObservationPoint As New ListViewObservationPoint
        
                    '2009/11 H.Nakamura
                    'clsListViewObservationPoint.EccentricCorrect = (AccountType = ACCOUNT_TYPE_ECCENTRICCORRECT)
                    If AccountType = ACCOUNT_TYPE_ECCENTRICCORRECT Then
                        clsListViewObservationPoint.ListType = LIST_VIEW_OBSERVATION_POINT_ECCENTRIC_CORRECT
                    ElseIf AccountType = ACCOUNT_TYPE_SEMIDYNA_GAN2KON Then
                        clsListViewObservationPoint.ListType = LIST_VIEW_OBSERVATION_POINT_FIXED_LIST
                    Else
                        clsListViewObservationPoint.ListType = LIST_VIEW_OBSERVATION_POINT_STANDARD
                    End If


                    Call m_clsObjectList.Initialize(lvObject, clsListViewObservationPoint)
                    Call m_clsObjectList.MakeFromChainList(lvObject, GetDocument().NetworkModel.RepresentPointHead, m_clsAccountParam.SelectedObjects)
                End If
    
                '値の設定。リストのListIndex等より後に設定する。
                Select Case m_clsAccountParam.RangeType
                Case RANGE_TYPE_SESSION
                    optSession.Value = True
                Case RANGE_TYPE_OBJECT
                    optObject.Value = True
                Case Else
                    optAll.Value = True
                End Select


                Exit Sub


            ErrorHandler:
                Call mdlMain.ErrorExit

            End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        private void Form_Load(object sender, EventArgs e)
        {
            //On Error GoTo ErrorHandler


            //'変数初期化。
            Result = DEFINE.vbCancel;

            ListViewOperator m_clsListViewOperator = new ListViewOperator();


            //'フォームの初期化
            fraCtrls1.FlatStyle = DEFINE.vbBSNone;
            fraCtrls2.FlatStyle = DEFINE.vbBSNone;
            fraCtrls3.FlatStyle = DEFINE.vbBSNone;

            //'値の設定。
            chkAutomation.Checked = Automation;


            string Caption = "";
            switch (AccountType)
            {
                case (long)ACCOUNT_TYPE.ACCOUNT_TYPE_HAND:
                    Caption = "観測手簿";
                    break;
                case (long)ACCOUNT_TYPE.ACCOUNT_TYPE_WRITE:
                    Caption = "観測記簿";
                    break;
                case (long)ACCOUNT_TYPE.ACCOUNT_TYPE_COORDINATE:
                    Caption = "座標計算簿";
                    break;
                case (long)ACCOUNT_TYPE.ACCOUNT_TYPE_ECCENTRICCORRECT:
                    Caption = "偏心計算簿";
                    m_clsAccountParam.RangeType = (RANGE_TYPE)(long)RANGE_TYPE.RANGE_TYPE_OBJECT;
                    break;
                case (long)ACCOUNT_TYPE.ACCOUNT_TYPE_SEMIDYNA_GAN2KON:  //'元期→今期補正表。'2009 / 11 H.Nakamura
                    Caption = "元期→今期補正表";
                    m_clsAccountParam.RangeType = (RANGE_TYPE)(long)RANGE_TYPE.RANGE_TYPE_OBJECT;
                    break;
                case (long)ACCOUNT_TYPE.ACCOUNT_TYPE_RESULTBASE:        //'2007/7/18 NGS Yamada
                    Caption = "座標一覧表";
                    break;
                case (long)ACCOUNT_TYPE.ACCOUNT_TYPE_NVF:
                    Caption = "NetSurvベクトルデータファイル";
                    break;
                case(long)ACCOUNT_TYPE.ACCOUNT_TYPE_NVB:
                    Caption = "NetSurvベクトルバイナリファイル";
                    break;
                case (long)ACCOUNT_TYPE.ACCOUNT_TYPE_JOB:
                    Caption = "Geodimeterジョブファイル";
                    break;
                case(long)ACCOUNT_TYPE.ACCOUNT_TYPE_RINEX:
                    Caption = "エクスポート";
                    break;
                case (long)ACCOUNT_TYPE.ACCOUNT_TYPE_CSV:
                    Caption = "CSVファイルの出力";
                    break;
            }
            this.Text = Caption;


            if (AccountType == (long)ACCOUNT_TYPE.ACCOUNT_TYPE_ECCENTRICCORRECT)
            {
                lblDescription.Text = lblDescription.Text.Replace("%s", "結果");
            }
            else if (AccountType == (long)ACCOUNT_TYPE.ACCOUNT_TYPE_SEMIDYNA_GAN2KON)      //'セミ・ダイナミック対応。'2009 / 11 H.Nakamura
            {
                lblDescription.Text = lblDescription.Text.Replace("%s", "固定点");
            }
            else
            {
                lblDescription.Text = lblDescription.Text.Replace("%s", "範囲");
            }



            Single nOffset = 0;
            if (AccountType == (long)ACCOUNT_TYPE.ACCOUNT_TYPE_ECCENTRICCORRECT || AccountType == (long)ACCOUNT_TYPE.ACCOUNT_TYPE_SEMIDYNA_GAN2KON)
            {
                optAll.Visible = false;
                optSession.Visible = false;
                cmbSession.Visible = false;
                optObject.Visible = false;
                nOffset = lvObject.Top - fraCtrls1.Top;
                lvObject.Top = lvObject.Top - (int)nOffset;
                nOffset = nOffset + fraCtrls3.Height;
                fraCtrls3.Visible = false;
                cmdSelect.Top = cmdSelect.Top - (int)nOffset;
                cmdUnselect.Top = cmdUnselect.Top - (int)nOffset;
                fraCtrls2.Top = fraCtrls2.Top - (int)nOffset;
                Height = Height - (int)nOffset;
            }
            else if (AccountType != (long)ACCOUNT_TYPE.ACCOUNT_TYPE_COORDINATE && AccountType != (long)ACCOUNT_TYPE.ACCOUNT_TYPE_RINEX && AccountType != (long)ACCOUNT_TYPE.ACCOUNT_TYPE_CSV)
            {
                optObject.Visible = false;
                lvObject.Visible = false;
                cmdSelect.Visible = false;
                cmdUnselect.Visible = false;
                //瀬戸口   nOffset = (Single)fraCtrls1.Left;
                //瀬戸口   fraCtrls2.Left = (int)nOffset + (fraCtrls3.Width - fraCtrls2.Width);
                //??    Width = Width - (ScaleWidth - (fraCtrls3.Left + fraCtrls3.Width + nOffset))
                //瀬戸口   Width = Width - (fraCtrls3.Width - (fraCtrls3.Left + fraCtrls3.Width + (int)nOffset));
                //瀬戸口   nOffset = fraCtrls3.Top - (fraCtrls1.Top + optObject.Top + nOffset);
                if (EnableAutomation && (AccountType == (long)ACCOUNT_TYPE.ACCOUNT_TYPE_NVF || AccountType == (long)ACCOUNT_TYPE.ACCOUNT_TYPE_NVB || AccountType == (long)ACCOUNT_TYPE.ACCOUNT_TYPE_JOB)){
                    //瀬戸口   fraCtrls3.Top = fraCtrls3.Top - (int)nOffset - fraCtrls1.Left;
                }
                else
                {
                    //瀬戸口   nOffset = nOffset + fraCtrls3.Height;
                    //瀬戸口   fraCtrls3.Visible = false;
                }
                fraCtrls2.Top = fraCtrls2.Top - (int)nOffset;
                //瀬戸口   Height = Height - (int)nOffset;

            }
            else
            {
                nOffset = fraCtrls3.Height;
                fraCtrls3.Visible = false;
                cmdSelect.Top = cmdSelect.Top - (int)nOffset;
                cmdUnselect.Top = cmdUnselect.Top - (int)nOffset;
                fraCtrls2.Top = fraCtrls2.Top - (int)nOffset;
                //瀬戸口   Height = Height - (int)nOffset;
                Height -= fraCtrls3.Height;
            }

            //'セッションリストの作成。
            Collection cobjSessionCollectionollection = new Collection();
            ChainList clsChainList;
            if (AccountType == (long)ACCOUNT_TYPE.ACCOUNT_TYPE_HAND || AccountType == (long)ACCOUNT_TYPE.ACCOUNT_TYPE_RINEX)
            {

#if false
                //------------ GetDocument() --------------------
                //[VB]  clsChainList = GetDocument().NetworkModel.RepresentPointHead();
                Document document2 = mdlMain.GetDocument();
                NetworkModel clsNetworkModel = document2.NetworkModel();
                clsChainList = (ChainList)clsNetworkModel.RepresentPointHead();


                Do While Not clsChainList Is Nothing
                        If Not clsChainList.Element.Genuine Then
                            Dim clsObservationPoint As ObservationPoint
                            If Not LookupCollectionObject(objSessionCollection, clsObservationPoint, clsChainList.Element.Session) Then
                                Call objSessionCollection.Add(clsObservationPoint, clsChainList.Element.Session)
                                Call cmbSession.AddItem(clsChainList.Element.Session)
                                If StrComp(clsChainList.Element.Session, m_clsAccountParam.Session) = 0 Then cmbSession.ListIndex = cmbSession.NewIndex
                            End If
                        End If
                        Set clsChainList = clsChainList.NextList
                    Loop
#endif
            }
            else if (AccountType != (long)ACCOUNT_TYPE.ACCOUNT_TYPE_ECCENTRICCORRECT && AccountType != (long)ACCOUNT_TYPE.ACCOUNT_TYPE_SEMIDYNA_GAN2KON)
            {

            }
            /*
                'セッションリストの作成。
                Dim objSessionCollection As New Collection
                Dim clsChainList As ChainList
                If AccountType = ACCOUNT_TYPE_HAND Or AccountType = ACCOUNT_TYPE_RINEX Then
                    Set clsChainList = GetDocument().NetworkModel.RepresentPointHead
                    Do While Not clsChainList Is Nothing
                        If Not clsChainList.Element.Genuine Then
                            Dim clsObservationPoint As ObservationPoint
                            If Not LookupCollectionObject(objSessionCollection, clsObservationPoint, clsChainList.Element.Session) Then
                                Call objSessionCollection.Add(clsObservationPoint, clsChainList.Element.Session)
                                Call cmbSession.AddItem(clsChainList.Element.Session)
                                If StrComp(clsChainList.Element.Session, m_clsAccountParam.Session) = 0 Then cmbSession.ListIndex = cmbSession.NewIndex
                            End If
                        End If
                        Set clsChainList = clsChainList.NextList
                    Loop
                ElseIf AccountType<> ACCOUNT_TYPE_ECCENTRICCORRECT And AccountType <> ACCOUNT_TYPE_SEMIDYNA_GAN2KON Then
                    Set clsChainList = GetDocument().NetworkModel.BaseLineVectorHead
                    Do While Not clsChainList Is Nothing
                        Dim clsBaseLineVector As BaseLineVector
                        If Not LookupCollectionObject(objSessionCollection, clsBaseLineVector, clsChainList.Element.Session) Then
                            Call objSessionCollection.Add(clsBaseLineVector, clsChainList.Element.Session)
                            Call cmbSession.AddItem(clsChainList.Element.Session)
                            If StrComp(clsChainList.Element.Session, m_clsAccountParam.Session) = 0 Then cmbSession.ListIndex = cmbSession.NewIndex
                        End If
                        Set clsChainList = clsChainList.NextList
                    Loop
                End If

            */



            //'リストの初期化。
            //lvObject.View = View.Tile;  //lvwReport;

            //瀬戸口   lvObject.CheckBoxes = true;
            lvObject.CheckBoxes = false;

            lvObject.Visible = true;    //瀬戸口   

            lvObject.FullRowSelect = true;
            lvObject.HideSelection = true;
            lvObject.LabelEdit = false;
            //lvObject.Sorted = true;
            lvObject.Sorting = SortOrder.None;

            ObjectList m_clsObjectList = new ObjectList();
            if (AccountType == (long)ACCOUNT_TYPE.ACCOUNT_TYPE_COORDINATE || AccountType == (long)ACCOUNT_TYPE.ACCOUNT_TYPE_CSV)
            {
                optObject.Text = "基線";
                //'基線ベクトルリスト。
                //ListViewBaseLineVector clsListViewBaseLineVector = new ListViewBaseLineVector();
                ListViewInterface clsListViewBaseLineVector = new ListViewInterface();

                m_clsObjectList.Initialize(lvObject, clsListViewBaseLineVector);

                //------------ GetDocument() --------------------
                Document document2 = mdlMain.GetDocument();
                NetworkModel clsNetworkModel = document2.NetworkModel();
                ChainList pChainList = (ChainList)clsNetworkModel.RepresentPointHead(); ;

                m_clsObjectList.MakeFromChainList(lvObject, pChainList, m_clsAccountParam.SelectedObjects());
            }
            else if (AccountType == (long)ACCOUNT_TYPE.ACCOUNT_TYPE_ECCENTRICCORRECT || AccountType == (long)ACCOUNT_TYPE.ACCOUNT_TYPE_SEMIDYNA_GAN2KON || AccountType == (long)ACCOUNT_TYPE.ACCOUNT_TYPE_RINEX)
            {
                optObject.Text = "観測点";
                //'観測点リスト。
                ListViewObservationPoint clsListViewObservationPoint = new ListViewObservationPoint();


                //'2009/11 H.Nakamura
                //  clsListViewObservationPoint.EccentricCorrect = (AccountType = ACCOUNT_TYPE_ECCENTRICCORRECT);

                if (AccountType == (long)ACCOUNT_TYPE.ACCOUNT_TYPE_ECCENTRICCORRECT)
                {
                    clsListViewObservationPoint.ListType((long)LIST_VIEW_OBSERVATION_POINT_TYPE.LIST_VIEW_OBSERVATION_POINT_ECCENTRIC_CORRECT);
                }
                else if (AccountType == (long)ACCOUNT_TYPE.ACCOUNT_TYPE_SEMIDYNA_GAN2KON)
                {
                    clsListViewObservationPoint.ListType((long)LIST_VIEW_OBSERVATION_POINT_TYPE.LIST_VIEW_OBSERVATION_POINT_FIXED_LIST);
                }
                else
                {
                    clsListViewObservationPoint.ListType((long)LIST_VIEW_OBSERVATION_POINT_TYPE.LIST_VIEW_OBSERVATION_POINT_STANDARD);

                }
#if false
                m_clsObjectList.Initialize(lvObject, clsListViewObservationPoint);
#else
                ClsObjectList_Initialize(lvObject, clsListViewObservationPoint);
#endif


//                Document document2 = mdlMain.GetDocument();
//                NetworkModel clsNetworkModel = document2.NetworkModel();
//                ChainList pChainList = (ChainList)clsNetworkModel.RepresentPointHead(); ;
//                m_clsObjectList.MakeFromChainList(lvObject, pChainList, m_clsAccountParam.SelectedObjects());


            }

            //'値の設定。リストのListIndex等より後に設定する。
            switch (m_clsAccountParam.RangeType)
            {
                case RANGE_TYPE.RANGE_TYPE_SESSION:
                    optSession.Checked = true;
                    break;
                case RANGE_TYPE.RANGE_TYPE_OBJECT:
                    optObject.Checked = true;
                    break;
                default:
                    optAll.Checked = true;
                    break;

            }



        }

        //ローカルプログラム：　ClsObjectList. Initializeを検討
        private void ClsObjectList_Initialize(System.Windows.Forms.ListView lvObject, ListViewObservationPoint clsListViewObservationPoint)
        {
            int nWidth;             //    Dim nWidth As Long
            long nTotalWidth;       //    Dim nTotalWidth As Long

            // ListViewコントロールのプロパティを設定
            lvObject.FullRowSelect = true;
            lvObject.GridLines = true;
            lvObject.Sorting = SortOrder.Ascending;
            lvObject.View = View.Details;

            lvObject.Columns.Clear();

            //--- Chaeck Boxの表示を有効 ---
            lvObject.View = View.Details;
            lvObject.CheckBoxes = true;



            //'観測点リストリストカラム名称、チェックボックス。
            nWidth = 20;
            nTotalWidth = nWidth;
            lvObject.Columns.Add(COL_NAM_OBSPNT_CHECK, nWidth);

            //'観測点リストリストカラム名称、セッション名。
            nWidth = 100;
            nTotalWidth += nWidth;
            lvObject.Columns.Add(COL_NAM_OBSPNT_SESSION, nWidth);

            //'観測点リストリストカラム名称、観測点番号。
            nWidth = 100;
            nTotalWidth += nWidth;
            lvObject.Columns.Add(COL_NAM_OBSPNT_NUMBER, nWidth);

            //'観測点リストリストカラム名称、観測点名称。
            nWidth = 100;
            nTotalWidth += nWidth;
            lvObject.Columns.Add(COL_NAM_OBSPNT_NAME, nWidth);

            //'観測点リストリストカラム名称、観測開始日時。
            nWidth = 130;
            nTotalWidth += nWidth;
            lvObject.Columns.Add(COL_NAM_OBSPNT_STRTIME, nWidth);
            //'観測点リストリストカラム名称、観測終了日時。
            nWidth = 130;
            nTotalWidth += nWidth;
            lvObject.Columns.Add(COL_NAM_OBSPNT_ENDTIME, nWidth);

            lvObject.Width = (int)nTotalWidth;




            for (int i = 0; i < 2; i++)
            {
                ListViewItem itemx = new ListViewItem();

                lvObject.CheckBoxes = true;

                itemx.Text = "";
                itemx.SubItems.Add("017B");
                itemx.SubItems.Add("001");
                itemx.SubItems.Add("3-21");
                itemx.SubItems.Add("2012/012/17 10:13:45");
                itemx.SubItems.Add("2012/012/17 10:43:30");
                lvObject.Items.Add(itemx);
            }


        }



        //==========================================================================================
        /*[VB]
        '終了処理。
            Private Sub Form_Unload(Cancel As Integer)

                On Error GoTo ErrorHandler


                Set m_clsListViewOperator = Nothing
                Set m_clsObjectList = Nothing


                Exit Sub


            ErrorHandler:
                Call mdlMain.ErrorExit


            End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]

        //==========================================================================================
        /*[VB]
            'プロジェクト全体。
            Private Sub optAll_Click()

                On Error GoTo ErrorHandler
    
            '    cmbSession.Enabled = False
            '    lvObject.Enabled = False
            '    cmdSelect.Enabled = False
            '    cmdUnselect.Enabled = False


                Exit Sub


            ErrorHandler:
                Call mdlMain.ErrorExit

            End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        private void optAll_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                //  On Error GoTo ErrorHandler


                //'    cmbSession.Enabled = False
                //'    lvObject.Enabled = True
                //'    cmdSelect.Enabled = True
                //'    cmdUnselect.Enabled = True


            }
            catch (Exception ex)
            {
                //ErrorHandler:
                //  Call mdlMain.ErrorExit
                MessageBox.Show(ex.Message, "エラー発生");


            }

        }


        //==========================================================================================
        /*[VB]
            'セッション。
            Private Sub optSession_Click()

                On Error GoTo ErrorHandler
    
            '    cmbSession.Enabled = True
            '    lvObject.Enabled = False
            '    cmdSelect.Enabled = False
            '    cmdUnselect.Enabled = False


                Exit Sub


            ErrorHandler:
                Call mdlMain.ErrorExit

            End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        private void optSession_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                //  On Error GoTo ErrorHandler


                //'    cmbSession.Enabled = False
                //'    lvObject.Enabled = True
                //'    cmdSelect.Enabled = True
                //'    cmdUnselect.Enabled = True


            }
            catch (Exception ex)
            {
                //ErrorHandler:
                //  Call mdlMain.ErrorExit
                MessageBox.Show(ex.Message, "エラー発生");


            }

        }


        //==========================================================================================
        /*[VB]
            'オブジェクト。
            Private Sub optObject_Click()

                On Error GoTo ErrorHandler
    
            '    cmbSession.Enabled = False
            '    lvObject.Enabled = True
            '    cmdSelect.Enabled = True
            '    cmdUnselect.Enabled = True


                Exit Sub


            ErrorHandler:
                Call mdlMain.ErrorExit

            End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        private void optObject_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                //  On Error GoTo ErrorHandler


                //'    cmbSession.Enabled = False
                //'    lvObject.Enabled = True
                //'    cmdSelect.Enabled = True
                //'    cmdUnselect.Enabled = True


            }
            catch (Exception ex)
            {
                //ErrorHandler:
                //  Call mdlMain.ErrorExit
                MessageBox.Show(ex.Message, "エラー発生");

            }

        }


        //==========================================================================================
        /*[VB]
            'リストの選択。
            Private Sub cmbSession_Click()

                On Error GoTo ErrorHandler

                optSession.Value = True


                Exit Sub


            ErrorHandler:
                Call mdlMain.ErrorExit

            End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        private void cmbSession_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //  On Error GoTo ErrorHandler

                optSession.Checked = true;

            }
            catch (Exception ex)
            {
                //ErrorHandler:
                //  Call mdlMain.ErrorExit
                MessageBox.Show(ex.Message, "エラー発生");

            }

        }



        //==========================================================================================
        /*[VB]
            'リストのチェック。
            Private Sub lvObject_ItemCheck(ByVal Item As MSComctlLib.ListItem)

                On Error GoTo ErrorHandler


                optObject.Value = True

                Exit Sub

            ErrorHandler:
                Call mdlMain.ErrorExit


            End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]

        //==========================================================================================
        /*[VB]
            'ソート。
            Private Sub lvObject_ColumnClick(ByVal ColumnHeader As MSComctlLib.ColumnHeader)

                On Error GoTo ErrorHandler


                Call m_clsListViewOperator.Sort(lvObject, ColumnHeader.Index)


                Exit Sub


            ErrorHandler:
                Call mdlMain.ErrorExit


            End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]

        //==========================================================================================
        /*[VB]
            '全選択。
            Private Sub cmdSelect_Click()

                On Error GoTo ErrorHandler

                Call m_clsListViewOperator.CheckAll(lvObject, True)
                optObject.Value = True


                Exit Sub


            ErrorHandler:
                Call mdlMain.ErrorExit

            End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        private void cmdSelect_Click(object sender, EventArgs e)
        {
            try
            {
                //  On Error GoTo ErrorHandler


                ListViewOperator m_clsListViewOperator = new ListViewOperator();

                m_clsListViewOperator.CheckAll(lvObject, true);
                optObject.Checked = true;

                return;

            }
            catch (Exception ex)
            {
                //ErrorHandler:
                //  Call mdlMain.ErrorExit
                MessageBox.Show(ex.Message, "エラー発生");

            }

        }




        //==========================================================================================
        /*[VB]
            '全解除。
            Private Sub cmdUnselect_Click()

                On Error GoTo ErrorHandler

                Call m_clsListViewOperator.CheckAll(lvObject, False)
                optObject.Value = True


                Exit Sub


            ErrorHandler:
                Call mdlMain.ErrorExit

            End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        private void cmdUnselect_Click(object sender, EventArgs e)
        {
            try
            {
                //  On Error GoTo ErrorHandler

                ListViewOperator m_clsListViewOperator = new ListViewOperator();

                m_clsListViewOperator.CheckAll(lvObject, false);
                optObject.Checked = true;

                return;

            }
            catch (Exception ex)
            {
                //ErrorHandler:
                //  Call mdlMain.ErrorExit
                MessageBox.Show(ex.Message, "エラー発生");

            }

        }


        //==========================================================================================
        /*[VB]
            'キャンセルでダイアログを終了する。
            Private Sub CancelButton_Click()

                On Error GoTo ErrorHandler

                Call Unload(Me)

                Exit Sub
            
            ErrorHandler:
                Call mdlMain.ErrorExit

            End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /// <summary>
        /// 'キャンセルでダイアログを終了する。
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelButton_Click(object sender, EventArgs e)
        {
            try
            {
                //  On Error GoTo ErrorHandler

                Result = DEFINE.vbCancel;              //ボタン(OK:1 / キャンセル:0)

                this.Close();

            }
            catch (Exception ex)
            {
                //ErrorHandler:
                //  Call mdlMain.ErrorExit

            }
        }



        //==========================================================================================
        /*[VB]
            'ＯＫでダイアログを終了する。
            Private Sub OKButton_Click()

                On Error GoTo ErrorHandler
    
                '入力値の検査。
                If Not CheckData() Then Exit Sub
                '値を反映させる。
                Call ReflectData
                '終了。
                Result = vbOK
                Call Unload(Me)

                Exit Sub

            ErrorHandler:
                Call mdlMain.ErrorExit

            End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        private void OKButton_Click(object sender, EventArgs e)
        {
            try
            {
                // On Error GoTo ErrorHandler

                //'入力値の検査。
                if (!CheckData())
                {
                    return;
                }

                //'値を反映させる。
                ReflectData();

                //'終了。
                Result = DEFINE.vbOK;     //ボタン(OK:1 / キャンセル:0)

                this.Close();

            }
            catch (Exception ex)
            {
                //ErrorHandler:
                //  Call mdlMain.ErrorExit
                MessageBox.Show(ex.Message, "エラー発生");
            }

        }


        //==========================================================================================
        /*[VB]
            '*******************************************************************************
            'インプリメンテーション

            '入力値を検査する。
            '
            '戻り値：
            '入力値が正常である場合 True を返す。
            'それ以外の場合 False を返す。
            Private Function CheckData() As Boolean

                CheckData = False


                If optSession.Value Then
                    If cmbSession.ListIndex< 0 Then
                        Call MsgBox("セッションを選択してください。", vbCritical)
                        Call cmbSession.SetFocus
                        Exit Function
                    End If
                ElseIf optObject.Value Then
                    If AccountType <> ACCOUNT_TYPE_COORDINATE And AccountType<> ACCOUNT_TYPE_ECCENTRICCORRECT And AccountType <> ACCOUNT_TYPE_SEMIDYNA_GAN2KON And AccountType<> ACCOUNT_TYPE_RINEX And AccountType <> ACCOUNT_TYPE_CSV Then
                        Call MsgBox("出力する範囲を選択してください。", vbCritical)
                        Exit Function
                    Else
                        'チェックあり？
                        If Not m_clsListViewOperator.IsChecked(lvObject) Then
                            If AccountType = ACCOUNT_TYPE_COORDINATE Or AccountType = ACCOUNT_TYPE_CSV Then
                                Call MsgBox("基線ベクトルを選択してください。", vbCritical)
                            ElseIf AccountType = ACCOUNT_TYPE_SEMIDYNA_GAN2KON Then 'セミ・ダイナミック対応。'2009/11 H.Nakamura
                                Call MsgBox("固定点を選択してください。", vbCritical)
                            Else
                                Call MsgBox("観測点を選択してください。", vbCritical)
                            End If
                            Call lvObject.SetFocus
                            Exit Function
                        End If
                    End If
                End If


                CheckData = True


            End Function
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        private bool CheckData()
        {
            bool CheckData = false;

#if false
            if (optSession.Checked)
            {
                if (cmbSession.ListIndex < 0)
                {
                    //Call MsgBox("セッションを選択してください。", vbCritical)
                    //cmbSession.SetFocus;
                    cmbSession.Focus();

                    return CheckData;

                }

            }
#endif
            CheckData = true;
            return CheckData;

        }

        //==========================================================================================
        /*[VB]
            '値を反映させる。
            Private Sub ReflectData()

                Automation = (chkAutomation.Value = 1)


                If optSession.Value Then
                    m_clsAccountParam.RangeType = RANGE_TYPE_SESSION
                    m_clsAccountParam.Session = cmbSession.Text
                ElseIf optObject.Value Then
                    m_clsAccountParam.RangeType = RANGE_TYPE_OBJECT
                    m_clsAccountParam.SelectedObjects = m_clsObjectList.GetSelectedKeys(lvObject)
                Else
                    m_clsAccountParam.RangeType = RANGE_TYPE_ALL
                End If


            End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /// <summary>
        ///    '値を反映させる。
        /// 
        /// </summary>
        private void ReflectData()
        {
            Automation = chkAutomation.Checked = true;

            AccountParam m_clsAccountParam = new AccountParam();

            if (optSession.Checked){
                m_clsAccountParam.RangeType = RANGE_TYPE.RANGE_TYPE_SESSION;
                m_clsAccountParam.Session = cmbSession.Text;
            }
            else if (optObject.Checked)
            {
                m_clsAccountParam.RangeType = RANGE_TYPE.RANGE_TYPE_OBJECT;
                m_clsAccountParam.SelectedObjects((object)m_clsObjectList.GetSelectedKeys(lvObject));
            }
            else
            {
                m_clsAccountParam.RangeType = RANGE_TYPE.RANGE_TYPE_ALL;
            }
        }

        private void frmAccountInfo2_Load(object sender, EventArgs e)
        {

        }

        private void optAll_CheckedChanged_1(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
