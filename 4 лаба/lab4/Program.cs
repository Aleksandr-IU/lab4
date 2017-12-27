using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace llabor04
{
    public partial class Form1 : Form
    {
        List<string> words = new List<string>();

        public Form1()
        {
            InitializeComponent();
        }

        private void loadButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            if (dialog.ShowDialog() != DialogResult.OK) return;

            Stopwatch time = new Stopwatch();
            time.Start();

            string text = File.ReadAllText(dialog.FileName);
            string[] textArray = Regex.Split(text, "\\W");
            foreach (string word in textArray)
            {
                string trimmedWord = word.Trim();
                if (!words.Contains(trimmedWord))
                    words.Add(trimmedWord);
            }
            textBoxText.Text = text.ToString();
            time.Stop();
            label.Text = "Time for file load: " + time.Elapsed.ToString();
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            string wordToFind = textBoxWord.Text.Trim();
            textBoxWord.Text = wordToFind;

            if (string.IsNullOrEmpty(wordToFind) && words.Count <= 0) return;

            string wordToFindUpper = wordToFind.ToUpper();
            List<string> tempList = new List<string>();

            Stopwatch time = new Stopwatch();
            time.Start();

            foreach (string str in words)
                if (str.ToUpper().Contains(wordToFindUpper))
                    tempList.Add(str);

            time.Stop();
            label.Text = "Time for word search " + time.Elapsed.ToString();
            listBox.BeginUpdate();
            listBox.Items.Clear();

            foreach (string str in tempList)
            {
                listBox.Items.Add(str);
            }
            listBox.EndUpdate();

        }
    }
}