using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
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


    }


}
