using SurvLine.mdl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SurvLine.mdl.MdiDefine;
using static SurvLine.mdl.DEFINE;
//  using static SurvLine.mdl.MdiDefine.DEFINE;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using System.Windows.Forms;

namespace SurvLine
{
    internal class FolderDialog
    {

        MdlUtility mdlUtility = new MdlUtility();
        MdiVBfunctions mdiVBfunctions = new MdiVBfunctions();


        public BROWSEINFO bi = new BROWSEINFO();

        //'*******************************************************************************
        //'フォルダダイアログ

        //Option Explicit

        //'プロパティ
        public string Caption;  //'キャプション。
        public string Title;    //'タイトル。
        public string Path;     //'パス。
        public bool CancelError;    //'キャンセル時にcdlCancelエラーを発生させる。

        public FolderDialog()
        {

        }

        //'*******************************************************************************
        //'メソッド


        //==========================================================================================
        /*[VB]
        'フォルダを開くダイアログ。
        '
        '引き数：
        'hWnd オーナーウィンドウのハンドル。
        Public Sub ShowOpen(ByVal hWnd As Long)

            On Error GoTo ErrorHandler


            Dim bi As BROWSEINFO
            bi.hwndOwner = hWnd
            bi.pidlRoot = 0
            bi.pszDisplayName = Space$(MAX_PATH)
            bi.lpszTitle = Title & vbNullChar
            bi.ulFlags = BIF_DEFAULT Or BIF_NEWDIALOGSTYLE
            bi.lpfn = FnPtrToLong(AddressOf BrowseCallbackProc)
            bi.lParam = 0


            Set FOLDER_DIALOG = Me


            Dim pidl As Long
            pidl = SHBrowseForFolder(bi)


            If pidl <> 0 Then
                Dim sPath As String* MAX_PATH
                Call SHGetPathFromIDList(pidl, sPath)
                Path = Left$(sPath, InStr(sPath, vbNullChar) - 1)
                Call CoTaskMemFree(pidl)
            Else
                If CancelError Then Call Err.Raise(cdlCancel)
            End If


            Set FOLDER_DIALOG = Nothing


            Exit Sub


        ErrorHandler:
            Set FOLDER_DIALOG = Nothing
            Call Err.Raise(Err.Number, Err.Source, Err.Description, Err.HelpFile, Err.HelpContext)


        End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /// <summary>
        /// 'フォルダを開くダイアログ。
        /// '
        /// '引き数：
        /// 'hWnd オーナーウィンドウのハンドル。
        /// 
        /// </summary>
        /// <param name="hWnd"></param>
        /// 
        public long BrowseCallbackProc()
        {
            return 0;
        }
        //------------------------------------------------------------------------------------------
        public long FolderBrowserDialog_Disp(ref string SelectedPath)
        {

            FolderBrowserDialog fbd = new FolderBrowserDialog();

            fbd.Description = "マイ・コンピュータ以下から選択"
                            + "\n-［新しいフォルダ］ボタン非表示"
                            + "\n- 開始フォルダはc:\\Inetpub\\wwwroot";

            fbd.RootFolder = Environment.SpecialFolder.MyComputer;
            fbd.SelectedPath = @"c:\inetpub\wwwroot";
            fbd.ShowNewFolderButton = false;


            DialogResult dr = fbd.ShowDialog();

            if (dr == DialogResult.OK)
            {
                Console.WriteLine(fbd.SelectedPath);

                SelectedPath = fbd.SelectedPath;
                return 0;
            }
            return 1;
        }
        //------------------------------------------------------------------------------------------
        public long FolderBrowserDialog1()
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();

            fbd.Description = "説明テキストがここに表示されます。";

            DialogResult dr = fbd.ShowDialog();

            if (dr == DialogResult.OK)
            {
                //Console.WriteLine(fbd.SelectedPath);
                // 出力例：C:\Documents and Settings\taka-e\デスクトップ
                return 1;   //OK
            }
            return 0;
        }
        //------------------------------------------------------------------------------------------
        public void ShowOpen(IntPtr hWnd)
        {

            try
            {
                //On Error GoTo ErrorHandler

                FolderBrowserDialog fbd = new FolderBrowserDialog();

                fbd.Description = "説明テキストがここに表示されます。";

                DialogResult dr = fbd.ShowDialog();

                if (dr == DialogResult.OK)
                {
                    string sPath;
                    sPath = fbd.SelectedPath;
#if false
                    //    Path = mdiVBfunctions.Left(sPath, mdiVBfunctions.InStr(sPath, "\0") - 1);
                    //CoTaskMemFree(pidl)
#else
                    Path = fbd.SelectedPath;
#endif
                }
                else
                {
                    if (CancelError)
                    {
                        //Then Call Err.Raise(cdlCancel)
                        MessageBox.Show("フォルダを選択をして下さい。", "エラー発生");

                    }
                }


            }
            catch (Exception ex)
            {
                //ErrorHandler:
                //mdlFolderDialog.FOLDER_DIALOG = null;
                //  Call Err.Raise(Err.Number, Err.Source, Err.Description, Err.HelpFile, Err.HelpContext)
                MessageBox.Show(ex.Message + Path, "エラー発生");
            }

#if false

            MdlFolderDialog mdlFolderDialog = new MdlFolderDialog();
            try
            {
                //On Error GoTo ErrorHandler

                FolderBrowserDialog1();



                BROWSEINFO bi = new BROWSEINFO();
                //BROWSEINFO bi;
                bi.hwndOwner = (long)hWnd;
                bi.pidlRoot = 0;
                bi.pszDisplayName = "";
                bi.pszDisplayName = bi.pszDisplayName.PadLeft((int)DEFINE.MAX_PATH);
                bi.lpszTitle = Title;   // & vbNullChar
                bi.ulFlags = BIF_DEFAULT | BIF_NEWDIALOGSTYLE;

                //  Button1.Click -= new EventHandler(Button1_Click);
                MdlFolderDialog mdlFolderDialog1 = new MdlFolderDialog();
                //  mdlFolderDialog1.BrowseCallbackProc -= new EventHandler(mdlFolderDialog1.BrowseCallbackProc);

                //bi.lpfn = mdlUtility.FnPtrToLong(mdlFolderDialog1.BrowseCallbackProc);
                bi.lParam = 0;

#if false
                FolderItem folderItem = null;
                Shell shell = new Shell();
                Folder folder = shell.BrowseForFolder(0, "Choose Folder", 0, @"C:\Program Files");
#endif

                mdlFolderDialog.FOLDER_DIALOG = this;

                long pidl;
#if false

                pidl = SHBrowseForFolder(ref bi);
#else
                string SelectedPath = "";

                pidl = FolderBrowserDialog_Disp(ref SelectedPath);
#endif

                if (pidl != 0)
                {
                    string sPath = "";
                    sPath = sPath.PadLeft((int)MAX_PATH);

                    long v = SHGetPathFromIDList(pidl, sPath);

                    Path = mdiVBfunctions.Left(sPath, (int)mdiVBfunctions.InStr(0, sPath, null) - 1);
                    CoTaskMemFree(pidl);

                }
                else
                {
                    if (CancelError)
                    {
                        //Then Call Err.Raise(cdlCancel)
                    }
                }

                mdlFolderDialog.FOLDER_DIALOG = null;

            }
            catch (Exception ex)
            {
                //ErrorHandler:
                mdlFolderDialog.FOLDER_DIALOG = null;
                //  Call Err.Raise(Err.Number, Err.Source, Err.Description, Err.HelpFile, Err.HelpContext)

            }

#endif

        }
        //==========================================================================================


    }
}
