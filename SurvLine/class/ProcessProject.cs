//24/01/28 K.setoguchi@NV---------->>>>>>>>>>
using SurvLine.mdl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace SurvLine
{
    internal class ProcessProject
    {

        //'*******************************************************************************
        //'現場関係処理

        //Option Explicit

        //Implements ProcessInterface

        private enum PROCESS_TYPE   //'処理種別。
        {
            PROCESS_TYPE_IMPORT,        // 'インポート。
            PROCESS_TYPE_EXPORTMULTI,   //'複数エクスポート。
            PROCESS_TYPE_EXPORTSINGLE,  //'単一エクスポート。
        }

        //'インプリメンテーション
        private PROCESS_TYPE m_nProcess;    //'処理種別。

        private List<string> m_sSrcNames;   // Private m_sSrcNames() As String '出力元。参照する配列の要素は(0 To ...)。Ubound 関数を使用する。
        private string m_sPath;             // '出力先のパス。


        public ProcessProject()
        {

        }
        public string Name { get; set; }


        //'*******************************************************************************
        //'インターフェース


        //==========================================================================================
        /*[VB]
            '処理。
            '
            '引き数：
            'clsProgressInterface ProgressInterface オブジェクト。
            Private Sub ProcessInterface_Process(ByVal clsProgressInterface As ProgressInterface)
                Dim clsProjectFileManager As New ProjectFileManager

                Select Case m_nProcess
                Case PROCESS_TYPE_IMPORT
                    Call clsProjectFileManager.ImportProjectFolders(m_sSrcNames, clsProgressInterface)
                Case PROCESS_TYPE_EXPORTMULTI
                    Call clsProjectFileManager.ExportProjectFolders(m_sSrcNames, m_sPath, clsProgressInterface)
                Case PROCESS_TYPE_EXPORTSINGLE
                    Call clsProjectFileManager.ExportProjectFolder(m_sSrcNames(0), m_sPath, clsProgressInterface)
                End Select
            End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        public void ProcessInterface_Process(ProcessInterface clsProgressInterface)
        {
            ProjectFileManager clsProjectFileManager = new ProjectFileManager();

            switch (m_nProcess)
            {
                case PROCESS_TYPE.PROCESS_TYPE_IMPORT:
                    clsProjectFileManager.ImportProjectFolders(ref m_sSrcNames, clsProgressInterface);
                    break;
                case PROCESS_TYPE.PROCESS_TYPE_EXPORTMULTI:
                    clsProjectFileManager.ExportProjectFolders(ref m_sSrcNames, m_sPath, clsProgressInterface);
                    break;
                case PROCESS_TYPE.PROCESS_TYPE_EXPORTSINGLE:
                    string m_sSrcNames2 = m_sSrcNames[0];
                    clsProjectFileManager.ExportProjectFolder(m_sSrcNames2, m_sPath, clsProgressInterface);
                    break;
                default:
                    break;
            }

        }


        //==========================================================================================
        /*[VB]
            'キャンセル確認。
            '
            '戻り値：
            'キャンセルが指定されている場合は True を返す。
            'それ以外の場合は False を返す。
            Private Function ProcessInterface_ConfirmCancel() As Boolean
                ProcessInterface_ConfirmCancel = True
            End Function
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        private bool ProcessInterface_ConfirmCancel()
        {
            return true;
        }



        //'*******************************************************************************
        //'メソッド

        //==========================================================================================
        /*[VB]
            'エクスポート(nvz)ファイルをインポートするする。
            '
            'sPath で指定されるエクスポートファイルをインポートする。
            '
            '引き数：
            'sPath エクスポートファイルのパス。
            '
            '戻り値：
            '正常終了の場合 True を返す。
            'キャンセルの場合 False を返す。
            Public Function ImportProjectFolders(ByRef sPath() As String) As Boolean

                ImportProjectFolders = False

                m_nProcess = PROCESS_TYPE_IMPORT
                m_sSrcNames = sPath

                frmProgressDialog.Bar = False
                frmProgressDialog.CancelError = True
                frmProgressDialog.Prompt = "インポート中･･･"


                Set frmProgressDialog.ProcessInterface = Me


                Call frmProgressDialog.Show(1)


                Select Case frmProgressDialog.Result
                Case vbCancel
                    Exit Function
                Case vbAbort
                    Call Err.Raise(frmProgressDialog.ErrNumber, frmProgressDialog.ErrSource, frmProgressDialog.ErrDescription, frmProgressDialog.ErrHelpFile, frmProgressDialog.ErrHelpContext)
                End Select

                ImportProjectFolders = True


            End Function
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        public bool ImportProjectFolders(ref List<string> sPath)
        {
            bool ImportProjectFolders = false;

            m_nProcess = PROCESS_TYPE.PROCESS_TYPE_IMPORT;
            m_sSrcNames = sPath;

            frmProgressDialog2 frmProgressDialog = new frmProgressDialog2();

            frmProgressDialog.Bar(0,false);
            frmProgressDialog.CancelError = true;
            frmProgressDialog.Prompt("インポート中･･･");

            ProcessInterface processInterface = new ProcessInterface();
            frmProgressDialog.Set_ProcessInterface(processInterface);


            //
            //坂井様へ：処理の進捗を表示するゲージ表示です：
            frmProgressDialog.ShowDialog();   //検討中

            frmProgressDialog.Form_Activate();


            ImportProjectFolders = true;
            return ImportProjectFolders;

        }




        //==========================================================================================
        /*[VB]
            'プロジェクトフォルダをエクスポートする。
            '
            'sFolderNames で指定されるフォルダを sPath で指定されるパスにエクスポートする。
            '
            '引き数：
            'sFolderNames フォルダ名。参照する配列の要素は(0 To ...)。Ubound 関数を使用する。
            'sPath 出力先のパス。
            'bSingle 単複フラグ。True=単一フォルダである。False=複数フォルダである。
            '
            '戻り値：
            '正常終了の場合 True を返す。
            'キャンセルの場合 False を返す。
            Public Function ExportProjectFolders(ByRef sFolderNames() As String, ByVal sPath As String, ByVal bSingle As Boolean) As Boolean

                ExportProjectFolders = False

                If bSingle Then
                    m_nProcess = PROCESS_TYPE_EXPORTSINGLE
                Else
                    m_nProcess = PROCESS_TYPE_EXPORTMULTI
                End If
                m_sSrcNames = sFolderNames
                m_sPath = sPath

                frmProgressDialog.Bar = False
                frmProgressDialog.CancelError = True
                frmProgressDialog.Prompt = "エクスポート中･･･"


                Set frmProgressDialog.ProcessInterface = Me


                Call frmProgressDialog.Show(1)


                Select Case frmProgressDialog.Result
                Case vbCancel
                    Exit Function
                Case vbAbort
                    Call Err.Raise(frmProgressDialog.ErrNumber, frmProgressDialog.ErrSource, frmProgressDialog.ErrDescription, frmProgressDialog.ErrHelpFile, frmProgressDialog.ErrHelpContext)
                End Select

                ExportProjectFolders = True


            End Function
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        public bool ExportProjectFolders(ref List<string> sFolderNames, string sPath, bool bSingle)
        {
            bool ExportProjectFolders = false;


            if (bSingle)
            {
                m_nProcess = PROCESS_TYPE.PROCESS_TYPE_EXPORTSINGLE;
            }
            else
            {
                m_nProcess = PROCESS_TYPE.PROCESS_TYPE_EXPORTMULTI;
            }


            m_sSrcNames = sFolderNames;
            m_sPath = sPath;

            frmProgressDialog2 frmProgressDialog = new frmProgressDialog2();

            frmProgressDialog.Bar(0,false);
            frmProgressDialog.Bar(1,false);
            frmProgressDialog.CancelError = true;
            frmProgressDialog.Prompt("エクスポート中･･･");


            //  frmProgressDialog.ProcessInterface((object)frmProgressDialog.clsProcessInterface);

            frmProgressDialog.ShowDialog();

            switch (frmProgressDialog.Result())
            {
                case DEFINE.vbCancel:
                    return ExportProjectFolders;
                case DEFINE.vbAbort:
                    //Call Err.Raise(frmProgressDialog.ErrNumber, frmProgressDialog.ErrSource, frmProgressDialog.ErrDescription, frmProgressDialog.ErrHelpFile, frmProgressDialog.ErrHelpContext)
                    break;
                default:
                    return ExportProjectFolders;
            }


            ExportProjectFolders = true;
            return ExportProjectFolders;
        }



    }
}
//<<<<<<<<<-----------24/01/28 K.setoguchi@NV
//***************************************************************************

