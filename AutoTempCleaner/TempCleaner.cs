using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace AutoTempCleaner
{
    public class TempCleaner
    {
        private readonly Timer timer;
        private readonly string tempPath = ConfigurationManager.AppSettings.Get("TempFolderPath");

        public TempCleaner()
        {
            timer = new Timer(1000) { AutoReset = true };
            timer.Elapsed += TimerElapsed;
        }

        private void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            File.SetAttributes(tempPath, FileAttributes.Normal);

            string[] directories = Directory.GetDirectories(tempPath);

            foreach (var dir in directories)
            {
                var subDir = Directory.GetFiles(dir);

                if (subDir.Length > 0)
                {
                    DeleteDirFiles(dir);
                }
                else
                {
                    Directory.Delete(dir);
                }
            }

            string[] files = Directory.GetFiles(tempPath);

            foreach (var file in files)
            {
                File.SetAttributes(file, FileAttributes.Normal);
                File.Delete(file);
            }

        }

        private void DeleteDirFiles(string dirPath)
        {
            File.SetAttributes(dirPath, FileAttributes.Normal);

            string[] files = Directory.GetFiles(dirPath);

            foreach (var file in files)
            {
                File.SetAttributes(file, FileAttributes.Normal);
                File.Delete(file);
            }
        }

        public void Start()
        {
            timer.Start();
        }

        public void Stop()
        {
            timer.Stop();
        }
    }
}
