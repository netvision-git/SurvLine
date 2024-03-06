using SurvLine;
using SurvLine.mdl;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static SurvLine.mdl.DEFINE;
using static SurvLine.mdl.MdlNSDefine;
using static SurvLine.mdl.MdlUtility;
using static SurvLine.mdl.MdlAccountMake;
using static SurvLine.mdl.MdlNSGUI;
using static SurvLine.mdl.MdlNSSDefine;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;
using static System.Net.WebRequestMethods;
using Microsoft.VisualBasic;
using System.Security.Claims;
using Microsoft.VisualBasic.Logging;

namespace SurvLine
{
    public class CoordinatePointFix : CoordinatePointXYZ
    {


        //==========================================================================================
        /*[VB]
        '*******************************************************************************
        '固定座標値

        Option Explicit

        Implements CoordinatePoint
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'インプリメンテーション
        Private m_nX As Double 'X。
        Private m_nY As Double 'Y。
        Private m_nZ As Double 'Z。
        Private m_nRoundX As Double '丸めX。
        Private m_nRoundY As Double '丸めY。
        Private m_nRoundZ As Double '丸めZ。
        Private m_nHeight As Double '楕円体高。
        Private m_nCoordinateType As COORDINATE_TYPE '座標値種別。
        Private m_nEditCode As EDITCODE_STYLE '編集コード。
        Private m_sEditX As String '入力X。
        Private m_sEditY As String '入力Y。
        Private m_sEditZ As String '入力Z。
        Private m_sEditLat As String '入力緯度。
        Private m_sEditLon As String '入力経度。
        Private m_sEditHeight As String '入力高さ(ｍ)。
        Private m_nEditCoordNum As Long '入力座標系番号(1～19)。
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
        private double m_nHeight;                   //'楕円体高。
        private COORDINATE_TYPE m_nCoordinateType;  //'座標値種別。
        private COORDINATE_TYPE m_nEditCode;        //'編集コード。
        private string m_sEditX;                    //'入力X。
        private string m_sEditY;                    //'入力Y。
        private string m_sEditZ;                    //'入力Z。
        private string m_sEditLat;                  //'入力緯度。
        private string m_sEditLon;                  //'入力経度。
        private string m_sEditHeight;               //'入力高さ(ｍ)。
        private long m_nEditCoordNum;               //'入力座標系番号(1～19)。
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'セミ・ダイナミック変換'2009/11 H.Nakamura
        Private m_nSemiDynaCounter As Long 'セミ・ダイナミック補正変換番号。0 は未計算。
        Private m_nLatKON As Double '今期緯度(度)。
        Private m_nLonKON As Double '今期経度(度)。
        Private m_nHeightKON As Double '今期楕円体高(ｍ)。
        Private m_nXKON As Double '今期X。
        Private m_nYKON As Double '今期Y。
        Private m_nZKON As Double '今期Z。
        Private m_nRoundXKON As Double '今期丸めX。
        Private m_nRoundYKON As Double '今期丸めY。
        Private m_nRoundZKON As Double '今期丸めZ。
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'セミ・ダイナミック変換'2009/11 H.Nakamura
        private long m_nSemiDynaCounter;            //'セミ・ダイナミック補正変換番号。0 は未計算。
        private double m_nLatKON;                   //'今期緯度(度)。
        private double m_nLonKON;                   //'今期経度(度)。
        private double m_nHeightKON;                //'今期楕円体高(ｍ)。
        private double m_nXKON;                     //'今期X。
        private double m_nYKON;                     //'今期Y。
        private double m_nZKON;                     //'今期Z。
        private double m_nRoundXKON;                //'今期丸めX。
        private double m_nRoundYKON;                //'今期丸めY。
        private double m_nRoundZKON;                //'今期丸めZ。
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
        'clsCoordinatePointFix コピー元のオブジェクト。
        Public Property Let CoordinatePointFix(ByVal clsCoordinatePoint As CoordinatePoint)
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
        'clsCoordinatePointFix コピー元のオブジェクト。
        */
        //public void CoordinatePointFix(CoordinatePoint clsCoordinatePoint)
        public void CoordinatePointFixcpy(CoordinatePoint clsCoordinatePoint)
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
            m_nEditCode = 0;
            //'2009/11 H.Nakamura
            //'セミ・ダイナミック補正変換を未計算にする。
            m_nSemiDynaCounter = 0;
            //'今期座標を初期化する。
            InitKON();
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
            m_nEditCode = 0;
            //'2009/11 H.Nakamura
            //'セミ・ダイナミック補正変換を未計算にする。
            m_nSemiDynaCounter = 0;
            //'今期座標を初期化する。
            InitKON();
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
        public override double YY()
        {
            //return CoordinatePoint_Y();
            if (MdlSemiDyna.m_bEnable)
            {
                if (m_nSemiDynaCounter < MdlSemiDyna.m_nCounter) { ConvSemiDyna(); }
                return m_nYKON;
            }
            else
            {
                return m_nY;
            }
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
            m_nEditCode = 0;
            //'2009/11 H.Nakamura
            //'セミ・ダイナミック補正変換を未計算にする。
            m_nSemiDynaCounter = 0;
            //'今期座標を初期化する。
            InitKON();
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
        public COORDINATE_TYPE CoordinateType()
        {
            return CoordinatePoint_CoordinateType();
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '編集コード。
        Public Property Get EditCode() As Long
            EditCode = m_nEditCode
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'編集コード。
        public long EditCode()
        {
            return (long)m_nEditCode;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '入力X。
        Public Property Get EditX() As String
            EditX = m_sEditX
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'入力X。
        public string EditX()
        {
            return m_sEditX;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '入力Y。
        Public Property Get EditY() As String
            EditY = m_sEditY
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'入力Y。
        public string EditY()
        {
            return m_sEditY;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '入力Z。
        Public Property Get EditZ() As String
            EditZ = m_sEditZ
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'入力Z。
        public string EditZ()
        {
            return m_sEditZ;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '入力緯度。
        Public Property Get EditLat() As String
            EditLat = m_sEditLat
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'入力緯度。
        public string EditLat()
        {
            return m_sEditLat;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '入力経度。
        Public Property Get EditLon() As String
            EditLon = m_sEditLon
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'入力経度。
        public string EditLon()
        {
            return m_sEditLon;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '入力高さ(ｍ)。
        Public Property Get EditHeight() As String
            EditHeight = m_sEditHeight
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'入力高さ(ｍ)。
        public string EditHeight()
        {
            return m_sEditHeight;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '入力座標系番号(1～19)。
        Public Property Get EditCoordNum() As Long
            EditCoordNum = m_nEditCoordNum
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'入力座標系番号(1～19)。
        public long EditCoordNum()
        {
            return m_nEditCoordNum;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '楕円体高。
        Public Property Get Height() As Double
            Height = m_nHeight
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'楕円体高。
        public double Height()
        {
            return m_nHeight;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '今期緯度。
        Public Property Get LatKON() As Double
            LatKON = m_nLatKON
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'今期緯度。
        public double LatKON()
        {
            return m_nLatKON;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '今期経度。
        Public Property Get LonKON() As Double
            LonKON = m_nLonKON
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'今期経度。
        public double LonKON()
        {
            return m_nLonKON;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '今期楕円体高。
        Public Property Get HeightKON() As Double
            HeightKON = m_nHeightKON
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'今期楕円体高。
        public double HeightKON()
        {
            return m_nHeightKON;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '今期X。
        Public Property Get XKON() As Double
            XKON = m_nXKON
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'今期X。
        public double XKON()
        {
            return m_nXKON;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '今期Y。
        Public Property Get YKON() As Double
            YKON = m_nYKON
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'今期Y。
        public double YKON()
        {
            return m_nYKON;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '今期Z。
        Public Property Get ZKON() As Double
            ZKON = m_nZKON
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'今期Z。
        public double ZKON()
        {
            return m_nZKON;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '今期丸めX。
        Public Property Get RoundXKON() As Double
            RoundXKON = m_nRoundXKON
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'今期丸めX。
        public double RoundXKON()
        {
            return m_nRoundXKON;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '今期丸めY。
        Public Property Get RoundYKON() As Double
            RoundYKON = m_nRoundYKON
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'今期丸めY。
        public double RoundYKON()
        {
            return m_nRoundYKON;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '今期丸めZ。
        Public Property Get RoundZKON() As Double
            RoundZKON = m_nRoundZKON
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'今期丸めZ。
        public double RoundZKON()
        {
            return m_nRoundZKON;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '元期X。
        Public Property Get XGAN() As Double
            XGAN = m_nX
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'元期X。
        public double XGAN()
        {
            return m_nX;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '元期Y。
        Public Property Get YGAN() As Double
            YGAN = m_nY
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'元期Y。
        public double YGAN()
        {
            return m_nY;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '元期Z。
        Public Property Get ZGAN() As Double
            ZGAN = m_nZ
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'元期Z。
        public double ZGAN()
        {
            return m_nZ;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '元期丸めX。
        Public Property Get RoundXGAN() As Double
            RoundXGAN = m_nRoundX
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'元期丸めX。
        public double RoundXGAN()
        {
            return m_nRoundX;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '元期丸めY。
        Public Property Get RoundYGAN() As Double
            RoundYGAN = m_nRoundY
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'元期丸めY。
        public double RoundYGAN()
        {
            return m_nRoundY;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '元期丸めZ。
        Public Property Get RoundZGAN() As Double
            RoundZGAN = m_nRoundZ
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'元期丸めZ。
        public double RoundZGAN()
        {
            return m_nRoundZ;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '*******************************************************************************
        'イベント

        '初期化。
        Private Sub Class_Initialize()

            m_nCoordinateType = COORDINATE_FIX
    
            'セミ・ダイナミック対応。'2009/11 H.Nakamura
            m_nSemiDynaCounter = 0
    
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

            m_nCoordinateType = COORDINATE_TYPE.COORDINATE_FIX;


            //'セミ・ダイナミック対応。'2009 / 11 H.Nakamura
            m_nSemiDynaCounter = 0;


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
        public void Load(int nFile, long nVersion)
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
        public bool Compare(CoordinatePoint clsCoordinatePoint)
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
        public void GetXYZ(ref double nX, ref double nY, ref double nZ)
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
        public void GetDEG(ref double nLat, ref double nLon, ref double nHeight, ref double vAlt, string sGeoidoPath)
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
        public void GetDMS(ref long nLatH, ref long nLatM, ref double nLatS, ref long nLonH, ref long nLonM, ref double nLonS, ref double nHeight, ref double vAlt, long nDecimal, string sGeoidoPath)
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
        'nDecimal 使用しない。
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
        'nDecimal 使用しない。
        'nCoordNum 座標系番号(1～19)。
        'sGeoidoPath ジオイドモデルのパス。標高がOFFの場合、空文字を指定する。
        */
        public void GetJGD(ref double nX, ref double nY, ref double nHeight, ref double vAlt, long nDecimal, long nCoordNum, string sGeoidoPath)
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
        public CoordinatePoint CreateCopy()
        {
            return CoordinatePoint_CreateCopy();
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '緯度経度(度分秒)を取得する。
        '
        '負の値の場合 sEditH にマイナスが付く。
        '
        '引き数：
        'sEdit 文字列。
        'sEditH 緯度(度)が設定される。
        'sEditM 緯度(分)が設定される。
        'sEditS 緯度(秒)が設定される。
        Public Sub GetEditDMS(ByVal sEdit As String, ByRef sEditH As String, ByRef sEditM As String, ByRef sEditS As String)
            Dim nH As Long
            Dim nM As Long
            nH = Val(Left$(sEdit, 4))
            nM = Val(Mid$(sEdit, 5, 2))
            sEditH = CStr(nH)
            sEditM = CStr(nM)
            sEditS = Mid$(sEdit, 7)
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '緯度経度(度分秒)を取得する。
        '
        '負の値の場合 sEditH にマイナスが付く。
        '
        '引き数：
        'sEdit 文字列。
        'sEditH 緯度(度)が設定される。
        'sEditM 緯度(分)が設定される。
        'sEditS 緯度(秒)が設定される。
        */
        public void GetEditDMS(string sEdit, ref string sEditH, ref string sEditM, ref string sEditS)
        {
            long nH;
            long nM;
            nH = Convert.ToInt32(Left(sEdit, 4));
            nM = Convert.ToInt32(Mid(sEdit, 5, 2));
            sEditH = nH.ToString();
            sEditM = nM.ToString();
            sEditS = Mid(sEdit, 7, 99);
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '地心直交で座標値を設定する。
        '
        '引き数：
        'sEditX 入力X。
        'sEditY 入力Y。
        'sEditZ 入力Z。
        Public Sub SetXYZ(ByVal sEditX As String, ByVal sEditY As String, ByVal sEditZ As String)

            'セミ・ダイナミック補正変換を未計算にする。'2009/11 H.Nakamura
            m_nSemiDynaCounter = 0
    
            m_sEditX = sEditX
            m_sEditY = sEditY
            m_sEditZ = sEditZ
            m_nEditCode = EDITCODE_COORD_XYZ
    
            m_nX = Val(m_sEditX)
            m_nY = Val(m_sEditY)
            m_nZ = Val(m_sEditZ)
            m_nRoundX = JpnRound(m_nX, ACCOUNT_DECIMAL_XYZ)
            m_nRoundY = JpnRound(m_nY, ACCOUNT_DECIMAL_XYZ)
            m_nRoundZ = JpnRound(m_nZ, ACCOUNT_DECIMAL_XYZ)
    
            '2009/11 H.Nakamura
            '今期座標を初期化する。
            Call InitKON
    
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '地心直交で座標値を設定する。
        '
        '引き数：
        'sEditX 入力X。
        'sEditY 入力Y。
        'sEditZ 入力Z。
        */
        public void SetXYZ(string sEditX, string sEditY, string sEditZ)
        {
            //'セミ・ダイナミック補正変換を未計算にする。'2009 / 11 H.Nakamura
            m_nSemiDynaCounter = 0;

            m_sEditX = sEditX;
            m_sEditY = sEditY;
            m_sEditZ = sEditZ;
            m_nEditCode = (COORDINATE_TYPE)EDITCODE_STYLE.EDITCODE_COORD_XYZ;

            m_nX = Convert.ToDouble(m_sEditX);
            m_nY = Convert.ToDouble(m_sEditY);
            m_nZ = Convert.ToDouble(m_sEditZ);
            m_nRoundX = JpnRound(m_nX, ACCOUNT_DECIMAL_XYZ);
            m_nRoundY = JpnRound(m_nY, ACCOUNT_DECIMAL_XYZ);
            m_nRoundZ = JpnRound(m_nZ, ACCOUNT_DECIMAL_XYZ);

            //'2009/11 H.Nakamura
            //'今期座標を初期化する。
            InitKON();
            return;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '緯度経度(度分秒)で座標値を設定する。
        '
        'nEditCode で楕円体高か標高か指定する。
        '
        '引き数：
        'nEditCode 編集コード。
        'sEditLatH 入力緯度度。
        'sEditLatM 入力緯度分。
        'sEditLatS 入力緯度秒。
        'sEditLonH 入力経度度。
        'sEditLonM 入力経度分。
        'sEditLonS 入力経度秒。
        'sEditHeight 入力高さ。
        'sGeoidoPath ジオイドモデルのパス。標高がOFFの場合、空文字を指定する。
        Public Sub SetDMS(ByVal nEditCode As EDITCODE_STYLE, ByVal sEditLatH As String, ByVal sEditLatM As String, ByVal sEditLatS As String, ByVal sEditLonH As String, ByVal sEditLonM As String, ByVal sEditLonS As String, ByVal sEditHeight As String, ByVal sGeoidoPath As String)

            'セミ・ダイナミック補正変換を未計算にする。'2009/11 H.Nakamura
            m_nSemiDynaCounter = 0
    
            m_sEditLat = DMS2STR(sEditLatH, sEditLatM, sEditLatS)
            m_sEditLon = DMS2STR(sEditLonH, sEditLonM, sEditLonS)
            m_sEditHeight = sEditHeight
            m_nEditCode = EDITCODE_COORD_DMS
    
            Dim nLat As Double
            Dim nLon As Double
            nLat = STR2DEG(m_sEditLat)
            nLon = STR2DEG(m_sEditLon)
    
            If (nEditCode And EDITCODE_VERT_HEIGHT) <> 0 Then
                m_nEditCode = m_nEditCode + EDITCODE_VERT_HEIGHT
                m_nHeight = Val(m_sEditHeight)
            Else
                m_nEditCode = m_nEditCode + EDITCODE_VERT_ALT
                Dim nGeo As Double
                If get_geo_height(sGeoidoPath, nLat, nLon, nGeo) <> 0 Then nGeo = 0
                m_nHeight = Val(m_sEditHeight) + nGeo
            End If
    
            Call WGS84dms_to_WGS84xyz(nLat, nLon, m_nHeight, m_nX, m_nY, m_nZ)
            m_nRoundX = JpnRound(m_nX, ACCOUNT_DECIMAL_XYZ)
            m_nRoundY = JpnRound(m_nY, ACCOUNT_DECIMAL_XYZ)
            m_nRoundZ = JpnRound(m_nZ, ACCOUNT_DECIMAL_XYZ)
    
            '2009/11 H.Nakamura
            '今期座標を初期化する。
            Call InitKON
    
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '緯度経度(度分秒)で座標値を設定する。
        '
        'nEditCode で楕円体高か標高か指定する。
        '
        '引き数：
        'nEditCode 編集コード。
        'sEditLatH 入力緯度度。
        'sEditLatM 入力緯度分。
        'sEditLatS 入力緯度秒。
        'sEditLonH 入力経度度。
        'sEditLonM 入力経度分。
        'sEditLonS 入力経度秒。
        'sEditHeight 入力高さ。
        'sGeoidoPath ジオイドモデルのパス。標高がOFFの場合、空文字を指定する。
        */
        public void SetDMS(EDITCODE_STYLE nEditCode, string sEditLatH, string sEditLatM, string sEditLatS, string sEditLonH, string sEditLonM, string sEditLonS, string sEditHeight, string sGeoidoPath)
        {

            //'セミ・ダイナミック補正変換を未計算にする。'2009 / 11 H.Nakamura
            m_nSemiDynaCounter = 0;


            m_sEditLat = DMS2STR(sEditLatH, sEditLatM, sEditLatS);
            m_sEditLon = DMS2STR(sEditLonH, sEditLonM, sEditLonS);
            m_sEditHeight = sEditHeight;
            m_nEditCode = (COORDINATE_TYPE)EDITCODE_STYLE.EDITCODE_COORD_DMS;


            double nLat;
            double nLon;
            nLat = STR2DEG(m_sEditLat);
            nLon = STR2DEG(m_sEditLon);

            if ((nEditCode & EDITCODE_STYLE.EDITCODE_VERT_HEIGHT) != 0)
            {
                m_nEditCode |= (COORDINATE_TYPE)EDITCODE_STYLE.EDITCODE_VERT_HEIGHT;
                m_nHeight = Convert.ToDouble(m_sEditHeight);
            }
            else
            {
                m_nEditCode |= (COORDINATE_TYPE)EDITCODE_STYLE.EDITCODE_VERT_ALT;
                double nGeo = 0;
                if (get_geo_height(sGeoidoPath, nLat, nLon, ref nGeo) != 0) { nGeo = 0; }
                m_nHeight = Convert.ToDouble(m_sEditHeight) + nGeo;
            }

            WGS84dms_to_WGS84xyz(nLat, nLon, m_nHeight, ref m_nX, ref m_nY, ref m_nZ);
            m_nRoundX = JpnRound(m_nX, ACCOUNT_DECIMAL_XYZ);
            m_nRoundY = JpnRound(m_nY, ACCOUNT_DECIMAL_XYZ);
            m_nRoundZ = JpnRound(m_nZ, ACCOUNT_DECIMAL_XYZ);

            //'2009/11 H.Nakamura
            //'今期座標を初期化する。
            InitKON();
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '緯度経度(度)で座標値を設定する。
        '
        'nEditCode で楕円体高か標高か指定する。
        '
        '引き数：
        'nEditCode 編集コード。
        'sEditLat 入力緯度。
        'sEditLon 入力経度。
        'sEditHeight 入力高さ。
        'sGeoidoPath ジオイドモデルのパス。標高がOFFの場合、空文字を指定する。
        Public Sub SetDEG(ByVal nEditCode As EDITCODE_STYLE, ByVal sEditLat As String, ByVal sEditLon As String, ByVal sEditHeight As String, ByVal sGeoidoPath As String)

            'セミ・ダイナミック補正変換を未計算にする。'2009/11 H.Nakamura
            m_nSemiDynaCounter = 0
    
            m_sEditLat = sEditLat
            m_sEditLon = sEditLon
            m_sEditHeight = sEditHeight
            m_nEditCode = EDITCODE_COORD_DEG
    
            Dim nLat As Double
            Dim nLon As Double
            nLat = Val(m_sEditLat)
            nLon = Val(m_sEditLon)
    
            If (nEditCode And EDITCODE_VERT_HEIGHT) <> 0 Then
                m_nEditCode = m_nEditCode + EDITCODE_VERT_HEIGHT
                m_nHeight = Val(m_sEditHeight)
            Else
                m_nEditCode = m_nEditCode + EDITCODE_VERT_ALT
                Dim nGeo As Double
                If get_geo_height(sGeoidoPath, nLat, nLon, nGeo) <> 0 Then nGeo = 0
                m_nHeight = Val(m_sEditHeight) + nGeo
            End If
    
            Call WGS84dms_to_WGS84xyz(nLat, nLon, m_nHeight, m_nX, m_nY, m_nZ)
            m_nRoundX = JpnRound(m_nX, ACCOUNT_DECIMAL_XYZ)
            m_nRoundY = JpnRound(m_nY, ACCOUNT_DECIMAL_XYZ)
            m_nRoundZ = JpnRound(m_nZ, ACCOUNT_DECIMAL_XYZ)
    
            '2009/11 H.Nakamura
            '今期座標を初期化する。
            Call InitKON
    
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '緯度経度(度)で座標値を設定する。
        '
        'nEditCode で楕円体高か標高か指定する。
        '
        '引き数：
        'nEditCode 編集コード。
        'sEditLat 入力緯度。
        'sEditLon 入力経度。
        'sEditHeight 入力高さ。
        'sGeoidoPath ジオイドモデルのパス。標高がOFFの場合、空文字を指定する。
        */
        public void SetDEG(EDITCODE_STYLE nEditCode, string sEditLat, string sEditLon, string sEditHeight, string sGeoidoPath)
        {
            //'セミ・ダイナミック補正変換を未計算にする。'2009 / 11 H.Nakamura
            m_nSemiDynaCounter = 0;

            m_sEditLat = sEditLat;
            m_sEditLon = sEditLon;
            m_sEditHeight = sEditHeight;
            m_nEditCode = (COORDINATE_TYPE)EDITCODE_STYLE.EDITCODE_COORD_DEG;

            double nLat;
            double nLon;
            nLat = Convert.ToDouble(m_sEditLat);
            nLon = Convert.ToDouble(m_sEditLon);

            if ((nEditCode & EDITCODE_STYLE.EDITCODE_VERT_HEIGHT) != 0)
            {
                m_nEditCode |= (COORDINATE_TYPE)EDITCODE_STYLE.EDITCODE_VERT_HEIGHT;
                m_nHeight = Convert.ToDouble(m_sEditHeight);
            }
            else
            {
                m_nEditCode |= (COORDINATE_TYPE)EDITCODE_STYLE.EDITCODE_VERT_ALT;
                double nGeo = 0;
                if (get_geo_height(sGeoidoPath, nLat, nLon, ref nGeo) != 0) { nGeo = 0; }
                m_nHeight = Convert.ToDouble(m_sEditHeight) + nGeo;
            }

            WGS84dms_to_WGS84xyz(nLat, nLon, m_nHeight, ref m_nX, ref m_nY, ref m_nZ);
            m_nRoundX = JpnRound(m_nX, ACCOUNT_DECIMAL_XYZ);
            m_nRoundY = JpnRound(m_nY, ACCOUNT_DECIMAL_XYZ);
            m_nRoundZ = JpnRound(m_nZ, ACCOUNT_DECIMAL_XYZ);

            //'2009/11 H.Nakamura
            //'今期座標を初期化する。
            InitKON();
            return;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '平面直角で座標値を設定する。
        '
        'nEditCode で楕円体高か標高か指定する。
        '
        '引き数：
        'nEditCode 編集コード。
        'nCoordNum 座標系番号(1～19)。
        'sEditX 入力X。
        'sEditY 入力Y。
        'sEditHeight 入力Z。
        'sGeoidoPath ジオイドモデルのパス。標高がOFFの場合、空文字を指定する。
        Public Sub SetJGD(ByVal nEditCode As EDITCODE_STYLE, ByVal nCoordNum As Long, ByVal sEditX As String, ByVal sEditY As String, ByVal sEditHeight As String, ByVal sGeoidoPath As String)

            'セミ・ダイナミック補正変換を未計算にする。'2009/11 H.Nakamura
            m_nSemiDynaCounter = 0
    
            m_sEditX = sEditX
            m_sEditY = sEditY
            m_sEditHeight = sEditHeight
            m_nEditCoordNum = nCoordNum
            m_nEditCode = EDITCODE_COORD_JGD
    
            Dim nLat As Double
            Dim nLon As Double
            Dim nHeight As Double
            Call JGDxyz_to_WGS84dms(m_nEditCoordNum, Val(m_sEditX), Val(m_sEditY), 0, nLat, nLon, nHeight)
    
            If (nEditCode And EDITCODE_VERT_HEIGHT) <> 0 Then
                m_nEditCode = m_nEditCode + EDITCODE_VERT_HEIGHT
                m_nHeight = Val(m_sEditHeight)
            Else
                m_nEditCode = m_nEditCode + EDITCODE_VERT_ALT
                Dim nGeo As Double
                If get_geo_height(sGeoidoPath, nLat, nLon, nGeo) <> 0 Then nGeo = 0
                m_nHeight = Val(m_sEditHeight) + nGeo
            End If
    
            Call WGS84dms_to_WGS84xyz(nLat, nLon, m_nHeight, m_nX, m_nY, m_nZ)
            m_nRoundX = JpnRound(m_nX, ACCOUNT_DECIMAL_XYZ)
            m_nRoundY = JpnRound(m_nY, ACCOUNT_DECIMAL_XYZ)
            m_nRoundZ = JpnRound(m_nZ, ACCOUNT_DECIMAL_XYZ)
    
            '2009/11 H.Nakamura
            '今期座標を初期化する。
            Call InitKON
    
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '平面直角で座標値を設定する。
        '
        'nEditCode で楕円体高か標高か指定する。
        '
        '引き数：
        'nEditCode 編集コード。
        'nCoordNum 座標系番号(1～19)。
        'sEditX 入力X。
        'sEditY 入力Y。
        'sEditHeight 入力Z。
        'sGeoidoPath ジオイドモデルのパス。標高がOFFの場合、空文字を指定する。
        */
        public void SetJGD(EDITCODE_STYLE nEditCode, long nCoordNum, string sEditX, string sEditY, string sEditHeight, string sGeoidoPath)
        {
            //'セミ・ダイナミック補正変換を未計算にする。'2009 / 11 H.Nakamura
            m_nSemiDynaCounter = 0;

            m_sEditX = sEditX;
            m_sEditY = sEditY;
            m_sEditHeight = sEditHeight;
            m_nEditCoordNum = nCoordNum;
            m_nEditCode = (COORDINATE_TYPE)EDITCODE_STYLE.EDITCODE_COORD_JGD;

            double nLat = 0;
            double nLon = 0;
            double nHeight = 0;
            JGDxyz_to_WGS84dms((int)m_nEditCoordNum, Convert.ToDouble(m_sEditX), Convert.ToDouble(m_sEditY), 0, ref nLat, ref nLon, ref nHeight);

            if ((nEditCode & EDITCODE_STYLE.EDITCODE_VERT_HEIGHT) != 0)
            {
                m_nEditCode |= (COORDINATE_TYPE)EDITCODE_STYLE.EDITCODE_VERT_HEIGHT;
                m_nHeight = Convert.ToDouble(m_sEditHeight);
            }
            else
            {
                m_nEditCode |= (COORDINATE_TYPE)EDITCODE_STYLE.EDITCODE_VERT_ALT;
                double nGeo = 0;
                if (get_geo_height(sGeoidoPath, nLat, nLon, ref nGeo) != 0) { nGeo = 0; }
                m_nHeight = Convert.ToDouble(m_sEditHeight) + nGeo;
            }

            WGS84dms_to_WGS84xyz(nLat, nLon, m_nHeight, ref m_nX, ref m_nY, ref m_nZ);
            m_nRoundX = JpnRound(m_nX, ACCOUNT_DECIMAL_XYZ);
            m_nRoundY = JpnRound(m_nY, ACCOUNT_DECIMAL_XYZ);
            m_nRoundZ = JpnRound(m_nZ, ACCOUNT_DECIMAL_XYZ);


            //'2009/11 H.Nakamura
            //'今期座標を初期化する。
            InitKON();
            return;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '標高を設定する。
        '
        '引き数：
        'sEditHeight 入力高さ。
        'nHeight 楕円体高。
        Public Sub SetAlt(ByVal sEditHeight As String, ByVal nHeight As Double)

            If (m_nEditCode And EDITCODE_COORD_XYZ) <> 0 Then Exit Sub
    
            'セミ・ダイナミック補正変換を未計算にする。'2009/11 H.Nakamura
            m_nSemiDynaCounter = 0
    
            m_sEditHeight = sEditHeight
    
            If (m_nEditCode And EDITCODE_VERT_HEIGHT) <> 0 Then m_nEditCode = m_nEditCode - EDITCODE_VERT_HEIGHT
            m_nEditCode = m_nEditCode Or EDITCODE_VERT_ALT
    
            m_nHeight = nHeight
    
            If (m_nEditCode And EDITCODE_COORD_DMS) <> 0 Then
                Dim nLat As Double
                Dim nLon As Double
                nLat = STR2DEG(m_sEditLat)
                nLon = STR2DEG(m_sEditLon)
            ElseIf (m_nEditCode And EDITCODE_COORD_DEG) <> 0 Then
                nLat = Val(m_sEditLat)
                nLon = Val(m_sEditLon)
            Else
                Dim nZ As Double
                Call JGDxyz_to_WGS84dms(m_nEditCoordNum, Val(m_sEditX), Val(m_sEditY), 0, nLat, nLon, nZ)
            End If
    
            Call WGS84dms_to_WGS84xyz(nLat, nLon, m_nHeight, m_nX, m_nY, m_nZ)
            m_nRoundX = JpnRound(m_nX, ACCOUNT_DECIMAL_XYZ)
            m_nRoundY = JpnRound(m_nY, ACCOUNT_DECIMAL_XYZ)
            m_nRoundZ = JpnRound(m_nZ, ACCOUNT_DECIMAL_XYZ)
    
            '2009/11 H.Nakamura
            '今期座標を初期化する。
            Call InitKON
    
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '標高を設定する。
        '
        '引き数：
        'sEditHeight 入力高さ。
        'nHeight 楕円体高。
        */
        public void SetAlt(string sEditHeight, double nHeight)
        {
            if ((m_nEditCode & (COORDINATE_TYPE)EDITCODE_STYLE.EDITCODE_COORD_XYZ) != 0)
            {
                return;
            }

            //'セミ・ダイナミック補正変換を未計算にする。'2009 / 11 H.Nakamura
            m_nSemiDynaCounter = 0;


            m_sEditHeight = sEditHeight;

            if ((m_nEditCode & (COORDINATE_TYPE)EDITCODE_STYLE.EDITCODE_VERT_HEIGHT) != 0)
            {
                m_nEditCode &= (COORDINATE_TYPE)EDITCODE_STYLE.EDITCODE_VERT_HEIGHT;
            }
            m_nEditCode |= (COORDINATE_TYPE)EDITCODE_STYLE.EDITCODE_VERT_ALT;

            m_nHeight = nHeight;

            double nLat = 0;
            double nLon = 0;
            if ((m_nEditCode & (COORDINATE_TYPE)EDITCODE_STYLE.EDITCODE_COORD_DMS) != 0)
            {
                nLat = STR2DEG(m_sEditLat);
                nLon = STR2DEG(m_sEditLon);
            }
            else if ((m_nEditCode & (COORDINATE_TYPE)EDITCODE_STYLE.EDITCODE_COORD_DEG) != 0)
            {
                nLat = Convert.ToDouble(m_sEditLat);
                nLon = Convert.ToDouble(m_sEditLon);
            }
            else
            {
                double nZ = 0;
                JGDxyz_to_WGS84dms((int)m_nEditCoordNum, Convert.ToDouble(m_sEditX), Convert.ToDouble(m_sEditY), 0, ref nLat, ref nLon, ref nZ);
            }

            WGS84dms_to_WGS84xyz(nLat, nLon, m_nHeight, ref m_nX, ref m_nY, ref m_nZ);
            m_nRoundX = JpnRound(m_nX, ACCOUNT_DECIMAL_XYZ);
            m_nRoundY = JpnRound(m_nY, ACCOUNT_DECIMAL_XYZ);
            m_nRoundZ = JpnRound(m_nZ, ACCOUNT_DECIMAL_XYZ);

            //'2009/11 H.Nakamura
            //'今期座標を初期化する。
            InitKON();
            return;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '緯度経度(度)で座標値を設定する。
        '
        'nEditCode で楕円体高か標高か指定する。
        '数値指定。
        '
        '引き数：
        'nEditCode 編集コード。
        'nEditLat 入力緯度。
        'nEditLon 入力経度。
        'nEditHeight 入力高さ。
        'sGeoidoPath ジオイドモデルのパス。標高がOFFの場合、空文字を指定する。
        Public Sub SetNumDEG(ByVal nEditCode As EDITCODE_STYLE, ByVal nEditLat As Double, ByVal nEditLon As Double, ByVal nEditHeight As Double, ByVal sGeoidoPath As String)
            Dim sEditLat As String
            Dim sEditLon As String
            Dim sEditHeight As String
            sEditLat = Format$(nEditLat, "0.0########################")
            sEditLon = Format$(nEditLon, "0.0########################")
            sEditHeight = Format$(nEditHeight, "0.0########################")
            Call SetDEG(nEditCode, sEditLat, sEditLon, sEditHeight, sGeoidoPath)
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '緯度経度(度)で座標値を設定する。
        '
        'nEditCode で楕円体高か標高か指定する。
        '数値指定。
        '
        '引き数：
        'nEditCode 編集コード。
        'nEditLat 入力緯度。
        'nEditLon 入力経度。
        'nEditHeight 入力高さ。
        'sGeoidoPath ジオイドモデルのパス。標高がOFFの場合、空文字を指定する。
        */
        public void SetNumDEG(EDITCODE_STYLE nEditCode, double nEditLat, double nEditLon, double nEditHeight, string sGeoidoPath)
        {
            string sEditLat;
            string sEditLon;
            string sEditHeight;
            sEditLat = Strings.Format(nEditLat, "0.0########################");
            sEditLon = Strings.Format(nEditLon, "0.0########################");
            sEditHeight = Strings.Format(nEditHeight, "0.0########################");
            SetDEG(nEditCode, sEditLat, sEditLon, sEditHeight, sGeoidoPath);
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '旧編集コードを設定する。
        '
        '引き数：
        'nEditCode 編集コード。
        'nCoordNum 座標系番号(1～19)。
        'sGeoidoPath ジオイドモデルのパス。標高がOFFの場合、空文字を指定する。
        Public Sub SetOldEditCode(ByVal nEditCode As EDITCODE_STYLE, ByVal nCoordNum As Long, ByVal sGeoidoPath As String)

            'セミ・ダイナミック補正変換を未計算にする。'2009/11 H.Nakamura
            m_nSemiDynaCounter = 0
    
            If (nEditCode And EDITCODE_COORD_DEG) <> 0 Then
                m_nEditCode = EDITCODE_COORD_DEG
                Dim nLat As Double
                Dim nLon As Double
                Call WGS84xyz_to_WGS84dms(m_nX, m_nY, m_nZ, nLat, nLon, m_nHeight)
                m_sEditLat = FormatRound0Trim(nLat, ACCOUNT_DECIMAL_DEGREE)
                m_sEditLon = FormatRound0Trim(nLon, ACCOUNT_DECIMAL_DEGREE)
            ElseIf (nEditCode And EDITCODE_COORD_DMS) <> 0 Then
                m_nEditCode = EDITCODE_COORD_DMS
                Call WGS84xyz_to_WGS84dms(m_nX, m_nY, m_nZ, nLat, nLon, m_nHeight)
                Dim nH As Long
                Dim nM As Long
                Dim nS As Double
                Call d_to_dms_decimal(nLat, nH, nM, nS, GUI_SEC_DECIMAL)
                m_sEditLat = DMS2STR(CStr(nH), CStr(Abs(nM)), FormatRound0Trim(Abs(nS), GUI_SEC_DECIMAL))
                Call d_to_dms_decimal(nLon, nH, nM, nS, GUI_SEC_DECIMAL)
                m_sEditLon = DMS2STR(CStr(nH), CStr(Abs(nM)), FormatRound0Trim(Abs(nS), GUI_SEC_DECIMAL))
            ElseIf (nEditCode And EDITCODE_COORD_JGD) <> 0 Then
                m_nEditCode = EDITCODE_COORD_JGD
                m_nEditCoordNum = nCoordNum
                Call WGS84xyz_to_WGS84dms(m_nX, m_nY, m_nZ, nLat, nLon, m_nHeight)
                Dim nX As Double
                Dim nY As Double
                Dim nZ As Double
                Call WGS84dms_to_JGDxyz(m_nEditCoordNum, nLat, nLon, 0, nX, nY, nZ)
                m_sEditX = FormatRound0Trim(nX, GUI_JGD_DECIMAL)
                m_sEditY = FormatRound0Trim(nY, GUI_JGD_DECIMAL)
            Else
                m_nEditCode = EDITCODE_COORD_XYZ
                m_sEditX = FormatRound0Trim(m_nX, GUI_XYZ_DECIMAL)
                m_sEditY = FormatRound0Trim(m_nY, GUI_XYZ_DECIMAL)
                m_sEditZ = FormatRound0Trim(m_nZ, GUI_XYZ_DECIMAL)
            End If
    
            If (m_nEditCode And EDITCODE_COORD_XYZ) = 0 Then
                If (nEditCode And EDITCODE_VERT_ALT) <> 0 And sGeoidoPath <> "" Then
                    Dim nGeo As Double
                    If get_geo_height(sGeoidoPath, nLat, nLon, nGeo) <> 0 Then
                        m_nEditCode = m_nEditCode + EDITCODE_VERT_HEIGHT
                        m_sEditHeight = FormatRound0Trim(m_nHeight, GUI_HEIGHT_DECIMAL)
                    Else
                        m_nEditCode = m_nEditCode + EDITCODE_VERT_ALT
                        m_sEditHeight = FormatRound0Trim(m_nHeight - nGeo, GUI_HEIGHT_DECIMAL)
                    End If
                Else
                    m_nEditCode = m_nEditCode + EDITCODE_VERT_HEIGHT
                    m_sEditHeight = FormatRound0Trim(m_nHeight, GUI_HEIGHT_DECIMAL)
                End If
            End If
    
            '2009/11 H.Nakamura
            '今期座標を初期化する。
            Call InitKON
    
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '旧編集コードを設定する。
        '
        '引き数：
        'nEditCode 編集コード。
        'nCoordNum 座標系番号(1～19)。
        'sGeoidoPath ジオイドモデルのパス。標高がOFFの場合、空文字を指定する。
        */
        public void SetOldEditCode(EDITCODE_STYLE nEditCode, long nCoordNum, string sGeoidoPath)
        {
            //'セミ・ダイナミック補正変換を未計算にする。'2009 / 11 H.Nakamura
            m_nSemiDynaCounter = 0;

            double nLat = 0;
            double nLon = 0;
            if ((nEditCode & EDITCODE_STYLE.EDITCODE_COORD_DEG) != 0)
            {
                m_nEditCode = (COORDINATE_TYPE)EDITCODE_STYLE.EDITCODE_COORD_DEG;
                WGS84xyz_to_WGS84dms(m_nX, m_nY, m_nZ, ref nLat, ref nLon, ref m_nHeight);
                m_sEditLat = FormatRound0Trim(nLat, ACCOUNT_DECIMAL_DEGREE);
                m_sEditLon = FormatRound0Trim(nLon, ACCOUNT_DECIMAL_DEGREE);
            }
            else if ((nEditCode & EDITCODE_STYLE.EDITCODE_COORD_DMS) != 0)
            {
                m_nEditCode = (COORDINATE_TYPE)EDITCODE_STYLE.EDITCODE_COORD_DMS;
                WGS84xyz_to_WGS84dms(m_nX, m_nY, m_nZ, ref nLat, ref nLon, ref m_nHeight);
                long nH = 0;
                long nM = 0;
                double nS = 0;
                //d_to_dms_decimal(nLat, ref nH, ref nM, ref nS, GUI_SEC_DECIMAL);
                D_to_Dms_decimal(nLat, ref nH, ref nM, ref nS, GUI_SEC_DECIMAL);
                m_sEditLat = DMS2STR(nH.ToString(), Math.Abs(nM).ToString(), FormatRound0Trim(Math.Abs(nS), GUI_SEC_DECIMAL));
                //d_to_dms_decimal(nLon, ref nH, ref nM, ref nS, GUI_SEC_DECIMAL);
                D_to_Dms_decimal(nLon, ref nH, ref nM, ref nS, GUI_SEC_DECIMAL);
                m_sEditLon = DMS2STR(nH.ToString(), Math.Abs(nM).ToString(), FormatRound0Trim(Math.Abs(nS), GUI_SEC_DECIMAL));
            }
            else if ((nEditCode & EDITCODE_STYLE.EDITCODE_COORD_JGD) != 0)
            {
                m_nEditCode = (COORDINATE_TYPE)EDITCODE_STYLE.EDITCODE_COORD_JGD;
                m_nEditCoordNum = nCoordNum;
                WGS84xyz_to_WGS84dms(m_nX, m_nY, m_nZ, ref nLat, ref nLon, ref m_nHeight);
                double nX = 0;
                double nY = 0;
                double nZ = 0;
                WGS84dms_to_JGDxyz((int)m_nEditCoordNum, nLat, nLon, 0, ref nX, ref nY, ref nZ);
                m_sEditX = FormatRound0Trim(nX, GUI_JGD_DECIMAL);
                m_sEditY = FormatRound0Trim(nY, GUI_JGD_DECIMAL);
            }
            else
            {
                m_nEditCode = (COORDINATE_TYPE)EDITCODE_STYLE.EDITCODE_COORD_XYZ;
                m_sEditX = FormatRound0Trim(m_nX, GUI_XYZ_DECIMAL);
                m_sEditY = FormatRound0Trim(m_nY, GUI_XYZ_DECIMAL);
                m_sEditZ = FormatRound0Trim(m_nZ, GUI_XYZ_DECIMAL);
            }

            if ((m_nEditCode & (COORDINATE_TYPE)EDITCODE_STYLE.EDITCODE_COORD_XYZ) == 0)
            {
                if ((nEditCode & EDITCODE_STYLE.EDITCODE_VERT_ALT) != 0 && sGeoidoPath != "")
                {
                    double nGeo = 0;
                    if (get_geo_height(sGeoidoPath, nLat, nLon, ref nGeo) != 0)
                    {
                        m_nEditCode |= (COORDINATE_TYPE)EDITCODE_STYLE.EDITCODE_VERT_HEIGHT;
                        m_sEditHeight = FormatRound0Trim(m_nHeight, GUI_HEIGHT_DECIMAL);
                    }
                    else
                    {
                        m_nEditCode |= (COORDINATE_TYPE)EDITCODE_STYLE.EDITCODE_VERT_ALT;
                        m_sEditHeight = FormatRound0Trim(m_nHeight - nGeo, GUI_HEIGHT_DECIMAL);
                    }
                }
                else
                {
                    m_nEditCode |= (COORDINATE_TYPE)EDITCODE_STYLE.EDITCODE_VERT_HEIGHT;
                    m_sEditHeight = FormatRound0Trim(m_nHeight, GUI_HEIGHT_DECIMAL);
                }
            }

            //'2009/11 H.Nakamura
            //'今期座標を初期化する。
            InitKON();
            return;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '2009/11 H.Nakamura
        '元期座標を緯度経度(度分秒)で取得する。
        Public Sub GetDMSGAN(nLatH As Long, nLatM As Long, nLatS As Double, nLonH As Long, nLonM As Long, nLonS As Double, nHeight As Double, vAlt As Variant, ByVal nDecimal As Long, ByVal sGeoidoPath As String)
            If (m_nEditCode And EDITCODE_COORD_DEG) <> 0 Then
                Dim nLat As Double
                Dim nLon As Double
                nLat = Val(m_sEditLat)
                nLon = Val(m_sEditLon)
                Call d_to_dms_decimal(nLat, nLatH, nLatM, nLatS, nDecimal)
                Call d_to_dms_decimal(nLon, nLonH, nLonM, nLonS, nDecimal)
            ElseIf (m_nEditCode And EDITCODE_COORD_DMS) <> 0 Then
                Call STR2DMS(m_sEditLat, nLatH, nLatM, nLatS)
                Call STR2DMS(m_sEditLon, nLonH, nLonM, nLonS)
                nLat = dms_to_d(nLatH, nLatM, nLatS)
                nLon = dms_to_d(nLonH, nLonM, nLonS)
            ElseIf (m_nEditCode And EDITCODE_COORD_JGD) <> 0 Then
                Call JGDxyz_to_WGS84dms(m_nEditCoordNum, Val(m_sEditX), Val(m_sEditY), 0, nLat, nLon, nHeight)
                Call d_to_dms_decimal(nLat, nLatH, nLatM, nLatS, nDecimal)
                Call d_to_dms_decimal(nLon, nLonH, nLonM, nLonS, nDecimal)
            Else
                Call WGS84xyz_to_WGS84dms(m_nX, m_nY, m_nZ, nLat, nLon, nHeight)
                Call d_to_dms_decimal(nLat, nLatH, nLatM, nLatS, nDecimal)
                Call d_to_dms_decimal(nLon, nLonH, nLonM, nLonS, nDecimal)
            End If
            Call GetHeightGAN(nLat, nLon, nHeight, vAlt, sGeoidoPath)
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '2009/11 H.Nakamura
        '元期座標を緯度経度(度分秒)で取得する。
        */
        public void GetDMSGAN(long nLatH, long nLatM, double nLatS, long nLonH, long nLonM, double nLonS, double nHeight, double vAlt, long nDecimal, string sGeoidoPath)
        {
            double nLat = 0;
            double nLon = 0;
            if ((m_nEditCode & (COORDINATE_TYPE)EDITCODE_STYLE.EDITCODE_COORD_DEG) != 0)
            {
                nLat = Convert.ToDouble(m_sEditLat);
                nLon = Convert.ToDouble(m_sEditLon);
                //d_to_dms_decimal(nLat, ref nLatH, ref nLatM, ref nLatS, nDecimal);
                //d_to_dms_decimal(nLon, ref nLonH, ref nLonM, ref nLonS, nDecimal);
                D_to_Dms_decimal(nLat, ref nLatH, ref nLatM, ref nLatS, nDecimal);
                D_to_Dms_decimal(nLon, ref nLonH, ref nLonM, ref nLonS, nDecimal);
            }
            else if ((m_nEditCode & (COORDINATE_TYPE)EDITCODE_STYLE.EDITCODE_COORD_DMS) != 0)
            {
                STR2DMS(m_sEditLat, ref nLatH, ref nLatM, ref nLatS);
                STR2DMS(m_sEditLon, ref nLonH, ref nLonM, ref nLonS);
                nLat = dms_to_d((int)nLatH, (int)nLatM, nLatS);
                nLon = dms_to_d((int)nLonH, (int)nLonM, nLonS);
            }
            else if ((m_nEditCode & (COORDINATE_TYPE)EDITCODE_STYLE.EDITCODE_COORD_JGD) != 0)
            {
                JGDxyz_to_WGS84dms((int)m_nEditCoordNum, Convert.ToDouble(m_sEditX), Convert.ToDouble(m_sEditY), 0, ref nLat, ref nLon, ref nHeight);
                //d_to_dms_decimal(nLat, ref nLatH, ref nLatM, ref nLatS, nDecimal);
                //d_to_dms_decimal(nLon, ref nLonH, ref nLonM, ref nLonS, nDecimal);
                D_to_Dms_decimal(nLat, ref nLatH, ref nLatM, ref nLatS, nDecimal);
                D_to_Dms_decimal(nLon, ref nLonH, ref nLonM, ref nLonS, nDecimal);
            }
            else
            {
                WGS84xyz_to_WGS84dms(m_nX, m_nY, m_nZ, ref nLat, ref nLon, ref nHeight);
                //d_to_dms_decimal(nLat, ref nLatH, ref nLatM, ref nLatS, nDecimal);
                //d_to_dms_decimal(nLon, ref nLonH, ref nLonM, ref nLonS, nDecimal);
                D_to_Dms_decimal(nLat, ref nLatH, ref nLatM, ref nLatS, nDecimal);
                D_to_Dms_decimal(nLon, ref nLonH, ref nLonM, ref nLonS, nDecimal);
            }
            GetHeightGAN(nLat, nLon, ref nHeight, ref vAlt, sGeoidoPath);
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '*******************************************************************************
        'インターフェース

        '座標値。
        Private Property Let CoordinatePoint_CoordinatePoint(ByVal RHS As CoordinatePoint)
            If RHS.CoordinateType = COORDINATE_FIX Then
                Dim clsCoordinatePointFix As CoordinatePointFix
                Set clsCoordinatePointFix = RHS
                m_nX = clsCoordinatePointFix.X
                m_nY = clsCoordinatePointFix.Y
                m_nZ = clsCoordinatePointFix.Z
                m_nRoundX = clsCoordinatePointFix.RoundX
                m_nRoundY = clsCoordinatePointFix.RoundY
                m_nRoundZ = clsCoordinatePointFix.RoundZ
                m_nEditCode = clsCoordinatePointFix.EditCode
                m_sEditX = clsCoordinatePointFix.EditX
                m_sEditY = clsCoordinatePointFix.EditY
                m_sEditZ = clsCoordinatePointFix.EditZ
                m_sEditLat = clsCoordinatePointFix.EditLat
                m_sEditLon = clsCoordinatePointFix.EditLon
                m_sEditHeight = clsCoordinatePointFix.EditHeight
                m_nEditCoordNum = clsCoordinatePointFix.EditCoordNum
                m_nHeight = clsCoordinatePointFix.Height
        
                '2009/11 H.Nakamura
                '今期座標。
                m_nLatKON = clsCoordinatePointFix.LatKON
                m_nLonKON = clsCoordinatePointFix.LonKON
                m_nHeightKON = clsCoordinatePointFix.HeightKON
                m_nXKON = clsCoordinatePointFix.XKON
                m_nYKON = clsCoordinatePointFix.YKON
                m_nZKON = clsCoordinatePointFix.ZKON
                m_nRoundXKON = clsCoordinatePointFix.RoundXKON
                m_nRoundYKON = clsCoordinatePointFix.RoundYKON
                m_nRoundZKON = clsCoordinatePointFix.RoundZKON
            Else
                m_nX = RHS.X
                m_nY = RHS.Y
                m_nZ = RHS.Z
                m_nRoundX = RHS.RoundX
                m_nRoundY = RHS.RoundY
                m_nRoundZ = RHS.RoundZ
                m_nEditCode = 0
        
                '今期座標を初期化する。
                Call InitKON
            End If
            '2009/11 H.Nakamura
            'セミ・ダイナミック補正変換を未計算にする。
            m_nSemiDynaCounter = 0
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
            if (RHS.CoordinateType() == COORDINATE_TYPE.COORDINATE_FIX)
            {
                CoordinatePointFix clsCoordinatePointFix;
                clsCoordinatePointFix = (CoordinatePointFix)RHS;
                m_nX = clsCoordinatePointFix.X;
                m_nY = clsCoordinatePointFix.Y;
                m_nZ = clsCoordinatePointFix.Z;
                m_nRoundX = clsCoordinatePointFix.RoundXX();
                m_nRoundY = clsCoordinatePointFix.RoundYY();
                m_nRoundZ = clsCoordinatePointFix.RoundZZ();
                m_nEditCode = (COORDINATE_TYPE)clsCoordinatePointFix.EditCode();
                m_sEditX = clsCoordinatePointFix.EditX();
                m_sEditY = clsCoordinatePointFix.EditY();
                m_sEditZ = clsCoordinatePointFix.EditZ();
                m_sEditLat = clsCoordinatePointFix.EditLat();
                m_sEditLon = clsCoordinatePointFix.EditLon();
                m_sEditHeight = clsCoordinatePointFix.EditHeight();
                m_nEditCoordNum = clsCoordinatePointFix.EditCoordNum();
                m_nHeight = clsCoordinatePointFix.Height();


                //'2009/11 H.Nakamura
                //'今期座標。
                m_nLatKON = clsCoordinatePointFix.LatKON();
                m_nLonKON = clsCoordinatePointFix.LonKON();
                m_nHeightKON = clsCoordinatePointFix.HeightKON();
                m_nXKON = clsCoordinatePointFix.XKON();
                m_nYKON = clsCoordinatePointFix.YKON();
                m_nZKON = clsCoordinatePointFix.ZKON();
                m_nRoundXKON = clsCoordinatePointFix.RoundXKON();
                m_nRoundYKON = clsCoordinatePointFix.RoundYKON();
                m_nRoundZKON = clsCoordinatePointFix.RoundZKON();
            }
            else
            {
                m_nX = RHS.X;
                m_nY = RHS.Y;
                m_nZ = RHS.Z;
                m_nRoundX = RHS.RoundXX();
                m_nRoundY = RHS.RoundYY();
                m_nRoundZ = RHS.RoundZZ();
                m_nEditCode = 0;


                //'今期座標を初期化する。
                InitKON();
            }
            //'2009/11 H.Nakamura
            //'セミ・ダイナミック補正変換を未計算にする。
            m_nSemiDynaCounter = 0;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'X。
        Private Property Let CoordinatePoint_X(ByVal RHS As Double)
            m_nX = RHS
            m_nRoundX = JpnRound(m_nX, ACCOUNT_DECIMAL_XYZ)
            m_nEditCode = 0
            '2009/11 H.Nakamura
            'セミ・ダイナミック補正変換を未計算にする。
            m_nSemiDynaCounter = 0
            '今期座標を初期化する。
            Call InitKON
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'X。
        Private Property Get CoordinatePoint_X() As Double
            '2009/11 H.Nakamura
            If mdlSemiDyna.m_bEnable Then
                If m_nSemiDynaCounter < mdlSemiDyna.m_nCounter Then Call ConvSemiDyna
                CoordinatePoint_X = m_nXKON
            Else
                CoordinatePoint_X = m_nX
            End If
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'Y。
        Private Property Let CoordinatePoint_Y(ByVal RHS As Double)
            m_nY = RHS
            m_nRoundY = JpnRound(m_nY, ACCOUNT_DECIMAL_XYZ)
            m_nEditCode = 0
            '2009/11 H.Nakamura
            'セミ・ダイナミック補正変換を未計算にする。
            m_nSemiDynaCounter = 0
            '今期座標を初期化する。
            Call InitKON
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'Y。
        Private Property Get CoordinatePoint_Y() As Double
            '2009/11 H.Nakamura
            If mdlSemiDyna.m_bEnable Then
                If m_nSemiDynaCounter < mdlSemiDyna.m_nCounter Then Call ConvSemiDyna
                CoordinatePoint_Y = m_nYKON
            Else
                CoordinatePoint_Y = m_nY
            End If
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'Z。
        Private Property Let CoordinatePoint_Z(ByVal RHS As Double)
            m_nZ = RHS
            m_nRoundZ = JpnRound(m_nZ, ACCOUNT_DECIMAL_XYZ)
            m_nEditCode = 0
            '2009/11 H.Nakamura
            'セミ・ダイナミック補正変換を未計算にする。
            m_nSemiDynaCounter = 0
            '今期座標を初期化する。
            Call InitKON
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'Z。
        Private Property Get CoordinatePoint_Z() As Double
            '2009/11 H.Nakamura
            If mdlSemiDyna.m_bEnable Then
                If m_nSemiDynaCounter < mdlSemiDyna.m_nCounter Then Call ConvSemiDyna
                CoordinatePoint_Z = m_nZKON
            Else
                CoordinatePoint_Z = m_nZ
            End If
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '丸めX。
        Private Property Get CoordinatePoint_RoundX() As Double
            '2009/11 H.Nakamura
            If mdlSemiDyna.m_bEnable Then
                If m_nSemiDynaCounter < mdlSemiDyna.m_nCounter Then Call ConvSemiDyna
                CoordinatePoint_RoundX = m_nRoundXKON
            Else
                CoordinatePoint_RoundX = m_nRoundX
            End If
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '丸めY。
        Private Property Get CoordinatePoint_RoundY() As Double
            '2009/11 H.Nakamura
            If mdlSemiDyna.m_bEnable Then
                If m_nSemiDynaCounter < mdlSemiDyna.m_nCounter Then Call ConvSemiDyna
                CoordinatePoint_RoundY = m_nRoundYKON
            Else
                CoordinatePoint_RoundY = m_nRoundY
            End If
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '丸めZ。
        Private Property Get CoordinatePoint_RoundZ() As Double
            '2009/11 H.Nakamura
            If mdlSemiDyna.m_bEnable Then
                If m_nSemiDynaCounter < mdlSemiDyna.m_nCounter Then Call ConvSemiDyna
                CoordinatePoint_RoundZ = m_nRoundZKON
            Else
                CoordinatePoint_RoundZ = m_nRoundZ
            End If
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
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
            Put #nFile, , m_nHeight
            Put #nFile, , m_nEditCode
            Call PutString(nFile, m_sEditX)
            Call PutString(nFile, m_sEditY)
            Call PutString(nFile, m_sEditZ)
            Call PutString(nFile, m_sEditLat)
            Call PutString(nFile, m_sEditLon)
            Call PutString(nFile, m_sEditHeight)
            Put #nFile, , m_nEditCoordNum
    
        #If SEMIDYNA <> 0 Then
            '2009/11 H.Nakamura
            '今期座標。
            Put #nFile, , m_nRoundXKON
            Put #nFile, , m_nRoundYKON
            Put #nFile, , m_nRoundZKON
            Put #nFile, , m_nXKON
            Put #nFile, , m_nYKON
            Put #nFile, , m_nZKON
            Put #nFile, , m_nLatKON
            Put #nFile, , m_nLonKON
            Put #nFile, , m_nHeightKON
        #End If
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'保存。
        private void CoordinatePoint_Save(int nFile)
        {
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
            If nVersion < 6400 Then
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
            If nVersion < 6400 Then
                m_nHeight = 0 '6200 <= nVersion < 6400 の間は開発㊥なので気にしない。
            Else
                Get #nFile, , m_nHeight
            End If
            If nVersion < 6200 Then
                m_nEditCode = 0
            Else
                Get #nFile, , m_nEditCode
                m_sEditX = GetString(nFile)
                m_sEditY = GetString(nFile)
                m_sEditZ = GetString(nFile)
                m_sEditLat = GetString(nFile)
                m_sEditLon = GetString(nFile)
                m_sEditHeight = GetString(nFile)
                Get #nFile, , m_nEditCoordNum
            End If
    
            '2009/11 H.Nakamura
            '今期座標。
            If nVersion < 8130 Then
                Call InitKON
            Else
        #If SEMIDYNA <> 0 Then
                Get #nFile, , m_nRoundXKON
                Get #nFile, , m_nRoundYKON
                Get #nFile, , m_nRoundZKON
                Get #nFile, , m_nXKON
                Get #nFile, , m_nYKON
                Get #nFile, , m_nZKON
                Get #nFile, , m_nLatKON
                Get #nFile, , m_nLonKON
                Get #nFile, , m_nHeightKON
        
                '8202以前には間違いがあったのでなかったことにする。
                If nVersion < 8202 Then Call InitKON
        #Else
                Call InitKON
        #End If
            End If
    
            'セミ・ダイナミック補正変換を未計算にする。'2009/11 H.Nakamura
            m_nSemiDynaCounter = 0
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'読み込み。
        private void CoordinatePoint_Load(int nFile, long nVersion)
        {
            return;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '指定されたオブジェクトと比較する。
        Private Function CoordinatePoint_Compare(ByVal clsCoordinatePoint As CoordinatePoint) As Boolean

            CoordinatePoint_Compare = False
    
            If clsCoordinatePoint.CoordinateType = COORDINATE_FIX Then
                Dim clsCoordinatePointFix As CoordinatePointFix
                Set clsCoordinatePointFix = clsCoordinatePoint
                If m_nEditCode <> clsCoordinatePointFix.EditCode Then Exit Function
                If (m_nEditCode And EDITCODE_COORD_XYZ) <> 0 Then
                    If Abs(Val(m_sEditX) - Val(clsCoordinatePointFix.EditX)) >= FLT_EPSILON Then Exit Function
                    If Abs(Val(m_sEditY) - Val(clsCoordinatePointFix.EditY)) >= FLT_EPSILON Then Exit Function
                    If Abs(Val(m_sEditZ) - Val(clsCoordinatePointFix.EditZ)) >= FLT_EPSILON Then Exit Function
                ElseIf m_nEditCode = 0 Then
                    If Abs(m_nX - clsCoordinatePoint.X) >= FLT_EPSILON Then Exit Function
                    If Abs(m_nY - clsCoordinatePoint.Y) >= FLT_EPSILON Then Exit Function
                    If Abs(m_nZ - clsCoordinatePoint.Z) >= FLT_EPSILON Then Exit Function
                Else
                    If (m_nEditCode And EDITCODE_COORD_DEG) <> 0 Then
                        If Abs(Val(m_sEditLat) - Val(clsCoordinatePointFix.EditLat)) >= FLT_EPSILON Then Exit Function
                        If Abs(Val(m_sEditLon) - Val(clsCoordinatePointFix.EditLon)) >= FLT_EPSILON Then Exit Function
                    ElseIf (m_nEditCode And EDITCODE_COORD_DMS) <> 0 Then
                        Dim nSrcH As Long
                        Dim nSrcM As Long
                        Dim nSrcS As Double
                        Dim nDstH As Long
                        Dim nDstM As Long
                        Dim nDstS As Double
                        Call STR2DMS(m_sEditLat, nSrcH, nSrcM, nSrcS)
                        Call STR2DMS(clsCoordinatePointFix.EditLat, nDstH, nDstM, nDstS)
                        If nSrcH <> nDstH Then Exit Function
                        If nSrcM <> nDstM Then Exit Function
                        If Abs(nSrcS - nDstS) >= FLT_EPSILON Then Exit Function
                        Call STR2DMS(m_sEditLon, nSrcH, nSrcM, nSrcS)
                        Call STR2DMS(clsCoordinatePointFix.EditLon, nDstH, nDstM, nDstS)
                        If nSrcH <> nDstH Then Exit Function
                        If nSrcM <> nDstM Then Exit Function
                        If Abs(nSrcS - nDstS) >= FLT_EPSILON Then Exit Function
                    ElseIf (m_nEditCode And EDITCODE_COORD_JGD) <> 0 Then
                        If m_nEditCoordNum <> clsCoordinatePointFix.EditCoordNum Then Exit Function
                        If Abs(Val(m_sEditX) - Val(clsCoordinatePointFix.EditX)) >= FLT_EPSILON Then Exit Function
                        If Abs(Val(m_sEditY) - Val(clsCoordinatePointFix.EditY)) >= FLT_EPSILON Then Exit Function
                    Else
                        Exit Function
                    End If
                    If Abs(Val(m_sEditHeight) - Val(clsCoordinatePointFix.EditHeight)) >= FLT_EPSILON Then Exit Function
                    If Abs(m_nHeight - clsCoordinatePointFix.Height) >= FLT_EPSILON Then Exit Function
                End If
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
            if (clsCoordinatePoint.CoordinateType() == COORDINATE_TYPE.COORDINATE_FIX)
            {
                CoordinatePointFix clsCoordinatePointFix;
                clsCoordinatePointFix = (CoordinatePointFix)clsCoordinatePoint;
                if (m_nEditCode != (COORDINATE_TYPE)clsCoordinatePointFix.EditCode())
                {
                    return false;
                }
                if ((m_nEditCode & (COORDINATE_TYPE)EDITCODE_STYLE.EDITCODE_COORD_XYZ) != 0)
                {
                    if (Math.Abs(Convert.ToDouble(m_sEditX) - Convert.ToDouble(clsCoordinatePointFix.EditX())) >= FLT_EPSILON) { return false; }
                    if (Math.Abs(Convert.ToDouble(m_sEditY) - Convert.ToDouble(clsCoordinatePointFix.EditY())) >= FLT_EPSILON) { return false; }
                    if (Math.Abs(Convert.ToDouble(m_sEditZ) - Convert.ToDouble(clsCoordinatePointFix.EditZ())) >= FLT_EPSILON) { return false; }
                }
                else if (m_nEditCode == 0)
                {
                    if (Math.Abs(m_nX - clsCoordinatePoint.X) >= FLT_EPSILON) { return false; }
                    if (Math.Abs(m_nY - clsCoordinatePoint.Y) >= FLT_EPSILON) { return false; }
                    if (Math.Abs(m_nZ - clsCoordinatePoint.Z) >= FLT_EPSILON) { return false; }
                }
                else
                {
                    if ((m_nEditCode & (COORDINATE_TYPE)EDITCODE_STYLE.EDITCODE_COORD_DEG) != 0)
                    {
                        if (Math.Abs(Convert.ToDouble(m_sEditLat) - Convert.ToDouble(clsCoordinatePointFix.EditLat())) >= FLT_EPSILON) { return false; }
                        if (Math.Abs(Convert.ToDouble(m_sEditLon) - Convert.ToDouble(clsCoordinatePointFix.EditLon())) >= FLT_EPSILON) { return false; }
                    }
                    else if ((m_nEditCode & (COORDINATE_TYPE)EDITCODE_STYLE.EDITCODE_COORD_DMS) != 0)
                    {
                        long nSrcH = 0;
                        long nSrcM = 0;
                        double nSrcS = 0;
                        long nDstH = 0;
                        long nDstM = 0;
                        double nDstS = 0;
                        STR2DMS(m_sEditLat, ref nSrcH, ref nSrcM, ref nSrcS);
                        STR2DMS(clsCoordinatePointFix.EditLat(), ref nDstH, ref nDstM, ref nDstS);
                        if (nSrcH != nDstH) { return false; }
                        if (nSrcM != nDstM) { return false; }
                        if (Math.Abs(nSrcS - nDstS) >= FLT_EPSILON) { return false; }
                        STR2DMS(m_sEditLon, ref nSrcH, ref nSrcM, ref nSrcS);
                        STR2DMS(clsCoordinatePointFix.EditLon(), ref nDstH, ref nDstM, ref nDstS);
                        if (nSrcH != nDstH) { return false; }
                        if (nSrcM != nDstM) { return false; }
                        if (Math.Abs(nSrcS - nDstS) >= FLT_EPSILON) { return false; }
                    }
                    else if ((m_nEditCode & (COORDINATE_TYPE)EDITCODE_STYLE.EDITCODE_COORD_JGD) != 0)
                    {
                        if (m_nEditCoordNum != clsCoordinatePointFix.EditCoordNum())
                        if (Math.Abs(Convert.ToDouble(m_sEditX) - Convert.ToDouble(clsCoordinatePointFix.EditX())) >= FLT_EPSILON) { return false; }
                        if (Math.Abs(Convert.ToDouble(m_sEditY) - Convert.ToDouble(clsCoordinatePointFix.EditY())) >= FLT_EPSILON) { return false; }
                    }
                    else
                    {
                        return false;
                    }
                    if (Math.Abs(Convert.ToDouble(m_sEditHeight) - Convert.ToDouble(clsCoordinatePointFix.EditHeight())) >= FLT_EPSILON) { return false; }
                    if (Math.Abs(m_nHeight - clsCoordinatePointFix.Height()) >= FLT_EPSILON) { return false; }
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
            '2009/11 H.Nakamura
            If mdlSemiDyna.m_bEnable Then
                If m_nSemiDynaCounter < mdlSemiDyna.m_nCounter Then Call ConvSemiDyna
                nX = m_nXKON
                nY = m_nYKON
                nZ = m_nZKON
            Else
                nX = m_nX
                nY = m_nY
                nZ = m_nZ
            End If
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'地心直交座標を取得する。
        private void CoordinatePoint_GetXYZ(ref double nX, ref double nY, ref double nZ)
        {
            //'2009/11 H.Nakamura
            if (MdlSemiDyna.m_bEnable)
            {
                if (m_nSemiDynaCounter < MdlSemiDyna.m_nCounter) { ConvSemiDyna(); }
                nX = m_nXKON;
                nY = m_nYKON;
                nZ = m_nZKON;
            }
            else
            {
                nX = m_nX;
                nY = m_nY;
                nZ = m_nZ;
            }
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '緯度経度(度)を取得する。
        Private Sub CoordinatePoint_GetDEG(nLat As Double, nLon As Double, nHeight As Double, vAlt As Variant, ByVal sGeoidoPath As String)
            '2009/11 H.Nakamura
            If mdlSemiDyna.m_bEnable Then
                If m_nSemiDynaCounter < mdlSemiDyna.m_nCounter Then Call ConvSemiDyna
                nLat = m_nLatKON
                nLon = m_nLonKON
                Call GetHeightKON(nHeight, vAlt, sGeoidoPath)
            Else
                If (m_nEditCode And EDITCODE_COORD_DEG) <> 0 Then
                    nLat = Val(m_sEditLat)
                    nLon = Val(m_sEditLon)
                ElseIf (m_nEditCode And EDITCODE_COORD_DMS) <> 0 Then
                    nLat = STR2DEG(m_sEditLat)
                    nLon = STR2DEG(m_sEditLon)
                ElseIf (m_nEditCode And EDITCODE_COORD_JGD) <> 0 Then
                    Call JGDxyz_to_WGS84dms(m_nEditCoordNum, Val(m_sEditX), Val(m_sEditY), 0, nLat, nLon, nHeight)
                Else
                    Call WGS84xyz_to_WGS84dms(m_nX, m_nY, m_nZ, nLat, nLon, nHeight)
                End If
                Call GetHeightGAN(nLat, nLon, nHeight, vAlt, sGeoidoPath)
            End If
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'緯度経度(度)を取得する。
        private void CoordinatePoint_GetDEG(double nLat, double nLon, double nHeight, double vAlt, string sGeoidoPath)
        {
            //'2009/11 H.Nakamura
            if (MdlSemiDyna.m_bEnable)
            {
                if (m_nSemiDynaCounter < MdlSemiDyna.m_nCounter) { ConvSemiDyna(); }
                nLat = m_nLatKON;
                nLon = m_nLonKON;
                GetHeightKON(ref nHeight, ref vAlt, sGeoidoPath);
            }
            else
            {
                if ((m_nEditCode & (COORDINATE_TYPE)EDITCODE_STYLE.EDITCODE_COORD_DEG) != 0)
                {
                    nLat = Convert.ToDouble(m_sEditLat);
                    nLon = Convert.ToDouble(m_sEditLon);
                }
                else if ((m_nEditCode & (COORDINATE_TYPE)EDITCODE_STYLE.EDITCODE_COORD_DMS) != 0)
                {
                    nLat = STR2DEG(m_sEditLat);
                    nLon = STR2DEG(m_sEditLon);
                }
                else if ((m_nEditCode & (COORDINATE_TYPE)EDITCODE_STYLE.EDITCODE_COORD_JGD) != 0)
                {
                    JGDxyz_to_WGS84dms((int)m_nEditCoordNum, Convert.ToDouble(m_sEditX), Convert.ToDouble(m_sEditY), 0, ref nLat, ref nLon, ref nHeight);
                }
                else
                {
                    WGS84xyz_to_WGS84dms(m_nX, m_nY, m_nZ, ref nLat, ref nLon, ref nHeight);
                }
                GetHeightGAN(nLat, nLon, ref nHeight, ref vAlt, sGeoidoPath);
            }
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '緯度経度(度分秒)を取得する。
        Private Sub CoordinatePoint_GetDMS(nLatH As Long, nLatM As Long, nLatS As Double, nLonH As Long, nLonM As Long, nLonS As Double, nHeight As Double, vAlt As Variant, ByVal nDecimal As Long, ByVal sGeoidoPath As String)
            '2009/11 H.Nakamura
            If mdlSemiDyna.m_bEnable Then
                If m_nSemiDynaCounter < mdlSemiDyna.m_nCounter Then Call ConvSemiDyna
                Call d_to_dms_decimal(m_nLatKON, nLatH, nLatM, nLatS, nDecimal)
                Call d_to_dms_decimal(m_nLonKON, nLonH, nLonM, nLonS, nDecimal)
                Call GetHeightKON(nHeight, vAlt, sGeoidoPath)
            Else
                If (m_nEditCode And EDITCODE_COORD_DEG) <> 0 Then
                    Dim nLat As Double
                    Dim nLon As Double
                    nLat = Val(m_sEditLat)
                    nLon = Val(m_sEditLon)
                    Call d_to_dms_decimal(nLat, nLatH, nLatM, nLatS, nDecimal)
                    Call d_to_dms_decimal(nLon, nLonH, nLonM, nLonS, nDecimal)
                ElseIf (m_nEditCode And EDITCODE_COORD_DMS) <> 0 Then
                    Call STR2DMS(m_sEditLat, nLatH, nLatM, nLatS)
                    Call STR2DMS(m_sEditLon, nLonH, nLonM, nLonS)
                    nLat = dms_to_d(nLatH, nLatM, nLatS)
                    nLon = dms_to_d(nLonH, nLonM, nLonS)
                ElseIf (m_nEditCode And EDITCODE_COORD_JGD) <> 0 Then
                    Call JGDxyz_to_WGS84dms(m_nEditCoordNum, Val(m_sEditX), Val(m_sEditY), 0, nLat, nLon, nHeight)
                    Call d_to_dms_decimal(nLat, nLatH, nLatM, nLatS, nDecimal)
                    Call d_to_dms_decimal(nLon, nLonH, nLonM, nLonS, nDecimal)
                Else
                    Call WGS84xyz_to_WGS84dms(m_nX, m_nY, m_nZ, nLat, nLon, nHeight)
                    Call d_to_dms_decimal(nLat, nLatH, nLatM, nLatS, nDecimal)
                    Call d_to_dms_decimal(nLon, nLonH, nLonM, nLonS, nDecimal)
                End If
                Call GetHeightGAN(nLat, nLon, nHeight, vAlt, sGeoidoPath)
            End If
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'緯度経度(度分秒)を取得する。
        private void CoordinatePoint_GetDMS(long nLatH, long nLatM, double nLatS, long nLonH, long nLonM, double nLonS, double nHeight, double vAlt, long nDecimal, string sGeoidoPath)
        {
            //'2009/11 H.Nakamura
            if (MdlSemiDyna.m_bEnable)
            {
                if (m_nSemiDynaCounter < MdlSemiDyna.m_nCounter) { ConvSemiDyna(); }
                //d_to_dms_decimal(m_nLatKON, ref nLatH, ref nLatM, ref nLatS, nDecimal);
                //d_to_dms_decimal(m_nLonKON, ref nLonH, ref nLonM, ref nLonS, nDecimal);
                D_to_Dms_decimal(m_nLatKON, ref nLatH, ref nLatM, ref nLatS, nDecimal);
                D_to_Dms_decimal(m_nLonKON, ref nLonH, ref nLonM, ref nLonS, nDecimal);
                GetHeightKON(ref nHeight, ref vAlt, sGeoidoPath);
            }
            else
            {
                double nLat = 0;
                double nLon = 0;
                if ((m_nEditCode & (COORDINATE_TYPE)EDITCODE_STYLE.EDITCODE_COORD_DEG) != 0)
                {
                    nLat = Convert.ToDouble(m_sEditLat);
                    nLon = Convert.ToDouble(m_sEditLon);
                    //d_to_dms_decimal(nLat, ref nLatH, ref nLatM, ref nLatS, nDecimal);
                    //d_to_dms_decimal(nLon, ref nLonH, ref nLonM, ref nLonS, nDecimal);
                    D_to_Dms_decimal(nLat, ref nLatH, ref nLatM, ref nLatS, nDecimal);
                    D_to_Dms_decimal(nLon, ref nLonH, ref nLonM, ref nLonS, nDecimal);
                }
                else if ((m_nEditCode & (COORDINATE_TYPE)EDITCODE_STYLE.EDITCODE_COORD_DMS) != 0)
                {
                    STR2DMS(m_sEditLat, ref nLatH, ref nLatM, ref nLatS);
                    STR2DMS(m_sEditLon, ref nLonH, ref nLonM, ref nLonS);
                    nLat = dms_to_d((int)nLatH, (int)nLatM, nLatS);
                    nLon = dms_to_d((int)nLonH, (int)nLonM, nLonS);
                }
                else if ((m_nEditCode & (COORDINATE_TYPE)EDITCODE_STYLE.EDITCODE_COORD_JGD) != 0)
                {
                    JGDxyz_to_WGS84dms((int)m_nEditCoordNum, Convert.ToDouble(m_sEditX), Convert.ToDouble(m_sEditY), 0, ref nLat, ref nLon, ref nHeight);
                    //d_to_dms_decimal(nLat, ref nLatH, ref nLatM, ref nLatS, nDecimal);
                    //d_to_dms_decimal(nLon, ref nLonH, ref nLonM, ref nLonS, nDecimal);
                    D_to_Dms_decimal(nLat, ref nLatH, ref nLatM, ref nLatS, nDecimal);
                    D_to_Dms_decimal(nLon, ref nLonH, ref nLonM, ref nLonS, nDecimal);
                }
                else
                {
                    WGS84xyz_to_WGS84dms(m_nX, m_nY, m_nZ, ref nLat, ref nLon, ref nHeight);
                    //d_to_dms_decimal(nLat, ref nLatH, ref nLatM, ref nLatS, nDecimal);
                    //d_to_dms_decimal(nLon, ref nLonH, ref nLonM, ref nLonS, nDecimal);
                    D_to_Dms_decimal(nLat, ref nLatH, ref nLatM, ref nLatS, nDecimal);
                    D_to_Dms_decimal(nLon, ref nLonH, ref nLonM, ref nLonS, nDecimal);
                }
                GetHeightGAN(nLat, nLon, ref nHeight, ref vAlt, sGeoidoPath);
            }
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '平面直角座標を取得する。
        Private Sub CoordinatePoint_GetJGD(nX As Double, nY As Double, nHeight As Double, vAlt As Variant, ByVal nDecimal As Long, ByVal nCoordNum As Long, ByVal sGeoidoPath As String)
            '2009/11 H.Nakamura
            If mdlSemiDyna.m_bEnable Then
                If m_nSemiDynaCounter < mdlSemiDyna.m_nCounter Then Call ConvSemiDyna
                Dim nZ As Double
                Call WGS84dms_to_JGDxyz(nCoordNum, m_nLatKON, m_nLonKON, 0, nX, nY, nZ)
                Call GetHeightKON(nHeight, vAlt, sGeoidoPath)
            Else
                If (m_nEditCode And EDITCODE_COORD_DEG) <> 0 Then
                    Dim nLat As Double
                    Dim nLon As Double
                    nLat = Val(m_sEditLat)
                    nLon = Val(m_sEditLon)
                    Call WGS84dms_to_JGDxyz(nCoordNum, nLat, nLon, 0, nX, nY, nZ)
                ElseIf (m_nEditCode And EDITCODE_COORD_DMS) <> 0 Then
                    nLat = STR2DEG(m_sEditLat)
                    nLon = STR2DEG(m_sEditLon)
                    Call WGS84dms_to_JGDxyz(nCoordNum, nLat, nLon, 0, nX, nY, nZ)
                ElseIf (m_nEditCode And EDITCODE_COORD_JGD) <> 0 Then
                    nX = Val(m_sEditX)
                    nY = Val(m_sEditY)
                    If nCoordNum <> m_nEditCoordNum Then
                        Call JGDxyz_to_WGS84dms(m_nEditCoordNum, nX, nY, 0, nLat, nLon, nHeight)
                        Call WGS84dms_to_JGDxyz(nCoordNum, nLat, nLon, 0, nX, nY, nZ)
                    Else
                        Call JGDxyz_to_WGS84dms(nCoordNum, nX, nY, 0, nLat, nLon, nHeight)
                    End If
                Else
                    Call WGS84xyz_to_WGS84dms(m_nX, m_nY, m_nZ, nLat, nLon, nHeight)
                    Call WGS84dms_to_JGDxyz(nCoordNum, nLat, nLon, 0, nX, nY, nZ)
                End If
                Call GetHeightGAN(nLat, nLon, nHeight, vAlt, sGeoidoPath)
            End If
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'平面直角座標を取得する。
        private void CoordinatePoint_GetJGD(double nX, double nY, double nHeight, double vAlt, long nDecimal, long nCoordNum, string sGeoidoPath)
        {
            //'2009/11 H.Nakamura
            double nZ = 0;
            if (MdlSemiDyna.m_bEnable)
            {
                if (m_nSemiDynaCounter < MdlSemiDyna.m_nCounter) { ConvSemiDyna(); }
                WGS84dms_to_JGDxyz((int)nCoordNum, m_nLatKON, m_nLonKON, 0, ref nX, ref nY, ref nZ);
                GetHeightKON(ref nHeight, ref vAlt, sGeoidoPath);
            }
            else
            {
                double nLat = 0;
                double nLon = 0;
                if ((m_nEditCode & (COORDINATE_TYPE)EDITCODE_STYLE.EDITCODE_COORD_DEG) != 0)
                {
                    nLat = Convert.ToDouble(m_sEditLat);
                    nLon = Convert.ToDouble(m_sEditLon);
                    WGS84dms_to_JGDxyz((int)nCoordNum, nLat, nLon, 0, ref nX, ref nY, ref nZ);
                }
                else if ((m_nEditCode & (COORDINATE_TYPE)EDITCODE_STYLE.EDITCODE_COORD_DMS) != 0)
                {
                    nLat = STR2DEG(m_sEditLat);
                    nLon = STR2DEG(m_sEditLon);
                    WGS84dms_to_JGDxyz((int)nCoordNum, nLat, nLon, 0, ref nX, ref nY, ref nZ);
                }
                else if ((m_nEditCode & (COORDINATE_TYPE)EDITCODE_STYLE.EDITCODE_COORD_JGD) != 0)
                {
                    nX = Convert.ToDouble(m_sEditX);
                    nY = Convert.ToDouble(m_sEditY);
                    if (nCoordNum != m_nEditCoordNum)
                    {
                        JGDxyz_to_WGS84dms((int)m_nEditCoordNum, nX, nY, 0, ref nLat, ref nLon, ref nHeight);
                        WGS84dms_to_JGDxyz((int)nCoordNum, nLat, nLon, 0, ref nX, ref nY, ref nZ);
                    }
                    else
                    {
                        JGDxyz_to_WGS84dms((int)nCoordNum, nX, nY, 0, ref nLat, ref nLon, ref nHeight);
                    }
                }
                else
                {
                    WGS84xyz_to_WGS84dms(m_nX, m_nY, m_nZ, ref nLat, ref nLon, ref nHeight);
                    WGS84dms_to_JGDxyz((int)nCoordNum, nLat, nLon, 0, ref nX, ref nY, ref nZ);
                }
                GetHeightGAN(nLat, nLon, ref nHeight, ref vAlt, sGeoidoPath);
            }
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'コピーしたオブジェクトを生成して返す。
        Private Function CoordinatePoint_CreateCopy() As CoordinatePoint
            Dim clsCoordinatePoint As CoordinatePoint
            Set clsCoordinatePoint = New CoordinatePointFix
            Let clsCoordinatePoint = Me
            Set CoordinatePoint_CreateCopy = clsCoordinatePoint
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'コピーしたオブジェクトを生成して返す。
        private CoordinatePoint CoordinatePoint_CreateCopy()
        {
            CoordinatePoint clsCoordinatePoint;
            clsCoordinatePoint = new CoordinatePointFix();
            clsCoordinatePoint = this;
            return clsCoordinatePoint;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '*******************************************************************************
        'インプリメンテーション

        '2009/11 H.Nakamura
        '元期座標の高さを取得する。
        '
        '引き数：
        'nLat 緯度(度)。
        'nLon 経度(度)。
        'nHeight 楕円体高が設定される。EDITCODE_COORD_XYZ の場合は設定されない(使用されない)。
        'vAlt 標高が設定される。標高が取得されない場合は null が設定される。
        'sGeoidoPath ジオイドモデルのパス。標高がOFFの場合、空文字を指定する。
        Private Sub GetHeightGAN(ByVal nLat As Double, ByVal nLon As Double, ByRef nHeight As Double, ByRef vAlt As Variant, ByVal sGeoidoPath As String)
            If (m_nEditCode And EDITCODE_VERT_ALT) <> 0 Then
                vAlt = Val(m_sEditHeight)
                If sGeoidoPath <> "" Then
                    Dim nGeoido As Double
                    If get_geo_height(sGeoidoPath, nLat, nLon, nGeoido) = 0 Then
                        nHeight = vAlt + nGeoido
                    Else
                        nHeight = m_nHeight
                    End If
                Else
                    nHeight = m_nHeight
                End If
            Else
                If (m_nEditCode And EDITCODE_VERT_HEIGHT) <> 0 Then nHeight = Val(m_sEditHeight)
                If sGeoidoPath <> "" Then
                    If get_geo_height(sGeoidoPath, nLat, nLon, nGeoido) = 0 Then
                        vAlt = nHeight - nGeoido
                    Else
                        vAlt = Null
                    End If
                Else
                    vAlt = Null
                End If
            End If
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '*******************************************************************************
        'インプリメンテーション

        '2009/11 H.Nakamura
        '元期座標の高さを取得する。
        '
        '引き数：
        'nLat 緯度(度)。
        'nLon 経度(度)。
        'nHeight 楕円体高が設定される。EDITCODE_COORD_XYZ の場合は設定されない(使用されない)。
        'vAlt 標高が設定される。標高が取得されない場合は null が設定される。
        'sGeoidoPath ジオイドモデルのパス。標高がOFFの場合、空文字を指定する。
        */
        private void GetHeightGAN(double nLat, double nLon, ref double nHeight, ref double vAlt, string sGeoidoPath)
        {
            double nGeoido = 0;
            if ((m_nEditCode & (COORDINATE_TYPE)EDITCODE_STYLE.EDITCODE_VERT_ALT) != 0)
            {
                vAlt = Convert.ToDouble(m_sEditHeight);
                if (sGeoidoPath != "")
                {
                    if (get_geo_height(sGeoidoPath, nLat, nLon, ref nGeoido) == 0)
                    {
                        nHeight = vAlt + nGeoido;
                    }
                    else
                    {
                        nHeight = m_nHeight;
                    }
                }
                else
                {
                    nHeight = m_nHeight;
                }
            }
            else
            {
                if ((m_nEditCode & (COORDINATE_TYPE)EDITCODE_STYLE.EDITCODE_VERT_HEIGHT) != 0) { nHeight = Convert.ToDouble(m_sEditHeight); }
                if (sGeoidoPath != "")
                {
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
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '2009/11 H.Nakamura
        '今期座標の高さを取得する。
        '
        '引き数：
        'nHeight 楕円体高が設定される。
        'vAlt 標高が設定される。標高が取得されない場合は null が設定される。
        'sGeoidoPath ジオイドモデルのパス。標高がOFFの場合、空文字を指定する。
        Private Sub GetHeightKON(ByRef nHeight As Double, ByRef vAlt As Variant, ByVal sGeoidoPath As String)
            nHeight = m_nHeightKON
            If sGeoidoPath <> "" Then
                Dim nGeoido As Double
                If get_geo_height(sGeoidoPath, m_nLatKON, m_nLonKON, nGeoido) = 0 Then
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
        '2009/11 H.Nakamura
        '今期座標の高さを取得する。
        '
        '引き数：
        'nHeight 楕円体高が設定される。
        'vAlt 標高が設定される。標高が取得されない場合は null が設定される。
        'sGeoidoPath ジオイドモデルのパス。標高がOFFの場合、空文字を指定する。
        */
        private void GetHeightKON(ref double nHeight, ref double vAlt, string sGeoidoPath)
        {
            nHeight = m_nHeightKON;
            if (sGeoidoPath != "")
            {
                double nGeoido = 0;
                if (get_geo_height(sGeoidoPath, m_nLatKON, m_nLonKON, ref nGeoido) == 0)
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

        //==========================================================================================
        /*[VB]
        '度分秒(文字列)を文字列に変換する。
        '
        '引き数：
        'sH 度。
        'sM 分。
        'sS 秒。
        '
        '戻り値：度分秒をあらわす文字列。
        Public Function DMS2STR(ByVal sH As String, ByVal sM As String, ByVal sS As String) As String
            sS = LTrimEx(sS, "-")
            sS = LTrimEx(sS, "0")
            Dim nPos As Long
            nPos = InStr(sS, ".")
            If 1 < nPos Then
                sS = RTrimEx(sS, "0")
                sS = RTrimEx(sS, ".")
            ElseIf nPos = 1 Then
                sS = "0" & sS
                sS = RTrimEx(sS, "0")
                sS = RTrimEx(sS, ".")
            ElseIf Len(sS) <= 0 Then
                sS = "0"
            End If
            DMS2STR = Format$(Val(sH), "+000;-000") & Format$(Val(sM), "00") & sS
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '度分秒(文字列)を文字列に変換する。
        '
        '引き数：
        'sH 度。
        'sM 分。
        'sS 秒。
        '
        '戻り値：度分秒をあらわす文字列。
        */
        public string DMS2STR(string sH, string sM, string sS)
        {
            sS = LTrimEx(sS, "-");
            sS = LTrimEx(sS, "0");
            long nPos;
            nPos = Strings.InStr(sS, ".");
            if (1 < nPos)
            {
                sS = RTrimEx(sS, "0");
                sS = RTrimEx(sS, ".");
            }
            else if (nPos == 1)
            {
                sS = "0" + sS;
                sS = RTrimEx(sS, "0");
                sS = RTrimEx(sS, ".");
            }
            else if (sS.Length <= 0)
            {
                sS = "0";
            }
            return Strings.Format(Convert.ToDouble(sH), "+000;-000") + Strings.Format(Convert.ToDouble(sM), "00") + sS;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '文字列を度分秒に変換する。
        '
        '引き数：
        'sEdit 文字列。
        'nH 度が設定される。
        'nM 分が設定される。
        'nS 秒が設定される。
        Public Sub STR2DMS(ByVal sEdit As String, ByRef nH As Long, ByRef nM As Long, ByRef nS As Double)
            nH = Val(Left$(sEdit, 4))
            Dim nSign As Long
            nSign = IIf(nH < 0, -1, 1)
            nM = Val(Mid$(sEdit, 5, 2)) * nSign
            nS = Val(Mid$(sEdit, 7)) * nSign
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '文字列を度分秒に変換する。
        '
        '引き数：
        'sEdit 文字列。
        'nH 度が設定される。
        'nM 分が設定される。
        'nS 秒が設定される。
        */
        public void STR2DMS(string sEdit, ref long nH, ref long nM, ref double nS)
        {
            nH = Convert.ToInt32(Left(sEdit, 4));
            long nSign;
            nSign = nH < 0 ? -1 : 1;
            nM = Convert.ToInt32(Mid(sEdit, 5, 2)) * nSign;
            nS = Convert.ToDouble(Mid(sEdit, 7,99)) * nSign;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '文字列(度分秒)を度に変換する。
        '
        '引き数：
        'sEdit 文字列。
        '
        '戻り値：度を返す。
        Public Function STR2DEG(ByVal sEdit As String) As Double
            Dim nH As Long
            Dim nM As Long
            Dim nS As Double
            Call STR2DMS(sEdit, nH, nM, nS)
            STR2DEG = dms_to_d(nH, nM, nS)
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '文字列(度分秒)を度に変換する。
        '
        '引き数：
        'sEdit 文字列。
        '
        '戻り値：度を返す。
        */
        public double STR2DEG(string sEdit)
        {
            long nH = 0;
            long nM = 0;
            double nS = 0;
            STR2DMS(sEdit, ref nH, ref nM, ref nS);
            return dms_to_d((int)nH, (int)nM, nS);
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '2009/11 H.Nakamura
        '今期座標を初期化する。
        Private Sub InitKON()

            On Error GoTo ErrorHandler
    
            Dim nLatGAN As Double
            Dim nLonGAN As Double
            Dim nHeightGAN As Double
    
            If (m_nEditCode And EDITCODE_COORD_DEG) <> 0 Then
                nLatGAN = Val(m_sEditLat)
                nLonGAN = Val(m_sEditLon)
                nHeightGAN = m_nHeight
            ElseIf (m_nEditCode And EDITCODE_COORD_DMS) <> 0 Then
                nLatGAN = STR2DEG(m_sEditLat)
                nLonGAN = STR2DEG(m_sEditLon)
                nHeightGAN = m_nHeight
            ElseIf (m_nEditCode And EDITCODE_COORD_JGD) <> 0 Then
                Call JGDxyz_to_WGS84dms(m_nEditCoordNum, Val(m_sEditX), Val(m_sEditY), 0, nLatGAN, nLonGAN, nHeightGAN)
                nHeightGAN = m_nHeight
            Else
                Call WGS84xyz_to_WGS84dms(m_nX, m_nY, m_nZ, nLatGAN, nLonGAN, nHeightGAN)
            End If
    
            m_nLatKON = nLatGAN
            m_nLonKON = nLonGAN
            m_nHeightKON = nHeightGAN
            m_nXKON = m_nX
            m_nYKON = m_nY
            m_nZKON = m_nZ
            m_nRoundXKON = m_nRoundX
            m_nRoundYKON = m_nRoundY
            m_nRoundZKON = m_nRoundZ
    
            Exit Sub
    
        ErrorHandler:
    
            m_nLatKON = 0
            m_nLonKON = 0
            m_nHeightKON = 0
            m_nXKON = 0
            m_nYKON = 0
            m_nZKON = 0
            m_nRoundXKON = 0
            m_nRoundYKON = 0
            m_nRoundZKON = 0
    
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '2009/11 H.Nakamura
        '今期座標を初期化する。
        */
        private void InitKON()
        {
            try
            {
                double nLatGAN = 0;
                double nLonGAN = 0;
                double nHeightGAN = 0;

                if ((m_nEditCode & (COORDINATE_TYPE)EDITCODE_STYLE.EDITCODE_COORD_DEG) != 0)
                {
                    nLatGAN = Convert.ToDouble(m_sEditLat);
                    nLonGAN = Convert.ToDouble(m_sEditLon);
                    nHeightGAN = m_nHeight;
                }
                else if ((m_nEditCode & (COORDINATE_TYPE)EDITCODE_STYLE.EDITCODE_COORD_DMS) != 0)
                {
                    nLatGAN = STR2DEG(m_sEditLat);
                    nLonGAN = STR2DEG(m_sEditLon);
                    nHeightGAN = m_nHeight;
                }
                else if ((m_nEditCode & (COORDINATE_TYPE)EDITCODE_STYLE.EDITCODE_COORD_JGD) != 0)
                {
                    JGDxyz_to_WGS84dms((int)m_nEditCoordNum, Convert.ToDouble(m_sEditX), Convert.ToDouble(m_sEditY), 0, ref nLatGAN, ref nLonGAN, ref nHeightGAN);
                    nHeightGAN = m_nHeight;
                }
                else
                {
                    WGS84xyz_to_WGS84dms(m_nX, m_nY, m_nZ, ref nLatGAN, ref nLonGAN, ref nHeightGAN);
                }

                m_nLatKON = nLatGAN;
                m_nLonKON = nLonGAN;
                m_nHeightKON = nHeightGAN;
                m_nXKON = m_nX;
                m_nYKON = m_nY;
                m_nZKON = m_nZ;
                m_nRoundXKON = m_nRoundX;
                m_nRoundYKON = m_nRoundY;
                m_nRoundZKON = m_nRoundZ;
                return;
            }

            catch
            {
                m_nLatKON = 0;
                m_nLonKON = 0;
                m_nHeightKON = 0;
                m_nXKON = 0;
                m_nYKON = 0;
                m_nZKON = 0;
                m_nRoundXKON = 0;
                m_nRoundYKON = 0;
                m_nRoundZKON = 0;
                return;
            }
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '2009/11 H.Nakamura
        'セミ・ダイナミック変換。
        Private Sub ConvSemiDyna()
        #If SEMIDYNA <> 0 Then

            If mdlSemiDyna.m_nSemiDyna <= 0 Then Exit Sub
    
            Dim nLatGAN As Double
            Dim nLonGAN As Double
            Dim nHeightGAN As Double
    
            If (m_nEditCode And EDITCODE_COORD_DEG) <> 0 Then
                nLatGAN = Val(m_sEditLat)
                nLonGAN = Val(m_sEditLon)
                nHeightGAN = m_nHeight
            ElseIf (m_nEditCode And EDITCODE_COORD_DMS) <> 0 Then
                nLatGAN = STR2DEG(m_sEditLat)
                nLonGAN = STR2DEG(m_sEditLon)
                nHeightGAN = m_nHeight
            ElseIf (m_nEditCode And EDITCODE_COORD_JGD) <> 0 Then
                Call JGDxyz_to_WGS84dms(m_nEditCoordNum, Val(m_sEditX), Val(m_sEditY), 0, nLatGAN, nLonGAN, nHeightGAN)
                nHeightGAN = m_nHeight
            Else
                Call WGS84xyz_to_WGS84dms(m_nX, m_nY, m_nZ, nLatGAN, nLonGAN, nHeightGAN)
            End If
    
            m_nSemiDynaCounter = mdlSemiDyna.ConvertGAN2KON(nLatGAN, nLonGAN, nHeightGAN, m_nLatKON, m_nLonKON, m_nHeightKON)
    
            If m_nSemiDynaCounter < 0 And mdlSemiDyna.m_bMessage Then
                mdlSemiDyna.m_bMessage = False
                Call MsgBox(MESSAGE_SEMIDYNA_FAILED, vbExclamation)
                Call InitKON
            End If
    
            Call WGS84dms_to_WGS84xyz(m_nLatKON, m_nLonKON, m_nHeightKON, m_nXKON, m_nYKON, m_nZKON)
            m_nRoundXKON = JpnRound(m_nXKON, ACCOUNT_DECIMAL_XYZ)
            m_nRoundYKON = JpnRound(m_nYKON, ACCOUNT_DECIMAL_XYZ)
            m_nRoundZKON = JpnRound(m_nZKON, ACCOUNT_DECIMAL_XYZ)
    
        #End If
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '2009/11 H.Nakamura
        'セミ・ダイナミック変換。
        */
        private void ConvSemiDyna()
        {
            return;
        }
        //==========================================================================================



        //23/12/20 K.Setoguchi---->>>>
        //***************************************************************************
        //***************************************************************************
        /// <summary>
        /// 今期座標を初期化する。
        /// </summary>
        /// <param name="Genba_S"></param>
        /// <returns>
        /// 戻り値:returns = Non
        /// </returns>
        //***************************************************************************
        public void InitKON(ref GENBA_STRUCT_S Genba_S)
        {
            //'2009/11 H.Nakamura
            //'今期座標を初期化する。
            //Private Sub InitKON()
            //
            //    On Error GoTo ErrorHandler
            //
            //
            //    Dim nLatGAN As Double
            //    Dim nLonGAN As Double
            //    Dim nHeightGAN As Double
            //
            //
            //    If(m_nEditCode And EDITCODE_COORD_DEG) <> 0 Then
            //        nLatGAN = Val(m_sEditLat)
            //        nLonGAN = Val(m_sEditLon)
            //        nHeightGAN = m_nHeight
            //    ElseIf(m_nEditCode And EDITCODE_COORD_DMS) <> 0 Then
            //        nLatGAN = STR2DEG(m_sEditLat)
            //        nLonGAN = STR2DEG(m_sEditLon)
            //        nHeightGAN = m_nHeight
            //    ElseIf(m_nEditCode And EDITCODE_COORD_JGD) <> 0 Then
            //        Call JGDxyz_to_WGS84dms(m_nEditCoordNum, Val(m_sEditX), Val(m_sEditY), 0, nLatGAN, nLonGAN, nHeightGAN)
            //        nHeightGAN = m_nHeight
            //    Else
            //        Call WGS84xyz_to_WGS84dms(m_nX, m_nY, m_nZ, nLatGAN, nLonGAN, nHeightGAN)
            //    End If
            //
            //
            //    m_nLatKON = nLatGAN
            //    m_nLonKON = nLonGAN
            //    m_nHeightKON = nHeightGAN
            //    m_nXKON = m_nX
            //    m_nYKON = m_nY
            //    m_nZKON = m_nZ
            //    m_nRoundXKON = m_nRoundX
            //    m_nRoundYKON = m_nRoundY
            //    m_nRoundZKON = m_nRoundZ
            //
            //    Exit Sub
            //
            //ErrorHandler:
            //
            //
            //    m_nLatKON = 0
            //    m_nLonKON = 0
            //    m_nHeightKON = 0
            //    m_nXKON = 0
            //    m_nYKON = 0
            //    m_nZKON = 0
            //    m_nRoundXKON = 0
            //    m_nRoundYKON = 0
            //    m_nRoundZKON = 0
            //    
            //End Sub
            //------------------------------------------------------------------------------------------
            //[C#]

            try
            {
                double nLatGAN = 0;
                double nLonGAN = 0;
                double nHeightGAN = 0;

                if ((m_nEditCode & (COORDINATE_TYPE)EDITCODE_STYLE.EDITCODE_COORD_DEG) != 0)
                {
                    nLatGAN = Convert.ToDouble(m_sEditLat);
                    nLonGAN = Convert.ToDouble(m_sEditLon);
                    nHeightGAN = m_nHeight;
                }
                else if ((m_nEditCode & (COORDINATE_TYPE)EDITCODE_STYLE.EDITCODE_COORD_DMS) != 0)
                {
                    nLatGAN = STR2DEG(m_sEditLat);
                    nLonGAN = STR2DEG(m_sEditLon);
                    nHeightGAN = m_nHeight;
                }
                else if ((m_nEditCode & (COORDINATE_TYPE)EDITCODE_STYLE.EDITCODE_COORD_JGD) != 0)
                {
                    JGDxyz_to_WGS84dms((int)m_nEditCoordNum, Convert.ToDouble(m_sEditX), Convert.ToDouble(m_sEditY), 0, ref nLatGAN, ref nLonGAN, ref nHeightGAN);
                    nHeightGAN = m_nHeight;
                }
                else
                {
                    WGS84xyz_to_WGS84dms(m_nX, m_nY, m_nZ, ref nLatGAN, ref nLonGAN, ref nHeightGAN);
                }

                m_nLatKON = nLatGAN;
                m_nLonKON = nLonGAN;
                m_nHeightKON = nHeightGAN;
                m_nXKON = m_nX;
                m_nYKON = m_nY;
                m_nZKON = m_nZ;
                m_nRoundXKON = m_nRoundX;
                m_nRoundYKON = m_nRoundY;
                m_nRoundZKON = m_nRoundZ;

                Genba_S.OPFix.m_nLatKON = nLatGAN;
                Genba_S.OPFix.m_nLonKON = nLonGAN;
                Genba_S.OPFix.m_nHeightKON = nHeightGAN;
                Genba_S.OPFix.m_nXKON = m_nX;
                Genba_S.OPFix.m_nYKON = m_nY;
                Genba_S.OPFix.m_nZKON = m_nZ;
                Genba_S.OPFix.m_nRoundXKON = m_nRoundX;
                Genba_S.OPFix.m_nRoundYKON = m_nRoundY;
                Genba_S.OPFix.m_nRoundZKON = m_nRoundZ;
                return;

            }


            catch
            {
                m_nLatKON = 0;
                m_nLonKON = 0;
                m_nHeightKON = 0;
                m_nXKON = 0;
                m_nYKON = 0;
                m_nZKON = 0;
                m_nRoundXKON = 0;
                m_nRoundYKON = 0;
                m_nRoundZKON = 0;

                Genba_S.OPFix.m_nLatKON = 0;
                Genba_S.OPFix.m_nLonKON = 0;
                Genba_S.OPFix.m_nHeightKON = 0;
                Genba_S.OPFix.m_nXKON = 0;
                Genba_S.OPFix.m_nYKON = 0;
                Genba_S.OPFix.m_nZKON = 0;
                Genba_S.OPFix.m_nRoundXKON = 0;
                Genba_S.OPFix.m_nRoundYKON = 0;
                Genba_S.OPFix.m_nRoundZKON = 0;
                return;
            }
            //==========================================================================================

        }
        //***************************************************************************
        //***************************************************************************
        //<<<<----23/12/20 K.Setoguchi


        //***************************************************************************
        //***************************************************************************
        /// <summary>
        ///'読み込み。
        ///     （VB Load関数をVC用に変更）
        ///'
        ///'引き数：
        ///  '引き数：
        /// バイナリファイル
        /// バージョン番号 OPFix
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
            //MdlUtility mdlUtility = new MdlUtility();

            //-----------------------------------------------------------------
            //Get #nFile, , m_nRoundX       public double m_nRoundX;          //As Double '丸めX。 0
            //(del)     Genba_S.m_nRoundX = br.ReadDouble();        //23/12/20 K.Setoguchi              
            Genba_S.OPFix.m_nRoundX = br.ReadDouble();              //23/12/20 K.Setoguchi
            m_nRoundX = Genba_S.OPFix.m_nRoundX;
            //-----------------------------------------------------------------
            //Get #nFile, , m_nRoundY       public double m_nRoundY;          //As Double '丸めY。 0
            //(del)     Genba_S.m_nRoundY = br.ReadDouble();        //23/12/20 K.Setoguchi
            Genba_S.OPFix.m_nRoundY = br.ReadDouble();              //23/12/20 K.Setoguchi
            m_nRoundY = Genba_S.OPFix.m_nRoundY;
            //-----------------------------------------------------------------
            //Get #nFile, , m_nRoundZ       public double m_nRoundZ;          //As Double '丸めZ。 0
            //(del)     Genba_S.m_nRoundZ = br.ReadDouble();
            Genba_S.OPFix.m_nRoundZ = br.ReadDouble();
            m_nRoundZ = Genba_S.OPFix.m_nRoundZ;
            //-----------------------------------------------------------------
            if (nVersion < 6400)
            {
                //-------------------------------------------
                //(del)     Genba_S.m_nX = Genba_S.m_nRoundX;       //23/12/20 K.Setoguchi
                Genba_S.OPFix.m_nX = Genba_S.OPFix.m_nRoundX;       //23/12/20 K.Setoguchi
                m_nX = Genba_S.OPFix.m_nX;
                //-------------------------------------------   
                //(del)     Genba_S.m_nY = Genba_S.m_nRoundY;       //23/12/20 K.Setoguchi
                Genba_S.OPFix.m_nY = Genba_S.OPFix.m_nRoundY;       //23/12/20 K.Setoguchi
                m_nY = Genba_S.OPFix.m_nY;
                //-------------------------------------------
                //(del)     Genba_S.m_nZ = Genba_S.m_nRoundZ;       //23/12/20 K.Setoguchi
                Genba_S.OPFix.m_nZ = Genba_S.OPFix.m_nRoundZ;       //23/12/20 K.Setoguchi
                m_nZ = Genba_S.OPFix.m_nZ;
                //-------------------------------------------
                //                Genba_S.m_nRoundX = JpnRound(Genba_S.m_nX, ACCOUNT_DECIMAL_XYZ)
                //                Genba_S.m_nRoundY = JpnRound(Genba_S.m_nY, ACCOUNT_DECIMAL_XYZ)
                //                Genba_S.m_nRoundZ = JpnRound(Genba_S.m_nZ, ACCOUNT_DECIMAL_XYZ)
            }
            else
            {
                //-------------------------------------------
                //Get #nFile, , m_nX    public double m_nX;               //As Double 'X。       0
                //(del)     Genba_S.m_nX = br.ReadDouble();         //23/12/20 K.Setoguchi
                Genba_S.OPFix.m_nX = br.ReadDouble();               //23/12/20 K.Setoguchi
                m_nX = Genba_S.OPFix.m_nX;
                //-------------------------------------------
                //Get #nFile, , m_nY    public double m_nY;               //As Double 'Y。       0
                //(del)     Genba_S.m_nY = br.ReadDouble();         //23/12/20 K.Setoguchi
                Genba_S.OPFix.m_nY = br.ReadDouble();               //23/12/20 K.Setoguchi
                m_nY = Genba_S.OPFix.m_nY;
                //-------------------------------------------
                //Get #nFile, , m_nZ    public double m_nZ;               //As Double 'Z。       0
                //(del)     Genba_S.m_nZ = br.ReadDouble();         //23/12/20 K.Setoguchi
                Genba_S.OPFix.m_nZ = br.ReadDouble();               //23/12/20 K.Setoguchi
                m_nZ = Genba_S.OPFix.m_nZ;
            }
            //-----------------------------------------------------------------
            if (nVersion < 6400)
            {
                //(del)     Genba_S.m_nHeight = 0;  //'6200 <= nVersion < 6400 の間は開発㊥なので気にしない。  //23/12/20 K.Setoguchi
                Genba_S.OPFix.m_nHeight = 0;        //'6200 <= nVersion < 6400 の間は開発㊥なので気にしない。  //23/12/20 K.Setoguchi
                m_nHeight = Genba_S.OPFix.m_nHeight;
            }
            else
            {
                //Get #nFile, , m_nHeight     public double m_nHeightKON;        //As Double '今期楕円体高(ｍ)。
                //(del)     Genba_S.m_nHeight = br.ReadDouble();    //23/12/20 K.Setoguchi
                Genba_S.OPFix.m_nHeight = br.ReadDouble();          //23/12/20 K.Setoguchi
                m_nHeight = Genba_S.OPFix.m_nHeight;
            }
            if (nVersion < 6200)
            {
                //(del)     Genba_S.m_nEditCode = 0;                //23/12/20 K.Setoguchi      //0
                Genba_S.OPFix.m_nEditCode = 0;                      //23/12/20 K.Setoguchi      //0
                m_nEditCode = 0;
            }
            else
            {
                //---------------------------------------------------------------
                //Get #nFile, , m_nEditCode       public int m_nEditCode;        //As EDITCODE_STYLE '編集コード。     0
                //(del)     Genba_S.m_nEditCode = br.ReadInt32();
                Genba_S.OPFix.m_nEditCode = br.ReadInt32();
                m_nEditCode = (COORDINATE_TYPE)Genba_S.OPFix.m_nEditCode;
                //--------------------------------
                //m_sEditX = GetString(nFile)     public string m_sEditX;            //As String '入力X。              ""
                //(del)     Genba_S.m_sEditX = mdlUtility.FileRead_GetString(br);       //23/12/20 K.Setoguchi
                Genba_S.OPFix.m_sEditX = FileRead_GetString(br);             //23/12/20 K.Setoguchi
                m_sEditX = Genba_S.OPFix.m_sEditX;
                //m_sEditY = GetString(nFile)     public string m_sEditY;            //As String '入力Y。              ""
                //(del)     Genba_S.m_sEditY = mdlUtility.FileRead_GetString(br);       //23/12/20 K.Setoguchi
                Genba_S.OPFix.m_sEditY = FileRead_GetString(br);             //23/12/20 K.Setoguchi
                m_sEditY = Genba_S.OPFix.m_sEditY;
                //m_sEditZ = GetString(nFile)     public string m_sEditZ;            //As String '入力Z。              ""
                //(del)     Genba_S.m_sEditZ = mdlUtility.FileRead_GetString(br);       //23/12/20 K.Setoguchi
                Genba_S.OPFix.m_sEditZ = FileRead_GetString(br);             //23/12/20 K.Setoguchi
                m_sEditZ = Genba_S.OPFix.m_sEditZ;
                //--------------------------------
                //m_sEditLat = GetString(nFile)   public string m_sEditLat;          //As String '入力緯度。             ""
                //(del)     Genba_S.m_sEditLat = mdlUtility.FileRead_GetString(br);     //23/12/20 K.Setoguchi
                Genba_S.OPFix.m_sEditLat = FileRead_GetString(br);           //23/12/20 K.Setoguchi
                m_sEditLat = Genba_S.OPFix.m_sEditLat;
                //m_sEditLon = GetString(nFile)   public string m_sEditLon;          //As String '入力経度。             ""
                //(del)     Genba_S.m_sEditLon = mdlUtility.FileRead_GetString(br);     //23/12/20 K.Setoguchi
                Genba_S.OPFix.m_sEditLon = FileRead_GetString(br);           //23/12/20 K.Setoguchi
                m_sEditLon = Genba_S.OPFix.m_sEditLon;
                //m_sEditHeight = GetString(nFile)public string m_sEditHeight;       //As String '入力高さ(ｍ)。         ""
                //(del)     Genba_S.m_sEditHeight = mdlUtility.FileRead_GetString(br);  //23/12/20 K.Setoguchi
                Genba_S.OPFix.m_sEditHeight = FileRead_GetString(br);        //23/12/20 K.Setoguchi
                m_sEditHeight = Genba_S.OPFix.m_sEditHeight;
                //--------------------------------
                //Get #nFile, , m_nEditCoordNum   public long m_nEditCoordNum;       //As Long '入力座標系番号(1～19)。  0
                //(del)     Genba_S.m_nEditCoordNum = br.ReadInt32();       //23/12/20 K.Setoguchi           
                Genba_S.OPFix.m_nEditCoordNum = br.ReadInt32();             //23/12/20 K.Setoguchi    
                m_nEditCoordNum = Genba_S.OPFix.m_nEditCoordNum;
            }

            //'2009/11 H.Nakamura
            //'今期座標。
            if (nVersion < 8130)
            {
                //InitKON();
                InitKON(ref Genba_S);
            }
            else
            {
#if SEMIDYNA
                //--------------------------------
                //Get #nFile, , m_nRoundXKON    public double m_nRoundXKON;        //As Double '今期丸めX。  0
                //(del)     Genba_S.m_nRoundXKON = br.ReadDouble();         //23/12/20 K.Setoguchi
                Genba_S.OPFix.m_nRoundXKON = br.ReadDouble();               //23/12/20 K.Setoguchi
                m_nRoundXKON = Genba_S.OPFix.m_nRoundXKON;
                //--------------------------------
                //Get #nFile, , m_nRoundYKON    public double m_nRoundYKON;        //As Double '今期丸めY。  0
                //(del)     Genba_S.m_nRoundYKON = br.ReadDouble();         //23/12/20 K.Setoguchi
                Genba_S.OPFix.m_nRoundYKON = br.ReadDouble();               //23/12/20 K.Setoguchi
                m_nRoundYKON = Genba_S.OPFix.m_nRoundYKON;
                //--------------------------------
                //Get #nFile, , m_nRoundZKON    public double m_nRoundZKON;        //As Double '今期丸めZ。  0
                //(del)     Genba_S.m_nRoundZKON = br.ReadDouble();         //23/12/20 K.Setoguchi
                Genba_S.OPFix.m_nRoundZKON = br.ReadDouble();               //23/12/20 K.Setoguchi
                m_nRoundZKON = Genba_S.OPFix.m_nRoundZKON;
                //--------------------------------
                //Get #nFile, , m_nXKON         public double m_nXKON;             //As Double '今期X。        0
                //(del)     Genba_S.m_nXKON = br.ReadDouble();              //23/12/20 K.Setoguchi
                Genba_S.OPFix.m_nXKON = br.ReadDouble();                    //23/12/20 K.Setoguchi
                m_nXKON = Genba_S.OPFix.m_nXKON;
                //--------------------------------
                //Get #nFile, , m_nYKON         public double m_nYKON;             //As Double '今期Y。        0
                //(del)     Genba_S.m_nYKON = br.ReadDouble();              //23/12/20 K.Setoguchi
                Genba_S.OPFix.m_nYKON = br.ReadDouble();                    //23/12/20 K.Setoguchi
                m_nYKON = Genba_S.OPFix.m_nYKON;
                //--------------------------------
                //Get #nFile, , m_nZKON         public double m_nZKON;             //As Double '今期Z。        0
                //(del)     Genba_S.m_nZKON = br.ReadDouble();              //23/12/20 K.Setoguchi
                Genba_S.OPFix.m_nZKON = br.ReadDouble();                    //23/12/20 K.Setoguchi
                m_nZKON = Genba_S.OPFix.m_nZKON;
                //--------------------------------
                //Get #nFile, , m_nLatKON       public double m_nLatKON;           //As Double '今期緯度(度)。    0
                //(del)     Genba_S.m_nLatKON = br.ReadDouble();            //23/12/20 K.Setoguchi
                Genba_S.OPFix.m_nLatKON = br.ReadDouble();                  //23/12/20 K.Setoguchi
                m_nLatKON = Genba_S.OPFix.m_nLatKON;
                //--------------------------------
                //Get #nFile, , m_nLonKON       public double m_nLonKON;           //As Double '今期経度(度)。    0
                //(del)     Genba_S.m_nLonKON = br.ReadDouble();            //23/12/20 K.Setoguchi
                Genba_S.OPFix.m_nLonKON = br.ReadDouble();                  //23/12/20 K.Setoguchi
                m_nLonKON = Genba_S.OPFix.m_nLonKON;
                //--------------------------------
                //Get #nFile, , m_nHeightKON    public double m_nHeightKON;        //As Double '今期楕円体高(ｍ)。  0
                //(del)     Genba_S.m_nHeightKON = br.ReadDouble();         //23/12/20 K.Setoguchi
                Genba_S.OPFix.m_nHeightKON = br.ReadDouble();               //23/12/20 K.Setoguchi
                m_nHeightKON = Genba_S.OPFix.m_nHeightKON;
                //--------------------------------

                //--------------------------------
                //'8202以前には間違いがあったのでなかったことにする。
                if (nVersion < 8202)
                { InitKON(ref Genba_S); }
                //(del)         { //Call InitKON; } 
#else
                InitKON();
#endif
            }
            //--------------------------------
            //'セミ・ダイナミック補正変換を未計算にする。'2009/11 H.Nakamura                                 0
            //(del)     Genba_S.m_nSemiDynaCounter = 0;     //As Long 'セミ・ダイナミック補正変換番号。0 は未計算。  //23/12/20 K.Setoguchi
            Genba_S.OPFix.m_nSemiDynaCounter = 0;           //As Long 'セミ・ダイナミック補正変換番号。0 は未計算。  //23/12/20 K.Setoguchi
            m_nSemiDynaCounter = (long)Genba_S.OPFix.m_nSemiDynaCounter;
            //--------------------------------
        }
        //(del)     //'セミ・ダイナミック変換'2009/11 H.Nakamura
        //(del)     public double m_nSemiDynaCounter;  //As Long 'セミ・ダイナミック補正変換番号。0 は未計算。

        //------------------------------------------------------------------------------------------------------
        // '読み込み。
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
        //    If nVersion< 6400 Then
        //        m_nHeight = 0 '6200 <= nVersion < 6400 の間は開発㊥なので気にしない。
        //    Else
        //        Get #nFile, , m_nHeight
        //    End If
        //    If nVersion < 6200 Then
        //        m_nEditCode = 0
        //    Else
        //        Get #nFile, , m_nEditCode
        //        m_sEditX = GetString(nFile)
        //        m_sEditY = GetString(nFile)
        //        m_sEditZ = GetString(nFile)
        //        m_sEditLat = GetString(nFile)
        //        m_sEditLon = GetString(nFile)
        //        m_sEditHeight = GetString(nFile)
        //        Get #nFile, , m_nEditCoordNum
        //    End If
        //    
        //    '2009/11 H.Nakamura
        //    '今期座標。
        //    If nVersion < 8130 Then
        //        Call InitKON
        //    Else
        //#If SEMIDYNA <> 0 Then
        //        Get #nFile, , m_nRoundXKON
        //        Get #nFile, , m_nRoundYKON
        //        Get #nFile, , m_nRoundZKON
        //        Get #nFile, , m_nXKON
        //        Get #nFile, , m_nYKON
        //        Get #nFile, , m_nZKON
        //        Get #nFile, , m_nLatKON
        //        Get #nFile, , m_nLonKON
        //        Get #nFile, , m_nHeightKON
        //        
        //        '8202以前には間違いがあったのでなかったことにする。
        //        If nVersion < 8202 Then Call InitKON
        //#Else
        //        Call InitKON
        //#End If
        //    End If
        //    
        //    'セミ・ダイナミック補正変換を未計算にする。'2009/11 H.Nakamura
        //    m_nSemiDynaCounter = 0
        //End Sub

    }
    //23/12/20 K.Setoguchi---->>>>
    //(del)     private void InitKON()
    //(del)     {
    //      ＜記載を削除し、上に移動する
    //(del)     {
    //<<<<----23/12/20 K.Setoguchi

}
