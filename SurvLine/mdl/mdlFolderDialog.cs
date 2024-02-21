using SurvLine.mdl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using static SurvLine.mdl.MdiDefine;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace SurvLine
{
    internal class MdlFolderDialog
    {

        MdiDefine mdiDefine = new MdiDefine();

        //==========================================================================================
        /*[VB]
            '*******************************************************************************
            'フォルダダイアログ

            Option Explicit

            Private Declare Function SendMessageLS Lib "user32" Alias "SendMessageA" (ByVal hWnd As Long, ByVal msg As Long, ByVal wParam As Long, ByVal sParam As String) As Long

            Public FOLDER_DIALOG As FolderDialog 'フォルダダイアログ。
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        //'*******************************************************************************
        //'フォルダダイアログ

        //Option Explicit

        //DEFINE 移動Private Declare Function SendMessageLS Lib "user32" Alias "SendMessageA" (ByVal hWnd As Long, ByVal msg As Long, ByVal wParam As Long, ByVal sParam As String) As Long

        public FolderDialog FOLDER_DIALOG;      //'フォルダダイアログ。



        //==========================================================================================
        /*[VB]
            'SHBrowseForFolder 関数のコールバック関数。
            '
            '引き数：
            'hWnd Window handle to the browse dialog box.
            'uMsg Value identifying the event.
            'lParam Value dependent upon the message contained in the uMsg parameter.
            'lpData Application-defined value that was specified in the lParam member of the BROWSEINFO structure.
            '
            '戻り値：0 を返す。
            Public Function BrowseCallbackProc(ByVal hWnd As Long, ByVal uMsg As Long, ByVal lParam As Long, ByVal lpData As Long) As Long
                If uMsg = BFFM_INITIALIZED Then
                    Call SetWindowText(hWnd, FOLDER_DIALOG.Caption)
                    Call SendMessageLS(hWnd, BFFM_SETSELECTIONA, True, FOLDER_DIALOG.Path)
                End If
            End Function
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        /// <summary>
        /// 'SHBrowseForFolder 関数のコールバック関数。
        /// '
        /// '引き数：
        /// 'hWnd Window handle to the browse dialog box.
        /// 'uMsg Value identifying the event.
        /// 'lParam Value dependent upon the message contained in the uMsg parameter.
        /// 'lpData Application-defined value that was specified in the lParam member of the BROWSEINFO structure.
        /// 
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="uMsg"></param>
        /// <param name="lParam"></param>
        /// <param name="lpData"></param>
        /// <returns>
        ///    '戻り値：0 を返す。
        /// </returns>
        public long BrowseCallbackProc(long hWnd, long uMsg, long lParam, long lpData)
        {
            if (uMsg == DEFINE.BFFM_INITIALIZED)
            {
                DEFINE.SetWindowText(hWnd, FOLDER_DIALOG.Caption);
                DEFINE.SendMessageLS(hWnd, DEFINE.BFFM_SETSELECTIONA, 0xFFFF, FOLDER_DIALOG.Path);
            }
            return 0;
        }

    }
}
