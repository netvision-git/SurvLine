//***************************************************************************
//24/02/20 K.setoguchi@NV---------->>>>>>>>>>
//***************************************************************************
using SurvLine.mdl;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static SurvLine.mdl.DEFINE;
using static SurvLine.mdl.MdlNSDefine;
using static SurvLine.mdl.MdlUtility;
using static SurvLine.mdl.MdlAccountMake;
using static SurvLine.mdl.MdlNSGUI;
using static SurvLine.mdl.MdlNSSDefine;
using static SurvLine.mdl.MdiVBfunctions;
using static System.Net.WebRequestMethods;

namespace SurvLine
{
    internal class MdlImport
    {
        MdiVBfunctions mdiVBfunctions = new MdiVBfunctions();

        MdlUtility mdlUtility = new MdlUtility();

        public MdlImport()
        {
        }

        //'*******************************************************************************
        //'インポート。
        //
        //Option Explicit
        //

        //==========================================================================================
        /*[VB]
            '定数
            Public Const IMPORT_TEMPPATH = "Import\" 'テンポラリフォルダのパス。
            Public Const NS5RIN_TEMPPATH = "Temp_NS5RIN" 'NS5RINのテンポラリフォルダのパス。
            Public Const CTR_PATH = "\Ctr" 'convertToRINEXのパス。
            '2020/11/09 FDC 暦ファイルの流用'''''''''''''''''''''''''''''''''''''''''''''''
            Public Const IMPORT_SPLITPATH = "split\" '分割ファイルの一時置き場
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

            'メッセージ。
            Private Const MSG_IMPORT_FILENAMEFAILED = "不正なファイル名が指定されました。"
            Private Const MSG_IMPORT_FILENOTFOUND = "が見つかりません。"
            Private Const MSG_IMPORT_NGS2RIN = "RINEXファイルの生成に失敗しました。"
            Private Const MSG_IMPORT_NGSRDRIN = "RINEXファイルの読み込みに失敗しました。"
            Private Const MSG_IMPORT_RINEXVERSIONFAILED = "このRINEXファイルバージョンには対応していません。"
            '2020/10/26 FDC 暦ファイルの流用'''''''''''''''''''''''''''''''''''''''''''''''
            Private Const MSG_IMPORT_DATAPARTFAILED = "データ部がない暦ファイルが指定されました。"
            Private Const MSG_IMPORT_SPLIT = "混合ファイルの分割に失敗しました。"
            Private Const MSG_IMPORT_APPLYFAILED = "衛星軌道情報の読み込みに失敗しました。"
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]

        //'定数
        public const string IMPORT_TEMPPATH = @"Import\";       //'テンポラリフォルダのパス。
        public const string NS5RIN_TEMPPATH = "Temp_NS5RIN";   //'NS5RINのテンポラリフォルダのパス。
        public const string CTR_PATH = @"\Ctr";                 //'convertToRINEXのパス。
        //'2020/11/09 FDC 暦ファイルの流用'''''''''''''''''''''''''''''''''''''''''''''''
        public const string IMPORT_SPLITPATH = @"split\";        //'分割ファイルの一時置き場
        //'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

        //'メッセージ。
        public const string MSG_IMPORT_FILENAMEFAILED = "不正なファイル名が指定されました。";
        public const string MSG_IMPORT_FILENOTFOUND = "が見つかりません。";
        public const string MSG_IMPORT_NGS2RIN = "RINEXファイルの生成に失敗しました。";
        public const string MSG_IMPORT_NGSRDRIN = "RINEXファイルの読み込みに失敗しました。";
        public const string MSG_IMPORT_RINEXVERSIONFAILED = "このRINEXファイルバージョンには対応していません。";
        //'2020/10/26 FDC 暦ファイルの流用'''''''''''''''''''''''''''''''''''''''''''''''
        public const string MSG_IMPORT_DATAPARTFAILED = "データ部がない暦ファイルが指定されました。";
        public const string MSG_IMPORT_SPLIT = "混合ファイルの分割に失敗しました。";
        public const string MSG_IMPORT_APPLYFAILED = "衛星軌道情報の読み込みに失敗しました。";
        //'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        //24/02/20 K.setoguchi@NV---------->>>>>>>>>>
        //***************************************************************************








        //==========================================================================================
        /*[VB]
            'ファイル名文字数を検査する。
            '
            'インポートするファイルのファイル名文字数を検査する。
            '
            '引き数：
            'sPath インポートするファイルのパス。
            '
            '戻り値：
            '問題が無い場合は True を返す。
            'それ以外の場合は False を返す。
            Public Function CheckFileTitle(ByVal sPath As String) As Boolean

                CheckFileTitle = False

                Dim sDrive As String
                Dim sDir As String
                Dim sTitle As String
                Dim sExt As String

                Call SplitPath(sPath, sDrive, sDir, sTitle, sExt)


                Dim nCode() As Byte
                nCode = StrConv(sTitle & sExt, vbFromUnicode)


                If UBound(nCode) >= 60 Then Exit Function

                CheckFileTitle = True


            End Function
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /// <summary>
        /// ファイル名文字数を検査する。
        ///  
        /// インポートするファイルのファイル名文字数を検査する。
        /// 
        /// 引き数：
        /// sPath インポートするファイルのパス。
        /// 
        /// </summary>
        /// <param name="sPath"></param>
        /// <returns>
        /// 戻り値：
        ///     問題が無い場合は True を返す。
        ///     それ以外の場合は False を返す。
        /// </returns>
        public bool CheckFileTitle(string sPath)
        {
            bool CheckFileTitle = false;

            string sDrive = "";
            string sDir = "";
            string sTitle = "";
            string sExt = "";

            SplitPath(sPath, ref sDrive, ref sDir, ref sTitle, ref sExt);

            byte[] nCode;
            nCode = StrConv($"{sTitle}{sExt}", DEFINE.vbFromUnicode);

            if (nCode.Length >= 60)
            {
                return CheckFileTitle;
            }

            CheckFileTitle = true;
            return CheckFileTitle;
        }


        //==========================================================================================
        /*[VB]
        'RINEXファイルを揃える。
        '
        'RINEXファイルは対になる2つのファイルが必要となる。
        'インポートするファイルとして指定されたファイルと対になるファイルを検索する。
        '対になる2つのファイルは同じフォルダになければならない。
        '
        '引き数：
        'sPath インポートするファイルのパス。参照する配列の要素は(0 To ...)。Ubound 関数を使用する。
        'sPathO インポートするOファイルのパスが設定される。配列の要素は(-1 To ...)、要素 -1 は未使用。
        'sPathN インポートするNファイルのパスが設定される。配列の要素は(-1 To ...)、要素 -1 は未使用。ファイルがない場合は空文字が設定される。
        'sPathG インポートするGファイルのパスが設定される。配列の要素は(-1 To ...)、要素 -1 は未使用。ファイルがない場合は空文字が設定される。
        'sPathJ インポートするJファイルのパスが設定される。配列の要素は(-1 To ...)、要素 -1 は未使用。ファイルがない場合は空文字が設定される。
        'sPathL インポートするLファイルのパスが設定される。配列の要素は(-1 To ...)、要素 -1 は未使用。ファイルがない場合は空文字が設定される。
        'sPathC インポートするCファイルのパスが設定される。配列の要素は(-1 To ...)、要素 -1 は未使用。ファイルがない場合は空文字が設定される。
        'sPathP インポートするPファイルのパスが設定される。配列の要素は(-1 To ...)、要素 -1 は未使用。ファイルがない場合は空文字が設定される。
        'clsProgressInterface ProgressInterface オブジェクト。
        'bImportCheckG 流用画面でのGlonass衛星に対するチェック状態
        'bImportCheckJ 流用画面でのQZSS衛星に対するチェック状態
        'bImportCheckL 流用画面でのGalileo衛星に対するチェック状態
        'bImportCheckC 流用画面でのBeiDou衛星に対するチェック状態
        'bNavFile 流用画面への遷移フラグ
        '2017/07/05 NS6000対応。'''''''''''''''''''''''''''''''''''''''''''''''''''''''
        'Public Sub ArrangeRinexPath(ByRef sPath() As String, ByRef sPathO() As String, ByRef sPathN() As String, ByRef sPathG() As String, ByVal clsProgressInterface As ProgressInterface)
        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '2020/10/23 FDC 暦ファイルの流用'''''''''''''''''''''''''''''''''''''''''''''''
        'Public Sub ArrangeRinexPath(ByRef sPath() As String, ByRef sPathO() As String, ByRef sPathN() As String, ByRef sPathG() As String, ByRef sPathJ() As String, ByRef sPathL() As String, ByRef sPathC() As String, ByRef sPathP() As String, ByVal clsProgressInterface As ProgressInterface)
        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Public Sub ArrangeRinexPath(ByRef sPath() As String, ByRef sPathO() As String, ByRef sPathN() As String, ByRef sPathG() As String, ByRef sPathJ() As String, ByRef sPathL() As String, ByRef sPathC() As String, ByRef sPathP() As String, ByVal clsProgressInterface As ProgressInterface, ByRef bImportCheckG() As Boolean, ByRef bImportCheckJ() As Boolean, ByRef bImportCheckL() As Boolean, ByRef bImportCheckC() As Boolean, ByRef bNavFile() As Boolean)
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

                '2017/07/05 NS6000対応。'''''''''''''''''''''''''''''''''''''''''''''''''''''''
                '指定されるファイルはoかnかgかjかlかcかp。
                '拡張子の1文字目と2文字目は数字とする。
                'まず指定されたパスの最後の1文字を除いたパスを作成する。
                'そのパスからoがあることを確認し、
                '続いてnかpがあることを確認する。
                'nもpも無ければダイアログで指示してもらう。
                'その後、gjlcの有無を評価する。
    
                ReDim sPathO(-1 To -1)
                ReDim sPathN(-1 To -1)
                ReDim sPathG(-1 To -1)
                '2017/07/05 NS6000対応。'''''''''''''''''''''''''''''''''''''''''''''''''''''''
                ReDim sPathJ(-1 To -1)
                ReDim sPathL(-1 To -1)
                ReDim sPathC(-1 To -1)
                ReDim sPathP(-1 To -1)
                '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                '2020/10/23 FDC 暦ファイルの流用、流用画面の衛星種別追加'''''''''''''''''''''''
                ReDim bImportCheckG(-1 To -1)
                ReDim bImportCheckJ(-1 To -1)
                ReDim bImportCheckL(-1 To -1)
                ReDim bImportCheckC(-1 To -1)
                ReDim bNavFile(-1 To -1)
                '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    
                Dim objTitles As New Collection
    
                '指定されたパスを評価。
                Dim i As Long
                For i = 0 To UBound(sPath)
                    Dim sDrive As String
                    Dim sDir As String
                    Dim sTitle As String
                    Dim sExt As String
                    Call SplitPath(sPath(i), sDrive, sDir, sTitle, sExt)
        
                    '拡張子は３文字。
                    sExt = StrConv(sExt, vbUpperCase)
                    If Len(sExt) <> 4 Then
                        Call MsgBox(MSG_IMPORT_FILENAMEFAILED, vbCritical)
                        Call Err.Raise(cdlCancel)
                    End If
                    '年の評価。
                    Dim nCode() As Byte
                    nCode = StrConv(sExt, vbFromUnicode)
                    If nCode(0) <> &H2E& Then
                        Call MsgBox(MSG_IMPORT_FILENAMEFAILED, vbCritical)
                        Call Err.Raise(cdlCancel)
                    End If
                    If nCode(1) < &H30& Or &H39& < nCode(1) Then
                        Call MsgBox(MSG_IMPORT_FILENAMEFAILED, vbCritical)
                        Call Err.Raise(cdlCancel)
                    End If
                    If nCode(2) < &H30& Or &H39& < nCode(2) Then
                        Call MsgBox(MSG_IMPORT_FILENAMEFAILED, vbCritical)
                        Call Err.Raise(cdlCancel)
                    End If
                    '種別の評価。
                    '2017/07/06 NS6000対応。'''''''''''''''''''''''''''''''''''''''''''''''''''''''
                    'If nCode(3) = &H47& Then
                    '    'Gファイルはとりあえず無視。
                    'ElseIf nCode(3) <> &H4E& And nCode(3) <> &H4F& Then
                    '    Call MsgBox(MSG_IMPORT_FILENAMEFAILED, vbCritical)
                    '    Call Err.Raise(cdlCancel)
                    'Else
                    '    Dim sKey As String
                    '    sKey = StrConv(sDrive & sDir & sTitle & Left$(sExt, 3), vbUpperCase)
                    '    Dim vItem As Variant
                    '    If LookupCollectionVariant(objTitles, vItem, sKey) Then
                    '        'NとO、もしくはPとOが揃った。
                    '        Call objTitles.Remove(sKey)
                    '        Dim nUBound As Long
                    '        nUBound = UBound(sPathO) + 1
                    '        ReDim Preserve sPathO(-1 To nUBound)
                    '        ReDim Preserve sPathN(-1 To nUBound)
                    '        sPathO(nUBound) = Left$(sPath(i), Len(sPath(i)) - 1) & RNX_OBS_EXTENSION '小文字を保持する。
                    '        sPathN(nUBound) = Left$(sPath(i), Len(sPath(i)) - 1) & RNX_NAV_EXTENSION '小文字を保持する。
                    '    Else
                    '        vItem = sPath(i) '小文字を保持する。
                    '        Call objTitles.Add(vItem, sKey)
                    '    End If
                    'End If
                    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                    If nCode(3) = &H4F& Then
                        'Oファイル
                    ElseIf nCode(3) = &H4E& Then
                        'Nファイル
                    ElseIf nCode(3) = &H47& Then
                        'Gファイル
                    'ElseIf nCode(3) = &H4A& Then
                    '    'Jファイル
                    ElseIf nCode(3) = &H51& Then
                        'Qファイル
                    ElseIf nCode(3) = &H4C& Then
                        'Lファイル
                    ElseIf nCode(3) = &H43& Then
                        'Cファイル
                    ElseIf nCode(3) = &H50& Then
                        'Pファイル
                    Else
                        Call MsgBox(MSG_IMPORT_FILENAMEFAILED, vbCritical)
                        Call Err.Raise(cdlCancel)
                    End If


                    Dim sKey As String
                    sKey = StrConv(sDrive & sDir & sTitle & Left$(sExt, 3), vbUpperCase)
                    Dim vItem As Variant
                    'まずoファイルを探して、ヒットすればよしとする。
                    If LookupCollectionVariant(objTitles, vItem, sKey) Then
                        '既に見つけているのでスキップする。
                    Else
                        '検索。
                        vItem = sPath(i) '小文字を保持する。
                        Dim clsFind As FileFind
                        Set clsFind = New FileFind
                        If clsFind.FindFile(sKey & RNX_OBS_EXTENSION) Then
                            vItem = sPath(i) '小文字を保持する。
                            Call objTitles.Add(vItem, sKey)
                        Else
                            Call MsgBox(Left$(vItem, Len(vItem) - 1) & RNX_OBS_EXTENSION & vbCrLf & MSG_IMPORT_FILENOTFOUND, vbCritical)
                            Call Err.Raise(cdlCancel)
                        End If
                    End If
                    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        
                    'プログレス。
                    Call clsProgressInterface.CheckCancel
                Next
    
                '2017/07/05 NS6000対応。'''''''''''''''''''''''''''''''''''''''''''''''''''''''
                '対になるファイルを検索。
                'Dim objObsFiles As New Collection
                'For Each vItem In objTitles
                '    '小文字を保持したパス。
                '    sTitle = Left$(vItem, Len(vItem) - 1)
                '    '検索するファイル。
                '    sKey = StrConv(sTitle, vbUpperCase)
                '    Dim sType As String
                '    If StrConv(Right$(vItem, 1), vbUpperCase) = StrConv(RNX_NAV_EXTENSION, vbUpperCase) Then
                '        sType = RNX_OBS_EXTENSION
                '    Else
                '        sType = RNX_NAV_EXTENSION
                '    End If
                '    '検索。
                '    Dim clsFind As FileFind
                '    Set clsFind = New FileFind
                '    If clsFind.FindFile(sKey & sType) Then
                '        'NとOが揃った。
                '        nUBound = UBound(sPathO) + 1
                '        ReDim Preserve sPathO(-1 To nUBound)
                '        ReDim Preserve sPathN(-1 To nUBound)
                '        sPathO(nUBound) = sTitle & RNX_OBS_EXTENSION '小文字を保持する。
                '        sPathN(nUBound) = sTitle & RNX_NAV_EXTENSION '小文字を保持する。
                '    Else
                '        If sType = RNX_OBS_EXTENSION Then
                '            Call MsgBox(sTitle & sType & vbCrLf & MSG_IMPORT_FILENOTFOUND, vbCritical)
                '            Call Err.Raise(cdlCancel)
                '        Else
                '            'NAVファイルが無い。
                '            Call objObsFiles.Add(vItem, sKey)
                '        End If
                '    End If
                '
                '    'プログレス。
                '    Call clsProgressInterface.CheckCancel
                'Next
                '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                'NかPを検索。
                Dim objObsFiles As New Collection
                For Each vItem In objTitles
                    '小文字を保持したパス。
                    sTitle = Left$(vItem, Len(vItem) - 1)
                    '検索するファイル。
                    sKey = StrConv(sTitle, vbUpperCase)
                    '検索。
                    Set clsFind = New FileFind
                    If clsFind.FindFile(sKey & RNX_MIX_EXTENSION) Then
                        'PとOが揃った。
                        Dim nUBound As Long
                        nUBound = UBound(sPathO) + 1
                        ReDim Preserve sPathO(-1 To nUBound)
                        ReDim Preserve sPathP(-1 To nUBound)
                        sPathO(nUBound) = sTitle & RNX_OBS_EXTENSION '小文字を保持する。
                        sPathP(nUBound) = sTitle & RNX_MIX_EXTENSION '小文字を保持する。
                    ElseIf clsFind.FindFile(sKey & RNX_NAV_EXTENSION) Then
                        'NとOが揃った。
                        nUBound = UBound(sPathO) + 1
                        ReDim Preserve sPathO(-1 To nUBound)
                        ReDim Preserve sPathN(-1 To nUBound)
                        sPathO(nUBound) = sTitle & RNX_OBS_EXTENSION '小文字を保持する。
                        sPathN(nUBound) = sTitle & RNX_NAV_EXTENSION '小文字を保持する。
                    Else
                        'NAVファイルが無い。
                        Call objObsFiles.Add(vItem, sKey)
                    End If
        
                    'プログレス。
                    Call clsProgressInterface.CheckCancel
                Next
                '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    
                '2020/10/23 FDC 暦ファイルの流用、流用画面の衛星種別追加'''''''''''''''''''''''
                nUBound = UBound(sPathO)
                ReDim Preserve bImportCheckG(-1 To nUBound)
                ReDim Preserve bImportCheckJ(-1 To nUBound)
                ReDim Preserve bImportCheckL(-1 To nUBound)
                ReDim Preserve bImportCheckC(-1 To nUBound)
                ReDim Preserve bNavFile(-1 To nUBound)
                '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    
                If objObsFiles.Count > 0 Then
                    'NAVファイルのユーザー指定。
                    Set frmNavFile.ObsFiles = objObsFiles
                    '2017/07/05 NS6000対応。'''''''''''''''''''''''''''''''''''''''''''''''''''''''
                    'Call frmNavFile.SetImportingNavPath(sPathN)
                    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                    Call frmNavFile.SetImportingNavPath(sPathN, sPathP)
                    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        
                    '2020/10/19 FDC 暦ファイルの流用、流用画面の衛星種別追加'''''''''''''''''''''''
                    frmNavFile.TypeEnableFlg = True
                    frmNavFile.m_bGloChecked = False
                    frmNavFile.m_bQzsChecked = False
                    frmNavFile.m_bGalChecked = False
                    frmNavFile.m_bBeiChecked = False
                    frmNavFile.m_sDivertMessage = ""


                    Call MsgBox(MSG_IMPORT_APPLYFAILED, vbCritical)
                    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''


                    Call frmNavFile.Show(1)
                    If frmNavFile.Result<> vbOK Then Call Err.Raise(cdlCancel)
        
                    'ユーザー指定NAVファイルの設定。
                    For Each vItem In objObsFiles
                        Dim vNavPath As Variant
                        If LookupCollectionVariant(frmNavFile.NavFiles, vNavPath, vItem) Then
                            'NとOが揃った。
                            nUBound = UBound(sPathO) + 1
                            '2017/07/05 NS6000対応。'''''''''''''''''''''''''''''''''''''''''''''''''''''''
                            'ReDim Preserve sPathO(-1 To nUbound)
                            'ReDim Preserve sPathN(-1 To nUbound)
                            'sPathO(nUbound) = Left$(vItem, Len(vItem) - 1) & RNX_OBS_EXTENSION '小文字を保持する。
                            'sPathN(nUbound) = vNavPath
                            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                            ReDim Preserve sPathO(-1 To nUBound)
                            sPathO(nUBound) = Left$(vItem, Len(vItem) - 1) & RNX_OBS_EXTENSION '小文字を保持する。
                            If StrConv(Right$(vNavPath, 1), vbUpperCase) = "N" Then
                                ReDim Preserve sPathN(-1 To nUBound)
                                sPathN(nUBound) = vNavPath
                            Else
                                'N以外ならPとみなす。
                                ReDim Preserve sPathP(-1 To nUBound)
                                sPathP(nUBound) = vNavPath
                            End If
                            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                            '2020/10/23 FDC 暦ファイルの流用'''''''''''''''''''''''''''''''''''''''''''''''
                            ReDim Preserve bImportCheckG(-1 To nUBound)
                            ReDim Preserve bImportCheckJ(-1 To nUBound)
                            ReDim Preserve bImportCheckL(-1 To nUBound)
                            ReDim Preserve bImportCheckC(-1 To nUBound)
                            ReDim Preserve bNavFile(-1 To nUBound)
                            bImportCheckG(nUBound) = frmNavFile.GloChecked.Item(vItem)
                            bImportCheckJ(nUBound) = frmNavFile.QzsChecked.Item(vItem)
                            bImportCheckL(nUBound) = frmNavFile.GalChecked.Item(vItem)
                            bImportCheckC(nUBound) = frmNavFile.BeiChecked.Item(vItem)
                            bNavFile(nUBound) = True
                            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                        End If
            
                        'プログレス。
                        Call clsProgressInterface.CheckCancel
                    Next
                End If
    
                'GJQLCファイルの検索。
                nUBound = UBound(sPathO)
                ReDim sPathG(-1 To nUBound)
                '2017/07/05 NS6000対応。'''''''''''''''''''''''''''''''''''''''''''''''''''''''
                ReDim Preserve sPathN(-1 To nUBound)
                ReDim Preserve sPathJ(-1 To nUBound)
                ReDim Preserve sPathL(-1 To nUBound)
                ReDim Preserve sPathC(-1 To nUBound)
                ReDim Preserve sPathP(-1 To nUBound)
                '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                For i = 0 To nUBound
                    '小文字を保持したパス。
                    '2020/10/26 FDC 暦ファイルの流用、流用画面の衛星種別追加'''''''''''''''''''''''
                    'sTitle = Left$(sPathO(i), Len(sPathO(i)) - 1)
                    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                    '検索するファイル。
                    '2017/07/05 NS6000対応。'''''''''''''''''''''''''''''''''''''''''''''''''''''''
                    'sKey = StrConv(sTitle, vbUpperCase)
                    'sType = RNX_GLO_EXTENSION
                    ''検索。
                    'Set clsFind = New FileFind
                    'If clsFind.FindFile(sKey & sType) Then
                    '    'Gファイルが見つかった。
                    '    sPathG(i) = sTitle & RNX_GLO_EXTENSION '小文字を保持する。
                    'Else
                    '    sPathG(i) = ""
                    'End If
                    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                    If sPathP(i) = "" Then
                        '小文字を保持したパス。
                        '2020/10/26 FDC 暦ファイルの流用、流用画面の衛星種別追加'''''''''''''''''''''''
                        If Not bNavFile(i) Then
                            sTitle = Left$(sPathO(i), Len(sPathO(i)) - 1)
                        Else
                            sTitle = Left$(sPathN(i), Len(sPathN(i)) - 1)
                        End If
                        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                        sKey = StrConv(sTitle, vbUpperCase)
                        'G検索。
                        Set clsFind = New FileFind
                        '2020/10/26 FDC 暦ファイルの流用、流用画面の衛星種別追加'''''''''''''''''''''''
                        'If clsFind.FindFile(sKey & RNX_GLO_EXTENSION) Then
                        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                        If clsFind.FindFile(sKey & RNX_GLO_EXTENSION) And(Not bNavFile(i) Or bImportCheckG(i)) Then
                        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                            'Gファイルが見つかった。
                            sPathG(i) = sTitle & RNX_GLO_EXTENSION '小文字を保持する。
                        Else
                            sPathG(i) = ""
                        End If
                        'J検索。
                        Set clsFind = New FileFind
                        '2020/10/26 FDC 暦ファイルの流用、流用画面の衛星種別追加'''''''''''''''''''''''
                        'If clsFind.FindFile(sKey & RNX_QZS_EXTENSION) Then
                        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                        If clsFind.FindFile(sKey & RNX_QZS_EXTENSION) And(Not bNavFile(i) Or bImportCheckJ(i)) Then
                        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                            'Qファイルが見つかった。
                            sPathJ(i) = sTitle & RNX_QZS_EXTENSION '小文字を保持する。
                        '2020/10/26 FDC 暦ファイルの流用、流用画面の衛星種別追加'''''''''''''''''''''''
                        'ElseIf clsFind.FindFile(sKey & "j") Then '2018/07/23 Hitz H.Nakamuar 暦ファイルは複数の拡張子を受け付ける。
                        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                        ElseIf clsFind.FindFile(sKey & "j") And(Not bNavFile(i) Or bImportCheckJ(i)) Then '2018/07/23 Hitz H.Nakamuar 暦ファイルは複数の拡張子を受け付ける。
                        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                            'Jファイルが見つかった。
                            sPathJ(i) = sTitle & "j" '小文字を保持する。
                        Else
                            sPathJ(i) = ""
                        End If
                        'L検索。
                        Set clsFind = New FileFind
                        '2020/10/26 FDC 暦ファイルの流用、流用画面の衛星種別追加'''''''''''''''''''''''
                        'If clsFind.FindFile(sKey & RNX_GAL_EXTENSION) Then
                        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                        If clsFind.FindFile(sKey & RNX_GAL_EXTENSION) And(Not bNavFile(i) Or bImportCheckL(i)) Then
                        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                            'Lファイルが見つかった。
                            sPathL(i) = sTitle & RNX_GAL_EXTENSION '小文字を保持する。
                        Else
                            sPathL(i) = ""
                        End If
                        'C検索。
                        Set clsFind = New FileFind
                        '2020/10/26 FDC 暦ファイルの流用、流用画面の衛星種別追加'''''''''''''''''''''''
                        'If clsFind.FindFile(sKey & RNX_BEI_EXTENSION) Then
                        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                        If clsFind.FindFile(sKey & RNX_BEI_EXTENSION) And(Not bNavFile(i) Or bImportCheckC(i)) Then
                        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                            'Cファイルが見つかった。
                            sPathC(i) = sTitle & RNX_BEI_EXTENSION '小文字を保持する。
                        '2020/10/26 FDC 暦ファイルの流用、流用画面の衛星種別追加'''''''''''''''''''''''
                        'ElseIf clsFind.FindFile(sKey & "f") Then '2018/07/23 Hitz H.Nakamuar 暦ファイルは複数の拡張子を受け付ける。
                        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                        ElseIf clsFind.FindFile(sKey & "f") And(Not bNavFile(i) Or bImportCheckC(i)) Then '2018/07/23 Hitz H.Nakamuar 暦ファイルは複数の拡張子を受け付ける。
                        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                            'Fファイルが見つかった。
                            sPathC(i) = sTitle & "f" '小文字を保持する。
                        Else
                            sPathC(i) = ""
                        End If
                    End If
                    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        
                    'プログレス。
                    Call clsProgressInterface.CheckCancel
                Next

            End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        public void ArrangeRinexPath(ref List<string> sPath, ref List<string> sPathO, ref List<string> sPathN, ref List<string> sPathG, ref List<string> sPathJ, ref List<string> sPathL, ref List<string> sPathC, ref List<string> sPathP, ProgressInterface clsProgressInterface, ref List<bool> bImportCheckG, ref List<bool> bImportCheckJ, ref List<bool> bImportCheckL, ref List<bool> bImportCheckC, ref List<bool> bNavFile)
        {
            //'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            //'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

            //'2017/07/05 NS6000対応。'''''''''''''''''''''''''''''''''''''''''''''''''''''''
            //'指定されるファイルはoかnかgかjかlかcかp。
            //'拡張子の1文字目と2文字目は数字とする。
            //'まず指定されたパスの最後の1文字を除いたパスを作成する。
            //'そのパスからoがあることを確認し、
            //'続いてnかpがあることを確認する。
            //'nもpも無ければダイアログで指示してもらう。
            //'その後、gjlcの有無を評価する。

            sPathO = new List<string>();
            sPathN = new List<string>();
            sPathG = new List<string>();
            //'2017/07/05 NS6000対応。'''''''''''''''''''''''''''''''''''''''''''''''''''''''
            sPathJ = new List<string>();
            sPathL = new List<string>();
            sPathC = new List<string>();
            sPathP = new List<string>();
            //'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            //'2020/10/23 FDC 暦ファイルの流用、流用画面の衛星種別追加'''''''''''''''''''''''
            bImportCheckG = new List<bool>();
            bImportCheckJ = new List<bool>();
            bImportCheckL = new List<bool>();
            bImportCheckC = new List<bool>();
            bNavFile = new List<bool>();
            //'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            //Collection objTitles = new Collection();
            //List<string> objTitles = new List<string>();
            Dictionary<string, object> objTitles = new Dictionary<string, object>();    //4


            //'指定されたパスを評価。
            for (int i2 = 0; i2 <= sPath.Count; i2++)
            {
                string sDrive = "";
                string sDir = "";
                string sTitle = "";
                string sExt = "";
                SplitPath(sPath[i2], ref sDrive, ref sDir, ref sTitle, ref sExt);


                //'拡張子は３文字。
                byte[] bExt = new byte[sExt.Length];
                bExt = StrConv(sExt, DEFINE.vbUpperCase);
                sExt = bExt.ToString();
                if (sExt.Length != 4)
                {
                    //Call MsgBox(MSG_IMPORT_FILENAMEFAILED, vbCritical)
                    //Call Err.Raise(cdlCancel)
                    MessageBox.Show(MSG_IMPORT_FILENAMEFAILED, "エラー発生", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    return;
                }
                //'年の評価。
                byte[] nCode = new byte[sExt.Length];
                nCode = StrConv(sExt, DEFINE.vbFromUnicode);

                if (nCode[0] != 0x2E)
                {
                    //Call MsgBox(MSG_IMPORT_FILENAMEFAILED, vbCritical)
                    MessageBox.Show(MSG_IMPORT_FILENAMEFAILED, "エラー発生", MessageBoxButtons.OK, MessageBoxIcon.Question);
                    return;
                }
                if (nCode[1] < 0x30 || 0x39 < nCode[1])
                {
                    //Call MsgBox(MSG_IMPORT_FILENAMEFAILED, vbCritical)
                    //Call Err.Raise(cdlCancel)
                    MessageBox.Show(MSG_IMPORT_FILENAMEFAILED, "エラー発生", MessageBoxButtons.OK, MessageBoxIcon.Question);
                    return;
                }
                if (nCode[2] < 0x30 || 0x39 < nCode[2])
                {
                    //Call MsgBox(MSG_IMPORT_FILENAMEFAILED, vbCritical)
                    //Call Err.Raise(cdlCancel)
                    MessageBox.Show(MSG_IMPORT_FILENAMEFAILED, "エラー発生", MessageBoxButtons.OK, MessageBoxIcon.Question);
                    return;
                }

                //'種別の評価。
                //'2017/07/06 NS6000対応。'''''''''''''''''''''''''''''''''''''''''''''''''''''''
                //'If nCode(3) = &H47& Then
                //  :        :        :        :        :        :
                //  :   コメントの為、省略     :        :        :
                //  :        :        :        :        :        :
                //'End If
                //'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                if (nCode[3] == 0x4F)
                {
                    //'Oファイル
                }
                else if (nCode[3] == 0x4E)
                {
                    //'Nファイル
                }
                else if (nCode[3] == 0x47)
                {
                    //'Gファイル
                }
                //else if (nCode[3] == 0x4A)
                //{
                //    //'Jファイル
                //}
                else if (nCode[3] == 0x51)
                {
                    //'Qファイル
                }
                else if (nCode[3] == 0x4C)
                {
                    //'Lファイル
                }
                else if (nCode[3] == 0x43)
                {
                    //'Cファイル
                }
                else if (nCode[3] == 0x50)
                {
                    //'Pファイル
                }
                else
                {
                    //Call MsgBox(MSG_IMPORT_FILENAMEFAILED, vbCritical)
                    // Call Err.Raise(cdlCancel)
                    MessageBox.Show(MSG_IMPORT_FILENAMEFAILED, "エラー発生", MessageBoxButtons.OK, MessageBoxIcon.Question);
                }

                byte[] bKey = new byte[sExt.Length];
                string sKey = "";
                bKey = StrConv($"{sDrive}{sDir}{sTitle}{mdiVBfunctions.Left(sExt, 3)}\\", DEFINE.vbUpperCase);
                sKey = bKey.ToString();

                string vItem = "";   // As Variant
                //'まずoファイルを探して、ヒットすればよしとする。
                if (LookupCollectionVariant(objTitles, ref vItem, sKey)){
                    //'既に見つけているのでスキップする。
                }
                else
                {
                    //'検索。
                    vItem = sPath[i2];    //'小文字を保持する。

                    if (System.IO.File.Exists($"sKey & MdlRINEXTYPE.RNX_OBS_EXTENSION"))
                    {
                        vItem = sPath[i2];   //'小文字を保持する。
                        //objTitles.Add(vItem: sKey);
                        //objTitles.Add($"{vItem}:{sKey}");
                    }
                    else
                    {
                        //Call MsgBox(Left$(vItem, Len(vItem) -1) &RNX_OBS_EXTENSION & vbCrLf & MSG_IMPORT_FILENOTFOUND, vbCritical)
                        //Call Err.Raise(cdlCancel)
                        MessageBox.Show($"{mdiVBfunctions.Left(vItem, vItem.Length - 1)}{MdlRINEXTYPE.RNX_OBS_EXTENSION}{DEFINE.vbCrLf}{MSG_IMPORT_FILENOTFOUND}", "エラー発生", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    //'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

                    //'プログレス。
                    clsProgressInterface.CheckCancel();
                }


                //'2017/07/05 NS6000対応。'''''''''''''''''''''''''''''''''''''''''''''''''''''''
                //'対になるファイルを検索。
                //'Dim objObsFiles As New Collection
                //'For Each vItem In objTitles
                //  :        :        :        :        :        :
                //  :   コメントの為、省略     :        :        :
                //  :        :        :        :        :        :
                //'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

                //'NかPを検索。
                //Collection objObsFiles = new Collection();
                List<string> objObsFiles = new List<string>();

                int nUBound = 0;
                foreach (var vItem2 in objTitles)
                {
                    //'小文字を保持したパス。
                    sTitle = mdiVBfunctions.Left(vItem, vItem.Length - 1);
                    //'検索するファイル。
                    byte[] bKey2 = new byte[sTitle.Length];
                    bKey2 = StrConv(sTitle, DEFINE.vbUpperCase);
                    sKey = bKey2.ToString();
                    //'検索。
                    if (System.IO.File.Exists($"{sKey}{MdlRINEXTYPE.RNX_MIX_EXTENSION}"))
                    {
                        //'PとOが揃った。
                        //  Dim nUBound As Long
                        //  nUBound = UBound(sPathO) + 1
                        //  ReDim Preserve sPathO(-1 To nUBound)
                        //  ReDim Preserve sPathP(-1 To nUBound)
                        //  sPathO(nUBound) = sTitle & RNX_OBS_EXTENSION '小文字を保持する。
                        //  sPathP(nUBound) = sTitle & RNX_MIX_EXTENSION '小文字を保持する。
                        sPathO.Add($"{sTitle}{MdlRINEXTYPE.RNX_OBS_EXTENSION}");   //'小文字を保持する。
                        sPathP.Add($"{sTitle}{MdlRINEXTYPE.RNX_MIX_EXTENSION}");   //'小文字を保持する。

                    }
                    else if (System.IO.File.Exists($"{sKey}{MdlRINEXTYPE.RNX_NAV_EXTENSION}"))
                    {
                        //'NとOが揃った。
                        //  nUBound = UBound(sPathO) + 1
                        //  ReDim Preserve sPathO(-1 To nUBound)
                        //  ReDim Preserve sPathN(-1 To nUBound)
                        sPathO.Add($"{sTitle}{MdlRINEXTYPE.RNX_OBS_EXTENSION}");   //'小文字を保持する。
                        sPathN.Add($"{sTitle}{MdlRINEXTYPE.RNX_NAV_EXTENSION}");   //'小文字を保持する。
                    }
                    else
                    {
                        //'NAVファイルが無い。
                        objObsFiles.Add($"{vItem}:{sKey}");
                    }

                    //'プログレス。
                    clsProgressInterface.CheckCancel();
                }


                //  //'2020/10/23 FDC 暦ファイルの流用、流用画面の衛星種別追加'''''''''''''''''''''''
                nUBound = sPathO.Count;
                //  ReDim Preserve bImportCheckG(-1 To nUBound)
                //  ReDim Preserve bImportCheckJ(-1 To nUBound)
                //  ReDim Preserve bImportCheckL(-1 To nUBound)
                //  ReDim Preserve bImportCheckC(-1 To nUBound)
                //  ReDim Preserve bNavFile(-1 To nUBound)
                //  '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

#if false   //瀬戸口より 衛星軌道情報ファイルの選択 のFORM　検討が必要

                '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    
                If objObsFiles.Count > 0 Then
                    'NAVファイルのユーザー指定。
                    Set frmNavFile.ObsFiles = objObsFiles
                    '2017/07/05 NS6000対応。'''''''''''''''''''''''''''''''''''''''''''''''''''''''
                    'Call frmNavFile.SetImportingNavPath(sPathN)
                    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                    Call frmNavFile.SetImportingNavPath(sPathN, sPathP)
                    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        
                    '2020/10/19 FDC 暦ファイルの流用、流用画面の衛星種別追加'''''''''''''''''''''''
                    frmNavFile.TypeEnableFlg = True
                    frmNavFile.m_bGloChecked = False
                    frmNavFile.m_bQzsChecked = False
                    frmNavFile.m_bGalChecked = False
                    frmNavFile.m_bBeiChecked = False
                    frmNavFile.m_sDivertMessage = ""
        
                    Call MsgBox(MSG_IMPORT_APPLYFAILED, vbCritical)
                    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        
                    Call frmNavFile.Show(1)
                    If frmNavFile.Result <> vbOK Then Call Err.Raise(cdlCancel)
        
                    'ユーザー指定NAVファイルの設定。
                    For Each vItem In objObsFiles
                        Dim vNavPath As Variant
                        If LookupCollectionVariant(frmNavFile.NavFiles, vNavPath, vItem) Then
                            'NとOが揃った。
                            nUBound = UBound(sPathO) + 1
                            '2017/07/05 NS6000対応。'''''''''''''''''''''''''''''''''''''''''''''''''''''''
                            'ReDim Preserve sPathO(-1 To nUbound)
                            'ReDim Preserve sPathN(-1 To nUbound)
                            'sPathO(nUbound) = Left$(vItem, Len(vItem) - 1) & RNX_OBS_EXTENSION '小文字を保持する。
                            'sPathN(nUbound) = vNavPath
                            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                            ReDim Preserve sPathO(-1 To nUBound)
                            sPathO(nUBound) = Left$(vItem, Len(vItem) - 1) & RNX_OBS_EXTENSION '小文字を保持する。
                            If StrConv(Right$(vNavPath, 1), vbUpperCase) = "N" Then
                                ReDim Preserve sPathN(-1 To nUBound)
                                sPathN(nUBound) = vNavPath
                            Else
                                'N以外ならPとみなす。
                                ReDim Preserve sPathP(-1 To nUBound)
                                sPathP(nUBound) = vNavPath
                            End If
                            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                            '2020/10/23 FDC 暦ファイルの流用'''''''''''''''''''''''''''''''''''''''''''''''
                            ReDim Preserve bImportCheckG(-1 To nUBound)
                            ReDim Preserve bImportCheckJ(-1 To nUBound)
                            ReDim Preserve bImportCheckL(-1 To nUBound)
                            ReDim Preserve bImportCheckC(-1 To nUBound)
                            ReDim Preserve bNavFile(-1 To nUBound)
                            bImportCheckG(nUBound) = frmNavFile.GloChecked.Item(vItem)
                            bImportCheckJ(nUBound) = frmNavFile.QzsChecked.Item(vItem)
                            bImportCheckL(nUBound) = frmNavFile.GalChecked.Item(vItem)
                            bImportCheckC(nUBound) = frmNavFile.BeiChecked.Item(vItem)
                            bNavFile(nUBound) = True
                            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                        End If
            
                        'プログレス。
                        Call clsProgressInterface.CheckCancel
                    Next
                End If

#endif
                // 'GJQLCファイルの検索。
                //
                //  nUBound = UBound(sPathO)
                nUBound = sPathO.Count;
                //  ReDim sPathG(-1 To nUBound)
                //'2017/07/05 NS6000対応。'''''''''''''''''''''''''''''''''''''''''''''''''''''''
                //  ReDim Preserve sPathN(-1 To nUBound)
                //  ReDim Preserve sPathJ(-1 To nUBound)
                //  ReDim Preserve sPathL(-1 To nUBound)
                //  ReDim Preserve sPathC(-1 To nUBound)
                //  ReDim Preserve sPathP(-1 To nUBound)
                //'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

                for (int i = 0; i <= nUBound; i++)
                {
                    if (sPathP[i] == "")
                    {
                        //'小文字を保持したパス。
                        //'2020/10/26 FDC 暦ファイルの流用、流用画面の衛星種別追加'''''''''''''''''''''''
                        if (!bNavFile[i])
                        {
                            sTitle = mdiVBfunctions.Left(sPathO[i], sPathO[i].Length - 1);
                        }
                        else
                        {
                            sTitle = mdiVBfunctions.Left(sPathN[i], sPathN[i].Length - 1);
                        }
                        //'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                        byte[] bKey2 = new byte[sTitle.Length];
                        bKey2 = StrConv(sTitle, DEFINE.vbUpperCase);
                        sKey = bKey2.ToString();
                        //'G検索。
                        //      Set clsFind = New FileFind
                        //      '2020/10/26 FDC 暦ファイルの流用、流用画面の衛星種別追加'''''''''''''''''''''''
                        //'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                        if (System.IO.File.Exists($"{sKey}{MdlRINEXTYPE.RNX_GLO_EXTENSION}") && (!bNavFile[i] || bImportCheckG[i]))
                        {
                            //'Gファイルが見つかった。
                            sPathG[i] = $"{sTitle}{MdlRINEXTYPE.RNX_GLO_EXTENSION}";    //'小文字を保持する。
                        }
                        else
                        {
                            sPathG[i] = "";
                        }


                        //'J検索。
                        //      Set clsFind = New FileFind
                        //      '2020/10/26 FDC 暦ファイルの流用、流用画面の衛星種別追加'''''''''''''''''''''''
                        //      'If clsFind.FindFile(sKey & RNX_QZS_EXTENSION) Then
                        //'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                        if (System.IO.File.Exists($"{sKey}{MdlRINEXTYPE.RNX_QZS_EXTENSION}") && (!bNavFile[i] || bImportCheckJ[i]))
                        {
                            //'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                            //'Qファイルが見つかった。
                            sPathJ[i] = $"{sTitle}{MdlRINEXTYPE.RNX_QZS_EXTENSION}";    //'小文字を保持する。


                            //  '2020/10/26 FDC 暦ファイルの流用、流用画面の衛星種別追加'''''''''''''''''''''''
                            //  'ElseIf clsFind.FindFile(sKey & "j") Then '2018 / 07 / 23 Hitz H.Nakamuar 暦ファイルは複数の拡張子を受け付ける。
                            //'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                        }
                        else if (System.IO.File.Exists($"{sKey}{MdlRINEXTYPE.RNX_QZS_EXTENSION}") && (!bNavFile[i] || bImportCheckJ[i]))
                        {
                            //'Jファイルが見つかった。
                            sPathJ[i] = $"{sTitle}j";   //'小文字を保持する。
                        }
                        else
                        {
                            sPathJ[i] = "";
                        }

                        //'L検索。
                        //      Set clsFind = New FileFind
                        //      '2020/10/26 FDC 暦ファイルの流用、流用画面の衛星種別追加'''''''''''''''''''''''
                        //      'If clsFind.FindFile(sKey & RNX_GAL_EXTENSION) Then
                        //'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                        if (System.IO.File.Exists($"{sKey}{MdlRINEXTYPE.RNX_GAL_EXTENSION}") && (!bNavFile[i] || bImportCheckL[i]))
                        {
                            //'Lファイルが見つかった。
                            sPathG[i] = $"{sTitle}{MdlRINEXTYPE.RNX_GAL_EXTENSION}";    //'小文字を保持する。
                        }
                        else
                        {
                            sPathL[i] = "";
                        }


                        //'C検索。
                        //      Set clsFind = New FileFind
                        //      '2020/10/26 FDC 暦ファイルの流用、流用画面の衛星種別追加'''''''''''''''''''''''
                        //      'If clsFind.FindFile(sKey & RNX_BEI_EXTENSION) Then
                        //'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                        if (System.IO.File.Exists($"{sKey}{MdlRINEXTYPE.RNX_BEI_EXTENSION}") && (!bNavFile[i] || bImportCheckC[i]))
                        {
                            //'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                            //'Cファイルが見つかった。
                            sPathC[i] = $"{sTitle}{MdlRINEXTYPE.RNX_BEI_EXTENSION}";    //'小文字を保持する。

                            //'2020/10/26 FDC 暦ファイルの流用、流用画面の衛星種別追加'''''''''''''''''''''''
                            //'ElseIf clsFind.FindFile(sKey & "f") Then '2018 / 07 / 23 Hitz H.Nakamuar 暦ファイルは複数の拡張子を受け付ける。
                            //'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                        }
                        else if (System.IO.File.Exists($"{sKey}f") && (!bNavFile[i] || bImportCheckC[i])) //'2018/07/23 Hitz H.Nakamuar 暦ファイルは複数の拡張子を受け付ける。
                        {
                            //'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                            //'Fファイルが見つかった。
                            sPathC[i] = $"{sTitle}f";   //'小文字を保持する。
                        }
                        else
                        {
                            sPathC[i] = "";
                        }


                    }
                    //'プログレス。
                    clsProgressInterface.CheckCancel();

                }

            }

        }


#if false




'convertToRINEX のパス。
Public Function GetCtrPath() As String

    On Error GoTo FileErrorHandler

    Dim sPath As String
    sPath = App.Path & CTR_PATH
        If Not IsFolderExists(sPath & "\") Then
        Dim sProgramFiles As String
        sProgramFiles = GetShellFolderPath(CSIDL_PROGRAM_FILES)
        If sProgramFiles = "" Then Call Err.Raise(ERR_FATAL)
        Dim sInstallFolder As String
        sInstallFolder = GetPrivateProfileString(PROFILE_DEF_SEC_HELP, PROFILE_DEF_KEY_INSTALLFOLDER, "\" & App.Title, App.Path & "\" & PROFILE_DEF_NAME)


        sPath = sProgramFiles & sInstallFolder & CTR_PATH
    End If


    GetCtrPath = sPath


FileErrorHandler:
        End Function

'DATファイルを読み込む。
'
'sPath で指定されるDATファイルを読み込み、ObservationPoint オブジェクトを生成する。
'
'引き数：
'sPath 読み込むファイルのパス。
'clsProgressInterface ProgressInterface オブジェクト。
'戻り値：生成した ObservationPoint オブジェクト(実観測点)を返す。
Public Function ReadDatFile(ByVal sPath As String, ByVal clsProgressInterface As ProgressInterface) As ObservationPoint

    On Error GoTo FileErrorHandler


    'テンポラリフォルダ。
    Dim sTempDir As String
    sTempDir = App.Path & TEMPORARY_PATH & IMPORT_TEMPPATH
    Call CreateDir(sTempDir, True)


    Dim sNs5rinDir As String
    sNs5rinDir = App.Path & TEMPORARY_PATH & NS5RIN_TEMPPATH
    Call CreateDir(sNs5rinDir, True)


    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    '2018/10/03 Hitz H.Nakamura
    'テンポラリファイルが削除されない。
    '記録として残るのは良いので、終わったら削除するのではなく最初に削除することとする。
    Call EmptyDir(sNs5rinDir, True)
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''


    '定義ファイル。
    Dim sRecPath As String
    Dim sAntPath As String
    sRecPath = App.Path & "\" & PROFILE_RCV_FILE
    sAntPath = App.Path & "\" & PROFILE_ANT_FILE


    'convertToRINEX フォルダ。
    Dim sCtrDir As String
    sCtrDir = GetCtrPath()
    Call CreateDir(sCtrDir, True)


    'プログレス。
    Call clsProgressInterface.CheckCancel
    clsProgressInterface.Prompt = sPath & vbCrLf & "のｲﾝﾎﾟｰﾄ中･･･"


    Dim nDatType As Long
    If CheckDatType(sPath, 0, "", nDatType) <> 0 Then Call Err.Raise(ERR_RINEX, , MSG_IMPORT_NGS2RIN)


    'プログレス。
    Call clsProgressInterface.CheckCancel


    'RINEX変換。
    Dim rnx_inp_usr As RNX_INP
    Dim rnx_obs_usr As RNX_OBS_INFO
    Dim rnx_nav_usr As RNX_NAV_INFO
    Dim rnx_raw_usr As RNX_RAW_DATA2
    Dim rnx_raw_usr4 As RNX_RAW_DATA4
    Dim rnx_eph_usr As RNX_EPH_DATA2
    Dim rnx_eph_usr4 As RNX_EPH_DATA4
    Dim ns3obs As NS3_OBS
    Dim ns3eph As NS3_EPH
    Dim ns4obs As NS4_OBS
    Dim ns4eph As NS4_EPH
    Dim ns4geph As NS4_GEPH


    If nDatType = NGS_DAT_TYPE_NS2 Then
        If GetSVInfo(sPath, sTempDir, sAntPath, sRecPath, 0, "", rnx_inp_usr, rnx_obs_usr, rnx_nav_usr, rnx_raw_usr, rnx_eph_usr) <> 0 Then Call Err.Raise(ERR_RINEX, , MSG_IMPORT_NGS2RIN)
    ElseIf nDatType = NGS_DAT_TYPE_NS3 Then
        Call InitSVDataNS3(ns3obs, ns3eph)
        If GetSVInfoNS3(sPath, sTempDir, sAntPath, sRecPath, 0, "", rnx_inp_usr, rnx_obs_usr, rnx_nav_usr, rnx_raw_usr, rnx_eph_usr, ns3obs, ns3eph) <> 0 Then Call Err.Raise(ERR_RINEX, , MSG_IMPORT_NGS2RIN)
    ElseIf nDatType = (NGS_DAT_TYPE_GLO Or NGS_DAT_TYPE_NS3) Then
        Call InitSVDataNS4(ns4obs, ns4eph, ns4geph)
        If GetSVInfoNS4(sPath, sTempDir, sAntPath, sRecPath, 0, "", rnx_inp_usr, rnx_obs_usr, rnx_nav_usr, rnx_raw_usr4, rnx_eph_usr4, ns4obs, ns4eph, ns4geph) <> 0 Then Call Err.Raise(ERR_RINEX, , MSG_IMPORT_NGS2RIN)
        ElseIf(nDatType And NGS_DAT_TYPE_NS5) <> 0 Then
        ElseIf(nDatType And NGS_DAT_TYPE_NS51) <> 0 Then
        ElseIf(nDatType And NGS_DAT_TYPE_NS6) <> 0 Then
    Else
        Call Err.Raise(ERR_RINEX, , MSG_IMPORT_NGS2RIN)
    End If
    
    'プログレス。
    Call clsProgressInterface.CheckCancel


    rnx_inp_usr.rnx_name_type = RNX_FILE_NAME_SRC
    rnx_inp_usr.run_by = "NS-Survey" & vbNullChar
    rnx_inp_usr.observer = "" & vbNullChar
    rnx_inp_usr.agency = "" & vbNullChar
    rnx_inp_usr.have_run_by = 1
    rnx_inp_usr.have_observer = 1
    rnx_inp_usr.have_agency = 1


    If nDatType = NGS_DAT_TYPE_NS2 Then
        If ConvertRinex(sPath, sTempDir, sAntPath, sRecPath, 0, "", rnx_inp_usr, rnx_obs_usr, rnx_nav_usr, rnx_raw_usr, rnx_eph_usr) <> 0 Then Call Err.Raise(ERR_RINEX, , MSG_IMPORT_NGS2RIN)
    ElseIf nDatType = NGS_DAT_TYPE_NS3 Then
        If ConvertRinexNS3(sPath, sTempDir, sAntPath, sRecPath, 0, "", rnx_inp_usr, rnx_obs_usr, rnx_nav_usr, rnx_raw_usr, rnx_eph_usr, ns3obs, ns3eph) <> 0 Then Call Err.Raise(ERR_RINEX, , MSG_IMPORT_NGS2RIN)
    ElseIf(nDatType And NGS_DAT_TYPE_NS3) <> 0 Then
        If ConvertRinexNS4(sPath, sTempDir, sAntPath, sRecPath, 0, "", rnx_inp_usr, rnx_obs_usr, rnx_nav_usr, rnx_raw_usr4, rnx_eph_usr4, ns4obs, ns4eph, ns4geph) <> 0 Then Call Err.Raise(ERR_RINEX, , MSG_IMPORT_NGS2RIN)
    ElseIf(nDatType And NGS_DAT_TYPE_NS5) <> 0 Then
        If ConvertNS5(sPath, sTempDir, sAntPath, sRecPath, "", sCtrDir, sNs5rinDir, rnx_inp_usr) <> 0 Then Call Err.Raise(ERR_RINEX, , MSG_IMPORT_NGS2RIN)
    ElseIf(nDatType And NGS_DAT_TYPE_NS51) <> 0 Then
        Dim param51 As param51
        param51.run_by = rnx_inp_usr.run_by
        param51.observer = rnx_inp_usr.observer
        param51.agency = rnx_inp_usr.agency
        If ConvertNS51(sPath, sTempDir, sAntPath, sRecPath, "", sNs5rinDir, param51) <> 0 Then Call Err.Raise(ERR_RINEX, , MSG_IMPORT_NGS2RIN)
        rnx_inp_usr.src_fname = param51.src_fname
        rnx_inp_usr.extension = param51.extension
    '2017/06/06 NS6000対応。'''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ElseIf (nDatType And NGS_DAT_TYPE_NS6) <> 0 Then
        Dim p6 As PARAM6
        p6.run_by = rnx_inp_usr.run_by
        p6.observer = rnx_inp_usr.observer
        p6.agency = rnx_inp_usr.agency
        If ConvertNS6(sPath, sTempDir, sAntPath, sRecPath, "", sNs5rinDir, p6) <> 0 Then Call Err.Raise(ERR_RINEX, , MSG_IMPORT_NGS2RIN)
        rnx_inp_usr.src_fname = p6.src_fname
        rnx_inp_usr.extension = p6.extension
    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Else
    End If
    
    'プログレス。
    Call clsProgressInterface.CheckCancel

    If nDatType = NGS_DAT_TYPE_NS2 Then
        If MakeSVInfo(sPath, sTempDir, sAntPath, sRecPath, 0, "", rnx_inp_usr, rnx_obs_usr, rnx_nav_usr, rnx_raw_usr, rnx_eph_usr) <> 0 Then Call Err.Raise(ERR_RINEX, , MSG_IMPORT_NGS2RIN)
    ElseIf nDatType = NGS_DAT_TYPE_NS3 Then
        If MakeSVInfoNS3(sPath, sTempDir, sAntPath, sRecPath, 0, "", rnx_inp_usr, rnx_obs_usr, rnx_nav_usr, rnx_raw_usr, rnx_eph_usr, ns3obs, ns3eph) <> 0 Then Call Err.Raise(ERR_RINEX, , MSG_IMPORT_NGS2RIN)
        Call DestroySVDataNS3(ns3obs, ns3eph)
    ElseIf(nDatType And NGS_DAT_TYPE_NS3) <> 0 Then
        If MakeSVInfoNS4(sPath, sTempDir, sAntPath, sRecPath, 0, "", rnx_inp_usr, rnx_obs_usr, rnx_nav_usr, rnx_raw_usr4, rnx_eph_usr4, ns4obs, ns4eph, ns4geph) <> 0 Then Call Err.Raise(ERR_RINEX, , MSG_IMPORT_NGS2RIN)
        Call DestroySVDataNS4(ns4obs, ns4eph, ns4geph)
    Else
    End If
    
    'プログレス。
    Call clsProgressInterface.CheckCancel
    
    '衛星情報ファイルの読み込み。
    Dim clsObservationPoint As ObservationPoint
    Set clsObservationPoint = ReadSvInfo(sPath, sTempDir, FixedStringToString(rnx_inp_usr.src_fname), FixedStringToString(rnx_inp_usr.extension), clsProgressInterface)


    If nDatType = NGS_DAT_TYPE_NS2 Then
        clsObservationPoint.ImportType = IMPORT_TYPE_DAT
        clsObservationPoint.GlonassFlag = False
        '2017/06/06 NS6000対応。'''''''''''''''''''''''''''''''''''''''''''''''''''''''
        clsObservationPoint.QZSSFlag = False
        clsObservationPoint.GalileoFlag = False
        clsObservationPoint.BeiDouFlag = False
        clsObservationPoint.MixedNav = False
        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ElseIf (nDatType And NGS_DAT_TYPE_NS3) <> 0 Then
        clsObservationPoint.ImportType = IMPORT_TYPE_NS3
        '2021/10/15 Hitz H.Nakamura
        '衛星情報ファイルの衛星の健康状態でON/OFFを判断している。衛星が一つも無い場合はOFFになっているのでその場合はOFFのままにする。衛星が一つ以上捕捉できていて、観測時にONになっていた場合のみ、ONにする。
        If clsObservationPoint.GlonassFlag Then
            If (nDatType And NGS_DAT_TYPE_GLO) = 0 Then
                clsObservationPoint.GlonassFlag = False
            End If
        End If
        '2017/06/06 NS6000対応。'''''''''''''''''''''''''''''''''''''''''''''''''''''''
        clsObservationPoint.QZSSFlag = False
        clsObservationPoint.GalileoFlag = False
        clsObservationPoint.BeiDouFlag = False
        clsObservationPoint.MixedNav = False
        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ElseIf (nDatType And NGS_DAT_TYPE_NS5) <> 0 Then
        clsObservationPoint.ImportType = IMPORT_TYPE_NS5
        '2021/10/15 Hitz H.Nakamura
        '衛星情報ファイルの衛星の健康状態でON/OFFを判断している。衛星が一つも無い場合はOFFになっているのでその場合はOFFのままにする。衛星が一つ以上捕捉できていて、観測時にONになっていた場合のみ、ONにする。
        If clsObservationPoint.GlonassFlag Then
            If (nDatType And NGS_DAT_TYPE_GLO) = 0 Then
                clsObservationPoint.GlonassFlag = False
            End If
        End If
        '2017/06/06 NS6000対応。'''''''''''''''''''''''''''''''''''''''''''''''''''''''
        clsObservationPoint.QZSSFlag = False
        clsObservationPoint.GalileoFlag = False
        clsObservationPoint.BeiDouFlag = False
        clsObservationPoint.MixedNav = False
        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    '2017/12/21 NS6000対応。'''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ElseIf (nDatType And NGS_DAT_TYPE_NS51) <> 0 Then
        clsObservationPoint.ImportType = IMPORT_TYPE_NS51
        '2021/10/15 Hitz H.Nakamura
        '衛星情報ファイルの衛星の健康状態でON/OFFを判断している。衛星が一つも無い場合はOFFになっているのでその場合はOFFのままにする。衛星が一つ以上捕捉できていて、観測時にONになっていた場合のみ、ONにする。
        If clsObservationPoint.GlonassFlag Then
            If (nDatType And NGS_DAT_TYPE_GLO) = 0 Then
                clsObservationPoint.GlonassFlag = False
            End If
        End If
        If clsObservationPoint.QZSSFlag Then
            If (nDatType And NGS_DAT_TYPE_QZS) = 0 Then
                clsObservationPoint.QZSSFlag = False
            End If
        End If
        '2021/10/15 Hitz H.Nakamura
        'REでもGalileoとBeiDouをONにすることを許可する。
        If clsObservationPoint.GalileoFlag Then
            If (nDatType And NGS_DAT_TYPE_GAL) = 0 Then
                clsObservationPoint.GalileoFlag = False
            End If
        End If
        If clsObservationPoint.BeiDouFlag Then
            If (nDatType And NGS_DAT_TYPE_BEI) = 0 Then
                clsObservationPoint.BeiDouFlag = False
            End If
        End If
        clsObservationPoint.MixedNav = True
    Else
        clsObservationPoint.ImportType = IMPORT_TYPE_NS6
        '2021/10/15 Hitz H.Nakamura
        '衛星情報ファイルの衛星の健康状態でON/OFFを判断している。衛星が一つも無い場合はOFFになっているのでその場合はOFFのままにする。衛星が一つ以上捕捉できていて、観測時にONになっていた場合のみ、ONにする。
        If clsObservationPoint.GlonassFlag Then
            If (nDatType And NGS_DAT_TYPE_GLO) = 0 Then
                clsObservationPoint.GlonassFlag = False
            End If
        End If
        If clsObservationPoint.QZSSFlag Then
            If (nDatType And NGS_DAT_TYPE_QZS) = 0 Then
                clsObservationPoint.QZSSFlag = False
            End If
        End If
        If clsObservationPoint.GalileoFlag Then
            If (nDatType And NGS_DAT_TYPE_GAL) = 0 Then
                clsObservationPoint.GalileoFlag = False
            End If
        End If
        If clsObservationPoint.BeiDouFlag Then
            If (nDatType And NGS_DAT_TYPE_BEI) = 0 Then
                clsObservationPoint.BeiDouFlag = False
            End If
        End If
        clsObservationPoint.MixedNav = True
    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    End If


    Set ReadDatFile = clsObservationPoint


    Exit Function


FileErrorHandler:
    If nDatType = NGS_DAT_TYPE_NS2 Then
        Call ExitSVData
    ElseIf nDatType = NGS_DAT_TYPE_NS3 Then
        Call DestroySVDataNS3(ns3obs, ns3eph)
    ElseIf nDatType = (NGS_DAT_TYPE_GLO Or NGS_DAT_TYPE_NS3) Then
        Call DestroySVDataNS4(ns4obs, ns4eph, ns4geph)
    End If
    If Err.Number = cdlCancel Then Call Err.Raise(Err.Number, Err.Source, Err.Description, Err.HelpFile, Err.HelpContext)
    Call MsgBox(Err.Description, vbCritical)

End Function


#endif


        //24/02/21 K.setoguchi@NV---------->>>>>>>>>>
        //***************************************************************************
        //==========================================================================================
        /*[VB]
            'RINEXファイルを読み込む。
            '
            'sPathO と sPathN で指定されるRINEXファイルを読み込み、ObservationPoint オブジェクトを生成する。
            '
            '引き数：
            'sPathO 読み込むOファイルのパス。
            'sPathN 読み込むNファイルのパス。ファイルの指定がない場合は空文字。
            'sPathG 読み込むGファイルのパス。ファイルの指定がない場合は空文字。
            'sPathJ 読み込むJファイルのパス。ファイルの指定がない場合は空文字。
            'sPathL 読み込むLファイルのパス。ファイルの指定がない場合は空文字。
            'sPathC 読み込むCファイルのパス。ファイルの指定がない場合は空文字。
            'sPathP 読み込むPファイルのパス。ファイルの指定がない場合は空文字。
            'clsProgressInterface ProgressInterface オブジェクト。
            'bImportCheckG 流用画面でのGlonass衛星に対するチェック状態
            'bImportCheckJ 流用画面でのQZSS衛星に対するチェック状態
            'bImportCheckL 流用画面でのGalileo衛星に対するチェック状態
            'bImportCheckC 流用画面でのBeiDou衛星に対するチェック状態
            'bNavFile 流用画面への遷移フラグ
            '戻り値：生成した ObservationPoint オブジェクト(実観測点)を返す。
            '2017/07/05 NS6000対応。'''''''''''''''''''''''''''''''''''''''''''''''''''''''
            'Public Function ReadRinexFile(ByVal sPathO As String, ByVal sPathN As String, ByVal sPathG As String, ByVal clsProgressInterface As ProgressInterface) As ObservationPoint
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            '2020/10/23 FDC 暦ファイルの流用'''''''''''''''''''''''''''''''''''''''''''''''
            'Public Function ReadRinexFile(ByVal sPathO As String, ByVal sPathN As String, ByVal sPathG As String, ByVal sPathJ As String, ByVal sPathL As String, ByVal sPathC As String, ByVal sPathP As String, ByVal clsProgressInterface As ProgressInterface) As ObservationPoint
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            Public Function ReadRinexFile(ByVal sPathO As String, ByRef sPathN As String, ByVal sPathG As String, ByVal sPathJ As String, ByVal sPathL As String, ByVal sPathC As String, ByRef sPathP As String, ByVal clsProgressInterface As ProgressInterface, ByVal bImportCheckG As Boolean, ByVal bImportCheckJ As Boolean, ByVal bImportCheckL As Boolean, ByVal bImportCheckC As Boolean, ByVal bNavFlg As Boolean) As ObservationPoint
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

                On Error GoTo FileErrorHandler
    
                'テンポラリフォルダ。
                Dim sTempDir As String
                sTempDir = App.Path & TEMPORARY_PATH & IMPORT_TEMPPATH
                Call CreateDir(sTempDir, True)
    
                '定義ファイル。
                Dim sRecPath As String
                Dim sAntPath As String
                sRecPath = App.Path & "\" & PROFILE_RCV_FILE
                sAntPath = App.Path & "\" & PROFILE_ANT_FILE
    
                '衛星情報ファイル。
                Dim sDrive As String
                Dim sDir As String
                Dim sTitle As String
                Dim sExt As String
                Call SplitPath(sPathO, sDrive, sDir, sTitle, sExt)
                Dim sSvInfoPath As String
                sSvInfoPath = sTempDir & sTitle & "." & RNX_SV_EXTENSION
    
                'プログレス。
                Call clsProgressInterface.CheckCancel
                clsProgressInterface.Prompt = sPathO & vbCrLf & "のｲﾝﾎﾟｰﾄ中･･･"
    
                '2017/12/21 NS6000対応。'''''''''''''''''''''''''''''''''''''''''''''''''''''''
                'RINEXファイルのバージョン。
                Dim nRinexVersion As Single
                nRinexVersion = CheckVersionRNX(sPathO)
                '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                '2020/11/09 FDC 暦ファイルの流用、混合ファイルの分割実行判定'''''''''''''''''''
                Dim bSplit As Boolean
                bSplit = False
                '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    
                'RINEX取得。
                '2017/12/21 NS6000対応。'''''''''''''''''''''''''''''''''''''''''''''''''''''''
                'If MakeSVInfoRNX(sPathO, sPathN, sSvInfoPath, sAntPath, sRecPath, 0, "") <> 0 Then Call Err.Raise(ERR_RINEX, , MSG_IMPORT_NGSRDRIN)
                'If MakeSVInfo2RNX(sPathO, sPathN, sPathG, sSvInfoPath, sAntPath, sRecPath, 0, "") <> 0 Then Call Err.Raise(ERR_RINEX, , MSG_IMPORT_NGSRDRIN)
                '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                If nRinexVersion < 2.1 Then
                    Call MsgBox(MSG_IMPORT_RINEXVERSIONFAILED, vbCritical)
                    Call Err.Raise(cdlCancel)
                '2020/10/21 FDC RINEX2.12対応''''''''''''''''''''''''''''''''''''''''''''''''''
                'ElseIf nRinexVersion < 2.12 Then
                '    If MakeSVInfo2RNX(sPathO, sPathN, sPathG, sSvInfoPath, sAntPath, sRecPath, 0, "") <> 0 Then Call Err.Raise(ERR_RINEX, , MSG_IMPORT_NGSRDRIN)
                ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                '2018/05/18 Hitz H.Nakamura RINEX3.00も書式は同じで ngs_rdrin302 でも読み込める。
                'ElseIf nRinexVersion < 3.02 Then
                '    Call MsgBox(MSG_IMPORT_RINEXVERSIONFAILED, vbCritical)
                '    Call Err.Raise(cdlCancel)
                'ElseIf nRinexVersion < 3# Then
                '    Call MsgBox(MSG_IMPORT_RINEXVERSIONFAILED, vbCritical)
                '    Call Err.Raise(cdlCancel)
                ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                ElseIf nRinexVersion < 3# Then
                    '混合ファイルの場合には分割を行う
                    If sPathP <> "" Then
                        Dim sSplitPath As String
                        sSplitPath = sTempDir & IMPORT_SPLITPATH
                        Dim sTmpPath As String
                        sTmpPath = sSplitPath & sTitle & "." & Left$(Right$(sPathO, 3), 2)
                        If Dir(sSplitPath, vbDirectory) <> "" Then
                            Call EmptyDir(sSplitPath)
                        Else
                            Call CreateDir(sSplitPath)
                        End If
                        '分割実施
                        If SplitRNX(sPathP, sTmpPath & RNX_NAV_EXTENSION, sTmpPath & RNX_GLO_EXTENSION, sTmpPath & RNX_QZS_EXTENSION, sTmpPath & RNX_GAL_EXTENSION, sTmpPath & RNX_BEI_EXTENSION) <> 0 Then Call Err.Raise(ERR_RINEX, , MSG_IMPORT_SPLIT)
                        sPathP = ""
                        sPathN = sTmpPath & RNX_NAV_EXTENSION
                        If ReadFile(sTmpPath & RNX_GLO_EXTENSION) Then
                            sPathG = sTmpPath & RNX_GLO_EXTENSION
                        End If
                        bSplit = True
                    End If

                    If MakeSVInfo2RNX(sPathO, sPathN, sPathG, sSvInfoPath, sAntPath, sRecPath, 0, "") <> 0 Then Call Err.Raise(ERR_RINEX, , MSG_IMPORT_NGSRDRIN)
                '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                Else
                    If MakeSVInfo302RNX(sPathO, sPathN, sPathG, sPathJ, sPathL, sPathC, sPathP, sSvInfoPath, sAntPath, sRecPath, 0, "", sTempDir) <> 0 Then Call Err.Raise(ERR_RINEX, , MSG_IMPORT_NGSRDRIN)
                End If
                '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    
                'プログレス。
                Call clsProgressInterface.CheckCancel
    
                '衛星情報ファイルの読み込み。
                Dim sRinexExt As String
                sRinexExt = Left$(Right$(sPathO, 3), 2)
                Dim clsObservationPoint As ObservationPoint
                Set clsObservationPoint = ReadSvInfo(sPathO, sTempDir, sTitle, sRinexExt, clsProgressInterface)


                clsObservationPoint.ImportType = IMPORT_TYPE_RINEX
                '2020/10/23 FDC 暦ファイルの流用'''''''''''''''''''''''''''''''''''''''''''''''
                If Not bNavFlg Then
                '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                '2017/06/06 NS6000対応。'''''''''''''''''''''''''''''''''''''''''''''''''''''''
                'clsObservationPoint.GlonassFlag = (sPathG <> "")
                '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                    If Not clsObservationPoint.GlonassFlag Then
                        clsObservationPoint.GlonassFlag = (sPathG<> "")
                    End If
                    If sPathP = "" Then
                        If clsObservationPoint.GlonassFlag And(sPathG = "") Then
                            'Call MsgBox(Left$(sPathO, Len(sPathO) - 1) & RNX_GLO_EXTENSION & vbCrLf & MSG_IMPORT_FILENOTFOUND, vbCritical)
                            'Call Err.Raise(cdlCancel)
                            clsObservationPoint.GlonassFlag = False
                        End If
                        If clsObservationPoint.QZSSFlag And(sPathJ = "") Then
                            'Call MsgBox(Left$(sPathO, Len(sPathO) - 1) & RNX_QZS_EXTENSION & vbCrLf & MSG_IMPORT_FILENOTFOUND, vbCritical)
                            'Call Err.Raise(cdlCancel)
                            clsObservationPoint.QZSSFlag = False
                        End If
                        If clsObservationPoint.GalileoFlag And(sPathL = "") Then
                            'Call MsgBox(Left$(sPathO, Len(sPathO) - 1) & RNX_GAL_EXTENSION & vbCrLf & MSG_IMPORT_FILENOTFOUND, vbCritical)
                            'Call Err.Raise(cdlCancel)
                            clsObservationPoint.GalileoFlag = False
                        End If
                        If clsObservationPoint.BeiDouFlag And(sPathC = "") Then
                            'Call MsgBox(Left$(sPathO, Len(sPathO) - 1) & RNX_BEI_EXTENSION & vbCrLf & MSG_IMPORT_FILENOTFOUND, vbCritical)
                            'Call Err.Raise(cdlCancel)
                            clsObservationPoint.BeiDouFlag = False
                        End If
                        clsObservationPoint.MixedNav = False
                    Else
                        clsObservationPoint.MixedNav = True
                    End If
                '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                '2020/10/23 FDC 暦ファイルの流用'''''''''''''''''''''''''''''''''''''''''''''''
                Else
                    clsObservationPoint.GlonassFlag = bImportCheckG
                    clsObservationPoint.QZSSFlag = bImportCheckJ
                    clsObservationPoint.GalileoFlag = bImportCheckL
                    clsObservationPoint.BeiDouFlag = bImportCheckC
                    If sPathP = "" Then
                        clsObservationPoint.MixedNav = False
                    Else
                        clsObservationPoint.MixedNav = True
                    End If
                End If
                '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    
                'RINEXファイルのコピー。
                Call CopyFile(sPathO, sTempDir & sTitle & "." & sRinexExt & RNX_OBS_EXTENSION, True)
                '2017/07/05 NS6000対応。'''''''''''''''''''''''''''''''''''''''''''''''''''''''
                'Call CopyFile(sPathN, sTempDir & sTitle & "." & sRinexExt & RNX_NAV_EXTENSION, True)
                'If sPathG <> "" Then
                '    Call CopyFile(sPathG, sTempDir & sTitle & "." & sRinexExt & RNX_GLO_EXTENSION, True)
                'End If
                '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                If sPathP<> "" Then
                    Call CopyFile(sPathP, sTempDir & sTitle & "." & sRinexExt & RNX_MIX_EXTENSION, True)
                    '2020/10/23 FDC 暦ファイルの流用'''''''''''''''''''''''''''''''''''''''''''''''
                    sPathP = sTempDir & sTitle & "." & sRinexExt & RNX_MIX_EXTENSION
                    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                Else
                    If sPathN<> "" Then
                        Call CopyFile(sPathN, sTempDir & sTitle & "." & sRinexExt & RNX_NAV_EXTENSION, True)
                        '2020/10/23 FDC 暦ファイルの流用'''''''''''''''''''''''''''''''''''''''''''''''
                        sPathN = sTempDir & sTitle & "." & sRinexExt & RNX_NAV_EXTENSION
                        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                    End If
                    If sPathG<> "" Then
                        Call CopyFile(sPathG, sTempDir & sTitle & "." & sRinexExt & RNX_GLO_EXTENSION, True)
                    End If
                    If sPathJ<> "" Then
                        Call CopyFile(sPathJ, sTempDir & sTitle & "." & sRinexExt & RNX_QZS_EXTENSION, True)
                    End If
                    If sPathL<> "" Then
                        Call CopyFile(sPathL, sTempDir & sTitle & "." & sRinexExt & RNX_GAL_EXTENSION, True)
                    End If
                    If sPathC<> "" Then
                        Call CopyFile(sPathC, sTempDir & sTitle & "." & sRinexExt & RNX_BEI_EXTENSION, True)
                    End If
                End If
                '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                '2020/11/09 FDC 暦ファイルの流用、分割ファイルの削除'''''''''''''''''''''''''''
                If bSplit Then
                    Call DeleteDir(sSplitPath)
                End If
                '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''


                Set ReadRinexFile = clsObservationPoint


                Exit Function


            FileErrorHandler:
                If Err.Number = cdlCancel Then Call Err.Raise(Err.Number, Err.Source, Err.Description, Err.HelpFile, Err.HelpContext)
                Call MsgBox(Err.Description, vbCritical)

            End Function

            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        public ObservationPoint ReadRinexFile(string sPathO, ref string sPathN, string sPathG, string sPathJ, string sPathL, string sPathC, ref string sPathP, ProgressInterface clsProgressInterface, bool bImportCheckG, bool bImportCheckJ, bool bImportCheckL, bool bImportCheckC, bool bNavFlg)
        {

            ObservationPoint observationPoint = new ObservationPoint();

            //On Error GoTo FileErrorHandler
            try
            {
                //'テンポラリフォルダ。
                string sTempDir = "";
                string App_Path = @"C:\Develop\NetSurv\Src\NS-App\NS-Survey";
                sTempDir = $"{App_Path}{MdlNSDefine.TEMPORARY_PATH}{IMPORT_TEMPPATH}";
                _ = mdlUtility.CreateDir(sTempDir, true);


                //'定義ファイル。
                string sRecPath;
                string sAntPath;
                sRecPath = $"{App_Path}\\{MdlNSDefine.PROFILE_RCV_FILE}";
                sAntPath = $"{App_Path}\\{MdlNSDefine.PROFILE_ANT_FILE}";

                //'衛星情報ファイル。
                string sDrive = "";
                string sDir = "";
                string sTitle = "";
                string sExt = "";
                SplitPath(sPathO, ref sDrive, ref sDir, ref sTitle, ref sExt);
                string sSvInfoPath = "";
                sSvInfoPath = $"{sTempDir}{sTitle}.{MdlRINEXTYPE.RNX_SV_EXTENSION}";

                //'プログレス。
                clsProgressInterface.CheckCancel();
                clsProgressInterface.Prompt = $"{sPathO}{DEFINE.vbCrLf}のｲﾝﾎﾟｰﾄ中･･･";


                //'2017/12/21 NS6000対応。'''''''''''''''''''''''''''''''''''''''''''''''''''''''
                //'RINEXファイルのバージョン。
                float nRinexVersion;
                nRinexVersion = MdlNGSRDRIN.CheckVersionRNX(sPathO);
                //'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                //'2020/11/09 FDC 暦ファイルの流用、混合ファイルの分割実行判定'''''''''''''''''''
                bool bSplit = false;
                //'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

                //'RINEX取得。

                if (nRinexVersion < 2.1)
                {
                    //  Call MsgBox(MSG_IMPORT_RINEXVERSIONFAILED, vbCritical)
                    //  Call Err.Raise(cdlCancel)

                }
                else if (nRinexVersion < 3.0)
                {
                    //'混合ファイルの場合には分割を行う
                    if (sPathP != "")
                    {
                        string sSplitPath;
                        sSplitPath = $"{sTempDir}{IMPORT_SPLITPATH}";
                        string sTmpPath;
                        sTmpPath = $"{sSplitPath}{sTitle}.{mdiVBfunctions.Left(mdiVBfunctions.Right(sPathO, 3), 2)}";

                        if (Directory.Exists(sTmpPath))
                        {
                            EmptyDir(sSplitPath);
                        }
                        else
                        {
                            mdlUtility.CreateDir(sSplitPath);
                        }

                        //'分割実施

                        if (MdlNGSRDRIN.SplitRNX(sPathP, 
                                        $"{sTmpPath}{MdlRINEXTYPE.RNX_NAV_EXTENSION}",
                                        $"{sTmpPath}{MdlRINEXTYPE.RNX_GLO_EXTENSION}",
                                        $"{sTmpPath}{MdlRINEXTYPE.RNX_QZS_EXTENSION}",
                                        $"{sTmpPath}{MdlRINEXTYPE.RNX_GAL_EXTENSION}",
                                        $"{sTmpPath}{MdlRINEXTYPE.RNX_BEI_EXTENSION}") != 0)
                        {
                            //Then Call Err.Raise(ERR_RINEX, , MSG_IMPORT_SPLIT)
                            MessageBox.Show(MSG_IMPORT_SPLIT, "エラー発生", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return observationPoint;
                        }
                        sPathP = "";
                        sPathN = $"{sTmpPath}{MdlRINEXTYPE.RNX_NAV_EXTENSION}";
                        if (ReadFile($"{sTmpPath}{MdlRINEXTYPE.RNX_GLO_EXTENSION}"))
                        {
                            sPathG = $"{sTmpPath}{MdlRINEXTYPE.RNX_GLO_EXTENSION}";
                        }
                        bSplit = true;



                    }

                    if (MdlNGSRDRIN.MakeSVInfo2RNX(sPathO, sPathN, sPathG, sSvInfoPath, sAntPath, sRecPath, 0, "") != 0)
                    {
                        //Then Call Err.Raise(ERR_RINEX, , MSG_IMPORT_NGSRDRIN)
                        MessageBox.Show(MSG_IMPORT_NGSRDRIN, "エラー発生", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return observationPoint;
                    }
                }
                else
                {
                    if (MdlNGSRDRIN.MakeSVInfo302RNX(sPathO, sPathN, sPathG, sPathJ, sPathL, sPathC, sPathP, sSvInfoPath, sAntPath, sRecPath, 0, "", sTempDir)  != 0)
                    {

                        //Then Call Err.Raise(ERR_RINEX, , MSG_IMPORT_NGSRDRIN)
                        MessageBox.Show(MSG_IMPORT_NGSRDRIN, "エラー発生", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return observationPoint;
                    }
                }
                //'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''


                //'プログレス。
                clsProgressInterface.CheckCancel();


                //'衛星情報ファイルの読み込み。
                string sRinexExt;
                sRinexExt = mdiVBfunctions.Left(mdiVBfunctions.Right(sPathO, 3), 2);
                ObservationPoint clsObservationPoint;
                clsObservationPoint = ReadSvInfo(sPathO, sTempDir, sTitle, sRinexExt, clsProgressInterface);


                clsObservationPoint.ImportType(IMPORT_TYPE.IMPORT_TYPE_RINEX);

                //'2020/10/23 FDC 暦ファイルの流用'''''''''''''''''''''''''''''''''''''''''''''''
                if (!bNavFlg)
                {
                    //'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                    //  '2017/06/06 NS6000対応。'''''''''''''''''''''''''''''''''''''''''''''''''''''''
                    //  'clsObservationPoint.GlonassFlag = (sPathG <> "")
                    //'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

                    if (!clsObservationPoint.GlonassFlag())
                    {
                        clsObservationPoint.GlonassFlag(sPathG != "");
                    }


                }



                //                aaaaaaaa;

#if false
    
                '衛星情報ファイルの読み込み。
                Dim sRinexExt As String
                sRinexExt = Left$(Right$(sPathO, 3), 2)
                Dim clsObservationPoint As ObservationPoint
                Set clsObservationPoint = ReadSvInfo(sPathO, sTempDir, sTitle, sRinexExt, clsProgressInterface)


                clsObservationPoint.ImportType = IMPORT_TYPE_RINEX
                '2020/10/23 FDC 暦ファイルの流用'''''''''''''''''''''''''''''''''''''''''''''''
                If Not bNavFlg Then
                '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                '2017/06/06 NS6000対応。'''''''''''''''''''''''''''''''''''''''''''''''''''''''
                'clsObservationPoint.GlonassFlag = (sPathG <> "")
                '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                    If Not clsObservationPoint.GlonassFlag Then
                        clsObservationPoint.GlonassFlag = (sPathG<> "")
                    End If
                    If sPathP = "" Then
                        If clsObservationPoint.GlonassFlag And(sPathG = "") Then
                            'Call MsgBox(Left$(sPathO, Len(sPathO) - 1) & RNX_GLO_EXTENSION & vbCrLf & MSG_IMPORT_FILENOTFOUND, vbCritical)
                            'Call Err.Raise(cdlCancel)
                            clsObservationPoint.GlonassFlag = False
                        End If
                        If clsObservationPoint.QZSSFlag And(sPathJ = "") Then
                            'Call MsgBox(Left$(sPathO, Len(sPathO) - 1) & RNX_QZS_EXTENSION & vbCrLf & MSG_IMPORT_FILENOTFOUND, vbCritical)
                            'Call Err.Raise(cdlCancel)
                            clsObservationPoint.QZSSFlag = False
                        End If
                        If clsObservationPoint.GalileoFlag And(sPathL = "") Then
                            'Call MsgBox(Left$(sPathO, Len(sPathO) - 1) & RNX_GAL_EXTENSION & vbCrLf & MSG_IMPORT_FILENOTFOUND, vbCritical)
                            'Call Err.Raise(cdlCancel)
                            clsObservationPoint.GalileoFlag = False
                        End If
                        If clsObservationPoint.BeiDouFlag And(sPathC = "") Then
                            'Call MsgBox(Left$(sPathO, Len(sPathO) - 1) & RNX_BEI_EXTENSION & vbCrLf & MSG_IMPORT_FILENOTFOUND, vbCritical)
                            'Call Err.Raise(cdlCancel)
                            clsObservationPoint.BeiDouFlag = False
                        End If
                        clsObservationPoint.MixedNav = False
                    Else
                        clsObservationPoint.MixedNav = True
                    End If
                '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                '2020/10/23 FDC 暦ファイルの流用'''''''''''''''''''''''''''''''''''''''''''''''
                Else
                    clsObservationPoint.GlonassFlag = bImportCheckG
                    clsObservationPoint.QZSSFlag = bImportCheckJ
                    clsObservationPoint.GalileoFlag = bImportCheckL
                    clsObservationPoint.BeiDouFlag = bImportCheckC
                    If sPathP = "" Then
                        clsObservationPoint.MixedNav = False
                    Else
                        clsObservationPoint.MixedNav = True
                    End If
                End If
                '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    
                'RINEXファイルのコピー。
                Call CopyFile(sPathO, sTempDir & sTitle & "." & sRinexExt & RNX_OBS_EXTENSION, True)
                '2017/07/05 NS6000対応。'''''''''''''''''''''''''''''''''''''''''''''''''''''''
                'Call CopyFile(sPathN, sTempDir & sTitle & "." & sRinexExt & RNX_NAV_EXTENSION, True)
                'If sPathG <> "" Then
                '    Call CopyFile(sPathG, sTempDir & sTitle & "." & sRinexExt & RNX_GLO_EXTENSION, True)
                'End If
                '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                If sPathP<> "" Then
                    Call CopyFile(sPathP, sTempDir & sTitle & "." & sRinexExt & RNX_MIX_EXTENSION, True)
                    '2020/10/23 FDC 暦ファイルの流用'''''''''''''''''''''''''''''''''''''''''''''''
                    sPathP = sTempDir & sTitle & "." & sRinexExt & RNX_MIX_EXTENSION
                    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                Else
                    If sPathN<> "" Then
                        Call CopyFile(sPathN, sTempDir & sTitle & "." & sRinexExt & RNX_NAV_EXTENSION, True)
                        '2020/10/23 FDC 暦ファイルの流用'''''''''''''''''''''''''''''''''''''''''''''''
                        sPathN = sTempDir & sTitle & "." & sRinexExt & RNX_NAV_EXTENSION
                        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                    End If
                    If sPathG<> "" Then
                        Call CopyFile(sPathG, sTempDir & sTitle & "." & sRinexExt & RNX_GLO_EXTENSION, True)
                    End If
                    If sPathJ<> "" Then
                        Call CopyFile(sPathJ, sTempDir & sTitle & "." & sRinexExt & RNX_QZS_EXTENSION, True)
                    End If
                    If sPathL<> "" Then
                        Call CopyFile(sPathL, sTempDir & sTitle & "." & sRinexExt & RNX_GAL_EXTENSION, True)
                    End If
                    If sPathC<> "" Then
                        Call CopyFile(sPathC, sTempDir & sTitle & "." & sRinexExt & RNX_BEI_EXTENSION, True)
                    End If
                End If
                '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                '2020/11/09 FDC 暦ファイルの流用、分割ファイルの削除'''''''''''''''''''''''''''
                If bSplit Then
                    Call DeleteDir(sSplitPath)
                End If
                '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''


                Set ReadRinexFile = clsObservationPoint


#endif



            }
            catch (Exception ex)
            {
                //FileErrorHandler:
                //    If Err.Number = cdlCancel Then Call Err.Raise(Err.Number, Err.Source, Err.Description, Err.HelpFile, Err.HelpContext)
                //    Call MsgBox(Err.Description, vbCritical)
                MessageBox.Show(ex.Message,"エラー発生", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }









            return observationPoint;
        }


        //<<<<<<<<<-----------24/02/21 K.setoguchi@NV
        //***************************************************************************

#if false



'衛星系の有効/無効を取得する。
Private Function GetSatFlag(ByRef nSVHealth() As Long, ByVal nStrNum As Integer, ByVal nEndNum As Integer) As Boolean
    GetSatFlag = False
    Dim i As Integer
    For i = nStrNum To nEndNum
        If UBound(nSVHealth) < i Then
            Exit Function
        End If
        If nSVHealth(i) <> 0 Then
            GetSatFlag = True
            Exit Function
        End If
    Next
End Function

#endif


        //24/02/21 K.setoguchi@NV---------->>>>>>>>>>
        //***************************************************************************

        //==========================================================================================
        /*[VB]
            '衛星情報ファイルを読み込む。
            '
            'sTempDir と sFileTitle で指定される衛星情報ファイルを読み込み、ObservationPoint オブジェクトを生成する。
            '
            '引き数：
            'sSrcPath もともと指定されたインポートファイルのパス。
            'sTempDir 読み込むファイルのフォルダパス。
            'sFileTitle 読み込むファイルのファイルタイトル。
            'sRinexExt 読み込むファイルの拡張子の年部分(先頭２文字)。
            'clsProgressInterface ProgressInterface オブジェクト。
            '
            '戻り値：生成した ObservationPoint オブジェクト(実観測点)を返す。
            Public Function ReadSvInfo(ByVal sSrcPath As String, ByVal sTempDir As String, ByVal sFileTitle As String, ByVal sRinexExt As String, ByVal clsProgressInterface As ProgressInterface) As ObservationPoint

                '衛星情報ファイル。
                Dim clsSatelliteInfoReader As New SatelliteInfoReader
                Call clsSatelliteInfoReader.OpenFile(sTempDir & sFileTitle & "." & RNX_SV_EXTENSION)
                Dim clsSatelliteInfoHeader As SatelliteInfoHeader
                Set clsSatelliteInfoHeader = clsSatelliteInfoReader.ReadHeader(clsProgressInterface)
                Dim clsSatelliteInfoChanges As SatelliteInfoChanges
                Set clsSatelliteInfoChanges = clsSatelliteInfoReader.ReadChanges2(clsSatelliteInfoHeader.TimeOfFirstObs, clsSatelliteInfoHeader.Interval, clsProgressInterface)
    
                'プログレス。
                Call clsProgressInterface.CheckCancel
                ''''''''''''''''''''''''''''''''''''''''''''
                'Debug 20140415
                If (clsSatelliteInfoHeader.ApproxPosX = 0) Then
                    If(clsSatelliteInfoHeader.ApproxPosY = 0) Then
                        If(clsSatelliteInfoHeader.ApproxPosZ = 0) Then
                            frm20140415.PointName = clsSatelliteInfoHeader.MarkerNumber & "(" & clsSatelliteInfoHeader.MarkerName & ")"
                            Call frm20140415.Show(1)
                            Dim nX As Double
                            Dim nY As Double
                            Dim nZ As Double
                            Call WGS84dms_to_WGS84xyz(frm20140415.Lat, frm20140415.Lon, frm20140415.Hei, nX, nY, nZ)
                            clsSatelliteInfoHeader.ApproxPosX = nX
                            clsSatelliteInfoHeader.ApproxPosY = nY
                            clsSatelliteInfoHeader.ApproxPosZ = nZ
                        End If
                    End If
                End If
                ''''''''''''''''''''''''''''''''''''''''''''
    
                '設定。
                Dim clsObservationPoint As New ObservationPoint
                clsObservationPoint.Number = clsSatelliteInfoHeader.MarkerNumber
                clsObservationPoint.Name = clsSatelliteInfoHeader.MarkerName
                clsObservationPoint.CoordinateObservation.X = clsSatelliteInfoHeader.ApproxPosX
                clsObservationPoint.CoordinateObservation.Y = clsSatelliteInfoHeader.ApproxPosY
                clsObservationPoint.CoordinateObservation.Z = clsSatelliteInfoHeader.ApproxPosZ
                clsObservationPoint.Attributes.StrTimeGPS = clsSatelliteInfoHeader.TimeOfFirstObs
                clsObservationPoint.Attributes.EndTimeGPS = clsSatelliteInfoHeader.TimeOfLastObs
                clsObservationPoint.Attributes.LeapSeconds = clsSatelliteInfoHeader.LeapSeconds
                clsObservationPoint.Interval = clsSatelliteInfoHeader.Interval
                clsObservationPoint.RecType = clsSatelliteInfoHeader.RecType
                clsObservationPoint.RecNumber = clsSatelliteInfoHeader.RecNumber
                clsObservationPoint.AntType = clsSatelliteInfoHeader.AntType
                clsObservationPoint.AntNumber = clsSatelliteInfoHeader.AntNumber
                clsObservationPoint.AntMeasurement = clsSatelliteInfoHeader.AntMeasurement
                clsObservationPoint.AntHeight = clsSatelliteInfoHeader.AntHeight
                clsObservationPoint.ElevationMask = clsSatelliteInfoHeader.ElevationMask
                clsObservationPoint.NumberOfMinSV = clsSatelliteInfoHeader.NumberOfMinSV
                clsObservationPoint.FileTitle = StrConv(sFileTitle, vbUpperCase)
                clsObservationPoint.RinexExt = StrConv(sRinexExt, vbUpperCase)
                clsObservationPoint.SrcPath = sSrcPath
                Set clsObservationPoint.SatelliteInfo = clsSatelliteInfoChanges '遷移状態。
    
                '2017/07/05 NS6000対応。'''''''''''''''''''''''''''''''''''''''''''''''''''''''
                '衛星フラグは“# OF OBSERV”で判断する。
                'clsObservationPoint.GlonassFlag = clsSatelliteInfoHeader.NumberObservR > 0
                'clsObservationPoint.QZSSFlag = clsSatelliteInfoHeader.NumberObservJ > 0
                'clsObservationPoint.GalileoFlag = clsSatelliteInfoHeader.NumberObservE > 0
                'clsObservationPoint.BeiDouFlag = clsSatelliteInfoHeader.NumberObservC > 0
                'clsObservationPoint.RinexVersion = clsSatelliteInfoHeader.RinexVersion
                '2018/05/22 衛星フラグは“SV HEALTH”で判断する。
                clsObservationPoint.GlonassFlag = GetSatFlag(clsSatelliteInfoHeader.SVHealth, 38, 63)
                clsObservationPoint.QZSSFlag = GetSatFlag(clsSatelliteInfoHeader.SVHealth, 65, 72)
            '   clsObservationPoint.GalileoFlag = GetSatFlag(clsSatelliteInfoHeader.SVHealth, 73, 102)
            '   clsObservationPoint.BeiDouFlag = GetSatFlag(clsSatelliteInfoHeader.SVHealth, 105, 139)
                clsObservationPoint.GalileoFlag = GetSatFlag(clsSatelliteInfoHeader.SVHealth, 73, 108)
                clsObservationPoint.BeiDouFlag = GetSatFlag(clsSatelliteInfoHeader.SVHealth, 109, 145)
                clsObservationPoint.RinexVersion = clsSatelliteInfoHeader.RinexVersion
                '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    
                'iniファイルに登録されていないものはUnknownにする。
                Dim sPath As String
                sPath = App.Path & "\" & PROFILE_ANT_FILE
                '測位方法が空文字の場合はデフォルトの測位方法として取り扱う。デフォルトの測位方法は0番とする。
                If clsObservationPoint.AntMeasurement = "" Then
                    Dim clsStringTokenizer As New StringTokenizer
                    clsStringTokenizer.Source = GetPrivateProfileString(clsObservationPoint.AntType, PROFILE_ANT_KEY_MEASUREMENT & "0", "", sPath)
                    Call clsStringTokenizer.Begin
                    Dim sToken As String
                    sToken = clsStringTokenizer.NextToken
                    clsObservationPoint.AntMeasurement = clsStringTokenizer.NextToken
                End If
                Dim nMeasurement As Long
                nMeasurement = 0
                Do
                    clsStringTokenizer.Source = GetPrivateProfileString(clsObservationPoint.AntType, PROFILE_ANT_KEY_MEASUREMENT & CStr(nMeasurement), "", sPath)
                    If clsStringTokenizer.Source = "" Then
                        '不明なアンテナは Unknown にする。
                        clsObservationPoint.AntType = GetPrivateProfileString(PROFILE_ANT_SEC_LIST, PROFILE_ANT_KEY_ANT & GetPrivateProfileString(PROFILE_ANT_SEC_LIST, PROFILE_ANT_KEY_UNKNOWN, "0", sPath), "", sPath)
                        clsObservationPoint.AntMeasurement = TRUE_VERTICAL
                        Exit Do
                    End If
                    Call clsStringTokenizer.Begin
                    sToken = clsStringTokenizer.NextToken
                    sToken = clsStringTokenizer.NextToken
                    If sToken = clsObservationPoint.AntMeasurement Then Exit Do
                    nMeasurement = nMeasurement + 1
                Loop
                Dim nHeight As Double
                If Not GetDocument().NetworkModel.ObservationShared.GetTrueVertical(clsObservationPoint.AntType, clsObservationPoint.AntMeasurement, clsObservationPoint.AntHeight, nHeight) Then
                    If Not GetDocument().NetworkModel.ObservationShared.GetMountVertical(clsObservationPoint.AntType, clsObservationPoint.AntMeasurement, clsObservationPoint.AntHeight, nHeight) Then
                        'アンテナ高が不正な場合は位相中心高0にする。
                        clsObservationPoint.AntMeasurement = TRUE_VERTICAL
                        clsObservationPoint.AntHeight = 0
                    End If
                End If
    
                'セッション名。
                If Len(clsSatelliteInfoHeader.SessionID) = 4 Then
                    'セッション名、大文字。
                    Dim sSession As String
                    sSession = StrConv(clsSatelliteInfoHeader.SessionID, vbUpperCase)
                    'ASCIIコード。
                    Dim nCode() As Byte
                    nCode = StrConv(sSession, vbFromUnicode)
                    Dim i As Long
                    For i = 0 To 2
                        '0～9。
                        If nCode(i) < &H30& Or &H39& < nCode(i) Then Exit For
                    Next
                    If i > 2 Then
                        'A～Z
                        If &H41& <= nCode(i) And nCode(i) <= &H5A& Then
                            'セッション名の書式は正しい。
                            clsObservationPoint.Session = sSession
                        End If
                    End If
                End If
    
                '2022/02/07 SattSignal の追加。''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                clsObservationPoint.SattSignalGPS = 0
                clsObservationPoint.SattSignalGLONASS = 0
                clsObservationPoint.SattSignalQZSS = 0
                clsObservationPoint.SattSignalGalileo = 0
                clsObservationPoint.SattSignalBeiDou = 0


                Dim sTypeOfObserv() As String
                Dim sObs As String
                sTypeOfObserv = clsSatelliteInfoHeader.TypeOfObserv
                For i = 0 To clsSatelliteInfoHeader.NumberObserv - 1
                    sObs = Left(sTypeOfObserv(i), 2)
                    If StrComp(sObs, "L1") = 0 Then
                        clsObservationPoint.SattSignalGPS = clsObservationPoint.SattSignalGPS Or &H1
                    ElseIf StrComp(sObs, "L2") = 0 Then
                        clsObservationPoint.SattSignalGPS = clsObservationPoint.SattSignalGPS Or &H2
                    ElseIf StrComp(sObs, "L5") = 0 Then
                        clsObservationPoint.SattSignalGPS = clsObservationPoint.SattSignalGPS Or &H4
                    End If
                Next
                sTypeOfObserv = clsSatelliteInfoHeader.TypeOfObservR
                For i = 0 To clsSatelliteInfoHeader.NumberObservR - 1
                    sObs = Left(sTypeOfObserv(i), 2)
                    If StrComp(sObs, "L1") = 0 Then
                        clsObservationPoint.SattSignalGLONASS = clsObservationPoint.SattSignalGLONASS Or &H1
                    ElseIf StrComp(sObs, "L2") = 0 Then
                        clsObservationPoint.SattSignalGLONASS = clsObservationPoint.SattSignalGLONASS Or &H2
                    ElseIf StrComp(sObs, "L3") = 0 Then
                        clsObservationPoint.SattSignalGLONASS = clsObservationPoint.SattSignalGLONASS Or &H4
                    End If
                Next
                sTypeOfObserv = clsSatelliteInfoHeader.TypeOfObservJ
                For i = 0 To clsSatelliteInfoHeader.NumberObservJ - 1
                    sObs = Left(sTypeOfObserv(i), 2)
                    If StrComp(sObs, "L1") = 0 Then
                        clsObservationPoint.SattSignalQZSS = clsObservationPoint.SattSignalQZSS Or &H1
                    ElseIf StrComp(sObs, "L2") = 0 Then
                        clsObservationPoint.SattSignalQZSS = clsObservationPoint.SattSignalQZSS Or &H2
                    ElseIf StrComp(sObs, "L5") = 0 Then
                        clsObservationPoint.SattSignalQZSS = clsObservationPoint.SattSignalQZSS Or &H4
                    End If
                Next
                sTypeOfObserv = clsSatelliteInfoHeader.TypeOfObservE
                For i = 0 To clsSatelliteInfoHeader.NumberObservE - 1
                    sObs = Left(sTypeOfObserv(i), 2)
                    If StrComp(sObs, "L1") = 0 Then
                        clsObservationPoint.SattSignalGalileo = clsObservationPoint.SattSignalGalileo Or &H1
                    ElseIf StrComp(sObs, "L5") = 0 Then
                        clsObservationPoint.SattSignalGalileo = clsObservationPoint.SattSignalGalileo Or &H2
                    ElseIf StrComp(sObs, "L7") = 0 Then
                        clsObservationPoint.SattSignalGalileo = clsObservationPoint.SattSignalGalileo Or &H2
                    ElseIf StrComp(sObs, "L8") = 0 Then
                        clsObservationPoint.SattSignalGalileo = clsObservationPoint.SattSignalGalileo Or &H2
                    End If
                Next
                sTypeOfObserv = clsSatelliteInfoHeader.TypeOfObservC
                For i = 0 To clsSatelliteInfoHeader.NumberObservC - 1
                    sObs = Left(sTypeOfObserv(i), 2)
                    If StrComp(sObs, "L1") = 0 Then
                        clsObservationPoint.SattSignalBeiDou = clsObservationPoint.SattSignalBeiDou Or &H1
                    ElseIf StrComp(sObs, "L7") = 0 Then
                        clsObservationPoint.SattSignalBeiDou = clsObservationPoint.SattSignalBeiDou Or &H2
                    ElseIf StrComp(sObs, "L6") = 0 Then
                        clsObservationPoint.SattSignalBeiDou = clsObservationPoint.SattSignalBeiDou Or &H4
                    End If
                Next
                '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    
                '2022/03/10 Hitz H.Nakamura '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                'RINEX2.11 の場合はGLONASSとQZSSはGPSの信号をコピーする。
                If clsSatelliteInfoHeader.Version< 100 Then
                    clsObservationPoint.SattSignalGLONASS = clsObservationPoint.SattSignalGPS
                    clsObservationPoint.SattSignalQZSS = clsObservationPoint.SattSignalGPS
                End If
                '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    
                '観測点番号が空の場合、観測点名称を流用する。
                If clsObservationPoint.Number = "" Then clsObservationPoint.Number = clsObservationPoint.Name
    
                '評価。
                If clsObservationPoint.Number = "" Then Call Err.Raise(ERR_FILE, , "観測点番号が読み込めませんでした。")
                If Not CheckCoordXYZ(clsObservationPoint.CoordinateObservation.RoundX, clsObservationPoint.CoordinateObservation.RoundY, clsObservationPoint.CoordinateObservation.RoundZ) Then Call Err.Raise(ERR_FILE, , "観測座標が読み込めませんでした。")
                If DateDiff("s", clsObservationPoint.Attributes.StrTimeGPS, MIN_TIME) > 0 Then Call Err.Raise(ERR_FILE, , "観測時間が読み込めませんでした。")
                If DateDiff("s", clsObservationPoint.Attributes.EndTimeGPS, MIN_TIME) > 0 Then Call Err.Raise(ERR_FILE, , "観測時間が読み込めませんでした。")
                If clsObservationPoint.Interval< 1 Then Call Err.Raise(ERR_FILE, , "データ取得間隔が読み込めませんでした。")
    
                '2017/07/06 NS6000対応。'''''''''''''''''''''''''''''''''''''''''''''''''''''''
                If clsObservationPoint.RinexVersion > 3020 Then Call Err.Raise(ERR_FILE, , "対応していないRINEXファイルのバージョンです。")
                '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    
                Set ReadSvInfo = clsObservationPoint


            End Function
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        public ObservationPoint ReadSvInfo(string sSrcPath, string sTempDir, string sFileTitle, string sRinexExt, ProgressInterface clsProgressInterface)
        {
            ObservationPoint ReadSvInfo;

            //'衛星情報ファイル。
            SatelliteInfoReader clsSatelliteInfoReader = new SatelliteInfoReader();
            clsSatelliteInfoReader.OpenFile($"{sTempDir}{sFileTitle}.{MdlRINEXTYPE.RNX_SV_EXTENSION}");

            SatelliteInfoHeader clsSatelliteInfoHeader = new SatelliteInfoHeader();
//          clsSatelliteInfoHeader = clsSatelliteInfoReader.ReadHeader(clsProgressInterface);


            SatelliteInfoChanges clsSatelliteInfoChanges;
//            clsSatelliteInfoChanges = clsSatelliteInfoReader.ReadChanges2(clsSatelliteInfoHeader.TimeOfFirstObs, (long)clsSatelliteInfoHeader.Interval, clsProgressInterface);


            //'プログレス。
            clsProgressInterface.CheckCancel();

            //''''''''''''''''''''''''''''''''''''''''''''

#if false   //'Debug 20140415
            if (clsSatelliteInfoHeader.ApproxPosX == 0)
            {
                if (clsSatelliteInfoHeader.ApproxPosY == 0)
                {
                    if (clsSatelliteInfoHeader.ApproxPosZ = 0) 
                    {

                        frm20140415.PointName = clsSatelliteInfoHeader.MarkerNumber & "(" & clsSatelliteInfoHeader.MarkerName & ")"
                        Call frm20140415.Show(1)
                        Dim nX As Double
                        Dim nY As Double
                        Dim nZ As Double
                        Call WGS84dms_to_WGS84xyz(frm20140415.Lat, frm20140415.Lon, frm20140415.Hei, nX, nY, nZ)
                        clsSatelliteInfoHeader.ApproxPosX = nX
                        clsSatelliteInfoHeader.ApproxPosY = nY
                        clsSatelliteInfoHeader.ApproxPosZ = nZ

                    }   //if (clsSatelliteInfoHeader.ApproxPosZ = 0)
                }   //if (clsSatelliteInfoHeader.ApproxPosY == 0)
            }   //if (clsSatelliteInfoHeader.ApproxPosX == 0)
#endif

            //'設定。
            ObservationPoint clsObservationPoint = new ObservationPoint();
            clsObservationPoint.Number(clsSatelliteInfoHeader.MarkerNumber);
            clsObservationPoint.Name(clsSatelliteInfoHeader.MarkerName);
            //      clsObservationPoint.CoordinateObservation.X = clsSatelliteInfoHeader.ApproxPosX
            //      clsObservationPoint.CoordinateObservation.Y = clsSatelliteInfoHeader.ApproxPosY
            //      clsObservationPoint.CoordinateObservation.Z = clsSatelliteInfoHeader.ApproxPosZ



#if false
            clsObservationPoint.Attributes.StrTimeGPS = clsSatelliteInfoHeader.TimeOfFirstObs;
            clsObservationPoint.Attributes.EndTimeGPS = clsSatelliteInfoHeader.TimeOfLastObs;
            clsObservationPoint.Attributes.LeapSeconds = clsSatelliteInfoHeader.LeapSeconds;

            clsObservationPoint.Interval(clsSatelliteInfoHeader.Interval);
            clsObservationPoint.RecType(clsSatelliteInfoHeader.RecType);
            clsObservationPoint.RecNumber(clsSatelliteInfoHeader.RecNumber);
            clsObservationPoint.AntType(clsSatelliteInfoHeader.AntType);
            clsObservationPoint.AntNumber(clsSatelliteInfoHeader.AntNumber);
            clsObservationPoint.AntMeasurement(clsSatelliteInfoHeader.AntMeasurement);
            clsObservationPoint.AntHeight(clsSatelliteInfoHeader.AntHeight);
            clsObservationPoint.ElevationMask = clsSatelliteInfoHeader.ElevationMask;
            clsObservationPoint.NumberOfMinSV = clsSatelliteInfoHeader.NumberOfMinSV;
            clsObservationPoint.FileTitle = StrConv(sFileTitle, vbUpperCase);
            clsObservationPoint.RinexExt = StrConv(sRinexExt, vbUpperCase);
            clsObservationPoint.SrcPath = sSrcPath
            clsObservationPoint.SatelliteInfo = clsSatelliteInfoChanges;    //'遷移状態。


    
                '2017/07/05 NS6000対応。'''''''''''''''''''''''''''''''''''''''''''''''''''''''
                '衛星フラグは“# OF OBSERV”で判断する。
                'clsObservationPoint.GlonassFlag = clsSatelliteInfoHeader.NumberObservR > 0
                'clsObservationPoint.QZSSFlag = clsSatelliteInfoHeader.NumberObservJ > 0
                'clsObservationPoint.GalileoFlag = clsSatelliteInfoHeader.NumberObservE > 0
                'clsObservationPoint.BeiDouFlag = clsSatelliteInfoHeader.NumberObservC > 0
                'clsObservationPoint.RinexVersion = clsSatelliteInfoHeader.RinexVersion
                '2018/05/22 衛星フラグは“SV HEALTH”で判断する。
                clsObservationPoint.GlonassFlag = GetSatFlag(clsSatelliteInfoHeader.SVHealth, 38, 63)
                clsObservationPoint.QZSSFlag = GetSatFlag(clsSatelliteInfoHeader.SVHealth, 65, 72)
            '   clsObservationPoint.GalileoFlag = GetSatFlag(clsSatelliteInfoHeader.SVHealth, 73, 102)
            '   clsObservationPoint.BeiDouFlag = GetSatFlag(clsSatelliteInfoHeader.SVHealth, 105, 139)
                clsObservationPoint.GalileoFlag = GetSatFlag(clsSatelliteInfoHeader.SVHealth, 73, 108)
                clsObservationPoint.BeiDouFlag = GetSatFlag(clsSatelliteInfoHeader.SVHealth, 109, 145)
                clsObservationPoint.RinexVersion = clsSatelliteInfoHeader.RinexVersion
                '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    
                'iniファイルに登録されていないものはUnknownにする。
                Dim sPath As String
                sPath = App.Path & "\" & PROFILE_ANT_FILE

#endif





            ReadSvInfo = clsObservationPoint;
            return clsObservationPoint;

        }

        //<<<<<<<<<-----------24/02/21 K.setoguchi@NV
        //***************************************************************************


#if false



'2020/10/23 FDC 暦ファイルの流用'''''''''''''''''''''''''''''''''''''''''''''''
'RINEXファイルを流用する。
'
'引き数：
'sPathO インポートするOファイルのパスが設定される。
'sPathN インポートするNファイルのパスが設定される。
'sPathG インポートするGファイルのパスが設定される。
'sPathJ インポートするJファイルのパスが設定される。
'sPathL インポートするLファイルのパスが設定される。
'sPathC インポートするCファイルのパスが設定される。
'sPathP インポートするPファイルのパスが設定される。
'clsProgressInterface ProgressInterface オブジェクト。
'sImportPathN インポート中のNファイル郡
'sImportPathP インポート中のPファイル郡
'bImportCheckG 流用画面でのGlonass衛星に対するチェック状態
'bImportCheckJ 流用画面でのQZSS衛星に対するチェック状態
'bImportCheckL 流用画面でのGalileo衛星に対するチェック状態
'bImportCheckC 流用画面でのBeiDou衛星に対するチェック状態
'bRinexFlg RINEXファイルの取り込みフラグ
'sMessage 流用画面のメッセージ
'bAlert 流用画面遷移時の警告メッセージ表示有無
Public Sub DivertRinexPath(ByRef sPathO As String, ByRef sPathN As String, ByRef sPathG As String, ByRef sPathJ As String, ByRef sPathL As String, ByRef sPathC As String, ByRef sPathP As String, ByVal clsProgressInterface As ProgressInterface, ByRef sImportPathN() As String, ByRef sImportPathP() As String, ByRef bImportCheckG As Boolean, ByRef bImportCheckJ As Boolean, ByRef bImportCheckL As Boolean, ByRef bImportCheckC As Boolean, ByVal bRinexFlg As Boolean, ByVal sMessage As String, ByVal bAlert As Boolean)

    sPathN = vbNullString
    sPathG = vbNullString
    sPathJ = vbNullString
    sPathL = vbNullString
    sPathC = vbNullString
    sPathP = vbNullString


    Dim objTitles As New Collection

    Dim sDrive As String
    Dim sDir As String
    Dim sTitle As String
    Dim sExt As String
    Call SplitPath(sPathO, sDrive, sDir, sTitle, sExt)


    Dim sKey As String
    Dim objObsFiles As New Collection
    sKey = StrConv(sTitle, vbUpperCase)
    Call objObsFiles.Add(sPathO, sKey)
    
    'NAVファイルのユーザー指定。
    Set frmNavFile.ObsFiles = objObsFiles
    Call frmNavFile.SetImportingNavPath(sImportPathN, sImportPathP)

    frmNavFile.TypeEnableFlg = bRinexFlg
    frmNavFile.m_bGloChecked = bImportCheckG
    frmNavFile.m_bQzsChecked = bImportCheckJ
    frmNavFile.m_bGalChecked = bImportCheckL
    frmNavFile.m_bBeiChecked = bImportCheckC
    frmNavFile.m_sDivertMessage = sMessage
    
    '警告メッセージ
    If bAlert Then
        Call MsgBox(MSG_IMPORT_APPLYFAILED, vbCritical)
    End If

    Call frmNavFile.Show(1)
    If frmNavFile.Result<> vbOK Then
        'テンポラリファイルのクリア。
        Call EmptyDir(App.Path & TEMPORARY_PATH & IMPORT_TEMPPATH, True)
        Call Err.Raise(cdlCancel)
    End If
    
    'ユーザー指定NAVファイルの設定。
    Dim vNavPath As Variant
    If LookupCollectionVariant(frmNavFile.NavFiles, vNavPath, sPathO) Then
        'NとOが揃った。
        If StrConv(Right$(vNavPath, 1), vbUpperCase) = "N" Then
            sPathN = vNavPath
        Else
            'N以外ならPとみなす。
            sPathP = vNavPath
        End If

        bImportCheckG = frmNavFile.GloChecked.Item(sPathO)
        bImportCheckJ = frmNavFile.QzsChecked.Item(sPathO)
        bImportCheckL = frmNavFile.GalChecked.Item(sPathO)
        bImportCheckC = frmNavFile.BeiChecked.Item(sPathO)
    End If
    
    'プログレス。
    Call clsProgressInterface.CheckCancel
    
    'GJQLCファイルの検索。
    If sPathN <> "" Then
        '小文字を保持したパス。
        sTitle = Left$(sPathN, Len(sPathN) - 1)
        '検索するファイル。
        If sPathP = "" Then
            sKey = StrConv(sTitle, vbUpperCase)
            'G検索。
            Dim clsFind As FileFind
            Set clsFind = New FileFind
            If clsFind.FindFile(sKey & RNX_GLO_EXTENSION) And bImportCheckG Then
                'Gファイルが見つかった。
                sPathG = sTitle & RNX_GLO_EXTENSION '小文字を保持する。
            Else
                sPathG = ""
            End If
            'J検索。
            Set clsFind = New FileFind
            If clsFind.FindFile(sKey & RNX_QZS_EXTENSION) And bImportCheckJ Then
                'Qファイルが見つかった。
                sPathJ = sTitle & RNX_QZS_EXTENSION '小文字を保持する。
            ElseIf clsFind.FindFile(sKey & "j") And bImportCheckJ Then
                'Jファイルが見つかった。
                sPathJ = sTitle & "j" '小文字を保持する。
            Else
                sPathJ = ""
            End If
            'L検索。
            Set clsFind = New FileFind
            If clsFind.FindFile(sKey & RNX_GAL_EXTENSION) And bImportCheckL Then
                'Lファイルが見つかった。
                sPathL = sTitle & RNX_GAL_EXTENSION '小文字を保持する。
            Else
                sPathL = ""
            End If
            'C検索。
            Set clsFind = New FileFind
            If clsFind.FindFile(sKey & RNX_BEI_EXTENSION) And bImportCheckC Then
                'Cファイルが見つかった。
                sPathC = sTitle & RNX_BEI_EXTENSION '小文字を保持する。
            ElseIf clsFind.FindFile(sKey & "f") And bImportCheckC Then
                'Fファイルが見つかった。
                sPathC = sTitle & "f" '小文字を保持する。
            Else
                sPathC = ""
            End If
        End If
    End If
    
    'プログレス。
    Call clsProgressInterface.CheckCancel


End Sub
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

'2020/10/23 FDC 暦ファイルの流用'''''''''''''''''''''''''''''''''''''''''''''''
'Datファイルインポート時の内容チェック
'
'引き数：
'sPath 対象ファイルパス
'sPathN インポートするNファイル
'sPathP インポートするPファイル
'clsObservationPoint ObservationPoint オブジェクト。
Public Function CheckImportDatFile(ByRef sPath As String, ByRef sPathN As String, ByRef sPathP As String, ByVal clsObservationPoint As ObservationPoint) As String
    CheckImportDatFile = ""
    
    'テンポラリフォルダ。
    Dim sTempDir As String
    sTempDir = App.Path & TEMPORARY_PATH & IMPORT_TEMPPATH
    
    'ファイル名
    Dim sDrive As String
    Dim sDir As String
    Dim sTitle As String
    Dim sExt As String
    Call SplitPath(sPath, sDrive, sDir, sTitle, sExt)


    Dim sFile As String
    Dim sFileN As String
    'データファイル
    sFile = Dir(sTempDir & sTitle & ".*" & RNX_OBS_EXTENSION)
    sPath = sTempDir & sFile

    If clsObservationPoint.MixedNav Then
        '混合ファイル
        sFile = Dir(sTempDir & sTitle & ".*" & RNX_MIX_EXTENSION)
        If sFile = "" Then
            CheckImportDatFile = sTitle & ".*" & RNX_MIX_EXTENSION & vbCrLf & MSG_IMPORT_FILENOTFOUND
            Exit Function
        End If
        If Not ReadMixedFile(sTempDir & sFile, clsObservationPoint) Then
            CheckImportDatFile = MSG_IMPORT_DATAPARTFAILED
            Exit Function
        End If
        sPathP = sTempDir & sFile
    Else
        'GPS
        sFile = Dir(sTempDir & sTitle & ".*" & RNX_NAV_EXTENSION)
        If sFile = "" Then
            CheckImportDatFile = sTitle & ".*" & RNX_NAV_EXTENSION & vbCrLf & MSG_IMPORT_FILENOTFOUND
            Exit Function
        End If
        If Not ReadFile(sTempDir & sFile) Then
            CheckImportDatFile = MSG_IMPORT_DATAPARTFAILED
            Exit Function
        End If
        sFileN = sFile
        'GLONASS
        If clsObservationPoint.GlonassFlag Then
            sFile = Dir(sTempDir & sTitle & ".*" & RNX_GLO_EXTENSION)
            If sFile = "" Then
                CheckImportDatFile = sTitle & ".*" & RNX_GLO_EXTENSION & vbCrLf & MSG_IMPORT_FILENOTFOUND
                Exit Function
            End If
            If Not ReadFile(sTempDir & sFile) Then
                CheckImportDatFile = MSG_IMPORT_DATAPARTFAILED
                Exit Function
            End If
        End If
        'QZSS
        If clsObservationPoint.QZSSFlag Then
            sFile = Dir(sTempDir & sTitle & ".*" & RNX_QZS_EXTENSION)
            If sFile = "" Then
                sFile = Dir(sTempDir & sTitle & ".*j")
                If sFile = "" Then
                    CheckImportDatFile = sTitle & ".*" & RNX_QZS_EXTENSION & vbCrLf & MSG_IMPORT_FILENOTFOUND
                    Exit Function
                End If
            End If
            If Not ReadFile(sTempDir & sFile) Then
                CheckImportDatFile = MSG_IMPORT_DATAPARTFAILED
                Exit Function
            End If
        End If
        'Galileo
        If clsObservationPoint.GalileoFlag Then
            sFile = Dir(sTempDir & sTitle & ".*" & RNX_GAL_EXTENSION)
            If sFile = "" Then
                CheckImportDatFile = sTitle & ".*" & RNX_GAL_EXTENSION & vbCrLf & MSG_IMPORT_FILENOTFOUND
                Exit Function
            End If
            If Not ReadFile(sTempDir & sFile) Then
                CheckImportDatFile = MSG_IMPORT_DATAPARTFAILED
                Exit Function
            End If
        End If
        'Beidou
        If clsObservationPoint.BeiDouFlag Then
            sFile = Dir(sTempDir & sTitle & ".*" & RNX_BEI_EXTENSION)
            If sFile = "" Then
                sFile = Dir(sTempDir & sTitle & ".*f")
                If sFile = "" Then
                    CheckImportDatFile = sTitle & ".*" & RNX_BEI_EXTENSION & vbCrLf & MSG_IMPORT_FILENOTFOUND
                    Exit Function
                End If
            End If
            If Not ReadFile(sTempDir & sFile) Then
                CheckImportDatFile = MSG_IMPORT_DATAPARTFAILED
                Exit Function
            End If
        End If
        sPathN = sTempDir & sFileN
    End If

End Function
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
#endif

        //==========================================================================================
        /*[VB]
            '2020/10/23 FDC 暦ファイルの流用'''''''''''''''''''''''''''''''''''''''''''''''
            '取り込みファイルの内容チェック
            '
            '引き数：
            'sPath 対象ファイルパス
            'clsObservationPoint ObservationPoint オブジェクト。
            Public Function CheckImportRinexFile(ByRef sPath As String, ByVal clsObservationPoint As ObservationPoint) As String
                CheckImportRinexFile = ""
    
                'テンポラリフォルダ。
                Dim sTempDir As String
                sTempDir = App.Path & TEMPORARY_PATH & IMPORT_TEMPPATH
    
                'ファイル名
                Dim sDrive As String
                Dim sDir As String
                Dim sTitle As String
                Dim sExt As String
                Call SplitPath(sPath, sDrive, sDir, sTitle, sExt)


                Dim sRinexExt As String
                sRinexExt = Left$(Right$(sPath, 3), 2)
    
                Dim sFile As String
                'データファイル
                sFile = Dir(sTempDir & sTitle & "." & sRinexExt & RNX_OBS_EXTENSION)
                sPath = sTempDir & sFile

                If clsObservationPoint.MixedNav Then
                    '混合ファイル
                    sFile = Dir(sTempDir & sTitle & "." & sRinexExt & RNX_MIX_EXTENSION)
                    If sFile = "" Then
                        CheckImportRinexFile = sTitle & "." & sRinexExt & RNX_MIX_EXTENSION & vbCrLf & MSG_IMPORT_FILENOTFOUND
                        Exit Function
                    End If
                    If Not ReadMixedFile(sTempDir & sFile, clsObservationPoint) Then
                        CheckImportRinexFile = MSG_IMPORT_DATAPARTFAILED
                        Exit Function
                    End If
                Else
                    'GPS
                    sFile = Dir(sTempDir & sTitle & "." & sRinexExt & RNX_NAV_EXTENSION)
                    If sFile = "" Then
                        CheckImportRinexFile = sTitle & "." & sRinexExt & RNX_NAV_EXTENSION & vbCrLf & MSG_IMPORT_FILENOTFOUND
                        Exit Function
                    End If
                    If Not ReadFile(sTempDir & sFile) Then
                        CheckImportRinexFile = MSG_IMPORT_DATAPARTFAILED
                        Exit Function
                    End If
                    'GLONASS
                    If clsObservationPoint.GlonassFlag Then
                        sFile = Dir(sTempDir & sTitle & "." & sRinexExt & RNX_GLO_EXTENSION)
                        If sFile = "" Then
                            CheckImportRinexFile = sTitle & "." & sRinexExt & RNX_GLO_EXTENSION & vbCrLf & MSG_IMPORT_FILENOTFOUND
                            Exit Function
                        End If
                        If Not ReadFile(sTempDir & sFile) Then
                            CheckImportRinexFile = MSG_IMPORT_DATAPARTFAILED
                            Exit Function
                        End If
                    End If
                    'QZSS
                    If clsObservationPoint.QZSSFlag Then
                        sFile = Dir(sTempDir & sTitle & "." & sRinexExt & RNX_QZS_EXTENSION)
                        If sFile = "" Then
                            sFile = Dir(sTempDir & sTitle & "." & sRinexExt & "j")
                            If sFile = "" Then
                                CheckImportRinexFile = sTitle & "." & sRinexExt & RNX_QZS_EXTENSION & vbCrLf & MSG_IMPORT_FILENOTFOUND
                                Exit Function
                            End If
                        End If
                        If Not ReadFile(sTempDir & sFile) Then
                            CheckImportRinexFile = MSG_IMPORT_DATAPARTFAILED
                            Exit Function
                        End If
                    End If
                    'Galileo
                    If clsObservationPoint.GalileoFlag Then
                        sFile = Dir(sTempDir & sTitle & "." & sRinexExt & RNX_GAL_EXTENSION)
                        If sFile = "" Then
                            CheckImportRinexFile = sTitle & "." & sRinexExt & RNX_GAL_EXTENSION & vbCrLf & MSG_IMPORT_FILENOTFOUND
                            Exit Function
                        End If
                        If Not ReadFile(sTempDir & sFile) Then
                            CheckImportRinexFile = MSG_IMPORT_DATAPARTFAILED
                            Exit Function
                        End If
                    End If
                    'Beidou
                    If clsObservationPoint.BeiDouFlag Then
                        sFile = Dir(sTempDir & sTitle & "." & sRinexExt & RNX_BEI_EXTENSION)
                        If sFile = "" Then
                            sFile = Dir(sTempDir & sTitle & "." & sRinexExt & "f")
                            If sFile = "" Then
                                CheckImportRinexFile = sTitle & "." & sRinexExt & RNX_BEI_EXTENSION & vbCrLf & MSG_IMPORT_FILENOTFOUND
                                Exit Function
                            End If
                        End If
                        If Not ReadFile(sTempDir & sFile) Then
                            CheckImportRinexFile = MSG_IMPORT_DATAPARTFAILED
                            Exit Function
                        End If
                    End If
                End If


            End Function
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        public string CheckImportRinexFile(string sPath, ObservationPoint clsObservationPoint)
        {
            string CheckImportRinexFile = "";



            //'テンポラリフォルダ。
            string sTempDir;
            string App_Path = @"C:\Develop\NetSurv\Src\NS-App\NS-Survey";
            sTempDir = $"{App_Path}{MdlNSDefine.TEMPORARY_PATH}{IMPORT_TEMPPATH}";


            //'ファイル名
            string sDrive = "";
            string sDir = "";
            string sTitle = "";
            string sExt = "";
            SplitPath(sPath, ref sDrive, ref sDir, ref sTitle, ref sExt);

            string sRinexExt;
            sRinexExt = mdiVBfunctions.Left(mdiVBfunctions.Right(sPath, 3), 2);


            string sFile = "";
            //'データファイル
            sFile = $"{sTempDir}{sTitle}.{sRinexExt}{MdlRINEXTYPE.RNX_OBS_EXTENSION}";
            sFile = Path.GetFileName(sFile);
            sPath = sTempDir + sFile;

            if (clsObservationPoint.MixedNav())
            {
                //'混合ファイル
                sFile = $"{sTempDir}{sTitle}.{sRinexExt}{MdlRINEXTYPE.RNX_MIX_EXTENSION}";
                sFile = Path.GetFileName(sFile);

                if (sFile == "")
                {
                    CheckImportRinexFile = $"{sTitle}.{sRinexExt}{MdlRINEXTYPE.RNX_MIX_EXTENSION}{DEFINE.vbCrLf}{MSG_IMPORT_FILENOTFOUND}";
                    return CheckImportRinexFile;
                }
                if (!ReadMixedFile(sTempDir + sFile, clsObservationPoint))
                {
                    CheckImportRinexFile = MSG_IMPORT_DATAPARTFAILED;
                    return CheckImportRinexFile;
                }
            }
            else
            {
                //'GPS

                //  sFile = Dir(sTempDir & sTitle & "." & sRinexExt & RNX_NAV_EXTENSION)
                sFile = $"{sTempDir}{sTitle}.{sRinexExt}{MdlRINEXTYPE.RNX_NAV_EXTENSION}";
                sFile = Path.GetFileName(sFile);
                sPath = sTempDir + sFile;

                if (sFile == "")
                {
                    CheckImportRinexFile = sTitle + "." + sRinexExt + MdlRINEXTYPE.RNX_NAV_EXTENSION + DEFINE.vbCrLf + MSG_IMPORT_FILENOTFOUND;
                    return CheckImportRinexFile;
                }
                if (!ReadFile(sTempDir + sFile))
                {
                    CheckImportRinexFile = MSG_IMPORT_DATAPARTFAILED;
                    return CheckImportRinexFile;
                }

                //'GLONASS
                if (clsObservationPoint.GlonassFlag())
                {
                    //  sFile = Dir(sTempDir & sTitle & "." & sRinexExt & RNX_GLO_EXTENSION)
                    sFile = $"{sTempDir}{sTitle}.{sRinexExt}{MdlRINEXTYPE.RNX_GLO_EXTENSION}";
                    sFile = Path.GetFileName(sFile);
                    sPath = sTempDir + sFile;

                    if (sFile == "")
                    {
                        CheckImportRinexFile = sTitle + "." + sRinexExt + MdlRINEXTYPE.RNX_GLO_EXTENSION + DEFINE.vbCrLf + MSG_IMPORT_FILENOTFOUND;
                        return CheckImportRinexFile;
                    }
                    if (!ReadFile(sTempDir + sFile))
                    {
                        CheckImportRinexFile = MSG_IMPORT_DATAPARTFAILED;
                        return CheckImportRinexFile;
                    }


                }
                //'QZSS
                if (clsObservationPoint.QZSSFlag())
                {
                    //  sFile = Dir(sTempDir & sTitle & "." & sRinexExt & RNX_QZS_EXTENSION)
                    sFile = $"{sTempDir}{sTitle}.{sRinexExt}{MdlRINEXTYPE.RNX_QZS_EXTENSION}";
                    sFile = Path.GetFileName(sFile);
                    sPath = sTempDir + sFile;

                    if (sFile == "")
                    {
                        //  sFile = Dir(sTempDir & sTitle & "." & sRinexExt & "j")
                        sFile = sTempDir + sTitle + "." + sRinexExt + "j";
                        sFile = Path.GetFileName(sFile);

                        if (sFile == "")
                        {
                            CheckImportRinexFile = sTitle + "." + sRinexExt + MdlRINEXTYPE.RNX_QZS_EXTENSION + DEFINE.vbCrLf + MSG_IMPORT_FILENOTFOUND;
                            return CheckImportRinexFile;
                        }
                    }
                    if (!ReadFile(sTempDir + sFile))
                    {
                        CheckImportRinexFile = MSG_IMPORT_DATAPARTFAILED;
                        return CheckImportRinexFile;
                    }
                }
                //'Galileo
                if (clsObservationPoint.GalileoFlag())
                {

                    //  sFile = Dir(sTempDir & sTitle & "." & sRinexExt & RNX_QZS_EXTENSION)
                    sFile = $"{sTempDir}{sTitle}.{sRinexExt}{MdlRINEXTYPE.RNX_QZS_EXTENSION}";
                    sFile = Path.GetFileName(sFile);
                    sPath = sTempDir + sFile;

                    if (sFile == "")
                    {
                        //  sFile = Dir(sTempDir & sTitle & "." & sRinexExt & RNX_GAL_EXTENSION)
                        sFile = sTempDir + sTitle + "." + sRinexExt + MdlRINEXTYPE.RNX_GAL_EXTENSION;
                        sFile = Path.GetFileName(sFile);

                        if (sFile == "")
                        {
                            CheckImportRinexFile = sTitle + "." + sRinexExt + MdlRINEXTYPE.RNX_GAL_EXTENSION + DEFINE.vbCrLf + MSG_IMPORT_FILENOTFOUND;
                            return CheckImportRinexFile;
                        }
                    }
                    if (!ReadFile(sTempDir + sFile))
                    {
                        CheckImportRinexFile = MSG_IMPORT_DATAPARTFAILED;
                        return CheckImportRinexFile;
                    }
                }
                //'Beidou
                if (clsObservationPoint.BeiDouFlag())
                {
                    //  sFile = Dir(sTempDir & sTitle & "." & sRinexExt & RNX_BEI_EXTENSION)
                    sFile = $"{sTempDir}{sTitle}.{sRinexExt}{MdlRINEXTYPE.RNX_BEI_EXTENSION}";
                    sFile = Path.GetFileName(sFile);
                    sPath = sTempDir + sFile;

                    if (sFile == "")
                    {
                        //  sFile = Dir(sTempDir & sTitle & "." & sRinexExt & "f")
                        sFile = sTempDir + sTitle + "." + sRinexExt + "f";
                        sFile = Path.GetFileName(sFile);

                        if (sFile == "")
                        {
                            CheckImportRinexFile = sTitle + "." + sRinexExt + MdlRINEXTYPE.RNX_BEI_EXTENSION + DEFINE.vbCrLf + MSG_IMPORT_FILENOTFOUND;
                            return CheckImportRinexFile;
                        }
                    }
                    if (!ReadFile(sTempDir + sFile))
                    {
                        CheckImportRinexFile = MSG_IMPORT_DATAPARTFAILED;
                        return CheckImportRinexFile;
                    }

                }
            }
            return CheckImportRinexFile;
        }




        //24/02/21 K.setoguchi@NV---------->>>>>>>>>>
        //***************************************************************************

        //==========================================================================================
        /*[VB]
            '2020/10/23 FDC 暦ファイルの流用'''''''''''''''''''''''''''''''''''''''''''''''
            'ファイル内容の読み込み
            '
            '引き数：
            'sFile 読み込み対象ファイル
            Private Function ReadFile(ByVal sFile As String) As Boolean
                Dim fileNo As Long
                Dim buf As String
                Dim buf_() As Byte
                Dim bDataRow As Boolean


                ReadFile = False

                bDataRow = False


                fileNo = FreeFile
                Open sFile For Binary As #fileNo
                ReDim buf_(LOF(fileNo))
                Get #fileNo, , buf_
                Close #fileNo

                buf = StrConv(buf_, vbUnicode)
                If buf<> "" Then
                    buf = Replace(buf, vbCrLf, vbLf)
                    Dim rows() As String
                    rows = Split(buf, vbLf)
                    Dim i As Long
                    For i = 0 To UBound(rows)
                        If Trim(rows(i)) <> "" And rows(i) <> Chr(&H0) Then
                            If bDataRow Then
                                ReadFile = True
                                Exit For
                            End If

                            If InStr(CStr(rows(i)), "END OF HEADER") > 0 Then
                                bDataRow = True
                            End If
                        End If
                    Next
                End If

            End Function
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        private bool ReadFile(string sFile)
        {
            bool ReadFile = false;




            ReadFile = true;
            return ReadFile;

        }



        //==========================================================================================
        /*[VB]
            '2020/10/23 FDC 暦ファイルの流用'''''''''''''''''''''''''''''''''''''''''''''''
            '混合ファイルの内容読み込み
            '
            '引き数：
            'sFile 対象ファイル
            'clsObservationPoint ObservationPoint オブジェクト。
            Private Function ReadMixedFile(ByVal sFile As String, ByVal clsObservationPoint As ObservationPoint) As Boolean
                Dim fileNo As Integer
                Dim buf As String
                Dim buf_() As Byte
                Dim bExsistGPS As Boolean
                Dim bExsistGLONASS As Boolean
                Dim bExsistQZSS As Boolean
                Dim bExsistGalileo As Boolean
                Dim bExsistBeidou As Boolean


                ReadMixedFile = False

                fileNo = FreeFile
                Open sFile For Binary As #fileNo
                ReDim buf_(FileLen(sFile))
                Get #fileNo, , buf_
                Close #fileNo
    
                bExsistGPS = False
                bExsistGLONASS = False
                bExsistQZSS = False
                bExsistGalileo = False
                bExsistBeidou = False

                buf = StrConv(buf_, vbUnicode)
                If buf<> "" Then
                    buf = Replace(buf, vbCrLf, vbLf)
                    Dim rows As Variant
                    rows = Split(buf, vbLf)


                    Dim i As Long
                    For i = 0 To UBound(rows)
                        Select Case Left(CStr(rows(i)), 1)
                        Case "G"
                            bExsistGPS = True
                        Case "R"
                            bExsistGLONASS = True
                        Case "J"
                            bExsistQZSS = True
                        Case "E"
                            bExsistGalileo = True
                        Case "C"
                            bExsistBeidou = True
                        End Select
                    Next
                End If


                If Not bExsistGPS Then
                    Exit Function
                End If
                If clsObservationPoint.GlonassFlag And Not bExsistGLONASS Then
                    Exit Function
                End If
                If clsObservationPoint.QZSSFlag And Not bExsistQZSS Then
                    Exit Function
                End If
                If clsObservationPoint.GalileoFlag And Not bExsistGalileo Then
                    Exit Function
                End If
                If clsObservationPoint.BeiDouFlag And Not bExsistBeidou Then
                    Exit Function
                End If

                ReadMixedFile = True
            End Function
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        private bool ReadMixedFile(string sFile, ObservationPoint clsObservationPoint)
        {
            bool ReadMixedFile = false;

            //瀬戸口後で

            ReadMixedFile = true;
            return ReadMixedFile;

        }






        //<<<<<<<<<-----------24/02/21 K.setoguchi@NV
        //***************************************************************************


#if false

        '2020/10/23 FDC 暦ファイルの流用'''''''''''''''''''''''''''''''''''''''''''''''
        '再流用処理発生時に一時ファイルの削除
        '
        '引き数：
        'sPath 削除対象の一時ファイル
        Public Function DeleteTempFile(ByVal sPath As String) As Boolean
            'ファイル名
            Dim sDrive As String
            Dim sDir As String
            Dim sTitle As String
            Dim sExt As String
            Call SplitPath(sPath, sDrive, sDir, sTitle, sExt)


            Dim sRinexExt As String
            sRinexExt = Left$(Right$(sPath, 3), 2)
    
            'テンポラリフォルダ。
            Dim sTempDir As String
            sTempDir = App.Path & TEMPORARY_PATH & IMPORT_TEMPPATH

            Dim sFile As String
            sFile = Dir(sTempDir & sTitle & "." & sRinexExt & RNX_MIX_EXTENSION)
            If sFile<> "" Then
                Call RemoveFile(sTempDir & sTitle & "." & sRinexExt & RNX_MIX_EXTENSION)
            End If
            sFile = Dir(sTempDir & sTitle & "." & sRinexExt & RNX_NAV_EXTENSION)
            If sFile<> "" Then
                Call RemoveFile(sTempDir & sTitle & "." & sRinexExt & RNX_NAV_EXTENSION)
            End If
            sFile = Dir(sTempDir & sTitle & "." & sRinexExt & RNX_GLO_EXTENSION)
            If sFile<> "" Then
                Call RemoveFile(sTempDir & sTitle & "." & sRinexExt & RNX_GLO_EXTENSION)
            End If
            sFile = Dir(sTempDir & sTitle & "." & sRinexExt & RNX_QZS_EXTENSION)
            If sFile<> "" Then
                Call RemoveFile(sTempDir & sTitle & "." & sRinexExt & RNX_QZS_EXTENSION)
            End If
            sFile = Dir(sTempDir & sTitle & "." & sRinexExt & "j")
            If sFile<> "" Then
                Call RemoveFile(sTempDir & sTitle & "." & sRinexExt & "j")
            End If
            sFile = Dir(sTempDir & sTitle & "." & sRinexExt & RNX_GAL_EXTENSION)
            If sFile<> "" Then
                Call RemoveFile(sTempDir & sTitle & "." & sRinexExt & RNX_GAL_EXTENSION)
            End If
            sFile = Dir(sTempDir & sTitle & "." & sRinexExt & RNX_BEI_EXTENSION)
            If sFile<> "" Then
                Call RemoveFile(sTempDir & sTitle & "." & sRinexExt & RNX_BEI_EXTENSION)
            End If
            sFile = Dir(sTempDir & sTitle & "." & sRinexExt & "f")
            If sFile<> "" Then
                Call RemoveFile(sTempDir & sTitle & "." & sRinexExt & "f")
            End If
            sFile = Dir(sTempDir & sTitle & "." & sRinexExt & RNX_SV_EXTENSION)
            If sFile<> "" Then
                Call RemoveFile(sTempDir & sTitle & "." & sRinexExt & RNX_SV_EXTENSION)
            End If
        End Function
        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        public object VbStrConv { get; private set; }
#endif
    }
}
