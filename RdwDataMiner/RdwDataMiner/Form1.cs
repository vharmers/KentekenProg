using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Services.Client;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data; 
using MySql.Data.MySqlClient;
using System.Net;
using System.IO; 

namespace RdwDataMiner
{
    public partial class Form1 : Form
    {
        List<string> filePath = new List<string>();

        public Form1()
        {
            InitializeComponent();
        }

        private void Browse_Click_1(object sender, EventArgs e)
        {
            //Create a OpenFileDialog instance to use
            OpenFileDialog openDialog = new OpenFileDialog();
            //We select what types are permitted to open
            openDialog.Filter = "Text Files (*.txt)|*.txt";
            //A title for the dialogbox
            openDialog.Title = "Open textfile";

            //If the user press cancel then end this void
            if (openDialog.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }

            //Set public variable ourPath to the current path
            string ourPath = Path.GetDirectoryName(openDialog.FileName);
            ourPath = ourPath + "\\" + Path.GetFileName(openDialog.FileName);

            filePath.Add(ourPath);

            listBox1.Items.Add("bestand toegevoegd....");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string line = string.Empty;
                TextReader readFile;
                if (listBox1.SelectedIndex >= 0)
                {
                    readFile = new StreamReader(filePath[listBox1.SelectedIndex]);
                }
                else
                {
                    readFile = new StreamReader(filePath.Last());
                }
                line = readFile.ReadToEnd();
                MessageBox.Show(line);
                readFile.Close();
            }

            catch (IOException ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}