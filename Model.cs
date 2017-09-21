using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace TaskManagerWatcher
{
    public class Model
    {
        private List<CheckProc> _ProcessList;
        public List<CheckProc> ProcessList
        {
            get
            {
                if (_ProcessList == null)
                {
                    _ProcessList = new List<CheckProc>();
                    _ProcessList.Add(new CheckProc("result", GetProgramPath("result")));
                    _ProcessList.Add(new CheckProc("fb", GetProgramPath("fb")));
                    _ProcessList.Add(new CheckProc("esprit", GetProgramPath("esprit")));
                    _ProcessList.Add(new CheckProc("fbdb", GetProgramPath("fbdb")));
                }
                return _ProcessList;
            }
        }
        private string startupPath;
        private string GetProgramPath(string key)
        {
            string[] files = Directory.GetFiles(StartupDir);
            foreach (string filePath in files)
            {
                if (filePath.IndexOf(key, StringComparison.OrdinalIgnoreCase) >= 0)
                    return filePath;
            }
            return "";
        }
        [DllImport("shell32.dll")]
        static extern bool SHGetSpecialFolderPath(IntPtr hwndOwner, [Out] StringBuilder lpszPath, int nFolder, bool fCreate);
        const int CSIDL_COMMON_STARTMENU = 0x16;  // All Users\Start Menu
        public string StartupDir
        {
            get
            {
                if (string.IsNullOrEmpty(startupPath))
                {
                    StringBuilder path = new StringBuilder(260);
                    SHGetSpecialFolderPath(IntPtr.Zero, path, CSIDL_COMMON_STARTMENU, false);
                    startupPath = path.ToString() + @"\Programs\Startup";
                }
                return startupPath;
            }
        }
    }
    public class CheckProc
    {
        public CheckProc(string key, string path)
        {
            Key = key;
            ProcPath = path;
        }
        public string Key { get; set; }
        public string ProcPath { get; set; }
    }
    public class WatcherLog
    {
        private string logPath = @"D:\TaskManagerWatcher.txt";

        public void WriteLog(string content)
        {
            using (StreamWriter sw = new StreamWriter(logPath, true))
            {
                string dateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                sw.WriteLine(dateTime + "  " + content);
            }
        }
    }
}
