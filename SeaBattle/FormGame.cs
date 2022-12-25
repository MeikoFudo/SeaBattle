using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SeaBattle
{
    public partial class FormGame : Form
    {
        public const int SIZEMAP = 10;
        public int SIZECELL = 40;
        public string MARKUP = "ABCDEFGHIJK";

        public int[,] mapPlayer = new int[SIZEMAP, SIZEMAP];
        public int[,] opponentMap = new int[SIZEMAP, SIZEMAP];

        public bool isPlaying = false;
        public FormGame()
        {
            InitializeComponent();
            this.Text = "SeaBattle";
            Init();
        }

        public void Init()
        {
            isPlaying = false;
            CreateMaps();
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
        public void CreateMaps()
        {
            this.Width = SIZEMAP * SIZECELL * 2 + 70;
            this.Height = SIZEMAP * SIZECELL;
            for (int i = 0; i < SIZEMAP; i++)
            {
                for (int j = 0; j < SIZEMAP; j++)
                {
                    mapPlayer[i, j] = 0;

                    Button button = new Button();
                    button.Size = new Size(SIZECELL, SIZECELL);
                    button.BackColor = Color.White;
                    button.Location = new Point(j * SIZECELL, i * SIZECELL);
                    if (j == 0 || i == 0)
                    {
                        button.BackColor = Color.Gray;
                        if (i == 0 && j > 0)
                            button.Text = MARKUP[j - 1].ToString();
                        if (j == 0 && i > 0)
                            button.Text = i.ToString();
                    }
                    else
                    {
                        button.Click += new EventHandler(ConfShips);
                    }
                    this.Controls.Add(button);

                }
            }


            for (int i = 0; i < SIZEMAP; i++)
            {
                for (int j = 0; j < SIZEMAP; j++)
                {
                    opponentMap[i, j] = 0;
                    mapPlayer[i, j] = 0;

                    Button button = new Button();
                    button.Size = new Size(SIZECELL, SIZECELL);
                    button.BackColor = Color.White;
                    button.Location = new Point(450 + j * SIZECELL, i * SIZECELL);
                    if (j == 0 || i == 0)
                    {
                        button.BackColor = Color.Gray;
                        if (i == 0 && j > 0)
                            button.Text = MARKUP[j - 1].ToString();
                        if (j == 0 && i > 0)
                            button.Text = i.ToString();
                    }
                    this.Controls.Add(button);
                }
            }
            Label map1 = new Label();
            map1.Text = "Player 1";
            map1.Location = new Point(SIZEMAP * SIZECELL / 2, SIZEMAP * SIZECELL + 10);
            this.Controls.Add(map1);

            Label map2 = new Label();
            map2.Text = "Player 2";
            map2.Location = new Point(450 + SIZEMAP * SIZECELL / 2, SIZEMAP * SIZECELL + 10);
            this.Controls.Add(map2);

            Button start = new Button();
            start.Text = "START";
            start.Click += new EventHandler(StartPlay);
            start.Location = new Point(0, SIZEMAP * SIZECELL + 20);
            this.Controls.Add(start);
        }
        public void StartPlay(object sender, EventArgs e)
        {
            isPlaying = true;
        }

        public void ConfShips(object sender, EventArgs e)
        {
            Button press = sender as Button;
            if (isPlaying)
            {
                if (mapPlayer[press.Location.Y / SIZECELL, press.Location.X / SIZECELL] == 0)
                {
                    press.BackColor = Color.Red;
                    mapPlayer[press.Location.Y / SIZECELL, press.Location.X / SIZECELL] = 1;
                }
                else
                {
                    press.BackColor = Color.White;
                    mapPlayer[press.Location.Y / SIZECELL, press.Location.X / SIZECELL] = 0;
                }
            }
        }

        public bool Shoot(int[,] map, Button press)
        {
            bool hit = false;
            if (isPlaying)
            {
                int delta = 0;
                if (press.Location.X > 320)
                    delta = 320;
                if (map[press.Location.Y / SIZECELL, (press.Location.X - delta) / SIZECELL] != 0)
                {
                    hit = true;
                    map[press.Location.Y / SIZECELL, (press.Location.X - delta) / SIZECELL] = 0;
                    press.BackColor = Color.Green;
                    press.Text = "X";
                }
                else
                {
                    hit = false;

                    press.BackColor = Color.Black;
                }
            }
            return hit;
        }
    }
}
