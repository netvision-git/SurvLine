using SurvLine.mdl;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Remoting;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static SurvLine.mdl.MdlNSDefine;
using static SurvLine.mdl.MdlUtility;
using static SurvLine.mdl.MdlAccountMake;
using static System.Net.WebRequestMethods;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using Microsoft.VisualBasic;

namespace SurvLine
{
    public class CoordinatePointXYZ : CoordinatePoint
    {

        //==========================================================================================
        /*[VB]
        '*******************************************************************************
        'XYZ座標値

        Option Explicit
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //==========================================================================================

        //==========================================================================================
        //[C#]
        public CoordinatePointXYZ()
        {
            Class_Initialize();
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        Implements CoordinatePoint

        'インプリメンテーション
        Private m_nX As Double 'X。
        Private m_nY As Double 'Y。
        Private m_nZ As Double 'Z。
        Private m_nRoundX As Double '丸めX。
        Private m_nRoundY As Double '丸めY。
        Private m_nRoundZ As Double '丸めZ。
        Private m_nCoordinateType As COORDINATE_TYPE '座標値種別。
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'インプリメンテーション
        private double m_nX;                        //'X。
        private double m_nY;                        //'Y。
        private double m_nZ;                        //'Z。
        private double m_nRoundX;                   //'丸めX。
        private double m_nRoundY;                   //'丸めY。
        private double m_nRoundZ;                   //'丸めZ。
        private COORDINATE_TYPE m_nCoordinateType;  //'座標値種別。

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
        Public Property Let CoordinatePointXYZ(ByVal clsCoordinatePoint As CoordinatePoint)
            Let CoordinatePoint_CoordinatePoint = clsCoordinatePoint
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '*******************************************************************************
        'プロパティ

        '座標値。
        '
        'このプロパティはデフォルトプロパティである。Letを使えば｢値の代入｣ができる。いわゆるC++で言うところのoperator=である。
        '
        '引き数：
        'clsCoordinatePoint コピー元のオブジェクト。
        */
        //public void CoordinatePointXYZ(CoordinatePoint clsCoordinatePoint)
        public void CoordinatePointXYZcpy(CoordinatePoint clsCoordinatePoint)
        {
            CoordinatePoint_CoordinatePoint(clsCoordinatePoint);
            return;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'X。
        Public Property Let X(ByVal nX As Double)
            CoordinatePoint_X = nX
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'X。
        public override void XX(double nX)
        {
            //CoordinatePoint_X(nX);
            m_nX = nX;
            m_nRoundX = JpnRound(m_nX, MdlAccountMake.ACCOUNT_DECIMAL_XYZ);
            return;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'X。
        Public Property Get X() As Double
            X = CoordinatePoint_X
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'X。
        //public double X()
        public override double XX()
        {
            //return CoordinatePoint_X();
            return m_nX;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'Y。
        Public Property Let Y(ByVal nY As Double)
            CoordinatePoint_Y = nY
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'Y。
        public override void YY(double nY)
        {
            //CoordinatePoint_Y(nY);
            m_nY = nY;
            m_nRoundY = JpnRound(m_nY, MdlAccountMake.ACCOUNT_DECIMAL_XYZ);
            return;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'Y。
        Public Property Get Y() As Double
            Y = CoordinatePoint_Y
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'Y。
        //public double Y()
        public override double YY()
        {
            //return CoordinatePoint_Y();
            return m_nY;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'Z。
        Public Property Let Z(ByVal nZ As Double)
            CoordinatePoint_Z = nZ
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'Z。
        public override void ZZ(double nZ)
        {
            //CoordinatePoint_Z(nZ);
            m_nZ = nZ;
            m_nRoundZ = JpnRound(m_nZ, MdlAccountMake.ACCOUNT_DECIMAL_XYZ);
            return;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'Z。
        Public Property Get Z() As Double
            Z = CoordinatePoint_Z
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'Z。
        //public double Z()
        public override double ZZ()
        {
            //return CoordinatePoint_Z();
            return m_nZ;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '丸めX。
        Public Property Get RoundX() As Double
            RoundX = CoordinatePoint_RoundX
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'丸めX。
        //public double RoundX()
        public override double RoundXX()
        {
            //return CoordinatePoint_RoundX();
            return m_nRoundX;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '丸めY。
        Public Property Get RoundY() As Double
            RoundY = CoordinatePoint_RoundY
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'丸めY。
        //public double RoundY()
        public override double RoundYY()
        {
            //return CoordinatePoint_RoundY();
            return m_nRoundY;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '丸めZ。
        Public Property Get RoundZ() As Double
            RoundZ = CoordinatePoint_RoundZ
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'丸めZ。
        //public double RoundZ()
        public override double RoundZZ()
        {
            //return CoordinatePoint_RoundZ();
            return m_nRoundZ;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '座標値種別。
        Public Property Get CoordinateType() As COORDINATE_TYPE
            CoordinateType = CoordinatePoint_CoordinateType
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'座標値種別。
        public override COORDINATE_TYPE CoordinateType()
        {
            //return CoordinatePoint_CoordinateType();
            return m_nCoordinateType;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '*******************************************************************************
        'イベント

        '初期化。
        Private Sub Class_Initialize()
            m_nCoordinateType = COORDINATE_XYZ
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '*******************************************************************************
        'イベント

        '初期化。
        */
        private void Class_Initialize()
        {
            m_nCoordinateType = COORDINATE_TYPE.COORDINATE_XYZ;
        }
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
            Call CoordinatePoint_Save(nFile)
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
            CoordinatePoint_Save(nFile);
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
            Call CoordinatePoint_Load(nFile, nVersion)
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
        public override void Load(int nFile, long nVersion)
        {
            CoordinatePoint_Load(nFile, nVersion);
        }
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
            Compare = CoordinatePoint_Compare(clsCoordinatePoint)
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
        public override bool Compare(CoordinatePoint clsCoordinatePoint)
        {
            return CoordinatePoint_Compare(clsCoordinatePoint);
        }
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
            Call CoordinatePoint_GetXYZ(nX, nY, nZ)
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
        public override void GetXYZ(ref double nX, ref double nY, ref double nZ)
        {
            CoordinatePoint_GetXYZ(ref nX, ref nY, ref nZ);
        }
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
            Call CoordinatePoint_GetDEG(nLat, nLon, nHeight, vAlt, sGeoidoPath)
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
        public override void GetDEG(ref double nLat, ref double nLon, ref double nHeight, ref double vAlt, string sGeoidoPath)
        {
            CoordinatePoint_GetDEG(nLat, nLon, nHeight, vAlt, sGeoidoPath);
        }
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
            Call CoordinatePoint_GetDMS(nLatH, nLatM, nLatS, nLonH, nLonM, nLonS, nHeight, vAlt, nDecimal, sGeoidoPath)
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
        public override void GetDMS(ref long nLatH, ref long nLatM, ref double nLatS, ref long nLonH, ref long nLonM, ref double nLonS, ref double nHeight,
            ref double vAlt, long nDecimal, string sGeoidoPath)
        {
            CoordinatePoint_GetDMS(nLatH, nLatM, nLatS, nLonH, nLonM, nLonS, nHeight, vAlt, nDecimal, sGeoidoPath);
        }
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
            Call CoordinatePoint_GetJGD(nX, nY, nHeight, vAlt, nDecimal, nCoordNum, sGeoidoPath)
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
        public override void GetJGD(ref double nX, ref double nY, ref double nHeight, ref double vAlt, long nDecimal, long nCoordNum, string sGeoidoPath)
        {
            CoordinatePoint_GetJGD(nX, nY, nHeight, vAlt, nDecimal, nCoordNum, sGeoidoPath);
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'コピーしたオブジェクトを生成して返す。
        '
        '戻り値：コピーしたオブジェクト。
        Public Function CreateCopy() As CoordinatePoint
            Set CreateCopy = CoordinatePoint_CreateCopy()
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        'コピーしたオブジェクトを生成して返す。
        '
        '戻り値：コピーしたオブジェクト。
        */
        public override CoordinatePoint CreateCopy()
        {
            return CoordinatePoint_CreateCopy();
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '*******************************************************************************
        'インターフェース

        '座標値。
        Private Property Let CoordinatePoint_CoordinatePoint(ByVal RHS As CoordinatePoint)
            m_nX = RHS.X
                m_nY = RHS.Y
            m_nZ = RHS.Z
                m_nRoundX = RHS.RoundX
            m_nRoundY = RHS.RoundY
                m_nRoundZ = RHS.RoundZ
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '*******************************************************************************
        'インターフェース

        '座標値。
        */
        private void CoordinatePoint_CoordinatePoint(CoordinatePoint RHS)
        {
            m_nX = RHS.X;
            m_nY = RHS.Y;
            m_nZ = RHS.Z;
            m_nRoundX = Math.Round(RHS.X);
            m_nRoundY = Math.Round(RHS.Y);
            m_nRoundZ = Math.Round(RHS.Z);
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'X。
        Private Property Let CoordinatePoint_X(ByVal RHS As Double)
            m_nX = RHS
            m_nRoundX = JpnRound(m_nX, ACCOUNT_DECIMAL_XYZ)
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'X。
        private void CoordinatePoint_X(double RHS)
        {
            //MdlUtility clsMdlUtility = new MdlUtility();
            m_nX = RHS;
            m_nRoundX = JpnRound(m_nX, ACCOUNT_DECIMAL_XYZ);
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'X。
        Private Property Get CoordinatePoint_X() As Double
            CoordinatePoint_X = m_nX
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'X。
        private double CoordinatePoint_X()
        {
            return m_nX;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'Y。
        Private Property Let CoordinatePoint_Y(ByVal RHS As Double)
            m_nY = RHS
                m_nRoundY = JpnRound(m_nY, ACCOUNT_DECIMAL_XYZ)
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'Y。
        private void CoordinatePoint_Y(double RHS)
        {
            //MdlUtility clsMdlUtility = new MdlUtility();
            m_nY = RHS;
            m_nRoundY = JpnRound(m_nY, ACCOUNT_DECIMAL_XYZ);
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'Y。
        Private Property Get CoordinatePoint_Y() As Double
            CoordinatePoint_Y = m_nY
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'Y。
        private double CoordinatePoint_Y()
        {
            return m_nY;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'Z。
        Private Property Let CoordinatePoint_Z(ByVal RHS As Double)
            m_nZ = RHS
            m_nRoundZ = JpnRound(m_nZ, ACCOUNT_DECIMAL_XYZ)
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'Z。
        private void CoordinatePoint_Z(double RHS)
        {
            //MdlUtility clsMdlUtility = new MdlUtility();
            m_nZ = RHS;
            m_nRoundZ = JpnRound(m_nZ, ACCOUNT_DECIMAL_XYZ);
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'Z。
        Private Property Get CoordinatePoint_Z() As Double
            CoordinatePoint_Z = m_nZ
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'Z。
        private double CoordinatePoint_Z()
        {
            return m_nZ;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '丸めX。
        Private Property Get CoordinatePoint_RoundX() As Double
            CoordinatePoint_RoundX = m_nRoundX
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'丸めX。
        private double CoordinatePoint_RoundX()
        {
            return m_nRoundX;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '丸めY。
        Private Property Get CoordinatePoint_RoundY() As Double
            CoordinatePoint_RoundY = m_nRoundY
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'丸めY。
        private double CoordinatePoint_RoundY()
        {
            return m_nRoundY;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '丸めZ。
        Private Property Get CoordinatePoint_RoundZ() As Double
            CoordinatePoint_RoundZ = m_nRoundZ
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'丸めZ。
        private double CoordinatePoint_RoundZ()
        {
            return m_nRoundZ;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '座標値種別。
        Private Property Get CoordinatePoint_CoordinateType() As COORDINATE_TYPE
            CoordinatePoint_CoordinateType = m_nCoordinateType
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'座標値種別。
        private COORDINATE_TYPE CoordinatePoint_CoordinateType()
        {
            return m_nCoordinateType;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '保存。
        Private Sub CoordinatePoint_Save(ByVal nFile As Integer)
            Put #nFile, , m_nRoundX
            Put #nFile, , m_nRoundY
            Put #nFile, , m_nRoundZ
            Put #nFile, , m_nX
            Put #nFile, , m_nY
            Put #nFile, , m_nZ
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'保存。
        private void CoordinatePoint_Save(int nFile)
        {
#if false
            /*
             *************************** 修正要 sakai
             */
            Put #nFile, , m_nRoundX
            Put #nFile, , m_nRoundY
            Put #nFile, , m_nRoundZ
            Put #nFile, , m_nX
            Put #nFile, , m_nY
            Put #nFile, , m_nZ
#endif
            return;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '読み込み。
        Private Sub CoordinatePoint_Load(ByVal nFile As Integer, ByVal nVersion As Long)
            Get #nFile, , m_nRoundX
            Get #nFile, , m_nRoundY
            Get #nFile, , m_nRoundZ
            If nVersion< 6400 Then
                m_nX = m_nRoundX
                m_nY = m_nRoundY
                m_nZ = m_nRoundZ
                m_nRoundX = JpnRound(m_nX, ACCOUNT_DECIMAL_XYZ)
                m_nRoundY = JpnRound(m_nY, ACCOUNT_DECIMAL_XYZ)
                m_nRoundZ = JpnRound(m_nZ, ACCOUNT_DECIMAL_XYZ)
            Else
                Get #nFile, , m_nX
                Get #nFile, , m_nY
                Get #nFile, , m_nZ
            End If
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'読み込み。
        private void CoordinatePoint_Load(int nFile, long nVersion)
        {
            /*
            Get #nFile, , m_nRoundX;
            Get #nFile, , m_nRoundY;
            Get #nFile, , m_nRoundZ;
            if (nVersion < 6400)
            {
                m_nX = m_nRoundX;
                m_nY = m_nRoundY;
                m_nZ = m_nRoundZ;
                m_nRoundX = JpnRound(m_nX, ACCOUNT_DECIMAL_XYZ);
                m_nRoundY = JpnRound(m_nY, ACCOUNT_DECIMAL_XYZ);
                m_nRoundZ = JpnRound(m_nZ, ACCOUNT_DECIMAL_XYZ);
            }
            else
            {
                Get #nFile, , m_nX;
                Get #nFile, , m_nY;
                Get #nFile, , m_nZ;
            }
            */
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '指定されたオブジェクトと比較する。
        Private Function CoordinatePoint_Compare(ByVal clsCoordinatePoint As CoordinatePoint) As Boolean

            CoordinatePoint_Compare = False

            If clsCoordinatePoint.CoordinateType = COORDINATE_XYZ Then
                If Abs(m_nX - clsCoordinatePoint.X) >= FLT_EPSILON Then Exit Function
                If Abs(m_nY - clsCoordinatePoint.Y) >= FLT_EPSILON Then Exit Function
                If Abs(m_nZ - clsCoordinatePoint.Z) >= FLT_EPSILON Then Exit Function
            Else
                Exit Function
                End If
                CoordinatePoint_Compare = True


        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'指定されたオブジェクトと比較する。
        private bool CoordinatePoint_Compare(CoordinatePoint clsCoordinatePoint)
        {
            if (clsCoordinatePoint.CoordinateType() == COORDINATE_TYPE.COORDINATE_XYZ)
            {
                if (Math.Abs(m_nX - clsCoordinatePoint.X) >= DEFINE.FLT_EPSILON)
                {
                    return false;
                }
                if (Math.Abs(m_nY - clsCoordinatePoint.Y) >= DEFINE.FLT_EPSILON)
                {
                    return false;
                }
                if (Math.Abs(m_nZ - clsCoordinatePoint.Z) >= DEFINE.FLT_EPSILON)
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
            return true;

        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '地心直交座標を取得する。
        Private Sub CoordinatePoint_GetXYZ(ByRef nX As Double, ByRef nY As Double, ByRef nZ As Double)
            nX = m_nRoundX
            nY = m_nRoundY
            nZ = m_nRoundZ
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'地心直交座標を取得する。
        private void CoordinatePoint_GetXYZ(ref double nX, ref double nY, ref double nZ)
        {
            nX = m_nRoundX;
            nY = m_nRoundY;
            nZ = m_nRoundZ;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '緯度経度(度)を取得する。
        Private Sub CoordinatePoint_GetDEG(nLat As Double, nLon As Double, nHeight As Double, vAlt As Variant, ByVal sGeoidoPath As String)
            Call WGS84xyz_to_WGS84dms(m_nRoundX, m_nRoundY, m_nRoundZ, nLat, nLon, nHeight)
            Call GetAlt(nLat, nLon, nHeight, vAlt, sGeoidoPath)
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'緯度経度(度)を取得する。
        private void CoordinatePoint_GetDEG(double nLat, double nLon, double nHeight, double vAlt, string sGeoidoPath)
        {
            WGS84xyz_to_WGS84dms(m_nRoundX, m_nRoundY, m_nRoundZ, ref nLat, ref nLon, ref nHeight);
            GetAlt(nLat, nLon, nHeight, ref vAlt, sGeoidoPath);
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '緯度経度(度分秒)を取得する。
        Private Sub CoordinatePoint_GetDMS(nLatH As Long, nLatM As Long, nLatS As Double, nLonH As Long, nLonM As Long, nLonS As Double, nHeight As Double, vAlt As Variant, ByVal nDecimal As Long, ByVal sGeoidoPath As String)
            Dim nLat As Double
            Dim nLon As Double
            Call WGS84xyz_to_WGS84dms(m_nRoundX, m_nRoundY, m_nRoundZ, nLat, nLon, nHeight)
            Call d_to_dms_decimal(nLat, nLatH, nLatM, nLatS, nDecimal)
            Call d_to_dms_decimal(nLon, nLonH, nLonM, nLonS, nDecimal)
            Call GetAlt(nLat, nLon, nHeight, vAlt, sGeoidoPath)
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'緯度経度(度分秒)を取得する。
        private void CoordinatePoint_GetDMS(long nLatH, long nLatM, double nLatS, long nLonH, long nLonM, double nLonS, double nHeight, double vAlt, long nDecimal, string sGeoidoPath)
        {
            double nLat = 0;
            double nLon = 0;
            WGS84xyz_to_WGS84dms(m_nRoundX, m_nRoundY, m_nRoundZ, ref nLat, ref nLon, ref nHeight);
            //d_to_dms_decimal(nLat, ref nLatH, ref nLatM, ref nLatS, nDecimal);
            //d_to_dms_decimal(nLon, ref nLonH, ref nLonM, ref nLonS, nDecimal);
            D_to_Dms_decimal(nLat, ref nLatH, ref nLatM, ref nLatS, nDecimal);
            D_to_Dms_decimal(nLon, ref nLonH, ref nLonM, ref nLonS, nDecimal);
            GetAlt(nLat, nLon, nHeight, ref vAlt, sGeoidoPath);
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '平面直角座標を取得する。
        Private Sub CoordinatePoint_GetJGD(nX As Double, nY As Double, nHeight As Double, vAlt As Variant, ByVal nDecimal As Long, ByVal nCoordNum As Long, ByVal sGeoidoPath As String)
            Dim nLat As Double
            Dim nLon As Double
            Call WGS84xyz_to_WGS84dms(m_nRoundX, m_nRoundY, m_nRoundZ, nLat, nLon, nHeight)
            '一旦DMSに変換して nDecimal で丸める。
            Dim nH As Long
            Dim nM As Long
            Dim nS As Double
            Call d_to_dms_decimal(nLat, nH, nM, nS, nDecimal)
            nLat = dms_to_d(nH, nM, nS)
            Call d_to_dms_decimal(nLon, nH, nM, nS, nDecimal)
            nLon = dms_to_d(nH, nM, nS)
            Dim nZ As Double
            Call WGS84dms_to_JGDxyz(nCoordNum, nLat, nLon, 0, nX, nY, nZ)
            Call GetAlt(nLat, nLon, nHeight, vAlt, sGeoidoPath)
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'平面直角座標を取得する。
        private void CoordinatePoint_GetJGD(double nX, double nY, double nHeight, double vAlt, long nDecimal, long nCoordNum, string sGeoidoPath)
        {
            double nLat = 0;
            double nLon = 0;
            WGS84xyz_to_WGS84dms(m_nRoundX, m_nRoundY, m_nRoundZ, ref nLat, ref nLon, ref nHeight);
            //'一旦DMSに変換して nDecimal で丸める。
            long nH = 0;
            long nM = 0;
            double nS = 0;
            //d_to_dms_decimal(nLat, ref nH, ref nM, ref nS, nDecimal);
            D_to_Dms_decimal(nLat, ref nH, ref nM, ref nS, nDecimal);
            nLat = dms_to_d((int)nH, (int)nM, nS);
            //d_to_dms_decimal(nLon, ref nH, ref nM, ref nS, nDecimal);
            D_to_Dms_decimal(nLon, ref nH, ref nM, ref nS, nDecimal);
            nLon = dms_to_d((int)nH, (int)nM, nS);
            double nZ = 0;
            WGS84dms_to_JGDxyz((int)nCoordNum, nLat, nLon, 0, ref nX, ref nY, ref nZ);
            GetAlt(nLat, nLon, nHeight, ref vAlt, sGeoidoPath);
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'コピーしたオブジェクトを生成して返す。
        Private Function CoordinatePoint_CreateCopy() As CoordinatePoint
            Dim clsCoordinatePoint As CoordinatePoint
            Set clsCoordinatePoint = New CoordinatePointXYZ
            Let clsCoordinatePoint = Me
            Set CoordinatePoint_CreateCopy = clsCoordinatePoint
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'コピーしたオブジェクトを生成して返す。
        private CoordinatePoint CoordinatePoint_CreateCopy()
        {
#if false
            /*
             *************************** 修正要 sakai
            */
            CoordinatePoint clsCoordinatePoint;
            clsCoordinatePoint = new CoordinatePointXYZ();
            clsCoordinatePoint = Me;
#endif
            return null;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '*******************************************************************************
        'インプリメンテーション

        '高さを取得する。
        '
        '引き数：
        'nLat 緯度(度)。
        'nLon 経度(度)。
        'nHeight 楕円体高。
        'vAlt 標高が設定される。標高が取得されない場合は null が設定される。
        'sGeoidoPath ジオイドモデルのパス。標高がOFFの場合、空文字を指定する。
        Private Sub GetAlt(ByVal nLat As Double, ByVal nLon As Double, ByVal nHeight As Double, ByRef vAlt As Variant, ByVal sGeoidoPath As String)
            If sGeoidoPath<> "" Then
                Dim nGeoido As Double
                If get_geo_height(sGeoidoPath, nLat, nLon, nGeoido) = 0 Then
                    vAlt = nHeight - nGeoido
                Else
                    vAlt = Null
                End If
            Else
                vAlt = Null
            End If
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '*******************************************************************************
        'インプリメンテーション

        '高さを取得する。
        '
        '引き数：
        'nLat 緯度(度)。
        'nLon 経度(度)。
        'nHeight 楕円体高。
        'vAlt 標高が設定される。標高が取得されない場合は null が設定される。
        'sGeoidoPath ジオイドモデルのパス。標高がOFFの場合、空文字を指定する。
        */
        private void GetAlt(double nLat, double nLon, double nHeight, ref double vAlt, string sGeoidoPath)
        {
            if (sGeoidoPath != "")
            {
                double nGeoido = 0;
                if (get_geo_height(sGeoidoPath, nLat, nLon, ref nGeoido) == 0)
                {
                    vAlt = nHeight - nGeoido;
                }
                else
                {
                    vAlt = 0;
                }
            }
            else
            {
                vAlt = 0;
            }
        }
        //==========================================================================================









        //***************************************************************************
        /// <summary>
        //'イベント
        ///     （VB Class_Initialize関数をVC用に変更）
        ///'
        ///'引き数：
        /// 読み込みデータ
        /// </summary>
        /// <param name="Genba_S">
        /// </param>
        /// <returns>
        /// 戻り値:returns = Non
        /// </returns>
        //***************************************************************************
        //'*******************************************************************************
        //'イベント
        //
        //'初期化。
        //Private Sub Class_Initialize()
        //    m_nCoordinateType = COORDINATE_XYZ
        //End Sub
        private void Class_Initialize(ref GENBA_STRUCT_S Genba_S)
        {
            ////[VB]m_nCoordinateType = COORDINATE_XYZ
            ////(del)     Genba_S.CPXYZ_m_nCoordinateType = 0;        //23/12/20 K.Setoguchi
            //Genba_S.CPXYZ.m_nCoordinateType = 0;                    //23/12/20 K.Setoguchi
            Genba_S.CPXYZ.m_nCoordinateType = (int)COORDINATE_TYPE.COORDINATE_XYZ;
            m_nCoordinateType = (int)COORDINATE_TYPE.COORDINATE_XYZ;
        }
        //***************************************************************************
        //***************************************************************************

        //***************************************************************************
        //***************************************************************************
        /// <summary>
        //'イベント
        ///     （VB Load関数をVC用に変更）
        ///'
        ///'引き数：
        ///  '引き数：
        /// バイナリファイル
        /// バージョン番号
        /// 読み込みデータ
        /// </summary>
        /// <param name="BinaryReader br">
        /// <param name="nVersion br">
        /// <param name="Genba_S">
        /// </param>
        /// <returns>
        /// 戻り値:returns = Non
        /// </returns>
        //***************************************************************************
        public void Load(BinaryReader br, long nVersion, ref GENBA_STRUCT_S Genba_S)
        {
            //[VB]Call CoordinatePoint_Load(nFile, nVersion)
            CoordinatePoint_Load(br, nVersion, ref Genba_S);
        }
        //---------------------------------------------------------------------------
        //[VB]'読み込み。
        //[VB]'
        //[VB]'引き数：
        //[VB]'nFile ファイル番号。
        //[VB]'nVersion ファイルバージョン。
        //[VB]Public Sub Load(ByVal nFile As Integer, ByVal nVersion As Long)
        //[VB]Call CoordinatePoint_Load(nFile, nVersion)
        //[VB]End Sub
        //***************************************************************************
        //***************************************************************************


        //***************************************************************************
        //***************************************************************************
        /// <summary>
        ///'読み込み。
        ///     （VB Load関数をVC用に変更）
        ///'
        ///'引き数：
        ///  '引き数：
        /// バイナリファイル
        /// バージョン番号
        /// 読み込みデータ
        /// </summary>
        /// <param name="BinaryReader br">
        /// <param name="nVersion br">
        /// <param name="Genba_S">
        /// </param>
        /// <returns>
        /// 戻り値:returns = Non
        /// </returns>
        //***************************************************************************
        private void CoordinatePoint_Load(BinaryReader br, long nVersion, ref GENBA_STRUCT_S Genba_S)
        {

            //-------------------------------------------
            Class_Initialize(ref Genba_S);      //NV追加

            //-------------------------------------------
            //[VB]  Get #nFile, , m_nRoundX                         0       -3796335.47
            //(del)     Genba_S.CPXYZ_m_nRoundX = br.ReadDouble();  //23/12/20 K.Setoguchi
            Genba_S.CPXYZ.m_nRoundX = br.ReadDouble();              //23/12/20 K.Setoguchi
            m_nRoundX = Genba_S.CPXYZ.m_nRoundX;
            //-------------------------------------------
            //[VB]  Get #nFile, , m_nRoundY                         0       3572066.263
            //(del)     Genba_S.CPXYZ_m_nRoundY = br.ReadDouble();  //23/12/20 K.Setoguchi
            Genba_S.CPXYZ.m_nRoundY = br.ReadDouble();              //23/12/20 K.Setoguchi    
            m_nRoundY = Genba_S.CPXYZ.m_nRoundY;
            //-------------------------------------------
            //[VB]  Get #nFile, , m_nRoundZ                         0       3663194.625
            //(del)     Genba_S.CPXYZ_m_nRoundZ = br.ReadDouble();  //23/12/20 K.Setoguchi
            Genba_S.CPXYZ.m_nRoundZ = br.ReadDouble();              //23/12/20 K.Setoguchi
            m_nRoundZ = Genba_S.CPXYZ.m_nRoundZ;
            //-------------------------------------------
            if (nVersion < 6400)
            {
                //-------------------------------------------
                //m_nX = m_nRoundX
                //(del)     Genba_S.CPXYZ_m_nX = Genba_S.CPXYZ_m_nRoundX;   //23/12/20 K.Setoguchi
                Genba_S.CPXYZ.m_nX = Genba_S.CPXYZ.m_nRoundX;               //23/12/20 K.Setoguchi
                m_nX = Genba_S.CPXYZ.m_nRoundX;
                //-------------------------------------------
                //m_nY = m_nRoundY
                //(del)     Genba_S.CPXYZ_m_nY = Genba_S.CPXYZ_m_nRoundX;   //23/12/20 K.Setoguchi
                Genba_S.CPXYZ.m_nY = Genba_S.CPXYZ.m_nRoundY;               //23/12/20 K.Setoguchi
                m_nY = Genba_S.CPXYZ.m_nRoundY;
                //-------------------------------------------
                //m_nZ = m_nRoundZ
                //(del)     Genba_S.CPXYZ_m_nZ = Genba_S.CPXYZ_m_nRoundX;   //23/12/20 K.Setoguchi
                Genba_S.CPXYZ.m_nZ = Genba_S.CPXYZ.m_nRoundZ;               //23/12/20 K.Setoguchi
                m_nZ = Genba_S.CPXYZ.m_nRoundZ;



                //-------------------------------------------//23/12/20 K.Setoguchi
                //[VB]      m_nRoundX = JpnRound(m_nX, ACCOUNT_DECIMAL_XYZ)                                             //23/12/20 K.Setoguchi
                //MdlUtility mdlUtility = new MdlUtility();                                                               //23/12/20 K.Setoguchi
                Genba_S.CPXYZ.m_nRoundX = JpnRound(Genba_S.CPXYZ.m_nX, MdlAccountMake.ACCOUNT_DECIMAL_XYZ);  //23/12/20 K.Setoguchi
                m_nRoundX = Genba_S.CPXYZ.m_nRoundX;
                //[VB]      m_nRoundY = JpnRound(m_nY, ACCOUNT_DECIMAL_XYZ)     //23/12/20 K.Setoguchi                  //23/12/20 K.Setoguchi       
                Genba_S.CPXYZ.m_nRoundY = JpnRound(Genba_S.CPXYZ.m_nY, MdlAccountMake.ACCOUNT_DECIMAL_XYZ);  //23/12/20 K.Setoguchi
                m_nRoundY = Genba_S.CPXYZ.m_nRoundY;
                //[VB]      m_nRoundZ = JpnRound(m_nZ, ACCOUNT_DECIMAL_XYZ)     //23/12/20 K.Setoguchi                  //23/12/20 K.Setoguchi
                Genba_S.CPXYZ.m_nRoundZ = JpnRound(Genba_S.CPXYZ.m_nZ, MdlAccountMake.ACCOUNT_DECIMAL_XYZ);  //23/12/20 K.Setoguchi
                m_nRoundZ = Genba_S.CPXYZ.m_nRoundZ;
                //-------------------------------------------//23/12/20 K.Setoguchi




            }
            else
            {
                //-------------------------------------------
                //Get #nFile, , m_nX                                0 /-3796335.4701
                //(del)     Genba_S.CPXYZ_m_nX = br.ReadDouble();           //23/12/20 K.Setoguchi
                Genba_S.CPXYZ.m_nX = br.ReadDouble();                       //23/12/20 K.Setoguchi
                m_nX = Genba_S.CPXYZ.m_nX;
                //-------------------------------------------   
                //Get #nFile, , m_nY                                0 /3572066.2631
                //(del)     Genba_S.CPXYZ_m_nY = br.ReadDouble();           //23/12/20 K.Setoguchi
                Genba_S.CPXYZ.m_nY = br.ReadDouble();                       //23/12/20 K.Setoguchi
                m_nY = Genba_S.CPXYZ.m_nY;
                //Get #nFile, , m_nZ                                0 /3663194.625 
                //(del)     Genba_S.CPXYZ_m_nZ = br.ReadDouble();           //23/12/20 K.Setoguchi
                Genba_S.CPXYZ.m_nZ = br.ReadDouble();                       //23/12/20 K.Setoguchi    
                m_nZ = Genba_S.CPXYZ.m_nZ;
                //-------------------------------------------
            }
        }
        //----------------------------------------------------------------------------------
        //'読み込み。
        //Private Sub CoordinatePoint_Load(ByVal nFile As Integer, ByVal nVersion As Long)
        //    Get #nFile, , m_nRoundX
        //    Get #nFile, , m_nRoundY
        //    Get #nFile, , m_nRoundZ
        //    If nVersion< 6400 Then
        //        m_nX = m_nRoundX
        //        m_nY = m_nRoundY
        //        m_nZ = m_nRoundZ
        //        m_nRoundX = JpnRound(m_nX, ACCOUNT_DECIMAL_XYZ)
        //        m_nRoundY = JpnRound(m_nY, ACCOUNT_DECIMAL_XYZ)
        //        m_nRoundZ = JpnRound(m_nZ, ACCOUNT_DECIMAL_XYZ)
        //    Else
        //        Get #nFile, , m_nX
        //        Get #nFile, , m_nY
        //        Get #nFile, , m_nZ
        //    End If
        //End Sub
        //***************************************************************************
        //***************************************************************************

    }
}
