

using System;
using System.Diagnostics;
using System.IO;
using Microsoft.Win32;
using static System.IO.FileInfo;
using System.Runtime.Versioning;
using System.Diagnostics.Eventing.Reader;


namespace EventLogs
{
    [SupportedOSPlatform("windows")] //supressing cross compatibility visual studio errors.
    class Program
    {
        
        static void Main(string[] args)
        {

            EventLogQuery query = new EventLogQuery("Microsoft-Windows-PowerShell/Operational", PathType.LogName, " *"); //example "Applications and Service Logs", filtered by logname instead of file name.
            EventLogReader reader = new EventLogReader(query);
            EventRecord eventRecord;
            while ((eventRecord = reader.ReadEvent()) != null)
            {
                //filter by record id 4104, powershell https://docs.microsoft.com/en-us/powershell/module/microsoft.powershell.core/about/about_logging_windows?view=powershell-7.2&viewFallbackFrom=powershell-7.1#viewing-the-powershell-event-log-entries-on-windows
                if (eventRecord.Id== 4104){
                    //filter EventID, Desc and First 100 chars of Description fields.
                    Console.WriteLine(String.Format("Date: {0} EventID: {1}\nDescription: \n\n {2} \n", eventRecord.TimeCreated, eventRecord.Id, eventRecord.FormatDescription().Substring(0,100)));
                System.Threading.Thread.Sleep(1000); //sleep for 1 second before printing next.
            }
            }

        }
       
    }
}
