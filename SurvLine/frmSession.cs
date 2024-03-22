using SurvLine.mdl;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static SurvLine.mdl.DEFINE;
using static SurvLine.mdl.DEFINE;
using static SurvLine.mdl.MdlMain;
using static SurvLine.mdl.MdlNSDefine;
using static SurvLine.mdl.MdlAccountMakeNSS;
using static SurvLine.mdl.MdlListPane;
using static SurvLine.mdl.MdlListProc;
using static SurvLine.mdl.MdlBaseLineAnalyser;
using static SurvLine.mdl.MdlUtility;
using static SurvLine.mdl.MdlSession;   //2
using static System.Collections.Specialized.BitVector32;
using System.Runtime.InteropServices;

namespace SurvLine
{
    public partial class frmSession : Form
    {


        //'*******************************************************************************
        //'セッションの変更画面
        //
        //Option Explicit
        //
        //'*******************************************************************************

        //==========================================================================================
        /*[VB]
            'プロパティ
            Public Result As Long 'ダイアログの結果。
            Public Session As String 'セッション名。
            Public Extend As Boolean '拡張フラグが設定される。True=関連するオブジェクトにも設定する。False=関連するオブジェクトまでは設定しない。

            'インプリメンテーション
            Private m_objElements As Collection '対象とするオブジェクト。要素はオブジェクト。キーは任意。
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'プロパティ
        public long Result;     //'ダイアログの結果。
        public string Session;  //'セッション名。
        public bool Extend;     //'拡張フラグが設定される。True=関連するオブジェクトにも設定する。False=関連するオブジェクトまでは設定しない。

        //'インプリメンテーション
        //  private Collection m_objElements;               //'対象とするオブジェクト。要素はオブジェクト。キーは任意。
        private Dictionary<string, object> m_objElements;   //'対象とするオブジェクト。要素はオブジェクト。キーは任意。

        public long SelectLine;     //ListPane選択位置  //2


        MdlSession mdlSession;


        //==========================================================================================
        /*[VB]
            '*******************************************************************************
            'プロパティ

            '対象とするオブジェクト。要素はオブジェクト。キーは任意。
            Property Set Elements(ByVal objElements As Collection)
                Set m_objElements = objElements
            End Property
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        public void Elements(Dictionary<string, object> objElements)
        {
            m_objElements = objElements;
        }


        //------------------------------------------------------------------------------------------
        //[C#]
        private MdlMain m_clsMdlMain;                                   //MdlMainインスタンス
        private object objElement;

        public frmSession(MdlMain clsMdlMain)
        {
            InitializeComponent();
            m_clsMdlMain = clsMdlMain;
            this.Load += Form_Load;
            return;
        }
        //------------------------------------------------------------------------------------------
        public frmSession()
        {
            InitializeComponent();
            this.Load += Form_Load;

        }


        //==========================================================================================
        /*[VB]
            '*******************************************************************************
            'イベント

            '初期化。
            Private Sub Form_Load()

                On Error GoTo ErrorHandler
    
                '変数初期化。
                Result = vbCancel
    
                'セッションリストの作成。
                Call MakeSessionList(cmbSession)
    
                '値の設定。
                Call InitializeValueList
    
                Exit Sub
    
            ErrorHandler:
                Call mdlMain.ErrorExit
    
            End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]  //2
        /// <summary>
        /// 初期化。
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form_Load(object sender, EventArgs e)  //2
        {

            mdlSession = new MdlSession(m_clsMdlMain);


            try
            {
                //'変数初期化。
                Result = vbCancel;

                //'セッションリストの作成。
                mdlSession.MakeSessionList(cmbSession);


                //'値の設定。
                InitializeValueList();

            }
            catch (Exception ex)
            {
                //ErrorHandler:
                //    Call mdlMain.ErrorExit
                _ = MessageBox.Show(ex.Message, "エラー発生", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }


        //==========================================================================================
        /*[VB]
            '終了処理。
            Private Sub Form_Unload(Cancel As Integer)

                On Error GoTo ErrorHandler
    
                Set m_objElements = Nothing
    
                Exit Sub
    
            ErrorHandler:
                Call mdlMain.ErrorExit
    
            End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]  //2
        /// <summary>
        /// 終了処理。
        /// 
        /// </summary>
        private void Form_Unload()      //2
        {
            m_objElements = null;
            this.Close();
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
        //[C#]  //2
        /// <summary>
        /// キャンセルでダイアログを終了する。
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelButton_Click(object sender, EventArgs e) //2
        {
            try
            {
                Result = vbCancel;
                Form_Unload();

            }
            catch (Exception ex)
            {
                //ErrorHandler:
                //    Call mdlMain.ErrorExit
                _ = MessageBox.Show(ex.Message, "エラー発生", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        //[C#]  //2
        /// <summary>
        /// ＯＫでダイアログを終了する。
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OKButton_Click(object sender, EventArgs e) //1
        {
            try
            {
                //'入力値の検査。
                if (!CheckData())
                {
                    return;
                }
                //'値を反映させる。
                ReflectData();
                //'終了。
                Result = vbOK;
                Form_Unload();

            }
            catch (Exception ex)
            {
                //ErrorHandler:
                //    Call mdlMain.ErrorExit
                _ = MessageBox.Show(ex.Message, "エラー発生", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        //'*******************************************************************************
        //'インプリメンテーション
        //'*******************************************************************************


        //==========================================================================================
        /*[VB]
            'セッションリストに初期値を設定する。
            Private Sub InitializeValueList()

                cmbSession.Text = ""

                Dim sSession As String
                Dim objElement As Object
                For Each objElement In m_objElements
                    If sSession = "" Then
                        sSession = objElement.Session
                    Else
                        If sSession <> objElement.Session Then Exit Sub
                    End If
                Next

                cmbSession.Text = sSession

            End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]  //2
        /// <summary>
        /// セッションリストに初期値を設定する。
        /// 
        /// </summary>
        /// 
        private void InitializeValueList()  //2
        {
            cmbSession.Text = "";
            string sSession = "";
            long nRows = 0;

            mdlSession.SelectLine = SelectLine;

            ChainList clsChainList;

            if (m_clsMdlMain.PaneSelectedTab == (long)LIST_NUM_PANE.LIST_NUM_OBSPNT)
            {
                //観測点

                clsChainList = m_clsMdlMain.GetDocument().NetworkModel().RepresentPointHead();

                ObservationPoint clsObservationPoint;

                //  foreach (object objElementA in m_objElements)
                while (clsChainList != null)
                {

                    clsObservationPoint = (ObservationPoint)clsChainList.Element;

                    if (nRows == SelectLine)
                    {
                        if (sSession == "")
                        {
                            sSession = clsObservationPoint.Session();

                        }
                        else
                        {
                            if (sSession != clsObservationPoint.Session())
                            {
                                return;
                            }
                        }
                    }
                    nRows++;
                    clsChainList = clsChainList.NextList();

                }
            }
            else if (m_clsMdlMain.PaneSelectedTab == (long)LIST_NUM_PANE.LIST_NUM_VECTOR)
            {
                //ベクトル

                clsChainList = m_clsMdlMain.GetDocument().NetworkModel().BaseLineVectorHead();

                BaseLineVector clsBaseLineVector;

                //  foreach (object objElementA in m_objElements)
                while (clsChainList != null)
                {

                    clsBaseLineVector = (BaseLineVector)clsChainList.Element;

                    if (nRows == SelectLine)
                    {
                        if (sSession == "")
                        {
                            sSession = clsBaseLineVector.Session;

                        }
                        else
                        {
                            if (sSession != clsBaseLineVector.Session)
                            {
                                return;
                            }
                        }
                    }
                    nRows++;
                    clsChainList = clsChainList.NextList();

                }
            }
            else
            {
                //座標
            }

            cmbSession.Text = sSession;

        }

        //==========================================================================================
        /*[VB]
            '入力値を検査する。
            '
            '戻り値：
            '入力値が正常である場合 True を返す。
            'それ以外の場合 False を返す。
            Private Function CheckData() As Boolean

                CheckData = False
    
                If Not CheckSessionInput(cmbSession, m_objElements) Then Exit Function
                If Not CheckSessionExtend(cmbSession.Text, m_objElements, Extend) Then Exit Function
    
                CheckData = True
    
            End Function
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]  //2
        /// <summary>
        /// '入力値を検査する。
        /// 
        /// </summary>
        /// <returns>
        /// '戻り値：
        /// '入力値が正常である場合 True を返す。
        /// 'それ以外の場合 False を返す。
        /// </returns>
        private bool CheckData()    //2
        {
            bool CheckData = false;

            foreach (object objElement2 in m_objElements)
            {
                objElement = objElement2;
            }


            if (!mdlSession.CheckSessionInput(cmbSession, objElement, true))
            {
                return CheckData;
            }

            if (!mdlSession.CheckSessionExtend(cmbSession.Text, objElement, ref Extend))
            {
                return CheckData;
            }

            CheckData = true;
            return CheckData;

        }


        //==========================================================================================
        /*[VB]
            '値を反映させる。
            Private Sub ReflectData()

                '値の取得。
                Session = cmbSession.Text
    
            End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /// <summary>
        /// 値を反映させる
        /// </summary>
        private void ReflectData()
        {
            //'値の取得。
            Session = cmbSession.Text;

        }


    }
}
