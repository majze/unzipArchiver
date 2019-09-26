using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backpostZippy
{
    class Program
    {
        static void Main(string[] args)
        {
            var dirList = Directory.GetDirectories(args[0], "*");
            Parallel.ForEach(dirList, months => { Extractor(months); });
            Console.WriteLine("** Job Complete **");
        }
        public static void Extractor(string dir)
        {
            // get starting dir for month
            var monthPath = new DirectoryInfo(dir);
            Console.WriteLine("Starting " + monthPath);
            // full path to 7zip exe
            var zipExePath = @"C:\Program Files\7-Zip\7z.exe";

            var zipList = monthPath.GetFiles("*.7z");

            // make directories for archives, PDF, XML
            var dirCD = Directory.CreateDirectory(monthPath.FullName + @"\CD");
            var dirPDF = Directory.CreateDirectory(monthPath.FullName + @"\PDF");
            var dirXML = Directory.CreateDirectory(monthPath.FullName + @"\XML");

            //unzip time
            Parallel.ForEach(zipList, zipArchive =>
            {
                // get XMLS
                ProcessStartInfo p = new ProcessStartInfo();
                p.FileName = zipExePath;
                p.Arguments = "e \"" + zipArchive.FullName + "\" -o\"" + dirXML.FullName + "\" *.xml -aoa";
                p.WindowStyle = ProcessWindowStyle.Hidden;
                Process x = Process.Start(p);
                x.WaitForExit();

                // get PDFs
                p = new ProcessStartInfo();
                p.FileName = zipExePath;
                p.Arguments = "e \"" + zipArchive.FullName + "\" -o\"" + dirPDF.FullName + "\" *.pdf -aoa";
                p.WindowStyle = ProcessWindowStyle.Hidden;
                x = Process.Start(p);
                x.WaitForExit();

                // move archives to /CD
                zipArchive.MoveTo(dirCD.FullName + @"\" + zipArchive.Name);
                Console.WriteLine("\t" + zipArchive.Name + " finished");
            });
            Console.WriteLine("Finished " + monthPath);
        }
    }
}


