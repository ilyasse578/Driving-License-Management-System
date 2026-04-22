using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace DVLD_Version_3_DataAccess
{

    public class clsLogger
    {

        public static void Log(string log, EventLogEntryType level = EventLogEntryType.Error)
        {
            string sourceName = "DVLD_App";


            // Create the event source if it does not exist
            if (!EventLog.SourceExists(sourceName))
            {
                EventLog.CreateEventSource(sourceName, "Application");

            }

            // Log an information event
            EventLog.WriteEntry(sourceName, log, level);
        }
        //private static string sourceName = "DVLD_Application";

        //static clsLogger()
        //{
        //    if (!EventLog.SourceExists(sourceName))
        //    {
        //        EventLog.CreateEventSource(sourceName, "Application");
        //    }
        //}


        ///// <summary>
        ///// Logs an exception to the Windows Event Viewer with the specified entry type.
        ///// The default entry type is Error if not specified.
        ///// </summary>
        ///// <param name="ex">The exception to log.</param>
        ///// <param name="entryType">The type of entry to log (e.g., Error, Warning, Information). Default is Error.</param>
        //public static void Log(Exception ex, EventLogEntryType entryType = EventLogEntryType.Error)
        //{
        //    EventLog.WriteEntry(sourceName, FormatErrorMessage(ex), entryType);
        //}

        //public static void Logstringerrors(string error, EventLogEntryType entryType = EventLogEntryType.Error)
        //{
        //    EventLog.WriteEntry(sourceName, error, entryType);
        //}



        //private static string FormatErrorMessage(Exception ex)
        //{

        //    // this is an example
        //    /*
        //        --- Exception Log ---
        //        Timestamp: 10/31/2024 2:45:32 PM
        //        Message: File not found.
        //        Inner Exception: File path was null.
        //        Stack Trace: at MyApp.Program.Main() in C:\Project\MyApp\Program.cs:line 24
        //        Source: MyApp
        //        -----------------------
        //     */

        //    string message =

        //         $"--- Exception Log ---\n" +
        //         $"Timestamp: {DateTime.Now}\n" +
        //         $"Message: {ex.Message}\n" +
        //         $"Inner Exception: {(ex.InnerException != null ? ex.InnerException.Message : "N/A")}\n" +
        //         $"Stack Trace: {ex.StackTrace}\n" +
        //         $"Source: {ex.Source}\n" +
        //         $"-----------------------";

        //    return message;
        //}


    }
}
