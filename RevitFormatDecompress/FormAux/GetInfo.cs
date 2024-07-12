using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitFormatDecompress
{
    internal class GetInfo
    {
        public static string TempFile()
        {
            // Get the Windows Temp directory
            string tempPath = Path.GetTempPath();

            // Define your custom folder and file inside the Temp directory
            string myFolder = "ReverseFolder";

            // Combine to create full directory and file paths
            string fullFolderPath = Path.Combine(tempPath, myFolder);

            string result = Path.Combine(fullFolderPath, "config.Json");

            return result;
        }
    }
}
