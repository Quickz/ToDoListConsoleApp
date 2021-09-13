using System;

namespace ToDoListConsoleApp
{
    class Program
    {
        private static AppContext database;

        private static void Main()
        {
            database = new AppContext();
            database.Tasks.Add(new Task() { Description = "Blank task" });
            database.SaveChanges();

            Console.WriteLine("Tasks:");
            foreach (Task task in database.Tasks)
            {
                Console.WriteLine($"{task.Id}. {task.Description}");
            }

            Console.ReadKey();
        }
    }
}
