using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace ToDoListConsoleApp
{
    class Program
    {
        private static AppContext database;

        private static void Main()
        {
            database = new AppContext(GetDatabasePath());
            Help();

            while (true)
            {
                Console.Write("> ");
                string input = Console.ReadLine().ToLower();

                if (input == "add")
                    Add();
                else if (Regex.IsMatch(input, "^add .{0,}$"))
                    Add(input[(input.IndexOf(' ') + 1)..^0]);
                else if (input == "remove")
                    Remove();
                else if (Regex.IsMatch(input, "^remove [0-9]{0,}$"))
                    Remove(input.Split(' ')[1]);
                else if (input == "print")
                    Print();
                else if (input == "help")
                    Help();
                else if (input == "clear")
                    Clear();
                else if (input == "exit")
                    break;
                else
                    Console.WriteLine("Invalid input!");
            }
        }

        private static string GetDatabasePath()
        {
            string targetPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                AppSettings.DatabaseTargetDirectory,
                AppSettings.DatabaseFileName);

            if (!File.Exists(targetPath))
            {
                string sourcePath = Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory,
                    AppSettings.DatabaseFileName);

                Directory.CreateDirectory(Path.GetDirectoryName(targetPath));
                File.Copy(sourcePath, targetPath);
            }

            return targetPath;
        }

        private static void Add()
        {
            Console.Write("Task description: ");
            Add(Console.ReadLine());
        }

        private static void Add(string taskDescription)
        {
            database.Tasks.Add(new Task() { Description = taskDescription });
            database.SaveChanges();
        }

        private static void Remove()
        {
            Console.Write("Task id: ");
            Remove(Console.ReadLine());
        }

        private static void Remove(string idString)
        {
            if (!int.TryParse(idString, out int id))
            {
                Console.WriteLine("Invalid ID!");
                return;
            }

            Task taskToRemove = database.Tasks.FirstOrDefault(task => task.Id == id);

            if (taskToRemove == null)
            {
                Console.WriteLine("No such task exists!");
                return;
            }

            database.Tasks.Remove(taskToRemove);
            database.SaveChanges();
        }
        
        private static void Print()
        {
            Console.WriteLine("Tasks:");
            foreach (Task task in database.Tasks)
            {
                Console.WriteLine($"{task.Id}) {task.Description}");
            }
        }

        private static void Help()
        {
            Console.WriteLine("Commands: add, remove, print, help, exit");
        }

        private static void Clear()
        {
            Console.Clear();
        }
    }
}
