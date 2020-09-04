using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;

namespace AutoTempCleaner
{
    class Program
    {
        static void Main(string[] args)
        {
            var exitCode = HostFactory.Run(x =>
            {
                x.Service<TempCleaner>(c =>
                {
                    c.ConstructUsing(cleaner => new TempCleaner());
                    c.WhenStarted(cleaner => cleaner.Start());
                    c.WhenStopped(cleaner => cleaner.Stop());
                });

                x.RunAsLocalSystem();
                x.SetServiceName("AutoTempCleaner");
                x.SetDisplayName("Auto Temp Cleaner");
                x.SetDescription("Service app that automatically cleans the temp folder");
            });

            int exitCodeValue = (int)Convert.ChangeType(exitCode, exitCode.GetTypeCode());
            Environment.ExitCode = exitCodeValue;

        }
    }
}
