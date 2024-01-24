using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace SurvLine
{
    public class Dispersion
    {

        //24/01/04 K.setoguchi@NV---------->>>>>>>>>>
        //'*******************************************************************************
        //'分散・共分散
        //
        //Option Explicit
        //
        ///'プロパティ
        public double XX;   // As Double '分散X。
        public double XY;   // As Double '共分散XY。
        public double YY;   // As Double '分散Y。
        public double XZ;   // As Double '共分散XZ。
        public double YZ;   // As Double '共分散YZ。
        public double ZZ;   // As Double '分散Z。
       //*****************  //**************************************************************
       //<<<<<<<<<-----------24/01/04 K.setoguchi@NV


        //24/01/04 K.setoguchi@NV---------->>>>>>>>>>
        //***************************************************************************
        /// <summary>
        /// 読み込み。
        ///
        /// 引き数：
        ///     BinaryWriter bw バイナリファイル
        ///     グローバルエリア
        /// </summary>
        /// <param name="br"></param>
        /// <param name="Genba_S"></param>
        public void Load(BinaryReader br, ref GENBA_STRUCT_S Genba_S)
        {
            //------------------------------
            //[VB]  Get #nFile, , XX
            //[VB]  Get #nFile, , XY
            //[VB]  Get #nFile, , YY
            //[VB]  Get #nFile, , XZ
            //[VB]  Get #nFile, , YZ
            //[VB]  Get #nFile, , ZZ

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
        //***************************************************************************
        //<<<<<<<<<-----------24/01/04 K.setoguchi@NV


        //24/01/04 K.setoguchi@NV---------->>>>>>>>>>
        //***************************************************************************
        /// <summary>
        //'メソッド
        //
        //'保存。
        //'
        //'引き数：
        /// BinaryWriter bw バイナリファイル
        /// </summary>
        /// <param name="bw"></param>
        public void Save(BinaryWriter bw)
        {

            bw.Write(XX);
            bw.Write(XY);
            bw.Write(YY);
            bw.Write(XZ);
            bw.Write(YZ);
            bw.Write(ZZ);
        }
        //--------------------------------------------------------------------------------------
        //'メソッド
        //
        //'保存。
        //'
        //'引き数：
        //'nFile ファイル番号。
        //Public Sub Save(ByVal nFile As Integer)
        //    Put #nFile, , XX
        //    Put #nFile, , XY
        //    Put #nFile, , YY
        //    Put #nFile, , XZ
        //    Put #nFile, , YZ
        //    Put #nFile, , ZZ
        //End Sub
        //***************************************************************************
        //<<<<<<<<<-----------24/01/04 K.setoguchi@NV


    }
}
