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

namespace SurvLine
{
    public partial class frmJobEdit2 : Form
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
        public string GeoidoPath{ get; set; }      //As String 'ジオイドモデルのパス。

        //'セミ・ダイナミック対応。'2009/11 H.Nakamura
        public bool SemiDynaEnable;     //As Boolean 'セミ・ダイナミックオプションの有効/無効。True=有効。False=無効。
        public string SemiDynaPath;     //As String  'セミ・ダイナミックパラメータファイルのパス。
        public bool SemiDynaValid;      //As Boolean 'セミ・ダイナミックの設定の許可。True=有効。False=無効。

        public bool bOK;                //ボタンOK:true /キャンセル:false



        public frmJobEdit2()
        {
            InitializeComponent();


            //23/12/29 K.setoguchi@NV---------->>>>>>>>>>
            //      //==========================
            //      //デバック用に瀬戸口作成
            //      //==========================
            //      cmbZone.SelectedIndex = 6;
            //      txtGeoidoPath.Text = @"C:\Develop\パラメータファイル\gsigeo2011_ver1.asc";
            //      txtSemiDynaPath.Text = @"C:\Develop\パラメータファイル\SemiDyna2009.par";
            //==========================
            //<<<<<<<<<-----------23/12/29 K.setoguchi@NV

        }

        private void CancelButton_Click(object sender, EventArgs e)
        {

            Result = MdiDefine.DEFINE.vbCancel;              //ボタン(OK:1 / キャンセル:0)

            this.Close();

        }
        private void OKButton_Click(object sender, EventArgs e)
        {


            //    '入力値の検査。
            //    If Not CheckData() Then Exit Sub
            if (!CheckData()){ return;}

            //    '値を反映させる。
            //    Call ReflectData
            ReflectData();

            //    '終了。
            //    Result = vbOK
            Result = MdiDefine.DEFINE.vbOK;              //ボタン(OK:1 / キャンセル:0)


            //    Call Unload(Me)
            this.Close();


        }
        //------------------------------------------
        //    On Error GoTo ErrorHandler
        //
        //
        //    '入力値の検査。
        //    If Not CheckData() Then Exit Sub
        //    '値を反映させる。
        //    Call ReflectData
        //    '終了。
        //    Result = vbOK
        //    Call Unload(Me)
        //
        //
        //    Exit Sub
        //
        //
        //ErrorHandler:
        //            Call mdlMain.ErrorExit
        //------------------------------------------

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
            MdlGUI mdlGUI = new MdlGUI();
            if (!mdlGUI.CheckFileNameInput(txtJobName.Text, "現場名", true))
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
            if (!mdlGUI.CheckStringInputInvalid(txtDistrictName.Text, "地区名", true))
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


        private void ReflectData()
        {
            JobName = txtJobName.Text;     //現場名
            DistrictName = txtDistrictName.Text;   //地区名
            Folder = lblFolder.Text;     //フォルダ
            CoordNum = (long)cmbZone.SelectedIndex;         //平面直角座標系:
            GeoidoEnable = chkGeoidoEnable.Checked;      //ジオイド補正する
            GeoidoPath = txtGeoidoPath.Text;            //ジオイド補正パス
            SemiDynaEnable = chkSemiDynaEnable.Checked; //セミ・ダイナミック補正する    
            SemiDynaPath = txtSemiDynaPath.Text;        //セミ・ダイナミック補正パス

        }
        //-------------------------------------------------------------
        //'値を反映させる。
        //Private Sub ReflectData()
        //    '値の取得。
        //    JobName = txtJobName.Text
        //    DistrictName = txtDistrictName.Text
        //    CoordNum = cmbZone.ListIndex + 1
        //    GeoidoEnable = (chkGeoidoEnable.Value = 1)
        //    If GeoidoEnable Then GeoidoPath = txtGeoidoPath.Text
        //    
        //    'セミ・ダイナミック対応。2009/11 H.Nakamura
        //    SemiDynaEnable = (chkSemiDynaEnable.Value = 1)
        //    If SemiDynaEnable Then SemiDynaPath = txtSemiDynaPath.Text
        //End Sub

    }




}
//<<<<<<<<<-----------23/12/26 K.setoguchi@NV
