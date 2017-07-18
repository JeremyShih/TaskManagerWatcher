using System;

namespace TaskManagerWatcher
{
    static class Program
    {
        static void Main(string[] args)
        {
            double waitMinute = 5;
            Ctrl ctrl = new Ctrl();
            Model model = new Model();
            try
            {
                do
                {
                    Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    foreach (CheckProc proc in model.ProcessList)
                    {
                        Console.WriteLine("Process Name = " + proc.Key);
                        bool status = ctrl.IsProcessOpen(proc.Key);
                        Console.WriteLine("Process is running = " + status);

                        if (status)
                            continue;
                        else
                        {
                            if (!System.IO.File.Exists(proc.ProcPath))
                            {
                                Console.WriteLine("Program file does not exist at the path: " + proc.ProcPath);
                                continue;
                            }
                            else
                            {
                                WatcherLog log = new WatcherLog();
                                log.WriteLog("Starting Process...");
                                Console.WriteLine("Starting Process...");
                                ctrl.StartProcess(proc.ProcPath);

                                status = ctrl.IsProcessOpen(proc.Key);
                                string result = status ? "Start Process Succeed" : "Start Process Failed";
                                log.WriteLog(result);
                                Console.WriteLine(result);
                            }
                        }

                    }
                    Console.WriteLine();
                    System.Threading.Thread.Sleep((int)(waitMinute * 60 * 1000));
                } while (model.ProcessList.Count > 0);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.ToString());
                WatcherLog log = new WatcherLog();
                log.WriteLog(ex.Message);
                log.WriteLog(ex.ToString());
                Console.ReadLine();
            }
        }
    }
}
