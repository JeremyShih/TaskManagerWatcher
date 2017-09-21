using System;
using System.Diagnostics;

namespace TaskManagerWatcher
{
    public class Ctrl
    {
        public bool IsProcessOpen(string name)
        {
            foreach (Process clsProcess in Process.GetProcesses())
            {
                int start = clsProcess.ProcessName.IndexOf(name, StringComparison.OrdinalIgnoreCase);
                int len = clsProcess.ProcessName.Length;
                int nameLength = name.Length;
                if (start >= 0 && start + nameLength == len)
                {
                    return true;
                }
            }
            return false;
        }
        public void StartProcess(string filePath)
        {
            Process open = new Process();
            open.StartInfo.FileName = filePath;
            open.Start();
        }
    }
}
