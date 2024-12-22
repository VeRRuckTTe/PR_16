/************************************************************************************
/* Практическая работа №16                                                          *
/* Выполнила: Корнеева В.Е., 2-ИСП                                                  *
/* Задание: Файлы                                                                   *
/************************************************************************************/

using System.IO;
using System;

namespace Урок_16
{
    class Program
    {
        static void Errors(Exception exception)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Упс.. ошибочка вышла: {exception.Message}");
        }
        static void Fill(FileStream fileStream, uint n)
        {
            StreamWriter writer = new StreamWriter(fileStream);
            for (int i = 0; i < n; i++)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("\nведите число: ");
                double number = Double.Parse(Console.ReadLine());
                if (number < -15.5 || number > 9.59) throw new ArgumentException();
                writer.Write(number + " ");
            }
            writer.Close();
        }
        static void Read(FileStream fileStream)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("\nВ файле находится: ");
            StreamReader reader = new StreamReader(fileStream);
            string line = reader.ReadLine();
            line = line.TrimEnd();
            string[] lines = line.Split(' ');
            foreach (string item in lines)
            {
                Console.Write(item + " ");
            }
            Console.WriteLine();

            short[] numbers = new Int16[0];
            int i = 0;
            foreach (string item in lines)
            {
                Array.Resize(ref numbers, numbers.Length + 1);
                numbers[i] = Int16.Parse(item);
                i++;
            }
            short min = numbers[0];

            foreach (short item in numbers)
            {
                if (item < min) min = item;
            }

            short max = Math.Abs(numbers[0]);
            foreach (short item in numbers)
            {
                if (Math.Abs(item) > max) max = Math.Abs(item);
            }
            i = 0;
            foreach (short item in numbers)
            {
                if (Math.Abs(item) == max) numbers[i] = min;
                i++;
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Изменённая последовательность: ");
            foreach (short item in numbers)
            {
                Console.Write(item + " ");
            }
            reader.Close();
        }
        static void Main(string[] args)
        {
            for (; ; )
            {
                try
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("Введите название файла: ");
                    string name = Console.ReadLine();
                    if (String.IsNullOrEmpty(name)) 
                    {
                        throw new Exception("Вы ничего не ввели или ввели некорректно");
                    }
                    foreach (char c in name)
                    {
                        if (Char.IsDigit(c))
                        {
                            throw new Exception("В вашем тексте не должны быть числа");
                        }
                    }
                    Console.Write("Введите количество чисел: ");
                    uint n = UInt32.Parse(Console.ReadLine());
                    FileStream fileStream = new FileStream(name, FileMode.Create, FileAccess.Write);
                    Fill(fileStream, n);
                    FileStream fileStream1 = new FileStream(name, FileMode.Open, FileAccess.Read);
                    Read(fileStream1);
                    fileStream.Close();
                    Console.ReadLine();
                }
                catch (ArgumentException aex)
                {
                    Console.Clear();
                    Errors(aex);
                }
                catch (Exception ex)
                {
                    Console.Clear();
                    Errors(ex);
                }
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("Хотите выполнить команду еще раз? \nНажмите Y для продолжение программы, иначе любую другую кнопку для завершения!");
                if (Console.ReadKey().Key == ConsoleKey.Y)
                {
                    Console.Clear();
                    continue;

                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Программа завершена. \tДо свидания!");
                    Console.ReadKey();
                    break;
                }
            }
        }
    }
}