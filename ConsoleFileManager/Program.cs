using System;
using System.IO;
using System.Xml.Serialization;
using System.Threading;

namespace ConsoleFileManager
{
    public class SerializeClass
    {
        public string Path { get; set; }
        public int PageSize { get; set; }
        public int Page { get; set; }
    }

    internal class Program
    {
        static string[] dirs;
        static string[] files;
        static string[] list;
        static int pageSize = 10;
        static string system = Environment.GetFolderPath(Environment.SpecialFolder.System);
        static string path = Path.GetPathRoot(system);
        static int pages;
        const string serializeFileName = "serialize.xml";

        //Метод, определяющий общий размер файлов в дирректории
        static long SizeAllFiles(DirectoryInfo info)
        {
            long size = 0;
            FileInfo[] files = info.GetFiles();
            for (int i = 0; i < files.Length; i++)
            {
                size += files[i].Length;
            }
            return size;
        }


        //Конвертация байт в килобайт, мегабайт и т.д. Возвращает текст в виде "{число} мегабайт"
        static string ConvertSize(long size)
        {
            double sizeD = Convert.ToDouble(size);
            string convert = String.Empty;
            int counter = 0;
            while (true)
            {
                sizeD /= 1024;
                if (sizeD < 1)
                {
                    sizeD *= 1024;
                    break;
                }
                counter++;
            }
            convert = Math.Round(sizeD, 1).ToString();

            switch (counter)
            {
                case 0:
                    return $"{convert} байт";
                case 1:
                    return $"{convert} килобайт";
                case 2:
                    return $"{convert} мегабайт";
                default:
                    return $"{convert} гигабайт";
            }
        }

        static void CommandLine(string path)
        {
            Console.Write($"\n{path}>");
        }

        static T[] CombineArray<T>(T[] array1, T[] array2)
        {
            T[] result = new T[array1.Length + array2.Length];
            for (int i = 0; i < result.Length; i++)
            {
                if (i < array1.Length)
                {
                    result[i] = array1[i];
                }
                else
                {
                    result[i] = array2[i - array1.Length];
                }
            }
            return result;
        }

        //Вывод списка файлов и дирректорий
        static void Print(string path, int page)
        {
            try
            {
                dirs = Directory.GetDirectories(path);
                files = Directory.GetFiles(path);

                if (dirs.Length == 0 & files.Length == 0)
                {
                    Console.WriteLine("Пустая дирректория");
                }
                list = CombineArray(dirs, files);

                Console.Clear();

                pages = Convert.ToInt32(Math.Round(((float)list.Length / pageSize), MidpointRounding.ToPositiveInfinity));
                if (page > pages)
                {
                    page = pages;
                }
                if (page <= 0)
                {
                    page = 1;
                }

                int skipFiles = pageSize * (page - 1);

                for (int i = skipFiles; i < skipFiles + pageSize; i++)
                {
                    if (list.Length <= i)
                    {
                        break;
                    }
                    if (i < dirs.Length)
                    {
                        Console.BackgroundColor = ConsoleColor.Yellow;
                    }
                    else
                    {
                        Console.BackgroundColor = ConsoleColor.Green;
                    }
                    Console.Write(" ");
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.Write($" {Path.GetFileName(list[i])}\n");

                }

                DirectoryInfo info = new DirectoryInfo(path);
                Console.WriteLine();
                Console.WriteLine($"Страница: {page} из {pages}");
                Console.WriteLine($"Время создания: {info.CreationTime}");
                Console.WriteLine($"Время изменения: {info.LastWriteTime}");
                Console.WriteLine($"Количество файлов: {files.Length}");
                Console.WriteLine($"Количество директорий: {dirs.Length}.");
                Console.WriteLine($"Размер файлов в каталоге: {ConvertSize(SizeAllFiles(info))}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                DirectoryInfo directoryInfo = new DirectoryInfo(path);
                Program.path = directoryInfo?.Parent?.FullName;
            }
        }


        //Метод, возвращающий массив строк из строки через символ разделитель
        static string[] SeparateString(string text, char separator)
        {
            int counter = 0;
            int iterator = 0;
            //вычисление количества разделителей для инициализации массива
            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] == separator)
                {
                    counter++;
                }
            }
            //инициализация массива строк
            string[] result = new string[counter + 1];
            counter = 0;
            //запись символов в массив строк
            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] == separator)
                {
                    char[] a = new char[i - iterator];
                    for (int j = 0; j < a.Length; j++)
                    {
                        a[j] = text[j + iterator];
                    }
                    result[counter] = new string(a);
                    counter++;
                    iterator = i + 1;
                    continue;
                }
                if (i == text.Length - 1)
                {
                    char[] a = new char[i - iterator + 1];
                    for (int j = 0; j < a.Length; j++)
                    {
                        a[j] = text[j + iterator];
                    }
                    result[counter] = new string(a);
                    continue;
                }
            }
            //избавление от возможных null в массиве
            for (int i = 0; i < result.Length; i++)
            {
                if (result[i] == null)
                {
                    result[i] = new string(String.Empty);
                }
            }
            return result;
        }


        //Метод, получения подстроки до первого пробела
        static string SeparateCommand(string text)
        {
            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] == ' ')
                {
                    char[] chars = new char[i];
                    for (int j = 0; j < chars.Length; j++)
                    {
                        chars[j] = text[j];
                    }
                    return new string(chars);
                }
            }
            return text;
        }

        //Сравнение текста по регистру
        static bool IsEquals(string input)
        {
            for (int i = 0; i < dirs.Length; i++)
            {
                string reference = Path.GetFileName(dirs[i]);
                if (reference == input) { return true; }
            }
            for (int i = 0; i < files.Length; i++)
            {
                string reference = Path.GetFileName(files[i]);
                if (reference == input) { return true; }
            }
            return false;
        }

        //Сериализация
        static void Serialize(SerializeClass serialize)
        {
            StringWriter writer = new StringWriter();
            XmlSerializer serializer = new XmlSerializer(typeof(SerializeClass));
            serializer.Serialize(writer, serialize);
            string xml = writer.ToString();
            File.WriteAllText(serializeFileName, xml);
        }

        //Копирование директории
        static void CopyDirectory(string path, string pathCopy)
        {
            Directory.CreateDirectory(pathCopy);
            dirs = Directory.GetDirectories(path);
            files = Directory.GetFiles(path);
            for (int i = 0; i < files.Length; i++)
            {
                File.Copy(files[i], Path.Combine(pathCopy, Path.GetFileName(files[i])));
            }
            for (int i = 0; i < dirs.Length; i++)
            {
                CopyDirectory(Path.Combine(path, dirs[i]), Path.Combine(pathCopy, Path.GetFileName(dirs[i])));
            }
        }


        static void Main()
        {
            //Команды при запуске программы. Десериализация и вывод списка файлов, дирректорий, информации о дирректории
            SerializeClass serialize = new SerializeClass();
            int page = 1;
            if (File.Exists(serializeFileName))
            {
                try
                {
                    string xmlText = File.ReadAllText(serializeFileName);
                    StringReader reader = new StringReader(xmlText);
                    XmlSerializer xml = new XmlSerializer(typeof(SerializeClass));
                    serialize = (SerializeClass)xml.Deserialize(reader);
                    path = serialize.Path;
                    pageSize = serialize.PageSize;
                    page = serialize.Page;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Произошла ошибка при десериализации: {ex.Message}\n" +
                        $"Для продолжения нажмите любую клавишу.");
                    Console.ReadKey(true);
                }
            }
            Print(path, page);
            Console.WriteLine("\nЧтобы посмотреть список команд введите cmdlist");
            CommandLine(path);

            //Цикл для ввода команд
            while (true)
            {
                string input = Console.ReadLine();
                string command = SeparateCommand(input);
                string[] args = SeparateString(input, '\'');

                try
                {
                    //Командой switch реализовываем парсер команд
                    switch (command)
                    {
                        case "cd":
                            if (!IsEquals(args[1]))
                            {
                                Console.WriteLine("Файл или дирректория не найдены");
                                break;
                            }
                            page = 1;
                            path = Path.Combine(path, args[1]);
                            Print(path, page);
                            serialize.Path = path;
                            Serialize(serialize);
                            break;

                        case "back":
                            page = 1;
                            DirectoryInfo directoryInfo = new DirectoryInfo(path);
                            if (directoryInfo.Parent != null)
                            {
                                Program.path = directoryInfo.Parent.FullName;
                            }
                            Print(path, page);
                            serialize.Path = path;
                            Serialize(serialize);
                            break;


                        case "mkdir":
                            Directory.CreateDirectory(Path.Combine(path, args[1]));
                            Print(path, page);
                            break;


                        case "delete":
                            if (!IsEquals(args[1]))
                            {
                                Console.WriteLine("Файл или дирректория не найдены");
                                break;
                            }
                            Console.WriteLine($"Вы действительно удалить \"{args[1]}\"? Введите команду yes, если да или no, если нет.");
                            string commandText = Console.ReadLine();
                            if (commandText == "yes")
                            {
                                FileInfo info = new FileInfo(Path.Combine(path, args[1]));
                                if (info.Attributes == FileAttributes.Directory)
                                {
                                    Directory.Delete(Path.Combine(path, args[1]), true);
                                }
                                else
                                {
                                    File.Delete(Path.Combine(path, args[1]));
                                }
                            }
                            Print(path, page);
                            break;

                        case "clear":
                            Print(path, page);
                            break;

                        case "pn":
                            if (page < pages)
                            {
                                page++;
                            }
                            Print(path, page);
                            serialize.Page = page;
                            Serialize(serialize);
                            break;

                        case "pp":
                            if (page > 1)
                            {
                                page--;
                            }
                            Print(path, page);
                            serialize.Page = page;
                            Serialize(serialize);
                            break;

                        case "p":
                            page = int.Parse(args[1]);
                            Print(path, page);
                            serialize.Page = page;
                            Serialize(serialize);
                            break;

                        case "copy":
                            if (!IsEquals(args[1]))
                            {
                                Console.WriteLine("Файл или дирректория не найдены");
                                break;
                            }
                            FileInfo info1 = new FileInfo(Path.Combine(path, args[1]));
                            if (info1.Attributes == FileAttributes.Directory)
                            {
                                CopyDirectory(Path.Combine(path, args[1]), Path.Combine(path, args[3]));
                            }
                            else
                            {
                                File.Copy(Path.Combine(path, args[1]), Path.Combine(path, args[3]));
                            }
                            Console.WriteLine("Успешно скопировано");
                            Thread.Sleep(1000);
                            Print(path, page);
                            break;

                        case "exit":
                            return;

                        case "pagesize":
                            pageSize = int.Parse(args[1]);
                            Print(path, page);
                            serialize.PageSize = pageSize;
                            Serialize(serialize);
                            break;

                        case "info":
                            if (!IsEquals(args[1]))
                            {
                                Console.WriteLine("Файл или дирректория не найдены");
                                break;
                            }
                            FileInfo fileInfo = new FileInfo(Path.Combine(path, args[1]));
                            Console.WriteLine($"Имя: {fileInfo.Name}");
                            if (fileInfo.Attributes.HasFlag(FileAttributes.Directory))
                            {
                                Console.WriteLine("Тип: Дирректория");
                            }
                            else
                            {
                                Console.WriteLine("Тип: Файл");
                                Console.WriteLine($"Размер: {ConvertSize(fileInfo.Length)}");
                                Console.WriteLine($"Расширение: {fileInfo.Extension}");
                                Console.WriteLine($"Аттрибуты: {fileInfo.Attributes}");
                            }
                            Console.WriteLine($"Время последнего доступа: {fileInfo.LastAccessTime}");
                            Console.WriteLine($"Время последнего изменения:{fileInfo.LastWriteTime}");
                            Console.WriteLine($"Время создания: {fileInfo.CreationTime}");
                            break;

                        case "":
                            Console.WriteLine($"Команда не распознана.");
                            break;

                        case "cmdlist":
                            Console.WriteLine("cd - переход к следующей дирректории\n" +
                                "back - возврат к предыдущей дирректории\n" +
                                "mkdir - создание дирректории\n" +
                                "delete - удаление\n" +
                                "copy - копирование\n" +
                                "move - перемещение\n" +
                                "clear - очистка консоли\n" +
                                "pn - следующая страница\n" +
                                "pp - предыдущая страница\n" +
                                "p 'x' - переход на \'x\' страницу\n" +
                                "exit - выход из программы\n" +
                                "info - информация о файле или дирректории\n" +
                                "pagesize - изменить количество элементов на странице\n");
                            break;

                        default:
                            Console.WriteLine($"Команда \"{command}\" не распознана.");
                            break;
                    }

                }
                //catch (IndexOutOfRangeException)
                //{
                //    Console.WriteLine("Команда введена неверно. Вводите путь через одинарные ковычки");
                //}
                //catch (Exception ex)
                //{
                //    Console.WriteLine(ex.Message);
                //}
                finally
                {
                    CommandLine(path);
                }
            }
        }
    }
}