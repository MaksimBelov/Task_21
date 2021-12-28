using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ConsoleApp1
{
    internal class Program
    {
        static int directionX = 1; // маркер направления обхода по горизонтали (первый садовник)
        static int directionY = 1; // маркер направления обхода по вертикали (второй садовник)
        static int a = 10; // длина сада
        static int b = 10; // ширина сада
        static string[,] sad = new string[a, b];
        static int efficienсy1 = 200; // задержка для первого садовника (условная "производительность")
        static int efficienсy2 = 100; // задержка для второго садовника (условная "производительность")
        static int flag = 0;

        static void Gardener1() // моделирует работу первого садовника
        {
            for (int y = b - 1; y >= 0; y--)
            {
                if (directionX == 1)
                {
                    for (int x = 0; x <= a - 1; x++)
                    {
                        if (sad[x, y] != "2" && sad[x, y] != "Т")// кириллическая T
                        {
                            if (sad[x, y] != "V")
                                sad[x, y] = "1";
                            else
                                sad[x, y] = "T";// латинская T
                            Thread.Sleep(efficienсy1);
                        }
                        else { continue; }
                    }
                }
                else
                {
                    for (int x = a - 1; x >= 0; x--)
                    {
                        if (sad[x, y] != "2" && sad[x, y] != "Т")// кириллическая T
                        {
                            if (sad[x, y] != "V")
                                sad[x, y] = "1";
                            else
                                sad[x, y] = "T";// латинская T
                            Thread.Sleep(efficienсy1);
                        }
                        else { continue; }
                    }
                }
                directionX = (-1) * directionX; //меняем направление хода по горизонтали
            }
        }

        static void Gardener2() // моделирует работу второго садовника
        {
            for (int x = a - 1; x >= 0; x--)
            {
                if (directionY == 1)
                {
                    for (int y = 0; y <= b - 1; y++)
                    {
                        if (sad[x, y] != "1" && sad[x, y] != "T")// латинская T
                        {
                            if (sad[x, y] != "V")
                                sad[x, y] = "2";
                            else
                                sad[x, y] = "Т";// кириллическая T
                            Thread.Sleep(efficienсy2);
                        }
                        else { continue; }
                    }
                }
                else
                {
                    for (int y = b - 1; y >= 0; y--)
                    {
                        if (sad[x, y] != "1" && sad[x, y] != "T")// латинская T
                        {
                            if (sad[x, y] != "V")
                                sad[x, y] = "2";
                            else
                                sad[x, y] = "Т";// кириллическая T
                            Thread.Sleep(efficienсy2);
                        }
                        else { continue; }
                    }
                }
                directionY = (-1) * directionY; //меняем направление хода по вертикали
            }
        }

        static void Print() // анимирует работу садовников
        {
            Console.Clear();
            for (int y = b - 1; y >= 0; y--)
            {
                for (int x = 0; x <= a - 1; x++)
                {
                    if (sad[x, y] == "1" || sad[x, y] == "T") // латинская T
                        Console.ForegroundColor = ConsoleColor.Red; // красный цвет - первый садовник
                    else if (sad[x, y] == "2" || sad[x, y] == "Т") // кириллическая T
                        Console.ForegroundColor = ConsoleColor.Green; // зеленый цвет - второй садовник
                    else
                        Console.ForegroundColor = ConsoleColor.DarkBlue; // синий цвет - необработанный участок

                    Console.Write($" {sad[x, y]} ");
                }
                Console.WriteLine();
            }
            Thread.Sleep(Math.Min(efficienсy1, efficienсy2));
        }

        static void Main(string[] args)
        {
            // формируем "план" сада (V - дерево, 0 - пустой участок)
            Random random = new Random();
            for (int y = b - 1; y >= 0; y--)
            {
                for (int x = 0; x <= a - 1; x++)
                {
                    int tree = random.Next(2);
                    if (tree == 1)
                        sad[x, y] = "V";
                    else
                        sad[x, y] = "0";
                }
            }

            // выводим "план" сада
            Console.WriteLine("План сада (V - дерево, 0 - пустой участок):");
            Console.WriteLine();
            for (int y = b - 1; y >= 0; y--)
            {
                for (int x = 0; x <= a - 1; x++)
                {
                    Console.Write($" {sad[x, y]} ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
            Console.Write("Нажмите любую клавишу...");
            Console.ReadKey();

            // запускаем потоки
            Thread thread1 = new Thread(Gardener1);
            Thread thread2 = new Thread(Gardener2);
            thread1.Start();
            thread2.Start();

            // анимируем работу садовников
            while (flag == 0)
            {
                foreach (string x in sad)
                {
                    if (x == "0" || x == "V")
                    {
                        flag = 0;
                        break;
                    }
                    else
                        flag = 1;
                }
                Print();
            }
            Console.ReadKey();
        }
    }
}
