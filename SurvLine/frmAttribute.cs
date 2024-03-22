using SurvLine.mdl;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static SurvLine.mdl.DEFINE;
using static SurvLine.mdl.MdlNSDefine;
using static SurvLine.mdl.MdlUtility;
using static SurvLine.mdl.MdlAccountMake;
using static SurvLine.mdl.MdlNSGUI;
using static SurvLine.mdl.MdlGUI;
using static SurvLine.mdl.MdlNSSDefine;
using static SurvLine.mdl.MdiVBfunctions;
using static SurvLine.mdl.MdlMain;
using static SurvLine.mdl.MdlNSUtility;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Microsoft.VisualBasic.Logging;
using System.Reflection;
using static SurvLine.frmAttribute;
using System.Runtime.Remoting;


namespace SurvLine
{
    public partial class frmAttribute : Form
    {

        //'*******************************************************************************
        //'観測データの編集画面
        //
        //Option Explicit
        //'*******************************************************************************


        //1==========================================================================================
        /*[VB]

            'インターフェース
            Implements VariantSortInterface 'バリアント型ソートインターフェース。

            'タブ番号。
            Private Enum TAB_NUMBER
                TAB_OBSERVATION = 0 '観測。
                TAB_RECEIVER '受信機。
                TAB_ANTENNA 'アンテナ。
            End Enum

            '構造体
            Private Type DEFTYPEINFO '定義情報。
                DefType As String '種別。
                Name As String '名称。
                AntMeasurement() As String '測位方法。
                DispMeasurement() As String '測位方法表示文字列。
                ListIndex As Long '測位方法のカレントのリストインデックス。
            End Type

            Private Type DEFTYPEINFO_MANUFACTURER 'メーカー別定義情報。
                Manufacturer As String 'メーカー名。
                DefTypeInfos() As DEFTYPEINFO '定義情報。
                ListIndex As Long '定義情報のカレントのリストインデックス。
                SortIndex As Long '並び順番号。
            End Type

            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]

        //'インターフェース
        //  Implements VariantSortInterface 'バリアント型ソートインターフェース。


        //'タブ番号。
        private enum TAB_NUMBER
        {
            TAB_OBSERVATION = 0,    //'観測。
            TAB_RECEIVER,           //'受信機。
            TAB_ANTENNA,            //'アンテナ。
        }

        // '構造体
        public struct DEFTYPEINFO   //'定義情報
        {
            public string DefType;                 //'種別。
            public string Name;                    //'名称。
            public List<string> AntMeasurement;    //'測位方法。
            public List<string> DispMeasurement;   //'測位方法表示文字列。
            public long ListIndex;                 //'測位方法のカレントのリストインデックス。
        }

        public struct DEFTYPEINFO_MANUFACTURER   //'メーカー別定義情報
        {
            public  string Manufacturer;                //'メーカー名。
            public List<DEFTYPEINFO> DefTypeInfos;     //'定義情報。
            public long ListIndex;                     //'定義情報のカレントのリストインデックス。
            public long SortIndex;                     //'並び順番号。
        }

        //1==========================================================================================
        /*[VB]
            'プロパティ
            Public Result As Long 'ダイアログの結果。
            Public Session As String 'セッション名。
            Public RecType As String '受信機名称。
            Public RecNumber As String '受信機シリアル番号。
            Public AntType As String 'アンテナ種別。
            Public AntNumber As String 'アンテナシリアル番号。
            Public AntMeasurement As String 'アンテナ測位方法。
            Public AntHeight As Double 'アンテナ高(ｍ)。
            Public Analysis As Boolean '解析済フラグ。True=解析済。False=未解析。
            Public Extend As Boolean '拡張フラグが設定される。True=関連するオブジェクトにも設定する。False=関連するオブジェクトまでは設定しない。
            Public ElevationMask As Long '仰角マスク(度)。
            Public ElevationMaskHand As Long '手簿に出力する最低高度角(度)。負の値の場合OFFとする。
            Public NumberOfMinSV As Long '最少衛星数。
            Public NumberOfMinSVHand As Long '手簿に出力する最少衛星数。0の場合はオプション設定から取得。1～12が最少衛星数。

            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]

        //'プロパティ
        public long Result;             //'ダイアログの結果。
        public string Session;          //'セッション名。
        public string RecType;          //'受信機名称。
        public string RecNumber;        //'受信機シリアル番号。
        public string AntType;          //'アンテナ種別。
        public string AntNumber;        //'アンテナシリアル番号。
        public string AntMeasurement;   //'アンテナ測位方法。
        public double AntHeight;        //'アンテナ高(ｍ)。
        public bool Analysis;           //'解析済フラグ。True=解析済。False=未解析。
        public bool Extend;             //'拡張フラグが設定される。True=関連するオブジェクトにも設定する。False=関連するオブジェクトまでは設定しない。
        public long ElevationMask;      //'仰角マスク(度)。
        public long ElevationMaskHand;  //'手簿に出力する最低高度角(度)。負の値の場合OFFとする。
        public long NumberOfMinSV;      //'最少衛星数。
        public long NumberOfMinSVHand;  //'手簿に出力する最少衛星数。0の場合はオプション設定から取得。1～12が最少衛星数。


        //1==========================================================================================
        /*[VB]
            'インプリメンテーション
            Private m_objElements As Collection '編集の対象とする観測点。コレクションであるが実質は一つのオブジェクトしか設定しない。要素は ObservationPoint オブジェクト(代表観測点)。キーは任意。
            Private m_tRecInfos() As DEFTYPEINFO_MANUFACTURER '受信機定義情報。配列の要素は(-1 To ...)、要素 -1 は未使用。
            Private m_tAntInfos() As DEFTYPEINFO_MANUFACTURER 'アンテナ定義情報。配列の要素は(-1 To ...)、要素 -1 は未使用。
            Private m_tInfos() As DEFTYPEINFO_MANUFACTURER '定義情報。配列の要素は(-1 To ...)、要素 -1 は未使用。
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'インプリメンテーション
        //private Collection m_objElements;       //'編集の対象とする観測点。コレクションであるが実質は一つのオブジェクトしか設定しない。要素は ObservationPoint オブジェクト(代表観測点)。キーは任意。
        public Dictionary<string, object> m_objElements;

        private List<DEFTYPEINFO_MANUFACTURER> m_tRecInfos = new List<DEFTYPEINFO_MANUFACTURER>();  //'受信機定義情報。配列の要素は(-1 To ...)、要素 -1 は未使用。
        private List<DEFTYPEINFO_MANUFACTURER> m_tAntInfos = new List<DEFTYPEINFO_MANUFACTURER>();  //'アンテナ定義情報。配列の要素は(-1 To ...)、要素 -1 は未使用。
        public List<DEFTYPEINFO_MANUFACTURER> m_tInfos = new List<DEFTYPEINFO_MANUFACTURER>();     //'定義情報。配列の要素は(-1 To ...)、要素 -1 は未使用。



        //------------------------------------------------------------------------------------------
        //[C#]
        //==========================================================================================
        //[C#]
        private MdlMain m_clsMdlMain;                                   //MdlMainインスタンス

        public frmAttribute(MdlMain clsMdlMain)
        {
            InitializeComponent();
            Load += Form_Load;
        }
        //------------------------------------------------------------------------------------------


        //'*******************************************************************************
        //'プロパティ
        //'*******************************************************************************


        //1==========================================================================================
        /*[VB]
            '観測点。
            Property Set ObservationPoint(ByVal clsObservationPoint As ObservationPoint)
                Set m_objElements = New Collection
                Call m_objElements.Add(clsObservationPoint)
            End Property
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        public static void ObservationPoint(ObservationPoint clsObservationPoint)
        {
            object m_objElements = new Dictionary<string, object>();
            //  m_objElements(clsObservationPoint);
        }



        //'*******************************************************************************
        //'イベント
        //'*******************************************************************************


        //1==========================================================================================
        /*[VB]
            '初期化。
            Private Sub Form_Load()

                On Error GoTo ErrorHandler
    
                '変数初期化。
                Result = vbCancel
                fraCtrls0.BorderStyle = vbBSNone
                fraCtrls1.BorderStyle = vbBSNone
    
                'タブ初期化。
                Dim objTab As Object
                For Each objTab In tsTab.Tabs
                    fraTab(objTab.Index - 1).BorderStyle = vbBSNone
                Next
                Call ChangeTab(tsTab, fraTab)
    
                '最少衛星数。
                Dim i As Long
                For i = 1 To 36
                    Call cmbNumberOfMinSV.AddItem(CStr(i))
                Next
    
                '値の設定。
                txtRecNumber.Text = RecNumber
                txtAntNumber.Text = AntNumber
                txtAntHeight.Text = FormatRound0Trim(AntHeight, GUI_ANTHEIGHT_DECIMAL)
    
                If ElevationMaskHand < 0 Then
            '       txtElevationMask.Text = CStr(ElevationMask) 'optElevationMaskObsData より先に設定。
                    txtElevationMask.Text = "15" 'optElevationMaskObsData より先に設定。
                    optElevationMaskObsData.Value = True
                Else
                    txtElevationMask.Text = CStr(ElevationMaskHand) 'optElevationMaskInput より先に設定。
                    optElevationMaskInput.Value = True
                End If
    
                'セッションリスト。
                Call MakeSessionList(cmbSession)
                cmbSession.Text = Session
    
                If NumberOfMinSVHand > 0 Then
                    cmbNumberOfMinSV.ListIndex = NumberOfMinSVHand - 1
                    optNumberOfMinSVStatic.Value = True
                Else
                    '2023/07/24 Hitz H.Nakamura **************************************************
                    '観測データの衛星数がリストボックスの相手無数を超える場合があった。
                    'そのような場合はリストボックスを未選択状態にしておく。
                    'cmbNumberOfMinSV.ListIndex = NumberOfMinSV - 1 'こっちが先。
                    '*****************************************************************************
                    If cmbNumberOfMinSV.ListCount < NumberOfMinSV Then
                        cmbNumberOfMinSV.ListIndex = -1
                    Else
                        cmbNumberOfMinSV.ListIndex = NumberOfMinSV - 1 'こっちが先。
                    End If
                    '*****************************************************************************
                    optNumberOfMinSVObsData.Value = True 'こっちが後。
                End If
    
                '定義情報の初期化。
                Call InitDefTypeInfo
    
                Exit Sub
    
            ErrorHandler:
                Call mdlMain.ErrorExit
    
            End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        private void fraTab(int Index)
        {
            fraTab1.FlatStyle = FlatStyle.Standard;

        }

        private void Form_Load(object sender, EventArgs e)
        {

            try
            {

                //'変数初期化。
                Result = vbCancel;
                fraCtrls0.FlatStyle = FlatStyle.Standard;
                fraCtrls0.FlatStyle = FlatStyle.Standard;

                //'タブ初期化。
                object objTab;

                fraTab0.FlatStyle = FlatStyle.Standard;
                fraTab1.FlatStyle = FlatStyle.Standard;
                fraTab2.FlatStyle = FlatStyle.Standard;

                ChangeTab(tsTab, fraTab0);
                ChangeTab(tsTab, fraTab1);
                ChangeTab(tsTab, fraTab2);



                //'最少衛星数。
                long i;
                for (i = 0; i < 36; i++)
                {
                    cmbNumberOfMinSV.Items.Add(i);
                }

                //'値の設定。
                txtRecNumber.Text = RecNumber;
                txtAntNumber.Text = AntNumber;
                txtAntHeight.Text = FormatRound0Trim(AntHeight, GUI_ANTHEIGHT_DECIMAL);

                if (ElevationMaskHand < 0)
                {
                    txtElevationMask.Text = "15";       //'optElevationMaskObsData より先に設定。
                    optElevationMaskObsData.Checked = true;
                }
                else
                {
                    txtElevationMask.Text = ElevationMaskHand.ToString();     //'optElevationMaskInput より先に設定。
                    optElevationMaskInput.Checked = true;
                }

                //'セッションリスト。
                //MakeSessionList(cmbSession);
                cmbSession.Items.Add(Session);

                if (NumberOfMinSVHand > 0)
                {
                    cmbNumberOfMinSV.SelectedIndex = (int)NumberOfMinSVHand - 1;
                    optNumberOfMinSVStatic.Checked = true;
                }
                else
                {
                    //'2023/07/24 Hitz H.Nakamura **************************************************
                    //  '観測データの衛星数がリストボックスの相手無数を超える場合があった。
                    //  'そのような場合はリストボックスを未選択状態にしておく。
                    //  'cmbNumberOfMinSV.ListIndex = NumberOfMinSV - 1 'こっちが先。
                    //'*****************************************************************************
                    if (cmbNumberOfMinSV.Items.Count < NumberOfMinSV)
                    {
                        cmbNumberOfMinSV.SelectedIndex = -1;
                    }
                    else
                    {
                        cmbNumberOfMinSV.SelectedIndex = (int)(NumberOfMinSV - 1);     //'こっちが先。
                    }
                    optNumberOfMinSVObsData.Checked = true;     //'こっちが後。

                }

                //'定義情報の初期化。
                InitDefTypeInfo();
    
            }
            catch (Exception ex)
            {
                //ErrorHandler:
                //    Call mdlMain.ErrorExit
                _ = MessageBox.Show(ex.Message + "frmAttribute Form_Load", "エラー発生", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

















        }

        //1==========================================================================================
        /*[VB]
            '終了処理。
            Private Sub Form_Unload(Cancel As Integer)

                On Error GoTo ErrorHandler
    
                Set m_objElements = Nothing
    
                Exit Sub
    
            ErrorHandler:
                Call mdlMain.ErrorExit
    
            End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]


        //1==========================================================================================
        /*[VB]
            'テキストをすべて選択する。
            Private Sub txtRecName_GotFocus()

                On Error GoTo ErrorHandler
    
                Call SelectText(txtRecName)
    
                Exit Sub
    
            ErrorHandler:
                Call mdlMain.ErrorExit
    
            End Sub

            'テキストをすべて選択する。
            Private Sub txtRecNumber_GotFocus()

                On Error GoTo ErrorHandler
    
                Call SelectText(txtRecNumber)
    
                Exit Sub
    
            ErrorHandler:
                Call mdlMain.ErrorExit
    
            End Sub

            'テキストをすべて選択する。
            Private Sub txtAntNumber_GotFocus()

                On Error GoTo ErrorHandler
    
                Call SelectText(txtAntNumber)
    
                Exit Sub
    
            ErrorHandler:
                Call mdlMain.ErrorExit
    
            End Sub

            'テキストをすべて選択する。
            Private Sub txtAntHeight_GotFocus()

                On Error GoTo ErrorHandler
    
                Call SelectText(txtAntHeight)
    
                Exit Sub
    
            ErrorHandler:
                Call mdlMain.ErrorExit
    
            End Sub

            'テキストをすべて選択する。
            Private Sub txtElevationMask_GotFocus()

                On Error GoTo ErrorHandler
    
                Call SelectText(txtElevationMask)
    
                Exit Sub
    
            ErrorHandler:
                Call mdlMain.ErrorExit
    
            End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]


        //1==========================================================================================
        /*[VB]
            'タブ変更
            Private Sub tsTab_MouseUp(Button As Integer, Shift As Integer, X As Single, Y As Single)

                On Error GoTo ErrorHandler
    
                Call ChangeTab(tsTab, fraTab)
    
                Exit Sub
    
            ErrorHandler:
                Call mdlMain.ErrorExit
    
            End Sub

            'タブ変更
            Private Sub tsTab_Click()

                On Error GoTo ErrorHandler
    
                Call ChangeTab(tsTab, fraTab)
    
                Exit Sub
    
            ErrorHandler:
                Call mdlMain.ErrorExit
    
            End Sub

            'リストの選択。
            Private Sub cmbNumberOfMinSV_Click()

                On Error GoTo ErrorHandler
    
                optNumberOfMinSVStatic.Value = True
    
                Exit Sub
    
            ErrorHandler:
                Call mdlMain.ErrorExit
    
            End Sub

            '受信機名称。
            Private Sub txtRecName_Change()

                On Error GoTo ErrorHandler
    
                optRecName.Value = True
    
                Exit Sub
    
            ErrorHandler:
                Call mdlMain.ErrorExit
    
            End Sub

            '受信機メーカー変更イベント。
            Private Sub cmbRecManufacturer_Click()

                On Error GoTo ErrorHandler
    
                '受信機名称の更新。
                Call MakeRecTypeList(cmbRecManufacturer.ListIndex)
    
                optRecList.Value = True
    
                Exit Sub
    
            ErrorHandler:
                Call mdlMain.ErrorExit
    
            End Sub

            '受信機名称ー変更イベント。
            Private Sub cmbRecType_Click()

                On Error GoTo ErrorHandler
    
                'カレントインデックスの更新。
                If cmbRecManufacturer.ListIndex >= 0 Then m_tRecInfos(cmbRecManufacturer.ListIndex).ListIndex = cmbRecType.ListIndex
    
                optRecList.Value = True
    
                Exit Sub
    
            ErrorHandler:
                Call mdlMain.ErrorExit
    
            End Sub

            'アンテナメーカー変更イベント。
            Private Sub cmbAntManufacturer_Click()

                On Error GoTo ErrorHandler
    
                'アンテナ名称の更新。
                Call MakeAntTypeList(cmbAntManufacturer.ListIndex)
    
                Exit Sub
    
            ErrorHandler:
                Call mdlMain.ErrorExit
    
            End Sub

            'アンテナ名称変更イベント。
            Private Sub cmbAntType_Click()

                On Error GoTo ErrorHandler
    
                'カレントインデックスの更新。
                If cmbAntManufacturer.ListIndex >= 0 Then m_tAntInfos(cmbAntManufacturer.ListIndex).ListIndex = cmbAntType.ListIndex
    
                '測位方法の更新。
                Call MakeMeasurementList(cmbAntManufacturer.ListIndex, cmbAntType.ListIndex)
    
                Exit Sub
    
            ErrorHandler:
                Call mdlMain.ErrorExit
    
            End Sub

            '測位方法変更イベント。
            Private Sub cmbAntMeasurement_Click()

                On Error GoTo ErrorHandler
    
                'カレントインデックスの更新。
                If cmbAntManufacturer.ListIndex >= 0 Then
                    If cmbAntType.ListIndex >= 0 Then m_tAntInfos(cmbAntManufacturer.ListIndex).DefTypeInfos(cmbAntType.ListIndex).ListIndex = cmbAntMeasurement.ListIndex
                End If
    
                Exit Sub
    
            ErrorHandler:
                Call mdlMain.ErrorExit
    
            End Sub

            '手簿に出力する最低高度角。
            Private Sub txtElevationMask_Change()

                On Error GoTo ErrorHandler
    
                optElevationMaskInput.Value = True
    
                Exit Sub
    
            ErrorHandler:
                Call mdlMain.ErrorExit
    
            End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]



        //1==========================================================================================
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
        private void CancelButton_Click(object sender, EventArgs e)
        {
            try
            {
                Result = vbCancel;
                Close();

            }
            catch (Exception ex)
            {
                _ = MessageBox.Show(ex.Message, "エラー発生", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        //1==========================================================================================
        /*[VB]
            'ＯＫでダイアログを終了する。
            Private Sub OKButton_Click()

                On Error GoTo ErrorHandler
    
                '入力値の検査。
                If Not CheckData() Then Exit Sub
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
                //'入力値の検査。
                if (!CheckData())
                {
                    return;
                }
                //'値を反映させる。
                ReflectData();
                //'終了。
                Result = vbOK;
                this.Close();

            }
            catch (Exception ex)
            {
                _ = MessageBox.Show(ex.Message, "エラー発生", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }


        //1==========================================================================================
        /*[VB]
            '*******************************************************************************
            'インターフェース

            '比較関数。
            '
            '引き数：
            'vElement1 オブジェクト1。
            'vElement2 オブジェクト2。
            '
            '戻り値：
            'vElement1 の方が小さい場合、負の値を返す。
            '等しい場合、0 を返す。
            'vElement2 の方が小さい場合、正の値を返す。
            Private Function VariantSortInterface_Compare(ByVal vElement1 As Variant, ByVal vElement2 As Variant) As Long
                VariantSortInterface_Compare = m_tInfos(vElement1).SortIndex - m_tInfos(vElement2).SortIndex
            End Function

            '*******************************************************************************
            'インプリメンテーション

            '定義情報をソートする。
            '
            'm_tInfos をソートして、その結果を tInfos に設定する。
            '
            '引き数：
            'tInfos ソートした定義情報が設定される。配列の要素は(-1 To ...)、要素 -1 は未使用。
            'nListIndex カレントインデックス。
            Private Sub SortInfos(ByRef tInfos() As DEFTYPEINFO_MANUFACTURER, ByRef nListIndex As Long)

                'ソート用配列を用意する。
                Dim vArray() As Variant
                Dim i As Long
                ReDim vArray(-1 To UBound(m_tInfos))
                For i = 0 To UBound(m_tInfos)
                    Let vArray(i) = i
                Next
    
                'クイックソート。
                Dim clsQuickSorter As New QuickSorter
                Call clsQuickSorter.SortVariant(Me, vArray, 0, UBound(vArray))
    
                ReDim tInfos(-1 To UBound(vArray))
                For i = 0 To UBound(vArray)
                    Let tInfos(i) = m_tInfos(vArray(i))
                Next
    
                'カレントインデックス。
                For i = 0 To UBound(vArray)
                    If nListIndex = vArray(i) Then
                        nListIndex = i
                        Exit For
                    End If
                Next
    
            End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        private void SortInfos(List<DEFTYPEINFO_MANUFACTURER> tInfos, ref long nListIndex)
        {

        }


        //1==========================================================================================
        /*[VB]
            '定義情報を初期化する。
            Private Sub InitDefTypeInfo()

                '受信機情報。
                Dim sPath As String
                sPath = App.Path & "\" & PROFILE_RCV_FILE
                Dim nUnknown As Long
                nUnknown = GetPrivateProfileString(PROFILE_RCV_SEC_LIST, PROFILE_RCV_KEY_UNKNOWN, "0", sPath)
                Dim nCount As Long
                nCount = GetPrivateProfileInt(PROFILE_RCV_SEC_LIST, PROFILE_RCV_KEY_COUNT, 0, sPath) '受信機数。
                ReDim m_tInfos(-1 To nCount - 1) '十分な配列数。
                Dim objKeyToIndex As New Collection
                Dim vIndex As Variant
                Dim nListIndex As Long
                Dim i As Long
                nCount = 0
                nListIndex = -1
                For i = 0 To UBound(m_tInfos)
                    'Unknown はスキップ。
                    If i = nUnknown Then
                        i = i + 1
                        If i > UBound(m_tInfos) Then Exit For
                    End If
        
                    Dim sRcv As String
                    sRcv = GetPrivateProfileString(PROFILE_RCV_SEC_LIST, PROFILE_RCV_KEY_RCV & CStr(i), "", sPath)
                    Dim sManufacturer As String
                    sManufacturer = GetPrivateProfileString(sRcv, PROFILE_RCV_KEY_MANUFACTURE, "", sPath)
        
                    If Not LookupCollectionVariant(objKeyToIndex, vIndex, sManufacturer) Then
                        'メーカー別定義情報の追加。
                        vIndex = nCount
                        nCount = nCount + 1
                        Call objKeyToIndex.Add(vIndex, sManufacturer)
                        m_tInfos(vIndex).Manufacturer = sManufacturer
                        m_tInfos(vIndex).ListIndex = -1
                        m_tInfos(vIndex).SortIndex = GetManufacturerSortIndex(sManufacturer)
                        ReDim m_tInfos(vIndex).DefTypeInfos(-1 To -1)
                    End If
        
                    '定義情報の追加。
                    Dim nUBound As Long
                    nUBound = UBound(m_tInfos(vIndex).DefTypeInfos) + 1
                    ReDim Preserve m_tInfos(vIndex).DefTypeInfos(-1 To nUBound)
                    m_tInfos(vIndex).DefTypeInfos(nUBound).DefType = sRcv
                    m_tInfos(vIndex).DefTypeInfos(nUBound).Name = GetPrivateProfileString(sRcv, PROFILE_RCV_KEY_TYPE, sRcv, sPath)
        
                    'カレントインデックス。
                    If m_tInfos(vIndex).DefTypeInfos(nUBound).DefType = RecType Then
                        nListIndex = vIndex
                        m_tInfos(vIndex).ListIndex = nUBound
                    End If
                Next
                ReDim Preserve m_tInfos(-1 To nCount - 1)
                Call SortInfos(m_tRecInfos, nListIndex)
    
                '受信機メーカーリスト。
                For i = 0 To UBound(m_tRecInfos)
                    Dim sItem As String
                    sItem = GetManufacturerDispJ(m_tRecInfos(i).Manufacturer)
                    If sItem = "" Then sItem = "不明"
                    Call cmbRecManufacturer.AddItem(sItem)
                Next
                '初期値。
                If cmbRecManufacturer.ListCount > 0 Then
                    If nListIndex < 0 Then
                        cmbRecManufacturer.ListIndex = 0
                        optRecName.Value = True
                    Else
                        cmbRecManufacturer.ListIndex = nListIndex
                        optRecList.Value = True
                    End If
                Else
                    Call MakeRecTypeList(-1)
                    optRecName.Value = True
                End If
    
                '受信機名。
                If optRecName.Value Then txtRecName.Text = RecType
    
                'アンテナ情報。
                sPath = App.Path & "\" & PROFILE_ANT_FILE
                nUnknown = GetPrivateProfileString(PROFILE_ANT_SEC_LIST, PROFILE_ANT_KEY_UNKNOWN, "0", sPath)
                nCount = GetPrivateProfileInt(PROFILE_ANT_SEC_LIST, PROFILE_ANT_KEY_COUNT, 0, sPath)
                ReDim m_tInfos(-1 To nCount - 1)
                Set objKeyToIndex = New Collection
                nCount = 0
                nListIndex = -1
                For i = 0 To UBound(m_tInfos)
                    Dim sAnt As String
                    sAnt = GetPrivateProfileString(PROFILE_ANT_SEC_LIST, PROFILE_ANT_KEY_ANT & CStr(i), "", sPath)
                    sManufacturer = GetPrivateProfileString(sAnt, PROFILE_ANT_KEY_MANUFACTURE, "", sPath)
        
                    If Not LookupCollectionVariant(objKeyToIndex, vIndex, sManufacturer) Then
                        'メーカー別定義情報の追加。
                        vIndex = nCount
                        nCount = nCount + 1
                        Call objKeyToIndex.Add(vIndex, sManufacturer)
                        m_tInfos(vIndex).Manufacturer = sManufacturer
                        m_tInfos(vIndex).ListIndex = -1
                        m_tInfos(vIndex).SortIndex = GetManufacturerSortIndex(sManufacturer)
                        ReDim m_tInfos(vIndex).DefTypeInfos(-1 To -1)
                    End If
        
                    '定義情報の追加。
                    nUBound = UBound(m_tInfos(vIndex).DefTypeInfos) + 1
                    ReDim Preserve m_tInfos(vIndex).DefTypeInfos(-1 To nUBound)
                    m_tInfos(vIndex).DefTypeInfos(nUBound).DefType = sAnt
                    m_tInfos(vIndex).DefTypeInfos(nUBound).Name = GetPrivateProfileString(sAnt, PROFILE_ANT_KEY_TYPE, "", sPath) & IIf(i = nUnknown, "", " (" & GetPrivateProfileString(sAnt, PROFILE_ANT_KEY_SUB, "", sPath) & ")")
                    m_tInfos(vIndex).DefTypeInfos(nUBound).ListIndex = -1
                    ReDim m_tInfos(vIndex).DefTypeInfos(nUBound).AntMeasurement(-1 To -1)
                    ReDim m_tInfos(vIndex).DefTypeInfos(nUBound).DispMeasurement(-1 To -1)
        
                    'カレントインデックス。
                    If m_tInfos(vIndex).DefTypeInfos(nUBound).DefType = AntType Then
                        nListIndex = vIndex
                        m_tInfos(vIndex).ListIndex = nUBound
                    End If
        
                    '測位方法。
                    Dim nMeasurement As Long
                    nMeasurement = 0
                    Do
                        Dim clsStringTokenizer As New StringTokenizer
                        clsStringTokenizer.Source = GetPrivateProfileString(m_tInfos(vIndex).DefTypeInfos(nUBound).DefType, PROFILE_ANT_KEY_MEASUREMENT & CStr(nMeasurement), "", sPath)
                        If clsStringTokenizer.Source = "" Then Exit Do
                        ReDim Preserve m_tInfos(vIndex).DefTypeInfos(nUBound).AntMeasurement(-1 To nMeasurement)
                        ReDim Preserve m_tInfos(vIndex).DefTypeInfos(nUBound).DispMeasurement(-1 To nMeasurement)
                        Call clsStringTokenizer.Begin
                        Dim sToken As String
                        sToken = clsStringTokenizer.NextToken
                        m_tInfos(vIndex).DefTypeInfos(nUBound).DispMeasurement(nMeasurement) = Mid$(sToken, 2, Len(sToken) - 2)
                        m_tInfos(vIndex).DefTypeInfos(nUBound).AntMeasurement(nMeasurement) = clsStringTokenizer.NextToken
            
                        'カレントインデックス。
                        If m_tInfos(vIndex).ListIndex = nUBound Then
                            If m_tInfos(vIndex).DefTypeInfos(nUBound).AntMeasurement(nMeasurement) = AntMeasurement Then
                                m_tInfos(vIndex).DefTypeInfos(nUBound).ListIndex = nMeasurement
                            End If
                        End If
            
                        nMeasurement = nMeasurement + 1
                    Loop
                Next
                ReDim Preserve m_tInfos(-1 To nCount - 1)
                Call SortInfos(m_tAntInfos, nListIndex)
    
                For i = 0 To UBound(m_tAntInfos)
                    sItem = GetManufacturerDispJ(m_tAntInfos(i).Manufacturer)
                    If sItem = "" Then sItem = "不明"
                    Call cmbAntManufacturer.AddItem(sItem)
                Next
                '初期値。
                If cmbAntManufacturer.ListCount > 0 Then
                    If nListIndex < 0 Then
                        cmbAntManufacturer.ListIndex = 0
                    Else
                        cmbAntManufacturer.ListIndex = nListIndex
                    End If
                Else
                    Call MakeAntTypeList(-1)
                End If
    
            End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        private void InitDefTypeInfo()
        {
            string App_Path = @"C:\Develop\NetSurv\Src\NS-App\NS-Survey";

            /*
                '受信機情報。
                Dim sPath As String
                sPath = App.Path & "\" & PROFILE_RCV_FILE
                Dim nUnknown As Long
                nUnknown = GetPrivateProfileString(PROFILE_RCV_SEC_LIST, PROFILE_RCV_KEY_UNKNOWN, "0", sPath)
                Dim nCount As Long
                nCount = GetPrivateProfileInt(PROFILE_RCV_SEC_LIST, PROFILE_RCV_KEY_COUNT, 0, sPath) '受信機数。
                //ReDim m_tInfos(-1 To nCount - 1) '十分な配列数。
                Dim objKeyToIndex As New Collection
                Dim vIndex As Variant
                Dim nListIndex As Long
                Dim i As Long
                nCount = 0
                nListIndex = -1
             */

            //'受信機情報。
            string sPath;
            sPath = App_Path + "\\" + PROFILE_RCV_FILE;
            long nUnknown;
            nUnknown = long.Parse(GetPrivateProfileString(PROFILE_RCV_SEC_LIST, PROFILE_RCV_KEY_UNKNOWN, "0", sPath));
            long nCount;
            nCount = GetPrivateProfileInt(PROFILE_RCV_SEC_LIST, PROFILE_RCV_KEY_COUNT, 0, sPath);   //'受信機数。
            Dictionary<string, object> objKeyToIndex = new Dictionary<string, object>();
            string vIndex = "";
            long nListIndex;
            long i;
            nCount = 0;
            nListIndex = -1;


            /*
                List<DEFTYPEINFO_MANUFACTURER> m_tInfos = new List<DEFTYPEINFO_MANUFACTURER>();     //'定義情報。配列の要素は(-1 To ...)、要素 -1 は未使用。
                public struct DEFTYPEINFO_MANUFACTURER   //'メーカー別定義情報
                {
                    string Manufacturer;                //'メーカー名。
                    List<DEFTYPEINFO> DefTypeInfos;     //'定義情報。
                    long ListIndex;                     //'定義情報のカレントのリストインデックス。
                    long SortIndex;                     //'並び順番号。
                }
             */

            DEFTYPEINFO_MANUFACTURER m_tInfosA = new DEFTYPEINFO_MANUFACTURER();
            DEFTYPEINFO DefTypeInfosA = new DEFTYPEINFO();

            for (i = 0; i < m_tInfos.Count;)
            {
                //'Unknown はスキップ。
                if (i == nUnknown)
                {
                    i = i + 1;
                    if (i > m_tInfos.Count) {
                        break;
                    }
                }
                string sRcv;
                sRcv = GetPrivateProfileString(PROFILE_RCV_SEC_LIST, PROFILE_RCV_KEY_RCV + i.ToString(), "", sPath);
                string sManufacturer;
                sManufacturer = GetPrivateProfileString(sRcv, PROFILE_RCV_KEY_MANUFACTURE, "", sPath);



                if (!LookupCollectionVariant(objKeyToIndex, ref vIndex, sManufacturer))
                {
                    //'メーカー別定義情報の追加。
                    vIndex = nCount.ToString();
                    nCount = nCount + 1;

                    objKeyToIndex.Add(vIndex, sManufacturer);

                    //メーカー別定義情報
                    m_tInfosA.Manufacturer = sManufacturer;             //'メーカー名。
                    m_tInfosA.ListIndex = -1;                           //'定義情報のカレントのリストインデックス。
                    m_tInfosA.SortIndex = GetManufacturerSortIndex(sManufacturer);          //'並び順番号。
                    m_tInfosA.DefTypeInfos = new List<DEFTYPEINFO>();   //'定義情報。
                    m_tInfos.Add(m_tInfosA);


                    //'定義情報の追加。
                    /*
                        Dim nUBound As Long
                        nUBound = UBound(m_tInfos(vIndex).DefTypeInfos) + 1
                        ReDim Preserve m_tInfos(vIndex).DefTypeInfos(-1 To nUBound)
                        m_tInfos(vIndex).DefTypeInfos(nUBound).DefType = sRcv
                        m_tInfos(vIndex).DefTypeInfos(nUBound).Name = GetPrivateProfileString(sRcv, PROFILE_RCV_KEY_TYPE, sRcv, sPath)
                     */
                    long nUBound;

                    DefTypeInfosA.DefType = sRcv;
                    DefTypeInfosA.Name = GetPrivateProfileString(sRcv, PROFILE_RCV_KEY_TYPE, sRcv, sPath);
                    m_tInfos[int.Parse(vIndex)].DefTypeInfos.Add(DefTypeInfosA);

                    nUBound = m_tInfos[int.Parse(vIndex)].DefTypeInfos.Count + 1;


                    //'カレントインデックス。
                    /*
                        If m_tInfos(vIndex).DefTypeInfos(nUBound).DefType = RecType Then
                            nListIndex = vIndex
                            m_tInfos(vIndex).ListIndex = nUBound
                        End If
                     */
                    if (m_tInfos[int.Parse(vIndex)].DefTypeInfos[(int)nUBound].DefType == RecType)
                    {
                        nListIndex = long.Parse(vIndex);
                        //  m_tInfos[0].ListIndex = nUBound;
                    }

                    //ReDim Preserve m_tInfos(-1 To nCount - 1)
                    SortInfos(m_tRecInfos, ref nListIndex);

#if false
    
                '受信機メーカーリスト。
                For i = 0 To UBound(m_tRecInfos)
                    Dim sItem As String
                    sItem = GetManufacturerDispJ(m_tRecInfos(i).Manufacturer)
                    If sItem = "" Then sItem = "不明"
                    Call cmbRecManufacturer.AddItem(sItem)
                Next
                '初期値。
                If cmbRecManufacturer.ListCount > 0 Then
                    If nListIndex < 0 Then
                        cmbRecManufacturer.ListIndex = 0
                        optRecName.Value = True
                    Else
                        cmbRecManufacturer.ListIndex = nListIndex
                        optRecList.Value = True
                    End If
                Else
                    Call MakeRecTypeList(-1)
                    optRecName.Value = True
                End If
    
                '受信機名。


#endif


                }//if (!LookupCollectionVariant(objKeyToIndex, ref vIndex, sManufacturer))


            }//for (i = 0; i < m_tInfos.Count)



#if false


#endif
        }





        //1==========================================================================================
        /*[VB]
            '受信機名称リストを作成する。
            '
            '引き数：
            'nManufacturer 受信機メーカー。m_tRecInfos のインデックス。
            Private Sub MakeRecTypeList(ByVal nManufacturer As Long)

                Call cmbRecType.Clear
    
                If nManufacturer < 0 Then Exit Sub
    
                Dim i As Long
                For i = 0 To UBound(m_tRecInfos(nManufacturer).DefTypeInfos)
                    Call cmbRecType.AddItem(m_tRecInfos(nManufacturer).DefTypeInfos(i).Name)
                Next
    
                If cmbRecType.ListCount > 0 Then
                    If m_tRecInfos(nManufacturer).ListIndex < 0 Or cmbRecType.ListCount <= m_tRecInfos(nManufacturer).ListIndex Then
                        cmbRecType.ListIndex = 0
                    Else
                        cmbRecType.ListIndex = m_tRecInfos(nManufacturer).ListIndex
                    End If
                End If
    
            End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]

        //1==========================================================================================
        /*[VB]
            'アンテナ名称リストを作成する。
            '
            '引き数：
            'nManufacturer 受信機メーカー。m_tAntInfos のインデックス。
            Private Sub MakeAntTypeList(ByVal nManufacturer As Long)

                Call cmbAntType.Clear
    
                If nManufacturer < 0 Then Exit Sub
    
                Dim i As Long
                For i = 0 To UBound(m_tAntInfos(nManufacturer).DefTypeInfos)
                    Call cmbAntType.AddItem(m_tAntInfos(nManufacturer).DefTypeInfos(i).Name)
                Next
    
                If cmbAntType.ListCount > 0 Then
                    If m_tAntInfos(nManufacturer).ListIndex < 0 Or cmbAntType.ListCount <= m_tAntInfos(nManufacturer).ListIndex Then
                        cmbAntType.ListIndex = 0
                    Else
                        cmbAntType.ListIndex = m_tAntInfos(nManufacturer).ListIndex
                    End If
                Else
                    Call MakeMeasurementList(-1, -1)
                End If
    
            End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]

        //1==========================================================================================
        /*[VB]
            'アンテナ測位方法リストを作成する。
            '
            '引き数：
            'nManufacturer 受信機メーカー。m_tAntInfos のインデックス。
            'nAntType アンテナ種別。m_tAntInfos.DefTypeInfos のインデックス。
            Private Sub MakeMeasurementList(ByVal nManufacturer As Long, ByVal nAntType As Long)

                Call cmbAntMeasurement.Clear
    
                If nManufacturer < 0 Then Exit Sub
                If nAntType < 0 Then Exit Sub
    
                Dim i As Long
                For i = 0 To UBound(m_tAntInfos(nManufacturer).DefTypeInfos(nAntType).AntMeasurement)
                    Call cmbAntMeasurement.AddItem(m_tAntInfos(nManufacturer).DefTypeInfos(nAntType).DispMeasurement(i))
                Next
    
                If cmbAntMeasurement.ListCount > 0 Then
                    If m_tAntInfos(nManufacturer).DefTypeInfos(nAntType).ListIndex < 0 Or cmbAntMeasurement.ListCount <= m_tAntInfos(nManufacturer).DefTypeInfos(nAntType).ListIndex Then
                        cmbAntMeasurement.ListIndex = 0
                    Else
                        cmbAntMeasurement.ListIndex = m_tAntInfos(nManufacturer).DefTypeInfos(nAntType).ListIndex
                    End If
                End If
    
            End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]

        //1==========================================================================================
        /*[VB]
            '入力値を検査する。
            '
            '戻り値：
            '入力値が正常である場合 True を返す。
            'それ以外の場合 False を返す。
            Private Function CheckData() As Boolean

                CheckData = False
    
                'セッション名。
                If Not CheckSessionInput(cmbSession, m_objElements, fraTab(TAB_OBSERVATION).Visible) Then
                    tsTab.Tabs(TAB_OBSERVATION + 1).Selected = True
                    Call cmbSession.SetFocus
                    Exit Function
                End If
    
                '手簿に出力する最低高度角。
                If optElevationMaskInput.Value Then
                    If Not CheckIntegerInputRange(txtElevationMask, "手簿に出力する最低高度角", udElevationMask.Min, udElevationMask.Max + 1, fraTab(TAB_OBSERVATION).Visible) Then
                        tsTab.Tabs(TAB_OBSERVATION + 1).Selected = True
                        Call txtElevationMask.SetFocus
                        Exit Function
                    End If
                End If
    
                '受信機名称。
                If optRecList.Value Then
                    If cmbRecManufacturer.ListIndex < 0 Then
                        Call MsgBox("受信機メーカーを選択してください。", vbCritical)
                        tsTab.Tabs(TAB_RECEIVER + 1).Selected = True
                        Call cmbRecManufacturer.SetFocus
                        Exit Function
                    End If
                    If cmbRecType.ListIndex < 0 Then
                        Call MsgBox("受信機名称を選択してください。", vbCritical)
                        tsTab.Tabs(TAB_RECEIVER + 1).Selected = True
                        Call cmbRecType.SetFocus
                        Exit Function
                    End If
                ElseIf optRecName.Value Then
                    If Not CheckInputLength(txtRecName, "受信機名称", RINEX_STR_RECNAME - 1, fraTab(TAB_RECEIVER).Visible) Then
                        tsTab.Tabs(TAB_RECEIVER + 1).Selected = True
                        Call txtRecName.SetFocus
                        Exit Function
                    End If
                    If Not CheckStringInputInvalid(txtRecName, "受信機名称", fraTab(TAB_RECEIVER).Visible) Then
                        tsTab.Tabs(TAB_RECEIVER + 1).Selected = True
                        Call txtRecName.SetFocus
                        Exit Function
                    End If
                Else
                    Call MsgBox("受信機を選択してください。", vbCritical)
                    tsTab.Tabs(TAB_RECEIVER + 1).Selected = True
                    Exit Function
                End If
    
                '受信機シリアル番号。
                If Not CheckInputLength(txtRecNumber, "受信機シリアル", RINEX_STR_RECNUMBER - 1, fraTab(TAB_RECEIVER).Visible) Then
                    tsTab.Tabs(TAB_RECEIVER + 1).Selected = True
                    Call txtRecNumber.SetFocus
                    Exit Function
                End If
                If Not CheckStringInputInvalid(txtRecNumber, "受信機シリアル", fraTab(TAB_RECEIVER).Visible) Then
                    tsTab.Tabs(TAB_RECEIVER + 1).Selected = True
                    Call txtRecNumber.SetFocus
                    Exit Function
                End If
    
                'アンテナ名称。
                If cmbAntManufacturer.ListIndex < 0 Then
                    Call MsgBox("アンテナメーカーを選択してください。", vbCritical)
                    tsTab.Tabs(TAB_ANTENNA + 1).Selected = True
                    Call cmbAntManufacturer.SetFocus
                    Exit Function
                End If
                If cmbAntType.ListIndex < 0 Then
                    Call MsgBox("アンテナ名称を選択してください。", vbCritical)
                    tsTab.Tabs(TAB_ANTENNA + 1).Selected = True
                    Call cmbAntType.SetFocus
                    Exit Function
                End If
    
                'アンテナシリアル番号。
                If Not CheckInputLength(txtAntNumber, "アンテナシリアル", RINEX_STR_ANTNUMBER - 1, fraTab(TAB_ANTENNA).Visible) Then
                    tsTab.Tabs(TAB_ANTENNA + 1).Selected = True
                    Call txtAntNumber.SetFocus
                    Exit Function
                End If
                If Not CheckStringInputInvalid(txtAntNumber, "アンテナシリアル", fraTab(TAB_ANTENNA).Visible) Then
                    tsTab.Tabs(TAB_ANTENNA + 1).Selected = True
                    Call txtAntNumber.SetFocus
                    Exit Function
                End If
    
                '測位方法。
                If cmbAntMeasurement.ListIndex < 0 Then
                    Call MsgBox("測位方法を選択してください。", vbCritical)
                    tsTab.Tabs(TAB_ANTENNA + 1).Selected = True
                    Call cmbAntMeasurement.SetFocus
                    Exit Function
                End If
    
                'アンテナ高。
                If Not CheckFloatInputRange(txtAntHeight, "アンテナ高", -32768, 32768, fraTab(TAB_ANTENNA).Visible) Then
                    tsTab.Tabs(TAB_ANTENNA + 1).Selected = True
                    Call txtAntHeight.SetFocus
                    Exit Function
                End If
                Dim nHeight As Double
                If Not GetDocument().NetworkModel.ObservationShared.GetTrueVertical(m_tAntInfos(cmbAntManufacturer.ListIndex).DefTypeInfos(cmbAntType.ListIndex).DefType, m_tAntInfos(cmbAntManufacturer.ListIndex).DefTypeInfos(cmbAntType.ListIndex).AntMeasurement(cmbAntMeasurement.ListIndex), Val(txtAntHeight.Text), nHeight) Then
                    If Not GetDocument().NetworkModel.ObservationShared.GetMountVertical(m_tAntInfos(cmbAntManufacturer.ListIndex).DefTypeInfos(cmbAntType.ListIndex).DefType, m_tAntInfos(cmbAntManufacturer.ListIndex).DefTypeInfos(cmbAntType.ListIndex).AntMeasurement(cmbAntMeasurement.ListIndex), Val(txtAntHeight.Text), nHeight) Then
                        Call MsgBox("アンテナ高が不正です。", vbCritical)
                        tsTab.Tabs(TAB_ANTENNA + 1).Selected = True
                        Call txtAntHeight.SetFocus
                        Exit Function
                    End If
                End If
    
                If Analysis Then
                    Dim bAntChange As Boolean
                    bAntChange = Abs(AntHeight - Val(txtAntHeight.Text)) >= FLT_EPSILON
        
                    If cmbAntManufacturer.ListIndex >= 0 Then
                        If cmbAntType.ListIndex >= 0 Then
                            If AntType <> m_tAntInfos(cmbAntManufacturer.ListIndex).DefTypeInfos(cmbAntType.ListIndex).DefType Then
                                bAntChange = True
                            ElseIf cmbAntMeasurement.ListIndex >= 0 Then
                                If AntMeasurement <> m_tAntInfos(cmbAntManufacturer.ListIndex).DefTypeInfos(cmbAntType.ListIndex).AntMeasurement(cmbAntMeasurement.ListIndex) Then
                                    bAntChange = True
                                End If
                            End If
                        End If
                    End If
        
                    If bAntChange Then
                        If MsgBox("アンテナの情報を変更しますと解析の結果が失われます。よろしいですか?", vbExclamation Or vbOKCancel) <> vbOK Then Exit Function
                    End If
                End If
    
                If Not CheckSessionExtend(cmbSession.Text, m_objElements, Extend) Then Exit Function
    
                CheckData = True
    
            End Function
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        private bool CheckData()
        {
            bool CheckData = false;




            CheckData = true;
            return CheckData;
        }



        //1==========================================================================================
        /*[VB]
            '値を反映させる。
            Private Sub ReflectData()

                '値の取得。
                Session = cmbSession.Text
                RecNumber = txtRecNumber.Text
                AntNumber = txtAntNumber.Text
                AntHeight = Val(txtAntHeight.Text)
    
                If optRecList.Value Then
                    If cmbRecManufacturer.ListIndex >= 0 Then
                        If cmbRecType.ListIndex >= 0 Then
                            RecType = m_tRecInfos(cmbRecManufacturer.ListIndex).DefTypeInfos(cmbRecType.ListIndex).DefType
                        End If
                    End If
                Else
                    RecType = txtRecName.Text
                End If
    
                If cmbAntManufacturer.ListIndex >= 0 Then
                    If cmbAntType.ListIndex >= 0 Then
                        AntType = m_tAntInfos(cmbAntManufacturer.ListIndex).DefTypeInfos(cmbAntType.ListIndex).DefType
                        If cmbAntMeasurement.ListIndex >= 0 Then
                            AntMeasurement = m_tAntInfos(cmbAntManufacturer.ListIndex).DefTypeInfos(cmbAntType.ListIndex).AntMeasurement(cmbAntMeasurement.ListIndex)
                        Else
                            AntMeasurement = ""
                        End If
                    End If
                End If
    
                ElevationMaskHand = IIf(optElevationMaskObsData.Value, -1, Val(txtElevationMask.Text))
                NumberOfMinSVHand = IIf(optNumberOfMinSVObsData.Value, 0, cmbNumberOfMinSV.ListIndex + 1)
    
            End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        private void ReflectData()
        {

        }







    }
}
