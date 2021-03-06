﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO.Pipes;
using System.IO;
using System.Diagnostics;
using RemoteDemoControlLib;
using System.Security.AccessControl;
using System.Security.Principal;

namespace DemoLauncher
{
    class DemoLaunchingServer
    {
        private static int numThreads = 4;

        public static void Main()
        {
            int i;
            Thread[] servers = new Thread[numThreads];

            Console.WriteLine("\n*** Named pipe server stream with impersonation example ***\n");
            Console.WriteLine("Waiting for client connect...\n");
            for (i = 0; i < numThreads; i++)
            {
                servers[i] = new Thread(ServerThread);
                servers[i].Start();
            }
            Thread.Sleep(250);
            while (i > 0)
            {
                for (int j = 0; j < numThreads; j++)
                {
                    if (servers[j] != null)
                    {
                        if (servers[j].Join(250))
                        {
                            Console.WriteLine("Server thread[{0}] finished.", servers[j].ManagedThreadId);
                            // TODO: should we really keep destroying and creating threads? This probably isn't the most efficient way to do this, but it was the simplest way to adapt the example
                            servers[j] = null;
                            servers[j] = new Thread(ServerThread);
                            servers[j].Start();
                            //i--;    // decrement the thread watch count
                        }
                    }
                }
            }
            Console.WriteLine("\nServer threads exhausted, exiting.");
        }

        private static void ServerThread(object data)
        {
            PipeSecurity ps = new PipeSecurity();
            ps.AddAccessRule(new PipeAccessRule("IIS_IUSRS", PipeAccessRights.ReadWrite, AccessControlType.Allow));
            ps.AddAccessRule(new PipeAccessRule(WindowsIdentity.GetCurrent().Owner, PipeAccessRights.FullControl, AccessControlType.Allow));
            using (NamedPipeServerStream pipeServer =
                new NamedPipeServerStream(RemoteDemoControlPipe.PIPE_NAME, PipeDirection.InOut, numThreads, PipeTransmissionMode.Message, PipeOptions.WriteThrough, 1024, 1024, ps))
            {

                int threadId = Thread.CurrentThread.ManagedThreadId;

                Console.WriteLine("Created thread[{0}].", threadId);

                // Wait for a client to connect
                pipeServer.WaitForConnection();

                Console.WriteLine("Client connected on thread[{0}].", threadId);
                try
                {
                    // Read the request from the client. Once the client has
                    // written to the pipe its security token will be available.

                    StreamString ss = new StreamString(pipeServer);

                    // Verify our identity to the connected client using a
                    // string that the client anticipates.

                    ss.WriteString(RemoteDemoControlPipe.PIPE_SIGNATURE);
                    string filename = ss.ReadString();

                    if (WorkstationLockedUtil.IsWorkstationLocked())
                    {
                        ss.WriteString("Workstation is locked. Scripts can not be run.");
                    }
                    else
                    {
                        // Display the name of the user we are impersonating.
                        Console.WriteLine("Launching script: {0} on thread[{1}] as user: {2}.",
                            filename, threadId, pipeServer.GetImpersonationUserName());

                        // Launch the specified script
                        System.Diagnostics.Process proc = new System.Diagnostics.Process();

                        proc.StartInfo.UseShellExecute = false;

                        if (File.Exists(filename))
                        {
                            Process.Start(filename);
                            ss.WriteString("Process started: " + filename);
                        }
                        else
                        {
                            ss.WriteString("File does not exist: " + filename);
                        }
                    }
                }
                // Catch the IOException that is raised if the pipe is broken
                // or disconnected.
                catch (IOException e)
                {
                    Console.WriteLine("ERROR: {0}", e.Message);
                }
                pipeServer.Close();
            }
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
            int len = 0;

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
