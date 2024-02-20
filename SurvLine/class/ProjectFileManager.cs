using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using System.Windows.Forms;
using System.Linq.Expressions;
using System.Xml.Linq;
using System.IO;
using System.Reflection.Emit;
using static System.Net.WebRequestMethods;
using System.Runtime.Remoting;
using System.Net.NetworkInformation;
using System.Net.Security;
using static System.Windows.Forms.AxHost;
using SurvLine.mdl;
using System.Drawing;
using static SurvLine.mdl.MdiDefine;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;
using System.Data.Common;
using System.Security.Cryptography;

namespace SurvLine
{

    public class ProjectFileManager
    {

        //23/12/29 K.setoguchi@NV---------->>>>>>>>>>
        MdlUtility mdlUtility = new MdlUtility();



        //'*******************************************************************************
        //'プロジェクトファイル管理
        //
        //Option Explicit
        //
        //'定数
        private const string DATA_FOLDER_FORMAT = "0000";       // As String 'データフォルダ書式。
        private const long MAX_PROJECT_NUMBER = 999;            // As Long '最大プロジェクト番号。
        //
        //'現場リストカラム番号。
        private enum COL_NUM_PROJECTLIST
        {
            COL_NUM_PROJECTLIST_CHECK = 0,      //'チェックボックス。
            COL_NUM_PROJECTLIST_JOBNAME,        //'現場名。
            COL_NUM_PROJECTLIST_DISTRICTNAME,   //'地区名。
            COL_NUM_PROJECTLIST_FOLDER,         //'フォルダ。
            COL_NUM_PROJECTLIST_MDATE,          //'最終更新日。
            COL_NUM_PROJECTLIST_CDATE,          //'作成日。
            COL_NUM_PROJECTLIST_COUNT,          //'番号数。
        }

        //<<<<<<<<<-----------23/12/29 K.setoguchi@NV

        //==========================================================================================
        /*[VB]
            //Private Const COL_NAM_PROJECTLIST_CHECK As String = "" '現場リストカラム名称、チェックボックス。
            //Private Const COL_NAM_PROJECTLIST_JOBNAME As String = "現場名" '現場リストカラム名称、現場名。
            //Private Const COL_NAM_PROJECTLIST_DISTRICTNAME As String = "地区名" '現場リストカラム名称、地区名。
            //Private Const COL_NAM_PROJECTLIST_FOLDER As String = "フォルダ" '現場リストカラム名称、フォルダ。
            //Private Const COL_NAM_PROJECTLIST_MDATE As String = "最終更新日" '現場リストカラム名称、最終更新日。
            //Private Const COL_NAM_PROJECTLIST_CDATE As String = "作成日" '現場リストカラム名称、作成日。

            //Private Const COL_WID_PROJECTLIST_CHECK As Long = 384      '現場リストカラム幅(Twips)、チェックボックス。
            //Private Const COL_WID_PROJECTLIST_JOBNAME As Long = 1620   '現場リストカラム幅(Twips)、現場名。
            //Private Const COL_WID_PROJECTLIST_DISTRICTNAME As Long =1620'現場リストカラム幅(Twips)、地区名。
            //Private Const COL_WID_PROJECTLIST_FOLDER As Long = 900      '現場リストカラム幅(Twips)、フォルダ。
            //Private Const COL_WID_PROJECTLIST_MDATE As Long = 1700      '現場リストカラム幅(Twips)、最終更新日。
            //Private Const COL_WID_PROJECTLIST_CDATE As Long = 1700      '現場リストカラム幅(Twips)、作成日。
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]

        private const string COL_NAM_PROJECTLIST_CHECK = "";               //'現場リストカラム名称、チェックボックス。
        private const string COL_NAM_PROJECTLIST_JOBNAME = "現場名";       //'現場リストカラム名称、現場名。
        private const string COL_NAM_PROJECTLIST_DISTRICTNAME = "地区名";  // '現場リストカラム名称、地区名。
        private const string COL_NAM_PROJECTLIST_FOLDER = "フォルダ";      //'現場リストカラム名称、フォルダ。
        private const string COL_NAM_PROJECTLIST_MDATE = "最終更新日";     //'現場リストカラム名称、最終更新日。
        private const string COL_NAM_PROJECTLIST_CDATE = "作成日";         //'現場リストカラム名称、作成日。

        private const int COL_WID_PROJECTLIST_CHECK = 384 / 19;              //'現場リストカラム幅(Twips)、チェックボックス。
        private const int COL_WID_PROJECTLIST_JOBNAME = 1620 / 9;           //'現場リストカラム幅(Twips)、現場名。
        private const int COL_WID_PROJECTLIST_DISTRICTNAME = 1620 / 9;      //'現場リストカラム幅(Twips)、地区名。。
        private const int COL_WID_PROJECTLIST_FOLDER = 900 / 9;             //'現場リストカラム幅(Twips)、フォルダ。
        private const int COL_WID_PROJECTLIST_MDATE = 1400 / 9;             //'現場リストカラム幅(Twips)、最終更新日。
        private const int COL_WID_PROJECTLIST_CDATE = 1400 / 9;             //'現場リストカラム幅(Twips)、作成日。


        //***************************************************************************
        /// <summary>
        /// 現場リスト数を算出。
        //  '引き数：
        //  無し
        /// </summary>
        /// <param name=""></param>
        /// <returns>
        /// </returns>
        //***************************************************************************
        public int GetProjectListCount()
        {
            int listcount = 0; //0:データなし

            var main = new FRM_MAIN();
            //仮---->>>>
            main.UserDataPath = @"C:\Develop\NetSurv\Src\NS-App\NS-Survey\UserData\";
            //<<<<----仮

            string sPath = main.UserDataPath;

            //フォルダの有無を確認
            if (sPath != null)
            {
                if (System.IO.Directory.Exists(sPath) == false)
                {
                    MessageBox.Show($"フォルダ {sPath} が見つかりました");
                }
            }
            // サブフォルダ名を取得
            var files = System.IO.Directory.GetDirectories(sPath);
            if (files == null)
            {
                MessageBox.Show($"フォルダ {sPath} 内のサブフォルダが見つかりました");
            }
            //******************************************
            // サブフォルダ名を数を算出
            //******************************************
            foreach (var file in files)
            {
                listcount++;
            }

            return listcount;
        }





        //23/12/26 K.setoguchi@NV---------->>>>>>>>>>
        //***************************************************************************
        //***************************************************************************



        //'*******************************************************************************
        //'メソッド


        //==========================================================================================
        /*[VB]
            'プロジェクトのリストを取得する。
            '
            '現在保存されているプロジェクトの一覧情報を取得する。
            '
            '引き数：
            'sJobNames 現場名が設定される。配列の要素は(-1 To ...)、要素 -1 は未使用。
            'sDistrictNames 地区名が設定される。配列の要素は(-1 To ...)、要素 -1 は未使用。
            'sFolderNames フォルダ名が設定される。配列の要素は(-1 To ...)、要素 -1 は未使用。
            'tModTime 更新日時が設定される。配列の要素は(-1 To ...)、要素 -1 は未使用。
            'tCreateTime 作成日時が設定される。配列の要素は(-1 To ...)、要素 -1 は未使用。
            Public Sub GetProjectList(ByRef sJobNames() As String, ByRef sDistrictNames() As String, ByRef sFolderNames() As String, ByRef tModTime() As Date, ByRef tCreateTime() As Date)

                ReDim sJobNames(-1 To -1)
                ReDim sDistrictNames(-1 To -1)
                ReDim sFolderNames(-1 To -1)
                ReDim tModTime(-1 To -1)
                ReDim tCreateTime(-1 To -1)


                Dim clsFind As New FileFind
                Dim nUBound As Long
                Dim sPath As String
                Dim sJobName As String
                Dim sDistrictName As String
                Dim sFolderName As String
                nUBound = -1
                sPath = frmMain.UserDataPath  '2008/10/13 NGS Yamada
            '    sPath = App.Path & DATA_FOLDER_NAME
                If Not clsFind.FindFile(sPath & "*") Then Exit Sub

                Do
                    If(clsFind.Attributes And FILE_ATTRIBUTE_DIRECTORY) = 0 Then GoTo ContinueHandler
                    sFolderName = clsFind.Name
                    If IsDots(sFolderName) Then GoTo ContinueHandler
                    If Not ReadInfo(sPath & sFolderName & "\" & DATA_FILE_NAME, sJobName, sDistrictName) Then GoTo ContinueHandler
                    nUBound = nUBound + 1
                    ReDim Preserve sJobNames(-1 To nUBound)
                    ReDim Preserve sDistrictNames(-1 To nUBound)
                    ReDim Preserve sFolderNames(-1 To nUBound)
                    ReDim Preserve tModTime(-1 To nUBound)
                    ReDim Preserve tCreateTime(-1 To nUBound)
                    sJobNames(nUBound) = sJobName
                    sDistrictNames(nUBound) = sDistrictName
                    sFolderNames(nUBound) = sFolderName
                    Dim clsFind2 As FileFind
                    Set clsFind2 = New FileFind
                    If clsFind2.FindFile(sPath & sFolderName & "\" & DATA_FILE_NAME) Then
                        tModTime(nUBound) = clsFind2.LastWriteTime
                        tCreateTime(nUBound) = clsFind2.CreationTime
                    End If
            ContinueHandler:
                Loop While clsFind.FindNext()

            End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //***************************************************************************
        /// <summary>
        /// 現場リスト情報を取得
        //  '引き数：
        //  現場リスト領域
        /// </summary>
        /// <param name="Genba"></param>
        /// <param name="nUBound"></param>
        /// <returns>
        /// </returns>
        //***************************************************************************
        public void GetProjectList(GENBA_STRUCT[] Genba, int nUBound, ref int datacount)
        {

            string sPath;
            string sFolderName;
            string sFileName;
            string sWork;


            var main = new FRM_MAIN();
            //仮---->>>>
            main.UserDataPath = @"C:\Develop\NetSurv\Src\NS-App\NS-Survey\UserData\";
            //<<<<----仮


            sPath = main.UserDataPath;


            //******************************************
            // サブフォルダ名を取得
            //******************************************
            var files = System.IO.Directory.GetDirectories(sPath);


            //  //******************************************
            //  // 固定のファイル名を設定
            //  //******************************************
            //  sFileName = GENBA_CONST.DATA_FILE_NAME;
            //  //******************************************
            //  // サブフォルダ名を保存
            //  //******************************************
            //  int i = 0;
            //
            //  foreach (var file in files)
            //  {
            //  //  If(clsFind.Attributes And FILE_ATTRIBUTE_DIRECTORY) = 0 Then GoTo ContinueHandler
            //
            //      //******************************************
            //      sFolderName = file;
            //      //******************************************
            //
            //      //******************************************
            //      //  サブフォルダ名をグローバル領域に設定
            //      //******************************************
            //      Genba[i].sFolderNames = System.IO.Path.GetFileName(sFolderName);
            //      //******************************************
            //      //  最終更新日をグローバル領域に設定
            //      //******************************************
            //      Genba[i].tModTime = System.IO.File.GetLastWriteTime($"{sFolderName}\\{sFileName}");
            //      //******************************************
            //      //画面表示＜作成日  .ToString("yyyy/MM/dd HH:mm")＞
            //      //******************************************
            //      Genba[i].tCreateTime = System.IO.File.GetCreationTime($"{sFolderName}\\{sFileName}");
            //      i++;
            //  }

            //-- VB >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
            //  Do
            //      If(clsFind.Attributes And FILE_ATTRIBUTE_DIRECTORY) = 0 Then GoTo ContinueHandler
            //      sFolderName = clsFind.Name
            //      If IsDots(sFolderName) Then GoTo ContinueHandler
            //      If Not ReadInfo(sPath &sFolderName & "\" & DATA_FILE_NAME, sJobName, sDistrictName) Then GoTo ContinueHandler
            //      nUBound = nUBound + 1
            //      ReDim Preserve sJobNames(-1 To nUBound)
            //      ReDim Preserve sDistrictNames(-1 To nUBound)
            //      ReDim Preserve sFolderNames(-1 To nUBound)
            //      ReDim Preserve tModTime(-1 To nUBound)
            //      ReDim Preserve tCreateTime(-1 To nUBound)
            //      sJobNames(nUBound) = sJobName
            //      sDistrictNames(nUBound) = sDistrictName
            //      sFolderNames(nUBound) = sFolderName
            //      Dim clsFind2 As FileFind
            //      Set clsFind2 = New FileFind
            //      If clsFind2.FindFile(sPath & sFolderName & "\" & DATA_FILE_NAME) Then
            //         tModTime(nUBound) = clsFind2.LastWriteTime
            //         tCreateTime(nUBound) = clsFind2.CreationTime
            //      End If
            //ContinueHandler:
            //      Loop While clsFind.FindNext()
            //-- VB <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
            string sJobName = "";
            string sDistrictName = "";

            //------------------------------------------
            MdlUtility Utility = new MdlUtility();


            //******************************************************************
            //パス内の全（フォルダ＋ファイル名＆読み込み）
            //******************************************************************
            int i = 0;
            datacount = 0;
            //------------------
            do
            {
                //------------------------------------------
                //[VB]  If(clsFind.Attributes And FILE_ATTRIBUTE_DIRECTORY) = 0 Then GoTo ContinueHandler
                FileAttributes attr = System.IO.File.GetAttributes(sPath);
                if (((long)attr & DEFINE.FILE_ATTRIBUTE_DIRECTORY) == 0)
                {
                    goto ContinueHandler;
                }
                //------------------------------------------

                //------------------------------------------
                //フォルダ名を設定   
                //------------------------------------------
                sFolderName = files[i];

                //------------------------------------------
                //[VB]  If IsDots(sFolderName) Then GoTo ContinueHandler
                if (Utility.IsDots(sFolderName))
                {
                    goto ContinueHandler;
                }
                //------------------------------------------
                //[VB]      If Not ReadInfo(sPath &sFolderName & "\" & DATA_FILE_NAME, sJobName, sDistrictName) Then GoTo ContinueHandler
                if (!(ReadInfo($"{sFolderName}\\{GENBA_CONST.DATA_FILE_NAME}", ref sJobName, ref sDistrictName)))
                {
                    goto ContinueHandler;
                }
                //------------------------------------------
                //[VB]以降は、まとめてVC#対応を行います
                //------------------------------------------
                //[VB]      nUBound = nUBound + 1
                //[VB]      ReDim Preserve sJobNames(-1 To nUBound)
                //[VB]      ReDim Preserve sDistrictNames(-1 To nUBound)
                //[VB]      ReDim Preserve sFolderNames(-1 To nUBound)
                //[VB]      ReDim Preserve tModTime(-1 To nUBound)
                //[VB]      ReDim Preserve tCreateTime(-1 To nUBound)
                //[VB]      sJobNames(nUBound) = sJobName
                //[VB]      sDistrictNames(nUBound) = sDistrictName
                //[VB]      sFolderNames(nUBound) = sFolderName
                //[VB]      Dim clsFind2 As FileFind
                //[VB]      Set clsFind2 = New FileFind
                //[VB]      If clsFind2.FindFile(sPath & sFolderName & "\" & DATA_FILE_NAME) Then
                //[VB]         tModTime(nUBound) = clsFind2.LastWriteTime
                //[VB]         tCreateTime(nUBound) = clsFind2.CreationTime
                //[VB]     End If

                //******************************************
                //  現場名をグローバル領域に設定
                //******************************************
                Genba[datacount].sJobNames = sJobName;

                //******************************************
                //  地区名をグローバル領域に設定
                //******************************************
                Genba[datacount].sDistrictNames = sDistrictName;

                //------------------------------------------
                //ファイル名を設定   
                //------------------------------------------
                //[VB]      sJobNames(nUBound) = sJobName
                sFileName = GENBA_CONST.DATA_FILE_NAME;



                //------------------------------------------
                //  サブフォルダ名をグローバル領域に設定
                //------------------------------------------
                Genba[datacount].sFolderNames = System.IO.Path.GetFileName(sFolderName);

                //******************************************
                //  最終更新日をグローバル領域に設定
                //******************************************
                Genba[datacount].tModTime = System.IO.File.GetLastWriteTime($"{sFolderName}\\{sFileName}");

                //******************************************
                //  画面表示＜作成日  .ToString("yyyy/MM/dd HH:mm")＞
                //******************************************
                Genba[datacount].tCreateTime = System.IO.File.GetCreationTime($"{sFolderName}\\{sFileName}");

                datacount++;

            //                //------------------------------------------
            //                //読み込み用でファイルをオープン           
            //                //------------------------------------------
            //                using (var fs = System.IO.File.OpenRead($"{sPath}\\{Genba[i].sFolderNames}\\{sFileName}"))
            //                {
            //                    using (var br = new BinaryReader(fs))
            //                    {
            //                        // ファイルの長さだけ読み込む
            //                        int count = (int)fs.Length;
            //                       count = 255;
            //                        byte[] data;
            //
            //                        //******************************************
            //                        //nVersion
            //                        //******************************************
            //                        long nVersion = br.ReadInt32();
            //
            //                        //******************************************
            //                        //その他の情報を読み込み
            //                        //******************************************
            //
            //                        //******************************************
            //                        //  現場名をグローバル領域に設定
            //                        //******************************************
            //                        Genba[i].sJobNames = Utility.FileRead_GetString(br);
            //
            //                        //******************************************
            //                        //  地区名をグローバル領域に設定
            //                        //******************************************
            //                        Genba[i].sDistrictNames = Utility.FileRead_GetString(br);

            //                      //DEBUG>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
            //                      //**************
            //                      //*Byte配列を定義
            //                      //**************
            //                      //data = br.ReadBytes(count);
            //                      //public BinaryReader(Stream input, Encoding encoding)
            //
            //                      //ASCII エンコード
            //                      //    string text = System.Text.Encoding.ASCII.GetString(data);
            //                      //データがEUCの場合
            //                      //    string text3 = System.Text.Encoding.GetEncoding("euc-jp").GetString(data);
            //                      //データがunicodeの場合
            //                      //    string text4 = System.Text.Encoding.Unicode.GetString(data);
            //                      //データがutf-8の場合
            //                      //    string text5 = System.Text.Encoding.UTF8.GetString(data);
            //                      //DEBUG>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

            //DEBUG>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
            //MessageBox.Show($"{i + 1}：行|{Genba[i].sJobNames} | {Genba[i].sFolderNames} | {Genba[i].tModTime} | {Genba[i].tCreateTime}");
            //MessageBox.Show($"{i + 1}：行 | {Genba[i].sJobNames}");
            //MessageBox.Show($"{i + 1}：行 | {Genba[i].sFolderNames}");
            //MessageBox.Show($"{i + 1}：行＜最終更新日＞| {Genba[i].tModTime.ToString("yyyy/MM/dd HH:mm")}");
            //MessageBox.Show($"{i + 1}：行＜作成日＞| {Genba[i].tCreateTime.ToString("yyyy/MM/dd HH:mm")}");
            //DEBUG<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<

            //DEBUG>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
            //StreamReader sr = new StreamReader($"{sPath}\\{Genba[i].sFolderNames}\\{sFileName}");
            // CSVファイルの一行を読み込む
            //string line = sr.ReadLine();
            // 読み込んだ一行をカンマ毎に分けて配列に格納する
            //string[] values = line.Split('\\');
            //                sFolderName = sr.ReadLine();
            //                //sFileName = sr.s();
            //                string[] values = line.Split('');
            //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<DEBUG
            //    }
            //}
            ContinueHandler:
                i++;

            } while (i < nUBound);

        }
        //23/12/26 K.setoguchi@NV---------->>>>>>>>>>
        /// <summary>
        /// 'メソッド
        ///
        /// 'プロジェクトのリストを取得する。
        /// '
        /// '現在保存されているプロジェクトの一覧情報を取得する。
        /// '
        /// '引き数：
        ///     'sJobNames 現場名が設定される。配列の要素は(-1 To ...)、要素 -1 は未使用。
        ///     'sDistrictNames 地区名が設定される。配列の要素は(-1 To ...)、要素 -1 は未使用。
        ///     'sFolderNames フォルダ名が設定される。配列の要素は(-1 To ...)、要素 -1 は未使用。
        ///     'tModTime 更新日時が設定される。配列の要素は(-1 To ...)、要素 -1 は未使用。
        ///     'tCreateTime 作成日時が設定される。配列の要素は(-1 To ...)、要素 -1 は未使用。
        /// </summary>
        /// <param name="sJobNames"></param>
        /// <param name="sDistrictNames"></param>
        /// <param name="sFolderNames"></param>
        /// <param name="tModTime"></param>
        /// <param name="tCreateTime"></param>
        /// <returns></returns>
        public void GetProjectList(ref List<string> sJobNames, ref List<string> sDistrictNames, ref List<string> sFolderNames, ref List<DateTime> tModTime, ref List<DateTime> tCreateTime)
        {

            //*******************************
            //
            //  パラメータ変更　瀬戸口健二　
            //
            //*******************************

            //    Dim clsFind As New FileFind
            //    Dim nUBound As Long
            //    Dim sPath As String
            //    Dim sJobName As String
            //    Dim sDistrictName As String
            //    Dim sFolderName As String
            //    nUBound = -1
            //    sPath = frmMain.UserDataPath  '2008/10/13 NGS Yamada
            //'    sPath = App.Path & DATA_FOLDER_NAME
            //    If Not clsFind.FindFile(sPath & "*") Then Exit Sub
            long nUBound;
            string sPath;
            string sJobName = "";
            string sDistrictName = "";
            string sFolderName;

            string sWork;


            var main = new FRM_MAIN();
            //仮---->>>>
            main.UserDataPath = @"C:\Develop\NetSurv\Src\NS-App\NS-Survey\UserData\";
            //<<<<----仮


            sPath = main.UserDataPath;


            //******************************************
            // サブフォルダ名を取得
            //******************************************
            var files = System.IO.Directory.GetDirectories(sPath);


            //------------------------------------------
            MdlUtility Utility = new MdlUtility();


            //******************************************************************
            //パス内の全（フォルダ＋ファイル名＆読み込み）
            //******************************************************************
            int i = 0;
            int datacount = 0;
            nUBound = 1;

            //------------------
            foreach (var file in files)
            {
                //------------------------------------------
                //[VB]  If(clsFind.Attributes And FILE_ATTRIBUTE_DIRECTORY) = 0 Then GoTo ContinueHandler
                FileAttributes attr = System.IO.File.GetAttributes(sPath);
                if (((long)attr & DEFINE.FILE_ATTRIBUTE_DIRECTORY) == 0)
                {
                    goto ContinueHandler2;
                }
                //------------------------------------------

                //------------------------------------------
                //フォルダ名を設定   
                //------------------------------------------
                sFolderName = files[i];

                //------------------------------------------
                //[VB]  If IsDots(sFolderName) Then GoTo ContinueHandler
                if (Utility.IsDots(sFolderName))
                {
                    goto ContinueHandler2;
                }
                //------------------------------------------
                //[VB]      If Not ReadInfo(sPath &sFolderName & "\" & DATA_FILE_NAME, sJobName, sDistrictName) Then GoTo ContinueHandler
                if (!(ReadInfo($"{sFolderName}\\{GENBA_CONST.DATA_FILE_NAME}", ref sJobName, ref sDistrictName)))
                {
                    goto ContinueHandler2;
                }

                //******************************************
                //  現場名をグローバル領域に設定
                //******************************************
                sJobNames.Add(sJobName);


                //******************************************
                //  地区名をグローバル領域に設定
                //******************************************
                sDistrictNames.Add(sDistrictName);

                //------------------------------------------
                //ファイル名を設定   
                //------------------------------------------
                //[VB]      sJobNames(nUBound) = sJobName
                string sFileName = GENBA_CONST.DATA_FILE_NAME;



                //------------------------------------------
                //  サブフォルダ名をグローバル領域に設定
                //------------------------------------------
                sFolderNames.Add(System.IO.Path.GetFileName(sFolderName));


                //******************************************
                //  最終更新日をグローバル領域に設定
                //******************************************
                tModTime.Add(System.IO.File.GetLastWriteTime($"{sFolderName}\\{sFileName}"));


                //******************************************
                //  画面表示＜作成日  .ToString("yyyy/MM/dd HH:mm")＞
                //******************************************
                tCreateTime.Add(System.IO.File.GetLastWriteTime($"{sFolderName}\\{sFileName}"));

                datacount++;

            ContinueHandler2:
                i++;

            }

        }







        //***************************************************************************
        //***************************************************************************
        //<<<<<<<<<-----------23/12/26 K.setoguchi@NV

        //***************************************************************************
        //==========================================================================================
        /*[VB]
            '新しくプロジェクトフォルダを作成する。
            '
            '空いている番号のフォルダを作成する。
            '
            '引き数：
            '戻り値：
            'フォルダが正常に作成された場合はフォルダパスを返す。
            'それ以外の場合は空文字を返す。
            Public Function CreateProjectFolder() As String
                Dim sDataFolderPath As String
                sDataFolderPath = frmMain.UserDataPath  '2008/10/13 NGS Yamada
            '    sDataFolderPath = App.Path & DATA_FOLDER_NAME
                Call CreateDir(sDataFolderPath, True)
                Dim sPath As String
                Dim clsFind As New FileFind
                Dim i As Long
                For i = 1 To MAX_PROJECT_NUMBER
                    CreateProjectFolder = Format$(i, DATA_FOLDER_FORMAT)
                    sPath = sDataFolderPath & CreateProjectFolder
                    If Not clsFind.FindFile(sPath) Then Exit For
                Next
                If i > MAX_PROJECT_NUMBER Then
                    CreateProjectFolder = ""
                Else
                    Call CreateDir(sPath, True)
                    Call CreateDir(sPath & OBSPOINT_PATH, True)
                End If
            End Function
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /// <summary>
        ///'新しくプロジェクトフォルダを作成する。
        ///'
        ///'空いている番号のフォルダを作成する。
        ///'
        /// </summary>
        /// <returns>
        //'戻り値：
        //'フォルダが正常に作成された場合はフォルダパスを返す。
        //'それ以外の場合は空文字を返す。
        /// </returns>
        public string CreateProjectFolder()
        {
            string CreateProjectFolder = "";


            //[VB]    Dim sDataFolderPath As String
            //[VB]    sDataFolderPath = frmMain.UserDataPath  '2008/10/13 NGS Yamada
            //[VB]'    sDataFolderPath = App.Path & DATA_FOLDER_NAME
            //[VB]    Call CreateDir(sDataFolderPath, True)
            string sDataFolderPath;
            //frmMain2 frmMain2 = new frmMain2(); 
            //sDataFolderPath = frmMain2.UserDataPath;  //'2008/10/13 NGS Yamada
            sDataFolderPath = @"C:\Develop\NetSurv\Src\NS-App\NS-Survey\UserData\";

            MdlUtility mdlUtility = new MdlUtility();
            mdlUtility.CreateDir(sDataFolderPath, true);


            string sPath;
            for (long i = 1; i <= MAX_PROJECT_NUMBER; i++)     //1 - 9999
            {
                CreateProjectFolder = i.ToString("0000");
                sPath = $"{sDataFolderPath}{CreateProjectFolder}";
                if (!System.IO.Directory.Exists($"{sDataFolderPath}{CreateProjectFolder}"))
                {
                    if (i > MAX_PROJECT_NUMBER)
                    {
                        CreateProjectFolder = "";
                    }
                    else
                    {
                        //存在しない場合
                        _ = mdlUtility.CreateDir(sPath, true);
                        _ = mdlUtility.CreateDir($"{sPath}{MdlNSSDefine.OBSPOINT_PATH}", true);
                    }
                    return CreateProjectFolder;

                }
            }
            return CreateProjectFolder;
        }

        //23/12/29 K.setoguchi@NV---------->>>>>>>>>>
        //***************************************************************************




        //==========================================================================================
        /*[VB]
        'プロジェクトフォルダを削除する。
        '
        'sFolderName で指定されるフォルダを削除する。
        '
        '引き数：
        'sFolderName フォルダ名。
        Public Sub DeleteProjectFolder(ByVal sFolderName As String)
            Call DeleteDir(frmMain.UserDataPath & sFolderName, True)
        '    Call DeleteDir(App.Path & DATA_FOLDER_NAME & sFolderName, True)
        End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /// <summary>
        ///'プロジェクトフォルダを削除する。
        ///'
        ///'sFolderName で指定されるフォルダを削除する。
        ///'
        ///'引き数：
        /// 'sFolderName フォルダ名。
        /// 
        /// </summary>
        /// <param name="sFolderName"></param>
        public void DeleteProjectFolder(string sFolderName)
        {
            string UserDataPath = @"C:\Develop\NetSurv\Src\NS-App\NS-Survey\UserData\";

            mdlUtility.DeleteDir($"{UserDataPath}{sFolderName}{MdlNSSDefine.OBSPOINT_PATH}", true);
            mdlUtility.DeleteDir($"{UserDataPath}{sFolderName}", true);
        }



        //24/01/28 K.setoguchi@NV---------->>>>>>>>>>
        //<<<<<<<<<-----------24/01/28 K.setoguchi@NV
        //***************************************************************************
        //***************************************************************************

        //==========================================================================================
        /*[VB]
            'エクスポート(nvz)ファイルをインポートするする。
            '
            'sPath で指定されるエクスポートファイルをインポートする。
            '
            '引き数：
            'sPath エクスポートファイルのパス。
            'clsProgressInterface ProgressInterface オブジェクト。
            Public Sub ImportProjectFolders(ByRef sPath() As String, ByVal clsProgressInterface As ProgressInterface)

                'プログレス。
                Call clsProgressInterface.CheckCancel
    
                '既存の現場名。
                Dim sJobNames() As String
                Dim sDistrictNames() As String
                Dim sFolderNames() As String
                Dim tModTime() As Date
                Dim tCreateTime() As Date
                Call GetProjectList(sJobNames, sDistrictNames, sFolderNames, tModTime, tCreateTime)
                Dim objJobNames As New Collection
                Dim i As Long
                For i = 0 To UBound(sJobNames)
                    Call SetAtCollectionVariant(objJobNames, sJobNames(i), sJobNames(i))
                Next
    
                'プログレス。
                Call clsProgressInterface.CheckCancel


                Dim bAllOK As Boolean
                bAllOK = False
                For i = 0 To UBound(sPath)
                    'テンポラリフォルダを空にする。
                    Call EmptyDir(App.Path & TEMPORARY_PATH & NSCAB_PATH)
        
                    'プログレス。
                    Call clsProgressInterface.CheckCancel
        
                    'ファイルの展開。
                    Dim sImportName As String
                    Dim clsProjectSolidifyer As New ProjectSolidifyer
                    If clsProjectSolidifyer.IsSolid(sPath(i)) Then
                        sImportName = clsProjectSolidifyer.ImportProjectFolder(sPath(i), App.Path & TEMPORARY_PATH & NSCAB_PATH, clsProgressInterface)
                    Else
                        Dim clsProjectCompressor As New ProjectCompressor
                        sImportName = clsProjectCompressor.ImportProjectFolder(sPath(i), App.Path & TEMPORARY_PATH & NSCAB_PATH, clsProgressInterface)
                    End If
                    Dim sImportPath As String
                    sImportPath = App.Path & TEMPORARY_PATH & NSCAB_PATH & sImportName
        
                    '既存の現場名と重複するか？
                    Dim sJobName As String
                    Dim sDistrictName As String
                    If Not ReadInfo(sImportPath & "\" & DATA_FILE_NAME, sJobName, sDistrictName) Then Call Err.Raise(ERR_FILE, , "ファイルにアクセスできません。")
                    Dim vJobName As Variant
                    If LookupCollectionVariant(objJobNames, vJobName, sJobName) Then
                        If Not bAllOK Then
                            Select Case frmMsgBoxOKCancel.DoModal("現場名""" & sJobName & """は既に存在します。" & vbCrLf & "現場名を変更して処理を続けますか?")
                            Case vbOK
                            Case 8 'すべてはい。
                                bAllOK = True
                            Case vbCancel
                                Exit Sub
                            End Select
                        End If
                        '現場名に連番を付ける。
                        Dim sOldJobName As String
                        sOldJobName = sJobName
                        Dim nNumber As Long
                        nNumber = 2
                        Do
                            sJobName = sOldJobName & "~(" & CStr(nNumber) & ")"
                            If Not LookupCollectionVariant(objJobNames, vJobName, sJobName) Then Exit Do
                            nNumber = nNumber + 1
                        Loop
                        If Not ChangeJobName(sImportPath & "\" & DATA_FILE_NAME, sJobName, clsProgressInterface) Then Call Err.Raise(ERR_FILE, , "ファイルにアクセスできません。")
                    End If
                    Call objJobNames.Add(sJobName, sJobName)
        
                    'プログレス。
                    Call clsProgressInterface.CheckCancel
        
                    '新しくプロジェクトフォルダを作成する。
                    Dim sFolderName As String
                    sFolderName = CreateProjectFolder
                    If sFolderName = "" Then Call Err.Raise(ERR_FILE, , "これ以上現場を追加できません。")
                    'テンポラリファイルと置き換える。
       
                    Call DeleteDir(frmMain.UserDataPath & sFolderName)
        
                    'Nameだと別のドライブに移動できないため、FileSystemObjectに変更　2009/6/11 NGS Yamada
                    Dim cFso As FileSystemObject
                    Set cFso = New FileSystemObject
                    Call cFso.CopyFolder(sImportPath, frmMain.UserDataPath & sFolderName)
                    Call cFso.DeleteFolder(sImportPath)
                    Set cFso = Nothing
        
            '        Name sImportPath As frmMain.UserDataPath & sFolderName

            '        Call DeleteDir(App.Path & DATA_FOLDER_NAME & sFolderName)
            '        Name sImportPath As App.Path & DATA_FOLDER_NAME & sFolderName
        
                    'プログレス。
                    Call clsProgressInterface.CheckCancel
                Next

            End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /// <summary>
        ///     エクスポート(nvz)ファイルをインポートするする。
        ///         ※現場のインポート
        ///         
        ///     sPath で指定されるエクスポートファイルをインポートする。
        ///     
        ///     引き数：
        ///         sPath エクスポートファイルのパス。
        ///         clsProgressInterface ProgressInterface オブジェクト。
        /// 
        /// </summary>
        /// <param name="sPath"></param>
        /// <param name="oclsProgressInterface"></param>
        public void ImportProjectFolders(ref List<string> sPath2, object lsProgressInterface)
        {
            //clsProgressInterface = (ProgressInterface)oclsProgressInterface;

            ProgressInterface clsProgressInterface = new ProgressInterface();

            List<string> sPath = new List<string>();
            sPath.Add(@"C:\Develop\現場エクスポート\0006\瀬戸口金城測量.nvz");


            //'プログレス。
            clsProgressInterface.CheckCancel();



            //'既存の現場名。
            List<string> sJobNames = new List<string>();
            List<string> sDistrictNames = new List<string>();
            List<string> sFolderNames = new List<string>(); 
            List<DateTime> tModTime = new List<DateTime>();
            List<DateTime> tCreateTime = new List<DateTime>(); ;
            GetProjectList(ref sJobNames, ref sDistrictNames, ref sFolderNames, ref tModTime, ref tCreateTime);

            //Collection objJobNames = new Collection();
            List<string> objJobNames = new List<string>();
            for (int ii = 0; ii < sJobNames.Count; ii++)
            {
                string sJobNames0 = sJobNames[ii];
                SetAtCollectionVariant(objJobNames, ref sJobNames0, sJobNames0);
            }


            //'プログレス。
            clsProgressInterface.CheckCancel();


            bool bAllOK;
            bAllOK = false;

            for (int ii = 0; ii <= sPath.Count; ii++)   //"C:\Develop\現場エクスポート\0006\瀬戸口金城測量.nvz"
            {

                //'テンポラリフォルダを空にする。
                string App_Path = @"C:\Develop\NetSurv\Src\NS-App\NS-Survey";
                bool bDMY = mdlUtility.EmptyDir($"{App_Path}{MdlNSDefine.TEMPORARY_PATH}{MdlNSSDefine.NSCAB_PATH}", false); //"C:\Develop\NetSurv\Src\NS-App\NS-Survey\Temp\NSCAB\"


                //'プログレス。
                clsProgressInterface.CheckCancel();


                //'ファイルの展開。
                string sImportName;
                ProjectSolidifyer clsProjectSolidifyer = new ProjectSolidifyer();


                if (clsProjectSolidifyer.IsSolid(sPath[ii]))    //"C:\Develop\現場エクスポート\0006\瀬戸口金城測量.nvz"
                {
                    sImportName = clsProjectSolidifyer.ImportProjectFolder(sPath[ii], $"{App_Path}{MdlNSDefine.TEMPORARY_PATH}{MdlNSSDefine.NSCAB_PATH}", clsProgressInterface);

                }
                else
                {
                    ProjectCompressor clsProjectCompressor = new ProjectCompressor();
                    //sImportName => Ex."0006"
                    sImportName = clsProjectCompressor.ImportProjectFolder(sPath[ii], $"{App_Path}{MdlNSDefine.TEMPORARY_PATH}{MdlNSSDefine.NSCAB_PATH}", clsProgressInterface);
                }


                string sImportPath;
                sImportPath = $"{App_Path}{MdlNSDefine.TEMPORARY_PATH}{MdlNSSDefine.NSCAB_PATH}{sImportName}";


                //'既存の現場名と重複するか？
                string sJobName;
                string sDistrictName;

#if false
                If Not ReadInfo(sImportPath & "\" & DATA_FILE_NAME, sJobName, sDistrictName) 
                    //Then Call Err.Raise(ERR_FILE, , "ファイルにアクセスできません。")
                    MessageBox.Show($"ファイルにアクセスできません。","エラー発生");



                Dim vJobName As Variant
                If LookupCollectionVariant(objJobNames, vJobName, sJobName) Then
                    If Not bAllOK Then
                        Select Case frmMsgBoxOKCancel.DoModal("現場名""" & sJobName & """は既に存在します。" & vbCrLf & "現場名を変更して処理を続けますか?")
                        Case vbOK
                        Case 8 'すべてはい。
                            bAllOK = True
                        Case vbCancel
                            Exit Sub
                        End Select
                    End If
                    '現場名に連番を付ける。
                    Dim sOldJobName As String
                    sOldJobName = sJobName
                    Dim nNumber As Long
                    nNumber = 2
                    Do
                        sJobName = sOldJobName & "~(" & CStr(nNumber) & ")"
                        If Not LookupCollectionVariant(objJobNames, vJobName, sJobName) Then Exit Do
                        nNumber = nNumber + 1
                    Loop
                    If Not ChangeJobName(sImportPath & "\" & DATA_FILE_NAME, sJobName, clsProgressInterface) Then Call Err.Raise(ERR_FILE, , "ファイルにアクセスできません。")
                End If
                Call objJobNames.Add(sJobName, sJobName)
#endif



            }




        }



        //==========================================================================================
        /*[VB]
            'プロジェクトフォルダをエクスポートする。
            '
            'sFolderNames で指定されるフォルダを sPath で指定されるフォルダにエクスポートする。
            '
            '引き数：
            'sFolderNames フォルダ名。参照する配列の要素は(0 To ...)。Ubound 関数を使用する。
            'sPath 出力先フォルダのパス。
            'clsProgressInterface ProgressInterface オブジェクト。
            Public Sub ExportProjectFolders(ByRef sFolderNames() As String, ByVal sPath As String, ByVal clsProgressInterface As ProgressInterface)

                Dim bAllYes As Boolean
                Dim i As Long
                For i = 0 To UBound(sFolderNames)
                    '現場名。
                    Dim sJobName As String
                    Dim sDistrictName As String
                    If Not ReadInfo(frmMain.UserDataPath & sFolderNames(i) & "\" & DATA_FILE_NAME, sJobName, sDistrictName) Then Call Err.Raise(ERR_FILE, , "ファイルにアクセスできません。")
            '        If Not ReadInfo(App.Path & DATA_FOLDER_NAME & sFolderNames(i) & "\" & DATA_FILE_NAME, sJobName, sDistrictName) Then Call Err.Raise(ERR_FILE, , "ファイルにアクセスできません。")
        
                    'プログレス。
                    Call clsProgressInterface.CheckCancel
                    clsProgressInterface.Prompt = sJobName & vbCrLf & "のエクスポート中･･･"


                    Dim sExportPath As String
                    sExportPath = sPath & "\" & sJobName & "." & NSSEXP_FILE_EXT
                    Dim bExport As Boolean
                    If bAllYes Then
                        bExport = True
                    Else
                        bExport = False
                        Dim clsFind As New FileFind
                        If clsFind.FindFile(sExportPath) Then
                            Select Case frmMsgBoxYesNoCancel.DoModal(sExportPath & "は既に存在します。上書きしますか?")
                            Case vbYes
                                bExport = True
                            Case 8 'すべてはい。
                                bExport = True
                                bAllYes = True
                            Case vbNo
                            Case vbCancel
                                Exit Sub
                            End Select
                        Else
                            bExport = True
                        End If
                    End If


                    If bExport Then
                        Dim clsProjectCompressor As New ProjectCompressor
                         If Not clsProjectCompressor.ExportProjectFolder(sFolderNames(i), sExportPath, clsProgressInterface) Then
                            Dim clsProjectSolidifyer As New ProjectSolidifyer
                            Call clsProjectSolidifyer.ExportProjectFolder(sFolderNames(i), sExportPath, clsProgressInterface)
                        End If
                    End If
        
                    'プログレス。
                    Call clsProgressInterface.CheckCancel
                Next

            End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        public void ExportProjectFolders(ref List<string> sFolderNames, string sPath, object oclsProgressInterface)
        {
            ProgressInterface clsProgressInterface = (ProgressInterface)oclsProgressInterface;
            if (clsProgressInterface.CheckCancel())
            {
                return;
            }


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

                '現場名。
                Dim sJobName As String
                Dim sDistrictName As String
            '    If Not ReadInfo(App.Path & DATA_FOLDER_NAME & sFolderName & "\" & DATA_FILE_NAME, sJobName, sDistrictName) Then Call Err.Raise(ERR_FILE, , "ファイルにアクセスできません。")
                If Not ReadInfo(frmMain.UserDataPath & sFolderName & "\" & DATA_FILE_NAME, sJobName, sDistrictName) Then Call Err.Raise(ERR_FILE, , "ファイルにアクセスできません。")
    
                'プログレス。
                Call clsProgressInterface.CheckCancel
                clsProgressInterface.Prompt = sJobName & vbCrLf & "のエクスポート中･･･"


                Dim clsProjectCompressor As New ProjectCompressor
                If Not clsProjectCompressor.ExportProjectFolder(sFolderName, sPath, clsProgressInterface) Then
                    Dim clsProjectSolidifyer As New ProjectSolidifyer
                    Call clsProjectSolidifyer.ExportProjectFolder(sFolderName, sPath, clsProgressInterface)
                End If

            End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        public void ExportProjectFolder(string sFolderName, string sPath, object oclsProgressInterface)
        {
            ProgressInterface clsProgressInterface = (ProgressInterface)oclsProgressInterface;

            if (clsProgressInterface.CheckCancel())
            {
                return;
            }

        }




        //<<<<<<<<<-----------24/01/28 K.setoguchi@NV
        //***************************************************************************
        //***************************************************************************

        //==========================================================================================
        /*[VB]
            '保存ファイルのフォルダパスを取得する。
            '
            '引き数：
            'sFolderName フォルダ名。
            '
            '戻り値：保存ファイルのフォルダパスを取得する。最後に'\'が付いている。
            Public Function GetSaveFilePath(ByVal sFolderName As String) As String
                GetSaveFilePath = frmMain.UserDataPath & sFolderName & "\"
            '    GetSaveFilePath = App.Path & DATA_FOLDER_NAME & sFolderName & "\"
            End Function
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //***************************************************************************
        /// <summary>
        /// 保存ファイルのフォルダパスを取得する。
        //  '引き数：
        //  sFolderName フォルダ名
        /// </summary>
        /// <param name="sPath"></param>
        /// <param name="ref sJobName"></param>
        /// <param name="ref sDistrictName"></param>
        /// <returns>
        /// 戻り値:returns = 保存ファイルのフォルダパスを取得する。最後に'\'が付いている。
        /// </returns>
        //***************************************************************************
        public string GetSaveFilePath(string sFolderName)
        {
            // GetSaveFilePath = $("{frmMain.UserDataPath} + { sFolderName}\\");
            //     @"C:\Develop\NetSurv\Src\NS-App\NS-Survey\UserData\";

            string UserDataPath = @"C:\Develop\NetSurv\Src\NS-App\NS-Survey\UserData\";
            string GetSaveFilePath = $"{UserDataPath}{sFolderName}\\";


            return GetSaveFilePath;
        }


        //==========================================================================================
        /*[VB]
            '現場リストを作成する。
            '
            '引き数：
            'lvProject 対象とするリストビュー。
            'bCheck チェックボックスの有無。True=チェックボックス有り。False=チェックボックス無し。
            Public Sub MakeProjectListView(ByVal lvProject As ListView, ByVal bCheck As Boolean)

                'リストの初期化。
                lvProject.View = lvwReport
                lvProject.Checkboxes = bCheck
                lvProject.FullRowSelect = True
                lvProject.LabelEdit = lvwManual
                lvProject.HideSelection = False
                lvProject.Sorted = True
    
                'カラムの初期化。
                Dim lvColumn As ColumnHeader
                Dim nWidth As Long
                Dim nTotalWidth As Long
                If bCheck Then
                    Set lvColumn = lvProject.ColumnHeaders.Add(, , COL_NAM_PROJECTLIST_CHECK)
                    nWidth = COL_WID_PROJECTLIST_CHECK
                    nTotalWidth = nTotalWidth + nWidth
                    lvColumn.Width = nWidth
                End If
                Set lvColumn = lvProject.ColumnHeaders.Add(, , COL_NAM_PROJECTLIST_JOBNAME)
                nWidth = COL_WID_PROJECTLIST_JOBNAME
                nTotalWidth = nTotalWidth + nWidth
                lvColumn.Width = nWidth
                Set lvColumn = lvProject.ColumnHeaders.Add(, , COL_NAM_PROJECTLIST_DISTRICTNAME)
                nWidth = COL_WID_PROJECTLIST_DISTRICTNAME
                nTotalWidth = nTotalWidth + nWidth
                lvColumn.Width = nWidth
                Set lvColumn = lvProject.ColumnHeaders.Add(, , COL_NAM_PROJECTLIST_FOLDER)
                nWidth = COL_WID_PROJECTLIST_FOLDER
                nTotalWidth = nTotalWidth + nWidth
                lvColumn.Width = nWidth
                Set lvColumn = lvProject.ColumnHeaders.Add(, , COL_NAM_PROJECTLIST_MDATE)
                nWidth = COL_WID_PROJECTLIST_MDATE
                nTotalWidth = nTotalWidth + nWidth
                lvColumn.Width = nWidth
                Set lvColumn = lvProject.ColumnHeaders.Add(, , COL_NAM_PROJECTLIST_CDATE)
                Dim tRect As RECT
                Dim nWidthScrollBar As Long
                nWidthScrollBar = GetSystemMetrics(SM_CXVSCROLL) + 1 'チョット余裕をもたせるために1だけ足しとく。ギリギリだとはみ出す。四捨五入だから？
                If GetClientRect(lvProject.hWnd, tRect) = 0 Then Call Err.Raise(ERR_FATAL, , GetLastErrorMessage())
                lvColumn.Width = CheckReturnMin((tRect.Right - tRect.Left - nWidthScrollBar) * Screen.TwipsPerPixelX - nTotalWidth, 0)
    
                Dim sJobNames() As String
                Dim sDistrictNames() As String
                Dim sFolderNames() As String
                Dim tModTime() As Date
                Dim tCreateTime() As Date
                Call GetProjectList(sJobNames, sDistrictNames, sFolderNames, tModTime, tCreateTime)
    
                'リストの作成。
                Dim lvItem As ListItem
                Dim lvSubItem As ListSubItem
                Dim i As Long
                For i = 0 To UBound(sFolderNames)
                    'アイテムの追加。
                    If bCheck Then
                        Set lvItem = lvProject.ListItems.Add(, , "")
                    Else
                        Set lvItem = lvProject.ListItems.Add(, , sJobNames(i))
                    End If
                    'フォルダ名をキーにする。
                    lvItem.Key = KEYPREFIX & sFolderNames(i)
                    'サブアイテムの追加。
                    If bCheck Then Set lvSubItem = lvItem.ListSubItems.Add(, , sJobNames(i))
                    Set lvSubItem = lvItem.ListSubItems.Add(, , sDistrictNames(i))
                    Set lvSubItem = lvItem.ListSubItems.Add(, , sFolderNames(i))
                    Set lvSubItem = lvItem.ListSubItems.Add(, , Format$(tModTime(i), "yyyy/mm/dd hh:nn"))
                    Set lvSubItem = lvItem.ListSubItems.Add(, , Format$(tCreateTime(i), "yyyy/mm/dd hh:nn"))
                Next

            End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //*******************************************************************************************
        /// <summary>
        /// 現場リストを作成する。
        ///  '引き数：
        ///  'lvProject 対象とするリストビュー。
        //  'bCheck チェックボックスの有無。True=チェックボックス有り。False=チェックボックス無し。
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <returns>
        /// </returns>
        //*******************************************************************************************
        public void MakeProjectListView(GENBA_STRUCT[] Genba, int ListCount, ref int datacount)
        {


            //*******************************
            //現場リスト情報を取得
            //*******************************
            GetProjectList(Genba, ListCount, ref datacount);


        }
        //***************************************************************************
        public void MakeProjectListView(ListView lvProject, bool bCheck)
        {

            //--------------------------
            //'カラムの初期化。 リストHaeder
            //--------------------------
            int nWidth = 0;             //    Dim nWidth As Long
            long nTotalWidth = 0;       //    Dim nTotalWidth As Long
            if (bCheck)
            {
                lvProject.Columns.Clear();
                lvProject.View = View.Details;
                lvProject.CheckBoxes = true;

                //'現場リストカラム名称、チェックボックス。
                nWidth = COL_WID_PROJECTLIST_CHECK;
                nTotalWidth = nWidth;
                lvProject.Columns.Add(COL_NAM_PROJECTLIST_CHECK, nWidth);
            }
            else
            {
                lvProject.Columns.Clear();
            }

            //'現場リストカラム名称、現場名。
            nWidth = COL_WID_PROJECTLIST_JOBNAME;
            nTotalWidth = nWidth;
            lvProject.Columns.Add(COL_NAM_PROJECTLIST_JOBNAME, nWidth);

            //'現場リストカラム名称、地区名。
            nWidth = COL_WID_PROJECTLIST_DISTRICTNAME;
            nTotalWidth += nWidth;
            lvProject.Columns.Add(COL_NAM_PROJECTLIST_DISTRICTNAME, nWidth);

            //'現場リストカラム名称、フォルダ。
            nWidth = COL_WID_PROJECTLIST_FOLDER;
            nTotalWidth += nWidth;
            lvProject.Columns.Add(COL_NAM_PROJECTLIST_FOLDER, nWidth);

            //'現場リストカラム名称、最終更新日。
            nWidth = COL_WID_PROJECTLIST_MDATE;
            nTotalWidth += nWidth;
            lvProject.Columns.Add(COL_NAM_PROJECTLIST_MDATE, nWidth);

            //'現場リストカラム名称、作成日。
            nWidth = COL_WID_PROJECTLIST_CDATE;
            nTotalWidth += nWidth;
            lvProject.Columns.Add(COL_NAM_PROJECTLIST_CDATE, nWidth);

            lvProject.Width = (int)nTotalWidth;


            //--------------------------
            //データ取得
            //--------------------------

            List<string> sJobNames = new List<string>();            //Dim sJobNames() As String
            List<string> sDistrictNames = new List<string>(); ;     //Dim sDistrictNames() As String
            List<string> sFolderNames = new List<string>(); ;       //Dim sFolderNames() As String
            List<DateTime> tModTime = new List<DateTime>(); ;       //Dim tModTime() As Date
            List<DateTime> tCreateTime = new List<DateTime>(); ;    //Dim tCreateTime() As Date

            GetProjectList(ref sJobNames,  ref sDistrictNames, ref sFolderNames, ref tModTime, ref tCreateTime);


            //--------------------------
            //  ＜＜＜ 現場の情報を表示　＞＞＞
            //--------------------------
            //'リストの作成。
            for (int i2 = 0; i2 < sFolderNames.Count; i2++)
            {

                //'フォルダ名をキーにする。
                string sp = "";
                ListViewItem item = new ListViewItem(sp)
                {
                    ImageKey = $"{MdlNSDefine.KEYPREFIX}{sFolderNames[i2]}"
                };
                //  m_sFolderNames.Add(Genba[i].sFolderNames);     //データ

                _ = item.SubItems.Add(sJobNames[i2]);
                _ = item.SubItems.Add(sDistrictNames[i2]);
                _ = item.SubItems.Add(sFolderNames[i2]);


                _ = item.SubItems.Add(tModTime[i2].ToString("yyyy/MM/dd HH:mm"));
                _ = item.SubItems.Add(tCreateTime[i2].ToString("yyyy/MM/dd HH:mm"));
                _ = lvProject.Items.Add(item);
            }

        }
        //***************************************************************************
        //***************************************************************************
        //***************************************************************************


        //23/12/29 K.setoguchi@NV---------->>>>>>>>>>
        //***************************************************************************

        //==========================================================================================
        /*[VB]
            '現場名の重複チェック。
            '
            'sJobName で指定された現場名がすでに存在するか確認する。
            '
            '引き数：
            'sFolder フォルダ名。
            'sJobName 現場名。
            '
            '戻り値：
            '重複しない場合は True を返す。
            '重複する場合は False を返す。
            Public Function CheckJobName(ByVal sFolder As String, ByVal sJobName As String) As Boolean

                CheckJobName = False

                Dim sJobNames() As String
                Dim sDistrictNames() As String
                Dim sFolderNames() As String
                Dim tModTime() As Date
                Dim tCreateTime() As Date
                Call GetProjectList(sJobNames, sDistrictNames, sFolderNames, tModTime, tCreateTime)


                Dim i As Long
                For i = 0 To UBound(sFolderNames)
                    If sFolderNames(i) <> sFolder Then
                        If sJobName = sJobNames(i) Then Exit Function
                    End If
                Next


                CheckJobName = True

            End Function
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /// <summary>
        /// '現場名の重複チェック。
        /// '
        /// 'sJobName で指定された現場名がすでに存在するか確認する。
        /// '
        /// '引き数：
        ///     'sFolder フォルダ名。
        ///     'sJobName 現場名。
        /// </summary>
        /// <param name="sFolder"></param>
        /// <param name="sJobName"></param>
        /// <returns>
        /// '戻り値：
        ///     '重複しない場合は True を返す。
        ///     '重複する場合は False を返す。
        /// </returns>
        public bool CheckJobName(string sFolder, string sJobName)
        {
            bool CheckJobName = false;

            //    Dim sJobNames() As String
            //    Dim sDistrictNames() As String
            //    Dim sFolderNames() As String
            //    Dim tModTime() As Date
            //    Dim tCreateTime() As Date
            //    Call GetProjectList(sJobNames, sDistrictNames, sFolderNames, tModTime, tCreateTime)
            List<string> sJobNames = new List<string>();
            List<string> sDistrictNames = new List<string>();
            List<string> sFolderNames = new List<string>();
            List<DateTime> tModTime = new List<DateTime>();
            List<DateTime> tCreateTime = new List<DateTime>();

            GetProjectList(ref sJobNames, ref sDistrictNames, ref sFolderNames, ref tModTime, ref tCreateTime);


            //    Dim i As Long
            //    For i = 0 To UBound(sFolderNames)
            //        If sFolderNames(i) <> sFolder Then
            //            If sJobName = sJobNames(i) Then Exit Function
            //        End If
            //    Next
            for (int i = 0; i < sJobNames.Count; i++)
            {
                if (sFolderNames[i] != sFolder)
                {
                    if (sJobName == sJobNames[i])
                    {
                        return CheckJobName;
                    }
                }
            }
            CheckJobName = true;
            return CheckJobName;
        }


        //24/01/28 K.setoguchi@NV---------->>>>>>>>>>
        //***************************************************************************

        //==========================================================================================
        /*[VB]
            '現場名、地区名を取得する。
            '
            'sFolder で指定されるフォルダの現場の情報を取得する。
            '
            '引き数：
            'sFolder フォルダ名。
            'sJobName 現場名が設定される。
            'sDistrictName 地区名が設定される。
            '
            '戻り値：
            '正常に取得できた場合は True を返す。
            'それ以外の場合は False を返す。
            Public Function GetJobInfo(ByVal sFolder As String, ByRef sJobName As String, ByRef sDistrictName As String) As Boolean
                GetJobInfo = False
                GetJobInfo = ReadInfo(frmMain.UserDataPath & sFolder & "\" & DATA_FILE_NAME, sJobName, sDistrictName)
            '    GetJobInfo = ReadInfo(App.Path & DATA_FOLDER_NAME & sFolder & "\" & DATA_FILE_NAME, sJobName, sDistrictName)
            End Function
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        public bool GetJobInfo(string sFolder, ref string sJobName, ref string sDistrictName)
        {
            bool GetJobInfo = false;

            //frmMain2.UserDataPath = "C:\Develop\NetSurv\Src\NS-App\NS-Survey\UserData\"
            string frmMain2_UserDataPath = @"C:\Develop\NetSurv\Src\NS-App\NS-Survey\UserData\";
            //sFolder = "0007"     DATA_FILE_NAME = "data"

            GetJobInfo = ReadInfo($"{frmMain2_UserDataPath}{sFolder}\\{GENBA_CONST.DATA_FILE_NAME}", ref sJobName, ref sDistrictName);

            return GetJobInfo;

        }


        //<<<<<<<<<-----------24/01/28 K.setoguchi@NV
        //***************************************************************************

        //'*******************************************************************************
        //'インプリメンテーション


        //==========================================================================================
        /*[VB]
            '現場名、地区名をファイルから取得する。
            '
            '引き数：
            'sPath 保存ファイルのパス。
            'sJobName 現場名が設定される。
            'sDistrictName 地区名が設定される。
            '
            '戻り値：
            '正常に取得できた場合は True を返す。
            'それ以外の場合は False を返す。
            Private Function ReadInfo(ByVal sPath As String, ByRef sJobName As String, ByRef sDistrictName As String) As Boolean
                ReadInfo = False
                Dim clsFile As New FileNumber
                Open sPath For Binary Access Read Lock Write As #clsFile.Number
                'ファイルバージョン。
                Dim nVersion As Long
                Get #clsFile.Number, , nVersion
                If nVersion < 1100 Then Exit Function
                '現場名、地区名。
                sJobName = GetString(clsFile.Number)
                sDistrictName = GetString(clsFile.Number)
                Call clsFile.CloseFile
                ReadInfo = True
            End Function
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //***************************************************************************
        /// <summary>
        /// 現場名、地区名をファイルから取得する。
        //  '引き数：
        //  sPath 保存ファイルのパス。
        //  sJobName 現場名が設定される。
        //  sDistrictName 地区名が設定される。
        /// </summary>
        /// <param name="sPath"></param>
        /// <param name="ref sJobName"></param>
        /// <param name="ref sDistrictName"></param>
        /// <returns>
        /// 戻り値:returns = 正常に取得できた場合は True を返す。
        //                   それ以外の場合は False を返す。
        /// </returns>
        //***************************************************************************
        public bool ReadInfo(string sPath, ref string sJobName, ref string sDistrictName)
        {
            bool ReadInfo = false;
            long nVersion;
            MdlUtility Utility = new MdlUtility();

            using (var fs = System.IO.File.OpenRead($"{sPath}"))
            {
                using (var br = new BinaryReader(fs))
                {
                    //******************************************
                    //nVersion
                    //******************************************
                    nVersion = br.ReadInt32();
                    if (nVersion < 1100) { return ReadInfo; };

                    //******************************************
                    //  現場名をグローバル領域に設定
                    //******************************************
                    sJobName = Utility.FileRead_GetString(br);

                    //******************************************
                    //  地区名をグローバル領域に設定
                    //******************************************
                    sDistrictName = Utility.FileRead_GetString(br);

                }
                fs.Close();

                ReadInfo = true;


            }
            return ReadInfo;
        }

        //==========================================================================================
        /*[VB]
            '現場名を変更する。
            '
            'sPath で指定された data ファイルの現場名を変更する。
            '
            '引き数：
            'sPath data ファイルのパス。
            'sJobName 新しい現場名。
            '
            '戻り値：
            '正常に取得できた場合は True を返す。
            'それ以外の場合は False を返す。
            'clsProgressInterface ProgressInterface オブジェクト。
            Private Function ChangeJobName(ByVal sPath As String, ByVal sJobName As String, ByVal clsProgressInterface As ProgressInterface) As Boolean

                ChangeJobName = False


                Dim nFileLen As Long
                nFileLen = FileLen(sPath)


                'テンポラリファイル。
                Dim sTemp As String
                sTemp = App.Path & TEMPORARY_PATH & DATA_FILE_NAME
                Call RemoveFile(sTemp)
                Dim clsDstFile As New FileNumber
                Open sTemp For Binary Access Write Lock Write As #clsDstFile.Number
    
                'data ファイル。
                Dim clsSrcFile As New FileNumber
                Open sPath For Binary Access Read Lock Write As #clsSrcFile.Number
    
                'ファイルバージョン。
                Dim nVersion As Long
                Get #clsSrcFile.Number, , nVersion
                If nVersion < 1100 Then Exit Function
                Put #clsDstFile.Number, , nVersion
    
                '現場名。
                Call GetString(clsSrcFile.Number)
                Call PutString(clsDstFile.Number, sJobName)


                'コピー。
                Dim nSize As Long
                'nVersionが3200以降のバージョンはチェックサム分だけ引く。　2009/5/28 NGS Yamada
            '    nSize = nFileLen - Loc(clsSrcFile.Number) - Len(nSize)
                If nVersion >= 3200 Then
                    nSize = nFileLen - Loc(clsSrcFile.Number) - Len(nSize)
                Else
                    nSize = nFileLen - Loc(clsSrcFile.Number)
                End If


                Call CopyFileContents(clsSrcFile.Number, clsDstFile.Number, nSize, FILEIOMEMSIZE, clsProgressInterface)


                'nVersionが3200以降のバージョンはチェックサムを追加。　2009/5/28 NGS Yamada
                If nVersion >= 3200 Then
                    'チェックサム。
                    nSize = Loc(clsDstFile.Number)
                    Put #clsDstFile.Number, , nSize
                End If


                Call clsSrcFile.CloseFile
                Call clsDstFile.CloseFile


                'テンポラリファイルと置き換える。
                Call ReplaceFile(sTemp, sPath)


                ChangeJobName = True


            End Function
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]



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
        public void SetAtCollectionVariant(List<string> objCollection, ref string vItem, string sKey)
        {
            try
            {
                //objCollection.Add($"{vItem}:{sKey}");
                objCollection.Add(vItem);
            }
            catch (Exception)
            {
                //CollectionHandler:
                return;
            }
        }
        //==========================================================================================


    }
}
