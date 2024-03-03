using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using System.Windows.Forms;
using SurvLine.mdl;
using System.IO;
using System.Runtime.InteropServices;
using static SurvLine.ProjectCompressor;
using Microsoft.VisualBasic;
using System.Runtime.ExceptionServices;










namespace SurvLine
{
    public delegate long NSCABP(long nCurSize);


    internal class ProjectCompressor
    {

        MdlUtility mdlUtility = new MdlUtility();

        MdlNSCAB mdlNSCAB = new MdlNSCAB();


        //'*******************************************************************************
        //'プロジェクトファイルの圧縮。

        //Option Explicit

        //==========================================================================================
        /*[VB]
            Private Const TIME_FILE_NAME As String = "time" 'タイムスタンプ保存ファイル名。
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        private const string TIME_FILE_NAME = "time";   //'タイムスタンプ保存ファイル名。


        //'*******************************************************************************
        //'メソッド


        //==========================================================================================
        /*[VB]
            'エクスポート(nvz)ファイルをインポートするする。
            '
            'sSrcPath で指定されるエクスポートファイルをインポートする。
            '
            '引き数：
            'sSrcPath エクスポートファイルのパス。
            'sDstPath インポート先フォルダのパス。
            'clsProgressInterface ProgressInterface オブジェクト。
            '
            '戻り値：インポートしたプロジェクトフォルダの名前。
            Public Function ImportProjectFolder(ByVal sSrcPath As String, ByVal sDstPath As String, ByVal clsProgressInterface As ProgressInterface) As String

                On Error GoTo ErrorHandler


                Set mdlNSCAB.ProgressNSCAB = clsProgressInterface
    
                'NSCAB準備。
                Dim hNSFD As Long
                hNSFD = NSFDCreate(AddressOf NSCABProgress)
                If hNSFD = 0 Then Call Err.Raise(ERR_FILE, , "CAB展開の準備ができません。")
    
                '展開。
                If NSFDCopy(hNSFD, sSrcPath, sDstPath) = 0 Then
                    Call clsProgressInterface.CheckCancel
                    Call Err.Raise(ERR_FILE, , "CAB展開できません。")
                End If
    
                'NSCAB終了。
                Call NSFDDestroy(hNSFD)
                hNSFD = 0
    
                '展開されたファイルの評価。
                Dim sImportName As String
                Dim sTimeName As String
                Dim clsFind As New FileFind
                Dim bFind As Boolean
                bFind = clsFind.FindFile(sDstPath & "*.*")
                Do While bFind
                    If(clsFind.Attributes And FILE_ATTRIBUTE_DIRECTORY) = 0 Then
                        If sTimeName<> "" Then Call Err.Raise(ERR_FILE, , "指定されたファイルは有効なファイルではありません。")
                        sTimeName = clsFind.Name
                    ElseIf Not IsDots(sDstPath & clsFind.Name) Then
                        If sImportName<> "" Then Call Err.Raise(ERR_FILE, , "指定されたファイルは有効なファイルではありません。")
                        sImportName = clsFind.Name
                    End If
                    bFind = clsFind.FindNext()
                Loop
                'data ファイルはあるか？
                Dim sDataPath As String
                sDataPath = sDstPath & sImportName & "\" & DATA_FILE_NAME
                If Not clsFind.FindFile(sDataPath) Then Call Err.Raise(ERR_FILE, , "data ファイルが見つかりません。")
                Call clsFind.FindClose
                'ObsPoint フォルダの存在を保証するためにとりあえず作る。
                Call CreateDir(sDstPath & sImportName & OBSPOINT_PATH, True)
    
                'タイムスタンプ。
                Dim tModTime As Date
                Dim tCreateTime As Date
                Dim clsFile As New FileNumber
                Open sDstPath & sTimeName For Binary Access Read Lock Write As #clsFile.Number
                Get #clsFile.Number, , tModTime
                Get #clsFile.Number, , tCreateTime
                Call clsFile.CloseFile
                'タイムスタンプの設定。
                Dim clsFileObj As New FileObject
                If Not clsFileObj.CreateFile(sDataPath, GENERIC_WRITE, FILE_SHARE_READ, 0, OPEN_EXISTING, FILE_ATTRIBUTE_NORMAL, 0) Then Call Err.Raise(ERR_FILE, , sDataPath & vbCrLf & "にアクセスできません。")
                Dim tCreationTime As Date
                Dim tLastAccessTime As Date
                Dim tLastWriteTime As Date
                If Not clsFileObj.GetFileTime(tCreationTime, tLastAccessTime, tLastWriteTime) Then Call Err.Raise(ERR_FILE, , sDataPath & vbCrLf & "にアクセスできません。")
                tCreationTime = tCreateTime
                tLastWriteTime = tModTime
                If Not clsFileObj.SetFileTime(tCreationTime, tLastAccessTime, tLastWriteTime) Then Call Err.Raise(ERR_FILE, , sDataPath & vbCrLf & "にアクセスできません。")
                Call clsFileObj.CloseHandle
                '削除。
                Call RemoveFile(sDstPath & sTimeName)


                ImportProjectFolder = sImportName

                Exit Function

            ErrorHandler:
                Call NSFDDestroy(hNSFD)
                Call Err.Raise(Err.Number, Err.Source, Err.Description, Err.HelpFile, Err.HelpContext)


            End Function
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /// <summary>
        /// 'エクスポート(nvz)ファイルをインポートするする。
        /// '
        /// 'sSrcPath で指定されるエクスポートファイルをインポートする。
        /// '
        /// '引き数：
        /// 'sSrcPath エクスポートファイルのパス。
        /// 'sDstPath インポート先フォルダのパス。
        /// 'clsProgressInterface ProgressInterface オブジェクト。
        /// '
        /// 
        /// </summary>
        /// <param name="sSrcPath"></param>
        /// <param name="sDstPath"></param>
        /// <param name="clsProgressInterface"></param>
        /// <returns>
        /// '戻り値：インポートしたプロジェクトフォルダの名前。
        /// </returns>
        /// 
        //public IntPtr GetFunctionPointer(){ }
        public string ImportProjectFolder(string sSrcPath, string sDstPath, ProgressInterface clsProgressInterface)
        {

            string ImportProjectFolder = "";


            //'NSCAB準備。
            long hNSFD = 0;

#if true
            //上位に定義済み   public delegate long NSCABP(long nCurSize);

            NSCABP myNSCABProgress = new NSCABP(mdlNSCAB.NSCABProgress);

//           hNSFD = MdlNSCAB.NSFDCreate(myNSCABProgress);

            //            string myStr="my strings"; 
            //            string ssb=Microsoft.VisualBasic.Strings.StrConv(myStr，Microsoft.VisualBasic.VbStrConv.Wide，0); 

            //            NSCABP myNSCABProgress = new NSCABP(mdlNSCAB.NSCABProgress);

            //            hNSFD = MdlNSCAB.NSFDCreate(myNSCABProgress);

#else

            hNSFD = MdlNSCAB.NSFDCreate(AddressOf mdlNSCAB.NSCABProgress);

//            hNSFD = MdlNSCAB.NSFDCreate(AddressOf mdlNSCAB.NSCABProgress);
#endif



            if (hNSFD == 0)
            {
                //If hNSFD = 0 Then Call Err.Raise(ERR_FILE, , "CAB展開の準備ができません。")
                MessageBox.Show("CAB展開の準備ができません。", "エラー発生");

            }

            //'展開。
            long sts = MdlNSCAB.NSFDCopy(hNSFD, sSrcPath, sDstPath);
            if (sts == 0)
            {
                clsProgressInterface.CheckCancel();
                //Call Err.Raise(ERR_FILE, , "CAB展開できません。")
                MessageBox.Show("CAB展開できません。", "エラー発生");

            }


            //'NSCAB終了。
            MdlNSCAB.NSFDDestroy(hNSFD);
            hNSFD = 0;


            //'展開されたファイルの評価。
            string sImportName = "";
            string sTimeName = "";
            int i = 0;
            //------------------------------------------
            var files = System.IO.Directory.GetDirectories(sDstPath);       //"C:\Develop\NetSurv\Src\NS-App\NS-Survey\Temp\NSCAB\"
            foreach (var file in files)     ////パス内の全（フォルダ＋ファイル名＆読み込み）
            {
                FileAttributes attr = System.IO.File.GetAttributes(sDstPath);
                if (((long)attr & DEFINE.FILE_ATTRIBUTE_DIRECTORY) == 0)
                {
                    sTimeName = files[i];
                    if (sTimeName != "")
                    {
                        //  If sTimeName<> "" Then Call Err.Raise(ERR_FILE, , "指定されたファイルは有効なファイルではありません。")
                        _ = MessageBox.Show("指定されたファイルは有効なファイルではありません。", "エラー発生");
                        return ImportProjectFolder;
                    }
                }
                else
                {
                    //------------------------------------------
                    //フォルダ名を設定   
                    //------------------------------------------
                    sImportName = files[i];
                    if (mdlUtility.IsDots(sImportName))
                    {
                        if (sImportName != "")
                        {
                            //If sImportName<> "" Then Call Err.Raise(ERR_FILE, , "指定されたファイルは有効なファイルではありません。")
                            _ = MessageBox.Show("指定されたファイルは有効なファイルではありません。", "エラー発生");
                            return ImportProjectFolder;
                        }
                    }

                }

                i++;
            }


            //'data ファイルはあるか？
            string sDataPath;
            sDataPath = $"{sDstPath}{sImportName}\\{GENBA_CONST.DATA_FILE_NAME}";   //Ex.)"C:\Develop\NetSurv\Src\NS-App\NS-Survey\Temp\NSCAB\0006\data"
            if (File.Exists(sDataPath))
            {
                //If Not clsFind.FindFile(sDataPath) Then Call Err.Raise(ERR_FILE, , "data ファイルが見つかりません。")
                _ = MessageBox.Show("data ファイルが見つかりません。", "エラー発生");
                return ImportProjectFolder;

            }
            //'ObsPoint フォルダの存在を保証するためにとりあえず作る。
            bool d = mdlUtility.CreateDir($"{sDstPath}{sImportName}{MdlNSSDefine.OBSPOINT_PATH}", false);    //"C:\Develop\NetSurv\Src\NS-App\NS-Survey\Temp\NSCAB\0006\data

            //'タイムスタンプ。
            DateTime tModTime;
            DateTime tCreateTime;
            tModTime = System.IO.File.GetLastWriteTime($"{sDstPath}{sTimeName}");
            tCreateTime = System.IO.File.GetCreationTime($"{sDstPath}{sTimeName}");

#if false
            //'タイムスタンプの設定。
            Dim clsFileObj As New FileObject
            If Not clsFileObj.CreateFile(sDataPath, GENERIC_WRITE, FILE_SHARE_READ, 0, OPEN_EXISTING, FILE_ATTRIBUTE_NORMAL, 0) Then Call Err.Raise(ERR_FILE, , sDataPath & vbCrLf & "にアクセスできません。")
            Dim tCreationTime As Date
            Dim tLastAccessTime As Date
            Dim tLastWriteTime As Date
            If Not clsFileObj.GetFileTime(tCreationTime, tLastAccessTime, tLastWriteTime) Then Call Err.Raise(ERR_FILE, , sDataPath & vbCrLf & "にアクセスできません。")
            tCreationTime = tCreateTime
            tLastWriteTime = tModTime
            If Not clsFileObj.SetFileTime(tCreationTime, tLastAccessTime, tLastWriteTime) Then Call Err.Raise(ERR_FILE, , sDataPath & vbCrLf & "にアクセスできません。")
            Call clsFileObj.CloseHandle
#endif




            ImportProjectFolder = sImportName;

            return ImportProjectFolder;
        }


        //==========================================================================================
        /*[VB]
            'プロジェクトフォルダをエクスポートする。
            '
            'sFolderName で指定されるフォルダを sPath で指定されるファイルにエクスポートする。
            '
            '引き数：
            'sFolderName フォルダ名。
            'sPath 出力先ファイルのパス。
            'clsProgressInterface ProgressInterface オブジェクト。
            '
            '戻り値：
            '正常終了の場合 True を返す。
            'それ以外の場合 False を返す。
            Public Function ExportProjectFolder(ByVal sFolderName As String, ByVal sPath As String, ByVal clsProgressInterface As ProgressInterface) As Boolean

                ExportProjectFolder = False

                On Error GoTo ErrorHandler

                Set mdlNSCAB.ProgressNSCAB = clsProgressInterface
    
                'タイムスタンプ。
                Dim clsFind As FileFind
                Set clsFind = New FileFind
                If Not clsFind.FindFile(frmMain.UserDataPath & sFolderName & "\" & DATA_FILE_NAME) Then Call Err.Raise(ERR_FILE, , DATA_FILE_NAME & " ファイルが見つかりません。")
            '    If Not clsFind.FindFile(App.Path & DATA_FOLDER_NAME & sFolderName & "\" & DATA_FILE_NAME) Then Call Err.Raise(ERR_FILE, , DATA_FILE_NAME & " ファイルが見つかりません。")
                'タイムスタンプ保存ファイル。
                Dim sTimePath As String
                sTimePath = App.Path & TEMPORARY_PATH & TIME_FILE_NAME
                Call RemoveFile(sTimePath)
                Dim clsFile As New FileNumber
                Open sTimePath For Binary Access Write Lock Write As #clsFile.Number
                Put #clsFile.Number, , clsFind.LastWriteTime
                Put #clsFile.Number, , clsFind.CreationTime
                Call clsFile.CloseFile
                Call clsFind.FindClose
    
                'テンポラリファイル。
                Dim sTemp As String
                sTemp = App.Path & TEMPORARY_PATH & EXPORT_TEMP_NAME
                Call RemoveFile(sTemp)
    
                'NSCAB準備。
                Dim hNSFC As Long
                hNSFC = NSFCCreate(sTemp, AddressOf NSCABProgress)
                If hNSFC = 0 Then Call Err.Raise(ERR_FILE, , "CAB圧縮の準備ができません。")
    
                '圧縮。
            '    If NSFCAddFolder(hNSFC, App.Path & DATA_FOLDER_NAME & sFolderName) = 0 Then
                If NSFCAddFolder(hNSFC, frmMain.UserDataPath & sFolderName) = 0 Then
                    Call clsProgressInterface.CheckCancel
                    Call Err.Raise(ERR_FILE, , "CAB圧縮できません。")
                End If
                If NSFCAddFile(hNSFC, sTimePath) = 0 Then
                    Call clsProgressInterface.CheckCancel
                    Call Err.Raise(ERR_FILE, , "CAB圧縮できません。")
                End If
    
                'NSCAB終了。
                Call NSFCDestroy(hNSFC)
                hNSFC = 0
    
                'テンポラリファイルと置き換える。
                Call ReplaceFile(sTemp, sPath)
                Call RemoveFile(sTimePath)


                ExportProjectFolder = True

                Exit Function

            ErrorHandler:
                Call NSFCDestroy(hNSFC)
                If Err.Number = cdlCancel Then Call Err.Raise(Err.Number, Err.Source, Err.Description, Err.HelpFile, Err.HelpContext)

            End Function
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]


    }
}
