using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Net.WebRequestMethods;

namespace SurvLine
{
    public class ObservationCommonAttributes
    {

        MdlUtility mdiUtility = new MdlUtility();
        EccentricCorrectionParam eccentricCorrectionParam = new EccentricCorrectionParam();
        CoordinatePointFix coordinatePointFix = new CoordinatePointFix();
        CoordinatePointXYZ coordinatePointXYZ = new CoordinatePointXYZ();



        //***************************************************************************
        //***************************************************************************
        /// <summary>
        ///'読み込み。
        ///'    長男が親観測点の Load を呼ぶ。
        ///     （VB Load関数をVC用に変更）
        ///'
        ///'引き数：
        ///  '引き数：
        /// バイナリファイル
        /// バージョン番号
        /// 読み込みデータ
        ///     ・ロードした観測点を保持するためのテンポラリ配列。配列の要素は(0 To ...)。Redim clsObservationPoints(0) で初期化されていること。
        ///     ・子観測点。
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
            //-----------------------------------------------------------------------
            //[VB]  Number = GetString(nFile)   public string Number;               //As String '番号。    "305"
            Genba_S.Number = mdiUtility.FileRead_GetString(br);

            //-----------------------------------------------------------------------
            //[VB]  Name = GetString(nFile)                                         "明地"
            Genba_S.Name = mdiUtility.FileRead_GetString(br);
            //-----------------------------------------------------------------------
            if (nVersion < 5600)
            {
                Genba_S.GenuineNumber = Genba_S.Number;
                Genba_S.GenuineName = Genba_S.Name;
            }
            else
            {
                //-----------------------------------------
                //[VB]  GenuineNumber = GetString(nFile)
                Genba_S.GenuineNumber = mdiUtility.FileRead_GetString(br);
                //-----------------------------------------
                //[VB]  GenuineName = GetString(nFile)
                Genba_S.Number = mdiUtility.FileRead_GetString(br);
            }
            //-----------------------------------------------------------------------
            //[VB]  Get #nFile, , ObjectType
            Genba_S.ObjectType = br.ReadInt32(); ;
            if (nVersion < 6200){
                //[VB]  Get #nFile, , OldEditCode
                Genba_S.OldEditCode = br.ReadInt32();
            }
            else
            {
                Genba_S.OldEditCode = 0;
            }

            //------------------------------------
            //[VB]  Call m_clsCoordinateFixed.Load(nFile, nVersion)             OK
            coordinatePointFix.Load(br, nVersion, ref Genba_S);

            if (nVersion >= 2100){
                //------------------------------------
                //[VB] Call m_clsEccentricCorrectionParam.Load(nFile, nVersion)     OK
                eccentricCorrectionParam.Load(br, nVersion, ref Genba_S);
                //------------------------------------
                //[VB] Call m_clsVectorEccentric.Load(nFile, nVersion)              OK
                coordinatePointXYZ.Load(br, nVersion, ref Genba_S);
                //------------------------------------
            }
        }
        //---------------------------------------------------------------------
        //'読み込み。
        //'
        //'引き数：
        //'nFile ファイル番号。
        //'nVersion ファイルバージョン。
        //Public Sub Load(ByVal nFile As Integer, ByVal nVersion As Long)
        //    Number = GetString(nFile)
        //    Name = GetString(nFile)
        //    If nVersion< 5600 Then
        //        GenuineNumber = Number
        //        GenuineName = Name
        //    Else
        //        GenuineNumber = GetString(nFile)
        //        GenuineName = GetString(nFile)
        //    End If
        //    Get #nFile, , ObjectType
        //    If nVersion< 6200 Then
        //        Get #nFile, , OldEditCode
        //    Else
        //        OldEditCode = 0
        //    End If
        //    Call m_clsCoordinateFixed.Load(nFile, nVersion)
        //    If nVersion >= 2100 Then
        //        Call m_clsEccentricCorrectionParam.Load(nFile, nVersion)
        //        Call m_clsVectorEccentric.Load(nFile, nVersion)
        //    End If
        //End Sub
        //***************************************************************************


    }
}
