using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.LinkLabel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using System.Windows.Forms;
using System.Collections.ObjectModel;
using static SurvLine.mdl.DEFINE;
using static SurvLine.mdl.MdlNSDefine;
using static SurvLine.mdl.MdlNSSDefine;
using static SurvLine.mdl.MdlRegistAngle;

namespace SurvLine
{
    public class RegistAngle
    {
        //==========================================================================================
        /*[VB]
        '*******************************************************************************
        '閉合登録
        '
        '閉合登録の操作を行う。
        '登録したオブジェクトは m_clsRegistHead に連結する。
        'm_clsRegistHead リストの最後尾は始点となる観測点(A)が設定される。
        'm_clsRegistHead リストの後ろから2番目は最初の基線ベクトル(B)が設定される。
        'm_clsRegistHead リストの後ろから3番目は基線ベクトル(B)の観測点(A)とは反対側の観測点が設定される。
        '以降、基線ベクトルと観測点が交互に設定され、先頭には終点となる観測点が設定される。
        'm_clsRegistHead に設定される ObservationPoint オブジェクトは接合観測点である。
        'm_clsCandidateHead リストにも基線ベクトルと観測点が混合で設定される。
        '最初(何も登録されていない初期状態)の候補オブジェクトは基線ベクトルのみである。
        '一つでも登録された後の候補オブジェクトは基線ベクトルと観測点が交互に設定される。
        '先頭が観測点で最後尾が基線ベクトルになるように交互に設定される。
        'm_clsCandidateHead に設定される ObservationPoint オブジェクトは接合観測点である。
        'BackSpaceキーはウィンドウズフックを利用してキーボードをフックする。

        Option Explicit
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //==========================================================================================

        //==========================================================================================
        //[C#]
        public RegistAngle()
        {
            Class_Initialize();
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '登録戻しイベント。
        '
        '閉合に最後に登録された基線ベクトルが登録から削除され、一つ巻き戻ったときに発生する。
        Event BackRegist()

        '閉合完成イベント。
        '
        '基線ベクトルが登録されたとき、閉合が完成された場合に発生する。
        '
        '引き数：
        'clsChainList 登録オブジェクトリストの先頭。
        Event CompletedAngle(ByVal clsChainList As ChainList)
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '登録戻しイベント。
        '
        '閉合に最後に登録された基線ベクトルが登録から削除され、一つ巻き戻ったときに発生する。
        */
#if false
        /*
         *************************** 修正要 sakai
         */
        Event BackRegist()
#endif

        /*
        '閉合完成イベント。
        '
        '基線ベクトルが登録されたとき、閉合が完成された場合に発生する。
        '
        '引き数：
        'clsChainList 登録オブジェクトリストの先頭。
        */
#if false
        /*
         *************************** 修正要 sakai
         */
        Event CompletedAngle(ByVal clsChainList As ChainList)
#endif
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'プロパティ
        Public Ring As Boolean '環フラグ。True=環閉合、False=電子基準点間閉合。
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'プロパティ
        public bool Ring;                                       //'環フラグ。True=環閉合、False=電子基準点間閉合。
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'インプリメンテーション
        Private m_clsStartVector As BaseLineVector 'スタート基線ベクトル。
        Private m_clsStartPoint As ObservationPoint 'スタート観測点(接合観測点)。
        Private m_clsRegistHead As New ChainList '登録オブジェクトリストのヘッダー。
        Private m_clsCandidateHead As New ChainList '候補オブジェクトリストのヘッダー。
        Private m_clsRegistVectors As Collection '登録基線ベクトルコレクション。要素は BaseLineVector オブジェクト。キーは要素のポインタ。
        Private m_hHook As Long 'フックハンドル。
        Private m_bCompleted As Boolean '閉合完成フラグ。
        Private m_nConnectDistance As Double '繋がっていなくても接続と見なす距離(ｍ)。
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'インプリメンテーション
        private BaseLineVector m_clsStartVector;                //'スタート基線ベクトル。
        private ObservationPoint m_clsStartPoint;               //'スタート観測点(接合観測点)。
        private ChainList m_clsRegistHead = new ChainList();    //'登録オブジェクトリストのヘッダー。
        private ChainList m_clsCandidateHead = new ChainList(); //'候補オブジェクトリストのヘッダー。
        private Dictionary<string, object> m_clsRegistVectors;  //'登録基線ベクトルコレクション。要素は BaseLineVector オブジェクト。キーは要素のポインタ。
        private long m_hHook;                                   //'フックハンドル。
        private bool m_bCompleted;                              //'閉合完成フラグ。
        private double m_nConnectDistance;                      //'繋がっていなくても接続と見なす距離(ｍ)。
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '*******************************************************************************
        'プロパティ

        '登録オブジェクトリスト。
        Property Get RegistList() As ChainList
            Set RegistList = m_clsRegistHead.NextList
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '*******************************************************************************
        'プロパティ
        
        '登録オブジェクトリスト。
        */
        public ChainList RegistList()
        {
            return m_clsRegistHead.NextList();
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '候補オブジェクトリスト。
        Property Get CandidateList() As ChainList
            Set CandidateList = m_clsCandidateHead.NextList
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'候補オブジェクトリスト。
        public ChainList CandidateList()
        {
            return m_clsCandidateHead.NextList();
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '閉合完成フラグ。
        Property Get Completed() As Boolean
            Completed = m_bCompleted
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'閉合完成フラグ。
        public bool Completed()
        {
            return m_bCompleted;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '*******************************************************************************
        'イベント

        '初期化。
        Private Sub Class_Initialize()

            On Error GoTo ErrorHandler


            m_hHook = 0
    
            If GetPrivateProfileInt(PROFILE_SAVE_SEC_CHECKCONNECT, PROFILE_SAVE_KEY_ENABLE, REGISTANGLE_DEF_CONNECTENABLE, App.Path & "\" & App.Title & PROFILE_SAVE_EXT) <> 0 Then
                m_nConnectDistance = GetPrivateProfileInt(PROFILE_SAVE_SEC_CHECKCONNECT, PROFILE_SAVE_KEY_DISTANCE, REGISTANGLE_DEF_CONNECTDISTANCE, App.Path & "\" & App.Title & PROFILE_SAVE_EXT) / 100
            Else
                m_nConnectDistance = -1
            End If


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
        
        '初期化。
        */
        private void Class_Initialize()
        {
            try
            {
                m_hHook = 0;

                string AppPath = System.IO.Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath);
                string AppTitle = "NS-Survey";

                if (GetPrivateProfileInt(PROFILE_SAVE_SEC_CHECKCONNECT, PROFILE_SAVE_KEY_ENABLE, (int)REGISTANGLE_DEF_CONNECTENABLE, AppPath + "\\" + AppTitle + PROFILE_SAVE_EXT) != 0)
                {
                    m_nConnectDistance = GetPrivateProfileInt(PROFILE_SAVE_SEC_CHECKCONNECT, PROFILE_SAVE_KEY_DISTANCE, (int)REGISTANGLE_DEF_CONNECTDISTANCE,
                                    AppPath + "\\" + AppTitle + PROFILE_SAVE_EXT) / 100;
                }
                else
                {
                    m_nConnectDistance = -1;
                }
            }

            catch (Exception)
            {
                return;
            }
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '終了処理。
        Private Sub Class_Terminate()

            On Error GoTo ErrorHandler


            Call m_clsRegistHead.RemoveAll
            Call m_clsCandidateHead.RemoveAll
            Call EndRegistAngle


            Exit Sub


        ErrorHandler:
            Call ErrorExit


        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'終了処理。
        private void Class_Terminate()
        {
            return;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '*******************************************************************************
        'メソッド

        '閉合登録の開始。
        '
        '引き数：
        'hWnd ウィンドウズフックをインストールするウィンドウのハンドル。
        Public Sub BeginRegist(ByVal hWnd As Long)
            'フックプロシージャのインストール。
            Call SetRegistAngle(Me)
            Dim hInstance As Long
            Dim nThreadID As Long
            hInstance = GetWindowLong(hWnd, GWL_HINSTANCE)
            nThreadID = GetCurrentThreadId()
            m_hHook = SetWindowsHookEx(WH_KEYBOARD, AddressOf KeyboardProc, hInstance, nThreadID)
            If m_hHook = 0 Then Call Err.Raise(ERR_FATAL, , GetLastErrorMessage())
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '*******************************************************************************
        'メソッド
        
        '閉合登録の開始。
        '
        '引き数：
        'hWnd ウィンドウズフックをインストールするウィンドウのハンドル。
        */
        public void BeginRegist(long hWnd)
        {
            return;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '閉合登録の終了。
        Public Sub EndRegistAngle()
            'フックプロシージャの削除。
            If m_hHook<> 0 Then
                Call UnhookWindowsHookEx(m_hHook)
                m_hHook = 0
            End If
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'閉合登録の終了。
        public void EndRegistAngle()
        {
            return;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'キーフック処理。
        '
        '引き数：
        'code フックコード。
        'wParam 仮想キーコード｡
        'lParam キーストロークメッセージの情報｡
        '
        '戻り値：CallNextHookEx の戻り値。
        Public Function KeyborardEvent(ByVal code As Long, ByVal wParam As Long, ByVal lParam As Long) As Long
            'BackSpaceキーなら登録閉合を一つ削除する。
            If code >= 0 Then
                If code = HC_ACTION Then
                    If wParam = VK_BACK Then
                        If(lParam And & H80000000) = 0 Then
                            '通知。
                            RaiseEvent BackRegist
                        End If
                    End If
                End If
            End If
            'フックチェイン。
            KeyborardEvent = CallNextHookEx(m_hHook, code, wParam, lParam)
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        'キーフック処理。
        '
        '引き数：
        'code フックコード。
        'wParam 仮想キーコード｡
        'lParam キーストロークメッセージの情報｡
        '
        '戻り値：CallNextHookEx の戻り値。
        */
        public long KeyborardEvent(long code, long wParam, long lParam)
        {
            return 0;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '指定された基線ベクトルが候補として有効か検査する。
        '
        '引き数：
        'clsBaseLineVector 基線ベクトル。
        '
        '戻り値：
        '候補として有効であれば True を返す。
        'それ以外の場合 False を返す。
        Public Function IsCandidate(ByVal clsBaseLineVector As BaseLineVector) As Boolean

            IsCandidate = False


            If m_clsStartVector Is Nothing Then
                '最初の候補として無効なら却下。
                If Not IsFirstCandidate(clsBaseLineVector) Then Exit Function
            Else
                '候補オブジェクトか？
                Dim clsChainList As ChainList
                Set clsChainList = m_clsCandidateHead.NextList
                Do While Not clsChainList Is Nothing
                    If clsChainList.Element Is clsBaseLineVector Then Exit Do
                    Set clsChainList = clsChainList.NextList
                Loop
                '候補オブジェクトでなければ却下。
                If clsChainList Is Nothing Then Exit Function
            End If


            IsCandidate = True


        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '指定された基線ベクトルが候補として有効か検査する。
        '
        '引き数：
        'clsBaseLineVector 基線ベクトル。
        '
        '戻り値：
        '候補として有効であれば True を返す。
        'それ以外の場合 False を返す。
        */
        public bool IsCandidate(BaseLineVector clsBaseLineVector)
        {
            return false;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '候補オブジェクトの初期化。
        Public Sub InitializeCandidate()
            m_bCompleted = False
            '候補オブジェクトの登録。
            Call RegistCandidate(Nothing, Nothing)
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'候補オブジェクトの初期化。
        public void InitializeCandidate()
        {
            return;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'クリック処理。
        '
        '指定された基線ベクトルを閉合に登録する。
        '
        '引き数：
        'clsBaseLineVector クリックされた基線ベクトル。
        '
        '戻り値：
        '指定された基線ベクトルが登録された場合 True を返す。
        'それ以外の場合 False を返す。
        Public Function Click(ByVal clsBaseLineVector As BaseLineVector) As Boolean

            Click = False
            m_bCompleted = False


            If m_clsStartVector Is Nothing Then
                '最初の候補として無効なら何もしない。
                If Not IsFirstCandidate(clsBaseLineVector) Then Exit Function
                '登録開始。
                Set m_clsStartVector = clsBaseLineVector
                Set m_clsStartPoint = Nothing
                '登録基線ベクトルコレクションへ追加。
                Set m_clsRegistVectors = New Collection
                Call SetAtCollectionObject(m_clsRegistVectors, clsBaseLineVector, Hex$(GetPointer(clsBaseLineVector)))
                '登録オブジェクトに追加する。とりあえず固定点が最後尾になるように。
                Call m_clsRegistHead.RemoveAll
                If clsBaseLineVector.StrPoint.TopParentPoint.Fixed Then
                    Call m_clsRegistHead.InsertNext(clsBaseLineVector.StrPoint)
                    Call m_clsRegistHead.InsertNext(clsBaseLineVector)
                    Call m_clsRegistHead.InsertNext(clsBaseLineVector.EndPoint)  '観測点を先頭に登録。
                Else
                    Call m_clsRegistHead.InsertNext(clsBaseLineVector.EndPoint)
                    Call m_clsRegistHead.InsertNext(clsBaseLineVector)
                    Call m_clsRegistHead.InsertNext(clsBaseLineVector.StrPoint)  '観測点を先頭に登録。
                End If
                '候補オブジェクトの登録。
                Call m_clsCandidateHead.RemoveAll
                Call RegistCandidate(m_clsRegistHead.NextList.Element, clsBaseLineVector)
                If Ring Then
                    '環閉合なら反対側も候補とする。
                    Call RegistCandidate(m_clsRegistHead.TailList.Element, clsBaseLineVector)
                Else
                    '電子基準点間でも、両端が固定点なら、反対側も候補とする。
                    If m_clsRegistHead.NextList.Element.TopParentPoint.Fixed Then Call RegistCandidate(m_clsRegistHead.TailList.Element, clsBaseLineVector)
                End If
                '完了トリガー。
                If Not Ring Then
                    '両端が固定点なら点間。
                    If m_clsRegistHead.NextList.Element.TopParentPoint.Fixed Then
                        m_bCompleted = True
                        RaiseEvent CompletedAngle(m_clsRegistHead.NextList)
                    End If
                End If
            Else
                '候補オブジェクトか？
                Dim clsChainList As ChainList
                Set clsChainList = m_clsCandidateHead.NextList
                Do While Not clsChainList Is Nothing
                    If clsChainList.Element Is clsBaseLineVector Then Exit Do
                    Set clsChainList = clsChainList.NextList
                Loop
                '候補オブジェクトでなければ何もしない。
                If clsChainList Is Nothing Then Exit Function
                'スタート観測点。
                Dim clsObservationPoint As ObservationPoint
                Set clsObservationPoint = clsChainList.PrevList.Element '一つ前が観測測点。
                If m_clsStartPoint Is Nothing Then
                    '登録オブジェクトの順番が決定したので登録しなおす。
                    Call m_clsRegistHead.RemoveAll
                    '根元の観測点。候補の反対側。
                    Dim clsBasePoint As ObservationPoint
                    If clsObservationPoint.RootPoint Is clsBaseLineVector.StrPoint.RootPoint Then
                        Set clsBasePoint = clsBaseLineVector.EndPoint
                    ElseIf clsObservationPoint.RootPoint Is clsBaseLineVector.EndPoint.RootPoint Then
                        Set clsBasePoint = clsBaseLineVector.StrPoint
                    ElseIf GetDistanceRound(clsObservationPoint.CoordinateAngleDiff, clsBaseLineVector.StrPoint.CoordinateAngleDiff) <= m_nConnectDistance Then
                        Set clsBasePoint = clsBaseLineVector.EndPoint
                    Else
                        Set clsBasePoint = clsBaseLineVector.StrPoint
                    End If
                    'スタート観測点を設定。
                    Dim clsStrPoint As ObservationPoint
                    Dim clsEndPoint As ObservationPoint
                    Set clsStrPoint = m_clsStartVector.StrPoint
                    Set clsEndPoint = m_clsStartVector.EndPoint
                    If clsStrPoint.RootPoint Is clsBasePoint.RootPoint Then
                        Set m_clsStartPoint = clsEndPoint
                        Call m_clsRegistHead.InsertNext(m_clsStartPoint)
                        Call m_clsRegistHead.InsertNext(m_clsStartVector)
                        Call m_clsRegistHead.InsertNext(clsStrPoint) '観測点を先頭に登録。
                    ElseIf clsEndPoint.RootPoint Is clsBasePoint.RootPoint Then
                        Set m_clsStartPoint = clsStrPoint
                        Call m_clsRegistHead.InsertNext(m_clsStartPoint)
                        Call m_clsRegistHead.InsertNext(m_clsStartVector)
                        Call m_clsRegistHead.InsertNext(clsEndPoint) '観測点を先頭に登録。
                    ElseIf GetDistanceRound(clsStrPoint.CoordinateAngleDiff, clsBasePoint.CoordinateAngleDiff) <= m_nConnectDistance Then
                        Set m_clsStartPoint = clsEndPoint
                        Call m_clsRegistHead.InsertNext(m_clsStartPoint)
                        Call m_clsRegistHead.InsertNext(m_clsStartVector)
                        Call m_clsRegistHead.InsertNext(clsStrPoint) '観測点を先頭に登録。
                    Else
                        Set m_clsStartPoint = clsStrPoint
                        Call m_clsRegistHead.InsertNext(m_clsStartPoint)
                        Call m_clsRegistHead.InsertNext(m_clsStartVector)
                        Call m_clsRegistHead.InsertNext(clsEndPoint) '観測点を先頭に登録。
                    End If
                End If
                '登録基線ベクトルコレクションへ追加。
                Call SetAtCollectionObject(m_clsRegistVectors, clsBaseLineVector, Hex$(GetPointer(clsBaseLineVector)))
                '登録オブジェクトに追加する。
                Call m_clsRegistHead.InsertNext(clsBaseLineVector)
                Call m_clsRegistHead.InsertNext(clsObservationPoint) '観測点を先頭に登録。
                '候補オブジェクトの登録。
                Call m_clsCandidateHead.RemoveAll
                Call RegistCandidate(clsObservationPoint, clsBaseLineVector)
                '完了トリガー。
                If Ring Then
                    'スタート観測点と一致したら環閉合。
                    If IsRing(clsObservationPoint, clsBaseLineVector) Then
                        m_bCompleted = True
                        RaiseEvent CompletedAngle(m_clsRegistHead.NextList)
                    End If
                Else
                    '固定点なら点間。
                    If clsObservationPoint.TopParentPoint.Fixed Then
                        m_bCompleted = True
                        RaiseEvent CompletedAngle(m_clsRegistHead.NextList)
                    End If
                End If
            End If


            Click = True


        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        'クリック処理。
        '
        '指定された基線ベクトルを閉合に登録する。
        '
        '引き数：
        'clsBaseLineVector クリックされた基線ベクトル。
        '
        '戻り値：
        '指定された基線ベクトルが登録された場合 True を返す。
        'それ以外の場合 False を返す。
        */
        public bool Click(BaseLineVector clsBaseLineVector)
        {
            return false;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '閉合バック処理。
        '
        '閉合に最後に登録された基線ベクトルを登録から削除する。
        Public Sub Back()

            m_bCompleted = False


            '候補オブジェクトのクリア。
            Call m_clsCandidateHead.RemoveAll


            If m_clsStartVector Is Nothing Then
                'まだ何も登録されていない。
                '候補オブジェクトの登録。
                Call RegistCandidate(Nothing, Nothing)
            ElseIf m_clsStartPoint Is Nothing Then
                'スタート基線ベクトルしか登録されていない。
                Set m_clsStartVector = Nothing
                Set m_clsRegistVectors = Nothing
                Call m_clsRegistHead.RemoveAll
                '候補オブジェクトの登録。
                Call RegistCandidate(Nothing, Nothing)
            Else
                Call RemoveAtCollection(m_clsRegistVectors, Hex$(GetPointer(m_clsRegistHead.NextList.NextList.Element)))
                Call m_clsRegistHead.RemoveNext
                Call m_clsRegistHead.RemoveNext
                'スタート基線ベクトルだけになった？
                If m_clsRegistHead.FollowingCount <= 3 Then Set m_clsStartPoint = Nothing


                Dim clsBaseLineVector As BaseLineVector
                Set clsBaseLineVector = m_clsRegistHead.NextList.NextList.Element


                '候補オブジェクトの登録。
                Call RegistCandidate(m_clsRegistHead.NextList.Element, clsBaseLineVector)
                '初回？。
                If m_clsStartPoint Is Nothing Then
                    If Ring Then
                        '環閉合なら反対側も候補とする。
                        Call RegistCandidate(m_clsRegistHead.TailList.Element, clsBaseLineVector)
                    Else
                        '電子基準点間でも、両端が固定点なら、反対側も候補とする。
                        If m_clsRegistHead.NextList.Element.TopParentPoint.Fixed Then Call RegistCandidate(m_clsRegistHead.TailList.Element, clsBaseLineVector)
                    End If
                End If


                '完了トリガー。
                If Ring Then
                    'スタート観測点と一致したら環閉合。
                    If IsRing(m_clsRegistHead.NextList.Element, clsBaseLineVector) Then
                        m_bCompleted = True
                        RaiseEvent CompletedAngle(m_clsRegistHead.NextList)
                    End If
                Else
                    '固定点なら点間。
                    If m_clsRegistHead.NextList.Element.TopParentPoint.Fixed Then
                        m_bCompleted = True
                        RaiseEvent CompletedAngle(m_clsRegistHead.NextList)
                    End If
                End If
            End If


        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '閉合バック処理。
        '
        '閉合に最後に登録された基線ベクトルを登録から削除する。
        */
        public void Back()
        {
            return;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '登録基線ベクトルを取得する。
        '
        '戻り値：登録されている基線ベクトルのキーの配列を返す。配列の要素は(-1 To ...)、要素 -1 は未使用。
        Public Function GetRegistVectors() As String()

            Dim nUBound As Long
            If m_clsRegistVectors Is Nothing Then
                nUBound = -1
            Else
                nUBound = m_clsRegistVectors.Count - 1
            End If


            Dim sRegistVectors() As String
            ReDim sRegistVectors(-1 To nUBound)
            Dim nIndex As Long
            nIndex = nUBound
            Dim clsChainList As ChainList
            Set clsChainList = m_clsRegistHead.NextList
            Do While Not clsChainList Is Nothing
                If(clsChainList.Element.ObjectType And OBJ_TYPE_BASELINEVECTOR) <> 0 Then
                    sRegistVectors(nIndex) = clsChainList.Element.Key
                    nIndex = nIndex - 1
                End If
                Set clsChainList = clsChainList.NextList
            Loop


            GetRegistVectors = sRegistVectors


        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '登録基線ベクトルを取得する。
        '
        '戻り値：登録されている基線ベクトルのキーの配列を返す。配列の要素は(-1 To ...)、要素 -1 は未使用。
        */
        public string[] GetRegistVectors()
        {
            return null;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '基線ベクトルを登録する。
        '
        '指定された基線ベクトルを閉合に登録する。
        '
        '引き数：
        'sRegistVectors 登録する基線ベクトルのキーの配列。参照する配列の要素は(0 To ...)。Ubound 関数を使用する。
        '
        '戻り値：
        '指定された基線ベクトルがすべて登録された場合 True を返す。
        'それ以外の場合 False を返す。
        Public Function SetRegistVectors(ByRef sRegistVectors() As String) As Boolean

            SetRegistVectors = False


            '基線ベクトルコレクション。
            Dim objBaseLineVectors As New Collection
            Dim clsChainList As ChainList
            Set clsChainList = GetDocument().NetworkModel.BaseLineVectorHead
            Do While Not clsChainList Is Nothing
                Call objBaseLineVectors.Add(clsChainList.Element, clsChainList.Element.Key)
                Set clsChainList = clsChainList.NextList
            Loop


            '登録する基線ベクトルのリスト。
            Dim clsBaseLineVectors() As BaseLineVector
            ReDim clsBaseLineVectors(-1 To UBound(sRegistVectors))
            Dim i As Long
            For i = 0 To UBound(sRegistVectors)
                If Not LookupCollectionObject(objBaseLineVectors, clsBaseLineVectors(i), sRegistVectors(i)) Then Exit Function
            Next


            '登録。
            For i = 0 To UBound(clsBaseLineVectors)
                If Not Click(clsBaseLineVectors(i)) Then Exit Function
            Next


            SetRegistVectors = True


        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '基線ベクトルを登録する。
        '
        '指定された基線ベクトルを閉合に登録する。
        '
        '引き数：
        'sRegistVectors 登録する基線ベクトルのキーの配列。参照する配列の要素は(0 To ...)。Ubound 関数を使用する。
        '
        '戻り値：
        '指定された基線ベクトルがすべて登録された場合 True を返す。
        'それ以外の場合 False を返す。
        */
        public bool SetRegistVectors(string[] sRegistVectors)
        {
            return false;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '*******************************************************************************
        'インプリメンテーション

        '候補オブジェクトの登録。
        '
        'clsObservationPoint と clsBaseLineVector から候補となるオブジェクトを決定し登録する。
        '
        '引き数：
        'clsObservationPoint 現在の終点となっている観測点(接合観測点)。
        'clsBaseLineVector 現時点で最後に登録された基線ベクトル。
        Private Sub RegistCandidate(ByVal clsObservationPoint As ObservationPoint, ByVal clsBaseLineVector As BaseLineVector)

            If clsObservationPoint Is Nothing Then
                '最初の候補として有効な基線ベクトルを登録。
                Dim clsChainList As ChainList
                Set clsChainList = GetDocument().NetworkModel.BaseLineVectorHead
                Do While Not clsChainList Is Nothing
                    If IsFirstCandidate(clsChainList.Element) Then
                        '候補オブジェクトに追加。
                        Call m_clsCandidateHead.InsertNext(clsChainList.Element)
                    End If
                    Set clsChainList = clsChainList.NextList
                Loop
            Else
                '接続基線ベクトル。
                Dim clsBaseLineVectors() As BaseLineVector
                ReDim clsBaseLineVectors(-1 To - 1)
                Call GetDocument().NetworkModel.GetConnectBaseLineVectorsEx(clsObservationPoint, clsBaseLineVectors)


                If m_nConnectDistance > 0 Then
                    '接続していなくても、距離が近ければ繋がっているとみなす。
                    Dim clsCoordinatePoint As CoordinatePoint
                    If clsBaseLineVector.StrPoint.RootPoint Is clsObservationPoint.RootPoint Then
                        Set clsCoordinatePoint = clsBaseLineVector.StrPoint.CoordinateAngleDiff
                    Else
                        Set clsCoordinatePoint = clsBaseLineVector.EndPoint.CoordinateAngleDiff
                    End If
                    Set clsChainList = GetDocument().NetworkModel.RepresentPointHead
                    Do While Not clsChainList Is Nothing
                        Dim clsNearPoint As ObservationPoint
                        Set clsNearPoint = clsChainList.Element
                        If Not clsNearPoint.RootPoint Is clsObservationPoint.RootPoint Then
                            If GetDistanceRound(clsCoordinatePoint, clsNearPoint.CoordinateAngleDiff) <= m_nConnectDistance Then
                                Call GetDocument().NetworkModel.GetConnectBaseLineVectors(clsNearPoint, clsBaseLineVectors)
                            End If
                        End If
                        Set clsChainList = clsChainList.NextList
                    Loop
                End If


                '候補オブジェクトの登録。
                Dim clsCandidatePoint As ObservationPoint
                Dim i As Long
                For i = 0 To UBound(clsBaseLineVectors)
                    '候補として無効な基線ベクトルは無視。
                    If Not clsBaseLineVectors(i).Candidate Then GoTo ContinueHandler
                    '接続観測点。
                    If clsBaseLineVectors(i).StrPoint.RootPoint Is clsObservationPoint.RootPoint Then
                        Set clsCandidatePoint = clsBaseLineVectors(i).EndPoint
                    ElseIf clsBaseLineVectors(i).EndPoint.RootPoint Is clsObservationPoint.RootPoint Then
                        Set clsCandidatePoint = clsBaseLineVectors(i).StrPoint
                    ElseIf GetDistanceRound(clsBaseLineVectors(i).StrPoint.CoordinateAngleDiff, clsObservationPoint.CoordinateAngleDiff) <= m_nConnectDistance Then
                        Set clsCandidatePoint = clsBaseLineVectors(i).EndPoint
                    Else
                        Set clsCandidatePoint = clsBaseLineVectors(i).StrPoint
                    End If
                    '既に登録されている場合、追加しない。
                    Dim clsRegistVector As BaseLineVector
                    If LookupCollectionObject(m_clsRegistVectors, clsRegistVector, Hex$(GetPointer(clsBaseLineVectors(i)))) Then GoTo ContinueHandler
                    '既に候補になっている場合、追加しない。
                    Set clsChainList = m_clsCandidateHead.NextList
                    Do While Not clsChainList Is Nothing
                        If clsChainList.Element Is clsBaseLineVectors(i) Then Exit Do
                        Set clsChainList = clsChainList.NextList
                    Loop
                    If Not clsChainList Is Nothing Then GoTo ContinueHandler
                    '候補オブジェクトに追加。
                    Call m_clsCandidateHead.InsertNext(clsBaseLineVectors(i))
                    Call m_clsCandidateHead.InsertNext(clsCandidatePoint) '観測点を先頭に登録。
        ContinueHandler:
                Next
            End If


        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '*******************************************************************************
        'インプリメンテーション
        
        '候補オブジェクトの登録。
        '
        'clsObservationPoint と clsBaseLineVector から候補となるオブジェクトを決定し登録する。
        '
        '引き数：
        'clsObservationPoint 現在の終点となっている観測点(接合観測点)。
        'clsBaseLineVector 現時点で最後に登録された基線ベクトル。
        */
        private void RegistCandidate(ObservationPoint clsObservationPoint, BaseLineVector clsBaseLineVector)
        {
            return;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '指定された基線ベクトルが最初の候補として有効か検査する。
        '
        '引き数：
        'clsBaseLineVector 基線ベクトル。
        '
        '戻り値：
        '候補として有効であれば True を返す。
        'それ以外の場合 False を返す。
        Public Function IsFirstCandidate(ByVal clsBaseLineVector As BaseLineVector) As Boolean

            IsFirstCandidate = False


            '候補として無効な基線ベクトルは無視。
            If Not clsBaseLineVector.Candidate Then Exit Function


            '電子基準点間閉合の場合、固定点につながっていなければ却下。
            If Not Ring Then
                If Not(clsBaseLineVector.StrPoint.TopParentPoint.Fixed Or clsBaseLineVector.EndPoint.TopParentPoint.Fixed) Then Exit Function
            End If


            IsFirstCandidate = True


        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '指定された基線ベクトルが最初の候補として有効か検査する。
        '
        '引き数：
        'clsBaseLineVector 基線ベクトル。
        '
        '戻り値：
        '候補として有効であれば True を返す。
        'それ以外の場合 False を返す。
        */
        public bool IsFirstCandidate(BaseLineVector clsBaseLineVector)
        {
            return false;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '環になっているか検査する。
        '
        '現在登録されているオブジェクトで環閉合が成立しているか検査する。
        '
        '引き数：
        'clsObservationPoint 現在の終点となっている観測点(接合観測点)。
        'clsBaseLineVector 現時点で最後に登録された基線ベクトル。
        '
        '戻り値：
        '環閉合が成立している場合 True を返す。
        'それ以外の場合 False を返す。
        Private Function IsRing(ByVal clsObservationPoint As ObservationPoint, ByVal clsBaseLineVector As BaseLineVector) As Boolean

            IsRing = True


            If m_clsStartPoint Is Nothing Then Exit Function
            If clsObservationPoint Is Nothing Then Exit Function
            If clsObservationPoint.RootPoint Is m_clsStartPoint.RootPoint Then Exit Function


            If m_nConnectDistance > 0 And Not m_clsStartVector Is Nothing And Not m_clsStartPoint Is Nothing Then
                '接続していなくても、距離が近ければ繋がっているとみなす。
                Dim clsCoordStr As CoordinatePoint
                If m_clsStartVector.StrPoint.RootPoint Is m_clsStartPoint.RootPoint Then
                    Set clsCoordStr = m_clsStartVector.StrPoint.CoordinateAngleDiff
                Else
                    Set clsCoordStr = m_clsStartVector.EndPoint.CoordinateAngleDiff
                End If
                Dim clsCoordEnd As CoordinatePoint
                If clsBaseLineVector.StrPoint.RootPoint Is clsObservationPoint.RootPoint Then
                    Set clsCoordEnd = clsBaseLineVector.StrPoint.CoordinateAngleDiff
                Else
                    Set clsCoordEnd = clsBaseLineVector.EndPoint.CoordinateAngleDiff
                End If
                If GetDistanceRound(clsCoordStr, clsCoordEnd) <= m_nConnectDistance Then Exit Function
            End If


            IsRing = False


        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '環になっているか検査する。
        '
        '現在登録されているオブジェクトで環閉合が成立しているか検査する。
        '
        '引き数：
        'clsObservationPoint 現在の終点となっている観測点(接合観測点)。
        'clsBaseLineVector 現時点で最後に登録された基線ベクトル。
        '
        '戻り値：
        '環閉合が成立している場合 True を返す。
        'それ以外の場合 False を返す。
        */
        private bool IsRing(ObservationPoint clsObservationPoint, BaseLineVector clsBaseLineVector)
        {
            return false;
        }
        //==========================================================================================
    }
}
