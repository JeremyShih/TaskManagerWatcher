using System;
using System.Collections.Generic;
using System.IO;

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
                }
                return _ProcessList;
            }
        }
        private string startupPath = @"C:\Users\UserName\AppData\Roaming\Microsoft\Windows\Start Menu\Programs\Startup";
        private string GetProgramPath(string key)
        {
            string[] files = Directory.GetFiles(startupPath);
            foreach(string filePath in files)
            {
                if (filePath.IndexOf(key, StringComparison.OrdinalIgnoreCase) >= 0)
                    return filePath;
            }
            return "";
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
            using (StreamWriter sw = new StreamWriter(logPath))
            {
                string dateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                sw.WriteLine(dateTime + "  " + content);
            }
        }
    }
}
