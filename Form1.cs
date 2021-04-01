using DiscordRPC;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Security;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        int[][] world;
        public Form1()
        {
            InitializeComponent();
            var client = new DiscordRpcClient("814090150144770078");
            client.Initialize();
            var presense = new RichPresence();
            presense.State = "123";
            client.SetPresence(presense);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    var reader = new StreamReader(openFileDialog1.FileName);
                    var result = reader.ReadToEnd();
                    world = JsonConvert.DeserializeObject<Dictionary<string, int[][]>>(result)["map"];
                }
                catch (SecurityException ex)
                {
                    MessageBox.Show($"Security error.\n\nError message: {ex.Message}\n\n" +
                    $"Details:\n\n{ex.StackTrace}");
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (world == null)
            {
                MessageBox.Show("You Abosralsa");
                return;
            }
            pictureBox1.Location = new Point(323, 12);
            int size = Convert.ToInt16(numericUpDown1.Value);
            Bitmap bm = new Bitmap(world.Length*size, world.Length*size);
            Graphics g = Graphics.FromImage(bm);
            progressBar1.Visible = true;
            progressBar1.Maximum = world.Length;
            Color[] colors = new Color[5]
            {
                Color.Green, Color.Blue, Color.Red, Color.Gray, Color.Yellow
            };
            for (int i = 0; i < world.Length; i++)
            {
                progressBar1.Value++;
                for (int j = 0; j < world[0].Length; j++)
                {
                    g.FillRectangle(new SolidBrush(colors[world[i][j]]), size*i, size*j, size * i + (size - 1), size * j + (size - 1));
                
                }
            }
            progressBar1.Value = 0;
            progressBar1.Visible = false;
            pictureBox1.Image = bm;

        }

        private void progressBar1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("alert", "123");
        }
    }
}
