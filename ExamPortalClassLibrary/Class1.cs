using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Security;

namespace ExamPortalClassLibrary
{
    public class Class1
    {
        //declare the Netapi32 : NetServerEnum method import
        [DllImport("Netapi32", CharSet = CharSet.Auto,
        SetLastError = true),
        SuppressUnmanagedCodeSecurityAttribute]

        // The NetServerEnum API function lists all servers of the 
        // specified type that are visible in a domain.
        public static extern int NetServerEnum(
            string ServerNane, // must be null
            int dwLevel,
            ref IntPtr pBuf,
            int dwPrefMaxLen,
            out int dwEntriesRead,
            out int dwTotalEntries,
            int dwServerType,
            string domain, // null for login domain
            out int dwResumeHandle
            );

        //declare the Netapi32 : NetApiBufferFree method import
        [DllImport("Netapi32", SetLastError = true),
        SuppressUnmanagedCodeSecurityAttribute]

        // Netapi32.dll : The NetApiBufferFree function frees 
        // the memory that the NetApiBufferAllocate function allocates.         
        public static extern int NetApiBufferFree(
            IntPtr pBuf);

        //create a _SERVER_INFO_100 STRUCTURE
        [StructLayout(LayoutKind.Sequential)]
        public struct _SERVER_INFO_100
        {
            internal int sv100_platform_id;
            [MarshalAs(UnmanagedType.LPWStr)]
            internal string sv100_name;
        }

        public static void Main(string[] args)
        {
            List<string> networkComputers = GetNetworkComputerNames();
            foreach (string computerName in networkComputers)
            {
                Console.WriteLine(computerName);
            }
            Console.ReadLine();
        }

        private static List<string> GetNetworkComputerNames()
        {
            List<string> networkComputerNames = new List<string>();
            const int MAX_PREFERRED_LENGTH = -1;
            int SV_TYPE_LOCAL_LIST_ONLY = 0x40000000;
            IntPtr buffer = IntPtr.Zero;
            IntPtr tmpBuffer = IntPtr.Zero;
            int entriesRead = 0;
            int totalEntries = 0;
            int resHandle = 0;
            int sizeofINFO = Marshal.SizeOf(typeof(_SERVER_INFO_100));

            try
            {
                int ret = NetServerEnum(null, 100, ref buffer,
                    MAX_PREFERRED_LENGTH,
                    out entriesRead,
                    out totalEntries, SV_TYPE_LOCAL_LIST_ONLY, null, out
                    resHandle);
                //if the returned with a NERR_Success 
                //(C++ term), =0 for C#
                if (ret != 0)
                {
                    //loop through all SV_TYPE_WORKSTATION and SV_TYPE_SERVER PC's
                    for (int i = 0; i < totalEntries; i++)
                    {
                        tmpBuffer = new IntPtr((int)buffer +
                                    (i * sizeofINFO));

                        //Have now got a pointer to the list of SV_TYPE_WORKSTATION and SV_TYPE_SERVER PC's
                        _SERVER_INFO_100 svrInfo = (_SERVER_INFO_100)
                            Marshal.PtrToStructure(tmpBuffer,
                                    typeof(_SERVER_INFO_100));

                        //add the Computer name to the List
                        networkComputerNames.Add(svrInfo.sv100_name);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                //The NetApiBufferFree function frees the allocated memory
                NetApiBufferFree(buffer);
            }

            return networkComputerNames;
        }
        public Class1() { }
    }
}
