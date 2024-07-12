using Newtonsoft.Json;
using System.IO;

namespace EditRevitFile
{
    public static class WritteInfo
    {
        public static void Run(string fileLocation, string output)
        {

            var myObject = new InfoConfigUser
            {
                Input = fileLocation,
                Output = output,
            };

            string fullFilePath = GetInfo.TempFile();

            // Serialize the object to JSON
            string jsonString = JsonConvert.SerializeObject(myObject);

            // Save the JSON string to a file
            File.WriteAllText(fullFilePath, jsonString);
        }
    }
}
