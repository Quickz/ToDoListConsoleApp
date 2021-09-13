using System;
using System.Linq;

namespace ToDoListConsoleApp
{
    class Program
    {
        private static AppContext database;

        private static void Main()
        {
            database = new AppContext();

            while (true)
            {
                string input = Console.ReadLine();

                if (input == "add")
                    Add();
                else if (input == "remove")
                    Remove();
                else if (input == "print")
                    Print();
                else if (input == "exit")
                    break;
            }
        }

        private static void Add()
        {
            Console.Write("Task description: ");
            string description = Console.ReadLine();
            database.Tasks.Add(new Task() { Description = description });
            database.SaveChanges();
        }

        private static void Remove()
        {
            Console.Write("Task id: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
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
    }
}
