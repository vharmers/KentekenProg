using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data; // aanmaken reference 
using MySql.Data.MySqlClient;// aanmaken reference 

namespace RdwDataMiner
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();

            foreach (var item in listBox1.SelectedItems)
            {
                MessageBox.Show(item.ToString());
            }

            listBox1.Items.Add("bestand toegevoegd....");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string MyConString = "SERVER=localhost;" + "DATABASE=testdb;" + "USER=root;";

            MySqlConnection connection = new MySqlConnection(MyConString);
            MySqlCommand command = connection.CreateCommand();
            MySqlDataReader Reader;

            command.CommandText = "select * from Kentekens";
            connection.Open();
            Reader = command.ExecuteReader();

            while (Reader.Read())
            {
                string thisrow = "";
                for (int i = 0; i < Reader.FieldCount; i++)
                    thisrow += Reader.GetValue(i).ToString() + ",";
                listBox1.Items.Add(thisrow);
            }

            connection.Close();
        }
    }
}
