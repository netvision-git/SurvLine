﻿using SurvLine.mdl;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static SurvLine.mdl.MdiDefine;

namespace SurvLine
{
    public partial class frmMain2 : Form
    {

        [DllImport("user32.dll")]
        static extern bool RedrawWindow(IntPtr hWnd, IntPtr lprcUpdate, IntPtr hrgnUpdate, int flags);

        public string receiveData = "";
        MdlUtility Utility = new MdlUtility();


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

            //------------------------------------------------------------
            //[VB]      '既存の値。
            //[VB]      frmJobEdit.JobName = ""
            //[VB]      frmJobEdit.DistrictName = ""
            //[VB]      frmJobEdit.Folder = ""
            frmJobEdit2 fJobEdit = new frmJobEdit2();



            fJobEdit.txtJobName.Text = "";
            fJobEdit.txtDistrictName.Text = "";
            fJobEdit.lblFolder.Text = "";


            //[VB]      frmJobEdit.CoordNum = GetPrivateProfileInt(PROFILE_SAVE_SEC_ACCOUNT, PROFILE_SAVE_KEY_COORDNUM, 9, App.Path & "\" & App.Title & PROFILE_SAVE_EXT)
            //
            //          PROFILE_SAVE_SEC_ACCOUNT =  "ACCOUNT"
            //          PROFILE_SAVE_KEY_COORDNUM = "COORDNUM"
            //          App.Path = "C:\Develop\NetSurv\Src\NS-App\NS-Survey"
            //          App.Title =  "NS-Survey"
            //          PROFILE_SAVE_EXT =   ".ini"
            //            fJobEdit.CoordNum = GetPrivateProfileInt(PROFILE_SAVE_SEC_ACCOUNT, PROFILE_SAVE_KEY_COORDNUM, 9, App.Path & "\" & App.Title & PROFILE_SAVE_EXT)
            fJobEdit.cmbZone.SelectedIndex = 7; //GetPrivateProfileInt                        //


            //[VB]      frmJobEdit.GeoidoEnable = Not m_clsDocument.GeoidoPathDef = ""
            //[VB]      frmJobEdit.GeoidoPath = m_clsDocument.GeoidoPathDef
            //fJobEdit.GeoidoEnable = Not m_clsDocument.GeoidoPathDef = "";     //true : "C:\Develop\パラメータファイル\gsigeo2011_ver1.asc"
            fJobEdit.chkGeoidoEnable.Checked = true;                                       //true : "C:\Develop\パラメータファイル\gsigeo2011_ver1.asc"
            //fJobEdit.GeoidoPath = m_clsDocument.GeoidoPathDef;                        //"C:\Develop\パラメータファイル\gsigeo2011_ver1.asc"
            fJobEdit.txtGeoidoPath.Text = @"C:\Develop\パラメータファイル\gsigeo2011_ver1.asc"; //"C:\Develop\パラメータファイル\gsigeo2011_ver1.asc"


            //'セミ・ダイナミック対応。'2009/11 H.Nakamura
            //[VB]      frmJobEdit.SemiDynaEnable = False
            //[VB]      frmJobEdit.SemiDynaPath = m_clsDocument.SemiDynaPathDef
            //[VB]      frmJobEdit.SemiDynaValid = True

            fJobEdit.chkSemiDynaEnable.Checked = false;
            //fJobEdit.SemiDynaPath = m_clsDocument.SemiDynaPathDef;          //"C:\Develop\パラメータファイル\SemiDyna2009.par"
            fJobEdit.txtSemiDynaPath.Text = @"C:\Develop\パラメータファイル\SemiDyna2009.par";
            fJobEdit.SemiDynaValid = true;

            //[VB]      //'Caption を設定すると Form_Load が走るので注意すべし！
            //[VB]      frmJobEdit.Caption = "新規現場の作成"
            fJobEdit.Text = "新規現場の作成";



            //    Call frmJobEdit.Show(1)
            //    If frmJobEdit.Result<> vbOK Then Exit Function
            fJobEdit.ShowDialog();

            if (fJobEdit.Result != DEFINE.vbOK) { return false; }


            //    '再描画。
            //    If RedrawWindow(Me.hWnd, 0, 0, RDW_UPDATENOW) = 0 Then Call Err.Raise(ERR_FATAL, , GetLastErrorMessage())

            // ＜＜＜　坂井様お願い致します。　＞＞＞           


            //--------------------------------------------
            //[VB]    'プロジェクトフォルダの生成。
            //[VB]    On Error GoTo FileErrorHandler
            //[VB]    Dim clsProjectFileManager As New ProjectFileManager
            //[VB]    Dim sProjectFolderName As String
            //[VB]
            //[VB]    sProjectFolderName = clsProjectFileManager.CreateProjectFolder
            //[VB]    If sProjectFolderName = "" Then
            //[VB]        Call MsgBox("これ以上現場を保存することができません。" & vbCrLf & "不要な現場を削除してください。", vbCritical)
            //[VB]        Exit Function
            //[VB]    End If
            ProjectFileManager clsProjectFileManager = new ProjectFileManager();
            string sProjectFolderName = clsProjectFileManager.CreateProjectFolder();        //"0004"
            if (sProjectFolderName == "")
            {
                MessageBox.Show($"これ以上現場を保存することができません。\n 不要な現場を削除してください。", "エラー発生");
                return CreateJob;

            }

            //--------------------------------------------
            //    On Error GoTo 0
            //   
            //    '既存の現場を閉じる。
            //    Call CloseProject
            //
            //



            //    '新規設定。
            //    Call m_clsDocument.SetJob(frmJobEdit.JobName, frmJobEdit.DistrictName, frmJobEdit.CoordNum, frmJobEdit.GeoidoEnable, frmJobEdit.GeoidoPath, frmJobEdit.SemiDynaEnable, frmJobEdit.SemiDynaPath)
            //
            //
            Document document = new Document();
            document.SetJob(fJobEdit.JobName, fJobEdit.DistrictName, fJobEdit.CoordNum, fJobEdit.GeoidoEnable, fJobEdit.GeoidoPath, fJobEdit.SemiDynaEnable, fJobEdit.SemiDynaPath);




            //    '2009/11 H.Nakamura
            //    'セミ・ダイナミックパラメータファイルのパスはデフォルトにも反映させる。
            //    If m_clsDocument.SemiDynaEnable Then
            //        m_clsDocument.SemiDynaPathDef = m_clsDocument.SemiDynaPath
            //        If WritePrivateProfileString(PROFILE_SAVE_SEC_SEMIDYNA, PROFILE_SAVE_KEY_PATH, m_clsDocument.SemiDynaPathDef, App.Path & "\" & App.Title & PROFILE_SAVE_EXT) = 0 Then Call Err.Raise(ERR_FATAL, , GetLastErrorMessage())
            //    End If
            //    
            if (document.m_bSemiDynaEnable)
            {
                document.SemiDynaPathDef = document.m_sSemiDynaPath;

                string App_Path = "";
                string App_Title = "";

                IniFileControl iniFileControl = new IniFileControl();
                if (!iniFileControl.WritePrivateProfileString(MdlNSDefine.PROFILE_SAVE_SEC_SEMIDYNA, MdlNSDefine.PROFILE_SAVE_KEY_PATH, document.SemiDynaPathDef, $"{App_Path}\\{App_Title}{MdlNSSDefine.PROFILE_SAVE_EXT}")){
                    //Call Err.Raise(ERR_FATAL, , GetLastErrorMessage())
                }
            }




            //--------------------------------------------
            //    '2009/11 H.Nakamura
            //    'セミ・ダイナミックの初期化。
            //    Call mdlSemiDyna.Initialize(m_clsDocument.SemiDynaEnable, m_clsDocument.SemiDynaPath)
            //    




            //--------------------------------------------
            //    '保存。
            //    Call Save(clsProjectFileManager.GetSaveFilePath(sProjectFolderName))
            //    


            //--------------------------------------------
            //    'iniファイルに保存。
            //    Dim sValue As String
            //    sValue = CStr(frmJobEdit.CoordNum)
            //    If WritePrivateProfileString(PROFILE_SAVE_SEC_ACCOUNT, PROFILE_SAVE_KEY_COORDNUM, sValue, App.Path & "\" & App.Title & PROFILE_SAVE_EXT) = 0 Then Call Err.Raise(ERR_FATAL, , GetLastErrorMessage())






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
            if (form.Result != MdiDefine.DEFINE.vbOK)
            {
                return false;
            }
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
            Load_frmMain(sPath, fListPane);                  //Cursor = Cursors.Default;
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

            Document document = new Document();


            //23/12/29 K.setoguchi@NV---------->>>>>>>>>>

            //[VB]  Set m_clsDocument = GetDocument()
            //[VB]  m_clsChangeSelRow.Value = True
            //[VB]  UserDataPath = m_clsDocument.UserDataPath;  //'2008/10/14 NGS Yamada
            UserDataPath = @"C:\Develop\NetSurv\Src\NS-App\NS-Survey\UserData\";
            //<<<<<<<<<-----------23/12/29 K.setoguchi@NV




            //[VB]          On Error GoTo FileErrorHandler
            //[VB]          Call m_clsDocument.Load(sPath)
            //[VB]          On Error GoTo 0
            try
            {
                //サンプル>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
                //   GENBA_STRUCT_S Genba_S = new GENBA_STRUCT_S();
                //サンプル <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<


                //-----------------------------------
                // 「XXXX.data」ファイル読み込み
                //-----------------------------------

                List<GENBA_STRUCT_S> List_Genba_S = new List<GENBA_STRUCT_S>();

                document.Load(sPath, ref List_Genba_S);

#if false   
//******************************************************************:
//坂井様へ　下記の処理は　ＮＧ　です、表示処理をお願いします
//******************************************************************:
#endif
                //-----------------------------------
                // リスト表示（観測点・ベクトル表示）
                //-----------------------------------
                listPane.ListDataDispMain();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "エラーが発生");
                return;
            }
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
        //'プロジェクトを閉じる。
        private void CloseProject()
        {
            //[VB]---------------------------------------------------
            //[VB]      '砂時計。
            //[VB]      Dim clsWaitCursor As New WaitCursor
            //[VB]      Set clsWaitCursor.Object = Me
            Cursor = Cursors.WaitCursor;




        }
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




        private void mnuFileClose_Click(object sender, EventArgs e)
        {
            MessageBox.Show(mnuFileClose.Text);

        }

        private void mnuFileSave_Click(object sender, EventArgs e)
        {
            MessageBox.Show(mnuFileSave.Text);

        }

        private void mnuFileSaveAs_Click(object sender, EventArgs e)
        {
            MessageBox.Show(mnuFileSaveAs.Text);

        }

        private void mnuFileEdit_Click(object sender, EventArgs e)
        {
            frmJobEdit2 form = new frmJobEdit2();
            form.Text = "現場の編集";


            form.ShowDialog();

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
    }
}
