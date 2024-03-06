using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using System.IO;
using static SurvLine.mdl.DEFINE;
using static SurvLine.mdl.MdlNSDefine;
using static SurvLine.mdl.MdlUtility;
using SurvLine.mdl;
using System.Runtime.InteropServices;
using static System.Net.WebRequestMethods;

namespace SurvLine
{
    public class ObservationShared
    {
        //==========================================================================================
        /*[VB]
        '*******************************************************************************
        '観測共有情報

        Option Explicit
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '測位方法情報。
        Private Type MEASUREMENTINFO
            DispName As String '表示名称。
            MountHori As Double 'アンテナ底部距離。
            MountVert As Double 'アンテナ底部距離。
            Unknown As Boolean 'アノンフラグ。
        End Type
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'測位方法情報。
        private struct MEASUREMENTINFO
        {
            public string DispName;             //'表示名称。
            public double MountHori;            //'アンテナ底部距離。
            public double MountVert;            //'アンテナ底部距離。
            public bool Unknown;                //'アノンフラグ。
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'プロパティ

        'インプリメンテーション
        Private m_tMeasurementInfos() As MEASUREMENTINFO '測位方法情報。配列の要素は(-1 To ...)、要素 -1 は未使用。
        Private m_objMeasurementInfoIndex As Collection '測位方法インデックス。要素はインデックス番号。キーは"測位方法"+"アンテナ種別"。
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        'プロパティ

        'インプリメンテーション
        */
        private MEASUREMENTINFO[] m_tMeasurementInfos;                      //'測位方法情報。配列の要素は(-1 To ...)、要素 -1 は未使用。
        public Dictionary<string, int> m_objMeasurementInfoIndex;          //'測位方法インデックス。要素はインデックス番号。キーは"測位方法"+"アンテナ種別"。
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '*******************************************************************************
        'プロパティ

        'アンテナ測位方法表示文字列。
        Property Get DispMeasurement(ByVal sAntType As String, ByVal sAntMeasurement As String) As String
            Dim vIndex As Variant
            If LookupCollectionVariant(m_objMeasurementInfoIndex, vIndex, sAntMeasurement & sAntType) Then
                DispMeasurement = m_tMeasurementInfos(vIndex).DispName
            Else
                DispMeasurement = ""
            End If
        End Property
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '*******************************************************************************
        'プロパティ

        'アンテナ測位方法表示文字列。
        */
        public string DispMeasurement(string sAntType, string sAntMeasurement)
        {
            int vIndex = 0;
            if (LookupCollectionVariant_si(m_objMeasurementInfoIndex, ref vIndex, sAntMeasurement + sAntType))
            {
                return m_tMeasurementInfos[vIndex].DispName;
            }
            else
            {
                return "";
            }
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '*******************************************************************************
        'イベント

        '初期化。
        Private Sub Class_Initialize()

            On Error GoTo ErrorHandler
    
            '測位方法情報。
            ReDim m_tMeasurementInfos(-1 To -1)
            Set m_objMeasurementInfoIndex = New Collection
            Dim sPath As String
            sPath = App.Path & "\" & PROFILE_ANT_FILE
            Dim nUnknown As Long
            nUnknown = GetPrivateProfileInt(PROFILE_ANT_SEC_LIST, PROFILE_ANT_KEY_UNKNOWN, 0, sPath)
            Dim nCount As Long
            nCount = GetPrivateProfileInt(PROFILE_ANT_SEC_LIST, PROFILE_ANT_KEY_COUNT, 0, sPath)
            Dim i As Long
            For i = 0 To nCount - 1
                Dim sAntType As String
                sAntType = GetPrivateProfileString(PROFILE_ANT_SEC_LIST, PROFILE_ANT_KEY_ANT & CStr(i), "", sPath)
                Dim nUBound As Long
                nUBound = 0
                Do
                    Dim clsStringTokenizer As New StringTokenizer
                    clsStringTokenizer.Source = GetPrivateProfileString(sAntType, PROFILE_ANT_KEY_MEASUREMENT & CStr(nUBound), "", sPath)
                    Dim vIndex As Variant
                    vIndex = UBound(m_tMeasurementInfos) + 1
                    If clsStringTokenizer.Source = "" Then Exit Do
                    ReDim Preserve m_tMeasurementInfos(-1 To vIndex)
                    Call clsStringTokenizer.Begin
                    Dim sToken As String
                    sToken = clsStringTokenizer.NextToken
                    m_tMeasurementInfos(vIndex).DispName = Mid$(sToken, 2, Len(sToken) - 2)
                    Dim sAntMeasurement As String
                    sAntMeasurement = clsStringTokenizer.NextToken
                    sToken = clsStringTokenizer.NextToken
                    m_tMeasurementInfos(vIndex).MountHori = Val(clsStringTokenizer.NextToken)
                    m_tMeasurementInfos(vIndex).MountVert = Val(clsStringTokenizer.NextToken)
                    m_tMeasurementInfos(vIndex).Unknown = (i = nUnknown)
                    Call SetAtCollectionVariant(m_objMeasurementInfoIndex, vIndex, sAntMeasurement & sAntType)
                    nUBound = nUBound + 1
                Loop
            Next


            Exit Sub


        ErrorHandler:
            Call mdlMain.ErrorExit

        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '*******************************************************************************
        'イベント

        '初期化。
        */
        public ObservationShared()
        {
            Class_Initialize();
        }

        public void Class_Initialize()
        {
            try
            {
                //'測位方法情報。
                MEASUREMENTINFO[] tMeasurementInfos = new MEASUREMENTINFO[0];
                Dictionary<string, int> objMeasurementInfoIndex = new Dictionary<string, int>();
                //DEFINE mdlDefine = new DEFINE();
                //MdlUtility mdlMdlUtility = new MdlUtility();
                string sPath;
                string AppPath = Path.GetDirectoryName(Application.ExecutablePath);
                sPath = AppPath + "\\" + PROFILE_ANT_FILE;
                long nUnknown;
                nUnknown = GetPrivateProfileInt(PROFILE_ANT_SEC_LIST, PROFILE_ANT_KEY_UNKNOWN, 0, sPath);
                long nCount;
                nCount = GetPrivateProfileInt(PROFILE_ANT_SEC_LIST, PROFILE_ANT_KEY_COUNT, 0, sPath);
                long i;
                int vIndex = 0;
                string sAntType;
                long nUBound;
                for (i = 0; i < nCount; i++)
                {
                    sAntType = GetPrivateProfileString(PROFILE_ANT_SEC_LIST, PROFILE_ANT_KEY_ANT + i.ToString(), "", sPath);
                    nUBound = 0;
                    do
                    {
                        StringTokenizer clsStringTokenizer = new StringTokenizer();
                        clsStringTokenizer.Source = GetPrivateProfileString(sAntType, PROFILE_ANT_KEY_MEASUREMENT + nUBound.ToString(), "", sPath);
                        if (clsStringTokenizer.Source == "")
                        {
                            break;
                        }
                        vIndex++;
                        nUBound++;
                    } while (true);
                }
                tMeasurementInfos = new MEASUREMENTINFO[vIndex + 1];
                vIndex = 0;
                for (i = 0; i < nCount; i++)
                {
                    sAntType = GetPrivateProfileString(PROFILE_ANT_SEC_LIST, PROFILE_ANT_KEY_ANT + i.ToString(), "", sPath);
                    nUBound = 0;
                    do
                    {
                        StringTokenizer clsStringTokenizer = new StringTokenizer();
                        clsStringTokenizer.Source = GetPrivateProfileString(sAntType, PROFILE_ANT_KEY_MEASUREMENT + nUBound.ToString(), "", sPath);
                        ////vIndex = m_tMeasurementInfos.Length + 1;
                        //vIndex = m_tMeasurementInfos.Length;
                        if (clsStringTokenizer.Source == "")
                        {
                            break;
                        }
                        //m_tMeasurementInfos = new MEASUREMENTINFO[vIndex + 1];
                        clsStringTokenizer.Begin();
                        string sToken;
                        sToken = clsStringTokenizer.NextToken();
                        tMeasurementInfos[vIndex].DispName = Mid(sToken, 2, sToken.Length - 2);
                        string sAntMeasurement;
                        sAntMeasurement = clsStringTokenizer.NextToken();
                        sToken = clsStringTokenizer.NextToken();
                        tMeasurementInfos[vIndex].MountHori = Convert.ToDouble(clsStringTokenizer.NextToken());
                        tMeasurementInfos[vIndex].MountVert = Convert.ToDouble(clsStringTokenizer.NextToken());
                        tMeasurementInfos[vIndex].Unknown = i == nUnknown;
                        SetAtCollectionVariant(objMeasurementInfoIndex, ref vIndex, sAntMeasurement + sAntType);
                        vIndex++;
                        nUBound++;
                    } while (true);
                }
                m_tMeasurementInfos = tMeasurementInfos;
                m_objMeasurementInfoIndex = objMeasurementInfoIndex;
                return;
            }


            catch (Exception)
            {
                //MdlMain.ErrorExit();
                return;
            }
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '終了。
        Private Sub Class_Terminate()

            On Error GoTo ErrorHandler

            Exit Sub

        ErrorHandler:
            Call mdlMain.ErrorExit

        End Sub
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'終了。
        private void Class_Terminate()
        {
            return;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        '*******************************************************************************
        'メソッド

        '位相中心高を取得する。
        '
        '引き数：
        'sAntType アンテナ種別。
        'sAntMeasurement アンテナ測位方法。
        'nHeight アンテナ高(ｍ)。
        'nTrueVertical 位相中心高(ｍ)が設定される。
        '
        '戻り値：
        '位相中心高が取得できた場合 True を返す。
        'それ以外の場合 False を返す。
        Public Function GetTrueVertical(ByVal sAntType As String, ByVal sAntMeasurement As String, ByVal nHeight As Double, ByRef nTrueVertical As Double) As Boolean
            GetTrueVertical = False
            If sAntMeasurement = TRUE_VERTICAL Then
                nTrueVertical = nHeight
                GetTrueVertical = True
            Else
                Dim nMountVertical As Double
                If GetMountVertical(sAntType, sAntMeasurement, nHeight, nMountVertical) Then
                    Dim vIndex As Variant
                    If LookupCollectionVariant(m_objMeasurementInfoIndex, vIndex, TRUE_VERTICAL & sAntType) Then
                        nTrueVertical = Sqr(m_tMeasurementInfos(vIndex).MountHori ^ 2 + (m_tMeasurementInfos(vIndex).MountVert + nMountVertical) ^ 2)
                        If nHeight< 0 Then nTrueVertical = -nTrueVertical
                        GetTrueVertical = True
                    End If
                End If
            End If
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        '*******************************************************************************
        'メソッド

        '位相中心高を取得する。
        '
        '引き数：
        'sAntType アンテナ種別。
        'sAntMeasurement アンテナ測位方法。
        'nHeight アンテナ高(ｍ)。
        'nTrueVertical 位相中心高(ｍ)が設定される。
        '
        '戻り値：
        '位相中心高が取得できた場合 True を返す。
        'それ以外の場合 False を返す。
        */
        public bool GetTrueVertical(string sAntType, string sAntMeasurement, double nHeight, ref double nTrueVertical)
        {
            if (sAntMeasurement == TRUE_VERTICAL)
            {
                nTrueVertical = nHeight;
                return true;
            }
            else
            {
                double nMountVertical = 0;
                if (GetMountVertical(sAntType, sAntMeasurement, nHeight, ref nMountVertical))
                {
                    int vIndex = 0;
                    if (LookupCollectionVariant_si(m_objMeasurementInfoIndex, ref vIndex, TRUE_VERTICAL + sAntType))
                    {
                        nTrueVertical = Math.Sqrt(Math.Pow(m_tMeasurementInfos[vIndex].MountHori, 2) + Math.Pow(m_tMeasurementInfos[vIndex].MountVert + nMountVertical, 2));
                        if (nHeight < 0)
                        {
                            nTrueVertical = -nTrueVertical;
                        }
                        return true;
                    }
                }
            }
            return false;
        }
        //==========================================================================================

        //==========================================================================================
        /*[VB]
        'アンテナマウント高を取得する。
        '
        '引き数：
        'sAntType アンテナ種別。
        'sAntMeasurement アンテナ測位方法。
        'nHeight アンテナ高(ｍ)。
        'nMountVertical アンテナマウント高(ｍ)が設定される。
        '
        '戻り値：
        'アンテナマウント高が取得できた場合 True を返す。
        'それ以外の場合 False を返す。
        Public Function GetMountVertical(ByVal sAntType As String, ByVal sAntMeasurement As String, ByVal nHeight As Double, ByRef nMountVertical As Double) As Boolean
            GetMountVertical = False
                Dim vIndex As Variant
            If LookupCollectionVariant(m_objMeasurementInfoIndex, vIndex, sAntMeasurement & sAntType) Then
                If m_tMeasurementInfos(vIndex).Unknown Then Exit Function
                Dim nDiff As Double
                nDiff = nHeight ^ 2 - m_tMeasurementInfos(vIndex).MountHori ^ 2
                If nDiff< 0 Then
                    'アンテナ種別、測位方法は解っている。値が不正。
                Else
                    If nHeight< 0 Then
                        nMountVertical = -Sqr(nDiff) - m_tMeasurementInfos(vIndex).MountVert
                    Else
                        nMountVertical = Sqr(nDiff) - m_tMeasurementInfos(vIndex).MountVert
                    End If
                    GetMountVertical = True
                End If
            Else
                'アンテナ種別不明。測位方法不明。
            End If
        End Function
        [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /*
        'アンテナマウント高を取得する。
        '
        '引き数：
        'sAntType アンテナ種別。
        'sAntMeasurement アンテナ測位方法。
        'nHeight アンテナ高(ｍ)。
        'nMountVertical アンテナマウント高(ｍ)が設定される。
        '
        '戻り値：
        'アンテナマウント高が取得できた場合 True を返す。
        'それ以外の場合 False を返す。
        */
        public bool GetMountVertical(string sAntType, string sAntMeasurement, double nHeight, ref double nMountVertical)
        {
            int vIndex = 0;
            if (LookupCollectionVariant_si(m_objMeasurementInfoIndex, ref vIndex, sAntMeasurement + sAntType))
            {
                if (m_tMeasurementInfos[vIndex].Unknown)
                {
                    return false;
                }
                double nDiff;
                nDiff = Math.Pow(nHeight, 2) - Math.Pow(m_tMeasurementInfos[vIndex].MountHori, 2);
                if (nDiff < 0)
                {
                    //'アンテナ種別、測位方法は解っている。値が不正。
                }
                else
                {
                    if (nHeight < 0)
                    {
                        nMountVertical = -Math.Sqrt(nDiff) - m_tMeasurementInfos[vIndex].MountVert;
                    }
                    else
                    {
                        nMountVertical = Math.Sqrt(nDiff) - m_tMeasurementInfos[vIndex].MountVert;
                    }
                    return true;
                }
            }
            else
            {
                //'アンテナ種別不明。測位方法不明。
            }
            return false;
        }
        //==========================================================================================
    }
}
