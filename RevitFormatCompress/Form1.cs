using Microsoft.VisualBasic.ApplicationServices;
using OpenMcdf;
using SharedRevitInfo;
using System.IO;
using System.IO.Packaging;

namespace RevitFormatCompress
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            RvtFile rvtFile = setRvtFileData();


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
            CFStream cero = partitions.AddStream("1");
            cero.SetData(rvtFile.partitions.cero);

            cf.Save(@"C:\Users\User\Desktop\Test.rvt");
            cf.Close();   
        }

        public static RvtFile setRvtFileData()
        {
            string path = "C:\\Users\\User\\Desktop\\Reverse\\Results\\House";

            RvtFile result = new RvtFile();
            result.basicFileInfo = File.ReadAllBytes(path + "\\BasicFileInfo");
            result.contents = File.ReadAllBytes(path + "\\Contents");
            result.projectInformation = File.ReadAllBytes(path + "\\ProjectInformation");
            result.revitPreview40 = File.ReadAllBytes(path + "\\RevitPreview4.0");
            result.TransmissionData = File.ReadAllBytes(path + "\\TransmissionData");

            Formats formats = new Formats();
            formats.latest = File.ReadAllBytes(path + "\\Formats\\Latest");
            result.formats = formats;

            Global global = new Global();
            global.contentDocuments = File.ReadAllBytes(path + "\\Global\\ContentDocuments");
            global.documentIncrementTable = File.ReadAllBytes(path + "\\Global\\DocumentIncrementTable");
            global.elemTable = File.ReadAllBytes(path + "\\Global\\ElemTable");
            global.history = File.ReadAllBytes(path + "\\Global\\History");
            global.latest = File.ReadAllBytes(path + "\\Global\\Latest");
            global.partitionTable = File.ReadAllBytes(path + "\\Global\\PartitionTable");
            result.global = global;

            Partitions partitions = new Partitions();
            partitions.cero = File.ReadAllBytes(path + "\\Partitions\\1");
            result.partitions = partitions;

            return result;
        }

    }
}