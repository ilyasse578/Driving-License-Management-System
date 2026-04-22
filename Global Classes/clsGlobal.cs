using DVLD_Version_3_Business;
using DVLD_Version_3_DataAccess;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_Version_3.Global_Classes
{

    internal static class clsGlobal
    {
        public static clsUser CurrentUser;

        public static bool RememberUserNameAndPasswordInRegistry(string userName, string password)
        {
            string keyPath = @"HKEY_CURRENT_USER\SOFTWARE\MyDataLogin";

            //or 


            string valueName = "LoginUserCurrent";
            string valueData = userName + "#//#" + password;

            try
            {
                Registry.SetValue(keyPath, valueName, valueData, RegistryValueKind.String);
                return true;
            }
            catch (Exception ex)
            {
               //clsLogger.Log(ex);
                return false;
            }

        }
        public static bool GetSortedCredentialFromRegistry(ref string userName, ref string password)
        {
            string keyPath = @"HKEY_CURRENT_USER\SOFTWARE\MyDataLogin";

            string valueName = "LoginUserCurrent";

            try
            {
                string value = Registry.GetValue(keyPath, valueName, null) as string;
                if (value != null)
                {
                    string[] valueData = value.Split(new string[] { "#//#" }, StringSplitOptions.None);
                    if (valueData.Length == 2)
                    {
                        userName = valueData[0];
                        password = valueData[1];
                        return true;
                    }

                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"an error occared {ex.Message}");
               // clsLogger.Log(ex, System.Diagnostics.EventLogEntryType);
            }
            return false;
        }

    }

 }
