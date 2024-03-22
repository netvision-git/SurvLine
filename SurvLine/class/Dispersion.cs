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
        /*
        '*******************************************************************************
        '分散・共分散

        Option Explicit
        */

        /*
        'プロパティ
        Public XX As Double '分散X。
        Public XY As Double '共分散XY。
        Public YY As Double '分散Y。
        Public XZ As Double '共分散XZ。
        Public YZ As Double '共分散YZ。
        Public ZZ As Double '分散Z。
        */
        //'プロパティ
        public double XX;   //'分散X。
        public double XY;   //'共分散XY。
        public double YY;   //'分散Y。
        public double XZ;   //'共分散XZ。
        public double YZ;   //'共分散YZ。
        public double ZZ;   //'分散Z。


        //==========================================================================================
        /*[VB]
        '*******************************************************************************
        'プロパティ

        '分散・共分散。
        '
        'このプロパティはデフォルトプロパティである。Letを使えば｢値の代入｣ができる。いわゆるC++で言うところのoperator=である。
        '
        '引き数：
        'clsDispersion コピー元のオブジェクト。
        Property Let Dispersion(ByVal clsDispersion As Dispersion)
            XX = clsDispersion.XX
            XY = clsDispersion.XY
            YY = clsDispersion.YY
            XZ = clsDispersion.XZ
            YZ = clsDispersion.YZ
            ZZ = clsDispersion.ZZ
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '*******************************************************************************
        'プロパティ

        '分散・共分散。
        '
        'このプロパティはデフォルトプロパティである。Letを使えば｢値の代入｣ができる。いわゆるC++で言うところのoperator=である。
        '
        '引き数：
        'clsDispersion コピー元のオブジェクト。
        */
        public void P_Dispersion(Dispersion clsDispersion)
        {
            XX = clsDispersion.XX;
            XY = clsDispersion.XY;
            YY = clsDispersion.YY;
            XZ = clsDispersion.XZ;
            YZ = clsDispersion.YZ;
            ZZ = clsDispersion.ZZ;
            return;
        }
        //==========================================================================================

        //==========================================================================================
        //[C#]
        public double Dispersion_XX()
        {
            return XX;
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
            Put #nFile, , XX
            Put #nFile, , XY
            Put #nFile, , YY
            Put #nFile, , XZ
            Put #nFile, , YZ
            Put #nFile, , ZZ
        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]      //0304  //2
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

        //==========================================================================================


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
            //double XX, XY, YY, XZ, YZ, ZZ;

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
