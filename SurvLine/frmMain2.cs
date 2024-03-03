using SurvLine.mdl;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static SurvLine.MdlAccountMakeNSS;
using static SurvLine.mdl.MdiDefine;
using static SurvLine.mdl.MdlNSDefine;
using static System.Net.Mime.MediaTypeNames;
using static System.Net.WebRequestMethods;
using static System.Windows.Forms.AxHost;

namespace SurvLine
{
    public partial class frmMain2 : Form
    {

        [DllImport("user32.dll")]
        static extern bool RedrawWindow(IntPtr hWnd, IntPtr lprcUpdate, IntPtr hrgnUpdate, int flags);

        public string receiveData = "";
        MdlUtility Utility = new MdlUtility();
        IniFileControl iniFileControl = new IniFileControl();

        Document m_clsDocument = new Document();
        MdlMain mdlMain = new MdlMain();

        MdlGUI mdlGUI = new MdlGUI();

        //23/12/29 K.setoguchi@NV---------->>>>>>>>>>
        //'2008/10/14 NGS Yamada
        //'ユーザデータを管理するフォルダのパスを追加
        //'パスの変更は、NS-Surveyの起動時のみに反省させるため、2つに別ける。
        //'各処理はこちらは参照。Documentは設定用
        //'form_loadで１度だけ同期を取る。
        public string UserDataPath;     // As String
        //<<<<<<<<<-----------23/12/29 K.setoguchi@NV


        public frmMain2()
        {
            InitializeComponent();
            Document m_clsDocument = new Document();

        }


        //*******************************************************************************
        //*******************************************************************************
        /// <summary>
        /// メインメニュー：新規現場の作成(&N)...
        ///  '引き数：
        ///  'system object：クラス階級のすべてのクラスをサポートし、派生クラスに下位レベルのサービスを提供。
        ///  'EventArgs :イベントデータを格納するクラスの基底クラスを表し、イベントデータを含まないイベントに使用する値を提供。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <returns>
        /// </returns>
        //*******************************************************************************
        private void mnuFileNew_Click(object sender, EventArgs e)
        {
            frmMain2 fmain = new frmMain2();

            //23/12/29 K.setoguchi@NV---------->>>>>>>>>>

            //            frmJobEdit2 fJobEdit = new frmJobEdit2();
            //            fJobEdit.Text = "2新規現場の作成";
            //
            //            //23/12/26 K.setoguchi@NV---------->>>>>>>>>>
            //            fJobEdit.lblFolder.Text = "";
            //            //<<<<<<<<<-----------23/12/26 K.setoguchi@NV
            //
            //
            //            fJobEdit.ShowDialog();
            //
            //            if (fJobEdit.Result != DEFINE.vbOK) { return; }
            //
            //

            //[VB]  On Error GoTo ErrorHandler

            //[VB]  '既存のプロジェクトを閉じるか確認する。
            //[VB]  If Not ConfirmCloseProject() Then Exit Sub


            //[VB]  '現場の新規作成。
            //[VB]  If Not CreateJob Then Exit Sub
            if (!CreateJob()) { return; }
            //------------------------------------------------------------




            //[VB]  'タイトル。
            //[VB]  Call UpdateTitle


            //[VB]  '砂時計。
            //[VB]  Dim clsWaitCursor As New WaitCursor
            //[VB]  Set clsWaitCursor.Object = Me


            //[VB]  'リストの作成。
            //[VB]  Call objListPane.SelectElement(Nothing)
            //[VB]  Call objListPane.RemakeList(False)
            //[VB]  
            //[VB]  
            //[VB]  'プロットの再描画。
            //[VB]  Call objPlotPane.UpdateLogicalDrawArea(True)
            //[VB]  Call objPlotPane.Redraw
            //[VB]  Call objPlotPane.Refresh


            //[VB]  '座標系番号を更新する。
            //[VB]  Call SetListIndexManual(cmbZone, m_clsDocument.CoordNum -1)

            //[VB]  'ステータスバーの更新。
            //[VB]  Call UpdateStatusBarAll


            //[VB]  'ドキュメントのOpen/Closeによるメニューの更新。
            //[VB]  Call UpdateDocumentMenu
            //[VB]  
            //[VB]  
            //[VB]  Call clsWaitCursor.Back
            //[VB]  
            //[VB]  Exit Sub
            //[VB]  
            //[VB]  ErrorHandler:
            //[VB]  Call mdlMain.ErrorExit
            //[VB]  
            //[VB]  End Sub


            //<<<<<<<<<-----------23/12/29 K.setoguchi@NV


        }

        //23/12/29 K.setoguchi@NV---------->>>>>>>>>>
        //'*******************************************************************************
        //'*******************************************************************************
        /// <summary>
        /// '現場の新規作成。
        /// '
        /// 
        /// </summary>
        /// <returns>
        /// '戻り値：
        ///     '正常終了の場合 True を返す。
        ///     'キャンセルの場合 False を返す。
        /// </returns>
        private bool CreateJob()
        {
            bool CreateJob = false;


            m_clsDocument.Class_Initialize();



            //------------------------------------------------------------
            //[VB]      '既存の値。
            //[VB]      frmJobEdit.JobName = ""
            //[VB]      frmJobEdit.DistrictName = ""
            //[VB]      frmJobEdit.Folder = ""
            frmJobEdit2 fJobEdit = new frmJobEdit2();


            fJobEdit.txtJobName.Text = "";
            fJobEdit.txtDistrictName.Text = "";
            fJobEdit.lblFolder.Text = "";


            //------------------------------------------------------------------------------------
            //[VB]      frmJobEdit.CoordNum = GetPrivateProfileInt(PROFILE_SAVE_SEC_ACCOUNT, PROFILE_SAVE_KEY_COORDNUM, 9, App.Path & "\" & App.Title & PROFILE_SAVE_EXT)
            //              PROFILE_SAVE_SEC_ACCOUNT =  "ACCOUNT"
            //              PROFILE_SAVE_KEY_COORDNUM = "COORDNUM"
            //              App.Path = "C:\Develop\NetSurv\Src\NS-App\NS-Survey"
            //              App.Title =  "NS-Survey"
            //              PROFILE_SAVE_EXT =   ".ini"
            string App_Path = @"C:\Develop\NetSurv\Src\NS-App\NS-Survey";
            string App_Title = "NS-Survey";
            fJobEdit.CoordNum = iniFileControl.GetPrivateProfileInt(MdlNSDefine.PROFILE_SAVE_SEC_ACCOUNT, MdlNSDefine.PROFILE_SAVE_KEY_COORDNUM, 9, $"{App_Path}\\{App_Title}{MdlNSSDefine.PROFILE_SAVE_EXT}");
            fJobEdit.CoordNum -= 1;
            fJobEdit.cmbZone.SelectedIndex = (int)fJobEdit.CoordNum;


            //------------------------------------------------------------------------------------
            //[VB]      frmJobEdit.GeoidoEnable = Not m_clsDocument.GeoidoPathDef = ""
            //[VB]      frmJobEdit.GeoidoPath = m_clsDocument.GeoidoPathDef
            fJobEdit.chkGeoidoEnable.Checked = true;                                       //true : "C:\Develop\パラメータファイル\gsigeo2011_ver1.asc"
            fJobEdit.txtGeoidoPath.Text = @"C:\Develop\パラメータファイル\gsigeo2011_ver1.asc"; //"C:\Develop\パラメータファイル\gsigeo2011_ver1.asc"

            if (m_clsDocument.GeoidoPathDef != "")
            {
                fJobEdit.chkGeoidoEnable.Checked = true;                                       //true : "C:\Develop\パラメータファイル\gsigeo2011_ver1.asc"
            }
            else
            {
                fJobEdit.chkGeoidoEnable.Checked = false;                                       //true : "C:\Develop\パラメータファイル\gsigeo2011_ver1.asc"

            }
            fJobEdit.txtGeoidoPath.Text = m_clsDocument.GeoidoPathDef;


            //------------------------------------------------------------------------------------
            //'セミ・ダイナミック対応。'2009/11 H.Nakamura
            //[VB]      frmJobEdit.SemiDynaEnable = False
            //[VB]      frmJobEdit.SemiDynaPath = m_clsDocument.SemiDynaPathDef
            //[VB]      frmJobEdit.SemiDynaValid = True
            fJobEdit.chkSemiDynaEnable.Checked = false;
            fJobEdit.SemiDynaPath = m_clsDocument.SemiDynaPathDef;          //"C:\Develop\パラメータファイル\SemiDyna2009.par"
            fJobEdit.txtSemiDynaPath.Text = m_clsDocument.SemiDynaPathDef;  //@"C:\Develop\パラメータファイル\SemiDyna2009.par";
            fJobEdit.SemiDynaValid = true;

            //------------------------------------------------------------------------------------
            //[VB]      //'Caption を設定すると Form_Load が走るので注意すべし！
            //[VB]      frmJobEdit.Caption = "新規現場の作成"
            fJobEdit.Text = "新規現場の作成";


            //--------------------------------------------
            //    Call frmJobEdit.Show(1)
            fJobEdit.ShowDialog();

            //--------------------------------------------
            //    If frmJobEdit.Result<> vbOK Then Exit Function
            if (fJobEdit.Result != DEFINE.vbOK) { return false; }


            //--------------------------------------------
            //    '再描画。
            //    If RedrawWindow(Me.hWnd, 0, 0, RDW_UPDATENOW) = 0 Then Call Err.Raise(ERR_FATAL, , GetLastErrorMessage())
            //
            //          //＜＜＜　坂井様お願い致します。　＞＞＞           


            //--------------------------------------------
            //[VB]    'プロジェクトフォルダの生成。
            //[VB]    On Error GoTo FileErrorHandler
            ProjectFileManager clsProjectFileManager = new ProjectFileManager();
            string sProjectFolderName = clsProjectFileManager.CreateProjectFolder();        //"0004"
            if (sProjectFolderName == "")
            {
                MessageBox.Show($"これ以上現場を保存することができません。\n 不要な現場を削除してください。", "エラー発生");
                return CreateJob;

            }

            //  C:\Develop\NetSurv\Src\NS-App\NS-Survey\UserData\0277\ObsPoint

            //--------------------------------------------
            //    On Error GoTo 0
            //   
            //    '既存の現場を閉じる。
            CloseProject();

            //--------------------------------------------
            //    '新規設定。
            m_clsDocument.SetJob(fJobEdit.JobName, fJobEdit.DistrictName, fJobEdit.CoordNum, fJobEdit.GeoidoEnable, fJobEdit.GeoidoPath, fJobEdit.SemiDynaEnable, fJobEdit.SemiDynaPath);



            //--------------------------------------------
            //    '2009/11 H.Nakamura
            //    'セミ・ダイナミックパラメータファイルのパスはデフォルトにも反映させる。
            App_Path = @"C:\Develop\NetSurv\Src\NS-App\NS-Survey";
            App_Title = "NS-Survey";
            if (m_clsDocument.m_bSemiDynaEnable)
            {
                m_clsDocument.SemiDynaPathDef = m_clsDocument.m_sSemiDynaPath;

                //--------------------------------------------
                //
                // "C:\Develop\NetSurv\Src\NS-App\NS-Survey" "\" "NS-Survey" .ini
                //      App_Path = C:\Hitz\NetSurv\Prog\Src
                //      App_Title =  "NS-Survey"  
                //
                if (!iniFileControl.WritePrivateProfileString(MdlNSDefine.PROFILE_SAVE_SEC_SEMIDYNA, MdlNSDefine.PROFILE_SAVE_KEY_PATH, m_clsDocument.SemiDynaPathDef, $"{App_Path}\\{App_Title}{MdlNSSDefine.PROFILE_SAVE_EXT}"))
                {
                    //Call Err.Raise(ERR_FATAL, , GetLastErrorMessage())
                }
            }


            //--------------------------------------------
            //    '2009/11 H.Nakamura
            //    'セミ・ダイナミックの初期化。
            MdlSemiDyna mdlSemiDyna = new MdlSemiDyna();
            mdlSemiDyna.Initialize(m_clsDocument.m_bSemiDynaEnable, m_clsDocument.m_sSemiDynaPath);



            //--------------------------------------------
            //    '保存。
            //--------  C:\Develop\NetSurv\Src\NS-App\NS-Survey\UserData\0277\data
            Save(clsProjectFileManager.GetSaveFilePath(sProjectFolderName));



            //--------------------------------------------
            //    'iniファイルに保存。
            //                      App_Path = C:\Hitz\NetSurv\Prog\Src
            //                      App_Title =  "NS-Survey"  
            string sValue;
            sValue = fJobEdit.CoordNum.ToString();
            if (iniFileControl.WritePrivateProfileString(MdlNSDefine.PROFILE_SAVE_SEC_ACCOUNT, MdlNSDefine.PROFILE_SAVE_KEY_COORDNUM, sValue, $"{App_Path}\\{App_Title}{MdlNSSDefine.PROFILE_SAVE_EXT}") == false)
            {
                //  Call Err.Raise(ERR_FATAL, , GetLastErrorMessage())
                MessageBox.Show("iniファイルに保存", "エラーが発生");
            }


            //<<<<<<<<<-----------24/01/04 K.setoguchi@NV


            return CreateJob;
        }
        //---------------------------------------------------------------------------------------------------------------
        //'現場の新規作成。
        //'
        //'戻り値：
        //'正常終了の場合 True を返す。
        //'キャンセルの場合 False を返す。
        //Private Function CreateJob() As Boolean
        //
        //   CreateJob = False
        //    
        //    '既存の値。
        //    frmJobEdit.JobName = ""
        //    frmJobEdit.DistrictName = ""
        //    frmJobEdit.Folder = ""
        //    frmJobEdit.CoordNum = GetPrivateProfileInt(PROFILE_SAVE_SEC_ACCOUNT, PROFILE_SAVE_KEY_COORDNUM, 9, App.Path & "\" & App.Title & PROFILE_SAVE_EXT)
        //    frmJobEdit.GeoidoEnable = Not m_clsDocument.GeoidoPathDef = ""
        //    frmJobEdit.GeoidoPath = m_clsDocument.GeoidoPathDef
        //    
        //    'セミ・ダイナミック対応。'2009/11 H.Nakamura
        //    frmJobEdit.SemiDynaEnable = False
        //    frmJobEdit.SemiDynaPath = m_clsDocument.SemiDynaPathDef
        //    frmJobEdit.SemiDynaValid = True
        //    
        //    'Caption を設定すると Form_Load が走るので注意すべし！
        //    frmJobEdit.Caption = "新規現場の作成"
        //
        //
        //    Call frmJobEdit.Show(1)
        //    If frmJobEdit.Result<> vbOK Then Exit Function
        //    
        //    '再描画。
        //    If RedrawWindow(Me.hWnd, 0, 0, RDW_UPDATENOW) = 0 Then Call Err.Raise(ERR_FATAL, , GetLastErrorMessage())
        //    
        //    'プロジェクトフォルダの生成。
        //    On Error GoTo FileErrorHandler
        //    Dim clsProjectFileManager As New ProjectFileManager
        //    Dim sProjectFolderName As String
        //
        //    sProjectFolderName = clsProjectFileManager.CreateProjectFolder
        //    If sProjectFolderName = "" Then
        //        Call MsgBox("これ以上現場を保存することができません。" & vbCrLf & "不要な現場を削除してください。", vbCritical)
        //        Exit Function
        //    End If
        //    On Error GoTo 0
        //   
        //    '既存の現場を閉じる。
        //    Call CloseProject
        //    
        //    '新規設定。
        //    Call m_clsDocument.SetJob(frmJobEdit.JobName, frmJobEdit.DistrictName, frmJobEdit.CoordNum, frmJobEdit.GeoidoEnable, frmJobEdit.GeoidoPath, frmJobEdit.SemiDynaEnable, frmJobEdit.SemiDynaPath)
        //    
        //    '2009/11 H.Nakamura
        //    'セミ・ダイナミックパラメータファイルのパスはデフォルトにも反映させる。
        //    If m_clsDocument.SemiDynaEnable Then
        //        m_clsDocument.SemiDynaPathDef = m_clsDocument.SemiDynaPath
        //        If WritePrivateProfileString(PROFILE_SAVE_SEC_SEMIDYNA, PROFILE_SAVE_KEY_PATH, m_clsDocument.SemiDynaPathDef, App.Path & "\" & App.Title & PROFILE_SAVE_EXT) = 0 Then Call Err.Raise(ERR_FATAL, , GetLastErrorMessage())
        //    End If
        //    
        //    '2009/11 H.Nakamura
        //    'セミ・ダイナミックの初期化。
        //    Call mdlSemiDyna.Initialize(m_clsDocument.SemiDynaEnable, m_clsDocument.SemiDynaPath)
        //    
        //    '保存。
        //    Call Save(clsProjectFileManager.GetSaveFilePath(sProjectFolderName))
        //    
        //    'iniファイルに保存。
        //    Dim sValue As String
        //    sValue = CStr(frmJobEdit.CoordNum)
        //    If WritePrivateProfileString(PROFILE_SAVE_SEC_ACCOUNT, PROFILE_SAVE_KEY_COORDNUM, sValue, App.Path & "\" & App.Title & PROFILE_SAVE_EXT) = 0 Then Call Err.Raise(ERR_FATAL, , GetLastErrorMessage())
        //
        //
        //    CreateJob = True
        //
        //
        //    Exit Function
        //
        //
        //FileErrorHandler:
        //    If Err.Number<> ERR_FILE Then Call Err.Raise(Err.Number, Err.Source, Err.Description, Err.HelpFile, Err.HelpContext)
        //    Call MsgBox(Err.Description, vbCritical)
        //
        //End Function
        //'*******************************************************************************
        //'*******************************************************************************
        //<<<<<<<<<-----------23/12/29 K.setoguchi@NV


        //24/01/04 K.setoguchi@NV---------->>>>>>>>>>
        //'*******************************************************************************
        //'*******************************************************************************
        /// <summary>
        ///インプリメンテーション
        ///
        /// プロジェクトを保存する。
        ///
        /// 引き数：
        ///     sPath 保存先ファイルのパス。
        /// </summary>
        /// <param name="sPath"></param>
        private void Save(string sPath)
        {

            //------------------------------
            //[VB]    '砂時計。
            //[VB]    Dim clsWaitCursor As New WaitCursor
            //[VB]    Set clsWaitCursor.Object = Me
            Cursor = Cursors.WaitCursor;

            //------------------------------
            //[VB]   On Error GoTo FileErrorHandler
            //[VB]   Call m_clsDocument.Save(sPath)
            //[VB]   On Error GoTo 0
            m_clsDocument.Save(sPath);

            //------------------------------
            //[VB]   Call clsWaitCursor.Back
            Cursor = Cursors.Default;

        }
        //--------------------------------
        //'インプリメンテーション
        //
        //'プロジェクトを保存する。
        //'
        //'引き数：
        //'sPath 保存先ファイルのパス。
        //Private Sub Save(ByVal sPath As String)
        //
        //    '砂時計。
        //    Dim clsWaitCursor As New WaitCursor
        //    Set clsWaitCursor.Object = Me
        //
        //    On Error GoTo FileErrorHandler
        //    Call m_clsDocument.Save(sPath)
        //    On Error GoTo 0
        //
        //
        //    Call clsWaitCursor.Back
        //
        //
        //    Exit Sub
        //
        //
        //FileErrorHandler:
        //    Call MsgBox(Err.Description, vbCritical)
        //
        //End Sub
        //'*******************************************************************************
        //'*******************************************************************************
        //<<<<<<<<<-----------24/01/04 K.setoguchi@NV



        //*******************************************************************************
        /// <summary>
        /// メインメニュー：現場を選択(&O)...
        ///  '引き数：
        ///  'system object：クラス階級のすべてのクラスをサポートし、派生クラスに下位レベルのサービスを提供。
        ///  'EventArgs :イベントデータを格納するクラスの基底クラスを表し、イベントデータを含まないイベントに使用する値を提供。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <returns>
        /// </returns>
        //*******************************************************************************
        private void mnuFileOpen_Click(object sender, EventArgs e)
        {
            //DEBUG >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
            //   MessageBox.Show(mnuFileOpen.Text);
            //DEBUG <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<

            //[VB]  On Error GoTo ErrorHandler
            //[VB]  
            //[VB]  '既存のプロジェクトを閉じるか確認する。
            //[VB]  If Not ConfirmCloseProject() Then Exit Sub
            //[VB]  
            //[VB]  
            //[VB]  'プロジェクトを開く。
            //[VB]  If Not OpenProject() Then Exit Sub

            ListPane fListPane = new ListPane();

            if (OpenProject(fListPane) == false) { return; }

            //fListPane.ShowDialog();


        }

        //***************************************
        //'タイトルを更新する。
        //Private Sub UpdateTitle()
        //    If m_clsDocument.IsEmpty Then
        //        Caption = App.Title
        //    Else
        //        Caption = App.Title & " - " & m_clsDocument.JobName
        //    End If
        //End Sub
        private void UpdateTitle()
        {

        }
        //**************************************************************************************
        //**************************************************************************************
        /// <summary>
        //'プロジェクトを開く。
        /// 引き数：
        //  frmListPane：現場の詳細データ表示フォーム。
        /// </summary>
        /// <param name="frmListPane"></param>
        /// <returns>
        //'戻り値：
        //  '正常終了の場合 True を返す。
        //  'キャンセルの場合 False を返す。
        /// </returns>
        //**************************************************************************************
        //[VB]  Private Function OpenProject() As Boolean
        private bool OpenProject(ListPane fListPane)
        {

            //[VB]---------------------------------------------------
            //[VB]      Call frmJobOpen.Show(1)
            //---------------------------------------------------
            frmJobOpen form = new frmJobOpen();
            form.fMain = this;          //サブフォームからの受信用信　this.ReceiveData
            //---------------------------------------------------
            form.ShowDialog();


            //23/12/26 K.setoguchi@NV---------->>>>>>>>>>
            //再検討            if (form.Result != MdiDefine.DEFINE.vbOK)
            //再検討            {
            //再検討            return false;
            //再検討        }
            //<<<<<<<<<-----------23/12/26 K.setoguchi@NV

            //[VB]---------------------------------------------------
            //[VB]      If frmJobOpen.Result<> vbOK Then Exit Function
            //---------------------------------------------------
            if (OpenProject_Check(this.ReceiveData, (int)form.lvProject.SelectedItems[0].Index) == false) { return false; }

            //[VB]---------------------------------------------------
            //[VB]      '再描画。
            //[VB]      If RedrawWindow(Me.hWnd, 0, 0, RDW_UPDATENOW) = 0 Then Call Err.Raise(ERR_FATAL, , GetLastErrorMessage())
            //----------------------------------.-----------------
            //再描画
            if (frmMain2.RedrawWindow(this.Handle, IntPtr.Zero, IntPtr.Zero, (int)DEFINE.RDW_UPDATENOW) == false)
            {
                return false;
            }

            //[VB]---------------------------------------------------
            //[VB]      '保存ファイルのパス。
            //[VB]      Dim sPath As String
            //[VB]      Dim clsProjectFileManager As New ProjectFileManager
            //[VB]      sPath = clsProjectFileManager.GetSaveFilePath(frmJobOpen.FolderName)
            string sPath;
            ProjectFileManager ProjectFile = new ProjectFileManager();
            ListViewItem itemx = form.lvProject.SelectedItems[0];

            //------------------------------
            //フォルダ:Ex:0006
            string FolderName = itemx.SubItems[2].Text;

            //------------------------------
            //パス　:Ex"C:\\Develop\\NetSurv\\Src\\NS-App\\NS-Survey\\UserData\\0006\\"
            sPath = ProjectFile.GetSaveFilePath(FolderName);


            //------------------------------
            //'閉じてから開く。
            //------------------------------
            //[VB]      Call CloseProject
            //------------------------------
            CloseProject();

            //-----------------------------------
            // 「XXXX.data」ファイル読み込み
            //-----------------------------------
            //[VB]      Call Load(sPath)
            //こちらは、未使用  Load_frmMain(sPath, fListPane);
            Load_frmMain(sPath);
            //------------------------------


            return true;
        }
        //***************************************
        //'プロジェクトを開く。
        //'
        //'戻り値：
        //'正常終了の場合 True を返す。
        //'キャンセルの場合 False を返す。
        //[VB]  Private Function OpenProject() As Boolean
        //[VB]  
        //[VB]      OpenProject = False
        //[VB]  
        //[VB]      Call frmJobOpen.Show(1)
        //[VB]      If frmJobOpen.Result<> vbOK Then Exit Function
        //[VB]      
        //[VB]      '再描画。
        //[VB]      If RedrawWindow(Me.hWnd, 0, 0, RDW_UPDATENOW) = 0 Then Call Err.Raise(ERR_FATAL, , GetLastErrorMessage())
        //[VB]      
        //[VB]      '保存ファイルのパス。
        //[VB]      Dim sPath As String
        //[VB]      Dim clsProjectFileManager As New ProjectFileManager
        //[VB]      sPath = clsProjectFileManager.GetSaveFilePath(frmJobOpen.FolderName)
        //[VB]      
        //[VB]      '閉じてから開く。
        //[VB]      Call CloseProject
        //[VB]      Call Load(sPath)
        //*********************************************************************************
        //**************************************************************************************


        //24/01/25 K.setoguchi@NV---------->>>>>>>>>>
        //***************************************************************************

        //==========================================================================================
        /*[VB]
        '砂時計。
          
            On Error GoTo FileErrorHandler
            Call m_clsDocument.Load(sPath)
            On Error GoTo 0
          
          
            Call clsWaitCursor.Back
          
          
          
        FileErrorHandler:
            Call MsgBox(Err.Description, vbCritical)
            Call m_clsDocument.Clear
          
            End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //          （注意事項）Load_frmMain関数は、２つ存在する
        /// <summary>
        ///  プロジェクトを読み込む
        ///  引き数：
        ///  sPath 読み込み元ファイルのパス。
        /// </summary>
        /// <param name="sPath"></param>
        private void Load_frmMain(string sPath)
        {
            //'砂時計。
            Cursor = Cursors.WaitCursor;

            try
            {
                m_clsDocument.Load(sPath);

            }
            catch (Exception)
            {
                //FileErrorHandler:
                //[VB]          Call MsgBox(Err.Description, vbCritical)
                m_clsDocument.Clear();

            }

            Cursor = Cursors.Default;

        }
        //<<<<<<<<<-----------24/01/25 K.setoguchi@NV
        //***************************************************************************


        //**************************************************************************************
        //**************************************************************************************
        /// <summary>
        ///  プロジェクトを読み込む
        ///  引き数：
        ///  sPath 読み込み元ファイルのパス。
        /// </summary>
        /// <param name="sPath"></param>
        //**************************************************************************************
        private void Load_frmMain(string sPath, ListPane listPane)
        {
            //[VB]      '砂時計。
            //[VB]          Dim clsWaitCursor As New WaitCursor
            //[VB]          Set clsWaitCursor.Object = Me
            Cursor = Cursors.WaitCursor;

            //Document m_clsDocument = new Document();  //24/01/25 K.setoguchi@NV

            //23/12/29 K.setoguchi@NV---------->>>>>>>>>>
            //m_clsDocument = GetDocument();
            //[VB]  m_clsChangeSelRow.Value = True
            //[VB]  UserDataPath = m_clsDocument.UserDataPath;  //'2008/10/14 NGS Yamada
            UserDataPath = @"C:\Develop\NetSurv\Src\NS-App\NS-Survey\UserData\";
            //<<<<<<<<<-----------23/12/29 K.setoguchi@NV

            //[VB]          On Error GoTo FileErrorHandler
            m_clsDocument.Load(sPath);                  //24/01/25 K.setoguchi@NV
            //[VB]          On Error GoTo 0
#if false   
//******************************************************************:
//坂井様へ　下記の処理は　ＮＧ　です、表示処理をお願いします
//******************************************************************:
            try
            {
                //サンプル>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
                //   GENBA_STRUCT_S Genba_S = new GENBA_STRUCT_S();
                //サンプル <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<


                //-----------------------------------
                // 「XXXX.data」ファイル読み込み
                //-----------------------------------

                List<GENBA_STRUCT_S> List_Genba_S = new List<GENBA_STRUCT_S>();

                m_clsDocument.Load(sPath, ref List_Genba_S);        //24/01/25 K.setoguchi@NV---------->>>>>>>>>>


                //-----------------------------------
                // リスト表示（観測点・ベクトル表示）
                //-----------------------------------
                listPane.ListDataDispMain();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "エラーが発生");
                m_clsDocument.Clear();
                return;
            }
#endif
        }
        //[VB]---------------------------------------------------
        //[VB]  'プロジェクトを読み込む。
        //[VB]  '
        //[VB]  '引き数：
        //[VB]  'sPath 読み込み元ファイルのパス。
        //[VB]  Private Sub Load(ByVal sPath As String)
        //[VB]  
        //[VB]      '砂時計。
        //[VB]          Dim clsWaitCursor As New WaitCursor
        //[VB]          Set clsWaitCursor.Object = Me
        //[VB]  
        //[VB]          On Error GoTo FileErrorHandler
        //[VB]          Call m_clsDocument.Load(sPath)
        //[VB]          On Error GoTo 0
        //[VB]  
        //[VB]  
        //[VB]          Call clsWaitCursor.Back
        //[VB]  
        //[VB]  
        //[VB]          Exit Sub
        //[VB]  
        //[VB]  FileErrorHandler:
        //[VB]          Call MsgBox(Err.Description, vbCritical)
        //[VB]          Call m_clsDocument.Clear
        //[VB]  
        //[VB]          End Sub
        //**************************************************************************************
        //**************************************************************************************




        //**************************************************************************************
        //**************************************************************************************
        /// <summary>
        /// プロジェクトを閉じる。
        /// 
        /// </summary>
        private void CloseProject()
        {
            //[VB]---------------------------------------------------
            //[VB]      '砂時計。
            //[VB]      Dim clsWaitCursor As New WaitCursor
            //[VB]      Set clsWaitCursor.Object = Me
            Cursor = Cursors.WaitCursor;

            //[VB]      'クリア。
            //[VB]      Call m_clsDocument.Clear

            m_clsDocument.Clear();

            //[VB]      '座標系番号を更新する。
            //[VB]      Call SetListIndexManual(cmbZone, m_clsDocument.CoordNum - 1)
            //
            //MdlGUI mdlGUI = new MdlGUI();
            //mdlGUI.SetListIndexManual(cmbZone, m_clsDocument.CoordNum - 1);

            //[VB]      Call clsWaitCursor.Back
            Cursor = Cursors.Default;


        }
        //---------------------------------------------------------------------------------------
        //[VB]    'プロジェクトを閉じる。
        //[VB]  Private Sub CloseProject()
        //[VB]    
        //[VB]      '砂時計。
        //[VB]      Dim clsWaitCursor As New WaitCursor
        //[VB]      Set clsWaitCursor.Object = Me
        //[VB]        
        //[VB]      'クリア。
        //[VB]      Call m_clsDocument.Clear
        //[VB]        
        //[VB]      '座標系番号を更新する。
        //[VB]      Call SetListIndexManual(cmbZone, m_clsDocument.CoordNum - 1)
        //[VB]    
        //[VB]    
        //[VB]      Call clsWaitCursor.Back
        //[VB]    
        //[VB]
        //[VB]  End Sub
        //**************************************************************************************
        //**************************************************************************************

        //**************************************************************************************
        //**************************************************************************************
        private bool OpenProject_Check(string sRecvData, int nIndex)
        {
            //---------------------------------------------------
            //サブフォームが「OK」ボタンにより処理を行う
            //---------------------------------------------------
            if (sRecvData == "OK")
            {

                try
                {
                    int itemxsle2 = nIndex;
                    if (itemxsle2 >= 0)
                    {
                        //  //*** 選択 OK ***
                        //  //DEBUG >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
                        //  ListViewItem itemx = form.lvProject.SelectedItems[0];
                        //  MessageBox.Show(itemx.Text + " | " + itemx.SubItems[1].Text + " | " + itemx.SubItems[2].Text);
                        //  //DEBUG <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<

                    }
                }
                catch (ArgumentOutOfRangeException)
                {
                    MessageBox.Show("「現場の選択画面」の操作を確認して下さい ");
                    return false;
                }
            }
            else
            {
                return false;
            }

            return true;
        }
        //**************************************************************************************
        //**************************************************************************************



        //24/01/25 K.setoguchi@NV---------->>>>>>>>>>
        //***************************************************************************
        //==========================================================================================
        /*[VB]
        'プロジェクトを閉じる。
        Private Sub mnuFileClose_Click()

            On Error GoTo ErrorHandler
    
            '既存のプロジェクトを閉じるか確認する。
            If Not ConfirmCloseProject() Then Exit Sub
    
            'プロジェクトを閉じる。
            Call CloseProject
    
            'タイトル。
            Call UpdateTitle
    
            '砂時計。
            Dim clsWaitCursor As New WaitCursor
            Set clsWaitCursor.Object = Me
    
            'リストの作成。
            Call objListPane.RemakeList(False)
    
            'プロットの再描画。
            Call objPlotPane.UpdateLogicalDrawArea(True)
            Call objPlotPane.Redraw
            Call objPlotPane.Refresh
    
            'ステータスバーの更新。
            Call UpdateStatusBarAll
    
            'ドキュメントのOpen/Closeによるメニューの更新。
            Call UpdateDocumentMenu


            Call clsWaitCursor.Back

            Exit Sub

        ErrorHandler:
            Call mdlMain.ErrorExit

        End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /// <summary>
        /// プロジェクトを閉じる。
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuFileClose_Click(object sender, EventArgs e)
        {

            //On Error GoTo ErrorHandler


            //坂井様へ  '既存のプロジェクトを閉じるか確認する。
            //坂井様へ  If Not ConfirmCloseProject() Then Exit Sub


            //'プロジェクトを閉じる。
            CloseProject();


            //'タイトル。
            UpdateTitle();


            //'砂時計。
            Cursor = Cursors.WaitCursor;

            //坂井様へ  'リストの作成。
            //坂井様へ  Call objListPane.RemakeList(False)

            //坂井様へ  'プロットの再描画。
            //坂井様へ  Call objPlotPane.UpdateLogicalDrawArea(True)
            //坂井様へ  Call objPlotPane.Redraw
            //坂井様へ  Call objPlotPane.Refresh


            //坂井様へ  'ステータスバーの更新。
            //坂井様へ  Call UpdateStatusBarAll


            //'ドキュメントのOpen/Closeによるメニューの更新。
            //坂井様へ  UpdateDocumentMenu();


            Cursor = Cursors.Default;


        }
        //<<<<<<<<<-----------24/01/25 K.setoguchi@NV
        //***************************************************************************


        //==========================================================================================
        /*[VB]
        'プロジェクトを上書き保存する。
        Private Sub mnuFileSave_Click()

            On Error GoTo ErrorHandler
    
            'プロジェクトの上書き保存。
            If Not SaveProject() Then Exit Sub
    
            Exit Sub
    
        ErrorHandler:
            Call mdlMain.ErrorExit
    
        End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /// <summary>
        /// プロジェクトを上書き保存する。
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuFileSave_Click(object sender, EventArgs e)
        {
            //On Error GoTo ErrorHandler

            //'プロジェクトの上書き保存。
            if (!SaveProject())
            {
                return;
            }

            return;

            //ErrorHandler:
            //    Call mdlMain.ErrorExit


        }

        //==========================================================================================
        /*[VB]
        'プロジェクトを上書き保存する。
        '
        '戻り値：
        '正常終了の場合 True を返す。
        'キャンセルの場合 False を返す。
        Private Function SaveProject() As Boolean

            SaveProject = False
    
            If m_clsDocument.Path = "" Then
                'If Not SaveAsProject() Then Exit Function
            Else
                Call Save(m_clsDocument.Path)
            End If
    
            SaveProject = True
    
        End Function            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /// <summary>
        ///'プロジェクトを上書き保存する。
        ///'
        /// </summary>
        /// <returns>
        ///'戻り値：
        ///'正常終了の場合 True を返す。
        ///'キャンセルの場合 False を返す。
        /// </returns>
        private bool SaveProject()
        {
            bool SaveProject = false;

            if (m_clsDocument.Path() == "")
            {
                if (!SaveAsProject())
                {
                    return SaveProject;
                }
            }
            else
            {
                Save(m_clsDocument.Path());
            }

            SaveProject = true;
            return SaveProject;

        }


        //==========================================================================================
        /*[VB]
        'プロジェクトを名前を付けて保存する。
        Private Sub mnuFileSaveAs_Click()

            On Error GoTo ErrorHandler
    
            'プロジェクトを名前を付けて保存。
            If Not SaveAsProject() Then Exit Sub
    
            'タイトル。
            Call UpdateTitle
    
            '座標系番号を更新する。
            Call SetListIndexManual(cmbZone, m_clsDocument.CoordNum - 1)
    
            '砂時計。
            Dim clsWaitCursor As New WaitCursor
            Set clsWaitCursor.Object = Me
    
            'プロットの再描画。
            Call objPlotPane.UpdateLogicalDrawArea(False)
            Call objPlotPane.Redraw
            Call objPlotPane.Refresh
    
            'ステータスバーの更新。
            Call UpdateStatusBar(STATUSBAR_NUM_COORDNUM)
            Call UpdateStatusBar(STATUSBAR_NUM_GEOIDO)
    
            Exit Sub
    
        ErrorHandler:
            Call mdlMain.ErrorExit
    
        End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /// <summary>
        /// 'プロジェクトを名前を付けて保存する。
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuFileSaveAs_Click(object sender, EventArgs e)
        {
            // MessageBox.Show(mnuFileSaveAs.Text);

            //'プロジェクトを名前を付けて保存。
            if (!SaveAsProject())
            {
                return;
            }


            //'タイトル。
            UpdateTitle();


            //'座標系番号を更新する。
            //Call SetListIndexManual(cmbZone, m_clsDocument.CoordNum -1)

            //'砂時計。
            Cursor = Cursors.WaitCursor;


            //'プロットの再描画。
            //Call objPlotPane.UpdateLogicalDrawArea(False)
            //Call objPlotPane.Redraw
            //Call objPlotPane.Refresh


            //'ステータスバーの更新。
            //Call UpdateStatusBar(STATUSBAR_NUM_COORDNUM)
            //Call UpdateStatusBar(STATUSBAR_NUM_GEOIDO)

        }

        //==========================================================================================
        /*[VB]
        'プロジェクトを名前を付けて保存する。
        '
        '戻り値：
        '正常終了の場合 True を返す。
        'キャンセルの場合 False を返す。
        Private Function SaveAsProject() As Boolean

            SaveAsProject = False
    
            '既存の値。
            frmJobEdit.JobName = m_clsDocument.JobName
            frmJobEdit.DistrictName = m_clsDocument.DistrictName
            frmJobEdit.Folder = ""
            frmJobEdit.CoordNum = m_clsDocument.CoordNum
            frmJobEdit.GeoidoEnable = m_clsDocument.GeoidoEnable
            frmJobEdit.GeoidoPath = m_clsDocument.GeoidoPath
    
            'セミ・ダイナミック対応。'2009/11 H.Nakamura
            frmJobEdit.SemiDynaEnable = m_clsDocument.SemiDynaEnable
            frmJobEdit.SemiDynaPath = m_clsDocument.SemiDynaPath
            frmJobEdit.SemiDynaValid = False
    
            'Caption を設定すると Form_Load が走るので注意すべし！
            frmJobEdit.Caption = "名前を付けて保存"
    
            Call frmJobEdit.Show(1)
            If frmJobEdit.Result <> vbOK Then Exit Function
    
            '再描画。
            If RedrawWindow(Me.hWnd, 0, 0, RDW_UPDATENOW) = 0 Then Call Err.Raise(ERR_FATAL, , GetLastErrorMessage())
    
            'プロジェクトフォルダの生成。
            On Error GoTo FileErrorHandler
            Dim clsProjectFileManager As New ProjectFileManager
            Dim sProjectFolderName As String
            sProjectFolderName = clsProjectFileManager.CreateProjectFolder
            If sProjectFolderName = "" Then
                Call MsgBox("これ以上現場を保存することができません。" & vbCrLf & "不要な現場を削除してください。", vbCritical)
                Exit Function
            End If
            On Error GoTo 0
    
            '設定。
            Call m_clsDocument.SetJob(frmJobEdit.JobName, frmJobEdit.DistrictName, frmJobEdit.CoordNum, frmJobEdit.GeoidoEnable, frmJobEdit.GeoidoPath, frmJobEdit.SemiDynaEnable, frmJobEdit.SemiDynaPath)
    
            '保存。
            Call Save(clsProjectFileManager.GetSaveFilePath(sProjectFolderName))
    
            SaveAsProject = True
    
            Exit Function
    
        FileErrorHandler:
            If Err.Number <> ERR_FILE Then Call Err.Raise(Err.Number, Err.Source, Err.Description, Err.HelpFile, Err.HelpContext)
            Call MsgBox(Err.Description, vbCritical)
    
        End Function
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /// <summary>
        /// 'プロジェクトを名前を付けて保存する。
        /// '
        /// '戻り値：
        /// '正常終了の場合 True を返す。
        /// 'キャンセルの場合 False を返す。
        /// 
        /// </summary>
        /// <returns>
        /// '戻り値：
        /// '正常終了の場合 True を返す。
        /// 'キャンセルの場合 False を返す。
        /// </returns>
        private bool SaveAsProject()
        {
            bool SaveAsProject = false;

            frmJobEdit2 frmJobEdit2 = new frmJobEdit2();


            //'既存の値。
            frmJobEdit2.JobName = m_clsDocument.JobName();
            frmJobEdit2.DistrictName = m_clsDocument.DistrictName();
            frmJobEdit2.Folder = "";
            frmJobEdit2.CoordNum = m_clsDocument.CoordNum();
            frmJobEdit2.GeoidoEnable = m_clsDocument.GeoidoEnable();
            frmJobEdit2.GeoidoPath = m_clsDocument.GeoidoPath();


            //'セミ・ダイナミック対応。'2009 / 11 H.Nakamura
            frmJobEdit2.SemiDynaEnable = m_clsDocument.SemiDynaEnable();
            frmJobEdit2.SemiDynaPath = m_clsDocument.SemiDynaPath();
            frmJobEdit2.SemiDynaValid = false;

            //'Caption を設定すると Form_Load が走るので注意すべし！
            frmJobEdit2.Text = "名前を付けて保存";

            frmJobEdit2.ShowDialog();
            if (frmJobEdit2.Result != DEFINE.vbOK) { return false; }

            //'再描画。
            //If RedrawWindow(Me.hWnd, 0, 0, RDW_UPDATENOW) = 0 Then Call Err.Raise(ERR_FATAL, , GetLastErrorMessage())


            //'プロジェクトフォルダの生成。
            ProjectFileManager clsProjectFileManager = new ProjectFileManager();
            string sProjectFolderName;
            sProjectFolderName = clsProjectFileManager.CreateProjectFolder();
            if (sProjectFolderName == "")
            {
                MessageBox.Show($"これ以上現場を保存することができません。\n 不要な現場を削除してください。", "エラー発生");
                return SaveAsProject;
            }

            //'設定。
            m_clsDocument.SetJob(frmJobEdit2.JobName, frmJobEdit2.DistrictName, frmJobEdit2.CoordNum, frmJobEdit2.GeoidoEnable, frmJobEdit2.GeoidoPath, frmJobEdit2.SemiDynaEnable, frmJobEdit2.SemiDynaPath);

            //'保存。
            Save(clsProjectFileManager.GetSaveFilePath(sProjectFolderName));


            SaveAsProject = true;
            return SaveAsProject;
        }


        //==========================================================================================
        /*[VB]
        private void mnuFileEdit_Click(object sender, EventArgs e)
        {

            On Error GoTo ErrorHandler


            '現場の新規作成。
            Call EditJob


            'タイトル。
            Call UpdateTitle


            '座標系番号を更新する。
            Call SetListIndexManual(cmbZone, m_clsDocument.CoordNum -1)
    
            '砂時計。
            Dim clsWaitCursor As New WaitCursor
            Set clsWaitCursor.Object = Me


            'プロットの再描画。
            Call objPlotPane.UpdateLogicalDrawArea(False)
            Call objPlotPane.Redraw
            Call objPlotPane.Refresh


            'メニューの更新。
            mnuAccountSemiDynaGAN2KON.Enabled = (mdlSemiDyna.m_nSemiDyna<> 0) 'セミ・ダイナミック対応。'2009 / 11 H.Nakamura


            'ステータスバーの更新。
            Call UpdateStatusBar(STATUSBAR_NUM_COORDNUM)
            Call UpdateStatusBar(STATUSBAR_NUM_GEOIDO)


            Exit Sub


        ErrorHandler:
                    Call mdlMain.ErrorExit

                End Sub

        }
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /// <summary>
        /// 'プロジェクトを編集する。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        private void mnuFileEdit_Click(object sender, EventArgs e)
        {

            //現場の新規作成。
            EditJob(sender);


            //'タイトル。
            UpdateTitle();

            //'座標系番号を更新する。
            //  SetListIndexManual(cmbZone, m_clsDocument.CoordNum - 1);

            //'砂時計。
            Cursor = Cursors.WaitCursor;


            //'プロットの再描画。
            //Call objPlotPane.UpdateLogicalDrawArea(False)
            //Call objPlotPane.Redraw
            //Call objPlotPane.Refresh


            //'メニューの更新。
            //mnuAccountSemiDynaGAN2KON.Enabled = (mdlSemiDyna.m_nSemiDyna<> 0) 'セミ・ダイナミック対応。'2009 / 11 H.Nakamura


            //'ステータスバーの更新。
            //UpdateStatusBar(STATUSBAR_NUM_COORDNUM);
            //UpdateStatusBar(STATUSBAR_NUM_GEOIDO);


        }

        //==========================================================================================
        /*[VB]
        '現場の編集。
        '
        '戻り値：
        '正常終了の場合 True を返す。
        'キャンセルの場合 False を返す。
        Private Function EditJob() As Boolean

            EditJob = False

            '既存の値。
            frmJobEdit.JobName = m_clsDocument.JobName
            frmJobEdit.DistrictName = m_clsDocument.DistrictName
            frmJobEdit.Folder = m_clsDocument.Folder
            frmJobEdit.CoordNum = m_clsDocument.CoordNum
            frmJobEdit.GeoidoEnable = m_clsDocument.GeoidoEnable
            frmJobEdit.GeoidoPath = m_clsDocument.GeoidoPath


            'セミ・ダイナミック対応。'2009/11 H.Nakamura
            frmJobEdit.SemiDynaEnable = m_clsDocument.SemiDynaEnable
            frmJobEdit.SemiDynaPath = m_clsDocument.SemiDynaPath
            frmJobEdit.SemiDynaValid = True


            Call frmJobEdit.Show(1)
            If frmJobEdit.Result<> vbOK Then Exit Function

            '再描画。
            If RedrawWindow(Me.hWnd, 0, 0, RDW_UPDATENOW) = 0 Then Call Err.Raise(ERR_FATAL, , GetLastErrorMessage())

            '設定。
            Call m_clsDocument.SetJob(frmJobEdit.JobName, frmJobEdit.DistrictName, frmJobEdit.CoordNum, frmJobEdit.GeoidoEnable, frmJobEdit.GeoidoPath, frmJobEdit.SemiDynaEnable, frmJobEdit.SemiDynaPath)

            '2009/11 H.Nakamura
            'セミ・ダイナミックパラメータファイルのパスはデフォルトにも反映させる。
            If m_clsDocument.SemiDynaEnable Then
                m_clsDocument.SemiDynaPathDef = m_clsDocument.SemiDynaPath
                If WritePrivateProfileString(PROFILE_SAVE_SEC_SEMIDYNA, PROFILE_SAVE_KEY_PATH, m_clsDocument.SemiDynaPathDef, App.Path & "\" & App.Title & PROFILE_SAVE_EXT) = 0 Then Call Err.Raise(ERR_FATAL, , GetLastErrorMessage())
            End If

            '2009/11 H.Nakamura
            'セミ・ダイナミックの初期化。
            Call mdlSemiDyna.Initialize(m_clsDocument.SemiDynaEnable, m_clsDocument.SemiDynaPath)

            EditJob = True


        End Function

            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        private bool EditJob(object sender)
        {

            bool EditJob = false;


            //frmJobEdit2 frmJobEdit2 = (frmJobEdit2)sender;
            frmJobEdit2 frmJobEdit2 = new frmJobEdit2();

            //'既存の値。
            frmJobEdit2.JobName = m_clsDocument.JobName();
            frmJobEdit2.DistrictName = m_clsDocument.DistrictName();
            frmJobEdit2.Folder = m_clsDocument.Folder();
            frmJobEdit2.CoordNum = m_clsDocument.CoordNum();
            frmJobEdit2.GeoidoEnable = m_clsDocument.GeoidoEnable();
            frmJobEdit2.GeoidoPath = m_clsDocument.GeoidoPath();


            //'セミ・ダイナミック対応。'2009 / 11 H.Nakamura
            frmJobEdit2.SemiDynaEnable = m_clsDocument.SemiDynaEnable();
            frmJobEdit2.SemiDynaPath = m_clsDocument.SemiDynaPath();
            frmJobEdit2.SemiDynaValid = true;


            frmJobEdit2.ShowDialog();
            if (frmJobEdit2.Result != DEFINE.vbOK) { return false; }



            //再描画
            if (frmMain2.RedrawWindow(this.Handle, IntPtr.Zero, IntPtr.Zero, (int)DEFINE.RDW_UPDATENOW) == false)
            {
                return false;
            }

            //'設定。
            m_clsDocument.SetJob(frmJobEdit2.JobName, frmJobEdit2.DistrictName, frmJobEdit2.CoordNum, frmJobEdit2.GeoidoEnable, frmJobEdit2.GeoidoPath, frmJobEdit2.SemiDynaEnable, frmJobEdit2.SemiDynaPath);


            //'2009/11 H.Nakamura
            //'セミ・ダイナミックパラメータファイルのパスはデフォルトにも反映させる。
            string App_Path = @"C:\Develop\NetSurv\Src\NS-App\NS-Survey";
            string App_Title = "NS-Survey";
            if (m_clsDocument.m_bSemiDynaEnable)
            {
                m_clsDocument.SemiDynaPathDef = m_clsDocument.m_sSemiDynaPath;

                //--------------------------------------------
                //
                // "C:\Develop\NetSurv\Src\NS-App\NS-Survey" "\" "NS-Survey" .ini
                //      App_Path = C:\Hitz\NetSurv\Prog\Src
                //      App_Title =  "NS-Survey"  
                //
                if (!iniFileControl.WritePrivateProfileString(MdlNSDefine.PROFILE_SAVE_SEC_SEMIDYNA, MdlNSDefine.PROFILE_SAVE_KEY_PATH, m_clsDocument.SemiDynaPathDef, $"{App_Path}\\{App_Title}{MdlNSSDefine.PROFILE_SAVE_EXT}"))
                {
                    //Call Err.Raise(ERR_FATAL, , GetLastErrorMessage())
                }
            }


            //'2009/11 H.Nakamura
            //'セミ・ダイナミックの初期化。
            MdlSemiDyna mdlSemiDyna = new MdlSemiDyna();
            mdlSemiDyna.Initialize(m_clsDocument.m_bSemiDynaEnable, m_clsDocument.m_sSemiDynaPath);


            return EditJob;
        }


        private void mnuCheck_Click(object sender, EventArgs e)
        {

        }

        private void mnuAnalysis_Click(object sender, EventArgs e)
        {

        }

        private void mnuAccount_Click(object sender, EventArgs e)
        {

        }

        private void frmMain_Load(object sender, EventArgs e)
        {

        }

        private void mnuAccountEtc_Click(object sender, EventArgs e)
        {

        }
        public string ReceiveData
        {
            set
            {
                receiveData = value;
            }
            get
            {
                return receiveData;
            }
        }

        private void toolBar1_ButtonClick(object sender, ToolBarButtonClickEventArgs e)
        {

        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {

        }

        private void 新規現場作成ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void toolStripSplitButton1_ButtonClick(object sender, EventArgs e)
        {

        }

        //観測点／ベクトル
        private void listPane1_Load(object sender, EventArgs e)
        {



        }

        private void mnuFile_Click(object sender, EventArgs e)
        {

        }





        //24/01/24 K.setoguchi@NV---------->>>>>>>>>>
        //***************************************************************************

        //==========================================================================================
        /*[VB]
        'プロジェクトを削除する。
        Private Sub mnuFileRemove_Click()

            On Error GoTo ErrorHandler
    
            '既存のプロジェクトを閉じる。
            If Not ConfirmCloseProject() Then Exit Sub
    
            '現場の削除。
            If Not RemoveJob Then Exit Sub
    
            'タイトル。
            Call UpdateTitle
    
            '砂時計。
            Dim clsWaitCursor As New WaitCursor
            Set clsWaitCursor.Object = Me
    
            'リストの作成。
            Call objListPane.RemakeList(False)
    
            'プロットの再描画。
            Call objPlotPane.UpdateLogicalDrawArea(True)
            Call objPlotPane.Redraw
            Call objPlotPane.Refresh
    
            'ステータスバーの更新。
            Call UpdateStatusBarAll
    
            'ドキュメントのOpen/Closeによるメニューの更新。
            Call UpdateDocumentMenu
    
            Call clsWaitCursor.Back
    
            Exit Sub
    
        ErrorHandler:
            Call mdlMain.ErrorExit

        End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /// <summary>
        /// 現場を削除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuFileRemove_Click(object sender, EventArgs e)
        {
            //坂井様へ  //On Error GoTo ErrorHandler  


            //坂井様へ //'既存のプロジェクトを閉じる。
            //坂井様へ //If Not ConfirmCloseProject() Then Exit Sub


            //'現場の削除。
            if (!RemoveJob())
            {
                return;
            }


            //'タイトル。
            UpdateTitle();


            //'砂時計。
            Cursor = Cursors.WaitCursor;


            //坂井様へ'リストの作成。
            //坂井様へCall objListPane.RemakeList(False)


            //坂井様へ'プロットの再描画。
            //坂井様へCall objPlotPane.UpdateLogicalDrawArea(True)
            //坂井様へCall objPlotPane.Redraw
            //坂井様へCall objPlotPane.Refresh


            //坂井様へ'ステータスバーの更新。
            //坂井様へCall UpdateStatusBarAll


            //坂井様へ'ドキュメントのOpen/Closeによるメニューの更新。
            //坂井様へCall UpdateDocumentMenu


            Cursor = Cursors.Default;


        }
        //<<<<<<<<<-----------24/01/24 K.setoguchi@NV
        //***************************************************************************


        //24/01/24 K.setoguchi@NV---------->>>>>>>>>>
        //***************************************************************************
        //==========================================================================================
        /*[VB]
        '現場の削除。
        '
        '戻り値：
        '正常終了の場合 True を返す。
        'キャンセルの場合 False を返す。
        Private Function RemoveJob() As Boolean

            RemoveJob = False
    
            On Error GoTo FileErrorHandler
    
            '再描画。
            If RedrawWindow(Me.hWnd, 0, 0, RDW_UPDATENOW) = 0 Then Call Err.Raise(ERR_FATAL, , GetLastErrorMessage())
    
            frmJobSelect.Caption = "現場を削除"
            frmJobSelect.Description = "削除する現場を選択してください｡"
            frmJobSelect.TextOK = "削除"
            frmJobSelect.MsgOK = "選択された現場を削除します。"
            frmJobSelect.MsgUnselected = "削除する現場をチェックしてください。"
            Call frmJobSelect.Show(1)
            If frmJobSelect.Result <> vbOK Then Exit Function
    
            '再描画。
            If RedrawWindow(Me.hWnd, 0, 0, RDW_UPDATENOW) = 0 Then Call Err.Raise(ERR_FATAL, , GetLastErrorMessage())
    
            '閉じてから削除する。
            Call CloseProject
    
            Dim sFolderNames() As String
            sFolderNames = frmJobSelect.FolderNames
            Dim clsProjectFileManager As New ProjectFileManager
    
            Dim i As Long
            For i = 0 To UBound(sFolderNames)
                Call clsProjectFileManager.DeleteProjectFolder(sFolderNames(i))
            Next
    
            RemoveJob = True
    
            Exit Function
    
        FileErrorHandler:
            If Err.Number <> ERR_FILE Then Call Err.Raise(Err.Number, Err.Source, Err.Description, Err.HelpFile, Err.HelpContext)
            Call MsgBox(Err.Description, vbCritical)
    
        End Function
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        private bool RemoveJob()
        {
            bool RemoveJob = false;

            //坂井様へOn Error GoTo FileErrorHandler


            //坂井様へ'再描画。
            //坂井様へIf RedrawWindow(Me.hWnd, 0, 0, RDW_UPDATENOW) = 0 Then Call Err.Raise(ERR_FATAL, , GetLastErrorMessage())


            frmJobSelect2 frmJobSelect = new frmJobSelect2();
            frmJobSelect.Text = "現場を削除";
            frmJobSelect.Description("削除する現場を選択してください｡");
            frmJobSelect.TextOK("削除");
            frmJobSelect.MsgOK = "選択された現場を削除します。";
            frmJobSelect.MsgUnselected = "削除する現場をチェックしてください。";

            frmJobSelect.ShowDialog();

            if (frmJobSelect.Result != DEFINE.vbOK) { return false; }

            //再描画
            if (frmMain2.RedrawWindow(this.Handle, IntPtr.Zero, IntPtr.Zero, (int)DEFINE.RDW_UPDATENOW) == false)
            {
                return false;
            }


            //'閉じてから削除する。
            CloseProject();


            // sFolderNames() As String
            List<string> sFolderNames = new List<string>();

            sFolderNames.Add(frmJobSelect.FolderNames());

            ProjectFileManager clsProjectFileManager = new ProjectFileManager();


            for (int i = 0; i < sFolderNames.Count; i++)
            {
                clsProjectFileManager.DeleteProjectFolder(sFolderNames[i]);
            }


            RemoveJob = true;
            return RemoveJob;
        }

        //<<<<<<<<<-----------24/01/24 K.setoguchi@NV
        //***************************************************************************




        //24/01/28 K.setoguchi@NV---------->>>>>>>>>>
        //***************************************************************************


        //==========================================================================================
        /*[VB]
            'DATファイルをインポートする。
            Private Sub mnuFileImportDAT_Click()

                On Error GoTo ErrorHandler
    
                'インポート。
                Call FileImportClick(IMPORT_TYPE_DAT)
    
                Exit Sub
    
            ErrorHandler:
                Call mdlMain.ErrorExit
    
            End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /// <summary>
        /// 【インポート】NetSurvデータファイル：   'DATファイルをインポートする。
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuFileImportDAT_Click(object sender, EventArgs e)
        {
            try
            {
                //'インポート。
                FileImportClick(IMPORT_TYPE.IMPORT_TYPE_DAT);

            }
            catch (Exception ex)
            {
                //Call mdlMain.ErrorExit

            }

        }



        //==========================================================================================
        /*[VB]
        'RINEXファイルをインポートする。
        Private Sub mnuFileImportRINEX_Click()

            On Error GoTo ErrorHandler
    
            'インポート。
            Call FileImportClick(IMPORT_TYPE_RINEX)
    
            Exit Sub
    
        ErrorHandler:
            Call mdlMain.ErrorExit
    
        End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /// <summary>
        ///  【インポート】RINEXファイル：RINEXファイルをインポートする。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuFileImportRINEX_Click(object sender, EventArgs e)
        {
            try
            {
                //'インポート。
                FileImportClick(IMPORT_TYPE.IMPORT_TYPE_RINEX);

            }
            catch (Exception ex)
            {
                //Call mdlMain.ErrorExit

            }

        }
        //<<<<<<<<<-----------24/01/28 K.setoguchi@NV
        //***************************************************************************



        //==========================================================================================
        /*[VB]
            '受信機からDATファイルをインポートする。 2007/4/10 NGS Yamada
            Private Sub mnuFileImportDirect_Click()

                On Error GoTo ErrorHandler
    
                'インポート。
                Call FileImportClick(IMPORT_TYPE_DIRECT)
    
                Exit Sub
    
            ErrorHandler:
                Call mdlMain.ErrorExit

            End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /// <summary>
        ////  【インポート】受信機からインポート：受信機からDATファイルをインポートする。 2007/4/10 NGS Yamada
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuFileImportDirect_Click(object sender, EventArgs e)
        {
            try
            {
                //'インポート。
                FileImportClick(IMPORT_TYPE.IMPORT_TYPE_DIRECT);

            }
            catch (Exception ex)
            {
                //Call mdlMain.ErrorExit

            }
        }



        //24/01/28 K.setoguchi@NV---------->>>>>>>>>>
        //***************************************************************************
        //==========================================================================================
        /*[VB]
        'インポートコマンド。
        Private Sub FileImportClick(ByVal nImportType As IMPORT_TYPE)

            'インポート。
            If Not Import(nImportType) Then Exit Sub
    
            '砂時計。
            Dim clsWaitCursor As New WaitCursor
            Set clsWaitCursor.Object = Me
    
            'リストの作成。
            Call objListPane.RemakeList(True)
    
            'プロットの再描画。
            Call objPlotPane.UpdateLogicalDrawArea(True)
            Call objPlotPane.Redraw
            Call objPlotPane.Refresh
    
            Call clsWaitCursor.Back
    
        End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /// <summary>
        /// インポートコマンド。
        /// 
        /// </summary>
        /// <param name="nImportType"></param>
        private void FileImportClick(IMPORT_TYPE nImportType)
        {
            if (!Import(nImportType))
            {
                return;
            }

            //'砂時計。
            Cursor = Cursors.WaitCursor;

            //坂井様へ'リストの作成。
            //坂井様へCall objListPane.RemakeList(True)


            //坂井様へ'プロットの再描画。
            //坂井様へCall objPlotPane.UpdateLogicalDrawArea(True)
            //坂井様へCall objPlotPane.Redraw
            //坂井様へCall objPlotPane.Refresh



            Cursor = Cursors.Default;

        }
        //<<<<<<<<<-----------24/01/28 K.setoguchi@NV
        //***************************************************************************

        //24/01/28 K.setoguchi@NV---------->>>>>>>>>>
        //***************************************************************************

        //==========================================================================================
        /*[VB]
        '外部ファイルをインポートする。
        '
        '引き数：
        'nImportType インポートファイル種別。
        '
        '戻り値：
        '正常終了の場合 True を返す。
        'キャンセルの場合 False を返す。
        Private Function Import(ByVal nImportType As IMPORT_TYPE) As Boolean

            Import = False
    
            Dim sPath() As String
    
            If nImportType <> IMPORT_TYPE_DIRECT Then
                dlgCommonDialog.DialogTitle = "インポート"
                dlgCommonDialog.CancelError = True
                dlgCommonDialog.flags = cdlOFNFileMustExist Or cdlOFNHideReadOnly Or cdlOFNLongNames Or cdlOFNNoChangeDir Or cdlOFNPathMustExist Or cdlOFNAllowMultiselect Or cdlOFNExplorer
                dlgCommonDialog.MaxFileSize = 1024
                dlgCommonDialog.FileName = ""
                Select Case nImportType
                Case IMPORT_TYPE_UNKNOWN
                    dlgCommonDialog.DefaultExt = IMPORT_FILE_EXT
                    dlgCommonDialog.Filter = IMPORT_FILE_FILTER
                Case IMPORT_TYPE_JOB
                    dlgCommonDialog.DefaultExt = JOB_FILE_EXT
                    dlgCommonDialog.Filter = JOB_FILE_FILTER
                Case IMPORT_TYPE_NVF
                    dlgCommonDialog.DefaultExt = NVF_FILE_EXT
                    dlgCommonDialog.Filter = NVF_FILE_FILTER
                Case IMPORT_TYPE_DAT
                    dlgCommonDialog.DefaultExt = DAT_FILE_EXT
                    dlgCommonDialog.Filter = DAT_FILE_FILTER
                Case IMPORT_TYPE_RINEX
                    dlgCommonDialog.DefaultExt = RINEX_FILE_EXT
                    dlgCommonDialog.Filter = RINEX_FILE_FILTER
                End Select
                dlgCommonDialog.FilterIndex = 1
        
                If Not GetFileDialog().ShowOpen(dlgCommonDialog) Then Exit Function
        
                '再描画。
                If RedrawWindow(Me.hWnd, 0, 0, RDW_UPDATENOW) = 0 Then Call Err.Raise(ERR_FATAL, , GetLastErrorMessage())
        
                Call ConvertOpenDialogMultiList(dlgCommonDialog.FileName, sPath)
            Else
                With frmImportDirect
        '            .ComPortType = m_clsDocument.ImportComPortType
                    .ComPortCount = 0
            
                    If m_clsDocument.ImportComPortType = True Then  '2007/7/2 NGS Yamada
        '                Dim autoComPort As String
        '                If SearchComPort(autoComPort) = True Then
        '                    .ComPort = autoComPort
                        If SearchComPort(frmImportDirect) = True Then
                        Else
                            Call MsgBox("NetSurvとの接続に失敗しました。", vbCritical)
                            Exit Function
                        End If
                    Else
                        '.ComPort = "COM" & m_clsDocument.ImportComPort + 1
                        Call .AddComPort(m_clsDocument.ImportComPort + 1)
                    End If
                    .DataSave = m_clsDocument.ImportDataSave
        '            .ComPort = "COM" & m_clsDocument.ImportComPort + 1
                    .Path = App.Path & TEMPORARY_PATH & IMPORT_PATH
                    .Show vbModal
            
                    If .Result <> vbOK Then Exit Function
            
                    sPath = .SaveFilePaths
                End With
            End If
            'インポート。
            Dim clsProcessImport As New ProcessImport
            Call clsProcessImport.Import(nImportType, sPath, False)
    
            '再描画。
            If RedrawWindow(Me.hWnd, 0, 0, RDW_UPDATENOW) = 0 Then Call Err.Raise(ERR_FATAL, , GetLastErrorMessage())
    
            Import = True
    
        End Function
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]

        private bool Import(IMPORT_TYPE nImportType)
        {
            bool Import = false;

            List<string> sPath = new List<string>();


            OpenFileDialog dlgCommonDialog = new OpenFileDialog();


            if (nImportType != IMPORT_TYPE.IMPORT_TYPE_DIRECT)
            {
                dlgCommonDialog.Title = "インポート";
                //dlgCommonDialog.CancelError = True
                //dlgCommonDialog.flags = cdlOFNFileMustExist Or cdlOFNHideReadOnly Or cdlOFNLongNames Or cdlOFNNoChangeDir Or cdlOFNPathMustExist Or cdlOFNAllowMultiselect Or cdlOFNExplorer
                //dlgCommonDialog.MaxFileSize = 1024;

                dlgCommonDialog.Filter = "テキストファイル (*.txt)|*.txt|"
                                 + "すべてのファイル (*.*)|*.*";
                dlgCommonDialog.FilterIndex = 2;
                dlgCommonDialog.Multiselect = true;

                dlgCommonDialog.FileName = "";
                switch (nImportType)
                {
                    case IMPORT_TYPE.IMPORT_TYPE_UNKNOWN:
                        dlgCommonDialog.DefaultExt = MdlNSSDefine.IMPORT_FILE_EXT;
                        dlgCommonDialog.Filter = MdlNSSDefine.IMPORT_FILE_FILTER;
                        break;
                    case IMPORT_TYPE.IMPORT_TYPE_JOB:
                        dlgCommonDialog.DefaultExt = JOB_FILE_EXT;
                        dlgCommonDialog.Filter = JOB_FILE_FILTER;
                        break;
                    case IMPORT_TYPE.IMPORT_TYPE_NVF:
                        dlgCommonDialog.DefaultExt = NVF_FILE_EXT;
                        dlgCommonDialog.Filter = NVF_FILE_FILTER;
                        break;
                    case IMPORT_TYPE.IMPORT_TYPE_DAT:
                        dlgCommonDialog.DefaultExt = DAT_FILE_EXT;
                        dlgCommonDialog.Filter = DAT_FILE_FILTER;
                        break;
                    case IMPORT_TYPE.IMPORT_TYPE_RINEX:
                        dlgCommonDialog.DefaultExt = RINEX_FILE_EXT;
                        dlgCommonDialog.Filter = RINEX_FILE_FILTER;
                        break;
                    default:
                        return Import;
                }
                dlgCommonDialog.FilterIndex = 1;

                FileDialog fd = (FileDialog)mdlMain.GetFileDialog();

                if (!fd.ShowOpen(dlgCommonDialog))
                {
                    return Import;
                }

                //'再描画。
                //If RedrawWindow(Me.hWnd, 0, 0, RDW_UPDATENOW) = 0 Then Call Err.Raise(ERR_FATAL, , GetLastErrorMessage())


                mdlGUI.ConvertOpenDialogMultiList(dlgCommonDialog, ref sPath);


            }
            else
            {
                //        With frmImportDirect
                //'            .ComPortType = m_clsDocument.ImportComPortType
                //            .ComPortCount = 0;

            }

            //'インポート。
            ProcessImport clsProcessImport = new ProcessImport();
            clsProcessImport.Import(nImportType, ref sPath, false);


            //            '再描画。
            //            If RedrawWindow(Me.hWnd, 0, 0, RDW_UPDATENOW) = 0 Then Call Err.Raise(ERR_FATAL, , GetLastErrorMessage())


            Import = true;
            return Import;

        }



        //==========================================================================================
        /*[VB]
            'プロジェクトをインポートする。
            Private Sub mnuFileImportProject_Click()

                On Error GoTo ErrorHandler
    
                '既存のプロジェクトを閉じる。
                If Not ConfirmCloseProject() Then Exit Sub
    
                '現場のインポート。
                If Not ImportProject Then Exit Sub
    
                'タイトル。
                Call UpdateTitle
    
                '砂時計。
                Dim clsWaitCursor As New WaitCursor
                Set clsWaitCursor.Object = Me
    
                'リストの作成。
                Call objListPane.RemakeList(False)
    
                'プロットの再描画。
                Call objPlotPane.UpdateLogicalDrawArea(True)
                Call objPlotPane.Redraw
                Call objPlotPane.Refresh
    
                'ステータスバーの更新。
                Call UpdateStatusBarAll
    
                'ドキュメントのOpen/Closeによるメニューの更新。
                Call UpdateDocumentMenu
    
                Call clsWaitCursor.Back
    
                Exit Sub
    
            ErrorHandler:
                Call mdlMain.ErrorExit

            End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /// <summary>
        /// プロジェクトをインポートする。
        ///     メニュー：現場のインポート。
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuFileImportProject_Click(object sender, EventArgs e)
        {
            try
            {
                //  On Error GoTo ErrorHandler

                //坂井様へ  '既存のプロジェクトを閉じる。
                //坂井様へ  If Not ConfirmCloseProject() Then Exit Sub


                //'現場のインポート。
                if (!ImportProject())
                {
                    return;
                }

                //'タイトル。
                UpdateTitle();


                //'砂時計。
                Cursor = Cursors.WaitCursor;


                //坂井様へ    'リストの作成。
                //坂井様へCall objListPane.RemakeList(False)


                //坂井様へ'プロットの再描画。
                //坂井様へCall objPlotPane.UpdateLogicalDrawArea(True)
                //坂井様へCall objPlotPane.Redraw
                //坂井様へCall objPlotPane.Refresh


                //坂井様へ'ステータスバーの更新。
                //坂井様へCall UpdateStatusBarAll


                //坂井様へ  'ドキュメントのOpen/Closeによるメニューの更新。
                //坂井様へ  Call UpdateDocumentMenu


                Cursor = Cursors.Default;

                return;
            }
            catch (Exception ex)
            {
                //ErrorHandler:
                //  Call mdlMain.ErrorExit

            }

        }
        //==========================================================================================
        /*[VB]
            '現場のインポート。
            '
            '戻り値：
            '正常終了の場合 True を返す。
            'キャンセルの場合 False を返す。
            Private Function ImportProject() As Boolean

                ImportProject = False
    
                On Error GoTo CancelHandler
    
                dlgCommonDialog.DialogTitle = "開く"
                dlgCommonDialog.CancelError = True
                dlgCommonDialog.DefaultExt = NSSEXP_FILE_EXT
                dlgCommonDialog.Filter = NSSEXP_FILE_FILTER
                dlgCommonDialog.FilterIndex = 1
                dlgCommonDialog.flags = cdlOFNFileMustExist Or cdlOFNHideReadOnly Or cdlOFNLongNames Or cdlOFNNoChangeDir Or cdlOFNPathMustExist Or cdlOFNAllowMultiselect Or cdlOFNExplorer
                dlgCommonDialog.MaxFileSize = 1024
                dlgCommonDialog.FileName = ""
    
                If Not GetFileDialog().ShowOpen(dlgCommonDialog) Then Exit Function
    
                '再描画。
                If RedrawWindow(Me.hWnd, 0, 0, RDW_UPDATENOW) = 0 Then Call Err.Raise(ERR_FATAL, , GetLastErrorMessage())
    
                '閉じてからインポートする。
                Call CloseProject
    
                Dim sPath() As String
                Call ConvertOpenDialogMultiList(dlgCommonDialog.FileName, sPath)
    
                'インポート。
                Dim clsProcessProject As New ProcessProject
    
                If Not clsProcessProject.ImportProjectFolders(sPath) Then Exit Function
    
                ImportProject = True
    
                Exit Function
    
            CancelHandler:
                If Err.Number <> cdlCancel Then Call MsgBox(Err.Description, vbCritical)
    
            End Function
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        private bool ImportProject()
        {
            bool ImportProject = false;
#if false
            dlgCommonDialog.Title = "インポート";
            //dlgCommonDialog.CancelError = True
            //dlgCommonDialog.flags = cdlOFNFileMustExist Or cdlOFNHideReadOnly Or cdlOFNLongNames Or cdlOFNNoChangeDir Or cdlOFNPathMustExist Or cdlOFNAllowMultiselect Or cdlOFNExplorer
            //dlgCommonDialog.MaxFileSize = 1024;

            dlgCommonDialog.Filter = "テキストファイル (*.txt)|*.txt|"
                             + "すべてのファイル (*.*)|*.*";
            dlgCommonDialog.FilterIndex = 2;
            dlgCommonDialog.Multiselect = true;

            dlgCommonDialog.FileName = "";
#else   //-------------------------------------------------------------------
            OpenFileDialog dlgCommonDialog = new OpenFileDialog();

            dlgCommonDialog.Title = "開く";
            //    dlgCommonDialog.CancelError = True
            //     dlgCommonDialog.DefaultExt = NSSEXP_FILE_EXT
            dlgCommonDialog.Filter = MdlNSDefine.NSSEXP_FILE_FILTER;
            dlgCommonDialog.FilterIndex = 1;
            //     dlgCommonDialog.flags = cdlOFNFileMustExist Or cdlOFNHideReadOnly Or cdlOFNLongNames Or cdlOFNNoChangeDir Or cdlOFNPathMustExist Or cdlOFNAllowMultiselect Or cdlOFNExplorer
            //     dlgCommonDialog.MaxFileSize = 1024
            dlgCommonDialog.FileName = "";
#endif

            FileDialog fd = (FileDialog)mdlMain.GetFileDialog();

            if (!fd.ShowOpen(dlgCommonDialog))
            {
                return ImportProject;
            }

            //'再描画。
            //If RedrawWindow(Me.hWnd, 0, 0, RDW_UPDATENOW) = 0 Then Call Err.Raise(ERR_FATAL, , GetLastErrorMessage())


            //'閉じてからインポートする。
            CloseProject();

            List<string> sPath = new List<string>();
            mdlGUI.ConvertOpenDialogMultiList(dlgCommonDialog, ref sPath);



            //'インポート。
            ProcessProject clsProcessProject = new ProcessProject();
            if (!clsProcessProject.ImportProjectFolders(ref sPath))
            {
                return ImportProject;
            }


            ImportProject = true;
            return ImportProject;

        }




        //==========================================================================================
        /*[VB]
            'プロジェクトをエクスポートする。
            Private Sub mnuFileExportProject_Click()

                On Error GoTo ErrorHandler
    
                '既存のプロジェクトを閉じる。
                If Not ConfirmCloseProject() Then Exit Sub
    
                '現場のエクスポート。
                If Not ExportProject Then Exit Sub
    
                'タイトル。
                Call UpdateTitle
    
                '砂時計。
                Dim clsWaitCursor As New WaitCursor
                Set clsWaitCursor.Object = Me
    
                'リストの作成。
                Call objListPane.RemakeList(False)
    
                'プロットの再描画。
                Call objPlotPane.UpdateLogicalDrawArea(True)
                Call objPlotPane.Redraw
                Call objPlotPane.Refresh
    
                'ステータスバーの更新。
                Call UpdateStatusBarAll
    
                'ドキュメントのOpen/Closeによるメニューの更新。
                Call UpdateDocumentMenu


                Call clsWaitCursor.Back

                Exit Sub

            ErrorHandler:
                Call mdlMain.ErrorExit

            End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /// <summary>
        ///  プロジェクトをエクスポートする。                
        ///     メニュー：現場のエクスポート。
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuFileExportProject_Click(object sender, EventArgs e)
        {

            try
            {
                //On Error GoTo ErrorHandler


                //坂井様へ  '既存のプロジェクトを閉じる。
                //坂井様へ  If Not ConfirmCloseProject() Then Exit Sub


                //'現場のエクスポート。
                if (!ExportProject()) { return; }


                //タイトル。
                UpdateTitle();


                //'砂時計。
                Cursor = Cursors.WaitCursor;


                //坂井様へ  'リストの作成。
                //坂井様へ  Call objListPane.RemakeList(False)


                //坂井様へ  'プロットの再描画。
                //坂井様へ  Call objPlotPane.UpdateLogicalDrawArea(True)
                //坂井様へ  Call objPlotPane.Redraw
                //坂井様へ  Call objPlotPane.Refresh


                //坂井様へ  'ステータスバーの更新。
                //坂井様へ  Call UpdateStatusBarAll


                //坂井様へ  'ドキュメントのOpen/Closeによるメニューの更新。
                //坂井様へ  Call UpdateDocumentMenu

                Cursor = Cursors.Default;

                return;
            }
            catch (Exception ex)
            {
                //ErrorHandler:
                //  Call mdlMain.ErrorExit
            }

        }


        //==========================================================================================
        /*[VB]
            '現場のエクスポート。
            '
            '戻り値：
            '正常終了の場合 True を返す。
            'キャンセルの場合 False を返す。
            Private Function ExportProject() As Boolean

                ExportProject = False
    
                On Error GoTo CancelHandler
    
                '再描画。
                If RedrawWindow(Me.hWnd, 0, 0, RDW_UPDATENOW) = 0 Then Call Err.Raise(ERR_FATAL, , GetLastErrorMessage())
    
                frmJobSelect.Caption = "現場をエクスポート"
                frmJobSelect.Description = "エクスポートする現場を選択してください｡"
                frmJobSelect.TextOK = "OK"
                frmJobSelect.MsgOK = ""
                frmJobSelect.MsgUnselected = "エクスポートする現場をチェックしてください。"
                Call frmJobSelect.Show(1)
                If frmJobSelect.Result <> vbOK Then Exit Function
    
                '再描画。
                If RedrawWindow(Me.hWnd, 0, 0, RDW_UPDATENOW) = 0 Then Call Err.Raise(ERR_FATAL, , GetLastErrorMessage())
    
                If 0 < UBound(frmJobSelect.FolderNames) Then
                    '出力先フォルダを選択。
                    Dim clsFolderDialog As New FolderDialog
                    clsFolderDialog.Caption = "現場をエクスポート"
                    clsFolderDialog.Title = "出力先のフォルダを選択してください。"
                    clsFolderDialog.Path = ""
                    clsFolderDialog.CancelError = True
                    Call clsFolderDialog.ShowOpen(Me.hWnd)
        
                    Dim sPath As String
                    Dim bSingle As Boolean
                    sPath = clsFolderDialog.Path
                    bSingle = False
                Else
                    Dim sFolderNames() As String
                    sFolderNames = frmJobSelect.FolderNames
                    Dim clsProjectFileManager As New ProjectFileManager
        
                    Dim sJobName As String
                    Dim sDistrictName As String
                    If Not clsProjectFileManager.GetJobInfo(sFolderNames(0), sJobName, sDistrictName) Then Exit Function
        
                    '出力先ファイルを選択。
                    dlgCommonDialog.DialogTitle = "名前を付けて保存"
                    dlgCommonDialog.CancelError = True
                    dlgCommonDialog.DefaultExt = NSSEXP_FILE_EXT
                    dlgCommonDialog.Filter = NSSEXP_FILE_FILTER
                    dlgCommonDialog.FilterIndex = 1
                    dlgCommonDialog.flags = cdlOFNHideReadOnly Or cdlOFNLongNames Or cdlOFNNoChangeDir Or cdlOFNNoReadOnlyReturn Or cdlOFNOverwritePrompt Or cdlOFNPathMustExist
                    dlgCommonDialog.FileName = sJobName & "." & NSSEXP_FILE_EXT
                    If Not GetFileDialog().ShowSave(dlgCommonDialog) Then Exit Function
        
                    sPath = dlgCommonDialog.FileName
                    bSingle = True
                End If
    
                '再描画。
                If RedrawWindow(Me.hWnd, 0, 0, RDW_UPDATENOW) = 0 Then Call Err.Raise(ERR_FATAL, , GetLastErrorMessage())
    
                '閉じてからエクスポートする。
                Call CloseProject
    
                'エクスポート。
                Dim clsProcessProject As New ProcessProject
                If Not clsProcessProject.ExportProjectFolders(frmJobSelect.FolderNames, sPath, bSingle) Then Exit Function
    
                ExportProject = True
    
                Exit Function
    
            CancelHandler:
                If Err.Number <> cdlCancel Then Call MsgBox(Err.Description, vbCritical)
    
            End Function
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /// <summary>
        /// 現場のエクスポート。
        /// 
        /// </summary>
        /// <returns>
        /// 戻り値：
        ///     正常終了の場合 True を返す。
        ///     キャンセルの場合 False を返す。
        /// </returns>
        private bool ExportProject()
        {
            bool ExportProject = false;

            try
            {
                //On Error GoTo CancelHandler


                //坂井様へ  '再描画。
                //坂井様へ  If RedrawWindow(Me.hWnd, 0, 0, RDW_UPDATENOW) = 0 Then Call Err.Raise(ERR_FATAL, , GetLastErrorMessage())


                frmJobSelect2 frmJobSelect = new frmJobSelect2();

                frmJobSelect.Text = "現場をエクスポート";
                frmJobSelect.lblDescription.Text = "エクスポートする現場を選択してください｡";
                frmJobSelect.TextOK("OK");
                frmJobSelect.MsgOK = "";
                frmJobSelect.MsgUnselected = "エクスポートする現場をチェックしてください。";

                frmJobSelect.ShowDialog();

                if (frmJobSelect.Result != DEFINE.vbOK) { return false; }

                ExportProject = true;

                //坂井様へ  '再描画。
                //坂井様へ  If RedrawWindow(Me.hWnd, 0, 0, RDW_UPDATENOW) = 0 Then Call Err.Raise(ERR_FATAL, , GetLastErrorMessage())

                string sPath;
                bool bSingle;
                List<string> sFolderNames = new List<string>();

                if (frmJobSelect.FolderNames() != null)
                {
                    //'出力先フォルダを選択。

                    FolderDialog clsFolderDialog = new FolderDialog();

                    clsFolderDialog.Caption = "現場をエクスポート";
                    clsFolderDialog.Title = "出力先のフォルダを選択してください。";
                    clsFolderDialog.Path = "";
                    clsFolderDialog.CancelError = true;

                    clsFolderDialog.ShowOpen(frmJobSelect.Handle);

                    sPath = clsFolderDialog.Path;
                    bSingle = false;

                }
                else
                {

                    sFolderNames.Add(frmJobSelect.FolderNames());

                    ProjectFileManager clsProjectFileManager = new ProjectFileManager();

                    string sJobName = "";
                    string sDistrictName = "";

                    if (!clsProjectFileManager.GetJobInfo(sFolderNames[0], ref sJobName, ref sDistrictName))
                    {
                        return ExportProject;
                    }


                    OpenFileDialog dlgCommonDialog = new OpenFileDialog();

                    //'出力先ファイルを選択。
                    dlgCommonDialog.Title = "名前を付けて保存";
                    //dlgCommonDialog.CancelError = True
                    //dlgCommonDialog.DefaultExt = NSSEXP_FILE_EXT
                    dlgCommonDialog.Filter = MdlNSDefine.NSSEXP_FILE_FILTER;
                    dlgCommonDialog.FilterIndex = 1;
                    //dlgCommonDialog.flags = cdlOFNHideReadOnly Or cdlOFNLongNames Or cdlOFNNoChangeDir Or cdlOFNNoReadOnlyReturn Or cdlOFNOverwritePrompt Or cdlOFNPathMustExist
                    dlgCommonDialog.FileName = $"{sJobName}.{MdlNSDefine.NSSEXP_FILE_EXT}";

                    FileDialog fd = (FileDialog)mdlMain.GetFileDialog();

                    if (!fd.ShowSave(dlgCommonDialog))
                    {
                        return ExportProject;
                    }

                    sPath = dlgCommonDialog.FileName;
                    bSingle = true;

                }

                //坂井様へ  '再描画。
                //坂井様へ  If RedrawWindow(Me.hWnd, 0, 0, RDW_UPDATENOW) = 0 Then Call Err.Raise(ERR_FATAL, , GetLastErrorMessage())


                //'閉じてからエクスポートする。
                CloseProject();


                //'エクスポート。
                //Dim clsProcessProject As New ProcessProject
                // If Not clsProcessProject.ExportProjectFolders(frmJobSelect.FolderNames, sPath, bSingle) Then Exit Function

                ProcessProject clsProcessProject = new ProcessProject();

                if (!clsProcessProject.ExportProjectFolders(ref sFolderNames, sPath, bSingle))
                {
                    return ExportProject;
                }

                ExportProject = true;


            }
            catch (Exception ex)
            {
                //CancelHandler:
                //  If Err.Number<> cdlCancel Then Call MsgBox(Err.Description, vbCritical)

            }


            return ExportProject;

        }

        //==========================================================================================
        /*[VB]
             'RINEXファイルをエクスポートする。
            Private Sub mnuFileExportRinex_Click()

                On Error GoTo ErrorHandler


                Dim clsOutputParam As New OutputParam
                Let clsOutputParam = m_clsDocument.OutputParam(OUTPUT_TYPE_RINEX)
    
                '帳票パラメータを入力する。
                frmAccountInfo.AccountType = ACCOUNT_TYPE_RINEX
                Let frmAccountInfo.AccountParam = clsOutputParam.AccountParam
                Call frmAccountInfo.Show(1)
                If frmAccountInfo.Result<> vbOK Then Exit Sub


                Let clsOutputParam.AccountParam = frmAccountInfo.AccountParam


                Call ExportRinex(clsOutputParam, True)

                Exit Sub

            ErrorHandler:
                Call mdlMain.ErrorExit

            End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /// <summary>
        /// 【エクスポート】RINEXファイル：RINEXファイルをエクスポートする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuFileExportRinex_Click(object sender, EventArgs e)
        {

            try
            {

                //On Error GoTo ErrorHandler

                OutputParam clsOutputParam = new OutputParam();

                clsOutputParam = (OutputParam)m_clsDocument.OutputParam((long)MdlNSSDefine.OUTPUT_TYPE.OUTPUT_TYPE_RINEX);



                frmAccountInfo2 frmAccountInfo = new frmAccountInfo2();

                //'帳票パラメータを入力する。
                frmAccountInfo.AccountType = (long)ACCOUNT_TYPE.ACCOUNT_TYPE_RINEX;



                frmAccountInfo.AccountParam(clsOutputParam.AccountParam());
                frmAccountInfo.ShowDialog();
                if (frmAccountInfo.Result != DEFINE.vbOK)
                {
                    return;
                }


                clsOutputParam.AccountParam((AccountParam)frmAccountInfo.AccountParam());


                ExportRinex(clsOutputParam, true);

            }
            catch (Exception ex)
            {
                //ErrorHandler:
                //  Call mdlMain.ErrorExit

            }

        }

        //==========================================================================================
        /*[VB]
            'RINEXファイルのエクスポート。
            '
            'bAutoFileName に True が指定された場合、出力先のフォルダだけを指定して出力ファイル名は自動で決定する。
            'bAutoFileName に False が指定された場合、出力ファイルのパスは一つ一つ指定する。
            '
            '引き数：
            'clsOutputParam 外部出力ファイル出力パラメータ。
            'bAutoFileName 自動ファイル名フラグ。
            Private Sub ExportRinex(ByVal clsOutputParam As OutputParam, ByVal bAutoFileName As Boolean)

                On Error GoTo CancelHandler
    
                '観測点リスト。
                Dim clsObservationPoints() As ObservationPoint
                clsObservationPoints = GetAccountObservationPoints(m_clsDocument.NetworkModel.RepresentPointHead, clsOutputParam.AccountParam)
                '出力するものがなければ戻る。
                If UBound(clsObservationPoints) < 0 Then
                    Call MsgBox(MESSAGE_CONTENT_EMPTY, vbExclamation)
                    Exit Sub
                End If


                If bAutoFileName Then
                    Dim clsFolderDialog As New FolderDialog
                    clsFolderDialog.Caption = "エクスポート"
                    clsFolderDialog.Title = "出力先のフォルダを選択してください。"
                    clsFolderDialog.Path = clsOutputParam.Path
                    clsFolderDialog.CancelError = True
                    Call clsFolderDialog.ShowOpen(Me.hWnd)

                    clsOutputParam.Path = clsFolderDialog.Path
        
                    '出力。
                    Dim clsProcessExport As New ProcessExport
                    Call clsProcessExport.ExportRinexMulti(clsOutputParam)
                Else
                    dlgCommonDialog.CancelError = True
                    dlgCommonDialog.DefaultExt = ""
                    dlgCommonDialog.Filter = RINEX_FILE_FILTER
                    dlgCommonDialog.FilterIndex = 1
                    dlgCommonDialog.flags = cdlOFNHideReadOnly Or cdlOFNLongNames Or cdlOFNNoChangeDir Or cdlOFNNoReadOnlyReturn Or cdlOFNPathMustExist 'ここでは上書き確認はしない。


                    Dim i As Long
                    For i = 0 To UBound(clsObservationPoints)
                        Dim sSrcRinexExt As String
                        sSrcRinexExt = StrConv("." & clsObservationPoints(i).RinexExt, vbUpperCase)


                        dlgCommonDialog.DialogTitle = clsObservationPoints(i).Number & "-" & clsObservationPoints(i).Session & " のエクスポート"
                        'RINEXファイルのファイル名称を「観測点No＋セッション名」に変更 2006/12/22 NGS Yamada
            '            dlgCommonDialog.FileName = IIf(clsOutputParam.Path <> "", clsOutputParam.Path & "\", "") & clsObservationPoints(i).SrcTitle & sSrcRinexExt & RNX_OBS_EXTENSION
                        dlgCommonDialog.FileName = IIf(clsOutputParam.Path<> "", clsOutputParam.Path & "\", "") & clsObservationPoints(i).RinexTitle & sSrcRinexExt & RNX_OBS_EXTENSION


                        If Not GetFileDialog().ShowSave(dlgCommonDialog) Then Exit Sub


                        Dim sDrive As String
                        Dim sDir As String
                        Dim sTitle As String
                        Dim sExt As String
                        Call SplitPath(dlgCommonDialog.FileName, sDrive, sDir, sTitle, sExt)
                        clsOutputParam.Path = RTrimEx(sDrive & sDir, "\")
                        Dim sDstRinexExt As String
                        sDstRinexExt = RTrimEx(StrConv(sExt, vbUpperCase), StrConv(RNX_OBS_EXTENSION, vbUpperCase))


                        If sSrcRinexExt<> sDstRinexExt Then
                            sTitle = sTitle & sExt
                            sDstRinexExt = sSrcRinexExt
                        End If


                        '出力。
                        Set clsProcessExport = New ProcessExport
                        If Not clsProcessExport.ExportRinexSingle(clsOutputParam, sTitle & sDstRinexExt, clsObservationPoints(i)) Then Exit Sub
                    Next
                End If


                Exit Sub


            CancelHandler:
                If Err.Number<> cdlCancel Then Call Err.Raise(Err.Number, Err.Source, Err.Description, Err.HelpFile, Err.HelpContext)


            End Sub

            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        private void ExportRinex(OutputParam clsOutputParam, bool bAutoFileName)
        {

            //On Error GoTo CancelHandler


            //'観測点リスト。
            List<ObservationPoint> clsObservationPoints = new List<ObservationPoint>();

//            clsObservationPoints = GetAccountObservationPoints(m_clsDocument.NetworkModel.RepresentPointHead, clsOutputParam.AccountParam)
            //'出力するものがなければ戻る。
            if(clsObservationPoints.Count < 0)
            {
                //Call MsgBox(MESSAGE_CONTENT_EMPTY, vbExclamation)
            }



        }

        //==========================================================================================
        /*[VB]
         * 新規
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /// <summary>
        /// 【インポート】NetSurvデータファイル：   'nvfファイルをインポートする。
        ///  インポート：
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuFileImportNVF_Click(object sender, EventArgs e)
        {
            try
            {
                //'インポート。
                FileImportClick(IMPORT_TYPE.IMPORT_TYPE_NVF);

            }
            catch (Exception ex)
            {
                //Call mdlMain.ErrorExit

            }

        }

        //==========================================================================================
        /*[VB]
            'NVFファイルを出力する。
            Private Sub mnuGenerateNVF_Click()

                On Error GoTo ErrorHandler
    
                '2023/06/30 Hitz H.Nakamura **************************************************
                'GNSS水準測量対応。
                'NS-Netwok から楕円体高の閉合差を削除。
                '楕円体高の閉合差の終点が重複する場合はNVFファイルの出力を許可しない。
                Dim nAngleDiffHeightCheck As Long
                nAngleDiffHeightCheck = 0
                Dim i As Long
                For i = 0 To m_clsDocument.AngleDiffParamHeight.Count - 1
                    Dim clsAngleDiffResult As AngleDiffResult
                    Set clsAngleDiffResult = m_clsDocument.AngleDiffParamHeight.AngleDiffs(i).AngleDiffResult
                    If clsAngleDiffResult.FromNumber = clsAngleDiffResult.ToNumber Then
                        '始点と終点が一致してしまう。
                        nAngleDiffHeightCheck = -2
                        Exit For
                    End If
                    Dim j As Long
                    For j = i + 1 To m_clsDocument.AngleDiffParamHeight.Count - 1
                        If clsAngleDiffResult.ToNumber = m_clsDocument.AngleDiffParamHeight.AngleDiffs(j).AngleDiffResult.ToNumber Then
                            '終点が重複する。
                            nAngleDiffHeightCheck = -3
                            i = m_clsDocument.AngleDiffParamHeight.Count + 1
                            Exit For
                        End If
                    Next
                Next
                If nAngleDiffHeightCheck < 0 Then
                    If nAngleDiffHeightCheck = -3 Then
                        Call MsgBox("楕円体高の閉合差の至点が重複しています。", vbExclamation)
                    Else
                        Call MsgBox("楕円体高の閉合差が不正です。", vbExclamation)
                    End If
                    Exit Sub
                End If
                '*****************************************************************************
    
                '帳票パラメータを入力する。
                frmOutputInfo.AccountType = ACCOUNT_TYPE_NVF
                Let frmOutputInfo.OutputParam = m_clsDocument.OutputParam(OUTPUT_TYPE_NVF)
                Let frmOutputInfo.OutputParam.Fixed = m_clsDocument.AccountOverlapParam.Fixed '2023/06/26 Hitz H.Nakamura GNSS水準測量対応。前半後半較差の追加。
                Let frmOutputInfo.EnableAutomation = CheckVersionNSN()
                Call frmOutputInfo.Show(1)
                If frmOutputInfo.Result<> vbOK Then Exit Sub
    
                '再描画。
                If RedrawWindow(Me.hWnd, 0, 0, RDW_UPDATENOW) = 0 Then Call Err.Raise(ERR_FATAL, , GetLastErrorMessage())
    
                Call GenerateOutput(OUTPUT_TYPE_NVF, frmOutputInfo.OutputParam, True)


                Exit Sub


            ErrorHandler:
                Call mdlMain.ErrorExit


            End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]  
        /// <summary>
        ///    'NVFファイルを出力する。
        ///     NetSurvベクトルデータファイル(&N)...
        ///     
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuGenerateNVF_Click(object sender, EventArgs e)
        {
            try
            {
                frmOutputInfo frmOutputInfo = new frmOutputInfo();

                frmOutputInfo.ShowDialog();

                if (frmOutputInfo.Result != DEFINE.vbOK)
                {
                    return;
                }



            }
            catch (Exception ex)
            {
                //ErrorHandler:
                //    Call mdlMain.ErrorExit

            }
        }


        //==========================================================================================
        /*[VB]
            'JOBファイルを出力する。
            Private Sub mnuGenerateJOB_Click()

                On Error GoTo ErrorHandler
    
                Dim clsOutputParam As New OutputParam
                Let clsOutputParam = m_clsDocument.OutputParam(OUTPUT_TYPE_JOB)
    
                '帳票パラメータを入力する。
                frmAccountInfo.AccountType = ACCOUNT_TYPE_JOB
                Let frmAccountInfo.AccountParam = clsOutputParam.AccountParam
                Let frmAccountInfo.EnableAutomation = False
                Let frmAccountInfo.Automation = False
                Call frmAccountInfo.Show(1)
                If frmAccountInfo.Result <> vbOK Then Exit Sub
    
                '再描画。
                If RedrawWindow(Me.hWnd, 0, 0, RDW_UPDATENOW) = 0 Then Call Err.Raise(ERR_FATAL, , GetLastErrorMessage())
    
                Let clsOutputParam.AccountParam = frmAccountInfo.AccountParam
                Let clsOutputParam.Fixed = m_clsDocument.AccountOverlapParam.Fixed '2023/06/26 Hitz H.Nakamura GNSS水準測量対応。前半後半較差の追加。
    
                Call GenerateOutput(OUTPUT_TYPE_JOB, clsOutputParam, True)
    
                Exit Sub
    
            ErrorHandler:
                Call mdlMain.ErrorExit
    
            End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        private void mnuGenerateJOB_Click(object sender, EventArgs e)
        {

        }




        //<<<<<<<<<-----------24/01/28 K.setoguchi@NV
        //***************************************************************************

    }
}
