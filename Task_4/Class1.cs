using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace BinaryDataLoader
{
    class Program
    {
        static void Main(string[] args)
        {
            string binaryFilePath = "students.dat";
            List<Student> students = ReadStudentsFromBinFile(binaryFilePath);
            // Вызов метода
            CreateStudentsDirectory(students);
        }

        static List<Student> ReadStudentsFromBinFile(string filePath)
        {
            List<Student> students = new List<Student>();
          
            using (FileStream fs = new FileStream(filePath, FileMode.Open))
            {
                using (BinaryReader br = new BinaryReader(fs, Encoding.UTF8))
                {
                    
                    while (br.BaseStream.Position != br.BaseStream.Length)
                    {
                        Student student = new Student
                        {
                            Name = br.ReadString(),
                            Group = br.ReadString(),
                            DateOfBirth = DateTime.FromBinary(br.ReadInt64()),
                            AverageScore = br.ReadDecimal()
                        };
                        students.Add(student);
                    }
                }
            }
            return students;
        }
        //Метод создания
        static void CreateStudentsDirectory(List<Student> students)
        {
            // Получение пути к десктопу
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            // Создание пути 
            string studentsDirectoryPath = Path.Combine(desktopPath, "Students");
            // Проверка существования директории, если нет - создание
            if (!Directory.Exists(studentsDirectoryPath))
                Directory.CreateDirectory(studentsDirectoryPath);
            // Группировка по группам
            var groupedByGroup = students.GroupBy(s => s.Group);
            // Перебор каждой группы для создания отдельного файла
            foreach (var group in groupedByGroup)
            {
                // Создание пути к файлу группы
                string groupFilePath = Path.Combine(studentsDirectoryPath, $"{group.Key}.txt");
                // Запись информации о каждом студенте в файл группы
                using (StreamWriter sw = new StreamWriter(groupFilePath))
                {
                    foreach (var student in group)
                    {
                        sw.WriteLine($"{student.Name}, {student.DateOfBirth.ToShortDateString()}, {student.AverageScore}");
                    }
                }
            }
        }
    }
    // Можно было вынести класс отдельно
    class Student
    {
        public string Name { get; set; }
        public string Group { get; set; }
        public DateTime DateOfBirth { get; set; }
        public decimal AverageScore { get; set; }
    }
}