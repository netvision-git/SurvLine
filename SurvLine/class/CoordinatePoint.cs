using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace SurvLine
{
    internal class CoordinatePoint
    {

        //'*******************************************************************************
        //'座標値
        //
        //Option Explicit

        //'プロパティ
        public double X;        // As Double 'X。
        public double Y;        // As Double 'Y。
        public double Z;        // As Double 'Z。

#if false
'*******************************************************************************
'プロパティ

'座標値。
'
'このプロパティはデフォルトプロパティである。Letを使えば｢値の代入｣ができる。いわゆるC++で言うところのoperator=である。
'
'引き数：
'clsCoordinatePoint コピー元のオブジェクト。
Public Property Let CoordinatePoint(ByVal clsCoordinatePoint As CoordinatePoint)
End Property

'丸めX。
Public Property Get RoundX() As Double
End Property

'丸めY。
Public Property Get RoundY() As Double
End Property

'丸めZ。
Public Property Get RoundZ() As Double
End Property

'座標値種別。
Public Property Get CoordinateType() As COORDINATE_TYPE
End Property

#endif

        //'*******************************************************************************
        //'*******************************************************************************
        /// <summary>
        /// メソッド
        ///
        /// 保存。
        ///
        /// 引き数：
        ///     bw バイナリファイル
        /// </summary>
        /// <param name="bw"></param>
        public void Save(BinaryWriter bw)
        {
        }
        //--------------------------------------------------------------------------------
        //'メソッド
        //
        //'保存。
        //'
        //'引き数：
        //'nFile ファイル番号。
        //Public Sub Save(ByVal nFile As Integer)
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
        }
        //--------------------------------------------------------------------------------
        //'読み込み。
        //'
        //'引き数：
        //'nFile ファイル番号。
        //'nVersion ファイルバージョン。
        //Public Sub Load(ByVal nFile As Integer, ByVal nVersion As Long)
        //End Sub
        //'*******************************************************************************
        //'*******************************************************************************

        //'*******************************************************************************
        //'*******************************************************************************
        /// <summary>
        //'指定されたオブジェクトと比較する。
        //'
        //'引き数：
        //'clsCoordinatePoint 比較対照オブジェクト。
        /// </summary>
        /// <param name="clsCoordinatePoint"></param>
        /// <returns>
        /// 戻り値：
        ///     一致する場合 True を返す。
        ///     それ以外の場合 False を返す。
        /// </returns>
        public bool Compare(CoordinatePoint clsCoordinatePoint)
        {
            return true;
        }
        //---------------------------------------------------------------------------------
        //'指定されたオブジェクトと比較する。
        //'
        //'引き数：
        //'clsCoordinatePoint 比較対照オブジェクト。
        //'
        //'戻り値：
        //'一致する場合 True を返す。
        //'それ以外の場合 False を返す。
        //Public Function Compare(ByVal clsCoordinatePoint As CoordinatePoint) As Boolean
        //End Function
        //'*******************************************************************************
        //'*******************************************************************************


        //'*******************************************************************************
        //'*******************************************************************************
        /// <summary>
        /// 地心直交座標を取得する。
        ///
        /// 引き数：
        ///     nX X座標が設定される。
        ///     nY Y座標が設定される。
        ///     nZ Z座標が設定される。
        /// </summary>
        /// <param name="nX"></param>
        /// <param name="nY"></param>
        /// <param name="nZ"></param>
        public void GetXYZ(double nX, double nY, double nZ)
        {
        }
        //'地心直交座標を取得する。
        //'
        //'引き数：
        //'nX X座標が設定される。
        //'nY Y座標が設定される。
        //'nZ Z座標が設定される。
        //Public Sub GetXYZ(ByRef nX As Double, ByRef nY As Double, ByRef nZ As Double)
        //End Sub
        //'*******************************************************************************
        //'*******************************************************************************

        /// <summary>
        /// 緯度経度(度)を取得する。
        ///
        /// 引き数：
        //      nLat 緯度(度)が設定される。
        ///     nLon 経度(度)が設定される。
        ///     nHeight 楕円体高が設定される。
        ///     vAlt 標高が設定される。標高が取得されない場合は null が設定される。
        //'sGeoidoPath ジオイドモデルのパス。標高がOFFの場合、空文字を指定する。
        /// </summary>
        /// <param name="nLat"></param>
        /// <param name="nLon"></param>
        /// <param name="nHeight"></param>
        /// <param name="vAlt"></param>
        /// <param name="sGeoidoPath"></param>
        public void GetDEG(double nLat, double nLon, double nHeight, double vAlt, string sGeoidoPath)
        {
        }
        //---------------------------------------------------------------------------------
        //'緯度経度(度)を取得する。
        //'
        //'引き数：
        //'nLat 緯度(度)が設定される。
        //'nLon 経度(度)が設定される。
        //'nHeight 楕円体高が設定される。
        //'vAlt 標高が設定される。標高が取得されない場合は null が設定される。
        //'sGeoidoPath ジオイドモデルのパス。標高がOFFの場合、空文字を指定する。
        //Public Sub GetDEG(ByRef nLat As Double, ByRef nLon As Double, ByRef nHeight As Double, ByRef vAlt As Variant, ByVal sGeoidoPath As String)
        //End Sub
        //'*******************************************************************************
        //'*******************************************************************************


        //'*******************************************************************************
        //'*******************************************************************************
        /// <summary>
        /// 緯度経度(度分秒)を取得する。
        ///
        /// 引き数：
        ///     nLatH 緯度(度)が設定される。
        ///     nLatM 緯度(分)が設定される。
        ///     nLatS 緯度(秒)が設定される。
        ///     nLonH 経度(度)が設定される。
        ///     nLonM 経度(分)が設定される。
        ///     nLonS 経度(秒)が設定される。
        ///     nHeight 楕円体高が設定される。
        ///     vAlt 標高が設定される。標高が取得されない場合は null が設定される。
        ///     nDecimal 秒の四捨五入桁。
        ///     sGeoidoPath ジオイドモデルのパス。標高がOFFの場合、空文字を指定する。
        /// </summary>
        /// <param name="nLatH"></param>
        /// <param name="nLatM"></param>
        /// <param name="nLatS"></param>
        /// <param name="nLonH"></param>
        /// <param name="nLonM"></param>
        /// <param name="nLonS"></param>
        /// <param name="nHeight"></param>
        /// <param name="vAlt"></param>
        /// <param name="nDecimal"></param>
        /// <param name="sGeoidoPath"></param>
        public void GetDMS(long nLatH, long nLatM, double nLatS, long nLonH, long nLonM, double nLonS, double nHeight, double vAlt, long nDecimal, string sGeoidoPath)
        {
        }
        //---------------------------------------------------------------------------------
        //'緯度経度(度分秒)を取得する。
        //'
        //'引き数：
        //'nLatH 緯度(度)が設定される。
        //'nLatM 緯度(分)が設定される。
        //'nLatS 緯度(秒)が設定される。
        //'nLonH 経度(度)が設定される。
        //'nLonM 経度(分)が設定される。
        //'nLonS 経度(秒)が設定される。
        //'nHeight 楕円体高が設定される。
        //'vAlt 標高が設定される。標高が取得されない場合は null が設定される。
        //'nDecimal 秒の四捨五入桁。
        //'sGeoidoPath ジオイドモデルのパス。標高がOFFの場合、空文字を指定する。
        //Public Sub GetDMS(ByRef nLatH As Long, ByRef nLatM As Long, ByRef nLatS As Double, ByRef nLonH As Long, ByRef nLonM As Long, ByRef nLonS As Double, ByRef nHeight As Double, ByRef vAlt As Variant, ByVal nDecimal As Long, ByVal sGeoidoPath As String)
        //End Sub
        //'*******************************************************************************
        //'*******************************************************************************


        //'*******************************************************************************
        //'*******************************************************************************
        /// <summary>
        /// 緯度経度(度分秒)を取得する。
        ///
        /// 引き数：
        ///     nLatH 緯度(度)が設定される。
        ///     nLatM 緯度(分)が設定される。
        ///     nLatS 緯度(秒)が設定される。
        ///     nLonH 経度(度)が設定される。
        ///     nLonM 経度(分)が設定される。
        ///     nLonS 経度(秒)が設定される。
        ///     nHeight 楕円体高が設定される。
        ///     vAlt 標高が設定される。標高が取得されない場合は null が設定される。
        ///     nDecimal 秒の四捨五入桁。
        ///     sGeoidoPath ジオイドモデルのパス。標高がOFFの場合、空文字を指定する。
        /// </summary>
        /// <param name="nX"></param>
        /// <param name="nY"></param>
        /// <param name="nHeight"></param>
        /// <param name="vAlt"></param>
        /// <param name="nDecimal"></param>
        /// <param name="nCoordNum"></param>
        /// <param name="sGeoidoPath"></param>
        public void GetJGD(double nX, double nY, double nHeight, double vAlt, long nDecimal, long nCoordNum, string sGeoidoPath)
        {
        }
        //---------------------------------------------------------------------------------
        //'平面直角座標を取得する。
        //'
        //'引き数：
        //'nX X座標が設定される。
        //'nY Y座標が設定される。
        //'nHeight 楕円体高が設定される。
        //'vAlt 標高が設定される。標高が取得されない場合は null が設定される。
        //'nDecimal 秒の四捨五入桁。
        //'nCoordNum 座標系番号(1～19)。
        //'sGeoidoPath ジオイドモデルのパス。標高がOFFの場合、空文字を指定する。
        //Public Sub GetJGD(ByRef nX As Double, ByRef nY As Double, ByRef nHeight As Double, ByRef vAlt As Variant, ByVal nDecimal As Long, ByVal nCoordNum As Long, ByVal sGeoidoPath As String)
        //End Sub
        //'*******************************************************************************
        //'*******************************************************************************

        //'*******************************************************************************
        //'*******************************************************************************
        /// <summary>
        /// コピーしたオブジェクトを生成して返す。
        ///
        /// </summary>
        /// <returns>
        ///     戻り値：コピーしたオブジェクト。
        /// </returns>
        public CoordinatePoint CreateCopy()
        {
            return new CoordinatePoint();
        }
        //---------------------------------------------------------------------------------
        //'コピーしたオブジェクトを生成して返す。
        //'
        //'戻り値：コピーしたオブジェクト。
        //Public Function CreateCopy() As CoordinatePoint
        //End Function
        //'*******************************************************************************
        //'*******************************************************************************


    }
}
