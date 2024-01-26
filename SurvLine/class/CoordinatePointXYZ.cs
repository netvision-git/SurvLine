using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace SurvLine
{
    public class CoordinatePointXYZ
    {

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
            //[VB]m_nCoordinateType = COORDINATE_XYZ
            //(del)     Genba_S.CPXYZ_m_nCoordinateType = 0;        //23/12/20 K.Setoguchi
            Genba_S.CPXYZ.m_nCoordinateType = 0;                    //23/12/20 K.Setoguchi
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
            //-------------------------------------------
            //[VB]  Get #nFile, , m_nRoundY                         0       3572066.263
            //(del)     Genba_S.CPXYZ_m_nRoundY = br.ReadDouble();  //23/12/20 K.Setoguchi
            Genba_S.CPXYZ.m_nRoundY = br.ReadDouble();              //23/12/20 K.Setoguchi    
            //-------------------------------------------
            //[VB]  Get #nFile, , m_nRoundZ                         0       3663194.625
            //(del)     Genba_S.CPXYZ_m_nRoundZ = br.ReadDouble();  //23/12/20 K.Setoguchi
            Genba_S.CPXYZ.m_nRoundZ = br.ReadDouble();              //23/12/20 K.Setoguchi
            //-------------------------------------------
            if (nVersion < 6400)
            {
                //-------------------------------------------
                //m_nX = m_nRoundX
                //(del)     Genba_S.CPXYZ_m_nX = Genba_S.CPXYZ_m_nRoundX;   //23/12/20 K.Setoguchi
                Genba_S.CPXYZ.m_nX = Genba_S.CPXYZ.m_nRoundX;               //23/12/20 K.Setoguchi
                //-------------------------------------------
                //m_nY = m_nRoundY
                //(del)     Genba_S.CPXYZ_m_nY = Genba_S.CPXYZ_m_nRoundX;   //23/12/20 K.Setoguchi
                Genba_S.CPXYZ.m_nY = Genba_S.CPXYZ.m_nRoundX;               //23/12/20 K.Setoguchi
                //-------------------------------------------
                //m_nZ = m_nRoundZ
                //(del)     Genba_S.CPXYZ_m_nZ = Genba_S.CPXYZ_m_nRoundX;   //23/12/20 K.Setoguchi
                Genba_S.CPXYZ.m_nZ = Genba_S.CPXYZ.m_nRoundX;               //23/12/20 K.Setoguchi



                //-------------------------------------------//23/12/20 K.Setoguchi
                //[VB]      m_nRoundX = JpnRound(m_nX, ACCOUNT_DECIMAL_XYZ)                                             //23/12/20 K.Setoguchi
                MdlUtility mdlUtility = new MdlUtility();                                                               //23/12/20 K.Setoguchi
                Genba_S.CPXYZ.m_nRoundX = mdlUtility.JpnRound(Genba_S.CPXYZ.m_nX, MdlAccountMake.ACCOUNT_DECIMAL_XYZ);  //23/12/20 K.Setoguchi
                //[VB]      m_nRoundY = JpnRound(m_nY, ACCOUNT_DECIMAL_XYZ)     //23/12/20 K.Setoguchi                  //23/12/20 K.Setoguchi       
                Genba_S.CPXYZ.m_nRoundY = mdlUtility.JpnRound(Genba_S.CPXYZ.m_nY, MdlAccountMake.ACCOUNT_DECIMAL_XYZ);  //23/12/20 K.Setoguchi
                //[VB]      m_nRoundZ = JpnRound(m_nZ, ACCOUNT_DECIMAL_XYZ)     //23/12/20 K.Setoguchi                  //23/12/20 K.Setoguchi
                Genba_S.CPXYZ.m_nRoundZ = mdlUtility.JpnRound(Genba_S.CPXYZ.m_nZ, MdlAccountMake.ACCOUNT_DECIMAL_XYZ);  //23/12/20 K.Setoguchi
                //-------------------------------------------//23/12/20 K.Setoguchi




            }
            else
            {
                //-------------------------------------------
                //Get #nFile, , m_nX                                0 /-3796335.4701
                //(del)     Genba_S.CPXYZ_m_nX = br.ReadDouble();           //23/12/20 K.Setoguchi
                Genba_S.CPXYZ.m_nX = br.ReadDouble();                       //23/12/20 K.Setoguchi
                //-------------------------------------------   
                //Get #nFile, , m_nY                                0 /3572066.2631
                //(del)     Genba_S.CPXYZ_m_nY = br.ReadDouble();           //23/12/20 K.Setoguchi
                Genba_S.CPXYZ.m_nY = br.ReadDouble();                       //23/12/20 K.Setoguchi
                //Get #nFile, , m_nZ                                0 /3663194.625 
                //(del)     Genba_S.CPXYZ_m_nZ = br.ReadDouble();           //23/12/20 K.Setoguchi
                Genba_S.CPXYZ.m_nZ = br.ReadDouble();                       //23/12/20 K.Setoguchi    
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
