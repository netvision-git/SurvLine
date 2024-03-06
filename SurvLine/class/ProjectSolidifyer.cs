using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using System.Windows.Forms;
using System.IO;
using static SurvLine.mdl.DEFINE;
using System.Runtime.InteropServices.ComTypes;
using static System.Windows.Forms.AxHost;
using SurvLine.mdl;
using static SurvLine.mdl.DEFINE;
using static SurvLine.mdl.MdlNSDefine;
using static SurvLine.mdl.MdlUtility;
using static SurvLine.mdl.MdlAccountMake;
using static SurvLine.mdl.MdlNSGUI;
using static SurvLine.mdl.MdlNSSDefine;


namespace SurvLine
{
    internal class ProjectSolidifyer
    {
        private readonly MdlUtility mdlUtility = new MdlUtility();

        public ProjectSolidifyer()
        {

        }

        //==========================================================================================
        /*[VB]
            '*******************************************************************************
            'プロジェクトファイルの梱包。

            Option Explicit

            '定数
            Private Const EXPORT_FILE_PREFIX As String = "NSSNVZ" 'エクスポートファイル識別文字。
            Private Const EXPORT_VERSION As Long = 100 'エクスポートファイルバージョン番号。

            'エクスポートファイル制御コード。
            Private Enum EXPORT_CODE
                EXPORT_CODE_OBS = 0 'ObsPoint フォルダのファイル。
                EXPORT_CODE_EOF 'ファイル終端。
            End Enum
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]

        //'定数
        private const string EXPORT_FILE_PREFIX = "NSSNVZ";     //'エクスポートファイル識別文字。
        private const long EXPORT_VERSION = 100;                //'エクスポートファイルバージョン番号。

        //'エクスポートファイル制御コード。
        private enum EXPORT_CODE
        {
            EXPORT_CODE_OBS = 0,    // 'ObsPoint フォルダのファイル。
            EXPORT_CODE_EOF,        // 'ファイル終端。
        }


        //'*******************************************************************************
        //'メソッド


        //==========================================================================================
        /*[VB]
            'Solidifyer で固められたファイルか？
            '
            '引き数：
            'sPath エクスポートファイルのパス。
            '
            '戻り値：
            'Solidifyer で固められたファイルであれば True を返す。
            'それ以外の場合は False を返す。
            Public Function IsSolid(ByVal sPath As String) As Boolean

                IsSolid = False
                    On Error GoTo ErrorHandler

                Dim clsFile As New FileNumber
                Open sPath For Binary Access Read Lock Write As #clsFile.Number
    
                'エクスポートファイル識別文字。
                Dim sPrefix As String
                sPrefix = GetString(clsFile.Number)


                Call clsFile.CloseFile


                If sPrefix <> EXPORT_FILE_PREFIX Then Exit Function


                IsSolid = True


                Exit Function


            ErrorHandler:


            End Function
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        public bool IsSolid(string sPath)
        {
            bool IsSolid = false;

            try
            {

                using (var fs = System.IO.File.OpenRead($"{sPath}"))
                {
                    using (var br = new BinaryReader(fs))
                    {
                        //'エクスポートファイル識別文字。
#if false
                        string sPrefix;
                        sPrefix = br.ReadString();
#else
                        string sPrefix;
                        byte[] data = new byte[6];
                        data = br.ReadBytes(6);
                        sPrefix = System.Text.Encoding.GetEncoding("shift_jis").GetString(data);
#endif

                        fs.Close();

                        if (sPrefix != EXPORT_FILE_PREFIX)
                        {
                            return IsSolid;
                        }

                        IsSolid = true;
                        return IsSolid;

                    }
                }
            }
            catch (Exception e)
            {
                return IsSolid;

            }


            return IsSolid;
        }


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

                'プロジェクトフォルダの作成。適当な名前。
                Dim sImportName As String
                sImportName = "0000"
                Dim sImportPath As String
                sImportPath = sDstPath & sImportName & "\"
                Call CreateDir(sImportPath)
                Call EmptyDir(sImportPath)
    
                'エクスポートファイル。
                Dim clsFile As New FileNumber
                Open sSrcPath For Binary Access Read Lock Write As #clsFile.Number
    
                'エクスポートファイル識別文字。
                Dim sPrefix As String
                sPrefix = GetString(clsFile.Number)
                If sPrefix<> EXPORT_FILE_PREFIX Then Call Err.Raise(ERR_FILE, , "指定されたファイルは有効なファイルではありません。")
                'エクスポートファイルバージョン番号。
                Dim nVersion As Long
                Get #clsFile.Number, , nVersion
                If nVersion< 100 Or EXPORT_VERSION<nVersion Then Call Err.Raise(ERR_FILE, , "指定されたファイルはバージョンが新しいため読み込めません。")
    
                'タイムスタンプ。
                Dim tModTime As Date
                Dim tCreateTime As Date
                Get #clsFile.Number, , tModTime
                Get #clsFile.Number, , tCreateTime
                'data ファイルの展開。
                Call ExtractExportFile(clsFile.Number, sImportPath & DATA_FILE_NAME, clsProgressInterface)
                'タイムスタンプの設定。
                Dim clsFileObj As New FileObject
                If Not clsFileObj.CreateFile(sImportPath & DATA_FILE_NAME, GENERIC_WRITE, FILE_SHARE_READ, 0, OPEN_EXISTING, FILE_ATTRIBUTE_NORMAL, 0) Then Call Err.Raise(ERR_FILE, , sImportPath & DATA_FILE_NAME & vbCrLf & "にアクセスできません。")
                Dim tCreationTime As Date
                Dim tLastAccessTime As Date
                Dim tLastWriteTime As Date
                If Not clsFileObj.GetFileTime(tCreationTime, tLastAccessTime, tLastWriteTime) Then Call Err.Raise(ERR_FILE, , sImportPath & DATA_FILE_NAME & vbCrLf & "にアクセスできません。")
                tCreationTime = tCreateTime
                tLastWriteTime = tModTime
                If Not clsFileObj.SetFileTime(tCreationTime, tLastAccessTime, tLastWriteTime) Then Call Err.Raise(ERR_FILE, , sImportPath & DATA_FILE_NAME & vbCrLf & "にアクセスできません。")
                Call clsFileObj.CloseHandle
    
                'ObsPoint フォルダ。
                Call CreateDir(sImportPath & "." & OBSPOINT_PATH)
                Call EmptyDir(sImportPath & "." & OBSPOINT_PATH)
                Do
                    '制御コード。
                    Dim nCode As EXPORT_CODE
                    Get #clsFile.Number, , nCode
                    If nCode<> EXPORT_CODE_OBS Then Exit Do
                    '観測点ファイルの読み込み。
                    Dim sName As String
                    sName = GetString(clsFile.Number)
                    Call ExtractExportFile(clsFile.Number, sImportPath & "." & OBSPOINT_PATH & sName, clsProgressInterface)
                    'プログレス。
                    Call clsProgressInterface.CheckCancel
                Loop

                Call clsFile.CloseFile

                ImportProjectFolder = sImportName


            End Function
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        public string ImportProjectFolder(string sSrcPath, string sDstPath, ProgressInterface clsProgressInterface)
        {
            string ImportProjectFolder = "";

            //'プロジェクトフォルダの作成。適当な名前。
            string sImportName = "0000";
            string sImportPath;

            try
            {
                sImportPath = $"{sDstPath}{sImportName}\\";
                _ = mdlUtility.CreateDir(sImportPath);
                _ = EmptyDir(sImportPath);

                //'エクスポートファイル。
                using (var fs = System.IO.File.OpenRead($"{sSrcPath}"))
                {
                    using (var br = new BinaryReader(fs))
                    {
                        //'エクスポートファイル識別文字。
#if false
                        string sPrefix;
                        sPrefix = br.ReadString();
#else
                        string sPrefix;
                        byte[] data = new byte[6];
                        data = br.ReadBytes(6);
                        sPrefix = System.Text.Encoding.GetEncoding("shift_jis").GetString(data);
#endif

                        fs.Close();

                        if (sPrefix != EXPORT_FILE_PREFIX)
                        {
                            //  Then Call Err.Raise(ERR_FILE, , "指定されたファイルは有効なファイルではありません。")
                            MessageBox.Show("指定されたファイルは有効なファイルではありません。","エラー発生");

                        }


                        //'エクスポートファイルバージョン番号。
                        long nVersion;
                        nVersion = br.ReadInt32();
                        if ((int)nVersion < 100 || EXPORT_VERSION < (int)nVersion) 
                        {
                            //  Then Call Err.Raise(ERR_FILE, , "指定されたファイルはバージョンが新しいため読み込めません。")
                            MessageBox.Show("指定されたファイルはバージョンが新しいため読み込めません。", "エラー発生");
                        };

                        //'タイムスタンプ。
                        DateTime tModTime;
                        DateTime tCreateTime;
                        data = br.ReadBytes(6);

                        data = br.ReadBytes(6);

#if false

                        'data ファイルの展開。
                        Call ExtractExportFile(clsFile.Number, sImportPath &DATA_FILE_NAME, clsProgressInterface)
                        'タイムスタンプの設定。
#endif
                        ImportProjectFolder = sImportName;

                    }
                }
            }
            catch (Exception e)
            {
                return sImportName;

            }


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
            Public Sub ExportProjectFolder(ByVal sFolderName As String, ByVal sPath As String, ByVal clsProgressInterface As ProgressInterface)

                'テンポラリファイル。
                Dim sTemp As String
                sTemp = App.Path & TEMPORARY_PATH & EXPORT_TEMP_NAME
                Call RemoveFile(sTemp)
    
                'テンポラリファイルに書き込み。
                Dim clsFile As New FileNumber
                Open sTemp For Binary Access Write Lock Write As #clsFile.Number
    
                'エクスポートファイル識別文字。
                Call PutString(clsFile.Number, EXPORT_FILE_PREFIX)
                'エクスポートファイルバージョン番号。
                Put #clsFile.Number, , EXPORT_VERSION
    
                'タイムスタンプ。
                Dim clsFind As FileFind
                Set clsFind = New FileFind
                If Not clsFind.FindFile(frmMain.UserDataPath & sFolderName & "\" & DATA_FILE_NAME) Then Call Err.Raise(ERR_FILE, , DATA_FILE_NAME & " ファイルが見つかりません。")
            '    If Not clsFind.FindFile(App.Path & DATA_FOLDER_NAME & sFolderName & "\" & DATA_FILE_NAME) Then Call Err.Raise(ERR_FILE, , DATA_FILE_NAME & " ファイルが見つかりません。")
                Put #clsFile.Number, , clsFind.LastWriteTime
                Put #clsFile.Number, , clsFind.CreationTime
                'data ファイルの書き込み。
                Call WriteExportFile(clsFile.Number, frmMain.UserDataPath & sFolderName & "\" & DATA_FILE_NAME, clsProgressInterface)
            '    Call WriteExportFile(clsFile.Number, App.Path & DATA_FOLDER_NAME & sFolderName & "\" & DATA_FILE_NAME, clsProgressInterface)
    
                'ObsPoint フォルダ。
                Dim bFind As Boolean
                bFind = clsFind.FindFile(frmMain.UserDataPath & sFolderName & OBSPOINT_PATH & "*.*")
            '    bFind = clsFind.FindFile(App.Path & DATA_FOLDER_NAME & sFolderName & OBSPOINT_PATH & "*.*")
                Do While bFind
                    If (clsFind.Attributes And FILE_ATTRIBUTE_DIRECTORY) = 0 Then
                        '観測点ファイルの書き込み。
                        Put #clsFile.Number, , EXPORT_CODE_OBS
                        Call PutString(clsFile.Number, clsFind.Name)
                        Call WriteExportFile(clsFile.Number, frmMain.UserDataPath & sFolderName & OBSPOINT_PATH & clsFind.Name, clsProgressInterface)
            '            Call WriteExportFile(clsFile.Number, App.Path & DATA_FOLDER_NAME & sFolderName & OBSPOINT_PATH & clsFind.Name, clsProgressInterface)
                    End If
                    bFind = clsFind.FindNext()
                    'プログレス。
                    Call clsProgressInterface.CheckCancel
                Loop
                Put #clsFile.Number, , EXPORT_CODE_EOF
    
                Call clsFile.CloseFile
    
                'テンポラリファイルと置き換える。
                Call ReplaceFile(sTemp, sPath)

            End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]

        //'*******************************************************************************
        //'インプリメンテーション


        //==========================================================================================
        /*[VB]
            'エクスポートファイルからファイルを展開する。
            '
            '引き数：
            'nFile エクスポートファイルのファイル番号。
            'sPath 展開先のファイルのパス。
            'clsProgressInterface ProgressInterface オブジェクト。
            Private Sub ExtractExportFile(ByVal nFile As Integer, ByVal sPath As String, ByVal clsProgressInterface As ProgressInterface)

                'ファイルのサイズ。
                Dim nSize As Long
                Get #nFile, , nSize
    
                Call RemoveFile(sPath)
                Dim clsFile As New FileNumber
                Open sPath For Binary Access Write Lock Write As #clsFile.Number
    
                'コピー。
                Call CopyFileContents(nFile, clsFile.Number, nSize, FILEIOMEMSIZE, clsProgressInterface)


                Call clsFile.CloseFile


            End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]


        //==========================================================================================
        /*[VB]
            'ファイルをエクスポートファイルに書き込む。
            '
            '引き数：
            'nFile ファイル番号。
            'sPath ファイルのパス。
            'clsProgressInterface ProgressInterface オブジェクト。
            Private Sub WriteExportFile(ByVal nFile As Integer, ByVal sPath As String, ByVal clsProgressInterface As ProgressInterface)

                'ファイルのサイズ。
                Dim nSize As Long
                nSize = FileLen(sPath)
                Put #nFile, , nSize
    
                Dim clsFile As New FileNumber
                Open sPath For Binary Access Read Lock Write As #clsFile.Number
    
                'コピー。
                Call CopyFileContents(clsFile.Number, nFile, nSize, FILEIOMEMSIZE, clsProgressInterface)


                Call clsFile.CloseFile


            End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]







    }
}
