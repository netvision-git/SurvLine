using SurvLine.mdl;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace SurvLine
{
    internal class KnownPoint
    {
        MdlUtility mdlUtility = new MdlUtility();

        //'*******************************************************************************
        //'既知点
        //
        //Option Explicit
        //
        //'プロパティ
        public bool Coordinate;             // As Boolean '座標指定フラグ。True=既知点は座標で指定される(Lat、Lon が有効)。False=既知点は固定点番号で指定される(Numberが有効)。
        public long Lat;                    // As Variant '緯度(度)。Empty は不定とみなす。
        public long Lon;                    // As Variant '経度(度)。Empty は不定とみなす。
        public string Number;               // As String '固定点番号。




        //'*******************************************************************************
        //'プロパティ
        //
        public KnownPoint()
        {

        }


        //'*******************************************************************************
        //'*******************************************************************************
        /// <summary>
        /// '既知点。
        /// '
        /// 'このプロパティはデフォルトプロパティである。Letを使えば｢値の代入｣ができる。いわゆるC++で言うところのoperator=である。
        /// '
        /// '引き数：
        ///     clsKnownPoint コピー元のオブジェクト。
        /// </summary>
        /// <param name="clsKnownPoint"></param>
        public KnownPoint(KnownPoint clsKnownPoint)
        {
            Coordinate = clsKnownPoint.Coordinate;
            Lat = clsKnownPoint.Lat;
            Lon = clsKnownPoint.Lon;
            Number = clsKnownPoint.Number;
        }
        //'既知点。
        //'
        //'このプロパティはデフォルトプロパティである。Letを使えば｢値の代入｣ができる。いわゆるC++で言うところのoperator=である。
        //'
        //'引き数：
        //'clsKnownPoint コピー元のオブジェクト。
        //Property Let KnownPoint(ByVal clsKnownPoint As KnownPoint)
        //    Coordinate = clsKnownPoint.Coordinate
        //    Lat = clsKnownPoint.Lat
        //    Lon = clsKnownPoint.Lon
        //    Number = clsKnownPoint.Number
        //End Property



        //***************
        //'メソッド
        //***************


        //'*******************************************************************************
        //'*******************************************************************************
        /// <summary>
        /// 保存。
        ///
        /// 引き数：
        ///     bw バイナリファイル
        /// </summary>
        /// <param name="bw"></param>
        public void Save( BinaryWriter bw)
        {
            mdlUtility.PutFileBool(bw, Coordinate);     //    Put #nFile, , Coordinate
            bw.Write((int)Lat);                         //    Put #nFile, , Lat
            bw.Write((int)Lon);                         //    Put #nFile, , Lon
            mdlUtility.FileWrite_PutString(bw, Number); //    Call PutString(nFile, Number)

        }
        //--------------------------------------------------------------------------------
        //'保存。
        //'
        //'引き数：
        //'nFile ファイル番号。
        //Public Sub Save(ByVal nFile As Integer)
        //    Put #nFile, , Coordinate
        //    Put #nFile, , Lat
        //    Put #nFile, , Lon
        //    Call PutString(nFile, Number)
        //End Sub
        //'*******************************************************************************
        //'*******************************************************************************


        //'*******************************************************************************
        //'*******************************************************************************
        /// <summary>
        /// 読み込み。
        ///
        /// 引き数：
        ///     br バイナリファイル
        ///     nVersion ファイルバージョン。
        /// </summary>
        /// <param name="br"></param>
        /// <param name="nVersion"></param>
        public void Load(BinaryReader br, long nVersion)
        {
            Coordinate = mdlUtility.GetFileBool(br);    //    Get #nFile, , Coordinate
            if (nVersion < 3600)
            {
                double nValue;              //        Dim nValue As Double
                nValue = br.ReadDouble();   //        Get #nFile, , nValue
                Lat = (long)nValue;         //        Lat = nValue
                nValue = br.ReadDouble();   //        Get #nFile, , nValue
                Lon = (long)nValue;         //        Lon = nValue
            }
            else
            {
                Lat = br.ReadInt32();       //        Get #nFile, , Lat
                Lon = br.ReadInt32();       //        Get #nFile, , Lon
            }
        }
        //---------------------------------------------------------------------------------
        //'読み込み。
        //'
        //'引き数：
        //'nFile ファイル番号。
        //'nVersion ファイルバージョン。
        //Public Sub Load(ByVal nFile As Integer, ByVal nVersion As Long)
        //    Get #nFile, , Coordinate
        //    If nVersion< 3600 Then
        //        Dim nValue As Double
        //        Get #nFile, , nValue
        //        Lat = nValue
        //        Get #nFile, , nValue
        //        Lon = nValue
        //    Else
        //        Get #nFile, , Lat
        //        Get #nFile, , Lon
        //    End If
        //    Number = GetString(nFile)
        //End Sub
        //'*******************************************************************************


        //'*******************************************************************************
        //'*******************************************************************************
        /// <summary>
        /// 指定されたオブジェクトと比較する。
        ///
        /// 引き数：
        /// clsKnownPoint 比較対照オブジェクト。
        ///
        /// </summary>
        /// <param name="clsKnownPoint"></param>
        /// <returns>
        ///   戻り値：
        ///     一致する場合 True を返す。
        ///     それ以外の場合 False を返す。
        /// </returns>
        public bool Compare(KnownPoint clsKnownPoint)
        {
            bool Compare = false;

            Document document = new Document();
            //-------------------------------------------------------------------
            //    If Coordinate<> clsKnownPoint.Coordinate Then Exit Function
            if (Coordinate != clsKnownPoint.Coordinate) { return Compare; }
            //-------------------------------------------------------------------
            //    If IsEmpty(Lat) <> IsEmpty(clsKnownPoint.Lat) Then Exit Function
            if (ReferenceEquals(Lat, null) != ReferenceEquals(clsKnownPoint.Lat, null)) { return Compare; }
            //-------------------------------------------------------------------
            //    If IsEmpty(Lon) <> IsEmpty(clsKnownPoint.Lon) Then Exit Function
            if (ReferenceEquals(Lon, null) != ReferenceEquals(clsKnownPoint.Lon, null)) { return Compare; }
            //-------------------------------------------------------------------

            //-------------------------------------------------------------------
            //    If Not IsEmpty(Lat) Then
            //        If Abs(Lat - clsKnownPoint.Lat) >= FLT_EPSILON Then Exit Function
            if (!(ReferenceEquals(Lat, null) == true)){

                if (Math.Abs(Lat - clsKnownPoint.Lat) >= MdiDefine.DEFINE.FLT_EPSILON)
                {
                    return Compare;
                }
            }

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

            //-------------------------------------------------------------------
            //    If StrComp(Number, clsKnownPoint.Number) <> 0 Then Exit Function
            if(Number.CompareTo(clsKnownPoint.Number) != 0)
            {
                return Compare;
            }


            Compare = true;
            return Compare;
        }
        //--------------------------------------------------------------------------------
        //'指定されたオブジェクトと比較する。
        //'
        //'引き数：
        //'clsKnownPoint 比較対照オブジェクト。
        //'
        //'戻り値：
        //'一致する場合 True を返す。
        //'それ以外の場合 False を返す。
        //Public Function Compare(ByVal clsKnownPoint As KnownPoint) As Boolean
        //
        //    Compare = False
        //
        //    If Coordinate<> clsKnownPoint.Coordinate Then Exit Function
        //    If IsEmpty(Lat) <> IsEmpty(clsKnownPoint.Lat) Then Exit Function
        //    If IsEmpty(Lon) <> IsEmpty(clsKnownPoint.Lon) Then Exit Function
        //    If Not IsEmpty(Lat) Then
        //        If Abs(Lat - clsKnownPoint.Lat) >= FLT_EPSILON Then Exit Function
        //    End If
        //    If Not IsEmpty(Lon) Then
        //        If Abs(Lon - clsKnownPoint.Lon) >= FLT_EPSILON Then Exit Function
        //    End If
        //    If StrComp(Number, clsKnownPoint.Number) <> 0 Then Exit Function
        //
        //    Compare = True
        //
        //
        //End Function



    }
}
