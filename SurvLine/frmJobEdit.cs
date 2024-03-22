//23/12/26 K.setoguchi@NV---------->>>>>>>>>>
using SurvLine.mdl;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using static SurvLine.mdl.MdlGUI;   //2


namespace SurvLine
{
    public partial class frmJobEdit : Form
    {

        //'*******************************************************************************
        //'現場の編集画面
        //
        //Option Explicit
        //
        //'プロパティ
        public long Result;             //As Long   'ダイアログの結果。   
        public string JobName;          //As String '現場名。
        public string DistrictName;     //As String '地区名。
        public string Folder;           //As String 'フォルダ名。
        public long CoordNum;           //As Long   '座標系番号(1～19)。
        public bool GeoidoEnable;       //As Boolean ジオイドオプションの有効/無効。True=有効。False=無効。
        public string GeoidoPath { get; set; }      //As String 'ジオイドモデルのパス。

        //'セミ・ダイナミック対応。'2009/11 H.Nakamura
        public bool SemiDynaEnable;     //As Boolean 'セミ・ダイナミックオプションの有効/無効。True=有効。False=無効。
        public string SemiDynaPath;     //As String  'セミ・ダイナミックパラメータファイルのパス。
        public bool SemiDynaValid;      //As Boolean 'セミ・ダイナミックの設定の許可。True=有効。False=無効。

        public bool bOK;                //ボタンOK:true /キャンセル:false

        //'*******************************************************************************
        //'イベント
        //'*******************************************************************************
        public frmJobEdit()
        {
            InitializeComponent();

            this.Load += Form_Load;         //24/01/25 K.setoguchi@NV---------->>>>>>>>>>


        }


        /// <summary>
        //'入力値を検査する。
        //'
        /// </summary>
        /// <returns>
        //  '戻り値：
        //  '入力値が正常である場合 True を返す。
        //  'それ以外の場合 False を返す。
        /// </returns>
        private bool CheckData()
        {
            bool bCheckData = false;

            //    '現場名。
            //    If Not CheckFileNameInput(txtJobName, "現場名") Then Exit Function
            //    Dim clsProjectFileManager As New ProjectFileManager
            //    If Not clsProjectFileManager.CheckJobName(Folder, txtJobName.Text) Then
            //        Call MsgBox("指定された現場名はすでに使用されています。", vbCritical)
            //        Call txtJobName.SetFocus
            //        Exit Function
            //    End If
            //MdlGUI mdlGUI = new MdlGUI(); //2
            if (!CheckFileNameInput(txtJobName.Text, "現場名", true))  //2
            {
                return bCheckData;
            }
            //    Dim clsProjectFileManager As New ProjectFileManager
            //    If Not clsProjectFileManager.CheckJobName(Folder, txtJobName.Text) Then
            ProjectFileManager projectFileManager = new ProjectFileManager();
            if (!projectFileManager.CheckJobName(Folder, txtJobName.Text))
            {
                return bCheckData;
            }

            //    '地区名。
            //    If Not CheckStringInputInvalid(txtDistrictName, "地区名") Then Exit Function
            if (!CheckStringInputInvalid(txtDistrictName.Text, "地区名", true))    //2
            {
                return bCheckData;
            }

            //    
            //    '座標系リストの評価。
            //    If cmbZone.ListIndex< 0 Then
            //        Call MsgBox("座標系を選択してください。", vbCritical)
            //        Call cmbZone.SetFocus
            //        Exit Function
            //    End If
            if (cmbZone.SelectedIndex < 0)
            {
                MessageBox.Show("座標系を選択してください。");
                return bCheckData;

            }


            //    Dim clsFind As New FileFind
            //    
            //    'ジオイドモデルのパスの評価。
            //    If chkGeoidoEnable.Value = 1 Then
            //        'ファイルの存在を確認。
            //        If Not clsFind.FindFile(txtGeoidoPath.Text) Then
            //            Call MsgBox("ジオイドモデルで指定されたファイルが見つかりません。", vbCritical)
            //            Call txtGeoidoPath.SetFocus
            //            Exit Function
            //        End If
            //        Select Case AltitudeSupport_EstimateFile(txtGeoidoPath.Text)
            //        Case 0
            //        Case 1
            //            Call MsgBox("ジオイドモデルで指定されたファイルが不正です。", vbCritical)
            //            Call txtGeoidoPath.SetFocus
            //            Exit Function
            //        Case Else
            //            Call Err.Raise(ERR_FATAL, , MESSAGE_GEOIDO_ESTIMATE)
            //        End Select
            //    End If
            if (chkGeoidoEnable.Checked)
            {

                //Ex.: C:\Develop\パラメータファイル\gsigeo2011_ver1.asc

                if (!System.IO.File.Exists(txtGeoidoPath.Text))
                {
                    MessageBox.Show("ジオイドモデルで指定されたファイルが見つかりません。");
                    return bCheckData;

                }
                //  var attr = System.IO.File.GetAttributes(txtGeoidoPath.Text);
                //  if ((attr & System.IO.FileAttributes.Normal) == 0)
                //  {
                //    MessageBox.Show("ジオイドモデルで指定されたファイルが不正です。");
                //    return bCheckData;
                //}
            }
            //--------------------------------------------------------------------
            //    '2009/11 H.Nakamura
            //    'セミ・ダイナミックパラメータファイルのパスの評価。
            //    If chkSemiDynaEnable.Value = 1 Then
            //        'ファイルの存在を確認。
            //        If Not clsFind.FindFile(txtSemiDynaPath.Text) Then
            //            Call MsgBox("セミ・ダイナミック補正で指定されたファイルが見つかりません。", vbCritical)
            //            Call txtSemiDynaPath.SetFocus
            //            Exit Function
            //        End If
            //        If Not mdlSemiDyna.EstimateFile(txtSemiDynaPath.Text) Then
            //            Call MsgBox("セミ・ダイナミック補正で指定されたファイルが不正です。", vbCritical)
            //            Call txtSemiDynaPath.SetFocus
            //            Exit Function
            //        End If
            //    End If
            //
            if (chkSemiDynaEnable.Checked)
            {

                //Ex.: C:\Develop\パラメータファイル\SemiDyna2009.par

                if (!System.IO.File.Exists(txtSemiDynaPath.Text))
                {
                    MessageBox.Show("セミ・ダイナミック補正で指定されたファイルが見つかりません。");
                    return bCheckData;

                }
                //var attr = System.IO.File.GetAttributes(txtSemiDynaPath.Text);
                //if ((attr & System.IO.FileAttributes.Normal) == 0)
                //{
                //    MessageBox.Show("セミ・ダイナミック補正で指定されたファイルが不正です。");
                //    return bCheckData;
                //}
            }

            bCheckData = true;
            return bCheckData;
        }
        //    '入力値を検査する。
        //'
        //'戻り値：
        //'入力値が正常である場合 True を返す。
        //'それ以外の場合 False を返す。
        //Private Function CheckData() As Boolean
        //
        //    CheckData = False
        //    
        //    '現場名。
        //    If Not CheckFileNameInput(txtJobName, "現場名") Then Exit Function
        //    Dim clsProjectFileManager As New ProjectFileManager
        //    If Not clsProjectFileManager.CheckJobName(Folder, txtJobName.Text) Then
        //        Call MsgBox("指定された現場名はすでに使用されています。", vbCritical)
        //        Call txtJobName.SetFocus
        //        Exit Function
        //    End If
        //    '地区名。
        //    If Not CheckStringInputInvalid(txtDistrictName, "地区名") Then Exit Function
        //    
        //    '座標系リストの評価。
        //    If cmbZone.ListIndex< 0 Then
        //        Call MsgBox("座標系を選択してください。", vbCritical)
        //        Call cmbZone.SetFocus
        //        Exit Function
        //    End If
        //
        //
        //    Dim clsFind As New FileFind
        //    
        //    'ジオイドモデルのパスの評価。
        //    If chkGeoidoEnable.Value = 1 Then
        //        'ファイルの存在を確認。
        //        If Not clsFind.FindFile(txtGeoidoPath.Text) Then
        //            Call MsgBox("ジオイドモデルで指定されたファイルが見つかりません。", vbCritical)
        //            Call txtGeoidoPath.SetFocus
        //            Exit Function
        //        End If
        //        Select Case AltitudeSupport_EstimateFile(txtGeoidoPath.Text)
        //        Case 0
        //        Case 1
        //            Call MsgBox("ジオイドモデルで指定されたファイルが不正です。", vbCritical)
        //            Call txtGeoidoPath.SetFocus
        //            Exit Function
        //        Case Else
        //            Call Err.Raise(ERR_FATAL, , MESSAGE_GEOIDO_ESTIMATE)
        //        End Select
        //    End If
        //    
        //    '2009/11 H.Nakamura
        //    'セミ・ダイナミックパラメータファイルのパスの評価。
        //    If chkSemiDynaEnable.Value = 1 Then
        //        'ファイルの存在を確認。
        //        If Not clsFind.FindFile(txtSemiDynaPath.Text) Then
        //            Call MsgBox("セミ・ダイナミック補正で指定されたファイルが見つかりません。", vbCritical)
        //            Call txtSemiDynaPath.SetFocus
        //            Exit Function
        //        End If
        //        If Not mdlSemiDyna.EstimateFile(txtSemiDynaPath.Text) Then
        //            Call MsgBox("セミ・ダイナミック補正で指定されたファイルが不正です。", vbCritical)
        //            Call txtSemiDynaPath.SetFocus
        //            Exit Function
        //        End If
        //    End If
        //
        //
        //    CheckData = True
        //
        //
        //End Function



        private void txtJobName_TextChanged(object sender, EventArgs e)
        {

        }




        //24/01/25 K.setoguchi@NV---------->>>>>>>>>>
        //<<<<<<<<<-----------24/01/25 K.setoguchi@NV
        //***************************************************************************
        //***************************************************************************

        //==========================================================================================
        /*[VB]
        '初期化。
        Private Sub Form_Load()

            On Error GoTo ErrorHandler
    
            '変数初期化。
            Result = vbCancel
    
            '最大文字数。
            txtJobName.MaxLength = GUI_TEXT_MAX_LENGTH
            txtDistrictName.MaxLength = GUI_TEXT_MAX_LENGTH
            txtGeoidoPath.MaxLength = MAX_PATH
    
            '値の設定。
            txtJobName.Text = JobName
            txtDistrictName.Text = DistrictName
            lblFolder.Caption = Folder
            cmbZone.ListIndex = CoordNum - 1
            chkGeoidoEnable.Value = IIf(GeoidoEnable, 1, 0)
            txtGeoidoPath.Text = GeoidoPath


            Call chkGeoidoEnable_Click
    
            'セミ・ダイナミック対応。'2009/11 H.Nakamura
            chkSemiDynaEnable.Value = IIf(SemiDynaEnable, 1, 0)
            txtSemiDynaPath.Text = SemiDynaPath


            If SemiDynaValid Then
                Call chkSemiDynaEnable_Click
            Else
                chkSemiDynaEnable.Enabled = False
                txtSemiDynaPath.Enabled = False
                cmdRefSemiDyna.Enabled = False
            End If


            Exit Sub


        ErrorHandler:
            Call mdlMain.ErrorExit

        End Sub

            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        private void Form_Load(object sender, EventArgs e)
        {
            //'変数初期化。
            Result = DEFINE.vbCancel;              //ボタン(OK:1 / キャンセル:0)

            //'最大文字数。
            txtJobName.MaxLength = (int)MdlNSGUI.GUI_TEXT_MAX_LENGTH;
            txtDistrictName.MaxLength = (int)MdlNSGUI.GUI_TEXT_MAX_LENGTH;
            txtGeoidoPath.MaxLength = (int)DEFINE.MAX_PATH;

            //'値の設定。
            txtJobName.Text = JobName;
            txtDistrictName.Text = DistrictName;
            lblFolder.Text = Folder;
            cmbZone.SelectedIndex = (int)CoordNum - 1;         //平面直角座標系:

            chkGeoidoEnable.Checked = GeoidoEnable; // (GeoidoEnable, 1, 0);
            txtGeoidoPath.Text = GeoidoPath;

            chkGeoidoEnable_Click(sender, e);


            //'セミ・ダイナミック対応。'2009 / 11 H.Nakamura
            chkSemiDynaEnable.Checked = SemiDynaEnable;    //IIf(SemiDynaEnable, 1, 0)
            txtSemiDynaPath.Text = SemiDynaPath;


            if (SemiDynaValid)
            {
                chkSemiDynaEnable_Click(sender, e);
            }
            else
            {
                chkSemiDynaEnable.Enabled = false;
                txtSemiDynaPath.Enabled = false;
                cmdRefSemiDyna.Enabled = false;

            }


        }
        //<<<<<<<<<-----------24/01/25 K.setoguchi@NV
        //***************************************************************************

        //==========================================================================================
        /*[VB]

        'テキストをすべて選択する。
        Private Sub txtJobName_GotFocus()

            On Error GoTo ErrorHandler

            Call SelectText(txtJobName)

            Exit Sub

        ErrorHandler:
            Call mdlMain.ErrorExit

        End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]

        //==========================================================================================
        /*[VB]

        'テキストをすべて選択する。
        Private Sub txtDistrictName_GotFocus()

            On Error GoTo ErrorHandler

            Call SelectText(txtDistrictName)

            Exit Sub

        ErrorHandler:
            Call mdlMain.ErrorExit

        End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]

        //==========================================================================================
        /*[VB]
        'テキストをすべて選択する。
        Private Sub txtGeoidoPath_GotFocus()

            On Error GoTo ErrorHandler

            Call SelectText(txtGeoidoPath)

            Exit Sub

        ErrorHandler:
            Call mdlMain.ErrorExit

        End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        private void txtGeoidoPath_GotFocus(object sender, EventArgs e)
        {
            return;
        }
        private void txtGeoidoPath_TextChanged(object sender, EventArgs e)
        {
            txtGeoidoPath_GotFocus(sender, e);
        }


        //==========================================================================================
        /*[VB]
        '2009/11 H.Nakamura
        'テキストをすべて選択する。
        Private Sub txtSemiDynaPath_GotFocus()

            On Error GoTo ErrorHandler

            Call SelectText(txtSemiDynaPath)

            Exit Sub

        ErrorHandler:
            Call mdlMain.ErrorExit

        End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        private void txtSemiDynaPath_GotFocus(object sender, EventArgs e)
        {
            //On Error GoTo ErrorHandler

            //SelectText(txtSemiDynaPath);

            return;

        //ErrorHandler:
            //Call mdlMain.ErrorExit;

        }
        private void txtSemiDynaPath_TextChanged(object sender, EventArgs e)
        {
            txtSemiDynaPath_GotFocus(sender, e);
        }




        //==========================================================================================
        /*[VB]
        'ジオイドモデルのパスの有効/無効化。
        Private Sub chkGeoidoEnable_Click()

            On Error GoTo ErrorHandler

            If chkGeoidoEnable.Value = 1 Then
                txtGeoidoPath.Enabled = True
                cmdRefGeoido.Enabled = True
            Else
                txtGeoidoPath.Enabled = False
                cmdRefGeoido.Enabled = False
            End If

            Exit Sub

        ErrorHandler:
            Call mdlMain.ErrorExit

        End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        private void chkGeoidoEnable_Click(object sender, EventArgs e)
        {

            //On Error GoTo ErrorHandler

            if (chkGeoidoEnable.Checked)
            {
                txtGeoidoPath.Enabled = true;
                cmdRefGeoido.Enabled = true;
            }
            else
            {
                txtGeoidoPath.Enabled = false;
                cmdRefGeoido.Enabled = false;
            }

            return;

            //ErrorHandler:
            //    Call mdlMain.ErrorExit

        }

        //==========================================================================================
        /// <summary>
        /// chkGeoidoEnable変化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkGeoidoEnable_CheckedChanged(object sender, EventArgs e)
        {
            chkGeoidoEnable_Click(sender, e);

        }
        //==========================================================================================


        //==========================================================================================
        /*[VB]
        '2009/11 H.Nakamura
        'セミ・ダイナミックパラメータファイルのパスの有効/無効化。
        Private Sub chkSemiDynaEnable_Click()

            On Error GoTo ErrorHandler

            If chkSemiDynaEnable.Value = 1 Then
                txtSemiDynaPath.Enabled = True
                cmdRefSemiDyna.Enabled = True
            Else
                txtSemiDynaPath.Enabled = False
                cmdRefSemiDyna.Enabled = False
            End If

            Exit Sub

        ErrorHandler:
            Call mdlMain.ErrorExit

        End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        private void chkSemiDynaEnable_Click(object sender, EventArgs e)
        {
            //On Error GoTo ErrorHandler

            if (chkSemiDynaEnable.Checked)
            {
                txtSemiDynaPath.Enabled = true;
                cmdRefSemiDyna.Enabled = true;
            }
            else
            {
                txtSemiDynaPath.Enabled = false;
                cmdRefSemiDyna.Enabled = false;

            }
            return;

            //ErrorHandler:
            //    Call mdlMain.ErrorExit

        }

        //==========================================================================================
        /// <summary>
        ///  chkSemiDynaEnable変換
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkSemiDynaEnable_CheckedChanged(object sender, EventArgs e)
        {
            chkSemiDynaEnable_Click(sender, e);
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'ファイルを開くダイアログでジオイドモデルのパスを設定する。
        Private Sub cmdRefGeoido_Click()

            On Error GoTo ErrorHandler

            Call Reference(txtGeoidoPath)

            Exit Sub

        ErrorHandler:
            Call mdlMain.ErrorExit

        End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /// <summary>
        ///  ファイルを開くダイアログでジオイドモデルのパスを設定する。
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdRefGeoido_Click(object sender, EventArgs e)
        {

            return;
        }

        //==========================================================================================
        /*[VB]
        '2009/11 H.Nakamura
        'ファイルを開くダイアログでセミ・ダイナミックパラメータファイルのパスを設定する。
        Private Sub cmdRefSemiDyna_Click()

            On Error GoTo ErrorHandler

            Call Reference(txtSemiDynaPath)

            Exit Sub

        ErrorHandler:
            Call mdlMain.ErrorExit

        End Sub

            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        private void cmdRefSemiDyna_Click(object sender, EventArgs e)
        {
            return;

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

            Result = DEFINE.vbCancel;              //ボタン(OK:1 / キャンセル:0)

            this.Close();

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
        /// <summary>
        /// ＯＫでダイアログを終了する。
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OKButton_Click(object sender, EventArgs e)
        {


            //    '入力値の検査。
            //    If Not CheckData() Then Exit Sub
            if (!CheckData()) { return; }

            //    '値を反映させる。
            //    Call ReflectData
            ReflectData();

            //    '終了。
            //    Result = vbOK
            Result = DEFINE.vbOK;              //ボタン(OK:1 / キャンセル:0)


            //    Call Unload(Me)
            this.Close();


        }

        //==========================================================================================
        /*[VB]
        '*******************************************************************************
        'インプリメンテーション

        'ファイルを開くダイアログでパスを設定する。
        '
        '引き数：
        'txtPath パスを設定するテキストボックス。
        Private Sub Reference(ByVal txtPath As TextBox)

            dlgCommonDialog.DialogTitle = "開く"
            dlgCommonDialog.CancelError = True
            If txtPath Is txtGeoidoPath Then
                dlgCommonDialog.DefaultExt = ""
                dlgCommonDialog.Filter = GENERAL_FILE_FILTER
            'セミ・ダイナミック対応。2009/11 H.Nakamura
            ElseIf txtPath Is txtSemiDynaPath Then
                dlgCommonDialog.DefaultExt = SEMIDYNA_FILE_EXT
                dlgCommonDialog.Filter = SEMIDYNA_FILE_FILTER
            Else
                dlgCommonDialog.DefaultExt = TKY_FILE_EXT
                dlgCommonDialog.Filter = TKY_FILE_FILTER
            End If
            dlgCommonDialog.FilterIndex = 1
            dlgCommonDialog.flags = cdlOFNFileMustExist Or cdlOFNHideReadOnly Or cdlOFNLongNames Or cdlOFNNoChangeDir Or cdlOFNPathMustExist
            dlgCommonDialog.FileName = txtPath.Text

            If Not GetFileDialog().ShowOpen(dlgCommonDialog) Then Exit Sub

            txtPath.Text = dlgCommonDialog.FileName

        End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]

        //==========================================================================================
        /*[VB]
        '入力値を検査する。
        '
        '戻り値：
        '入力値が正常である場合 True を返す。
        'それ以外の場合 False を返す。
        Private Function CheckData() As Boolean

            CheckData = False

            '現場名。
            If Not CheckFileNameInput(txtJobName, "現場名") Then Exit Function
            Dim clsProjectFileManager As New ProjectFileManager
            If Not clsProjectFileManager.CheckJobName(Folder, txtJobName.Text) Then
                Call MsgBox("指定された現場名はすでに使用されています。", vbCritical)
                Call txtJobName.SetFocus
                Exit Function
            End If
            '地区名。
            If Not CheckStringInputInvalid(txtDistrictName, "地区名") Then Exit Function

            '座標系リストの評価。
            If cmbZone.ListIndex < 0 Then
                Call MsgBox("座標系を選択してください。", vbCritical)
                Call cmbZone.SetFocus
                Exit Function
            End If

            Dim clsFind As New FileFind

            'ジオイドモデルのパスの評価。
            If chkGeoidoEnable.Value = 1 Then
                'ファイルの存在を確認。
                If Not clsFind.FindFile(txtGeoidoPath.Text) Then
                    Call MsgBox("ジオイドモデルで指定されたファイルが見つかりません。", vbCritical)
                    Call txtGeoidoPath.SetFocus
                    Exit Function
                End If
                Select Case AltitudeSupport_EstimateFile(txtGeoidoPath.Text)
                Case 0
                Case 1
                    Call MsgBox("ジオイドモデルで指定されたファイルが不正です。", vbCritical)
                    Call txtGeoidoPath.SetFocus
                    Exit Function
                Case Else
                    Call Err.Raise(ERR_FATAL, , MESSAGE_GEOIDO_ESTIMATE)
                End Select
            End If

            '2009/11 H.Nakamura
            'セミ・ダイナミックパラメータファイルのパスの評価。
            If chkSemiDynaEnable.Value = 1 Then
                'ファイルの存在を確認。
                If Not clsFind.FindFile(txtSemiDynaPath.Text) Then
                    Call MsgBox("セミ・ダイナミック補正で指定されたファイルが見つかりません。", vbCritical)
                    Call txtSemiDynaPath.SetFocus
                    Exit Function
                End If
                If Not mdlSemiDyna.EstimateFile(txtSemiDynaPath.Text) Then
                    Call MsgBox("セミ・ダイナミック補正で指定されたファイルが不正です。", vbCritical)
                    Call txtSemiDynaPath.SetFocus
                    Exit Function
                End If
            End If

            CheckData = True

        End Function

            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]




        //==========================================================================================
        /*[VB]
        '値を反映させる。
        Private Sub ReflectData()
            '値の取得。
            JobName = txtJobName.Text
            DistrictName = txtDistrictName.Text
            CoordNum = cmbZone.ListIndex + 1
            GeoidoEnable = (chkGeoidoEnable.Value = 1)
            If GeoidoEnable Then GeoidoPath = txtGeoidoPath.Text

            'セミ・ダイナミック対応。2009/11 H.Nakamura
            SemiDynaEnable = (chkSemiDynaEnable.Value = 1)
            If SemiDynaEnable Then SemiDynaPath = txtSemiDynaPath.Text
        End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /// <summary>
        /// '値を反映させる。
        /// 
        /// </summary>
        private void ReflectData()
        {
            JobName = txtJobName.Text;     //現場名
            DistrictName = txtDistrictName.Text;   //地区名
            Folder = lblFolder.Text;     //フォルダ
            CoordNum = (long)cmbZone.SelectedIndex + 1;         //平面直角座標系:      //24/01/04 K.setoguchi@NV
            GeoidoEnable = chkGeoidoEnable.Checked;      //ジオイド補正する
            GeoidoPath = txtGeoidoPath.Text;            //ジオイド補正パス
            SemiDynaEnable = chkSemiDynaEnable.Checked; //セミ・ダイナミック補正する    
            SemiDynaPath = txtSemiDynaPath.Text;        //セミ・ダイナミック補正パス

        }

    }
}
//<<<<<<<<<-----------23/12/26 K.setoguchi@NV
