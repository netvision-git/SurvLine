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
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static SurvLine.mdl.MdiDefine;
using static System.Net.Mime.MediaTypeNames;
using static System.Net.WebRequestMethods;

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
            if (!CreateJob()){ return; }
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
                if (!iniFileControl.WritePrivateProfileString(MdlNSDefine.PROFILE_SAVE_SEC_SEMIDYNA, MdlNSDefine.PROFILE_SAVE_KEY_PATH, m_clsDocument.SemiDynaPathDef, $"{App_Path}\\{App_Title}{MdlNSSDefine.PROFILE_SAVE_EXT}")){
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
            if (iniFileControl.WritePrivateProfileString(MdlNSDefine.PROFILE_SAVE_SEC_ACCOUNT, MdlNSDefine.PROFILE_SAVE_KEY_COORDNUM, sValue, $"{App_Path}\\{App_Title}{MdlNSSDefine.PROFILE_SAVE_EXT}") == false){
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

            if (m_clsDocument.Path() == ""){
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
            if (sProjectFolderName == ""){
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
            if(!RemoveJob())
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



    }
}
