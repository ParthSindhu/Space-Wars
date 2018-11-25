// trying to use git here

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Space_wars
{

    class Program
    {
        static void Main(string[] args)
        {
            game game1 = new game(50, 30);
            game1.start();
            Console.ReadKey();
        }
    }

    class projectile
    {
        const int PROSPEED = 2;

        enum edir
        {
            up, down, left, right
        }

        private edir direction;
        private int x, y;
        private int speed = PROSPEED;

        public projectile(int newdir, int newx, int newy)
        {
            direction = (edir)newdir;
            x = newx;
            y = newy;
        }

        public void update()
        {
            for (int i = 0; i < speed; i++)
                switch ((int)direction)
                {
                    case 0:
                        y--;
                        break;

                    case 3:
                        y++;
                        break;

                    case 2:
                        x++;
                        break;

                    case 1:
                        x--;
                        break;

                }
        }

        public int getx
        {
            get { return x; }
        }

        public int gety
        {
            get { return y; }
        }
    }

    class tank
    {
        const int WEAPONSTART = 5;

        public enum edir
        {
            up, left, right ,down
        }

        private int speed;
        private int health;
        private edir direction;
        private int x, y;
        int weaponcount;
        public List<projectile> weapon = new List<projectile>();

        public edir newdirection
        {
            get { return direction; }
            set { direction = value; }
        }

        public tank(int newx, int newy)
        {
            speed = 1;
            weaponcount = WEAPONSTART;
            health = 100; // 100 => full
            x = newx;
            y = newy;
            direction = (edir)1;
        }

        private void move()
        {
            switch ((int)direction)
            {
                case 0:
                    y--;
                    break;

                case 3:
                    y++;
                    break;

                case 2:
                    x++;
                    break;

                case 1:
                    x--;
                    break;

            }
        }

        public void update()
        {
            for (int i = 0; i < speed; i++)
            {
                move();
            }
        }

        public int damage
        {
            set
            {
                if (value > 0)
                    health -= value;
                else
                    Console.WriteLine("ERROR!");
            }
        }

        public int gethealth
        {
            get { return health; }
        }

        public int weaponcountdisplay
        {
            get { return weaponcount; }
        }

        public void fire()
        {
            projectile temp = new projectile((int)direction, x, y);
            weapon.Add(temp);
            weaponcount--;
        }

        public int getx
        {
            get { return x; }
        }

        public int gety
        {
            get { return y; }
        }
    }

    class game
    {
        private static int height, width;
        private int win;

        public game(int w, int h)
        {
            height = h;
            width = w;
            win = 0;
        }

        private tank tank1 = new tank(3, 3);
        private tank tank2 = new tank(50 - 5, 30 - 5); // $$$$$$$$$$$$$$$$problem###########

        void input(char ch)
        {
               

                switch (ch)
                {
                    // player 1 - w a s d + e 
                    case 'w':
                        tank1.newdirection = (tank.edir)0;
                        break;

                    case 'a':
                        tank1.newdirection = (tank.edir)1;
                        break;

                    case 's':
                        tank1.newdirection = (tank.edir)3;
                        break;

                    case 'd':
                        tank1.newdirection = (tank.edir)2;
                        break;

                    case 'e':
                        tank1.fire();
                        break;

                    // player 2 - i j k l + o 
                    case 'i':
                        tank2.newdirection = (tank.edir)0;
                        break;

                    case 'j':
                        tank2.newdirection = (tank.edir)1;
                        break;

                    case 'k':
                        tank2.newdirection = (tank.edir)3;
                        break;

                    case 'l':
                        tank2.newdirection = (tank.edir)2;
                        break;

                    case 'o':
                        tank2.fire();
                        break;
                        // intructions
                }

                // use fire function and change direction
            
        }

        void draw()
        {
            Console.BackgroundColor = ConsoleColor.White;
            // tank1
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.SetCursorPosition(tank1.getx - 1, tank1.gety);

            Console.Write("=" + "1" + "=");

            Console.SetCursorPosition(tank1.getx, tank1.gety - 1);
            Console.Write("#");
            Console.SetCursorPosition(tank1.getx, tank1.gety + 1);
            Console.Write("#");

            Console.ForegroundColor = ConsoleColor.DarkBlue;
            for (int i = 0; i < tank1.weapon.Count; i++)
            {
                Console.SetCursorPosition(tank1.weapon[i].getx, tank1.weapon[i].gety);
                Console.Write("o");
            }

            // tank 2

            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.SetCursorPosition(tank2.getx - 1, tank2.gety);

            Console.Write("=" + "2" + "=");

            Console.SetCursorPosition(tank2.getx, tank2.gety - 1);
            Console.Write("#");
            Console.SetCursorPosition(tank2.getx, tank2.gety + 1);
            Console.Write("#");

            Console.ForegroundColor = ConsoleColor.DarkBlue;
            for (int i = 0; i < tank2.weapon.Count; i++)
            {
                Console.SetCursorPosition(tank2.weapon[i].getx, tank2.weapon[i].gety);
                Console.Write("o");
            }
            //boundry
            //vertical
            for (int i = 0; i < height; i++)
            {
                Console.SetCursorPosition(width, i);
                Console.Write("#");
            }
            //horizontal
            Console.SetCursorPosition(0, height);
            for (int i = 0; i < width; i++)
                Console.Write("#");
            Console.WriteLine();
            Console.WriteLine("Tank1 Health :" + tank1.gethealth.ToString() + " Weapon Count : " + tank1.weaponcountdisplay);
            Console.WriteLine("Tank2 Health :" + tank2.gethealth.ToString() + " Weapon Count : " + tank2.weaponcountdisplay);
        }

        void update()
        {
            tank1.update();
            tank2.update();

            for (int i = 0; i < tank1.weapon.Count; i++)
            {
                tank1.weapon[i].update();
            }

            for (int i = 0; i < tank2.weapon.Count; i++)
            {
                tank2.weapon[i].update();
            }
        }

        void logic()
        {
            update();

            if (tank1.getx <= 2|| tank1.gety <= 2 || tank1.getx >= width - 2 || tank1.gety >= height - 2)
                  tank1.newdirection = (tank.edir)(3 - (int)tank1.newdirection );

            if (tank2.getx <= 2 || tank2.gety <= 2 || tank2.getx >= width - 2 || tank2.gety >= height - 2)
                tank2.newdirection = (tank.edir)(3 - (int)tank2.newdirection);

            if (tank2.getx == tank1.getx && tank2.gety == tank1.gety)
            {
                tank2.newdirection = (tank.edir)(3 - (int)tank2.newdirection);
                tank1.newdirection = (tank.edir)(3 - (int)tank1.newdirection);
            }

            bool check = false;

            for (int i = 0; i < tank2.weapon.Count; i++)
            {
                if (tank2.weapon[i].getx == tank1.getx && tank2.weapon[i].gety == tank1.gety)
                {
                    tank1.damage = 5;
                    tank2.weapon.RemoveAt(i);
                    check = true;
                }
                if(!check)
                if (tank2.weapon[i].getx >= width || tank2.weapon[i].gety >= height || tank2.weapon[i].getx <= 0 || tank2.weapon[i].gety <= 0)
                    tank2.weapon.RemoveAt(i);
            }

            check = false;
            for (int i = 0; i < tank1.weapon.Count; i++)
            {
                if (tank1.weapon[i].getx == tank2.getx && tank1.weapon[i].gety == tank2.gety)
                {
                    tank2.damage = 5;
                    tank1.weapon.RemoveAt(i);
                    check = true;
                }
                if(!check)
                if (tank1.weapon[i].getx >= width || tank1.weapon[i].gety >= height || tank1.weapon[i].getx <= 0 || tank1.weapon[i].gety <= 0)
                    tank1.weapon.RemoveAt(i);
            }

            if (tank1.gethealth <= 0)
                win = 2;
            if (tank2.gethealth <= 0)
                win = 1;
        }

        void inputer()
        {    
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                input(key.KeyChar);
            }   
        }
   
        public void start()
        {
            while (win == 0)
            {
                for (int i = 0; i < 500; i++) ;
                logic();
                draw();
                inputer();
            }

            Console.WriteLine("Player " + win.ToString() + " Has Won!");

        }

    }
}
