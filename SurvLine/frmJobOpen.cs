using Microsoft.SqlServer.Server;
using Microsoft.VisualBasic;
using SurvLine.mdl;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace SurvLine
{
    public partial class frmJobOpen : Form
    {
        //23/12/26 K.setoguchi@NV---------->>>>>>>>>>
        // '現場を選択画面
        //
        //Option Explicit
        //
        //'プロパティ
        public long Result;         // As Long 'ダイアログの結果。
        public string FolderName;   // As String 'フォルダ名が設定される。

        //'インプリメンテーション
        private long m_nSortIndex;  // As Long'ソートカラムインデックス。

        //<<<<<<<<<-----------23/12/26 K.setoguchi@NV
        //--------------------------------------------------------------------------------------------------


        private string sendData = "";
        public frmMain2 fMain;

        public frmJobOpen()
        {

            //Private Const COL_NAM_PROJECTLIST_CHECK As String = "" '現場リストカラム名称、チェックボックス。
            //Private Const COL_NAM_PROJECTLIST_JOBNAME As String = "現場名" '現場リストカラム名称、現場名。
            //Private Const COL_NAM_PROJECTLIST_DISTRICTNAME As String = "地区名" '現場リストカラム名称、地区名。
            //Private Const COL_NAM_PROJECTLIST_FOLDER As String = "フォルダ" '現場リストカラム名称、フォルダ。
            //Private Const COL_NAM_PROJECTLIST_MDATE As String = "最終更新日" '現場リストカラム名称、最終更新日。
            //Private Const COL_NAM_PROJECTLIST_CDATE As String = "作成日" '現場リストカラム名称、作成日。
            const string COL_NAM_PROJECTLIST_CHECK = "";               //'現場リストカラム名称、チェックボックス。
            const string COL_NAM_PROJECTLIST_JOBNAME = "現場名";       //'現場リストカラム名称、現場名。
            const string COL_NAM_PROJECTLIST_DISTRICTNAME = "地区名";  // '現場リストカラム名称、地区名。
            const string COL_NAM_PROJECTLIST_FOLDER = "フォルダ";      //'現場リストカラム名称、フォルダ。
            const string COL_NAM_PROJECTLIST_MDATE = "最終更新日";     //'現場リストカラム名称、最終更新日。
            const string COL_NAM_PROJECTLIST_CDATE = "作成日";         //'現場リストカラム名称、作成日。

            //Private Const COL_WID_PROJECTLIST_CHECK As Long = 384      '現場リストカラム幅(Twips)、チェックボックス。
            //Private Const COL_WID_PROJECTLIST_JOBNAME As Long = 1620   '現場リストカラム幅(Twips)、現場名。
            //Private Const COL_WID_PROJECTLIST_DISTRICTNAME As Long =1620'現場リストカラム幅(Twips)、地区名。
            //Private Const COL_WID_PROJECTLIST_FOLDER As Long = 900      '現場リストカラム幅(Twips)、フォルダ。
            //Private Const COL_WID_PROJECTLIST_MDATE As Long = 1700      '現場リストカラム幅(Twips)、最終更新日。
            //Private Const COL_WID_PROJECTLIST_CDATE As Long = 1700      '現場リストカラム幅(Twips)、作成日。
            const int COL_WID_PROJECTLIST_CHECK = 384;                  //'現場リストカラム幅(Twips)、チェックボックス。
            const int COL_WID_PROJECTLIST_JOBNAME = 1620 / 9;           //'現場リストカラム幅(Twips)、現場名。
            const int COL_WID_PROJECTLIST_DISTRICTNAME = 1620 / 9;      //'現場リストカラム幅(Twips)、地区名。。
            const int COL_WID_PROJECTLIST_FOLDER = 900 / 9;             //'現場リストカラム幅(Twips)、フォルダ。
            const int COL_WID_PROJECTLIST_MDATE = 1400 / 9;             //'現場リストカラム幅(Twips)、最終更新日。
            const int COL_WID_PROJECTLIST_CDATE = 1400 / 9;             //'現場リストカラム幅(Twips)、作成日。


            InitializeComponent();

            //23/12/26 K.setoguchi@NV---------->>>>>>>>>>
            Result = DEFINE.vbCancel;    /// As Long 'ダイアログの結果。
            //<<<<<<<<<-----------23/12/26 K.setoguchi@NV



            //**************************************
            //* 現場の選択　リストを表示
            //**************************************


            int nWidth;             //    Dim nWidth As Long
            long nTotalWidth;       //    Dim nTotalWidth As Long

            lvProject.View = View.Details;
            lvProject.Columns.Clear();

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



            ProjectFileManager ProjectFile = new ProjectFileManager();

            //現場の情報を取得
            int listcount = ProjectFile.GetProjectListCount();

            //現場リスト数により、現場リスト領域を確保
            var Genba = new GENBA_STRUCT[listcount];

            //現場の情報を取得
            int datacount = 0;
            ProjectFile.MakeProjectListView(Genba, listcount, ref datacount);

            //現場の情報を表示
            for (int i = 0; i < datacount; i++)
            {
                var item = new ListViewItem(Genba[i].sJobNames);
                item.SubItems.Add(Genba[i].sDistrictNames);
                item.SubItems.Add(Genba[i].sFolderNames);
                item.SubItems.Add(Genba[i].tModTime.ToString("yyyy/MM/dd HH:mm"));
                item.SubItems.Add(Genba[i].tCreateTime.ToString("yyyy/MM/dd HH:mm"));
                lvProject.Items.Add(item);
            }
            Result = DEFINE.vbCancel;         // As Long 'ダイアログの結果。

    }

    private void OKButton_Click(object sender, EventArgs e)
        {
            //DEBUG>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
            // 選択項目を取得する
            //ListViewItem itemx = lvProject.SelectedItems[0];
            //MessageBox.Show(itemx.Text + " | " + itemx.SubItems[1].Text + " | " + itemx.SubItems[2].Text); 
            //            lvProject.View = View.LargeIcon;
            //DEBUG<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<

            try
            {
                ListViewItem itemx = lvProject.SelectedItems[0];
                if (itemx == null)
                {
                    _ = MessageBox.Show(itemx.Text + " | " + itemx.SubItems[1].Text + " | " + itemx.SubItems[2].Text);
                    return;

                }

                Result = DEFINE.vbOK;         // As Long 'ダイアログの結果。


            }
            catch (ArgumentOutOfRangeException)
            {
                _ = MessageBox.Show("「現場の選択画面」の操作を確認して下さい ", "エラー発生", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //メインフォームに送信する
            if (fMain != null)
            {
                fMain.ReceiveData = "OK";

                //23/12/26 K.setoguchi@NV---------->>>>>>>>>>
                Result = DEFINE.vbOK;    /// As Long 'ダイアログの結果。
                //<<<<<<<<<-----------23/12/26 K.setoguchi@NV

            }
            this.Close();

            //23/12/26 K.setoguchi@NV---------->>>>>>>>>>
            //Result = DEFINE.vbCancel;    /// As Long 'ダイアログの結果。
           //<<<<<<<<<-----------23/12/26 K.setoguchi@NV

        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            //メインフォームに送信する
            if (fMain != null) {
                fMain.ReceiveData = "NG";
            }
            //lvProject.Columns.Clear();
            //lvProject.Items.Clear();
           this.Close();
        }

        public void lvProject_SelectedIndexChanged(object sender, EventArgs e)
        {
         //  lvProject.FullRowSelect = true;
         // lvProject.View = View.LargeIcon;
        }
}
}
