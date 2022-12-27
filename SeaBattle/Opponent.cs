using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SeaBattle
{
    public class Opponent
    {
        public int[,] mapPlayer = new int[FormGame.SIZEMAP, FormGame.SIZEMAP];
        public int[,] opponentMap = new int[FormGame.SIZEMAP, FormGame.SIZEMAP];

        public Button[,] allMyButtons = new Button[FormGame.SIZEMAP, FormGame.SIZEMAP];
        public Button[,] allOpponentButtons = new Button[FormGame.SIZEMAP, FormGame.SIZEMAP];

        public Opponent(int[,] mapPlayer, int[,] opponentMap, Button[,] allMyButtons, Button[,] allOpponentButtons)
        {
            this.mapPlayer = mapPlayer;
            this.opponentMap = opponentMap;
            this.allOpponentButtons = allOpponentButtons;
            this.allMyButtons = allMyButtons;
        }

        public bool Border(int i, int j)
        {
            if (i < 0 || j < 0 || i >= FormGame.SIZEMAP || j >= FormGame.SIZEMAP)
            {
                return false;
            }
            return true;
        }

        public bool IsEmpty(int i, int j, int length)  //Проверка на наличие метки на карте противника 
        {
            bool isEmpty = true;

            for (int k = j; k < j + length; k++)
            {
                if (mapPlayer[i, k] != 0)
                {
                    isEmpty = false;
                    break;
                }
            }

            return isEmpty;
        }  

        public int[,] Ships()
        {
            int SHIPLENGTH = 4;
            int DELTA = 4;
            int COUNTSHIPS = 10;
            Random r = new Random();

            int X = 0;
            int Y = 0;

            while (COUNTSHIPS > 0)
            {
                for (int i = 0; i < DELTA / 4; i++)
                {
                    X = r.Next(1, FormGame.SIZEMAP);
                    Y = r.Next(1, FormGame.SIZEMAP);

                    while (!Border(X, Y + SHIPLENGTH - 1) || !IsEmpty(X, Y, SHIPLENGTH))
                    {
                        X = r.Next(1, FormGame.SIZEMAP);
                        Y = r.Next(1, FormGame.SIZEMAP);
                    }
                    for (int k = Y; k < Y + SHIPLENGTH; k++)
                    {
                        mapPlayer[X, k] = 1;
                    }



                    COUNTSHIPS--;
                    if (COUNTSHIPS <= 0)
                        break;
                }
                DELTA += 4;
                SHIPLENGTH--;
            }
            return mapPlayer;
        }

        public bool Shoot()
        {
            bool hit = false;
            Random r = new Random();

            int posX = r.Next(1, FormGame.SIZEMAP);
            int posY = r.Next(1, FormGame.SIZEMAP);

            while (allOpponentButtons[posX, posY].BackColor == Color.Blue || allOpponentButtons[posX, posY].BackColor == Color.Black)
            {
                posX = r.Next(1, FormGame.SIZEMAP);
                posY = r.Next(1, FormGame.SIZEMAP);
            }

            if (opponentMap[posX, posY] != 0)
            {
                hit = true;
                opponentMap[posX, posY] = 0;
                allOpponentButtons[posX, posY].BackColor = Color.Blue;
                allOpponentButtons[posX, posY].Text = "X";
            }
            else
            {
                hit = false;
                allOpponentButtons[posX, posY].BackColor = Color.Black;
            }
            if (hit)
                Shoot();
            return hit;
        }
    }
}
