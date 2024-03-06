using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using System.Windows.Forms;
using System.Diagnostics;

namespace SurvLine.mdl
{
    internal class MdlMatrix
    {
        //==========================================================================================
        /*[VB]
        '*******************************************************************************
        '行列
        '
        '配列と行列の対応。
        'A･B を考える。
        '例えば次のような行列の場合、
        '　               　┌   ┐
        '┌               ┐│B00│
        '│a00 a01 a02 a03││B10│
        '│a10 a11 a12 a13││B20│
        '└               ┘│B30│
        '　               　└   ┘
        'A()とB()の次元はA(2,4)とB(4,1)になる。
        'つまり配列の1次元目が行数、2次元目が列数に対応する。

        Option Explicit
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        Private Const MSG_MATRIX_ARRAYINVALID = "行列の配列数が不正です。"
        Private Const MSG_MATRIX_NOINVERSE = "行列の逆行列が求まりません。"
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        private const string MSG_MATRIX_ARRAYINVALID = "行列の配列数が不正です。";
        private const string MSG_MATRIX_NOINVERSE = "行列の逆行列が求まりません。";
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '*******************************************************************************
        'メソッド

        '逆行列。
        '
        '引き数：
        'A 行列。
        'clsProgressInterface ProgressInterface オブジェクト。
        '
        '戻り値：Aの逆行列。
        Public Function Inverse(ByRef A() As Double, Optional ByVal clsProgressInterface As ProgressInterface = Nothing) As Double()
            If UBound(A, 1) <> UBound(A, 2) Then Call Err.Raise(ERR_MATRIX, , MSG_MATRIX_ARRAYINVALID) 'MATRIXERR
    
            On Error GoTo ErrorHandler
    
            Dim i As Long
            Dim j As Long
            Dim k As Long
    
            Dim M As Long
            Dim W As Long
            Dim mv As Double
            Dim sv As Double
    
            Dim S As Long
            Dim aa() As Double
            Dim bb() As Double
            Dim cc() As Double
            Dim P() As Long
            S = UBound(A, 1)
            ReDim aa(S, S)
            ReDim bb(S)
            ReDim cc(S, S)
            ReDim P(S)
            For i = 0 To S
                For j = 0 To S
                    aa(i, j) = A(i, j)
                Next
                P(i) = i
            Next
    
            'プログレス。
            If Not clsProgressInterface Is Nothing Then Call clsProgressInterface.CheckCancel
    
            'LU分解。
            For i = 0 To S
                'ピボット選択。
                M = i
                mv = Abs(aa(P(M), i))
                For j = i + 1 To S
                    sv = Abs(aa(P(j), i))
                    If mv < sv Then
                        M = j
                        mv = sv
                    End If
                Next
                'プログレス。
                If Not clsProgressInterface Is Nothing Then Call clsProgressInterface.CheckCancel
                'SWAP
                W = P(i)
                P(i) = P(M)
                P(M) = W
                'LU分解。
                For j = i + 1 To S
                    aa(P(j), i) = aa(P(j), i) / aa(P(i), i)
                    For k = i + 1 To S
                        aa(P(j), k) = aa(P(j), k) - aa(P(i), k) * aa(P(j), i)
                    Next
                Next
                'プログレス。
                If Not clsProgressInterface Is Nothing Then Call clsProgressInterface.CheckCancel
            Next
    
            '逆行列を求める。
            For k = 0 To S
                '初期化。
                For i = 0 To S
                    If i = k Then
                        bb(i) = 1
                    Else
                        bb(i) = 0
                    End If
                Next
                'プログレス。
                If Not clsProgressInterface Is Nothing Then Call clsProgressInterface.CheckCancel
                '解を求める。
                For i = 0 To S
                    For j = i + 1 To S
                        bb(P(j)) = bb(P(j)) - bb(P(i)) * aa(P(j), i)
                    Next
                Next
                'プログレス。
                If Not clsProgressInterface Is Nothing Then Call clsProgressInterface.CheckCancel
                For i = S To 0 Step -1
                    For j = i + 1 To S
                        bb(P(i)) = bb(P(i)) - aa(P(i), j) * bb(P(j))
                    Next
                    bb(P(i)) = bb(P(i)) / aa(P(i), i)
                Next
                'プログレス。
                If Not clsProgressInterface Is Nothing Then Call clsProgressInterface.CheckCancel
                'ピボット選択で入れ替わった値を元に戻す。
                For i = 0 To S
                    cc(i, k) = bb(P(i))
                Next
                'プログレス。
                If Not clsProgressInterface Is Nothing Then Call clsProgressInterface.CheckCancel
            Next
    
            Inverse = cc
    
            Exit Function
    
        ErrorHandler:
            If Err.Number <> cdlCancel Then
                Call Err.Raise(ERR_NOINVERSE, , MSG_MATRIX_NOINVERSE)
            Else
                Call Err.Raise(Err.Number, Err.Source, Err.Description, Err.HelpFile, Err.HelpContext)
            End If
    
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '*******************************************************************************
        'メソッド

        '逆行列。
        '
        '引き数：
        'A 行列。
        'clsProgressInterface ProgressInterface オブジェクト。
        '
        '戻り値：Aの逆行列。
        */
        public static double[,] Inverse(ref double[,] A, ProgressInterface clsProgressInterface = null)
        {
            if (A.GetLength(0) != A.GetLength(1))
            {
#if false
                Err.Raise(ERR_MATRIX, , MSG_MATRIX_ARRAYINVALID); //'MATRIXERR
#endif
            }

            long i = 0;
            long j = 0;
            long k = 0;

            long M = 0;
            long W = 0;
            double mv = 0;
            double sv = 0;

            long S = 0;
            double[,] aa = new double[S, S];
            double[] bb = new double[S];
            double[,] cc = new double[S, S];
            long[] P = new long[S];
            try
            {
                S = A.GetLength(0);
                for (i = 0; i < S; i++)
                {
                    for (j = 0; j < S; j++)
                    {
                        aa[i, j] = A[i, j];
                    }
                    P[i] = i;
                }

                //'プログレス。
                if (clsProgressInterface != null)
                {
                    clsProgressInterface.CheckCancel();
                }

                //'LU分解。
                for (i = 0; i < S; i++)
                {
                    //'ピボット選択。
                    M = i;
                    mv = Math.Abs(aa[P[M], i]);
                    for (j = i + 1; j < S; j++)
                    {
                        sv = Math.Abs(aa[P[j], i]);
                        if (mv < sv)
                        {
                            M = j;
                            mv = sv;
                        }
                    }
                    //'プログレス。
                    if (clsProgressInterface != null)
                    {
                        clsProgressInterface.CheckCancel();
                    }
                    //'SWAP
                    W = P[i];
                    P[i] = P[M];
                    P[M] = W;
                    //'LU分解。
                    for (j = i + 1; j < S; j++)
                    {
                        aa[P[j], i] = aa[P[j], i] / aa[P[i], i];
                        for (k = i + 1; k < S; k++)
                        {
                            aa[P[j], k] = aa[P[j], k] - aa[P[i], k] * aa[P[j], i];
                        }
                    }
                    //'プログレス。
                    if (clsProgressInterface != null)
                    {
                        clsProgressInterface.CheckCancel();
                    }
                }

                //'逆行列を求める。
                for (k = 0; k < S; k++)
                {
                    //'初期化。
                    for (i = 0; i < S; i++)
                    {
                        if (i == k)
                        {
                            bb[i] = 1;
                        }
                        else
                        {
                            bb[i] = 0;
                        }
                    }
                    //'プログレス。
                    if (clsProgressInterface != null)
                    {
                        clsProgressInterface.CheckCancel();
                    }
                    //'解を求める。
                    for (i = 0; i < S; i++)
                    {
                        for (j = i + 1; j < S; j++)
                        {
                            bb[P[j]] = bb[P[j]] - (bb[P[i]] * aa[P[j], i]);
                        }
                    }
                    //'プログレス。
                    if (clsProgressInterface != null)
                    {
                        clsProgressInterface.CheckCancel();
                    }
                    for (i = S; i > 0; i--)
                    {
                        for (j = i + 1; j < S; j++)
                        {
                            bb[P[i]] = bb[P[i]] - aa[P[i], j] * bb[P[j]];
                        }
                        bb[P[i]] = bb[P[i]] / aa[P[i], i];
                    }
                    //'プログレス。
                    if (clsProgressInterface != null)
                    {
                        clsProgressInterface.CheckCancel();
                    }
                    //'ピボット選択で入れ替わった値を元に戻す。
                    for (i = 0; i < S; i++)
                    {
                        cc[i, k] = bb[P[i]];
                    }
                    //'プログレス。
                    if (clsProgressInterface != null)
                    {
                        clsProgressInterface.CheckCancel();
                    }
                }

                return cc;


            }


            catch (Exception)
            {
#if false
                if (Err.Number != cdlCancel)
                {
                    Err.Raise(ERR_NOINVERSE, , MSG_MATRIX_NOINVERSE);
                }
                else
                {
                    Err.Raise(Err.Number, Err.Source, Err.Description, Err.HelpFile, Err.HelpContext);
                }
#endif
                return cc;
            }
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '転置。
        '
        '引き数：
        'A 行列。
        '
        '戻り値：Aの転置列。
        Public Function Transpose(ByRef A() As Double) As Double()
            Dim C() As Double
            ReDim C(UBound(A, 2), UBound(A, 1))
            Dim i As Long
            Dim j As Long
            For i = 0 To UBound(A, 1)
                For j = 0 To UBound(A, 2)
                    C(j, i) = A(i, j)
                Next
            Next
            Transpose = C
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '転置。
        '
        '引き数：
        'A 行列。
        '
        '戻り値：Aの転置列。
        */
        public static double[,] Transpose(ref double[,] A)
        {
            double[,] C = new double[A.GetLength(1), A.GetLength(0)];
            long i;
            long j;
            for (i = 0; i < A.GetLength(0); i++)
            {
                for (j = 0; j < A.GetLength(1); j++)
                {
                    C[j, i] = A[i, j];
                }
            }
            return C;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '積。
        '
        '引き数：
        'A 行列。
        'B 行列。
        'clsProgressInterface ProgressInterface オブジェクト。
        '
        '戻り値：A･B の結果。
        Public Function Product(ByRef A() As Double, ByRef B() As Double, Optional ByVal clsProgressInterface As ProgressInterface = Nothing) As Double()
            If UBound(A, 2) <> UBound(B, 1) Then Call Err.Raise(ERR_MATRIX, , MSG_MATRIX_ARRAYINVALID) 'MATRIXERR
            Dim C() As Double
            ReDim C(UBound(A, 1), UBound(B, 2))
            Dim i As Long
            Dim j As Long
            Dim k As Long
            For i = 0 To UBound(B, 2)
                For j = 0 To UBound(A, 1)
                    C(j, i) = 0
                    For k = 0 To UBound(A, 2)
                        C(j, i) = C(j, i) + A(j, k) * B(k, i)
                    Next
                    'プログレス。
                    If Not clsProgressInterface Is Nothing Then Call clsProgressInterface.CheckCancel
                Next
            Next
            Product = C
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '積。
        '
        '引き数：
        'A 行列。
        'B 行列。
        'clsProgressInterface ProgressInterface オブジェクト。
        '
        '戻り値：A･B の結果。
        */
        public static double[,] Product(ref double[,] A, ref double[,] B, ProgressInterface clsProgressInterface = null)
        {
            if (A.GetLength(1) != B.GetLength(0))
            {
#if false
                Err.Raise(ERR_MATRIX, , MSG_MATRIX_ARRAYINVALID);           //'MATRIXERR
#endif
            }
            double[,] C = new double[A.GetLength(0), B.GetLength(1)];
            long i;
            long j;
            long k;
            for (i = 0; i < B.GetLength(1); i++)
            {
                for (j = 0; j < A.GetLength(0); j++)
                {
                    C[j, i] = 0;
                    for (k = 0; k < A.GetLength(1); k++)
                    {
                        C[j, i] = C[j, i] + A[j, k] * B[k, i];
                    }
                    //'プログレス。
                    if (clsProgressInterface != null)
                    {
                        clsProgressInterface.CheckCancel();
                    }
                }
            }
            return C;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '乗。
        '
        '引き数：
        'A 行列。
        'K 係数。
        '
        '戻り値：A*k の結果。
        Public Function Multiplication(ByRef A() As Double, ByVal k As Double) As Double()
            Dim C() As Double
            ReDim C(UBound(A, 1), UBound(A, 2))
            Dim i As Long
            Dim j As Long
            For i = 0 To UBound(A, 1)
                For j = 0 To UBound(A, 2)
                    C(i, j) = A(i, j) * k
                Next
            Next
            Multiplication = C
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '乗。
        '
        '引き数：
        'A 行列。
        'K 係数。
        '
        '戻り値：A*k の結果。
        */
        public static double[,] Multiplication(ref double[,] A, double k)
        {
            double[,] C = new double[A.GetLength(0), A.GetLength(1)];
            long i;
            long j;
            for (i = 0; i < A.GetLength(0); i++)
            {
                for (j = 0; j < A.GetLength(1); j++)
                {
                    C[i, j] = A[i, j] * k;
                }
            }
            return C;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '除。
        '
        '引き数：
        'A 行列。
        'K 係数。
        '
        '戻り値：A/k の結果。
        Public Function Division(ByRef A() As Double, ByVal k As Double) As Double()
            Dim C() As Double
            ReDim C(UBound(A, 1), UBound(A, 2))
            Dim i As Long
            Dim j As Long
            For i = 0 To UBound(A, 1)
                For j = 0 To UBound(A, 2)
                    C(i, j) = A(i, j) / k
                Next
            Next
            Division = C
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '除。
        '
        '引き数：
        'A 行列。
        'K 係数。
        '
        '戻り値：A/k の結果。
        */
        public static double[,] Division(ref double[,] A, double k)
        {
            double[,] C = new double[A.GetLength(0), A.GetLength(1)];
            long i;
            long j;
            for (i = 0; i < A.GetLength(0); i++)
            {
                for (j = 0; j < A.GetLength(1); j++)
                {
                    C[i, j] = A[i, j] / k;
                }
            }
            return C;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '和。
        '
        '引き数：
        'A 行列。
        'B 行列。
        '
        '戻り値：A+B の結果。
        Public Function Addition(ByRef A() As Double, ByRef B() As Double) As Double()
            If UBound(A, 1) <> UBound(B, 1) Or UBound(A, 2) <> UBound(B, 2) Then Call Err.Raise(ERR_MATRIX, , MSG_MATRIX_ARRAYINVALID) 'MATRIXERR
            Dim C() As Double
            ReDim C(UBound(A, 1), UBound(A, 2))
            Dim i As Long
            Dim j As Long
            For i = 0 To UBound(A, 1)
                For j = 0 To UBound(A, 2)
                    C(i, j) = A(i, j) + B(i, j)
                Next
            Next
            Addition = C
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '和。
        '
        '引き数：
        'A 行列。
        'B 行列。
        '
        '戻り値：A+B の結果。
        */
        public static double[,] Addition(ref double[,] A, ref double[,] B)
        {
            if (A.GetLength(0) != B.GetLength(0) || A.GetLength(1) != B.GetLength(1))
            {
#if false
                Err.Raise(ERR_MATRIX, , MSG_MATRIX_ARRAYINVALID);       //'MATRIXERR
#endif
            }
            double[,] C = new double[A.GetLength(0), A.GetLength(1)];
            long i;
            long j;
            for (i = 0; i < A.GetLength(0); i++)
            {
                for (j = 0; j < A.GetLength(1); j++)
                {
                    C[i, j] = A[i, j] + B[i, j];
                }
            }
            return C;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '差。
        '
        '引き数：
        'A 行列。
        'B 行列。
        '
        '戻り値：A-B の結果。
        Public Function Subtraction(ByRef A() As Double, ByRef B() As Double) As Double()
            If UBound(A, 1) <> UBound(B, 1) Or UBound(A, 2) <> UBound(B, 2) Then Call Err.Raise(ERR_MATRIX, , MSG_MATRIX_ARRAYINVALID) 'MATRIXERR
            Dim C() As Double
            ReDim C(UBound(A, 1), UBound(A, 2))
            Dim i As Long
            Dim j As Long
            For i = 0 To UBound(A, 1)
                For j = 0 To UBound(A, 2)
                    C(i, j) = A(i, j) - B(i, j)
                Next
            Next
            Subtraction = C
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '差。
        '
        '引き数：
        'A 行列。
        'B 行列。
        '
        '戻り値：A-B の結果。
        */
        public static double[,] Subtraction(ref double[,] A, ref double[,] B)
        {
            if (A.GetLength(0) != B.GetLength(0) || A.GetLength(1) != B.GetLength(1))
            {
#if false
                Err.Raise(ERR_MATRIX, , MSG_MATRIX_ARRAYINVALID);       //'MATRIXERR
#endif
            }
            double[,] C = new double[A.GetLength(0), A.GetLength(1)];
            long i;
            long j;
            for (i = 0; i < A.GetLength(0); i++)
            {
                for (j = 0; j < A.GetLength(1); j++)
                {
                    C[i, j] = A[i, j] - B[i, j];
                }
            }
            return C;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'デバッグ出力。
        Public Sub DebugPrintMatrix(ByRef A() As Double)
            Dim sBuff As String
            Dim i As Long
            Dim j As Long
            Debug.Print "Matrix: (" & CStr(UBound(A, 1) + 1) & "," & CStr(UBound(A, 2) + 1) & ")"
            For i = 0 To UBound(A, 1)
                sBuff = ""
                For j = 0 To UBound(A, 2)
                    sBuff = sBuff & "," & Format0Trim(A(i, j), "0.00000000000000000000")
                Next
                Debug.Print sBuff
            Next
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'デバッグ出力。
        public void DebugPrintMatrix(ref double[,] A)
        {
#if false
            string sBuff;
            long i;
            long j;
            //Debug.Print "Matrix: (" & CStr(UBound(A, 1) + 1) & "," & CStr(UBound(A, 2) + 1) & ")"
            for (i = 0; i < A.GetLength(0); i++)
            {
                sBuff = "";
                for (j = 0; j < A.GetLength(1); j++)
                {
                    //sBuff = sBuff + "," + Format0Trim(A(i, j), "0.00000000000000000000");
                }
                //Debug.Print sBuff
            }
#else
            return;
#endif
        }
        //==========================================================================================
    }
}
