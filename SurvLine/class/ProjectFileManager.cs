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

namespace SurvLine
{

    public class ProjectFileManager
    {

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
        //[VB]  Public Sub MakeProjectListView(ByVal lvProject As ListView, ByVal bCheck As Boolean)
        public void MakeProjectListView(GENBA_STRUCT[] Genba, int ListCount, ref int datacount)
        {

            //************************************************************
            //'リストの初期化。
            //************************************************************
            //[VB]            var frmJob = new frmJobOpen();
            //[VB]            frmJob.lvProject.View = View.LargeIcon;
            //[VB]
            //[VB]        lvProject.View = lvwReport
            //[VB]        lvProject.Checkboxes = bCheck
            //[VB]        lvProject.FullRowSelect = True
            //[VB]        lvProject.LabelEdit = lvwManual
            //[VB]        lvProject.HideSelection = False
            //[VB]        lvProject.Sorted = True
            //
            //var fJobOpen = new frmJobOpen();
            //fJobOpen.lvProject.View = View.Details;
            //fJobOpen.lvProject.Columns.Clear();
            //************************************************************



            //    'カラムの初期化。
            //    Dim lvColumn As ColumnHeader
            //    Dim nWidth As Long
            //    Dim nTotalWidth As Long
            //    If bCheck Then
            //        Set lvColumn = lvProject.ColumnHeaders.Add(, , COL_NAM_PROJECTLIST_CHECK)
            //        nWidth = COL_WID_PROJECTLIST_CHECK
            //        nTotalWidth = nTotalWidth + nWidth
            //        lvColumn.Width = nWidth
            //    End If
            //    Set lvColumn = lvProject.ColumnHeaders.Add(, , COL_NAM_PROJECTLIST_JOBNAME)
            //    nWidth = COL_WID_PROJECTLIST_JOBNAME
            //    nTotalWidth = nTotalWidth + nWidth
            //    lvColumn.Width = nWidth
            //    Set lvColumn = lvProject.ColumnHeaders.Add(, , COL_NAM_PROJECTLIST_DISTRICTNAME)
            //    nWidth = COL_WID_PROJECTLIST_DISTRICTNAME
            //    nTotalWidth = nTotalWidth + nWidth
            //    lvColumn.Width = nWidth
            //    Set lvColumn = lvProject.ColumnHeaders.Add(, , COL_NAM_PROJECTLIST_FOLDER)
            //    nWidth = COL_WID_PROJECTLIST_FOLDER
            //    nTotalWidth = nTotalWidth + nWidth
            //    lvColumn.Width = nWidth
            //    Set lvColumn = lvProject.ColumnHeaders.Add(, , COL_NAM_PROJECTLIST_MDATE)
            //    nWidth = COL_WID_PROJECTLIST_MDATE
            //    nTotalWidth = nTotalWidth + nWidth
            //    lvColumn.Width = nWidth
            //    Set lvColumn = lvProject.ColumnHeaders.Add(, , COL_NAM_PROJECTLIST_CDATE)
            //    Dim tRect As RECT
            //    Dim nWidthScrollBar As Long
            //    nWidthScrollBar = GetSystemMetrics(SM_CXVSCROLL) + 1 'チョット余裕をもたせるために1だけ足しとく。ギリギリだとはみ出す。四捨五入だから？
            //    If GetClientRect(lvProject.hWnd, tRect) = 0 Then Call Err.Raise(ERR_FATAL, , GetLastErrorMessage())
            //    lvColumn.Width = CheckReturnMin((tRect.Right - tRect.Left - nWidthScrollBar) * Screen.TwipsPerPixelX - nTotalWidth, 0)

            //String sJobNames;           //Dim sJobNames() As String
            //String sDistrictNames;      //Dim sDistrictNames() As String
            //String sFolderNames;        //Dim sFolderNames() As String
            //DateTime tModTime;          //Dim tModTime() As Date
            //DateTime tCreateTime;       //Dim tCreateTime() As Date

            //  //現場リスト数を算出。
            //  int ListCount = GetProjectListCount();
            //   //現場リスト数により、現場リスト領域を確保
            //   var Genba = new GENBA_STRUCT[ListCount];

            //*******************************
            //現場リスト情報を取得
            //*******************************
            GetProjectList(Genba, ListCount, ref datacount);


            //*******************************
            //var frmJob3 = new GetProjectList(sJobNames);
            //var frmJob2 = new GetProjectList(String sJobNames, String sDistrictNames, String sFolderNames, DateTime tModTime, DateTime tCreateTime); ;
            //var frmJob2 = new GetProjectList(sJobNames, sDistrictNames, sFolderNames, tModTime, tCreateTime);

            // GetProjectList( sJobNames, sDistrictNames, sFolderNames, tModTime, tCreateTime);
            //    GetProjectList(String sJobNames, String sDistrictNames, String sFolderNames, DateTime tModTime, DateTime tCreateTime);

            //var aa = new frmJobOpen.
            //object lvProject = new frmJobOpen.ControlCollection.lvProject.ListView();
            //
            //   'sJobNames。
            //   Dim lvItem As ListItem
            //    Dim lvSubItem As   
            //    Dim i As Long
            //    For i = 0 To UBound(sFolderNames)
            //        'アイテムの追加。
            //        If bCheck Then
            //            Set lvItem = lvProject.ListItems.Add(, , "")
            //        Else
            //            Set lvItem = lvProject.ListItems.Add(, , sJobNames(i))
            //        End If
            //        'フォルダ名をキーにする。
            //        lvItem.Key = KEYPREFIX & sFolderNames(i)
            //        'サブアイテムの追加。
            //        If bCheck Then Set lvSubItem = lvItem.ListSubItems.Add(, , sJobNames(i))
            //        Set lvSubItem = lvItem.ListSubItems.Add(, , sDistrictNames(i))
            //        Set lvSubItem = lvItem.ListSubItems.Add(, , sFolderNames(i))
            //        Set lvSubItem = lvItem.ListSubItems.Add(, , Format$(tModTime(i), "yyyy/mm/dd hh:nn"))
            //        Set lvSubItem = lvItem.ListSubItems.Add(, , Format$(tCreateTime(i), "yyyy/mm/dd hh:nn"))
            //    Next
        }

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
                sFolderNames.Add(sFolderName);


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
        //'メソッド
        //
        //'プロジェクトのリストを取得する。
        //'
        //'現在保存されているプロジェクトの一覧情報を取得する。
        //'
        //'引き数：
        //'sJobNames 現場名が設定される。配列の要素は(-1 To ...)、要素 -1 は未使用。
        //'sDistrictNames 地区名が設定される。配列の要素は(-1 To ...)、要素 -1 は未使用。
        //'sFolderNames フォルダ名が設定される。配列の要素は(-1 To ...)、要素 -1 は未使用。
        //'tModTime 更新日時が設定される。配列の要素は(-1 To ...)、要素 -1 は未使用。
        //'tCreateTime 作成日時が設定される。配列の要素は(-1 To ...)、要素 -1 は未使用。
        //Public Sub GetProjectList(ByRef sJobNames() As String, ByRef sDistrictNames() As String, ByRef sFolderNames() As String, ByRef tModTime() As Date, ByRef tCreateTime() As Date)
        //
        //    ReDim sJobNames(-1 To -1)
        //    ReDim sDistrictNames(-1 To -1)
        //    ReDim sFolderNames(-1 To -1)
        //    ReDim tModTime(-1 To -1)
        //    ReDim tCreateTime(-1 To -1)
        //
        //
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
        //
        //    Do
        //        If(clsFind.Attributes And FILE_ATTRIBUTE_DIRECTORY) = 0 Then GoTo ContinueHandler
        //        sFolderName = clsFind.Name
        //        If IsDots(sFolderName) Then GoTo ContinueHandler
        //        If Not ReadInfo(sPath & sFolderName & "\" & DATA_FILE_NAME, sJobName, sDistrictName) Then GoTo ContinueHandler
        //        nUBound = nUBound + 1
        //        ReDim Preserve sJobNames(-1 To nUBound)
        //        ReDim Preserve sDistrictNames(-1 To nUBound)
        //        ReDim Preserve sFolderNames(-1 To nUBound)
        //        ReDim Preserve tModTime(-1 To nUBound)
        //        ReDim Preserve tCreateTime(-1 To nUBound)
        //        sJobNames(nUBound) = sJobName
        //        sDistrictNames(nUBound) = sDistrictName
        //        sFolderNames(nUBound) = sFolderName
        //        Dim clsFind2 As FileFind
        //        Set clsFind2 = New FileFind
        //        If clsFind2.FindFile(sPath & sFolderName & "\" & DATA_FILE_NAME) Then
        //            tModTime(nUBound) = clsFind2.LastWriteTime
        //            tCreateTime(nUBound) = clsFind2.CreationTime
        //        End If
        //ContinueHandler:
        //    Loop While clsFind.FindNext()
        //
        //End Sub
        //<<<<<<<<<-----------23/12/26 K.setoguchi@NV

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

        //'*******************************************************************************
        //'インプリメンテーション
        //
        //'現場名、地区名をファイルから取得する。
        //'
        //'引き数：
        //'sPath 保存ファイルのパス。
        //'sJobName 現場名が設定される。
        //'sDistrictName 地区名が設定される。
        //'
        //'戻り値：
        //'正常に取得できた場合は True を返す。
        //'それ以外の場合は False を返す。
        //Private Function ReadInfo(ByVal sPath As String, ByRef sJobName As String, ByRef sDistrictName As String) As Boolean
        //    ReadInfo = False
        //    Dim clsFile As New FileNumber
        //    Open sPath For Binary Access Read Lock Write As #clsFile.Number
        //    'ファイルバージョン。
        //    Dim nVersion As Long
        //    Get #clsFile.Number, , nVersion
        //    If nVersion< 1100 Then Exit Function
        //    '現場名、地区名。
        //    sJobName = GetString(clsFile.Number)
        //    sDistrictName = GetString(clsFile.Number)
        //    Call clsFile.CloseFile
        //    ReadInfo = True
        //End Function

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
        //[VB]  '保存ファイルのフォルダパスを取得する。
        //[VB]  '
        //[VB]  '引き数：
        //[VB]  'sFolderName フォルダ名。
        //[VB]  '
        //[VB]  '戻り値：保存ファイルのフォルダパスを取得する。最後に'\'が付いている。
        //[VB]  Public Function GetSaveFilePath(ByVal sFolderName As String) As String
        //[VB]      GetSaveFilePath = frmMain.UserDataPath & sFolderName & "\"
        //[VB]  '    GetSaveFilePath = App.Path & DATA_FOLDER_NAME & sFolderName & "\"
        //[VB]  End Function



        //23/12/26 K.setoguchi@NV---------->>>>>>>>>>
        //***************************************************************************
        //***************************************************************************
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
        //-------------------------------------------------------------------
        //'現場名の重複チェック。
        //'
        //'sJobName で指定された現場名がすでに存在するか確認する。
        //'
        //'引き数：
        //'sFolder フォルダ名。
        //'sJobName 現場名。
        //'
        //'戻り値：
        //'重複しない場合は True を返す。
        //'重複する場合は False を返す。
        //Public Function CheckJobName(ByVal sFolder As String, ByVal sJobName As String) As Boolean
        //
        //    CheckJobName = False
        //
        //    Dim sJobNames() As String
        //    Dim sDistrictNames() As String
        //    Dim sFolderNames() As String
        //    Dim tModTime() As Date
        //    Dim tCreateTime() As Date
        //    Call GetProjectList(sJobNames, sDistrictNames, sFolderNames, tModTime, tCreateTime)
        //
        //
        //    Dim i As Long
        //    For i = 0 To UBound(sFolderNames)
        //        If sFolderNames(i) <> sFolder Then
        //            If sJobName = sJobNames(i) Then Exit Function
        //        End If
        //    Next
        //
        //
        //    CheckJobName = True
        //
        //End Function
        //***************************************************************************
        //***************************************************************************
        //<<<<<<<<<-----------23/12/26 K.setoguchi@NV






    }
}
