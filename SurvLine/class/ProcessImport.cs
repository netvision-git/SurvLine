//24/01/28 K.setoguchi@NV---------->>>>>>>>>>
using SurvLine.mdl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static SurvLine.mdl.MdiDefine;
using static SurvLine.mdl.MdlNSDefine;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace SurvLine
{
    internal class ProcessImport
    {

        //'*******************************************************************************
        //'インポート処理

        //Option Explicit



        //==========================================================================================
        /*[VB]
        Implements ProcessInterface;
        //'インプリメンテーション
        Private m_sPath() As String 'インポートするファイルのパス。参照する配列の要素は(0 To ...)。
        Private m_nImportType As IMPORT_TYPE 'インポートファイル種別。
        Private m_bMsg As Boolean 'メッセージの有無。True=ファイルに無効なデータが含まれていた場合メッセージを表示する。False=メッセージは表示しない。

            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]

        //  private interface ProcessInterface;

        private List<string> m_sPath;           //'インポートするファイルのパス。参照する配列の要素は(0 To ...)。
        private IMPORT_TYPE m_nImportType;  //'インポートファイル種別。
        private bool m_bMsg;                //'メッセージの有無。True=ファイルに無効なデータが含まれていた場合メッセージを表示する。False=メッセージは表示しない。



        //'*******************************************************************************
        //'インターフェース

        //==========================================================================================
        /*[VB]
        '処理。
        '
        '引き数：
        'clsProgressInterface ProgressInterface オブジェクト。
        Private Sub ProcessInterface_Process(ByVal clsProgressInterface As ProgressInterface)
            If Not GetDocument().Import(m_nImportType, m_sPath, clsProgressInterface) Then
                If m_bMsg Then Call MsgBox("無効なデータが含まれていました。", vbExclamation)
            End If
        End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
#if false
        private void ProcessInterface_Process(ProgressInterface clsProgressInterface)
        {
            if (!GetDocument().Import(m_nImportType, m_sPath, clsProgressInterface){

                if (m_bMsg)
                {
                    MessageBox.Show("無効なデータが含まれていました。", "エラー発生");
                }
            }

        }
#endif
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
            bool ProcessInterface_ConfirmCancel = true;
            return ProcessInterface_ConfirmCancel;
        }



        //'*******************************************************************************
        //'メソッド


        //==========================================================================================
        /*[VB]
        '外部ファイルをインポートする。
        '
        '引き数：
        'nImportType インポートファイル種別。
        'sPath インポートするファイルのパス。参照する配列の要素は(0 To ...)。
        'bMsg メッセージの有無。True=ファイルに無効なデータが含まれていた場合メッセージを表示する。False=メッセージは表示しない。
        Public Sub Import(ByVal nImportType As IMPORT_TYPE, ByRef sPath() As String, Optional ByVal bMsg As Boolean = True)

            m_sPath = sPath
            m_nImportType = nImportType
            m_bMsg = bMsg

            frmProgressDialog.Bar = False
            frmProgressDialog.CancelError = True


            Select Case nImportType
            Case IMPORT_TYPE_UNKNOWN
                frmProgressDialog.Prompt = "外部ﾌｧｲﾙのｲﾝﾎﾟｰﾄ中･･･"
            Case IMPORT_TYPE_JOB
                frmProgressDialog.Prompt = "Geodimeterｼﾞｮﾌﾞﾌｧｲﾙのｲﾝﾎﾟｰﾄ中･･･"
            Case IMPORT_TYPE_NVF
                frmProgressDialog.Prompt = "NetSurvﾍﾞｸﾄﾙﾃﾞｰﾀﾌｧｲﾙのｲﾝﾎﾟｰﾄ中･･･"
            Case IMPORT_TYPE_NVB
                frmProgressDialog.Prompt = "NetSurvﾍﾞｸﾄﾙﾊﾞｲﾅﾘﾌｧｲﾙのｲﾝﾎﾟｰﾄ中･･･"
            Case IMPORT_TYPE_CSV
                frmProgressDialog.Prompt = "CSVﾍﾞｸﾄﾙﾃﾞｰﾀﾌｧｲﾙのｲﾝﾎﾟｰﾄ中･･･"
            Case IMPORT_TYPE_DAT, IMPORT_TYPE_DIRECT   '2007/4/10 NGS Yamada
                frmProgressDialog.Prompt = "NetSurvﾃﾞｰﾀﾌｧｲﾙのｲﾝﾎﾟｰﾄ中･･･"
            Case IMPORT_TYPE_RINEX
                frmProgressDialog.Prompt = "RINEXﾌｧｲﾙのｲﾝﾎﾟｰﾄ中･･･"
            Case IMPORT_TYPE_PC
                frmProgressDialog.Prompt = "観測ﾃﾞｰﾀﾌｧｲﾙのｲﾝﾎﾟｰﾄ中･･･"
            Case IMPORT_TYPE_PDA
                frmProgressDialog.Prompt = "観測ﾃﾞｰﾀﾌｧｲﾙのｲﾝﾎﾟｰﾄ中･･･"
            Case IMPORT_TYPE_ANDROID
                frmProgressDialog.Prompt = "観測ﾃﾞｰﾀﾌｧｲﾙのｲﾝﾎﾟｰﾄ中･･･"
            End Select


            Set frmProgressDialog.ProcessInterface = Me


            Call frmProgressDialog.Show(1)


            If frmProgressDialog.Result = vbAbort Then Call Err.Raise(frmProgressDialog.ErrNumber, frmProgressDialog.ErrSource, frmProgressDialog.ErrDescription, frmProgressDialog.ErrHelpFile, frmProgressDialog.ErrHelpContext)

        End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        public void Import(IMPORT_TYPE nImportType, ref List<string> sPath, bool bMsg = true)
        {
            m_sPath = sPath;
            m_nImportType = nImportType;
            m_bMsg = bMsg;

            frmProgressDialog2 frmProgressDialog = new frmProgressDialog2();

            frmProgressDialog.Bar(0, false);
            frmProgressDialog.Bar(1, false);

            frmProgressDialog.CancelError = true;


            switch (nImportType)
            {
                case IMPORT_TYPE.IMPORT_TYPE_UNKNOWN:
                    frmProgressDialog.Text = "外部ﾌｧｲﾙのｲﾝﾎﾟｰﾄ中･･･";
                    break;
                case IMPORT_TYPE.IMPORT_TYPE_JOB:
                    frmProgressDialog.Text = "Geodimeterｼﾞｮﾌﾞﾌｧｲﾙのｲﾝﾎﾟｰﾄ中･･･";
                    break;
                case IMPORT_TYPE.IMPORT_TYPE_NVF:
                    frmProgressDialog.Text = "NetSurvﾍﾞｸﾄﾙﾃﾞｰﾀﾌｧｲﾙのｲﾝﾎﾟｰﾄ中･･･";
                    break;
                case IMPORT_TYPE.IMPORT_TYPE_NVB:
                    frmProgressDialog.Text = "NetSurvﾍﾞｸﾄﾙﾊﾞｲﾅﾘﾌｧｲﾙのｲﾝﾎﾟｰﾄ中･･･";
                    break;
                case IMPORT_TYPE.IMPORT_TYPE_CSV:
                    frmProgressDialog.Text = "CSVﾍﾞｸﾄﾙﾃﾞｰﾀﾌｧｲﾙのｲﾝﾎﾟｰﾄ中･･･";
                    break;
                case IMPORT_TYPE.IMPORT_TYPE_DIRECT:       //'2007/4/10 NGS Yamada
                    frmProgressDialog.Text = "NetSurvﾃﾞｰﾀﾌｧｲﾙのｲﾝﾎﾟｰﾄ中･･･";
                    break;
                case IMPORT_TYPE.IMPORT_TYPE_RINEX:
                    frmProgressDialog.Text = "RINEXﾌｧｲﾙのｲﾝﾎﾟｰﾄ中･･･";
                    break;
                case IMPORT_TYPE.IMPORT_TYPE_PC:
                    frmProgressDialog.Text = "観測ﾃﾞｰﾀﾌｧｲﾙのｲﾝﾎﾟｰﾄ中･･･";
                    break;
                case IMPORT_TYPE.IMPORT_TYPE_PDA:
                    frmProgressDialog.Text = "観測ﾃﾞｰﾀﾌｧｲﾙのｲﾝﾎﾟｰﾄ中･･･";
                    break;
                case IMPORT_TYPE.IMPORT_TYPE_ANDROID:
                    frmProgressDialog.Text = "観測ﾃﾞｰﾀﾌｧｲﾙのｲﾝﾎﾟｰﾄ中･･･";
                    break;
                default:
                    break;
            }

            //Set frmProgressDialog.ProcessInterface = Me


            frmProgressDialog.ShowDialog();


            if (frmProgressDialog.Result() == DEFINE.vbAbort)
            {
                //  Then Call Err.Raise(frmProgressDialog.ErrNumber, frmProgressDialog.ErrSource, frmProgressDialog.ErrDescription, frmProgressDialog.ErrHelpFile, frmProgressDialog.ErrHelpContext)
                string Meg;
                Meg = $"{frmProgressDialog.ErrNumber()}{frmProgressDialog.ErrSource()}{frmProgressDialog.ErrDescription()}{frmProgressDialog.ErrHelpFile()}{frmProgressDialog.ErrHelpContext()}";
                _ = MessageBox.Show(Meg, "エラー発生");
            }




        }





    }
}
