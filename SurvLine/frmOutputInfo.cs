using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static SurvLine.MdlAccountMakeNSS;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace SurvLine
{
    public partial class frmOutputInfo : Form
    {
        //'*******************************************************************************
        //'ファイル出力情報画面
        //
        //Option Explicit

        //==========================================================================================
        /*[VB]
            'プロパティ
            Public Result As Long 'ダイアログの結果。
            Public AccountType As ACCOUNT_TYPE '帳票種別。
            Public EnableAutomation As Boolean 'OLEオートメーション有効フラグ。True=OLEオートメーションは有効。False=OLEオートメーションは無し。

            'インプリメンテーション
            Private m_clsOutputParam As New OutputParam '外部出力ファイル出力パラメータ。
            Private m_objFixed As Collection '固定点コレクション。要素は ObservationPoint オブジェクト(HeadPoint)。キーは要素のポインタ。'2023/06/26 Hitz H.Nakamura GNSS水準測量対応。前半後半較差の追加。
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        public long Result;     //'ダイアログの結果。
        //public ACCOUNT_TYPE AccountType;  //'帳票種別。
        public long AccountType;            //'帳票種別。
        public bool EnableAutomation;       //'OLEオートメーション有効フラグ。True=OLEオートメーションは有効。False=OLEオートメーションは無し。





        public frmOutputInfo()
        {
            InitializeComponent();
        }

        private void frmOutputInfo_Load(object sender, EventArgs e)
        {

        }






    }
}
