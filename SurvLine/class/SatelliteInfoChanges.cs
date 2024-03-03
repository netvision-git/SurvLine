using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using System.Windows.Forms;

namespace SurvLine
{
    internal class SatelliteInfoChanges
    {



        //'*******************************************************************************
        //'衛星情報遷移状態
        //
        //  Option Explicit


        //==========================================================================================
        /*[VB]
            Private Type State '状態。
                TimeGPS As Date '時刻(GPS)。
                Numbers() As Long '衛星番号。見えている衛星の番号のリスト。配列の要素は(-1 To ...)、要素 -1 は未使用。
            End Type

            'インプリメンテーション
            Private m_tStates() As State '状態。配列の要素は(-1 To ...)、要素 -1 は未使用。

            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        public struct State
        {
            DateTime TimeGPS;   //'時刻(GPS)。
            List<long> Numbers; //'衛星番号。見えている衛星の番号のリスト。配列の要素は(-1 To ...)、要素 -1 は未使用。
        }

        //'インプリメンテーション
        List<State> m_tStates;  //'状態。配列の要素は(-1 To ...)、要素 -1 は未使用。


        //'*******************************************************************************
        //'イベント

        public SatelliteInfoChanges()
        {
            Class_Initialize();
        }


        //==========================================================================================
        /*[VB]
            '初期化。
            Private Sub Class_Initialize()

                On Error GoTo ErrorHandler


                ReDim m_tStates(-1 To -1)


                Exit Sub


            ErrorHandler:
                Call mdlMain.ErrorExit


            End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        private void Class_Initialize()
        {
            try
            {
                //  On Error GoTo ErrorHandler

                m_tStates.Clear();

            }
            catch (Exception ex)
            {
                //ErrorHandler:
                //    Call mdlMain.ErrorExit
                _ = MessageBox.Show(ex.Message, "エラー発生", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

#if false

'*******************************************************************************
'プロパティ

'状態数。
Property Get Count() As Long
    Count = UBound(m_tStates) + 1
End Property

'時刻(GPS)。
Property Get TimeGPS(ByVal nIndex As Long) As Date
    TimeGPS = m_tStates(nIndex).TimeGPS
End Property

'衛星番号。配列の要素は(-1 To ...)、要素 -1 は未使用。
Property Get Numbers(ByVal nIndex As Long) As Long()
    Numbers = m_tStates(nIndex).Numbers
End Property

'*******************************************************************************
'メソッド

'状態を評価して必要なら追加する。
'
'引き数：
'tTimeGPS 時刻(GPS)。
'bPrevNumbers 前回見えていた衛星。配列インデックスが衛星番号に対応する。True=見えていた。False=見えていなかった。参照する配列の要素は(0 To PPS_MAXPRN_CAPA)。
'bNextNumbers 今回見えている衛星。配列インデックスが衛星番号に対応する。True=見えている。False=見えていない。参照する配列の要素は(0 To PPS_MAXPRN_CAPA)。
Public Sub AddEvaluation(ByVal tTimeGPS As Date, ByRef bPrevNumbers() As Boolean, ByRef bNextNumbers() As Boolean)

    Dim nUBound As Long
    nUBound = UBound(m_tStates)


    Dim nCommonNumbers() As Long
    nCommonNumbers = GetCommonNumbers(bPrevNumbers, bNextNumbers)


    If nUBound< 0 Then
        '初回なら無条件に追加。
        Call Add(tTimeGPS, nCommonNumbers)
    Else
        If UBound(m_tStates(nUBound).Numbers) <> UBound(nCommonNumbers) Then
            '衛星数が異なれば追加。
            Call Add(tTimeGPS, nCommonNumbers)
        Else
            Dim i As Long
            For i = 0 To UBound(m_tStates(nUBound).Numbers)
                '番号が異なれば追加。
                If m_tStates(nUBound).Numbers(i) <> nCommonNumbers(i) Then
                    Call Add(tTimeGPS, nCommonNumbers)
                    Exit For
                End If
            Next
        End If
    End If

End Sub

'状態の追加。
'
'指定された時間の状態がすでに登録されていた場合は上書きする。
'
'引き数：
'tTimeGPS 時刻(GPS)。
'nNumbers 衛星番号。配列の要素は(-1 To ...)、要素 -1 は未使用。
Public Sub Add(ByVal tTimeGPS As Date, ByRef nNumbers() As Long)
    Dim nUBound As Long
    nUBound = UBound(m_tStates) + 1
    If DateDiff("s", m_tStates(nUBound - 1).TimeGPS, tTimeGPS) = 0 Then
        '時間が同じなら上書き。
        m_tStates(nUBound - 1).Numbers = nNumbers
    ElseIf DateDiff("s", m_tStates(nUBound - 1).TimeGPS, tTimeGPS) > 0 Then
        '未来なら追加。
        ReDim Preserve m_tStates(-1 To nUBound)
        m_tStates(nUBound).TimeGPS = tTimeGPS
        m_tStates(nUBound).Numbers = nNumbers
    End If
End Sub

'*******************************************************************************
'インプリメンテーション

'共通する番号を取得する。
'
'bPrevNumbers と bNextNumbers の両方で見えていた衛星の番号を取得する。
'
'引き数：
'bPrevNumbers 前回見えていた衛星。配列インデックスが衛星番号に対応する。True=見えていた。False=見えていなかった。参照する配列の要素は(0 To PPS_MAXPRN_CAPA)。
'bNextNumbers 今回見えている衛星。配列インデックスが衛星番号に対応する。True=見えている。False=見えていない。参照する配列の要素は(0 To PPS_MAXPRN_CAPA)。
'
'戻り値：共通する衛星番号を返す。配列の要素は(-1 To ...)、要素 -1 は未使用。
Private Function GetCommonNumbers(ByRef bPrevNumbers() As Boolean, ByRef bNextNumbers() As Boolean) As Long()
    Dim nCommonNumbers() As Long
    ReDim nCommonNumbers(-1 To -1)
    Dim i As Long
    For i = 0 To PPS_MAXPRN_CAPA
        If bPrevNumbers(i) And bNextNumbers(i) Then
            Dim nUBound As Long
            nUBound = UBound(nCommonNumbers) + 1
            ReDim Preserve nCommonNumbers(-1 To nUBound)
            nCommonNumbers(nUBound) = i
        End If
    Next
    GetCommonNumbers = nCommonNumbers
End Function
#endif
    }
}
