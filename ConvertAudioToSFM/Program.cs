using System;
using System.IO;
using System.Windows.Forms;
using System.Security.Principal;

namespace ConvertAudioToSFM
{
    internal class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            if (IsAdministrator() == false)
            {
                MessageBox.Show("You need to run this in administrator mode for whatever fucking reason, else ffmpeg or cmd stuff do not work! :D\nYou can check the code here, and if you can fix it, please tell me.\nhttps://github.com/ShiftTGC/ConvertAudioToSFM", "Oopsie, YOU, yes, YOU, did a fucky wucky!",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            string fullPath; 
            Logo();
            OpenFileDialog fd = new OpenFileDialog();
            fd.Filter = "All Media Files|*.wav;*.aac;*.wma;*.wmv;*.avi;*.mpg;*.mpeg;*.m1v;*.mp2;*.mp3;*.mpa;*.mpe;*.m3u;*.mp4;*.mov;*.3g2;*.3gp2;*.3gp;*.3gpp;*.m4a;*.cda;*.aif;*.aifc;*.aiff;*.mid;*.midi;*.rmi;*.mkv;*.WAV;*.AAC;*.WMA;*.WMV;*.AVI;*.MPG;*.MPEG;*.M1V;*.MP2;*.MP3;*.MPA;*.MPE;*.M3U;*.MP4;*.MOV;*.3G2;*.3GP2;*.3GP;*.3GPP;*.M4A;*.CDA;*.AIF;*.AIFC;*.AIFF;*.MID;*.MIDI;*.RMI;*.MKV";
            fd.ShowDialog();
            fullPath = fd.FileName;
            Console.Write(fullPath);
            //Console.WriteLine(Path.GetDirectoryName(fullPath));
            //if (fullPath.ToLower() == "folder"); //May end up making a easy way to mass-convert... Maybe...
            //}
            string startCMD;
            startCMD = $"/C ffmpeg -y -i \"{fullPath}\" -ar 44100 \"{AddSuffix(fullPath, "_sfm-16PCM-44.1khz")}\"";
            Console.WriteLine(startCMD);
            System.Diagnostics.Process.Start("CMD.exe", startCMD);

        }

        static string AddSuffix(string filename, string suffix)
        {
            //Stolen from https://stackoverflow.com/questions/24367571/insert-string-into-a-filepath-string-before-the-file-extension-c-sharp
            string fDir = Path.GetDirectoryName(filename);
            string fName = Path.GetFileNameWithoutExtension(filename);
            string fExt = ".wav";
            return Path.Combine(fDir, String.Concat(fName, suffix, fExt));
        }

        static void Logo()
        {
            Console.WriteLine("" +
                "===============================\n" +
                "||Shift's SFM Audio Converter||\n" +
                "===============================v1.3\n" +
                "INFO: ffmpeg not included. ffmpeg must be accacible through PATH\n" +
                "You can download ffmpeg for free here: https://ffmpeg.org/\n" +
                "---------------------------------------------------------------------------------------------\n" +
                "This tool was thrown together by ShiftTGC\n" +
                "Twitter: @ShiftTGC\n" +
                "GitHub: ShiftTGC\n" +
                "YouTube: ShiftTGC\n" +
                "Twiitch: ShiftTGC\n" +
                "XBOX LIVE: .... Come on. You should know by now.\n" +
                "\n" +
                "In case of batch-processing, this should help you get staretd. Too lazy to implement atm:\n" +
                "ffmpeg -y -i \"input.*\" -ar 44100 \"output.wav\"");
        }

        public static bool IsAdministrator()
        {
            using (WindowsIdentity identity = WindowsIdentity.GetCurrent())
            {
                WindowsPrincipal principal = new WindowsPrincipal(identity);
                return principal.IsInRole(WindowsBuiltInRole.Administrator);
            }
        }


    }
}