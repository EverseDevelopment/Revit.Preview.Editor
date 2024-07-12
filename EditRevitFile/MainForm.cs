using System.IO;
using System.Windows.Forms;
using System;
using Newtonsoft.Json;


namespace EditRevitFile
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            GetInfoJson(this);
        }

        public static void GetInfoJson(MainForm form)
        {
            string fullFilePath = GetInfo.TempFile();


            if (File.Exists(fullFilePath))
            {
                string json = File.ReadAllText(fullFilePath);
                var obj = JsonConvert.DeserializeObject<InfoConfigUser>(json);

                form.InputBox.Text = obj.Input;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String fileLocation = InputBox.Text;
            String filename = Path.GetFileName(fileLocation);

            // Get the path to the system's temporary folder
            string tempPath = Path.GetTempPath();

            // Define the name of the new folder
            string newFolderName = "RevitImageEdition";

            // Combine the temp path with the new folder name
            string newFolderPath = Path.Combine(tempPath, newFolderName);

            // Check if the folder already exists
            if (!Directory.Exists(newFolderPath))
            {
                // Create the folder
                Directory.CreateDirectory(newFolderPath);
                Console.WriteLine("Folder created at: " + newFolderPath);
            }
            else
            {
                Console.WriteLine("Folder already exists at: " + newFolderPath);
            }

            ProcessRevitFile.Run(InputBox.Text, newFolderPath, ImageBox.Text);
        }

        private void button3_Click_1(object sender, EventArgs e)
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

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            // Create a new instance of the OpenFileDialog class
            var openFileDialog = new OpenFileDialog();

            // Show the OpenFileDialog and check if the user clicked on the OK button
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Set the text of the TextBox to the selected file
                ImageBox.Text = openFileDialog.FileName;
            }
        }
    }
}
