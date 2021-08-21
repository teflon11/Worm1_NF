using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Worm1_NF
{
    class Program
    {
        static void Main()
        {
            ConsoleKey consKey;
            DateTime dateTimeFrom;
            DateTime dateTimeTo;
            double elapsedTime;

            WormClass wrm = new WormClass();
            wrm.Init();
            wrm.Show();

            dateTimeFrom = DateTime.Now;

            while (wrm.gameOver == false)
            {
                if (Console.KeyAvailable)
                {
                    consKey = Console.ReadKey(true).Key;

                    switch (consKey)
                    {
                        case ConsoleKey.LeftArrow:
                            wrm.NapLeft();
                            break;
                        case ConsoleKey.RightArrow:
                            wrm.NapRight();
                            break;
                        case ConsoleKey.Spacebar:
                            //Console.Beep();
                            Console.Write(" Пауза ");
                            Console.CursorVisible = false;
                            Console.ReadKey();
                            Console.Clear();
                            break;
                    }

                    if (consKey == ConsoleKey.Escape)
                    {
                        break;//while
                    }
                }

                dateTimeTo = DateTime.Now;
                elapsedTime = (dateTimeTo - dateTimeFrom).TotalSeconds;
                if (elapsedTime > wrm.wDelay)
                {
                    wrm.Step();
                    wrm.Show();
                    dateTimeFrom = dateTimeTo;
                }
            }
            Console.Beep();
            Console.Write(" Игра окончена! ");

            Console.ReadKey();
        }
    }
}
