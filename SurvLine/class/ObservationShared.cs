using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using System.Collections;
namespace SurvLine
{
    internal class ObservationShared
    {

        //'*******************************************************************************
        //'観測共有情報
        //
        //Option Explicit
        //
        //'測位方法情報。
        //Private Type MEASUREMENTINFO
        //    DispName As String '表示名称。
        //    MountHori As Double 'アンテナ底部距離。
        //    MountVert As Double 'アンテナ底部距離。
        //    Unknown As Boolean 'アノンフラグ。
        //End Type
        public struct MEASUREMENTINFO
        {
            string DispName;        // As String '表示名称。
            double MountHori;       // As Double 'アンテナ底部距離。
            double MountVert;       // As Double 'アンテナ底部距離。
        }

        //public sealed class Collection : System.Collections.IList;

        //
        //'プロパティ
        //
        //'インプリメンテーション
        private MEASUREMENTINFO m_tMeasurementInfos;    // As MEASUREMENTINFO '測位方法情報。配列の要素は(-1 To ...)、要素 -1 は未使用。
        private object m_objMeasurementInfoIndex;   // As Collection '測位方法インデックス。要素はインデックス番号。キーは"測位方法"+"アンテナ種別"。

        //'*******************************************************************************
        //'プロパティ
        //
        //'アンテナ測位方法表示文字列。
        //Property Get DispMeasurement(ByVal sAntType As String, ByVal sAntMeasurement As String) As String
        //    Dim vIndex As Variant
        //    If LookupCollectionVariant(m_objMeasurementInfoIndex, vIndex, sAntMeasurement & sAntType) Then
        //        DispMeasurement = m_tMeasurementInfos(vIndex).DispName
        //    Else
        //        DispMeasurement = ""
        //    End If
        //End Property







    }
}
