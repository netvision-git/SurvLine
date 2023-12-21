using SurvLine.mdl;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
//using static SurvLine.mdl.MdlListPane;

namespace SurvLine
{

    public partial class ListPane : UserControl
    {


        MdlListPane mdilistPane = new MdlListPane();

        public long m_list;

        //コンストラクタ
        public ListPane()
        {
            InitializeComponent();


        }



        //**************************************************************************************
        //**************************************************************************************
        /// <summary>
        /// '初期化 ListPane
        /// 引き数：
        //  ListPane：観測点＆ベクトル表示
        /// </summary>
        /// <param name="ListPane"></param>
        /// <returns>
        /// </returns>
        //**************************************************************************************
        public void UserControl_Initialize(ListPane listPane)
        {


            listPane.Height = 600;
            listPane.objTab.Width = 800;
            listPane.objTab.Height = 500;



            //[VB]  'リストの作成。
            //[VB]  For i = 0 To LIST_NUM_COUNT - 1
            //[VB]      Call MakeList(i, grdFlexGrid(i))
            //[VB]  Next
            //'リストの作成.
            for (int i = 0; i < (int)MdlListPane.LIST_NUM_PANE.LIST_NUM_COUNT; i++)
            {
                mdilistPane.MakeList((long)i, listPane);
            }


        }
        //--------------------------------------------------------------------------------
        //[VB]  '初期化。
        //[VB]  Private Sub UserControl_Initialize()
        //[VB]  
        //[VB]  '   ユーザーコントロールの中でユーザーコントロールをLoadすると、コンパイル時に実行時エラー7が発生する。
        //[VB]  '   コンパイル時にEndステートメントで終了するとexeファイルが生成されない。
        //[VB]  '   しょうがないのでエラーを無視する。
        //[VB]  '    On Error GoTo ErrorHandler
        //[VB]  On Error Resume Next
        //[VB]  
        //[VB]  '非表示リスト選択オブジェクトコレクション。
        //[VB]  Dim i As Long
        //[VB]  For i = 0 To LIST_OBJ_TYPE_INDEX_COUNT - 1
        //[VB]  Set m_objHideSelected(i) = New Collection
        //[VB]  Next
        //[VB]  
        //[VB]  'ソートオーダー。
        //[VB]  For i = 0 To LIST_NUM_COUNT - 1
        //[VB]      Set m_clsListSort(i) = New ListSort
        //[VB]  Next
        //[VB]  Call InitializeSortOrder(m_clsListSort)
        //[VB]  
        //[VB]  'タブコントロール。
        //[VB]  objTab.Left = ScaleLeft
        //[VB]  objTab.Top = ScaleTop
        //[VB]  objTab.TabsPerRow = LIST_NUM_COUNT
        //[VB]  objTab.Tabs = LIST_NUM_COUNT
        //[VB]  
        //[VB]  'リストのロード
        //[VB]  For i = 1 To LIST_NUM_COUNT - 1
        //[VB]  Load picEdgeMask(i)
        //[VB]  Load grdFlexGrid(i)
        //[VB]  picEdgeMask(i).Visible = True
        //[VB]  grdFlexGrid(i).Visible = True
        //[VB]  Next
        //[VB]  
        //[VB]  For i = 0 To LIST_NUM_COUNT - 1
        //[VB]  'エッジマスク。
        //[VB]  picEdgeMask(i).BorderStyle = vbBSNone
        //[VB]  '        picEdgeMask(i).Left = 0 'Left を設定するとタブ切り替えによる表示/非表示がうまく働かない・・・。リソースエディタで設定すること！
        //[VB]  picEdgeMask(i).Height = EDGE_MASK_HEIGHT * Screen.TwipsPerPixelY
        //[VB]  'グリッド。
        //[VB]  '        grdFlexGrid(i).Left = 0 'Left を設定するとタブ切り替えによる表示/非表示がうまく働かない・・・。リソースエディタで設定すること！
        //[VB]  grdFlexGrid(i).Top = 0
        //[VB]  grdFlexGrid(i).TabStop = False
        //[VB]  Next
        //[VB]  
        //[VB]  'タブストップ。
        //[VB]  grdFlexGrid(objTab.Tab).TabStop = True
        //[VB]  
        //[VB]  
        //[VB]  ReDim m_objMap(LIST_NUM_COUNT - 1)
        //[VB]  
        //[VB]  'リストの作成。
        //[VB]  For i = 0 To LIST_NUM_COUNT - 1
        //[VB]      Call MakeList(i, grdFlexGrid(i))
        //[VB]  Next
        //[VB]  
        //[VB]  Exit Sub
        //[VB]  
        //[VB]  ErrorHandler:
        //[VB]  Call mdlMain.ErrorExit
        //[VB]  
        //[VB]  
        //[VB]  End Sub
        //'*******************************************************************************
        //'*******************************************************************************


        public void ListDataDispMain()
        {
            ListPane listPane = new ListPane();

            mdilistPane.ListDataDisp(listPane);

        }
        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void ListPane_Load(object sender, EventArgs e)
        {

            ListPane listPane = (ListPane)sender;

            m_list = 12;


            UserControl_Initialize(listPane);

        }

        private void grdFlexGrid1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        public long M_list
        {
            set
            {
                m_list = value;
            }
            get
            {
                return m_list;
            }
        }

    }
}
