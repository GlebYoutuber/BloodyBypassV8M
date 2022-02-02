using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BloodyBypass
{
    class Program
    {
        private static Random random = new Random();

        public static string RandomString(int length)
        {
            const string chars = "AaBbCcDdEeFfGgHhIiJjKkLlMmNnOoPpQqRrSsTtUuVvWwXxYyZz0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public static void Extract(string nameSpace, string outDirectory, string internalFilePath, string resourceName)
        {
            //DON'T TOUCH THIS CODE!!!

            Assembly assembly = Assembly.GetCallingAssembly();

            using (Stream s = assembly.GetManifestResourceStream(nameSpace + "." + (internalFilePath == "" ? "" : internalFilePath + ".") + resourceName))
            using (BinaryReader r = new BinaryReader(s))
            using (FileStream fs = new FileStream(outDirectory + "\\" + resourceName, FileMode.OpenOrCreate))
            using (BinaryWriter w = new BinaryWriter(fs))
                w.Write(r.ReadBytes((int)s.Length));
        }
        static void Main(string[] args)
        {
            Console.Title = RandomString(10);
            Extract("BloodyBypass", @"C:\Windows", "Resources", "USBDeview.exe");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Starting Bloody Bypass");
            Thread.Sleep(10000);
            Console.WriteLine("Disabling and Enabling Bloody Mouse");
            ProcessStartInfo nmbld = new ProcessStartInfo();
            nmbld.FileName = "cmd.exe";
            nmbld.Arguments = "/c USBDeview /disable_by_pid 09da;319d";
            nmbld.WindowStyle = ProcessWindowStyle.Hidden;
            Process.Start(nmbld);
            Thread.Sleep(5000);
            ProcessStartInfo xxx = new ProcessStartInfo();
            xxx.FileName = "cmd.exe";
            xxx.Arguments = "/c USBDeview /enable_by_pid 09da;319d";
            xxx.WindowStyle = ProcessWindowStyle.Hidden;
            Process.Start(xxx);
            Thread.Sleep(5000);
            Console.WriteLine("Successfully Bypassed EAC Kicked");
            Thread.Sleep(7000);
            Environment.Exit(-7000);
        }
    }
}
