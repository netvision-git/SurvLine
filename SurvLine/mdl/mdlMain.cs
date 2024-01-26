using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using System.Windows.Forms;
using static SurvLine.NetworkModel;
using System.IO;
using SurvLine.mdl;
using static SurvLine.mdl.MdlNSDefine;
using static SurvLine.mdl.MdlNSSDefine;
//using static SurvLine.mdl.MdlBaseLineAnalyser;


namespace SurvLine.mdl
{
    public class MdlMain
    {
        //==========================================================================================
        /*[VB]
        '*******************************************************************************
        'メインモジュール

        Option Explicit
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        Private m_clsDocument As Document 'ドキュメント。
        Private m_clsFileDialog As FileDialog 'ファイルダイアログ。
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        private Document m_clsDocument;         //'ドキュメント。
        private FileDialog m_clsFileDialog;     //'ファイルダイアログ。

        private Random rnd;

        private MdlMain mdlMain;
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'アプリケーションエントリポイント。
        Sub Main()

            On Error GoTo ErrorHandler
    
            '２重起動防止。
            If App.PrevInstance Then
                End
                Exit Sub
            End If


            Call Randomize
    
            'テンポラリディレクトリを作成する。
            Call CreateDir(App.Path & TEMPORARY_PATH, True)
            Call CreateDir(App.Path & TEMPORARY_PATH & "." & OBSPOINT_PATH, True)
            Call CreateDir(App.Path & TEMPORARY_PATH & "." & ANALYSIS_PATH, True)
            Call CreateDir(App.Path & TEMPORARY_PATH & PPS_PATH, True)
            Call CreateDir(App.Path & TEMPORARY_PATH & NSCAB_PATH, True)
            Call CreateDir(App.Path & TEMPORARY_PATH & IMPORT_PATH, True)   '2007/9/5 NGS Yamada
            Call CreateDir(App.Path & TEMPORARY_PATH & EXPORT_PATH, True)   '2022/10/12 Hitz H.Nakamura エクスポートでRINEXバージョンの選択を追加。
    
            Call InitializeBaseLineAnalyser
            Call InitializeAccountMake


            If App.StartMode = vbSModeStandalone Then
                Call frmMain.Show
                If Not CheckNetFramework() Then
                    '2022/03/29 Hitz H.Nakamura '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                    '.NET Framework 2.0 SP1 が無くても起動できるようにする。
                    'Err.Description = ".NET Framework 2.0 SP1 をｲﾝｽﾄｰﾙしてください。"
                    'ErrorExit
                    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                    Call MsgBox(".NET Framework 2.0 SP1 の確認ができませんでした。別途インストールされていることを確認してください。", vbCritical)
                    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                End If
            End If


            Exit Sub


        ErrorHandler:
            Call mdlMain.ErrorExit


        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'アプリケーションエントリポイント。
        public void Main()
        {

            //Mutex名を決める（必ずアプリケーション固有の文字列に変更すること！）
            string mutexName = "SurvLine";
            //Mutexオブジェクトを作成する
            System.Threading.Mutex mutex = new System.Threading.Mutex(false, mutexName);

            bool hasHandle = false;

            try
            {
                //'２重起動防止。
                try
                {
                    //ミューテックスの所有権を要求する
                    hasHandle = mutex.WaitOne(0, false);
                }
                catch (System.Threading.AbandonedMutexException)
                {
                    //別のアプリケーションがミューテックスを解放しないで終了した時
                    hasHandle = true;
                }
                //ミューテックスを得られたか調べる
                if (hasHandle == false)
                {
                    //得られなかった場合は、すでに起動していると判断して終了
                    _ = MessageBox.Show("多重起動はできません。");
                    return;
                }

                rnd = new Random();

                //'テンポラリディレクトリを作成する。
                MdlUtility clsMdlUtility = new MdlUtility();
                string AppPath = Path.GetDirectoryName(Application.ExecutablePath);
                _ = clsMdlUtility.CreateDir(AppPath + TEMPORARY_PATH, true);
                _ = clsMdlUtility.CreateDir(AppPath + TEMPORARY_PATH + "." + OBSPOINT_PATH, true);
                _ = clsMdlUtility.CreateDir(AppPath + TEMPORARY_PATH + "." + ANALYSIS_PATH, true);
                _ = clsMdlUtility.CreateDir(AppPath + TEMPORARY_PATH + PPS_PATH, true);
                _ = clsMdlUtility.CreateDir(AppPath + TEMPORARY_PATH + NSCAB_PATH, true);
                _ = clsMdlUtility.CreateDir(AppPath + TEMPORARY_PATH + IMPORT_PATH, true);
                _ = clsMdlUtility.CreateDir(AppPath + TEMPORARY_PATH + EXPORT_PATH, true);   //'2022/10/12 Hitz H.Nakamura エクスポートでRINEXバージョンの選択を追加。

                MdlBaseLineAnalyser clsMdlBaseLineAnalyser = new MdlBaseLineAnalyser();
                clsMdlBaseLineAnalyser.InitializeBaseLineAnalyser();

                MdlAccountMakeNSS clsMdlAccountMakeNSS = new MdlAccountMakeNSS();
                clsMdlAccountMakeNSS.InitializeAccountMake();

#if false
                if (App.StartMode == vbSModeStandalone)
                {
                    frmMain clsfrmMain = new frmMain();
                    clsfrmMain.Show();
                    if (!CheckNetFramework())
                    {
                        //'2022/03/29 Hitz H.Nakamura '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                        //'.NET Framework 2.0 SP1 が無くても起動できるようにする。
                        //'Err.Description = ".NET Framework 2.0 SP1 をｲﾝｽﾄｰﾙしてください。"
                        //'ErrorExit
                        //'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                        //MsgBox(".NET Framework 2.0 SP1 の確認ができませんでした。別途インストールされていることを確認してください。", vbCritical);
                        //'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                        _ = MessageBox.Show(".NET Framework 2.0 SP1 の確認ができませんでした。別途インストールされていることを確認してください。");
                    }
                }
#else
                //frmMain clsfrmMain = new frmMain();
                //clsfrmMain.Show();
                if (!CheckNetFramework())
                {
                    //'2022/03/29 Hitz H.Nakamura '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                    //'.NET Framework 2.0 SP1 が無くても起動できるようにする。
                    //'Err.Description = ".NET Framework 2.0 SP1 をｲﾝｽﾄｰﾙしてください。"
                    //'ErrorExit
                    //'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                    //MsgBox(".NET Framework 2.0 SP1 の確認ができませんでした。別途インストールされていることを確認してください。", vbCritical);
                    //'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                    _ = MessageBox.Show(".NET Framework 2.0 SP1 の確認ができませんでした。別途インストールされていることを確認してください。");
                }
#endif
            }

            catch (Exception ex)
            {
                mdlMain.ErrorExit();
                return;
            }

            finally
            {
                /*
                if (hasHandle)
                {
                    //ミューテックスを解放する
                    mutex.ReleaseMutex();
                }
                mutex.Close();
                */
            }
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'ドキュメントを取得する。
        '
        '戻り値：Document オブジェクト。
        Public Function GetDocument() As Document
            If m_clsDocument Is Nothing Then Set m_clsDocument = New Document
            Set GetDocument = m_clsDocument
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        'ドキュメントを取得する。
        '
        '戻り値：Document オブジェクト。
        */
        public Document GetDocument()
        {
            if (m_clsDocument == null)
            {
                m_clsDocument = new Document();
                m_clsDocument.Class_Initialize();
            }
            return m_clsDocument;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'ファイルダイアログを取得する。
        '
        '戻り値：FileDialog オブジェクト。
        Public Function GetFileDialog() As FileDialog
            If m_clsFileDialog Is Nothing Then Set m_clsFileDialog = New FileDialog
            Set GetFileDialog = m_clsFileDialog
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        'ファイルダイアログを取得する。
        '
        '戻り値：FileDialog オブジェクト。
        */
        public FileDialog GetFileDialog()
        {
#if false
            /*
             *************************** 修正要 sakai
             */
             * if (m_clsFileDialog == null) { FileDialog m_clsFileDialog = new FileDialog; }
#endif
            return m_clsFileDialog;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'エラー終了。
        '
        'メッセージを出力し、アプリケーションを終了する。
        Public Sub ErrorExit()
            Dim sMsg As String
            If Err.Description<> "" Then sMsg = Err.Description & vbCrLf & vbCrLf
            Call MsgBox(sMsg & "予期せぬｴﾗｰが発生しました。" & vbCrLf & "ｱﾌﾟﾘｹｰｼｮﾝを終了します。", vbCritical)
            Call EndControll
            Set m_clsDocument = Nothing
            Set m_clsFileDialog = Nothing
            End
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        'エラー終了。
        '
        'メッセージを出力し、アプリケーションを終了する。
        */
        public void ErrorExit()
        {
            string sMsg;
#if false
            /*
             *************************** 修正要 sakai
             */
            if (Err.Description != "") { sMsg = Err.Description & "\r\n" & "\r\n"; }
            Call MsgBox(sMsg &"予期せぬｴﾗｰが発生しました。" & vbCrLf & "ｱﾌﾟﾘｹｰｼｮﾝを終了します。", vbCritical)
            EndControll();
#endif
            m_clsDocument = null;
            m_clsFileDialog = null;
            return;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '.Net Framework のチェック。
        '
        Public Function CheckNetFramework() As Boolean

            On Error GoTo ErrorHandler


            Dim clsRegSoftware As New RegKey
            If Not clsRegSoftware.OpenKey(HKEY_LOCAL_MACHINE, "SOFTWARE") Then Exit Function
            Dim clsRegMicrosoft As New RegKey
            If Not clsRegMicrosoft.OpenKey(clsRegSoftware.Key, "Microsoft") Then Exit Function
            Dim clsRegNetFrameworkSetup As New RegKey
            If Not clsRegNetFrameworkSetup.OpenKey(clsRegMicrosoft.Key, "NET Framework Setup") Then Exit Function
            Dim clsRegNdp As New RegKey
            If Not clsRegNdp.OpenKey(clsRegNetFrameworkSetup.Key, "NDP") Then Exit Function

            Dim sKeyName As String
            Dim nIndex As Long
            nIndex = 0
            Do
                sKeyName = clsRegNdp.EnumKeyName(nIndex, 64)
                If sKeyName = "" Then Exit Do
                If Left$(sKeyName, 1) = "v" Then
                    Dim clsRegNetVersion As New RegKey
                    If Not clsRegNetVersion.OpenKey(clsRegNdp.Key, sKeyName) Then Exit Function
                    Dim nInstall As Long
                    nInstall = clsRegNetVersion.QueryValue("Install", 0)
                    If nInstall = 1 Then
                        Dim sVersion As String
                        sVersion = clsRegNetVersion.QueryValueString("Version", "")
                        If Left$(sVersion, 2) = "3." Then
                            CheckNetFramework = True
                            Exit Do
                        ElseIf Left$(sVersion, 2) = "2." Then
                            Dim nSp As Long
                            nSp = clsRegNetVersion.QueryValue("SP", 0)
                            If nSp >= 1 Then
                                CheckNetFramework = True
                                Exit Do
                            End If
                        End If
                        Dim nSub As Long
                        nSub = 0
                        Do
                            sKeyName = clsRegNetVersion.EnumKeyName(nSub, 64)
                            If sKeyName = "" Then Exit Do
                            Dim clsRegSub As New RegKey
                            If Not clsRegSub.OpenKey(clsRegNetVersion.Key, sKeyName) Then Exit Function
                            nInstall = clsRegSub.QueryValue("Install", 0)
                            If nInstall = 1 Then
                                sVersion = clsRegSub.QueryValueString("Version", "")
                                If Left$(sVersion, 2) = "3." Then
                                    CheckNetFramework = True
                                    Exit Do
                                ElseIf Left$(sVersion, 2) = "2." Then
                                    nSp = clsRegSub.QueryValue("SP", 0)
                                    If nSp >= 1 Then
                                        CheckNetFramework = True
                                        Exit Do
                                    End If
                                End If
                            End If
                            nSub = nSub + 1
                        Loop
                        If sKeyName<> "" Then Exit Do
                    End If
                End If
                nIndex = nIndex + 1
            Loop

        ErrorHandler:


        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '.Net Framework のチェック。
        '
        */
        public bool CheckNetFramework()
        {
            return true;
        }
        //==========================================================================================
    }
}
