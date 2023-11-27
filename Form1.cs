using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CsvHelper;
using HtmlAgilityPack;
using System.IO;
using System.Collections.Generic;
using System.Globalization;
using HtmlDocument = HtmlAgilityPack.HtmlDocument;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Runtime.InteropServices;
using Bunifu.Framework.UI;
using System.Windows.Controls;
using DocumentFormat.OpenXml.Vml;

namespace bunifu
{

    public partial class Form1 : Form
    {
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
          (
              int nLeftRect,     // x-coordinate of upper-left corner
              int nTopRect,      // y-coordinate of upper-left corner
              int nRightRect,    // x-coordinate of lower-right corner
              int nBottomRect,   // y-coordinate of lower-right corner
              int nWidthEllipse, // width of ellipse
              int nHeightEllipse // height of ellipse
          );
        string file;
        public double fraction;

        public Form1()
        {

            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 25, 25));
        }




        private void Form1_Load(object sender, EventArgs e)
        {
            bunifuThinButton22.Enabled = false;
            bunifuThinButton22.ButtonText = "";
            checkplag.Enabled = false;
            checkplag.ButtonText = "";
            
        }

        private void bunifuCustomLabel1_Click(object sender, EventArgs e)
        {

        }

        private void tocheck_TextChanged(object sender, EventArgs e)
        {

        }

        private void checkplag_Click(object sender, EventArgs e)
        {


        }

        private void bunifuLabel2_Click(object sender, EventArgs e)
        {

        }

        private void Output_TextChanged(object sender, EventArgs e)
        {

        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void bunifuLabel2_Click_1(object sender, EventArgs e)
        {

        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            try
            {
                int size = -1;
                OpenFileDialog openFileDialog1 = new OpenFileDialog();
                DialogResult result = openFileDialog1.ShowDialog(); // Show the dialog.
                if (result == DialogResult.OK) // Test result.
                {
                    file = openFileDialog1.FileName;
                    try
                    {
                        string text = File.ReadAllText(file);
                        size = text.Length;
                    }
                    catch (IOException)
                    {
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Wrong Format of File (Provide .txt file)", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            try
            {
                string url = urlbox.Text;
                HtmlWeb web = new HtmlWeb();
                HtmlDocument doc = web.Load(url);
                var HeaderNames = doc.DocumentNode.SelectNodes("//div[@class='detail-center']");
                var titles = new List<Row>();
                foreach (var item in HeaderNames)
                {
                    titles.Add(new Row { Title = item.InnerText });
                }
                using (var writer = new StreamWriter("C:\\Users\\SD\\source\\repos\\bunifu\\webcontent.txt"))
                using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    csv.WriteRecords(titles);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error In Web Scrapping (No Internet / Wrong Format)", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            checkplag.Enabled = true;
            checkplag.ButtonText = "Check Plagiarism";


        }

        private void bunifuThinButton22_Click(object sender, EventArgs e)
        {
            charts c = new charts();
            c.Show();
            //double value1 = double.Parse(Output.Text);
            double value1 = fraction;
            double value2 = 100 - value1;

            // Create a list to hold the values
            List<double> values = new List<double> { value2, value1 };


            // Update the chart with new values
            c.UpdateChart(values);
            double value = fraction;



            // Update the gauge with new value
            c.UpdateGauge(value);



        }

        private void Checkplag_Click_1(object sender, EventArgs e)
        {
            try
            {
                string wordListReader = File.ReadAllText("C:\\Users\\SD\\source\\repos\\bunifu\\wordlist.txt");
                string[] wordlist = wordListReader.Split(new char[] { ' ', '\t', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
              
                for (int a = 0; a < wordlist.Length; a++)
                    Console.WriteLine(a + " " + wordlist[a]);

                int[,] map = new int[5, 28];

                string fileContent = File.ReadAllText("C:\\Users\\SD\\source\\repos\\bunifu\\index.txt");

                string[] integerStrings = fileContent.Split(new char[] { ' ', '\t', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

                int[] integers = new int[integerStrings.Length];

                for (int n = 0; n < integerStrings.Length; n++)
                    integers[n] = int.Parse(integerStrings[n]);

                int count = 0;
                for (int col = 0; col < 28; col++)
                    for (int row = 0; row < 5; row++)
                    {
                        map[row, col] = integers[count];
                        count++;
                    }
               
                string text = "";
                char[] letters;
                foreach (string line in System.IO.File.ReadLines(file))

                {
                    // letters are converted to lowercase
                    letters = line.ToLower().ToCharArray();
                    foreach (char letter in letters)
                    {
                        text += letter;
                    }
                }
                string text1 = "";
                char[] letters1;
                foreach (string line in System.IO.File.ReadLines("C:\\Users\\SD\\source\\repos\\bunifu\\webcontent.txt"))
                {
                    // letters are converted to lowercase
                    letters1 = line.ToLower().ToCharArray();
                    foreach (char letter in letters1)
                    {
                        text1 += letter;
                    }
                }
                string[] temp = { "by", "to", "can", "of", "the", "from" };
                int index;

                for (int x = 0; x < temp.Length; x++)
                {
                    while (text.IndexOf(temp[x]) != -1)
                    {
                        index = text.IndexOf(temp[x]);
                        text = text.Remove(index, temp[x].Length + 1);
                    }
                    while (text1.IndexOf(temp[x]) != -1)
                    {
                        index = text1.IndexOf(temp[x]);
                        text1 = text1.Remove(index, temp[x].Length + 1);
                    }
                }
                text = text.Replace(",", " ");
                text1 = text1.Replace(",", " ");

                char[] seperators = { ' ', ',', '.' };
                string[] l1 = text.Split(seperators);
                string[] l2 = text1.Split(seperators);

                for (int a = 0; a < l1.Length; a++)
                    Console.WriteLine(a + " " + l1[a]);

                for (int a = 0; a < l2.Length; a++)
                    Console.WriteLine(a + " " + l2[a]);

                //search words
                int[] index_l1 = new int[l1.Length];
                int[] index_l2 = new int[l2.Length];

                for (int i = 0; i < index_l1.Length; i++)
                    index_l1[i] = -1;

                for (int i = 0; i < index_l2.Length; i++)
                    index_l2[i] = -1;

                int counter1 = 0;
                int counter2 = 0;
                int sum = 0;
                string[] arrcnt = new string[l1.Length];
                for (int i = 0; i < l1.Length; i++)
                    for (int j = 0; j < l2.Length; j++)
                    {
                        if (l1[i] == l2[j])
                        {
                            if (counter1 < index_l1.Length && counter2 < index_l2.Length)
                            {
                                index_l1[counter1++] = i;
                                index_l2[counter2++] = j;
                            }
                            sum = sum + 1;
                            arrcnt[i] = "i = " + i + " j = " + j;
                            break;
                        }
                    }
                listBox1.Items.Add("Values \t Detected Words");
                listBox2.Items.Add("Undetected Words");
                for (int i = 0; i < index_l1.Length; i++)
                {
                    if (index_l1[i] != -1)
                    {
                        listBox1.Items.Add("" + index_l1[i] + "\t" + l1[i]);
                    }
                    else
                    {
                        listBox2.Items.Add("\t" + l1[i]);
                    }
                }
           
                Console.WriteLine("Sum = " + sum);
                //search
                int index_key = 0;
                int match = 0;
                fraction = 0;
                for (int iterator1 = 0; iterator1 < index_l1.Length; iterator1++)

                    if (index_l1[iterator1] == -1)
                    {
                        Console.WriteLine("iterator1 = " + iterator1);
                        Console.WriteLine(l1[iterator1]);
                        index_key = Array.IndexOf(wordlist, l1[iterator1]);
                        int synsize = 0;
                        if (index_key != -1)
                        {
                            synsize = map[0, index_key];
                            Console.WriteLine("synsize = " + synsize + "word = " + wordlist[index_key] + " syn1 = " + wordlist[map[1, index_key]] + " syn2 = " + wordlist[map[2, index_key]]);
                        }
                        if (synsize == 2)
                        {
                            int m1 = map[1, index_key];
                            int m2 = map[2, index_key];
                            match = Array.IndexOf(l2, wordlist[m1]);
                            if (match < 0)
                                match = Array.IndexOf(l2, wordlist[m2]);
                            if (match > 0)
                                fraction = Convert.ToDouble(sum + map[4, index_key] / 10);

                            Console.WriteLine("match = " + match);
                        }
                        else
                        {
                            if (index_key != -1)
                            {
                                int syn1 = map[1, index_key];
                                match = Array.IndexOf(l2, wordlist[syn1]);
                            }

                            if (match > 0)
                                fraction = Convert.ToDouble(sum + map[3, index_key] / 10);
                        }
                    }
                Console.Read();
                Console.WriteLine("" + sum);
                Console.WriteLine("" + l1.Length);
                fraction = (Convert.ToDouble(sum) / Convert.ToDouble(l1.Length)) * 100.0;
                Console.WriteLine("Average match = " + fraction + " %");
                Console.Read();
                Output.Text = " " + String.Format("{0:0.00}", fraction) + " %";
                bunifuThinButton22.Enabled = true;
                bunifuThinButton22.ButtonText = "Analytics";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "File Not Selected", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void bunifuPictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Output_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void Output_Click(object sender, EventArgs e)
        {

        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {


            listBox1.Items.Clear();
            listBox2.Items.Clear();
            Output.ResetText();
            bunifuThinButton22.Enabled = false;
            bunifuThinButton22.ButtonText = "";
            checkplag.Enabled = false;
            checkplag.ButtonText = "";
            file = "";

        }

        private void bunifuImageButton2_Click(object sender, EventArgs e)
        {

            this.Close();
        }

        private void bunifuLabel3_Click(object sender, EventArgs e)
        {

        }

        private void bunifuImageButton3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
    public class Row
    {
        public string Title { get; set; }

    }
}
