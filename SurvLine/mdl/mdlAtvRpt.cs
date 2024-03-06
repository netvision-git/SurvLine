using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using System.Windows.Forms;
using System.Xml.Linq;
using static SurvLine.mdl.mdlNSNote;
using Microsoft.VisualBasic.Logging;

namespace SurvLine.mdl
{
    internal class mdlAtvRpt
    {
        //==========================================================================================
        /*[VB]
        Option Explicit
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        Public Type strcPrintDist
            sNo1 As String
            sNo2 As String
            sDist As String
            s3Dist As String
            s2Dist As String
        End Type

        '重複基線テーブル(点検計算簿クラス&レポートで使用)
        Public Type strcJyufuku
            Point1 As String
            Name1 As String
            Point2 As String
            Name2 As String
            dX1 As Double
            dY1 As Double
            dZ1 As Double
            dX2 As Double
            dY2 As Double
            dZ2 As Double
            kousaX As Double
            kousaY As Double
            kousaZ As Double
            sSession1 As String
            sSession2 As String
        End Type

        'セット間較差の点検及び座標計算印字テーブル、レポート出力向け
        Public Type strcSetXYZPrint
            sNo1 As String '観測点No1
            sName1 As String '観測点名称1
            sNo2 As String '観測点No2
            sName2 As String '観測点名称2
            sDX1 As String '1セットX
            sDY1 As String '1セットY
            sDZ1 As String '1セットZ
            sDX2 As String '2セットX
            sDY2 As String '2セットY
            sDZ2 As String '2セットZ
            sKX As String 'セット間較差X
            sKY As String 'セット間較差Y
            sKZ As String 'セット間較差Z
            sAveX As String '平均値X
            sAveY As String '平均値X
            sAveZ As String '平均値X
        End Type

        'セット間較差の点検(基線ベクトル較差)、レポート出力向け
        Public Type strcSetVecPrint
            sNo1 As String '観測点No1
            sName1 As String '観測点名称1
            sNo2 As String '観測点No2
            sName2 As String '観測点名称2
            sDX1 As String '1セットX
            sDY1 As String '1セットY
            sDZ1 As String '1セットZ
            sDX2 As String '2セットX
            sDY2 As String '2セットY
            sDZ2 As String '2セットZ
            sKX1 As String '較差DX
            sKY1 As String '較差DY
            sKZ1 As String '較差DZ
            sKX2 As String '較差N
            sKY2 As String '較差E
            sKZ2 As String '較差U
        End Type

        '基線ベクトルの環閉合、レポート出力向け
        Public Type strcKanheigouPrint
            sNo1 As String
            sNo2 As String
            sName1 As String
            sName2 As String
            sDX As String
            sDY As String
            sDZ As String
            sSession As String
        End Type
        Public Type strcKanheigouPrint2
            sAddDX As String
            sAddDY As String
            sAddDZ As String
            sN As String
            sE As String
            sU As String
            sN2 As String
            sE2 As String
            sU2 As String
        End Type
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        public struct strcPrintDist
        {
            string sNo1;
            string sNo2;
            string sDist;
            string s3Dist;
            string s2Dist;
        }

        //'重複基線テーブル(点検計算簿クラス&レポートで使用)
        public struct strcJyufuku
        {
            string Point1;
            string Name1;
            string Point2;
            string Name2;
            double dX1;
            double dY1;
            double dZ1;
            double dX2;
            double dY2;
            double dZ2;
            double kousaX;
            double kousaY;
            double kousaZ;
            string sSession1;
            string sSession2;
        }

        //'セット間較差の点検及び座標計算印字テーブル、レポート出力向け
        public struct strcSetXYZPrint
        {
            string sNo1;                //'観測点No1
            string sName1;              //'観測点名称1
            string sNo2;                //'観測点No2
            string sName2;              //'観測点名称2
            string sDX1;                //'1セットX
            string sDY1;                //'1セットY
            string sDZ1;                //'1セットZ
            string sDX2;                //'2セットX
            string sDY2;                //'2セットY
            string sDZ2;                //'2セットZ
            string sKX;                 //'セット間較差X
            string sKY;                 //'セット間較差Y
            string sKZ;                 //'セット間較差Z
            string sAveX;               //'平均値X
            string sAveY;               //'平均値X
            string sAveZ;               //'平均値X
        }

        //'セット間較差の点検(基線ベクトル較差)、レポート出力向け
        public struct strcSetVecPrint
        {
            string sNo1;                //'観測点No1
            string sName1;              //'観測点名称1
            string sNo2;                //'観測点No2
            string sName2;              //'観測点名称2
            string sDX1;                //'1セットX
            string sDY1;                //'1セットY
            string sDZ1;                //'1セットZ
            string sDX2;                //'2セットX
            string sDY2;                //'2セットY
            string sDZ2;                //'2セットZ
            string sKX1;                //'較差DX
            string sKY1;                //'較差DY
            string sKZ1;                //'較差DZ
            string sKX2;                //'較差N
            string sKY2;                //'較差E
            string sKZ2;                //'較差U
        }

        //'基線ベクトルの環閉合、レポート出力向け
        public struct strcKanheigouPrint
        {
            string sNo1;
            string sNo2;
            string sName1;
            string sName2;
            string sDX;
            string sDY;
            string sDZ;
            string sSession;
        }
        public struct strcKanheigouPrint2
        {
            string sAddDX;
            string sAddDY;
            string sAddDZ;
            string sN;
            string sE;
            string sU;
            string sN2;
            string sE2;
            string sU2;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'ToolBar関連
        Public Const CONST_TOOLBARADDSEP As Long = 999
        Public Const CONST_TOOLBARPRINT As Long = 1000
        Public Const CONST_PRINTBTNCAP As String = "印刷"
        Public Const CONST_TOOLBARPDF As Long = 1001
        Public Const CONST_PDFBTNCAP As String = "PDFへ出力"
        Public Const CONST_TOOLBARCLOSE As Long = 1002
        Public Const CONST_CLOSEBTNCAP As String = "閉じる"
        Public Const TBAR_INDEX As Integer = 0                  '0:〈見出し一覧〉ボタン
        Public Const TBAR_SEP1 As Integer = 1                   '1:  セパレーター
        Public Const TBAR_PRINT As Integer = 2                  '2:〈印刷〉ボタン
        Public Const TBAR_SEP2 As Integer = 3                   '3:  セパレーター
        Public Const TBAR_COPY As Integer = 4                   '4:〈コピー〉ボタン
        Public Const TBAR_SEP3 As Integer = 5                   '5:  セパレータ
        Public Const TBAR_SEARCH As Integer = 6                 '6:〈検索〉ボタン
        Public Const TBAR_SEP4 As Integer = 7                   '7:  セパレーター
        Public Const TBAR_UQPAGE As Integer = 8                 '8:〈単一ページ〉ボタン
        Public Const TBAR_MLPAGE As Integer = 9                 '9:〈複数ページ〉ボタン
        Public Const TBAR_SEP5 As Integer = 10                  '10: セパレータ
        Public Const TBAR_RED As Integer = 11                   '11:〈縮小〉ボタン
        Public Const TBAR_EXP As Integer = 12                   '12:〈拡大〉ボタン
        Public Const TBAR_MAG As Integer = 13                   '13:〈倍率〉コンボボックス
        Public Const TBAR_SEP6 As Integer = 14                  '14: セパレータ
        Public Const TBAR_BEFOREPAGE As Integer = 15            '15:〈前ページ〉ボタン
        Public Const TBAR_NEXTPAGE As Integer = 16              '16:〈次ページ〉ボタン
        Public Const TBAR_PAGEBOX As Integer = 17               '17:〈ページ番号〉テキストボックス
        Public Const TBAR_SEP7 As Integer = 18                  '18: セパレーター
        Public Const TBAR_RETURN As Integer = 19                '19:〈戻る〉ボタン
        Public Const TBAR_NEXT As Integer = 20                  '20:〈進む〉ボタン

        Public giPageNum As Integer                             '手簿記簿等で使用するページ番号変数

        Public sDeviceBackup As String
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'ToolBar関連
        public const long CONST_TOOLBARADDSEP = 999;
        public const long CONST_TOOLBARPRINT = 1000;
        public const string CONST_PRINTBTNCAP = "印刷";
        public const long CONST_TOOLBARPDF = 1001;
        public const string CONST_PDFBTNCAP = "PDFへ出力";
        public const long CONST_TOOLBARCLOSE = 1002;
        public const string CONST_CLOSEBTNCAP = "閉じる";
        public const int TBAR_INDEX = 0;                        //'0:〈見出し一覧〉ボタン
        public const int TBAR_SEP1 = 1;                         //'1:  セパレーター
        public const int TBAR_PRINT = 2;                        //'2:〈印刷〉ボタン
        public const int TBAR_SEP2 = 3;                         //'3:  セパレーター
        public const int TBAR_COPY = 4;                         //'4:〈コピー〉ボタン
        public const int TBAR_SEP3 = 5;                         //'5:  セパレータ
        public const int TBAR_SEARCH = 6;                       //'6:〈検索〉ボタン
        public const int TBAR_SEP4 = 7;                         //'7:  セパレーター
        public const int TBAR_UQPAGE = 8;                       //'8:〈単一ページ〉ボタン
        public const int TBAR_MLPAGE = 9;                       //'9:〈複数ページ〉ボタン
        public const int TBAR_SEP5 = 10;                        //'10: セパレータ
        public const int TBAR_RED = 11;                         //'11:〈縮小〉ボタン
        public const int TBAR_EXP = 12;                         //'12:〈拡大〉ボタン
        public const int TBAR_MAG = 13;                         //'13:〈倍率〉コンボボックス
        public const int TBAR_SEP6 = 14;                        //'14: セパレータ
        public const int TBAR_BEFOREPAGE = 15;                  //'15:〈前ページ〉ボタン
        public const int TBAR_NEXTPAGE = 16;                    //'16:〈次ページ〉ボタン
        public const int TBAR_PAGEBOX = 17;                     //'17:〈ページ番号〉テキストボックス
        public const int TBAR_SEP7 = 18;                        //'18: セパレーター
        public const int TBAR_RETURN = 19;                      //'19:〈戻る〉ボタン
        public const int TBAR_NEXT = 20;                        //'20:〈進む〉ボタン

        public int giPageNum;                                   //'手簿記簿等で使用するページ番号変数

        public string sDeviceBackup;
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'ツールバーの設定
        Public Sub SetToolberState(ar As ActiveReport, bShowToolbar As Boolean, bShowIndex As Boolean)

            With ar.Toolbar
        '        'セパレータを追加
        '        .Tools.Add ("")
        '        .Tools.Item(.Tools.Count - 1).Id = CONST_TOOLBARADDSEP
        '        .Tools(.Tools.Count - 1).Type = 2
        '        '印刷ボタンを追加
        '        .Tools.Add (CONST_PRINTBTNCAP)
        '        .Tools.Item(.Tools.Count - 1).Id = CONST_TOOLBARPRINT
        '        .Tools(.Tools.Count - 1).Type = 0
        '        .Tools(.Tools.Count - 1).AddIcon LoadResPicture(104, vbResIcon)
        '        .Tools(.Tools.Count - 1).Tooltip = "印刷"
                'セパレータを追加
                .Tools.Add ("")
                .Tools.Item(.Tools.Count - 1).ID = CONST_TOOLBARADDSEP
                .Tools(.Tools.Count - 1).Type = 2
                'PDFボタンを追加
                .Tools.Add(CONST_PDFBTNCAP)
                .Tools.Item(.Tools.Count - 1).ID = CONST_TOOLBARPDF
                .Tools(.Tools.Count - 1).Type = 0
                .Tools(.Tools.Count - 1).AddIcon LoadResPicture(101, vbResIcon)
                .Tools(.Tools.Count - 1).Tooltip = "PDFファイルへエクスポートします"
                'セパレータを追加
                .Tools.Add("")
                .Tools.Item(.Tools.Count - 1).ID = CONST_TOOLBARADDSEP
                .Tools(.Tools.Count - 1).Type = 2
                '閉じるボタンを追加
                .Tools.Add(CONST_CLOSEBTNCAP)
                .Tools.Item(.Tools.Count - 1).ID = CONST_TOOLBARCLOSE
                .Tools(.Tools.Count - 1).Type = 0
                .Tools(.Tools.Count - 1).AddIcon LoadResPicture(102, vbResIcon)
                .Tools(.Tools.Count - 1).Tooltip = "プレビューを閉じます"
                '各ツールバーの表示・非表示設定
                .Tools(TBAR_INDEX).Visible = bShowIndex '見出し一覧の表示/非表示設定
                '.Tools(TBAR_SEP1).Visible = bShowIndex 'セパレータの表示/非表示設定
                '.Tools(TBAR_PRINT).Visible = False '印刷ボタン(オリジナル)を消す
                .Tools(TBAR_SEP1).Visible = False 'セパレーター1を消す
                .Tools(TBAR_COPY).Visible = False 'コピーボタンを消す
                .Tools(TBAR_SEP3).Visible = False 'セパレータを消す
            End With


            ar.TOCVisible = bShowIndex '見出し一覧表示の設定
            ar.TOCWidth = 2250 'デフォルト 1800 Twips：Twips単位
            ar.ToolbarVisible = bShowToolbar 'ツールバー全体を表示

        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'ツールバーの設定

#if false   //エラーが取れません！ sakai

        public void SetToolberState(ActiveReport ar, bool bShowToolbar, bool bShowIndex)
        {

            //With ar.Toolbar
        //'        //'セパレータを追加
        //'        .Tools.Add ("")
        //'        .Tools.Item(.Tools.Count - 1).Id = CONST_TOOLBARADDSEP
        //'        .Tools(.Tools.Count - 1).Type = 2
        //'        //'印刷ボタンを追加
        //'        .Tools.Add (CONST_PRINTBTNCAP)
        //'        .Tools.Item(.Tools.Count - 1).Id = CONST_TOOLBARPRINT
        //'        .Tools(.Tools.Count - 1).Type = 0
        //'        .Tools(.Tools.Count - 1).AddIcon LoadResPicture(104, vbResIcon)
        //'        .Tools(.Tools.Count - 1).Tooltip = "印刷"
            //'セパレータを追加
            ar.Toolbar.Tools.Add("");
            ar.Toolbar.Tools.Item(ar.Toolbar.Tools.Count - 1).ID = CONST_TOOLBARADDSEP;
            ar.Toolbar.Tools(ar.Toolbar.Tools.Count - 1).Type = 2;
            //'PDFボタンを追加
            ar.Toolbar.Tools.Add(CONST_PDFBTNCAP);
            ar.Toolbar.Tools.Item(ar.Toolbar.Tools.Count - 1).ID = CONST_TOOLBARPDF;
            ar.Toolbar.Tools(ar.Toolbar.Tools.Count - 1).Type = 0;
            ar.Toolbar.Tools(ar.Toolbar.Tools.Count - 1).AddIcon LoadResPicture(101, vbResIcon);
            ar.Toolbar.Tools(ar.Toolbar.Tools.Count - 1).Tooltip = "PDFファイルへエクスポートします";
            //'セパレータを追加
            ar.Toolbar.Tools.Add("");
            ar.Toolbar.Tools.Item(ar.Toolbar.Tools.Count - 1).ID = CONST_TOOLBARADDSEP;
            ar.Toolbar.Tools(ar.Toolbar.Tools.Count - 1).Type = 2;
            //'閉じるボタンを追加
            ar.Toolbar.Tools.Add(CONST_CLOSEBTNCAP);
            ar.Toolbar.Tools.Item(ar.Toolbar.Tools.Count - 1).ID = CONST_TOOLBARCLOSE;
            ar.Toolbar.Tools(ar.Toolbar.Tools.Count - 1).Type = 0;
            ar.Toolbar.Tools(ar.Toolbar.Tools.Count - 1).AddIcon LoadResPicture(102, vbResIcon);
            ar.Toolbar.Tools(ar.Toolbar.Tools.Count - 1).Tooltip = "プレビューを閉じます";
            //'各ツールバーの表示・非表示設定
            ar.Toolbar.Tools(TBAR_INDEX).Visible = bShowIndex; //'見出し一覧の表示/非表示設定
            //'.Tools(TBAR_SEP1).Visible = bShowIndex; //'セパレータの表示 / 非表示設定
            //'.Tools(TBAR_PRINT).Visible = False; //'印刷ボタン(オリジナル)を消す
            ar.Toolbar.Tools(TBAR_SEP1).Visible = false; //'セパレーター1を消す
            ar.Toolbar.Tools(TBAR_COPY).Visible = false; //'コピーボタンを消す
            ar.Toolbar.Tools(TBAR_SEP3).Visible = false; //'セパレータを消す
            //End With


            ar.TOCVisible = bShowIndex; //'見出し一覧表示の設定
            ar.TOCWidth = 2250; //'デフォルト 1800 Twips：Twips単位
            ar.ToolbarVisible = bShowToolbar; //'ツールバー全体を表示

            return;

        }

#endif      //エラーが取れません！ sakai

        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'PDFボタン検出時処理
        Public Sub PDF_Export(hWnd As Long, ac As ActiveReport, ByVal Tool As DDActiveReports2.DDTool, sOutputFolder As String, Optional ByVal sDefoName As String = "")

            On Error GoTo FileErrorHandler


            Dim oPDF As ARExportPDF
            Dim sPfile As String
            Dim sPath As String
            Dim dlg As clsCmnDlg
            Dim sFilter As String

            If Tool.ID = CONST_TOOLBARPDF Then
                Set dlg = New clsCmnDlg
                sFilter = "PDFファイル(*.pdf)" & vbNullChar & "*.pdf" & vbNullChar
                sPath = dlg.SaveDlg(hWnd, sFilter, "*.pdf", "エクスポート先を指定", sDefoName, sOutputFolder)
                Set dlg = Nothing
                If sPath <> "" Then
                    Set oPDF = New ARExportPDF
                    oPDF.FileName = sPath
                    ac.Export oPDF
                    Set oPDF = Nothing
                    sOutputFolder = GetFolder(sPath)
                End If
                Set dlg = Nothing
            End If


            Exit Sub


        FileErrorHandler:
            Call MsgBox(Err.Description, vbCritical)

        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'PDFボタン検出時処理

#if false   //エラーが取れません！ sakai

        public void PDF_Export(IntPtr hWnd, ActiveReport ac, DDActiveReports2.DDTool Tool, string sOutputFolder, string sDefoName = "")
        {

            try
            {
                ARExportPDF oPDF;
                string sPfile;
                string sPath;
                clsCmnDlg dlg;
                string sFilter;

                if (Tool.ID == CONST_TOOLBARPDF)
                {
                    clsCmnDlg dlg = new clsCmnDlg;
                    char NullChar = '\0';
                    sFilter = "PDFファイル(*.pdf)" + NullChar + "*.pdf" + NullChar;
                    sPath = dlg.SaveDlg(hWnd, sFilter, "*.pdf", "エクスポート先を指定", sDefoName, sOutputFolder);
                    dlg = null;
                    if (sPath != "")
                    {
                        oPDF = new ARExportPDF;
                        oPDF.FileName = sPath;
                        ac.Export oPDF;
                        oPDF = null;
                        sOutputFolder = GetFolder(sPath);
                    }
                    dlg = null;
                }
                return;

            }


            catch
            {
                //MsgBox(Err.Description, vbCritical)
                _ = MessageBox.Show("");
                return;
            }
        }

#endif      //エラーが取れません！ sakai

        //==========================================================================================

        //==========================================================================================
        /*[VB]
        ''PDFボタン検出時処理 & 印刷処理
        'Public Sub PDF_Export(hwnd As Long, ac As ActiveReport, ByVal Tool As DDActiveReports2.DDTool, sOutputFolder As String, Optional ByVal sDefoName As String = "")
        '    Dim oPDF As ARExportPDF
        '    Dim sPfile As String
        '    Dim sPath As String
        '    Dim dlg As clsCmnDlg
        '    Dim sFilter As String
        '    Dim bRes As Boolean
        '    Dim clSelect As New NSSelect.clsPrinterSelect
        '    Dim sSelectDevice As String
        '
        '    If Tool.Id = CONST_TOOLBARPDF Then
        '        Set dlg = New clsCmnDlg
        '        sFilter = "PDFファイル(*.pdf)" & vbNullChar & "*.pdf" & vbNullChar
        '        sPath = dlg.SaveDlg(hwnd, sFilter, "*.pdf", "エクスポート先を指定", sDefoName, sOutputFolder)
        '        Set dlg = Nothing
        '        If sPath <> "" Then
        '            Set oPDF = New ARExportPDF
        '            oPDF.FileName = sPath
        '            ac.Export oPDF
        '            Set oPDF = Nothing
        '            sOutputFolder = GetFolder(sPath)
        '        End If
        '        Set dlg = Nothing
        '    ElseIf Tool.Id = CONST_TOOLBARPRINT Then
        '        bRes = clSelect.PrinterDlg(sDeviceBackup, sSelectDevice)
        '        If bRes Then
        '            If clSelect.PossibilityCheckAcPrinter(sDeviceBackup) Then
        '                ac.Printer.DeviceName = sDeviceBackup
        '                Call GoPrint(ac)
        '                ac.Printer.DeviceName = ""
        '            Else
        '                bRes = clSelect.SetPrinterDefault(sSelectDevice)
        '                If bRes Then
        '                    ac.Printer.DeviceName = sSelectDevice
        '                    Call GoPrint(ac)
        '                    ac.Printer.DeviceName = ""
        '                    bRes = clSelect.SetPrinterDefault(sDeviceBackup)
        '                End If
        '            End If
        '        End If
        '    End If
        'End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'ファイル名を含むパスをからフォルダを取り出す
        Private Function GetFolder(ByVal sPath As String) As String
            Dim i As Integer
            sPath = Trim(sPath)
            i = InStrRev(sPath, "\")
            If i > 0 Then
                GetFolder = Left(sPath, i)
            Else
                GetFolder = ""
            End If
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'ファイル名を含むパスをからフォルダを取り出す
        private string GetFolder(string sPath)
        {
            int i;
            sPath = sPath.Trim();
            i = sPath.IndexOf("\\");
            if (i > 0)
            {
                return MdlUtility.Left(sPath, i);
            }
            else
            {
                return "";
            }
        }
        //==========================================================================================


        //==========================================================================================
        /*[VB]
        '閉じるボタン検出?
        Public Function IsToolbarClose(ac As ActiveReport, ByVal Tool As DDActiveReports2.DDTool)
            IsToolbarClose = IIf(Tool.ID = CONST_TOOLBARCLOSE, True, False)
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'閉じるボタン検出?

#if false   //エラーが取れません！ sakai

        public bool IsToolbarClose(ActiveReport ac, DDActiveReports2.DDTool Tool)
        {
            return Tool.ID == CONST_TOOLBARCLOSE ? true : false;
        }
        
#endif   //エラーが取れません！ sakai

        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'ページ設定
        Public Sub ActiveReportPageSettings(ar As ActiveReport, nOrientation As Integer)
            'sDeviceBackup = Printer.DeviceName '通常使うプリンタを保存(ar.Printer.DeviceNameは参照しただけで落ちる・・・)
            'ar.Printer.DeviceName = ""  '' 仮想プリンタの利用
            With ar.PageSettings
                .PaperSize = vbPRPSA4 'A4
                .Orientation = nOrientation
                '左マージンをデフォルト(1インチ)から3/4インチにする
                .LeftMargin = 1440 * (3 / 4)
                '右マージンをデフォルト(1インチ)から3/4インチにする
                .RightMargin = 1440 * (3 / 4)
                '上マージンをデフォルト(1インチ)から3/4インチにする
                .TopMargin = 1440 * (3 / 4)
                '下マージンをデフォルト(1インチ)から1/3インチにする
                .BottomMargin = 1440 * (2 / 5)
                '幅の設定
                If nOrientation = ddOPortrait Then '縦置の場合
                    ar.PrintWidth = 11904 - (.LeftMargin + .RightMargin) 'ar.Printer.PaperWidthは11904
                ElseIf nOrientation = ddOLandscape Then '横置きの場合
                    ar.PrintWidth = 16839.9 - (.LeftMargin + .RightMargin) 'ar.Printer.PaperWidthは16839.9
                End If
            End With
            'ar.Printer.DeviceName = Printer.DeviceName
            'ar.Printer.DeviceName = sDeviceBackup
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'ページ設定

#if false   //エラーが取れません！ sakai

        public void ActiveReportPageSettings(ActiveReport ar, int nOrientation)
        {
            //'sDeviceBackup = Printer.DeviceName '通常使うプリンタを保存(ar.Printer.DeviceNameは参照しただけで落ちる・・・)
            //'ar.Printer.DeviceName = ""  '' 仮想プリンタの利用
            //With ar.PageSettings
            ar.PageSettings.PaperSize = 9;                      // vbPRPSA4; //'A4
            ar.PageSettings.Orientation = nOrientation;
            //'左マージンをデフォルト(1インチ)から3/4インチにする
            ar.PageSettings.LeftMargin = 1440 * (3 / 4);
            //'右マージンをデフォルト(1インチ)から3/4インチにする
            ar.PageSettings.RightMargin = 1440 * (3 / 4);
            //'上マージンをデフォルト(1インチ)から3/4インチにする
            ar.PageSettings.TopMargin = 1440 * (3 / 4);
            //'下マージンをデフォルト(1インチ)から1/3インチにする
            ar.PageSettings.BottomMargin = 1440 * (2 / 5);
            //'幅の設定
            //if (nOrientation == ddOPortrait)
            if (nOrientation == 1)
            {                                   //'縦置の場合
                ar.PrintWidth = 11904 - (ar.PageSettings.LeftMargin + .RightMargin); //'ar.Printer.PaperWidthは11904
            }
            //else if (nOrientation == ddOLandscape)
            else if (nOrientation == 2)
            {                                   //'横置きの場合
                ar.PrintWidth = 16839.9 - (ar.PageSettings.LeftMargin + ar.PageSettings.RightMargin); //'ar.Printer.PaperWidthは16839.9
            }
            //End With
            //'ar.Printer.DeviceName = Printer.DeviceName
            //'ar.Printer.DeviceName = sDeviceBackup
        }

#endif      //エラーが取れません！ sakai

        //==========================================================================================

        //==========================================================================================
        /*[VB]
        ''ページ設定
        'Public Sub ActiveReportPageSettings(ar As ActiveReport)
        '    'Dim sDeviceBackup As String
        '    sDeviceBackup = Printer.DeviceName '通常使うプリンタを保存(ar.Printer.DeviceNameは参照しただけで落ちる・・・)
        '    ar.Printer.DeviceName = ""  '' 仮想プリンタの利用
        '    With ar.PageSettings
        '        .PaperSize = vbPRPSA4 'A4
        '        .Orientation = ddOPortrait '縦置き
        '        '左マージンをデフォルト(1インチ)から3/4インチにする
        '        .LeftMargin = 1440 * (3 / 4)
        '        '右マージンをデフォルト(1インチ)から3/4インチにする
        '        .RightMargin = 1440 * (3 / 4)
        '        '上マージンをデフォルト(1インチ)から3/4インチにする
        '        .TopMargin = 1440 * (3 / 4)
        '        '下マージンをデフォルト(1インチ)から1/3インチにする
        '        .BottomMargin = 1440 * (2 / 5)
        '        '幅の設定
        '        ar.PrintWidth = 11904 - (.LeftMargin + .RightMargin) 'ar.Printer.PaperWidthは11904
        '        'ar.PrintWidth = ar.Printer.PaperWidth - (.LeftMargin + .RightMargin)
        '    End With
        '    'ar.Printer.DeviceName = sDeviceBackup '戻す
        'End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'フォーム位置の設定
        Public Function LoadPreviewPos(lLeft As Long, lTop As Long, lWidth As Long, lHeight As Long) As Boolean
            Dim bRes As Boolean
            Dim S As String
            '
            bRes = GetRegValue("PrTop", S)
            If bRes Then
                lTop = CLng(S)
            Else
                lTop = 315
            End If
            '
            bRes = GetRegValue("PrLeft", S)
            If bRes Then
                lLeft = CLng(S)
            Else
                lLeft = 1305
            End If
            '
            bRes = GetRegValue("PrHeight", S)
            If bRes Then
                lHeight = CLng(S)
            Else
                lHeight = 10590
            End If
            '
            bRes = GetRegValue("PrWidth", S)
            If bRes Then
                lWidth = CLng(S)
            Else
                lWidth = 12840
            End If
            LoadPreviewPos = bRes
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'フォーム位置の設定
        public bool LoadPreviewPos(long lLeft, long lTop, long lWidth, long lHeight)
        {
            bool bRes;
            string S = null;
            mdlNSNote clsmdlNSNote = new mdlNSNote();
            //'
            bRes = clsmdlNSNote.GetRegValue("PrTop", ref S);
            if (bRes)
            {
                lTop = long.Parse(S);
            }
            else
            {
                lTop = 315;
            }
            //'
            bRes = clsmdlNSNote.GetRegValue("PrLeft", ref S);
            if (bRes)
            {
                lLeft = long.Parse(S);
            }
            else
            {
                lLeft = 1305;
            }
            //'
            bRes = clsmdlNSNote.GetRegValue("PrHeight", ref S);
            if (bRes)
            {
                lHeight = long.Parse(S);
            }
            else
            {
                lHeight = 10590;
            }
            //'
            bRes = clsmdlNSNote.GetRegValue("PrWidth", ref S);
            if (bRes)
            {
                lWidth = long.Parse(S);
            }
            else
            {
                lWidth = 12840;
            }
            return bRes;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'フォーム位置を保存
        Public Function SavePreviewPos(lLeft As Long, lTop As Long, lWidth As Long, lHeight As Long) As Boolean
            Dim bRes As Boolean
            Dim S As String
            SavePreviewPos = False
            S = CStr(lTop)
            bRes = SetRegValue("PrTop", S)
            If Not bRes Then Exit Function
            S = CStr(lLeft)
            bRes = SetRegValue("PrLeft", S)
            S = CStr(lHeight)
            bRes = SetRegValue("PrHeight", S)
            S = CStr(lWidth)
            bRes = SetRegValue("PrWidth", S)
            If bRes Then SavePreviewPos = True
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'フォーム位置を保存
        public bool SavePreviewPos(long lLeft, long lTop, long lWidth, long lHeight)
        {
            bool bRes;
            string S = null;
            mdlNSNote clsmdlNSNote = new mdlNSNote();
            S = lTop.ToString();
            bRes = clsmdlNSNote.SetRegValue("PrTop", ref S);
            if (!bRes)
            {
                return false;
            }
            S = lLeft.ToString();
            bRes = clsmdlNSNote.SetRegValue("PrLeft", ref S);
            S = lHeight.ToString();
            bRes = clsmdlNSNote.SetRegValue("PrHeight", ref S);
            S = lWidth.ToString();
            bRes = clsmdlNSNote.SetRegValue("PrWidth", ref S);
            if (!bRes)
            {
                return false;
            }
            return true;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '対象プリンターの確認
        Public Function FoundDefoPrinter() As Boolean
            Dim P As Printer
            For Each P In Printers
                MsgBox "p.DeviceName=" & P.DeviceName
            Next
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'対象プリンターの確認
        public bool FoundDefoPrinter()
        {
            /*
            Printer P;
            For Each P In Printers
                MsgBox "p.DeviceName=" & P.DeviceName
            Next
            */
            return false;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '帳票にページ番号を出力するか?
        Public Function IsShowPage() As Boolean
            Dim sValue As String
            IsShowPage = GetRegValue("ページ番号を印刷する", sValue)
            If IsShowPage Then
                If CInt(sValue) <> 1 Then
                    IsShowPage = False
                End If
            Else
                IsShowPage = True
            End If
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'帳票にページ番号を出力するか?
        public bool IsShowPage()
        {
            string sValue = null;
            bool IsS;
            mdlNSNote clsmdlNSNote = new mdlNSNote();
            IsS = clsmdlNSNote.GetRegValue("ページ番号を印刷する", ref sValue);
            if (IsS)
            {
                if (int.Parse(sValue) != 1)
                {
                    return false;
                }
            }
            else
            {
                return true;
            }
            return true;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '記簿の使用した楕円体高
        Public Function GetKiboDaentaikou() As String
            Dim sValue As String, bRes As Boolean
            If GetRegValue("記簿に出力する楕円体高モデル", sValue) Then
                GetKiboDaentaikou = sValue
            Else
                GetKiboDaentaikou = "GRS80"
            End If
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'記簿の使用した楕円体高
        public string GetKiboDaentaikou()
        {
            string sValue = null;
            bool bRes;
            mdlNSNote clsmdlNSNote = new mdlNSNote();
            if (clsmdlNSNote.GetRegValue("記簿に出力する楕円体高モデル", ref sValue))
            {
                return sValue;
            }
            else
            {
                return "GRS80";
            }
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        ''強制印刷
        'Public Sub GoPrint(ac As ActiveReport)
        '    Dim I As Long
        '    With ac.Printer
        '        .StartJob "テストドキュメント" '印刷ジョブ開始
        '        .StartPage '1つの印刷ジョブ内で、新しいページを始める
        '        For I = 1 To ac.Pages.Count
        '            DoEvents
        '            If .Status = ddJSAborted Then
        '                Debug.Print "印刷を中止しました"
        '                Exit Sub
        '            End If
        '            .PrintPage ac.Pages(I - 1) ', 100, 100, 5000, 5000'圧縮する場合、数字を入れる!
        '            Debug.Print CStr(I) & "/" & CStr(ac.Pages.Count) & "ページ目印刷中"
        '        Next I
        '        .EndPage '処理中のページを終了
        '        .EndJob '印刷ジョブ終了
        '    End With
        'End Sub
        '
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //==========================================================================================
    }
}
