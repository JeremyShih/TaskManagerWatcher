﻿using System;

namespace TaskManagerWatcher
{
    static class Program
    {
        static void Main(string[] args)
        {
            AppDomain.CurrentDomain.ProcessExit += new EventHandler(CurrentDomain_ProcessExit);

            double waitMinute = 10;
            Ctrl ctrl = new Ctrl();
            Model model = new Model();
            Console.WriteLine("Startup Path = " + model.StartupDir);
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

                        if (!status)
                        {
                            if (!System.IO.File.Exists(proc.ProcPath))
                            {
                                Console.WriteLine("Program file does not exist at the path: " + proc.ProcPath);
                            }
                            else
                            {
                                WatcherLog log = new WatcherLog();
                                log.WriteLog("Process Name = " + proc.Key);
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
                } while (true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.ToString());
                WatcherLog log = new WatcherLog();
                log.WriteLog(ex.Message);
                log.WriteLog(ex.ToString());
                Console.ReadLine();
            }
        }
        static void CurrentDomain_ProcessExit(object sender, EventArgs e)
        {
            WatcherLog log = new WatcherLog();
            log.WriteLog("Process terminated!");
        }
    }
}
