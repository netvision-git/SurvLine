using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SurvLine.mdl.MdlUtility;
using static SurvLine.Dispersion;

namespace SurvLine.mdl
{
    internal class MdlOutputFile
    {
        //==========================================================================================
        /*[VB]
        '*******************************************************************************
        '外部ファイル出力

        Option Explicit
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '少数点以下桁数。NVFの制限が最も大きくなければいけない。
        Public Const OUTPUT_DECIMAL_XYZ As Long = 12 '座標。
        Public Const OUTPUT_DECIMAL_VECTOR As Long = 10 'ベクトル。
        Public Const OUTPUT_DECIMAL_DISPERSION As Long = 18 '分散・共分散。
        Public Const OUTPUT_DECIMAL_ANTHEIGHT As Long = 10 'アンテナ高。
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'少数点以下桁数。NVFの制限が最も大きくなければいけない。
#if false
        public const long OUTPUT_DECIMAL_XYZ = 12;          //'座標。
        public const long OUTPUT_DECIMAL_VECTOR = 10;       //'ベクトル。
        public const long OUTPUT_DECIMAL_DISPERSION = 18;   //'分散・共分散。
        public const long OUTPUT_DECIMAL_ANTHEIGHT = 10;    //'アンテナ高。
#else
        public const long OUTPUT_DECIMAL_XYZ = 12;          //'座標。
        public const long OUTPUT_DECIMAL_VECTOR = 10;       //'ベクトル。
        public const long OUTPUT_DECIMAL_DISPERSION = 15;   //'分散・共分散。
        public const long OUTPUT_DECIMAL_ANTHEIGHT = 10;    //'アンテナ高。
#endif
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '出力するオブジェクトがあるか検査する。
        '
        '指定されたパラメータの条件を満たすオブジェクトがあるか評価する。
        '
        '引き数：
        'clsOutputParam 外部出力ファイル出力パラメータ。
        'clsBaseLineVectorHead 基線ベクトルリストのヘッダー。要素は BaseLineVector オブジェクト。
        '
        '戻り値：
        '出力する要素がある場合 True を返す。
        'それ以外の場合 False を返す。
        Public Function IsGenerateOutput(ByVal clsOutputParam As OutputParam, ByVal clsBaseLineVectorHead As ChainList) As Boolean
            '基線ベクトルリスト。
            Dim clsBaseLineVectors() As BaseLineVector
            clsBaseLineVectors = GetAccountBaseLineVectors(clsBaseLineVectorHead, clsOutputParam.AccountParam, New BaseLineVectorSessionSorter)
            '要素数。
            IsGenerateOutput = UBound(clsBaseLineVectors) >= 0
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '外部ファイルを出力する。
        '
        '引き数：
        'clsOutputParam 外部出力ファイル出力パラメータ。
        'clsDocument ドキュメントオブジェクト。
        'clsOutputInterface 外部ファイル出力インターフェース。
        'clsProgressInterface ProgressInterface オブジェクト。
        Public Sub GenerateOutputFile(ByVal clsOutputParam As OutputParam, ByVal clsDocument As Document, ByVal clsOutputInterface As OutputInterface, ByVal clsProgressInterface As ProgressInterface)

            '基線ベクトルリスト。
            Dim clsBaseLineVectors() As BaseLineVector
            clsBaseLineVectors = GetAccountBaseLineVectors(clsDocument.NetworkModel.BaseLineVectorHead, clsOutputParam.AccountParam, New BaseLineVectorSessionSorter)
    
            '観測点を重複して出力しないように出力済みの観測点を記録する。
            Dim objObservationPoints As New Collection
    
            Dim clsFile As FileNumber
            Set clsFile = clsOutputInterface.FileOpen(clsOutputParam.Path)
    
            'ヘッダー。
            Call clsOutputInterface.WriteHeader(clsFile.Number, clsDocument)
    
            '地心直交で入力された固定座標は出力しない。
            Dim bXYZ As Boolean
            Dim bOld As Boolean
    
            Dim i As Long
            For i = 0 To UBound(clsBaseLineVectors)
                '基線ベクトル情報の出力。
                Call clsOutputInterface.WriteVector(clsFile.Number, clsBaseLineVectors(i))
        
                '固定点の出力。
                Dim clsObservationPoint As ObservationPoint
                Dim objItem As Object
                Set clsObservationPoint = clsBaseLineVectors(i).StrPoint.RootPoint
                If clsObservationPoint.Fixed Then
                    If (clsObservationPoint.CoordinateFixed.EditCode And EDITCODE_COORD_XYZ) <> 0 Then
                        '地心直交で入力された固定座標は出力しない。
                        bXYZ = True
                    ElseIf clsObservationPoint.CoordinateFixed.EditCode = 0 Then
                        '旧バージョンのアプリで入力された固定座標は出力しない。
                        bOld = True
                    Else
                        If Not LookupCollectionObject(objObservationPoints, objItem, Hex$(GetPointer(clsObservationPoint))) Then
                            Call clsOutputInterface.WriteFixed(clsFile.Number, clsObservationPoint, IIf(clsDocument.GeoidoEnable, clsDocument.GeoidoPath, ""))
                            Call objObservationPoints.Add(clsObservationPoint, Hex$(GetPointer(clsObservationPoint)))
                        End If
                    End If
                End If
                Set clsObservationPoint = clsBaseLineVectors(i).EndPoint.RootPoint
                If clsObservationPoint.Fixed Then
                    If (clsObservationPoint.CoordinateFixed.EditCode And EDITCODE_COORD_XYZ) <> 0 Then
                        '地心直交で入力された固定座標は出力しない。
                        bXYZ = True
                    ElseIf clsObservationPoint.CoordinateFixed.EditCode = 0 Then
                        '旧バージョンのアプリで入力された固定座標は出力しない。
                        bOld = True
                    Else
                        If Not LookupCollectionObject(objObservationPoints, objItem, Hex$(GetPointer(clsObservationPoint))) Then
                            Call clsOutputInterface.WriteFixed(clsFile.Number, clsObservationPoint, IIf(clsDocument.GeoidoEnable, clsDocument.GeoidoPath, ""))
                            Call objObservationPoints.Add(clsObservationPoint, Hex$(GetPointer(clsObservationPoint)))
                        End If
                    End If
                End If
        
                'プログレス。
                Call clsProgressInterface.CheckCancel
            Next
    
            '点検計算結果データの出力。'2016/01/12 Hitz H.Nakamura
            Call clsOutputInterface.WriteCheckResult(clsFile.Number, clsDocument)
            'プログレス。
            Call clsProgressInterface.CheckCancel
    
            'フッター。
            Call clsOutputInterface.WriteFooter(clsFile.Number, Nothing)
    
            '警告メッセージ。
            Call clsOutputInterface.Exclamation(bXYZ, bOld)
    
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '丸めフィルタ。
        '
        '外部ファイルの出力精度に合わせて値を丸める。
        '分散･共分散を丸める。
        '
        '引き数：
        'clsDispersion Dispersion オブジェクト。
        '
        '戻り値：丸めた結果。
        Public Function FilterDispersion(ByVal clsDispersion As Dispersion) As Dispersion
            Dim clsRound As New Dispersion
            clsRound.XX = JpnRound(clsDispersion.XX, OUTPUT_DECIMAL_DISPERSION)
            clsRound.XY = JpnRound(clsDispersion.XY, OUTPUT_DECIMAL_DISPERSION)
            clsRound.XZ = JpnRound(clsDispersion.XZ, OUTPUT_DECIMAL_DISPERSION)
            clsRound.YY = JpnRound(clsDispersion.YY, OUTPUT_DECIMAL_DISPERSION)
            clsRound.YZ = JpnRound(clsDispersion.YZ, OUTPUT_DECIMAL_DISPERSION)
            clsRound.ZZ = JpnRound(clsDispersion.ZZ, OUTPUT_DECIMAL_DISPERSION)
            Set FilterDispersion = clsRound
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '丸めフィルタ。
        '
        '外部ファイルの出力精度に合わせて値を丸める。
        '分散･共分散を丸める。
        '
        '引き数：
        'clsDispersion Dispersion オブジェクト。
        '
        '戻り値：丸めた結果。
        */
        public static Dispersion FilterDispersion(Dispersion clsDispersion)
        {
            Dispersion clsRound = new Dispersion();
            clsRound.XX = JpnRound(clsDispersion.Dispersion_XX(), OUTPUT_DECIMAL_DISPERSION);
            clsRound.XY = JpnRound(clsDispersion.XY, OUTPUT_DECIMAL_DISPERSION);
            clsRound.XZ = JpnRound(clsDispersion.XZ, OUTPUT_DECIMAL_DISPERSION);
            clsRound.YY = JpnRound(clsDispersion.YY, OUTPUT_DECIMAL_DISPERSION);
            clsRound.YZ = JpnRound(clsDispersion.YZ, OUTPUT_DECIMAL_DISPERSION);
            clsRound.ZZ = JpnRound(clsDispersion.ZZ, OUTPUT_DECIMAL_DISPERSION);
            return clsRound;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '丸めフィルタ。
        '
        '外部ファイルの出力精度に合わせて値を丸める。
        'アンテナ高を丸める。
        '
        '引き数：
        'nAntHeight アンテナ高。
        '
        '戻り値：丸めた結果。
        Public Function FilterAntHeight(ByVal nAntHeight As Double) As Double
            FilterAntHeight = JpnRound(nAntHeight, OUTPUT_DECIMAL_ANTHEIGHT)
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //==========================================================================================
    }
}
