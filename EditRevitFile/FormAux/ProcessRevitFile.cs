using OpenMcdf;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System;
using Newtonsoft.Json;
using EditRevitFile.Preview;

namespace EditRevitFile
{
    public static class ProcessRevitFile
    {
        public static void Run(string fileLocation, string outputFolder, string imageLoc)
        {
            String filename = Path.GetFileName(fileLocation);
            CompoundFile cf = new CompoundFile(fileLocation);

            List<CMDF> contentsList = new List<CMDF>();

            List<string> rootStreams = new List<string>
                { "Contents", "BasicFileInfo", "RevitPreview4.0", "TransmissionData", "ProjectInformation" };

            List<string> Globalstreams = new List<string>
                { "History", "ElemTable", "PartitionTable", "ContentDocuments", "DocumentIncrementTable" };

            int count = 0;

            Action<CFItem> storageAction = (item) =>
            {

                if (item.IsStream)
                {
                    CMDF cmdf = new CMDF();
                    cmdf.name = item.Name;

                    if (rootStreams.Contains(item.Name))
                    {
                        cmdf.child = false;
                    }
                    else
                    {
                        cmdf.child = true;

                        if (Globalstreams.Contains(item.Name))
                        {
                            cmdf.parentName = "Global";
                        }
                        else if (item.Name == "Latest")
                        {
                            if (count == 0)
                            {
                                cmdf.parentName = "Global";
                                count = 1;
                            }
                            else
                            {
                                cmdf.parentName = "Formats";
                            }
                        }
                        else
                        {
                            cmdf.parentName = "Partitions";
                        }
                    }

                    contentsList.Add(cmdf);
                    Console.WriteLine("Stream: " + item.Name);
                }

            };

            cf.RootStorage.VisitEntries(storageAction, true);
            string fileName = Path.GetFileName(fileLocation).Replace(".rvt", "");
            ProcessStreams(contentsList, cf, fileName, outputFolder);

            WritteInfo.Run(fileLocation, outputFolder);
            cf.Close();

            string pathLocationPreview = Path.Combine(outputFolder, fileName);
            PreviewEdit.Run(pathLocationPreview, imageLoc);

            Compress.RevitFile(pathLocationPreview, fileLocation);
            MessageBox.Show("Succesfully Edited");
        }

        private static void ProcessStreams(List<CMDF> cmdfFiles, CompoundFile cf, string fileName, string outputFolder)
        {
            string outputMainFolder = Path.Combine(outputFolder, fileName);
            if (!Directory.Exists(outputMainFolder))
            {
                Directory.CreateDirectory(outputMainFolder);
            }

            string outputUrl = null;
            foreach (CMDF cmdf in cmdfFiles)
            {
                byte[] temp = GetSingleStream(cmdf.parentName, cmdf.name, cf);

                string fileresult = cmdf.name;

                if (cmdf.child == false)
                {
                    outputUrl = Path.Combine(outputMainFolder, fileresult);
                }
                else
                {
                    string tempUrl = Path.Combine(outputMainFolder, cmdf.parentName);
                    if (!Directory.Exists(tempUrl))
                    {
                        Directory.CreateDirectory(tempUrl);
                    }
                    outputUrl = Path.Combine(tempUrl, fileresult);
                }

                if (cmdf.name == "BasicFileInfo" || cmdf.name == "TransmissionData")
                {
                    Encoding encoding = FileEncoding.Get(temp);
                    string basicInfoString = encoding.GetString(temp);
                    string basicInfoReader = basicInfoString.Replace("\0", string.Empty);
                    File.WriteAllText(outputUrl, basicInfoReader);
                }
                else
                {
                    File.WriteAllBytes(outputUrl, temp);
                }
            }
        }

        private static byte[] GetSingleStream(string storage, string stream, CompoundFile cf)
        {
            byte[] result = null;

            CFStream foundStream = null;
            if (storage != null)
            {
                try
                {
                    CFStorage foundStorage = cf.RootStorage.GetStorage(storage);
                    foundStream = foundStorage.GetStream(stream);

                }
                catch (Exception)
                {
                    MessageBox.Show("The property name is not valid");
                    return result;
                }

            }
            else
            {
                foundStream = cf.RootStorage.GetStream(stream);
            }

            result = foundStream.GetData();

            return result;
        }
    }
}
