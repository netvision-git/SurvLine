using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace SurvLine
{
    internal class MdlNSCAB
    {

        //'*******************************************************************************
        //'NSCAB
        //
        //Option Explicit

        //==========================================================================================
        /*[VB]
            Public Declare Function NSFCCreate Lib "NSCAB" (ByVal lpDstPath As String, ByVal pfnProgress As Long) As Long
            Public Declare Function NSFCAddFile Lib "NSCAB" (ByVal hNSFC As Long, ByVal lpSrcPath As String) As Long
            Public Declare Function NSFCAddFolder Lib "NSCAB" (ByVal hNSFC As Long, ByVal lpSrcPath As String) As Long
            Public Declare Function NSFCDestroy Lib "NSCAB" (ByVal hNSFC As Long) As Long
            Public Declare Function NSFDCreate Lib "NSCAB" (ByVal pfnProgress As Long) As Long
            Public Declare Function NSFDCopy Lib "NSCAB" (ByVal hNSFD As Long, ByVal lpSrcPath As String, ByVal lpDstPath As String) As Long
            Public Declare Function NSFDDestroy Lib "NSCAB" (ByVal hNSFD As Long) As Long

            Public ProgressNSCAB As ProgressInterface
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]

        [DllImport("NSCAB")] //K.S
        public static extern long NSFCCreate(string lpDstPath, long pfnProgress);
        [DllImport("NSCAB")] //K.S
        public static extern long NSFCAddFile(long hNSFC, string lpSrcPath);
        [DllImport("NSCAB")] //K.S
        public static extern long NSFCAddFolder(long hNSFC, string lpSrcPath);
        [DllImport("NSCAB")] //K.S
        public static extern long NSFCDestroy(long hNSFC);
        [DllImport("NSCAB")] //K.S
        public static extern long NSFDCreate(long pfnProgress);
        [DllImport("NSCAB")] //K.S
        public static extern long NSFDCopy(long hNSFD, string lpSrcPath, string lpDstPath);
        [DllImport("NSCAB")] //K.S
        public static extern long NSFDDestroy(long hNSFD);


        public ProgressInterface ProgressNSCAB;


        //==========================================================================================
        /*[VB]
            '進捗。
            '
            '引き数：
            'nCurSize 圧縮の場合、圧縮サイズの累計バイト数。展開の場合、0。
            '
            '戻り値：
            '処理を続ける場合は 1 を返す。
            'キャンセルする場合は 0 を返す。
            Public Function NSCABProgress(ByVal nCurSize As Long) As Long

                On Error GoTo ErrorHandler


                If ProgressNSCAB.Cancel Then
                    NSCABProgress = 0
                Else
                    NSCABProgress = 1
                End If


                Exit Function


            ErrorHandler:
                NSCABProgress = 0


            End Function
            [VB]*/
        //------------------------------------------------------------------------------------------
        //[C#]
        public long NSCABProgress(long nCurSize)
        {
            //On Error GoTo ErrorHandler
            long NSCABProgress = 0;

            try
            {
                NSCABProgress = ProgressNSCAB.CheckCancel() ? 0 : 1;
                return NSCABProgress;

            }
            catch (Exception)
            {
                //ErrorHandler:
                NSCABProgress = 0;
            }


            return NSCABProgress;
        }




    }
}
