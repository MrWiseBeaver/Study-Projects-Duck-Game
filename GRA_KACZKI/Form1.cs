using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace GRA_KACZKI
{
    public partial class Form1 : Form
    {
        string name;
        int intervalTimer1;
        bool switchTime = false;
        int counterPoints=0;
        int counterAll=0;
        const int ConstTime =2500;
        Image backgroundScreen1 = Image.FromFile("backGround.jpg");
        Image backgroundScreen2 = Image.FromFile("landScape.png");
        Image duck = Image.FromFile("duck.png");

        Random value = new Random();


        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            name = textBox1.Text;
            FirstScreen(false);
            SecondScreen(true);
            SetUpTimer1();
            counterPoints = 0;
            counterAll = 0;
            textBox2.Text = "";
            this.BackgroundImage = backgroundScreen2;
        }

        private void FirstScreen(bool state)
        {
            button1.Visible = state;
            label1.Visible = state;
            textBox1.Visible = state;
        }

        private void SecondScreen(bool state)
        {
            button2.Visible = state;
            label2.Visible = state;
            label3.Visible = state;
            label4.Visible = state;
            textBox2.Visible = state;
            trackBar1.Visible = state;
        }
        private void SetUpTimer1()
        {
            intervalTimer1 = ConstTime;
            timer1.Enabled = true;
        }


        private void button2_Click(object sender, EventArgs e)
        {
            FirstScreen(true);
            SecondScreen(false);
            this.BackgroundImage = backgroundScreen1;
            timer1.Enabled = false;
            button3.Visible = false;
            save();
        }
        
        private void save()
        {
            using (StreamWriter sw = new StreamWriter("Wyniki.txt", true))
            {
                DateTime date1 = DateTime.Now;
                string word = "Nazwa gracza: " + name + "\n" + "Wynik: " +
                    Convert.ToString(counterPoints) + " / " + Convert.ToString(counterAll) + "\n" +
                        "Godzina: " + date1 + "\n" + "\n";
                sw.WriteLine(word);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            textBox2.Text = Convert.ToString(counterPoints) + " / " + Convert.ToString(counterAll);
            HandleDuck();
            switchTime = !switchTime;

            Random Time = new Random();
            int randomTime  = Time.Next(-100, 100);
            if (switchTime)
            {
                intervalTimer1 =  ConstTime + randomTime ;
            }
            else
            {
                intervalTimer1 = randomTime+ ConstTime /trackBar1.Value;
            }
            timer1.Interval = intervalTimer1;
        }
        private void HandleDuck()
        {
            button3.Visible = !button3.Visible;
            if (button3.Visible)
            {
                counterAll++;
            }
            else
            {
                int posX = value.Next(100, this.Width - 200);
                int posY = value.Next(20, this.Height - 200);
                button3.Location = new System.Drawing.Point(posX, posY);
            }

            // int posX = 555 * value.Next(0, 1000) % (this.Width - 200) + 100;
            //int posY = 222 * value.Next(100, this.Height) % (this.Height - 300) + 100;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            counterPoints++;
            timer1.Interval = ConstTime;
            button3.Visible = !button3.Visible;
            textBox2.Text = Convert.ToString(counterPoints) + " / " + Convert.ToString(counterAll);
        }


    }
}
