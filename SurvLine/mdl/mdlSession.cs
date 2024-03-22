using System;
using System.Collections.Generic;
using System.Net;
using System.Windows.Forms;
using static SurvLine.mdl.DEFINE;
using static SurvLine.mdl.MdiVBfunctions;
using static SurvLine.mdl.MdlGUI;
using static SurvLine.mdl.MdlListPane;
using static SurvLine.mdl.MdlNSDefine;
using static SurvLine.mdl.MdlUtility;


namespace SurvLine.mdl
{
    public class MdlSession
    {
        //==========================================================================================
        /*[VB]
        '*******************************************************************************
        'セッション
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '定数
        Private Const MSG_SESSION_ERROR As String = "セッション名には4文字の半角英数字を指定してください。"
        Private Const MSG_OVERLAP_ERROR As String = "セッション名が重複します。"
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'定数
        private const string MSG_SESSION_ERROR = "セッション名には4文字の半角英数字を指定してください。";
        private const string MSG_OVERLAP_ERROR = "セッション名が重複します。";
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        Option Explicit
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //==========================================================================================

        public long SelectLine;     //ListPane選択位置  //2



        private MdlMain m_clsMdlMain;                                   //MdlMainインスタンス
        private MdlListPane m_clsMdlListPane;

        //public MdlSession(MdlMain clsMdlMain, MdlListPane clsMdlListPane)
        public MdlSession(MdlMain clsMdlMain)
        {
            m_clsMdlMain = clsMdlMain;
            //  m_clsMdlListPane = clsMdlListPane;
            return;
        }


        //==========================================================================================
        //ListPane選択位置を設定       //2
        public void SetSelectLine(long nSelectLine)    //2
        {
            SelectLine = nSelectLine;

        }
        //ListPane選択位置を取得        //2
        public long GetSelectLine()    //2
        {
            return SelectLine;
        }



        //==========================================================================================
        /*[VB]
        'セッションリストの作成。
        '
        '引き数：
        'cmbSession リストを作成するコンボボックス。
        Public Sub MakeSessionList(ByVal cmbSession As ComboBox)

            '観測点のリストアップ。
            Dim objSessions As New Collection
            Dim objElement As Object
            Dim clsChainList As ChainList
            Set clsChainList = GetDocument().NetworkModel.RepresentPointHead
            Do While Not clsChainList Is Nothing
                If Not clsChainList.Element.Genuine Then
                    If Not LookupCollectionObject(objSessions, objElement, clsChainList.Element.Session) Then
                        Call objSessions.Add(clsChainList.Element, clsChainList.Element.Session)
                        Call cmbSession.AddItem(clsChainList.Element.Session)
                    End If
                End If
                Set clsChainList = clsChainList.NextList
            Loop
    
            '基線ベクトルのリストアップ。
            Set clsChainList = GetDocument().NetworkModel.BaseLineVectorHead
            Do While Not clsChainList Is Nothing
                If Not LookupCollectionObject(objSessions, objElement, clsChainList.Element.Session) Then
                    Call objSessions.Add(clsChainList.Element, clsChainList.Element.Session)
                    Call cmbSession.AddItem(clsChainList.Element.Session)
                End If
                Set clsChainList = clsChainList.NextList
            Loop

        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]  //2
        public void MakeSessionList(ComboBox cmbSession)    //2
        {

            //'観測点のリストアップ。
            long nRows;
            nRows = 0;

            try
            {
                /*----------------------------------------------------------------------------------------
                    '観測点のリストアップ。
                    Dim objSessions As New Collection
                    Dim objElement As Object
                    Dim clsChainList As ChainList
                    Set clsChainList = GetDocument().NetworkModel.RepresentPointHead
                    Do While Not clsChainList Is Nothing
                        If Not clsChainList.Element.Genuine Then
                            If Not LookupCollectionObject(objSessions, objElement, clsChainList.Element.Session) Then
                                Call objSessions.Add(clsChainList.Element, clsChainList.Element.Session)
                                Call cmbSession.AddItem(clsChainList.Element.Session)
                            End If
                        End If
                        Set clsChainList = clsChainList.NextList
                    Loop
                */
                cmbSession.Items.Clear();

                Dictionary<string, object> objSessions = new Dictionary<string, object>();
                object objElement = new object();
                ChainList clsChainList;
                clsChainList = m_clsMdlMain.GetDocument().NetworkModel().RepresentPointHead();

                while (clsChainList != null)
                {
                    ObservationPoint clsObservationPoint = (ObservationPoint)clsChainList.Element;

                    if (!clsObservationPoint.Genuine())
                    {
                        if (!LookupCollectionObject(objSessions, ref objElement, clsObservationPoint.Session()))
                        {
                            objSessions.Add(clsObservationPoint.Session(), clsChainList.Element);
                            int v = cmbSession.Items.Add(clsObservationPoint.Session());
                        }

                    }

                    nRows++;
                    clsChainList = clsChainList.NextList();
                }
                /*----------------------------------------------------------------------------------------
                    '基線ベクトルのリストアップ。
                    Set clsChainList = GetDocument().NetworkModel.BaseLineVectorHead
                    Do While Not clsChainList Is Nothing
                        If Not LookupCollectionObject(objSessions, objElement, clsChainList.Element.Session) Then
                            Call objSessions.Add(clsChainList.Element, clsChainList.Element.Session)
                            Call cmbSession.AddItem(clsChainList.Element.Session)
                        End If
                        Set clsChainList = clsChainList.NextList
                    Loop
                 */
                clsChainList = m_clsMdlMain.GetDocument().NetworkModel().BaseLineVectorHead();

                while (clsChainList != null)
                {
                    BaseLineVector clsBaseLineVector = (BaseLineVector)clsChainList.Element;

                    if (!LookupCollectionObject(objSessions, ref objElement, clsBaseLineVector.Session))
                    {
                        objSessions.Add(clsBaseLineVector.Session, clsChainList.Element);
                        int v = cmbSession.Items.Add(clsBaseLineVector.Session);
                    }

                    nRows++;
                    clsChainList = clsChainList.NextList();
                }

            }
            catch (Exception ex)
            {
                _ = MessageBox.Show(ex.Message, "エラー発生", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }


        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '入力値を検査する。
        '
        'コンボボックスに入力されたセッション名が objElements で指定されたオブジェクトのセッション名として許可されるか評価する。
        '
        '引き数：
        'cmbSession 対象とするコンボボックス。
        'objElements 対象とするオブジェクト。要素はオブジェクト。キーは任意。
        'bFocus 検査に引っかかった場合フォーカスを移すか？
        '
        '戻り値：
        '入力値が正常である場合 True を返す。
        'それ以外の場合 False を返す。
        Public Function CheckSessionInput(ByVal cmbSession As ComboBox, ByRef objElements As Collection, Optional ByVal bFocus As Boolean = True) As Boolean

            CheckSessionInput = False
    
            '大文字。
            cmbSession.Text = StrConv(cmbSession.Text, vbUpperCase)
    
            '文字数検査。
            If Not CheckLength(cmbSession.Text, 4, 4) Then
                Call MsgBox(MSG_SESSION_ERROR, vbCritical)
                If bFocus Then Call cmbSession.SetFocus
                Exit Function
            End If
    
            '文字検査。
            Dim nCode() As Byte
            Dim i As Long
            'ASCIIコード。
            nCode = StrConv(cmbSession.Text, vbFromUnicode)
            '１文字ずつ検査。
            For i = 0 To UBound(nCode)
                '0～9、A～Z。
                If(nCode(i) < &H30& Or &H39& < nCode(i)) And(nCode(i) < &H41& Or &H5A& < nCode(i)) Then Exit For
            Next
            If i <= UBound(nCode) Then
                Call MsgBox(MSG_SESSION_ERROR, vbCritical)
                If bFocus Then Call cmbSession.SetFocus
                Exit Function
            End If
    
            '重複の検査。
            If Not CheckSessionOverlap(cmbSession.Text, objElements, MSG_OVERLAP_ERROR) Then
                If bFocus Then Call cmbSession.SetFocus
                Exit Function
            End If


            CheckSessionInput = True


        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        public bool CheckSessionInput(ComboBox cmbSession, object objElements, bool bFocus)
        {
            bool CheckSessionInput = false;

            //'大文字。
            cmbSession.Text = cmbSession.Text.ToUpper();

            //'文字数検査。
            if (!CheckLength(cmbSession.Text, 4, 4))
            {
                //Call MsgBox(MSG_SESSION_ERROR, vbCritical)
                _ = MessageBox.Show(MSG_SESSION_ERROR, "エラー発生", MessageBoxButtons.OK, MessageBoxIcon.Error);

                if (bFocus)
                {
                    _ = cmbSession.Focus();
                    return CheckSessionInput;
                }

            }

            //'文字検査。
            byte[] nCode = new byte[4];
            long i;
            //'ASCIIコード。
            nCode = StrConv(cmbSession.Text, vbFromUnicode);
            //'１文字ずつ検査。
            for (i = 0; i < nCode.Length; i++)
            {
                //'0～9、A～Z。
                //If(nCode(i) < &H30 & Or & H39 & < nCode(i)) And(nCode(i) < &H41 & Or & H5A & < nCode(i)) Then Exit For
                if ((nCode[i] < 0x30 || 0x39 < nCode[i]) && (nCode[i] < 0x41 || 0x5A < nCode[i]))
                {
                    return CheckSessionInput;
                }
            }
            if (i < nCode.Length)
            {
                _ = MessageBox.Show(MSG_SESSION_ERROR, "エラー発生", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (bFocus)
                {
                    _ = cmbSession.Focus();
                    return CheckSessionInput;
                }
            }

            //'重複の検査。
            if (!CheckSessionOverlap(cmbSession.Text, objElements, MSG_OVERLAP_ERROR))
            {
                if (bFocus)
                {
                    _ = cmbSession.Focus();
                    return CheckSessionInput;
                }

            }

            CheckSessionInput = true;
            return CheckSessionInput;

        }


        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '拡張の検査。
        '
        'sSession で指定されるセッション名を objElements で指定されるオブジェクトに設定する時に、objElements に関連するオブジェクトにも sSession を設定するか確認する。
        '関連するオブジェクトとは、
        'objElements が基線ベクトルであった場合はその両端の観測点(代表観測点)。
        'objElements が観測点(代表観測点)であった場合はその接続基線ベクトル。
        'それらのオブジェクトに sSession が設定可能か検査する。
        '
        '引き数：
        'sSession セッション名。
        'objElements 対象とするオブジェクト。要素はオブジェクト。キーは任意。
        'bExtend 拡張フラグが設定される。True=関連するオブジェクトにも設定する。False=関連するオブジェクトまでは設定しない。
        '
        '戻り値：
        '問題が無い場合 True を返す。
        'それ以外の場合 False を返す。
        Public Function CheckSessionExtend(ByVal sSession As String, ByVal objElements As Collection, ByRef bExtend As Boolean) As Boolean

            CheckSessionExtend = False
    
            '確認。
            If Not ConfirmSessionExtend(sSession, objElements, bExtend) Then Exit Function
            If Not bExtend Then
                CheckSessionExtend = True
                Exit Function
            End If
    
            '拡張設定対象オブジェクト。
            Dim objExtends As New Collection
            Set objExtends = GetSessionExtend(objElements)
    
            '同一観測点名称の検査。
            If Not CheckSessionObsName(objExtends, "同じ観測点Noの観測点を同じセッションに変更することは出来ません｡ ", "同じ観測点Noを始点終点に持つ基線ベクトルを同じセッションに変更することは出来ません｡ ") Then Exit Function
    
            'セッション名重複の検査。
            If Not CheckSessionOverlap(sSession, objExtends, MSG_OVERLAP_ERROR) Then Exit Function

            CheckSessionExtend = True


        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        public bool CheckSessionExtend(string sSession, object objElements, ref bool bExtend)
        {

            bool CheckSessionExtend = false;

            //'確認。
            if (!ConfirmSessionExtend(sSession, objElements, ref bExtend))
            {
                return CheckSessionExtend;
            }
            else
            {
                if (!bExtend)
                {
                    CheckSessionExtend = true;
                    return CheckSessionExtend;
                }
            }


#if false   //ここは、重要では無い為、後で対応（瀬戸口）


            //'拡張設定対象オブジェクト。
            Collection objExtends = new Collection();
            //  objExtends = GetSessionExtend(objElements);


            //'同一観測点名称の検査。
            //If Not CheckSessionObsName(objExtends, "同じ観測点Noの観測点を同じセッションに変更することは出来ません｡ ", "同じ観測点Noを始点終点に持つ基線ベクトルを同じセッションに変更することは出来ません｡ ") Then Exit Function


            //'セッション名重複の検査。
            //  If Not CheckSessionOverlap(sSession, objExtends, MSG_OVERLAP_ERROR) Then Exit Function

#endif

            CheckSessionExtend = true;
            return CheckSessionExtend;



        }

        //==========================================================================================

        //==========================================================================================
        /*[VB]
            '拡張設定対象オブジェクトを取得する。
            '
            'objElements に関連するオブジェクトを取得する。
            '関連するオブジェクトとは、
            'objElements が基線ベクトルであった場合はその両端の観測点(代表観測点)。
            'objElements が観測点(代表観測点)であった場合はその接続基線ベクトル。
            '
            '引き数：
            'objElements 対象とするオブジェクト。要素はオブジェクト。キーは任意。
            '
            '戻り値：拡張設定対象オブジェクトを返す。要素はオブジェクト。キーは要素のポインタ。
            Public Function GetSessionExtend(ByVal objElements As Collection) As Collection
                Dim objExtends As New Collection
                If(objElements.Item(1).ObjectType And OBJ_TYPE_OBSERVATIONPOINT) <> 0 Then
                    Dim clsObservationPoint As ObservationPoint
                    For Each clsObservationPoint In objElements
                        Dim clsBaseLineVectors() As BaseLineVector
                        ReDim clsBaseLineVectors(-1 To -1)
                        Call GetDocument().NetworkModel.GetConnectBaseLineVectors(clsObservationPoint, clsBaseLineVectors)
                        Dim i As Long
                        For i = 0 To UBound(clsBaseLineVectors)
                            Call SetAtCollectionObject(objExtends, clsBaseLineVectors(i), Hex$(GetPointer(clsBaseLineVectors(i))))
                        Next
                    Next
                Else
                    Dim clsBaseLineVector As BaseLineVector
                    For Each clsBaseLineVector In objElements
                        Call SetAtCollectionObject(objExtends, clsBaseLineVector.StrPoint.TopParentPoint, Hex$(GetPointer(clsBaseLineVector.StrPoint.TopParentPoint)))
                        Call SetAtCollectionObject(objExtends, clsBaseLineVector.EndPoint.TopParentPoint, Hex$(GetPointer(clsBaseLineVector.EndPoint.TopParentPoint)))
                    Next
                End If
                Set GetSessionExtend = objExtends
            End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]

        public  Dictionary<string, object> GetSessionExtend(object objElements)
        {
            /*
            */
            return null;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
            'セッション名重複の検査。
            '
            'sSession で指定されるセッション名を objElements で指定されるオブジェクトに設定した場合、セッション名の重複が起こるか検査する。
            '重複が起こる場合は sMessage で指定されたメッセージで警告する。
            '
            '引き数：
            'sSession セッション名。
            'objElements 対象とするオブジェクト。要素はオブジェクト。キーは任意。
            'sMessage メッセージ。
            '
            '戻り値：
            '重複しない場合 True を返す。
            'それ以外の場合 False を返す。
            Private Function CheckSessionOverlap(ByVal sSession As String, ByVal objElements As Collection, ByVal sMessage As String) As Boolean

                CheckSessionOverlap = False
    
                '重複の検査。
                Dim objElement As Object
                Set objElement = objElements.Item(1)
                If(objElement.ObjectType And OBJ_TYPE_OBSERVATIONPOINT) <> 0 Then
                    Dim clsObservationPoint As ObservationPoint
                    For Each clsObservationPoint In objElements
                        Dim clsRepresentPoint As ObservationPoint
                        Set clsRepresentPoint = clsObservationPoint.HeadPoint
                        Do While Not clsRepresentPoint Is Nothing
                            If Not clsRepresentPoint Is clsObservationPoint.TopParentPoint Then
                                If clsRepresentPoint.Session = sSession Then
                                    Call MsgBox(sMessage, vbCritical)
                                    Exit Function
                                End If
                            End If
                            Set clsRepresentPoint = clsRepresentPoint.NextPoint
                        Loop
                    Next
                Else
                    Dim objCollection As New Collection
                    Dim clsChainList As ChainList
                    Set clsChainList = GetDocument().NetworkModel.BaseLineVectorHead
                    '既存のキー。
                    Do While Not clsChainList Is Nothing
                        Call objCollection.Add(clsChainList.Element, clsChainList.Element.Key)
                        Set clsChainList = clsChainList.NextList
                    Loop
                    Dim clsBaseLineVector As BaseLineVector
                    'これから変更するキーは除外。
                    For Each clsBaseLineVector In objElements
                        Call objCollection.Remove(clsBaseLineVector.Key)
                    Next
                    '重複検査。
                    For Each clsBaseLineVector In objElements
                        Dim sKey As String
                        sKey = GetBaseLineVectorKey(clsBaseLineVector.StrPoint.Number, clsBaseLineVector.EndPoint.Number, sSession)
                        If LookupCollectionObject(objCollection, objElement, sKey) Then
                            Call MsgBox(sMessage, vbCritical)
                            Exit Function
                        Else
                            Call objCollection.Add(clsBaseLineVector, sKey)
                        End If
                    Next
                End If


                CheckSessionOverlap = True


            End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        private bool CheckSessionOverlap(string sSession, object objElements, string sMessage)
        {
            bool CheckSessionOverlap = false;

#if false
        //ここは、重要では無い為、後で対応（瀬戸口）
#endif
            CheckSessionOverlap = true;
            return CheckSessionOverlap;

        }



        //==========================================================================================

        //==========================================================================================
        /*[VB]
            '同一観測点名称の検査。
            '
            'objElements で指定されたオブジェクトの観測点番号が同一であるか検査する。
            'objElements が観測点である場合、その観測点同士の観測点番号が同じであるか検査する。
            'objElements が基線ベクトルである場合、その始点終点の観測点番号が同じであるか検査する。
            '観測点番号が同じである時、sMessageObsPnt もしくは sMessageVector で指定されるメッセージを表示する。
            '
            '引き数：
            'objElements 対象とするオブジェクト。要素はオブジェクト。キーは任意。
            'sMessageObsPnt objElements が観測点であった場合のメッセージ。
            'sMessageVector objElements が基線ベクトルであった場合のメッセージ。
            '
            '戻り値：
            '観測点番号が重複しない場合は True を返す。
            '同じ観測点番号がある場合は False を返す。
            Public Function CheckSessionObsName(ByVal objElements As Collection, ByVal sMessageObsPnt As String, ByVal sMessageVector As String) As Boolean

                CheckSessionObsName = False
    
                '同じ観測点番号が選択されていたら許可しない。
                Dim objElement As Object
                Dim objNumbers As New Collection
                If(objElements.Item(1).ObjectType And OBJ_TYPE_OBSERVATIONPOINT) <> 0 Then
                    For Each objElement In objElements
                        If LookupCollectionObject(objNumbers, objElement, objElement.Number) Then
                            Call MsgBox(sMessageObsPnt, vbCritical)
                            Exit Function
                        Else
                            Call objNumbers.Add(objElement, objElement.Number)
                        End If
                    Next
                Else
                    Dim clsBaseLineVector As BaseLineVector
                    For Each clsBaseLineVector In objElements
                        Dim sNumber As String
                        If clsBaseLineVector.StrPoint.Number<clsBaseLineVector.EndPoint.Number Then
                            sNumber = clsBaseLineVector.StrPoint.Number & "\" & clsBaseLineVector.EndPoint.Number
                        Else
                            sNumber = clsBaseLineVector.EndPoint.Number & "\" & clsBaseLineVector.StrPoint.Number
                        End If
                        If LookupCollectionObject(objNumbers, clsBaseLineVector, sNumber) Then
                            Call MsgBox(sMessageVector, vbCritical)
                            Exit Function
                        Else
                            Call objNumbers.Add(clsBaseLineVector, sNumber)
                        End If
                    Next
                End If


                CheckSessionObsName = True


            End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]  //2
        /// <summary>
        /// 同一観測点名称の検査。
        /// '
        /// objElements で指定されたオブジェクトの観測点番号が同一であるか検査する。
        /// objElements が観測点である場合、その観測点同士の観測点番号が同じであるか検査する。
        /// objElements が基線ベクトルである場合、その始点終点の観測点番号が同じであるか検査する。
        /// 観測点番号が同じである時、sMessageObsPnt もしくは sMessageVector で指定されるメッセージを表示する。
        /// '
        /// 引き数：
        /// objElements 対象とするオブジェクト。要素はオブジェクト。キーは任意。
        /// sMessageObsPnt objElements が観測点であった場合のメッセージ。
        /// sMessageVector objElements が基線ベクトルであった場合のメッセージ。
        /// 
        /// </summary>
        /// <param name="objElements"></param>
        /// <param name="sMessageObsPnt"></param>
        /// <param name="sMessageVector"></param>
        /// <returns>
        /// 戻り値：
        ///  観測点番号が重複しない場合は True を返す。
        ///  同じ観測点番号がある場合は False を返す。
        /// </returns>
        public static bool CheckSessionObsName(object objElements, string sMessageObsPnt, string sMessageVector)
        {
            bool CheckSessionObsName = false;

#if false
        //ここは、重要では無い為、後で対応（瀬戸口）
#endif


            CheckSessionObsName = true;
            return CheckSessionObsName;

        }



        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '拡張の確認。
        '
        '引き数：
        'sSession セッション名。
        'objElements 対象とするオブジェクト。要素はオブジェクト。キーは任意。
        'bExtend 拡張フラグが設定される。True=関連するオブジェクトにも設定する。False=関連するオブジェクトまでは設定しない。
        '
        '戻り値：
        '正常終了の場合 True を返す。
        'キャンセルの場合 False を返す。
        Private Function ConfirmSessionExtend(ByVal sSession As String, ByVal objElements As Collection, ByRef bExtend As Boolean) As Boolean

            ConfirmSessionExtend = False
            bExtend = False


            Dim objElement As Object
            Set objElement = objElements.Item(1)
            If(objElement.ObjectType And OBJ_TYPE_OBSERVATIONPOINT) <> 0 Then
                Dim clsObservationPoint As ObservationPoint
                For Each clsObservationPoint In objElements
                    Dim clsBaseLineVectors() As BaseLineVector
                    ReDim clsBaseLineVectors(-1 To -1)
                    Call GetDocument().NetworkModel.GetConnectBaseLineVectors(clsObservationPoint, clsBaseLineVectors)
                    Dim i As Long
                    For i = 0 To UBound(clsBaseLineVectors)
                        If clsBaseLineVectors(i).Session<> sSession Then
                            Select Case MsgBox("選択されている観測点を始点、または終点とする基線ベクトルのセッション名も変更しますか?", vbExclamation Or vbYesNoCancel)
                            Case vbYes
                                bExtend = True
                            Case vbCancel
                                Exit Function
                            End Select
                            ConfirmSessionExtend = True
                            Exit Function
                        End If
                    Next
                Next
            Else
                Dim clsBaseLineVector As BaseLineVector
                For Each clsBaseLineVector In objElements
                    If clsBaseLineVector.StrPoint.Session<> sSession Or clsBaseLineVector.EndPoint.Session<> sSession Then
                        Select Case MsgBox("選択されている基線ベクトルの始点と終点のセッション名も変更しますか?", vbExclamation Or vbYesNoCancel)
                        Case vbYes
                            bExtend = True
                        Case vbCancel
                            Exit Function
                        End Select
                        ConfirmSessionExtend = True
                        Exit Function
                    End If
                Next
            End If

            ConfirmSessionExtend = True

        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]  //2
        /// <summary>
        /// 拡張の確認。
        /// '
        /// 引き数：
        /// sSession セッション名。
        /// objElements 対象とするオブジェクト。要素はオブジェクト。キーは任意。
        /// bExtend 拡張フラグが設定される。True=関連するオブジェクトにも設定する。False=関連するオブジェクトまでは設定しない。
        /// 
        /// </summary>
        /// <param name="sSession"></param>
        /// <param name="objElements"></param>
        /// <param name="bExtend"></param>
        /// <returns>
        /// 戻り値：
        ///  正常終了の場合 True を返す。
        ///  キャンセルの場合 False を返す。
        /// </returns>
        private bool ConfirmSessionExtend(string sSession, object objElements, ref bool bExtend)
        {
            bool ConfirmSessionExtend = false;
            bExtend = false;


            long nRows = 0;

            ChainList clsChainList;

            //===========================================================================
            if (m_clsMdlMain.PaneSelectedTab == (long)LIST_NUM_PANE.LIST_NUM_OBSPNT)
            //===========================================================================
            {
                //TAB：観測点の選択の場合

                clsChainList = m_clsMdlMain.GetDocument().NetworkModel().RepresentPointHead();
                ObservationPoint clsObservationPoint;
                while (clsChainList != null)
                {
                    clsObservationPoint = (ObservationPoint)clsChainList.Element;
                    //------------------------------------------------------------------------------
                    if (nRows == m_clsMdlMain.PaneSelcttedNo)
                    {
                        if ((clsObservationPoint.ObjectType & OBJ_TYPE_OBSERVATIONPOINT) != 0)
                        {
                            List<BaseLineVector> clsBaseLineVectors = new List<BaseLineVector>();

                            //  m_clsMdlMain.GetDocument().NetworkModel().GetConnectBaseLineVectors(clsObservationPoint, ref clsBaseLineVectors);

                            Sessoin_GetConnectBaseLineVectors(clsObservationPoint, ref clsBaseLineVectors);

                            //=====================================================
                            for (int i = 0; i < clsBaseLineVectors.Count; i++)
                            {
                                if (clsBaseLineVectors[i].Session != sSession)
                                {
                                    DialogResult msg = MessageBox.Show("選択されている観測点を始点、または終点とする基線ベクトルのセッション名も変更しますか?", "確認", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                                    switch (msg)
                                    {
                                        case DialogResult.Yes:
                                            bExtend = true;
                                            ConfirmSessionExtend = true;
                                            return ConfirmSessionExtend;
                                        case DialogResult.No:
                                            ConfirmSessionExtend = true;
                                            return ConfirmSessionExtend;
                                        case DialogResult.Cancel:
                                            return ConfirmSessionExtend;
                                        default:
                                            return ConfirmSessionExtend;
                                    }
                                }
                            }//for (int i = 0; i < clsBaseLineVectors.Length; i++)
                             //=====================================================
                        }//if ((clsObservationPoint.ObjectType & OBJ_TYPE_OBSERVATIONPOINT) != 0)
                    }//if (nRows == m_clsMdlMain.PaneSelcttedNo)
                    //------------------------------------------------------------------------------
                    nRows++;
                    clsChainList = clsChainList.NextList();

                }//while (clsChainList != null)
            }
            //===========================================================================
            else  //if (m_clsMdlMain.PaneSelectedTab != (long)LIST_NUM_PANE.LIST_NUM_OBSPNT)
            //===========================================================================
            {

                //TAB：ベクトル選択の場合

                clsChainList = m_clsMdlMain.GetDocument().NetworkModel().BaseLineVectorHead();
                BaseLineVector clsBaseLineVector;

                while (clsChainList != null)
                {
                    clsBaseLineVector = (BaseLineVector)clsChainList.Element;

                    //[VB]  If clsBaseLineVector.StrPoint.Session<> sSession Or clsBaseLineVector.EndPoint.Session<> sSession Then
                    if (clsBaseLineVector.StrPoint().Session() != sSession || clsBaseLineVector.EndPoint().Session() != sSession)
                    {
                        DialogResult msg = MessageBox.Show("選択されている基線ベクトルの始点と終点のセッション名も変更しますか?", "確認", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                        switch (msg)
                        {
                            case DialogResult.Yes:
                                bExtend = true;
                                ConfirmSessionExtend = true;
                                return ConfirmSessionExtend;
                            case DialogResult.No:
                                ConfirmSessionExtend = true;
                                return ConfirmSessionExtend;
                            case DialogResult.Cancel:
                                return ConfirmSessionExtend;
                            default:
                                return ConfirmSessionExtend;
                        }

                    }

                    clsChainList = clsChainList.NextList();


                }//while (clsChainList != null)

                //===========================================================================
            }//if (m_clsMdlMain.PaneSelectedTab == (long)LIST_NUM_PANE.LIST_NUM_OBSPNT)
            //===========================================================================

            ConfirmSessionExtend = true;
            return ConfirmSessionExtend;


        }

        //---------------------------------------------------------------------------
        //新規    //2
        /// <summary>
        /// 観測点に接続する、ベクトルの開始点／終了点のベクトル情報を取得する
        /// 
        /// </summary>
        /// <param name="clsObservationPoint"></param>
        /// <param name="clsBaseLineVectors"></param>
        public void Sessoin_GetConnectBaseLineVectors(ObservationPoint clsObservationPoint, ref List<BaseLineVector> clsBaseLineVectors)   //2)
        {
            ChainList clsChainList;
            clsChainList = m_clsMdlMain.GetDocument().NetworkModel().BaseLineVectorHead();
            BaseLineVector dataBaseLineVector;

            string sCheck = clsObservationPoint.DispNumber();

            while (clsChainList != null)
            {
                dataBaseLineVector = (BaseLineVector)clsChainList.Element;

                if (dataBaseLineVector.StrPoint().TopParentPoint().Number() == sCheck || dataBaseLineVector.EndPoint().TopParentPoint().Number() == sCheck)
                {
                    clsBaseLineVectors.Add(dataBaseLineVector);
                }

                clsChainList = clsChainList.NextList();
            }

        }


        //==========================================================================================
        //==========================================================================================
        //==========================================================================================
    }
}
