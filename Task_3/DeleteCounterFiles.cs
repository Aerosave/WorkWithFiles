using System;
using DeleteFile;
using Counter;
using System.Diagnostics.Metrics;

namespace DeleteCounterFile
{

    public class DeleteCounterFile
    {
        static void Main(string[] args)
        {
            // Запрос пути до папки у пользователя
            Console.WriteLine("Введите путь до папки:");
            string directoryPath = Console.ReadLine();

            directoryPath = DeleteFile.DeleteFile.RemoveQuotes(directoryPath);

            // Проверка существования папки
            if (!Directory.Exists(directoryPath))
            {
                Console.WriteLine("Папка не существует.");
                return;
            }

            try
            {
                // Вызов метода для получения размера директории
                long size = Counter.Counter.GetDirectorySize(directoryPath);
                Console.WriteLine("Размер папки в байтах: " + size);
                Console.WriteLine("Для удаления старых файлов введите - 'Удалить', если не хотите удалять нажмите 'Enter' или что-нибудь другое ");
                var answer = Console.ReadLine();
                // Удаление файлов
                if (answer == "Удалить")
                {
                    DirectoryInfo directInfo = new DirectoryInfo(directoryPath);
                    DeleteFile.DeleteFile.CleanDirectory(directInfo);
                    long newSize = Counter.Counter.GetDirectorySize(directoryPath);
                    long deletedSize = size - newSize;
                    Console.WriteLine("Удалено байтов: " + deletedSize);
                    Console.WriteLine("Текущий размер папки в байтах: " + newSize);

                }
                else
                {
                    Console.WriteLine("Удаление файлов отменено.");
                }
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
    }
}


