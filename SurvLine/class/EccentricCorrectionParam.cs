using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Net.WebRequestMethods;

namespace SurvLine
{
    public class EccentricCorrectionParam
    {
        MdlUtility mdlUtility = new MdlUtility();
        Document document = new Document();

        //***************************************************************************
        //***************************************************************************
        /// <summary>
        ///'読み込み。
        ///'    長男が親観測点の Load を呼ぶ。
        ///     （VB Load関数をVC用に変更）
        ///'
        ///'引き数：
        /// バイナリファイル
        /// バージョン番号
        /// </summary>
        /// <param name="BinaryReader br">
        /// <param name="nVersion br">
        /// <param name="Genba_S">
        /// </param>
        /// <returns>
        /// 戻り値:returns = Non
        /// </returnsGenba_S
        //***************************************************************************
        public void Load(BinaryReader br, long nVersion, ref GENBA_STRUCT_S Genba_S)
        {
            //-----------------------------------
            //[VB]  Get #nFile, , AngleType     public int AngleType;               //As ANGLE_TYPE '角度種別。              0
            Genba_S.AngleType = br.ReadInt32();
            //-----------------------------------
            //[VB]  Marker = GetString(nFile)   public string Marker;               //As String '方位標(基線ベクトルのキー)。0
            Genba_S.Marker = mdlUtility.FileRead_GetString(br);
            //-----------------------------------
            //[VB]  Get #nFile, , Horizontal    public double Horizontal;           //As Double '水平角(ラジアン)。         0
            Genba_S.Horizontal = br.ReadDouble();
            //-----------------------------------
            //[VB]  Get #nFile, , Direction     public double Direction;            //As Double '方位角(ラジアン)。         0
            Genba_S.Direction = br.ReadDouble();
            //-----------------------------------
            //[VB]  Get #nFile, , Distance      public double Distance;             //As Double '斜距離(ｍ)。                0
            Genba_S.Distance = br.ReadDouble();
            //-----------------------------------
            //[VB]  Get #nFile, , UseTS         public bool UseTS;                  //As Boolean '測距儀を使用。           0
            Genba_S.UseTS = document.GetFileBool(br);
            //-----------------------------------
            //[VB]  Get #nFile, , FromHeightTS  public double FromHeightTS;         //As Double '測距儀高(ｍ)。           0
            Genba_S.FromHeightTS = br.ReadDouble();
            //-----------------------------------
            //[VB]  Get #nFile, , ToHeightTS    public double ToHeightTS;           //As Double '反射鏡高(ｍ)。           0
            Genba_S.ToHeightTS = br.ReadDouble();
            //-----------------------------------
            //[VB]  Get #nFile, , ElevationBC   public double ElevationBC;          //As Double '高度角B→C(ラジアン)。  0
            Genba_S.ElevationBC = br.ReadDouble();
            //-----------------------------------
            //[VB]  Get #nFile, , FromHeightBC  public double FromHeightBC;         //As Double '器械高B→C(ｍ)。         0
            Genba_S.FromHeightBC = br.ReadDouble();
            //-----------------------------------
            //[VB]  Get #nFile, , ToHeightBC    public double ToHeightBC;           //As Double '目標高B→C(ｍ)。         0
            Genba_S.ToHeightBC = br.ReadDouble();
            //-----------------------------------
            //[VB]  Get #nFile, , ElevationCB   public double ElevationCB;          //As Double '高度角C→B(ラジアン)。      0     
            Genba_S.ElevationCB = br.ReadDouble();
            //-----------------------------------
            //[VB]  Get #nFile, , FromHeightCB  public double FromHeightCB;         //As Double '器械高C→B(ｍ)。         0
            Genba_S.FromHeightCB = br.ReadDouble();
            //-----------------------------------
            //[VB]  Get #nFile, , ToHeightCB     public double ToHeightCB;           //As Double '目標高C→B(ｍ)。        0
            Genba_S.ToHeightCB = br.ReadDouble();
            //-----------------------------------
            //-----------------------------------
            if (nVersion < 7800) {
            }
            else
            {
                //-----------------------------------
                //[VB]  MarkNumber = GetString(nFile)   public string MarkNumber;           //As String  '手入力時の方位標のNo   ""
                Genba_S.MarkNumber = mdlUtility.FileRead_GetString(br);
                //-----------------------------------
                //[VB]  MarkName = GetString(nFile)     public string MarkName;             //As String  '手入力時の方位標の名称   ""
                Genba_S.MarkName = mdlUtility.FileRead_GetString(br);
                //-----------------------------------
                //[VB]  Get #nFile, , MarkEditCode      public int MarkEditCode;            //As EDITCODE_STYLE '編集コード。0の時はデフォルト。   0
                Genba_S.MarkEditCode = br.ReadInt32();
                //-----------------------------------
                //[VB]  Get #nFile, , MarkLat           public double MarkLat;              //As Double '緯度(度)。             0
                Genba_S.MarkLat = br.ReadDouble();
                //-----------------------------------
                //[VB]  Get #nFile, , MarkLon           public double MarkLon;              //As Double '経度(度)。             0
                Genba_S.MarkLon = br.ReadDouble();
                //-----------------------------------
                //[VB]  Get #nFile, , MarkX             public double MarkX;                //As Double '平面直角X(ｍ)。          0
                Genba_S.MarkX = br.ReadDouble();
                //-----------------------------------
                //[VB]  Get #nFile, , MarkY             public double MarkY;                //As Double '平面直角Y(ｍ)。          0
                Genba_S.MarkY = br.ReadDouble();
                //-----------------------------------
                //[VB]  Get #nFile, , MarkHeight        public double MarkHeight;           //As Double '楕円体高(ｍ)。           0
                Genba_S.MarkHeight = br.ReadDouble();
                //-----------------------------------
                //[VB]  Get #nFile, , MarkAlt           public double MarkAlt;              //As Double    '標高(ｍ)。          0
                Genba_S.MarkAlt = br.ReadDouble();
                //-----------------------------------
            }
            //-----------------------------------
            // '2009/11 H.Nakamura
            if (nVersion < 8201)
            {
                //UsePoint = ""; Public UsePoint As String '偏心に使用する基線(基線ベクトルのキー)。'2009 / 11 H.Nakamura
                Genba_S.UsePoint = "";

            }
            else
            {
                //UsePoint = GetString(nFile);    Public UsePoint As String '偏心に使用する基線(基線ベクトルのキー)。'2009 / 11 H.Nakamura 0
                Genba_S.UsePoint = mdlUtility.FileRead_GetString(br);
            }

        }
        //  '読み込み。
        //  '
        //  '引き数：
        //  'nFile ファイル番号。
        //  'nVersion ファイルバージョン。
        //  Public Sub Load(ByVal nFile As Integer, ByVal nVersion As Long)
        //  Dim clsObservationPoints() As ObservationPoint
        //  
        //      Get #nFile, , AngleType
        //      Marker = GetString(nFile)
        //      Get #nFile, , Horizontal
        //      Get #nFile, , Direction
        //      Get #nFile, , Distance
        //      Get #nFile, , UseTS
        //      Get #nFile, , FromHeightTS
        //      Get #nFile, , ToHeightTS
        //      Get #nFile, , ElevationBC
        //      Get #nFile, , FromHeightBC
        //      Get #nFile, , ToHeightBC
        //      Get #nFile, , ElevationCB
        //      Get #nFile, , FromHeightCB
        //      Get #nFile, , ToHeightCB
        //
        //      If nVersion< 7800 Then
        //      Else
        //          MarkNumber = GetString(nFile)
        //          MarkName = GetString(nFile)
        //          Get #nFile, , MarkEditCode
        //          Get #nFile, , MarkLat
        //          Get #nFile, , MarkLon
        //          Get #nFile, , MarkX
        //          Get #nFile, , MarkY
        //          Get #nFile, , MarkHeight
        //          Get #nFile, , MarkAlt
        //      End If
        //
        //      '2009/11 H.Nakamura
        //      If nVersion < 8201 Then
        //          UsePoint = ""
        //      Else
        //          UsePoint = GetString(nFile)
        //      End If
        // End Sub




    }
}
