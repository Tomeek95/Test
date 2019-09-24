using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unequal
{
    class Program
    {
        public static int[,] board = new int[5, 5];
        public static List<int> clue_sign = new List<int>();
        public static int row = 0;
        public static int column = 0;

        public static bool check_signs()
        {
            for (int i = 0; i < clue_sign.Count();)
            {
                int x1 = clue_sign[i++];
                int y1 = clue_sign[i++];
                int x2 = clue_sign[i++];
                int y2 = clue_sign[i++];
                if (board[x1, y1] <= board[x2, y2])
                    return false;
            }
            return true;
        }

        public static List<int> check_row_column()
        {
            List<int> list = new List<int>() { 1, 2, 3, 4, 5 };
            if (board[row, column] != 0)
                return new List<int> { board[row, column] };

            for (int i = 0; i < 5; i++)
            {
                if (list.Contains(board[i, column]))
                    list.Remove(board[i, column]);
                if (list.Contains(board[row, i]))
                    list.Remove(board[row, i]);
            }
            return list;
        }

        public static bool search()
        {
            if (column == 5)
            {
                column = 0;
                row++;
            }
            if (row == 5)
            {
                if (check_signs())
                    return true;
                else
                    return false;
            }

            List<int> possibilities = check_row_column();
            int backt_value = 0;
            if (board[row, column] != 0)
                backt_value = board[row, column];

            foreach (int i in possibilities)
            {
                board[row, column] = i;
                column++;

                if (search())
                {
                    return true;
                }
                else
                {
                    column--;
                    if (column == -1)
                    {
                        column = 4;
                        row--;
                    }
                    board[row, column] = backt_value;
                }
            }
            return false;
        }

        public static void print(int[,] board)
        {
            for (int row = 0; row < 5; row++)
            {
                for (int column = 0; column < 5; column++)
                {
                    Console.Write(board[row, column] + " ");
                }
                Console.Write("\n");
            }
        }

        public static void filling_board(int[,] board, List<int> clue_sign)
        {
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    board[i, j] = 0;
                }
            }

            Console.WriteLine("Enter coordinates with their values!");
            string line = Console.ReadLine();
            string[] token = line.Split(' ');
            if (token.Count() != 1)
            {
                for (int i = 0; i < token.Count(); i += 3)
                {
                    board[int.Parse(token[i]), int.Parse(token[i + 1])] = int.Parse(token[i + 2]);
                }
            }
            Console.WriteLine("Enter coordinates that are representing clue signs in the direction of: '>'!");
            line = Console.ReadLine();
            token = line.Split(' ');
            if (token.Count() != 1)
            {
                for (int i = 0; i < token.Count(); i += 2)
                {
                    clue_sign.Add(int.Parse(token[i]));
                    clue_sign.Add(int.Parse(token[i + 1]));
                }
            }
        }

        public static void Main(String[] args)
        {
            filling_board(board, clue_sign);

            if (search())
            {
                Console.WriteLine("Solution: ");
                print(board);
            }
            else
            {
                Console.WriteLine("There is no solution: ");
                print(board);
            }
            Console.ReadKey();
        }
    }
}