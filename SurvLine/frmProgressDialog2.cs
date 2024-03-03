using SurvLine.mdl;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using static SurvLine.mdl.MdiDefine;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace SurvLine
{
    public partial class frmProgressDialog2 : Form
    {
        //interface ProgressInterface


        MdiVBfunctions mdiVBfunctions = new MdiVBfunctions();
        MdlMain mdlMain = new MdlMain();



        //'*******************************************************************************
        //'プログレスダイアログ

        //Option Explicit

        //==========================================================================================
        /*[VB]
            Implements ProgressInterface
            'プロパティ
            Public CancelError As Boolean 'キャンセル時に cdlCancel エラーを発生させるか？Ture=発生させる。False=発生させない。

            'インプリメンテーション
            Private m_bExit As Boolean 'ダイアログ終了フラグ。True=終了。False=終了しない。
            Private m_bCancel As Boolean 'キャンセルフラグ。True=キャンセル。False=続行。
            Private m_clsProcessInterface As ProcessInterface '処理インターフェース。
            Private m_nResult As Long  '結果。
            Private m_nProgressSpan As Single 'プログレスバー間の間隔。
            Private m_nProgressSpan2 As Single 'プログレスバーと次のオブジェクトの間隔。
            Private m_nPromptSpan As Single 'プロンプトとキャンセルボタンの間隔。
            Private m_nButtonBottom As Single 'キャンセルボタンの底辺。
            Private m_nHeight As Single '高さの初期値。
            Private m_clsErrInfo As New ErrInfo 'エラー情報。
            Private m_bInitialActive As Boolean '初回アクティブ。
            Private m_sPrompt As String 'プロンプト。
            Private m_bBar(1) As Boolean 'プログレスバーVisible。
            Private m_bCancelVisible As Boolean 'キャンセルボタンVisible。
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]


        //'プロパティ
        public bool CancelError;    // As Boolean 'キャンセル時に cdlCancel エラーを発生させるか？Ture=発生させる。False=発生させない。

        //'インプリメンテーション
        public bool m_bExit;                   // As Boolean 'ダイアログ終了フラグ。True=終了。False=終了しない。
        public bool m_bCancel;                 // As Boolean 'キャンセルフラグ。True=キャンセル。False=続行。
        //----------------------------------------
        private ProcessInterface processInterface = new ProcessInterface();
        private ProcessInterface m_clsProcessInterface;  // As  '処理インターフェース。


        public long m_nResult;                 // As Long  '結果。
        public Single m_nProgressSpan;         // As Single 'プログレスバー間の間隔。
        public Single m_nProgressSpan2;        // As Single 'プログレスバーと次のオブジェクトの間隔。
        public Single m_nPromptSpan;           // As Single 'プロンプトとキャンセルボタンの間隔。
        public Single m_nButtonBottom;         // As Single 'キャンセルボタンの底辺。
        public Single m_nHeight;               // As Single '高さの初期値。
        //----------------------------------------
        private ErrInfo m_clsErrInfo = new ErrInfo();   //  private m_clsErrInfo As New ErrInfo;    //'エラー情報。


        public bool m_bInitialActive;          // As Boolean '初回アクティブ。
        public string m_sPrompt;               // As String 'プロンプト。

        public bool[] m_bBar = new bool[2];    //'プログレスバーVisible。  //  Private m_bBar(1) As Boolean 'プログレスバーVisible。

        public bool m_bCancelVisible;      // As Boolean 'キャンセルボタンVisible。


        ProgressBar[] pgbProgress = new ProgressBar[2];

        //'*******************************************************************************
        //'プロパティ

        //==========================================================================================
        /*[VB]
            'キャンセル。
            Property Let Cancel(ByVal bCancel As Boolean)
                m_bCancel = bCancel
            End Property
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /// <summary>
        /// キャンセル。
        /// 
        /// </summary>
        /// <param name="bCancel"></param>
        public void Cancel(bool bCancel)
        {
            m_bCancel = bCancel;
        }

        //==========================================================================================
        /*[VB]
            'キャンセル。
            Property Get Cancel() As Boolean
                Cancel = m_bCancel
            End Property
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /// <summary>
        /// キャンセル。
        /// 
        /// </summary>
        /// <returns>
        ///     m_bCancel
        /// </returns>
        public bool Cancel()
        {
            return m_bCancel;
        }

        //==========================================================================================
        /*[VB]
            '最小位置。
            Property Let MinPos(Optional ByVal nIndex As Long = 0, ByVal nPos As Integer)
                pgbProgress(nIndex).Min = nPos
            End Property
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /// <summary>
        /// 最小位置。
        /// 
        /// </summary>
        /// <param name="nIndex"></param>
        /// <param name="nPos"></param>
        public void MinPos(long nIndex, int nPos)
        {
            pgbProgress[nIndex].Minimum = nPos;
        }


        //==========================================================================================
        /*[VB]
        '最小位置。
            Property Get MinPos(Optional ByVal nIndex As Long = 0) As Integer
                MinPos = pgbProgress(nIndex).Min
            End Property
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /// <summary>
        /// 最小位置。
        /// 
        /// </summary>
        /// <param name="nIndex"></param>
        /// <returns>
        ///     pgbProgress[nIndex].Minimum
        /// </returns>
        public int MinPos(long nIndex = 0)
        {
            return pgbProgress[nIndex].Minimum;
        }

        //==========================================================================================
        /*[VB]
            '最大位置。
            Property Let MaxPos(Optional ByVal nIndex As Long = 0, ByVal nPos As Integer)
                pgbProgress(nIndex).Max = nPos
            End Property
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /// <summary>
        /// 最大位置。
        /// 
        /// </summary>
        /// <param name="nIndex"></param>
        /// <param name="nPos"></param>
        public void MaxPos(long nIndex, int nPos)
        {
            nIndex = 0;
            pgbProgress[nIndex].Maximum = nPos;
        }

        //==========================================================================================
        /*[VB]
            '最大位置。
            Property Get MaxPos(Optional ByVal nIndex As Long = 0) As Integer
                MaxPos = pgbProgress(nIndex).Max
            End Property
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /// <summary>
        /// 最大位置。
        /// 
        /// </summary>
        /// <param name="nIndex"></param>
        /// <returns>
        ///     pgbProgress[nIndex].Maximum
        /// </returns>
        public int MaxPos(long nIndex = 0)
        {
            return pgbProgress[nIndex].Maximum;
        }

        //==========================================================================================
        /*[VB]
            '現在位置。
            Property Let CurPos(Optional ByVal nIndex As Long = 0, ByVal nPos As Integer)
                pgbProgress(nIndex).Value = nPos
            End Property
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /// <summary>
        /// 現在位置。
        /// 
        /// </summary>
        /// <param name="nIndex"></param>
        /// <param name="nPos"></param>
        public void CurPos(long nIndex, int nPos)
        {
            pgbProgress[nIndex].Value = nPos;
        }


        //==========================================================================================
        /*[VB]
            '現在位置。
            Property Get CurPos(Optional ByVal nIndex As Long = 0) As Integer
                CurPos = pgbProgress(nIndex).Value
            End Property
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /// <summary>
        /// 現在位置。
        /// 
        /// </summary>
        /// <param name="nIndex"></param>
        /// <returns>
        ///     pgbProgress[nIndex].Value
        /// </returns>
        public int CurPos(long nIndex = 0)
        {
            return pgbProgress[nIndex].Value;
        }

        //==========================================================================================
        /*[VB]
            'キャンセルの可否。
            Property Let CancelEnable(ByVal bEnable As Boolean)
                CancelButton.Enabled = bEnable
            End Property
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /// <summary>
        /// キャンセルの可否。(set)
        /// 
        /// </summary>
        /// <param name="bEnable"></param>
        public void CancelEnable(bool bEnable)
        {
#if false   //瀬戸口
            CancelButton.Enabled = bEnable;
#else
            CancelButton.Enabled = true;
#endif
        }


        //==========================================================================================
        /*[VB]
            'キャンセルの可否。
            Property Get CancelEnable() As Boolean
                CancelEnable = CancelButton.Enabled
            End Property
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /// <summary>
        /// キャンセルの可否。(get)
        /// 
        /// </summary>
        /// <returns>
        ///     CancelButton.Enabled
        /// </returns>
        public bool CancelEnable()
        {
            return CancelButton.Enabled;
        }


        //==========================================================================================
        /*[VB]
        'プロンプト。
        Property Let Prompt(ByVal sPrompt As String)
            m_sPrompt = sPrompt
            lblTextSize.Caption = sPrompt
            Do
                If lblTextSize.Height <= lblPrompt.Height + Screen.TwipsPerPixelX* 2 Then
                    If lblTextSize.Width <= lblPrompt.Width + Screen.TwipsPerPixelX* 4 Then Exit Do
                End If
                sPrompt = Mid$(sPrompt, 2)
                lblTextSize.Width = lblPrompt.Width
                lblTextSize.Caption = "..." & sPrompt
            Loop
            lblPrompt.Caption = lblTextSize.Caption
            Call Arrange
        End Property
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /// <summary>
        /// プロンプト。
        /// 
        /// </summary>
        /// <param name="sPrompt"></param>
        public void Prompt(string sPrompt)
        {
            m_sPrompt = sPrompt;
            lblTextSize.Text = sPrompt;

#if false
            for (; ; )
            {
                if (lblTextSize.Height <= lblPrompt.Height + Screen.TwipsPerPixelX * 2) //瀬戸口
                {
                    if (lblTextSize.Width <= lblPrompt.Width + Screen.TwipsPerPixelX * 4)   //瀬戸口
                    {
                        return;
                    }
//                    sPrompt = Mid$(sPrompt, 2);
                    lblTextSize.Width = lblPrompt.Width;
                    lblTextSize.Text = $"...{sPrompt}";
                }

            }
#endif
            lblPrompt.Text = lblTextSize.Text;

 //           Arrange();
        }


        //==========================================================================================
        /*[VB]
            'プロンプト。
            Property Get Prompt() As String
                Prompt = m_sPrompt
            End Property
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /// <summary>
        /// プロンプト。
        /// 
        /// </summary>
        /// <returns>
        ///     m_sPrompt
        /// </returns>
        public string Prompt()
        {
            return m_sPrompt;
        }

        //==========================================================================================
        /*[VB]
            'バー。
            Property Let Bar(Optional ByVal nIndex As Long = 0, ByVal bBar As Boolean)
                m_bBar(nIndex) = bBar
                Call Arrange
            End Property
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /// <summary>
        /// バー。
        /// 
        /// </summary>
        /// <param name="nIndex"></param>
        /// <param name="bBar"></param>
        public void Bar(long nIndex, bool bBar)
        {
            m_bBar[nIndex] = bBar;
            Arrange();
        }

        //==========================================================================================
        /*[VB]
            'バー。
            Property Get Bar(Optional ByVal nIndex As Long = 0) As Boolean
                Bar = m_bBar(nIndex)
            End Property
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /// <summary>
        /// バー。
        /// 
        /// </summary>
        /// <param name="nIndex"></param>
        /// <returns>
        ///     m_bBar
        /// </returns>
        public bool Bar(long nIndex = 0)
        {
            return m_bBar[(int)nIndex];
        }


        //==========================================================================================
        /*[VB]
            'キャンセルボタン表示。
            Property Let CancelVisible(ByVal bCancelVisible As Boolean)
                m_bCancelVisible = bCancelVisible
                Call Arrange
            End Property
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /// <summary>
        /// キャンセルボタン表示。
        /// 
        /// </summary>
        /// <param name="bCancelVisible"></param>
        public void CancelVisible(bool bCancelVisible)
        {
            m_bCancelVisible = bCancelVisible;
            Arrange();
        }

        //==========================================================================================
        /*[VB]
        'キャンセルボタン表示。
        Property Get CancelVisible() As Boolean
            CancelVisible = m_bCancelVisible
        End Property
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /// <summary>
        /// キャンセルボタン表示。
        /// 
        /// </summary>
        /// <returns>
        ///     m_bCancelVisible
        /// </returns>
        public bool CancelVisible()
        {
            return m_bCancelVisible;
        }

        //==========================================================================================
        /*[VB]
            '処理インターフェース。
            Property Set ProcessInterface(ByVal clsProcessInterface As ProcessInterface)
                Set m_clsProcessInterface = clsProcessInterface
            End Property
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /// <summary>
        /// 処理インターフェース。
        /// 
        /// </summary>
        /// <param name="clsProcessInterface"></param>
        public void Set_ProcessInterface(object clsProcessInterface)
        {
            m_clsProcessInterface = (ProcessInterface)clsProcessInterface;
            return;
        }
        //==========================================================================================
        /*[VB]
            '結果。
            Property Get Result() As Long
                Result = m_nResult
            End Property
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /// <summary>
        /// 結果。
        /// 
        /// </summary>
        /// <returns>
        ///     m_nResult
        /// </returns>
        public long Result()
        {
            return m_nResult;
        }

        //==========================================================================================
        /*[VB]
            'エラー番号。
            Property Get ErrNumber() As Long
                ErrNumber = m_clsErrInfo.Number
            End Property
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /// <summary>
        /// エラー番号。
        /// 
        /// </summary>
        /// <returns>
        ///     m_clsErrInfo.Number
        /// </returns>
        public long ErrNumber()
        {
            return m_clsErrInfo.Number;
        }

        //==========================================================================================
        /*[VB]
            'エラーソース。
            Property Get ErrSource() As String
                ErrSource = m_clsErrInfo.Source
            End Property
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /// <summary>
        /// エラーソース。
        /// 
        /// </summary>
        /// <returns>
        ///     m_clsErrInfo.Source
        /// </returns>
        public string ErrSource()
        {
            return m_clsErrInfo.Source;
        }

        //==========================================================================================
        /*[VB]
            'エラー説明。
            Property Get ErrDescription() As String
                ErrDescription = m_clsErrInfo.Description
            End Property
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /// <summary>
        /// エラー説明。
        /// </summary>
        /// <returns>
        ///     m_clsErrInfo.Description
        /// </returns>
        public string ErrDescription()
        {
            return m_clsErrInfo.Description;
        }

        //==========================================================================================
        /*[VB]
            'エラーヘルプファイル。
            Property Get ErrHelpFile() As String
                ErrHelpFile = m_clsErrInfo.HelpFile
            End Property
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /// <summary>
        /// エラーヘルプファイル。
        /// 
        /// </summary>
        /// <returns>
        ///     m_clsErrInfo.HelpFile
        /// </returns>
        public string ErrHelpFile()
        {
            return m_clsErrInfo.HelpFile;
        }

        //==========================================================================================
        /*[VB]
            'エラーコンテキスト番号。
            Property Get ErrHelpContext() As Variant
                ErrHelpContext = m_clsErrInfo.HelpContext
            End Property
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /// <summary>
        /// エラーコンテキスト番号。
        /// 
        /// </summary>
        /// <returns>
        ///     m_clsErrInfo.HelpContext
        /// </returns>
        public long ErrHelpContext()
        {
            return m_clsErrInfo.HelpContext;
        }

        //'*******************************************************************************
        //'*******************************************************************************
        /// <summary>
        /// '初期化。
        /// 
        /// </summary>
        public frmProgressDialog2()
        {
            InitializeComponent();
            Form_Initialize();
            this.Load += Form_Load;

        }


        //'*******************************************************************************
        //'イベント


        //==========================================================================================
        /*[VB]
            '初期化。
            Private Sub Form_Initialize()

                On Error GoTo ErrorHandler

                m_clsProcessInterface = processInterface;

                m_bBar(0) = True
                m_bBar(1) = False
                m_bCancelVisible = True


                Exit Sub


            ErrorHandler:
                Call mdlMain.ErrorExit

            End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /// <summary>
        /// 初期化。
        /// 
        /// </summary>
        private void Form_Initialize()
        {

            try
            {
                m_clsProcessInterface = processInterface;

                pgbProgress[0] = this.pgbProgress0;
                pgbProgress[1] = this.pgbProgress1;

                //On Error GoTo ErrorHandler

                m_bBar[0] = true;
                m_bBar[1] = false;
                m_bCancelVisible = true;

                return;

            }
            catch (Exception)
            {
                //ErrorHandler:
                mdlMain.ErrorExit();

            }
        }

        //==========================================================================================
        /*[VB]
            '初期化。
            Private Sub Form_Load()

                On Error GoTo ErrorHandler

                '変数初期化。
                CancelError = False
                m_nResult = vbAbort
                m_bExit = False
                m_bCancel = False
                m_bInitialActive = True


                m_clsErrInfo.Number = vbObjectError
                m_clsErrInfo.HelpContext = Err.HelpContext


                If Caption = "" Then Caption = App.Title


                m_nProgressSpan = pgbProgress(1).Top - pgbProgress(0).Top
                m_nProgressSpan2 = lblPrompt.Top - pgbProgress(1).Top
                m_nPromptSpan = CancelButton.Top - lblPrompt.Top
                m_nButtonBottom = CancelButton.Top + CancelButton.Height
                m_nHeight = Height

                'コントロールの再配置。
                Call Arrange


                Exit Sub


            ErrorHandler:
                Call Abort


            End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /// <summary>
        /// '初期化。
        /// </summary>
        private void Form_Load(object sender, EventArgs e)
        {
            Form_Initialize();

            try
            {
                //On Error GoTo ErrorHandler

                //'変数初期化。
                CancelError = false;
                m_nResult = DEFINE.vbAbort;
                m_bExit = false;
                m_bCancel = false;
                m_bInitialActive = true;


                m_clsErrInfo.Number = DEFINE.vbObjectError;
                //  m_clsErrInfo.HelpContext = Err.HelpContext;

                if (this.Text == "")
                {
                    string App_Title = "NS-Survey";
                    this.Text = App_Title;
                }

                m_nProgressSpan = (Single)pgbProgress[1].Top - (Single)pgbProgress[0].Top;
                m_nProgressSpan2 = lblPrompt.Top - pgbProgress[1].Top;
                m_nPromptSpan = CancelButton.Top - lblPrompt.Top;
                m_nButtonBottom = CancelButton.Top + CancelButton.Height;
                m_nHeight = this.Height;

                //'コントロールの再配置。
                Arrange();


                Activate();



                return;
            }
            catch (Exception ex)
            {
                //ErrorHandler:
                Abort();
                return;

            }
        }


        //==========================================================================================
        /*[VB]
        '終了。
        Private Sub Form_Unload(Cancel As Integer)

            On Error GoTo ErrorHandler

                Set m_clsProcessInterface = Nothing

                m_bBar(0) = True
                m_bBar(1) = False
                m_bCancelVisible = True

            Exit Sub

            ErrorHandler:
                Call Abort

            End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /// <summary>
        /// 終了。
        /// 
        /// </summary>
        /// <param name="Cancel"></param>
        private void Form_Unload(int Cancel)
        {
            try
            {
                object m_clsProcessInterface = null;
                m_bBar[0] = true;
                m_bBar[1] = false;
                m_bCancelVisible = true;

            }
            catch (Exception ex)
            {
                //ErrorHandler:
                Abort();

            }
        }


        //==========================================================================================
        /*[VB]
        '終了。
        Private Sub Form_QueryUnload(Cancel As Integer, UnloadMode As Integer)

            On Error GoTo ErrorHandler

            'ダイアログの終了が指示されていない場合は終了をキャンセルする。
            If Not m_bExit Then
            If CancelButton.Enabled Then
                '処理をキャンセル。
                Call CancelButton_Click
            End If
            '終了をキャンセル。
            Cancel = 1
            End If


        Exit Sub


        ErrorHandler:
            Call Abort


        End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //フォームの右上の×をクリックしても終了しないように設定する　
        //
        /// <summary>
        /// 終了。
        /// 
        /// </summary>
        /// <param name="Cancel"></param>
        /// <param name="UnloadMode"></param>
        private void Form_QueryUnload(int Cancel, int UnloadMode)
        {
            try
            {
                //On Error GoTo ErrorHandler

                //'ダイアログの終了が指示されていない場合は終了をキャンセルする。
                if (!m_bExit) {
                    if (CancelButton.Enabled) {
                        //'処理をキャンセル。
                        CancelButton_Click();
                    }
                }
                //'終了をキャンセル。
                Cancel = 1;

            }
            catch (Exception ex)
            {
                //ErrorHandler:
                Abort();

            }

        }

        //==========================================================================================
        /*[VB]
            '処理開始。
            Private Sub Form_Activate()

                On Error GoTo ErrorHandler

                If m_bInitialActive Then
                    m_bInitialActive = False
                    '処理。
                    Call Process
                End If
            Exit Sub

            ErrorHandler:
                Call Abort

            End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /// <summary>
        /// 処理開始。
        /// 
        /// </summary>
        public void Form_Activate()
        {
            try
            {
                //  On Error GoTo ErrorHandler

                if (m_bInitialActive)
                {
                    m_bInitialActive = false;
                    //'処理。
                    Process();
                }
            }
            catch (Exception)
            {
                //ErrorHandler:
                Abort();
            }
        }


        //==========================================================================================
        /*[VB]
            'キャンセルでダイアログを終了する。
            Private Sub CancelButton_Click()

                On Error GoTo ErrorHandler

                'キャンセル可能か問い合わせる。
                If m_clsProcessInterface.ConfirmCancel Then
                '処理のキャンセルを支持する。
                m_bCancel = True
                End If

            Exit Sub

            ErrorHandler:
                Call Abort

            End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /// <summary>
        /// キャンセルでダイアログを終了する。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelButton_Click(object sender, EventArgs e)
        {
            CancelButton_Click();
        }

        /// <summary>
        /// キャンセルでダイアログを終了する。
        /// </summary>
        private void CancelButton_Click()
        {

            try
            {
                //On Error GoTo ErrorHandler

                //'キャンセル可能か問い合わせる。
                if (m_clsProcessInterface.ConfirmCancel())
                {
                    //'処理のキャンセルを支持する。
                    m_bCancel = true;
                }
                return;

            }
            catch (Exception)
            {
                //ErrorHandler:
                Abort();

            }

        }


        //'*******************************************************************************
        //'インターフェース


        //==========================================================================================
        /*[VB]
            'キャンセル。
            Private Property Get ProgressInterface_Cancel() As Boolean
                DoEvents
                ProgressInterface_Cancel = Cancel
            End Property
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /// <summary>
        /// キャンセル。
        /// 
        /// </summary>
        /// <returns>
        ///     Cancel
        /// </returns>
        private bool ProgressInterface_Cancel()
        {
            mdiVBfunctions.DoEvents();
            return Cancel();
        }

        //==========================================================================================
        /*[VB]
            '最小位置。
            Private Property Let ProgressInterface_MinPos(Optional ByVal nIndex As Long = 0, ByVal nPos As Integer)
                MinPos(nIndex) = nPos
                DoEvents
            End Property
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /// <summary>
        /// 最小位置。
        /// 
        /// </summary>
        /// <param name="nIndex"></param>
        /// <param name="nPos"></param>
        private void ProgressInterface_MinPos(long nIndex, int nPos)
        {
            nIndex = 0;
            MinPos(nIndex, nPos);
            mdiVBfunctions.DoEvents();

        }


        //==========================================================================================
        /*[VB]
            '最小位置。
            Private Property Get ProgressInterface_MinPos(Optional ByVal nIndex As Long = 0) As Integer
                DoEvents
                ProgressInterface_MinPos = MinPos(nIndex)
            End Property
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /// <summary>
        ///  最小位置。
        /// 
        /// </summary>
        /// <param name="nIndex"></param>
        /// <returns>
        ///     MinPos
        /// </returns>
        private int ProgressInterface_MinPos(long nIndex)
        {
            mdiVBfunctions.DoEvents();
            return MinPos(nIndex);
        }


        //==========================================================================================
        /*[VB]
            '最大位置。
            Private Property Let ProgressInterface_MaxPos(Optional ByVal nIndex As Long = 0, ByVal nPos As Integer)
                MaxPos(nIndex) = nPos
                DoEvents
            End Property
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /// <summary>
        ///  最大位置。
        /// 
        /// </summary>
        private void ProgressInterface_MaxPos(long nIndex, int nPos)
        {
            nIndex = 0;
            MaxPos(nIndex, nPos);
            mdiVBfunctions.DoEvents();
        }


        //==========================================================================================
        /*[VB]
            '最大位置。
            Private Property Get ProgressInterface_MaxPos(Optional ByVal nIndex As Long = 0) As Integer
                DoEvents
                ProgressInterface_MaxPos = MaxPos(nIndex)
            End Property
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /// <summary>
        /// 最大位置。
        /// 
        /// </summary>
        /// <param name="nIndex"></param>
        /// <returns>
        ///     MaxPos
        /// </returns>
        private int ProgressInterface_MaxPos(long nIndex = 0)
        {
            mdiVBfunctions.DoEvents();
            return MaxPos(nIndex);
        }

        //==========================================================================================
        /*[VB]
            '現在位置。
            Private Property Let ProgressInterface_CurPos(Optional ByVal nIndex As Long = 0, ByVal nPos As Integer)
                CurPos(nIndex) = nPos
                DoEvents
            End Property
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /// <summary>
        /// 現在位置。
        /// 
        /// </summary>
        /// <param name="nIndex"></param>
        /// <param name="nPos"></param>
        private void ProgressInterface_CurPos(long nIndex, int nPos)
        {
            nIndex = 0;
            mdiVBfunctions.DoEvents();

            

            CurPos(nIndex, nPos);
        }

        //==========================================================================================
        /*[VB]
            '現在位置。
            Private Property Get ProgressInterface_CurPos(Optional ByVal nIndex As Long = 0) As Integer
                DoEvents
                ProgressInterface_CurPos = CurPos(nIndex)
            End Property
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /// <summary>
        /// 現在位置。
        /// 
        /// </summary>
        /// <param name="nIndex"></param>
        /// <returns>
        ///     CurPos
        /// </returns>
        public int ProgressInterface_CurPos(long nIndex)
        {
            mdiVBfunctions.DoEvents();
            nIndex = 0;
            return CurPos(nIndex);
        }

        //==========================================================================================
        /*[VB]
            'キャンセルの可否。
            Private Property Let ProgressInterface_CancelEnable(ByVal bEnable As Boolean)
                CancelEnable = bEnable
            End Property
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /// <summary>
        /// キャンセルの可否。
        /// 
        /// </summary>
        /// <param name="bEnable"></param>
        private void ProgressInterface_CancelEnable(bool bEnable)
        {
            CancelEnable(bEnable);
        }


        //==========================================================================================
        /*[VB]
            'キャンセルの可否。
            Private Property Get ProgressInterface_CancelEnable() As Boolean
                ProgressInterface_CancelEnable = CancelEnable
            End Property
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /// <summary>
        /// キャンセルの可否。
        /// 
        /// </summary>
        /// <returns>
        ///     CancelEnable
        /// </returns>
        private bool ProgressInterface_CancelEnable()
        {
            return CancelEnable();
        }


        //==========================================================================================
        /*[VB]
            'プロンプト。
            Private Property Let ProgressInterface_Prompt(ByVal sPrompt As String)
                Prompt = sPrompt
                DoEvents
            End Property
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /// <summary>
        /// プロンプト。
        /// 
        /// </summary>
        /// <param name="sPrompt"></param>
        private void ProgressInterface_Prompt(string sPrompt)
        {
            Prompt(sPrompt);
            mdiVBfunctions.DoEvents();
        }


        //==========================================================================================
        /*[VB]
            'プロンプト。
            Private Property Get ProgressInterface_Prompt() As String
                DoEvents
                ProgressInterface_Prompt = Prompt
            End Property
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /// <summary>
        /// プロンプト。
        /// 
        /// </summary>
        /// <returns>
        ///     Prompt
        /// </returns>
        private string ProgressInterface_Prompt()
        {
            mdiVBfunctions.DoEvents();
            return Prompt();
        }


        //==========================================================================================
        /*[VB]
            'キャンセルを設定する。
            Private Sub ProgressInterface_SetCancel(Optional ByVal bCancel As Boolean = True)
                Cancel = bCancel
                DoEvents
            End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /// <summary>
        /// キャンセルを設定する。
        /// 
        /// </summary>
        /// <param name="bCancel"></param>
        private void ProgressInterface_SetCancel(bool bCancel = true)
        {
            Cancel(bCancel);
            mdiVBfunctions.DoEvents();
        }


        //==========================================================================================
        /*[VB]
        'キャンセルをチェックする。
            Private Function ProgressInterface_CheckCancel() As Boolean
                DoEvents
                ProgressInterface_CheckCancel = Cancel
                If CancelError And Cancel Then Call Err.Raise(cdlCancel)
            End Function
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /// <summary>
        /// キャンセルをチェックする。
        /// 
        /// </summary>
        /// <returns></returns>
        private bool ProgressInterface_CheckCancel()
        {
            mdiVBfunctions.DoEvents();

            bool ProgressInterface_CheckCancel = Cancel();

            if (CancelError = true && Cancel())
            {
                //  Err.Raise(DEFINE.cdlCancel);
            }

            return ProgressInterface_CheckCancel;
        }



        //'*******************************************************************************
        //'インプリメンテーション


        //==========================================================================================
        /*[VB]
            '失敗終了。
            Private Sub Abort()
                'エラー情報の設定。
                Call m_clsErrInfo.SetErr
                '異常終了。
                Call EndDialog(vbAbort)
            End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /// <summary>
        /// 失敗終了。
        /// 
        /// </summary>
        private void Abort()
        {
            //'エラー情報の設定。
            m_clsErrInfo.SetErr();
            //'異常終了。
            EndDialog(DEFINE.vbAbort);
        }



        //==========================================================================================
        /*[VB]
            'コントロールの再配置。
            Private Sub Arrange()

            Dim bTop As Boolean
            Dim nBottom As Single
            nBottom = 0

            'プログレスバーの位置。
            pgbProgress(0).Visible = m_bBar(0)
            If m_bBar(0) Then
                pgbProgress(1).Top = pgbProgress(0).Top + m_nProgressSpan
                bTop = True
                nBottom = pgbProgress(0).Top + pgbProgress(0).Height
            Else
                If bTop Then
                    'ありえない。
                Else
                    pgbProgress(1).Top = pgbProgress(0).Top
                End If
            End If

            'プロンプトの位置。
            pgbProgress(1).Visible = m_bBar(1)
            If m_bBar(1) Then
                lblPrompt.Top = pgbProgress(1).Top + m_nProgressSpan2
                bTop = True
                nBottom = pgbProgress(1).Top + pgbProgress(1).Height
            Else
                If bTop Then
                    lblPrompt.Top = pgbProgress(0).Top + m_nProgressSpan2
                Else
                    lblPrompt.Top = pgbProgress(0).Top
                End If
            End If

            'キャンセルボタンの位置。
            Dim bPrompt As Boolean
            bPrompt = lblPrompt.Caption<> ""
            lblPrompt.Visible = bPrompt
            If bPrompt Then
                CancelButton.Top = lblPrompt.Top + m_nPromptSpan
                bTop = True
                nBottom = lblPrompt.Top + lblPrompt.Height
            Else
                If bTop Then
                    CancelButton.Top = pgbProgress(1).Top + m_nProgressSpan2
                Else
                    CancelButton.Top = pgbProgress(0).Top
                End If
            End If

            '高さ。
            CancelButton.Visible = m_bCancelVisible
            If m_bCancelVisible Then
                nBottom = CancelButton.Top + CancelButton.Height
            Else
            End If
            Height = m_nHeight - (m_nButtonBottom - nBottom)

            End Sub

            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /// <summary>
        /// コントロールの再配置。
        /// 
        /// </summary>
        private void Arrange()
        {
            bool bTop = false;      // As Boolean
            Single nBottom;         // As Single
            nBottom = 0;

            //'プログレスバーの位置。
            pgbProgress[0].Visible = m_bBar[0];
            if (m_bBar[0])
            {
                pgbProgress[1].Top = pgbProgress[0].Top + (int)m_nProgressSpan;
                bTop = true;
                nBottom = pgbProgress[0].Top + pgbProgress[0].Height;

            }
            else
            {
                if (bTop)
                {
                    //'ありえない。
                }
                else
                {
                    pgbProgress[1].Top = pgbProgress[0].Top;
                }
            }

            //'プロンプトの位置。
            pgbProgress[1].Visible = m_bBar[1];
            if (m_bBar[1])
            {
                lblPrompt.Top = pgbProgress[1].Top + (int)m_nProgressSpan2;
                bTop = true;
                nBottom = pgbProgress[1].Top + pgbProgress[1].Height;
            }
            else
            {
                if (bTop)
                {
                    lblPrompt.Top = pgbProgress[0].Top + (int)m_nProgressSpan2;
                }
                else
                {
                    lblPrompt.Top = pgbProgress[0].Top;
                }

            }


            //'キャンセルボタンの位置。
            bool bPrompt = false;
            // bPrompt = lblPrompt.Caption<> ""
            if (lblPrompt.Text != "")
            {
                bPrompt = true;
            }

#if false  //瀬戸口

            lblPrompt.Visible = bPrompt;
            if (bPrompt)
            {
                CancelButton.Top = lblPrompt.Top + (int)m_nPromptSpan;
                bTop = true;
                nBottom = lblPrompt.Top + lblPrompt.Height;
            }
            else
            {
                CancelButton.Top = bTop ? pgbProgress[1].Top + (int)m_nProgressSpan2 : pgbProgress[0].Top;

            }
#endif

            //'高さ。
            CancelButton.Visible = m_bCancelVisible;
            if (m_bCancelVisible)
            {
                nBottom = CancelButton.Top + CancelButton.Height;

            }

#if false  //瀬戸口
            Height = (int)(m_nHeight - (m_nButtonBottom - nBottom));
#else
            Height = this.Height;
#endif

        }

        //==========================================================================================
        /*[VB]
            '処理。
            Private Sub Process()

            On Error GoTo CancelHandler

                '処理。
                Call m_clsProcessInterface.Process(Me)

                '正常終了。
                Call EndDialog(vbOK)


            Exit Sub

            CancelHandler:

                'キャンセルでなければエラーをスルー。
                If Not CancelError Or Err.Number<> cdlCancel Then Call Err.Raise(Err.Number, Err.Source, Err.Description, Err.HelpFile, Err.HelpContext)

                'キャンセル終了。
                Call EndDialog(vbCancel)


            End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /// <summary>
        /// 処理。
        /// 
        /// </summary>
        private void Process()
        {

            //'正常終了。

            try
            {
                //On Error GoTo CancelHandler

                //'処理。
                //                Call m_clsProcessInterface.Process(Me)
                ProcessProject processProject = new ProcessProject();
                processProject.ProcessInterface_Process(m_clsProcessInterface);



                //'正常終了。
                EndDialog(DEFINE.vbOK);


            }
            catch (Exception)
            {
                //CancelHandler:

                //'キャンセルでなければエラーをスルー。   
//                if (!CancelError) || (ErrInfo.Number != DEFINE.cdlCancel )
//                {
//                    //Then Call Err.Raise(Err.Number, Err.Source, Err.Description, Err.HelpFile, Err.HelpContext)
//                }

                //'キャンセル終了。
                EndDialog(DEFINE.vbCancel);

            }

        }

        //==========================================================================================
        /*[VB]
            'ダイアログを終了させる。
            '
            '引き数：
            'nResult 結果。
            Private Sub EndDialog(ByVal nResult As Long)
                '結果。
                m_nResult = nResult
                'ダイアログの終了を指示する。
                m_bExit = True
                '終了。
                Unload Me
            End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /// <summary>
        /// 'ダイアログを終了させる。
        /// '
        /// '引き数：
        /// 'nResult 結果。
        /// 
        /// </summary>
        /// <param name="nResult"></param>
        private void EndDialog(long nResult)
        {
            //'結果。
            m_nResult = nResult;
            //'ダイアログの終了を指示する。
            m_bExit = true;
            //'終了。
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
