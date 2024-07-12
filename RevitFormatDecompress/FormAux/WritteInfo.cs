using Newtonsoft.Json;
using System.IO;


namespace RevitFormatDecompress.FormAux
{
    public static class WritteInfo
    {
        public static void Run(string storageString, string fileString, string fileLocation,bool select, string output)
        {

            var myObject = new InfoConfigUser
            {
                Storage = storageString,
                File = fileString,
                Input = fileLocation,
                Output = output,
                ExportAll = select
            };

            string fullFilePath = GetInfo.TempFile();

            // Serialize the object to JSON
            string jsonString = JsonConvert.SerializeObject(myObject);

            // Save the JSON string to a file
            File.WriteAllText(fullFilePath, jsonString);
        }
    }
}
