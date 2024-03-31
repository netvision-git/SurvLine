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
using static System.Net.Mime.MediaTypeNames;
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
using static SurvLine.mdl.MdlAutoOrderVector;


namespace SurvLine
{
    public partial class frmAutoOrderVector : Form
    {

        //5==========================================================================================
        /*[VB]
            '*******************************************************************************
            '基線ベクトルの向きの自動整列画面

            Option Explicit

            'プロパティ
            Public Result As Long 'ダイアログの結果。

            'インプリメンテーション
            Private m_clsAutoOrderVectorParam As New AutoOrderVectorParam '基線ベクトルの向きの自動整列パラメータ。
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#] //5

        //'基線ベクトルの向きの自動整列画面


        //'プロパティ
        public long Result;     //'ダイアログの結果。

        //'インプリメンテーション
        private AutoOrderVectorParam m_clsAutoOrderVectorParam;       //'基線ベクトルの向きの自動整列パラメータ。


        //------------------------------------------------------------------------------------------
        //[C#] //5
        private MdlMain m_clsMdlMain;                                   //MdlMainインスタンス
        public frmAutoOrderVector(MdlMain clsMdlMain)
        {
            InitializeComponent();
            m_clsMdlMain = clsMdlMain;

            Load += Form_Load;
        }
        //------------------------------------------------------------------------------------------


        //5==========================================================================================
        /*[VB]
        '*******************************************************************************
        'プロパティ

        '基線ベクトルの向きの自動整列パラメータ。
        Property Let AutoOrderVectorParam(ByVal clsAutoOrderVectorParam As AutoOrderVectorParam)
            Let m_clsAutoOrderVectorParam = clsAutoOrderVectorParam
        End Property

        '基線ベクトルの向きの自動整列パラメータ。
        Property Get AutoOrderVectorParam() As AutoOrderVectorParam
            Set AutoOrderVectorParam = m_clsAutoOrderVectorParam
        End Property
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#] //5
        //'基線ベクトルの向きの自動整列パラメータ。
        public void AutoOrderVectorParam(object clsAutoOrderVectorParam)
        {
            m_clsAutoOrderVectorParam = (AutoOrderVectorParam)clsAutoOrderVectorParam;
        }
        //'基線ベクトルの向きの自動整列パラメータ。
        public object AutoOrderVectorParam()
        {
            return m_clsAutoOrderVectorParam;
        }

        //5==========================================================================================
        /*[VB]
            '*******************************************************************************
            'イベント

            '初期化。
            Private Sub Form_Load()

                On Error GoTo ErrorHandler
    
                '変数初期化。
                Result = vbCancel
    
                '固定点リスト。
                Dim clsChainList As ChainList
                Set clsChainList = GetDocument().NetworkModel.RepresentPointHead
                Do While Not clsChainList Is Nothing
                    Dim clsObservationPoint As ObservationPoint
                    Set clsObservationPoint = clsChainList.Element
                    If clsObservationPoint.Fixed And Not clsObservationPoint.Genuine And clsObservationPoint.PrevPoint Is Nothing Then
                        Call cmbFixed.AddItem(clsObservationPoint.Number)
                    End If
                    Set clsChainList = clsChainList.NextList
                Loop
    
                '値の設定。
                Dim i As Long
                For i = 0 To cmbFixed.ListCount - 1
                    If cmbFixed.List(i) = m_clsAutoOrderVectorParam.Fixed Then
                        cmbFixed.ListIndex = i
                        Exit For
                    End If
                Next
                If cmbFixed.ListCount <= i And 0 < cmbFixed.ListCount Then cmbFixed.ListIndex = 0
    
                '先にcmbFixed.ListIndexを設定してからオプションボタンを設定する。cmbFixed_Clickの処理があるから。
                If m_clsAutoOrderVectorParam.ProcType = AUTOORDERVECTOR_PROCTYPE_ONE Then
                    optOne.Value = True
                Else
                    optAll.Value = True
                End If


                Exit Sub


            ErrorHandler:
                Call mdlMain.ErrorExit

            End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#] //5
        /// <summary>
        /// 初期化。
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form_Load(object sender, EventArgs e)
        {
            try
            {

                //m_clsAutoOrderVectorParam = new AutoOrderVectorParam();    //'基線ベクトルの向きの自動整列パラメータ。

                m_clsAutoOrderVectorParam = (AutoOrderVectorParam)m_clsMdlMain.GetDocument().AutoOrderVectorParam();
                m_clsAutoOrderVectorParam.AutoOrderVectorParam_Sub(m_clsAutoOrderVectorParam);


                //'変数初期化。
                Result = vbCancel;


                /*--------------------------------------------------
                '固定点リスト。
                Dim clsChainList As ChainList
                Set clsChainList = GetDocument().NetworkModel.RepresentPointHead
                Do While Not clsChainList Is Nothing
                    Dim clsObservationPoint As ObservationPoint
                    Set clsObservationPoint = clsChainList.Element
                    If clsObservationPoint.Fixed And Not clsObservationPoint.Genuine And clsObservationPoint.PrevPoint Is Nothing Then
                        Call cmbFixed.AddItem(clsObservationPoint.Number)
                    End If
                    Set clsChainList = clsChainList.NextList
                Loop
                 */
                ChainList clsChainList;
                clsChainList = m_clsMdlMain.GetDocument().NetworkModel().RepresentPointHead();
                while (clsChainList != null)
                {
                    ObservationPoint clsObservationPoint;

                    clsObservationPoint = (ObservationPoint)clsChainList.Element;

#if false   //デバック用
                    bool Fixe = clsObservationPoint.Fixed();    //

                    if(Fixe)
                    {
                        m_clsAutoOrderVectorParam.Fixed = "固定";    //    clsAutoOrderVectorParam.Fixed
                        m_clsAutoOrderVectorParam.ProcType = clsObservationPoint.ProcType();

                    }
                    else
                    {
                        m_clsAutoOrderVectorParam.Fixed = null;     //    clsAutoOrderVectorParam.Fixed
                    }
#endif

                    if (clsObservationPoint.Fixed() && !clsObservationPoint.Genuine() && clsObservationPoint.PrevPoint() == null)
                    {
                        _ = cmbFixed.Items.Add(clsObservationPoint.Number());
                    }
                    //--------------------------------------------------
                    clsChainList = clsChainList.NextList();
                    //--------------------------------------------------
                }

                /*--------------------------------------------------------
                    '値の設定。
                    Dim i As Long
                    For i = 0 To cmbFixed.ListCount - 1
                        If cmbFixed.List(i) = m_clsAutoOrderVectorParam.Fixed Then
                            cmbFixed.ListIndex = i
                            Exit For
                        End If
                    Next
                    If cmbFixed.ListCount <= i And 0 < cmbFixed.ListCount Then cmbFixed.ListIndex = 0
                 */
                int i;
                for (i = 0; i < cmbFixed.Items.Count; i++)
                {
                    if ((string)cmbFixed.Items[i] == m_clsAutoOrderVectorParam.Fixed)
                    {
                        cmbFixed.SelectedIndex = i;
                        break;
                    }

                }
                if (cmbFixed.Items.Count <= i && 0 < cmbFixed.Items.Count)
                {
                    cmbFixed.SelectedIndex = 0;
                }

                /*----------------------------------------------------------------
                    '先にcmbFixed.ListIndexを設定してからオプションボタンを設定する。cmbFixed_Clickの処理があるから。
                    If m_clsAutoOrderVectorParam.ProcType = AUTOORDERVECTOR_PROCTYPE_ONE Then
                        optOne.Value = True
                    Else
                        optAll.Value = True
                    End If
                */
                //'先にcmbFixed.ListIndexを設定してからオプションボタンを設定する。cmbFixed_Clickの処理があるから。
                if (m_clsAutoOrderVectorParam.ProcType == AUTOORDERVECTOR_PROCTYPE.AUTOORDERVECTOR_PROCTYPE_ONE)
                {
                    optOne.Checked = true;
                }
                else
                {
                    optAll.Checked = true;
                }


            }
            catch (Exception ex)
            {
                //ErrorHandler:
                //    Call mdlMain.ErrorExit
                _ = MessageBox.Show(ex.Message + "frmAutoOrderVector Form_Load", "エラー発生", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }


        //5==========================================================================================
        /*[VB]
            'リストの選択。
            Private Sub cmbFixed_Click()

                On Error GoTo ErrorHandler
    
                optOne.Value = True
    
                Exit Sub
    
            ErrorHandler:
                Call mdlMain.ErrorExit
    
            End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#] //5
        private void cmbFixed_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                optOne.Checked = true;
            }
            catch (Exception ex)
            {
                _ = MessageBox.Show(ex.Message, "エラー発生", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }


        //5==========================================================================================
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
        //[C#] //5
        private void CancelButton_Click(object sender, EventArgs e)
        {
            try
            {
                Close();
            }
            catch (Exception ex)
            {
                _ = MessageBox.Show(ex.Message, "エラー発生", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //5==========================================================================================
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
        //[C#] //5
        private void OKButton_Click(object sender, EventArgs e)
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

                Close();

            }
            catch (Exception ex)
            {
                _ = MessageBox.Show(ex.Message, "エラー発生", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        //5==========================================================================================
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
    
                If optOne.Value Then
                    If cmbFixed.ListIndex < 0 Then
                        Call MsgBox("固定点を選択してください。", vbCritical)
                        Call cmbFixed.SetFocus
                        Exit Function
                    End If
                ElseIf optAll.Value Then
                Else
                    Call MsgBox("始点とする固定点を選択してください。", vbCritical)
                    Exit Function
                End If
    
                CheckData = True
    
            End Function
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#] //5
        private bool CheckData()
        {
            bool CheckData = false;

            if (optOne.Checked)
            {
                if (cmbFixed.SelectedIndex < 0)
                {
                    _ = MessageBox.Show("固定点を選択してください。", "エラー発生", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    _ = cmbFixed.Focus();
                    return CheckData;
                }
            }
            else if (optAll.Checked)
            {

            }
            else
            {
                _ = MessageBox.Show("始点とする固定点を選択してください。", "エラー発生", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return CheckData;
            }
            CheckData = true;
            return CheckData;
        }



        //5==========================================================================================
        /*[VB]
            '値を反映させる。
            Private Sub ReflectData()

                '値の取得。
                If optOne.Value Then
                    m_clsAutoOrderVectorParam.ProcType = AUTOORDERVECTOR_PROCTYPE_ONE
                    m_clsAutoOrderVectorParam.Fixed = cmbFixed.List(cmbFixed.ListIndex)
                ElseIf optAll.Value Then
                    m_clsAutoOrderVectorParam.ProcType = AUTOORDERVECTOR_PROCTYPE_all
                End If
    
            End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#] //5
        private void ReflectData()
        {
            //'値の取得。
            if (optOne.Checked)
            {
                m_clsAutoOrderVectorParam.ProcType = AUTOORDERVECTOR_PROCTYPE.AUTOORDERVECTOR_PROCTYPE_ONE;
                m_clsAutoOrderVectorParam.Fixed = cmbFixed.Text;
            }
            else if (optAll.Checked)
            {
                m_clsAutoOrderVectorParam.ProcType = AUTOORDERVECTOR_PROCTYPE.AUTOORDERVECTOR_PROCTYPE_all;
            }
        }

    }
}
