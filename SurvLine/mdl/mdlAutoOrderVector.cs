using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
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
using static SurvLine.mdl.MdlMain;
using static SurvLine.mdl.MdlGUI;
using static SurvLine.mdl.MdlNSUtility;
using System.Collections;

namespace SurvLine.mdl
{
    internal class MdlAutoOrderVector
    {

        //5==========================================================================================
        /*[VB]
            '*******************************************************************************
            '基線ベクトルの向きの自動整列。

            Option Explicit

            '整列手段。
            Public Enum AUTOORDERVECTOR_PROCTYPE
                AUTOORDERVECTOR_PROCTYPE_ONE '固定点。
                AUTOORDERVECTOR_PROCTYPE_all '全ての固定点。
            End Enum
          [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#] //5
        /// <summary>
        /// 整列手段
        /// </summary>
        public enum AUTOORDERVECTOR_PROCTYPE
        {
            AUTOORDERVECTOR_PROCTYPE_ONE,       //'固定点。
            AUTOORDERVECTOR_PROCTYPE_all,       //'全ての固定点。
        }


        //------------------------------------------------------------------------------------------
        //[C#] //5
        private MdlMain m_clsMdlMain;   //5
        public MdlAutoOrderVector(MdlMain mdlMain)
        {
            //  this.mdlMain = mdlMain;
            m_clsMdlMain = mdlMain;     //5
        }


        //5==========================================================================================
        /*[VB]
            '基線ベクトルの向きの自動整列。
            '
            'ObservationPoint と BaseLineVector の WorkKey を使用する。
            '
            '引き数：
            'clsNetworkModel ネットワークモデル。
            'clsAutoOrderVectorParam 基線ベクトルの向きの自動整列パラメータ。
            Public Sub Order(ByVal clsNetworkModel As NetworkModel, ByVal clsAutoOrderVectorParam As AutoOrderVectorParam)

                '方向を設定したらWorkKeyに印をつける。
                Call clsNetworkModel.ClearWorkKey(0)
    
                '出発点。
                Dim objNexts As New Collection
                If clsAutoOrderVectorParam.ProcType = AUTOORDERVECTOR_PROCTYPE_ONE Then
                    Dim clsChainList As ChainList
                    Set clsChainList = clsNetworkModel.RepresentPointHead
                    Do While Not clsChainList Is Nothing
                        Dim clsObservationPoint As ObservationPoint
                        Set clsObservationPoint = clsChainList.Element
                        If clsObservationPoint.Number = clsAutoOrderVectorParam.Fixed Then
                            Call objNexts.Add(clsObservationPoint.HeadPoint)
                            Exit Do
                        End If
                        Set clsChainList = clsChainList.NextList
                    Loop
                Else
                    Set clsChainList = clsNetworkModel.RepresentPointHead
                    Do While Not clsChainList Is Nothing
                        Set clsObservationPoint = clsChainList.Element
                        If clsObservationPoint.Fixed And Not clsObservationPoint.Genuine And clsObservationPoint.PrevPoint Is Nothing Then
                            Call objNexts.Add(clsObservationPoint)
                        End If
                        Set clsChainList = clsChainList.NextList
                    Loop
                End If
    
                '整列。全ての基線ベクトルを評価するまで。
                Do While objNexts.Count > 0
                    Dim objCurrents As Collection
                    Set objCurrents = objNexts
                    Set objNexts = New Collection
                    For Each clsObservationPoint In objCurrents
                        '評価済みの場合はスキップする。
                        If clsObservationPoint.WorkKey = 0 Then
                            '評価済みの印。
                            clsObservationPoint.WorkKey = 1
                            '観測点に繋がっている基線ベクトル。
                            Dim clsBaseLineVectors() As BaseLineVector
                            ReDim clsBaseLineVectors(-1 To -1)
                            Call clsNetworkModel.GetConnectBaseLineVectorsEx(clsObservationPoint, clsBaseLineVectors)
                            '基線ベクトルを評価。
                            Dim i As Long
                            For i = 0 To UBound(clsBaseLineVectors)
                                '評価済みの場合はスキップする。
                                If clsBaseLineVectors(i).WorkKey = 0 Then
                                    '評価済みの印。
                                    clsBaseLineVectors(i).WorkKey = 1
                                    '向きの評価。
                                    If Not clsBaseLineVectors(i).Enable Then
                                        '無効な基線ベクトルは除外。
                                    ElseIf clsBaseLineVectors(i).StrPoint.HeadPoint.WorkKey = 0 Then
                                        '進行方向の観測点。
                                        Dim clsBuddyPoint As ObservationPoint
                                        Set clsBuddyPoint = clsBaseLineVectors(i).StrPoint.HeadPoint
                                        '反転。
                                        Call clsNetworkModel.ReplaceBaseLineVector(clsBaseLineVectors(i))
                                        '次の観測点を登録。
                                        Call SetAtCollectionObject(objNexts, clsBuddyPoint, Hex$(GetPointer(clsBuddyPoint)))
                                    ElseIf clsBaseLineVectors(i).EndPoint.HeadPoint.WorkKey = 0 Then
                                        '進行方向の観測点。
                                        Set clsBuddyPoint = clsBaseLineVectors(i).EndPoint.HeadPoint
                                        '次の観測点を登録。
                                        Call SetAtCollectionObject(objNexts, clsBuddyPoint, Hex$(GetPointer(clsBuddyPoint)))
                                    Else
                                        '行き止まり。
                                    End If
                                End If
                            Next
                        End If
                    Next
                Loop
    
                '評価されていない基線ベクトルはあるか？
                Set clsChainList = clsNetworkModel.BaseLineVectorHead
                Do While Not clsChainList Is Nothing
                    Dim clsBaseLineVector As BaseLineVector
                    Set clsBaseLineVector = clsChainList.Element
                    If clsBaseLineVector.WorkKey = 0 Then
                        Dim sText As String
                        If clsAutoOrderVectorParam.ProcType = AUTOORDERVECTOR_PROCTYPE_ONE Then
                            sText = "指定された固定点とつながりの無い基線ベクトルがあります。これらの基線ベクトルは整列されません。"
                        Else
                            sText = "固定点とつながりの無い基線ベクトルがあります。これらの基線ベクトルは整列されません。"
                        End If
                        Call MsgBox(sText, vbExclamation)
                        Exit Do
                    End If
                    Set clsChainList = clsChainList.NextList
                Loop
    
            End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#] //5
        /// <summary>
        /// 基線ベクトルの向きの自動整列。
        /// '
        /// ObservationPoint と BaseLineVector の WorkKey を使用する。
        /// '
        /// 引き数：
        /// clsNetworkModel ネットワークモデル。
        /// clsAutoOrderVectorParam 基線ベクトルの向きの自動整列パラメータ。
        /// 
        /// </summary>
        /// <param name="clsNetworkModel"></param>
        /// <param name="clsAutoOrderVectorParam"></param>
        public void Order(NetworkModel clsNetworkModel, AutoOrderVectorParam clsAutoOrderVectorParam)
        {
            /*-------------------------------------------------
                '方向を設定したらWorkKeyに印をつける。
                Call clsNetworkModel.ClearWorkKey(0)
             */
            clsNetworkModel.ClearWorkKey(0);


            /*------------------------------------------------------------
                '出発点。
                Dim objNexts As New Collection
                If clsAutoOrderVectorParam.ProcType = AUTOORDERVECTOR_PROCTYPE_ONE Then
                    Dim clsChainList As ChainList
                    Set clsChainList = clsNetworkModel.RepresentPointHead
                    Do While Not clsChainList Is Nothing
                        Dim clsObservationPoint As ObservationPoint
                        Set clsObservationPoint = clsChainList.Element
                        If clsObservationPoint.Number = clsAutoOrderVectorParam.Fixed Then
                            Call objNexts.Add(clsObservationPoint.HeadPoint)
                            Exit Do
                        End If
                        Set clsChainList = clsChainList.NextList
                    Loop
                Else
                    Set clsChainList = clsNetworkModel.RepresentPointHead
                    Do While Not clsChainList Is Nothing
                        Set clsObservationPoint = clsChainList.Element
                        If clsObservationPoint.Fixed And Not clsObservationPoint.Genuine And clsObservationPoint.PrevPoint Is Nothing Then
                            Call objNexts.Add(clsObservationPoint)
                        End If
                        Set clsChainList = clsChainList.NextList
                    Loop
                End If
    
             */
            //*******
            //出発点。
            //*******
            Dictionary<string, object> objNexts = new Dictionary<string, object>();
            if (clsAutoOrderVectorParam.ProcType == AUTOORDERVECTOR_PROCTYPE.AUTOORDERVECTOR_PROCTYPE_ONE)
            {
                ChainList clsChainList;
                clsChainList = clsNetworkModel.RepresentPointHead();
                while (clsChainList != null)
                {
                    ObservationPoint clsObservationPoint;
                    clsObservationPoint = (ObservationPoint)clsChainList.Element;
                    if (clsObservationPoint.Number() == clsAutoOrderVectorParam.Fixed)
                    {
                        objNexts.Add(GetPointer(clsObservationPoint).ToString(), clsObservationPoint.HeadPoint());
                        break;
                    }
                    //
                    clsChainList = clsChainList.NextList();
                    //
                }//while (clsChainList != null)
                //========================================================
            }
            else  //if (clsAutoOrderVectorParam.ProcType == AUTOORDERVECTOR_PROCTYPE.AUTOORDERVECTOR_PROCTYPE_ONE)
            {
                ChainList clsChainList;
                clsChainList = clsNetworkModel.RepresentPointHead();
                while (clsChainList != null)
                {
                    ObservationPoint clsObservationPoint;
                    clsObservationPoint = (ObservationPoint)clsChainList.Element;
                    if (clsObservationPoint.Fixed() && !clsObservationPoint.Genuine() && clsObservationPoint.PrevPoint() == null)
                    {
                        objNexts.Add(GetPointer(clsObservationPoint).ToString(), clsObservationPoint);
                    }
                    //
                    clsChainList = clsChainList.NextList();
                    //
                }
            }     //if (clsAutoOrderVectorParam.ProcType == AUTOORDERVECTOR_PROCTYPE.AUTOORDERVECTOR_PROCTYPE_ONE)


            /*--------------------------------------------------------------------------------------------------------
                '整列。全ての基線ベクトルを評価するまで。
                Do While objNexts.Count > 0
                    Dim objCurrents As Collection
                    Set objCurrents = objNexts
                    Set objNexts = New Collection
                    For Each clsObservationPoint In objCurrents
                        '評価済みの場合はスキップする。
                        If clsObservationPoint.WorkKey = 0 Then
                            '評価済みの印。
                            clsObservationPoint.WorkKey = 1
                            '観測点に繋がっている基線ベクトル。
                            Dim clsBaseLineVectors() As BaseLineVector
                            ReDim clsBaseLineVectors(-1 To -1)
                            Call clsNetworkModel.GetConnectBaseLineVectorsEx(clsObservationPoint, clsBaseLineVectors)
                            '基線ベクトルを評価。
                            Dim i As Long
                            For i = 0 To UBound(clsBaseLineVectors)
                                '評価済みの場合はスキップする。
                                If clsBaseLineVectors(i).WorkKey = 0 Then
                                    '評価済みの印。
                                    clsBaseLineVectors(i).WorkKey = 1
                                    '向きの評価。
                                    If Not clsBaseLineVectors(i).Enable Then
                                        '無効な基線ベクトルは除外。
                                    ElseIf clsBaseLineVectors(i).StrPoint.HeadPoint.WorkKey = 0 Then
                                        '進行方向の観測点。
                                        Dim clsBuddyPoint As ObservationPoint
                                        Set clsBuddyPoint = clsBaseLineVectors(i).StrPoint.HeadPoint
                                        '反転。
                                        Call clsNetworkModel.ReplaceBaseLineVector(clsBaseLineVectors(i))
                                        '次の観測点を登録。
                                        Call SetAtCollectionObject(objNexts, clsBuddyPoint, Hex$(GetPointer(clsBuddyPoint)))
                                    ElseIf clsBaseLineVectors(i).EndPoint.HeadPoint.WorkKey = 0 Then
                                        '進行方向の観測点。
                                        Set clsBuddyPoint = clsBaseLineVectors(i).EndPoint.HeadPoint
                                        '次の観測点を登録。
                                        Call SetAtCollectionObject(objNexts, clsBuddyPoint, Hex$(GetPointer(clsBuddyPoint)))
                                    Else
                                        '行き止まり。
                                    End If
                                End If
                            Next
                        End If
                    Next
                Loop
             */
            //'整列。全ての基線ベクトルを評価するまで。
            while (objNexts.Count > 0)
            {
                //*****************************************************
                ObservationPoint clsObservationPoint;   // = new Dictionary<string, object>();
                Dictionary<string, object> objCurrents = objNexts;
                //[VB]  Set objNexts = New Collection
                objNexts = new Dictionary<string, object>();

                foreach (ObservationPoint obj in objCurrents.Values.Cast<ObservationPoint>())
                {
                    clsObservationPoint = (ObservationPoint)obj;

                    //'評価済みの場合はスキップする。
                    if (clsObservationPoint.WorkKey == 0)
                    {
                        //評価済みの印。
                        clsObservationPoint.WorkKey = 1;
                        //'観測点に繋がっている基線ベクトル。
                        List<BaseLineVector> clsBaseLineVectors = new List<BaseLineVector>();

                        //**************************************************************************
                        //ReDim clsBaseLineVectors(-1 To - 1)
                        //  clsNetworkModel.GetConnectBaseLineVectorsEx(clsObservationPoint, clsBaseLineVectors);
                        //  MdlMain m_clsMdlMain = new MdlMain();
                        m_clsMdlMain.GetDocument().Session_GetConnectBaseLineVectors(clsObservationPoint, ref clsBaseLineVectors);
                        //**************************************************************************

                        //基線ベクトルを評価。
                        int i;
                        for (i = 0; i < clsBaseLineVectors.Count; i++)
                        {
                            //評価済みの場合はスキップする。
                            if (clsBaseLineVectors[i].WorkKey == 0)
                            {
                                //評価済みの印。
                                clsBaseLineVectors[i].WorkKey = 1;
                                //向きの評価。
                                if (!clsBaseLineVectors[i].Enable())
                                {
                                    //'無効な基線ベクトルは除外。
                                }
                                else if (clsBaseLineVectors[i].StrPoint().HeadPoint().WorkKey == 0)
                                {

                                    //'進行方向の観測点。
                                    ObservationPoint clsBuddyPoint;
                                    clsBuddyPoint = clsBaseLineVectors[i].StrPoint().HeadPoint();
                                    //'反転。
                                    clsNetworkModel.ReplaceBaseLineVector(clsBaseLineVectors[i]);
                                    //'次の観測点を登録。
                                    SetAtCollectionObject(objNexts, clsBuddyPoint, GetPointer(clsBuddyPoint).ToString());

                                }
                                else if (clsBaseLineVectors[i].EndPoint().HeadPoint().WorkKey == 0)
                                {
                                    //'進行方向の観測点。
                                    ObservationPoint clsBuddyPoint;
                                    clsBuddyPoint = clsBaseLineVectors[i].EndPoint().HeadPoint();
                                    //'次の観測点を登録。
                                    SetAtCollectionObject(objNexts, clsBuddyPoint, GetPointer(clsBuddyPoint).ToString());
                                }
                                else
                                {
                                    //行き止まり。

                                }//if (!clsBaseLineVectors[i].Enable())

                            }//if (clsBaseLineVectors[i].WorkKey == 0)

                        }// for (i = 0; i < clsBaseLineVectors.Count; i++)

                    }//if ( clsObservationPoint.WorkKey == 0)
                }   //foreach (object obj in objCurrents)
                //*****************************************************
            }//Loop //while (objNexts.Count > 0)


            /*-------------------------------------------------------------------------------------------------------
                '評価されていない基線ベクトルはあるか？
                Set clsChainList = clsNetworkModel.BaseLineVectorHead
                Do While Not clsChainList Is Nothing
                    Dim clsBaseLineVector As BaseLineVector
                    Set clsBaseLineVector = clsChainList.Element
                    If clsBaseLineVector.WorkKey = 0 Then
                        Dim sText As String
                        If clsAutoOrderVectorParam.ProcType = AUTOORDERVECTOR_PROCTYPE_ONE Then
                            sText = "指定された固定点とつながりの無い基線ベクトルがあります。これらの基線ベクトルは整列されません。"
                        Else
                            sText = "固定点とつながりの無い基線ベクトルがあります。これらの基線ベクトルは整列されません。"
                        End If
                        Call MsgBox(sText, vbExclamation)
                        Exit Do
                    End If
                    Set clsChainList = clsChainList.NextList
                Loop
             */



        }


        //===========================================================
        //===========================================================
    }
}
