﻿using System;
using Microsoft.Win32;

namespace Aerocord
{
    class SysInfo
    {
        public static string GetRegistryValue(string Path, string ValueName, string Default)
        {
            string value = Default;
            RegistryHive Hive;

            switch (Path.Split('\\')[0])
            {
                case "HKEY_CLASSES_ROOT":
                    Hive = RegistryHive.ClassesRoot;
                    break;
                case "HKEY_CURRENT_USER":
                    Hive = RegistryHive.CurrentUser;
                    break;
                case "HKEY_LOCAL_MACHINE":
                    Hive = RegistryHive.LocalMachine;
                    break;
                case "HKEY_USERS":
                    Hive = RegistryHive.Users;
                    break;
                case "HKEY_CURRENT_CONFIG":
                    Hive = RegistryHive.CurrentConfig;
                    break;
                default:
                    Hive = RegistryHive.LocalMachine;
                    break;
            }

            try
            {
                using (RegistryKey baseKey = RegistryKey.OpenBaseKey(Hive, Environment.Is64BitOperatingSystem ? RegistryView.Registry64 : RegistryView.Registry32))
                {
                    using (RegistryKey localKey = baseKey.OpenSubKey(Path.Substring(Path.IndexOf('\\') + 1)))
                    {
                        if (localKey != null)
                        {
                            object regValue = localKey.GetValue(ValueName);
                            if (regValue != null)
                            {
                                value = regValue.ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
            }

            return value;
        }

        public static string GetVersionString()
        {
            string LCUVer = GetRegistryValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion", "LCUVer", "None");
            if (LCUVer != "None") return LCUVer;
            string MajorMinorVersion = GetRegistryValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion", "ProductName", "Windows 7");
            if (MajorMinorVersion.Contains("Windows 10") || MajorMinorVersion.Contains("Windows 11")) MajorMinorVersion = "10.0"; else MajorMinorVersion = "6.1";
            string CurrentBuild = GetRegistryValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion", "CurrentBuild", "7601");
            return MajorMinorVersion + "." + CurrentBuild;
        }
    }
}
