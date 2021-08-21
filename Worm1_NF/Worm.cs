using System;

namespace Worm1_NF
{
    class WormClass
    {
        private struct Coor
        {
            public int x;
            public int y;
        }

        public Boolean gameOver = false;
        public double wDelay = 0.5;
        private readonly int wMaxLen = 1000;
        private readonly int fWidth = 80;
        private readonly int fHeight = 40;
        private int wNap = 1;
        private int wLen = 3;
        private int[,] fField;
        private char[] fChar;
        private Coor[] wData;
        private Coor coor;
        private int wStep = 0;
        private int wHead = 0;
        private int wTail = 0;
        private int wQueue = 0;

        private Random rnd;


        public WormClass()
        {
            rnd = new Random();
            fField = new int[fWidth, fHeight];
            fChar = new char[fWidth * fHeight];
            wData = new Coor[wMaxLen];
            coor = new Coor();
        }
        public void Step()
        {
            wStep++;
            coor = wData[wHead];

            switch (wNap)
            {
                case 1:
                    {
                        coor.x++;
                        break;
                    }
                case 2:
                    {
                        coor.y++;
                        break;
                    }
                case 3:
                    {
                        coor.x--;
                        break;
                    }
                case 0:
                    {
                        coor.y--;
                        break;
                    }
            }
            if ((fField[coor.x, coor.y] > 48) && (fField[coor.x, coor.y] < 58))
            {
                wQueue = fField[coor.x, coor.y] - 48;
                wDelay *= 0.95;
                RandSetNum();
            }
            else
            {
                if ((fField[coor.x, coor.y] == 88) || (fField[coor.x, coor.y] == 64))
                {
                    gameOver = true;
                }

            }
            AddHead(coor);
            fField[coor.x, coor.y] = 88;
            coor = GetTale();
            fField[coor.x, coor.y] = 32;

        }

        public void NapLeft()
        {
            wNap--;
            if (wNap < 0) wNap = 3;
        }
        public void NapRight()
        {
            wNap++;
            if (wNap > 3) wNap = 0;
        }
        private Coor GetTale()
        {
            if (wQueue == 0)
            {
                wTail++;
                if (wTail >= wMaxLen) wTail = 0;
            }
            else
            {
                wLen++;
                wQueue--;
            }

            return wData[wTail];
        }

        private void AddHead(Coor c)
        {
            wHead++;
            if (wHead >= wMaxLen) wHead = 0;
            wData[wHead] = c;
        }
        private void RandSetNum()
        {
            int x, y;
            do
            {
                x = rnd.Next(1, 80);
                y = rnd.Next(1, 40);
            } while (fField[x, y] != 32);
            fField[x, y] = rnd.Next(48, 58);
            if (fField[x, y] == 48)
            {
                fField[x, y] = 64;
                RandSetNum();
            }
        }

        public void Init()
        {
            Console.SetWindowSize(fWidth, fHeight + 1);
            Console.SetBufferSize(fWidth, fHeight + 1);
            Console.CursorVisible = false;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Title = "Червяк 1.0";
            Console.Clear();
            Console.WriteLine("Управление:\n" +
                                "Стрелки вправо и влево\n" +
                                "Space - пауза\n" +
                                "Esc - выход");
            Console.ReadKey();
            Console.Clear();

            for (int x = 0; x < fWidth; x++) for (int y = 0; y < fHeight; y++) fField[x, y] = 32;

            for (int x = 0; x < fWidth; x++)
            {
                fField[x, 0] = 88;
                fField[x, fHeight - 1] = 88;
            }
            for (int y = 0; y < fHeight; y++)
            {
                fField[0, y] = 88;
                fField[fWidth - 1, y] = 88;
            }

            coor.x = fWidth / 2;
            coor.y = fHeight / 2;
            AddHead(coor);
            fField[coor.x, coor.y] = 88;
            coor.x++;
            AddHead(coor);
            fField[coor.x, coor.y] = 88;
            coor.x++;
            AddHead(coor);
            fField[coor.x, coor.y] = 88;


            for (int n = 0; n < 10; n++) RandSetNum();

        }
        public void Show()
        {
            for (int y = 0; y < fHeight; y++)
            {
                for (int x = 0; x < fWidth; x++)
                {
                    fChar[y * fWidth + x] = (char)fField[x, y];
                }
            }
            Console.SetCursorPosition(0, 0);
            Console.Write(fChar);
            Console.Write("Шагов: {0}\tДлинна {1}\tЗадержка {2} ", wStep, wLen, string.Format("{0:0.###}", wDelay));
        }
    }
}