//24/01/28 K.setoguchi@NV---------->>>>>>>>>>
using SurvLine.mdl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using static SurvLine.mdl.MdlUtility;

namespace SurvLine
{
    internal class FileDialog
    {
        MdlUtility mdlUtility = new MdlUtility();


        //'*******************************************************************************
        //'ファイルダイアログ
        //'
        //'フォルダーパスを管理する。
        //'アプリでこのオブジェクトを一つだけインスタンス化することにより、フォルダーパスを常に記憶する。
        //'アプリ終了時にはレジストリにフォルダーパスを保存する。
        //
        //Option Explicit
        //
        //'インプリメンテーション
        private string m_sFolderPath;   //As String 'フォルダーパス。



        public FileDialog()
        {
            Class_Initialize();
        }



        //'*******************************************************************************
        //'プロパティ
        //'*******************************************************************************

        //==========================================================================================
        /*[VB]
        'フォルダーパス。
        Property Let FolderPath(ByVal sFolderPath As String)
            Dim sDrive As String
            Dim sDir As String
            Dim sTitle As String
            Dim sExt As String
            Call SplitPath(sFolderPath, sDrive, sDir, sTitle, sExt)
            m_sFolderPath = sDrive & sDir
        End Property
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        public void FolderPath(string sFolderPath)
        {

        }

        //==========================================================================================
        /*[VB]
        'フォルダーパス。
        Property Get FolderPath() As String
            FolderPath = m_sFolderPath
        End Property
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        public string FolderPath()
        {
            string FolderPath = m_sFolderPath;
            return FolderPath;
        }


        //'*******************************************************************************
        //'イベント
        //'*******************************************************************************

        //==========================================================================================
        /*[VB]
        '初期化。
        Private Sub Class_Initialize()
            'フォルダーパスを読み込む。
            Dim clsRegNsNetwork As New RegKey
            Set clsRegNsNetwork = OpenRegistry()
            m_sFolderPath = clsRegNsNetwork.QueryValueString(REG_KEY_FOLDERPATH, App.Path)
        End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        private void Class_Initialize()
        {

        }

        //==========================================================================================
        /*[VB]
        '終了処理。
        Private Sub Class_Terminate()
            'フォルダーパスを保存する。
            Dim clsRegNsNetwork As New RegKey
            Set clsRegNsNetwork = OpenRegistry()
            Call clsRegNsNetwork.SetString(REG_KEY_FOLDERPATH, m_sFolderPath)
        End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]

        //==========================================================================================
        /*[VB]
        '*******************************************************************************
        'メソッド

        //==========================================================================================
        /*[VB]
            'ファイルを開くダイアログ。
            '
            '引き数：
            'dlgCommonDialog CommonDialog オブジェクト。
            '
            '戻り値：
            'ダイアログがOKで終了した場合 True を返す。
            'それ以外の場合 False を返す。
            Public Function ShowOpen(ByVal dlgCommonDialog As CommonDialog) As Boolean

                ShowOpen = False

                Dim sDrive As String
                Dim sDir As String
                Dim sTitle As String
                Dim sExt As String
                Call SplitPath(dlgCommonDialog.FileName, sDrive, sDir, sTitle, sExt)
                Dim sPath As String
                sPath = RTrimEx(sDrive & sDir, "\")


                Dim clsFind As New FileFind
                If clsFind.FindFile(sPath) Then
                    dlgCommonDialog.InitDir = sPath
                Else
                    dlgCommonDialog.InitDir = m_sFolderPath
                End If


                dlgCommonDialog.FileName = sTitle & sExt


                On Error GoTo CancelHandler
                dlgCommonDialog.ShowOpen
                On Error GoTo 0


                FolderPath = dlgCommonDialog.FileName


                ShowOpen = True


                Exit Function


            CancelHandler:
                If Err.Number = cdlBufferTooSmall Then
                    Call MsgBox("選択されたﾌｧｲﾙが多すぎます。", vbCritical)
                ElseIf Err.Number = cdlCancel Then
                Else
                    Call MsgBox(Err.Description, vbCritical)
                End If

            End Function
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        public bool ShowOpen(OpenFileDialog dlgCommonDialog)
        {
            bool ShowOpen = false;


            string sDrive = "";
            string sDir = "";
            string sTitle = "";
            string sExt = "";
            SplitPath(dlgCommonDialog.FileName, ref sDrive, ref sDir, ref sTitle, ref sExt);
            string sPath;


            sPath = RTrimEx($"{sDrive}{sDir}", "\\");


            if(System.IO.File.Exists(sPath))
            {
                //存在する
                dlgCommonDialog.InitialDirectory = sPath;

            }
            else
            {
                //存在しない
                dlgCommonDialog.InitialDirectory = m_sFolderPath;
            }

            dlgCommonDialog.FileName = $"{sTitle}{sExt}";

            try
            {

                if (dlgCommonDialog.ShowDialog() == DialogResult.OK)
                {
                    FolderPath(dlgCommonDialog.FileName);

                    ShowOpen = true;
                    return ShowOpen;
                }


            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "エラー発生");
#if false
                If Err.Number = cdlBufferTooSmall Then
                    Call MsgBox("選択されたﾌｧｲﾙが多すぎます。", vbCritical)
                ElseIf Err.Number = cdlCancel Then
                Else
                    Call MsgBox(Err.Description, vbCritical)
                End If
#endif

            }


            return ShowOpen;

        }




        //==========================================================================================
        /*[VB]
        '名前を付けて保存ダイアログ。
        '
        '引き数：
        'dlgCommonDialog CommonDialog オブジェクト。
        '
        '戻り値：
        'ダイアログがOKで終了した場合 True を返す。
        'それ以外の場合 False を返す。
        Public Function ShowSave(ByVal dlgCommonDialog As CommonDialog) As Boolean

            ShowSave = False

            Dim sDrive As String
            Dim sDir As String
            Dim sTitle As String
            Dim sExt As String
            Call SplitPath(dlgCommonDialog.FileName, sDrive, sDir, sTitle, sExt)
            Dim sPath As String
            sPath = RTrimEx(sDrive & sDir, "\")


            Dim clsFind As New FileFind
            If clsFind.FindFile(sPath) Then
                dlgCommonDialog.InitDir = sPath
            Else
                dlgCommonDialog.InitDir = m_sFolderPath
            End If


            dlgCommonDialog.FileName = sTitle & sExt


            On Error GoTo CancelHandler
            dlgCommonDialog.ShowSave
            On Error GoTo 0


            FolderPath = dlgCommonDialog.FileName


            ShowSave = True


            Exit Function


        CancelHandler:
            If Err.Number<> cdlCancel Then Call MsgBox(Err.Description, vbCritical)

        End Function

            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        public bool ShowSave(OpenFileDialog dlgCommonDialog)
        {
            bool ShowSave = false;

            string sDrive = "";
            string sDir = "";
            string sTitle = "";
            string sExt = "";
            SplitPath(dlgCommonDialog.FileName, ref sDrive, ref sDir, ref sTitle, ref sExt);
            string sPath;
            sPath = RTrimEx($"{sDrive}{sDir}", "\\");


            if (System.IO.File.Exists(sPath))
            {
                dlgCommonDialog.InitialDirectory = sPath;
            }
            else
            {
                dlgCommonDialog.InitialDirectory = m_sFolderPath;
            }

            dlgCommonDialog.FileName = $"{sTitle}{sExt}";


            //On Error GoTo CancelHandler
            try
            {
                if (dlgCommonDialog.ShowDialog() == DialogResult.OK)
                {
                    FolderPath(dlgCommonDialog.FileName);
                    ShowSave = true;

                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "エラー発生");
#if false   //検討
                //CancelHandler:
                //  If Err.Number<> cdlCancel Then Call MsgBox(Err.Description, vbCritical)
#endif      //検討

            }

            return ShowSave;
        }







}
}
//<<<<<<<<<-----------24/01/28 K.setoguchi@NV
