using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace GameOfLifeTwoWPF
{
    public class make
    {
        public const int gridLength = 25;
        public int[,] grid = new int[gridLength, gridLength];
        public int[,] tempgrid = new int[gridLength, gridLength];

        Random alive = new Random();


        public void makeGrid()
        {
            for (int x = 1; x < gridLength - 1; x++)
            {
                for (int y = 1; y < gridLength - 1; y++)
                {
                    int rand = alive.Next(0, 5);
                    rand = (rand < 1 ? 1 : 0);
                    grid[x, y] = rand;
                }
            }/*
            grid = new int[10, 10] { { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                     { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                     { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                     { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                     { 0, 0, 0, 1, 1, 1, 0, 0, 0, 0 },
                                     { 0, 0, 1, 0, 0, 0, 1, 0, 0, 0 },
                                     { 0, 0, 0, 1, 1, 1, 0, 0, 0, 0 },
                                     { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                     { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                     { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }};
                                     */
            tempgrid = (int[,])grid.Clone();

        }

        public void checkPos()
        {
            for (int x = 0; x < gridLength - 1; x++)
            {
                for (int y = 0; y < gridLength - 1; y++)
                {
                    action(checkAround(x, y), x, y);
                }
            }
        }

        public int checkAround(int x, int y)
        {
            int check = 0;
            List<int> dir = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7 };
            List<int> exception = new List<int>();


            switch (x)
            {
                case 0:
                    exception.Add(0);
                    exception.Add(1);
                    exception.Add(2);
                    break;
                case gridLength:
                    exception.Add(5);
                    exception.Add(6);
                    exception.Add(7);
                    break;
            }

            switch (y)
            {
                case 0:
                    exception.Add(0);
                    exception.Add(3);
                    exception.Add(5);
                    break;
                case gridLength:
                    exception.Add(2);
                    exception.Add(4);
                    exception.Add(7);
                    break;
            }

            exception = exception.Distinct().ToList();
            exception.Sort();
            exception.Reverse();


            for (int i = 0; i < exception.Count; i++)
            {
                dir.RemoveAt(exception[i]);
            }

            foreach (int i in dir)
            {
                int tempX = x;
                int tempY = y;
                check = check + checkCurrentPos(i, tempX, tempY);
            }
            return check;
        }

        public int checkCurrentPos(int i, int tempX, int tempY)
        {
            int check = 0;
            switch (i)
            {
                case 0:
                    tempX--;
                    tempY--;
                    if (grid[tempX, tempY] == 1)
                    {
                        check++;
                    }
                    break;
                case 1:
                    tempX--;
                    if (grid[tempX, tempY] == 1)
                    {
                        check++;
                    }
                    break;
                case 2:
                    tempX--;
                    tempY++;
                    if (grid[tempX, tempY] == 1)
                    {
                        check++;
                    }
                    break;
                case 3:
                    tempY--;
                    if (grid[tempX, tempY] == 1)
                    {
                        check++;
                    }
                    break;
                case 4:
                    tempY++;
                    if (grid[tempX, tempY] == 1)
                    {
                        check++;
                    }
                    break;
                case 5:
                    tempX++;
                    tempY--;
                    if (grid[tempX, tempY] == 1)
                    {
                        check++;
                    }
                    break;
                case 6:
                    tempX++;
                    if (grid[tempX, tempY] == 1)
                    {
                        check++;
                    }
                    break;
                case 7:
                    tempX++;
                    tempY++;
                    if (grid[tempX, tempY] == 1)
                    {
                        check++;
                    }
                    break;
            }
            return check;
        }

        public void action(int check, int x, int y)
        {

            if (grid[x, y] == 1)
            {
                if (check == 0 || check == 1 || check >= 4)
                {
                    tempgrid[x, y] = 0;
                }
            }
            else
            {
                if (check == 3)
                {
                    tempgrid[x, y] = 1;
                }
            }
        }
    }
}
