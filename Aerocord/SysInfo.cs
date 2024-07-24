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
                RegistryKey localKey =
                    RegistryKey.OpenBaseKey(Hive,
                        Environment.Is64BitOperatingSystem ? RegistryView.Registry64 : RegistryView.Registry32);
                localKey = localKey.OpenSubKey(Path.Substring(Path.IndexOf('\\') + 1));
                if (localKey != null)
                {
                    value = localKey.GetValue(ValueName).ToString();
                    localKey.Close();
                }
                return value;
        }
        public static string GetVersionString()
        {
            return GetRegistryValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion", "LCUVer", "6.1.7601.17514");
        }
    }
}