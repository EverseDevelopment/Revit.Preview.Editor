using System;
using System.Collections.Generic;
using System.Text;

namespace SharedRevitInfo
{
    public class RvtFile
    {
        public byte[] basicFileInfo { get; set; }

        public byte[] contents { get; set; }

        public byte[] projectInformation { get; set; }

        public byte[] revitPreview40 { get; set; }

        public byte[] TransmissionData { get; set; }

        public Formats formats { get; set; }

        public Global global { get; set; }
        public Partitions partitions { get; set; }
    }

    public class Partitions
    {
        public byte[] cero { get; set; }
    }

    public class Formats
    {
        public byte[] latest { get; set; }

    }
    public class Global
    {
        public byte[] contentDocuments { get; set; }

        public byte[] documentIncrementTable { get; set; }

        public byte[] elemTable { get; set; }

        public byte[] history { get; set; }

        public byte[] latest { get; set; }

        public byte[] partitionTable { get; set; }
    }
}
