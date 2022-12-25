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
        public int SIZECELL = 30;
        public string MARKUP = "ABCDEFGHIJK";

        public int[,] mapPlayer = new int[SIZEMAP, SIZEMAP];
        public int[,] opponentMap = new int[SIZEMAP, SIZEMAP];

        public bool isPlaying = false;

        public Button[,] allMyButtons = new Button[SIZEMAP, SIZEMAP];
        public Button[,] allOpponentButtons = new Button[SIZEMAP, SIZEMAP];
        public FormGame()
        {
            InitializeComponent();
            this.Text = "SeaBattle";
            Init();
        }

        public Opponent opponent;
        public void Init()
        {
            isPlaying = false;
            CreateMaps();
            opponent = new Opponent(opponentMap, mapPlayer, allOpponentButtons, allMyButtons);
            opponentMap = opponent.Ships();
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
        public void CreateMaps()
        {
            this.Width = SIZEMAP * SIZECELL * 2 + 50;
            this.Height = (SIZEMAP + 3) * SIZECELL + 20;
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
                    allMyButtons[i, j] = button;
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
                    else
                    {
                        button.Click += new EventHandler(PlayerShoot);
                    }
                    allOpponentButtons[i, j] = button;
                    this.Controls.Add(button);
                }
            }
            Label map1 = new Label();
            map1.Text = "Player 1";
            map1.Location = new Point(SIZEMAP * SIZECELL / 2, SIZEMAP * SIZECELL + 10);
            this.Controls.Add(map1);

            Label map2 = new Label();
            map2.Text = "Player 2";
            map2.Location = new Point(350 + SIZEMAP * SIZECELL / 2, SIZEMAP * SIZECELL + 10);
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
                int offset = 0;
                if (press.Location.X > 320)
                    offset = 320;
                if (map[press.Location.Y / SIZECELL, (press.Location.X - offset) / SIZECELL] != 0)
                {
                    hit = true;
                    map[press.Location.Y / SIZECELL, (press.Location.X - offset) / SIZECELL] = 0;
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

        public bool CheckIfMapIsNotEmpty()
        {
            bool isEmpty1 = true;
            bool isEmpty2 = true;
            for (int i = 1; i < SIZEMAP; i++)
            {
                for (int j = 1; j < SIZEMAP; j++)
                {
                    if (mapPlayer[i, j] != 0)
                        isEmpty1 = false;
                    if (opponentMap[i, j] != 0)
                        isEmpty2 = false;
                }
            }
            if (isEmpty1 || isEmpty2)
                return false;
            else return true;
        }
        public void PlayerShoot(object sender, EventArgs e)
        {

            Button pressedButton = sender as Button;
            bool playerTurn = Shoot(opponentMap, pressedButton);
            if (!playerTurn)
                opponent.Shoot();

            if (!CheckIfMapIsNotEmpty())
            {
                this.Controls.Clear();
                Init();
            }
        }
    }
}
