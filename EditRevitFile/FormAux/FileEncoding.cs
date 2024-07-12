using System.IO;
using System.Text;

namespace EditRevitFile
{
    public static class FileEncoding
    {
        public static Encoding Get(byte[] temp)
        {
            string text;
            Encoding encoding;
            using (StreamReader reader = new StreamReader(new MemoryStream(temp),
                                                          detectEncodingFromByteOrderMarks: true))
            {
                text = reader.ReadToEnd();
                encoding = reader.CurrentEncoding; // the reader detects the encoding for you!
            }
            return encoding;
        }
    }
}
