using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
using static SurvLine.mdl.MdlAutoOrderVector;

namespace SurvLine
{
    internal class AutoOrderVectorParam
    {

        //5==========================================================================================
        /*[VB]
            '*******************************************************************************
            '基線ベクトルの向きの自動整列パラメータ

            Option Explicit

            Public ProcType As AUTOORDERVECTOR_PROCTYPE '整列手段。
            Public Fixed As String '固定点。

            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#] //5
        public AUTOORDERVECTOR_PROCTYPE ProcType;   //'整列手段。
        public string Fixed;                        //'固定点。


        //------------------------------------------------------------------------------------------
        //[C#] //5
        public AutoOrderVectorParam()
        {

        }

        //5==========================================================================================
        /*[VB]
            'インプリメンテーション

            '*******************************************************************************
            'プロパティ

            '帳票パラメータ。
            '
            'このプロパティはデフォルトプロパティである。Letを使えば｢値の代入｣ができる。いわゆるC++で言うところのoperator=である。
            '
            '引き数：
            'clsAutoOrderVectorParam コピー元のオブジェクト。
            Property Let AutoOrderVectorParam(ByVal clsAutoOrderVectorParam As AutoOrderVectorParam)
                ProcType = clsAutoOrderVectorParam.ProcType
                Fixed = clsAutoOrderVectorParam.Fixed
            End Property
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#] //5
        public void AutoOrderVectorParam_Sub(AutoOrderVectorParam clsAutoOrderVectorParam)
        {
            ProcType = clsAutoOrderVectorParam.ProcType;
            Fixed = clsAutoOrderVectorParam.Fixed;
        }


        //5==========================================================================================
        /*[VB]
            '*******************************************************************************
            'メソッド

            '保存。
            '
            '引き数：
            'nFile ファイル番号。
            Public Sub Save(ByVal nFile As Integer)
                Put #nFile, , ProcType
                Call PutString(nFile, Fixed)
            End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#] //5
        public void Save(int nFile)
        {
            //  Put #nFile, , ProcType
            //  Call PutString(nFile, Fixed)
        }

        //5==========================================================================================
        /*[VB]
            '読み込み。
            '
            '引き数：
            'nFile ファイル番号。
            'nVersion ファイルバージョン。
            Public Sub Load(ByVal nFile As Integer, ByVal nVersion As Long)
                Get #nFile, , ProcType
                Fixed = GetString(nFile)
            End Sub
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#] //5
        public void Load(int nFile, long nVersion)
        {
            //Get #nFile, , ProcType
            //    Fixed = GetString(nFile)

        }

        //5==========================================================================================
        /*[VB]
            '指定されたオブジェクトと比較する。
            '
            '引き数：
            'clsAutoOrderVectorParam 比較対照オブジェクト。
            '
            '戻り値：
            '一致する場合 True を返す。
            'それ以外の場合 False を返す。
            Public Function Compare(ByVal clsAutoOrderVectorParam As AutoOrderVectorParam) As Boolean

                Compare = False
    
                If ProcType <> clsAutoOrderVectorParam.ProcType Then Exit Function
                If StrComp(Fixed, clsAutoOrderVectorParam.Fixed) <> 0 Then Exit Function
    
                Compare = True
    
            End Function
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#] //5
        public bool Compare(AutoOrderVectorParam clsAutoOrderVectorParam)
        {
            bool Compare = false;

            if (ProcType != clsAutoOrderVectorParam.ProcType)
            {
                return Compare;
            }

            if (Fixed.CompareTo(clsAutoOrderVectorParam.Fixed) != 0)
            {
                return Compare;
            }

            Compare = true;
            return Compare;

        }

        //==================================================

    }

}
