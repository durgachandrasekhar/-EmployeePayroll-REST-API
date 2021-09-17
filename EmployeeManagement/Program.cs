using System;

namespace EmployeeManagement
{
    class Program
    {
        static void Main(string[] args)
        {
            //Welcome Message
            Console.WriteLine("Welcome To Employee Management!");
            LinqToDataTable table = new LinqToDataTable();
            table.AddToDataTable();
        }
    }
}