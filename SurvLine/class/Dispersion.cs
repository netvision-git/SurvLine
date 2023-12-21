using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurvLine
{
    public class Dispersion
    {

        //**************************************************************************************
        //**************************************************************************************
        /// <summary>
        /// 読み込み
        /// 引き数：
        ///     'nFile ファイル番号。
        ///     'nVersion ファイルバージョン。
        /// </summary>
        /// <param name="sPath"></param>
        /// <param name="nVersion"></param>
        /// <returns>
        /// </returns>
        //**************************************************************************************
        public void Load(BinaryReader br, ref GENBA_STRUCT_S Genba_S)
        {
            //------------------------------
            //[VB]  Get #nFile, , XX
            //[VB]  Get #nFile, , XY
            //[VB]  Get #nFile, , YY
            //[VB]  Get #nFile, , XZ
            //[VB]  Get #nFile, , YZ
            //[VB]  Get #nFile, , ZZ
            double XX, XY, YY, XZ, YZ, ZZ;

            XX = br.ReadDouble(); Genba_S.XX = XX;
            XY = br.ReadDouble(); Genba_S.XX = XY;
            YY = br.ReadDouble(); Genba_S.YY = YY;
            XZ = br.ReadDouble(); Genba_S.XZ = XZ;
            YZ = br.ReadDouble(); Genba_S.YZ = YZ;
            ZZ = br.ReadDouble(); Genba_S.ZZ = ZZ;

        }
        //--------------------------------------------------------------------------------------
        //[VB]  '読み込み。
        //[VB]'
        //[VB]'引き数：
        //[VB]'nFile ファイル番号。
        //[VB]'nVersion ファイルバージョン。
        //[VB]Public Sub Load(ByVal nFile As Integer, ByVal nVersion As Long)
        //[VB]  Get #nFile, , XX
        //[VB]  Get #nFile, , XY
        //[VB]  Get #nFile, , YY
        //[VB]  Get #nFile, , XZ
        //[VB]  Get #nFile, , YZ
        //[VB]  Get #nFile, , ZZ
        //[VB]End Sub

    }
}
