using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace EmployeeManagement
{
    class LinqToDataTable
    {
        public void AddToDataTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add("ID");
            table.Columns.Add("ProductName");

            table.Rows.Add("1", "Chai");
            table.Rows.Add("2", "Quseo Cabrales");
            table.Rows.Add("3", "tofu");

            DisplayProductsFromTable(table);
        }
        public void DisplayProductsFromTable(DataTable table)
        {
            var productNames = from products in table.AsEnumerable() select products.Field<string>("ProductName");
            Console.WriteLine("Product Names : ");
            foreach (string productName in productNames)
            {
                Console.WriteLine(productName);
            }
            Console.WriteLine("------------------");
            //DataColumn column in loadDT.Columns
            foreach (DataColumn column in table.Columns)
            {
                Console.WriteLine("Column Name : " + column);

            }
        }
    }
}