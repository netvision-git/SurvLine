using NTS;
using SurvLine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurvLine
{
    public class CoordinatePointFix
    {
        MdlUtility mdlUtility = new MdlUtility();

         //23/12/20 K.setoguchi@NV---------->>>>>>>>>>>
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

        }
        //***************************************************************************
        //***************************************************************************
        //<<<<<<<<<-----------23/12/20 K.setoguchi@NV


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
            //-----------------------------------------------------------------
            //Get #nFile, , m_nRoundX       public double m_nRoundX;          //As Double '丸めX。 0
            //(del)     Genba_S.m_nRoundX = br.ReadDouble();        //23/12/20 K.Setoguchi              
            Genba_S.OPFix.m_nRoundX = br.ReadDouble();              //23/12/20 K.Setoguchi
            //-----------------------------------------------------------------
            //Get #nFile, , m_nRoundY       public double m_nRoundY;          //As Double '丸めY。 0
            //(del)     Genba_S.m_nRoundY = br.ReadDouble();        //23/12/20 K.Setoguchi
            Genba_S.OPFix.m_nRoundY = br.ReadDouble();              //23/12/20 K.Setoguchi
            //-----------------------------------------------------------------
            //Get #nFile, , m_nRoundZ       public double m_nRoundZ;          //As Double '丸めZ。 0
            //(del)     Genba_S.m_nRoundZ = br.ReadDouble();
            Genba_S.OPFix.m_nRoundZ = br.ReadDouble();
            //-----------------------------------------------------------------
            if (nVersion < 6400)
            {
                //-------------------------------------------
                //(del)     Genba_S.m_nX = Genba_S.m_nRoundX;       //23/12/20 K.Setoguchi
                Genba_S.OPFix.m_nX = Genba_S.OPFix.m_nRoundX;       //23/12/20 K.Setoguchi
                //-------------------------------------------   
                //(del)     Genba_S.m_nY = Genba_S.m_nRoundY;       //23/12/20 K.Setoguchi
                Genba_S.OPFix.m_nY = Genba_S.OPFix.m_nRoundY;       //23/12/20 K.Setoguchi
                //-------------------------------------------
                //(del)     Genba_S.m_nZ = Genba_S.m_nRoundZ;       //23/12/20 K.Setoguchi
                Genba_S.OPFix.m_nZ = Genba_S.OPFix.m_nRoundZ;       //23/12/20 K.Setoguchi
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
                //-------------------------------------------
                //Get #nFile, , m_nY    public double m_nY;               //As Double 'Y。       0
                //(del)     Genba_S.m_nY = br.ReadDouble();         //23/12/20 K.Setoguchi
                Genba_S.OPFix.m_nY = br.ReadDouble();               //23/12/20 K.Setoguchi
                //-------------------------------------------
                //Get #nFile, , m_nZ    public double m_nZ;               //As Double 'Z。       0
                //(del)     Genba_S.m_nZ = br.ReadDouble();         //23/12/20 K.Setoguchi
                Genba_S.OPFix.m_nZ = br.ReadDouble();               //23/12/20 K.Setoguchi
            }
            //-----------------------------------------------------------------
            if (nVersion < 6400)
            {
                //(del)     Genba_S.m_nHeight = 0;  //'6200 <= nVersion < 6400 の間は開発㊥なので気にしない。  //23/12/20 K.Setoguchi
                Genba_S.OPFix.m_nHeight = 0;        //'6200 <= nVersion < 6400 の間は開発㊥なので気にしない。  //23/12/20 K.Setoguchi
            }
            else
            {
                //Get #nFile, , m_nHeight     public double m_nHeightKON;        //As Double '今期楕円体高(ｍ)。
                //(del)     Genba_S.m_nHeight = br.ReadDouble();    //23/12/20 K.Setoguchi
                Genba_S.OPFix.m_nHeight = br.ReadDouble();          //23/12/20 K.Setoguchi
            }
            if (nVersion < 6200)
            {
                //(del)     Genba_S.m_nEditCode = 0;                //23/12/20 K.Setoguchi      //0
                Genba_S.OPFix.m_nEditCode = 0;                      //23/12/20 K.Setoguchi      //0
            }
            else
            {
                //---------------------------------------------------------------
                //Get #nFile, , m_nEditCode       public int m_nEditCode;        //As EDITCODE_STYLE '編集コード。     0
                //(del)     Genba_S.m_nEditCode = br.ReadInt32();
                Genba_S.OPFix.m_nEditCode = br.ReadInt32();
                //--------------------------------
                //m_sEditX = GetString(nFile)     public string m_sEditX;            //As String '入力X。              ""
                //(del)     Genba_S.m_sEditX = mdlUtility.FileRead_GetString(br);       //23/12/20 K.Setoguchi
                Genba_S.OPFix.m_sEditX = mdlUtility.FileRead_GetString(br);             //23/12/20 K.Setoguchi
                //m_sEditY = GetString(nFile)     public string m_sEditY;            //As String '入力Y。              ""
                //(del)     Genba_S.m_sEditY = mdlUtility.FileRead_GetString(br);       //23/12/20 K.Setoguchi
                Genba_S.OPFix.m_sEditY = mdlUtility.FileRead_GetString(br);             //23/12/20 K.Setoguchi
                //m_sEditZ = GetString(nFile)     public string m_sEditZ;            //As String '入力Z。              ""
                //(del)     Genba_S.m_sEditZ = mdlUtility.FileRead_GetString(br);       //23/12/20 K.Setoguchi
                Genba_S.OPFix.m_sEditZ = mdlUtility.FileRead_GetString(br);             //23/12/20 K.Setoguchi
                //--------------------------------
                //m_sEditLat = GetString(nFile)   public string m_sEditLat;          //As String '入力緯度。             ""
                //(del)     Genba_S.m_sEditLat = mdlUtility.FileRead_GetString(br);     //23/12/20 K.Setoguchi
                Genba_S.OPFix.m_sEditLat = mdlUtility.FileRead_GetString(br);           //23/12/20 K.Setoguchi
                //m_sEditLon = GetString(nFile)   public string m_sEditLon;          //As String '入力経度。             ""
                //(del)     Genba_S.m_sEditLon = mdlUtility.FileRead_GetString(br);     //23/12/20 K.Setoguchi
                Genba_S.OPFix.m_sEditLon = mdlUtility.FileRead_GetString(br);           //23/12/20 K.Setoguchi
                //m_sEditHeight = GetString(nFile)public string m_sEditHeight;       //As String '入力高さ(ｍ)。         ""
                //(del)     Genba_S.m_sEditHeight = mdlUtility.FileRead_GetString(br);  //23/12/20 K.Setoguchi
                Genba_S.OPFix.m_sEditHeight = mdlUtility.FileRead_GetString(br);        //23/12/20 K.Setoguchi
                //--------------------------------
                //Get #nFile, , m_nEditCoordNum   public long m_nEditCoordNum;       //As Long '入力座標系番号(1～19)。  0
                //(del)     Genba_S.m_nEditCoordNum = br.ReadInt32();       //23/12/20 K.Setoguchi           
                Genba_S.OPFix.m_nEditCoordNum = br.ReadInt32();             //23/12/20 K.Setoguchi    
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
                //--------------------------------
                //Get #nFile, , m_nRoundYKON    public double m_nRoundYKON;        //As Double '今期丸めY。  0
                //(del)     Genba_S.m_nRoundYKON = br.ReadDouble();         //23/12/20 K.Setoguchi
                Genba_S.OPFix.m_nRoundYKON = br.ReadDouble();               //23/12/20 K.Setoguchi
                //--------------------------------
                //Get #nFile, , m_nRoundZKON    public double m_nRoundZKON;        //As Double '今期丸めZ。  0
                //(del)     Genba_S.m_nRoundZKON = br.ReadDouble();         //23/12/20 K.Setoguchi
                Genba_S.OPFix.m_nRoundZKON = br.ReadDouble();               //23/12/20 K.Setoguchi
                //--------------------------------
                //Get #nFile, , m_nXKON         public double m_nXKON;             //As Double '今期X。        0
                //(del)     Genba_S.m_nXKON = br.ReadDouble();              //23/12/20 K.Setoguchi
                Genba_S.OPFix.m_nXKON = br.ReadDouble();                    //23/12/20 K.Setoguchi
                //--------------------------------
                //Get #nFile, , m_nYKON         public double m_nYKON;             //As Double '今期Y。        0
                //(del)     Genba_S.m_nYKON = br.ReadDouble();              //23/12/20 K.Setoguchi
                Genba_S.OPFix.m_nYKON = br.ReadDouble();                    //23/12/20 K.Setoguchi
                //--------------------------------
                //Get #nFile, , m_nZKON         public double m_nZKON;             //As Double '今期Z。        0
                //(del)     Genba_S.m_nZKON = br.ReadDouble();              //23/12/20 K.Setoguchi
                Genba_S.OPFix.m_nZKON = br.ReadDouble();                    //23/12/20 K.Setoguchi
                //--------------------------------
                //Get #nFile, , m_nLatKON       public double m_nLatKON;           //As Double '今期緯度(度)。    0
                //(del)     Genba_S.m_nLatKON = br.ReadDouble();            //23/12/20 K.Setoguchi
                Genba_S.OPFix.m_nLatKON = br.ReadDouble();                  //23/12/20 K.Setoguchi
                //--------------------------------
                //Get #nFile, , m_nLonKON       public double m_nLonKON;           //As Double '今期経度(度)。    0
                //(del)     Genba_S.m_nLonKON = br.ReadDouble();            //23/12/20 K.Setoguchi
                Genba_S.OPFix.m_nLonKON = br.ReadDouble();                  //23/12/20 K.Setoguchi
                //--------------------------------
                //Get #nFile, , m_nHeightKON    public double m_nHeightKON;        //As Double '今期楕円体高(ｍ)。  0
                //(del)     Genba_S.m_nHeightKON = br.ReadDouble();         //23/12/20 K.Setoguchi
                Genba_S.OPFix.m_nHeightKON = br.ReadDouble();               //23/12/20 K.Setoguchi
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
     //23/12/20 K.setoguchi@NV---------->>>>>>>>>>>
    //(del)     private void InitKON()
    //(del)     {
    //      ＜記載を削除し、上に移動する
    //(del)     {
    //<<<<<<<<<-----------23/12/20 K.setoguchi@NV

}
