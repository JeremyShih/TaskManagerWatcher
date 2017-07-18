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
                if (clsProcess.ProcessName.IndexOf(name, StringComparison.OrdinalIgnoreCase) >= 0)
                    return true;
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
