using SurvLine.mdl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;
using static SurvLine.mdl.MdlNSDefine;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace SurvLine
{
    public abstract class CoordinatePoint
    {
        //==========================================================================================
        /*[VB]
        '*******************************************************************************
        '座標値

        Option Explicit
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'プロパティ
        Public X As Double 'X。
        Public Y As Double 'Y。
        Public Z As Double 'Z。
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'*******************************************************************************
        //'座標値

        //'プロパティ
        public double X;        //'X。
        public double Y;        //'Y。
        public double Z;        //'Z。
        //==========================================================================================

        //==========================================================================================
        //[C#]
        public abstract void XX(double nX);
        public abstract double XX();
        public abstract void YY(double nY);
        public abstract double YY();
        public abstract void ZZ(double nZ);
        public abstract double ZZ();
        //==========================================================================================

        //==========================================================================================
        /*[VB]
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
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '丸めX。
        Public Property Get RoundX() As Double
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        public abstract double RoundXX();
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '丸めY。
        Public Property Get RoundY() As Double
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        public abstract double RoundYY();
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '丸めZ。
        Public Property Get RoundZ() As Double
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        public abstract double RoundZZ();
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '座標値種別。
        Public Property Get CoordinateType() As COORDINATE_TYPE
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'座標値種別。
        public abstract COORDINATE_TYPE CoordinateType();
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
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '*******************************************************************************
        'メソッド

        '保存。
        '
        '引き数：
        'nFile ファイル番号。
        */
        public void Save(int nFile)
        {
            return;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '読み込み。
        '
        '引き数：
        'nFile ファイル番号。
        'nVersion ファイルバージョン。
        Public Sub Load(ByVal nFile As Integer, ByVal nVersion As Long)
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '読み込み。
        '
        '引き数：
        'nFile ファイル番号。
        'nVersion ファイルバージョン。
        */
        public abstract void Load(int nFile, long nVersion);
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '指定されたオブジェクトと比較する。
        '
        '引き数：
        'clsCoordinatePoint 比較対照オブジェクト。
        '
        '戻り値：
        '一致する場合 True を返す。
        'それ以外の場合 False を返す。
        Public Function Compare(ByVal clsCoordinatePoint As CoordinatePoint) As Boolean
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '指定されたオブジェクトと比較する。
        '
        '引き数：
        'clsCoordinatePoint 比較対照オブジェクト。
        '
        '戻り値：
        '一致する場合 True を返す。
        'それ以外の場合 False を返す。
        */
        public abstract bool Compare(CoordinatePoint clsCoordinatePoint);
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '地心直交座標を取得する。
        '
        '引き数：
        'nX X座標が設定される。
        'nY Y座標が設定される。
        'nZ Z座標が設定される。
        Public Sub GetXYZ(ByRef nX As Double, ByRef nY As Double, ByRef nZ As Double)
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '地心直交座標を取得する。
        '
        '引き数：
        'nX X座標が設定される。
        'nY Y座標が設定される。
        'nZ Z座標が設定される。
        */
        public abstract void GetXYZ(ref double nX, ref double nY, ref double nZ);
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '緯度経度(度)を取得する。
        '
        '引き数：
        'nLat 緯度(度)が設定される。
        'nLon 経度(度)が設定される。
        'nHeight 楕円体高が設定される。
        'vAlt 標高が設定される。標高が取得されない場合は null が設定される。
        'sGeoidoPath ジオイドモデルのパス。標高がOFFの場合、空文字を指定する。
        Public Sub GetDEG(ByRef nLat As Double, ByRef nLon As Double, ByRef nHeight As Double, ByRef vAlt As Variant, ByVal sGeoidoPath As String)
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '緯度経度(度)を取得する。
        '
        '引き数：
        'nLat 緯度(度)が設定される。
        'nLon 経度(度)が設定される。
        'nHeight 楕円体高が設定される。
        'vAlt 標高が設定される。標高が取得されない場合は null が設定される。
        'sGeoidoPath ジオイドモデルのパス。標高がOFFの場合、空文字を指定する。
        */
        public abstract void GetDEG(ref double nLat, ref double nLon, ref double nHeight, ref double vAlt, string sGeoidoPath);
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '緯度経度(度分秒)を取得する。
        '
        '引き数：
        'nLatH 緯度(度)が設定される。
        'nLatM 緯度(分)が設定される。
        'nLatS 緯度(秒)が設定される。
        'nLonH 経度(度)が設定される。
        'nLonM 経度(分)が設定される。
        'nLonS 経度(秒)が設定される。
        'nHeight 楕円体高が設定される。
        'vAlt 標高が設定される。標高が取得されない場合は null が設定される。
        'nDecimal 秒の四捨五入桁。
        'sGeoidoPath ジオイドモデルのパス。標高がOFFの場合、空文字を指定する。
        Public Sub GetDMS(ByRef nLatH As Long, ByRef nLatM As Long, ByRef nLatS As Double, ByRef nLonH As Long, ByRef nLonM As Long, ByRef nLonS As Double, ByRef nHeight As Double, ByRef vAlt As Variant, ByVal nDecimal As Long, ByVal sGeoidoPath As String)
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '緯度経度(度分秒)を取得する。
        '
        '引き数：
        'nLatH 緯度(度)が設定される。
        'nLatM 緯度(分)が設定される。
        'nLatS 緯度(秒)が設定される。
        'nLonH 経度(度)が設定される。
        'nLonM 経度(分)が設定される。
        'nLonS 経度(秒)が設定される。
        'nHeight 楕円体高が設定される。
        'vAlt 標高が設定される。標高が取得されない場合は null が設定される。
        'nDecimal 秒の四捨五入桁。
        'sGeoidoPath ジオイドモデルのパス。標高がOFFの場合、空文字を指定する。
        */
        public abstract void GetDMS(ref long nLatH, ref long nLatM, ref double nLatS, ref long nLonH, ref long nLonM, ref double nLonS, ref double nHeight,
            ref double vAlt, long nDecimal, string sGeoidoPath);
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '平面直角座標を取得する。
        '
        '引き数：
        'nX X座標が設定される。
        'nY Y座標が設定される。
        'nHeight 楕円体高が設定される。
        'vAlt 標高が設定される。標高が取得されない場合は null が設定される。
        'nDecimal 秒の四捨五入桁。
        'nCoordNum 座標系番号(1～19)。
        'sGeoidoPath ジオイドモデルのパス。標高がOFFの場合、空文字を指定する。
        Public Sub GetJGD(ByRef nX As Double, ByRef nY As Double, ByRef nHeight As Double, ByRef vAlt As Variant, ByVal nDecimal As Long, ByVal nCoordNum As Long, ByVal sGeoidoPath As String)
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '平面直角座標を取得する。
        '
        '引き数：
        'nX X座標が設定される。
        'nY Y座標が設定される。
        'nHeight 楕円体高が設定される。
        'vAlt 標高が設定される。標高が取得されない場合は null が設定される。
        'nDecimal 秒の四捨五入桁。
        'nCoordNum 座標系番号(1～19)。
        'sGeoidoPath ジオイドモデルのパス。標高がOFFの場合、空文字を指定する。
        */
        public abstract void GetJGD(ref double nX, ref double nY, ref double nHeight, ref double vAlt, long nDecimal, long nCoordNum, string sGeoidoPath);
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'コピーしたオブジェクトを生成して返す。
        '
        '戻り値：コピーしたオブジェクト。
        Public Function CreateCopy() As CoordinatePoint
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        'コピーしたオブジェクトを生成して返す。
        '
        '戻り値：コピーしたオブジェクト。
        */
        public abstract CoordinatePoint CreateCopy();
        //==========================================================================================
    }
}
