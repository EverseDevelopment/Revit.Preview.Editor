using OpenMcdf;
using System.Collections.Generic;
using System.IO;
using System.IO.Packaging;
using System.Text;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Text.Json;
using System;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;
using RevitFormatDecompress.FormAux;
using RevitFormatDecompress.Objects;
using System.Collections;
using System.Xml;
using CodeCave.Revit.Toolkit.OLE;
using System.Runtime.InteropServices.ComTypes;
using System.IO.Compression;

namespace RevitFormatDecompress
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            GetInfoJson(this);
        }

        public static void GetInfoJson(Form1 form)
        {
            string fullFilePath = GetInfo.TempFile();


            if (File.Exists(fullFilePath))
            {
                string json = File.ReadAllText(fullFilePath);
                var obj = JsonConvert.DeserializeObject<InfoConfigUser>(json);

                form.InputBox.Text = obj.Input;
                form.BoxStorage.Text = obj.Storage;
                form.FileBox.Text = obj.File;
                form.OutputBox.Text = obj.Output;
                form.CheckboxAll.Checked = obj.ExportAll;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String fileLocation = InputBox.Text;
            String filename = Path.GetFileName(fileLocation);
            CompoundFile cf = new CompoundFile(fileLocation);

            if (CheckboxAll.Checked == true)
            {
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
                string fileName = Path.GetFileName(InputBox.Text).Replace(".rvt", "");
                ProcessStreams(contentsList, cf, fileName);
            }
            else
            {
                byte[] temp = GetSingleStream(BoxStorage.Text, FileBox.Text, cf);
                CMDF cmdf = new CMDF();

                if (BoxStorage.Text == null || BoxStorage.Text == "")
                {
                    cmdf.parentName = null;
                    cmdf.child = false;
                }
                else
                {
                    cmdf.parentName = BoxStorage.Text;
                    cmdf.child = true;
                }

                cmdf.name = FileBox.Text;
                List<CMDF> listCmdf = new List<CMDF> { cmdf };
                string fileName = Path.GetFileName(InputBox.Text).Replace(".rvt","");
                ProcessStreams(listCmdf, cf, fileName);

                //Dictionary<string, string> test = GetProperties(temp, encoding);
                //File.WriteAllLines(@"C:\Users\User\Desktop\myfile.txt", test.Select(x => "[" + x.Key + " " + x.Value + "]").ToArray());
            }

            WritteInfo.Run(BoxStorage.Text, FileBox.Text, fileLocation, CheckboxAll.Checked, OutputBox.Text);
            cf.Close();
            MessageBox.Show("Succesfully exported");
        }

        private void ProcessStreams(List<CMDF> cmdfFiles, CompoundFile cf, string fileName)
        {
            string outputMainFolder = Path.Combine(OutputBox.Text, fileName);
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

        private byte[] GetSingleStream(string storage, string stream, CompoundFile cf)
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


        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckboxAll.Checked == true)
            {
                BoxStorage.Enabled = false;
                FileBox.Enabled = false;
            }
            else
            {
                BoxStorage.Enabled = true;
                FileBox.Enabled = true;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Create a new instance of the OpenFileDialog class
            var openFileDialog = new OpenFileDialog();

            // Show the OpenFileDialog and check if the user clicked on the OK button
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Set the text of the TextBox to the selected file
                InputBox.Text = openFileDialog.FileName;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (var folderBrowserDialog = new FolderBrowserDialog())
            {
                DialogResult result = folderBrowserDialog.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(folderBrowserDialog.SelectedPath))
                {
                    OutputBox.Text = folderBrowserDialog.SelectedPath;
                }
            }
        }
    }
}
