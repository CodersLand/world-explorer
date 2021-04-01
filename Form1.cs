using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using DiscordRPC;
using System.IO;
using System.Linq;
using System.Security;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        int[,] world;
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
            Dictionary<string, int[,]> ar = new Dictionary<string, int[,]>();
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try  
                {
                    var sr = new StreamReader(openFileDialog1.FileName);
                    string text = sr.ReadToEnd();
                    textBox1.Text = text;
                    world = JsonConvert.DeserializeObject<Dictionary<string, int[,]>>(text)["map"];
                    
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
            Bitmap bm = new Bitmap(world.Length, world.Length);
            pictureBox1.Image = bm;
        }
    }
}
