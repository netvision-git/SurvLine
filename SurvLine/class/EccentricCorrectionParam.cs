using SurvLine.mdl;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Net.WebRequestMethods;
using static SurvLine.mdl.mdlEccentricCorrection;
using static SurvLine.mdl.MdlNSDefine;
using System.Drawing.Drawing2D;
using System.Security.Cryptography;
using static SurvLine.mdl.DEFINE;
using static SurvLine.mdl.MdlNSDefine;
using static SurvLine.mdl.MdlUtility;
using static SurvLine.mdl.MdlAccountMake;
using static SurvLine.mdl.MdlNSGUI;
using static SurvLine.mdl.MdlNSSDefine;


namespace SurvLine
{
    public class EccentricCorrectionParam
    {
        ////Document document = new Document();

        public EccentricCorrectionParam(MdlMain mdlMain)
        {
            this.mdlMain = mdlMain;
            document = mdlMain.GetDocument();
        }

        private Document document;
        private MdlMain mdlMain;


        //==========================================================================================
        /*[VB]
        '*******************************************************************************
        '偏心補正パラメータ

        Option Explicit
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'プロパティ
        Public AngleType As 
        '角度種別。
        Public Marker As String '方位標(基線ベクトルのキー)。
        Public UsePoint As String '偏心に使用する基線(基線ベクトルのキー)。'2009/11 H.Nakamura
        Public Horizontal As Double '水平角(ラジアン)。
        Public Direction As Double '方位角(ラジアン)。
        Public Distance As Double '斜距離(ｍ)。
        Public UseTS As Boolean '測距儀を使用。
        Public FromHeightTS As Double '測距儀高(ｍ)。
        Public ToHeightTS As Double '反射鏡高(ｍ)。
        Public ElevationBC As Double '高度角B→C(ラジアン)。
        Public FromHeightBC As Double '器械高B→C(ｍ)。
        Public ToHeightBC As Double '目標高B→C(ｍ)。
        Public ElevationCB As Double '高度角C→B(ラジアン)。
        Public FromHeightCB As Double '器械高C→B(ｍ)。
        Public ToHeightCB As Double '目標高C→B(ｍ)。

        '2007/3/15 追加 NGS Yamada
        Public MarkNumber As String  '手入力時の方位標のNo
        Public MarkName As String  '手入力時の方位標の名称
        Public MarkEditCode As EDITCODE_STYLE '編集コード。0の時はデフォルト。
        Public MarkLat As Double '緯度(度)。
        Public MarkLon As Double '経度(度)。
        Public MarkX As Double '平面直角X(ｍ)。
        Public MarkY As Double '平面直角Y(ｍ)。
        Public MarkHeight As Double '楕円体高(ｍ)。
        Public MarkAlt As Double    '標高(ｍ)。
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'プロパティ
        //public ANGLE_TYPE AngleType;            //'角度種別。
        public int AngleType;                   //'角度種別。
        public string Marker;                   //'方位標(基線ベクトルのキー)。
        public string UsePoint;                 //'偏心に使用する基線(基線ベクトルのキー)。//'2009/11 H.Nakamura
        public double Horizontal;               //'水平角(ラジアン)。
        public double Direction;                //'方位角(ラジアン)。
        public double Distance;                 //'斜距離(ｍ)。
        public bool UseTS;                      //'測距儀を使用。
        public double FromHeightTS;             //'測距儀高(ｍ)。
        public double ToHeightTS;               //'反射鏡高(ｍ)。
        public double ElevationBC;              //'高度角B→C(ラジアン)。
        public double FromHeightBC;             //'器械高B→C(ｍ)。
        public double ToHeightBC;               //'目標高B→C(ｍ)。
        public double ElevationCB;              //'高度角C→B(ラジアン)。
        public double FromHeightCB;             //'器械高C→B(ｍ)。
        public double ToHeightCB;               //'目標高C→B(ｍ)。

        //'2007/3/15 追加 NGS Yamada
        public string MarkNumber;               //'手入力時の方位標のNo
        public string MarkName;                 //'手入力時の方位標の名称
        //public EDITCODE_STYLE MarkEditCode;     //'編集コード。0の時はデフォルト。
        public int MarkEditCode;                //'編集コード。0の時はデフォルト。
        public double MarkLat;                  //'緯度(度)。
        public double MarkLon;                  //'経度(度)。
        public double MarkX;                    //'平面直角X(ｍ)。
        public double MarkY;                    //'平面直角Y(ｍ)。
        public double MarkHeight;               //'楕円体高(ｍ)。
        public double MarkAlt;                  //'標高(ｍ)。
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '*******************************************************************************
        'プロパティ

        '偏心補正パラメータ。
        '
        'このプロパティはデフォルトプロパティである。Letを使えば｢値の代入｣ができる。いわゆるC++で言うところのoperator=である。
        '
        '引き数：
        'clsEccentricCorrectionParam コピー元のオブジェクト。
        Property Let EccentricCorrectionParam(ByVal clsEccentricCorrectionParam As EccentricCorrectionParam)

            AngleType = clsEccentricCorrectionParam.AngleType
            Marker = clsEccentricCorrectionParam.Marker
            UsePoint = clsEccentricCorrectionParam.UsePoint '2009/11 H.Nakamura
            Horizontal = clsEccentricCorrectionParam.Horizontal
            Direction = clsEccentricCorrectionParam.Direction
            Distance = clsEccentricCorrectionParam.Distance
            UseTS = clsEccentricCorrectionParam.UseTS
            FromHeightTS = clsEccentricCorrectionParam.FromHeightTS
            ToHeightTS = clsEccentricCorrectionParam.ToHeightTS
            ElevationBC = clsEccentricCorrectionParam.ElevationBC
            FromHeightBC = clsEccentricCorrectionParam.FromHeightBC
            ToHeightBC = clsEccentricCorrectionParam.ToHeightBC
            ElevationCB = clsEccentricCorrectionParam.ElevationCB
            FromHeightCB = clsEccentricCorrectionParam.FromHeightCB
            ToHeightCB = clsEccentricCorrectionParam.ToHeightCB
    
            '2007/3/15 追加 NGS Yamada
            MarkNumber = clsEccentricCorrectionParam.MarkNumber
            MarkName = clsEccentricCorrectionParam.MarkName
            MarkEditCode = clsEccentricCorrectionParam.MarkEditCode
            MarkLat = clsEccentricCorrectionParam.MarkLat
            MarkLon = clsEccentricCorrectionParam.MarkLon
            MarkX = clsEccentricCorrectionParam.MarkX
            MarkY = clsEccentricCorrectionParam.MarkY
            MarkHeight = clsEccentricCorrectionParam.MarkHeight
            MarkAlt = clsEccentricCorrectionParam.MarkAlt
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '*******************************************************************************
        'イベント

        '初期化。
        Private Sub Class_Initialize()

            On Error GoTo ErrorHandler
    
            AngleType = ANGLETYPE_HORIZONTAL
            Marker = ""
            UsePoint = "" '2009/11 H.Nakamura
            Horizontal = 0
            Direction = 0
            Distance = 0
            UseTS = False
            FromHeightTS = 0
            ToHeightTS = 0
            ElevationBC = 0
            FromHeightBC = 0
            ToHeightBC = 0
            ElevationCB = 0
            FromHeightCB = 0
            ToHeightCB = 0
            MarkNumber = ""
            MarkName = ""
            MarkEditCode = 0
            MarkLat = 0
            MarkLon = 0
            MarkX = 0
            MarkY = 0
            MarkHeight = 0
            MarkAlt = 0
    
            Exit Sub
    
        ErrorHandler:
            Call mdlMain.ErrorExit
    
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '*******************************************************************************
        'メソッド

        '保存。
        '
        '引き数：
        'nFile ファイル番号。
        Public Sub Save(ByVal nFile As Integer)

            Put #nFile, , AngleType
            Call PutString(nFile, Marker)
            Put #nFile, , Horizontal
            Put #nFile, , Direction
            Put #nFile, , Distance
            Put #nFile, , UseTS
            Put #nFile, , FromHeightTS
            Put #nFile, , ToHeightTS
            Put #nFile, , ElevationBC
            Put #nFile, , FromHeightBC
            Put #nFile, , ToHeightBC
            Put #nFile, , ElevationCB
            Put #nFile, , FromHeightCB
            Put #nFile, , ToHeightCB
    
            '2007/3/15 追加 NGS Yamada
            Call PutString(nFile, MarkNumber)
            Call PutString(nFile, MarkName)
            Put #nFile, , MarkEditCode
            Put #nFile, , MarkLat
            Put #nFile, , MarkLon
            Put #nFile, , MarkX
            Put #nFile, , MarkY
            Put #nFile, , MarkHeight
            Put #nFile, , MarkAlt
    
            '2009/11 H.Nakamura
            Call PutString(nFile, UsePoint)
    
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '読み込み。
        '
        '引き数：
        'nFile ファイル番号。
        'nVersion ファイルバージョン。
        Public Sub Load(ByVal nFile As Integer, ByVal nVersion As Long)
            Dim clsObservationPoints() As ObservationPoint

            Get #nFile, , AngleType
            Marker = GetString(nFile)
            Get #nFile, , Horizontal
            Get #nFile, , Direction
            Get #nFile, , Distance
            Get #nFile, , UseTS
            Get #nFile, , FromHeightTS
            Get #nFile, , ToHeightTS
            Get #nFile, , ElevationBC
            Get #nFile, , FromHeightBC
            Get #nFile, , ToHeightBC
            Get #nFile, , ElevationCB
            Get #nFile, , FromHeightCB
            Get #nFile, , ToHeightCB
    
            If nVersion < 7800 Then
            Else
                MarkNumber = GetString(nFile)
                MarkName = GetString(nFile)
                Get #nFile, , MarkEditCode
                Get #nFile, , MarkLat
                Get #nFile, , MarkLon
                Get #nFile, , MarkX
                Get #nFile, , MarkY
                Get #nFile, , MarkHeight
                Get #nFile, , MarkAlt
            End If
    
            '2009/11 H.Nakamura
            If nVersion < 8201 Then
                UsePoint = ""
            Else
                UsePoint = GetString(nFile)
            End If
    
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
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
            AngleType = Genba_S.AngleType;
            //-----------------------------------
            //[VB]  Marker = GetString(nFile)   public string Marker;               //As String '方位標(基線ベクトルのキー)。0
            Genba_S.Marker = FileRead_GetString(br);
            Marker = Genba_S.Marker;
            //-----------------------------------
            //[VB]  Get #nFile, , Horizontal    public double Horizontal;           //As Double '水平角(ラジアン)。         0
            Genba_S.Horizontal = br.ReadDouble();
            Horizontal = Genba_S.Horizontal;
            //-----------------------------------
            //[VB]  Get #nFile, , Direction     public double Direction;            //As Double '方位角(ラジアン)。         0
            Genba_S.Direction = br.ReadDouble();
            Direction = Genba_S.Direction;
            //-----------------------------------
            //[VB]  Get #nFile, , Distance      public double Distance;             //As Double '斜距離(ｍ)。                0
            Genba_S.Distance = br.ReadDouble();
            Distance = Genba_S.Distance;
            //-----------------------------------
            //[VB]  Get #nFile, , UseTS         public bool UseTS;                  //As Boolean '測距儀を使用。           0
            Genba_S.UseTS = document.GetFileBool(br);
            UseTS = Genba_S.UseTS;
            //-----------------------------------
            //[VB]  Get #nFile, , FromHeightTS  public double FromHeightTS;         //As Double '測距儀高(ｍ)。           0
            Genba_S.FromHeightTS = br.ReadDouble();
            FromHeightTS = Genba_S.FromHeightTS;
            //-----------------------------------
            //[VB]  Get #nFile, , ToHeightTS    public double ToHeightTS;           //As Double '反射鏡高(ｍ)。           0
            Genba_S.ToHeightTS = br.ReadDouble();
            ToHeightTS = Genba_S.ToHeightTS;
            //-----------------------------------
            //[VB]  Get #nFile, , ElevationBC   public double ElevationBC;          //As Double '高度角B→C(ラジアン)。  0
            Genba_S.ElevationBC = br.ReadDouble();
            ElevationBC = Genba_S.ElevationBC;
            //-----------------------------------
            //[VB]  Get #nFile, , FromHeightBC  public double FromHeightBC;         //As Double '器械高B→C(ｍ)。         0
            Genba_S.FromHeightBC = br.ReadDouble();
            FromHeightBC = Genba_S.FromHeightBC;
            //-----------------------------------
            //[VB]  Get #nFile, , ToHeightBC    public double ToHeightBC;           //As Double '目標高B→C(ｍ)。         0
            Genba_S.ToHeightBC = br.ReadDouble();
            ToHeightBC = Genba_S.ToHeightBC;
            //-----------------------------------
            //[VB]  Get #nFile, , ElevationCB   public double ElevationCB;          //As Double '高度角C→B(ラジアン)。      0     
            Genba_S.ElevationCB = br.ReadDouble();
            ElevationCB = Genba_S.ElevationCB;
            //-----------------------------------
            //[VB]  Get #nFile, , FromHeightCB  public double FromHeightCB;         //As Double '器械高C→B(ｍ)。         0
            Genba_S.FromHeightCB = br.ReadDouble();
            FromHeightCB = Genba_S.FromHeightCB;
            //-----------------------------------
            //[VB]  Get #nFile, , ToHeightCB     public double ToHeightCB;           //As Double '目標高C→B(ｍ)。        0
            Genba_S.ToHeightCB = br.ReadDouble();
            ToHeightCB = Genba_S.ToHeightCB;
            //-----------------------------------
            //-----------------------------------
            if (nVersion < 7800) {
            }
            else
            {
                //-----------------------------------
                //[VB]  MarkNumber = GetString(nFile)   public string MarkNumber;           //As String  '手入力時の方位標のNo   ""
                Genba_S.MarkNumber = FileRead_GetString(br);
                MarkNumber = Genba_S.MarkNumber;
                //-----------------------------------
                //[VB]  MarkName = GetString(nFile)     public string MarkName;             //As String  '手入力時の方位標の名称   ""
                Genba_S.MarkName = FileRead_GetString(br);
                MarkName = Genba_S.MarkName;
                //-----------------------------------
                //[VB]  Get #nFile, , MarkEditCode      public int MarkEditCode;            //As EDITCODE_STYLE '編集コード。0の時はデフォルト。   0
                Genba_S.MarkEditCode = br.ReadInt32();
                MarkEditCode = Genba_S.MarkEditCode;
                //-----------------------------------
                //[VB]  Get #nFile, , MarkLat           public double MarkLat;              //As Double '緯度(度)。             0
                Genba_S.MarkLat = br.ReadDouble();
                MarkLat = Genba_S.MarkLat;
                //-----------------------------------
                //[VB]  Get #nFile, , MarkLon           public double MarkLon;              //As Double '経度(度)。             0
                Genba_S.MarkLon = br.ReadDouble();
                MarkLon = Genba_S.MarkLon;
                //-----------------------------------
                //[VB]  Get #nFile, , MarkX             public double MarkX;                //As Double '平面直角X(ｍ)。          0
                Genba_S.MarkX = br.ReadDouble();
                MarkX = Genba_S.MarkX;
                //-----------------------------------
                //[VB]  Get #nFile, , MarkY             public double MarkY;                //As Double '平面直角Y(ｍ)。          0
                Genba_S.MarkY = br.ReadDouble();
                MarkY = Genba_S.MarkY;
                //-----------------------------------
                //[VB]  Get #nFile, , MarkHeight        public double MarkHeight;           //As Double '楕円体高(ｍ)。           0
                Genba_S.MarkHeight = br.ReadDouble();
                MarkHeight = Genba_S.MarkHeight;
                //-----------------------------------
                //[VB]  Get #nFile, , MarkAlt           public double MarkAlt;              //As Double    '標高(ｍ)。          0
                Genba_S.MarkAlt = br.ReadDouble();
                MarkAlt = Genba_S.MarkAlt;
                //-----------------------------------
            }
            //-----------------------------------
            // '2009/11 H.Nakamura
            if (nVersion < 8201)
            {
                //UsePoint = ""; Public UsePoint As String '偏心に使用する基線(基線ベクトルのキー)。'2009 / 11 H.Nakamura
                Genba_S.UsePoint = "";
                UsePoint = Genba_S.UsePoint;
            }
            else
            {
                //UsePoint = GetString(nFile);    Public UsePoint As String '偏心に使用する基線(基線ベクトルのキー)。'2009 / 11 H.Nakamura 0
                Genba_S.UsePoint = FileRead_GetString(br);
                UsePoint = Genba_S.UsePoint;
            }

        }
        //==========================================================================================
            
        //==========================================================================================
        /*[VB]
        '指定されたオブジェクトと比較する。
        '
        '引き数：
        'clsEccentricCorrectionParam 比較対照オブジェクト。
        '
        '戻り値：
        '一致する場合 True を返す。
        'それ以外の場合 False を返す。
        Public Function Compare(ByVal clsEccentricCorrectionParam As EccentricCorrectionParam) As Boolean

            Compare = False
    
            If AngleType <> clsEccentricCorrectionParam.AngleType Then Exit Function
            If Marker <> clsEccentricCorrectionParam.Marker Then Exit Function
            If UsePoint <> clsEccentricCorrectionParam.UsePoint Then Exit Function '2009/11 H.Nakamura
            If FLT_EPSILON <= Abs(Horizontal - clsEccentricCorrectionParam.Horizontal) Then Exit Function
            If FLT_EPSILON <= Abs(Direction - clsEccentricCorrectionParam.Direction) Then Exit Function
            If FLT_EPSILON <= Abs(Distance - clsEccentricCorrectionParam.Distance) Then Exit Function
            If UseTS <> clsEccentricCorrectionParam.UseTS Then Exit Function
            If FLT_EPSILON <= Abs(FromHeightTS - clsEccentricCorrectionParam.FromHeightTS) Then Exit Function
            If FLT_EPSILON <= Abs(ToHeightTS - clsEccentricCorrectionParam.ToHeightTS) Then Exit Function
            If FLT_EPSILON <= Abs(ElevationBC - clsEccentricCorrectionParam.ElevationBC) Then Exit Function
            If FLT_EPSILON <= Abs(FromHeightBC - clsEccentricCorrectionParam.FromHeightBC) Then Exit Function
            If FLT_EPSILON <= Abs(ToHeightBC - clsEccentricCorrectionParam.ToHeightBC) Then Exit Function
            If FLT_EPSILON <= Abs(ElevationCB - clsEccentricCorrectionParam.ElevationCB) Then Exit Function
            If FLT_EPSILON <= Abs(FromHeightCB - clsEccentricCorrectionParam.FromHeightCB) Then Exit Function
            If FLT_EPSILON <= Abs(ToHeightCB - clsEccentricCorrectionParam.ToHeightCB) Then Exit Function
    
            '2007/3/15 追加 NGS Yamada
            If MarkNumber <> clsEccentricCorrectionParam.MarkNumber Then Exit Function
            If MarkName <> clsEccentricCorrectionParam.MarkName Then Exit Function
            If MarkEditCode <> clsEccentricCorrectionParam.MarkEditCode Then Exit Function
            If Abs(MarkLat - clsEccentricCorrectionParam.MarkLat) >= FLT_EPSILON Then Exit Function
            If Abs(MarkLon - clsEccentricCorrectionParam.MarkLon) >= FLT_EPSILON Then Exit Function
            If Abs(MarkX - clsEccentricCorrectionParam.MarkX) >= FLT_EPSILON Then Exit Function
            If Abs(MarkY - clsEccentricCorrectionParam.MarkY) >= FLT_EPSILON Then Exit Function
            If Abs(MarkHeight - clsEccentricCorrectionParam.MarkHeight) >= FLT_EPSILON Then Exit Function
            If Abs(MarkAlt - clsEccentricCorrectionParam.MarkAlt) >= FLT_EPSILON Then Exit Function
    
            Compare = True
    
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '指定されたオブジェクトと比較する。
        '
        '引き数：
        'clsEccentricCorrectionParam 比較対照オブジェクト。
        '
        '戻り値：
        '一致する場合 True を返す。
        'それ以外の場合 False を返す。
        */
        public bool Compare(EccentricCorrectionParam clsEccentricCorrectionParam)
        {
            if (AngleType != clsEccentricCorrectionParam.AngleType){ return false; }
            if (Marker != clsEccentricCorrectionParam.Marker) { return false; }
            if (UsePoint != clsEccentricCorrectionParam.UsePoint){ return false; }                              //'2009/11 H.Nakamura
            if (FLT_EPSILON <= Math.Abs(Horizontal - clsEccentricCorrectionParam.Horizontal)) { return false; }
            if (FLT_EPSILON <= Math.Abs(Direction - clsEccentricCorrectionParam.Direction)) { return false; }
            if (FLT_EPSILON <= Math.Abs(Distance - clsEccentricCorrectionParam.Distance)) { return false; }
            if (UseTS != clsEccentricCorrectionParam.UseTS) { return false; }
            if (FLT_EPSILON <= Math.Abs(FromHeightTS - clsEccentricCorrectionParam.FromHeightTS)) { return false; }
            if (FLT_EPSILON <= Math.Abs(ToHeightTS - clsEccentricCorrectionParam.ToHeightTS)) { return false; }
            if (FLT_EPSILON <= Math.Abs(ElevationBC - clsEccentricCorrectionParam.ElevationBC)) { return false; }
            if (FLT_EPSILON <= Math.Abs(FromHeightBC - clsEccentricCorrectionParam.FromHeightBC)) { return false; }
            if (FLT_EPSILON <= Math.Abs(ToHeightBC - clsEccentricCorrectionParam.ToHeightBC)) { return false; }
            if (FLT_EPSILON <= Math.Abs(ElevationCB - clsEccentricCorrectionParam.ElevationCB)) { return false; }
            if (FLT_EPSILON <= Math.Abs(FromHeightCB - clsEccentricCorrectionParam.FromHeightCB)) { return false; }
            if (FLT_EPSILON <= Math.Abs(ToHeightCB - clsEccentricCorrectionParam.ToHeightCB)) { return false; }

            //'2007/3/15 追加 NGS Yamada
            if (MarkNumber != clsEccentricCorrectionParam.MarkNumber) { return false; }
            if (MarkName != clsEccentricCorrectionParam.MarkName) { return false; }
            if (MarkEditCode != clsEccentricCorrectionParam.MarkEditCode) { return false; }
            if (Math.Abs(MarkLat - clsEccentricCorrectionParam.MarkLat) >= FLT_EPSILON) { return false; }
            if (Math.Abs(MarkLon - clsEccentricCorrectionParam.MarkLon) >= FLT_EPSILON) { return false; }
            if (Math.Abs(MarkX - clsEccentricCorrectionParam.MarkX) >= FLT_EPSILON) { return false; }
            if (Math.Abs(MarkY - clsEccentricCorrectionParam.MarkY) >= FLT_EPSILON) { return false; }
            if (Math.Abs(MarkHeight - clsEccentricCorrectionParam.MarkHeight) >= FLT_EPSILON) { return false; }
            if (Math.Abs(MarkAlt - clsEccentricCorrectionParam.MarkAlt) >= FLT_EPSILON) { return false; }

            return true;

        }
        //==========================================================================================
    }
}
