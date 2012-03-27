using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RemoteDemoControl.Models;
using System.IO;
using System.Security.Principal;

namespace RemoteDemoControl.Controllers
{
    public class DemosController : Controller
    {
        //
        // GET: /Demos/

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string demoScriptFileName)
        {
            string demoName = demoScriptFileName;
            //ViewBag.Message = "Starting " + demoName + " demo...";
            DemoLaunchingClient client = new DemoLaunchingClient();
            ViewBag.Message = client.LaunchRemoteProcess("Demos\\" + demoName);
            return View();
        }

        public ActionResult ThumbnailImage(String image)
        {
            DirectoryInfo directory = new DirectoryInfo(DemosModel.DEMOS_DIRECTORY_PATH);
            //FileSystemInfo[] files = directory.GetFileSystemInfos(image);
            //if (files.Length == 1)
            //{
            //    FileSystemInfo imageFileInfo = files[0];
            FileInfo imageFileInfo = new FileInfo(DemosModel.DEMOS_DIRECTORY_PATH + "\\" + image);
            if (imageFileInfo.Exists)
            {
                return new FilePathResult(imageFileInfo.FullName, "image/" + imageFileInfo.Extension.Trim('.'));
            }

            throw new FileNotFoundException();
        }

        private static void TestIdentity()
        {
            // Retrieve the Windows account token for the current user.
            IntPtr logonToken = LogonUser();

            // Property implementations.
            UseProperties(logonToken);

            WindowsIdentity windowsIdentity = new WindowsIdentity(logonToken);
            string propertyDescription = "The Windows identity named ";

            // Retrieve the Windows logon name from the Windows identity object.
            propertyDescription += windowsIdentity.Name;

            // Verify that the user account is not considered to be an Anonymous
            // account by the system.
            if (!windowsIdentity.IsAnonymous)
            {
                propertyDescription += " is not an Anonymous account";
            }

            // Verify that the user account has been authenticated by Windows.
            if (windowsIdentity.IsAuthenticated)
            {
                propertyDescription += ", is authenticated";
            }

            // Verify that the user account is considered to be a System account
            // by the system.
            if (windowsIdentity.IsSystem)
            {
                propertyDescription += ", is a System account";
            }
            // Verify that the user account is considered to be a Guest account
            // by the system.
            if (windowsIdentity.IsGuest)
            {
                propertyDescription += ", is a Guest account";
            }

            // Retrieve the authentication type for the 
            String authenticationType = windowsIdentity.AuthenticationType;

            // Append the authenication type to the output message.
            if (authenticationType != null)
            {
                propertyDescription += (" and uses " + authenticationType);
                propertyDescription += (" authentication type.");
            }

            TokenImpersonationLevel token = windowsIdentity.ImpersonationLevel;
            propertyDescription += (" The impersonation level for the current user is : " + token.ToString());

            throw new Exception(propertyDescription);
        }

        // Retrieve the account token from the current WindowsIdentity object
        // instead of calling the unmanaged LogonUser method in the advapi32.dll.
        private static IntPtr LogonUser()
        {
            IntPtr accountToken = WindowsIdentity.GetCurrent().Token;
            Console.WriteLine("Token number is: " + accountToken.ToString());

            return accountToken;
        }

        // Access the properties of a WindowsIdentity object.
        private static void UseProperties(IntPtr logonToken)
        {
            WindowsIdentity windowsIdentity = new WindowsIdentity(logonToken);
            string propertyDescription = "The Windows identity named ";

            // Retrieve the Windows logon name from the Windows identity object.
            propertyDescription += windowsIdentity.Name;

            // Verify that the user account is not considered to be an Anonymous
            // account by the system.
            if (!windowsIdentity.IsAnonymous)
            {
                propertyDescription += " is not an Anonymous account";
            }

            // Verify that the user account has been authenticated by Windows.
            if (windowsIdentity.IsAuthenticated)
            {
                propertyDescription += ", is authenticated";
            }

            // Verify that the user account is considered to be a System account
            // by the system.
            if (windowsIdentity.IsSystem)
            {
                propertyDescription += ", is a System account";
            }
            // Verify that the user account is considered to be a Guest account
            // by the system.
            if (windowsIdentity.IsGuest)
            {
                propertyDescription += ", is a Guest account";
            }

            // Retrieve the authentication type for the 
            String authenticationType = windowsIdentity.AuthenticationType;

            // Append the authenication type to the output message.
            if (authenticationType != null)
            {
                propertyDescription += (" and uses " + authenticationType);
                propertyDescription += (" authentication type.");
            }

            Console.WriteLine(propertyDescription);

            // Display the SID for the owner.
            Console.Write("The SID for the owner is : ");
            SecurityIdentifier si = windowsIdentity.Owner;
            Console.WriteLine(si.ToString());
            // Display the SIDs for the groups the current user belongs to.
            Console.WriteLine("Display the SIDs for the groups the current user belongs to.");
            IdentityReferenceCollection irc = windowsIdentity.Groups;
            foreach (IdentityReference ir in irc)
                Console.WriteLine(ir.Value);
            TokenImpersonationLevel token = windowsIdentity.ImpersonationLevel;
            Console.WriteLine("The impersonation level for the current user is : " + token.ToString());
        }
    }
}
