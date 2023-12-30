using SurvLine.mdl;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
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




namespace SurvLine
{

    public class MdlUtility
    {
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
        public bool DirFileAllCopy(string sSrcPath, string sDstPath, bool bRaise)
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
                    string fileName = System.IO.Path.Combine(sSrcPath, fName);


                    // Use the Path.Combine method to safely append the file name to the path.
                    // Will overwrite if the destination file already exists.
                    System.IO.File.Copy(Path.Combine(sSrcPath, fName), Path.Combine(sDstPath, fName), true);
                }
                sts = true;

            }
            catch (Exception ex)
            {
                if (bRaise) { MessageBox.Show(ex.Message, "エラー発生"); }
            }


            return sts;
        }
        //***************************************************************************
        //***************************************************************************


        //***************************************************************************
        //***************************************************************************
        /// <summary>
        /// 文字列の末尾から、任意の文字を省く。
        /// （VB RTrimEx関数をVC用に変更）
        ///  引き数：
        ///     sValue 文字列。
        ///     sTarget 省く文字。
        //      bRaise Err.Raise の有無。
        /// </summary>
        /// <param name="sSrcPath">
        /// <param name="sDstPath">
        /// <param name="bRaiser">
        /// </param>
        /// <returns>
        /// 戻り値:returns = 文字列
        /// </returns>
        //***************************************************************************
        //[VB]  Public Function RTrimEx(ByVal sValue As String, ByVal sTarget As String) As String
        public string RTrimEx( string sValue, string sTarget)
        {
            //文字末尾が"\"の場合は、文字から削除する
            if (sValue.EndsWith(sTarget))
            {
                sValue = sValue.Remove(sValue.Length - 1);

            }
            return sValue;
        }
        //----------------------------------------------
        //[VB]  '文字列の末尾から、任意の文字を省く。
        //[VB]  '
        //[VB]  '引き数：
        //[VB]  'sValue 文字列。
        //[VB]  'sTarget 省く文字。
        //[VB]  '
        //[VB]  '戻り値：結果文字列。
        //[VB]  Public Function RTrimEx(ByVal sValue As String, ByVal sTarget As String) As String
        //[VB]      Dim nValue() As Byte
        //[VB]      Dim nTarget() As Byte
        //[VB]      Dim i As Long
        //[VB]      nValue = sValue
        //[VB]      nTarget = Left$(sTarget, 1)
        //[VB]      For i = UBound(nValue) To 0 Step -2
        //[VB]          If nValue(i) <> nTarget(1) Or nValue(i - 1) <> nTarget(0) Then Exit For
        //[VB]      Next
        //[VB]      RTrimEx = LeftB$(sValue, i + 1)
        //[VB]  End Function
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
        public bool CopyDir(string sSrcPath, string sDstPath, bool bRaise = false)
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


                if (System.IO.Directory.Exists(sDstPath) == false)
                {
                    //コピー先ディレクトリが無いので、作成する
                    System.IO.Directory.CreateDirectory(sDstPath);
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
                if (bRaise) { MessageBox.Show(ex.Message, "エラー発生"); }
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
        public bool DeleteDir(string sPath, bool bRaise)
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
                if (bRaise) { MessageBox.Show(ex.Message, "エラー発生"); }
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
        public string FileRead_GetString(BinaryReader br)
        {
            long vertype = br.ReadInt16();
            long size = br.ReadInt16();
            string sGetData = "";

            if (vertype == 8 && size > 0)
            {
                byte[] data = new byte[size];
                data = br.ReadBytes((int)size);
                sGetData = System.Text.Encoding.GetEncoding("shift_jis").GetString(data);

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
        public bool IsDots(string sPath)
        {
            long nLen;
            nLen = sPath.Length;

            if( sPath.Substring((int)(nLen - 1), 0) != ".")
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
                            if (sPath.Substring((int)(nLen - 3), 0) == "\\") { return true; }
                            else { return false; };
                        }
                    }
                    else{
                        if (sPath.Substring((int)(nLen - 2), 0) == "\\") { return true; }
                        else { return false; }
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
        /// <returns></returns>
        //***************************************************************************
        public double JpnRound(double nVa, long nDec)
        {

            double ans = 0;
            //double nFig;


            //nFig = 10 ^ nDec;

            //nVa = nVa * nFig;

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

        //23/12/29 K.setoguchi@NV---------->>>>>>>>>>
        //***************************************************************************
        //***************************************************************************
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
                //[VB]    sPath = RTrimEx(sPath, "\")
                //[VB]    If Right$(sPath, 1) = ":" Then Exit Function
                //[VB]    Dim clsFind As New FileFind
                sPath = RTrimEx(sPath, "\\");
                if (mdiVBfunctions.Right(sPath, 1) == ":")
                {
                    return CreateDir;
                }


                //[VB]    Dim clsFind As New FileFind
                //[VB]    If Not clsFind.FindFile(sPath) Then   //"C:\Develop\NetSurv\Src\NS-App\NS-Survey\UserData"
                //[VB]        Dim sDrive As String
                //[VB]        Dim sDir As String
                //[VB]        Dim sTitle As String
                //[VB]        Dim sExt As String
                //[VB]        Call SplitPath(sPath, sDrive, sDir, sTitle, sExt) 
                //[VB]
                //                                              sPath = "C:\Develop\NetSurv\Src\NS-App\NS-Survey\UserData2"
                //                                              sDrive = "C:" 
                //                                              sDir   = "\Develop\NetSurv\Src\NS-App\NS-Survey\" 
                //                                              sTitle = "UserData2"
                //                                              sExt = "" 
                //
                //[VB]        If Not CreateDir(sDrive & sDir, bRaise) Then Exit Function
                //[VB]        Call MkDir(sPath)
                //[VB]    End If
                //
                if (!System.IO.Directory.Exists(sPath))
                {
                    //存在しない場合は、フォルダを作成
                    //      ※　VC#では、複数のフォルダを一気に作成できる
                    System.IO.Directory.CreateDirectory(sPath);

                    CreateDir = true;
                    bRaise = true;
                }
                else
                {
                    CreateDir = true;
                    CreateDir = true;
                }

            }
            catch (Exception e)
            {
                CreateDir = false;
                bRaise = false;
            }

            return CreateDir;

        }

        //'ディレクトリを作成する。
        //'
        //'引き数：
        //'sPath 作成するディレクトリのパス。
        //'bRaise Err.Raise の有無。
        //'
        //'戻り値：
        //'正常終了の場合 True を返す。
        //'それ以外の場合 False を返す。
        //Public Function CreateDir(ByVal sPath As String, Optional ByVal bRaise As Boolean = False) As Boolean
        //    CreateDir = False
        //    On Error GoTo FileErrorHandler
        //    sPath = RTrimEx(sPath, "\")
        //    If Right$(sPath, 1) = ":" Then Exit Function
        //    Dim clsFind As New FileFind
        //    If Not clsFind.FindFile(sPath) Then
        //        Dim sDrive As String
        //        Dim sDir As String
        //        Dim sTitle As String
        //        Dim sExt As String
        //        Call SplitPath(sPath, sDrive, sDir, sTitle, sExt)
        //        If Not CreateDir(sDrive & sDir, bRaise) Then Exit Function
        //        Call MkDir(sPath)
        //    End If
        //    CreateDir = True
        //    Exit Function
        //FileErrorHandler:
        //    If bRaise Then Call Err.Raise(Err.Number, Err.Source, Err.Description, Err.HelpFile, Err.HelpContext)
        //End Function
        //***************************************************************************
        //***************************************************************************
        //<<<<<<<<<-----------23/12/29 K.setoguchi@NV



        public void SplitPath(string sPath, string sDrive, string sDir, string sTitle, string sExt)
        {

        }

        //'パス名を構成要素に分解する。
        //'
        //'引き数：
        //'sPath パス。
        //'sDrive ドライブ。"C:"
        //'sDir フォルダパス。"\AAA\BBB\"
        //'sTitle ファイルタイトル。"CCC"
        //'sExt 拡張子。".DDD"
        //Public Sub SplitPath(ByVal sPath As String, ByRef sDrive As String, ByRef sDir As String, ByRef sTitle As String, ByRef sExt As String)
        //    'ディレクトリ位置。
        //    Dim nPosDir As Long
        //    nPosDir = InStrRev(sPath, ":") + 1
        //    'ファイルタイトル位置。
        //    Dim nPosTitle As Long
        //    nPosTitle = InStrRev(sPath, "\") + 1
        //    If nPosTitle <= 1 Then nPosTitle = nPosDir
        //    '拡張子位置。
        //    Dim nPosExt As Long
        //    nPosExt = InStrRev(sPath, ".")
        //    If nPosExt < nPosTitle Then nPosExt = Len(sPath) + 1
        //    '拡張子。
        //    If nPosExt < nPosTitle Then
        //        sExt = ""
        //    Else
        //        sExt = Mid$(sPath, nPosExt)
        //    End If
        //    'ファイルタイトル。
        //    sTitle = Mid$(sPath, nPosTitle, nPosExt - nPosTitle)
        //    'ディレクトリパス。
        //    sDir = Mid$(sPath, nPosDir, nPosTitle - nPosDir)
        //    'ドライブ。
        //    sDrive = Mid$(sPath, 1, nPosDir - 1)
        //End Sub


    }



}
