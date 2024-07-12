using OpenMcdf;
using System.IO;
using System.Collections.Generic;

namespace EditRevitFile
{
    public static class Compress
    {
        public static void RevitFile(string filesLocation, string fileSaveLoc)
        {
            RvtFile rvtFile = setRvtFileData(filesLocation);

            CompoundFile cf = new CompoundFile();
            CFStream basicInfoFile = cf.RootStorage.AddStream("BasicFileInfo");
            basicInfoFile.SetData(rvtFile.basicFileInfo);
            CFStream contents = cf.RootStorage.AddStream("Contents");
            contents.SetData(rvtFile.contents);
            CFStream projectInformation = cf.RootStorage.AddStream("ProjectInformation");
            projectInformation.SetData(rvtFile.projectInformation);
            CFStream revitPreview = cf.RootStorage.AddStream("RevitPreview4.0");
            revitPreview.SetData(rvtFile.revitPreview40);
            CFStream transmissionData = cf.RootStorage.AddStream("TransmissionData");
            transmissionData.SetData(rvtFile.TransmissionData);

            CFStorage formats = cf.RootStorage.AddStorage("Formats");
            CFStream formatsLatest = formats.AddStream("Latest");
            formatsLatest.SetData(rvtFile.formats.latest);

            CFStorage global = cf.RootStorage.AddStorage("Global");
            CFStream contentDocuments = global.AddStream("ContentDocuments");
            contentDocuments.SetData(rvtFile.global.contentDocuments);
            CFStream documentIncrementTable = global.AddStream("DocumentIncrementTable");
            documentIncrementTable.SetData(rvtFile.global.documentIncrementTable);
            CFStream elemTable = global.AddStream("ElemTable");
            elemTable.SetData(rvtFile.global.elemTable);
            CFStream history = global.AddStream("History");
            history.SetData(rvtFile.global.history);
            CFStream globalLatest = global.AddStream("Latest");
            globalLatest.SetData(rvtFile.global.latest);
            CFStream partitionTable = global.AddStream("PartitionTable");
            partitionTable.SetData(rvtFile.global.partitionTable);

            CFStorage partitions = cf.RootStorage.AddStorage("Partitions");
            foreach (var partition in rvtFile.partitions)
            {
                CFStream partitionStream = partitions.AddStream(partition.Key);
                partitionStream.SetData(partition.Value);
            }

            cf.Save(fileSaveLoc);
            cf.Close();
        }

        public static RvtFile setRvtFileData(string path)
        {
            RvtFile result = new RvtFile();
            result.basicFileInfo = File.ReadAllBytes(Path.Combine(path, "BasicFileInfo"));
            result.contents = File.ReadAllBytes(Path.Combine(path, "Contents"));
            result.projectInformation = File.ReadAllBytes(Path.Combine(path, "ProjectInformation"));
            result.revitPreview40 = File.ReadAllBytes(Path.Combine(path, "RevitPreview4.0"));
            result.TransmissionData = File.ReadAllBytes(Path.Combine(path, "TransmissionData"));

            Formats formats = new Formats();
            formats.latest = File.ReadAllBytes(Path.Combine(path, "Formats", "Latest"));
            result.formats = formats;

            Global global = new Global();
            global.contentDocuments = File.ReadAllBytes(Path.Combine(path, "Global", "ContentDocuments"));
            global.documentIncrementTable = File.ReadAllBytes(Path.Combine(path, "Global", "DocumentIncrementTable"));
            global.elemTable = File.ReadAllBytes(Path.Combine(path, "Global", "ElemTable"));
            global.history = File.ReadAllBytes(Path.Combine(path, "Global", "History"));
            global.latest = File.ReadAllBytes(Path.Combine(path, "Global", "Latest"));
            global.partitionTable = File.ReadAllBytes(Path.Combine(path, "Global", "PartitionTable"));
            result.global = global;

            Dictionary<string, byte[]> partitions = new Dictionary<string, byte[]>();
            string partitionsPath = Path.Combine(path, "Partitions");
            foreach (var filePath in Directory.GetFiles(partitionsPath))
            {
                string fileName = Path.GetFileName(filePath);
                partitions[fileName] = File.ReadAllBytes(filePath);
            }
            result.partitions = partitions;

            return result;
        }
    }
}
