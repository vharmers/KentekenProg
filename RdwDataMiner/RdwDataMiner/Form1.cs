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
using System.Diagnostics; 


namespace RdwDataMiner
{
    public partial class Form1 : Form
    {
        List<string> filePath = new List<string>();

        PerformanceCounter perform = new PerformanceCounter("Processor", "% Processor Time", "_Total");


        public Form1()
        {
            InitializeComponent();
            CPU_Time();
        }

        public string CPU_Time()
        {
            return perform.NextValue() + "% ";
        }
        /// <summary>
        /// When you click on button browse this open the file dialog and the user can choiche his/her textfile to import it in the program
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        /// <summary>
        /// this is the seconde step after you open a file in your program the user can read the file text en it will show the output of the filetext and it calculte also the CPU time.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        { 
            try
            {
                label1.Text = "CPU Time: " + CPU_Time();
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