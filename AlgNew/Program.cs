using System;

namespace Project
{
    class Program
    {
        static void Main(string[] args)
        {
            
            // Console.WriteLine("Введите количество строк:"); //Задание 2 (а)
            // int t = Convert.ToInt32(Console.ReadLine());
            // Console.WriteLine("Введите количество столбцов:");
            // int i = Convert.ToInt32(Console.ReadLine());

            // int[,] array = GenerateArray(t, i);
            
            // int otvet = array[t-1, 0];
            
            // PrintArray(array);

            // // Вывод элемента массива с координатами (t-1, 0)
            // Console.WriteLine("Элемент массива в нижнем левом углу " + otvet);

            Console.WriteLine("Введите количество строк:"); //Задание 2(б)
            int t = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Введите количество столбцов:");
            int i = Convert.ToInt32(Console.ReadLine());

            int[,] array = GenerateArray(t, i);
            PrintArray(array);

            Random random = new Random();
            int RandomElement = random.Next(array.GetLength(0));

            int otvet = array[RandomElement, 2];

            Console.WriteLine("Случайный элемент третьего столбца массива:  " + otvet);
            
        }

        // Метод для генерации двумерного массива
        public static int[,] GenerateArray(int t, int i)
        {
            int[,] table = new int[t, i];
            Random random = new Random();
            for (int a = 0; a < t; a++)
            {
                for (int b = 0; b < i; b++)
                {
                    table[a, b] = random.Next(0, 9);
                }
            }
            return table;
        }

        // Метод для вывода массива в консоль
        public static void PrintArray(int[,] array)
        {
            for (int a = 0; a < array.GetLength(0); a++)
            {
                for (int b = 0; b < array.GetLength(1); b++)
                {
                    Console.Write(array[a, b] + " ");
                }
                Console.WriteLine();
            }
        }
    }
}
