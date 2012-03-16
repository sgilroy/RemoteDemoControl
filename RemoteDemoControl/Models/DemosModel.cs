using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace RemoteDemoControl.Models
{
    public class DemosModel
    {
        static string[] validDemoExtensions = { ".exe", ".bat", ".ahk" };

        public IEnumerable<string> DemoFileNames 
        {
            get
            {
                //foreach(System.IO.FileInfo fileInfo in 
                // Specify the directories you want to manipulate.
                //string path = @"C:\Users\ml\Documents\AutoBodyHot\Demos";
                string path = @"\\interplay\C$\Users\ml\Documents\AutoBodyHot\Demos";
                DirectoryInfo directory = new DirectoryInfo(path);

                try 
                {
                    // Determine whether the directory exists.
                    if (!directory.Exists) 
                    {
                    }
                    else
                    {
                        foreach (FileInfo fileInfo in directory.EnumerateFiles())
                        {
                            if (validDemoExtensions.Contains(fileInfo.Extension.ToLower()))
                                yield return fileInfo.Name;
                        }
                    }
                } 
                //catch (Exception e) 
                //{
                //    Console.WriteLine("The process failed: {0}", e.ToString());
                //} 
                finally {}
            }
        }
    }
}