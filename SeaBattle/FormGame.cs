using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SeaBattle
{
    public partial class FormGame : Form
    {
        public const int SIZEMAP = 10;
        public int SIZECELL = 90;
        public string MARKUP = "ABCDEFGHIJK";
        public int[,] mapPlayer = new int[SIZEMAP, SIZEMAP];
        public int[,] opponentMap = new int[SIZEMAP, SIZEMAP];
        public FormGame()
        {
            InitializeComponent();
            Init();
        }

        public void Init()
        {
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
                    button.Location = new Point(j * SIZECELL, i * SIZECELL);
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
                    button.Location = new Point(350+j * SIZECELL, i * SIZECELL);
                    this.Controls.Add(button);
                }
            }
        }
    }
}
