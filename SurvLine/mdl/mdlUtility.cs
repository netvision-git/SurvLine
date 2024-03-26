using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Remoting;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;
using static System.Net.WebRequestMethods;
using static System.Windows.Forms.AxHost;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static SurvLine.mdl.DEFINE;
using static SurvLine.mdl.MdlNSDefine;
using static SurvLine.mdl.MdlAccountMake;
using static SurvLine.mdl.MdlNSGUI;
using static SurvLine.mdl.MdlNSSDefine;
using static SurvLine.mdl.MdiVBfunctions;
using System.Collections;
using System.Drawing.Drawing2D;





namespace SurvLine.mdl
{

    public class MdlUtility
    {


        //==========================================================================================
        /*[VB]
        '*******************************************************************************
        'ユーティリティ

        Option Explicit
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        Public Declare Function CeGetLastError Lib "rapi.dll" () As Long '2016/02/18 Add
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        [DllImport("rapi.dll")]
        public static extern long CeGetLastError();
        //==========================================================================================


        //==========================================================================================
        //[C#]
        public static string Mid(string str, int start, int len)
        {
            if (start <= 0)
            {
                //throw new ArgumentException("引数'start'は1以上でなければなりません。");
                return "";
            }
            if (len < 0)
            {
                //throw new ArgumentException("引数'len'は0以上でなければなりません。");
                return "";
            }
            return str == null || str.Length < start ? "" : str.Length < (start + len) ? str.Substring(start - 1) : str.Substring(start - 1, len);
        }

        public static string Right(string str, int len)
        {
            if (len < 0)
            {
                //throw new ArgumentException("引数'len'は0以上でなければなりません。");
                return "";
            }
            return str == null ? "" : str.Length <= len ? str : str.Substring(str.Length - len, len);
        }

        public static string Left(string str, int len)
        {
            if (len < 0)
            {
                //throw new ArgumentException("引数'len'は0以上でなければなりません。");
                return "";
            }
            return str == null ? "" : str.Length <= len ? str : str.Substring(0, len);
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'ディレクトリを作成する。
        '
        '引き数：
        'sPath 作成するディレクトリのパス。
        'bRaise Err.Raise の有無。
        '
        '戻り値：
        '正常終了の場合 True を返す。
        'それ以外の場合 False を返す。
        Public Function CreateDir(ByVal sPath As String, Optional ByVal bRaise As Boolean = False) As Boolean
            CreateDir = False
            On Error GoTo FileErrorHandler
            sPath = RTrimEx(sPath, "\")
            If Right$(sPath, 1) = ":" Then Exit Function
            Dim clsFind As New FileFind
            If Not clsFind.FindFile(sPath) Then
                Dim sDrive As String
                Dim sDir As String
                Dim sTitle As String
                Dim sExt As String
                Call SplitPath(sPath, sDrive, sDir, sTitle, sExt)
                If Not CreateDir(sDrive & sDir, bRaise) Then Exit Function
                Call MkDir(sPath)
            End If
            CreateDir = True
            Exit Function
        FileErrorHandler:
            If bRaise Then Call Err.Raise(Err.Number, Err.Source, Err.Description, Err.HelpFile, Err.HelpContext)
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //修正要 saka -> K.Setoguchi
        //[C#]
        /// <summary>
        /// ディレクトリを作成する。
        ///
        /// 引き数：
        ///     sPath 作成するディレクトリのパス。
        ///     bRaise Err.Raise の有無。
        /// </summary>
        /// <param name="sPath"></param>
        /// <param name="bRaise"></param>
        /// <returns>
        /// 戻り値：
        ///     正常終了の場合 True を返す。
        ///     それ以外の場合 False を返す。
        /// </returns>
        public bool CreateDir(string sPath, bool bRaise = false)
        {
            bool CreateDir = false;
            MdiVBfunctions mdiVBfunctions = new MdiVBfunctions();

            try
            {
                sPath = RTrimEx(sPath, "\\");
                if (mdiVBfunctions.Right(sPath, 1) == ":")
                {
                    return CreateDir;
                }


                if (!Directory.Exists(sPath))
                {
                    //存在しない場合は、フォルダを作成
                    //      ※　VC#では、複数のフォルダを一気に作成できる
                    _ = Directory.CreateDirectory(sPath);

                    CreateDir = true;
                    bRaise = true;
                }
                else
                {
                    CreateDir = true;
                    CreateDir = true;
                }

            }
            catch (Exception)
            {
                CreateDir = false;
                bRaise = false;
            }

            return CreateDir;

        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'ファイルをコピーする。
        '
        '引き数：
        'sSrcPath コピー元ファイルのパス。
        'sDstPath コピー先ファイルのパス。
        'bRaise Err.Raise の有無。
        '
        '戻り値：
        '正常終了の場合 True を返す。
        'それ以外の場合 False を返す。
        Public Function CopyFile(ByVal sSrcPath As String, ByVal sDstPath As String, Optional ByVal bRaise As Boolean = False) As Boolean
            CopyFile = False
            On Error GoTo FileErrorHandler
            Dim fs
            Set fs = CreateObject("Scripting.FileSystemObject")
            Call fs.CopyFile(sSrcPath, sDstPath, True)
            CopyFile = True
            Exit Function
        FileErrorHandler:
            If bRaise Then Call Err.Raise(Err.Number, Err.Source, Err.Description, Err.HelpFile, Err.HelpContext)
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /// <summary>
        /// ファイルを削除する。
        ///
        /// 引き数：
        ///     sPath 削除するファイルのパス。
        ///     bRaise Err.Raise の有無。
        /// 
        /// </summary>
        /// <param name="sPath"></param>
        /// <param name="bRaise"></param>
        /// <returns>
        /// </returns>
        public bool RemoveFile(string sPath, bool bRaise = false)
        {
            bool RemoveFile = false;

            try
            {
                if (System.IO.File.Exists(sPath))
                {
                    System.IO.File.Delete(sPath);
                    RemoveFile = true;
                }

            }
            catch (Exception)
            {
                if (bRaise)
                {
                    //flase の為、メッセージは、不要
                }

            }

            return RemoveFile;
        }
        /*
        'ファイルをコピーする。
        '
        '引き数：
        'sSrcPath コピー元ファイルのパス。
        'sDstPath コピー先ファイルのパス。
        'bRaise Err.Raise の有無。
        '
        '戻り値：
        '正常終了の場合 True を返す。
        'それ以外の場合 False を返す。
        */
        public static bool CopyFile(string sSrcPath, string sDstPath, bool bRaise = false)
        {
            try
            {
#if false
                int fs;
                fs = CreateObject("Scripting.FileSystemObject");
                fs.CopyFile(sSrcPath, sDstPath, true);
#endif
                return true;
            }

            catch (Exception)
            {
                if (bRaise)
                {
                    //Err.Raise(Err.Number, Err.Source, Err.Description, Err.HelpFile, Err.HelpContext);
                }
                return false;
            }

        }
        //==========================================================================================


        //==========================================================================================
        /*[VB]
        'ディレクトリを空にする。
        '
        '引き数：
        'sPath 空にするディレクトリのパス。
        'bRaise Err.Raise の有無。
        '
        '戻り値：
        '正常終了の場合 True を返す。
        'それ以外の場合 False を返す。
        Public Function EmptyDir(ByVal sPath As String, Optional ByVal bRaise As Boolean = False) As Boolean
            EmptyDir = False
            On Error GoTo FileErrorHandler
            Dim clsFind As New FileFind
            If clsFind.FindFile(RTrimEx(sPath, "\")) Then
                If Mid$(sPath, Len(sPath), 1) <> "\" Then sPath = sPath & "\"
                Dim sFormat As String
                sFormat = sPath & "*"
                Set clsFind = New FileFind
                Dim bFind As Boolean
                bFind = clsFind.FindFile(sFormat)
                Do While bFind
                    Dim sFind As String
                    sFind = sPath & clsFind.Name
                    If(clsFind.Attributes And FILE_ATTRIBUTE_DIRECTORY) = 0 Then
                        Call RemoveFile(sFind, bRaise)
                    Else
                        If Not IsDots(sFind) Then
                            If Not EmptyDir(sFind, bRaise) Then Exit Function
                            Call RmDir(sFind)
                        End If
                    End If
                    bFind = clsFind.FindNext()
                Loop
            End If
            EmptyDir = True
            Exit Function
        FileErrorHandler:
            If bRaise Then Call Err.Raise(Err.Number, Err.Source, Err.Description, Err.HelpFile, Err.HelpContext)
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /// <summary>
        ///'ディレクトリを空にする。
        ///'
        ///'引き数：
        ///'sPath 空にするディレクトリのパス。
        ///'bRaise Err.Raise の有無。
        /// 
        /// </summary>
        /// <param name="sPath"></param>
        /// <param name="bRaise"></param>
        /// <returns>
        ///'戻り値：
        ///'正常終了の場合 True を返す。
        ///'それ以外の場合 False を返す。
        /// </returns>
        public static bool EmptyDir(string sPath, bool bRaise = false)
        {

            bool EmptyDir = false;

            try
            {
                //文字末尾が"\"の場合は、文字から削除する
                sPath = RTrimEx(sPath, "\\");
                //このフォルダの有無
                if (Directory.Exists(sPath))
                {

                    sPath = $"{sPath}\\";

                    string[] picList = Directory.GetFiles(sPath, "*.*");
                    string fName;

                    foreach (string f in picList)
                    {
                        // Remove path from the file name.
                        fName = f.Substring(sPath.Length + 1);

                        if (IsDots(fName) == false)
                        {
                            if (System.IO.File.Exists(Path.Combine(sPath, fName)))
                            {
                                //ファイル有り
                                System.IO.File.Delete(Path.Combine(sPath, fName));
                            }
                        }

                    }
                    EmptyDir = true;

                }
            }
            catch (Exception ex)
            {
                if (bRaise) { _ = MessageBox.Show(ex.Message, "エラー発生", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }

            return EmptyDir;

        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'フォルダーの存在を確認する。
        '
        '引き数：
        'sPath 検査対象のパス。
        '
        '戻り値：
        'フォルダーが存在する場合 True を返す。
        'それ以外の場合 False を返す。
        Public Function IsFolderExists(ByVal sPath As String) As Boolean
            IsFolderExists = False
            On Error GoTo FileErrorHandler
            Dim fs
            Set fs = CreateObject("Scripting.FileSystemObject")
            IsFolderExists = fs.FolderExists(sPath)
            Exit Function
        FileErrorHandler:
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        'フォルダーの存在を確認する。
        '
        '引き数：
        'sPath 検査対象のパス。
        '
        '戻り値：
        'フォルダーが存在する場合 True を返す。
        'それ以外の場合 False を返す。
        */
        public static bool IsFolderExists(string sPath)
        {
            try
            {
                if (Directory.Exists(sPath))
                {
                    return true;
                }
            }
            catch (Exception)
            {
                //  MessageBox.Show(ex.Message, "エラー発生", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return false;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'ファイルの存在を確認する。
        '
        '引き数：
        'sPath 検査対象のパス。
        '
        '戻り値：
        'ファイルが存在する場合 True を返す。
        'それ以外の場合 False を返す。
        Public Function IsFileExists(ByVal sPath As String) As Boolean
            IsFileExists = False
            On Error GoTo FileErrorHandler
            Dim fs
            Set fs = CreateObject("Scripting.FileSystemObject")
            IsFileExists = fs.FileExists(sPath)
            Exit Function
        FileErrorHandler:
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        'ファイルの存在を確認する。
        '
        '引き数：
        'sPath 検査対象のパス。
        '
        '戻り値：
        'ファイルが存在する場合 True を返す。
        'それ以外の場合 False を返す。
        */
        public static bool IsFileExists(string sPath)
        {
            try
            {
                if (System.IO.File.Exists(sPath))
                {
                    return true;
                }
            }
            catch (Exception)
            {
                //  MessageBox.Show(ex.Message, "エラー発生", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return false;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'HTMLファイルをブラウザで開く。
        '
        '引き数：
        'sPath HTMLファイルのパス。
        Public Sub ExecuteHtml(ByVal sPath As String)
            If ShellExecute(0, "open", sPath, vbNullString, vbNullString, SW_SHOWNORMAL) <= 32 Then Call Err.Raise(vbObjectError Or & H200 & Or & H1 &, , "ブラウザを起動できませんでした。")
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        'HTMLファイルをブラウザで開く。
        '
        '引き数：
        'sPath HTMLファイルのパス。
        */
        public static void ExecuteHtml(string sPath)
        {
            return;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
            '文字列変数をバリアントに変換してファイルに書き込む。
            '
            '引き数：
            'nFile ファイル番号。
            'sBuff ファイルに書き込む文字列。
            Public Sub PutString(ByVal nFile As Integer, ByVal sBuff As String)
                Dim V As Variant
                V = sBuff
                Put #nFile, , V
            End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
#if false
        public static void PutString(int nFile, string sBuff)
        {
            return;
        }
#else
        //***************************************************************************
        /// <summary>
        //'文字列変数をバリアントに変換してファイルに書き込む。
        //'
        //'引き数：
        ///     br バイナリファイル
        ///     sBuff ファイルに書き込む文字列。
        /// </summary>
        /// <param name="br"></param>
        /// <param name="sBuff"></param>
        public static void FileWrite_PutString(BinaryWriter bw, string sBuff)
        {
            ushort size;
            ushort vertype = 8;
            bw.Write(vertype);

            if (sBuff == null)
            {
                size = 0;
                bw.Write(size);
            }
            else
            {
                byte[] shiftJisBytes = Encoding.GetEncoding("Shift-JIS").GetBytes(sBuff);
                size = (ushort)shiftJisBytes.Length;
                bw.Write(size);
                //-------------------------------------------------------------------------
                bw.Write(shiftJisBytes);
                //bw.Write(sBuff);
                //-------------------------------------------------------------------------
            }
        }

#endif



        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '文字列の動的配列をファイルに書き込む。
        '
        '引き数：
        'nFile ファイル番号。
        'sBuff ファイルに書き込む文字列配列。参照する配列の要素は(0 To ...)。Ubound 関数を使用する。
        Public Sub PutArray(ByVal nFile As Integer, ByRef sBuff() As String)
            Dim nUBound As Long
            nUBound = UBound(sBuff) '要素なしは認めない。
            Put #nFile, , nUBound
            Put #nFile, , sBuff
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '文字列の動的配列をファイルに書き込む。
        '
        '引き数：
        'nFile ファイル番号。
        'sBuff ファイルに書き込む文字列配列。参照する配列の要素は(0 To ...)。Ubound 関数を使用する。
        */
        public static void PutArray(int nFile, string[] sBuff)
        {
            return;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'ファイルから文字列の動的配列を読み込む。
        '
        '引き数：
        'nFile ファイル番号。
        'sBuff ファイルから読み込んだ文字列を設定する文字列配列。配列の要素は(0 To ...)。
        Public Sub GetArray(ByVal nFile As Integer, ByRef sBuff() As String)
            Dim nUBound As Long
            Get #nFile, , nUBound
            ReDim sBuff(nUBound)
            Get #nFile, , sBuff
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        'ファイルから文字列の動的配列を読み込む。
        '
        '引き数：
        'nFile ファイル番号。
        'sBuff ファイルから読み込んだ文字列を設定する文字列配列。配列の要素は(0 To ...)。
        */
        public static void GetArray(int nFile, string[] sBuff)
        {
            return;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'ファイルを置き換える。
        '
        'sSrcFile のパスを sDstFile に変更する。
        'sDstFile が存在する場合は削除する。
        '
        '引き数：
        'sSrcFile 対象ファイルのパス。
        'sDstFile 変更先ファイルのパス。
        Public Sub ReplaceFile(ByVal sSrcFile As String, ByVal sDstFile As String)
            On Error Resume Next
            Call RemoveFile(sDstFile)
            On Error GoTo 0
            Name sSrcFile As sDstFile
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /// <summary>
        /// ファイルを置き換える。
        ///
        /// sSrcFile のパスを sDstFile に変更する。
        /// sDstFile が存在する場合は削除する。
        ///     (sSrcFile -- (Move)--> sDstFile )
        ///     
        /// 引き数：
        ///     sSrcFile 対象ファイルのパス。
        ///     sDstFile 変更先ファイルのパス。
        /// 
        /// </summary>
        /// <param name="sSrcFile"></param>
        /// <param name="sDstFile"></param>
        public void ReplaceFile(string sSrcFile, string sDstFile)
        {
            bool dmy = RemoveFile(sDstFile, false);

            System.IO.File.Move(sSrcFile, sDstFile);

        }
        //==========================================================================================

        //***************************************************************************
        /// <summary>
        /// bool型データ読み出し VB対応
        /// </summary>
        /// <param name="br"></param>
        /// <returns></returns>
        public static bool GetFileBool(BinaryReader br)
        {
            bool bBool = false;

            ushort uWork;
            uWork = br.ReadUInt16();
            if (uWork == 0xffff)
            {
                bBool = true;
            }
            else
            {
                bBool = false;
            }
            return bBool;
        }


        //***************************************************************************
        /// <summary>
        /// bool型データ書き込み VB対応
        /// bw : バイナリファイル
        /// data：書き込む　boolデータ
        /// </summary>
        /// <param name="bw"></param>
        /// <param name="data"></param>
        public static void PutFileBool(BinaryWriter bw, bool data)
        {
            ushort uWork = 0;

            if (data)
            {
                uWork = 0xffff;
            }
            bw.Write(uWork);

            return; ;
        }


        //==========================================================================================
        /*[VB]
        '文字列の先頭から、任意の文字を省く。
        '
        '引き数：
        'sValue 文字列。
        'sTarget 省く文字。
        '
        '戻り値：結果文字列。
        Public Function LTrimEx(ByVal sValue As String, ByVal sTarget As String) As String
            Dim nValue() As Byte
            Dim nTarget() As Byte
            Dim i As Long
            nValue = sValue
            nTarget = Left$(sTarget, 1)
            For i = 0 To UBound(nValue) Step 2
                If nValue(i) <> nTarget(0) Or nValue(i + 1) <> nTarget(1) Then Exit For
            Next
            LTrimEx = MidB$(sValue, i + 1)
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]  //2
        /// <summary>
        /// 文字列の先頭から、任意の文字を省く。
        /// '
        /// 引き数：
        /// sValue 文字列。
        /// sTarget 省く文字。
        /// 
        /// </summary>
        /// <param name="sValue"></param>
        /// <param name="sTarget"></param>
        /// <returns>
        /// 戻り値：結果文字列。
        /// </returns>
        public static string LTrimEx(string sValue, string sTarget)
        {
            string LTrimEx;

            int i = 0;
            int set = 0;

            //ASCII エンコード
            //  byte[] nValue = new byte[sValue.Length];
            //  byte[] nTarget = new byte[sTarget.Length];
            byte[] nValue = Encoding.ASCII.GetBytes(sValue);
            byte[] nTarget = Encoding.ASCII.GetBytes(sTarget);

            for (i = 0; i < nValue.Length; i += 2)
            {
                // If nValue(i) <> nTarget(0) Or nValue(i +1) <> nTarget(1) Then Exit For

                if (nValue[i] != nTarget[0] || nValue[i + 1] != nTarget[1])
                {
                    break;
                }

            }

            LTrimEx = Mid(sValue, i + 1, sValue.Length);

            return LTrimEx;
        }

#if false   //2
        /*
        '文字列の先頭から、任意の文字を省く。
        '
        '引き数：
        'sValue 文字列。
        'sTarget 省く文字。
        '
        '戻り値：結果文字列。
        */
        public static string sss LTrimEx(string sValue, string sTarget)     //2 
        {
            int size = sValue.Length;
            if (size > 0)   //データ無し
            {
                return sValue;
            }

            //省く文字の有無
            int sts = 0;
            do
            {
                sts = sValue.IndexOf(sTarget, size - 1);
                if (sts >= 0)
                {
                    sValue = Mid(sValue, sts - 1, 1);
                }
                sts--;

            } while (sts >= 0);

            return sValue;
        }
#endif  //2
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'コレクションから指定されたキーのアイテムを取得する。
        '
        'アイテムは Object。
        '
        '引き数：
        'objCollection コレクション。
        'vItem 取得されたアイテム。
        'sKey キー。
        '
        '戻り値：
        'コレクションにアイテムが存在する場合は True を返す。
        'コレクションにアイテムが無い場合は False を返す。
        Public Function LookupCollectionObject(ByVal objCollection As Collection, ByRef vItem As Object, ByVal sKey As String) As Boolean
            On Error GoTo CollectionHandler
            Set vItem = objCollection.Item(sKey)
            LookupCollectionObject = True
        CollectionHandler:
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        'コレクションから指定されたキーのアイテムを取得する。
        '
        'アイテムは Object。
        '
        '引き数：
        'objCollection コレクション。
        'vItem 取得されたアイテム。
        'sKey キー。
        '
        '戻り値：
        'コレクションにアイテムが存在する場合は True を返す。
        'コレクションにアイテムが無い場合は False を返す。
        */
        public static bool LookupCollectionObject(Dictionary<string, object> objCollection, ref object vItem, string sKey)
        {
            try
            {

#if false
                foreach ( var kvp in objCollection) //4
                {
                    if (sKey == kvp.Key)            //4
                    {
                        vItem = kvp.Value;          //4
                        break;
                    }
                }                                   //4
#endif

                bool rtn = objCollection.TryGetValue(sKey, out vItem);
                return rtn;
            }

            catch
            {
                return false;
            }
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'コレクションから指定されたキーのアイテムを取得する。
        '
        'アイテムは Variant。
        '
        '引き数：
        'objCollection コレクション。
        'vItem 取得されたアイテム。
        'sKey キー。
        '
        '戻り値：
        'コレクションにアイテムが存在する場合は True を返す。
        'コレクションにアイテムが無い場合は False を返す。
        Public Function LookupCollectionVariant(ByVal objCollection As Collection, ByRef vItem As Variant, ByVal sKey As String) As Boolean
            On Error GoTo CollectionHandler
            vItem = objCollection.Item(sKey)
            LookupCollectionVariant = True
        CollectionHandler:
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        public static bool LookupCollectionVariant(Dictionary<string, object> objKeyToIndex, ref string vItem, string sKey)
        {
            try
            {
                object oItem;
                bool rtn = objKeyToIndex.TryGetValue(sKey, out oItem);
                //  vItem = oItem.ToString();          //4


                foreach (var kvp in objKeyToIndex) //4
                {
                    if (sKey == kvp.Key)            //4
                    {
                        vItem = kvp.Value.ToString();          //4
                        break;
                    }
                }                                   //4

                return rtn;


            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// コレクションから指定されたキーのアイテムを取得する。
        /// '
        /// アイテムは Variant。
        /// 
        /// 引き数：
        /// objCollection コレクション。
        /// vItem 取得されたアイテム。
        /// sKey キー。
        /// 
        /// </summary>
        /// <param name="objCollection"></param>
        /// <param name="vItem"></param>
        /// <param name="sKey"></param>
        /// <returns>
        /// '戻り値：
        /// 'コレクションにアイテムが存在する場合は True を返す。
        /// 'コレクションにアイテムが無い場合は False を返す。
        /// </returns>
        /// 
        public static bool LookupCollectionVariant(Dictionary<string, int> objCollection, ref int vItem, string sKey)
        {
            try
            {
                bool rtn = objCollection.TryGetValue(sKey, out vItem);
                return rtn;
            }

            catch
            {
                return false;
            }
        }
        public static bool LookupCollectionVariant_si(Dictionary<string, int> objCollection, ref int vItem, string sKey)
        {
            try
            {
                bool rtn = objCollection.TryGetValue(sKey, out vItem);
                return rtn;
            }

            catch
            {
                return false;
            }
        }
        public static bool LookupCollectionVariant_so(Dictionary<string, object> objCollection, ref object vItem, string sKey)
        {
            try
            {
                bool rtn = objCollection.TryGetValue(sKey, out vItem);
                return rtn;
            }

            catch
            {
                return false;
            }
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'コレクションに指定されたキーのアイテムを設定する。
        '
        'アイテムは Object。
        '
        '引き数：
        'objCollection コレクション。
        'vItem 設定するアイテム。
        'sKey キー。
        Public Sub SetAtCollectionObject(ByVal objCollection As Collection, ByRef vItem As Object, ByVal sKey As String)
            On Error GoTo CollectionHandler
            Call objCollection.Add(vItem, sKey)
        CollectionHandler:
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        'コレクションに指定されたキーのアイテムを設定する。
        '
        'アイテムは Object。
        '
        '引き数：
        'objCollection コレクション。
        'vItem 設定するアイテム。
        'sKey キー。
        */
        public static void SetAtCollectionObject(Dictionary<string, object> objCollection, object vItem, string sKey)
        {
            try
            {
                objCollection.Add(sKey, vItem);
                return;
            }

            catch
            {
                return;
            }
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'コレクションに指定されたキーのアイテムを設定する。
        '
        'アイテムは Variant。
        '
        '引き数：
        'objCollection コレクション。
        'vItem 設定するアイテム。
        'sKey キー。
        Public Sub SetAtCollectionVariant(ByVal objCollection As Collection, ByRef vItem As Variant, ByVal sKey As String)
            On Error GoTo CollectionHandler
            Call objCollection.Add(vItem, sKey)
        CollectionHandler:
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        'コレクションに指定されたキーのアイテムを設定する。
        '
        'アイテムは Variant。
        '
        '引き数：
        'objCollection コレクション。
        'vItem 設定するアイテム。
        'sKey キー。
        */
        public static void SetAtCollectionVariant(Dictionary<string, int> objCollection, ref int vItem, string sKey)
        {
            try
            {
                objCollection.Add(sKey, vItem);
                return;
            }
            catch { return; }
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'コレクションから指定されたキーのアイテムを削除する。
        '
        '引き数：
        'objCollection コレクション。
        'sKey キー。
        Public Sub RemoveAtCollection(ByVal objCollection As Collection, ByVal sKey As String)
            On Error GoTo CollectionHandler
            Call objCollection.Remove(sKey)
        CollectionHandler:
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        'コレクションから指定されたキーのアイテムを削除する。
        '
        '引き数：
        'objCollection コレクション。
        'sKey キー。
        */
        public static void RemoveAtCollection(Dictionary<string, object> objCollection, string sKey)
        {
            try
            {
                _ = objCollection.Remove(sKey);
                return;
            }
            catch { return; }
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'コレクションから全てのアイテムを削除する。
        '
        '引き数：
        'objCollection コレクション。
        Public Sub RemoveAllCollection(ByVal objCollection As Collection)
            Dim nCount As Long
            Dim i As Long
            nCount = objCollection.Count
            For i = 1 To nCount
                Call objCollection.Remove(1)
            Next
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        'コレクションから全てのアイテムを削除する。
        '
        '引き数：
        'objCollection コレクション。
        */
        public static void RemoveAllCollection(Dictionary<string, object> objCollection)
        {
            try
            {
                objCollection.Clear();
                return;
            }
            catch { return; }
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'オブジェクトのポインタを取得する。
        '
        '引き数：
        'obj オブジェクト。
        '
        '戻り値：オブジェクトのポインタ。
        Public Function GetPointer(ByRef obj As Object) As Long
            Dim N As Long
            Call MoveMemory(N, obj, 4)
            GetPointer = N
        '   GetPointer = ObjPtr(obj)
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        'オブジェクトのポインタを取得する。
        '
        '引き数：
        'obj オブジェクト。
        '
        '戻り値：オブジェクトのポインタ。
        */
        public static long GetPointer(object obj)
        {
            //IntPtr N = (IntPtr)0;
            IntPtr N = new IntPtr();
            RtlMoveMemory(ref N, ref obj, 4);
            return (long)N;
        }
        public static long GetPPointer(ref object obj)
        {
            object Obj = new object();
            IntPtr N = new IntPtr();
            RtlMoveMemory(ref N, ref Obj, 4);
            return (long)N;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'パス名を構成要素に分解する。
        '
        '引き数：
        'sPath パス。
        'sDrive ドライブ。"C:"
        'sDir フォルダパス。"\AAA\BBB\"
        'sTitle ファイルタイトル。"CCC"
        'sExt 拡張子。".DDD"
        Public Sub SplitPath(ByVal sPath As String, ByRef sDrive As String, ByRef sDir As String, ByRef sTitle As String, ByRef sExt As String)
            'ディレクトリ位置。
            Dim nPosDir As Long
            nPosDir = InStrRev(sPath, ":") + 1
            'ファイルタイトル位置。
            Dim nPosTitle As Long
            nPosTitle = InStrRev(sPath, "\") + 1
            If nPosTitle <= 1 Then nPosTitle = nPosDir
            '拡張子位置。
            Dim nPosExt As Long
            nPosExt = InStrRev(sPath, ".")
            If nPosExt < nPosTitle Then nPosExt = Len(sPath) + 1
            '拡張子。
            If nPosExt < nPosTitle Then
                sExt = ""
            Else
                sExt = Mid$(sPath, nPosExt)
            End If
            'ファイルタイトル。
            sTitle = Mid$(sPath, nPosTitle, nPosExt - nPosTitle)
            'ディレクトリパス。
            sDir = Mid$(sPath, nPosDir, nPosTitle - nPosDir)
            'ドライブ。
            sDrive = Mid$(sPath, 1, nPosDir - 1)
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        'パス名を構成要素に分解する。
        '
        '引き数：
        'sPath パス。
        'sDrive ドライブ。"C:"
        'sDir フォルダパス。"\AAA\BBB\"
        'sTitle ファイルタイトル。"CCC"
        'sExt 拡張子。".DDD"
        */
        public static void SplitPath(string sPath, ref string sDrive, ref string sDir, ref string sTitle, ref string sExt)
        {
            //'ディレクトリ位置。
            long nPosDir;
            nPosDir = InStrRev(sPath, ":") + 1;
            //'ファイルタイトル位置。
            long nPosTitle;
            nPosTitle = InStrRev(sPath, "\\") + 1;
            if (nPosTitle <= 1)
            {
                nPosTitle = nPosDir;
            }
            //'拡張子位置。
            long nPosExt;
            nPosExt = InStrRev(sPath, ".");
            if (nPosExt < nPosTitle)
            {
                nPosExt = sPath.Length + 1;
            }
            //'拡張子。
            if (nPosExt < nPosTitle)
            {
                sExt = "";
            }
            else
            {
                sExt = Mid(sPath, (int)nPosExt, 99);
            }
            //'ファイルタイトル。
            sTitle = Mid(sPath, (int)nPosTitle, (int)(nPosExt - nPosTitle));
            //'ディレクトリパス。
            sDir = Mid(sPath, (int)nPosDir, (int)(nPosTitle - nPosDir));
            //'ドライブ。
            sDrive = Mid(sPath, 1, (int)nPosDir - 1);
            return;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'NULL終端固定長文字列を文字列に変換する。
        '
        '引き数：
        'S NULL終端固定長文字列。
        '
        '戻り値：文字列。
        Public Function FixedStringToString(ByVal sFixed As String) As String
            FixedStringToString = Left$(sFixed, InStr(sFixed, vbNullChar) - 1)
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        'NULL終端固定長文字列を文字列に変換する。
        '
        '引き数：
        'S NULL終端固定長文字列。
        '
        '戻り値：文字列。
        */
        public static string FixedStringToString(string sFixed)
        {
            return Left(sFixed, (int)InStr(1, sFixed, null) - 1);
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '関数ポインタ取得。
        '
        '引き数：
        'lngFnPtr AddressOf の戻り値。
        '
        '戻り値：関数のポインタ。
        Public Function FnPtrToLong(ByVal lngFnPtr As Long) As Long
           FnPtrToLong = lngFnPtr
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '関数ポインタ取得。
        '
        '引き数：
        'lngFnPtr AddressOf の戻り値。
        '
        '戻り値：関数のポインタ。
        */
        public static long FnPtrToLong(long lngFnPtr)
        {
            return lngFnPtr;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '値チェック。
        '
        '引き数：
        'vValue 値。
        'vMin 最小値。
        '
        '戻り値：
        'vValue が vMin より小さい場合は vMin を返す。
        'vValue が vMin より大きい場合は vValue を返す。
        Public Function CheckReturnMin(ByVal vValue As Variant, ByVal vMin As Variant) As Variant
            If vValue < vMin Then
                CheckReturnMin = vMin
            Else
                CheckReturnMin = vValue
            End If
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '値チェック。
        '
        '引き数：
        'vValue 値。
        'vMin 最小値。
        '
        '戻り値：
        'vValue が vMin より小さい場合は vMin を返す。
        'vValue が vMin より大きい場合は vValue を返す。
        */
        public static int CheckReturnMin(int vValue, int vMin)
        {
            return vValue < vMin ? vMin : vValue;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'Ceエラーメッセージを取得する。2016/02/18 Add
        '
        'GetLastError で示されるエラーのエラーメッセージを取得する。
        '
        '戻り値：エラーメッセージ。
        Public Function CeGetLastErrorMessage() As String
            CeGetLastErrorMessage = GetErrorMessage(CeGetLastError())
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        'Ceエラーメッセージを取得する。2016/02/18 Add
        '
        'GetLastError で示されるエラーのエラーメッセージを取得する。
        '
        '戻り値：エラーメッセージ。
        */
        public static string CeGetLastErrorMessage()
        {
            return GetErrorMessage(CeGetLastError());
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'エラーメッセージを取得する。
        '
        'GetLastError で示されるエラーのエラーメッセージを取得する。
        '
        '戻り値：エラーメッセージ。
        Public Function GetLastErrorMessage() As String
            GetLastErrorMessage = GetErrorMessage(GetLastError())
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        'エラーメッセージを取得する。
        '
        'GetLastError で示されるエラーのエラーメッセージを取得する。
        '
        '戻り値：エラーメッセージ。
        */
        public static string GetLastErrorMessage()
        {
            return "";
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'エラーメッセージを取得する。
        '
        '指定されたエラーコードのエラーメッセージを取得する。
        '
        '引き数：
        'lCode エラーコード。
        '
        '戻り値：エラーメッセージ。
        Public Function GetErrorMessage(ByVal lCode As Long) As String
            Dim sRtrnCode As String
            Dim lRet As Long
            sRtrnCode = Space$(1024)
            lRet = FormatMessage(FORMAT_MESSAGE_FROM_SYSTEM, 0 &, lCode, 0 &, sRtrnCode, 1024 &, 0 &)
            If lRet > 0 Then
                sRtrnCode = Left$(sRtrnCode, InStr(sRtrnCode, vbNullChar) - 1)
                If Right$(sRtrnCode, Len(vbCrLf)) = vbCrLf Then sRtrnCode = Left$(sRtrnCode, Len(sRtrnCode) - Len(vbCrLf))
                GetErrorMessage = sRtrnCode
            Else
                GetErrorMessage = ""
            End If
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        'エラーメッセージを取得する。
        '
        '指定されたエラーコードのエラーメッセージを取得する。
        '
        '引き数：
        'lCode エラーコード。
        '
        '戻り値：エラーメッセージ。
        */
        public static string GetErrorMessage(long lCode)
        {

            string sRtrnCode = "";
            long lRet;
            sRtrnCode.PadLeft(1024);

#if false
            lRet = FormatMessage(FORMAT_MESSAGE_FROM_SYSTEM, 0 &, lCode, 0 &, sRtrnCode, 1024 &, 0 &);
#else
            lRet = 0;
#endif
            if (lRet > 0)
            {
                sRtrnCode = Left(sRtrnCode, (int)InStr(0, sRtrnCode, null) - 1);
                if (Right(sRtrnCode, 2) == "/r/n")
                {
                    sRtrnCode = Left(sRtrnCode, sRtrnCode.Length - 2);
                    return sRtrnCode;
                }
            }
            else
            {
                return "";
            }
            return "";
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '空文字に対応した WritePrivateProfileString 関数。
        '
        '空文字とデフォルト値を区別する。
        '
        '引き数：
        'lpAppName セクション名。
        'lpKeyName キー名。
        'lpString 文字列値。
        'lpFileName .ini ファイルの名前。
        '
        '戻り値：WritePrivateProfileString の戻り値。
        Public Function WritePrivateProfileStringEx(ByVal lpAppName As String, ByVal lpKeyName As String, ByVal lpString As String, ByVal lpFileName As String) As Long
            WritePrivateProfileStringEx = WritePrivateProfileString(lpAppName, lpKeyName, "@" & lpString, lpFileName)
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '空文字に対応した WritePrivateProfileString 関数。
        '
        '空文字とデフォルト値を区別する。
        '
        '引き数：
        'lpAppName セクション名。
        'lpKeyName キー名。
        'lpString 文字列値。
        'lpFileName .ini ファイルの名前。
        '
        '戻り値：WritePrivateProfileString の戻り値。
        */
        public static long WritePrivateProfileStringEx(string lpAppName, string lpKeyName, string lpString, string lpFileName)
        {
            return WritePrivateProfileString(lpAppName, lpKeyName, "@" + lpString, lpFileName);
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '空文字に対応した GetPrivateProfileString 関数。
        '
        '空文字とデフォルト値を区別する。
        '空文字を lpEmpty に置き換えて書き込み、lpEmpty を読み込んだら空文字に置き換える。
        '
        '引き数：
        'lpAppName セクション名。
        'lpKeyName キー名。
        'lpDefault 既定の文字列。
        'lpFileName .ini ファイルの名前。
        '
        '戻り値：文字列値。
        Public Function GetPrivateProfileStringEx(ByVal lpAppName As String, ByVal lpKeyName As String, ByVal lpDefault As String, ByVal lpFileName As String) As String
            Dim sValue As String
            sValue = GetPrivateProfileString(lpAppName, lpKeyName, "@" & lpDefault, lpFileName)
            If Left(sValue, 1) = "@" Then
                GetPrivateProfileStringEx = Mid$(sValue, 2)
            Else
                GetPrivateProfileStringEx = sValue
            End If
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '空文字に対応した GetPrivateProfileString 関数。
        '
        '空文字とデフォルト値を区別する。
        '空文字を lpEmpty に置き換えて書き込み、lpEmpty を読み込んだら空文字に置き換える。
        '
        '引き数：
        'lpAppName セクション名。
        'lpKeyName キー名。
        'lpDefault 既定の文字列。
        'lpFileName .ini ファイルの名前。
        '
        '戻り値：文字列値。
        */
        public static string GetPrivateProfileStringEx(string lpAppName, string lpKeyName, string lpDefault, string lpFileName)
        {
            string sValue;
            sValue = GetPrivateProfileString(lpAppName, lpKeyName, "@" + lpDefault, lpFileName);
            return Left(sValue, 1) == "@" ? Mid(sValue, 2, 99) : sValue;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '複数行文字列をiniファイルに書き込む。
        '
        '引き数：
        'lpAppName セクション名。
        'lpKeyName キー。
        'lpString 複数行文字列。
        'lpFileName iniファイルのパス。
        '
        '戻り値：
        '正常終了の場合 0 以外の値を返す。
        'それ以外の場合 0 を返す。
        Public Function WritePrivateProfileMultiString(ByVal lpAppName As String, ByVal lpKeyName As String, ByVal lpString As String, ByVal lpFileName As String) As Long
            Dim nCode() As Byte
            nCode = StrConv(lpString, vbFromUnicode)
            Dim nSize As Long
            nSize = UBound(nCode) + 1
            ReDim Preserve nCode(nSize + 3)
            Call MoveMemory(nCode(4), nCode(0), nSize)
            Dim nLong(3) As Byte
            Call MoveMemory(nLong(0), nSize, 4)
            nCode(0) = nLong(3)
            nCode(1) = nLong(2)
            nCode(2) = nLong(1)
            nCode(3) = nLong(0)
            WritePrivateProfileMultiString = WritePrivateProfileStruct(lpAppName, lpKeyName, nCode(0), UBound(nCode) + 1, lpFileName)
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '複数行文字列をiniファイルに書き込む。
        '
        '引き数：
        'lpAppName セクション名。
        'lpKeyName キー。
        'lpString 複数行文字列。
        'lpFileName iniファイルのパス。
        '
        '戻り値：
        '正常終了の場合 0 以外の値を返す。
        'それ以外の場合 0 を返す。
        */
        public static long WritePrivateProfileMultiString(string lpAppName, string lpKeyName, string lpString, string lpFileName)
        {
#if false
            Dim nCode() As Byte
            nCode = StrConv(lpString, vbFromUnicode)
            Dim nSize As Long
            nSize = UBound(nCode) + 1
            ReDim Preserve nCode(nSize + 3)
            Call MoveMemory(nCode(4), nCode(0), nSize)
            Dim nLong(3) As Byte
            Call MoveMemory(nLong(0), nSize, 4)
            nCode(0) = nLong(3)
            nCode(1) = nLong(2)
            nCode(2) = nLong(1)
            nCode(3) = nLong(0)
            WritePrivateProfileMultiString = WritePrivateProfileStruct(lpAppName, lpKeyName, nCode(0), UBound(nCode) + 1, lpFileName)
#else
            return 0;
#endif
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '複数行文字列をiniファイルから読み込む。
        '
        '引き数：
        'lpAppName セクション名。
        'lpKeyName キー。
        'lpDefault デフォルト値。
        'lpFileName iniファイルのパス。
        '
        '戻り値：読み込んだ複数行文字列。
        Public Function GetPrivateProfileMultiString(ByVal lpAppName As String, ByVal lpKeyName As String, ByVal lpDefault As String, ByVal lpFileName As String) As String
            GetPrivateProfileMultiString = lpDefault
            On Error GoTo ErrorHandler
            Dim sBuff As String
            sBuff = GetPrivateProfileString(lpAppName, lpKeyName, "00000000", lpFileName)
            Dim nSize As String
            nSize = Val("&H" & Left$(sBuff, 8))
            Dim nCode() As Byte
            ReDim nCode(nSize + 3)
            If GetPrivateProfileStruct(lpAppName, lpKeyName, nCode(0), UBound(nCode) + 1, lpFileName) = 0 Then Exit Function
            Call MoveMemory(nCode(0), nCode(4), nSize)
            ReDim Preserve nCode(nSize - 1)
            GetPrivateProfileMultiString = StrConv(nCode, vbUnicode)
        ErrorHandler:
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '複数行文字列をiniファイルから読み込む。
        '
        '引き数：
        'lpAppName セクション名。
        'lpKeyName キー。
        'lpDefault デフォルト値。
        'lpFileName iniファイルのパス。
        '
        '戻り値：読み込んだ複数行文字列。
        */
        public static string GetPrivateProfileMultiString(string lpAppName, string lpKeyName, string lpDefault, string lpFileName)
        {
            try
            {
#if false
                GetPrivateProfileMultiString = lpDefault
                On Error GoTo ErrorHandler
                Dim sBuff As String
                sBuff = GetPrivateProfileString(lpAppName, lpKeyName, "00000000", lpFileName)
                Dim nSize As String
                nSize = Val("&H" & Left$(sBuff, 8))
                Dim nCode() As Byte
                ReDim nCode(nSize +3)
                If GetPrivateProfileStruct(lpAppName, lpKeyName, nCode(0), UBound(nCode) + 1, lpFileName) = 0 Then Exit Function
                Call MoveMemory(nCode(0), nCode(4), nSize)
                ReDim Preserve nCode(nSize - 1)
                GetPrivateProfileMultiString = StrConv(nCode, vbUnicode)
#else
                return null;
#endif
            }

            catch (Exception)
            {
                return null;
            }
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '複数行文字列を一行ずつ配列に分ける。改行は省く。
        '
        '引き数：
        'sText 複数行文字列。
        '
        '戻り値：文字列配列。配列の要素は(-1 To ...)、要素 -1 は未使用。
        Public Function SeparateMultiString(ByVal sText As String) As String()
            Dim nCrLf As Long
            nCrLf = Len(vbCrLf)
            Dim sLines() As String
            ReDim sLines(-1 To 0)
            Dim nIndex As Long
            nIndex = 0
            Dim nStart As Long
            nStart = 1
            Do
                Dim nPos As Long
                nPos = InStr(nStart, sText, vbCrLf)
                If nStart <= nPos Then
                    sLines(nIndex) = Mid(sText, nStart, nPos - nStart)
                    nIndex = nIndex + 1
                    ReDim Preserve sLines(-1 To nIndex)
                    nStart = nPos + nCrLf
                Else
                    sLines(nIndex) = Mid(sText, nStart)
                    Exit Do
                End If
            Loop
            SeparateMultiString = sLines
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '複数行文字列を一行ずつ配列に分ける。改行は省く。
        '
        '引き数：
        'sText 複数行文字列。
        '
        '戻り値：文字列配列。配列の要素は(-1 To ...)、要素 -1 は未使用。
        */
        public static string[] SeparateMultiString(string sText)
        {
            long nCrLf;
            nCrLf = 2;
            string[] sLines;
            sLines = new string[0];
            long nIndex;
            nIndex = 0;
            long nStart;
            nStart = 1;
            long nPos;
            do
            {
                nPos = InStr((int)nStart, sText, "\r\n");
                if (nStart <= nPos)
                {
                    sLines[nIndex] = Mid(sText, (int)nStart, (int)(nPos - nStart));
                    nIndex = nIndex + 1;
                    sLines = new string[nIndex];
                    nStart = nPos + nCrLf;
                }
                else
                {
                    sLines[nIndex] = Mid(sText, (int)nStart, 99);
                    break;
                }
            } while (true);

            return sLines;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'バージョンを取得する。
        '
        '指定されたモジュールのバージョンを取得する。
        '
        '引き数：
        'sPath モジュールのパス。
        'nMajor 取得されたメジャーバージョン。
        'nMinor 取得されたマイナーバージョン。
        'nBuild 取得されたビルドバージョン。
        'nPrivate 取得されたプライベートバージョン。
        Public Sub GetModuleVersion(ByVal sPath As String, ByRef nMajor As Long, ByRef nMinor As Long, ByRef nBuild As Long, ByRef nPrivate As Long)
            Dim dwHandle As Long
            Dim nSize As Long
            nSize = GetFileVersionInfoSize(sPath, dwHandle)
            If nSize <= 0 Then Call Err.Raise(vbObjectError Or & H200 & Or & H1 &, , GetLastErrorMessage())
            Dim nBuff() As Byte
            ReDim nBuff(nSize)
            If GetFileVersionInfo(sPath, dwHandle, nSize, nBuff(0)) = 0 Then Call Err.Raise(vbObjectError Or & H200 & Or & H1 &, , GetLastErrorMessage())
            Dim pBuffer As Long
            Dim uLen As Long
            If VerQueryValue(nBuff(0), "\", pBuffer, uLen) = 0 Then Call Err.Raise(vbObjectError Or &H200& Or &H1&, , GetLastErrorMessage())
            Dim rInfo As VS_FIXEDFILEINFO
            Call MoveMemory(rInfo, ByVal pBuffer, LenB(rInfo))
            Dim nMS(1) As Integer
            Dim nLS(1) As Integer
            Call MoveMemory(nMS(0), rInfo.dwFileVersionMS, LenB(nMS(0)) * 2)
            Call MoveMemory(nLS(0), rInfo.dwFileVersionLS, LenB(nLS(0)) * 2)
            nMajor = nMS(1)
            nMinor = nMS(0)
            nBuild = nLS(1)
            nPrivate = nLS(0)
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        'バージョンを取得する。
        '
        '指定されたモジュールのバージョンを取得する。
        '
        '引き数：
        'sPath モジュールのパス。
        'nMajor 取得されたメジャーバージョン。
        'nMinor 取得されたマイナーバージョン。
        'nBuild 取得されたビルドバージョン。
        'nPrivate 取得されたプライベートバージョン。
        */
        public static void GetModuleVersion(string sPath, long nMajor, long nMinor, long nBuild, long nPrivate)
        {
#if false
            long dwHandle = 0;
            long nSize = 0;
            nSize = GetFileVersionInfoSize(sPath, dwHandle);
            if (nSize <= 0)
            {
                //Err.Raise(vbObjectError | 0x200 | 0x01, , GetLastErrorMessage());
            }
            byte[] nBuff = new byte[nSize];
            if (GetFileVersionInfo(sPath, dwHandle, nSize, nBuff[0]) == 0)
            {
                //Err.Raise(vbObjectError  0x200 | 0x01, , GetLastErrorMessage());
            }
            long pBuffer;
            long uLen;
            if (VerQueryValue(nBuff(0), "\\", pBuffer, uLen) == 0)
            {
                //Err.Raise(vbObjectError | 0x200 | 0x01, , GetLastErrorMessage());
            }
            VS_FIXEDFILEINFO rInfo;
            MoveMemory(rInfo, pBuffer, rInfo.length);
            int[] nMS = new int[1024];
            int[] nLS = new int[1024];
            MoveMemory(ref (byte)nMS[0], ref (byte)rInfo.dwFileVersionMS, Strings.Len(nMS[0]) * 2);
            MoveMemory(ref (byte)nLS[0], ref (byte)rInfo.dwFileVersionLS, Strings.Len(nLS[0]) * 2);
            nMajor = nMS[1];
            nMinor = nMS[0];
            nBuild = nLS[1];
            nPrivate = nLS[0];
#endif
            return;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'バージョン情報を取得する。
        '
        'リソースから指定されたキーのバージョン情報を読み取る。
        '
        '引き数：
        'hModule モジュールのハンドル。
        'sKey キー。
        'sVersionInfo バージョン情報が設定される。
        Public Sub GetVersionInfo(ByVal hModule As Long, ByVal sKey As String, ByRef sVersionInfo As String)

            Dim sText As String * MAX_PATH
            sText = ""
            Call GetModuleFileName(hModule, sText, Len(sText) - 1)
            Dim sPath As String
            sPath = Left$(sText, InStr(sText, vbNullChar) - 1)


            Dim dwHandle As Long
            Dim nSize As Long
            nSize = GetFileVersionInfoSize(sPath, dwHandle)
            If nSize <= 0 Then Call Err.Raise(vbObjectError Or & H200 & Or & H1 &, , GetLastErrorMessage())
            Dim nBuff() As Byte
            ReDim nBuff(nSize)
            If GetFileVersionInfo(sPath, dwHandle, nSize, nBuff(0)) = 0 Then Call Err.Raise(vbObjectError Or & H200 & Or & H1 &, , GetLastErrorMessage())


            Dim pBuffer As Long
            Dim uLen As Long
            If VerQueryValue(nBuff(0), "\VarFileInfo\Translation", pBuffer, uLen) = 0 Then Call Err.Raise(vbObjectError Or & H200 & Or & H1 &, , GetLastErrorMessage())
            Dim nLangAndCodePage(1) As Integer
            Call MoveMemory(nLangAndCodePage(0), ByVal pBuffer, LenB(nLangAndCodePage(0)) * (UBound(nLangAndCodePage) + 1))


            Dim sSubBlock As String
            sSubBlock = "\StringFileInfo\" & Replace(Format$(Hex$(nLangAndCodePage(0)), " < @@@@"), " ", "0") & Replace(Format$(Hex$(nLangAndCodePage(1)), " < @@@@"), " ", "0") & "\" & sKey
            If VerQueryValue(nBuff(0), sSubBlock, pBuffer, uLen) = 0 Then Call Err.Raise(vbObjectError Or & H200 & Or & H1 &, , GetLastErrorMessage())
            Dim nVersionInfo() As Byte
            ReDim nVersionInfo(uLen)
            Call MoveMemory(nVersionInfo(0), ByVal pBuffer, uLen)
            sVersionInfo = StrConv(nVersionInfo, vbUnicode)


        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        'バージョン情報を取得する。
        '
        'リソースから指定されたキーのバージョン情報を読み取る。
        '
        '引き数：
        'hModule モジュールのハンドル。
        'sKey キー。
        'sVersionInfo バージョン情報が設定される。
        */
        public static void GetVersionInfo(long hModule, string sKey, string sVersionInfo)
        {
#if false
            Dim sText As String *MAX_PATH
            sText = ""
            Call GetModuleFileName(hModule, sText, Len(sText) -1)
            Dim sPath As String
            sPath = Left$(sText, InStr(sText, vbNullChar) - 1)


            Dim dwHandle As Long
            Dim nSize As Long
            nSize = GetFileVersionInfoSize(sPath, dwHandle)
            If nSize <= 0 Then Call Err.Raise(vbObjectError Or & H200 & Or & H1 &, , GetLastErrorMessage())
            Dim nBuff() As Byte
            ReDim nBuff(nSize)
            If GetFileVersionInfo(sPath, dwHandle, nSize, nBuff(0)) = 0 Then Call Err.Raise(vbObjectError Or & H200 & Or & H1 &, , GetLastErrorMessage())


            Dim pBuffer As Long
            Dim uLen As Long
            If VerQueryValue(nBuff(0), "\VarFileInfo\Translation", pBuffer, uLen) = 0 Then Call Err.Raise(vbObjectError Or & H200 & Or & H1 &, , GetLastErrorMessage())
            Dim nLangAndCodePage(1) As Integer
            Call MoveMemory(nLangAndCodePage(0), ByVal pBuffer, LenB(nLangAndCodePage(0)) *(UBound(nLangAndCodePage) + 1))


            Dim sSubBlock As String
            sSubBlock = "\StringFileInfo\" & Replace(Format$(Hex$(nLangAndCodePage(0)), " < @@@@"), " ", "0") & Replace(Format$(Hex$(nLangAndCodePage(1)), " < @@@@"), " ", "0") & "\" & sKey
            If VerQueryValue(nBuff(0), sSubBlock, pBuffer, uLen) = 0 Then Call Err.Raise(vbObjectError Or & H200 & Or & H1 &, , GetLastErrorMessage())
            Dim nVersionInfo() As Byte
            ReDim nVersionInfo(uLen)
            Call MoveMemory(nVersionInfo(0), ByVal pBuffer, uLen)
            sVersionInfo = StrConv(nVersionInfo, vbUnicode)
#endif
            return;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '特殊フォルダーのパスの取得。
        '
        '引き数：
        'nFolder 取得するフォルダーのクラスID。
        '
        '戻り値：
        '正常終了の場合はフォルダーのパスを返す。
        'それ以外の場合は空文字を返す。
        Public Function GetShellFolderPath(ByVal nFolder As Long) As String
            GetShellFolderPath = ""
            On Error GoTo FileErrorHandler
            Dim sPath As String * MAX_PATH
            If SHGetFolderPath(0, nFolder, 0, 0, sPath) <> S_OK Then
                Exit Function
            End If
            GetShellFolderPath = Left$(sPath, InStr(sPath, vbNullChar) - 1)
            Exit Function
        FileErrorHandler:
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '特殊フォルダーのパスの取得。
        '
        '引き数：
        'nFolder 取得するフォルダーのクラスID。
        '
        '戻り値：
        '正常終了の場合はフォルダーのパスを返す。
        'それ以外の場合は空文字を返す。
        */
        public static string GetShellFolderPath(long nFolder)
        {
            try
            {
                string sPath = "";
#if false
                if (SHGetFolderPath(0, nFolder, 0, 0, sPath) != S_OK)
                {
                    return null;
                }
                return Left(sPath, Strings.InStr(sPath, null) - 1);
#else
                return null;
#endif
            }

            catch (Exception)
            {
                return null;
            }
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '指定されたウィンドウのフォントをコピーする。
        '
        '引き数：
        'hWnd コピー元のウィンドウのハンドル。
        'hDC フォントのサイズを取得するためのデバイスコンテキストのハンドル。
        'nPoint フォントのポイント。
        'bBold 太字指定。
        '
        '戻り値：作成されたフォントのハンドル。
        Public Function CopyWindowFont(ByVal hWnd As Long, ByVal hDC As Long, Optional nPoint As Long = -1, Optional bBold As Boolean = False) As Long
            On Error GoTo ErrorHandler
            Dim hFont As Long
            hFont = SendMessage(hWnd, WM_GETFONT, 0, 0)
            Dim rLogFont As LOGFONT
            Call GetObjectFont(hFont, LenB(rLogFont), rLogFont)
            Dim hSrcDC As Long
            hSrcDC = GetDC(hWnd)
            If nPoint < 0 Then
                nPoint = MulDiv(rLogFont.lfHeight, 720, GetDeviceCaps(hSrcDC, LOGPIXELSY))
            Else
                nPoint = nPoint * 10
            End If
            rLogFont.lfHeight = MulDiv(nPoint, GetDeviceCaps(hDC, LOGPIXELSY), 720)
            If bBold Then rLogFont.lfWeight = 700
            CopyWindowFont = CreateFontIndirect(rLogFont)
            Call ReleaseDC(hWnd, hSrcDC)
            Exit Function
        ErrorHandler:
            If hSrcDC<> 0 Then Call ReleaseDC(hWnd, hSrcDC)
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '指定されたウィンドウのフォントをコピーする。
        '
        '引き数：
        'hWnd コピー元のウィンドウのハンドル。
        'hDC フォントのサイズを取得するためのデバイスコンテキストのハンドル。
        'nPoint フォントのポイント。
        'bBold 太字指定。
        '
        '戻り値：作成されたフォントのハンドル。
        */
        public static long CopyWindowFont(long hWnd, long hDC, long nPoint = -1, bool bBold = false)
        {
            return 0;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'ポイントと名前を指定してフォントを作成する。
        '
        '引き数：
        'nPointSize フォントのポイント。
        'lpszFaceName フォントの名前。
        'hDC フォントのサイズを取得するためのデバイスコンテキストのハンドル。
        '
        '戻り値：作成されたフォントのハンドル。
        Public Function CreatePointFont(ByVal nPointSize As Long, ByVal lpszFaceName As String, Optional ByVal hDC As Long = 0) As Long
            Dim rLogFont As LOGFONT
            rLogFont.lfCharSet = DEFAULT_CHARSET
            rLogFont.lfHeight = nPointSize
            rLogFont.lfFaceName = lpszFaceName & vbNullChar
            Dim hTmp As Long
            If hDC = 0 Then
                hTmp = GetDC(0)
            Else
                hTmp = hDC
            End If
            Dim rPoint As POINT
            rPoint.Y = GetDeviceCaps(hTmp, LOGPIXELSY) * rLogFont.lfHeight
            rPoint.Y = rPoint.Y / 720
            Call DPtoLP(hDC, rPoint, 1)
            Dim rOrg As POINT
            rOrg.X = 0
            rOrg.Y = 0
            Call DPtoLP(hDC, rOrg, 1)
            rLogFont.lfHeight = -Abs(rPoint.Y - rOrg.Y)
            If hDC = 0 Then Call ReleaseDC(0, hTmp)
            CreatePointFont = CreateFontIndirect(rLogFont)
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        'ポイントと名前を指定してフォントを作成する。
        '
        '引き数：
        'nPointSize フォントのポイント。
        'lpszFaceName フォントの名前。
        'hDC フォントのサイズを取得するためのデバイスコンテキストのハンドル。
        '
        '戻り値：作成されたフォントのハンドル。
        */
        public static long CreatePointFont(long nPointSize, string lpszFaceName, long hDC = 0)
        {
            return 0;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '少数部切り捨て。
        '
        'nDigits で指定された少数点以下桁数で切り捨てる。
        '
        '引き数：
        'nValue 値。
        'nDigits 少数点以下桁数。
        '
        '戻り値：結果値。
        Public Function RoundDown(ByVal nValue As Double, ByVal nDigits As Long) As Double
            If nValue < &H7FFFFFFF Then
                Dim nInteger As Long
                nInteger = Fix(nValue)
                nValue = nValue - nInteger
                If nDigits <= 8 Then
                    Dim nCoeff As Long
                    nCoeff = 10 ^ nDigits
                    Dim nRound As Long
                    nValue = nValue * nCoeff
                    nRound = Fix(nValue)
                    RoundDown = nInteger + (nRound / nCoeff)
                Else
                    nValue = nValue * 100000000
                    nValue = RoundDown(nValue, nDigits - 8)
                    RoundDown = nInteger + (nValue / 100000000)
                End If
            Else
                nValue = nValue / 100000000
                nValue = RoundDown(nValue, nDigits + 8)
                RoundDown = nValue * 100000000
            End If
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '少数部切り捨て。
        '
        'nDigits で指定された少数点以下桁数で切り捨てる。
        '
        '引き数：
        'nValue 値。
        'nDigits 少数点以下桁数。
        '
        '戻り値：結果値。
        */
        public static double RoundDown(double nValue, long nDigits)
        {
            long nInteger;
            if (nValue < 0x7FFFFFFF)
            {
                nInteger = (long)Math.Truncate(nValue);
                nValue -= nInteger;
                if (nDigits <= 8)
                {
                    long nCoeff;
                    nCoeff = (long)Math.Pow(10, nDigits);
                    long nRound;
                    nValue *= nCoeff;
                    nRound = (long)Math.Truncate(nValue);
                    return nInteger + (nRound / nCoeff);
                }
                else
                {
                    nValue *= 100000000;
                    nValue = RoundDown(nValue, nDigits - 8);
                    return nInteger + (nValue / 100000000);
                }
            }
            else
            {
                nValue /= 100000000;
                nValue = RoundDown(nValue, nDigits + 8);
                return nValue * 100000000;
            }
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'アプリケーションのマイナーバージョン番号を取得する。
        '
        '戻り値：アプリケーションのマイナーバージョン番号を返す。
        Public Function GetAppMinorVersion() As Integer
            GetAppMinorVersion = Fix(App.Minor / 10)
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        'アプリケーションのマイナーバージョン番号を取得する。
        '
        '戻り値：アプリケーションのマイナーバージョン番号を返す。
        */
        public static int GetAppMinorVersion()
        {
#if false
            return (int)Math.Truncate(AppMinor / 10);
#else
            return 0;
#endif
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'アプリケーションのビルドバージョン番号を取得する。
        '
        '戻り値：アプリケーションのビルドバージョン番号を返す。
        Public Function GetAppBuildVersion() As Integer
            GetAppBuildVersion = App.Minor Mod 10
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        'アプリケーションのビルドバージョン番号を取得する。
        '
        '戻り値：アプリケーションのビルドバージョン番号を返す。
        */
        public static int GetAppBuildVersion()
        {
#if false
            return AppMinor % 10
#else
            return 0;
#endif
        }
        //==========================================================================================

        //***************************************************************************
        //***************************************************************************
        /// <summary>
        //[新規]  フォルダのファイルごとコピー。
        ///  '引き数：
        //  'sSrcPath コピー元ディレクトリのパス。
        //  'sDstPath コピー先ディレクトリのパス。
        //  'bRaise Err.Raise の有無。
        /// </summary>
        /// <param name="sSrcPath">
        /// <param name="sDstPath">
        /// <param name="bRaiser">
        /// </param>
        /// <returns>
        /// 戻り値:returns = 
        ///         '正常終了の場合 True を返す。
        ///         'それ以外の場合 False を返す。
        /// </returns>
        //***************************************************************************
        //　フォルダのファイルごとコピー
        public static bool DirFileAllCopy(string sSrcPath, string sDstPath, bool bRaise)
        {

            bool sts = false;

            //文字末尾が"\"の場合は、文字から削除する
            sSrcPath = RTrimEx(sSrcPath, "\\");
            sDstPath = RTrimEx(sDstPath, "\\");


            string[] picList = Directory.GetFiles(sSrcPath, "*.*");

            string fName;
            try
            {
                // Copy picture files.
                foreach (string f in picList)
                {
                    // Remove path from the file name.
                    fName = f.Substring(sSrcPath.Length + 1);

                    // ファイルパス結合
                    string fileName = Path.Combine(sSrcPath, fName);


                    // Use the Path.Combine method to safely append the file name to the path.
                    // Will overwrite if the destination file already exists.
                    System.IO.File.Copy(Path.Combine(sSrcPath, fName), Path.Combine(sDstPath, fName), true);
                }
                sts = true;

            }
            catch (Exception ex)
            {
                if (bRaise) { _ = MessageBox.Show(ex.Message, "エラー発生", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }


            return sts;
        }
        //***************************************************************************
        //***************************************************************************



        //2==========================================================================================
        /*[VB]
        '文字列の末尾から、任意の文字を省く。
        '
        '引き数：
        'sValue 文字列。
        'sTarget 省く文字。
        '
        '戻り値：結果文字列。
        Public Function RTrimEx(ByVal sValue As String, ByVal sTarget As String) As String
            Dim nValue() As Byte
            Dim nTarget() As Byte
            Dim i As Long
            nValue = sValue
            nTarget = Left$(sTarget, 1)
            For i = UBound(nValue) To 0 Step -2
            If nValue(i) <> nTarget(1) Or nValue(i - 1) <> nTarget(0) Then Exit For
            Next
            RTrimEx = LeftB$(sValue, i + 1)
        End Function
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#] 2
        public static string RTrimEx(string sValue, string sTarget)
        {
            string RTrimEx = sValue;


            //ASCII エンコード
            byte[] nValue = Encoding.ASCII.GetBytes(sValue);
            byte[] nTarget = Encoding.ASCII.GetBytes(sTarget);


            //文字末尾が"\"の場合は、文字から削除する
            if (sTarget.Contains("\\"))
            {
                if (sValue.EndsWith(sTarget))
                {
                    RTrimEx = sValue.Remove(sValue.Length - 1);
                    return RTrimEx;
                }
                else
                {
                    return RTrimEx;
                }

            }

            //右から数字の指定文字を削除する
            int i;
            for (i = nValue.Length - 1; i <= 0; i -= 2)
            {
                // If nValue(i) <> nTarget(1) Or nValue(i -1) <> nTarget(0) Then Exit For
                if (nValue[i] != nTarget[1] || nValue[i - 1] != nTarget[0])
                {
                    break;
                }

            }
            //  RTrimEx = LeftB$(sValue, i + 1)
            RTrimEx = sValue.Substring(0, i + 1);


            return RTrimEx;

        }
#if false
                public static string RTrimEx(string sValue, string sTarget)
                {
                    //文字末尾が"\"の場合は、文字から削除する
                    if (sValue.EndsWith(sTarget))
                    {
                        sValue = sValue.Remove(sValue.Length - 1);

                    }
                    return sValue;
                }
#endif
            //***************************************************************************
            //***************************************************************************


            //***************************************************************************
            //***************************************************************************
            /// <summary>
            //[VB]  'ディレクトリをコピーする。
            /// （VB CopyDir関数をVC用に変更）
            ///  '引き数：
            //  'sSrcPath コピー元ディレクトリのパス。
            //  'sDstPath コピー先ディレクトリのパス。
            //  'bRaise Err.Raise の有無。
            /// </summary>
            /// <param name="sSrcPath">
            /// <param name="sDstPath">
            /// <param name="bRaiser">
            /// </param>
            /// <returns>
            /// 戻り値:returns = 
            ///         '正常終了の場合 True を返す。
            ///         'それ以外の場合 False を返す。
            /// </returns>
            //***************************************************************************
            //[VB]  Public Function CopyDir(ByVal sSrcPath As String, ByVal sDstPath As String, Optional ByVal bRaise As Boolean = False) As Boolean
            public static bool CopyDir(string sSrcPath, string sDstPath, bool bRaise = false)
        {
            bool sts = false;

            //***************************************************************
            // あらかじめ dll をコピーしておく
            //var asm = System.Reflection.Assembly.LoadFrom("system32.dll");
            //dynamic obj = asm.CreateInstance("system32.Sample");
            //  obj.copy(sSrcPath, sDstPath);
            //***************************************************************


            try
            {
                //----------------------------------------------
                //[VB]      Dim fs, f
                //[VB]      Set fs = CreateObject("Scripting.FileSystemObject")
                //[VB]      Set f = fs.GetFolder(sSrcPath)
                //[VB]      Call f.Copy(RTrimEx(sDstPath, "\"))
                //[VB]      CopyDir = True
                //----------------------------------------------
                //文字末尾が"\"の場合は、文字から削除する
                sDstPath = RTrimEx(sDstPath, "\\");


                if (Directory.Exists(sDstPath) == false)
                {
                    //コピー先ディレクトリが無いので、作成する
                    _ = Directory.CreateDirectory(sDstPath);
                }



                //DEBUG>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
                //Object aa = new object("Scripting.FileSystemObject");
                //CreateObject("Scripting.FileSystemObject")
                //var s = new System.FileSystemObject();
                //System.IO.Directory.Move(sSrcPath, sDstPath);
                // インスタンスの生成
                //  hRet = new CreateInstance("Scripting.FileSystemObject");
                //DEBUG<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<


                //　フォルダのファイルごとコピー
                bool dym3 = DirFileAllCopy(sSrcPath, sDstPath, false);
                sts = true;




                sts = true;
            }
            catch (Exception ex)
            {
                if (bRaise) { _ = MessageBox.Show(ex.Message, "エラー発生", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }



            return sts;
        }
        //----------------------------------------------
        //[VB]  'ディレクトリをコピーする。
        //[VB]  '
        //[VB]  '引き数：
        //[VB]  'sSrcPath コピー元ディレクトリのパス。
        //[VB]  'sDstPath コピー先ディレクトリのパス。
        //[VB]  'bRaise Err.Raise の有無。
        //[VB]  '
        //[VB]  '戻り値：
        //[VB]  '正常終了の場合 True を返す。
        //[VB]  'それ以外の場合 False を返す。
        //[VB]  Public Function CopyDir(ByVal sSrcPath As String, ByVal sDstPath As String, Optional ByVal bRaise As Boolean = False) As Boolean
        //[VB]      CopyDir = False
        //[VB]      On Error GoTo FileErrorHandler
        //[VB]      Dim fs, f
        //[VB]      Set fs = CreateObject("Scripting.FileSystemObject")
        //[VB]      Set f = fs.GetFolder(sSrcPath)
        //[VB]      Call f.Copy(RTrimEx(sDstPath, "\"))
        //[VB]      CopyDir = True
        //[VB]  Exit Function
        //[VB]  FileErrorHandler:
        //[VB]      If bRaise Then Call Err.Raise(Err.Number, Err.Source, Err.Description, Err.HelpFile, Err.HelpContext)
        //[VB]  End Function
        //***************************************************************************
        //***************************************************************************

        //***************************************************************************
        //***************************************************************************
        /// <summary>
        /// Variant型(VerType8)のバイナリファイルデータをstring型で読み込む
        /// （VB GetString関数をVC用に変更）
        ///  '引き数：
        /// バイナリファイル
        /// </summary>
        /// <param name="BinaryReader br">
        /// </param>
        /// <returns>
        /// 戻り値:returns = 
        ///         '正常終了の場合 True を返す。
        ///         'それ以外の場合 False を返す。
        /// </returns>
        //***************************************************************************
        public static bool DeleteDir(string sPath, bool bRaise)
        {
            bool sts = false;

            try
            {
                //文字末尾が"\"の場合は、文字から削除する
                sPath = RTrimEx(sPath, "\\");
                //このフォルダの有無
                if (Directory.Exists(sPath))
                {
                    //フォルダ有り

                    string[] picList = Directory.GetFiles(sPath, "*.*");
                    string fName;

                    foreach (string f in picList)
                    {
                        // Remove path from the file name.
                        fName = f.Substring(sPath.Length + 1);

                        if (IsDots(fName) == false)
                        {
                            if (System.IO.File.Exists(Path.Combine(sPath, fName)))
                            {
                                //ファイル有り
                                System.IO.File.Delete(Path.Combine(sPath, fName));
                            }
                        }

                    }
                    Directory.Delete(sPath);

                    sts = true;
                }
                else
                {
                    sts = false;
                }

            }
            catch (Exception ex)
            {
                if (bRaise) { _ = MessageBox.Show(ex.Message, "エラー発生", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }

            return sts;
        }
        //----------------------------------------------
        //[VB]  'ディレクトリを削除する。
        //[VB]  '
        //[VB]  '引き数：
        //[VB]  'sPath 削除するディレクトリのパス。
        //[VB]  'bRaise Err.Raise の有無。
        //[VB]  '
        //[VB]  '戻り値：
        //[VB]  '正常終了の場合 True を返す。
        //[VB]  'それ以外の場合 False を返す。
        //[VB]  Public Function DeleteDir(ByVal sPath As String, Optional ByVal bRaise As Boolean = False) As Boolean
        //[VB]      DeleteDir = False
        //[VB]      On Error GoTo FileErrorHandler
        //[VB]      Dim clsFind As New FileFind
        //[VB]      If clsFind.FindFile(RTrimEx(sPath, "\")) Then
        //[VB]          If Not EmptyDir(sPath, bRaise) Then Exit Function
        //[VB]          Call RmDir(sPath)
        //[VB]      End If
        //[VB]      DeleteDir = True
        //[VB]      Exit Function
        //[VB]  FileErrorHandler:
        //[VB]      If bRaise Then Call Err.Raise(Err.Number, Err.Source, Err.Description, Err.HelpFile, Err.HelpContext)
        //[VB]  End Function


        //***************************************************************************
        /// <summary>
        /// Variant型(VerType8)のバイナリファイルデータをstring型で読み込む
        /// （VB GetString関数をVC用に変更）
        ///  '引き数：
        /// バイナリファイル
        /// </summary>
        /// <param name="BinaryReader br">
        /// </param>
        /// <returns>
        /// 戻り値:returns = string型の読み込みデータ
        /// </returns>
        //***************************************************************************
        public static string FileRead_GetString(BinaryReader br)
        {
            long vertype = br.ReadInt16();
            long size = br.ReadInt16();
            string sGetData = "";

            if (vertype == 8 && size > 0)
            {
                _ = new byte[size];
                byte[] data = br.ReadBytes((int)size);
                sGetData = Encoding.GetEncoding("shift_jis").GetString(data);

            }
            return sGetData;
        }
        //***************************************************************************
        /// <summary>
        /// 指定されたパスが"."、もしくは".."であるか検査する。
        ///     （VB IsDots 関数をVC用に変更）
        /// 引き数：
        /// sPath 検査対象のパス。
        /// </summary>
        /// <param name="sPath">
        /// </param>
        /// <returns>
        /// 戻り値:returns = "."、もしくは".."である場合 True を返す。
        ///                  それ以外の場合 False を返す
        /// </returns>
        //***************************************************************************
        //[VB] Public Function IsDots(ByVal sPath As String) As Boolean
        public static bool IsDots(string sPath)
        {
            long nLen;
            nLen = sPath.Length;

            if (sPath.Substring((int)(nLen - 1), 0) != ".")
            {
                return false;
            }
            else
            {
                if (nLen < 2) { return true; }
                else
                {
                    if (sPath.Substring((int)(nLen - 2), 0) == ".")
                    {
                        if (nLen < 3) { return true; }
                        else
                        {
                            return sPath.Substring((int)(nLen - 3), 0) == "\\"; ;
                        }
                    }
                    else
                    {
                        return sPath.Substring((int)(nLen - 2), 0) == "\\";
                    }
                }
            }
        }
        // Public Function IsDots(ByVal sPath As String) As Boolean
        //    Dim nLen As Long
        //    nLen = Len(sPath)
        //    If Mid$(sPath, nLen, 1) <> "." Then
        //        IsDots = False
        //    Else
        //        If nLen< 2 Then
        //            IsDots = True
        //        Else
        //            If Mid$(sPath, nLen - 1, 1) = "." Then
        //                If nLen< 3 Then
        //                    IsDots = True
        //                Else
        //                    If Mid$(sPath, nLen - 2, 1) = "\" Then
        //                        IsDots = True
        //                    Else
        //                        IsDots = False
        //                    End If
        //               End If
        //            ElseIf Mid$(sPath, nLen - 1, 1) = "\" Then
        //                IsDots = True
        //            Else
        //                IsDots = False
        //            End If
        //        End If
        //    End If
        //End Function



        //***************************************************************************
        //***************************************************************************
        /// <summary>
        ///'nDec で指定された少数点以下桁数で四捨五入する。
        ///'
        ///'引き数：
        ///'nVal 値。
        ///'nDec 少数点以下桁数。
        /// </summary>
        /// <param name="nVa"></param>
        /// <param name="nDec"></param>
        /// <returns>
        /// 戻り値：
        ///     指定された少数点以下桁数で四捨五入
        /// </returns>
        //***************************************************************************
        public static double JpnRound(double nVa, long nDec)
        {

            double ans = 0;


            //nFig = 10 ^ nDec;

            //nVa = nVa * nFig;

            if (nVa == 0)
            {
                return ans;
            }
            ans = (double)Math.Round(nVa, (int)nDec);


            return ans;

        }
        //--------------------------------------------------------------------------
        //'四捨五入。
        //'
        //'nDec で指定された少数点以下桁数で四捨五入する。
        //'
        //'引き数：
        //'nVal 値。
        //'nDec 少数点以下桁数。
        //'
        //'戻り値：結果値。
        //Public Function JpnRound(ByVal nVal As Double, ByVal nDec As Long) As Double
        //'    Dim nFig As Double
        //'    nFig = 10 ^ nDec
        //'    JpnRound = Fix(nVal * nFig + 0.5 * Sgn(nVal)) / nFig
        //    Dim sFormat As String
        //    sFormat = "0." & String(nDec, "0")
        //    JpnRound = Val(Format$(nVal, sFormat))
        //End Function
        //***************************************************************************
        //***************************************************************************
        //***************************************************************************



    }


}
