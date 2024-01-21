using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurvLine
{
    internal class pmemo
    {
        //***********************
        //  変更履歴
        //***********************


        //***************************************************************************
        //***************************************************************************
        //プログラム＜プロジェクト＞
        //Git管理：コメント＞   24/01/04 現場関連の更新
        //
        //＜コメント＞
        //24/01/04 K.setoguchi@NV---------->>>>>>>>>>
        //<<<<<<<<<-----------24/01/04 K.setoguchi@NV
        //***************************************************************************
        //***************************************************************************

        //==========================================================================================
        /*[VB]
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]



#if false   //メモ
    //-------------------------------------------------------------------
    //    If Not IsEmpty(Lon) Then
    //        If Abs(Lon - clsKnownPoint.Lon) >= FLT_EPSILON Then Exit Function
    //    End If
    if (!(ReferenceEquals(Lon, null) == true))
    {

        if (Math.Abs(Lat - clsKnownPoint.Lon) >= MdiDefine.DEFINE.FLT_EPSILON)
        {
            return Compare;
        }
    }
#endif


        //*******************************************************************
        //プログラム＜プロジェクト＞
        //
        //Git管理：コメント＞   23/12/26 新規作成 Ini直前ませ作成
        //
        //＜コメント＞
        //23/12/29 K.setoguchi@NV---------->>>>>>>>>>
        //<<<<<<<<<-----------23/12/29 K.setoguchi@NV


        //*******************************************************************
        //プログラム＜プロジェクト＞ Src_SurvLine_231227_1240_ToSakai
        //
        //Git管理：コメント＞   23/12/26 新規作成 書込の直前ませ作成
        //
        //＜コメント＞
        //23/12/26 K.setoguchi@NV---------->>>>>>>>>>
        //<<<<<<<<<-----------23/12/26 K.setoguchi@NV
        //
        //23/12/24 K.setoguchi@NV---------->>>>>>>>>>
        //<<<<<<<<<-----------23/12/24 K.setoguchi@NV
        //
        //
        //23/12/22 K.setoguchi@NV---------->>>>>>>>>>
        //<<<<<<<<<-----------23/12/22 K.setoguchi@NV
        //
        //23/12/20 K.setoguchi@NV---------->>>>>>>>>>
        //<<<<<<<<<-----------23/12/20 K.setoguchi@NV

        //***************************************************************************

#if false   //-----------------------------------------
    //テストコーディング
    //-----------------------------------------

    //ObsDataMask---->>>>>>>>>>>>>>>>>>>>>>>>>
        var xlist = new List<List<string>>();
        xlist.Add(new List<string>());

        xlist[0].Add("あ");
        xlist[0].Add("い");
        xlist[0].Add("う");

        xlist.Add(new List<string>());
        xlist[1].Add("か");
        xlist[1].Add("き");

        //------------------------
        List<List<string>> dataListA = new List<List<string>>();
        List<string> Val1 = new List<string> {"A","B","C" };
        List<string> Val2 = new List<string> {"1","2","2" };

        dataListA.Add(Val2);

        //------------------------
        List<List<string>> dddataList;
        List<List<string>> dataList = new List<List<string>>();
        List<string> Val11 = new List<string> { "A", "B", "C" };
        List<string> Val21 = new List<string> { "1", "2", "2" };

        dddataList = dataList;
        dddataList.Add(Val11);  //[0]"A", [1]"B", [2]"C"
        dddataList.Add(Val21);  //[0]"1", [1]"2", [2]"2"

        //------------------------
        //------------------------
        //Genba_S.MaskInfo_StrTimes;
        //List<List<MaskInfo_T>> BdataList = new List<List<MaskInfo_T>>();
        //Genba_S.MaskInfo_StrTimes = BdataList;
        //List<MaskInfo_T> MaskInfo_StrTimes = new List<MaskInfo_T>();
        //MaskInfo_StrTimes = DateTime.MinValue;
        //Genba_S.m_tMaskInfos_T[0].Add(MaskInfo_ST);


        //public List<List<DateTime>> MaskInfo_StrTimes;



        List<List<DateTime>> dataList1 = new List<List<DateTime>>();
        Genba_S.m_tMaskInfos_StrTimes = dataList1;
        //-------------------------------------------------------------
        DateTime Times = DateTime.MinValue;

        for( int X = 0;  X < 10; X++)
        {
            List<DateTime> stdate = new List<DateTime>();
            stdate.Add(Times);
            stdate.Add(Times);
            stdate.Add(Times);
            stdate.Add(Times);
            stdate.Add(Times);
            Genba_S.m_tMaskInfos_StrTimes.Add(stdate);

        }
    //<<<<<<<<<<<<<<<-------------ObsDataMask----


#endif      //-----------------------------------------



    }
}
