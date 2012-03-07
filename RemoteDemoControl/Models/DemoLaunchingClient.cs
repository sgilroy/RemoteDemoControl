using System;
using System.IO;
using System.IO.Pipes;
using System.Text;
using System.Security.Principal;
using System.Diagnostics;
using System.Threading;

namespace RemoteDemoControl.Models
{
    public class DemoLaunchingClient
    {
        public string StartDemo()
        {
            return LaunchRemoteProcess("start.ahk");
        }

        public string LaunchRemoteProcess(string processFileName)
        {
            NamedPipeClientStream pipeClient =
                new NamedPipeClientStream(".", "testpipe",
                    PipeDirection.InOut, PipeOptions.None,
                    TokenImpersonationLevel.Impersonation);

            string message = null;
            // Connecting to server...
            try
            {
                pipeClient.Connect(100);
            }
            catch (TimeoutException)
            {
                return "Failed to connect to demo launcher. Please make sure DemoLauncher is running on this machine.";
            }

            StreamString ss = new StreamString(pipeClient);
            // Validate the server's signature string
            if (ss.ReadString() == "I am the one true server!")
            {
                // The client security token is sent with the first write.
                // Send the name of the file whose contents are returned
                // by the server.
                ss.WriteString(processFileName);

                message = ss.ReadString();
            }
            else
            {
                message = "Demo launcher could not be verified. Please make sure the correct version of DemoLauncher is running on this machine.";
            }
            pipeClient.Close();
            return message;
        }

        public string StopDemo()
        {
            return LaunchRemoteProcess("stop.ahk");
        }
    }

    // Defines the data protocol for reading and writing strings on our stream
    public class StreamString
    {
        private Stream ioStream;
        private UnicodeEncoding streamEncoding;

        public StreamString(Stream ioStream)
        {
            this.ioStream = ioStream;
            streamEncoding = new UnicodeEncoding();
        }

        public string ReadString()
        {
            int len;
            len = ioStream.ReadByte() * 256;
            len += ioStream.ReadByte();
            byte[] inBuffer = new byte[len];
            ioStream.Read(inBuffer, 0, len);

            return streamEncoding.GetString(inBuffer);
        }

        public int WriteString(string outString)
        {
            byte[] outBuffer = streamEncoding.GetBytes(outString);
            int len = outBuffer.Length;
            if (len > UInt16.MaxValue)
            {
                len = (int)UInt16.MaxValue;
            }
            ioStream.WriteByte((byte)(len / 256));
            ioStream.WriteByte((byte)(len & 255));
            ioStream.Write(outBuffer, 0, len);
            ioStream.Flush();

            return outBuffer.Length + 2;
        }
    }
}