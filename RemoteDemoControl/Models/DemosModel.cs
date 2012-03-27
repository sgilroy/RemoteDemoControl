using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Web.Mvc;

namespace RemoteDemoControl.Models
{
    public class DemosModel
    {
        static string[] validDemoExtensions = { ".exe", ".bat", ".ahk" };
        private HttpServerUtilityBase Server;
        //public const string DEMOS_DIRECTORY_PATH = @"\\interplay\C$\Users\ml\Documents\AutoBodyHot\Demos";
        public const string DEMOS_DIRECTORY_PATH = @"C:\Users\ml\Documents\AutoBodyHot\Demos";

        public DemosModel(HttpServerUtilityBase Server)
        {
            // TODO: Complete member initialization
            this.Server = Server;
        }

        public IEnumerable<InterPlayDemo> Demos 
        {
            get
            {
                string path = DEMOS_DIRECTORY_PATH;
                DirectoryInfo directory = new DirectoryInfo(path);

                // Determine whether the directory exists.
                if (!directory.Exists) 
                {
                }
                else
                {
                    XmlReaderSettings settings = new XmlReaderSettings();
                    settings.ValidationType = ValidationType.Schema;
                    settings.Schemas.Add("http://interplay.media.mit.edu/schemas/InterPlayDemo.xsd",  Server.MapPath("~/Content/schemas/InterPlayDemo.xsd"));
                    XmlSerializer xmlSerializer = new XmlSerializer(typeof(InterPlayDemo));

                    foreach (FileInfo launchScriptFileInfo in directory.EnumerateFiles())
                    {
                        if (validDemoExtensions.Contains(launchScriptFileInfo.Extension.ToLower()))
                        {
                            InterPlayDemo demo = null;
                            FileInfo demoFileInfo = new FileInfo(launchScriptFileInfo.FullName + ".xml");
                            if (demoFileInfo.Exists)
                            {
                                //using (XmlReader reader = XmlReader.Create(demoFileInfo.FullName, settings))
                                using (XmlReader reader = XmlReader.Create(demoFileInfo.FullName))
                                {
                                    try
                                    {
                                        demo = (InterPlayDemo)xmlSerializer.Deserialize(reader);
                                    }
                                    catch (InvalidOperationException e)
                                    {
                                        System.Console.WriteLine(e.ToString());
                                    }
                                }
                            }

                            if (demo == null)
                            {
                                demo = new InterPlayDemo();
                            }
                            demo.LaunchScriptFile = launchScriptFileInfo.Name;
                            if (String.IsNullOrEmpty(demo.Title))
                            {
                                demo.Title = launchScriptFileInfo.Name.Substring(0, launchScriptFileInfo.Name.Length - launchScriptFileInfo.Extension.Length);
                            }
                            if (String.IsNullOrEmpty(demo.ThumbnailFile))
                            {
                                demo.ThumbnailFile = launchScriptFileInfo.Name + ".thumbnail.png";
                            }

                            if (String.IsNullOrEmpty(demo.ThumbnailUrl))
                            {
                                FileInfo thumbnailFileInfo = new FileInfo(DEMOS_DIRECTORY_PATH + "\\" + demo.ThumbnailFile);
                                if (thumbnailFileInfo.Exists)
                                {
                                    demo.ThumbnailUrl = "Demos/ThumbnailImage?image=" + thumbnailFileInfo.Name;
                                }
                                else
                                {
                                    demo.ThumbnailUrl = "../../Content/images/default_demo_thumbnail.png";
                                }
                            }

                            yield return demo;
                        }
                    }
                }
            }
        }
    }
}