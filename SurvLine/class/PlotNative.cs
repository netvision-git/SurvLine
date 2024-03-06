using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static SurvLine.mdl.MdlPlotProc;
using static SurvLine.mdl.DEFINE;
using static SurvLine.mdl.MdlNSSDefine;
using static SurvLine.mdl.MdlNSDefine;
using static SurvLine.frmMain;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Runtime.InteropServices;
using System.IO;
using static System.Net.WebRequestMethods;
using System.Collections;
using System.Xml.Linq;
using static SurvLine.PlotPane;
using static SurvLine.mdl.MdlUtility;
using static SurvLine.ClickManager;
using static SurvLine.mdl.MdlBaseLineAnalyser;
using static SurvLine.mdl.MdlNSUtility;
using static SurvLine.ChainList;
using static SurvLine.ObservationPoint;
using SurvLine.mdl;
using static System.Net.Mime.MediaTypeNames;
using System.Drawing.Drawing2D;

namespace SurvLine
{
    public class PlotNative
    {
        //==========================================================================================
        /*[VB]
        '*******************************************************************************
        'プロットペインネイティブコード

        Option Explicit
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '変数
        Private m_PlotPointLabel As Long 'プロット画面に表示するラベルの種類。0=観測点No,1=観測点名称,2=観測点No(観測点名称)。 2006/10/6 NGS Yamada
        Private m_DisableVectorVisible As Boolean '「無効」なベクトルをプロット画面に表示するか？。True=表示,False=非表示。 2006/10/10 NGS Yamada
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'変数
        private long m_PlotPointLabel;              //'プロット画面に表示するラベルの種類。0=観測点No,1=観測点名称,2=観測点No(観測点名称)。 2006/10/6 NGS Yamada
        private bool m_DisableVectorVisible;        //'「無効」なベクトルをプロット画面に表示するか？。True=表示,False=非表示。 2006/10/10 NGS Yamada
        private MdlMain m_clsMdlMain;               //MdlMainインスタンス
        private MdlPlotProc m_clsMdlPlotProc;       //MdlPlotProcインスタンス

        //==========================================================================================


        //==========================================================================================
        /*[VB]
        '線の太さ 2006/10/10 NGS Yamada
        Private Vector_Width_Normal As Long       '通常
        Private Vector_Width_Emphasis As Long     '強調
        Private Vector_Width_Duplication As Long  '重複
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'線の太さ 2006/10/10 NGS Yamada
        private long Vector_Width_Normal;           //'通常
        private long Vector_Width_Emphasis;         //'強調
        private long Vector_Width_Duplication;      //'重複
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'シンボルの色
        Private Obspnt_Color_Symbol_Enable As Long  '有効観測点記号色。
        Private Obspnt_Color_Symbol_Disable As Long  '無効観測点記号色。
        Private Obspnt_Color_Symbol_Fixed As Long  '固定観測点記号色。
        Private Obspnt_Color_Symbol_Emphasis As Long  '強調観測点記号色。
        Private Obspnt_Color_Symbol_Emphasis_Disable As Long  '強調無効観測点記号色。
        Private Obspnt_Color_Symbol_Emphasis_Fixed As Long  '強調固定観測点記号色。
        Private Obspnt_Color_Symbol_Angle As Long '登録閉合観測点記号色。
        Private Obspnt_Color_Symbol_Angle_Fixed As Long  '登録閉合固定観測点記号色。
        Private Obspnt_Color_Symbol_Candidate As Long  '閉合候補観測点記号色。
        Private Obspnt_Color_Symbol_Candidate_Fixed As Long  '閉合候補固定観測点記号色。
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'シンボルの色
        private long Obspnt_Color_Symbol_Enable;            //'有効観測点記号色。
        private long Obspnt_Color_Symbol_Disable;           //'無効観測点記号色。
        private long Obspnt_Color_Symbol_Fixed;             //'固定観測点記号色。
        private long Obspnt_Color_Symbol_Emphasis;          //'強調観測点記号色。
        private long Obspnt_Color_Symbol_Emphasis_Disable;  //'強調無効観測点記号色。
        private long Obspnt_Color_Symbol_Emphasis_Fixed;    //'強調固定観測点記号色。
        private long Obspnt_Color_Symbol_Angle;             //'登録閉合観測点記号色。
        private long Obspnt_Color_Symbol_Angle_Fixed;       //'登録閉合固定観測点記号色。
        //  private long Obspnt_Color_Symbol_Candidate;         //'閉合候補観測点記号色。
        //  private long Obspnt_Color_Symbol_Candidate_Fixed;   //'閉合候補固定観測点記号色。
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '定数
        'Private Const OBSPNT_COLOR_ENABLE As Long = &HFFFF& '有効観測点色。
        'Private Const OBSPNT_COLOR_DISABLE As Long = &H666666 '無効観測点色。
        'Private Const OBSPNT_COLOR_FIXED As Long = &HFF9999 '固定観測点色。
        'Private Const OBSPNT_COLOR_EMPHASIS As Long = &H66FFFF '強調観測点色。
        'Private Const OBSPNT_COLOR_EMPHASIS_DISABLE As Long = &H999999 '強調無効観測点色。
        'Private Const OBSPNT_COLOR_EMPHASIS_FIXED As Long = &HFF99CC '強調固定観測点色。
        'Private Const OBSPNT_COLOR_ANGLE As Long = &H99FF& '登録閉合観測点色。
        'Private Const OBSPNT_COLOR_ANGLE_FIXED As Long = &HFF& '登録閉合固定観測点色。
        'Private Const OBSPNT_COLOR_CANDIDATE As Long = &HFFCC33 '閉合候補観測点色。
        'Private Const OBSPNT_COLOR_CANDIDATE_FIXED As Long = &HFF0000 '閉合候補固定観測点色。
        'Private Const OBSPNT_COLOR_SYMBOL_ENABLE As Long = &H33FF66 '有効観測点記号色。
        'Private Const OBSPNT_COLOR_SYMBOL_DISABLE As Long = &H666666 '無効観測点記号色。
        'Private Const OBSPNT_COLOR_SYMBOL_FIXED As Long = &HFF66FF '固定観測点記号色。
        'Private Const OBSPNT_COLOR_SYMBOL_EMPHASIS As Long = &H66FFCC '強調観測点記号色。
        'Private Const OBSPNT_COLOR_SYMBOL_EMPHASIS_DISABLE As Long = &H999999 '強調無効観測点記号色。
        'Private Const OBSPNT_COLOR_SYMBOL_EMPHASIS_FIXED As Long = &HFF99FF '強調固定観測点記号色。
        'Private Const OBSPNT_COLOR_SYMBOL_ANGLE As Long = &HFF& '登録閉合観測点記号色。
        'Private Const OBSPNT_COLOR_SYMBOL_ANGLE_FIXED As Long = &HFF& '登録閉合固定観測点記号色。
        'Private Const OBSPNT_COLOR_SYMBOL_CANDIDATE As Long = &HFFCC99 '閉合候補観測点記号色。
        'Private Const OBSPNT_COLOR_SYMBOL_CANDIDATE_FIXED As Long = &HFFCC66 '閉合候補固定観測点記号色。

        'Private Const VECTOR_COLOR_ENABLE As Long = &HFFFFFF '未解析基線ベクトル色。
        'Private Const VECTOR_COLOR_DISABLE As Long = &H666666 '無効基線ベクトル色。
        'Private Const VECTOR_COLOR_FLOAT As Long = &HFFFF& 'FLOAT基線ベクトル色。
        'Private Const VECTOR_COLOR_FIX As Long = &HFF00& 'FIX基線ベクトル色。
        'Private Const VECTOR_COLOR_FAILED As Long = &HFF& '解析失敗基線ベクトル色。
        'Private Const VECTOR_COLOR_ANGLE As Long = &H66CCFF '登録閉合基線ベクトル色。
        'Private Const VECTOR_COLOR_CANDIDATE As Long = &HFF9900 '閉合候補基線ベクトル色。
        'Private Const VECTOR_COLOR_CORRECT As Long = &HFFFFCC '偏心補正基線ベクトル色。
        'Private Const VECTOR_COLOR_ECCENTRIC As Long = &HCCFFFF '偏心ベクトル色。
        'Private Const VECTOR_COLOR_EMPHASIS As Long = &HFFFFFF '強調未解析基線ベクトル色。
        'Private Const VECTOR_COLOR_EMPHASIS_DISABLE As Long = &H999999 '強調無効基線ベクトル色。
        'Private Const VECTOR_COLOR_EMPHASIS_FLOAT As Long = &H66FFFF '強調FLOAT基線ベクトル色。
        'Private Const VECTOR_COLOR_EMPHASIS_FIX As Long = &H66FF66 '強調FIX基線ベクトル色。
        'Private Const VECTOR_COLOR_EMPHASIS_FAILED As Long = &H6666FF '強調解析失敗基線ベクトル色。
        'Private Const VECTOR_COLOR_EMPHASIS_CANDIDATE As Long = &HFFFFCC '強調閉合候補基線ベクトル色。

        Private Const PROFILE_OBSPNT_COLOR_SEC As String = "OBSPNT_COLOR" 'ベクトルの色。
        Private Const OBSPNT_COLOR_KEY_ENABLE_R As String = "ENABLE_R" '有効観測点色。
        Private Const OBSPNT_COLOR_KEY_ENABLE_G As String = "ENABLE_G"
        Private Const OBSPNT_COLOR_KEY_ENABLE_B As String = "ENABLE_B"
        Private Const OBSPNT_COLOR_KEY_DISABLE_R As String = "DISABLE_R" '無効観測点色。
        Private Const OBSPNT_COLOR_KEY_DISABLE_G As String = "DISABLE_G"
        Private Const OBSPNT_COLOR_KEY_DISABLE_B As String = "DISABLE_B"
        Private Const OBSPNT_COLOR_KEY_FIXED_R As String = "FIXED_R" '固定観測点色。
        Private Const OBSPNT_COLOR_KEY_FIXED_G As String = "FIXED_G"
        Private Const OBSPNT_COLOR_KEY_FIXED_B As String = "FIXED_B"
        Private Const OBSPNT_COLOR_KEY_EMPHASIS_R As String = "EMPHASIS_R" '強調観測点色。
        Private Const OBSPNT_COLOR_KEY_EMPHASIS_G As String = "EMPHASIS_G"
        Private Const OBSPNT_COLOR_KEY_EMPHASIS_B As String = "EMPHASIS_B"
        Private Const OBSPNT_COLOR_KEY_EMPHASIS_DISABLE_R As String = "EMPHASIS_DISABLE_R" '強調無効観測点色。
        Private Const OBSPNT_COLOR_KEY_EMPHASIS_DISABLE_G As String = "EMPHASIS_DISABLE_G"
        Private Const OBSPNT_COLOR_KEY_EMPHASIS_DISABLE_B As String = "EMPHASIS_DISABLE_B"
        Private Const OBSPNT_COLOR_KEY_EMPHASIS_FIXED_R As String = "EMPHASIS_FIXED_R" '強調固定観測点色。
        Private Const OBSPNT_COLOR_KEY_EMPHASIS_FIXED_G As String = "EMPHASIS_FIXED_G"
        Private Const OBSPNT_COLOR_KEY_EMPHASIS_FIXED_B As String = "EMPHASIS_FIXED_B"
        Private Const OBSPNT_COLOR_KEY_ANGLE_R As String = "ANGLE_R" '登録閉合観測点色。
        Private Const OBSPNT_COLOR_KEY_ANGLE_G As String = "ANGLE_G"
        Private Const OBSPNT_COLOR_KEY_ANGLE_B As String = "ANGLE_B"
        Private Const OBSPNT_COLOR_KEY_ANGLE_FIXED_R As String = "ANGLE_FIXED_R" '登録閉合固定観測点色。
        Private Const OBSPNT_COLOR_KEY_ANGLE_FIXED_G As String = "ANGLE_FIXED_G"
        Private Const OBSPNT_COLOR_KEY_ANGLE_FIXED_B As String = "ANGLE_FIXED_B"
        Private Const OBSPNT_COLOR_KEY_CANDIDATE_R As String = "CANDIDATE_R" '閉合候補観測点色。
        Private Const OBSPNT_COLOR_KEY_CANDIDATE_G As String = "CANDIDATE_G"
        Private Const OBSPNT_COLOR_KEY_CANDIDATE_B As String = "CANDIDATE_B"
        Private Const OBSPNT_COLOR_KEY_CANDIDATE_FIXED_R As String = "CANDIDATE_FIXED_R" '閉合候補固定観測点色。
        Private Const OBSPNT_COLOR_KEY_CANDIDATE_FIXED_G As String = "CANDIDATE_FIXED_G"
        Private Const OBSPNT_COLOR_KEY_CANDIDATE_FIXED_B As String = "CANDIDATE_FIXED_B"
        Private Const OBSPNT_COLOR_KEY_SYMBOL_ENABLE_R As String = "SYMBOL_ENABLE_R" '有効観測点記号色。
        Private Const OBSPNT_COLOR_KEY_SYMBOL_ENABLE_G As String = "SYMBOL_ENABLE_G"
        Private Const OBSPNT_COLOR_KEY_SYMBOL_ENABLE_B As String = "SYMBOL_ENABLE_B"
        Private Const OBSPNT_COLOR_KEY_SYMBOL_DISABLE_R As String = "SYMBOL_DISABLE_R" '無効観測点記号色。
        Private Const OBSPNT_COLOR_KEY_SYMBOL_DISABLE_G As String = "SYMBOL_DISABLE_G"
        Private Const OBSPNT_COLOR_KEY_SYMBOL_DISABLE_B As String = "SYMBOL_DISABLE_B"
        Private Const OBSPNT_COLOR_KEY_SYMBOL_FIXED_R As String = "SYMBOL_FIXED_R" '固定観測点記号色。
        Private Const OBSPNT_COLOR_KEY_SYMBOL_FIXED_G As String = "SYMBOL_FIXED_G"
        Private Const OBSPNT_COLOR_KEY_SYMBOL_FIXED_B As String = "SYMBOL_FIXED_B"
        Private Const OBSPNT_COLOR_KEY_SYMBOL_EMPHASIS_R As String = "SYMBOL_EMPHASIS_R" '強調観測点記号色。
        Private Const OBSPNT_COLOR_KEY_SYMBOL_EMPHASIS_G As String = "SYMBOL_EMPHASIS_G"
        Private Const OBSPNT_COLOR_KEY_SYMBOL_EMPHASIS_B As String = "SYMBOL_EMPHASIS_B"
        Private Const OBSPNT_COLOR_KEY_SYMBOL_EMPHASIS_DISABLE_R As String = "SYMBOL_EMPHASIS_DISABLE_R" '強調無効観測点記号色。
        Private Const OBSPNT_COLOR_KEY_SYMBOL_EMPHASIS_DISABLE_G As String = "SYMBOL_EMPHASIS_DISABLE_G"
        Private Const OBSPNT_COLOR_KEY_SYMBOL_EMPHASIS_DISABLE_B As String = "SYMBOL_EMPHASIS_DISABLE_B"
        Private Const OBSPNT_COLOR_KEY_SYMBOL_EMPHASIS_FIXED_R As String = "SYMBOL_EMPHASIS_FIXED_R" '強調固定観測点記号色。
        Private Const OBSPNT_COLOR_KEY_SYMBOL_EMPHASIS_FIXED_G As String = "SYMBOL_EMPHASIS_FIXED_G"
        Private Const OBSPNT_COLOR_KEY_SYMBOL_EMPHASIS_FIXED_B As String = "SYMBOL_EMPHASIS_FIXED_B"
        Private Const OBSPNT_COLOR_KEY_SYMBOL_ANGLE_R As String = "SYMBOL_ANGLE_R" '登録閉合観測点記号色。
        Private Const OBSPNT_COLOR_KEY_SYMBOL_ANGLE_G As String = "SYMBOL_ANGLE_G"
        Private Const OBSPNT_COLOR_KEY_SYMBOL_ANGLE_B As String = "SYMBOL_ANGLE_B"
        Private Const OBSPNT_COLOR_KEY_SYMBOL_ANGLE_FIXED_R As String = "SYMBOL_ANGLE_FIXED_R" '登録閉合固定観測点記号色。
        Private Const OBSPNT_COLOR_KEY_SYMBOL_ANGLE_FIXED_G As String = "SYMBOL_ANGLE_FIXED_G"
        Private Const OBSPNT_COLOR_KEY_SYMBOL_ANGLE_FIXED_B As String = "SYMBOL_ANGLE_FIXED_B"
        Private Const OBSPNT_COLOR_KEY_SYMBOL_CANDIDATE_R As String = "SYMBOL_CANDIDATE_R" '閉合候補観測点記号色。
        Private Const OBSPNT_COLOR_KEY_SYMBOL_CANDIDATE_G As String = "SYMBOL_CANDIDATE_G"
        Private Const OBSPNT_COLOR_KEY_SYMBOL_CANDIDATE_B As String = "SYMBOL_CANDIDATE_B"
        Private Const OBSPNT_COLOR_KEY_SYMBOL_CANDIDATE_FIXED_R As String = "SYMBOL_CANDIDATE_FIXED_R" '閉合候補固定観測点記号色。
        Private Const OBSPNT_COLOR_KEY_SYMBOL_CANDIDATE_FIXED_G As String = "SYMBOL_CANDIDATE_FIXED_G"
        Private Const OBSPNT_COLOR_KEY_SYMBOL_CANDIDATE_FIXED_B As String = "SYMBOL_CANDIDATE_FIXED_B"

        Private Const OBSPNT_COLOR_DEF_ENABLE_R As Long = 255 '有効観測点色。
        Private Const OBSPNT_COLOR_DEF_ENABLE_G As Long = 255
        Private Const OBSPNT_COLOR_DEF_ENABLE_B As Long = 0
        Private Const OBSPNT_COLOR_DEF_DISABLE_R As Long = 102 '無効観測点色。
        Private Const OBSPNT_COLOR_DEF_DISABLE_G As Long = 102
        Private Const OBSPNT_COLOR_DEF_DISABLE_B As Long = 102
        Private Const OBSPNT_COLOR_DEF_FIXED_R As Long = 153 '固定観測点色。
        Private Const OBSPNT_COLOR_DEF_FIXED_G As Long = 153
        Private Const OBSPNT_COLOR_DEF_FIXED_B As Long = 255
        Private Const OBSPNT_COLOR_DEF_EMPHASIS_R As Long = 255 '強調観測点色。
        Private Const OBSPNT_COLOR_DEF_EMPHASIS_G As Long = 255
        Private Const OBSPNT_COLOR_DEF_EMPHASIS_B As Long = 102
        Private Const OBSPNT_COLOR_DEF_EMPHASIS_DISABLE_R As Long = 153 '強調無効観測点色。
        Private Const OBSPNT_COLOR_DEF_EMPHASIS_DISABLE_G As Long = 153
        Private Const OBSPNT_COLOR_DEF_EMPHASIS_DISABLE_B As Long = 153
        Private Const OBSPNT_COLOR_DEF_EMPHASIS_FIXED_R As Long = 204 '強調固定観測点色。
        Private Const OBSPNT_COLOR_DEF_EMPHASIS_FIXED_G As Long = 153
        Private Const OBSPNT_COLOR_DEF_EMPHASIS_FIXED_B As Long = 255
        Private Const OBSPNT_COLOR_DEF_ANGLE_R As Long = 255 '登録閉合観測点色。
        Private Const OBSPNT_COLOR_DEF_ANGLE_G As Long = 153
        Private Const OBSPNT_COLOR_DEF_ANGLE_B As Long = 0
        Private Const OBSPNT_COLOR_DEF_ANGLE_FIXED_R As Long = 255 '登録閉合固定観測点色。
        Private Const OBSPNT_COLOR_DEF_ANGLE_FIXED_G As Long = 0
        Private Const OBSPNT_COLOR_DEF_ANGLE_FIXED_B As Long = 0
        Private Const OBSPNT_COLOR_DEF_CANDIDATE_R As Long = 51 '閉合候補観測点色。
        Private Const OBSPNT_COLOR_DEF_CANDIDATE_G As Long = 204
        Private Const OBSPNT_COLOR_DEF_CANDIDATE_B As Long = 255
        Private Const OBSPNT_COLOR_DEF_CANDIDATE_FIXED_R As Long = 0 '閉合候補固定観測点色。
        Private Const OBSPNT_COLOR_DEF_CANDIDATE_FIXED_G As Long = 0
        Private Const OBSPNT_COLOR_DEF_CANDIDATE_FIXED_B As Long = 255
        Private Const OBSPNT_COLOR_DEF_SYMBOL_ENABLE_R As Long = 102 '有効観測点記号色。
        Private Const OBSPNT_COLOR_DEF_SYMBOL_ENABLE_G As Long = 255
        Private Const OBSPNT_COLOR_DEF_SYMBOL_ENABLE_B As Long = 51
        Private Const OBSPNT_COLOR_DEF_SYMBOL_DISABLE_R As Long = 102 '無効観測点記号色。
        Private Const OBSPNT_COLOR_DEF_SYMBOL_DISABLE_G As Long = 102
        Private Const OBSPNT_COLOR_DEF_SYMBOL_DISABLE_B As Long = 102
        Private Const OBSPNT_COLOR_DEF_SYMBOL_FIXED_R As Long = 255 '固定観測点記号色。
        Private Const OBSPNT_COLOR_DEF_SYMBOL_FIXED_G As Long = 102
        Private Const OBSPNT_COLOR_DEF_SYMBOL_FIXED_B As Long = 255
        Private Const OBSPNT_COLOR_DEF_SYMBOL_EMPHASIS_R As Long = 204 '強調観測点記号色。
        Private Const OBSPNT_COLOR_DEF_SYMBOL_EMPHASIS_G As Long = 255
        Private Const OBSPNT_COLOR_DEF_SYMBOL_EMPHASIS_B As Long = 102
        Private Const OBSPNT_COLOR_DEF_SYMBOL_EMPHASIS_DISABLE_R As Long = 153 '強調無効観測点記号色。
        Private Const OBSPNT_COLOR_DEF_SYMBOL_EMPHASIS_DISABLE_G As Long = 153
        Private Const OBSPNT_COLOR_DEF_SYMBOL_EMPHASIS_DISABLE_B As Long = 153
        Private Const OBSPNT_COLOR_DEF_SYMBOL_EMPHASIS_FIXED_R As Long = 255 '強調固定観測点記号色。
        Private Const OBSPNT_COLOR_DEF_SYMBOL_EMPHASIS_FIXED_G As Long = 153
        Private Const OBSPNT_COLOR_DEF_SYMBOL_EMPHASIS_FIXED_B As Long = 255
        Private Const OBSPNT_COLOR_DEF_SYMBOL_ANGLE_R As Long = 255 '登録閉合観測点記号色。
        Private Const OBSPNT_COLOR_DEF_SYMBOL_ANGLE_G As Long = 0
        Private Const OBSPNT_COLOR_DEF_SYMBOL_ANGLE_B As Long = 0
        Private Const OBSPNT_COLOR_DEF_SYMBOL_ANGLE_FIXED_R As Long = 255 '登録閉合固定観測点記号色。
        Private Const OBSPNT_COLOR_DEF_SYMBOL_ANGLE_FIXED_G As Long = 0
        Private Const OBSPNT_COLOR_DEF_SYMBOL_ANGLE_FIXED_B As Long = 0
        Private Const OBSPNT_COLOR_DEF_SYMBOL_CANDIDATE_R As Long = 153 '閉合候補観測点記号色。
        Private Const OBSPNT_COLOR_DEF_SYMBOL_CANDIDATE_G As Long = 204
        Private Const OBSPNT_COLOR_DEF_SYMBOL_CANDIDATE_B As Long = 255
        Private Const OBSPNT_COLOR_DEF_SYMBOL_CANDIDATE_FIXED_R As Long = 102 '閉合候補固定観測点記号色。
        Private Const OBSPNT_COLOR_DEF_SYMBOL_CANDIDATE_FIXED_G As Long = 204
        Private Const OBSPNT_COLOR_DEF_SYMBOL_CANDIDATE_FIXED_B As Long = 255

        Private Const PROFILE_VECTOR_WIDTH_SEC As String = "VECTOR_WIDTH" 'ベクトルの幅。
        Private Const VECTOR_WIDTH_KEY_NORMAL As String = "NORMAL"           '通常
        Private Const VECTOR_WIDTH_DEF_NORMAL As Long = 1
        Private Const VECTOR_WIDTH_KEY_EMPHASIS As String = "EMPHASIS"       '強調
        Private Const VECTOR_WIDTH_DEF_EMPHASIS As Long = 4
        Private Const VECTOR_WIDTH_KEY_DUPLICATION As String = "DUPLICATION" '重複
        Private Const VECTOR_WIDTH_DEF_DUPLICATION As Long = 3

        Private Const PROFILE_VECTOR_COLOR_SEC As String = "VECTOR_COLOR" 'ベクトルの色。
        Private Const VECTOR_COLOR_KEY_ENABLE_R As String = "ENABLE_R" '未解析基線ベクトル色。
        Private Const VECTOR_COLOR_KEY_ENABLE_G As String = "ENABLE_G"
        Private Const VECTOR_COLOR_KEY_ENABLE_B As String = "ENABLE_B"
        Private Const VECTOR_COLOR_KEY_DISABLE_R As String = "DISABLE_R" '無効基線ベクトル色。
        Private Const VECTOR_COLOR_KEY_DISABLE_G As String = "DISABLE_G"
        Private Const VECTOR_COLOR_KEY_DISABLE_B As String = "DISABLE_B"
        Private Const VECTOR_COLOR_KEY_FLOAT_R As String = "FLOAT_R" 'FLOAT基線ベクトル色。
        Private Const VECTOR_COLOR_KEY_FLOAT_G As String = "FLOAT_G"
        Private Const VECTOR_COLOR_KEY_FLOAT_B As String = "FLOAT_B"
        Private Const VECTOR_COLOR_KEY_FIX_R As String = "FIX_R" 'FIX基線ベクトル色。
        Private Const VECTOR_COLOR_KEY_FIX_G As String = "FIX_G"
        Private Const VECTOR_COLOR_KEY_FIX_B As String = "FIX_B"
        Private Const VECTOR_COLOR_KEY_FIX_LOWRATIO_R As String = "FIX_LOWRATIO_R" 'FIX(バイアス決定比<最小バイアス決定比)基線ベクトル色。2006/10/12 NGS Yamada
        Private Const VECTOR_COLOR_KEY_FIX_LOWRATIO_G As String = "FIX_LOWRATIO_G"
        Private Const VECTOR_COLOR_KEY_FIX_LOWRATIO_B As String = "FIX_LOWRATIO_B"
        Private Const VECTOR_COLOR_KEY_FAILED_R As String = "FAILED_R" '解析失敗基線ベクトル色。
        Private Const VECTOR_COLOR_KEY_FAILED_G As String = "FAILED_G"
        Private Const VECTOR_COLOR_KEY_FAILED_B As String = "FAILED_B"
        Private Const VECTOR_COLOR_KEY_ANGLE_R As String = "ANGLE_R" '登録閉合基線ベクトル色。
        Private Const VECTOR_COLOR_KEY_ANGLE_G As String = "ANGLE_G"
        Private Const VECTOR_COLOR_KEY_ANGLE_B As String = "ANGLE_B"
        Private Const VECTOR_COLOR_KEY_CANDIDATE_R As String = "CANDIDATE_R" '閉合候補基線ベクトル色。
        Private Const VECTOR_COLOR_KEY_CANDIDATE_G As String = "CANDIDATE_G"
        Private Const VECTOR_COLOR_KEY_CANDIDATE_B As String = "CANDIDATE_B"
        Private Const VECTOR_COLOR_KEY_CORRECT_R As String = "CORRECT_R" '偏心補正基線ベクトル色。
        Private Const VECTOR_COLOR_KEY_CORRECT_G As String = "CORRECT_G"
        Private Const VECTOR_COLOR_KEY_CORRECT_B As String = "CORRECT_B"
        Private Const VECTOR_COLOR_KEY_ECCENTRIC_R As String = "ECCENTRIC_R" '偏心ベクトル色。
        Private Const VECTOR_COLOR_KEY_ECCENTRIC_G As String = "ECCENTRIC_G"
        Private Const VECTOR_COLOR_KEY_ECCENTRIC_B As String = "ECCENTRIC_B"
        Private Const VECTOR_COLOR_KEY_EMPHASIS_R As String = "EMPHASIS_R" '強調未解析基線ベクトル色。
        Private Const VECTOR_COLOR_KEY_EMPHASIS_G As String = "EMPHASIS_G"
        Private Const VECTOR_COLOR_KEY_EMPHASIS_B As String = "EMPHASIS_B"
        Private Const VECTOR_COLOR_KEY_EMPHASIS_DISABLE_R As String = "EMPHASIS_DISABLE_R" '強調無効基線ベクトル色。
        Private Const VECTOR_COLOR_KEY_EMPHASIS_DISABLE_G As String = "EMPHASIS_DISABLE_G"
        Private Const VECTOR_COLOR_KEY_EMPHASIS_DISABLE_B As String = "EMPHASIS_DISABLE_B"
        Private Const VECTOR_COLOR_KEY_EMPHASIS_FLOAT_R As String = "EMPHASIS_FLOAT_R" '強調FLOAT基線ベクトル色。
        Private Const VECTOR_COLOR_KEY_EMPHASIS_FLOAT_G As String = "EMPHASIS_FLOAT_G"
        Private Const VECTOR_COLOR_KEY_EMPHASIS_FLOAT_B As String = "EMPHASIS_FLOAT_B"
        Private Const VECTOR_COLOR_KEY_EMPHASIS_FIX_R As String = "EMPHASIS_FIX_R" '強調FIX基線ベクトル色。
        Private Const VECTOR_COLOR_KEY_EMPHASIS_FIX_G As String = "EMPHASIS_FIX_G"
        Private Const VECTOR_COLOR_KEY_EMPHASIS_FIX_B As String = "EMPHASIS_FIX_B"
        Private Const VECTOR_COLOR_KEY_EMPHASIS_FIX_LOWRATIO_R As String = "EMPHASIS_FIX_LOWRATIO_R" '強調FIX(バイアス決定比<最小バイアス決定比)基線ベクトル色。2006/10/12 NGS Yamada
        Private Const VECTOR_COLOR_KEY_EMPHASIS_FIX_LOWRATIO_G As String = "EMPHASIS_FIX_LOWRATIO_G"
        Private Const VECTOR_COLOR_KEY_EMPHASIS_FIX_LOWRATIO_B As String = "EMPHASIS_FIX_LOWRATIO_B"
        Private Const VECTOR_COLOR_KEY_EMPHASIS_FAILED_R As String = "EMPHASIS_FAILED_R" '強調解析失敗基線ベクトル色。
        Private Const VECTOR_COLOR_KEY_EMPHASIS_FAILED_G As String = "EMPHASIS_FAILED_G"
        Private Const VECTOR_COLOR_KEY_EMPHASIS_FAILED_B As String = "EMPHASIS_FAILED_B"
        Private Const VECTOR_COLOR_KEY_EMPHASIS_CANDIDATE_R As String = "EMPHASIS_CANDIDATE_R" '強調閉合候補基線ベクトル色。
        Private Const VECTOR_COLOR_KEY_EMPHASIS_CANDIDATE_G As String = "EMPHASIS_CANDIDATE_G"
        Private Const VECTOR_COLOR_KEY_EMPHASIS_CANDIDATE_B As String = "EMPHASIS_CANDIDATE_B"

        Private Const VECTOR_COLOR_DEF_ENABLE_R As Long = 255 '未解析基線ベクトル色。
        Private Const VECTOR_COLOR_DEF_ENABLE_G As Long = 255
        Private Const VECTOR_COLOR_DEF_ENABLE_B As Long = 255
        Private Const VECTOR_COLOR_DEF_DISABLE_R As Long = 102 '無効基線ベクトル色。
        Private Const VECTOR_COLOR_DEF_DISABLE_G As Long = 102
        Private Const VECTOR_COLOR_DEF_DISABLE_B As Long = 102
        Private Const VECTOR_COLOR_DEF_FLOAT_R As Long = 255 'FLOAT基線ベクトル色。
        Private Const VECTOR_COLOR_DEF_FLOAT_G As Long = 255
        Private Const VECTOR_COLOR_DEF_FLOAT_B As Long = 0
        Private Const VECTOR_COLOR_DEF_FIX_R As Long = 0 'FIX基線ベクトル色。
        Private Const VECTOR_COLOR_DEF_FIX_G As Long = 255
        Private Const VECTOR_COLOR_DEF_FIX_B As Long = 0
        Private Const VECTOR_COLOR_DEF_FIX_LOWRATIO_R As Long = 255 'FIX(バイアス決定比<最小バイアス決定比)基線ベクトル色。2006/10/12 NGS Yamada
        Private Const VECTOR_COLOR_DEF_FIX_LOWRATIO_G As Long = 128
        Private Const VECTOR_COLOR_DEF_FIX_LOWRATIO_B As Long = 128
        Private Const VECTOR_COLOR_DEF_FAILED_R As Long = 255 '解析失敗基線ベクトル色。
        Private Const VECTOR_COLOR_DEF_FAILED_G As Long = 0
        Private Const VECTOR_COLOR_DEF_FAILED_B As Long = 0
        Private Const VECTOR_COLOR_DEF_ANGLE_R As Long = 255 '登録閉合基線ベクトル色。
        Private Const VECTOR_COLOR_DEF_ANGLE_G As Long = 204
        Private Const VECTOR_COLOR_DEF_ANGLE_B As Long = 102
        Private Const VECTOR_COLOR_DEF_CANDIDATE_R As Long = 0 '閉合候補基線ベクトル色。
        Private Const VECTOR_COLOR_DEF_CANDIDATE_G As Long = 153
        Private Const VECTOR_COLOR_DEF_CANDIDATE_B As Long = 255
        Private Const VECTOR_COLOR_DEF_CORRECT_R As Long = 204 '偏心補正基線ベクトル色。
        Private Const VECTOR_COLOR_DEF_CORRECT_G As Long = 255
        Private Const VECTOR_COLOR_DEF_CORRECT_B As Long = 255
        Private Const VECTOR_COLOR_DEF_ECCENTRIC_R As Long = 255 '偏心ベクトル色。
        Private Const VECTOR_COLOR_DEF_ECCENTRIC_G As Long = 255
        Private Const VECTOR_COLOR_DEF_ECCENTRIC_B As Long = 204
        Private Const VECTOR_COLOR_DEF_EMPHASIS_R As Long = 255 '強調未解析基線ベクトル色。
        Private Const VECTOR_COLOR_DEF_EMPHASIS_G As Long = 255
        Private Const VECTOR_COLOR_DEF_EMPHASIS_B As Long = 255
        Private Const VECTOR_COLOR_DEF_EMPHASIS_DISABLE_R As Long = 153 '強調無効基線ベクトル色。
        Private Const VECTOR_COLOR_DEF_EMPHASIS_DISABLE_G As Long = 153
        Private Const VECTOR_COLOR_DEF_EMPHASIS_DISABLE_B As Long = 153
        Private Const VECTOR_COLOR_DEF_EMPHASIS_FLOAT_R As Long = 255 '強調FLOAT基線ベクトル色。
        Private Const VECTOR_COLOR_DEF_EMPHASIS_FLOAT_G As Long = 255
        Private Const VECTOR_COLOR_DEF_EMPHASIS_FLOAT_B As Long = 102
        Private Const VECTOR_COLOR_DEF_EMPHASIS_FIX_R As Long = 102 '強調FIX基線ベクトル色。
        Private Const VECTOR_COLOR_DEF_EMPHASIS_FIX_G As Long = 255
        Private Const VECTOR_COLOR_DEF_EMPHASIS_FIX_B As Long = 102
        Private Const VECTOR_COLOR_DEF_EMPHASIS_FIX_LOWRATIO_R As Long = 255 '強調FIX(バイアス決定比<最小バイアス決定比)基線ベクトル色。2006/10/12 NGS Yamada
        Private Const VECTOR_COLOR_DEF_EMPHASIS_FIX_LOWRATIO_G As Long = 128
        Private Const VECTOR_COLOR_DEF_EMPHASIS_FIX_LOWRATIO_B As Long = 128
        Private Const VECTOR_COLOR_DEF_EMPHASIS_FAILED_R As Long = 255 '強調解析失敗基線ベクトル色。
        Private Const VECTOR_COLOR_DEF_EMPHASIS_FAILED_G As Long = 102
        Private Const VECTOR_COLOR_DEF_EMPHASIS_FAILED_B As Long = 102
        Private Const VECTOR_COLOR_DEF_EMPHASIS_CANDIDATE_R As Long = 204 '強調閉合候補基線ベクトル色。
        Private Const VECTOR_COLOR_DEF_EMPHASIS_CANDIDATE_G As Long = 255
        Private Const VECTOR_COLOR_DEF_EMPHASIS_CANDIDATE_B As Long = 255

        Private Const LISTTYPE_STR_OBSPNT As String = "観測点"
        Private Const LISTTYPE_STR_VECTOR As String = "ベクトル"
        Private Const LISTTYPE_OFFSET As Long = 100 'リスト種別オフセット。

        Private Const PLOT_GENUINE_RADIUS As Single = 50 '本点半径(Twips)。
        Private Const PLOT_GENUINE_RADIUS_FIX As Single = 65 '本点(固定点)半径(Twips)。
        Private Const PLOT_GENUINE_SYMBOL_SPACE As Long = 85 '本点の中心から記号までのＹ軸距離(Twips)。

        Private Const MULTI_WIDTH As Double = 20 '偏心補正ベクトルの２重線の幅。
        Private Const DUPLICATION_WIDTH As Double = 25 '重複ベクトルの２重線の幅。2006/10/11 NGS Yamada

        Private Const COS60 As Double = 0.5 'cos(60°)
        Private Const COS120 As Double = -0.5 'cos(120°)
        Private Const SIN120 As Double = 0.866025403784439 'sin(240°)
        Private Const COS240 As Double = -0.5 'cos(120°)
        Private Const SIN240 As Double = -0.866025403784439 'sin(240°)
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'定数
        private const string PROFILE_OBSPNT_COLOR_SEC = "OBSPNT_COLOR";                                 //'ベクトルの色。
        private const string OBSPNT_COLOR_KEY_ENABLE_R = "ENABLE_R";                                    //'有効観測点色。
        private const string OBSPNT_COLOR_KEY_ENABLE_G = "ENABLE_G";
        private const string OBSPNT_COLOR_KEY_ENABLE_B = "ENABLE_B";
        private const string OBSPNT_COLOR_KEY_DISABLE_R = "DISABLE_R";                                  //'無効観測点色。
        private const string OBSPNT_COLOR_KEY_DISABLE_G = "DISABLE_G";
        private const string OBSPNT_COLOR_KEY_DISABLE_B = "DISABLE_B";
        private const string OBSPNT_COLOR_KEY_FIXED_R = "FIXED_R";                                      //'固定観測点色。
        private const string OBSPNT_COLOR_KEY_FIXED_G = "FIXED_G";
        private const string OBSPNT_COLOR_KEY_FIXED_B = "FIXED_B";
        private const string OBSPNT_COLOR_KEY_EMPHASIS_R = "EMPHASIS_R";                                //'強調観測点色。
        private const string OBSPNT_COLOR_KEY_EMPHASIS_G = "EMPHASIS_G";
        private const string OBSPNT_COLOR_KEY_EMPHASIS_B = "EMPHASIS_B";
        private const string OBSPNT_COLOR_KEY_EMPHASIS_DISABLE_R = "EMPHASIS_DISABLE_R";                //'強調無効観測点色。
        private const string OBSPNT_COLOR_KEY_EMPHASIS_DISABLE_G = "EMPHASIS_DISABLE_G";
        private const string OBSPNT_COLOR_KEY_EMPHASIS_DISABLE_B = "EMPHASIS_DISABLE_B";
        private const string OBSPNT_COLOR_KEY_EMPHASIS_FIXED_R = "EMPHASIS_FIXED_R";                    //'強調固定観測点色。
        private const string OBSPNT_COLOR_KEY_EMPHASIS_FIXED_G = "EMPHASIS_FIXED_G";
        private const string OBSPNT_COLOR_KEY_EMPHASIS_FIXED_B = "EMPHASIS_FIXED_B";
        private const string OBSPNT_COLOR_KEY_ANGLE_R = "ANGLE_R";                                      //'登録閉合観測点色。
        private const string OBSPNT_COLOR_KEY_ANGLE_G = "ANGLE_G";
        private const string OBSPNT_COLOR_KEY_ANGLE_B = "ANGLE_B";
        private const string OBSPNT_COLOR_KEY_ANGLE_FIXED_R = "ANGLE_FIXED_R";                          //'登録閉合固定観測点色。
        private const string OBSPNT_COLOR_KEY_ANGLE_FIXED_G = "ANGLE_FIXED_G";
        private const string OBSPNT_COLOR_KEY_ANGLE_FIXED_B = "ANGLE_FIXED_B";
        private const string OBSPNT_COLOR_KEY_CANDIDATE_R = "CANDIDATE_R";                              //'閉合候補観測点色。
        private const string OBSPNT_COLOR_KEY_CANDIDATE_G = "CANDIDATE_G";
        private const string OBSPNT_COLOR_KEY_CANDIDATE_B = "CANDIDATE_B";
        private const string OBSPNT_COLOR_KEY_CANDIDATE_FIXED_R = "CANDIDATE_FIXED_R";                  //'閉合候補固定観測点色。
        private const string OBSPNT_COLOR_KEY_CANDIDATE_FIXED_G = "CANDIDATE_FIXED_G";
        private const string OBSPNT_COLOR_KEY_CANDIDATE_FIXED_B = "CANDIDATE_FIXED_B";
        private const string OBSPNT_COLOR_KEY_SYMBOL_ENABLE_R = "SYMBOL_ENABLE_R";                      //'有効観測点記号色。
        private const string OBSPNT_COLOR_KEY_SYMBOL_ENABLE_G = "SYMBOL_ENABLE_G";
        private const string OBSPNT_COLOR_KEY_SYMBOL_ENABLE_B = "SYMBOL_ENABLE_B";
        private const string OBSPNT_COLOR_KEY_SYMBOL_DISABLE_R = "SYMBOL_DISABLE_R";                    //'無効観測点記号色。
        private const string OBSPNT_COLOR_KEY_SYMBOL_DISABLE_G = "SYMBOL_DISABLE_G";
        private const string OBSPNT_COLOR_KEY_SYMBOL_DISABLE_B = "SYMBOL_DISABLE_B";
        private const string OBSPNT_COLOR_KEY_SYMBOL_FIXED_R = "SYMBOL_FIXED_R";                        //'固定観測点記号色。
        private const string OBSPNT_COLOR_KEY_SYMBOL_FIXED_G = "SYMBOL_FIXED_G";
        private const string OBSPNT_COLOR_KEY_SYMBOL_FIXED_B = "SYMBOL_FIXED_B";
        private const string OBSPNT_COLOR_KEY_SYMBOL_EMPHASIS_R = "SYMBOL_EMPHASIS_R";                  //'強調観測点記号色。
        private const string OBSPNT_COLOR_KEY_SYMBOL_EMPHASIS_G = "SYMBOL_EMPHASIS_G";
        private const string OBSPNT_COLOR_KEY_SYMBOL_EMPHASIS_B = "SYMBOL_EMPHASIS_B";
        private const string OBSPNT_COLOR_KEY_SYMBOL_EMPHASIS_DISABLE_R = "SYMBOL_EMPHASIS_DISABLE_R";  //'強調無効観測点記号色。
        private const string OBSPNT_COLOR_KEY_SYMBOL_EMPHASIS_DISABLE_G = "SYMBOL_EMPHASIS_DISABLE_G";
        private const string OBSPNT_COLOR_KEY_SYMBOL_EMPHASIS_DISABLE_B = "SYMBOL_EMPHASIS_DISABLE_B";
        private const string OBSPNT_COLOR_KEY_SYMBOL_EMPHASIS_FIXED_R = "SYMBOL_EMPHASIS_FIXED_R";      //'強調固定観測点記号色。
        private const string OBSPNT_COLOR_KEY_SYMBOL_EMPHASIS_FIXED_G = "SYMBOL_EMPHASIS_FIXED_G";
        private const string OBSPNT_COLOR_KEY_SYMBOL_EMPHASIS_FIXED_B = "SYMBOL_EMPHASIS_FIXED_B";
        private const string OBSPNT_COLOR_KEY_SYMBOL_ANGLE_R = "SYMBOL_ANGLE_R";                        //'登録閉合観測点記号色。
        private const string OBSPNT_COLOR_KEY_SYMBOL_ANGLE_G = "SYMBOL_ANGLE_G";
        private const string OBSPNT_COLOR_KEY_SYMBOL_ANGLE_B = "SYMBOL_ANGLE_B";
        private const string OBSPNT_COLOR_KEY_SYMBOL_ANGLE_FIXED_R = "SYMBOL_ANGLE_FIXED_R";            //'登録閉合固定観測点記号色。
        private const string OBSPNT_COLOR_KEY_SYMBOL_ANGLE_FIXED_G = "SYMBOL_ANGLE_FIXED_G";
        private const string OBSPNT_COLOR_KEY_SYMBOL_ANGLE_FIXED_B = "SYMBOL_ANGLE_FIXED_B";
        private const string OBSPNT_COLOR_KEY_SYMBOL_CANDIDATE_R = "SYMBOL_CANDIDATE_R";                //'閉合候補観測点記号色。
        private const string OBSPNT_COLOR_KEY_SYMBOL_CANDIDATE_G = "SYMBOL_CANDIDATE_G";
        private const string OBSPNT_COLOR_KEY_SYMBOL_CANDIDATE_B = "SYMBOL_CANDIDATE_B";
        private const string OBSPNT_COLOR_KEY_SYMBOL_CANDIDATE_FIXED_R = "SYMBOL_CANDIDATE_FIXED_R";    //'閉合候補固定観測点記号色。
        private const string OBSPNT_COLOR_KEY_SYMBOL_CANDIDATE_FIXED_G = "SYMBOL_CANDIDATE_FIXED_G";
        private const string OBSPNT_COLOR_KEY_SYMBOL_CANDIDATE_FIXED_B = "SYMBOL_CANDIDATE_FIXED_B";

        private const long OBSPNT_COLOR_DEF_ENABLE_R = 255;                     //'有効観測点色。
        private const long OBSPNT_COLOR_DEF_ENABLE_G = 255;
        private const long OBSPNT_COLOR_DEF_ENABLE_B = 0;
        private const long OBSPNT_COLOR_DEF_DISABLE_R = 102;                    //'無効観測点色。
        private const long OBSPNT_COLOR_DEF_DISABLE_G = 102;
        private const long OBSPNT_COLOR_DEF_DISABLE_B = 102;
        private const long OBSPNT_COLOR_DEF_FIXED_R = 153;                      //'固定観測点色。
        private const long OBSPNT_COLOR_DEF_FIXED_G = 153;
        private const long OBSPNT_COLOR_DEF_FIXED_B = 255;
        private const long OBSPNT_COLOR_DEF_EMPHASIS_R = 255;                   //'強調観測点色。
        private const long OBSPNT_COLOR_DEF_EMPHASIS_G = 255;
        private const long OBSPNT_COLOR_DEF_EMPHASIS_B = 102;
        private const long OBSPNT_COLOR_DEF_EMPHASIS_DISABLE_R = 153;           //'強調無効観測点色。
        private const long OBSPNT_COLOR_DEF_EMPHASIS_DISABLE_G = 153;
        private const long OBSPNT_COLOR_DEF_EMPHASIS_DISABLE_B = 153;
        private const long OBSPNT_COLOR_DEF_EMPHASIS_FIXED_R = 204;             //'強調固定観測点色。
        private const long OBSPNT_COLOR_DEF_EMPHASIS_FIXED_G = 153;
        private const long OBSPNT_COLOR_DEF_EMPHASIS_FIXED_B = 255;
        private const long OBSPNT_COLOR_DEF_ANGLE_R = 255;                      //'登録閉合観測点色。
        private const long OBSPNT_COLOR_DEF_ANGLE_G = 153;
        private const long OBSPNT_COLOR_DEF_ANGLE_B = 0;
        private const long OBSPNT_COLOR_DEF_ANGLE_FIXED_R = 255;                //'登録閉合固定観測点色。
        private const long OBSPNT_COLOR_DEF_ANGLE_FIXED_G = 0;
        private const long OBSPNT_COLOR_DEF_ANGLE_FIXED_B = 0;
        private const long OBSPNT_COLOR_DEF_CANDIDATE_R = 51;                   //'閉合候補観測点色。
        private const long OBSPNT_COLOR_DEF_CANDIDATE_G = 204;
        private const long OBSPNT_COLOR_DEF_CANDIDATE_B = 255;
        private const long OBSPNT_COLOR_DEF_CANDIDATE_FIXED_R = 0;              //'閉合候補固定観測点色。
        private const long OBSPNT_COLOR_DEF_CANDIDATE_FIXED_G = 0;
        private const long OBSPNT_COLOR_DEF_CANDIDATE_FIXED_B = 255;
        private const long OBSPNT_COLOR_DEF_SYMBOL_ENABLE_R = 102;              //'有効観測点記号色。
        private const long OBSPNT_COLOR_DEF_SYMBOL_ENABLE_G = 255;
        private const long OBSPNT_COLOR_DEF_SYMBOL_ENABLE_B = 51;
        private const long OBSPNT_COLOR_DEF_SYMBOL_DISABLE_R = 102;             //'無効観測点記号色。
        private const long OBSPNT_COLOR_DEF_SYMBOL_DISABLE_G = 102;
        private const long OBSPNT_COLOR_DEF_SYMBOL_DISABLE_B = 102;
        private const long OBSPNT_COLOR_DEF_SYMBOL_FIXED_R = 255;               //'固定観測点記号色。
        private const long OBSPNT_COLOR_DEF_SYMBOL_FIXED_G = 102;
        private const long OBSPNT_COLOR_DEF_SYMBOL_FIXED_B = 255;
        private const long OBSPNT_COLOR_DEF_SYMBOL_EMPHASIS_R = 204;            //'強調観測点記号色。
        private const long OBSPNT_COLOR_DEF_SYMBOL_EMPHASIS_G = 255;
        private const long OBSPNT_COLOR_DEF_SYMBOL_EMPHASIS_B = 102;
        private const long OBSPNT_COLOR_DEF_SYMBOL_EMPHASIS_DISABLE_R = 153;    //'強調無効観測点記号色。
        private const long OBSPNT_COLOR_DEF_SYMBOL_EMPHASIS_DISABLE_G = 153;
        private const long OBSPNT_COLOR_DEF_SYMBOL_EMPHASIS_DISABLE_B = 153;
        private const long OBSPNT_COLOR_DEF_SYMBOL_EMPHASIS_FIXED_R = 255;      //'強調固定観測点記号色。
        private const long OBSPNT_COLOR_DEF_SYMBOL_EMPHASIS_FIXED_G = 153;
        private const long OBSPNT_COLOR_DEF_SYMBOL_EMPHASIS_FIXED_B = 255;
        private const long OBSPNT_COLOR_DEF_SYMBOL_ANGLE_R = 255;               //'登録閉合観測点記号色。
        private const long OBSPNT_COLOR_DEF_SYMBOL_ANGLE_G = 0;
        private const long OBSPNT_COLOR_DEF_SYMBOL_ANGLE_B = 0;
        private const long OBSPNT_COLOR_DEF_SYMBOL_ANGLE_FIXED_R = 255;         //'登録閉合固定観測点記号色。
        private const long OBSPNT_COLOR_DEF_SYMBOL_ANGLE_FIXED_G = 0;
        private const long OBSPNT_COLOR_DEF_SYMBOL_ANGLE_FIXED_B = 0;
        private const long OBSPNT_COLOR_DEF_SYMBOL_CANDIDATE_R = 153;           //'閉合候補観測点記号色。
        private const long OBSPNT_COLOR_DEF_SYMBOL_CANDIDATE_G = 204;
        private const long OBSPNT_COLOR_DEF_SYMBOL_CANDIDATE_B = 255;
        private const long OBSPNT_COLOR_DEF_SYMBOL_CANDIDATE_FIXED_R = 102;     //'閉合候補固定観測点記号色。
        private const long OBSPNT_COLOR_DEF_SYMBOL_CANDIDATE_FIXED_G = 204;
        private const long OBSPNT_COLOR_DEF_SYMBOL_CANDIDATE_FIXED_B = 255;

        private const string PROFILE_VECTOR_WIDTH_SEC = "VECTOR_WIDTH";         //'ベクトルの幅。
        private const string VECTOR_WIDTH_KEY_NORMAL = "NORMAL";                //'通常
        private const long VECTOR_WIDTH_DEF_NORMAL = 1;
        private const string VECTOR_WIDTH_KEY_EMPHASIS = "EMPHASIS";            //'強調
        private const long VECTOR_WIDTH_DEF_EMPHASIS = 4;
        private const string VECTOR_WIDTH_KEY_DUPLICATION = "DUPLICATION";      //'重複
        private const long VECTOR_WIDTH_DEF_DUPLICATION = 3;

        private const string PROFILE_VECTOR_COLOR_SEC = "VECTOR_COLOR";                             //'ベクトルの色。
        private const string VECTOR_COLOR_KEY_ENABLE_R = "ENABLE_R";                                //'未解析基線ベクトル色。
        private const string VECTOR_COLOR_KEY_ENABLE_G = "ENABLE_G";
        private const string VECTOR_COLOR_KEY_ENABLE_B = "ENABLE_B";
        private const string VECTOR_COLOR_KEY_DISABLE_R = "DISABLE_R";                              //'無効基線ベクトル色。
        private const string VECTOR_COLOR_KEY_DISABLE_G = "DISABLE_G";
        private const string VECTOR_COLOR_KEY_DISABLE_B = "DISABLE_B";
        private const string VECTOR_COLOR_KEY_FLOAT_R = "FLOAT_R";                                  //'FLOAT基線ベクトル色。
        private const string VECTOR_COLOR_KEY_FLOAT_G = "FLOAT_G";
        private const string VECTOR_COLOR_KEY_FLOAT_B = "FLOAT_B";
        private const string VECTOR_COLOR_KEY_FIX_R = "FIX_R";                                      //'FIX基線ベクトル色。
        private const string VECTOR_COLOR_KEY_FIX_G = "FIX_G";
        private const string VECTOR_COLOR_KEY_FIX_B = "FIX_B";
        private const string VECTOR_COLOR_KEY_FIX_LOWRATIO_R = "FIX_LOWRATIO_R";                    //'FIX(バイアス決定比<最小バイアス決定比)基線ベクトル色。2006/10/12 NGS Yamada
        private const string VECTOR_COLOR_KEY_FIX_LOWRATIO_G = "FIX_LOWRATIO_G";
        private const string VECTOR_COLOR_KEY_FIX_LOWRATIO_B = "FIX_LOWRATIO_B";
        private const string VECTOR_COLOR_KEY_FAILED_R = "FAILED_R";                                //'解析失敗基線ベクトル色。
        private const string VECTOR_COLOR_KEY_FAILED_G = "FAILED_G";
        private const string VECTOR_COLOR_KEY_FAILED_B = "FAILED_B";
        private const string VECTOR_COLOR_KEY_ANGLE_R = "ANGLE_R";                                  //'登録閉合基線ベクトル色。
        private const string VECTOR_COLOR_KEY_ANGLE_G = "ANGLE_G";
        private const string VECTOR_COLOR_KEY_ANGLE_B = "ANGLE_B";
        private const string VECTOR_COLOR_KEY_CANDIDATE_R = "CANDIDATE_R";                          //'閉合候補基線ベクトル色。
        private const string VECTOR_COLOR_KEY_CANDIDATE_G = "CANDIDATE_G";
        private const string VECTOR_COLOR_KEY_CANDIDATE_B = "CANDIDATE_B";
        private const string VECTOR_COLOR_KEY_CORRECT_R = "CORRECT_R";                              //'偏心補正基線ベクトル色。
        private const string VECTOR_COLOR_KEY_CORRECT_G = "CORRECT_G";
        private const string VECTOR_COLOR_KEY_CORRECT_B = "CORRECT_B";
        private const string VECTOR_COLOR_KEY_ECCENTRIC_R = "ECCENTRIC_R";                          //'偏心ベクトル色。
        private const string VECTOR_COLOR_KEY_ECCENTRIC_G = "ECCENTRIC_G";
        private const string VECTOR_COLOR_KEY_ECCENTRIC_B = "ECCENTRIC_B";
        private const string VECTOR_COLOR_KEY_EMPHASIS_R = "EMPHASIS_R";                            //'強調未解析基線ベクトル色。
        private const string VECTOR_COLOR_KEY_EMPHASIS_G = "EMPHASIS_G";
        private const string VECTOR_COLOR_KEY_EMPHASIS_B = "EMPHASIS_B";
        private const string VECTOR_COLOR_KEY_EMPHASIS_DISABLE_R = "EMPHASIS_DISABLE_R";            //'強調無効基線ベクトル色。
        private const string VECTOR_COLOR_KEY_EMPHASIS_DISABLE_G = "EMPHASIS_DISABLE_G";
        private const string VECTOR_COLOR_KEY_EMPHASIS_DISABLE_B = "EMPHASIS_DISABLE_B";
        private const string VECTOR_COLOR_KEY_EMPHASIS_FLOAT_R = "EMPHASIS_FLOAT_R";                //'強調FLOAT基線ベクトル色。
        private const string VECTOR_COLOR_KEY_EMPHASIS_FLOAT_G = "EMPHASIS_FLOAT_G";
        private const string VECTOR_COLOR_KEY_EMPHASIS_FLOAT_B = "EMPHASIS_FLOAT_B";
        private const string VECTOR_COLOR_KEY_EMPHASIS_FIX_R = "EMPHASIS_FIX_R";                    //'強調FIX基線ベクトル色。
        private const string VECTOR_COLOR_KEY_EMPHASIS_FIX_G = "EMPHASIS_FIX_G";
        private const string VECTOR_COLOR_KEY_EMPHASIS_FIX_B = "EMPHASIS_FIX_B";
        private const string VECTOR_COLOR_KEY_EMPHASIS_FIX_LOWRATIO_R = "EMPHASIS_FIX_LOWRATIO_R";  //'強調FIX(バイアス決定比<最小バイアス決定比)基線ベクトル色。2006/10/12 NGS Yamada
        private const string VECTOR_COLOR_KEY_EMPHASIS_FIX_LOWRATIO_G = "EMPHASIS_FIX_LOWRATIO_G";
        private const string VECTOR_COLOR_KEY_EMPHASIS_FIX_LOWRATIO_B = "EMPHASIS_FIX_LOWRATIO_B";
        private const string VECTOR_COLOR_KEY_EMPHASIS_FAILED_R = "EMPHASIS_FAILED_R";              //'強調解析失敗基線ベクトル色。
        private const string VECTOR_COLOR_KEY_EMPHASIS_FAILED_G = "EMPHASIS_FAILED_G";
        private const string VECTOR_COLOR_KEY_EMPHASIS_FAILED_B = "EMPHASIS_FAILED_B";
        private const string VECTOR_COLOR_KEY_EMPHASIS_CANDIDATE_R = "EMPHASIS_CANDIDATE_R";        //'強調閉合候補基線ベクトル色。
        private const string VECTOR_COLOR_KEY_EMPHASIS_CANDIDATE_G = "EMPHASIS_CANDIDATE_G";
        private const string VECTOR_COLOR_KEY_EMPHASIS_CANDIDATE_B = "EMPHASIS_CANDIDATE_B";

        private const long VECTOR_COLOR_DEF_ENABLE_R = 255;                                         //'未解析基線ベクトル色。
        private const long VECTOR_COLOR_DEF_ENABLE_G = 255;
        private const long VECTOR_COLOR_DEF_ENABLE_B = 255;
        private const long VECTOR_COLOR_DEF_DISABLE_R = 102;                                        //'無効基線ベクトル色。
        private const long VECTOR_COLOR_DEF_DISABLE_G = 102;
        private const long VECTOR_COLOR_DEF_DISABLE_B = 102;
        private const long VECTOR_COLOR_DEF_FLOAT_R = 255;                                          //'FLOAT基線ベクトル色。
        private const long VECTOR_COLOR_DEF_FLOAT_G = 255;
        private const long VECTOR_COLOR_DEF_FLOAT_B = 0;
        private const long VECTOR_COLOR_DEF_FIX_R = 0;                                              //'FIX基線ベクトル色。
        private const long VECTOR_COLOR_DEF_FIX_G = 255;
        private const long VECTOR_COLOR_DEF_FIX_B = 0;
        private const long VECTOR_COLOR_DEF_FIX_LOWRATIO_R = 255;                                   //'FIX(バイアス決定比<最小バイアス決定比)基線ベクトル色。2006/10/12 NGS Yamada
        private const long VECTOR_COLOR_DEF_FIX_LOWRATIO_G = 128;
        private const long VECTOR_COLOR_DEF_FIX_LOWRATIO_B = 128;
        private const long VECTOR_COLOR_DEF_FAILED_R = 255;                                         //'解析失敗基線ベクトル色。
        private const long VECTOR_COLOR_DEF_FAILED_G = 0;
        private const long VECTOR_COLOR_DEF_FAILED_B = 0;
        private const long VECTOR_COLOR_DEF_ANGLE_R = 255;                                          //'登録閉合基線ベクトル色。
        private const long VECTOR_COLOR_DEF_ANGLE_G = 204;
        private const long VECTOR_COLOR_DEF_ANGLE_B = 102;
        private const long VECTOR_COLOR_DEF_CANDIDATE_R = 0;                                        //'閉合候補基線ベクトル色。
        private const long VECTOR_COLOR_DEF_CANDIDATE_G = 153;
        private const long VECTOR_COLOR_DEF_CANDIDATE_B = 255;
        private const long VECTOR_COLOR_DEF_CORRECT_R = 204;                                        //'偏心補正基線ベクトル色。
        private const long VECTOR_COLOR_DEF_CORRECT_G = 255;
        private const long VECTOR_COLOR_DEF_CORRECT_B = 255;
        private const long VECTOR_COLOR_DEF_ECCENTRIC_R = 255;                                      //'偏心ベクトル色。
        private const long VECTOR_COLOR_DEF_ECCENTRIC_G = 255;
        private const long VECTOR_COLOR_DEF_ECCENTRIC_B = 204;
        private const long VECTOR_COLOR_DEF_EMPHASIS_R = 255;                                       //'強調未解析基線ベクトル色。
        private const long VECTOR_COLOR_DEF_EMPHASIS_G = 255;
        private const long VECTOR_COLOR_DEF_EMPHASIS_B = 255;
        private const long VECTOR_COLOR_DEF_EMPHASIS_DISABLE_R = 153;                               //'強調無効基線ベクトル色。
        private const long VECTOR_COLOR_DEF_EMPHASIS_DISABLE_G = 153;
        private const long VECTOR_COLOR_DEF_EMPHASIS_DISABLE_B = 153;
        private const long VECTOR_COLOR_DEF_EMPHASIS_FLOAT_R = 255;                                 //'強調FLOAT基線ベクトル色。
        private const long VECTOR_COLOR_DEF_EMPHASIS_FLOAT_G = 255;
        private const long VECTOR_COLOR_DEF_EMPHASIS_FLOAT_B = 102;
        private const long VECTOR_COLOR_DEF_EMPHASIS_FIX_R = 102;                                   //'強調FIX基線ベクトル色。
        private const long VECTOR_COLOR_DEF_EMPHASIS_FIX_G = 255;
        private const long VECTOR_COLOR_DEF_EMPHASIS_FIX_B = 102;
        private const long VECTOR_COLOR_DEF_EMPHASIS_FIX_LOWRATIO_R = 255;                          //'強調FIX(バイアス決定比<最小バイアス決定比)基線ベクトル色。2006/10/12 NGS Yamada
        private const long VECTOR_COLOR_DEF_EMPHASIS_FIX_LOWRATIO_G = 128;
        private const long VECTOR_COLOR_DEF_EMPHASIS_FIX_LOWRATIO_B = 128;
        private const long VECTOR_COLOR_DEF_EMPHASIS_FAILED_R = 255;                                //'強調解析失敗基線ベクトル色。
        private const long VECTOR_COLOR_DEF_EMPHASIS_FAILED_G = 102;
        private const long VECTOR_COLOR_DEF_EMPHASIS_FAILED_B = 102;
        private const long VECTOR_COLOR_DEF_EMPHASIS_CANDIDATE_R = 204;                             //'強調閉合候補基線ベクトル色。
        private const long VECTOR_COLOR_DEF_EMPHASIS_CANDIDATE_G = 255;
        private const long VECTOR_COLOR_DEF_EMPHASIS_CANDIDATE_B = 255;

        private const string LISTTYPE_STR_OBSPNT = "観測点";
        private const string LISTTYPE_STR_VECTOR = "ベクトル";
        private const long LISTTYPE_OFFSET = 100;                                                   //'リスト種別オフセット。

        private const float PLOT_GENUINE_RADIUS = 50;                                               //'本点半径(Twips)。
        private const float PLOT_GENUINE_RADIUS_FIX = 65;                                           //'本点(固定点)半径(Twips)。
        private const long PLOT_GENUINE_SYMBOL_SPACE = 85;                                          //'本点の中心から記号までのＹ軸距離(Twips)。

        private const double MULTI_WIDTH = 20;                                                      //'偏心補正ベクトルの２重線の幅。
        private const double DUPLICATION_WIDTH = 25;                                                //'重複ベクトルの２重線の幅。2006/10/11 NGS Yamada

        private const double COS60 = 0.5;                                                           //'cos(60°)
        private const double COS120 = -0.5;                                                         //'cos(120°)
        private const double SIN120 = 0.866025403784439;                                            //'sin(240°)
        private const double COS240 = -0.5;                                                         //'cos(120°)
        private const double SIN240 = -0.866025403784439;                                           //'sin(240°)
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'インプリメンテーション
        Private m_nPlotType As PLOTTYPE_ENUM 'プロット種別。
        Private m_hFontListType As Long 'リスト種別フォント。
        Private m_hBrushNull As Long 'NULLブラシ。
        Private m_hPenObspntEnable As Long '有効観測点。
        Private m_hPenObspntDisable As Long '無効観測点。
        Private m_hPenObspntFixed As Long '固定観測点。
        Private m_hPenObspntEmphasis As Long '強調観測点。
        Private m_hPenObspntEmphasisDisable As Long '強調無効観測点。
        Private m_hPenObspntEmphasisFixed As Long '強調固定観測点。
        Private m_hPenObspntAngle As Long '登録閉合観測点。
        Private m_hPenObspntAngleFixed As Long '登録閉合固定観測点。
        Private m_hPenObspntCandidate As Long '閉合候補観測点。
        Private m_hPenObspntCandidateFixed As Long '閉合候補固定観測点。
        Private m_hPenVectorEnable As Long '未解析基線ベクトル。
        Private m_hPenVectorDisable As Long '無効基線ベクトル。
        Private m_hPenVectorFloat As Long 'FLOAT基線ベクトル。
        Private m_hPenVectorFix As Long 'FIX基線ベクトル。
        Private m_hPenVectorFix_LowRatio As Long 'FIX(バイアス決定比<最小バイアス決定比)基線ベクトル。　2006/10/12 NGS Yamada
        Private m_hPenVectorFailed As Long '解析失敗基線ベクトル。
        Private m_hPenVectorDuplication As Long '重複未解析基線ベクトル。
        Private m_hPenVectorDuplicationFloat As Long '重複FLOAT基線ベクトル。
        Private m_hPenVectorDuplicationFix As Long '重複FIX基線ベクトル。
        Private m_hPenVectorDuplicationFailed As Long '重複解析失敗析基線ベクトル。
        Private m_hPenVectorAngle As Long '登録閉合基線ベクトル。
        Private m_hPenVectorCandidate As Long '閉合候補基線ベクトル。
        Private m_hPenVectorCorrect As Long '偏心補正ベクトル。
        Private m_hPenVectorEccentric As Long '偏心ベクトル。
        Private m_hPenVectorEmphasis As Long '強調未解析基線ベクトル。
        Private m_hPenVectorEmphasisDisable As Long '強調無効基線ベクトル。
        Private m_hPenVectorEmphasisFloat As Long '強調FLOAT基線ベクトル。
        Private m_hPenVectorEmphasisFix As Long '強調FIX基線ベクトル。
        Private m_hPenVectorEmphasisFix_LowRatio As Long '強調FIX(バイアス決定比<最小バイアス決定比)基線ベクトル。　2006/10/12 NGS Yamada
        Private m_hPenVectorEmphasisFailed As Long '強調解析失敗基線ベクトル。
        Private m_hPenVectorEmphasisCandidate As Long '強調閉合候補基線ベクトル。
        Private m_hPenVectorEmphasisCorrect As Long '強調偏心補正ベクトル。
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'インプリメンテーション
        private PLOTTYPE_ENUM m_nPlotType;                      //'プロット種別。
        private long m_hFontListType;                           //'リスト種別フォント。
        private long m_hBrushNull;                              //'NULLブラシ。
        private long m_hPenObspntEnable;                        //'有効観測点。
        private long m_hPenObspntDisable;                       //'無効観測点。
        private long m_hPenObspntFixed;                         //'固定観測点。
        private long m_hPenObspntEmphasis;                      //'強調観測点。
        private long m_hPenObspntEmphasisDisable;               //'強調無効観測点。
        private long m_hPenObspntEmphasisFixed;                 //'強調固定観測点。
        private long m_hPenObspntAngle;                         //'登録閉合観測点。
        private long m_hPenObspntAngleFixed;                    //'登録閉合固定観測点。
        private long m_hPenObspntCandidate;                     //'閉合候補観測点。
        private long m_hPenObspntCandidateFixed;                //'閉合候補固定観測点。
        private long m_hPenVectorEnable;                        //'未解析基線ベクトル。
        private long m_hPenVectorDisable;                       //'無効基線ベクトル。
        private long m_hPenVectorFloat;                         //'FLOAT基線ベクトル。
        private long m_hPenVectorFix;                           //'FIX基線ベクトル。
        private long m_hPenVectorFix_LowRatio;                  //'FIX(バイアス決定比<最小バイアス決定比)基線ベクトル。　2006/10/12 NGS Yamada
        private long m_hPenVectorFailed;                        //'解析失敗基線ベクトル。
        private long m_hPenVectorDuplication;                   //'重複未解析基線ベクトル。
        private long m_hPenVectorDuplicationFloat;              //'重複FLOAT基線ベクトル。
        private long m_hPenVectorDuplicationFix;                //'重複FIX基線ベクトル。
        private long m_hPenVectorDuplicationFailed;             //'重複解析失敗析基線ベクトル。
        private long m_hPenVectorAngle;                         //'登録閉合基線ベクトル。
        private long m_hPenVectorCandidate;                     //'閉合候補基線ベクトル。
        private long m_hPenVectorCorrect;                       //'偏心補正ベクトル。
        private long m_hPenVectorEccentric;                     //'偏心ベクトル。
        private long m_hPenVectorEmphasis;                      //'強調未解析基線ベクトル。
        private long m_hPenVectorEmphasisDisable;               //'強調無効基線ベクトル。
        private long m_hPenVectorEmphasisFloat;                 //'強調FLOAT基線ベクトル。
        private long m_hPenVectorEmphasisFix;                   //'強調FIX基線ベクトル。
        private long m_hPenVectorEmphasisFix_LowRatio;          //'強調FIX(バイアス決定比<最小バイアス決定比)基線ベクトル。　2006/10/12 NGS Yamada
        private long m_hPenVectorEmphasisFailed;                //'強調解析失敗基線ベクトル。
        private long m_hPenVectorEmphasisCandidate;             //'強調閉合候補基線ベクトル。
        private long m_hPenVectorEmphasisCorrect;               //'強調偏心補正ベクトル。
        //==========================================================================================

        //==========================================================================================
        //[C#]
        public PlotNative(MdlMain clsMdlMain, MdlPlotProc clsMdlPlotProc)
        {
            m_clsMdlMain = clsMdlMain;
            m_clsMdlPlotProc = clsMdlPlotProc;
            return;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '*******************************************************************************
        'イベント

        '終了。
        Private Sub Class_Terminate()

            On Error GoTo ErrorHandler


            If m_hFontListType<> 0 Then Call DeleteObject(m_hFontListType)
            If m_hBrushNull<> 0 Then Call DeleteObject(m_hBrushNull)
            If m_hPenObspntEnable<> 0 Then Call DeleteObject(m_hPenObspntEnable)
            If m_hPenObspntDisable<> 0 Then Call DeleteObject(m_hPenObspntDisable)
            If m_hPenObspntFixed<> 0 Then Call DeleteObject(m_hPenObspntFixed)
            If m_hPenObspntEmphasis<> 0 Then Call DeleteObject(m_hPenObspntEmphasis)
            If m_hPenObspntEmphasisDisable<> 0 Then Call DeleteObject(m_hPenObspntEmphasisDisable)
            If m_hPenObspntEmphasisFixed<> 0 Then Call DeleteObject(m_hPenObspntEmphasisFixed)
            If m_hPenObspntAngle<> 0 Then Call DeleteObject(m_hPenObspntAngle)
            If m_hPenObspntAngleFixed<> 0 Then Call DeleteObject(m_hPenObspntAngleFixed)
            If m_hPenObspntCandidate<> 0 Then Call DeleteObject(m_hPenObspntCandidate)
            If m_hPenObspntCandidateFixed<> 0 Then Call DeleteObject(m_hPenObspntCandidateFixed)
            If m_hPenVectorEnable<> 0 Then Call DeleteObject(m_hPenVectorEnable)
            If m_hPenVectorDisable<> 0 Then Call DeleteObject(m_hPenVectorDisable)
            If m_hPenVectorFloat<> 0 Then Call DeleteObject(m_hPenVectorFloat)
            If m_hPenVectorFix<> 0 Then Call DeleteObject(m_hPenVectorFix)
            If m_hPenVectorFailed<> 0 Then Call DeleteObject(m_hPenVectorFailed)
            If m_hPenVectorDuplication<> 0 Then Call DeleteObject(m_hPenVectorDuplication)
            If m_hPenVectorDuplicationFloat<> 0 Then Call DeleteObject(m_hPenVectorDuplicationFloat)
            If m_hPenVectorDuplicationFix<> 0 Then Call DeleteObject(m_hPenVectorDuplicationFix)
            If m_hPenVectorDuplicationFailed<> 0 Then Call DeleteObject(m_hPenVectorDuplicationFailed)
            If m_hPenVectorAngle<> 0 Then Call DeleteObject(m_hPenVectorAngle)
            If m_hPenVectorCandidate<> 0 Then Call DeleteObject(m_hPenVectorCandidate)
            If m_hPenVectorCorrect<> 0 Then Call DeleteObject(m_hPenVectorCorrect)
            If m_hPenVectorEccentric<> 0 Then Call DeleteObject(m_hPenVectorEccentric)
            If m_hPenVectorEmphasis<> 0 Then Call DeleteObject(m_hPenVectorEmphasis)
            If m_hPenVectorEmphasisDisable<> 0 Then Call DeleteObject(m_hPenVectorEmphasisDisable)
            If m_hPenVectorEmphasisFloat<> 0 Then Call DeleteObject(m_hPenVectorEmphasisFloat)
            If m_hPenVectorEmphasisFix<> 0 Then Call DeleteObject(m_hPenVectorEmphasisFix)
            If m_hPenVectorEmphasisFailed<> 0 Then Call DeleteObject(m_hPenVectorEmphasisFailed)
            If m_hPenVectorEmphasisCandidate<> 0 Then Call DeleteObject(m_hPenVectorEmphasisCandidate)
            If m_hPenVectorEmphasisCorrect<> 0 Then Call DeleteObject(m_hPenVectorEmphasisCorrect)

            Exit Sub

        ErrorHandler:
            Call mdlMain.ErrorExit

        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '*******************************************************************************
        'イベント
        
        '終了。
        */
        public void Class_Terminate()
        {
            try
            {
                if (m_hFontListType != 0)                   { _ = DeleteObject((IntPtr)m_hFontListType); }
                if (m_hBrushNull != 0)                      { _ = DeleteObject((IntPtr)m_hBrushNull); }
                if (m_hPenObspntEnable != 0)                { _ = DeleteObject((IntPtr)m_hPenObspntEnable); }
                if (m_hPenObspntDisable != 0)               { _ = DeleteObject((IntPtr)m_hPenObspntDisable); }
                if (m_hPenObspntFixed != 0)                 { _ = DeleteObject((IntPtr)m_hPenObspntFixed); }
                if (m_hPenObspntEmphasis != 0)              { _ = DeleteObject((IntPtr)m_hPenObspntEmphasis); }
                if (m_hPenObspntEmphasisDisable != 0)       { _ = DeleteObject((IntPtr)m_hPenObspntEmphasisDisable); }
                if (m_hPenObspntEmphasisFixed != 0)         { _ = DeleteObject((IntPtr)m_hPenObspntEmphasisFixed); }
                if (m_hPenObspntAngle != 0)                 { _ = DeleteObject((IntPtr)m_hPenObspntAngle); }
                if (m_hPenObspntAngleFixed != 0)            { _ = DeleteObject((IntPtr)m_hPenObspntAngleFixed); }
                if (m_hPenObspntCandidate != 0)             { _ = DeleteObject((IntPtr)m_hPenObspntCandidate); }
                if (m_hPenObspntCandidateFixed != 0)        { _ = DeleteObject((IntPtr)m_hPenObspntCandidateFixed); }
                if (m_hPenVectorEnable != 0)                { _ = DeleteObject((IntPtr)m_hPenVectorEnable); }
                if (m_hPenVectorDisable != 0)               { _ = DeleteObject((IntPtr)m_hPenVectorDisable); }
                if (m_hPenVectorFloat != 0)                 { _ = DeleteObject((IntPtr)m_hPenVectorFloat); }
                if (m_hPenVectorFix != 0)                   { _ = DeleteObject((IntPtr)m_hPenVectorFix); }
                if (m_hPenVectorFailed != 0)                { _ = DeleteObject((IntPtr)m_hPenVectorFailed); }
                if (m_hPenVectorDuplication != 0)           { _ = DeleteObject((IntPtr)m_hPenVectorDuplication); }
                if (m_hPenVectorDuplicationFloat != 0)      { _ = DeleteObject((IntPtr)m_hPenVectorDuplicationFloat); }
                if (m_hPenVectorDuplicationFix != 0)        { _ = DeleteObject((IntPtr)m_hPenVectorDuplicationFix); }
                if (m_hPenVectorDuplicationFailed != 0)     { _ = DeleteObject((IntPtr)m_hPenVectorDuplicationFailed); }
                if (m_hPenVectorAngle != 0)                 { _ = DeleteObject((IntPtr)m_hPenVectorAngle); }
                if (m_hPenVectorCandidate != 0)             { _ = DeleteObject((IntPtr)m_hPenVectorCandidate); }
                if (m_hPenVectorCorrect != 0)               { _ = DeleteObject((IntPtr)m_hPenVectorCorrect); }
                if (m_hPenVectorEccentric != 0)             { _ = DeleteObject((IntPtr)m_hPenVectorEccentric); }
                if (m_hPenVectorEmphasis != 0)              { _ = DeleteObject((IntPtr)m_hPenVectorEmphasis); }
                if (m_hPenVectorEmphasisDisable != 0)       { _ = DeleteObject((IntPtr)m_hPenVectorEmphasisDisable); }
                if (m_hPenVectorEmphasisFloat != 0)         { _ = DeleteObject((IntPtr)m_hPenVectorEmphasisFloat); }
                if (m_hPenVectorEmphasisFix != 0)           { _ = DeleteObject((IntPtr)m_hPenVectorEmphasisFix); }
                if (m_hPenVectorEmphasisFailed != 0)        { _ = DeleteObject((IntPtr)m_hPenVectorEmphasisFailed); }
                if (m_hPenVectorEmphasisCandidate != 0)     { _ = DeleteObject((IntPtr)m_hPenVectorEmphasisCandidate); }
                if (m_hPenVectorEmphasisCorrect != 0)       { _ = DeleteObject((IntPtr)m_hPenVectorEmphasisCorrect); }
                return;
            }

            catch
            {
                m_clsMdlMain.ErrorExit();
                return;
            }
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '*******************************************************************************
        'メソッド

        '初期化。
        '
        '引き数：
        'nPlotType プロット種別。
        'hDC デバイスコンテキストのハンドル。nPlotType が PLOTTYPE_PRINT の時に必要。
        Public Sub Initialize(ByVal nPlotType As PLOTTYPE_ENUM, Optional ByVal hDC As Long)


            Dim lngColR As Long
            Dim lngColG As Long
            Dim lngColB As Long

            m_nPlotType = nPlotType

            If m_hBrushNull = 0 Then
                Dim tLogBrush As LOGBRUSH
                tLogBrush.lbStyle = BS_NULL
                tLogBrush.lbHatch = 0
                tLogBrush.lbColor = 0
                m_hBrushNull = CreateBrushIndirect(tLogBrush)
            End If
    
            '線の太さ
            Vector_Width_Normal = GetPrivateProfileInt(PROFILE_VECTOR_WIDTH_SEC, VECTOR_WIDTH_KEY_NORMAL, VECTOR_WIDTH_DEF_NORMAL, App.Path & "\" & PROFILE_DEF_NAME)
            Vector_Width_Emphasis = GetPrivateProfileInt(PROFILE_VECTOR_WIDTH_SEC, VECTOR_WIDTH_KEY_EMPHASIS, VECTOR_WIDTH_DEF_EMPHASIS, App.Path & "\" & PROFILE_DEF_NAME)
            Vector_Width_Duplication = GetPrivateProfileInt(PROFILE_VECTOR_WIDTH_SEC, VECTOR_WIDTH_KEY_DUPLICATION, VECTOR_WIDTH_DEF_DUPLICATION, App.Path & "\" & PROFILE_DEF_NAME)


            'シンボル色の設定
            '有効観測点色。
            lngColR = GetPrivateProfileInt(PROFILE_OBSPNT_COLOR_SEC, OBSPNT_COLOR_KEY_ENABLE_R, OBSPNT_COLOR_DEF_ENABLE_R, App.Path & "\" & PROFILE_DEF_NAME)
            lngColG = GetPrivateProfileInt(PROFILE_OBSPNT_COLOR_SEC, OBSPNT_COLOR_KEY_ENABLE_G, OBSPNT_COLOR_DEF_ENABLE_G, App.Path & "\" & PROFILE_DEF_NAME)
            lngColB = GetPrivateProfileInt(PROFILE_OBSPNT_COLOR_SEC, OBSPNT_COLOR_KEY_ENABLE_B, OBSPNT_COLOR_DEF_ENABLE_B, App.Path & "\" & PROFILE_DEF_NAME)
            If m_hPenObspntEnable = 0 Then m_hPenObspntEnable = CreatePen(PS_SOLID, Vector_Width_Normal, RGB(lngColR, lngColG, lngColB))


            '無効観測点色。
            lngColR = GetPrivateProfileInt(PROFILE_OBSPNT_COLOR_SEC, OBSPNT_COLOR_KEY_DISABLE_R, OBSPNT_COLOR_DEF_DISABLE_R, App.Path & "\" & PROFILE_DEF_NAME)
            lngColG = GetPrivateProfileInt(PROFILE_OBSPNT_COLOR_SEC, OBSPNT_COLOR_KEY_DISABLE_G, OBSPNT_COLOR_DEF_DISABLE_G, App.Path & "\" & PROFILE_DEF_NAME)
            lngColB = GetPrivateProfileInt(PROFILE_OBSPNT_COLOR_SEC, OBSPNT_COLOR_KEY_DISABLE_B, OBSPNT_COLOR_DEF_DISABLE_B, App.Path & "\" & PROFILE_DEF_NAME)
            If m_hPenObspntDisable = 0 Then m_hPenObspntDisable = CreatePen(PS_SOLID, Vector_Width_Normal, RGB(lngColR, lngColG, lngColB))


            '固定観測点色。
            lngColR = GetPrivateProfileInt(PROFILE_OBSPNT_COLOR_SEC, OBSPNT_COLOR_KEY_FIXED_R, OBSPNT_COLOR_DEF_FIXED_R, App.Path & "\" & PROFILE_DEF_NAME)
            lngColG = GetPrivateProfileInt(PROFILE_OBSPNT_COLOR_SEC, OBSPNT_COLOR_KEY_FIXED_G, OBSPNT_COLOR_DEF_FIXED_G, App.Path & "\" & PROFILE_DEF_NAME)
            lngColB = GetPrivateProfileInt(PROFILE_OBSPNT_COLOR_SEC, OBSPNT_COLOR_KEY_FIXED_B, OBSPNT_COLOR_DEF_FIXED_B, App.Path & "\" & PROFILE_DEF_NAME)
            If m_hPenObspntFixed = 0 Then m_hPenObspntFixed = CreatePen(PS_SOLID, Vector_Width_Normal, RGB(lngColR, lngColG, lngColB))


            '強調観測点色。
            lngColR = GetPrivateProfileInt(PROFILE_OBSPNT_COLOR_SEC, OBSPNT_COLOR_KEY_EMPHASIS_R, OBSPNT_COLOR_DEF_EMPHASIS_R, App.Path & "\" & PROFILE_DEF_NAME)
            lngColG = GetPrivateProfileInt(PROFILE_OBSPNT_COLOR_SEC, OBSPNT_COLOR_KEY_EMPHASIS_G, OBSPNT_COLOR_DEF_EMPHASIS_G, App.Path & "\" & PROFILE_DEF_NAME)
            lngColB = GetPrivateProfileInt(PROFILE_OBSPNT_COLOR_SEC, OBSPNT_COLOR_KEY_EMPHASIS_B, OBSPNT_COLOR_DEF_EMPHASIS_B, App.Path & "\" & PROFILE_DEF_NAME)
            If m_hPenObspntEmphasis = 0 Then m_hPenObspntEmphasis = CreatePen(PS_SOLID, Vector_Width_Emphasis, RGB(lngColR, lngColG, lngColB))


            '強調無効観測点色。
            lngColR = GetPrivateProfileInt(PROFILE_OBSPNT_COLOR_SEC, OBSPNT_COLOR_KEY_EMPHASIS_DISABLE_R, OBSPNT_COLOR_DEF_EMPHASIS_DISABLE_R, App.Path & "\" & PROFILE_DEF_NAME)
            lngColG = GetPrivateProfileInt(PROFILE_OBSPNT_COLOR_SEC, OBSPNT_COLOR_KEY_EMPHASIS_DISABLE_G, OBSPNT_COLOR_DEF_EMPHASIS_DISABLE_G, App.Path & "\" & PROFILE_DEF_NAME)
            lngColB = GetPrivateProfileInt(PROFILE_OBSPNT_COLOR_SEC, OBSPNT_COLOR_KEY_EMPHASIS_DISABLE_B, OBSPNT_COLOR_DEF_EMPHASIS_DISABLE_B, App.Path & "\" & PROFILE_DEF_NAME)
            If m_hPenObspntEmphasisDisable = 0 Then m_hPenObspntEmphasisDisable = CreatePen(PS_SOLID, Vector_Width_Emphasis, RGB(lngColR, lngColG, lngColB))


            '強調固定観測点色。
            lngColR = GetPrivateProfileInt(PROFILE_OBSPNT_COLOR_SEC, OBSPNT_COLOR_KEY_EMPHASIS_FIXED_R, OBSPNT_COLOR_DEF_EMPHASIS_FIXED_R, App.Path & "\" & PROFILE_DEF_NAME)
            lngColG = GetPrivateProfileInt(PROFILE_OBSPNT_COLOR_SEC, OBSPNT_COLOR_KEY_EMPHASIS_FIXED_G, OBSPNT_COLOR_DEF_EMPHASIS_FIXED_G, App.Path & "\" & PROFILE_DEF_NAME)
            lngColB = GetPrivateProfileInt(PROFILE_OBSPNT_COLOR_SEC, OBSPNT_COLOR_KEY_EMPHASIS_FIXED_B, OBSPNT_COLOR_DEF_EMPHASIS_FIXED_B, App.Path & "\" & PROFILE_DEF_NAME)
            If m_hPenObspntEmphasisFixed = 0 Then m_hPenObspntEmphasisFixed = CreatePen(PS_SOLID, Vector_Width_Emphasis, RGB(lngColR, lngColG, lngColB))


            '登録閉合観測点色。
            lngColR = GetPrivateProfileInt(PROFILE_OBSPNT_COLOR_SEC, OBSPNT_COLOR_KEY_ANGLE_R, OBSPNT_COLOR_DEF_ANGLE_R, App.Path & "\" & PROFILE_DEF_NAME)
            lngColG = GetPrivateProfileInt(PROFILE_OBSPNT_COLOR_SEC, OBSPNT_COLOR_KEY_ANGLE_G, OBSPNT_COLOR_DEF_ANGLE_G, App.Path & "\" & PROFILE_DEF_NAME)
            lngColB = GetPrivateProfileInt(PROFILE_OBSPNT_COLOR_SEC, OBSPNT_COLOR_KEY_ANGLE_B, OBSPNT_COLOR_DEF_ANGLE_B, App.Path & "\" & PROFILE_DEF_NAME)
            If m_hPenObspntAngle = 0 Then m_hPenObspntAngle = CreatePen(PS_SOLID, Vector_Width_Emphasis, RGB(lngColR, lngColG, lngColB))


            '登録閉合固定観測点色。
            lngColR = GetPrivateProfileInt(PROFILE_OBSPNT_COLOR_SEC, OBSPNT_COLOR_KEY_ANGLE_FIXED_R, OBSPNT_COLOR_DEF_ANGLE_FIXED_R, App.Path & "\" & PROFILE_DEF_NAME)
            lngColG = GetPrivateProfileInt(PROFILE_OBSPNT_COLOR_SEC, OBSPNT_COLOR_KEY_ANGLE_FIXED_G, OBSPNT_COLOR_DEF_ANGLE_FIXED_G, App.Path & "\" & PROFILE_DEF_NAME)
            lngColB = GetPrivateProfileInt(PROFILE_OBSPNT_COLOR_SEC, OBSPNT_COLOR_KEY_ANGLE_FIXED_B, OBSPNT_COLOR_DEF_ANGLE_FIXED_B, App.Path & "\" & PROFILE_DEF_NAME)
            If m_hPenObspntAngleFixed = 0 Then m_hPenObspntAngleFixed = CreatePen(PS_SOLID, Vector_Width_Emphasis, RGB(lngColR, lngColG, lngColB))


            '閉合候補観測点色。
            lngColR = GetPrivateProfileInt(PROFILE_OBSPNT_COLOR_SEC, OBSPNT_COLOR_KEY_CANDIDATE_R, OBSPNT_COLOR_DEF_CANDIDATE_R, App.Path & "\" & PROFILE_DEF_NAME)
            lngColG = GetPrivateProfileInt(PROFILE_OBSPNT_COLOR_SEC, OBSPNT_COLOR_KEY_CANDIDATE_G, OBSPNT_COLOR_DEF_CANDIDATE_G, App.Path & "\" & PROFILE_DEF_NAME)
            lngColB = GetPrivateProfileInt(PROFILE_OBSPNT_COLOR_SEC, OBSPNT_COLOR_KEY_CANDIDATE_B, OBSPNT_COLOR_DEF_CANDIDATE_B, App.Path & "\" & PROFILE_DEF_NAME)
            If m_hPenObspntCandidate = 0 Then m_hPenObspntCandidate = CreatePen(PS_SOLID, Vector_Width_Emphasis, RGB(lngColR, lngColG, lngColB))


            '閉合候補固定観測点色。
            lngColR = GetPrivateProfileInt(PROFILE_OBSPNT_COLOR_SEC, OBSPNT_COLOR_KEY_CANDIDATE_FIXED_R, OBSPNT_COLOR_DEF_CANDIDATE_FIXED_R, App.Path & "\" & PROFILE_DEF_NAME)
            lngColG = GetPrivateProfileInt(PROFILE_OBSPNT_COLOR_SEC, OBSPNT_COLOR_KEY_CANDIDATE_FIXED_G, OBSPNT_COLOR_DEF_CANDIDATE_FIXED_G, App.Path & "\" & PROFILE_DEF_NAME)
            lngColB = GetPrivateProfileInt(PROFILE_OBSPNT_COLOR_SEC, OBSPNT_COLOR_KEY_CANDIDATE_FIXED_B, OBSPNT_COLOR_DEF_CANDIDATE_FIXED_B, App.Path & "\" & PROFILE_DEF_NAME)
            If m_hPenObspntCandidateFixed = 0 Then m_hPenObspntCandidateFixed = CreatePen(PS_SOLID, Vector_Width_Emphasis, RGB(lngColR, lngColG, lngColB))



            '文字色の設定
            '有効観測点記号色。
            lngColR = GetPrivateProfileInt(PROFILE_OBSPNT_COLOR_SEC, OBSPNT_COLOR_KEY_SYMBOL_ENABLE_R, OBSPNT_COLOR_DEF_SYMBOL_ENABLE_R, App.Path & "\" & PROFILE_DEF_NAME)
            lngColG = GetPrivateProfileInt(PROFILE_OBSPNT_COLOR_SEC, OBSPNT_COLOR_KEY_SYMBOL_ENABLE_G, OBSPNT_COLOR_DEF_SYMBOL_ENABLE_G, App.Path & "\" & PROFILE_DEF_NAME)
            lngColB = GetPrivateProfileInt(PROFILE_OBSPNT_COLOR_SEC, OBSPNT_COLOR_KEY_SYMBOL_ENABLE_B, OBSPNT_COLOR_DEF_SYMBOL_ENABLE_B, App.Path & "\" & PROFILE_DEF_NAME)
            Obspnt_Color_Symbol_Enable = RGB(lngColR, lngColG, lngColB) '有効観測点記号色。


            '無効観測点記号色。
            lngColR = GetPrivateProfileInt(PROFILE_OBSPNT_COLOR_SEC, OBSPNT_COLOR_KEY_SYMBOL_DISABLE_R, OBSPNT_COLOR_DEF_SYMBOL_DISABLE_R, App.Path & "\" & PROFILE_DEF_NAME)
            lngColG = GetPrivateProfileInt(PROFILE_OBSPNT_COLOR_SEC, OBSPNT_COLOR_KEY_SYMBOL_DISABLE_G, OBSPNT_COLOR_DEF_SYMBOL_DISABLE_G, App.Path & "\" & PROFILE_DEF_NAME)
            lngColB = GetPrivateProfileInt(PROFILE_OBSPNT_COLOR_SEC, OBSPNT_COLOR_KEY_SYMBOL_DISABLE_B, OBSPNT_COLOR_DEF_SYMBOL_DISABLE_B, App.Path & "\" & PROFILE_DEF_NAME)
            Obspnt_Color_Symbol_Disable = RGB(lngColR, lngColG, lngColB) '無効観測点記号色。


            '固定観測点記号色。
            lngColR = GetPrivateProfileInt(PROFILE_OBSPNT_COLOR_SEC, OBSPNT_COLOR_KEY_SYMBOL_FIXED_R, OBSPNT_COLOR_DEF_SYMBOL_FIXED_R, App.Path & "\" & PROFILE_DEF_NAME)
            lngColG = GetPrivateProfileInt(PROFILE_OBSPNT_COLOR_SEC, OBSPNT_COLOR_KEY_SYMBOL_FIXED_G, OBSPNT_COLOR_DEF_SYMBOL_FIXED_G, App.Path & "\" & PROFILE_DEF_NAME)
            lngColB = GetPrivateProfileInt(PROFILE_OBSPNT_COLOR_SEC, OBSPNT_COLOR_KEY_SYMBOL_FIXED_B, OBSPNT_COLOR_DEF_SYMBOL_FIXED_B, App.Path & "\" & PROFILE_DEF_NAME)
            Obspnt_Color_Symbol_Fixed = RGB(lngColR, lngColG, lngColB) '固定観測点記号色。


            '強調観測点記号色。
            lngColR = GetPrivateProfileInt(PROFILE_OBSPNT_COLOR_SEC, OBSPNT_COLOR_KEY_SYMBOL_EMPHASIS_R, OBSPNT_COLOR_DEF_SYMBOL_EMPHASIS_R, App.Path & "\" & PROFILE_DEF_NAME)
            lngColG = GetPrivateProfileInt(PROFILE_OBSPNT_COLOR_SEC, OBSPNT_COLOR_KEY_SYMBOL_EMPHASIS_G, OBSPNT_COLOR_DEF_SYMBOL_EMPHASIS_G, App.Path & "\" & PROFILE_DEF_NAME)
            lngColB = GetPrivateProfileInt(PROFILE_OBSPNT_COLOR_SEC, OBSPNT_COLOR_KEY_SYMBOL_EMPHASIS_B, OBSPNT_COLOR_DEF_SYMBOL_EMPHASIS_B, App.Path & "\" & PROFILE_DEF_NAME)
            Obspnt_Color_Symbol_Emphasis = RGB(lngColR, lngColG, lngColB) '強調観測点記号色。


            '強調無効観測点記号色。
            lngColR = GetPrivateProfileInt(PROFILE_OBSPNT_COLOR_SEC, OBSPNT_COLOR_KEY_SYMBOL_EMPHASIS_DISABLE_R, OBSPNT_COLOR_DEF_SYMBOL_EMPHASIS_DISABLE_R, App.Path & "\" & PROFILE_DEF_NAME)
            lngColG = GetPrivateProfileInt(PROFILE_OBSPNT_COLOR_SEC, OBSPNT_COLOR_KEY_SYMBOL_EMPHASIS_DISABLE_G, OBSPNT_COLOR_DEF_SYMBOL_EMPHASIS_DISABLE_G, App.Path & "\" & PROFILE_DEF_NAME)
            lngColB = GetPrivateProfileInt(PROFILE_OBSPNT_COLOR_SEC, OBSPNT_COLOR_KEY_SYMBOL_EMPHASIS_DISABLE_B, OBSPNT_COLOR_DEF_SYMBOL_EMPHASIS_DISABLE_B, App.Path & "\" & PROFILE_DEF_NAME)
            Obspnt_Color_Symbol_Emphasis_Disable = RGB(lngColR, lngColG, lngColB) '強調無効観測点記号色。


            '強調固定観測点記号色。
            lngColR = GetPrivateProfileInt(PROFILE_OBSPNT_COLOR_SEC, OBSPNT_COLOR_KEY_SYMBOL_EMPHASIS_FIXED_R, OBSPNT_COLOR_DEF_SYMBOL_EMPHASIS_FIXED_R, App.Path & "\" & PROFILE_DEF_NAME)
            lngColG = GetPrivateProfileInt(PROFILE_OBSPNT_COLOR_SEC, OBSPNT_COLOR_KEY_SYMBOL_EMPHASIS_FIXED_G, OBSPNT_COLOR_DEF_SYMBOL_EMPHASIS_FIXED_G, App.Path & "\" & PROFILE_DEF_NAME)
            lngColB = GetPrivateProfileInt(PROFILE_OBSPNT_COLOR_SEC, OBSPNT_COLOR_KEY_SYMBOL_EMPHASIS_FIXED_B, OBSPNT_COLOR_DEF_SYMBOL_EMPHASIS_FIXED_B, App.Path & "\" & PROFILE_DEF_NAME)
            Obspnt_Color_Symbol_Emphasis_Fixed = RGB(lngColR, lngColG, lngColB) '強調固定観測点記号色。


            '登録閉合観測点記号色。
            lngColR = GetPrivateProfileInt(PROFILE_OBSPNT_COLOR_SEC, OBSPNT_COLOR_KEY_SYMBOL_ANGLE_R, OBSPNT_COLOR_DEF_SYMBOL_ANGLE_R, App.Path & "\" & PROFILE_DEF_NAME)
            lngColG = GetPrivateProfileInt(PROFILE_OBSPNT_COLOR_SEC, OBSPNT_COLOR_KEY_SYMBOL_ANGLE_G, OBSPNT_COLOR_DEF_SYMBOL_ANGLE_G, App.Path & "\" & PROFILE_DEF_NAME)
            lngColB = GetPrivateProfileInt(PROFILE_OBSPNT_COLOR_SEC, OBSPNT_COLOR_KEY_SYMBOL_ANGLE_B, OBSPNT_COLOR_DEF_SYMBOL_ANGLE_B, App.Path & "\" & PROFILE_DEF_NAME)
            Obspnt_Color_Symbol_Angle = RGB(lngColR, lngColG, lngColB) '登録閉合観測点記号色。


            '登録閉合固定観測点記号色。
            lngColR = GetPrivateProfileInt(PROFILE_OBSPNT_COLOR_SEC, OBSPNT_COLOR_KEY_SYMBOL_ANGLE_FIXED_R, OBSPNT_COLOR_DEF_SYMBOL_ANGLE_FIXED_R, App.Path & "\" & PROFILE_DEF_NAME)
            lngColG = GetPrivateProfileInt(PROFILE_OBSPNT_COLOR_SEC, OBSPNT_COLOR_KEY_SYMBOL_ANGLE_FIXED_G, OBSPNT_COLOR_DEF_SYMBOL_ANGLE_FIXED_G, App.Path & "\" & PROFILE_DEF_NAME)
            lngColB = GetPrivateProfileInt(PROFILE_OBSPNT_COLOR_SEC, OBSPNT_COLOR_KEY_SYMBOL_ANGLE_FIXED_B, OBSPNT_COLOR_DEF_SYMBOL_ANGLE_FIXED_B, App.Path & "\" & PROFILE_DEF_NAME)
            Obspnt_Color_Symbol_Angle_Fixed = RGB(lngColR, lngColG, lngColB) '登録閉合固定観測点記号色。


            '閉合候補観測点記号色。
            lngColR = GetPrivateProfileInt(PROFILE_OBSPNT_COLOR_SEC, OBSPNT_COLOR_KEY_SYMBOL_CANDIDATE_R, OBSPNT_COLOR_DEF_SYMBOL_CANDIDATE_R, App.Path & "\" & PROFILE_DEF_NAME)
            lngColG = GetPrivateProfileInt(PROFILE_OBSPNT_COLOR_SEC, OBSPNT_COLOR_KEY_SYMBOL_CANDIDATE_G, OBSPNT_COLOR_DEF_SYMBOL_CANDIDATE_G, App.Path & "\" & PROFILE_DEF_NAME)
            lngColB = GetPrivateProfileInt(PROFILE_OBSPNT_COLOR_SEC, OBSPNT_COLOR_KEY_SYMBOL_CANDIDATE_B, OBSPNT_COLOR_DEF_SYMBOL_CANDIDATE_B, App.Path & "\" & PROFILE_DEF_NAME)
            Obspnt_Color_Symbol_Candidate = RGB(lngColR, lngColG, lngColB) '閉合候補観測点記号色。


            '閉合候補固定観測点記号色。
            lngColR = GetPrivateProfileInt(PROFILE_OBSPNT_COLOR_SEC, OBSPNT_COLOR_KEY_SYMBOL_CANDIDATE_FIXED_R, OBSPNT_COLOR_DEF_SYMBOL_CANDIDATE_FIXED_R, App.Path & "\" & PROFILE_DEF_NAME)
            lngColG = GetPrivateProfileInt(PROFILE_OBSPNT_COLOR_SEC, OBSPNT_COLOR_KEY_SYMBOL_CANDIDATE_FIXED_G, OBSPNT_COLOR_DEF_SYMBOL_CANDIDATE_FIXED_G, App.Path & "\" & PROFILE_DEF_NAME)
            lngColB = GetPrivateProfileInt(PROFILE_OBSPNT_COLOR_SEC, OBSPNT_COLOR_KEY_SYMBOL_CANDIDATE_FIXED_B, OBSPNT_COLOR_DEF_SYMBOL_CANDIDATE_FIXED_B, App.Path & "\" & PROFILE_DEF_NAME)
            Obspnt_Color_Symbol_Candidate_Fixed = RGB(lngColR, lngColG, lngColB) '閉合候補固定観測点記号色。



        '    If m_hPenVectorEnable = 0 Then m_hPenVectorEnable = CreatePen(PS_SOLID, VECTOR_WIDTH_NORMAL, VECTOR_COLOR_ENABLE)
        '    If m_hPenVectorDisable = 0 Then m_hPenVectorDisable = CreatePen(PS_SOLID, VECTOR_WIDTH_NORMAL, VECTOR_COLOR_DISABLE)
        '    If m_hPenVectorFloat = 0 Then m_hPenVectorFloat = CreatePen(PS_SOLID, VECTOR_WIDTH_NORMAL, VECTOR_COLOR_FLOAT)
        '    If m_hPenVectorFix = 0 Then m_hPenVectorFix = CreatePen(PS_SOLID, VECTOR_WIDTH_NORMAL, VECTOR_COLOR_FIX)
        '    If m_hPenVectorFailed = 0 Then m_hPenVectorFailed = CreatePen(PS_SOLID, VECTOR_WIDTH_NORMAL, VECTOR_COLOR_FAILED)
        '    If m_hPenVectorAngle = 0 Then m_hPenVectorAngle = CreatePen(PS_SOLID, VECTOR_WIDTH_EMPHASIS, VECTOR_COLOR_ANGLE)
        '    If m_hPenVectorCandidate = 0 Then m_hPenVectorCandidate = CreatePen(PS_SOLID, VECTOR_WIDTH_EMPHASIS, VECTOR_COLOR_CANDIDATE)
        '    If m_hPenVectorCorrect = 0 Then m_hPenVectorCorrect = CreatePen(PS_SOLID, VECTOR_WIDTH_NORMAL, VECTOR_COLOR_CORRECT)
        '    If m_hPenVectorEccentric = 0 Then m_hPenVectorEccentric = CreatePen(PS_SOLID, VECTOR_WIDTH_NORMAL, VECTOR_COLOR_ECCENTRIC)
        '    If m_hPenVectorEmphasis = 0 Then m_hPenVectorEmphasis = CreatePen(PS_SOLID, VECTOR_WIDTH_EMPHASIS, VECTOR_COLOR_EMPHASIS)
        '    If m_hPenVectorEmphasisDisable = 0 Then m_hPenVectorEmphasisDisable = CreatePen(PS_SOLID, VECTOR_WIDTH_EMPHASIS, VECTOR_COLOR_EMPHASIS_DISABLE)
        '    If m_hPenVectorEmphasisFloat = 0 Then m_hPenVectorEmphasisFloat = CreatePen(PS_SOLID, VECTOR_WIDTH_EMPHASIS, VECTOR_COLOR_EMPHASIS_FLOAT)
        '    If m_hPenVectorEmphasisFix = 0 Then m_hPenVectorEmphasisFix = CreatePen(PS_SOLID, VECTOR_WIDTH_EMPHASIS, VECTOR_COLOR_EMPHASIS_FIX)
        '    If m_hPenVectorEmphasisFailed = 0 Then m_hPenVectorEmphasisFailed = CreatePen(PS_SOLID, VECTOR_WIDTH_EMPHASIS, VECTOR_COLOR_EMPHASIS_FAILED)
        '    If m_hPenVectorEmphasisCandidate = 0 Then m_hPenVectorEmphasisCandidate = CreatePen(PS_SOLID, VECTOR_WIDTH_EMPHASIS, VECTOR_COLOR_EMPHASIS_CANDIDATE)
        '    If m_hPenVectorEmphasisCorrect = 0 Then m_hPenVectorEmphasisCorrect = CreatePen(PS_SOLID, VECTOR_WIDTH_EMPHASIS, VECTOR_COLOR_CORRECT)


            'ベクトル色の設定
            '未解析基線ベクトル色。
            lngColR = GetPrivateProfileInt(PROFILE_VECTOR_COLOR_SEC, VECTOR_COLOR_KEY_ENABLE_R, VECTOR_COLOR_DEF_ENABLE_R, App.Path & "\" & PROFILE_DEF_NAME)
            lngColG = GetPrivateProfileInt(PROFILE_VECTOR_COLOR_SEC, VECTOR_COLOR_KEY_ENABLE_G, VECTOR_COLOR_DEF_ENABLE_G, App.Path & "\" & PROFILE_DEF_NAME)
            lngColB = GetPrivateProfileInt(PROFILE_VECTOR_COLOR_SEC, VECTOR_COLOR_KEY_ENABLE_B, VECTOR_COLOR_DEF_ENABLE_B, App.Path & "\" & PROFILE_DEF_NAME)
            If m_hPenVectorEnable = 0 Then m_hPenVectorEnable = CreatePen(PS_SOLID, Vector_Width_Normal, RGB(lngColR, lngColG, lngColB))


            '無効基線ベクトル色。
            lngColR = GetPrivateProfileInt(PROFILE_VECTOR_COLOR_SEC, VECTOR_COLOR_KEY_DISABLE_R, VECTOR_COLOR_DEF_DISABLE_R, App.Path & "\" & PROFILE_DEF_NAME)
            lngColG = GetPrivateProfileInt(PROFILE_VECTOR_COLOR_SEC, VECTOR_COLOR_KEY_DISABLE_G, VECTOR_COLOR_DEF_DISABLE_G, App.Path & "\" & PROFILE_DEF_NAME)
            lngColB = GetPrivateProfileInt(PROFILE_VECTOR_COLOR_SEC, VECTOR_COLOR_KEY_DISABLE_B, VECTOR_COLOR_DEF_DISABLE_B, App.Path & "\" & PROFILE_DEF_NAME)
            If m_hPenVectorDisable = 0 Then m_hPenVectorDisable = CreatePen(PS_SOLID, Vector_Width_Normal, RGB(lngColR, lngColG, lngColB))


            'FLOAT基線ベクトル色。
            lngColR = GetPrivateProfileInt(PROFILE_VECTOR_COLOR_SEC, VECTOR_COLOR_KEY_FLOAT_R, VECTOR_COLOR_DEF_FLOAT_R, App.Path & "\" & PROFILE_DEF_NAME)
            lngColG = GetPrivateProfileInt(PROFILE_VECTOR_COLOR_SEC, VECTOR_COLOR_KEY_FLOAT_G, VECTOR_COLOR_DEF_FLOAT_G, App.Path & "\" & PROFILE_DEF_NAME)
            lngColB = GetPrivateProfileInt(PROFILE_VECTOR_COLOR_SEC, VECTOR_COLOR_KEY_FLOAT_B, VECTOR_COLOR_DEF_FLOAT_B, App.Path & "\" & PROFILE_DEF_NAME)
            If m_hPenVectorFloat = 0 Then m_hPenVectorFloat = CreatePen(PS_SOLID, Vector_Width_Normal, RGB(lngColR, lngColG, lngColB))


            'FIX基線ベクトル色。
            lngColR = GetPrivateProfileInt(PROFILE_VECTOR_COLOR_SEC, VECTOR_COLOR_KEY_FIX_R, VECTOR_COLOR_DEF_FIX_R, App.Path & "\" & PROFILE_DEF_NAME)
            lngColG = GetPrivateProfileInt(PROFILE_VECTOR_COLOR_SEC, VECTOR_COLOR_KEY_FIX_G, VECTOR_COLOR_DEF_FIX_G, App.Path & "\" & PROFILE_DEF_NAME)
            lngColB = GetPrivateProfileInt(PROFILE_VECTOR_COLOR_SEC, VECTOR_COLOR_KEY_FIX_B, VECTOR_COLOR_DEF_FIX_B, App.Path & "\" & PROFILE_DEF_NAME)
            If m_hPenVectorFix = 0 Then m_hPenVectorFix = CreatePen(PS_SOLID, Vector_Width_Normal, RGB(lngColR, lngColG, lngColB))


            'FIX(バイアス決定比<最小バイアス決定比)基線ベクトル色。2006/10/12 NGS Yamada
            lngColR = GetPrivateProfileInt(PROFILE_VECTOR_COLOR_SEC, VECTOR_COLOR_KEY_FIX_LOWRATIO_R, VECTOR_COLOR_DEF_FIX_LOWRATIO_R, App.Path & "\" & PROFILE_DEF_NAME)
            lngColG = GetPrivateProfileInt(PROFILE_VECTOR_COLOR_SEC, VECTOR_COLOR_KEY_FIX_LOWRATIO_G, VECTOR_COLOR_DEF_FIX_LOWRATIO_G, App.Path & "\" & PROFILE_DEF_NAME)
            lngColB = GetPrivateProfileInt(PROFILE_VECTOR_COLOR_SEC, VECTOR_COLOR_KEY_FIX_LOWRATIO_B, VECTOR_COLOR_DEF_FIX_LOWRATIO_B, App.Path & "\" & PROFILE_DEF_NAME)
            If m_hPenVectorFix_LowRatio = 0 Then m_hPenVectorFix_LowRatio = CreatePen(PS_SOLID, Vector_Width_Normal, RGB(lngColR, lngColG, lngColB))


            '解析失敗基線ベクトル色。
            lngColR = GetPrivateProfileInt(PROFILE_VECTOR_COLOR_SEC, VECTOR_COLOR_KEY_FAILED_R, VECTOR_COLOR_DEF_FAILED_R, App.Path & "\" & PROFILE_DEF_NAME)
            lngColG = GetPrivateProfileInt(PROFILE_VECTOR_COLOR_SEC, VECTOR_COLOR_KEY_FAILED_G, VECTOR_COLOR_DEF_FAILED_G, App.Path & "\" & PROFILE_DEF_NAME)
            lngColB = GetPrivateProfileInt(PROFILE_VECTOR_COLOR_SEC, VECTOR_COLOR_KEY_FAILED_B, VECTOR_COLOR_DEF_FAILED_B, App.Path & "\" & PROFILE_DEF_NAME)
            If m_hPenVectorFailed = 0 Then m_hPenVectorFailed = CreatePen(PS_SOLID, Vector_Width_Normal, RGB(lngColR, lngColG, lngColB))


            '登録閉合基線ベクトル色。
            lngColR = GetPrivateProfileInt(PROFILE_VECTOR_COLOR_SEC, VECTOR_COLOR_KEY_ANGLE_R, VECTOR_COLOR_DEF_ANGLE_R, App.Path & "\" & PROFILE_DEF_NAME)
            lngColG = GetPrivateProfileInt(PROFILE_VECTOR_COLOR_SEC, VECTOR_COLOR_KEY_ANGLE_G, VECTOR_COLOR_DEF_ANGLE_G, App.Path & "\" & PROFILE_DEF_NAME)
            lngColB = GetPrivateProfileInt(PROFILE_VECTOR_COLOR_SEC, VECTOR_COLOR_KEY_ANGLE_B, VECTOR_COLOR_DEF_ANGLE_B, App.Path & "\" & PROFILE_DEF_NAME)
            If m_hPenVectorAngle = 0 Then m_hPenVectorAngle = CreatePen(PS_SOLID, Vector_Width_Emphasis, RGB(lngColR, lngColG, lngColB))


            '閉合候補基線ベクトル色。
            lngColR = GetPrivateProfileInt(PROFILE_VECTOR_COLOR_SEC, VECTOR_COLOR_KEY_CANDIDATE_R, VECTOR_COLOR_DEF_CANDIDATE_R, App.Path & "\" & PROFILE_DEF_NAME)
            lngColG = GetPrivateProfileInt(PROFILE_VECTOR_COLOR_SEC, VECTOR_COLOR_KEY_CANDIDATE_G, VECTOR_COLOR_DEF_CANDIDATE_G, App.Path & "\" & PROFILE_DEF_NAME)
            lngColB = GetPrivateProfileInt(PROFILE_VECTOR_COLOR_SEC, VECTOR_COLOR_KEY_CANDIDATE_B, VECTOR_COLOR_DEF_CANDIDATE_B, App.Path & "\" & PROFILE_DEF_NAME)
            If m_hPenVectorCandidate = 0 Then m_hPenVectorCandidate = CreatePen(PS_SOLID, Vector_Width_Emphasis, RGB(lngColR, lngColG, lngColB))


            '偏心補正基線ベクトル色。
            lngColR = GetPrivateProfileInt(PROFILE_VECTOR_COLOR_SEC, VECTOR_COLOR_KEY_CORRECT_R, VECTOR_COLOR_DEF_CORRECT_R, App.Path & "\" & PROFILE_DEF_NAME)
            lngColG = GetPrivateProfileInt(PROFILE_VECTOR_COLOR_SEC, VECTOR_COLOR_KEY_CORRECT_G, VECTOR_COLOR_DEF_CORRECT_G, App.Path & "\" & PROFILE_DEF_NAME)
            lngColB = GetPrivateProfileInt(PROFILE_VECTOR_COLOR_SEC, VECTOR_COLOR_KEY_CORRECT_B, VECTOR_COLOR_DEF_CORRECT_B, App.Path & "\" & PROFILE_DEF_NAME)
            If m_hPenVectorCorrect = 0 Then m_hPenVectorCorrect = CreatePen(PS_SOLID, Vector_Width_Normal, RGB(lngColR, lngColG, lngColB))


            '偏心ベクトル色。
            lngColR = GetPrivateProfileInt(PROFILE_VECTOR_COLOR_SEC, VECTOR_COLOR_KEY_ECCENTRIC_R, VECTOR_COLOR_DEF_ECCENTRIC_R, App.Path & "\" & PROFILE_DEF_NAME)
            lngColG = GetPrivateProfileInt(PROFILE_VECTOR_COLOR_SEC, VECTOR_COLOR_KEY_ECCENTRIC_G, VECTOR_COLOR_DEF_ECCENTRIC_G, App.Path & "\" & PROFILE_DEF_NAME)
            lngColB = GetPrivateProfileInt(PROFILE_VECTOR_COLOR_SEC, VECTOR_COLOR_KEY_ECCENTRIC_B, VECTOR_COLOR_DEF_ECCENTRIC_B, App.Path & "\" & PROFILE_DEF_NAME)
            If m_hPenVectorEccentric = 0 Then m_hPenVectorEccentric = CreatePen(PS_SOLID, Vector_Width_Normal, RGB(lngColR, lngColG, lngColB))


            '強調未解析基線ベクトル色。
            lngColR = GetPrivateProfileInt(PROFILE_VECTOR_COLOR_SEC, VECTOR_COLOR_KEY_EMPHASIS_R, VECTOR_COLOR_DEF_EMPHASIS_R, App.Path & "\" & PROFILE_DEF_NAME)
            lngColG = GetPrivateProfileInt(PROFILE_VECTOR_COLOR_SEC, VECTOR_COLOR_KEY_EMPHASIS_G, VECTOR_COLOR_DEF_EMPHASIS_G, App.Path & "\" & PROFILE_DEF_NAME)
            lngColB = GetPrivateProfileInt(PROFILE_VECTOR_COLOR_SEC, VECTOR_COLOR_KEY_EMPHASIS_B, VECTOR_COLOR_DEF_EMPHASIS_B, App.Path & "\" & PROFILE_DEF_NAME)
            If m_hPenVectorEmphasis = 0 Then m_hPenVectorEmphasis = CreatePen(PS_SOLID, Vector_Width_Emphasis, RGB(lngColR, lngColG, lngColB))


            '強調無効基線ベクトル色。
            lngColR = GetPrivateProfileInt(PROFILE_VECTOR_COLOR_SEC, VECTOR_COLOR_KEY_EMPHASIS_DISABLE_R, VECTOR_COLOR_DEF_EMPHASIS_DISABLE_R, App.Path & "\" & PROFILE_DEF_NAME)
            lngColG = GetPrivateProfileInt(PROFILE_VECTOR_COLOR_SEC, VECTOR_COLOR_KEY_EMPHASIS_DISABLE_G, VECTOR_COLOR_DEF_EMPHASIS_DISABLE_G, App.Path & "\" & PROFILE_DEF_NAME)
            lngColB = GetPrivateProfileInt(PROFILE_VECTOR_COLOR_SEC, VECTOR_COLOR_KEY_EMPHASIS_DISABLE_B, VECTOR_COLOR_DEF_EMPHASIS_DISABLE_B, App.Path & "\" & PROFILE_DEF_NAME)
            If m_hPenVectorEmphasisDisable = 0 Then m_hPenVectorEmphasisDisable = CreatePen(PS_SOLID, Vector_Width_Emphasis, RGB(lngColR, lngColG, lngColB))


            '強調FLOAT基線ベクトル色。
            lngColR = GetPrivateProfileInt(PROFILE_VECTOR_COLOR_SEC, VECTOR_COLOR_KEY_EMPHASIS_FLOAT_R, VECTOR_COLOR_DEF_EMPHASIS_FLOAT_R, App.Path & "\" & PROFILE_DEF_NAME)
            lngColG = GetPrivateProfileInt(PROFILE_VECTOR_COLOR_SEC, VECTOR_COLOR_KEY_EMPHASIS_FLOAT_G, VECTOR_COLOR_DEF_EMPHASIS_FLOAT_G, App.Path & "\" & PROFILE_DEF_NAME)
            lngColB = GetPrivateProfileInt(PROFILE_VECTOR_COLOR_SEC, VECTOR_COLOR_KEY_EMPHASIS_FLOAT_B, VECTOR_COLOR_DEF_EMPHASIS_FLOAT_B, App.Path & "\" & PROFILE_DEF_NAME)
            If m_hPenVectorEmphasisFloat = 0 Then m_hPenVectorEmphasisFloat = CreatePen(PS_SOLID, Vector_Width_Emphasis, RGB(lngColR, lngColG, lngColB))


            '強調FIX基線ベクトル色。
            lngColR = GetPrivateProfileInt(PROFILE_VECTOR_COLOR_SEC, VECTOR_COLOR_KEY_EMPHASIS_FIX_R, VECTOR_COLOR_DEF_EMPHASIS_FIX_R, App.Path & "\" & PROFILE_DEF_NAME)
            lngColG = GetPrivateProfileInt(PROFILE_VECTOR_COLOR_SEC, VECTOR_COLOR_KEY_EMPHASIS_FIX_G, VECTOR_COLOR_DEF_EMPHASIS_FIX_G, App.Path & "\" & PROFILE_DEF_NAME)
            lngColB = GetPrivateProfileInt(PROFILE_VECTOR_COLOR_SEC, VECTOR_COLOR_KEY_EMPHASIS_FIX_B, VECTOR_COLOR_DEF_EMPHASIS_FIX_B, App.Path & "\" & PROFILE_DEF_NAME)
            If m_hPenVectorEmphasisFix = 0 Then m_hPenVectorEmphasisFix = CreatePen(PS_SOLID, Vector_Width_Emphasis, RGB(lngColR, lngColG, lngColB))


            '強調FIX(バイアス決定比<最小バイアス決定比)基線ベクトル。　2006/10/12 NGS Yamada
            lngColR = GetPrivateProfileInt(PROFILE_VECTOR_COLOR_SEC, VECTOR_COLOR_KEY_EMPHASIS_FIX_LOWRATIO_R, VECTOR_COLOR_DEF_EMPHASIS_FIX_LOWRATIO_R, App.Path & "\" & PROFILE_DEF_NAME)
            lngColG = GetPrivateProfileInt(PROFILE_VECTOR_COLOR_SEC, VECTOR_COLOR_KEY_EMPHASIS_FIX_LOWRATIO_G, VECTOR_COLOR_DEF_EMPHASIS_FIX_LOWRATIO_G, App.Path & "\" & PROFILE_DEF_NAME)
            lngColB = GetPrivateProfileInt(PROFILE_VECTOR_COLOR_SEC, VECTOR_COLOR_KEY_EMPHASIS_FIX_LOWRATIO_B, VECTOR_COLOR_DEF_EMPHASIS_FIX_LOWRATIO_B, App.Path & "\" & PROFILE_DEF_NAME)
            If m_hPenVectorEmphasisFix_LowRatio = 0 Then m_hPenVectorEmphasisFix_LowRatio = CreatePen(PS_SOLID, Vector_Width_Emphasis, RGB(lngColR, lngColG, lngColB))

            '強調解析失敗基線ベクトル色。
            lngColR = GetPrivateProfileInt(PROFILE_VECTOR_COLOR_SEC, VECTOR_COLOR_KEY_EMPHASIS_FAILED_R, VECTOR_COLOR_DEF_EMPHASIS_FAILED_R, App.Path & "\" & PROFILE_DEF_NAME)
            lngColG = GetPrivateProfileInt(PROFILE_VECTOR_COLOR_SEC, VECTOR_COLOR_KEY_EMPHASIS_FAILED_G, VECTOR_COLOR_DEF_EMPHASIS_FAILED_G, App.Path & "\" & PROFILE_DEF_NAME)
            lngColB = GetPrivateProfileInt(PROFILE_VECTOR_COLOR_SEC, VECTOR_COLOR_KEY_EMPHASIS_FAILED_B, VECTOR_COLOR_DEF_EMPHASIS_FAILED_B, App.Path & "\" & PROFILE_DEF_NAME)
            If m_hPenVectorEmphasisFailed = 0 Then m_hPenVectorEmphasisFailed = CreatePen(PS_SOLID, Vector_Width_Emphasis, RGB(lngColR, lngColG, lngColB))


            '強調閉合候補基線ベクトル色。
            lngColR = GetPrivateProfileInt(PROFILE_VECTOR_COLOR_SEC, VECTOR_COLOR_KEY_EMPHASIS_CANDIDATE_R, VECTOR_COLOR_DEF_EMPHASIS_CANDIDATE_R, App.Path & "\" & PROFILE_DEF_NAME)
            lngColG = GetPrivateProfileInt(PROFILE_VECTOR_COLOR_SEC, VECTOR_COLOR_KEY_EMPHASIS_CANDIDATE_G, VECTOR_COLOR_DEF_EMPHASIS_CANDIDATE_G, App.Path & "\" & PROFILE_DEF_NAME)
            lngColB = GetPrivateProfileInt(PROFILE_VECTOR_COLOR_SEC, VECTOR_COLOR_KEY_EMPHASIS_CANDIDATE_B, VECTOR_COLOR_DEF_EMPHASIS_CANDIDATE_B, App.Path & "\" & PROFILE_DEF_NAME)
            If m_hPenVectorEmphasisCandidate = 0 Then m_hPenVectorEmphasisCandidate = CreatePen(PS_SOLID, Vector_Width_Emphasis, RGB(lngColR, lngColG, lngColB))


            '強調偏心補正基線ベクトル色。
            lngColR = GetPrivateProfileInt(PROFILE_VECTOR_COLOR_SEC, VECTOR_COLOR_KEY_CORRECT_R, VECTOR_COLOR_DEF_CORRECT_R, App.Path & "\" & PROFILE_DEF_NAME)
            lngColG = GetPrivateProfileInt(PROFILE_VECTOR_COLOR_SEC, VECTOR_COLOR_KEY_CORRECT_G, VECTOR_COLOR_DEF_CORRECT_G, App.Path & "\" & PROFILE_DEF_NAME)
            lngColB = GetPrivateProfileInt(PROFILE_VECTOR_COLOR_SEC, VECTOR_COLOR_KEY_CORRECT_B, VECTOR_COLOR_DEF_CORRECT_B, App.Path & "\" & PROFILE_DEF_NAME)
            If m_hPenVectorEmphasisCorrect = 0 Then m_hPenVectorEmphasisCorrect = CreatePen(PS_SOLID, Vector_Width_Emphasis, RGB(lngColR, lngColG, lngColB))


            If m_nPlotType = PLOTTYPE_STANDARD Or m_nPlotType = PLOTTYPE_ANGLE Then
                lngColR = GetPrivateProfileInt(PROFILE_VECTOR_COLOR_SEC, VECTOR_COLOR_KEY_ENABLE_R, VECTOR_COLOR_DEF_ENABLE_R, App.Path & "\" & PROFILE_DEF_NAME)
                lngColG = GetPrivateProfileInt(PROFILE_VECTOR_COLOR_SEC, VECTOR_COLOR_KEY_ENABLE_G, VECTOR_COLOR_DEF_ENABLE_G, App.Path & "\" & PROFILE_DEF_NAME)
                lngColB = GetPrivateProfileInt(PROFILE_VECTOR_COLOR_SEC, VECTOR_COLOR_KEY_ENABLE_B, VECTOR_COLOR_DEF_ENABLE_B, App.Path & "\" & PROFILE_DEF_NAME)
                If m_hPenVectorDuplication = 0 Then m_hPenVectorDuplication = CreatePen(PS_SOLID, Vector_Width_Duplication, RGB(lngColR, lngColG, lngColB))


                lngColR = GetPrivateProfileInt(PROFILE_VECTOR_COLOR_SEC, VECTOR_COLOR_KEY_FLOAT_R, VECTOR_COLOR_DEF_FLOAT_R, App.Path & "\" & PROFILE_DEF_NAME)
                lngColG = GetPrivateProfileInt(PROFILE_VECTOR_COLOR_SEC, VECTOR_COLOR_KEY_FLOAT_G, VECTOR_COLOR_DEF_FLOAT_G, App.Path & "\" & PROFILE_DEF_NAME)
                lngColB = GetPrivateProfileInt(PROFILE_VECTOR_COLOR_SEC, VECTOR_COLOR_KEY_FLOAT_B, VECTOR_COLOR_DEF_FLOAT_B, App.Path & "\" & PROFILE_DEF_NAME)
                If m_hPenVectorDuplicationFloat = 0 Then m_hPenVectorDuplicationFloat = CreatePen(PS_SOLID, Vector_Width_Duplication, RGB(lngColR, lngColG, lngColB))


                lngColR = GetPrivateProfileInt(PROFILE_VECTOR_COLOR_SEC, VECTOR_COLOR_KEY_FIX_R, VECTOR_COLOR_DEF_FIX_R, App.Path & "\" & PROFILE_DEF_NAME)
                lngColG = GetPrivateProfileInt(PROFILE_VECTOR_COLOR_SEC, VECTOR_COLOR_KEY_FIX_G, VECTOR_COLOR_DEF_FIX_G, App.Path & "\" & PROFILE_DEF_NAME)
                lngColB = GetPrivateProfileInt(PROFILE_VECTOR_COLOR_SEC, VECTOR_COLOR_KEY_FIX_B, VECTOR_COLOR_DEF_FIX_B, App.Path & "\" & PROFILE_DEF_NAME)
                If m_hPenVectorDuplicationFix = 0 Then m_hPenVectorDuplicationFix = CreatePen(PS_SOLID, Vector_Width_Duplication, RGB(lngColR, lngColG, lngColB))


                lngColR = GetPrivateProfileInt(PROFILE_VECTOR_COLOR_SEC, VECTOR_COLOR_KEY_FAILED_R, VECTOR_COLOR_DEF_FAILED_R, App.Path & "\" & PROFILE_DEF_NAME)
                lngColG = GetPrivateProfileInt(PROFILE_VECTOR_COLOR_SEC, VECTOR_COLOR_KEY_FAILED_G, VECTOR_COLOR_DEF_FAILED_G, App.Path & "\" & PROFILE_DEF_NAME)
                lngColB = GetPrivateProfileInt(PROFILE_VECTOR_COLOR_SEC, VECTOR_COLOR_KEY_FAILED_B, VECTOR_COLOR_DEF_FAILED_B, App.Path & "\" & PROFILE_DEF_NAME)
                If m_hPenVectorDuplicationFailed = 0 Then m_hPenVectorDuplicationFailed = CreatePen(PS_SOLID, Vector_Width_Duplication, RGB(lngColR, lngColG, lngColB))
            }else {
                Dim nLogPixelSX As Long
                nLogPixelSX = GetDeviceCaps(hDC, LOGPIXELSX)


                lngColR = GetPrivateProfileInt(PROFILE_VECTOR_COLOR_SEC, VECTOR_COLOR_KEY_ENABLE_R, VECTOR_COLOR_DEF_ENABLE_R, App.Path & "\" & PROFILE_DEF_NAME)
                lngColG = GetPrivateProfileInt(PROFILE_VECTOR_COLOR_SEC, VECTOR_COLOR_KEY_ENABLE_G, VECTOR_COLOR_DEF_ENABLE_G, App.Path & "\" & PROFILE_DEF_NAME)
                lngColB = GetPrivateProfileInt(PROFILE_VECTOR_COLOR_SEC, VECTOR_COLOR_KEY_ENABLE_B, VECTOR_COLOR_DEF_ENABLE_B, App.Path & "\" & PROFILE_DEF_NAME)
                If m_hPenVectorDuplication = 0 Then m_hPenVectorDuplication = CreatePen(PS_SOLID, 10 / 1440 * nLogPixelSX, RGB(lngColR, lngColG, lngColB))


                lngColR = GetPrivateProfileInt(PROFILE_VECTOR_COLOR_SEC, VECTOR_COLOR_KEY_FLOAT_R, VECTOR_COLOR_DEF_FLOAT_R, App.Path & "\" & PROFILE_DEF_NAME)
                lngColG = GetPrivateProfileInt(PROFILE_VECTOR_COLOR_SEC, VECTOR_COLOR_KEY_FLOAT_G, VECTOR_COLOR_DEF_FLOAT_G, App.Path & "\" & PROFILE_DEF_NAME)
                lngColB = GetPrivateProfileInt(PROFILE_VECTOR_COLOR_SEC, VECTOR_COLOR_KEY_FLOAT_B, VECTOR_COLOR_DEF_FLOAT_B, App.Path & "\" & PROFILE_DEF_NAME)
                If m_hPenVectorDuplicationFloat = 0 Then m_hPenVectorDuplicationFloat = CreatePen(PS_SOLID, 10 / 1440 * nLogPixelSX, RGB(lngColR, lngColG, lngColB))


                lngColR = GetPrivateProfileInt(PROFILE_VECTOR_COLOR_SEC, VECTOR_COLOR_KEY_FIX_R, VECTOR_COLOR_DEF_FIX_R, App.Path & "\" & PROFILE_DEF_NAME)
                lngColG = GetPrivateProfileInt(PROFILE_VECTOR_COLOR_SEC, VECTOR_COLOR_KEY_FIX_G, VECTOR_COLOR_DEF_FIX_G, App.Path & "\" & PROFILE_DEF_NAME)
                lngColB = GetPrivateProfileInt(PROFILE_VECTOR_COLOR_SEC, VECTOR_COLOR_KEY_FIX_B, VECTOR_COLOR_DEF_FIX_B, App.Path & "\" & PROFILE_DEF_NAME)
                If m_hPenVectorDuplicationFix = 0 Then m_hPenVectorDuplicationFix = CreatePen(PS_SOLID, 10 / 1440 * nLogPixelSX, RGB(lngColR, lngColG, lngColB))


                lngColR = GetPrivateProfileInt(PROFILE_VECTOR_COLOR_SEC, VECTOR_COLOR_KEY_FAILED_R, VECTOR_COLOR_DEF_FAILED_R, App.Path & "\" & PROFILE_DEF_NAME)
                lngColG = GetPrivateProfileInt(PROFILE_VECTOR_COLOR_SEC, VECTOR_COLOR_KEY_FAILED_G, VECTOR_COLOR_DEF_FAILED_G, App.Path & "\" & PROFILE_DEF_NAME)
                lngColB = GetPrivateProfileInt(PROFILE_VECTOR_COLOR_SEC, VECTOR_COLOR_KEY_FAILED_B, VECTOR_COLOR_DEF_FAILED_B, App.Path & "\" & PROFILE_DEF_NAME)
                If m_hPenVectorDuplicationFailed = 0 Then m_hPenVectorDuplicationFailed = CreatePen(PS_SOLID, 10 / 1440 * nLogPixelSX, RGB(lngColR, lngColG, lngColB))
            End If


        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '*******************************************************************************
        'メソッド
        
        '初期化。
        '
        '引き数：
        'nPlotType プロット種別。
        'hDC デバイスコンテキストのハンドル。nPlotType が PLOTTYPE_PRINT の時に必要。
        */
        public void Initialize(PLOTTYPE_ENUM nPlotType, long hDC = 0)
        {
            long lngColR;
            long lngColG;
            long lngColB;

            m_nPlotType = nPlotType;

            if (m_hBrushNull == 0)
            {
                LOGBRUSH tLogBrush;
                tLogBrush.lbStyle = BS_NULL;
                tLogBrush.lbHatch = 0;
                tLogBrush.lbColor = 0;
                m_hBrushNull = CreateBrushIndirect(ref tLogBrush);
            }

            string AppPath = Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath);
            string allPath = AppPath + "\\" + PROFILE_DEF_NAME;

            //'線の太さ
            Vector_Width_Normal = GetPrivateProfileInt(PROFILE_VECTOR_WIDTH_SEC, VECTOR_WIDTH_KEY_NORMAL, (int)VECTOR_WIDTH_DEF_NORMAL, AppPath + "\\" + PROFILE_DEF_NAME);
            Vector_Width_Emphasis = GetPrivateProfileInt(PROFILE_VECTOR_WIDTH_SEC, VECTOR_WIDTH_KEY_EMPHASIS, (int)VECTOR_WIDTH_DEF_EMPHASIS, AppPath + "\\" + PROFILE_DEF_NAME);
            Vector_Width_Duplication = GetPrivateProfileInt(PROFILE_VECTOR_WIDTH_SEC, VECTOR_WIDTH_KEY_DUPLICATION, (int)VECTOR_WIDTH_DEF_DUPLICATION, AppPath + "\\" + PROFILE_DEF_NAME);


            //'シンボル色の設定
            //'有効観測点色。
            lngColR = GetPrivateProfileInt(PROFILE_OBSPNT_COLOR_SEC, OBSPNT_COLOR_KEY_ENABLE_R, (int)OBSPNT_COLOR_DEF_ENABLE_R, AppPath + "\\" + PROFILE_DEF_NAME);
            lngColG = GetPrivateProfileInt(PROFILE_OBSPNT_COLOR_SEC, OBSPNT_COLOR_KEY_ENABLE_G, (int)OBSPNT_COLOR_DEF_ENABLE_G, AppPath + "\\" + PROFILE_DEF_NAME);
            lngColB = GetPrivateProfileInt(PROFILE_OBSPNT_COLOR_SEC, OBSPNT_COLOR_KEY_ENABLE_B, (int)OBSPNT_COLOR_DEF_ENABLE_B, AppPath + "\\" + PROFILE_DEF_NAME);
            //if (m_hPenObspntEnable == 0) { m_hPenObspntEnable = CreatePen((int)PS_SOLID, (int)Vector_Width_Normal, (uint)((lngColR << 16) | (lngColG << 8) | lngColB)); }
            if (m_hPenObspntEnable == 0) { m_hPenObspntEnable = (PS_SOLID << 56) | (Vector_Width_Normal << 32) | (lngColR << 16) | (lngColG << 8) | lngColB; }


            //'無効観測点色。
            lngColR = GetPrivateProfileInt(PROFILE_OBSPNT_COLOR_SEC, OBSPNT_COLOR_KEY_DISABLE_R, (int)OBSPNT_COLOR_DEF_DISABLE_R, AppPath + "\\" + PROFILE_DEF_NAME);
            lngColG = GetPrivateProfileInt(PROFILE_OBSPNT_COLOR_SEC, OBSPNT_COLOR_KEY_DISABLE_G, (int)OBSPNT_COLOR_DEF_DISABLE_G, AppPath + "\\" + PROFILE_DEF_NAME);
            lngColB = GetPrivateProfileInt(PROFILE_OBSPNT_COLOR_SEC, OBSPNT_COLOR_KEY_DISABLE_B, (int)OBSPNT_COLOR_DEF_DISABLE_B, AppPath + "\\" + PROFILE_DEF_NAME);
            //if (m_hPenObspntDisable == 0) { m_hPenObspntDisable = CreatePen((int)PS_SOLID, (int)Vector_Width_Normal, (uint)((lngColR << 16) | (lngColG << 8) | lngColB)); }
            if (m_hPenObspntDisable == 0) { m_hPenObspntDisable = (PS_SOLID << 56) | (Vector_Width_Normal << 32) | (lngColR << 16) | (lngColG << 8) | lngColB; }


            //'固定観測点色。
            lngColR = GetPrivateProfileInt(PROFILE_OBSPNT_COLOR_SEC, OBSPNT_COLOR_KEY_FIXED_R, (int)OBSPNT_COLOR_DEF_FIXED_R, AppPath + "\\" + PROFILE_DEF_NAME);
            lngColG = GetPrivateProfileInt(PROFILE_OBSPNT_COLOR_SEC, OBSPNT_COLOR_KEY_FIXED_G, (int)OBSPNT_COLOR_DEF_FIXED_G, AppPath + "\\" + PROFILE_DEF_NAME);
            lngColB = GetPrivateProfileInt(PROFILE_OBSPNT_COLOR_SEC, OBSPNT_COLOR_KEY_FIXED_B, (int)OBSPNT_COLOR_DEF_FIXED_B, AppPath + "\\" + PROFILE_DEF_NAME);
            //if (m_hPenObspntFixed == 0) { m_hPenObspntFixed = CreatePen((int)PS_SOLID, (int)Vector_Width_Normal, (uint)((lngColR << 16) | (lngColG << 8) | lngColB)); }
            if (m_hPenObspntFixed == 0) { m_hPenObspntFixed = (PS_SOLID << 56) | (Vector_Width_Normal << 32) | (lngColR << 16) | (lngColG << 8) | lngColB; }


            //'強調観測点色。
            lngColR = GetPrivateProfileInt(PROFILE_OBSPNT_COLOR_SEC, OBSPNT_COLOR_KEY_EMPHASIS_R, (int)OBSPNT_COLOR_DEF_EMPHASIS_R, AppPath + "\\" + PROFILE_DEF_NAME);
            lngColG = GetPrivateProfileInt(PROFILE_OBSPNT_COLOR_SEC, OBSPNT_COLOR_KEY_EMPHASIS_G, (int)OBSPNT_COLOR_DEF_EMPHASIS_G, AppPath + "\\" + PROFILE_DEF_NAME);
            lngColB = GetPrivateProfileInt(PROFILE_OBSPNT_COLOR_SEC, OBSPNT_COLOR_KEY_EMPHASIS_B, (int)OBSPNT_COLOR_DEF_EMPHASIS_B, AppPath + "\\" + PROFILE_DEF_NAME);
            //if (m_hPenObspntEmphasis == 0) { m_hPenObspntEmphasis = CreatePen((int)PS_SOLID, (int)Vector_Width_Emphasis, (uint)((lngColR << 16) | (lngColG << 8) | lngColB)); }
            if (m_hPenObspntEmphasis == 0) { m_hPenObspntEmphasis = (PS_SOLID << 56) | (Vector_Width_Emphasis << 32) | (lngColR << 16) | (lngColG << 8) | lngColB; }


            //'強調無効観測点色。
            lngColR = GetPrivateProfileInt(PROFILE_OBSPNT_COLOR_SEC, OBSPNT_COLOR_KEY_EMPHASIS_DISABLE_R, (int)OBSPNT_COLOR_DEF_EMPHASIS_DISABLE_R, AppPath + "\\" + PROFILE_DEF_NAME);
            lngColG = GetPrivateProfileInt(PROFILE_OBSPNT_COLOR_SEC, OBSPNT_COLOR_KEY_EMPHASIS_DISABLE_G, (int)OBSPNT_COLOR_DEF_EMPHASIS_DISABLE_G, AppPath + "\\" + PROFILE_DEF_NAME);
            lngColB = GetPrivateProfileInt(PROFILE_OBSPNT_COLOR_SEC, OBSPNT_COLOR_KEY_EMPHASIS_DISABLE_B, (int)OBSPNT_COLOR_DEF_EMPHASIS_DISABLE_B, AppPath + "\\" + PROFILE_DEF_NAME);
            //if (m_hPenObspntEmphasisDisable == 0) { m_hPenObspntEmphasisDisable = CreatePen((int)PS_SOLID, (int)Vector_Width_Emphasis, (uint)((lngColR << 16) | (lngColG << 8) | lngColB)); }
            if (m_hPenObspntEmphasisDisable == 0) { m_hPenObspntEmphasisDisable = (PS_SOLID << 56) | (Vector_Width_Emphasis << 32) | (lngColR << 16) | (lngColG << 8) | lngColB; }


            //'強調固定観測点色。
            lngColR = GetPrivateProfileInt(PROFILE_OBSPNT_COLOR_SEC, OBSPNT_COLOR_KEY_EMPHASIS_FIXED_R, (int)OBSPNT_COLOR_DEF_EMPHASIS_FIXED_R, AppPath + "\\" + PROFILE_DEF_NAME);
            lngColG = GetPrivateProfileInt(PROFILE_OBSPNT_COLOR_SEC, OBSPNT_COLOR_KEY_EMPHASIS_FIXED_G, (int)OBSPNT_COLOR_DEF_EMPHASIS_FIXED_G, AppPath + "\\" + PROFILE_DEF_NAME);
            lngColB = GetPrivateProfileInt(PROFILE_OBSPNT_COLOR_SEC, OBSPNT_COLOR_KEY_EMPHASIS_FIXED_B, (int)OBSPNT_COLOR_DEF_EMPHASIS_FIXED_B, AppPath + "\\" + PROFILE_DEF_NAME);
            //if (m_hPenObspntEmphasisFixed == 0) { m_hPenObspntEmphasisFixed = CreatePen((int)PS_SOLID, (int)Vector_Width_Emphasis, (uint)((lngColR << 16) | (lngColG << 8) | lngColB)); }
            if (m_hPenObspntEmphasisFixed == 0) { m_hPenObspntEmphasisFixed = (PS_SOLID << 56) | (Vector_Width_Emphasis << 32) | (lngColR << 16) | (lngColG << 8) | lngColB; }


            //'登録閉合観測点色。
            lngColR = GetPrivateProfileInt(PROFILE_OBSPNT_COLOR_SEC, OBSPNT_COLOR_KEY_ANGLE_R, (int)OBSPNT_COLOR_DEF_ANGLE_R, AppPath + "\\" + PROFILE_DEF_NAME);
            lngColG = GetPrivateProfileInt(PROFILE_OBSPNT_COLOR_SEC, OBSPNT_COLOR_KEY_ANGLE_G, (int)OBSPNT_COLOR_DEF_ANGLE_G, AppPath + "\\" + PROFILE_DEF_NAME);
            lngColB = GetPrivateProfileInt(PROFILE_OBSPNT_COLOR_SEC, OBSPNT_COLOR_KEY_ANGLE_B, (int)OBSPNT_COLOR_DEF_ANGLE_B, AppPath + "\\" + PROFILE_DEF_NAME);
            //if (m_hPenObspntAngle == 0) { m_hPenObspntAngle = CreatePen((int)PS_SOLID, (int)Vector_Width_Emphasis, (uint)((lngColR << 16) | (lngColG << 8) | lngColB)); }
            if (m_hPenObspntAngle == 0) { m_hPenObspntAngle = (PS_SOLID << 56) | (Vector_Width_Emphasis << 32) | (lngColR << 16) | (lngColG << 8) | lngColB; }


            //'登録閉合固定観測点色。
            lngColR = GetPrivateProfileInt(PROFILE_OBSPNT_COLOR_SEC, OBSPNT_COLOR_KEY_ANGLE_FIXED_R, (int)OBSPNT_COLOR_DEF_ANGLE_FIXED_R, AppPath + "\\" + PROFILE_DEF_NAME);
            lngColG = GetPrivateProfileInt(PROFILE_OBSPNT_COLOR_SEC, OBSPNT_COLOR_KEY_ANGLE_FIXED_G, (int)OBSPNT_COLOR_DEF_ANGLE_FIXED_G, AppPath + "\\" + PROFILE_DEF_NAME);
            lngColB = GetPrivateProfileInt(PROFILE_OBSPNT_COLOR_SEC, OBSPNT_COLOR_KEY_ANGLE_FIXED_B, (int)OBSPNT_COLOR_DEF_ANGLE_FIXED_B, AppPath + "\\" + PROFILE_DEF_NAME);
            //if (m_hPenObspntAngleFixed == 0) { m_hPenObspntAngleFixed = CreatePen((int)PS_SOLID, (int)Vector_Width_Emphasis, (uint)((lngColR << 16) | (lngColG << 8) | lngColB)); }
            if (m_hPenObspntAngleFixed == 0) { m_hPenObspntAngleFixed = (PS_SOLID << 56) | (Vector_Width_Emphasis << 32) | (lngColR << 16) | (lngColG << 8) | lngColB; }


            //'閉合候補観測点色。
            lngColR = GetPrivateProfileInt(PROFILE_OBSPNT_COLOR_SEC, OBSPNT_COLOR_KEY_CANDIDATE_R, (int)OBSPNT_COLOR_DEF_CANDIDATE_R, AppPath + "\\" + PROFILE_DEF_NAME);
            lngColG = GetPrivateProfileInt(PROFILE_OBSPNT_COLOR_SEC, OBSPNT_COLOR_KEY_CANDIDATE_G, (int)OBSPNT_COLOR_DEF_CANDIDATE_G, AppPath + "\\" + PROFILE_DEF_NAME);
            lngColB = GetPrivateProfileInt(PROFILE_OBSPNT_COLOR_SEC, OBSPNT_COLOR_KEY_CANDIDATE_B, (int)OBSPNT_COLOR_DEF_CANDIDATE_B, AppPath + "\\" + PROFILE_DEF_NAME);
            //if (m_hPenObspntCandidate == 0) { m_hPenObspntCandidate = CreatePen((int)PS_SOLID, (int)Vector_Width_Emphasis, (uint)((lngColR << 16) | (lngColG << 8) | lngColB)); }
            if (m_hPenObspntCandidate == 0) { m_hPenObspntCandidate = (PS_SOLID << 56) | (Vector_Width_Emphasis << 32) | (lngColR << 16) | (lngColG << 8) | lngColB; }


            //'閉合候補固定観測点色。
            lngColR = GetPrivateProfileInt(PROFILE_OBSPNT_COLOR_SEC, OBSPNT_COLOR_KEY_CANDIDATE_FIXED_R, (int)OBSPNT_COLOR_DEF_CANDIDATE_FIXED_R, AppPath + "\\" + PROFILE_DEF_NAME);
            lngColG = GetPrivateProfileInt(PROFILE_OBSPNT_COLOR_SEC, OBSPNT_COLOR_KEY_CANDIDATE_FIXED_G, (int)OBSPNT_COLOR_DEF_CANDIDATE_FIXED_G, AppPath + "\\" + PROFILE_DEF_NAME);
            lngColB = GetPrivateProfileInt(PROFILE_OBSPNT_COLOR_SEC, OBSPNT_COLOR_KEY_CANDIDATE_FIXED_B, (int)OBSPNT_COLOR_DEF_CANDIDATE_FIXED_B, AppPath + "\\" + PROFILE_DEF_NAME);
            //if (m_hPenObspntCandidateFixed == 0) { m_hPenObspntCandidateFixed = CreatePen((int)PS_SOLID, (int)Vector_Width_Emphasis, (uint)((lngColR << 16) | (lngColG << 8) | lngColB)); }
            if (m_hPenObspntCandidateFixed == 0) { m_hPenObspntCandidateFixed = (PS_SOLID << 56) | (Vector_Width_Emphasis << 32) | (lngColR << 16) | (lngColG << 8) | lngColB; }



            //'文字色の設定
            //'有効観測点記号色。
            lngColR = GetPrivateProfileInt(PROFILE_OBSPNT_COLOR_SEC, OBSPNT_COLOR_KEY_SYMBOL_ENABLE_R, (int)OBSPNT_COLOR_DEF_SYMBOL_ENABLE_R, AppPath + "\\" + PROFILE_DEF_NAME);
            lngColG = GetPrivateProfileInt(PROFILE_OBSPNT_COLOR_SEC, OBSPNT_COLOR_KEY_SYMBOL_ENABLE_G, (int)OBSPNT_COLOR_DEF_SYMBOL_ENABLE_G, AppPath + "\\" + PROFILE_DEF_NAME);
            lngColB = GetPrivateProfileInt(PROFILE_OBSPNT_COLOR_SEC, OBSPNT_COLOR_KEY_SYMBOL_ENABLE_B, (int)OBSPNT_COLOR_DEF_SYMBOL_ENABLE_B, AppPath + "\\" + PROFILE_DEF_NAME);
            //Obspnt_Color_Symbol_Enable = (uint)((lngColR << 16) | (lngColG << 8) | lngColB); //'有効観測点記号色。
            Obspnt_Color_Symbol_Enable = (PS_SOLID << 56) | (Vector_Width_Normal << 32) | (lngColR << 16) | (lngColG << 8) | lngColB;


            //'無効観測点記号色。
            lngColR = GetPrivateProfileInt(PROFILE_OBSPNT_COLOR_SEC, OBSPNT_COLOR_KEY_SYMBOL_DISABLE_R, (int)OBSPNT_COLOR_DEF_SYMBOL_DISABLE_R, AppPath + "\\" + PROFILE_DEF_NAME);
            lngColG = GetPrivateProfileInt(PROFILE_OBSPNT_COLOR_SEC, OBSPNT_COLOR_KEY_SYMBOL_DISABLE_G, (int)OBSPNT_COLOR_DEF_SYMBOL_DISABLE_G, AppPath + "\\" + PROFILE_DEF_NAME);
            lngColB = GetPrivateProfileInt(PROFILE_OBSPNT_COLOR_SEC, OBSPNT_COLOR_KEY_SYMBOL_DISABLE_B, (int)OBSPNT_COLOR_DEF_SYMBOL_DISABLE_B, AppPath + "\\" + PROFILE_DEF_NAME);
            //Obspnt_Color_Symbol_Disable = (uint)((lngColR << 16) | (lngColG << 8) | lngColB); //'無効観測点記号色。
            Obspnt_Color_Symbol_Disable = (PS_SOLID << 56) | (Vector_Width_Normal << 32) | (lngColR << 16) | (lngColG << 8) | lngColB;


            //'固定観測点記号色。
            lngColR = GetPrivateProfileInt(PROFILE_OBSPNT_COLOR_SEC, OBSPNT_COLOR_KEY_SYMBOL_FIXED_R, (int)OBSPNT_COLOR_DEF_SYMBOL_FIXED_R, AppPath + "\\" + PROFILE_DEF_NAME);
            lngColG = GetPrivateProfileInt(PROFILE_OBSPNT_COLOR_SEC, OBSPNT_COLOR_KEY_SYMBOL_FIXED_G, (int)OBSPNT_COLOR_DEF_SYMBOL_FIXED_G, AppPath + "\\" + PROFILE_DEF_NAME);
            lngColB = GetPrivateProfileInt(PROFILE_OBSPNT_COLOR_SEC, OBSPNT_COLOR_KEY_SYMBOL_FIXED_B, (int)OBSPNT_COLOR_DEF_SYMBOL_FIXED_B, AppPath + "\\" + PROFILE_DEF_NAME);
            //Obspnt_Color_Symbol_Fixed = (uint)((lngColR << 16) | (lngColG << 8) | lngColB); //'固定観測点記号色。
            Obspnt_Color_Symbol_Fixed = (PS_SOLID << 56) | (Vector_Width_Normal << 32) | (lngColR << 16) | (lngColG << 8) | lngColB;


            //'強調観測点記号色。
            lngColR = GetPrivateProfileInt(PROFILE_OBSPNT_COLOR_SEC, OBSPNT_COLOR_KEY_SYMBOL_EMPHASIS_R, (int)OBSPNT_COLOR_DEF_SYMBOL_EMPHASIS_R, AppPath + "\\" + PROFILE_DEF_NAME);
            lngColG = GetPrivateProfileInt(PROFILE_OBSPNT_COLOR_SEC, OBSPNT_COLOR_KEY_SYMBOL_EMPHASIS_G, (int)OBSPNT_COLOR_DEF_SYMBOL_EMPHASIS_G, AppPath + "\\" + PROFILE_DEF_NAME);
            lngColB = GetPrivateProfileInt(PROFILE_OBSPNT_COLOR_SEC, OBSPNT_COLOR_KEY_SYMBOL_EMPHASIS_B, (int)OBSPNT_COLOR_DEF_SYMBOL_EMPHASIS_B, AppPath + "\\" + PROFILE_DEF_NAME);
            //Obspnt_Color_Symbol_Emphasis = (uint)((lngColR << 16) | (lngColG << 8) | lngColB); //'強調観測点記号色。
            Obspnt_Color_Symbol_Emphasis = (PS_SOLID << 56) | (Vector_Width_Normal << 32) | (lngColR << 16) | (lngColG << 8) | lngColB;


            //'強調無効観測点記号色。
            lngColR = GetPrivateProfileInt(PROFILE_OBSPNT_COLOR_SEC, OBSPNT_COLOR_KEY_SYMBOL_EMPHASIS_DISABLE_R, (int)OBSPNT_COLOR_DEF_SYMBOL_EMPHASIS_DISABLE_R, AppPath + "\\" + PROFILE_DEF_NAME);
            lngColG = GetPrivateProfileInt(PROFILE_OBSPNT_COLOR_SEC, OBSPNT_COLOR_KEY_SYMBOL_EMPHASIS_DISABLE_G, (int)OBSPNT_COLOR_DEF_SYMBOL_EMPHASIS_DISABLE_G, AppPath + "\\" + PROFILE_DEF_NAME);
            lngColB = GetPrivateProfileInt(PROFILE_OBSPNT_COLOR_SEC, OBSPNT_COLOR_KEY_SYMBOL_EMPHASIS_DISABLE_B, (int)OBSPNT_COLOR_DEF_SYMBOL_EMPHASIS_DISABLE_B, AppPath + "\\" + PROFILE_DEF_NAME);
            //Obspnt_Color_Symbol_Emphasis_Disable = (uint)((lngColR << 16) | (lngColG << 8) | lngColB); //'強調無効観測点記号色。
            Obspnt_Color_Symbol_Emphasis_Disable = (PS_SOLID << 56) | (Vector_Width_Normal << 32) | (lngColR << 16) | (lngColG << 8) | lngColB;


            //'強調固定観測点記号色。
            lngColR = GetPrivateProfileInt(PROFILE_OBSPNT_COLOR_SEC, OBSPNT_COLOR_KEY_SYMBOL_EMPHASIS_FIXED_R, (int)OBSPNT_COLOR_DEF_SYMBOL_EMPHASIS_FIXED_R, AppPath + "\\" + PROFILE_DEF_NAME);
            lngColG = GetPrivateProfileInt(PROFILE_OBSPNT_COLOR_SEC, OBSPNT_COLOR_KEY_SYMBOL_EMPHASIS_FIXED_G, (int)OBSPNT_COLOR_DEF_SYMBOL_EMPHASIS_FIXED_G, AppPath + "\\" + PROFILE_DEF_NAME);
            lngColB = GetPrivateProfileInt(PROFILE_OBSPNT_COLOR_SEC, OBSPNT_COLOR_KEY_SYMBOL_EMPHASIS_FIXED_B, (int)OBSPNT_COLOR_DEF_SYMBOL_EMPHASIS_FIXED_B, AppPath + "\\" + PROFILE_DEF_NAME);
            //Obspnt_Color_Symbol_Emphasis_Fixed = (uint)((lngColR << 16) | (lngColG << 8) | lngColB); //'強調固定観測点記号色。
            Obspnt_Color_Symbol_Emphasis_Fixed = (PS_SOLID << 56) | (Vector_Width_Normal << 32) | (lngColR << 16) | (lngColG << 8) | lngColB;


            //'登録閉合観測点記号色。
            lngColR = GetPrivateProfileInt(PROFILE_OBSPNT_COLOR_SEC, OBSPNT_COLOR_KEY_SYMBOL_ANGLE_R, (int)OBSPNT_COLOR_DEF_SYMBOL_ANGLE_R, AppPath + "\\" + PROFILE_DEF_NAME);
            lngColG = GetPrivateProfileInt(PROFILE_OBSPNT_COLOR_SEC, OBSPNT_COLOR_KEY_SYMBOL_ANGLE_G, (int)OBSPNT_COLOR_DEF_SYMBOL_ANGLE_G, AppPath + "\\" + PROFILE_DEF_NAME);
            lngColB = GetPrivateProfileInt(PROFILE_OBSPNT_COLOR_SEC, OBSPNT_COLOR_KEY_SYMBOL_ANGLE_B, (int)OBSPNT_COLOR_DEF_SYMBOL_ANGLE_B, AppPath + "\\" + PROFILE_DEF_NAME);
            //Obspnt_Color_Symbol_Angle = (uint)((lngColR << 16) | (lngColG << 8) | lngColB); //'登録閉合観測点記号色。
            Obspnt_Color_Symbol_Angle = (PS_SOLID << 56) | (Vector_Width_Normal << 32) | (lngColR << 16) | (lngColG << 8) | lngColB;


            //'登録閉合固定観測点記号色。
            lngColR = GetPrivateProfileInt(PROFILE_OBSPNT_COLOR_SEC, OBSPNT_COLOR_KEY_SYMBOL_ANGLE_FIXED_R, (int)OBSPNT_COLOR_DEF_SYMBOL_ANGLE_FIXED_R, AppPath + "\\" + PROFILE_DEF_NAME);
            lngColG = GetPrivateProfileInt(PROFILE_OBSPNT_COLOR_SEC, OBSPNT_COLOR_KEY_SYMBOL_ANGLE_FIXED_G, (int)OBSPNT_COLOR_DEF_SYMBOL_ANGLE_FIXED_G, AppPath + "\\" + PROFILE_DEF_NAME);
            lngColB = GetPrivateProfileInt(PROFILE_OBSPNT_COLOR_SEC, OBSPNT_COLOR_KEY_SYMBOL_ANGLE_FIXED_B, (int)OBSPNT_COLOR_DEF_SYMBOL_ANGLE_FIXED_B, AppPath + "\\" + PROFILE_DEF_NAME);
            //Obspnt_Color_Symbol_Angle_Fixed = (uint)((lngColR << 16) | (lngColG << 8) | lngColB); //'登録閉合固定観測点記号色。
            Obspnt_Color_Symbol_Angle_Fixed = (PS_SOLID << 56) | (Vector_Width_Normal << 32) | (lngColR << 16) | (lngColG << 8) | lngColB;


            //'閉合候補観測点記号色。
            lngColR = GetPrivateProfileInt(PROFILE_OBSPNT_COLOR_SEC, OBSPNT_COLOR_KEY_SYMBOL_CANDIDATE_R, (int)OBSPNT_COLOR_DEF_SYMBOL_CANDIDATE_R, AppPath + "\\" + PROFILE_DEF_NAME);
            lngColG = GetPrivateProfileInt(PROFILE_OBSPNT_COLOR_SEC, OBSPNT_COLOR_KEY_SYMBOL_CANDIDATE_G, (int)OBSPNT_COLOR_DEF_SYMBOL_CANDIDATE_G, AppPath + "\\" + PROFILE_DEF_NAME);
            lngColB = GetPrivateProfileInt(PROFILE_OBSPNT_COLOR_SEC, OBSPNT_COLOR_KEY_SYMBOL_CANDIDATE_B, (int)OBSPNT_COLOR_DEF_SYMBOL_CANDIDATE_B, AppPath + "\\" + PROFILE_DEF_NAME);
            //Obspnt_Color_Symbol_Candidate = (uint)((lngColR << 16) | (lngColG << 8) | lngColB); //'閉合候補観測点記号色。
#if false   //坂井様へエラーがでます
            Obspnt_Color_Symbol_Candidate = (PS_SOLID << 56) | (Vector_Width_Normal << 32) | (lngColR << 16) | (lngColG << 8) | lngColB;
#endif

            //'閉合候補固定観測点記号色。
            lngColR = GetPrivateProfileInt(PROFILE_OBSPNT_COLOR_SEC, OBSPNT_COLOR_KEY_SYMBOL_CANDIDATE_FIXED_R, (int)OBSPNT_COLOR_DEF_SYMBOL_CANDIDATE_FIXED_R, AppPath + "\\" + PROFILE_DEF_NAME);
            lngColG = GetPrivateProfileInt(PROFILE_OBSPNT_COLOR_SEC, OBSPNT_COLOR_KEY_SYMBOL_CANDIDATE_FIXED_G, (int)OBSPNT_COLOR_DEF_SYMBOL_CANDIDATE_FIXED_G, AppPath + "\\" + PROFILE_DEF_NAME);
            lngColB = GetPrivateProfileInt(PROFILE_OBSPNT_COLOR_SEC, OBSPNT_COLOR_KEY_SYMBOL_CANDIDATE_FIXED_B, (int)OBSPNT_COLOR_DEF_SYMBOL_CANDIDATE_FIXED_B, AppPath + "\\" + PROFILE_DEF_NAME);
            //Obspnt_Color_Symbol_Candidate_Fixed = (uint)((lngColR << 16) | (lngColG << 8) | lngColB); //'閉合候補固定観測点記号色。
#if false   //坂井様へエラーがでます
            Obspnt_Color_Symbol_Candidate_Fixed = (PS_SOLID << 56) | (Vector_Width_Normal << 32) | (lngColR << 16) | (lngColG << 8) | lngColB;
#endif


            //'ベクトル色の設定
            //'未解析基線ベクトル色。
            lngColR = GetPrivateProfileInt(PROFILE_VECTOR_COLOR_SEC, VECTOR_COLOR_KEY_ENABLE_R, (int)VECTOR_COLOR_DEF_ENABLE_R, AppPath + "\\" + PROFILE_DEF_NAME);
            lngColG = GetPrivateProfileInt(PROFILE_VECTOR_COLOR_SEC, VECTOR_COLOR_KEY_ENABLE_G, (int)VECTOR_COLOR_DEF_ENABLE_G, AppPath + "\\" + PROFILE_DEF_NAME);
            lngColB = GetPrivateProfileInt(PROFILE_VECTOR_COLOR_SEC, VECTOR_COLOR_KEY_ENABLE_B, (int)VECTOR_COLOR_DEF_ENABLE_B, AppPath + "\\" + PROFILE_DEF_NAME);
            //if (m_hPenVectorEnable == 0) { m_hPenVectorEnable = CreatePen((int)PS_SOLID, (int)Vector_Width_Normal, (uint)((lngColR << 16) | (lngColG << 8) | lngColB)); }
            if (m_hPenVectorEnable == 0) { m_hPenVectorEnable = (PS_SOLID << 56) | (Vector_Width_Normal << 32) | (lngColR << 16) | (lngColG << 8) | lngColB; }


            //'無効基線ベクトル色。
            lngColR = GetPrivateProfileInt(PROFILE_VECTOR_COLOR_SEC, VECTOR_COLOR_KEY_DISABLE_R, (int)VECTOR_COLOR_DEF_DISABLE_R, AppPath + "\\" + PROFILE_DEF_NAME);
            lngColG = GetPrivateProfileInt(PROFILE_VECTOR_COLOR_SEC, VECTOR_COLOR_KEY_DISABLE_G, (int)VECTOR_COLOR_DEF_DISABLE_G, AppPath + "\\" + PROFILE_DEF_NAME);
            lngColB = GetPrivateProfileInt(PROFILE_VECTOR_COLOR_SEC, VECTOR_COLOR_KEY_DISABLE_B, (int)VECTOR_COLOR_DEF_DISABLE_B, AppPath + "\\" + PROFILE_DEF_NAME);
            //if (m_hPenVectorDisable == 0) { m_hPenVectorDisable = CreatePen((int)PS_SOLID, (int)Vector_Width_Normal, (uint)((lngColR << 16) | (lngColG << 8) | lngColB)); }
            if (m_hPenVectorDisable == 0) { m_hPenVectorDisable = (PS_SOLID << 56) | (Vector_Width_Normal << 32) | (lngColR << 16) | (lngColG << 8) | lngColB; }


            //'FLOAT基線ベクトル色。
            lngColR = GetPrivateProfileInt(PROFILE_VECTOR_COLOR_SEC, VECTOR_COLOR_KEY_FLOAT_R, (int)VECTOR_COLOR_DEF_FLOAT_R, AppPath + "\\" + PROFILE_DEF_NAME);
            lngColG = GetPrivateProfileInt(PROFILE_VECTOR_COLOR_SEC, VECTOR_COLOR_KEY_FLOAT_G, (int)VECTOR_COLOR_DEF_FLOAT_G, AppPath + "\\" + PROFILE_DEF_NAME);
            lngColB = GetPrivateProfileInt(PROFILE_VECTOR_COLOR_SEC, VECTOR_COLOR_KEY_FLOAT_B, (int)VECTOR_COLOR_DEF_FLOAT_B, AppPath + "\\" + PROFILE_DEF_NAME);
            //if (m_hPenVectorFloat == 0) { m_hPenVectorFloat = CreatePen((int)PS_SOLID, (int)Vector_Width_Normal, (uint)((lngColR << 16) | (lngColG << 8) | lngColB)); }
            if (m_hPenVectorFloat == 0) { m_hPenVectorFloat = (PS_SOLID << 56) | (Vector_Width_Normal << 32) | (lngColR << 16) | (lngColG << 8) | lngColB; }


            //'FIX基線ベクトル色。
            lngColR = GetPrivateProfileInt(PROFILE_VECTOR_COLOR_SEC, VECTOR_COLOR_KEY_FIX_R, (int)VECTOR_COLOR_DEF_FIX_R, AppPath + "\\" + PROFILE_DEF_NAME);
            lngColG = GetPrivateProfileInt(PROFILE_VECTOR_COLOR_SEC, VECTOR_COLOR_KEY_FIX_G, (int)VECTOR_COLOR_DEF_FIX_G, AppPath + "\\" + PROFILE_DEF_NAME);
            lngColB = GetPrivateProfileInt(PROFILE_VECTOR_COLOR_SEC, VECTOR_COLOR_KEY_FIX_B, (int)VECTOR_COLOR_DEF_FIX_B, AppPath + "\\" + PROFILE_DEF_NAME);
            //if (m_hPenVectorFix == 0) { m_hPenVectorFix = CreatePen((int)PS_SOLID, (int)Vector_Width_Normal, (uint)((lngColR << 16) | (lngColG << 8) | lngColB)); }
            if (m_hPenVectorFix == 0) { m_hPenVectorFix = (PS_SOLID << 56) | (Vector_Width_Normal << 32) | (lngColR << 16) | (lngColG << 8) | lngColB; }


            //'FIX(バイアス決定比<最小バイアス決定比)基線ベクトル色。2006/10/12 NGS Yamada
            lngColR = GetPrivateProfileInt(PROFILE_VECTOR_COLOR_SEC, VECTOR_COLOR_KEY_FIX_LOWRATIO_R, (int)VECTOR_COLOR_DEF_FIX_LOWRATIO_R, AppPath + "\\" + PROFILE_DEF_NAME);
            lngColG = GetPrivateProfileInt(PROFILE_VECTOR_COLOR_SEC, VECTOR_COLOR_KEY_FIX_LOWRATIO_G, (int)VECTOR_COLOR_DEF_FIX_LOWRATIO_G, AppPath + "\\" + PROFILE_DEF_NAME);
            lngColB = GetPrivateProfileInt(PROFILE_VECTOR_COLOR_SEC, VECTOR_COLOR_KEY_FIX_LOWRATIO_B, (int)VECTOR_COLOR_DEF_FIX_LOWRATIO_B, AppPath + "\\" + PROFILE_DEF_NAME);
            //if (m_hPenVectorFix_LowRatio == 0) { m_hPenVectorFix_LowRatio = CreatePen((int)PS_SOLID, (int)Vector_Width_Normal, (uint)((lngColR << 16) | (lngColG << 8) | lngColB)); }
            if (m_hPenVectorFix_LowRatio == 0) { m_hPenVectorFix_LowRatio = (PS_SOLID << 56) | (Vector_Width_Normal << 32) | (lngColR << 16) | (lngColG << 8) | lngColB; }


            //'解析失敗基線ベクトル色。
            lngColR = GetPrivateProfileInt(PROFILE_VECTOR_COLOR_SEC, VECTOR_COLOR_KEY_FAILED_R, (int)VECTOR_COLOR_DEF_FAILED_R, AppPath + "\\" + PROFILE_DEF_NAME);
            lngColG = GetPrivateProfileInt(PROFILE_VECTOR_COLOR_SEC, VECTOR_COLOR_KEY_FAILED_G, (int)VECTOR_COLOR_DEF_FAILED_G, AppPath + "\\" + PROFILE_DEF_NAME);
            lngColB = GetPrivateProfileInt(PROFILE_VECTOR_COLOR_SEC, VECTOR_COLOR_KEY_FAILED_B, (int)VECTOR_COLOR_DEF_FAILED_B, AppPath + "\\" + PROFILE_DEF_NAME);
            //if (m_hPenVectorFailed == 0) { m_hPenVectorFailed = CreatePen((int)PS_SOLID, (int)Vector_Width_Normal, (uint)((lngColR << 16) | (lngColG << 8) | lngColB)); }
            if (m_hPenVectorFailed == 0) { m_hPenVectorFailed = (PS_SOLID << 56) | (Vector_Width_Normal << 32) | (lngColR << 16) | (lngColG << 8) | lngColB; }


            //'登録閉合基線ベクトル色。
            lngColR = GetPrivateProfileInt(PROFILE_VECTOR_COLOR_SEC, VECTOR_COLOR_KEY_ANGLE_R, (int)VECTOR_COLOR_DEF_ANGLE_R, AppPath + "\\" + PROFILE_DEF_NAME);
            lngColG = GetPrivateProfileInt(PROFILE_VECTOR_COLOR_SEC, VECTOR_COLOR_KEY_ANGLE_G, (int)VECTOR_COLOR_DEF_ANGLE_G, AppPath + "\\" + PROFILE_DEF_NAME);
            lngColB = GetPrivateProfileInt(PROFILE_VECTOR_COLOR_SEC, VECTOR_COLOR_KEY_ANGLE_B, (int)VECTOR_COLOR_DEF_ANGLE_B, AppPath + "\\" + PROFILE_DEF_NAME);
            //if (m_hPenVectorAngle == 0) { m_hPenVectorAngle = CreatePen((int)PS_SOLID, (int)Vector_Width_Emphasis, (uint)((lngColR << 16) | (lngColG << 8) | lngColB)); }
            if (m_hPenVectorAngle == 0) { m_hPenVectorAngle = (PS_SOLID << 56) | (Vector_Width_Emphasis << 32) | (lngColR << 16) | (lngColG << 8) | lngColB; }


            //'閉合候補基線ベクトル色。
            lngColR = GetPrivateProfileInt(PROFILE_VECTOR_COLOR_SEC, VECTOR_COLOR_KEY_CANDIDATE_R, (int)VECTOR_COLOR_DEF_CANDIDATE_R, AppPath + "\\" + PROFILE_DEF_NAME);
            lngColG = GetPrivateProfileInt(PROFILE_VECTOR_COLOR_SEC, VECTOR_COLOR_KEY_CANDIDATE_G, (int)VECTOR_COLOR_DEF_CANDIDATE_G, AppPath + "\\" + PROFILE_DEF_NAME);
            lngColB = GetPrivateProfileInt(PROFILE_VECTOR_COLOR_SEC, VECTOR_COLOR_KEY_CANDIDATE_B, (int)VECTOR_COLOR_DEF_CANDIDATE_B, AppPath + "\\" + PROFILE_DEF_NAME);
            //if (m_hPenVectorCandidate == 0) { m_hPenVectorCandidate = CreatePen((int)PS_SOLID, (int)Vector_Width_Emphasis, (uint)((lngColR << 16) | (lngColG << 8) | lngColB)); }
            if (m_hPenVectorCandidate == 0) { m_hPenVectorCandidate = (PS_SOLID << 56) | (Vector_Width_Emphasis << 32) | (lngColR << 16) | (lngColG << 8) | lngColB; }


            //'偏心補正基線ベクトル色。
            lngColR = GetPrivateProfileInt(PROFILE_VECTOR_COLOR_SEC, VECTOR_COLOR_KEY_CORRECT_R, (int)VECTOR_COLOR_DEF_CORRECT_R, AppPath + "\\" + PROFILE_DEF_NAME);
            lngColG = GetPrivateProfileInt(PROFILE_VECTOR_COLOR_SEC, VECTOR_COLOR_KEY_CORRECT_G, (int)VECTOR_COLOR_DEF_CORRECT_G, AppPath + "\\" + PROFILE_DEF_NAME);
            lngColB = GetPrivateProfileInt(PROFILE_VECTOR_COLOR_SEC, VECTOR_COLOR_KEY_CORRECT_B, (int)VECTOR_COLOR_DEF_CORRECT_B, AppPath + "\\" + PROFILE_DEF_NAME);
            //if (m_hPenVectorCorrect == 0) { m_hPenVectorCorrect = CreatePen((int)PS_SOLID, (int)Vector_Width_Normal, (uint)((lngColR << 16) | (lngColG << 8) | lngColB)); }
            if (m_hPenVectorCorrect == 0) { m_hPenVectorCorrect = (PS_SOLID << 56) | (Vector_Width_Normal << 32) | (lngColR << 16) | (lngColG << 8) | lngColB; }


            //'偏心ベクトル色。
            lngColR = GetPrivateProfileInt(PROFILE_VECTOR_COLOR_SEC, VECTOR_COLOR_KEY_ECCENTRIC_R, (int)VECTOR_COLOR_DEF_ECCENTRIC_R, AppPath + "\\" + PROFILE_DEF_NAME);
            lngColG = GetPrivateProfileInt(PROFILE_VECTOR_COLOR_SEC, VECTOR_COLOR_KEY_ECCENTRIC_G, (int)VECTOR_COLOR_DEF_ECCENTRIC_G, AppPath + "\\" + PROFILE_DEF_NAME);
            lngColB = GetPrivateProfileInt(PROFILE_VECTOR_COLOR_SEC, VECTOR_COLOR_KEY_ECCENTRIC_B, (int)VECTOR_COLOR_DEF_ECCENTRIC_B, AppPath + "\\" + PROFILE_DEF_NAME);
            //if (m_hPenVectorEccentric == 0) { m_hPenVectorEccentric = CreatePen((int)PS_SOLID, (int)Vector_Width_Normal, (uint)((lngColR << 16) | (lngColG << 8) | lngColB)); }
            if (m_hPenVectorEccentric == 0) { m_hPenVectorEccentric = (PS_SOLID << 56) | (Vector_Width_Normal << 32) | (lngColR << 16) | (lngColG << 8) | lngColB; }


            //'強調未解析基線ベクトル色。
            lngColR = GetPrivateProfileInt(PROFILE_VECTOR_COLOR_SEC, VECTOR_COLOR_KEY_EMPHASIS_R, (int)VECTOR_COLOR_DEF_EMPHASIS_R, AppPath + "\\" + PROFILE_DEF_NAME);
            lngColG = GetPrivateProfileInt(PROFILE_VECTOR_COLOR_SEC, VECTOR_COLOR_KEY_EMPHASIS_G, (int)VECTOR_COLOR_DEF_EMPHASIS_G, AppPath + "\\" + PROFILE_DEF_NAME);
            lngColB = GetPrivateProfileInt(PROFILE_VECTOR_COLOR_SEC, VECTOR_COLOR_KEY_EMPHASIS_B, (int)VECTOR_COLOR_DEF_EMPHASIS_B, AppPath + "\\" + PROFILE_DEF_NAME);
            //if (m_hPenVectorEmphasis == 0) { m_hPenVectorEmphasis = CreatePen((int)PS_SOLID, (int)Vector_Width_Emphasis, (uint)((lngColR << 16) | (lngColG << 8) | lngColB)); }
            if (m_hPenVectorEmphasis == 0) { m_hPenVectorEmphasis = (PS_SOLID << 56) | (Vector_Width_Emphasis << 32) | (lngColR << 16) | (lngColG << 8) | lngColB; }


            //'強調無効基線ベクトル色。
            lngColR = GetPrivateProfileInt(PROFILE_VECTOR_COLOR_SEC, VECTOR_COLOR_KEY_EMPHASIS_DISABLE_R, (int)VECTOR_COLOR_DEF_EMPHASIS_DISABLE_R, AppPath + "\\" + PROFILE_DEF_NAME);
            lngColG = GetPrivateProfileInt(PROFILE_VECTOR_COLOR_SEC, VECTOR_COLOR_KEY_EMPHASIS_DISABLE_G, (int)VECTOR_COLOR_DEF_EMPHASIS_DISABLE_G, AppPath + "\\" + PROFILE_DEF_NAME);
            lngColB = GetPrivateProfileInt(PROFILE_VECTOR_COLOR_SEC, VECTOR_COLOR_KEY_EMPHASIS_DISABLE_B, (int)VECTOR_COLOR_DEF_EMPHASIS_DISABLE_B, AppPath + "\\" + PROFILE_DEF_NAME);
            //if (m_hPenVectorEmphasisDisable == 0) { m_hPenVectorEmphasisDisable = CreatePen((int)PS_SOLID, (int)Vector_Width_Emphasis, (uint)((lngColR << 16) | (lngColG << 8) | lngColB)); }
            if (m_hPenVectorEmphasisDisable == 0) { m_hPenVectorEmphasisDisable = (PS_SOLID << 56) | (Vector_Width_Emphasis << 32) | (lngColR << 16) | (lngColG << 8) | lngColB; }


            //'強調FLOAT基線ベクトル色。
            lngColR = GetPrivateProfileInt(PROFILE_VECTOR_COLOR_SEC, VECTOR_COLOR_KEY_EMPHASIS_FLOAT_R, (int)VECTOR_COLOR_DEF_EMPHASIS_FLOAT_R, AppPath + "\\" + PROFILE_DEF_NAME);
            lngColG = GetPrivateProfileInt(PROFILE_VECTOR_COLOR_SEC, VECTOR_COLOR_KEY_EMPHASIS_FLOAT_G, (int)VECTOR_COLOR_DEF_EMPHASIS_FLOAT_G, AppPath + "\\" + PROFILE_DEF_NAME);
            lngColB = GetPrivateProfileInt(PROFILE_VECTOR_COLOR_SEC, VECTOR_COLOR_KEY_EMPHASIS_FLOAT_B, (int)VECTOR_COLOR_DEF_EMPHASIS_FLOAT_B, AppPath + "\\" + PROFILE_DEF_NAME);
            //if (m_hPenVectorEmphasisFloat == 0) { m_hPenVectorEmphasisFloat = CreatePen((int)PS_SOLID, (int)Vector_Width_Emphasis, (uint)((lngColR << 16) | (lngColG << 8) | lngColB)); }
            if (m_hPenVectorEmphasisFloat == 0) { m_hPenVectorEmphasisFloat = (PS_SOLID << 56) | (Vector_Width_Emphasis << 32) | (lngColR << 16) | (lngColG << 8) | lngColB; }


            //'強調FIX基線ベクトル色。
            lngColR = GetPrivateProfileInt(PROFILE_VECTOR_COLOR_SEC, VECTOR_COLOR_KEY_EMPHASIS_FIX_R, (int)VECTOR_COLOR_DEF_EMPHASIS_FIX_R, AppPath + "\\" + PROFILE_DEF_NAME);
            lngColG = GetPrivateProfileInt(PROFILE_VECTOR_COLOR_SEC, VECTOR_COLOR_KEY_EMPHASIS_FIX_G, (int)VECTOR_COLOR_DEF_EMPHASIS_FIX_G, AppPath + "\\" + PROFILE_DEF_NAME);
            lngColB = GetPrivateProfileInt(PROFILE_VECTOR_COLOR_SEC, VECTOR_COLOR_KEY_EMPHASIS_FIX_B, (int)VECTOR_COLOR_DEF_EMPHASIS_FIX_B, AppPath + "\\" + PROFILE_DEF_NAME);
            //if (m_hPenVectorEmphasisFix == 0) { m_hPenVectorEmphasisFix = CreatePen((int)PS_SOLID, (int)Vector_Width_Emphasis, (uint)((lngColR << 16) | (lngColG << 8) | lngColB)); }
            if (m_hPenVectorEmphasisFix == 0) { m_hPenVectorEmphasisFix = (PS_SOLID << 56) | (Vector_Width_Emphasis << 32) | (lngColR << 16) | (lngColG << 8) | lngColB; }


            //'強調FIX(バイアス決定比<最小バイアス決定比)基線ベクトル。　2006/10/12 NGS Yamada
            lngColR = GetPrivateProfileInt(PROFILE_VECTOR_COLOR_SEC, VECTOR_COLOR_KEY_EMPHASIS_FIX_LOWRATIO_R, (int)VECTOR_COLOR_DEF_EMPHASIS_FIX_LOWRATIO_R, AppPath + "\\" + PROFILE_DEF_NAME);
            lngColG = GetPrivateProfileInt(PROFILE_VECTOR_COLOR_SEC, VECTOR_COLOR_KEY_EMPHASIS_FIX_LOWRATIO_G, (int)VECTOR_COLOR_DEF_EMPHASIS_FIX_LOWRATIO_G, AppPath + "\\" + PROFILE_DEF_NAME);
            lngColB = GetPrivateProfileInt(PROFILE_VECTOR_COLOR_SEC, VECTOR_COLOR_KEY_EMPHASIS_FIX_LOWRATIO_B, (int)VECTOR_COLOR_DEF_EMPHASIS_FIX_LOWRATIO_B, AppPath + "\\" + PROFILE_DEF_NAME);
            //if (m_hPenVectorEmphasisFix_LowRatio == 0) { m_hPenVectorEmphasisFix_LowRatio = CreatePen((int)PS_SOLID, (int)Vector_Width_Emphasis, (uint)((lngColR << 16) | (lngColG << 8) | lngColB)); }
            if (m_hPenVectorEmphasisFix_LowRatio == 0) { m_hPenVectorEmphasisFix_LowRatio = (PS_SOLID << 56) | (Vector_Width_Emphasis << 32) | (lngColR << 16) | (lngColG << 8) | lngColB; }

            //'強調解析失敗基線ベクトル色。
            lngColR = GetPrivateProfileInt(PROFILE_VECTOR_COLOR_SEC, VECTOR_COLOR_KEY_EMPHASIS_FAILED_R, (int)VECTOR_COLOR_DEF_EMPHASIS_FAILED_R, AppPath + "\\" + PROFILE_DEF_NAME);
            lngColG = GetPrivateProfileInt(PROFILE_VECTOR_COLOR_SEC, VECTOR_COLOR_KEY_EMPHASIS_FAILED_G, (int)VECTOR_COLOR_DEF_EMPHASIS_FAILED_G, AppPath + "\\" + PROFILE_DEF_NAME);
            lngColB = GetPrivateProfileInt(PROFILE_VECTOR_COLOR_SEC, VECTOR_COLOR_KEY_EMPHASIS_FAILED_B, (int)VECTOR_COLOR_DEF_EMPHASIS_FAILED_B, AppPath + "\\" + PROFILE_DEF_NAME);
            //if (m_hPenVectorEmphasisFailed == 0) { m_hPenVectorEmphasisFailed = CreatePen((int)PS_SOLID, (int)Vector_Width_Emphasis, (uint)((lngColR << 16) | (lngColG << 8) | lngColB)); }
            if (m_hPenVectorEmphasisFailed == 0) { m_hPenVectorEmphasisFailed = (PS_SOLID << 56) | (Vector_Width_Emphasis << 32) | (lngColR << 16) | (lngColG << 8) | lngColB; }


            //'強調閉合候補基線ベクトル色。
            lngColR = GetPrivateProfileInt(PROFILE_VECTOR_COLOR_SEC, VECTOR_COLOR_KEY_EMPHASIS_CANDIDATE_R, (int)VECTOR_COLOR_DEF_EMPHASIS_CANDIDATE_R, AppPath + "\\" + PROFILE_DEF_NAME);
            lngColG = GetPrivateProfileInt(PROFILE_VECTOR_COLOR_SEC, VECTOR_COLOR_KEY_EMPHASIS_CANDIDATE_G, (int)VECTOR_COLOR_DEF_EMPHASIS_CANDIDATE_G, AppPath + "\\" + PROFILE_DEF_NAME);
            lngColB = GetPrivateProfileInt(PROFILE_VECTOR_COLOR_SEC, VECTOR_COLOR_KEY_EMPHASIS_CANDIDATE_B, (int)VECTOR_COLOR_DEF_EMPHASIS_CANDIDATE_B, AppPath + "\\" + PROFILE_DEF_NAME);
            //if (m_hPenVectorEmphasisCandidate == 0) { m_hPenVectorEmphasisCandidate = CreatePen((int)PS_SOLID, (int)Vector_Width_Emphasis, (uint)((lngColR << 16) | (lngColG << 8) | lngColB)); }
            if (m_hPenVectorEmphasisCandidate == 0) { m_hPenVectorEmphasisCandidate = (PS_SOLID << 56) | (Vector_Width_Emphasis << 32) | (lngColR << 16) | (lngColG << 8) | lngColB; }


            //'強調偏心補正基線ベクトル色。
            lngColR = GetPrivateProfileInt(PROFILE_VECTOR_COLOR_SEC, VECTOR_COLOR_KEY_CORRECT_R, (int)VECTOR_COLOR_DEF_CORRECT_R, AppPath + "\\" + PROFILE_DEF_NAME);
            lngColG = GetPrivateProfileInt(PROFILE_VECTOR_COLOR_SEC, VECTOR_COLOR_KEY_CORRECT_G, (int)VECTOR_COLOR_DEF_CORRECT_G, AppPath + "\\" + PROFILE_DEF_NAME);
            lngColB = GetPrivateProfileInt(PROFILE_VECTOR_COLOR_SEC, VECTOR_COLOR_KEY_CORRECT_B, (int)VECTOR_COLOR_DEF_CORRECT_B, AppPath + "\\" + PROFILE_DEF_NAME);
            //if (m_hPenVectorEmphasisCorrect == 0) { m_hPenVectorEmphasisCorrect = CreatePen((int)PS_SOLID, (int)Vector_Width_Emphasis, (uint)((lngColR << 16) | (lngColG << 8) | lngColB)); }
            if (m_hPenVectorEmphasisCorrect == 0) { m_hPenVectorEmphasisCorrect = (PS_SOLID << 56) | (Vector_Width_Emphasis << 32) | (lngColR << 16) | (lngColG << 8) | lngColB; }


            if (m_nPlotType == PLOTTYPE_ENUM.PLOTTYPE_STANDARD || m_nPlotType == PLOTTYPE_ENUM.PLOTTYPE_ANGLE)
            {
                lngColR = GetPrivateProfileInt(PROFILE_VECTOR_COLOR_SEC, VECTOR_COLOR_KEY_ENABLE_R, (int)VECTOR_COLOR_DEF_ENABLE_R, AppPath + "\\" + PROFILE_DEF_NAME);
                lngColG = GetPrivateProfileInt(PROFILE_VECTOR_COLOR_SEC, VECTOR_COLOR_KEY_ENABLE_G, (int)VECTOR_COLOR_DEF_ENABLE_G, AppPath + "\\" + PROFILE_DEF_NAME);
                lngColB = GetPrivateProfileInt(PROFILE_VECTOR_COLOR_SEC, VECTOR_COLOR_KEY_ENABLE_B, (int)VECTOR_COLOR_DEF_ENABLE_B, AppPath + "\\" + PROFILE_DEF_NAME);
                //if (m_hPenVectorDuplication == 0) { m_hPenVectorDuplication = CreatePen((int)PS_SOLID, (int)Vector_Width_Duplication, (uint)((lngColR << 16) | (lngColG << 8) | lngColB)); }
                if (m_hPenVectorDuplication == 0) { m_hPenVectorDuplication = (PS_SOLID << 56) | (Vector_Width_Duplication << 32) | (lngColR << 16) | (lngColG << 8) | lngColB; }


                lngColR = GetPrivateProfileInt(PROFILE_VECTOR_COLOR_SEC, VECTOR_COLOR_KEY_FLOAT_R, (int)VECTOR_COLOR_DEF_FLOAT_R, AppPath + "\\" + PROFILE_DEF_NAME);
                lngColG = GetPrivateProfileInt(PROFILE_VECTOR_COLOR_SEC, VECTOR_COLOR_KEY_FLOAT_G, (int)VECTOR_COLOR_DEF_FLOAT_G, AppPath + "\\" + PROFILE_DEF_NAME);
                lngColB = GetPrivateProfileInt(PROFILE_VECTOR_COLOR_SEC, VECTOR_COLOR_KEY_FLOAT_B, (int)VECTOR_COLOR_DEF_FLOAT_B, AppPath + "\\" + PROFILE_DEF_NAME);
                //if (m_hPenVectorDuplicationFloat == 0) { m_hPenVectorDuplicationFloat = CreatePen((int)PS_SOLID, (int)Vector_Width_Duplication, (uint)((lngColR << 16) | (lngColG << 8) | lngColB)); }
                if (m_hPenVectorDuplicationFloat == 0) { m_hPenVectorDuplicationFloat = (PS_SOLID << 56) | (Vector_Width_Duplication << 32) | (lngColR << 16) | (lngColG << 8) | lngColB; }


                lngColR = GetPrivateProfileInt(PROFILE_VECTOR_COLOR_SEC, VECTOR_COLOR_KEY_FIX_R, (int)VECTOR_COLOR_DEF_FIX_R, AppPath + "\\" + PROFILE_DEF_NAME);
                lngColG = GetPrivateProfileInt(PROFILE_VECTOR_COLOR_SEC, VECTOR_COLOR_KEY_FIX_G, (int)VECTOR_COLOR_DEF_FIX_G, AppPath + "\\" + PROFILE_DEF_NAME);
                lngColB = GetPrivateProfileInt(PROFILE_VECTOR_COLOR_SEC, VECTOR_COLOR_KEY_FIX_B, (int)VECTOR_COLOR_DEF_FIX_B, AppPath + "\\" + PROFILE_DEF_NAME);
                //if (m_hPenVectorDuplicationFix == 0) { m_hPenVectorDuplicationFix = CreatePen((int)PS_SOLID, (int)Vector_Width_Duplication, (uint)((lngColR << 16) | (lngColG << 8) | lngColB)); }
                if (m_hPenVectorDuplicationFix == 0) { m_hPenVectorDuplicationFix = (PS_SOLID << 56) | (Vector_Width_Duplication << 32) | (lngColR << 16) | (lngColG << 8) | lngColB; }


                lngColR = GetPrivateProfileInt(PROFILE_VECTOR_COLOR_SEC, VECTOR_COLOR_KEY_FAILED_R, (int)VECTOR_COLOR_DEF_FAILED_R, AppPath + "\\" + PROFILE_DEF_NAME);
                lngColG = GetPrivateProfileInt(PROFILE_VECTOR_COLOR_SEC, VECTOR_COLOR_KEY_FAILED_G, (int)VECTOR_COLOR_DEF_FAILED_G, AppPath + "\\" + PROFILE_DEF_NAME);
                lngColB = GetPrivateProfileInt(PROFILE_VECTOR_COLOR_SEC, VECTOR_COLOR_KEY_FAILED_B, (int)VECTOR_COLOR_DEF_FAILED_B, AppPath + "\\" + PROFILE_DEF_NAME);
                //if (m_hPenVectorDuplicationFailed == 0) { m_hPenVectorDuplicationFailed = CreatePen((int)PS_SOLID, (int)Vector_Width_Duplication, (uint)((lngColR << 16) | (lngColG << 8) | lngColB)); }
                if (m_hPenVectorDuplicationFailed == 0) { m_hPenVectorDuplicationFailed = (PS_SOLID << 56) | (Vector_Width_Duplication << 32) | (lngColR << 16) | (lngColG << 8) | lngColB; }
            }
            else
            {
                long nLogPixelSX;
                nLogPixelSX = GetDeviceCaps((IntPtr)hDC, (int)LOGPIXELSX);


                lngColR = GetPrivateProfileInt(PROFILE_VECTOR_COLOR_SEC, VECTOR_COLOR_KEY_ENABLE_R, (int)VECTOR_COLOR_DEF_ENABLE_R, AppPath + "\\" + PROFILE_DEF_NAME);
                lngColG = GetPrivateProfileInt(PROFILE_VECTOR_COLOR_SEC, VECTOR_COLOR_KEY_ENABLE_G, (int)VECTOR_COLOR_DEF_ENABLE_G, AppPath + "\\" + PROFILE_DEF_NAME);
                lngColB = GetPrivateProfileInt(PROFILE_VECTOR_COLOR_SEC, VECTOR_COLOR_KEY_ENABLE_B, (int)VECTOR_COLOR_DEF_ENABLE_B, AppPath + "\\" + PROFILE_DEF_NAME);
                //if (m_hPenVectorDuplication == 0) { m_hPenVectorDuplication = CreatePen((int)PS_SOLID, (int)(10 / 1440 * nLogPixelSX), (uint)((lngColR << 16) | (lngColG << 8) | lngColB)); }
                if (m_hPenVectorDuplication == 0) { m_hPenVectorDuplication = (PS_SOLID << 56) | ((10 / 1440 * nLogPixelSX) << 32) | (lngColR << 16) | (lngColG << 8) | lngColB; }


                lngColR = GetPrivateProfileInt(PROFILE_VECTOR_COLOR_SEC, VECTOR_COLOR_KEY_FLOAT_R, (int)VECTOR_COLOR_DEF_FLOAT_R, AppPath + "\\" + PROFILE_DEF_NAME);
                lngColG = GetPrivateProfileInt(PROFILE_VECTOR_COLOR_SEC, VECTOR_COLOR_KEY_FLOAT_G, (int)VECTOR_COLOR_DEF_FLOAT_G, AppPath + "\\" + PROFILE_DEF_NAME);
                lngColB = GetPrivateProfileInt(PROFILE_VECTOR_COLOR_SEC, VECTOR_COLOR_KEY_FLOAT_B, (int)VECTOR_COLOR_DEF_FLOAT_B, AppPath + "\\" + PROFILE_DEF_NAME);
                //if (m_hPenVectorDuplicationFloat == 0) { m_hPenVectorDuplicationFloat = CreatePen((int)PS_SOLID, (int)(10 / 1440 * nLogPixelSX), (uint)((lngColR << 16) | (lngColG << 8) | lngColB)); }
                if (m_hPenVectorDuplicationFloat == 0) { m_hPenVectorDuplicationFloat = (PS_SOLID << 56) | ((10 / 1440 * nLogPixelSX) << 32) | (lngColR << 16) | (lngColG << 8) | lngColB; }


                lngColR = GetPrivateProfileInt(PROFILE_VECTOR_COLOR_SEC, VECTOR_COLOR_KEY_FIX_R, (int)VECTOR_COLOR_DEF_FIX_R, AppPath + "\\" + PROFILE_DEF_NAME);
                lngColG = GetPrivateProfileInt(PROFILE_VECTOR_COLOR_SEC, VECTOR_COLOR_KEY_FIX_G, (int)VECTOR_COLOR_DEF_FIX_G, AppPath + "\\" + PROFILE_DEF_NAME);
                lngColB = GetPrivateProfileInt(PROFILE_VECTOR_COLOR_SEC, VECTOR_COLOR_KEY_FIX_B, (int)VECTOR_COLOR_DEF_FIX_B, AppPath + "\\" + PROFILE_DEF_NAME);
                //if (m_hPenVectorDuplicationFix == 0) { m_hPenVectorDuplicationFix = CreatePen((int)PS_SOLID, (int)(10 / 1440 * nLogPixelSX), (uint)((lngColR << 16) | (lngColG << 8) | lngColB)); }
                if (m_hPenVectorDuplicationFix == 0) { m_hPenVectorDuplicationFix = (PS_SOLID << 56) | ((10 / 1440 * nLogPixelSX) << 32) | (lngColR << 16) | (lngColG << 8) | lngColB; }


                lngColR = GetPrivateProfileInt(PROFILE_VECTOR_COLOR_SEC, VECTOR_COLOR_KEY_FAILED_R, (int)VECTOR_COLOR_DEF_FAILED_R, AppPath + "\\" + PROFILE_DEF_NAME);
                lngColG = GetPrivateProfileInt(PROFILE_VECTOR_COLOR_SEC, VECTOR_COLOR_KEY_FAILED_G, (int)VECTOR_COLOR_DEF_FAILED_G, AppPath + "\\" + PROFILE_DEF_NAME);
                lngColB = GetPrivateProfileInt(PROFILE_VECTOR_COLOR_SEC, VECTOR_COLOR_KEY_FAILED_B, (int)VECTOR_COLOR_DEF_FAILED_B, AppPath + "\\" + PROFILE_DEF_NAME);
                //if (m_hPenVectorDuplicationFailed == 0) { m_hPenVectorDuplicationFailed = CreatePen((int)PS_SOLID, (int)(10 / 1440 * nLogPixelSX), (uint)((lngColR << 16) | (lngColG << 8) | lngColB)); }
                if (m_hPenVectorDuplicationFailed == 0) { m_hPenVectorDuplicationFailed = (PS_SOLID << 56) | ((10 / 1440 * nLogPixelSX) << 32) | (lngColR << 16) | (lngColG << 8) | lngColB; }
            }
            return;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'オブジェクトの描画。
        '
        '強調オブジェクト等の変化の多い部分を描画する。
        'nClip～は記号を描画するためのクリップ領域。
        '記号はクリップ領域をはみ出さないように描画する。
        'clsRegistAngle が Nothing でない場合、閉合関係のオブジェクトも描画する。
        '
        '引き数：
        'hDC デバイスコンテキストのハンドル。
        'nDeviceLeft 論理エリア上のデバイス左辺位置(ピクセル)。
        'nDeviceTop 論理エリア上のデバイス上辺位置(ピクセル)。
        'nClipLeft 論理エリア上のクリップ領域左辺(ピクセル)。
        'nClipTop 論理エリア上のクリップ領域上辺(ピクセル)。
        'nClipWidth 論理エリア上のクリップ領域幅(ピクセル)。
        'nClipHeight 論理エリア上のクリップ領域高さ(ピクセル)。
        'nMapScale Twips→ピクセル、の係数。
        'clsEmphasisHead 強調オブジェクトリストのヘッダー。
        'clsRegistAngle RegistAngle オブジェクト。
        'bArrow 矢印描画の指定。True=描画する。False=描画しない。
        Public Sub DrawObjectFilm(ByVal hDC As Long, ByVal nDeviceLeft As Double, ByVal nDeviceTop As Double, ByVal nClipLeft As Double, ByVal nClipTop As Double, ByVal nClipWidth As Double, ByVal nClipHeight As Double, ByVal nMapScale As Double, ByVal clsEmphasisHead As ChainList, ByVal clsRegistAngle As RegistAngle, ByVal bArrow As Boolean)

            Dim clsChainList As ChainList
            Dim clsBaseLineVector As BaseLineVector
            Dim clsObservationPoint As ObservationPoint


            '2006/10/11 NGS Yamada
            Dim DuplicationFlag As Boolean  '重複フラグ True=重複,False=重複で無い。
            DuplicationFlag = False 'デフォルトはFalse  '2006 / 12 / 12 NGS Yamada


            '閉合。
            If Not clsRegistAngle Is Nothing Then
                '登録閉合表示。基線ベクトル。
                Set clsChainList = clsRegistAngle.RegistList
                Do While Not clsChainList Is Nothing
                    If(clsChainList.Element.ObjectType And OBJ_TYPE_BASELINEVECTOR) <> 0 Then
                        '基線ベクトル。
                        Call DrawBaseLineVectorNSS(hDC, nDeviceLeft, nDeviceTop, nMapScale, clsChainList.Element, m_hPenVectorAngle, m_hPenVectorAngle, bArrow, False, False)
                    End If
                    Set clsChainList = clsChainList.NextList
                Loop


                '登録閉合表示。観測点。
                Set clsChainList = clsRegistAngle.RegistList
                Do While Not clsChainList Is Nothing
                    If(clsChainList.Element.ObjectType And OBJ_TYPE_OBSERVATIONPOINT) <> 0 Then
                        '観測点。
                        If Not clsChainList.Element.Fixed Then
                            '固定点以外。
                            Call DrawObservationPointNSS(hDC, nDeviceLeft, nDeviceTop, nClipLeft, nClipTop, nClipWidth, nClipHeight, nMapScale, clsChainList.Element, m_hPenObspntAngle, m_hBrushNull, Obspnt_Color_Symbol_Angle)
                        }else {
                            '固定点。
                            Call DrawObservationPointNSS(hDC, nDeviceLeft, nDeviceTop, nClipLeft, nClipTop, nClipWidth, nClipHeight, nMapScale, clsChainList.Element, m_hPenObspntAngleFixed, m_hBrushNull, Obspnt_Color_Symbol_Angle_Fixed)
                        End If
                    End If
                    Set clsChainList = clsChainList.NextList
                Loop


                '閉合候補表示。基線ベクトル。
                Set clsChainList = clsRegistAngle.CandidateList
                Do While Not clsChainList Is Nothing
                    If(clsChainList.Element.ObjectType And OBJ_TYPE_BASELINEVECTOR) <> 0 Then
                        '基線ベクトル。
                        Call DrawBaseLineVectorNSS(hDC, nDeviceLeft, nDeviceTop, nMapScale, clsChainList.Element, m_hPenVectorCandidate, m_hPenVectorCandidate, bArrow, False, False)
                    End If
                    Set clsChainList = clsChainList.NextList
                Loop


        '        '閉合候補表示。観測点。
        '        Set clsChainList = clsRegistAngle.CandidateList
        '        Do While Not clsChainList Is Nothing
        '            If (clsChainList.Element.ObjectType And OBJ_TYPE_OBSERVATIONPOINT) <> 0 Then
        '                '観測点。
        '                If Not clsChainList.Element.Fixed Then
        '                    '固定点以外。
        '                    Call DrawObservationPointNSS(hDC, nDeviceLeft, nDeviceTop, nClipLeft, nClipTop, nClipWidth, nClipHeight, nMapScale, clsChainList.Element, m_hPenObspntCandidate, m_hBrushNull, OBSPNT_COLOR_SYMBOL_CANDIDATE)
        '                }else {
        '                    '固定点。
        '                    Call DrawObservationPointNSS(hDC, nDeviceLeft, nDeviceTop, nClipLeft, nClipTop, nClipWidth, nClipHeight, nMapScale, clsChainList.Element, m_hPenObspntCandidateFixed, m_hBrushNull, OBSPNT_COLOR_SYMBOL_CANDIDATE_FIXED)
        '                End If
        '            End If
        '            Set clsChainList = clsChainList.NextList
        '        Loop
            End If


            '最初に重複基線ベクトルを探す。
            Dim objDuplicationCollection As New Collection '重複基線ベクトルを判断するためのコレクション。
            Dim objDuplicationVectors As New Collection '探し当てた重複基線ベクトルを記憶するためのコレクション。
            Dim clsDuplicationVector As BaseLineVector
            Dim sKey As String
            Set clsChainList = GetDocument().NetworkModel.BaseLineVectorHead
            Do While Not clsChainList Is Nothing
                Set clsBaseLineVector = clsChainList.Element
                'Valid だけ評価しておく。→Enableも追加（有効-無効は重複ではないとする）Yamada
                If clsBaseLineVector.Enable And clsBaseLineVector.Valid Then
                    '重複を判断するためのキー。
                    sKey = GetDuplicationKey(clsBaseLineVector)
                    '重複の判断。
                    If Not LookupCollectionObject(objDuplicationCollection, clsDuplicationVector, sKey) Then
                        '1本目の基線ベクトルである。
                        'objDuplicationCollection コレクションに1本目のオブジェクトを設定する。
                        Call objDuplicationCollection.Add(clsBaseLineVector, sKey)
                    }else {
                        '2本目以降の基線ベクトルである。
                        '見つかった重複基線ベクトルは objDuplicationVectors コレクションに追加する。
                        Call objDuplicationVectors.Add(clsBaseLineVector, Hex$(GetPointer(clsBaseLineVector)))
                        '1本目も objDuplicationVectors コレクションに追加しなければならない。
                        If clsDuplicationVector Is Nothing Then
                            '1本目はすでに追加されている。
                        }else {
                            '1本目の重複基線ベクトルも objDuplicationVectors コレクションに追加する。
                            Call objDuplicationVectors.Add(clsDuplicationVector, Hex$(GetPointer(clsDuplicationVector)))
                            '1本目を objDuplicationVectors コレクションに追加したので、その印として objDuplicationCollection の内容を Nothing に置き換える。
                            Call objDuplicationCollection.Remove(sKey)
                            Call objDuplicationCollection.Add(Nothing, sKey)
                        End If
                    End If
                End If
                Set clsChainList = clsChainList.NextList
            Loop


            '強調表示。
            Set clsChainList = clsEmphasisHead.NextList
            Do While Not clsChainList Is Nothing
                If(clsChainList.Element.ObjectType And OBJ_TYPE_OBSERVATIONPOINT) <> 0 Then
                    '観測点。
                    If Not clsChainList.Element.Fixed Then
                        '固定点以外。
                        If clsChainList.Element.Enable Then
                            Call DrawObservationPointNSS(hDC, nDeviceLeft, nDeviceTop, nClipLeft, nClipTop, nClipWidth, nClipHeight, nMapScale, clsChainList.Element, m_hPenObspntEmphasis, m_hBrushNull, Obspnt_Color_Symbol_Emphasis)
                        }else {
                            Call DrawObservationPointNSS(hDC, nDeviceLeft, nDeviceTop, nClipLeft, nClipTop, nClipWidth, nClipHeight, nMapScale, clsChainList.Element, m_hPenObspntEmphasisDisable, m_hBrushNull, Obspnt_Color_Symbol_Emphasis_Disable)
                        End If
                    }else {
                        '固定点。
                        If clsChainList.Element.Enable Then
                            Call DrawObservationPointNSS(hDC, nDeviceLeft, nDeviceTop, nClipLeft, nClipTop, nClipWidth, nClipHeight, nMapScale, clsChainList.Element, m_hPenObspntEmphasisFixed, m_hBrushNull, Obspnt_Color_Symbol_Emphasis_Fixed)
                        }else {
                            Call DrawObservationPointNSS(hDC, nDeviceLeft, nDeviceTop, nClipLeft, nClipTop, nClipWidth, nClipHeight, nMapScale, clsChainList.Element, m_hPenObspntEmphasisDisable, m_hBrushNull, Obspnt_Color_Symbol_Emphasis_Disable)
                        End If
                    End If
                }else {If(clsChainList.Element.ObjectType And OBJ_TYPE_BASELINEVECTOR) <> 0 Then
                    Set clsBaseLineVector = clsChainList.Element


                    '重複しているか？'2006 / 10 / 11 NGS Yamada
                    If LookupCollectionObject(objDuplicationVectors, clsDuplicationVector, Hex$(GetPointer(clsBaseLineVector))) Then
                        DuplicationFlag = True
                    }else {
                        DuplicationFlag = False
                    End If


                    '基線ベクトル。
                    If Not clsRegistAngle Is Nothing Then
                        Call DrawBaseLineVectorNSS(hDC, nDeviceLeft, nDeviceTop, nMapScale, clsBaseLineVector, m_hPenVectorEmphasisCandidate, m_hPenVectorEmphasisCandidate, bArrow, False, False)
                    }else {If clsBaseLineVector.Enable Then
                        Dim hPenLine As Long
                        Dim hPenArrow As Long
                        If clsBaseLineVector.Analysis = ANALYSIS_STATUS_FIX Then
                            '重複しているか？'2006 / 10 / 11 NGS Yamada
        '                    If LookupCollectionObject(objDuplicationVectors, clsDuplicationVector, Hex$(GetPointer(clsBaseLineVector))) Then
        '                        DuplicationFlag = True
        '                    }else {
        '                        DuplicationFlag = False
        '                    End If
                            'バイアス決定比によってベクトルの色を変える　2006/10/12 NGS Yamada
                            If clsBaseLineVector.Bias >= clsBaseLineVector.MinRatio Then
                                hPenLine = m_hPenVectorEmphasisFix
                                hPenArrow = m_hPenVectorEmphasisFix
                            }else {
                                hPenLine = m_hPenVectorEmphasisFix_LowRatio
                                hPenArrow = m_hPenVectorEmphasisFix_LowRatio
                            End If
        '                    Call DrawBaseLineVectorNSS(hDC, nDeviceLeft, nDeviceTop, nMapScale, clsBaseLineVector, m_hPenVectorEmphasisFix, m_hPenVectorEmphasisFix, bArrow, True, m_hPenVectorEmphasisCorrect, m_hPenVectorEmphasisDisable)
                        }else {If clsBaseLineVector.Analysis = ANALYSIS_STATUS_FLOAT Then
                            hPenLine = m_hPenVectorEmphasisFloat
                            hPenArrow = m_hPenVectorEmphasisFloat
        '                    Call DrawBaseLineVectorNSS(hDC, nDeviceLeft, nDeviceTop, nMapScale, clsBaseLineVector, m_hPenVectorEmphasisFloat, m_hPenVectorEmphasisFloat, bArrow, True, m_hPenVectorEmphasisCorrect, m_hPenVectorEmphasisDisable)
                        }else {If clsBaseLineVector.Analysis = ANALYSIS_STATUS_FAILED Then
                            hPenLine = m_hPenVectorEmphasisFailed
                            hPenArrow = m_hPenVectorEmphasisFailed
        '                    Call DrawBaseLineVectorNSS(hDC, nDeviceLeft, nDeviceTop, nMapScale, clsBaseLineVector, m_hPenVectorEmphasisFailed, m_hPenVectorEmphasisFailed, bArrow, True, m_hPenVectorEmphasisCorrect, m_hPenVectorEmphasisDisable)
                        }else {
                            hPenLine = m_hPenVectorEmphasis
                            hPenArrow = m_hPenVectorEmphasis
        '                    Call DrawBaseLineVectorNSS(hDC, nDeviceLeft, nDeviceTop, nMapScale, clsBaseLineVector, m_hPenVectorEmphasis, m_hPenVectorEmphasis, bArrow, True, m_hPenVectorEmphasisCorrect, m_hPenVectorEmphasisDisable)
                        End If
                        Call DrawBaseLineVectorNSS(hDC, nDeviceLeft, nDeviceTop, nMapScale, clsBaseLineVector, hPenLine, hPenArrow, bArrow, True, DuplicationFlag, m_hPenVectorCorrect, m_hPenVectorDisable)
                    }else {
                        Call DrawBaseLineVectorNSS(hDC, nDeviceLeft, nDeviceTop, nMapScale, clsBaseLineVector, m_hPenVectorEmphasisDisable, m_hPenVectorEmphasisDisable, bArrow, True, DuplicationFlag, m_hPenVectorEmphasisDisable, m_hPenVectorEmphasisDisable)
                    End If
                End If
                Set clsChainList = clsChainList.NextList
            Loop


        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        'オブジェクトの描画。
        '
        '強調オブジェクト等の変化の多い部分を描画する。
        'nClip～は記号を描画するためのクリップ領域。
        '記号はクリップ領域をはみ出さないように描画する。
        'clsRegistAngle が Nothing でない場合、閉合関係のオブジェクトも描画する。
        '
        '引き数：
        'hDC デバイスコンテキストのハンドル。
        'nDeviceLeft 論理エリア上のデバイス左辺位置(ピクセル)。
        'nDeviceTop 論理エリア上のデバイス上辺位置(ピクセル)。
        'nClipLeft 論理エリア上のクリップ領域左辺(ピクセル)。
        'nClipTop 論理エリア上のクリップ領域上辺(ピクセル)。
        'nClipWidth 論理エリア上のクリップ領域幅(ピクセル)。
        'nClipHeight 論理エリア上のクリップ領域高さ(ピクセル)。
        'nMapScale Twips→ピクセル、の係数。
        'clsEmphasisHead 強調オブジェクトリストのヘッダー。
        'clsRegistAngle RegistAngle オブジェクト。
        'bArrow 矢印描画の指定。True=描画する。False=描画しない。
        */
#if false
        public void DrawObjectFilm(long hDC, double nDeviceLeft, double nDeviceTop, double nClipLeft, double nClipTop, double nClipWidth, double nClipHeight, double nMapScale,
                        ChainList clsEmphasisHead, RegistAngle clsRegistAngle, bool bArrow)
        {
#else
        public Bitmap DrawObjectFilm(Bitmap hDC, double nDeviceLeft, double nDeviceTop, double nClipLeft, double nClipTop, double nClipWidth, double nClipHeight, double nMapScale,
                        ChainList clsEmphasisHead, RegistAngle clsRegistAngle, bool bArrow)
        {
            Bitmap btmp = hDC;
#endif
            ChainList clsChainList;
            BaseLineVector clsBaseLineVector;
            ObservationPoint clsObservationPoint;

            //'2006/10/11 NGS Yamada
            bool DuplicationFlag;           //'重複フラグ True=重複,False=重複で無い。
            DuplicationFlag = false;        //'デフォルトはFalse  //'2006 / 12 / 12 NGS Yamada


            //'閉合。
            if (clsRegistAngle != null)
            {
                //'登録閉合表示。基線ベクトル。
                clsChainList = clsRegistAngle.RegistList();
                while (clsChainList != null)
                {
                    clsObservationPoint = (ObservationPoint)clsChainList.Element;
                    if ((clsObservationPoint.ObjectType & OBJ_TYPE_BASELINEVECTOR) != 0)
                    {
#if false
                        //'基線ベクトル。
                        DrawBaseLineVectorNSS(hDC, nDeviceLeft, nDeviceTop, nMapScale, (BaseLineVector)clsChainList.Element, m_hPenVectorAngle, m_hPenVectorAngle, bArrow, false, false);
#else
                        //'基線ベクトル。
                        btmp = DrawBaseLineVectorNSS(btmp, nDeviceLeft, nDeviceTop, nMapScale, (BaseLineVector)clsChainList.Element, m_hPenVectorAngle, m_hPenVectorAngle, bArrow, false, false);
#endif
                    }
                    clsChainList = clsChainList.NextList();
                }


                //'登録閉合表示。観測点。
                clsChainList = clsRegistAngle.RegistList();
                while (clsChainList != null)
                {
                    clsObservationPoint = (ObservationPoint)clsChainList.Element;
                    if ((clsObservationPoint.ObjectType & OBJ_TYPE_OBSERVATIONPOINT) != 0)
                    {
                        //'観測点。
                        if (!clsObservationPoint.Fixed())
                        {
#if false
                            //'固定点以外。
                            DrawObservationPointNSS(hDC, nDeviceLeft, nDeviceTop, nClipLeft, nClipTop, nClipWidth, nClipHeight, nMapScale, (ObservationPoint)clsChainList.Element,
                                m_hPenObspntAngle, m_hBrushNull, Obspnt_Color_Symbol_Angle);
#else
                            //'固定点以外。
                            btmp = DrawObservationPointNSS(btmp, nDeviceLeft, nDeviceTop, nClipLeft, nClipTop, nClipWidth, nClipHeight, nMapScale, (ObservationPoint)clsChainList.Element,
                                m_hPenObspntAngle, m_hBrushNull, Obspnt_Color_Symbol_Angle);
#endif
                        }
                        else {
#if false
                            //'固定点。
                            DrawObservationPointNSS(hDC, nDeviceLeft, nDeviceTop, nClipLeft, nClipTop, nClipWidth, nClipHeight, nMapScale, (ObservationPoint)clsChainList.Element,
                                m_hPenObspntAngleFixed, m_hBrushNull, Obspnt_Color_Symbol_Angle_Fixed);
#else
                            //'固定点。
                            btmp = DrawObservationPointNSS(btmp, nDeviceLeft, nDeviceTop, nClipLeft, nClipTop, nClipWidth, nClipHeight, nMapScale, (ObservationPoint)clsChainList.Element,
                                m_hPenObspntAngleFixed, m_hBrushNull, Obspnt_Color_Symbol_Angle_Fixed);
#endif
                        }
                    }
                    clsChainList = clsChainList.NextList();
                }


                //'閉合候補表示。基線ベクトル。
                clsChainList = clsRegistAngle.CandidateList();
                while (clsChainList != null)
                {
                    clsObservationPoint = (ObservationPoint)clsChainList.Element;
                    if ((clsObservationPoint.ObjectType & OBJ_TYPE_BASELINEVECTOR) != 0)
                    {
#if false
                        //'基線ベクトル。
                        DrawBaseLineVectorNSS(hDC, nDeviceLeft, nDeviceTop, nMapScale, (BaseLineVector)clsChainList.Element, m_hPenVectorCandidate, m_hPenVectorCandidate, bArrow, false, false);
#else
                        //'基線ベクトル。
                        btmp = DrawBaseLineVectorNSS(btmp, nDeviceLeft, nDeviceTop, nMapScale, (BaseLineVector)clsChainList.Element, m_hPenVectorCandidate, m_hPenVectorCandidate, bArrow, false, false);
#endif
                    }
                    clsChainList = clsChainList.NextList();
                }
            }


            //'最初に重複基線ベクトルを探す。
            Dictionary<string, object> objDuplicationCollection = new Dictionary<string, object>();         //'重複基線ベクトルを判断するためのコレクション。
            Dictionary<string, object> objDuplicationVectors = new Dictionary<string, object>();            //'探し当てた重複基線ベクトルを記憶するためのコレクション。
            BaseLineVector clsDuplicationVector = null;
            object objDuplicationVector = null;
            string sKey;
            clsChainList = m_clsMdlMain.GetDocument().NetworkModel().BaseLineVectorHead();
            while (clsChainList != null)
            {
                clsBaseLineVector = (BaseLineVector)clsChainList.Element;
                //'Valid だけ評価しておく。→Enableも追加（有効-無効は重複ではないとする）Yamada
                if (clsBaseLineVector.Enable() && clsBaseLineVector.Valid()){
                    //'重複を判断するためのキー。
                    //MdlNSUtility clsMdlNSUtility = new MdlNSUtility();
                    sKey = GetDuplicationKey(clsBaseLineVector);
                    //'重複の判断。
                    objDuplicationVector = clsDuplicationVector;
                    if (!LookupCollectionObject(objDuplicationCollection, ref objDuplicationVector, sKey))
                    {
                        //'1本目の基線ベクトルである。
                        //'objDuplicationCollection コレクションに1本目のオブジェクトを設定する。
                        objDuplicationCollection.Add(sKey, objDuplicationVector);
                    }
                    else
                    {
                        //'2本目以降の基線ベクトルである。
                        //'見つかった重複基線ベクトルは objDuplicationVectors コレクションに追加する。
                        clsDuplicationVector = (BaseLineVector)objDuplicationVector;
                        objDuplicationVectors.Add(Convert.ToString(GetPointer(clsBaseLineVector)), clsBaseLineVector);
                        //'1本目も objDuplicationVectors コレクションに追加しなければならない。
                        if (clsDuplicationVector == null)
                        {
                            //'1本目はすでに追加されている。
                        }
                        else
                        {
                            //'1本目の重複基線ベクトルも objDuplicationVectors コレクションに追加する。
                            objDuplicationVectors.Add(Convert.ToString(GetPointer(clsDuplicationVector)), clsDuplicationVector);
                            //'1本目を objDuplicationVectors コレクションに追加したので、その印として objDuplicationCollection の内容を Nothing に置き換える。
                            objDuplicationCollection.Remove(sKey);
                            objDuplicationCollection.Add(null, sKey);
                        }
                    }
                }
                clsChainList = clsChainList.NextList();
            }


            //'強調表示。
            clsChainList = clsEmphasisHead.NextList();
            while (clsChainList != null)
            {
                clsObservationPoint = (ObservationPoint)clsChainList.Element;
                if ((clsObservationPoint.ObjectType & OBJ_TYPE_OBSERVATIONPOINT) != 0)
                {
                    //'観測点。
                    if (!clsObservationPoint.Fixed())
                    {
                        //'固定点以外。
                        if (clsObservationPoint.Enable())
                        {
#if false
                            DrawObservationPointNSS(hDC, nDeviceLeft, nDeviceTop, nClipLeft, nClipTop, nClipWidth, nClipHeight, nMapScale, (ObservationPoint)clsChainList.Element,
                                m_hPenObspntEmphasis, m_hBrushNull, Obspnt_Color_Symbol_Emphasis);
#else
                            btmp = DrawObservationPointNSS(btmp, nDeviceLeft, nDeviceTop, nClipLeft, nClipTop, nClipWidth, nClipHeight, nMapScale, (ObservationPoint)clsChainList.Element,
                                m_hPenObspntEmphasis, m_hBrushNull, Obspnt_Color_Symbol_Emphasis);
#endif
                        }
                        else
                        {
#if false
                            DrawObservationPointNSS(hDC, nDeviceLeft, nDeviceTop, nClipLeft, nClipTop, nClipWidth, nClipHeight, nMapScale, (ObservationPoint)clsChainList.Element,
                                m_hPenObspntEmphasisDisable, m_hBrushNull, Obspnt_Color_Symbol_Emphasis_Disable);
#else
                            btmp = DrawObservationPointNSS(btmp, nDeviceLeft, nDeviceTop, nClipLeft, nClipTop, nClipWidth, nClipHeight, nMapScale, (ObservationPoint)clsChainList.Element,
                                m_hPenObspntEmphasisDisable, m_hBrushNull, Obspnt_Color_Symbol_Emphasis_Disable);
#endif
                        }
                    }
                    else
                    {
                        //'固定点。
                        if (clsObservationPoint.Enable())
                        {
#if false
                            DrawObservationPointNSS(hDC, nDeviceLeft, nDeviceTop, nClipLeft, nClipTop, nClipWidth, nClipHeight, nMapScale, (ObservationPoint)clsChainList.Element,
                                m_hPenObspntEmphasisFixed, m_hBrushNull, Obspnt_Color_Symbol_Emphasis_Fixed);
#else
                            btmp = DrawObservationPointNSS(btmp, nDeviceLeft, nDeviceTop, nClipLeft, nClipTop, nClipWidth, nClipHeight, nMapScale, (ObservationPoint)clsChainList.Element,
                                m_hPenObspntEmphasisFixed, m_hBrushNull, Obspnt_Color_Symbol_Emphasis_Fixed);
#endif
                        }
                        else
                        {
#if false
                            DrawObservationPointNSS(hDC, nDeviceLeft, nDeviceTop, nClipLeft, nClipTop, nClipWidth, nClipHeight, nMapScale, (ObservationPoint)clsChainList.Element,
                                m_hPenObspntEmphasisDisable, m_hBrushNull, Obspnt_Color_Symbol_Emphasis_Disable);
#else
                            btmp = DrawObservationPointNSS(btmp, nDeviceLeft, nDeviceTop, nClipLeft, nClipTop, nClipWidth, nClipHeight, nMapScale, (ObservationPoint)clsChainList.Element,
                                m_hPenObspntEmphasisDisable, m_hBrushNull, Obspnt_Color_Symbol_Emphasis_Disable);
#endif
                        }
                    }
                }
                else if ((clsObservationPoint.ObjectType & OBJ_TYPE_BASELINEVECTOR) != 0)
                {
                    clsBaseLineVector = (BaseLineVector)clsChainList.Element;


                    //'重複しているか？//'2006 / 10 / 11 NGS Yamada
                    objDuplicationVector = clsDuplicationVector;
                    DuplicationFlag = LookupCollectionObject(objDuplicationVectors, ref objDuplicationVector, Convert.ToString(GetPointer(clsBaseLineVector)));


                    //'基線ベクトル。
                    if (clsRegistAngle != null){
#if false
                        DrawBaseLineVectorNSS(hDC, nDeviceLeft, nDeviceTop, nMapScale, clsBaseLineVector, m_hPenVectorEmphasisCandidate,
                            m_hPenVectorEmphasisCandidate, bArrow, false, false);
#else
                        btmp = DrawBaseLineVectorNSS(btmp, nDeviceLeft, nDeviceTop, nMapScale, clsBaseLineVector, m_hPenVectorEmphasisCandidate,
                            m_hPenVectorEmphasisCandidate, bArrow, false, false);
#endif
                    }
                    else if (clsBaseLineVector.Enable())
                    {
                        long hPenLine;
                        long hPenArrow;
                        if (clsBaseLineVector.Analysis == ANALYSIS_STATUS.ANALYSIS_STATUS_FIX)
                        {
                            //'重複しているか？//'2006 / 10 / 11 NGS Yamada
                            //'                    if (LookupCollectionObject(objDuplicationVectors, clsDuplicationVector, Hex$(GetPointer(clsBaseLineVector)))){
                            //'                        DuplicationFlag = True
                            //'                    }else {
                            //'                        DuplicationFlag = False
                            //'                    }
                            //'バイアス決定比によってベクトルの色を変える　2006/10/12 NGS Yamada
                            if (clsBaseLineVector.Bias >= clsBaseLineVector.MinRatio)
                            {
                                hPenLine = m_hPenVectorEmphasisFix;
                                hPenArrow = m_hPenVectorEmphasisFix;
                            }
                            else
                            {
                                hPenLine = m_hPenVectorEmphasisFix_LowRatio;
                                hPenArrow = m_hPenVectorEmphasisFix_LowRatio;
                            }
        //'                    Call DrawBaseLineVectorNSS(hDC, nDeviceLeft, nDeviceTop, nMapScale, clsBaseLineVector, m_hPenVectorEmphasisFix, m_hPenVectorEmphasisFix, bArrow, True, m_hPenVectorEmphasisCorrect, m_hPenVectorEmphasisDisable)
                        }
                        else if (clsBaseLineVector.Analysis == ANALYSIS_STATUS.ANALYSIS_STATUS_FLOAT)
                        {
                            hPenLine = m_hPenVectorEmphasisFloat;
                            hPenArrow = m_hPenVectorEmphasisFloat;
        //'                    Call DrawBaseLineVectorNSS(hDC, nDeviceLeft, nDeviceTop, nMapScale, clsBaseLineVector, m_hPenVectorEmphasisFloat, m_hPenVectorEmphasisFloat, bArrow, True, m_hPenVectorEmphasisCorrect, m_hPenVectorEmphasisDisable)
                        }
                        else if (clsBaseLineVector.Analysis == ANALYSIS_STATUS.ANALYSIS_STATUS_FAILED)
                        {
                            hPenLine = m_hPenVectorEmphasisFailed;
                            hPenArrow = m_hPenVectorEmphasisFailed;
        //'                    Call DrawBaseLineVectorNSS(hDC, nDeviceLeft, nDeviceTop, nMapScale, clsBaseLineVector, m_hPenVectorEmphasisFailed, m_hPenVectorEmphasisFailed, bArrow, True, m_hPenVectorEmphasisCorrect, m_hPenVectorEmphasisDisable)
                        }
                        else
                        {
                            hPenLine = m_hPenVectorEmphasis;
                            hPenArrow = m_hPenVectorEmphasis;
        //'                    Call DrawBaseLineVectorNSS(hDC, nDeviceLeft, nDeviceTop, nMapScale, clsBaseLineVector, m_hPenVectorEmphasis, m_hPenVectorEmphasis, bArrow, True, m_hPenVectorEmphasisCorrect, m_hPenVectorEmphasisDisable)
                        }
#if false
                        DrawBaseLineVectorNSS(hDC, nDeviceLeft, nDeviceTop, nMapScale, clsBaseLineVector, hPenLine, hPenArrow, bArrow, true, DuplicationFlag,
                            m_hPenVectorCorrect, m_hPenVectorDisable);
#else
                        btmp = DrawBaseLineVectorNSS(btmp, nDeviceLeft, nDeviceTop, nMapScale, clsBaseLineVector, hPenLine, hPenArrow, bArrow, true, DuplicationFlag,
                            m_hPenVectorCorrect, m_hPenVectorDisable);
#endif
                    }
                    else
                    {
#if false
                        DrawBaseLineVectorNSS(hDC, nDeviceLeft, nDeviceTop, nMapScale, clsBaseLineVector, m_hPenVectorEmphasisDisable, m_hPenVectorEmphasisDisable,
                            bArrow, true, DuplicationFlag, m_hPenVectorEmphasisDisable, m_hPenVectorEmphasisDisable);
#else
                        btmp = DrawBaseLineVectorNSS(btmp, nDeviceLeft, nDeviceTop, nMapScale, clsBaseLineVector, m_hPenVectorEmphasisDisable, m_hPenVectorEmphasisDisable,
                            bArrow, true, DuplicationFlag, m_hPenVectorEmphasisDisable, m_hPenVectorEmphasisDisable);
#endif
                    }
                }
                clsChainList = clsChainList.NextList();
            }
#if false
            return;
#else
            return btmp;
#endif
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'オブジェクトの描画。
        '
        '観測点や基線ベクトル等のオブジェクトを描画する。
        'nClip～は記号を描画するためのクリップ領域。
        '記号はクリップ領域をはみ出さないように描画する。
        'bIsEnable が True の場合、有効なオブジェクトのみ描画する。
        'bIsEnable が False の場合、無効なオブジェクトも描画する。
        'nSwitch は描画するオブジェクトを決定するためのスイッチであり、アプリごとに固有の内容を設定する。
        'nClick に OBJ_TYPE_OBSERVATIONPOINT が指定されている場合、観測点をクリックオブジェクトとして登録する。
        'nClick に OBJ_TYPE_BASELINEVECTOR が指定されている場合、基線ベクトルをクリックオブジェクトとして登録する。
        '
        '引き数：
        'hDC デバイスコンテキストのハンドル。
        'nDeviceLeft 論理エリア上のデバイス左辺位置(ピクセル)。
        'nDeviceTop 論理エリア上のデバイス上辺位置(ピクセル)。
        'nDeviceRight 論理エリア上のデバイス右辺位置(ピクセル)。
        'nDeviceBottom 論理エリア上のデバイス下辺位置(ピクセル)。
        'nClipLeft 論理エリア上のクリップ領域左辺(ピクセル)。
        'nClipTop 論理エリア上のクリップ領域上辺(ピクセル)。
        'nClipWidth 論理エリア上のクリップ領域幅(ピクセル)。
        'nClipHeight 論理エリア上のクリップ領域高さ(ピクセル)。
        'nMapScale Twips→ピクセル、の係数。
        'clsClickManager ClickManager オブジェクト。
        'bArrow 矢印描画の指定。True=描画する。False=描画しない。
        'bIsEnable 有効/無効評価フラグ。
        'nSwitch 評価スイッチ。
        'nClick クリックオブジェクトを登録するオブジェクトの種類を指定する。
        Public Sub DrawObject(ByVal hDC As Long, ByVal nDeviceLeft As Double, ByVal nDeviceTop As Double, ByVal nDeviceRight As Double, ByVal nDeviceBottom As Double, ByVal nClipLeft As Double, ByVal nClipTop As Double, ByVal nClipWidth As Double, ByVal nClipHeight As Double, ByVal nMapScale As Double, ByVal clsClickManager As ClickManager, ByVal bArrow As Boolean, ByVal bIsEnable As Boolean, ByVal nSwitch As Long, ByVal nClick As Long)

            Dim clsClickObject As ClickObjectInterface


            Dim bClickPoint As Boolean
            Dim bClickVector As Boolean
            bClickPoint = (nClick And OBJ_TYPE_OBSERVATIONPOINT) <> 0
            bClickVector = (nClick And OBJ_TYPE_BASELINEVECTOR) <> 0




            '観測点、基線ベクトルの描画。
            Dim clsChainList As ChainList
            Dim clsBaseLineVector As BaseLineVector
            Dim clsObservationPoint As ObservationPoint
            Dim nX As Double
            Dim nY As Double
            Dim nW As Double
            Dim nH As Double


            '2006/10/11 NGS Yamada
            Dim DuplicationFlag As Boolean  '重複フラグ True=重複,False=重複で無い。
            DuplicationFlag = False 'デフォルトはFalse  '2006 / 12 / 12 NGS Yamada


            'オプションの「観測点のラベル」に合わせて表示項目を変更 2006/10/6 NGS Yamada
            m_PlotPointLabel = GetDocument().PlotPointLabel


            '「無効」なベクトルをプロット画面に表示するか？ 2006/10/10 NGS Yamada
            m_DisableVectorVisible = GetDocument().DisableVectorVisible


            '偏心ベクトル。
            Set clsChainList = GetDocument().NetworkModel.IsolatePointHead
            Do While Not clsChainList Is Nothing
                Set clsObservationPoint = clsChainList.Element
                If clsObservationPoint.Genuine Then
                    '描画。
                    Call DrawCorrectVectorMulti(hDC, nDeviceLeft, nDeviceTop, nMapScale, clsObservationPoint, clsObservationPoint.CorrectPoint, m_hPenVectorEccentric, m_hPenVectorDisable)
                End If
                Set clsChainList = clsChainList.NextList
            Loop


            If m_DisableVectorVisible Then '「無効」なベクトルをプロット画面に表示するか？ 2006/10/10 NGS Yamada
                '無効基線ベクトル。
                Set clsChainList = GetDocument().NetworkModel.BaseLineVectorHead
                Do While Not clsChainList Is Nothing
                    Set clsBaseLineVector = clsChainList.Element
                    If Not clsBaseLineVector.Enable And clsBaseLineVector.Valid Then
                        'クリックオブジェクト登録。
                        If bClickVector Then Set clsClickObject = clsClickManager.RegistVector(clsBaseLineVector)
                        '描画。補正ベクトルもあるので無条件に描画する。
                        Call DrawBaseLineVectorNSS(hDC, nDeviceLeft, nDeviceTop, nMapScale, clsBaseLineVector, m_hPenVectorDisable, m_hPenVectorDisable, bArrow, True, False, m_hPenVectorDisable, m_hPenVectorDisable)
                    End If
                    Set clsChainList = clsChainList.NextList
                Loop
            End If


            '無効観測点。
            Set clsChainList = GetDocument().NetworkModel.RepresentPointHead
            Do While Not clsChainList Is Nothing
                Set clsObservationPoint = clsChainList.Element
                If Not clsObservationPoint.Enable Then
                    If bClickPoint Then
                        'クリックオブジェクト登録。
                        If clsObservationPoint.Fixed Then
                            Set clsClickObject = clsClickManager.RegistFixed(clsObservationPoint)
                        Else
                            Set clsClickObject = clsClickManager.RegistPoint(clsObservationPoint)
                        End If
                        '描画。
                        If clsClickObject.IsInclude(nDeviceLeft, nDeviceTop, nDeviceRight, nDeviceBottom) Then
                            Call DrawObservationPointNSS(hDC, nDeviceLeft, nDeviceTop, nClipLeft, nClipTop, nClipWidth, nClipHeight, nMapScale, clsObservationPoint, m_hPenObspntDisable, m_hBrushNull, Obspnt_Color_Symbol_Disable, nX, nY, nW, nH)
                            Set clsClickObject = clsClickManager.RegistSymbol(clsObservationPoint, nX, nY, nW, nH)
                        End If
                    Else
                        Call DrawObservationPointNSS(hDC, nDeviceLeft, nDeviceTop, nClipLeft, nClipTop, nClipWidth, nClipHeight, nMapScale, clsObservationPoint, m_hPenObspntDisable, m_hBrushNull, Obspnt_Color_Symbol_Disable, nX, nY, nW, nH)
                    End If
                End If
                Set clsChainList = clsChainList.NextList
            Loop


            '有効基線ベクトル。


            '最初に重複基線ベクトルを探す。
            Dim objDuplicationCollection As New Collection '重複基線ベクトルを判断するためのコレクション。
            Dim objDuplicationVectors As New Collection '探し当てた重複基線ベクトルを記憶するためのコレクション。
            Dim clsDuplicationVector As BaseLineVector
            Dim sKey As String
            Set clsChainList = GetDocument().NetworkModel.BaseLineVectorHead
            Do While Not clsChainList Is Nothing
                Set clsBaseLineVector = clsChainList.Element
                'Valid だけ評価しておく。→Enableも追加（有効-無効は重複ではないとする）Yamada
                If clsBaseLineVector.Enable And clsBaseLineVector.Valid Then
                    '重複を判断するためのキー。
                    sKey = GetDuplicationKey(clsBaseLineVector)
                    '重複の判断。
                    If Not LookupCollectionObject(objDuplicationCollection, clsDuplicationVector, sKey) Then
                        '1本目の基線ベクトルである。
                        'objDuplicationCollection コレクションに1本目のオブジェクトを設定する。
                        Call objDuplicationCollection.Add(clsBaseLineVector, sKey)
                    Else
                        '2本目以降の基線ベクトルである。
                        '見つかった重複基線ベクトルは objDuplicationVectors コレクションに追加する。
                        Call objDuplicationVectors.Add(clsBaseLineVector, Hex$(GetPointer(clsBaseLineVector)))
                        '1本目も objDuplicationVectors コレクションに追加しなければならない。
                        If clsDuplicationVector Is Nothing Then
                            '1本目はすでに追加されている。
                        Else
                            '1本目の重複基線ベクトルも objDuplicationVectors コレクションに追加する。
                            Call objDuplicationVectors.Add(clsDuplicationVector, Hex$(GetPointer(clsDuplicationVector)))
                            '1本目を objDuplicationVectors コレクションに追加したので、その印として objDuplicationCollection の内容を Nothing に置き換える。
                            Call objDuplicationCollection.Remove(sKey)
                            Call objDuplicationCollection.Add(Nothing, sKey)
                        End If
                    End If
                End If
                Set clsChainList = clsChainList.NextList
            Loop


            Dim hPenLine As Long
            Dim hPenArrow As Long
            Set clsChainList = GetDocument().NetworkModel.BaseLineVectorHead
            Do While Not clsChainList Is Nothing
                Set clsBaseLineVector = clsChainList.Element
                If clsBaseLineVector.Enable And clsBaseLineVector.Valid Then
                    '重複しているか？'2006 / 10 / 11 NGS Yamada
                    If LookupCollectionObject(objDuplicationVectors, clsDuplicationVector, Hex$(GetPointer(clsBaseLineVector))) Then
                        DuplicationFlag = True
                    Else
                        DuplicationFlag = False
                    End If
                    If clsBaseLineVector.Analysis = ANALYSIS_STATUS_FIX Then
        '                Debug.Print "MinRatio = " & clsBaseLineVector.MinRatio & " : Ratio = " & clsBaseLineVector.Bias
                        'バイアス決定比によってベクトルの色を変える　2006/10/12 NGS Yamada
                        If clsBaseLineVector.Bias >= clsBaseLineVector.MinRatio Then
                            hPenLine = m_hPenVectorFix
                            hPenArrow = m_hPenVectorFix
                        Else
                            hPenLine = m_hPenVectorFix_LowRatio
                            hPenArrow = m_hPenVectorFix_LowRatio
                        End If
                    ElseIf clsBaseLineVector.Analysis = ANALYSIS_STATUS_FLOAT Then
                        hPenLine = m_hPenVectorFloat
                        hPenArrow = m_hPenVectorFloat
                    ElseIf clsBaseLineVector.Analysis = ANALYSIS_STATUS_FAILED Then
                        hPenLine = m_hPenVectorFailed
                        hPenArrow = m_hPenVectorFailed
                    Else
                        hPenLine = m_hPenVectorEnable
                        hPenArrow = m_hPenVectorEnable
                    End If
                    '重複時の基線ベクトルは強調線から２重線に変更したため、重複によって線種を分ける処理は削除 2006/10/12 NGS Yamada

        '            '重複しているか？
        '            If Not LookupCollectionObject(objDuplicationVectors, clsDuplicationVector, Hex$(GetPointer(clsBaseLineVector))) Then
        '                DuplicationFlag = False '2006 / 10 / 11 NGS Yamada
        '                If clsBaseLineVector.Analysis = ANALYSIS_STATUS_FIX Then
        '                    Debug.Print "MinRatio = " & clsBaseLineVector.MinRatio & " : Ratio = " & clsBaseLineVector.Bias
        '                    hPenLine = m_hPenVectorFix
        '                    hPenArrow = m_hPenVectorFix
        '                ElseIf clsBaseLineVector.Analysis = ANALYSIS_STATUS_FLOAT Then
        '                    hPenLine = m_hPenVectorFloat
        '                    hPenArrow = m_hPenVectorFloat
        '                ElseIf clsBaseLineVector.Analysis = ANALYSIS_STATUS_FAILED Then
        '                    hPenLine = m_hPenVectorFailed
        '                    hPenArrow = m_hPenVectorFailed
        '                Else
        '                    hPenLine = m_hPenVectorEnable
        '                    hPenArrow = m_hPenVectorEnable
        '                End If
        '            Else
        '                DuplicationFlag = True '2006 / 10 / 11 NGS Yamada
        '                If clsBaseLineVector.Analysis = ANALYSIS_STATUS_FIX Then
        '                    hPenLine = m_hPenVectorDuplicationFix
        '                    hPenArrow = m_hPenVectorFix
        '                ElseIf clsBaseLineVector.Analysis = ANALYSIS_STATUS_FLOAT Then
        '                    hPenLine = m_hPenVectorDuplicationFloat
        '                    hPenArrow = m_hPenVectorFloat
        '                ElseIf clsBaseLineVector.Analysis = ANALYSIS_STATUS_FAILED Then
        '                    hPenLine = m_hPenVectorDuplicationFailed
        '                    hPenArrow = m_hPenVectorFailed
        '                Else
        '                    hPenLine = m_hPenVectorDuplication
        '                    hPenArrow = m_hPenVectorEnable
        '                End If
        '            End If
                    'クリックオブジェクト登録。
                    If bClickVector Then Set clsClickObject = clsClickManager.RegistVector(clsBaseLineVector)
                    '描画。補正ベクトルもあるので無条件に描画する。
                    Call DrawBaseLineVectorNSS(hDC, nDeviceLeft, nDeviceTop, nMapScale, clsBaseLineVector, hPenLine, hPenArrow, bArrow, True, DuplicationFlag, m_hPenVectorCorrect, m_hPenVectorDisable)
                End If
                Set clsChainList = clsChainList.NextList
            Loop


            '有効観測点。
            Set clsChainList = GetDocument().NetworkModel.RepresentPointHead
            Do While Not clsChainList Is Nothing
                Set clsObservationPoint = clsChainList.Element
                If clsObservationPoint.Enable And(Not clsObservationPoint.Fixed) Then
                    If bClickPoint Then
                        'クリックオブジェクト登録。
                        Set clsClickObject = clsClickManager.RegistPoint(clsObservationPoint)
                        '描画。
                        If clsClickObject.IsInclude(nDeviceLeft, nDeviceTop, nDeviceRight, nDeviceBottom) Then
                            Call DrawObservationPointNSS(hDC, nDeviceLeft, nDeviceTop, nClipLeft, nClipTop, nClipWidth, nClipHeight, nMapScale, clsObservationPoint, m_hPenObspntEnable, m_hBrushNull, Obspnt_Color_Symbol_Enable, nX, nY, nW, nH)
                            Set clsClickObject = clsClickManager.RegistSymbol(clsObservationPoint, nX, nY, nW, nH)
                        End If
                    Else
                        Call DrawObservationPointNSS(hDC, nDeviceLeft, nDeviceTop, nClipLeft, nClipTop, nClipWidth, nClipHeight, nMapScale, clsObservationPoint, m_hPenObspntEnable, m_hBrushNull, Obspnt_Color_Symbol_Enable, nX, nY, nW, nH)
                    End If
                End If
                Set clsChainList = clsChainList.NextList
            Loop


            '固定観測点。
            Set clsChainList = GetDocument().NetworkModel.RepresentPointHead
            Do While Not clsChainList Is Nothing
                Set clsObservationPoint = clsChainList.Element
                If clsObservationPoint.Enable And clsObservationPoint.Fixed Then
                    If bClickPoint Then
                        'クリックオブジェクト登録。
                        Set clsClickObject = clsClickManager.RegistFixed(clsObservationPoint)
                        '描画。
                        If clsClickObject.IsInclude(nDeviceLeft, nDeviceTop, nDeviceRight, nDeviceBottom) Then
                            Call DrawObservationPointNSS(hDC, nDeviceLeft, nDeviceTop, nClipLeft, nClipTop, nClipWidth, nClipHeight, nMapScale, clsObservationPoint, m_hPenObspntFixed, m_hBrushNull, Obspnt_Color_Symbol_Fixed, nX, nY, nW, nH)
                            Set clsClickObject = clsClickManager.RegistSymbol(clsObservationPoint, nX, nY, nW, nH)
                        End If
                    Else
                        Call DrawObservationPointNSS(hDC, nDeviceLeft, nDeviceTop, nClipLeft, nClipTop, nClipWidth, nClipHeight, nMapScale, clsObservationPoint, m_hPenObspntFixed, m_hBrushNull, Obspnt_Color_Symbol_Fixed, nX, nY, nW, nH)
                    End If
                End If
                Set clsChainList = clsChainList.NextList
            Loop


        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        'オブジェクトの描画。
        '
        '観測点や基線ベクトル等のオブジェクトを描画する。
        'nClip～は記号を描画するためのクリップ領域。
        '記号はクリップ領域をはみ出さないように描画する。
        'bIsEnable が True の場合、有効なオブジェクトのみ描画する。
        'bIsEnable が False の場合、無効なオブジェクトも描画する。
        'nSwitch は描画するオブジェクトを決定するためのスイッチであり、アプリごとに固有の内容を設定する。
        'nClick に OBJ_TYPE_OBSERVATIONPOINT が指定されている場合、観測点をクリックオブジェクトとして登録する。
        'nClick に OBJ_TYPE_BASELINEVECTOR が指定されている場合、基線ベクトルをクリックオブジェクトとして登録する。
        '
        '引き数：
        'hDC デバイスコンテキストのハンドル。
        'nDeviceLeft 論理エリア上のデバイス左辺位置(ピクセル)。
        'nDeviceTop 論理エリア上のデバイス上辺位置(ピクセル)。
        'nDeviceRight 論理エリア上のデバイス右辺位置(ピクセル)。
        'nDeviceBottom 論理エリア上のデバイス下辺位置(ピクセル)。
        'nClipLeft 論理エリア上のクリップ領域左辺(ピクセル)。
        'nClipTop 論理エリア上のクリップ領域上辺(ピクセル)。
        'nClipWidth 論理エリア上のクリップ領域幅(ピクセル)。
        'nClipHeight 論理エリア上のクリップ領域高さ(ピクセル)。
        'nMapScale Twips→ピクセル、の係数。
        'clsClickManager ClickManager オブジェクト。
        'bArrow 矢印描画の指定。True=描画する。False=描画しない。
        'bIsEnable 有効/無効評価フラグ。
        'nSwitch 評価スイッチ。
        'nClick クリックオブジェクトを登録するオブジェクトの種類を指定する。
        */
#if false
        public void DrawObject(long hDC, double nDeviceLeft, double nDeviceTop, double nDeviceRight, double nDeviceBottom, double nClipLeft, double nClipTop, double nClipWidth,
            double nClipHeight, double nMapScale, ClickManager clsClickManager, bool bArrow, bool bIsEnable, long nSwitch, long nClick)
        {
#else
        public Bitmap DrawObject(Bitmap hDC, double nDeviceLeft, double nDeviceTop, double nDeviceRight, double nDeviceBottom, double nClipLeft, double nClipTop, double nClipWidth,
            double nClipHeight, double nMapScale, ClickManager clsClickManager, bool bArrow, bool bIsEnable, long nSwitch, long nClick)
        {
            Bitmap btmp = hDC;
#endif
            bool rtn;
            //ClickObjectInterface clsClickObject;
            ClickCircle clsClickCircle;
            ClickRect clsClickRect;
            ClickVector clsClickVector;
            ClickTriangle clsClickTriangle;

            bool bClickPoint;
            bool bClickVector;
            bClickPoint = (nClick & OBJ_TYPE_OBSERVATIONPOINT) != 0;
            bClickVector = (nClick & OBJ_TYPE_BASELINEVECTOR) != 0;

            //'観測点、基線ベクトルの描画。
            ChainList clsChainList;
            BaseLineVector clsBaseLineVector;
            ObservationPoint clsObservationPoint;
            double nX = 0;
            double nY = 0;
            double nW = 0;
            double nH = 0;


            //'2006/10/11 NGS Yamada
            bool DuplicationFlag;       //'重複フラグ True=重複,False=重複で無い。
            DuplicationFlag = false;    //'デフォルトはFalse  '2006 / 12 / 12 NGS Yamada


            //'オプションの「観測点のラベル」に合わせて表示項目を変更 2006/10/6 NGS Yamada
            m_PlotPointLabel = m_clsMdlMain.GetDocument().PlotPointLabel;


            //'「無効」なベクトルをプロット画面に表示するか？ 2006/10/10 NGS Yamada
            m_DisableVectorVisible = m_clsMdlMain.GetDocument().DisableVectorVisible;


            //'偏心ベクトル。
            clsChainList = m_clsMdlMain.GetDocument().NetworkModel().IsolatePointHead();
            while (clsChainList != null)
            {
                clsObservationPoint = (ObservationPoint)clsChainList.Element;
                if (clsObservationPoint.Genuine())
                {
#if false
                    //'描画。
                    DrawCorrectVectorMulti(hDC, nDeviceLeft, nDeviceTop, nMapScale, clsObservationPoint, clsObservationPoint.CorrectPoint(), m_hPenVectorEccentric, m_hPenVectorDisable);
#else
                    //'描画。
                    btmp = DrawCorrectVectorMulti(btmp, nDeviceLeft, nDeviceTop, nMapScale, clsObservationPoint, clsObservationPoint.CorrectPoint(), m_hPenVectorEccentric, m_hPenVectorDisable);
#endif
                }
                clsChainList = clsChainList.NextList();
            }


            if (m_DisableVectorVisible) //'「無効」なベクトルをプロット画面に表示するか？ 2006/10/10 NGS Yamada
            {
                //'無効基線ベクトル。
                clsChainList = m_clsMdlMain.GetDocument().NetworkModel().BaseLineVectorHead();
                while (clsChainList != null)
                {
                    clsBaseLineVector = (BaseLineVector)clsChainList.Element;
                    if (!clsBaseLineVector.Enable() && clsBaseLineVector.Valid())
                    {
                        //'クリックオブジェクト登録。
                        if (bClickVector)
                        {
                            clsClickVector = clsClickManager.RegistVector(clsBaseLineVector);
                        }
#if false
                        //'描画。補正ベクトルもあるので無条件に描画する。
                        DrawBaseLineVectorNSS(hDC, nDeviceLeft, nDeviceTop, nMapScale, clsBaseLineVector, m_hPenVectorDisable, m_hPenVectorDisable,
                            bArrow, true, false, m_hPenVectorDisable, m_hPenVectorDisable);
#else
                        //'描画。補正ベクトルもあるので無条件に描画する。
                        btmp = DrawBaseLineVectorNSS(btmp, nDeviceLeft, nDeviceTop, nMapScale, clsBaseLineVector, m_hPenVectorDisable, m_hPenVectorDisable,
                            bArrow, true, false, m_hPenVectorDisable, m_hPenVectorDisable);
#endif
                    }
                    clsChainList = clsChainList.NextList();
                }
            }


            //'無効観測点。
            clsChainList = m_clsMdlMain.GetDocument().NetworkModel().RepresentPointHead();
            while (clsChainList != null)
            {
                clsObservationPoint = (ObservationPoint)clsChainList.Element;
                if (!clsObservationPoint.Enable())
                {
                    if (bClickPoint)
                    {
                        //'クリックオブジェクト登録。
                        if (clsObservationPoint.Fixed())
                        {
                            clsClickTriangle = clsClickManager.RegistFixed(clsObservationPoint);
                            rtn = clsClickTriangle.IsInclude(nDeviceLeft, nDeviceTop, nDeviceRight, nDeviceBottom);
                        }
                        else
                        {
                            clsClickCircle = clsClickManager.RegistPoint(clsObservationPoint);
                            rtn = clsClickCircle.IsInclude(nDeviceLeft, nDeviceTop, nDeviceRight, nDeviceBottom);
                        }
                        //'描画。
                        if (rtn)
                        {
#if false
                            DrawObservationPointNSS(hDC, nDeviceLeft, nDeviceTop, nClipLeft, nClipTop, nClipWidth, nClipHeight, nMapScale, clsObservationPoint,
                                m_hPenObspntDisable, m_hBrushNull, Obspnt_Color_Symbol_Disable, nX, nY, nW, nH);
#else
                            btmp = DrawObservationPointNSS(btmp, nDeviceLeft, nDeviceTop, nClipLeft, nClipTop, nClipWidth, nClipHeight, nMapScale, clsObservationPoint,
                                m_hPenObspntDisable, m_hBrushNull, Obspnt_Color_Symbol_Disable, nX, nY, nW, nH);
#endif
                            clsClickRect = clsClickManager.RegistSymbol(clsObservationPoint, nX, nY, nW, nH);
                        }
                    }
                    else
                    {
#if false
                        DrawObservationPointNSS(hDC, nDeviceLeft, nDeviceTop, nClipLeft, nClipTop, nClipWidth, nClipHeight, nMapScale, clsObservationPoint,
                            m_hPenObspntDisable, m_hBrushNull, Obspnt_Color_Symbol_Disable, nX, nY, nW, nH);
#else
                        btmp = DrawObservationPointNSS(btmp, nDeviceLeft, nDeviceTop, nClipLeft, nClipTop, nClipWidth, nClipHeight, nMapScale, clsObservationPoint,
                            m_hPenObspntDisable, m_hBrushNull, Obspnt_Color_Symbol_Disable, nX, nY, nW, nH);
#endif
                    }
                }
                clsChainList = clsChainList.NextList();
            }


            //'有効基線ベクトル。


            //'最初に重複基線ベクトルを探す。
            Dictionary<string, object> objDuplicationCollection = new Dictionary<string, object>(); //'重複基線ベクトルを判断するためのコレクション。
            Dictionary<string, object> objDuplicationVectors = new Dictionary<string, object>(); //'探し当てた重複基線ベクトルを記憶するためのコレクション。
            BaseLineVector clsDuplicationVector = null;
            object objDuplicationVector = null;
            string sKey;
            clsChainList = m_clsMdlMain.GetDocument().NetworkModel().BaseLineVectorHead();
            int i1 = 0;
            while (clsChainList != null)
            {
                clsBaseLineVector = (BaseLineVector)clsChainList.Element;
                //'Valid だけ評価しておく。→Enableも追加（有効-無効は重複ではないとする）Yamada
                if (clsBaseLineVector.Enable() && clsBaseLineVector.Valid())
                {
                    //'重複を判断するためのキー。
                    sKey = GetDuplicationKey(clsBaseLineVector);
                    //'重複の判断。
                    objDuplicationVector = clsDuplicationVector;
                    if (!LookupCollectionObject(objDuplicationCollection, ref objDuplicationVector, sKey))
                    {
                        //'1本目の基線ベクトルである。
                        //'objDuplicationCollection コレクションに1本目のオブジェクトを設定する。
                        objDuplicationCollection.Add(sKey, objDuplicationVector);
                    }
                    else
                    {
                        //'2本目以降の基線ベクトルである。
                        //'見つかった重複基線ベクトルは objDuplicationVectors コレクションに追加する。
                        clsDuplicationVector = (BaseLineVector)objDuplicationVector;
                        objDuplicationVectors.Add(Convert.ToString(GetPointer(clsBaseLineVector)), clsBaseLineVector);
                        //'1本目も objDuplicationVectors コレクションに追加しなければならない。
                        if (clsDuplicationVector == null)
                        {
                            //'1本目はすでに追加されている。
                        }
                        else
                        {
                            //'1本目の重複基線ベクトルも objDuplicationVectors コレクションに追加する。
                            objDuplicationVectors.Add(Convert.ToString(GetPointer(clsDuplicationVector)), clsDuplicationVector);
                            //'1本目を objDuplicationVectors コレクションに追加したので、その印として objDuplicationCollection の内容を Nothing に置き換える。
                            objDuplicationCollection.Remove(sKey);
                            objDuplicationCollection.Add(null, sKey);
                        }
                    }
                }
                clsChainList = clsChainList.NextList();
                i1++;
            }
            clsDuplicationVector = (BaseLineVector)objDuplicationVector;

            long hPenLine;
            long hPenArrow;
            clsChainList = m_clsMdlMain.GetDocument().NetworkModel().BaseLineVectorHead();
            int i2 = 0;
            while (clsChainList != null)
            {
                clsBaseLineVector = (BaseLineVector)clsChainList.Element;
                if (clsBaseLineVector.Enable() && clsBaseLineVector.Valid())
                {
                    //'重複しているか？//'2006 / 10 / 11 NGS Yamada
                    objDuplicationVector = clsDuplicationVector;
                    if (LookupCollectionObject(objDuplicationVectors, ref objDuplicationVector, Convert.ToString(GetPointer(clsBaseLineVector))))
                    {
                        DuplicationFlag = true;
                    }
                    else
                    {
                        DuplicationFlag = false;
                    }
                    if (clsBaseLineVector.Analysis == ANALYSIS_STATUS.ANALYSIS_STATUS_FIX)
                    {
                        //'                Debug.Print "MinRatio = " & clsBaseLineVector.MinRatio & " : Ratio = " & clsBaseLineVector.Bias
                        //'バイアス決定比によってベクトルの色を変える　2006/10/12 NGS Yamada
                        if (clsBaseLineVector.Bias >= clsBaseLineVector.MinRatio)
                        {
                            hPenLine = m_hPenVectorFix;
                            hPenArrow = m_hPenVectorFix;
                        }
                        else
                        {
                            hPenLine = m_hPenVectorFix_LowRatio;
                            hPenArrow = m_hPenVectorFix_LowRatio;
                        }
                    }
                    else if (clsBaseLineVector.Analysis == ANALYSIS_STATUS.ANALYSIS_STATUS_FLOAT)
                    {
                        hPenLine = m_hPenVectorFloat;
                        hPenArrow = m_hPenVectorFloat;
                    }
                    else if (clsBaseLineVector.Analysis == ANALYSIS_STATUS.ANALYSIS_STATUS_FAILED)
                    {
                        hPenLine = m_hPenVectorFailed;
                        hPenArrow = m_hPenVectorFailed;
                    }
                    else
                    {
                        hPenLine = m_hPenVectorEnable;
                        hPenArrow = m_hPenVectorEnable;
                    }
                    //'重複時の基線ベクトルは強調線から２重線に変更したため、重複によって線種を分ける処理は削除 2006/10/12 NGS Yamada

                    //'            //'重複しているか？
                    //'            If Not LookupCollectionObject(objDuplicationVectors, clsDuplicationVector, Hex$(GetPointer(clsBaseLineVector))) Then
                    //'                DuplicationFlag = False //'2006 / 10 / 11 NGS Yamada
                    //'                If clsBaseLineVector.Analysis = ANALYSIS_STATUS_FIX Then
                    //'                    Debug.Print "MinRatio = " & clsBaseLineVector.MinRatio & " : Ratio = " & clsBaseLineVector.Bias
                    //'                    hPenLine = m_hPenVectorFix
                    //'                    hPenArrow = m_hPenVectorFix
                    //'                ElseIf clsBaseLineVector.Analysis = ANALYSIS_STATUS_FLOAT Then
                    //'                    hPenLine = m_hPenVectorFloat
                    //'                    hPenArrow = m_hPenVectorFloat
                    //'                ElseIf clsBaseLineVector.Analysis = ANALYSIS_STATUS_FAILED Then
                    //'                    hPenLine = m_hPenVectorFailed
                    //'                    hPenArrow = m_hPenVectorFailed
                    //'                Else
                    //'                    hPenLine = m_hPenVectorEnable
                    //'                    hPenArrow = m_hPenVectorEnable
                    //'                End If
                    //'            Else
                    //'                DuplicationFlag = True //'2006 / 10 / 11 NGS Yamada
                    //'                If clsBaseLineVector.Analysis = ANALYSIS_STATUS_FIX Then
                    //'                    hPenLine = m_hPenVectorDuplicationFix
                    //'                    hPenArrow = m_hPenVectorFix
                    //'                ElseIf clsBaseLineVector.Analysis = ANALYSIS_STATUS_FLOAT Then
                    //'                    hPenLine = m_hPenVectorDuplicationFloat
                    //'                    hPenArrow = m_hPenVectorFloat
                    //'                ElseIf clsBaseLineVector.Analysis = ANALYSIS_STATUS_FAILED Then
                    //'                    hPenLine = m_hPenVectorDuplicationFailed
                    //'                    hPenArrow = m_hPenVectorFailed
                    //'                Else
                    //'                    hPenLine = m_hPenVectorDuplication
                    //'                    hPenArrow = m_hPenVectorEnable
                    //'                End If
                    //'            End If
                    //'クリックオブジェクト登録。
                    if (bClickVector)
                    {
                        clsClickVector = clsClickManager.RegistVector(clsBaseLineVector);
                    }
#if false
                    //'描画。補正ベクトルもあるので無条件に描画する。
                    DrawBaseLineVectorNSS(hDC, nDeviceLeft, nDeviceTop, nMapScale, clsBaseLineVector, hPenLine, hPenArrow, bArrow, true, DuplicationFlag,
                        m_hPenVectorCorrect, m_hPenVectorDisable);
#else
                    //'描画。補正ベクトルもあるので無条件に描画する。
                    btmp = DrawBaseLineVectorNSS(btmp, nDeviceLeft, nDeviceTop, nMapScale, clsBaseLineVector, hPenLine, hPenArrow, bArrow, true, DuplicationFlag,
                        m_hPenVectorCorrect, m_hPenVectorDisable);
#endif
                }
                clsChainList = clsChainList.NextList();
                i2++;
            }


            //'有効観測点。
            clsChainList = m_clsMdlMain.GetDocument().NetworkModel().RepresentPointHead();
            while (clsChainList != null)
            {
                clsObservationPoint = (ObservationPoint)clsChainList.Element;
                if (clsObservationPoint.Enable() && (!clsObservationPoint.Fixed()))
                {
                    if (bClickPoint)
                    {
                        //'クリックオブジェクト登録。
                        clsClickCircle = clsClickManager.RegistPoint(clsObservationPoint);
                        //'描画。
                        if (clsClickCircle.IsInclude(nDeviceLeft, nDeviceTop, nDeviceRight, nDeviceBottom))
                        {
#if false
                            DrawObservationPointNSS(hDC, nDeviceLeft, nDeviceTop, nClipLeft, nClipTop, nClipWidth, nClipHeight, nMapScale, clsObservationPoint,
                                m_hPenObspntEnable, m_hBrushNull, Obspnt_Color_Symbol_Enable, nX, nY, nW, nH);
#else
                            btmp = DrawObservationPointNSS(btmp, nDeviceLeft, nDeviceTop, nClipLeft, nClipTop, nClipWidth, nClipHeight, nMapScale, clsObservationPoint,
                                m_hPenObspntEnable, m_hBrushNull, Obspnt_Color_Symbol_Enable, nX, nY, nW, nH);
#endif
                            clsClickRect = clsClickManager.RegistSymbol(clsObservationPoint, nX, nY, nW, nH);
                        }
                    }
                    else
                    {
#if false
                        DrawObservationPointNSS(hDC, nDeviceLeft, nDeviceTop, nClipLeft, nClipTop, nClipWidth, nClipHeight, nMapScale, clsObservationPoint,
                            m_hPenObspntEnable, m_hBrushNull, Obspnt_Color_Symbol_Enable, nX, nY, nW, nH);
#else
                        btmp = DrawObservationPointNSS(btmp, nDeviceLeft, nDeviceTop, nClipLeft, nClipTop, nClipWidth, nClipHeight, nMapScale, clsObservationPoint,
                            m_hPenObspntEnable, m_hBrushNull, Obspnt_Color_Symbol_Enable, nX, nY, nW, nH);
#endif
                    }
                }
                clsChainList = clsChainList.NextList();
            }

            //'固定観測点。
            clsChainList = m_clsMdlMain.GetDocument().NetworkModel().RepresentPointHead();
            while (clsChainList != null)
            {
                clsObservationPoint = (ObservationPoint)clsChainList.Element;
                if (clsObservationPoint.Enable() && clsObservationPoint.Fixed())
                {
                    if (bClickPoint)
                    {
                        //'クリックオブジェクト登録。
                        clsClickTriangle = clsClickManager.RegistFixed(clsObservationPoint);
                        //'描画。
                        if (clsClickTriangle.IsInclude(nDeviceLeft, nDeviceTop, nDeviceRight, nDeviceBottom))
                        {
#if false
                            DrawObservationPointNSS(hDC, nDeviceLeft, nDeviceTop, nClipLeft, nClipTop, nClipWidth, nClipHeight, nMapScale, clsObservationPoint,
                                m_hPenObspntFixed, m_hBrushNull, Obspnt_Color_Symbol_Fixed, nX, nY, nW, nH);
#else
                            btmp = DrawObservationPointNSS(btmp, nDeviceLeft, nDeviceTop, nClipLeft, nClipTop, nClipWidth, nClipHeight, nMapScale, clsObservationPoint,
                                m_hPenObspntFixed, m_hBrushNull, Obspnt_Color_Symbol_Fixed, nX, nY, nW, nH);
#endif
                            clsClickRect = clsClickManager.RegistSymbol(clsObservationPoint, nX, nY, nW, nH);
                        }
                    }
                    else
                    {
#if false
                        DrawObservationPointNSS(hDC, nDeviceLeft, nDeviceTop, nClipLeft, nClipTop, nClipWidth, nClipHeight, nMapScale, clsObservationPoint,
                            m_hPenObspntFixed, m_hBrushNull, Obspnt_Color_Symbol_Fixed, nX, nY, nW, nH);
#else
                        btmp = DrawObservationPointNSS(btmp, nDeviceLeft, nDeviceTop, nClipLeft, nClipTop, nClipWidth, nClipHeight, nMapScale, clsObservationPoint,
                            m_hPenObspntFixed, m_hBrushNull, Obspnt_Color_Symbol_Fixed, nX, nY, nW, nH);
#endif
                    }
                }
                clsChainList = clsChainList.NextList();
            }
#if false
            return;
#else
            return btmp;
#endif
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '候補ポップアップメニューコマンド文字列。
        '
        '閉合画面の配置図で、基線ベクトルが重なっている場合にポップアップさせるメニューに表示する項目の文字列を取得する。
        '
        '引き数：
        'clsBaseLineVector 基線ベクトル。
        Public Function GetCandidateCommandString(ByVal clsBaseLineVector As BaseLineVector)
            GetCandidateCommandString = clsBaseLineVector.Session & " " & clsBaseLineVector.StrPoint.Number & "-" & clsBaseLineVector.EndPoint.Number & vbTab & DISP_ANALYSIS(clsBaseLineVector.Analysis)
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '候補ポップアップメニューコマンド文字列。
        '
        '閉合画面の配置図で、基線ベクトルが重なっている場合にポップアップさせるメニューに表示する項目の文字列を取得する。
        '
        '引き数：
        'clsBaseLineVector 基線ベクトル。
        */
        public string GetCandidateCommandString(BaseLineVector clsBaseLineVector)
        {
//            GENBA_STRUCT_S Genba_S = new GENBA_STRUCT_S();
            return clsBaseLineVector.Session + " " + clsBaseLineVector.StrPoint().Number() + "-" +
                            clsBaseLineVector.EndPoint().Number() + "\t" + DISP_ANALYSIS[(int)clsBaseLineVector.Analysis];
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '*******************************************************************************
        'インプリメンテーション

        '基線ベクトルを描画する。
        '
        '引き数：
        'hDC デバイスコンテキストのハンドル。
        'nDeviceLeft 論理エリア上のデバイス左辺位置(ピクセル)。
        'nDeviceTop 論理エリア上のデバイス上辺位置(ピクセル)。
        'nMapScale Twips→ピクセル、の係数。
        'clsBaseLineVector 基線ベクトル。
        'hPenLine 線のペン。
        'hPenArrow 矢印のペン。
        'bArrow 矢印描画の指定。True=描画する。False=描画しない。
        'bCorrect 補正フラグ。True=基線ベクトルの始点終点のどちらか、または両方が偏心点である場合、偏心補正後の基線ベクトルも描画する。False=偏心補正後の基線ベクトルは描画しない。
        'bDuplication 重複基線フラグ True=重複基線
        'hPenCorrectEnable 有効な偏心補正後の基線ベクトルの線のペン。
        'hPenCorrectDisable 無効な偏心補正後の基線ベクトルの線のペン。
        Private Sub DrawBaseLineVectorNSS(ByVal hDC As Long, ByVal nDeviceLeft As Double, ByVal nDeviceTop As Double, ByVal nMapScale As Double, ByVal clsBaseLineVector As BaseLineVector, ByVal hPenLine As Long, ByVal hPenArrow As Long, ByVal bArrow As Boolean, ByVal bCorrect As Boolean, ByVal bDuplication As Boolean, Optional ByVal hPenCorrectEnable As Long, Optional ByVal hPenCorrectDisable As Long)

            If bCorrect Then
                '偏心点である場合、偏心補正後の基線ベクトルも描画する。
                Dim bCorrectDraw As Boolean
                Dim clsStrPoint As ObservationPoint
                If clsBaseLineVector.StrPoint.EnableEccentric Then
                    Set clsStrPoint = clsBaseLineVector.StrPoint.CorrectPoint
                    bCorrectDraw = True
                Else
                    Set clsStrPoint = clsBaseLineVector.StrPoint
                End If
                Dim clsEndPoint As ObservationPoint
                If clsBaseLineVector.EndPoint.EnableEccentric Then
                    Set clsEndPoint = clsBaseLineVector.EndPoint.CorrectPoint
                    bCorrectDraw = True
                Else
                    Set clsEndPoint = clsBaseLineVector.EndPoint
                End If
                If bCorrectDraw Then
                    '基線ベクトルが無効である場合、補正ベクトルも無効。
                    Dim hPen As Long
                    If clsBaseLineVector.Enable Then
                        hPen = hPenCorrectEnable
                    Else
                        hPen = hPenCorrectDisable
                    End If
                    '補正ベクトルの描画。
                    Call DrawCorrectVector(hDC, nDeviceLeft, nDeviceTop, nMapScale, clsStrPoint, clsEndPoint, hPenCorrectEnable, hPenCorrectDisable)
                End If
            End If


            '基線ベクトルを描画する。
            If bDuplication Then
                Call DrawBaseLineVectorDuplication(hDC, nDeviceLeft, nDeviceTop, nMapScale, clsBaseLineVector, hPenLine, hPenArrow, bArrow)
            Else
                Call DrawBaseLineVector(hDC, nDeviceLeft, nDeviceTop, nMapScale, clsBaseLineVector, hPenLine, hPenArrow, bArrow)
            End If
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '*******************************************************************************
        'インプリメンテーション

        '基線ベクトルを描画する。
        '
        '引き数：
        'hDC デバイスコンテキストのハンドル。
        'nDeviceLeft 論理エリア上のデバイス左辺位置(ピクセル)。
        'nDeviceTop 論理エリア上のデバイス上辺位置(ピクセル)。
        'nMapScale Twips→ピクセル、の係数。
        'clsBaseLineVector 基線ベクトル。
        'hPenLine 線のペン。
        'hPenArrow 矢印のペン。
        'bArrow 矢印描画の指定。True=描画する。False=描画しない。
        'bCorrect 補正フラグ。True=基線ベクトルの始点終点のどちらか、または両方が偏心点である場合、偏心補正後の基線ベクトルも描画する。False=偏心補正後の基線ベクトルは描画しない。
        'bDuplication 重複基線フラグ True=重複基線
        'hPenCorrectEnable 有効な偏心補正後の基線ベクトルの線のペン。
        'hPenCorrectDisable 無効な偏心補正後の基線ベクトルの線のペン。
        */
#if false
        private void DrawBaseLineVectorNSS(long hDC, double nDeviceLeft, double nDeviceTop, double nMapScale, BaseLineVector clsBaseLineVector,
                        long hPenLine, long hPenArrow, bool bArrow, bool bCorrect, bool bDuplication, long hPenCorrectEnable = 0, long hPenCorrectDisable = 0)
        {
#else
        private Bitmap DrawBaseLineVectorNSS(Bitmap hDC, double nDeviceLeft, double nDeviceTop, double nMapScale, BaseLineVector clsBaseLineVector,
                        long hPenLine, long hPenArrow, bool bArrow, bool bCorrect, bool bDuplication, long hPenCorrectEnable = 0, long hPenCorrectDisable = 0)
        {
            Bitmap btmp = hDC;
#endif
            if (bCorrect)
            {
                //'偏心点である場合、偏心補正後の基線ベクトルも描画する。
                bool bCorrectDraw = false;
                ObservationPoint clsStrPoint;
                if (clsBaseLineVector.StrPoint().EnableEccentric())
                {
                    clsStrPoint = clsBaseLineVector.StrPoint().CorrectPoint();
                    bCorrectDraw = true;
                }
                else
                {
                    clsStrPoint = clsBaseLineVector.StrPoint();
                }
                ObservationPoint clsEndPoint;
                if (clsBaseLineVector.EndPoint().EnableEccentric())
                {
                    clsEndPoint = clsBaseLineVector.EndPoint().CorrectPoint();
                    bCorrectDraw = true;
                }
                else
                {
                    clsEndPoint = clsBaseLineVector.EndPoint();
                }
                if (bCorrectDraw)
                {
                    //'基線ベクトルが無効である場合、補正ベクトルも無効。
                    long hPen;
                    if (clsBaseLineVector.Enable())
                    {
                        hPen = hPenCorrectEnable;
                    }
                    else
                    {
                        hPen = hPenCorrectDisable;
                    }
                    //'補正ベクトルの描画。
#if false
                    DrawCorrectVector(hDC, nDeviceLeft, nDeviceTop, nMapScale, clsStrPoint, clsEndPoint, hPenCorrectEnable, hPenCorrectDisable);
#else
                    btmp = DrawCorrectVector(btmp, nDeviceLeft, nDeviceTop, nMapScale, clsStrPoint, clsEndPoint, hPenCorrectEnable, hPenCorrectDisable);
#endif
                }
            }

            //'基線ベクトルを描画する。
            if (bDuplication)
            {
#if false
                DrawBaseLineVectorDuplication(hDC, nDeviceLeft, nDeviceTop, nMapScale, clsBaseLineVector, hPenLine, hPenArrow, bArrow);
#else
                btmp = DrawBaseLineVectorDuplication(btmp, nDeviceLeft, nDeviceTop, nMapScale, clsBaseLineVector, hPenLine, hPenArrow, bArrow);
#endif
            }
            else
            {
#if false
                m_clsMdlPlotProc.DrawBaseLineVector(hDC, nDeviceLeft, nDeviceTop, nMapScale, clsBaseLineVector, hPenLine, hPenArrow, bArrow);
#else
                btmp = m_clsMdlPlotProc.DrawBaseLineVector(btmp, nDeviceLeft, nDeviceTop, nMapScale, clsBaseLineVector, hPenLine, hPenArrow, bArrow);
#endif
            }
#if false
            return;
#else
            return btmp;
#endif
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '偏心補正基線ベクトルを描画する。
        '
        '引き数：
        'hDC デバイスコンテキストのハンドル。
        'nDeviceLeft 論理エリア上のデバイス左辺位置(ピクセル)。
        'nDeviceTop 論理エリア上のデバイス上辺位置(ピクセル)。
        'nMapScale Twips→ピクセル、の係数。
        'clsStrPoint 始点(接合観測点、ただし本点の場合は実観測点)。
        'clsEndPoint 終点(接合観測点、ただし本点の場合は実観測点)。
        'hPenEnable 有効な線のペン。
        'hPenDisable 無効な線のペン。
        Private Sub DrawCorrectVector(ByVal hDC As Long, ByVal nDeviceLeft As Double, ByVal nDeviceTop As Double, ByVal nMapScale As Double, ByVal clsStrPoint As ObservationPoint, ByVal clsEndPoint As ObservationPoint, ByVal hPenEnable As Long, ByVal hPenDisable As Long)

            '方向余弦を求める
            Dim clsStrDevicePoint As TwipsPoint
            Dim clsEndDevicePoint As TwipsPoint
            Set clsStrDevicePoint = clsStrPoint.DevicePoint
            Set clsEndDevicePoint = clsEndPoint.DevicePoint


            Dim nStrRadius As Double
            Dim nEndRadius As Double
            If clsStrPoint.Genuine Then
                nStrRadius = PLOT_GENUINE_RADIUS * nMapScale
            Else
                nStrRadius = PLOT_OBSPNT_RADIUS * nMapScale
            End If
            If clsEndPoint.Genuine Then
                nEndRadius = PLOT_GENUINE_RADIUS * nMapScale
            Else
                nEndRadius = PLOT_OBSPNT_RADIUS * nMapScale
            End If


            Dim L As Double, M As Double
            If Not GetDirectionCosines(clsStrDevicePoint, clsEndDevicePoint, L, M) Then Exit Sub


            Dim xp1 As Double, yp1 As Double, xp2 As Double, yp2 As Double
            If clsStrPoint.Fixed Then
                'P1(三角との交点)を求める
                Call GetTriangleIntersection(M, L, xp1, yp1, PLOT_OBSPNT_RADIUS_FIX * nMapScale)
            Else
                'P1(X1の円周点)を求める
                xp1 = nStrRadius * L
                yp1 = nStrRadius * M
            End If
            xp1 = xp1 + clsStrDevicePoint.X
            yp1 = yp1 + clsStrDevicePoint.Y
            If clsEndPoint.Fixed Then
                'P2(三角との交点)を求める
                Call GetTriangleIntersection(-M, -L, xp2, yp2, PLOT_OBSPNT_RADIUS_FIX * nMapScale)
            Else
                'P2(X2の円周点)を求める
                xp2 = nEndRadius * -L
                yp2 = nEndRadius * -M
            End If
            xp2 = xp2 + clsEndDevicePoint.X
            yp2 = yp2 + clsEndDevicePoint.Y


            '本点が無効である場合、無効色。
            Dim hPen As Long
            hPen = hPenEnable
            If clsStrPoint.Genuine Then
                If Not clsStrPoint.Enable Then hPen = hPenDisable
            End If
            If clsEndPoint.Genuine Then
                If Not clsEndPoint.Enable Then hPen = hPenDisable
            End If


            Call SelectObject(hDC, hPen)
            Dim tPoint As POINT
            Call MoveToEx(hDC, xp1 - nDeviceLeft, yp1 - nDeviceTop, tPoint)
            Call LineTo(hDC, xp2 - nDeviceLeft, yp2 - nDeviceTop)


        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '偏心補正基線ベクトルを描画する。
        '
        '引き数：
        'hDC デバイスコンテキストのハンドル。
        'nDeviceLeft 論理エリア上のデバイス左辺位置(ピクセル)。
        'nDeviceTop 論理エリア上のデバイス上辺位置(ピクセル)。
        'nMapScale Twips→ピクセル、の係数。
        'clsStrPoint 始点(接合観測点、ただし本点の場合は実観測点)。
        'clsEndPoint 終点(接合観測点、ただし本点の場合は実観測点)。
        'hPenEnable 有効な線のペン。
        'hPenDisable 無効な線のペン。
        */
#if false
        private void DrawCorrectVector(long hDC, double nDeviceLeft, double nDeviceTop, double nMapScale, ObservationPoint clsStrPoint, ObservationPoint clsEndPoint,
            long hPenEnable, long hPenDisable)
        {
#else
        private Bitmap DrawCorrectVector(Bitmap hDC, double nDeviceLeft, double nDeviceTop, double nMapScale, ObservationPoint clsStrPoint, ObservationPoint clsEndPoint,
            long hPenEnable, long hPenDisable)
        {
            Bitmap btmp = hDC;
            int L_SOLID = (int)(hPenEnable >> 56);
            int L_Width = (int)((hPenEnable >> 32) & 0xFFFFFF);
            int L_RGB = (int)(hPenEnable & 0xFFFFFF);
#endif
            //'方向余弦を求める
            TwipsPoint clsStrDevicePoint;
            TwipsPoint clsEndDevicePoint;
            clsStrDevicePoint = clsStrPoint.DevicePoint();
            clsEndDevicePoint = clsEndPoint.DevicePoint();

            double nStrRadius = 0;
            double nEndRadius = 0;
            if (clsStrPoint.Genuine())
            {
                nStrRadius = PLOT_GENUINE_RADIUS * nMapScale;
            }
            else
            {
                nStrRadius = PLOT_OBSPNT_RADIUS * nMapScale;
            }
            if (clsEndPoint.Genuine())
            {
                nEndRadius = PLOT_GENUINE_RADIUS * nMapScale;
            }
            else
            {
                nEndRadius = PLOT_OBSPNT_RADIUS * nMapScale;
            }


            double L = 0;
            double M = 0;
            if (!m_clsMdlPlotProc.GetDirectionCosines(clsStrDevicePoint, clsEndDevicePoint, ref L, ref M))
            {
#if false
                return;
#else
                return btmp;
#endif
            }

            double xp1 = 0;
            double yp1 = 0;
            double xp2 = 0;
            double yp2 = 0;
            if (clsStrPoint.Fixed())
            {
                //'P1(三角との交点)を求める
                m_clsMdlPlotProc.GetTriangleIntersection(M, L, ref xp1, ref yp1, (long)(PLOT_OBSPNT_RADIUS_FIX * nMapScale));
            }
            else
            {
                //'P1(X1の円周点)を求める
                xp1 = nStrRadius * L;
                yp1 = nStrRadius * M;
            }
            xp1 = xp1 + clsStrDevicePoint.X;
            yp1 = yp1 + clsStrDevicePoint.Y;
            if (clsEndPoint.Fixed())
            {
                //'P2(三角との交点)を求める
                m_clsMdlPlotProc.GetTriangleIntersection(-M, -L, ref xp2, ref yp2, (long)(PLOT_OBSPNT_RADIUS_FIX * nMapScale));
            }
            else
            {
                //'P2(X2の円周点)を求める
                xp2 = nEndRadius * -L;
                yp2 = nEndRadius * -M;
            }
            xp2 = xp2 + clsEndDevicePoint.X;
            yp2 = yp2 + clsEndDevicePoint.Y;


            //'本点が無効である場合、無効色。
            long hPen;
            hPen = hPenEnable;
            if (clsStrPoint.Genuine())
            {
                if (!clsStrPoint.Enable())
                {
                    hPen = hPenDisable;
                }
            }
            if (clsEndPoint.Genuine())
            {
                if (!clsEndPoint.Enable())
                {
                    hPen = hPenDisable;
                }
            }

#if false
            _ =SelectObject((IntPtr)hDC, (int)hPen);
            POINT tPoint = default;
            _ = MoveToEx((IntPtr)hDC, (int)(xp1 - nDeviceLeft), (int)(yp1 - nDeviceTop), ref tPoint);
            _ = LineTo((IntPtr)hDC, (int)(xp2 - nDeviceLeft), (int)(yp2 - nDeviceTop));
            return;
#else
            //１.Bitmapクラスを作成(大きさはPictureBoxのサイズ)
            //Bitmap btmp = (Bitmap)hDC;
            //２.Graphicクラスを生成する(先ほどのBitmap(Imageオフジェクト)クラスを使う)
            Graphics grph = Graphics.FromImage(btmp);
            //３.Penオブジェクト生成(今回は白の太さ２)
            //Color Clr = Color.White;
            //Pen pen = new Pen(Clr, 2);
            Color Clr = Color.FromArgb((int)(L_RGB | 0xFF000000));
            Pen pen = new Pen(Clr, L_Width);
            //４.SmoothingModeを設定
            //(高品質で低速なレンダリング)を指定する
            grph.SmoothingMode = SmoothingMode.HighQuality;
            //５.線を引く
            grph.DrawLine(pen, new Point((int)(xp1 - nDeviceLeft), (int)(yp1 - nDeviceTop)), new Point((int)(xp2 - nDeviceLeft), (int)(yp2 - nDeviceTop)));
            //６.解放する
            pen.Dispose();
            grph.Dispose();
            //７.表示を渡す
            return btmp;
#endif
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '偏心ベクトルを描画する。
        '
        '2重線のベクトルを描画する。
        '
        '引き数：
        'hDC デバイスコンテキストのハンドル。
        'nDeviceLeft 論理エリア上のデバイス左辺位置(ピクセル)。
        'nDeviceTop 論理エリア上のデバイス上辺位置(ピクセル)。
        'nMapScale Twips→ピクセル、の係数。
        'clsStrPoint 始点(本点の場合は実観測点、偏心点の場合は HeadPoint)。
        'clsEndPoint 終点(本点の場合は実観測点、偏心点の場合は HeadPoint)。
        'hPenEnable 有効な線のペン。
        'hPenDisable 無効な線のペン。
        Private Sub DrawCorrectVectorMulti(ByVal hDC As Long, ByVal nDeviceLeft As Double, ByVal nDeviceTop As Double, ByVal nMapScale As Double, ByVal clsStrPoint As ObservationPoint, ByVal clsEndPoint As ObservationPoint, ByVal hPenEnable As Long, ByVal hPenDisable As Long)

            '定数。
            Dim nPLOT_OBSPNT_RADIUS_FIX As Double
            Dim nMULTI_WIDTH As Double
            nPLOT_OBSPNT_RADIUS_FIX = PLOT_OBSPNT_RADIUS_FIX * nMapScale
            nMULTI_WIDTH = MULTI_WIDTH * nMapScale


            '本点が無効である場合、無効色。
            Dim hPen As Long
            hPen = hPenEnable
            If clsStrPoint.Genuine Then
                If Not clsStrPoint.Enable Then hPen = hPenDisable
            End If
            If clsEndPoint.Genuine Then
                If Not clsEndPoint.Enable Then hPen = hPenDisable
            End If


            '方向余弦を求める
            Dim clsStrDevicePoint As TwipsPoint
            Dim clsEndDevicePoint As TwipsPoint
            Set clsStrDevicePoint = clsStrPoint.DevicePoint
            Set clsEndDevicePoint = clsEndPoint.DevicePoint


            Dim nStrRadius As Double
            Dim nEndRadius As Double
            If clsStrPoint.Genuine Then
                nStrRadius = PLOT_GENUINE_RADIUS * nMapScale
            Else
                nStrRadius = PLOT_OBSPNT_RADIUS * nMapScale
            End If
            If clsEndPoint.Genuine Then
                nEndRadius = PLOT_GENUINE_RADIUS * nMapScale
            Else
                nEndRadius = PLOT_OBSPNT_RADIUS * nMapScale
            End If


            Dim L As Double, M As Double
            If Not GetDirectionCosines(clsStrDevicePoint, clsEndDevicePoint, L, M) Then Exit Sub


            Dim xp3s As Double, yp3s As Double, xp4s As Double, yp4s As Double
            Dim xp3e As Double, yp3e As Double, xp4e As Double, yp4e As Double
            Dim xm As Double, ym As Double
            Dim nL As Double
            If clsStrPoint.Fixed Then
                '三角との交点を求める
                Call GetTriangleIntersectionMulti(M, L, xp3s, yp3s, nPLOT_OBSPNT_RADIUS_FIX, -nMULTI_WIDTH)
                Call GetTriangleIntersectionMulti(M, L, xp4s, yp4s, nPLOT_OBSPNT_RADIUS_FIX, nMULTI_WIDTH)
                xp3s = xp3s + clsStrDevicePoint.X
                yp3s = yp3s + clsStrDevicePoint.Y
                xp4s = xp4s + clsStrDevicePoint.X
                yp4s = yp4s + clsStrDevicePoint.Y
            Else
                '円周点を求める
                nL = Sqr(nStrRadius ^ 2 + nMULTI_WIDTH ^ 2)
                xm = clsStrDevicePoint.X + nL * L
                xp3s = xm + nMULTI_WIDTH * M
                xp4s = xm - nMULTI_WIDTH * M
                ym = clsStrDevicePoint.Y + nL * M
                yp3s = ym - nMULTI_WIDTH * L
                yp4s = ym + nMULTI_WIDTH * L
            End If
            If clsEndPoint.Fixed Then
                '三角との交点を求める
                Call GetTriangleIntersectionMulti(-M, -L, xp3e, yp3e, nPLOT_OBSPNT_RADIUS_FIX, nMULTI_WIDTH)
                Call GetTriangleIntersectionMulti(-M, -L, xp4e, yp4e, nPLOT_OBSPNT_RADIUS_FIX, -nMULTI_WIDTH)
                xp3e = xp3e + clsEndDevicePoint.X
                yp3e = yp3e + clsEndDevicePoint.Y
                xp4e = xp4e + clsEndDevicePoint.X
                yp4e = yp4e + clsEndDevicePoint.Y
            Else
                '円周点を求める
                nL = Sqr(nEndRadius ^ 2 + nMULTI_WIDTH ^ 2)
                xm = clsEndDevicePoint.X - nL * L
                xp3e = xm + nMULTI_WIDTH * M
                xp4e = xm - nMULTI_WIDTH * M
                ym = clsEndDevicePoint.Y - nL * M
                yp3e = ym - nMULTI_WIDTH * L
                yp4e = ym + nMULTI_WIDTH * L
            End If


            Call SelectObject(hDC, hPen)
            Dim tPoint As POINT
            Call MoveToEx(hDC, xp3s - nDeviceLeft, yp3s - nDeviceTop, tPoint)
            Call LineTo(hDC, xp3e - nDeviceLeft, yp3e - nDeviceTop)
            Call MoveToEx(hDC, xp4s - nDeviceLeft, yp4s - nDeviceTop, tPoint)
            Call LineTo(hDC, xp4e - nDeviceLeft, yp4e - nDeviceTop)


        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '偏心ベクトルを描画する。
        '
        '2重線のベクトルを描画する。
        '
        '引き数：
        'hDC デバイスコンテキストのハンドル。
        'nDeviceLeft 論理エリア上のデバイス左辺位置(ピクセル)。
        'nDeviceTop 論理エリア上のデバイス上辺位置(ピクセル)。
        'nMapScale Twips→ピクセル、の係数。
        'clsStrPoint 始点(本点の場合は実観測点、偏心点の場合は HeadPoint)。
        'clsEndPoint 終点(本点の場合は実観測点、偏心点の場合は HeadPoint)。
        'hPenEnable 有効な線のペン。
        'hPenDisable 無効な線のペン。
        */
#if false
        private void DrawCorrectVectorMulti(long hDC, double nDeviceLeft, double nDeviceTop, double nMapScale, ObservationPoint clsStrPoint, ObservationPoint clsEndPoint,
            long hPenEnable, long hPenDisable)
        {
#else
        private Bitmap DrawCorrectVectorMulti(Bitmap hDC, double nDeviceLeft, double nDeviceTop, double nMapScale, ObservationPoint clsStrPoint, ObservationPoint clsEndPoint,
            long hPenEnable, long hPenDisable)
        {
            Bitmap btmp = hDC;
            int L_SOLID = (int)(hPenEnable >> 56);
            int L_Width = (int)((hPenEnable >> 32) & 0xFFFFFF);
            int L_RGB = (int)(hPenEnable & 0xFFFFFF);
#endif
            //'定数。
            double nPLOT_OBSPNT_RADIUS_FIX = 0;
            double nMULTI_WIDTH = 0;
            nPLOT_OBSPNT_RADIUS_FIX = PLOT_OBSPNT_RADIUS_FIX * nMapScale;
            nMULTI_WIDTH = MULTI_WIDTH * nMapScale;

            //  long Sqrt1;
            //  long Sqrt2;
            //  double Sqrt3;
            //  double Sqrt4;
            //  double Sqrt5;
            //  double Sqrt6;


            //'本点が無効である場合、無効色。
            long hPen = 0;
            hPen = hPenEnable;
            if (clsStrPoint.Genuine())
            {
                if (!clsStrPoint.Enable())
                {
                    hPen = hPenDisable;
                }
            }
            if (clsEndPoint.Genuine())
            {
                if (!clsEndPoint.Enable())
                {
                    hPen = hPenDisable;
                }
            }


            //'方向余弦を求める
            TwipsPoint clsStrDevicePoint;
            TwipsPoint clsEndDevicePoint;
            clsStrDevicePoint = clsStrPoint.DevicePoint();
            clsEndDevicePoint = clsEndPoint.DevicePoint();


            double nStrRadius = 0;
            double nEndRadius = 0;
            if (clsStrPoint.Genuine())
            {
                nStrRadius = PLOT_GENUINE_RADIUS * nMapScale;
            }
            else
            {
                nStrRadius = PLOT_OBSPNT_RADIUS * nMapScale;
            }
            if (clsEndPoint.Genuine())
            {
                nEndRadius = PLOT_GENUINE_RADIUS * nMapScale;
            }
            else
            {
                nEndRadius = PLOT_OBSPNT_RADIUS * nMapScale;
            }


            double L = 0;
            double M = 0;
            if (!m_clsMdlPlotProc.GetDirectionCosines(clsStrDevicePoint, clsEndDevicePoint, ref L, ref M))
            {
#if false
                return;
#else
                return btmp;
#endif
            }


            double xp3s = 0;
            double yp3s = 0;
            double xp4s = 0;
            double yp4s = 0;
            double xp3e = 0;
            double yp3e = 0;
            double xp4e = 0;
            double yp4e = 0;
            double xm = 0;
            double ym = 0;
            double nL = 0;
            if (clsStrPoint.Fixed())
            {
                //'三角との交点を求める
                GetTriangleIntersectionMulti(M, L, ref xp3s, ref yp3s, (long)nPLOT_OBSPNT_RADIUS_FIX, -nMULTI_WIDTH);
                GetTriangleIntersectionMulti(M, L, ref xp4s, ref yp4s, (long)nPLOT_OBSPNT_RADIUS_FIX, nMULTI_WIDTH);
                xp3s = xp3s + clsStrDevicePoint.X;
                yp3s = yp3s + clsStrDevicePoint.Y;
                xp4s = xp4s + clsStrDevicePoint.X;
                yp4s = yp4s + clsStrDevicePoint.Y;
            }
            else
            {
                //'円周点を求める
                nL = Math.Sqrt(Math.Pow(nStrRadius, 2) + Math.Pow(nMULTI_WIDTH, 2));
                xm = clsStrDevicePoint.X + (nL * L);
                xp3s = xm + (nMULTI_WIDTH * M);
                xp4s = xm - (nMULTI_WIDTH * M);
                ym = clsStrDevicePoint.Y + (nL * M);
                yp3s = ym - (nMULTI_WIDTH * L);
                yp4s = ym + (nMULTI_WIDTH * L);
            }
            if (clsEndPoint.Fixed())
            {
                //'三角との交点を求める
                GetTriangleIntersectionMulti(-M, -L, ref xp3e, ref yp3e, (long)nPLOT_OBSPNT_RADIUS_FIX, nMULTI_WIDTH);
                GetTriangleIntersectionMulti(-M, -L, ref xp4e, ref yp4e, (long)nPLOT_OBSPNT_RADIUS_FIX, -nMULTI_WIDTH);
                xp3e = xp3e + clsEndDevicePoint.X;
                yp3e = yp3e + clsEndDevicePoint.Y;
                xp4e = xp4e + clsEndDevicePoint.X;
                yp4e = yp4e + clsEndDevicePoint.Y;
            }
            else
            {
                //'円周点を求める
                nL = Math.Sqrt(Math.Pow(nEndRadius, 2) + Math.Pow(nMULTI_WIDTH, 2));
                xm = clsEndDevicePoint.X - (nL * L);
                xp3e = xm + (nMULTI_WIDTH * M);
                xp4e = xm - (nMULTI_WIDTH * M);
                ym = clsEndDevicePoint.Y - (nL * M);
                yp3e = ym - (nMULTI_WIDTH * L);
                yp4e = ym + (nMULTI_WIDTH * L);
            }

#if false
            _ = SelectObject((IntPtr)hDC, (int)hPen);
            POINT tPoint = default;
            _ = MoveToEx((IntPtr)hDC, (int)(xp3s - nDeviceLeft), (int)(yp3s - nDeviceTop), ref tPoint);
            _ = LineTo((IntPtr)hDC, (int)(xp3e - nDeviceLeft), (int)(yp3e - nDeviceTop));
            _ = MoveToEx((IntPtr)hDC, (int)(xp4s - nDeviceLeft), (int)(yp4s - nDeviceTop), ref tPoint);
            _ = LineTo((IntPtr)hDC, (int)(xp4e - nDeviceLeft), (int)(yp4e - nDeviceTop));
            return;
#else
            //１.Bitmapクラスを作成(大きさはPictureBoxのサイズ)
            //Bitmap btmp = (Bitmap)hDC;
            //２.Graphicクラスを生成する(先ほどのBitmap(Imageオフジェクト)クラスを使う)
            Graphics grph = Graphics.FromImage(btmp);
            //３.Penオブジェクト生成(今回は白の太さ２)
            //Color Clr = Color.White;
            //Pen pen = new Pen(Clr, 2);
            Color Clr = Color.FromArgb((int)(L_RGB | 0xFF000000));
            Pen pen = new Pen(Clr, L_Width);
            //４.SmoothingModeを設定
            //(高品質で低速なレンダリング)を指定する
            grph.SmoothingMode = SmoothingMode.HighQuality;
            //５.線を引く
            grph.DrawLine(pen, new Point((int)(xp3s - nDeviceLeft), (int)(yp3s - nDeviceTop)), new Point((int)(xp3e - nDeviceLeft), (int)(yp3e - nDeviceTop)));
            grph.DrawLine(pen, new Point((int)(xp4s - nDeviceLeft), (int)(yp4s - nDeviceTop)), new Point((int)(xp4e - nDeviceLeft), (int)(yp4e - nDeviceTop)));
            //６.解放する
            pen.Dispose();
            grph.Dispose();
            //７.表示を渡す
            return btmp;
#endif
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '2重線と固定点(三角形)との交点を求める。
        '
        '2重線のうちの片方の線との交点が取得できる。
        'nW の符号を反転させてもう1回 GetTriangleIntersectionMulti を呼べば反対側の線との交点が取得できる。
        '
        '引き数：
        'nS ベクトルの向き。正弦。
        'nC ベクトルの向き。余弦。
        'xp 求まった接点X(ピクセル)。
        'yp 求まった接点Y(ピクセル)。
        'nRadius 半径(ピクセル)。正三角形に外接する円の半径。
        'nW 2重線の幅(ピクセル)。
        Public Sub GetTriangleIntersectionMulti(ByVal nS As Double, ByVal nC As Double, ByRef xp As Double, ByRef yp As Double, ByVal nRadius As Long, ByVal nW As Double)

            Dim CosΦ0 As Double
            Dim SinΦ0 As Double
            CosΦ0 = (nW + Sqr(3 * (nRadius ^ 2 - nW ^ 2))) / (2 * nRadius)
            SinΦ0 = Sqr(1 - CosΦ0 ^ 2)


            Dim CosΦ1 As Double
            CosΦ1 = CosΦ0 * COS120 - SinΦ0 * SIN120
            Dim SinΦ2 As Double
            SinΦ2 = CosΦ0 * SIN240 + SinΦ0 * COS240


            Dim N As Long
            Dim CosΦ As Double
            Dim SinΦ As Double
            If nW < 0 Then
                If(CosΦ0 < nC) Or(SinΦ2 <= nS And nS <= 0 And 0 <= nC) Then
                    '-240°
                    N = 0
                    CosΦ = nC * COS120 - nS * SIN120
                    SinΦ = nC * SIN120 + nS * COS120
                ElseIf(nC <= CosΦ1) Or(nS <= 0) Then
                    '-120°
                    N = 1
                    CosΦ = nC * COS240 - nS * SIN240
                    SinΦ = nC * SIN240 + nS * COS240
                Else
                    '補正無し。
                    N = 2
                    CosΦ = nC
                    SinΦ = nS
                End If
            Else
                If(CosΦ0 < nC) Or(nS <= SinΦ2) Or(0 <= nC And nS <= 0) Then
                    '-240°
                    N = 0
                    CosΦ = nC * COS120 - nS * SIN120
                    SinΦ = nC * SIN120 + nS * COS120
                ElseIf(nC <= CosΦ1) Or(nS <= 0) Then
                    '-120°
                    N = 1
                    CosΦ = nC * COS240 - nS * SIN240
                    SinΦ = nC * SIN240 + nS * COS240
                Else
                    '補正無し。
                    N = 2
                    CosΦ = nC
                    SinΦ = nS
                End If
            End If


            Dim nX As Double
            Dim nY As Double
            nY = nRadius * 0.5
            If Abs(CosΦ) < FLT_EPSILON Then
                nX = -nW
            Else
                Dim A As Double
                Dim B As Double
                A = SinΦ / CosΦ
                B = nW / CosΦ
                nX = (nY - B) / A
            End If


            If N = 0 Then
                '+240°
                xp = nX * COS240 - nY * SIN240
                yp = nX * SIN240 + nY * COS240
            ElseIf N = 1 Then
                '+120°
                xp = nX * COS120 - nY * SIN120
                yp = nX * SIN120 + nY * COS120
            Else
                '補正無し。
                xp = nX
                yp = nY
            End If


        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '2重線と固定点(三角形)との交点を求める。
        '
        '2重線のうちの片方の線との交点が取得できる。
        'nW の符号を反転させてもう1回 GetTriangleIntersectionMulti を呼べば反対側の線との交点が取得できる。
        '
        '引き数：
        'nS ベクトルの向き。正弦。
        'nC ベクトルの向き。余弦。
        'xp 求まった接点X(ピクセル)。
        'yp 求まった接点Y(ピクセル)。
        'nRadius 半径(ピクセル)。正三角形に外接する円の半径。
        'nW 2重線の幅(ピクセル)。
        */
        public void GetTriangleIntersectionMulti(double nS, double nC, ref double xp, ref double yp, long nRadius, double nW)
        {
            double CosΦ0;
            double SinΦ0;
            CosΦ0 = (nW + Math.Sqrt(3 * (Math.Pow(nRadius, 2) - Math.Pow(nW, 2)))) / (2 * nRadius);
            SinΦ0 = Math.Sqrt(1 - Math.Pow(CosΦ0, 2));


            double CosΦ1;
            CosΦ1 = (CosΦ0 * COS120) - (SinΦ0 * SIN120);
            double SinΦ2;
            SinΦ2 = (CosΦ0 * SIN240) + (SinΦ0 * COS240);


            long N;
            double CosΦ;
            double SinΦ;
            if (nW < 0)
            {
                if ((CosΦ0 < nC) || (SinΦ2 <= nS && nS <= 0 && 0 <= nC))
                {
                    //'-240°
                    N = 0;
                    CosΦ = (nC * COS120) - (nS * SIN120);
                    SinΦ = (nC * SIN120) + (nS * COS120);
                }
                else if ((nC <= CosΦ1) || (nS <= 0))
                {
                    //'-120°
                    N = 1;
                    CosΦ = (nC * COS240) - (nS * SIN240);
                    SinΦ = (nC * SIN240) + (nS * COS240);
                }
                else
                {
                    //'補正無し。
                    N = 2;
                    CosΦ = nC;
                    SinΦ = nS;
                }
            }
            else
            {
                if ((CosΦ0 < nC) || (nS <= SinΦ2) || (0 <= nC && nS <= 0))
                {
                    //'-240°
                    N = 0;
                    CosΦ = (nC * COS120) - (nS * SIN120);
                    SinΦ = (nC * SIN120) + (nS * COS120);
                }
                else if ((nC <= CosΦ1) || (nS <= 0))
                {
                    //'-120°
                    N = 1;
                    CosΦ = (nC * COS240) - (nS * SIN240);
                    SinΦ = (nC * SIN240) + (nS * COS240);
                }
                else
                {
                    //'補正無し。
                    N = 2;
                    CosΦ = nC;
                    SinΦ = nS;
                }
            }


            double nX;
            double nY;
            nY = nRadius * 0.5;
            if (Math.Abs(CosΦ) < FLT_EPSILON)
            {
                nX = -nW;
            }
            else
            {
                double A;
                double B;
                A = SinΦ / CosΦ;
                B = nW / CosΦ;
                nX = (nY - B) / A;
            }


            if (N == 0)
            {
                //'+240°
                xp = (nX * COS240) - (nY * SIN240);
                yp = (nX * SIN240) + (nY * COS240);
            }
            else if (N == 1)
            {
                //'+120°
                xp = (nX * COS120) - (nY * SIN120);
                yp = (nX * SIN120) + (nY * COS120);
            }
            else
            {
                //'補正無し。
                xp = nX;
                yp = nY;
            }
            return;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '観測点を描画する。
        '
        'nClip～は記号を描画するためのクリップ領域。
        '記号はクリップ領域をはみ出さないように描画する。
        '
        '引き数：
        'hDC デバイスコンテキストのハンドル。
        'nDeviceLeft 論理エリア上のデバイス左辺位置(ピクセル)。
        'nDeviceTop 論理エリア上のデバイス上辺位置(ピクセル)。
        'nClipLeft 論理エリア上のクリップ領域左辺(ピクセル)。
        'nClipTop 論理エリア上のクリップ領域上辺(ピクセル)。
        'nClipWidth 論理エリア上のクリップ領域幅(ピクセル)。
        'nClipHeight 論理エリア上のクリップ領域高さ(ピクセル)。
        'nMapScale Twips→ピクセル、の係数。
        'clsObservationPoint 描画する観測点。
        'hPen 観測点を描画するペン。
        'hBrush 観測点を塗り潰すブラシ。
        'nSymbol 観測点記号の色。
        'nX 観測点記号を描画した位置(ピクセル)が設定される。
        'nY 観測点記号を描画した位置(ピクセル)が設定される。
        'nW 観測点記号を描画した位置(ピクセル)が設定される。
        'nH 観測点記号を描画した位置(ピクセル)が設定される。
        Private Sub DrawObservationPointNSS(ByVal hDC As Long, ByVal nDeviceLeft As Double, ByVal nDeviceTop As Double, ByVal nClipLeft As Double, ByVal nClipTop As Double, ByVal nClipWidth As Double, ByVal nClipHeight As Double, ByVal nMapScale As Double, ByVal clsObservationPoint As ObservationPoint, ByVal hPen As Long, ByVal hBrush As Long, ByVal nSymbol As Long, Optional ByRef nX As Double, Optional ByRef nY As Double, Optional ByRef nW As Double, Optional ByRef nH As Double)
            If clsObservationPoint.Genuine Then
                '描画。
                If clsObservationPoint.Fixed Then
                    Call DrawObservationPointFix(hDC, nDeviceLeft, nDeviceTop, clsObservationPoint.DevicePoint, hPen, PLOT_GENUINE_RADIUS_FIX * nMapScale)
                Else
                    Call DrawObservationPointRover(hDC, nDeviceLeft, nDeviceTop, clsObservationPoint.DevicePoint, hPen, hBrush, PLOT_GENUINE_RADIUS * nMapScale)
                End If
                '記号。
                'オプションの「観測点のラベル」に合わせて表示項目を変更 2006/10/6 NGS Yamada
                Dim strPlotPointLabel As String
                If m_PlotPointLabel = 0 Then
                    strPlotPointLabel = clsObservationPoint.GenuineNumber
                ElseIf m_PlotPointLabel = 1 Then
                    strPlotPointLabel = clsObservationPoint.GenuineName
                Else
                    strPlotPointLabel = clsObservationPoint.GenuineNumber & "(" & clsObservationPoint.GenuineName & ")"
                End If
                Call DrawObservationPointSymbol(hDC, nDeviceLeft, nDeviceTop, nClipLeft, nClipTop, nClipWidth, nClipHeight, clsObservationPoint.DevicePoint, strPlotPointLabel, nSymbol, PLOT_GENUINE_SYMBOL_SPACE * nMapScale, nX, nY, nW, nH)
            Else
                Call DrawObservationPoint(hDC, nDeviceLeft, nDeviceTop, nClipLeft, nClipTop, nClipWidth, nClipHeight, nMapScale, clsObservationPoint, hPen, hBrush, nSymbol, m_PlotPointLabel, nX, nY, nW, nH)
            End If
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '観測点を描画する。
        '
        'nClip～は記号を描画するためのクリップ領域。
        '記号はクリップ領域をはみ出さないように描画する。
        '
        '引き数：
        'hDC デバイスコンテキストのハンドル。
        'nDeviceLeft 論理エリア上のデバイス左辺位置(ピクセル)。
        'nDeviceTop 論理エリア上のデバイス上辺位置(ピクセル)。
        'nClipLeft 論理エリア上のクリップ領域左辺(ピクセル)。
        'nClipTop 論理エリア上のクリップ領域上辺(ピクセル)。
        'nClipWidth 論理エリア上のクリップ領域幅(ピクセル)。
        'nClipHeight 論理エリア上のクリップ領域高さ(ピクセル)。
        'nMapScale Twips→ピクセル、の係数。
        'clsObservationPoint 描画する観測点。
        'hPen 観測点を描画するペン。
        'hBrush 観測点を塗り潰すブラシ。
        'nSymbol 観測点記号の色。
        'nX 観測点記号を描画した位置(ピクセル)が設定される。
        'nY 観測点記号を描画した位置(ピクセル)が設定される。
        'nW 観測点記号を描画した位置(ピクセル)が設定される。
        'nH 観測点記号を描画した位置(ピクセル)が設定される。
        */
#if false
        private void DrawObservationPointNSS(long hDC, double nDeviceLeft, double nDeviceTop, double nClipLeft, double nClipTop, double nClipWidth, double nClipHeight, double nMapScale,
            ObservationPoint clsObservationPoint, long hPen, long hBrush, long nSymbol, double nX = 0, double nY = 0, double nW = 0, double nH = 0)
        {
#else
        private Bitmap DrawObservationPointNSS(Bitmap hDC, double nDeviceLeft, double nDeviceTop, double nClipLeft, double nClipTop, double nClipWidth, double nClipHeight, double nMapScale,
            ObservationPoint clsObservationPoint, long hPen, long hBrush, long nSymbol, double nX = 0, double nY = 0, double nW = 0, double nH = 0)
        {
            Bitmap btmp = hDC;
#endif
            if (clsObservationPoint.Genuine())
            {
                //'描画。
                if (clsObservationPoint.Fixed())
                {
#if false
                    m_clsMdlPlotProc.DrawObservationPointFix(hDC, nDeviceLeft, nDeviceTop, clsObservationPoint.DevicePoint(), hPen, (float)(PLOT_GENUINE_RADIUS_FIX * nMapScale));
#else
                    btmp = m_clsMdlPlotProc.DrawObservationPointFix(btmp, nDeviceLeft, nDeviceTop, clsObservationPoint.DevicePoint(), hPen, (float)(PLOT_GENUINE_RADIUS_FIX * nMapScale));
#endif
                }
                else
                {
#if false
                    m_clsMdlPlotProc.DrawObservationPointRover(hDC, nDeviceLeft, nDeviceTop, clsObservationPoint.DevicePoint(), hPen, hBrush, (float)(PLOT_GENUINE_RADIUS * nMapScale));
#else
                    btmp = m_clsMdlPlotProc.DrawObservationPointRover(btmp, nDeviceLeft, nDeviceTop, clsObservationPoint.DevicePoint(), hPen, hBrush, (float)(PLOT_GENUINE_RADIUS * nMapScale));
#endif
                }
                //'記号。
                //'オプションの「観測点のラベル」に合わせて表示項目を変更 2006/10/6 NGS Yamada
                string strPlotPointLabel;
                if (m_PlotPointLabel == 0)
                {
                    strPlotPointLabel = clsObservationPoint.GenuineNumber();
                }
                else if (m_PlotPointLabel == 1)
                {
                    strPlotPointLabel = clsObservationPoint.GenuineName();
                }
                else
                {
                    strPlotPointLabel = clsObservationPoint.GenuineNumber() + "(" + clsObservationPoint.GenuineName() + ")";
                }
#if false
                m_clsMdlPlotProc.DrawObservationPointSymbol(hDC, nDeviceLeft, nDeviceTop, nClipLeft, nClipTop, nClipWidth, nClipHeight,
                        clsObservationPoint.DevicePoint(), strPlotPointLabel, nSymbol, (long)(PLOT_GENUINE_SYMBOL_SPACE * nMapScale), ref nX, ref nY, ref nW, ref nH);
#else
                btmp = m_clsMdlPlotProc.DrawObservationPointSymbol(btmp, nDeviceLeft, nDeviceTop, nClipLeft, nClipTop, nClipWidth, nClipHeight,
                        clsObservationPoint.DevicePoint(), strPlotPointLabel, nSymbol, (long)(PLOT_GENUINE_SYMBOL_SPACE * nMapScale), ref nX, ref nY, ref nW, ref nH);
#endif
            }
            else
            {
#if false
                m_clsMdlPlotProc.DrawObservationPoint(hDC, nDeviceLeft, nDeviceTop, nClipLeft, nClipTop, nClipWidth, nClipHeight,
                        nMapScale, clsObservationPoint, hPen, hBrush, nSymbol, m_PlotPointLabel, ref nX, ref nY, ref nW, ref nH);
#else
                btmp = m_clsMdlPlotProc.DrawObservationPoint(btmp, nDeviceLeft, nDeviceTop, nClipLeft, nClipTop, nClipWidth, nClipHeight,
                        nMapScale, clsObservationPoint, hPen, hBrush, nSymbol, m_PlotPointLabel, ref nX, ref nY, ref nW, ref nH);
#endif
            }
#if false
            return;
#else
            return btmp;
#endif
        }
        //==========================================================================================


        //==========================================================================================
        /*[VB]
        '重複ベクトルを描画する。2006/10/12 NGS Yamada
        '
        '引き数：
        'hDC デバイスコンテキストのハンドル。
        'nDeviceLeft 論理エリア上のデバイス左辺位置(ピクセル)。
        'nDeviceTop 論理エリア上のデバイス上辺位置(ピクセル)。
        'nMapScale Twips→ピクセル、の係数。
        'clsBaseLineVector 基線ベクトル。
        'hPenLine 線のペン。
        'hPenArrow 矢印のペン。
        'bArrow 矢印描画の指定。True=描画する。False=描画しない。
        Private Sub DrawBaseLineVectorDuplication(ByVal hDC As Long, ByVal nDeviceLeft As Double, ByVal nDeviceTop As Double, ByVal nMapScale As Double, ByVal clsBaseLineVector As BaseLineVector, ByVal hPenLine As Long, ByVal hPenArrow As Long, ByVal bArrow As Boolean)

            '定数。
            Dim nPLOT_OBSPNT_RADIUS_FIX As Double
            Dim nPLOT_OBSPNT_RADIUS As Double
            Dim nPLOT_VECTOR_ARROW_LENGTH As Double
            Dim nPLOT_VECTOR_ARROW_WIDTH As Double
            Dim nMULTI_WIDTH As Double
            nPLOT_OBSPNT_RADIUS_FIX = PLOT_OBSPNT_RADIUS_FIX * nMapScale
            nPLOT_OBSPNT_RADIUS = PLOT_OBSPNT_RADIUS * nMapScale
            nPLOT_VECTOR_ARROW_LENGTH = PLOT_VECTOR_ARROW_LENGTH * nMapScale
            nPLOT_VECTOR_ARROW_WIDTH = PLOT_VECTOR_ARROW_WIDTH * nMapScale
            nMULTI_WIDTH = DUPLICATION_WIDTH * nMapScale


            Call SelectObject(hDC, hPenLine)


            '方向余弦を求める
            Dim clsStrDevicePoint As TwipsPoint
            Dim clsEndDevicePoint As TwipsPoint
            Set clsStrDevicePoint = clsBaseLineVector.StrPoint.DevicePoint
            Set clsEndDevicePoint = clsBaseLineVector.EndPoint.DevicePoint


            Dim nStrRadius As Double
            Dim nEndRadius As Double
            If clsBaseLineVector.StrPoint.Genuine Then
                nStrRadius = PLOT_GENUINE_RADIUS * nMapScale
            Else
                nStrRadius = PLOT_OBSPNT_RADIUS * nMapScale
            End If
            If clsBaseLineVector.StrPoint.Genuine Then
                nEndRadius = PLOT_GENUINE_RADIUS * nMapScale
            Else
                nEndRadius = PLOT_OBSPNT_RADIUS * nMapScale
            End If

            Dim L As Double, M As Double
            If Not GetDirectionCosines(clsStrDevicePoint, clsEndDevicePoint, L, M) Then
                '始点と終点の座標が同じ場合、点を打っておく。
                Call Ellipse(hDC, clsStrDevicePoint.X - nDeviceLeft, clsStrDevicePoint.Y - nDeviceTop, clsStrDevicePoint.X - nDeviceLeft + 1, clsStrDevicePoint.Y - nDeviceTop + 1)
                Exit Sub
            End If


            Dim xp1 As Double, yp1 As Double, xp2 As Double, yp2 As Double


            Dim xp3s As Double, yp3s As Double, xp4s As Double, yp4s As Double
            Dim xp3e As Double, yp3e As Double, xp4e As Double, yp4e As Double
            Dim xm As Double, ym As Double
            Dim nL As Double



            If clsBaseLineVector.StrPoint.TopParentPoint.Based Then
                '中心
                xp1 = 0
                yp1 = 0
                xp3s = clsStrDevicePoint.X + nMULTI_WIDTH * M
                xp4s = clsStrDevicePoint.X - nMULTI_WIDTH * M
                yp3s = clsStrDevicePoint.Y - nMULTI_WIDTH * L
                yp4s = clsStrDevicePoint.Y + nMULTI_WIDTH * L
            ElseIf clsBaseLineVector.StrPoint.TopParentPoint.Fixed Then
                '三角との交点を求める
                Call GetTriangleIntersection(M, L, xp1, yp1, nPLOT_OBSPNT_RADIUS_FIX)
                Call GetTriangleIntersectionMulti(M, L, xp3s, yp3s, nPLOT_OBSPNT_RADIUS_FIX, -nMULTI_WIDTH)
                Call GetTriangleIntersectionMulti(M, L, xp4s, yp4s, nPLOT_OBSPNT_RADIUS_FIX, nMULTI_WIDTH)
                xp3s = xp3s + clsStrDevicePoint.X
                yp3s = yp3s + clsStrDevicePoint.Y
                xp4s = xp4s + clsStrDevicePoint.X
                yp4s = yp4s + clsStrDevicePoint.Y
            Else
                'P1(X1の円周点)を求める
                xp1 = nPLOT_OBSPNT_RADIUS * L
                yp1 = nPLOT_OBSPNT_RADIUS * M
                '円周点を求める
                nL = Sqr(nStrRadius ^ 2 + nMULTI_WIDTH ^ 2)
                xm = clsStrDevicePoint.X + nL * L
                xp3s = xm + nMULTI_WIDTH * M
                xp4s = xm - nMULTI_WIDTH * M
                ym = clsStrDevicePoint.Y + nL * M
                yp3s = ym - nMULTI_WIDTH * L
                yp4s = ym + nMULTI_WIDTH * L
            End If
            xp1 = xp1 + clsStrDevicePoint.X
            yp1 = yp1 + clsStrDevicePoint.Y


            Dim dR As Double
            If clsBaseLineVector.EndPoint.TopParentPoint.Based Then
                '中心
                xp2 = 0
                yp2 = 0
                xp3e = clsEndDevicePoint.X + nMULTI_WIDTH * M
                xp4e = clsEndDevicePoint.X - nMULTI_WIDTH * M
                yp3e = clsEndDevicePoint.Y - nMULTI_WIDTH * L
                yp4e = clsEndDevicePoint.Y + nMULTI_WIDTH * L
            ElseIf clsBaseLineVector.EndPoint.TopParentPoint.Fixed Then
                'P2(三角との交点)を求める
                Call GetTriangleIntersection(-M, -L, xp2, yp2, nPLOT_OBSPNT_RADIUS_FIX)
                dR = Sqr(xp2 * xp2 + yp2 * yp2)
                '三角との交点を求める
                Call GetTriangleIntersectionMulti(-M, -L, xp3e, yp3e, nPLOT_OBSPNT_RADIUS_FIX, nMULTI_WIDTH)
                Call GetTriangleIntersectionMulti(-M, -L, xp4e, yp4e, nPLOT_OBSPNT_RADIUS_FIX, -nMULTI_WIDTH)
                xp3e = xp3e + clsEndDevicePoint.X
                yp3e = yp3e + clsEndDevicePoint.Y
                xp4e = xp4e + clsEndDevicePoint.X
                yp4e = yp4e + clsEndDevicePoint.Y
            Else
                'P2(X2の円周点)を求める
                xp2 = nPLOT_OBSPNT_RADIUS * -L
                yp2 = nPLOT_OBSPNT_RADIUS * -M
                dR = nPLOT_OBSPNT_RADIUS
                '円周点を求める
                nL = Sqr(nEndRadius ^ 2 + nMULTI_WIDTH ^ 2)
                xm = clsEndDevicePoint.X - nL * L
                xp3e = xm + nMULTI_WIDTH * M
                xp4e = xm - nMULTI_WIDTH * M
                ym = clsEndDevicePoint.Y - nL * M
                yp3e = ym - nMULTI_WIDTH * L
                yp4e = ym + nMULTI_WIDTH * L
            End If
            xp2 = xp2 + clsEndDevicePoint.X
            yp2 = yp2 + clsEndDevicePoint.Y


            Dim tPoint As POINT
            Call MoveToEx(hDC, xp3s - nDeviceLeft, yp3s - nDeviceTop, tPoint)
            Call LineTo(hDC, xp3e - nDeviceLeft, yp3e - nDeviceTop)
            Call MoveToEx(hDC, xp4s - nDeviceLeft, yp4s - nDeviceTop, tPoint)
            Call LineTo(hDC, xp4e - nDeviceLeft, yp4e - nDeviceTop)


            If bArrow Then
                Dim xp3 As Double, yp3 As Double, xp4 As Double, yp4 As Double
        '        Dim xm As Double, ym As Double


                'P3(左矢印の先) と P4(右矢印の先) を求める
                xm = clsEndDevicePoint.X - (dR + nPLOT_VECTOR_ARROW_LENGTH) * L
                xp3 = xm - nPLOT_VECTOR_ARROW_WIDTH * M
                xp4 = xm + nPLOT_VECTOR_ARROW_WIDTH * M
                ym = clsEndDevicePoint.Y - (dR + nPLOT_VECTOR_ARROW_LENGTH) * M
                yp3 = ym + nPLOT_VECTOR_ARROW_WIDTH * L
                yp4 = ym - nPLOT_VECTOR_ARROW_WIDTH * L


                Call SelectObject(hDC, hPenArrow)
                Call MoveToEx(hDC, xp3 - nDeviceLeft, yp3 - nDeviceTop, tPoint)
                Call LineTo(hDC, xp2 - nDeviceLeft, yp2 - nDeviceTop)
                Call LineTo(hDC, xp4 - nDeviceLeft, yp4 - nDeviceTop)
            End If
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '重複ベクトルを描画する。2006/10/12 NGS Yamada
        '
        '引き数：
        'hDC デバイスコンテキストのハンドル。
        'nDeviceLeft 論理エリア上のデバイス左辺位置(ピクセル)。
        'nDeviceTop 論理エリア上のデバイス上辺位置(ピクセル)。
        'nMapScale Twips→ピクセル、の係数。
        'clsBaseLineVector 基線ベクトル。
        'hPenLine 線のペン。
        'hPenArrow 矢印のペン。
        'bArrow 矢印描画の指定。True=描画する。False=描画しない。
        */
#if false
        private void DrawBaseLineVectorDuplication(long hDC, double nDeviceLeft, double nDeviceTop, double nMapScale, BaseLineVector clsBaseLineVector, long hPenLine, long hPenArrow, bool bArrow)
        {
#else
        private Bitmap DrawBaseLineVectorDuplication(Bitmap hDC, double nDeviceLeft, double nDeviceTop, double nMapScale, BaseLineVector clsBaseLineVector, long hPenLine, long hPenArrow, bool bArrow)
        {
            Bitmap btmp = hDC;
            int L_SOLID = (int)(hPenLine >> 56);
            int L_Width = (int)((hPenLine >> 32) & 0xFFFFFF);
            int L_RGB = (int)(hPenLine & 0xFFFFFF);
            int A_SOLID = (int)(hPenArrow >> 56);
            int A_Width = (int)((hPenArrow >> 32) & 0xFFFFFF);
            int A_RGB = (int)(hPenArrow & 0xFFFFFF);
#endif
            //'定数。
            double nPLOT_OBSPNT_RADIUS_FIX;
            double nPLOT_OBSPNT_RADIUS;
            double nPLOT_VECTOR_ARROW_LENGTH;
            double nPLOT_VECTOR_ARROW_WIDTH;
            double nMULTI_WIDTH;
            nPLOT_OBSPNT_RADIUS_FIX = PLOT_OBSPNT_RADIUS_FIX * nMapScale;
            nPLOT_OBSPNT_RADIUS = PLOT_OBSPNT_RADIUS * nMapScale;
            nPLOT_VECTOR_ARROW_LENGTH = PLOT_VECTOR_ARROW_LENGTH * nMapScale;
            nPLOT_VECTOR_ARROW_WIDTH = PLOT_VECTOR_ARROW_WIDTH * nMapScale;
            nMULTI_WIDTH = DUPLICATION_WIDTH * nMapScale;

            //  long Sqrt1;
            //  long Sqrt2;
            //  double Sqrt3;
            //  double Sqrt4;
            //  double Sqrt5;
            //  double Sqrt6;
#if false
            SelectObject((IntPtr)hDC, (int)hPenLine);
#endif
            //'方向余弦を求める
            TwipsPoint clsStrDevicePoint;
            TwipsPoint clsEndDevicePoint;
            clsStrDevicePoint = clsBaseLineVector.StrPoint().DevicePoint();
            clsEndDevicePoint = clsBaseLineVector.EndPoint().DevicePoint();


            double nStrRadius;
            double nEndRadius;
            if (clsBaseLineVector.StrPoint().Genuine())
            {
                nStrRadius = PLOT_GENUINE_RADIUS * nMapScale;
            }
            else
            {
                nStrRadius = PLOT_OBSPNT_RADIUS * nMapScale;
            }
            if (clsBaseLineVector.StrPoint().Genuine())
            {
                nEndRadius = PLOT_GENUINE_RADIUS * nMapScale;
            }
            else
            {
                nEndRadius = PLOT_OBSPNT_RADIUS * nMapScale;
            }

            double L = 0;
            double M = 0;
            if (!m_clsMdlPlotProc.GetDirectionCosines(clsStrDevicePoint, clsEndDevicePoint, ref L, ref M))
            {
#if false
                //'始点と終点の座標が同じ場合、点を打っておく。
                _ = Ellipse((IntPtr)hDC, (int)(clsStrDevicePoint.X - nDeviceLeft), (int)(clsStrDevicePoint.Y - nDeviceTop),
                            (int)(clsStrDevicePoint.X - nDeviceLeft + 1), (int)(clsStrDevicePoint.Y - nDeviceTop + 1));
                return;
#else
                //１.Bitmapクラスを作成(大きさはPictureBoxのサイズ)
                //Bitmap btmp = hDC;
                //２.Graphicクラスを生成する(先ほどのBitmap(Imageオフジェクト)クラスを使う)
                Graphics grph1 = Graphics.FromImage(btmp);
                //３.Penオブジェクト生成(今回は白の太さ２)
                //Color Clr1 = Color.White;
                //Pen pen1 = new Pen(Clr1, 2);
                Color Clr1 = Color.FromArgb((int)(L_RGB | 0xFF000000));
                Pen pen1 = new Pen(Clr1, L_Width);
                //４.SmoothingModeを設定
                //(高品質で低速なレンダリング)を指定する
                grph1.SmoothingMode = SmoothingMode.HighQuality;
                //５.円を引く
                int sx;
                int sy;
                int ex;
                int ey;
                int esx;
                int esy;
                sx = (int)(clsStrDevicePoint.X - nDeviceLeft);
                sy = (int)(clsStrDevicePoint.Y - nDeviceTop);
                ex = (int)(clsStrDevicePoint.X - nDeviceLeft + 1);
                ey = (int)(clsStrDevicePoint.Y - nDeviceTop + 1);
                esx = ex - sx;
                esy = ey - sy;
                grph1.DrawEllipse(pen1, sx, sy, esx, esy);
                //６.解放する
                pen1.Dispose();
                grph1.Dispose();
                //７.表示を渡す
                return btmp;
#endif
            }


            double xp1 = 0;
            double yp1 = 0;
            double xp2 = 0;
            double yp2 = 0;


            double xp3s = 0;
            double yp3s = 0;
            double xp4s = 0;
            double yp4s = 0;
            double xp3e = 0;
            double yp3e = 0;
            double xp4e = 0;
            double yp4e = 0;
            double xm = 0;
            double ym = 0;
            double nL = 0;



            if (clsBaseLineVector.StrPoint().TopParentPoint().Based())
            {
                //'中心
                xp1 = 0;
                yp1 = 0;
                xp3s = clsStrDevicePoint.X + nMULTI_WIDTH * M;
                xp4s = clsStrDevicePoint.X - nMULTI_WIDTH * M;
                yp3s = clsStrDevicePoint.Y - nMULTI_WIDTH * L;
                yp4s = clsStrDevicePoint.Y + nMULTI_WIDTH * L;
            }
            else if (clsBaseLineVector.StrPoint().TopParentPoint().Fixed())
            {
                //'三角との交点を求める
                m_clsMdlPlotProc.GetTriangleIntersection(M, L, ref xp1, ref yp1, (long)nPLOT_OBSPNT_RADIUS_FIX);
                GetTriangleIntersectionMulti(M, L, ref xp3s, ref yp3s, (long)nPLOT_OBSPNT_RADIUS_FIX, -nMULTI_WIDTH);
                GetTriangleIntersectionMulti(M, L, ref xp4s, ref yp4s, (long)nPLOT_OBSPNT_RADIUS_FIX, nMULTI_WIDTH);
                xp3s = xp3s + clsStrDevicePoint.X;
                yp3s = yp3s + clsStrDevicePoint.Y;
                xp4s = xp4s + clsStrDevicePoint.X;
                yp4s = yp4s + clsStrDevicePoint.Y;
            }
            else
            {
                //'P1(X1の円周点)を求める
                xp1 = nPLOT_OBSPNT_RADIUS * L;
                yp1 = nPLOT_OBSPNT_RADIUS * M;
                //'円周点を求める
                nL = Math.Sqrt(Math.Pow(nStrRadius, 2) + Math.Pow(nMULTI_WIDTH, 2));
                xm = clsStrDevicePoint.X + (nL * L);
                xp3s = xm + (nMULTI_WIDTH * M);
                xp4s = xm - (nMULTI_WIDTH * M);
                ym = clsStrDevicePoint.Y + (nL * M);
                yp3s = ym - (nMULTI_WIDTH * L);
                yp4s = ym + (nMULTI_WIDTH * L);
            }
            xp1 = xp1 + clsStrDevicePoint.X;
            yp1 = yp1 + clsStrDevicePoint.Y;


            double dR = 0;
            if (clsBaseLineVector.EndPoint().TopParentPoint().Based())
            {
                //'中心
                xp2 = 0;
                yp2 = 0;
                xp3e = clsEndDevicePoint.X + nMULTI_WIDTH * M;
                xp4e = clsEndDevicePoint.X - nMULTI_WIDTH * M;
                yp3e = clsEndDevicePoint.Y - nMULTI_WIDTH * L;
                yp4e = clsEndDevicePoint.Y + nMULTI_WIDTH * L;
            }
            else if (clsBaseLineVector.EndPoint().TopParentPoint().Fixed())
            {
                //'P2(三角との交点)を求める
                m_clsMdlPlotProc.GetTriangleIntersection(-M, -L, ref xp2, ref yp2, (long)nPLOT_OBSPNT_RADIUS_FIX);
                dR = Math.Sqrt(xp2 * xp2 + yp2 * yp2);
                //'三角との交点を求める
                GetTriangleIntersectionMulti(-M, -L, ref xp3e, ref yp3e, (long)nPLOT_OBSPNT_RADIUS_FIX, nMULTI_WIDTH);
                GetTriangleIntersectionMulti(-M, -L, ref xp4e, ref yp4e, (long)nPLOT_OBSPNT_RADIUS_FIX, -nMULTI_WIDTH);
                xp3e = xp3e + clsEndDevicePoint.X;
                yp3e = yp3e + clsEndDevicePoint.Y;
                xp4e = xp4e + clsEndDevicePoint.X;
                yp4e = yp4e + clsEndDevicePoint.Y;
            }
            else
            {
                //'P2(X2の円周点)を求める
                xp2 = nPLOT_OBSPNT_RADIUS * -L;
                yp2 = nPLOT_OBSPNT_RADIUS * -M;
                dR = nPLOT_OBSPNT_RADIUS;
                //'円周点を求める
                nL = Math.Sqrt(Math.Pow(nEndRadius, 2) + Math.Pow(nMULTI_WIDTH, 2));
                xm = clsEndDevicePoint.X - (nL * L);
                xp3e = xm + (nMULTI_WIDTH * M);
                xp4e = xm - (nMULTI_WIDTH * M);
                ym = clsEndDevicePoint.Y - (nL * M);
                yp3e = ym - (nMULTI_WIDTH * L);
                yp4e = ym + (nMULTI_WIDTH * L);
            }
            xp2 = xp2 + clsEndDevicePoint.X;
            yp2 = yp2 + clsEndDevicePoint.Y;

#if false
            POINT tPoint = default;
            _ = MoveToEx((IntPtr)hDC, (int)(xp3s - nDeviceLeft), (int)(yp3s - nDeviceTop), ref tPoint);
            _ = LineTo((IntPtr)hDC, (int)(xp3e - nDeviceLeft), (int)(yp3e - nDeviceTop));
            _ = MoveToEx((IntPtr)hDC, (int)(xp4s - nDeviceLeft), (int)(yp4s - nDeviceTop), ref tPoint);
            _ = LineTo((IntPtr)hDC, (int)(xp4e - nDeviceLeft), (int)(yp4e - nDeviceTop));
#else
            //１.Bitmapクラスを作成(大きさはPictureBoxのサイズ)
            //Bitmap btmp = (Bitmap)hDC;
            //２.Graphicクラスを生成する(先ほどのBitmap(Imageオフジェクト)クラスを使う)
            Graphics grph = Graphics.FromImage(btmp);
            //３.Penオブジェクト生成(今回は白の太さ２)
            //Color Clr = Color.White;
            //Pen pen = new Pen(Clr, 2);
            Color Clr = Color.FromArgb((int)(L_RGB | 0xFF000000));
            Pen pen = new Pen(Clr, L_Width);
            //４.SmoothingModeを設定
            //(高品質で低速なレンダリング)を指定する
            grph.SmoothingMode = SmoothingMode.HighQuality;
            //５.線を引く
            grph.DrawLine(pen, new Point((int)(xp3s - nDeviceLeft), (int)(yp3s - nDeviceTop)), new Point((int)(xp3e - nDeviceLeft), (int)(yp3e - nDeviceTop)));
            grph.DrawLine(pen, new Point((int)(xp4s - nDeviceLeft), (int)(yp4s - nDeviceTop)), new Point((int)(xp4e - nDeviceLeft), (int)(yp4e - nDeviceTop)));
            //６.解放する
            //pen.Dispose();
            //grph.Dispose();
            //７.表示を渡す
            //return btmp;
#endif


            if (bArrow){
                double xp3;
                double yp3;
                double xp4;
                double yp4;
                //'        Dim xm As Double, ym As Double


                //'P3(左矢印の先) と P4(右矢印の先) を求める
                xm = clsEndDevicePoint.X - (dR + nPLOT_VECTOR_ARROW_LENGTH) * L;
                xp3 = xm - nPLOT_VECTOR_ARROW_WIDTH * M;
                xp4 = xm + nPLOT_VECTOR_ARROW_WIDTH * M;
                ym = clsEndDevicePoint.Y - (dR + nPLOT_VECTOR_ARROW_LENGTH) * M;
                yp3 = ym + nPLOT_VECTOR_ARROW_WIDTH * L;
                yp4 = ym - nPLOT_VECTOR_ARROW_WIDTH * L;

#if false
                _ = SelectObject((IntPtr)hDC, (int)hPenArrow);
                _ = MoveToEx((IntPtr)hDC, (int)(xp3 - nDeviceLeft), (int)(yp3 - nDeviceTop), ref tPoint);
                _ = LineTo((IntPtr)hDC, (int)(xp2 - nDeviceLeft), (int)(yp2 - nDeviceTop));
                _ = LineTo((IntPtr)hDC, (int)(xp4 - nDeviceLeft), (int)(yp4 - nDeviceTop));
#else
                //１.Bitmapクラスを作成(大きさはPictureBoxのサイズ)
                //Bitmap btmp = (Bitmap)hDC;
                //２.Graphicクラスを生成する(先ほどのBitmap(Imageオフジェクト)クラスを使う)
                //Graphics grph = Graphics.FromImage(btmp);
                //３.Penオブジェクト生成(今回は白の太さ２)
                //Color Clr = Color.White;
                //Pen pen = new Pen(Clr, 2);
                Color Clr1 = Color.FromArgb((int)(A_RGB | 0xFF000000));
                Pen pen1 = new Pen(Clr1, A_Width);
                //４.SmoothingModeを設定
                //(高品質で低速なレンダリング)を指定する
                grph.SmoothingMode = SmoothingMode.HighQuality;
                //５.線を引く
                grph.DrawLine(pen1, new Point((int)(xp3 - nDeviceLeft), (int)(yp3 - nDeviceTop)), new Point((int)(xp2 - nDeviceLeft), (int)(yp2 - nDeviceTop)));
                grph.DrawLine(pen1, new Point((int)(xp3 - nDeviceLeft), (int)(yp3 - nDeviceTop)), new Point((int)(xp4 - nDeviceLeft), (int)(yp4 - nDeviceTop)));
                //６.解放する
                pen1.Dispose();
                //grph.Dispose();
                //７.表示を渡す
                //return btmp;
#endif
            }
#if false
            return;
#else
            //６.解放する
            pen.Dispose();
            grph.Dispose();
            //７.表示を渡す
            return btmp;
#endif
        }
        //==========================================================================================
    }
}
