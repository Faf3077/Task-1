using System;
using System.Collections.Generic;
using System.IO;

namespace dirfil
{
    class Program
    {
        static List<string> AllFiles = new List<string>();

        static void Main(string[] args)
        {
            //путь корневой директории
            string MainDirectoryPath = @"C:\Users\Андрей\Downloads\Task-1\Task-1\bin\Debug\test";
            //Вызов метода для просмотра содержимого корневой папки
            GetAllFoldersFromDirectory(MainDirectoryPath);
            Console.WriteLine("Изначальный порядок файлов:");
            foreach (string s in AllFiles)
            {
                Console.WriteLine(s);
            }
            Console.WriteLine("\n —-------------------- \n");
            for (int i = 0; i < AllFiles.Count; i++)
            {
                for (int j = 0; j < AllFiles.Count - 1; j++)
                {
                    if (FileCompare(Path.GetFileName(AllFiles[j]), Path.GetFileName(AllFiles[j + 1])))
                    {
                        string s = AllFiles[j];
                        AllFiles[j] = AllFiles[j + 1];
                        AllFiles[j + 1] = s;
                    }
                }
            }
            Console.WriteLine("Порядок файлов после сортировки:");
            foreach (string s in AllFiles)
            {
                Console.WriteLine(s);
            }
            Console.WriteLine("\n —-------------------- \n");
            //Объединение текстов всех файлов в единую строку
            string AllFileString = "";
            foreach (string NextFile in AllFiles)
            {
                AllFileString += File.ReadAllText(NextFile);
            }
            Console.WriteLine("Объеденённый из всех файлов текст: ");
            Console.WriteLine(AllFileString);
            File.WriteAllText(MainDirectoryPath + @"\Out.txt", AllFileString);
            Console.ReadLine();
        }
        static void GetAllFoldersFromDirectory(string DirectoryPath)
        {
            //Пройти по всем папкам внутри проверяемой
            foreach (string NextDirectory in Directory.GetDirectories(DirectoryPath))
            {
                //Вызвать для обнаруженой папки данный метод
                GetAllFoldersFromDirectory(NextDirectory);
            }
            //Перебрать все файлы с расширением .txt в проверяемой папке
            foreach (string NexFile in Directory.GetFiles(DirectoryPath))
            {
                AllFiles.Add(NexFile);
            }
        }
        /* Сравнеие по true и false */
        static bool FileCompare(string FileNameA, string FileNameB)
        {
            FileNameA = FileNameA.ToLower();
            FileNameB = FileNameB.ToLower();
            /* Проверка какая из строк длиннее */
            if (FileNameA.Length > FileNameB.Length)
            {
                //Перебор всех символов в строке и сравнение их числового кода
                for (byte i = 0; i < FileNameA.Length; i++)
                {
                    if ((int)FileNameA[i] > (int)FileNameB[i])
                    {
                        return true;
                    }
                }
                return false;
            }
            else
            {
                //Перебор всех символов в строке и сравнение их числового кода
                for (byte i = 0; i < FileNameB.Length; i++)
                {
                    if ((int)FileNameB[i] > (int)FileNameA[i])
                    {
                        return false;
                    }
                }
                return true;
            }
        }    
    }
}