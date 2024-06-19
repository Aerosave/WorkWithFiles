namespace Counter
{
    public class Counter
    {
        static void Main()
        {

            Console.WriteLine("Введите путь до папки:");
            string directoryPath = Console.ReadLine();

            directoryPath = RemoveQuotes(directoryPath);


            // Проверка существования папки
            if (!Directory.Exists(directoryPath))
            {
                Console.WriteLine("Путь не существует.");
                return;
            }

            try
            {
                // Вызов метода для получения размера директории
                long size = GetDirectorySize(directoryPath);
                Console.WriteLine("Размер папки в байтах: " + size);
                Console.ReadKey();
            }
            catch (UnauthorizedAccessException)
            {
                // Обработка ошибки отсутствия прав доступа
                Console.WriteLine("Нет прав доступа к одной из папок или файлов.");
            }
            catch (Exception ex)
            {
                // Обработка остальных ошибок
                Console.WriteLine($"Произошла ошибка: {ex.Message}");
            }
        }


        // Метод для рекурсивного подсчета размера директории
        public static long GetDirectorySize(string dirPath)
        {

            long size = 0;

            // Получение всех файлов в директории и подсчет их размера
            FileInfo[] files = new DirectoryInfo(dirPath).GetFiles();
            foreach (FileInfo file in files)
            {
                size += file.Length;
            }

            // Получение всех поддиректорий и рекурсивный подсчет их размера
            DirectoryInfo[] dirs = new DirectoryInfo(dirPath).GetDirectories();
            foreach (DirectoryInfo dir in dirs)
            {
                size += GetDirectorySize(dir.FullName);
            }
            return size;
        }

        // Метод для удаления двойных кавычек из строки при копировании
        public static string RemoveQuotes(string input)
        {
            return input.Replace("\"", "");
        }
    }
}