using SurvLine.mdl;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace SurvLine
{
    public partial class frmJobSelect2 : Form
    {

        //'*******************************************************************************
        //'現場の複数選択画面
        //
        //Option Explicit
        //
        //'プロパティ
        public long Result;             // As Long 'ダイアログの結果。
        public string MsgOK;            // As String 'OKボタンが押された時のメッセージ。空文字ならメッセージは出力しない。
        public string MsgUnselected;    // As String '未選択警告メッセージ。

        //'インプリメンテーション
        List<string> m_sFolderNames;    //private m_sFolderNames() As String '選択された現場のフォルダ名。配列の要素は(-1 To ...)、要素 -1 は未使用。

        private long m_nSortIndex;      // As Long 'ソートカラムインデックス。


        //24/01/24 K.setoguchi@NV---------->>>>>>>>>>
        //***************************************************************************
        const string COL_NAM_PROJECTLIST_CHECK = "";               //'現場リストカラム名称、チェックボックス。
        const string COL_NAM_PROJECTLIST_JOBNAME = "現場名";       //'現場リストカラム名称、現場名。
        const string COL_NAM_PROJECTLIST_DISTRICTNAME = "地区名";  // '現場リストカラム名称、地区名。
        const string COL_NAM_PROJECTLIST_FOLDER = "フォルダ";      //'現場リストカラム名称、フォルダ。
        const string COL_NAM_PROJECTLIST_MDATE = "最終更新日";     //'現場リストカラム名称、最終更新日。
        const string COL_NAM_PROJECTLIST_CDATE = "作成日";         //'現場リストカラム名称、作成日。

        const int COL_WID_PROJECTLIST_CHECK = 384;                  //'現場リストカラム幅(Twips)、チェックボックス。
        const int COL_WID_PROJECTLIST_JOBNAME = 1620 / 9;           //'現場リストカラム幅(Twips)、現場名。
        const int COL_WID_PROJECTLIST_DISTRICTNAME = 1620 / 9;      //'現場リストカラム幅(Twips)、地区名。。
        const int COL_WID_PROJECTLIST_FOLDER = 900 / 9;             //'現場リストカラム幅(Twips)、フォルダ。
        const int COL_WID_PROJECTLIST_MDATE = 1400 / 9;             //'現場リストカラム幅(Twips)、最終更新日。
        const int COL_WID_PROJECTLIST_CDATE = 1400 / 9;             //'現場リストカラム幅(Twips)、作成日。
                                                                    //<<<<<<<<<-----------24/01/24 K.setoguchi@NV
                                                                    //***************************************************************************


        //'*******************************************************************************
        //'プロパティ
        //'*******************************************************************************

        //==========================================================================================
        /*[VB]
        '説明文。
        Property Let Description(ByVal sDescription As String)
            lblDescription.Caption = sDescription
        End Property
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        public void Description(string sDescription)
        {
            lblDescription.Text = sDescription;
        }

        //==========================================================================================
        /*[VB]
        'OKボタンのテキスト。
        Property Let TextOK(ByVal sTextOK As String)
            OKButton.Caption = sTextOK
        End Property
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        public void TextOK(string sTextOK)
        {
            OKButton.Text = sTextOK;
        }

        //==========================================================================================
        /*[VB]
        '選択された現場のフォルダ名。配列の要素は(-1 To ...)、要素 -1 は未使用。
        Property Get FolderNames() As String()
            FolderNames = m_sFolderNames
        End Property
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'選択された現場のフォルダ名。配列の要素は(-1 To ...)、要素 -1 は未使用。
        public string FolderNames()
        {

            ListViewItem itemx = lvProject.SelectedItems[0];
            if (itemx == null)
            {
                MessageBox.Show(itemx.Text + " | " + itemx.SubItems[1].Text + " | " + itemx.SubItems[2].Text);

            }
            return itemx.SubItems[2].Text;
            //return m_sFolderNames[(int)lvProject.];
        }

        //'*******************************************************************************
        //'イベント
        //'*******************************************************************************

        //'初期化。
        public frmJobSelect2()
        {
            InitializeComponent();
            this.Load += Form_Load;
        }


        //==========================================================================================
        /*[VB]
        '初期化。
        Private Sub Form_Load()

            On Error GoTo ErrorHandler
    
            '変数初期化。
            Result = vbCancel
            m_nSortIndex = -1
    
            '現場リストの作成。
            Dim clsProjectFileManager As New ProjectFileManager
            Call clsProjectFileManager.MakeProjectListView(lvProject, True)
    
            'ソート。
            lvProject.SortKey = 3
    
            '選択行の初期化。
            If lvProject.ListItems.Count > 0 Then Set lvProject.SelectedItem = lvProject.ListItems(1)


            Exit Sub


        ErrorHandler:
            Call mdlMain.ErrorExit

        End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form_Load(object sender, EventArgs e)
        {

            //23/12/26 K.setoguchi@NV---------->>>>>>>>>>
            Result = MdiDefine.DEFINE.vbCancel;    /// As Long 'ダイアログの結果。
            //<<<<<<<<<-----------23/12/26 K.setoguchi@NV

            List<string> m_sFolderNamesA = new List<string>();
            m_sFolderNames = m_sFolderNamesA;


#if false   //------------------------------

    坂井様へ　以降のご対応をお願い致します。

#endif      //-----------------------------



            //**************************************
            //* 現場を削除の選択　リストを表示
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

                m_sFolderNames.Add(Genba[i].sFolderNames);     //データ


                item.SubItems.Add(Genba[i].tModTime.ToString("yyyy/MM/dd HH:mm"));
                item.SubItems.Add(Genba[i].tCreateTime.ToString("yyyy/MM/dd HH:mm"));
                lvProject.Items.Add(item);
            }

        }


        //==========================================================================================
        /*[VB]
        'ソート。
        Private Sub lvProject_ColumnClick(ByVal ColumnHeader As MSComctlLib.ColumnHeader)

            On Error GoTo ErrorHandler


            If m_nSortIndex = ColumnHeader.Index Then
                If lvProject.SortOrder = lvwAscending Then
                    lvProject.SortOrder = lvwDescending
                Else
                    lvProject.SortOrder = lvwAscending
                End If
            Else
                m_nSortIndex = ColumnHeader.Index
                lvProject.SortOrder = lvwAscending
            End If
            lvProject.SortKey = m_nSortIndex - 1


            Exit Sub


        ErrorHandler:
            Call mdlMain.ErrorExit

        End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
#if false   //------------------------------

    坂井様へ　以降のご対応をお願い致します。
        private void lvProject_ColumnClick(MSComctlLib.ColumnHeader ColumnHeader)
        {

        }

#endif      //-----------------------------


        //==========================================================================================
        /*[VB]
        '全選択。
        Private Sub cmdSelect_Click()

            On Error GoTo ErrorHandler

            Dim lvItem As ListItem
            For Each lvItem In lvProject.ListItems
                lvItem.Checked = True
            Next

            Exit Sub

        ErrorHandler:
            Call mdlMain.ErrorExit

        End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /// <summary>
        /// '全選択。
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdSelect_Click(object sender, EventArgs e)
        {
#if false   //------------------------------

    坂井様へ　以降のご対応をお願い致します。

#endif      //-----------------------------

        }



        //==========================================================================================
        /*[VB]
        '全解除。
        Private Sub cmdUnselect_Click()

            On Error GoTo ErrorHandler

            Dim lvItem As ListItem
            For Each lvItem In lvProject.ListItems
                lvItem.Checked = False
            Next

            Exit Sub

        ErrorHandler:
            Call mdlMain.ErrorExit

        End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //全選択
        private void cmdUnselect_Click(object sender, EventArgs e)
        {
#if false   //------------------------------

    坂井様へ　以降のご対応をお願い致します。

#endif      //-----------------------------

        }



        //==========================================================================================
        /*[VB]
        'キャンセルでダイアログを終了する。
        Private Sub CancelButton_Click()

            On Error GoTo ErrorHandler

            Call Unload(Me)

            Exit Sub

        ErrorHandler:
            Call mdlMain.ErrorExit
                End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /// <summary>
        /// キャンセルでダイアログを終了する。
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelButton_Click(object sender, EventArgs e)
        {
            Result = MdiDefine.DEFINE.vbCancel;    /// As Long 'ダイアログの結果。

            this.Close();

        }

        //==========================================================================================
        /*[VB]
        'ＯＫでダイアログを終了する。
        Private Sub OKButton_Click()

            On Error GoTo ErrorHandler
    
            '入力値の検査。
            If Not CheckData() Then Exit Sub
            '確認。
            If MsgOK <> "" Then
                If MsgBox(MsgOK, vbOKCancel + vbExclamation) <> vbOK Then Exit Sub
            End If
            '値を反映させる。
            Call ReflectData
            '終了。
            Result = vbOK
            Call Unload(Me)


            Exit Sub


        ErrorHandler:
            Call mdlMain.ErrorExit

        End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /// <summary>
        /// 'ＯＫでダイアログを終了する。
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OKButton_Click(object sender, EventArgs e)
        {
            try
            {
                ListViewItem itemx = lvProject.SelectedItems[0];
                if (itemx == null)
                {
                    MessageBox.Show(itemx.Text + " | " + itemx.SubItems[1].Text + " | " + itemx.SubItems[2].Text);

                }
            }
            catch (ArgumentOutOfRangeException)
            {
                MessageBox.Show("「現場を削除 画面」の操作を確認して下さい ");
                return;
            }

            Result = MdiDefine.DEFINE.vbOK;    /// As Long 'ダイアログの結果。

            this.Close();


        }


        //==========================================================================================
        /*[VB]
        '*******************************************************************************
        'インプリメンテーション

        '入力値を検査する。
        '
        '戻り値：
        '入力値が正常である場合 True を返す。
        'それ以外の場合 False を返す。
        Private Function CheckData() As Boolean

            CheckData = False


            Dim nCount As Long
            nCount = 0
            Dim lvItem As ListItem
            For Each lvItem In lvProject.ListItems
                If lvItem.Checked Then nCount = nCount + 1
            Next

            If nCount <= 0 Then
                Call MsgBox(MsgUnselected, vbCritical)
                Exit Function
            End If

            CheckData = True


        End Function
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /// <summary>
        /// インプリメンテーション
        ///
        /// 入力値を検査する。
        /// 
        /// </summary>
        /// <returns>
        ///  '戻り値：
        ///     '入力値が正常である場合 True を返す。
        ///     'それ以外の場合 False を返す。
        /// </returns>
        private bool CheckData()
        {

            return true;
        }


        //==========================================================================================
        /*[VB]
        '値を反映させる。
        Private Sub ReflectData()
            ReDim m_sFolderNames(-1 To -1)
            Dim lvItem As ListItem
            For Each lvItem In lvProject.ListItems
                If lvItem.Checked Then
                    ReDim Preserve m_sFolderNames(-1 To UBound(m_sFolderNames) + 1)
                    m_sFolderNames(UBound(m_sFolderNames)) = Mid$(lvItem.Key, Len(KEYPREFIX) + 1)
                End If
            Next
        End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /// <summary>
        /// 値を反映させる。
        /// 
        /// </summary>
        private void ReflectData()
        {

        }

    }
}
