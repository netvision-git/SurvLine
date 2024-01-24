using SurvLine;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static NTS.MdlAccountMake;
using static System.Collections.Specialized.BitVector32;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace SurvLine
{
    internal class AccountParam
    {

        MdlUtility mdlUtility = new MdlUtility();

        //'*******************************************************************************
        //'帳票パラメータ
        //'
        //'帳票出力の条件を保持する。
        //
        //Option Explicit
        //
        public RANGE_TYPE RangeType;            // As RANGE_TYPE '範囲タイプ。
        public string Session;                  // As String 'セッション名。
        public SELECTED_OBJECT_TYPE ObjectType; // As SELECTED_OBJECT_TYPE '選択オブジェクトタイプ。m_objSelectedObjects の内容を示す。
        public bool Initial;                    // As Boolean '初期フラグ。True=帳票表示を一回もしていない。False=既に帳票表示をしている。

        //'インプリメンテーション
        //  Private m_objSelectedObjects As New Collection '選択オブジェクト。RangeType が RANGE_TYPE_OBJECT の時に有効。要素はオブジェクトのキー(文字列)。キーは要素と同じ。
        Collection m_objSelectedObjects;

        //'*******************************************************************************
        //'プロパティ
        //




        //'*******************************************************************************
        //'*******************************************************************************
        /// <summary>
        /// '帳票パラメータ。
        /// '
        /// 'このプロパティはデフォルトプロパティである。Letを使えば｢値の代入｣ができる。いわゆるC++で言うところのoperator=である。
        /// '
        /// '引き数：
        ///     clsAccountParam コピー元のオブジェクト。
        /// </summary>
        /// <param name="clsAccountParam"></param>
        //---------------------------------------------------------------------------------
        public AccountParam()
        {
            Class_Initialize();     //InitializeComponent();
        }
        //---------------------------------------------------------------------------------


        //---------------------------------------------------------------------------------
        //'帳票パラメータ。
        //---------------------------------------------------------------------------------

        //'*******************************************************************************
        //'*******************************************************************************
        /// <summary>
        /// '帳票パラメータ。
        /// '
        /// 'このプロパティはデフォルトプロパティである。Letを使えば｢値の代入｣ができる。いわゆるC++で言うところのoperator=である。
        /// 
        /// </summary>
        /// <param name="clsAccountParam"></param>
        public AccountParam(AccountParam clsAccountParam)
        {
            Class_Initialize();     //InitializeComponent();

            RangeType = clsAccountParam.RangeType;
            Session = clsAccountParam.Session;
            ObjectType = clsAccountParam.ObjectType;
    //        SelectedObjects = clsAccountParam.SelectedObjects;
            Initial = clsAccountParam.Initial;

        }
        //---------------------------------------------------------------------------------
        //'帳票パラメータ。
        //'
        //'このプロパティはデフォルトプロパティである。Letを使えば｢値の代入｣ができる。いわゆるC++で言うところのoperator=である。
        //'
        //'引き数：
        //'clsAccountParam コピー元のオブジェクト。
        //Property Let AccountParam(ByVal clsAccountParam As AccountParam)
        //    RangeType = clsAccountParam.RangeType
        //    Session = clsAccountParam.Session
        //    ObjectType = clsAccountParam.ObjectType
        //    SelectedObjects = clsAccountParam.SelectedObjects
        //    Initial = clsAccountParam.Initial
        ///End Property
        //'*******************************************************************************


        //'*******************************************************************************
        //'*******************************************************************************
        private void SelectedObjects(Collection objSelectedObjects)
        {
            m_objSelectedObjects = new Collection();

            long vKey;       // As Variant

  //          foreach (vKey in objSelectedObjects)    //foreach (型名 変数 in コレクション) 文
  //          {
  //              m_objSelectedObjects.Add(vKey, vKey);
  //          }

        }
        //---------------------------------------------------------------------------------
        //        '選択オブジェクト。 For Each 子要素 As 子要素の型名 In 親要素(複数の値が入った変数)
        //        Property Let SelectedObjects(ByVal objSelectedObjects As Collection)
        //            Set m_objSelectedObjects = New Collection
        //            Dim vKey As Variant
        //            For Each vKey In objSelectedObjects
        //                Call m_objSelectedObjects.Add(vKey, vKey)
        //            Next
        //        End Property
        //'*******************************************************************************


        //'*******************************************************************************
        //'*******************************************************************************
        /// <summary>
        /// '選択オブジェクト。
        /// 
        /// </summary>
        /// <returns></returns>
        private Collection SelectedObjects()
        {
            Collection SelectedObjects = m_objSelectedObjects;
            return SelectedObjects;
        }
        //        '選択オブジェクト。
        //        Property Get SelectedObjects() As Collection
        //            Set SelectedObjects = m_objSelectedObjects
        //        End Property
        //'*******************************************************************************






        //<<<<<<<<<-----------24/01/04 K.setoguchi@NV



        //'*******************************************************************************
        //'*******************************************************************************
        /// <summary>
        /// 'イベント
        ///
        /// '初期化。
        /// </summary>
        private void Class_Initialize()
        {
            m_objSelectedObjects = new Collection();  // '選択オブジェクト。RangeType が RANGE_TYPE_OBJECT の時に有効。要素はオブジェクトのキー(文字列)。キーは要素と同じ。
            Initial = true;

        }
        //'*******************************************************************************
        //'イベント
        //
        //'初期化。
        //Private Sub Class_Initialize()
        //    Initial = True
        //End Sub
        //'*******************************************************************************


        //'*******************************************************************************
        //'*******************************************************************************
        public void Save(BinaryWriter bw)
        {
            bw.Write((Int32)RangeType);                     //    Put #nFile, , RangeType
            mdlUtility.FileWrite_PutString(bw, Session);    //    Call PutString(nFile, Session)
            bw.Write((Int32)ObjectType);                    //    Put #nFile, , ObjectType
            mdlUtility.PutFileBool(bw,Initial);                       //    Put #nFile, , Initial

            bw.Write(m_objSelectedObjects.Count);           //    Put #nFile, , m_objSelectedObjects.Count
            //    Dim vKey As Variant
            //    For Each vKey In m_objSelectedObjects
            //        Call PutString(nFile, vKey)
            //    Next
        }

        //'メソッド
        //
        //'保存。
        //'
        //'引き数：
        //'nFile ファイル番号。
        //Public Sub Save(ByVal nFile As Integer)
        //
        //    Put #nFile, , RangeType
        //    Call PutString(nFile, Session)
        //    Put #nFile, , ObjectType
        //    Put #nFile, , Initial
        //    
        //    Put #nFile, , m_objSelectedObjects.Count
        //    Dim vKey As Variant
        //    For Each vKey In m_objSelectedObjects
        //        Call PutString(nFile, vKey)
        //    Next
        //
        //End Sub
        //'*******************************************************************************


        
        public void Load(BinaryReader br, long nVersion)
        {

        }
//'読み込み。
//'
//'引き数：
//'nFile ファイル番号。
//'nVersion ファイルバージョン。
//Public Sub Load(ByVal nFile As Integer, ByVal nVersion As Long)
//
//    Get #nFile, , RangeType
//    Session = GetString(nFile)
//
//
//    If nVersion< 3200 Then
//        ObjectType = SELECTED_OBJECT_TYPE_UNKNOWN
//    Else
//        Get #nFile, , ObjectType
//    End If
//    If nVersion < 4300 Then
//        Initial = True
//    Else
//        Get #nFile, , Initial
//    End If
//
//
//    Dim nCount As Long
//    Get #nFile, , nCount
//    Dim vKey As Variant
//    Dim i As Long
//    For i = 0 To nCount - 1
//        vKey = GetString(nFile)
//        Call m_objSelectedObjects.Add(vKey, vKey)
//    Next
//    If nVersion < 1200 Then Set m_objSelectedObjects = New Collection
//
//End Sub

#if false
'指定されたオブジェクトと比較する。
'
'引き数：
'clsAccountParam 比較対照オブジェクト。
'
'戻り値：
'一致する場合 True を返す。
'それ以外の場合 False を返す。
Public Function Compare(ByVal clsAccountParam As AccountParam) As Boolean

    Compare = False

    If RangeType<> clsAccountParam.RangeType Then Exit Function
    If StrComp(Session, clsAccountParam.Session) <> 0 Then Exit Function
    If ObjectType<> clsAccountParam.ObjectType Then Exit Function
    If Initial <> clsAccountParam.Initial Then Exit Function


    If m_objSelectedObjects.Count<> clsAccountParam.SelectedObjects.Count Then Exit Function
    Dim vKey As Variant
    For Each vKey In clsAccountParam.SelectedObjects
        If Not LookupCollectionVariant(m_objSelectedObjects, vKey, vKey) Then Exit Function
    Next

    Compare = True


End Function


#endif

    }
}
