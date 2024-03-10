using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SurvLine
{
    public partial class frmEccentricCorrection : Form
    {

        //'*******************************************************************************
        //'オプション画面
        //
        //Option Explicit
        //
        //'*******************************************************************************

        //1==========================================================================================
        /*[VB]
            'タブ番号。
            Private Enum TAB_NUMBER
                TAB_ANGLE = 0 '角度。
                TAB_DISTANCE '距離。
                TAB_ELEVATION '高度角補正。
            End Enum

            'プロパティ
            Public Result As Long 'ダイアログの結果。
            Public GenuineNumber As String '本点番号。
            Public GenuineName As String '本点名称。

            '2007/3/15 追加 NGS Yamada
            Public MarkNumber As String
            Public MarkName As String

            'インプリメンテーション
            Private m_clsEccentricCorrectionParam As New EccentricCorrectionParam '偏心補正パラメータ。
            Private m_clsObservationPoint As ObservationPoint '観測点(HeadPoint)。
            Private m_clsMarkers() As BaseLineVector '方位標ベクトル。配列の要素は(-1 To ...)、要素 -1 は未使用。

            Private m_bUsePointChanging As Boolean '偏心座標候補コンボボックスがChange中。'2009/11 H.Nakamura
            Private m_clsMarkPoint As New ObservationPoint  '手入力時の方位標 2007/3/15 NGS Yamada
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]

        //'タブ番号。
        private enum TAB_NUMBER
        {
            TAB_ANGLE = 0,      //'角度。
            TAB_DISTANCE,       //'距離。
            TAB_ELEVATION,      //'高度角補正。
        }

        //'プロパティ
        public long Result;             //'ダイアログの結果。
        public string GenuineNumber;    //'本点番号。
        public string GenuineName;      //'本点名称。

        //'2007/3/15 追加 NGS Yamada
        public string MarkNumber;
        public string MarkName;

#if false
        //'インプリメンテーション
        private EccentricCorrectionParam m_clsEccentricCorrectionParam = new EccentricCorrectionParam();  //'偏心補正パラメータ。


        private ObservationPoint m_clsObservationPoint;                 //'観測点(HeadPoint)。
        private m_clsMarkers() As BaseLineVector '方位標ベクトル。配列の要素は(-1 To ...)、要素 -1 は未使用。

        private m_bUsePointChanging As Boolean '偏心座標候補コンボボックスがChange中。'2009/11 H.Nakamura
        private m_clsMarkPoint As New ObservationPoint  '手入力時の方位標 2007/3/15 NGS Yamada
#endif


        //------------------------------------------------------------------------------------------
        //[C#]
        public frmEccentricCorrection()
        {
            InitializeComponent();

            //  EccentricCorrectionParam eccentricCorrectionParam = new EccentricCorrectionParam();
        }







    private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
