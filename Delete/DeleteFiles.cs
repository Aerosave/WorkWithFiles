namespace DeleteFile
{
    public class DeleteFile
    {
        /// <summary>
        /// Метод исполнитель
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            // Запрос пути до папки у пользователя
            Console.WriteLine("Введите путь до папки:");
            string directoryPath = Console.ReadLine();

            directoryPath = RemoveQuotes(directoryPath);

            // Проверка существования папки
            if (!Directory.Exists(directoryPath))
            {
                Console.WriteLine("Папка не существует.");
                return;
            }

            try
            {
                // Создание объекта DirectoryInfo для работы с папкой
                DirectoryInfo directInfo = new DirectoryInfo(directoryPath);
                // Очистка папки
                CleanDirectory(directInfo);
                Console.WriteLine("Очистка завершена.");
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

        // Метод для очистки папки
        public static void CleanDirectory(DirectoryInfo directoryInfo)
        {
            // Интервал времени для проверки неиспользованных файлов
            TimeSpan interval = TimeSpan.FromMinutes(30);

            // Удаление файлов
            foreach (FileInfo file in directoryInfo.GetFiles())
            {
                try
                {
                    // Проверка времени последнего доступа к файлу
                    if (DateTime.Now - file.LastAccessTime > interval)
                    {
                        // Удаление файла
                        file.Delete();
                        Console.WriteLine($"Файл {file.Name} удален.");
                    }
                }
                catch (Exception ex)
                {
                    // Обработка ошибок при удалении файла
                    Console.WriteLine($"Не удалось удалить файл {file.Name}: {ex.Message}");
                }
            }

            // Удаление папок
            foreach (DirectoryInfo dir in directoryInfo.GetDirectories())
            {
                try
                {
                    // Проверка времени последнего доступа к папке
                    if (DateTime.Now - dir.LastAccessTime > interval)
                    {
                        // Удаление папки
                        dir.Delete(true);
                        Console.WriteLine($"Папка {dir.Name} удалена.");
                    }
                    else
                    {
                        // Рекурсивная очистка подпапок
                        CleanDirectory(dir);
                    }
                }
                catch (Exception ex)
                {
                    // Обработка ошибок при удалении папки
                    Console.WriteLine($"Не удалось удалить папку {dir.Name}: {ex.Message}");
                }
            }
        }

        // Метод для удаления двойных кавычек из строки при копировании
        public static string RemoveQuotes(string input)
        {
            return input.Replace("\"", "");
        }
    }
}